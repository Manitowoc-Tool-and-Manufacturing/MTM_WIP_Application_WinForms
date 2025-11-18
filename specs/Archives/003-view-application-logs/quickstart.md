# Quickstart: View Application Logs Feature

**Date**: 2025-10-26  
**Feature**: 003-view-application-logs  
**Phase**: 1 - Design

## Overview

This quickstart guide provides developers with setup instructions, development workflow, and testing procedures for implementing the View Application Logs feature.

---

## Prerequisites

### Required Software

-   Visual Studio 2022 (17.8+) or VS Code with C# DevKit
-   .NET 8.0 SDK
-   MySQL 5.7+ (MAMP or standalone)
-   Git for version control

### Required Knowledge

-   C# 12 language features
-   Windows Forms (WinForms) development
-   Async/await patterns
-   Regular expressions
-   File I/O operations

### Project Context

-   Branch: `003-view-application-logs`
-   Target framework: .NET 8.0 Windows Forms
-   Database: MySQL 5.7+ (not required for this feature - file-based only)

---

## Environment Setup

### 1. Clone and Build

```powershell
# Navigate to repository root
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms

# Checkout feature branch
git checkout 003-view-application-logs

# Restore dependencies
dotnet restore

# Build in Debug mode (uses test database - not needed for logs feature)
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug

# Verify build succeeds
# Expected: 0 errors, 0 warnings
```

### 2. Configure Network Log Path

**Location**: `Models/Model_AppVariables.cs` or create new configuration setting

```csharp
// Add to Model_AppVariables or new config class
public static class LogViewerConfiguration
{
    /// <summary>
    /// Base path for network log storage. Configurable per environment.
    /// </summary>
    public static string BaseLogPath { get; set; } = @"\\networkpath\logs";

    /// <summary>
    /// Maximum file size (in bytes) before requiring windowing.
    /// Default: 10MB per FR-044.
    /// </summary>
    public static long MaxFileSize { get; set; } = 10 * 1024 * 1024;

    /// <summary>
    /// Number of entries to load per window.
    /// Default: 1000 per research decision.
    /// </summary>
    public static int WindowSize { get; set; } = 1000;
}
```

**For Local Development**: Update `BaseLogPath` to local test directory:

```csharp
// Development override
#if DEBUG
    public static string BaseLogPath { get; set; } = @"C:\TestLogs";
#else
    public static string BaseLogPath { get; set; } = @"\\networkpath\logs";
#endif
```

### 3. Create Test Log Files

Run the test data generator script to create sample log files:

```powershell
# Navigate to test data directory
cd .specify\scripts\powershell

# Generate test logs (creates Normal, App Error, DB Error samples)
.\Generate-TestLogFiles.ps1 -OutputPath "C:\TestLogs" -UserCount 3 -SessionsPerUser 5

# Expected output:
# - C:\TestLogs\user1\session1_normal.log
# - C:\TestLogs\user1\session1_app_error.log
# - C:\TestLogs\user1\session1_db_error.log
# - ... (3 users × 5 sessions × 3 file types = 45 files)
```

**Manual Test File Creation** (if script unavailable):

```
C:\TestLogs\
├── bjones\
│   ├── 20251026_143000_normal.log
│   ├── 20251026_143000_app_error.log
│   └── 20251026_143000_db_error.log
└── ajohnson\
    ├── 20251026_150000_normal.log
    ├── 20251026_150000_app_error.log
    └── 20251026_150000_db_error.log
```

**Sample Normal Log Entry**:

```
[2025-10-26 14:30:15.234] HIGH ✅ PROCEDURE md_part_ids_Get_All - Query executed successfully
Details: {"elapsed_ms": 45, "rows_returned": 127, "status": 0}
```

**Sample Application Error Entry**:

```
ERROR TYPE: NullReferenceException
Exception Message: Object reference not set to an instance of an object.
Stack Trace:
   at MTM_WIP_Application_Winforms.Forms.MainForm.LoadInventory() in MainForm.cs:line 234
   at MTM_WIP_Application_Winforms.Forms.MainForm.OnLoad(EventArgs e) in MainForm.cs:line 145
```

**Sample Database Error Entry**:

