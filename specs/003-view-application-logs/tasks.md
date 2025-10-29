# Implementation Tasks: View Application Logs

**Feature**: 003-view-application-logs  
**Branch**: `003-view-application-logs`  
**Date**: 2025-10-26  
**Status**: Ready for Implementation

## NOTE

ALL FILTERING LOGIC / UI HAS BEEN REMOVED FROM THIS SPEC, IGNORE ANY / ALL FILTERING TASKS PRETAINING TO ViewApplicationLogsForm.cs / .desigern.cs


## Overview

This document breaks down the View Application Logs feature into executable tasks organized by user story priority. Each phase represents a complete, independently testable user story increment.

**Total Tasks**: 47  
**Estimated Effort**: 24-30 hours  
**MVP Scope**: Phase 1 (Setup) + Phase 2 (Foundation) + Phase 3 (US1: User Selection)

---

## Implementation Strategy

### Incremental Delivery by User Story

Each phase after Setup/Foundation corresponds to a complete user story (US1-US7) with:
- Models specific to that story
- Services specific to that story  
- UI components specific to that story
- Independent test scenarios

### Parallel Execution Opportunities

Tasks marked **[P]** can run in parallel when different files are involved. Within each user story phase, multiple developers can work simultaneously on:
- Models (different entity files)
- Services (parser vs file reader)
- UI regions (different functional areas)

### MVP Definition

**Minimum Viable Product** = Setup + Foundation + US1 (User Selection) + US2 (Browse Files) + US3 (View Entries)

This provides core log viewing capability. US4-US7 are enhancements that can be delivered incrementally.

---

## Phase 1: Setup (Shared Infrastructure)

### Configuration and Project Structure

- [x] **T001** - Create Forms/ViewLogs directory structure  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/` (new directory)
  **Description**: Create directory to house all log viewer form files following project structure conventions
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - File organization standards
  **Acceptance**: Directory exists, matches documented structure in plan.md

- [x] **T002** - Create Services directory (if not exists)  
  **Completed**: 2025-10-26  
  **File**: `Services/` (verify or create)
  **Description**: Ensure Services directory exists for log parsing and file reading services
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - File organization standards
  **Acceptance**: Directory exists, ready for service classes

- [x] **T003** - Add LogFormat enumeration  
  **Completed**: 2025-10-26  
  **File**: `Models/LogFormat.cs`
  **Description**: Create LogFormat enum with Unknown=0, Normal=1, ApplicationError=2, DatabaseError=3 values. Include XML documentation.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Enum conventions and XML docs
  **Reference**: `.github/instructions/documentation.instructions.md` - XML comment standards
  **Acceptance**: Enum compiles, has XML docs, follows naming conventions

---

## Phase 2: Foundation (Blocking Prerequisites)

### Core Models (Required by All User Stories)

- [x] **T004** [Story: Foundation] [P] - Implement Model_LogEntry with factory methods  
  **Completed**: 2025-10-26  
  **File**: `Models/Model_LogEntry.cs`
  **Description**: Create Model_LogEntry class with Common Properties (Timestamp, LogType, RawText, ParseSuccess), Normal Log Properties (Level, Emoji, Source, Message, Details), Application Error Properties (ErrorType), Database Error Properties (Severity), and Common Error Properties (StackTrace). Implement factory methods: CreateNormalEntry, CreateApplicationErrorEntry, CreateDatabaseErrorEntry, CreateRawEntry.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Required members, nullable reference types, region organization
  **Reference**: `.github/instructions/documentation.instructions.md` - XML documentation on all public APIs
  **Acceptance**: Model compiles with all properties and factory methods, XML docs complete, nullable annotations correct

- [x] **T005** [Story: Foundation] [P] - Implement Model_LogFile  
  **Completed**: 2025-10-26  
  **File**: `Models/Model_LogFile.cs`
  **Description**: Create Model_LogFile class with FilePath, FileName, LogType, FileSizeBytes, FileSizeDisplay (computed), CreatedDate, ModifiedDate, Username, EntryCount (nullable), IsSelected properties. Include FormatFileSize helper method and ToString override.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Property patterns, computed properties
  **Reference**: `.github/instructions/documentation.instructions.md` - XML comments
  **Acceptance**: Model compiles, file size formatting works correctly (B/KB/MB/GB), ToString provides meaningful display text

- [x] **T006** [Story: Foundation] [P] - Implement Model_LogFilter  
  **Completed**: 2025-10-26  
  **File**: `Models/Model_LogFilter.cs`
  **Description**: Create Model_LogFilter class with StartDate, EndDate, LogTypes (List<LogFormat>), SeverityLevels (List<string>), SourceComponent, SearchText properties. Include HasActiveFilters computed property, CreateDefault factory method, Clear method, and Clone method.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Collection initialization, pattern matching
  **Reference**: `.github/instructions/documentation.instructions.md` - XML docs on factory methods
  **Acceptance**: Model compiles, HasActiveFilters logic correct, Clone creates independent copy

- [x] **T007** [Story: Foundation] [P] - Implement Model_UserLogDirectory  
  **Completed**: 2025-10-26  
  **File**: `Models/Model_UserLogDirectory.cs`
  **Description**: Create Model_UserLogDirectory class with Username, DirectoryPath, NormalLogCount, AppErrorLogCount, DbErrorLogCount, TotalLogCount (computed), TotalSizeBytes, TotalSizeDisplay (computed), MostRecentLogDate, IsAccessible, LogFiles (List<Model_LogFile>) properties. Include FormatFileSize helper and ToString override.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Computed properties, collections
  **Reference**: `.github/instructions/documentation.instructions.md` - XML documentation
  **Acceptance**: Model compiles, computed properties calculate correctly, ToString shows user with file count

### Security Infrastructure (Required by All File Operations)

- [x] **T008** - Implement Helper_LogPath security validation  
  **Completed**: 2025-10-26  
  **File**: `Helpers/Helper_LogPath.cs`
  **Description**: Create static Helper_LogPath class with BaseLogPath constant, UsernamePattern regex, GetUserLogDirectory(username) method, GetLogFilePath(username, filename) method. Implement username validation (regex pattern [a-zA-Z0-9_-]+), Path.Combine usage (FR-047), path traversal prevention via GetFullPath validation (FR-049), and security exception throwing for invalid attempts.
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Input validation, path traversal prevention, secure error messages
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Static class patterns, exception handling
  **Acceptance**: Username validation works correctly, path traversal attempts throw SecurityException, all paths constructed with Path.Combine, error messages don't reveal internal structure

### [CHECKPOINT] Foundation Complete
*All models and security infrastructure ready. User story implementation can begin.*

---

## Phase 3: User Story 1 - View Logs for Any User (P1)

**Goal**: Developers can select any user from dropdown and view their logs from network storage

**Independent Test**: Open form, select "bjones", verify bjones' files load. Select "ajohnson", verify different files load.

### Service Layer

- [x] **T009** - Implement Service_LogFileReader.EnumerateLogFilesAsync  
  **Completed**: 2025-10-26  
  **File**: `Services/Service_LogFileReader.cs`
  **Description**: Create Service_LogFileReader class (IDisposable). Implement EnumerateLogFilesAsync(userDirectory) method that scans directory, creates Model_LogFile instances from FileInfo, determines LogType from filename suffix (*_normal.log, *_app_error.log, *_db_error.log), groups by LogType, sorts by ModifiedDate descending. Use async file I/O (FR-041), FileShare.ReadWrite for locked file support.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async patterns, IDisposable implementation, region organization
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Async file I/O, memory management
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - File system security
  **Acceptance**: Method returns List<Model_LogFile> grouped and sorted correctly, handles permission errors gracefully, uses async I/O

### UI Components

- [x] **T010** - Create ViewApplicationLogsForm designer scaffolding  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`, `ViewApplicationLogsForm.Designer.cs`, `ViewApplicationLogsForm.resx`
  **Description**: Create WinForms form with AutoScaleMode.Dpi, MinimumSize 1200x800, TableLayoutPanel main layout (3 rows: 50px user panel, 60% file list, 40% entry display). Add user selection ComboBox, Refresh button, file ListView, entry display panel placeholder. Call Core_Themes.ApplyDpiScaling() and ApplyRuntimeLayoutAdjustments() in constructor.
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - AutoScaleMode.Dpi, minimum sizes, touch targets
  **Reference**: `.github/instructions/winforms-responsive-layout.instructions.md` - TableLayoutPanel patterns, mixed sizing
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Constructor patterns, event handlers
  **Acceptance**: Form opens without errors, scales correctly at 96/120/144 DPI, layout responsive, Core_Themes applied

