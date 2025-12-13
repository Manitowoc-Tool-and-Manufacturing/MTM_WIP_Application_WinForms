# Research: Developer Tools Consolidation & Core Services Refactoring

**Feature**: 006-dev-tools-consolidation  
**Date**: 2025-12-13  
**Status**: Complete

---

## R1: Log Storage Format

### Decision
**Logs are stored in CSV files only** - there is no database logging of general application logs.

### Findings

**Service_LoggingUtility.cs** creates three CSV log files per session:
- `{username}_{timestamp}_normal.csv` - General logs
- `{username}_{timestamp}_db_error.csv` - Database errors
- `{username}_{timestamp}_app_error.csv` - Application errors

**CSV Format**:
```csv
Timestamp,Level,Source,Message,Details
yyyy-MM-dd HH:mm:ss,{level},{source},{message},{details}
```

Fields are escaped via `EscapeCsvField()` method to handle commas/quotes in content.

**Log Directories**:
- **Primary**: `X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs` (network share, or developer-specific path based on user)
- **Fallback**: `%ProgramData%\MTM_WIP_Application_Winforms\Logs`
- **Organization**: Logs organized by username subdirectories

### Implications for Implementation

1. The Developer Tools Logs tab will need a **CSV parsing service** to read log files
2. No database queries needed for general application logs
3. The existing `Form_ViewLogsForm` already has CSV parsing code that can be migrated
4. Auto-refresh will need to use `FileSystemWatcher` or periodic file checks
5. Large log files may require virtualization for performance

---

## R2: Log Database Table Schema

### Decision
**A `log_error` table exists** for database error logging, separate from CSV logging.

### Findings

**Table Schema** (`Database/UpdatedDatabase/07_log_error.sql`):

| Column | Type | Notes |
|--------|------|-------|
| `ID` | INT(11) PK | Auto-increment |
| `User` | VARCHAR(100) | Username |
| `Severity` | ENUM('Information','Warning','Error','Critical','High') | Error level |
| `ErrorType` | VARCHAR(100) | Exception type |
| `ErrorMessage` | TEXT | Error message |
| `StackTrace` | TEXT | Full stack trace |
| `ModuleName` | VARCHAR(200) | Source module |
| `MethodName` | VARCHAR(200) | Source method |
| `AdditionalInfo` | TEXT | Context data |
| `MachineName` | VARCHAR(100) | Machine name |
| `OSVersion` | VARCHAR(100) | OS version |
| `AppVersion` | VARCHAR(50) | App version |
| `ErrorTime` | DATETIME | Timestamp (DEFAULT CURRENT_TIMESTAMP) |

**Existing Stored Procedures**:

| Procedure | Purpose |
|-----------|---------|
| `log_error_Add_Error` | Insert new error |
| `log_error_Get_All` | Get all errors |
| `log_error_Get_ByDateRange` | Filter by date range |
| `log_error_Get_ByUser` | Filter by user |
| `log_error_Get_Unique` | Get unique errors (for grouping) |
| `log_error_Delete_All` | Clear all errors |
| `log_error_Delete_ById` | Delete specific error |

### Implications for Implementation

1. **Dashboard statistics** can query the `log_error` table for error counts
2. Hybrid approach: CSV for full log viewing, database for structured error analytics
3. Existing stored procedures cover most analytics needs
4. New stored procedure `md_devtools_GetLogStatistics` can aggregate counts by severity/date
5. `log_error_Get_Unique` provides foundation for error grouping feature

---

## R3: Form_ViewLogsForm Patterns

### Decision
**Rich filtering/grouping patterns exist** that should be migrated and enhanced.

### Findings

**Filtering Methods** (from `Form_ViewLogsForm.cs`):

```csharp
// Core filtering infrastructure
ApplyCurrentFilter()       // Apply active filter to entries
ApplyErrorsOnlyFilter()    // Show only ERROR/CRITICAL
ApplyPerformanceFilter()   // Performance-related entries
ApplyTodayFilter()         // Today's entries only
ClearAllFilters()          // Reset all filters
PopulateSourceDropdown()   // Source-based filtering
```

**Grouping Implementation**:

```csharp
GroupEntries()             // Groups ERROR/CRITICAL by {ErrorType}_{MethodName}
NavigateToGroup()          // Navigate between grouped errors
_groupedEntries            // Dictionary tracking occurrence counts
```

**Export Functionality**:

- Export to `.txt` format via `SaveFileDialog`
- Timestamped filename: `logs_export_{timestamp}.txt`
- `FormatEntriesForExport()` handles formatting
- Performance target: 500 entries in <1 second

**Columns Displayed**:
- Timestamp, Level, Source, Message, Details
- Severity color coding via `ApplySeverityColorCoding()`
- Log type icons via `GetSeverityEmoji()`: 🔴 Error, ⚠️ Warning, ℹ️ Info

### Implications for Implementation

1. **Direct Migration**: Move filtering/grouping code to `Service_DeveloperTools`
2. **Reuse Patterns**: Same filtering model applies to new Logs tab
3. **Enhance Export**: Add CSV and JSON export options (not just TXT)
4. **Preserve UX**: Keep emoji severity indicators and color coding
5. **Performance**: Existing patterns designed for 1000+ entries

---

## R4: Feedback Table Schema

### Decision
**Comprehensive `UserFeedback` table exists** with robust filtering support.

### Findings

**Table Schema** (`Database/UpdatedDatabase/12_UserFeedback.sql`):

