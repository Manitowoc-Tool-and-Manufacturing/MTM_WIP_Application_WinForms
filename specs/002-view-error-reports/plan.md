# Implementation Plan: View Error Reports Window

**Feature Branch**: `002-view-error-reports`  
**Created**: 2025-10-25  
**Status**: Planning Complete

---

## Overview

Build a comprehensive WinForms window for developers to browse, filter, search, and manage user-submitted error reports. The window displays reports in a master-detail layout with filtering, pagination, status management, and export capabilities.

---

## Technical Context

### Language & Framework
- **Language**: C# 12 / .NET 8.0
- **Framework**: Windows Forms (WinForms)
- **UI Pattern**: Master-Detail with SplitContainer (Option A from spec)

### Database
- **Database**: MySQL 5.7.24+
- **Connector**: MySql.Data 9.4.0
- **Access Pattern**: Stored procedures only (no inline SQL)
- **Table**: `error_reports` (from Feature 001)

### Project Type
- Desktop application (single executable)
- Source structure: Forms-based with DAO layer separation

### Key Dependencies
- **Feature 001**: Error Reporting System (MUST be complete)
  - `error_reports` table schema
  - `Dao_ErrorReports` base class
  - All `sp_error_reports_*` stored procedures
  - `Model_ErrorReport` POCO

### External Libraries
- ClosedXML (Excel export)
- MySql.Data (database connector)
- System.Windows.Forms (UI framework)

---

## Constitution Check

### Principle I: Stored Procedure Only Database Access ✅

**Status**: COMPLIANT

All data access routes through `Dao_ErrorReports` methods which call stored procedures via `Helper_Database_StoredProcedure`:
- `sp_error_reports_GetAll` (filtering, pagination)
- `sp_error_reports_GetByID` (single report details)
- `sp_error_reports_UpdateStatus` (status changes)
- `sp_error_reports_GetUserList` (dropdown population)
- `sp_error_reports_GetMachineList` (dropdown population)

No inline SQL, all parameters use dictionary pattern without `p_` prefix.

---

### Principle II: DaoResult<T> Wrapper Pattern ✅

**Status**: COMPLIANT

All DAO methods return `DaoResult<T>`:
- `GetFilteredReportsAsync()` → `DaoResult<PaginatedResult<Model_ErrorReport>>`
- `UpdateReportStatusAsync()` → `DaoResult<Model_ErrorReport>`
- `GetUserListAsync()` → `DaoResult<List<string>>`
- `ExportToCSVAsync()` → `DaoResult<string>`

UI layer checks `IsSuccess` before accessing `Data`, handles failures with `Service_ErrorHandler`.

---

### Principle III: Region Organization and Method Ordering ✅

**Status**: COMPLIANT

Form will follow standard region structure:
```csharp
#region Fields
#region Properties
#region Constructors
#region Form Lifecycle (Load, Shown, FormClosing)
#region Data Loading
#region Filter and Search
#region Pagination
#region Status Updates
#region Export Operations
#region Grid Events
#region UI Helpers
#region Cleanup
```

Methods ordered: public → protected → private → static within each region.

---

### Principle IV: Service_ErrorHandler for All User-Facing Errors ✅

**Status**: COMPLIANT

No `MessageBox.Show()` calls. All errors use `Service_ErrorHandler`:
- `HandleException()` for caught exceptions
- `ShowConfirmation()` for status update prompts
- `HandleValidationError()` for filter validation

---

### Principle V: Core_Themes DPI Scaling ✅

**Status**: COMPLIANT

Form constructor includes:
```csharp
InitializeComponent();
Core_Themes.ApplyDpiScaling(this);
Core_Themes.ApplyRuntimeLayoutAdjustments(this);
```

All controls sized using DPI-aware measurements, tested at 100%, 125%, 150% scaling.

---

### Principle VII: XML Documentation ✅

**Status**: COMPLIANT

All public methods and DAO methods will include XML documentation:
```csharp
/// <summary>
/// Loads error reports based on current filter criteria with pagination.
/// </summary>
/// <param name="pageNumber">Page number to load (1-based).</param>
/// <returns>Task completing when reports are loaded and bound to grid.</returns>
```

