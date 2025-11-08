---
description: 'Comprehensive print dialog system with preview, filtering, presets, and export capabilities'
applyTo: 'Forms/Shared/PrintForm.cs,Controls/**/*.cs,Helpers/Helper_PrintExport.cs'
---

# Print Form System Instructions

## Overview

The PrintForm system provides a comprehensive, feature-rich print dialog for DataGridView controls throughout the MTM WIP Application. It replaces direct Core_DgvPrinter usage with a modern, user-friendly interface that includes preview, filtering, column management, presets, and multi-format export.

**Created**: 2025-11-08  
**Status**: Ready for integration (92% complete)  
**Location**: `Forms/Shared/PrintForm.cs`

## Core Principles

### User-Centric Design
- Print preview BEFORE committing to paper
- Save/load print configurations as presets
- Filter data before printing to reduce waste
- Export to multiple formats (PDF, Excel, Image)
- Persist user preferences across sessions

### Data Integrity
- Non-destructive filtering (original DataGridView unchanged)
- Selected row tracking for "Show Selected Rows" filter
- Column order preservation
- Full undo capability (filters can be cleared)

### Performance
- Async-ready architecture for future enhancements
- Efficient DataTable filtering using LINQ
- Minimal memory footprint with proper disposal
- Progress feedback for long-running operations

## Architecture

### Component Structure

```
PrintForm (Main Dialog)
├── Preview Tab
│   ├── PrintPreviewControl (live preview)
│   ├── Zoom controls (25%-200%)
│   └── Page navigation (First, Prev, Next, Last)
├── Settings Tab
│   ├── Printer selection
│   ├── Color/B&W toggle
│   ├── Orientation (Portrait/Landscape)
│   ├── Page range (All, Current, From-To)
│   ├── Export options (PDF, Excel, Image)
│   └── Preset management (Save, Load, Delete)
└── Columns Tab
    ├── Column visibility (CheckedListBox)
    ├── Column reordering (Move Up/Down)
    └── Data filtering
        ├── Text filters (Contains, Equals)
        ├── Date range filters
        └── Show Selected Rows filter
```

### Data Flow

```
DataGridView → PrintForm Constructor
    ↓
Convert to DataTable + Capture Selected Rows
    ↓
Apply Filters (if any)
    ↓
Apply Column Visibility/Order
    ↓
Export/Print with filtered & ordered data
```

## Usage Patterns

### Basic Usage (Replace Core_DgvPrinter)

**BEFORE (Legacy Pattern):**
```csharp
private void Print_Click(object? sender, EventArgs e)
{
    try
    {
        if (myDataGridView?.Rows.Count == 0)
        {
            Service_ErrorHandler.ShowWarning("No data to print.", "Print");
            return;
        }

        var printer = new Core_DgvPrinter();
        printer.ShowPrintPreview(myDataGridView);
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
    }
}
```

**AFTER (New PrintForm Pattern):**
```csharp
private void Print_Click(object? sender, EventArgs e)
{
    try
    {
        if (myDataGridView?.Rows.Count == 0)
        {
            Service_ErrorHandler.ShowWarning("No data to print.", "Print");
            return;
        }

        using (var printForm = new Forms.Shared.PrintForm(myDataGridView))
        {
            printForm.ShowDialog(this.FindForm());
        }
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
    }
}
```

### Advanced Usage with Progress Feedback

```csharp
private void Print_Click(object? sender, EventArgs e)
{
    _progressHelper?.ShowProgress();
    _progressHelper?.UpdateProgress(10, "Preparing print dialog...");

    try
    {
        if (myDataGridView?.Rows.Count == 0)
        {
            Service_ErrorHandler.ShowWarning("No data to print.", "Print");
            return;
        }

        _progressHelper?.UpdateProgress(50, "Opening print dialog...");
        
        using (var printForm = new Forms.Shared.PrintForm(myDataGridView))
        {
            printForm.ShowDialog(this.FindForm());
        }
        
        _progressHelper?.ShowSuccess("Print dialog closed");
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
            controlName: nameof(MyControl));
    }
    finally
    {
        _progressHelper?.HideProgress();
    }
}
```

## Features

### 1. Print Preview

**Zoom Levels:**
- 25%, 50%, 75%, 100%, 150%, 200%
- Zoom In/Out buttons with wraparound
- Dropdown selection for direct access

