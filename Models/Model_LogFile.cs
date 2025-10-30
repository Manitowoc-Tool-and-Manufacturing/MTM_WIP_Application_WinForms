namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Represents metadata about a single log file in the user's log directory.
/// Includes file properties, size formatting, and log type detection.
/// </summary>
public class Model_LogFile
{
    #region Properties

    /// <summary>
    /// Full absolute path to the log file.
    /// </summary>
    public required string FilePath { get; init; }

    /// <summary>
    /// File name without directory path.
    /// </summary>
    public required string FileName { get; init; }

    /// <summary>
    /// Detected log format type based on filename pattern.
    /// </summary>
    public required LogFormat LogType { get; init; }

    /// <summary>
    /// File size in bytes.
    /// </summary>
    public required long FileSizeBytes { get; init; }

    /// <summary>
    /// Human-readable file size (e.g., "1.5 MB", "234 KB").
    /// Computed from FileSizeBytes using FormatFileSize.
    /// </summary>
    public string FileSizeDisplay => FormatFileSize(FileSizeBytes);

    /// <summary>
    /// File creation date from filesystem.
    /// </summary>
    public required DateTime CreatedDate { get; init; }

    /// <summary>
    /// File last modified date from filesystem.
    /// </summary>
    public required DateTime ModifiedDate { get; init; }

    /// <summary>
    /// Username who owns this log file (derived from directory structure).
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Number of log entries in this file (optional, populated on-demand).
    /// Null until counted.
    /// </summary>
    public int? EntryCount { get; set; }

    /// <summary>
    /// Indicates if this log file is currently selected in the UI.
    /// </summary>
    public bool IsSelected { get; set; }

    #endregion

    #region Helpers

    /// <summary>
    /// Formats file size in bytes to human-readable string with appropriate unit.
    /// </summary>
    /// <param name="bytes">Size in bytes.</param>
    /// <returns>Formatted string like "1.5 MB" or "234 KB".</returns>
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
    /// Returns a display-friendly string representation of this log file.
    /// </summary>
    /// <returns>String in format "FileName (FileSizeDisplay) - ModifiedDate".</returns>
    public override string ToString()
    {
        return $"{FileName} ({FileSizeDisplay}) - {ModifiedDate:yyyy-MM-dd HH:mm:ss}";
    }

    #endregion
}
