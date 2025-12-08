namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Defines the severity levels for general application logging.
/// </summary>
public enum Enum_LogLevel
{
    /// <summary>
    /// Detailed debug information.
    /// </summary>
    Debug,

    /// <summary>
    /// General informational messages.
    /// </summary>
    Information,

    /// <summary>
    /// Warning messages for non-critical issues.
    /// </summary>
    Warning,

    /// <summary>
    /// Error messages for failed operations.
    /// </summary>
    Error,

    /// <summary>
    /// Critical errors requiring immediate attention.
    /// </summary>
    Critical
}
