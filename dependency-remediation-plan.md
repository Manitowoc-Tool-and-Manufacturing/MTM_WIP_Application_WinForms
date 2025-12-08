# Dependency Cycle Remediation Plan

## Executive Summary
This document outlines a strategy to resolve circular dependencies identified in the codebase. The cycles primarily cluster around two areas: the Application Startup sequence and the Logging infrastructure. The general remediation strategy involves applying the Dependency Inversion Principle, extracting shared configuration, and moving towards a strict orchestration pattern for startup.

## 1. Startup Sequence Cycles (Cycles 1-7, 12, 13)
**Components Involved:**
- `Service_OnStartup_AppLifecycle`
- `Service_Onstartup_StartupSplashApplicationContext`
- `Service_OnStartup_User`
- `Service_OnStartup_Database`

**Analysis:**
The startup components are tightly coupled, likely calling each other directly to trigger subsequent steps or update status. For example, the Lifecycle service triggers the Splash Context, which might trigger User validation, which triggers Database checks, which reports back to Lifecycle.
Additionally, `Service_OnStartup_Database` calls `Service_OnStartup_AppLifecycle.ShowNonCriticalError`, creating a direct back-reference.

**Remediation Strategy: The Orchestrator Pattern**
1.  **Designate an Orchestrator:** Transform `Service_OnStartup_AppLifecycle` into a pure orchestrator.
2.  **Linearize Execution:** The orchestrator should call individual services (`Database`, `User`, `AppDataCleaner`) sequentially or asynchronously, rather than services calling each other. **Crucially, the execution order must be preserved** (e.g., Database check -> User validation) to ensure dependencies are met.
3.  **Event/Status Decoupling:**
    -   Instead of services calling `AppLifecycle` or `SplashContext` to update progress, they should return status objects (e.g., `Model_StartupResult`) or raise events.
    -   The Orchestrator listens to these events or checks the return values and updates the `SplashContext` directly.
4.  **Remove Back-References:**
    -   Ensure `Service_OnStartup_Database` and `Service_OnStartup_User` have no reference to `Service_OnStartup_AppLifecycle` or `Service_Onstartup_StartupSplashApplicationContext`.
    -   Replace calls like `Service_OnStartup_AppLifecycle.ShowNonCriticalError` in `Service_OnStartup_Database` with direct calls to `Service_ErrorHandler` or by returning the error in the result object for the Orchestrator to handle.

## 2. Logging & Cleaner Cycles (Cycles 8, 11)
**Components Involved:**
- `LoggingUtility`
- `Service_OnStartup_AppDataCleaner`

**Analysis:**
`LoggingUtility` calls `Service_OnStartup_AppDataCleaner.DeleteDirectoryContents` inside `CleanUpOldLogsAsync`. This creates a cycle and also risks `LoggingUtility` performing broader application data cleanup than it should (e.g., wiping `AppData` folders).

**Remediation Strategy: Separation of Concerns**
1.  **Extract Configuration:** Move log file path definitions and retention policies into a shared, dependency-free configuration model or helper (e.g., `Model_LogConfiguration` or `Helper_LogPath`).
2.  **Decouple Cleanup:**
    -   **Restrict LoggingUtility:** `LoggingUtility` should *only* be responsible for cleaning up its own log files (e.g., `.log`, `.csv` in the log directory). It should **NOT** call `Service_OnStartup_AppDataCleaner` to wipe entire application data folders.
    -   **Move App Data Cleanup:** The call to `Service_OnStartup_AppDataCleaner.DeleteDirectoryContents` for `AppData` and `LocalAppData` should be removed from `LoggingUtility`. This cleanup task should be invoked explicitly by the **Startup Orchestrator** (see Section 1) if needed, completely independent of the logging system.
3.  **Unidirectional Dependency:** `Service_OnStartup_AppDataCleaner` can depend on `LoggingUtility` to log its actions, but `LoggingUtility` must not know about the cleaner.

## 3. Logging & Database Variables Cycles (Cycles 9, 10)
**Components Involved:**
- `LoggingUtility`
- `Helper_Database_Variables`

**Analysis:**
`Helper_Database_Variables` uses `LoggingUtility` to report configuration errors. Conversely, `LoggingUtility` depends on `Helper_Database_Variables` (specifically `GetLogFilePathAsync`) to determine where to write logs.

**Remediation Strategy: Configuration Extraction**
1.  **Abstract Settings:** Create a `Core_Configuration` or `Model_AppSettings` component that holds primitive values (Debug Level, Connection Strings). This component should have zero dependencies.
2.  **Extract Log Path Logic:** Move `GetLogFilePathAsync` from `Helper_Database_Variables` to a new `Helper_LogPath` or `Core_LogConfiguration`. This new helper must **NOT** depend on `LoggingUtility`. If it encounters an error (e.g., directory creation fails), it should throw an exception rather than trying to log it via `LoggingUtility`.
3.  **Initialize Early:** Load these settings at the very beginning of the application lifecycle.
4.  **Break the Link:**
    -   Modify `LoggingUtility` to use the new `Helper_LogPath` or configuration model.
    -   Ensure `Helper_Database_Variables` does not depend on `LoggingUtility` for critical path logic. If `GetConnectionString` fails, it should throw or return a result, allowing the caller to handle/log the error.

## Implementation Roadmap
1.  **Phase 1: Shared Configuration.** Extract paths, settings, and constants into dependency-free Models/Helpers.
2.  **Phase 2: Fix Logging.** Refactor `LoggingUtility` to rely only on the extracted configuration. Remove `AppDataCleaner` and `Database_Variables` dependencies from it.
3.  **Phase 3: Refactor Startup.** Rewrite `Service_OnStartup_AppLifecycle` to orchestrate the startup process linearly, removing cross-dependencies between the specific startup services.
