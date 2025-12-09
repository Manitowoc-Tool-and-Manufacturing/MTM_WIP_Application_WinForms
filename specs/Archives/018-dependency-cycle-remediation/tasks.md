# Tasks: Dependency Cycle Remediation

**Feature**: Dependency Cycle Remediation
**Status**: Draft

## Phase 1: Setup & Shared Infrastructure
**Goal**: Establish the foundational models and helpers required to break dependency cycles.
**Independent Test**: Can instantiate `Model_StartupResult` and call `Helper_LogPath` methods without errors.

- [ ] T001 [P] Create `Model_StartupResult` in `Models/Model_StartupResult.cs`
- [ ] T002 [P] Create `Helper_LogPath` in `Helpers/Helper_LogPath.cs` with `GetLogFilePathAsync` logic extracted from `Helper_Database_Variables`
- [ ] T003 Update `Helper_Database_Variables.cs` to remove `GetLogFilePathAsync` and any logging dependencies (throw exceptions instead)

## Phase 2: Foundational - Logging Decoupling
**Goal**: Ensure the logging system functions without circular dependencies on other services.
**Independent Test**: `LoggingUtility` can write logs without triggering `Service_OnStartup_AppDataCleaner` or `Helper_Database_Variables` errors.

- [ ] T004 Update `Service_LoggingUtility.cs` to use `Helper_LogPath` for file paths
- [ ] T005 Remove `Service_OnStartup_AppDataCleaner` dependency from `Service_LoggingUtility.cs` (remove `CleanUpOldLogsAsync` call to `DeleteDirectoryContents` for app data)
- [ ] T006 Verify `Service_LoggingUtility.cs` has no remaining circular dependencies

## Phase 3: User Story 1 - Application Startup (P1)
**Goal**: Implement the Orchestrator pattern for application startup to linearize execution and remove cycles.
**Independent Test**: Application starts up successfully; `Service_OnStartup_AppLifecycle` orchestrates the flow without services calling back into it.

- [ ] T007 [P] [US1] Refactor `Service_OnStartup_Database.cs` to return `Model_StartupResult` instead of calling `AppLifecycle`
- [ ] T008 [P] [US1] Refactor `Service_OnStartup_User.cs` to return `Model_StartupResult` instead of calling `AppLifecycle`
- [ ] T009 [US1] Refactor `Service_OnStartup_AppLifecycle.cs` to implement the Orchestrator pattern (call Cleaner, Database, User sequentially)
- [ ] T010 [US1] Update `Service_OnStartup_AppLifecycle.cs` to handle `Model_StartupResult` failures (log and show errors)
- [ ] T011 [US1] Ensure `Service_OnStartup_AppDataCleaner.cs` is called explicitly by the Orchestrator, not by Logging

## Phase 4: User Story 2 - Logging System (P1)
**Goal**: Verify and refine logging system behavior after decoupling.
**Independent Test**: Logs are written correctly during startup and runtime; no regressions in logging functionality.

- [ ] T012 [US2] Verify `LoggingUtility` correctly handles log rotation/cleanup for its own files (if applicable/retained)
- [ ] T013 [US2] Add integration test/verification step to ensure logs are written to the path defined in `Helper_LogPath`

## Final Phase: Polish & Verification
**Goal**: Final code cleanup and verification of cycle removal.

- [ ] T014 Run dependency analysis tool to confirm 0 cycles in Startup/Logging namespaces
- [ ] T015 Perform full application startup test (Happy Path)
- [ ] T016 Perform startup failure test (Database Down) to verify error handling via Orchestrator

## Dependencies

- T001, T002, T003 (Setup) must be done before T004 (Logging)
- T004, T005 (Logging) must be done before T009 (Orchestrator) to ensure logging works during startup
- T007, T008 (Service Refactoring) can be done in parallel with T004/T005 but must be done before T009

## Implementation Strategy

1.  **Foundation**: Extract `Helper_LogPath` and `Model_StartupResult` first. This is low-risk and enables the rest.
2.  **Logging**: Fix `LoggingUtility` next. This is critical because startup relies on logging.
3.  **Services**: Refactor `Database` and `User` services to return results. This prepares them for the Orchestrator.
4.  **Orchestrator**: Finally, rewrite `AppLifecycle` to wire everything together linearly.
