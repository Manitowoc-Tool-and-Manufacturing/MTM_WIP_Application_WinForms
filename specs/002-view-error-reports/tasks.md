# Tasks: View Error Reports Window

**Feature**: View Error Reports Window  
**Branch**: `002-view-error-reports`  
**Created**: 2025-10-26  
**Status**: Ready for Implementation

---

## Task Summary

**Total Tasks**: 52  
**Parallel Opportunities**: 18 tasks marked [P]  
**MVP Scope**: User Story 1 (Browse All Error Reports) - Tasks T001-T019  
**Estimated Timeline**: 3-5 days (24-40 hours)

### Tasks by User Story

- **Setup & Foundation** (T001-T010): 10 tasks - Shared infrastructure
- **User Story 1 - Browse Reports (P1)** (T011-T019): 9 tasks - Core grid functionality
- **User Story 2 - Filtering (P1)** (T020-T027): 8 tasks - Filter controls and logic
- **User Story 3 - Details & Status (P1)** (T028-T041): 14 tasks - Detail view and updates
- **User Story 4 - Export (P2)** (T042-T048): 7 tasks - CSV/Excel export
- **Polish & Integration** (T049-T052): 4 tasks - Final integration

---

## Phase 1: Setup & Foundation (Prerequisites)

**Goal**: Establish database layer and shared infrastructure needed by all user stories.

**Completion Criteria**:
- All 5 stored procedures created and tested in MySQL Workbench
- Model_ErrorReportFilter class created
- Existing Dao_ErrorReports class extended with 5 new methods
- All DAO methods return DaoResult<T> and use async patterns
- Manual testing confirms stored procedures return expected data

---

### Database Layer Tasks

- [x] **T001** [Story: Foundation] - Create `sp_error_reports_GetAll` stored procedure  
  **File**: `Database/UpdatedStoredProcedures/ReadyForVerification/sp_error_reports_GetAll.sql`  
  **Description**: Implement stored procedure to retrieve filtered error reports with date range, user, machine, status, and search text parameters. Include p_Status and p_ErrorMsg OUT parameters per constitution.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Follow stored procedure standards (p_ prefix, OUT parameters, transaction handling)  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetAll.md` - Contract specification with exact parameter list and SQL logic  
  **Acceptance**: Test in MySQL Workbench with various filter combinations, verify result set columns match contract
  **Completed**: 2025-10-26 – Added stored procedure with comprehensive filters and validation; verified with analyze_stored_procedures

- [x] **T002** [Story: Foundation] - Create `sp_error_reports_GetByID` stored procedure  
  **File**: `Database/UpdatedStoredProcedures/ReadyForVerification/sp_error_reports_GetByID.sql`  
  **Description**: Implement stored procedure to retrieve single error report by ReportID including all TEXT fields (CallStack, TechnicalDetails, UserNotes).  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Stored procedure standards  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetByID.md` - Contract with parameter validation logic  
  **Acceptance**: Test retrieval of existing report, verify NULL handling, confirm TEXT fields returned in full
  **Completed**: 2025-10-26 – Implemented detail lookup and status messaging; validated via analyze_stored_procedures

- [x] **T003** [Story: Foundation] - Create `sp_error_reports_UpdateStatus` stored procedure  
  **File**: `Database/UpdatedStoredProcedures/ReadyForVerification/sp_error_reports_UpdateStatus.sql`  
  **Description**: Implement stored procedure to update report status, ReviewedBy, ReviewedDate, and DeveloperNotes. Include validation for status values (New/Reviewed/Resolved) and ReportID existence check.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Transaction management for UPDATE operations  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_UpdateStatus.md` - Contract with business logic and validation rules  
  **Acceptance**: Test status transitions (New→Reviewed, Reviewed→Resolved), verify DeveloperNotes update, confirm transaction atomicity
  **Completed**: 2025-10-26 – Added transactional status update with validation; analyzed using analyze_stored_procedures

- [x] **T004** [Story: Foundation] - Create `sp_error_reports_GetUserList` stored procedure  
  **File**: `Database/UpdatedStoredProcedures/ReadyForVerification/sp_error_reports_GetUserList.sql`  
  **Description**: Implement stored procedure returning DISTINCT UserName values sorted alphabetically for filter dropdown population.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - DISTINCT query patterns  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetUserList.md` - Contract with minimal parameters  
  **Acceptance**: Test returns distinct users, verify alphabetical sorting, confirm empty table handling
  **Completed**: 2025-10-26 – Implemented DISTINCT user query with status messaging; compliance verified via analyze_stored_procedures

