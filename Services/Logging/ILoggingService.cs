using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Logging;

/// <summary>
/// Interface for application logging services.
/// Provides structured logging to CSV files with support for different severity levels.
/// </summary>
public interface ILoggingService
{
    #region Core Logging Methods

    /// <summary>
    /// Logs a message with default Information level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Log(string message);

    /// <summary>
    /// Logs a message with specified level and optional context.
    /// </summary>
    /// <param name="level">The log level (Information, Warning, Error, Critical).</param>
    /// <param name="source">The source component (module, class, method).</param>
    /// <param name="message">The message to log.</param>
    /// <param name="user">Optional user identifier.</param>
    /// <param name="exception">Optional exception for error logs.</param>
    void Log(Enum_LogLevel level, string source, string message, string? user = null, Exception? exception = null);

    #endregion

    #region Specialized Logging Methods

    /// <summary>
    /// Logs an application error with full exception details.
    /// </summary>
    /// <param name="ex">The exception to log.</param>
    /// <param name="additionalMessage">Optional additional context message.</param>
    void LogApplicationError(Exception ex, string? additionalMessage = null);

    /// <summary>
    /// Logs a database error with severity classification.
    /// </summary>
    /// <param name="ex">The database exception.</param>
    /// <param name="severity">The database error severity level.</param>
    void LogDatabaseError(Exception ex, Enum_DatabaseEnum_ErrorSeverity severity = Enum_DatabaseEnum_ErrorSeverity.Error);

    /// <summary>
    /// Logs an informational application event.
    /// </summary>
    /// <param name="message">The information message.</param>
    void LogApplicationInfo(string message);

    #endregion

    #region Initialization and Cleanup

    /// <summary>
    /// Initializes the logging system (creates directories, sets up file paths).
    /// Should be called once at application startup.
    /// </summary>
    Task InitializeAsync();

    /// <summary>
    /// Cleans up old log files based on retention policy.
    /// </summary>
    /// <param name="retentionDays">Number of days to retain logs (default: 30).</param>
    Task CleanUpOldLogsAsync(int retentionDays = 30);

    #endregion
}
