using System;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Logging;

/// <summary>
/// Static wrapper for ILoggingService to maintain backward compatibility.
/// Delegates all calls to Service_Logging.Instance.
/// </summary>
public static class LoggingUtility
{
    #region Initialization

    /// <summary>
    /// Initializes the logging system.
    /// </summary>
    public static async Task InitializeLoggingAsync()
    {
        await Service_Logging.Instance.InitializeAsync();
    }

    /// <summary>
    /// Cleans up old log files.
    /// </summary>
    public static async Task CleanUpOldLogsIfNeededAsync()
    {
        await Service_Logging.Instance.CleanUpOldLogsAsync();
    }

    #endregion

    #region Logging Methods

    /// <summary>
    /// Logs a message with default Information level.
    /// </summary>
    public static void Log(string message)
    {
        Service_Logging.Instance.Log(message);
    }

    /// <summary>
    /// Logs a message with specified level and optional context.
    /// </summary>
    public static void Log(Enum_LogLevel level, string source, string message, string? details = null, Exception? ex = null)
    {
        Service_Logging.Instance.Log(level, source, message, null, ex);
    }

    /// <summary>
    /// Logs an application error with full exception details.
    /// </summary>
    public static void LogApplicationError(Exception ex)
    {
        Service_Logging.Instance.LogApplicationError(ex);
    }

    /// <summary>
    /// Logs a database error with severity classification.
    /// </summary>
    public static void LogDatabaseError(Exception ex, Enum_DatabaseEnum_ErrorSeverity severity = Enum_DatabaseEnum_ErrorSeverity.Error)
    {
        Service_Logging.Instance.LogDatabaseError(ex, severity);
    }

    /// <summary>
    /// Logs an informational application event.
    /// </summary>
    public static void LogApplicationInfo(string message)
    {
        Service_Logging.Instance.LogApplicationInfo(message);
    }

    #endregion
}
