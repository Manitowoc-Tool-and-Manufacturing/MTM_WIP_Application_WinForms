# Feature Clarification Questions

**Feature**: Print and Export System Refactor
**Date**: 2025-11-08
**Status**: Pending User Answers

---

## Instructions

Please review each question below and select/provide your answer. After completing all questions, this will be converted to `clarification-answers.md` for the AI assistant to integrate into spec.md.

---

## Q1: Phase 1 Temporary User Message

**Category**: User Experience

**Question**: When users click print buttons during Phase 1 (after removal but before new system), what message should they see?

**Options**:
- **A)** Simple message: "Print functionality is being rebuilt. Coming soon!"
- **B)** Detailed message with timeline: "Print functionality is temporarily unavailable during system upgrade. Expected completion: [DATE]"
- **C)** No message - disable/hide print buttons completely
- **D)** Custom message: _______________________________

**Technical Impact**: Affects how we replace old print button handlers during Phase 1 removal.

**Your Answer**: A

---

## Q2: NuGet Package Naming

**Category**: Architecture

**Question**: What should the NuGet package be named?

**Options**:
- **A)** `MTM.WinForms.PrintExport` (generic MTM namespace)
- **B)** `Manitowoc.WinForms.PrintExport` (full company name)
- **C)** `WinForms.PrintExport.MTM` (functionality-first)
- **D)** Something else: _______________________________

**Technical Impact**: Determines package ID, namespace structure, and discoverability on NuGet.org.

**Your Answer**: A

---

## Q3: NuGet Package License

**Category**: Legal/Distribution

**Question**: What license should the NuGet package use?

**Options**:
- **A)** MIT License (most permissive - allows commercial use, modification, distribution)
- **B)** Apache 2.0 (permissive with patent grant)
- **C)** GPL 3.0 (copyleft - requires derivative works to be open source)
- **D)** Proprietary/Closed (MTM internal use only, no public NuGet distribution)
- **E)** Other: _______________________________

**Technical Impact**: 
- MIT/Apache: Can be used freely by anyone (public NuGet.org)
- GPL: Forces derivative works to be open source
- Proprietary: Private NuGet feed only, not public

**Your Answer**: D

---

## Q4: Multi-Format Export Priority

**Category**: Features

**Question**: Which export formats are MUST-HAVE for Phase 4, and which are NICE-TO-HAVE?

**Rate each format (M = Must-Have, N = Nice-to-Have, S = Skip for now)**:
- PDF Export: M 
- Excel Export: M
- PNG Image Export: S
- JPG Image Export: S
- CSV Export: S (not currently in spec)
- HTML Export: S (not currently in spec)

**Technical Impact**: Determines Phase 4 scope and timeline. Nice-to-Have features can be deferred to version 1.1.0 of NuGet package.

**Your Answers**: 
- PDF Export: M 
- Excel Export: M
- PNG Image Export: S
- JPG Image Export: S
- CSV Export: S
- HTML Export: S

---

## Q5: Print Preview Requirements

**Category**: User Interface

**Question**: Should the print preview support zoom levels beyond the standard options?

**Current Spec**: 25%, 50%, 75%, 100%, 125%, 150%, 200%

**Options**:
- **A)** Keep as-is (7 preset zoom levels)
- **B)** Add "Fit to Width" and "Fit to Page" options
- **C)** Add freeform zoom slider (10%-500%)
- **D)** Custom zoom levels: _______________________________

**Technical Impact**: Affects PrintPreviewControl configuration and zoom ComboBox options.

**Your Answer**: A and B

---

## Q6: Page Range Validation

**Category**: Data Validation

**Question**: How should invalid page ranges be handled (e.g., From=5, To=2)?

**Options**:
- **A)** Auto-correct silently (swap From/To if From > To)
- **B)** Show validation error, prevent refresh
- **C)** Allow but show warning icon
- **D)** Disable Print/Export buttons until valid

**Technical Impact**: Determines validation logic in `PageRangeControl_Leave` event handler.

**Your Answer**: D

---

## Q7: Column Reordering Interface

**Category**: User Interface

**Question**: How should users reorder columns in the print dialog?

**Options**:
- **A)** Drag-and-drop checklist (like current spec implies)
- **B)** Up/Down arrow buttons next to selected column
- **C)** Numeric order textboxes (1, 2, 3...)
- **D)** No reordering - print in DataGridView order only

**Technical Impact**: Affects PrintForm settings panel design and complexity.

**Your Answer**: A and B

---

## Q8: Excel Export Page Range Warning

**Category**: User Experience

**Question**: The spec shows a warning when using page ranges with Excel export (because it's approximate). Should users be able to disable this warning?

**Options**:
- **A)** Always show warning (no way to disable)
- **B)** Add "Don't show again" checkbox on warning dialog
- **C)** Add setting in print dialog to disable warnings
- **D)** Never show warning - just export with approximation

