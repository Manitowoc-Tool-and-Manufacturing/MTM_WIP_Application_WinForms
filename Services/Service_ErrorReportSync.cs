using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MTM_WIP_Application_WinForms.Models;
using MySql.Data.MySqlClient;

namespace MTM_Inventory_Application.Services;

/// <summary>
/// Service for synchronizing queued error reports with the database.
/// Handles startup sync, manual sync, and cleanup of old reports.
/// </summary>
internal static class Service_ErrorReportSync
{
    #region Fields

    private static readonly SemaphoreSlim _syncLock = new SemaphoreSlim(1, 1);
    private static readonly string QueueDirectory = Model_AppVariables.ErrorReporting.QueueDirectory;
    private static readonly string ArchiveDirectory = Model_AppVariables.ErrorReporting.ArchiveDirectory;

    #endregion

    #region Sync Operations

    /// <summary>
    /// Synchronizes pending error reports on application startup.
    /// Non-blocking operation that processes queued reports if database is available.
    /// </summary>
    /// <returns>DaoResult containing count of successfully synced reports.</returns>
    public static async Task<DaoResult<int>> SyncOnStartupAsync()
    {
        // Try to acquire lock immediately without waiting
        bool lockAcquired = await _syncLock.WaitAsync(0);
        
        if (!lockAcquired)
        {
            LoggingUtility.Log("[Service_ErrorReportSync] Sync already in progress, skipping startup sync");
            return DaoResult<int>.Failure("Sync operation already in progress");
        }

        try
        {
            // Check database connectivity
            if (!await IsDatabaseAvailableAsync())
            {
                LoggingUtility.Log("[Service_ErrorReportSync] Database unavailable, skipping startup sync");
                return DaoResult<int>.Success(0, "Database unavailable - sync deferred");
            }

            // Process pending files
            int successCount = await ProcessPendingFilesAsync();

            LoggingUtility.Log($"[Service_ErrorReportSync] Startup sync completed: {successCount} reports submitted");

            return DaoResult<int>.Success(
                successCount,
                successCount > 0 
                    ? $"Successfully submitted {successCount} pending error report(s)"
                    : "No pending error reports to sync");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<int>.Failure("Error during startup sync", ex);
        }
        finally
        {
            _syncLock.Release();
        }
    }

    /// <summary>
    /// Manually triggers synchronization of pending error reports.
    /// Used from Developer Settings menu.
    /// </summary>
    /// <returns>DaoResult containing count of successfully synced reports.</returns>
    public static async Task<DaoResult<int>> SyncManuallyAsync()
    {
        // Try to acquire lock immediately
        bool lockAcquired = await _syncLock.WaitAsync(0);
        
        if (!lockAcquired)
        {
            return DaoResult<int>.Failure("Another sync operation is already in progress. Please wait and try again.");
        }

        try
        {
            // Check database connectivity
            if (!await IsDatabaseAvailableAsync())
            {
                return DaoResult<int>.Failure("Database is not currently available. Please check your connection and try again.");
            }

            // Get pending count for progress indication
            int pendingCount = GetPendingReportCount();
            
            if (pendingCount == 0)
            {
                return DaoResult<int>.Success(0, "No pending error reports to sync");
            }

            // Show progress indicator if count exceeds threshold
            bool showProgress = pendingCount > Model_AppVariables.ErrorReporting.SyncProgressThreshold;
            
            if (showProgress)
            {
                LoggingUtility.Log($"[Service_ErrorReportSync] Starting manual sync of {pendingCount} reports (progress will be shown)");
            }

            // Process pending files
            int successCount = await ProcessPendingFilesAsync();

            LoggingUtility.Log($"[Service_ErrorReportSync] Manual sync completed: {successCount} of {pendingCount} reports submitted");

            return DaoResult<int>.Success(
                successCount,
                $"Successfully submitted {successCount} of {pendingCount} pending error report(s)");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<int>.Failure("Error during manual sync operation", ex);
        }
        finally
        {
            _syncLock.Release();
        }
    }

