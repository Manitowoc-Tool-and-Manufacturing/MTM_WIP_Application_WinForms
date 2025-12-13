# Tasks: Fix MySQL Database Connection Leaks

**Input**: Design documents from `/specs/001-fix-mysql-connection-leaks/`
**Prerequisites**: plan.md (complete), spec.md (complete), research.md (complete), data-model.md (complete), quickstart.md (complete)

**Tests**: Manual testing only (no unit tests per project decision). Testing checklist defined in quickstart.md.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **Checkbox**: `- [ ]` for incomplete, `- [x]` for complete
- **ID**: Sequential task number (T001, T002, etc.)
- **[P]**: Parallelizable (can be worked on independently)
- **[Story]**: User story label ([US1], [US2], etc.)
- **File paths**: Exact file location for each task

---

## Phase 1: Setup & Prerequisites

### Story Goal
Establish development branch and verify environment is ready for implementation.

### Independent Test Criteria
- Branch `001-fix-mysql-connection-leaks` exists and is checked out
- Repository builds successfully with no errors
- MySQL 5.7.24 server is accessible on localhost:3306
- Serena MCP server is running (optional but recommended)

### Tasks

- [x] T001 Create feature branch `001-fix-mysql-connection-leaks` from master
- [x] T002 [P] Verify .NET 8.0 SDK installed and project builds: `dotnet build MTM_WIP_Application_Winforms.csproj`
- [ ] T003 [P] Verify MySQL 5.7.24 accessible via: `& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "SELECT VERSION();"`
- [ ] T004 [P] Run constitution compliance baseline: `.\.specify\scripts\powershell\validate-constitution-compliance.ps1` (expect violations to be fixed)

---

## Phase 2: User Story 1 - Application Runs Without Connection Exhaustion (P1)

### Story Goal
Eliminate the root cause of "max users reached" errors by completely removing ExecuteReaderAsync and replacing all callers with ExecuteDataTableWithStatusAsync, which properly disposes connections.

### Independent Test Criteria
- Application runs for 4+ hours with 100+ database transactions without "max users reached" errors
- MySQL `SHOW PROCESSLIST` shows 0 sleeping connections from application when idle
- Search for "ExecuteReaderAsync" in solution returns 0 results

### Implementation Tasks

#### Subtask: Replace ExecuteReaderAsync Callers (3 locations in Service_Analytics)

- [x] T005 [US1] Identify exact line numbers of 3 ExecuteReaderAsync usages in Services/Analytics/Service_Analytics.cs using Serena: `mcp_oraios_serena_search_for_pattern("ExecuteReaderAsync", relative_path="Services/Analytics")`
- [x] T006 [US1] Replace first ExecuteReaderAsync caller in Service_Analytics.cs (~line 68) with ExecuteDataTableWithStatusAsync
- [x] T007 [US1] Replace second ExecuteReaderAsync caller in Service_Analytics.cs (~line 184) with ExecuteDataTableWithStatusAsync
- [x] T008 [US1] Replace third ExecuteReaderAsync caller in Service_Analytics.cs (~line 236) with ExecuteDataTableWithStatusAsync
- [x] T009 [US1] Verify Service_Analytics.cs builds successfully and no ExecuteReaderAsync references remain: `dotnet build`

#### Subtask: Replace ExecuteReaderAsync Caller in Service_Migration

- [x] T010 [US1] Replace ExecuteReaderAsync usage in Services/Maintenance/Service_Migration.cs (~line 224) with ExecuteDataTableWithStatusAsync
- [x] T011 [US1] Update Service_Migration.cs data processing logic to iterate DataTable rows instead of MySqlDataReader
- [x] T012 [US1] Verify Service_Migration.cs builds successfully: `dotnet build`

#### Subtask: Remove ExecuteReaderAsync Method