**Technical Impact**: Determines if we need user preference storage for warning suppression.

**Your Answer**: During this implementation the goal will be to NOT have it approximate so D

---

## Q9: NuGet Package Targeting

**Category**: Architecture

**Question**: Which .NET versions should the NuGet package support?

**Current Spec**: net8.0-windows, net6.0-windows

**Options**:
- **A)** Keep as-is (net8.0-windows, net6.0-windows)
- **B)** Add net7.0-windows for completeness
- **C)** Add .NET Framework 4.8 support for legacy projects
- **D)** Only net8.0-windows (reduce maintenance burden)
- **E)** Custom combination: _______________________________

**Technical Impact**: 
- More targets = broader compatibility but harder to maintain
- .NET Framework 4.8 requires different dependencies and code paths

**Your Answer**: Remove Nuget Package Phase

---

## Q10: Progress Dialog Design

**Category**: User Experience

**Question**: The "Generating Preview..." dialog - should it have a cancel button?

**Options**:
- **A)** No cancel button (operation too fast to need it)
- **B)** Cancel button for large datasets (>1000 rows)
- **C)** Always show cancel button with progress bar
- **D)** Show elapsed time counter instead

**Technical Impact**: 
- Cancel button requires CancellationToken support in Core_TablePrinter
- Progress bar requires reporting from rendering engine

**Your Answer**: C and D

---

## Q11: Print Settings Persistence

**Category**: Features

**Question**: Should print settings (printer, orientation, page range) be remembered between sessions?

**Options**:
- **A)** Remember per-grid (different settings for Remove tab vs Transactions tab)
- **B)** Remember globally (same settings for all grids)
- **C)** Always reset to defaults
- **D)** Remember only printer selection, reset everything else

**Technical Impact**: Determines if we need Model_Print_CoreSettings persistence to file/database.

**Your Answer**: Remember Printer, Column Order and Selection (per-grid), reset everything else

---

## Q12: Phase Timeline Flexibility

**Category**: Project Management

**Question**: The spec estimates 9-13 days total. Is this timeline flexible?

**Options**:
- **A)** Hard deadline - must complete in 13 days
- **B)** Soft deadline - can extend if quality requires
- **C)** Phase 6 (NuGet) can be deferred to separate project
- **D)** Only Phase 1-3 are critical, rest can wait

**Technical Impact**: Affects which features are MVP and which can be deferred.

**Your Answer**: Whatever is easiest for implementation, as this entire spec will be implemented using AI so timeframe means nothing

---

## Q13: Removed Entry Points Documentation

**Category**: Process

**Question**: Should removed-entry-points.md include screenshots of where print buttons were located?

**Options**:
- **A)** Yes - include screenshots for clarity
- **B)** No - text descriptions sufficient
- **C)** Only for complex UI locations
- **D)** Record screen video instead

**Technical Impact**: Documentation completeness for Phase 5 reconnection.

**Your Answer**: B

---

## Q14: Error Handling Strategy

**Category**: Architecture

**Question**: How should the new print system handle errors?

**Current MTM App**: Uses Service_ErrorHandler with severity levels

**For NuGet Package**:
- **A)** Throw standard exceptions (ArgumentException, InvalidOperationException, etc.)
- **B)** Return Result<T> pattern with success/error status
- **C)** Use callbacks/events for error notification
- **D)** Mix: Throw exceptions for programming errors, return results for operational errors

**Technical Impact**: 
- NuGet package can't depend on MTM's Service_ErrorHandler
- Must use standard .NET patterns

**Your Answer**: Remove Nuget Package Phase

---

## Q15: Print Preview Theme Integration

**Category**: User Interface

**Question**: Should the print dialog match the MTM app's theme system (Model_Shared_UserUiColors)?

**In MTM App**:
- **A)** Yes - integrate with existing theme system (Phase 5)
- **B)** No - use standard Windows Forms appearance
- **C)** Partially - only main colors (background, text)

**In NuGet Package**:
- **A)** Provide theme hooks/interfaces for consumers to implement
- **B)** Standard appearance only (no theming)
- **C)** Include basic theme support (light/dark mode)

**Technical Impact**: Complexity of PrintForm design and NuGet package flexibility.

**Your Answer (MTM App)**: A
**Your Answer (NuGet Package)**: Remove Nuget Package Phase

---

## Summary

**Total Questions**: 15
**Categories**: User Experience (5), Architecture (4), Features (3), User Interface (3), Project Management (1), Process (1), Legal/Distribution (1), Data Validation (1)

---

## Next Steps

1. **Answer all questions above**
2. **Save this file as `clarification-answers.md`**
3. **Share with AI assistant**
4. **Assistant will update spec.md with clarified requirements**

---

## Notes

- Some questions may not apply - mark as N/A if appropriate
- Can provide additional context for any answer
- Multiple-choice answers can be combined (e.g., "A + B")
- Custom answers encouraged when options don't fit
