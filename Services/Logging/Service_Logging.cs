using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Vml.Office;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Services.Logging;

/// <summary>
/// Implementation of ILoggingService using CSV files.
/// </summary>
public class Service_Logging : ILoggingService
{
    #region Fields

    private string _appErrorLogFile = string.Empty;
    private string _dbErrorLogFile = string.Empty;
    private string _logDirectory = string.Empty;
    private string _normalLogFile = string.Empty;
    private readonly object _logLock = new();
    private readonly HashSet<string> _filesWithHeaders = new();
    private readonly BlockingCollection<(string FilePath, string LogEntry)> _logQueue = new();
    
    // Static instance for backward compatibility
    private static ILoggingService? _instance;

    /// <summary>
    /// Thread-local flag to prevent recursive logging in LogDatabaseError when database operations fail.
    /// </summary>
    [ThreadStatic]
    private static bool _isLoggingDatabaseError;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the logging service.
    /// Set by DI container during startup.
    /// </summary>
    public static ILoggingService Instance 
    {
        get => _instance ?? throw new InvalidOperationException(
            "Logging service not initialized. Ensure DI container is configured.");
        internal set => _instance = value;
    }

    #endregion

    #region Constructor

    public Service_Logging()
    {
        // Start background log processor
        _ = Task.Run(ProcessLogQueue);
        AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
    }

    #endregion

    #region Initialization and Cleanup

    public async Task InitializeAsync()
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
                // Fallback to CommonApplicationData directory
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

            Log("Initializing logging...");

