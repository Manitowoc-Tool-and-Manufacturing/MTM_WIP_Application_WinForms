# Phase 0: Research & Technical Decisions

**Feature**: Transaction Viewer Form Redesign  
**Date**: 2025-10-29  
**Status**: Complete

## Overview

This research phase resolves all technical unknowns from the specification and establishes architectural patterns for the Transaction Viewer Form redesign. The feature replaces a 2136-line monolithic form with a SOLID-based architecture using separate UserControls, a ViewModel for business logic, and strict adherence to MTM constitution principles.

---

## Research Question 1: SOLID Decomposition Strategy

### Decision
Decompose the existing Transactions.cs into 5 distinct components:
1. **Transactions.cs** (Form) - Orchestration and layout only (~500 lines)
2. **TransactionSearchControl.cs** (UserControl) - Filter UI (~300 lines)
3. **TransactionGridControl.cs** (UserControl) - DataGridView + pagination (~300 lines)
4. **TransactionDetailPanel.cs** (UserControl) - Side panel display (~200 lines)
5. **Model_Transactions_ViewModel.cs** (ViewModel) - Business logic and state (~400 lines)

### Rationale
- **Single Responsibility**: Each component has one clear purpose (search, display, details, orchestration, logic)
- **Open/Closed**: New filter types can be added to SearchControl without modifying Form or Grid
- **Liskov Substitution**: UserControls can be swapped or mocked for testing
- **Interface Segregation**: Controls expose minimal public API via events
- **Dependency Inversion**: Form depends on UserControl abstractions, ViewModel depends on DAO interfaces

### Alternatives Considered
- **Option A: Keep monolithic form with helper methods** - Rejected: Still violates SRP, difficult to test
- **Option B: Use MVVM pattern with data binding** - Rejected: WinForms data binding is limited, adds complexity
- **Option C: Create 10+ micro-components** - Rejected: Over-engineering, maintenance burden

### Implementation Guidelines
- **Event-driven communication**: UserControls raise events (SearchRequested, RowSelected), Form handles and delegates to ViewModel
- **ViewModel access**: Form creates ViewModel in constructor, passes to controls when needed (e.g., for async operations)
- **Progress reporting**: Form owns Helper_StoredProcedureProgress, passes to ViewModel methods
- **Error handling**: ViewModel catches exceptions, returns Model_Dao_Result, Form displays via Service_ErrorHandler

---

## Research Question 2: Existing Stored Procedure Compatibility

### Decision
Use existing stored procedures **without modification**:
- `inv_transactions_Search` - Accepts 11 filter parameters, returns paginated results
- `inv_transactions_SmartSearch` - Dynamic WHERE clause for advanced queries
- `inv_transactions_GetAnalytics` - Summary statistics (total IN/OUT/TRANSFER)

### Rationale
- **Zero database risk**: No schema changes, no stored procedure refactoring
- **Battle-tested logic**: Existing procedures have been in production for years
- **Immediate implementation**: No waiting for DBA approval or testing cycles
- **Backward compatibility**: Old form and new form can coexist during migration

### Alternatives Considered
- **Option A: Create new optimized stored procedures** - Rejected: Unnecessary risk, existing procedures perform well
- **Option B: Use dynamic SQL in application** - Rejected: Violates Constitution Principle I (Stored Procedure Only)
- **Option C: Replace with Entity Framework** - Rejected: Violates Constitution, massive refactoring effort

### Parameter Mapping Strategy
C# code removes `p_` prefix, helper adds automatically:
```csharp
// In Model_Transactions_SearchCriteria → Dictionary<string, object>
["PartID"] = criteria.PartID ?? (object)DBNull.Value;      // MySQL: p_PartID
["User"] = criteria.User ?? (object)DBNull.Value;          // MySQL: p_User
["FromLocation"] = criteria.FromLocation ?? (object)DBNull.Value; // MySQL: p_FromLocation
// ... etc
```

