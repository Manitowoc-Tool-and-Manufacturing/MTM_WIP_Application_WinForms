# Data Model: Error Reporting System

**Feature**: Error Reporting with User Notes & Offline Queue  
**Date**: 2025-10-25  
**Source**: Extracted from spec.md and research.md

---

## Entities

### ErrorReport

**Purpose**: Represents a user-submitted error report persisted in the database.

**Attributes**:

| Attribute | Type | Constraints | Description |
|-----------|------|-------------|-------------|
| ReportID | INT | PRIMARY KEY, AUTO_INCREMENT | Unique identifier for the error report |
| ReportDate | DATETIME | NOT NULL | Timestamp when error occurred and report was submitted |
| UserName | VARCHAR(100) | NOT NULL, INDEX | Windows username of user who encountered error |
| AppVersion | VARCHAR(50) | NULL | Application version (e.g., "5.0.0.142") |
| ErrorType | VARCHAR(255) | NULL | Exception type (e.g., "NullReferenceException", "SqlException") |
| ErrorSummary | TEXT | NULL | User-friendly error message (max 64KB) |
| UserNotes | TEXT | NULL | User-provided context describing what they were doing |
| TechnicalDetails | TEXT | NULL | Full exception details including inner exceptions |
| CallStack | TEXT | NULL | Complete stack trace at time of error |
| Status | ENUM('New', 'Reviewed', 'Resolved') | NOT NULL, DEFAULT 'New', INDEX | Lifecycle tracking for developer review |
| ReviewedBy | VARCHAR(100) | NULL | Developer username who reviewed the report |
| ReviewedDate | DATETIME | NULL | When report was marked as Reviewed |
| DeveloperNotes | TEXT | NULL | Internal notes from developers about resolution |

**Relationships**:
- Belongs to User (by UserName) - many error reports per user
- Status tracks lifecycle: New → Reviewed → Resolved

**Indexes**:
- `idx_user (UserName)` - Query reports by user
- `idx_date (ReportDate DESC)` - Chronological report listing
- `idx_status (Status)` - Filter by review status

**Validation Rules**:
- ReportDate must be <= NOW() (prevent future-dated reports)
- UserName is required (cannot submit anonymous reports)
- ErrorSummary or TechnicalDetails required (cannot be both NULL)
- Status transitions: New → Reviewed → Resolved (one-way flow)
- ReviewedBy required when Status = 'Reviewed' or 'Resolved'

**State Transitions**:
```
New (on creation)
  ↓ (developer reviews)
Reviewed (ReviewedBy set, ReviewedDate set)
  ↓ (issue fixed and verified)
Resolved (DeveloperNotes typically added)
```

---

### PendingErrorReportFile (Conceptual Model)

**Purpose**: Represents an offline queued error report stored as a local SQL file.

**Attributes**:

| Attribute | Type | Description |
|-----------|------|-------------|
| FileName | string | Generated filename (ErrorReport_YYYYMMDD_HHMMSS_UserName_GUID.sql) |
| FilePath | string | Full absolute path in Pending folder |
| CreationDate | DateTime | File creation timestamp (from FileInfo) |
| FileSize | long | Size in bytes |
| AttemptCount | int | Number of sync attempts (tracked in-memory during processing) |

**Lifecycle States**:
1. **Created** - File written to Pending folder when database unavailable
2. **Processing** - Being executed by sync operation
3. **Sent** - Successfully submitted, moved to Sent archive folder
4. **Corrupt** - Execution failed, renamed with .corrupt extension
5. **Stale** - Pending >30 days (flagged during cleanup, optionally archived)

**File Naming Convention**:
```
Format: ErrorReport_{DateTime}_{UserName}_{GUID}.sql
Example: ErrorReport_20251025_143022_JohnSmith_a3f8b2e7.sql

Components:
- DateTime: YYYYMMdd_HHmmss (sortable, chronological)
- UserName: Sanitized Windows username (spaces removed)
- GUID: First 6 chars of Guid.NewGuid() (uniqueness guarantee)
```