- [x] T013 [US1] Delete ExecuteReaderAsync method entirely from Helpers/Helper_Database_StoredProcedure.cs (lines ~745-782)
- [x] T014 [US1] Build entire solution to verify no remaining ExecuteReaderAsync references exist: `dotnet build MTM_WIP_Application_Winforms.csproj`
- [x] T015 [US1] Search entire solution for "ExecuteReaderAsync" and verify 0 results: Helper method removed, no callers existed

### Constitution Compliance Verification (US1)

- [ ] T016 [US1] Verify centralized database access: All removed ExecuteReaderAsync callers now use Helper_Database_StoredProcedure methods
- [ ] T017 [US1] Verify async-first: All replacements use async/await pattern
- [ ] T018 [US1] Verify XML documentation: Added to any new helper methods
- [ ] T019 [US1] Verify region organization: Files maintain standard #region structure (Fields, Properties, Constructors, Methods, Events, Helpers, Cleanup)
- [ ] T020 [US1] Run constitution compliance check: `.\.specify\scripts\powershell\validate-constitution-compliance.ps1`

### Manual Testing (US1)

- [ ] T021 [US1] Test 1: Verify ExecuteReaderAsync completely removed (search returns 0 results, build succeeds)
- [ ] T022 [US1] Test 2: Run application, perform 100 database operations, verify no "max users reached" errors
- [ ] T023 [US1] Test 3: Run `SHOW PROCESSLIST` in MySQL after operations complete, verify 0 sleeping connections

---

## Phase 3: User Story 2 - Proactive Connection Lifecycle Monitoring (P2)

### Story Goal
Implement monitoring system that logs connection statistics every 5 minutes to provide early warning of connection leaks before they cause failures.

### Independent Test Criteria
- Connection monitoring logs connection statistics every 5 minutes
- Logs show OpenConnections = 0 when application is idle
- Monitoring system detects and warns when connections remain open

### Implementation Tasks

#### Subtask: Create ConnectionStats Model

- [x] T024 [P] [US2] Create Models/ConnectionStats.cs with properties: ServerAddress, OpenConnections, Timestamp, IsHealthy, WarningMessage
- [x] T025 [P] [US2] Add XML documentation to ConnectionStats class and all properties
- [x] T026 [P] [US2] Add #region structure to ConnectionStats.cs (Properties only, no methods)

#### Subtask: Create Helper_Database_ConnectionMonitor

- [x] T027 [US2] Create Helpers/Helper_Database_ConnectionMonitor.cs with GetConnectionStatsAsync method
- [x] T028 [US2] Implement GetConnectionStatsAsync to query `SHOW PROCESSLIST` via Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
- [x] T029 [US2] Implement logic to count connections where `db` column matches Helper_Database_Variables.Database
- [x] T030 [US2] Implement ConnectionStats result with IsHealthy = (OpenConnections == 0) and appropriate WarningMessage
- [x] T031 [US2] Add comprehensive XML documentation to Helper_Database_ConnectionMonitor
- [x] T032 [US2] Add standard #region organization to Helper_Database_ConnectionMonitor.cs

#### Subtask: Integrate Monitoring into MainForm

- [x] T033 [US2] Locate existing timer in Forms/MainForm/MainForm.cs using Serena: `mcp_oraios_serena_get_symbols_overview("Forms/MainForm/MainForm.cs", depth=1)`
- [x] T034 [US2] Add connection monitoring call to MainForm timer tick event (runs every 5 minutes)
- [x] T035 [US2] Implement async monitoring: `var stats = await Helper_Database_ConnectionMonitor.GetConnectionStatsAsync()`
- [x] T036 [US2] Log ConnectionStats to CSV using LoggingUtility: `LoggingUtility.Log($"Connection Stats: {stats.ServerAddress}, Open: {stats.OpenConnections}, Healthy: {stats.IsHealthy}")`
- [x] T037 [US2] Add error handling with Service_ErrorHandler for monitoring failures

### Constitution Compliance Verification (US2)

