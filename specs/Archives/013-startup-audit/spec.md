# Specification: Startup Process Analysis and Hardening

## 1. Overview
**Feature**: Startup Process Analysis and Hardening
**Short Name**: `startup-audit`
**Branch**: `002-startup-audit`

This specification outlines the improvements to the application startup process. The goal is to eliminate race conditions, remove redundant database checks, improve user feedback during loading, and ensure a robust, crash-resistant boot sequence.

## 2. Problem Statement
The current startup process has several issues:
1.  **Race Condition**: User access permissions (`Dao_System.System_UserAccessTypeAsync`) are loaded in a fire-and-forget manner in `Program.cs`. The `MainForm` often loads before permissions are set, potentially showing incorrect UI states or "Unknown" privileges.
2.  **Redundancy**: Database connectivity is checked synchronously in `Program.cs` (blocking) and then again asynchronously in `StartupSplashApplicationContext`.
3.  **Blocking I/O**: `Service_OnStartup_User.LoadUserSettings` performs synchronous database calls on the main thread before the splash screen appears.
4.  **Documentation**: Lack of clear documentation for the startup sequence (addressed by new instruction file).

## 3. User Scenarios
*   **Scenario 1: Successful Launch**
    *   User launches app.
    *   Splash screen appears immediately.
    *   Progress bar updates as DB, User Settings, and Permissions are loaded.
    *   Main Form appears with correct Title (User Name | Role).
*   **Scenario 2: Database Down**
    *   User launches app.
    *   Splash screen appears.
    *   "Verifying database connectivity..." step fails.
    *   User sees a friendly error dialog explaining the connection issue.
    *   App closes gracefully when dialog is dismissed.
*   **Scenario 3: Slow Network**
    *   User launches app.
    *   Splash screen waits for "Loading User Permissions..." step.
    *   Main Form does *not* appear until permissions are loaded.
*   **Scenario 4: User Not Found**
    *   User launches app.
    *   Splash screen shows "Verifying Permissions...".
    *   System detects user is not in the database.
    *   User sees error: "You do not currently have a username set. Please contact your supervisor."
    *   App closes when dialog is dismissed.
*   **Scenario 5: Shared Workstation Login (SHOP2/MTMDC)**
    *   User launches app on a shared machine (Windows user is "SHOP2" or "MTMDC").
    *   Splash screen pauses.
    *   User is prompted to enter their personal Username and PIN.
    *   **Success**: User enters valid credentials. App proceeds with the entered username.
    *   **Failure**: User fails 3 attempts. App closes.

## 4. Functional Requirements

### 4.1. Refactor Program.cs
*   **Remove Blocking Calls**: Remove `Service_OnStartup_Database.ValidateConnectivity()` and `Service_OnStartup_User.LoadUserSettings()` from `Program.Main`.
*   **Remove Fire-and-Forget**: Remove `Service_OnStartup_User.LoadUserAccess()` from `Program.Main`.
*   **Focus**: `Program.Main` should only handle Environment Setup (Logging, DI, WinForms) and then launch the Splash Context.

### 4.2. Update StartupSplashApplicationContext
*   **Integrate User Loading**: Add steps to `RunStartupAsync` to:
    1.  Await `Service_OnStartup_User.LoadUserSettings()`.
    2.  Await `Dao_System.System_UserAccessTypeAsync()`.
*   **Single DB Check**: Rely on the existing `VerifyDatabaseConnectivityWithHelperAsync` as the single source of truth for connectivity.
*   **Progress Updates**: Add specific progress steps for "Loading User Settings" and "Verifying Permissions".

### 4.3. Service Updates
*   **Async Refactoring**: Ensure `Service_OnStartup_User` methods are async-friendly (return `Task`).
*   **Error Handling**: Ensure all new async steps have try-catch blocks that log to `LoggingUtility` and show errors via `Service_ErrorHandler`.

### 4.4. Shared Workstation Logic
*   **Detection**: Check if the detected Windows username is "SHOP2" or "MTMDC" (case-insensitive).
*   **Prompt**: If detected, show a modal dialog requesting **Username** and **PIN**.
*   **Validation**: Validate credentials against the `usr_users` table.
*   **Context Switch**: On success, update `Model_Application_Variables.User` to the validated username.
*   **Security**:
    *   Mask the PIN input.
    *   Allow maximum **3 attempts**.
    *   If 3 attempts fail, show an error and **exit the application**.

### 4.5. Feature Parity & Implementation Audit
*   **Implementation Order**: Ensure all new features (Shared Workstation Logic, User Not Found handling, Async Refactoring) are fully implemented **before** conducting the final audit.
*   **Documentation**: Document the current startup process in detail before applying changes (ignoring the new features to be added).
*   **Comparison**: After refactoring, compare the new implementation against the pre-refactor documentation to ensure no critical startup features were inadvertently removed.
*   **Verification**:
    *   Verify all original startup steps (version checks, theme loading, parameter cache) are present.
    *   Verify new **Shared Workstation Logic** functions correctly.
    *   Verify **User Not Found** handling functions correctly.

## 5. Technical Considerations
*   **Thread Safety**: Ensure UI updates (Splash Screen) happen on the UI thread.
*   **Timeout Handling**: DB calls should have a **30-second timeout** to prevent the Splash Screen from hanging indefinitely.
*   **Fallback Strategy**:
    *   **Technical Failure**: If User Access check fails due to DB error or timeout, the application must load in **ReadOnly Mode** (disable editing features).
    *   **User Not Found**: If the user's name is not found in the database, the application must show a dialog: "You do not currently have a username set. Please contact your supervisor." and then **exit the application**. Do not allow the user to proceed.

## 6. Success Criteria
*   Application launches without crashing.
*   `MainForm` title always displays the correct User Role (never "Unknown" due to race condition).
*   No blocking database calls occur before the Splash Screen is shown.
*   Database connectivity is verified exactly once.
*   Startup sequence is documented in `.github/instructions/startup-sequence.instructions.md`.
*   **Regression Testing**: Verified that no critical startup features were removed by comparing the pre-refactor documentation with the post-refactor implementation.

## 7. Assumptions
*   The `Dao_System` and `Dao_User` methods are functioning correctly.
*   The Splash Screen supports text updates (verified).
