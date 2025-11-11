# Feature Specification: Print and Export System Refactor

**Feature Branch**: `006-print-and-export`  
**Created**: 2025-11-08  
**Status**: Draft  
**Input**: Complete refactor of print/export functionality with systematic removal of old system and phased implementation of new architecture using Compact Sidebar UI

---

## Overview

Complete redesign of the print and export system to properly handle page ranges, preview generation, and multi-format exports (PDF, Excel). The refactor addresses fundamental architectural issues where page range calculations were based on row estimates rather than actual print system pagination.

### Current Problems

1. **Incorrect Page Range Handling**: Row-based estimation (31 rows/page) doesn't match actual print pagination
2. **Double Filtering**: Data filtered multiple times causing incorrect output
3. **Mixed Responsibilities**: Export logic scattered across multiple classes
4. **No Progress Indication**: Long operations block UI without feedback
5. **Inconsistent Behavior**: Page ranges work differently for PDF vs Excel

### Solution Approach

The refactor will be implemented in five phases to minimize disruption and ensure quality at each step:

1. **Phase 1: Systematic Removal** - Remove all existing print/export code, replace with temporary user messages
2. **Phase 2: Core Infrastructure** - Build new print foundation with accurate pagination
3. **Phase 3: Print Dialog** - New UI with Compact Sidebar design, live preview, and settings
4. **Phase 4: Export System** - Unified export architecture for PDF and Excel with exact page ranges
5. **Phase 5: Integration** - Connect new system to existing UI entry points

---

## User Scenarios & Testing

### User Story 1 - Print DataGridView with Preview (Priority: P1)

A manufacturing supervisor needs to print a filtered view of transactions for a production meeting. They want to see exactly what will print before sending it to the printer.

**Why this priority**: Core functionality that users depend on daily. Without working print, users cannot generate physical reports for shop floor meetings.

**Independent Test**: Can be fully tested by loading sample data, opening print dialog, viewing preview, and printing to PDF or physical printer. Delivers immediate value for report generation.

**Acceptance Scenarios**:

1. **Given** user has transaction data displayed in grid, **When** user clicks Print button, **Then** print dialog opens showing accurate preview of first page
2. **Given** print dialog is open with preview, **When** user navigates to page 5, **Then** preview shows actual content that will appear on page 5 (not row-based estimate)
3. **Given** user selects landscape orientation, **When** preview refreshes, **Then** page content reorganizes to landscape layout with correct pagination
4. **Given** user selects pages 3-7, **When** user clicks Print, **Then** only pages 3-7 are sent to printer (verified by page count)

---

### User Story 2 - Export to PDF/Excel with Page Ranges (Priority: P2)

A quality manager needs to export specific pages of a large inspection report to share with external auditors. They need the export to match the preview exactly.

**Why this priority**: Essential for compliance and auditing workflows. Users frequently need to share subsets of large reports without manual editing.

**Independent Test**: Can be tested by generating multi-page report, selecting page range 5-10, exporting to PDF and Excel, and verifying both formats contain exactly the same data as preview pages 5-10.

**Acceptance Scenarios**:

1. **Given** preview shows 25 pages of data, **When** user selects pages 8-12 and exports to PDF, **Then** PDF contains exactly 5 pages matching preview pages 8-12
2. **Given** user exports pages 1-3 to Excel, **When** Excel file is opened, **Then** Excel contains exactly the rows visible on preview pages 1-3 (no approximation)
3. **Given** user changes column visibility, **When** preview refreshes and user exports, **Then** export reflects current column selection

---

### User Story 3 - Customize Print Columns and Order (Priority: P3)

A production planner wants to print a simplified report with only Part Number, Quantity, and Location columns for shop floor workers.

**Why this priority**: Improves usability and reduces paper waste by allowing users to print only relevant information.

**Independent Test**: Can be tested by opening print dialog, hiding Status and Notes columns, reordering remaining columns, and verifying printed output matches selections.

**Acceptance Scenarios**:

1. **Given** grid has 8 columns displayed, **When** user unchecks 3 columns in print dialog, **Then** preview shows only 5 selected columns
2. **Given** columns are in order A, B, C, D, **When** user moves column C to first position using arrow buttons, **Then** preview updates to show C, A, B, D order
3. **Given** user saves column preferences, **When** user restarts application and reopens print dialog for same grid, **Then** previously selected columns and order are restored

