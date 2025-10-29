# Implementation Tasks: Transaction Viewer Form Redesign

**Feature ID**: F005  
**Branch**: `005-transaction-viewer-form`  
**Last Updated**: 2025-10-29  
**Status**: Ready for Implementation

---

## Overview

Complete architectural redesign of the Transactions form (`Forms/Transactions/Transactions.cs`) from a 2136-line monolithic implementation to a clean SOLID-based architecture with <500 lines per component. This task breakdown organizes work by user story priority to enable incremental delivery and independent testing.

**Goal**: Replace existing transaction viewer with maintainable, testable implementation that provides superior UX while adhering to MTM constitution principles.

**Components**: 5 new files (3 UserControls, 1 ViewModel, 1 DetailDialog) + 1 refactored Form + 1 refactored DAO

---

## Task Summary

- **Total Tasks**: 64
- **Estimated Effort**: 5-7 days (single developer)
- **MVP Scope**: P1 User Stories (US-001 through US-005) = 35 tasks
- **Priority Distribution**:
  - Setup & Foundation: 9 tasks
  - P1 (Core Viewing): 26 tasks (US-001 through US-005)
  - P2 (Advanced Features): 19 tasks (US-006 through US-010)
  - P3 (Analytics): 5 tasks (US-011 through US-012)
  - Polish & Integration: 5 tasks

**Parallel Opportunities**: 18 tasks marked [P] can execute concurrently

---

## Phase 1: Setup & Infrastructure

### Initial Project Setup

- [ ] **T001** - Create TransactionSearchCriteria model
  **File**: `Models/TransactionSearchCriteria.cs`
  **Description**: Implement search criteria encapsulation with validation methods (IsValid, IsDateRangeValid) and ToString summary. Include properties for PartID, User, FromLocation, ToLocation, Operation, TransactionType, DateFrom, DateTo, Notes.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Follow naming conventions, nullable patterns, and file-scoped namespaces
  **Reference**: `.github/instructions/documentation.instructions.md` - Include XML documentation for all public members
  **Acceptance**: Model compiles, has XML docs, validation methods work correctly, nullable annotations correct

- [ ] **T002** [P] - Create TransactionSearchResult model
  **File**: `Models/TransactionSearchResult.cs`
  **Description**: Implement pagination wrapper with Transactions list, TotalRecordCount, CurrentPage, PageSize properties. Include calculated properties: TotalPages, HasPreviousPage, HasNextPage, PaginationSummary.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Use expression-bodied properties for calculated values
  **Reference**: `.github/instructions/documentation.instructions.md` - Document pagination logic in XML comments
  **Acceptance**: Model compiles, calculated properties return correct values, pagination logic verified

- [ ] **T003** [P] - Create TransactionAnalytics model
  **File**: `Models/TransactionAnalytics.cs`
  **Description**: Implement analytics summary with TotalTransactions, TotalIN, TotalOUT, TotalTRANSFER counts. Include calculated percentage properties and DateRange tuple.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Handle division by zero in percentage calculations
  **Acceptance**: Model compiles, percentage calculations correct, handles zero totals gracefully

- [ ] **T004** - Create Controls/Transactions directory structure
  **Description**: Create new directory `Controls/Transactions/` for transaction-specific UserControls. This separates transaction viewer components from shared controls.
  **Acceptance**: Directory exists, can be committed to git

- [ ] **T005** - Create Forms/Transactions/TransactionDetailDialog shell
  **File**: `Forms/Transactions/TransactionDetailDialog.cs` and `.Designer.cs`
  **Description**: Create new Form for modal transaction detail display. Basic shell with OK/Close buttons only - implementation in P2 phase.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Apply Core_Themes.ApplyDpiScaling in constructor
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Set AutoScaleMode = AutoScaleMode.Dpi
  **Acceptance**: Form compiles, designer opens without errors, themes applied

- [ ] **T006** - Refactor Dao_Transactions: Add SearchAsync method signature
  **File**: `Data/Dao_Transactions.cs`
  **Description**: Add method signature for SearchAsync (returns DaoResult<List<Model_Transactions>>) with TransactionSearchCriteria parameter. Add MapDataRowToModel helper method signature. Implementation in US-001 tasks.
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Follow stored procedure invocation pattern with Helper_Database_StoredProcedure
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Use async/await patterns, ConfigureAwait(false) in DAO layer
  **Acceptance**: Method signatures compile, XML documentation complete, code ready for implementation

