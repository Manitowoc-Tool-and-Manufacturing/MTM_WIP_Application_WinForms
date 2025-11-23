# Research: View Application Logs Feature

**Date**: 2025-10-26  
**Feature**: 003-view-application-logs  
**Phase**: 0 - Research & Technical Decisions

## Overview

This document consolidates research findings for implementing a log viewer form in the MTM WinForms application. The feature requires parsing three distinct log formats, managing file I/O performance, implementing security validation for file paths, and maintaining WinForms best practices.

---

## 1. Log Format Parsing Strategy

### Decision: Regex-Based Parser with Format Detection

**Rationale**:
- Three distinct log formats require different parsing strategies
- Service_DebugTracer format (normal logs) uses structured markers with JSON blocks
- Application error logs use two-line paired format (error type + details)
- Database error logs use severity-prefixed format with stack traces
- Regex patterns provide performance and flexibility for structured text parsing
- Timeout protection (1 second per regex operation) prevents ReDoS attacks per FR-050

**Implementation Approach**:
```csharp
// Service_LogParser.cs pattern
public enum LogFormat { Normal, ApplicationError, DatabaseError, Unknown }

public class Service_LogParser
{
    private static readonly Regex NormalLogPattern = new Regex(
        @"^\[(?<timestamp>.*?)\]\s+(?<level>LOW|MEDIUM|HIGH|DATA)\s+(?<emoji>.*?)\s+(?<source>.*?)\s+-\s+(?<message>.*?)(?:\nDetails:\s+(?<details>.*?))?$",
        RegexOptions.Multiline | RegexOptions.Compiled,
        TimeSpan.FromSeconds(1)
    );
    
    public static LogFormat DetectFormat(string firstLine)
    {
        if (firstLine.Contains("ERROR TYPE:")) return LogFormat.ApplicationError;
        if (firstLine.Contains("SEVERITY:")) return LogFormat.DatabaseError;
        if (firstLine.StartsWith("[") && 
            (firstLine.Contains("LOW") || firstLine.Contains("MEDIUM") || 
             firstLine.Contains("HIGH") || firstLine.Contains("DATA"))) 
            return LogFormat.Normal;
        return LogFormat.Unknown;
    }
}
```

**Alternatives Considered**:
- **JSON parsing**: Rejected - logs are not pure JSON, mixed format with text markers
- **Manual string splitting**: Rejected - fragile, difficult to maintain, no timeout protection
- **Third-party log parsers**: Rejected - unnecessary dependency, formats are custom

---

## 2. Async File I/O with Windowing

### Decision: FileStream with Async ReadLine, Lazy Loading for Large Files

**Rationale**:
- FR-041 mandates async file I/O to prevent UI blocking
- FR-044 requires windowing for files >10MB (load 1000 entries at a time)
- StreamReader with FileShare.ReadWrite enables reading locked files per edge case requirements
- Lazy loading reduces memory footprint during extended sessions (SC-010)
- Performance target: Load 1000-entry file in <2s (SC-003)

**Implementation Approach**:
```csharp
// Service_LogFileReader.cs pattern
public class Service_LogFileReader : IDisposable
{
    private FileStream _fileStream;
    private StreamReader _reader;
    private const int WindowSize = 1000;
    
    public async Task<List<Model_LogEntry>> LoadEntriesAsync(
        string filePath, 
        int startIndex, 
        int count,
        CancellationToken cancellationToken = default)
    {
        await using var stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite,  // Allow reading locked files
            bufferSize: 4096,
            useAsync: true
        );
        
        using var reader = new StreamReader(stream);
        var entries = new List<Model_LogEntry>(count);
        
        // Skip to startIndex
        for (int i = 0; i < startIndex && !reader.EndOfStream; i++)
        {
            await reader.ReadLineAsync();
        }
        
        // Read window
        for (int i = 0; i < count && !reader.EndOfStream; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var line = await reader.ReadLineAsync();
            if (line != null)
            {
                var entry = Service_LogParser.ParseEntry(line);
                entries.Add(entry);
            }
        }
        
        return entries;
    }
}
```

**Alternatives Considered**:
- **File.ReadAllLines**: Rejected - loads entire file into memory, violates FR-044
- **Memory-mapped files**: Rejected - overkill for text parsing, complicates implementation
- **Synchronous I/O**: Rejected - violates FR-041, blocks UI thread

