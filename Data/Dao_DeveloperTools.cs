using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;

namespace MTM_WIP_Application_Winforms.Data;

/// <summary>
/// Data Access Object for Developer Tools.
/// </summary>
public class Dao_DeveloperTools : IDao_DeveloperTools
{
    #region Methods

    /// <summary>
    /// Gets log statistics for a specific date range.
    /// </summary>
    /// <param name="dateFrom">Start date.</param>
    /// <param name="dateTo">End date.</param>
    /// <returns>Result containing DataTable with statistics.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetLogStatisticsAsync(DateTime dateFrom, DateTime dateTo)
    {
        var parameters = new Dictionary<string, object>
        {
            { "DateFrom", dateFrom },
            { "DateTo", dateTo }
        };

        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_devtools_GetLogStatistics",
            parameters);
    }

    /// <summary>
    /// Gets grouped error data for analysis.
    /// </summary>
    /// <param name="dateFrom">Start date.</param>
    /// <param name="dateTo">End date.</param>
    /// <returns>Result containing DataTable with grouped errors.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetErrorGroupingsAsync(DateTime dateFrom, DateTime dateTo)
    {
        var parameters = new Dictionary<string, object>
        {
            { "DateFrom", dateFrom },
            { "DateTo", dateTo }
        };

        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_devtools_GetErrorGroupings",
            parameters);
    }

    /// <summary>
    /// Gets log timeline data for charting.
    /// </summary>
    /// <param name="dateFrom">Start date.</param>
    /// <param name="dateTo">End date.</param>
    /// <param name="groupBy">Grouping interval ('Hour' or 'Day').</param>
    /// <returns>Result containing DataTable with timeline data.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetLogTimelineAsync(DateTime dateFrom, DateTime dateTo, string groupBy)
    {
        var parameters = new Dictionary<string, object>
        {
            { "DateFrom", dateFrom },
            { "DateTo", dateTo },
            { "GroupBy", groupBy }
        };

        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_devtools_GetLogTimeline",
            parameters);
    }

    /// <summary>
    /// Gets log entries matching the filter.
    /// </summary>
    /// <param name="filter">Filter criteria.</param>
    /// <returns>Result containing DataTable with log entries.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetLogEntriesAsync(Model_DevLogFilter filter)
    {
        var parameters = new Dictionary<string, object>
        {
            { "DateFrom", filter.DateFrom ?? new DateTime(1753, 1, 1) },
            { "DateTo", filter.DateTo ?? new DateTime(2100, 1, 1) },
            { "Severities", string.Join(",", filter.Severities) },
            { "Source", filter.Source ?? "" },
            { "SearchText", filter.SearchText ?? "" },
            { "User", filter.User ?? "" },
            { "ErrorType", filter.ErrorType ?? "" },
            { "MaxResults", filter.MaxResults ?? 100 },
            { "Skip", filter.Skip }
        };

        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_devtools_GetLogEntries",
            parameters);
    }

    /// <summary>
    /// Gets recent error entries.
    /// </summary>
    /// <param name="count">Number of errors to retrieve.</param>
    /// <returns>Result containing DataTable with recent errors.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetRecentErrorsAsync(int count)
    {
        var parameters = new Dictionary<string, object>
        {
            { "DateFrom", new DateTime(1753, 1, 1) },
            { "DateTo", new DateTime(2100, 1, 1) },
            { "Severities", "Error,Critical" },
            { "Source", "" },
            { "SearchText", "" },
            { "User", "" },
            { "ErrorType", "" },
            { "MaxResults", count },
            { "Skip", 0 }
        };

        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_devtools_GetLogEntries",
            parameters);
    }

    /// <summary>
    /// Gets feedback summary statistics.
    /// </summary>
    /// <returns>Result containing DataTable with feedback summary.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetFeedbackSummaryAsync()
    {
        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_devtools_GetFeedbackSummary",
            null);
    }

    /// <summary>
    /// Gets distinct values for a specific log field.
    /// </summary>
    /// <param name="field">Field name (Source, User, ErrorType, Level).</param>
    /// <returns>Result containing DataTable with distinct values.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetDistinctValuesAsync(string field)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Field", field }
        };

        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_devtools_GetDistinctLogValues",
            parameters);
    }

    /// <summary>
    /// Gets database health information (table sizes).
    /// </summary>
    /// <returns>Result containing DataTable with table sizes.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetDatabaseHealthAsync()
    {
        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_system_GetTableSizes",
            null);
    }

    /// <summary>
    /// Gets database statistics.
    /// </summary>
    /// <returns>Result containing DataTable with database stats.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetDatabaseStatsAsync()
    {
        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_devtools_GetDatabaseStats",
            null);
    }

    #endregion
}
