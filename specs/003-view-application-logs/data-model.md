# Data Model: View Application Logs

**Date**: 2025-10-26  
**Feature**: 003-view-application-logs  
**Phase**: 1 - Design

## Overview

This document defines the data models required for the View Application Logs feature. The models represent parsed log entries with type-specific fields, log file metadata, filter criteria, and user log directory information.

---

## Core Entities

### 1. Model_LogEntry

Represents a single parsed log entry with fields appropriate to the log format type.

**Purpose**: Encapsulates structured log data parsed from raw text files with fallback to raw view for unparseable entries.

**Location**: `Models/Model_LogEntry.cs`

```csharp
/// <summary>
/// Represents a parsed log entry with type-specific fields.
/// </summary>
public class Model_LogEntry
{
    #region Common Properties
    
    /// <summary>
    /// Timestamp when the log entry was recorded.
    /// </summary>
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Type of log entry (Normal, ApplicationError, DatabaseError).
    /// </summary>
    public LogFormat LogType { get; set; }
    
    /// <summary>
    /// Raw unparsed log text (fallback when parsing fails).
    /// </summary>
    public string RawText { get; set; }
    
    /// <summary>
    /// Indicates whether parsing succeeded for this entry.
    /// </summary>
    public bool ParseSuccess { get; set; }
    
    #endregion
    
    #region Normal Log Properties (Service_DebugTracer format)
    
    /// <summary>
    /// Log level for Normal logs: LOW, MEDIUM, HIGH, DATA.
    /// </summary>
    public string Level { get; set; }
    
    /// <summary>
    /// Emoji indicator from Normal logs (✅ ⏱️ 🗄️ ➡️ ⬅️).
    /// </summary>
    public string Emoji { get; set; }
    
    /// <summary>
    /// Source component or procedure name for Normal logs.
    /// </summary>
    public string Source { get; set; }
    
    /// <summary>
    /// Main message text.
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// JSON details or structured data for Normal logs.
    /// </summary>
    public string Details { get; set; }
    
    #endregion
    
    #region Application Error Log Properties
    
    /// <summary>
    /// Error type classification for Application Error logs.
    /// </summary>
    public string ErrorType { get; set; }
    
    #endregion
    
    #region Database Error Log Properties
    
    /// <summary>
    /// Severity level for Database Error logs: WARNING, ERROR, CRITICAL.
    /// </summary>
    public string Severity { get; set; }
    
    #endregion
    
    #region Common Error Properties
    
    /// <summary>
    /// Stack trace for Application Error and Database Error logs.
    /// </summary>
    public string StackTrace { get; set; }
    
    #endregion
    
    #region Factory Methods
    
    /// <summary>
    /// Creates a log entry from parsed Normal log format.
    /// </summary>
    public static Model_LogEntry CreateNormalEntry(
        DateTime timestamp,
        string level,
        string emoji,
        string source,
        string message,
        string details,
        string rawText)
    {
        return new Model_LogEntry
        {
            Timestamp = timestamp,
            LogType = LogFormat.Normal,
            Level = level,
            Emoji = emoji,
            Source = source,
            Message = message,
            Details = details,
            RawText = rawText,
            ParseSuccess = true
        };
    }
    
    /// <summary>
    /// Creates a log entry from parsed Application Error format.
    /// </summary>
    public static Model_LogEntry CreateApplicationErrorEntry(
        DateTime timestamp,
        string errorType,
        string message,
        string stackTrace,
        string rawText)
    {
        return new Model_LogEntry
        {
            Timestamp = timestamp,
            LogType = LogFormat.ApplicationError,
            ErrorType = errorType,
            Message = message,
            StackTrace = stackTrace,
            RawText = rawText,
            ParseSuccess = true
        };
    }
    
    /// <summary>
    /// Creates a log entry from parsed Database Error format.
    /// </summary>
    public static Model_LogEntry CreateDatabaseErrorEntry(
        DateTime timestamp,
        string severity,
        string message,
        string stackTrace,
        string rawText)
    {
        return new Model_LogEntry
        {
            Timestamp = timestamp,
            LogType = LogFormat.DatabaseError,
            Severity = severity,
            Message = message,
            StackTrace = stackTrace,
            RawText = rawText,
            ParseSuccess = true
        };
    }
    
    /// <summary>
    /// Creates a log entry with raw text only (parsing failed).
    /// </summary>
    public static Model_LogEntry CreateRawEntry(string rawText)
    {
        return new Model_LogEntry
        {
            Timestamp = DateTime.MinValue,
            LogType = LogFormat.Unknown,
            RawText = rawText,
            ParseSuccess = false
        };
    }
    
    #endregion
}
```

