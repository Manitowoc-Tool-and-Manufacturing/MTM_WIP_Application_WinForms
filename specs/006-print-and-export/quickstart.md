# Quick Start Guide: Print and Export System

**Feature**: Print and Export System Refactor  
**Audience**: Developers implementing tasks from tasks.md  
**Last Updated**: 2025-11-08

---

## Overview

This guide provides quick-start instructions for implementing the print and export system refactor. It assumes you have read the specification (spec.md) and are ready to begin implementation.

---

## Prerequisites

**Required Reading**:
- `spec.md` - Complete feature specification
- `plan.md` - Implementation plan and constitution compliance
- `data-model.md` - Entity definitions and validation rules
- `research.md` - Technical decisions and rationale

**Required Instruction Files**:
- `.github/instructions/csharp-dotnet8.instructions.md` - C# patterns, async/await, region organization
- `.github/instructions/ui-compliance/theming-compliance.instructions.md` - Theme integration, DPI scaling, UI architecture
- `.github/instructions/winforms-responsive-layout.instructions.md` - Layout patterns, TableLayoutPanel usage
- `.github/instructions/documentation.instructions.md` - XML comments, code documentation

**Development Environment**:
- Visual Studio 2022 or VS Code with C# extensions
- .NET 8.0 SDK installed
- MySQL 5.7+ with `mtm_wip_application_winforms_test` database (Debug builds)
- MAMP or standalone MySQL server running

---

## Implementation Flow

Follow phases sequentially. Do NOT skip Phase 1 removal before implementing Phase 2.

### Phase 1: Systematic Removal (Days 1-2)

**Goal**: Remove old print system cleanly without breaking compilation.

**Steps**:

1. **Identify removal targets** (from FR-001):
   ```
   Old files to DELETE:
   - Helpers/Helper_PrintExport.cs
   - Core/Core_DgvPrinter.cs
   - Forms/Shared/PrintForm.cs (old version)
   - Forms/Shared/PrintForm.Designer.cs (old version)
   - Any other files with "Print" in name that predate 2025-11-08
   ```

2. **Find usages** before deleting:
   ```powershell
   # Search for all references to old print classes
   grep -r "Helper_PrintExport" --include="*.cs"
   grep -r "Core_DgvPrinter" --include="*.cs"
   grep -r "PrintForm" --include="*.cs"
   ```

3. **Replace with temporary message** (FR-002):
   ```csharp
   // In Control_RemoveTab.cs (or wherever Print button exists)
   private void btnPrint_Click(object sender, EventArgs e)
   {
       Service_ErrorHandler.ShowInformation(
           "Print functionality is being rebuilt. Coming soon!",
           "Feature Temporarily Unavailable"
       );
   }
   ```

4. **Delete old files** via Git:
   ```powershell
   git rm Helpers/Helper_PrintExport.cs
   git rm Core/Core_DgvPrinter.cs
   git rm Forms/Shared/PrintForm.cs
   git rm Forms/Shared/PrintForm.Designer.cs
   git commit -m "Phase 1: Remove old print system"
   ```

5. **Verify compilation**:
   ```powershell
   dotnet build MTM_WIP_Application_Winforms.csproj -c Debug
   ```

**Success Criteria**: Application compiles, Print buttons show temporary message, no old print code remains.

---

### Phase 2: Core Infrastructure (Days 3-5)

**Goal**: Implement Core_TablePrinter with accurate pagination and Helper_PrintManager orchestration.

**Implementation Order**:

#### 2A. Create Model Classes

```csharp
// Models/Model_PrintJob.cs
namespace MTM.Models
{
    /// <summary>
    /// Represents a complete print or export operation.
    /// See data-model.md for full documentation.
    /// </summary>
    public class Model_PrintJob
    {
        public DataTable SourceData { get; set; }
        public string Title { get; set; }
        public List<string> VisibleColumns { get; set; }
        public List<string> ColumnOrder { get; set; }
        public string PrinterName { get; set; }
        public PageOrientation Orientation { get; set; }
        public PrintColorMode ColorMode { get; set; }
        public PrintPageRange PageRange { get; set; }
        public int? FromPage { get; set; }
        public int? ToPage { get; set; }
        public int TotalPages { get; set; }
    }
}
```

