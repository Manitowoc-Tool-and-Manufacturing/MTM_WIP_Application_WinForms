# Research: Print and Export System Refactor

**Feature**: Print and Export System Refactor  
**Date**: 2025-11-08  
**Phase**: 0 - Research & Technical Decisions

---

## Research Questions from Technical Context

Based on the specification and technical context, the following areas required research to make informed architectural decisions.

---

## R1: Windows Print System Page Range Handling

**Question**: How does Windows GDI+ printing handle page ranges when using PrintDocument.PrinterSettings.PrintRange?

**Research Findings**:

The Windows print system provides three PrintRange options:
- **AllPages**: Prints entire document
- **Selection**: Prints current selection (requires SelectionRange set)
- **SomePages**: Prints specific page range using FromPage/ToPage

**Key Mechanism**:
1. PrintDocument raises `PrintPage` event for each page
2. Event handler sets `e.HasMorePages = true` to continue to next page
3. Internal page counter tracks current page number
4. Print system AUTOMATICALLY skips pages outside FromPage/ToPage range
5. Renderer still generates ALL pages but print spooler only sends selected pages to printer

**Decision**: Pass ALL data to PrintDocument, set PrintRange.SomePages with FromPage/ToPage, let Windows handle page filtering

**Rationale**: 
- Eliminates row-based estimation (current 31 rows/page hardcoded value)
- Page count from preview generation is 100% accurate for print job
- Windows spooler handles actual page selection efficiently
- Same mechanism works for PDF export ("Microsoft Print to PDF" printer)

**Alternatives Considered**:
- ❌ **Row filtering before print**: Estimate rows per page → filter DataTable → print subset
  - Rejected because: Estimation error is the root cause of current bug
- ❌ **Custom pagination logic**: Track page numbers in PrintPage handler, skip rendering unwanted pages
  - Rejected because: More complex than built-in PrintRange, prone to off-by-one errors

**Implementation Impact**: Core_TablePrinter.PrintPage handler always renders next row, sets HasMorePages until data exhausted. PrinterSettings configured externally with user's FromPage/ToPage selection.

---

## R2: Excel Export with Exact Page Ranges

**Question**: How can Excel export match exact page boundaries from print preview without approximation?

**Research Findings**:

**Approach 1: Print to Excel via Print Driver** (REJECTED)
- Windows "Print to Excel" drivers exist but unreliable for programmatic use
- Output format varies by driver vendor
- Cell formatting often corrupted

**Approach 2: ClosedXML with Print-Derived Page Boundaries** (SELECTED)
- Use print rendering engine to determine row ranges per page
- Store page boundaries during preview generation: `List<PageBoundary> { StartRow, EndRow }`
- Export to Excel using ClosedXML, writing only rows within selected page ranges
- Apply formatting to match print appearance (headers, fonts, borders)

**Decision**: Use ClosedXML with print-derived page boundaries

**Rationale**:
- Exact row ranges eliminate approximation warnings (current system shows "Excel page ranges are approximate")
- ClosedXML already in project dependencies (used for other exports)
- Print preview generates page boundaries as side-effect (no extra processing)
- Excel output has correct data even if visual formatting differs slightly from print

**Alternatives Considered**:
- ❌ **PDF only, skip Excel**: Simplifies Phase 4 but users need editable exports for auditing
- ❌ **Approximate page ranges**: Maintains current behavior but doesn't solve core issue
- ❌ **Print to bitmap → OCR → Excel**: Extremely complex, accuracy issues, not programmatically feasible

**Implementation Impact**:
- Core_TablePrinter tracks `List<PageBoundary>` during rendering
- Helper_ExportManager.ExportToExcel receives page boundaries and selected range
- Only rows within (FromPage...ToPage) boundaries are written to Excel
- Success Criterion SC-004 verifiable: "Excel export of pages 1-3 contains exactly the same rows as preview pages 1-3"

---

## R3: PrintPreviewControl Zoom Levels

**Question**: What zoom levels does PrintPreviewControl natively support, and how to implement "Fit to Width" and "Fit to Page"?

**Research Findings**:

**Native PrintPreviewControl Properties**:
- `Zoom` property accepts decimal values (0.1 = 10%, 2.0 = 200%)
- `AutoZoom` property enables automatic "Fit to Page" behavior
- No built-in "Fit to Width" - requires custom calculation

**"Fit to Width" Implementation**:
```csharp
// Calculate zoom to fit page width to control width
double pageWidth = printDocument.DefaultPageSettings.Bounds.Width;
double controlWidth = printPreviewControl.ClientSize.Width;
double fitToWidthZoom = controlWidth / pageWidth;
printPreviewControl.Zoom = fitToWidthZoom;
printPreviewControl.AutoZoom = false; // Disable auto
```

