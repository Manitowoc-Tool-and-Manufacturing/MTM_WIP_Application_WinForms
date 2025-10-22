# Category 4: Phase 1 New Failures

**Priority**: 🟠 INVESTIGATION NEEDED  
**Status**: 🟠 Investigation Required  
**Tests**: 0/2 passing (0%)  
**Estimated Effort**: 1-2 hours investigation + fix time TBD

---

## Root Cause Analysis

**Problem**: These 2 tests were passing before Phase 1 sys_ui_tables_name fix and started failing after that change.

**Investigation Findings** (via MCP tools):
- `analyze_stored_procedures` - Phase 1 fixed table name reference in 9 tests
- `compare_databases` - No schema changes detected beyond table name fix
- `validate_schema` - Test database schema matches snapshot
- Root cause for new failures: **UNKNOWN** - requires investigation

**Why Tests Are Failing**:
- **Unknown**: Tests passed before, fail after Phase 1 fix
- **Hypothesis 1**: Phase 1 fix revealed pre-existing data issues
- **Hypothesis 2**: Phase 1 fix changed query behavior unexpectedly
- **Hypothesis 3**: Tests have hidden dependencies on data modified by Phase 1 fixes
- **Hypothesis 4**: Timing/ordering issue now exposed by Phase 1 changes

**Root Cause**: **REQUIRES INVESTIGATION** - must determine why these specific tests regressed.

---

## Investigation Strategy

### Phase 1: Understand What Changed

1. **Review Phase 1 Changes**
   - Read Phase 1 session log: `history/sessions/2025-10-19-part1-phase1-completion.md`
   - Identify exact changes made to test files
   - Check if Dao_Inventory or Dao_Transactions were modified

2. **Compare Test Behavior**
   - **GetInventoryByLocation_ValidLocation_ReturnsInventory**:
     - What did it test before Phase 1?
     - What does it test after Phase 1?
     - Did table name fix affect inventory queries?
   
   - **GetTransactionHistory_DateRange_ReturnsTransactions**:
     - What did it test before Phase 1?
     - What does it test after Phase 1?
     - Did table name fix affect transaction history queries?

3. **Check for Hidden Dependencies**
   - Do these tests depend on data created/modified by Phase 1 tests?
   - Do these tests depend on table name reference that was fixed?
   - Do these tests have timing dependencies (run order matters)?

### Phase 2: Reproduce and Debug

1. **Run Tests in Isolation**
   ```powershell
   # Run just the inventory test
   dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests.GetInventoryByLocation_ValidLocation_ReturnsInventory"
   
   # Run just the transactions test
   dotnet test --filter "FullyQualifiedName~Dao_Transactions_Tests.GetTransactionHistory_DateRange_ReturnsTransactions"
   ```

2. **Run with Debugger**
   - Step through test execution
   - Inspect actual vs expected values
   - Check database state before/after test

3. **Check Database State**
   ```sql
   -- Check inventory table for location
   SELECT * FROM inv_current_inventory WHERE Location = 'FLOOR';
   
   -- Check transaction history table
   SELECT * FROM sys_transaction_history WHERE TransactionDate BETWEEN '2025-01-01' AND '2025-12-31';
   ```

### Phase 3: Determine Fix Approach

Based on findings, choose approach:

**Scenario A: Test Assumptions Wrong**
- Update test to match correct behavior
- Document why assumptions changed

**Scenario B: Data Setup Needed**
- Add test data setup (like Categories 1 & 2)
- Create inventory or transaction records for testing

**Scenario C: Phase 1 Fix Had Side Effects**
- Review if Phase 1 fix was correct
- May need to adjust Phase 1 changes
- Document unexpected interaction

**Scenario D: Test Ordering Issue**
- Make tests independent (proper setup/teardown)
- Remove implicit dependencies

---

## Test List

### Tests to Investigate (0/2 complete)

- [ ] **GetInventoryByLocation_ValidLocation_ReturnsInventory** | `Tests/Integration/Dao_Inventory_Tests.cs:89`
  - **Error**: TBD - need to run test to see actual error
  - **Status Before Phase 1**: ✅ Passing
  - **Status After Phase 1**: ❌ Failing
  - **Fix approach**: TBD after investigation
  - **Verification**: TBD
  - **Investigation Questions**:
    - Does test query inv_current_inventory table?
    - Does test depend on specific inventory records existing?
    - Did Phase 1 table name fix affect this query?
    - Does test need test data setup like Category 1?

