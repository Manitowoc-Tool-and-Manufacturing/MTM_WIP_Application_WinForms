# Research: View Error Reports Window

**Feature**: 002-view-error-reports  
**Created**: 2025-10-25  
**Status**: Complete

---

## Research Questions

### Q1: DataGridView Performance with Large Datasets

**Question**: How should we handle displaying 10,000+ error reports without UI lag?

**Decision**: Implement server-side pagination with 100 records per page default

**Rationale**:
- DataGridView performs poorly with >1000 rows loaded simultaneously
- Server-side pagination keeps memory footprint low
- Stored procedure already supports `@p_Page` and `@p_PageSize` parameters
- Network transfer reduced to ~10KB per page vs. loading full dataset

**Alternatives Considered**:
- Virtual mode DataGridView: Complex state management, harder to debug
- Client-side filtering: Still requires loading full dataset, memory issues persist
- Lazy loading: Difficult UX with scrolling, unpredictable behavior

**Implementation Notes**:
- Add pagination controls (Previous, Next, Page X of Y)
- Show "Showing 1-100 of 10,243 reports" label
- Persist page size preference in user settings
- Jump to page dropdown for quick navigation

---

### Q2: Stored Procedure Dependencies

**Question**: Do required stored procedures exist from Feature 001?

**Decision**: Reuse `sp_error_reports_*` procedures from 001-error-reporting-with

**Rationale**:
- Feature 001 already implemented complete error_reports table schema
- Stored procedures follow `sp_error_reports_GetAll`, `sp_error_reports_UpdateStatus` pattern
- No new procedures needed for basic viewing/filtering
- Status update logic already implemented with audit trail

**Available Procedures** (from 001):
- `sp_error_reports_GetAll` - Supports filtering by date, user, status, machine
- `sp_error_reports_GetByID` - Retrieve single report details
- `sp_error_reports_UpdateStatus` - Change status, add developer notes
- `sp_error_reports_GetUserList` - Populate user filter dropdown  
- `sp_error_reports_GetMachineList` - Populate machine filter dropdown

**Verification**: Confirmed in `Database/UpdatedStoredProcedures/ReadyForVerification/`

---

### Q3: Export Format Selection

**Question**: Should export support CSV, Excel, or both?

**Decision**: Support both CSV (ClosedXML) and Excel (.xlsx) formats

**Rationale**:
- CSV: Lightweight, universal compatibility, easy parsing in scripts
- Excel: Formatted columns, clickable links, better for management reports
- ClosedXML library already in project dependencies from existing export features
- Minimal additional code to support both formats

**Implementation Notes**:
- Export button opens submenu: "Export as CSV" / "Export as Excel"
- CSV: Use `System.IO.StreamWriter` with comma delimiter, quote strings
- Excel: Use `ClosedXML.Excel.XLWorkbook`, apply column formatting
- Include filters applied in export filename: `ErrorReports_New_2025-10-25.xlsx`

---

### Q4: Detail View Implementation

**Question**: Should detail view be side panel or modal dialog?

**Decision**: Bottom detail panel (40% height) as specified in UI Option A

**Rationale**:
- Spec explicitly selected "Option A: Master-Detail Horizontal Split"
- Panel keeps context visible while showing details
- No modal window management complexity
- Consistent with existing MTM WinForms patterns (Transactions form uses similar layout)
- Allows quick switching between reports without closing dialogs

**Implementation Notes**:
- Use SplitContainer control with Orientation=Horizontal
- Top panel: DataGridView (60% height, resizable)
- Bottom panel: Detail controls in GroupBox
- SelectionChanged event populates detail panel
- Include collapsible sections for long text (Technical Details, Call Stack)

---

### Q5: Color-Coding Implementation

**Question**: How to implement status color-coding that respects themes?

**Decision**: Use DataGridView row BackColor with light pastel colors

**Rationale**:
- Spec requires: New=LightCoral, Reviewed=LightGoldenrodYellow, Resolved=LightGreen
- WinForms DataGridView supports row-level BackColor property
- Light colors remain readable with dark text in all themes
- Core_Themes utility can adjust colors for theme compatibility if needed