**Page Navigation:**
- First Page, Previous Page, Next Page, Last Page buttons
- Current page indicator (e.g., "Page 1 of 5")
- Keyboard support (planned: Page Up/Down, Home/End)

**Live Updates:**
- Preview refreshes when filters change
- Preview updates when columns are reordered
- Preview reflects orientation changes

### 2. Printer Settings

**Printer Selection:**
- Populated from `PrinterSettings.InstalledPrinters`
- Remembers last selected printer (persisted in `%AppData%\MTM\PrintSettings.json`)
- Validates printer availability before use

**Color Mode:**
- Color printing (default)
- Black & White printing
- Persisted preference

**Orientation:**
- Portrait (default)
- Landscape
- Updates preview immediately

**Page Range:**
- All Pages (default)
- Current Page (single page)
- Page Range (From/To with validation)

### 3. Column Management

**Visibility Control:**
- CheckedListBox showing all columns
- Select All / Deselect All buttons
- Pre-populates with currently visible columns

**Column Reordering:**
- Move Up / Move Down buttons
- Changes column order in output
- Does NOT affect source DataGridView

**Persistence:**
- Column selections saved to user settings
- Column order persisted across sessions

### 4. Data Filtering

**Text Filters:**
```csharp
// Contains filter (case-insensitive substring match)
Filter: Column="PartNumber", Type="Contains", Value="ABC"
Result: Shows rows where PartNumber contains "ABC"

// Equals filter (exact match)
Filter: Column="Status", Type="Equals", Value="Active"
Result: Shows only rows where Status exactly equals "Active"
```

**Date Range Filter:**
```csharp
// Date range filter
Filter: Column="ReceiveDate", Type="Date Range", 
        DateFrom=2025-01-01, DateTo=2025-12-31
Result: Shows rows where ReceiveDate is within 2025
```

**Show Selected Rows:**
```csharp
// Filter to selected rows only
Filter: Type="Show Selected Rows"
Result: Shows only rows that were selected in source DataGridView
Note: Captured at PrintForm construction time
```

**Filter Management:**
- Active filters displayed in ListBox
- Remove individual filters
- Clear All Filters button
- Status bar shows: "X of Y rows (N filters active)"

**Filter Chaining:**
Filters are applied sequentially:
```
Original Data (100 rows)
  → Filter 1: Contains "ABC" (50 rows)
    → Filter 2: Date Range 2025 (30 rows)
      → Filter 3: Show Selected (10 rows)
Final Result: 10 rows printed
```

### 5. Print Presets

**Preset Contents:**
- Printer selection
- Color/orientation settings
- Page range preferences
- Column visibility and order
- Active filters
- Export options

**Preset Operations:**
```csharp
// Save current configuration
1. Configure print settings (filters, columns, printer, etc.)
2. Click "Save Preset" button
3. Enter preset name (e.g., "Monthly Inventory Report")
4. Preset saved to: %AppData%\MTM\PrintPresets\{PresetName}.json

// Load preset
1. Select preset from dropdown
2. All settings automatically restored
3. Preview updates to reflect preset

// Delete preset
1. Select preset from dropdown
2. Click "Delete Preset" button
3. Confirm deletion
4. Preset file removed from disk
```

**Preset Storage Format:**
```json
{
  "PresetName": "Monthly Inventory Report",
  "PrinterName": "Microsoft Print to PDF",
  "IsColor": false,
  "Orientation": "Landscape",
  "PageRange": "All",
  "PageFrom": 1,
  "PageTo": 1,
  "ExportToPdf": true,
  "ExportToExcel": false,
  "ExportToImage": false,
  "SelectedColumns": ["PartNumber", "Description", "Quantity", "Location"],
  "ColumnOrder": ["PartNumber", "Description", "Quantity", "Location", "User"],
  "Filters": [
    {
      "ColumnName": "Status",
      "FilterType": "Equals",
      "FilterValue": "Active"
    }
  ],
  "CreatedDate": "2025-11-08T10:30:00",
  "LastUsedDate": "2025-11-08T14:45:00"
}
```

### 6. Export Functionality

