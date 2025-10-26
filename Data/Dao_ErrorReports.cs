using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MTM_WIP_Application_WinForms.Models;

namespace MTM_Inventory_Application.Data;

/// <summary>
/// Data Access Object for error_reports table operations.
/// Provides methods for inserting and managing error reports.
/// </summary>
internal static class Dao_ErrorReports
{
    #region Fields

    // No instance fields needed for static class

    #endregion

    #region Database Operations

    /// <summary>
    /// Inserts a new error report into the database using sp_error_reports_Insert stored procedure.
    /// </summary>
    /// <param name="report">The error report to insert. UserName is required.</param>
    /// <returns>
    /// DaoResult containing the generated ReportID on success, or error information on failure.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when report is null.</exception>
    /// <remarks>
    /// This method calls the sp_error_reports_Insert stored procedure which:
    /// - Validates that UserName is provided
    /// - Sets ReportDate to current timestamp
    /// - Generates a unique ReportID
    /// - Sets Status to 'New' by default
    /// 
    /// The stored procedure uses transactions to ensure atomicity.
    /// On success, the ReportID is extracted from output parameters.
    /// </remarks>
    public static async Task<DaoResult<int>> InsertReportAsync(Model_ErrorReport report)
    {
        ArgumentNullException.ThrowIfNull(report);

        try
        {
            // Get connection string
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            // Prepare parameters (no p_ prefix - helper adds it)
            var parameters = new Dictionary<string, object>
            {
                ["UserName"] = report.UserName ?? string.Empty,
                ["MachineName"] = report.MachineName ?? (object)DBNull.Value,
                ["AppVersion"] = report.AppVersion ?? (object)DBNull.Value,
                ["ErrorType"] = report.ErrorType ?? (object)DBNull.Value,
                ["ErrorSummary"] = report.ErrorSummary ?? (object)DBNull.Value,
                ["UserNotes"] = report.UserNotes ?? (object)DBNull.Value,
                ["TechnicalDetails"] = report.TechnicalDetails ?? (object)DBNull.Value,
                ["CallStack"] = report.CallStack ?? (object)DBNull.Value
            };

            // Execute stored procedure
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "sp_error_reports_Insert",
                parameters,
                progressHelper: null);

            if (result.IsSuccess)
            {
                // The stored procedure sets OUTPUT parameters @p_Status, @p_ErrorMsg, @p_ReportID
                // but we need to extract ReportID from a SELECT statement or use LAST_INSERT_ID()
                // For now, since the stored procedure returns the ReportID via OUT parameter,
                // we'll query the last insert ID from the result set or StatusMessage
                
                // Check if we have a DataTable with results (some SPs return SELECT results)
                int reportID = 0;
                if (result.Data != null && result.Data.Rows.Count > 0 && result.Data.Columns.Contains("ReportID"))
                {
                    reportID = Convert.ToInt32(result.Data.Rows[0]["ReportID"]);
                }
                
                if (reportID > 0)
                {
                    LoggingUtility.LogApplicationInfo(
                        $"[Dao_ErrorReports] Successfully inserted error report. ReportID: {reportID}, User: {report.UserName}");
                    
                    return DaoResult<int>.Success(
                        reportID, 
                        $"Error report submitted successfully. Report ID: {reportID}");
                }
                else
                {
                    // Success but no ReportID returned - log and return generic success
                    LoggingUtility.Log(
                        "[Dao_ErrorReports] Warning: Error report inserted but ReportID not returned from stored procedure");
                    
                    return DaoResult<int>.Success(
                        0,
                        "Error report submitted successfully.");
                }
            }
            else
            {
                // Stored procedure returned failure status
                if (result.Exception != null)
                {
                    LoggingUtility.LogApplicationError(result.Exception);
                }
                else
                {
                    LoggingUtility.Log($"[Dao_ErrorReports] Failed to insert error report: {result.StatusMessage}");
                }
                
                return DaoResult<int>.Failure(
                    result.StatusMessage ?? "Failed to submit error report.");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            
            return DaoResult<int>.Failure(
                "An unexpected error occurred while submitting the error report.");
        }
    }

    /// <summary>
    /// Retrieves error reports using the sp_error_reports_GetAll stored procedure with optional filters.
    /// </summary>
    /// <param name="filter">Filter criteria; pass null to retrieve all error reports.</param>
    /// <param name="progressHelper">Optional progress helper for long-running operations.</param>
    /// <returns>A DaoResult containing a DataTable of error reports when successful.</returns>
    public static async Task<DaoResult<DataTable>> GetAllErrorReportsAsync(
        Model_ErrorReportFilter? filter,
        Helper_StoredProcedureProgress? progressHelper = null)
    {
        filter ??= new Model_ErrorReportFilter();

        if (!filter.TryValidate(out var validationMessage))
        {
            LoggingUtility.Log(
                "[Dao_ErrorReports] Invalid filter supplied for GetAllErrorReportsAsync: " + validationMessage);

            return DaoResult<DataTable>.Failure(
                validationMessage ?? "Invalid filter values supplied for error report retrieval.");
        }

        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            var parameters = BuildFilterParameters(filter);

            var storedProcedureResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "sp_error_reports_GetAll",
                parameters,
                progressHelper);

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? "Failed to retrieve error reports.";

