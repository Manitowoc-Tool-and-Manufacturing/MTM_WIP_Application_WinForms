using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace MTM_WIP_Application_Winforms.Services;

/// <summary>
/// Provides asynchronous log file reading and enumeration services with format detection
/// and windowed entry loading for performance.
/// </summary>
public class Service_LogFileReader
{
    #region Constants

    /// <summary>
    /// Default window size for loading log entries (lines to load at once).
    /// </summary>
    private const int DefaultWindowSize = 500;

    /// <summary>
    /// Regex pattern for detecting normal log filename format:
    /// - USERNAME YYYY-MM-DD @ H-MM [AM/PM]_normal.csv
    /// </summary>
    private static readonly Regex NormalLogPattern = new(@"_normal\.csv$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Regex pattern for detecting application error filename format:
    /// - USERNAME YYYY-MM-DD @ H-MM [AM/PM]_app_error.csv
    /// </summary>
    private static readonly Regex AppErrorPattern = new(@"_app_error\.csv$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Regex pattern for detecting database error filename format:
    /// - USERNAME YYYY-MM-DD @ H-MM [AM/PM]_db_error.csv
    /// </summary>
    private static readonly Regex DbErrorPattern = new(@"_db_error\.csv$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    #endregion

    #region File Enumeration

    /// <summary>
    /// Enumerates all log files for a specific user asynchronously.
    /// </summary>
    /// <param name="username">Username to enumerate log files for.</param>
    /// <returns>List of Model_LogFile instances representing the user's log files.</returns>
    /// <exception cref="ArgumentException">Thrown if username is invalid.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if directory access is denied.</exception>
    public static async Task<List<Model_LogFile>> EnumerateLogFilesAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));
        }

        // Get and validate user directory path
        string? userDirectory = Helper_LogPath.GetUserLogDirectory(username);
        if (userDirectory == null)
        {

            return new List<Model_LogFile>();
        }

        // Ensure directory exists
        if (!Directory.Exists(userDirectory))
        {

            return new List<Model_LogFile>();
        }

        var logFiles = new List<Model_LogFile>();

        try
        {
            // Enumerate files asynchronously
            await Task.Run(() =>
            {
                var files = Directory.GetFiles(userDirectory, "*.csv", SearchOption.TopDirectoryOnly);

                foreach (var filePath in files)
                {
                    try
                    {
                        // Validate path security
                        if (!Helper_LogPath.IsPathSafe(filePath))
                        {

                            continue;
                        }

                        var fileInfo = new FileInfo(filePath);
                        if (!fileInfo.Exists)
                        {
                            continue;
                        }

                        // Skip empty files - don't show them in the viewer
                        if (fileInfo.Length == 0)
                        {

                            continue;
                        }

                        // Detect log type from filename
                        LogFormat logType = DetectLogTypeFromFilename(fileInfo.Name);

                        // Create log file model
                        var logFile = new Model_LogFile
                        {
                            FilePath = filePath,
                            FileName = fileInfo.Name,
                            LogType = logType,
                            FileSizeBytes = fileInfo.Length,
                            CreatedDate = fileInfo.CreationTime,
                            ModifiedDate = fileInfo.LastWriteTime,
                            Username = username,
                            IsSelected = false
                        };

                        logFiles.Add(logFile);
                    }
                    catch (Exception ex)
                    {

                        LoggingUtility.LogApplicationError(ex);
                    }
                }
            }).ConfigureAwait(false);

            // Sort by modified date descending (most recent first)
            logFiles = logFiles.OrderByDescending(f => f.ModifiedDate).ToList();


        }
        catch (UnauthorizedAccessException ex)
        {

            LoggingUtility.LogApplicationError(ex);
            throw;
        }
        catch (Exception ex)
        {

            LoggingUtility.LogApplicationError(ex);
            throw;
        }

