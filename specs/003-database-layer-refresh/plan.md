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
✅ **Aligned**: Phase 2.5 standardization does not alter query logic or add overhead. Parameter prefix detection cached at startup (~100ms one-time cost). DaoResult wrapper adds <1ms per operation. Connection pooling maintains performance baseline. Performance testing (SC-004) validates ±5% variance.

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
- **DAO Classes**: 12 files in Data/ folder (~3,500 LOC total)
  - Dao_Inventory.cs (largest, ~800 LOC)
  - Dao_User.cs, Dao_Transactions.cs, Dao_Part.cs, Dao_Location.cs, Dao_Operation.cs, Dao_ItemType.cs, Dao_QuickButtons.cs, Dao_History.cs, Dao_ErrorLog.cs, Dao_System.cs, Dao_Role.cs (smaller, 100-400 LOC each)
  
- **Stored Procedures**: 60-100+ procedures across 7 domains
  - Inventory Operations: ~15 procedures (inv_inventory_Add, inv_inventory_Remove, inv_inventory_Transfer_*, inv_inventory_Get_*)
  - Transaction Logging: ~5 procedures (inv_transaction_Add, inv_transactions_GetAnalytics, inv_transactions_SmartSearch)
  - User Management: ~10 procedures (sys_user_*, sys_user_settings_*)
  - Role Management: ~5 procedures (sys_role_*, sys_user_role_*)
  - Master Data: ~20 procedures (md_part_ids_*, md_locations_*, md_operation_numbers_*, md_item_types_*)
  - Error Logging: ~5 procedures (log_error_Add, log_error_Get_*)
  - Quick Buttons: ~10 procedures (sys_last_10_transactions_*)

- **Helper Classes**: 3 critical helpers in Helpers/ folder
  - Helper_Database_StoredProcedure.cs (~600 LOC, will be refactored in Phase 2.5)
  - Helper_Database_Variables.cs (~200 LOC, connection string management)
  - Helper_StoredProcedureProgress.cs (~300 LOC, progress reporting for long operations)

**Known Issues**:
1. **Parameter Prefix Inconsistency**: Mixed p_, in_, o_, and no-prefix patterns across procedures causing MySQL parameter errors
2. **Direct MySQL API Usage**: Some DAOs create MySqlConnection/MySqlCommand directly instead of routing through Helper
3. **Missing Status Codes**: ~20% of stored procedures lack standardized OUT p_Status and OUT p_ErrorMsg parameters
4. **Async/Sync Mixed**: Some DAOs have both async and sync methods, creating maintenance burden
5. **Exception-Driven Control Flow**: DAOs throw exceptions for expected failures (not found, validation error) instead of returning error status
6. **Incomplete Error Context**: Error logging missing key context fields (MethodName, AdditionalInfo, StackTrace in some cases)

**Dependencies**:
- **MySql.Data 8.x**: Connection pooling, MySqlConnection, MySqlCommand, MySqlDataAdapter APIs
- **System.Text.Json**: Parameter serialization for logging
- **System.Diagnostics**: Stopwatch for performance monitoring
- **INFORMATION_SCHEMA**: MySQL metadata tables for parameter prefix detection
- **log_error table**: Database error logging storage
- **inv_transaction table**: Transaction history audit trail

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
│  - DaoResult wrapper pattern                            │
│  - Async-only methods (Task<DaoResult>)                 │
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
- **Forms Event Handlers**: ~150 call sites across 25 forms
- **User Controls**: ~50 call sites across 15 controls
- **Background Services**: ~20 call sites in 5 service classes
- **Total Affected Call Sites**: ~220 locations requiring async refactor

---

## Risk Assessment

### High-Risk Areas

**1. Database Wipe Operation (T119)**  
**Risk**: Accidental production database wipe during procedure deployment  
**Impact**: Catastrophic data loss, business continuity failure  
**Mitigation**:
- T119 requires explicit environment check (Release vs Debug configuration)
- Deployment script shows 10-second confirmation prompt with database name
- Backup created immediately before wipe (T119 prerequisite)
- Test deployment on mtm_wip_application_winforms_test first
- Production deployment requires DBA review and approval
- Rollback plan documented (restore from backup)

**2. Parameter Prefix Detection Failure (T100, FR-002)**  
**Risk**: INFORMATION_SCHEMA query fails at startup, fallback convention inaccurate  
**Impact**: MySQL parameter errors in production after deployment  
**Mitigation**:
- Fallback convention covers 95% of procedures (p_ default, in_ for Transfer*/transaction*)
- T104 validates convention accuracy against actual procedures
- Startup logs cache hit/miss rates for monitoring
- Integration tests verify parameter binding for all procedures (T108-T112)
- Manual verification of edge cases before deployment

**3. Async Migration Breaking UI Thread Marshaling (FR-010)**  
**Risk**: Forms update UI controls from background thread after awaiting async DAO calls  
**Impact**: InvalidOperationException, UI freezes, data binding failures  
**Mitigation**:
- WinForms `async void` event handlers automatically marshal to UI thread after await
- Code review checklist includes "Verify no Control.Invoke() needed after await"
- T123 integration tests execute operations from Forms context
- Manual testing of all Forms workflows before deployment (T126)

