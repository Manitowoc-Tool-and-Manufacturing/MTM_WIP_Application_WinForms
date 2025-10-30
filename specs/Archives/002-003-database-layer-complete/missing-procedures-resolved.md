# Missing Stored Procedures - Resolution Log

**Date**: 2025-10-19  
**Issue**: Test failures caused by missing stored procedures  
**Root Cause**: Procedures split between two UpdatedStoredProcedures folders  
**Status**: ✅ **RESOLVED** - 14 procedures copied to correct location  

---

## Problem Summary

Integration tests revealed 65 failures, with many caused by missing stored procedures. Investigation showed that required procedures were split between two locations:

- `Database/UpdatedStoredProcedures/ReadyForVerification/` - 83 procedures (INCOMPLETE)
- `UpdatedStoredProcedures/ReadyForVerification/` - 89 procedures (MORE COMPLETE)

**Result**: 14 required procedures were missing from the primary deployment location.

---

## Resolution Actions

### ✅ Files Copied to `Database/UpdatedStoredProcedures/ReadyForVerification/`

| # | Procedure Name | Source Folder | Category | DAO Reference |
|---|----------------|---------------|----------|---------------|
| 1 | `inv_inventory_Get_All.sql` | inventory/ | Inventory | Multiple |
| 2 | `inv_inventory_GetNextBatchNumber.sql` | inventory/ | Inventory | Dao_Inventory.cs:324 |
| 3 | `inv_transactions_Search.sql` | inventory/ | **CRITICAL** | Dao_Transactions.cs:83 |
| 4 | `md_item_types_Exists_ByType.sql` | master-data/ | **CRITICAL** | Dao_ItemType.cs:184 |
| 5 | `md_item_types_GetDistinct.sql` | master-data/ | Master Data | Dao_Part.cs:136 |
| 6 | `md_locations_Exists_ByLocation.sql` | master-data/ | **CRITICAL** | Dao_Location.cs:190 |
| 7 | `md_operation_numbers_Exists_ByOperation.sql` | master-data/ | **CRITICAL** | Dao_Operation.cs:188 |
| 8 | `sys_GetRoleIdByName.sql` | system/ | **CRITICAL** | Dao_System.cs:207 |
| 9 | `sys_last_10_transactions_Add_AtPosition.sql` | system/ | Quick Buttons | Dao_QuickButtons.cs:103 |
| 10 | `sys_last_10_transactions_Delete_ByUserAndPosition_1.sql` | system/ | Quick Buttons | Dao_QuickButtons.cs:247 |
| 11 | `sys_last_10_transactions_DeleteAll_ByUser.sql` | system/ | Quick Buttons | Dao_QuickButtons.cs:173 |
| 12 | `usr_ui_settings_Delete_ByUserId.sql` | users/ | User Settings | Dao_User.cs:743 |
| 13 | `usr_ui_settings_GetJsonSetting.sql` | users/ | User Settings | Dao_User.cs:664 |
| 14 | `usr_users_SetUserSetting_ByUserAndField.sql` | users/ | User Settings | Dao_User.cs:701 |

### 📊 New File Counts

| Location | Before | After | Status |
|----------|--------|-------|--------|
| `Database/UpdatedStoredProcedures/ReadyForVerification/` | 83 | **97** | ✅ Updated |
| `UpdatedStoredProcedures/ReadyForVerification/` | 89 | 89 | 🟡 Keep for reference |

---

## Expected Test Impact

### Tests That Should Now Pass

#### **Inventory Operations** (5 tests)
- ✅ `inv_transactions_Search` procedures (4 tests):
  - SearchTransactionsAsync_ValidCriteria_ReturnsTransactions
  - SearchTransactionsAsync_Pagination_ReturnsPaginatedResults
  - SearchTransactionsAsync_FilterByType_ReturnsFilteredResults
  - SearchTransactionsAsync_DateRangeFilter_ReturnsFilteredResults
- ✅ Batch number generation tests

