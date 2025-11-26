# Quickstart: Startup Process Analysis and Hardening

**Branch**: `002-startup-audit` | **Date**: 2025-11-25

## 1. Overview
This feature hardens the application startup by moving all critical I/O operations to an asynchronous context managed by the Splash Screen. It also introduces a secure login flow for shared workstations.

## 2. Key Components

### 2.1. Program.cs
- **Role**: Entry point.
- **Changes**: Stripped down to environment setup only. No DB calls.

### 2.2. StartupSplashApplicationContext
- **Role**: Orchestrator.
- **Changes**:
    - Runs `RunStartupAsync`.
    - Checks for Shared Workstation users.
    - Shows `Form_SharedLogin` if needed.
    - Verifies DB connectivity (once).
    - Loads User Settings & Permissions (async).

### 2.3. Form_SharedLogin
- **Role**: Modal dialog for SHOP2/MTMDC users.
- **Features**: Username/PIN input, validation, 3-strike lockout.

## 3. Usage

### 3.1. Normal Startup
1.  Run application.
2.  Splash screen loads.
3.  Main Form opens.

### 3.2. Shared Workstation Startup
1.  Run application on machine logged in as "SHOP2".
2.  Splash screen pauses.
3.  Login dialog appears.
4.  Enter credentials.
5.  Main Form opens as the entered user.

## 4. Development Notes
- **Async**: Ensure all new service methods are `async Task`.
- **Testing**: Use the `mtm_wip_application_winforms_test` database.
- **Logs**: Check `LoggingUtility` output for startup traces.