---

## 3. Path Security Validation

### Decision: Helper_LogPath with Path.Combine and Base Directory Validation

**Rationale**:
- FR-047 mandates Path.Combine for all path operations
- FR-049 requires validation against base directory to prevent path traversal attacks
- Network storage path structure: `\\networkpath\logs\{username}\{logfile}`
- Windows-specific path validation with GetFullPath() normalization
- Whitelist approach: validate username matches known user pattern

**Implementation Approach**:
```csharp
// Helper_LogPath.cs pattern
public static class Helper_LogPath
{
    private static readonly string BaseLogPath = @"\\networkpath\logs";
    private static readonly Regex UsernamePattern = new Regex(
        @"^[a-zA-Z0-9_-]+$",
        RegexOptions.Compiled
    );
    
    public static string GetUserLogDirectory(string username)
    {
        // FR-048: Validate input before file operations
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty", nameof(username));
        
        if (!UsernamePattern.IsMatch(username))
            throw new ArgumentException("Username contains invalid characters", nameof(username));
        
        // FR-047: Use Path.Combine
        var userPath = Path.Combine(BaseLogPath, username);
        
        // FR-049: Validate against base directory (prevent path traversal)
        var fullPath = Path.GetFullPath(userPath);
        var fullBasePath = Path.GetFullPath(BaseLogPath);
        
        if (!fullPath.StartsWith(fullBasePath, StringComparison.OrdinalIgnoreCase))
            throw new SecurityException("Path traversal attempt detected");
        
        return fullPath;
    }
    
    public static string GetLogFilePath(string username, string filename)
    {
        var userDir = GetUserLogDirectory(username);
        
        // Validate filename
        if (string.IsNullOrWhiteSpace(filename) || 
            filename.Contains("..") || 
            Path.IsPathRooted(filename))
            throw new ArgumentException("Invalid filename", nameof(filename));
        
        return Path.Combine(userDir, filename);
    }
}
```

**Security Considerations**:
- Input validation before any file system access
- Path normalization with GetFullPath() to resolve ".." sequences
- Base directory validation prevents escaping log directory structure
- Filename validation rejects rooted paths and parent directory references
- Error messages do not reveal internal path structure (FR-051)

**Alternatives Considered**:
- **String concatenation**: Rejected - violates FR-047, platform-specific issues
- **Trust user input**: Rejected - security vulnerability, violates FR-048/FR-049
- **Database-backed file registry**: Rejected - overkill, adds database dependency

---

## 4. WinForms Layout with DPI Scaling

### Decision: TableLayoutPanel with AutoScaleMode.Dpi

**Rationale**:
- FR-052 mandates AutoScaleMode.Dpi for high-DPI support
- FR-053 requires Core_Themes.ApplyDpiScaling() in constructor
- FR-055 requires TableLayoutPanel with mixed Absolute/Percent sizing
- FR-056 sets MinimumSize of 1200x800 for 1080p resolution
- Constitution principle III requires region organization with Constructors region calling theme methods

**Layout Structure**:
```
ViewApplicationLogsForm (MinimumSize: 1200x800)
├── TableLayoutPanel (Main) - Dock: Fill
│   ├── Row 0 (50px Absolute): User Selection Panel
│   │   ├── Label "Select User:"
│   │   ├── ComboBox (user dropdown)
│   │   └── Button "Refresh"
│   ├── Row 1 (60%): File List Panel
│   │   ├── GroupBox "Log Files"
│   │   └── ListView (files with type, date, size)
│   └── Row 2 (40%): Log Entry Display Panel
│       ├── TableLayoutPanel (Entry Display)
│       │   ├── Row 0 (Auto): Navigation Bar (Previous, Entry X of Y, Next)
│       │   ├── Row 1 (Percent 100%): Parsed/Raw View Area
│       │   └── Row 2 (Auto): Action Bar (Copy, Export, Toggle View)
│       └── TableLayoutPanel (Filter Panel)
│           ├── Date Range Pickers
│           ├── Log Type Dropdown
│           ├── Severity Checkboxes (dynamic based on log type)
│           ├── Source Dropdown
│           └── Search TextBox
```

