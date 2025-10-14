# Tasks: Comprehensive Database Layer Refactor

**Input**: Design documents from `/specs/002-comprehensive-database-layer/`  
**Prerequisites**: plan.md ✅, spec.md ✅, research.md ✅, data-model.md ✅, contracts/ ✅, quickstart.md ✅

**Tests**: Integration tests included per FR-018 requirement for per-test-class transaction validation

**Organization**: Tasks grouped by user story to enable independent implementation and testing

## Format: `[ID] [P?] [Story] Description`
- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (US1-US5 from spec.md)
- Exact file paths included in descriptions

## Path Conventions
- Root: `C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\`
- Models: `Models/`
- Helpers: `Helpers/`
- Data (DAOs): `Data/`
- Forms: `Forms/`
- Controls: `Controls/`
- Services: `Services/`
- Tests: `Tests/Integration/`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Initialize INFORMATION_SCHEMA parameter cache and DaoResult foundation

- [ ] T001 [P] Create `Models/Model_DaoResult.cs` with base DaoResult class (IsSuccess, Message, Exception properties, Success/Failure factory methods)
- [ ] T002 [P] Create `Models/Model_DaoResult_Generic.cs` with DaoResult<T> generic class extending DaoResult with Data property
- [ ] T003 Create `Models/Model_ParameterPrefixCache.cs` with dictionary structure for INFORMATION_SCHEMA caching and GetParameterPrefix lookup method
- [ ] T004 Update `Program.cs` to query INFORMATION_SCHEMA.PARAMETERS at startup, populate ParameterPrefixCache dictionary, log initialization timing (~100-200ms expected)

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core Helper_Database_StoredProcedure refactor - MUST complete before ANY DAO refactoring

**⚠️ CRITICAL**: No DAO work can begin until Helper refactor is complete

- [ ] T005 Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus` to query ParameterPrefixCache, apply detected prefixes to parameters, return DaoResult (async only, remove useAsync parameter)
- [ ] T006 Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteDataTableWithStatus` to query ParameterPrefixCache, apply detected prefixes, return DaoResult<DataTable> (async only)
- [ ] T007 Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteScalarWithStatus` to query ParameterPrefixCache, apply detected prefixes, return DaoResult<T> (async only)
- [ ] T008 Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteWithCustomOutput` to query ParameterPrefixCache, apply detected prefixes, return DaoResult<Dictionary<string, object>> (async only)
- [ ] T009 Add retry logic to all 4 Helper execution methods for transient errors (1205 deadlock, 1213 lock timeout, 2006 server gone, 2013 lost connection) with 3 attempts and exponential backoff
- [ ] T010 Add performance monitoring to all 4 Helper execution methods with configurable thresholds (Query: 500ms, Modification: 1000ms, Batch: 5000ms, Report: 2000ms) and warning logs when exceeded
- [ ] T011 Update `Helpers/Helper_Database_Variables.cs` to add TestDatabaseName constant = "mtm_wip_application_winform_test" per clarification Q8
- [ ] T012 Update `Logging/LoggingUtility.cs` to add recursive prevention check in LogDatabaseError method (catch exceptions, fallback to file logging if log_error table unavailable)
- [ ] T013 Create integration test infrastructure: `Tests/Integration/BaseIntegrationTest.cs` with [TestInitialize] BeginTransaction and [TestCleanup] Rollback using TestDatabaseName connection string

**Checkpoint**: Foundation ready - all Helper execution methods return DaoResult variants with parameter prefix detection

---

## Phase 3: User Story 1 - Developer Adds New Database Operation (Priority: P1) 🎯 MVP

**Goal**: Standardize DAO pattern to eliminate parameter prefix errors and provide consistent error handling

**Independent Test**: Create test stored procedure, implement DAO method, verify DaoResult responses with valid/invalid data

### Tests for User Story 1

**NOTE: Write these tests FIRST using BaseIntegrationTest, ensure they FAIL before DAO implementation**

- [ ] T014 [P] [US1] Create `Tests/Integration/Dao_System_Tests.cs` with test for GetDatabaseVersionAsync (basic SELECT query returning DaoResult<DataTable>)
- [ ] T015 [P] [US1] Create `Tests/Integration/Dao_ErrorLog_Tests.cs` with tests for LogErrorAsync (INSERT operation returning DaoResult, test valid data and invalid data scenarios)
- [ ] T016 [P] [US1] Create `Tests/Integration/Helper_Database_StoredProcedure_Tests.cs` with tests for parameter prefix detection (verify p_, in_, o_ prefixes correctly detected from ParameterPrefixCache)

### Implementation for User Story 1

- [ ] T017 [P] [US1] Refactor `Data/Dao_System.cs` to async methods returning DaoResult variants (GetDatabaseVersionAsync, CheckConnectivityAsync, GetSettingsAsync) routing through Helper_Database_StoredProcedure
- [ ] T018 [P] [US1] Refactor `Data/Dao_ErrorLog.cs` to async methods returning DaoResult variants (LogErrorAsync with recursive prevention, GetErrorsAsync, SearchErrorsAsync)
- [ ] T019 [US1] Update `Program.cs` to use Dao_System.CheckConnectivityAsync for startup database validation per FR-014, display actionable error message if database unavailable, terminate gracefully on failure
- [ ] T020 [US1] Add XML documentation to Model_DaoResult.cs, Model_DaoResult_Generic.cs following documentation standards (summary, param, returns, example tags)

**Checkpoint**: Basic DAO pattern established with System and ErrorLog DAOs - can create new database operations following this pattern

---

## Phase 4: User Story 2 - Reliable Database Operations (Priority: P1)

**Goal**: Ensure all database operations handle errors gracefully with consistent status processing

**Independent Test**: Execute 100 consecutive operations, force disconnect mid-operation, verify proper status codes without crashes

### Tests for User Story 2

- [ ] T021 [P] [US2] Create `Tests/Integration/Dao_Inventory_Tests.cs` with 100 consecutive AddInventoryAsync operations, verify all return DaoResult.Success with Status=0
- [ ] T022 [P] [US2] Create `Tests/Integration/ConnectionPooling_Tests.cs` with 100 concurrent GetInventoryAsync operations, verify connection pool healthy (5-100 connections), all operations succeed
- [ ] T023 [P] [US2] Create `Tests/Integration/TransactionManagement_Tests.cs` with multi-step TransferInventoryAsync, force mid-operation failure, verify rollback with zero orphaned records

### Implementation for User Story 2

- [ ] T024 [P] [US2] Refactor `Data/Dao_Inventory.cs` to async methods returning DaoResult variants (GetAllInventoryAsync, AddInventoryAsync, RemoveInventoryAsync, TransferInventoryAsync, SearchInventoryAsync, UpdateInventoryAsync) with proper transaction management for TransferInventoryAsync multi-step operation
- [ ] T025 [P] [US2] Refactor `Data/Dao_Transactions.cs` to async methods returning DaoResult variants (LogTransactionAsync, GetTransactionHistoryAsync, SearchTransactionsAsync) routing through Helper_Database_StoredProcedure
- [ ] T026 [P] [US2] Refactor `Data/Dao_History.cs` to async methods returning DaoResult variants (GetInventoryHistoryAsync, GetRemoveHistoryAsync, GetTransferHistoryAsync)
- [ ] T027 [US2] Update `Forms/MainForm/MainForm.cs` inventory operations to async/await patterns: btnAdd_Click, btnRemove_Click, btnTransfer_Click event handlers using await Dao_Inventory methods, check DaoResult.IsSuccess before updating UI
- [ ] T028 [US2] Update `Forms/Transactions/Transactions.cs` to async/await patterns: LoadTransactionsAsync, btnSearch_Click event handlers using await Dao_Transactions methods
- [ ] T029 [US2] Integrate Service_ErrorHandler with DaoResult failures: display result.Message in MessageBox on IsSuccess=false, log result.Exception if not null

**Checkpoint**: Core inventory and transaction operations reliable with consistent error handling - 100 consecutive operations succeed

---

## Phase 5: User Story 3 - Developer Troubleshoots Database Issues (Priority: P2)

**Goal**: Comprehensive error logging with full context and clear messages

**Independent Test**: Trigger error conditions, review log_error table and Service_DebugTracer output, verify all context included

### Tests for User Story 3

- [ ] T030 [P] [US3] Create `Tests/Integration/ErrorLogging_Tests.cs` with forced connection failure, verify log_error entry includes User, Severity, ErrorType, ErrorMessage, StackTrace, MethodName, MachineName, OSVersion, AppVersion, ErrorTime
- [ ] T031 [P] [US3] Create `Tests/Integration/ValidationErrors_Tests.cs` with null required parameter to DAO method, verify user-friendly validation message returned (not MySQL exception), detailed technical info logged
- [ ] T032 [P] [US3] Create `Tests/Integration/ErrorCooldown_Tests.cs` with same error triggered 10 times within 5 seconds, verify all logged to database but UI shows MessageBox only once

### Implementation for User Story 3

- [ ] T033 [P] [US3] Refactor `Data/Dao_User.cs` to async methods returning DaoResult variants (AuthenticateUserAsync, GetAllUsersAsync, CreateUserAsync, UpdateUserAsync, DeleteUserAsync)
- [ ] T034 [P] [US3] Refactor `Data/Dao_Part.cs` to async methods returning DaoResult variants (GetPartAsync, CreatePartAsync, UpdatePartAsync, DeletePartAsync, SearchPartsAsync)
- [ ] T035 [US3] Add Service_DebugTracer integration to all DAO methods: TraceMethodEntry with parameters at method start, TraceMethodExit with result before return
- [ ] T036 [US3] Implement error cooldown mechanism in Service_ErrorHandler: track last error message and timestamp, suppress duplicate UI errors within 5 seconds, still log all occurrences to database
- [ ] T037 [US3] Add three-tier severity classification to LoggingUtility.LogDatabaseError per clarification Q5: Critical (data integrity risk), Error (operation failed), Warning (unexpected but handled)
- [ ] T038 [US3] Update User Management controls to async/await patterns: `Controls/SettingsForm/Control_Add_User.cs`, `Controls/SettingsForm/Control_Edit_User.cs`, `Controls/SettingsForm/Control_Remove_User.cs` - LoadUsersAsync, btnSave_Click, btnDelete_Click event handlers

**Checkpoint**: Comprehensive error logging operational - can troubleshoot production issues with full context

---

## Phase 6: User Story 4 - Database Administrator Maintains Schema (Priority: P2)

**Goal**: Uniform parameter naming and output conventions across all stored procedures

**Independent Test**: Query all stored procedures for consistency, run validation script, confirm 0 inconsistencies

### Tests for User Story 4

- [ ] T039 [P] [US4] Create `Tests/Integration/StoredProcedureValidation_Tests.cs` with query to verify all 60+ procedures have OUT p_Status and OUT p_ErrorMsg parameters
- [ ] T040 [P] [US4] Create `Scripts/Validate-Parameter-Prefixes.ps1` PowerShell script to query INFORMATION_SCHEMA.PARAMETERS, check all parameters use standard prefixes (p_, in_, o_), report inconsistencies
- [ ] T041 [P] [US4] Create `Tests/Integration/ParameterNaming_Tests.cs` to verify stored procedure parameter names match C# model properties in PascalCase (PartID, Location, Operation)

### Implementation for User Story 4

- [ ] T042 [P] [US4] Refactor `Data/Dao_Location.cs` to async methods returning DaoResult variants (GetAllLocationsAsync, CreateLocationAsync, UpdateLocationAsync, DeleteLocationAsync)
- [ ] T043 [P] [US4] Refactor `Data/Dao_Operation.cs` to async methods returning DaoResult variants (GetAllOperationsAsync, CreateOperationAsync, UpdateOperationAsync, DeleteOperationAsync)
- [ ] T044 [P] [US4] Refactor `Data/Dao_ItemType.cs` to async methods returning DaoResult variants (GetAllItemTypesAsync, CreateItemTypeAsync, UpdateItemTypeAsync, DeleteItemTypeAsync)
- [ ] T045 [P] [US4] Refactor `Data/Dao_QuickButtons.cs` to async methods returning DaoResult variants (GetQuickButtonsAsync, SaveQuickButtonAsync, DeleteQuickButtonAsync)
- [ ] T046 [US4] Update Location Management controls to async/await patterns: `Controls/SettingsForm/Control_Add_Location.cs`, `Controls/SettingsForm/Control_Edit_Location.cs`, `Controls/SettingsForm/Control_Remove_Location.cs` - LoadLocationsAsync, btnSave_Click event handlers
- [ ] T047 [US4] Update Operation Management controls to async/await patterns: `Controls/SettingsForm/Control_Add_Operation.cs`, `Controls/SettingsForm/Control_Edit_Operation.cs`, `Controls/SettingsForm/Control_Remove_Operation.cs` - LoadOperationsAsync, btnSave_Click event handlers
- [ ] T048 [US4] Update `Controls/MainForm/Control_QuickButtons.cs` to async/await patterns: LoadQuickButtonsAsync, btnQuickButton_Click event handlers
- [ ] T049 [US4] Run `Scripts/Validate-Parameter-Prefixes.ps1` against production database, fix any reported inconsistencies in stored procedures or DAO code

**Checkpoint**: All stored procedures follow uniform conventions - parameter validation script reports 0 inconsistencies

---

## Phase 7: User Story 5 - Performance Analyst Reviews Execution (Priority: P3)

**Goal**: Visibility into database operation timing and connection pool metrics

**Independent Test**: Execute large queries, concurrent operations, batch modifications, verify timing logged and pool healthy

### Tests for User Story 5

- [ ] T050 [P] [US5] Create `Tests/Integration/PerformanceMonitoring_Tests.cs` with inventory search returning 10,000+ rows, verify operation timing logged, warning if >500ms threshold exceeded for Query category
- [ ] T051 [P] [US5] Create `Tests/Integration/ConcurrentOperations_Tests.cs` with 100 concurrent GetInventoryAsync calls, verify connection pool handles load (5-100 connections healthy), all operations succeed
- [ ] T052 [P] [US5] Create `Tests/Integration/TransactionRollback_Tests.cs` with batch removal of 100 items in transaction, force mid-batch error, verify complete rollback with no partial commits

### Implementation for User Story 5

- [ ] T053 [P] [US5] Add operation category detection to Helper_Database_StoredProcedure based on stored procedure name patterns: *_get_*/*_search_* = Query (500ms), *_add_*/*_update_*/*_delete_* = Modification (1000ms), *_batch_*/*_bulk_* = Batch (5000ms), *_report_*/*_summary_* = Report (2000ms)
- [ ] T054 [P] [US5] Add performance threshold configuration to Model_AppVariables: QueryThresholdMs, ModificationThresholdMs, BatchThresholdMs, ReportThresholdMs with defaults from FR-020
- [ ] T055 [P] [US5] **OPTION A - MainForm Tab Controls (MVP)**: Update `Controls/MainForm/Control_InventoryTab.cs` to async/await patterns: LoadInventoryAsync, grid refresh operations, search functionality using await Dao_Inventory methods
- [ ] T056 [P] [US5] **OPTION A**: Update `Controls/MainForm/Control_AdvancedInventory.cs` to async/await patterns: LoadInventoryAsync, advanced search/filter operations, bulk operations using await Dao_Inventory methods
- [ ] T057 [P] [US5] **OPTION A**: Update `Controls/MainForm/Control_RemoveTab.cs` to async/await patterns: LoadRemoveHistoryAsync, removal operations using await Dao_History.GetRemoveHistoryAsync
- [ ] T058 [P] [US5] **OPTION A**: Update `Controls/MainForm/Control_AdvancedRemove.cs` to async/await patterns: LoadRemoveHistoryAsync, advanced filtering, bulk removal operations using await Dao_History methods
- [ ] T059 [P] [US5] **OPTION A**: Update `Controls/MainForm/Control_TransferTab.cs` to async/await patterns: LoadTransferHistoryAsync, transfer operations using await Dao_History.GetTransferHistoryAsync
- [ ] T060 [P] [US5] **OPTION A**: Validate all MainForm tab controls updated - verify async patterns, DaoResult handling, no blocking .Result/.Wait() calls
- [ ] T061 [P] [US5] **OPTION B - User Management (Comprehensive)**: Update `Controls/SettingsForm/Control_Add_User.cs` to async/await patterns: LoadUsersAsync, btnSave_Click using await Dao_User.CreateUserAsync
- [ ] T062 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Edit_User.cs` to async/await patterns: LoadUsersAsync, btnSave_Click using await Dao_User.UpdateUserAsync
- [ ] T063 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Remove_User.cs` to async/await patterns: LoadUsersAsync, btnDelete_Click using await Dao_User.DeleteUserAsync
- [ ] T064 [P] [US5] **OPTION B - Location Management**: Update `Controls/SettingsForm/Control_Add_Location.cs` to async/await patterns: LoadLocationsAsync, btnSave_Click using await Dao_Location.CreateLocationAsync
- [ ] T065 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Edit_Location.cs` to async/await patterns: LoadLocationsAsync, btnSave_Click using await Dao_Location.UpdateLocationAsync
- [ ] T066 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Remove_Location.cs` to async/await patterns: LoadLocationsAsync, btnDelete_Click using await Dao_Location.DeleteLocationAsync
- [ ] T067 [P] [US5] **OPTION B - Operation Management**: Update `Controls/SettingsForm/Control_Add_Operation.cs` to async/await patterns: LoadOperationsAsync, btnSave_Click using await Dao_Operation.CreateOperationAsync
- [ ] T068 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Edit_Operation.cs` to async/await patterns: LoadOperationsAsync, btnSave_Click using await Dao_Operation.UpdateOperationAsync
- [ ] T069 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Remove_Operation.cs` to async/await patterns: LoadOperationsAsync, btnDelete_Click using await Dao_Operation.DeleteOperationAsync
- [ ] T070 [P] [US5] **OPTION B - Part Management**: Update `Controls/SettingsForm/Control_Add_PartID.cs` to async/await patterns: LoadPartsAsync, btnSave_Click using await Dao_Part.CreatePartAsync
- [ ] T071 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Edit_PartID.cs` to async/await patterns: LoadPartsAsync, btnSave_Click using await Dao_Part.UpdatePartAsync
- [ ] T072 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Remove_PartID.cs` to async/await patterns: LoadPartsAsync, btnDelete_Click using await Dao_Part.DeletePartAsync
- [ ] T073 [P] [US5] **OPTION B - ItemType Management**: Update `Controls/SettingsForm/Control_Add_ItemType.cs` to async/await patterns: LoadItemTypesAsync, btnSave_Click using await Dao_ItemType.CreateItemTypeAsync
- [ ] T074 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Edit_ItemType.cs` to async/await patterns: LoadItemTypesAsync, btnSave_Click using await Dao_ItemType.UpdateItemTypeAsync
- [ ] T075 [P] [US5] **OPTION B**: Update `Controls/SettingsForm/Control_Remove_ItemType.cs` to async/await patterns: LoadItemTypesAsync, btnDelete_Click using await Dao_ItemType.DeleteItemTypeAsync
- [ ] T076 [P] [US5] **OPTION B**: Validate all Settings controls updated - verify async patterns, DaoResult handling, no blocking .Result/.Wait() calls across all 18 controls
- [ ] T077 [US5] Create `Services/Service_Startup.cs` to encapsulate Program.cs startup validation logic (database connectivity, parameter cache initialization, connection pool health check)
- [ ] T078 [US5] Create performance dashboard in Service_DebugTracer: track average execution time per stored procedure, connection pool statistics (active/idle/total connections), slow query frequency per category
- [ ] T079 [US5] Add connection pool health check to startup validation: verify pool configuration (MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30), log pool statistics after initialization