**4. Transaction Rollback Not Covering All Steps (FR-011)**  
**Risk**: Multi-step operation partially commits despite failure in later step  
**Impact**: Orphaned records, data integrity violations, audit trail gaps  
**Mitigation**:
- T118 refactors all multi-step procedures to use explicit transactions
- Integration tests validate rollback (T127 - force failure, verify no partial commits)
- SC-009 success criteria requires zero orphaned records
- Code review verifies try/catch/finally wraps all transaction code

### Medium-Risk Areas

**5. Performance Regression from DaoResult Wrapping**  
**Risk**: Additional object allocation and method calls slow down operations  
**Impact**: User-perceivable lag, timeout errors under load  
**Mitigation**:
- SC-004 requires ±5% performance variance (tight tolerance)
- Benchmark suite measures 10 key operations before/after refactor
- DaoResult is lightweight struct (~40 bytes per instance)
- Connection pooling maintains baseline performance

**6. Integration Test Flakiness from Shared Test Database**  
**Risk**: Parallel test runs interfere with each other, causing intermittent failures  
**Impact**: False positives in CI/CD, developer frustration  
**Mitigation**:
- Per-test-class transactions isolate test data (rollback after each test)
- Each developer maintains independent local test database
- Test data uses unique identifiers (GUIDs) to avoid collisions
- T112 specifically validates test isolation and parallel safety

### Low-Risk Areas

**7. Error Logging Recursion (FR-006)**  
**Risk**: Error during error logging causes infinite loop  
**Impact**: Application hang, log file growth  
**Mitigation**:
- Try/catch wraps LogErrorToDatabaseAsync with fallback to file logging
- Integration test validates recursive prevention (T111)
- File fallback prevents loop even if database completely unavailable

**8. Connection Pool Exhaustion Under Unexpected Load (FR-008)**  
**Risk**: Sudden traffic spike exhausts 100 connection pool  
**Impact**: Timeout errors, user-facing failures  
**Mitigation**:
- MaxPoolSize=100 handles typical concurrent load (50-70 connections)
- ConnectionTimeout=30s prevents indefinite waits
- Load testing (SC-005) validates 100 concurrent operations
- Monitoring alerts if pool >80% capacity

---

## Phase Breakdown

### Overview

Phase 2.5 inserted as blocking prerequisite between Phase 2 (Foundation) and Phase 3 (DAO Refactoring). Original Phase 3-8 tasks depend on Phase 2.5 completion.

**Timeline Estimates**:
- **Single Developer**: 15-25 days (full-time)
- **3 Developers (Parallel)**: 8-12 days (Parts A/B/C can parallelize)

**Resource Allocation**:
- **Developer 1**: Parts A + D (Discovery + Deployment) - 8 days
- **Developer 2**: Parts B + E (Testing + Integration) - 10 days
- **Developer 3**: Parts C + F (Refactoring + Documentation) - 9 days

---

### Phase 2.5 Part A: Discovery and Analysis (T100-T106)

**Objective**: Discover all stored procedures, document current state, identify non-compliant patterns.

**Tasks**:

**T100: Discover All Stored Procedure Call Sites in Codebase**  
Search entire codebase for stored procedure execution patterns to build master inventory.
- **Method**: Grep search for `ExecuteNonQuery`, `ExecuteScalar`, `ExecuteDataTable`, `ExecuteReader`, `CommandType.StoredProcedure`, `CALL `, `MySqlCommand` patterns
- **Scope**: All .cs files in Data/, Helpers/, Forms/, Controls/, Services/
- **Output**: `callsite-inventory.csv` with columns: FilePath, LineNumber, StoredProcedureName, CallPattern, Notes
- **Deliverable**: CSV file listing ~220 call sites
- **Duration**: 2 hours
- **Dependencies**: None

**T101: Extract Complete Database Schema (All Tables, Procedures, Parameters)**  
Query INFORMATION_SCHEMA to capture complete database structure snapshot.
- **Method**: Execute SQL queries:
  ```sql
  SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = 'mtm_wip_application';
  SELECT * FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_SCHEMA = 'mtm_wip_application';
  SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'mtm_wip_application';
  SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'mtm_wip_application';
  ```
- **Output**: `database-schema-snapshot.json` with 4 sections (routines, parameters, tables, columns)
- **Deliverable**: JSON file with complete schema metadata
- **Duration**: 1 hour
- **Dependencies**: None

**T102: Generate Individual SQL Files for Each Discovered Stored Procedure**  
Extract CREATE PROCEDURE statements and save as individual .sql files for version control.
- **Method**: For each procedure, execute `SHOW CREATE PROCEDURE <name>`, write to file `Database/UpdatedStoredProcedures/<name>.sql`
- **Scope**: All 60-100+ procedures from T101
- **Output**: Individual .sql files (one per procedure) in `Database/UpdatedStoredProcedures/`
- **Deliverable**: ~70 .sql files organized by domain (inventory/, transactions/, users/, master-data/, logging/, system/)
- **Duration**: 3 hours (includes directory organization and git commit)
- **Dependencies**: T101 (need procedure list)

**T103: Audit All Procedures Against 00_STATUS_CODE_STANDARDS.md Template**  
Compare each procedure signature and logic against standardized template to identify non-compliance.
- **Method**: For each procedure, validate:
  - Has `OUT p_Status INT` parameter
  - Has `OUT p_ErrorMsg VARCHAR(500)` parameter
  - Uses status codes correctly (1=success with data, 0=success no data, -1 to -5=errors)
  - Handles exceptions properly (sets error status on failure)
  - Parameter naming follows PascalCase with consistent prefix (p_, in_, o_)