```
SEVERITY: ERROR
[2025-10-26 14:32:10.567] MySqlException: Timeout expired. The timeout period elapsed prior to completion of the operation.
Stack Trace:
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader() in MySqlCommand.cs:line 456
   at Helper_Database_StoredProcedure.ExecuteDataTableWithStatus() in Helper_Database_StoredProcedure.cs:line 789
```

---

## Development Workflow

### Phase 1: Core Models (Estimated: 2 hours)

**Task**: Create data model classes per `data-model.md`

```powershell
# Create model files
New-Item -Path "Models\Model_LogEntry.cs" -ItemType File
New-Item -Path "Models\Model_LogFile.cs" -ItemType File
New-Item -Path "Models\Model_LogFilter.cs" -ItemType File
New-Item -Path "Models\Model_UserLogDirectory.cs" -ItemType File
New-Item -Path "Models\LogFormat.cs" -ItemType File
```

**Implementation Order**:

1. `LogFormat.cs` - Enumeration (no dependencies)
2. `Model_LogEntry.cs` - Core entity with factory methods
3. `Model_LogFile.cs` - File metadata entity
4. `Model_LogFilter.cs` - Filter criteria entity
5. `Model_UserLogDirectory.cs` - Directory aggregation entity

**Validation**: Build project, verify no errors.

```powershell
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug
```

### Phase 2: Security & Path Handling (Estimated: 2 hours)

**Task**: Implement Helper_LogPath with security validation

```powershell
# Create helper file
New-Item -Path "Helpers\Helper_LogPath.cs" -ItemType File
```

**Key Methods**:

-   `GetUserLogDirectory(username)` - Validates and returns user directory path
-   `GetLogFilePath(username, filename)` - Constructs full file path with validation
-   `ValidateUsername(username)` - Regex validation
-   `PreventPathTraversal(path)` - Security check against base directory

**Testing**:

```csharp
// Manual test in Program.cs or test form
var testCases = new[]
{
    ("bjones", true),           // Valid
    ("user-123", true),         // Valid with hyphen
    ("../admin", false),        // Path traversal attempt
    ("user;drop", false),       // Invalid characters
    ("", false)                 // Empty
};

foreach (var (username, shouldSucceed) in testCases)
{
    try
    {
        var path = Helper_LogPath.GetUserLogDirectory(username);
        Console.WriteLine($"✅ {username}: {path}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ {username}: {ex.Message}");
    }
}
```

### Phase 3: Log Parsing Service (Estimated: 4 hours)

**Task**: Implement Service_LogParser with regex patterns and format detection

```powershell
# Create service files
New-Item -Path "Services\Service_LogParser.cs" -ItemType File
```

**Key Methods**:

-   `DetectFormat(firstLine)` - Returns LogFormat based on content analysis
-   `ParseEntry(rawText, format)` - Returns Model_LogEntry for given format
-   `ParseNormalLog(rawText)` - Regex parsing for Service_DebugTracer format
-   `ParseApplicationError(rawText)` - Parsing for error log format
-   `ParseDatabaseError(rawText)` - Parsing for database error format

**Testing**:

```csharp
// Test parser with sample entries
var normalLog = "[2025-10-26 14:30:15.234] HIGH ✅ PROCEDURE md_part_ids_Get_All - Query executed successfully\nDetails: {\"elapsed_ms\": 45}";
var parsedNormal = Service_LogParser.ParseEntry(normalLog, LogFormat.Normal);

Console.WriteLine($"Timestamp: {parsedNormal.Timestamp}");
Console.WriteLine($"Level: {parsedNormal.Level}");
Console.WriteLine($"Source: {parsedNormal.Source}");
Console.WriteLine($"Parse Success: {parsedNormal.ParseSuccess}");

// Expected output:
// Timestamp: 2025-10-26 14:30:15.234
// Level: HIGH
// Source: PROCEDURE md_part_ids_Get_All
// Parse Success: True
```

**Performance Validation**:

```csharp
var stopwatch = Stopwatch.StartNew();
for (int i = 0; i < 1000; i++)
{
    var entry = Service_LogParser.ParseEntry(normalLog, LogFormat.Normal);
}
stopwatch.Stop();

// Target: <10ms for 1000 parses
Console.WriteLine($"Parse rate: {stopwatch.ElapsedMilliseconds}ms for 1000 entries");
```

