# Research: View Error Reports Window

**Feature**: View Error Reports Window  
**Created**: 2025-10-25  
**Status**: Complete

## Research Questions from Technical Context

All technical context items were resolved during planning. No unknowns requiring research agents.

## Technology Decisions

### 1. DataGridView Binding Strategy

**Decision**: Use `DataTable` binding with synchronous `DataSource` property assignment

**Rationale**:
- Existing pattern throughout codebase (Control_AdvancedRemove, Control_History, etc.)
- WinForms DataGridView works best with DataTable for dynamic columns
- Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync returns DataTable directly
- No need for ObservableCollection or custom binding since this is read-mostly grid
- Color-coding via CellFormatting event handler based on Status column

**Alternatives Considered**:
- BindingList<T> with strongly-typed models: Rejected because filtering and sorting are handled server-side via stored procedure parameters, not client-side LINQ
- Manual row-by-row population: Rejected due to performance overhead and complexity

### 2. Master-Detail Layout Approach

**Decision**: Horizontal split (Option A from spec) with 60% grid / 40% detail panel

**Rationale**:
- Matches existing WinForms patterns in application (SplitContainer with Orientation.Horizontal)
- Grid requires horizontal space for multiple columns (ID, Date, User, Machine, Type, Summary, Status)
- Detail view needs vertical space for long text fields (CallStack, TechnicalDetails)
- User workflow: Browse list → select → view details (top-to-bottom reading pattern)

**Alternatives Considered**:
- Option B (Vertical split with filter panel): Rejected because filters can be inline above grid (less screen real estate)
- Option C (Modal popup): Rejected because context switching slows developer workflow
- Option D (Three panels): Rejected due to complexity and cramped space per panel

### 3. Filter Implementation Pattern

**Decision**: Build filter parameters Dictionary and pass to stored procedure

**Rationale**:
- Stored procedure handles complex WHERE clause construction and SQL injection prevention
- Existing pattern in Control_AdvancedRemove demonstrates successful approach
- Parameters: DateFrom, DateTo, UserName, Status, MachineName, SearchText
- Server-side filtering reduces data transfer and client-side processing
- Search text applies via LIKE '%term%' on ErrorSummary, UserNotes, TechnicalDetails columns

**Alternatives Considered**:
- Client-side DataTable filtering: Rejected due to performance with large datasets and complexity
- Multiple stored procedures per filter combination: Rejected due to explosion of SP variants

### 4. Stored Procedure Inventory

**Decision**: Need to create 4 additional stored procedures beyond existing sp_error_reports_Insert

**Required Procedures**:
1. **sp_error_reports_GetAll** - Retrieve filtered reports
   - IN: DateFrom, DateTo, UserName, Status, MachineName, SearchText (all optional)
   - OUT: DataTable with columns matching grid (ID, Date, User, Machine, Type, Summary, Status)
   - Logic: Dynamic WHERE clause construction, LIKE search across text fields

2. **sp_error_reports_GetByID** - Get single report details
   - IN: ReportID
   - OUT: Single row with all fields including full CallStack and TechnicalDetails

3. **sp_error_reports_UpdateStatus** - Change status and add developer notes
   - IN: ReportID, NewStatus, DeveloperNotes, ReviewedBy, ReviewedDate
   - OUT: Success/failure indicator

4. **sp_error_reports_GetUserList** - Populate user filter dropdown
   - OUT: Distinct UserName values from error_reports table

5. **sp_error_reports_GetMachineList** - Populate machine filter dropdown
   - OUT: Distinct MachineName values from error_reports table

**Rationale**:
- Follows stored-procedure-only mandate from Constitution
- Each procedure has single responsibility
- GetAll handles filtering logic server-side for performance
- GetByID avoids transferring 10KB+ call stacks until needed

### 5. Export Strategy

**Decision**: Use ClosedXML for Excel export, custom CSV writer for CSV

**Rationale**:
- ClosedXML already in project dependencies (seen in documentation references)
- CSV export via StringBuilder for simplicity and performance
- Export filtered dataset (DataTable from current grid binding)
- Both formats support UTF-8 for special characters in error text
- SaveFileDialog for user-chosen path

**Alternatives Considered**:
- EPPlus: Rejected because ClosedXML already available
- System.IO.Packaging for Excel: Rejected due to complexity vs ClosedXML

### 6. Status Management Workflow

**Decision**: Confirmation dialog before status change, developer notes optional

