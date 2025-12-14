using System;
using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;

namespace MTM_WIP_Application_Winforms.Data;

/// <summary>
/// Interface for Developer Tools data access.
/// </summary>
public interface IDao_DeveloperTools
{
    /// <summary>
    /// Gets log statistics for a specific date range.
    /// </summary>
    /// <param name="dateFrom">Start date.</param>
    /// <param name="dateTo">End date.</param>
    /// <returns>Result containing DataTable with statistics.</returns>
    Task<Model_Dao_Result<DataTable>> GetLogStatisticsAsync(DateTime dateFrom, DateTime dateTo);

    /// <summary>
    /// Gets grouped error data for analysis.
    /// </summary>
    /// <param name="dateFrom">Start date.</param>
    /// <param name="dateTo">End date.</param>
    /// <returns>Result containing DataTable with grouped errors.</returns>
    Task<Model_Dao_Result<DataTable>> GetErrorGroupingsAsync(DateTime dateFrom, DateTime dateTo);

    /// <summary>
    /// Gets log timeline data for charting.
    /// </summary>
    /// <param name="dateFrom">Start date.</param>
    /// <param name="dateTo">End date.</param>
    /// <param name="groupBy">Grouping interval ('Hour' or 'Day').</param>
    /// <returns>Result containing DataTable with timeline data.</returns>
    Task<Model_Dao_Result<DataTable>> GetLogTimelineAsync(DateTime dateFrom, DateTime dateTo, string groupBy);

    /// <summary>
    /// Gets log entries matching the filter.
    /// </summary>
    /// <param name="filter">Filter criteria.</param>
    /// <returns>Result containing DataTable with log entries.</returns>
    Task<Model_Dao_Result<DataTable>> GetLogEntriesAsync(Model_DevLogFilter filter);

    /// <summary>
    /// Gets recent error entries.
    /// </summary>
    /// <param name="count">Number of errors to retrieve.</param>
    /// <returns>Result containing DataTable with recent errors.</returns>
    Task<Model_Dao_Result<DataTable>> GetRecentErrorsAsync(int count);

    /// <summary>
    /// Truncates the log table.
    /// </summary>
    /// <returns>Result indicating success or failure.</returns>
    Task<Model_Dao_Result> TruncateLogsAsync();

    /// <summary>
    /// Inserts a log entry into the database.
    /// </summary>
    /// <param name="entry">The log entry to insert.</param>
    /// <returns>Result indicating success or failure.</returns>
    Task<Model_Dao_Result> InsertLogEntryAsync(Model_DevLogEntry entry);

    /// <summary>
    /// Inserts a batch of log entries into the database using a transaction.
    /// </summary>
    /// <param name="entries">The list of log entries to insert.</param>
    /// <returns>Result indicating success or failure.</returns>
    Task<Model_Dao_Result> InsertLogEntriesBatchAsync(List<Model_DevLogEntry> entries);

    /// <summary>
    /// Gets feedback summary statistics.
    /// </summary>
    /// <returns>Result containing DataTable with feedback summary.</returns>
    Task<Model_Dao_Result<DataTable>> GetFeedbackSummaryAsync();

    /// <summary>
    /// Gets distinct values for a specific log field.
    /// </summary>
    /// <param name="field">Field name (Source, User, ErrorType, Level).</param>
    /// <returns>Result containing DataTable with distinct values.</returns>
    Task<Model_Dao_Result<DataTable>> GetDistinctValuesAsync(string field);

    /// <summary>
    /// Gets database health information (table sizes).
    /// </summary>
    /// <returns>Result containing DataTable with table sizes.</returns>
    Task<Model_Dao_Result<DataTable>> GetDatabaseHealthAsync();

    /// <summary>
    /// Gets database statistics.
    /// </summary>
    /// <returns>Result containing DataTable with database stats.</returns>
    Task<Model_Dao_Result<DataTable>> GetDatabaseStatsAsync();
}
