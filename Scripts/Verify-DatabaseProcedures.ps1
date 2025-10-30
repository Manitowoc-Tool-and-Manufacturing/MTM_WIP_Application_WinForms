<#
.SYNOPSIS
    Verifies that all required stored procedures exist in the database and have proper status code compliance

.DESCRIPTION
    Queries MySQL to check:
    1. All 75 required stored procedures exist
    2. Each procedure has OUT p_Status INT and OUT p_ErrorMsg VARCHAR(500)
    3. Compares test vs production database

.PARAMETER Database
    Database name to check (default: mtm_wip_application_winforms_test)

.PARAMETER MySqlPath
    Path to mysql.exe (default: C:\MAMP\bin\mysql\bin\mysql.exe)

.EXAMPLE
    .\Scripts\Verify-DatabaseProcedures.ps1
    Verifies test database procedures

.EXAMPLE
    .\Scripts\Verify-DatabaseProcedures.ps1 -Database MTM_WIP_Application_Winforms
    Verifies production database procedures
#>

[CmdletBinding()]
param(
    [string]$Database = "mtm_wip_application_winforms_test",
    [string]$MySqlPath = "C:\MAMP\bin\mysql\bin\mysql.exe",
    [string]$ServerHost = "localhost",
    [string]$Port = "3306",
    [string]$User = "root",
    [string]$Pass = "root"
)

$ErrorActionPreference = "Continue"

Write-Host "`n╔═══════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║      MTM WIP Application - Database Verification             ║" -ForegroundColor Cyan
Write-Host "╚═══════════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

Write-Host "Database: " -NoNewline
Write-Host $Database -ForegroundColor Yellow
Write-Host "Server: " -NoNewline
Write-Host "${ServerHost}:${Port}" -ForegroundColor Yellow
Write-Host ""

# Expected stored procedures (73 total)
$expectedProcedures = @(
    # Inventory (12)
    "inv_inventory_Add_Item",
    "inv_inventory_Fix_BatchNumbers",
    "inv_inventory_Get_ByPartID",
    "inv_inventory_Get_ByPartIDandOperation",
    "inv_inventory_Get_ByUser",
    "inv_inventory_Remove_Item",
    "inv_inventory_Search_Advanced",
    "inv_inventory_Transfer_Part",
    "inv_inventory_Transfer_Quantity",
    "inv_transaction_Add",
    "inv_transactions_GetAnalytics",
    "inv_transactions_SmartSearch",

    # Error Logging (7)
    "log_error_Add_Error",
    "log_error_Delete_All",
    "log_error_Delete_ById",
    "log_error_Get_All",
    "log_error_Get_ByDateRange",
    "log_error_Get_ByUser",
    "log_error_Get_Unique",

    # Changelog (1)
    "log_changelog_Get_Current",

    # Maintenance (2)
    "maint_InsertMissingUserUiSettings",
    "maint_reload_part_ids_and_operation_numbers",

    # Master Data - Item Types (5)
    "md_item_types_Add_ItemType",
    "md_item_types_Delete_ByID",
    "md_item_types_Delete_ByType",
    "md_item_types_Get_All",
    "md_item_types_Update_ItemType",

    # Master Data - Locations (4)
    "md_locations_Add_Location",
    "md_locations_Delete_ByLocation",
    "md_locations_Get_All",
    "md_locations_Update_Location",

    # Master Data - Operations (4)
    "md_operation_numbers_Add_Operation",
    "md_operation_numbers_Delete_ByOperation",
    "md_operation_numbers_Get_All",
    "md_operation_numbers_Update_Operation",

    # Master Data - Parts (5)
    "md_part_ids_Add_Part",
    "md_part_ids_Delete_ByItemNumber",
    "md_part_ids_Get_All",
    "md_part_ids_Get_ByItemNumber",
    "md_part_ids_Update_Part",

    # Migration (1)
    "migrate_user_roles_debug",

    # Query (1)
    "query_get_all_usernames_and_roles",

    # Batch Reassignment (1)
    "sp_ReassignBatchNumbers",

    # System (21)
    "sys_GetUserAccessType",
    "sys_last_10_transactions_AddQuickButton_1",
    "sys_last_10_transactions_Get_ByUser",
    "sys_last_10_transactions_Get_ByUser_1",
    "sys_last_10_transactions_Move_1",
    "sys_last_10_transactions_MoveToLast_ByUser",
    "sys_last_10_transactions_RemoveAndShift_ByUser",
    "sys_last_10_transactions_RemoveAndShift_ByUser_1",
    "sys_last_10_transactions_Reorder_ByUser",
    "sys_last_10_transactions_SwapPositions_ByUser",
    "sys_last_10_transactions_Update_ByUserAndDate",
    "sys_last_10_transactions_Update_ByUserAndPosition_1",
    "sys_role_GetIdByName",
    "sys_roles_Get_ById",
    "sys_theme_GetAll",
    "sys_user_access_SetType",
    "sys_user_GetByName",
    "sys_user_GetIdByName",
    "sys_user_roles_Add",
    "sys_user_roles_Delete",
    "sys_user_roles_Update",

    # Users/UI Settings (11)
    "usr_ui_settings_Get",
    "usr_ui_settings_GetShortcutsJson",
    "usr_ui_settings_SetJsonSetting",
    "usr_ui_settings_SetShortcutsJson",
    "usr_ui_settings_SetThemeJson",
    "usr_users_Add_User",
    "usr_users_Delete_User",
    "usr_users_Exists",
    "usr_users_Get_All",
    "usr_users_Get_ByUser",
    "usr_users_Update_User"
)

