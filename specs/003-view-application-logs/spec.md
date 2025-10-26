# Feature Specification: View Application Logs with User Selection

**Feature Branch**: `003-view-application-logs`  
**Created**: 2025-10-25  
**Last Updated**: 2025-10-26  
**Status**: Active Development  
**Input**: Create log viewer window with user selection, log type filtering (Normal/Application Error/Database Error), file browsing, parsed log entry display, and filtering capabilities for accessing logs from network storage

**Log File Context**:
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
4. **Given** logs are stored at network path with username subdirectories, **When** user is selected, **Then** system retrieves logs from correct network path
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

### Edge Cases

- During Planning Phase if the agent thinks of any new clarifcation questions first reference spec-old.md in this feature folder to see if the answer is there, otherwise stop and ask.
- What happens when selected user has no log files? (Display message: "No log files found for selected user")
- How does system handle log files > 100MB? (Use paging/windowing, load entries in chunks of 1000)
- What if log file is locked by another process? (Open with FileShare.ReadWrite flag, show warning if write-locked)
- How does system handle malformed log entries that don't match any known format? (Fall back to raw view, log parse failure)
- What if network path becomes unavailable mid-session? (Display connection error, offer retry button)
- How does system handle emoji characters on systems without emoji font support? (Detect font capability, show text fallback like "[SUCCESS]")
- What if user deletes log file while viewing it? (Detect file deletion on navigation, show file no longer exists message)
- How does filtering behave when log file contains mix of parseable and unparseable entries? (Show both in results, unparseable entries display in raw format)
- What if two log entries have identical timestamps? (Preserve file order, use line number as secondary sort key)
- How does auto-refresh behave if file list grows to 100+ files? (Maintain performance with lazy loading, virtual scrolling in file list)

## Requirements *(mandatory)*

<!--
  ACTION REQUIRED: The content in this section represents placeholders.
  Fill them out with the right functional requirements.
-->

### Functional Requirements

#### Core Log File Access
- **FR-001**: System MUST provide dropdown/combobox to select any user who has generated logs
- **FR-002**: User dropdown MUST include "All Users" option for system-wide logs
- **FR-003**: System MUST retrieve user list from network log storage directory structure automatically
- **FR-004**: System MUST construct log path by combining network base path with selected username
- **FR-005**: System MUST display list of available log files for selected user, grouped by log type
- **FR-006**: System MUST recognize three log file types by filename suffix: `*_normal.log`, `*_app_error.log`, `*_db_error.log`
- **FR-007**: File list MUST show filename, log type badge/icon, date/time, and file size for each log file
- **FR-008**: File list MUST be sorted by date descending (newest first) within each log type group
- **FR-009**: System MUST provide log type filter dropdown (All Types/Normal/Application Error/Database Error)
- **FR-010**: Clicking log file MUST load contents and parse using appropriate format for that log type

#### Log Parsing and Display
- **FR-011**: System MUST parse Normal log entries into fields: Timestamp, Level (LOW/MEDIUM/HIGH/DATA), Emoji, Source, Message, JSON Details
- **FR-012**: System MUST parse Application Error entries into fields: Timestamp, Error Type, Message, Stack Trace (two-line paired format)
- **FR-013**: System MUST parse Database Error entries into fields: Timestamp, Severity (WARNING/ERROR/CRITICAL), Message, Stack Trace
- **FR-014**: System MUST display parsed entry in field layout appropriate to log type (Normal: 6 fields, App Error: 4 fields, DB Error: 5 fields)
- **FR-015**: System MUST color-code severity indicators (Normal: LOW=Gray/MEDIUM=Blue/HIGH=Green/DATA=Cyan, DB Error: WARNING=Yellow/ERROR=Red/CRITICAL=DarkRed, App Error: Red)
- **FR-016**: System MUST display emoji indicators from Normal logs (✅ ⏱️ 🗄️ ➡️ ⬅️) in UI
- **FR-017**: System MUST format JSON blocks in Details field with preserved indentation and scrolling support
- **FR-018**: System MUST provide Previous/Next navigation buttons to move between entries
- **FR-019**: System MUST display entry position indicator "Entry X of Y" or "Entry X of Y (Filtered: Z shown)" when filters active
- **FR-020**: System MUST gracefully handle malformed log entries by falling back to raw text display

#### Filtering and Search
- **FR-021**: System MUST provide date range filter with Start Date and End Date pickers
- **FR-022**: System MUST provide log type filter dropdown that filters both file list and entries
- **FR-023**: System MUST adapt severity filter checkboxes based on current log type (Normal: LOW/MEDIUM/HIGH/DATA, DB Error: WARNING/ERROR/CRITICAL, App Error: no severity)
- **FR-024**: System MUST provide source component filter dropdown populated from actual log data
- **FR-025**: System MUST provide free-text search across all log entry fields (timestamp, level, source, message, details)
- **FR-026**: Filtering MUST update entry count and navigation to show only matching entries
- **FR-027**: System MUST provide "Clear Filters" button to reset all filter controls
- **FR-028**: System MUST persist filter state when navigating between files of same log type
- **FR-029**: System MUST provide quick filter buttons: "Errors Only", "Performance", "Today"