**Implementation Notes**:
```csharp
private void ApplyRowColoring(DataGridViewRow row, string status)
{
    row.DefaultCellStyle.BackColor = status switch
    {
        "New" => Color.LightCoral,
        "Reviewed" => Color.LightGoldenrodYellow,
        "Resolved" => Color.LightGreen,
        _ => Color.White
    };
}
```

---

### Q6: Filter Persistence

**Question**: Should applied filters persist between window opens?

**Decision**: Yes, save filter state to user preferences

**Rationale**:
- Developers typically work on same issues across multiple sessions
- Reduces repetitive filter re-application
- Improves workflow efficiency for common debugging scenarios
- User preferences infrastructure already exists in project

**Implementation Notes**:
- Save to Model_UserUiColors or create Model_UserPreferences extension
- Persist: DateRange, SelectedUsers[], SelectedStatuses[], SearchText
- Restore on form load if preferences exist
- Add "Reset to Defaults" button to clear saved filters

---

## Technology Stack Confirmation

### Database Layer
- MySQL 5.7.24 (MAMP compatible)
- MySql.Data 9.4.0 connector
- Stored procedures via `Helper_Database_StoredProcedure`

### WinForms Components
- DataGridView (built-in, grid display)
- SplitContainer (layout management)
- DateTimePicker (date range filters)
- ComboBox (dropdowns for user/status/machine)
- TextBox (search input)

### Export Libraries
- ClosedXML (Excel .xlsx generation)
- System.IO.StreamWriter (CSV export)

### Existing Patterns
- Dao_ErrorReports (already exists from Feature 001)
- Model_ErrorReport (POCO model)
- DaoResult<T> pattern for data access
- Service_ErrorHandler for user-facing errors
- Core_Themes for DPI/theming

---

## Architecture Decisions

### Form Structure
```
ViewErrorReportsForm : Form
├── FilterPanel (GroupBox) - Top 15%
│   ├── DateRangePickers
│   ├── ComboBoxes (User, Status, Machine)
│   ├── SearchTextBox
│   └── Apply/Clear Buttons
├── SplitContainer (85%)
│   ├── Panel1: DataGridView (60%)
│   └── Panel2: DetailPanel (40%)
└── StatusStrip (Progress + Count Label)
```

### Data Flow
```
User Action → FilterPanel
          ↓
    Build Filter Criteria
          ↓
    Dao_ErrorReports.GetFilteredReportsAsync()
          ↓
    Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync()
          ↓
    sp_error_reports_GetAll (MySQL)
          ↓
    DataTable → List<Model_ErrorReport>
          ↓
    Bind to DataGridView
          ↓
    Apply Row Coloring
```

### State Management
- Current page number
- Page size (default 100)
- Applied filters (FilterCriteria object)
- Selected report (for detail panel)
- Sort column and direction

---

## Performance Targets

- Initial load: < 1 second for 100 reports
- Filter application: < 500ms for 1000 reports
- Pagination navigation: < 200ms per page
- Export 500 reports: < 2 seconds (CSV), < 4 seconds (Excel)
- Status update: < 300ms including grid refresh
- Search: < 400ms for text search across 1000 reports

---

## Security Considerations

- All database queries via parameterized stored procedures (SQL injection protected)
- No inline SQL permitted per Constitution Principle I
- Developer notes logged with timestamp and username for audit trail
- Export permissions checked (admin-only configuration option available)
- No sensitive data in exported files (validated in Dao layer)

---

## Dependencies

### From Feature 001
- ✅ error_reports table schema
- ✅ Dao_ErrorReports class
- ✅ Model_ErrorReport POCO
- ✅ All `sp_error_reports_*` stored procedures
- ✅ Service_ErrorHandler integration

### Existing Infrastructure
- ✅ Helper_Database_StoredProcedure
- ✅ Helper_Database_Variables
- ✅ Core_Themes (DPI scaling)
- ✅ LoggingUtility
- ✅ ClosedXML library

---

## Open Questions

None - all research questions resolved.

---

## Next Steps

1. Create data-model.md with entity relationships
2. Generate plan.md with implementation architecture
3. Create contracts/ directory with DAO interface definitions
4. Generate quickstart.md for development workflow
5. Run agent context update to add View Error Reports to AGENTS.md