### Phase 4: File Reader Service (Estimated: 4 hours)

**Task**: Implement Service_LogFileReader with async I/O and windowing

```powershell
# Create service file
New-Item -Path "Services\Service_LogFileReader.cs" -ItemType File
```

**Key Methods**:

-   `EnumerateLogFilesAsync(userDirectory)` - Returns List<Model_LogFile>
-   `LoadEntriesAsync(filePath, startIndex, count)` - Window-based loading
-   `GetTotalEntryCountAsync(filePath)` - Counts entries without full load
-   `DetectLogFormat(filePath)` - Returns LogFormat from filename

**Testing**:

```csharp
// Test file enumeration
var files = await Service_LogFileReader.EnumerateLogFilesAsync(@"C:\TestLogs\bjones");
Console.WriteLine($"Found {files.Count} log files");

foreach (var file in files)
{
    Console.WriteLine($"{file.FileName} - {file.LogType} - {file.FileSizeDisplay}");
}

// Test windowed loading
var entries = await Service_LogFileReader.LoadEntriesAsync(
    @"C:\TestLogs\bjones\20251026_143000_normal.log",
    0,
    1000
);

Console.WriteLine($"Loaded {entries.Count} entries");
```

**Performance Validation** (SC-003):

```csharp
var stopwatch = Stopwatch.StartNew();
var entries = await Service_LogFileReader.LoadEntriesAsync(filePath, 0, 1000);
stopwatch.Stop();

// Target: <2000ms for 1000 entries
Console.WriteLine($"Load time: {stopwatch.ElapsedMilliseconds}ms for {entries.Count} entries");
Assert.IsTrue(stopwatch.ElapsedMilliseconds < 2000, "Load exceeded 2-second target");
```

### Phase 5: WinForms UI (Estimated: 8 hours)

**Task**: Create ViewApplicationLogsForm with region-organized layout

```powershell
# Create form files
New-Item -Path "Forms\ViewLogs\" -ItemType Directory
New-Item -Path "Forms\ViewLogs\ViewApplicationLogsForm.cs" -ItemType File
# Designer and resx files will be auto-generated
```

**Form Structure** (follow constitution region organization):

```csharp
public partial class ViewApplicationLogsForm : Form
{
    #region Fields
    private Helper_StoredProcedureProgress _progressHelper;
    private Service_LogFileReader _logFileReader;
    private List<Model_LogEntry> _currentEntries;
    private List<int> _filteredIndices;
    private int _currentFilteredIndex = 0;
    private Model_LogFilter _activeFilter;
    private bool _isParsedView = true;
    #endregion

    #region Properties
    public string SelectedUsername { get; private set; }
    public LogFormat SelectedLogType { get; private set; }
    #endregion

    #region Progress Control Methods
    private void SetProgressControls(ProgressBar progress, Label status)
    {
        _progressHelper = Helper_StoredProcedureProgress.Create(progress, status, this);
    }
    #endregion

    #region Constructors
    public ViewApplicationLogsForm()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        InitializeAsync().ConfigureAwait(false);
    }

    public ViewApplicationLogsForm(string username) : this()
    {
        SelectedUsername = username;
    }
    #endregion

    #region Initialization
    private async Task InitializeAsync()
    {
        _logFileReader = new Service_LogFileReader();
        _activeFilter = Model_LogFilter.CreateDefault();
        await LoadUserListAsync();
        WireUpEvents();
    }
    #endregion

    #region File Operations
    private async Task LoadUserListAsync() { /* ... */ }
    private async Task LoadLogFilesAsync(string username) { /* ... */ }
    private async Task LoadLogFileAsync(string filePath) { /* ... */ }
    #endregion

    #region Entry Navigation
    private void ShowCurrentEntry() { /* ... */ }
    private void NavigateNext() { /* ... */ }
    private void NavigatePrevious() { /* ... */ }
    #endregion

    #region Filtering
    private void ApplyFilter() { /* ... */ }
    private void ClearFilters() { /* ... */ }
    private void UpdateSeverityOptions() { /* ... */ }
    #endregion

    #region Button Clicks
    private async void btnRefresh_Click(object sender, EventArgs e) { /* ... */ }
    private void btnPrevious_Click(object sender, EventArgs e) { /* ... */ }
    private void btnNext_Click(object sender, EventArgs e) { /* ... */ }
    private void btnToggleView_Click(object sender, EventArgs e) { /* ... */ }
    private async void btnExport_Click(object sender, EventArgs e) { /* ... */ }
    private void btnCopyEntry_Click(object sender, EventArgs e) { /* ... */ }
    private void btnOpenDirectory_Click(object sender, EventArgs e) { /* ... */ }
    #endregion

    #region ComboBox & UI Events
    private async void cmbUsers_SelectedIndexChanged(object sender, EventArgs e) { /* ... */ }
    private void lstLogFiles_SelectedIndexChanged(object sender, EventArgs e) { /* ... */ }
    private void dtpStartDate_ValueChanged(object sender, EventArgs e) { /* ... */ }
    private void txtSearch_TextChanged(object sender, EventArgs e) { /* ... */ }
    #endregion

    #region Helpers
    private void WireUpEvents() { /* ... */ }
    private void UpdateEntryCountDisplay() { /* ... */ }
    private string FormatEntryForDisplay(Model_LogEntry entry) { /* ... */ }
    #endregion

    #region Cleanup
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _logFileReader?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
    #endregion
}
```