**Checkpoint**: Performance monitoring operational - slow queries logged with category-specific thresholds, connection pool healthy under load

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: Documentation, validation, and final cleanup

- [ ] T080 [P] Update `Documentation/Copilot Files/04-patterns-and-templates.md` with DaoResult<T> pattern examples, Helper_Database_StoredProcedure usage, async DAO method templates
- [ ] T081 [P] Update `README.md` Database Access Patterns section with INFORMATION_SCHEMA parameter caching explanation, DaoResult wrapper pattern, async-first architecture
- [ ] T082 [P] Create `Documentation/Database-Layer-Migration-Guide.md` with before/after comparison, migration checklist for developers, troubleshooting common async migration issues
- [ ] T083 Run complete manual validation per success criteria SC-001 through SC-010: zero parameter errors, 100% Helper routing, all 60+ procedures tested, <5% performance variance, pool health, error logging without recursion, <15min new operation time, 90% ticket reduction target, transaction rollback verification, <3s startup validation
- [ ] T084 Review all DAO files for region organization per constitution Principle III: #region Initialization, #region Public Methods, #region Protected Methods, #region Private Methods, #region Static Methods, #region Event Handlers, #region Properties, #region Fields, #region Dispose, #region Nested Types
- [ ] T085 Run static code analysis to verify 100% of database operations in Data/ folder route through Helper_Database_StoredProcedure (no direct MySqlConnection/MySqlCommand usage) per SC-002
- [ ] T086 Execute `Scripts/Validate-Parameter-Prefixes.ps1` final validation across all 60+ stored procedures, confirm 0 inconsistencies reported
- [ ] T087 Run quickstart.md validation: follow quickstart guide to create test DAO method, verify guide accurate and complete in <15 minutes per SC-007

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
  - Creates DaoResult foundation and ParameterPrefixCache structure
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
  - Refactors Helper_Database_StoredProcedure to use ParameterPrefixCache and return DaoResult
  - **⚠️ CRITICAL**: No DAO refactoring can begin until Phase 2 complete