**Validation Rules**:
- `Timestamp` must be valid DateTime (defaults to DateTime.MinValue for unparseable entries)
- `RawText` is always preserved regardless of parse success
- `ParseSuccess` flag determines whether UI shows parsed or raw view
- Type-specific properties are null/empty when not applicable to LogType

**State Transitions**:
- Entry is immutable after creation (read-only after factory method)
- No state changes occur during application lifecycle

---

### 2. Model_LogFile

Represents metadata for a log file on disk.

**Purpose**: Provides file information for display in file list and tracking of loaded files.

**Location**: `Models/Model_LogFile.cs`

```csharp
/// <summary>
/// Represents a log file with metadata for display and selection.
/// </summary>
public class Model_LogFile
{
    /// <summary>
    /// Full path to the log file on disk.
    /// </summary>
    public string FilePath { get; set; }
    
    /// <summary>
    /// Filename without path (display name).
    /// </summary>
    public string FileName { get; set; }
    
    /// <summary>
    /// Type of log file based on filename suffix.
    /// </summary>
    public LogFormat LogType { get; set; }
    
    /// <summary>
    /// File size in bytes.
    /// </summary>
    public long FileSizeBytes { get; set; }
    
    /// <summary>
    /// Human-readable file size (e.g., "1.5 MB").
    /// </summary>
    public string FileSizeDisplay => FormatFileSize(FileSizeBytes);
    
    /// <summary>
    /// File creation date/time.
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// File last modified date/time.
    /// </summary>
    public DateTime ModifiedDate { get; set; }
    
    /// <summary>
    /// Username who owns this log file.
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Cached count of log entries in file (optional, loaded on demand).
    /// </summary>
    public int? EntryCount { get; set; }
    
    /// <summary>
    /// Indicates whether this file is currently selected/loaded.
    /// </summary>
    public bool IsSelected { get; set; }
    
    /// <summary>
    /// Formats file size into human-readable string.
    /// </summary>
    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes;
        int order = 0;
        
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        
        return $"{len:0.##} {sizes[order]}";
    }
    
    /// <summary>
    /// Returns display text for file list item.
    /// </summary>
    public override string ToString()
    {
        return $"{FileName} - {CreatedDate:yyyy-MM-dd HH:mm} - {FileSizeDisplay}";
    }
}
```

**Validation Rules**:
- `FilePath` must be valid file system path
- `FileName` must not be null or empty
- `LogType` determined by filename suffix: `*_normal.log`, `*_app_error.log`, `*_db_error.log`
- `FileSizeBytes` must be >= 0
- `CreatedDate` and `ModifiedDate` must be valid DateTime values

**Relationships**:
- Belongs to one `Model_UserLogDirectory`
- Contains zero or more `Model_LogEntry` instances (loaded on demand)

---

### 3. Model_LogFilter

Encapsulates active filter criteria for log entry filtering.

**Purpose**: Centralizes filter state and provides predicate logic for entry matching.

**Location**: `Models/Model_LogFilter.cs`

