# Research: Startup Process Analysis and Hardening

**Branch**: `002-startup-audit` | **Date**: 2025-11-25

## 1. Unknowns & Clarifications

### 1.1. Shared Workstation Login UI
**Question**: Should the Shared Workstation Login use a new Form or a dialog within the Splash Screen?
**Decision**: Use a new modal `Form_Login` (or similar) triggered from `StartupSplashApplicationContext`.
**Rationale**: Keeps the Splash Screen logic simple. A dedicated form allows for proper input validation, masking, and theming.
**Alternatives**:
- *Extend Splash Screen*: Too complex, mixes concerns.
- *InputBox*: Too simple, no masking or validation.

### 1.2. Service_OnStartup_User Refactoring
**Question**: `LoadUserSettings` currently returns `void` and calls DB synchronously. How deep does the refactoring go?
**Decision**: Change signature to `public static async Task LoadUserSettingsAsync()`. Update all internal DAO calls to be awaited.
**Rationale**: Compliance with Constitution (Async-First).
**Impact**: `Program.cs` can no longer call this. It MUST be called from an async context (`StartupSplashApplicationContext`).

### 1.3. SplashScreenForm Threading
**Question**: `StartupSplashApplicationContext` runs `RunStartupAsync`. Does `SplashScreenForm` need `Invoke` for updates?
**Decision**: Yes, if `RunStartupAsync` runs on a background thread (via `Task.Run` or similar). However, `ApplicationContext` runs on the UI thread. `await` yields control, but continuations run on the captured context (UI thread).
**Verification**: `RunStartupAsync` is `async void` (or `async Task` called without await in constructor/event).
**Refinement**: `RunStartupAsync` is called from `Shown` event. It runs on UI thread. `await` yields to message loop. Updates after `await` are back on UI thread. No `Invoke` needed unless `Task.Run` is explicitly used for CPU-bound work.
**Action**: Ensure `RunStartupAsync` does NOT use `Task.Run` for UI updates, but DOES use it for heavy synchronous work (if any remains). DB calls are async IO, so they are fine.

## 2. Technology Choices

### 2.1. Login Form
- **Base**: `ThemedForm` (Constitution IX)
- **Controls**: `ThemedTextBox` (Username), `ThemedTextBox` (PIN, PasswordChar='*'), `ThemedButton` (Login, Cancel)
- **Validation**: `Service_Validation` (Constitution XI)

### 2.2. PIN Hashing
- **Current State**: Check `usr_users` table schema. Is PIN hashed?
- **Assumption**: PIN is likely plain text or simple hash in legacy DB.
- **Action**: Match existing authentication pattern found in `Dao_User` or `Dao_System`. Do not invent new crypto unless specified.

## 3. Best Practices

### 3.1. Startup Sequence
- **Pattern**: "Splash Screen as Application Context"
- **Benefit**: Allows complex async logic before the Main Form exists.
- **Reference**: `Service_Onstartup_StartupSplashApplicationContext.cs` (existing)

### 3.2. Async Void
- **Rule**: Avoid `async void` except for event handlers.
- **Application**: `SplashScreen.Shown` handler will be `async void`. It calls `RunStartupAsync()`.

## 4. Consolidated Findings

- **Refactoring**: `Service_OnStartup_User` needs full async conversion.
- **UI**: New `Form_SharedLogin` required.
- **Logic**: `StartupSplashApplicationContext` becomes the central orchestrator.
- **Safety**: `Program.cs` becomes purely synchronous setup.

