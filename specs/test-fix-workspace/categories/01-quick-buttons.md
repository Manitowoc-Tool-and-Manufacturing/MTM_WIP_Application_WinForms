# Category 1: Quick Button Failures

**Priority**: 🔴 HIGH  
**Status**: ✅ COMPLETE  
**Tests**: 12/12 passing (100%)  
**Actual Effort**: 4 hours

---

## Root Cause Analysis

**Problem**: Tests attempt UPDATE/MOVE/DELETE operations on quick buttons that don't exist in test database.

**Investigation Findings** (via MCP tools):
- `analyze_stored_procedures` revealed quick button SPs require existing usr_users.UserID foreign keys
- `validate_dao_patterns` confirmed Dao_QuickButtons methods correctly call stored procedures
- `verify_test_seed` showed test database has ZERO quick button test data

**Why Tests Are Failing**:
1. Tests assume quick button records exist (they don't)
2. Tests assume test users exist (they don't)
3. Foreign key constraints prevent button creation without valid users
4. No test data setup/teardown pattern established

**Root Cause**: Missing test data setup strategy. Tests need:
- Test users in usr_users table (TEST-USER, TEST-USER-2)
- Test quick buttons in sys_quick_buttons table
- Proper setup/teardown to maintain test isolation

---

## Fix Strategy

### High-Level Approach

1. **Create Test Data Setup Helper** (BaseIntegrationTest or dedicated helper)
   - Create method: `CreateTestUsersAsync()` 
   - Create method: `CreateTestQuickButtonsAsync()`
   - Use ON DUPLICATE KEY UPDATE to prevent errors on re-run

2. **Update Each Test** to call setup before execution
   - Add test data setup in TestInitialize or individual test setup
   - Verify test data exists before executing DAO operations
   - Clean up test data in TestCleanup (optional for test DB)

3. **Establish Pattern** for future tests
   - Document test data setup pattern in integration-testing.instructions.md
   - Create reusable methods other test categories can use
   - Consider MCP tool: `generate_test_seed_sql` for complex scenarios

### Test Data Requirements

**Test Users Needed**:
```sql
INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
VALUES 
    ('TEST-USER', 'Test User', SHA2('password', 256), 1, NOW()),
    ('TEST-USER-2', 'Test User 2', SHA2('password', 256), 1, NOW())
ON DUPLICATE KEY UPDATE UserID = UserID;
```

**Test Quick Buttons Needed**:
```sql
INSERT INTO sys_quick_buttons (UserID, ButtonText, Operation, Location, PartNumber, Position, IsActive)
VALUES
    ('TEST-USER', 'Test Button 1', '100', 'FLOOR', 'PART-001', 1, 1),
    ('TEST-USER', 'Test Button 2', '100', 'FLOOR', 'PART-002', 2, 1),
    ('TEST-USER-2', 'Test Button 3', '110', 'SHIPPING', 'PART-003', 1, 1);
```

### Code Changes Needed

**Location**: `Tests/Integration/BaseIntegrationTest.cs` (add helpers) or `Tests/Integration/Dao_QuickButtons_Tests.cs` (per-class setup)

**Changes**:
1. Add `CreateTestUsersAsync()` method
2. Add `CreateTestQuickButtonsAsync()` method
3. Add `CleanupTestQuickButtonsAsync()` method
4. Add `CleanupTestUsersAsync()` method
5. Call setup methods in TestInitialize or before each test
6. Optionally call cleanup in TestCleanup

### Verification Steps

After implementing setup:
1. Run `dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"`
2. Verify all 10 tests pass
3. Run tests twice to ensure idempotent (ON DUPLICATE KEY UPDATE working)
4. Check test database for residual TEST-* data (should be present after, can clean manually or via script)

---

## Test List

### Tests to Fix (0/10 complete)

- [ ] **AddQuickButton_ValidData_InsertsButton** | `Tests/Integration/Dao_QuickButtons_Tests.cs:45`
  - **Error**: "Cannot add quick button - user ID 'TEST-USER' does not exist"
  - **Fix approach**: Create TEST-USER before test, verify insertion successful
  - **Verification**: Button appears in sys_quick_buttons with correct UserID

- [ ] **GetQuickButton_ExistingButton_ReturnsButton** | `Tests/Integration/Dao_QuickButtons_Tests.cs:78`
  - **Error**: "No quick buttons found for test retrieval"
  - **Fix approach**: Create test button before test, retrieve by ButtonID
  - **Verification**: Retrieved button matches created button data

- [ ] **GetQuickButtonsByUser_ExistingUser_ReturnsButtons** | `Tests/Integration/Dao_QuickButtons_Tests.cs:112`
  - **Error**: "User 'TEST-USER' has no quick buttons"
  - **Fix approach**: Create 2-3 buttons for TEST-USER, verify count and data
  - **Verification**: Returns correct number of buttons for user

- [ ] **UpdateQuickButton_ExistingButton_UpdatesButton** | `Tests/Integration/Dao_QuickButtons_Tests.cs:145`
  - **Error**: "Cannot update - button does not exist"
  - **Fix approach**: Create button, update ButtonText, verify change persisted
  - **Verification**: Updated button reflects new ButtonText value

- [ ] **UpdateQuickButtonPositions_ValidPositions_UpdatesPositions** | `Tests/Integration/Dao_QuickButtons_Tests.cs:178`
  - **Error**: "No buttons to reposition"
  - **Fix approach**: Create 3+ buttons, update positions, verify new order
  - **Verification**: All buttons have updated Position values

- [ ] **DeleteQuickButton_ExistingButton_DeletesButton** | `Tests/Integration/Dao_QuickButtons_Tests.cs:211`
  - **Error**: "Cannot delete - button does not exist"
  - **Fix approach**: Create button, delete it, verify removal
  - **Verification**: Button no longer retrievable after deletion

- [ ] **GetNextPosition_ExistingButtons_ReturnsNextPosition** | `Tests/Integration/Dao_QuickButtons_Tests.cs:244`
  - **Error**: "No existing buttons to determine next position"
  - **Fix approach**: Create buttons at positions 1,2,3, verify next is 4
  - **Verification**: Returns max(Position) + 1

- [ ] **MoveQuickButton_ValidMove_UpdatesPosition** | `Tests/Integration/Dao_QuickButtons_Tests.cs:277`
  - **Error**: "Cannot move - button does not exist"
  - **Fix approach**: Create button at position 1, move to position 5, verify
  - **Verification**: Button Position updated correctly

- [ ] **GetQuickButtonCount_ExistingUser_ReturnsCount** | `Tests/Integration/Dao_QuickButtons_Tests.cs:310`
  - **Error**: "User 'TEST-USER' has zero buttons"
  - **Fix approach**: Create 3 buttons for TEST-USER, verify count returns 3
  - **Verification**: Count matches actual number of buttons

- [ ] **QuickButtonExists_ExistingButton_ReturnsTrue** | `Tests/Integration/Dao_QuickButtons_Tests.cs:343`
  - **Error**: "Button does not exist for exists check"
  - **Fix approach**: Create button, verify exists returns true
  - **Verification**: Returns true for existing button, false for non-existent

---

## Relevant MCP Tools

### For This Category

- **generate_test_seed_sql** - Generate SQL seed scripts for test users and buttons
  ```
  generate_test_seed_sql(
    config_file: "path/to/quick-button-seed-config.json",
    output_sql: "path/to/seed-quick-buttons.sql"
  )
  ```
  *Use when*: Setting up complex test data scenarios

- **verify_test_seed** - Validate seeded data exists before running tests
  ```
  verify_test_seed(
    config_file: "path/to/quick-button-seed-config.json"
  )
  ```
  *Use when*: Confirming test data setup worked correctly

- **validate_dao_patterns** - Verify DAO code follows MTM standards
  ```
  validate_dao_patterns(
    dao_dir: "Data/",
    recursive: true
  )
  ```
  *Use when*: After modifying Dao_QuickButtons.cs

- **audit_database_cleanup** - Check for residual TEST-* data
  ```
  audit_database_cleanup(
    config_file: "path/to/cleanup-config.json",
    dry_run: true
  )
  ```
  *Use when*: After test runs to verify isolation

---

## Commands

### PowerShell Test Commands

```powershell
# Run all quick button tests
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"

# Run single test
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests.AddQuickButton_ValidData_InsertsButton"

# Run with detailed output
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests" --logger "console;verbosity=detailed"
```

### SQL Manual Testing

```sql
-- Connect to test database
USE mtm_wip_application_winforms_test;

-- Check test users exist
SELECT * FROM usr_users WHERE UserID LIKE 'TEST-%';

-- Check test buttons exist
SELECT * FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%';

-- Create test user manually
INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
VALUES ('TEST-USER', 'Test User', SHA2('password', 256), 1, NOW())
ON DUPLICATE KEY UPDATE UserID = UserID;

-- Create test button manually
INSERT INTO sys_quick_buttons (UserID, ButtonText, Operation, Location, PartNumber, Position, IsActive)
VALUES ('TEST-USER', 'Manual Test Button', '100', 'FLOOR', 'PART-001', 1, 1);

-- Clean up test data
DELETE FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%';
DELETE FROM usr_users WHERE UserID LIKE 'TEST-%';
```

---

## Code Locations

### DAO Files
- `Data/Dao_QuickButtons.cs` - Quick button data access methods

### Test Files
- `Tests/Integration/Dao_QuickButtons_Tests.cs` - All 10 failing tests
- `Tests/Integration/BaseIntegrationTest.cs` - Base class for integration tests (add helpers here)

### Stored Procedures
- `Database/UpdatedStoredProcedures/ReadyForVerification/sp_QuickButtons_Add.sql`
- `Database/UpdatedStoredProcedures/ReadyForVerification/sp_QuickButtons_Get.sql`
- `Database/UpdatedStoredProcedures/ReadyForVerification/sp_QuickButtons_GetByUser.sql`
- `Database/UpdatedStoredProcedures/ReadyForVerification/sp_QuickButtons_Update.sql`
- `Database/UpdatedStoredProcedures/ReadyForVerification/sp_QuickButtons_Delete.sql`

### Database Tables
- `usr_users` - User records (foreign key parent)
- `sys_quick_buttons` - Quick button records

---

## Notes & Gotchas

### Common Mistakes to Avoid

1. **Forgetting ON DUPLICATE KEY UPDATE**: Without this, re-running tests will fail with duplicate key errors
2. **Wrong cleanup order**: Delete child records (sys_quick_buttons) BEFORE parent records (usr_users) due to foreign keys
3. **Hardcoding ButtonID**: ButtonID is AUTO_INCREMENT, use retrieved values not hardcoded ones
4. **Assuming button order**: Don't rely on ButtonID order, use Position field for ordering

### Dependencies

- **Blocks**: Category 2 (System DAO) tests also need test users, can share helper method
- **Depends on**: None - this category can be completed independently

### Test Isolation Considerations

- Quick button tests should use unique UserID values (TEST-USER, TEST-USER-2) to avoid conflicts with other test categories
- Consider using GUIDs in UserID for guaranteed uniqueness: `TEST-QB-{GUID}`
- Test cleanup is optional for test database but recommended for cleanliness

### Performance Considerations

- Creating test data adds time to each test (~100-200ms per setup call)
- Consider TestClass-level setup (create once per class) vs test-level setup (create per test)
- ON DUPLICATE KEY UPDATE is fast and prevents errors on re-runs

---

## Progress Tracking

**Last Updated**: 2025-10-21  
**Current Status**: ✅ COMPLETE - All 12 tests passing consistently across multiple runs  
**Next Step**: None - Category complete

**Completion**: 12/12 (100%)  
**Progress Bar**: [██████████] 100%

---

## Implementation Notes

### Session 2025-10-21: Category COMPLETE ✅

**Phase A: Test Data Foundation - COMPLETED ✅** (Session 2025-10-19)

**Phase B: Test Method Updates - COMPLETED ✅** (Session 2025-10-21)

**Phase C: Bug Fixes & Validation - COMPLETED ✅** (Session 2025-10-21)

**Changes Made to BaseIntegrationTest.cs**:

1. **Added `CreateTestUsersAsync()` method** (Lines ~270-320)
   - Creates 4 test users in usr_users table:
     - TEST-USER: Regular active user (password: TestPass123)
     - TEST-ADMIN: Admin user (password: AdminPass123)  
     - TEST-INACTIVE: Inactive user (password: password)
     - TEST-USER-2: Second regular user (password: TestPass456)
   - Uses SHA2 hashing for passwords (matches production pattern)
   - Uses ON DUPLICATE KEY UPDATE for idempotency (safe to call multiple times)
   - Includes comprehensive XML documentation

2. **Added `CreateTestQuickButtonsAsync()` method** (Lines ~322-400)
   - Targets sys_last_10_transactions table (stores quick button data)
   - Creates 4 test quick buttons for TestUser_QB test user
   - Checks table existence before attempting inserts
   - Dynamically inspects column structure (User vs UserID)
   - Uses ON DUPLICATE KEY UPDATE for idempotency
   - Automatically creates test users if they don't exist

3. **Added `CleanupTestQuickButtonsAsync()` method** (Lines ~402-450)
   - Removes test quick buttons from sys_last_10_transactions
   - Checks table existence before cleanup
   - Pattern matches on User LIKE 'Test%' OR 'TEST-%'
   - Safe to call multiple times (idempotent)

4. **Added `CleanupTestUsersAsync()` method** (Lines ~452-500)
   - Removes test users from usr_users table
   - Automatically cleans dependent quick button records first (foreign key safety)
   - Pattern matches on UserID LIKE 'TEST-%'
   - Safe to call multiple times (idempotent)

5. **Updated `CleanupTestData()` method** (Line ~260)
   - Added calls to new cleanup helpers
   - Maintains proper cleanup order (dependent records before parent records)

**Build Status**: ✅ Compiles successfully with 0 new warnings/errors

**Verification Performed**:
- Full rebuild: SUCCESS (65 existing warnings, no new issues)
- Code follows BaseIntegrationTest patterns
- Proper async/await usage throughout
- XML documentation complete for all new methods
- Table existence checks prevent runtime errors

**Changes Made to Dao_QuickButtons_Tests.cs**:

1. **Added `SetupTestDataAsync()` private method** (Lines ~25-35)
   - Calls CreateTestUsersAsync() and CreateTestQuickButtonsAsync()
   - Used by all tests to ensure test data exists

**Phase C: Bug Fixes & Final Validation - COMPLETED ✅** (Session 2025-10-21)

**Critical Bugs Fixed**:

1. **Fixed table column mismatch**: `PartId` → `PartID` in test setup
2. **Added connectionString parameter**: All 8 DAO methods now accept optional `connectionString` parameter for test database support
3. **Fixed position handling**: Removed incorrect `+1` offset in Move/Remove methods that was converting 1-based positions to invalid values
4. **Fixed contiguous position requirement**: Updated test data to create positions 1-10 contiguously (no gaps) as required by quick button business rules
5. **Fixed regex corruption**: Corrected PowerShell bulk edit that created `connectionString ?? connectionString ?? ...` duplication

**Test Data Updates**:
- Created 10 contiguous quick button positions (1-10) for TestUser_QB
- Created 1 quick button at position 1 for TestUser_QB_2
- Ensures proper testing of boundary conditions (position 1 and position 10)

**Final Validation**:
- ✅ All 12 tests passing (100%)
- ✅ Ran 5 consecutive times - 100% pass rate
- ✅ Test isolation confirmed - cleanup working correctly
- ✅ No residual data issues between runs
- ✅ Average test duration: 950ms per full suite

**Test Results Summary**:
```
Run 1: ✅ 12/12 passed (912 ms)
Run 2: ✅ 12/12 passed (966 ms)  
Run 3: ✅ 12/12 passed (1000 ms)
Run 4: ✅ 12/12 passed (955 ms)
Run 5: ✅ 12/12 passed (932 ms)
```
   - Calls `CreateTestUsersAsync()` from BaseIntegrationTest
   - Calls `CreateTestQuickButtonsAsync()` from BaseIntegrationTest
   - Idempotent - safe to call multiple times per test
   - Logs completion to test output

2. **Updated ALL 10 test methods** to call `SetupTestDataAsync()`:
   - UpdateQuickButtonAsync_ValidData_UpdatesButton ✅
   - AddQuickButtonAsync_ValidData_AddsButton ✅
   - RemoveQuickButtonAndShiftAsync_ValidPosition_RemovesAndShifts ✅
   - DeleteAllQuickButtonsForUserAsync_ValidUser_DeletesAllButtons ✅
   - MoveQuickButtonAsync_ValidPositions_MovesButton ✅
   - AddOrShiftQuickButtonAsync_ValidData_AddsOrShifts ✅
   - RemoveAndShiftQuickButtonAsync_ValidPosition_RemovesAndShifts ✅
   - AddQuickButtonAtPositionAsync_ValidData_AddsAtPosition ✅
   - UpdateQuickButtonAsync_Position1_UpdatesButton ✅
   - UpdateQuickButtonAsync_Position10_UpdatesButton ✅
   - MoveQuickButtonAsync_SamePosition_HandlesGracefully ✅
   - QuickButtonWorkflow_CompleteSequence_ExecutesSuccessfully ✅

3. **Added TestUser2 constant** for multi-user test scenarios

4. **Build Verification**: ✅ SUCCESS with 0 warnings in edited files

**Next Steps for Phase C (Test Execution & Validation)**:

1. Run quick button tests individually to verify:
   - Test data setup works correctly
   - Tests can find and use test data  
   - Cleanup works properly between runs
   - All 10 tests pass

2. Fix any runtime issues discovered:
   - Missing stored procedures
   - Parameter mismatches
   - Table structure differences
   - Data format expectations

3. Document actual vs expected behavior for failing tests

**Design Decisions**:

- **Why sys_last_10_transactions?**: Existing tests reference this table name for quick buttons, despite the misleading name
- **Why TestUser_QB prefix?**: Matches existing test naming pattern in CleanupTestData()
- **Why dynamic column inspection?**: Table structure may vary between test/production databases
- **Why separate cleanup methods?**: Allows targeted cleanup and proper dependency ordering
- **Why SetupTestDataAsync() instead of TestInitialize?**: Base class TestInitialize is not virtual, so tests use explicit setup calls

**Implementation Notes**:

All test methods now follow this pattern:
```csharp
[TestMethod]
public async Task SomeTest_Scenario_ExpectedResult()
{
    // Setup: Ensure table exists and create test data
    await EnsureQuickButtonTableAsync();
    await SetupTestDataAsync();

    // Arrange
    // ... test-specific setup ...

    // Act
    var result = await Dao_QuickButtons.SomeMethod(...);

    // Assert
    AssertSuccess(result, "Expected ...");
}
```

---

## Phase C: Test Execution & Runtime Validation

**Status**: Ready to execute  
**Next Action**: Run tests and capture results

### Test Execution Checklist

- [ ] Run all 10 tests together: `dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"`
- [ ] Document pass/fail for each test
- [ ] Capture error messages for failures
- [ ] Identify patterns in failures (missing SPs, wrong parameters, etc.)
- [ ] Create fix plan for each failing test
- [ ] Implement fixes
- [ ] Re-run until all tests pass

---

**Category Maintainer**: Development Team  
**Reference**: .github/instructions/integration-testing.instructions.md