```csharp
/// <summary>
/// Represents active filter criteria for log entries.
/// </summary>
public class Model_LogFilter
{
    /// <summary>
    /// Start date for date range filter (inclusive). Null means no lower bound.
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// End date for date range filter (inclusive). Null means no upper bound.
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Log types to include (Normal, ApplicationError, DatabaseError).
    /// Empty/null means all types.
    /// </summary>
    public List<LogFormat> LogTypes { get; set; }
    
    /// <summary>
    /// Severity levels to include (context-dependent on log type).
    /// For Normal logs: LOW, MEDIUM, HIGH, DATA.
    /// For Database Error logs: WARNING, ERROR, CRITICAL.
    /// Empty/null means all severity levels.
    /// </summary>
    public List<string> SeverityLevels { get; set; }
    
    /// <summary>
    /// Source component filter (exact match). Null/empty means all sources.
    /// </summary>
    public string SourceComponent { get; set; }
    
    /// <summary>
    /// Free-text search term to match against all text fields.
    /// Uses case-insensitive substring matching with regex timeout.
    /// </summary>
    public string SearchText { get; set; }
    
    /// <summary>
    /// Indicates whether any filters are active.
    /// </summary>
    public bool HasActiveFilters =>
        StartDate.HasValue ||
        EndDate.HasValue ||
        (LogTypes?.Any() == true) ||
        (SeverityLevels?.Any() == true) ||
        !string.IsNullOrWhiteSpace(SourceComponent) ||
        !string.IsNullOrWhiteSpace(SearchText);
    
    /// <summary>
    /// Creates a default filter with no active criteria.
    /// </summary>
    public static Model_LogFilter CreateDefault()
    {
        return new Model_LogFilter
        {
            LogTypes = new List<LogFormat>(),
            SeverityLevels = new List<string>()
        };
    }
    
    /// <summary>
    /// Clears all filter criteria.
    /// </summary>
    public void Clear()
    {
        StartDate = null;
        EndDate = null;
        LogTypes?.Clear();
        SeverityLevels?.Clear();
        SourceComponent = null;
        SearchText = null;
    }
    
    /// <summary>
    /// Creates a clone of this filter.
    /// </summary>
    public Model_LogFilter Clone()
    {
        return new Model_LogFilter
        {
            StartDate = this.StartDate,
            EndDate = this.EndDate,
            LogTypes = this.LogTypes?.ToList(),
            SeverityLevels = this.SeverityLevels?.ToList(),
            SourceComponent = this.SourceComponent,
            SearchText = this.SearchText
        };
    }
}
```

**Validation Rules**:
- `StartDate` must be <= `EndDate` when both are set
- `LogTypes` must contain valid LogFormat enum values
- `SeverityLevels` must contain valid severity strings for the context log type
- `SearchText` length should be <= 500 characters to prevent performance issues

**Usage Pattern**:
- Form creates default filter on initialization
- Filter properties updated via UI controls (date pickers, checkboxes, textboxes)
- Filter applied to entry list using LINQ predicates
- Filter state persists when navigating between files of same log type (FR-028)

---

### 4. Model_UserLogDirectory

Represents a user's log directory with aggregated statistics.

**Purpose**: Provides user selection information and summary metrics for log directory.

**Location**: `Models/Model_UserLogDirectory.cs`

