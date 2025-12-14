using System;

namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Represents a single log entry from CSV files or database.
/// </summary>
public class Model_DevLogEntry
{
    #region Properties

    /// <summary>
    /// Unique identifier (database ID or file line number).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Timestamp when the log entry was created.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Log level: Information, Warning, Error, Critical.
    /// </summary>
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Source component (module, class, or method name).
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Log message content.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Additional details (stack trace, context data).
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// User who triggered the log entry (if applicable).
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Error type for exceptions (e.g., MySqlException, NullReferenceException).
    /// </summary>
    public string? ErrorType { get; set; }

    /// <summary>
    /// Stack trace for error entries.
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// Machine name where the log originated.
    /// </summary>
    public string? MachineName { get; set; }

    /// <summary>
    /// Application version at time of logging.
    /// </summary>
    public string? AppVersion { get; set; }

    /// <summary>
    /// Source of the log entry (File or Database).
    /// </summary>
    public Enum_LogSource LogSource { get; set; } = Enum_LogSource.File;

    /// <summary>
    /// Original file path (for CSV-sourced entries).
    /// </summary>
    public string? SourceFilePath { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Returns true if this is an error or critical entry.
    /// </summary>
    public bool IsError => Level.Equals("Error", StringComparison.OrdinalIgnoreCase) ||
                           Level.Equals("Critical", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Returns the severity emoji for display.
    /// </summary>
    public string SeverityEmoji => Level.ToUpperInvariant() switch
    {
        "CRITICAL" => "🔴",
        "ERROR" => "🔴",
        "WARNING" => "⚠️",
        "INFORMATION" => "ℹ️",
        "INFO" => "ℹ️",
        _ => "📝"
    };

    #endregion
}

/// <summary>
/// Source of a log entry.
/// </summary>
public enum Enum_LogSource
{
    /// <summary>Log entry read from CSV file.</summary>
    File,
    /// <summary>Log entry read from database.</summary>
    Database
}
