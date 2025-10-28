using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace MTM_Inventory_Application.Services;

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
            LoggingUtility.Log($"[Service_LogFileReader] Invalid username or directory path: {username}");
            return new List<Model_LogFile>();
        }

        // Ensure directory exists
        if (!Directory.Exists(userDirectory))
        {
            LoggingUtility.Log($"[Service_LogFileReader] Directory does not exist: {userDirectory}");
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
                            LoggingUtility.Log($"[Service_LogFileReader] Skipping unsafe path: {filePath}");
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
                            LoggingUtility.Log($"[Service_LogFileReader] Skipping empty file: {fileInfo.Name}");
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
                        LoggingUtility.Log($"[Service_LogFileReader] Error processing file: {filePath}");
                        LoggingUtility.LogApplicationError(ex);
                    }
                }
            }).ConfigureAwait(false);

            // Sort by modified date descending (most recent first)
            logFiles = logFiles.OrderByDescending(f => f.ModifiedDate).ToList();

            LoggingUtility.Log($"[Service_LogFileReader] Enumerated {logFiles.Count} log files for user: {username}");
        }
        catch (UnauthorizedAccessException ex)
        {
            LoggingUtility.Log($"[Service_LogFileReader] Access denied to directory: {userDirectory}");
            LoggingUtility.LogApplicationError(ex);
            throw;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogFileReader] Error enumerating log files for user: {username}");
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

    #region Entry Loading

    /// <summary>
    /// Loads log entries from a file asynchronously with windowing support for large files.
    /// </summary>
    /// <param name="filePath">Full path to the log file.</param>
    /// <param name="startIndex">Starting entry index (0-based).</param>
    /// <param name="count">Number of entries to load (default: DefaultWindowSize).</param>
    /// <returns>List of raw log entry lines.</returns>
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

            int currentIndex = 0;
            int loadedCount = 0;

            while (!reader.EndOfStream && loadedCount < count)
            {
                string? line = await reader.ReadLineAsync().ConfigureAwait(false);

                if (line == null)
                {
                    break;
                }

                // Skip lines until we reach startIndex
                if (currentIndex < startIndex)
                {
                    currentIndex++;
                    continue;
                }

                entries.Add(line);
                loadedCount++;
                currentIndex++;
            }

            LoggingUtility.Log($"[Service_LogFileReader] Loaded {entries.Count} entries from {Path.GetFileName(filePath)} (start: {startIndex}, requested: {count})");
        }
        catch (UnauthorizedAccessException ex)
        {
            LoggingUtility.Log($"[Service_LogFileReader] Access denied to file: {filePath}");
            LoggingUtility.LogApplicationError(ex);
            throw;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogFileReader] Error loading entries from file: {filePath}");
            LoggingUtility.LogApplicationError(ex);
            throw;
        }

        return entries;
    }

    /// <summary>
    /// Gets the total number of log entries (lines) in a file asynchronously.
    /// </summary>
    /// <param name="filePath">Full path to the log file.</param>
    /// <returns>Total number of entries in the file.</returns>
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

        int lineCount = 0;

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

            while (!reader.EndOfStream)
            {
                await reader.ReadLineAsync().ConfigureAwait(false);
                lineCount++;
            }

            LoggingUtility.Log($"[Service_LogFileReader] Counted {lineCount} entries in {Path.GetFileName(filePath)}");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogFileReader] Error counting entries in file: {filePath}");
            LoggingUtility.LogApplicationError(ex);
            throw;
        }

        return lineCount;
    }

    #endregion
}
