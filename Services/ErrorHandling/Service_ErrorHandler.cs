using System.Runtime.CompilerServices;
using MTM_WIP_Application_Winforms.Forms.ErrorDialog;
using MTM_WIP_Application_Winforms.Forms.MainForm;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services;

internal static class Service_ErrorHandler
{
    #region Fields

    private static readonly Dictionary<string, int> s_errorFrequency = [];
    private static readonly Dictionary<string, DateTime> s_lastErrorTimestamp = [];
    private static readonly object s_errorLock = new();
    private static readonly TimeSpan s_errorCooldownPeriod = TimeSpan.FromSeconds(5);

    #endregion

    #region Public Methods - Enhanced Error Handling

    /// <summary>
    /// Handle any exception with enhanced error dialog and automatic logging.
    /// This is the core handler used by other specific methods.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="severity">The severity level of the error.</param>
    /// <param name="retryAction">Optional action to retry the failed operation.</param>
    /// <param name="contextData">Additional context information.</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    /// <param name="controlName">Name of the control or form where error occurred.</param>
    /// <returns>True if user chose to retry and retry succeeded, false otherwise.</returns>
    public static bool HandleException(Exception ex,
        Enum_ErrorSeverity severity = Enum_ErrorSeverity.Medium,
        Func<bool>? retryAction = null,
        Dictionary<string, object>? contextData = null,
        [CallerMemberName] string callerName = "",
        string controlName = "")
    {
        try
        {
            // Always log the error first
            LoggingUtility.LogApplicationError(ex);
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
            LoggingUtility.LogApplicationError(innerEx);
            FallbackErrorDisplay(ex, callerName);
            return false;
        }
    }

    /// <summary>
    /// Handle database-specific errors with automatic connection recovery.
    /// </summary>
    /// <param name="ex">The database exception.</param>
    /// <param name="retryAction">Optional action to retry the database operation.</param>
    /// <param name="contextData">Additional context information.</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    /// <param name="controlName">Name of the control or form where error occurred.</param>
    /// <param name="methodName">Specific method name if different from caller.</param>
    /// <param name="dbSeverity">Database-specific severity level.</param>
    /// <returns>True if retry was successful.</returns>
    public static bool HandleDatabaseError(Exception ex,
        Func<bool>? retryAction = null,
        Dictionary<string, object>? contextData = null,
        [CallerMemberName] string callerName = "",
        string controlName = "",
        string methodName = "",
        Enum_DatabaseEnum_ErrorSeverity dbSeverity = Enum_DatabaseEnum_ErrorSeverity.Error)
    {
        // Add database-specific context
        var dbContextData = contextData ?? [];
        dbContextData["ErrorType"] = "Database";
        dbContextData["ConnectionString"] = "Hidden for security";
        dbContextData["DatabaseSeverity"] = dbSeverity.ToString();

        LoggingUtility.LogDatabaseError(ex, dbSeverity);

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

        return HandleException(ex, uiSeverity, retryAction, dbContextData, effectiveCallerName, controlName);
    }

