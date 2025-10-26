# Feature Specification: View Application Logs with User Selection

**Feature Branch**: `feature/view-application-logs`  
**Created**: 2025-10-25  
**Last Updated**: 2025-10-26  
**Status**: Active Development  
**Compliance**: Enforces ui-scaling-consistency, winforms-responsive-layout, security-best-practices, performance-optimization, mysql-database, csharp-dotnet8, code-review-standards

**Cross-References**:
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling, AutoScaleMode.Dpi
- `.github/instructions/winforms-responsive-layout.instructions.md` - TableLayoutPanel patterns, responsive architecture
- `.github/instructions/security-best-practices.instructions.md` - Input validation, file path security
- `.github/instructions/performance-optimization.instructions.md` - Async/await, file I/O, memory management
- `.github/instructions/mysql-database.instructions.md` - Not directly applicable (log files, not database)
- `.github/instructions/csharp-dotnet8.instructions.md` - C# language patterns, WinForms conventions
- `.github/instructions/code-review-standards.instructions.md` - Quality gates, testing requirements

**Input**: Create log viewer window with user selection, log type filtering (Normal/Application Error/Database Error), file browsing, parsed log entry display, and filtering capabilities for accessing logs from network storage

**Log File Types**:
- `*_normal.log` - Normal application logging (Service_DebugTracer output with JSON and emoji markers)
- `*_app_error.log` - Application error logging (exceptions with stack traces)
- `*_db_error.log` - Database error logging (MySQL exceptions with severity levels)

---

## User Scenarios & Testing *(mandatory)*

### User Story 1 - View Logs for Any User (Priority: P1)

Developers can select any user from a dropdown and view that user's application logs from network storage, enabling troubleshooting of issues reported by specific users.

**Why this priority**: Core functionality. Developers need to investigate issues for specific users by reviewing their logs. Without user selection, tool is limited to current developer's logs only.

**Independent Test**: Open View Application Logs window, select "bjones" from user dropdown, verify log files for bjones load from network storage path. Select different user "ajohnson", verify different log files load.

**Acceptance Scenarios**:

1. **Given** window opens, **When** user dropdown displays, **Then** dropdown is populated with all users who have generated logs
2. **Given** "All Users" is selected, **When** log files load, **Then** system-wide logs from all users are displayed
3. **Given** "bjones" is selected from dropdown, **When** file list refreshes, **Then** only bjones' log files appear
4. **Given** logs are stored at `\\server\logs\MTM_Application\{username}\`, **When** user is selected, **Then** system retrieves logs from correct network path
5. **Given** network path is inaccessible, **When** loading fails, **Then** error message displays and suggests checking network connection

---

### User Story 2 - Browse and Load Log Files (Priority: P1)

Developers can see a list of available log files for selected user, grouped by log type (Normal/App Error/DB Error), sorted by date, with file size information, and click to load contents into parsed viewer.

**Why this priority**: Essential navigation. Users generate multiple log files over time across three different categories. Developers need to select the correct time period and log type for investigation.

**Independent Test**: Select user with 10 log sessions (30 total files: 10 normal, 10 app error, 10 db error). Verify file list shows all files grouped by type, ordered newest first. Verify each file shows date, time, size, and type indicator. Click normal log file. Verify that file's contents load into log entry display.

**Acceptance Scenarios**:

1. **Given** selected user has 15 log sessions (45 files total), **When** file list loads, **Then** files appear grouped by type (Normal/App Error/DB Error) and sorted by date descending (newest first)
2. **Given** file list displays, **When** viewing file information, **Then** each file shows filename, type icon/badge, date/time, and file size in KB/MB
3. **Given** normal log file is clicked, **When** file loads, **Then** log entries are parsed using Service_DebugTracer format and first entry displays
4. **Given** app error log file is clicked, **When** file loads, **Then** error entries are parsed with exception/stack trace format
5. **Given** db error log file is clicked, **When** file loads, **Then** database error entries are parsed with severity levels
6. **Given** file has 543 log entries, **When** file loads, **Then** entry count displays "Entry 1 of 543"
7. **Given** "Refresh" button is clicked, **When** file list reloads, **Then** new files created since last load appear in list
8. **Given** log type filter is selected (e.g., "Show Normal Logs Only"), **When** applied, **Then** only files of that type appear in list

---

### User Story 3 - View Parsed Log Entries (Priority: P1)

Developers can view log entries in structured format with separate fields appropriate to the log type: Normal logs show timestamp/level/source/message/JSON details, Application Error logs show timestamp/exception/stack trace, Database Error logs show timestamp/severity/message/stack trace.

**Why this priority**: Raw log text is difficult to read across three different formats. Parsed format dramatically improves readability and enables understanding of complex errors and performance traces.

**Independent Test**: Load normal log file, navigate to performance entry with JSON. Verify Timestamp shows "2025-10-26 14:46:32.820", Level shows "HIGH", Emoji indicator "✅", Source shows "PROCEDURE md_part_ids_Get_All", Message shows main text, Details shows formatted JSON. Switch to app error log. Verify format changes to show Exception type, Message, and Stack Trace fields. Click Next button. Verify entry 2 displays with different values.

**Acceptance Scenarios**:

**Normal Log Format (_normal.log)**:
1. **Given** normal log entry is displayed, **When** viewing parsed fields, **Then** shows: Timestamp, Level (LOW/MEDIUM/HIGH/DATA), Emoji Indicator, Source/Component, Message, JSON Details (if present)
2. **Given** log level is "HIGH", **When** displayed, **Then** Level field shows emoji indicator (✅ ⏱️ 🗄️) based on operation type
3. **Given** log has JSON data block, **When** displayed, **Then** Details field shows formatted JSON with syntax highlighting
4. **Given** log has performance metrics, **When** displayed, **Then** shows elapsed time, status code, and result summary

**Application Error Format (_app_error.log)**:
5. **Given** app error entry is displayed, **When** viewing parsed fields, **Then** shows: Timestamp, Error Type label, Exception Message, Stack Trace
6. **Given** error entry, **When** displayed, **Then** Exception Message field has red background indicator
7. **Given** stack trace exists, **When** displayed, **Then** multi-line textbox shows formatted stack trace with preserved line breaks

**Database Error Format (_db_error.log)**:
8. **Given** db error entry is displayed, **When** viewing parsed fields, **Then** shows: Timestamp, Severity (WARNING/ERROR/CRITICAL), Exception Type, Message, Stack Trace
9. **Given** severity is "CRITICAL", **When** displayed, **Then** Severity field has dark red background
10. **Given** severity is "WARNING", **When** displayed, **Then** Severity field has yellow background

**Navigation**:
11. **Given** Message field contains 500 characters, **When** displayed, **Then** textbox is multi-line and shows full message with scrollbar if needed
12. **Given** "Previous" button is clicked, **When** current entry is 5, **Then** entry 4 displays
13. **Given** "Next" button is clicked, **When** current entry is 5, **Then** entry 6 displays
14. **Given** current entry is 1, **When** "Previous" is clicked, **Then** button is disabled or wraps to last entry
15. **Given** current entry is last, **When** "Next" is clicked, **Then** button is disabled or wraps to first entry

---

### User Story 4 - Filter Log Entries (Priority: P2)

Developers can filter visible log entries by date range, log type (Normal/App Error/DB Error), severity level (for normal logs: LOW/MEDIUM/HIGH/DATA; for db errors: WARNING/ERROR/CRITICAL), source component, and search text to focus on relevant entries during investigation.

**Why this priority**: Log files often contain thousands of entries across multiple types. Filtering is essential to find specific issues without manual scrolling.

**Independent Test**: Load normal log with 1000 entries. Apply log type filter (Normal logs only). Verify count updates. Apply severity filter (HIGH only). Verify only HIGH entries appear in navigation (e.g., "Entry 1 of 23"). Switch to db error log. Apply severity filter (CRITICAL only). Verify filtering works with different severity scheme. Add date range filter (last 24 hours). Verify count decreases further. Use search to find entries containing "timeout". Verify navigation only includes matching entries.

**Acceptance Scenarios**:

1. **Given** date range filter is applied (10/24/2025 - 10/25/2025), **When** filters apply, **Then** only entries within date range are navigable
2. **Given** log type filter dropdown shows options (All Types/Normal/Application Error/Database Error), **When** "Normal" selected, **Then** only normal log entries are navigable
3. **Given** normal log is loaded, **When** severity checkboxes show (LOW, MEDIUM, HIGH, DATA), **Then** unchecking "LOW" and "MEDIUM" filters to HIGH and DATA only
4. **Given** database error log is loaded, **When** severity checkboxes adapt to show (WARNING, ERROR, CRITICAL), **Then** filtering works with db-specific severity levels
5. **Given** source filter dropdown shows all components from current log, **When** "Dao_Inventory" is selected, **Then** only entries from Dao_Inventory are navigable
6. **Given** search text "connection timeout" is entered, **When** filters apply, **Then** only entries containing that phrase in any field are navigable
7. **Given** multiple filters are active, **When** "Clear Filters" is clicked, **Then** all filters reset and all entries become navigable again
8. **Given** filters are applied, **When** entry count updates, **Then** displays "Showing 15 of 543 entries"
9. **Given** log type is switched via filter, **When** new type selected, **Then** system loads most recent file of that type automatically

---

### User Story 5 - Switch Between Parsed and Raw View (Priority: P2)

Developers can toggle between parsed structured view and raw log text view for cases where original formatting is needed or parsing fails.

**Why this priority**: Parsing may not work for all log formats. Developers sometimes need to see exact raw text, especially for copy/paste or unusual formatting.

**Independent Test**: View parsed entry. Click "Raw View" toggle. Verify textbox shows raw log line exactly as in file. Toggle back to "Parsed View". Verify fields repopulate.

**Acceptance Scenarios**:

1. **Given** parsed view is active, **When** "Raw View" toggle is clicked, **Then** single multiline textbox displays raw log text for current entry
2. **Given** raw view is active, **When** "Parsed View" toggle is clicked, **Then** structured fields reappear with parsed data
3. **Given** log entry cannot be parsed, **When** entry loads, **Then** system automatically switches to Raw View and shows parse error message

---

### User Story 6 - Export and Copy Log Data (Priority: P2)

Developers can export filtered log entries to file or copy current entry to clipboard for sharing with team or including in documentation.

**Why this priority**: Enables collaboration and documentation. Developers need to share findings with team or create bug reports.

**Independent Test**: Apply filter showing 30 entries. Click "Export Visible". Verify text file contains all 30 entries with timestamp, level, message. Select entry 5. Click "Copy Entry". Paste into notepad. Verify formatted entry text appears.

**Acceptance Scenarios**:

1. **Given** 30 filtered entries exist, **When** "Export Visible" is clicked, **Then** SaveFileDialog opens and file is saved with all 30 entries in readable format
2. **Given** current entry is displayed, **When** "Copy Entry" is clicked, **Then** entry copies to clipboard as formatted text with all fields
3. **Given** parsed view is active, **When** copy occurs, **Then** clipboard contains formatted structure (Timestamp: ... Level: ... Message: ...)
4. **Given** raw view is active, **When** copy occurs, **Then** clipboard contains raw log line text

---

### User Story 7 - Access from Error Dialog (Priority: P3)

Users who click "View Logs" from error dialog have the log viewer open with their own logs pre-selected, providing immediate context for the error they just encountered.

**Why this priority**: Convenience feature. Reduces clicks needed to view relevant logs during error investigation.

**Independent Test**: Trigger error as user "bjones". Click "View Logs" button in error dialog. Verify View Application Logs window opens with "bjones" pre-selected in user dropdown and current date's log file loaded.

**Acceptance Scenarios**:

1. **Given** error occurs for user "bjones", **When** "View Logs" button clicked from error dialog, **Then** View Application Logs opens with "bjones" selected
2. **Given** log viewer opens from error dialog, **When** loading completes, **Then** most recent log file is selected and loaded
3. **Given** log viewer opens from Settings menu, **When** loading completes, **Then** user dropdown shows "Select User" placeholder and no logs loaded until selection made

---

---

## Testing and Validation *(mandatory)*

### Manual Testing Checklist

#### Resolution and DPI Testing (Per FR-031 to FR-037)
- [ ] **1920x1080 @ 96 DPI (100%)** - Standard desktop
  - All controls visible without scrolling
  - Buttons minimum 75x32 pixels
  - Text readable in all fields
- [ ] **1920x1080 @ 120 DPI (125%)** - Common laptop
  - Controls scale proportionally
  - No clipping or overlap
  - Margins/padding consistent
- [ ] **2560x1440 @ 144 DPI (150%)** - High-res laptop
  - Touch targets adequate (48px+)
  - Layout maintains proportions
- [ ] **3840x2160 @ 192 DPI (200%)** - 4K display
  - All text legible
  - Buttons easily clickable
  - No layout breaks
- [ ] **1366x768 @ 96 DPI** - Minimum resolution
  - Form fits on screen
  - Scrollbars only in entry details
  - Critical controls visible

#### Functional Testing - User Selection (US-1)
- [ ] Open form, verify user dropdown populates within 2 seconds
- [ ] Select user "bjones", verify file list updates for that user
- [ ] Select "All Users", verify combined file list displays
- [ ] Network path inaccessible scenario:
  - Disconnect from network or rename share
  - Select user, verify error message: "Unable to access log files"
  - Verify no exception details revealed to user
  - Check log for detailed error (path, exception)

#### Functional Testing - File Browsing (US-2)
- [ ] File list shows filename, date, size for each entry
- [ ] Files sorted newest first (descending date)
- [ ] Click file, verify content loads within 1 second (small file)
- [ ] Click file, verify progress indicator for large file (>5MB)
- [ ] Click "Refresh", verify list updates with new files
- [ ] Auto-refresh checkbox:
  - Enable, wait 5 seconds, verify list refreshes
  - Minimize form, verify timer pauses (performance test)
  - Restore form, verify timer resumes

#### Functional Testing - Entry Display (US-3)
- [ ] Load log with 500+ entries
- [ ] Verify first entry displays in parsed view
- [ ] Check all fields populated: Timestamp, Level, Source, Message, Details
- [ ] Verify color coding: Error=Red, Warning=Yellow, Info=Blue
- [ ] Click "Next", verify entry 2 displays
- [ ] Click "Previous", verify entry 1 displays
- [ ] Navigate to last entry, verify "Next" wraps to first (or disables)
- [ ] Entry count shows "Entry X of 543"

#### Functional Testing - Filtering (US-4)
- [ ] Set date range (last 24 hours), click "Apply"
  - Verify entry count updates: "Showing 45 of 543"
  - Verify navigation only shows matching entries
- [ ] Uncheck "Info" and "Debug" severity levels
  - Verify only Error/Warning entries navigable
- [ ] Select source "Dao_Inventory" from dropdown
  - Verify only entries from that component appear
- [ ] Enter search text "timeout"
  - Verify only matching entries shown
  - Verify search across all fields (Message, Details, Source)
- [ ] Click "Clear Filters"
  - Verify all filters reset
  - Verify count returns to "Entry X of 543"

#### Functional Testing - View Modes (US-5)
- [ ] Load entry in parsed view, verify all fields display
- [ ] Toggle "Raw View", verify original log line displays
- [ ] Toggle back to "Parsed View", verify fields repopulate
- [ ] Load malformed entry (invalid format)
  - Verify auto-switches to Raw View
  - Verify parse error message displays

#### Functional Testing - Export and Copy (US-6)
- [ ] Apply filter showing 30 entries
- [ ] Click "Export Visible"
  - SaveFileDialog appears
  - Select path, save
  - Open exported file, verify 30 entries present
  - Verify format: Timestamp | Level | Source | Message
- [ ] Navigate to entry 5
- [ ] Click "Copy Entry"
  - Paste into Notepad
  - Verify formatted text with all fields

#### Functional Testing - Integration (US-7)
- [ ] Trigger error in application as user "bjones"
- [ ] Click "View Logs" from error dialog
  - Form opens with "bjones" pre-selected
  - Most recent log file loaded
  - Current day's entries visible
- [ ] Open from Settings menu
  - Form opens with no user selected
  - Placeholder "Select User" in dropdown
  - No log file loaded until selection

#### Security Testing (FR-045 to FR-050)
- [ ] **Path Traversal Attack**:
  - Manually edit user selection (if possible via debugging)
  - Try user name: `../../Windows/System32`
  - Verify: Security exception, error logged, user sees generic message
- [ ] **Regex DoS Attack**:
  - Enter search pattern: `(a+)+$` with 50 'a's
  - Verify: Search completes within 1 second or times out safely
- [ ] **File Path Exposure**:
  - Cause various file errors (permissions, not found, etc.)
  - Verify: User messages never show `\\server\logs\` paths
- [ ] **Credential Security**:
  - Inspect code for hardcoded paths or credentials
  - Verify: All paths from config, no hardcoded UNC paths with credentials

#### Performance Testing (FR-038 to FR-044)
- [ ] **Large File Handling**:
  - Load log file > 50MB
  - Verify: UI remains responsive during load
  - Verify: Progress indicator updates
  - Verify: No freezing or "Not Responding"
- [ ] **Navigation Speed**:
  - Navigate through 1000+ entries
  - Verify: Previous/Next responds < 100ms
- [ ] **Memory Management**:
  - Open 10 different log files sequentially
  - Check Task Manager memory
  - Verify: Memory doesn't grow unbounded (file streams disposed)
- [ ] **Auto-Refresh Performance**:
  - Enable auto-refresh
  - Minimize form
  - Wait 30 seconds
  - Verify: Timer did not fire while minimized (check logs)

#### Error Handling Testing (FR-058 to FR-060)
- [ ] **File Deleted Mid-Session**:
  - Load file, navigate to entry 50
  - Delete file from file system
  - Click "Next"
  - Verify: Service_ErrorHandler displays "Log file no longer exists"
- [ ] **Network Disconnection**:
  - Load remote log file
  - Disconnect network
  - Click "Refresh"
  - Verify: Error dialog with retry option
- [ ] **Parse Failure**:
  - Create test log with invalid format
  - Load file
  - Verify: Falls back to Raw View, shows parse warning

---

### Success Criteria

**Must Pass (BLOCKING)**:
- ✅ All Priority 1 functional tests pass (User Selection, File Browsing, Entry Display)
- ✅ Zero compilation errors
- ✅ Zero DPI scaling violations (`validate_ui_scaling` passes)
- ✅ Zero security vulnerabilities (`check_security` passes)
- ✅ All async operations use proper await (no blocking)
- ✅ Form.MinimumSize set and tested at 1366x768
- ✅ AutoScaleMode.Dpi verified in Designer.cs

**Should Pass (HIGH PRIORITY)**:
- ✅ All Priority 2 functional tests pass (Filtering, Export, Integration)
- ✅ UI response time < 100ms for all interactions
- ✅ Large file handling (50MB+) without freezing
- ✅ Memory leak testing passes (10+ file loads)
- ✅ Security tests pass (path traversal, regex timeout)
- ✅ Error handling uses Service_ErrorHandler consistently

**Nice to Have (MEDIUM PRIORITY)**:
- ✅ Priority 3 functional tests pass (Edge cases, malformed entries)
- ✅ Auto-refresh performs well (pauses when minimized)
- ✅ Export format is user-friendly and readable
- ✅ Copy to clipboard preserves formatting

---

## Implementation Guidance

### Phase 1: Foundation (Days 1-2)

#### Create Project Structure
```csharp
// Models/Model_LogEntry.cs
public class Model_LogEntry
{
    public DateTime Timestamp { get; set; }
    public LogLevel Level { get; set; }
    public string Source { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
    public int? ThreadId { get; set; }
    public string RawText { get; set; }
    public int LineNumber { get; set; }
}

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error,
    Fatal
}

// Models/Model_LogFile.cs
public class Model_LogFile
{
    public string FilePath { get; set; }
    public string FileName { get; set; }
    public long FileSize { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastModified { get; set; }
    public string UserName { get; set; }
    public int? EntryCount { get; set; }
}

// Models/Model_LogFilter.cs
public class Model_LogFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<LogLevel> SelectedLevels { get; set; } = new();
    public string SelectedSource { get; set; }
    public string SearchText { get; set; }
    