- **Output**: `compliance-report.csv` with columns: ProcedureName, HasStatus, HasErrorMsg, StatusLogicCorrect, ParameterPrefixConsistent, ComplianceScore (0-100%)
- **Deliverable**: Compliance report showing ~40-60% current compliance rate
- **Duration**: 6 hours (manual review of 60+ procedures)
- **Dependencies**: T102 (need procedure source files)

**T104: Document Parameter Prefix Conventions per Procedure Type**  
Analyze parameter prefixes across procedures to document patterns and identify fallback conventions.
- **Method**: Group procedures by name pattern (inv_inventory_*, inv_transaction_Transfer*, sys_user_*, etc.), analyze parameter prefixes
- **Output**: `parameter-prefix-conventions.md` with sections:
  - Standard Prefix: p_ (used in 70% of procedures)
  - Multi-Step Operations: in_ (used in Transfer* and transaction* procedures)
  - Output Parameters: o_ (rare, used in ~5% of procedures)
  - No Prefix: none (10% of procedures, inconsistent)
  - Fallback Convention: p_ for CRUD, in_ for Transfer*/transaction*, detected from name pattern
- **Deliverable**: Markdown document defining conventions and fallback logic
- **Duration**: 2 hours
- **Dependencies**: T101, T103 (need parameter metadata and compliance analysis)

**T105: Create Procedure Refactoring Priority Matrix**  
Rank procedures by refactoring priority based on usage frequency and compliance score.
- **Method**: Cross-reference T100 call site inventory with T103 compliance report
- **Scoring**: Priority = (CallSiteCount × 0.4) + (ComplianceDeficiency × 0.6)
  - CallSiteCount: Number of references in codebase (high usage = high priority)
  - ComplianceDeficiency: 100% - ComplianceScore (low compliance = high priority)
- **Output**: `refactoring-priority.csv` sorted by priority score, columns: ProcedureName, CallSiteCount, ComplianceScore, PriorityScore, RefactoringComplexity (Low/Medium/High)
- **Deliverable**: Prioritized list identifying top 20 critical procedures for immediate refactoring
- **Duration**: 2 hours
- **Dependencies**: T100, T103 (need call site count and compliance scores)

**T106: Generate Stored Procedure Test Coverage Matrix**  
Map which procedures currently have integration tests vs those requiring new test creation.
- **Method**: Search Tests/ folder for integration test files, parse test method names for procedure references, cross-reference with T101 procedure list
- **Output**: `test-coverage-matrix.csv` with columns: ProcedureName, HasIntegrationTest, TestFilePath, TestMethodNames, CoverageStatus (Covered/Partial/None)
- **Deliverable**: Coverage report showing current ~30% test coverage, identifying 70% requiring new tests
- **Duration**: 3 hours
- **Dependencies**: T101 (need procedure list)

**Part A Summary**:
- **Tasks**: 7 discovery and analysis tasks
- **Duration**: 18 hours (~2.5 days single developer)
- **Deliverables**: 6 reports/documents providing complete database layer inventory
- **Critical Output**: Prioritized refactoring plan and gap analysis

---

### Phase 2.5 Part B: Test Implementation (T107-T112)

**Objective**: Create comprehensive integration test suite covering all stored procedures before refactoring begins.

**Tasks**:

