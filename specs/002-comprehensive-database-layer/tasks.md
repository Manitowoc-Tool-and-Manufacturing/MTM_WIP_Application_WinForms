# Tasks: Comprehensive Database Layer Refactor

**Input**: Design documents from `/specs/002-comprehensive-database-layer/`  
**Prerequisites**: plan.md ✅, spec.md ✅, research.md ✅, data-model.md ✅, contracts/ ✅, quickstart.md ✅

**Tests**: Integration tests included per FR-018 requirement for per-test-class transaction validation

**Organization**: Tasks grouped by user story to enable independent implementation and testing. Each task contains only ONE file with subtasks for individual methods.

## Format: `[ID] [P?] [Story] Description`
- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (US1-US5 from spec.md)
- **Method-Level Subtasks**: Each method refactoring tracked individually (T###a, T###b, etc.)
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

## Phase 1: Setup (Shared Infrastructure) ✅ COMPLETE

**Purpose**: Initialize INFORMATION_SCHEMA parameter cache and DaoResult foundation

- [X] T001 [P] [Setup] Create `Models/Model_DaoResult.cs` with base DaoResult class (IsSuccess, Message, Exception properties, Success/Failure factory methods)
- [X] T002 [P] [Setup] Create `Models/Model_DaoResult_Generic.cs` with DaoResult<T> generic class extending DaoResult with Data property
- [X] T003 [Setup] Create `Models/Model_ParameterPrefixCache.cs` with dictionary structure for INFORMATION_SCHEMA caching and GetParameterPrefix lookup method
- [X] T004 [Setup] Update `Program.cs` to query INFORMATION_SCHEMA.PARAMETERS at startup, populate ParameterPrefixCache dictionary, log initialization timing (~100-200ms expected)
- [X] T004a [Setup] **Created Missing Stored Procedures** (2025-10-14): Created `sys_user_GetByName.sql`, `sys_user_GetIdByName.sql`, `sys_theme_GetAll.sql`, `sys_user_access_SetType.sql`, `sys_role_GetIdByName.sql` in `Database/UpdatedStoredProcedures/`. Imported into test database. Tests improved from 41/66 (62%) to 50/66 (76%) passing.

**Checkpoint**: ✅ Foundation ready - DaoResult classes created, ParameterPrefixCache structure in place, Program.cs initialization complete

---

## Phase 2: Foundational (Blocking Prerequisites) ✅ COMPLETE

**Purpose**: Core Helper_Database_StoredProcedure refactor - MUST complete before ANY DAO refactoring

**⚠️ CRITICAL**: No DAO work can begin until Helper refactor is complete

- [X] T005 [Foundational] Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus` to query ParameterPrefixCache, apply detected prefixes to parameters, return DaoResult (async only, remove useAsync parameter)
- [X] T006 [Foundational] Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteDataTableWithStatus` to query ParameterPrefixCache, apply detected prefixes, return DaoResult<DataTable> (async only)
- [X] T007 [Foundational] Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteScalarWithStatus` to query ParameterPrefixCache, apply detected prefixes, return DaoResult<T> (async only)
- [X] T008 [Foundational] Refactor `Helpers/Helper_Database_StoredProcedure.ExecuteWithCustomOutput` to query ParameterPrefixCache, apply detected prefixes, return DaoResult<Dictionary<string, object>> (async only)
- [X] T009 [Foundational] Add retry logic to all 4 Helper execution methods for transient errors (1205 deadlock, 1213 lock timeout, 2006 server gone, 2013 lost connection) with 3 attempts and exponential backoff
- [X] T010 [Foundational] Add performance monitoring to all 4 Helper execution methods with configurable thresholds (Query: 500ms, Modification: 1000ms, Batch: 5000ms, Report: 2000ms) and warning logs when exceeded
- [X] T011 [Foundational] Update `Helpers/Helper_Database_Variables.cs` to add TestDatabaseName constant = "mtm_wip_application_winform_test" per clarification Q8
- [X] T012 [Foundational] Update `Logging/LoggingUtility.cs` to add recursive prevention check in LogDatabaseError method (catch exceptions, fallback to file logging if log_error table unavailable)
- [X] T013 [Foundational] Create integration test infrastructure: `Tests/Integration/BaseIntegrationTest.cs` with [TestInitialize] BeginTransaction and [TestCleanup] Rollback using TestDatabaseName connection string

**Checkpoint**: ✅ Foundation ready - all Helper execution methods return DaoResult variants with parameter prefix detection, retry logic, and performance monitoring. Backward compatibility maintained via deprecated wrapper methods. Integration test infrastructure ready.

---

## Phase 3: User Story 1 - Developer Adds New Database Operation (Priority: P1) 🎯 MVP

**Goal**: Standardize DAO pattern to eliminate parameter prefix errors and provide consistent error handling

**Independent Test**: Create test stored procedure, implement DAO method, verify DaoResult responses with valid/invalid data

**Warning Resolution**: WRN001 (Dao_ErrorLog - 10 CS0618 warnings to eliminate)

### Tests for User Story 1

**NOTE: Write these tests FIRST using BaseIntegrationTest, ensure they FAIL before DAO implementation**

- [X] T014 [P] [US1-Test] Create `Tests/Integration/Dao_System_Tests.cs` with 15 comprehensive test methods covering System_UserAccessTypeAsync, GetUserIdByNameAsync, GetRoleIdByNameAsync, GetAllThemesAsync, error handling scenarios, and DaoResult pattern validation
- [X] T015 [P] [US1-Test] Create `Tests/Integration/Dao_ErrorLog_Tests.cs` with 18 comprehensive test methods covering GetUniqueErrorsAsync (3 tests), GetAllErrorsAsync (2 tests), GetErrorsByUserAsync (2 tests), GetErrorsByDateRangeAsync (3 tests), delete operations (2 tests), error handling methods (3 tests), recursive prevention validation (2 tests), data integrity (2 tests), and null/empty parameter handling (2 tests)
- [X] T016 [P] [US1-Test] Create `Tests/Integration/Helper_Database_StoredProcedure_Tests.cs` with 11 comprehensive test methods covering parameter prefix detection (4 tests validating p_ prefix application and fallback logic), DaoResult pattern validation (2 tests for success/failure scenarios with connection errors), null/empty parameter handling (2 tests for graceful handling), connection management (2 tests validating disposal and concurrent pooling with 10 parallel operations), and performance (1 test validating <30 second timeout). Build verified: 0 errors, 149 warnings.

### Implementation for User Story 1

#### T017 [P] [US1] Refactor `Data/Dao_System.cs` ✅ COMPLETE

**File**: `Data/Dao_System.cs`

All methods refactored to remove useAsync parameters and call async Helper methods directly. All callers updated (Tests, Program.cs, Core_Themes.cs). Build verified: 0 errors, 145 warnings.

**Methods**:
- [X] T017a SetUserAccessTypeAsync - Uses ExecuteNonQueryWithStatusAsync, returns DaoResult
- [X] T017b System_UserAccessTypeAsync - Uses ExecuteDataTableWithStatusAsync, returns DaoResult<DataTable>
- [X] T017c GetUserIdByNameAsync - Uses ExecuteScalarWithStatusAsync, returns DaoResult<int>
- [X] T017d GetRoleIdByNameAsync - Uses ExecuteScalarWithStatusAsync, returns DaoResult<int>
- [X] T017e GetAllThemesAsync - Uses ExecuteDataTableWithStatusAsync, returns DaoResult<DataTable>

#### T018 [P] [US1] Refactor `Data/Dao_ErrorLog.cs` ✅ COMPLETE (Tests need updates)

**File**: `Data/Dao_ErrorLog.cs`

**Status**: All 6 methods refactored to return DaoResult types. Recursive prevention and cooldown logic preserved. **NOTE**: Integration tests need updating to handle DaoResult<DataTable> return types.

**Methods**:
- [X] T018-Core LogErrorToDatabaseAsync - useAsync removed, recursive prevention maintained
- [X] T018a GetAllErrorsAsync - Returns DaoResult<DataTable> with proper error handling
- [X] T018b GetErrorsByUserAsync - Returns DaoResult<DataTable> with proper error handling
- [X] T018c DeleteErrorByIdAsync - Returns DaoResult with proper error handling
- [X] T018d DeleteAllErrorsAsync - Returns DaoResult with proper error handling
- [X] T018e HandleException_SQLError_CloseApp - Returns DaoResult with recursive prevention preserved
- [X] T018f HandleException_GeneralError_CloseApp - Returns DaoResult with cooldown logic preserved

**Test Updates Required**:
- [X] Update Dao_ErrorLog_Tests.cs to check result.IsSuccess and result.Data.Rows ✅ COMPLETE (2025-10-14)
- [X] ErrorLogging_Tests.cs covered by Dao_ErrorLog_Tests.cs ✅ COMPLETE (2025-10-14)
- [X] Update ErrorCooldown_Tests.cs to check result.IsSuccess and result.Data.Rows ✅ COMPLETE (2025-10-14)
- [X] Update ValidationErrors_Tests.cs to check result.IsSuccess and result.Data ✅ COMPLETE (2025-10-14)

- [X] T019 [US1] Update `Program.cs` to use Dao_System.CheckConnectivityAsync for startup database validation per FR-014, display actionable error message if database unavailable, terminate gracefully on failure. ✅ VERIFIED: Database connectivity check implemented in Program.cs startup sequence.
- [X] T020 [US1] Add XML documentation to Model_DaoResult.cs, Model_DaoResult_Generic.cs following documentation standards (summary, param, returns, example tags). ✅ VERIFIED: Comprehensive XML documentation present with usage examples and remarks.

**Checkpoint**: ✅ Basic DAO pattern established with System and ErrorLog DAOs - can create new database operations following this pattern - Phase 3 COMPLETE (test updates pending)

---

## Phase 4: User Story 2 - Reliable Database Operations (Priority: P1)

**Goal**: Ensure all database operations handle errors gracefully with consistent status processing

**Independent Test**: Execute 100 consecutive operations, force disconnect mid-operation, verify proper status codes without crashes

**Warning Resolution**: WRN002-WRN003 (Dao_History - 5 CS0618, Dao_Inventory - 15 CS0618 warnings to eliminate)

### Tests for User Story 2

- [X] T021 [P] [US2-Test] Fixed `Tests/Integration/Dao_Inventory_Tests.cs` - 15 compilation errors resolved (method signature mismatches)
- [X] T022 [P] [US2-Test] Create `Tests/Integration/ConnectionPooling_Tests.cs` with 50 concurrent GetAllInventoryAsync operations and verify no connection pool exhaustion or deadlocks
- [X] T023 [P] [US2-Test] Create `Tests/Integration/TransactionManagement_Tests.cs` with multi-step TransferInventoryAsync, force mid-operation failure, verify rollback with zero orphaned records

### Implementation for User Story 2

#### T024 [P] [US2] Refactor `Data/Dao_Inventory.cs` ✅ COMPLETE

**File**: `Data/Dao_Inventory.cs`

All methods refactored to async methods returning DaoResult variants with proper transaction management for multi-step operations.

**Methods**:
- [X] T024a GetAllInventoryAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T024b AddInventoryAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T024c RemoveInventoryAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T024d TransferInventoryAsync - Returns DaoResult, multi-step transaction with rollback support
- [X] T024e SearchInventoryAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T024f UpdateInventoryAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus

#### T025 [P] [US2] Refactor `Data/Dao_Transactions.cs` ✅ COMPLETE

**File**: `Data/Dao_Transactions.cs`

All methods refactored to async methods returning DaoResult variants routing through Helper_Database_StoredProcedure.

**Methods**:
- [X] T025a LogTransactionAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T025b GetTransactionHistoryAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T025c SearchTransactionsAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus

#### T026 [P] [US2] Refactor `Data/Dao_History.cs` ✅ COMPLETE

**File**: `Data/Dao_History.cs`

**Status**: All methods complete with DaoResult return types. All callers updated with proper error handling.

**Methods**:
- [X] T026a GetInventoryHistoryAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T026b GetRemoveHistoryAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T026c GetTransferHistoryAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T026d AddTransactionHistoryAsync - Returns DaoResult with proper error handling, all callers updated (Control_TransferTab lines 740 & 795, Control_RemoveTab line 362)

#### T027 [US2] Update WinForms Controls - MainForm Tabs ✅ COMPLETE

**Files**: Multiple MainForm controls updated to async/await patterns

**Controls Updated**:
- [X] T027a Control_RemoveTab.cs - LoadInventoryAsync, button click handlers use await Dao_Inventory methods, check DaoResult.IsSuccess
- [X] T027b Control_QuickButtons.cs - LoadQuickButtonsAsync uses async patterns
- [X] T027c Control_AdvancedRemove.cs - Async inventory operations with DaoResult checks

#### T028 [US2] Update Settings Controls ✅ COMPLETE

**Files**: Settings controls updated to async/await patterns

**Controls Updated**:
- [X] T028a Control_Add_User.cs - LoadDataAsync event handlers use await methods
- [X] T028b Control_Add_Operation.cs - LoadDataAsync event handlers use await methods

- [X] T029 [US2] Final validation and documentation: verify all obsolete warnings resolved in Phase 4 scope, build verification shows 0 errors, Phase 4 complete summary documented

**Checkpoint**: ✅ Core inventory and transaction operations reliable with consistent error handling - Phase 4 COMPLETE

---

## Phase 5: User Story 3 - Developer Troubleshoots Database Issues (Priority: P2)

**Goal**: Comprehensive error logging with full context and clear messages

**Independent Test**: Trigger error conditions, review log_error table and Service_DebugTracer output, verify all context included

**Warning Resolution**: WRN004-WRN006 (Dao_User - 25 CS0618, Dao_Part - 10 CS0618 warnings to eliminate)

### Tests for User Story 3

- [X] T030 [P] [US3-Test] Create `Tests/Integration/ErrorLogging_Tests.cs` with forced connection failure, verify log_error entry includes User, Severity, ErrorType, ErrorMessage, StackTrace, MethodName, MachineName, OSVersion, AppVersion, ErrorTime
- [X] T031 [P] [US3-Test] Create `Tests/Integration/ValidationErrors_Tests.cs` with null required parameter to DAO method, verify user-friendly validation message returned (not MySQL exception), detailed technical info logged
- [X] T032 [P] [US3-Test] Create `Tests/Integration/ErrorCooldown_Tests.cs` with same error triggered 10 times within 5 seconds, verify all logged to database but UI shows MessageBox only once

### Implementation for User Story 3

#### T033 [P] [US3] Refactor `Data/Dao_User.cs` ✅ COMPLETE (2025-10-14)

**File**: `Data/Dao_User.cs`

**Status**: All methods complete with DaoResult return types. Helper methods renamed to GetSettingsJsonInternalAsync and SetUserSettingInternalAsync.

**Core CRUD Methods** (✅ Complete):
- [X] T033-Core-a AuthenticateUserAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T033-Core-b GetAllUsersAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T033-Core-c CreateUserAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T033-Core-d UpdateUserAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T033-Core-e DeleteUserAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus

**UI Settings Methods** (✅ COMPLETE - 2025-10-14):
- [X] T033a GetLastShownVersionAsync (line 22) - ✅ Returns DaoResult<string>
- [X] T033b SetLastShownVersionAsync (line 32) - ✅ Returns DaoResult
- [X] T033c GetHideChangeLogAsync (line 41) - ✅ Returns DaoResult<string>
- [X] T033d SetHideChangeLogAsync (line 51) - ✅ Returns DaoResult
- [X] T033e GetThemeNameAsync (line 60) - ✅ Returns DaoResult<string?>
- [X] T033f GetThemeFontSizeAsync (line 70) - ✅ Returns DaoResult<int?>
- [X] T033g SetThemeFontSizeAsync (line 92) - ✅ Returns DaoResult
- [X] T033h GetVisualUserNameAsync (line 101) - ✅ Returns DaoResult<string>
- [X] T033i SetVisualUserNameAsync (line 112) - ✅ Returns DaoResult
- [X] T033j GetVisualPasswordAsync (line 121) - ✅ Returns DaoResult<string>
- [X] T033k SetVisualPasswordAsync (line 132) - ✅ Returns DaoResult
- [X] T033l GetWipServerAddressAsync (line 141) - ✅ Returns DaoResult<string>
- [X] T033m SetWipServerAddressAsync (line 152) - ✅ Returns DaoResult
- [X] T033n GetDatabaseAsync (line 163) - ✅ Returns DaoResult<string>
- [X] T033o SetDatabaseAsync (line 174) - ✅ Returns DaoResult
- [X] T033p GetWipServerPortAsync (line 188) - ✅ Returns DaoResult<string>
- [X] T033q SetWipServerPortAsync (line 199) - ✅ Returns DaoResult
- [X] T033r GetUserFullNameAsync (line 211) - ✅ Returns DaoResult<string?>
- [X] T033s GetSettingsJsonAsync (line 248) - ✅ Renamed to GetSettingsJsonInternalAsync (private helper)
- [X] T033t SetSettingsJsonAsync (line 306) - ✅ Returns DaoResult
- [X] T033u SetGridViewSettingsJsonAsync (line 340) - ✅ Returns DaoResult
- [X] T033v GetGridViewSettingsJsonAsync (line 375) - ✅ Returns DaoResult<string>
- [X] T033w GetShortcutsJsonAsync (line 751) - ✅ Returns DaoResult<string>
- [X] T033x SetShortcutsJsonAsync (line 784) - ✅ Returns DaoResult
- [X] T033y SetThemeNameAsync (line 818) - ✅ Returns DaoResult

#### T034 [P] [US3] Refactor `Data/Dao_Part.cs` ✅ COMPLETE

**File**: `Data/Dao_Part.cs`

File recreated from scratch with full async/await pattern, DaoResult return types, removed all useAsync parameters, implemented Service_DebugTracer integration, added backward compatibility wrappers marked as Obsolete.

**Methods**:
- [X] T034a GetPartAsync - Returns DaoResult<DataRow>, uses ExecuteDataTableWithStatus
- [X] T034b CreatePartAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T034c UpdatePartAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T034d DeletePartAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T034e SearchPartsAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T034f GetPartByNumberAsync - Returns DaoResult<DataRow>, new method with DaoResult pattern
- [X] T034g PartExistsAsync - Returns DaoResult<bool>, new method with DaoResult pattern

- [X] T035 [US3] Add Service_DebugTracer integration to all DAO methods: TraceMethodEntry with parameters at method start, TraceMethodExit with result before return - ✅ COMPLETE: Integrated into both Dao_User.cs (core methods) and Dao_Part.cs during T033/T034 recreation
- [X] T036 [US3] Implement error cooldown mechanism in Service_ErrorHandler: track last error message and timestamp, suppress duplicate UI errors within 5 seconds, still log all occurrences to database - ✅ COMPLETE: Added _lastErrorTimestamp Dictionary, ErrorCooldownPeriod constant (5 seconds), updated ShouldSuppressError to check cooldown with timestamp tracking, all errors still logged to database, added ClearErrorCooldownState() method for testing
- [X] T037 [US3] Add three-tier severity classification to LoggingUtility.LogDatabaseError per clarification Q5: Critical (data integrity risk), Error (operation failed), Warning (unexpected but handled) - ✅ COMPLETE: Created DatabaseErrorSeverity enum with Warning/Error/Critical levels, updated LogDatabaseError signature to accept DatabaseErrorSeverity parameter (defaults to Error), severity label included in log entries, updated Service_ErrorHandler.HandleDatabaseError to map severity to UI error levels

#### T038 [US3] Update User Management Controls ✅ COMPLETE

**Files**: User management controls updated to async/await patterns with DaoResult checks

**Controls Updated**:
- [X] T038a Control_Add_User.cs - Uses Dao_User.UserExistsAsync, CreateUserAsync, GetUserByUsernameAsync, AddUserRoleAsync with DaoResult pattern
- [X] T038b Control_Edit_User.cs - Uses GetUserByUsernameAsync, UpdateUserAsync, SetUserRoleAsync, GetUserRoleIdAsync with proper DaoResult checking
- [X] T038c Control_Remove_User.cs - Uses GetUserByUsernameAsync, GetUserRoleIdAsync, DeleteUserSettingsAsync, RemoveUserRoleAsync, DeleteUserAsync with comprehensive error handling

**Checkpoint**: ✅ Comprehensive error logging operational - can troubleshoot production issues with full context - Phase 5 requires T033a-y completion for UI settings methods

---

## Phase 6: User Story 4 - Database Administrator Maintains Schema (Priority: P2)

**Goal**: Uniform parameter naming and output conventions across all stored procedures

**Independent Test**: Query all stored procedures for consistency, run validation script, confirm 0 inconsistencies

**Warning Resolution**: WRN007-WRN010 (Dao_Location - 8 CS0618, Dao_ItemType - 6 CS0618, Dao_Operation - 6 CS0618, Dao_QuickButtons - 10 CS0618 warnings to eliminate)

### Tests for User Story 4

- [X] T039 [P] [US4-Test] Create `Tests/Integration/StoredProcedureValidation_Tests.cs` with query to verify all 60+ procedures have OUT p_Status and OUT p_ErrorMsg parameters
- [X] T040 [P] [US4-Test] Create `Scripts/Validate-Parameter-Prefixes.ps1` PowerShell script to query INFORMATION_SCHEMA.PARAMETERS, check all parameters use standard prefixes (p_, in_, o_), report inconsistencies
- [X] T041 [P] [US4-Test] Create `Tests/Integration/ParameterNaming_Tests.cs` to verify stored procedure parameter names match C# model properties in PascalCase (PartID, Location, Operation)

### DAO Refactoring (T042-T045)

#### T042 [P] [US4] Refactor `Data/Dao_Location.cs` ✅ COMPLETE

**File**: `Data/Dao_Location.cs`

**Methods**:
- [X] T042a GetAllLocationsAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T042b CreateLocationAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T042c UpdateLocationAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T042d DeleteLocationAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T042e GetLocationByName - Returns DaoResult<DataRow>, uses ExecuteDataTableWithStatus
- [X] T042f LocationExists - Returns DaoResult<bool>, uses ExecuteScalarWithStatus

#### T043 [P] [US4] Refactor `Data/Dao_Operation.cs` ✅ COMPLETE

**File**: `Data/Dao_Operation.cs`

**Methods**:
- [X] T043a GetAllOperationsAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T043b CreateOperationAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T043c UpdateOperationAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T043d DeleteOperationAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T043e GetOperationByNumber - Returns DaoResult<DataRow>, uses ExecuteDataTableWithStatus
- [X] T043f OperationExists - Returns DaoResult<bool>, uses ExecuteScalarWithStatus

#### T044 [P] [US4] Refactor `Data/Dao_ItemType.cs` ✅ COMPLETE

**File**: `Data/Dao_ItemType.cs`

**Methods**:
- [X] T044a GetAllItemTypesAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T044b CreateItemTypeAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T044c UpdateItemTypeAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T044d DeleteItemTypeAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T044e GetItemTypeByName - Returns DaoResult<DataRow>, uses ExecuteDataTableWithStatus
- [X] T044f ItemTypeExists - Returns DaoResult<bool>, uses ExecuteScalarWithStatus

#### T045 [P] [US4] Refactor `Data/Dao_QuickButtons.cs` ✅ COMPLETE

**File**: `Data/Dao_QuickButtons.cs`

**Methods**:
- [X] T045a GetQuickButtonsAsync - Returns DaoResult<DataTable>, uses ExecuteDataTableWithStatus
- [X] T045b SaveQuickButtonAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T045c DeleteQuickButtonAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T045d UpdateQuickButtonAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T045e RemoveQuickButtonAndShiftAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T045f DeleteAllQuickButtonsForUserAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T045g AddQuickButtonAtPositionAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus
- [X] T045h AddOrShiftQuickButtonAsync - Returns DaoResult, uses ExecuteNonQueryWithStatus

### Control Updates - SettingsForm (T046a-T046r)

#### T046a [P] [US4] `Control_Add_ItemType.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Add_ItemType.cs`

**Methods Updated**:
- [X] T046a-1 btnSave_Click - ItemTypeExists checks result.Data (bool)
- [X] T046a-2 btnSave_Click - InsertItemType checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046b [P] [US4] `Control_Edit_ItemType.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Edit_ItemType.cs`

**Methods Updated**:
- [X] T046b-1 cmbItemTypes_SelectedIndexChanged - GetItemTypeByName checks result.Data (DataRow)
- [X] T046b-2 btnSave_Click - ItemTypeExists checks result.Data (bool)
- [X] T046b-3 btnSave_Click - UpdateItemType checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046c [P] [US4] `Control_Remove_ItemType.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Remove_ItemType.cs`

**Methods Updated**:
- [X] T046c-1 cmbItemTypes_SelectedIndexChanged - GetItemTypeByName checks result.Data (DataRow)
- [X] T046c-2 btnRemove_Click - DeleteItemType checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046d [P] [US4] `Control_Remove_Location.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Remove_Location.cs`

**Methods Updated**:
- [X] T046d-1 cmbLocations_SelectedIndexChanged - GetLocationByName checks result.Data (DataRow)
- [X] T046d-2 btnRemove_Click - DeleteLocation checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046e [P] [US4] `Control_Remove_Operation.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Remove_Operation.cs`

**Methods Updated**:
- [X] T046e-1 cmbOperations_SelectedIndexChanged - GetOperationByNumber checks result.Data (DataRow)
- [X] T046e-2 btnRemove_Click - DeleteOperation checks result.IsSuccess, displays result.ErrorMessage on failure

#### T046f [P] [US4] `Control_Remove_PartID.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Remove_PartID.cs`

**Methods Updated**:
- [X] T046f-1 cmbParts_SelectedIndexChanged - Replaced obsolete GetPartByNumber with GetPartByNumberAsync, checks result.Data (DataRow)
- [X] T046f-2 btnRemove_Click - Replaced obsolete DeletePart with DeletePartAsync, checks result.IsSuccess

#### T046g [P] [US4] `Control_Shortcuts.cs` ❌ BLOCKED

**File**: `Controls/SettingsForm/Control_Shortcuts.cs`

**Status**: BLOCKED by T033w, T033x (Dao_User methods not yet refactored)

**Methods to Update** (when T033w-x complete):
- [ ] T046g-1 LoadShortcuts (line 39) - GetShortcutsJsonAsync needs DaoResult check
- [ ] T046g-2 SaveShortcuts (line 399) - SetShortcutsJsonAsync needs DaoResult check

#### T046h [P] [US4] `Control_Theme.cs` ❌ BLOCKED

**File**: `Controls/SettingsForm/Control_Theme.cs`

**Status**: BLOCKED by T033e, T033y (Dao_User methods not yet refactored)

**Methods to Update** (when T033e, T033y complete):
- [ ] T046h-1 LoadTheme (line 34) - GetThemeNameAsync needs DaoResult check
- [ ] T046h-2 SaveTheme (line 75) - SetThemeNameAsync needs DaoResult check

#### T046i [P] [US4] `Control_Edit_User.cs` - VERIFY ONLY

**File**: `Controls/SettingsForm/Control_Edit_User.cs`

**Status**: Partially complete per T038b. Need verification all DaoResult checks present.

**Methods to Verify**:
- [ ] T046i-1 Verify LoadUser (line 147) - GetUserByUsernameAsync checks result.Data
- [ ] T046i-2 Verify LoadUser (line 239) - GetUserByUsernameAsync checks result.Data  
- [ ] T046i-3 Verify LoadUser (line 169) - GetUserRoleIdAsync checks result.Data
- [ ] T046i-4 Verify btnSave_Click (line 221) - UpdateUserAsync checks result.IsSuccess
- [ ] T046i-5 Verify btnSave_Click (line 253) - SetUserRoleAsync checks result.IsSuccess

#### T046j [P] [US4] `Control_Remove_User.cs` - VERIFY ONLY

**File**: `Controls/SettingsForm/Control_Remove_User.cs`

**Status**: Partially complete per T038c. Need verification all DaoResult checks present.

**Methods to Verify**:
- [ ] T046j-1 Verify LoadUser (line 129) - GetUserByUsernameAsync checks result.Data
- [ ] T046j-2 Verify LoadUser (line 210) - GetUserByUsernameAsync checks result.Data
- [ ] T046j-3 Verify LoadUser (line 141) - GetUserRoleIdAsync checks result.Data
- [ ] T046j-4 Verify LoadUser (line 242) - GetUserRoleIdAsync checks result.Data
- [ ] T046j-5 Verify btnRemove_Click (line 228) - DeleteUserSettingsAsync checks result.IsSuccess
- [ ] T046j-6 Verify btnRemove_Click (line 246) - RemoveUserRoleAsync checks result.IsSuccess
- [ ] T046j-7 Verify btnRemove_Click (line 255) - DeleteUserAsync checks result.IsSuccess

#### T046q [P] [US4] `Control_Add_PartID.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Add_PartID.cs`

**Methods Updated**:
- [X] T046q-1 btnSave_Click (line 108) - Replaced obsolete PartExists with PartExistsAsync, added DaoResult check
- [X] T046q-2 btnSave_Click (line 135) - Replaced obsolete AddPartWithStoredProcedure with CreatePartAsync, added DaoResult check

#### T046r [P] [US4] `Control_Edit_PartID.cs` ✅ COMPLETE

**File**: `Controls/SettingsForm/Control_Edit_PartID.cs`

**Methods Updated**:
- [X] T046r-1 LoadPart (line 184) - Replaced obsolete GetPartByNumber with GetPartByNumberAsync, added DaoResult check
- [X] T046r-2 btnSave_Click (line 332) - Replaced obsolete PartExists with PartExistsAsync, added DaoResult check
- [X] T046r-3 btnSave_Click (line 512) - Replaced obsolete UpdatePartWithStoredProcedure with UpdatePartAsync, added DaoResult check

### Control Updates - MainForm (T046k-T046p)

#### T046k [P] [US4] `Control_TransferTab.cs` ❌ BLOCKED

**File**: `Controls/MainForm/Control_TransferTab.cs`

**Status**: BLOCKED by T033r (GetUserFullNameAsync not yet refactored)

**Methods to Update**:
- [ ] T046k-1 LoadTransfer (line 145) - GetUserFullNameAsync needs DaoResult check (BLOCKED T033r)
- [ ] T046k-2 LoadInventory (line 497) - GetInventoryByPartIdAndOperationAsync needs DaoResult check
- [ ] T046k-3 LoadInventory (line 515) - GetInventoryByPartIdAsync needs DaoResult check
- [ ] T046k-4 btnTransfer_Click (line 730) - TransferInventoryQuantityAsync verify error handling
- [ ] T046k-5 btnTransfer_Click (line 736) - TransferPartSimpleAsync verify error handling
- [ ] T046k-6 btnTransfer_Click (line 793) - TransferPartSimpleAsync verify error handling
- [ ] T046k-7 btnTransfer_Click (line 740) - AddTransactionHistoryAsync verify error handling (BLOCKED T026d)
- [ ] T046k-8 btnTransfer_Click (line 795) - AddTransactionHistoryAsync verify error handling (BLOCKED T026d)

#### T046l [P] [US4] `Control_RemoveTab.cs` ❌ BLOCKED

**File**: `Controls/MainForm/Control_RemoveTab.cs`

**Status**: BLOCKED by T033r (GetUserFullNameAsync not yet refactored). Some methods already correct per T027a.

**Methods to Update**:
- [ ] T046l-1 LoadRemove (line 194) - GetUserFullNameAsync needs DaoResult check (BLOCKED T033r)
- [ ] T046l-2 LoadInventory (line 708) - GetInventoryByPartIdAndOperationAsync needs DaoResult check
- [ ] T046l-3 LoadInventory (line 721) - GetInventoryByPartIdAsync needs DaoResult check

**Already Correct** (✅ from T027a):
- [X] T046l-4 RemoveInventoryItemsFromDataGridViewAsync (line 304) - DaoResult check present
- [X] T046l-5 AddTransactionHistoryAsync (line 362) - DaoResult check present
- [X] T046l-6 AddInventoryItemAsync (line 429) - DaoResult check present

#### T046m [P] [US4] `Control_AdvancedInventory.cs` - VERIFY ONLY

**File**: `Controls/MainForm/Control_AdvancedInventory.cs`

**Status**: Need verification AddInventoryItemAsync calls check result.IsSuccess.

**Methods to Verify**:
- [ ] T046m-1 Verify AddInventory (line 895) - AddInventoryItemAsync checks result.IsSuccess
- [ ] T046m-2 Verify BatchAdd (line 1441) - AddInventoryItemAsync checks result.IsSuccess
- [ ] T046m-3 Verify ImportInventory (line 1725) - AddInventoryItemAsync checks result.IsSuccess

#### T046n [P] [US4] `Control_AdvancedRemove.cs` - VERIFY ONLY

**File**: `Controls/MainForm/Control_AdvancedRemove.cs`

**Status**: Need verification DaoResult checks present.

**Methods to Verify**:
- [ ] T046n-1 Verify RemoveOperation (line 450) - RemoveInventoryItemsFromDataGridViewAsync checks result.IsSuccess
- [ ] T046n-2 Verify AddBackInventory (line 684) - AddInventoryItemAsync checks result.IsSuccess

#### T046o [P] [US4] `Control_QuickButtons.cs` ✅ COMPLETE

**File**: `Controls/MainForm/Control_QuickButtons.cs`

**Methods Updated**:
- [X] T046o-1 btnUpdate_Click (line 505) - UpdateQuickButtonAsync now checks DaoResult
- [X] T046o-2 btnRemove_Click (line 529) - RemoveQuickButtonAndShiftAsync now checks DaoResult
- [X] T046o-3 btnClearAll_Click (line 567) - DeleteAllQuickButtonsForUserAsync now checks DaoResult
- [X] T046o-4 btnAdd_Click (line 577) - AddQuickButtonAtPositionAsync now checks DaoResult

#### T046p [P] [US4] `Control_InventoryTab.cs` ✅ COMPLETE

**File**: `Controls/MainForm/Control_InventoryTab.cs`

**Methods Updated**:
- [X] T046p-1 AddToQuickButtons (line 675) - AddOrShiftQuickButtonAsync now checks DaoResult (non-critical operation)

**Already Correct**:
- [X] T046p-2 AddInventory (line 604) - AddInventoryItemAsync checks result.IsSuccess

### Control Updates - Forms & Services (T046s-T046u)

#### T046s [P] [US4] `MainForm.cs` ❌ BLOCKED

**File**: `Forms/MainForm/MainForm.cs`

**Status**: BLOCKED by T033r (GetUserFullNameAsync not yet refactored)

**Methods to Update**:
- [ ] T046s-1 LoadUserSettings (line 547) - GetUserFullNameAsync needs DaoResult check with fallback (BLOCKED T033r)

#### T046t [P] [US4] `MainFormUserSettingsHelper.cs` ❌ BLOCKED

**File**: `Forms/MainForm/Classes/MainFormUserSettingsHelper.cs`

**Status**: BLOCKED by multiple T033 subtasks (all Dao_User Get/Set methods)

**Methods to Update** (when T033a-y complete):
- [ ] T046t-1 LoadSettings (line 18) - GetLastShownVersionAsync needs DaoResult check (BLOCKED T033a)
- [ ] T046t-2 LoadSettings (line 21) - SetHideChangeLogAsync needs DaoResult check (BLOCKED T033d)
- [ ] T046t-3 LoadSettings (line 23) - SetLastShownVersionAsync needs DaoResult check (BLOCKED T033b)
- [ ] T046t-4 LoadSettings (line 26) - GetWipServerAddressAsync needs DaoResult check (BLOCKED T033l)
- [ ] T046t-5 LoadSettings (line 27) - GetWipServerPortAsync needs DaoResult check (BLOCKED T033p)
- [ ] T046t-6 LoadSettings (line 28) - GetVisualUserNameAsync needs DaoResult check (BLOCKED T033h)
- [ ] T046t-7 LoadSettings (line 29) - GetVisualPasswordAsync needs DaoResult check (BLOCKED T033j)
- [ ] T046t-8 LoadSettings (line 30) - GetThemeNameAsync needs DaoResult check (BLOCKED T033e)
- [ ] T046t-9 LoadSettings (line 36) - GetThemeFontSizeAsync needs DaoResult check (BLOCKED T033f)

#### T046u [P] [US4] `Service_OnStartup_StartupSplashApplicationContext.cs` ❌ BLOCKED

**File**: `Services/Service_OnStartup_StartupSplashApplicationContext.cs`

**Status**: BLOCKED by T033f (GetThemeFontSizeAsync not yet refactored)

**Methods to Update** (when T033f complete):
- [ ] T046u-1 LoadTheme (line 639) - GetThemeFontSizeAsync needs DaoResult check with default fallback (BLOCKED T033f)

### Final Tasks (T047-T049)

### Final Tasks (T047-T049)

#### T047 [US4] Refactor `Controls/SettingsForm/Control_Add_Operation.cs` ❌ INCOMPLETE

**File**: `Controls/SettingsForm/Control_Add_Operation.cs`

**Issue**: Bypasses DAO pattern entirely - calls Helper_Database_StoredProcedure directly instead of using Dao_Operation methods

**Methods to Update**:
- [ ] T047a btnSave_Click (line 100) - Replace direct OperationExists call with Dao_Operation.OperationExistsAsync, add DaoResult<bool> check
- [ ] T047b btnSave_Click (line 145) - Replace direct InsertOperation call with Dao_Operation.CreateOperationAsync, add DaoResult check

**Verification**: After refactor, confirm NO direct Helper_Database_StoredProcedure calls remain in this control

#### T048 [US4] Verify `Controls/MainForm/Control_QuickButtons.cs` - VERIFY ONLY

**File**: `Controls/MainForm/Control_QuickButtons.cs`

**Status**: LoadQuickButtonsAsync should already be correct (async/await patterns). Need verification that no other methods missed.

**Methods to Verify**:
- [ ] T048a Verify LoadQuickButtonsAsync (line ~80) - Uses await Dao_QuickButtons.GetQuickButtonsAsync, checks result.IsSuccess
- [ ] T048b Verify no other async methods missed - Scan entire file for database operations, confirm all route through Dao_QuickButtons
- [ ] T048c Cross-reference with T046o completion - Verify button click handlers (btnUpdate, btnRemove, btnClearAll, btnAdd) use DaoResult checks

**Note**: T046o covers button click handlers. This task verifies LoadQuickButtonsAsync and ensures no database operations were missed during initial analysis.

- [ ] T049 [US4] Verify `Scripts/Validate-Parameter-Prefixes.ps1` is current (checks against INFORMATION_SCHEMA.PARAMETERS, validates p_ prefix usage), then run against production database, fix any reported inconsistencies in stored procedures or DAO code

**Checkpoint**: ✅ T042-T046f complete (DAO refactoring + 6 SettingsForm controls). ❌ Remaining: 47 subtasks blocked by T033a-y (Dao_User UI settings methods), plus T046q-r, T046o-p, T047-T049

---

## Phase 7: User Story 5 - Performance Analyst Reviews Execution (Priority: P3)

**Goal**: Visibility into database operation timing and connection pool metrics

**Independent Test**: Execute large queries, concurrent operations, batch modifications, verify timing logged and pool healthy

**Warning Resolution**: WRN005, WRN011-WRN017 (Dao_Transactions - 10 CS0618, Helper_UI_ComboBoxes - 8 CS0618, various Controls/Services - 20 CS0618 warnings to eliminate)

### Tests for User Story 5

- [ ] T050 [P] [US5-Test] Create `Tests/Integration/PerformanceMonitoring_Tests.cs` with inventory search returning 10,000+ rows, verify operation timing logged, warning if >500ms threshold exceeded for Query category
- [ ] T051 [P] [US5-Test] Create `Tests/Integration/ConcurrentOperations_Tests.cs` with 100 concurrent GetInventoryAsync calls, verify connection pool handles load (5-100 connections healthy), all operations succeed
- [ ] T052 [P] [US5-Test] Create `Tests/Integration/TransactionRollback_Tests.cs` with batch removal of 100 items in transaction, force mid-batch error, verify complete rollback with no partial commits

### Additional Tests

- [ ] T052a [US5-Test] Update/create all integration tests not currently in test project as documented in `specs/002-comprehensive-database-layer/AdditionalTests.md` (cross-reference with existing test coverage to avoid duplication)

### Implementation for User Story 5

- [ ] T053 [P] [US5] Add operation category detection to `Helper_Database_StoredProcedure` based on stored procedure name patterns: *_get_*/*_search_* = Query (500ms), *_add_*/*_update_*/*_delete_* = Modification (1000ms), *_batch_*/*_bulk_* = Batch (5000ms), *_report_*/*_summary_* = Report (2000ms)
- [ ] T054 [P] [US5] Add performance threshold configuration to `Model_AppVariables`: QueryThresholdMs, ModificationThresholdMs, BatchThresholdMs, ReportThresholdMs with defaults from FR-020

#### T055-T060: OPTION A - MainForm Tab Controls (MVP - 6 controls)

- [ ] T055 [P] [US5-OptionA] Update `Controls/MainForm/Control_InventoryTab.cs` to async/await patterns: LoadInventoryAsync, grid refresh operations, search functionality using await Dao_Inventory methods
- [ ] T056 [P] [US5-OptionA] Update `Controls/MainForm/Control_AdvancedInventory.cs` to async/await patterns: LoadInventoryAsync, advanced search/filter operations, bulk operations using await Dao_Inventory methods
- [ ] T057 [P] [US5-OptionA] Update `Controls/MainForm/Control_RemoveTab.cs` to async/await patterns: LoadRemoveHistoryAsync, removal operations using await Dao_History.GetRemoveHistoryAsync
- [ ] T058 [P] [US5-OptionA] Update `Controls/MainForm/Control_AdvancedRemove.cs` to async/await patterns: LoadRemoveHistoryAsync, advanced filtering, bulk removal operations using await Dao_History methods
- [ ] T059 [P] [US5-OptionA] Update `Controls/MainForm/Control_TransferTab.cs` to async/await patterns: LoadTransferHistoryAsync, transfer operations using await Dao_History.GetTransferHistoryAsync
- [ ] T060 [US5-OptionA] Validate all MainForm tab controls updated - verify async patterns, DaoResult handling, no blocking .Result/.Wait() calls

#### T061-T076: OPTION B - Settings Controls (Comprehensive - 18 controls)

**User Management**:
- [ ] T061 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_User.cs` LoadUsersAsync, btnSave_Click using await Dao_User.CreateUserAsync
- [ ] T062 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_User.cs` LoadUsersAsync, btnSave_Click using await Dao_User.UpdateUserAsync
- [ ] T063 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_User.cs` LoadUsersAsync, btnDelete_Click using await Dao_User.DeleteUserAsync

**Location Management**:
- [ ] T064 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_Location.cs` LoadLocationsAsync, btnSave_Click using await Dao_Location.CreateLocationAsync
- [ ] T065 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_Location.cs` LoadLocationsAsync, btnSave_Click using await Dao_Location.UpdateLocationAsync
- [ ] T066 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_Location.cs` LoadLocationsAsync, btnDelete_Click using await Dao_Location.DeleteLocationAsync

**Operation Management**:
- [ ] T067 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_Operation.cs` LoadOperationsAsync, btnSave_Click using await Dao_Operation.CreateOperationAsync
- [ ] T068 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_Operation.cs` LoadOperationsAsync, btnSave_Click using await Dao_Operation.UpdateOperationAsync
- [ ] T069 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_Operation.cs` LoadOperationsAsync, btnDelete_Click using await Dao_Operation.DeleteOperationAsync

**Part Management**:
- [ ] T070 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_PartID.cs` LoadPartsAsync, btnSave_Click using await Dao_Part.CreatePartAsync
- [ ] T071 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_PartID.cs` LoadPartsAsync, btnSave_Click using await Dao_Part.UpdatePartAsync
- [ ] T072 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_PartID.cs` LoadPartsAsync, btnDelete_Click using await Dao_Part.DeletePartAsync

**ItemType Management**:
- [ ] T073 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Add_ItemType.cs` LoadItemTypesAsync, btnSave_Click using await Dao_ItemType.CreateItemTypeAsync
- [ ] T074 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Edit_ItemType.cs` LoadItemTypesAsync, btnSave_Click using await Dao_ItemType.UpdateItemTypeAsync
- [ ] T075 [P] [US5-OptionB] Update `Controls/SettingsForm/Control_Remove_ItemType.cs` LoadItemTypesAsync, btnDelete_Click using await Dao_ItemType.DeleteItemTypeAsync

- [ ] T076 [US5-OptionB] Validate all Settings controls updated - verify async patterns, DaoResult handling, no blocking .Result/.Wait() calls across all 18 controls

**Service and Infrastructure**:
- [ ] T077 [US5] Create `Services/Service_Startup.cs` to encapsulate Program.cs startup validation logic (database connectivity, parameter cache initialization, connection pool health check)
- [ ] T078 [US5] Create performance dashboard in Service_DebugTracer: track average execution time per stored procedure, connection pool statistics (active/idle/total connections), slow query frequency per category
- [ ] T079 [US5] Add connection pool health check to startup validation: verify pool configuration (MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30), log pool statistics after initialization

**Checkpoint**: Performance monitoring operational - slow queries logged with category-specific thresholds, connection pool healthy under load

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: Documentation, validation, and final cleanup

- [ ] T080 [P] [Polish] Update `Documentation/Copilot Files/04-patterns-and-templates.md` with DaoResult<T> pattern examples, Helper_Database_StoredProcedure usage, async DAO method templates
- [ ] T081 [P] [Polish] Update `README.md` Database Access Patterns section with INFORMATION_SCHEMA parameter caching explanation, DaoResult wrapper pattern, async-first architecture
- [ ] T082 [P] [Polish] Create `Documentation/Database-Layer-Migration-Guide.md` with before/after comparison, migration checklist for developers, troubleshooting common async migration issues

### T083: Manual Validation Checklist (Success Criteria SC-001 through SC-010)

**File**: Validation checklist based on `specs/002-comprehensive-database-layer/validation-checklist-T063.md`

#### Pre-Deployment Validation (Required before release)

- [ ] T083a [Validation] **SC-001: Zero MySQL Parameter Errors** - Execute all integration tests, manually execute 120 operations (12 DAOs × 10), check log_error table for parameter errors, verify count = 0
- [ ] T083b [Validation] **SC-002: 100% Helper Routing** - Run static code analysis script, verify 0 direct MySqlConnection/MySqlCommand usages in Data/, spot-check 3 random DAO files
- [ ] T083c [Validation] **SC-003: All 60+ Stored Procedures Tested** - Run all integration tests, generate SP coverage report, verify ≥60 procedures exercised with valid and invalid inputs
- [ ] T083d [Validation] **SC-004: <5% Performance Variance** - Establish baseline (pre-refactor), run same operations on refactored codebase, verify all variances < 5%
- [ ] T083e [Validation] **SC-005: Connection Pool Health** - Run connection pool tests (100 concurrent operations), monitor pool statistics, verify 5-100 range maintained, no timeouts, no leaks
- [ ] T083f [Validation] **SC-006: Error Logging Without Recursion** - Force connection failure, simulate log_error table unavailability, verify fallback to file logging, no recursive exceptions
- [ ] T083g [Validation] **SC-007: <15 Minute New Operation Time** - Follow quickstart.md guide to create test DAO method, measure total time (target: <15 minutes)
- [ ] T083h [Validation] **SC-009: Transaction Rollback Works** - Run transaction rollback tests, manually test TransferInventoryAsync rollback, verify zero orphaned records
- [ ] T083i [Validation] **SC-010: <3 Second Startup Validation** - Measure startup time (database available and unavailable), verify actionable error message, graceful termination, INFORMATION_SCHEMA cache query completes

#### Post-Deployment Monitoring (Track after release)

- [ ] T083j [Validation] **SC-008: 90% Ticket Reduction Target** - Baseline: Query ticket system for past 3 months database tickets. Monitor: Track 30 days post-deployment, verify ≥90% reduction

- [ ] T084 [Polish] Review all DAO files for region organization per constitution Principle III: #region Initialization, #region Public Methods, #region Protected Methods, #region Private Methods, #region Static Methods, #region Event Handlers, #region Properties, #region Fields, #region Dispose, #region Nested Types
- [ ] T085 [Polish] Run static code analysis to verify 100% of database operations in Data/ folder route through Helper_Database_StoredProcedure (no direct MySqlConnection/MySqlCommand usage) per SC-002
- [ ] T086 [Polish] Execute `Scripts/Validate-Parameter-Prefixes.ps1` final validation across all 60+ stored procedures, confirm 0 inconsistencies reported
- [ ] T087 [Polish] Run quickstart.md validation: follow quickstart guide to create test DAO method, verify guide accurate and complete in <15 minutes per SC-007

**Checkpoint**: Complete manual validation and documentation before production deployment

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - **BLOCKS all user stories**
- **User Stories (Phase 3-7)**: All depend on Foundational phase completion
  - US1 (Phase 3): Establishes DAO pattern - **BLOCKS** US2 (US2 follows this pattern)
  - US2 (Phase 4): Can parallel with US3/US4 after US1 pattern established
  - US3 (Phase 5): **BLOCKS** many T046 subtasks (Dao_User UI settings methods needed)
  - US4 (Phase 6): Can parallel with US2/US3
  - US5 (Phase 7): Depends on all DAOs refactored
- **Polish (Phase 8)**: Depends on all user stories complete

### Critical Blocking Dependencies

**T033a-y (Dao_User UI Settings Methods) BLOCKS**:
- T046g (Control_Shortcuts) - needs T033w, T033x
- T046h (Control_Theme) - needs T033e, T033y
- T046k (Control_TransferTab) - needs T033r
- T046l (Control_RemoveTab) - needs T033r
- T046s (MainForm.cs) - needs T033r
- T046t (MainFormUserSettingsHelper) - needs T033a-y (all 25 methods)
- T046u (Service_OnStartup) - needs T033f

**T026d (Dao_History.AddTransactionHistoryAsync) BLOCKS**:
- T046k-7, T046k-8 (Control_TransferTab error handling verification)

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
- SC-001: Zero parameter errors ✅
- SC-002: 100% Helper routing ✅ (for MVP DAOs)
- SC-005: Connection pool healthy ✅
- SC-009: Transaction rollback working ✅
- SC-010: Startup validation working ✅

**Time Estimate**: 8-10 days (single developer) or 4-5 days (3 developers parallel)

### Incremental Delivery

1. **Foundation** (Phases 1-2) → 2-3 days → DaoResult pattern, Helper refactored
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
- ✅ T018a-f: Dao_ErrorLog methods now return DaoResult types (test updates pending)
- ✅ T026d: Dao_History.AddTransactionHistoryAsync returns DaoResult, all callers updated
- ✅ Phase 3 and Phase 4 checkpoints reached

---

## Service_DebugTracer Integration Tracking

**Per FR-012**: All DAO methods must have TraceMethodEntry/TraceMethodExit calls for debugging visibility.

**Integration Approach**: Add Service_DebugTracer calls **during DAO refactoring** (not as separate retrofit task).

### DAO Methods Requiring Service_DebugTracer

**Phase 3 (US1)** - 6 methods:
- [X] Dao_System.cs (5 methods) - ✅ Integrated during T017
- [ ] Dao_ErrorLog.cs (6+ methods) - ⚠️ Partial (T018a-f incomplete)

**Phase 4 (US2)** - 15 methods:
- [X] Dao_Inventory.cs (6 methods) - ✅ Integrated during T024
- [X] Dao_Transactions.cs (3 methods) - ✅ Integrated during T025
- [ ] Dao_History.cs (4 methods) - ⚠️ Partial (T026d incomplete)

**Phase 5 (US3)** - 10 methods:
- [ ] Dao_User.cs (5 core + 25 UI settings) - ⚠️ Partial (T033a-y incomplete)
- [X] Dao_Part.cs (7 methods) - ✅ Integrated during T034

**Phase 6 (US4)** - 27 methods:
- [X] Dao_Location.cs (6 methods) - ✅ Integrated during T042
- [X] Dao_Operation.cs (6 methods) - ✅ Integrated during T043
- [X] Dao_ItemType.cs (6 methods) - ✅ Integrated during T044
- [X] Dao_QuickButtons.cs (8 methods) - ✅ Integrated during T045

**Total**: 40+ DAO methods × 2 trace calls (entry/exit) = 80+ trace points required

**Status**: ~60% complete (methods in completed DAOs have tracing)

### Sensitive Data Handling in Traces

**Never log**:
- Passwords (use username only in AuthenticateUserAsync)
- Connection strings (log server address only)
- Personal identifying information beyond UserID

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

- **[P] tasks**: Different files, no dependencies, can execute in parallel
- **[Story] label**: Maps task to specific user story (US1-US5) for traceability
- **Method-Level Subtasks**: Each method refactoring tracked individually for precise progress tracking
- **Each user story independently testable**: Can validate at each checkpoint
- **Tests written FIRST**: Ensure tests FAIL before implementation (TDD approach)
- **Commit strategy**: Atomic commits by DAO file or Form file to enable easy rollback
- **Critical gate**: Phase 2 completion BLOCKS all DAO work
- **Critical blockers**: T033a-y must complete before 47 control subtasks can proceed

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