**"Fit to Page" Implementation**:
```csharp
printPreviewControl.AutoZoom = true; // Enables fit-to-page
```

**Decision**: Implement both "Fit to Width" and "Fit to Page" per user clarification (Q5 Answer: A and B)

**Rationale**:
- "Fit to Page" uses built-in AutoZoom (simple, reliable)
- "Fit to Width" requires custom calculation but straightforward
- Users frequently want to maximize horizontal space when viewing wide tables
- Standard zoom presets provide fine-grained control when needed

**Alternatives Considered**:
- ❌ **Freeform zoom slider**: Adds UI complexity, standard presets + fit options cover 95% of use cases
- ❌ **Preset levels only**: Missing "Fit to Width" causes poor UX for wide tables

**Implementation Impact**:
- Zoom ComboBox items: "25%", "50%", "75%", "100%", "125%", "150%", "200%", "Fit to Width", "Fit to Page"
- ComboBox selection handler sets Zoom or AutoZoom based on choice
- Initial load defaults to "Fit to Page" for best first impression

---

## R4: Progress Dialog with Cancellation Support

**Question**: How to implement responsive cancellation for preview generation that may take several seconds?

**Research Findings**:

**CancellationToken Pattern**:
```csharp
// PrintForm.cs
private async Task GeneratePreviewAsync(CancellationTokenSource cts)
{
    using var progressDialog = new ProgressDialog();
    progressDialog.CancellationTokenSource = cts;
    progressDialog.Show(this);
    
    try
    {
        await Task.Run(() => {
            // Preview generation logic
            for (int page = 0; page < totalPages; page++)
            {
                cts.Token.ThrowIfCancellationRequested();
                // Render page
            }
        }, cts.Token);
    }
    catch (OperationCanceledException)
    {
        // User cancelled - clean up gracefully
    }
    finally
    {
        progressDialog.Close();
    }
}
```

**Elapsed Time Counter**:
```csharp
// ProgressDialog.cs
private System.Windows.Forms.Timer _timer;
private DateTime _startTime;

private void ProgressDialog_Load(object sender, EventArgs e)
{
    _startTime = DateTime.Now;
    _timer = new Timer { Interval = 1000 }; // 1 second
    _timer.Tick += (s, args) => {
        var elapsed = DateTime.Now - _startTime;
        lblElapsed.Text = elapsed.ToString(@"mm\:ss");
    };
    _timer.Start();
}
```

**Decision**: Use CancellationTokenSource with modal progress dialog, Timer for elapsed time

**Rationale**:
- CancellationToken is .NET standard for async cancellation
- Modal dialog prevents user interaction with print form during generation
- Timer provides visible feedback that operation is progressing
- Clean exception handling ensures resources released on cancel

**Alternatives Considered**:
- ❌ **BackgroundWorker**: Legacy pattern, CancellationToken more modern
- ❌ **Modeless dialog**: User might change settings during preview generation causing inconsistency
- ❌ **No cancellation**: 1500-row dataset could take 10+ seconds, poor UX

**Implementation Impact**:
- FR-019 requires progress bar - include if Core_TablePrinter can report % complete
- Cancel button wired to `cts.Cancel()`
- Success Criterion SC-005: "Cancel within 500ms" verifiable by measuring time between button click and dialog close

---

## R5: Settings Persistence Storage Location

**Question**: Should per-grid print settings (printer, columns) be stored in MySQL database or local JSON files?

**Research Findings**:

**Option A: MySQL Database**
- Pros: Centralized, multi-machine sync, backup with database
- Cons: Requires schema changes (new table), adds stored procedures, increases DB dependency

**Option B: Local JSON Files in AppData**
- Pros: No schema changes, no stored procedures, simple read/write, user-specific
- Cons: Not synced across machines, lost if profile deleted

**Decision**: Local JSON files in `%APPDATA%\MTM\PrintSettings\{GridName}.json`

**Rationale**:
- Avoids database schema changes during refactor (keeps scope focused)
- Print settings are user-preference data, not business data
- Each grid (Remove tab, Transactions tab) gets dedicated JSON file
- System.Text.Json already in project dependencies
- Can migrate to database in version 1.1 if multi-machine sync requested

**JSON Structure**:
```json
{
  "GridName": "Control_RemoveTab",
  "PrinterName": "Microsoft Print to PDF",
  "VisibleColumns": ["PartNumber", "Quantity", "Location", "Date"],
  "ColumnOrder": ["PartNumber", "Quantity", "Location", "Date"],
  "LastModified": "2025-11-08T14:30:00Z"
}
```