    /// <summary>
    /// Show a user-friendly error message (validation or business logic error).
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <param name="title">The title of the message box (default: "Error").</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    public static void ShowUserError(string message, string title = "Error", [CallerMemberName] string callerName = "")
    {
        try
        {
            // Log as warning/info, not error
            LoggingUtility.Log($"[User Error] {message} (Caller: {callerName})");

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    /// <summary>
    /// Show a generic error dialog (not an exception - just a user error message).
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <param name="title">The title of the message box (default: "Error").</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    public static void ShowError(string message, string title = "Error", [CallerMemberName] string callerName = "")
    {
        try
        {
            // Log as error
            LoggingUtility.Log($"[User Error] {message} (Caller: {callerName})");

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    /// <summary>
    /// Show a confirmation dialog (not an error - just a user confirmation).
    /// </summary>
    /// <param name="message">The confirmation message.</param>
    /// <param name="title">The title of the dialog (default: "Confirmation").</param>
    /// <param name="buttons">The buttons to display (default: YesNo).</param>
    /// <param name="icon">The icon to display (default: Question).</param>
    /// <returns>The DialogResult chosen by the user.</returns>
    public static DialogResult ShowConfirmation(string message, string title = "Confirmation",
        MessageBoxButtons buttons = MessageBoxButtons.YesNo,
        MessageBoxIcon icon = MessageBoxIcon.Question)
    {
        try
        {
            return MessageBox.Show(message, title, buttons, icon);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DialogResult.Cancel;
        }
    }

    /// <summary>
    /// Show a warning dialog (not an error - just a user warning).
    /// </summary>
    /// <param name="message">The warning message.</param>
    /// <param name="title">The title of the dialog (default: "Warning").</param>
    /// <param name="buttons">The buttons to display (default: OKCancel).</param>
    /// <param name="icon">The icon to display (default: Warning).</param>
    /// <returns>The DialogResult chosen by the user.</returns>
    public static DialogResult ShowWarning(string message, string title = "Warning",
        MessageBoxButtons buttons = MessageBoxButtons.OKCancel,
        MessageBoxIcon icon = MessageBoxIcon.Warning)
    {
        try
        {
            return MessageBox.Show(message, title, buttons, icon);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DialogResult.Cancel;
        }
    }

    /// <summary>
    /// Show an information dialog (not an error - just informational message).
    /// </summary>
    /// <param name="message">The information message.</param>
    /// <param name="title">The title of the dialog (default: "Information").</param>
    /// <param name="buttons">The buttons to display (default: OK).</param>
    /// <param name="icon">The icon to display (default: Information).</param>
    /// <param name="controlName">Optional control name for logging context.</param>
    /// <returns>The DialogResult chosen by the user.</returns>
    public static DialogResult ShowInformation(string message, string title = "Information",
        MessageBoxButtons buttons = MessageBoxButtons.OK,
        MessageBoxIcon icon = MessageBoxIcon.Information,
        string controlName = "")
    {
        try
        {
            var logMessage = $"Information dialog shown: {title} - {message}";
            if (!string.IsNullOrEmpty(controlName))
            {
                logMessage += $" (Control: {controlName})";
            }
            LoggingUtility.Log(logMessage);

            return MessageBox.Show(message, title, buttons, icon);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DialogResult.Cancel;
        }
    }

    /// <summary>
    /// Handle validation errors (user input errors).
    /// </summary>
    /// <param name="message">The validation error message.</param>
    /// <param name="field">The name of the field that failed validation.</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    /// <param name="controlName">Name of the control where validation failed.</param>
    public static void HandleValidationError(string message, string field = "",
        [CallerMemberName] string callerName = "",
        string controlName = "")
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

            HandleException(validationEx, Enum_ErrorSeverity.Low, null, contextData, callerName, controlName);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    /// <summary>
    /// Handle file operation errors.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="filePath">The path of the file involved.</param>
    /// <param name="retryAction">Optional retry action.</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    /// <param name="controlName">Name of the control or form.</param>
    /// <returns>True if retry was successful.</returns>
    public static bool HandleFileError(Exception ex, string filePath = "",
        Func<bool>? retryAction = null,
        [CallerMemberName] string callerName = "",
        string controlName = "")
    {
        try
        {
            var contextData = new Dictionary<string, object>
            {
                ["FilePath"] = filePath,
                ["FileExists"] = !string.IsNullOrEmpty(filePath) && File.Exists(filePath),
                ["ErrorType"] = "File Operation"
            };

            return HandleException(ex, Enum_ErrorSeverity.Medium, retryAction, contextData, callerName, controlName);
        }
        catch (Exception innerEx)
        {
            LoggingUtility.LogApplicationError(innerEx);
            return false;
        }
    }

    /// <summary>
    /// Handle network/connectivity errors.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="retryAction">Optional retry action.</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    /// <param name="controlName">Name of the control or form.</param>
    /// <returns>True if retry was successful.</returns>
    public static bool HandleNetworkError(Exception ex,
        Func<bool>? retryAction = null,
        [CallerMemberName] string callerName = "",
        string controlName = "")
    {
        try
        {
            var contextData = new Dictionary<string, object>
            {
                ["ErrorType"] = "Network/Connectivity",
                ["NetworkAvailable"] = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()
            };

            return HandleException(ex, Enum_ErrorSeverity.High, retryAction, contextData, callerName, controlName);
        }
        catch (Exception innerEx)
        {
            LoggingUtility.LogApplicationError(innerEx);
            return false;
        }
    }

    /// <summary>
    /// Handle unauthorized access with appropriate messaging.
    /// </summary>
    /// <param name="operation">The operation that was attempted.</param>
    /// <param name="callerName">Automatically filled caller method name.</param>
    /// <param name="controlName">Name of the control or form.</param>
    public static void HandleUnauthorizedAccess(string operation = "",
        [CallerMemberName] string callerName = "",
        string controlName = "")
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

            HandleException(unauthorizedEx, Enum_ErrorSeverity.Medium, null, contextData, callerName, controlName);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    /// <summary>
    /// Clear error cooldown state (useful for testing or after resolving known issues).
    /// </summary>
    public static void ClearErrorCooldownState()
    {
        lock (s_errorLock)
        {
            s_lastErrorTimestamp.Clear();
            s_errorFrequency.Clear();
        }
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

    private static void LogErrorContext(string callerName, string controlName, Dictionary<string, object>? contextData)
    {
        try
        {
            var contextLog = $"Error context - Caller: {callerName}, Control: {controlName}";
            if (contextData?.Count > 0)
            {
                contextLog += $", Context: {string.Join(", ", contextData.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
            }
            LoggingUtility.Log(contextLog);
        }
        catch (Exception logEx)
        {
            // Don't let logging errors break error handling
            System.Diagnostics.Debug.WriteLine($"Failed to log error context: {logEx.Message}");
        }
    }

    private static bool IsDatabaseError(Exception ex)
    {
        return ex is MySql.Data.MySqlClient.MySqlException ||
               ex.Message.Contains("database", StringComparison.OrdinalIgnoreCase) ||
               ex.Message.Contains("connection", StringComparison.OrdinalIgnoreCase) ||
               ex.Message.Contains("mysql", StringComparison.OrdinalIgnoreCase) ||
               ex.Message.Contains("sql", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsFatalError(Exception ex)
    {
        return ex is OutOfMemoryException ||
               ex is StackOverflowException ||
               ex is AccessViolationException ||
               ex is System.Security.SecurityException;
    }

    private static void HandleConnectionRecovery()
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
            LoggingUtility.LogApplicationError(ex);
        }
    }

    private static bool ShouldSuppressError(Exception ex, string callerName)
    {
        lock (s_errorLock)
        {
            var errorKey = $"{ex.GetType().Name}:{callerName}:{ex.Message.GetHashCode()}";
            var now = DateTime.Now;

            // Check if this is a duplicate error within the cooldown period
            if (s_lastErrorTimestamp.TryGetValue(errorKey, out var lastTimestamp))
            {
                var timeSinceLastError = now - lastTimestamp;

                // Update frequency counter
                if (s_errorFrequency.ContainsKey(errorKey))
                {
                    s_errorFrequency[errorKey]++;
                }
                else
                {
                    s_errorFrequency[errorKey] = 1;
                }

                // Log that we're suppressing the UI display but still logging to database
                if (timeSinceLastError < s_errorCooldownPeriod)
                {
                    // Update timestamp for next occurrence
                    s_lastErrorTimestamp[errorKey] = now;
                    return true; // Suppress UI display
                }

                // Cooldown period expired, allow display but check frequency
                s_lastErrorTimestamp[errorKey] = now;

                // Suppress if we've seen this error more than 10 times in this session (spam protection)
                if (s_errorFrequency[errorKey] > 10)
                {
                    return true;
                }

                return false; // Allow UI display
            }

            // First occurrence of this error
            s_lastErrorTimestamp[errorKey] = now;
            s_errorFrequency[errorKey] = 1;
            return false; // Allow UI display
        }
    }

    private static void HandleFatalError(Exception ex, string callerName)
    {
        try
        {
            // Give user a chance to save work or see what happened
            var message = $"A fatal error has occurred and the application must close.\n\n" +
                         $"Error: {ex.Message}\n\n" +
                         $"Location: {callerName}\n" +
                         $"The error details have been logged for analysis.";

            MessageBox.Show(message, "Fatal Application Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            // Force application termination
            Environment.Exit(1);
        }
        catch
        {
            // If even our fatal error handler fails, just terminate
            Environment.Exit(1);
        }
    }

    private static void FallbackErrorDisplay(Exception ex, string callerName)
    {
        try
        {
            var message = $"An error occurred in {callerName}:\n\n{ex.Message}";
            MessageBox.Show(message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch
        {
            // Last resort - show basic system error
            MessageBox.Show($"Critical error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static bool IsRunningAsAdministrator()
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
