# MTM WIP Application Constitution

<!--
Sync Impact Report (2025-11-11):
- Version change: None → 1.0.0
- Initial constitution creation based on codebase analysis
- Principles derived from existing code patterns in:
  * Service_ErrorHandler.cs (centralized error handling)
  * LoggingUtility.cs (structured logging and CSV output)
  * Helper_Database_StoredProcedure.cs (comprehensive database patterns)
  * DAO layer patterns (Data/Dao_*.cs files)
- Templates requiring updates:
  ✅ plan-template.md - Aligned with MTM-specific patterns
  ✅ spec-template.md - Aligned with MTM WIP workflow
  ✅ tasks-template.md - Integrated MTM development phases
- Follow-up TODOs: None
-->

## Core Principles

### I. Centralized Error Handling (NON-NEGOTIABLE)

**All exceptions MUST be handled through Service_ErrorHandler**, which provides a sophisticated multi-layer error handling system with automatic fallbacks and anti-spam protection.

#### Core Architecture

**Primary error handling methods** (use these, never MessageBox.Show()):
- `HandleException()` - General exception handling with EnhancedErrorDialog
- `HandleDatabaseError()` - Database-specific with connection recovery
- `HandleValidationError()` - User input validation errors
- `HandleFileError()` - File operation errors with retry support
- `HandleNetworkError()` - Network/connectivity errors
- `ShowConfirmation()`, `ShowWarning()`, `ShowInformation()` - Non-error dialogs (these are the ONLY acceptable MessageBox wrappers)

**Severity classification system**:
- `Enum_ErrorSeverity.Low` - Warnings, validation errors (amber icon, continues normally)
- `Enum_ErrorSeverity.Medium` - Recoverable errors, retry available (red icon, operation failed)
- `Enum_ErrorSeverity.High` - Critical errors, data integrity risk (dark red icon, immediate attention)
- `Enum_ErrorSeverity.Fatal` - Application termination required (black icon, Environment.Exit(1))

#### Error Suppression & Anti-Spam System

**Error frequency tracking** prevents dialog flooding:
- **Error Key**: `{ExceptionType}:{CallerName}:{MessageHashCode}` uniquely identifies errors
- **5-second cooldown**: Duplicate errors within window are suppressed from UI (still logged)
- **Session frequency limit**: Errors shown >10 times are permanently suppressed
- **Always logs**: Even suppressed errors are written to log files for diagnostics

**Example suppression scenario**:
1. Database connection fails at 10:00:00 → Dialog shown, logged
2. Same error at 10:00:02 → Suppressed (within 5s), logged only
3. Same error at 10:00:07 → Dialog shown again (cooldown expired), logged
4. After 10 occurrences → Permanently suppressed, logged only

#### Context Enrichment System

**Automatic caller detection** using `[CallerMemberName]` attribute:
```csharp
// Caller's method name automatically captured
Service_ErrorHandler.HandleException(ex); 
// Logs: "Error in LoadInventoryData"
```

**Context dictionary** for diagnostic data:
```csharp
var contextData = new Dictionary<string, object>
{
    ["PartID"] = partNumber,
    ["Operation"] = operationName,
    ["UserAction"] = "Transfer Inventory"
};
Service_ErrorHandler.HandleDatabaseError(ex, contextData: contextData);
// Context logged with error for root cause analysis
```

#### Specialized Error Handlers

**Database error mapping** (severity translation):
- `Enum_DatabaseEnum_ErrorSeverity.Warning` → `Enum_ErrorSeverity.Low` (UI display)
- `Enum_DatabaseEnum_ErrorSeverity.Error` → `Enum_ErrorSeverity.Medium`
- `Enum_DatabaseEnum_ErrorSeverity.Critical` → `Enum_ErrorSeverity.High`
- Triggers `ConnectionRecoveryManager.HandleConnectionLost()` automatically

**Fatal error detection** (automatic termination):
- `OutOfMemoryException`, `StackOverflowException`, `AccessViolationException`, `SecurityException`
- Shows final error dialog, logs, calls `Environment.Exit(1)`

#### EnhancedErrorDialog Features