---

### Principle IX: Async/Await Patterns ✅

**Status**: COMPLIANT

All I/O operations use async/await:
- Database queries: `await Dao_ErrorReports.GetFilteredReportsAsync()`
- Status updates: `await Dao_ErrorReports.UpdateReportStatusAsync()`
- Export operations: `await Dao_ErrorReports.ExportToExcelAsync()`

No `.Result` or `.Wait()` blocking calls on UI thread.

---

## Architecture

### Component Hierarchy

```
ViewErrorReportsForm : Form
│
├── FilterPanel (GroupBox, Dock=Top)
│   ├── dtpDateFrom (DateTimePicker)
│   ├── dtpDateTo (DateTimePicker)
│   ├── cmbUsers (ComboBox, MultiSelect)
│   ├── cmbMachines (ComboBox, MultiSelect)
│   ├── chkNew, chkReviewed, chkResolved (CheckBoxes)
│   ├── txtSearch (TextBox)
│   ├── btnApplyFilters (Button)
│   └── btnClearFilters (Button)
│
├── splitMain (SplitContainer, Dock=Fill, Orientation=Horizontal)
│   │
│   ├── Panel1 (60% height)
│   │   └── dgvReports (DataGridView)
│   │       ├── colReportID
│   │       ├── colDateTime
│   │       ├── colUser
│   │       ├── colMachine
│   │       ├── colErrorType
│   │       ├── colSummary (truncated)
│   │       └── colStatus
│   │
│   └── Panel2 (40% height)
│       └── grpDetails (GroupBox)
│           ├── lblErrorType, txtErrorType
│           ├── lblTimestamp, txtTimestamp
│           ├── lblUser, txtUser
│           ├── lblMachine, txtMachine
│           ├── lblVersion, txtVersion
│           ├── lblSummary, txtSummary (multiline)
│           ├── grpUserNotes (GroupBox, highlighted)
│           │   └── txtUserNotes (multiline)
│           ├── btnExpandTechnical (Button)
│           ├── txtTechnicalDetails (multiline, collapsed)
│           ├── btnExpandCallStack (Button)
│           ├── txtCallStack (multiline, collapsed)
│           ├── btnMarkReviewed (Button)
│           ├── btnMarkResolved (Button)
│           ├── btnCopyAll (Button)
│           └── btnExportReport (Button)
│
├── paginationPanel (Panel, Dock=Bottom)
│   ├── btnPrevPage (Button)
│   ├── lblPageInfo (Label) - "Page 1 of 10"
│   ├── cmbPageSize (ComboBox) - "100 per page"
│   ├── btnNextPage (Button)
│   └── txtJumpToPage (TextBox + Button)
│
└── statusStrip (StatusStrip)
    ├── progressBar (ProgressBar)
    └── lblRecordCount (Label) - "Showing 1-100 of 10,243 reports"
```

---

## Data Flow

### 1. Form Load Sequence

```
ViewErrorReportsForm_Load()
    ↓
LoadDropdownDataAsync()
    ↓
Dao_ErrorReports.GetUserListAsync()
Dao_ErrorReports.GetMachineListAsync()
    ↓
Populate cmbUsers, cmbMachines
    ↓
LoadSavedFilters() // from user preferences
    ↓
ApplyDefaultFilters() // Last 30 days, All statuses
    ↓
LoadReportsAsync(pageNumber: 1)
```

### 2. Filter Application Flow

```
btnApplyFilters_Click()
    ↓
ValidateFilterCriteria()
    ↓
BuildFilterCriteria()
    ↓
LoadReportsAsync(pageNumber: 1) // Reset to page 1
```

### 3. Data Retrieval Flow

