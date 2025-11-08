# Tasks: Comprehensive Database Layer Refactor

**Input**: Design documents from `/specs/002-comprehensive-database-layer/`  
**Prerequisites**: plan.md ✅, spec.md ✅, research.md ✅, data-model.md ✅, contracts/ ✅, quickstart.md ✅

**Tests**: Integration tests included per FR-018 requirement for per-test-class transaction validation

**Organization**: Tasks grouped by user story to enable independent implementation and testing. Each task contains only ONE file with subtasks for individual methods.

## Format: `[ID] [P?] [Story] Description`

-   **[P]**: Can run in parallel (different files, no dependencies)
-   **[Story]**: Which user story this task belongs to (US1-US5 from spec.md)
-   **Method-Level Subtasks**: Each method refactoring tracked individually (T###a, T###b, etc.)
-   Exact file paths included in descriptions

## Path Conventions

-   Root: `C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\`
-   Models: `Models/`
-   Helpers: `Helpers/`
-   Data (DAOs): `Data/`
-   Forms: `Forms/`
-   Controls: `Controls/`
-   Services: `Services/`
-   Tests: `Tests/Integration/`

---

## Phase 1: Setup (Shared Infrastructure) ✅ COMPLETE

**Purpose**: Initialize INFORMATION_SCHEMA parameter cache and Model_Dao_Result foundation

-   [x] T001 [P] [Setup] Create `Models/Model_Dao_Result.cs` with base Model_Dao_Result class (IsSuccess, Message, Exception properties, Success/Failure factory methods)
-   [x] T002 [P] [Setup] Create `Models/Model_Dao_Result_Generic.cs` with Model_Dao_Result<T> generic class extending Model_Dao_Result with Data property
-   [x] T003 [Setup] Create `Models/Model_ParameterPrefix_Cache.cs` with dictionary structure for INFORMATION_SCHEMA caching and GetParameterPrefix lookup method
-   [x] T004 [Setup] Update `Program.cs` to query INFORMATION_SCHEMA.PARAMETERS at startup, populate ParameterPrefixCache dictionary, log initialization timing (~100-200ms expected)
-   [x] T004a [Setup] **Created Missing Stored Procedures** (2025-10-14): Created `sys_user_GetByName.sql`, `sys_user_GetIdByName.sql`, `sys_theme_GetAll.sql`, `sys_user_access_SetType.sql`, `sys_role_GetIdByName.sql` in `Database/UpdatedStoredProcedures/`. Imported into test database. Tests improved from 41/66 (62%) to 50/66 (76%) passing.

**Checkpoint**: ✅ Foundation ready - Model_Dao_Result classes created, ParameterPrefixCache structure in place, Program.cs initialization complete

---

## Phase 2: Foundational (Blocking Prerequisites) ✅ COMPLETE

**Purpose**: Core Helper_Database_StoredProcedure refactor - MUST complete before ANY DAO refactoring

**⚠️ CRITICAL**: No DAO work can begin until Helper refactor is complete

-   [x] T005 [Foundational] Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus` to query ParameterPrefixCache, apply detected prefixes to parameters, return Model_Dao_Result (async only, remove useAsync parameter)
-   [x] T006 [Foundational] Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteDataTableWithStatus` to query ParameterPrefixCache, apply detected prefixes, return Model_Dao_Result<DataTable> (async only)
-   [x] T007 [Foundational] Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteScalarWithStatus` to query ParameterPrefixCache, apply detected prefixes, return Model_Dao_Result<T> (async only)
-   [x] T008 [Foundational] Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteWithCustomOutput` to query ParameterPrefixCache, apply detected prefixes, return Model_Dao_Result<Dictionary<string, object>> (async only)
-   [x] T009 [Foundational] Add retry logic to all 4 Helper execution methods for transient errors (1205 deadlock, 1213 lock timeout, 2006 server gone, 2013 lost connection) with 3 attempts and exponential backoff
-   [x] T010 [Foundational] Add performance monitoring to all 4 Helper execution methods with configurable thresholds (Query: 500ms, Modification: 1000ms, Batch: 5000ms, Report: 2000ms) and warning logs when exceeded
-   [x] T011 [Foundational] Update `Helpers/Helper_Database_Variables.cs` to add TestDatabaseName constant = "mtm_wip_application_winform_test" per clarification Q8
-   [x] T012 [Foundational] Update `Logging/LoggingUtility.cs` to add recursive prevention check in LogDatabaseError method (catch exceptions, fallback to file logging if log_error table unavailable)
-   [x] T013 [Foundational] Create integration test infrastructure: `Tests/Integration/BaseIntegrationTest.cs` with [TestInitialize] BeginTransaction and [TestCleanup] Rollback using TestDatabaseName connection string

**Checkpoint**: ✅ Foundation ready - all Helper execution methods return Model_Dao_Result variants with parameter prefix detection, retry logic, and performance monitoring. Backward compatibility maintained via deprecated wrapper methods. Integration test infrastructure ready.

---

## Phase 2.5: Stored Procedure Standardization (Blocking Prerequisites) 🚨 CRITICAL

**Purpose**: Comprehensive audit and standardization of ALL stored procedures in the codebase to ensure consistent status code handling, parameter naming, and error reporting per `Database/CurrentStoredProcedures/00_STATUS_CODE_STANDARDS.md`

**⚠️ CRITICAL**: This phase BLOCKS all DAO refactoring work. All stored procedures must be standardized before proceeding with Phases 3-8.

**Scope**: ALL stored procedures across entire codebase (estimated 60-100+ procedures based on Database/CurrentStoredProcedures/ and Database/UpdatedStoredProcedures/)

**Database**: `MTM_WIP_Application_Winforms` (production) as source of truth

**Success Criteria**:

-   Zero compilation errors in C#
-   100% test pass rate (all procedures)
-   No runtime exceptions during execution
-   All procedures meet status code standards (OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500))
-   All procedures use p\_ prefix for parameters
-   Performance benchmarks met (queries < 30 seconds)

### Phase 2.5 - Part A: Discovery and Analysis (T100-T106)

#### T100 [P] [SP-Discovery] Comprehensive Stored Procedure Discovery

**Purpose**: Identify ALL stored procedure calls across the entire C# codebase

**Execution**:

1. Search ALL C# files recursively for stored procedure invocations:
    - `Helper_Database_StoredProcedure.Execute*` calls
    - Direct `MySqlCommand` calls (legacy patterns)
    - String literals containing stored procedure names
2. Extract stored procedure names and calling contexts
3. Generate comprehensive CSV report: `Database/STORED_PROCEDURE_CALLSITES.csv`

**Report Columns**:

-   StoredProcedureName
-   CallerFile (absolute path)
-   CallerMethod
-   LineNumber
-   CallPattern (Helper vs Direct)
-   ParameterCount
-   HasStatusChecking (bool)

**Deliverable**: CSV file with ALL stored procedure calls documented

**Estimated Procedures**: 60-100+ based on folder analysis

---

#### T101 [P] [SP-Discovery] Database Schema Extraction

**Purpose**: Extract complete stored procedure definitions from production database

**Execution**:

1. Connect to `MTM_WIP_Application_Winforms` database
2. Query INFORMATION_SCHEMA.ROUTINES for all stored procedures
3. Use `SHOW CREATE PROCEDURE` to extract full definitions
4. Generate individual SQL files in `Database/EXTRACTED_PROCEDURES/`

**SQL Query**:

```sql
SELECT ROUTINE_NAME
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_SCHEMA = 'MTM_WIP_Application_Winforms'
  AND ROUTINE_TYPE = 'PROCEDURE'
ORDER BY ROUTINE_NAME;
```

**Deliverable**:

-   Individual `.sql` files per procedure in `Database/EXTRACTED_PROCEDURES/`
-   Extraction log with procedure count and timestamp

---

#### T102 [SP-Discovery] Cross-Reference Analysis

**Purpose**: Match database procedures with C# callsites and identify gaps

**Execution**:

1. Load T100 CSV (C# callsites) and T101 extraction log (database procedures)
2. Generate three-way analysis:
    - **In Both**: Procedures called from C# AND exist in database ✅
    - **C# Only**: Procedures called but NOT in database ⚠️ (broken references)
    - **Database Only**: Procedures in database but NOT called ⚠️ (unused/orphaned)
3. Generate analysis report: `Database/CROSS_REFERENCE_ANALYSIS.md`

**Report Sections**:

-   Executive Summary (counts for each category)
-   Broken References (C# Only) - MUST FIX
-   Unused Procedures (Database Only) - Document for deprecation
-   Validated Procedures (In Both) - Ready for standardization

**Deliverable**: Markdown analysis report with action items

---

#### T103 [P] [SP-Discovery] Parameter Mapping Validation

**Purpose**: Validate C# parameter usage matches stored procedure signatures

**Execution**:

1. For each procedure in "In Both" category from T102:
    - Query INFORMATION_SCHEMA.PARAMETERS for procedure parameters
    - Extract C# parameter dictionaries from callsite code (T100 CSV)
    - Compare parameter names (accounting for p\_ prefix removal in C#)
    - Check for missing/extra parameters in C# calls
2. Generate validation report: `Database/PARAMETER_VALIDATION_REPORT.csv`

**Report Columns**:

-   StoredProcedureName
-   ExpectedParameters (from INFORMATION_SCHEMA)
-   ActualParameters (from C# callsite)
-   MismatchType (Missing/Extra/NameDifference/Match)
-   CallerFile
-   LineNumber
-   Severity (Critical/Warning/Info)

**Deliverable**: CSV report with parameter mismatches flagged

---

#### T104 [P] [SP-Discovery] Status Code Standards Compliance Audit

**Purpose**: Audit all procedures for compliance with `00_STATUS_CODE_STANDARDS.md`

**Execution**:

1. For each extracted procedure (from T101):
    - Parse SQL to check for `OUT p_Status INT` parameter
    - Parse SQL to check for `OUT p_ErrorMsg VARCHAR(500)` parameter
    - Check for proper status code usage (1, 0, -1 to -5)
    - Check for `DECLARE EXIT HANDLER FOR SQLEXCEPTION`
    - Check for `START TRANSACTION` / `COMMIT` / `ROLLBACK` patterns
    - Validate FOUND_ROWS() or ROW_COUNT() usage
2. Generate compliance report: `Database/STATUS_CODE_COMPLIANCE_REPORT.csv`

**Report Columns**:

-   StoredProcedureName
-   HasStatusParameter (bool)
-   HasErrorMsgParameter (bool)
-   HasExitHandler (bool)
-   HasTransactionControl (bool - for INSERT/UPDATE/DELETE)
-   UsesFoundRows (bool - for SELECT)
-   UsesRowCount (bool - for DML)
-   ComplianceScore (0-100%)
-   RequiresRefactoring (bool)

**Deliverable**: CSV compliance report with refactoring priorities

---

#### T105 [SP-Discovery] Consolidate Discovery Findings

**Purpose**: Synthesize all discovery data into executive summary and action plan

**Execution**:

1. Aggregate data from T100-T104 reports
2. Calculate statistics:
    - Total procedures discovered
    - Broken references count (immediate blockers)
    - Non-compliant procedures count
    - Average compliance score
    - Estimated refactoring effort (hours)
3. Generate prioritized action plan with three tiers:
    - **Tier 1 - Critical**: Broken references, zero compliance (MUST FIX)
    - **Tier 2 - High**: <50% compliance, high usage frequency
    - **Tier 3 - Medium**: 50-99% compliance, low usage frequency
4. Create executive summary: `Database/SP_DISCOVERY_EXECUTIVE_SUMMARY.md`

**Summary Sections**:

-   Discovery Statistics
-   Compliance Overview (with charts/graphs if possible)
-   Critical Issues (Tier 1 broken references)
-   Refactoring Priorities (Tier 1-3 breakdown)
-   Estimated Timeline
-   Resource Requirements

**Deliverable**: Executive summary with actionable refactoring plan

---

#### T106 [SP-Discovery] Generate Integration Test Scaffolds

**Purpose**: Auto-generate test class scaffolds for ALL discovered procedures

**Execution**:

1. For each procedure in T102 "In Both" category:
    - Parse procedure signature from INFORMATION_SCHEMA.PARAMETERS
    - Generate test class scaffold using naming convention: `{ProcedureName}_Tests.cs`
    - Include test methods:
        - `Test_{ProcedureName}_Success_ReturnsStatus1`
        - `Test_{ProcedureName}_NoData_ReturnsStatus0`
        - `Test_{ProcedureName}_InvalidInput_ReturnsStatusNegative2`
        - `Test_{ProcedureName}_DatabaseError_ReturnsStatusNegative1`
    - Use BaseIntegrationTest inheritance
    - Add TODO comments with parameter examples
2. Generate test scaffolds in `Tests/Integration/GeneratedTests/`
3. Generate test execution script: `Tests/run-all-sp-tests.ps1`

**Test Scaffold Template**:

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTM_WIP_Application_Winforms.Tests.Integration.GeneratedTests
{
    [TestClass]
    public class {ProcedureName}_Tests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task Test_{ProcedureName}_Success_ReturnsStatus1()
        {
            // TODO: Arrange - Set up valid test data
            var parameters = new Dictionary<string, object>
            {
                // TODO: Add parameters from INFORMATION_SCHEMA
            };

            // Act
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                _connectionString, "{ProcedureName}", parameters);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            // TODO: Add specific assertions
        }

        // TODO: Implement remaining test methods
    }
}
```

**Deliverable**:

-   Test scaffolds for ALL procedures in `Tests/Integration/GeneratedTests/`
-   PowerShell test runner script

---

**Checkpoint**: ✅ Discovery complete - comprehensive inventory of all stored procedures, compliance audit, parameter validation, test scaffolds generated

---

### Phase 2.5 - Part B: Test Implementation (T107-T112)

**Purpose**: Implement comprehensive integration tests for ALL stored procedures before refactoring

**Strategy**: Test-Driven Standardization - ensure tests FAIL for non-compliant procedures, then refactor until tests PASS

#### T107 [P] [SP-Tests] Implement Core System Procedure Tests

**Scope**: System management procedures (authentication, roles, themes, access control)

**Procedures** (estimated 10-15):

-   `sys_user_GetByName`
-   `sys_user_GetIdByName`
-   `sys_theme_GetAll`
-   `sys_user_access_SetType`
-   `sys_role_GetIdByName`
-   All other `sys_*` procedures from discovery

**Execution**:

1. Review generated test scaffolds from T106
2. Implement full test methods with:
    - Valid input test data (status 1 expected)
    - Empty result test data (status 0 expected)
    - Invalid input test data (status -2 expected)
    - Forced error scenarios (status -1 expected)
3. Run tests and document CURRENT state (expected to fail for non-compliant procedures)
4. Generate baseline test results: `Tests/BASELINE_SYSTEM_TESTS.csv`

**Deliverable**: Fully implemented test classes for all system procedures with baseline results

---

#### T108 [P] [SP-Tests] Implement Inventory Management Procedure Tests

**Scope**: Inventory CRUD and search procedures

**Procedures** (estimated 15-20):

-   `inv_inventory_Add_Item`
-   `inv_inventory_Remove_Item`
-   `inv_inventory_Transfer_Part`
-   `inv_inventory_Transfer_Quantity`
-   `inv_inventory_Get_ByPartID`
-   `inv_inventory_Get_ByPartIDandOperation`
-   `inv_inventory_Get_ByUser`
-   `inv_inventory_Fix_BatchNumbers`
-   All other `inv_*` procedures from discovery

**Execution**: Same as T107 but for inventory procedures

**Special Considerations**:

-   Test transaction rollback for transfer operations
-   Test batch number integrity
-   Test concurrent access scenarios

**Deliverable**: Fully implemented test classes for all inventory procedures with baseline results

---

#### T109 [P] [SP-Tests] Implement Transaction and History Procedure Tests

**Scope**: Transaction logging and history retrieval procedures

**Procedures** (estimated 10-15):

-   `inv_transaction_Add`
-   `inv_transactions_GetAnalytics`
-   `inv_transactions_SmartSearch`
-   All history procedures (`hist_*` or similar)

**Execution**: Same as T107 but for transaction/history procedures

**Special Considerations**:

-   Test date range filtering
-   Test analytics calculations
-   Test smart search accuracy

**Deliverable**: Fully implemented test classes for all transaction/history procedures with baseline results

---

#### T110 [P] [SP-Tests] Implement Master Data Procedure Tests

**Scope**: Part, Location, Operation, ItemType CRUD procedures

**Procedures** (estimated 20-30):

-   All `md_part_ids_*` procedures
-   All `md_locations_*` procedures
-   All `md_operation_numbers_*` procedures
-   All `md_item_types_*` procedures

**Execution**: Same as T107 but for master data procedures

**Special Considerations**:

-   Test referential integrity (cannot delete in-use master data)
-   Test duplicate prevention
-   Test case sensitivity handling

**Deliverable**: Fully implemented test classes for all master data procedures with baseline results

---

#### T111 [P] [SP-Tests] Implement Logging and Maintenance Procedure Tests

**Scope**: Error logging, quick buttons, maintenance procedures

**Procedures** (estimated 10-15):

-   All `log_error_*` procedures
-   All `sys_last_10_transactions_*` procedures (quick buttons)
-   `maint_InsertMissingUserUiSettings`
-   `maint_reload_part_ids_and_operation_numbers`
-   `sp_ReassignBatchNumbers`

**Execution**: Same as T107 but for logging/maintenance procedures

**Special Considerations**:

-   Test recursive error logging prevention
-   Test quick button position management
-   Test maintenance script idempotency

**Deliverable**: Fully implemented test classes for all logging/maintenance procedures with baseline results

---

#### T112 [SP-Tests] Consolidate Test Results and Generate Coverage Report

**Purpose**: Aggregate all test results and calculate baseline coverage metrics

**Execution**:

1. Run ALL implemented tests (T107-T111) in single batch
2. Generate test execution report with:
    - Total procedures tested
    - Pass/Fail breakdown by category
    - Compliance score distribution
    - Critical failures (broken references, zero compliance)
3. Generate coverage matrix: `Tests/SP_TEST_COVERAGE_MATRIX.csv`

**Coverage Matrix Columns**:

-   StoredProcedureName
-   Category (System/Inventory/Transaction/MasterData/Logging)
-   TestClassGenerated (bool)
-   TestsImplemented (count)
-   TestsPassing (count)
-   TestsFailing (count)
-   PassRate (%)
-   ReadyForRefactoring (bool - only if tests implemented and failing as expected)

**Deliverable**:

-   Comprehensive coverage report
-   Prioritized refactoring queue based on test failures

---

**Checkpoint**: ✅ Test infrastructure complete - ALL procedures have tests, baseline metrics established, ready for standardization refactoring

---

### Phase 2.5 - Part C: Stored Procedure Refactoring (T113-T118)

**Purpose**: Refactor ALL stored procedures to meet `00_STATUS_CODE_STANDARDS.md` requirements

**Strategy**: Tier-based refactoring starting with critical broken references, then high-usage non-compliant procedures

#### T113 [P] [SP-Refactor] Tier 1 Critical - Fix Broken References

**Scope**: Procedures called from C# but NOT in database (from T102 "C# Only" category)

**Execution**:

1. Review T102 broken references list
2. For each broken reference:
    - **Option A**: Create missing stored procedure from C# caller logic
    - **Option B**: Update C# caller to use existing alternative procedure
    - **Option C**: Remove dead code if functionality no longer needed
3. Implement choice following `00_STATUS_CODE_STANDARDS.md` template
4. Run associated tests from T107-T111 to verify
5. Update T100 CSV to mark broken reference as resolved

**Estimated Procedures**: 5-10 (based on typical codebase analysis)

**Deliverable**:

-   All broken references resolved
-   New/updated SQL files in `Database/CurrentStoredProcedures/`
-   Test results showing 100% pass for this tier

---

#### T114 [P] [SP-Refactor] Tier 1 Critical - Zero Compliance Procedures

**Scope**: Procedures with 0% compliance score from T104 (missing both OUT p_Status and OUT p_ErrorMsg)

**Execution**:

1. Review T104 compliance report for 0% score procedures
2. For each procedure:
    - Add `OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)` parameters
    - Add `DECLARE EXIT HANDLER FOR SQLEXCEPTION` block
    - Add proper status code logic (1, 0, -1 to -5)
    - Add transaction control for DML operations
    - Add FOUND_ROWS() or ROW_COUNT() checks
3. Follow template from `00_STATUS_CODE_STANDARDS.md`
4. Save refactored procedure to `Database/CurrentStoredProcedures/{procedureName}.sql`
5. Run associated tests to verify status codes working
6. Update T104 CSV to mark as compliant

**Estimated Procedures**: 10-20 (based on typical legacy codebase)

**Deliverable**:

-   All zero-compliance procedures refactored
-   Updated SQL files with full compliance
-   Test results showing proper status code handling

---

#### T115 [P] [SP-Refactor] Tier 2 High - Partial Compliance High-Usage Procedures

**Scope**: Procedures with 1-49% compliance AND called >10 times in codebase (from T100 + T104)

**Execution**:

1. Cross-reference T100 callsite count with T104 compliance scores
2. Identify procedures with high usage but low compliance
3. For each procedure:
    - Add missing compliance elements (status/error parameters, exit handlers, etc.)
    - Standardize parameter naming (p\_ prefix)
    - Add validation logic if missing
    - Refactor status code logic to match standards
4. Save refactored procedure to `Database/CurrentStoredProcedures/{procedureName}.sql`
5. Run tests and verify 100% pass rate
6. Update C# callers if parameter names changed (coordinate with T103 findings)

**Estimated Procedures**: 20-30

**Deliverable**:

-   High-usage procedures fully compliant
-   C# callers updated for any parameter name changes
-   Test results showing 100% pass

---

#### T116 [P] [SP-Refactor] Tier 2 High - Parameter Naming Standardization

**Scope**: Procedures with parameter mismatches from T103 (missing p\_ prefix, name inconsistencies)

**Execution**:

1. Review T103 parameter validation report
2. For each procedure with parameter naming issues:
    - Standardize ALL parameters to use `p_` prefix
    - Match C# PascalCase names (e.g., `p_PartNumber`, `p_LocationCode`)
    - Update procedure logic to use new parameter names
    - Add OUT parameters if missing (`OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)`)
3. Save refactored procedure to `Database/CurrentStoredProcedures/{procedureName}.sql`
4. Verify Model*ParameterPrefixCache will detect p* prefix correctly
5. Run tests to ensure Helper_Database_StoredProcedure applies prefixes correctly
6. Update T103 CSV to mark parameter naming as standardized

**Estimated Procedures**: 30-50 (most procedures will need this)

**Deliverable**:

-   ALL procedures use p\_ prefix consistently
-   Parameter naming matches C# expectations
-   No parameter mismatch warnings in T103 report

---

#### T117 [P] [SP-Refactor] Tier 3 Medium - Remaining Non-Compliant Procedures

**Scope**: Procedures with 50-99% compliance (minor issues, low usage, or unused procedures)

**Execution**:

1. Review T104 compliance report for 50-99% score procedures
2. For each procedure:
    - Add missing compliance elements to reach 100%
    - Refine error messages for clarity
    - Optimize query performance if slow (use EXPLAIN)
    - Add comments documenting business logic
3. Save refactored procedure to `Database/CurrentStoredProcedures/{procedureName}.sql`
4. Run tests and verify improvements
5. Update T104 CSV to mark as 100% compliant

**Estimated Procedures**: 20-40

**Deliverable**:

-   ALL procedures reach 100% compliance
-   Performance optimizations applied
-   Documentation comments added

---

#### T118 [SP-Refactor] Handle Unused/Orphaned Procedures

**Scope**: Procedures in database but NOT called from C# (from T102 "Database Only" category)

**Execution**:

1. Review T102 unused procedures list
2. For each unused procedure:
    - **Option A**: Confirm legitimately unused → document for deprecation (add to `Database/DEPRECATED_PROCEDURES.md`)
    - **Option B**: Discovery missed usage → add to refactoring queue (T113-T117)
    - **Option C**: Used by external tools/scripts → document in `Database/EXTERNAL_PROCEDURES.md` and standardize anyway
3. Refactor external/to-be-deprecated procedures to standards (for future use)
4. Generate deprecation plan with timeline for removal

**Estimated Procedures**: 10-20

**Deliverable**:

-   Documentation of unused procedures
-   Deprecation plan with timeline
-   Standardized procedures even if currently unused

---

**Checkpoint**: ✅ All procedures refactored and standardized - 100% compliance with `00_STATUS_CODE_STANDARDS.md`

---

### Phase 2.5 - Part D: Database Deployment (T119-T121)

**Purpose**: Deploy standardized procedures to database with comprehensive backup and rollback plan

**⚠️ CRITICAL**: User will create database backup BEFORE execution

#### T119 [SP-Deploy] Pre-Deployment Validation

**Purpose**: Final validation before database changes

**Execution**:

1. Run ALL tests (T107-T111) against CURRENT database state
2. Document current test pass rate as baseline
3. Verify backup exists (user responsibility - confirm before proceeding)
4. Generate deployment checklist: `Database/DEPLOYMENT_CHECKLIST.md`

**Deployment Checklist**:

-   [ ] All tests implemented (T107-T111 complete)
-   [ ] Baseline test results documented
-   [ ] Database backup created by user (MANUAL STEP)
-   [ ] Backup verified restorable (MANUAL STEP)
-   [ ] All SQL files generated (T113-T118 complete)
-   [ ] No broken references remain (T113 complete)
-   [ ] Connection string points to correct database (`MTM_WIP_Application_Winforms`)
-   [ ] Team notified of deployment window
-   [ ] Rollback plan reviewed and understood

**Deliverable**: Deployment checklist with all items verified

---

#### T120 [SP-Deploy] Automated Database Wipe and Install

**Purpose**: Systematically replace all stored procedures with standardized versions

**⚠️ WARNING**: This step DROPS all existing stored procedures. Ensure backup exists.

**Execution**:

1. **Generate Deployment Script** (`Database/DEPLOY_STANDARDIZED_PROCEDURES.sql`):

```sql
-- ===================================================================
-- AUTOMATED STORED PROCEDURE DEPLOYMENT
-- Generated: {timestamp}
-- Database: MTM_WIP_Application_Winforms
-- Total Procedures: {count}
-- ===================================================================

-- Step 1: Drop ALL existing stored procedures
DROP PROCEDURE IF EXISTS sys_user_GetByName;
DROP PROCEDURE IF EXISTS sys_user_GetIdByName;
-- ... (repeat for ALL discovered procedures from T101)

-- Step 2: Install standardized procedures
SOURCE Database/CurrentStoredProcedures/sys_user_GetByName.sql;
SOURCE Database/CurrentStoredProcedures/sys_user_GetIdByName.sql;
-- ... (repeat for ALL refactored procedures from T113-T118)

-- Step 3: Verify installation
SELECT COUNT(*) AS InstalledProcedures
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_SCHEMA = 'MTM_WIP_Application_Winforms'
  AND ROUTINE_TYPE = 'PROCEDURE';
```

2. **Execute Deployment**:

    - Run deployment script against `MTM_WIP_Application_Winforms`
    - Log execution output to `Database/DEPLOYMENT_LOG_{timestamp}.txt`
    - Capture any errors/warnings
    - Verify procedure count matches expected

3. **Post-Deployment Verification**:
    - Query INFORMATION_SCHEMA to confirm all procedures installed
    - Verify no orphaned procedures remain
    - Check for any installation errors

**Deliverable**:

-   Deployment script executed successfully
-   Deployment log with timestamp
-   Verification query showing expected procedure count

---

#### T121 [SP-Deploy] Post-Deployment Test Validation

**Purpose**: Verify all procedures working correctly after deployment

**Execution**:

1. Re-run ALL integration tests (T107-T111) against DEPLOYED database
2. Compare results to baseline from T119:
    - Calculate pass rate improvement
    - Identify any regressions (tests that passed before but fail now)
    - Document any unexpected failures
3. Generate post-deployment report: `Tests/POST_DEPLOYMENT_VALIDATION_REPORT.md`

**Report Sections**:

-   Deployment Summary (timestamp, procedure count, duration)
-   Test Results Comparison (before/after pass rates)
-   Regressions (if any) with root cause analysis
-   Compliance Achievement (% of procedures now meeting standards)
-   Performance Impact (query timing before/after)
-   Recommendations (any follow-up actions needed)

**Success Criteria** (from user requirements):

-   ✅ Zero compilation errors in C#
-   ✅ 100% test pass rate
-   ✅ No runtime exceptions
-   ✅ All procedures have p_Status and p_ErrorMsg
-   ✅ All parameters use p\_ prefix
-   ✅ All queries < 30 seconds

**Deliverable**:

-   Post-deployment validation report
-   Confirmation that ALL success criteria met
-   Rollback decision (proceed vs. rollback) with justification

---

**Checkpoint**: ✅ Database deployment complete - all procedures standardized, tests passing, ready for DAO refactoring

---

### Phase 2.5 - Part E: End-to-End Integration Testing (T122-T128)

**Purpose**: Validate complete caller → Helper → SP → Database flow for ALL procedures

**Strategy**: Test from actual C# callsites (Forms, Controls, Services) to ensure real-world workflows function correctly

#### T122 [P] [SP-Integration] System and Authentication Workflow Tests

**Scope**: Test authentication, role management, theme selection, access control from MainForm and SettingsForm

**Test Scenarios**:

1. **User Login Flow**:
    - MainForm startup → Dao_System.AuthenticateUserAsync → sys_user_Authenticate → database
    - Valid credentials → status 1, user data returned
    - Invalid credentials → status 0, no data
    - Database error → status -1, error message
2. **Role Assignment Flow**:
    - Control_Add_User → Dao_System.SetUserAccessTypeAsync → sys_user_access_SetType → database
    - Verify role assigned correctly
3. **Theme Loading Flow**:
    - Core_Themes.ApplyTheme → Dao_System.GetAllThemesAsync → sys_theme_GetAll → database
    - Verify themes loaded and applied

**Execution**:

1. Launch application (automated via test harness if possible)
2. Execute workflows with valid/invalid data
3. Monitor logs for errors
4. Capture screenshots/videos of successful flows
5. Document any issues in `Tests/INTEGRATION_ISSUES_SYSTEM.md`

**Deliverable**: System workflow validation report with pass/fail for each scenario

---

#### T123 [P] [SP-Integration] Inventory Management Workflow Tests

**Scope**: Test inventory add/remove/transfer from Control_InventoryTab, Control_RemoveTab, Control_TransferTab

**Test Scenarios**:

1. **Add Inventory Flow**:
    - Control_InventoryTab → Dao_Inventory.AddInventoryAsync → inv_inventory_Add_Item → database
    - Verify inventory added, DataGridView refreshed
2. **Remove Inventory Flow**:
    - Control_RemoveTab → Dao_Inventory.RemoveInventoryAsync → inv_inventory_Remove_Item → database
    - Verify inventory removed, transaction logged
3. **Transfer Inventory Flow**:
    - Control_TransferTab → Dao_Inventory.TransferInventoryAsync → inv_inventory_Transfer_Part → database
    - Verify transfer succeeds, source/destination updated
    - Force mid-transfer failure → verify rollback

**Execution**: Same as T122 but for inventory workflows

**Deliverable**: Inventory workflow validation report

---

#### T124 [P] [SP-Integration] Transaction Logging and History Workflow Tests

**Scope**: Test transaction logging and history retrieval

**Test Scenarios**:

1. **Transaction Logging Flow**:
    - Any inventory operation → Dao_Transactions.LogTransactionAsync → inv_transaction_Add → database
    - Verify transaction recorded with correct metadata
2. **History Retrieval Flow**:
    - Control_HistoryTab → Dao_History.GetInventoryHistoryAsync → inv_transactions_Get → database
    - Verify history displayed correctly
    - Test date range filtering
3. **Analytics Flow**:
    - Reporting feature → Dao_Transactions.GetAnalyticsAsync → inv_transactions_GetAnalytics → database
    - Verify analytics calculations accurate

**Execution**: Same as T122 but for transaction/history workflows

**Deliverable**: Transaction/history workflow validation report

---

#### T125 [P] [SP-Integration] Master Data Management Workflow Tests

**Scope**: Test part/location/operation/itemtype CRUD from SettingsForm controls

**Test Scenarios**:

1. **Add Master Data Flow**:
    - Control_Add_PartID → Dao_Part.CreatePartAsync → md_part_ids_Add_Part → database
    - Verify part added, dropdown refreshed
    - Test duplicate prevention
2. **Edit Master Data Flow**:
    - Control_Edit_Location → Dao_Location.UpdateLocationAsync → md_locations_Update_Location → database
    - Verify location updated, changes reflected
3. **Delete Master Data Flow**:
    - Control_Remove_Operation → Dao_Operation.DeleteOperationAsync → md_operation_numbers_Delete_ByOperation → database
    - Test referential integrity (cannot delete if in use)

**Execution**: Same as T122 but for master data workflows

**Deliverable**: Master data workflow validation report

---

#### T126 [P] [SP-Integration] Error Logging and Recovery Workflow Tests

**Scope**: Test error logging, recursive prevention, cooldown logic

**Test Scenarios**:

1. **Error Logging Flow**:
    - Force database error → Dao_ErrorLog.LogErrorToDatabaseAsync → log_error_Add_Error → database
    - Verify error logged with full context
2. **Recursive Prevention Flow**:
    - Force error during error logging → Dao_ErrorLog recursive prevention logic
    - Verify fallback to file logging, no infinite loop
3. **Error Cooldown Flow**:
    - Trigger same error 10 times in 5 seconds
    - Verify all logged to database but UI shows MessageBox only once

**Execution**: Same as T122 but for error handling workflows

**Deliverable**: Error handling workflow validation report

---

#### T127 [P] [SP-Integration] Quick Buttons and User Preferences Workflow Tests

**Scope**: Test quick button management and user settings persistence

**Test Scenarios**:

1. **Quick Button Management Flow**:
    - Control_QuickButtons → Dao_QuickButtons.AddQuickButtonAtPositionAsync → sys_last_10_transactions_AddQuickButton → database
    - Verify button added, position correct
    - Test reordering, removal, clear all
2. **User Settings Persistence Flow**:
    - Update settings → Dao_User.SetSettingsJsonAsync → sys_user_settings_Update → database
    - Close app, reopen → verify settings loaded correctly

**Execution**: Same as T122 but for quick buttons/settings workflows

**Deliverable**: Quick buttons/settings workflow validation report

---

#### T128 [SP-Integration] Consolidate Integration Test Results

**Purpose**: Aggregate all integration test results and generate final validation report

**Execution**:

1. Compile results from T122-T127
2. Calculate overall success metrics:
    - Total workflows tested
    - Pass/Fail/Partial breakdown
    - Critical failures requiring immediate attention
    - Performance metrics (average response time per workflow)
3. Generate final report: `Tests/FINAL_INTEGRATION_VALIDATION_REPORT.md`

**Final Report Sections**:

-   Executive Summary (overall pass rate)
-   Success Criteria Validation (10 criteria from user requirements)
-   Workflow-by-Workflow Results
-   Performance Analysis (caller → SP → database timing)
-   Known Issues and Workarounds
-   Recommendations for Production Deployment
-   Sign-Off Checklist

**Success Criteria Validation**:

-   ✅ Zero compilation errors in C#
-   ✅ 100% test pass rate (all procedures)
-   ✅ No runtime exceptions during workflows
-   ✅ All procedures have OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)
-   ✅ All parameters use p\_ prefix consistently
-   ✅ All procedures include validation logic
-   ✅ Performance benchmarks met (all queries < 30 seconds)
-   ✅ Connection pool healthy under load
-   ✅ Transaction rollback working correctly
-   ✅ Error logging without recursion

