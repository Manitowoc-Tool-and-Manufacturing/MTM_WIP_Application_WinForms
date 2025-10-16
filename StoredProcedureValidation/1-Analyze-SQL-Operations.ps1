<#
.SYNOPSIS
    Extract detailed SQL operations from stored procedures (WITH PROGRESS)
.DESCRIPTION
    Parses each stored procedure to extract:
    - INSERT: table, columns, values
    - UPDATE: table, SET clauses, WHERE conditions
    - DELETE: table, WHERE conditions
    - SELECT: table, columns, WHERE conditions (for context)
    
    Reads configuration from SPSettings.json for portability
#>

[CmdletBinding()]
param(
    [string]$SettingsPath = $env:SP_SETTINGS_PATH
)

# Load settings
if (-not $SettingsPath -or -not (Test-Path $SettingsPath)) {
    $SettingsPath = Join-Path $PSScriptRoot "SPSettings.json"
}

if (-not (Test-Path $SettingsPath)) {
    Write-Host "✗ SPSettings.json not found" -ForegroundColor Red
    exit 1
}

$settings = Get-Content $SettingsPath -Raw | ConvertFrom-Json
$storedProceduresPath = Join-Path $PSScriptRoot $settings.Paths.StoredProceduresFolder
$outputFile = Join-Path $PSScriptRoot $settings.Paths.SQLOperationsJSON

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   SQL OPERATIONS ANALYSIS                             ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

Write-Host "Parsing stored procedures for detailed operations...`n" -ForegroundColor Gray

if (-not (Test-Path $storedProceduresPath)) {
    Write-Host "✗ Stored procedures folder not found: $storedProceduresPath" -ForegroundColor Red
    Write-Host "  Check StoredProceduresFolder path in SPSettings.json" -ForegroundColor Yellow
    exit 1
}

$allDetails = @{}
$sqlFiles = Get-ChildItem -Path $storedProceduresPath -Recurse -Filter "*.sql"

Write-Host "Found $($sqlFiles.Count) SQL files to analyze`n" -ForegroundColor Green

$totalFiles = $sqlFiles.Count
$currentFile = 0
$startTime = Get-Date

foreach ($file in $sqlFiles) {
    $currentFile++
    $procName = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)
    
    # Calculate progress
    $percentComplete = [math]::Round(($currentFile / $totalFiles) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    $elapsed = (Get-Date) - $startTime
    $eta = if ($currentFile -gt 0) { 
        $totalSeconds = $elapsed.TotalSeconds / $currentFile * $totalFiles
        $remaining = [TimeSpan]::FromSeconds($totalSeconds - $elapsed.TotalSeconds)
        " ETA: {0:mm}:{0:ss}" -f $remaining
    } else { "" }
    
    Write-Host ("`r[{0}{1}] {2}% ({3}/{4}) Processing: {5,-40}{6}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentFile, $totalFiles, 
        $procName.Substring(0, [Math]::Min(40, $procName.Length)), $eta) -NoNewline -ForegroundColor Yellow
    
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
        $pairs = @()
        for ($i = 0; $i -lt [Math]::Min($columns.Count, $values.Count); $i++) {
            $pairs += "$($columns[$i]) = $($values[$i])"
        }
        
        $operations += [PSCustomObject]@{
            Index = $opIndex++
            Type = "INSERT"
            Table = $table
            Columns = $columns
            Values = $values
            ColumnValuePairs = $pairs
            RawSQL = $match.Value.Trim()
        }
    }
    
    # Extract UPDATE statements
    $updatePattern = '(?is)UPDATE\s+`?(\w+)`?\s+SET\s+([^;]+?)(?:WHERE\s+([^;]+?))?(?=;|COMMIT|ROLLBACK|END\s+IF)'
    $updateMatches = [regex]::Matches($content, $updatePattern)
    
    foreach ($match in $updateMatches) {
        $table = $match.Groups[1].Value.Trim()
        $setClause = $match.Groups[2].Value.Trim()
        $whereClause = $match.Groups[3].Value.Trim()
        
        # Parse SET clauses
        $setClauses = ($setClause -replace '\s+', ' ') -split ',' | ForEach-Object {
            $_.Trim()
        }
        
        # Parse WHERE conditions
        $whereConditions = if ($whereClause) {
            ($whereClause -replace '\s+', ' ') -split '\sAND\s|\sOR\s' | ForEach-Object {
                $_.Trim()
            }
        } else {
            @()
        }
        
        $operations += [PSCustomObject]@{
            Index = $opIndex++
            Type = "UPDATE"
            Table = $table
            SetClauses = $setClauses
            WhereConditions = $whereConditions
            HasWhere = ($whereConditions.Count -gt 0)
            RawSQL = $match.Value.Trim()
        }
    }
    
    # Extract DELETE statements
    $deletePattern = '(?is)DELETE\s+FROM\s+`?(\w+)`?(?:\s+WHERE\s+([^;]+?))?(?=;|COMMIT|ROLLBACK|END\s+IF)'
    $deleteMatches = [regex]::Matches($content, $deletePattern)
    
    foreach ($match in $deleteMatches) {
        $table = $match.Groups[1].Value.Trim()
        $whereClause = $match.Groups[2].Value.Trim()
        
        # Parse WHERE conditions
        $whereConditions = if ($whereClause) {
            ($whereClause -replace '\s+', ' ') -split '\sAND\s|\sOR\s' | ForEach-Object {
                $_.Trim()
            }
        } else {
            @()
        }
        
        $operations += [PSCustomObject]@{
            Index = $opIndex++
            Type = "DELETE"
            Table = $table
            WhereConditions = $whereConditions
            HasWhere = ($whereConditions.Count -gt 0)
            Warning = if ($whereConditions.Count -eq 0) { "DELETE without WHERE - affects all rows!" } else { "" }
            RawSQL = $match.Value.Trim()
        }
    }
    
    # Store results
    $allDetails[$procName] = [PSCustomObject]@{
        ProcedureName = $procName
        TotalOperations = $operations.Count
        Operations = $operations
    }
}

Write-Host "`n`n✓ Analysis complete!`n" -ForegroundColor Green

# Export to JSON
Write-Host "Writing results to JSON..." -ForegroundColor Cyan
$allDetails | ConvertTo-Json -Depth 10 | Set-Content $OutputFile -Encoding UTF8

Write-Host "✓ Output written to: $OutputFile" -ForegroundColor Green
Write-Host "  Total procedures analyzed: $totalFiles" -ForegroundColor Gray
Write-Host "  Time elapsed: $($elapsed.ToString('mm\:ss'))`n" -ForegroundColor Gray
