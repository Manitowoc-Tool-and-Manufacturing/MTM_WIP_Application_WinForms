using System;

namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Aggregated log statistics for dashboard display.
/// </summary>
public class Model_LogStatistics
{
    #region Properties

    /// <summary>
    /// Start of the statistics period.
    /// </summary>
    public DateTime PeriodStart { get; set; }

    /// <summary>
    /// End of the statistics period.
    /// </summary>
    public DateTime PeriodEnd { get; set; }

    /// <summary>
    /// Total number of log entries in the period.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Number of Critical entries.
    /// </summary>
    public int CriticalCount { get; set; }

    /// <summary>
    /// Number of Error entries.
    /// </summary>
    public int ErrorCount { get; set; }

    /// <summary>
    /// Number of Warning entries.
    /// </summary>
    public int WarningCount { get; set; }

    /// <summary>
    /// Number of Information entries.
    /// </summary>
    public int InfoCount { get; set; }

    /// <summary>
    /// Timestamp of the most recent error.
    /// </summary>
    public DateTime? LastErrorTime { get; set; }

    /// <summary>
    /// Message of the most recent error.
    /// </summary>
    public string? LastErrorMessage { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Combined error and critical count.
    /// </summary>
    public int ErrorsAndCritical => ErrorCount + CriticalCount;

    /// <summary>
    /// Percentage of entries that are errors/critical.
    /// </summary>
    public double ErrorPercentage => TotalCount > 0 
        ? Math.Round((double)ErrorsAndCritical / TotalCount * 100, 1) 
        : 0;

    #endregion
}
