# Fix All Test Failures - Systematic Resolution Workflow

**Auto-Delete**: This file should be deleted once all tasks in `test-failure-fixes.md` are complete and tests pass.

---

## Mission

⚠️ **PREREQUISITE**: Before fixing individual test failures, complete the DAO Transaction Support refactoring documented in `specs/002-003-database-layer-complete/dao-transaction-refactor.md`. This refactoring is required to fix 43 of the 43 remaining test failures.

**After DAO refactoring is complete**, work through any remaining test failures documented in `specs/002-003-database-layer-complete/test-failure-fixes.md` systematically.

---

## Current Blocker: DAO Transaction Architecture

**Status**: 🔴 **CRITICAL BLOCKER** - 43 tests failing due to architectural issue

**Problem**: Tests use transaction isolation but DAOs create new connections, causing "Parameter 'p_Status' not found in collection" errors.

**Solution**: Follow `specs/002-003-database-layer-complete/dao-transaction-refactor.md` checklist to:
1. Refactor Helper_Database_StoredProcedure to accept optional connection/transaction
2. Update DAO methods to accept optional connection/transaction
3. Update tests to pass test connection/transaction to DAOs

**Once complete**, return to this workflow to fix any remaining issues.

---

## Workflow Instructions

### 1. Read Current State
- Read `specs/002-003-database-layer-complete/test-failure-fixes.md` to understand all pending issues
- Identify the highest priority category that's not yet complete
- Focus on one category at a time to maintain clarity

### 2. Fix One Category at a Time

For each category in priority order:

**Priority Order:**
1. 🔴 **CRITICAL** - Stored Procedure Parameter Mismatches (15 tests)
2. 🔴 **CRITICAL** - Transaction Management Failures (3 tests)
3. 🔴 **HIGH** - Master Data DAO Connection Failures (6 tests)
4. 🔴 **HIGH** - Quick Buttons DAO Failures (12 tests)
5. 🔴 **HIGH** - Error Log DAO Failures (7 tests)
6. 🔴 **HIGH** - Dao_Transactions Failures (7 tests)
7. 🟡 **MEDIUM** - Dao_System Failures (6 tests)
8. 🟡 **MEDIUM** - Transaction History Logging (3 tests)
9. 🟢 **LOW** - Error Cooldown Test Failures (6 tests)
10. 🟡 **INFORMATIONAL** - Query Procedure Compliance (2 tests)
11. 🟡 **INFORMATIONAL** - Parameter Naming Conventions (2 tests)

### 3. Resolution Steps for Each Issue

**For EVERY issue in the current category:**

#### Step A: Analyze the Problem
```markdown
1. Read the test file referenced in the error
2. Read the DAO file being tested
3. Read the stored procedure SQL file (if applicable)
4. Identify the exact mismatch/problem
5. Document your findings
```

#### Step B: Implement the Fix
```markdown
Choose the appropriate fix strategy:

**For Parameter Mismatches:**
- Read the stored procedure to see actual parameter names
- Update DAO code to match procedure signature
- OR update stored procedure to match DAO expectations
- OR add parameter override to Dao_ParameterPrefixOverrides.cs

**For Missing Procedures:**
- Search CurrentStoredProcedures for existing version
- Copy/update to UpdatedStoredProcedures
- Deploy using Deploy-StoredProcedures.ps1
- Verify deployment succeeded

**For Connection Failures:**
- Test manual execution of procedure in MySQL
- Check DELIMITER syntax
- Verify procedure compiles without errors
- Check for typos in procedure name

**For Logic Errors:**
- Review test expectations vs procedure behavior
- Fix procedure logic or test assertions
- Document why the change was needed
```

#### Step C: Verify the Fix
```powershell
# Run ONLY the tests for the issue you just fixed
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Tests

# Example: Run specific test class
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests" --logger "console;verbosity=normal"

# Example: Run specific test method
dotnet test --filter "FullyQualifiedName~TransferPartSimpleAsync_ValidData" --logger "console;verbosity=normal"
```

#### Step D: Update Progress
```markdown
1. Update test-failure-fixes.md:
   - Change status from 🔴 to ✅ for fixed issue
   - Update progress tracking section
   - Document what was changed

2. If ALL tests in category now pass:
   - Mark entire category as ✅ Complete
   - Update category status table
   - Move to next priority category
```

### 4. Full Test Run After Each Category

After completing an entire category:
```powershell
# Run full test suite
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Tests
dotnet test MTM_Inventory_Application.Tests.csproj --logger "console;verbosity=normal" --nologo

# Document new pass/fail counts in test-failure-fixes.md
```

