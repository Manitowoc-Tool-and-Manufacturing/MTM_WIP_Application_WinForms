# Final comprehensive fix for all remaining 34 non-compliant procedures
# This script systematically adds missing status checks, FOUND_ROWS, and ROW_COUNT

$nonCompliant = @(
    'inv_inventory_Fix_BatchNumbers',
    'inv_inventory_Get_All',
    'inv_inventory_Get_ByPartID',
    'inv_inventory_Get_ByPartIDandOperation',
    'inv_inventory_GetNextBatchNumber',
    'inv_inventory_Search_Advanced',
    'inv_inventory_Transfer_Part',
    'inv_transactions_GetAnalytics',
    'inv_transactions_SmartSearch',
    'log_changelog_Get_Current',
    'log_error_Get_All',
    'log_error_Get_ByDateRange',
    'log_error_Get_ByUser',
    'log_error_Get_Unique',
    'md_item_types_GetDistinct',
    'md_locations_Exists_ByLocation',
    'md_operation_numbers_Exists_ByOperation',
    'sys_GetUserAccessType',
    'sys_last_10_transactions_Add_AtPosition',
    'sys_last_10_transactions_Get_ByUser',
    'sys_last_10_transactions_RemoveAndShift_ByUser',
    'sys_theme_GetAll',
    'sys_user_access_SetType',
    'sys_user_GetByName',
    'sys_user_GetIdByName',
    'sys_user_roles_Update',
    'usr_ui_settings_Delete_ByUserId',
    'usr_ui_settings_Get',
    'usr_ui_settings_GetShortcutsJson',
    'usr_ui_settings_SetShortcutsJson',
    'usr_ui_settings_SetThemeJson',
    'usr_users_Add_User',
    'usr_users_Delete_User',
    'usr_users_Exists'
)

$fixed = 0
$skipped = 0

Write-Host "`n=== Final Comprehensive Fix ===" -ForegroundColor Cyan
Write-Host "Fixing $($nonCompliant.Count) remaining procedures`n" -ForegroundColor Yellow

foreach ($procName in $nonCompliant) {
    $file = "Database\CurrentStoredProcedures\$procName.sql"
    
    if (-not (Test-Path $file)) {
        Write-Host "⚠ Skip: $procName (file not found)" -ForegroundColor Gray
        $skipped++
        continue
    }
    
    $content = Get-Content $file -Raw
    $original = $content
    $changes = @()
    
    # Fix 1: Add success status before END$$ if missing SET p_Status = 1
    if ($content -notmatch "SET p_Status = 1" -and $content -match "END\$\$") {
        # Only add if there's no status setting at all
        if ($content -notmatch "SET p_Status = " -or $content -match "SET p_Status = -1") {
            $content = $content -replace "(\s+)END\$\$", "`n    SET p_Status = 1;`n    SET p_ErrorMsg = NULL;`$1END`$`$"
            $changes += "Added success status"
        }
    }
    
    # Fix 2: Add FOUND_ROWS check for SELECT queries (only if doesn't already have it)
    if ($content -match "SELECT .+ FROM" -and $content -notmatch "FOUND_ROWS\(\)" -and $content -notmatch "SELECT .+ INTO @") {
        # Add v_Count variable if missing
        if ($content -notmatch "DECLARE v_Count INT") {
            if ($content -match "(BEGIN\s*\r?\n)") {
                $content = $content -replace "(BEGIN\s*\r?\n)", "`$1    DECLARE v_Count INT DEFAULT 0;`n`n"
                $changes += "Added v_Count variable"
            }
        }
        
        # Add FOUND_ROWS check before the final status setting or END
        if ($content -match "(SELECT[^;]+;)(\s+)(SET p_Status = |END\$\$)") {
            $check = "`n`n    SELECT FOUND_ROWS() INTO v_Count;`n    IF v_Count > 0 THEN`n        SET p_Status = 1;`n        SET p_ErrorMsg = NULL;`n    ELSE`n        SET p_Status = 0;`n        SET p_ErrorMsg = 'No records found';`n    END IF;`n"
            $content = $content -replace "(SELECT[^;]+;)(\s+)(SET p_Status = 1|END\$\$)", "`$1$check`$2`$3"
            # Remove duplicate success status if we just added it
            $content = $content -replace "END IF;\s+SET p_Status = 1;\s+SET p_ErrorMsg = NULL;", "END IF;"
            $changes += "Added FOUND_ROWS check"
        }
    }
    
    # Fix 3: Add ROW_COUNT check for INSERT/UPDATE/DELETE (only if doesn't already have it)
    if ($content -match "\b(INSERT|UPDATE|DELETE)\b" -and $content -notmatch "ROW_COUNT\(\)") {
        # Add v_Count variable if missing
        if ($content -notmatch "DECLARE v_Count INT") {
            if ($content -match "(BEGIN\s*\r?\n)") {
                $content = $content -replace "(BEGIN\s*\r?\n)", "`$1    DECLARE v_Count INT DEFAULT 0;`n`n"
                $changes += "Added v_Count variable"
            }
        }
        
        # Add ROW_COUNT check after the last INSERT/UPDATE/DELETE
        if ($content -match "((INSERT|UPDATE|DELETE)[^;]+;)(\s+)(SET p_Status|END\$\$)") {
            $check = "`n`n    SET v_Count = ROW_COUNT();`n    IF v_Count > 0 THEN`n        SET p_Status = 1;`n        SET p_ErrorMsg = CONCAT('Successfully affected ', v_Count, ' row(s)');`n    ELSE`n        SET p_Status = 0;`n        SET p_ErrorMsg = 'No rows were affected';`n    END IF;`n"
            $content = $content -replace "((INSERT|UPDATE|DELETE)[^;]+;)(\s+)(SET p_Status = 1|END\$\$)", "`$1$check`$3`$4"
            # Remove duplicate success status
            $content = $content -replace "END IF;\s+SET p_Status = 1;\s+SET p_ErrorMsg = NULL;", "END IF;"
            $changes += "Added ROW_COUNT check"
        }
    }
    
    # Fix 4: Fix EXIT HANDLER that doesn't set p_Status
    if ($content -match "DECLARE EXIT HANDLER FOR SQLEXCEPTION" -and $content -match "BEGIN\s*END;" -and $content -notmatch "SET p_Status = -1") {
        $content = $content -replace "(DECLARE EXIT HANDLER FOR SQLEXCEPTION\s*BEGIN)\s*END;", "`$1`n        SET p_Status = -1;`n        SET p_ErrorMsg = 'Database error occurred while executing $procName';`n    END;"
        $changes += "Fixed EXIT HANDLER"
    }
    
    if ($changes.Count -gt 0 -and $content -ne $original) {
        Set-Content $file -Value $content -NoNewline
        Write-Host "✓ $procName - $($changes -join ', ')" -ForegroundColor Green
        $fixed++
    } else {
        Write-Host "- $procName (no changes)" -ForegroundColor Gray
    }
}

Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Fixed: $fixed" -ForegroundColor Green
Write-Host "Skipped: $skipped" -ForegroundColor Gray
Write-Host "`nRun Verify-StatusCodeStandards.ps1 to check final results!" -ForegroundColor Yellow
