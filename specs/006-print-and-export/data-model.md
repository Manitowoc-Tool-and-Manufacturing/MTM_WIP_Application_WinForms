# Data Model: Print and Export System Refactor

**Feature**: Print and Export System Refactor  
**Date**: 2025-11-08  
**Phase**: 1 - Design & Contracts

---

## Overview

This document defines the data structures for the print and export system. The model consists of configuration objects, persistence entities, and runtime tracking structures.

---

## Core Entities

### 1. PrintJob

**Purpose**: Represents a complete print or export operation with all configuration details.

**Properties**:

| Property | Type | Nullable | Description | Validation Rules |
|----------|------|----------|-------------|------------------|
| SourceData | DataTable | No | Source data to print (all rows, all columns) | Must have at least 1 row |
| Title | string | No | Document title (appears in header) | Max 100 characters |
| VisibleColumns | List<string> | No | Column names to include in output | Must match SourceData column names |
| ColumnOrder | List<string> | No | Display order of columns (left to right) | Must match VisibleColumns exactly |
| PrinterName | string | Yes | Target printer (null = default printer) | Must exist in installed printers |
| Orientation | PageOrientation | No | Portrait or Landscape | Enum: Portrait, Landscape |
| ColorMode | PrintColorMode | No | Color or Grayscale | Enum: Color, Grayscale |
| PageRange | PrintPageRange | No | All, Current, or Custom range | Enum: AllPages, CurrentPage, CustomRange |
| FromPage | int | Yes | Starting page (when PageRange = Custom) | Must be >= 1 and <= ToPage |
| ToPage | int | Yes | Ending page (when PageRange = Custom) | Must be >= FromPage and <= TotalPages |
| TotalPages | int | No | Actual page count (set after preview generation) | Calculated by Core_TablePrinter |

**Relationships**:
- Used by: Core_TablePrinter (rendering), Helper_PrintManager (orchestration), Helper_ExportManager (PDF/Excel export)
- Created from: DataGridView via Helper_PrintManager.CreatePrintJob()
- Persisted: VisibleColumns + ColumnOrder saved in PrintSettings, other properties are session-only

**State Transitions**:
1. **Created**: PrintJob instantiated with SourceData and defaults
2. **Configured**: User modifies printer, orientation, columns, page range in PrintForm
3. **Preview Generated**: TotalPages set after Core_TablePrinter renders
4. **Executing**: Print or export operation in progress
5. **Completed**: Operation finished successfully
6. **Cancelled**: User cancelled during preview generation

**Example**:
```csharp
var job = new PrintJob
{
    SourceData = transactionData,
    Title = "Transaction Report - 2025-11-08",
    VisibleColumns = new List<string> { "PartNumber", "Quantity", "Location", "Date" },
    ColumnOrder = new List<string> { "PartNumber", "Quantity", "Location", "Date" },
    PrinterName = "Microsoft Print to PDF",
    Orientation = PageOrientation.Landscape,
    ColorMode = PrintColorMode.Grayscale,
    PageRange = PrintPageRange.CustomRange,
    FromPage = 3,
    ToPage = 7,
    TotalPages = 12 // Set after preview
};
```

---

### 2. PrintSettings

**Purpose**: Stores user preferences per grid for persistence between sessions.

**Properties**:

| Property | Type | Nullable | Description | Validation Rules |
|----------|------|----------|-------------|------------------|
| GridName | string | No | Unique identifier for grid (e.g., "Control_RemoveTab") | Must be valid C# identifier |
| PrinterName | string | Yes | Last selected printer | Must exist in installed printers or null |
| VisibleColumns | List<string> | No | Last column visibility selection | Must match grid's available columns |
| ColumnOrder | List<string> | No | Last column ordering | Must match VisibleColumns exactly |
| LastModified | DateTime | No | Timestamp of last save | UTC datetime |

**Persistence**:
- **Location**: `%APPDATA%\MTM\PrintSettings\{GridName}.json`
- **Format**: JSON using System.Text.Json
- **Lifetime**: Permanent (until user clears app data or reinstalls)

**Not Persisted** (per FR-030):
- Orientation (defaults to Portrait each session)
- ColorMode (defaults to Color each session)
- PageRange (defaults to AllPages each session)
- Zoom level (defaults to "Fit to Page" each session)

**Relationships**:
- Loaded by: PrintForm constructor when opening dialog
- Saved by: PrintForm when user clicks Print or Export (success only)
- One-to-one with: DataGridView instances (each grid has its own settings file)

**LastModified Update Triggers**:
- **On Save()**: Updates `LastModified = DateTime.UtcNow` when saving to JSON file
- **On Load()**: Preserves existing `LastModified` value from file (read-only after load)
- **Trigger Points**: User clicks Print/Export button AND operation completes successfully (no errors)
- **Not Updated**: When print/export fails, when dialog cancelled, when preview-only generated

**Example JSON**:
```json
{
  "GridName": "Control_RemoveTab",
  "PrinterName": "HP LaserJet Pro M404",
  "VisibleColumns": ["PartNumber", "Description", "Quantity", "Location"],
  "ColumnOrder": ["PartNumber", "Quantity", "Location", "Description"],
  "LastModified": "2025-11-08T14:30:00Z"
}
```