- **User Stories (Phase 3-7)**: All depend on Foundational phase completion
  - US1 (Phase 3): Establishes DAO pattern with System and ErrorLog DAOs
  - US2 (Phase 4): Core Inventory/Transactions - depends on US1 pattern established
  - US3 (Phase 5): User/Part DAOs with enhanced logging - can parallel with US2 if staffed
  - US4 (Phase 6): Location/Operation/ItemType/QuickButtons - can parallel with US2/US3 if staffed
  - US5 (Phase 7): Performance monitoring - depends on all DAOs refactored to measure timing
- **Polish (Phase 8)**: Depends on all user stories complete

### User Story Dependencies

- **User Story 1 (P1 - Phase 3)**: 
  - Depends: Foundational complete
  - Blocks: US2 (establishes DAO pattern that US2 follows)
  - Independent: Can test System and ErrorLog DAOs standalone

- **User Story 2 (P1 - Phase 4)**: 
  - Depends: US1 complete (needs DAO pattern established)
  - Blocks: None (other stories can parallel)
  - Independent: Can test Inventory/Transactions DAOs standalone

- **User Story 3 (P2 - Phase 5)**: 
  - Depends: Foundational complete, ideally US1 pattern established
  - Blocks: None
  - Independent: Can test User/Part DAOs standalone
  - **Parallel opportunity**: Can work alongside US2/US4 if multiple developers