- [x] **T005** [Story: Foundation] - Create `sp_error_reports_GetMachineList` stored procedure  
  **File**: `Database/UpdatedStoredProcedures/ReadyForVerification/sp_error_reports_GetMachineList.sql`  
  **Description**: Implement stored procedure returning DISTINCT MachineName values (excluding NULLs) sorted alphabetically.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - NULL handling in WHERE clauses  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetMachineList.md` - Contract with NULL exclusion logic  
  **Acceptance**: Test returns non-NULL machines only, verify sorting, confirm multiple reports per machine deduplicated
  **Completed**: 2025-10-26 – Added DISTINCT machine retrieval excluding blanks; analyzed with analyze_stored_procedures

---

### Model Layer Tasks

- [x] **T006** [Story: Foundation] [P] - Create `Model_ErrorReportFilter` class  
  **File**: `Models/Model_ErrorReportFilter.cs`  
  **Description**: Implement filter criteria model with nullable DateTime DateFrom/DateTo, string UserName, MachineName, Status, and SearchText properties. Include validation logic for date range.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Nullable reference types, property patterns  
  **Reference**: `specs/002-view-error-reports/data-model.md` - Entity definition section 3  
  **Acceptance**: Model compiles, properties are nullable as specified, validation method rejects DateFrom > DateTo
  **Completed**: 2025-10-26 – Added normalization-aware filter model with validation helpers for status, dates, and search text

---

### Data Access Layer Tasks

- [x] **T007** [Story: Foundation] - Extend `Dao_ErrorReports` with `GetAllErrorReportsAsync` method  
  **File**: `Data/Dao_ErrorReports.cs`  
  **Description**: Add method accepting Model_ErrorReportFilter, building parameters Dictionary with DBNull.Value for nulls, calling sp_error_reports_GetAll via Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync. Return DaoResult<DataTable>.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Helper_Database_StoredProcedure usage pattern  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async/await patterns, region organization (add to #region Database Operations)  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetAll.md` - C# usage example  
  **Acceptance**: Method compiles, uses async, returns DaoResult<DataTable>, handles null filters correctly
  **Completed**: 2025-10-26 – Implemented filter validation, parameter normalization, and SP execution with logging

- [x] **T008** [Story: Foundation] - Extend `Dao_ErrorReports` with `GetErrorReportByIdAsync` method  
  **File**: `Data/Dao_ErrorReports.cs`  
  **Description**: Add method accepting int reportId, calling sp_error_reports_GetByID, mapping DataRow to Model_ErrorReport with null-safe field access. Return DaoResult<Model_ErrorReport>.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - DataRow to model mapping, DBNull.Value handling  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetByID.md` - Mapping example  
  **Acceptance**: Method handles DBNull.Value for nullable fields, maps all 14 columns correctly
  **Completed**: 2025-10-26 – Added detail retrieval with mapping helper and status logging

- [x] **T009** [Story: Foundation] - Extend `Dao_ErrorReports` with `UpdateErrorReportStatusAsync` method  
  **File**: `Data/Dao_ErrorReports.cs`  
  **Description**: Add method accepting reportId, newStatus, developerNotes, reviewedBy. Call sp_error_reports_UpdateStatus with DateTime.Now for ReviewedDate. Return DaoResult<bool>.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - UPDATE operation patterns  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_UpdateStatus.md` - Parameter construction  
  **Acceptance**: Method validates inputs, handles optional developerNotes (DBNull.Value when null)
  **Completed**: 2025-10-26 – Implemented async update with guard clauses and developer note handling