**User-facing capabilities**:
- **Plain English summary**: Context-aware descriptions based on exception type
- **Technical details**: Full stack trace, exception type, timestamp, machine/user info
- **Call stack tree**: Visual tree with color-coded components (Controls=Purple, DAOs=Orange, Helpers=Red)
- **Retry button**: Executes provided `Func<bool>` retry action if available
- **Copy details**: Clipboard integration for error reports
- **Report issue**: Opens Form_ReportIssue with pre-populated error data
- **View logs**: Opens ViewApplicationLogsForm filtered to current user
- **Keyboard shortcuts**: Escape to close, Ctrl+C to copy details

#### Fallback Error Handling

**Three-tier fallback system** prevents error handling failures:
1. **Primary**: EnhancedErrorDialog with full features
2. **Fallback**: Simple MessageBox if EnhancedErrorDialog throws exception
3. **Last resort**: Basic system MessageBox if fallback fails

**Example fallback chain**:
```csharp
try {
    using var errorDialog = new EnhancedErrorDialog(...);
    errorDialog.ShowDialog();
} catch (Exception innerEx) {
    LoggingUtility.LogApplicationError(innerEx);
    FallbackErrorDisplay(ex, callerName); // Simple MessageBox
}
```

**Rationale**: Centralized error handling ensures every error is logged consistently, provides users with actionable feedback and retry capabilities, prevents error flooding through intelligent suppression, enables systematic debugging with rich context data, and never crashes the application due to error handling failures through multi-tier fallbacks.

### II. Structured Logging with CSV Format

**All logging MUST use LoggingUtility methods** with thread-safe CSV formatting, asynchronous file writes, and recursion prevention.

#### Multi-File Logging Strategy

**Three separate log files** for clear separation of concerns:
- `{basename}_normal.csv` - General application events, user actions, informational messages
- `{basename}_app_error.csv` - Application-level exceptions (non-database)
- `{basename}_db_error.csv` - Database-specific errors with severity levels

**File naming pattern**: `{UserName} {Timestamp}_normal.csv`
- Example: `JohnDoe 11-11-2025 @ 2-30 PM_normal.csv`
- Location: Determined by `Helper_Database_Variables.GetLogFilePathAsync()`

#### Logging Methods

**Public API** (these are the only acceptable logging methods):
```csharp
// General application events (normal.csv)
LoggingUtility.Log("User clicked Save button");
LoggingUtility.LogApplicationInfo("Application started successfully");

// Application errors (app_error.csv)
LoggingUtility.LogApplicationError(exception);

// Database errors (db_error.csv) with severity
LoggingUtility.LogDatabaseError(exception, Enum_DatabaseEnum_ErrorSeverity.Error);
```

**NEVER use**: `Console.WriteLine()`, `Debug.WriteLine()` (except in LoggingUtility itself), `Trace.WriteLine()`, direct file writes

#### CSV Format Standard

**Fixed column structure** (RFC 4180 compliant):
```
Timestamp,Level,Source,Message,Details
2025-11-11 14:30:45,INFO,Application,User logged in,
2025-11-11 14:31:12,ERROR,Database,Connection timeout,"Type: MySqlException\nStack Trace: at MySql..."
```

**Field escaping rules** (`EscapeCsvField()` implementation):
- Fields with commas → Wrapped in quotes: `"Part 123, Operation 456"`
- Fields with newlines → Wrapped in quotes: `"Line 1\nLine 2"`
- Fields with quotes → Quote doubled and wrapped: `"He said ""Hello"""`
- Empty fields → Empty string (no quotes needed)

**Timestamp format**: `yyyy-MM-dd HH:mm:ss` (sortable, Excel-friendly, unambiguous)

#### Thread-Safety Architecture

**Lock-based synchronization**:
```csharp
private static readonly Lock LogLock = new();

lock (LogLock) {
    FlushLogEntryToDisk(_normalLogFile, csvEntry);
}
```

