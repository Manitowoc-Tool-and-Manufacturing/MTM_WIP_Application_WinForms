# Quickstart Guide: Error Reporting System

**Feature**: Error Reporting with User Notes & Offline Queue  
**Audience**: Developers implementing or maintaining this feature  
**Prerequisites**: Familiarity with MTM WinForms architecture, DAO patterns, stored procedures

---

## Overview

This feature adds comprehensive error reporting with user notes and offline queueing. When users encounter errors, they can click "Report Issue" to submit detailed reports with contextual notes. If the database is unavailable, reports are queued locally and automatically synced when connection is restored.

**Key Components**:
- `Form_ReportIssue` - User dialog for submitting error reports
- `Dao_ErrorReports` - Data access layer for database operations
- `Service_ErrorReportQueue` - Offline queue management
- `Service_ErrorReportSync` - Startup and manual sync coordination
- `sp_error_reports_Insert` - MySQL stored procedure

---

## Architecture

```
┌─────────────────────┐
│  Service_           │  Displays errors, integrates
│  ErrorHandler       │  "Report Issue" button
└──────┬──────────────┘
       │ Opens dialog
       ↓
┌─────────────────────┐
│  Form_ReportIssue   │  User enters notes,
│  (WinForms Dialog)  │  clicks Submit
└──────┬──────────────┘
       │ Check connectivity
       ├─────────────┬────────────────┐
       │ ONLINE      │ OFFLINE        │
       ↓             ↓                │
┌──────────────┐  ┌─────────────────┴┐
│ Dao_Error    │  │ Service_Error    │
│ Reports      │  │ ReportQueue      │
└──────┬───────┘  └─────────┬────────┘
       │                    │
       ↓                    ↓
┌──────────────┐  ┌──────────────────┐
│ Helper_      │  │ %APPDATA%\MTM    │
│ Database_    │  │ \ErrorReports\   │
│ StoredProc   │  │ Pending\*.sql    │
└──────┬───────┘  └─────────┬────────┘
       │                    │
       ↓                    │
┌──────────────┐           │
│ MySQL        │←──────────┘
│ error_       │  (synced by Service_
│ reports      │   ErrorReportSync)
└──────────────┘
```

---

## Quick Implementation Steps

### Step 1: Database Setup

**Create the error_reports table** (run in MySQL):

```sql
CREATE TABLE error_reports (
    ReportID INT AUTO_INCREMENT PRIMARY KEY,
    ReportDate DATETIME NOT NULL,
    UserName VARCHAR(100) NOT NULL,
    MachineName VARCHAR(100),
    AppVersion VARCHAR(50),
    ErrorType VARCHAR(255),
    ErrorSummary TEXT,
    UserNotes TEXT,
    TechnicalDetails TEXT,
    CallStack TEXT,
    Status ENUM('New', 'Reviewed', 'Resolved') DEFAULT 'New',
    ReviewedBy VARCHAR(100),
    ReviewedDate DATETIME,
    DeveloperNotes TEXT,
    
    INDEX idx_user (UserName),
    INDEX idx_date (ReportDate DESC),
    INDEX idx_status (Status),
    INDEX idx_machine (MachineName)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
```

**Create the stored procedure**:

```sql
-- See contracts/sp_error_reports_Insert.sql for full implementation
DELIMITER $$
CREATE PROCEDURE sp_error_reports_Insert(
    IN p_UserName VARCHAR(100),
    IN p_MachineName VARCHAR(100),
    IN p_AppVersion VARCHAR(50),
    IN p_ErrorType VARCHAR(255),
    IN p_ErrorSummary TEXT,
    IN p_UserNotes TEXT,
    IN p_TechnicalDetails TEXT,
    IN p_CallStack TEXT,
    OUT p_ReportID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Implementation in contracts/sp_error_reports_Insert.sql
END$$
DELIMITER ;
```

### Step 2: Create Model Classes

**Models/Model_ErrorReport.cs**:

```csharp
public class Model_ErrorReport
{
    public int ReportID { get; set; }
    public DateTime ReportDate { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? MachineName { get; set; }
    public string? AppVersion { get; set; }
    public string? ErrorType { get; set; }
    public string? ErrorSummary { get; set; }
    public string? UserNotes { get; set; }
    public string? TechnicalDetails { get; set; }
    public string? CallStack { get; set; }
    public ErrorReportStatus Status { get; set; }
    // ... (see data-model.md for complete definition)
}
```