```
LoadReportsAsync(pageNumber)
    ↓
Show progressBar
    ↓
Build ErrorReportFilterCriteria from UI
    ↓
Dao_ErrorReports.GetFilteredReportsAsync(criteria)
    ↓
Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync()
    ↓
sp_error_reports_GetAll(@p_DateFrom, @p_DateTo, ...)
    ↓
MySQL query with indexes (Timestamp, UserName, Status, MachineName)
    ↓
Return DataTable + TotalCount
    ↓
Map DataTable → List<Model_ErrorReport>
    ↓
Transform to List<ErrorReportGridRow> (truncate summaries, format dates)
    ↓
Wrap in PaginatedResult<ErrorReportGridRow>
    ↓
Return DaoResult<PaginatedResult<ErrorReportGridRow>>
    ↓
Bind dgvReports.DataSource to BindingList
    ↓
Apply row coloring (New=LightCoral, Reviewed=Yellow, Resolved=Green)
    ↓
Update pagination controls (Previous/Next enable state)
    ↓
Update lblRecordCount "Showing 1-100 of 10,243"
    ↓
Hide progressBar
```

### 4. Status Update Flow

```
btnMarkReviewed_Click()
    ↓
Service_ErrorHandler.ShowConfirmation("Mark as Reviewed?")
    ↓
Show dialog for developer notes input
    ↓
Dao_ErrorReports.UpdateReportStatusAsync(
    reportId: selectedReport.ReportID,
    newStatus: "Reviewed",
    reviewedBy: currentUsername,
    developerNotes: userInput
)
    ↓
sp_error_reports_UpdateStatus(@p_ReportID, @p_NewStatus, ...)
    ↓
MySQL UPDATE with ReviewedDate = NOW()
    ↓
Return updated Model_ErrorReport
    ↓
LoadReportsAsync() // Refresh grid to show updated colors
    ↓
Service_ErrorHandler.ShowSuccess("Status updated to Reviewed")
```

### 5. Export Flow

```
btnExportCSV_Click()
    ↓
SaveFileDialog.ShowDialog()
    ↓
Get current filter criteria (ignore pagination)
    ↓
Dao_ErrorReports.ExportToCSVAsync(criteria, outputPath)
    ↓
Fetch all matching reports (no page limit)
    ↓
StreamWriter with UTF-8 BOM
    ↓
Write header row
    ↓
For each report: write CSV row with quoted fields
    ↓
Close StreamWriter
    ↓
Return DaoResult<string> with file path
    ↓
Service_ErrorHandler.ShowSuccess($"Exported {count} reports to {path}")
```

---

## File Structure

### New Files

```
Forms/Development/ViewErrorReportsForm.cs
Forms/Development/ViewErrorReportsForm.Designer.cs
Forms/Development/ViewErrorReportsForm.resx
Models/ErrorReportFilterCriteria.cs
Models/ErrorReportGridRow.cs
Models/PaginatedResult.cs
Models/ErrorReportViewerPreferences.cs
```

### Modified Files

```
Data/Dao_ErrorReports.cs (extend with new methods)
```

### No New Database Files

All stored procedures exist from Feature 001.

---

## Implementation Phases

### Phase 1: DAO Extensions (2 hours)

**Location**: `Data/Dao_ErrorReports.cs`

**Tasks**:
1. Add `GetFilteredReportsAsync()` method
   - Build parameter dictionary from ErrorReportFilterCriteria
   - Call `sp_error_reports_GetAll` via Helper_Database_StoredProcedure
   - Map DataTable to List<Model_ErrorReport>
   - Wrap in PaginatedResult
   - Return DaoResult<PaginatedResult<Model_ErrorReport>>

2. Add `GetUserListAsync()` method
   - Call `sp_error_reports_GetUserList`
   - Extract distinct UserName values
   - Cache for 5 minutes (MemoryCache)
   - Return DaoResult<List<string>>

3. Add `GetMachineListAsync()` method
   - Call `sp_error_reports_GetMachineList`
   - Extract distinct MachineName values
   - Cache for 5 minutes
   - Return DaoResult<List<string>>

4. Add `UpdateReportStatusAsync()` method
   - Validate newStatus against allowed values
   - Call `sp_error_reports_UpdateStatus`
   - Return updated Model_ErrorReport
   - Return DaoResult<Model_ErrorReport>

5. Add `ExportToCSVAsync()` method
   - Build filter criteria without pagination
   - Fetch all matching reports
   - Write to StreamWriter with CSV formatting
   - Handle >50,000 record limit
   - Return DaoResult<string> with file path