---

## Detailed Fix Strategies by Category

### Category 1: Stored Procedure Parameter Mismatches (PRIORITY 1)

**Issues to Fix:**
- T-SP-001: inv_inventory_transfer_quantity - Missing `p_BatchNumber` (3 tests)
- T-SP-002: inv_inventory_Transfer_Part - Missing `p_BatchNumber` (2 tests)
- T-SP-003: inv_transactions_Search - Missing `p_UserName` (4 tests)
- T-SP-004: inv_transactions_SmartSearch - Missing `p_WhereClause` (1 test)
- T-SP-005: inv_transactions_GetAnalytics - Missing `p_UserName` (2 tests)

**Action Plan:**
```markdown
1. Read each stored procedure file in Database/UpdatedStoredProcedures/ReadyForVerification/
2. Compare procedure signature with DAO method calls
3. For EACH mismatch:
   - Open the SQL file
   - Add the missing parameter to the CREATE PROCEDURE signature
   - Handle the parameter in procedure logic
   - Save file
4. Redeploy ALL modified procedures:
   ```powershell
   cd Database
   .\Deploy-StoredProcedures.ps1 -Database "mtm_wip_application_winforms_test" -Force
   ```
5. Run affected tests
6. Mark issues as ✅ Complete if tests pass
```

### Category 2: Transaction Management (PRIORITY 2)

**Issues to Fix:**
- T-TM-001, T-TM-002, T-TM-003: Same as Category 1 (inv_inventory_transfer_quantity parameter)

**Action Plan:**
```markdown
These will be automatically fixed when Category 1 is complete.
Verify rollback behavior works correctly after parameter fixes.
```

### Category 3: Master Data Connection Failures (PRIORITY 3)

**Issues to Fix:**
- T-MD-001: md_item_types_Exists_ByType (2 tests)
- T-MD-002: md_locations_Exists_ByLocation (2 tests)
- T-MD-003: md_operation_numbers_Exists_ByOperation (2 tests)

**Action Plan:**
```markdown
1. Test manual execution in MySQL:
   ```sql
   USE mtm_wip_application_winforms_test;
   CALL md_item_types_Exists_ByType('Dunnage', @status, @error);
   SELECT @status, @error;
   ```
2. If procedure doesn't exist or has syntax errors:
   - Read procedure file
   - Check DELIMITER statements
   - Verify CREATE PROCEDURE syntax
   - Fix syntax errors
   - Redeploy
3. If procedure exists but connection fails:
   - Check procedure name spelling
   - Verify DAO is calling correct name
   - Check parameter prefix handling
4. Run tests to verify
```

### Category 4: Quick Buttons DAO (PRIORITY 4)

**Issues to Fix:**
- T-QB-001 through T-QB-012 (12 tests)

**Action Plan:**
```markdown
1. Check if procedures exist and are deployed:
   - sys_last_10_transactions_*
2. Read Dao_QuickButtons.cs to see which procedures are called
3. For each failing test:
   - Identify procedure being called
   - Verify procedure exists in deployed database
   - Check parameter matches
   - Fix mismatches
4. Redeploy if needed
5. Run QuickButtons tests:
   ```powershell
   dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"
   ```
```

### Category 5: Error Log DAO (PRIORITY 5)

**Issues to Fix:**
- T-EL-001 through T-EL-007 (7 tests)

**Action Plan:**
```markdown
1. Check log_error_* procedures exist and are deployed
2. Read Dao_ErrorLog.cs implementation
3. Check for parameter mismatches
4. Verify procedure logic matches test expectations
5. Fix any issues found
6. Run ErrorLog tests:
   ```powershell
   dotnet test --filter "FullyQualifiedName~Dao_ErrorLog_Tests"
   ```
```

### Category 6: Dao_Transactions (PRIORITY 6)

**Issues to Fix:**
- Already covered in Category 1 (parameter mismatches)

### Category 7: Dao_System (PRIORITY 7)

**Issues to Fix:**
- T-SYS-001 through T-SYS-006 (6 tests)

**Action Plan:**
```markdown
1. Check sys_* procedures (user management, roles, access)
2. Verify role data exists in test database:
   ```sql
   SELECT * FROM sys_roles;
   ```
3. If missing test data, seed required roles
4. Check parameter handling in procedures
5. Run System tests:
   ```powershell
   dotnet test --filter "FullyQualifiedName~Dao_System_Tests"
   ```
```