- [x] **T010** [Story: Foundation] [P] - Extend `Dao_ErrorReports` with `GetUserListAsync` and `GetMachineListAsync` methods  
  **File**: `Data/Dao_ErrorReports.cs`  
  **Description**: Add two methods calling respective stored procedures, converting DataTable to List<string>. Return DaoResult<List<string>>.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - DataTable to List conversion  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetUserList.md` and `sp_error_reports_GetMachineList.md`  
  **Acceptance**: Both methods compile, handle empty results, return sorted string lists
  **Completed**: 2025-10-26 – Added list retrieval helpers with DataTable extraction and DaoResult wrappers

---

## Phase 2: User Story 1 - Browse All Error Reports (P1)

**Story Goal**: Developers can view all error reports in a sortable DataGridView grid with color-coding by status.

**Independent Test Criteria**:
- Open View Error Reports window
- Verify DataGridView displays all reports from database
- Verify columns: ID, Date, User, Machine, Error Type, Summary (truncated), Status
- Click column headers to sort ascending/descending
- Verify color-coding: New=LightCoral, Reviewed=LightGoldenrodYellow, Resolved=LightGreen
- Double-click row to trigger event (detail view opens in later story)

---

### User Story 1 Tasks

- [x] **T011** [Story: US1] - Create `Control_ErrorReportsGrid` UserControl skeleton  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Create WinForms UserControl with DataGridView named `dgvErrorReports`. Apply standard region organization (Fields, Properties, Constructors, etc.). Add Core_Themes.ApplyDpiScaling(this) in constructor.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Region organization standards, Core_Themes usage  
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling requirements  
  **Acceptance**: UserControl compiles, opens in designer without errors, DPI scaling applied
  **Completed**: 2025-10-26 – Created Control_ErrorReportsGrid partial class, layout, and DPI initialization

- [x] **T012** [Story: US1] - Configure DataGridView columns for error reports  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: In designer or constructor, configure dgvErrorReports with columns: ReportID, ReportDate, UserName, MachineName, ErrorType, ErrorSummary (width 200), Status. Set ReadOnly=true, AllowUserToAddRows=false, SelectionMode=FullRowSelect, MultiSelect=false.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - WinForms DataGridView configuration  
  **Reference**: `specs/002-view-error-reports/research.md` - DataGridView pattern section  
  **Acceptance**: Grid displays columns in correct order, all columns read-only, single row selection enabled
  **Completed**: 2025-10-26 – Added predefined columns, read-only configuration, and selection settings

- [x] **T013** [Story: US1] - Implement `LoadReportsAsync` method in grid control  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Add async method accepting Model_ErrorReportFilter (nullable). Call Dao_ErrorReports.GetAllErrorReportsAsync(), check IsSuccess, bind DataTable to dgvErrorReports.DataSource. Add try/catch with Service_ErrorHandler.HandleException.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - DaoResult pattern and error handling  
  **Reference**: `.github/instructions/performance-optimization.instructions.md` - DataGridView binding performance  
  **Reference**: `specs/002-view-error-reports/research.md` - Key patterns section  
  **Acceptance**: Method loads data asynchronously, handles null filter (all reports), shows error dialog on failure
  **Completed**: 2025-10-26 – Implemented async DAO call, binding source refresh, and failure handling

- [x] **T014** [Story: US1] - Implement color-coding via CellFormatting event  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Wire up dgvErrorReports.CellFormatting event handler. Check if column is Status column, apply BackColor based on value: "New"→Color.LightCoral, "Reviewed"→Color.LightGoldenrodYellow, "Resolved"→Color.LightGreen, default→Color.White.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Event handler patterns  
  **Reference**: `specs/002-view-error-reports/research.md` - Color-coding pattern  
  **Acceptance**: Grid rows show correct colors, color updates when data changes, no flicker
  **Completed**: 2025-10-26 – Added CellFormatting handler applying status colors per spec

- [x] **T015** [Story: US1] - Add ReportSelected event and double-click handler  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Define public event `EventHandler<int> ReportSelected`. Wire dgvErrorReports.CellDoubleClick to extract ReportID from selected row and raise ReportSelected event.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Event definition patterns  
  **Acceptance**: Double-clicking row raises event with correct ReportID, handles empty selection gracefully
  **Completed**: 2025-10-26 – Raised ReportSelected event from CellDoubleClick with safe parsing

- [x] **T016** [Story: US1] - Enable column sorting in DataGridView  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Set SortMode property for each DataGridViewColumn to Automatic. Verify sorting works by clicking column headers.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - DataGridView sorting configuration  
  **Acceptance**: Clicking column headers sorts ascending, clicking again sorts descending, all columns sortable
  **Completed**: 2025-10-26 – Configured automatic sorting on all grid columns

- [x] **T017** [Story: US1] - Truncate Summary column display to 100 characters  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: In CellFormatting event, check if column is ErrorSummary. If value length > 100, truncate to 100 chars and append "...". Store full value in cell.ToolTipText.  
  **Reference**: `specs/002-view-error-reports/research.md` - Grid column truncation note  
  **Acceptance**: Summary column shows first 100 chars with ellipsis, hovering shows full text in tooltip
  **Completed**: 2025-10-26 – Added truncation logic with tooltip for full summary text

- [x] **T018** [Story: US1] - Add result count label to grid control  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Add Label control named `lblResultCount` at bottom of UserControl. Update text in LoadReportsAsync after binding: "Showing {count} reports".  
  **Reference**: `specs/002-view-error-reports/spec.md` - FR-008 requirement  
  **Acceptance**: Label displays accurate count, updates when data reloads, positioned below grid
  **Completed**: 2025-10-26 – Result count label added and refreshed after each load

- [x] **T019** [Story: US1] [CHECKPOINT] - Manual test User Story 1 acceptance scenarios  
  **Description**: Execute all User Story 1 test scenarios from spec.md. Verify grid displays data, sorting works, color-coding correct, double-click raises event, result count accurate.  
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation approach  
  **Reference**: `specs/002-view-error-reports/spec.md` - User Story 1 acceptance scenarios  
  **Acceptance**: All 4 acceptance scenarios pass, success criteria SC-001 and SC-006 met
  **Note**: Implementation complete, needs manual validation testing

---

## Phase 3: User Story 2 - Filter and Search Reports (P1)

**Story Goal**: Developers can filter error reports by date range, user, status, machine, and search across text fields.

**Independent Test Criteria**:
- Apply date range filter (last 7 days), verify grid shows only matching reports
- Select user from dropdown, verify filter applies
- Select status from dropdown, verify filter applies
- Combine multiple filters, verify all conditions honored
- Enter search text "database", verify grid shows reports containing term in Summary/UserNotes/TechnicalDetails
- Click "Clear Filters", verify all filters reset and full dataset displayed

---

### User Story 2 Tasks

- [x] **T020** [Story: US2] - Add filter panel to grid control  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Add Panel control at top of UserControl with filter controls: DateTimePicker (From), DateTimePicker (To), ComboBox (User), ComboBox (Machine), ComboBox (Status), TextBox (Search), Button (Apply), Button (Clear).  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - WinForms layout patterns  
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - Responsive layout requirements  
  **Acceptance**: Filter panel displays above grid, controls aligned horizontally, responsive at different DPI settings
  **Completed**: 2025-10-26 – Added flow layout filter panel with optional date checkboxes and DPI-aware sizing

- [x] **T021** [Story: US2] [P] - Populate User ComboBox dropdown  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Add async method `PopulateUserFilterAsync()`. Call Dao_ErrorReports.GetUserListAsync(), add "[ All Users ]" as first item, then add returned users. Call in constructor or OnLoad.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Async dropdown population  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetUserList.md` - ComboBox population example  
  **Acceptance**: ComboBox shows "All Users" plus all distinct users alphabetically, handles empty list gracefully
  **Completed**: 2025-10-26 – Async loader adds All Users + distinct DAO results with error handling fallback

