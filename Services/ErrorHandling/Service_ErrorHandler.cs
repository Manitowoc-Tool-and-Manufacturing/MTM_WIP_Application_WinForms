using System.Runtime.CompilerServices;
using MTM_WIP_Application_Winforms.Forms.ErrorDialog;
using MTM_WIP_Application_Winforms.Forms.MainForm;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Services;

public class Service_ErrorHandler : IService_ErrorHandler
{
    #region Fields

    private readonly ILoggingService _logger;
    private readonly Dictionary<string, int> _errorFrequency = [];
    private readonly Dictionary<string, DateTime> _lastErrorTimestamp = [];
    private readonly object _errorLock = new();
    private readonly TimeSpan _errorCooldownPeriod = TimeSpan.FromSeconds(5);

    // Static instance for backward compatibility
    private static IService_ErrorHandler? _instance;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the error handler service.
    /// </summary>
    public static IService_ErrorHandler Instance
    {
        get => _instance ?? throw new InvalidOperationException(
            "Error handler service not initialized. Ensure DI container is configured.");
        internal set => _instance = value;
    }

    #endregion

    #region Constructor

    public Service_ErrorHandler(ILoggingService logger)
    {
        _logger = logger;
    }

    #endregion

    #region IService_ErrorHandler Implementation

    bool IService_ErrorHandler.HandleException(Exception ex,
        Enum_ErrorSeverity severity,
        Func<bool>? retryAction,
        Dictionary<string, object>? contextData,
        string callerName,
        string controlName)
    {
        try
        {
            // Always log the error first
            _logger.LogApplicationError(ex);
            LogErrorContext(callerName, controlName, contextData);

            // Handle connection recovery if it's a database error
            if (IsDatabaseError(ex))
            {
                HandleConnectionRecovery();
            }

            // Check error frequency to prevent spam
            if (ShouldSuppressError(ex, callerName))
            {
                return false;
            }

            // Show enhanced error dialog
            using var errorDialog = new EnhancedErrorDialog(ex, callerName, controlName, severity, retryAction, contextData);
            var result = errorDialog.ShowDialog();

            // Handle critical errors that should terminate the application
            if (severity == Enum_ErrorSeverity.Fatal || IsFatalError(ex))
            {
                HandleFatalError(ex, callerName);
                return false;
            }

            return errorDialog.ShouldRetry && result == DialogResult.Retry;
        }
        catch (Exception innerEx)
        {
            // Fallback error handling if our enhanced handler fails
            _logger.LogApplicationError(innerEx);
            FallbackErrorDisplay(ex, callerName);
            return false;
        }
    }

    bool IService_ErrorHandler.HandleDatabaseError(Exception ex,
        Func<bool>? retryAction,
        Dictionary<string, object>? contextData,
        string callerName,
        string controlName,
        string methodName,
        Enum_DatabaseEnum_ErrorSeverity dbSeverity)
    {
        // Add database-specific context
        var dbContextData = contextData ?? [];
        dbContextData["ErrorType"] = "Database";
        dbContextData["ConnectionString"] = "Hidden for security";
        dbContextData["DatabaseSeverity"] = dbSeverity.ToString();

        _logger.LogDatabaseError(ex, dbSeverity);

        // Use methodName if provided, otherwise use callerName
        var effectiveCallerName = !string.IsNullOrEmpty(methodName) ? methodName : callerName;

        // Map database severity to general error severity for UI display
        var uiSeverity = dbSeverity switch
        {
            Enum_DatabaseEnum_ErrorSeverity.Warning => Enum_ErrorSeverity.Low,
            Enum_DatabaseEnum_ErrorSeverity.Error => Enum_ErrorSeverity.Medium,
            Enum_DatabaseEnum_ErrorSeverity.Critical => Enum_ErrorSeverity.High,
            _ => Enum_ErrorSeverity.Medium
        };

        return ((IService_ErrorHandler)this).HandleException(ex, uiSeverity, retryAction, dbContextData, effectiveCallerName, controlName);
    }

    void IService_ErrorHandler.ShowUserError(string message, string title, string callerName)
    {
        try
        {
            // Log as warning/info, not error
            _logger.Log($"[User Error] {message} (Caller: {callerName})");

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
        }
    }

    void IService_ErrorHandler.ShowError(string message, string title, string callerName)
    {
        try
        {
            // Log as error
            _logger.Log($"[User Error] {message} (Caller: {callerName})");

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
        }
    }

    DialogResult IService_ErrorHandler.ShowConfirmation(string message, string title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon)
    {
        try
        {
            return MessageBox.Show(message, title, buttons, icon);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return DialogResult.Cancel;
        }
    }

    DialogResult IService_ErrorHandler.ShowWarning(string message, string title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon)
    {
        try
        {
            return MessageBox.Show(message, title, buttons, icon);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return DialogResult.Cancel;
        }
    }

    DialogResult IService_ErrorHandler.ShowInformation(string message, string title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        string controlName)
    {
        try
        {
            var logMessage = $"Information dialog shown: {title} - {message}";
            if (!string.IsNullOrEmpty(controlName))
            {
                logMessage += $" (Control: {controlName})";
            }
            _logger.Log(logMessage);

            return MessageBox.Show(message, title, buttons, icon);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return DialogResult.Cancel;
        }
    }