6. Add `ExportToExcelAsync()` method
   - Same as CSV but use ClosedXML
   - Apply column formatting, freeze panes
   - Return DaoResult<string> with file path

**Testing**:
- Unit test each method with sample data
- Verify DaoResult wrapping
- Test pagination boundaries (first page, last page)
- Test filter combinations

---

### Phase 2: Form UI Layout (3 hours)

**Location**: `Forms/Development/ViewErrorReportsForm.cs`

**Tasks**:
1. Create form with standard MTM layout
   - Size: 1200x800 (design-time)
   - StartPosition: CenterScreen
   - Text: "View Error Reports"
   - Call Core_Themes.ApplyDpiScaling() in constructor

2. Add FilterPanel (GroupBox)
   - Dock: Top, Height: 80
   - Controls arranged in 3 rows:
     - Row 1: DateFrom, DateTo, User dropdown
     - Row 2: Machine dropdown, Status checkboxes
     - Row 3: Search textbox, Apply/Clear buttons

3. Add SplitContainer
   - Dock: Fill
   - Orientation: Horizontal
   - SplitterDistance: 450 (60% of 750)
   - Panel1: DataGridView
   - Panel2: Detail panel

4. Configure DataGridView
   - Columns: ReportID, DateTime, User, Machine, ErrorType, Summary, Status
   - AllowUserToAddRows: false
   - ReadOnly: true
   - SelectionMode: FullRowSelect
   - MultiSelect: false
   - Enable column sorting

5. Add Detail Panel controls
   - GroupBox: "Report Details"
   - Labels and TextBoxes for each field
   - User Notes GroupBox with highlighted border
   - Collapsible sections for Technical/CallStack
   - Action buttons: Mark Reviewed, Mark Resolved, Copy All, Export

6. Add pagination controls
   - Panel docked at bottom
   - Previous/Next buttons
   - Page info label
   - Page size combo (25, 50, 100, 200)
   - Jump to page textbox

7. Add StatusStrip
   - ProgressBar (hidden by default)
   - Record count label

**Testing**:
- Run form at 100%, 125%, 150% DPI
- Verify splitter resizes correctly
- Check tab order
- Verify all controls visible at minimum size

---

### Phase 3: Data Binding & Filtering (2 hours)

**Location**: `Forms/Development/ViewErrorReportsForm.cs`

**Tasks**:
1. Implement Form_Load event
   - Call LoadDropdownDataAsync()
   - Load saved filters from preferences
   - Apply default filters (Last 30 days)
   - Call LoadReportsAsync(1)

2. Implement LoadDropdownDataAsync()
   - Fetch user list from DAO
   - Populate cmbUsers with "All" + user names
   - Fetch machine list from DAO
   - Populate cmbMachines with "All" + machine names

3. Implement BuildFilterCriteria()
   - Read all filter control values
   - Create ErrorReportFilterCriteria object
   - Set pagination values
   - Return criteria

4. Implement LoadReportsAsync(pageNumber)
   - Show progress bar
   - Build filter criteria
   - Call Dao_ErrorReports.GetFilteredReportsAsync()
   - Check IsSuccess
   - Map to ErrorReportGridRow list
   - Bind to DataGridView
   - Call ApplyRowColoring()
   - Update pagination controls
   - Update record count label
   - Hide progress bar
   - Handle errors with Service_ErrorHandler

5. Implement ApplyRowColoring()
   - Iterate DataGridView rows
   - Set row.DefaultCellStyle.BackColor based on Status
   - New → LightCoral
   - Reviewed → LightGoldenrodYellow
   - Resolved → LightGreen

6. Implement filter button handlers
   - btnApplyFilters_Click: ValidateFilters → LoadReportsAsync(1)
   - btnClearFilters_Click: Reset all controls → LoadReportsAsync(1)

7. Implement pagination handlers
   - btnPrevPage_Click: LoadReportsAsync(currentPage - 1)
   - btnNextPage_Click: LoadReportsAsync(currentPage + 1)
   - cmbPageSize_SelectedIndexChanged: LoadReportsAsync(1)

8. Implement grid selection handler
   - dgvReports_SelectionChanged: Populate detail panel from selected row