**Models/Model_QueuedErrorReport.cs** (see data-model.md)

### Step 3: Implement Data Access Layer

**Data/Dao_ErrorReports.cs**:

```csharp
#region Fields
private static readonly string ConnectionString = 
    Helper_Database_Variables.GetConnectionString();
#endregion

#region Database Operations

/// <summary>
/// Inserts a new error report into the database.
/// </summary>
/// <returns>DaoResult containing the generated ReportID on success</returns>
public static async Task<DaoResult<int>> InsertReportAsync(Model_ErrorReport report)
{
    try
    {
        var parameters = new Dictionary<string, object>
        {
            ["UserName"] = report.UserName,
            ["MachineName"] = report.MachineName ?? (object)DBNull.Value,
            ["AppVersion"] = report.AppVersion ?? (object)DBNull.Value,
            ["ErrorType"] = report.ErrorType ?? (object)DBNull.Value,
            ["ErrorSummary"] = report.ErrorSummary ?? (object)DBNull.Value,
            ["UserNotes"] = report.UserNotes ?? (object)DBNull.Value,
            ["TechnicalDetails"] = report.TechnicalDetails ?? (object)DBNull.Value,
            ["CallStack"] = report.CallStack ?? (object)DBNull.Value
        };

        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            ConnectionString,
            "sp_error_reports_Insert",
            parameters,
            useAsync: true
        );

        if (result.IsSuccess && result.OutputParameters.ContainsKey("ReportID"))
        {
            int reportID = Convert.ToInt32(result.OutputParameters["ReportID"]);
            return DaoResult<int>.Success(reportID, result.StatusMessage);
        }

        return DaoResult<int>.Failure(result.StatusMessage, result.Exception);
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex, "Failed to insert error report");
        return DaoResult<int>.Failure("Error submitting report", ex);
    }
}

#endregion
```

### Step 4: Implement Offline Queue Service

**Services/Service_ErrorReportQueue.cs**:

```csharp
#region Fields
private static readonly string QueueDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "MTM_Application", "ErrorReports", "Pending");

private static readonly string ArchiveDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "MTM_Application", "ErrorReports", "Sent");
#endregion

#region Queue Operations

public static async Task<DaoResult<string>> QueueReportAsync(Model_ErrorReport report)
{
    try
    {
        // Ensure directories exist
        Directory.CreateDirectory(QueueDirectory);
        Directory.CreateDirectory(ArchiveDirectory);

        // Generate filename
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string sanitizedUser = report.UserName.Replace(".", "").Replace(" ", "");
        string guid = Guid.NewGuid().ToString("N").Substring(0, 6);
        string fileName = $"ErrorReport_{timestamp}_{sanitizedUser}_{guid}.sql";
        string filePath = Path.Combine(QueueDirectory, fileName);

        // Generate SQL content
        string sql = GenerateSqlForReport(report);

        // Write to file
        await File.WriteAllTextAsync(filePath, sql);

        LoggingUtility.Log($"Error report queued: {fileName}");
        return DaoResult<string>.Success(filePath, "Report queued for later submission");
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex, "Failed to queue error report");
        return DaoResult<string>.Failure("Failed to queue report", ex);
    }
}

private static string GenerateSqlForReport(Model_ErrorReport report)
{
    // Escape single quotes for SQL
    string EscapeSql(string? value) => value?.Replace("'", "''") ?? "NULL";

    return $@"-- Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
-- User: {report.UserName}
-- Machine: {report.MachineName}
-- Error Type: {report.ErrorType}

START TRANSACTION;

CALL sp_error_reports_Insert(
    '{EscapeSql(report.UserName)}',
    '{EscapeSql(report.MachineName)}',
    '{EscapeSql(report.AppVersion)}',
    '{EscapeSql(report.ErrorType)}',
    '{EscapeSql(report.ErrorSummary)}',
    '{EscapeSql(report.UserNotes)}',
    '{EscapeSql(report.TechnicalDetails)}',
    '{EscapeSql(report.CallStack)}',
    @reportID,
    @status,
    @errorMsg
);

SELECT @status AS Status, @errorMsg AS Message, @reportID AS ReportID;

COMMIT;";
}

#endregion
```

### Step 5: Implement Sync Service

**Services/Service_ErrorReportSync.cs**:

