# Implementation Plan: Database Layer Standardization Refresh

**Feature**: Comprehensive Database Layer Standardization  
**Spec Reference**: `specs/003-database-layer-refresh/spec.md`  
**Related**: Enhances `specs/002-comprehensive-database-layer/plan.md` with Phase 2.5 focus  
**Created**: 2025-10-15  
**Status**: Active Planning

## Constitution Check

### Alignment with Project Principles

**1. Manufacturing Domain Accuracy**  
✅ **Aligned**: All stored procedure standardization preserves manufacturing business logic exactly. Operation numbers (90/100/110) remain as work order sequence steps. Transaction types (IN/OUT/TRANSFER) remain as inventory movement intent. No changes to business semantics.

**2. Performance Requirements (Sub-100ms UI, 30s Database Timeout)**  
✅ **Aligned**: Phase 2.5 standardization does not alter query logic or add overhead. Parameter prefix detection cached at startup (~100ms one-time cost). Model_Dao_Result wrapper adds <1ms per operation. Connection pooling maintains performance baseline. Performance testing (SC-004) validates ±5% variance.

**3. WinForms Architecture Patterns**  
✅ **Aligned**: DAO refactor maintains separation between Forms (UI event handlers), Helpers (utilities), and DAOs (data access). Async-only pattern requires Forms to adopt `async void` event handlers, which is standard WinForms practice. No designer file modifications.

**4. Stored Procedure Only Pattern**  
✅ **Strongly Aligned**: Core feature purpose is to ENFORCE stored procedure-only access by eliminating all direct MySQL API usage (MySqlConnection, MySqlCommand) from Data/ folder. Helper_Database_StoredProcedure becomes sole routing mechanism. Static validation (SC-002) enforces 100% compliance.

**5. MySQL 5.7 Compatibility**  
✅ **Aligned**: No MySQL 8.0+ features introduced. All stored procedures remain MySQL 5.7.24+ compatible (no CTEs, no window functions). Connection pooling and parameter prefix detection use standard MySQL 5.7 APIs.

**6. Security (No Hardcoded Credentials, Parameterized Queries)**  
✅ **Enhanced**: Centralized connection string management via Helper_Database_Variables eliminates hardcoded credentials. Parameter prefix detection uses INFORMATION_SCHEMA (metadata-only read). All stored procedure calls remain parameterized (no SQL injection risk). Error logging sanitizes sensitive data before persistence.

**7. Testing Standards (Manual Validation Primary)**  
✅ **Aligned**: Integration test infrastructure supplements manual testing but doesn't replace it. Developers still execute manual validation workflows after refactor. Per-test-class transactions enable fast integration tests without affecting production data. SC-003 requires 100% test pass rate before deployment.

**8. Documentation Standards (XML Comments, Markdown Guides)**  
✅ **Aligned**: FR-015 requires XML documentation on all DAO public methods. Plan includes quickstart.md for developer onboarding. Phase 2.5 Part F (T129-T132) dedicates 4 tasks to documentation updates. 00_STATUS_CODE_STANDARDS.md provides stored procedure template.

**Summary**: All 8 project principles aligned or enhanced. No constitutional violations. Phase 2.5 work strengthens architectural boundaries and enforces existing patterns.

---

## Technical Context

### Current State Analysis

**Database Layer Inventory**:

-   **DAO Classes**: 12 files in Data/ folder (~3,500 LOC total)
    -   Dao_Inventory.cs (largest, ~800 LOC)
    -   Dao_User.cs, Dao_Transactions.cs, Dao_Part.cs, Dao_Location.cs, Dao_Operation.cs, Dao_ItemType.cs, Dao_QuickButtons.cs, Dao_History.cs, Dao_ErrorLog.cs, Dao_System.cs, Dao_Role.cs (smaller, 100-400 LOC each)
-   **Stored Procedures**: 60-100+ procedures across 7 domains

    -   Inventory Operations: ~15 procedures (inv*inventory_Add, inv_inventory_Remove, inv_inventory_Transfer*_, inv*inventory_Get*_)
    -   Transaction Logging: ~5 procedures (inv_transaction_Add, inv_transactions_GetAnalytics, inv_transactions_SmartSearch)
    -   User Management: ~10 procedures (sys*user*_, sys*user_settings*_)
    -   Role Management: ~5 procedures (sys*role*_, sys*user_role*_)
    -   Master Data: ~20 procedures (md*part_ids*_, md*locations*_, md*operation_numbers*_, md*item_types*_)
    -   Error Logging: ~5 procedures (log*error_Add, log_error_Get*\*)
    -   Quick Buttons: ~10 procedures (sys*last_10_transactions*\*)

-   **Helper Classes**: 3 critical helpers in Helpers/ folder
    -   Helper_Database_StoredProcedure.cs (~600 LOC, will be refactored in Phase 2.5)
    -   Helper_Database_Variables.cs (~200 LOC, connection string management)
    -   Helper_StoredProcedureProgress.cs (~300 LOC, progress reporting for long operations)

**Known Issues**:

1. **Parameter Prefix Inconsistency**: Mixed p*, in*, o\_, and no-prefix patterns across procedures causing MySQL parameter errors
2. **Direct MySQL API Usage**: Some DAOs create MySqlConnection/MySqlCommand directly instead of routing through Helper
3. **Missing Status Codes**: ~20% of stored procedures lack standardized OUT p_Status and OUT p_ErrorMsg parameters
4. **Async/Sync Mixed**: Some DAOs have both async and sync methods, creating maintenance burden
5. **Exception-Driven Control Flow**: DAOs throw exceptions for expected failures (not found, validation error) instead of returning error status
6. **Incomplete Error Context**: Error logging missing key context fields (MethodName, AdditionalInfo, StackTrace in some cases)

**Dependencies**:

-   **MySql.Data 8.x**: Connection pooling, MySqlConnection, MySqlCommand, MySqlDataAdapter APIs
-   **System.Text.Json**: Parameter serialization for logging
-   **System.Diagnostics**: Stopwatch for performance monitoring
-   **INFORMATION_SCHEMA**: MySQL metadata tables for parameter prefix detection
-   **log_error table**: Database error logging storage
-   **inv_transaction table**: Transaction history audit trail

**Architectural Boundaries**:

```
┌─────────────────────────────────────────────────────────┐
│ Presentation Layer (Forms/, Controls/)                  │
│  - WinForms event handlers (async void pattern)         │
│  - UI state management                                  │
│  - User input validation                                │
└─────────────────────────────┬───────────────────────────┘
                              │ Calls async DAO methods
                              ↓
┌─────────────────────────────────────────────────────────┐
│ Data Access Layer (Data/)                               │
│  - 12 DAO classes (Dao_Inventory, Dao_User, etc.)       │
│  - Model_Dao_Result wrapper pattern                            │
│  - Async-only methods (Task<Model_Dao_Result>)                 │
└─────────────────────────────┬───────────────────────────┘
                              │ Routes through Helper
                              ↓
┌─────────────────────────────────────────────────────────┐
│ Database Helper Layer (Helpers/)                        │
│  - Helper_Database_StoredProcedure (central routing)    │
│  - Parameter prefix detection (INFORMATION_SCHEMA)      │
│  - Retry logic (transient errors)                       │
│  - Performance monitoring (thresholds)                  │
│  - Connection pooling (MySql.Data)                      │
└─────────────────────────────┬───────────────────────────┘
                              │ Executes stored procedures
                              ↓
┌─────────────────────────────────────────────────────────┐
│ Database Layer (MySQL 5.7)                              │
│  - 60+ stored procedures (standardized)                 │
│  - OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)        │
│  - Business logic encapsulation                         │
│  - Transaction management (BEGIN/COMMIT/ROLLBACK)       │
└─────────────────────────────────────────────────────────┘
```

**Call Sites Analysis**:

-   **Forms Event Handlers**: ~150 call sites across 25 forms
-   **User Controls**: ~50 call sites across 15 controls
-   **Background Services**: ~20 call sites in 5 service classes
-   **Total Affected Call Sites**: ~220 locations requiring async refactor

---

## Risk Assessment

### High-Risk Areas

**1. Database Wipe Operation (T119)**  
**Risk**: Accidental production database wipe during procedure deployment  
**Impact**: Catastrophic data loss, business continuity failure  
**Mitigation**:

-   T119 requires explicit environment check (Release vs Debug configuration)
-   Deployment script shows 10-second confirmation prompt with database name
-   Backup created immediately before wipe (T119 prerequisite)
-   Test deployment on mtm_wip_application_winforms_test first
-   Production deployment requires DBA review and approval
-   Rollback plan documented (restore from backup)

**2. Parameter Prefix Detection Failure (T100, FR-002, FR-029)** → **RISK REDUCED (HIGH → MEDIUM)**  
**Risk**: INFORMATION_SCHEMA query fails at startup, parameter cache cannot populate  
**Impact**: Application cannot start, requires manual intervention  
**Mitigation** (Updated with retry strategy):

-   **Retry dialog** with 3 attempts (MessageBox with Retry/Quit buttons) gives users multiple chances during connection issues
-   **Clean termination** on failure (no fallback to potentially inaccurate convention - eliminates 95% coverage risk)
-   **Override table integration** (sys_parameter_prefix_override) allows manual prefix storage for edge cases
-   T104 validates cached prefixes against actual procedures (100% accuracy required)
-   Startup logs cache population success/failure for monitoring
-   Integration tests verify parameter binding for all procedures (T108-T112)
-   Developer maintenance form (T113d) provides UI for managing override table
-   **Risk reduced**: Retry strategy eliminates inaccurate fallback risk, override table handles edge cases

