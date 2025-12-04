# Error Handling Refactoring Checklist

This document tracks the refactoring of error handling to ensure strict adherence to the "Centralized Error Handling" policy. All direct `MessageBox.Show` calls must be replaced with `Service_ErrorHandler` methods.

## Refactoring Goals
1.  Eliminate all direct `MessageBox.Show` calls in Forms and Controls.
2.  Replace `MessageBox.Show` with `Service_ErrorHandler.ShowUserError`, `ShowWarning`, `ShowInformation`, or `ShowConfirmation`.
3.  Ensure all exceptions are handled via `Service_ErrorHandler.HandleException` or `HandleDatabaseError`.
4.  Review `Service_OnStartup_AppLifecycle.cs` to align with `Service_ErrorHandler` where safe.

## Checklist

- [ ] Refactor `MainForm.cs`
- [ ] Refactor `SettingsForm.cs`
- [ ] Refactor `Dialog_EditParameterOverride.cs`
- [ ] Refactor `Form_QuickButtonEdit.cs`
- [ ] Review and Refactor `Service_OnStartup_AppLifecycle.cs` (Use `Service_ErrorHandler` where possible, keep fallback)

## Implementation Table

| File | Line | Current Implementation | Required Action |
| :--- | :--- | :--- | :--- |
| `MainForm.cs` | 601 | `MessageBox.Show(...)` | Replace with `Service_ErrorHandler.ShowConfirmation(...)` or `ShowWarning(...)` depending on context. |
| `MainForm.cs` | 1483 | `MessageBox.Show("Be aware...", ...)` | Replace with `Service_ErrorHandler.ShowInformation(...)`. |
| `SettingsForm.cs` | 452 | `MessageBox.Show(...)` | Replace with `Service_ErrorHandler.ShowConfirmation(...)`. |
| `Dialog_EditParameterOverride.cs` | 203 | `MessageBox.Show(...)` | Replace with `Service_ErrorHandler.ShowUserError(...)` or `ShowWarning(...)`. |
| `Form_QuickButtonEdit.cs` | 124 | `MessageBox.Show(...)` | Replace with `Service_ErrorHandler.ShowUserError(...)` or `ShowWarning(...)`. |
| `Form_QuickButtonEdit.cs` | 287 | `MessageBox.Show(...)` | Replace with `Service_ErrorHandler.ShowUserError(...)` or `ShowWarning(...)`. |
| `Service_OnStartup_AppLifecycle.cs` | 314 | `MessageBox.Show(...)` (in `ShowDatabaseError`) | Consider replacing with `Service_ErrorHandler.ShowError(...)` or `HandleException`. |
| `Service_OnStartup_AppLifecycle.cs` | 334 | `MessageBox.Show(...)` (in `ShowFatalError`) | Consider replacing with `Service_ErrorHandler.HandleException(..., Fatal)`. |
| `Service_OnStartup_AppLifecycle.cs` | 351 | `MessageBox.Show(...)` (in `ShowNonCriticalError`) | Consider replacing with `Service_ErrorHandler.ShowWarning(...)`. |
| `Service_OnStartup_AppLifecycle.cs` | 368 | `MessageBox.Show(...)` (in `ShowFileSystemError`) | Consider replacing with `Service_ErrorHandler.HandleFileError(...)`. |
| `Service_OnStartup_AppLifecycle.cs` | 385 | `MessageBox.Show(...)` (in `ShowSecurityError`) | Consider replacing with `Service_ErrorHandler.HandleUnauthorizedAccess(...)`. |
| `Service_OnStartup_AppLifecycle.cs` | 402 | `MessageBox.Show(...)` (in `ShowSystemError`) | Consider replacing with `Service_ErrorHandler.ShowError(...)`. |
| `Service_OnStartup_AppLifecycle.cs` | 419 | `MessageBox.Show(...)` (in `ShowTimeoutError`) | Consider replacing with `Service_ErrorHandler.HandleNetworkError(...)`. |

## Notes
- `Service_ErrorHandler` methods automatically log errors, so ensure duplicate logging is removed if present.
- `Service_OnStartup_AppLifecycle.cs` is critical code; ensure `Service_ErrorHandler` is initialized and safe to use before replacing calls. If `Service_ErrorHandler` depends on components not yet initialized during startup, keep the fallback `MessageBox.Show`.