            Debug.WriteLine("[DEBUG] Logging initialization completed");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[DEBUG] Error during logging initialization: {ex.Message}");
            // Create fallback logging
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
                _logDirectory = "";
                _normalLogFile = "";
                _dbErrorLogFile = "";
                _appErrorLogFile = "";
            }
        }
    }

    public async Task CleanUpOldLogsAsync(int retentionDays = 30)
    {
        if (string.IsNullOrEmpty(_logDirectory)) return;

        try
        {
            // Run the file operations on a background thread with proper timeout
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await Task.Run(() =>
            {
                try
                {
                    // Clean up both .log (legacy) and .csv (current) files
                    var logFiles = Directory.GetFiles(_logDirectory, "*.*")
                        .Where(f => f.EndsWith(".log", StringComparison.OrdinalIgnoreCase) ||
                                   f.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(File.GetCreationTime)
                        .ToList();

                    var cutoffDate = DateTime.Now.AddDays(-retentionDays);
                    var filesToDelete = logFiles.Where(f => File.GetCreationTime(f) < cutoffDate).ToList();

                    // Also enforce max count of 50 to prevent buildup
                    if (logFiles.Count > 50)
                    {
                        filesToDelete.AddRange(logFiles.Skip(50));
                    }
                    
                    filesToDelete = filesToDelete.Distinct().ToList();

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
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("[DEBUG] Log cleanup timed out");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[DEBUG] Error during log file cleanup: {ex.Message}");
                }
            }, cts.Token);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[DEBUG] Failed to clean up old log files: {ex.Message}");
        }
    }

    #endregion

    #region Core Logging Methods

    public void Log(string message)
    {
        var timestamp = DateTime.Now;
        var csvEntry = FormatCsvEntry(timestamp, "INFO", "Application", message, null);

        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - {message}");

        lock (_logLock)
        {
            FlushLogEntryToDisk(_normalLogFile, csvEntry);
        }
    }

    public void Log(Enum_LogLevel level, string source, string message, string? user = null, Exception? exception = null)
    {
        var timestamp = DateTime.Now;
        string levelStr = level.ToString().ToUpper();
        
        string fullDetails = string.Empty;
        if (exception != null)
        {
            fullDetails += $"\nException: {exception.Message}\nStack Trace: {exception.StackTrace}";
        }
        if (!string.IsNullOrEmpty(user))
        {
            fullDetails += $"\nUser: {user}";
        }

        var csvEntry = FormatCsvEntry(timestamp, levelStr, source, message, string.IsNullOrWhiteSpace(fullDetails) ? null : fullDetails);

        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - [{levelStr}] {source} - {message}");
        if (exception != null)
        {
            Debug.WriteLine($"Exception: {exception.Message}");
        }

        lock (_logLock)
        {
            if (level == Enum_LogLevel.Error || level == Enum_LogLevel.Critical)
            {
                FlushLogEntryToDisk(_appErrorLogFile, csvEntry);
            }
            else
            {
                FlushLogEntryToDisk(_normalLogFile, csvEntry);
            }
        }
    }

    #endregion

    #region Specialized Logging Methods

    public void LogApplicationError(Exception ex, string? additionalMessage = null)
    {
        var timestamp = DateTime.Now;
        var source = ex.Source ?? "Application";
        var details = $"Type: {ex.GetType().Name}\nStack Trace: {ex.StackTrace}";
        if (!string.IsNullOrEmpty(additionalMessage))
        {
            details = $"Message: {additionalMessage}\n{details}";
        }
        
        var csvEntry = FormatCsvEntry(timestamp, "ERROR", source, ex.Message, details);

        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Application Error - {ex.Message}");
        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Stack Trace - {ex.StackTrace}");

        lock (_logLock)
        {
            FlushLogEntryToDisk(_appErrorLogFile, csvEntry);
        }
    }

    public void LogDatabaseError(Exception ex, Enum_DatabaseEnum_ErrorSeverity severity = Enum_DatabaseEnum_ErrorSeverity.Error)
    {
        if (_isLoggingDatabaseError)
        {
            Debug.WriteLine($"[DEBUG] Database error during logging (recursion prevented): {ex.Message}");
            try
            {
                var timestamp = DateTime.Now;
                var fallbackCsv = FormatCsvEntry(timestamp, "ERROR", "Database (Fallback)", ex.Message, null);
                if (!string.IsNullOrEmpty(_dbErrorLogFile))
                {
                    File.AppendAllText(_dbErrorLogFile, fallbackCsv + Environment.NewLine);
                }
            }
            catch { }
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

            Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Database Error [{severityLabel}] - {ex.Message}");
            Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Stack Trace - {ex.StackTrace}");

            lock (_logLock)
            {
                FlushLogEntryToDisk(_dbErrorLogFile, csvEntry);
            }
        }
        finally
        {
            _isLoggingDatabaseError = false;
        }
    }

    public void LogApplicationInfo(string message)
    {
        var timestamp = DateTime.Now;
        var csvEntry = FormatCsvEntry(timestamp, "INFO", "Application", message, null);

        Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - Application Info - {message}");

        lock (_logLock)
        {
            FlushLogEntryToDisk(_normalLogFile, csvEntry);
        }
    }

    #endregion

    #region Helper Methods

    private void FlushLogEntryToDisk(string filePath, string logEntry)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            _logQueue.Add((filePath, logEntry));
        }
    }

    private async Task ProcessLogQueue()
    {
        foreach (var (filePath, logEntry) in _logQueue.GetConsumingEnumerable())
        {
            await WriteLogEntryToFileAsync(filePath, logEntry);
        }
    }

    private async Task WriteLogEntryToFileAsync(string filePath, string logEntry)
    {
        const int maxRetries = 5;
        const int delayMs = 100;
        int attempt = 0;
        while (true)
        {
            try
            {
                bool needsHeader = false;
                
                if (!_filesWithHeaders.Contains(filePath) && !File.Exists(filePath))
                {
                    needsHeader = true;
                    _filesWithHeaders.Add(filePath);
                }

                await using var fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                await using var writer = new StreamWriter(fs);

                if (needsHeader)
                {
                    await writer.WriteLineAsync("Timestamp,Level,Source,Message,Details");
                }

                await writer.WriteLineAsync(logEntry);
                break;
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

    private string EscapeCsvField(string? field)
    {
        if (string.IsNullOrEmpty(field))
        {
            return string.Empty;
        }

        if (field.Contains(',') || field.Contains('\n') || field.Contains('\r') || field.Contains('"'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }

        return field;
    }

    private string FormatCsvEntry(DateTime timestamp, string level, string source, string message, string? details = null)
    {
        var timestampStr = timestamp.ToString("yyyy-MM-dd HH:mm:ss");
        var csvLine = $"{timestampStr},{EscapeCsvField(level)},{EscapeCsvField(source)},{EscapeCsvField(message)},{EscapeCsvField(details)}";
        return csvLine;
    }

    private void OnProcessExit(object? sender, EventArgs e)
    {
        var timestamp = DateTime.Now;
        var shutdownMsg = "Application exiting.";
        
        var csvEntry = FormatCsvEntry(timestamp, "INFO", "Application", shutdownMsg, null);
        
        lock (_logLock)
        {
            FlushLogEntryToDisk(_normalLogFile, csvEntry);
            FlushLogEntryToDisk(_dbErrorLogFile, csvEntry);
            FlushLogEntryToDisk(_appErrorLogFile, csvEntry);
        }
    }

    #endregion
}
