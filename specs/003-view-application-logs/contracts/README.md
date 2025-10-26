# Contracts: View Application Logs

**Date**: 2025-10-26  
**Feature**: 003-view-application-logs  
**Phase**: 1 - Design

## Overview

This directory typically contains API contracts (REST endpoints, GraphQL schemas, message formats) for features with external interfaces. However, the View Application Logs feature is a **WinForms desktop UI** with **file system I/O only** and does not expose external APIs.

---

## Internal Contracts

While there are no external API contracts, the feature defines **internal interface contracts** between services and the UI layer:

### Service_LogParser Contract

**Purpose**: Parse raw log text into structured Model_LogEntry objects

**Interface**:
```csharp
public static class Service_LogParser
{
    /// <summary>
    /// Detects log format from first line of log entry.
    /// </summary>
    /// <param name="firstLine">First line of log entry text</param>
    /// <returns>LogFormat enum value</returns>
    public static LogFormat DetectFormat(string firstLine);
    
    /// <summary>
    /// Parses raw log text into structured Model_LogEntry.
    /// </summary>
    /// <param name="rawText">Raw log entry text</param>
    /// <param name="format">Expected log format (or Unknown for auto-detection)</param>
    /// <returns>Model_LogEntry with ParseSuccess flag indicating result</returns>
    public static Model_LogEntry ParseEntry(string rawText, LogFormat format = LogFormat.Unknown);
}
```

**Contract Guarantees**:
- Always returns Model_LogEntry (never null)
- ParseSuccess flag indicates whether parsing succeeded
- RawText property always preserved regardless of parse success
- Regex timeout protection (1 second max) prevents ReDoS
- Thread-safe (static methods, no shared state)

**Error Handling**:
- Parse failures return Model_LogEntry with ParseSuccess=false, RawText populated
- Regex timeout exceptions caught and logged, returns unparsed entry
- Invalid input (null/empty) returns unparsed entry with empty RawText

---

### Service_LogFileReader Contract

**Purpose**: Provide async file I/O with windowing for large files

**Interface**:
```csharp
public class Service_LogFileReader : IDisposable
{
    /// <summary>
    /// Enumerates log files in user directory.
    /// </summary>
    /// <param name="userDirectory">Validated user directory path</param>
    /// <returns>List of Model_LogFile with metadata</returns>
    public async Task<List<Model_LogFile>> EnumerateLogFilesAsync(string userDirectory);
    
    /// <summary>
    /// Loads window of log entries from file.
    /// </summary>
    /// <param name="filePath">Validated file path</param>
    /// <param name="startIndex">Zero-based index of first entry to load</param>
    /// <param name="count">Number of entries to load</param>
    /// <param name="cancellationToken">Cancellation token for async operation</param>
    /// <returns>List of parsed Model_LogEntry objects</returns>
    public async Task<List<Model_LogEntry>> LoadEntriesAsync(
        string filePath,
        int startIndex,
        int count,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets total entry count without loading all entries.
    /// </summary>
    /// <param name="filePath">Validated file path</param>
    /// <returns>Total number of entries in file</returns>
    public async Task<int> GetTotalEntryCountAsync(string filePath);
}
```

**Contract Guarantees**:
- All methods are async and never block UI thread
- EnumerateLogFilesAsync returns empty list if directory has no files (not null)
- LoadEntriesAsync returns entries up to count or end of file, whichever is first
- FileShare.ReadWrite allows reading locked files
- CancellationToken support enables operation interruption
- Disposable pattern ensures file handles released

**Error Handling**:
- UnauthorizedAccessException thrown for permission issues
- IOException thrown for file system errors (network unavailable, file locked)
- ArgumentException thrown for invalid paths (caught by Helper_LogPath validation)
- Cancellation via CancellationToken throws OperationCanceledException

---

### Helper_LogPath Contract

**Purpose**: Provide secure file path construction with validation

**Interface**:
```csharp
public static class Helper_LogPath
{
    /// <summary>
    /// Gets validated user log directory path.
    /// </summary>
    /// <param name="username">Username (validated against pattern)</param>
    /// <returns>Full path to user log directory</returns>
    /// <exception cref="ArgumentException">Invalid username format</exception>
    /// <exception cref="SecurityException">Path traversal attempt detected</exception>
    public static string GetUserLogDirectory(string username);
    
    /// <summary>
    /// Constructs validated log file path.
    /// </summary>
    /// <param name="username">Username (validated)</param>
    /// <param name="filename">Filename (validated)</param>
    /// <returns>Full path to log file</returns>
    /// <exception cref="ArgumentException">Invalid filename format</exception>
    /// <exception cref="SecurityException">Path traversal attempt detected</exception>
    public static string GetLogFilePath(string username, string filename);
}
```