**3. Async Migration Breaking UI Thread Marshaling (FR-010)**  
**Risk**: Forms update UI controls from background thread after awaiting async DAO calls  
**Impact**: InvalidOperationException, UI freezes, data binding failures  
**Mitigation**:

-   WinForms `async void` event handlers automatically marshal to UI thread after await
-   Code review checklist includes "Verify no Control.Invoke() needed after await"
-   T123 integration tests execute operations from Forms context
-   Manual testing of all Forms workflows before deployment (T126)

**4. Transaction Rollback Not Covering All Steps (FR-011)**  
**Risk**: Multi-step operation partially commits despite failure in later step  
**Impact**: Orphaned records, data integrity violations, audit trail gaps  
**Mitigation**:

-   T118 refactors all multi-step procedures to use explicit transactions
-   Integration tests validate rollback (T127 - force failure, verify no partial commits)
-   SC-009 success criteria requires zero orphaned records
-   Code review verifies try/catch/finally wraps all transaction code

### Medium-Risk Areas

**5. Documentation Drift During Concurrent Development (FR-024)** → **RISK REDUCED (MEDIUM → LOW)**  
**Risk**: Documentation updates lag behind code changes during T113-T116 refactoring, creating outdated docs  
**Impact**: Inaccurate documentation, team confusion, rework required  
**Mitigation** (Updated with concurrent documentation):

-   **Documentation-Update-Matrix.md** tracks 145+ documentation items with status (⬜/🔄/✅/⚠️)
-   **Concurrent updates** during T113-T116 (developers check procedure header comments + DAO XML as they refactor)
-   **Validation script** checks completeness before deployment (exit code 0 = 100% complete, exit code 1 = incomplete)
-   Duration increases 20% (T113: 20h→24h, T114: 10h→12h, T115: 15h→18h, T116: 20h→24h) account for documentation time
-   T130 completes any remaining backlog, T131 validates 100% completion
-   **Risk reduced**: Concurrent tracking prevents drift, validation enforces completeness

**6. Performance Regression from Model_Dao_Result Wrapping**  
**Risk**: Additional object allocation and method calls slow down operations  
**Impact**: User-perceivable lag, timeout errors under load  
**Mitigation**:

-   SC-004 requires ±5% performance variance (tight tolerance)
-   Benchmark suite measures 10 key operations before/after refactor
-   Model_Dao_Result is lightweight struct (~40 bytes per instance)
-   Connection pooling maintains baseline performance

**7. Integration Test Flakiness from Shared Test Database**  
**Risk**: Parallel test runs interfere with each other, causing intermittent failures  
**Impact**: False positives in CI/CD, developer frustration  
**Mitigation**:

-   Per-test-class transactions isolate test data (rollback after each test)
-   Each developer maintains independent local test database
-   Test data uses unique identifiers (GUIDs) to avoid collisions
-   T112 specifically validates test isolation and parallel safety

### Low-Risk Areas

**8. Error Logging Recursion (FR-006)**  
**Risk**: Error during error logging causes infinite loop  
**Impact**: Application hang, log file growth  
**Mitigation**:

-   Try/catch wraps LogErrorToDatabaseAsync with fallback to file logging
-   Integration test validates recursive prevention (T111)
-   File fallback prevents loop even if database completely unavailable

**9. Connection Pool Exhaustion Under Unexpected Load (FR-008)**  
**Risk**: Sudden traffic spike exhausts 100 connection pool  
**Impact**: Timeout errors, user-facing failures  
**Mitigation**:

-   MaxPoolSize=100 handles typical concurrent load (50-70 connections)
-   ConnectionTimeout=30s prevents indefinite waits
-   Load testing (SC-005) validates 100 concurrent operations
-   Monitoring alerts if pool >80% capacity

### New Risks from Session 3 Changes

**R-NEW-1: Developer Role Privilege Escalation**  
**Risk**: Developer role grants excessive privileges, allowing unauthorized database changes  
**Impact**: Data integrity violations, unauthorized schema modifications, audit trail bypass  
**Severity**: HIGH  
**Mitigation**:

