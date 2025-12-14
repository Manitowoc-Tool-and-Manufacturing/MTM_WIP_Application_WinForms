using System;
using System.Collections.Generic;

namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Represents a group of similar errors.
/// </summary>
public class Model_ErrorGrouping
{
    #region Properties

    /// <summary>
    /// Unique key for the group (e.g., ErrorType_MethodName).
    /// </summary>
    public string GroupKey { get; set; } = string.Empty;

    /// <summary>
    /// Error type for the group.
    /// </summary>
    public string ErrorType { get; set; } = string.Empty;

    /// <summary>
    /// Method/source where the error occurs.
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Representative error message (from first occurrence).
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Number of occurrences in this group.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// First occurrence timestamp.
    /// </summary>
    public DateTime FirstOccurrence { get; set; }

    /// <summary>
    /// Last occurrence timestamp.
    /// </summary>
    public DateTime LastOccurrence { get; set; }

    /// <summary>
    /// List of unique users who encountered this error.
    /// </summary>
    public List<string> AffectedUsers { get; set; } = [];

    #endregion

    #region Computed Properties

    /// <summary>
    /// Duration between first and last occurrence.
    /// </summary>
    public TimeSpan Duration => LastOccurrence - FirstOccurrence;

    /// <summary>
    /// Average occurrences per day.
    /// </summary>
    public double OccurrencesPerDay => Duration.TotalDays > 0 
        ? Math.Round(Count / Duration.TotalDays, 2) 
        : Count;

    #endregion
}
