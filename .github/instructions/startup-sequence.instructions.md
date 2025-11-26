# Startup Sequence Instructions

> **Description**: Documentation of the application boot sequence, initialization phases, and startup architecture.
> **Apply To**: `Program.cs`, `Services/Startup/*.cs`, `Forms/Splash/*.cs`

## Overview

The application startup process is a multi-phase sequence designed to ensure critical services (Logging, Database, User Context) are initialized before the main UI is presented. The process is orchestrated by `Program.cs` (entry point) and `Service_Onstartup_StartupSplashApplicationContext` (async initialization).

## Architecture

### 1. Entry Point (`Program.cs`)
The `Main` method is responsible for the "Pre-UI" initialization phase. It performs synchronous setup of the environment.

**Responsibilities:**
- **System Setup**: Debugging, Global Exception Handling, Cleanup Hooks.
- **WinForms Init**: HighDpiMode, VisualStyles.
- **Service Init**: Logging, Dependency Injection.
- **Launch**: Passes control to `StartupSplashApplicationContext`.

### 2. Async Initialization (`StartupSplashApplicationContext`)
This context manages the Splash Screen and executes the "Loading" phase asynchronously. It prevents the UI from freezing while performing I/O operations.

**Responsibilities:**
- **Splash Screen**: Shows progress to the user.
- **Async Tasks**: Database verification, Data loading, Theme initialization.
- **Transition**: Closes Splash and shows `MainForm` only when ready.

## Startup Phases

### Phase 1: Environment Setup (Synchronous)
*Occurs in `Program.cs`*
1.  **Debugging**: `Service_OnStartup_System.InitializeDebugging()`
2.  **Exception Handling**: `Service_OnStartup_AppLifecycle.SetupGlobalExceptionHandling()`
3.  **WinForms**: `Service_OnStartup_System.InitializeWinForms()`
4.  **Logging**: `Service_OnStartup_Reporting.InitializeLogging()`
5.  **User ID**: `Service_OnStartup_User.IdentifyUser()`
6.  **DI Container**: `Service_OnStartup_DependencyInjection.ConfigureServices()`

### Phase 2: Loading & Verification (Async)
*Occurs in `Service_Onstartup_StartupSplashApplicationContext.RunStartupAsync()`*
1.  **Cleanup**: Old logs and temp files.
2.  **Database Check**: `VerifyDatabaseConnectivityWithHelperAsync()` (Critical).
3.  **Data Setup**: `Helper_UI_ComboBoxes.SetupDataTables()`.
4.  **Cache**: `Model_Application_Variables.ReloadColorCodePartsAsync()`.
5.  **Themes**: `Core_AppThemes.InitializeThemeSystemAsync()`.
6.  **User Settings**: Load user-specific configs.
7.  **User Access**: Verify permissions (Critical).

### Phase 3: UI Presentation
1.  **MainForm Creation**: Instantiate `MainForm`.
2.  **Configuration**: Inject dependencies/instances into controls.
3.  **Transition**: Show `MainForm`, Close `SplashScreen`.

## Critical Rules

1.  **No Blocking I/O in Program.cs**: All database calls and file I/O should be moved to the Async Phase in `StartupSplashApplicationContext`.
2.  **Await Critical Tasks**: User Access and Database Connectivity must be awaited. Do not use fire-and-forget (`_ = Task`) for critical initialization.
3.  **Fail Gracefully**: If a critical step fails (e.g., DB down), show a user-friendly error dialog and exit. Do not crash.
4.  **Progress Feedback**: Update the Splash Screen for every major step to keep the user informed.

## Error Handling

*   **Global Exceptions**: Handled by `Service_OnStartup_AppLifecycle`.
*   **Startup Exceptions**: Caught in `RunStartupAsync` and displayed via `ShowErrorDialog` before safe exit.
*   **Logging**: All startup errors must be logged to `LoggingUtility`.

## Common Pitfalls

*   **Race Conditions**: Starting `MainForm` before `UserAccess` is loaded. Always await permission checks.
*   **Redundant Checks**: Checking DB connectivity in both `Program.cs` and `SplashContext`. Do it once in `SplashContext`.
*   **UI Thread Blocking**: Running heavy initialization in `MainForm` constructor. Move to `Shown` event or `SplashContext`.