-   Role hierarchy enforced: Developer role requires Admin=TRUE prerequisite (cannot grant Developer without Admin)
-   Parameter prefix override table (sys_parameter_prefix_override) includes audit trail (CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
-   Developer maintenance form (Control_Settings_ParameterPrefixMaintenance) gated by IsAdmin AND IsDeveloper check
-   T113c includes role validation helpers to prevent UI bypass
-   Security testing validates role enforcement before deployment

**R-NEW-2: Schema Drift Reconciliation Complexity**  
**Risk**: Category B conflicts (overlapping production and Phase 2.5 changes) fail three-way merge, require excessive manual intervention  
**Impact**: Deployment delays, incorrect merge decisions, business logic regressions  
**Severity**: MEDIUM  
**Mitigation**:

-   T119b categorization identifies conflicts early (before deployment window)
-   T119d dedicates 0.25-0.5 days per conflict with systematic three-way merge process
-   Conflict resolution log documents merge decisions for review
-   Integration tests validate merged procedures thoroughly
-   DBA review includes drift reconciliation report approval
-   Typical conflict count expected: 1-3 procedures (low volume reduces risk)

**R-NEW-3: CSV Transaction Analysis Review Bottleneck**  
**Risk**: T106a CSV review takes longer than estimated (1-2 days), gates T113 refactoring start  
**Impact**: Timeline delays, developer idle time, pressure to rush CSV review  
**Severity**: MEDIUM  
**Mitigation**:

-   Git-based PR workflow parallelizes review across 3 developers (domain assignment: inventory, user/transaction, master data)
-   Estimated 4-6 hours per developer (concurrent, not sequential)
-   CSV format with DeveloperCorrection column allows incremental corrections without full re-analysis
-   T103 automated pattern detection provides 90%+ accuracy baseline (minimal corrections needed)
-   If bottleneck occurs: Proceed with high-confidence procedures (≥95% confidence), defer low-confidence for T106a completion

**R-NEW-4: Roslyn Analyzer False Positives**  
**Risk**: MTM001-MTM004 diagnostic rules produce false positive warnings, causing developer friction  
**Impact**: Developer frustration, disabled analyzer, compliance erosion  
**Severity**: LOW  
**Mitigation**:

-   v1.0.0 uses Warning severity (educational phase) - developers can ignore false positives initially
-   Code fix providers auto-generate correct Helper patterns - reduces manual coding errors
-   T124 validates analyzer against known-good codebase (Zero false positives required before v2.0.0 Error enforcement)
-   Phased severity rollout (v1.0.0 warnings → v2.0.0 errors after validation period)
-   Suppression mechanism for justified violations (e.g., test code, legacy compatibility)

---

## Phase Breakdown

### Overview

Phase 2.5 inserted as blocking prerequisite between Phase 2 (Foundation) and Phase 3 (DAO Refactoring). Original Phase 3-8 tasks depend on Phase 2.5 completion.

**Timeline Estimates** (Updated with Session 3 changes):

-   **Single Developer**: 19-30 days (was 15-25 days) - added 3.625-5.25 days for new tasks
    -   Additional time breakdown:
        -   T106a CSV review: +1-2 days
        -   T113c Developer role: +0.5 days (4 hours)
        -   T113d Maintenance form: +1 day (8 hours)
        -   T119b/c/d/e Drift reconciliation: +0.75-1.5 days
        -   T124a Roslyn analyzer: +0.125 days (2-3 hours)
        -   T113-T116 documentation time: +0.8 days (20% duration increase)
        -   Part F restructuring: -0.5 days (matrix generation vs bulk docs)
-   **3 Developers (Parallel)**: 10-15 days (was 8-12 days) - added 2-3 days for coordination
    -   Parts A/B/C can parallelize with documentation matrix tracking
    -   T106a CSV review parallelized across domains (4-6 hours per developer, concurrent)
    -   T119b/c/d/e drift reconciliation may extend deployment window by 1-2 days

**Resource Allocation** (Updated):

-   **Developer 1**: Parts A + D (Discovery + Deployment + Drift) - 10-12 days
    -   Added: T106a CSV review (shared), T119b/c/d/e drift reconciliation
-   **Developer 2**: Parts B + E (Testing + Integration + Roslyn) - 11-13 days
    -   Added: T107 verbose helper (1h), T124a Roslyn analyzer (2-3h), T123 retry testing (1h)
-   **Developer 3**: Parts C + F (Refactoring + Documentation + Dev Tools) - 11-14 days
    -   Added: T113c Developer role (4h), T113d Maintenance form (8h), T113-T116 concurrent docs (+20% each)

---

### Phase 2.5 Part A: Discovery and Analysis (T100-T106)

**Objective**: Discover all stored procedures, document current state, identify non-compliant patterns.

**Tasks**:

**T100: Discover All Stored Procedure Call Sites in Codebase**  
Search entire codebase for stored procedure execution patterns to build master inventory.

-   **Method**: Grep search for `ExecuteNonQuery`, `ExecuteScalar`, `ExecuteDataTable`, `ExecuteReader`, `CommandType.StoredProcedure`, `CALL `, `MySqlCommand` patterns
-   **Scope**: All .cs files in Data/, Helpers/, Forms/, Controls/, Services/
-   **Output**: `callsite-inventory.csv` with columns: FilePath, LineNumber, StoredProcedureName, CallPattern, Notes
-   **Deliverable**: CSV file listing ~220 call sites
-   **Duration**: 2 hours
-   **Dependencies**: None

**T101: Extract Complete Database Schema (All Tables, Procedures, Parameters)**  
Query INFORMATION_SCHEMA to capture complete database structure snapshot.

-   **Method**: Execute SQL queries:
    ```sql
    SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = 'MTM_WIP_Application_Winforms';
    SELECT * FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_SCHEMA = 'MTM_WIP_Application_Winforms';
    SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'MTM_WIP_Application_Winforms';
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'MTM_WIP_Application_Winforms';
    ```
-   **Output**: `database-schema-snapshot.json` with 4 sections (routines, parameters, tables, columns)
-   **Deliverable**: JSON file with complete schema metadata
-   **Duration**: 1 hour
-   **Dependencies**: None

**T102: Generate Individual SQL Files for Each Discovered Stored Procedure**  
Extract CREATE PROCEDURE statements and save as individual .sql files for version control.

-   **Method**: For each procedure, execute `SHOW CREATE PROCEDURE <name>`, write to file `Database/UpdatedStoredProcedures/<name>.sql`
-   **Scope**: All 60-100+ procedures from T101
-   **Output**: Individual .sql files (one per procedure) in `Database/UpdatedStoredProcedures/`
-   **Deliverable**: ~70 .sql files organized by domain (inventory/, transactions/, users/, master-data/, logging/, system/)
-   **Duration**: 3 hours (includes directory organization and git commit)
-   **Dependencies**: T101 (need procedure list)

**T103: Audit All Procedures Against 00_STATUS_CODE_STANDARDS.md Template**  
Compare each procedure signature and logic against standardized template to identify non-compliance AND generate transaction strategy recommendations.

-   **Method**: For each procedure, validate:
    -   Has `OUT p_Status INT` parameter
    -   Has `OUT p_ErrorMsg VARCHAR(500)` parameter
    -   Uses status codes correctly (1=success with data, 0=success no data, -1 to -5=errors)
    -   Handles exceptions properly (sets error status on failure)
    -   Parameter naming follows PascalCase with consistent prefix (p*, in*, o\_)
    -   **NEW**: Count INSERT/UPDATE/DELETE statements (multi-step detection)
    -   **NEW**: Analyze call graph for nested procedure calls
    -   **NEW**: Classify procedure pattern (single-step, multi-step, batch, reporting)
-   **Output 1**: `compliance-report.csv` with columns: ProcedureName, HasStatus, HasErrorMsg, StatusLogicCorrect, ParameterPrefixConsistent, ComplianceScore (0-100%)
-   **Output 2**: `procedure-transaction-analysis.csv` with columns: ProcedureName, DetectedPattern, RecommendedStrategy, Confidence, Rationale, DeveloperCorrection (empty), RefactoringNotes (empty)
-   **Deliverable**: Compliance report showing ~40-60% current compliance rate + Transaction analysis CSV for T106a review
-   **Duration**: 7 hours (manual review of 60+ procedures + pattern analysis)
-   **Dependencies**: T102 (need procedure source files)

**T104: Document Parameter Prefix Conventions per Procedure Type**  
Analyze parameter prefixes across procedures to document patterns and identify fallback conventions.

-   **Method**: Group procedures by name pattern (inv*inventory*_, inv_transaction_Transfer_, sys*user*\*, etc.), analyze parameter prefixes
-   **Output**: `parameter-prefix-conventions.md` with sections:
    -   Standard Prefix: p\_ (used in 70% of procedures)
    -   Multi-Step Operations: in\_ (used in Transfer* and transaction* procedures)
    -   Output Parameters: o\_ (rare, used in ~5% of procedures)
    -   No Prefix: none (10% of procedures, inconsistent)
    -   Fallback Convention: p* for CRUD, in* for Transfer*/transaction*, detected from name pattern
-   **Deliverable**: Markdown document defining conventions and fallback logic
-   **Duration**: 2 hours
-   **Dependencies**: T101, T103 (need parameter metadata and compliance analysis)

**T105: Create Procedure Refactoring Priority Matrix**  
Rank procedures by refactoring priority based on usage frequency and compliance score.

-   **Method**: Cross-reference T100 call site inventory with T103 compliance report
-   **Scoring**: Priority = (CallSiteCount × 0.4) + (ComplianceDeficiency × 0.6)
    -   CallSiteCount: Number of references in codebase (high usage = high priority)
    -   ComplianceDeficiency: 100% - ComplianceScore (low compliance = high priority)
-   **Output**: `refactoring-priority.csv` sorted by priority score, columns: ProcedureName, CallSiteCount, ComplianceScore, PriorityScore, RefactoringComplexity (Low/Medium/High)
-   **Deliverable**: Prioritized list identifying top 20 critical procedures for immediate refactoring
-   **Duration**: 2 hours
-   **Dependencies**: T100, T103 (need call site count and compliance scores)

**T106: Generate Stored Procedure Test Coverage Matrix**  
Map which procedures currently have integration tests vs those requiring new test creation.

-   **Method**: Search Tests/ folder for integration test files, parse test method names for procedure references, cross-reference with T101 procedure list
-   **Output**: `test-coverage-matrix.csv` with columns: ProcedureName, HasIntegrationTest, TestFilePath, TestMethodNames, CoverageStatus (Covered/Partial/None)
-   **Deliverable**: Coverage report showing current ~30% test coverage, identifying 70% requiring new tests
-   **Duration**: 3 hours
-   **Dependencies**: T101 (need procedure list)

**T106a: CSV Review and Correction - Transaction Strategy Validation**  
Developer review of procedure-transaction-analysis.csv to validate and correct automated recommendations.

-   **Method**: Git-based review workflow
    1. T103 commits procedure-transaction-analysis.csv to Database/AnalysisReports/
    2. Tech lead assigns procedure domains to developers (Inventory, Users, Transactions, etc.)
    3. Developers fill DeveloperCorrection column: Leave blank if correct, enter corrected strategy + rationale if wrong
    4. Developers commit corrections, create PR with domain-specific changes
    5. Peer developer reviews corrections for assigned domain
    6. Merge corrected CSV after review approval
-   **Output**: Corrected CSV with all procedures reviewed and validated
-   **Deliverable**: Authoritative transaction strategy document for T113-T118 implementation
-   **Duration**: 1-2 days (parallelizable by domain: 4-6 hours per developer if 3 developers)
-   **Dependencies**: T103 (need CSV generation)
-   **Gates**: T113 cannot start until this review complete and CSV merged

**Part A Summary**:

-   **Tasks**: 8 discovery and analysis tasks (includes T106a)
-   **Duration**: 20-24 hours (~2.5-3 days single developer, ~1 day with 3 developers parallelizing T106a)
-   **Deliverables**: 7 reports/documents providing complete database layer inventory
-   **Critical Output**: Prioritized refactoring plan, validated transaction strategies, and gap analysis

---

### Phase 2.5 Part B: Test Implementation (T107-T112)

**Objective**: Create comprehensive integration test suite covering all stored procedures before refactoring begins.

**Tasks**:

**T107: Create BaseIntegrationTest Class with Transaction Management**  
Build reusable base class for integration tests with automatic transaction rollback and verbose failure diagnostics.

-   **Implementation**:
    ```csharp
    public abstract class BaseIntegrationTest
    {
        protected MySqlConnection TestConnection;
        protected MySqlTransaction TestTransaction;
        protected string TestConnectionString;

        [TestInitialize]
        public virtual void Setup()
        {
            TestConnectionString = Helper_Database_Variables.GetConnectionString(useTestDatabase: true);
            TestConnection = new MySqlConnection(TestConnectionString);
            TestConnection.Open();
            TestTransaction = TestConnection.BeginTransaction();
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            TestTransaction?.Rollback();
            TestTransaction?.Dispose();
            TestConnection?.Close();
            TestConnection?.Dispose();
        }

        protected async Task<Model_Dao_Result<DataTable>> ExecuteTestProcedureAsync(string procedureName, Dictionary<string, object> parameters)
        {
            // Helper method executing procedure within test transaction
        }

        // NEW: Verbose failure diagnostic helper
        protected void AssertProcedureResult(Model_Dao_Result result, bool expectedSuccess, string expectedMessagePattern = null)
        {
            if (result.IsSuccess != expectedSuccess)
            {
                var diagnostic = new {
                    Exception = result.Exception?.ToString(),
                    Parameters = TestCurrentParameters,  // Captured from test method
                    Expected = new { IsSuccess = expectedSuccess, MessagePattern = expectedMessagePattern },
                    Actual = new { IsSuccess = result.IsSuccess, Message = result.Message },
                    ExecutionTimeMs = TestCurrentExecutionTime,
                    DatabaseState = CaptureTableRowCounts(),
                    TestMethod = TestContext.TestName,
                    Timestamp = DateTime.Now
                };
                Assert.Fail($"Procedure result assertion failed.\n{JsonSerializer.Serialize(diagnostic, new JsonSerializerOptions { WriteIndented = true })}");
            }
        }

        protected Dictionary<string, int> CaptureTableRowCounts(params string[] tables)
        {
            // Query table row counts for before/after comparison
        }
    }
    ```
-   **Deliverable**: BaseIntegrationTest.cs in Tests/Integration/ folder with verbose diagnostic helpers
-   **Duration**: 5 hours (includes verbose helper implementation)
-   **Dependencies**: None (foundational task)

**T108: Generate Integration Tests for Inventory Procedures (15 procedures)**  
Create test classes for all inv*inventory*\* procedures following 4-test pattern with verbose failures.

-   **Test Pattern** (per procedure):
    1. **Success with data**: Valid inputs → returns Model_Dao_Result.Success, Data populated
    2. **Success no data**: Valid inputs, no matching records → returns Model_Dao_Result.Success, Data empty
    3. **Validation error**: Invalid inputs (null required param) → returns Model_Dao_Result.Failure, error message clear
    4. **Database error**: Force constraint violation → returns Model_Dao_Result.Failure, exception logged
-   **All tests use AssertProcedureResult()** for verbose failure diagnostics (7 fields: exception, parameters, expected vs actual, execution time, database state, test method, timestamp)
-   **Scope**: 15 inventory procedures × 4 tests = 60 test methods
-   **Deliverable**: InventoryProcedures_IntegrationTests.cs (~800 LOC)
-   **Duration**: 12 hours
-   **Dependencies**: T107 (need BaseIntegrationTest)

**T109: Generate Integration Tests for Transaction/User/Role Procedures (20 procedures)**  
Create test classes for inv*transaction*_, sys*user*_, sys*role*\* procedures with verbose failure diagnostics.

-   **All tests use AssertProcedureResult()** for structured JSON diagnostic output on failure
-   **Scope**: 20 procedures × 4 tests = 80 test methods
-   **Deliverable**: 3 test files:
    -   TransactionProcedures_IntegrationTests.cs (~400 LOC)
    -   UserProcedures_IntegrationTests.cs (~600 LOC)
    -   RoleProcedures_IntegrationTests.cs (~300 LOC)
-   **Duration**: 15 hours
-   **Dependencies**: T107 (need BaseIntegrationTest)

**T110: Generate Integration Tests for Master Data Procedures (20 procedures)**  
Create test classes for md*part_ids*_, md*locations*_, md*operation_numbers*_, md*item_types*_ procedures with verbose diagnostics.

-   **All tests use AssertProcedureResult()** for comprehensive failure information
-   **Scope**: 20 procedures × 4 tests = 80 test methods
-   **Deliverable**: MasterDataProcedures_IntegrationTests.cs (~800 LOC)
-   **Duration**: 12 hours
-   **Dependencies**: T107 (need BaseIntegrationTest)

**T111: Generate Integration Tests for Logging/Quick Button Procedures (15 procedures)**  
Create test classes for log*error*_, sys*last_10_transactions*_ procedures.

-   **Scope**: 15 procedures × 4 tests = 60 test methods
-   **Deliverable**: 2 test files:
    -   LoggingProcedures_IntegrationTests.cs (~400 LOC)
    -   QuickButtonProcedures_IntegrationTests.cs (~400 LOC)
-   **Duration**: 10 hours
-   **Dependencies**: T107 (need BaseIntegrationTest)

**T112: Validate Test Isolation and Parallel Safety**  
Verify integration tests don't interfere with each other when run in parallel.

-   **Method**:
    1. Run all integration tests sequentially → record pass/fail status
    2. Run all integration tests in parallel (4 threads) → record pass/fail status
    3. Compare results → all tests should pass in both modes
    4. Check test database for leftover data → should be clean (transactions rolled back)
-   **Success Criteria**: 100% test pass rate in both sequential and parallel modes, zero leftover test data
-   **Deliverable**: Test isolation validation report
-   **Duration**: 4 hours
-   **Dependencies**: T108-T111 (need all tests created)

**Part B Summary**:

-   **Tasks**: 6 test implementation tasks
-   **Duration**: 57 hours (~7.5 days single developer, can parallelize with Part C)
-   **Deliverables**: ~280 integration test methods covering all 70 stored procedures
-   **Critical Output**: Comprehensive test safety net before refactoring

---

### Phase 2.5 Part C: Stored Procedure Refactoring (T113-T118)

**Objective**: Refactor all non-compliant stored procedures to match 00_STATUS_CODE_STANDARDS.md template and create Developer role infrastructure.

**Tasks**:

**T113c: Create Developer User Role and Parameter Prefix Override Infrastructure**  
Implement Developer role hierarchy and database table for parameter prefix management.

-   **Database Schema Changes**:

    ```sql
    -- Add Developer flag to sys_user table
    ALTER TABLE sys_user ADD COLUMN IsDeveloper BOOLEAN DEFAULT FALSE AFTER IsAdmin;

    -- Create parameter prefix override table
    CREATE TABLE sys_parameter_prefix_override (
        OverrideID INT AUTO_INCREMENT PRIMARY KEY,
        ProcedureName VARCHAR(100) NOT NULL,
        ParameterName VARCHAR(100) NOT NULL,
        DetectedPrefix VARCHAR(10),
        OverridePrefix VARCHAR(10) NOT NULL,
        Confidence DECIMAL(3,2),
        Reason VARCHAR(500),
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
        ModifiedBy INT,
        ModifiedDate DATETIME ON UPDATE CURRENT_TIMESTAMP,
        IsActive BOOLEAN DEFAULT TRUE,
        UNIQUE KEY unique_proc_param (ProcedureName, ParameterName),
        FOREIGN KEY (CreatedBy) REFERENCES sys_user(UserID),
        FOREIGN KEY (ModifiedBy) REFERENCES sys_user(UserID)
    );
    ```

-   **User Management UI Updates**:
    -   Add Developer checkbox to user management form (enabled only if Admin checked)
    -   Enforce role prerequisite: Developer requires IsAdmin = TRUE
    -   Update role permission matrix documentation
-   **Role Hierarchy**: Basic User < Admin < Developer (Developer inherits all Admin permissions)
-   **Deliverable**: Schema migration script, updated user management form, role validation helpers
-   **Duration**: 0.5 days (4 hours)
-   **Dependencies**: None (prerequisite for T113d)

**T113d: Create Parameter Prefix Maintenance Form (Developer Tools)**  
Build Settings Form UI for managing parameter prefix overrides.

-   **UserControl**: Control_Settings_ParameterPrefixMaintenance
-   **TreeView Location**: Settings Form → Development → Parameter Prefix Management
-   **UI Components**:
    -   DataGridView: Columns for ProcedureName, ParameterName, DetectedPrefix, OverridePrefix, Confidence, Reason
    -   Buttons: Add Override, Edit Override, Delete Override, Save All, Reload Cache, Export, Import
    -   Audit Log Panel: Shows recent changes (CreatedBy, ModifiedDate, Action)
    -   Filter Controls: Search by procedure name, filter by confidence level
-   **Functionality**:
    -   CRUD operations on sys_parameter_prefix_override table
    -   Export overrides to JSON file for environment transfer
    -   Import overrides from JSON file
    -   Reload button triggers cache refresh (calls Helper_Database_StoredProcedure.RefreshPrefixCache())
    -   Role validation: Visible only to users with IsAdmin=TRUE AND IsDeveloper=TRUE
-   **Deliverable**: Fully functional maintenance form with database persistence
-   **Duration**: 1 day (8 hours)
-   **Dependencies**: T113c (need database schema and role infrastructure)

**T113: Refactor Top 20 Priority Procedures from T105 Matrix**  
Standardize highest-priority procedures (most used, least compliant) with concurrent documentation.

-   **Method**: For each procedure:
    1. Add `OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)` parameters if missing
    2. Standardize parameter prefixes (p* for CRUD, in* for multi-step per corrected CSV from T106a)
    3. Implement proper error handling (SET p_Status=-1, SET p_ErrorMsg='Error description')
    4. Add success status logic (SET p_Status=1 for data returned, SET p_Status=0 for no data)
    5. Update procedure header comments with parameter documentation
    6. **Concurrent documentation** (tracked in Documentation-Update-Matrix.md):
        - ☐ Update procedure header comments
        - ☐ Update DAO XML documentation for calling method
        - ☐ Update 00_STATUS_CODE_STANDARDS.md if new pattern demonstrated
        - ☐ Update quickstart.md if commonly used procedure
    7. Save updated procedure to `Database/UpdatedStoredProcedures/<name>.sql`
    8. Mark documentation checkboxes complete in matrix
-   **Scope**: Top 20 procedures from refactoring priority matrix
-   **Deliverable**: 20 refactored .sql files with standardized signatures + concurrent documentation updates
-   **Duration**: 24 hours (1.2 hours per procedure average, includes documentation)
-   **Dependencies**: T105 (need priority matrix), T106a (need corrected transaction strategy CSV)

**T114: Refactor Remaining Inventory Procedures with Documentation**  
Standardize all remaining inv*inventory*_ and inv*transaction*_ procedures not covered in T113.

-   **Includes concurrent documentation checkboxes** (same 4 items as T113)
-   **Scope**: ~10 remaining inventory/transaction procedures
-   **Deliverable**: 10 refactored .sql files + documentation updates
-   **Duration**: 12 hours (includes documentation time)
-   **Dependencies**: T105, T113 (complete high-priority first)

**T115: Refactor User/Role Management Procedures with Documentation**  
Standardize all sys*user*_, sys*role*_, sys*user_role*\* procedures.

-   **Includes concurrent documentation checkboxes** (tracked in matrix)
-   **Scope**: ~15 user/role management procedures
-   **Deliverable**: 15 refactored .sql files + documentation updates
-   **Duration**: 18 hours (includes documentation time)
-   **Dependencies**: T105, T113

**T116: Refactor Master Data Procedures with Documentation**  
Standardize all md*part_ids*_, md*locations*_, md*operation_numbers*_, md*item_types*_ procedures.

-   **Includes concurrent documentation checkboxes** (tracked in matrix)
-   **Scope**: ~20 master data procedures
-   **Deliverable**: 20 refactored .sql files + documentation updates
-   **Duration**: 24 hours (includes documentation time)
-   **Dependencies**: T105, T113

**T117: Refactor Logging/Quick Button/System Procedures**  
Standardize all log*error*_, sys*last_10_transactions*_, and remaining system procedures.

-   **Scope**: ~15 logging/quick button/system procedures
-   **Deliverable**: 15 refactored .sql files
-   **Duration**: 15 hours
-   **Dependencies**: T105, T113

**T118: Add Explicit Transaction Management to Multi-Step Procedures**  
Identify and enhance procedures with multiple logical steps to use BEGIN/COMMIT/ROLLBACK.

-   **Candidates**: inv*inventory_Transfer*\* (deduct + add + log), user creation with role assignment, batch operations
-   **Method**: Wrap multi-step logic in transaction block:
    ```sql
    BEGIN
        DECLARE EXIT HANDLER FOR SQLEXCEPTION
        BEGIN
            SET p_Status = -1;
            SET p_ErrorMsg = 'Transaction failed. All changes rolled back.';
            ROLLBACK;
        END;

        START TRANSACTION;

        -- Step 1: Deduct from source
        -- Step 2: Add to destination
        -- Step 3: Log transaction

        COMMIT;
        SET p_Status = 1;
        SET p_ErrorMsg = 'Transfer completed successfully';
    END;
    ```
-   **Scope**: ~10 multi-step procedures requiring transaction safety
-   **Deliverable**: 10 refactored .sql files with explicit transaction management
-   **Duration**: 15 hours (complex logic requires careful review)
-   **Dependencies**: T113-T117 (refactor basic structure first, then add transactions)

**Part C Summary**:

-   **Tasks**: 6 refactoring tasks
-   **Duration**: 95 hours (~12 days single developer, can parallelize with Part B)
-   **Deliverables**: ~90 refactored stored procedures meeting 100% compliance
-   **Critical Output**: All procedures standardized before database deployment

---

### Phase 2.5 Part D: Database Deployment (T119-T121)

**Objective**: Deploy refactored stored procedures to test and production databases safely.

**Tasks**:

**T119: Create Deployment Script with Environment Safety Checks**  
Build PowerShell script that wipes old procedures and installs new ones with safety mechanisms.

-   **Implementation**:

    ```powershell
    param(
        [Parameter(Mandatory=$true)]
        [ValidateSet('Test','Production')]
        [string]$Environment
    )

    # Environment configuration
    $config = @{
        'Test' = @{
            Server = 'localhost'
            Database = 'mtm_wip_application_winforms_test'
            Port = 3306
        }
        'Production' = @{
            Server = '172.16.1.104'
            Database = 'MTM_WIP_Application_Winforms'
            Port = 3306
        }
    }

    # Safety confirmation
    if ($Environment -eq 'Production') {
        Write-Host "WARNING: About to wipe ALL procedures in PRODUCTION database!" -ForegroundColor Red
        Write-Host "Database: $($config.Production.Database) on $($config.Production.Server)" -ForegroundColor Yellow
        $confirmation = Read-Host "Type 'CONFIRM' to proceed"
        if ($confirmation -ne 'CONFIRM') {
            Write-Host "Deployment cancelled" -ForegroundColor Green
            exit 0
        }
        Start-Sleep -Seconds 10  # 10-second final warning
    }

    # 1. Create backup
    Write-Host "Creating backup..."
    $backupFile = "backup_procedures_$(Get-Date -Format 'yyyyMMdd_HHmmss').sql"
    # Execute mysqldump for procedures only

    # 2. Wipe existing procedures
    Write-Host "Dropping existing procedures..."
    $procedures = Execute-Query -Query "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='$Database' AND ROUTINE_TYPE='PROCEDURE'"
    foreach ($proc in $procedures) {
        Execute-Query -Query "DROP PROCEDURE IF EXISTS $($proc.ROUTINE_NAME)"
    }

    # 3. Install new procedures from UpdatedStoredProcedures/
    Write-Host "Installing new procedures..."
    Get-ChildItem -Path "UpdatedStoredProcedures\*.sql" -Recurse | ForEach-Object {
        $content = Get-Content $_.FullName -Raw
        Execute-Query -Query $content
        Write-Host "Installed: $($_.Name)" -ForegroundColor Green
    }

    # 4. Validate installation
    Write-Host "Validating deployment..."
    $newCount = (Execute-Query -Query "SELECT COUNT(*) as Count FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='$Database'").Count
    Write-Host "Deployment complete. $newCount procedures installed." -ForegroundColor Green
    ```

-   **Deliverable**: `Deploy-StoredProcedures.ps1` script with safety checks
-   **Duration**: 6 hours (includes testing and documentation)
-   **Dependencies**: T113-T118 (need all refactored procedures ready)

**T119b: Re-audit Production Procedures for Schema Drift Detection**  
Run fresh audit of production database to detect changes made during Phase 2.5 implementation period.

-   **Method**:
    1. Execute T101 audit steps against current production (same query, fresh timestamp)
    2. Compare re-audit results to baseline audit from Phase 2.5 start
    3. Identify drift: procedures added, modified, or deleted since baseline
    4. Categorize each drifted procedure:
        - **Category A - Independent Hotfix**: Production change unrelated to Phase 2.5 refactoring (preserve business logic, apply standards separately)
        - **Category B - Conflicting Change**: Production change affects same procedure being refactored (manual three-way merge required)
        - **Category C - New Procedure**: Procedure added to production during Phase 2.5 (full refactoring required)
    5. Generate drift report with categorization
-   **Output**: Drift report CSV with columns: ProcedureName, DriftType (Added/Modified/Deleted), Category (A/B/C), BaselineVersion, CurrentVersion, ConflictRisk (Low/Medium/High)
-   **Deliverable**: Categorized drift report for T119c/d/e processing
-   **Duration**: 0.25 days (2 hours)
-   **Dependencies**: T113-T118 complete (need refactoring finished before comparing drift)

**T119c: Refactor Category A Procedures (Independent Hotfixes)**  
Apply Phase 2.5 standards to production hotfixes while preserving business logic changes.

-   **Method**:
    1. For each Category A procedure, extract from current production (not baseline)
    2. Apply 00_STATUS_CODE_STANDARDS.md template (add OUT params, standardize prefixes, error handling)
    3. Preserve all production business logic changes (new queries, logic branches, parameter additions)
    4. Document hotfix origin in procedure header comments: "HOTFIX: Applied during Phase 2.5, standardized YYYY-MM-DD"
    5. Save to UpdatedStoredProcedures/ with \_hotfix suffix temporarily for tracking
    6. Test refactored hotfix procedures using integration tests
-   **Deliverable**: Category A procedures refactored and tested (typically 2-5 procedures)
-   **Duration**: 0.25-0.5 days (depends on hotfix count, 1-2 hours per procedure)
-   **Dependencies**: T119b (need categorized drift report)

**T119d: Merge Category B Conflicts (Three-Way Merge)**  
Manually resolve conflicts where production changes overlap with Phase 2.5 refactoring.

-   **Method**:
    1. For each Category B procedure, gather three versions:
        - **Baseline**: Original procedure from Phase 2.5 start (T102 extraction)
        - **Refactored**: Phase 2.5 standardized version (from T113-T118)
        - **Production**: Current production version with hotfix changes
    2. Perform three-way merge using diff tool (VS Code, Beyond Compare, etc.)
    3. Merge strategy:
        - Keep Phase 2.5 standardization (OUT params, error handling, prefixes)
        - Integrate production business logic changes
        - Resolve conflicts favoring production business logic over baseline
    4. Document merge decisions in conflict resolution log
    5. Test merged procedures thoroughly (integration tests + manual validation)
-   **Deliverable**: Category B procedures merged with conflict resolution documentation (typically 1-3 procedures)
-   **Duration**: 0.25-0.5 days (depends on conflict count, 2-3 hours per conflict)
-   **Dependencies**: T119b (need categorized conflicts), T119c (resolve hotfixes first to reduce conflict surface)

**T119e: Refactor Category C Procedures (New Procedures)**  
Full Phase 2.5 treatment for procedures added to production during implementation.

-   **Method**:
    1. Extract Category C procedures from current production
    2. Run compliance audit (same checks as T103) to establish baseline scores
    3. Apply full refactoring workflow: add OUT params, standardize prefixes, error handling, documentation
    4. Create integration tests (4-test pattern from T108-T111)
    5. Update Documentation-Update-Matrix.md with new procedure entries
    6. Save to UpdatedStoredProcedures/ with standard organization
-   **Deliverable**: Category C procedures fully refactored, tested, and documented (typically 1-4 procedures)
-   **Duration**: 0.25-0.5 days (depends on new procedure count, 1-2 hours per procedure)
-   **Dependencies**: T119b (need categorized new procedures), T119c/d (handle conflicts first)

**T120: Execute Test Database Deployment and Validation**  
Deploy to mtm_wip_application_winforms_test using post-reconciliation procedure set and validate all procedures executable.

-   **Method**:
    1. Run `Deploy-StoredProcedures.ps1 -Environment Test`
    2. **Deployment uses post-reconciliation procedures**: Refactored baseline (T113-T118) + Category A hotfixes (T119c) + Category B merged (T119d) + Category C new (T119e)
    3. Execute all integration tests (T108-T111) against test database
    4. Review deployment logs for errors or warnings
    5. Manually test 5 high-priority procedures via DAO calls
-   **Success Criteria**: All integration tests pass (100% pass rate), no deployment errors, drift procedures properly integrated
-   **Deliverable**: Test deployment validation report with drift reconciliation confirmation
-   **Duration**: 4 hours
-   **Dependencies**: T119 (need deployment script), T119b/c/d/e (need reconciled procedures), T108-T111 (need integration tests)

**T121: Execute Production Database Deployment (with DBA Review)**  
Deploy to MTM_WIP_Application_Winforms after DBA approval, backup verification, and drift reconciliation review.

-   **Prerequisites**:
    -   DBA reviews deployment plan, refactored procedures, AND drift reconciliation report (T119b/c/d/e)
    -   Full database backup created and validated (restore test)
    -   Rollback plan documented (restore from backup + previous procedure SQL files)
    -   Maintenance window scheduled (off-hours deployment)
-   **Method**:
    1. Notify users of maintenance window (30 minutes)
    2. Stop application services accessing database
    3. Create fresh backup immediately before deployment
    4. Run `Deploy-StoredProcedures.ps1 -Environment Production`
    5. Execute smoke tests (5 critical procedures: inventory add, user auth, transaction log, part search, location get)
    6. Restart application services
    7. Monitor error logs for 1 hour post-deployment
-   **Success Criteria**: All smoke tests pass, zero production errors in 1-hour monitoring window
-   **Rollback Plan**: If deployment fails or errors detected, restore backup immediately (15-minute RTO)
-   **Deliverable**: Production deployment validation report
-   **Duration**: 3 hours (includes monitoring period)
-   **Dependencies**: T120 (test deployment must succeed first)

**Part D Summary**:

-   **Tasks**: 3 deployment tasks
-   **Duration**: 13 hours (~2 days including monitoring)
-   **Deliverables**: Production database with 100% standardized procedures, drift reconciliation complete
-   **Critical Output**: Safe, validated deployment with rollback capability

**Part D Summary**:

-   **Tasks**: 7 (T119-T121 + T119b/c/d/e drift reconciliation)
-   **Duration**: 19-21 hours (2.5-3 days including drift reconciliation: 0.75-1.5 days)
-   **Key Deliverables**: Deployment script with safety checks, drift detection and categorization, hotfix/conflict/new procedure reconciliation, validated test deployment, production deployment with DBA approval
-   **Critical Success Factor**: Schema drift properly categorized and reconciled before deployment, all drift procedures (Category A/B/C) integrated into deployment set

---

### Phase 2.5 Part E: End-to-End Integration Testing (T122-T128)

**Objective**: Validate entire refactored database layer works correctly in application context.

**Tasks**:

**T122: Execute All Integration Tests Against New Procedures (Post-Deployment)**  
Re-run complete integration test suite to validate refactored procedures behave identically to originals.

-   **Method**: Execute full test suite (T108-T111 tests) against test database with new procedures
-   **Success Criteria**: 100% pass rate (same as pre-deployment baseline)
-   **Regression Detection**: Any test failures indicate behavior changes requiring investigation
-   **Deliverable**: Integration test results report comparing pre/post deployment
-   **Duration**: 2 hours (includes analysis of any failures)
-   **Dependencies**: T120 (test database deployed)

**T123: Test Parameter Prefix Detection and Retry Strategy at Startup**  
Validate INFORMATION_SCHEMA query populates parameter cache correctly at application startup, with retry dialog on failure.

-   **Method**:
    1. **Success path**: Enable Service_DebugTracer detailed logging, launch application (triggers Program.cs cache initialization), review logs for cache population messages, verify cache contains all 70+ procedures with correct parameter prefixes
    2. **Retry dialog testing**:
        - Temporarily block INFORMATION_SCHEMA access (firewall rule or database permissions)
        - Launch application, verify MessageBox appears with clear error message
        - Test "Retry" button functionality (3 attempts total with connection retries)
        - Test "Quit" button functionality (clean application termination, no fallback)
        - Verify cache loads from sys_parameter_prefix_override table for stored overrides
    3. **Override table integration**: Add 2-3 test entries to sys_parameter_prefix_override, verify cache prioritizes override values over INFORMATION_SCHEMA results
-   **Success Criteria**: Cache populates in <200ms on success, retry dialog appears within 5 seconds on failure, 3 retry attempts before termination, override table values correctly loaded and prioritized
-   **Deliverable**: Startup validation report with cache statistics, retry dialog screenshots, override integration confirmation
-   **Duration**: 4 hours (includes retry testing scenarios and override table validation)
-   **Dependencies**: T120 (test deployment), T113d (override table exists), FR-002 implementation, FR-029 (retry strategy)

**T124: Validate DAO Method Calls Route Through Helper Correctly**  
Confirm all 220 call sites use Helper_Database_StoredProcedure execution methods (no direct MySQL API).

-   **Method**:
    1. **Roslyn analyzer execution**: Run MTM.CodeAnalysis.DatabaseAccess analyzer (from T124a) against entire solution
    2. Review analyzer output for diagnostic violations (MTM001-MTM004 rules)
    3. Search Data/ folder for `MySqlConnection`, `MySqlCommand`, `MySqlDataAdapter` patterns using grep
    4. Review T100 callsite-inventory.csv for any non-Helper patterns
    5. Manually inspect top 10 most-used DAOs (Dao_Inventory, Dao_User, Dao_Transactions)
    6. Generate violation report with file locations, rule violations, suggested fixes
-   **Success Criteria**: Zero direct MySQL API usage detected (100% Helper routing compliance), Roslyn analyzer reports no violations
-   **Deliverable**: Compliance validation report with analyzer output
-   **Duration**: 3 hours
-   **Dependencies**: T124a (Roslyn analyzer developed), Phase 3-6 DAO refactoring (assume complete for this test)

**T124a: Develop Roslyn Analyzer for Database Access Compliance**  
Create custom Roslyn analyzer to detect and fix database access violations in real-time during development.

-   **Method**:
    1. **Project setup**: Create MTM.CodeAnalysis.DatabaseAccess class library project targeting .NET Standard 2.0 (Roslyn compatibility)
    2. **Diagnostic rules** (4 rules):
        - **MTM001**: Direct MySqlConnection usage detected → "Use Helper_Database_StoredProcedure.ExecuteDataTableWithStatus() instead"
        - **MTM002**: Direct MySqlCommand usage detected → "Use Helper_Database_StoredProcedure for stored procedure execution"
        - **MTM003**: Inline SQL detected (CommandText contains spaces/semicolons) → "Only stored procedures permitted, no inline SQL"
        - **MTM004**: Missing p_Status/p_ErrorMsg output parameter check → "Stored procedure results must validate p_Status output"
    3. **Code fix providers**: Implement CodeFixProvider for MTM001/MTM002 with "Convert to Helper_Database_StoredProcedure" quick action
    4. **Severity configuration**:
        - **v1.0.0** (initial rollout): All rules as Warning severity (educational phase)
        - **v2.0.0** (enforcement): MTM001/MTM002/MTM003 as Error severity, MTM004 remains Warning
    5. **NuGet package**: Build and publish to internal feed or local source
    6. **IDE integration**: Add `<PackageReference Include="MTM.CodeAnalysis.DatabaseAccess" Version="1.0.0" />` to MTM_WIP_Application_Winforms.csproj
-   **Deliverables**:
    -   MTM.CodeAnalysis.DatabaseAccess.csproj with 4 diagnostic analyzers
    -   2 code fix providers (MTM001/MTM002)
    -   NuGet package (1.0.0 warnings, 2.0.0 errors)
    -   Integration documentation in quickstart.md
-   **Duration**: 2-3 hours (analyzer development, testing, packaging)
-   **Dependencies**: None (prerequisite for T124)

**T125: Test Error Logging with Recursive Prevention**  
Force error logging failures to validate fallback to file logging and no infinite loops.

-   **Method**:
    1. Temporarily rename log_error table to simulate unavailability
    2. Trigger database error (force constraint violation in inventory add)
    3. Verify error logged to `Logs/database-errors.log` file instead
    4. Check application remains stable (no hang, no crash)
    5. Restore log_error table, trigger another error, verify database logging resumes
-   **Success Criteria**: Fallback activates automatically, file log contains full error context, application stable
-   **Deliverable**: Recursive prevention validation report
-   **Duration**: 2 hours
-   **Dependencies**: FR-006 implementation (recursive prevention in LogErrorToDatabaseAsync)

**T126: Execute Manual Testing of All Forms Workflows**  
Manually test all 25 forms to validate database operations work correctly from user perspective.

-   **Workflows**:
    -   **MainForm**: Login, inventory grid load, part search, operation filter
    -   **Control_Inventory**: Add inventory (IN transaction), adjust quantity
    -   **Control_Transactions_Remove**: Remove inventory (OUT transaction)
    -   **Control_Transactions_Transfer**: Transfer between locations (TRANSFER transaction with 3-step atomic operation)
    -   **Control_Settings**: Update user preferences, change password
    -   **Control_QuickButtons**: Add/remove/reorder quick buttons
    -   **Control_History**: View transaction history, filter by date/user/part
    -   **Control_Reports**: Generate Excel reports, print DataGridView
-   **Success Criteria**: All workflows complete successfully, data persists correctly, error dialogs show user-friendly messages
-   **Deliverable**: Manual testing checklist with pass/fail status per workflow
-   **Duration**: 8 hours (comprehensive workflow testing)
-   **Dependencies**: T120-T125 (all technical validation complete)

**T127: Validate Transaction Rollback in Multi-Step Operations**  
Test explicit transaction management (T118) by forcing failures mid-operation.

-   **Method**:
    1. Test TransferInventoryAsync: Force step 2 (add to destination) to fail with constraint violation
    2. Verify database state: Source quantity unchanged (step 1 rolled back), no transaction log entry (step 3 not executed)
    3. Test batch removal: Delete 100 items, force failure at item #50
    4. Verify database state: All 100 items still present (complete rollback)
    5. Test user creation with role: Force role assignment failure
    6. Verify database state: User record not created (complete rollback)
-   **Success Criteria**: Zero orphaned records (SC-009), complete rollback in all scenarios
-   **Deliverable**: Transaction rollback validation report
-   **Duration**: 4 hours
-   **Dependencies**: T118 (explicit transaction management implemented)

**T128: Performance Benchmark Comparison (Pre vs Post Refactor)**  
Execute performance benchmark suite to validate ±5% variance from baseline.

-   **Method**:
    1. Execute 10 key operations 100 times each: inventory add, inventory search (large result), inventory transfer, user auth, transaction log retrieval, part search, location get, operation get, quick button get, error log retrieval
    2. Measure execution time per operation using Stopwatch
    3. Calculate mean, median, P95, P99 for each operation
    4. Compare to pre-refactor baseline (established before Phase 2.5)
    5. Validate variance within ±5% tolerance
-   **Success Criteria**: All operations within ±5% of baseline (SC-004)
-   **Deliverable**: Performance comparison report with charts
-   **Duration**: 4 hours (includes baseline measurement if not already captured)
-   **Dependencies**: T120 (test deployment), baseline measurement

**Part E Summary**:

-   **Tasks**: 8 integration testing tasks (T122-T128 + T124a Roslyn analyzer development)
-   **Duration**: 28-29 hours (~3.5-4 days single developer)
-   **Deliverables**: Complete validation covering technical, functional, and performance aspects, Roslyn analyzer for ongoing compliance enforcement
-   **Critical Output**: Confidence in production readiness, automated compliance tooling

---

### Phase 2.5 Part F: Documentation and Knowledge Transfer (T129-T132)

**Objective**: Update all documentation to reflect refactored database layer, enable concurrent documentation tracking, and ensure team adoption.

**Tasks**:

**T129: Generate Documentation-Update-Matrix.md for Concurrent Tracking**  
Create Markdown table to track documentation updates performed concurrently during T113-T116 refactoring.

-   **Method**:
    1. Create `Documentation-Update-Matrix.md` in `specs/003-database-layer-refresh/` folder
    2. Generate table with columns: File Path (clickable link), Status (⬜ Not Started, 🔄 In Progress, ✅ Complete, ⚠️ Needs Review), Last Updated, Assigned To, Notes
    3. Populate rows for all documentation targets:
        - **Per-procedure rows**: 70+ procedures × 2 files (procedure header comments, DAO XML documentation) = ~140 rows
        - **Standards documents**: 00_STATUS_CODE_STANDARDS.md, DEVELOPMENT-STANDARDS.md updates
        - **Quickstart documents**: quickstart.md sections for each DAO domain
    4. Add validation script section: PowerShell script to check matrix completeness (exit code 0 if 100% complete, exit code 1 if any ⬜/🔄/⚠️ status)
    5. Integrate into T113-T116 workflow: Developers check off matrix rows as they complete procedure refactoring
-   **Deliverable**: Documentation-Update-Matrix.md with ~145 trackable items, validation script
-   **Duration**: 2 hours (matrix generation, validation script development)
-   **Dependencies**: T113-T118 procedure list finalized (know what to track)

**T130: Perform Bulk Documentation Updates from Matrix**  
Complete any remaining documentation items from Documentation-Update-Matrix.md not finished during concurrent updates.

-   **Method**:
    1. Run validation script from T129 to identify incomplete items (⬜/🔄/⚠️ status)
    2. Review incomplete items, prioritize by importance (core procedures first)
    3. Complete documentation updates:
        - **Procedure header comments**: Add purpose, parameters, return codes, examples
        - **DAO XML documentation**: Add comprehensive XML comments (summary, param, returns, exceptions)
        - **Standards updates**: Incorporate lessons learned into 00_STATUS_CODE_STANDARDS.md (common pitfalls, multi-step transactions, complex parameters, testing guidance)
        - **Quickstart sections**: Update quickstart.md with DAO-specific examples
    4. Update matrix status to ✅ as items completed
    5. Re-run validation script until exit code 0 (100% complete)
-   **Deliverable**: 100% complete Documentation-Update-Matrix.md (all ✅), updated standards and quickstart documents
-   **Duration**: 8 hours (completing concurrent documentation backlog)
-   **Dependencies**: T113-T118 (refactoring complete), T129 (matrix exists)

**T131: Validate Documentation-Update-Matrix.md Completeness**  
Run validation script to ensure all documentation items completed before final approval.

-   **Method**:
    1. Execute validation script from T129 (PowerShell script in matrix document)
    2. Script checks: all rows have ✅ status, all file path links valid, no ⬜/🔄/⚠️ status remaining
    3. Generate completeness report: total items, completed items, completion percentage, list of incomplete items (if any)
    4. If <100% complete: Loop back to T130, complete remaining items
    5. If 100% complete: Generate final approval report, archive matrix as project artifact
-   **Success Criteria**: Exit code 0 (100% complete), all 145+ items marked ✅
-   **Deliverable**: Documentation completeness validation report
-   **Duration**: 1 hour (validation execution and report generation)
-   **Dependencies**: T130 (bulk updates complete)

**T132: Create Phase 2.5 Comprehensive Implementation Guide**  
Compile complete reference document covering all refactoring work, decisions, and outcomes.

```csharp
/// <summary>
/// Adds new inventory item to database with automatic transaction logging.
/// </summary>
/// <param name="partId">Part number identifier (required, non-empty).</param>
/// <param name="locationCode">Location code from md_locations (required, must exist).</param>
/// <param name="quantity">Quantity to add (required, must be positive).</param>
/// <param name="operationNumber">Operation number from md_operation_numbers (optional).</param>
/// <param name="userId">Current user ID for audit trail (required).</param>
/// <returns>
/// Model_Dao_Result indicating success or failure:
/// - IsSuccess=true: Inventory added successfully, Message contains confirmation
/// - IsSuccess=false: Addition failed, Message contains user-friendly error, Exception contains technical details
/// </returns>
/// <exception cref="ArgumentNullException">Thrown if required parameters (partId, locationCode, userId) are null.</exception>
/// <remarks>
/// Routes through Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync calling inv_inventory_Add.
/// Operation logged to inv_transaction table automatically by stored procedure.
/// Validates location and operation existence before insertion.
/// </remarks>
public async Task<Model_Dao_Result> AddInventoryAsync(string partId, string locationCode, decimal quantity, int? operationNumber, int userId)
{
    // Implementation
}
```

-   **Scope**: All public methods in 12 DAO classes (~150 methods total)
-   **Deliverable**: Updated DAO .cs files with complete XML documentation
-   **Duration**: 10 hours (detailed documentation takes time)
-   **Dependencies**: Phase 3-6 DAO refactoring (assume complete)

**T132: Create Phase 2.5 Comprehensive Implementation Guide**  
Compile complete reference document covering all refactoring work, decisions, and outcomes.

-   **Sections**:
    1. **Executive Summary**: 2-paragraph overview of what was accomplished, including drift reconciliation
    2. **Metrics**:
        - Stored Procedures Refactored: 70+ (100% of database layer)
        - Schema Drift Procedures: Category A (hotfixes), Category B (conflicts), Category C (new procedures) - actual counts TBD during T119b
        - Compliance Score: 100% (all procedures meet standards post-reconciliation)
        - Integration Tests Created: 280+ test methods (4 per procedure)
        - Test Pass Rate: 100% (verbose diagnostics enabled)
        - Parameter Prefix Errors: 0 (down from ~20/month baseline)
        - Performance Variance: ±3% (within ±5% tolerance per SC-004)
        - Database-Related Tickets: 92% reduction (baseline 50/month → current 4/month)
        - Roslyn Analyzer Violations: 0 (100% Helper routing compliance)
    3. **Success Criteria Results**: Map each SC-001 through SC-018 to actual measured outcome (includes new SC-011 through SC-018 from Session 3)
    4. **Timeline**: Actual vs estimated (target: 10-15 days parallel with 3 developers, actual: TBD)
    5. **Lessons Learned**: Top 15 insights from refactoring
        - Parameter prefix conventions and override table benefits
        - Transaction management complexity and rollback strategies
        - Test isolation importance and verbose diagnostic value
        - Schema drift reconciliation workflow (Category A/B/C classification)
        - CSV transaction analysis accuracy and peer review benefits
        - Concurrent documentation vs bulk documentation trade-offs
        - Roslyn analyzer real-time feedback vs post-hoc validation
        - Developer role infrastructure for maintenance tools
        - Retry strategy vs fallback logic for critical startup operations
        - Three-way merge techniques for Category B conflicts
    6. **Next Steps**: Phase 3-8 DAO refactoring can now proceed with confidence
    7. **Appendices**:
        - **Appendix A**: Schema drift reconciliation detailed report (Category A/B/C procedures, resolution strategies, merge decisions)
        - **Appendix B**: CSV transaction analysis summary (pattern detection accuracy, developer corrections, implementation outcomes)
        - **Appendix C**: Roslyn analyzer integration guide (installation, configuration, custom rule development, severity phasing)
-   **Deliverable**: specs/003-database-layer-refresh/phase-2.5-completion-report.md with appendices
-   **Duration**: 4 hours (expanded to include drift reconciliation, CSV analysis, Roslyn integration documentation)
-   **Dependencies**: T100-T131 (all Phase 2.5 work complete including drift reconciliation)

**Part F Summary**:

-   **Tasks**: 4 documentation tasks (T129-T132)
-   **Duration**: 15 hours (~2 days single developer)
-   **Deliverables**: Documentation-Update-Matrix.md with validation script, 100% complete documentation updates, completeness validation, comprehensive implementation guide with appendices
-   **Critical Output**: Concurrent documentation tracking, knowledge transfer, lessons learned captured, drift reconciliation documented

---

## Cross-Phase Dependencies

### Phase 2.5 Prerequisites (From Original Plan)

Phase 2.5 depends on Phase 1-2 completion:

-   **Phase 1**: Model_Dao_Result<T> pattern defined and implemented (T001-T006)
-   **Phase 2**: Helper_Database_StoredProcedure refactored with 4 execution methods (T007-T016)
-   **Phase 2**: INFORMATION_SCHEMA parameter cache implemented (T011)

### Phases 3-8 Blocked by Phase 2.5

Phase 2.5 blocks all downstream DAO refactoring work:

-   **Phase 3 (Inventory DAO)**: T017-T018 superseded by Phase 2.5 Part C (T113-T114 refactor procedures first)
-   **Phase 4 (User/Transaction DAOs)**: T024-T026 superseded by Phase 2.5 Part C (T115, T117)
-   **Phase 5 (Master Data DAOs)**: T033-T034 superseded by Phase 2.5 Part C (T116)
-   **Phase 6 (Logging/Quick Button DAOs)**: T042-T045 superseded by Phase 2.5 Part C (T117)
-   **Phase 3-6 (DAO Testing)**: T039-T041 superseded by Phase 2.5 Part B (T108-T112 comprehensive test suite)

**Rationale**: Cannot refactor DAO methods until underlying stored procedures are standardized. DAO refactoring assumes procedures follow 00_STATUS_CODE_STANDARDS.md with correct parameter prefixes and output parameters.

---

## Trade-Offs and Decisions

### Decision 1: Async-Only vs Async/Sync Dual Support

**Options**:

1. **Async-only** (chosen): All DAO methods return Task<Model_Dao_Result>, no sync wrappers
2. **Async/Sync dual**: Provide both async and sync versions of every method

**Decision**: Async-only with immediate migration requirement (FR-010)

**Rationale**:

-   **Pros of async-only**:
    -   Clean, modern codebase with single execution path
    -   Forces best practices (responsive UI, non-blocking operations)
    -   No technical debt from sync wrapper maintenance
    -   Simplified testing (one code path to validate)
-   **Cons**:
    -   Requires all calling code (Forms, Controls, Services) to migrate immediately
    -   WinForms async void event handlers can be confusing for developers
    -   No gradual migration path for complex forms

**Migration Support**: Provide detailed quickstart.md with WinForms async patterns to ease transition.

---

### Decision 2: Complete Database Wipe vs Incremental Procedure Updates

**Options**:

1. **Complete wipe and reinstall** (chosen): Drop all procedures, install fresh from UpdatedStoredProcedures/
2. **Incremental updates**: ALTER each procedure individually, preserving procedures not yet refactored

**Decision**: Complete wipe with backup safety net (T119-T121)

**Rationale**:

-   **Pros of wipe**:
    -   Guarantees clean slate with zero legacy code
    -   Simpler deployment script (no conditional logic for which procedures to update)
    -   Eliminates risk of missed procedures during incremental updates
    -   Version control clarity (all procedures in UpdatedStoredProcedures/ represent current state)
-   **Cons**:
    -   Higher risk operation (catastrophic if backup fails)
    -   No partial rollback (must restore entire backup if deployment fails)
    -   Requires longer maintenance window (30 minutes vs 5 minutes for incremental)

**Safety Mechanisms**: Environment checks, 10-second confirmation, immediate backup, test deployment first (T120 validates before production).

---

### Decision 3: INFORMATION_SCHEMA vs Convention-Only for Parameter Prefixes

**Options**:

1. **INFORMATION_SCHEMA with fallback** (chosen): Query database schema at startup, fall back to convention if unavailable
2. **Convention-only**: Use naming conventions exclusively (p* default, in* for Transfer\*)

**Decision**: INFORMATION_SCHEMA primary, convention fallback (FR-002)

**Rationale**:

-   **Pros of schema query**:
    -   100% accuracy for parameter prefixes (reads actual database definitions)
    -   Handles edge cases (mixed prefixes, custom patterns) automatically
    -   No maintenance of convention rules as procedures evolve
-   **Cons**:
    -   Startup performance cost (~100-200ms one-time query)
    -   Requires database connectivity before application fully loads (already required per FR-014)
    -   Convention fallback still needed for robustness

**Performance Impact**: Negligible - 100-200ms startup overhead acceptable for accuracy gain. Cache lasts entire application lifetime.

---

### Decision 4: Per-Test-Class Transactions vs Database Reset for Integration Tests

**Options**:

1. **Per-test-class transactions** (chosen): Begin transaction in [TestInitialize], rollback in [TestCleanup]
2. **Database reset**: Restore database from snapshot before each test run

**Decision**: Per-test-class transactions (FR-018)

**Rationale**:

-   **Pros of transactions**:
    -   Fast execution (~50ms per test vs ~5 seconds for database reset)
    -   Isolated tests (each test has clean transaction scope)
    -   Parallel-safe (each developer's test database independent)
    -   No schema drift (test data never commits)
-   **Cons**:
    -   Tests can't validate COMMIT behavior (transaction always rolls back)
    -   Complex setup for multi-table test data (insert all prerequisite records within transaction)

**Validation Gap**: Multi-step transaction testing (T127) done separately with manual validation to test actual COMMIT/ROLLBACK behavior.

---

### Decision 5: Centralized Error Logging vs Distributed Logging

**Options**:

1. **Centralized database logging** (chosen): All errors logged to log_error table via Dao_ErrorLog
2. **Distributed file logging**: Each component logs to separate files

**Decision**: Centralized database logging with file fallback (FR-005, FR-006)

**Rationale**:

-   **Pros of centralized**:
    -   Single source of truth for error analysis
    -   Rich querying (filter by user, date range, severity, error type)
    -   Correlation across components (track error cascades)
    -   Supports audit/compliance requirements
-   **Cons**:
    -   Database dependency for error logging (recursion risk if database unavailable)
    -   Additional database load from error logging operations

**Safety**: File fallback (Logs/database-errors.log) handles recursive case where database unavailable during error logging.

---

## Rollback Plan

### Pre-Deployment Preparation

**Backup Creation**:

-   Full database backup immediately before T121 production deployment
-   Backup includes: All tables, all stored procedures, all data
-   Backup validated via restore test to empty database
-   Backup stored in two locations (local backup directory + network share)

**Procedure Source Preservation**:

-   Current procedures exported to `Database/CurrentStoredProcedures_BACKUP_2025-10-15/` before wipe
-   Git tag created: `pre-phase-2.5-deployment` marking last commit before deployment
-   T102 generated .sql files in `Database/UpdatedStoredProcedures/` committed to version control

**Rollback Script**:

```powershell
# Rollback-StoredProcedures.ps1
param([string]$BackupFile)

# 1. Validate backup file exists
if (-not (Test-Path $BackupFile)) {
    Write-Error "Backup file not found: $BackupFile"
    exit 1
}

# 2. Drop new procedures (same as wipe logic)
Write-Host "Dropping newly deployed procedures..."
# Execute DROP for each procedure

# 3. Restore from backup
Write-Host "Restoring procedures from backup: $BackupFile"
mysql -h 172.16.1.104 -u root -p MTM_WIP_Application_Winforms < $BackupFile

# 4. Validate restoration
$restoredCount = Execute-Query -Query "SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='MTM_WIP_Application_Winforms'"
Write-Host "Rollback complete. $restoredCount procedures restored." -ForegroundColor Green
```

### Rollback Triggers

**When to Rollback**:

1. **Deployment fails mid-execution**: Script errors, connection failures, syntax errors in procedure files
2. **Smoke tests fail post-deployment**: Any of 5 critical procedures return errors
3. **Production errors detected**: Error log spike (>10 database errors in 5 minutes) during monitoring window
4. **Application crashes**: Multiple users report crashes or data integrity issues

**Rollback Decision Window**: First 1 hour after deployment (monitoring period)

### Rollback Execution

**Steps** (15-minute RTO):

1. **Stop application services** (prevent new database operations)
2. **Execute rollback script** with validated backup file path
3. **Validate restoration** (procedure count matches pre-deployment, spot-check 5 critical procedures)
4. **Restart application services**
5. **Smoke test validation** (execute same 5 critical procedures as post-deployment)
6. **Notify stakeholders** (deployment rolled back, original procedures restored)
7. **Root cause analysis** (review deployment logs, identify failure cause)

**Success Criteria**: Application returns to pre-deployment functionality within 15 minutes.

### Post-Rollback Actions

**Immediate** (Day 0):

-   Document root cause of deployment failure
-   Identify specific procedure(s) or process step(s) that failed
-   Update deployment script to prevent recurrence

**Short-Term** (Day 1-3):

-   Fix identified issues in UpdatedStoredProcedures/ files
-   Re-test deployment on test database (T120 repeat)
-   Schedule new production deployment window

**Long-Term** (Week 1-2):

-   Review rollback execution, identify process improvements
-   Update rollback documentation based on lessons learned
-   Consider blue/green deployment approach for future major deployments

---

**Document Version**: 1.0  
**Last Updated**: 2025-10-15  
**Next Review**: After Phase 2.5 completion
