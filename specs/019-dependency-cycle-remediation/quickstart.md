# Quickstart: Dependency Cycle Remediation

## Running the Refactored Startup

The startup sequence is now orchestrated by `Service_OnStartup_AppLifecycle`.

1.  **Entry Point**: The application entry point (`Program.cs`) calls `Service_OnStartup_AppLifecycle.RunApplication()`.
2.  **Orchestration**: This method sequentially calls:
    -   `Service_OnStartup_AppDataCleaner.WipeAppDataFolders()`
    -   `Service_OnStartup_Database.ValidateConnectivity()`
    -   `Service_OnStartup_User.ValidateUser()`
3.  **Error Handling**: If any step fails, the Orchestrator handles the error (logs it, shows a dialog if needed) and decides whether to proceed or exit.

## Logging

Logging is now decoupled.
-   **Configuration**: Log paths are determined by `Helper_LogPath`.
-   **Usage**: `LoggingUtility.Log()` works as before, but without circular dependencies.

## Verification

To verify the fix:
1.  Run the dependency analysis tool (if available).
2.  Launch the application.
3.  Check `AppData/MTM/Logs` to ensure logs are being written.