**Excel Export (ClosedXML):**
```csharp
// Features:
- Respects column order and visibility
- Applies all active filters
- Formatted headers (bold, gray background)
- Auto-sized columns for readability
- File save dialog with timestamp: "Export_20251108_143022.xlsx"

// Implementation:
Helper_PrintExport.ExportToExcel(
    filteredData, 
    filePath, 
    columnOrder, 
    visibleColumns);
```

**PDF Export (Print to PDF):**
```csharp
// Features:
- Uses Microsoft Print to PDF printer
- Respects orientation (Portrait/Landscape)
- Respects color mode (Color/B&W)
- File save dialog with timestamp: "Export_20251108_143022.pdf"

// Implementation:
Helper_PrintExport.ExportToPdf(
    filteredData, 
    filePath, 
    columnOrder, 
    visibleColumns, 
    isLandscape, 
    isColor);
```

**Image Export (Bitmap Rendering):**
```csharp
// Features:
- Renders data as table image (PNG or JPG)
- Formatted with borders and headers
- Supports both PNG and JPEG formats
- File save dialog with timestamp: "Export_20251108_143022.png"

// Implementation:
Helper_PrintExport.ExportToImage(
    filteredData, 
    filePath, 
    columnOrder, 
    visibleColumns, 
    imageFormat);
```

**Export Button Usage:**
1. Check desired export format(s) (PDF, Excel, Image)
2. Click "Export Settings" button
3. For each checked format:
   - File save dialog appears
   - Choose location and filename
   - Export executes with current filters/columns
4. Success message displays export path

### 7. Settings Persistence

**User Settings File:**
```
Location: %AppData%\MTM\PrintSettings.json

Contents:
{
  "LastPrinterName": "Microsoft Print to PDF",
  "LastIsColor": true,
  "LastOrientation": "Portrait",
  "LastSelectedColumns": ["PartID", "Description", "Quantity"],
  "LastColumnOrder": ["PartID", "Description", "Quantity", "Location"]
}
```

**Auto-Save Behavior:**
- Settings saved when PrintForm closes successfully
- Settings loaded when PrintForm opens
- Graceful fallback to defaults if settings file missing

## Keyboard Shortcuts

- **Ctrl+P**: Execute print operation
- **Esc**: Cancel and close dialog
- **Tab/Shift+Tab**: Navigate between controls
- **Enter**: Confirm dialogs and inputs

## User Interface Guidelines

### Status Bar Messages

```csharp
// No filters active
"100 rows"

// Filters active
"25 of 100 rows (2 filters active)"

// No data
"No rows to display"
```

### Error Messages

```csharp
// No data to print
"No data to print." (Warning)

// No printer available
"No printers are installed on this system." (Warning)

// Invalid page range
"'From Page' must be less than or equal to 'To Page'." (Validation)

// Selected rows filter with no selection
"No rows are currently selected in the source grid." (Warning)

// Export with no formats selected
"Please select at least one export format." (Warning)
```

### Tooltips

All controls have comprehensive tooltips:
- **Zoom controls**: "Zoom in (increase preview size)"
- **Column selection**: "Select columns to include in output"
- **Filter type**: "Select filter type (Contains, Equals, Date Range)"
- **Export buttons**: "Export to PDF file"
- And 90+ more...

## Integration Checklist

When integrating PrintForm into a new control:

- [ ] Replace `Core_DgvPrinter.ShowPrintPreview()` call
- [ ] Add `using MTM_WIP_Application_Winforms.Forms.Shared;`
- [ ] Wrap in `using` statement for proper disposal
- [ ] Pass parent form for modal centering: `.ShowDialog(this.FindForm())`
- [ ] Remove legacy column visibility code (PrintForm handles it)
- [ ] Update progress messages if using `Helper_StoredProcedureProgress`
- [ ] Test with selected rows (for "Show Selected Rows" filter)
- [ ] Test with empty DataGridView (should show warning)

## Testing Scenarios

### Smoke Test

1. Open form with DataGridView
2. Click Print button
3. PrintForm opens centered on parent
4. Preview shows data
5. Close with Cancel button

### Column Management Test

1. Open PrintForm
2. Go to Columns tab
3. Deselect half of columns
4. Move remaining columns up/down
5. Return to Preview tab
6. Verify preview shows correct columns in new order

### Filtering Test