**Content Structure** (SQL file):
```sql
-- File header with metadata
-- Generated: 2025-10-25 14:30:22
-- User: John.Smith
-- Machine: WORKSTATION-05
-- Error Type: NullReferenceException

START TRANSACTION;

CALL sp_error_reports_Insert(
    'John.Smith',                              -- p_UserName
    '5.0.0.142',                               -- p_AppVersion
    'NullReferenceException',                  -- p_ErrorType
    'Object reference not set to instance...', -- p_ErrorSummary
    'I was clicking the Save button...',       -- p_UserNotes (USER PROVIDED)
    'Full exception details with inner...',    -- p_TechnicalDetails
    'at MTM.Forms.MainForm.btnSave_Click...',  -- p_CallStack
    @reportID,                                 -- OUT p_ReportID
    @status,                                   -- OUT p_Status
    @errorMsg                                  -- OUT p_ErrorMsg
);

-- Validation check
SELECT @status AS Status, @errorMsg AS Message, @reportID AS ReportID;

COMMIT;
```

---

### Model_ErrorReport_Core (C# Class)

**Purpose**: C# entity representing ErrorReport for data transfer between layers.

```csharp
namespace MTM_WIP_Application_WinForms.Models
{
    /// <summary>
    /// Represents an error report submitted by a user with optional notes.
    /// </summary>
    public class Model_ErrorReport_Core
    {
        /// <summary>
        /// Unique identifier for the error report (auto-generated by database).
        /// </summary>
        public int ReportID { get; set; }

        /// <summary>
        /// Timestamp when the error occurred and report was created.
        /// </summary>
        public DateTime ReportDate { get; set; }

        /// <summary>
        /// Windows username of the user who encountered the error.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Application version at the time of the error.
        /// </summary>
        public string? AppVersion { get; set; }

        /// <summary>
        /// Type of exception (e.g., "NullReferenceException").
        /// </summary>
        public string? ErrorType { get; set; }

        /// <summary>
        /// User-friendly summary of the error.
        /// </summary>
        public string? ErrorSummary { get; set; }

        /// <summary>
        /// User-provided context describing what they were doing when error occurred.
        /// </summary>
        public string? UserNotes { get; set; }

        /// <summary>
        /// Full exception details including inner exceptions.
        /// </summary>
        public string? TechnicalDetails { get; set; }

        /// <summary>
        /// Complete stack trace at the time of the error.
        /// </summary>
        public string? CallStack { get; set; }

        /// <summary>
        /// Current lifecycle status of the report.
        /// </summary>
        public ErrorReportStatus Status { get; set; } = ErrorReportStatus.New;

        /// <summary>
        /// Developer username who reviewed this report.
        /// </summary>
        public string? ReviewedBy { get; set; }

        /// <summary>
        /// Timestamp when report was reviewed by developer.
        /// </summary>
        public DateTime? ReviewedDate { get; set; }

        /// <summary>
        /// Internal notes from developers about investigation or resolution.
        /// </summary>
        public string? DeveloperNotes { get; set; }
    }

    /// <summary>
    /// Lifecycle status of an error report.
    /// </summary>
    public enum ErrorReportStatus
    {
        /// <summary>
        /// Report has been submitted but not yet reviewed by developers.
        /// </summary>
        New = 0,

        /// <summary>
        /// Developer has reviewed the report and is investigating.
        /// </summary>
        Reviewed = 1,

        /// <summary>
        /// Issue has been resolved and verified.
        /// </summary>
        Resolved = 2
    }
}
```

---

### Model_ErrorReport_Core_Queued (C# Class)

**Purpose**: Represents a queued error report file for offline queue management.

```csharp
namespace MTM_WIP_Application_WinForms.Models
{
    /// <summary>
    /// Represents an error report queued in the local file system for later submission.
    /// </summary>
    public class Model_ErrorReport_Core_Queued
    {
        /// <summary>
        /// Full absolute path to the SQL file.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// File name only (without directory path).
        /// </summary>
        public string FileName => Path.GetFileName(FilePath);

        /// <summary>
        /// Timestamp when the file was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Size of the file in bytes.
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// Number of times sync has attempted to process this file.
        /// </summary>
        public int AttemptCount { get; set; }

        /// <summary>
        /// Whether the file appears to be valid SQL (basic validation).
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Creates a Model_ErrorReport_Core_Queued from a FileInfo object.
        /// </summary>
        public static Model_ErrorReport_Core_Queued FromFileInfo(FileInfo fileInfo)
        {
            return new Model_ErrorReport_Core_Queued
            {
                FilePath = fileInfo.FullName,
                CreationDate = fileInfo.CreationTime,
                FileSize = fileInfo.Length,
                AttemptCount = 0,
                IsValid = ValidateSqlFile(fileInfo.FullName)
            };
        }

        private static bool ValidateSqlFile(string filePath)
        {
            try
            {
                // Basic validation: file exists, readable, contains expected keywords
                if (!File.Exists(filePath)) return false;
                
                string content = File.ReadAllText(filePath);
                return content.Contains("CALL sp_error_reports_Insert") 
                    && content.Contains("START TRANSACTION")
                    && content.Contains("COMMIT");
            }
            catch
            {
                return false;
            }
        }
    }
}
```

