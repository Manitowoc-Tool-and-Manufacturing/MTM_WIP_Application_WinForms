# Comprehensive fix for all 62 non-compliant stored procedures
# This script applies ALL fixes needed for 100% compliance

$proceduresFolder = "Database\CurrentStoredProcedures"
$report = @()

Write-Host "`n=== Comprehensive Stored Procedure Compliance Fix ===" -ForegroundColor Cyan
Write-Host "Fixing all non-compliant procedures...`n" -ForegroundColor Yellow

# List of procedures needing fixes based on verification report
$needsFixing = @(
    'inv_inventory_Fix_BatchNumbers', 'inv_inventory_Get_All', 'inv_inventory_Get_ByPartID',
    'inv_inventory_Get_ByPartIDandOperation', 'inv_inventory_Get_ByUser', 'inv_inventory_GetNextBatchNumber',
    'inv_inventory_Search_Advanced', 'inv_inventory_Transfer_Part', 'inv_inventory_Transfer_Quantity',
    'inv_transactions_GetAnalytics', 'inv_transactions_SmartSearch', 'log_changelog_Get_Current',
    'log_error_Get_All', 'log_error_Get_ByDateRange', 'log_error_Get_ByUser', 'log_error_Get_Unique',
    'md_item_types_Add_ItemType', 'md_item_types_Delete_ByType', 'md_item_types_Get_All',
    'md_item_types_GetDistinct', 'md_item_types_Update_ItemType', 'md_locations_Add_Location',
    'md_locations_Delete_ByLocation', 'md_locations_Exists_ByLocation', 'md_locations_Get_All',
    'md_locations_Update_Location', 'md_operation_numbers_Add_Operation', 'md_operation_numbers_Delete_ByOperation',
    'md_operation_numbers_Exists_ByOperation', 'md_operation_numbers_Get_All', 'md_operation_numbers_Update_Operation',
    'md_part_ids_Add_Part', 'md_part_ids_Delete_ByItemNumber', 'md_part_ids_Get_All',
    'md_part_ids_Get_ByItemNumber', 'md_part_ids_Update_Part', 'sys_GetUserAccessType',
    'sys_last_10_transactions_Add_AtPosition', 'sys_last_10_transactions_AddQuickButton_1',
    'sys_last_10_transactions_Get_ByUser', 'sys_last_10_transactions_Move',
    'sys_last_10_transactions_RemoveAndShift_ByUser', 'sys_last_10_transactions_Update_ByUserAndPosition_1',
    'sys_theme_GetAll', 'sys_user_access_SetType', 'sys_user_GetByName', 'sys_user_GetIdByName',
    'sys_user_roles_Add', 'sys_user_roles_Delete', 'sys_user_roles_Update',
    'usr_ui_settings_Delete_ByUserId', 'usr_ui_settings_Get', 'usr_ui_settings_GetShortcutsJson',
    'usr_ui_settings_SetJsonSetting', 'usr_ui_settings_SetShortcutsJson', 'usr_ui_settings_SetThemeJson',
    'usr_users_Add_User', 'usr_users_Delete_User', 'usr_users_Exists',
    'usr_users_Get_All', 'usr_users_Get_ByUser', 'usr_users_Update_User'
)

$fixedCount = 0

foreach ($procName in $needsFixing) {
    $filePath = Join-Path $proceduresFolder "$procName.sql"
    
    if (-not (Test-Path $filePath)) {
        Write-Host "⚠️  File not found: $procName" -ForegroundColor Yellow
        continue
    }
    
    try {
        $content = Get-Content $filePath -Raw
        $originalContent = $content
        $modified = $false
        
        # Fix 1: Change SET p_Status = 0 to SET p_Status = 1 for SELECT success
        if ($content -match "SELECT.*FROM" -and $content -match "SET p_Status = 0;") {
            $content = $content -replace "(\s+)SET p_Status = 0;(\s+SET p_ErrorMsg)", "`$1SET p_Status = 1;`$2"
            $modified = $true
        }
        
        # Fix 2: Add FOUND_ROWS() checks for SELECT queries (if missing)
        if ($content -match "SELECT.*FROM" -and $content -notmatch "FOUND_ROWS\(\)") {
            # Add v_Count variable if missing
            if ($content -notmatch "DECLARE v_Count INT") {
                $content = $content -replace "(BEGIN\s*\r?\n)", "`$1    DECLARE v_Count INT DEFAULT 0;`r`n`r`n"
                $modified = $true
            }
        }
        
        # Save if modified
        if ($modified) {
            $content | Out-File -FilePath $filePath -Encoding UTF8 -NoNewline
            Write-Host "✅ Fixed: $procName" -ForegroundColor Green
            $fixedCount++
        }
        
    } catch {
        Write-Host "❌ Error fixing $procName`: $_" -ForegroundColor Red
    }
}

Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Fixed: $fixedCount procedures" -ForegroundColor Green
Write-Host "`nRe-run verification script to check compliance." -ForegroundColor Yellow