---

### User Story 4 - Monitor Long-Running Operations (Priority: P3)

A user generates a preview of a 1500-row dataset. They want to see progress and be able to cancel if it takes too long.

**Why this priority**: Prevents UI freezes and gives users control over long operations. Quality-of-life improvement that enhances perceived performance.

**Independent Test**: Can be tested by loading large dataset, opening print dialog, observing progress indicator with elapsed time, and clicking cancel button to abort operation.

**Acceptance Scenarios**:

1. **Given** user initiates preview generation for large dataset, **When** rendering begins, **Then** modal progress dialog appears with "Generating Preview..." message
2. **Given** progress dialog is showing, **When** 5 seconds elapse, **Then** elapsed time counter shows "00:05" and continues incrementing
3. **Given** preview is still rendering, **When** user clicks Cancel button, **Then** operation stops immediately and dialog closes without error
4. **Given** preview completes successfully, **When** dialog auto-closes, **Then** user sees complete preview with all pages available

---

### Edge Cases

- **Empty Grid**: What happens when user tries to print empty DataGridView? → Show message "No data to print"
- **Single Row**: How does pagination handle 1 row of data? → Shows single page with header and one data row
- **Very Wide Tables**: What if table has 20+ columns that don't fit on page? → Automatic column wrapping or horizontal overflow (need design decision)
- **Invalid Page Range**: User enters From=10, To=5? → Disable Print/Export buttons until corrected
- **Page Range Exceeds Total**: User enters From=1, To=100 but only 12 pages exist? → Auto-clamp to max pages or show validation warning
- **Printer Unavailable**: What if selected printer goes offline? → Show error dialog with option to select different printer
- **Export Permission Denied**: User tries to export to read-only location? → Show error with suggestion to choose different save location
- **Theme Colors**: How do themed DataGridView colors translate to print? → Use printer-friendly defaults (black text, white background) regardless of theme
- **Long Operation Cancel**: What if user cancels during page 8 of 50? → Clean up partial render, release resources, close dialog gracefully

---

## Requirements

### Functional Requirements

#### Phase 1: Removal
- **FR-001**: System MUST remove all existing print/export code files without breaking compilation
- **FR-002**: System MUST replace print button handlers with temporary user message: "Print functionality is being rebuilt. Coming soon!"
- **FR-003**: System MUST document all removed entry points in `removed-entry-points.md` with text descriptions (no screenshots)
- **FR-004**: System MUST verify solution builds successfully after removal

#### Phase 2: Core Infrastructure
- **FR-005**: System MUST render DataTable content to PrintDocument with accurate pagination based on actual page layout
- **FR-006**: System MUST calculate rows-per-page dynamically based on page size, margins, font, and orientation
- **FR-007**: System MUST return actual page count after preview generation (not row-based estimate)
- **FR-008**: System MUST support landscape and portrait orientations
- **FR-009**: System MUST support color and grayscale print modes
- **FR-010**: System MUST pass ALL data to print system and use Windows PrintRange/FromPage/ToPage for page selection

#### Phase 3: Print Dialog UI
- **FR-011**: Print dialog MUST use Compact Sidebar layout (80% preview, 20% settings sidebar)
- **FR-012**: Settings sidebar MUST have collapsible sections with icons: 🖨️ Printer, 📄 Setup, 📑 Range, ⚙️ Columns
- **FR-013**: Preview area MUST show PrintPreviewControl with navigation buttons (First ◀◀, Prev ◀, Next ▶, Last ▶▶)
- **FR-014**: Preview MUST display accurate page counter in format "Page X / Y" matching PrintDocument.PageNumber property exactly (no tolerance for estimation)
- **FR-015**: Zoom control MUST support: 25%, 50%, 75%, 100%, 125%, 150%, 200%, Fit to Width, Fit to Page
- **FR-016**: Page range selection MUST support: All Pages, Current Page, Custom (From/To)
- **FR-017**: System MUST disable Print/Export buttons when page range is invalid (From > To)
- **FR-018**: Column management MUST support visibility checkboxes and drag-and-drop reordering with Up/Down arrow buttons
- **FR-019**: System MUST show "Generating Preview..." modal dialog with cancel button, elapsed time counter, and progress bar during preview generation
- **FR-020**: Print dialog MUST integrate with MTM theme system (Model_Shared_UserUiColors)
- **FR-021**: Print dialog MUST apply Core_Themes.ApplyDpiScaling() and ApplyRuntimeLayoutAdjustments()

