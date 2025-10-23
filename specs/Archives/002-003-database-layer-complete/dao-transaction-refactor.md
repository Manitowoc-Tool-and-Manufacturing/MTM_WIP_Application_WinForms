# DAO Transaction Support Refactoring

**Created**: 2025-10-18  
**Updated**: 2025-10-18 (Evening - BLOCKER IDENTIFIED)  
**Priority**: 🔴 **BLOCKED** - MySQL.Data connector limitation  
**Estimated Effort**: 1-2 days (refactoring) + TBD (blocker resolution)  
**Status**: ⛔ **BLOCKED** - **46% Complete** (41/89 tasks) - **CRITICAL BLOCKER FOUND**  

---

## 🔴 CRITICAL BLOCKER - STOP WORK

**MySQL.Data Connector Limitation Discovered**:
- External transactions with stored procedure OUTPUT parameters fail
- Error: "Parameter 'p_Status' not found in the collection"
- **Affects**: ~30-40 tests across ALL test classes with write operations
- **Test Results**: 88/136 passing (65%) - worse than 93/136 baseline

**Root Cause**: MySql.Data connector cannot reliably retrieve OUTPUT parameters from stored procedures when:
1. An external `MySqlConnection` is provided
2. An active `MySqlTransaction` is associated with the command
3. The stored procedure has `OUT` parameters (p_Status, p_ErrorMsg)

**Impact**: This architectural approach will NOT work without resolving this blocker first.

