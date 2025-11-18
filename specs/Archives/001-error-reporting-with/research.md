# Research: Error Reporting with User Notes & Offline Queue

**Date**: 2025-10-25  
**Feature**: Error Reporting System for Manufacturing WinForms Application  
**Purpose**: Resolve technical approach for offline queue serialization, concurrency patterns, and SQL file generation

---

## Research Questions

### Q1: Offline Queue File Format

**Question**: Should offline error reports be stored as SQL INSERT statements or JSON files?

**Decision**: **SQL INSERT statements** calling the stored procedure

**Rationale**:
- **Manual recovery**: DBAs can execute `.sql` files directly in MySQL Workbench during emergency recovery
- **Constitution compliance**: Maintains stored procedure pattern even offline (files contain `CALL sp_error_reports_Insert(...)`)
- **Transaction consistency**: Each file is self-contained SQL transaction matching online submission
- **Debugging**: Plain text SQL is human-readable for troubleshooting queue issues
- **Fallback execution**: If sync process fails, files can be batch-executed via mysql CLI

**Alternatives Considered**:
- **JSON serialization**: Rejected because requires deserialization code + SQL construction = complexity, breaks stored procedure visibility
- **Binary format**: Rejected due to lack of human readability and manual recovery difficulty
- **XML**: Rejected as unnecessarily verbose compared to SQL, harder for DBAs to work with

**Implementation Notes**:
```sql
-- File format: ErrorReport_20251025_143022_UserJohn_GUID.sql
START TRANSACTION;

CALL sp_error_reports_Insert(
    'John.Smith',                    -- p_UserName
    'WORKSTATION-05',                -- p_MachineName
    '5.0.0.142',                     -- p_AppVersion
    'NullReferenceException',        -- p_ErrorType
    'Object reference not set...',   -- p_ErrorSummary
    'I was clicking Save button...', -- p_UserNotes
    'Stack trace details...',        -- p_TechnicalDetails
    'Full call stack...',            -- p_CallStack
    @reportID,                       -- OUT p_ReportID
    @status,                         -- OUT p_Status
    @errorMsg                        -- OUT p_ErrorMsg
);

-- Check stored procedure status
SELECT @status AS Status, @errorMsg AS Message, @reportID AS ReportID;

COMMIT;
```

---

### Q2: Concurrent Sync Prevention Pattern

**Question**: How to prevent multiple sync operations from running simultaneously (startup auto-sync + manual trigger)?

**Decision**: **SemaphoreSlim** with `WaitAsync(0)` (immediate timeout)

**Rationale**:
- **Non-blocking**: `WaitAsync(0)` returns immediately if lock held, avoiding UI thread blocking
- **Cross-thread safe**: Works correctly with startup background thread + UI thread manual trigger
- **.NET standard**: Built-in primitive, no external dependencies
- **Async-friendly**: Works with async/await patterns (`using` with IDisposable scope)
- **Clear semantics**: Easier to reason about than custom locking flags

**Alternatives Considered**:
- **lock (object) {}**: Rejected because doesn't support async/await, would block UI thread
- **Manual bool flag**: Rejected due to race conditions without proper memory barriers
- **Mutex**: Rejected as overkill for single-process scenario, doesn't support async
- **ReaderWriterLockSlim**: Rejected because we only need exclusive access, not read/write distinction

**Implementation Pattern**:
```csharp
private static readonly SemaphoreSlim _syncLock = new SemaphoreSlim(1, 1);

public static async Task<Model_Dao_Result<int>> SyncPendingReportsAsync()
{
    // Try to acquire lock with immediate timeout
    if (!await _syncLock.WaitAsync(0))
    {
        return Model_Dao_Result<int>.Failure("Sync already in progress", null);
    }

    try
    {
        // Perform sync operations
        int count = await ProcessQueueAsync();
        return Model_Dao_Result<int>.Success(count, $"Synced {count} reports");
    }
    finally
    {
        _syncLock.Release();
    }
}
```

---

### Q3: Startup Sync Performance Impact

**Question**: How to minimize startup delay when processing pending reports?

**Decision**: **Fire-and-forget background task** with threshold-based progress UI

