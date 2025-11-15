# MTM WIP Application Constitution

<!--
Sync Impact Report (2025-11-15):
- Version change: 1.0.0 → 1.1.0
- MINOR version bump: Five new core principles added
- Added Principles:
  * VII. XML Documentation Standards (mandatory documentation)
  * VIII. Null Safety Requirements (nullable reference types)
  * IX. Theme System Integration (UI consistency)
  * X. Resource Disposal (memory leak prevention) [was XI, renumbered]
  * XI. Input Validation Service (centralized validation with Service_Validation)
- Modified sections:
  * Development Standards → Added XML Documentation subsection
  * Quality Assurance Standards → Added Null Safety and Disposal verification
  * Code Review Requirements → Added Theme, Documentation, and Validation checks
- Templates requiring updates:
  ✅ constitution.md - Updated with new principles
  ⚠ plan-template.md - Needs XML documentation task phase
  ⚠ spec-template.md - Needs validation service integration guidance
  ⚠ tasks-template.md - Needs disposal verification and validation tasks
- Follow-up TODOs:
  * Implement Service_Validation (new service for centralized validation)
  * Update suggestiontextbox-constitution-audit.prompt.md with new principles
  * Create validation service specification
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

### VII. XML Documentation Standards (MANDATORY)

**All public members MUST have XML documentation** using standard tags:
- `<summary>` - Required for ALL public classes, methods, properties, events
- `<param>` - Required for each method parameter
- `<returns>` - Required for methods returning values (include Model_Dao_Result usage guidance)
- `<exception>` - Document exceptions that can be thrown
- `<remarks>` - Optional for complex scenarios, usage examples, important notes

**Inline comments (`//`) are DISCOURAGED** except for:
- Non-obvious business logic requiring explanation
- Workarounds for known issues with ticket references
- Complex algorithms where intent is not self-evident

**XML Documentation format**:
```csharp
/// <summary>
/// Updates the part in the database with current form values.
/// Handles RequiresColorCode changes with automatic cache reload.
/// </summary>
/// <param name="partId">The unique identifier for the part to update</param>
/// <param name="requiresColorCode">Whether this part requires color code tracking</param>
/// <returns>
/// Model_Dao_Result containing success/failure status.
/// Check IsSuccess before accessing Data.
/// ErrorMessage contains user-friendly message on failure.
/// </returns>
/// <exception cref="ArgumentException">Thrown when partId is null or empty</exception>
/// <remarks>
/// If RequiresColorCode changes, automatically triggers cache reload via
/// Model_Application_Variables.ReloadColorCodePartsAsync().
/// </remarks>
public async Task<Model_Dao_Result<bool>> UpdatePartAsync(string partId, bool requiresColorCode)
{
    // Implementation
}
```

**Rationale**: XML documentation enables IntelliSense for developers, maintains codebase searchability, enforces self-documenting code, and supports automated documentation generation.

### VIII. Null Safety Requirements (ENFORCED)

**Nullable reference types MUST be enabled** (`<Nullable>enable</Nullable>` in .csproj):
- Use `?` suffix for nullable reference types (`string?`, `DataRow?`)
- Use null-conditional operators (`?.`) and null-coalescing (`??`) for safe access
- Check `DBNull.Value` before accessing DataRow/DataTable columns
- Verify column existence with `DataTable.Columns.Contains()` before access

**Null safety patterns**:
```csharp
// ✅ CORRECT - Null-safe DataRow access
private void LoadPartData(DataRow? row)
{
    if (row == null)
    {
        return;
    }

    // Check column exists
    if (row.Table.Columns.Contains("PartID"))
    {
        var partIdValue = row["PartID"];
        string partId = partIdValue != DBNull.Value 
            ? partIdValue.ToString() ?? string.Empty 
            : string.Empty;
    }
}

// ❌ WRONG - Assumes non-null
private void LoadPartData(DataRow row)
{
    string partId = row["PartID"].ToString(); // Can throw NullReferenceException
}
```