**Layout Implementation**:

-   Use TableLayoutPanel for main layout (FR-055)
-   Set AutoScaleMode.Dpi (FR-052)
-   MinimumSize: 1200x800 (FR-056)
-   Proper Padding (10px) and Margin (5px) (FR-057)

**Testing UI**:

1. Run application in Debug mode
2. Navigate to Settings → View Application Logs
3. Verify user dropdown populates
4. Select test user, verify file list appears
5. Click log file, verify entries display
6. Test Next/Previous navigation
7. Apply filters, verify entry count updates
8. Toggle Raw/Parsed view
9. Test Export and Copy functionality

### Phase 6: Integration & Error Handling (Estimated: 2 hours)

**Task**: Wire up error dialog integration and Service_ErrorHandler

**Error Dialog Changes**:

```csharp
// In ErrorDialog form, add "View Logs" button handler
private void btnViewLogs_Click(object sender, EventArgs e)
{
    var currentUsername = Model_AppVariables.CurrentUser?.Username ?? "Unknown";
    var logViewerForm = new ViewApplicationLogsForm(currentUsername);
    logViewerForm.Show();
}
```

**Error Handling Pattern**:

```csharp
try
{
    await LoadLogFileAsync(filePath);
}
catch (UnauthorizedAccessException ex)
{
    Service_ErrorHandler.HandleException(
        ex,
        ErrorSeverity.Medium,
        retryAction: () => LoadLogFileAsync(filePath),
        contextData: new Dictionary<string, object>
        {
            ["Username"] = SelectedUsername,
            ["FilePath"] = Path.GetFileName(filePath)
        },
        controlName: nameof(ViewApplicationLogsForm)
    );
}
```

---

## Testing Procedures

### Manual Validation Scenarios

#### Test Scenario 1: User Selection and File Loading

**Steps**:

1. Open View Application Logs form
2. Verify user dropdown shows all users with log files
3. Select "bjones" from dropdown
4. Verify file list updates within 500ms (SC-002)
5. Verify files grouped by type (Normal, App Error, DB Error)
6. Verify files sorted newest first

**Expected**: File list populates correctly with type badges and metadata

#### Test Scenario 2: Log Entry Parsing and Display

**Steps**:

1. Select Normal log file
2. Verify first entry displays with fields: Timestamp, Level, Emoji, Source, Message, Details
3. Click Next button
4. Verify entry 2 displays with different values
5. Click Previous button
6. Verify returns to entry 1
7. Verify navigation responds within 50ms (SC-004)

**Expected**: Parsed entries display correctly with type-appropriate fields

#### Test Scenario 3: Filtering

**Steps**:

1. Load log file with 1000+ entries
2. Apply date range filter (yesterday to today)
3. Verify entry count updates: "Entry 1 of X (Showing X of 1000)"
4. Check "HIGH" severity only
5. Verify count decreases further
6. Enter search text "timeout"
7. Verify filtering completes within 300ms (SC-005)
8. Click "Clear Filters"
9. Verify all entries accessible again