```csharp
// Models/Model_PrintSettings.cs
namespace MTM.Models
{
    /// <summary>
    /// User preferences for print operations, persisted per-grid.
    /// See data-model.md for persistence rules.
    /// </summary>
    public class Model_PrintSettings
    {
        public string GridName { get; set; }
        public string PrinterName { get; set; }
        public List<string> VisibleColumns { get; set; }
        public List<string> ColumnOrder { get; set; }
        public DateTime LastModified { get; set; }
        
        // Methods
        public static Model_PrintSettings Load(string gridName) { /* See data-model.md */ }
        public void Save() { /* Saves to %APPDATA%\MTM\PrintSettings\{GridName}.json */ }
    }
}
```

#### 2B. Implement Core_TablePrinter

**Key Requirements**:
- PageBoundary tracking during rendering (see research.md R1)
- No row-based estimation (31 rows/page hardcoded value removed)
- Proper PrintDocument.PrintPage event handling

```csharp
// Core/Core_TablePrinter.cs

#region Fields
private DataTable _sourceData;
private List<string> _visibleColumns;
private List<string> _columnOrder;
private List<PageBoundary> _pageBoundaries;
private int _currentRowIndex;
#endregion

#region Rendering Methods
/// <summary>
/// Renders DataTable to PrintDocument with exact page boundaries.
/// See research.md R1 for Windows print system integration.
/// </summary>
public PrintDocument CreatePrintDocument(Model_PrintJob job)
{
    var doc = new PrintDocument();
    doc.PrintPage += OnPrintPage;
    // Configure from job settings
    return doc;
}

private void OnPrintPage(object sender, PrintPageEventArgs e)
{
    // Track PageBoundary for each page
    // Set e.HasMorePages = true when data remains
    // See data-model.md PageBoundary section
}
#endregion
```

**Testing Checklist**:
- [ ] 100-row dataset generates correct page count (FR-012)
- [ ] PageBoundary.StartRow/EndRow indices are accurate
- [ ] No hardcoded "31 rows per page" values remain
- [ ] Portrait and Landscape orientations render correctly

#### 2C. Implement Helper_PrintManager

**Responsibilities**:
- Create PrintJob from DataGridView
- Orchestrate preview generation
- Handle async with CancellationToken

```csharp
// Helpers/Helper_PrintManager.cs

#region Public Methods
/// <summary>
/// Shows print dialog for the specified DataGridView.
/// See spec.md User Story US-001 for workflow.
/// </summary>
public static async Task ShowPrintDialogAsync(DataGridView grid)
{
    var printJob = CreatePrintJob(grid);
    var settings = Model_PrintSettings.Load(grid.Name);
    
    using var dialog = new PrintForm(printJob, settings);
    dialog.ShowDialog();
}
#endregion

#region Helper Methods
private static Model_PrintJob CreatePrintJob(DataGridView grid)
{
    var dt = GetDataTableFromGrid(grid); // See research.md R6
    return new Model_PrintJob
    {
        SourceData = dt,
        Title = $"{grid.Name} - {DateTime.Now:yyyy-MM-dd}",
        VisibleColumns = GetVisibleColumns(grid),
        ColumnOrder = GetVisibleColumns(grid)
    };
}
#endregion
```

**Success Criteria**: SC-001 "Preview generation < 2 seconds for 100 rows" must pass.

---

### Phase 3: Print Dialog UI (Days 6-8)

**Goal**: Implement Compact Sidebar print dialog with theme integration.

**WinForms Designer Steps**:

1. **Create PrintForm** (right-click Forms/Shared → Add → Form → Windows Form):
   - Name: `PrintForm.cs`
   - Base class: `Form`

2. **Apply Mockup 3 Compact Sidebar layout**:
   - Root container: `PrintForm_Panel_Main` (Dock.Fill, AutoSize = true)
   - Master layout: `PrintForm_TableLayout_Master` (2 columns: 20%, 80%)
   - Sidebar: `PrintForm_Panel_Sidebar` (contains collapsible sections)
   - Preview: `PrintForm_Panel_PreviewViewport` (contains PrintPreviewControl)

3. **Sidebar Sections** (collapsible panels with icons per mockup):
   - 🖨️ Printer Settings (printer dropdown, orientation radio buttons)
   - 📄 Page Settings (page range radio buttons, from/to textboxes)
   - 📑 Column Settings (CheckedListBox + Up/Down arrows)
   - ⚙️ Options (color mode, zoom level dropdown)

