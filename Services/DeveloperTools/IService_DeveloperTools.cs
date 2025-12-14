using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;

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
    /// <returns>Model_Dao_Result containing list of Model_DevLogEntry on success.</returns>
    Task<Model_Dao_Result<List<Model_DevLogEntry>>> GetRecentErrorsAsync(int count = 10);

    /// <summary>
    /// Synchronizes logs from CSV files to the database.
    /// Truncates the database table before inserting.
    /// </summary>
    /// <param name="progress">Optional progress reporter.</param>
    /// <returns>Model_Dao_Result indicating success or failure.</returns>
    Task<Model_Dao_Result> SyncLogsToDatabaseAsync(IProgress<(int current, int total)>? progress = null);

    /// <summary>
    /// Purges all log files from the log directory.
    /// </summary>
    /// <returns>Model_Dao_Result indicating success or failure.</returns>
    Task<Model_Dao_Result> PurgeLogsAsync();

    #endregion

    #region Log Queries

    /// <summary>
    /// Gets log entries matching the specified filter.
    /// </summary>
    /// <param name="filter">Filter criteria for log queries.</param>
    /// <returns>Model_Dao_Result containing list of Model_DevLogEntry on success.</returns>
    Task<Model_Dao_Result<List<Model_DevLogEntry>>> GetLogEntriesAsync(Model_DevLogFilter filter);

    /// <summary>
    /// Gets log entries grouped by the specified criteria.
    /// </summary>
    /// <param name="groupBy">How to group entries (ErrorType, Source, Hour, Day).</param>
    /// <param name="filter">Optional filter to apply before grouping.</param>
    /// <returns>Model_Dao_Result containing list of Model_ErrorGrouping on success.</returns>
    Task<Model_Dao_Result<List<Model_ErrorGrouping>>> GetErrorGroupingsAsync(
        Enum_LogGroupBy groupBy, 
        Model_DevLogFilter? filter = null);

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
    Task<Model_Dao_Result<DataTable>> GetUserFeedbackAsync(string username);

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
        List<Model_DevLogEntry> entries, 
        string filePath, 
        Enum_ExportFormat format);

    #endregion
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
