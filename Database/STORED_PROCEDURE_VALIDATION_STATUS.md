# Stored Procedure Validation Status

**Last Updated:** November 2, 2025  
**Validation Script:** `Database\Scripts\Validate-StoredProcedureParameters.ps1`

## Summary

Automated validation infrastructure created to verify that DAO method parameter names match MySQL stored procedure parameter definitions.

### Overall Statistics

- **Total Stored Procedures:** 77
- **Passed Validation:** 37 (48%)
- **Failed Validation:** 40 (52%)
  - **Parse Errors:** 22 (methods with special patterns)
  - **Parameter Mismatches:** 18 (actual name/order problems)

## Validation Script Features

The PowerShell validation script (`Validate-StoredProcedureParameters.ps1`):

1. ✅ Queries MySQL INFORMATION_SCHEMA for SP parameter definitions
2. ✅ Parses C# DAO files using character-by-character tokenization (no regex failures)
3. ✅ Compares parameter names (strips `p_` prefix for comparison)
4. ✅ Validates parameter order (critical for positional binding)
5. ✅ Generates timestamped CSV reports in `Database/ValidationRuns/`
6. ✅ Color-coded terminal output for easy issue identification

### How to Run

```powershell
# From repository root
.\Database\Scripts\Validate-StoredProcedureParameters.ps1

# Results exported to: Database\ValidationRuns\sp-validation-YYYYMMDD-HHMMSS.csv
```

## Recently Fixed Issues

### usr_users_Add_User ✅ FIXED (November 2, 2025)

