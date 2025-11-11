# ============================================================================
# Archive Unused Stored Procedure Files
# ============================================================================
# Purpose: Move SP files not called by code to Archive folder
# Date: 2025-11-04
# ============================================================================

param(
    [switch]$WhatIf = $false
)

$ErrorActionPreference = 'Stop'
$repoRoot = "C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms"
$spFolder = Join-Path $repoRoot "Database\UpdatedStoredProcedures\ReadyForVerification"
$archiveFolder = Join-Path $repoRoot "Database\UpdatedStoredProcedures\Archive\unused-procedures-2025-11-04"

# Load analysis results
$analysisFile = Join-Path $repoRoot "Database\sp_sync_analysis.json"
if (-not (Test-Path $analysisFile)) {
    Write-Host "❌ Analysis file not found: $analysisFile" -ForegroundColor Red
    Write-Host "   Run sync_stored_procedures.ps1 first" -ForegroundColor Yellow
    exit 1
}

$analysis = Get-Content $analysisFile | ConvertFrom-Json

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  ARCHIVE UNUSED STORED PROCEDURE FILES" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

# Procedures to KEEP even if not called by code (manual exclusions)
$keepProcedures = @(
    # Maintenance procedures (run manually or scheduled)
    'maint_transactions_FindDuplicates',
    'maint_transactions_RemoveDuplicates',
    'maint_InsertMissingUserUiSettings',
    'maint_reload_part_ids_and_operation_numbers',
    
    # Migration/troubleshooting
    'migrate_user_roles_debug',
    
    # Query utilities (may be called dynamically or from admin tools)
    'query_check_procedure_exists',
    'query_get_all_stored_procedures',
    'query_get_all_usernames_and_roles',
    'query_get_procedure_parameters',
    
    # Utilities
    'sp_ReassignBatchNumbers',
    
    # Logging procedures (may be called dynamically)
    'log_changelog_Get_Current',
    'log_error_Delete_All',
    'log_error_Delete_ById',
    'log_error_Get_All',
    'log_error_Get_ByDateRange',
    'log_error_Get_ByUser'
)

$filesToArchive = $analysis.Analysis.FileNotCalled | Where-Object { 
    $_ -notin $keepProcedures 
}

Write-Host "Files not called by code: $($analysis.Analysis.FileNotCalled.Count)" -ForegroundColor Yellow
Write-Host "Manual exclusions (kept): $($keepProcedures.Count)" -ForegroundColor Green
Write-Host "Files to archive: $($filesToArchive.Count)" -ForegroundColor Magenta
Write-Host ""

if ($filesToArchive.Count -eq 0) {
    Write-Host "✅ No files need to be archived" -ForegroundColor Green
    exit 0
}

Write-Host "Procedures to archive:" -ForegroundColor Magenta
$filesToArchive | ForEach-Object { Write-Host "  - $_" -ForegroundColor Magenta }
Write-Host ""

Write-Host "Procedures kept (manual exclusions):" -ForegroundColor Green
$keepProcedures | Where-Object { $_ -in $analysis.Analysis.FileNotCalled } | ForEach-Object { 
    Write-Host "  - $_" -ForegroundColor Green 
}
Write-Host ""

if ($WhatIf) {
    Write-Host "⚠️  Running in WhatIf mode - no files will be moved" -ForegroundColor Yellow
    Write-Host ""
    exit 0
}

# Create archive folder
if (-not (Test-Path $archiveFolder)) {
    New-Item -Path $archiveFolder -ItemType Directory | Out-Null
    Write-Host "Created archive folder: $archiveFolder" -ForegroundColor Gray
    Write-Host ""
}

# Move files
$moved = 0
$errors = 0

foreach ($procName in $filesToArchive) {
    try {
        # Find the file
        $file = Get-ChildItem $spFolder -Recurse -Filter "$procName.sql" -File | Select-Object -First 1
        
        if ($file) {
            $destPath = Join-Path $archiveFolder $file.Name
            Move-Item -Path $file.FullName -Destination $destPath -Force
            Write-Host "✓ Archived: $procName" -ForegroundColor Green
            $moved++
        }
        else {
            Write-Host "⚠ Not found: $procName" -ForegroundColor Yellow
        }
    }
    catch {
        Write-Host "✗ Error archiving $procName`: $_" -ForegroundColor Red
        $errors++
    }
}

Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  SUMMARY" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""
Write-Host "Files archived: " -NoNewline; Write-Host $moved -ForegroundColor Green
Write-Host "Errors:         " -NoNewline; Write-Host $errors -ForegroundColor Red
Write-Host ""
Write-Host "Archive location: $archiveFolder" -ForegroundColor Gray
Write-Host ""

if ($moved -gt 0) {
    Write-Host "✅ Successfully archived $moved unused stored procedure files" -ForegroundColor Green
}