- **User Story 4 (P2 - Phase 6)**: 
  - Depends: Foundational complete, ideally US1 pattern established
  - Blocks: None
  - Independent: Can test Location/Operation/ItemType/QuickButtons DAOs standalone
  - **Parallel opportunity**: Can work alongside US2/US3 if multiple developers

- **User Story 5 (P3 - Phase 7)**: 
  - Depends: All DAOs refactored (US1-US4 complete) to measure performance accurately
  - Blocks: None
  - Independent: Can test performance monitoring standalone once DAOs ready

### Within Each User Story

- Tests FIRST (ensure they FAIL before implementation)
- DAO refactoring before Form/Control updates (Forms depend on async DAO methods)
- Form/Control updates can parallel (different files marked [P])
- Complete story validation before moving to next priority

### Parallel Opportunities

#### Phase 1 (Setup) - All tasks can parallel:
```
T001 Model_DaoResult.cs
T002 Model_DaoResult_Generic.cs  
T003 Model_ParameterPrefixCache.cs
T004 Program.cs INFORMATION_SCHEMA initialization
```

#### Phase 2 (Foundational) - Sequential execution required:
- T005-T008 must complete first (Helper execution methods)
- T009-T010 depend on T005-T008 (add retry/performance to Helper)
- T011-T013 can parallel with T009-T010

