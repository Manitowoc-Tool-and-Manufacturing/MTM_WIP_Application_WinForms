<#
.SYNOPSIS
    Finds stored procedures referenced in C# code that don't have SQL files.

.DESCRIPTION
    This script:
    1. Scans all C# files for stored procedure references
    2. Extracts procedure names from ExecuteDataTableWithStatus calls
    3. Checks if corresponding .sql files exist in CurrentStoredProcedures folder
    4. Reports missing SQL files
    5. Generates a report of findings

.NOTES
    Created: 2025-10-14
    Part of database layer validation
#>

[CmdletBinding()]
param(
    [switch]$ShowFound,
    [switch]$Detailed
)

$ErrorActionPreference = "Stop"

# Paths
$repoRoot = Split-Path $PSScriptRoot -Parent
$proceduresFolder = Join-Path $repoRoot "Database\CurrentStoredProcedures"
$reportPath = Join-Path $repoRoot "Database\CurrentStoredProcedures\MISSING_PROCEDURES_REPORT.txt"

Write-Host "`n=== Missing Stored Procedure Detection ===" -ForegroundColor Cyan
Write-Host "Scanning codebase for stored procedure references...`n" -ForegroundColor Gray

# Get all C# files (excluding obj/bin)
$csFiles = Get-ChildItem -Path $repoRoot -Filter "*.cs" -Recurse | 
    Where-Object { $_.FullName -notmatch "\\obj\\|\\bin\\" }

Write-Host "Scanning $($csFiles.Count) C# files..." -ForegroundColor Yellow

# Regex patterns to find stored procedure names
$patterns = @(
    # Pattern 1: "procedureName" in quotes (anywhere in code)
    '"([a-z_][a-z0-9_]+)"'
)

# Track all procedure references
$procedureReferences = @{}

foreach ($csFile in $csFiles) {
    $content = Get-Content -Path $csFile.FullName -Raw
    
    foreach ($pattern in $patterns) {
        $matches = [regex]::Matches($content, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
        
        foreach ($match in $matches) {
            if ($match.Groups.Count -gt 1) {
                $procName = $match.Groups[1].Value
                
                # Filter out common false positives
                if ($procName -match '^(sp_|sys_|usr_|md_|inv_|log_|maint_|migrate_|query_)' -and 
                    $procName -notmatch '(test|example|demo|temp|debug)') {
                    
                    if (-not $procedureReferences.ContainsKey($procName)) {
                        $procedureReferences[$procName] = @{
                            Name = $procName
                            Files = @()
                            Count = 0
                        }
                    }
                    
                    $procedureReferences[$procName].Files += $csFile.FullName
                    $procedureReferences[$procName].Count++
                }
            }
        }
    }
}

Write-Host "Found $($procedureReferences.Count) unique stored procedure references`n" -ForegroundColor Cyan

# Get all SQL files in CurrentStoredProcedures
$sqlFiles = Get-ChildItem -Path $proceduresFolder -Filter "*.sql" | 
    Where-Object { $_.Name -ne "CurrentStoredProcedures.sql" } |
    ForEach-Object { $_.BaseName }

$sqlFileSet = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
foreach ($sqlFile in $sqlFiles) {
    [void]$sqlFileSet.Add($sqlFile)
}

Write-Host "Found $($sqlFileSet.Count) SQL files in CurrentStoredProcedures folder`n" -ForegroundColor Cyan

# Check each referenced procedure
$missingProcedures = @()
$foundProcedures = @()

foreach ($procName in $procedureReferences.Keys | Sort-Object) {
    $ref = $procedureReferences[$procName]
    
    if ($sqlFileSet.Contains($procName)) {
        $foundProcedures += $ref
        if ($ShowFound) {
            Write-Host "  [FOUND]   $procName (referenced $($ref.Count) times)" -ForegroundColor Green
        }
    } else {
        $missingProcedures += $ref
        Write-Host "  [MISSING] $procName (referenced $($ref.Count) times)" -ForegroundColor Red
        
        if ($Detailed) {
            foreach ($file in ($ref.Files | Select-Object -Unique)) {
                $relativePath = $file.Replace($repoRoot, "").TrimStart('\')
                Write-Host "            Referenced in: $relativePath" -ForegroundColor DarkGray
            }
        }
    }
}

# Summary
Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Total procedure references found: $($procedureReferences.Count)" -ForegroundColor White
Write-Host "Procedures with SQL files: $($foundProcedures.Count)" -ForegroundColor Green
Write-Host "Missing SQL files: $($missingProcedures.Count)" -ForegroundColor Red

if ($missingProcedures.Count -eq 0) {
    Write-Host "`n✅ All referenced stored procedures have SQL files!" -ForegroundColor Green
} else {
    Write-Host "`n⚠️  Missing SQL files detected!" -ForegroundColor Yellow
    Write-Host "`n=== Missing Procedures ===" -ForegroundColor Yellow
    foreach ($proc in $missingProcedures | Sort-Object { $_.Count } -Descending) {
        Write-Host "  - $($proc.Name) (referenced $($proc.Count) times)" -ForegroundColor DarkYellow
    }
}

# Generate report
$reportContent = @"
Missing Stored Procedure SQL Files Report
Generated: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")

=== Summary ===
Total unique procedure references in code: $($procedureReferences.Count)
Procedures with SQL files: $($foundProcedures.Count)
Missing SQL files: $($missingProcedures.Count)

=== Missing Procedures ===
"@

if ($missingProcedures.Count -eq 0) {
    $reportContent += "`nNone - All referenced procedures have SQL files!"
} else {
    foreach ($proc in $missingProcedures | Sort-Object { $_.Count } -Descending) {
        $reportContent += "`n`n$($proc.Name)"
        $reportContent += "`n  Referenced $($proc.Count) times in:"
        foreach ($file in ($proc.Files | Select-Object -Unique)) {
            $relativePath = $file.Replace($repoRoot, "").TrimStart('\')
            $reportContent += "`n    - $relativePath"
        }
    }
}

$reportContent += "`n`n=== Procedures with SQL Files ==="
foreach ($proc in $foundProcedures | Sort-Object Name) {
    $reportContent += "`n- $($proc.Name) (referenced $($proc.Count) times)"
}

$reportContent | Out-File -FilePath $reportPath -Encoding UTF8

Write-Host "`nReport saved to: $reportPath" -ForegroundColor Cyan

# Additional analysis: SQL files without code references
Write-Host "`n=== Additional Analysis ===" -ForegroundColor Cyan
$unreferencedSqlFiles = @()

foreach ($sqlFile in $sqlFiles) {
    if (-not $procedureReferences.ContainsKey($sqlFile)) {
        $unreferencedSqlFiles += $sqlFile
    }
}

if ($unreferencedSqlFiles.Count -gt 0) {
    Write-Host "Found $($unreferencedSqlFiles.Count) SQL files with no code references:" -ForegroundColor Yellow
    foreach ($sqlFile in $unreferencedSqlFiles | Sort-Object) {
        Write-Host "  - $sqlFile" -ForegroundColor DarkYellow
    }
    Write-Host "`nNote: These may be utility procedures or referenced indirectly" -ForegroundColor Gray
} else {
    Write-Host "All SQL files are referenced in the codebase ✅" -ForegroundColor Green
}

# Exit code
if ($missingProcedures.Count -gt 0) {
    exit 1
} else {
    exit 0
}