**Null parameter handling**:
```csharp
// Public methods - validate parameters
public void ProcessPart(string partId)
{
    ArgumentNullException.ThrowIfNullOrEmpty(partId);
    // Safe to proceed
}

// Event handlers - nullable parameters
private void Button_Click(object? sender, EventArgs e)
{
    // sender can be null - check if needed
}
```

**Rationale**: Prevents 90% of NullReferenceExceptions at compile time, eliminates runtime null checks through static analysis, improves code reliability, and enables better IntelliSense guidance.

### IX. Theme System Integration (UI CONSISTENCY)

**All UI components MUST use the centralized theme system**:
- Forms inherit from `ThemedForm` (NEVER `Form` directly)
- User controls inherit from `ThemedUserControl` (NEVER `UserControl` directly)
- NO manual `BackColor`, `ForeColor`, or `Font` assignment - use theme tokens
- NO direct `MessageBox.Show()` - use `Service_ErrorHandler` for themed dialogs

**Theme integration pattern**:
```csharp
// ✅ CORRECT - Inherits theme automatically
public partial class EditPartForm : ThemedForm
{
    public EditPartForm()
    {
        InitializeComponent();
        // Theme automatically applied by base class
        // NO manual color/font setting needed
    }
}

// ❌ WRONG - Manual styling breaks theme consistency
public partial class EditPartForm : Form
{
    public EditPartForm()
    {
        InitializeComponent();
        this.BackColor = Color.White; // Hardcoded - breaks dark mode
        this.Font = new Font("Arial", 10); // Ignores theme
    }
}
```

**Theme-aware dialogs**:
```csharp
// ✅ CORRECT - Uses themed error handler
Service_ErrorHandler.ShowWarning("Part not found");

// ❌ WRONG - Raw MessageBox bypasses theme
MessageBox.Show("Part not found"); // Not themed, inconsistent
```

**Rationale**: Ensures consistent look/feel across application, supports dark mode and accessibility themes, centralizes UI updates (change theme tokens once, all UI updates), and improves user experience through visual consistency.

### X. Resource Disposal (PREVENT MEMORY LEAKS)

**All `IDisposable` resources MUST be properly disposed**:
- Use `using` statements for database connections, readers, streams
- Override `Dispose(bool disposing)` in forms/controls to clean up resources
- Unsubscribe from ALL event handlers in `Dispose()` to prevent memory leaks
- Dispose DataTables after use (or use `using` when appropriate)

**Disposal patterns**:
```csharp
// Database resources - using statement
public async Task<Model_Dao_Result<DataTable>> GetDataAsync()
{
    using var connection = new MySqlConnection(connectionString);
    using var command = new MySqlCommand(sql, connection);
    await connection.OpenAsync();
    
    using var reader = await command.ExecuteReaderAsync();
    var table = new DataTable();
    table.Load(reader);
    return Model_Dao_Result<DataTable>.Success(table);
    // connection, command, reader automatically disposed
}

// Form/Control disposal - event cleanup
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        try
        {
            // Unsubscribe from events to prevent memory leaks
            if (saveButton != null)
            {
                saveButton.Click -= SaveButton_Click;
            }
            
            if (partSuggestionTextBox != null)
            {
                partSuggestionTextBox.SuggestionSelected -= Part_SuggestionSelected;
            }
            
            // Dispose any IDisposable fields
            _someDisposableResource?.Dispose();
            
            // Dispose components if present
            components?.Dispose();
        }
        catch (Exception ex)
        {
            // Log but don't throw during disposal
            LoggingUtility.LogApplicationError(ex);
        }
    }

    base.Dispose(disposing);
}
```

**Rationale**: Prevents memory leaks from undisposed resources, avoids handle exhaustion in long-running applications, ensures proper cleanup of database connections, and prevents event handler reference loops.

### XI. Input Validation Service (CENTRALIZED)