#### Phase 3 (US1) - Tests can parallel, then DAOs:
```
# Parallel: All tests
T014 Dao_System_Tests.cs
T015 Dao_ErrorLog_Tests.cs
T016 Helper_Database_StoredProcedure_Tests.cs

# Parallel: All DAOs
T017 Dao_System.cs
T018 Dao_ErrorLog.cs

# Sequential
T019 Program.cs startup validation (depends on T017)
T020 XML documentation (depends on T001-T002)
```

#### Phase 4 (US2) - Major parallel opportunity:
```
# Parallel: All tests
T021 Dao_Inventory_Tests.cs
T022 ConnectionPooling_Tests.cs
T023 TransactionManagement_Tests.cs

# Parallel: All DAOs
T024 Dao_Inventory.cs (complex - TransferInventoryAsync transaction)
T025 Dao_Transactions.cs
T026 Dao_History.cs

# Parallel: All Forms
T027 MainForm.cs inventory operations
T028 Transactions.cs (CORRECTED PATH)
T029 Service_ErrorHandler integration (different file)
```

#### Phase 5 (US3) - Can FULLY parallel with Phase 4 if multiple developers:
```
# Parallel: All tests
T030 ErrorLogging_Tests.cs
T031 ValidationErrors_Tests.cs
T032 ErrorCooldown_Tests.cs

# Parallel: All DAOs
T033 Dao_User.cs
T034 Dao_Part.cs

# Sequential within US3
T035 Service_DebugTracer integration (depends on T033-T034)
T036 Error cooldown in Service_ErrorHandler
T037 Three-tier severity in LoggingUtility
T038 UserManagementForm.cs (depends on T033)
```

