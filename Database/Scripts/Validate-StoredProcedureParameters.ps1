<#
.SYNOPSIS
    Validates that DAO method parameter names match stored procedure parameter names.

.DESCRIPTION
    For each stored procedure called in DAO files:
    1. Gets the SP parameter list from MySQL
    2. Finds the DAO method that calls it
    3. Compares parameter names (DAO sends without p_ prefix, SP expects with p_ prefix)
    4. Reports mismatches

    This version uses tokenization instead of regex for robust parsing of Dictionary declarations.

.EXAMPLE
    .\Validate-StoredProcedureParameters.ps1
#>

param(
    [string]$MySqlPath = "C:\MAMP\bin\mysql\bin\mysql.exe",
    [string]$ServerHost = "localhost",
    [int]$Port = 3306,
    [string]$User = "root",
    [string]$DbPassword = "root",
    [string]$Database = "MTM_WIP_Application_Winforms",
    [string]$DaoDirectory = "Data",
    [string]$StoredProcedureListFile = "Database\all_stored_procedures_called.txt"
)

function Extract-DictionaryParameters {
    <#
    .SYNOPSIS
        Extracts parameter names from Dictionary<string, object> initialization in C# code.
    
    .DESCRIPTION
        Parses C# code to find Dictionary parameter assignments like ["ParameterName"] = value
        Uses character-by-character tokenization to handle multi-line declarations robustly.
    
    .PARAMETER Content
        The C# source code content to parse
    
    .PARAMETER StoredProcedureName
        The stored procedure name to look for
    
    .RETURNS
        Array of parameter names (strings) in the order they appear
    #>
    param(
        [string]$Content,
        [string]$StoredProcedureName
    )
    
    # Find the stored procedure call
    $spIndex = $Content.IndexOf("`"$StoredProcedureName`"")
    if ($spIndex -eq -1) {
        return @()
    }
    
    # Find the Dictionary initialization after the SP name
    # Look for "new Dictionary<string, object>" pattern
    $dictPattern = "new Dictionary<string, object>"
    $dictIndex = $Content.IndexOf($dictPattern, $spIndex)
    if ($dictIndex -eq -1) {
        return @()
    }
    
    # Find the opening brace of the Dictionary
    $openBraceIndex = $Content.IndexOf("{", $dictIndex)
    if ($openBraceIndex -eq -1) {
        return @()
    }
    
    # Find the matching closing brace (handle nested braces)
    $braceCount = 1
    $closeBraceIndex = $openBraceIndex + 1
    $inString = $false
    $escapeNext = $false
    
    while ($closeBraceIndex -lt $Content.Length -and $braceCount -gt 0) {
        $char = $Content[$closeBraceIndex]
        
        if ($escapeNext) {
            $escapeNext = $false
        }
        elseif ($char -eq '\') {
            $escapeNext = $true
        }
        elseif ($char -eq '"') {
            $inString = -not $inString
        }
        elseif (-not $inString) {
            if ($char -eq '{') {
                $braceCount++
            }
            elseif ($char -eq '}') {
                $braceCount--
            }
        }
        
        $closeBraceIndex++
    }
    
    if ($braceCount -ne 0) {
        Write-Verbose "Failed to find matching closing brace for Dictionary"
        return @()
    }
    
    # Extract the Dictionary content
    $dictContent = $Content.Substring($openBraceIndex + 1, $closeBraceIndex - $openBraceIndex - 2)
    
    # Parse parameter names from ["ParameterName"] = value patterns
    $parameters = @()
    $lines = $dictContent -split '\r?\n'
    
    foreach ($line in $lines) {
        $line = $line.Trim()
        
        # Skip empty lines and comments
        if ($line -eq '' -or $line.StartsWith('//')) {
            continue
        }
        
        # Look for ["ParameterName"] pattern
        if ($line -match '\["([^"]+)"\]\s*=') {
            $paramName = $Matches[1]
            $parameters += $paramName
        }
    }
    
    return $parameters
}

$ErrorActionPreference = "Stop"

Write-Host "=== Stored Procedure Parameter Validation ===" -ForegroundColor Cyan
Write-Host "Database: $Database" -ForegroundColor Gray
Write-Host "DAO Directory: $DaoDirectory" -ForegroundColor Gray
Write-Host ""

# Read list of stored procedures
if (-not (Test-Path $StoredProcedureListFile)) {
    Write-Host "ERROR: Stored procedure list file not found: $StoredProcedureListFile" -ForegroundColor Red
    exit 1
}

$storedProcedures = Get-Content $StoredProcedureListFile | Where-Object { $_ -match '\S' }
Write-Host "Found $($storedProcedures.Count) stored procedures to validate" -ForegroundColor Green
Write-Host ""

$results = @()
$totalChecked = 0
$totalIssues = 0

foreach ($spName in $storedProcedures) {
    $totalChecked++
    Write-Host "[$totalChecked/$($storedProcedures.Count)] Checking: $spName" -ForegroundColor Yellow
    
    # Get SP parameters from MySQL
    $query = @"
SELECT PARAMETER_NAME, ORDINAL_POSITION, DATA_TYPE, PARAMETER_MODE
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_NAME = '$spName'
  AND SPECIFIC_SCHEMA = '$Database'
  AND PARAMETER_MODE IN ('IN', 'INOUT')
ORDER BY ORDINAL_POSITION;
"@
    
    try {
        $spParamsOutput = & $MySqlPath -h $ServerHost -P $Port -u $User -p"$DbPassword" -D $Database -e $query 2>&1 | Where-Object { $_ -notmatch "Warning.*password" }
        
        if ($LASTEXITCODE -ne 0) {
            Write-Host "  ⚠️  Failed to query MySQL for $spName" -ForegroundColor Red
            $results += [PSCustomObject]@{
                StoredProcedure = $spName
                Status = "ERROR"
                Issue = "Failed to query MySQL"
                DaoFile = "N/A"
                DaoLineNumber = "N/A"
            }
            $totalIssues++
            continue
        }
        
        # Parse SP parameters (skip header, filter out empty lines)
        $spParams = $spParamsOutput | Select-Object -Skip 1 | Where-Object { $_ -match '\S' } | ForEach-Object {
            $parts = $_ -split '\s+' | Where-Object { $_ -match '\S' }
            if ($parts.Count -ge 2) {
                @{
                    Name = $parts[0] -replace '^p_', ''  # Remove p_ prefix for comparison
                    OriginalName = $parts[0]
                    Position = [int]$parts[1]
                }
            }
        } | Where-Object { $_ -ne $null }
        
        # Filter out output parameters (p_Status, p_ErrorMsg)
        $spInputParams = $spParams | Where-Object { $_.OriginalName -notin @('p_Status', 'p_ErrorMsg') }
        
        if ($spInputParams.Count -eq 0) {
            Write-Host "  ℹ️  No input parameters" -ForegroundColor Gray
            $results += [PSCustomObject]@{
                StoredProcedure = $spName
                Status = "OK"
                Issue = "No input parameters"
                DaoFile = "N/A"
                DaoLineNumber = "N/A"
            }
            continue
        }
        
        # Find DAO file that calls this SP
        $daoFiles = Get-ChildItem -Path $DaoDirectory -Filter "Dao_*.cs" -Recurse
        $found = $false
        
        foreach ($daoFile in $daoFiles) {
            $daoContent = Get-Content $daoFile.FullName -Raw
            
            # Look for calls to this stored procedure
            if ($daoContent -match "`"$spName`"") {
                $found = $true
                
                # Extract Dictionary parameters using the robust parser
                $daoParams = Extract-DictionaryParameters -Content $daoContent -StoredProcedureName $spName
                
                if ($daoParams.Count -eq 0) {
                    Write-Host "  ⚠️  Could not parse Dictionary parameters" -ForegroundColor Yellow
                    $results += [PSCustomObject]@{
                        StoredProcedure = $spName
                        Status = "PARSE_ERROR"
                        Issue = "Could not extract Dictionary parameters"
                        DaoFile = $daoFile.Name
                        DaoLineNumber = "N/A"
                    }
                    $totalIssues++
                    break
                }
                
                # Compare parameters
                $missing = $spInputParams | Where-Object { $_.Name -notin $daoParams } | ForEach-Object { $_.Name }
                $extra = $daoParams | Where-Object { $_ -notin ($spInputParams | ForEach-Object { $_.Name }) }
                
                # Check parameter order (order matters!)
                $orderMismatch = $false
                $orderDetails = @()
                
                if ($daoParams.Count -eq $spInputParams.Count) {
                    for ($i = 0; $i -lt $daoParams.Count; $i++) {
                        if ($daoParams[$i] -ne $spInputParams[$i].Name) {
                            $orderMismatch = $true
                            $orderDetails += "Position $($i+1): Expected '$($spInputParams[$i].Name)' but got '$($daoParams[$i])'"
                        }
                    }
                }
                
                if ($missing.Count -gt 0 -or $extra.Count -gt 0 -or $orderMismatch) {
                    $issue = @()
                    if ($missing.Count -gt 0) {
                        $issue += "Missing in DAO: $($missing -join ', ')"
                    }
                    if ($extra.Count -gt 0) {
                        $issue += "Extra in DAO: $($extra -join ', ')"
                    }
                    if ($orderMismatch) {
                        $issue += "⚠️ PARAMETER ORDER MISMATCH"
                        $issue += "  SP expects: $($spInputParams | ForEach-Object { $_.Name } | Join-String -Separator ', ')"
                        $issue += "  DAO sends:  $($daoParams -join ', ')"
                        if ($orderDetails.Count -gt 0) {
                            $issue += "  Details: $($orderDetails -join '; ')"
                        }
                    }
                    
                    Write-Host "  ❌ ISSUE: $($issue[0])" -ForegroundColor Red
                    if ($issue.Count -gt 1) {
                        foreach ($detail in $issue[1..($issue.Count-1)]) {
                            Write-Host "           $detail" -ForegroundColor Red
                        }
                    }
                    
                    $results += [PSCustomObject]@{
                        StoredProcedure = $spName
                        Status = "MISMATCH"
                        Issue = $issue -join ' | '
                        DaoFile = $daoFile.Name
                        DaoLineNumber = "See file"
                    }
                    $totalIssues++
                } else {
                    Write-Host "  ✅ OK ($($daoParams.Count) parameters match)" -ForegroundColor Green
                    $results += [PSCustomObject]@{
                        StoredProcedure = $spName
                        Status = "OK"
                        Issue = "$($daoParams.Count) parameters match in correct order"
                        DaoFile = $daoFile.Name
                        DaoLineNumber = "N/A"
                    }
                }
                
                break
            }
        }
        
        if (-not $found) {
            Write-Host "  ⚠️  Not found in DAO files" -ForegroundColor Yellow
            $results += [PSCustomObject]@{
                StoredProcedure = $spName
                Status = "NOT_FOUND"
                Issue = "SP not found in DAO files"
                DaoFile = "N/A"
                DaoLineNumber = "N/A"
            }
            $totalIssues++
        }
        
    } catch {
        Write-Host "  ❌ ERROR: $_" -ForegroundColor Red
        $results += [PSCustomObject]@{
            StoredProcedure = $spName
            Status = "ERROR"
            Issue = $_.Exception.Message
            DaoFile = "N/A"
            DaoLineNumber = "N/A"
        }
        $totalIssues++
    }
    
    Write-Host ""
}