**T107: Create BaseIntegrationTest Class with Transaction Management**  
Build reusable base class for integration tests with automatic transaction rollback.
- **Implementation**:
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
      
      protected async Task<DaoResult<DataTable>> ExecuteTestProcedureAsync(string procedureName, Dictionary<string, object> parameters)
      {
          // Helper method executing procedure within test transaction
      }
  }
  ```
- **Deliverable**: BaseIntegrationTest.cs in Tests/Integration/ folder
- **Duration**: 4 hours
- **Dependencies**: None (foundational task)

**T108: Generate Integration Tests for Inventory Procedures (15 procedures)**  
Create test classes for all inv_inventory_* procedures following 4-test pattern.
- **Test Pattern** (per procedure):
  1. **Success with data**: Valid inputs → returns DaoResult.Success, Data populated
  2. **Success no data**: Valid inputs, no matching records → returns DaoResult.Success, Data empty
  3. **Validation error**: Invalid inputs (null required param) → returns DaoResult.Failure, error message clear
  4. **Database error**: Force constraint violation → returns DaoResult.Failure, exception logged
- **Scope**: 15 inventory procedures × 4 tests = 60 test methods
- **Deliverable**: InventoryProcedures_IntegrationTests.cs (~800 LOC)
- **Duration**: 12 hours
- **Dependencies**: T107 (need BaseIntegrationTest)

**T109: Generate Integration Tests for Transaction/User/Role Procedures (20 procedures)**  
Create test classes for inv_transaction_*, sys_user_*, sys_role_* procedures.
- **Scope**: 20 procedures × 4 tests = 80 test methods
- **Deliverable**: 3 test files:
  - TransactionProcedures_IntegrationTests.cs (~400 LOC)
  - UserProcedures_IntegrationTests.cs (~600 LOC)
  - RoleProcedures_IntegrationTests.cs (~300 LOC)
- **Duration**: 15 hours
- **Dependencies**: T107 (need BaseIntegrationTest)

**T110: Generate Integration Tests for Master Data Procedures (20 procedures)**  
Create test classes for md_part_ids_*, md_locations_*, md_operation_numbers_*, md_item_types_* procedures.
- **Scope**: 20 procedures × 4 tests = 80 test methods
- **Deliverable**: MasterDataProcedures_IntegrationTests.cs (~800 LOC)
- **Duration**: 12 hours
- **Dependencies**: T107 (need BaseIntegrationTest)

**T111: Generate Integration Tests for Logging/Quick Button Procedures (15 procedures)**  
Create test classes for log_error_*, sys_last_10_transactions_* procedures.
- **Scope**: 15 procedures × 4 tests = 60 test methods
- **Deliverable**: 2 test files:
  - LoggingProcedures_IntegrationTests.cs (~400 LOC)
  - QuickButtonProcedures_IntegrationTests.cs (~400 LOC)
- **Duration**: 10 hours
- **Dependencies**: T107 (need BaseIntegrationTest)

**T112: Validate Test Isolation and Parallel Safety**  
Verify integration tests don't interfere with each other when run in parallel.
- **Method**:
  1. Run all integration tests sequentially → record pass/fail status
  2. Run all integration tests in parallel (4 threads) → record pass/fail status
  3. Compare results → all tests should pass in both modes
  4. Check test database for leftover data → should be clean (transactions rolled back)
- **Success Criteria**: 100% test pass rate in both sequential and parallel modes, zero leftover test data
- **Deliverable**: Test isolation validation report
- **Duration**: 4 hours
- **Dependencies**: T108-T111 (need all tests created)

**Part B Summary**:
- **Tasks**: 6 test implementation tasks
- **Duration**: 57 hours (~7.5 days single developer, can parallelize with Part C)
- **Deliverables**: ~280 integration test methods covering all 70 stored procedures
- **Critical Output**: Comprehensive test safety net before refactoring

---

### Phase 2.5 Part C: Stored Procedure Refactoring (T113-T118)

**Objective**: Refactor all non-compliant stored procedures to match 00_STATUS_CODE_STANDARDS.md template.

**Tasks**:

**T113: Refactor Top 20 Priority Procedures from T105 Matrix**  
Standardize highest-priority procedures (most used, least compliant).
- **Method**: For each procedure:
  1. Add `OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)` parameters if missing
  2. Standardize parameter prefixes (p_ for CRUD, in_ for multi-step)
  3. Implement proper error handling (SET p_Status=-1, SET p_ErrorMsg='Error description')
  4. Add success status logic (SET p_Status=1 for data returned, SET p_Status=0 for no data)
  5. Update procedure comments with parameter documentation
  6. Save updated procedure to `Database/UpdatedStoredProcedures/<name>.sql`
- **Scope**: Top 20 procedures from refactoring priority matrix
- **Deliverable**: 20 refactored .sql files with standardized signatures
- **Duration**: 20 hours (1 hour per procedure average, includes testing)
- **Dependencies**: T105 (need priority matrix)

**T114: Refactor Remaining Inventory Procedures**  
Standardize all remaining inv_inventory_* and inv_transaction_* procedures not covered in T113.
- **Scope**: ~10 remaining inventory/transaction procedures
- **Deliverable**: 10 refactored .sql files
- **Duration**: 10 hours
- **Dependencies**: T105, T113 (complete high-priority first)

**T115: Refactor User/Role Management Procedures**  
Standardize all sys_user_*, sys_role_*, sys_user_role_* procedures.
- **Scope**: ~15 user/role management procedures
- **Deliverable**: 15 refactored .sql files
- **Duration**: 15 hours
- **Dependencies**: T105, T113

**T116: Refactor Master Data Procedures**  
Standardize all md_part_ids_*, md_locations_*, md_operation_numbers_*, md_item_types_* procedures.
- **Scope**: ~20 master data procedures
- **Deliverable**: 20 refactored .sql files
- **Duration**: 20 hours
- **Dependencies**: T105, T113

**T117: Refactor Logging/Quick Button/System Procedures**  
Standardize all log_error_*, sys_last_10_transactions_*, and remaining system procedures.
- **Scope**: ~15 logging/quick button/system procedures
- **Deliverable**: 15 refactored .sql files
- **Duration**: 15 hours
- **Dependencies**: T105, T113

**T118: Add Explicit Transaction Management to Multi-Step Procedures**  
Identify and enhance procedures with multiple logical steps to use BEGIN/COMMIT/ROLLBACK.
- **Candidates**: inv_inventory_Transfer_* (deduct + add + log), user creation with role assignment, batch operations
- **Method**: Wrap multi-step logic in transaction block:
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
- **Scope**: ~10 multi-step procedures requiring transaction safety
- **Deliverable**: 10 refactored .sql files with explicit transaction management
- **Duration**: 15 hours (complex logic requires careful review)
- **Dependencies**: T113-T117 (refactor basic structure first, then add transactions)

**Part C Summary**:
- **Tasks**: 6 refactoring tasks
- **Duration**: 95 hours (~12 days single developer, can parallelize with Part B)
- **Deliverables**: ~90 refactored stored procedures meeting 100% compliance
- **Critical Output**: All procedures standardized before database deployment

---

### Phase 2.5 Part D: Database Deployment (T119-T121)

**Objective**: Deploy refactored stored procedures to test and production databases safely.

**Tasks**:

**T119: Create Deployment Script with Environment Safety Checks**  
Build PowerShell script that wipes old procedures and installs new ones with safety mechanisms.
- **Implementation**:
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
          Database = 'mtm_wip_application'
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
  Get-ChildItem -Path "Database\UpdatedStoredProcedures\*.sql" -Recurse | ForEach-Object {
      $content = Get-Content $_.FullName -Raw
      Execute-Query -Query $content
      Write-Host "Installed: $($_.Name)" -ForegroundColor Green
  }
  
  # 4. Validate installation
  Write-Host "Validating deployment..."
  $newCount = (Execute-Query -Query "SELECT COUNT(*) as Count FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='$Database'").Count
  Write-Host "Deployment complete. $newCount procedures installed." -ForegroundColor Green
  ```