### Performance Validation
- **Existing benchmarks**: inv_transactions_Search completes in 0.8-1.5s for 90-day ranges (24,000+ records)
- **Indexing**: Table has indexes on `PartID`, `User`, `ReceiveDate` columns
- **Pagination**: Stored procedure implements LIMIT/OFFSET for client-side pagination (50 records per page)

---

## Research Question 3: WinForms ViewModel Integration Pattern

### Decision
Implement **Passive ViewModel** pattern (not MVVM):
- ViewModel contains business logic and async operations
- ViewModel returns Model_Dao_Result<T> to Form
- Form updates controls imperatively (not via data binding)
- UserControls remain dumb views with event notifications

### Rationale
- **WinForms compatibility**: Avoids complex data binding infrastructure
- **Testability**: ViewModel can be unit tested without UI dependencies
- **Constitution compliance**: Maintains async-first pattern (Principle VI)
- **Simplicity**: Familiar event-driven model for WinForms developers

### Alternatives Considered
- **Option A: Full MVVM with INotifyPropertyChanged** - Rejected: Over-engineering for WinForms, requires binding framework
- **Option B: Keep all logic in Form** - Rejected: Violates SOLID, impossible to test
- **Option C: Use Presenter pattern (MVP)** - Rejected: More complex than needed, introduces additional abstraction layer

### Communication Flow
```
User Action (Button Click)
  ↓
UserControl raises Event (SearchRequested)
  ↓
Form handles Event
  ↓
Form calls ViewModel method (SearchTransactionsAsync)
  ↓
ViewModel calls DAO (Dao_Transactions.SearchAsync)
  ↓
ViewModel processes Model_Dao_Result
  ↓
ViewModel returns Model_Dao_Result to Form
  ↓
Form updates UI (Grid, Status, Progress)
  ↓
Form raises event to UserControl (ResultsUpdated)
```

### ViewModel Responsibilities
- **Validation**: Check search criteria validity before DAO calls
- **Orchestration**: Coordinate multiple DAO calls (e.g., load dropdowns in parallel)
- **Transformation**: Map DataTable rows to Model_Transactions_Core objects
- **Caching**: Store dropdown data (parts, users, locations) for performance
- **Progress**: Accept Helper_StoredProcedureProgress and call ShowProgress/UpdateProgress/ShowSuccess

---

## Research Question 4: Pagination Strategy

### Decision
Implement **server-side pagination** with client-side page navigation:
- Stored procedure accepts `p_Page` and `p_PageSize` parameters
- Client requests pages on-demand (page 1, page 2, etc.)
- Grid displays 50 records per page (configurable via Model_Application_Variables)
- Navigation controls: Previous/Next buttons + page number indicator

### Rationale
- **Performance**: Avoids loading 24,000+ records into memory
- **Responsiveness**: Each page load completes in <500ms
- **Existing pattern**: Database already supports LIMIT/OFFSET in stored procedures
- **User experience**: Familiar pagination controls from other forms

### Alternatives Considered
- **Option A: Load all records, paginate client-side** - Rejected: Memory intensive, slow initial load
- **Option B: Virtual scrolling (load-as-scroll)** - Rejected: Complex implementation, unfamiliar UX
- **Option C: Cursor-based pagination** - Rejected: Requires stored procedure changes, more complex

### Implementation Details
- **Page size**: 50 records (default), configurable in `Model_Application_Variables.TransactionPageSize`
- **Page tracking**: ViewModel stores current page number and total record count
- **Navigation buttons**: Enabled/disabled based on current page (disable Previous on page 1, disable Next on last page)
- **Page indicator**: "Page 1 of 5" label shows current position
- **Jump to page**: Optional future enhancement (not Priority 1)

### Edge Cases
- **Empty results**: Show "No transactions found" message, disable pagination controls
- **Single page**: Hide pagination controls when total records ≤ page size
- **Large result sets**: Total page count calculated from record count (returned by stored procedure or separate COUNT query)

---

## Research Question 5: DataGridView Column Configuration

### Decision
Use **code-based column configuration** (not designer):
- Define columns in `TransactionGridControl.InitializeColumns()` method
- Set column properties: Name, HeaderText, DataPropertyName, Width, AutoSizeMode
- Use DataGridViewTextBoxColumn for all text fields
- Format DateTime columns with custom formatting