**All user input MUST be validated through Service_Validation before database operations**:
- Validate at UI layer BEFORE calling DAOs
- Use `Service_Validation` for all validation logic (centralized, extensible)
- Report validation errors via `Service_ErrorHandler.HandleValidationError()`
- Apply standard transformations (trim whitespace, ToUpperInvariant for codes)

**Service_Validation architecture** (extensible validator pattern):
```csharp
public static class Service_Validation
{
    // Validator registry - easily add new validators
    private static readonly Dictionary<string, IValidator> Validators = new()
    {
        ["PartNumber"] = new PartNumberValidator(),
        ["ItemType"] = new ItemTypeValidator(),
        ["ColorCode"] = new ColorCodeValidator(),
        ["Quantity"] = new NumericValidator(min: 0, allowDecimals: true),
        ["Operation"] = new OperationValidator()
    };

    /// <summary>
    /// Validates input using registered validator.
    /// Returns ValidationResult with success/failure and normalized value.
    /// </summary>
    public static ValidationResult Validate(string validatorKey, string input)
    {
        if (!Validators.TryGetValue(validatorKey, out var validator))
        {
            throw new InvalidOperationException($"No validator registered for '{validatorKey}'");
        }

        return validator.Validate(input);
    }

    /// <summary>
    /// Registers a new validator for a field type.
    /// Use during application startup or in audit process when new validation needed.
    /// </summary>
    public static void RegisterValidator(string key, IValidator validator)
    {
        Validators[key] = validator;
    }
}

// Validator interface
public interface IValidator
{
    ValidationResult Validate(string input);
}

// Validation result
public class ValidationResult
{
    public bool IsValid { get; set; }
    public string NormalizedValue { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
}
```

**Usage in forms**:
```csharp
private bool ValidateInput()
{
    // Part number validation
    var partResult = Service_Validation.Validate("PartNumber", partNumberTextBox.Text);
    if (!partResult.IsValid)
    {
        Service_ErrorHandler.HandleValidationError(
            partResult.ErrorMessage,
            field: partResult.FieldName,
            controlName: nameof(EditPartForm));
        partNumberTextBox.Focus();
        partNumberTextBox.SelectAll();
        return false;
    }
    
    // Use normalized value (trimmed, uppercased)
    partNumberTextBox.Text = partResult.NormalizedValue;

    // Quantity validation
    var qtyResult = Service_Validation.Validate("Quantity", quantityTextBox.Text);
    if (!qtyResult.IsValid)
    {
        Service_ErrorHandler.HandleValidationError(
            qtyResult.ErrorMessage,
            field: qtyResult.FieldName,
            controlName: nameof(EditPartForm));
        quantityTextBox.Focus();
        return false;
    }

    return true;
}

private async void SaveButton_Click(object? sender, EventArgs e)
{
    // ALWAYS validate before DAO
    if (!ValidateInput())
    {
        return; // Stop - don't call DAO with invalid data
    }

    // Safe to proceed with validated input
    var result = await Dao_Part.UpdatePartAsync(...);
}
```