**Constructor Pattern**:
```csharp
public partial class ViewApplicationLogsForm : Form
{
    #region Constructors
    
    public ViewApplicationLogsForm()
    {
        InitializeComponent();
        
        // FR-053: DPI scaling and theme application
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        InitializeAsync().ConfigureAwait(false);
    }
    
    private async Task InitializeAsync()
    {
        await LoadUserListAsync();
        WireUpEvents();
    }
    
    #endregion
}
```

**Alternatives Considered**:
- **Fixed pixel layouts**: Rejected - fails at different DPI settings, violates FR-052
- **Manual DPI calculation**: Rejected - Core_Themes provides tested implementation
- **FlowLayoutPanel**: Rejected - less predictable sizing, harder to achieve responsive behavior

---

## 5. Filter Implementation with Entry Navigation

### Decision: In-Memory Filtering with Index Tracking

**Rationale**:
- FR-021 through FR-028 define comprehensive filtering requirements
- FR-026 requires filter updates to affect entry navigation
- Performance target: Filter 5000 entries to 100 matches in <300ms (SC-005)
- Filtering operates on already-loaded entry window (1000 entries max per FR-044)
- LINQ provides performant predicate composition for multiple filter criteria

**Implementation Approach**:
```csharp
// ViewApplicationLogsForm filtering logic
public class LogEntryNavigator
{
    private List<Model_LogEntry> _allEntries;
    private List<int> _filteredIndices;  // Indices into _allEntries
    private int _currentFilteredIndex = 0;
    private Model_LogFilter _activeFilter;
    
    public void ApplyFilter(Model_LogFilter filter)
    {
        _activeFilter = filter;
        
        var query = _allEntries.Select((entry, index) => new { entry, index });
        
        // Date range
        if (filter.StartDate.HasValue)
            query = query.Where(x => x.entry.Timestamp >= filter.StartDate.Value);
        if (filter.EndDate.HasValue)
            query = query.Where(x => x.entry.Timestamp <= filter.EndDate.Value);
        
        // Log type
        if (filter.LogTypes?.Any() == true)
            query = query.Where(x => filter.LogTypes.Contains(x.entry.LogType));
        
        // Severity (dynamic based on log type)
        if (filter.SeverityLevels?.Any() == true)
            query = query.Where(x => filter.SeverityLevels.Contains(x.entry.Severity));
        
        // Source component
        if (!string.IsNullOrWhiteSpace(filter.SourceComponent))
            query = query.Where(x => x.entry.Source == filter.SourceComponent);
        
        // Search text (with regex timeout per FR-050)
        if (!string.IsNullOrWhiteSpace(filter.SearchText))
        {
            var searchRegex = new Regex(
                Regex.Escape(filter.SearchText),
                RegexOptions.IgnoreCase,
                TimeSpan.FromSeconds(1)
            );
            query = query.Where(x => 
                searchRegex.IsMatch(x.entry.Message) ||
                searchRegex.IsMatch(x.entry.Details ?? ""));
        }
        
        _filteredIndices = query.Select(x => x.index).ToList();
        _currentFilteredIndex = 0;
        
        UpdateEntryCountDisplay();
    }
    
    private void UpdateEntryCountDisplay()
    {
        if (_filteredIndices.Count < _allEntries.Count)
            lblEntryCount.Text = $"Entry {_currentFilteredIndex + 1} of {_filteredIndices.Count} (Showing {_filteredIndices.Count} of {_allEntries.Count})";
        else
            lblEntryCount.Text = $"Entry {_currentFilteredIndex + 1} of {_allEntries.Count}";
    }
}
```

**Performance Considerations**:
- LINQ materializes filtered list once per filter change
- Index tracking avoids copying entry data
- Regex timeout prevents ReDoS for complex search patterns
- Filter persists when navigating between files of same type (FR-028)

**Alternatives Considered**:
- **Database-backed filtering**: Rejected - logs are files, not database records
- **Re-parse on filter**: Rejected - violates performance targets
- **Streaming filter**: Rejected - complicates navigation, harder to track position

---

## 6. Error Handling Integration

### Decision: Service_ErrorHandler with Context Data

**Rationale**:
- Constitution principle VII mandates Service_ErrorHandler, forbids MessageBox.Show()
- FR-039 requires user-friendly error messages without internal path exposure
- FR-040 requires logged parse failures
- Error scenarios: network path unavailable, file locked, parse failure, malformed entries

