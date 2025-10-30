namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Represents a single parsed log entry from any log format type.
/// Supports Normal, ApplicationError, and DatabaseError log formats with
/// type-specific properties and factory methods for safe construction.
/// </summary>
public class Model_LogEntry
{
    #region Common Properties

    /// <summary>
    /// Timestamp when the log entry was created.
    /// </summary>
    public required DateTime Timestamp { get; init; }

    /// <summary>
    /// Format type of this log entry (Normal, ApplicationError, DatabaseError, or Unknown).
    /// </summary>
    public required LogFormat LogType { get; init; }

    /// <summary>
    /// Raw text of the log entry as it appears in the file.
    /// Always populated regardless of parse success.
    /// </summary>
    public required string RawText { get; init; }

    /// <summary>
    /// Indicates whether the log entry was successfully parsed into structured fields.
    /// False indicates parsing failed and only RawText is reliable.
    /// </summary>
    public required bool ParseSuccess { get; init; }

    #endregion

    #region Normal Log Properties

    /// <summary>
    /// Log level for Normal log entries (INFO, WARNING, ERROR, etc.).
    /// Null for other log types.
    /// </summary>
    public string? Level { get; init; }

    /// <summary>
    /// Emoji indicator for Normal log entries (ℹ️, ⚠️, ❌, etc.).
    /// Null for other log types.
    /// </summary>
    public string? Emoji { get; init; }

    /// <summary>
    /// Source component or module that generated the log entry.
    /// Used in Normal logs, may be present in error logs.
    /// </summary>
    public string? Source { get; init; }

    /// <summary>
    /// Primary log message text.
    /// Present in all log types.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Additional details or JSON payload for Normal log entries.
    /// Null if no details present.
    /// </summary>
    public string? Details { get; init; }

    #endregion

    #region Application Error Properties

    /// <summary>
    /// Type of application error (e.g., "System.NullReferenceException").
    /// Null for non-ApplicationError log types.
    /// </summary>
    public string? ErrorType { get; init; }

    #endregion

    #region Database Error Properties

    /// <summary>
    /// Database error severity level (e.g., "CRITICAL", "ERROR", "WARNING").
    /// Null for non-DatabaseError log types.
    /// </summary>
    public string? Severity { get; init; }

    #endregion

    #region Common Error Properties

    /// <summary>
    /// Stack trace for error log entries.
    /// Null for Normal logs or errors without stack traces.
    /// </summary>
    public string? StackTrace { get; init; }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a Normal log entry with standard log level, emoji, source, and message.
    /// </summary>
    /// <param name="timestamp">When the log entry was created.</param>
    /// <param name="level">Log level (INFO, WARNING, ERROR, etc.).</param>
    /// <param name="emoji">Emoji indicator (ℹ️, ⚠️, ❌, etc.).</param>
    /// <param name="source">Source component that generated the log.</param>
    /// <param name="message">Primary log message.</param>
    /// <param name="details">Additional details or JSON payload (optional).</param>
    /// <param name="rawText">Original log text.</param>
    /// <returns>A new Normal log entry instance.</returns>
    public static Model_LogEntry CreateNormalEntry(
        DateTime timestamp,
        string level,
        string emoji,
        string source,
        string message,
        string? details,
        string rawText)
    {
        return new Model_LogEntry
        {
            Timestamp = timestamp,
            LogType = LogFormat.Normal,
            ParseSuccess = true,
            Level = level,
            Emoji = emoji,
            Source = source,
            Message = message,
            Details = details,
            RawText = rawText
        };
    }

    /// <summary>
    /// Creates an Application Error log entry with error type and stack trace.
    /// </summary>
    /// <param name="timestamp">When the error occurred.</param>
    /// <param name="errorType">Type of application error (exception type).</param>
    /// <param name="message">Error message.</param>
    /// <param name="stackTrace">Stack trace (optional).</param>
    /// <param name="rawText">Original log text.</param>
    /// <returns>A new ApplicationError log entry instance.</returns>
    public static Model_LogEntry CreateApplicationErrorEntry(
        DateTime timestamp,
        string errorType,
        string message,
        string? stackTrace,
        string rawText)
    {
        return new Model_LogEntry
        {
            Timestamp = timestamp,
            LogType = LogFormat.ApplicationError,
            ParseSuccess = true,
            ErrorType = errorType,
            Message = message,
            StackTrace = stackTrace,
            RawText = rawText
        };
    }

    /// <summary>
    /// Creates a Database Error log entry with severity and database-specific details.
    /// </summary>
    /// <param name="timestamp">When the database error occurred.</param>
    /// <param name="severity">Error severity (CRITICAL, ERROR, WARNING).</param>
    /// <param name="message">Error message.</param>
    /// <param name="details">Database-specific details (error codes, query, etc.).</param>
    /// <param name="rawText">Original log text.</param>
    /// <returns>A new DatabaseError log entry instance.</returns>
    public static Model_LogEntry CreateDatabaseErrorEntry(
        DateTime timestamp,
        string severity,
        string message,
        string? details,
        string rawText)
    {
        return new Model_LogEntry
        {
            Timestamp = timestamp,
            LogType = LogFormat.DatabaseError,
            ParseSuccess = true,
            Severity = severity,
            Message = message,
            Details = details,
            RawText = rawText
        };
    }

    /// <summary>
    /// Creates a raw/unparsed log entry when format detection or parsing fails.
    /// Only RawText is populated; all other fields are null.
    /// </summary>
    /// <param name="timestamp">Timestamp extracted from filename or current time.</param>
    /// <param name="rawText">Original log text.</param>
    /// <returns>A new Unknown log entry instance with ParseSuccess=false.</returns>
    public static Model_LogEntry CreateRawEntry(DateTime timestamp, string rawText)
    {
        return new Model_LogEntry
        {
            Timestamp = timestamp,
            LogType = LogFormat.Unknown,
            ParseSuccess = false,
            RawText = rawText
        };
    }

    #endregion
}
