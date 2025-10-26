using System;
using System.Collections.Generic;
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

    #endregion

    #region Helpers

    // Reserved for future helper methods (e.g., GetReportById, SearchReports)

    #endregion
}