1. Add text filter (Contains "ABC")
2. Status bar shows reduced row count
3. Add date range filter
4. Status bar shows further reduction
5. Remove one filter
6. Status bar shows increase
7. Clear all filters
8. Status bar shows original count

### Preset Test

1. Configure filters, columns, printer
2. Save as "Test Preset"
3. Change all settings to different values
4. Load "Test Preset" from dropdown
5. Verify all settings restored
6. Delete "Test Preset"
7. Verify preset removed from dropdown

### Export Test

1. Check "Export to Excel"
2. Click "Export Settings"
3. Choose file location
4. Verify Excel file created with correct data/columns
5. Repeat for PDF and Image formats

## Common Pitfalls

### Pitfall 1: Not Disposing PrintForm

❌ **WRONG:**
```csharp
var printForm = new PrintForm(myDataGridView);
printForm.ShowDialog();
// Memory leak! PrintForm not disposed
```

✅ **CORRECT:**
```csharp
using (var printForm = new PrintForm(myDataGridView))
{
    printForm.ShowDialog(this.FindForm());
}
// PrintForm properly disposed
```

### Pitfall 2: Passing Null DataGridView

❌ **WRONG:**
```csharp
using (var printForm = new PrintForm(myDataGridView))
{
    printForm.ShowDialog();
}
// Throws ArgumentNullException if myDataGridView is null
```

✅ **CORRECT:**
```csharp
if (myDataGridView == null || myDataGridView.Rows.Count == 0)
{
    Service_ErrorHandler.ShowWarning("No data to print.", "Print");
    return;
}

using (var printForm = new PrintForm(myDataGridView))
{
    printForm.ShowDialog(this.FindForm());
}
```

### Pitfall 3: Not Passing Parent Form

❌ **WRONG:**
```csharp
printForm.ShowDialog();
// Dialog not centered, may appear off-screen
```

✅ **CORRECT:**
```csharp
printForm.ShowDialog(this.FindForm());
// Dialog centered on parent form
```

### Pitfall 4: Modifying Source DataGridView

❌ **WRONG:**
```csharp
// Don't filter source DataGridView before passing to PrintForm
myDataGridView.Rows.Clear();
foreach (var item in filteredItems)
{
    myDataGridView.Rows.Add(item);
}
using (var printForm = new PrintForm(myDataGridView))
{
    printForm.ShowDialog();
}
```

✅ **CORRECT:**
```csharp
// PrintForm handles filtering internally - pass full DataGridView
using (var printForm = new PrintForm(myDataGridView))
{
    // User can filter inside PrintForm
    printForm.ShowDialog(this.FindForm());
}
```

### Pitfall 5: Ignoring Selected Rows

```csharp
// If user has rows selected in DataGridView, PrintForm captures them
// The "Show Selected Rows" filter will be available
// Make sure DataGridView.SelectionMode allows row selection:

myDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
// or
myDataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
```

## Files and Dependencies

### Core Files

```
Forms/Shared/PrintForm.cs (1,100+ lines)
Forms/Shared/PrintForm.Designer.cs
Models/Model_PrintPreset.cs
Models/Model_Print_Settings.cs
Helpers/Helper_PrintExport.cs (270+ lines)
```

### Dependencies

```
Required:
- System.Drawing.Printing
- ClosedXML (Excel export)
- MTM_WIP_Application_Winforms.Core (Core_Themes, Core_DgvPrinter)
- MTM_WIP_Application_Winforms.Services (Service_ErrorHandler)
- MTM_WIP_Application_Winforms.Logging (LoggingUtility)

Optional:
- Microsoft Print to PDF printer (for PDF export)
```

### Storage Locations

```
User Settings: %AppData%\MTM\PrintSettings.json
Presets: %AppData%\MTM\PrintPresets\*.json
```

## Performance Considerations

### Memory Usage

- DataTable copy created for filtering (~2x source data size)
- Preview rendering uses GDI+ (moderate CPU usage)
- Excel export creates in-memory workbook (moderate memory)
- Image export creates bitmap (size = width × height × 4 bytes)

### Optimization Tips

```csharp
// For large DataGridViews (1000+ rows), consider:

1. Pre-filter before opening PrintForm:
   myDataGridView.DataSource = filteredData; // Reduce initial dataset

2. Limit visible columns:
   // Fewer columns = faster rendering

3. Use page range printing:
   // Print "From Page 1 To Page 10" instead of all pages

4. Export to Excel instead of PDF:
   // Excel export is faster than PDF rendering
```

