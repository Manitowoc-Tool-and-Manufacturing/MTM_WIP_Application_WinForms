# Tasks: Startup Process Analysis and Hardening

**Feature**: Startup Process Analysis and Hardening
**Branch**: `002-startup-audit`
**Spec**: [specs/002-startup-audit/spec.md](specs/002-startup-audit/spec.md)

## Phase 1: Setup & Documentation

*Goal: Prepare the environment and document the current state for parity checks.*

- [x] T001 Document current startup sequence in `specs/002-startup-audit/startup-audit-baseline.md` (ignoring new features)
- [x] T002 [P] Create `Forms/Shared/Form_SharedLogin.cs` (skeleton) inheriting from `ThemedForm`
- [x] T003 [P] Create `Forms/Shared/Form_SharedLogin.Designer.cs` (skeleton)
- [x] T004 [P] Register `Form_SharedLogin` in `Program.cs` DI container (if applicable) or prepare for manual instantiation

## Phase 2: Foundational Refactoring

*Goal: Convert synchronous startup services to async and prepare the orchestrator.*

- [x] T005 Refactor `Service_OnStartup_User.LoadUserSettings` to `LoadUserSettingsAsync` (return `Task`) in `Services/Startup/Service_OnStartup_User.cs`
- [x] T006 Refactor `Service_OnStartup_User.LoadUserAccess` to `LoadUserAccessAsync` (return `Task`) in `Services/Startup/Service_OnStartup_User.cs`
- [x] T007 Ensure all DAO calls within `Service_OnStartup_User` are awaited and use `Model_Dao_Result`
- [x] T008 Update `Service_OnStartup_Database.ValidateConnectivity` to be async-friendly (if needed) or confirm `VerifyDatabaseConnectivityWithHelperAsync` usage

## Phase 3: User Story 1 - Refactor Program.cs & Splash Context

*Goal: Eliminate race conditions and blocking I/O by moving logic to the Splash Context.*

- [x] T009 [US1] Remove blocking `ValidateConnectivity` call from `Program.cs`
- [x] T010 [US1] Remove blocking `LoadUserSettings` call from `Program.cs`
- [x] T011 [US1] Remove fire-and-forget `LoadUserAccess` call from `Program.cs`
- [x] T012 [US1] Update `Services/Startup/Service_OnStartup_StartupSplashApplicationContext.cs` to await `LoadUserSettingsAsync` in `RunStartupAsync`
- [x] T013 [US1] Update `Services/Startup/Service_OnStartup_StartupSplashApplicationContext.cs` to await `LoadUserAccessAsync` in `RunStartupAsync`
- [x] T014 [US1] Implement 30-second timeout for DB checks in `RunStartupAsync`
- [x] T015 [US1] Implement "ReadOnly Mode" fallback for technical failures in `RunStartupAsync`
- [x] T016 [US1] Implement "User Not Found" check and exit logic in `RunStartupAsync`

## Phase 4: User Story 2 - Shared Workstation Login

*Goal: Implement secure login flow for shared workstations.*

- [x] T017 [US2] Implement UI for `Forms/Shared/Form_SharedLogin.cs` (Username, PIN, Login Button, Cancel Button)
- [x] T018 [US2] Implement validation logic using `Service_Validation` in `Forms/Shared/Form_SharedLogin.cs`
- [x] T019 [US2] Implement credential verification against `usr_users` in `Forms/Shared/Form_SharedLogin.cs`
- [x] T020 [US2] Implement 3-attempt lockout logic in `Forms/Shared/Form_SharedLogin.cs`
- [x] T021 [US2] Integrate `Form_SharedLogin` into `Services/Startup/Service_OnStartup_StartupSplashApplicationContext.cs` (check for SHOP2/MTMDC)
- [x] T022 [US2] Update `Model_Application_Variables.User` upon successful shared login

## Phase 5: Polish & Audit

*Goal: Verify feature parity and ensure robustness.*

- [x] T023 Verify all original startup steps (version, theme, cache) are present in `RunStartupAsync`
- [x] T024 Compare new implementation against `specs/002-startup-audit/startup-audit-baseline.md`
- [x] T025 Verify "User Not Found" dialog appears and exits app
- [x] T026 Verify "Shared Workstation" login appears for SHOP2/MTMDC
- [x] T027 Verify "ReadOnly Mode" activates on DB timeout/failure

## Dependencies

- Phase 2 (Async Refactoring) MUST complete before Phase 3 (Program.cs Refactor)
- Phase 3 (Splash Context) MUST complete before Phase 4 (Shared Login Integration)
- Phase 1 (Baseline Doc) MUST complete before Phase 5 (Audit)

## Implementation Strategy

1.  **Baseline**: Document current state to ensure no regressions.
2.  **Async Core**: Convert services to async first.
3.  **Orchestrator**: Move logic to Splash Context and clean up Program.cs.
4.  **New Features**: Add Shared Login and strict error handling.
5.  **Verify**: Audit against baseline.