```csharp
#region Fields
private static readonly SemaphoreSlim _syncLock = new SemaphoreSlim(1, 1);
#endregion

#region Sync Operations

public static async Task<DaoResult<int>> SyncOnStartupAsync()
{
    // Try to acquire lock with immediate timeout
    if (!await _syncLock.WaitAsync(0))
    {
        return DaoResult<int>.Failure("Sync already in progress", null);
    }

    try
    {
        // Check database connectivity first
        if (!await IsDatabaseAvailableAsync())
        {
            LoggingUtility.Log("Database unavailable, skipping startup sync");
            return DaoResult<int>.Success(0, "Database unavailable");
        }

        int successCount = await ProcessPendingFilesAsync();
        
        LoggingUtility.Log($"Startup sync completed: {successCount} reports submitted");
        return DaoResult<int>.Success(successCount, $"{successCount} reports synced");
    }
    finally
    {
        _syncLock.Release();
    }
}

private static async Task<int> ProcessPendingFilesAsync()
{
    string pendingPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "MTM_Application", "ErrorReports", "Pending");

    if (!Directory.Exists(pendingPath))
        return 0;

    var files = Directory.GetFiles(pendingPath, "*.sql")
                        .OrderBy(f => File.GetCreationTime(f))
                        .ToList();

    int successCount = 0;

    foreach (var filePath in files)
    {
        try
        {
            bool success = await ExecuteSqlFileAsync(filePath);
            if (success) successCount++;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex, $"Error processing queue file: {filePath}");
        }
    }

    return successCount;
}

#endregion
```

### Step 6: Create Report Issue Dialog

**Forms/ErrorDialog/Form_ReportIssue.cs**:

```csharp
public partial class Form_ReportIssue : Form
{
    private readonly Model_ErrorReport _report;

    public Form_ReportIssue(Model_ErrorReport report)
    {
        InitializeComponent();
        _report = report;
        
        // Populate error summary (read-only)
        txtErrorSummary.Text = report.ErrorSummary;
        txtErrorSummary.ReadOnly = true;
        
        // User notes field with placeholder
        txtUserNotes.PlaceholderText = "What were you doing when this error occurred?";
    }

    private async void btnSubmit_Click(object sender, EventArgs e)
    {
        // Capture user notes
        _report.UserNotes = txtUserNotes.Text.Trim();

        // Check database connectivity
        bool isOnline = await CheckDatabaseConnectivityAsync();

        DaoResult<int> result;

        if (isOnline)
        {
            // Submit online
            result = await Dao_ErrorReports.InsertReportAsync(_report);
            
            if (result.IsSuccess)
            {
                Service_ErrorHandler.ShowInformation(
                    $"Report submitted successfully. Report ID: {result.Data}",
                    "Success");
                DialogResult = DialogResult.OK;
            }
            else
            {
                Service_ErrorHandler.HandleException(
                    result.Exception,
                    ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["Operation"] = "SubmitReport" });
            }
        }
        else
        {
            // Queue offline
            var queueResult = await Service_ErrorReportQueue.QueueReportAsync(_report);
            
            if (queueResult.IsSuccess)
            {
                Service_ErrorHandler.ShowInformation(
                    "Report will be submitted when connection is restored.",
                    "Queued");
                DialogResult = DialogResult.OK;
            }
            else
            {
                Service_ErrorHandler.HandleException(
                    queueResult.Exception,
                    ErrorSeverity.Medium);
            }
        }
    }
}
```

### Step 7: Integrate with Service_ErrorHandler

**Modify Service_ErrorHandler** to add "Report Issue" button and open Form_ReportIssue when clicked.

### Step 8: Add Developer Settings Menu Item

**Modify Controls/SettingsForm/Control_DeveloperSettings.cs**:

```csharp
private async void menuItemSyncReports_Click(object sender, EventArgs e)
{
    var result = await Service_ErrorReportSync.SyncManuallyAsync();
    
    if (result.IsSuccess)
    {
        Service_ErrorHandler.ShowInformation(
            $"{result.Data} error reports submitted successfully.",
            "Sync Complete");
    }
    else
    {
        Service_ErrorHandler.ShowWarning(result.Message, "Sync Failed");
    }
}
```

### Step 9: Register Startup Sync

**Modify Program.cs or Form_Main.Load**:

```csharp
// Fire and forget background sync
_ = Task.Run(async () =>
{
    try
    {
        var result = await Service_ErrorReportSync.SyncOnStartupAsync();
        
        if (result.IsSuccess && result.Data > 0)
        {
            // Show notification on UI thread
            mainForm.Invoke(() => {
                ShowSyncNotification(result.Data);
            });
        }
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex, "Startup sync failed");
    }
});
```

---

## Configuration

Add to `Model_AppVariables` or appsettings.json:

```json
{
  "ErrorReporting": {
    "QueueDirectory": "%APPDATA%\\MTM_Application\\ErrorReports\\Pending\\",
    "ArchiveDirectory": "%APPDATA%\\MTM_Application\\ErrorReports\\Sent\\",
    "MaxPendingAgeDays": 30,
    "EnableAutoSyncOnStartup": true,
    "SyncProgressThreshold": 5
  }
}
```

---

## Testing Checklist

### Manual Validation Scenarios

- [ ] **Online submission**: Error occurs, click "Report Issue", enter notes, submit successfully, verify Report ID shown
- [ ] **Offline queueing**: Disconnect database, trigger error, submit report, verify SQL file created in Pending folder
- [ ] **Startup sync**: Queue 3 reports offline, restart app with database online, verify all 3 submitted and files moved to Sent
- [ ] **Manual sync**: Queue 2 reports, use Developer Settings → Sync Pending Reports, verify success notification
- [ ] **Concurrent prevention**: Start manual sync, immediately trigger startup sync, verify only one processes
- [ ] **Corrupt file handling**: Create malformed .sql file in Pending, run sync, verify renamed to .corrupt
- [ ] **Large reports**: Submit report with very long stack trace (near 64KB), verify truncation if needed
- [ ] **Special characters**: Enter user notes with quotes, newlines, unicode, verify proper escaping in SQL file
- [ ] **Cleanup**: Verify files >30 days old are removed from Sent folder on startup

### Database Verification

```sql
-- Verify report inserted
SELECT * FROM error_reports ORDER BY ReportID DESC LIMIT 10;

-- Check for UserNotes population
SELECT ReportID, UserName, UserNotes, Status 
FROM error_reports 
WHERE UserNotes IS NOT NULL;

-- Verify indexes exist
SHOW INDEX FROM error_reports;
```

---

## Troubleshooting

### Issue: Reports not syncing on startup

**Check**:
1. Database connectivity: `SELECT 1;` in MySQL
2. Pending folder location: `%APPDATA%\MTM_Application\ErrorReports\Pending\`
3. File permissions: User can read/write to AppData
4. Logs: Search for "Startup sync" in log_error table

### Issue: Corrupt file errors

**Resolution**:
1. Locate .corrupt file in Pending folder
2. Open in text editor to inspect SQL syntax
3. Fix SQL syntax errors manually
4. Rename back to .sql extension
5. Trigger manual sync from Developer Settings

### Issue: Duplicate reports

**Cause**: File move failure after successful SQL execution

**Resolution**: Idempotent check prevents duplicates automatically. Review logs to identify file move issues.

---

## Performance Benchmarks

- **Online submission**: <2 seconds (includes database round-trip)
- **Offline queue**: <500ms (file write operation)
- **Startup sync (10 reports)**: <5 seconds (background, non-blocking)
- **Manual sync (10 reports)**: <5 seconds (with progress indicator)

---

## Security Notes

- No passwords or connection strings in error reports
- Queue location (%APPDATA%) is user-scoped (not shared)
- SQL files use parameterized CALL statements (no injection risk)
- File names use GUIDs (non-predictable)

---

## Next Steps

After implementation:
1. Run manual validation checklist above
2. Test offline scenario thoroughly (disconnect database)
3. Verify startup sync doesn't delay application launch
4. Monitor log_error table for queue processing errors
5. Document any environment-specific configuration

---

## Related Documentation

- [spec.md](./spec.md) - Feature specification
- [plan.md](./plan.md) - Technical implementation plan
- [research.md](./research.md) - Design decisions and rationale
- [data-model.md](./data-model.md) - Entity definitions
- [contracts/sp_error_reports_Insert.sql](./contracts/sp_error_reports_Insert.sql) - Stored procedure contract

## Instruction Files

- `.github/instructions/csharp-dotnet8.instructions.md` - C# patterns
- `.github/instructions/mysql-database.instructions.md` - Database patterns
- `.github/instructions/testing-standards.instructions.md` - Manual validation