**Testing**:
- Apply various filter combinations
- Verify pagination Previous/Next enable/disable correctly
- Verify page size changes reset to page 1
- Verify grid selection populates detail panel
- Test with empty result sets

---

### Phase 4: Export Functionality (2 hours)

**Location**: Form + DAO

**Tasks**:
1. Add export buttons to form
   - btnExportCSV: Export filtered reports to CSV
   - btnExportExcel: Export filtered reports to Excel
   - btnExportSelected: Export only selected rows

2. Implement btnExportCSV_Click()
   - Show SaveFileDialog with .csv filter
   - Get current filter criteria
   - Call Dao_ErrorReports.ExportToCSVAsync()
   - Show progress during export
   - Handle errors
   - Show success message with file path

3. Implement btnExportExcel_Click()
   - Show SaveFileDialog with .xlsx filter
   - Get current filter criteria
   - Call Dao_ErrorReports.ExportToExcelAsync()
   - Show progress during export
   - Handle errors
   - Show success message

4. Implement btnExportSelected_Click()
   - Get selected rows from DataGridView
   - Create temporary criteria with selected ReportIDs
   - Export using CSV or Excel (user choice)

5. Add progress reporting
   - Update progress bar during export
   - Show "Exporting X of Y reports..." in status label

**Testing**:
- Export 10, 100, 1000 reports
- Verify CSV opens correctly in Excel
- Verify Excel formatting (headers, column widths)
- Test with filters applied
- Test "Export Selected" with multiple selections

---

### Phase 5: Status Update Operations (1 hour)

**Location**: `Forms/Development/ViewErrorReportsForm.cs`

**Tasks**:
1. Implement btnMarkReviewed_Click()
   - Show confirmation dialog
   - Prompt for developer notes (multiline input)
   - Call Dao_ErrorReports.UpdateReportStatusAsync()
   - Check IsSuccess
   - Reload current page to show updated row
   - Show success message

2. Implement btnMarkResolved_Click()
   - Same flow as Reviewed
   - Different status value

3. Implement status update dialog
   - Form with multiline TextBox for notes
   - OK/Cancel buttons
   - Validation: notes optional but recommended

4. Add copy functionality
   - btnCopyAll_Click: Copy all report details to clipboard
   - Format as text with headers

5. Add single report export
   - btnExportReport_Click: Export selected report to JSON or TXT

**Testing**:
- Update status of New → Reviewed
- Update status of Reviewed → Resolved
- Update status of Resolved → New (regression test)
- Verify ReviewedBy and ReviewedDate populated
- Verify grid refreshes with new color
- Copy report and paste into text editor

---

## Testing Strategy

### Manual Testing Checklist

#### Functional Testing
- [ ] Form loads without errors at all DPI scales
- [ ] Filter dropdowns populate from database
- [ ] Date range filter works (From ≤ To validation)
- [ ] User filter (single and multiple selection)
- [ ] Machine filter works correctly
- [ ] Status checkboxes filter correctly
- [ ] Search text filters across Summary, UserNotes, Technical
- [ ] At least one filter must be selected
- [ ] Clear filters resets all controls
- [ ] Grid displays correct columns
- [ ] Column sorting works (click headers)
- [ ] Row colors match status (New=red, Reviewed=yellow, Resolved=green)
- [ ] Selecting row populates detail panel
- [ ] Detail panel shows all fields correctly
- [ ] User Notes section highlighted
- [ ] Technical Details/Call Stack expand/collapse
- [ ] Previous button disabled on page 1
- [ ] Next button disabled on last page
- [ ] Page size dropdown changes results per page
- [ ] Jump to page works
- [ ] Record count label accurate
- [ ] Mark as Reviewed updates status and color
- [ ] Mark as Resolved updates status and color
- [ ] Developer notes saved correctly
- [ ] ReviewedBy and ReviewedDate populated
- [ ] Export to CSV creates valid file
- [ ] Export to Excel creates formatted workbook
- [ ] Export respects current filters
- [ ] Export Selected exports only selected rows
- [ ] Copy All copies report to clipboard
- [ ] Single report export works

