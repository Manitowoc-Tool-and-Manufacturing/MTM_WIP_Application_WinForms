# Startup Audit Baseline

**Date**: 2025-11-25
**Branch**: `002-startup-audit`

## Overview

This document captures the state of the application startup sequence prior to the refactoring in the `startup-audit` feature. It serves as a baseline for verifying feature parity after the changes.

## 1. Program.cs (Entry Point)

The `Main` method performs the following synchronous steps:

1.  **Initialize Debugging**: `Service_OnStartup_System.InitializeDebugging()`
2.  **Global Exception Handling**: `Service_OnStartup_AppLifecycle.SetupGlobalExceptionHandling()`
3.  **Cleanup Handlers**: `Service_OnStartup_AppLifecycle.SetupCleanupHandlers()`
4.  **WinForms Init**: `Service_OnStartup_System.InitializeWinForms()`
5.  **Logging Init**: `Service_OnStartup_Reporting.InitializeLogging()`
6.  **User Identification**: `Service_OnStartup_User.IdentifyUser()`
7.  **Load User Settings**: `Service_OnStartup_User.LoadUserSettings()` (Synchronous DB call)
8.  **Validate Connectivity**: `Service_OnStartup_Database.ValidateConnectivity()` (Synchronous DB call)
9.  **Initialize Parameter Cache**: `Service_OnStartup_Database.InitializeParameterCache()`
10. **Sync Error Reports**: `Service_OnStartup_Reporting.SyncErrorReports()`
11. **Load User Access**: `Service_OnStartup_User.LoadUserAccess()` (Fire-and-forget async call)
12. **Trace Setup**: `Service_OnStartup_System.SetupTrace()`
13. **DI Configuration**: `Service_OnStartup_DependencyInjection.ConfigureServices()`
14. **Run Application**: `Service_OnStartup_AppLifecycle.RunApplication()` (Launches `StartupSplashApplicationContext`)

## 2. StartupSplashApplicationContext (Async Sequence)

The `RunStartupAsync` method performs the following steps:

1.  **Logging Init**: `LoggingUtility.InitializeLoggingAsync()` (Redundant?)
2.  **Log Cleanup**: `LoggingUtility.CleanUpOldLogsIfNeededAsync()`
3.  **App Data Cleanup**: `Service_OnStartup_AppDataCleaner.WipeAppDataFolders()`
4.  **Verify DB Connectivity**: `VerifyDatabaseConnectivityWithHelperAsync()` (Redundant check)
5.  **Setup Data Tables**: `Helper_UI_ComboBoxes.SetupDataTables()`
6.  **Load Color Code Cache**: `Model_Application_Variables.ReloadColorCodePartsAsync()`
7.  **Version Checker**: `Service_Timer_VersionChecker.Initialize()`
8.  **Theme System Init**: `Core_AppThemes.InitializeThemeSystemAsync()`
9.  **Load Theme Settings**: `LoadThemeSettingsAsync()`
10. **Create Main Form**: `new MainForm()`
11. **Configure Form Instances**: `ConfigureFormInstances()`
12. **Apply Theme**: `ApplyThemeToMainForm()`
13. **Show Main Form**: `ShowMainForm()`

## 3. Critical Observations

*   **Race Condition**: `LoadUserAccess` in `Program.cs` is not awaited. `MainForm` may load before permissions are set.
*   **Blocking I/O**: `LoadUserSettings` and `ValidateConnectivity` in `Program.cs` block the UI thread before the splash screen appears.
*   **Redundancy**: Logging initialization and DB connectivity checks happen in both `Program.cs` and `StartupSplashApplicationContext`.

## 4. Feature Parity Checklist

After refactoring, the following features must still be present and functional:

- [ ] Debugging and Exception Handling setup
- [ ] WinForms visual styles
- [ ] Logging initialization
- [ ] User identification (Windows Identity)
- [ ] User Settings loading (Theme, Animations, etc.)
- [ ] Database Connectivity verification
- [ ] Parameter Cache initialization
- [ ] Error Report syncing
- [ ] User Access Permission loading
- [ ] DI Container configuration
- [ ] Log and App Data cleanup
- [ ] Data Table setup (ComboBoxes)
- [ ] Color Code Cache loading
- [ ] Version Checker initialization
- [ ] Theme System initialization and application
- [ ] Main Form creation and display