### Rationale
- **Maintainability**: Columns defined in code are easier to modify than designer
- **DPI scaling**: Manual column widths adapt to DPI scaling via Core_Themes
- **Type safety**: Strongly typed column references prevent typos
- **Consistency**: Matches pattern used in other MTM forms (Inventory, History)

### Alternatives Considered
- **Option A: Designer-based columns** - Rejected: Designer can corrupt, difficult to diff in git
- **Option B: Auto-generate columns from DataTable** - Rejected: No control over column order, formatting, widths
- **Option C: Custom DataGridViewColumn subclasses** - Rejected: Over-engineering for simple display

### Column Definitions
```csharp
new DataGridViewTextBoxColumn {
    Name = "colID", HeaderText = "ID", DataPropertyName = "ID",
    Width = 80, AutoSizeMode = DataGridViewAutoSizeColumnMode.None
},
new DataGridViewTextBoxColumn {
    Name = "colType", HeaderText = "Type", DataPropertyName = "TransactionType",
    Width = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.None
},
new DataGridViewTextBoxColumn {
    Name = "colPartNumber", HeaderText = "Part Number", DataPropertyName = "PartID",
    Width = 150, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
},
// ... etc
```

### Formatting Strategy
- **DateTime**: Use `DefaultCellStyle.Format = "MM/dd/yy HH:mm"` for compact display
- **Quantity**: Right-align with `DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight`
- **TransactionType**: Display enum value directly (IN/OUT/TRANSFER)
- **Null values**: Display empty string (not "null" or blank space)

---

## Research Question 6: Error Handling Strategy

### Decision
Implement **layered error handling**:
1. **DAO Layer**: Catch MySqlException, wrap in Model_Dao_Result.Failure()
2. **ViewModel Layer**: Validate inputs, propagate Model_Dao_Result failures
3. **Form Layer**: Call Service_ErrorHandler.HandleException() with retry actions
4. **UserControl Layer**: Display validation errors inline (ErrorProvider)

### Rationale
- **Constitution compliance**: Principle VII mandates Service_ErrorHandler (no MessageBox)
- **User experience**: Validation errors show next to offending control
- **Debugging**: Full exception details logged to log_error table
- **Retry support**: Service_ErrorHandler provides retry button for transient failures

### Alternatives Considered
- **Option A: Let exceptions bubble to global handler** - Rejected: Loses context, can't provide retry action
- **Option B: Show all errors in status bar** - Rejected: Easy to miss, no actionable guidance
- **Option C: Modal error dialogs for everything** - Rejected: Interrupts workflow, annoying for validation errors

### Error Severity Classification
- **Low**: Validation errors (empty search, invalid date range) → `Service_ErrorHandler.HandleValidationError()`
- **Medium**: Transient database errors (timeout, connection lost) → `Service_ErrorHandler.HandleException(..., Enum_ErrorSeverity.Medium, retryAction)`
- **High**: Unexpected exceptions (null reference, index out of range) → `Service_ErrorHandler.HandleException(..., Enum_ErrorSeverity.High)`
- **Fatal**: Never used (no unrecoverable errors in transaction viewer)

### Retry Action Pattern
```csharp
try
{
    var result = await _viewModel.SearchTransactionsAsync(criteria, _progressHelper);
    if (!result.IsSuccess)
    {
        Service_ErrorHandler.HandleValidationError(result.Message, "Transaction Search");
        return;
    }
    // Display results
}
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
        retryAction: async () => await SearchTransactionsAsync(), // Pass current method as retry action
        contextData: new Dictionary<string, object> {
            ["SearchCriteria"] = criteria.ToString(),
            ["UserName"] = Model_Application_Variables.CurrentUser?.UserName ?? "Unknown"
        },
        controlName: nameof(Transactions)
    );
}
```

---

## Research Question 7: Theme and DPI Scaling Integration