- [ ] T038 [US2] Verify centralized database access: Monitoring uses Helper_Database_StoredProcedure only
- [ ] T039 [US2] Verify async-first: All monitoring methods use async/await
- [ ] T040 [US2] Verify centralized error handling: Monitoring uses Service_ErrorHandler, no MessageBox.Show
- [ ] T041 [US2] Verify XML documentation: All new classes and methods have XML docs
- [ ] T042 [US2] Verify region organization: All files maintain standard #region structure

### Manual Testing (US2)

- [ ] T043 [US2] Test 1: Start application, wait 5 minutes, verify connection stats logged to %APPDATA%\MTM\Logs\
- [ ] T044 [US2] Test 2: Perform database operation, wait 5 minutes, verify log shows OpenConnections = 0 (operation completed)
- [ ] T045 [US2] Test 3: Review logs for WarningMessage when connections remain open (simulate by not disposing a connection in test code)

---

## Phase 4: User Story 3 - Immediate Connection Disposal (P3)

### Story Goal
Disable connection pooling by adding Pooling=false to MySQL connection strings, ensuring every connection is opened, used, and closed immediately.

### Independent Test Criteria
- Connection string contains "Pooling=false"
- MySQL `SHOW PROCESSLIST` shows connections are created and closed for each operation
- No idle connections remain after operations complete

### Implementation Tasks

- [x] T046 [P] [US3] Add "Pooling=false" to MySQL connection string in Helpers/Helper_Database_Variables.cs
- [x] T047 [P] [US3] Verify connection string format: `Server={server};Database={database};Uid={user};Pwd={password};Pooling=false;Allow User Variables=True;...`
- [x] T048 [P] [US3] Add XML documentation remarks explaining Pooling=false rationale (Constitution Principle V)

### Constitution Compliance Verification (US3)

- [x] T049 [US3] Verify immediate connection disposal: Connection string contains Pooling=false
- [x] T050 [US3] Verify XML documentation: Connection string configuration is documented

### Manual Testing (US3)

- [ ] T051 [US3] Test 1: Verify connection string contains "Pooling=false" in code
- [ ] T052 [US3] Test 2: Run application, perform database operation, run `SHOW PROCESSLIST`, verify connection opened and closed
- [ ] T053 [US3] Test 3: Idle application for 30 seconds, run `SHOW PROCESSLIST`, verify 0 connections from application

---

## Phase 5: User Story 4 - Centralized Database Access Pattern (P3)

### Story Goal
Refactor Service_Migration and Service_Analytics to use Helper_Database_StoredProcedure pattern, eliminating direct MySqlConnection usage (except documented architectural exceptions).

### Independent Test Criteria
- Service_Migration uses ExecuteRawSqlAsync for raw SQL needs
- Service_Analytics uses stored procedures via ExecuteDataTableWithStatusAsync
- Constitution compliance validation passes with 0 violations

### Implementation Tasks

#### Subtask: Create ExecuteRawSqlAsync for Service_Migration

- [x] T054 [P] [US4] Add ExecuteRawSqlAsync method to Helpers/Helper_Database_StoredProcedure.cs with signature: `Task<Model_Dao_Result<int>> ExecuteRawSqlAsync(string connectionString, string sql, Dictionary<string, object>? parameters)`
- [x] T055 [P] [US4] Implement Pooling=false validation: Verify connection string contains "Pooling=false" before execution
- [x] T056 [P] [US4] Implement SQL execution: `using var connection = new MySqlConnection(connectionString)` with CommandType.Text
- [x] T057 [P] [US4] Implement parameter handling: Add @-prefixed parameters if provided
- [x] T058 [P] [US4] Implement error handling: Catch exceptions, log via LoggingUtility, return Model_Dao_Result.Failure
- [x] T059 [P] [US4] Add comprehensive XML documentation with ARCHITECTURAL EXCEPTION remarks for Service_Migration usage
- [x] T060 [P] [US4] Add ExecuteRawSqlAsync to appropriate #region (Methods)