    public bool MatchesEntry(Model_LogEntry entry)
    {
        // Filter logic implementation
    }
}
```

#### Create Helper_LogParser
```csharp
// Helpers/Helper_LogParser.cs
public static class Helper_LogParser
{
    private static readonly Regex _logPattern = new Regex(
        @"^\[(?<timestamp>[\d\-\s:\.]+)\]\s+\[(?<level>\w+)\]\s+\[(?<source>[\w\.]+)\]\s+(?<message>.+)$",
        RegexOptions.Compiled | RegexOptions.Multiline,
        TimeSpan.FromSeconds(1) // ReDoS protection (FR-050)
    );
    
    /// <summary>
    /// Parses a log file line into a structured LogEntry.
    /// </summary>
    /// <param name="rawLine">Raw log line text</param>
    /// <param name="lineNumber">Line number in file</param>
    /// <returns>Parsed LogEntry or null if parsing fails</returns>
    public static Model_LogEntry ParseLogLine(string rawLine, int lineNumber)
    {
        try
        {
            var match = _logPattern.Match(rawLine);
            if (!match.Success)
                return null;
            
            return new Model_LogEntry
            {
                Timestamp = DateTime.Parse(match.Groups["timestamp"].Value),
                Level = Enum.Parse<LogLevel>(match.Groups["level"].Value, ignoreCase: true),
                Source = match.Groups["source"].Value,
                Message = match.Groups["message"].Value.Trim(),
                RawText = rawLine,
                LineNumber = lineNumber
            };
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex, $"Failed to parse log line {lineNumber}");
            return null;
        }
    }
    
    /// <summary>
    /// Asynchronously reads and parses a log file.
    /// </summary>
    public static async Task<List<Model_LogEntry>> ParseLogFileAsync(string filePath, IProgress<int> progress = null)
    {
        var entries = new List<Model_LogEntry>();
        int lineNumber = 0;
        
        try
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, bufferSize: 4096, useAsync: true);
            using var reader = new StreamReader(stream);
            
            string line;
            string detailsBuffer = null;
            Model_LogEntry currentEntry = null;
            
            while ((line = await reader.ReadLineAsync()) != null)
            {
                lineNumber++;
                
                var entry = ParseLogLine(line, lineNumber);
                if (entry != null)
                {
                    // New entry found
                    if (currentEntry != null)
                    {
                        currentEntry.Details = detailsBuffer?.Trim();
                        entries.Add(currentEntry);
                    }
                    currentEntry = entry;
                    detailsBuffer = null;
                }
                else
                {
                    // Continuation line (details, stack trace, etc.)
                    detailsBuffer += line + Environment.NewLine;
                }
                
                if (lineNumber % 100 == 0)
                    progress?.Report(lineNumber);
            }
            
            // Add last entry
            if (currentEntry != null)
            {
                currentEntry.Details = detailsBuffer?.Trim();
                entries.Add(currentEntry);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex, $"Failed to parse log file: {filePath}");
            throw;
        }
        
        return entries;
    }
}
```

### Phase 2: Form Implementation (Days 3-5)

#### Form Constructor and Initialization (FR-063)
```csharp
// Forms/ViewLogs/Form_ViewApplicationLogs.cs
public partial class Form_ViewApplicationLogs : Form
{
    #region Fields
    private string _preSelectedUser;
    private List<Model_LogEntry> _logEntries;
    private List<Model_LogEntry> _filteredEntries;
    private int _currentEntryIndex;
    private Model_LogFilter _currentFilter;
    private System.Windows.Forms.Timer _autoRefreshTimer;
    private bool _isRawView;
    #endregion
    
    #region Properties
    private string NetworkLogPath => 
        Helper_Database_Variables.GetConfigValue("NetworkLogPath") ?? 
        @"\\server\logs\MTM_Application";
    #endregion
    
    #region Constructors
    public Form_ViewApplicationLogs(string preSelectedUser = null)
    {
        InitializeComponent();
        
        // DPI scaling (REQUIRED per FR-031, FR-032, FR-033)
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        // Performance optimization (FR-044)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        
        // Form constraints (FR-036)
        this.MinimumSize = new Size(1000, 700);
        this.StartPosition = FormStartPosition.CenterParent;
        
        // Initialize state
        _preSelectedUser = preSelectedUser;
        _logEntries = new List<Model_LogEntry>();
        _filteredEntries = new List<Model_LogEntry>();
        _currentFilter = new Model_LogFilter();
        
        // Setup auto-refresh timer (FR-023)
        _autoRefreshTimer = new System.Windows.Forms.Timer
        {
            Interval = 5000, // 5 seconds
            Enabled = false
        };
        _autoRefreshTimer.Tick += AutoRefreshTimer_Tick;
        
        // Wire up events
        WireUpEvents();
    }
    #endregion
    
    #region Form Load and Initialization
    private async void Form_ViewApplicationLogs_Load(object sender, EventArgs e)
    {
        try
        {
            await LoadUsersAsync(); // FR-003
            
            if (!string.IsNullOrEmpty(_preSelectedUser))
            {
                cmbUsers.SelectedItem = _preSelectedUser; // FR-027
                await LoadFileListAsync(_preSelectedUser);
            }
        }
        catch (Exception ex)
        {
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.High, 
                contextData: new Dictionary<string, object> { ["Action"] = "FormLoad" });
        }
    }
    
    private void WireUpEvents()
    {
        this.Load += Form_ViewApplicationLogs_Load;
        this.VisibleChanged += Form_VisibleChanged; // FR-044
        
        cmbUsers.SelectedIndexChanged += cmbUsers_SelectedIndexChanged;
        lstFiles.SelectedIndexChanged += lstFiles_SelectedIndexChanged;
        
        btnPrevious.Click += btnPrevious_Click;
        btnNext.Click += btnNext_Click;
        btnRefresh.Click += btnRefresh_Click;
        btnApplyFilter.Click += btnApplyFilter_Click;
        btnClearFilter.Click += btnClearFilter_Click;
        btnCopyEntry.Click += btnCopyEntry_Click;
        btnExportVisible.Click += btnExportVisible_Click;
        btnOpenDirectory.Click += btnOpenDirectory_Click;
        
        chkAutoRefresh.CheckedChanged += chkAutoRefresh_CheckedChanged;
        rbParsedView.CheckedChanged += ViewMode_CheckedChanged;
        rbRawView.CheckedChanged += ViewMode_CheckedChanged;
    }
    #endregion
    
    #region File and User Selection
    /// <summary>
    /// Loads list of users who have log directories. (FR-003)
    /// </summary>
    private async Task LoadUsersAsync()
    {
        try
        {
            // Validate base path (FR-047)
            if (!Directory.Exists(NetworkLogPath))
            {
                throw new DirectoryNotFoundException($"Log path not accessible: {NetworkLogPath}");
            }
            
            var basePath = Path.GetFullPath(NetworkLogPath);
            
            await Task.Run(() =>
            {
                var users = Directory.EnumerateDirectories(basePath)
                    .Select(Path.GetFileName)
                    .Where(u => !string.IsNullOrEmpty(u))
                    .OrderBy(u => u)
                    .ToList();
                
                // UI update must happen on UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    cmbUsers.Items.Clear();
                    cmbUsers.Items.Add("Select User..."); // FR-028
                    cmbUsers.Items.Add("All Users"); // FR-002
                    foreach (var user in users)
                    {
                        cmbUsers.Items.Add(user);
                    }
                    cmbUsers.SelectedIndex = 0;
                });
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            LoggingUtility.LogApplicationError(ex, $"Access denied to log path: {NetworkLogPath}");
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.High, 
                userMessage: "Unable to access log files. Check network permissions."); // FR-049
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.High,
                userMessage: "Unable to load user list. Check network connection."); // FR-029, FR-049
        }
    }
    
    /// <summary>
    /// Loads file list for selected user. (FR-005, FR-006, FR-007)
    /// </summary>
    private async Task LoadFileListAsync(string userName)
    {
        try
        {
            // Validate user input (FR-046)
            if (!Regex.IsMatch(userName, @"^[A-Za-z0-9_-]+$") && userName != "All Users")
            {
                throw new ArgumentException("Invalid user name format");
            }
            
            // Build and validate path (FR-004, FR-045, FR-047)
            string userPath = Path.Combine(NetworkLogPath, userName);
            string fullPath = Path.GetFullPath(userPath);
            
            if (!fullPath.StartsWith(Path.GetFullPath(NetworkLogPath), StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityException("Path traversal attempt detected");
            }
            
            await Task.Run(() =>
            {
                var files = Directory.EnumerateFiles(fullPath, "*.log")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.LastWriteTime) // FR-007
                    .Select(f => new Model_LogFile
                    {
                        FilePath = f.FullName,
                        FileName = f.Name,
                        FileSize = f.Length,
                        LastModified = f.LastWriteTime,
                        UserName = userName
                    })
                    .ToList();
                
                // UI update on UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    lstFiles.Items.Clear();
                    foreach (var file in files)
                    {
                        var displayText = $"{file.FileName} | {FormatFileSize(file.FileSize)} | {file.LastModified:yyyy-MM-dd HH:mm}";
                        lstFiles.Items.Add(new { Display = displayText, Data = file });
                    }
                    
                    lblFileCount.Text = $"{files.Count} file(s) found";
                });
            });
        }
        catch (DirectoryNotFoundException ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleValidationError(
                $"Log directory not found for user: {userName}",
                nameof(cmbUsers));
        }
        catch (SecurityException ex)
        {
            LoggingUtility.LogApplicationError(ex, $"Security violation: {userName}");
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Fatal,
                userMessage: "Security error accessing logs."); // FR-049
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                userMessage: "Unable to load log files."); // FR-029
        }
    }
    
    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        int order = 0;
        double size = bytes;
        
        while (size >= 1024 && order < sizes.Length - 1)
        {
            order++;
            size /= 1024;
        }
        
        return $"{size:0.##} {sizes[order]}";
    }
    #endregion
    
    // ... Continue with remaining regions ...
}
```

### Phase 3: Navigation and Filtering (Days 6-7)

*Implementation continues with Entry Navigation, Filtering, Export, etc. following patterns above*

---

## MCP Tool Integration

### Pre-Implementation Validation
```bash
# Verify UI scaling compliance before development
mcp_mtm-workflow_validate_ui_scaling(
  source_dir: "Forms/ViewLogs",
  recursive: true
)

# Check security patterns
mcp_mtm-workflow_check_security(
  source_dir: "Forms/ViewLogs",
  scan_type: "all",
  recursive: true
)
```

### During Development
```bash
# Validate DAO patterns (if logs are stored in database later)
# Not applicable for file-based logs

