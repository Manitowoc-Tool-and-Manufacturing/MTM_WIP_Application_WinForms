# Interface Contract: IService_DeveloperTools

**Feature**: 006-dev-tools-consolidation  
**Date**: 2025-12-13  
**Status**: Draft

---

## Overview

`IService_DeveloperTools` provides analytical and diagnostic data access for the Developer Tools form. It aggregates data from CSV log files, the `log_error` database table, and feedback tables to provide dashboard statistics, filtered log queries, and error groupings.

## Interface Definition

```csharp
namespace MTM_WIP_Application_Winforms.Services.DeveloperTools;

/// <summary>
/// Service interface for Developer Tools analytics and diagnostics.
/// Provides methods to query logs, compute statistics, and retrieve system health information.
/// </summary>
public interface IService_DeveloperTools
{
    #region Dashboard Statistics

    /// <summary>
    /// Gets aggregated log statistics for a date range.
    /// </summary>
    /// <param name="start">Start date (inclusive).</param>
    /// <param name="end">End date (inclusive).</param>
    /// <returns>Model_Dao_Result containing Model_LogStatistics on success.</returns>
    Task<Model_Dao_Result<Model_LogStatistics>> GetLogStatisticsAsync(DateTime start, DateTime end);

    /// <summary>
    /// Gets feedback summary statistics.
    /// </summary>
    /// <returns>Model_Dao_Result containing Model_FeedbackSummary on success.</returns>
    Task<Model_Dao_Result<Model_FeedbackSummary>> GetFeedbackSummaryAsync();

    /// <summary>
    /// Gets the most recent error entries.
    /// </summary>
    /// <param name="count">Maximum number of entries to return.</param>
    /// <returns>Model_Dao_Result containing list of Model_LogEntry on success.</returns>
    Task<Model_Dao_Result<List<Model_LogEntry>>> GetRecentErrorsAsync(int count = 10);

    #endregion

    #region Log Queries

    /// <summary>
    /// Gets log entries matching the specified filter.
    /// </summary>
    /// <param name="filter">Filter criteria for log queries.</param>
    /// <returns>Model_Dao_Result containing list of Model_LogEntry on success.</returns>
    Task<Model_Dao_Result<List<Model_LogEntry>>> GetLogEntriesAsync(Model_LogFilter filter);

    /// <summary>
    /// Gets log entries grouped by the specified criteria.
    /// </summary>
    /// <param name="groupBy">How to group entries (ErrorType, Source, Hour, Day).</param>
    /// <param name="filter">Optional filter to apply before grouping.</param>
    /// <returns>Model_Dao_Result containing list of Model_ErrorGrouping on success.</returns>
    Task<Model_Dao_Result<List<Model_ErrorGrouping>>> GetErrorGroupingsAsync(
        Enum_LogGroupBy groupBy, 
        Model_LogFilter? filter = null);

    /// <summary>
    /// Gets timeline data for charting (hourly or daily counts).
    /// </summary>
    /// <param name="start">Start date.</param>
    /// <param name="end">End date.</param>
    /// <param name="granularity">Granularity (Hour or Day).</param>
    /// <returns>Model_Dao_Result containing dictionary of timestamp to counts.</returns>
    Task<Model_Dao_Result<Dictionary<DateTime, Model_LogStatistics>>> GetLogTimelineAsync(
        DateTime start, 
        DateTime end, 
        Enum_TimelineGranularity granularity = Enum_TimelineGranularity.Day);

    /// <summary>
    /// Gets distinct values for a log field (for filter dropdowns).
    /// </summary>
    /// <param name="field">Field name (Source, User, ErrorType).</param>
    /// <returns>Model_Dao_Result containing list of distinct values.</returns>
    Task<Model_Dao_Result<List<string>>> GetDistinctValuesAsync(string field);

    #endregion

    #region System Health

    /// <summary>
    /// Gets current system health status based on recent error counts.
    /// </summary>
    /// <returns>Model_Dao_Result containing Model_SystemHealthStatus on success.</returns>
    Task<Model_Dao_Result<Model_SystemHealthStatus>> GetSystemHealthAsync();

    /// <summary>
    /// Gets database health information.
    /// </summary>
    /// <returns>Model_Dao_Result containing Model_DatabaseHealth on success.</returns>
    Task<Model_Dao_Result<Model_DatabaseHealth>> GetDatabaseHealthAsync();

    /// <summary>
    /// Gets current performance metrics.
    /// </summary>
    /// <returns>Model_PerformanceMetrics with current values.</returns>
    Model_PerformanceMetrics GetPerformanceMetrics();

    #endregion

    #region User Feedback (for System Health form)

    /// <summary>
    /// Gets feedback submitted by a specific user.
    /// </summary>
    /// <param name="userId">User ID to filter by.</param>
    /// <returns>Model_Dao_Result containing DataTable of user's feedback.</returns>
    Task<Model_Dao_Result<DataTable>> GetUserFeedbackAsync(int userId);

    #endregion

    #region Export

    /// <summary>
    /// Exports log entries to a file.
    /// </summary>
    /// <param name="entries">Log entries to export.</param>
    /// <param name="filePath">Destination file path.</param>
    /// <param name="format">Export format (CSV, JSON, TXT).</param>
    /// <returns>Model_Dao_Result with success/failure status.</returns>
    Task<Model_Dao_Result<bool>> ExportLogsAsync(
        List<Model_LogEntry> entries, 
        string filePath, 
        Enum_ExportFormat format);

    #endregion
}

/// <summary>
/// How to group log entries.
/// </summary>
public enum Enum_LogGroupBy
{
    /// <summary>No grouping.</summary>
    None,
    /// <summary>Group by error type (exception class).</summary>
    ErrorType,
    /// <summary>Group by source module.</summary>
    Source,
    /// <summary>Group by hour of day.</summary>
    Hour,
    /// <summary>Group by calendar day.</summary>
    Day
}

/// <summary>
/// Timeline granularity for charts.
/// </summary>
public enum Enum_TimelineGranularity
{
    /// <summary>Hourly data points.</summary>
    Hour,
    /// <summary>Daily data points.</summary>
    Day
}

/// <summary>
/// Export file format.
/// </summary>
public enum Enum_ExportFormat
{
    /// <summary>Comma-separated values.</summary>
    CSV,
    /// <summary>JSON format.</summary>
    JSON,
    /// <summary>Plain text.</summary>
    TXT
}
```

