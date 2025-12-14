using System;

namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// System health status for user display.
/// </summary>
public class Model_SystemHealthStatus
{
    #region Properties

    /// <summary>
    /// Overall health indicator.
    /// </summary>
    public Enum_HealthIndicator Status { get; set; } = Enum_HealthIndicator.Green;

    /// <summary>
    /// User-friendly status message.
    /// </summary>
    public string Message { get; set; } = "System Operating Normally";

    /// <summary>
    /// Timestamp when health was last evaluated.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;

    /// <summary>
    /// Number of errors in the evaluation period.
    /// </summary>
    public int ErrorCount { get; set; }

    /// <summary>
    /// Number of warnings in the evaluation period.
    /// </summary>
    public int WarningCount { get; set; }

    /// <summary>
    /// Timestamp of last error (if any).
    /// </summary>
    public DateTime? LastErrorTime { get; set; }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a health status based on error count.
    /// </summary>
    /// <param name="errorCount">Number of errors in last 24 hours.</param>
    /// <param name="lastErrorTime">Time of last error.</param>
    public static Model_SystemHealthStatus FromErrorCount(int errorCount, DateTime? lastErrorTime = null)
    {
        return errorCount switch
        {
            0 => new Model_SystemHealthStatus
            {
                Status = Enum_HealthIndicator.Green,
                Message = "System Operating Normally",
                ErrorCount = 0
            },
            >= 1 and <= 5 => new Model_SystemHealthStatus
            {
                Status = Enum_HealthIndicator.Yellow,
                Message = $"Minor Issues Detected ({errorCount} errors in last 24 hours)",
                ErrorCount = errorCount,
                LastErrorTime = lastErrorTime
            },
            _ => new Model_SystemHealthStatus
            {
                Status = Enum_HealthIndicator.Red,
                Message = $"System Experiencing Issues ({errorCount} errors in last 24 hours)",
                ErrorCount = errorCount,
                LastErrorTime = lastErrorTime
            }
        };
    }

    #endregion
}

/// <summary>
/// Health indicator colors.
/// </summary>
public enum Enum_HealthIndicator
{
    /// <summary>No errors - system healthy.</summary>
    Green,
    /// <summary>Minor issues - 1-5 errors.</summary>
    Yellow,
    /// <summary>Significant issues - 6+ errors.</summary>
    Red
}