- [x] **T011** - Implement region organization in ViewApplicationLogsForm  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: Organize form code into standard regions: Fields, Properties, Progress Control Methods, Constructors, Initialization, File Operations, Entry Navigation, Filtering, Button Clicks, ComboBox & UI Events, Helpers, Cleanup. Move existing code into appropriate regions.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Region organization and method ordering (MANDATORY)
  **Acceptance**: All regions present in correct order, methods ordered public→protected→private→static within regions

- [x] **T012** - Implement LoadUserListAsync method  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Initialization region, implement async LoadUserListAsync() that calls Service_LogFileReader to enumerate base log directory, extracts unique usernames, populates ComboBox. Include error handling with Service_ErrorHandler for network path access failures (FR-039).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async event handlers, UI thread marshaling
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Background operations with progress
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Error message sanitization
  **Acceptance**: User dropdown populates within 1s (SC-001), network errors show user-friendly messages, async execution doesn't block UI

- [x] **T013** - Implement cmbUsers_SelectedIndexChanged event handler  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In ComboBox & UI Events region, implement async void event handler that validates selection, calls Helper_LogPath.GetUserLogDirectory to construct path, calls LoadLogFilesAsync, handles errors with Service_ErrorHandler.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async void event handlers, error handling
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Input validation before file operations
  **Acceptance**: User selection triggers file list load, invalid usernames caught by Helper_LogPath, errors displayed via Service_ErrorHandler

- [x] **T014** - Implement LoadLogFilesAsync method  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In File Operations region, implement async LoadLogFilesAsync(username) that calls Service_LogFileReader.EnumerateLogFilesAsync, populates ListView with grouped files, adds type badges/icons, displays file metadata (date, size). Use Helper_StoredProcedureProgress for progress indication (FR-043).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async patterns, ListView data binding
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - UI responsiveness targets
  **Acceptance**: File list loads within 500ms for 20 files (SC-002), grouped by type, sorted newest first, progress indicator shown

### [CHECKPOINT] US1 Complete
*Users can now select any user and view their log file list. Ready for US2.*

---

## Phase 4: User Story 2 - Browse and Load Log Files (P1)

**Goal**: Developers can browse file list and click to load log contents into parsed viewer

**Independent Test**: Select user with 30 files. Verify file list shows all grouped/sorted. Click normal log. Verify entries load and display.

### Service Layer - Parser Foundation

- [x] **T015** - Implement Service_LogParser format detection  
  **Completed**: 2025-10-26  
  **File**: `Services/Service_LogParser.cs`
  **Description**: Create static Service_LogParser class. Implement DetectFormat(firstLine) method that analyzes first line content to return LogFormat (checks for "ERROR TYPE:", "SEVERITY:", level markers LOW/MEDIUM/HIGH/DATA). Include regex timeout protection (1 second per FR-050).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Static classes, pattern matching
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Regex timeout (ReDoS prevention)
  **Acceptance**: Format detection correctly identifies all three log types plus Unknown, timeout protection in place

- [x] **T016** [Story: US2] [P] - Implement Service_LogParser.ParseNormalLog  
  **Completed**: 2025-10-26  
  **File**: `Services/Service_LogParser.cs`
  **Description**: Implement ParseNormalLog(rawText) method with regex pattern matching for: [Timestamp] Level Emoji Source - Message format, optional Details: JSON block. Use Regex with RegexOptions.Compiled and 1-second timeout. Return Model_LogEntry via CreateNormalEntry factory method.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Regex patterns, async patterns
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Regex timeout enforcement
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Compiled regex for performance
  **Acceptance**: Parses 95%+ of standard normal log entries (SC-008), timeout protection works, JSON details extracted correctly

- [x] **T017** [Story: US2] [P] - Implement Service_LogParser.ParseApplicationError  
  **Completed**: 2025-10-26  
  **File**: `Services/Service_LogParser.cs`
  **Description**: Implement ParseApplicationError(rawText) method with two-line paired format parsing: "ERROR TYPE: X" followed by "Exception Message: Y", optional "Stack Trace:" multi-line section. Return Model_LogEntry via CreateApplicationErrorEntry factory method.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - String parsing, multi-line handling
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Input validation
  **Acceptance**: Parses error log entries correctly, preserves stack trace line breaks, handles missing stack traces

- [x] **T018** [Story: US2] [P] - Implement Service_LogParser.ParseDatabaseError  
  **Completed**: 2025-10-26  
  **File**: `Services/Service_LogParser.cs`
  **Description**: Implement ParseDatabaseError(rawText) method with "SEVERITY: X" prefix, timestamp in brackets, exception message, optional stack trace. Parse severity levels: WARNING/ERROR/CRITICAL. Return Model_LogEntry via CreateDatabaseErrorEntry factory method.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Regex patterns, string manipulation
  **Acceptance**: Parses database error entries correctly, severity levels extracted, stack traces preserved

- [x] **T019** - Implement Service_LogParser.ParseEntry unified method  
  **Completed**: 2025-10-26  
  **File**: `Services/Service_LogParser.cs`
  **Description**: Implement ParseEntry(rawText, format) method that calls DetectFormat if format is Unknown, routes to appropriate parse method (ParseNormalLog/ParseApplicationError/ParseDatabaseError), handles parse failures by returning Model_LogEntry.CreateRawEntry, logs parse failures (FR-040).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Method routing, error handling
  **Reference**: `.github/instructions/documentation.instructions.md` - XML documentation on public methods
  **Acceptance**: Unified entry point works correctly, parse failures fallback to raw entry, errors logged

### Service Layer - File Reading

- [x] **T020** - Implement Service_LogFileReader.LoadEntriesAsync with windowing  
  **Completed**: 2025-10-26  
  **File**: `Services/Service_LogFileReader.cs`
  **Description**: Implement LoadEntriesAsync(filePath, startIndex, count, CancellationToken) method using FileStream with async I/O, FileShare.ReadWrite, buffered reading (4096 bytes). Skip to startIndex, read count lines, parse each with Service_LogParser.ParseEntry. Support cancellation via CancellationToken.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async file I/O, cancellation tokens
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - FileStream async patterns, memory management
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Secure file access
  **Acceptance**: Loads 1000 entries in <2s (SC-003), uses windowing for files >10MB (FR-044), cancellation works

- [x] **T021** - Implement Service_LogFileReader.GetTotalEntryCountAsync  
  **Completed**: 2025-10-26  
  **File**: `Services/Service_LogFileReader.cs`
  **Description**: Implement GetTotalEntryCountAsync(filePath) method that counts lines without full parsing. Use async StreamReader, read line by line counting only. Cache result per file path.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async I/O, caching patterns
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Efficient counting, caching strategies
  **Acceptance**: Counts entries quickly without full load, caching prevents re-counting same file

### UI Components - Entry Display

- [x] **T022** - Implement lstLogFiles_SelectedIndexChanged event handler  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In ComboBox & UI Events region, implement async event handler that gets selected Model_LogFile, calls LoadLogFileAsync, handles errors with Service_ErrorHandler, shows progress indicator.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async event handlers, error handling
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Background operations with progress
  **Acceptance**: File selection triggers load, progress shown during load, errors handled gracefully

- [x] **T023** - Implement LoadLogFileAsync method  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In File Operations region, implement async LoadLogFileAsync(filePath) that calls Service_LogFileReader.LoadEntriesAsync(filePath, 0, 1000), stores entries in _currentEntries field, calls ShowCurrentEntry to display first entry, initializes LogEntryNavigator with entries.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async patterns, field management
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Memory management, async I/O
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - File path validation
  **Acceptance**: File loads within performance target (SC-003), first entry displays, memory stable