        return logFiles;
    }

    #endregion

    #region Format Detection

    /// <summary>
    /// Detects log format type from filename pattern.
    /// </summary>
    /// <param name="filename">Filename to analyze.</param>
    /// <returns>Detected LogFormat type or Unknown if pattern doesn't match.</returns>
    private static LogFormat DetectLogTypeFromFilename(string filename)
    {
        if (NormalLogPattern.IsMatch(filename))
        {
            return LogFormat.Normal;
        }

        if (AppErrorPattern.IsMatch(filename))
        {
            return LogFormat.ApplicationError;
        }

        if (DbErrorPattern.IsMatch(filename))
        {
            return LogFormat.DatabaseError;
        }

        return LogFormat.Unknown;
    }

    #endregion

    #region CSV Parsing Helper

    /// <summary>
    /// Reads a single CSV line, handling quoted fields with embedded commas and newlines.
    /// Continues reading multiple lines if within a quoted field.
    /// </summary>
    /// <param name="reader">StreamReader to read from.</param>
    /// <returns>Complete CSV line (may span multiple physical lines), or null if end of stream.</returns>
    private static async Task<string?> ReadCsvLineAsync(StreamReader reader)
    {
        if (reader.EndOfStream)
        {
            return null;
        }

        var lineBuilder = new StringBuilder();
        bool inQuotes = false;

        while (!reader.EndOfStream)
        {
            string? physicalLine = await reader.ReadLineAsync().ConfigureAwait(false);
            if (physicalLine == null)
            {
                break;
            }

            if (lineBuilder.Length > 0)
            {
                lineBuilder.AppendLine(); // Preserve newline within quoted field
            }

            lineBuilder.Append(physicalLine);

            // Count quotes in this line to determine if we're inside a quoted field
            for (int i = 0; i < physicalLine.Length; i++)
            {
                if (physicalLine[i] == '"')
                {
                    // Check if this is an escaped quote ("")
                    if (i + 1 < physicalLine.Length && physicalLine[i + 1] == '"')
                    {
                        i++; // Skip the escaped quote
                    }
                    else
                    {
                        inQuotes = !inQuotes; // Toggle quote state
                    }
                }
            }

            // If we're not inside quotes, this CSV record is complete
            if (!inQuotes)
            {
                break;
            }
        }

        return lineBuilder.Length > 0 ? lineBuilder.ToString() : null;
    }

    #endregion

    #region Entry Loading

    /// <summary>
    /// Loads log entries from a CSV file asynchronously with windowing support for large files.
    /// Handles multi-line quoted CSV fields properly.
    /// </summary>
    /// <param name="filePath">Full path to the CSV log file.</param>
    /// <param name="startIndex">Starting entry index (0-based, excludes header row).</param>
    /// <param name="count">Number of entries to load (default: DefaultWindowSize).</param>
    /// <returns>List of raw CSV entry lines.</returns>
    /// <exception cref="ArgumentException">Thrown if filePath is invalid.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if file access is denied.</exception>
    /// <exception cref="FileNotFoundException">Thrown if file does not exist.</exception>
    public static async Task<List<string>> LoadEntriesAsync(
        string filePath,
        int startIndex = 0,
        int count = DefaultWindowSize)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }

        // Validate path security
        if (!Helper_LogPath.IsPathSafe(filePath))
        {
            throw new UnauthorizedAccessException($"Access denied to file outside log directory: {filePath}");
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Log file not found: {filePath}");
        }

        if (startIndex < 0)
        {
            throw new ArgumentException("Start index cannot be negative.", nameof(startIndex));
        }

        if (count <= 0)
        {
            throw new ArgumentException("Count must be greater than zero.", nameof(count));
        }

        var entries = new List<string>();

        try
        {
            await using var stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite, // Allow other processes to write while we read
                bufferSize: 4096,
                useAsync: true);

            using var reader = new StreamReader(stream, Encoding.UTF8);

            // Skip CSV header if present
            if (!reader.EndOfStream)
            {
                string? header = await ReadCsvLineAsync(reader).ConfigureAwait(false);
                if (header != null && header.StartsWith("Timestamp,", StringComparison.OrdinalIgnoreCase))
                {

                }
                else
                {
                    // Not a header, treat as first data row
                    if (startIndex == 0 && header != null)
                    {
                        entries.Add(header);
                    }
                }
            }

            int currentIndex = 0;
            int loadedCount = entries.Count; // May be 1 if we added header-as-data

            while (!reader.EndOfStream && loadedCount < count)
            {
                string? csvLine = await ReadCsvLineAsync(reader).ConfigureAwait(false);

                if (csvLine == null)
                {
                    break;
                }

                // Skip entries until we reach startIndex
                if (currentIndex < startIndex)
                {
                    currentIndex++;
                    continue;
                }

                entries.Add(csvLine);
                loadedCount++;
                currentIndex++;
            }


        }
        catch (UnauthorizedAccessException ex)
        {

            LoggingUtility.LogApplicationError(ex);
            throw;
        }
        catch (Exception ex)
        {

            LoggingUtility.LogApplicationError(ex);
            throw;
        }

        return entries;
    }

    /// <summary>
    /// Gets the total number of log entries (data rows, excluding header) in a CSV file asynchronously.
    /// </summary>
    /// <param name="filePath">Full path to the CSV log file.</param>
    /// <returns>Total number of data entries in the file.</returns>
    /// <exception cref="ArgumentException">Thrown if filePath is invalid.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if file access is denied.</exception>
    /// <exception cref="FileNotFoundException">Thrown if file does not exist.</exception>
    public static async Task<int> GetTotalEntryCountAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }

        // Validate path security
        if (!Helper_LogPath.IsPathSafe(filePath))
        {
            throw new UnauthorizedAccessException($"Access denied to file outside log directory: {filePath}");
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Log file not found: {filePath}");
        }

        int entryCount = 0;

        try
        {
            await using var stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite,
                bufferSize: 4096,
                useAsync: true);

            using var reader = new StreamReader(stream, Encoding.UTF8);

            // Skip CSV header if present
            bool skippedHeader = false;
            if (!reader.EndOfStream)
            {
                string? firstLine = await ReadCsvLineAsync(reader).ConfigureAwait(false);
                if (firstLine != null && firstLine.StartsWith("Timestamp,", StringComparison.OrdinalIgnoreCase))
                {
                    skippedHeader = true;
                }
                else
                {
                    // Not a header, count it as data
                    entryCount = 1;
                }
            }

            while (!reader.EndOfStream)
            {
                await ReadCsvLineAsync(reader).ConfigureAwait(false);
                entryCount++;
            }


        }
        catch (Exception ex)
        {

            LoggingUtility.LogApplicationError(ex);
            throw;
        }

        return entryCount;
    }

    #endregion
}