    void IService_ErrorHandler.HandleValidationError(string message, string field,
        string callerName,
        string controlName)
    {
        try
        {
            var validationEx = new ArgumentException($"Validation failed for {field}: {message}");
            var contextData = new Dictionary<string, object>
            {
                ["ValidationType"] = "Input Validation",
                ["Field"] = field,
                ["UserMessage"] = message
            };

            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _logger.LogApplicationError(validationEx);
            LogErrorContext(callerName, controlName, contextData);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
        }
    }

    bool IService_ErrorHandler.HandleFileError(Exception ex, string filePath,
        Func<bool>? retryAction,
        string callerName,
        string controlName)
    {
        try
        {
            var contextData = new Dictionary<string, object>
            {
                ["FilePath"] = filePath,
                ["FileExists"] = !string.IsNullOrEmpty(filePath) && File.Exists(filePath),
                ["ErrorType"] = "File Operation"
            };

            return ((IService_ErrorHandler)this).HandleException(ex, Enum_ErrorSeverity.Medium, retryAction, contextData, callerName, controlName);
        }
        catch (Exception innerEx)
        {
            _logger.LogApplicationError(innerEx);
            return false;
        }
    }

    bool IService_ErrorHandler.HandleNetworkError(Exception ex,
        Func<bool>? retryAction,
        string callerName,
        string controlName)
    {
        try
        {
            var contextData = new Dictionary<string, object>
            {
                ["ErrorType"] = "Network/Connectivity",
                ["NetworkAvailable"] = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()
            };

            return ((IService_ErrorHandler)this).HandleException(ex, Enum_ErrorSeverity.High, retryAction, contextData, callerName, controlName);
        }
        catch (Exception innerEx)
        {
            _logger.LogApplicationError(innerEx);
            return false;
        }
    }

    void IService_ErrorHandler.HandleUnauthorizedAccess(string operation,
        string callerName,
        string controlName)
    {
        try
        {
            var unauthorizedEx = new UnauthorizedAccessException(
                $"Access denied for operation: {operation}. Please check your permissions or run as administrator.");

            var contextData = new Dictionary<string, object>
            {
                ["Operation"] = operation,
                ["UserName"] = Environment.UserName,
                ["IsAdmin"] = IsRunningAsAdministrator()
            };

            ((IService_ErrorHandler)this).HandleException(unauthorizedEx, Enum_ErrorSeverity.Medium, null, contextData, callerName, controlName);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
        }
    }

    void IService_ErrorHandler.ClearErrorCooldownState()
    {
        lock (_errorLock)
        {
            _lastErrorTimestamp.Clear();
            _errorFrequency.Clear();
        }
    }

    #endregion

    #region Static Wrappers for Backward Compatibility

    public static bool HandleException(Exception ex,
        Enum_ErrorSeverity severity = Enum_ErrorSeverity.Medium,
        Func<bool>? retryAction = null,
        Dictionary<string, object>? contextData = null,
        [CallerMemberName] string callerName = "",
        string controlName = "")
    {
        return Instance.HandleException(ex, severity, retryAction, contextData, callerName, controlName);
    }

    public static bool HandleDatabaseError(Exception ex,
        Func<bool>? retryAction = null,
        Dictionary<string, object>? contextData = null,
        [CallerMemberName] string callerName = "",
        string controlName = "",
        string methodName = "",
        Enum_DatabaseEnum_ErrorSeverity dbSeverity = Enum_DatabaseEnum_ErrorSeverity.Error)
    {
        return Instance.HandleDatabaseError(ex, retryAction, contextData, callerName, controlName, methodName, dbSeverity);
    }

    public static void ShowUserError(string message, string title = "Error", [CallerMemberName] string callerName = "")
    {
        Instance.ShowUserError(message, title, callerName);
    }

    public static void ShowError(string message, string title = "Error", [CallerMemberName] string callerName = "")
    {
        Instance.ShowError(message, title, callerName);
    }

    public static DialogResult ShowConfirmation(string message, string title = "Confirmation",
        MessageBoxButtons buttons = MessageBoxButtons.YesNo,
        MessageBoxIcon icon = MessageBoxIcon.Question)
    {
        return Instance.ShowConfirmation(message, title, buttons, icon);
    }

    public static DialogResult ShowWarning(string message, string title = "Warning",
        MessageBoxButtons buttons = MessageBoxButtons.OKCancel,
        MessageBoxIcon icon = MessageBoxIcon.Warning)
    {
        return Instance.ShowWarning(message, title, buttons, icon);
    }

    public static DialogResult ShowInformation(string message, string title = "Information",
        MessageBoxButtons buttons = MessageBoxButtons.OK,
        MessageBoxIcon icon = MessageBoxIcon.Information,
        string controlName = "")
    {
        return Instance.ShowInformation(message, title, buttons, icon, controlName);
    }