#### Performance Testing
- [ ] Loads 100 reports in < 1 second
- [ ] Filters 1000 reports in < 500ms
- [ ] Pagination < 200ms per page
- [ ] Export 500 to CSV in < 2 seconds
- [ ] Export 500 to Excel in < 4 seconds
- [ ] Status update < 300ms
- [ ] Search across 1000 reports in < 400ms

#### Error Handling Testing
- [ ] Invalid date range shows validation error
- [ ] Database connection failure handled gracefully
- [ ] Export to invalid path shows error
- [ ] Large dataset export (>50,000) prevented with message
- [ ] Concurrent status update (two developers) handled
- [ ] Empty result set displays appropriately
- [ ] Network interruption during load handled

#### UI/UX Testing
- [ ] Tab order logical
- [ ] Keyboard shortcuts work (if implemented)
- [ ] Progress bar shows during long operations
- [ ] Error messages clear and actionable
- [ ] Success messages confirm actions
- [ ] Confirmation dialogs for destructive actions
- [ ] Form scales correctly at 100%, 125%, 150% DPI
- [ ] Splitter resizable and position persists
- [ ] Colors readable in all themes

---

## Performance Targets

| Operation | Target | Measurement |
|-----------|--------|-------------|
| Initial load | < 1s | Time from Form_Load to grid populated (100 records) |
| Filter application | < 500ms | Time from Apply click to grid refresh (1000 records) |
| Pagination | < 200ms | Time from Previous/Next click to grid refresh |
| Export CSV (500) | < 2s | Time from Export click to file saved |
| Export Excel (500) | < 4s | Time from Export click to workbook saved |
| Status update | < 300ms | Time from button click to grid refresh |
| Search | < 400ms | Time from Apply to results (1000 records searched) |
| Dropdown population | < 200ms | Time to load User/Machine lists |

---

## Security Considerations

- ✅ All database queries use parameterized stored procedures (SQL injection protected)
- ✅ No inline SQL anywhere in application code
- ✅ Developer notes logged with username and timestamp (audit trail)
- ✅ Status changes logged to database for compliance
- ✅ Export permissions can be restricted (admin-only flag in config)
- ✅ No sensitive data in logs (passwords, credentials filtered)
- ✅ Clipboard operations only for currently visible report
- ✅ File export validates output path (prevent directory traversal)

---

## Deployment Notes

### Prerequisites
- Feature 001 must be deployed to production
- `error_reports` table exists with data
- All `sp_error_reports_*` procedures deployed
- ClosedXML library in bin directory

### Configuration
- No new config required
- Uses existing `Helper_Database_Variables.GetConnectionString()`
- Uses existing `Model_AppVariables` for command timeout

### Rollout Plan
1. Deploy to test environment
2. Run manual testing checklist
3. Verify with sample production data (anonymized)
4. Deploy to production during maintenance window
5. Monitor logs for errors in first 24 hours

---

## Maintenance

### Future Enhancements
- Add batch status update (multiple reports at once)
- Add report assignment (assign to developer)
- Add email notifications for new reports
- Add report analytics dashboard
- Add saved filter presets
- Add report comparison (diff two reports)

### Known Limitations
- No real-time updates (manual refresh required)
- Export limited to 50,000 records per operation
- No report deletion (status-based only)
- Filter persistence per-user (not shared)

---

## References

- **Specification**: `specs/002-view-error-reports/spec.md`
- **Research**: `specs/002-view-error-reports/research.md`
- **Data Model**: `specs/002-view-error-reports/data-model.md`
- **Contracts**: `specs/002-view-error-reports/contracts/Dao_ErrorReports_Extensions.md`
- **Quickstart**: `specs/002-view-error-reports/quickstart.md`

### Instruction Files
- `.github/instructions/csharp-dotnet8.instructions.md`
- `.github/instructions/mysql-database.instructions.md`
- `.github/instructions/testing-standards.instructions.md`
- `.github/instructions/performance-optimization.instructions.md`
- `.github/instructions/code-review-standards.instructions.md`

---

**Plan Status**: ✅ Complete  
**Next Step**: `/speckit.tasks` to generate task breakdown  
**Estimated Total Effort**: 10 hours