**Implementation Impact**:
- Model_PrintSettings class with Load/Save methods
- FR-029: "Remember printer selection, column order, column visibility per grid between sessions"
- FR-030: "Reset orientation, color mode, page range, zoom each session" (not persisted)

---

## R6: DataGridView to DataTable Conversion

**Question**: How to reliably convert DataGridView display data to DataTable for printing?

**Research Findings**:

**Scenario 1: DataGridView.DataSource is DataTable**
```csharp
// Direct cast
DataTable dt = (DataTable)dataGridView.DataSource;
```

**Scenario 2: DataGridView.DataSource is BindingSource**
```csharp
BindingSource bs = (BindingSource)dataGridView.DataSource;
DataTable dt = (DataTable)bs.DataSource;
```

**Scenario 3: Unbound DataGridView (manual row population)**
```csharp
// Create DataTable from grid structure
DataTable dt = new DataTable();
foreach (DataGridViewColumn col in dataGridView.Columns)
{
    dt.Columns.Add(col.Name, col.ValueType ?? typeof(string));
}
foreach (DataGridViewRow row in dataGridView.Rows)
{
    if (!row.IsNewRow)
    {
        var values = row.Cells.Cast<DataGridViewCell>()
            .Select(c => c.Value ?? DBNull.Value).ToArray();
        dt.Rows.Add(values);
    }
}
```

**Decision**: Support all three scenarios with helper method in Helper_PrintManager

**Rationale**:
- MTM application uses DataTable DataSource in most grids (confirmed in existing controls)
- Helper method provides fallback for edge cases
- Unbound grid scenario rare but possible in development/testing

**Implementation Impact**:
- Helper_PrintManager.GetDataTableFromGrid(DataGridView dgv) method
- Handles type detection and appropriate conversion
- Logs warning if unusual DataSource type encountered

---

## R7: Print Dialog Theme Integration

**Question**: How to properly integrate Compact Sidebar print dialog with MTM theme system?

**Research Findings**:

**Required Theme Methods** (from Constitution Principle IX):
```csharp
public PrintForm(DataGridView sourceGrid)
{
    InitializeComponent();
    
    // MANDATORY: Theme integration
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    // Custom initialization
    InitializePreview(sourceGrid);
    LoadSettings(sourceGrid.Name);
    ApplyThemeColors();
}
```

**Theme Color Application**:
```csharp
private void ApplyThemeColors()
{
    var colors = Model_AppVariables.UserUiColors;
    
    // Sidebar panels
    pnlSidebar.BackColor = colors.PanelBackColor ?? SystemColors.Control;
    
    // Preview viewport
    pnlPreviewViewport.BackColor = Color.FromArgb(149, 165, 166); // #95a5a6 gray per mockup
    
    // Buttons
    btnPrint.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
    btnPrint.ForeColor = colors.ButtonForeColor ?? SystemColors.ControlText;
    
    // Export dropdown
    btnExport.BackColor = colors.AccentColor ?? SystemColors.Highlight;
    btnExport.ForeColor = Color.White;
}
```

**WinForms UI Architecture Compliance**:
- Control naming: `PrintForm_Panel_Main`, `PrintForm_TableLayout_Sidebar`, `PrintForm_ComboBox_Zoom`
- AutoSize cascade: Root Panel → TableLayoutPanel (80/20 split) → child containers
- Leaf controls: Zoom ComboBox MinimumSize/MaximumSize = 120x23

**Decision**: Full theme integration following Constitution Principle IX patterns

**Rationale**:
- FR-020 and FR-021 mandate theme compliance
- Consistent visual experience across MTM application
- DPI scaling essential for 100%-200% range (manufacturing monitors vary widely)
- UI architecture prevents "12,000 pixel control" issue documented in constitution

**Reference Documentation**:
- `Documentation/Theme-System-Reference.md` - Complete theme API
- `.github/instructions/ui-compliance/theming-compliance.instructions.md` - Patterns and enforcement

---

## Research Summary

All technical unknowns resolved. Key decisions:

1. **Page Range Handling**: Windows PrintRange.SomePages with FromPage/ToPage (no row filtering)
2. **Excel Export**: ClosedXML with print-derived page boundaries (exact, not approximate)
3. **Zoom Levels**: Standard presets + "Fit to Width" (custom) + "Fit to Page" (AutoZoom)
4. **Cancellation**: CancellationTokenSource with modal progress dialog and elapsed timer
5. **Settings Storage**: Local JSON in AppData (avoids database schema changes)
6. **DataTable Conversion**: Helper method supporting DataTable, BindingSource, and unbound grids
7. **Theme Integration**: Full compliance with Principle IX (DPI scaling, color tokens, UI architecture)

**Ready for Phase 1**: Data model and contracts generation can proceed with confidence in technical approach.