**Standard validator implementations**:
```csharp
// Part number validator (format + business rules)
public class PartNumberValidator : IValidator
{
    private static readonly Regex Pattern = new(@"^R-[A-Z0-9]+-\d{2}$", RegexOptions.Compiled);

    public ValidationResult Validate(string input)
    {
        // Normalize first
        string normalized = input.Trim().ToUpperInvariant();

        // Required check
        if (string.IsNullOrEmpty(normalized))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Part number is required.",
                FieldName = "PartNumber"
            };
        }

        // Format check
        if (!Pattern.IsMatch(normalized))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Part number must follow format: R-ABC-01\n" +
                              "(R- prefix, alphanumeric code, dash, 2-digit number)",
                FieldName = "PartNumber"
            };
        }

        return new ValidationResult
        {
            IsValid = true,
            NormalizedValue = normalized,
            FieldName = "PartNumber"
        };
    }
}

// Numeric validator (reusable with configuration)
public class NumericValidator : IValidator
{
    private readonly decimal _min;
    private readonly decimal _max;
    private readonly bool _allowDecimals;

    public NumericValidator(decimal min = 0, decimal max = decimal.MaxValue, bool allowDecimals = true)
    {
        _min = min;
        _max = max;
        _allowDecimals = allowDecimals;
    }

    public ValidationResult Validate(string input)
    {
        string normalized = input.Trim();

        if (!decimal.TryParse(normalized, out decimal value))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = $"Must be a valid {(_allowDecimals ? "number" : "whole number")}.",
                FieldName = "Quantity"
            };
        }

        if (!_allowDecimals && value != Math.Floor(value))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Decimal values are not allowed.",
                FieldName = "Quantity"
            };
        }

        if (value < _min || value > _max)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = $"Must be between {_min} and {_max}.",
                FieldName = "Quantity"
            };
        }

        return new ValidationResult
        {
            IsValid = true,
            NormalizedValue = normalized,
            FieldName = "Quantity"
        };
    }
}
```

**Extensibility for audit process**:
```csharp
// During audit, if new validation type needed, easily register:
Service_Validation.RegisterValidator("LocationCode", new LocationCodeValidator());
Service_Validation.RegisterValidator("WorkOrder", new WorkOrderValidator());

// No need to modify Service_Validation class - just implement IValidator
```

**Rationale**: Centralized validation ensures consistency across forms, reduces code duplication, provides immediate user feedback with clear error messages, improves data quality by preventing bad data from reaching database, and offers easy extensibility through validator registration pattern enabling audit process to add new validators without modifying core service.

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

### XML Documentation

**All public members require XML documentation**:
- Methods: `<summary>`, `<param>`, `<returns>`, `<exception>` (if applicable)
- Classes: `<summary>` describing purpose and usage
- Properties: `<summary>` with get/set behavior if non-trivial
- Events: `<summary>` describing when event is raised

**Model_Dao_Result documentation standard**:
```csharp
/// <returns>
/// Model_Dao_Result containing DataTable on success.
/// Check IsSuccess before accessing Data.
/// ErrorMessage contains user-friendly message on failure.
/// </returns>
```

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
- XML documentation present on all public members
- Nullable reference types enabled and properly annotated
- Forms/Controls inherit from `ThemedForm`/`ThemedUserControl`
- Event handlers unsubscribed in `Dispose()` override
- Input validation uses `Service_Validation` before DAO calls

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
- Verify XML documentation coverage on public members
- Check null safety annotations (nullable types properly marked)
- Confirm theme integration (ThemedForm/ThemedUserControl inheritance)
- Validate disposal patterns (event cleanup, resource disposal)
- Verify input validation through Service_Validation

**Complexity justification required for**:
- Direct MessageBox.Show() usage (MUST justify why Service_ErrorHandler insufficient)
- Synchronous database calls (MUST justify why async impossible)
- Console.WriteLine() logging (MUST justify why LoggingUtility insufficient)
- Non-standard result types (MUST justify why Model_Dao_Result pattern inadequate)
- Missing XML documentation (MUST justify why documentation not applicable)
- Nullable warnings suppression (MUST justify why null case impossible)
- Direct Form/UserControl inheritance (MUST justify why theme bypass needed)
- Missing Dispose() override (MUST justify no disposable resources)
- Bypassing Service_Validation (MUST justify why validation not applicable)

### Runtime Development Guidance

For AI agents implementing features, refer to:
- `.github/prompts/speckit.*.prompt.md` - Feature specification and task generation workflows
- `.specify/templates/` - Standard templates for specs, plans, tasks, and checklists
- `Database/` - Database documentation, schema snapshots, and stored procedure inventories
- This constitution for non-negotiable patterns and standards

**Version**: 1.1.0 | **Ratified**: 2025-11-11 | **Last Amended**: 2025-11-15
