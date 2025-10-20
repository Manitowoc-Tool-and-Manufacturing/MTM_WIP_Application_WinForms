#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Extracts missing stored procedures from backup file and creates individual SQL files.
#>

param()

$ErrorActionPreference = "Stop"

# Paths
$repoRoot = Split-Path $PSScriptRoot -Parent
$backupFile = Join-Path $repoRoot "Database\CurrentStoredProcedures\NeedsVerifcation\stored_procedures_backup_20250813_200811.sql"
$outputFolder = Join-Path $repoRoot "Database\CurrentStoredProcedures"

# Procedures found in backup
$proceduresToExtract = @(
    'inv_inventory_Get_All',
    'inv_inventory_GetNextBatchNumber',
    'inv_inventory_Search_Advanced',
    'inv_transactions_GetAnalytics',
    'inv_transactions_SmartSearch',
    'md_item_types_GetDistinct',
    'md_locations_Exists_ByLocation',
    'md_operation_numbers_Exists_ByOperation',
    'md_part_ids_GetItemType_ByPartID',
    'sys_GetRoleIdByName',
    'sys_last_10_transactions_Add_AtPosition',
    'sys_last_10_transactions_DeleteAll_ByUser',
    'sys_last_10_transactions_Move',
    'usr_ui_settings_Delete_ByUserId',
    'usr_ui_settings_GetJsonSetting',
    'usr_user_roles_GetRoleId_ByUserId',
    'usr_users_SetUserSetting_ByUserAndField'
)

Write-Host "`n=== Extracting Procedures from Backup ===" -ForegroundColor Cyan
Write-Host "Source: $backupFile" -ForegroundColor Gray
Write-Host "Output: $outputFolder`n" -ForegroundColor Gray

$backupContent = Get-Content $backupFile -Raw

$extractedCount = 0
$failedCount = 0

foreach ($procName in $proceduresToExtract) {
    # Pattern to match procedure from DELIMITER ;; to END ;;
    $pattern = "(?s)DELIMITER ;;[\r\n]+CREATE DEFINER=\``root\``@\``localhost\`` PROCEDURE \``$procName\``.*?END ;;[\r\n]+DELIMITER ;"
    
    if ($backupContent -match $pattern) {
        $procedureBlock = $Matches[0]
        
        # Convert ;; to $$ for consistency with other procedures
        $procedureBlock = $procedureBlock -replace "DELIMITER ;;", "DELIMITER `$`$"
        $procedureBlock = $procedureBlock -replace "END ;;", "END`$`$"
        
        # Add DROP IF EXISTS at the beginning
        $finalSql = "DROP PROCEDURE IF EXISTS ``$procName``;`n" + $procedureBlock
        
        # Write to individual file
        $outputPath = Join-Path $outputFolder "$procName.sql"
        [System.IO.File]::WriteAllText($outputPath, $finalSql, [System.Text.Encoding]::UTF8)
        
        Write-Host "[EXTRACTED] $procName" -ForegroundColor Green
        $extractedCount++
    } else {
        Write-Host "[FAILED] $procName - Could not extract" -ForegroundColor Red
        $failedCount++
    }
}

Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Successfully extracted: $extractedCount" -ForegroundColor Green
Write-Host "Failed to extract: $failedCount" -ForegroundColor $(if ($failedCount -gt 0) { "Red" } else { "Green" })

Write-Host "`n=== Next Steps ===" -ForegroundColor Cyan
Write-Host "1. Review extracted procedures in: $outputFolder" -ForegroundColor Gray
Write-Host "2. Create the 5 missing procedures manually:" -ForegroundColor Gray
Write-Host "   - inv_transaction_Add" -ForegroundColor Yellow
Write-Host "   - inv_transaction_log" -ForegroundColor Yellow
Write-Host "   - inv_transactions_Search" -ForegroundColor Yellow
Write-Host "   - md_item_types_Exists_ByType" -ForegroundColor Yellow
Write-Host "   - sys_last_10_transactions_Delete_ByUserAndPosition_1" -ForegroundColor Yellow
Write-Host "3. Deploy all procedures using Deploy-StoredProcedures.ps1" -ForegroundColor Gray

Write-Host "`nExtraction complete!" -ForegroundColor Cyan