4. **Action Bar** (bottom of sidebar):
   - Print button (primary action, theme AccentColor)
   - Export dropdown button (PDF/Excel options)
   - Cancel button (close dialog)

**Theme Integration** (MANDATORY per Constitution Principle IX):

```csharp
// Forms/Shared/PrintForm.cs

public PrintForm(Model_PrintJob job, Model_PrintSettings settings)
{
    InitializeComponent();
    
    // MANDATORY: Theme integration
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    // Custom initialization
    _printJob = job;
    _settings = settings;
    InitializePreview();
    LoadSettings();
    ApplyThemeColors();
}

private void ApplyThemeColors()
{
    var colors = Model_AppVariables.UserUiColors;
    
    // Sidebar panels
    PrintForm_Panel_Sidebar.BackColor = colors.PanelBackColor ?? SystemColors.Control;
    
    // Preview viewport (gray per mockup)
    PrintForm_Panel_PreviewViewport.BackColor = Color.FromArgb(149, 165, 166);
    
    // Buttons
    PrintForm_Button_Print.BackColor = colors.AccentColor ?? SystemColors.Highlight;
    PrintForm_Button_Print.ForeColor = Color.White;
    
    PrintForm_Button_Export.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
    PrintForm_Button_Cancel.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
}
```

**Control Naming** (NO abbreviations):
- `PrintForm_ComboBox_Printer` (NOT `cboPrinter`)
- `PrintForm_RadioButton_Portrait` (NOT `rbPortrait`)
- `PrintForm_CheckedListBox_Columns` (NOT `clbColumns`)

**Testing Checklist**:
- [ ] All controls have `{ComponentName}_{ControlType}_{Purpose}` names
- [ ] AutoSize cascade from root Panel to leaf controls
- [ ] Leaf controls (ComboBox, TextBox) have MinimumSize/MaximumSize set
- [ ] No container widths exceed 1000px
- [ ] Core_Themes.ApplyDpiScaling called in constructor
- [ ] Theme colors applied from Model_UserUiColors

**Success Criteria**: SC-007 "UI functions correctly at 100%-200% DPI" must pass.

---

### Phase 4: PDF and Excel Export (Days 9-10)

**Goal**: Implement unified export system with exact page range support.

#### 4A. Create Helper_ExportManager

```csharp
// Helpers/Helper_ExportManager.cs

#region Export Methods
/// <summary>
/// Exports print job to PDF using Microsoft Print to PDF.
/// See research.md R1 for PrintRange.SomePages usage.
/// </summary>
public static async Task<DaoResult> ExportToPdfAsync(
    Model_PrintJob job,
    List<PageBoundary> boundaries,
    string filePath,
    CancellationToken ct)
{
    // Set job.PrinterName = "Microsoft Print to PDF"
    // Configure FromPage/ToPage from user selection
    // PrintDocument.Print() outputs to filePath
}

/// <summary>
/// Exports print job to Excel using ClosedXML with exact page boundaries.
/// See research.md R2 and data-model.md PageBoundary section.
/// </summary>
public static async Task<DaoResult> ExportToExcelAsync(
    Model_PrintJob job,
    List<PageBoundary> boundaries,
    string filePath,
    CancellationToken ct)
{
    // Filter boundaries by FromPage/ToPage
    // Extract rows using boundary.StartRow/EndRow
    // Write to Excel with ClosedXML
    // See data-model.md "Usage in Excel Export" example
}
#endregion
```

**Key Implementation Details**:

1. **PDF Export** (research.md R1):
   - Set `job.PrinterName = "Microsoft Print to PDF"`
   - Configure `PrintDocument.PrinterSettings.PrintToFile = true`
   - Set `PrinterSettings.PrintFileName = filePath`
   - Windows handles page filtering via FromPage/ToPage

2. **Excel Export** (research.md R2):
   ```csharp
   var selectedBoundaries = boundaries
       .Where(pb => pb.PageNumber >= job.FromPage && pb.PageNumber <= job.ToPage)
       .ToList();
   
   var rowsToExport = new List<DataRow>();
   foreach (var boundary in selectedBoundaries)
   {
       for (int i = boundary.StartRow; i < boundary.EndRow; i++)
       {
           rowsToExport.Add(job.SourceData.Rows[i]);
       }
   }
   
   WriteToExcel(rowsToExport, job.VisibleColumns, job.ColumnOrder, filePath);
   ```

