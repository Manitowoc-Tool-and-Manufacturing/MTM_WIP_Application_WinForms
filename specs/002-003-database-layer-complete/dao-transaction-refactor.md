# DAO Transaction Support Refactoring

**Created**: 2025-10-18  
**Priority**: 🔴 **CRITICAL** - Blocks 43 test failures  
**Estimated Effort**: 1-2 days  
**Status**: In Progress  

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

### Phase 2: Update High-Priority DAOs ⏳ PENDING

**Goal**: Add optional transaction support to DAOs that have failing tests

**Priority DAOs** (in order):
1. [ ] **Dao_Inventory** (6 failing tests)
   - TransferPartSimpleAsync
   - TransferInventoryQuantityAsync
   - GetInventoryByPartIdAsync
   - GetInventoryByPartIdAndOperationAsync
   - RemoveInventoryItemAsync
   - AddInventoryItemAsync (used by test setup)

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

6. [ ] **Dao_System** (6 failing tests)
   - SetUserAccessTypeAsync
   - GetUserIdByNameAsync
   - GetRoleIdByNameAsync

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

#### Dao_System Methods (Priority 6)

- [ ] **SetUserAccessTypeAsync** (3 tests affected)
- [ ] **GetUserIdByNameAsync** (2 tests affected)
- [ ] **GetRoleIdByNameAsync** (1 test)

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

## Progress Tracking

| Phase | Tasks | Completed | Status | Time Spent |
|-------|-------|-----------|--------|------------|
| 1. Helper Refactoring | 6 | 6 | ✅ **COMPLETE** | 1.5h / 2h |
| 2. DAO Updates | 35 | 6 | 🔄 In Progress (Dao_Inventory complete) | 1.0h / 6h |
| 3. Test Updates | 43 | 0 | ⏳ Pending | 0h / 6h |
| 4. Verification | 5 | 0 | ⏳ Pending | 0h / 2h |
| **TOTAL** | **89** | **12** | **13%** | **2.5h / 16h** |

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