- [ ] **GetTransactionHistory_DateRange_ReturnsTransactions** | `Tests/Integration/Dao_Transactions_Tests.cs:145`
  - **Error**: TBD - need to run test to see actual error
  - **Status Before Phase 1**: ✅ Passing
  - **Status After Phase 1**: ❌ Failing
  - **Fix approach**: TBD after investigation
  - **Verification**: TBD
  - **Investigation Questions**:
    - Does test query sys_transaction_history table?
    - Does test depend on specific transaction records existing?
    - Did Phase 1 table name fix affect this query?
    - Does test need test data setup?
    - Is date range used in test valid?

---

## Investigation Workflow

### Step-by-Step Investigation

1. **Run Both Tests and Capture Errors**
   ```powershell
   dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests.GetInventoryByLocation_ValidLocation_ReturnsInventory" --logger "console;verbosity=detailed" > inventory-test-output.txt
   
   dotnet test --filter "FullyQualifiedName~Dao_Transactions_Tests.GetTransactionHistory_DateRange_ReturnsTransactions" --logger "console;verbosity=detailed" > transactions-test-output.txt
   ```

2. **Read Test Code**
   - Open `Tests/Integration/Dao_Inventory_Tests.cs` line ~89
   - Open `Tests/Integration/Dao_Transactions_Tests.cs` line ~145
   - Understand what each test is trying to do

3. **Read DAO Implementation**
   - Open `Data/Dao_Inventory.cs` - find GetInventoryByLocation method
   - Open `Data/Dao_Transactions.cs` - find GetTransactionHistory method
   - Understand what stored procedures they call

4. **Check Stored Procedures**
   - Find inventory SP in `Database/UpdatedStoredProcedures/`
   - Find transaction history SP in `Database/UpdatedStoredProcedures/`
   - Look for table name references that Phase 1 might have affected

5. **Check Test Data**
   ```sql
   -- Check if inventory exists for test
   SELECT COUNT(*) FROM inv_current_inventory WHERE Location = 'FLOOR';
   
   -- Check if transactions exist for test
   SELECT COUNT(*) FROM sys_transaction_history WHERE TransactionDate >= '2025-01-01';
   ```

6. **Form Hypothesis**
   - Based on errors and investigation, form hypothesis about root cause
   - Test hypothesis with targeted fixes or data changes

7. **Implement Fix**
   - Apply minimal fix to resolve issue
   - Document rationale in this file

8. **Verify Fix**
   - Run both tests to confirm passing
   - Run ALL tests to ensure no regressions
   - Run Phase 1 tests again to ensure they still pass

---

## Relevant MCP Tools

### For Investigation

- **analyze_stored_procedures** - Check if SPs changed during Phase 1
  ```
  analyze_stored_procedures(
    procedures_dir: "Database/UpdatedStoredProcedures/ReadyForVerification/"
  )
  ```
  *Use when*: Understanding if stored procedures are correct

- **validate_schema** - Verify database schema is correct
  ```
  validate_schema(
    config_file: ".mcp/samples/schema-validation-config.json"
  )
  ```
  *Use when*: Confirming no schema drift introduced by Phase 1

- **analyze_dependencies** - Map stored procedure call chains
  ```
  analyze_dependencies(
    procedures_dir: "Database/UpdatedStoredProcedures/"
  )
  ```
  *Use when*: Understanding if SPs have unexpected dependencies

- **validate_dao_patterns** - Check if DAOs follow patterns
  ```
  validate_dao_patterns(
    dao_dir: "Data/",
    recursive: true
  )
  ```
  *Use when*: Verifying DAO code is correct after Phase 1 changes

---

## Commands

### PowerShell Investigation Commands

```powershell
# Run both failing tests together
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests.GetInventoryByLocation_ValidLocation_ReturnsInventory|FullyQualifiedName~Dao_Transactions_Tests.GetTransactionHistory_DateRange_ReturnsTransactions"

# Run with detailed output
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests.GetInventoryByLocation" --logger "console;verbosity=detailed"

# Run Phase 1 tests again to confirm they still pass
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests.GetAllInventory"
dotnet test --filter "FullyQualifiedName~Dao_ItemType_Tests.GetAllItemTypes"

# Run all inventory tests to see if others fail
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests"

# Run all transaction tests to see if others fail
dotnet test --filter "FullyQualifiedName~Dao_Transactions_Tests"
```

