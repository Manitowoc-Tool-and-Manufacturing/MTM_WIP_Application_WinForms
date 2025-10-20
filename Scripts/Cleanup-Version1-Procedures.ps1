# =============================================
# Cleanup-Version1-Procedures.ps1
# Purpose: Remove _1 version markers from stored procedures
# =============================================

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Cleanup _1 Version Markers" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$proceduresPath = Join-Path $PSScriptRoot "..\Database\UpdatedStoredProcedures\ReadyForVerification\system"

# Define the procedures to clean up
$cleanupActions = @(
    @{
        OldFile = "sys_last_10_transactions_RemoveAndShift_ByUser_1.sql"
        NewFile = "sys_last_10_transactions_RemoveAndShift_ByUser.sql"
        OldProc = "sys_last_10_transactions_RemoveAndShift_ByUser_1"
        NewProc = "sys_last_10_transactions_RemoveAndShift_ByUser"
        Action = "REPLACE" # Replace the bad non-_1 version with the good _1 version
    },
    @{
        OldFile = "sys_last_10_transactions_Get_ByUser_1.sql"
        NewFile = "sys_last_10_transactions_Get_ByUser.sql"
        OldProc = "sys_last_10_transactions_Get_ByUser_1"
        NewProc = "sys_last_10_transactions_Get_ByUser"
        Action = "REPLACE" # Replace if _1 is better
    },
    @{
        OldFile = "sys_last_10_transactions_AddQuickButton_1.sql"
        NewFile = "sys_last_10_transactions_AddQuickButton.sql"
        OldProc = "sys_last_10_transactions_AddQuickButton_1"
        NewProc = "sys_last_10_transactions_AddQuickButton"
        Action = "RENAME" # Only _1 exists, just rename
    },
    @{
        OldFile = "sys_last_10_transactions_Delete_ByUserAndPosition_1.sql"
        NewFile = "sys_last_10_transactions_Delete_ByUserAndPosition.sql"
        OldProc = "sys_last_10_transactions_Delete_ByUserAndPosition_1"
        NewProc = "sys_last_10_transactions_Delete_ByUserAndPosition"
        Action = "RENAME"
    },
    @{
        OldFile = "sys_last_10_transactions_Move_1.sql"
        NewFile = "sys_last_10_transactions_Move.sql"
        OldProc = "sys_last_10_transactions_Move_1"
        NewProc = "sys_last_10_transactions_Move"
        Action = "RENAME"
    },
    @{
        OldFile = "sys_last_10_transactions_Update_ByUserAndPosition_1.sql"
        NewFile = "sys_last_10_transactions_Update_ByUserAndPosition.sql"
        OldProc = "sys_last_10_transactions_Update_ByUserAndPosition_1"
        NewProc = "sys_last_10_transactions_Update_ByUserAndPosition"
        Action = "RENAME"
    }
)

foreach ($action in $cleanupActions) {
    $oldPath = Join-Path $proceduresPath $action.OldFile
    $newPath = Join-Path $proceduresPath $action.NewFile
    
    Write-Host "Processing: $($action.OldFile)" -ForegroundColor Yellow
    Write-Host "  Action: $($action.Action)" -ForegroundColor Cyan
    
    if ($action.Action -eq "REPLACE") {
        # Delete old non-_1 version if it exists
        if (Test-Path $newPath) {
            Write-Host "  Deleting old version: $($action.NewFile)" -ForegroundColor Red
            Remove-Item $newPath -Force
        }
    }
    
    # Read the _1 file
    if (Test-Path $oldPath) {
        $content = Get-Content $oldPath -Raw
        
        # Replace procedure name in content
        $content = $content -replace "DROP PROCEDURE IF EXISTS ``$($action.OldProc)``", "DROP PROCEDURE IF EXISTS ``$($action.NewProc)``"
        $content = $content -replace "CREATE DEFINER=``root``@``localhost`` PROCEDURE ``$($action.OldProc)``", "CREATE DEFINER=``root``@``localhost`` PROCEDURE ``$($action.NewProc)``"
        
        # Write to new file (without _1)
        Set-Content -Path $newPath -Value $content -Encoding UTF8
        Write-Host "  Created: $($action.NewFile)" -ForegroundColor Green
        
        # Delete old _1 file
        Remove-Item $oldPath -Force
        Write-Host "  Deleted: $($action.OldFile)" -ForegroundColor Green
    } else {
        Write-Host "  WARNING: $($action.OldFile) not found!" -ForegroundColor Red
    }
    
    Write-Host ""
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Cleanup Complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Run Deploy-Simple.ps1 to deploy cleaned procedures" -ForegroundColor White
Write-Host "2. Drop old _1 procedures from databases" -ForegroundColor White