#### Phase 6 (US4) - Can FULLY parallel with Phase 4/5 if multiple developers:
```
# Parallel: All tests
T039 StoredProcedureValidation_Tests.cs
T040 Validate-Parameter-Prefixes.ps1 script
T041 ParameterNaming_Tests.cs

# Parallel: All DAOs
T042 Dao_Location.cs
T043 Dao_Operation.cs
T044 Dao_ItemType.cs
T045 Dao_QuickButtons.cs

# Parallel: All Forms/Controls
T046 LocationManagementForm.cs
T047 OperationManagementForm.cs
T048 QuickButtonsControl.cs

# Final sequential
T049 Run validation script (depends on T040)
```

#### Phase 7 (US5) - Depends on all DAOs complete:
```
# Parallel: All tests
T050 PerformanceMonitoring_Tests.cs
T051 ConcurrentOperations_Tests.cs
T052 TransactionRollback_Tests.cs

# Parallel: Helper updates
T053 Operation category detection
T054 Performance threshold configuration

# Parallel: OPTION A - MainForm Tab Controls (6 controls - MVP scope)
T055 Control_InventoryTab.cs async migration
T056 Control_AdvancedInventory.cs async migration
T057 Control_RemoveTab.cs async migration
T058 Control_AdvancedRemove.cs async migration
T059 Control_TransferTab.cs async migration
T060 MainForm tab validation (verify all 6 async compliant)

# Parallel: OPTION B - Settings Controls (18 controls - Comprehensive scope)
T061-T063 User Management controls (Add/Edit/Remove)
T064-T066 Location Management controls (Add/Edit/Remove)
T067-T069 Operation Management controls (Add/Edit/Remove)
T070-T072 Part Management controls (Add/Edit/Remove)
T073-T075 ItemType Management controls (Add/Edit/Remove)
T076 Settings controls validation (verify all 18 async compliant)

# Service and Infrastructure
T077 Service_Startup.cs creation for startup validation encapsulation

# Sequential: Dashboard and health check
T078 Performance dashboard in Service_DebugTracer
T079 Connection pool health check
```