**Problem 1 (C# DAO):** Parameter order mismatch - `WipServerPort` must come before `WipDatabase`
**Fixed In:** `Data/Dao_User.cs` line 864-882
**Status:** ✅ Parameters now sent in correct order

**Problem 2 (MySQL SP):** INSERT statement column name case mismatch
- Table column: `WIPDatabase` (capital IP)  
- SP INSERT was using: `WipDatabase` (lowercase ip)
**Fixed In:** `Database/UpdatedStoredProcedures/ReadyForVerification/users/usr_users_Add_User.sql` line 51
**Deployed:** November 2, 2025 19:08
**Status:** ✅ Column name now matches table definition

**Final Validation Result:** ✅ 14 parameters match in correct order, user creation working

## Common Issue Patterns

### 1. Parameter Prefix Issues

**Pattern:** DAO sends `p_ParameterName` but SP expects `ParameterName` (without `p_` prefix)

**Examples:**
- `inv_inventory_Add_Item`: Sends `p_Operation`, `p_User` instead of `Operation`, `User`
- `inv_inventory_Get_ByPartIDandOperation`: Sends `p_Operation` instead of `Operation`
- `md_operation_numbers_Add_Operation`: Sends `p_Operation` instead of `Operation`

**Fix Strategy:** Remove `p_` prefix from Dictionary keys in DAO methods (helper adds it automatically)

### 2. Parameter Order Mismatches

**Pattern:** DAO sends parameters in wrong order compared to SP definition

**Example:** `md_locations_Delete_ByLocation`
- SP expects: `Location`
- DAO sends: `Location, IssuedBy, Building` (extra parameters in wrong SP)

**Fix Strategy:** Reorder Dictionary entries to match SP ORDINAL_POSITION from MySQL

### 3. Missing/Extra Parameters

**Pattern:** DAO missing required parameters or includes parameters SP doesn't accept

**Examples:**
- `inv_transactions_GetAnalytics`: Missing `UserName, IsAdmin, FromDate, ToDate`
- `md_part_ids_Add_Part`: Missing `Description, IssuedBy, ItemType`
- `usr_settings_SetShortcutsJson`: Missing `UserId, ShortcutsJson`

**Fix Strategy:** Add missing parameters or remove extra ones based on SP definition

### 4. Parse Errors (22 occurrences)

**Pattern:** Methods using special patterns the parser can't handle yet

**Examples:**
- `inv_inventory_Remove_Item` - Likely uses ExecuteScalarWithStatus
- `inv_inventory_Transfer_Part` - Complex multi-line parameter construction
- All `sys_last_10_transactions_*` methods - Quick button operations

**Fix Strategy:** These need manual inspection - many may be correct but use patterns like:
- Inline parameter construction
- ExecuteScalar methods (not ExecuteDataTableWithStatus)
- Dynamic parameter building

## Validation Results by Category

### ✅ Fully Validated (37 procedures)

**Inventory:**
- `inv_inventory_Get_ByPartID`
- `inv_inventory_Fix_BatchNumbers` (no parameters)
- `inv_inventory_GetNextBatchNumber` (no parameters)

**Error Logging:**
- `log_error_Delete_ById`
- `log_error_Get_ByUser`
- `log_error_Delete_All` (no parameters)
- `log_error_Get_All` (no parameters)
- `log_error_Get_Unique` (no parameters)

**Master Data:**
- `md_part_ids_Get_ByItemNumber`
- `md_part_ids_Get_All` (no parameters)
- `md_item_types_Get_All` (no parameters)
- `md_item_types_GetDistinct` (no parameters)
- `md_locations_Get_All` (no parameters)
- `md_operation_numbers_Get_All` (no parameters)

**System Tables:**
- `sys_parameter_prefix_overrides_Add` ✅
- `sys_parameter_prefix_overrides_Delete_ById` ✅
- `sys_parameter_prefix_overrides_Get_ById` ✅
- `sys_parameter_prefix_overrides_Update_ById` ✅
- `sys_parameter_prefix_overrides_Get_All` (no parameters)
- `sys_theme_GetAll` (no parameters)
- `sys_GetUserAccessType` (no parameters)
- `sys_user_roles_Add` ✅
- `sys_user_roles_Delete` ✅
- `sys_user_roles_Get_ById` ✅
- `sys_user_roles_Update` ✅

**User Management:**
- `usr_users_Add_User` ✅ **RECENTLY FIXED**
- `usr_users_Delete_User` ✅
- `usr_users_Exists` ✅
- `usr_users_Get_ByUser` ✅
- `usr_users_Update_User` ✅
- `usr_users_SetUserSetting_ByUserAndField` ✅
- `usr_users_Get_All` (no parameters)

**UI Settings:**
- `usr_settings_Delete_ByUserId` ✅
- `usr_settings_Get` ✅
- `usr_settings_GetJsonSetting` ✅
- `usr_settings_GetShortcutsJson` ✅
- `usr_settings_Get_All` (no parameters)

### ⚠️ Parameter Mismatches (18 procedures)

**Inventory Operations:**
1. `inv_inventory_Add_Item` - Missing: `Operation, User` / Extra: `p_Operation, p_User, BatchNumber`
2. `inv_inventory_Get_ByPartIDandOperation` - Missing: `Operation` / Extra: `p_Operation` + ORDER MISMATCH
3. `inv_inventory_Get_ByUser` - Missing: `User` / Extra: `p_User`

**Transaction Analytics:**
4. `inv_transactions_GetAnalytics` - Missing: `UserName, IsAdmin, FromDate, ToDate`
5. `inv_transactions_Search` - Missing 15 parameters / Extra: `WhereClause`
6. `inv_transactions_SmartSearch` - Missing: `WhereClause, Page, PageSize` / Extra: `UserName, IsAdmin, FromDate, ToDate`

**Error Logging:**
7. `log_error_Get_ByDateRange` - Missing: `EndDate`

**Master Data - Locations:**
8. `md_locations_Add_Location` - Extra: `OldLocation`
9. `md_locations_Delete_ByLocation` - Extra: `IssuedBy, Building` + ORDER MISMATCH
10. `md_locations_Update_Location` - Missing: `OldLocation, IssuedBy, Building`

**Master Data - Operations:**
11. `md_operation_numbers_Add_Operation` - Missing: `Operation` / Extra: `p_Operation, NewOperation`
12. `md_operation_numbers_Delete_ByOperation` - Missing: `Operation` / Extra: `p_Operation, IssuedBy`
13. `md_operation_numbers_Update_Operation` - Missing: `Operation, NewOperation, IssuedBy` / Extra: `p_Operation`

**Master Data - Parts:**
14. `md_part_ids_Add_Part` - Missing: `Description, IssuedBy, ItemType` / Extra: `id, type`
15. `md_part_ids_Update_Part` - Missing 6 parameters / Extra: `partNumber, partType, user`

**UI Settings:**
16. `usr_settings_SetJsonSetting` - Missing: `DgvName, SettingJson`
17. `usr_settings_SetShortcutsJson` - Missing: `UserId, ShortcutsJson` / Extra: `user`
18. `usr_settings_SetThemeJson` - Missing: `ThemeJson`

### 🔍 Parse Errors - Need Manual Inspection (22 procedures)

**Inventory Operations:**
- `inv_inventory_Remove_Item`
- `inv_inventory_Transfer_Part`
- `inv_inventory_transfer_quantity`
- `inv_transaction_Add`

**Error Logging:**
- `log_error_Add_Error`

**Master Data - Item Types:**
- `md_item_types_Add_ItemType`
- `md_item_types_Delete_ByType`
- `md_item_types_Exists_ByType`
- `md_item_types_Update_ItemType`

**Master Data - Locations:**
- `md_locations_Exists_ByLocation`

**Master Data - Operations:**
- `md_operation_numbers_Exists_ByOperation`

**Master Data - Parts:**
- `md_part_ids_Delete_ByItemNumber`

**System Functions:**
- `sys_GetRoleIdByName`
- `sys_user_access_SetType`
- `sys_user_GetIdByName`

**Quick Buttons (All 7 procedures):**
- `sys_last_10_transactions_Add_AtPosition`
- `sys_last_10_transactions_AddQuickButton`
- `sys_last_10_transactions_Delete_ByUserAndPosition`
- `sys_last_10_transactions_DeleteAll_ByUser`
- `sys_last_10_transactions_Move`
- `sys_last_10_transactions_RemoveAndShift_ByUser`
- `sys_last_10_transactions_Update_ByUserAndPosition`

**Note:** Parse errors don't necessarily mean issues - many may be using ExecuteScalar or other patterns that work correctly but the validation script can't parse yet.

## Next Steps

### Priority 1: Fix Parameter Prefix Issues (High Impact, Low Risk)

Remove `p_` prefix from DAO Dictionary keys where helper adds it automatically:
- `inv_inventory_Add_Item`
- `inv_inventory_Get_ByPartIDandOperation`
- `inv_inventory_Get_ByUser`
- `md_operation_numbers_*` (3 procedures)

### Priority 2: Fix Parameter Order Mismatches (Critical for Correctness)

Reorder parameters to match SP definitions:
- `md_locations_Delete_ByLocation`

### Priority 3: Add Missing Parameters (Feature Gaps)

Investigate and add missing parameters:
- `inv_transactions_GetAnalytics`
- `inv_transactions_Search`
- `usr_settings_SetJsonSetting`
- `usr_settings_SetThemeJson`

### Priority 4: Manual Inspection of Parse Errors (22 procedures)

Review each manually to determine if:
- Parameters are correct but use patterns script can't parse
- Script needs enhancement to handle the pattern
- Actual parameter issues exist

## Validation History

### November 2, 2025
- ✅ Fixed `usr_users_Add_User` parameter order (WipServerPort before WipDatabase)
- ✅ Created automated validation script with tokenization-based parsing
- ✅ Initial validation run: 37 pass / 40 issues
- ✅ Generated baseline CSV report

## Technical Notes

### Parameter Prefix Convention

**MTM Convention:** DAO methods send parameter names **without** the `p_` prefix. The `Helper_Database_StoredProcedure` class automatically adds `p_` when building MySQL commands.

**Example:**
```csharp
// DAO sends:
new Dictionary<string, object> {
    ["UserName"] = userName,  // No p_ prefix
    ["PartID"] = partId       // No p_ prefix
}

// Helper converts to:
// MySQL parameter: p_UserName
// MySQL parameter: p_PartID
```

### Why Parameter Order Matters

MySQL stored procedures bind parameters **positionally** when using `CALL` statements. Even though parameters are named, the order must match the stored procedure definition's `ORDINAL_POSITION`.

**Consequence of Wrong Order:** Parameters get assigned to wrong variables inside the stored procedure, causing data corruption or unexpected behavior.

## References

- **Instruction File:** `.github/instructions/mysql-database.instructions.md`
- **DAO Pattern Guide:** `.github/instructions/csharp-dotnet8.instructions.md`
- **Validation Script:** `Database/Scripts/Validate-StoredProcedureParameters.ps1`
- **Latest Results:** `Database/ValidationRuns/sp-validation-20251102-190547.csv`