**Rationale**:
- **Non-blocking startup**: Application window shows immediately, sync runs in background
- **User visibility**: Show progress indicator only if >5 reports (configurable threshold)
- **Gradual degradation**: If sync takes long, user can still interact with main UI
- **Logging**: All sync results logged to `log_error` table for audit trail even if UI not shown
- **Notification**: Toast notification on completion shows sync summary ("X reports submitted")

**Alternatives Considered**:
- **Synchronous startup sync**: Rejected due to 500ms performance goal - could exceed with network latency
- **Blocking splash screen**: Rejected because manufacturing users need immediate access to app
- **Deferred sync (first idle)**: Rejected because critical errors should sync ASAP, not wait for idle
- **Background service**: Rejected as overkill for simple startup task, adds complexity

**Implementation Pattern**:
```csharp
// In Program.cs or Form_Main.Load
private async void InitiateStartupSync()
{
    // Fire and forget, no await (non-blocking)
    _ = Task.Run(async () =>
    {
        try
        {
            var result = await Service_ErrorReportSync.SyncOnStartupAsync();
            
            // Show notification only if reports were synced
            if (result.IsSuccess && result.Data > 0)
            {
                // Marshal to UI thread for notification
                this.Invoke(() => ShowSyncNotification(result.Data));
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex, "Startup sync failed");
        }
    });
}
```

---

### Q4: File Move Failure Recovery

**Question**: What if SQL INSERT succeeds but moving file to Sent folder fails?

**Decision**: **Idempotent check before execution** + retry on next sync

**Rationale**:
- **Prevent duplicates**: Check if report with matching ReportDate + UserName already exists before executing SQL
- **Safe retry**: Failed file stays in Pending folder, retried on next sync without creating duplicates
- **Audit trail**: Both attempts logged, DBAs can identify retry patterns
- **Graceful degradation**: One file failure doesn't block processing of other files (independent transactions)

**Alternatives Considered**:
- **Skip duplicate detection**: Rejected because file move failures would create duplicate reports
- **Delete file before execution**: Rejected because SQL failure would lose error report permanently
- **Two-phase commit**: Rejected as MySQL 5.7 doesn't support distributed transactions across file system + DB
- **Rename instead of move**: Rejected because complicates cleanup and archival management

**Implementation Pattern**:
```csharp
private async Task<bool> ExecuteSqlFileAsync(string filePath)
{
    try
    {
        // Step 1: Read SQL file
        string sql = await File.ReadAllTextAsync(filePath);
        
        // Step 2: Extract identifiers (username, timestamp) from filename or SQL
        var fileInfo = ParseFileInfo(filePath);
        
        // Step 3: Check for existing report (idempotent)
        if (await ReportExistsAsync(fileInfo.UserName, fileInfo.Timestamp))
        {
            
            // Move to Sent folder (already processed)
            File.Move(filePath, GetArchivePath(filePath));
            return true;
        }
        
        // Step 4: Execute SQL (calls stored procedure)
        var result = await Helper_Database_StoredProcedure.ExecuteNonQueryAsync(sql);
        
        if (!result.IsSuccess)
        {
            // Leave file in Pending for retry
            LoggingUtility.LogApplicationError(result.Exception, $"SQL execution failed: {filePath}");
            return false;
        }
        
        // Step 5: Move to Sent folder
        try
        {
            File.Move(filePath, GetArchivePath(filePath));
        }
        catch (IOException ex)
        {
            // SQL succeeded but move failed - will retry on next sync (idempotent check prevents duplicate)
            LoggingUtility.LogApplicationError(ex, $"File move failed after successful SQL: {filePath}");
            return false; // Considered failure for retry logic
        }
        
        return true;
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex, $"Error processing queue file: {filePath}");
        return false;
    }
}
```

---

### Q5: Queue Directory Location

**Question**: Where should pending error reports be stored on local file system?

**Decision**: **%APPDATA%\MTM_Application\ErrorReports\** with Pending and Sent subfolders

**Rationale**:
- **User-writable**: AppData is always writable without elevation, even in restricted environments
- **Per-user isolation**: Each Windows user gets own queue (prevents permission conflicts)
- **Application grouping**: Follows Windows conventions for application data storage
- **Backup-friendly**: Standard backup tools include AppData by default
- **Portable**: Works across different Windows versions and deployment scenarios