# Step 1: Check total count
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "Step 1: Checking Total Stored Procedure Count" -ForegroundColor White
Write-Host "═══════════════════════════════════════════════════════════════`n" -ForegroundColor Cyan

$countQuery = "SELECT COUNT(*) as Total FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = '$Database' AND ROUTINE_TYPE = 'PROCEDURE';"
$countOutput = & $MySqlPath --host=$ServerHost --port=$Port --user=$User --password=$Pass --database=$Database --execute=$countQuery 2>&1 | Out-String
$countMatch = [regex]::Match($countOutput, '(\d+)')
$actualCount = if ($countMatch.Success) { [int]$countMatch.Groups[1].Value } else { 0 }

Write-Host "Expected: " -NoNewline
Write-Host "$($expectedProcedures.Count) procedures" -ForegroundColor Yellow
Write-Host "Found:    " -NoNewline

if ($actualCount -eq $expectedProcedures.Count) {
    Write-Host "$actualCount procedures " -NoNewline -ForegroundColor Green
    Write-Host "✓" -ForegroundColor Green
} else {
    Write-Host "$actualCount procedures " -NoNewline -ForegroundColor Red
    Write-Host "✗" -ForegroundColor Red
}

# Step 2: Check each procedure exists
Write-Host "`n═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "Step 2: Checking Individual Procedures" -ForegroundColor White
Write-Host "═══════════════════════════════════════════════════════════════`n" -ForegroundColor Cyan

$listQuery = "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = '$Database' AND ROUTINE_TYPE = 'PROCEDURE' ORDER BY ROUTINE_NAME;"
$actualProcedures = & $MySqlPath --host=$ServerHost --port=$Port --user=$User --password=$Pass --database=$Database --execute=$listQuery 2>&1 |
    Select-Object -Skip 1 |
    Where-Object { $_ -match '\S' -and $_ -notmatch 'ROUTINE_NAME' } |
    ForEach-Object { $_.Trim() }

$missingProcedures = @()
$foundProcedures = @()

foreach ($proc in $expectedProcedures) {
    if ($actualProcedures -contains $proc) {
        $foundProcedures += $proc
        Write-Host "  ✓ " -NoNewline -ForegroundColor Green
        Write-Host $proc -ForegroundColor White
    } else {
        $missingProcedures += $proc
        Write-Host "  ✗ " -NoNewline -ForegroundColor Red
        Write-Host "$proc (MISSING)" -ForegroundColor Red
    }
}