**Implementation Approach**:
```csharp
// Error handling patterns
private async Task LoadLogFileAsync(string filePath)
{
    try
    {
        var entries = await Service_LogFileReader.LoadEntriesAsync(filePath, 0, 1000);
        DisplayEntries(entries);
    }
    catch (UnauthorizedAccessException ex)
    {
        Service_ErrorHandler.HandleException(
            ex,
            ErrorSeverity.Medium,
            retryAction: () => LoadLogFileAsync(filePath),
            contextData: new Dictionary<string, object>
            {
                ["Username"] = _selectedUsername,
                ["LogType"] = _selectedLogType
            },
            controlName: nameof(ViewApplicationLogsForm)
        );
    }
    catch (IOException ex) when (ex.Message.Contains("network"))
    {
        // FR-039: User-friendly message without path details
        Service_ErrorHandler.HandleException(
            ex,
            ErrorSeverity.Medium,
            customMessage: "Unable to access log files. Please check network connection.",
            retryAction: () => LoadLogFileAsync(filePath),
            contextData: new Dictionary<string, object>
            {
                ["Username"] = _selectedUsername
            },
            controlName: nameof(ViewApplicationLogsForm)
        );
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        Service_ErrorHandler.HandleException(
            ex,
            ErrorSeverity.High,
            contextData: new Dictionary<string, object>
            {
                ["FilePath"] = Path.GetFileName(filePath),  // Filename only, not full path
                ["Username"] = _selectedUsername
            },
            controlName: nameof(ViewApplicationLogsForm)
        );
    }
}

private Model_LogEntry ParseEntryWithFallback(string rawText)
{
    try
    {
        var entry = Service_LogParser.ParseEntry(rawText);
        if (entry.ParseSuccess)
            return entry;
        
        // FR-040: Log parse failure
        
        
        return Model_LogEntry.CreateRawEntry(rawText);
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        return Model_LogEntry.CreateRawEntry(rawText);
    }
}
```

**Error Severity Mapping**:
- **Low**: Parse failures (non-critical, app continues)
- **Medium**: Network access errors, file locked (recoverable with retry)
- **High**: Unexpected exceptions, security validation failures
- **Fatal**: Not applicable for this feature

**Alternatives Considered**:
- **MessageBox.Show()**: Rejected - violates constitution principle VII
- **Silent failure**: Rejected - violates FR-040 logging requirement
- **Full path in error messages**: Rejected - violates FR-051 security requirement

---

## 7. Performance Monitoring and Optimization

### Decision: Async Operations with Progress Reporting

**Rationale**:
- SC-001 through SC-007 define specific performance benchmarks
- FR-043 requires progress indicator for background operations
- Existing Helper_StoredProcedureProgress pattern can be adapted for file operations
- Performance logging identifies bottlenecks during development

**Implementation Approach**:
```csharp
// Progress reporting during file operations
private Helper_StoredProcedureProgress _progressHelper;

private async Task LoadFileWithProgressAsync(string filePath)
{
    var stopwatch = Stopwatch.StartNew();
    
    _progressHelper?.ShowProgress("Loading log file...");
    
    try
    {
        var entries = await Service_LogFileReader.LoadEntriesAsync(filePath, 0, 1000);
        
        stopwatch.Stop();
        
        // Performance logging
        if (stopwatch.ElapsedMilliseconds > 2000)
        {
            
        }
        
        _progressHelper?.ShowSuccess($"Loaded {entries.Count} entries in {stopwatch.ElapsedMilliseconds}ms");
        
        DisplayEntries(entries);
    }
    catch (Exception ex)
    {
        _progressHelper?.ShowError("Failed to load log file");
        throw;
    }
}

// Auto-refresh timer with performance consideration (FR-046)
private System.Windows.Forms.Timer _autoRefreshTimer;

private void InitializeAutoRefresh()
{
    _autoRefreshTimer = new System.Windows.Forms.Timer
    {
        Interval = 5000  // 5 seconds per FR-033
    };
    _autoRefreshTimer.Tick += async (s, e) => await RefreshFileListAsync();
    
    // FR-046: Pause when form is minimized/hidden
    this.Resize += (s, e) =>
    {
        if (this.WindowState == FormWindowState.Minimized)
            _autoRefreshTimer.Stop();
        else if (chkAutoRefresh.Checked)
            _autoRefreshTimer.Start();
    };
}
```