**Deliverable**:

-   Final integration validation report
-   Production deployment recommendation (GO / NO-GO)
-   Sign-off from stakeholders

---

**Checkpoint**: ✅ End-to-end integration testing complete - ALL workflows validated, ready for production deployment

---

### Phase 2.5 - Part F: Documentation and Knowledge Transfer (T129-T132)

**Purpose**: Document standardization work and enable future maintenance

#### T129 [P] [SP-Docs] Generate Stored Procedure Reference Documentation

**Purpose**: Create comprehensive reference for all standardized procedures

**Execution**:

1. For each procedure, generate documentation page:
    - Procedure name and purpose
    - Parameters (with types and descriptions)
    - Return values (status codes and their meanings)
    - Business logic summary
    - Usage examples (C# code calling the procedure)
    - Performance characteristics (average execution time)
    - Related procedures (calls or is called by)
2. Generate master index: `Database/STORED_PROCEDURE_REFERENCE.md`
3. Generate quick reference card: `Database/SP_QUICK_REFERENCE.pdf` (single-page cheat sheet)

**Deliverable**:

-   Comprehensive procedure reference documentation
-   Quick reference card for developers

---

#### T130 [P] [SP-Docs] Update Developer Onboarding Documentation

**Purpose**: Update developer guides with standardized procedure patterns

**Files to Update**:

-   `Documentation/Copilot Files/07-database-and-stored-procedures.md`
-   `AGENTS.md` (Database Operations Pattern section)
-   `specs/002-comprehensive-database-layer/quickstart.md`

**Updates**:

-   Reference `00_STATUS_CODE_STANDARDS.md` as authoritative source
-   Add examples of standardized procedure templates
-   Update DAO implementation patterns to match standardized procedures
-   Add troubleshooting guide for common procedure issues

**Deliverable**: Updated developer documentation files

---

#### T131 [SP-Docs] Create Stored Procedure Maintenance Guide

**Purpose**: Document maintenance procedures for future procedure development

**Content**:

1. **Adding New Procedures**:
    - Template selection (SELECT vs INSERT/UPDATE/DELETE)
    - Parameter naming conventions
    - Status code implementation
    - Testing requirements before deployment
2. **Modifying Existing Procedures**:
    - Backward compatibility checklist
    - Parameter change process (coordinate with C# updates)
    - Testing regression suite
3. **Deprecating Procedures**:
    - Deprecation announcement process
    - Timeline for removal
    - Migration path documentation
4. **Performance Optimization**:
    - Using EXPLAIN to analyze queries
    - Index recommendations
    - Query refactoring patterns

**Deliverable**: `Database/STORED_PROCEDURE_MAINTENANCE_GUIDE.md`

---

#### T132 [SP-Docs] Generate Phase 2.5 Completion Report

**Purpose**: Document the entire standardization effort for stakeholders

**Report Sections**:

1. **Project Overview**:
    - Objectives and success criteria
    - Timeline and resource allocation
    - Key stakeholders
2. **Discovery Findings** (from T100-T106):
    - Total procedures discovered
    - Broken references fixed
    - Compliance scores before/after
3. **Refactoring Summary** (from T113-T118):
    - Procedures refactored by tier
    - Parameter naming standardization
    - Compliance achievement metrics
4. **Testing Results** (from T107-T128):
    - Test coverage (procedures × test methods)
    - Integration test pass rates
    - Performance benchmarks
5. **Deployment Summary** (from T119-T121):
    - Deployment timeline
    - Issues encountered and resolved
    - Rollback events (if any)
6. **Lessons Learned**:
    - What went well
    - What could be improved
    - Recommendations for future work
7. **Maintenance Plan**:
    - Ongoing monitoring strategy
    - Quarterly compliance audits
    - Training plan for new developers

**Deliverable**: `Database/PHASE_2.5_COMPLETION_REPORT.md` with executive summary for leadership

---

**Checkpoint**: ✅ Documentation complete - knowledge transferred, maintenance plan established, Phase 2.5 COMPLETE

---

### Phase 2.5: Task Summary

**Total Tasks**: 33 tasks (T100-T132)  
**Estimated Duration**: 15-25 days (single developer) or 8-12 days (3 developers parallel)

**Task Breakdown**:

-   **Part A - Discovery**: 7 tasks (T100-T106) - 3-4 days
-   **Part B - Testing**: 6 tasks (T107-T112) - 3-5 days
-   **Part C - Refactoring**: 6 tasks (T113-T118) - 5-8 days
-   **Part D - Deployment**: 3 tasks (T119-T121) - 1-2 days
-   **Part E - Integration**: 7 tasks (T122-T128) - 2-4 days
-   **Part F - Documentation**: 4 tasks (T129-T132) - 1-2 days

**Critical Dependencies**:

-   T100-T106 must complete before T107 starts (need discovery data for test generation)
-   T107-T112 must complete before T113 starts (tests must exist before refactoring)
-   T113-T118 must complete before T119 starts (all procedures refactored before deployment)
-   T119-T121 must complete before T122 starts (deployment before integration testing)
-   T122-T128 can parallel after deployment (different workflows)
-   T129-T132 can parallel (documentation tasks independent)

**Parallelization Opportunities**:

-   **Part A**: T100, T101, T104, T106 can parallel (T102-T103 depend on T100-T101)
-   **Part B**: All 6 tasks can parallel (T107-T111 independent, T112 waits for all)
-   **Part C**: T113-T117 can parallel by tier (T118 sequential)
-   **Part E**: T122-T127 can fully parallel (different workflows)
-   **Part F**: All 4 tasks can parallel (documentation tasks)

**Resource Allocation** (3-developer team):

-   Developer 1: Discovery (T100-T106) → System/Auth tests (T107) → System procedures refactor (T113-T114 subset)
-   Developer 2: Inventory tests (T108) → Inventory procedures refactor (T115-T116 subset) → Integration testing (T122-T124)
-   Developer 3: Master data tests (T110) → Master data procedures refactor (T116-T117 subset) → Documentation (T129-T132)

**Success Criteria** (from user requirements):

1. ✅ Zero compilation errors in C#
2. ✅ 100% test pass rate (all procedures)
3. ✅ No runtime exceptions during execution
4. ✅ All procedures have OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)
5. ✅ All parameters use p\_ prefix consistently
6. ✅ All procedures include validation logic
7. ✅ Performance benchmarks met (queries < 30 seconds)
8. ✅ Connection pool healthy under load
9. ✅ Transaction rollback working correctly
10. ✅ Error logging without recursion

---

### Impact on Existing Tasks (Superseded Tasks)

**⚠️ IMPORTANT**: Phase 2.5 completion SUPERSEDES the following tasks from Phases 3-8 that involved stored procedure work:

#### Phase 3 (US1) - System and ErrorLog DAO Tasks

**T017 - Refactor Dao_System.cs**: ✅ SUPERSEDED BY T113-T114 (System procedures standardized)

-   **Remaining Work**: Verify C# DAO methods correctly handle standardized status codes from T113-T114
-   **Dependencies**: T113-T114 complete → verify T017 DAO methods → mark complete

**T018 - Refactor Dao_ErrorLog.cs**: ✅ SUPERSEDED BY T113-T114 (Error logging procedures standardized)

-   **Remaining Work**: Verify recursive prevention logic works with standardized procedures
-   **Dependencies**: T113-T114 complete → verify T018 DAO methods → mark complete

#### Phase 4 (US2) - Inventory, Transactions, History DAO Tasks

**T024 - Refactor Dao_Inventory.cs**: ✅ SUPERSEDED BY T115-T116 (Inventory procedures standardized)

-   **Remaining Work**: Verify transaction rollback logic works with standardized procedures
-   **Dependencies**: T115-T116 complete → verify T024 DAO methods → mark complete

**T025 - Refactor Dao_Transactions.cs**: ✅ SUPERSEDED BY T115-T116 (Transaction procedures standardized)

-   **Remaining Work**: Verify transaction logging captures standardized status codes
-   **Dependencies**: T115-T116 complete → verify T025 DAO methods → mark complete

**T026 - Refactor Dao_History.cs**: ✅ SUPERSEDED BY T115-T116 (History procedures standardized)

-   **Remaining Work**: Verify history retrieval handles standardized return values
-   **Dependencies**: T115-T116 complete → verify T026 DAO methods → mark complete

#### Phase 5 (US3) - User and Part DAO Tasks

**T033 - Refactor Dao_User.cs**: ✅ SUPERSEDED BY T116-T117 (User procedures standardized)

-   **Remaining Work**: Verify all 25 UI settings methods handle standardized procedures
-   **Dependencies**: T116-T117 complete → verify T033a-y DAO methods → unblock T046 subtasks

**T034 - Refactor Dao_Part.cs**: ✅ SUPERSEDED BY T116-T117 (Part procedures standardized)

-   **Remaining Work**: Verify part CRUD operations work with standardized procedures
-   **Dependencies**: T116-T117 complete → verify T034 DAO methods → mark complete

#### Phase 6 (US4) - Master Data DAO Tasks

**T042 - Refactor Dao_Location.cs**: ✅ SUPERSEDED BY T116-T117 (Location procedures standardized)
**T043 - Refactor Dao_Operation.cs**: ✅ SUPERSEDED BY T116-T117 (Operation procedures standardized)
**T044 - Refactor Dao_ItemType.cs**: ✅ SUPERSEDED BY T116-T117 (ItemType procedures standardized)
**T045 - Refactor Dao_QuickButtons.cs**: ✅ SUPERSEDED BY T116-T117 (QuickButtons procedures standardized)

-   **Remaining Work**: Verify all master data DAOs handle standardized procedures
-   **Dependencies**: T116-T117 complete → verify T042-T045 DAO methods → mark complete

#### Validation Tasks Affected

**T039 - StoredProcedureValidation_Tests**: ✅ SUPERSEDED BY T104 (Compliance audit automated)

-   **Status**: T104 compliance audit replaces manual validation script

**T040 - Validate-Parameter-Prefixes.ps1**: ✅ SUPERSEDED BY T103 + T116 (Parameter validation automated + standardized)

-   **Status**: T103 performs validation, T116 fixes all issues

**T041 - ParameterNaming_Tests**: ✅ SUPERSEDED BY T103 + T112 (Parameter validation + test coverage)

-   **Status**: T103 validates naming, T112 verifies with tests

---

### Updated Task Completion Strategy

**Before Phase 2.5**:

-   DAO refactoring happened first → discovered procedure issues → created procedures → tested

**After Phase 2.5**:

-   Procedures standardized FIRST → DAOs verified against standards → tests confirm compliance

**New Workflow**:

1. Complete Phase 2.5 (T100-T132) - Standardize ALL procedures
2. Return to Phases 3-8 DAO tasks:
    - **Skip**: Stored procedure creation/modification (already done in Phase 2.5)
    - **Do**: Verify DAO methods correctly call standardized procedures
    - **Do**: Update C# code if parameter names changed
    - **Do**: Run integration tests to confirm workflows

**Time Savings**: Phases 3-8 now significantly faster since procedure work is complete

---

**Phase 2.5 Status**: ⚠️ NOT STARTED - BLOCKS ALL DAO REFACTORING

---

## Phase 3: User Story 1 - Developer Adds New Database Operation (Priority: P1) 🎯 MVP

**Goal**: Standardize DAO pattern to eliminate parameter prefix errors and provide consistent error handling

**Independent Test**: Create test stored procedure, implement DAO method, verify Model_Dao_Result responses with valid/invalid data

**Warning Resolution**: WRN001 (Dao_ErrorLog - 10 CS0618 warnings to eliminate)

### Tests for User Story 1

**NOTE: Write these tests FIRST using BaseIntegrationTest, ensure they FAIL before DAO implementation**

-   [x] T014 [P] [US1-Test] Create `Tests/Integration/Dao_System_Tests.cs` with 15 comprehensive test methods covering System_UserAccessTypeAsync, GetUserIdByNameAsync, GetRoleIdByNameAsync, GetAllThemesAsync, error handling scenarios, and Model_Dao_Result pattern validation
-   [x] T015 [P] [US1-Test] Create `Tests/Integration/Dao_ErrorLog_Tests.cs` with 18 comprehensive test methods covering GetUniqueErrorsAsync (3 tests), GetAllErrorsAsync (2 tests), GetErrorsByUserAsync (2 tests), GetErrorsByDateRangeAsync (3 tests), delete operations (2 tests), error handling methods (3 tests), recursive prevention validation (2 tests), data integrity (2 tests), and null/empty parameter handling (2 tests)
-   [x] T016 [P] [US1-Test] Create `Tests/Integration/Helper_Database_StoredProcedure_Tests.cs` with 11 comprehensive test methods covering parameter prefix detection (4 tests validating p\_ prefix application and fallback logic), Model_Dao_Result pattern validation (2 tests for success/failure scenarios with connection errors), null/empty parameter handling (2 tests for graceful handling), connection management (2 tests validating disposal and concurrent pooling with 10 parallel operations), and performance (1 test validating <30 second timeout). Build verified: 0 errors, 149 warnings.

### Implementation for User Story 1

#### T017 [P] [US1] Refactor `Data/Dao_System.cs` ✅ COMPLETE

**File**: `Data/Dao_System.cs`

All methods refactored to remove useAsync parameters and call async Helper methods directly. All callers updated (Tests, Program.cs, Core_Themes.cs). Build verified: 0 errors, 145 warnings.

**Methods**:

-   [x] T017a SetUserAccessTypeAsync - Uses ExecuteNonQueryWithStatusAsync, returns Model_Dao_Result
-   [x] T017b System_UserAccessTypeAsync - Uses ExecuteDataTableWithStatusAsync, returns Model_Dao_Result<DataTable>
-   [x] T017c GetUserIdByNameAsync - Uses ExecuteScalarWithStatusAsync, returns Model_Dao_Result<int>
-   [x] T017d GetRoleIdByNameAsync - Uses ExecuteScalarWithStatusAsync, returns Model_Dao_Result<int>
-   [x] T017e GetAllThemesAsync - Uses ExecuteDataTableWithStatusAsync, returns Model_Dao_Result<DataTable>

#### T018 [P] [US1] Refactor `Data/Dao_ErrorLog.cs` ✅ COMPLETE (Tests need updates)

**File**: `Data/Dao_ErrorLog.cs`

**Status**: All 6 methods refactored to return Model_Dao_Result types. Recursive prevention and cooldown logic preserved. **NOTE**: Integration tests need updating to handle Model_Dao_Result<DataTable> return types.

**Methods**:

-   [x] T018-Core LogErrorToDatabaseAsync - useAsync removed, recursive prevention maintained
-   [x] T018a GetAllErrorsAsync - Returns Model_Dao_Result<DataTable> with proper error handling
-   [x] T018b GetErrorsByUserAsync - Returns Model_Dao_Result<DataTable> with proper error handling
-   [x] T018c DeleteErrorByIdAsync - Returns Model_Dao_Result with proper error handling
-   [x] T018d DeleteAllErrorsAsync - Returns Model_Dao_Result with proper error handling
-   [x] T018e HandleException_SQLError_CloseApp - Returns Model_Dao_Result with recursive prevention preserved
-   [x] T018f HandleException_GeneralError_CloseApp - Returns Model_Dao_Result with cooldown logic preserved

**Test Updates Required**:

-   [x] Update Dao_ErrorLog_Tests.cs to check result.IsSuccess and result.Data.Rows ✅ COMPLETE (2025-10-14)
-   [x] ErrorLogging_Tests.cs covered by Dao_ErrorLog_Tests.cs ✅ COMPLETE (2025-10-14)
-   [x] Update ErrorCooldown_Tests.cs to check result.IsSuccess and result.Data.Rows ✅ COMPLETE (2025-10-14)
-   [x] Update ValidationErrors_Tests.cs to check result.IsSuccess and result.Data ✅ COMPLETE (2025-10-14)

-   [x] T019 [US1] Update `Program.cs` to use Dao_System.CheckConnectivityAsync for startup database validation per FR-014, display actionable error message if database unavailable, terminate gracefully on failure. ✅ VERIFIED: Database connectivity check implemented in Program.cs startup sequence.
-   [x] T020 [US1] Add XML documentation to Model_Dao_Result.cs, Model_Dao_Result_Generic.cs following documentation standards (summary, param, returns, example tags). ✅ VERIFIED: Comprehensive XML documentation present with usage examples and remarks.

**Checkpoint**: ✅ Basic DAO pattern established with System and ErrorLog DAOs - can create new database operations following this pattern - Phase 3 COMPLETE (test updates pending)

---

## Phase 4: User Story 2 - Reliable Database Operations (Priority: P1)

**Goal**: Ensure all database operations handle errors gracefully with consistent status processing

**Independent Test**: Execute 100 consecutive operations, force disconnect mid-operation, verify proper status codes without crashes

**Warning Resolution**: WRN002-WRN003 (Dao_History - 5 CS0618, Dao_Inventory - 15 CS0618 warnings to eliminate)

### Tests for User Story 2

-   [x] T021 [P] [US2-Test] Fixed `Tests/Integration/Dao_Inventory_Tests.cs` - 15 compilation errors resolved (method signature mismatches)
-   [x] T022 [P] [US2-Test] Create `Tests/Integration/ConnectionPooling_Tests.cs` with 50 concurrent GetAllInventoryAsync operations and verify no connection pool exhaustion or deadlocks
-   [x] T023 [P] [US2-Test] Create `Tests/Integration/TransactionManagement_Tests.cs` with multi-step TransferInventoryAsync, force mid-operation failure, verify rollback with zero orphaned records

### Implementation for User Story 2

#### T024 [P] [US2] Refactor `Data/Dao_Inventory.cs` ✅ COMPLETE

**File**: `Data/Dao_Inventory.cs`

All methods refactored to async methods returning Model_Dao_Result variants with proper transaction management for multi-step operations.

**Methods**:

-   [x] T024a GetAllInventoryAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T024b AddInventoryAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T024c RemoveInventoryAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T024d TransferInventoryAsync - Returns Model_Dao_Result, multi-step transaction with rollback support
-   [x] T024e SearchInventoryAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T024f UpdateInventoryAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus

#### T025 [P] [US2] Refactor `Data/Dao_Transactions.cs` ✅ COMPLETE

**File**: `Data/Dao_Transactions.cs`

All methods refactored to async methods returning Model_Dao_Result variants routing through Helper_Database_StoredProcedure.

**Methods**:

-   [x] T025a LogTransactionAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T025b GetTransactionHistoryAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T025c SearchTransactionsAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus

#### T026 [P] [US2] Refactor `Data/Dao_History.cs` ✅ COMPLETE

**File**: `Data/Dao_History.cs`

**Status**: All methods complete with Model_Dao_Result return types. All callers updated with proper error handling.

**Methods**:

-   [x] T026a GetInventoryHistoryAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T026b GetRemoveHistoryAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T026c GetTransferHistoryAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T026d AddTransactionHistoryAsync - Returns Model_Dao_Result with proper error handling, all callers updated (Control_TransferTab lines 740 & 795, Control_RemoveTab line 362)

#### T027 [US2] Update WinForms Controls - MainForm Tabs ✅ COMPLETE

**Files**: Multiple MainForm controls updated to async/await patterns

**Controls Updated**:

-   [x] T027a Control_RemoveTab.cs - LoadInventoryAsync, button click handlers use await Dao_Inventory methods, check Model_Dao_Result.IsSuccess
-   [x] T027b Control_QuickButtons.cs - LoadQuickButtonsAsync uses async patterns
-   [x] T027c Control_AdvancedRemove.cs - Async inventory operations with Model_Dao_Result checks

#### T028 [US2] Update Settings Controls ✅ COMPLETE

**Files**: Settings controls updated to async/await patterns

**Controls Updated**:

-   [x] T028a Control_Add_User.cs - LoadDataAsync event handlers use await methods
-   [x] T028b Control_Add_Operation.cs - LoadDataAsync event handlers use await methods

-   [x] T029 [US2] Final validation and documentation: verify all obsolete warnings resolved in Phase 4 scope, build verification shows 0 errors, Phase 4 complete summary documented

**Checkpoint**: ✅ Core inventory and transaction operations reliable with consistent error handling - Phase 4 COMPLETE

---

## Phase 5: User Story 3 - Developer Troubleshoots Database Issues (Priority: P2)

**Goal**: Comprehensive error logging with full context and clear messages

**Independent Test**: Trigger error conditions, review log_error table and Service_DebugTracer output, verify all context included

**Warning Resolution**: WRN004-WRN006 (Dao_User - 25 CS0618, Dao_Part - 10 CS0618 warnings to eliminate)

### Tests for User Story 3

-   [x] T030 [P] [US3-Test] Create `Tests/Integration/ErrorLogging_Tests.cs` with forced connection failure, verify log_error entry includes User, Severity, ErrorType, ErrorMessage, StackTrace, MethodName, MachineName, OSVersion, AppVersion, ErrorTime
-   [x] T031 [P] [US3-Test] Create `Tests/Integration/ValidationErrors_Tests.cs` with null required parameter to DAO method, verify user-friendly validation message returned (not MySQL exception), detailed technical info logged
-   [x] T032 [P] [US3-Test] Create `Tests/Integration/ErrorCooldown_Tests.cs` with same error triggered 10 times within 5 seconds, verify all logged to database but UI shows MessageBox only once

### Implementation for User Story 3

#### T033 [P] [US3] Refactor `Data/Dao_User.cs` ✅ COMPLETE (2025-10-14)

**File**: `Data/Dao_User.cs`

**Status**: All methods complete with Model_Dao_Result return types. Helper methods renamed to GetSettingsJsonInternalAsync and SetUserSettingInternalAsync.

**Core CRUD Methods** (✅ Complete):

-   [x] T033-Core-a AuthenticateUserAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T033-Core-b GetAllUsersAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T033-Core-c CreateUserAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T033-Core-d UpdateUserAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T033-Core-e DeleteUserAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus

**UI Settings Methods** (✅ COMPLETE - 2025-10-14):

-   [x] T033a GetLastShownVersionAsync (line 22) - ✅ Returns Model_Dao_Result<string>
-   [x] T033b SetLastShownVersionAsync (line 32) - ✅ Returns Model_Dao_Result
-   [x] T033c GetHideChangeLogAsync (line 41) - ✅ Returns Model_Dao_Result<string>
-   [x] T033d SetHideChangeLogAsync (line 51) - ✅ Returns Model_Dao_Result
-   [x] T033e GetThemeNameAsync (line 60) - ✅ Returns Model_Dao_Result<string?>
-   [x] T033f GetThemeFontSizeAsync (line 70) - ✅ Returns Model_Dao_Result<int?>
-   [x] T033g SetThemeFontSizeAsync (line 92) - ✅ Returns Model_Dao_Result
-   [x] T033h GetVisualUserNameAsync (line 101) - ✅ Returns Model_Dao_Result<string>
-   [x] T033i SetVisualUserNameAsync (line 112) - ✅ Returns Model_Dao_Result
-   [x] T033j GetVisualPasswordAsync (line 121) - ✅ Returns Model_Dao_Result<string>
-   [x] T033k SetVisualPasswordAsync (line 132) - ✅ Returns Model_Dao_Result
-   [x] T033l GetWipServerAddressAsync (line 141) - ✅ Returns Model_Dao_Result<string>
-   [x] T033m SetWipServerAddressAsync (line 152) - ✅ Returns Model_Dao_Result
-   [x] T033n GetDatabaseAsync (line 163) - ✅ Returns Model_Dao_Result<string>
-   [x] T033o SetDatabaseAsync (line 174) - ✅ Returns Model_Dao_Result
-   [x] T033p GetWipServerPortAsync (line 188) - ✅ Returns Model_Dao_Result<string>
-   [x] T033q SetWipServerPortAsync (line 199) - ✅ Returns Model_Dao_Result
-   [x] T033r GetUserFullNameAsync (line 211) - ✅ Returns Model_Dao_Result<string?>
-   [x] T033s GetSettingsJsonAsync (line 248) - ✅ Renamed to GetSettingsJsonInternalAsync (private helper)
-   [x] T033t SetSettingsJsonAsync (line 306) - ✅ Returns Model_Dao_Result
-   [x] T033u SetGridViewSettingsJsonAsync (line 340) - ✅ Returns Model_Dao_Result
-   [x] T033v GetGridViewSettingsJsonAsync (line 375) - ✅ Returns Model_Dao_Result<string>
-   [x] T033w GetShortcutsJsonAsync (line 751) - ✅ Returns Model_Dao_Result<string>
-   [x] T033x SetShortcutsJsonAsync (line 784) - ✅ Returns Model_Dao_Result
-   [x] T033y SetThemeNameAsync (line 818) - ✅ Returns Model_Dao_Result

#### T034 [P] [US3] Refactor `Data/Dao_Part.cs` ✅ COMPLETE

**File**: `Data/Dao_Part.cs`

File recreated from scratch with full async/await pattern, Model_Dao_Result return types, removed all useAsync parameters, implemented Service_DebugTracer integration, added backward compatibility wrappers marked as Obsolete.

**Methods**:

-   [x] T034a GetPartAsync - Returns Model_Dao_Result<DataRow>, uses ExecuteDataTableWithStatus
-   [x] T034b CreatePartAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T034c UpdatePartAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T034d DeletePartAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T034e SearchPartsAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T034f GetPartByNumberAsync - Returns Model_Dao_Result<DataRow>, new method with Model_Dao_Result pattern
-   [x] T034g PartExistsAsync - Returns Model_Dao_Result<bool>, new method with Model_Dao_Result pattern

-   [x] T035 [US3] Add Service_DebugTracer integration to all DAO methods: TraceMethodEntry with parameters at method start, TraceMethodExit with result before return - ✅ COMPLETE: Integrated into both Dao_User.cs (core methods) and Dao_Part.cs during T033/T034 recreation
-   [x] T036 [US3] Implement error cooldown mechanism in Service_ErrorHandler: track last error message and timestamp, suppress duplicate UI errors within 5 seconds, still log all occurrences to database - ✅ COMPLETE: Added \_lastErrorTimestamp Dictionary, ErrorCooldownPeriod constant (5 seconds), updated ShouldSuppressError to check cooldown with timestamp tracking, all errors still logged to database, added ClearErrorCooldownState() method for testing
-   [x] T037 [US3] Add three-tier severity classification to LoggingUtility.LogDatabaseError per clarification Q5: Critical (data integrity risk), Error (operation failed), Warning (unexpected but handled) - ✅ COMPLETE: Created Enum_DatabaseEnum_ErrorSeverity enum with Warning/Error/Critical levels, updated LogDatabaseError signature to accept Enum_DatabaseEnum_ErrorSeverity parameter (defaults to Error), severity label included in log entries, updated Service_ErrorHandler.HandleDatabaseError to map severity to UI error levels

#### T038 [US3] Update User Management Controls ✅ COMPLETE

**Files**: User management controls updated to async/await patterns with Model_Dao_Result checks

**Controls Updated**:

-   [x] T038a Control_Add_User.cs - Uses Dao_User.UserExistsAsync, CreateUserAsync, GetUserByUsernameAsync, AddUserRoleAsync with Model_Dao_Result pattern
-   [x] T038b Control_Edit_User.cs - Uses GetUserByUsernameAsync, UpdateUserAsync, SetUserRoleAsync, GetUserRoleIdAsync with proper Model_Dao_Result checking
-   [x] T038c Control_Remove_User.cs - Uses GetUserByUsernameAsync, GetUserRoleIdAsync, DeleteUserSettingsAsync, RemoveUserRoleAsync, DeleteUserAsync with comprehensive error handling

**Checkpoint**: ✅ Comprehensive error logging operational - can troubleshoot production issues with full context - Phase 5 requires T033a-y completion for UI settings methods

---

## Phase 6: User Story 4 - Database Administrator Maintains Schema (Priority: P2)

**Goal**: Uniform parameter naming and output conventions across all stored procedures

**Independent Test**: Query all stored procedures for consistency, run validation script, confirm 0 inconsistencies

**Warning Resolution**: WRN007-WRN010 (Dao_Location - 8 CS0618, Dao_ItemType - 6 CS0618, Dao_Operation - 6 CS0618, Dao_QuickButtons - 10 CS0618 warnings to eliminate)

### Tests for User Story 4

-   [x] T039 [P] [US4-Test] Create `Tests/Integration/StoredProcedureValidation_Tests.cs` with query to verify all 60+ procedures have OUT p_Status and OUT p_ErrorMsg parameters
-   [x] T040 [P] [US4-Test] Create `Scripts/Validate-Parameter-Prefixes.ps1` PowerShell script to query INFORMATION*SCHEMA.PARAMETERS, check all parameters use standard prefixes (p*, in*, o*), report inconsistencies
-   [x] T041 [P] [US4-Test] Create `Tests/Integration/ParameterNaming_Tests.cs` to verify stored procedure parameter names match C# model properties in PascalCase (PartID, Location, Operation)

### DAO Refactoring (T042-T045)

#### T042 [P] [US4] Refactor `Data/Dao_Location.cs` ✅ COMPLETE

**File**: `Data/Dao_Location.cs`

**Methods**:

-   [x] T042a GetAllLocationsAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T042b CreateLocationAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T042c UpdateLocationAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T042d DeleteLocationAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T042e GetLocationByName - Returns Model_Dao_Result<DataRow>, uses ExecuteDataTableWithStatus
-   [x] T042f LocationExists - Returns Model_Dao_Result<bool>, uses ExecuteScalarWithStatus

#### T043 [P] [US4] Refactor `Data/Dao_Operation.cs` ✅ COMPLETE

**File**: `Data/Dao_Operation.cs`

**Methods**:

-   [x] T043a GetAllOperationsAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T043b CreateOperationAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T043c UpdateOperationAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T043d DeleteOperationAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T043e GetOperationByNumber - Returns Model_Dao_Result<DataRow>, uses ExecuteDataTableWithStatus
-   [x] T043f OperationExists - Returns Model_Dao_Result<bool>, uses ExecuteScalarWithStatus

#### T044 [P] [US4] Refactor `Data/Dao_ItemType.cs` ✅ COMPLETE

**File**: `Data/Dao_ItemType.cs`

**Methods**:

-   [x] T044a GetAllItemTypesAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T044b CreateItemTypeAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T044c UpdateItemTypeAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T044d DeleteItemTypeAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T044e GetItemTypeByName - Returns Model_Dao_Result<DataRow>, uses ExecuteDataTableWithStatus
-   [x] T044f ItemTypeExists - Returns Model_Dao_Result<bool>, uses ExecuteScalarWithStatus

#### T045 [P] [US4] Refactor `Data/Dao_QuickButtons.cs` ✅ COMPLETE

**File**: `Data/Dao_QuickButtons.cs`

**Methods**:

-   [x] T045a GetQuickButtonsAsync - Returns Model_Dao_Result<DataTable>, uses ExecuteDataTableWithStatus
-   [x] T045b SaveQuickButtonAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T045c DeleteQuickButtonAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T045d UpdateQuickButtonAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T045e RemoveQuickButtonAndShiftAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T045f DeleteAllQuickButtonsForUserAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T045g AddQuickButtonAtPositionAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus
-   [x] T045h AddOrShiftQuickButtonAsync - Returns Model_Dao_Result, uses ExecuteNonQueryWithStatus

### Control Updates - SettingsForm (T046a-T046r)

#### T046a [P] [US4] `Control_Add_ItemType.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Add_ItemType.cs`

**Methods Updated**:

-   [x] T046a-1 btnSave_Click - ItemTypeExists checks result.Data (bool)
-   [x] T046a-2 btnSave_Click - InsertItemType checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046b [P] [US4] `Control_Edit_ItemType.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Edit_ItemType.cs`

**Methods Updated**:

-   [x] T046b-1 cmbItemTypes_SelectedIndexChanged - GetItemTypeByName checks result.Data (DataRow)
-   [x] T046b-2 btnSave_Click - ItemTypeExists checks result.Data (bool)
-   [x] T046b-3 btnSave_Click - UpdateItemType checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046c [P] [US4] `Control_Remove_ItemType.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Remove_ItemType.cs`

**Methods Updated**:

-   [x] T046c-1 cmbItemTypes_SelectedIndexChanged - GetItemTypeByName checks result.Data (DataRow)
-   [x] T046c-2 btnRemove_Click - DeleteItemType checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046d [P] [US4] `Control_Remove_Location.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Remove_Location.cs`

**Methods Updated**:

-   [x] T046d-1 cmbLocations_SelectedIndexChanged - GetLocationByName checks result.Data (DataRow)
-   [x] T046d-2 btnRemove_Click - DeleteLocation checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046e [P] [US4] `Control_Remove_Operation.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Remove_Operation.cs`

**Methods Updated**:

-   [x] T046e-1 cmbOperations_SelectedIndexChanged - GetOperationByNumber checks result.Data (DataRow)
-   [x] T046e-2 btnRemove_Click - DeleteOperation checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046f [P] [US4] `Control_Remove_PartID.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Remove_PartID.cs`

**Methods Updated**:

-   [x] T046f-1 cmbParts_SelectedIndexChanged - Replaced obsolete GetPartByNumber with GetPartByNumberAsync, checks result.Data (DataRow)
-   [x] T046f-2 btnRemove_Click - Replaced obsolete DeletePart with DeletePartAsync, checks result.IsSuccess

#### T046g [P] [US4] `Control_Shortcuts.cs` ❌ BLOCKED

**File**: `Controls/SettingsForm/Control_Shortcuts.cs`

**Status**: BLOCKED by T033w, T033x (Dao_User methods not yet refactored)

**Methods to Update** (when T033w-x complete):

-   [ ] T046g-1 LoadShortcuts (line 39) - GetShortcutsJsonAsync needs Model_Dao_Result check
-   [ ] T046g-2 SaveShortcuts (line 399) - SetShortcutsJsonAsync needs Model_Dao_Result check

#### T046h [P] [US4] `Control_Theme.cs` ❌ BLOCKED

**File**: `Controls/SettingsForm/Control_Theme.cs`

**Status**: BLOCKED by T033e, T033y (Dao_User methods not yet refactored)

**Methods to Update** (when T033e, T033y complete):

-   [ ] T046h-1 LoadTheme (line 34) - GetThemeNameAsync needs Model_Dao_Result check
-   [ ] T046h-2 SaveTheme (line 75) - SetThemeNameAsync needs Model_Dao_Result check

#### T046i [P] [US4] `Control_Edit_User.cs` - VERIFY ONLY

**File**: `Controls/SettingsForm/Control_Edit_User.cs`

**Status**: Partially complete per T038b. Need verification all Model_Dao_Result checks present.

**Methods to Verify**:

-   [ ] T046i-1 Verify LoadUser (line 147) - GetUserByUsernameAsync checks result.Data
-   [ ] T046i-2 Verify LoadUser (line 239) - GetUserByUsernameAsync checks result.Data
-   [ ] T046i-3 Verify LoadUser (line 169) - GetUserRoleIdAsync checks result.Data
-   [ ] T046i-4 Verify btnSave_Click (line 221) - UpdateUserAsync checks result.IsSuccess
-   [ ] T046i-5 Verify btnSave_Click (line 253) - SetUserRoleAsync checks result.IsSuccess

#### T046j [P] [US4] `Control_Remove_User.cs` - VERIFY ONLY

**File**: `Controls/SettingsForm/Control_Remove_User.cs`

**Status**: Partially complete per T038c. Need verification all Model_Dao_Result checks present.

**Methods to Verify**:

-   [ ] T046j-1 Verify LoadUser (line 129) - GetUserByUsernameAsync checks result.Data
-   [ ] T046j-2 Verify LoadUser (line 210) - GetUserByUsernameAsync checks result.Data
-   [ ] T046j-3 Verify LoadUser (line 141) - GetUserRoleIdAsync checks result.Data
-   [ ] T046j-4 Verify LoadUser (line 242) - GetUserRoleIdAsync checks result.Data
-   [ ] T046j-5 Verify btnRemove_Click (line 228) - DeleteUserSettingsAsync checks result.IsSuccess
-   [ ] T046j-6 Verify btnRemove_Click (line 246) - RemoveUserRoleAsync checks result.IsSuccess
-   [ ] T046j-7 Verify btnRemove_Click (line 255) - DeleteUserAsync checks result.IsSuccess

#### T046q [P] [US4] `Control_Add_PartID.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Add_PartID.cs`

**Methods Updated**:

-   [x] T046q-1 btnSave_Click (line 108) - Replaced obsolete PartExists with PartExistsAsync, added Model_Dao_Result check
-   [x] T046q-2 btnSave_Click (line 135) - Replaced obsolete AddPartWithStoredProcedure with CreatePartAsync, added Model_Dao_Result check

#### T046r [P] [US4] `Control_Edit_PartID.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Edit_PartID.cs`

**Methods Updated**:

-   [x] T046r-1 LoadPart (line 184) - Replaced obsolete GetPartByNumber with GetPartByNumberAsync, added Model_Dao_Result check
-   [x] T046r-2 btnSave_Click (line 332) - Replaced obsolete PartExists with PartExistsAsync, added Model_Dao_Result check
-   [x] T046r-3 btnSave_Click (line 512) - Replaced obsolete UpdatePartWithStoredProcedure with UpdatePartAsync, added Model_Dao_Result check

### Control Updates - MainForm (T046k-T046p)

#### T046k [P] [US4] `Control_TransferTab.cs` ❌ BLOCKED

**File**: `Controls/MainForm/Control_TransferTab.cs`

**Status**: BLOCKED by T033r (GetUserFullNameAsync not yet refactored)

**Methods to Update**:

-   [ ] T046k-1 LoadTransfer (line 145) - GetUserFullNameAsync needs Model_Dao_Result check (BLOCKED T033r)
-   [ ] T046k-2 LoadInventory (line 497) - GetInventoryByPartIdAndOperationAsync needs Model_Dao_Result check
-   [ ] T046k-3 LoadInventory (line 515) - GetInventoryByPartIdAsync needs Model_Dao_Result check
-   [ ] T046k-4 btnTransfer_Click (line 730) - TransferInventoryQuantityAsync verify error handling
-   [ ] T046k-5 btnTransfer_Click (line 736) - TransferPartSimpleAsync verify error handling
-   [ ] T046k-6 btnTransfer_Click (line 793) - TransferPartSimpleAsync verify error handling
-   [ ] T046k-7 btnTransfer_Click (line 740) - AddTransactionHistoryAsync verify error handling (BLOCKED T026d)
-   [ ] T046k-8 btnTransfer_Click (line 795) - AddTransactionHistoryAsync verify error handling (BLOCKED T026d)

#### T046l [P] [US4] `Control_RemoveTab.cs` ❌ BLOCKED

**File**: `Controls/MainForm/Control_RemoveTab.cs`

**Status**: BLOCKED by T033r (GetUserFullNameAsync not yet refactored). Some methods already correct per T027a.

**Methods to Update**:

-   [ ] T046l-1 LoadRemove (line 194) - GetUserFullNameAsync needs Model_Dao_Result check (BLOCKED T033r)
-   [ ] T046l-2 LoadInventory (line 708) - GetInventoryByPartIdAndOperationAsync needs Model_Dao_Result check
-   [ ] T046l-3 LoadInventory (line 721) - GetInventoryByPartIdAsync needs Model_Dao_Result check

**Already Correct** (✅ from T027a):

-   [x] T046l-4 RemoveInventoryItemsFromDataGridViewAsync (line 304) - Model_Dao_Result check present
-   [x] T046l-5 AddTransactionHistoryAsync (line 362) - Model_Dao_Result check present
-   [x] T046l-6 AddInventoryItemAsync (line 429) - Model_Dao_Result check present

#### T046m [P] [US4] `Control_AdvancedInventory.cs` - VERIFY ONLY

**File**: `Controls/MainForm/Control_AdvancedInventory.cs`

**Status**: Need verification AddInventoryItemAsync calls check result.IsSuccess.

**Methods to Verify**:

-   [ ] T046m-1 Verify AddInventory (line 895) - AddInventoryItemAsync checks result.IsSuccess
-   [ ] T046m-2 Verify BatchAdd (line 1441) - AddInventoryItemAsync checks result.IsSuccess
-   [ ] T046m-3 Verify ImportInventory (line 1725) - AddInventoryItemAsync checks result.IsSuccess

#### T046n [P] [US4] `Control_AdvancedRemove.cs` - VERIFY ONLY

**File**: `Controls/MainForm/Control_AdvancedRemove.cs`

**Status**: Need verification Model_Dao_Result checks present.

**Methods to Verify**:

-   [ ] T046n-1 Verify RemoveOperation (line 450) - RemoveInventoryItemsFromDataGridViewAsync checks result.IsSuccess
-   [ ] T046n-2 Verify AddBackInventory (line 684) - AddInventoryItemAsync checks result.IsSuccess

#### T046o [P] [US4] `Control_QuickButtons.cs` ✅ COMPLETE

**File**: `Controls/MainForm/Control_QuickButtons.cs`

**Methods Updated**:

-   [x] T046o-1 btnUpdate_Click (line 505) - UpdateQuickButtonAsync now checks Model_Dao_Result
-   [x] T046o-2 btnRemove_Click (line 529) - RemoveQuickButtonAndShiftAsync now checks Model_Dao_Result
-   [x] T046o-3 btnClearAll_Click (line 567) - DeleteAllQuickButtonsForUserAsync now checks Model_Dao_Result
-   [x] T046o-4 btnAdd_Click (line 577) - AddQuickButtonAtPositionAsync now checks Model_Dao_Result

#### T046p [P] [US4] `Control_InventoryTab.cs` ✅ COMPLETE

**File**: `Controls/MainForm/Control_InventoryTab.cs`

**Methods Updated**:

-   [x] T046p-1 AddToQuickButtons (line 675) - AddOrShiftQuickButtonAsync now checks Model_Dao_Result (non-critical operation)

**Already Correct**:

-   [x] T046p-2 AddInventory (line 604) - AddInventoryItemAsync checks result.IsSuccess

### Control Updates - Forms & Services (T046s-T046u)

#### T046s [P] [US4] `MainForm.cs` ❌ BLOCKED

**File**: `Forms/MainForm/MainForm.cs`

**Status**: BLOCKED by T033r (GetUserFullNameAsync not yet refactored)

**Methods to Update**:

-   [ ] T046s-1 LoadUserSettings (line 547) - GetUserFullNameAsync needs Model_Dao_Result check with fallback (BLOCKED T033r)

#### T046t [P] [US4] `MainFormUserSettingsHelper.cs` ❌ BLOCKED

**File**: `Forms/MainForm/Classes/MainFormUserSettingsHelper.cs`

**Status**: BLOCKED by multiple T033 subtasks (all Dao_User Get/Set methods)

**Methods to Update** (when T033a-y complete):

-   [ ] T046t-1 LoadSettings (line 18) - GetLastShownVersionAsync needs Model_Dao_Result check (BLOCKED T033a)
-   [ ] T046t-2 LoadSettings (line 21) - SetHideChangeLogAsync needs Model_Dao_Result check (BLOCKED T033d)
-   [ ] T046t-3 LoadSettings (line 23) - SetLastShownVersionAsync needs Model_Dao_Result check (BLOCKED T033b)
-   [ ] T046t-4 LoadSettings (line 26) - GetWipServerAddressAsync needs Model_Dao_Result check (BLOCKED T033l)
-   [ ] T046t-5 LoadSettings (line 27) - GetWipServerPortAsync needs Model_Dao_Result check (BLOCKED T033p)
-   [ ] T046t-6 LoadSettings (line 28) - GetVisualUserNameAsync needs Model_Dao_Result check (BLOCKED T033h)
-   [ ] T046t-7 LoadSettings (line 29) - GetVisualPasswordAsync needs Model_Dao_Result check (BLOCKED T033j)
-   [ ] T046t-8 LoadSettings (line 30) - GetThemeNameAsync needs Model_Dao_Result check (BLOCKED T033e)
-   [ ] T046t-9 LoadSettings (line 36) - GetThemeFontSizeAsync needs Model_Dao_Result check (BLOCKED T033f)

#### T046u [P] [US4] `Service_OnStartup_StartupSplashApplicationContext.cs` ❌ BLOCKED

**File**: `Services/Service_OnStartup_StartupSplashApplicationContext.cs`

**Status**: BLOCKED by T033f (GetThemeFontSizeAsync not yet refactored)

**Methods to Update** (when T033f complete):

-   [ ] T046u-1 LoadTheme (line 639) - GetThemeFontSizeAsync needs Model_Dao_Result check with default fallback (BLOCKED T033f)

### Final Tasks (T047-T049)

### Final Tasks (T047-T049)

#### T047 [US4] Refactor `Controls/SettingsForm/Control_Add_Operation.cs` ❌ INCOMPLETE

**File**: `Controls/SettingsForm/Control_Add_Operation.cs`

**Issue**: Bypasses DAO pattern entirely - calls Helper_Database_StoredProcedure directly instead of using Dao_Operation methods

**Methods to Update**:

-   [ ] T047a btnSave_Click (line 100) - Replace direct OperationExists call with Dao_Operation.OperationExistsAsync, add Model_Dao_Result<bool> check
-   [ ] T047b btnSave_Click (line 145) - Replace direct InsertOperation call with Dao_Operation.CreateOperationAsync, add Model_Dao_Result check

**Verification**: After refactor, confirm NO direct Helper_Database_StoredProcedure calls remain in this control

#### T048 [US4] Verify `Controls/MainForm/Control_QuickButtons.cs` - VERIFY ONLY

**File**: `Controls/MainForm/Control_QuickButtons.cs`

**Status**: LoadQuickButtonsAsync should already be correct (async/await patterns). Need verification that no other methods missed.

**Methods to Verify**:

-   [ ] T048a Verify LoadQuickButtonsAsync (line ~80) - Uses await Dao_QuickButtons.GetQuickButtonsAsync, checks result.IsSuccess
-   [ ] T048b Verify no other async methods missed - Scan entire file for database operations, confirm all route through Dao_QuickButtons
-   [ ] T048c Cross-reference with T046o completion - Verify button click handlers (btnUpdate, btnRemove, btnClearAll, btnAdd) use Model_Dao_Result checks

**Note**: T046o covers button click handlers. This task verifies LoadQuickButtonsAsync and ensures no database operations were missed during initial analysis.

-   [ ] T049 [US4] Verify `Scripts/Validate-Parameter-Prefixes.ps1` is current (checks against INFORMATION*SCHEMA.PARAMETERS, validates p* prefix usage), then run against production database, fix any reported inconsistencies in stored procedures or DAO code

**Checkpoint**: ✅ T042-T046f complete (DAO refactoring + 6 SettingsForm controls). ❌ Remaining: 47 subtasks blocked by T033a-y (Dao_User UI settings methods), plus T046q-r, T046o-p, T047-T049

---

## Phase 7: User Story 5 - Performance Analyst Reviews Execution (Priority: P3)

**Goal**: Visibility into database operation timing and connection pool metrics

**Independent Test**: Execute large queries, concurrent operations, batch modifications, verify timing logged and pool healthy

**Warning Resolution**: WRN005, WRN011-WRN017 (Dao_Transactions - 10 CS0618, Helper_UI_ComboBoxes - 8 CS0618, various Controls/Services - 20 CS0618 warnings to eliminate)

### Tests for User Story 5

-   [ ] T050 [P] [US5-Test] Create `Tests/Integration/PerformanceMonitoring_Tests.cs` with inventory search returning 10,000+ rows, verify operation timing logged, warning if >500ms threshold exceeded for Query category
-   [ ] T051 [P] [US5-Test] Create `Tests/Integration/ConcurrentOperations_Tests.cs` with 100 concurrent GetInventoryAsync calls, verify connection pool handles load (5-100 connections healthy), all operations succeed
-   [ ] T052 [P] [US5-Test] Create `Tests/Integration/TransactionRollback_Tests.cs` with batch removal of 100 items in transaction, force mid-batch error, verify complete rollback with no partial commits

### Additional Tests

-   [ ] T052a [US5-Test] Update/create all integration tests not currently in test project as documented in `specs/002-comprehensive-database-layer/AdditionalTests.md` (cross-reference with existing test coverage to avoid duplication)

### Implementation for User Story 5

-   [ ] T053 [P] [US5] Add operation category detection to `Helper_Database_StoredProcedure` based on stored procedure name patterns: _*get*_/_*search*_ = Query (500ms), _*add*_/_*update*_/_*delete*_ = Modification (1000ms), _*batch*_/_*bulk*_ = Batch (5000ms), _*report*_/_*summary*_ = Report (2000ms)
-   [ ] T054 [P] [US5] Add performance threshold configuration to `Model_Application_Variables`: QueryThresholdMs, ModificationThresholdMs, BatchThresholdMs, ReportThresholdMs with defaults from FR-020

#### T055-T060: OPTION A - MainForm Tab Controls (MVP - 6 controls)

-   [ ] T055 [P] [US5-OptionA] Update `Controls/MainForm/Control_InventoryTab.cs` to async/await patterns: LoadInventoryAsync, grid refresh operations, search functionality using await Dao_Inventory methods
-   [ ] T056 [P] [US5-OptionA] Update `Controls/MainForm/Control_AdvancedInventory.cs` to async/await patterns: LoadInventoryAsync, advanced search/filter operations, bulk operations using await Dao_Inventory methods
-   [ ] T057 [P] [US5-OptionA] Update `Controls/MainForm/Control_RemoveTab.cs` to async/await patterns: LoadRemoveHistoryAsync, removal operations using await Dao_History.GetRemoveHistoryAsync
-   [ ] T058 [P] [US5-OptionA] Update `Controls/MainForm/Control_AdvancedRemove.cs` to async/await patterns: LoadRemoveHistoryAsync, advanced filtering, bulk removal operations using await Dao_History methods
-   [ ] T059 [P] [US5-OptionA] Update `Controls/MainForm/Control_TransferTab.cs` to async/await patterns: LoadTransferHistoryAsync, transfer operations using await Dao_History.GetTransferHistoryAsync
-   [ ] T060 [US5-OptionA] Validate all MainForm tab controls updated - verify async patterns, Model_Dao_Result handling, no blocking .Result/.Wait() calls

#### T061-T076: OPTION B - Settings Controls (Comprehensive - 18 controls)

**User Management**:

-   [ ] T061 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_User.cs` LoadUsersAsync, btnSave_Click using await Dao_User.CreateUserAsync
-   [ ] T062 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_User.cs` LoadUsersAsync, btnSave_Click using await Dao_User.UpdateUserAsync
-   [ ] T063 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_User.cs` LoadUsersAsync, btnDelete_Click using await Dao_User.DeleteUserAsync

**Location Management**:

-   [ ] T064 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_Location.cs` LoadLocationsAsync, btnSave_Click using await Dao_Location.CreateLocationAsync
-   [ ] T065 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_Location.cs` LoadLocationsAsync, btnSave_Click using await Dao_Location.UpdateLocationAsync
-   [ ] T066 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_Location.cs` LoadLocationsAsync, btnDelete_Click using await Dao_Location.DeleteLocationAsync

**Operation Management**:

-   [ ] T067 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_Operation.cs` LoadOperationsAsync, btnSave_Click using await Dao_Operation.CreateOperationAsync
-   [ ] T068 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_Operation.cs` LoadOperationsAsync, btnSave_Click using await Dao_Operation.UpdateOperationAsync
-   [ ] T069 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_Operation.cs` LoadOperationsAsync, btnDelete_Click using await Dao_Operation.DeleteOperationAsync

**Part Management**:

-   [ ] T070 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_PartID.cs` LoadPartsAsync, btnSave_Click using await Dao_Part.CreatePartAsync
-   [ ] T071 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_PartID.cs` LoadPartsAsync, btnSave_Click using await Dao_Part.UpdatePartAsync
-   [ ] T072 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_PartID.cs` LoadPartsAsync, btnDelete_Click using await Dao_Part.DeletePartAsync

**ItemType Management**:

-   [ ] T073 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_ItemType.cs` LoadItemTypesAsync, btnSave_Click using await Dao_ItemType.CreateItemTypeAsync
-   [ ] T074 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_ItemType.cs` LoadItemTypesAsync, btnSave_Click using await Dao_ItemType.UpdateItemTypeAsync
-   [ ] T075 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_ItemType.cs` LoadItemTypesAsync, btnDelete_Click using await Dao_ItemType.DeleteItemTypeAsync

-   [ ] T076 [US5-OptionB] Validate all Settings controls updated - verify async patterns, Model_Dao_Result handling, no blocking .Result/.Wait() calls across all 18 controls

**Service and Infrastructure**:

-   [ ] T077 [US5] Create `Services/Service_Startup.cs` to encapsulate Program.cs startup validation logic (database connectivity, parameter cache initialization, connection pool health check)
-   [ ] T078 [US5] Create performance dashboard in Service_DebugTracer: track average execution time per stored procedure, connection pool statistics (active/idle/total connections), slow query frequency per category
-   [ ] T079 [US5] Add connection pool health check to startup validation: verify pool configuration (MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30), log pool statistics after initialization

**Checkpoint**: Performance monitoring operational - slow queries logged with category-specific thresholds, connection pool healthy under load

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: Documentation, validation, and final cleanup

-   [ ] T080 [P] [Polish] Update `Documentation/Copilot Files/04-patterns-and-templates.md` with Model_Dao_Result<T> pattern examples, Helper_Database_StoredProcedure usage, async DAO method templates
-   [ ] T081 [P] [Polish] Update `README.md` Database Access Patterns section with INFORMATION_SCHEMA parameter caching explanation, Model_Dao_Result wrapper pattern, async-first architecture
-   [ ] T082 [P] [Polish] Create `Documentation/Database-Layer-Migration-Guide.md` with before/after comparison, migration checklist for developers, troubleshooting common async migration issues

### T083: Manual Validation Checklist (Success Criteria SC-001 through SC-010)

**File**: Validation checklist based on `specs/002-comprehensive-database-layer/validation-checklist-T063.md`

#### Pre-Deployment Validation (Required before release)

-   [ ] T083a [Validation] **SC-001: Zero MySQL Parameter Errors** - Execute all integration tests, manually execute 120 operations (12 DAOs × 10), check log_error table for parameter errors, verify count = 0
-   [ ] T083b [Validation] **SC-002: 100% Helper Routing** - Run static code analysis script, verify 0 direct MySqlConnection/MySqlCommand usages in Data/, spot-check 3 random DAO files
-   [ ] T083c [Validation] **SC-003: All 60+ Stored Procedures Tested** - Run all integration tests, generate SP coverage report, verify ≥60 procedures exercised with valid and invalid inputs
-   [ ] T083d [Validation] **SC-004: <5% Performance Variance** - Establish baseline (pre-refactor), run same operations on refactored codebase, verify all variances < 5%
-   [ ] T083e [Validation] **SC-005: Connection Pool Health** - Run connection pool tests (100 concurrent operations), monitor pool statistics, verify 5-100 range maintained, no timeouts, no leaks
-   [ ] T083f [Validation] **SC-006: Error Logging Without Recursion** - Force connection failure, simulate log_error table unavailability, verify fallback to file logging, no recursive exceptions
-   [ ] T083g [Validation] **SC-007: <15 Minute New Operation Time** - Follow quickstart.md guide to create test DAO method, measure total time (target: <15 minutes)
-   [ ] T083h [Validation] **SC-009: Transaction Rollback Works** - Run transaction rollback tests, manually test TransferInventoryAsync rollback, verify zero orphaned records
-   [ ] T083i [Validation] **SC-010: <3 Second Startup Validation** - Measure startup time (database available and unavailable), verify actionable error message, graceful termination, INFORMATION_SCHEMA cache query completes

#### Post-Deployment Monitoring (Track after release)

-   [ ] T083j [Validation] **SC-008: 90% Ticket Reduction Target** - Baseline: Query ticket system for past 3 months database tickets. Monitor: Track 30 days post-deployment, verify ≥90% reduction

-   [ ] T084 [Polish] Review all DAO files for region organization per constitution Principle III: #region Initialization, #region Public Methods, #region Protected Methods, #region Private Methods, #region Static Methods, #region Event Handlers, #region Properties, #region Fields, #region Dispose, #region Nested Types
-   [ ] T085 [Polish] Run static code analysis to verify 100% of database operations in Data/ folder route through Helper_Database_StoredProcedure (no direct MySqlConnection/MySqlCommand usage) per SC-002
-   [ ] T086 [Polish] Execute `Scripts/Validate-Parameter-Prefixes.ps1` final validation across all 60+ stored procedures, confirm 0 inconsistencies reported
-   [ ] T087 [Polish] Run quickstart.md validation: follow quickstart guide to create test DAO method, verify guide accurate and complete in <15 minutes per SC-007

**Checkpoint**: Complete manual validation and documentation before production deployment

---

## Dependencies & Execution Order

### Phase Dependencies

-   **Setup (Phase 1)**: No dependencies - can start immediately
-   **Foundational (Phase 2)**: Depends on Setup completion - **BLOCKS all user stories**
-   **User Stories (Phase 3-7)**: All depend on Foundational phase completion
    -   US1 (Phase 3): Establishes DAO pattern - **BLOCKS** US2 (US2 follows this pattern)
    -   US2 (Phase 4): Can parallel with US3/US4 after US1 pattern established
    -   US3 (Phase 5): **BLOCKS** many T046 subtasks (Dao_User UI settings methods needed)
    -   US4 (Phase 6): Can parallel with US2/US3
    -   US5 (Phase 7): Depends on all DAOs refactored
-   **Polish (Phase 8)**: Depends on all user stories complete

### Critical Blocking Dependencies

**T033a-y (Dao_User UI Settings Methods) BLOCKS**:

-   T046g (Control_Shortcuts) - needs T033w, T033x
-   T046h (Control_Theme) - needs T033e, T033y
-   T046k (Control_TransferTab) - needs T033r
-   T046l (Control_RemoveTab) - needs T033r
-   T046s (MainForm.cs) - needs T033r
-   T046t (MainFormUserSettingsHelper) - needs T033a-y (all 25 methods)
-   T046u (Service_OnStartup) - needs T033f

**T026d (Dao_History.AddTransactionHistoryAsync) BLOCKS**:

-   T046k-7, T046k-8 (Control_TransferTab error handling verification)

### Parallel Opportunities

**Phase 1** (Setup): All 4 tasks can parallel  
**Phase 2** (Foundational): Sequential execution required (T005-T008 must complete first)  
**Phase 3** (US1): Tests parallel (T014-T016), then DAOs parallel (T017-T018)  
**Phase 4** (US2): Tests parallel (T021-T023), DAOs parallel (T024-T026), Controls parallel (T027-T028)  
**Phase 5** (US3): Tests parallel (T030-T032), can fully parallel with Phase 4 if multiple developers  
**Phase 6** (US4): Tests parallel (T039-T041), DAOs parallel (T042-T045), Controls parallel (T046a-u where not blocked)  
**Phase 7** (US5): Tests parallel (T050-T052), implementation tasks highly parallelizable  
**Phase 8** (Polish): Documentation parallel (T080-T082), validation sequential (T083-T087)

---

## Implementation Strategy

### MVP First (Phases 1-4 Complete)

**Delivers**: Core database layer reliability with System, ErrorLog, Inventory, Transactions, History DAOs

**Success Criteria Met**:

-   SC-001: Zero parameter errors ✅
-   SC-002: 100% Helper routing ✅ (for MVP DAOs)
-   SC-005: Connection pool healthy ✅
-   SC-009: Transaction rollback working ✅
-   SC-010: Startup validation working ✅

**Time Estimate**: 8-10 days (single developer) or 4-5 days (3 developers parallel)

### Incremental Delivery

1. **Foundation** (Phases 1-2) → 2-3 days → Model_Dao_Result pattern, Helper refactored
2. **MVP** (Phases 3-4) → 4-5 days → Core operations reliable (System, ErrorLog, Inventory, Transactions, History)
3. **Enhanced Logging** (Phase 5) → 3-4 days → User/Part DAOs, Service_DebugTracer, error cooldown
4. **Schema Consistency** (Phase 6) → 3-4 days → Location/Operation/ItemType/QuickButtons DAOs, parameter validation
5. **Performance** (Phase 7) → 2-3 days → Monitoring dashboard, remaining Forms/Controls async
6. **Polish** (Phase 8) → 2-3 days → Documentation, validation, cleanup

**Total Time**: 16-21 days (single developer) or 10-12 days (3 developers parallel after Foundation)

### Completion Status Summary

**Phases Complete**: 1 ✅, 2 ✅, 3 ✅, 4 ✅  
**Phase In Progress**: 5 ⚠️ (T033a-y blocking 47 subtasks), 6 ⚠️ (T033a-y blocking)  
**Phases Not Started**: 7 ❌, 8 ❌

**Total Tasks**: 87 main tasks + 200+ method-level subtasks  
**Tasks Complete**: ~45% (foundation + core DAOs + history/transactions + controls)  
**Critical Blockers**: T033a-y (25 Dao_User UI settings methods)

**Recent Completions (2025-10-14)**:

-   ✅ T018a-f: Dao_ErrorLog methods now return Model_Dao_Result types (test updates pending)
-   ✅ T026d: Dao_History.AddTransactionHistoryAsync returns Model_Dao_Result, all callers updated
-   ✅ Phase 3 and Phase 4 checkpoints reached

---

## Service_DebugTracer Integration Tracking

**Per FR-012**: All DAO methods must have TraceMethodEntry/TraceMethodExit calls for debugging visibility.

**Integration Approach**: Add Service_DebugTracer calls **during DAO refactoring** (not as separate retrofit task).

### DAO Methods Requiring Service_DebugTracer

**Phase 3 (US1)** - 6 methods:

-   [x] Dao_System.cs (5 methods) - ✅ Integrated during T017
-   [ ] Dao_ErrorLog.cs (6+ methods) - ⚠️ Partial (T018a-f incomplete)

**Phase 4 (US2)** - 15 methods:

-   [x] Dao_Inventory.cs (6 methods) - ✅ Integrated during T024
-   [x] Dao_Transactions.cs (3 methods) - ✅ Integrated during T025
-   [ ] Dao_History.cs (4 methods) - ⚠️ Partial (T026d incomplete)

**Phase 5 (US3)** - 10 methods:

-   [ ] Dao_User.cs (5 core + 25 UI settings) - ⚠️ Partial (T033a-y incomplete)
-   [x] Dao_Part.cs (7 methods) - ✅ Integrated during T034

**Phase 6 (US4)** - 27 methods:

-   [x] Dao_Location.cs (6 methods) - ✅ Integrated during T042
-   [x] Dao_Operation.cs (6 methods) - ✅ Integrated during T043
-   [x] Dao_ItemType.cs (6 methods) - ✅ Integrated during T044
-   [x] Dao_QuickButtons.cs (8 methods) - ✅ Integrated during T045

**Total**: 40+ DAO methods × 2 trace calls (entry/exit) = 80+ trace points required

**Status**: ~60% complete (methods in completed DAOs have tracing)

### Sensitive Data Handling in Traces

**Never log**:

-   Passwords (use username only in AuthenticateUserAsync)
-   Connection strings (log server address only)
-   Personal identifying information beyond UserID

**Safe parameter logging**:

```csharp
Service_DebugTracer.TraceMethodEntry(nameof(AuthenticateUserAsync), new { username }); // ✅ Safe
Service_DebugTracer.TraceMethodEntry(nameof(GetInventoryAsync), new { locationCode, includeInactive }); // ✅ Safe
```

---

## Scope Options Summary

### Option A: MainForm Tab Controls (MVP - 6 controls)

**Tasks T055-T060**: High-traffic manufacturing operations  
**Rationale**: Core workflows (inventory add/remove/transfer) - maximum impact  
**Effort**: 2-3 days

### Option B: Settings Controls (Comprehensive - 18 controls)

**Tasks T061-T076**: Complete settings management async migration  
**Rationale**: Predictable CRUD patterns, all DAOs already refactored  
**Effort**: 3-4 days (highly parallelizable)

**Recommendation**: Complete Option A first (MVP), then Option B (comprehensive coverage)

---

## Notes

-   **[P] tasks**: Different files, no dependencies, can execute in parallel
-   **[Story] label**: Maps task to specific user story (US1-US5) for traceability
-   **Method-Level Subtasks**: Each method refactoring tracked individually for precise progress tracking
-   **Each user story independently testable**: Can validate at each checkpoint
-   **Tests written FIRST**: Ensure tests FAIL before implementation (TDD approach)
-   **Commit strategy**: Atomic commits by DAO file or Form file to enable easy rollback
-   **Critical gate**: Phase 2 completion BLOCKS all DAO work
-   **Critical blockers**: T033a-y must complete before 47 control subtasks can proceed

---

## File Structure Changes from Original

**Original**: 468 lines, monolithic task descriptions  
**Refactored**: ~540 lines, method-level granularity

**Key Improvements**:

1. ✅ **Method-level subtasks**: Every method tracked individually (T###a, T###b pattern)
2. ✅ **Completion status**: Each subtask marked complete/incomplete/blocked
3. ✅ **Blocking dependencies**: Clear visibility which tasks block others
4. ✅ **Incorporated validation checklist**: T083a-j now includes SC-001 through SC-010
5. ✅ **Incorporated Service_DebugTracer tracking**: Integration status by phase
6. ✅ **Incorporated T046 detailed plan**: 47 subtasks with line numbers and specific updates
7. ✅ **One file structure**: All information consolidated from 4 separate documents

**Trade-off**: Increased line count for better tracking granularity and progress visibility

---

**Last Updated**: 2025-10-14  
**Total Line Count**: Check with `(Get-Content tasks.md | Measure-Object -Line).Lines`  
**Original Line Count**: 468 lines  
**Expected New Count**: ~540 lines (15% increase for 300%+ granularity improvement)
