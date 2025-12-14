using System;
using System.Collections.Generic;

namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Filter criteria for querying logs.
/// </summary>
public class Model_DevLogFilter
{
    #region Properties

    /// <summary>
    /// Start date for filtering (inclusive).
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// End date for filtering (inclusive).
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Filter by specific severity levels.
    /// </summary>
    public List<string> Severities { get; set; } = [];

    /// <summary>
    /// Filter by source/module name (supports partial match).
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Search text (applied to Message and Details).
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// Whether SearchText is a regular expression.
    /// </summary>
    public bool IsRegex { get; set; }

    /// <summary>
    /// Filter by specific user.
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Filter by error type (exception class name).
    /// </summary>
    public string? ErrorType { get; set; }

    /// <summary>
    /// Maximum number of entries to return.
    /// </summary>
    public int? MaxResults { get; set; }

    /// <summary>
    /// Offset for pagination (0-based).
    /// </summary>
    public int Skip { get; set; }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a filter for today's entries.
    /// </summary>
    public static Model_DevLogFilter Today() => new()
    {
        DateFrom = DateTime.Today,
        DateTo = DateTime.Today.AddDays(1).AddTicks(-1)
    };

    /// <summary>
    /// Creates a filter for the last 7 days.
    /// </summary>
    public static Model_DevLogFilter Last7Days() => new()
    {
        DateFrom = DateTime.Today.AddDays(-7),
        DateTo = DateTime.Now
    };

    /// <summary>
    /// Creates a filter for the last 30 days.
    /// </summary>
    public static Model_DevLogFilter Last30Days() => new()
    {
        DateFrom = DateTime.Today.AddDays(-30),
        DateTo = DateTime.Now
    };

    /// <summary>
    /// Creates a filter for errors only.
    /// </summary>
    public static Model_DevLogFilter ErrorsOnly() => new()
    {
        Severities = ["Error", "Critical"]
    };

    #endregion
}

/// <summary>
/// Grouping options for log analysis.
/// </summary>
public enum Enum_LogGroupBy
{
    /// <summary>No grouping.</summary>
    None,
    /// <summary>Group by error type.</summary>
    ErrorType,
    /// <summary>Group by source component.</summary>
    Source,
    /// <summary>Group by hour of day.</summary>
    Hour,
    /// <summary>Group by day.</summary>
    Day
}