**Thread-static recursion prevention**:
```csharp
[ThreadStatic]
private static bool _isLoggingDatabaseError;

public static void LogDatabaseError(Exception ex, ...) {
    if (_isLoggingDatabaseError) {
        // Recursion detected! Use fallback:
        Debug.WriteLine($"[DEBUG] Recursion prevented: {ex.Message}");
        File.AppendAllText(_dbErrorLogFile, fallbackCsv); // Direct write
        return;
    }
    try {
        _isLoggingDatabaseError = true; // Set per-thread flag
        // Normal logging logic with database operations
    } finally {
        _isLoggingDatabaseError = false; // Always reset
    }
}
```

**Why thread-static?**: Each thread gets its own `_isLoggingDatabaseError` flag, preventing recursion within a thread while allowing concurrent logging from multiple threads.

#### Asynchronous Write Pattern

**Fire-and-forget async writes** (non-blocking):
```csharp
_ = Task.Run(async () => {
    const int maxRetries = 5;
    const int delayMs = 100;
    
    for (int attempt = 0; attempt < maxRetries; attempt++) {
        try {
            await using var fs = new FileStream(filePath, FileMode.Append, 
                FileAccess.Write, FileShare.Write);
            await using var writer = new StreamWriter(fs);
            await writer.WriteLineAsync(logEntry);
            break; // Success
        }
        catch (IOException) when (attempt < maxRetries - 1) {
            await Task.Delay(delayMs); // Retry with delay
        }
    }
});
```

**Benefits**:
- **Non-blocking**: UI thread never waits for disk I/O
- **Retry logic**: Handles transient file locking (5 attempts, 100ms delay)
- **File sharing**: `FileShare.Write` allows multi-process access
- **Error isolation**: Write failures don't crash application

#### CSV Header Management

**Automatic header writing** for new files:
```csharp
private static readonly HashSet<string> _filesWithHeaders = new();

bool needsHeader = false;
lock (LogLock) {
    if (!_filesWithHeaders.Contains(filePath) && !File.Exists(filePath)) {
        needsHeader = true;
        _filesWithHeaders.Add(filePath);
    }
}
if (needsHeader) {
    await writer.WriteLineAsync("Timestamp,Level,Source,Message,Details");
}
```

**Ensures**: Each log file starts with proper CSV header for Excel compatibility.

#### Initialization & Fallback System

**Three-tier initialization strategy**:

1. **Primary**: Database-driven log path with timeout
   ```csharp
   using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
   logFilePath = await Helper_Database_Variables.GetLogFilePathAsync(server, userName);
   ```

2. **Timeout fallback**: CommonApplicationData directory
   ```csharp
   var fallbackDir = Path.Combine(
       Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
       "MTM_WIP_Application_Winforms", "Logs", userName);
   ```

3. **Complete failure**: Disable logging (prevents crash)
   ```csharp
   _logDirectory = "";
   _normalLogFile = "";
   _dbErrorLogFile = "";
   _appErrorLogFile = "";
   ```

**Never crashes**: Application continues even if logging completely fails.

#### Lifecycle Management

**Initialization** (called at app startup):
```csharp
await LoggingUtility.InitializeLoggingAsync();
```

**Graceful shutdown** (automatic via event hook):
```csharp
AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

private static void OnProcessExit(object? sender, EventArgs e) {
    var shutdownMsg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Application exiting.";
    lock (LogLock) {
        FlushLogEntryToDisk(_normalLogFile, shutdownMsg);
        FlushLogEntryToDisk(_dbErrorLogFile, shutdownMsg);
        FlushLogEntryToDisk(_appErrorLogFile, shutdownMsg);
    }
}
```

**Log file cleanup** (maintains last 20 logs):
```csharp
await LoggingUtility.CleanUpOldLogsIfNeededAsync();
```

#### Debug Integration

**Dual output strategy** during development:
```csharp
Debug.WriteLine($"{timestamp:yyyy-MM-dd HH:mm:ss} - {message}");
// AND
FlushLogEntryToDisk(_normalLogFile, csvEntry);
```

**Debugger detection** (skips cleanup when debugging):
```csharp
if (Debugger.IsAttached) return;
// Skip app data cleanup during development
```

**Rationale**: Structured CSV logging enables systematic analysis with Excel/PowerBI, separates concerns across dedicated log files, provides thread-safe concurrent logging without deadlocks, prevents production crashes through recursion detection and async write isolation, enables real-time debugging through Debug.WriteLine integration, and gracefully handles initialization failures with multi-tier fallbacks.