**Contract Guarantees**:
- Username validated against pattern: `[a-zA-Z0-9_-]+`
- Filename validated: no parent directory references, not rooted
- All paths constructed with Path.Combine (FR-047)
- Path traversal prevention via GetFullPath validation (FR-049)
- Returns normalized full paths only
- Thread-safe (static methods, no shared state)

**Error Handling**:
- ArgumentException for null/empty/invalid input (FR-048)
- SecurityException for path traversal attempts (FR-049)
- Error messages do not reveal internal path structure (FR-051)

---

## Form-Level Contracts

### ViewApplicationLogsForm Constructor Contract

**Purpose**: Provide two initialization modes (Settings menu vs Error Dialog)

**Interface**:
```csharp
public partial class ViewApplicationLogsForm : Form
{
    /// <summary>
    /// Creates form with no pre-selection (Settings menu access).
    /// </summary>
    public ViewApplicationLogsForm();
    
    /// <summary>
    /// Creates form with user pre-selected (Error Dialog access).
    /// </summary>
    /// <param name="username">Username to pre-select and load logs for</param>
    public ViewApplicationLogsForm(string username);
}
```

**Contract Guarantees**:
- Parameterless constructor shows user dropdown with no selection (FR-038)
- Parameterized constructor pre-selects user and loads most recent error log (FR-037)
- Both constructors call Core_Themes.ApplyDpiScaling() and ApplyRuntimeLayoutAdjustments() (FR-053)
- Form follows constitution region organization
- Async initialization via InitializeAsync() pattern

---

## Data Model Contracts

### Model_LogEntry Factory Methods

See `data-model.md` for complete definitions. Key contracts:

**CreateNormalEntry**:
- Parameters: timestamp, level, emoji, source, message, details, rawText
- Returns: Model_LogEntry with LogType=Normal, ParseSuccess=true

**CreateApplicationErrorEntry**:
- Parameters: timestamp, errorType, message, stackTrace, rawText
- Returns: Model_LogEntry with LogType=ApplicationError, ParseSuccess=true

**CreateDatabaseErrorEntry**:
- Parameters: timestamp, severity, message, stackTrace, rawText
- Returns: Model_LogEntry with LogType=DatabaseError, ParseSuccess=true

**CreateRawEntry**:
- Parameters: rawText
- Returns: Model_LogEntry with LogType=Unknown, ParseSuccess=false, Timestamp=DateTime.MinValue

---

## Event Contracts

### Form Events

**User Selection Changed**:
```csharp
private async void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
{
    // Contract: Load file list for selected user
    // Must be async to prevent UI blocking
    // Must handle errors with Service_ErrorHandler
}
```

**Log File Selected**:
```csharp
private async void lstLogFiles_SelectedIndexChanged(object sender, EventArgs e)
{
    // Contract: Load log entries from selected file
    // Must apply current filter after loading
    // Must update entry count display
}
```

**Filter Changed**:
```csharp
private void ApplyFilter()
{
    // Contract: Update filtered indices list
    // Must reset current position to 0
    // Must update entry count display
    // Must maintain filter state when navigating between same log type
}
```

---

## Performance Contracts

All service methods must meet performance targets defined in spec.md success criteria:

- **EnumerateLogFilesAsync**: <500ms for 20 files (SC-002)
- **LoadEntriesAsync**: <2000ms for 1000 entries (SC-003)
- **ParseEntry**: <10ms per entry average
- **ApplyFilter**: <300ms for 5000→100 entries (SC-005)

---

## Security Contracts

All file path operations must:
- Use Path.Combine for path construction (FR-047)
- Validate input before file system access (FR-048)
- Prevent path traversal via base directory validation (FR-049)
- Include regex timeout protection (1 second) for search patterns (FR-050)
- Sanitize error messages to hide internal paths (FR-051)

---

## Summary

While this feature does not expose external APIs, it defines clear **internal service contracts** that ensure:

1. **Service_LogParser**: Reliable parsing with fallback to raw view
2. **Service_LogFileReader**: Async file I/O with performance guarantees
3. **Helper_LogPath**: Secure path construction with validation
4. **ViewApplicationLogsForm**: Dual initialization modes with DPI scaling
5. **Model_LogEntry**: Type-safe factory methods with guaranteed properties

These contracts enable **testability**, **maintainability**, and **clear separation of concerns** between UI and service layers.

---

## References

- **Data Model Definitions**: `specs/003-view-application-logs/data-model.md`
- **Implementation Plan**: `specs/003-view-application-logs/plan.md`
- **Feature Specification**: `specs/003-view-application-logs/spec.md`