#### Phase 8 (Polish) - Major parallel opportunity:
```
# Parallel: All documentation
T080 04-patterns-and-templates.md
T081 README.md
T082 Database-Layer-Migration-Guide.md

# Sequential: Validation tasks
T083 Manual validation (depends on all implementation)
T084 Region organization review
T085 Static code analysis
T086 Parameter prefix validation script
T087 Quickstart.md validation
```

---

## Parallel Example: Phase 4 (User Story 2)

```bash
# Launch all US2 tests together:
├── T021: Dao_Inventory_Tests.cs (100 consecutive operations)
├── T022: ConnectionPooling_Tests.cs (100 concurrent operations)
└── T023: TransactionManagement_Tests.cs (rollback validation)

# Launch all US2 DAOs together (after tests written):
├── T024: Dao_Inventory.cs (includes TransferInventoryAsync with transaction)
├── T025: Dao_Transactions.cs
└── T026: Dao_History.cs

# Launch all US2 Forms together (after DAOs refactored):
├── T027: MainForm.cs (btnAdd_Click, btnRemove_Click, btnTransfer_Click)
├── T028: Transactions.cs (LoadTransactionsAsync, btnSearch_Click) [CORRECTED]
└── T029: Service_ErrorHandler integration (separate file, no conflict)
```

**Time Savings**: Sequential execution ~12 hours, parallel execution ~4 hours for US2 implementation

---

## Implementation Strategy

### MVP First (User Stories 1 + 2 Only)

**Rationale**: US1 and US2 are both P1 priority and deliver core database layer reliability. This represents minimum viable product for the refactor.

1. ✅ Complete Phase 1: Setup (DaoResult foundation, ParameterPrefixCache)
2. ✅ Complete Phase 2: Foundational (Helper_Database_StoredProcedure refactor) - **CRITICAL GATE**
3. ✅ Complete Phase 3: User Story 1 (DAO pattern with System/ErrorLog)
4. ✅ Complete Phase 4: User Story 2 (Inventory/Transactions DAOs + Forms async migration)
5. **STOP and VALIDATE**: 
   - Run T021-T023 integration tests
   - Execute 100 consecutive inventory operations
   - Force disconnect and verify graceful error handling
   - Verify connection pool healthy under load
6. **MVP Ready**: Core database operations reliable with uniform pattern
7. Deploy to staging for validation before continuing to US3-US5

**Success Criteria for MVP**:
- SC-001: Zero parameter errors ✅
- SC-002: 100% Helper routing ✅ (at least for System/ErrorLog/Inventory/Transactions DAOs)
- SC-005: Connection pool healthy ✅
- SC-009: Transaction rollback working ✅
- SC-010: Startup validation working ✅

### Incremental Delivery

