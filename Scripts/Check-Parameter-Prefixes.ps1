# Check-Parameter-Prefixes.ps1
# Script to verify all stored procedure calls have correct parameter prefixes

param(
    [string]$WorkspacePath = "c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms"
)

Write-Host "=== Parameter Prefix Verification Script ===" -ForegroundColor Cyan
Write-Host "Workspace: $WorkspacePath`n" -ForegroundColor Gray

# Read the database parameters file
$paramFile = Join-Path $WorkspacePath "Database_Parameters.txt"
if (-not (Test-Path $paramFile)) {
    Write-Host "ERROR: Database_Parameters.txt not found!" -ForegroundColor Red
    exit 1
}

# Parse parameters to find non-standard prefixes
$nonStandardProcedures = @{}
Get-Content $paramFile | Select-Object -Skip 1 | ForEach-Object {
    if ($_ -match '^(\S+)\s+(in_|o_)(\S+)\s+') {
        $procName = $matches[1]
        $prefix = $matches[2]
        $paramName = $matches[2] + $matches[3]
        
        if (-not $nonStandardProcedures.ContainsKey($procName)) {
            $nonStandardProcedures[$procName] = @{
                Prefix = $prefix
                Parameters = @()
            }
        }
        $nonStandardProcedures[$procName].Parameters += $paramName
    }
}

Write-Host "Found $($nonStandardProcedures.Count) stored procedures with non-standard prefixes:" -ForegroundColor Yellow
foreach ($proc in $nonStandardProcedures.Keys | Sort-Object) {
    $info = $nonStandardProcedures[$proc]
    Write-Host "  - $proc (uses $($info.Prefix) prefix, $($info.Parameters.Count) parameters)" -ForegroundColor White
}

# Search for calls to these procedures in C# code
Write-Host "`nSearching for stored procedure calls in C# code..." -ForegroundColor Cyan

$csFiles = Get-ChildItem -Path $WorkspacePath -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue |
    Where-Object { $_.FullName -notmatch '\\obj\\|\\bin\\' }

$findings = @()

foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw
    
    foreach ($procName in $nonStandardProcedures.Keys) {
        if ($content -match $procName) {
            # Check if parameters use correct prefix
            $expectedPrefix = $nonStandardProcedures[$procName].Prefix
            
            # Look for parameter dictionary after procedure name
            if ($content -match "(?s)""$procName"".*?Dictionary<string, object>\s*\(\)\s*\{(.*?)\}") {
                $paramBlock = $matches[1]
                
                # Check each parameter
                foreach ($expectedParam in $nonStandardProcedures[$procName].Parameters) {
                    $paramNameWithoutPrefix = $expectedParam -replace '^(in_|o_)', ''
                    
                    # Check if parameter is present with correct prefix
                    if ($paramBlock -match "\[""$expectedParam""\]") {
                        # Correct - has explicit prefix
                        continue
                    }
                    elseif ($paramBlock -match "\[""$paramNameWithoutPrefix""\]") {
                        # PROBLEM - missing prefix
                        $findings += [PSCustomObject]@{
                            File = $file.Name
                            FullPath = $file.FullName
                            Procedure = $procName
                            Parameter = $paramNameWithoutPrefix
                            ExpectedPrefix = $expectedPrefix
                            Status = "MISSING_PREFIX"
                        }
                    }
                }
            }
        }
    }
}

if ($findings.Count -eq 0) {
    Write-Host "`n✅ All non-standard prefix parameters are correctly specified!" -ForegroundColor Green
} else {
    Write-Host "`n❌ Found $($findings.Count) parameters missing explicit prefixes:" -ForegroundColor Red
    $findings | Format-Table -AutoSize
    
    Write-Host "`nTo fix these issues, update the parameter names in the Dictionary to include the prefix." -ForegroundColor Yellow
    Write-Host "Example: [`"PartID`"] should be [`"in_PartID`"] for transfer procedures`n" -ForegroundColor Gray
}

# Summary
Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Non-standard procedures checked: $($nonStandardProcedures.Count)" -ForegroundColor White
Write-Host "C# files scanned: $($csFiles.Count)" -ForegroundColor White
Write-Host "Issues found: $($findings.Count)" -ForegroundColor $(if ($findings.Count -eq 0) { "Green" } else { "Red" })

# Return findings for potential automated fixing
return $findings
