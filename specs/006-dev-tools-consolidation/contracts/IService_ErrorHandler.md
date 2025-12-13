# Interface Contract: IService_ErrorHandler

**Feature**: 006-dev-tools-consolidation  
**Date**: 2025-12-13  
**Status**: Draft

---

## Overview

`IService_ErrorHandler` is the DI-enabled interface extracted from the static `Service_ErrorHandler`. It provides centralized error handling, exception processing, and user-facing error dialogs while supporting constructor injection.

## Interface Definition

```csharp
namespace MTM_WIP_Application_Winforms.Services.ErrorHandling;

/// <summary>
/// Interface for centralized error handling services.
/// Provides exception handling, user notifications, and error logging integration.
/// </summary>
public interface IService_ErrorHandler
{
    #region Exception Handling

    /// <summary>
    /// Handles any exception with enhanced error dialog and automatic logging.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="severity">The severity level of the error.</param>
    /// <param name="retryAction">Optional action to retry the failed operation.</param>
    /// <param name="contextData">Additional context information.</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    /// <param name="controlName">Name of the control or form where error occurred.</param>
    /// <returns>True if user chose to retry and retry succeeded, false otherwise.</returns>
    bool HandleException(
        Exception ex,
        Enum_ErrorSeverity severity = Enum_ErrorSeverity.Medium,
        Func<bool>? retryAction = null,
        Dictionary<string, object>? contextData = null,
        [CallerMemberName] string callerName = "",
        string controlName = "");

    /// <summary>
    /// Handles database-specific errors with automatic connection recovery.
    /// </summary>
    /// <param name="ex">The database exception.</param>
    /// <param name="retryAction">Optional action to retry the database operation.</param>
    /// <param name="contextData">Additional context information.</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    /// <param name="controlName">Name of the control or form where error occurred.</param>
    /// <param name="methodName">Specific method name if different from caller.</param>
    /// <param name="dbSeverity">Database-specific severity level.</param>
    /// <returns>True if retry was successful.</returns>
    bool HandleDatabaseError(
        Exception ex,
        Func<bool>? retryAction = null,
        Dictionary<string, object>? contextData = null,
        [CallerMemberName] string callerName = "",
        string controlName = "",
        string methodName = "",
        Enum_DatabaseEnum_ErrorSeverity dbSeverity = Enum_DatabaseEnum_ErrorSeverity.Error);

    /// <summary>
    /// Handles file operation errors.
    /// </summary>
    bool HandleFileError(
        Exception ex,
        string filePath = "",
        Func<bool>? retryAction = null,
        [CallerMemberName] string callerName = "",
        string controlName = "");

    /// <summary>
    /// Handles network/connectivity errors.
    /// </summary>
    bool HandleNetworkError(
        Exception ex,
        Func<bool>? retryAction = null,
        [CallerMemberName] string callerName = "",
        string controlName = "");

    /// <summary>
    /// Handles validation errors (user input errors).
    /// </summary>
    void HandleValidationError(
        string message,
        string field = "",
        [CallerMemberName] string callerName = "",
        string controlName = "");

    /// <summary>
    /// Handles unauthorized access with appropriate messaging.
    /// </summary>
    void HandleUnauthorizedAccess(
        string operation = "",
        [CallerMemberName] string callerName = "",
        string controlName = "");

    #endregion

    #region User Notifications

    /// <summary>
    /// Shows a user-friendly error message (validation or business logic error).
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <param name="title">The title of the message box (default: "Error").</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    void ShowUserError(
        string message,
        string title = "Error",
        [CallerMemberName] string callerName = "");

    /// <summary>
    /// Shows a generic error dialog.
    /// </summary>
    void ShowError(
        string message,
        string title = "Error",
        [CallerMemberName] string callerName = "");

    /// <summary>
    /// Shows a warning dialog.
    /// </summary>
    DialogResult ShowWarning(
        string message,
        string title = "Warning",
        MessageBoxButtons buttons = MessageBoxButtons.OKCancel,
        MessageBoxIcon icon = MessageBoxIcon.Warning);

    /// <summary>
    /// Shows an information dialog.
    /// </summary>
    DialogResult ShowInformation(
        string message,
        string title = "Information",
        MessageBoxButtons buttons = MessageBoxButtons.OK,
        MessageBoxIcon icon = MessageBoxIcon.Information,
        string controlName = "");

    /// <summary>
    /// Shows a confirmation dialog.
    /// </summary>
    DialogResult ShowConfirmation(
        string message,
        string title = "Confirmation",
        MessageBoxButtons buttons = MessageBoxButtons.YesNo,
        MessageBoxIcon icon = MessageBoxIcon.Question);

    #endregion

    #region Utility Methods

    /// <summary>
    /// Clears error cooldown state (useful for testing or after resolving known issues).
    /// </summary>
    void ClearErrorCooldownState();

    #endregion
}
```

## Implementation Notes

### Service Registration

```csharp
// In Service_OnStartup_DependencyInjection.ConfigureServices()
services.AddSingleton<IService_ErrorHandler, Service_ErrorHandler>();
```

### Dependency on ILoggingService

The `Service_ErrorHandler` implementation will inject `ILoggingService`:

```csharp
public class Service_ErrorHandler : IService_ErrorHandler
{
    private readonly ILoggingService _logger;
    
    public Service_ErrorHandler(ILoggingService logger)
    {
        _logger = logger;
    }
    
    // Implementation uses _logger instead of static LoggingUtility calls
}
```

### Backward Compatibility

Similar to `ILoggingService`, a static accessor pattern will be used:

```csharp
public class Service_ErrorHandler : IService_ErrorHandler
{
    private static IService_ErrorHandler? _instance;
    
    /// <summary>
    /// Gets the singleton instance of the error handler service.
    /// </summary>
    public static IService_ErrorHandler Instance
    {
        get => _instance ?? throw new InvalidOperationException(
            "Error handler service not initialized.");
        internal set => _instance = value;
    }
    
    // Static wrappers for backward compatibility
    public static bool HandleException(Exception ex, ...) 
        => Instance.HandleException(ex, ...);
}
```

### Error Dialog Behavior

The `HandleException` method:
1. Logs the error via `ILoggingService`
2. Checks error cooldown to prevent spam
3. Shows `EnhancedErrorDialog` for user interaction
4. Handles fatal errors by logging and terminating
5. Returns true if user chose retry and retry succeeded

## Usage Examples

### New Code (Recommended)

```csharp
public class MyForm : ThemedForm
{
    private readonly IService_ErrorHandler _errorHandler;
    
    public MyForm(IService_ErrorHandler errorHandler)
    {
        _errorHandler = errorHandler;
        InitializeComponent();
    }
    
    private async Task LoadDataAsync()
    {
        try
        {
            var result = await _dao.GetDataAsync();
            if (!result.IsSuccess)
            {
                _errorHandler.ShowUserError(result.ErrorMessage);
                return;
            }
            // Process data
        }
        catch (Exception ex)
        {
            _errorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["User"] = Model_Application_Variables.User,
                    ["Operation"] = "LoadData"
                });
        }
    }
}
```

### Legacy Code (Backward Compatible)

```csharp
// Existing code continues to work
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
}
```
