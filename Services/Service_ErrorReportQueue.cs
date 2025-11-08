using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_WinForms.Models;

namespace MTM_WIP_Application_Winforms.Services;

/// <summary>
/// Service for queueing error reports as local SQL files when database is unavailable.
/// Manages pending and archived error report files.
/// </summary>
internal static class Service_ErrorReportQueue
{
    #region Fields

    private static readonly string QueueDirectory = Model_Application_Variables.ErrorReporting.QueueDirectory;
    private static readonly string ArchiveDirectory = Model_Application_Variables.ErrorReporting.ArchiveDirectory;

    #endregion

    #region Queue Operations

    /// <summary>
    /// Queues an error report as a local SQL file for later synchronization.
    /// Creates Pending and Sent directories if they don't exist.
    /// </summary>
    /// <param name="report">The error report to queue.</param>
    /// <returns>Model_Dao_Result containing the file path on success.</returns>
    public static async Task<Model_Dao_Result<string>> QueueReportAsync(Model_ErrorReport_Core report)
    {
        ArgumentNullException.ThrowIfNull(report);

        try
        {
            // Ensure directories exist
            Directory.CreateDirectory(QueueDirectory);
            Directory.CreateDirectory(ArchiveDirectory);

            // Generate filename: ErrorReport_{timestamp}_{sanitizedUser}_{guid}.sql
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string sanitizedUser = SanitizeUsername(report.UserName);
            string guidPart = Guid.NewGuid().ToString().Substring(0, 6);
            string filename = $"ErrorReport_{timestamp}_{sanitizedUser}_{guidPart}.sql";
            string filePath = Path.Combine(QueueDirectory, filename);

            // Generate SQL content
            string sqlContent = GenerateSqlForReport(report);

            // Write to file
            await File.WriteAllTextAsync(filePath, sqlContent, Encoding.UTF8);

            LoggingUtility.Log($"[Service_ErrorReportQueue] Queued error report: {filename}");

            return Model_Dao_Result<string>.Success(
                filePath,
                $"Error report queued successfully. Will be submitted when database connection is restored.");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<string>.Failure(
                "Failed to queue error report for offline submission.",
                ex);
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Generates SQL file content for an error report.
    /// Creates a complete SQL transaction that calls sp_error_reports_Insert.
    /// </summary>
    /// <param name="report">The error report to convert to SQL.</param>
    /// <returns>SQL file content as string.</returns>
    private static string GenerateSqlForReport(Model_ErrorReport_Core report)
    {
        var sql = new StringBuilder();

        // Header with metadata
        sql.AppendLine("-- =============================================");
        sql.AppendLine("-- Queued Error Report");
        sql.AppendLine($"-- Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sql.AppendLine($"-- User: {report.UserName}");
        sql.AppendLine($"-- Error Type: {report.ErrorType ?? "Unknown"}");
        sql.AppendLine("-- =============================================");
        sql.AppendLine();

        // Start transaction
        sql.AppendLine("START TRANSACTION;");
        sql.AppendLine();

        // Call stored procedure
        sql.AppendLine("CALL sp_error_reports_Insert(");
        sql.AppendLine($"    {EscapeSqlString(report.UserName)},                    -- p_UserName");
        sql.AppendLine($"    {EscapeSqlString(report.MachineName)},                 -- p_MachineName");
        sql.AppendLine($"    {EscapeSqlString(report.AppVersion)},                  -- p_AppVersion");
        sql.AppendLine($"    {EscapeSqlString(report.ErrorType)},                   -- p_ErrorType");
        sql.AppendLine($"    {EscapeSqlString(report.ErrorSummary)},                -- p_ErrorSummary");
        sql.AppendLine($"    {EscapeSqlString(report.UserNotes)},                   -- p_UserNotes");
        sql.AppendLine($"    {EscapeSqlString(report.TechnicalDetails)},            -- p_TechnicalDetails");
        sql.AppendLine($"    {EscapeSqlString(report.CallStack)},                   -- p_CallStack");
        sql.AppendLine("    @reportID,                                             -- OUT p_ReportID");
        sql.AppendLine("    @status,                                               -- OUT p_Status");
        sql.AppendLine("    @errorMsg                                              -- OUT p_ErrorMsg");
        sql.AppendLine(");");
        sql.AppendLine();

        // Validation check
        sql.AppendLine("-- Validation check");
        sql.AppendLine("SELECT @status AS Status, @errorMsg AS Message, @reportID AS ReportID;");
        sql.AppendLine();

        // Commit transaction
        sql.AppendLine("COMMIT;");

        return sql.ToString();
    }

    /// <summary>
    /// Escapes a string value for safe inclusion in SQL statements.
    /// Handles NULL values and single quote escaping.
    /// </summary>
    /// <param name="value">The string value to escape.</param>
    /// <returns>Escaped SQL string literal or NULL.</returns>
    private static string EscapeSqlString(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "NULL";
        }

        // Escape single quotes by doubling them
        string escaped = value.Replace("'", "''");

        // Wrap in single quotes
        return $"'{escaped}'";
    }

    /// <summary>
    /// Sanitizes a username for use in filenames.
    /// Removes dots, spaces, and special characters that could cause file system issues.
    /// </summary>
    /// <param name="username">The username to sanitize.</param>
    /// <returns>Sanitized username safe for filenames.</returns>
    private static string SanitizeUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return "Unknown";
        }

        // Remove invalid filename characters
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = new StringBuilder();

        foreach (char c in username)
        {
            if (!invalidChars.Contains(c) && c != '.' && c != ' ')
            {
                sanitized.Append(c);
            }
        }

        string result = sanitized.ToString();
        return string.IsNullOrEmpty(result) ? "Unknown" : result;
    }

    #endregion
}