**Alternatives Considered**:
- **ProgramData**: Rejected because requires elevated permissions to write, complicates deployment
- **Temp folder**: Rejected because cleanup policies might delete pending reports
- **Application directory**: Rejected because Program Files is read-only for non-admin users
- **Network share**: Rejected because adds network dependency to offline scenario (defeats purpose)

**Directory Structure**:
```
%APPDATA%\MTM_Application\ErrorReports\
├── Pending\                            # Unprocessed reports
│   ├── ErrorReport_20251025_143022_UserJohn_a3f8b2.sql
│   ├── ErrorReport_20251025_143156_UserMary_7d9c4e.sql
│   └── ErrorReport_20251025_144301_UserJohn_b2e7a9.sql
│
└── Sent\                               # Successfully submitted
    ├── ErrorReport_20251023_091234_UserJohn_f4e3d2.sql
    └── ErrorReport_20251024_155623_UserMary_c8a1b5.sql
```

**Fallback Strategy**: If AppData is inaccessible (rare edge case like corrupted user profile), fall back to:
1. `%TEMP%\MTM_ErrorReports\` (temporary location)
2. Log warning to Windows Event Log
3. Show notification to user about temporary queue location

---

### Q6: Corrupted File Handling

**Question**: How to handle malformed SQL files in queue without blocking other reports?

**Decision**: **Try-catch with .corrupt extension** + continue processing

**Rationale**:
- **Isolation**: One bad file doesn't stop processing of other valid files
- **Visibility**: `.corrupt` extension makes issues obvious to administrators
- **Debugging**: Corrupt file preserved for forensic analysis
- **Recovery**: Admin can manually fix and rename back to `.sql` to retry
- **Monitoring**: Log each corrupt file detection for alerting

**Implementation Pattern**:
```csharp
private async Task ProcessQueuedFilesAsync()
{
    var pendingFiles = Directory.GetFiles(pendingPath, "*.sql")
                                .OrderBy(f => File.GetCreationTime(f));
    
    int successCount = 0;
    int failureCount = 0;
    
    foreach (var filePath in pendingFiles)
    {
        try
        {
            bool success = await ExecuteSqlFileAsync(filePath);
            if (success) successCount++;
            else failureCount++;
        }
        catch (SqlException ex)
        {
            // SQL syntax error or execution failure
            HandleCorruptFile(filePath, ex);
            failureCount++;
        }
        catch (Exception ex)
        {
            // Other errors (I/O, serialization, etc.)
            LoggingUtility.LogApplicationError(ex, $"Unexpected error processing: {filePath}");
            failureCount++;
        }
    }
    
    
}

