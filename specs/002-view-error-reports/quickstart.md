# Quickstart: View Error Reports Window Development

**Feature**: 002-view-error-reports  
**Created**: 2025-10-25  
**Target Branch**: `002-view-error-reports`

---

## Prerequisites

✅ Feature 001 (Error Reporting System) must be complete:
- `error_reports` table exists in database
- `Dao_ErrorReports` base class implemented
- All `sp_error_reports_*` stored procedures deployed
- `Model_ErrorReport` POCO defined

## Development Setup

### 1. Clone and Switch Branch

```powershell
git clone https://github.com/Dorotel/MTM_WIP_Application_WinForms.git
cd MTM_WIP_Application_WinForms
git checkout 002-view-error-reports
```

### 2. Build Project

```powershell
dotnet restore
dotnet build MTM_Inventory_Application.csproj -c Debug
```

### 3. Verify Database Connection

```powershell
# Test database: mtm_wip_application_winforms_test (Debug mode)
# Connection string in Helper_Database_Variables.GetConnectionString()
```

---

## Implementation Order

### Phase 1: Core DAO Extensions (Est: 2 hours)

**Location**: `Data/Dao_ErrorReports.cs`

**Tasks**:
1. Add `GetFilteredReportsAsync()` method
2. Add `GetUserListAsync()` and `GetMachineListAsync()` methods
3. Add caching for dropdown lists (5-minute cache)
4. Add `UpdateReportStatusAsync()` method
5. Unit test all methods with sample data

**Files to Modify**:
- `Data/Dao_ErrorReports.cs` (extend existing class)

**Files to Create**:
- `Models/ErrorReportFilterCriteria.cs`
- `Models/PaginatedResult.cs`

**Test**:
```csharp
var criteria = new ErrorReportFilterCriteria { PageSize = 10 };
var result = await Dao_ErrorReports.GetFilteredReportsAsync(criteria);
Assert.IsTrue(result.IsSuccess);
Assert.IsTrue(result.Data.Items.Count <= 10);
```

---

### Phase 2: Form UI Layout (Est: 3 hours)

**Location**: `Forms/Development/ViewErrorReportsForm.cs`

**Tasks**:
1. Create Form with SplitContainer layout
2. Add filter panel controls (top 15%)
   - DateTimePickers for date range
   - ComboBoxes for User, Status, Machine
   - TextBox for search
   - Apply/Clear buttons
3. Add DataGridView (60% of split)
   - Configure columns
   - Enable sorting
4. Add detail panel (40% of split)
   - GroupBox with report details
   - Collapsible sections for long text
5. Add StatusStrip with progress bar and count label

**UI Structure**:
```
ViewErrorReportsForm
├── FilterPanel (GroupBox, Dock=Top, Height=80)
├── SplitContainer (Dock=Fill, Orientation=Horizontal)
│   ├── Panel1: dgvReports (DataGridView)
│   └── Panel2: DetailPanel (GroupBox)
└── StatusStrip (progress, countLabel)
```

**Test**: Run form, verify layout scales properly at 100%, 125%, 150% DPI

---

### Phase 3: Data Binding (Est: 2 hours)

**Location**: `Forms/Development/ViewErrorReportsForm.cs`

**Tasks**:
1. Implement `LoadReportsAsync()` method
2. Bind DataGridView to `BindingList<ErrorReportGridRow>`
3. Implement filter button click handlers
4. Implement pagination (Previous/Next/Page selector)
5. Apply row color-coding based on status
6. Wire up row selection to detail panel

**Key Methods**:
```csharp
private async Task LoadReportsAsync()
{
    var criteria = BuildFilterCriteria();
    var result = await Dao_ErrorReports.GetFilteredReportsAsync(criteria);
    if (result.IsSuccess)
    {
        dgvReports.DataSource = new BindingList<ErrorReportGridRow>(
            result.Data.Items.Select(MapToGridRow).ToList()
        );
        ApplyRowColoring();
    }
}
```

---

### Phase 4: Export Functionality (Est: 2 hours)

