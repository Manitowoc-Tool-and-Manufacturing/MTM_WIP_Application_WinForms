<#
.SYNOPSIS
    Extract detailed SQL operations from stored procedures
.DESCRIPTION
    Parses each stored procedure to extract:
    - INSERT: table, columns, values
    - UPDATE: table, SET clauses, WHERE conditions
    - DELETE: table, WHERE conditions
    - SELECT: table, columns, WHERE conditions (for context)
#>

[CmdletBinding()]
param(
    [string]$StoredProceduresPath = ".\UpdatedStoredProcedures\ReadyForVerification",
    [string]$OutputFile = ".\sql-operations-detailed.json"
)

Write-Host "=== SQL Operation Detail Extractor ===" -ForegroundColor Cyan
Write-Host "Parsing stored procedures for detailed operations..." -ForegroundColor Gray

$allDetails = @{}
$sqlFiles = Get-ChildItem -Path $StoredProceduresPath -Recurse -Filter "*.sql"

Write-Host "Found $($sqlFiles.Count) SQL files`n" -ForegroundColor Green

foreach ($file in $sqlFiles) {
    $procName = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)
    $content = Get-Content $file.FullName -Raw
    
    $operations = @()
    $opIndex = 1
    
    # Extract INSERT statements with detailed breakdown
    $insertPattern = '(?is)INSERT\s+INTO\s+`?(\w+)`?\s*\(([^)]+)\)\s*VALUES\s*\(([^)]+)\)'
    $insertMatches = [regex]::Matches($content, $insertPattern)
    
    foreach ($match in $insertMatches) {
        $table = $match.Groups[1].Value.Trim()
        $columnsRaw = $match.Groups[2].Value
        $valuesRaw = $match.Groups[3].Value
        
        # Clean and split columns
        $columns = ($columnsRaw -replace '\s+', ' ') -split ',' | ForEach-Object { 
            $_.Trim() -replace '`', ''
        }
        
        # Clean and split values
        $values = ($valuesRaw -replace '\s+', ' ') -split ',' | ForEach-Object { 
            $_.Trim()
        }
        
        # Create column-value pairs
        $columnValuePairs = @()
        for ($i = 0; $i -lt [Math]::Min($columns.Count, $values.Count); $i++) {
            $columnValuePairs += "$($columns[$i]) = $($values[$i])"
        }
        
        $operations += @{
            Index = $opIndex++
            Type = "INSERT"
            Table = $table
            Columns = $columns
            Values = $values
            ColumnValuePairs = $columnValuePairs
            RawSQL = $match.Value.Substring(0, [Math]::Min(200, $match.Value.Length))
        }
    }
    
    # Extract UPDATE statements
    $updatePattern = '(?is)UPDATE\s+`?(\w+)`?\s+SET\s+([^;]+?)(?:WHERE\s+([^;]+?))?(?=;|COMMIT|ROLLBACK|END\s+IF)'
    $updateMatches = [regex]::Matches($content, $updatePattern)
    
    foreach ($match in $updateMatches) {
        $table = $match.Groups[1].Value.Trim()
        $setClause = ($match.Groups[2].Value -replace '\s+', ' ').Trim()
        $whereClause = if ($match.Groups[3].Success) { 
            ($match.Groups[3].Value -replace '\s+', ' ').Trim() 
        } else { 
            "NO CONDITIONS (affects all rows)" 
        }
        
        # Parse SET statements
        $setStatements = $setClause -split ',' | ForEach-Object { 
            $_.Trim() 
        } | Where-Object { $_ -ne '' }
        
        # Parse WHERE conditions
        $conditions = if ($whereClause -ne "NO CONDITIONS (affects all rows)") {
            $whereClause -split '\s+AND\s+|\s+OR\s+' | ForEach-Object { 
                $_.Trim() 
            } | Where-Object { $_ -ne '' }
        } else {
            @("NO CONDITIONS (affects all rows)")
        }
        
        $operations += @{
            Index = $opIndex++
            Type = "UPDATE"
            Table = $table
            SetClauses = $setStatements
            WhereConditions = $conditions
            RawSQL = $match.Value.Substring(0, [Math]::Min(200, $match.Value.Length))
        }
    }
    
    # Extract DELETE statements
    $deletePattern = '(?is)DELETE\s+FROM\s+`?(\w+)`?(?:\s+WHERE\s+([^;]+?))?(?=;|COMMIT|ROLLBACK|END\s+IF)'
    $deleteMatches = [regex]::Matches($content, $deletePattern)
    
    foreach ($match in $deleteMatches) {
        $table = $match.Groups[1].Value.Trim()
        $whereClause = if ($match.Groups[2].Success) { 
            ($match.Groups[2].Value -replace '\s+', ' ').Trim() 
        } else { 
            "NO CONDITIONS (deletes all rows!)" 
        }
        
        # Parse WHERE conditions
        $conditions = if ($whereClause -ne "NO CONDITIONS (deletes all rows!)") {
            $whereClause -split '\s+AND\s+|\s+OR\s+' | ForEach-Object { 
                $_.Trim() 
            } | Where-Object { $_ -ne '' }
        } else {
            @("⚠️ NO CONDITIONS (deletes all rows!)")
        }
        
        $operations += @{
            Index = $opIndex++
            Type = "DELETE"
            Table = $table
            WhereConditions = $conditions
            RawSQL = $match.Value.Substring(0, [Math]::Min(200, $match.Value.Length))
        }
    }
    
    # Extract SELECT statements (for read operations context)
    $selectPattern = '(?is)SELECT\s+(.+?)\s+FROM\s+`?(\w+)`?(?:\s+(?:WHERE|JOIN|GROUP|ORDER|LIMIT)\s+([^;]+?))?(?=;|INTO|$)'
    $selectMatches = [regex]::Matches($content, $selectPattern)
    
    $selectCount = 0
    foreach ($match in $selectMatches) {
        # Skip SELECTs that are part of INSERT INTO ... SELECT
        if ($match.Value -match '(?i)INTO\s+@?\w+') {
            continue
        }
        
        $selectCount++
        if ($selectCount -le 5) {  # Limit to first 5 SELECTs to avoid clutter
            $columnsRaw = ($match.Groups[1].Value -replace '\s+', ' ').Trim()
            $table = $match.Groups[2].Value.Trim()
            $conditions = if ($match.Groups[3].Success) { 
                ($match.Groups[3].Value -replace '\s+', ' ').Trim() 
            } else { 
                "No conditions" 
            }
            
            # Parse columns (limit to first 10 to keep it readable)
            $columns = $columnsRaw -split ',' | ForEach-Object { 
                $_.Trim() 
            } | Select-Object -First 10
            
            $operations += @{
                Index = $opIndex++
                Type = "SELECT"
                Table = $table
                Columns = $columns
                Conditions = $conditions
                RawSQL = $match.Value.Substring(0, [Math]::Min(150, $match.Value.Length))
            }
        }
    }
    
    $allDetails[$procName] = @{
        ProcedureName = $procName
        TotalOperations = $operations.Count
        Operations = $operations
    }
    
    Write-Host "  ✓ $procName - $($operations.Count) operations" -ForegroundColor Gray
}

Write-Host "`nSaving detailed operations..." -ForegroundColor Cyan
$allDetails | ConvertTo-Json -Depth 10 | Out-File -FilePath $OutputFile -Encoding UTF8
Write-Host "✓ Saved: $OutputFile" -ForegroundColor Green

Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "  Procedures analyzed: $($allDetails.Count)" -ForegroundColor White
$totalOps = ($allDetails.Values | ForEach-Object { $_.TotalOperations } | Measure-Object -Sum).Sum
Write-Host "  Total operations: $totalOps" -ForegroundColor White