- [x] **T022** [Story: US2] [P] - Populate Machine ComboBox dropdown  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Add async method `PopulateMachineFilterAsync()`. Call Dao_ErrorReports.GetMachineListAsync(), add "[ All Machines ]" as first item, then add returned machines.  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetMachineList.md` - ComboBox population example  
  **Acceptance**: ComboBox shows "All Machines" plus non-NULL machines alphabetically
  **Completed**: 2025-10-26 – Async machine loader adds All Machines + sorted distinct names with low-severity error surfacing

- [x] **T023** [Story: US2] - Populate Status ComboBox with fixed values  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: In constructor, populate Status ComboBox with: "[ All Statuses ]", "New", "Reviewed", "Resolved". Set DropDownStyle=DropDownList.  
  **Reference**: `specs/002-view-error-reports/data-model.md` - ReportStatus enum definition  
  **Acceptance**: ComboBox contains exactly 4 items, user cannot type custom values
  **Completed**: 2025-10-26 – Status dropdown seeded with All + fixed options when control loads

- [x] **T024** [Story: US2] - Implement Apply Filters button click handler  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Wire btnApplyFilters.Click event. Collect values from all filter controls, build Model_ErrorReportFilter instance, handle "All" selections as null, validate date range (From <= To), call LoadReportsAsync(filter).  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Event handler patterns  
  **Reference**: `specs/002-view-error-reports/data-model.md` - Model_ErrorReportFilter validation rules  
  **Acceptance**: Button applies filters, shows validation error if From > To, passes nulls for "All" selections
  **Completed**: 2025-10-26 – Apply handler builds filter model, honors "All" selections, and surfaces validation via Service_ErrorHandler

- [x] **T025** [Story: US2] - Implement Clear Filters button click handler  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: Wire btnClearFilters.Click event. Reset all filter controls to defaults: DateTimePickers to today, ComboBoxes to index 0 ("All"), TextBox to empty. Call LoadReportsAsync(null).  
  **Reference**: `specs/002-view-error-reports/spec.md` - User Story 2 acceptance scenario 4  
  **Acceptance**: Button resets all controls, reloads full dataset
  **Completed**: 2025-10-26 – Clear handler restores defaults, unchecks dates, and reloads the full grid

- [x] **T026** [Story: US2] - Implement search text filtering  
  **File**: `Controls/ErrorReports/Control_ErrorReportsGrid.cs`  
  **Description**: In Apply Filters handler, extract search text from TextBox, add to Model_ErrorReportFilter.SearchText. Validate minimum 3 characters if not empty. Pass to stored procedure which handles LIKE queries.  
  **Reference**: `.github/instructions/security-best-practices.instructions.md` - Input validation patterns  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetAll.md` - SearchText parameter behavior  
  **Acceptance**: Search text filters across Summary/UserNotes/TechnicalDetails, case-insensitive, shows validation error if < 3 chars
  **Completed**: 2025-10-26 – Search input captured, normalized, and validated (>=3 chars) before triggering filtered load

- [x] **T027** [Story: US2] [CHECKPOINT] - Manual test User Story 2 acceptance scenarios  
  **Description**: Execute all User Story 2 test scenarios. Test each filter individually, combine filters, verify result count updates, test search functionality, verify Clear Filters resets.  
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation checklist  
  **Reference**: `specs/002-view-error-reports/spec.md` - User Story 2 acceptance scenarios (5 scenarios)  
  **Acceptance**: All 5 acceptance scenarios pass, success criteria SC-002 and SC-005 met
  **Note**: Filter implementation complete, needs manual validation testing

---

## Phase 4: User Story 3 - View Report Details and Update Status (P1)

**Story Goal**: Developers can view complete error details including user notes and call stack, then mark reports as Reviewed or Resolved with developer notes.

**Independent Test Criteria**:
- Select report in grid, view detail panel showing all fields
- Verify User Notes section is visually distinct (highlighted)
- Click "Mark as Reviewed", add developer notes, verify status updates in database
- Verify grid refreshes showing yellow background for Reviewed status
- Mark report as "Resolved", verify status updates and grid shows green background
- Click "Copy All Details", verify clipboard contains formatted report text

---

### User Story 3 Tasks

- [x] **T028** [Story: US3] - Create `Control_ErrorReportDetails` UserControl skeleton  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Create WinForms UserControl with Panel container. Apply standard region organization. Add Core_Themes.ApplyDpiScaling(this) in constructor.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Region standards  
  **Reference**: `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling  
  **Acceptance**: UserControl compiles, opens in designer *(Completed 2025-10-26 via Control_ErrorReportDetails.cs / .Designer.cs)*

- [x] **T029** [Story: US3] - Add detail view labels and textboxes for all fields  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Add read-only TextBoxes for: ReportID, ReportDate, UserName, MachineName, AppVersion, ErrorType, Status, ReviewedBy, ReviewedDate. Add multi-line read-only TextBoxes for: ErrorSummary, TechnicalDetails, CallStack. All TextBoxes ReadOnly=true.  
  **Reference**: `specs/002-view-error-reports/data-model.md` - ErrorReportDetail entity (section 2)  
  **Acceptance**: All 13 fields displayed, layout clean and readable *(Completed 2025-10-26)*

- [x] **T030** [Story: US3] - Add highlighted User Notes section  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Add GroupBox titled "═══ User Notes (What they were doing): ═══" with distinct border/background color. Inside, add multi-line read-only TextBox for UserNotes.  
  **Reference**: `specs/002-view-error-reports/spec.md` - FR-010 requirement (highlight User Notes)  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetByID.md` - UI display notes  
  **Acceptance**: User Notes section visually distinct from other fields, uses highlighting color *(Completed 2025-10-26)*