```csharp
/// <summary>
/// Represents a user's log directory with file count and size metrics.
/// </summary>
public class Model_UserLogDirectory
{
    /// <summary>
    /// Username (directory name).
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Full path to user's log directory.
    /// </summary>
    public string DirectoryPath { get; set; }
    
    /// <summary>
    /// Number of Normal log files (*_normal.log).
    /// </summary>
    public int NormalLogCount { get; set; }
    
    /// <summary>
    /// Number of Application Error log files (*_app_error.log).
    /// </summary>
    public int AppErrorLogCount { get; set; }
    
    /// <summary>
    /// Number of Database Error log files (*_db_error.log).
    /// </summary>
    public int DbErrorLogCount { get; set; }
    
    /// <summary>
    /// Total number of log files across all types.
    /// </summary>
    public int TotalLogCount => NormalLogCount + AppErrorLogCount + DbErrorLogCount;
    
    /// <summary>
    /// Total size of all log files in bytes.
    /// </summary>
    public long TotalSizeBytes { get; set; }
    
    /// <summary>
    /// Human-readable total size.
    /// </summary>
    public string TotalSizeDisplay => FormatFileSize(TotalSizeBytes);
    
    /// <summary>
    /// Date of most recent log file in directory.
    /// </summary>
    public DateTime? MostRecentLogDate { get; set; }
    
    /// <summary>
    /// Indicates whether directory is accessible (permissions check).
    /// </summary>
    public bool IsAccessible { get; set; }
    
    /// <summary>
    /// List of log files in this directory (loaded on demand).
    /// </summary>
    public List<Model_LogFile> LogFiles { get; set; }
    
    /// <summary>
    /// Formats file size into human-readable string.
    /// </summary>
    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes;
        int order = 0;
        
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        
        return $"{len:0.##} {sizes[order]}";
    }
    
    /// <summary>
    /// Returns display text for user dropdown.
    /// </summary>
    public override string ToString()
    {
        return $"{Username} ({TotalLogCount} files)";
    }
}
```

**Validation Rules**:
- `Username` must match pattern `[a-zA-Z0-9_-]+` (validated by Helper_LogPath)
- `DirectoryPath` must be valid directory path under base log directory
- File counts must be >= 0
- `TotalSizeBytes` must be >= 0
- `MostRecentLogDate` is null only when directory has no log files

**Relationships**:
- Contains zero or more `Model_LogFile` instances
- One directory per username

---

## Supporting Enumerations

### LogFormat

Represents the type of log file and entry format.

**Location**: `Models/LogFormat.cs` (or inline in Model_LogEntry.cs)

```csharp
/// <summary>
/// Represents the type of log file format.
/// </summary>
public enum LogFormat
{
    /// <summary>
    /// Unknown or unparseable log format.
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Normal application logs (*_normal.log) with Service_DebugTracer format.
    /// </summary>
    Normal = 1,
    
    /// <summary>
    /// Application error logs (*_app_error.log) with exception format.
    /// </summary>
    ApplicationError = 2,
    
    /// <summary>
    /// Database error logs (*_db_error.log) with severity and MySQL exceptions.
    /// </summary>
    DatabaseError = 3
}
```

**Usage**:
- Determines parsing strategy in Service_LogParser
- Determines field layout in form display
- Determines available severity filter options
- File list grouping and type badges

---

## Entity Relationships

```
Model_UserLogDirectory (1)
    ├── Username: string
    ├── DirectoryPath: string
    ├── Metrics: counts, sizes, dates
    └── LogFiles (0..*)
            │
            └── Model_LogFile (*)
                    ├── FilePath: string
                    ├── FileName: string
                    ├── LogType: LogFormat
                    ├── Metadata: size, dates
                    └── EntryCount: int?
                            │
                            └── [Loaded on demand] Model_LogEntry (*)
                                    ├── Timestamp: DateTime
                                    ├── LogType: LogFormat
                                    ├── RawText: string
                                    ├── ParseSuccess: bool
                                    └── Type-specific properties

Model_LogFilter (singleton per session)
    ├── Date range
    ├── Log types
    ├── Severity levels (dynamic based on log type)
    ├── Source component
    └── Search text
```

---

## Data Flow Patterns

### 1. User Selection → File List

```
User selects username in dropdown
    ↓
Helper_LogPath.GetUserLogDirectory(username) → validates and returns path
    ↓
Service_LogFileReader.EnumerateLogFiles(path) → scans directory
    ↓
Create Model_LogFile instances from FileInfo
    ↓
Group by LogFormat and sort by ModifiedDate descending
    ↓
Display in ListView with type badges
```

### 2. File Selection → Entry Display

```
User clicks log file in ListView
    ↓
Service_LogFileReader.LoadEntriesAsync(filePath, 0, 1000) → reads first window
    ↓
Service_LogParser.ParseEntry(rawLine) → returns Model_LogEntry
    ↓
Display entry in type-appropriate field layout
    ↓
Initialize LogEntryNavigator with entries and default filter
    ↓
Update entry count: "Entry 1 of N"
```