# Summary
Write-Host "=== SUMMARY ===" -ForegroundColor Cyan
Write-Host "Total Checked: $totalChecked" -ForegroundColor White
Write-Host "Passed: $($totalChecked - $totalIssues)" -ForegroundColor Green
Write-Host "Issues Found: $totalIssues" -ForegroundColor $(if ($totalIssues -eq 0) { "GREEN" } else { "RED" })
Write-Host ""

# Export results
$resultsFile = "Database\ValidationRuns\sp-validation-$(Get-Date -Format 'yyyyMMdd-HHmmss').csv"
$results | Export-Csv -Path $resultsFile -NoTypeInformation
Write-Host "Results exported to: $resultsFile" -ForegroundColor Cyan

# Show issues
$issues = $results | Where-Object { $_.Status -ne "OK" }
if ($issues.Count -gt 0) {
    Write-Host ""
    Write-Host "=== ISSUES DETAILS ===" -ForegroundColor Red
    Write-Host "Total Issues: $($issues.Count)" -ForegroundColor Red
    Write-Host ""
    
    # Group by status
    $grouped = $issues | Group-Object Status | Sort-Object Count -Descending
    foreach ($group in $grouped) {
        Write-Host "$($group.Name): $($group.Count) issues" -ForegroundColor Yellow
    }
    
    Write-Host ""
    $issues | Format-Table -AutoSize -Wrap
    exit 1
} else {
    Write-Host "✅ All stored procedures validated successfully!" -ForegroundColor Green
    exit 0
}