**Expected**: Filtering works correctly and meets performance targets

#### Test Scenario 4: Error Handling

**Steps**:

1. Disconnect network (if using network path)
2. Attempt to load user logs
3. Verify user-friendly error message appears (no internal paths revealed)
4. Click Retry button
5. Reconnect network
6. Verify retry succeeds

**Expected**: Graceful error handling with retry support

#### Test Scenario 5: Performance Validation

**Steps**:

1. Create test file with 1000 entries
2. Measure load time (use stopwatch in code)
3. Verify <2 seconds (SC-003)
4. Navigate through 50 entries
5. Verify each navigation <50ms (SC-004)
6. Apply complex filter
7. Verify filter <300ms (SC-005)

**Expected**: All performance targets met

### Automated Testing (Future)

Once infrastructure is in place, create integration tests:

```csharp
[TestClass]
public class Service_LogParser_Tests
{
    [TestMethod]
    public void ParseNormalLog_ValidEntry_ReturnsSuccessfulParse()
    {
        // Arrange
        var rawText = "[2025-10-26 14:30:15.234] HIGH ✅ PROCEDURE md_part_ids_Get_All - Query executed successfully";

        // Act
        var entry = Service_LogParser.ParseEntry(rawText, LogFormat.Normal);

        // Assert
        Assert.IsTrue(entry.ParseSuccess);
        Assert.AreEqual("HIGH", entry.Level);
        Assert.AreEqual("PROCEDURE md_part_ids_Get_All", entry.Source);
    }
}
```

---

## Debugging Tips

### Common Issues

**Issue**: User dropdown is empty

-   **Cause**: Network path not accessible or no log files exist
-   **Fix**: Verify BaseLogPath configuration, check directory exists, ensure user subdirectories present

**Issue**: Parsing fails for entries

-   **Cause**: Log format doesn't match regex pattern
-   **Fix**: Enable debug logging in Service_LogParser, compare actual vs expected format, adjust regex

**Issue**: UI freezes during file loading

-   **Cause**: Synchronous file I/O or missing async/await
-   **Fix**: Verify all file operations use async methods, check for blocking .Result/.Wait() calls

**Issue**: High memory usage

-   **Cause**: Loading entire file instead of windowing
-   **Fix**: Verify Service_LogFileReader uses window loading, check WindowSize configuration

**Issue**: Path traversal security error

-   **Cause**: Invalid username or filename with ".." sequences
-   **Fix**: Verify Helper_LogPath validation logic, check input sanitization

### Debugging Tools

**Enable Verbose Logging**:

```csharp
// In Program.cs or form constructor
Service_DebugTracer.SetDebugLevel(DebugLevel.HIGH);

```

**Performance Profiling**:

```csharp
// Wrap operations with Stopwatch
var sw = Stopwatch.StartNew();
await LoadLogFileAsync(filePath);
sw.Stop();

```

**Regex Testing**:
Use online tools like [regex101.com](https://regex101.com) to test parsing patterns before implementing.

---

## Next Steps

After completing implementation:

1. **Generate tasks.md**: Run `/speckit.tasks` command to create detailed task breakdown
2. **Code Review**: Review implementation against constitution principles (region organization, async patterns, error handling)
3. **Manual Testing**: Execute all test scenarios from spec.md User Stories
4. **Performance Validation**: Verify all success criteria (SC-001 through SC-012)
5. **Documentation**: Update XML comments, add inline comments for non-obvious logic
6. **Pull Request**: Create PR with test results and validation documentation

---

## References

-   **Feature Specification**: `specs/003-view-application-logs/spec.md`
-   **Implementation Plan**: `specs/003-view-application-logs/plan.md`
-   **Research Decisions**: `specs/003-view-application-logs/research.md`
-   **Data Model**: `specs/003-view-application-logs/data-model.md`
-   **Instruction Files**:
    -   `.github/instructions/csharp-dotnet8.instructions.md`
    -   `.github/instructions/ui-scaling-consistency.instructions.md`
    -   `.github/instructions/winforms-responsive-layout.instructions.md`
    -   `.github/instructions/security-best-practices.instructions.md`
    -   `.github/instructions/performance-optimization.instructions.md`
