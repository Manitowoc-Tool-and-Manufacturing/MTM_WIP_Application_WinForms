# Quickstart Guide: View Error Reports Window

**Feature**: View Error Reports Window  
**For**: Developers implementing the feature  
**Created**: 2025-10-25

---

## 1. Prerequisites

Before starting implementation, ensure you have:

-   ✅ Read [spec.md](./spec.md) - Complete feature specification
-   ✅ Reviewed [plan.md](./plan.md) - Technical context and constitution compliance
-   ✅ Studied [research.md](./research.md) - Technology decisions and patterns
-   ✅ Understood [data-model.md](./data-model.md) - Entity definitions and relationships
-   ✅ Reviewed [contracts/](./contracts/) - All 5 stored procedure contracts
-   ✅ Access to Debug database: `mtm_wip_application_winforms_test`
-   ✅ Existing error_reports table with sample data (from error-reporting-system feature)
-   ✅ Visual Studio 2022 with .NET 8.0 SDK installed
-   ✅ WinForms designer familiarity

---

## 2. Implementation Order

Follow this sequence to build the feature incrementally:

### Phase A: Database Layer (Day 1)

**Goal**: Create stored procedures and test in MySQL Workbench

1. **Create Stored Procedures** (in Database/UpdatedStoredProcedures/ReadyForVerification/)

    - [ ] `sp_error_reports_GetAll.sql` - Filter and retrieve reports
    - [ ] `sp_error_reports_GetByID.sql` - Get single report details
    - [ ] `sp_error_reports_UpdateStatus.sql` - Change status with notes
    - [ ] `sp_error_reports_GetUserList.sql` - Populate user filter dropdown
    - [ ] `sp_error_reports_GetMachineList.sql` - Populate machine filter dropdown

2. **Test Each Stored Procedure** (MySQL Workbench)

    ```sql
    -- Test GetAll with filters
    CALL sp_error_reports_GetAll(
        '2025-10-18 00:00:00',
        '2025-10-25 23:59:59',
        NULL, NULL, 'New', NULL,
        @status, @errMsg
    );
    SELECT @status, @errMsg;

    -- Test GetByID
    CALL sp_error_reports_GetByID(123, @status, @errMsg);
    SELECT @status, @errMsg;

    -- Test UpdateStatus
    CALL sp_error_reports_UpdateStatus(
        123, 'Reviewed', 'Investigating issue', 'Dev.Smith', NOW(),
        @status, @errMsg
    );
    SELECT @status, @errMsg;

    -- Test GetUserList
    CALL sp_error_reports_GetUserList(@status, @errMsg);
    SELECT @status, @errMsg;

    -- Test GetMachineList
    CALL sp_error_reports_GetMachineList(@status, @errMsg);
    SELECT @status, @errMsg;
    ```

3. **Reference**: See [contracts/](./contracts/) for exact parameter definitions and expected outputs

---

### Phase B: Data Access Layer (Day 1-2)

**Goal**: Extend Dao_ErrorReports with new methods

1. **Create Model_ErrorReport_Core_Filter.cs** (in Models/)
   ```csharp
   namespace MTM_WIP_Application_Winforms.Models;
   
   public class Model_ErrorReport_Core_Filter
   {
       public DateTime? DateFrom { get; set; }
       public DateTime? DateTo { get; set; }
       public string? UserName { get; set; }
       public string? MachineName { get; set; }
       public string? Status { get; set; }
       public string? SearchText { get; set; }
   }
   ```