**Location**: `Data/Dao_ErrorReports.cs`

**Tasks**:
1. Implement `ExportToCSVAsync()` method
2. Implement `ExportToExcelAsync()` method (using ClosedXML)
3. Add export button click handlers in form
4. Add SaveFileDialog with filters
5. Show progress during export
6. Handle export errors gracefully

**Test**:
```powershell
# Export 100 test reports to CSV
# Verify: File created, UTF-8 encoding, all columns present, opens in Excel
```

---

### Phase 5: Status Update (Est: 1 hour)

**Location**: `Forms/Development/ViewErrorReportsForm.cs`

**Tasks**:
1. Add "Mark as Reviewed" button to detail panel
2. Add "Mark as Resolved" button
3. Create status update dialog for developer notes
4. Call `Dao_ErrorReports.UpdateReportStatusAsync()`
5. Refresh grid after status change
6. Show success message

**Workflow**:
```
User clicks "Mark as Reviewed"
    → Dialog prompts for developer notes
    → UpdateReportStatusAsync() called
    → Grid refreshes with updated row color
    → Success message displayed
```

---

## Testing Checklist

### Manual Testing

- [ ] Form loads without errors
- [ ] Filter controls populate from database (users, machines)
- [ ] Date range filter works correctly
- [ ] User filter (single and multiple selection)
- [ ] Status filter (checkboxes)
- [ ] Machine filter works
- [ ] Search text filters across Summary, UserNotes, TechnicalDetails
- [ ] Pagination controls work (Previous/Next/Jump to page)
- [ ] Grid sorts by clicking column headers
- [ ] Row color-coding correct (New=red, Reviewed=yellow, Resolved=green)
- [ ] Double-click opens detail view
- [ ] Detail panel shows all fields correctly
- [ ] "Mark as Reviewed" button updates status
- [ ] "Mark as Resolved" button updates status
- [ ] Export to CSV creates valid file
- [ ] Export to Excel creates formatted workbook
- [ ] Export respects current filters
- [ ] "Export Selected" only exports selected rows
- [ ] Form scales correctly at 125%, 150% DPI
- [ ] Progress bar shows during long operations
- [ ] Error messages display via Service_ErrorHandler

### Performance Testing

- [ ] Loads 100 reports in < 1 second
- [ ] Filters 1000 reports in < 500ms
- [ ] Pagination navigation in < 200ms
- [ ] Export 500 reports to CSV in < 2 seconds
- [ ] Export 500 reports to Excel in < 4 seconds
- [ ] Status update completes in < 300ms

---

## Common Issues & Solutions

### Issue: Grid doesn't refresh after status update

**Solution**: Reload current page after update:
```csharp
await UpdateReportStatusAsync(reportId, newStatus, notes);
await LoadReportsAsync(); // Refresh grid
```

### Issue: Export fails with large datasets

**Solution**: Add row limit validation:
```csharp
if (totalRecords > 50000)
{
    Service_ErrorHandler.HandleValidationError(
        "Dataset too large. Apply more filters.",
        "Export Validation"
    );
    return;
}
```

### Issue: Date range filter returns no results

**Solution**: Ensure DateTo includes end of day:
```csharp
criteria.DateTo = datePickerTo.Value.Date.AddDays(1).AddSeconds(-1);
```

---

## Resources

- **Spec**: `specs/002-view-error-reports/spec.md`
- **Research**: `specs/002-view-error-reports/research.md`
- **Data Model**: `specs/002-view-error-reports/data-model.md`
- **DAO Contract**: `specs/002-view-error-reports/contracts/Dao_ErrorReports_Extensions.md`
- **Instruction Files**:
  - `.github/instructions/csharp-dotnet8.instructions.md`
  - `.github/instructions/mysql-database.instructions.md`
  - `.github/instructions/testing-standards.instructions.md`

---

## Next Steps After Completion

1. Run manual testing checklist
2. Verify all constitution principles met
3. Create PR with description from spec
4. Update AGENTS.md with Feature 002 summary
5. Mark feature complete in project tracking