# Check error handling compliance
mcp_mtm-workflow_validate_error_handling(
  source_dir: "Forms/ViewLogs",
  recursive: true
)

# Verify performance patterns
mcp_mtm-workflow_analyze_performance(
  source_dir: "Forms/ViewLogs",
  focus: "ui",
  recursive: true
)
```

### Post-Implementation Validation
```bash
# Final build check
mcp_mtm-workflow_validate_build(
  workspace_root: "C:\\...\\MTM_WIP_Application_WinForms"
)

# XML documentation coverage
mcp_mtm-workflow_check_xml_docs(
  source_dir: "Forms/ViewLogs",
  min_coverage: 80,
  recursive: true
)

# Security scan
mcp_mtm-workflow_check_security(
  source_dir: ".",
  scan_type: "all",
  recursive: true
)
```

---

## Quality Gates (Code Review Checklist)

### BLOCKING (Must Pass Before Merge)
- [ ] Zero compilation errors
- [ ] Zero DPI scaling violations (AutoScaleMode.Dpi, Core_Themes calls present)
- [ ] Zero security vulnerabilities (path validation, no hardcoded paths)
- [ ] All file I/O operations are async
- [ ] No `.Result` or `.Wait()` calls on UI thread
- [ ] All error handling uses Service_ErrorHandler (no MessageBox.Show)
- [ ] Form.MinimumSize = new Size(1000, 700) set
- [ ] Region organization matches FR-055
- [ ] Dispose pattern implemented correctly (FR-064)

### HIGH PRIORITY (Should Pass)
- [ ] All Priority 1 functional tests pass
- [ ] UI response time < 100ms measured
- [ ] Large file test (50MB) passes without freezing
- [ ] Memory leak test passes (10+ file loads, stable memory)
- [ ] Path traversal security test passes
- [ ] ReDoS protection test passes (regex timeout)
- [ ] XML documentation coverage >= 80%
- [ ] No hardcoded strings (use constants or config)

### MEDIUM PRIORITY (Nice to Have)
- [ ] Priority 2 functional tests pass
- [ ] Auto-refresh performance optimized (pauses when minimized)
- [ ] Export format is user-friendly
- [ ] Copy to clipboard preserves formatting
- [ ] Code follows all naming conventions (FR-065)
- [ ] Helper methods extracted for reusability

---

## Deployment Checklist

### Pre-Deployment
1. [ ] Run all manual tests at multiple DPI settings
2. [ ] Run all MCP validation tools, confirm pass
3. [ ] Verify network path configuration in production config
4. [ ] Test with actual production log files (sanitized copy)
5. [ ] Performance test with largest expected log file
6. [ ] Update user documentation with new feature

### Deployment Steps
1. [ ] Merge feature branch to master
2. [ ] Build Release configuration
3. [ ] Deploy to test environment
4. [ ] Smoke test with production data
5. [ ] Deploy to production
6. [ ] Monitor for errors in first 24 hours

### Post-Deployment
1. [ ] Verify users can access feature from Settings menu
2. [ ] Verify integration with error dialog works
3. [ ] Check performance metrics (response times)
4. [ ] Gather user feedback
5. [ ] Update PatchNotes.md with deployment summary

---

## Appendix: Instruction File Compliance Matrix

| Requirement ID | Instruction File | Section | Compliance Status |
|----------------|------------------|---------|-------------------|
| FR-031 to FR-037 | ui-scaling-consistency.instructions.md | AutoScaleMode, DPI scaling | ✅ Enforced |
| FR-038 to FR-044 | performance-optimization.instructions.md | Async/await, file I/O | ✅ Enforced |
| FR-045 to FR-050 | security-best-practices.instructions.md | Input validation, path security | ✅ Enforced |
| FR-051 to FR-065 | csharp-dotnet8.instructions.md | C# patterns, naming, regions | ✅ Enforced |
| Layout patterns | winforms-responsive-layout.instructions.md | TableLayoutPanel, responsive | ✅ Enforced |
| Code review | code-review-standards.instructions.md | Quality gates, testing | ✅ Enforced |

**Compliance Score**: 100% - All instruction files integrated

---

- **Very large log files**: What if log file is 500MB with 1 million entries? Implement paging/windowing, load entries in chunks of 1000.
- **Malformed log entries**: What if log line doesn't match expected pattern? Show in Raw View with parse warning, include in navigation.
- **Missing log files**: What if network path exists but user has no logs? Show "No log files found for this user" message.
- **Network disconnection during load**: What if network drops while loading file? Show error, allow retry button.
- **Concurrent log writes**: What if application is writing to log while viewer is open? "Auto-Refresh" option reloads file every 5 seconds for live tailing.
- **Non-standard log formats**: What if old logs use different format? Implement format detection, try multiple parse patterns.
- **Special characters in logs**: What if log contains null bytes or control characters? Sanitize display, show as [BINARY] or escape sequences.

---

## Requirements *(mandatory)*

### Functional Requirements

#### Core Log File Access (FR-001 to FR-010)
- **FR-001**: Window MUST provide dropdown/combobox to select any user who has generated logs
  - **Security** (`.github/instructions/security-best-practices.instructions.md`): Validate user selection against directory structure to prevent path traversal attacks
  - **Input Validation**: Use allowlist of discovered users only, reject arbitrary input
- **FR-002**: User dropdown MUST include "All Users" option for system-wide logs
  - **Performance** (`.github/instructions/performance-optimization.instructions.md`): Lazy-load file lists only when user selection changes
- **FR-003**: System MUST retrieve user list from network log storage directory structure
  - **Security**: Use `Path.Combine()` for all path operations, never string concatenation
  - **Async**: Use `Directory.EnumerateDirectories()` with async wrapper
- **FR-004**: System MUST construct log path as `{NetworkLogPath}\{SelectedUser}\`
  - **Security**: Validate final path remains within expected network root using `Path.GetFullPath()` comparison
  - **C# Pattern** (`.github/instructions/csharp-dotnet8.instructions.md`): Use `Path.Combine()` and validate case-sensitivity
- **FR-005**: Window MUST display list of available log files for selected user, grouped by log type
  - **UI Layout**: Use DataGridView or ListBox with grouping/filtering capability
  - **Log Types**: Display badge or icon for each type (Normal 📊, App Error ❌, DB Error 🗄️)
- **FR-006**: System MUST recognize three log file types by filename suffix:
  - `*_normal.log` - Normal application logging (Service_DebugTracer format)
  - `*_app_error.log` - Application exception logging (LoggingUtility.LogApplicationError format)
  - `*_db_error.log` - Database exception logging (LoggingUtility.LogDatabaseError format)
- **FR-007**: File list MUST show filename, log type, date/time, and file size for each log file
  - **Performance**: Cache FileInfo data, don't re-query on every selection change
- **FR-008**: File list MUST be sorted by date descending (newest first) within each log type group
  - **Performance**: Use LINQ OrderByDescending with deferred execution
- **FR-009**: File list MUST provide log type filter dropdown (All Types/Normal/Application Error/Database Error)
  - **UI Pattern**: Filter updates file list without reloading from disk
- **FR-010**: Clicking log file MUST load contents and parse using appropriate format for log type
  - **Async**: MUST use `async Task LoadLogFileAsync(string filePath, LogFileType logType)` pattern
  - **Performance**: Load file in background, show progress indicator

#### Log Parsing and Display (FR-011 to FR-020)
#### Log Parsing and Display (FR-011 to FR-020)
- **FR-011**: System MUST parse Normal log entries (_normal.log) into fields: Timestamp, Level (LOW/MEDIUM/HIGH/DATA), Emoji, Source, Message, JSON Details
  - **C# Pattern**: Use dedicated `Helper_LogParser.ParseNormalLogEntry()` method with regex
  - **Format**: `2025-10-26 14:46:32 - [14:46:32.820] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (62ms) - Status: 1`
  - **JSON Handling**: Multi-line JSON blocks following entries must be captured in Details field
- **FR-012**: System MUST parse Application Error entries (_app_error.log) into fields: Timestamp, Error Type, Message, Stack Trace
  - **Format Line 1**: `2025-10-26 14:46:32 - Application Error - {Exception.Message}`
  - **Format Line 2**: `2025-10-26 14:46:32 - Stack Trace - {Exception.StackTrace}`
  - **Pairing**: Two-line format must be paired into single entry
- **FR-013**: System MUST parse Database Error entries (_db_error.log) into fields: Timestamp, Severity (WARNING/ERROR/CRITICAL), Message, Stack Trace
  - **Format Line 1**: `2025-10-26 14:46:32 - Database Error [CRITICAL] - {Exception.Message}`
  - **Format Line 2**: `2025-10-26 14:46:32 - Stack Trace - {Exception.StackTrace}`
  - **Severity Extraction**: Parse severity level from [SEVERITY] tag
- **FR-014**: System MUST display parsed entry in field layout appropriate to log type:
  - **Normal Log**: Timestamp | Level + Emoji | Source | Thread (if present) | Message (multiline) | JSON Details (multiline)
  - **App Error**: Timestamp | Error Label | Message (multiline) | Stack Trace (multiline)
  - **DB Error**: Timestamp | Severity + Color | Message (multiline) | Stack Trace (multiline)
- **FR-015**: System MUST color-code severity indicators:
  - **Normal Log Levels**: LOW=Gray, MEDIUM=Blue, HIGH=Green, DATA=Cyan
  - **DB Error Severity**: WARNING=Yellow, ERROR=Red, CRITICAL=DarkRed
  - **App Error**: Always Red indicator for error entries
- **FR-016**: System MUST display emoji indicators from Normal logs (✅ ⏱️ 🗄️ ➡️ ⬅️) alongside level in separate label or icon
  - **UI Pattern**: Use Label control with font supporting emoji (Segoe UI Emoji)
- **FR-017**: System MUST format JSON blocks in Details field with:
  - **Indentation**: Preserve original JSON formatting
  - **Syntax Highlighting**: Optional - use RichTextBox with custom coloring or monospace TextBox
  - **Scrolling**: Multi-line textbox with vertical scrollbar for large JSON
- **FR-018**: System MUST provide Previous/Next navigation buttons to move between entries
  - **UI Scaling**: Buttons MUST have `MinimumSize = new Size(75, 32)` for DPI compliance
  - **Async**: Navigation must not block UI thread even for large files
- **FR-019**: System MUST display entry position indicator "Entry X of Y (Filtered: Z shown)"
  - **Format**: "Entry 5 of 543 (Filtered: 23 shown)" when filters active
  - **Format**: "Entry 5 of 543" when no filters active
- **FR-020**: System MUST gracefully handle malformed log entries by falling back to raw view
  - **Pattern**: Catch parsing exceptions, log with `LoggingUtility`, display raw line in Details field

#### Filtering and Search (FR-021 to FR-029)
#### Filtering and Search (FR-021 to FR-029)
- **FR-021**: System MUST provide date range filter (Start Date, End Date DateTimePickers)
  - **UI Layout**: Place in filter panel with `AutoSize` rows
- **FR-022**: System MUST provide log type filter dropdown (All Types, Normal, Application Error, Database Error)
  - **Behavior**: Selecting type filters file list to show only files of that type
  - **Auto-load**: When type changes, automatically loads most recent file of that type
- **FR-023**: System MUST adapt severity filter checkboxes based on current log type:
  - **Normal Logs**: LOW, MEDIUM, HIGH, DATA checkboxes
  - **Database Errors**: WARNING, ERROR, CRITICAL checkboxes
  - **Application Errors**: No severity filter (all are errors)
  - **Mixed View**: Show union of all severity types when multiple log types visible
- **FR-024**: System MUST provide source component filter (dropdown populated from log data)
  - **Performance**: Build source list once when file loads, cache for session
  - **Normal Logs**: Extract from source field (e.g., "Helper_Database_StoredProcedure")
  - **Error Logs**: Extract from stack trace top frame (e.g., "Dao_Inventory")
- **FR-025**: System MUST provide free-text search across all log entry fields
  - **Security**: Sanitize search text, use regex safely (avoid ReDoS attacks)
  - **Performance**: Use compiled regex when same pattern is reused
  - **Scope**: Search Timestamp, Level/Severity, Source, Message, Details/Stack Trace
- **FR-026**: Filtering MUST update entry count and navigation to show only matching entries
  - **Performance**: Filter in background thread, don't block UI
  - **Status**: Update label to show "Showing 15 of 543" or "Entry 5 of 23 (Filtered from 543)"
- **FR-027**: System MUST provide "Clear Filters" button to reset all filters
  - **UI Pattern**: Reset all filter controls to default state
  - **Behavior**: Date range clears to "All dates", severity to "All levels", source to "All sources", search text to empty
- **FR-028**: System MUST persist filter state when navigating between files of same type
  - **Pattern**: Store filter state in form-level fields, reapply when loading same log type
- **FR-029**: System MUST provide quick filter buttons for common scenarios:
  - **"Errors Only"**: Shows only Application Error and Database Error log types
  - **"Performance"**: Filters Normal logs to HIGH level with "PERFORMANCE" in message
  - **"Today"**: Date range to current date only

#### View Modes and Actions (FR-030 to FR-036)
#### View Modes and Actions (FR-030 to FR-036)
- **FR-030**: System MUST provide toggle between "Parsed View" and "Raw View"
  - **Layout**: Use RadioButtons or toggle button
  - **Behavior**: Parsed view shows structured fields, Raw view shows original log line(s)
- **FR-031**: Raw View MUST display original log line text without parsing
  - **Performance**: Store raw text pointer/offset, don't duplicate in memory
  - **Format**: For error logs with 2 lines, show both lines concatenated with separator
- **FR-032**: System MUST provide "Refresh" button to reload file list
  - **Async**: Use `async void btnRefresh_Click` with proper error handling
  - **Behavior**: Reloads file list, preserves current log type filter
- **FR-033**: System MUST provide "Auto-Refresh" checkbox to reload file list every 5 seconds
  - **Pattern**: Use `System.Windows.Forms.Timer`, dispose on form close
  - **Performance**: Pause timer when form minimized/hidden (FR-044)
- **FR-034**: System MUST provide "Export Visible" button to save filtered entries to file
  - **Async**: Use async file I/O (`StreamWriter` with `async` methods)
  - **Security**: Validate export path using SaveFileDialog
  - **Format**: Export as tab-delimited or CSV with columns matching log type
- **FR-035**: System MUST provide "Copy Entry" button to copy current entry to clipboard
  - **Pattern**: Use `Clipboard.SetText()` with try/catch for clipboard access failures
  - **Format**: Copy formatted text with field labels (e.g., "Timestamp: ... | Level: ... | Message: ...")
- **FR-036**: System MUST provide "Open Log Directory" button to open folder in Windows Explorer
  - **Security**: Validate path exists and is accessible before launching `Process.Start()`
  - **Behavior**: Opens currently selected user's log folder

#### Integration and Error Handling (FR-037 to FR-040)
#### Integration and Error Handling (FR-037 to FR-040)
- **FR-037**: When opened from error dialog, system MUST pre-select current user and load most recent error log
  - **Pattern**: Accept optional constructor parameters `string preSelectedUser = null, LogFileType preferredType = LogFileType.ApplicationError`
  - **Behavior**: If preferredType specified, filter to that type and load most recent file
- **FR-038**: When opened from Settings menu, system MUST show user selector without pre-selection
  - **Pattern**: Default constructor behavior when no user specified
  - **Default View**: Shows "All Types" in log type filter, no file pre-loaded
- **FR-039**: System MUST handle network path access errors gracefully with user-friendly messages
  - **Error Handling** (`.github/instructions/csharp-dotnet8.instructions.md`): Use try/catch with specific exception types
  - **Security**: Never reveal internal path structure in error messages to users
- **FR-040**: System MUST gracefully handle parse errors by falling back to raw view
  - **Pattern**: Catch parsing exceptions, log with `LoggingUtility`, switch to raw display
  - **User Message**: "Unable to parse log entry. Displaying raw format."

---

### UI Scaling and DPI Compliance (FR-041 to FR-047)
### UI Scaling and DPI Compliance (FR-041 to FR-047)
**(Per `.github/instructions/ui-scaling-consistency.instructions.md` and `.github/instructions/winforms-responsive-layout.instructions.md`)**

- **FR-041**: Form MUST use `AutoScaleMode.Dpi` with `AutoScaleDimensions = new SizeF(96F, 96F)`
- **FR-042**: Form constructor MUST call `Core_Themes.ApplyDpiScaling(this)` immediately after `InitializeComponent()`
- **FR-043**: Form constructor MUST call `Core_Themes.ApplyRuntimeLayoutAdjustments(this)` after DPI scaling
- **FR-044**: All interactive controls (buttons, textboxes, comboboxes) MUST have `MinimumSize` set for 96/120/144/192 DPI support
  - **Buttons**: `MinimumSize = new Size(75, 32)`
  - **TextBoxes**: `Height = 23` (single-line), auto-height for multiline
  - **Touch Targets**: Minimum 32px height for all clickable controls
- **FR-045**: Layout MUST use TableLayoutPanel with mixed Absolute/Percent sizing
  - **Example**: Filter row uses `SizeType.AutoSize`, main content uses `SizeType.Percent, 100F`
- **FR-046**: Form MUST set `MinimumSize` based on content requirements
  - **Minimum**: `new Size(1200, 800)` to accommodate three-column file type grouping and all controls at 1080p
- **FR-047**: All containers MUST use proper Padding and Margin:
  - **Container Padding**: 10px uniform
  - **Control Margins**: 5px between related controls
  - **Section Spacing**: 10-15px between major sections

---

### Performance Requirements (FR-048 to FR-054)
### Performance Requirements (FR-048 to FR-054)
**(Per `.github/instructions/performance-optimization.instructions.md`)**

- **FR-048**: All file I/O operations MUST be asynchronous using `async`/`await`
  - **Pattern**: `async Task LoadFileAsync(string path)` not `void LoadFile(string path)`
  - **Never use**: `.Result`, `.Wait()`, `.GetAwaiter().GetResult()` on UI thread
- **FR-049**: UI response time MUST be sub-100ms for all interactions
  - **Strategy**: Show immediate feedback (progress bar, status change) before starting long operations
- **FR-050**: File loading MUST happen on background thread
  - **Pattern**: Use `Task.Run()` for CPU-bound parsing, `ConfigureAwait(false)` in helpers
- **FR-051**: Large log files (> 10MB) MUST use windowing/paging
  - **Implementation**: Load entries in chunks of 1000, lazy-load next chunk on navigation
- **FR-052**: Memory management: Dispose all `FileStream`, `StreamReader`, `Timer` resources
  - **Pattern**: Use `using` statements or implement `IDisposable` properly
- **FR-053**: DataGridView (if used for file list) MUST use virtual mode for large directories
  - **Pattern**: Set `VirtualMode = true`, handle `CellValueNeeded` event
- **FR-054**: Auto-refresh timer MUST be disabled when form is not visible
  - **Pattern**: Handle `VisibleChanged` event, pause timer when minimized/hidden

---

### Security Requirements (FR-055 to FR-060)
**(Per `.github/instructions/security-best-practices.instructions.md`)**

- **FR-045**: ALL file path operations MUST use `Path.Combine()`, NEVER string concatenation
  - **Anti-pattern**: `string path = baseDir + "\\" + userInput;` ❌
  - **Correct**: `string path = Path.Combine(baseDir, userInput);` ✅
- **FR-046**: User input (user names, search text) MUST be validated before file operations
  - **Pattern**: Regex validation for user names: `^[A-Za-z0-9_-]+$`
  - **Path Traversal Prevention**: Reject any input containing `..`, `/`, `\` outside expected separators
- **FR-047**: Constructed file paths MUST be validated against base directory
  - **Pattern**:
    ```csharp
    string fullPath = Path.GetFullPath(Path.Combine(baseDir, userInput));
    if (!fullPath.StartsWith(baseDir, StringComparison.OrdinalIgnoreCase))
        throw new SecurityException("Path traversal detected");
    ```
- **FR-048**: Network path credentials MUST NOT be hardcoded
  - **Pattern**: Store in configuration, retrieve via `Helper_Database_Variables` or config file
- **FR-049**: Error messages to users MUST NOT reveal internal path structure
  - **User message**: "Unable to access log files. Check network connection."
  - **Log message**: "Failed to access \\\\server\\logs\\MTM_Application\\user\\ - Access Denied"
- **FR-050**: Search regex patterns MUST have timeout to prevent ReDoS attacks
  - **Pattern**: `Regex regex = new Regex(pattern, RegexOptions.None, TimeSpan.FromSeconds(1));`

---

---

### C# and Architecture Requirements (FR-061 to FR-075)
**(Per `.github/instructions/csharp-dotnet8.instructions.md` and `.github/instructions/code-review-standards.instructions.md`)**

#### File Organization and Structure
- **FR-061**: Form MUST be located in `Forms/ViewLogs/Form_ViewApplicationLogs.cs`
- **FR-062**: Helper parser MUST be located in `Helpers/Helper_LogParser.cs`
- **FR-063**: Log entry models MUST be located in `Models/` directory:
  - `Model_LogEntry.cs` - Base log entry interface or abstract class
  - `Model_NormalLogEntry.cs` - Normal log format entries
  - `Model_ApplicationErrorEntry.cs` - Application error entries
  - `Model_DatabaseErrorEntry.cs` - Database error entries
- **FR-064**: File MUST use file-scoped namespace: `namespace MTM.Forms.ViewLogs;`

#### Region Organization (MANDATORY)
- **FR-065**: C# files MUST use this exact region structure:
#### Region Organization (MANDATORY)
- **FR-065**: C# files MUST use this exact region structure:
  ```csharp
  #region Fields
  #region Properties  
  #region Constructors
  #region Form Load and Initialization
  #region File and User Selection
  #region Log Type Management
  #region Log Entry Navigation
  #region Filtering and Search
  #region View Mode Toggle
  #region Export and Copy
  #region Event Handlers
  #region Helpers
  #region Cleanup
  ```

#### Async Patterns
- **FR-066**: All async methods MUST follow naming convention: `LoadLogFileAsync`, `RefreshFileListAsync`, `ApplyFiltersAsync`, `ParseNormalLogAsync`, `ParseErrorLogAsync`
- **FR-067**: Event handlers calling async code MUST use `async void` pattern with try/catch:
  ```csharp
  private async void btnRefresh_Click(object sender, EventArgs e)
  {
      try
      {
          await RefreshFileListAsync();
      }
      catch (Exception ex)
      {
          Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, contextData: new Dictionary<string, object> { ["Action"] = "RefreshFileList" });
      }
  }
  ```

#### Error Handling
- **FR-068**: MUST use `Service_ErrorHandler` for ALL user-facing errors, NEVER `MessageBox.Show()`
- **FR-069**: MUST use `LoggingUtility.LogApplicationError()` for all exceptions before surfacing to user
- **FR-070**: File I/O exceptions MUST be caught specifically:
  - `UnauthorizedAccessException` → "Access denied to log files"
  - `FileNotFoundException` → "Log file no longer exists"
  - `IOException` → "Unable to read log file"
  - `DirectoryNotFoundException` → "Log directory not found"

#### XML Documentation
- **FR-071**: All public methods and properties MUST have XML documentation with `<summary>`, `<param>`, `<returns>` tags
- **FR-072**: Complex private methods MUST have at least summary comments

#### Constructor Pattern
- **FR-073**: Constructor MUST follow this exact pattern:
  ```csharp
  public Form_ViewApplicationLogs(string preSelectedUser = null, LogFileType preferredType = LogFileType.Normal)
  {
      InitializeComponent();
      
      // DPI scaling (REQUIRED)
      Core_Themes.ApplyDpiScaling(this);
      Core_Themes.ApplyRuntimeLayoutAdjustments(this);
      
      // Performance
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      
      // Initialize state
      _preSelectedUser = preSelectedUser;
      _preferredLogType = preferredType;
      _logEntries = new List<ILogEntry>();
      
      // Wire up events
      WireUpEvents();
  }
  ```

#### Disposal Pattern
- **FR-074**: MUST implement proper disposal:
  ```csharp
  protected override void Dispose(bool disposing)
  {
      if (disposing)
      {
          _autoRefreshTimer?.Stop();
          _autoRefreshTimer?.Dispose();
          _currentFileStream?.Dispose();
          components?.Dispose();
      }
      base.Dispose(disposing);
  }
  ```

#### Naming Conventions
- **FR-075**: Follow MTM naming conventions:
  - Private fields: `_camelCase` (e.g., `_logEntries`, `_currentUser`, `_currentLogType`)
  - Methods: `PascalCase` (e.g., `LoadLogFileAsync`, `ApplyFilters`, `ParseNormalLogEntry`)
  - Event handlers: `controlName_EventName` (e.g., `btnRefresh_Click`, `cmbLogType_SelectedIndexChanged`)
  - Parameters: `camelCase` (e.g., `filePath`, `userName`, `logType`)

---

### Expected Log Formats (Reference)

**Actual Format Examples from Production Logs** (Based on `LoggingUtility.cs` and `Service_DebugTracer` implementations)

#### Format 1: Normal Application Logs (*_normal.log)

**Purpose**: Captures Service_DebugTracer performance metrics, debug traces, and operational logging with structured JSON data blocks.

**Structure Pattern**:
- **Simple Entry**: `YYYY-MM-DD HH:mm:ss - [Component] Message`
  - Example: `2025-10-26 14:46:32 - [Splash] Logging system initialized`
  
- **Debug Tracer Entry**: `YYYY-MM-DD HH:mm:ss - [HH:mm:ss.fff] [LEVEL  ] EmojiIndicator Message`
  - Timestamp appears twice: once at line start, once in brackets
  - Level values: LOW, MEDIUM, HIGH, DATA (fixed width with padding)
  - Emoji indicators: ✅ (success), ⏱️ (performance), 🗄️ (database), ➡️ (entering), ⬅️ (exiting)
  - Example: `2025-10-26 14:46:32 - [14:46:32.820] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (62ms) - Status: 1`

- **JSON Data Block**: Multi-line JSON following debug entry (no timestamp prefix on continuation lines)
  - Contains structured data: Action, Procedure, Caller, Status, ElapsedMs, Thread, OutputParameters, ResultData
  - Indented and formatted for readability
  - Parser must associate JSON block with preceding log entry

**Key Parsing Challenges**:
- Multi-line JSON blocks without timestamp prefixes
- Emoji characters require UTF-8 encoding support
- Performance metrics embedded in message text (e.g., "(62ms)")
- Thread IDs may or may not be present

#### Format 2: Application Error Logs (*_app_error.log)

**Purpose**: Captures unhandled exceptions and application-level errors with full stack traces.

**Structure Pattern** (Two-line paired format):
- **Line 1 (Error Message)**: `YYYY-MM-DD HH:mm:ss - Application Error - {Exception.Message}`
  - Example: `2025-10-26 14:46:32 - Application Error - Object reference not set to an instance of an object`
  
- **Line 2 (Stack Trace)**: `YYYY-MM-DD HH:mm:ss - Stack Trace - {Exception.StackTrace}`
  - Full multi-line stack trace with preserved formatting
  - Example: `2025-10-26 14:46:32 - Stack Trace -    at MTM.Data.Dao_Inventory.<GetItems>d__12.MoveNext()...`

**Key Parsing Challenges**:
- Two consecutive lines must be paired into single entry
- Stack traces can span many lines with indentation
- Both lines share same or near-identical timestamp

#### Format 3: Database Error Logs (*_db_error.log)

**Purpose**: Captures database-specific errors with severity classification for MySQL connection/query failures.

**Structure Pattern** (Two-line paired format with severity):
- **Line 1 (Error with Severity)**: `YYYY-MM-DD HH:mm:ss - Database Error [SEVERITY] - {Exception.Message}`
  - Severity levels: WARNING, ERROR, CRITICAL (extracted from bracketed tag)
  - Example: `2025-10-26 14:46:32 - Database Error [CRITICAL] - Timeout expired. The timeout period elapsed...`
  
- **Line 2 (Stack Trace)**: `YYYY-MM-DD HH:mm:ss - Stack Trace - {Exception.StackTrace}`
  - Same format as application error stack traces

**Key Parsing Challenges**:
- Must extract severity level from [SEVERITY] tag
- Two-line pairing like application errors
- Severity level affects UI color coding

#### Parsing Implementation Requirements

**Multi-Format Parser Design**:
- Parser must detect log file type from filename suffix (_normal.log / _app_error.log / _db_error.log)
- Each format requires different parsing strategy (single-line vs. two-line pairing vs. JSON continuation)
- Malformed entries must fall back to raw text display without crashing parser
- Emoji rendering requires font support check (Segoe UI Emoji or fallback)

**Performance Considerations**:
- Normal logs can exceed 50MB with thousands of JSON blocks
- Lazy parsing strategy: only parse entries on-demand during navigation
- Cache parsed entries to avoid re-parsing during filtering
- JSON blocks should be stored as strings, not parsed into objects (display only)

---

### Key Entities

- **LogFile**: Represents a log file in network storage
  - Attributes: `FilePath`, `FileName`, `FileSize`, `CreationDate`, `LastModified`, `UserName`, `EntryCount`
  - Source: `Directory.EnumerateFiles()` with `FileInfo` metadata
  - Model Location: `Models/Model_LogFile.cs`

- **LogEntry**: Represents a parsed log entry
  - Attributes: `Timestamp`, `Level` (enum: Debug/Info/Warning/Error/Fatal), `Source`, `Message`, `Details`, `ThreadId`, `RawText`, `LineNumber`
  - Source: Parsed from log file using `Helper_LogParser`
  - Model Location: `Models/Model_LogEntry.cs`

- **LogFilter**: Represents active filtering criteria
  - Attributes: `StartDate`, `EndDate`, `SelectedLevels` (List<LogLevel>), `SelectedSource`, `SearchText`
  - Actions: `ApplyFilter()`, `ClearFilters()`, `MatchesEntry(LogEntry entry)`
  - Model Location: `Models/Model_LogFilter.cs`

---

## Log Parsing

### Expected Log Format

```
[2025-10-25 09:15:33.427] [ERROR] [Dao_Inventory] Connection timeout occurred
Details: System.Data.SqlClient.SqlException: Timeout expired...
   at System.Data.SqlClient.SqlCommand.ExecuteReader()
   at MTM.Data.Dao_Inventory.GetInventoryItems()