## Migration Guide

### Step 1: Identify Print Calls

```bash
# Search for Core_DgvPrinter usage
grep -r "Core_DgvPrinter" Controls/
grep -r "ShowPrintPreview" Controls/
```

### Step 2: Replace Implementation

For each print button/menu item:

1. Comment out old Core_DgvPrinter code
2. Add new PrintForm code
3. Test basic functionality
4. Remove commented code once verified

### Step 3: Test Integration

- Verify print preview works
- Test column selection
- Test basic filtering
- Test preset save/load

### Step 4: User Training

- Demonstrate new features to users
- Explain preset system benefits
- Show export capabilities
- Distribute keyboard shortcut cheat sheet

## Future Enhancements

### Planned Features (Not Yet Implemented)

- [ ] Core_DgvPrinter integration for actual printing (currently uses legacy path)
- [ ] Progress indicators for long exports (>5 seconds)
- [ ] Print preview zoom with mouse wheel
- [ ] Drag-and-drop column reordering
- [ ] Filter templates (save filter combinations)
- [ ] Export format templates (column selections per export type)
- [ ] Batch export (all formats with one click)
- [ ] Email integration (export and email)
- [ ] Cloud storage integration (export to OneDrive/Dropbox)

### Known Limitations

- PDF export requires Microsoft Print to PDF printer
- Image export limited to visible columns (wide tables may be clipped)
- Date range filters require proper DateTime columns
- Text filters are case-insensitive (no case-sensitive option)
- Maximum recommended dataset: 10,000 rows

## Troubleshooting

### Issue: Preview is blank

**Cause**: No data in DataGridView or all columns deselected  
**Solution**: Check DataGridView.Rows.Count and column selection

### Issue: Preset not loading

**Cause**: Preset file corrupted or printer no longer exists  
**Solution**: Delete preset file and recreate

### Issue: Export fails silently

**Cause**: Insufficient permissions to write to selected location  
**Solution**: Choose different export location or run with elevated permissions

### Issue: "Show Selected Rows" filter shows no rows

**Cause**: No rows were selected in source DataGridView  
**Solution**: Select rows before opening PrintForm

### Issue: Slow preview rendering

**Cause**: Large dataset or many columns  
**Solution**: Apply filters to reduce row count, hide unnecessary columns

## Support and Maintenance

### Logging

All operations logged via `LoggingUtility`:
```
[PrintForm] Form initialized with 250 rows
[PrintForm] Filter added: Contains 'ABC' on PartNumber
[PrintForm] Filters applied. 45 of 250 rows.
[PrintForm] Preset saved: Monthly Report
[PrintForm] Excel export successful: C:\Exports\Data_20251108.xlsx
```

### Error Handling

All exceptions caught and routed through `Service_ErrorHandler`:
- Low severity: User-caused validation errors
- Medium severity: File I/O errors, export failures
- High severity: Critical failures (should be rare)

### Code Review Checklist

When reviewing PrintForm-related changes:

- [ ] Proper disposal with `using` statements
- [ ] Null checks on DataGridView parameter
- [ ] Parent form passed to ShowDialog
- [ ] Progress feedback provided (if applicable)
- [ ] Error handling with Service_ErrorHandler
- [ ] Logging for significant operations
- [ ] XML documentation on public methods
- [ ] Tooltips on new controls
- [ ] Keyboard shortcuts documented

## Version History

### v1.0.0 (2025-11-08)

**Initial Release:**
- Complete tabbed interface (Preview, Settings, Columns)
- Print preview with zoom and navigation
- Column visibility and reordering
- Data filtering (4 types: Contains, Equals, Date Range, Selected Rows)
- Preset save/load/delete system
- Settings persistence
- Export to Excel, PDF, Image
- Keyboard shortcuts (Ctrl+P, Esc)
- 90+ tooltips
- Comprehensive error handling
- Full Core_Themes DPI scaling integration
- XML documentation

**Known Issues:**
- Core_DgvPrinter integration pending (uses legacy print path)
- Progress indicators for exports not implemented
- User documentation pending

---

**Last Updated**: 2025-11-08  
**Maintainer**: Development Team  
**Status**: Ready for integration testing