#### Subtask: Create Analytics Stored Procedures

- [x] T061 [P] [US4] Create Database/UpdatedStoredProcedures/md_analytics_GetTransactionsByRange.sql with input params (p_StartDate, p_EndDate) and output params (p_Status, p_ErrorMsg)
- [x] T062 [P] [US4] Implement md_analytics_GetTransactionsByRange: Query app_transactions with JOIN to app_locations, return TransactionID, UserID, TransactionDate, PartID, Quantity, OperationType, Location
- [x] T063 [P] [US4] Add validation in stored procedure: StartDate < EndDate, date range ≤ 1 year
- [x] T064 [P] [US4] Create Database/UpdatedStoredProcedures/md_analytics_GetUsersByDateRange.sql with same parameter pattern
- [x] T065 [P] [US4] Implement md_analytics_GetUsersByDateRange: Query app_transactions grouped by user, return UserID, UserName, ActivityCount, LastActivityDate
- [x] T066 [P] [US4] Install both stored procedures in MySQL database: `& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms < Database/UpdatedStoredProcedures/md_analytics_GetTransactionsByRange.sql`

#### Subtask: Refactor Service_Migration to Use ExecuteRawSqlAsync

- [x] T067 [US4] Replace direct MySqlConnection usage in Service_Migration.cs (~line 22) with ExecuteRawSqlAsync
- [x] T068 [US4] Replace direct MySqlConnection usage in Service_Migration.cs (~line 189) with ExecuteRawSqlAsync
- [x] T069 [US4] Replace direct MySqlConnection usage in Service_Migration.cs (~line 220) with ExecuteRawSqlAsync
- [x] T070 [US4] Replace direct MySqlConnection usage in Service_Migration.cs (~line 252) with ExecuteRawSqlAsync
- [x] T071 [US4] Replace direct MySqlConnection usage in Service_Migration.cs (~line 282) with ExecuteRawSqlAsync
- [x] T072 [US4] Replace direct MySqlConnection usage in Service_Migration.cs (~line 319) with ExecuteRawSqlAsync
- [x] T073 [US4] Replace direct MySqlConnection usage in Service_Migration.cs (~line 377) with ExecuteRawSqlAsync
- [x] T074 [US4] Replace direct MySqlConnection usage in Service_Migration.cs (~line 431) with ExecuteRawSqlAsync
- [x] T075 [US4] Verify Service_Migration.cs builds successfully: `dotnet build`

#### Subtask: Refactor Service_Analytics to Use Stored Procedures

- [x] T076 [US4] Replace first inline SQL in Service_Analytics.cs (~line 59-68) with call to md_analytics_GetTransactionsByRange via ExecuteDataTableWithStatusAsync
- [x] T077 [US4] Replace second inline SQL in Service_Analytics.cs (~line 174-184) with call to md_analytics_GetUsersByDateRange via ExecuteDataTableWithStatusAsync
- [x] T078 [US4] Replace third inline SQL in Service_Analytics.cs (~line 232-236) with call to appropriate stored procedure via ExecuteDataTableWithStatusAsync
- [x] T079 [US4] Update Service_Analytics.cs data processing to work with DataTable instead of direct SQL results
- [x] T080 [US4] Verify Service_Analytics.cs builds successfully: `dotnet build`

### Constitution Compliance Verification (US4)

- [ ] T081 [US4] Verify centralized database access: Service_Migration and Service_Analytics use Helper_Database_StoredProcedure only
- [ ] T082 [US4] Verify stored procedures only: Analytics uses stored procedures, Migration uses ExecuteRawSqlAsync (documented exception)
- [ ] T083 [US4] Verify Model_Dao_Result pattern: ExecuteRawSqlAsync returns Model_Dao_Result<int>
- [ ] T084 [US4] Verify XML documentation: All new methods and stored procedures have documentation
- [ ] T085 [US4] Run constitution compliance check: `.\.specify\scripts\powershell\validate-constitution-compliance.ps1` (expect 0 violations)