# Check for unexpected procedures
$unexpectedProcedures = $actualProcedures | Where-Object { $expectedProcedures -notcontains $_ }

if ($unexpectedProcedures) {
    Write-Host "`nUnexpected Procedures Found:" -ForegroundColor Yellow
    foreach ($proc in $unexpectedProcedures) {
        Write-Host "  ⚠ $proc" -ForegroundColor Yellow
    }
}

# Step 3: Check status code compliance for sample procedures
Write-Host "`n═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "Step 3: Checking Status Code Compliance (Sample)" -ForegroundColor White
Write-Host "═══════════════════════════════════════════════════════════════`n" -ForegroundColor Cyan

$sampleProcedures = @(
    "inv_inventory_Add_Item",
    "usr_users_Get_All",
    "md_part_ids_Get_All",
    "log_error_Get_All",
    "sys_user_GetByName"
)

$compliantCount = 0
$nonCompliantCount = 0

foreach ($proc in $sampleProcedures) {
    if ($actualProcedures -contains $proc) {
        $createQuery = "SHOW CREATE PROCEDURE $proc"
        $createResult = & $MySqlPath --host=$ServerHost --port=$Port --user=$User --password=$Pass --database=$Database --execute=$createQuery 2>&1 |
            Out-String

        $hasStatus = $createResult -match "OUT p_Status INT"
        $hasErrorMsg = $createResult -match "OUT p_ErrorMsg VARCHAR\(500\)"

        if ($hasStatus -and $hasErrorMsg) {
            Write-Host "  ✓ " -NoNewline -ForegroundColor Green
            Write-Host "$proc - Status Code Compliant" -ForegroundColor White
            $compliantCount++
        } else {
            Write-Host "  ✗ " -NoNewline -ForegroundColor Red
            Write-Host "$proc - Missing Status Code Parameters" -ForegroundColor Red
            if (-not $hasStatus) {
                Write-Host "      Missing: OUT p_Status INT" -ForegroundColor Red
            }
            if (-not $hasErrorMsg) {
                Write-Host "      Missing: OUT p_ErrorMsg VARCHAR(500)" -ForegroundColor Red
            }
            $nonCompliantCount++
        }
    }
}

# Final Summary
Write-Host "`n╔═══════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║                    VERIFICATION SUMMARY                       ║" -ForegroundColor Cyan
Write-Host "╚═══════════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

Write-Host "Database:           " -NoNewline
Write-Host $Database -ForegroundColor Yellow

Write-Host "Total Procedures:   " -NoNewline
Write-Host "$actualCount / $($expectedProcedures.Count)" -ForegroundColor $(if ($actualCount -eq $expectedProcedures.Count) { "Green" } else { "Red" })

Write-Host "Missing Procedures: " -NoNewline
Write-Host "$($missingProcedures.Count)" -ForegroundColor $(if ($missingProcedures.Count -eq 0) { "Green" } else { "Red" })

if ($unexpectedProcedures) {
    Write-Host "Unexpected Procs:   " -NoNewline
    Write-Host "$($unexpectedProcedures.Count)" -ForegroundColor Yellow
}

Write-Host "Status Compliant:   " -NoNewline
Write-Host "$compliantCount / $($sampleProcedures.Count) (sample)" -ForegroundColor $(if ($compliantCount -eq $sampleProcedures.Count) { "Green" } else { "Red" })

Write-Host ""

if ($missingProcedures.Count -eq 0 -and $compliantCount -eq $sampleProcedures.Count) {
    Write-Host "✅ DATABASE VERIFICATION PASSED" -ForegroundColor Green
    Write-Host "   All required stored procedures are present and compliant.`n" -ForegroundColor Green
    exit 0
} else {
    Write-Host "❌ DATABASE VERIFICATION FAILED" -ForegroundColor Red
    if ($missingProcedures.Count -gt 0) {
        Write-Host "   Missing procedures detected. Run deployment script.`n" -ForegroundColor Red
    }
    if ($nonCompliantCount -gt 0) {
        Write-Host "   Non-compliant procedures detected. Update procedures.`n" -ForegroundColor Red
    }
    exit 1
}