- [ ] **T007** - Create TransactionViewModel shell class
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Create ViewModel class with standard region organization (#region Fields, Properties, Constructors, Public Methods, Private Methods). Add constructor, private Dao_Transactions field. Method implementations in story-specific tasks.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Follow region organization and method ordering standards
  **Acceptance**: Class compiles, follows region organization, under 100 lines at this stage

- [ ] **T008** - Create integration test shell
  **File**: `Tests/Integration/Dao_Transactions_Tests.cs`
  **Description**: Create test class inheriting from BaseIntegrationTest. Add [TestClass] attribute and GetTestConnectionString helper reference. Test implementations in story-specific tasks.
  **Reference**: `.github/instructions/integration-testing.instructions.md` - Follow discovery-first workflow and BaseIntegrationTest pattern
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation as primary QA approach
  **Acceptance**: Test class compiles, can run empty test suite, connection string available

- [ ] **T009** - [CHECKPOINT] - Foundation Complete
  **Description**: Verify all foundation files compile, designer files open, directory structure correct. All models have XML documentation. Integration test infrastructure ready.
  **Acceptance**: Solution builds with 0 errors, 0 warnings. All new files follow constitution principles.

---

## Phase 2: Priority 1 (P1) - Core Viewing

### User Story 1 (US-001): View All Transactions

*Goal: Grid displays user's transactions with all columns, loads within 2s for last 30 days, newest first.*

- [ ] **T010** - Implement TransactionGridControl UI layout
  **File**: `Controls/Transactions/TransactionGridControl.cs` and `.Designer.cs`
  **Description**: Create UserControl with DataGridView (fills parent), StatusStrip with pagination controls (Previous/Next buttons, page label, record count label). Apply Core_Themes in constructor.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Follow region organization, apply DPI scaling in constructor
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Use TableLayoutPanel for responsive layout if needed
  **Acceptance**: Control renders in designer, themes applied, controls laid out correctly at 100%-200% DPI

- [ ] **T011** - Configure TransactionGridControl DataGridView columns
  **File**: `Controls/Transactions/TransactionGridControl.cs`
  **Description**: In InitializeColumns() method, create columns programmatically: ID (80px), Type (100px), Part Number (150px, Fill), Quantity (80px), From Location (120px), To Location (120px), User (100px), Date (140px). Set sorting, alignment, format strings.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Use AutoScaleMode for DPI-aware column widths
  **Acceptance**: Columns display correctly, sort on click, formatting applied (date: MM/dd/yy HH:mm, quantity: right-align)

- [ ] **T012** - Implement TransactionGridControl DisplayResults method
  **File**: `Controls/Transactions/TransactionGridControl.cs`
  **Description**: Create DisplayResults(TransactionSearchResult) method that binds Transactions list to DataGridView, updates pagination buttons (enable/disable), displays page indicator ("Page 1 of 5"), shows record count. Raise PageChanged event on button clicks.
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Suspend/resume layout during bulk updates
  **Acceptance**: Grid updates, pagination UI correct, buttons enable/disable properly, events raised

- [ ] **T013** - Implement TransactionGridControl row selection event
  **File**: `Controls/Transactions/TransactionGridControl.cs`
  **Description**: Handle DataGridView SelectionChanged event, raise RowSelected(Model_Transactions) event with selected transaction. Extract Model_Transactions from selected row.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Use null-conditional operators for safe navigation
  **Acceptance**: RowSelected event fires on selection, passes correct transaction object, handles no selection gracefully

- [ ] **T014** - Implement Dao_Transactions.SearchAsync method
  **File**: `Data/Dao_Transactions.cs`
  **Description**: Implement SearchAsync method body: map TransactionSearchCriteria to Dictionary<string, object> parameters (no p_ prefix), call Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync with "inv_transactions_Search", map DataTable rows to List<Model_Transactions> using MapDataRowToModel, wrap in DaoResult.
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Follow stored procedure pattern, parameter naming (no p_ prefix), DaoResult wrapper
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Use async/await, ConfigureAwait(false)
  **Acceptance**: Method calls stored procedure correctly, returns DaoResult with transactions, handles errors

- [ ] **T015** - Implement Dao_Transactions.MapDataRowToModel helper
  **File**: `Data/Dao_Transactions.cs`
  **Description**: Create private MapDataRowToModel(DataRow) method that extracts columns to Model_Transactions properties. Handle DBNull.Value for nullable fields (BatchNumber, Notes, FromLocation, ToLocation, Operation). Parse DateTime, int, TransactionType enum.
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Handle DBNull.Value correctly
  **Acceptance**: Method maps all fields correctly, handles nulls, parses enum values

- [ ] **T016** - Implement TransactionViewModel.SearchTransactionsAsync method
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Create SearchTransactionsAsync(criteria, page, pageSize, progress) method: validate criteria with IsValid/IsDateRangeValid, call _dao.SearchAsync, report progress at 20%/40%/80%, wrap results in TransactionSearchResult, update _currentCriteria and _currentResults fields, return DaoResult<TransactionSearchResult>.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async patterns, null safety
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Progress reporting pattern
  **Acceptance**: Method orchestrates search, validates inputs, reports progress, returns DaoResult

- [ ] **T017** [P] - Create integration test: Dao_Transactions.SearchAsync with date range
  **File**: `Tests/Integration/Dao_Transactions_Tests.cs`
  **Description**: Add test method SearchAsync_WithDateRange_ReturnsTransactions: arrange criteria with DateFrom/DateTo last 30 days, act call dao.SearchAsync, assert IsSuccess true, Data not null, Count > 0.
  **Reference**: `.github/instructions/integration-testing.instructions.md` - Follow discovery-first workflow, use grep_search to verify actual method signatures
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation as primary approach
  **Acceptance**: Test passes, verifies method signature matches implementation, null-safe assertions

- [ ] **T018** - Refactor Transactions.cs: Create shell with ViewModel field
  **File**: `Forms/Transactions/Transactions.cs`
  **Description**: Replace existing 2136-line implementation with new shell: private _viewModel and _progressHelper fields, InitializeComponent call, Core_Themes application, InitializeViewModel/InitializeProgressReporting/WireUpEvents methods (stubs). Save old implementation to .backup file first.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Region organization, DPI scaling in constructor
  **Reference**: `.github/instructions/code-review-standards.instructions.md` - Follow refactoring checklist
  **Acceptance**: Form compiles, designer opens, themes applied, under 150 lines at this stage, old code backed up

- [ ] **T019** - Add TransactionGridControl to Transactions.cs designer
  **File**: `Forms/Transactions/Transactions.Designer.cs`
  **Description**: Add TransactionGridControl instance to form, dock fill in main content area. Wire up PageChanged and RowSelected events in WireUpEvents() method in code-behind.
  **Acceptance**: Control appears in designer, docked correctly, events wired, compiles

- [ ] **T020** - Implement Transactions.cs SearchControl_SearchRequested handler
  **File**: `Forms/Transactions/Transactions.cs`
  **Description**: Implement async event handler that calls ExecuteSearchAsync(criteria). Include try/catch with Service_ErrorHandler.HandleException, retry action, context data.
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Error handling without exposing internals
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Async void for event handlers
  **Acceptance**: Handler calls ViewModel, displays results or errors, retry works, async void pattern correct

- [ ] **T021** - Implement Transactions.cs ExecuteSearchAsync method
  **File**: `Forms/Transactions/Transactions.cs`
  **Description**: Create private async Task ExecuteSearchAsync(criteria, page=1) method: call _viewModel.SearchTransactionsAsync with _progressHelper, check result.IsSuccess, call gridControl.DisplayResults(result.Data), handle errors with Service_ErrorHandler.HandleValidationError.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async patterns, error handling
  **Acceptance**: Method executes search, updates grid on success, shows user-friendly errors on failure

- [ ] **T022** - Implement Transactions.cs GridControl_PageChanged handler
  **File**: `Forms/Transactions/Transactions.cs`
  **Description**: Implement async event handler for pagination: extract new page number, call ExecuteSearchAsync with current criteria and new page.
  **Acceptance**: Handler changes pages correctly, maintains current search criteria

- [ ] **T023** - [CHECKPOINT] - US-001 Manual Validation
  **Description**: Manual test scenario: Open Transactions form, verify grid displays transactions from last 30 days, verify columns show correct data, verify newest transactions first, verify load time <2s. Test pagination (Previous/Next buttons).
  **Acceptance**: All US-001 acceptance criteria pass manual validation

### User Story 2 (US-002): Search by Part Number

*Goal: Part search field with autocomplete, results within 1s, validation for empty search.*

- [ ] **T024** - Implement TransactionSearchControl UI layout
  **File**: `Controls/Transactions/TransactionSearchControl.cs` and `.Designer.cs`
  **Description**: Create UserControl with TableLayoutPanel: Part Number ComboBox (AutoComplete mode), User ComboBox, Location ComboBox, Operation TextBox, Date range DateTimePickers with quick filter radio buttons (Today/Week/Month/All), Transaction type CheckBoxes (IN/OUT/TRANSFER), Notes TextBox, Search/Reset/Export/Print buttons. Apply Core_Themes.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - DPI scaling in constructor
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - TableLayoutPanel percentage sizing
  **Acceptance**: Control renders in designer, all controls laid out, themes applied, tab order logical

- [ ] **T025** - Implement TransactionSearchControl BuildCriteria method
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: Create private BuildCriteria() method that reads UI controls, populates TransactionSearchCriteria object, trims strings, handles null/empty values. Return criteria object.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Null safety, string handling
  **Acceptance**: Method correctly extracts all filter values, handles empty controls, returns valid criteria

- [ ] **T026** - Implement TransactionSearchControl Search button handler
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: Handle btnSearch_Click: call BuildCriteria, validate with IsValid and IsDateRangeValid, show Service_ErrorHandler.HandleValidationError if invalid, raise SearchRequested event if valid.
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Input validation at UI boundary
  **Acceptance**: Button validates inputs, shows user-friendly errors, raises event on success

- [ ] **T027** - Implement TransactionViewModel LoadPartsAsync method
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Create LoadPartsAsync() method: call Dao_Part.GetAllPartsAsync (or equivalent), cache results in _cachedParts field, return DaoResult<List<string>>.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async patterns, caching strategy
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Cache dropdown data for performance
  **Acceptance**: Method loads parts, caches correctly, handles errors

- [ ] **T028** - Implement Transactions.cs InitializeDropdownsAsync method
  **File**: `Forms/Transactions/Transactions.cs`
  **Description**: Create async method that calls _viewModel.LoadPartsAsync, LoadUsersAsync, LoadLocationsAsync in parallel with Task.WhenAll, populates searchControl ComboBoxes, handles errors.
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Parallel async operations with Task.WhenAll
  **Acceptance**: Dropdowns populate on form load, parallel loading works, errors handled

- [ ] **T029** - Configure Part Number ComboBox autocomplete
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: Set ComboBox properties: AutoCompleteMode = SuggestAppend, AutoCompleteSource = ListItems, data source to parts list.
  **Acceptance**: Autocomplete works, suggests matches as user types

- [ ] **T030** - Add search control to Transactions.cs designer
  **File**: `Forms/Transactions/Transactions.Designer.cs`
  **Description**: Add TransactionSearchControl instance, dock top or place in TableLayoutPanel. Wire SearchRequested event in WireUpEvents().
  **Acceptance**: Search control appears in designer, docked correctly, event wired

- [ ] **T031** [CHECKPOINT] - US-002 Manual Validation
  **Description**: Manual test: Enter part number with autocomplete suggestions, click Search, verify results appear within 1s. Test empty search shows validation error message.
  **Acceptance**: All US-002 acceptance criteria pass manual validation

### User Story 3 (US-003): Filter by Date Range

*Goal: Date range picker with quick filters (Today/Week/Month/All), validation for invalid ranges.*

- [ ] **T032** - Implement TransactionSearchControl quick filter handlers
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: Handle radio button CheckedChanged events for Today/Week/Month/All: set DateFrom/DateTo DateTimePicker values accordingly. Today = 00:00-23:59, Week = Monday-Sunday, Month = 1st-last day.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - DateTime calculations
  **Acceptance**: Quick filters set date pickers correctly, values match expected ranges

- [ ] **T033** - Implement date range validation in BuildCriteria
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: In BuildCriteria method, after reading date pickers, validate DateFrom <= DateTo with TransactionSearchCriteria.IsDateRangeValid(). If invalid, keep criteria but validation will fail in button handler.
  **Acceptance**: Invalid date ranges detected, validation error shown to user

- [ ] **T034** [CHECKPOINT] - US-003 Manual Validation
  **Description**: Manual test: Select "Today" quick filter, verify dates set correctly. Select "This Week", verify Monday-Sunday range. Select "This Month", verify full month range. Manually enter DateTo < DateFrom, verify validation error shown.
  **Acceptance**: All US-003 acceptance criteria pass manual validation

### User Story 4 (US-004): Filter by Transaction Type

*Goal: Checkboxes for IN/OUT/TRANSFER, at least one required, real-time filtering.*

- [ ] **T035** - Implement transaction type checkbox layout
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: In designer, add CheckBox controls for IN, OUT, TRANSFER with default all checked. Use FlowLayoutPanel or GroupBox for visual grouping.
  **Acceptance**: Checkboxes laid out clearly, all checked by default

- [ ] **T036** - Implement transaction type validation in BuildCriteria
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: In BuildCriteria, read checkbox states, build TransactionType flags or list. Validate at least one checked, show error if all unchecked.
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Client-side validation as UX aid
  **Acceptance**: At least one type required, error shown if none selected

- [ ] **T037** [CHECKPOINT] - US-004 Manual Validation
  **Description**: Manual test: Uncheck all transaction types, click Search, verify validation error. Check only IN, verify results show only IN transactions. Check IN and OUT, verify combined results.
  **Acceptance**: All US-004 acceptance criteria pass manual validation

### User Story 5 (US-005): Admin View All Users

*Goal: User dropdown shows all users for admin, regular users see only their own data.*

- [ ] **T038** - Implement TransactionViewModel LoadUsersAsync method
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Create LoadUsersAsync() method: call Dao_User.GetAllUsersAsync (or equivalent), filter based on Model_AppVariables.CurrentUser.IsAdmin, cache in _cachedUsers field, return DaoResult<List<string>>.
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Role-based access control
  **Acceptance**: Method loads users, filters based on admin flag, caches correctly

- [ ] **T039** - Populate User dropdown in Transactions.cs
  **File**: `Forms/Transactions/Transactions.cs`
  **Description**: In InitializeDropdownsAsync, call _viewModel.LoadUsersAsync, populate searchControl User ComboBox. If regular user, set to current user and disable control.
  **Acceptance**: Dropdown populated, admin sees all users, regular user sees only self with disabled dropdown

- [ ] **T040** [P] - Create integration test: Admin vs regular user filtering
  **File**: `Tests/Integration/Dao_Transactions_Tests.cs`
  **Description**: Add test AdminSearchAsync_AllUsers_ReturnsAllData and RegularUserSearchAsync_FiltersByUser: verify admin can search all users, regular user only sees own transactions.
  **Reference**: `.github/instructions/integration-testing.instructions.md` - Discovery-first workflow
  **Acceptance**: Tests pass, validate role-based filtering

- [ ] **T041** [CHECKPOINT] - US-005 Manual Validation
  **Description**: Manual test as admin: Verify User dropdown shows all users. Manual test as regular user: Verify dropdown shows only current user and is disabled.
  **Acceptance**: All US-005 acceptance criteria pass manual validation

- [ ] **T042** [Story: P1] [CHECKPOINT] - P1 Complete: All Core Viewing Features Implemented
  **Description**: Verify all P1 user stories (US-001 through US-005) pass manual validation. Verify solution builds with 0 errors, 0 warnings. Run MCP validation tools (validate_dao_patterns, validate_error_handling, check_xml_docs). Verify all files under line count limits.
  **Acceptance**: P1 MVP complete, ready for user acceptance testing, all constitution principles satisfied

---

## Phase 3: Priority 2 (P2) - Advanced Features

### User Story 6 (US-006): Multi-Field Search

*Goal: Combined search across Part + Location + User + Date + Notes with partial text matching.*

- [ ] **T043** - Implement Notes partial matching in BuildCriteria
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: Read Notes TextBox value, include in TransactionSearchCriteria. No special handling needed (stored procedure handles LIKE matching).
  **Acceptance**: Notes field included in search criteria

- [ ] **T044** - Verify Dao_Transactions.SearchAsync handles multiple filters
  **File**: `Data/Dao_Transactions.cs`
  **Description**: Verify existing SearchAsync implementation passes all non-null criteria parameters to stored procedure. Stored procedure applies AND logic.
  **Acceptance**: Multiple filters work together correctly

- [ ] **T045** [CHECKPOINT] - US-006 Manual Validation
  **Description**: Manual test: Enter Part Number + User + Date + Notes, verify results match all criteria (AND logic). Test partial Notes matching (e.g., search "batch" finds "batch 123").
  **Acceptance**: All US-006 acceptance criteria pass manual validation

### User Story 7 (US-007): Pagination Navigation

*Goal: Page size 50, Previous/Next buttons, page indicator, total record count displayed.*

- [ ] **T046** - Update Model_AppVariables with TransactionPageSize setting
  **File**: `Models/Model_AppVariables.cs`
  **Description**: Add public static int TransactionPageSize property, default value 50. Allow configuration via settings.
  **Acceptance**: Property added, default value 50, configurable

- [ ] **T047** - Implement page size configuration in TransactionViewModel
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Update SearchTransactionsAsync to use Model_AppVariables.TransactionPageSize instead of hardcoded 50. Add PageSize property getter.
  **Acceptance**: Page size configurable, ViewModel uses setting

- [ ] **T048** - Add jump to page feature in TransactionGridControl
  **File**: `Controls/Transactions/TransactionGridControl.cs`
  **Description**: Add TextBox for page number input, add Go button, validate input (1 to TotalPages), raise PageChanged event with entered page.
  **Acceptance**: User can jump to specific page, validation works, out-of-range handled

- [ ] **T049** - Display total record count in grid status bar
  **File**: `Controls/Transactions/TransactionGridControl.cs`
  **Description**: In DisplayResults method, update status label with "245 records | Page 1 of 5" format using TransactionSearchResult properties.
  **Acceptance**: Status label shows total records and pagination info

- [ ] **T050** [CHECKPOINT] - US-007 Manual Validation
  **Description**: Manual test: Perform search with >50 results, verify Previous/Next buttons work, verify page indicator updates, verify jump to page works, verify total count displayed.
  **Acceptance**: All US-007 acceptance criteria pass manual validation

### User Story 8 (US-008): Export to Excel

*Goal: Export button respects filters, includes all columns, filename `Transactions_[Date]_[User].xlsx`, completes within 5s for 1000 records.*

- [ ] **T051** - Implement TransactionViewModel ExportToExcelAsync method
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Create ExportToExcelAsync(filePath, progress) method: use ClosedXML to create workbook, add worksheet "Transactions", write headers (bold, gray background), write data rows from _currentResults.Transactions, auto-size columns, save to filePath, return DaoResult<string>.
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - Offload file I/O to background thread with Task.Run
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async patterns for I/O operations
  **Acceptance**: Method generates Excel file, formatted correctly, completes within 5s for 1000 records

- [ ] **T052** - Add Export button handler to TransactionSearchControl
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: Add btnExport_Click handler: raise ExportRequested event. Wire event in Transactions.cs to show SaveFileDialog, generate filename "Transactions_{DateTime.Now:yyyyMMdd}_{userName}.xlsx", call _viewModel.ExportToExcelAsync.
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Validate file path before writing
  **Acceptance**: Export button triggers workflow, SaveFileDialog shown, filename format correct

- [ ] **T053** [P] - Create integration test: ExportToExcelAsync generates valid file
  **File**: `Tests/Integration/TransactionViewModel_Tests.cs`
  **Description**: Add test ExportToExcelAsync_ValidData_CreatesExcelFile: arrange sample transactions, act call ExportToExcelAsync with temp file path, assert file exists, verify Excel can open file, verify row count.
  **Reference**: `.github/instructions/testing-standards.instructions.md` - File-based integration test pattern
  **Acceptance**: Test passes, Excel file valid, row count matches input

- [ ] **T054** [CHECKPOINT] - US-008 Manual Validation
  **Description**: Manual test: Perform search, click Export button, choose save location, verify file created with correct filename format, open in Excel and verify all columns present, verify data matches grid display. Test with 1000+ records, verify completes within 5s.
  **Acceptance**: All US-008 acceptance criteria pass manual validation

### User Story 9 (US-009): Print Reports

*Goal: Print preview with formatted layout, header shows filters and date range, footer shows page numbers and totals.*

- [ ] **T055** - Implement TransactionViewModel PrintPreviewAsync method
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Create PrintPreviewAsync() method: use Core_DgvPrinter or System.Drawing.Printing to generate print document from _currentResults.Transactions, include header (filter criteria, date range), footer (page numbers, total count), return DaoResult<PrintDocument>.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Printing infrastructure pattern
  **Acceptance**: Method generates print document, header/footer formatted correctly

- [ ] **T056** - Add Print button handler to TransactionSearchControl
  **File**: `Controls/Transactions/TransactionSearchControl.cs`
  **Description**: Add btnPrint_Click handler: raise PrintRequested event. Wire event in Transactions.cs to call _viewModel.PrintPreviewAsync, show PrintPreviewDialog.
  **Acceptance**: Print button triggers workflow, print preview dialog shown

- [ ] **T057** [CHECKPOINT] - US-009 Manual Validation
  **Description**: Manual test: Perform search, click Print button, verify print preview shows, verify header displays filter criteria and date range, verify footer shows page numbers and totals, verify layout professional.
  **Acceptance**: All US-009 acceptance criteria pass manual validation

### User Story 10 (US-010): Detail Side Panel

*Goal: Click row shows side panel, displays all transaction fields, shows related transactions (same batch, same part).*

- [ ] **T058** - Implement TransactionDetailPanel UI layout
  **File**: `Controls/Transactions/TransactionDetailPanel.cs` and `.Designer.cs`
  **Description**: Create UserControl with TableLayoutPanel: label/value pairs for all Model_Transactions fields (ID, Type, Part, Batch, Qty, Locations, Operation, User, Date, Notes), "Related Transactions" DataGridView (compact view), "View Batch History" button. Apply Core_Themes.
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Responsive layout with TableLayoutPanel
  **Acceptance**: Control renders in designer, labels/values laid out vertically, themes applied

- [ ] **T059** - Implement TransactionDetailPanel DisplayTransaction method
  **File**: `Controls/Transactions/TransactionDetailPanel.cs`
  **Description**: Create DisplayTransaction(Model_Transactions) method: populate label/value pairs from transaction properties, call LoadRelatedTransactionsAsync to populate DataGridView.
  **Acceptance**: Method displays all transaction fields, handles nulls gracefully

- [ ] **T060** - Implement TransactionViewModel LoadRelatedTransactionsAsync method
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Create LoadRelatedTransactionsAsync(batchNumber) method: call _dao.SearchAsync with criteria filtered by BatchNumber, return DaoResult<List<Model_Transactions>>.
  **Acceptance**: Method loads related transactions, handles empty results

- [ ] **T061** - Add TransactionDetailPanel to Transactions.cs designer
  **File**: `Forms/Transactions/Transactions.Designer.cs`
  **Description**: Add TransactionDetailPanel to form, dock right or place in TableLayoutPanel column. Handle GridControl RowSelected event to call detailPanel.DisplayTransaction.
  **Acceptance**: Detail panel appears in designer, event wired, displays when row selected

- [ ] **T062** [CHECKPOINT] - US-010 Manual Validation
  **Description**: Manual test: Perform search, click on transaction row, verify detail panel displays all fields correctly, verify related transactions shown (same batch), verify "View Batch History" button present.
  **Acceptance**: All US-010 acceptance criteria pass manual validation

- [ ] **T063** [Story: P2] [CHECKPOINT] - P2 Complete: All Advanced Features Implemented
  **Description**: Verify all P2 user stories (US-006 through US-010) pass manual validation. Verify solution builds with 0 errors, 0 warnings. Run MCP validation tools. Verify file sizes still under limits (<500 lines per file).
  **Acceptance**: P2 features complete, ready for extended user acceptance testing

---

## Phase 4: Priority 3 (P3) - Analytics & Visualization

### User Story 11 (US-011): Transaction Analytics

*Goal: Summary cards (Total, IN, OUT, TRANSFER), charts, analytics respect current date filter.*

- [ ] **T064** - Implement Dao_Transactions GetAnalyticsAsync method
  **File**: `Data/Dao_Transactions.cs`
  **Description**: Create GetAnalyticsAsync(userName, isAdmin, dateFrom, dateTo) method: call Helper_Database_StoredProcedure with "inv_transactions_GetAnalytics", map single result row to TransactionAnalytics model, return DaoResult<TransactionAnalytics>.
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Stored procedure invocation pattern
  **Acceptance**: Method calls stored procedure, maps result correctly, wraps in DaoResult

- [ ] **T065** - Implement TransactionViewModel GetAnalyticsAsync method
  **File**: `Models/TransactionViewModel.cs`
  **Description**: Create GetAnalyticsAsync(progress) method: extract dateFrom/dateTo from _currentCriteria, call _dao.GetAnalyticsAsync with current user info, report progress, return DaoResult<TransactionAnalytics>.
  **Acceptance**: Method orchestrates analytics retrieval, uses current search filters

- [ ] **T066** [P] - Create analytics display UserControl (optional future enhancement)
  **File**: `Controls/Transactions/TransactionAnalyticsControl.cs`
  **Description**: Create UserControl with summary cards (Labels showing counts and percentages), chart controls (optional - placeholder for now). DisplayAnalytics(TransactionAnalytics) method populates cards.
  **Acceptance**: Control renders summary cards, updates with analytics data

- [ ] **T067** [CHECKPOINT] - US-011 Manual Validation
  **Description**: Manual test (if analytics control added): Perform search with date filter, verify analytics cards show correct counts and percentages, verify analytics match filtered results (not all data).
  **Acceptance**: US-011 acceptance criteria pass if analytics UI implemented, or marked as future enhancement

### User Story 12 (US-012): Batch History View

*Goal: "View Batch History" button opens filtered view, shows full timeline from IN to current state.*

- [ ] **T068** - Implement "View Batch History" button handler
  **File**: `Controls/Transactions/TransactionDetailPanel.cs`
  **Description**: Handle btnViewBatchHistory_Click: get BatchNumber from current transaction, raise ViewBatchHistoryRequested event with batch number. Wire event in Transactions.cs to create new TransactionSearchCriteria with BatchNumber filter, call ExecuteSearchAsync.
  **Acceptance**: Button triggers batch history view, search results filtered by batch

- [ ] **T069** [CHECKPOINT] - US-012 Manual Validation
  **Description**: Manual test: Select transaction with batch number, click "View Batch History" button, verify search updates to show only transactions for that batch, verify timeline shows full item lifecycle (IN to current).
  **Acceptance**: All US-012 acceptance criteria pass manual validation

---

## Phase 5: Polish & Integration

### Final Integration Tasks

- [ ] **T070** - Run complete MCP validation suite
  **Description**: Execute all MCP tools: validate_dao_patterns, validate_error_handling, check_xml_docs (95%+ coverage), analyze_performance (no HIGH issues), check_security (no CRITICAL/HIGH issues), validate_build (0 errors, 0 warnings).
  **Reference**: `.github/instructions/code-review-standards.instructions.md` - Quality gates and validation tools
  **Acceptance**: All MCP validation tools pass

- [ ] **T071** - Verify file size limits
  **Description**: Check line counts for all new/refactored files: Transactions.cs <500, TransactionSearchControl.cs <300, TransactionGridControl.cs <300, TransactionDetailPanel.cs <200, TransactionViewModel.cs <400. Refactor if exceeded.
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Region organization helps manage file size
  **Acceptance**: All files under line count limits per FR-001

- [ ] **T072** - Complete all 8 manual validation scenarios
  **Description**: Execute manual test scenarios from spec.md: Basic Search, Part Number Search, Multi-Filter, Export, Pagination, Error Handling, Detail View, Performance. Document results.
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation checklist
  **Acceptance**: All 8 scenarios pass, results documented

- [ ] **T073** - Update Help documentation
  **File**: `Documentation/Help/` (HTML help files)
  **Description**: Update help system with new Transaction Viewer UI screenshots, search instructions, export/print guidance. Update F1 context-sensitive help mappings.
  **Reference**: `.github/instructions/documentation.instructions.md` - Help system documentation standards
  **Acceptance**: Help docs updated, screenshots current, F1 help works

- [ ] **T074** [CHECKPOINT] - Final Deployment Readiness
  **Description**: Verify all tasks complete, all tests pass, documentation updated, constitution compliance verified, ready for parallel running migration period. Create feature toggle in Settings for "Use New Transaction Viewer".
  **Acceptance**: Feature complete, tested, documented, ready for user rollout

---

## Dependencies

### Task Dependency Graph (by User Story)

```
Foundation (T001-T009)
    ↓
US-001 (T010-T023) ← Core viewing, blocks all other stories
    ↓
    ├── US-002 (T024-T031) ← Part search
    ├── US-003 (T032-T034) ← Date filters
    ├── US-004 (T035-T037) ← Type filters
    └── US-005 (T038-T041) ← Admin users
        ↓
        P1 Checkpoint (T042)
            ↓
            ├── US-006 (T043-T045) ← Multi-field search
            ├── US-007 (T046-T050) ← Pagination
            ├── US-008 (T051-T054) ← Excel export
            ├── US-009 (T055-T057) ← Print reports
            └── US-010 (T058-T062) ← Detail panel
                ↓
                P2 Checkpoint (T063)
                    ↓
                    ├── US-011 (T064-T067) ← Analytics (optional)
                    └── US-012 (T068-T069) ← Batch history
                        ↓
                        Integration (T070-T074)
```

### Blocking Tasks

- **T001-T009**: Foundation must complete before any user story
- **T010-T023** (US-001): Core viewing must complete before other P1 stories
- **T042**: P1 checkpoint must complete before P2 stories
- **T063**: P2 checkpoint must complete before P3 stories

### Parallel Execution Opportunities

**Foundation Phase (can run concurrently):**
- T002 (TransactionSearchResult) || T003 (TransactionAnalytics) || T004 (Directory structure)

**US-001 Phase:**
- T011 (Grid columns) || T017 (Integration test) after T010 completes
- T010 (GridControl UI) || T006 (DAO signature) || T007 (ViewModel shell) || T008 (Test shell)

**P1 Phases:**
- US-002, US-003, US-004, US-005 can run in parallel after US-001 complete

**P2 Phases:**
- US-006, US-007, US-008, US-009, US-010 can run in parallel after P1 complete
- T053 (Export test) can run parallel with T051-T052

**P3 Phases:**
- US-011, US-012 can run in parallel after P2 complete

**Total Parallel Tasks**: 18 tasks marked [P]

---

## Implementation Strategy

### MVP Definition (Minimum Viable Product)

**Scope**: Priority 1 User Stories (US-001 through US-005)
- **Tasks**: T001-T042 (42 tasks)
- **Estimated Effort**: 3-4 days
- **Deliverable**: Core transaction viewing with search, filters, pagination
- **Success Criteria**: All P1 acceptance criteria pass, constitution compliance verified

**Rationale**: P1 provides complete replacement for existing transaction viewer's core functionality. Users can perform 95% of daily tasks with P1 alone.

### Incremental Delivery Plan

1. **Sprint 1 (Days 1-4)**: Foundation + P1 (T001-T042)
   - Deliverable: MVP ready for internal testing
   - Milestone: Feature toggle enabled for developers

2. **Sprint 2 (Days 5-6)**: P2 Advanced Features (T043-T063)
   - Deliverable: Export, print, detail panel
   - Milestone: Feature toggle enabled for power users

3. **Sprint 3 (Day 7)**: P3 Analytics + Integration (T064-T074)
   - Deliverable: Full feature set, documentation, deployment
   - Milestone: Feature toggle enabled for all users

### Rollback Plan

If critical issues discovered during parallel running period:
1. Disable "Use New Transaction Viewer" toggle in Settings
2. Document issue in GitHub Issues with reproduction steps
3. Fix in feature branch, retest, re-enable toggle
4. Old Transactions.cs remains available as fallback throughout migration

---

## Task Execution Notes

### Before Starting Implementation

1. Review all instruction files listed in plan.md "Relevant Instruction Files" section
2. Read quickstart.md for development setup and common pitfalls
3. Verify MAMP MySQL running, test database accessible
4. Create feature branch: `git checkout -b 005-transaction-viewer-form`

### During Implementation

1. Mark tasks in progress before starting work (manually or via MCP tool)
2. Follow discovery-first workflow for DAO/integration tests (grep_search → verify signatures → implement)
3. Run MCP validation tools after completing each phase
4. Commit after each checkpoint, push to remote

### After Completion

1. Complete final integration tasks (T070-T074)
2. Generate post-refactor compliance report
3. Submit PR with MCP validation results
4. Schedule user acceptance testing session

---

**Last Updated**: 2025-10-29  
**Version**: 1.0  
**Ready for**: Implementation