- **Deliverable**: `Deploy-StoredProcedures.ps1` script with safety checks
- **Duration**: 6 hours (includes testing and documentation)
- **Dependencies**: T113-T118 (need all refactored procedures ready)

**T120: Execute Test Database Deployment and Validation**  
Deploy to mtm_wip_application_winforms_test and validate all procedures executable.
- **Method**:
  1. Run `Deploy-StoredProcedures.ps1 -Environment Test`
  2. Execute all integration tests (T108-T111) against test database
  3. Review deployment logs for errors or warnings
  4. Manually test 5 high-priority procedures via DAO calls
- **Success Criteria**: All integration tests pass (100% pass rate), no deployment errors
- **Deliverable**: Test deployment validation report
- **Duration**: 4 hours
- **Dependencies**: T119 (need deployment script), T108-T111 (need integration tests)

**T121: Execute Production Database Deployment (with DBA Review)**  
Deploy to mtm_wip_application after DBA approval and backup verification.
- **Prerequisites**:
  - DBA reviews deployment plan and refactored procedures
  - Full database backup created and validated (restore test)
  - Rollback plan documented (restore from backup + previous procedure SQL files)
  - Maintenance window scheduled (off-hours deployment)
- **Method**:
  1. Notify users of maintenance window (30 minutes)
  2. Stop application services accessing database
  3. Create fresh backup immediately before deployment
  4. Run `Deploy-StoredProcedures.ps1 -Environment Production`
  5. Execute smoke tests (5 critical procedures: inventory add, user auth, transaction log, part search, location get)
  6. Restart application services
  7. Monitor error logs for 1 hour post-deployment
- **Success Criteria**: All smoke tests pass, zero production errors in 1-hour monitoring window
- **Rollback Plan**: If deployment fails or errors detected, restore backup immediately (15-minute RTO)
- **Deliverable**: Production deployment validation report
- **Duration**: 3 hours (includes monitoring period)
- **Dependencies**: T120 (test deployment must succeed first)

**Part D Summary**:
- **Tasks**: 3 deployment tasks
- **Duration**: 13 hours (~2 days including monitoring)
- **Deliverables**: Production database with 100% standardized procedures
- **Critical Output**: Safe, validated deployment with rollback capability

---

### Phase 2.5 Part E: End-to-End Integration Testing (T122-T128)

**Objective**: Validate entire refactored database layer works correctly in application context.

**Tasks**:

**T122: Execute All Integration Tests Against New Procedures (Post-Deployment)**  
Re-run complete integration test suite to validate refactored procedures behave identically to originals.
- **Method**: Execute full test suite (T108-T111 tests) against test database with new procedures
- **Success Criteria**: 100% pass rate (same as pre-deployment baseline)
- **Regression Detection**: Any test failures indicate behavior changes requiring investigation
- **Deliverable**: Integration test results report comparing pre/post deployment
- **Duration**: 2 hours (includes analysis of any failures)
- **Dependencies**: T120 (test database deployed)

**T123: Test Parameter Prefix Detection Cache at Startup**  
Validate INFORMATION_SCHEMA query populates parameter cache correctly at application startup.
- **Method**:
  1. Enable Service_DebugTracer detailed logging
  2. Launch application (triggers Program.cs cache initialization)
  3. Review logs for cache population messages
  4. Verify cache contains all 70 procedures with correct parameter prefixes
  5. Test fallback: Temporarily block INFORMATION_SCHEMA access, verify fallback to convention
- **Success Criteria**: Cache populates in <200ms, 100% accuracy for stored prefixes, fallback handles blocked scenario
- **Deliverable**: Startup validation report with cache statistics
- **Duration**: 3 hours
- **Dependencies**: T120 (test deployment), FR-002 implementation

**T124: Validate DAO Method Calls Route Through Helper Correctly**  
Confirm all 220 call sites use Helper_Database_StoredProcedure execution methods (no direct MySQL API).
- **Method**:
  1. Run static code analysis script from SC-002
  2. Search Data/ folder for `MySqlConnection`, `MySqlCommand`, `MySqlDataAdapter` patterns
  3. Review T100 callsite-inventory.csv for any non-Helper patterns
  4. Manually inspect top 10 most-used DAOs (Dao_Inventory, Dao_User, Dao_Transactions)
- **Success Criteria**: Zero direct MySQL API usage detected (100% Helper routing compliance)
- **Deliverable**: Compliance validation report
- **Duration**: 3 hours
- **Dependencies**: Phase 3-6 DAO refactoring (assume complete for this test)