    public static void HandleValidationError(string message, string field = "",
        [CallerMemberName] string callerName = "",
        string controlName = "")
    {
        Instance.HandleValidationError(message, field, callerName, controlName);
    }

    public static bool HandleFileError(Exception ex, string filePath = "",
        Func<bool>? retryAction = null,
        [CallerMemberName] string callerName = "",
        string controlName = "")
    {
        return Instance.HandleFileError(ex, filePath, retryAction, callerName, controlName);
    }

    public static bool HandleNetworkError(Exception ex,
        Func<bool>? retryAction = null,
        [CallerMemberName] string callerName = "",
        string controlName = "")
    {
        return Instance.HandleNetworkError(ex, retryAction, callerName, controlName);
    }

    public static void HandleUnauthorizedAccess(string operation = "",
        [CallerMemberName] string callerName = "",
        string controlName = "")
    {
        Instance.HandleUnauthorizedAccess(operation, callerName, controlName);
    }

    public static void ClearErrorCooldownState()
    {
        Instance.ClearErrorCooldownState();
    }

    #endregion

    #region Legacy Compatibility Methods

    [Obsolete("Use HandleException or HandleDatabaseError instead", false)]
    public static void HandleDatabaseError()
    {
        var dbEx = new Exception("Legacy database error handling - please update to use new HandleDatabaseError method");
        HandleDatabaseError(dbEx);
    }

    [Obsolete("Use HandleException instead", false)]
    public static void HandleGeneralException(Exception ex)
    {
        HandleException(ex, Enum_ErrorSeverity.Medium);
    }

    [Obsolete("Use HandleUnauthorizedAccess instead", false)]
    public static void HandleUnauthorizedAccessException(UnauthorizedAccessException ex)
    {
        HandleUnauthorizedAccess(ex.Message);
    }

    #endregion

    #region Private Helper Methods

    private void LogErrorContext(string callerName, string controlName, Dictionary<string, object>? contextData)
    {
        try
        {
            var contextLog = $"Error context - Caller: {callerName}, Control: {controlName}";
            if (contextData?.Count > 0)
            {
                contextLog += $", Context: {string.Join(", ", contextData.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
            }
            _logger.Log(contextLog);
        }
        catch (Exception logEx)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to log error context: {logEx.Message}");
        }
    }

    private bool IsDatabaseError(Exception ex)
    {
        return ex is MySql.Data.MySqlClient.MySqlException ||
               ex.Message.Contains("database", StringComparison.OrdinalIgnoreCase) ||
               ex.Message.Contains("connection", StringComparison.OrdinalIgnoreCase) ||
               ex.Message.Contains("mysql", StringComparison.OrdinalIgnoreCase) ||
               ex.Message.Contains("sql", StringComparison.OrdinalIgnoreCase);
    }

    private bool IsFatalError(Exception ex)
    {
        return ex is OutOfMemoryException ||
               ex is StackOverflowException ||
               ex is AccessViolationException ||
               ex is System.Security.SecurityException;
    }

    private void HandleConnectionRecovery()
    {
        try
        {
            if (Application.OpenForms.OfType<MainForm>().Any())
            {
                var mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                mainForm?.ConnectionRecoveryManager.HandleConnectionLost();
            }
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
        }
    }

    private bool ShouldSuppressError(Exception ex, string callerName)
    {
        lock (_errorLock)
        {
            var errorKey = $"{ex.GetType().Name}:{callerName}:{ex.Message.GetHashCode()}";
            var now = DateTime.Now;

            if (_lastErrorTimestamp.TryGetValue(errorKey, out var lastTimestamp))
            {
                var timeSinceLastError = now - lastTimestamp;

                if (_errorFrequency.ContainsKey(errorKey))
                {
                    _errorFrequency[errorKey]++;
                }
                else
                {
                    _errorFrequency[errorKey] = 1;
                }

                if (timeSinceLastError < _errorCooldownPeriod)
                {
                    _lastErrorTimestamp[errorKey] = now;
                    return true;
                }

                _lastErrorTimestamp[errorKey] = now;

                if (_errorFrequency[errorKey] > 10)
                {
                    return true;
                }

                return false;
            }

            _lastErrorTimestamp[errorKey] = now;
            _errorFrequency[errorKey] = 1;
            return false;
        }
    }

    private void HandleFatalError(Exception ex, string callerName)
    {
        try
        {
            var message = $"A fatal error has occurred and the application must close.\n\n" +
                         $"Error: {ex.Message}\n\n" +
                         $"Location: {callerName}\n" +
                         $"The error details have been logged for analysis.";

            MessageBox.Show(message, "Fatal Application Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            Environment.Exit(1);
        }
        catch
        {
            Environment.Exit(1);
        }
    }

    private void FallbackErrorDisplay(Exception ex, string callerName)
    {
        try
        {
            var message = $"An error occurred in {callerName}:\n\n{ex.Message}";
            MessageBox.Show(message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch
        {
            MessageBox.Show($"Critical error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool IsRunningAsAdministrator()
    {
        try
        {
            using var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        catch
        {
            return false;
        }
    }

    #endregion
}