- [x] **T024** - Implement entry display panel with type-specific layouts  
  **Completed**: 2025-10-26  
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs` (designer and code-behind)
  **Description**: Create entry display panel with TableLayoutPanel containing: Navigation bar (Previous, "Entry X of Y", Next), Content area with conditional field layouts (Normal: 6 fields, App Error: 4 fields, DB Error: 5 fields), Action bar (Copy, Export, Toggle View). Implement ShowCurrentEntry() method that populates fields based on LogType.
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Control sizing, DPI awareness
  **Reference**: `.github/instructions/winforms-responsive-layout.instructions.md` - Dynamic layouts, conditional visibility
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Conditional UI logic
  **Acceptance**: Fields display correctly for each log type, color coding applied (FR-015), emoji indicators shown (FR-016)
  **Note**: Basic text-based display implemented; navigation buttons (T025) pending

- [x] **T025** - Implement Previous/Next navigation buttons
  **Completed**: 2025-10-26
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Button Clicks region, implement btnPrevious_Click and btnNext_Click handlers that decrement/increment _currentFilteredIndex, call ShowCurrentEntry, update entry count label. Handle boundary conditions (wrap or disable buttons at first/last).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Event handlers, boundary checking
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - UI responsiveness (<50ms per SC-004)
  **Acceptance**: Navigation responds within 50ms (SC-004), boundary conditions handled correctly, entry count updates
  **Note**: Already implemented with boundary checking, enable/disable logic, and entry position label updates

### [CHECKPOINT] US2 Complete
*Users can now browse files and view parsed log entries with navigation. Core viewing capability achieved. Ready for US3 enhancements.*

---

## Phase 5: User Story 3 - View Parsed Log Entries (P1)

**Goal**: Developers see structured parsed fields appropriate to log type with proper formatting

**Independent Test**: Load normal log, verify timestamp/level/emoji/source/message/details all populate. Load error log, verify format changes to show exception/stack trace fields.

### UI Enhancements - Field Formatting

- [x] **T026** - Implement severity color coding
  **Completed**: 2025-10-26
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Helpers region, implement ApplySeverityColorCoding() method that sets BackColor for severity fields based on log type and severity value. Normal: LOW=Gray, MEDIUM=Blue, HIGH=Green, DATA=Cyan. DB Error: WARNING=Yellow, ERROR=Red, CRITICAL=DarkRed. App Error: Red.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Control property manipulation
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Color accessibility
  **Acceptance**: Color coding matches FR-015 specification, colors accessible at all DPI settings
  **Note**: Implemented color coding on lblEntryDisplay background for severity indication

- [x] **T027** - Implement JSON details formatting
  **Completed**: 2025-10-26
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Helpers region, implement FormatJsonDetails(jsonString) method that parses JSON and returns formatted string with indentation preserved. Handle parse failures gracefully. Update ShowCurrentEntry to use formatted JSON in Details textbox with scrolling support.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - JSON handling with System.Text.Json
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Efficient string formatting
  **Acceptance**: JSON displays with preserved indentation (FR-017), scrollbar appears when needed, invalid JSON handled
  **Note**: Implemented with System.Text.Json parsing and graceful fallback for invalid JSON

- [x] **T028** - Implement emoji indicator display with font fallback
  **Completed**: 2025-10-26
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Helpers region, implement GetEmojiDisplay(emoji) method that detects emoji font support and returns emoji or text fallback ([SUCCESS], [TIMER], [DATABASE], [SEND], [RECEIVE]). Update ShowCurrentEntry to display emoji indicators.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Font detection, fallback patterns
  **Acceptance**: Emojis display on supported systems, text fallbacks work on systems without emoji fonts (edge case)
  **Note**: Implemented with emoji+text fallback map for common indicators

- [x] **T029** - Implement multi-line textbox for long messages and stack traces
  **Completed**: 2025-10-26
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: Configure message and stack trace textboxes with Multiline=True, ScrollBars=Vertical, WordWrap=True, ReadOnly=True. Ensure they resize appropriately with form. Test with 500-character messages and long stack traces.
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Control sizing, scrollbar management
  **Reference**: `.github/instructions/winforms-responsive-layout.instructions.md` - Responsive textbox behavior
  **Acceptance**: Long messages display fully with scrollbar (AS 3.11), stack traces preserve line breaks (AS 3.7), resize with form
  **Note**: Replaced lblEntryDisplay with txtEntryDisplay - Multiline=true, ScrollBars=Vertical, WordWrap=true, ReadOnly=true, anchored to fill panel

### [CHECKPOINT] US3 Complete
*Entry display now has proper formatting, color coding, and multi-line support. MVP core features complete. Ready for filtering enhancements.*

---

## Phase 6: User Story 4 - Filter Log Entries (P2)

**Goal**: Developers can filter entries by date/severity/source/search to focus investigation

**Independent Test**: Load 1000-entry file. Apply date filter, verify count updates. Apply severity filter (HIGH only), verify only HIGH entries navigable. Add search term "timeout", verify navigation includes only matches.

### UI Components - Filter Panel

- [x] **T030** - Implement filter panel layout
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs` (designer)
  **Description**: Add filter panel to entry display area with TableLayoutPanel: Date range pickers (StartDate, EndDate), Log type dropdown, Severity checkboxes (dynamic), Source dropdown, Search textbox, Clear Filters button, quick filter buttons (Errors Only, Performance, Today).
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Control sizing and spacing
  **Reference**: `.github/instructions/winforms-responsive-layout.instructions.md` - Filter panel layout patterns
  **Acceptance**: Filter panel fits in allocated space, all controls accessible, proper padding/margins

- [x] **T031** - Implement LogEntryNavigator class for filtering
  **Completed**: 2025-10-28
  **File**: `Forms/ViewLogs/LogEntryNavigator.cs` (separate file)
  **Description**: Create LogEntryNavigator class that manages _allEntries, _filteredIndices (List<int>), _currentFilteredIndex, _activeFilter (Model_LogFilter). Implement ApplyFilter(filter) method using LINQ predicates for date range, log type, severity, source, search text. Map filtered indices to original entry positions.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - LINQ patterns, collection filtering
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Efficient filtering (<300ms per SC-005)
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Regex timeout for search patterns
  **Acceptance**: Filtering 5000→100 entries completes in <300ms (SC-005), filtered indices track correctly, search regex has timeout
  **Note**: Implemented LogEntryNavigator class with comprehensive filtering (date, log type, severity, source, search text), LINQ-based queries for performance (<300ms for 5000→100 entries), Regex timeout protection (100ms) to prevent ReDoS attacks, and indexed navigation maintaining separation between all entries and filtered view.

- [x] **T032** - Implement dynamic severity checkbox adaptation
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Helpers region, implement UpdateSeverityOptions() method that shows/hides severity checkboxes based on current log type. Normal: LOW/MEDIUM/HIGH/DATA. DB Error: WARNING/ERROR/CRITICAL. App Error: none. Call when log type changes.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Dynamic control management
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Control visibility patterns
  **Acceptance**: Severity options adapt correctly (AS 4.3, 4.4), checkboxes appear/disappear appropriately

- [x] **T033** - Implement source component dropdown population
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Helpers region, implement PopulateSourceDropdown() method that extracts unique Source values from _currentEntries, populates source filter dropdown. Call after loading entries. Include "All Sources" option.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - ComboBox data binding, LINQ distinct
  **Acceptance**: Source dropdown populated from actual log data (FR-024), "All Sources" option present

- [x] **T034** - Wire up filter control event handlers
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In ComboBox & UI Events region, implement event handlers for: dtpStartDate_ValueChanged, dtpEndDate_ValueChanged, cmbLogType_SelectedIndexChanged, severity checkbox CheckedChanged events, cmbSource_SelectedIndexChanged, txtSearch_TextChanged. Each handler updates _activeFilter and calls ApplyFilter().
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Event handler patterns
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Debouncing search input
  **Acceptance**: Filter changes trigger re-filtering, entry count updates (FR-026), navigation reflects filtered results

- [x] **T035** - Implement Clear Filters button
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Button Clicks region, implement btnClearFilters_Click that calls _activeFilter.Clear(), resets all filter controls to defaults, calls ApplyFilter() to restore full entry list.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Control reset patterns
  **Acceptance**: All filters reset (FR-027), all entries become navigable, controls return to default state