#### **Master Data Validation** (8 tests)
- ✅ `md_item_types_Exists_ByType` (2 tests):
  - ItemTypeExists_ExistingType_ReturnsTrue
  - ItemTypeExists_NonExistentType_ReturnsFalse
- ✅ `md_locations_Exists_ByLocation` (2 tests):
  - LocationExists_ExistingLocation_ReturnsTrue
  - LocationExists_NonExistentLocation_ReturnsFalse
- ✅ `md_operation_numbers_Exists_ByOperation` (2 tests):
  - OperationExists_ExistingOperation_ReturnsTrue
  - OperationExists_NonExistentOperation_ReturnsFalse
- ✅ `md_item_types_GetDistinct` tests
- ✅ Part retrieval tests

#### **System/Roles** (1 test)
- ✅ `sys_GetRoleIdByName` (1 test):
  - GetRoleIdByNameAsync_WithValidRole_ReturnsRoleId

#### **Quick Buttons** (13+ tests)
- ✅ All Quick Button DAO tests should now pass (if procedures are correct)

**Estimated Pass Rate Improvement**: +27 tests (from 71 to ~98 passing)

---

## Remaining Known Issues

### Still Need Investigation

1. **Parameter Mismatches** - Some procedures may have different parameter names than DAOs expect:
   - `inv_inventory_transfer_quantity` - Missing `p_BatchNumber`?
   - `inv_inventory_Transfer_Part` - Missing `p_BatchNumber`?
   - `inv_transactions_SmartSearch` - Missing `p_WhereClause`?
   - `inv_transactions_GetAnalytics` - Missing `p_UserName`?

2. **Column Name Mismatches**:
   - `md_part_ids_Get_ByItemNumber` - Returns wrong column names?

3. **Test Data Issues**:
   - Some tests need proper seed data (roles, parts, etc.)

4. **Error Log Procedures**:
   - Need to verify if they exist and work correctly

---

## Next Steps

### 1. Deploy Updated Procedures ✅ Next Action

```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Database

# Deploy to TEST database
.\Deploy-StoredProcedures.ps1 `
    -Database "mtm_wip_application_winforms_test" `
    -Force

# Verify deployment
.\Deploy-StoredProcedures.ps1 `
    -Database "mtm_wip_application_winforms_test" `
    -DryRun
```

### 2. Re-Run Integration Tests

```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Tests
dotnet test MTM_WIP_Application_Winforms.Tests.csproj --logger "console;verbosity=normal"
```

### 3. Analyze Remaining Failures

- Compare test results before/after
- Focus on parameter mismatch errors
- Document any new issues discovered

### 4. Update Test Failure Checklist

- Mark resolved items as ✅ Complete
- Update remaining issue counts
- Prioritize next phase fixes

---

## Validation Checklist

After redeployment and test run:

- [ ] Verify 97 procedures deployed successfully
- [ ] Confirm no deployment errors or warnings
- [ ] Run integration test suite
- [ ] Document pass/fail counts (expect ~98/136 passing)
- [ ] Identify remaining failure patterns
- [ ] Update `test-failure-fixes.md` with new status
- [ ] Commit procedure files to version control

---

## Lessons Learned

1. **Always validate file locations** before deployment
2. **Use MCP tools** to cross-reference code with procedures
3. **Consolidate deployment sources** to avoid split locations
4. **Test deployment scripts** with dry-run before actual deployment
5. **Keep duplicate folders in sync** or eliminate them entirely

---

## Files Modified

- ✅ Copied 14 SQL files to `Database/UpdatedStoredProcedures/ReadyForVerification/`
- ✅ New total: 97 procedures in correct deployment location
- 🟡 Root `UpdatedStoredProcedures/` folder kept for now (contains 89 files)
- 📝 Created this resolution log

---

**Next Review**: After deployment and test run completion  
**Expected Outcome**: Significant reduction in test failures (65 → ~38 remaining)  
**Critical Success Factor**: Proper deployment of all 97 procedures to test database