### 3. Filter Application → Entry Navigation

```
User modifies filter controls (date range, severity checkboxes, search text)
    ↓
Create/update Model_LogFilter with new criteria
    ↓
LogEntryNavigator.ApplyFilter(filter) → LINQ query on loaded entries
    ↓
Build filtered index list: List<int> (indices into original entry list)
    ↓
Reset current position to 0
    ↓
Display first matching entry
    ↓
Update count: "Entry 1 of X (Showing X of Y)"
```

### 4. Navigation (Next/Previous)

```
User clicks Next/Previous button
    ↓
Increment/decrement current filtered index
    ↓
Map filtered index to original entry index
    ↓
Retrieve Model_LogEntry at original index
    ↓
Display entry in type-appropriate format
    ↓
Update position: "Entry X of Y"
```

---

## Memory Management

### Windowing Strategy

**Problem**: Log files can contain 5000+ entries. Loading all into memory risks high memory usage.

**Solution**: Load entries in windows of 1000 (configurable).

**Implementation**:
```csharp
// Service_LogFileReader maintains current window
public class Service_LogFileReader
{
    private int _currentWindowStart = 0;
    private const int WindowSize = 1000;
    private List<Model_LogEntry> _currentWindow;
    
    public async Task<Model_LogEntry> GetEntryAsync(int absoluteIndex)
    {
        // Check if index is in current window
        if (absoluteIndex >= _currentWindowStart && 
            absoluteIndex < _currentWindowStart + WindowSize)
        {
            return _currentWindow[absoluteIndex - _currentWindowStart];
        }
        
        // Load new window
        _currentWindowStart = (absoluteIndex / WindowSize) * WindowSize;
        _currentWindow = await LoadEntriesAsync(
            _currentFilePath, 
            _currentWindowStart, 
            WindowSize
        );
        
        return _currentWindow[absoluteIndex - _currentWindowStart];
    }
}
```

**Trade-offs**:
- Memory: 1000 entries × ~500 bytes avg = ~500KB per window (acceptable)
- Performance: Window load requires file seek and parse (cached for navigation within window)
- Complexity: Filtering operates on current window only (acceptable for current requirements)

**Future Optimization**: If files grow beyond 10,000 entries, implement virtual scrolling with multi-window cache.

---

## Validation Summary

| Model | Key Validations |
|-------|----------------|
| **Model_LogEntry** | Timestamp valid, RawText preserved, Type-specific properties null when not applicable |
| **Model_LogFile** | FilePath valid, LogType matches filename suffix, FileSize >= 0 |
| **Model_LogFilter** | StartDate <= EndDate, SearchText <= 500 chars, Severity values valid for LogType |
| **Model_UserLogDirectory** | Username matches pattern, DirectoryPath under base path, Counts >= 0 |

---

## Testing Considerations

### Model Instantiation Tests
- Verify factory methods create entries with correct properties
- Test raw entry fallback when parsing fails
- Validate filter clone creates independent copy

### Validation Tests
- Test Model_LogFilter with invalid date ranges (StartDate > EndDate)
- Test Helper_LogPath with invalid usernames (path traversal attempts)
- Verify file size formatting edge cases (0 bytes, 1023 bytes, 1GB+)

### Performance Tests
- Measure memory footprint of 1000 Model_LogEntry instances
- Test filter application on 5000 entries (target <300ms per SC-005)
- Verify window loading performance (target <2s per SC-003)

---

## References

- Feature Specification: `specs/003-view-application-logs/spec.md`
- Research Decisions: `specs/003-view-application-logs/research.md`
- Implementation Plan: `specs/003-view-application-logs/plan.md`
- Relevant Instruction Files:
  - `.github/instructions/csharp-dotnet8.instructions.md` (nullable reference types, required members)
  - `.github/instructions/performance-optimization.instructions.md` (memory management, collection optimization)
