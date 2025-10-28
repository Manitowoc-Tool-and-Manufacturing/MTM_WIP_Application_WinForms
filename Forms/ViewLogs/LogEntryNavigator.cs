using MTM_Inventory_Application.Models;
using System.Text.RegularExpressions;

namespace MTM_Inventory_Application.Forms.ViewLogs;

/// <summary>
/// Manages navigation and filtering of log entries with indexed access to filtered results.
/// Maintains separation between all entries and filtered view for efficient navigation.
/// </summary>
public class LogEntryNavigator
{
    #region Fields

    private List<Model_LogEntry> _allEntries = new();
    private List<int> _filteredIndices = new();
    private int _currentFilteredIndex = 0;
    private Model_LogFilter _activeFilter = Model_LogFilter.CreateDefault();

    // Regex timeout to prevent ReDoS attacks
    private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(100);

    #endregion

    #region Properties

    /// <summary>
    /// Gets the total count of all entries before filtering.
    /// </summary>
    public int TotalCount => _allEntries.Count;

    /// <summary>
    /// Gets the count of entries matching the current filter.
    /// </summary>
    public int FilteredCount => _filteredIndices.Count;

    /// <summary>
    /// Gets the current position in the filtered results (1-based).
    /// </summary>
    public int CurrentPosition => FilteredCount > 0 ? _currentFilteredIndex + 1 : 0;

    /// <summary>
    /// Gets whether there is a previous entry available.
    /// </summary>
    public bool HasPrevious => _currentFilteredIndex > 0;

    /// <summary>
    /// Gets whether there is a next entry available.
    /// </summary>
    public bool HasNext => _currentFilteredIndex < FilteredCount - 1;

    /// <summary>
    /// Gets the currently active filter.
    /// </summary>
    public Model_LogFilter ActiveFilter => _activeFilter;

    #endregion

    #region Initialization

    /// <summary>
    /// Loads entries and applies the default filter (no filtering).
    /// </summary>
    /// <param name="entries">List of log entries to manage.</param>
    public void LoadEntries(List<Model_LogEntry> entries)
    {
        _allEntries = entries ?? new List<Model_LogEntry>();
        _currentFilteredIndex = 0;
        ApplyFilter(Model_LogFilter.CreateDefault());
    }

    #endregion

    #region Navigation

    /// <summary>
    /// Gets the current log entry, or null if no entries or out of bounds.
    /// </summary>
    public Model_LogEntry? GetCurrentEntry()
    {
        if (FilteredCount == 0 || _currentFilteredIndex < 0 || _currentFilteredIndex >= FilteredCount)
        {
            return null;
        }

        int originalIndex = _filteredIndices[_currentFilteredIndex];
        return _allEntries[originalIndex];
    }

    /// <summary>
    /// Moves to the previous entry in the filtered list.
    /// </summary>
    /// <returns>True if moved successfully, false if already at first entry.</returns>
    public bool MovePrevious()
    {
        if (!HasPrevious)
        {
            return false;
        }

        _currentFilteredIndex--;
        return true;
    }

    /// <summary>
    /// Moves to the next entry in the filtered list.
    /// </summary>
    /// <returns>True if moved successfully, false if already at last entry.</returns>
    public bool MoveNext()
    {
        if (!HasNext)
        {
            return false;
        }

        _currentFilteredIndex++;
        return true;
    }

    /// <summary>
    /// Moves to the first entry in the filtered list.
    /// </summary>
    public void MoveToFirst()
    {
        _currentFilteredIndex = 0;
    }

    /// <summary>
    /// Moves to the last entry in the filtered list.
    /// </summary>
    public void MoveToLast()
    {
        if (FilteredCount > 0)
        {
            _currentFilteredIndex = FilteredCount - 1;
        }
    }

    /// <summary>
    /// Moves to a specific position in the filtered list (1-based).
    /// </summary>
    /// <param name="position">Position to move to (1 to FilteredCount).</param>
    /// <returns>True if moved successfully, false if position out of range.</returns>
    public bool MoveToPosition(int position)
    {
        if (position < 1 || position > FilteredCount)
        {
            return false;
        }

        _currentFilteredIndex = position - 1;
        return true;
    }

    #endregion

    #region Filtering