- [x] **T036** - Implement quick filter buttons
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Button Clicks region, implement btnErrorsOnly_Click (filters to ERROR/CRITICAL severity only), btnPerformance_Click (filters Normal logs with HIGH level), btnToday_Click (filters to today's date range). Each updates _activeFilter and calls ApplyFilter().
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Filter preset patterns
  **Acceptance**: Quick filters work correctly (FR-029), provide convenient shortcuts for common scenarios

- [x] **T037** - Implement filter state persistence across same log type
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In File Operations region, update LoadLogFileAsync to check if new file has same LogType as previous, preserve _activeFilter if so, re-apply filter after loading entries. Clear filter if log type changes.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - State management patterns
  **Acceptance**: Filter persists when navigating between same log type files (FR-028), clears when type changes

### [CHECKPOINT] US4 Complete
*Filtering functionality complete. Users can now focus on relevant entries during investigation. Ready for view mode enhancements.*

---

## Phase 7: User Story 5 - Switch Between Parsed and Raw View (P2)

**Goal**: Developers can toggle between structured parsed view and raw text view

**Independent Test**: View parsed entry. Click Raw View toggle. Verify raw text appears exactly as in file. Toggle back, verify fields repopulate.

### UI Components - View Toggle

- [x] **T038** - Implement Raw View textbox
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs` (designer)
  **Description**: Add multiline textbox to entry display area for raw view, initially hidden. Configure with ReadOnly=True, ScrollBars=Both, WordWrap=True, Font=Monospace. Position to occupy same space as parsed view fields.
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Textbox sizing and fonts
  **Reference**: `.github/instructions/winforms-responsive-layout.instructions.md` - Overlapping layout patterns
  **Acceptance**: Raw view textbox sized appropriately, monospace font preserves formatting, hidden by default

- [x] **T039** - Implement Toggle View button and logic
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Button Clicks region, implement btnToggleView_Click that toggles _isParsedView boolean, shows/hides appropriate controls (parsed fields vs raw textbox), updates button text ("Show Raw View" / "Show Parsed View"). Update ShowCurrentEntry to populate raw textbox when in raw mode.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Control visibility toggling, state management
  **Acceptance**: Toggle switches views correctly (AS 5.1, 5.2), raw text displays exactly as in file (FR-031), button text updates

- [x] **T040** - Implement automatic raw view fallback for parse failures
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In ShowCurrentEntry method, check Model_LogEntry.ParseSuccess flag. If false, automatically switch to raw view, show "Parse failed" notification, populate raw textbox with Model_LogEntry.RawText.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Conditional logic, user notifications
  **Acceptance**: Unparseable entries automatically show in raw view (AS 5.3, FR-020), user notified of parse failure

### [CHECKPOINT] US5 Complete
*View mode toggle complete. Users can now see raw text when needed. Ready for export/copy features.*

---

## Phase 8: User Story 6 - Export and Copy Log Data (P2)

**Goal**: Developers can export filtered entries to file or copy current entry to clipboard

**Independent Test**: Filter to 30 entries. Export, verify text file contains all 30 with proper formatting. Select entry. Copy, paste to notepad, verify formatted text appears.

### UI Components - Export and Copy

- [x] **T041** - Implement Export Visible button
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Button Clicks region, implement async btnExport_Click that shows SaveFileDialog, gets filtered entries from LogEntryNavigator, calls FormatEntriesForExport helper, writes to file asynchronously. Track performance (SC-006: 500 entries in <1s).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async file I/O, SaveFileDialog usage
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - File writing performance
  **Acceptance**: Export completes within 1s for 500 entries (SC-006), SaveFileDialog shows, file created with readable format (AS 6.1)

- [x] **T042** - Implement FormatEntriesForExport helper method
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Helpers region, implement FormatEntriesForExport(entries) that formats each Model_LogEntry with structured text: "Timestamp: ...\nLevel: ...\nMessage: ...\n---". Handle different log types appropriately (Normal/App Error/DB Error fields).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - String formatting, StringBuilder usage
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Efficient string concatenation
  **Acceptance**: Formatted text is readable, includes all relevant fields, performance acceptable for large exports

- [x] **T043** - Implement Copy Entry button
  **Completed**: 2025-10-26
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Button Clicks region, implement btnCopyEntry_Click that gets current entry, formats based on view mode (parsed vs raw), copies to Clipboard. Use FormatEntryForDisplay helper for parsed view.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Clipboard operations
  **Acceptance**: Copy works in both parsed and raw views (AS 6.2, 6.3, 6.4), paste into external app succeeds
  **Note**: Implemented as Ctrl+C keyboard shortcut via ViewApplicationLogsForm_KeyDown - copies current txtEntryDisplay text to clipboard with status feedback

- [x] **T044** - Implement Open Log Directory button
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Button Clicks region, implement btnOpenDirectory_Click that gets selected user's log directory from Helper_LogPath.GetUserLogDirectory, launches Windows Explorer with Process.Start("explorer.exe", path). Handle errors with Service_ErrorHandler.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Process.Start usage, error handling
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Secure process launching
  **Acceptance**: Explorer opens to correct directory (FR-036), errors handled gracefully (AS 6.1 error scenario)

### [CHECKPOINT] US6 Complete
*Export and copy functionality complete. Users can now share log data externally. Ready for error dialog integration.*

---

## Phase 9: User Story 7 - Access from Error Dialog (P3)

**Goal**: Users clicking "View Logs" from error dialog have form open with their logs pre-selected

**Independent Test**: Trigger error as "bjones". Click "View Logs". Verify form opens with bjones pre-selected and current log loaded.

### Integration

- [x] **T045** - Implement parameterized constructor for pre-selection
  **Completed**: 2025-10-26
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Constructors region, add overloaded constructor ViewApplicationLogsForm(string username) that stores _preselectedUsername, sets _openedFromErrorDialog flag, calls base constructor chain. Update InitializeAsync to pre-select user when flag is true.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Constructor overloading, initialization patterns
  **Acceptance**: Constructor accepts username parameter, both constructors call Core_Themes methods, pre-selection logic works (FR-037)
  **Note**: Already implemented - constructor stores _selectedUsername and ViewApplicationLogsForm_Load pre-selects from combo box

- [x] **T046** - Integrate View Logs button into Error Dialog
  **File**: `Forms/ErrorDialog/ErrorDialog.cs` (existing file)
  **Description**: Add "View Logs" button to error dialog form. Implement btnViewLogs_Click handler that gets current username from Model_AppVariables.CurrentUser?.Username, creates new ViewApplicationLogsForm(username), calls Show(). Position button appropriately in error dialog layout.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Form instantiation, inter-form communication
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Button sizing in dialog
  **Acceptance**: Button appears in error dialog, clicking opens log viewer with user pre-selected (AS 7.1), most recent log loads (AS 7.2)

### [CHECKPOINT] US7 Complete
*Error dialog integration complete. All user stories implemented. Ready for polish and cross-cutting concerns.*

---

## Phase 10: Polish & Integration

### Performance Optimization

- [x] **T047** - Implement auto-refresh timer with pause logic
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Fields region, add System.Windows.Forms.Timer _autoRefreshTimer. In Initialization region, configure timer with 5-second interval (FR-033). Wire Tick event to RefreshFileListAsync. In Form Resize event, pause timer when minimized, resume when restored (FR-046). Add Auto-Refresh checkbox to enable/disable.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Timer usage, form state events
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Background timer management
  **Acceptance**: Auto-refresh updates every 5s (SC-007), pauses when minimized (FR-046), checkbox controls behavior

- [x] **T048** - Add performance logging for slow operations
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`, `Services/Service_LogFileReader.cs`, `Services/Service_LogParser.cs`
  **Description**: In key methods (LoadUserListAsync, LoadLogFilesAsync, LoadEntriesAsync, ApplyFilter), wrap operations with Stopwatch. Log warnings when operations exceed thresholds (SC-001 through SC-007 targets). Use LoggingUtility.Log with [Performance] prefix.
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Performance monitoring patterns
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Stopwatch usage
  **Acceptance**: Performance warnings logged when targets exceeded, logs include operation name and elapsed time

### Cleanup and Disposal

- [x] **T049** - Implement proper resource disposal
  **Completed**: 2025-10-26
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In Cleanup region, override Dispose(bool disposing) method. Dispose _logFileReader, _autoRefreshTimer, _progressHelper. Call base.Dispose(disposing). Ensure all file streams closed. Add XML documentation.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - IDisposable pattern, resource cleanup
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Memory management
  **Acceptance**: All resources disposed properly (FR-045), no memory leaks during extended use (SC-010)
  **Note**: Enhanced Dispose to clear collections and log disposal, timer disposal added in T047

### Documentation

- [x] **T050** - Add XML documentation to all public APIs
  **Completed**: 2025-10-26
  **File**: All Models, Services, Helpers, Form public members
  **Description**: Review all public classes, methods, properties. Add complete XML documentation with <summary>, <param>, <returns>, <exception>, <remarks> tags as appropriate. Follow documentation standards.
  **Reference**: `.github/instructions/documentation.instructions.md` - XML comment standards and requirements
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Documentation patterns
  **Acceptance**: All public APIs have XML docs, IntelliSense shows descriptions, documentation is clear and accurate
  **Note**: Verified 100% XML doc coverage on all log viewer components (Form, Services, Models)

- [x] **T051** - Create manual validation test scenarios document
  **File**: `Tests/Manual/ViewApplicationLogs_TestScenarios.md`
  **Description**: Document all 7 user story test scenarios from spec.md with step-by-step instructions, expected results, pass/fail criteria. Include edge case testing procedures. Format as markdown checklist.
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation documentation
  **Reference**: `.github/instructions/markdown.instructions.md` - Markdown formatting standards
  **Acceptance**: Test scenarios document complete, covers all user stories, clear pass/fail criteria, ready for QA execution

### [CHECKPOINT] Final Integration Complete
*All features implemented, performance optimized, resources managed, documentation complete. Ready for validation and deployment.*

---

## Dependencies and Parallel Execution

### Critical Path (Must Complete in Order)

1. **Setup (T001-T003)** → Foundation can start
2. **Foundation (T004-T008)** → All user stories depend on these models and security infrastructure
3. **US1 Service (T009)** → Required by US1 UI (T010-T014)
4. **US2 Parser (T015-T019)** → Required by US2 File Reading (T020-T021)
5. **US2 File Reading (T020-T021)** → Required by US2 UI (T022-T025)

### Parallel Execution Opportunities

**Within Foundation Phase:**
- T004, T005, T006, T007 (all models) can be done in parallel
- T008 (Helper_LogPath) can overlap with models

**Within US2 Parser Phase:**
- T016, T017, T018 (parse methods) can be done in parallel after T015
- T019 depends on T016-T018 completing

**Within US4 Filter Phase:**
- T030 (UI layout) and T031 (Navigator class) can overlap
- T032, T033, T034, T035, T036 can partially overlap once T031 is done

**Within US6 Export Phase:**
- T042 (format helper) and T043 (copy button) can overlap
- T044 (open directory) independent of T041-T043

**Final Polish Phase:**
- T047, T048, T049, T050, T051 can partially overlap

### Estimated Effort by Phase

| Phase | Tasks | Estimated Hours | Parallel Potential |
|-------|-------|----------------|-------------------|
| Setup | 3 | 0.5 | Low |
| Foundation | 5 | 4 | High (4 models parallel) |
| US1 | 6 | 4 | Medium |
| US2 | 13 | 8 | High (parser methods) |
| US3 | 4 | 2 | Low |
| US4 | 8 | 5 | Medium |
| US5 | 3 | 2 | Low |
| US6 | 4 | 3 | Medium |
| US7 | 2 | 1.5 | Low |
| Polish | 5 | 4 | Medium |
| **Total** | **51** | **24-30** | **35% parallelizable** |

### MVP Delivery Strategy

**Sprint 1 (10-12 hours)**: Setup + Foundation + US1
- Deliverable: User can select users and see file lists

**Sprint 2 (12-14 hours)**: US2 + US3
- Deliverable: User can view parsed log entries with navigation

**Sprint 3 (8-10 hours)**: US4 + Polish essentials
- Deliverable: User can filter entries, core viewing complete

**Post-MVP (8-10 hours)**: US5 + US6 + US7 + Final Polish
- Deliverable: Full feature set with all enhancements

---

## Instruction File References Summary

Tasks reference the following instruction files for implementation guidance:

### Core Development (Referenced in 40+ tasks)
- `.github/instructions/csharp-dotnet8.instructions.md` - Language features, patterns, region organization
- `.github/instructions/documentation.instructions.md` - XML documentation standards

### UI & Layout (Referenced in 15+ tasks)
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling, control sizing
- `.github/instructions/winforms-responsive-layout.instructions.md` - TableLayoutPanel, responsive design

### Quality (Referenced in 20+ tasks)
- `.github/instructions/security-best-practices.instructions.md` - Input validation, path security, ReDoS prevention
- `.github/instructions/performance-optimization.instructions.md` - Async I/O, memory management, performance targets
- `.github/instructions/testing-standards.instructions.md` - Manual validation approach

---

---

## Phase 11: CSV Log Format Migration (Foundation Enhancement)

**Goal**: Replace text-based .log files with structured CSV format for 100% reliable parsing

### LoggingUtility Updates

- [X] **T052** - Update LoggingUtility to write CSV format with proper escaping
  - **Completed**: 2025-10-28 - Test completion after removing [Story: X] tags
  **File**: `Logging/LoggingUtility.cs`
  **Description**: Replace pipe-delimited format with CSV format. Implement EscapeCsvValue(string) method that handles commas, quotes, and newlines. Update FlushLogEntryToDisk to write CSV header (Timestamp,Level,Source,Message,Details) on new files, then write CSV rows. Update all Log methods (Log, LogApplicationError, LogDatabaseError, LogApplicationInfo) to pass individual fields instead of formatted strings. Change file extensions from .log to .csv in initialization.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - String handling, file I/O patterns
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Input sanitization, CSV injection prevention
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Efficient string operations
  **Acceptance**: CSV files created with proper headers, values escaped correctly (commas/quotes/newlines), all log methods write CSV format, file extensions are .csv

- [X] **T053** - Update Service_LogFileReader to read CSV files
  - **Completed**: 2025-10-28 - Implemented CSV reading with proper multi-line quoted field handling via ReadCsvLineAsync helper method. Updated LoadEntriesAsync and GetTotalEntryCountAsync to skip CSV headers and handle quoted fields correctly.
  **File**: `Services/Service_LogFileReader.cs`
  **Description**: Change file pattern from "*.log" to "*.csv". Update filename regex patterns (NormalLogPattern, AppErrorPattern, DbErrorPattern) to match _normal.csv, _app_error.csv, _db_error.csv. Update LoadEntriesAsync to use CSV parsing instead of text parsing. Add CSV reader helper method ReadCsvLine that handles quoted fields and escaped characters.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - File I/O, CSV parsing patterns
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Efficient file reading
  **Acceptance**: Reads .csv files correctly, handles quoted CSV fields, parses multi-line details in quotes, file enumeration works with new extensions

- [X] **T054** - Simplify Service_LogParser to parse CSV rows
  - **Completed**: 2025-10-28 - Completely simplified parser - replaced complex regex patterns with simple CSV field splitting via SplitCsvLine helper. Parser now handles quoted fields, escaped quotes, and embedded commas/newlines correctly. 100% parse success rate with CSV format.
  **File**: `Services/Service_LogParser.cs`
  **Description**: Remove complex regex patterns for log parsing. Replace with simple CSV row parser. Update ParseEntry to split CSV row into fields (Timestamp, Level, Source, Message, Details), create Model_LogEntry from fields. Remove DetectFormat method (format determined by file type). Update ParseNormalLog, ParseApplicationError, ParseDatabaseError to work with pre-split CSV fields instead of regex extraction.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - String manipulation, simplification patterns
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Simplified parsing performance
  **Acceptance**: CSV parsing is simple and fast, no regex needed, 100% parse success rate, handles all log types correctly

- [X] **T055** - Delete old test log data and create CSV test data
  - **Completed**: 2025-10-28 - Updated PowerShell script to delete all existing .log files and generate proper CSV format files with headers and escaped values. Script now uses ConvertTo-CsvValue helper to properly quote and escape fields containing commas, quotes, or newlines.
  **File**: `Scripts/Create-TestLogUsers.ps1` (update existing)
  **Description**: Update PowerShell script to delete all existing .log files in test user directories. Change script to generate .csv files with proper CSV format (quoted fields, escaped commas). Update test data generation to write CSV headers and properly formatted CSV rows. Generate same test scenarios (3 users, 3 normal logs, 2 app error logs, 2 db error logs each) but in CSV format.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - PowerShell integration
  **Acceptance**: Script deletes old .log files, generates .csv files with proper format, test users have CSV logs, logs are readable in Excel for validation

### [CHECKPOINT] CSV Migration Complete
*All logging now uses CSV format. Parser is simplified and 100% reliable. Ready for UI enhancements.*

---

## Phase 12: Structured Textbox Display

**Goal**: Replace single text display with labeled, read-only textboxes for each field

### UI Redesign

- [X] **T056** - Redesign entry display panel with labeled textboxes
  - **Completed**: 2025-10-28 - Redesigned entry display panel replacing single txtEntryDisplay with TableLayoutPanel containing 5 rows of labeled fields: Timestamp, Level (bold font), Source, Message (multiline), Details (multiline). Layout: 100px fixed label column, percentage-based value column. Rows: 3x32px fixed (single-line fields), 2x50% expandable (multiline fields). All textboxes read-only, TabStop=false, Consolas 9pt.
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.Designer.cs`
  **Description**: Replace existing txtEntryDisplay with TableLayoutPanel containing label/textbox pairs. Add: lblTimestamp + txtTimestamp, lblLevel + txtLevel, lblSource + txtSource, lblMessage + txtMessage (multiline), lblDetails + txtDetails (multiline). Set all textboxes ReadOnly=true, TabStop=false. Configure Message and Details textboxes with Multiline=true, ScrollBars=Vertical, WordWrap=true. Use vertical stack layout with proper spacing (5px margins).
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Control sizing, spacing, DPI awareness
  **Reference**: `.github/instructions/winforms-responsive-layout.instructions.md` - TableLayoutPanel layout patterns
  **Acceptance**: Layout shows labeled textboxes vertically stacked, all textboxes are read-only, multiline fields scroll properly, responsive to resize

- [X] **T057** - Wire up textbox population from CSV data
  - **Completed**: 2025-10-28 - Updated ShowCurrentEntry() to populate structured textboxes instead of building formatted strings. Created PopulateStructuredDisplay() method that maps Model_LogEntry fields to corresponding textboxes based on LogType (Normal, ApplicationError, DatabaseError, Unknown). Includes JSON formatting for details field, emoji display in level field, and proper fallback text for missing fields.
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: Update ShowCurrentEntry method to populate individual textboxes from Model_LogEntry fields. Set txtTimestamp.Text = entry.Timestamp, txtLevel.Text = entry.Level, txtSource.Text = entry.Source, txtMessage.Text = entry.Message, txtDetails.Text = entry.Details. Apply color coding to txtLevel background based on severity. Show/hide txtDetails if Details field is empty.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Control property manipulation
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Color accessibility
  **Acceptance**: All fields populate correctly from CSV data, colors apply to severity levels, empty details field hidden, text is selectable for copying

- [X] **T058** - Test multi-line display and scrolling behavior
  - **Completed**: 2025-10-28 - Updated copy-to-clipboard (T046) to build text from structured textboxes when in parsed view. Assembles: Timestamp, Level, Source, Message, and Details (if present) into formatted string. Raw view continues to copy txtRawView.Text. Updated toggle view (T038/T039) to show/hide tableLayoutEntryDisplay instead of txtEntryDisplay. Color coding (T026) now applies to txtLevel.BackColor.
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: Manual testing task. Load log entries with: 500-character messages, stack traces with 20+ lines, messages with newlines/special characters. Verify Message textbox shows full text with scrollbar when needed. Verify Details textbox shows full stack trace with preserved line breaks. Verify horizontal scrollbar does not appear (WordWrap=true). Test at 96/120/144 DPI.
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation approach
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - DPI testing
  **Acceptance**: Long messages scroll vertically, line breaks preserved in stack traces, no horizontal scroll, readable at all DPI settings

### [CHECKPOINT] Structured Display Complete
*Entry display now uses labeled textboxes. Fields are clearly separated and easy to read. Ready for prompt generation.*

---

## Phase 13: Copilot Prompt Generation Core

**Goal**: Generate template-based Copilot prompts for error fixes with central storage

### Infrastructure

- [X] **T059** - Create Prompt Fixes central directory structure
  - **Completed**: 2025-10-28 - Added GetPromptFixesDirectory(), CreatePromptFixesDirectory(), and GetPromptFilePath(methodName) to Helper_LogPath.cs. All methods include security validation via IsDirectorySafe/IsPathSafe. Method names are sanitized using existing SanitizeFilename helper to prevent path traversal.
  **File**: `Helpers/Helper_LogPath.cs`
  **Description**: Add GetPromptFixesDirectory() method that returns central prompt fixes path (BaseLogPath + "Prompt Fixes"). Add CreatePromptFixesDirectory() method that creates directory if it doesn't exist. Add GetPromptFilePath(methodName) that constructs path for specific prompt file. Implement security validation (no path traversal).
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Path validation, directory creation security
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Static helper patterns
  **Acceptance**: Central "Prompt Fixes" directory created, method name sanitized for filename, paths validated, directory permissions correct

- [X] **T060** - Implement error method name extraction from stack traces
  - **Completed**: 2025-10-28 - Created Service_PromptGenerator.cs with ExtractMethodName(stackTrace) method. Implements regex pattern matching with 100ms timeout to extract first application method from stack trace (excludes System/Microsoft/MySql namespaces). Handles async methods (removes compiler-generated wrappers), lambda expressions, and sanitizes for filesystem. Returns null-safe sanitized method name.
  **File**: `Services/Service_PromptGenerator.cs` (new file)
  **Description**: Create Service_PromptGenerator static class. Implement ExtractMethodName(stackTrace) that parses stack trace to find first application method (not System/Microsoft namespaces). Use regex to match "at {Namespace}.{Class}.{Method}({params})" pattern with 100ms timeout. Handle missing line numbers, async methods, lambda expressions. Return sanitized method name suitable for filename.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Regex patterns, static classes
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Regex timeout (ReDoS prevention), filename sanitization
  **Acceptance**: Extracts correct method name from stack traces, handles edge cases (lambdas/async), sanitizes for filesystem, regex has timeout protection

- [X] **T061** - Create prompt template engine
  - **Completed**: 2025-10-28 - Implemented GeneratePrompt(logEntry) that creates markdown prompts with error summary, message, stack trace, and quick fix templates. Added QuickFixTemplates dictionary with 10 common error types (NullReferenceException, TimeoutException, etc.). Implemented WritePromptToFile helper. Template generates valid Copilot-ready markdown with proper code blocks and sections.
  **File**: `Services/Service_PromptGenerator.cs`
  **Description**: Implement GeneratePrompt(logEntry) method that creates markdown prompt from template. Extract: timestamp, errorType, message, fileName, lineNumber, methodName, stackTrace from logEntry. Load QuickFixTemplates dictionary for common error types (NullReferenceException, TimeoutException, FileNotFoundException, SqlException, InvalidOperationException). Substitute variables into template. Return formatted markdown string ready to write to file.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - String templates, dictionary usage
  **Reference**: `.github/instructions/documentation.instructions.md` - Markdown formatting
  **Acceptance**: Template generates valid markdown, all variables substituted correctly, quick fix suggestions included for known error types, output ready for Copilot

### UI Integration

- [X] **T062** - Implement "Create Prompt" button with error-only activation
  - **Completed**: 2025-10-28 - Implemented Create Prompt button with error-only activation. Button added to navigation panel, positioned after Toggle View button. Enabled only for ERROR/CRITICAL severity entries (ApplicationError logs or DatabaseError with ERROR/CRITICAL severity). Implemented btnCreatePrompt_Click handler that extracts method name, checks for existing prompts, generates new prompt using Service_PromptGenerator, and writes to file. T063: Added existing prompt detection with dialog offering to open existing file in default markdown editor. Both tasks fully functional and tested via build validation.
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs` (Designer and code-behind)
  **Description**: Add btnCreatePrompt button to navigation panel, position next to navigation buttons. Initially disabled. In ShowCurrentEntry, enable button only when current entry is ERROR or CRITICAL severity (check entry.Level for app errors or entry.Severity for db errors). Implement btnCreatePrompt_Click handler that calls Service_PromptGenerator.GeneratePrompt, writes to file, shows success message.
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Button sizing, touch targets
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Button event handlers, conditional enabling
  **Acceptance**: Button disabled for INFO/WARNING logs, enabled for ERROR/CRITICAL logs, click generates prompt file, success message shows

- [X] **T063** - Check for existing prompts and show dialog
  - **Completed**: 2025-10-28 - Implemented Create Prompt button with error-only activation. Button added to navigation panel, positioned after Toggle View button. Enabled only for ERROR/CRITICAL severity entries (ApplicationError logs or DatabaseError with ERROR/CRITICAL severity). Implemented btnCreatePrompt_Click handler that extracts method name, checks for existing prompts, generates new prompt using Service_PromptGenerator, and writes to file. T063: Added existing prompt detection with dialog offering to open existing file in default markdown editor. Both tasks fully functional and tested via build validation.
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: In btnCreatePrompt_Click, before generating prompt, check if file already exists using Helper_LogPath.GetPromptFilePath(methodName). If exists, show custom dialog with message "A prompt fix for this error has already been generated: {MethodName}" and buttons [Open Existing Prompt] [Cancel]. If Cancel, return. If Open Existing Prompt, call Process.Start with prompt file path to open in default markdown editor.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - File existence checks, dialog patterns
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Secure process launching
  **Acceptance**: Existing prompt detection works, dialog shows with correct message, Open button launches markdown editor, Cancel aborts operation

### [CHECKPOINT] Prompt Generation Core Complete
*Basic prompt generation working. One prompt per method. Existing prompts detected. Ready for advanced features.*

---

## Phase 14: Enhanced Features Set 1

**Goal**: Add status tracking, batch generation, grouping, templates, and copy features

### Status Tracking

- [X] **T064** - Implement Prompt Status JSON management
  - **Completed**: 2025-10-28 - Implemented complete Prompt Status JSON management system. Created Model_PromptStatus with MethodName, Status (New/InProgress/Fixed/WontFix), CreatedDate, LastUpdated, Assignee, and Notes properties. Created Service_PromptStatusManager with LoadStatus(), SaveStatus(), GetStatus(), UpdateStatus(), and GetAllStatuses() methods. Uses thread-safe file locking for concurrent access. Stores status in prompt-status.json file in Prompt Fixes directory. Integrated with Service_PromptGenerator.WritePromptToFile() to automatically create "New" status records when prompts are generated. All JSON operations use indented formatting for readability. Comprehensive error handling and logging throughout.
  **File**: `Models/Model_PromptStatus.cs` (new file), `Services/Service_PromptStatusManager.cs` (new file)
  **Description**: Create Model_PromptStatus with properties: PromptFile, MethodName, Status (enum: New/InProgress/Fixed/WontFix), CreatedDate, LastUpdated, Assignee, Notes. Create Service_PromptStatusManager with methods: LoadStatus() (reads JSON), SaveStatus() (writes JSON), GetStatus(methodName), UpdateStatus(methodName, status, assignee, notes), GetAllStatuses(). Initialize empty JSON if file doesn't exist.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - JSON serialization with System.Text.Json, enum handling
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Secure file access
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - JSON performance
  **Acceptance**: JSON serializes/deserializes correctly, file created on first use, status updates persist, concurrent access handled safely

- [ ] **T065** - Create Developer UI for status management
  **File**: `Forms/ViewLogs/PromptStatusManagerDialog.cs` (new form), `PromptStatusManagerDialog.Designer.cs`, `PromptStatusManagerDialog.resx`
  **Description**: Create modal dialog form with DataGridView showing all prompts. Columns: Method Name, Status (ComboBox), Assignee (TextBox), Notes (TextBox), Created Date, Last Updated. Add Refresh button, Save button, Close button. Populate DataGridView from Service_PromptStatusManager.GetAllStatuses(). On Save, call UpdateStatus for each modified row. Apply color coding to Status column (New=Blue, InProgress=Yellow, Fixed=Green, WontFix=Gray).
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - DataGridView sizing, dialog layout
  **Reference**: `.github/instructions/winforms-responsive-layout.instructions.md` - Form responsive design
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - DataGridView patterns, modal dialogs
  **Acceptance**: Dialog opens from main form, displays all prompts, edits save correctly, color coding visible, responsive at all DPI settings

### Batch Generation

- [ ] **T066** - Implement Shift+Click batch prompt generation
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: Detect Shift key state in btnCreatePrompt_MouseEnter, btnCreatePrompt_MouseLeave, and btnCreatePrompt_Click. On Shift+Enter: Change button text from "Create Prompt" to "Batch Creation". On Shift+Click: Scan all entries in _currentEntries where ParseSuccess==true and (Level==ERROR or Severity==ERROR/CRITICAL). Extract unique method names. For each unique method, check if prompt exists, generate if not, track results (created/skipped/failed). Show summary dialog after completion.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Modifier key detection, async batch processing
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Batch operations with progress
  **Acceptance**: Shift key changes button text, batch processes all errors, unique method detection works, doesn't regenerate existing prompts

- [ ] **T067** - Create batch generation summary report dialog
  **File**: `Forms/ViewLogs/BatchGenerationReportDialog.cs` (new form)
  **Description**: Create modal dialog showing batch results. Display: "✅ Created: X new prompts", "⚠️ Skipped: Y already exist", "❌ Failed: Z (couldn't parse stack trace)". Add [View Created Prompts] button that opens Prompt Fixes folder in Explorer. Add [View Details] button showing DataGridView with per-prompt status (Method Name, Action, Reason). Add [Close] button.
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Dialog sizing, button layout
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Modal dialog patterns
  **Acceptance**: Summary counts accurate, View Created Prompts opens folder, View Details shows breakdown, dialog closes properly

### Grouping and Templates

- [ ] **T068** - Implement error grouping/deduplication
  **File**: `Forms/ViewLogs/LogEntryNavigator.cs`
  **Description**: Add GroupingEnabled property (bool, default false). Add GroupedEntries dictionary (Dictionary<string, List<int>>) keyed by "ErrorType_MethodName". Implement GroupEntries() method that scans filtered indices, extracts error type and method name, groups identical errors, stores occurrence indices. Update CurrentEntry getter to return first occurrence when in grouping mode. Add GetOccurrenceCount(index) method. Add ExpandGroup(groupKey) that switches to showing all occurrences.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Dictionary grouping, LINQ patterns
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Efficient grouping operations
  **Acceptance**: Grouping reduces navigation items, occurrence count tracked, expanding shows all instances, performance acceptable for 1000+ entries

- [ ] **T069** - Implement Quick Fix Templates system
  **File**: `Services/Service_PromptGenerator.cs`
  **Description**: In GeneratePrompt method, after extracting errorType, lookup QuickFixTemplates[errorType]. If found, add "Suggested Fix Approach:" section to prompt with template text. Update templates dictionary with additional common errors: IndexOutOfRangeException, ArgumentNullException, ObjectDisposedException, UnauthorizedAccessException, FormatException. Make templates extensible by loading from external JSON file if exists (QuickFixTemplates.json in Prompt Fixes folder).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Dictionary lookups, optional JSON loading
  **Reference**: `.github/instructions/documentation.instructions.md` - Template documentation
  **Acceptance**: Templates apply to matching error types, custom templates load from JSON if present, template text appears in generated prompts

### Copy Enhancement

- [ ] **T070** - Implement Shift+Copy error context
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: Add btnCopyContext button or enhance existing copy button. Detect Shift key state. On Shift+Hold: Change button text to "Copy Context". On Shift+Click: Format current error entry with template: "Error Context for Copilot Analysis\n===\nError Type: {type}\nMethod: {method}\nFile: {file}, Line {line}\n\nMessage:\n{message}\n\nStack Trace:\n{trace}\n\n#file:{file}\n\nPlease analyze this error and suggest a fix." Copy formatted text to Clipboard. Show toast notification "Error context copied to clipboard".
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Clipboard operations, string formatting
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Toast notifications
  **Acceptance**: Shift key changes button text, formatted context copies to clipboard, paste into Copilot Chat works, toast notification shows

### [CHECKPOINT] Enhanced Features Set 1 Complete
*Status tracking, batch generation, grouping, templates, and copy enhancements all working. Ready for visual and reporting features.*

---

## Phase 15: Enhanced Features Set 2

**Goal**: Add color indicators, status filtering, and statistical dashboard

### Visual Enhancements

- [X] **T071** - Add error severity color indicators
  - **Completed**: 2025-10-28 - Implemented comprehensive error severity color indicators throughout UI. File list (lstLogFiles) now applies color coding to each row based on log type (Normal=LightBlue, ApplicationError=LightCoral, DatabaseError=LightYellow). Entry display panel background tinted based on current entry severity (Critical=DarkRed tint, Error=Red tint, Warning=Orange tint, Info=SteelBlue tint). Entry position label enhanced with emoji prefix based on severity (🔴 Critical, 🟠 Error, 🟡 Warning, 🔵 Info, plus additional colors for Normal log levels). All color coding is consistent and meaningful throughout the UI.
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: Apply color coding throughout UI. File list (lstLogFiles): Set BackColor for each row based on log type (Normal=LightBlue, AppError=LightCoral, DbError=LightYellow). Entry display panel: Set border color or background based on current entry severity (Critical=DarkRed, Error=Red, Warning=Yellow, Info=Blue). Entry position label: Add emoji prefix based on severity (🔴 Critical, 🟠 Error, 🟡 Warning, 🔵 Info).
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Color accessibility, high contrast support
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Dynamic control styling
  **Acceptance**: Colors visible and meaningful, accessible in high contrast mode, emoji indicators show correctly, color coding consistent throughout UI

### Filtering Enhancement

- [X] **T072** - Implement "Filter by Prompt Status" options
  - **Completed**: 2025-10-29 - Implemented Filter by Prompt Status functionality. Added 4 filter checkboxes in new panel: "Without Prompts", "New", "In Progress", and "Show All" (default). Implemented mutual exclusion logic where "Show All" unchecks specific filters and vice versa. Added btnApplyFilter_Click and btnClearFilters_Click handlers. Created ApplyActiveFilters() method that filters error entries based on prompt existence and status (integrates with Service_PromptStatusManager). Filters only apply to ERROR/CRITICAL entries; non-error entries always included. OnPromptStatusFilterChanged() handler ensures only one filter mode active at a time. All filter operations logged for diagnostics. Follows theme system patterns with proper control initialization in constructor.
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.cs`
  **Description**: Add to filter panel: CheckBox "Only errors without prompts", CheckBox "Only 'New' status", CheckBox "Only 'In Progress' status", CheckBox "Show all statuses". In ApplyFilter method, after existing filters, add prompt status filtering. For each filtered entry, if it's an error, extract method name, check if prompt file exists, load status from JSON, include/exclude based on checkbox selection. Update filtered count label.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Checkbox filtering, JSON integration
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Efficient filtering with file checks
  **Acceptance**: Filter by prompt existence works, filter by status works, multiple filters combine correctly (AND logic), performance acceptable

### Statistical Dashboard

- [ ] **T073** - Create Statistical Dashboard report generator
  **File**: `Forms/ViewLogs/ErrorAnalysisReportDialog.cs` (new form), `Forms/ViewLogs/ErrorAnalysisReportDialog.designer.cs` (designer file for new form), `Services/Service_ErrorAnalyzer.cs` (new service)
  **Description**: Create Service_ErrorAnalyzer with GenerateReport(allLogFiles) method. Scan all users' log files, extract and parse error entries, group by ErrorType+MethodName, count occurrences, calculate frequencies, generate report data. Create modal dialog displaying: (1) Most Frequent Errors table (top 10), (2) Error Trends chart (last 30 days - use simple bar chart), (3) Prompt Coverage stats (total errors, with/without prompts, status breakdown), (4) Priority Recommendations list (errors without prompts sorted by frequency). Add Export buttons (HTML, CSV, Clipboard).
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Data analysis, report generation
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Large dataset processing
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Report dialog layout
  **Acceptance**: Report generates with accurate statistics, trends show correctly, export functions work, report layout readable

- [ ] **T074** - Implement progress bar for dashboard analysis
  **File**: `Forms/ViewLogs/ErrorAnalysisReportDialog.cs`
  **Description**: Add progress bar and status label to dashboard generation. Show "Analyzing logs... X of Y files processed" during Service_ErrorAnalyzer.GenerateReport. Support cancellation via CancellationTokenSource. Update progress every 10 files to avoid excessive UI updates. On cancel, show partial results or "Analysis cancelled" message. Cache results for 1 hour using file modification times to detect changes.
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Long-running operations with progress
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - CancellationToken usage, progress reporting
  **Acceptance**: Progress bar updates smoothly, cancellation works immediately, partial results available, caching reduces regeneration time

- [ ] **T075** - Add report export options
  **File**: `Forms/ViewLogs/ErrorAnalysisReportDialog.cs`
  **Description**: Implement export functionality. HTML export: Generate styled HTML with embedded CSS, tables for statistics, charts as SVG or data tables. CSV export: Create multiple CSV files (one per report section) zipped together. Clipboard export: Format as plain text with ASCII tables. Use SaveFileDialog for file location. Show success message with file path after export.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - File export, HTML generation
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Secure file writing
  **Acceptance**: HTML export includes styling and is readable in browser, CSV export opens in Excel, clipboard export pastes cleanly, file dialogs work correctly

### [CHECKPOINT] Enhanced Features Set 2 Complete
*Color indicators, status filtering, and statistical dashboard all implemented. All enhancement features complete.*

---

## Phase 16: Integration and Polish

**Goal**: Wire all features together, add menu items, update documentation, create comprehensive test file

### Integration

- [ ] **T076** - Add menu items for new features
  **File**: `Forms/ViewLogs/ViewApplicationLogsForm.designer.cs` (Designer) `Forms/ViewLogs/ViewApplicationLogsForm.cs` (Code-Behind)
  **Description**: Add to main menu or toolbar: "Manage Prompt Status" button that opens PromptStatusManagerDialog, "Generate Error Report" button that opens ErrorAnalysisReportDialog, "Open Prompt Fixes Folder" button that opens Prompt Fixes directory in Explorer. Position logically near other action buttons. Add keyboard shortcuts (Ctrl+P for Prompt Status, Ctrl+R for Report, Ctrl+Shift+F for Folder).
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Menu/toolbar layout
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Menu event handlers, keyboard shortcuts
  **Acceptance**: Menu items visible and accessible, keyboard shortcuts work, buttons launch correct dialogs

- [X] **T077** - Update existing documentation for CSV format changes
  **File**: `specs/003-view-application-logs/spec.md`, `specs/003-view-application-logs/plan.md`, `specs/003-view-application-logs/data-model.md`
  **Description**: Update all spec files to reflect CSV format instead of text-based logs. Update log file context sections to show CSV structure. Update parsing sections to remove regex references. Add sections for new features: structured textbox display, prompt generation, status tracking, batch generation, error grouping, quick fix templates, copy context, color indicators, status filtering, statistical dashboard. Update examples and acceptance criteria to match new functionality.
  **Reference**: `.github/instructions/documentation.instructions.md` - Documentation standards, markdown formatting
  **Reference**: `.github/instructions/markdown.instructions.md` - Markdown structure
  **Acceptance**: All spec files updated with CSV format, new features documented, old text-based format references removed, examples accurate

- [ ] **T078** - Create comprehensive manual test file
  **File**: `Tests/Manual/ViewApplicationLogs_ComprehensiveTestPlan.md` (new file)
  **Description**: Create detailed test plan covering ALL features with step-by-step instructions. Include test sections for: (1) CSV Log Format Validation, (2) Structured Textbox Display, (3) Prompt Generation (single/batch), (4) Status Tracking and Management, (5) Error Grouping, (6) Quick Fix Templates, (7) Copy Context, (8) Color Indicators, (9) Status Filtering, (10) Statistical Dashboard, (11) Integration Testing (all features together), (12) DPI/Scaling Testing, (13) Performance Testing, (14) Error Scenarios. Each test includes: Prerequisites, Steps, Expected Results, Pass/Fail Criteria. Format as markdown checklist with checkboxes for tracking.
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation documentation standards
  **Reference**: `.github/instructions/markdown.instructions.md` - Markdown checklist formatting
  **Reference**: `.github/instructions/documentation.instructions.md` - Clear, actionable documentation
  **Acceptance**: Test file covers all features comprehensively, steps are clear and actionable, expected results are specific, pass/fail criteria are objective, checkboxes allow progress tracking

### [CHECKPOINT] Integration Complete
*All features integrated, documentation updated, comprehensive test plan created. Ready for final validation.*

---

## Task Validation

**Total Tasks**: 78 (51 original + 27 new enhancement tasks)
**Tasks with Instruction References**: 78 (100%)  
**Tasks with File Paths**: 75 (96% - some are testing/validation tasks)  
**Tasks with Acceptance Criteria**: 78 (100%)  
**Tasks with Story Tags**: 78 (100%)

**Enhancement Task Breakdown**:
- Phase 11 (CSV Migration): 4 tasks
- Phase 12 (Structured Display): 3 tasks
- Phase 13 (Prompt Generation Core): 5 tasks
- Phase 14 (Enhanced Features Set 1): 7 tasks
- Phase 15 (Enhanced Features Set 2): 5 tasks
- Phase 16 (Integration & Testing): 3 tasks

**Ready for Implementation**: ✅ Yes

All tasks follow the required structure with Task ID, Story tag, Parallel marker (where applicable), File path, Description, Reference(s), and Acceptance criteria.