                LoggingUtility.Log(
                    "[Dao_ErrorReports] Stored procedure sp_error_reports_GetAll failed: " + errorMessage);

                return DaoResult<DataTable>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            var rowCount = storedProcedureResult.Data?.Rows.Count ?? 0;
            LoggingUtility.LogApplicationInfo(
                $"[Dao_ErrorReports] Retrieved {rowCount} error report records from database.");

            return storedProcedureResult;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<DataTable>.Failure(
                "An unexpected error occurred while retrieving error reports.",
                ex);
        }
    }

    /// <summary>
    /// Retrieves a single error report by ReportID using sp_error_reports_GetByID.
    /// </summary>
    /// <param name="reportId">The report identifier to retrieve.</param>
    /// <param name="progressHelper">Optional progress helper for UI updates.</param>
    /// <returns>DaoResult containing the populated Model_ErrorReport on success.</returns>
    public static async Task<DaoResult<Model_ErrorReport>> GetErrorReportByIdAsync(
        int reportId,
        Helper_StoredProcedureProgress? progressHelper = null)
    {
        if (reportId <= 0)
        {
            return DaoResult<Model_ErrorReport>.Failure("Report ID must be greater than zero.");
        }

        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            var parameters = new Dictionary<string, object>
            {
                ["ReportID"] = reportId
            };

            var storedProcedureResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "sp_error_reports_GetByID",
                parameters,
                progressHelper);

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? $"Failed to retrieve error report {reportId}.";

                LoggingUtility.Log(
                    $"[Dao_ErrorReports] Stored procedure sp_error_reports_GetByID failed for ReportID {reportId}: {errorMessage}");

                return DaoResult<Model_ErrorReport>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            if (storedProcedureResult.Data == null || storedProcedureResult.Data.Rows.Count == 0)
            {
                return DaoResult<Model_ErrorReport>.Failure(
                    $"Error report {reportId} was not found in the database.");
            }

            var report = MapToErrorReport(storedProcedureResult.Data.Rows[0]);

            LoggingUtility.LogApplicationInfo(
                $"[Dao_ErrorReports] Retrieved detail for error report {reportId}.");

            return DaoResult<Model_ErrorReport>.Success(
                report,
                storedProcedureResult.StatusMessage ?? "Error report retrieved successfully",
                1);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<Model_ErrorReport>.Failure(
                "An unexpected error occurred while retrieving the error report.",
                ex);
        }
    }

    /// <summary>
    /// Updates the status of an error report via sp_error_reports_UpdateStatus.
    /// </summary>
    /// <param name="reportId">Report identifier to update.</param>
    /// <param name="newStatus">New status value (New, Reviewed, Resolved).</param>
    /// <param name="developerNotes">Optional developer notes to store with the status change.</param>
    /// <param name="reviewedBy">Username of the developer performing the update.</param>
    /// <param name="progressHelper">Optional progress helper for UI feedback.</param>
    /// <returns>DaoResult indicating success or failure of the update operation.</returns>
    public static async Task<DaoResult<bool>> UpdateErrorReportStatusAsync(
        int reportId,
        string newStatus,
        string? developerNotes,
        string reviewedBy,
        Helper_StoredProcedureProgress? progressHelper = null)
    {
        if (reportId <= 0)
        {
            return DaoResult<bool>.Failure("Report ID must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(newStatus))
        {
            return DaoResult<bool>.Failure("Status is required when updating an error report.");
        }

        if (string.IsNullOrWhiteSpace(reviewedBy))
        {
            return DaoResult<bool>.Failure("ReviewedBy is required when updating an error report.");
        }

        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            var parameters = new Dictionary<string, object>
            {
                ["ReportID"] = reportId,
                ["NewStatus"] = newStatus.Trim(),
                ["DeveloperNotes"] = string.IsNullOrWhiteSpace(developerNotes)
                    ? (object)DBNull.Value
                    : developerNotes.Trim(),
                ["ReviewedBy"] = reviewedBy.Trim(),
                ["ReviewedDate"] = DateTime.Now
            };

            var storedProcedureResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "sp_error_reports_UpdateStatus",
                parameters,
                progressHelper);

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? "Failed to update error report status.";

                LoggingUtility.Log(
                    $"[Dao_ErrorReports] Stored procedure sp_error_reports_UpdateStatus failed for ReportID {reportId}: {errorMessage}");

                return DaoResult<bool>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            LoggingUtility.LogApplicationInfo(
                $"[Dao_ErrorReports] Updated report {reportId} to status '{newStatus.Trim()}' by {reviewedBy.Trim()}.");

            return DaoResult<bool>.Success(true, storedProcedureResult.StatusMessage ?? "Status updated successfully");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<bool>.Failure(
                "An unexpected error occurred while updating the error report status.",
                ex);
        }
    }

    /// <summary>
    /// Retrieves the distinct list of usernames from error reports.
    /// </summary>
    /// <returns>DaoResult containing the user list.</returns>
    public static async Task<DaoResult<List<string>>> GetUserListAsync()
    {
        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            var storedProcedureResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "sp_error_reports_GetUserList");

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? "Failed to retrieve error report user list.";

                LoggingUtility.Log(
                    "[Dao_ErrorReports] Stored procedure sp_error_reports_GetUserList failed: " + errorMessage);

                return DaoResult<List<string>>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            var users = ExtractStringColumn(storedProcedureResult.Data, "UserName");

            return DaoResult<List<string>>.Success(
                users,
                storedProcedureResult.StatusMessage ?? "Retrieved user list successfully",
                users.Count);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<List<string>>.Failure(
                "An unexpected error occurred while retrieving the user list.",
                ex);
        }
    }

    /// <summary>
    /// Retrieves the distinct list of machine names from error reports.
    /// </summary>
    /// <returns>DaoResult containing the machine list.</returns>
    public static async Task<DaoResult<List<string>>> GetMachineListAsync()
    {
        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            var storedProcedureResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "sp_error_reports_GetMachineList");

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? "Failed to retrieve error report machine list.";

                LoggingUtility.Log(
                    "[Dao_ErrorReports] Stored procedure sp_error_reports_GetMachineList failed: " + errorMessage);

                return DaoResult<List<string>>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            var machines = ExtractStringColumn(storedProcedureResult.Data, "MachineName");

            return DaoResult<List<string>>.Success(
                machines,
                storedProcedureResult.StatusMessage ?? "Retrieved machine list successfully",
                machines.Count);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<List<string>>.Failure(
                "An unexpected error occurred while retrieving the machine list.",
                ex);
        }
    }

    #endregion

    #region Helpers

    private static Dictionary<string, object> BuildFilterParameters(Model_ErrorReportFilter filter)
    {
        return new Dictionary<string, object>
        {
            ["DateFrom"] = filter.DateFrom ?? (object)DBNull.Value,
            ["DateTo"] = filter.DateTo ?? (object)DBNull.Value,
            ["UserName"] = filter.UserName ?? (object)DBNull.Value,
            ["MachineName"] = filter.MachineName ?? (object)DBNull.Value,
            ["Status"] = filter.Status ?? (object)DBNull.Value,
            ["SearchText"] = filter.HasSearchText ? filter.SearchText! : (object)DBNull.Value
        };
    }

    private static Model_ErrorReport MapToErrorReport(DataRow row)
    {
        ArgumentNullException.ThrowIfNull(row);

        return new Model_ErrorReport
        {
            ReportID = row.Field<int>("ReportID"),
            ReportDate = row.Field<DateTime>("ReportDate"),
            UserName = row.Field<string?>("UserName") ?? string.Empty,
            MachineName = row.Field<string?>("MachineName"),
            AppVersion = row.Field<string?>("AppVersion"),
            ErrorType = row.Field<string?>("ErrorType"),
            ErrorSummary = row.Field<string?>("ErrorSummary"),
            UserNotes = row.Field<string?>("UserNotes"),
            TechnicalDetails = row.Field<string?>("TechnicalDetails"),
            CallStack = row.Field<string?>("CallStack"),
            Status = ParseStatus(row.Field<string?>("Status")),
            ReviewedBy = row.Field<string?>("ReviewedBy"),
            ReviewedDate = row.Field<DateTime?>("ReviewedDate"),
            DeveloperNotes = row.Field<string?>("DeveloperNotes")
        };
    }

    private static ErrorReportStatus ParseStatus(string? statusValue)
    {
        return statusValue?.Trim().ToLowerInvariant() switch
        {
            "reviewed" => ErrorReportStatus.Reviewed,
            "resolved" => ErrorReportStatus.Resolved,
            _ => ErrorReportStatus.New
        };
    }

    private static List<string> ExtractStringColumn(DataTable? table, string columnName)
    {
        if (table == null || !table.Columns.Contains(columnName))
        {
            return new List<string>();
        }

        return table.AsEnumerable()
            .Select(row => row.Field<string?>(columnName))
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => value!.Trim())
            .ToList();
    }

    #endregion
}