## Implementation Notes

### Service Registration

```csharp
// In Service_OnStartup_DependencyInjection.ConfigureServices()
services.AddTransient<IService_DeveloperTools, Service_DeveloperTools>();
```

### Dependencies

```csharp
public class Service_DeveloperTools : IService_DeveloperTools
{
    private readonly ILoggingService _logger;
    private readonly IDao_DeveloperTools _dao;
    private readonly IService_FeedbackManager _feedbackManager;
    
    public Service_DeveloperTools(
        ILoggingService logger,
        IDao_DeveloperTools dao,
        IService_FeedbackManager feedbackManager)
    {
        _logger = logger;
        _dao = dao;
        _feedbackManager = feedbackManager;
    }
}
```

### Data Sources

| Method | Data Source |
|--------|-------------|
| `GetLogStatisticsAsync` | CSV files + `log_error` table |
| `GetFeedbackSummaryAsync` | `UserFeedback` table via `_feedbackManager` |
| `GetRecentErrorsAsync` | `log_error` table |
| `GetLogEntriesAsync` | CSV files (primary) |
| `GetErrorGroupingsAsync` | `log_error` table |
| `GetLogTimelineAsync` | `log_error` table |
| `GetSystemHealthAsync` | `log_error` table |
| `GetDatabaseHealthAsync` | `Helper_Database_StoredProcedure` diagnostics |
| `GetPerformanceMetrics` | .NET `Process` and `GC` classes |
| `GetUserFeedbackAsync` | `UserFeedback` table via `_feedbackManager` |

### CSV File Parsing

The service will include a private helper for CSV parsing:

```csharp
private async Task<List<Model_LogEntry>> ParseCsvLogsAsync(string directory, Model_LogFilter filter)
{
    var entries = new List<Model_LogEntry>();
    var files = Directory.GetFiles(directory, "*.csv", SearchOption.AllDirectories);
    
    foreach (var file in files)
    {
        // Skip files outside date range based on filename
        // Parse CSV with streaming for large files
        // Apply filter criteria
        // Add to results
    }
    
    return entries;
}
```

## Usage Examples

### Dashboard Statistics

```csharp
public async Task LoadDashboardAsync()
{
    var start = DateTime.Today.AddDays(-7);
    var end = DateTime.Now;
    
    var statsResult = await _devToolsService.GetLogStatisticsAsync(start, end);
    if (statsResult.IsSuccess)
    {
        lblErrorCount.Text = statsResult.Data.ErrorCount.ToString();
        lblWarningCount.Text = statsResult.Data.WarningCount.ToString();
    }
    
    var feedbackResult = await _devToolsService.GetFeedbackSummaryAsync();
    if (feedbackResult.IsSuccess)
    {
        lblFeedbackCount.Text = feedbackResult.Data.OpenCount.ToString();
    }
}
```

### Filtered Log Query

```csharp
public async Task SearchLogsAsync()
{
    var filter = new Model_LogFilter
    {
        DateFrom = dtpFrom.Value,
        DateTo = dtpTo.Value,
        Severities = ["Error", "Critical"],
        SearchText = txtSearch.Text,
        IsRegex = chkRegex.Checked,
        MaxResults = 1000
    };
    
    var result = await _devToolsService.GetLogEntriesAsync(filter);
    if (result.IsSuccess)
    {
        dgvLogs.DataSource = result.Data;
    }
    else
    {
        _errorHandler.ShowUserError(result.ErrorMessage);
    }
}
```

### Error Grouping

```csharp
public async Task ShowGroupedErrorsAsync()
{
    var result = await _devToolsService.GetErrorGroupingsAsync(
        Enum_LogGroupBy.ErrorType,
        Model_LogFilter.Last7Days());
    
    if (result.IsSuccess)
    {
        foreach (var group in result.Data.OrderByDescending(g => g.Count))
        {
            // Display grouped errors
        }
    }
}
```