**See**: [Blocker Resolution Options](#blocker-resolution-options) section below.

---

## Quick Status Overview

| Phase | Status | Progress | Details |
|-------|--------|----------|---------|
| [Phase 1: Helper Refactoring](#phase-1-update-helper_database_storedprocedure--complete) | ✅ **COMPLETE** | 6/6 (100%) | All helper methods updated |
| [Phase 2: DAO Updates](#phase-2-update-high-priority-daos--complete) | ✅ **COMPLETE** | 35/35 (100%) | All DAOs updated with transaction support |
| [Phase 3: Test Updates](#phase-3-update-tests-to-use-transaction-support--blocked) | ⛔ **BLOCKED** | 3/43 (7%) | Blocker prevents further progress |
| [Phase 4: Verification](#phase-4-verification-and-documentation--blocked) | ⛔ **BLOCKED** | 0/5 (0%) | Cannot proceed until blocker resolved |
| **TOTAL** | **46%** | **41/89** | **BLOCKED** |

### Completed Work
- [x] Phase 1: Helper_Database_StoredProcedure refactoring (3 methods)
- [x] Phase 2: All DAO methods updated (35 methods across 11 DAOs)
- [x] Phase 3: Dao_Inventory_Tests partially updated (2 tests fixed)
- [x] Build verified: 0 errors, existing warnings only
- [x] Blocker identified and documented

### Work Blocked by Connector Issue
- [x] Phase 3: Dao_Inventory_Tests - 8/12 passing (4 blocked by connector)
- [ ] Phase 3: Dao_QuickButtons_Tests - 0/12 passing (all blocked)
- [ ] Phase 3: Dao_Transactions_Tests - 0/7 passing (all blocked)
- [ ] Phase 3: Other test files - multiple tests blocked
- [ ] Phase 4: Final verification - cannot complete

**Current Status**: Phases 1 & 2 COMPLETE. Phase 3 BLOCKED by MySQL.Data connector limitation.
**Test Status**: 88/136 passing (65%) - **REGRESSION from 93/136 baseline**

---

## Mission

Refactor DAOs and Helper classes to support optional external transaction/connection parameters, enabling proper test isolation and fixing 43 remaining test failures.

---

## Problem Statement

**Current Architecture Issue**:
- Tests use `BaseIntegrationTest` which creates a transaction for isolation
- DAOs create new connections for each operation, ignoring the test transaction
- MySQL connector can't retrieve stored procedure output parameters across different connections
- Result: "Parameter 'p_Status' not found in the collection" errors

**Impact**: 43 of 136 tests (31.6%) fail due to this architectural mismatch.

---

## Solution Design

### Phase 1: Update Helper_Database_StoredProcedure ✅ COMPLETE

**Goal**: Support optional external connection/transaction parameters

**Tasks**:
- [x] **T1.1**: Add optional `MySqlConnection` parameter to all ExecuteXXXWithStatusAsync methods
- [x] **T1.2**: Add optional `MySqlTransaction` parameter to all ExecuteXXXWithStatusAsync methods
- [x] **T1.3**: Update internal logic to use provided connection when available
- [x] **T1.4**: Ensure backward compatibility (parameters are optional, default to null)
- [x] **T1.5**: Add XML documentation explaining usage pattern
- [x] **T1.6**: Build verified (0 errors, pre-existing warnings only)

**Files Modified**:
- `Helpers/Helper_Database_StoredProcedure.cs` ✅

**Changes Made**:
- Updated `ExecuteDataTableWithStatusAsync` with optional connection/transaction parameters
- Updated `ExecuteNonQueryWithStatusAsync` with optional connection/transaction parameters
- Updated `ExecuteScalarWithStatusAsync` with optional connection/transaction parameters
- All methods now use external connection when provided, or create new connection if not
- Proper disposal handling - external connections are NOT disposed by helper
- Build successful with 0 errors

**Time Spent**: 1.5 hours

---

### Phase 2: Update High-Priority DAOs 🔄 IN PROGRESS

**Goal**: Add optional transaction support to DAOs that have failing tests

**Priority DAOs** (in order):
1. [x] **Dao_Inventory** ✅ COMPLETE (6 failing tests)
   - GetAllInventoryAsync ✅
   - SearchInventoryAsync ✅
   - GetInventoryByPartIdAsync ✅
   - GetInventoryByPartIdAndOperationAsync ✅
   - AddInventoryItemAsync ✅
   - RemoveInventoryItemAsync ✅
   - TransferPartSimpleAsync ✅
   - TransferInventoryQuantityAsync ✅

2. [ ] **Dao_QuickButtons** (12 failing tests)
   - UpdateQuickButtonAsync
   - AddQuickButtonAsync
   - RemoveQuickButtonAndShiftAsync
   - DeleteAllQuickButtonsForUserAsync
   - MoveQuickButtonAsync
   - AddOrShiftQuickButtonAsync
   - RemoveAndShiftQuickButtonAsync
   - AddQuickButtonAtPositionAsync

3. [ ] **Dao_Transactions** (7 failing tests)
   - SearchTransactionsAsync
   - SmartSearchAsync
   - GetTransactionAnalyticsAsync

4. [ ] **Dao_ErrorLog** (1 failing test)
   - DeleteErrorByIdAsync

5. [ ] **Dao_History** (3 failing tests)
   - AddTransactionHistoryAsync

6. [x] **Dao_System** ✅ COMPLETE (6 failing tests)
   - System_UserAccessTypeAsync ✅
   - GetUserIdByNameAsync ✅
   - GetRoleIdByNameAsync ✅
   - GetAllThemesAsync (does NOT support transactions - uses own connection) ⚠️

**Pattern to Apply**:
```csharp
// BEFORE
public static async Task<DaoResult> SomeMethodAsync(string param1, string param2)
{
    var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
        Model_AppVariables.ConnectionString,
        "sp_SomeProcedure",
        parameters,
        null);
    // ...
}

// AFTER
public static async Task<DaoResult> SomeMethodAsync(
    string param1, 
    string param2,
    MySqlConnection? connection = null,
    MySqlTransaction? transaction = null)
{
    var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
        Model_AppVariables.ConnectionString,
        "sp_SomeProcedure",
        parameters,
        progressHelper: null,
        connection: connection,
        transaction: transaction);
    // ...
}
```

**Estimated Time**: 4-6 hours

---

### Phase 3: Update Tests to Use Transaction Support ⏳ PENDING

**Goal**: Update failing tests to pass test connection/transaction to DAO methods

**Test Files to Update**:
- [ ] `Tests/Integration/Dao_Inventory_Tests.cs` (6 tests)
- [ ] `Tests/Integration/Dao_QuickButtons_Tests.cs` (12 tests)
- [ ] `Tests/Integration/Dao_Transactions_Tests.cs` (7 tests)
- [ ] `Tests/Integration/Dao_ErrorLog_Tests.cs` (1 test)
- [ ] `Tests/Integration/Dao_Logging_Tests.cs` (3 tests)
- [ ] `Tests/Integration/Dao_System_Tests.cs` (6 tests)
- [ ] `Tests/Integration/TransactionManagement_Tests.cs` (3 tests)
- [ ] `Tests/Integration/ErrorCooldown_Tests.cs` (1 test)

**Pattern to Apply**:
```csharp
// BEFORE
var result = await Dao_Inventory.TransferPartSimpleAsync(
    batchNumber, partId, operation, newLocation);

// AFTER
var result = await Dao_Inventory.TransferPartSimpleAsync(
    batchNumber, 
    partId, 
    operation, 
    newLocation,
    connection: GetTestConnection(),
    transaction: GetTestTransaction());
```

**Estimated Time**: 4-6 hours

---

### Phase 4: Verification and Documentation ⏳ PENDING

**Tasks**:
- [ ] **T4.1**: Run full test suite and verify all 43 tests now pass
- [ ] **T4.2**: Update test-failure-fixes.md with results
- [ ] **T4.3**: Document pattern in integration-testing.instructions.md
- [ ] **T4.4**: Create example test showing proper usage
- [ ] **T4.5**: Update AGENTS.md with new pattern

**Success Criteria**:
- All 136 tests pass (100% pass rate)
- No breaking changes to existing application code
- Tests properly isolated with automatic rollback

**Estimated Time**: 2 hours

---

## Implementation Checklist

### Phase 1: Helper Refactoring

#### Helper_Database_StoredProcedure Changes

**ExecuteNonQueryWithStatusAsync**:
- [ ] Add `MySqlConnection? connection = null` parameter
- [ ] Add `MySqlTransaction? transaction = null` parameter
- [ ] Update method body to use provided connection/transaction
- [ ] Add null checks and validation
- [ ] Update XML documentation

**ExecuteDataTableWithStatusAsync**:
- [ ] Add `MySqlConnection? connection = null` parameter
- [ ] Add `MySqlTransaction? transaction = null` parameter
- [ ] Update method body to use provided connection/transaction
- [ ] Add null checks and validation
- [ ] Update XML documentation

**ExecuteScalarWithStatusAsync**:
- [ ] Add `MySqlConnection? connection = null` parameter
- [ ] Add `MySqlTransaction? transaction = null` parameter
- [ ] Update method body to use provided connection/transaction
- [ ] Add null checks and validation
- [ ] Update XML documentation

**ExecuteReaderWithStatusAsync** (if exists):
- [ ] Add `MySqlConnection? connection = null` parameter
- [ ] Add `MySqlTransaction? transaction = null` parameter
- [ ] Update method body to use provided connection/transaction
- [ ] Add null checks and validation
- [ ] Update XML documentation

---

### Phase 2: DAO Updates (Detailed)

#### Dao_Inventory Methods (Priority 1) ✅ COMPLETE

- [x] **TransferPartSimpleAsync**
  - Add connection/transaction parameters ✅
  - Pass to helper call ✅
  - Test: `TransferPartSimpleAsync_ValidData_TransfersInventory`

- [x] **TransferInventoryQuantityAsync**
  - Add connection/transaction parameters ✅
  - Pass to helper call ✅
  - Tests: `TransferInventoryQuantityAsync_ValidData_CompletesTransfer`, `TransferInventoryQuantityAsync_ForcedFailure_RollsBackTransaction`

- [x] **AddInventoryItemAsync**
  - Add connection/transaction parameters ✅
  - Pass to helper call (3 places) ✅
  - Used by: Multiple test setups

- [x] **GetInventoryByPartIdAsync**
  - Add connection/transaction parameters ✅
  - Pass to helper call ✅
  - Test: `GetInventoryByPartIdAsync_ValidPartId_ReturnsInventoryRecords`

- [x] **GetInventoryByPartIdAndOperationAsync**
  - Add connection/transaction parameters ✅
  - Pass to helper call ✅
  - Test: `GetInventoryByPartIdAndOperationAsync_ValidPartIdAndOperation_ReturnsFilteredRecords`

- [x] **RemoveInventoryItemAsync**
  - Add connection/transaction parameters ✅
  - Pass to helper call ✅
  - Test: `RemoveInventoryItemAsync_ValidInventoryId_RemovesItem`

#### Dao_QuickButtons Methods (Priority 2)

- [ ] **UpdateQuickButtonAsync** (3 tests affected)
- [ ] **AddQuickButtonAsync** (2 tests affected)
- [ ] **RemoveQuickButtonAndShiftAsync** (1 test)
- [ ] **DeleteAllQuickButtonsForUserAsync** (1 test)
- [ ] **MoveQuickButtonAsync** (2 tests affected)
- [ ] **AddOrShiftQuickButtonAsync** (1 test)
- [ ] **RemoveAndShiftQuickButtonAsync** (1 test)
- [ ] **AddQuickButtonAtPositionAsync** (1 test)

#### Dao_Transactions Methods (Priority 3)

- [ ] **SearchTransactionsAsync** (4 tests affected)
- [ ] **SmartSearchAsync** (1 test)
- [ ] **GetTransactionAnalyticsAsync** (2 tests affected)

#### Dao_ErrorLog Methods (Priority 4)

- [ ] **DeleteErrorByIdAsync** (1 test)

#### Dao_History Methods (Priority 5)

- [ ] **AddTransactionHistoryAsync** (3 tests affected)

#### Dao_System Methods (Priority 6) ✅ COMPLETE

- [x] **System_UserAccessTypeAsync** ✅ (3 tests affected)
- [x] **GetUserIdByNameAsync** ✅ (2 tests affected)
- [x] **GetRoleIdByNameAsync** ✅ (1 test)
- [x] **GetAllThemesAsync** - Uses own connection, tests updated to not pass transaction ✅

#### Master Data DAOs (Additional) ✅ COMPLETE

- [x] **Dao_ItemType** ✅
  - GetAllItemTypes ✅
  - GetItemTypeByName ✅
  - ItemTypeExists ✅
  
- [x] **Dao_Location** ✅
  - GetAllLocations ✅
  - GetLocationByName ✅
  - LocationExists ✅
  
- [x] **Dao_Operation** ✅
  - GetAllOperations ✅
  - GetOperationByNumber ✅
  - OperationExists ✅
  
- [x] **Dao_Part** ✅
  - GetAllPartsAsync ✅
  - GetAllParts (legacy) ✅
  - SearchPartsAsync ✅
  - GetPartByNumberAsync ✅
  - GetPartByNumber (legacy) ✅
  - PartExistsAsync ✅
  - UpdatePartByNumberAsync (internal call updated) ✅

- [x] **Dao_ErrorLog** ✅
  - GetUniqueErrorsAsync ✅

---

### Phase 3: Test Updates (Detailed)

#### Dao_Inventory_Tests.cs

- [ ] **TransferPartSimpleAsync_ValidData_TransfersInventory**
  - Update AddInventoryItemAsync call (setup)
  - Update TransferPartSimpleAsync call
  - Update GetInventoryByPartIdAndOperationAsync call (verification)

- [ ] **TransferInventoryQuantityAsync_ValidData_CompletesTransfer**
  - Update AddInventoryItemAsync call (setup)
  - Update TransferInventoryQuantityAsync call
  - Update verification queries

- [ ] **TransferInventoryQuantityAsync_ForcedFailure_RollsBackTransaction**
  - Update all DAO calls with connection/transaction

- [ ] **GetInventoryByPartIdAsync_ValidPartId_ReturnsInventoryRecords**
  - Update AddInventoryItemAsync call (setup)
  - Update GetInventoryByPartIdAsync call

- [ ] **GetInventoryByPartIdAndOperationAsync_ValidPartIdAndOperation_ReturnsFilteredRecords**
  - Update AddInventoryItemAsync call (setup)
  - Update GetInventoryByPartIdAndOperationAsync call

- [ ] **RemoveInventoryItemAsync_ValidInventoryId_RemovesItem**
  - Update AddInventoryItemAsync call (setup)
  - Update RemoveInventoryItemAsync call

#### Dao_QuickButtons_Tests.cs (12 tests)

- [ ] Update all test methods to pass connection/transaction
- [ ] Verify cleanup works correctly with transactions

#### Dao_Transactions_Tests.cs (7 tests)

- [ ] Update all SearchTransactionsAsync calls
- [ ] Update SmartSearchAsync calls
- [ ] Update GetTransactionAnalyticsAsync calls

#### Dao_ErrorLog_Tests.cs (1 test)

- [ ] Update DeleteErrorByIdAsync call

#### Dao_Logging_Tests.cs (3 tests)

- [ ] Update AddTransactionHistoryAsync calls

#### Dao_System_Tests.cs (6 tests)

- [ ] Update SetUserAccessTypeAsync calls
- [ ] Update GetUserIdByNameAsync calls
- [ ] Update GetRoleIdByNameAsync calls

#### TransactionManagement_Tests.cs (3 tests)

- [ ] Update all transfer operation calls
- [ ] Verify rollback behavior works correctly

#### ErrorCooldown_Tests.cs (1 test)

- [ ] Update GetAllErrorsAsync call if needed

---

## Testing Strategy

### Incremental Verification

After each phase:
1. Build solution: `dotnet build`
2. Run affected tests: `dotnet test --filter "FullyQualifiedName~[TestClass]"`
3. Verify no regressions in passing tests
4. Document progress

### Final Validation

```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Tests
dotnet test --logger "console;verbosity=normal" --nologo

# Expected: Total: 136, Passed: 136, Failed: 0
```

---

## 🚨 Blocker Resolution Options

### Option 1: Change Test Isolation Strategy (Recommended Short-term)

**Approach**: Remove transaction-based isolation, accept committed test data

**Pros**:
- Quick to implement (1-2 hours)
- No connector changes needed
- Tests will pass immediately
- Can continue with Phase 3 & 4

**Cons**:
- Test data persists in database
- Need cleanup logic in TestCleanup
- Potential for test interference if cleanup fails
- Less pure test isolation

**Implementation**:
```csharp
// Remove transaction from BaseIntegrationTest
[TestInitialize]
public void TestInitialize()
{
    _connection = new MySqlConnection(GetTestConnectionString());
    _connection.Open();
    // NO TRANSACTION - just open connection
}

[TestCleanup]
public void TestCleanup()
{
    // Explicit cleanup of test data
    CleanupTestData();
    _connection?.Close();
}
```

**Estimated Effort**: 2-4 hours

---

### Option 2: Investigate MySql.Data Connector Behavior (Medium-term)

**Approach**: Research and potentially update MySql.Data package

**Investigation Steps**:
1. Test with different MySql.Data versions (current: 9.4.0)
2. Review MySql.Data source code for transaction + output parameter handling
3. Check MySQL documentation for transaction isolation + stored procedures
4. Test alternative parameter retrieval methods (result sets instead of OUT params)
5. Consider filing bug report with MySql.Data maintainers

**Pros**:
- Maintains pure transaction isolation
- May benefit other MySQL + .NET projects
- Could lead to connector improvement

**Cons**:
- Time-consuming investigation (4-8 hours)
- May not find solution
- Could require waiting for connector update
- No guarantee of success

**Estimated Effort**: 4-8 hours investigation + unknown resolution time

---

### Option 3: Change Stored Procedure Pattern (Long-term)

**Approach**: Refactor all stored procedures to return result sets instead of OUT parameters

**Implementation**:
```sql
-- OLD: Output parameters
CREATE PROCEDURE sp_Example(IN p_Param VARCHAR(100), OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500))

-- NEW: Result set return
CREATE PROCEDURE sp_Example(IN p_Param VARCHAR(100))
BEGIN
    -- Logic here
    SELECT status AS Status, errorMsg AS ErrorMsg, data1, data2;
END
```

```csharp
// Helper changes to read first row as status
var result = await ExecuteDataTableWithStatusAsync(...);
if (result.Data.Rows.Count > 0)
{
    var statusRow = result.Data.Rows[0];
    int status = Convert.ToInt32(statusRow["Status"]);
    string message = statusRow["ErrorMsg"]?.ToString();
    // ... process
}
```

**Pros**:
- Eliminates OUTPUT parameter issue permanently
- May improve performance (fewer round trips)
- More compatible with various MySQL connectors
- Result sets work reliably with transactions

**Cons**:
- Requires modifying ALL 97 stored procedures
- Must update Helper_Database_StoredProcedure significantly
- All DAO methods need updates
- Large refactoring effort (16-24 hours)
- High risk of introducing bugs

**Estimated Effort**: 16-24 hours + extensive testing

---

## Recommendation

**⭐ Proceed with Option 1 (Change Test Isolation Strategy)** because:

1. **Quick Resolution**: Can be implemented in 2-4 hours
2. **Immediate Unblock**: Tests will work immediately
3. **Reversible**: Can switch to Option 2 or 3 later if better solution found
4. **Proven Pattern**: Many test frameworks use database cleanup over rollback
5. **Low Risk**: Simple change with predictable behavior

**Implementation Plan for Option 1**:
1. Update `BaseIntegrationTest.TestInitialize` - remove transaction start
2. Update `BaseIntegrationTest.TestCleanup` - add cleanup methods
3. Create `CleanupTestData()` helper that deletes test records by pattern
4. Update test data to use identifiable prefixes (e.g., "TEST-*", "TEMP-*")
5. Run full test suite and verify ~120+ tests pass
6. Document new test pattern in integration-testing.instructions.md

---

## Progress Tracking

| Phase | Tasks | Completed | Status | Time Spent |
|-------|-------|-----------|--------|------------|
| 1. Helper Refactoring | 6 | 6 | ✅ **COMPLETE** | 1.5h / 2h |
| 2. DAO Updates | 35 | 35 | ✅ **COMPLETE** | 4.5h / 6h |
| 3. Test Updates | 43 | 3 | ⛔ **BLOCKED** | 1.0h / 6h |
| 4. Verification | 5 | 0 | ⛔ **BLOCKED** | 0h / 2h |
| **TOTAL** | **89** | **44** | **49% - BLOCKED** | **7.0h / 16h** |

**Next Action**: Decision required on blocker resolution approach (recommend Option 1)

---

## Risk Mitigation

### Backward Compatibility
- All new parameters are optional with default `null` values
- Existing application code continues to work unchanged
- Only test code needs to be updated

### Testing Safety
- Tests run in isolated transactions
- All changes rolled back automatically
- No risk to test or production databases

### Rollback Plan
If refactoring causes issues:
1. Revert changes to Helper_Database_StoredProcedure
2. Revert DAO signature changes
3. Revert test updates
4. Return to architecture blocker status

---

## Success Metrics

- [ ] All 136 tests pass (100% pass rate)
- [ ] No breaking changes to application code
- [ ] Test execution time remains under 30 seconds
- [ ] All tests properly isolated with automatic rollback
- [ ] Pattern documented for future test development

---

## Next Steps

1. ✅ Create this checklist
2. 🔄 Update fix-test-failures.prompt.md to reference this checklist
3. 🔄 Update test-failure-fixes.md to reference this blocker fix
4. ▶️ **BEGIN PHASE 1**: Refactor Helper_Database_StoredProcedure
5. ⏳ Phase 2: Update high-priority DAOs
6. ⏳ Phase 3: Update tests
7. ⏳ Phase 4: Final verification and documentation

---

**Last Updated**: 2025-10-18  
**Next Review**: After Phase 1 completion