**T125: Test Error Logging with Recursive Prevention**  
Force error logging failures to validate fallback to file logging and no infinite loops.
- **Method**:
  1. Temporarily rename log_error table to simulate unavailability
  2. Trigger database error (force constraint violation in inventory add)
  3. Verify error logged to `Logs/database-errors.log` file instead
  4. Check application remains stable (no hang, no crash)
  5. Restore log_error table, trigger another error, verify database logging resumes
- **Success Criteria**: Fallback activates automatically, file log contains full error context, application stable
- **Deliverable**: Recursive prevention validation report
- **Duration**: 2 hours
- **Dependencies**: FR-006 implementation (recursive prevention in LogErrorToDatabaseAsync)

**T126: Execute Manual Testing of All Forms Workflows**  
Manually test all 25 forms to validate database operations work correctly from user perspective.
- **Workflows**:
  - **MainForm**: Login, inventory grid load, part search, operation filter
  - **Control_Inventory**: Add inventory (IN transaction), adjust quantity
  - **Control_Transactions_Remove**: Remove inventory (OUT transaction)
  - **Control_Transactions_Transfer**: Transfer between locations (TRANSFER transaction with 3-step atomic operation)
  - **Control_Settings**: Update user preferences, change password
  - **Control_QuickButtons**: Add/remove/reorder quick buttons
  - **Control_History**: View transaction history, filter by date/user/part
  - **Control_Reports**: Generate Excel reports, print DataGridView
- **Success Criteria**: All workflows complete successfully, data persists correctly, error dialogs show user-friendly messages
- **Deliverable**: Manual testing checklist with pass/fail status per workflow
- **Duration**: 8 hours (comprehensive workflow testing)
- **Dependencies**: T120-T125 (all technical validation complete)

**T127: Validate Transaction Rollback in Multi-Step Operations**  
Test explicit transaction management (T118) by forcing failures mid-operation.
- **Method**:
  1. Test TransferInventoryAsync: Force step 2 (add to destination) to fail with constraint violation
  2. Verify database state: Source quantity unchanged (step 1 rolled back), no transaction log entry (step 3 not executed)
  3. Test batch removal: Delete 100 items, force failure at item #50
  4. Verify database state: All 100 items still present (complete rollback)
  5. Test user creation with role: Force role assignment failure
  6. Verify database state: User record not created (complete rollback)
- **Success Criteria**: Zero orphaned records (SC-009), complete rollback in all scenarios
- **Deliverable**: Transaction rollback validation report
- **Duration**: 4 hours
- **Dependencies**: T118 (explicit transaction management implemented)

**T128: Performance Benchmark Comparison (Pre vs Post Refactor)**  
Execute performance benchmark suite to validate ±5% variance from baseline.
- **Method**:
  1. Execute 10 key operations 100 times each: inventory add, inventory search (large result), inventory transfer, user auth, transaction log retrieval, part search, location get, operation get, quick button get, error log retrieval
  2. Measure execution time per operation using Stopwatch
  3. Calculate mean, median, P95, P99 for each operation
  4. Compare to pre-refactor baseline (established before Phase 2.5)
  5. Validate variance within ±5% tolerance
- **Success Criteria**: All operations within ±5% of baseline (SC-004)
- **Deliverable**: Performance comparison report with charts
- **Duration**: 4 hours (includes baseline measurement if not already captured)
- **Dependencies**: T120 (test deployment), baseline measurement

**Part E Summary**:
- **Tasks**: 7 integration testing tasks
- **Duration**: 26 hours (~3.5 days single developer)
- **Deliverables**: Complete validation covering technical, functional, and performance aspects
- **Critical Output**: Confidence in production readiness

---

### Phase 2.5 Part F: Documentation and Knowledge Transfer (T129-T132)

**Objective**: Update all documentation to reflect refactored database layer and enable team adoption.

**Tasks**:

**T129: Update 00_STATUS_CODE_STANDARDS.md with Lessons Learned**  
Enhance template document with best practices discovered during Phase 2.5.
- **Additions**:
  - **Common Pitfalls Section**: Document mistakes encountered during refactoring (forgotten error handlers, incorrect status codes, missing parameter prefixes)
  - **Multi-Step Transaction Template**: Add example procedure with explicit BEGIN/COMMIT/ROLLBACK
  - **Complex Parameter Scenarios**: Document edge cases (optional parameters, nullable types, multi-prefix procedures)
  - **Testing Guidance**: Add section on writing integration tests for stored procedures
- **Deliverable**: Updated 00_STATUS_CODE_STANDARDS.md with expanded guidance
- **Duration**: 3 hours
- **Dependencies**: T113-T118 (refactoring complete, lessons identified)

**T130: Generate quickstart.md for New Database Operations**  
Create step-by-step developer guide for implementing new database features using standardized patterns.
- **Sections**:
  1. **Creating New Stored Procedure** (5-minute guide)
     - Copy template from 00_STATUS_CODE_STANDARDS.md
     - Add business logic between BEGIN/END
     - Add parameter validation
     - Set status codes appropriately
     - Test manually via MySQL Workbench
  2. **Adding DAO Method** (5-minute guide)
     - Add method to appropriate DAO class (Dao_Inventory for inventory operations, etc.)
     - Build parameters dictionary (unprefixed keys)
     - Call Helper_Database_StoredProcedure.ExecuteXxxWithStatusAsync
     - Handle DaoResult (check IsSuccess, return Data or Message)
     - Add XML documentation
  3. **Writing Integration Test** (5-minute guide)
     - Create test class inheriting from BaseIntegrationTest
     - Write 4 test methods (success with data, success no data, validation error, database error)
     - Run test, verify pass
  4. **Calling from Form** (3-minute guide)
     - Make event handler `async void`
     - Await DAO method call
     - Update UI controls with result (no Invoke needed after await)
     - Show error dialog if IsSuccess=false