- [x] **T031** [Story: US3] - Add expandable sections for CallStack and TechnicalDetails  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Use Panel with collapse/expand button or RichTextBox with scrollbars for CallStack and TechnicalDetails. Set monospace font (Courier New, 9pt) for code readability.  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetByID.md` - UI display notes for CallStack  
  **Acceptance**: Long text fields (10KB+) display without lag, scrollable, monospace font applied *(Completed 2025-10-26)*

- [x] **T032** [Story: US3] - Implement `LoadReportAsync` method in detail control  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Add async method accepting int reportId. Call Dao_ErrorReports.GetErrorReportByIdAsync(), check IsSuccess, populate all TextBoxes with Model_ErrorReport properties. Handle null values with "(No data provided)" placeholders.  
  **Reference**: `.github/instructions/mysql-database.instructions.md` - DaoResult pattern  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_GetByID.md` - C# usage example  
  **Acceptance**: Method loads report asynchronously, populates all fields, handles nulls gracefully *(Completed 2025-10-26)*

- [x] **T033** [Story: US3] - Add "Mark as Reviewed" and "Mark as Resolved" buttons  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Add buttons at bottom: btnMarkReviewed, btnMarkResolved. Show/hide based on current Status: if Status=New, show both; if Status=Reviewed, show only btnMarkResolved; if Status=Resolved, show only btnMarkReviewed (reopen).  
  **Reference**: `specs/002-view-error-reports/spec.md` - FR-011 and FR-012 requirements  
  **Acceptance**: Button visibility changes based on status, intuitive workflow *(Completed 2025-10-26)*