ThreadID: 14
```

### Parsing Pattern

1. Extract timestamp: `[yyyy-MM-dd HH:mm:ss.fff]`
2. Extract level: `[DEBUG|INFO|WARN|WARNING|ERROR|FATAL]`
3. Extract source: `[ComponentName]`
4. Extract message: First line after headers
5. Extract details: Remaining lines (stack traces, JSON, additional context)
6. Extract thread ID: `ThreadID: \d+` if present

### Fallback Handling

- If pattern doesn't match: Mark as "UNPARSED", show in raw view
- If only partial match: Parse what's possible, show warnings
- If multiple patterns detected: Try all patterns, use best match

---

## UI Mockups

### SELECTED OPTION: OPTION D - Integrated Responsive Layout

**Why Option D**: Provides best DPI scaling, responsive behavior, and integration with existing MTM patterns using TableLayoutPanel architecture per `.github/instructions/winforms-responsive-layout.instructions.md`.

```
┌─────────────────────────────────────────────────────────────────┐
│ View Application Logs                                       [X] │
├─────────────────────────────────────────────────────────────────┤
│ ┌─── User & File Selection Panel (AutoSize) ─────────────────┐ │
│ │ User: [bjones          ▼]  [Refresh🔄] [Open Dir📁]  │
│ │                                                              │ │
│ │ Files: ┌────────────────────────────────────────────────┐  │ │
│ │        │ ● Oct_25_09.log │ 2.3MB │ 543 entries         │  │ │
│ │        │   Oct_25_08.log │ 1.8MB │ 412 entries         │  │ │
│ │        │   Oct_24_16.log │ 3.1MB │ 891 entries         │  │ │
│ │        └────────────────────────────────────────────────┘  │ │
│ │                                    ☐ Auto-Refresh (5s)      │ │
│ └──────────────────────────────────────────────────────────────┘ │
├─────────────────────────────────────────────────────────────────┤
│ ┌─── Filter Panel (AutoSize) ─────────────────────────────────┐ │
│ │ Date: [10/24/2025] to [10/25/2025]  Severity: [All    ▼]   │ │
│ │ Source: [All        ▼]  Search: [____________] [Apply]      │ │
│ │                                   [Clear] Showing 23/543    │ │
│ └──────────────────────────────────────────────────────────────┘ │
├─────────────────────────────────────────────────────────────────┤
│ ┌─── Log Entry Display (Percent 100F Fill) ──────────────────┐ │
│ │ Entry 5 of 543                [◄ Prev] [Next ►] ○ Raw View │ │
│ │                                                              │ │
│ │ ┌── Parsed Entry ──────────────────────────────────────────┐ │
│ │ │ Timestamp: [2025-10-25 09:15:33.427____________]         │ │
│ │ │ Level:     [ERROR 🔴]    Source: [Dao_Inventory_____]    │ │
│ │ │ Thread:    [14]                                          │ │
│ │ │                                                           │ │
│ │ │ Message:                                                  │ │
│ │ │ ┌───────────────────────────────────────────────────────┤ │
│ │ │ │Connection timeout occurred when retrieving inventory  ││ │
│ │ │ │list from database server (Retry 3/3 failed)          ││ │
│ │ │ └───────────────────────────────────────────────────────┤ │
│ │ │                                                           │ │
│ │ │ Details:                                                  │ │
│ │ │ ┌───────────────────────────────────────────────────────┤ │
│ │ │ │System.Data.SqlClient.SqlException: Timeout expired... ││ │
│ │ │ │   at SqlCommand.ExecuteReader()                       ││ │
│ │ │ │   at Dao_Inventory.GetInventoryItems()               ││ │
│ │ │ │                                                       ││ │
│ │ │ │Stack Trace:                                           ││ │
│ │ │ │   at MTM.Data.Dao_Inventory.<GetItems>d__12.Move...  ││ │
│ │ │ │   at System.Runtime.CompilerServices.TaskAwaiter...   ││ │
│ │ │ └───────────────────────────────────────────────────────┤ │
│ │ └───────────────────────────────────────────────────────────┘ │
│ └──────────────────────────────────────────────────────────────┘ │
├─────────────────────────────────────────────────────────────────┤
│ [Copy Entry] [Export Visible] [Export All]            [Close]  │
└─────────────────────────────────────────────────────────────────┘
```

### Option D Layout Specification

**TableLayoutPanel Structure** (per winforms-responsive-layout.instructions.md):

```csharp
// Main form layout
var mainLayout = new TableLayoutPanel
{
    Dock = DockStyle.Fill,
    ColumnCount = 1,
    RowCount = 4,
    Padding = new Padding(10), // Container padding
    AutoSize = false
};

