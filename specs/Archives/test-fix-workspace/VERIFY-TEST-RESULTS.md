# Quick Verification Guide - Test Results

**Run this immediately to see test results:**

```powershell
# Simple command to see pass/fail summary
dotnet test MTM_WIP_Application_Winforms.csproj `
  --filter "FullyQualifiedName~Integration.Dao_QuickButtons_Tests" `
  --logger "console;verbosity=minimal"
```

**Expected Output**:
```
Passed!  - Failed:     0, Passed:    10, Skipped:     0, Total:    10, Duration: < 1 s
```

**If you see different results**, run this for details:
```powershell
dotnet test MTM_WIP_Application_Winforms.csproj `
  --filter "FullyQualifiedName~Integration.Dao_QuickButtons_Tests" `
  --logger "console;verbosity=detailed" > detailed-results.txt

Get-Content detailed-results.txt
```

---

## 🎯 What to Look For

### ✅ Success Scenario (All 10 Pass)
**Output**: `Passed! - Failed: 0, Passed: 10`

**Next Steps**:
1. Run tests 2-3 more times to verify idempotency
2. Update Category 1 checkboxes in `categories/01-quick-buttons.md`
3. Update DASHBOARD.md progress to 10/10
4. Move to Category 2 (System DAO)

---

### ⚠️ Partial Success (Some Fail)
**Output**: `Failed! - Failed: X, Passed: Y`

**Next Steps**:
1. Capture error messages for failed tests
2. Group failures by pattern (missing SP, parameters, etc.)
3. Fix one group at a time
4. Re-run until all pass

**Common Failure Patterns**:

**Missing Stored Procedure**:
```
System.InvalidOperationException: Procedure 'sys_last_10_transactions_Update_ByUserAndPosition' doesn't exist
```
**Fix**: Deploy procedure OR mark test Inconclusive

**Parameter Mismatch**:
```
MySql.Data.MySqlClient.MySqlException: Parameter 'p_User' not found
```
**Fix**: Update DAO parameter names to match SP

**No Test Data**:
```
Assert.IsTrue failed. Expected successful update of quick button
Status: -1, Message: No record found for user
```
**Fix**: Verify CreateTestUsersAsync() ran successfully

---

### ❌ All Fail (Systemic Issue)
**Output**: `Failed! - Failed: 10, Passed: 0`

**Likely Causes**:
1. Database connection issue
2. sys_last_10_transactions table doesn't exist
3. Test users not created

**Debug Steps**:
```powershell
# Check database connection
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT 1"

# Check if table exists
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test `
  -e "SHOW TABLES LIKE 'sys_last_10_transactions'"

# Check if test users exist
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test `
  -e "SELECT * FROM usr_users WHERE UserID LIKE 'TEST-%'"
```

---

## 📊 Test Status Tracking

Once you have results, update this table in `categories/01-quick-buttons.md`:

| # | Test Method | Status | Notes |
|---|-------------|--------|-------|
| 1 | UpdateQuickButtonAsync_ValidData_UpdatesButton | ✅/❌ | |
| 2 | AddQuickButtonAsync_ValidData_AddsButton | ✅/❌ | |
| 3 | RemoveQuickButtonAndShiftAsync_ValidPosition_RemovesAndShifts | ✅/❌ | |
| 4 | DeleteAllQuickButtonsForUserAsync_ValidUser_DeletesAllButtons | ✅/❌ | |
| 5 | MoveQuickButtonAsync_ValidPositions_MovesButton | ✅/❌ | |
| 6 | AddOrShiftQuickButtonAsync_ValidData_AddsOrShifts | ✅/❌ | |
| 7 | RemoveAndShiftQuickButtonAsync_ValidPosition_RemovesAndShifts | ✅/❌ | |
| 8 | AddQuickButtonAtPositionAsync_ValidData_AddsAtPosition | ✅/❌ | |
| 9 | UpdateQuickButtonAsync_Position1_UpdatesButton | ✅/❌ | |
| 10 | UpdateQuickButtonAsync_Position10_UpdatesButton | ✅/❌ | |
| 11 | MoveQuickButtonAsync_SamePosition_HandlesGracefully | ✅/❌ | |
| 12 | QuickButtonWorkflow_CompleteSequence_ExecutesSuccessfully | ✅/❌ | |

---

## 🎉 If All Tests Pass

**Congratulations!** Your infrastructure worked perfectly on first try.

**Complete these steps**:

1. **Verify idempotency** (run 2-3 more times):
   ```powershell
   dotnet test MTM_WIP_Application_Winforms.csproj `
     --filter "FullyQualifiedName~Integration.Dao_QuickButtons_Tests"
   ```
   All runs should show: `Passed! - Failed: 0, Passed: 10`

2. **Update category file** - Check all boxes in `categories/01-quick-buttons.md`

3. **Update DASHBOARD.md**:
   - Change progress to: `10/10 (100%)`
   - Update progress bar: `[██████████] 10/10 passing`
   - Change status to: "COMPLETE ✅"

4. **Update TOC.md**:
   - Update latest: "2025-10-21 - Category 1 COMPLETE ✅"
   - Update remaining: 13 tests (was 23)

5. **Move to Category 2** - System DAO tests can reuse CreateTestUsersAsync()!

**Total Time**: Phases A, B, C complete in ~2 hours 🎉

---

## 🔧 If Some Tests Fail

**Don't panic** - this is normal and expected.

**Example Fix Workflow**:

```powershell
# Step 1: Capture failures
dotnet test MTM_WIP_Application_Winforms.csproj `
  --filter "FullyQualifiedName~Integration.Dao_QuickButtons_Tests" `
  --logger "console;verbosity=detailed" > failures.txt

# Step 2: Read error messages
Get-Content failures.txt | Select-String -Pattern "Failed|Error|Exception" -Context 2

# Step 3: Group by pattern
# Example: 3 tests fail with "Procedure doesn't exist"
# Example: 2 tests fail with "Parameter 'p_User' not found"

# Step 4: Fix Group 1 (Missing procedures)
# Deploy procedures from Database/UpdatedStoredProcedures/ OR
# Add Assert.Inconclusive() to tests until procedures ready

# Step 5: Re-run affected tests
dotnet test --filter "FullyQualifiedName~UpdateQuickButtonAsync"

# Step 6: Move to next fix group
# Repeat until all pass
```

---

**Status**: Phase C verification pending  
**Action Required**: Run test command above and document results  
**Time Remaining**: 15 min (if all pass) to 1-2 hours (if fixes needed)