#### View Modes and Actions
- **FR-030**: System MUST provide toggle between "Parsed View" (structured fields) and "Raw View" (original text)
- **FR-031**: Raw View MUST display original log line text without any parsing or transformation
- **FR-032**: System MUST provide "Refresh" button to reload file list with newly created files
- **FR-033**: System MUST provide "Auto-Refresh" checkbox to reload file list every 5 seconds
- **FR-034**: System MUST provide "Export Visible" button to save filtered entries to text file
- **FR-035**: System MUST provide "Copy Entry" button to copy current entry to clipboard as formatted text
- **FR-036**: System MUST provide "Open Log Directory" button to open selected user's folder in Windows Explorer

#### Integration and Error Handling
- **FR-037**: When opened from error dialog, system MUST pre-select current user and load most recent error log automatically
- **FR-038**: When opened from Settings menu, system MUST show user selector without pre-selection
- **FR-039**: System MUST handle network path access errors with user-friendly messages without revealing internal paths
- **FR-040**: System MUST handle parse errors by logging failure and switching to raw view with notification

#### Performance Requirements
- **FR-041**: All file I/O operations MUST be asynchronous to prevent UI blocking
- **FR-042**: UI interactions (button clicks, dropdown changes) MUST respond within 100ms
- **FR-043**: File loading MUST happen on background thread with progress indicator
- **FR-044**: Large log files (> 10MB) MUST use windowing/paging (load 1000 entries at a time)
- **FR-045**: Memory management MUST dispose all file streams, timers, and resources properly
- **FR-046**: Auto-refresh timer MUST pause when form is minimized or hidden

#### Security Requirements
- **FR-047**: ALL file path operations MUST use Path.Combine(), never string concatenation
- **FR-048**: User input (names, search text) MUST be validated before any file operations
- **FR-049**: Constructed file paths MUST be validated against base directory to prevent path traversal
- **FR-050**: Search regex patterns MUST have timeout (1 second) to prevent ReDoS attacks
- **FR-051**: Error messages to users MUST NOT reveal internal path structure or sensitive information

#### UI Scaling and DPI Compliance
- **FR-052**: Form MUST use AutoScaleMode.Dpi for proper scaling across different screen DPI settings
- **FR-053**: Form constructor MUST call Core_Themes.ApplyDpiScaling() and ApplyRuntimeLayoutAdjustments()
- **FR-054**: All interactive controls MUST have minimum sizes (Buttons: 75x32, touch targets: 32px height)
- **FR-055**: Layout MUST use TableLayoutPanel with mixed Absolute/Percent sizing for responsive behavior
- **FR-056**: Form MUST set MinimumSize of 1200x800 to accommodate all controls at 1080p resolution
- **FR-057**: All containers MUST use proper Padding (10px) and Margin (5px) for professional appearance

### Key Entities *(include if feature involves data)*

- **LogFilter**: Represents active filtering criteria
  - Attributes: StartDate, EndDate, SelectedLevels[], SelectedSource, SearchText
  - Actions: ApplyFilter(), ClearFilters()

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

Add to `Model_Application_Variables` or appsettings.json:

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

**Note**: These instruction files provide implementation guidance when this spec moves to the planning and task execution phases. They are listed here for reference but should not influence the specification itself (specs remain technology-agnostic).

### For Implementation Phase:
- `.github/instructions/csharp-dotnet8.instructions.md` - C# language features, naming conventions, WinForms patterns, async/await, region organization
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling, AutoScaleMode.Dpi, control minimum sizes, touch target requirements
- `.github/instructions/winforms-responsive-layout.instructions.md` - TableLayoutPanel patterns, mixed sizing, responsive architecture, padding/margin standards
- `.github/instructions/documentation.instructions.md` - XML documentation, code comments, markdown standards

### For Quality Assurance:
- `.github/instructions/security-best-practices.instructions.md` - Input validation, path traversal prevention, ReDoS protection, secure error messages
- `.github/instructions/performance-optimization.instructions.md` - Async I/O, UI responsiveness, memory management, background threading
- `.github/instructions/testing-standards.instructions.md` - Manual validation approach, success criteria patterns, testing workflows
- `.github/instructions/code-review-standards.instructions.md` - Quality checklist, review process, compilation verification

**When to reference**: Implementation team should review relevant instruction files during `/speckit.plan` and `/speckit.tasks` phases.

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: User dropdown populates with all users from network storage within 1 second
- **SC-002**: Log file list loads within 500ms for user with 20 log files
- **SC-003**: Log file parsing completes within 2 seconds for 1000-entry file
- **SC-004**: Entry navigation (Next/Previous) responds within 50ms
- **SC-005**: Filtering 5000 entries down to 100 matching entries completes within 300ms
- **SC-006**: Export of 500 filtered entries completes within 1 second
- **SC-007**: Auto-refresh updates file list within 500ms every 5 seconds
- **SC-008**: Parse success rate exceeds 95% for standard log format entries
- **SC-009**: Form scales correctly without clipping at 96/120/144/192 DPI (tested at 1080p, 1440p, 4K resolutions)
- **SC-010**: Memory usage remains stable during extended sessions (no leaks after viewing 50+ files)
- **SC-011**: Network path access failures provide clear user guidance within 100ms
- **SC-012**: 90% of developers can complete primary task (view user's error logs) on first attempt without assistance
