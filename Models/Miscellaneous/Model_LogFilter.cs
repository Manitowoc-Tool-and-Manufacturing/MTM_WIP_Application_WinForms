namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Represents filter criteria for log entry display and navigation.
/// Supports filtering by date range, log types, severity levels, source component, and text search.
/// </summary>
public class Model_LogFilter
{
    #region Properties

    /// <summary>
    /// Earliest timestamp to include (inclusive). Null means no start limit.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Latest timestamp to include (inclusive). Null means no end limit.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Log format types to include. Empty list means include all types.
    /// </summary>
    public List<Model_LogFormat> LogTypes { get; set; } = new();

    /// <summary>
    /// Severity levels to include (e.g., "INFO", "WARNING", "ERROR").
    /// Empty list means include all severities.
    /// </summary>
    public List<string> SeverityLevels { get; set; } = new();

    /// <summary>
    /// Source component to filter by. Null or empty means include all sources.
    /// </summary>
    public string? SourceComponent { get; set; }

    /// <summary>
    /// Text to search for in message or details fields. Null or empty means no text filter.
    /// Case-insensitive search.
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// Indicates whether any filter criteria are currently active.
    /// Returns true if any property has a non-default value.
    /// </summary>
    public bool HasActiveFilters =>
        StartDate.HasValue ||
        EndDate.HasValue ||
        LogTypes.Count > 0 ||
        SeverityLevels.Count > 0 ||
        !string.IsNullOrWhiteSpace(SourceComponent) ||
        !string.IsNullOrWhiteSpace(SearchText);

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a default filter with no active criteria.
    /// </summary>
    /// <returns>A new Model_LogFilter with all properties at default values.</returns>
    public static Model_LogFilter CreateDefault()
    {
        return new Model_LogFilter();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Resets all filter criteria to default (no filtering).
    /// </summary>
    public void Clear()
    {
        StartDate = null;
        EndDate = null;
        LogTypes.Clear();
        SeverityLevels.Clear();
        SourceComponent = null;
        SearchText = null;
    }

    /// <summary>
    /// Creates a deep copy of this filter instance.
    /// </summary>
    /// <returns>A new Model_LogFilter with the same criteria as this instance.</returns>
    public Model_LogFilter Clone()
    {
        return new Model_LogFilter
        {
            StartDate = StartDate,
            EndDate = EndDate,
            LogTypes = new List<Model_LogFormat>(LogTypes),
            SeverityLevels = new List<string>(SeverityLevels),
            SourceComponent = SourceComponent,
            SearchText = SearchText
        };
    }

    #endregion
}