    /// <summary>
    /// Applies a filter to the entries and rebuilds the filtered index list.
    /// Resets navigation to the first filtered entry.
    /// </summary>
    /// <param name="filter">Filter to apply.</param>
    public void ApplyFilter(Model_LogFilter filter)
    {
        _activeFilter = filter ?? Model_LogFilter.CreateDefault();
        _filteredIndices.Clear();
        _currentFilteredIndex = 0;

        // If no active filters, include all entries
        if (!_activeFilter.HasActiveFilters)
        {
            for (int i = 0; i < _allEntries.Count; i++)
            {
                _filteredIndices.Add(i);
            }
            return;
        }

        // Apply filters using LINQ
        var query = _allEntries.AsEnumerable();

        // Date range filter
        if (_activeFilter.StartDate.HasValue)
        {
            query = query.Where(e => e.Timestamp >= _activeFilter.StartDate.Value);
        }

        if (_activeFilter.EndDate.HasValue)
        {
            // Include entire end date (through 23:59:59.999)
            DateTime endOfDay = _activeFilter.EndDate.Value.Date.AddDays(1).AddMilliseconds(-1);
            query = query.Where(e => e.Timestamp <= endOfDay);
        }

        // Log type filter
        if (_activeFilter.LogTypes != null && _activeFilter.LogTypes.Count > 0)
        {
            query = query.Where(e => _activeFilter.LogTypes.Contains(e.LogType));
        }

        // Severity filter (only applies to Normal and DatabaseError logs)
        if (_activeFilter.SeverityLevels != null && _activeFilter.SeverityLevels.Count > 0)
        {
            query = query.Where(e =>
            {
                // Normal logs have Level property
                if (e.LogType == LogFormat.Normal && !string.IsNullOrEmpty(e.Level))
                {
                    return _activeFilter.SeverityLevels.Contains(e.Level, StringComparer.OrdinalIgnoreCase);
                }

                // DatabaseError logs have Severity property
                if (e.LogType == LogFormat.DatabaseError && !string.IsNullOrEmpty(e.Severity))
                {
                    return _activeFilter.SeverityLevels.Contains(e.Severity, StringComparer.OrdinalIgnoreCase);
                }

                // ApplicationError logs don't have configurable severity - exclude from severity filtering
                return e.LogType == LogFormat.ApplicationError;
            });
        }

        // Source component filter
        if (!string.IsNullOrWhiteSpace(_activeFilter.SourceComponent))
        {
            string sourceFilter = _activeFilter.SourceComponent.Trim();
            query = query.Where(e => !string.IsNullOrEmpty(e.Source) &&
                                    e.Source.Contains(sourceFilter, StringComparison.OrdinalIgnoreCase));
        }

        // Search text filter (searches across relevant fields)
        if (!string.IsNullOrWhiteSpace(_activeFilter.SearchText))
        {
            string searchText = _activeFilter.SearchText.Trim();

            // Try to compile regex with timeout for security
            Regex? searchRegex = null;
            try
            {
                searchRegex = new Regex(Regex.Escape(searchText), RegexOptions.IgnoreCase, RegexTimeout);
            }
            catch (ArgumentException)
            {
                // Invalid regex pattern - fall back to simple contains
                searchRegex = null;
            }

            query = query.Where(e =>
            {
                try
                {
                    if (searchRegex != null)
                    {
                        // Use regex matching with timeout protection
                        return (!string.IsNullOrEmpty(e.Message) && searchRegex.IsMatch(e.Message)) ||
                               (!string.IsNullOrEmpty(e.Details) && searchRegex.IsMatch(e.Details)) ||
                               (!string.IsNullOrEmpty(e.Source) && searchRegex.IsMatch(e.Source)) ||
                               (!string.IsNullOrEmpty(e.ErrorType) && searchRegex.IsMatch(e.ErrorType)) ||
                               (!string.IsNullOrEmpty(e.StackTrace) && searchRegex.IsMatch(e.StackTrace));
                    }
                    else
                    {
                        // Fall back to simple contains
                        return (!string.IsNullOrEmpty(e.Message) && e.Message.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                               (!string.IsNullOrEmpty(e.Details) && e.Details.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                               (!string.IsNullOrEmpty(e.Source) && e.Source.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                               (!string.IsNullOrEmpty(e.ErrorType) && e.ErrorType.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||
                               (!string.IsNullOrEmpty(e.StackTrace) && e.StackTrace.Contains(searchText, StringComparison.OrdinalIgnoreCase));
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    // Regex timeout - skip this entry to prevent DoS
                    return false;
                }
            });
        }

        // Execute query and build filtered index list
        var filteredEntries = query.ToList();
        foreach (var entry in filteredEntries)
        {
            int originalIndex = _allEntries.IndexOf(entry);
            if (originalIndex >= 0)
            {
                _filteredIndices.Add(originalIndex);
            }
        }
    }

    /// <summary>
    /// Clears all filters and shows all entries.
    /// </summary>
    public void ClearFilters()
    {
        ApplyFilter(Model_LogFilter.CreateDefault());
    }

    #endregion
}