| Column | Type | Notes |
|--------|------|-------|
| `FeedbackID` | INT PK | Auto-increment |
| `FeedbackType` | VARCHAR(50) | Bug/Feature/Question |
| `TrackingNumber` | VARCHAR(50) | Unique tracking ID |
| `UserID` | INT FK | Submitting user |
| `SubmissionDateTime` | DATETIME | Auto-set |
| `LastUpdatedDateTime` | DATETIME | Auto-update |
| `ApplicationVersion` | VARCHAR(50) | App version |
| `OSVersion` | VARCHAR(100) | OS info |
| `MachineIdentifier` | VARCHAR(100) | Machine name |
| `WindowForm` | VARCHAR(100) | Origin form |
| `ActiveSection` | VARCHAR(100) | Origin section |
| `Category` | VARCHAR(100) | Category |
| `CustomCategory` | VARCHAR(100) | User-defined category |
| `Severity` | VARCHAR(50) | Bug severity |
| `Priority` | VARCHAR(50) | Priority level |
| `Title` | VARCHAR(255) | Summary |
| `Description` | MEDIUMTEXT | Full description |
| `StepsToReproduce` | MEDIUMTEXT | Repro steps |
| `ExpectedBehavior` | MEDIUMTEXT | Expected behavior |
| `ActualBehavior` | MEDIUMTEXT | Actual behavior |
| `BusinessJustification` | MEDIUMTEXT | For feature requests |
| `AffectedUsers` | VARCHAR(50) | Impact scope |
| `Status` | VARCHAR(50) | New/In Progress/Resolved |
| `AssignedToDeveloperID` | INT FK | Assigned developer |
| `DeveloperNotes` | MEDIUMTEXT | Developer notes |
| `ResolutionDateTime` | DATETIME | Resolution timestamp |
| `IsDuplicate` | TINYINT(1) | Duplicate flag |
| `DuplicateOfFeedbackID` | INT FK | Original if duplicate |

**Existing Stored Procedures**:

| Procedure | Purpose |
|-----------|---------|
| `md_feedback_GetAll` | Filterable query (Status, Type, User, Date, Developer, Category) |
| `md_feedback_GetById` | Single feedback lookup |
| `md_feedback_GetByUser` | User's submitted feedback |
| `md_feedback_Insert` | Create new feedback |
| `md_feedback_UpdateStatus` | Change status |
| `md_feedback_UpdateDetails` | Edit details |
| `md_feedback_MarkDuplicate` | Mark as duplicate |
| `md_feedback_ExportToCsv` | Export functionality |

**DAO Filter Pattern** (`Dao_UserFeedback.cs`):
```csharp
// All parameters optional, default DBNull.Value
FilterStatus, FilterFeedbackType, FilterUserID, 
FilterDateFrom, FilterDateTo, FilterAssignedDeveloperID, FilterCategory
```

### Implications for Implementation

1. **Existing Infrastructure**: All filtering/CRUD operations already implemented
2. **Dashboard Stats**: Add `md_feedback_GetSummary` for status counts
3. **User View**: `md_feedback_GetByUser` supports System Health form
4. **Bulk Operations**: May need new SP for bulk status updates
5. **Export**: `md_feedback_ExportToCsv` already handles export

---

## R5: Chart Library

### Decision
**No WinForms charting library installed**. Visualization uses WebView2 + HTML/JavaScript.

### Findings

**No Chart Libraries Found**:
- No `System.Windows.Forms.DataVisualization.Charting` reference
- No third-party libraries (LiveCharts, OxyPlot, ScottPlot)
- `.csproj` does not include any charting packages

**Current Visualization Approach**:
- Uses **WebView2** control for rich visualizations
- HTML templates stored in `Documentation/Help/Templates/`
- Data injected via JavaScript: `window.injectedData = {json}`
- Example: `VisualUserAnalytics_Enhanced.html` (user analytics charts)

**Other UI Approaches Used**:
- DataGridView for tabular data
- TreeView for hierarchical data (transaction lifecycle)
- Simple GDI+ drawing for watermarks (`Helper_PrintManager`)

### Options Analysis

| Option | Pros | Cons |
|--------|------|------|
| **WebView2 + Chart.js** | Already used pattern, rich visuals, modern appearance | Requires HTML template, async complexity |
| **Custom GDI+ Drawing** | No dependencies, full control, native | More code, less polished appearance |
| **Add LiveCharts2** | Native WinForms, modern, declarative | New dependency, learning curve |
| **Text-based Display** | Simplest, reliable, fast | Less visual impact |

### Recommendation

Use **WebView2 + Chart.js** pattern for Dashboard timeline charts:
- Proven approach already in codebase
- Professional-looking visualizations
- Responsive and interactive
- Can reuse existing WebView2 infrastructure

For simpler metrics, use **text-based summary cards** with color coding:
- Fast to implement
- No async complexity
- Works offline
- Consistent with existing UI patterns

---

## Summary of Research Outcomes

| Area | Finding | Implementation Impact |
|------|---------|----------------------|
| **Log Storage** | CSV files in user directories | Need CSV parser service |
| **Database Logs** | `log_error` table with stored procedures | Query for dashboard stats |
| **ViewLogs Patterns** | Rich filtering/grouping/export | Direct code migration |
| **Feedback Schema** | 25+ columns, comprehensive SPs | Infrastructure ready |
| **Charts** | WebView2 + HTML pattern preferred | Reuse existing approach |

### Unresolved Items

None - all research questions answered.

### Next Steps

1. Generate `data-model.md` with entity definitions
2. Generate stored procedure contracts for new analytics queries
3. Generate `quickstart.md` with code patterns
4. Update agent context with new service patterns
