// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;
using System.Collections.Concurrent;
using DocumentFormat.OpenXml.Vml.Office;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Services.Logging;

#region LoggingUtility

internal static class LoggingUtility
{
    #region Fields

    private static string _appErrorLogFile = string.Empty;
    private static string _dbErrorLogFile = string.Empty;
    private static string _logDirectory = string.Empty;
    private static string _normalLogFile = string.Empty;
    private static readonly Lock LogLock = new();
    private static readonly HashSet<string> _filesWithHeaders = new();
    private static readonly BlockingCollection<(string FilePath, string LogEntry)> _logQueue = new();

    /// <summary>
    /// Thread-local flag to prevent recursive logging in LogDatabaseError when database operations fail.
    /// </summary>
    [ThreadStatic]
    private static bool _isLoggingDatabaseError;

    #endregion

    #region LogCleanup

/// <summary>
/// The CleanUpOldLogsAsync function cleans up old log files and application data directories in a
/// specified directory with a maximum number of logs to keep.
/// </summary>
/// <param name="logDirectory">The `logDirectory` parameter in the `CleanUpOldLogsAsync` method is the
/// directory path where log files are stored. This method is responsible for cleaning up old log files
/// in the specified directory based on the provided criteria.</param>
/// <param name="maxLogs">The `maxLogs` parameter in the `CleanUpOldLogsAsync` method specifies the
/// maximum number of log files that should be retained in the `logDirectory`. If the number of log
/// files in the directory exceeds this value, the method will delete the oldest log files to ensure
/// that only the most</param>
/// <returns>
/// The `CleanUpOldLogsAsync` method returns a `Task` representing the asynchronous operation of
/// cleaning up old log files and application data directories.
/// </returns>
    private static async Task CleanUpOldLogsAsync(string logDirectory, int maxLogs)
    {
        try
        {
            // Run the file operations on a background thread with proper timeout
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await Task.Run(() =>
            {
                try
                {
                    // Clean up both .log (legacy) and .csv (current) files
                    var logFiles = Directory.GetFiles(logDirectory, "*.*")
                        .Where(f => f.EndsWith(".log", StringComparison.OrdinalIgnoreCase) ||
                                   f.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(File.GetCreationTime)
                        .ToList();

                    if (logFiles.Count > maxLogs)
                    {
                        var filesToDelete = logFiles.Skip(maxLogs).ToList();
                        foreach (var logFile in filesToDelete)
                        {
                            cts.Token.ThrowIfCancellationRequested();
                            try
                            {
                                File.Delete(logFile);
                                Debug.WriteLine($"[DEBUG] Deleted old log file: {logFile}");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"[DEBUG] Failed to delete log file {logFile}: {ex.Message}");
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("[DEBUG] Log cleanup timed out");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[DEBUG] Error during log file cleanup: {ex.Message}");
                }
            }, cts.Token);

            if (Debugger.IsAttached) return;

            // Application data cleanup is now handled by the Orchestrator (Service_OnStartup_AppLifecycle)
            // to avoid circular dependencies.
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[DEBUG] Failed to clean up old log files or application data: {ex.Message}");
            // Don't call Log() here to avoid potential recursion
        }
    }

/// <summary>
/// The function `CleanUpOldLogsIfNeededAsync` checks if a log directory is not empty and then calls
/// `CleanUpOldLogsAsync` to clean up old logs.
/// </summary>
    public static async Task CleanUpOldLogsIfNeededAsync()
    {
        if (!string.IsNullOrEmpty(_logDirectory))
            await CleanUpOldLogsAsync(_logDirectory, 20);
    }

    #endregion

    #region LogFileWriting

/// <summary>
/// The FlushLogEntryToDisk function asynchronously writes a log entry to a file, handling retries in
/// case of IOException and logging any exceptions.
/// </summary>
/// <param name="filePath">The `filePath` parameter in the `FlushLogEntryToDisk` method is the path to
/// the file where the log entry will be written. It specifies the location on disk where the log
/// information will be stored.</param>
/// <param name="logEntry">The `logEntry` parameter in the `FlushLogEntryToDisk` method represents the
/// log entry that needs to be written to the specified file. This log entry typically contains
/// information such as timestamp, log level, source, message, and details of the event that occurred.
/// The method attempts to write</param>
    private static void FlushLogEntryToDisk(string filePath, string logEntry)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            _logQueue.Add((filePath, logEntry));
        }
    }

    private static async Task ProcessLogQueue()
    {
        foreach (var (filePath, logEntry) in _logQueue.GetConsumingEnumerable())
        {
            await WriteLogEntryToFileAsync(filePath, logEntry);
        }
    }

    private static async Task WriteLogEntryToFileAsync(string filePath, string logEntry)
    {
        const int maxRetries = 5;
        const int delayMs = 100;
        int attempt = 0;
        while (true)
        {
            try
            {
                // Check if we need to write CSV header
                bool needsHeader = false;
                
                if (!_filesWithHeaders.Contains(filePath) && !File.Exists(filePath))
                {
                    needsHeader = true;
                    _filesWithHeaders.Add(filePath);
                }

                // Use FileStream with FileShare.ReadWrite to allow multiple processes to write/read
                await using var fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                await using var writer = new StreamWriter(fs);

                // Write CSV header if this is a new file
                if (needsHeader)
                {
                    await writer.WriteLineAsync("Timestamp,Level,Source,Message,Details");
                }

                await writer.WriteLineAsync(logEntry);
                break; // Success
            }
            catch (IOException) when (attempt < maxRetries)
            {
                attempt++;
                await Task.Delay(delayMs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DEBUG] Failed to write log entry to file: {ex.Message}");
                break;
            }
        }
    }

    #endregion

    #region Initialization

/// <summary>
/// The InitializeLoggingAsync function initializes logging, handles timeouts, and sets up log file
/// paths with fallback options.
/// </summary>
    public static async Task InitializeLoggingAsync()
    {
        try
        {
            Debug.WriteLine("[DEBUG] Starting logging initialization...");

            var server = new MySqlConnectionStringBuilder(Model_Application_Variables.ConnectionString).Server;
            var userName = Model_Application_Variables.User;

            Debug.WriteLine($"[DEBUG] Server: {server}, User: {userName}");

            // Add timeout for log path operations with proper async pattern
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            string logFilePath;
            try
            {
                logFilePath = await Helper_LogPath.GetLogFilePathAsync(server, userName);
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("[DEBUG] Log path creation timed out, using fallback");
                // Fallback to CommonApplicationData directory (matches Helper_LogPath fallback location)
                var fallbackDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "MTM_WIP_Application_Winforms",
                    "Logs",
                    userName);
                Directory.CreateDirectory(fallbackDir);
                var timestamp = DateTime.Now.ToString("MM-dd-yyyy @ h-mm tt");
                logFilePath = Path.Combine(fallbackDir, $"{userName} {timestamp}.csv");
            }

            _logDirectory = Path.GetDirectoryName(logFilePath) ?? "";
            var baseFileName = Path.GetFileNameWithoutExtension(logFilePath);
            _normalLogFile = Path.Combine(_logDirectory, $"{baseFileName}_normal.csv");
            _dbErrorLogFile = Path.Combine(_logDirectory, $"{baseFileName}_db_error.csv");
            _appErrorLogFile = Path.Combine(_logDirectory, $"{baseFileName}_app_error.csv");

            Debug.WriteLine($"[DEBUG] Log directory: {_logDirectory}");
            Debug.WriteLine($"[DEBUG] Normal log file: {_normalLogFile}");

            // Start background log processor
            _ = Task.Run(ProcessLogQueue);

            Log("Initializing logging...");
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            Debug.WriteLine("[DEBUG] Logging initialization completed");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[DEBUG] Error during logging initialization: {ex.Message}");
            // Create fallback logging to CommonApplicationData directory (matches Helper_LogPath fallback location)
            try
            {
                var fallbackDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "MTM_WIP_Application_Winforms",
                    "Logs");
                Directory.CreateDirectory(fallbackDir);
                var timestamp = DateTime.Now.ToString("MM-dd-yyyy @ h-mm tt");
                var fallbackFile = Path.Combine(fallbackDir, $"fallback_{timestamp}.csv");
                _logDirectory = fallbackDir;
                _normalLogFile = fallbackFile;
                _dbErrorLogFile = fallbackFile;
                _appErrorLogFile = fallbackFile;
                Debug.WriteLine($"[DEBUG] Using fallback logging to: {fallbackDir}");
            }
            catch (Exception fallbackEx)
            {
                Debug.WriteLine($"[DEBUG] Fallback logging also failed: {fallbackEx.Message}");
                // If even fallback fails, disable logging
                _logDirectory = "";
                _normalLogFile = "";
                _dbErrorLogFile = "";
                _appErrorLogFile = "";
            }
        }
    }

    #endregion

    #region CSV Formatting Helper

    /// <summary>
    /// Escapes a CSV field value by wrapping in quotes and escaping internal quotes.
    /// </summary>
    private static string EscapeCsvField(string? field)
    {
        if (string.IsNullOrEmpty(field))
        {
            return string.Empty;
        }

        // If field contains comma, newline, carriage return, or quote, wrap in quotes and escape internal quotes
        if (field.Contains(',') || field.Contains('\n') || field.Contains('\r') || field.Contains('"'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }

        return field;
    }

    /// <summary>
    /// Formats a CSV log entry with proper field escaping.
    /// CSV Format: Timestamp,Level,Source,Message,Details
    /// </summary>
    private static string FormatCsvEntry(DateTime timestamp, string level, string source, string message, string? details = null)
    {
        var timestampStr = timestamp.ToString("yyyy-MM-dd HH:mm:ss");
        var csvLine = $"{timestampStr},{EscapeCsvField(level)},{EscapeCsvField(source)},{EscapeCsvField(message)},{EscapeCsvField(details)}";
        return csvLine;
    }

    #endregion

    #region LoggingMethods

    /// <summary>
    /// The Log function in C# is used to output a message to a log file or console.
    /// </summary>
    /// <param name="message">The `message` parameter in the `Log` method is a string that represents the
    /// information or data that you want to log or output. When you call this method, you would pass a
    /// string message as an argument to be logged or displayed.</param>
    public static void Log(string message)
    {
        var timestamp = DateTime.Now;
        var csvEntry = FormatCsvEntry(timestamp, "INFO", "Application", message, null);

        // Output to Debug console (visible in Output window when debugging)
        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - {message}");

        lock (LogLock)
        {
            FlushLogEntryToDisk(_normalLogFile, csvEntry);
        }
    }

    /// <summary>
    /// Logs a message with a specific severity level, source, and optional details/exception.
    /// </summary>
    /// <param name="level">The severity level.</param>
    /// <param name="source">The source component.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="details">Optional details.</param>
    /// <param name="ex">Optional exception.</param>
    public static void Log(Enum_LogLevel level, string source, string message, string? details = null, Exception? ex = null)
    {
        var timestamp = DateTime.Now;
        string levelStr = level.ToString().ToUpper();
        
        string fullDetails = details ?? string.Empty;
        if (ex != null)
        {
            fullDetails += $"\nException: {ex.Message}\nStack Trace: {ex.StackTrace}";
        }

        var csvEntry = FormatCsvEntry(timestamp, levelStr, source, message, string.IsNullOrWhiteSpace(fullDetails) ? null : fullDetails);

        // Output to Debug console
        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - [{levelStr}] {source} - {message}");
        if (ex != null)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

        lock (LogLock)
        {
            // Route errors to app error log if it's an Error or Critical
            if (level == Enum_LogLevel.Error || level == Enum_LogLevel.Critical)
            {
                FlushLogEntryToDisk(_appErrorLogFile, csvEntry);
            }
            else
            {
                FlushLogEntryToDisk(_normalLogFile, csvEntry);
            }
        }
    }/// <summary>
/// This C# function logs application errors along with the exception details.
/// </summary>
/// <param name="Exception">The parameter `ex` in the `LogApplicationError` method is of type
/// `Exception`. This means that the method expects an object of type `Exception` to be passed as an
/// argument when it is called. This allows the method to handle and log any exceptions that occur
/// within the application.</param>
    public static void LogApplicationError(Exception ex)
    {
        var timestamp = DateTime.Now;
        var source = ex.Source ?? "Application";
        var details = $"Type: {ex.GetType().Name}\nStack Trace: {ex.StackTrace}";
        var csvEntry = FormatCsvEntry(timestamp, "ERROR", source, ex.Message, details);

        // Output to Debug console (visible in Output window when debugging)
        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Application Error - {ex.Message}");
        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Stack Trace - {ex.StackTrace}");

        lock (LogLock)
        {
            FlushLogEntryToDisk(_appErrorLogFile, csvEntry);
        }
    }

/// <summary>
/// The function `LogDatabaseError` logs database errors with an optional severity level parameter.
/// </summary>
/// <param name="Exception">The `Exception ex` parameter in the `LogDatabaseError` method is used to
/// pass an exception object that represents the error or exception that occurred in the database
/// operation. This allows the method to log details about the error for debugging and troubleshooting
/// purposes.</param>
/// <param name="Enum_DatabaseEnum_ErrorSeverity">The Enum_DatabaseEnum_ErrorSeverity is an enumeration
/// that defines different levels of error severity that can be associated with a database error. It
/// likely contains values such as "Error", "Warning", "Information", etc., to categorize the severity
/// of the error that occurred in the database.</param>
    public static void LogDatabaseError(Exception ex, Enum_DatabaseEnum_ErrorSeverity severity = Enum_DatabaseEnum_ErrorSeverity.Error)
    {
        // Prevent recursive logging if database operation called from logging itself fails
        if (_isLoggingDatabaseError)
        {
            // Fallback to Debug output to avoid infinite recursion
            Debug.WriteLine($"[DEBUG] Database error during logging (recursion prevented): {ex.Message}");

            // Try direct file logging as last resort
            try
            {
                var timestamp = DateTime.Now;
                var fallbackCsv = FormatCsvEntry(timestamp, "ERROR", "Database (Fallback)", ex.Message, null);
                if (!string.IsNullOrEmpty(_dbErrorLogFile))
                {
                    File.AppendAllText(_dbErrorLogFile, fallbackCsv + Environment.NewLine);
                }
            }
            catch
            {
                // Silently fail - we're already in error recovery mode
            }
            return;
        }

        try
        {
            _isLoggingDatabaseError = true;

            var timestamp = DateTime.Now;
            var severityLabel = severity switch
            {
                Enum_DatabaseEnum_ErrorSeverity.Warning => "WARNING",
                Enum_DatabaseEnum_ErrorSeverity.Error => "ERROR",
                Enum_DatabaseEnum_ErrorSeverity.Critical => "CRITICAL",
                _ => "ERROR"
            };

            var source = ex.Source ?? "Database";
            var details = $"Type: {ex.GetType().Name}\nStack Trace: {ex.StackTrace}";
            var csvEntry = FormatCsvEntry(timestamp, severityLabel, source, ex.Message, details);

            // Output to Debug console (visible in Output window when debugging)
            Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Database Error [{severityLabel}] - {ex.Message}");
            Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Stack Trace - {ex.StackTrace}");

            lock (LogLock)
            {
                FlushLogEntryToDisk(_dbErrorLogFile, csvEntry);
            }
        }
        finally
        {
            _isLoggingDatabaseError = false;
        }
    }

/// <summary>
/// The function LogApplicationInfo logs the provided message as application information.
/// </summary>
/// <param name="message">The `message` parameter in the `LogApplicationInfo` method is a string that
/// represents the information or message that you want to log or record in the application. This
/// message could be any relevant information that you want to track for debugging, monitoring, or
/// auditing purposes within your application.</param>
    public static void LogApplicationInfo(string message)
    {
        var timestamp = DateTime.Now;
        var csvEntry = FormatCsvEntry(timestamp, "INFO", "Application", message, null);

        // Output to Debug console (visible in Output window when debugging)
        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Application Info - {message}");

        lock (LogLock)
        {
            FlushLogEntryToDisk(_normalLogFile, csvEntry);
        }
    }

    #endregion

    #region Shutdown

/// <summary>
/// The above function is a private static method in C# that handles the process exit event.
/// </summary>
/// <param name="sender">The `sender` parameter in the `OnProcessExit` method refers to the object that
/// raised the event. In this case, it would typically be the object that triggered the process exit
/// event.</param>
/// <param name="EventArgs">The EventArgs class is the base class for classes containing event data. It
/// provides a parameterless constructor and is typically used when no additional data needs to be
/// passed to an event handler. In the context of your code snippet, the EventArgs parameter in the
/// OnProcessExit method represents the event data associated with the</param>
    private static void OnProcessExit(object? sender, EventArgs e)
    {
        var timestamp = DateTime.Now;
        var shutdownMsg = "Application exiting.";
        
        var csvEntry = FormatCsvEntry(timestamp, "INFO", "Application", shutdownMsg, null);
        
        lock (LogLock)
        {
            FlushLogEntryToDisk(_normalLogFile, csvEntry);
            FlushLogEntryToDisk(_dbErrorLogFile, csvEntry);
            FlushLogEntryToDisk(_appErrorLogFile, csvEntry);
        }
    }

    #endregion
}

#endregion
