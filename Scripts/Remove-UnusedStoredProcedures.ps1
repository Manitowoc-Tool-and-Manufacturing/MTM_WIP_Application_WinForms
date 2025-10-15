<#
.SYNOPSIS
    Identifies and removes unused stored procedure files from CurrentStoredProcedures folder.

.DESCRIPTION
    This script:
    1. Gets all .sql files from Database/CurrentStoredProcedures
    2. Searches the codebase for references to each procedure
    3. Identifies procedures that are NOT referenced
    4. Deletes the unused procedure files
    5. Generates a report of removed procedures

.NOTES
    Created: 2025-10-14
    Part of database cleanup initiative
#>

[CmdletBinding()]
param(
    [switch]$WhatIf
)

$ErrorActionPreference = "Stop"

# Paths
$repoRoot = Split-Path $PSScriptRoot -Parent
$proceduresFolder = Join-Path $repoRoot "Database\CurrentStoredProcedures"
$reportPath = Join-Path $repoRoot "Database\CurrentStoredProcedures\UNUSED_PROCEDURES_REMOVED.txt"

Write-Host "`n=== Stored Procedure Usage Analysis ===" -ForegroundColor Cyan
Write-Host "Analyzing procedures in: $proceduresFolder`n" -ForegroundColor Gray

# Get all SQL files (excluding standards doc)
$sqlFiles = Get-ChildItem -Path $proceduresFolder -Filter "*.sql" | 
    Where-Object { $_.Name -ne "CurrentStoredProcedures.sql" } |
    Sort-Object Name

Write-Host "Found $($sqlFiles.Count) stored procedure files to analyze`n" -ForegroundColor Yellow

# Extract procedure names from filenames (they match the procedure names)
$allProcedures = $sqlFiles | ForEach-Object { 
    [PSCustomObject]@{
        FileName = $_.Name
        ProcedureName = $_.BaseName  # Filename without .sql extension
        FilePath = $_.FullName
        IsUsed = $false
    }
}

Write-Host "Searching codebase for procedure references..." -ForegroundColor Cyan

# Search for each procedure in the codebase (.cs files)
$csFiles = Get-ChildItem -Path $repoRoot -Filter "*.cs" -Recurse | 
    Where-Object { $_.FullName -notmatch "\\obj\\|\\bin\\" }

$totalFiles = $csFiles.Count
Write-Host "Scanning $totalFiles C# files...`n" -ForegroundColor Gray

foreach ($proc in $allProcedures) {
    $procName = $proc.ProcedureName
    
    # Search for the procedure name in all C# files
    $found = $false
    foreach ($csFile in $csFiles) {
        if ((Select-String -Path $csFile.FullName -Pattern $procName -SimpleMatch -Quiet)) {
            $found = $true
            break
        }
    }
    
    $proc.IsUsed = $found
    
    if ($found) {
        Write-Host "  [USED]   $procName" -ForegroundColor Green
    } else {
        Write-Host "  [UNUSED] $procName" -ForegroundColor Red
    }
}

# Separate used and unused procedures
$usedProcedures = $allProcedures | Where-Object { $_.IsUsed -eq $true }
$unusedProcedures = $allProcedures | Where-Object { $_.IsUsed -eq $false }

# Display summary
Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Total procedures analyzed: $($allProcedures.Count)" -ForegroundColor White
Write-Host "Used procedures: $($usedProcedures.Count)" -ForegroundColor Green
Write-Host "Unused procedures: $($unusedProcedures.Count)" -ForegroundColor Red

if ($unusedProcedures.Count -eq 0) {
    Write-Host "`nAll procedures are in use! No cleanup needed." -ForegroundColor Green
    exit 0
}

# Display list of unused procedures
Write-Host "`n=== Unused Procedures ===" -ForegroundColor Yellow
$unusedProcedures | ForEach-Object {
    Write-Host "  - $($_.ProcedureName)" -ForegroundColor DarkYellow
}

# Delete unused procedures
if ($WhatIf) {
    Write-Host "`n[WHATIF] Would delete $($unusedProcedures.Count) unused procedure files" -ForegroundColor Magenta
} else {
    Write-Host "`n=== Deleting Unused Procedures ===" -ForegroundColor Red
    
    $deletedFiles = @()
    foreach ($proc in $unusedProcedures) {
        try {
            Remove-Item -Path $proc.FilePath -Force
            Write-Host "  Deleted: $($proc.FileName)" -ForegroundColor DarkRed
            $deletedFiles += $proc.ProcedureName
        } catch {
            Write-Host "  ERROR deleting $($proc.FileName): $_" -ForegroundColor Red
        }
    }
    
    # Generate report
    $reportContent = @"
Unused Stored Procedures Removed
Generated: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")

Total procedures analyzed: $($allProcedures.Count)
Used procedures (kept): $($usedProcedures.Count)
Unused procedures (removed): $($deletedFiles.Count)

=== Removed Procedures ===
$($deletedFiles | ForEach-Object { "- $_" } | Out-String)

=== Kept Procedures (Still in Use) ===
$($usedProcedures | ForEach-Object { "- $($_.ProcedureName)" } | Out-String)
"@
    
    $reportContent | Out-File -FilePath $reportPath -Encoding UTF8
    Write-Host "`nReport saved to: $reportPath" -ForegroundColor Cyan
    Write-Host "Deleted $($deletedFiles.Count) unused procedure files" -ForegroundColor Green
}

Write-Host "`n=== Cleanup Complete ===" -ForegroundColor Cyan