### III. Model_Dao_Result Pattern for Data Layer

**All DAO methods MUST return Model_Dao_Result<T>** to provide:
- Consistent success/failure indication (`IsSuccess`, `IsFailure`)
- Standardized error messages and exception wrapping
- Row count tracking for data operations
- Status codes from stored procedures

**Database operations follow this contract**:
```csharp
public static async Task<Model_Dao_Result<DataTable>> GetSomeDataAsync(...)
{
    try {
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(...);
        return result.IsSuccess 
            ? Model_Dao_Result<DataTable>.Success(result.Data, result.StatusMessage)
            : Model_Dao_Result<DataTable>.Failure(result.ErrorMessage);
    }
    catch (Exception ex) {
        Service_ErrorHandler.HandleDatabaseError(ex, ...);
        return Model_Dao_Result<DataTable>.Failure("User-friendly message", ex);
    }
}
```

**Rationale**: Standardized result patterns eliminate ambiguity, enable consistent error handling chains, provide rich diagnostic data, and make async/await patterns cleaner.

### IV. Async-First Database Operations

**All database operations MUST be async**:
- Use `async Task<Model_Dao_Result<T>>` signatures
- Always `await` database calls - never `.Result` or `.Wait()`
- Helper_Database_StoredProcedure handles retry logic for transient errors
- Automatic exponential backoff for connection failures

**Transaction support for testing**:
- External `MySqlConnection` and `MySqlTransaction` parameters enable test isolation
- Test methods provide connection/transaction; production methods create their own
- External connections are NOT disposed by helper methods - caller owns lifecycle

**Rationale**: Async prevents UI blocking, enables better resource utilization, supports automatic retry logic, and provides clean test isolation patterns.

### V. WinForms Best Practices

**UI thread marshaling requirements**:
- Use `Invoke()` or `BeginInvoke()` when updating UI from background threads
- Data-bound controls automatically marshal to UI thread
- Long-running operations MUST use background threads or async/await

**Form lifecycle management**:
- Dispose forms properly in `using` statements or explicit `.Dispose()` calls
- Use `ShowDialog()` for modal dialogs, `Show()` for modeless windows
- Clean up event handlers in `Dispose()` override to prevent memory leaks

**Rationale**: WinForms is single-threaded and requires explicit marshaling. Proper disposal prevents memory leaks and handle exhaustion.

### VI. Stored Procedure Parameter Conventions

**Parameter prefix auto-detection using Model_ParameterPrefix_Cache**:
- Helper_Database_StoredProcedure automatically detects prefixes (`p_`, `in_`, `o_`)
- DAO methods pass parameters WITHOUT prefixes - helper adds them
- Cache queries INFORMATION_SCHEMA for actual procedure signatures
- Standard output parameters: `p_Status` (INT), `p_ErrorMsg` (VARCHAR)

**Status code conventions**:
- `1` = Success with data returned
- `0` = Success without data (valid empty result)
- Negative values = Error (specific error codes defined per procedure)

**Rationale**: Auto-detection eliminates parameter name mismatches, reduces DAO boilerplate, and ensures stored procedures can evolve independently of C# code.

## Development Standards

### Code Organization

**Namespace and folder structure**:
- `MTM_WIP_Application_Winforms.Forms.*` - UI forms and user controls
- `MTM_WIP_Application_Winforms.Data` - DAO layer (database access)
- `MTM_WIP_Application_Winforms.Services` - Business logic and cross-cutting concerns
- `MTM_WIP_Application_Winforms.Helpers` - Utility functions and shared infrastructure
- `MTM_WIP_Application_Winforms.Models` - Data models, enums, and DTOs
- `MTM_WIP_Application_Winforms.Logging` - Logging infrastructure

**Region organization in files**:
- `#region Fields` - Private fields and constants
- `#region Properties` - Public properties
- `#region Constructors` - Constructor methods
- `#region Public Methods` - Public API surface
- `#region Private Methods` - Internal implementation
- `#region Event Handlers` - UI event handlers

### Error Handling Workflow