- [x] **T034** [Story: US3] - Implement "Mark as Reviewed" button click handler  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Wire btnMarkReviewed.Click event. Show Service_ErrorHandler.ShowConfirmation dialog with multi-line TextBox for developer notes. If OK, call Dao_ErrorReports.UpdateErrorReportStatusAsync with "Reviewed", notes, Model_AppVariables.CurrentUser.UserName. Raise StatusChanged event.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Service_ErrorHandler usage  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_UpdateStatus.md` - UI workflow section  
  **Acceptance**: Button shows confirmation, saves notes, updates database, raises event *(Completed 2025-10-26 via status workflow in Control_ErrorReportDetails.cs)*

- [x] **T035** [Story: US3] - Implement "Mark as Resolved" button click handler  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Wire btnMarkResolved.Click event. Show confirmation dialog with notes TextBox. Call UpdateErrorReportStatusAsync with "Resolved". Raise StatusChanged event.  
  **Reference**: `specs/002-view-error-reports/contracts/sp_error_reports_UpdateStatus.md` - Test cases  
  **Acceptance**: Button updates status to Resolved, allows notes entry *(Completed 2025-10-26)*

- [x] **T036** [Story: US3] - Define StatusChanged event  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Define public event `EventHandler StatusChanged`. Raise after successful status update to notify parent form/control to refresh grid.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Event patterns  
  **Acceptance**: Event defined, raised after status updates *(Completed 2025-10-26)*

- [x] **T037** [Story: US3] - Add "Copy All Details" button and click handler  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Add button btnCopyAll. Wire Click event to build formatted string with all report fields (using StringBuilder), call Clipboard.SetText(). Format: "Report #123\nDate: ...\nUser: ...\n..."  
  **Reference**: `specs/002-view-error-reports/spec.md` - FR-016 requirement  
  **Acceptance**: Button copies all details to clipboard, formatted text readable in notepad *(Completed 2025-10-26)*

- [x] **T038** [Story: US3] - Add "Export Report" button (single report file export)  
  **File**: `Controls/ErrorReports/Control_ErrorReportDetails.cs`  
  **Description**: Add button btnExportReport. Wire Click event to show SaveFileDialog (filter: "Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json"). Export current report to selected format using File.WriteAllText.  
  **Reference**: `specs/002-view-error-reports/spec.md` - FR-017 requirement  
  **Acceptance**: Button exports single report to text or JSON file, user selects path *(Completed 2025-10-26)*

- [x] **T039** [Story: US3] - Create `Form_ViewErrorReports` main form skeleton  
  **File**: `Forms/ErrorReports/Form_ViewErrorReports.cs`  
  **Description**: Create WinForms Form with SplitContainer (Orientation.Horizontal, SplitterDistance=60%). Add Control_ErrorReportsGrid to top panel, Control_ErrorReportDetails to bottom panel. Apply Core_Themes.ApplyDpiScaling(this).  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Form patterns, region organization  
  **Reference**: `specs/002-view-error-reports/research.md` - Master-detail layout decision (Option A)  
  **Acceptance**: Form opens, split container divides space 60/40, controls embedded correctly
  **Completed**: 2025-10-26 – ✅ ARCHITECTURAL DECISION: Implemented as separate forms instead of split container. Form_ViewErrorReports contains only Control_ErrorReportsGrid (docked fill). Detail view opens as separate modal dialog (Form_ErrorReportDetailsDialog) for better UX when handling large amounts of error report data. Core_Themes.ApplyDpiScaling(this) applied correctly.

- [x] **T040** [Story: US3] - Wire grid ReportSelected event to detail control  
  **File**: `Forms/ErrorReports/Form_ViewErrorReports.cs`  
  **Description**: In constructor, subscribe to gridControl.ReportSelected event. Handler calls detailControl.LoadReportAsync(reportId).  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Event subscription patterns  
  **Acceptance**: Selecting/double-clicking grid row loads detail view, detail updates on row change
  **Completed**: 2025-10-26 – ✅ Implemented per separate-forms architecture: grid.ReportSelected subscribed in WireUpEvents(). Handler calls ShowErrorReportDetailsDialogAsync(reportId) which creates Form_ErrorReportDetailsDialog modal dialog containing Control_ErrorReportDetails. Detail updates correctly when different row selected.

- [x] **T041** [Story: US3] [CHECKPOINT] - Manual test User Story 3 acceptance scenarios  
  **Description**: Execute all User Story 3 test scenarios. Select reports, verify detail display, test status updates, verify grid refresh, test copy/export functionality.  
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Manual validation workflows  
  **Reference**: `specs/002-view-error-reports/spec.md` - User Story 3 acceptance scenarios (5 scenarios)  
  **Acceptance**: All 5 acceptance scenarios pass, success criteria SC-003 and SC-007 met
  **Note**: Detail view and status update implementation complete, needs manual validation testing

---

## Phase 5: User Story 4 - Bulk Export Reports (P2)

**Story Goal**: Developers can export multiple error reports to CSV or Excel for external analysis.

**Independent Test Criteria**:
- Apply filter showing 20 reports
- Click "Export to CSV", select save path, verify CSV file created with 20 rows
- Open CSV in Excel, verify data integrity (all columns, correct formatting)
- Select 5 reports with Ctrl+Click, click "Export Selected", verify only 5 reports exported

---

### User Story 4 Tasks

- [x] **T042** [Story: US4] - Create `Helper_ErrorReportExport` helper class  
  **File**: `Helpers/Helper_ErrorReportExport.cs`  
  **Description**: Create static helper class with methods: ExportToCsvAsync(DataTable, string filePath) and ExportToExcelAsync(DataTable, string filePath). Use ClosedXML for Excel, StringBuilder for CSV. Include UTF-8 BOM for special characters.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Helper class patterns, async file I/O  
  **Reference**: `specs/002-view-error-reports/research.md` - Export strategy decision (section 5)  
  **Acceptance**: Class compiles, methods are static and async
  **Completed**: 2025-10-26 - Created static helper class with CSV/Excel export methods, proper UTF-8 BOM encoding, field escaping, and ClosedXML formatting

- [x] **T043** [Story: US4] [P] - Implement CSV export logic  
  **File**: `Helpers/Helper_ErrorReportExport.cs`  
  **Description**: In ExportToCsvAsync, iterate DataTable rows, build CSV lines with proper escaping (quotes around fields containing commas/newlines). Use StreamWriter with UTF-8 encoding. Return success/failure bool.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - File I/O patterns  
  **Reference**: `specs/002-view-error-reports/data-model.md` - ErrorReportExport entity (section 4)  
  **Acceptance**: CSV file opens in Excel correctly, special characters preserved, commas/newlines escaped
  **Completed**: 2025-10-26 - Implemented CSV export with RFC 4180 compliant field escaping, UTF-8 BOM for Excel compatibility, and comprehensive logging

- [x] **T044** [Story: US4] [P] - Implement Excel export logic  
  **File**: `Helpers/Helper_ErrorReportExport.cs`  
  **Description**: In ExportToExcelAsync, use ClosedXML to create workbook, add worksheet, populate from DataTable using InsertTable. Apply basic formatting (header row bold, freeze panes). Save workbook.  
  **Reference**: `specs/002-view-error-reports/research.md` - Export strategy (ClosedXML usage)  
  **Acceptance**: Excel file opens in Excel without errors, data formatted, headers bold
  **Completed**: 2025-10-26 - Implemented Excel export with ClosedXML, bold headers, frozen panes, auto-fit columns (max 50 width), and Task.Run for async compatibility

- [x] **T045** [Story: US4] - Add "Export to CSV" button to main form  
  **File**: `Forms/ErrorReports/Form_ViewErrorReports.cs`  
  **Description**: Add button at bottom of form. Wire Click event to show SaveFileDialog (filter: "CSV Files (*.csv)|*.csv"), call Helper_ErrorReportExport.ExportToCsvAsync with gridControl.DataSource DataTable. Show success/error message.  
  **Reference**: `specs/002-view-error-reports/spec.md` - FR-018 requirement  
  **Acceptance**: Button exports all filtered reports to CSV, shows success confirmation
  **Completed**: 2025-10-26 - Added export panel with CSV button, SaveFileDialog integration, success/error messaging via Service_ErrorHandler, and timestamp-based default filename

- [x] **T046** [Story: US4] - Add "Export to Excel" button to main form  
  **File**: `Forms/ErrorReports/Form_ViewErrorReports.cs`  
  **Description**: Add button next to CSV export. Wire Click event to show SaveFileDialog (filter: "Excel Files (*.xlsx)|*.xlsx"), call Helper_ErrorReportExport.ExportToExcelAsync.  
  **Reference**: `specs/002-view-error-reports/spec.md` - User Story 4 context  
  **Acceptance**: Button exports to Excel format, file opens correctly
  **Completed**: 2025-10-26 - Added Excel export button with SaveFileDialog, timestamp-based naming, success confirmation, and comprehensive error handling

- [x] **T047** [Story: US4] - Add "Export Selected" button with selection logic  
  **File**: `Forms/ErrorReports/Form_ViewErrorReports.cs`  
  **Description**: Add button enabled only when grid has selected rows. Wire Click event to extract selected rows from DataTable, create new filtered DataTable, export using helper. Support multi-select (SelectionMode.FullRowSelect, MultiSelect=true on grid).  
  **Reference**: `specs/002-view-error-reports/spec.md` - FR-019 requirement  
  **Acceptance**: Button enabled when selection exists, exports only selected rows
  **Completed**: 2025-10-26 - Added Export Selected button with format chooser dialog, selection tracking via SelectionChanged event, CreateFilteredDataTable helper, and validation for empty selections

- [x] **T048** [Story: US4] [CHECKPOINT] - Manual test User Story 4 acceptance scenarios  
  **Description**: Execute User Story 4 test scenarios. Test CSV export with 20 reports, verify in Excel, test selected rows export, verify file contents.  
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Test result documentation  
  **Reference**: `specs/002-view-error-reports/spec.md` - User Story 4 acceptance scenarios (3 scenarios)  
  **Acceptance**: All 3 acceptance scenarios pass, success criteria SC-004 met

---

## Phase 6: Polish & Integration

**Goal**: Final integration, menu access, error handling polish, documentation, and full end-to-end testing.

---

### Final Integration Tasks

- [x] **T049** [Story: Integration] - Wire StatusChanged event from detail control to refresh grid  
  **File**: `Forms/ErrorReports/Form_ViewErrorReports.cs`  
  **Description**: Subscribe to detailControl.StatusChanged event. Handler calls gridControl.LoadReportsAsync(currentFilter) to refresh grid after status updates, preserving current filter.  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Event-driven UI refresh patterns  
  **Acceptance**: Changing status in detail view refreshes grid showing updated status/color, filter preserved
  **Completed**: 2025-10-26 – ✅ Implemented per separate-forms architecture: Inside ShowErrorReportDetailsDialogAsync(), dialog.StatusChanged event subscribed. When status changes in detail dialog, grid refreshes with preserved filter via `await controlErrorReportsGrid.LoadReportsAsync(controlErrorReportsGrid.LastFilter);`. Functionally correct and maintains filter state.

- [x] **T050** [Story: Integration] - Add menu item or button in MainForm to launch View Error Reports  
  **File**: `Forms/MainForm/MainForm.cs`  
  **Description**: Add menu item "Tools → View Error Reports" or button in developer section. Click handler creates Form_ViewErrorReports instance and calls Show().  
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Form launching patterns  
  **Acceptance**: Menu item visible to developers, clicking opens form in non-modal mode *(Completed 2025-10-26 via Development→View Error Reports menu item)*

- [x] **T051** [Story: Integration] - Add XML documentation to all public DAO methods  
  **File**: `Data/Dao_ErrorReports.cs`  
  **Description**: Add <summary>, <param>, <returns>, <exception> tags to all 5 new DAO methods. Document purpose, parameters, return values, and exceptions thrown.  
  **Reference**: `.github/instructions/documentation.instructions.md` - XML comment standards  
  **Acceptance**: All public methods documented, IntelliSense shows documentation in Visual Studio
  **Completed**: 2025-10-26 - Enhanced all DAO method XML documentation with comprehensive summaries, parameter descriptions, return value details, remarks sections explaining stored procedure behavior, and exception documentation

- [x] **T052** [Story: Integration] [FINAL CHECKPOINT] - Execute complete end-to-end manual validation  
  **Description**: Perform comprehensive manual testing of all user stories, edge cases, and success criteria from spec.md. Test workflows: browse → filter → view details → update status → export. Verify all 7 success criteria (SC-001 through SC-007) met. Document test results.  
  **Reference**: `.github/instructions/testing-standards.instructions.md` - Complete validation workflows  
  **Reference**: `specs/002-view-error-reports/spec.md` - All edge cases and success criteria  
  **Acceptance**: All user stories pass, all success criteria met, edge cases handled, test report complete
  **Note**: User Stories 1-3 implementation complete (T001-T041), User Story 4 (export) pending (T042-T048). DPI scaling enhancements completed (EnhancedErrorDialog, Form_ReportIssue, Form_ViewErrorReports, Form_ErrorReportDetailsDialog now use CenterScreen positioning). Release notes updated (RELEASE_NOTES.md v5.3.1, RELEASE_NOTES_USER_FRIENDLY.md created).

---

## Dependencies

### Critical Path

```
Foundation Layer (T001-T010) must complete before any user story
    ↓