### SQL Investigation Queries

```sql
-- Connect to test database
USE mtm_wip_application_winforms_test;

-- Check inventory records by location
SELECT * FROM inv_current_inventory WHERE Location = 'FLOOR' LIMIT 10;
SELECT * FROM inv_current_inventory WHERE Location = 'RECEIVING' LIMIT 10;
SELECT * FROM inv_current_inventory WHERE Location = 'SHIPPING' LIMIT 10;

-- Check transaction history records
SELECT * FROM sys_transaction_history 
WHERE TransactionDate >= '2025-01-01' 
ORDER BY TransactionDate DESC 
LIMIT 10;

-- Check if Phase 1 table name is referenced
SELECT * FROM sys_ui_tables 
WHERE TableName LIKE '%inventory%' OR TableName LIKE '%transaction%';

-- Check table structures
DESCRIBE inv_current_inventory;
DESCRIBE sys_transaction_history;
```

---

## Code Locations

### DAO Files (May Need Investigation)
- `Data/Dao_Inventory.cs` - Inventory data access
- `Data/Dao_Transactions.cs` - Transaction data access

### Test Files (Need to Review)
- `Tests/Integration/Dao_Inventory_Tests.cs` - Line ~89
- `Tests/Integration/Dao_Transactions_Tests.cs` - Line ~145

### Stored Procedures (Check for Phase 1 Changes)
- `Database/UpdatedStoredProcedures/ReadyForVerification/sp_Inventory_GetByLocation.sql` (if exists)
- `Database/UpdatedStoredProcedures/ReadyForVerification/sp_Transactions_GetHistory.sql` (if exists)

### Phase 1 Session Log
- `history/sessions/2025-10-19-part1-phase1-completion.md` - Review what changed

### Database Tables
- `inv_current_inventory` - Inventory records
- `sys_transaction_history` - Transaction records
- `sys_ui_tables` - Table name references (Phase 1 fixed this)

---

## Notes & Gotchas

### Investigation Pitfalls to Avoid

1. **Assuming Phase 1 Was Wrong**: Phase 1 fix was correct, these failures may reveal pre-existing issues
2. **Over-Complicating**: Start simple - run tests, read errors, form hypothesis
3. **Ignoring Logs**: Phase 1 session log has detailed context
4. **Not Checking Data**: May just need test data like Categories 1 & 2

### Possible Root Causes (Ranked by Likelihood)

1. **Missing Test Data** (HIGH): Tests may need inventory/transaction records created
2. **Table Name Side Effect** (MEDIUM): Phase 1 fix may have changed query results
3. **Test Order Dependency** (MEDIUM): Tests may depend on Phase 1 tests running first
4. **Date Range Issue** (LOW): Transaction history date range may be invalid
5. **Phase 1 Fix Incorrect** (VERY LOW): Phase 1 fix was thorough and validated

### Dependencies

- **Depends on**: Phase 1 completion (already done)
- **Blocks**: None - low priority, can be fixed after Categories 1-3

### Test Isolation Considerations

- Once investigated, ensure tests are independent (no reliance on Phase 1 test execution order)
- If test data needed, add proper setup like Categories 1 & 2
- Document any discovered dependencies in this file

---

## Investigation Results

*(To be filled in after investigation)*

### GetInventoryByLocation_ValidLocation_ReturnsInventory

**Investigation Date**: TBD  
**Actual Error Message**: TBD  
**Root Cause Found**: TBD  
**Fix Applied**: TBD  
**Verification Results**: TBD

---

### GetTransactionHistory_DateRange_ReturnsTransactions

**Investigation Date**: TBD  
**Actual Error Message**: TBD  
**Root Cause Found**: TBD  
**Fix Applied**: TBD  
**Verification Results**: TBD

---

## Progress Tracking

**Last Updated**: 2025-10-19  
**Current Status**: Investigation not started  
**Next Step**: Run both tests to capture actual error messages

**Completion**: 0/2 (0%)  
**Progress Bar**: [░░░░░░░░░░] 0%

---

**Category Maintainer**: Development Team  
**Reference**: .github/instructions/integration-testing.instructions.md  
**Phase 1 Log**: history/sessions/2025-10-19-part1-phase1-completion.md