#### Phase 4: Export System
- **FR-022**: System MUST export to PDF with exact page ranges matching preview (no approximation)
- **FR-023**: System MUST export to Excel with exact page ranges matching preview (no approximation)
- **FR-024**: PDF export MUST use "Microsoft Print to PDF" printer with same print settings as preview
- **FR-025**: Excel export MUST use print rendering engine to determine exact page boundaries
- **FR-026**: Export dropdown MUST show only PDF and Excel options (PNG/JPG/CSV/HTML deferred)

#### Phase 5: Integration
- **FR-027**: System MUST reconnect all original print entry points documented in Phase 1
- **FR-028**: Print dialog MUST accept DataGridView as constructor parameter
- **FR-029**: System MUST remember printer selection, column order, and column visibility per grid between sessions
- **FR-030**: System MUST reset orientation, color mode, page range, and zoom level each session (do not persist)

### Key Entities

- **PrintJob**: Represents complete print/export configuration including source data (DataTable), column visibility/order, title, printer settings, orientation, color mode, and page range
- **PrintSettings**: Stores per-grid user preferences (printer name, column selections, column order) in file or database for persistence between sessions
- **PageBoundary**: Represents exact row ranges that appear on each printed page (calculated by rendering engine, not estimated)

---

## Relevant Instruction Files

### For Implementation Phase:
- `.github/instructions/csharp-dotnet8.instructions.md` - C# language features, naming conventions, WinForms patterns, async/await
- `.github/instructions/mysql-database.instructions.md` - Stored procedure standards (if persistence to database used)
- `.github/instructions/testing-standards.instructions.md` - Manual validation approach, success criteria patterns
- `.github/instructions/documentation.instructions.md` - XML documentation, code comments
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling requirements for print dialog
- `.github/instructions/winforms-responsive-layout.instructions.md` - TableLayoutPanel patterns for dialog layout
- `.github/instructions/ui-compliance/theming-compliance.instructions.md` - Theme system integration requirements

### For Quality Assurance:
- `.github/instructions/security-best-practices.instructions.md` - Input validation, file path security
- `.github/instructions/performance-optimization.instructions.md` - Async I/O, UI responsiveness
- `.github/instructions/code-review-standards.instructions.md` - Quality checklist, review process

**When to reference**: Implementation team should review relevant instruction files during `/speckit.plan` and `/speckit.tasks` phases.

---

## Success Criteria

### Measurable Outcomes

- **SC-001**: Users can open print dialog and see accurate preview in under 2 seconds for datasets up to 100 rows
- **SC-002**: Page count shown in preview matches actual printed page count with 100% accuracy (no row-based estimation errors)
- **SC-003**: PDF export of pages 5-10 contains exactly the same content as preview pages 5-10 (verified by manual comparison)
- **SC-004**: Excel export of pages 1-3 contains exactly the same rows as preview pages 1-3 (no approximation, verified by row count match)
- **SC-005**: Users can cancel long-running preview generation within 500ms of clicking Cancel button
- **SC-006**: Print dialog settings (printer, columns) are restored when reopening dialog for same grid after application restart
- **SC-007**: Print functionality is restored for all original entry points (Remove tab, Transactions tab, context menus) with zero regression
- **SC-008**: 100% of test scenarios in User Stories 1-4 pass manual validation
- **SC-009**: Solution builds without errors after Phase 1 removal and after each subsequent phase completion
- **SC-010**: Print dialog renders correctly at 100%, 125%, 150%, and 200% DPI scaling

---

## Assumptions

1. **Printer Availability**: Users have access to at least "Microsoft Print to PDF" printer (installed by default on Windows 10+)
2. **DataGridView Data Source**: All grids use DataTable as underlying data source or can easily convert to DataTable
3. **Theme System**: MTM theme system (Model_Shared_UserUiColors, Core_Themes) is fully functional and accessible
4. **File System Access**: Application has write permissions to user's Documents folder or temp directory for export operations
5. **Windows Forms**: Application remains WinForms-based (not migrating to WPF/Avalonia during this refactor)
6. **Print System**: Windows GDI+ printing APIs are sufficient for requirements (no need for advanced PDF libraries like iTextSharp)
7. **Column Metadata**: DataGridView columns have meaningful HeaderText that can be used for print headers
8. **Session Persistence**: Settings can be persisted to local JSON file in user AppData folder (no database requirement)
9. **Cancellation Support**: Core_TablePrinter rendering can be refactored to support CancellationToken for responsive cancel operations
10. **Timeline Flexibility**: Implementation timeline is flexible since entire spec will be AI-driven (quality over speed)