### Decision
Apply **Core_Themes** in all component constructors:
- Form constructor: `Core_Themes.ApplyDpiScaling(this)` + `Core_Themes.ApplyRuntimeLayoutAdjustments(this)`
- UserControl constructors: Same pattern
- Set `AutoScaleMode = AutoScaleMode.Dpi` for all forms/controls

### Rationale
- **Constitution compliance**: Principle VI mandates responsive UI at all DPI scales
- **Consistency**: Matches pattern in all other MTM forms
- **User experience**: Application renders correctly at 100%, 125%, 150%, 200% DPI
- **Manual validation**: Testable by changing Windows display scaling settings

### Alternatives Considered
- **Option A: Let WinForms handle DPI automatically** - Rejected: Inconsistent results, layout breaks at high DPI
- **Option B: Use PerMonitorV2 DPI awareness** - Rejected: Requires .NET 4.7+ features, not compatible with existing forms
- **Option C: Fixed pixel sizes** - Rejected: Unusable on high-DPI displays

### Layout Considerations
- **TableLayoutPanel**: Use percentage sizing (not absolute pixels) for responsive layout
- **Font sizes**: Core_Themes adjusts font sizes based on DPI scale factor
- **Control spacing**: Use standard padding/margins (10px padding, 5px margins) that scale with DPI
- **Minimum sizes**: Set MinimumSize on DataGridView to prevent collapse at high DPI

---

## Research Question 8: Export to Excel Strategy

### Decision
Use **ClosedXML** library (already included in project):
- Export current search results (respects filters and pagination)
- Include all visible columns (ID, Type, Part, Qty, From, To, User, Date)
- Format headers (bold, background color)
- Auto-size columns for readability
- Filename: `Transactions_[Date]_[User].xlsx`

### Rationale
- **Existing dependency**: ClosedXML already used in Inventory reports
- **User familiarity**: Excel format preferred by manufacturing users
- **Formatting control**: ClosedXML allows cell styling, formulas, etc.
- **Performance**: Generates 1000-row Excel in <5 seconds

### Alternatives Considered
- **Option A: Export to CSV** - Rejected: No formatting, difficult to read large datasets
- **Option B: Use EPPlus library** - Rejected: License issues, unnecessary dependency change
- **Option C: Export to PDF** - Rejected: Not searchable/filterable, users can't manipulate data

### Implementation Pattern
```csharp
public async Task ExportToExcelAsync(List<Model_Transactions_Core> transactions, string filePath)
{
    using var workbook = new XLWorkbook();
    var worksheet = workbook.Worksheets.Add("Transactions");
    
    // Headers
    worksheet.Cell(1, 1).Value = "ID";
    worksheet.Cell(1, 2).Value = "Type";
    // ... etc
    worksheet.Range(1, 1, 1, 8).Style.Font.Bold = true;
    worksheet.Range(1, 1, 1, 8).Style.Fill.BackgroundColor = XLColor.LightGray;
    
    // Data rows
    int row = 2;
    foreach (var txn in transactions)
    {
        worksheet.Cell(row, 1).Value = txn.ID;
        worksheet.Cell(row, 2).Value = txn.TransactionType.ToString();
        // ... etc
        row++;
    }
    
    worksheet.Columns().AdjustToContents();
    await Task.Run(() => workbook.SaveAs(filePath)); // Offload file I/O to background thread
}
```

### Export Scope
- **Current page only**: Export only the 50 records visible on current page
- **All pages**: Optional future enhancement (requires loading all pages into memory)
- **Filtered results**: Export respects current search criteria (only filtered transactions)

---

## Research Question 9: Integration Testing Approach

### Decision
Use **BaseIntegrationTest** pattern with test database:
- Test Dao_Transactions methods against `mtm_wip_application_winforms_test` database
- Use discovery-first workflow (grep_search → verify signatures → write tests)
- Mock ViewModel methods when testing UI event handlers
- Manual validation for end-to-end UI workflows

### Rationale
- **Constitution compliance**: Principle IV mandates manual validation testing
- **Existing pattern**: BaseIntegrationTest used in other DAO test suites
- **Test isolation**: Test database prevents production data corruption
- **ViewModel testability**: Business logic testable without WinForms dependencies

