// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;
using DocumentFormat.OpenXml.Vml.Office;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Logging;

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

    /// <summary>
    /// Thread-local flag to prevent recursive logging in LogDatabaseError when database operations fail.
    /// </summary>
    [ThreadStatic]
    private static bool _isLoggingDatabaseError;

    #endregion

    #region LogCleanup

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
                    var logFiles = Directory.GetFiles(logDirectory, "*.log")
                        .OrderByDescending(File.GetCreationTime)
                        .ToList();

                    if (logFiles.Count > maxLogs)
                    {
                        var filesToDelete = logFiles.Skip(maxLogs).ToList();
                        foreach (var logFile in filesToDelete)
                        {
                            cts.Token.ThrowIfCancellationRequested();
                            File.Delete(logFile);
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

            // Clean up application data directories
            var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MTM_WIP_Application_Winforms");
            var localAppDataPath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MTM_WIP_Application_Winforms");

            // Run directory cleanup operations asynchronously
            await Task.Run(() =>
            {
                Service_OnStartup_AppDataCleaner.DeleteDirectoryContents(appDataPath);
                Service_OnStartup_AppDataCleaner.DeleteDirectoryContents(localAppDataPath);
            }, cts.Token);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[DEBUG] Failed to clean up old log files or application data: {ex.Message}");
            // Don't call Log() here to avoid potential recursion
        }
    }

    public static async Task CleanUpOldLogsIfNeededAsync()
    {
        if (!string.IsNullOrEmpty(_logDirectory))
            await CleanUpOldLogsAsync(_logDirectory, 20);
    }

    #endregion

    #region LogFileWriting

    private static void FlushLogEntryToDisk(string filePath, string logEntry)
    {
        try
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                _ = Task.Run(async () =>
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
                            lock (LogLock)
                            {
                                if (!_filesWithHeaders.Contains(filePath) && !File.Exists(filePath))
                                {
                                    needsHeader = true;
                                    _filesWithHeaders.Add(filePath);
                                }
                            }

                            // Use FileStream with FileShare.Write to allow multiple processes to write
                            await using var fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Write);
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
                });
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[DEBUG] Failed to initiate log write operation: {ex.Message}");
        }
    }

    #endregion

    #region Initialization

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
                logFilePath = await Helper_Database_Variables.GetLogFilePathAsync(server, userName);
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

        // If field contains comma, newline, or quote, wrap in quotes and escape internal quotes
        if (field.Contains(',') || field.Contains('\n') || field.Contains('"'))
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

    private static void OnProcessExit(object? sender, EventArgs e)
    {
        var shutdownMsg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Application exiting.";
        lock (LogLock)
        {
            FlushLogEntryToDisk(_normalLogFile, shutdownMsg);
            FlushLogEntryToDisk(_dbErrorLogFile, shutdownMsg);
            FlushLogEntryToDisk(_appErrorLogFile, shutdownMsg);
        }
    }

    #endregion
}

#endregion