---

## Entity Relationships

```
┌─────────────────────────────────────────┐
│         ErrorReport (Database)          │
├─────────────────────────────────────────┤
│ PK: ReportID (INT AUTO_INCREMENT)       │
│ ReportDate (DATETIME) [Indexed]         │
│ UserName (VARCHAR) [Indexed]            │
│ AppVersion (VARCHAR)                    │
│ ErrorType (VARCHAR)                     │
│ ErrorSummary (TEXT)                     │
│ UserNotes (TEXT) ← USER PROVIDED        │
│ TechnicalDetails (TEXT)                 │
│ CallStack (TEXT)                        │
│ Status (ENUM) [Indexed]                 │
│ ReviewedBy (VARCHAR)                    │
│ ReviewedDate (DATETIME)                 │
│ DeveloperNotes (TEXT)                   │
└─────────────────────────────────────────┘
             ↑
             │ (persisted via stored procedure)
             │
┌────────────┴────────────────────────────┐
│  Model_ErrorReport_Core (C# Application)     │
└─────────────────────────────────────────┘
             ↑
             │ (serialized to SQL when offline)
             │
┌────────────┴────────────────────────────┐
│ PendingErrorReportFile (File System)    │
├─────────────────────────────────────────┤
│ Location: %APPDATA%\MTM_Application\    │
│           ErrorReports\Pending\          │
│ Format: ErrorReport_DateTime_User.sql   │
│ Content: CALL sp_error_reports_Insert   │
└─────────────────────────────────────────┘
             │
             │ (sync process moves to)
             ↓
┌─────────────────────────────────────────┐
│ Sent Archive (File System)              │
├─────────────────────────────────────────┤
│ Location: %APPDATA%\MTM_Application\    │
│           ErrorReports\Sent\             │
│ Retention: 30 days (configurable)       │
└─────────────────────────────────────────┘
```

---

## Data Flow Diagrams

### Online Submission Flow

```
User Encounters Error
       ↓
Service_ErrorHandler shows error dialog
       ↓
User clicks "Report Issue"
       ↓
Form_ReportIssue dialog opens
  - Shows error summary (read-only)
  - User enters notes (optional)
       ↓
User clicks "Submit"
       ↓
Check database connectivity
       ↓
   [ONLINE] → Dao_ErrorReports.InsertReportAsync()
       ↓            ↓
       ↓     Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync()
       ↓            ↓
       ↓     CALL sp_error_reports_Insert(...)
       ↓            ↓
       ↓     MySQL: INSERT INTO error_reports
       ↓            ↓
       ↓     Return ReportID, p_Status=0, p_ErrorMsg='Success'
       ↓            ↓
       ↓     Model_Dao_Result<int>.Success(reportID, message)
       ↓
Success message: "Report submitted successfully. Report ID: 12345"
```

### Offline Queueing Flow

```
User Encounters Error
       ↓
Service_ErrorHandler shows error dialog
       ↓
User clicks "Report Issue"
       ↓
Form_ReportIssue dialog opens
  - Shows error summary (read-only)
  - User enters notes (optional)
       ↓
User clicks "Submit"
       ↓
Check database connectivity
       ↓
   [OFFLINE] → Service_ErrorReportQueue.QueueReportAsync()
       ↓            ↓
       ↓     Generate SQL file with CALL sp_error_reports_Insert
       ↓            ↓
       ↓     Write to %APPDATA%\MTM_Application\ErrorReports\Pending\
       ↓            ↓
       ↓     Filename: ErrorReport_20251025_143022_JohnSmith_a3f8b2.sql
       ↓            ↓
       ↓     Return Model_Dao_Result<string>.Success(filePath, "Queued")
       ↓
Success message: "Report queued. Will be submitted when connection restored."
```

### Startup Sync Flow