### Alternatives Considered
- **Option A: No tests** - Rejected: High-risk refactoring without safety net
- **Option B: Full UI automation** - Rejected: WinForms UI automation is brittle, expensive
- **Option C: Mock all database calls** - Rejected: Doesn't validate stored procedure integration

### Test Coverage Strategy
- **DAO Layer**: 100% integration tests for SearchAsync, GetAnalyticsAsync methods
- **ViewModel Layer**: Unit tests for validation logic, async orchestration
- **Form Layer**: Manual validation only (event wiring, UI updates)
- **UserControls Layer**: Manual validation only (layout, theme, DPI scaling)

### Test Organization
```
Tests/
├── Integration/
│   ├── Dao_Transactions_Tests.cs        # NEW: DAO integration tests
│   └── BaseIntegrationTest.cs           # EXISTING: Base class with connection string
└── Manual/
    └── TransactionViewer_TestScenarios.md  # NEW: Manual test checklist
```

---

## Research Question 10: Migration and Rollback Strategy

### Decision
Implement **parallel running period** with feature toggle:
- **Week 1-2**: New form opt-in via Settings → Developer → "Use New Transaction Viewer"
- **Week 3-4**: New form default, old form opt-out via "Use Legacy Transaction Viewer"
- **Week 5+**: Retire old form, remove toggle

### Rationale
- **Risk mitigation**: Users can switch back if issues discovered
- **Gradual adoption**: Power users test first, rollout to all users incrementally
- **Feedback collection**: Issues reported and fixed before full transition
- **Zero data loss**: Both forms read from same `inv_transaction` table

### Alternatives Considered
- **Option A: Big bang replacement** - Rejected: High risk, no fallback
- **Option B: A/B testing with random assignment** - Rejected: Confusing for users, hard to support
- **Option C: Keep both forms permanently** - Rejected: Maintenance burden, code duplication

### Rollback Plan
If critical issues discovered:
1. Disable "Use New Transaction Viewer" toggle in Settings
2. Set default to old form via Model_Application_Variables
3. Document issue in GitHub Issues with reproduction steps
4. Fix identified problems in feature branch
5. Re-enable toggle when stable

### Data Migration
**No database migration required** - both forms read from same `inv_transaction` table. No schema changes.

---

## Summary of Decisions

| Research Question | Decision | Key Rationale |
|------------------|----------|---------------|
| **1. SOLID Decomposition** | 5 components (Form, 3 UserControls, ViewModel) | Single Responsibility Principle, testability, <500 lines per file |
| **2. Stored Procedures** | Use existing 3 procedures without modification | Zero database risk, constitution compliance |
| **3. ViewModel Integration** | Passive ViewModel pattern (not MVVM) | WinForms compatibility, testability, simplicity |
| **4. Pagination Strategy** | Server-side pagination (50 records/page) | Performance, responsiveness, existing pattern |
| **5. DataGridView Columns** | Code-based column configuration | Maintainability, DPI scaling, consistency |
| **6. Error Handling** | Layered with Service_ErrorHandler | Constitution compliance, user experience, retry support |
| **7. Theme and DPI Scaling** | Core_Themes in all constructors | Constitution compliance, consistency, manual validation |
| **8. Export to Excel** | ClosedXML library | Existing dependency, user familiarity, formatting control |
| **9. Integration Testing** | BaseIntegrationTest + manual validation | Constitution compliance, existing pattern, test isolation |
| **10. Migration Strategy** | Parallel running with feature toggle | Risk mitigation, gradual adoption, zero data loss |

---

## Implementation Readiness

✅ **All research questions resolved** - No remaining "NEEDS CLARIFICATION" items  
✅ **Constitution compliance verified** - All principles satisfied  
✅ **Existing patterns reused** - No new architectural patterns introduced  
✅ **Dependencies identified** - ClosedXML (existing), Core_Themes, Service_ErrorHandler  
✅ **Risk mitigation planned** - Parallel running, rollback strategy, integration tests  

**Next Phase**: Generate data-model.md (entity definitions) and quickstart.md (development guide).