2. **Extend Data/Dao_ErrorReports.cs** (add to #region Database Operations)
   - [ ] `GetAllErrorReportsAsync(Model_ErrorReport_Core_Filter filter)` → `Model_Dao_Result<DataTable>`
   - [ ] `GetErrorReportByIdAsync(int reportId)` → `Model_Dao_Result<Model_ErrorReport_Core>`
   - [ ] `UpdateErrorReportStatusAsync(int reportId, string newStatus, string notes, string reviewedBy)` → `Model_Dao_Result<bool>`
   - [ ] `GetUserListAsync()` → `Model_Dao_Result<List<string>>`
   - [ ] `GetMachineListAsync()` → `Model_Dao_Result<List<string>>`

3. **Test Each DAO Method** (Debug console or test project)
   ```csharp
   // Quick manual test in Program.cs or test console
   var filter = new Model_ErrorReport_Core_Filter { Status = "New" };
   var result = await Dao_ErrorReports.GetAllErrorReportsAsync(filter);
   if (result.IsSuccess)
       Console.WriteLine($"Retrieved {result.Data.Rows.Count} reports");
   ```

    ```csharp
    // Quick manual test in Program.cs or test console
    var filter = new Model_ErrorReportFilter { Status = "New" };
    var result = await Dao_ErrorReports.GetAllErrorReportsAsync(filter);
    if (result.IsSuccess)
        Console.WriteLine($"Retrieved {result.Data.Rows.Count} reports");
    ```

4. **Reference**: See [contracts/sp*error_reports*\*.md](./contracts/) for C# usage examples

---

### Phase C: Helper Classes (Day 2)

**Goal**: Create export functionality

1. **Create Helpers/Helper_ErrorReportExport.cs**

    - [ ] `ExportToCsvAsync(DataTable data, string filePath)` → `Task<bool>`
    - [ ] `ExportToExcelAsync(DataTable data, string filePath)` → `Task<bool>`
    - [ ] Use ClosedXML for Excel, StringBuilder for CSV
    - [ ] Handle UTF-8 encoding for special characters

2. **Test Export Methods**
    ```csharp
    var testData = await Dao_ErrorReports.GetAllErrorReportsAsync(null);
    bool success = await Helper_ErrorReportExport.ExportToCsvAsync(
        testData.Data,
        @"C:\Temp\test_export.csv"
    );
    ```

---

### Phase D: UI Controls (Day 3-4)

**Goal**: Build reusable UserControl components

1. **Create Controls/ErrorReports/Control_ErrorReportsGrid.cs**
   - [ ] DataGridView with columns: ID, Date, User, Machine, Type, Summary, Status
   - [ ] Color-coding via CellFormatting event (New=Red, Reviewed=Yellow, Resolved=Green)
   - [ ] Double-click handler to raise event for detail view
   - [ ] Column sorting enabled
   - [ ] Method: `LoadReportsAsync(Model_ErrorReport_Core_Filter filter)`
   - [ ] Event: `ReportSelected(int reportId)`

2. **Create Controls/ErrorReports/Control_ErrorReportDetails.cs**

    - [ ] Labels/TextBoxes for all report fields
    - [ ] Highlight UserNotes section (distinct border/background)
    - [ ] Expandable sections for CallStack and TechnicalDetails
    - [ ] Buttons: Mark as Reviewed, Mark as Resolved, Copy All, Export
    - [ ] Method: `LoadReportAsync(int reportId)`
    - [ ] Event: `StatusChanged()`

3. **Test Controls in Isolation**
    - Create temporary form to host each control
    - Verify data binding and events
    - Test color-coding and layout at different DPI settings

---

### Phase E: Main Form (Day 4-5)

**Goal**: Assemble components into complete window

1. **Create Forms/ErrorReports/Form_ViewErrorReports.cs**

    - [ ] SplitContainer with Orientation.Horizontal (60% grid / 40% detail)
    - [ ] Top panel: Filter controls (Date, User, Machine, Status, Search) + Apply/Clear buttons
    - [ ] Middle panel: Control_ErrorReportsGrid
    - [ ] Bottom panel: Control_ErrorReportDetails
    - [ ] Status label: "Showing X of Y reports"
    - [ ] Export buttons: Export to CSV, Export Selected

2. **Wire Up Events**

    - [ ] Grid ReportSelected → Load detail view
    - [ ] Detail StatusChanged → Refresh grid and detail
    - [ ] Apply Filters → Call LoadReportsAsync with filter
    - [ ] Clear Filters → Reset controls and load all reports
    - [ ] Export buttons → Call Helper_ErrorReportExport

3. **Region Organization** (per constitution)
    ```
    #region Fields
    #region Properties
    #region Constructors
    #region Database Operations (LoadReportsAsync)
    #region Button Clicks
    #region ComboBox & UI Events
    #region Helpers
    #region Cleanup
    ```

---

### Phase F: Integration & Testing (Day 5)

**Goal**: End-to-end manual validation

1. **Launch from MainForm** (add menu item or button)

    ```csharp
    // In MainForm.cs
    private void btnViewErrorReports_Click(object sender, EventArgs e)
    {
        var form = new Form_ViewErrorReports();
        form.Show();
    }
    ```

2. **Execute Manual Test Scenarios** (from spec.md)

    - [ ] User Story 1: Browse All Error Reports
    - [ ] User Story 2: Filter and Search Reports
    - [ ] User Story 3: View Details and Update Status
    - [ ] User Story 4: Bulk Export Reports
    - [ ] Edge Cases: Large result sets, long text fields, concurrent updates

3. **Verify Success Criteria** (from spec.md)
    - [ ] SC-001: 100 reports load in < 1s
    - [ ] SC-002: Filtering 1000→50 in < 500ms
    - [ ] SC-003: Status update in < 300ms
    - [ ] SC-004: Export 500 reports in < 2s
    - [ ] SC-005: Search returns in < 400ms
    - [ ] SC-006: Color-coding 100% correct
    - [ ] SC-007: Detail view displays 10KB callstack without lag

---

## 3. Key Patterns to Follow

### DataGridView Binding

```csharp
private async Task LoadReportsAsync(Model_ErrorReport_Core_Filter filter)
{
    try
    {
        _progressHelper?.ShowProgress("Loading error reports...");

        var result = await Dao_ErrorReports.GetAllErrorReportsAsync(filter);

        if (!result.IsSuccess)
        {
            Service_ErrorHandler.HandleException(
                result.Exception, 
                Enum_ErrorSeverity.Medium,
                message: result.StatusMessage);
            return;
        }

        DataTable reports = result.Data ?? new DataTable();
        dgvReports.DataSource = reports;

        // Configure columns
        ConfigureGridColumns();

        // Update status label
        lblStatus.Text = $"Showing {reports.Rows.Count} reports";

        _progressHelper?.ShowSuccess($"Loaded {reports.Rows.Count} reports");
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
    }
}
```

### Color-Coding Event

```csharp
private void dgvReports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
{
    if (e.ColumnIndex == statusColumnIndex && e.Value != null)
    {
        string status = e.Value.ToString() ?? string.Empty;
        e.CellStyle.BackColor = status switch
        {
            "New" => Color.LightCoral,
            "Reviewed" => Color.LightGoldenrodYellow,
            "Resolved" => Color.LightGreen,
            _ => Color.White
        };
    }
}
```

### Status Update with Confirmation

```csharp
private async void btnMarkReviewed_Click(object sender, EventArgs e)
{
    string notes = txtDeveloperNotes.Text.Trim();

    var confirmation = Service_ErrorHandler.ShowConfirmation(
        "Mark this report as Reviewed?",
        "Confirm Status Change");

    if (confirmation != DialogResult.OK)
        return;

    var result = await Dao_ErrorReports.UpdateErrorReportStatusAsync(
        _currentReportId,
        "Reviewed",
        notes,
        Model_Application_Variables.CurrentUser.UserName
    );

    if (result.IsSuccess)
    {
        await RefreshCurrentReport();
        StatusChanged?.Invoke(); // Notify grid to refresh
    }
    else
    {
        Service_ErrorHandler.HandleException(
            result.Exception, 
            Enum_ErrorSeverity.Medium,
            message: result.StatusMessage);
    }
}
```

---

## 4. Common Pitfalls

### ❌ Don't Do This

```csharp
// Direct MessageBox usage
MessageBox.Show("Error loading reports");

// Synchronous database call on UI thread
var result = Dao_ErrorReports.GetAllErrorReportsAsync(filter).Result;

// Inline SQL
string sql = "SELECT * FROM error_reports WHERE Status = 'New'";

// No error handling
var result = await Dao_ErrorReports.GetAllErrorReportsAsync(filter);
dgvReports.DataSource = result.Data; // Might be null!
```

### ✅ Do This Instead

```csharp
// Service_ErrorHandler
Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);

// Async all the way
var result = await Dao_ErrorReports.GetAllErrorReportsAsync(filter);

// Stored procedure via Helper
Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(...);

// Null-safe error handling
if (!result.IsSuccess || result.Data == null)
{
    Service_ErrorHandler.HandleException(...);
    return;
}
```

---

## 5. Testing Checklist

Before marking feature complete:

- [ ] All 5 stored procedures created and tested in MySQL Workbench
- [ ] All 5 DAO methods implemented with Model_Dao_Result pattern
- [ ] Export helper class created and tested (CSV + Excel)
- [ ] Grid UserControl displays data with color-coding
- [ ] Detail UserControl loads complete report
- [ ] Main form integrates all components
- [ ] All filter combinations work correctly
- [ ] Status update workflow functions end-to-end
- [ ] Export to CSV/Excel produces valid files
- [ ] Manual validation scenarios pass (spec.md User Stories 1-4)
- [ ] Success criteria met (SC-001 through SC-007)
- [ ] No MessageBox.Show calls (use Service_ErrorHandler)
- [ ] Region organization follows constitution
- [ ] XML documentation on all public DAO methods
- [ ] Core_Themes.ApplyDpiScaling called in constructors

---

## 6. Troubleshooting

### Grid Not Loading Data

1. Check stored procedure works in MySQL Workbench
2. Verify DAO method returns IsSuccess=true
3. Confirm DataTable has rows: `result.Data.Rows.Count`
4. Check DataGridView.DataSource assignment
5. Use LoggingUtility.Log to trace execution

### Color-Coding Not Appearing

1. Verify CellFormatting event is wired up
2. Check Status column index is correct
3. Confirm Status values exactly match: "New", "Reviewed", "Resolved"
4. Use debugger to step through event handler

### Status Update Fails

1. Verify stored procedure parameters match C# call
2. Check ReviewedBy is not empty string
3. Confirm ReportID exists in database
4. Review stored procedure p_Status output value

### Export Fails

1. Check file path is writable
2. Verify DataTable is not null or empty
3. Confirm file extension matches format (.csv or .xlsx)
4. Test with small dataset first (10 rows)

---

## 7. Resources

### Documentation

-   [spec.md](./spec.md) - Complete feature specification
-   [plan.md](./plan.md) - Implementation plan
-   [research.md](./research.md) - Technology decisions
-   [data-model.md](./data-model.md) - Entity definitions
-   [contracts/](./contracts/) - Stored procedure contracts

### Instruction Files

-   `.github/instructions/csharp-dotnet8.instructions.md` - C# patterns, async, regions
-   `.github/instructions/mysql-database.instructions.md` - Stored procedures, Helper usage
-   `.github/instructions/testing-standards.instructions.md` - Manual validation approach

### Existing Code Patterns

-   `Data/Dao_ErrorReports.cs` - Current DAO structure (extend this)
-   `Controls/MainForm/Control_AdvancedRemove.cs` - DataGridView binding example
-   `Controls/Shared/ColumnOrderDialog.cs` - DataGridView manipulation
-   `Helpers/Helper_Database_StoredProcedure.cs` - ExecuteDataTableWithStatusAsync

---

## 8. Estimated Timeline

-   **Day 1**: Database layer (stored procedures) - 4 hours
-   **Day 2**: Data access layer (DAO methods) + Helper - 4 hours
-   **Day 3**: UI controls (Grid + Detail) - 6 hours
-   **Day 4**: Main form assembly - 4 hours
-   **Day 5**: Integration, testing, polish - 6 hours

**Total**: ~24 hours (3 full days or 5 half-days)

---

## 9. Next Steps

After completing implementation:

1. Run `/speckit.tasks` to generate tasks.md with detailed checklist
2. Commit changes to branch `002-view-error-reports`
3. Generate comprehensive test report documenting manual validation results
4. Create pull request with constitution compliance verification
5. Code review focusing on stored procedure usage, region organization, async patterns

---

**Questions?** Review [spec.md](./spec.md) edge cases and [research.md](./research.md) implementation notes first. If still unclear, clarify with project lead before implementing.