**Rationale**:
- Prevents accidental status changes
- Developer notes captured for audit trail
- Status transitions: Any → Any (flexible workflow per edge case in spec)
- Service_ErrorHandler.ShowConfirmation for consistency

**Status Values**:
- "New" (default) → Red background (LightCoral)
- "Reviewed" → Yellow background (LightGoldenrodYellow)
- "Resolved" → Green background (LightGreen)

### 7. Performance Optimization Strategies

**Decision**: Implement paging if result set exceeds 1000 records

**Rationale**:
- Spec mentions 10,000 reports as edge case
- DataGridView performance degrades with >1000 rows
- Add TOP N / LIMIT to stored procedure with offset parameter
- Display "Showing X-Y of Z records" with navigation buttons
- Initial implementation without paging acceptable if dataset stays <1000

**Alternatives Considered**:
- Virtual scrolling: Rejected due to WinForms complexity
- Load all records: Acceptable for MVP, add paging if needed

## Key Patterns from Codebase Analysis

### DataGridView Pattern (from Control_AdvancedRemove)

```csharp
// Execute stored procedure
var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "procedureName",
    parameters,
    progressHelper);

if (!result.IsSuccess)
{
    Service_ErrorHandler.HandleException(
        result.Exception, 
        Enum_ErrorSeverity.Medium,
        message: result.ErrorMessage);
    return;
}

// Bind to grid
DataTable dt = result.Data ?? new DataTable();
myDataGridView.DataSource = dt;

// Configure columns
foreach (DataGridViewColumn column in myDataGridView.Columns)
{
    column.Visible = visibleColumns.Contains(column.Name);
}

// Color coding via event
myDataGridView.CellFormatting += (s, e) =>
{
    if (e.ColumnIndex == statusColumnIndex && e.Value != null)
    {
        string status = e.Value.ToString();
        e.CellStyle.BackColor = status switch
        {
            "New" => Color.LightCoral,
            "Reviewed" => Color.LightGoldenrodYellow,
            "Resolved" => Color.LightGreen,
            _ => Color.White
        };
    }
};
```

### Dao_ErrorReports Pattern

Existing DAO has region structure:
- Fields (none for static class)
- Database Operations
- Helpers

Follow same pattern when adding methods:
- GetAllErrorReportsAsync() → Database Operations region
- GetErrorReportByIdAsync() → Database Operations region
- UpdateErrorReportStatusAsync() → Database Operations region
- Export methods → New Helper_ErrorReportExport class

### Form Constructor Pattern

```csharp
public Form_ViewErrorReports()
{
    InitializeComponent();
    WireUpEvents();
    InitializeProgressHelper();
    LoadInitialData();
}
```

## Implementation Notes

1. **Grid Column Truncation**: Summary column shows first 100 characters with "..." ellipsis. Full text visible in detail panel.

2. **Null Safety**: All text fields from database may be NULL. Use `?? string.Empty` or `DBNull.Value` checks.

3. **Progress Reporting**: Use `Helper_StoredProcedureProgress` for operations expected to take >1 second (initial load, bulk export).

4. **Double-Click Handler**: Attach to DataGridView.CellDoubleClick event to show detail panel or dialog.

5. **Copy to Clipboard**: Use StringBuilder to format all fields, then `Clipboard.SetText()`.

6. **Status Column**: Must be read-only in grid. Changes only via button clicks with confirmation.

7. **Date Formatting**: Display as "yyyy-MM-dd HH:mm:ss" in grid, full DateTime in detail view.

8. **Search Behavior**: Apply filters clears previous selection. Search text case-insensitive.

## Dependencies Verified

✅ MySql.Data 9.4.0 - Already in project  
✅ ClosedXML - Referenced in documentation  
✅ Helper_Database_StoredProcedure - Exists with ExecuteDataTableWithStatusAsync  
✅ Service_ErrorHandler - Exists with HandleException and ShowConfirmation  
✅ Core_Themes - Exists with ApplyDpiScaling  
✅ Dao_ErrorReports - Exists, ready to extend  
✅ Model_ErrorReport_Core - Exists with all required fields

## Risk Assessment

**Low Risk**:
- DataGridView binding (proven pattern)
- Stored procedure approach (standard)
- Form/UserControl structure (existing conventions)

**Medium Risk**:
- Performance with 10,000+ records (mitigated by paging plan)
- Complex filtering logic in stored procedure (testable independently)

**High Risk**: None identified

## Next Steps

Proceed to Phase 1: Generate data-model.md and contracts/