**Testing Checklist**:
- [ ] SC-003: PDF of pages 3-7 matches preview pages 3-7 exactly
- [ ] SC-004: Excel of pages 1-3 contains exactly same rows as preview pages 1-3
- [ ] Edge case: Last page partial rows handled correctly
- [ ] Edge case: Single page export works
- [ ] Error handling: Invalid file paths handled via Service_ErrorHandler

---

### Phase 5: Integration (Days 11-12)

**Goal**: Wire new system to existing UI entry points.

**Steps**:

1. **Update Print button in Control_RemoveTab.cs**:
   ```csharp
   // Replace temporary message with actual call
   private async void btnPrint_Click(object sender, EventArgs e)
   {
       await Helper_PrintManager.ShowPrintDialogAsync(dataGridViewRemove);
   }
   ```

2. **Add to other tabs** (if applicable):
   - Control_Transactions.cs (if has print functionality)
   - Control_Inventory.cs (future enhancement)

3. **Update help documentation**:
   - Add section to `Documentation/Help/printing.html`
   - Explain new UI, page range selection, export options

**Testing Checklist**:
- [ ] All existing Print buttons route to new system
- [ ] No references to old Helper_PrintExport remain
- [ ] Help documentation updated
- [ ] SC-010: Zero regressions in other features

**Success Criteria**: All 10 success criteria from spec.md must pass.

---

## Common Patterns

### Region Organization (Constitution Principle III)

All C# files MUST follow standard region order:

```csharp
#region Fields
private DataGridView _sourceGrid;
private Model_PrintJob _printJob;
private CancellationTokenSource _cts;
#endregion

#region Properties
public int TotalPages => _printJob?.TotalPages ?? 0;
#endregion

#region Progress Control Methods
public void SetProgressControls(ProgressBar bar, Label label)
{
    // If applicable
}
#endregion

#region Constructors
public PrintForm(Model_PrintJob job, Model_PrintSettings settings)
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    // Custom init
}
#endregion

#region Preview Generation
private async Task GeneratePreviewAsync() { }
#endregion

#region Button Clicks
private async void btnPrint_Click(object sender, EventArgs e) { }
private async void btnExport_Click(object sender, EventArgs e) { }
private void btnCancel_Click(object sender, EventArgs e) { }
#endregion

#region Helpers
private void ApplyThemeColors() { }
private void LoadSettings() { }
private void SaveSettings() { }
#endregion

#region Cleanup
protected override void Dispose(bool disposing)
{
    _cts?.Cancel();
    _cts?.Dispose();
    base.Dispose(disposing);
}
#endregion
```

### Error Handling Pattern

```csharp
try
{
    var result = await Helper_ExportManager.ExportToPdfAsync(job, boundaries, filePath, ct);
    
    if (result.IsSuccess)
    {
        Service_ErrorHandler.ShowInformation(
            $"PDF exported successfully to:\n{filePath}",
            "Export Complete"
        );
    }
    else
    {
        Service_ErrorHandler.HandleException(
            result.Exception,
            ErrorSeverity.Medium,
            contextData: new Dictionary<string, object>
            {
                ["FilePath"] = filePath,
                ["PageRange"] = $"{job.FromPage}-{job.ToPage}"
            }
        );
    }
}
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, ErrorSeverity.High);
}
```

### Async UI Operations

```csharp
private async void btnGeneratePreview_Click(object sender, EventArgs e)
{
    btnGeneratePreview.Enabled = false;
    btnPrint.Enabled = false;
    
    _cts = new CancellationTokenSource();
    
    try
    {
        using var progressDialog = new ProgressDialog("Generating preview...");
        progressDialog.Show(this);
        
        await Task.Run(() =>
        {
            var printer = new Core_TablePrinter();
            var doc = printer.CreatePrintDocument(_printJob);
            printPreviewControl.Document = doc;
            _printJob.TotalPages = printer.PageCount;
        }, _cts.Token);
        
        progressDialog.Close();
        UpdatePageRangeControls(); // Enable FromPage/ToPage inputs
    }
    catch (OperationCanceledException)
    {
        // User cancelled - clean up
    }
    finally
    {
        btnGeneratePreview.Enabled = true;
        btnPrint.Enabled = true;
        _cts?.Dispose();
    }
}
```

