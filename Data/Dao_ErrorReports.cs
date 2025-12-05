using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_WinForms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

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
    /// Model_Dao_Result containing the generated ReportID on success, or error information on failure.
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
    public static async Task<Model_Dao_Result<int>> InsertReportAsync(Model_ErrorReport_Core report,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        ArgumentNullException.ThrowIfNull(report);

        try
        {
            // Get connection string
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            // Prepare parameters (WITH p_ prefix - sp_error_reports_Insert uses p_ prefix)
            // NOTE: Must include p_ prefix explicitly since this may be called during early startup
            // before parameter prefix cache is fully initialized
            var parameters = new Dictionary<string, object>
            {
                ["p_UserName"] = report.UserName ?? string.Empty,
                ["p_MachineName"] = report.MachineName ?? (object)DBNull.Value,
                ["p_AppVersion"] = report.AppVersion ?? (object)DBNull.Value,
                ["p_ErrorType"] = report.ErrorType ?? (object)DBNull.Value,
                ["p_ErrorSummary"] = report.ErrorSummary ?? (object)DBNull.Value,
                ["p_UserNotes"] = report.UserNotes ?? (object)DBNull.Value,
                ["p_TechnicalDetails"] = report.TechnicalDetails ?? (object)DBNull.Value,
                ["p_CallStack"] = report.CallStack ?? (object)DBNull.Value
            };

            // Execute stored procedure with custom output parameter for ReportID
            var outputParams = new List<string> { "ReportID" };
            
            var result = await Helper_Database_StoredProcedure.ExecuteWithCustomOutputAsync(
                connectionString,
                "sp_error_reports_Insert",
                parameters,
                outputParams,
                progressHelper: null);

            if (result.IsSuccess)
            {
                // Extract ReportID from output parameters
                int reportID = 0;
                if (result.Data != null && result.Data.ContainsKey("ReportID"))
                {
                    reportID = Convert.ToInt32(result.Data["ReportID"]);
                }

                if (reportID > 0)
                {
                    LoggingUtility.LogApplicationInfo(
                        $"[Dao_ErrorReports] Successfully inserted error report. ReportID: {reportID}, User: {report.UserName}");

                    return Model_Dao_Result<int>.Success(
                        reportID,
                        $"Error report submitted successfully. Report ID: {reportID}");
                }
                else
                {
                    // Success but no ReportID returned - log and return generic success
                    LoggingUtility.Log(
                        "[Dao_ErrorReports] Warning: Error report inserted but ReportID not returned from stored procedure");

                    return Model_Dao_Result<int>.Success(
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

                return Model_Dao_Result<int>.Failure(
                    result.StatusMessage ?? "Failed to submit error report.");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);

            return Model_Dao_Result<int>.Failure(
                "An unexpected error occurred while submitting the error report.");
        }
    }

    /// <summary>
    /// Retrieves error reports using the sp_error_reports_GetAll stored procedure with optional filters.
    /// Supports filtering by date range, user, machine, status, and search text across multiple fields.
    /// </summary>
    /// <param name="filter">Filter criteria; pass null to retrieve all error reports. All filter properties are optional.</param>
    /// <param name="progressHelper">Optional progress helper for long-running operations.</param>
    /// <returns>A Model_Dao_Result containing a DataTable of error reports when successful. Never null, but may be empty.</returns>
    /// <remarks>
    /// The stored procedure sp_error_reports_GetAll performs server-side filtering for optimal performance.
    /// Search text filters across ErrorSummary, UserNotes, and TechnicalDetails fields using LIKE queries.
    /// Date filters are inclusive (DateFrom >= ReportDate <= DateTo).
    /// Results are ordered by ReportDate DESC (newest first).
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown when filter validation fails (e.g., DateFrom > DateTo).</exception>
    public static async Task<Model_Dao_Result<DataTable>> GetAllErrorReportsAsync(
        Model_ErrorReport_Core_Filter? filter,
        Helper_StoredProcedureProgress? progressHelper = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        filter ??= new Model_ErrorReport_Core_Filter();

        if (!filter.TryValidate(out var validationMessage))
        {
            LoggingUtility.Log(
                "[Dao_ErrorReports] Invalid filter supplied for GetAllErrorReportsAsync: " + validationMessage);

            return Model_Dao_Result<DataTable>.Failure(
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
                progressHelper,
                connection: connection,
                transaction: transaction);

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? "Failed to retrieve error reports.";

                LoggingUtility.Log(
                    "[Dao_ErrorReports] Stored procedure sp_error_reports_GetAll failed: " + errorMessage);

                return Model_Dao_Result<DataTable>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            var rowCount = storedProcedureResult.Data?.Rows.Count ?? 0;
            LoggingUtility.LogApplicationInfo(
                $"[Dao_ErrorReports] Retrieved {rowCount} error report records from database.");

            return storedProcedureResult;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<DataTable>.Failure(
                "An unexpected error occurred while retrieving error reports.",
                ex);
        }
    }

    /// <summary>
    /// Retrieves a single error report by ReportID using sp_error_reports_GetByID.
    /// Returns complete report including all TEXT fields (CallStack, TechnicalDetails, UserNotes).
    /// </summary>
    /// <param name="reportId">The report identifier to retrieve. Must be greater than zero.</param>
    /// <param name="progressHelper">Optional progress helper for UI updates.</param>
    /// <returns>Model_Dao_Result containing the populated Model_ErrorReport_Core on success, or error information if not found.</returns>
    /// <remarks>
    /// This method retrieves all 14 fields from the error_reports table including large TEXT fields.
    /// The stored procedure includes validation for ReportID existence and returns appropriate status codes.
    /// NULL fields are safely converted to empty strings or null as appropriate per the model.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown when reportId is less than or equal to zero.</exception>
    public static async Task<Model_Dao_Result<Model_ErrorReport_Core>> GetErrorReportByIdAsync(
        int reportId,
        Helper_StoredProcedureProgress? progressHelper = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        if (reportId <= 0)
        {
            return Model_Dao_Result<Model_ErrorReport_Core>.Failure("Report ID must be greater than zero.");
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
                progressHelper,
                connection: connection,
                transaction: transaction);

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? $"Failed to retrieve error report {reportId}.";

                LoggingUtility.Log(
                    $"[Dao_ErrorReports] Stored procedure sp_error_reports_GetByID failed for ReportID {reportId}: {errorMessage}");

                return Model_Dao_Result<Model_ErrorReport_Core>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            if (storedProcedureResult.Data == null || storedProcedureResult.Data.Rows.Count == 0)
            {
                return Model_Dao_Result<Model_ErrorReport_Core>.Failure(
                    $"Error report {reportId} was not found in the database.");
            }

            var report = MapToErrorReport(storedProcedureResult.Data.Rows[0]);

            LoggingUtility.LogApplicationInfo(
                $"[Dao_ErrorReports] Retrieved detail for error report {reportId}.");

            return Model_Dao_Result<Model_ErrorReport_Core>.Success(
                report,
                storedProcedureResult.StatusMessage ?? "Error report retrieved successfully",
                1);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<Model_ErrorReport_Core>.Failure(
                "An unexpected error occurred while retrieving the error report.",
                ex);
        }
    }

    /// <summary>
    /// Updates the status of an error report using sp_error_reports_UpdateStatus.
    /// Automatically sets ReviewedDate to current timestamp when status changes.
    /// Supports transitions between New, Reviewed, and Resolved states.
    /// </summary>
    /// <param name="reportId">Report identifier to update.</param>
    /// <param name="newStatus">New status value (New, Reviewed, Resolved). Must match valid status enum.</param>
    /// <param name="developerNotes">Optional developer notes to store with the status change. Saved to DeveloperNotes field.</param>
    /// <param name="reviewedBy">Username of the developer performing the update. Required, cannot be empty.</param>
    /// <param name="progressHelper">Optional progress helper for UI feedback.</param>
    /// <returns>Model_Dao_Result indicating success or failure of the update operation.</returns>
    /// <remarks>
    /// The stored procedure validates:
    /// - ReportID exists
    /// - NewStatus is one of: New, Reviewed, Resolved
    /// - ReviewedBy is not empty
    ///
    /// Uses transactions to ensure atomicity of the update.
    /// ReviewedDate is automatically set to NOW() by the stored procedure.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown when validation fails for reportId, newStatus, or reviewedBy.</exception>
    public static async Task<Model_Dao_Result<bool>> UpdateErrorReportStatusAsync(
        int reportId,
        string newStatus,
        string? developerNotes,
        string reviewedBy,
        Helper_StoredProcedureProgress? progressHelper = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        if (reportId <= 0)
        {
            return Model_Dao_Result<bool>.Failure("Report ID must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(newStatus))
        {
            return Model_Dao_Result<bool>.Failure("Status is required when updating an error report.");
        }

        if (string.IsNullOrWhiteSpace(reviewedBy))
        {
            return Model_Dao_Result<bool>.Failure("ReviewedBy is required when updating an error report.");
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
                progressHelper,
                connection: connection,
                transaction: transaction);

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? "Failed to update error report status.";

                LoggingUtility.Log(
                    $"[Dao_ErrorReports] Stored procedure sp_error_reports_UpdateStatus failed for ReportID {reportId}: {errorMessage}");

                return Model_Dao_Result<bool>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            LoggingUtility.LogApplicationInfo(
                $"[Dao_ErrorReports] Updated report {reportId} to status '{newStatus.Trim()}' by {reviewedBy.Trim()}.");

            return Model_Dao_Result<bool>.Success(true, storedProcedureResult.StatusMessage ?? "Status updated successfully");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<bool>.Failure(
                "An unexpected error occurred while updating the error report status.",
                ex);
        }
    }

    /// <summary>
    /// Retrieves the distinct list of usernames from error reports using sp_error_reports_GetUserList.
    /// Returns alphabetically sorted list for populating filter dropdowns.
    /// </summary>
    /// <returns>Model_Dao_Result containing the user list. Empty list if no reports exist.</returns>
    /// <remarks>
    /// Uses DISTINCT to eliminate duplicates and ORDER BY UserName for alphabetical sorting.
    /// This method is typically called once when initializing filter controls.
    /// The calling code should prepend UI options like "[ All Users ]".
    /// </remarks>
    public static async Task<Model_Dao_Result<List<string>>> GetUserListAsync(
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            var storedProcedureResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "sp_error_reports_GetUserList",
                connection: connection,
                transaction: transaction);

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? "Failed to retrieve error report user list.";

                LoggingUtility.Log(
                    "[Dao_ErrorReports] Stored procedure sp_error_reports_GetUserList failed: " + errorMessage);

                return Model_Dao_Result<List<string>>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            var users = ExtractStringColumn(storedProcedureResult.Data, "UserName");

            return Model_Dao_Result<List<string>>.Success(
                users,
                storedProcedureResult.StatusMessage ?? "Retrieved user list successfully",
                users.Count);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<List<string>>.Failure(
                "An unexpected error occurred while retrieving the user list.",
                ex);
        }
    }

    /// <summary>
    /// Retrieves the distinct list of machine names from error reports using sp_error_reports_GetMachineList.
    /// Returns alphabetically sorted list excluding NULL/empty values for populating filter dropdowns.
    /// </summary>
    /// <returns>Model_Dao_Result containing the machine list. Empty list if no reports exist with machine names.</returns>
    /// <remarks>
    /// Uses DISTINCT with WHERE MachineName IS NOT NULL AND MachineName != '' to filter blanks.
    /// Results are sorted alphabetically by MachineName.
    /// This method is typically called once when initializing filter controls.
    /// The calling code should prepend UI options like "[ All Machines ]".
    /// </remarks>
    public static async Task<Model_Dao_Result<List<string>>> GetMachineListAsync(
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            var storedProcedureResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                connectionString,
                "sp_error_reports_GetMachineList",
                connection: connection,
                transaction: transaction);

            if (!storedProcedureResult.IsSuccess)
            {
                var errorMessage = storedProcedureResult.ErrorMessage
                    ?? storedProcedureResult.StatusMessage
                    ?? "Failed to retrieve error report machine list.";

                LoggingUtility.Log(
                    "[Dao_ErrorReports] Stored procedure sp_error_reports_GetMachineList failed: " + errorMessage);

                return Model_Dao_Result<List<string>>.Failure(errorMessage, storedProcedureResult.Exception);
            }

            var machines = ExtractStringColumn(storedProcedureResult.Data, "MachineName");

            return Model_Dao_Result<List<string>>.Success(
                machines,
                storedProcedureResult.StatusMessage ?? "Retrieved machine list successfully",
                machines.Count);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<List<string>>.Failure(
                "An unexpected error occurred while retrieving the machine list.",
                ex);
        }
    }

    /// <summary>
    /// Deletes an error report using sp_error_reports_Delete.
    /// </summary>
    /// <param name="reportId">The report identifier to delete.</param>
    /// <param name="connection">Optional database connection.</param>
    /// <param name="transaction">Optional database transaction.</param>
    /// <returns>Model_Dao_Result indicating success or failure.</returns>
    public static async Task<Model_Dao_Result<bool>> DeleteReportAsync(
        int reportId,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        if (reportId <= 0)
        {
            return Model_Dao_Result<bool>.Failure("Report ID must be greater than zero.");
        }

        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);

            var parameters = new Dictionary<string, object>
            {
                ["ReportID"] = reportId
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                connectionString,
                "sp_error_reports_Delete",
                parameters,
                connection: connection,
                transaction: transaction);

            if (!result.IsSuccess)
            {
                LoggingUtility.Log(
                    $"[Dao_ErrorReports] Failed to delete report {reportId}: {result.ErrorMessage}");
                return Model_Dao_Result<bool>.Failure(result.ErrorMessage ?? "Failed to delete report.");
            }

            LoggingUtility.LogApplicationInfo($"[Dao_ErrorReports] Deleted report {reportId}.");
            return Model_Dao_Result<bool>.Success(true, "Report deleted successfully.");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<bool>.Failure(
                "An unexpected error occurred while deleting the report.",
                ex);
        }
    }

    #endregion

    #region Helpers

    private static Dictionary<string, object> BuildFilterParameters(Model_ErrorReport_Core_Filter filter)
    {
        return new Dictionary<string, object>
        {
            ["DateFrom"] = filter.DateFrom ?? (object)DBNull.Value,
            ["DateTo"] = filter.DateTo ?? (object)DBNull.Value,
            ["UserName"] = filter.UserName ?? (object)DBNull.Value,
            ["MachineName"] = filter.MachineName ?? (object)DBNull.Value,
            ["StatusFilter"] = filter.Status ?? (object)DBNull.Value,
            ["SearchText"] = filter.HasSearchText ? filter.SearchText! : (object)DBNull.Value
        };
    }

    private static Model_ErrorReport_Core MapToErrorReport(DataRow row)
    {
        ArgumentNullException.ThrowIfNull(row);

        return new Model_ErrorReport_Core
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