---

## Dependencies

### Internal Dependencies
- MTM Theme System (Model_Shared_UserUiColors, Core_Themes)
- Service_ErrorHandler for error display
- LoggingUtility for operation logging
- Existing DataGridView implementations in MainForm tabs

### External Dependencies
- .NET 8.0 Windows Forms APIs (PrintDocument, PrintPreviewControl, PrintDialog)
- Windows GDI+ for rendering
- System.Drawing for graphics operations
- Microsoft Print to PDF printer (Windows 10+ default)
- ClosedXML library for Excel export (already in project)

### Removed Dependencies
- No NuGet package creation (MTM app only)
- No multi-.NET-version targeting (net8.0-windows only)
- No public distribution requirements (internal MTM use)

---

## Out of Scope

The following items are explicitly excluded from this refactor:

1. **Image Export Formats**: PNG, JPG image exports (deferred to future version)
2. **CSV Export**: Comma-separated value export (deferred)
3. **HTML Export**: Web-based export format (deferred)
4. **NuGet Package**: No reusable library creation (MTM app only)
5. **Print Templates**: Custom page templates or branding (use defaults)
6. **Advanced Formatting**: Cell borders, colors, fonts carry over to print (print uses black text, white background)
7. **Print History**: No record of what was printed/exported
8. **Multi-Grid Export**: Cannot combine data from multiple grids in single print job
9. **Scheduled Printing**: No automated/scheduled print jobs
10. **Email Integration**: No direct "Print to Email" functionality

---

## Phase Implementation Details

### Phase 1: Systematic Removal

**Objective**: Create clean slate by removing all old print code

**Files to Delete**:
- Forms/Shared/PrintForm.cs
- Forms/Shared/PrintForm.Designer.cs
- Helpers/Helper_PrintManager.cs
- Helpers/Helper_ExportManager.cs
- Core/Core_TablePrinter.cs
- Models/Model_Print_Core_Job.cs
- Models/Model_Print_CoreSettings.cs

**Code to Replace**:
- All print button click handlers → Show MessageBox: "Print functionality is being rebuilt. Coming soon!"
- Search entire solution for PrintForm references and replace with TODO markers

**Documentation**:
- Create `removed-entry-points.md` listing all entry points with method names and context

**Completion Criteria**:
- Solution builds successfully
- All print buttons show temporary message
- No active references to deleted files

---

### Phase 2: Core Infrastructure

**New Components**:

**Core_TablePrinter.cs**:
- Render DataTable to PrintDocument with page-aware tracking
- Calculate dynamic rows-per-page based on layout
- Support landscape/portrait, headers/footers
- Return actual page count

**Helper_PrintManager.cs**:
- Orchestrate print operations
- Create PrintDocument from DataTable
- Apply printer settings
- Generate preview
- Execute print jobs

**Model_Print_Core_Job.cs**:
- Configuration object with DataTable, columns, settings
- Windows PrintRange/FromPage/ToPage properties

**Completion Criteria**:
- Preview shows correct pagination
- Page count matches actual print output
- Landscape/portrait rendering works

---

### Phase 3: Print Dialog

**PrintForm.cs Design** (Mockup 3: Compact Sidebar):