**Performance Benchmarks**:
- User dropdown load: <1s (SC-001)
- File list load (20 files): <500ms (SC-002)
- Parse 1000 entries: <2s (SC-003)
- Entry navigation: <50ms (SC-004)
- Filtering 5000→100: <300ms (SC-005)
- Export 500 entries: <1s (SC-006)
- Auto-refresh: <500ms (SC-007)

**Alternatives Considered**:
- **Synchronous loading with busy cursor**: Rejected - poor UX, violates async-first principle
- **No progress feedback**: Rejected - violates FR-043
- **Continuous auto-refresh**: Rejected - violates FR-046 (pause when minimized)

---

## 8. Integration with Error Dialog

### Decision: Constructor Overload for Pre-Selection

**Rationale**:
- FR-037 requires pre-selection when opened from error dialog
- FR-038 requires no pre-selection when opened from Settings menu
- Constructor overload pattern maintains single form class with different initialization

**Implementation Approach**:
```csharp
public partial class ViewApplicationLogsForm : Form
{
    private string _preselectedUsername;
    private bool _openedFromErrorDialog;
    
    // Constructor for Settings menu access (FR-038)
    public ViewApplicationLogsForm()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        _openedFromErrorDialog = false;
        InitializeAsync().ConfigureAwait(false);
    }
    
    // Constructor for Error Dialog access (FR-037)
    public ViewApplicationLogsForm(string username)
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        _preselectedUsername = username;
        _openedFromErrorDialog = true;
        InitializeAsync().ConfigureAwait(false);
    }
    
    private async Task InitializeAsync()
    {
        await LoadUserListAsync();
        
        if (_openedFromErrorDialog && !string.IsNullOrEmpty(_preselectedUsername))
        {
            // Pre-select user and load most recent error log
            cmbUsers.SelectedItem = _preselectedUsername;
            await LoadMostRecentErrorLogAsync();
        }
        else
        {
            // Show placeholder, wait for user selection
            cmbUsers.SelectedIndex = -1;
            cmbUsers.Text = "Select User";
        }
        
        WireUpEvents();
    }
}

// Error Dialog integration
private void btnViewLogs_Click(object sender, EventArgs e)
{
    var currentUsername = Model_AppVariables.CurrentUser?.Username ?? "Unknown";
    var logViewerForm = new ViewApplicationLogsForm(currentUsername);
    logViewerForm.Show();
}
```

**Alternatives Considered**:
- **Separate forms**: Rejected - code duplication, maintenance burden
- **Static state**: Rejected - thread safety issues, complicates testing
- **Event-based initialization**: Rejected - harder to reason about initialization order

---

## 9. Export and Clipboard Operations

### Decision: SaveFileDialog with Formatted Text Output

**Rationale**:
- FR-034 requires "Export Visible" functionality
- FR-035 requires "Copy Entry" functionality
- FR-036 requires "Open Log Directory" functionality
- Formatted text preserves readability when pasted into documentation or bug reports

**Implementation Approach**:
```csharp
// Export filtered entries
private async Task ExportVisibleEntriesAsync()
{
    using var dialog = new SaveFileDialog
    {
        Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
        DefaultExt = "txt",
        FileName = $"LogExport_{_selectedUsername}_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
    };
    
    if (dialog.ShowDialog() == DialogResult.OK)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var filteredEntries = GetFilteredEntries();
        var formattedText = FormatEntriesForExport(filteredEntries);
        
        await File.WriteAllTextAsync(dialog.FileName, formattedText);
        
        stopwatch.Stop();
        
        // SC-006: Export 500 entries in <1s
        if (stopwatch.ElapsedMilliseconds > 1000)
        {
            
        }
        
        Service_ErrorHandler.ShowInformation($"Exported {filteredEntries.Count} entries to {Path.GetFileName(dialog.FileName)}");
    }
}

// Copy current entry
private void CopyCurrentEntry()
{
    var currentEntry = GetCurrentEntry();
    var formattedText = _isParsedView 
        ? FormatEntryParsed(currentEntry)
        : currentEntry.RawText;
    
    Clipboard.SetText(formattedText);
}

// Open log directory in Explorer
private void OpenLogDirectory()
{
    try
    {
        var userLogPath = Helper_LogPath.GetUserLogDirectory(_selectedUsername);
        Process.Start("explorer.exe", userLogPath);
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(
            ex,
            ErrorSeverity.Low,
            customMessage: "Unable to open log directory",
            controlName: nameof(ViewApplicationLogsForm)
        );
    }
}

private string FormatEntryParsed(Model_LogEntry entry)
{
    var sb = new StringBuilder();
    sb.AppendLine($"Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss.fff}");
    
    if (entry.LogType == LogFormat.Normal)
    {
        sb.AppendLine($"Level: {entry.Level} {entry.Emoji}");
        sb.AppendLine($"Source: {entry.Source}");
        sb.AppendLine($"Message: {entry.Message}");
        if (!string.IsNullOrEmpty(entry.Details))
            sb.AppendLine($"Details: {entry.Details}");
    }
    else if (entry.LogType == LogFormat.ApplicationError)
    {
        sb.AppendLine($"Error Type: {entry.ErrorType}");
        sb.AppendLine($"Message: {entry.Message}");
        sb.AppendLine($"Stack Trace:\n{entry.StackTrace}");
    }
    else if (entry.LogType == LogFormat.DatabaseError)
    {
        sb.AppendLine($"Severity: {entry.Severity}");
        sb.AppendLine($"Message: {entry.Message}");
        sb.AppendLine($"Stack Trace:\n{entry.StackTrace}");
    }
    
    return sb.ToString();
}
```