---

### 3. PageBoundary

**Purpose**: Tracks exact row ranges that appear on each printed page (calculated during rendering, not estimated).

**Properties**:

| Property | Type | Nullable | Description | Validation Rules |
|----------|------|----------|-------------|------------------|
| PageNumber | int | No | 1-based page number | Must be >= 1 |
| StartRow | int | No | First row index on this page (0-based) | Must be >= 0 and < EndRow |
| EndRow | int | No | Last row index on this page (0-based, exclusive) | Must be > StartRow and <= DataTable.Rows.Count |

**Lifecycle**:
1. **Generated**: Core_TablePrinter creates PageBoundary for each rendered page
2. **Stored**: List<PageBoundary> held in PrintJob or Core_TablePrinter instance
3. **Consumed**: Helper_ExportManager uses boundaries to extract rows for Excel export
4. **Discarded**: Cleared when print dialog closes

**Usage in Excel Export**:
```csharp
// User selects pages 3-7 for export
var selectedBoundaries = pageBoundaries
    .Where(pb => pb.PageNumber >= 3 && pb.PageNumber <= 7)
    .ToList();

// Extract rows
var rowsToExport = new List<DataRow>();
foreach (var boundary in selectedBoundaries)
{
    for (int i = boundary.StartRow; i < boundary.EndRow; i++)
    {
        rowsToExport.Add(sourceData.Rows[i]);
    }
}

// Write to Excel using ClosedXML
WriteToExcel(rowsToExport, filePath);
```

**Example**:
```csharp
// For a 100-row dataset with dynamic pagination:
new PageBoundary { PageNumber = 1, StartRow = 0, EndRow = 28 },   // 28 rows (varies by content height)
new PageBoundary { PageNumber = 2, StartRow = 28, EndRow = 57 },  // 29 rows
new PageBoundary { PageNumber = 3, StartRow = 57, EndRow = 85 },  // 28 rows
new PageBoundary { PageNumber = 4, StartRow = 85, EndRow = 100 }  // 15 rows (last page)
```

---

## Enumerations

### PageOrientation
```csharp
public enum PageOrientation
{
    Portrait = 0,
    Landscape = 1
}
```

### PrintColorMode
```csharp
public enum PrintColorMode
{
    Color = 0,
    Grayscale = 1
}
```

### PrintPageRange
```csharp
public enum PrintPageRange
{
    AllPages = 0,      // Corresponds to System.Drawing.Printing.PrintRange.AllPages
    CurrentPage = 1,   // Print only the page currently shown in preview
    CustomRange = 2    // Corresponds to System.Drawing.Printing.PrintRange.SomePages
}
```

---

## Value Objects

### ZoomLevel

**Purpose**: Represents zoom options for print preview.

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| DisplayName | string | Text shown in ComboBox (e.g., "100%", "Fit to Page") |
| ZoomValue | double? | Decimal zoom (0.25 = 25%, 2.0 = 200%, null for AutoZoom) |
| IsAutoZoom | bool | True for "Fit to Page", false otherwise |

**Predefined Instances**:
```csharp
public static readonly ZoomLevel[] StandardZoomLevels = new[]
{
    new ZoomLevel { DisplayName = "25%", ZoomValue = 0.25, IsAutoZoom = false },
    new ZoomLevel { DisplayName = "50%", ZoomValue = 0.5, IsAutoZoom = false },
    new ZoomLevel { DisplayName = "75%", ZoomValue = 0.75, IsAutoZoom = false },
    new ZoomLevel { DisplayName = "100%", ZoomValue = 1.0, IsAutoZoom = false },
    new ZoomLevel { DisplayName = "125%", ZoomValue = 1.25, IsAutoZoom = false },
    new ZoomLevel { DisplayName = "150%", ZoomValue = 1.5, IsAutoZoom = false },
    new ZoomLevel { DisplayName = "200%", ZoomValue = 2.0, IsAutoZoom = false },
    new ZoomLevel { DisplayName = "Fit to Width", ZoomValue = null, IsAutoZoom = false }, // Calculated dynamically
    new ZoomLevel { DisplayName = "Fit to Page", ZoomValue = null, IsAutoZoom = true }
};
```

---

## Data Flow

### Print Workflow

```
User clicks Print button in Control_RemoveTab
    ↓
Helper_PrintManager.ShowPrintDialog(dataGridView)
    ↓
Create PrintJob from DataGridView.DataSource
    ↓
Load PrintSettings from %APPDATA%\MTM\PrintSettings\Control_RemoveTab.json
    ↓
Show PrintForm(printJob, printSettings)
    ↓
User modifies: printer, orientation, columns, page range
    ↓
User clicks "Generate Preview" or dialog auto-generates
    ↓
Core_TablePrinter.RenderPreview(printJob, cancellationToken)
    ↓
Populate List<PageBoundary> during rendering
    ↓
Set PrintJob.TotalPages = pageBoundaries.Count
    ↓
Update PrintPreviewControl with rendered document
    ↓
User clicks "Print"
    ↓
Helper_PrintManager.ExecutePrint(printJob)
    ↓
PrintDocument.Print() with PrinterSettings.PrintRange = SomePages, FromPage, ToPage
    ↓
Save PrintSettings to JSON (printer, columns only)
    ↓
Success dialog or error via Service_ErrorHandler
```