1. **Foundation** (Phase 1-2) → DaoResult pattern and Helper refactored
2. **MVP** (Phase 3-4) → Core database operations reliable (System, ErrorLog, Inventory, Transactions)
3. **Enhanced Logging** (Phase 5) → User/Part DAOs with Service_DebugTracer integration, error cooldown
4. **Schema Consistency** (Phase 6) → Location/Operation/ItemType/QuickButtons DAOs, parameter validation
5. **Performance** (Phase 7) → Monitoring dashboard, all remaining Forms/Controls/Services async
6. **Polish** (Phase 8) → Documentation, validation, final cleanup

**Each increment delivers value**:
- After Phase 4: Core manufacturing operations (inventory add/remove/transfer) work reliably
- After Phase 5: Troubleshooting capabilities enhanced with comprehensive logging
- After Phase 6: All master data maintenance (locations, operations, item types) standardized
- After Phase 7: Performance visibility and optimization complete
- After Phase 8: Full documentation and validation

### Parallel Team Strategy

With 3 developers available after Foundational phase complete:

1. **Team completes Phase 1-2 together** (Setup + Foundational) - ~2-3 days
2. **Phase 3 (US1)** - Single developer establishes pattern - ~1-2 days
3. **Phase 4-6 in parallel** (once US1 pattern established):
   - **Developer A**: User Story 2 (Inventory/Transactions) - ~3-4 days
   - **Developer B**: User Story 3 (User/Part + enhanced logging) - ~3-4 days  
   - **Developer C**: User Story 4 (Location/Operation/ItemType/QuickButtons) - ~3-4 days
4. **Phase 7 (US5)** - Team collaboration for performance monitoring - ~2-3 days
5. **Phase 8 (Polish)** - Parallel documentation + sequential validation - ~2-3 days

**Time Savings**: Sequential ~20-25 days, parallel (3 developers) ~10-12 days

---

## Scope Options Summary

### Option A: MainForm Tab Controls (MVP - 6 controls)
**Tasks T055-T060**: Focus on high-traffic manufacturing operations
- Control_InventoryTab.cs - Primary inventory view
- Control_AdvancedInventory.cs - Complex inventory operations
- Control_RemoveTab.cs - Removal tracking
- Control_AdvancedRemove.cs - Bulk removal operations
- Control_TransferTab.cs - Transfer operations
- Validation task

**Rationale**: These controls handle the core manufacturing workflows (inventory add/remove/transfer) and represent the highest-value async migration targets. Completing Option A delivers MVP scope with maximum impact.

### Option B: Settings Controls (Comprehensive - 18 controls)
**Tasks T061-T076**: Complete settings management async migration
- User Management (3 controls): Add/Edit/Remove user operations
- Location Management (3 controls): Add/Edit/Remove location operations
- Operation Management (3 controls): Add/Edit/Remove operation operations
- Part Management (3 controls): Add/Edit/Remove part operations
- ItemType Management (3 controls): Add/Edit/Remove item type operations
- Validation task

**Rationale**: Settings controls follow predictable CRUD patterns (Add/Edit/Remove per entity). All 18 controls use the same DAO async methods (already refactored in Phases 4-6), making this straightforward parallel work. Completing Option B provides comprehensive coverage across all master data maintenance operations.

### Implementation Strategy
- **MVP First**: Complete Option A (T055-T060) to deliver core manufacturing operations async
- **Comprehensive Coverage**: Add Option B (T061-T076) for complete settings management async migration
- **Parallel Execution**: Both options highly parallelizable - different files, no dependencies
- **Validation Gates**: T060 validates Option A completion, T076 validates Option B completion

**Total Task Count**: 87 tasks (up from 67) with expanded scope including both options

---

## Notes

- **[P] tasks**: Different files, no dependencies, can execute in parallel
- **[Story] label**: Maps task to specific user story (US1-US5) for traceability
- **Each user story independently testable**: Can validate at each checkpoint
- **Tests written FIRST**: Ensure tests FAIL before implementation (TDD approach)
- **Commit strategy**: Atomic commits by DAO file or Form file to enable easy rollback
- **Avoid**: Vague tasks, same-file conflicts, cross-story dependencies that break independence
- **Constitution compliance**: All tasks preserve region organization (Principle III), async-first (Principle VI), manual validation (Principle IV)
- **Critical gate**: Phase 2 completion BLOCKS all DAO work - verify Helper refactor complete before proceeding
- **Scope expansion**: Tasks T055-T076 represent OPTION A (MVP - 6 MainForm tabs) and OPTION B (Comprehensive - 18 Settings controls) verified as existing and ready for async migration
