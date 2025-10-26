namespace MTM_Inventory_Application.Models;

/// <summary>
/// Defines the types of log formats that can be parsed by the log viewing system.
/// </summary>
public enum LogFormat
{
    /// <summary>
    /// Unknown or unparseable log format. Used when format detection fails.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Standard application log format with timestamp, level, source, and message.
    /// Format: [yyyy-MM-dd HH:mm:ss] [LEVEL] [emoji] [SOURCE] Message
    /// </summary>
    Normal = 1,

    /// <summary>
    /// Application error log format with error type, stack trace, and context.
    /// Format: [yyyy-MM-dd HH:mm:ss] Application Error: ErrorType
    /// </summary>
    ApplicationError = 2,

    /// <summary>
    /// Database error log format with severity, MySQL error codes, and query details.
    /// Format: [yyyy-MM-dd HH:mm:ss] [SEVERITY] Database Error: Message
    /// </summary>
    DatabaseError = 3
}