### Excel Export Workflow

```
User clicks "Export" dropdown → "Excel" in PrintForm
    ↓
Helper_ExportManager.ExportToExcel(printJob, pageBoundaries, selectedPageRange, filePath)
    ↓
Filter pageBoundaries by selectedPageRange (FromPage to ToPage)
    ↓
Extract DataRow[] from PrintJob.SourceData using boundary.StartRow/EndRow indices
    ↓
Create ClosedXML workbook
    ↓
Write headers (from VisibleColumns in ColumnOrder)
    ↓
Write data rows (only rows within page boundaries)
    ↓
Apply basic formatting (bold headers, borders)
    ↓
workbook.SaveAs(filePath)
    ↓
Success message via Service_ErrorHandler
```

---

## Validation Rules

### Print Job Validation

```csharp
public class PrintJobValidator
{
    public static DaoResult Validate(PrintJob job)
    {
        if (job.SourceData == null || job.SourceData.Rows.Count == 0)
            return DaoResult.Failure("No data to print");
            
        if (string.IsNullOrWhiteSpace(job.Title))
            return DaoResult.Failure("Document title is required");
            
        if (job.VisibleColumns == null || job.VisibleColumns.Count == 0)
            return DaoResult.Failure("At least one column must be visible");
            
        if (!job.VisibleColumns.All(col => job.SourceData.Columns.Contains(col)))
            return DaoResult.Failure("VisibleColumns contains invalid column names");
            
        if (!job.ColumnOrder.SequenceEqual(job.VisibleColumns))
            return DaoResult.Failure("ColumnOrder must match VisibleColumns");
            
        if (job.PageRange == PrintPageRange.CustomRange)
        {
            if (job.FromPage < 1 || job.ToPage < job.FromPage)
                return DaoResult.Failure("Invalid page range: FromPage must be >= 1 and <= ToPage");
                
            if (job.TotalPages > 0 && job.ToPage > job.TotalPages)
                return DaoResult.Failure($"ToPage ({job.ToPage}) exceeds TotalPages ({job.TotalPages})");
        }
        
        return DaoResult.Success();
    }
}
```

### Settings Persistence Validation

```csharp
public class PrintSettingsValidator
{
    public static DaoResult Validate(PrintSettings settings, DataGridView grid)
    {
        if (string.IsNullOrWhiteSpace(settings.GridName))
            return DaoResult.Failure("GridName is required");
            
        var availableColumns = grid.Columns.Cast<DataGridViewColumn>()
            .Select(c => c.Name).ToList();
            
        if (!settings.VisibleColumns.All(col => availableColumns.Contains(col)))
            return DaoResult.Failure("VisibleColumns contains columns not in grid");
            
        if (!settings.ColumnOrder.SequenceEqual(settings.VisibleColumns))
            return DaoResult.Failure("ColumnOrder must match VisibleColumns");
            
        return DaoResult.Success();
    }
}
```

---

## Migration Notes

### Phase 1: Removal
- Old Model_PrintJob.cs deleted (replaced with new design)
- Old Model_PrintSettings.cs deleted (replaced with new design)

### Phase 2: Core Infrastructure
- New Model_PrintJob.cs created with properties above
- PageBoundary struct created as internal type in Core_TablePrinter.cs

### Phase 3: Print Dialog
- ZoomLevel value object created in PrintForm.cs namespace
- PrintSettings loading/saving implemented in Helper_PrintManager

### Phase 4: Export System
- No new entities (uses existing PrintJob and PageBoundary)

### Phase 5: Integration
- No data model changes (wiring only)

---

## Success Criteria Mapping

| Success Criterion | Data Model Support |
|-------------------|-------------------|
| SC-002: Page count 100% accurate | PageBoundary list provides exact count (no estimation) |
| SC-003: PDF matches preview exactly | PrintJob.PageRange with FromPage/ToPage ensures same data sent to PDF printer |
| SC-004: Excel matches preview exactly | PageBoundary.StartRow/EndRow indices extract exact rows for ClosedXML |
| SC-006: Settings restored after restart | PrintSettings JSON persistence in AppData |

---

## Extension Points (Future)

**Not in Scope for Current Refactor**:

1. **Multi-Grid Export**: Combine data from multiple DataGridViews in single print job
   - Would require: `List<PrintJob>` with aggregation logic
   
2. **Print Templates**: Custom headers/footers, company branding
   - Would require: PrintTemplate entity with logo, custom text placeholders
   
3. **Print History**: Record of what was printed when
   - Would require: PrintHistoryEntry with timestamp, user, job details, page count
   
4. **Scheduled Printing**: Automated print jobs at specific times
   - Would require: ScheduledPrintJob with cron expression, recurrence rules

These extensions would introduce new entities but current data model provides foundation.