---

## Testing Workflow

### Manual Validation (per testing-standards.instructions.md)

**After each phase**:

1. **Build application**:
   ```powershell
   dotnet build MTM_WIP_Application_Winforms.csproj -c Debug
   ```

2. **Run application**:
   ```powershell
   dotnet run --project MTM_WIP_Application_Winforms.csproj
   ```

3. **Exercise workflows**:
   - Navigate to Remove tab
   - Click Print button
   - Verify expected behavior per phase

4. **Document results** in feature branch commit messages or spec annotations.

### Success Criteria Validation

Track progress using checklist from spec.md:

```
SC-001: Preview generation < 2 seconds for 100 rows → Test with TransactionHistory dataset
SC-002: Page count 100% accurate → Compare preview.PageCount with actual print job
SC-003: PDF matches preview exactly → Print pages 3-7, verify row content
SC-004: Excel matches preview exactly → Export pages 1-3, count rows manually
SC-005: Cancel response < 500ms → Click Cancel during preview, measure time
SC-006: Settings restored after restart → Close app, reopen, check printer selection
SC-007: UI functions at 100%-200% DPI → Change Windows display scaling, verify
SC-008: All columns selectable → Uncheck all, verify Print disabled
SC-009: Invalid page ranges prevented → Enter FromPage=10, ToPage=5, verify validation
SC-010: Zero regressions → Run through all existing features
```

---

## Troubleshooting

### Issue: PrintPreviewControl shows blank pages

**Cause**: PrintPage event handler not setting `e.HasMorePages` correctly.

**Fix**: Ensure `OnPrintPage` sets `e.HasMorePages = true` when more data remains:
```csharp
private void OnPrintPage(object sender, PrintPageEventArgs e)
{
    // Render current page
    // ...
    
    e.HasMorePages = (_currentRowIndex < _sourceData.Rows.Count);
}
```

### Issue: Page count doesn't match actual printed pages

**Cause**: PageBoundary tracking logic incorrect.

**Fix**: Verify PageBoundary created for each page in OnPrintPage:
```csharp
_pageBoundaries.Add(new PageBoundary
{
    PageNumber = _pageBoundaries.Count + 1,
    StartRow = _pageStartRow,
    EndRow = _currentRowIndex
});
```

### Issue: Excel export has wrong row count

**Cause**: PageBoundary filtering logic incorrect.

**Fix**: Use inclusive StartRow, exclusive EndRow per data-model.md:
```csharp
for (int i = boundary.StartRow; i < boundary.EndRow; i++) // NOT <=
{
    rowsToExport.Add(sourceData.Rows[i]);
}
```

### Issue: Theme colors not applied

**Cause**: Missing `ApplyThemeColors()` call or called before InitializeComponent().

**Fix**: Ensure constructor order per theming-compliance.instructions.md:
```csharp
public PrintForm(...)
{
    InitializeComponent();          // 1. Designer init
    Core_Themes.ApplyDpiScaling(this);  // 2. DPI scaling
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);  // 3. Layout adjustments
    ApplyThemeColors();             // 4. Custom theme colors
}
```

---

## Next Steps

1. **Review tasks.md**: After `/speckit.tasks` generates tasks, use this quick start for implementation guidance.

2. **Reference instruction files**: When implementing specific components, consult relevant instruction files listed in Prerequisites.

3. **Track progress**: Update task checkboxes in tasks.md as work completes.

4. **Run validation**: Use MCP tools `validate_ui_scaling` and `check_checklists` before merging to master.

---

## Additional Resources

**Documentation**:
- `Documentation/Theme-System-Reference.md` - Complete theme API and color token catalog
- `specs/005-transaction-viewer-form/RefactorPortion/UI-Architecture-Analysis.md` - WinForms layout patterns

**Instruction Files**:
- `.github/instructions/csharp-dotnet8.instructions.md` - Language features and async patterns
- `.github/instructions/winforms-responsive-layout.instructions.md` - TableLayoutPanel patterns
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling standards

**MCP Tools**:
- `validate_ui_scaling` - Check theme compliance before merge
- `generate_ui_fix_plan` - Auto-generate fixes for UI violations
- `check_checklists` - Validate requirements checklist completion

**Questions?** Consult research.md for technical decisions or spec.md for requirements clarification.