**Preview Area (80% width)**:
- PrintPreviewControl full area
- Toolbar: First/Prev/Next/Last buttons, Page X/Y counter, Zoom dropdown
- Gray viewport (#95a5a6), white page with shadow

**Settings Sidebar (20% width)**:
- 🖨️ Printer (default open): Dropdown
- 📄 Setup (default open): Orientation radio, Color mode radio
- 📑 Range (default open): All/Current/Custom radio, From/To inputs
- ⚙️ Columns (default collapsed): Visibility checkboxes, Up/Down arrows

**Action Bar**:
- Export dropdown (PDF/Excel)
- Print button (green)
- Cancel button (gray)

**Behaviors**:
- Progress dialog during preview generation
- Disable Print/Export when range invalid
- Theme integration via Model_Shared_UserUiColors
- DPI scaling via Core_Themes

**Completion Criteria**:
- All UI elements functional
- Preview updates on settings change
- Progress cancellation works
- Theme colors applied correctly

---

### Phase 4: Export System

**PDF Export**:
- Use "Microsoft Print to PDF"
- Pass ALL data with FromPage/ToPage
- Exact page range matching preview

**Excel Export**:
- Use print rendering to determine page boundaries
- Extract only rows on selected pages
- No approximation warnings

**Helper_ExportManager.cs**:
- Static methods: ExportToPdf, ExportToExcel
- File path validation
- Success/error reporting

**Completion Criteria**:
- PDF matches preview exactly
- Excel matches preview exactly
- Export dropdown works
- File permissions handled

---

### Phase 5: Integration

**Tasks**:
1. Find all TODO markers from Phase 1
2. Replace with PrintForm constructor calls
3. Pass DataGridView to dialog
4. Test each entry point
5. Verify settings persistence

**Entry Points**:
- Remove tab print button
- Transactions tab print button
- Context menu print options

**Completion Criteria**:
- All original print buttons restored
- Settings persistence working
- No TODO markers remain
- Full regression testing passed

---

## Testing Strategy

### Manual Validation Approach

Since this is WinForms with no automated UI testing, use comprehensive manual test plans:

**Unit-Level Tests** (code-based):
- Helper_PrintManager_Tests.cs
- Helper_ExportManager_Tests.cs
- Core_TablePrinter_Tests.cs
- Model_Print_Core_Job_Tests.cs

**Integration Tests** (workflow-based):
- Small dataset (10 rows) - Fast validation
- Medium dataset (100 rows) - Typical use
- Large dataset (1000+ rows) - Performance/pagination
- Wide dataset (20 columns) - Layout handling
- Empty dataset - Error handling

**End-to-End Scenarios**:
1. Load grid → Print all pages
2. Load grid → Print pages 3-7
3. Load grid → Export PDF pages 1-5
4. Load grid → Export Excel pages 10-15
5. Load grid → Change columns → Print
6. Load grid → Change orientation → Print
7. Large grid → Cancel preview
8. Restart app → Verify settings restored

### Acceptance Testing Checklist

- [ ] Phase 1: All print buttons show "Coming soon" message
- [ ] Phase 2: Preview pagination matches actual print output
- [ ] Phase 3: All UI controls functional, theme integrated
- [ ] Phase 4: PDF export matches preview exactly
- [ ] Phase 4: Excel export matches preview exactly
- [ ] Phase 5: All entry points reconnected
- [ ] Cross-phase: Settings persist between sessions
- [ ] Cross-phase: DPI scaling works at 100%-200%
- [ ] Cross-phase: No compilation errors
- [ ] Cross-phase: No memory leaks during large operations

---

## Risk Mitigation

### Risk 1: Users Need Print During Refactor
**Impact**: High  
**Probability**: Medium  
**Mitigation**: 
- Show clear message in Phase 1: "Coming soon"
- Keep old code in git history for emergency rollback
- Flexible timeline allows pausing if critical need arises

### Risk 2: Excel Exact Page Ranges Technically Complex
**Impact**: Medium  
**Probability**: Medium  
**Mitigation**:
- Design uses print rendering engine (same as PDF)
- If too complex, fall back to PDF-only initially
- Excel can be version 1.1 feature

### Risk 3: Preview Generation Too Slow
**Impact**: Medium  
**Probability**: Low  
**Mitigation**:
- Implement cancellation (FR-019)
- Show progress with elapsed time
- Optimize rendering for common cases

### Risk 4: Settings Persistence Bugs
**Impact**: Low  
**Probability**: Medium  
**Mitigation**:
- Use simple JSON file in AppData
- Fail gracefully if corrupt (reset to defaults)
- Version the settings format

---

## Notes

- All new files use same names as deleted files (clean replacement)
- Git history preserves old implementation for reference
- Architecture follows standard Windows printing best practices
- MTM app only (no NuGet distribution)
- Timeline flexible (AI-driven implementation, quality over speed)
- Mockup 3 (Compact Sidebar) selected for optimal screen space usage
- Clarifications incorporated: Simple Phase 1 message, PDF/Excel only, exact page ranges (no approximation), full theme integration, settings persistence per-grid
