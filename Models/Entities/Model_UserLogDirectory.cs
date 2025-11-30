namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Represents a user's log directory with file metadata and aggregate statistics.
/// Provides summary information for user selection and log file browsing.
/// </summary>
public class Model_UserLogDirectory
{
    #region Properties

    /// <summary>
    /// Username associated with this log directory.
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Full path to the user's log directory.
    /// </summary>
    public required string DirectoryPath { get; init; }

    /// <summary>
    /// Count of normal log files in this directory.
    /// </summary>
    public int NormalLogCount { get; set; }

    /// <summary>
    /// Count of application error log files in this directory.
    /// </summary>
    public int AppErrorLogCount { get; set; }

    /// <summary>
    /// Count of database error log files in this directory.
    /// </summary>
    public int DbErrorLogCount { get; set; }

    /// <summary>
    /// Total count of all log files across all types.
    /// Computed from type-specific counts.
    /// </summary>
    public int TotalLogCount => NormalLogCount + AppErrorLogCount + DbErrorLogCount;

    /// <summary>
    /// Total size of all log files in bytes.
    /// </summary>
    public long TotalSizeBytes { get; set; }

    /// <summary>
    /// Human-readable total size (e.g., "15.3 MB", "2.1 GB").
    /// Computed from TotalSizeBytes using FormatFileSize.
    /// </summary>
    public string TotalSizeDisplay => FormatFileSize(TotalSizeBytes);

    /// <summary>
    /// Date of the most recently modified log file in this directory.
    /// Null if directory is empty or inaccessible.
    /// </summary>
    public DateTime? MostRecentLogDate { get; set; }

    /// <summary>
    /// Indicates whether the directory is accessible (permissions check passed).
    /// </summary>
    public bool IsAccessible { get; set; } = true;

    /// <summary>
    /// Collection of log file metadata for this user's directory.
    /// Populated on-demand when browsing files.
    /// </summary>
    public List<Model_LogFile> LogFiles { get; set; } = new();

    #endregion

    #region Helpers

    /// <summary>
    /// Formats file size in bytes to human-readable string with appropriate unit.
    /// </summary>
    /// <param name="bytes">Size in bytes.</param>
    /// <returns>Formatted string like "15.3 MB" or "2.1 GB".</returns>
    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;

        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }

        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// Returns a display-friendly string representation of this user log directory.
    /// </summary>
    /// <returns>String in format "Username (TotalLogCount files, TotalSizeDisplay)".</returns>
    public override string ToString()
    {
        return $"{Username} ({TotalLogCount} files, {TotalSizeDisplay})";
    }

    #endregion
}