    /// <summary>
    /// Gets the count of pending error reports waiting to be synchronized.
    /// </summary>
    /// <returns>Number of .sql files in the pending queue.</returns>
    public static int GetPendingReportCount()
    {
        try
        {
            if (!Directory.Exists(QueueDirectory))
            {
                return 0;
            }

            return Directory.GetFiles(QueueDirectory, "*.sql").Length;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_ErrorReportSync] Error getting pending count: {ex.Message}");
            return 0;
        }
    }

    #endregion

    #region File Processing

    /// <summary>
    /// Processes all pending SQL files in chronological order.
    /// Files are processed sequentially to avoid database lock conflicts.
    /// </summary>
    /// <returns>Count of successfully processed files.</returns>
    private static async Task<int> ProcessPendingFilesAsync()
    {
        if (!Directory.Exists(QueueDirectory))
        {
            return 0;
        }

        // Get all SQL files ordered by creation time (chronological processing)
        var sqlFiles = Directory.GetFiles(QueueDirectory, "*.sql")
            .OrderBy(f => File.GetCreationTime(f))
            .ToList();

        if (sqlFiles.Count == 0)
        {
            return 0;
        }

        int successCount = 0;
        int failureCount = 0;

        foreach (string filePath in sqlFiles)
        {
            bool success = await ExecuteSqlFileAsync(filePath);
            
            if (success)
            {
                successCount++;
            }
            else
            {
                failureCount++;
            }
        }

        LoggingUtility.Log($"[Service_ErrorReportSync] Queue sync complete: {successCount} success, {failureCount} failures");

        return successCount;
    }

    /// <summary>
    /// Executes a single queued SQL file.
    /// Implements idempotent check to prevent duplicate submissions.
    /// </summary>
    /// <param name="filePath">Full path to the SQL file.</param>
    /// <returns>True if file was successfully executed and archived, false otherwise.</returns>
    private static async Task<bool> ExecuteSqlFileAsync(string filePath)
    {
        try
        {
            // Read SQL file content
            string sqlContent = await File.ReadAllTextAsync(filePath);

            // Parse file info for idempotent check
            var (userName, timestamp) = ParseFileInfo(filePath);

            // Check if report already exists (idempotent operation)
            if (await ReportExistsAsync(userName, timestamp))
            {
                LoggingUtility.Log($"[Service_ErrorReportSync] Report already exists, skipping: {Path.GetFileName(filePath)}");
                
                // Move to archive to prevent reprocessing
                MoveToArchive(filePath);
                return true;
            }

            // Execute SQL file
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
            
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlContent;
                    command.CommandTimeout = Model_AppVariables.CommandTimeoutSeconds;
                    
                    await command.ExecuteNonQueryAsync();
                }
            }

            // Success - move to archive
            MoveToArchive(filePath);
            
            LoggingUtility.Log($"[Service_ErrorReportSync] Successfully processed: {Path.GetFileName(filePath)}");
            
            return true;
        }
        catch (MySqlException ex)
        {
            LoggingUtility.LogApplicationError(ex);
            
            // SQL execution failed - mark as corrupt
            HandleCorruptFile(filePath, ex);
            
            return false;
        }
        catch (IOException ex)
        {
            // File move failure - log but leave in pending for retry
            LoggingUtility.Log($"[Service_ErrorReportSync] File operation failed for {Path.GetFileName(filePath)}: {ex.Message}");
            
            return false;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            
            // Unexpected error - mark as corrupt
            HandleCorruptFile(filePath, ex);
            
            return false;
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Checks if an error report already exists in the database.
    /// Implements 1-second tolerance for timestamp matching to handle rounding.
    /// </summary>
    /// <param name="userName">Username from the queued report.</param>
    /// <param name="reportDate">Timestamp from the queued report filename.</param>
    /// <returns>True if matching report exists, false otherwise.</returns>
    private static async Task<bool> ReportExistsAsync(string userName, DateTime reportDate)
    {
        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
            
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                
                using (var command = connection.CreateCommand())
                {
                    // Query with 1-second tolerance for timestamp matching
                    command.CommandText = @"
                        SELECT COUNT(*) 
                        FROM error_reports 
                        WHERE UserName = @userName 
                        AND ABS(TIMESTAMPDIFF(SECOND, ReportDate, @reportDate)) <= 1";
                    
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@reportDate", reportDate);
                    
                    long count = (long)await command.ExecuteScalarAsync();
                    
                    return count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_ErrorReportSync] Error checking for duplicate report: {ex.Message}");
            
            // On error, assume report doesn't exist to allow retry
            return false;
        }
    }

    /// <summary>
    /// Checks if the database is available for connections.
    /// </summary>
    /// <returns>True if database is reachable, false otherwise.</returns>
    private static async Task<bool> IsDatabaseAvailableAsync()
    {
        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
            
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT 1";
                    await command.ExecuteScalarAsync();
                }
                
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Handles a corrupt SQL file by renaming it with .corrupt extension.
    /// </summary>
    /// <param name="filePath">Path to the corrupt file.</param>
    /// <param name="ex">Exception that caused the corruption detection.</param>
    private static void HandleCorruptFile(string filePath, Exception ex)
    {
        try
        {
            string corruptPath = Path.ChangeExtension(filePath, ".corrupt");
            
            // If corrupt file already exists, append timestamp
            if (File.Exists(corruptPath))
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                corruptPath = Path.ChangeExtension(filePath, $".corrupt.{timestamp}");
            }
            
            File.Move(filePath, corruptPath);
            
            LoggingUtility.Log($"[Service_ErrorReportSync] Marked file as corrupt: {Path.GetFileName(corruptPath)}");
            LoggingUtility.LogApplicationError(ex);
        }
        catch (IOException ioEx)
        {
            LoggingUtility.Log($"[Service_ErrorReportSync] Failed to rename corrupt file {Path.GetFileName(filePath)}: {ioEx.Message}");
        }
    }

    /// <summary>
    /// Parses username and timestamp from the SQL filename.
    /// Expected format: ErrorReport_YYYYMMDD_HHMMSS_UserName_GUID.sql
    /// </summary>
    /// <param name="filePath">Full path to the SQL file.</param>
    /// <returns>Tuple containing username and timestamp.</returns>
    private static (string UserName, DateTime Timestamp) ParseFileInfo(string filePath)
    {
        try
        {
            string filename = Path.GetFileNameWithoutExtension(filePath);
            
            // Pattern: ErrorReport_YYYYMMDD_HHMMSS_UserName_GUID
            var match = Regex.Match(filename, @"ErrorReport_(\d{8})_(\d{6})_(.+?)_[a-f0-9]+$");
            
            if (match.Success)
            {
                string dateStr = match.Groups[1].Value;
                string timeStr = match.Groups[2].Value;
                string userName = match.Groups[3].Value;
                
                DateTime timestamp = DateTime.ParseExact(
                    $"{dateStr}{timeStr}",
                    "yyyyMMddHHmmss",
                    System.Globalization.CultureInfo.InvariantCulture);
                
                return (userName, timestamp);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_ErrorReportSync] Error parsing filename {Path.GetFileName(filePath)}: {ex.Message}");
        }
        
        // Return defaults on parse failure
        return ("Unknown", DateTime.UtcNow);
    }

    /// <summary>
    /// Moves a successfully processed file to the archive directory.
    /// </summary>
    /// <param name="filePath">Path to the file to archive.</param>
    private static void MoveToArchive(string filePath)
    {
        try
        {
            Directory.CreateDirectory(ArchiveDirectory);
            
            string filename = Path.GetFileName(filePath);
            string archivePath = Path.Combine(ArchiveDirectory, filename);
            
            // If file already exists in archive, append timestamp
            if (File.Exists(archivePath))
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(filename);
                string ext = Path.GetExtension(filename);
                archivePath = Path.Combine(ArchiveDirectory, $"{filenameWithoutExt}_{timestamp}{ext}");
            }
            
            File.Move(filePath, archivePath);
        }
        catch (IOException ex)
        {
            LoggingUtility.Log($"[Service_ErrorReportSync] Failed to move file to archive: {ex.Message}");
            throw; // Re-throw to indicate file move failure
        }
    }

    #endregion

    #region Cleanup

    /// <summary>
    /// Cleans up old error report files from the archive directory.
    /// Logs warnings for stale pending files but does not delete them.
    /// </summary>
    public static async Task CleanupOldReportsAsync()
    {
        try
        {
            // Clean up old archived (Sent) files
            if (Directory.Exists(ArchiveDirectory))
            {
                DateTime archiveCutoff = DateTime.Now.AddDays(-Model_AppVariables.ErrorReporting.MaxSentArchiveAgeDays);
                var oldArchiveFiles = Directory.GetFiles(ArchiveDirectory, "*.sql")
                    .Where(f => File.GetCreationTime(f) < archiveCutoff)
                    .ToList();

                int deletedCount = 0;
                foreach (string file in oldArchiveFiles)
                {
                    try
                    {
                        File.Delete(file);
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.Log($"[Service_ErrorReportSync] Failed to delete old archive file {Path.GetFileName(file)}: {ex.Message}");
                    }
                }

                if (deletedCount > 0)
                {
                    LoggingUtility.Log($"[Service_ErrorReportSync] Cleaned up {deletedCount} old archived error reports");
                }
            }

            // Check for stale pending files (log warnings, don't delete)
            if (Directory.Exists(QueueDirectory))
            {
                DateTime pendingCutoff = DateTime.Now.AddDays(-Model_AppVariables.ErrorReporting.MaxPendingAgeDays);
                var stalePendingFiles = Directory.GetFiles(QueueDirectory, "*.sql")
                    .Where(f => File.GetCreationTime(f) < pendingCutoff)
                    .ToList();

                if (stalePendingFiles.Any())
                {
                    LoggingUtility.Log($"[Service_ErrorReportSync] WARNING: {stalePendingFiles.Count} pending error reports are older than {Model_AppVariables.ErrorReporting.MaxPendingAgeDays} days. Manual review recommended.");
                }
            }

            await Task.CompletedTask; // Make method truly async
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    #endregion
}