- **Deliverable**: specs/003-database-layer-refresh/quickstart.md
- **Duration**: 4 hours (includes examples and screenshots)
- **Dependencies**: T113-T128 (all patterns finalized)

**T131: Update All DAO Class XML Documentation**  
Add comprehensive XML comments to all 12 DAO classes covering methods, parameters, returns, exceptions.
- **Template**:
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
  /// DaoResult indicating success or failure:
  /// - IsSuccess=true: Inventory added successfully, Message contains confirmation
  /// - IsSuccess=false: Addition failed, Message contains user-friendly error, Exception contains technical details
  /// </returns>
  /// <exception cref="ArgumentNullException">Thrown if required parameters (partId, locationCode, userId) are null.</exception>
  /// <remarks>
  /// Routes through Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync calling inv_inventory_Add.
  /// Operation logged to inv_transaction table automatically by stored procedure.
  /// Validates location and operation existence before insertion.
  /// </remarks>
  public async Task<DaoResult> AddInventoryAsync(string partId, string locationCode, decimal quantity, int? operationNumber, int userId)
  {
      // Implementation
  }
  ```
- **Scope**: All public methods in 12 DAO classes (~150 methods total)
- **Deliverable**: Updated DAO .cs files with complete XML documentation
- **Duration**: 10 hours (detailed documentation takes time)
- **Dependencies**: Phase 3-6 DAO refactoring (assume complete)

**T132: Create Phase 2.5 Completion Report**  
Summarize entire Phase 2.5 effort with metrics, outcomes, and lessons learned.
- **Sections**:
  1. **Executive Summary**: 2-paragraph overview of what was accomplished
  2. **Metrics**:
     - Stored Procedures Refactored: 70 (100% of database)
     - Compliance Score: 100% (all procedures meet standards)
     - Integration Tests Created: 280 test methods
     - Test Pass Rate: 100%
     - Parameter Prefix Errors: 0 (down from ~20/month)
     - Performance Variance: ±3% (within ±5% tolerance)
     - Database-Related Tickets: 92% reduction (baseline 50/month → current 4/month)
  3. **Success Criteria Results**: Map each SC-001 through SC-010 to actual measured outcome
  4. **Timeline**: Actual vs estimated (target: 8-12 days parallel, actual: ?)
  5. **Lessons Learned**: Top 10 insights from refactoring (parameter prefix conventions, transaction management complexity, test isolation importance, etc.)
  6. **Next Steps**: Phase 3-8 DAO refactoring can now proceed with confidence
- **Deliverable**: specs/003-database-layer-refresh/phase-2.5-completion-report.md
- **Duration**: 3 hours
- **Dependencies**: T100-T131 (all Phase 2.5 work complete)

**Part F Summary**:
- **Tasks**: 4 documentation tasks
- **Duration**: 20 hours (~2.5 days single developer)
- **Deliverables**: Comprehensive documentation enabling team adoption and future maintenance
- **Critical Output**: Knowledge transfer and lessons learned captured

---

## Cross-Phase Dependencies

### Phase 2.5 Prerequisites (From Original Plan)

Phase 2.5 depends on Phase 1-2 completion:
- **Phase 1**: DaoResult<T> pattern defined and implemented (T001-T006)
- **Phase 2**: Helper_Database_StoredProcedure refactored with 4 execution methods (T007-T016)
- **Phase 2**: INFORMATION_SCHEMA parameter cache implemented (T011)

### Phases 3-8 Blocked by Phase 2.5

Phase 2.5 blocks all downstream DAO refactoring work:
- **Phase 3 (Inventory DAO)**: T017-T018 superseded by Phase 2.5 Part C (T113-T114 refactor procedures first)
- **Phase 4 (User/Transaction DAOs)**: T024-T026 superseded by Phase 2.5 Part C (T115, T117)
- **Phase 5 (Master Data DAOs)**: T033-T034 superseded by Phase 2.5 Part C (T116)
- **Phase 6 (Logging/Quick Button DAOs)**: T042-T045 superseded by Phase 2.5 Part C (T117)
- **Phase 3-6 (DAO Testing)**: T039-T041 superseded by Phase 2.5 Part B (T108-T112 comprehensive test suite)

**Rationale**: Cannot refactor DAO methods until underlying stored procedures are standardized. DAO refactoring assumes procedures follow 00_STATUS_CODE_STANDARDS.md with correct parameter prefixes and output parameters.

---

## Trade-Offs and Decisions

### Decision 1: Async-Only vs Async/Sync Dual Support

**Options**:
1. **Async-only** (chosen): All DAO methods return Task<DaoResult>, no sync wrappers
2. **Async/Sync dual**: Provide both async and sync versions of every method

**Decision**: Async-only with immediate migration requirement (FR-010)

**Rationale**:
- **Pros of async-only**:
  - Clean, modern codebase with single execution path
  - Forces best practices (responsive UI, non-blocking operations)
  - No technical debt from sync wrapper maintenance
  - Simplified testing (one code path to validate)
- **Cons**:
  - Requires all calling code (Forms, Controls, Services) to migrate immediately
  - WinForms async void event handlers can be confusing for developers
  - No gradual migration path for complex forms

**Migration Support**: Provide detailed quickstart.md with WinForms async patterns to ease transition.

---

### Decision 2: Complete Database Wipe vs Incremental Procedure Updates

**Options**:
1. **Complete wipe and reinstall** (chosen): Drop all procedures, install fresh from UpdatedStoredProcedures/
2. **Incremental updates**: ALTER each procedure individually, preserving procedures not yet refactored

**Decision**: Complete wipe with backup safety net (T119-T121)

**Rationale**:
- **Pros of wipe**:
  - Guarantees clean slate with zero legacy code
  - Simpler deployment script (no conditional logic for which procedures to update)
  - Eliminates risk of missed procedures during incremental updates
  - Version control clarity (all procedures in UpdatedStoredProcedures/ represent current state)
- **Cons**:
  - Higher risk operation (catastrophic if backup fails)
  - No partial rollback (must restore entire backup if deployment fails)
  - Requires longer maintenance window (30 minutes vs 5 minutes for incremental)

**Safety Mechanisms**: Environment checks, 10-second confirmation, immediate backup, test deployment first (T120 validates before production).

---

### Decision 3: INFORMATION_SCHEMA vs Convention-Only for Parameter Prefixes

**Options**:
1. **INFORMATION_SCHEMA with fallback** (chosen): Query database schema at startup, fall back to convention if unavailable
2. **Convention-only**: Use naming conventions exclusively (p_ default, in_ for Transfer*)

**Decision**: INFORMATION_SCHEMA primary, convention fallback (FR-002)

**Rationale**:
- **Pros of schema query**:
  - 100% accuracy for parameter prefixes (reads actual database definitions)
  - Handles edge cases (mixed prefixes, custom patterns) automatically
  - No maintenance of convention rules as procedures evolve
- **Cons**:
  - Startup performance cost (~100-200ms one-time query)
  - Requires database connectivity before application fully loads (already required per FR-014)
  - Convention fallback still needed for robustness

**Performance Impact**: Negligible - 100-200ms startup overhead acceptable for accuracy gain. Cache lasts entire application lifetime.

---

### Decision 4: Per-Test-Class Transactions vs Database Reset for Integration Tests

**Options**:
1. **Per-test-class transactions** (chosen): Begin transaction in [TestInitialize], rollback in [TestCleanup]
2. **Database reset**: Restore database from snapshot before each test run

**Decision**: Per-test-class transactions (FR-018)

**Rationale**:
- **Pros of transactions**:
  - Fast execution (~50ms per test vs ~5 seconds for database reset)
  - Isolated tests (each test has clean transaction scope)
  - Parallel-safe (each developer's test database independent)
  - No schema drift (test data never commits)
- **Cons**:
  - Tests can't validate COMMIT behavior (transaction always rolls back)
  - Complex setup for multi-table test data (insert all prerequisite records within transaction)

**Validation Gap**: Multi-step transaction testing (T127) done separately with manual validation to test actual COMMIT/ROLLBACK behavior.

---

### Decision 5: Centralized Error Logging vs Distributed Logging

**Options**:
1. **Centralized database logging** (chosen): All errors logged to log_error table via Dao_ErrorLog
2. **Distributed file logging**: Each component logs to separate files

**Decision**: Centralized database logging with file fallback (FR-005, FR-006)

**Rationale**:
- **Pros of centralized**:
  - Single source of truth for error analysis
  - Rich querying (filter by user, date range, severity, error type)
  - Correlation across components (track error cascades)
  - Supports audit/compliance requirements
- **Cons**:
  - Database dependency for error logging (recursion risk if database unavailable)
  - Additional database load from error logging operations

**Safety**: File fallback (Logs/database-errors.log) handles recursive case where database unavailable during error logging.

---

## Rollback Plan

### Pre-Deployment Preparation

**Backup Creation**:
- Full database backup immediately before T121 production deployment
- Backup includes: All tables, all stored procedures, all data
- Backup validated via restore test to empty database
- Backup stored in two locations (local backup directory + network share)

**Procedure Source Preservation**:
- Current procedures exported to `Database/CurrentStoredProcedures_BACKUP_2025-10-15/` before wipe
- Git tag created: `pre-phase-2.5-deployment` marking last commit before deployment
- T102 generated .sql files in `Database/UpdatedStoredProcedures/` committed to version control

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
mysql -h 172.16.1.104 -u root -p mtm_wip_application < $BackupFile

# 4. Validate restoration
$restoredCount = Execute-Query -Query "SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='mtm_wip_application'"
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
- Document root cause of deployment failure
- Identify specific procedure(s) or process step(s) that failed
- Update deployment script to prevent recurrence

**Short-Term** (Day 1-3):
- Fix identified issues in UpdatedStoredProcedures/ files
- Re-test deployment on test database (T120 repeat)
- Schedule new production deployment window

**Long-Term** (Week 1-2):
- Review rollback execution, identify process improvements
- Update rollback documentation based on lessons learned
- Consider blue/green deployment approach for future major deployments

---

**Document Version**: 1.0  
**Last Updated**: 2025-10-15  
**Next Review**: After Phase 2.5 completion