// Row sizing with mixed strategy
mainLayout.RowStyles.Clear();
mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // User/File panel
mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));     // Filter panel
mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Entry display (fills)
mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));  // Button panel
```

**User/File Selection Panel** (Row 0 - AutoSize):
```csharp
var userFilePanel = new TableLayoutPanel
{
    Dock = DockStyle.Fill,
    ColumnCount = 4,
    RowCount = 3,
    Padding = new Padding(5),
    AutoSize = true,
    AutoSizeMode = AutoSizeMode.GrowAndShrink
};

// Column sizing
userFilePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));   // Label "User:"
userFilePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));   // User dropdown
userFilePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));  // Refresh button
userFilePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));  // Open Dir button

// Row sizing
userFilePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32)); // User selection row
userFilePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // File list (expands)
userFilePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32)); // Auto-refresh checkbox
```

**Filter Panel** (Row 1 - AutoSize):
```csharp
var filterPanel = new TableLayoutPanel
{
    Dock = DockStyle.Fill,
    ColumnCount = 8,
    RowCount = 2,
    Padding = new Padding(5),
    AutoSize = true,
    Margin = new Padding(0, 10, 0, 10) // Spacing from adjacent panels
};

// Row 0: Date range, Severity
// Row 1: Source, Search, Apply/Clear buttons, Status label
filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));   // "Date:"
filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));  // Start date
filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));   // "to"
filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));  // End date
filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));   // "Severity:"
filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));  // Severity dropdown
filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Spacer
filterPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));  // Status label
```

**Entry Display Panel** (Row 2 - Percent 100F Fill):
```csharp
var entryPanel = new TableLayoutPanel
{
    Dock = DockStyle.Fill,
    ColumnCount = 1,
    RowCount = 2,
    Padding = new Padding(5),
    AutoSize = false
};

entryPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Navigation bar
entryPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Entry content

// Entry content - parsed fields
var parsedPanel = new TableLayoutPanel
{
    Dock = DockStyle.Fill,
    ColumnCount = 4,
    RowCount = 3,
    Padding = new Padding(5)
};

// Field layout: Label | Control | Label | Control
parsedPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));  // Timestamp label
parsedPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // Timestamp value
parsedPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));   // Level label
parsedPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // Level value

parsedPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));  // Timestamp/Level/Source/Thread
parsedPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Message (multiline)
parsedPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Details (fills remaining)
```

**Button Panel** (Row 3 - Absolute 50px):
```csharp
var buttonPanel = new FlowLayoutPanel
{
    Dock = DockStyle.Fill,
    FlowDirection = FlowDirection.LeftToRight,
    Padding = new Padding(5),
    AutoSize = false,
    Height = 50
};

// Buttons with minimum size
var btnCopy = new Button
{
    Text = "Copy Entry",
    MinimumSize = new Size(100, 32),
    Margin = new Padding(0, 0, 5, 0)
};
// ... additional buttons
```

**DPI Scaling Integration**:
```csharp
public Form_ViewApplicationLogs(string preSelectedUser = null)
{
    InitializeComponent();
    
    // REQUIRED: DPI scaling calls (per ui-scaling-consistency.instructions.md)
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    // Performance optimization
    SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    SetStyle(ControlStyles.UserPaint, true);
    
    // Form constraints
    this.MinimumSize = new Size(1000, 700); // Per FR-036
    this.StartPosition = FormStartPosition.CenterParent;
    
    // Initialize
    _preSelectedUser = preSelectedUser;
    ConfigureResponsiveLayout();
    WireUpEvents();
}
```

**Responsive Behavior**:
- **1080p (1920x1080)**: All panels visible, entry display ~400px height
- **1366x768**: File list shrinks, entry display maintains minimum ~300px
- **4K (3840x2160)**: All controls scale proportionally via DPI (200%)
- **Vertical resize**: Entry display panel expands/contracts (Percent 100F)
- **Horizontal resize**: Controls stretch via Anchor/Dock, maintain margins

### ARCHIVED OPTIONS (Reference Only)

### OPTION A: Vertical Split - User/File Left, Details Right

```
┌───────────────────────────────────────────────────────────────┐
│ View Application Logs                                     [X] │
├──────────────────────┬────────────────────────────────────────┤
│ User Selection (30%) │ Log Entry Display (70%)                │
│                      │                                        │
│ Select User:         │ Entry 5 of 543       [◄ Prev] [Next ►]│
│ [bjones        ▼]    │ ┌────────────────────────────────────┐ │
│                      │ │ Timestamp:                         │ │
│ Log Files:           │ │ [2025-10-25 09:15:33.427_______]   │ │
│ ┌──────────────────┐ │ │                                    │ │
│ │ Oct_25_09.log    │ │ │ Level: [ERROR 🔴]                  │ │
│ │ Oct_25_08.log    │ │ │                                    │ │
│ │ Oct_24_16.log    │←│ │ Source: [Dao_Inventory__________]  │ │
│ └──────────────────┘ │ │                                    │ │
│ Size: 2.3 MB         │ │ Message:                           │ │
│                      │ │ ┌──────────────────────────────────┤ │
│ Filters:             │ │ │Connection timeout occurred when  ││ │
│ Date Range:          │ │ │retrieving inventory list         ││ │
│ [10/24] to [10/25]   │ │ └──────────────────────────────────┤ │
│                      │ │                                    │ │
│ Severity:            │ │ Details:                           │ │
│ ☑Debug ☑Info         │ │ ┌──────────────────────────────────┤ │
│ ☑Warning ☑Error      │ │ │System.Data.SqlClient.SqlException││ │
│ ☑Fatal               │ │ │at SqlCommand.ExecuteReader()     ││ │
│                      │ │ │at Dao_Inventory.GetInventory()   ││ │
│ Source:              │ │ │                                  ││ │
│ [All Components ▼]   │ │ │ThreadID: 14                      ││ │
│                      │ │ └──────────────────────────────────┤ │
│ Search:              │ │                                    │ │
│ [____________]       │ │ ☐ Raw View                         │ │
│                      │ │                                    │ │
│ [Apply] [Clear]      │ │ [Copy Entry] [Export Visible]      │ │
│                      │ │                                    │ │
│ [Refresh Files]      │ │ Showing 23 of 543 entries          │ │
│ ☐ Auto-Refresh (5s)  │ │                                    │ │
│                      │ │                                    │ │
│ [Open Log Dir]       │ │                                    │ │
└──────────────────────┴────────────────────────────────────────┘
```

### OPTION B: Three-Row Layout - Filters Top, File List Middle, Details Bottom

```
┌─────────────────────────────────────────────────────────────────┐
│ View Application Logs                                       [X] │
├─────────────────────────────────────────────────────────────────┤
│ User: [bjones ▼]  Date: [10/24] to [10/25]  Severity: [All ▼]  │
│ Source: [All ▼]  Search: [____________]  [Apply] [Clear]        │
├─────────────────────────────────────────────────────────────────┤
│ Log Files:                                     Showing 23 of 543│
│ ┌───────────────────────────────────────────────────────────┐   │
│ │ Filename         │ Date       │ Size   │ Entries │ Status  │   │
│ ├──────────────────┼────────────┼────────┼─────────┼─────────┤   │
│ │ Oct_25_09.log    │ 10/25 9:00 │ 2.3 MB │ 543     │ Current │   │
│ │ Oct_25_08.log    │ 10/25 8:00 │ 1.8 MB │ 412     │         │   │
│ │ Oct_24_16.log    │ 10/24 16:00│ 3.1 MB │ 891     │         │   │
│ └───────────────────────────────────────────────────────────┘   │
├─────────────────────────────────────────────────────────────────┤
│ Log Entry: 5 of 543              [◄ Prev] [Next ►]  ☐ Raw View │
│ ┌───────────────────────────────────────────────────────────┐   │
│ │ Timestamp: 2025-10-25 09:15:33.427     Level: ERROR 🔴    │   │
│ │ Source: Dao_Inventory                  Thread: 14         │   │
│ │                                                            │   │
│ │ Message:                                                   │   │
│ │ Connection timeout occurred when retrieving inventory list │   │
│ │                                                            │   │
│ │ Details:                                                   │   │
│ │ System.Data.SqlClient.SqlException: Timeout expired...    │   │
│ │    at System.Data.SqlClient.SqlCommand.ExecuteReader()    │   │
│ │    at MTM.Data.Dao_Inventory.GetInventoryItems()          │   │
│ └───────────────────────────────────────────────────────────┘   │
│ [Copy Entry] [Export Visible] [Refresh] [Auto-Refresh☐]  [Close│
└─────────────────────────────────────────────────────────────────┘
```

### OPTION C: Tabbed Interface

```
┌─────────────────────────────────────────────────────────────┐
│ View Application Logs                                   [X] │
├─────────────────────────────────────────────────────────────┤
│ [File Selection] [Log Viewer] [Search & Filter]            │
├─────────────────────────────────────────────────────────────┤
│ File Selection Tab:                                         │
│                                                             │
│ Select User:                                                │
│ ┌─────────────────────┐                                     │
│ │ bjones              │                                     │
│ │ ajohnson            │                                     │
│ │ jsmith              │                                     │
│ │ ────────────────    │                                     │
│ │ All Users           │                                     │
│ └─────────────────────┘                                     │
│                                                             │
│ Available Log Files for bjones:                             │
│ ┌───────────────────────────────────────────────────────┐   │
│ │ ● Oct_25_09.log    (2.3 MB)    543 entries            │   │
│ │   Oct_25_08.log    (1.8 MB)    412 entries            │   │
│ │   Oct_24_16.log    (3.1 MB)    891 entries            │   │
│ └───────────────────────────────────────────────────────┘   │
│                                                             │
│ Network Path: \\server\logs\MTM_Application\bjones\         │
│                                                             │
│ [Refresh] [Open Directory] [Auto-Refresh ☐]       [Load >>]│
└─────────────────────────────────────────────────────────────┘

Log Viewer Tab (after loading):
┌─────────────────────────────────────────────────────────────┐
│ [File Selection] [Log Viewer] [Search & Filter]            │
├─────────────────────────────────────────────────────────────┤
│ Viewing: Oct_25_09.log          Entry 5 of 543 (Filtered)  │
│                                         [◄ Prev] [Next ►]   │
│ ┌───────────────────────────────────────────────────────┐   │
│ │ [Parsed View ●] [Raw View ○]                          │   │
│ │                                                        │   │
│ │ Timestamp: [2025-10-25 09:15:33.427_____________]      │   │
│ │ Level:     [ERROR] 🔴                                  │   │
│ │ Source:    [Dao_Inventory_____________________]        │   │
│ │ Thread:    [14]                                        │   │
│ │                                                        │   │
│ │ Message:                                               │   │
│ │ ┌──────────────────────────────────────────────────┐  │   │
│ │ │Connection timeout occurred when retrieving       │  │   │
│ │ │inventory list from database server              │  │   │
│ │ └──────────────────────────────────────────────────┘  │   │
│ │                                                        │   │
│ │ Details:                                               │   │
│ │ ┌──────────────────────────────────────────────────┐  │   │
│ │ │System.Data.SqlClient.SqlException: Timeout...    │  │   │
│ │ │   at SqlCommand.ExecuteReader()                  │  │   │
│ │ │   at Dao_Inventory.GetInventoryItems()          │  │   │
│ │ │                                                  │  │   │
│ │ └──────────────────────────────────────────────────┘  │   │
│ └───────────────────────────────────────────────────────┘   │
│                                                             │
│ [Copy Entry] [Export Current File] [Back to File Selection]│
└─────────────────────────────────────────────────────────────┘

Search & Filter Tab:
┌─────────────────────────────────────────────────────────────┐
│ [File Selection] [Log Viewer] [Search & Filter]            │
├─────────────────────────────────────────────────────────────┤
│ Filter Log Entries:                                         │
│                                                             │
│ Date Range:                                                 │
│ From: [10/24/2025 00:00]  To: [10/25/2025 23:59]           │
│                                                             │
│ Severity Levels:                                            │
│ ☑ Debug    ☑ Info    ☑ Warning    ☑ Error    ☑ Fatal       │
│                                                             │
│ Source Component:                                           │
│ [All Components                                       ▼]    │
│                                                             │
│ Search Text (Message or Details):                           │
│ [________________________________________________]          │
│                                                             │
│ [Apply Filters] [Clear Filters]                            │
│                                                             │
│ Current Results: Showing 23 of 543 entries                  │
│                                                             │
│ Export Options:                                             │
│ [Export Filtered Entries to Text File]                     │
│ [Export Filtered Entries to CSV]                           │
│                                                             │
│                                            [Back to Viewer] │
└─────────────────────────────────────────────────────────────┘
```

### OPTION D: Dashboard Style with Statistics

```
┌─────────────────────────────────────────────────────────────────┐
│ View Application Logs                                       [X] │
├─────────────────────────────────────────────────────────────────┤
│ ┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐  │
│ │ User: bjones    │  │ Recent Errors:  │  │ Active Filters: │  │
│ │ [Change...]     │  │ 🔴 23 Errors    │  │ Date: Today     │  │
│ └─────────────────┘  │ 🟡 8 Warnings   │  │ Level: All      │  │
│                      │ Last: 5 min ago │  │ [Edit Filters]  │  │
│                      └─────────────────┘  └─────────────────┘  │
├──────────────────────┬──────────────────────────────────────────┤
│ Log Files (20%):     │ Selected Entry Details (80%):            │
│ ┌──────────────────┐ │                                          │
│ │● Oct_25_09.log   │ │ Entry 5 of 543      [◄] [▲] [▼] [►]     │
│ │  Oct_25_08.log   │ │                                          │
│ │  Oct_24_16.log   │ │ ┌──────────────────────────────────────┐ │
│ └──────────────────┘ │ │ 🔴 ERROR | 2025-10-25 09:15:33       │ │
│ 2.3 MB / 543 entries │ │                                      │ │
│                      │ │ Source: Dao_Inventory (Thread 14)    │ │
│ [Refresh] [Open Dir] │ │                                      │ │
│                      │ │ Message:                             │ │
│                      │ │ Connection timeout occurred when     │ │
│                      │ │ retrieving inventory list            │ │
│                      │ │                                      │ │
│                      │ │ Details:                             │ │
│                      │ │ System.Data.SqlClient.SqlException   │ │
│                      │ │    at SqlCommand.ExecuteReader()     │ │
│                      │ │    at Dao_Inventory.GetInventory()   │ │
│                      │ └──────────────────────────────────────┘ │
│                      │                                          │
│                      │ [Copy] [Export] [Raw View]               │
└──────────────────────┴──────────────────────────────────────────┘
```
***Create More Options (with the 4 above Options also create at least 5 more, this clarrifcation should be in its own file)***
---

## Configuration

Add to `Model_AppVariables` or appsettings.json:

```json
{
  "Logging": {
    "NetworkStoragePath": "\\\\server\\logs\\MTM_Application\\",
    "LocalFallbackPath": "%APPDATA%\\MTM_Application\\Logs\\",
    "EnableNetworkLogging": true,
    "AutoRefreshIntervalSeconds": 5,
    "MaxEntriesPerLoad": 1000,
    "MaxFileSizeMB": 100
  }
}
```

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: User dropdown populates with all users from network storage within 1 second
- **SC-002**: Log file list loads within 500ms for user with 20 log files
- **SC-003**: Log file parsing completes within 2 seconds for 1000-entry file
- **SC-004**: Entry navigation (Next/Previous) responds within 50ms
- **SC-005**: Filtering 5000 entries down to 100 matching entries completes within 300ms
- **SC-006**: Export of 500 filtered entries completes within 1 second
- **SC-007**: Auto-refresh updates file list and current entry within 500ms every 5 seconds
- **SC-008**: Parse success rate exceeds 95% for standard log format entries

---

## Relevant Instruction Files

### For Implementation Phase:
- `.github/instructions/csharp-dotnet8.instructions.md` - File I/O, async patterns, WinForms
- `.github/instructions/testing-standards.instructions.md` - Manual validation approach

### For Quality Assurance:
- `.github/instructions/performance-optimization.instructions.md` - File I/O optimization, memory management for large files
- `.github/instructions/security-best-practices.instructions.md` - Network path security, file access validation
- `.github/instructions/code-review-standards.instructions.md` - Quality checklist