```
Application Starts (Program.cs)
       ↓
Service_ErrorReportSync.SyncOnStartupAsync()
  (runs in background, non-blocking)
       ↓
Check database connectivity
       ↓
   [OFFLINE] → Log "Database unavailable, skipping sync" → END
       ↓
   [ONLINE] → Enumerate files in Pending folder
       ↓            ↓
       ↓     Get *.sql files ordered by creation time
       ↓            ↓
       ↓     For each file sequentially:
       ↓       1. Read SQL content
       ↓       2. Check if report exists (idempotent)
       ↓       3. Execute SQL (CALL sp_error_reports_Insert)
       ↓       4. If success: Move to Sent folder
       ↓       5. If failure: Leave in Pending for retry
       ↓       6. Catch SQL errors: Rename to .corrupt
       ↓            ↓
       ↓     Count successes and failures
       ↓            ↓
       ↓     Model_Dao_Result<int>.Success(successCount, message)
       ↓
Show notification (if count > 0): "X error reports submitted"
```

---

## Validation Rules Summary

### Database Constraints
- `ReportDate NOT NULL` - Every report must have timestamp
- `UserName NOT NULL` - Cannot submit anonymous reports
- `Status DEFAULT 'New'` - All reports start as New
- Indexes on UserName, ReportDate, Status for query performance

### Application Validations
- ErrorSummary OR TechnicalDetails required (at least one must be populated)
- UserNotes optional but encouraged (placeholder text guides user)
- Status transitions enforced: New → Reviewed → Resolved (no backward transitions)
- ReviewedBy required when Status changes from New
- File name validation: Must match pattern `ErrorReport_*_*.sql`

### File System Constraints
- Pending folder: Maximum 100 files (warning logged at 80%)
- File age: Pending >30 days flagged as stale
- File size: Warning if >1MB (unusual for error report)
- Sent folder: Automatic cleanup >30 days

---

## Performance Considerations

### Database Indexes
- `idx_date` DESC for recent reports queries (Developer Tools dashboard)
- `idx_user` for user-specific report history
- `idx_status` for filtered views (New reports only)
- Composite index consideration: (Status, ReportDate) for "unreviewed recent" queries

### Query Patterns
- **Common**: SELECT recent New reports for developer review dashboard
- **Occasional**: SELECT all reports by specific user
- **Rare**: Full-text search on ErrorSummary/UserNotes (not indexed initially)

### Storage Estimates
- Average report size: ~5KB (including stack traces)
- Expected volume: 5-20 reports/day/user × 50 users = 250-1000 reports/day
- 30-day retention: ~7,500-30,000 reports retained
- Storage estimate: 37MB-150MB for 30-day window

### File System Performance
- Sequential file processing minimizes I/O contention
- Async File.ReadAllTextAsync for non-blocking reads
- File count warning at 80% capacity (80 files in Pending)
- Directory enumeration cached to avoid repeated scans

---

## Security Considerations

### Data Privacy
- UserName stored (Windows account, not personally identifiable)
- No passwords or secrets in error reports
- TechnicalDetails may contain file paths (review for sensitivity)

### File System Security
- Queue location: %APPDATA% (user-writable, user-readable only by default)
- File permissions: Inherit from parent directory (typically owner only)
- No elevated privileges required for queue operations
- Fallback location (%TEMP%) also user-scoped

### SQL Injection Prevention
- Stored procedure pattern eliminates inline SQL
- Offline SQL files contain parameterized CALL statements
- No user input concatenated into SQL strings
- Parameters properly escaped during SQL file generation

### Input Validation
- File name sanitization prevents path traversal
- GUID ensures no predictable file names
- SQL content validation before execution (basic check for CALL statement)
- Maximum text field sizes enforced (TEXT = 64KB limit)

---

## Summary

Data model defined for:
- **ErrorReport entity** with 14 attributes supporting full error context and developer workflow
- **PendingErrorReportFile conceptual model** managing offline queue lifecycle
- **C# model classes** (Model_ErrorReport_Core, Model_ErrorReport_Core_Queued) for application layer
- **State transitions** (New → Reviewed → Resolved) and file lifecycle (Created → Processing → Sent/Corrupt/Stale)
- **Validation rules** ensuring data quality and constitutional compliance
- **Performance indexes** optimizing common query patterns
- **Security posture** minimizing sensitive data exposure

Ready for contract generation (stored procedure and DAO method signatures).
