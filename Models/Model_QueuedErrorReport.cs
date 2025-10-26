using System;
using System.IO;

namespace MTM_WIP_Application_WinForms.Models;

/// <summary>
/// Represents an offline queued error report stored as a local SQL file.
/// Used for tracking pending reports that need to be synchronized with the database.
/// </summary>
public class Model_QueuedErrorReport
{
    #region Properties

    /// <summary>
    /// Full absolute path to the SQL file in the Pending folder.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Filename only (without directory path).
    /// Format: ErrorReport_YYYYMMDD_HHMMSS_UserName_GUID.sql
    /// Example: "ErrorReport_20251025_143022_JohnSmith_a3f8b2.sql"
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// File creation timestamp (from FileInfo.CreationTime).
    /// Used for sorting files chronologically during sync.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// File size in bytes.
    /// Used for logging and diagnostics.
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// Number of sync attempts for this file (tracked in-memory during processing).
    /// Incremented each time ExecuteSqlFileAsync is called for this file.
    /// </summary>
    public int AttemptCount { get; set; }

    /// <summary>
    /// Indicates whether the SQL file passes basic validation checks.
    /// Validated during FromFileInfo factory method.
    /// </summary>
    public bool IsValid { get; set; }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a Model_QueuedErrorReport instance from a FileInfo object.
    /// Performs basic validation on the file.
    /// </summary>
    /// <param name="fileInfo">FileInfo for the SQL file.</param>
    /// <returns>Model_QueuedErrorReport instance with populated properties.</returns>
    /// <exception cref="ArgumentNullException">Thrown when fileInfo is null.</exception>
    public static Model_QueuedErrorReport FromFileInfo(FileInfo fileInfo)
    {
        ArgumentNullException.ThrowIfNull(fileInfo);

        var queuedReport = new Model_QueuedErrorReport
        {
            FilePath = fileInfo.FullName,
            FileName = fileInfo.Name,
            CreationDate = fileInfo.CreationTime,
            FileSize = fileInfo.Length,
            AttemptCount = 0
        };

        // Validate SQL file
        queuedReport.IsValid = ValidateSqlFile(fileInfo.FullName);

        return queuedReport;
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Performs basic validation on the SQL file to ensure it's processable.
    /// Checks for:
    /// - File exists and is readable
    /// - Contains required SQL keywords (START TRANSACTION, CALL sp_error_reports_Insert, COMMIT)
    /// - File size is reasonable (not empty, not excessively large)
    /// </summary>
    /// <param name="filePath">Full path to the SQL file.</param>
    /// <returns>True if file passes validation, false otherwise.</returns>
    private static bool ValidateSqlFile(string filePath)
    {
        try
        {
            // Check file exists
            if (!File.Exists(filePath))
                return false;

            // Check file size is reasonable (not empty, not >10MB)
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Length == 0 || fileInfo.Length > 10 * 1024 * 1024)
                return false;

            // Read file content
            string content = File.ReadAllText(filePath);

            // Validate required SQL keywords are present
            if (!content.Contains("START TRANSACTION", StringComparison.OrdinalIgnoreCase))
                return false;

            if (!content.Contains("CALL sp_error_reports_Insert", StringComparison.OrdinalIgnoreCase))
                return false;

            if (!content.Contains("COMMIT", StringComparison.OrdinalIgnoreCase))
                return false;

            // Passed all validation checks
            return true;
        }
        catch
        {
            // Any exception during validation means file is invalid
            return false;
        }
    }

    #endregion
}