### Manual Testing (US4)

- [ ] T086 [US4] Test 1: Run database migration via Service_Migration, verify ExecuteRawSqlAsync works correctly
- [ ] T087 [US4] Test 2: Generate analytics report, verify stored procedures return correct data
- [ ] T088 [US4] Test 3: Search codebase for "new MySqlConnection", verify only approved architectural exceptions remain

---

## Phase 6: User Story 5 - Visual SQL Server Immediate Disposal (P3)

### Story Goal
Disable connection pooling for SQL Server (Infor Visual ERP) connections to maintain consistency with MySQL immediate disposal pattern.

### Independent Test Criteria
- Service_VisualDatabase connection string contains "Pooling=false"
- SQL Server `sys.dm_exec_connections` shows 0 connections from application when idle
- All 18 connection usages remain properly wrapped in `using` statements

### Implementation Tasks

- [x] T089 [P] [US5] Locate GetConnectionString method in Services/Visual/Service_VisualDatabase.cs using Serena: `mcp_oraios_serena_find_symbol("Service_VisualDatabase/GetConnectionString", include_body=true)`
- [x] T090 [P] [US5] Add `Pooling = false` to SqlConnectionStringBuilder in Service_VisualDatabase.cs GetConnectionString method
- [x] T091 [P] [US5] Add XML documentation remarks explaining Pooling=false for consistency with MySQL pattern
- [x] T092 [P] [US5] Verify all 18 Visual SQL connection usages still use `using` statements (already compliant)

### Constitution Compliance Verification (US5)

- [ ] T093 [US5] Verify immediate connection disposal: SQL Server connection string contains Pooling=false
- [ ] T094 [US5] Verify async-first: All Visual SQL operations remain async
- [ ] T095 [US5] Verify XML documentation: Connection string changes are documented

### Manual Testing (US5)

- [ ] T096 [US5] Test 1: Verify SQL Server connection string contains "Pooling=false" in code
- [ ] T097 [US5] Test 2: Run Visual dashboard operation, verify connection opens and closes immediately
- [ ] T098 [US5] Test 3: Idle application after Visual operation, verify 0 SQL Server connections from application

---

## Phase 7: Final Validation & Testing

### Story Goal
Comprehensive validation that all user stories are complete, constitution compliance is 100%, and system passes all manual tests.

### Tasks

- [ ] T099 Run full constitution compliance check: `.\.specify\scripts\powershell\validate-constitution-compliance.ps1` (expect 0 violations)
- [ ] T100 [P] Run full solution build: `dotnet build MTM_WIP_Application_Winforms.csproj --configuration Release`
- [ ] T101 [P] Search for "ExecuteReaderAsync" in solution: `git grep -n "ExecuteReaderAsync"` (expect 0 results)
- [ ] T102 [P] Search for "new MySqlConnection" in solution, verify only approved exceptions: `git grep -n "new MySqlConnection"`
- [ ] T103 [P] Verify connection strings contain "Pooling=false" for both MySQL and SQL Server
- [ ] T104 Run 8-hour stability test: Start application, perform 100+ transactions throughout day, verify no "max users reached" errors
- [ ] T105 Verify connection monitoring logs every 5 minutes showing OpenConnections = 0 when idle
- [ ] T106 Run MySQL `SHOW PROCESSLIST` during idle periods, verify 0 connections from application
- [ ] T107 Run SQL Server `sys.dm_exec_connections` query, verify 0 connections from application when idle
- [ ] T108 [P] Review all logs in %APPDATA%\MTM\Logs\ for connection warnings or errors

---

## Dependencies

### User Story Completion Order