### Category 8: Transaction History Logging (PRIORITY 8)

**Issues to Fix:**
- T-TL-001 (3 tests)

**Action Plan:**
```markdown
1. Find which procedure handles transaction history
2. Search for procedure in codebase:
   ```powershell
   grep -r "AddTransactionHistory" Data/
   ```
3. Verify procedure exists
4. Check parameter matches
5. Fix any issues
6. Run Logging tests:
   ```powershell
   dotnet test --filter "FullyQualifiedName~Dao_Logging_Tests"
   ```
```

### Category 9: Error Cooldown (PRIORITY 9)

**Issues to Fix:**
- T-EC-001 through T-EC-006 (6 tests)

**Action Plan:**
```markdown
1. Read ErrorCooldown_Tests.cs to understand what's expected
2. Check Service_ErrorHandler initialization
3. These may be test initialization issues, not procedure issues
4. May need to skip or mark as expected if they're testing UI-dependent features
5. Run ErrorCooldown tests:
   ```powershell
   dotnet test --filter "FullyQualifiedName~ErrorCooldown_Tests"
   ```
```

### Category 10-11: Informational Issues

**These are expected and document inconsistencies rather than failures:**
- Query procedures (expected to have different patterns)
- Parameter naming (documentation for future standardization)

**Action**: Document findings, mark as informational, move to completion.

---

## Verification Commands

### Run Specific Test Classes
```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Tests

# Inventory tests
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests"

# Transaction tests
dotnet test --filter "FullyQualifiedName~Dao_Transactions_Tests"

# Master Data tests
dotnet test --filter "FullyQualifiedName~Dao_MasterData_Tests"

# Quick Button tests
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"

# Error Log tests
dotnet test --filter "FullyQualifiedName~Dao_ErrorLog_Tests"

# System tests
dotnet test --filter "FullyQualifiedName~Dao_System_Tests"

# Transaction Management tests
dotnet test --filter "FullyQualifiedName~TransactionManagement_Tests"

# All tests
dotnet test MTM_Inventory_Application.Tests.csproj --logger "console;verbosity=normal" --nologo
```

### Deploy Stored Procedures
```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Database

# Dry run to see what would be deployed
.\Deploy-StoredProcedures.ps1 -Database "mtm_wip_application_winforms_test" -DryRun

# Actually deploy
.\Deploy-StoredProcedures.ps1 -Database "mtm_wip_application_winforms_test" -Force
```

### Check Procedure Deployment Status
```sql
-- Connect to test database
USE mtm_wip_application_winforms_test;

-- List all stored procedures
SHOW PROCEDURE STATUS WHERE Db = 'mtm_wip_application_winforms_test';

-- Check specific procedure
SHOW CREATE PROCEDURE inv_inventory_transfer_quantity;

-- Test procedure execution
CALL inv_inventory_transfer_quantity('BATCH-001', 'PART-001', '100', 'FLOOR', 5, @status, @msg);
SELECT @status, @msg;
```

---

## Success Criteria

**Complete when:**
- [ ] All 11 categories processed
- [ ] Test pass rate ≥ 95% (129+ passing out of 136)
- [ ] All CRITICAL and HIGH priority issues resolved
- [ ] test-failure-fixes.md updated with final results
- [ ] All changes committed to git
- [ ] This prompt file deleted (task complete)

**Final Validation:**
```powershell
# Run full suite one final time
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Tests
dotnet test MTM_Inventory_Application.Tests.csproj --logger "console;verbosity=normal" --nologo

# Should show: Total: 136, Passed: 129+, Failed: <7
```

---

## Communication Protocol

**While working:**
- Remain silent during fix implementation
- Only communicate when:
  - Starting a new category
  - Asking clarifying questions about test expectations
  - Reporting test results after each fix
  - Summarizing progress at category completion

**Status Updates:**
```markdown
✅ Category X Complete - [N] tests fixed
🔧 Working on Category X - Issue T-XX-NNN
❌ Category X - Issue T-XX-NNN requires clarification
```

---

## Start Command

When ready to begin, say: **"Start fixing test failures"**

The agent will:
1. Read test-failure-fixes.md
2. Start with Category 1 (Stored Procedure Parameter Mismatches)
3. Work through each issue systematically
4. Run tests after each fix
5. Update progress tracking
6. Continue until all categories complete
7. Delete this file when done

---

**DO NOT SKIP STEPS. DO NOT BATCH FIXES. FIX AND TEST ONE ISSUE AT A TIME.**