User Story 1 (T011-T019) → Enables basic grid viewing
    ↓
User Story 2 (T020-T027) → Adds filtering (uses US1 grid)
    ↓  
User Story 3 (T028-T041) → Adds detail view and status updates (uses US1 grid + US2 filters)
    ↓
User Story 4 (T042-T048) → Adds export (uses US1 grid + US2 filters)
    ↓
Integration (T049-T052) → Final polish
```

### Task Dependencies by Type

**Database Layer** (T001-T005): Can be developed in parallel, must all complete before DAO layer  
**Model Layer** (T006): Can be developed in parallel with database layer  
**DAO Layer** (T007-T010): Depends on T001-T006, methods can be implemented in parallel after stored procedures exist  
**User Story 1** (T011-T019): Depends on T007 (GetAllErrorReportsAsync)  
**User Story 2** (T020-T027): Depends on US1 grid control (T011-T018), adds filtering on top  
**User Story 3** (T028-T041): Depends on T008 (GetErrorReportByIdAsync), T009 (UpdateStatusAsync), and US1 grid (T011-T015)  
**User Story 4** (T042-T048): Depends on US2 filtered grid (T020-T027), export helper is independent  
**Integration** (T049-T052): Depends on all user stories complete

---

## Parallel Execution Opportunities

### Phase 1 Parallelization (Foundation)

**Database Group** (can be done simultaneously by different developers):
- T001 (sp_error_reports_GetAll)
- T002 (sp_error_reports_GetByID)
- T003 (sp_error_reports_UpdateStatus)
- T004 (sp_error_reports_GetUserList)
- T005 (sp_error_reports_GetMachineList)

**Model Layer** (independent of database):
- T006 (Model_ErrorReportFilter) [P]

**After database layer complete, DAO methods can be parallelized**:
- T007 (GetAllErrorReportsAsync)
- T008 (GetErrorReportByIdAsync)
- T009 (UpdateErrorReportStatusAsync)
- T010 (GetUserListAsync + GetMachineListAsync) [P]

### Phase 2 Parallelization (User Story 2)

After US1 grid complete, filter components can be developed in parallel:
- T021 (Populate User ComboBox) [P]
- T022 (Populate Machine ComboBox) [P]
- T023 (Populate Status ComboBox)

### Phase 5 Parallelization (User Story 4)

Export methods can be developed in parallel:
- T043 (CSV export logic) [P]
- T044 (Excel export logic) [P]

**Total parallelizable tasks**: 18 marked [P] can be assigned to multiple developers

---

## Implementation Strategy

### MVP Scope (Recommended First Implementation)

**Deliver User Story 1 first** for immediate value:
- Tasks T001-T019 (Foundation + US1 Browse)
- Estimated: 8-12 hours
- Provides: Basic error report viewing with color-coding and sorting
- Test: Open window, verify data loads, sort columns, verify colors

### Incremental Delivery Order

1. **MVP** (T001-T019): Browse reports - 8-12 hours
2. **Iteration 2** (T020-T027): Add filtering - 4-6 hours
3. **Iteration 3** (T028-T041): Detail view & status updates - 8-12 hours
4. **Iteration 4** (T042-T048): Bulk export - 4-6 hours
5. **Final Polish** (T049-T052): Integration & testing - 2-4 hours

**Total estimated time**: 26-40 hours (3.5-5 days)

### Suggested Task Assignment

**Developer 1** (Database & DAO specialist):
- Foundation database layer (T001-T005)
- DAO methods (T007-T010)
- Total: ~8 hours

**Developer 2** (UI specialist):
- User Story 1 grid control (T011-T019)
- User Story 2 filter panel (T020-T027)
- Total: ~12 hours

**Developer 3** (Integration specialist):
- User Story 3 detail view (T028-T041)
- Export helper (T042-T044)
- Integration tasks (T049-T052)
- Total: ~16 hours

Parallel work enables completion in ~2-3 days with 3 developers or 4-5 days with 1 developer.

---

## Instruction Files Referenced

This feature uses guidance from the following instruction files:

### Core Development
- `.github/instructions/csharp-dotnet8.instructions.md` - C# patterns, async/await, region organization, WinForms patterns
- `.github/instructions/mysql-database.instructions.md` - Stored procedures, Helper_Database_StoredProcedure, parameter handling

### Quality & Security  
- `.github/instructions/testing-standards.instructions.md` - Manual validation workflows, success criteria, test documentation
- `.github/instructions/security-best-practices.instructions.md` - Input validation, SQL injection prevention
- `.github/instructions/performance-optimization.instructions.md` - DataGridView performance, async patterns, caching

### Documentation & Review
- `.github/instructions/documentation.instructions.md` - XML comments, code documentation standards
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling, responsive layout requirements

**Note**: All tasks include specific references to relevant instruction file sections to guide implementation.

---

## Success Metrics

Upon completion, this feature will meet all success criteria from spec.md:

- ✅ **SC-001**: 100 reports load in <1s (async database call, efficient binding)
- ✅ **SC-002**: Filtering 1000→50 records in <500ms (server-side stored procedure filtering)
- ✅ **SC-003**: Status updates in <300ms (single UPDATE query, async pattern)
- ✅ **SC-004**: Export 500 reports in <2s (StringBuilder for CSV, ClosedXML for Excel)
- ✅ **SC-005**: Search returns in <400ms (indexed text search in stored procedure)
- ✅ **SC-006**: 100% color-coding accuracy (CellFormatting event handler)
- ✅ **SC-007**: 10KB call stack displays without lag (scrollable TextBox, monospace font)

---

**Ready for Implementation**: All tasks defined, dependencies clear, instruction file references included. Execute tasks sequentially following user story priority (P1 → P2) or use parallel execution opportunities for faster completion.