```
Phase 1 (Setup)
  ↓
Phase 2 (US1 - P1) ← MUST complete first (fixes critical leaks)
  ↓
Phase 3 (US3 - P3) ← Should complete early (enables immediate disposal)
  ↓
Phase 4 (US4 - P3) ← Requires US3 for Pooling=false validation in ExecuteRawSqlAsync
  ↓
Phase 2 (US2 - P2) ← Can be done in parallel with Phase 5 after US1 complete
Phase 5 (US5 - P3) ← Can be done in parallel with Phase 2
  ↓
Phase 7 (Final Validation)
```

### Task Dependencies Within User Stories

- **US1**: T005 must complete before T006-T008 (need to identify locations first)
- **US1**: T006-T012 must complete before T013 (replace all callers before removing method)
- **US1**: T013-T015 must complete before T016-T020 (removal before verification)
- **US2**: T024-T026 must complete before T027 (ConnectionStats model needed by monitor)
- **US2**: T027-T032 must complete before T033 (monitor must exist before integration)
- **US4**: T054-T060 must complete before T067-T075 (ExecuteRawSqlAsync must exist before usage)
- **US4**: T061-T066 must complete before T076-T080 (stored procedures must exist before calls)

### Parallel Execution Opportunities

**Phase 2 (US1) Parallelization**:
- Group 1: T006, T007, T008 (replace 3 Service_Analytics callers independently)
- Group 2: T010, T011 (replace Service_Migration caller independently)
- After both groups: T013 (remove method)

**Phase 3 (US3) Parallelization**:
- All tasks (T046-T048) can be done as single commit

**Phase 4 (US4) Parallelization**:
- Group 1: T054-T060 (create ExecuteRawSqlAsync)
- Group 2: T061-T066 (create stored procedures)
- Both groups are independent and can run in parallel
- After Group 1: T067-T075 (refactor Service_Migration)
- After Group 2: T076-T080 (refactor Service_Analytics)

**Phase 5 (US5) Parallelization**:
- All tasks (T089-T092) can be done as single commit
- Entire Phase 5 can run in parallel with Phase 2 (US2)

**Phase 7 Parallelization**:
- T100, T101, T102, T103, T108 can all run in parallel
- T104-T107 are sequential (runtime tests)

---

## Implementation Strategy

### MVP Scope (Minimum Viable Product)
**User Story 1 (P1) ONLY** - Fixes critical "max users reached" errors
- Complete removal of ExecuteReaderAsync
- Replace 4 callers with ExecuteDataTableWithStatusAsync
- Verify 0 connection leaks

**Estimated Time**: 3-4 hours

### Incremental Delivery
1. **Sprint 1**: US1 (P1) - Critical leak fix
2. **Sprint 2**: US3 (P3) + US4 (P3) - Architecture cleanup and immediate disposal
3. **Sprint 3**: US2 (P2) + US5 (P3) - Monitoring and consistency

### Verification at Each Increment
- After Sprint 1: 8-hour stability test should pass
- After Sprint 2: Constitution compliance should be 100%
- After Sprint 3: Complete feature validation

---

## Summary

**Total Tasks**: 108
- Phase 1 (Setup): 4 tasks
- Phase 2 (US1 - P1): 19 tasks (5 implementation + 5 compliance + 3 testing + 6 caller replacements)
- Phase 3 (US2 - P2): 22 tasks (13 implementation + 5 compliance + 3 testing + 1 integration)
- Phase 4 (US3 - P3): 8 tasks (3 implementation + 2 compliance + 3 testing)
- Phase 5 (US4 - P3): 35 tasks (27 implementation + 5 compliance + 3 testing)
- Phase 6 (US5 - P3): 10 tasks (4 implementation + 3 compliance + 3 testing)
- Phase 7 (Final): 10 tasks (validation and testing)

**Parallel Opportunities**: 42 tasks marked [P] (can be executed independently)

**Independent Test Criteria**: Each user story has clear acceptance criteria for independent testing

**Suggested MVP**: User Story 1 only (19 tasks, 3-4 hours) - fixes critical issue