private void HandleCorruptFile(string filePath, Exception ex)
{
    string corruptPath = Path.ChangeExtension(filePath, ".corrupt");
    
    try
    {
        File.Move(filePath, corruptPath);
        LoggingUtility.LogApplicationError(ex, $"Corrupt file detected, renamed: {filePath} -> {corruptPath}");
        
        // Optional: Send alert to monitoring system
        // AlertService.NotifyCorruptQueue(corruptPath);
    }
    catch (IOException ioEx)
    {
        LoggingUtility.LogApplicationError(ioEx, $"Failed to rename corrupt file: {filePath}");
    }
}
```

---

### Q7: Retention Period and Cleanup

**Question**: How long should successfully sent reports be kept in archive folder?

**Decision**: **Configurable retention (default 30 days)** with startup cleanup

**Rationale**:
- **Audit trail**: 30 days provides reasonable investigation window without consuming excessive disk space
- **Configurable**: Allows adjustment for environments with different compliance requirements
- **Automated cleanup**: Runs on application startup (low-impact background operation)
- **Graceful**: Cleanup failures logged but don't block application functionality

**Configuration**:
```csharp
// In Model_Application_Variables or appsettings.json
public class ErrorReportingConfig
{
    public int MaxPendingAgeDays { get; set; } = 30;
    public int MaxSentArchiveAgeDays { get; set; } = 30;
    public string QueueDirectory { get; set; } = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                     "MTM_Application", "ErrorReports");
}
```

**Cleanup Logic**:
```csharp
private async Task CleanupOldReportsAsync()
{
    try
    {
        var config = Model_Application_Variables.ErrorReporting;
        var cutoffDate = DateTime.Now.AddDays(-config.MaxSentArchiveAgeDays);
        
        // Cleanup Sent folder
        var sentPath = Path.Combine(config.QueueDirectory, "Sent");
        var oldFiles = Directory.GetFiles(sentPath, "*.sql")
                                .Where(f => File.GetCreationTime(f) < cutoffDate)
                                .ToList();
        
        foreach (var file in oldFiles)
        {
            try
            {
                File.Delete(file);
                
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex, $"Failed to delete old report: {file}");
            }
        }
        
        // Also cleanup stale Pending folder (>30 days means likely abandoned)
        var pendingPath = Path.Combine(config.QueueDirectory, "Pending");
        var stalePending = Directory.GetFiles(pendingPath, "*.sql")
                                    .Where(f => File.GetCreationTime(f) < cutoffDate)
                                    .ToList();
        
        if (stalePending.Any())
        {
            
            // Optional: Move to .stale extension or separate folder for admin review
        }
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex, "Cleanup of old error reports failed");
    }
}
```

---

## Technology Stack Decisions

### Serialization: System.Text.Json vs SQL Files

**Decision**: **SQL files** (as covered in Q1)

**Rationale Summary**:
- Already have stored procedure (`sp_error_reports_Insert`)
- SQL files maintain constitution compliance (stored procedure only pattern)
- Manual recovery capability for DBAs
- No additional serialization dependencies

### Concurrency: Best Practices

**Decision**: **SemaphoreSlim** for async lock coordination (as covered in Q2)

**Best Practices Applied**:
- Non-blocking async patterns
- Immediate timeout (`WaitAsync(0)`) for fail-fast behavior
- Finally block ensures release even on exceptions
- Static instance for cross-instance coordination

### File System Operations: Best Practices

**Decision**: **Async I/O** with robust error handling

**Best Practices**:
- Use `File.ReadAllTextAsync` / `File.WriteAllTextAsync` for async operations
- Wrap file operations in try-catch with specific exception types (IOException, UnauthorizedAccessException)
- Log all I/O failures with full context (file path, operation, timestamp)
- Use `Path.Combine` for cross-platform path building (even though Windows-only)
- Check directory existence before operations, create if missing

**Security Considerations**:
- Validate file names to prevent path traversal attacks
- Use GUIDs in filenames to prevent collisions and prediction
- File permissions: Default ACL from AppData (current user read/write)
- No sensitive data in file names (username is safe, error details are not)

---

## Integration Points

### Existing Systems

**Service_ErrorHandler Integration**:
- Add "Report Issue" button handler that opens Form_ReportIssue
- Pass exception details, user context, and error summary to dialog
- Error cooldown mechanism ensures same error can't be reported multiple times rapidly

**Helper_Database_StoredProcedure Usage**:
- All database operations route through existing helper
- Maintains connection pooling, retry logic, and logging patterns
- Model_Dao_Result<T> wrapper pattern for consistent error handling

**LoggingUtility Integration**:
- All sync operations logged (start, completion, failures)
- Corrupt file detection logged with ERROR severity
- Queue depth metrics logged for monitoring

**Model_Application_Variables Configuration**:
- Store ErrorReportingConfig settings in existing configuration system
- Leverage existing environment-aware database selection logic

---

## Summary

All research questions resolved with concrete decisions documented above. Key architectural choices:

1. **Offline queue uses SQL files** calling stored procedures (maintains constitution compliance)
2. **SemaphoreSlim prevents concurrent sync** operations (async-friendly locking)
3. **Background startup sync** with fire-and-forget pattern (non-blocking, <500ms impact)
4. **Idempotent execution** with file move recovery (safe retries, no duplicates)
5. **AppData queue location** with fallback strategy (user-writable, backup-friendly)
6. **Corrupt file isolation** with .corrupt extension (debugging, non-blocking)
7. **Configurable retention** with automated cleanup (30-day default, startup cleanup)

**Ready for Phase 1**: Data model, contracts, and quickstart can now be generated with concrete implementation details.