**Exception handling chain**:
1. **DAO layer**: Catch database exceptions, log via `LoggingUtility.LogDatabaseError()`, return `Model_Dao_Result.Failure()`
2. **Service layer**: Check `result.IsFailure`, invoke `Service_ErrorHandler.HandleDatabaseError()`
3. **UI layer**: Display errors via error handler, optionally provide retry action

**Severity classification**:
- `Enum_ErrorSeverity.Low` - Validation errors, user mistakes (yellow warning)
- `Enum_ErrorSeverity.Medium` - Recoverable errors, transient failures (orange alert)
- `Enum_ErrorSeverity.High` - Critical errors requiring intervention (red error)
- `Enum_ErrorSeverity.Fatal` - Application-breaking errors, immediate shutdown

### Performance Monitoring

**Performance thresholds** (configured in `Model_Application_Variables`):
- Query operations: 500ms warning threshold
- Modification operations: 1000ms warning threshold
- Batch operations: 5000ms warning threshold
- Report generation: 2000ms warning threshold

**Automatic logging**: Helper_Database_StoredProcedure logs warnings when thresholds exceeded.

## Quality Assurance Standards

### Code Review Requirements

**All PRs must verify**:
- Exception handling uses `Service_ErrorHandler` (no direct `MessageBox.Show()`)
- Database operations return `Model_Dao_Result<T>` pattern
- Logging uses `LoggingUtility` methods (no `Console.WriteLine()`)
- Async/await used correctly (no `.Result` or `.Wait()`)
- Proper `using` statements for IDisposable resources
- UI thread marshaling for cross-thread operations

## Deployment and Versioning

### Version Numbering

**Format**: `MAJOR.MINOR.PATCH.BUILD`
- **MAJOR**: Breaking database schema changes, incompatible API changes
- **MINOR**: New features, stored procedure additions, UI enhancements
- **PATCH**: Bug fixes, performance improvements, non-breaking refactors
- **BUILD**: Automated build number (incremented per build)

**Current version** (from AssemblyInfo.cs): `6.2.1.0`

### Database Migration Strategy

**Stored procedure updates**:
- New procedures added to `Database/UpdatedStoredProcedures/`
- Validation scripts in `Database/Scripts/`
- Sync reports generated documenting changes
- Test procedures in test database before production deployment

**Schema evolution**:
- ALTER TABLE scripts in `Database/Scripts/`
- Backward compatibility maintained for 1 minor version
- Data migration scripts for breaking changes
- Rollback scripts maintained for emergency recovery

## Governance

### Constitution Authority

**This constitution supersedes all other development practices**. When conflicts arise between this constitution and:
- Individual code comments
- Legacy code patterns
- External documentation
- Verbal agreements

...the constitution prevails. Amendments require documented justification and approval.

### Amendment Process

**To amend this constitution**:
1. Document proposed change with rationale
2. Demonstrate how existing principles are insufficient
3. Analyze impact on existing codebase (breaking vs non-breaking)
4. Update version number according to semantic versioning rules
5. Update `LAST_AMENDED_DATE` and add entry to Sync Impact Report
6. Propagate changes to dependent templates and documentation

### Compliance Verification

**All PRs/reviews must**:
- Verify error handling uses Service_ErrorHandler
- Confirm logging uses LoggingUtility
- Check database operations return Model_Dao_Result
- Validate async/await patterns
- Ensure CSV log format compliance
- Test transaction rollback in integration tests

**Complexity justification required for**:
- Direct MessageBox.Show() usage (MUST justify why Service_ErrorHandler insufficient)
- Synchronous database calls (MUST justify why async impossible)
- Console.WriteLine() logging (MUST justify why LoggingUtility insufficient)
- Non-standard result types (MUST justify why Model_Dao_Result pattern inadequate)

### Runtime Development Guidance

For AI agents implementing features, refer to:
- `.github/prompts/speckit.*.prompt.md` - Feature specification and task generation workflows
- `.specify/templates/` - Standard templates for specs, plans, tasks, and checklists
- `Database/` - Database documentation, schema snapshots, and stored procedure inventories
- This constitution for non-negotiable patterns and standards

**Version**: 1.0.0 | **Ratified**: 2025-11-11 | **Last Amended**: 2025-11-11