**Alternatives Considered**:
- **CSV export**: Rejected - loses formatting for stack traces, less readable
- **JSON export**: Rejected - not human-readable when pasted
- **Direct Process.Start on filename**: Rejected - security concern, prefer directory only

---

## Summary of Technical Decisions

| Area | Decision | Key Rationale |
|------|----------|---------------|
| **Parsing** | Regex-based with format detection | Three formats require different patterns, timeout protection |
| **File I/O** | Async FileStream with windowing | FR-041 async requirement, FR-044 large file handling |
| **Path Security** | Helper_LogPath with validation | FR-047/FR-049 security requirements, path traversal prevention |
| **Layout** | TableLayoutPanel + AutoScaleMode.Dpi | FR-052/FR-053 DPI requirements, responsive design |
| **Filtering** | In-memory LINQ with index tracking | Performance targets, FR-026 navigation requirements |
| **Error Handling** | Service_ErrorHandler with context | Constitution principle VII, FR-039/FR-040 requirements |
| **Performance** | Async + progress reporting | SC-001 through SC-007 benchmarks, FR-043 progress indicator |
| **Integration** | Constructor overload pattern | FR-037/FR-038 different entry points |
| **Export** | SaveFileDialog with formatted text | FR-034/FR-035 export requirements, readability |

---

## Open Questions / Risks

### Resolved:
- ✅ Log format parsing strategy → Regex with format detection
- ✅ Large file handling → Windowing with 1000-entry chunks
- ✅ Path security → Helper_LogPath with validation
- ✅ Filter performance → In-memory LINQ on windowed entries
- ✅ DPI scaling → Core_Themes integration, TableLayoutPanel

### Remaining (if any):
- ⚠️ **Network path configuration**: Is base log path `\\networkpath\logs` configurable or hardcoded? 
  - **Decision needed during implementation**: Add to Model_AppVariables or configuration file
  - **Impact**: Affects Helper_LogPath implementation
  - **Recommendation**: Make configurable for different environments (Dev/Test/Prod)

---

## Next Steps

**Phase 1 - Design**:
1. Generate data-model.md with entity definitions for LogEntry, LogFile, LogFilter, UserLogDirectory
2. Create contracts/ directory (if applicable - may not need API contracts for WinForms)
3. Generate quickstart.md with developer setup instructions
4. Update agent context files with new technology patterns from this research

**Phase 2 - Tasks** (separate command):
1. Generate tasks.md with implementation breakdown
2. Map tasks to instruction file references
3. Define testing scenarios and validation criteria

---

## References

- Feature Specification: `specs/003-view-application-logs/spec.md`
- Constitution: `.specify/memory/constitution.md`
- Instruction Files:
  - `.github/instructions/csharp-dotnet8.instructions.md`
  - `.github/instructions/security-best-practices.instructions.md`
  - `.github/instructions/performance-optimization.instructions.md`
  - `.github/instructions/ui-scaling-consistency.instructions.md`
  - `.github/instructions/winforms-responsive-layout.instructions.md`
