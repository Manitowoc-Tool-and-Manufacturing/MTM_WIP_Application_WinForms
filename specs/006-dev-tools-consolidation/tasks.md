# Tasks: Developer Tools Consolidation & Core Services Refactoring

**Feature**: 006-dev-tools-consolidation  
**Input**: plan.md, spec.md, research.md, data-model.md, contracts/  
**Generated**: 2025-12-13

---

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (US1-US7)
- Exact file paths included in descriptions

## Path Conventions

- **Single project**: Root at `c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\`
- **Services**: `Services/`
- **Data**: `Data/`
- **Forms**: `Forms/`
- **Models**: `Models/`
- **Database**: `Database/UpdatedStoredProcedures/`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and model creation for all stories

- [x] T001 Create Models/DeveloperTools/ folder structure
- [x] T002 [P] Create Model_LogEntry.cs in Models/DeveloperTools/Model_LogEntry.cs with Enum_LogSource
- [x] T003 [P] Create Model_LogStatistics.cs in Models/DeveloperTools/Model_LogStatistics.cs
- [x] T004 [P] Create Model_LogFilter.cs in Models/DeveloperTools/Model_LogFilter.cs with factory methods and Enum_LogGroupBy
- [x] T005 [P] Create Model_ErrorGrouping.cs in Models/DeveloperTools/Model_ErrorGrouping.cs
- [x] T006 [P] Create Model_SystemHealthStatus.cs in Models/DeveloperTools/Model_SystemHealthStatus.cs with Enum_HealthIndicator
- [x] T007 [P] Create Model_DatabaseHealth.cs in Models/DeveloperTools/Model_DatabaseHealth.cs
- [x] T008 [P] Create Model_PerformanceMetrics.cs in Models/DeveloperTools/Model_PerformanceMetrics.cs with Capture() factory
- [x] T009 [P] Create Model_FeedbackSummary.cs in Models/DeveloperTools/Model_FeedbackSummary.cs

**Checkpoint**: All model classes created and ready for service implementation

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story UI work can begin

**⚠️ CRITICAL**: User Story 1 (DI refactoring) blocks ALL other story work

### 2.1 Database Infrastructure

- [x] T010 [P] Create md_devtools_GetLogStatistics.sql stored procedure in Database/UpdatedStoredProcedures/md_devtools_GetLogStatistics.sql - aggregate error counts by severity for date range from log_error table
- [x] T011 [P] Create md_devtools_GetErrorGroupings.sql stored procedure in Database/UpdatedStoredProcedures/md_devtools_GetErrorGroupings.sql - group errors by ErrorType/MethodName with counts
- [x] T012 [P] Create md_devtools_GetLogTimeline.sql stored procedure in Database/UpdatedStoredProcedures/md_devtools_GetLogTimeline.sql - hourly/daily error counts for charting
- [x] T013 Execute stored procedures against development database to verify MySQL 5.7.24 compatibility

### 2.2 DAO Layer

- [x] T014 [P] Create IDao_DeveloperTools.cs interface in Data/IDao_DeveloperTools.cs with GetLogStatisticsAsync, GetErrorsAsync, GetErrorGroupingsAsync methods
- [x] T015 Create Dao_DeveloperTools.cs implementation in Data/Dao_DeveloperTools.cs using Helper_Database_StoredProcedure pattern

**Checkpoint**: Database layer ready - service layer can now be built

---

## Phase 3: User Story 1 - Core Service Dependency Injection (Priority: P1) 🎯 MVP

**Goal**: Convert LoggingUtility and Service_ErrorHandler from static to DI-enabled services while maintaining backward compatibility

**Independent Test**: Verify application starts, logs work, error handling works, and services can be constructor-injected into test classes

### 3.1 ILoggingService Creation

- [x] T016 [US1] Create ILoggingService.cs interface in Services/Logging/ILoggingService.cs with Log, LogApplicationError, LogDatabaseError, LogApplicationInfo, InitializeAsync, CleanUpOldLogsAsync methods
- [x] T017 [US1] Create Service_Logging.cs implementation in Services/Logging/Service_Logging.cs implementing ILoggingService with static Instance property for backward compatibility
- [x] T018 [US1] Modify existing Service_LoggingUtility.cs in Services/Logging/Service_LoggingUtility.cs to become static wrapper class delegating to Service_Logging.Instance

### 3.2 IService_ErrorHandler Creation

- [x] T019 [US1] Create IService_ErrorHandler.cs interface in Services/ErrorHandling/IService_ErrorHandler.cs with HandleException, HandleDatabaseError, HandleFileError, HandleNetworkError, HandleValidationError, HandleUnauthorizedAccess, ShowUserError, ShowError, ShowWarning, ShowInformation, ShowConfirmation methods
- [x] T020 [US1] Modify Service_ErrorHandler.cs in Services/ErrorHandling/Service_ErrorHandler.cs to implement IService_ErrorHandler, inject ILoggingService, add static Instance property

### 3.3 DI Container Registration

- [x] T021 [US1] Modify Service_OnStartup_DependencyInjection.cs in Services/Startup/Service_OnStartup_DependencyInjection.cs to register ILoggingService as singleton, IService_ErrorHandler as singleton, and set Instance properties after build

### 3.4 Service_DeveloperTools Creation

- [x] T022 [US1] Create IService_DeveloperTools.cs interface in Services/DeveloperTools/IService_DeveloperTools.cs per contracts/IService_DeveloperTools.md
- [x] T023 [US1] Create Service_DeveloperTools.cs implementation in Services/DeveloperTools/Service_DeveloperTools.cs with CSV parsing, database queries, statistics aggregation
- [x] T024 [US1] Register IService_DeveloperTools in Service_OnStartup_DependencyInjection.cs as transient
- [x] T024a [US3] Implement SyncLogsToDatabaseAsync in Service_DeveloperTools.cs with batch insertion and progress reporting
- [x] T024b [US3] Implement PurgeLogsAsync in Service_DeveloperTools.cs to delete CSV log files

### 3.5 Backward Compatibility Verification

- [x] T025 [US1] Build project and verify zero compilation errors
- [ ] T026 [US1] Run application and verify logging works via LoggingUtility.Log() static calls
- [ ] T027 [US1] Run application and verify error handling works via Service_ErrorHandler static calls

### 3.6 Constitution Compliance for US1

- [x] T028 [US1] **Async/Await**: Verify no blocking calls in new service implementations
- [x] T029 [US1] **XML Documentation**: Verify all public members in ILoggingService, IService_ErrorHandler, IService_DeveloperTools have XML docs
- [x] T030 [US1] **Region Organization**: Verify all new files use standard #region structure (Fields, Properties, Constructors, Methods, Events, Helpers, Cleanup)

**Checkpoint**: Core services are DI-enabled with backward compatibility. Application runs identically to before refactoring.

---

## Phase 4: User Story 2 - Developer Dashboard View (Priority: P2)

**Goal**: Create Dashboard tab showing real-time statistics, error trends, and feedback summary

**Independent Test**: Open Developer Tools, verify Dashboard shows accurate 24h error/warning/feedback counts

### 4.1 Dashboard Tab Implementation

- [x] T031 [US2] Create tabDashboard TabPage in Form_DeveloperTools.Designer.cs with summary cards layout (Errors, Warnings, Feedback panels at top)
- [x] T032 [US2] Implement CreateSummaryCard() helper method in Form_DeveloperTools.cs returning Panel with title Label and value Label
- [x] T033 [US2] Add dgvRecentErrors DataGridView to Dashboard tab for 10 most recent errors
- [x] T034 [US2] Implement LoadDashboardDataAsync() in Form_DeveloperTools.cs calling _devToolsService.GetLogStatisticsAsync() for 24h period
- [x] T035 [US2] Implement UpdateDashboardCards() method to update summary card values with InvokeRequired check
- [x] T036 [US2] Add Refresh button to Dashboard tab wired to LoadDashboardDataAsync()
- [x] T037 [US2] Implement navigation from dgvRecentErrors row click to Logs tab filtered to that error

### 4.2 Timeline Chart (Optional Enhancement)

- [x] T038 [P] [US2] Add WebView2 control to Dashboard for error trend chart OR implement text-based trend summary
- [x] T039 [US2] Implement GetLogTimelineAsync() call for 7-day data and inject into chart/summary

### 4.3 Constitution Compliance for US2

- [x] T040 [US2] **Error Handling**: Verify LoadDashboardDataAsync uses Service_ErrorHandler for all exceptions
- [x] T041 [US2] **Theme Integration**: Verify Dashboard tab controls integrate with ThemedForm colors

**Checkpoint**: Dashboard tab shows accurate statistics, recent errors, and 7-day trend

---

## Phase 5: User Story 3 - Advanced Log Viewer (Priority: P2)

**Goal**: Create Logs tab with regex search, filtering, grouping, and export capabilities

**Independent Test**: Generate test log entries, verify search/filter/group/export all work correctly

### 5.1 Logs Tab Layout

- [x] T042 [US3] Create tabLogs TabPage in Form_DeveloperTools.Designer.cs with search panel (TextBox, CheckBox for regex, date pickers, severity checkboxes)
- [x] T043 [US3] Add dgvLogs DataGridView with columns: Timestamp, Level, Source, Message (virtualizing for performance)
- [x] T044 [US3] Add SplitContainer with log list (top) and details panel (bottom)
- [x] T045 [US3] Add cmbGroupBy ComboBox with options: None, ErrorType, Source, Hour, Day

### 5.2 Search and Filtering

- [x] T046 [US3] Implement SearchLogsAsync() method in Form_DeveloperTools.cs building Model_LogFilter from UI controls
- [x] T047 [US3] Wire txtSearch TextBox to SearchLogsAsync with debounce (300ms)
- [x] T048 [US3] Implement date range presets (Today, Last 7 Days, Last 30 Days, Custom) in cmbDateRange
- [x] T049 [US3] Implement severity filter checkboxes (Info, Warning, Error, Critical)
- [x] T050 [US3] Implement regex search support when chkRegex is checked

### 5.3 Grouping

- [x] T051 [US3] Implement GroupLogsAsync() method calling _devToolsService.GetErrorGroupingsAsync()
- [x] T052 [US3] Display grouped results in TreeView or expandable DataGridView sections

### 5.4 Details Panel

- [x] T053 [US3] Implement dgvLogs_SelectionChanged to populate details panel with full message, stack trace, context JSON
- [x] T054 [US3] Add Copy button to copy selected log entry to clipboard
- [x] T055 [US3] Format context data as indented JSON in details TextBox

### 5.5 Export

- [x] T056 [US3] Add Export button with dropdown: CSV, JSON, TXT
- [x] T057 [US3] Implement ExportLogsAsync() calling _devToolsService.ExportLogsAsync() with SaveFileDialog

### 5.6 Constitution Compliance for US3

- [x] T058 [US3] **Error Handling**: Verify all async methods use try/catch with Service_ErrorHandler
- [x] T059 [US3] **Model_Dao_Result**: Verify all service calls check IsSuccess before accessing Data

**Checkpoint**: Logs tab provides full search, filter, group, and export functionality

---

## Phase 6: User Story 4 - Enhanced Feedback Management (Priority: P3)

**Goal**: Create Feedback tab with filtering, bulk operations, and priority indicators

**Independent Test**: Create test feedback entries, verify filter/bulk operations/priority display work

### 6.1 Feedback Tab Layout

- [x] T060 [US4] Create tabFeedback TabPage in Form_DeveloperTools.Designer.cs
- [x] T061 [US4] Add stats bar Panel showing Total, Open, In Progress, Resolved counts
- [x] T062 [US4] Migrate existing feedback DataGridView from current Form_DeveloperTools to tabFeedback
- [x] T063 [US4] Add filter controls: Status dropdown, Type dropdown, Date range pickers

### 6.2 Filtering and Display

- [x] T064 [US4] Implement LoadFeedbackAsync() calling existing _feedbackManager methods
- [x] T065 [US4] Implement FilterFeedback() method applying UI filter selections
- [x] T066 [US4] Implement priority color-coding in DataGridView RowPrePaint (Red=High, Yellow=Medium, White=Low)

### 6.3 Bulk Operations

- [x] T067 [US4] Add checkbox column to feedback DataGridView for multi-select
- [x] T068 [US4] Add "Mark Selected as Reviewed" button
- [x] T069 [US4] Implement BulkUpdateStatusAsync() method for bulk status changes

### 6.4 Constitution Compliance for US4

- [x] T070 [US4] **Error Handling**: Verify bulk operations use Service_ErrorHandler for failures
- [x] T071 [US4] **Async/Await**: Verify no blocking calls in feedback operations

**Checkpoint**: Feedback tab provides enhanced management capabilities

---

## Phase 7: User Story 5 - System Information View (Priority: P3)

**Goal**: Create System Info tab with database health, app info, and performance metrics

**Independent Test**: Verify each panel displays accurate information about database, app, and performance

### 7.1 System Info Tab Layout

- [x] T072 [US5] Create tabSystemInfo TabPage in Form_DeveloperTools.Designer.cs with 3-panel layout
- [x] T073 [US5] Create Database Health panel with connection status indicator, version label, uptime label
- [x] T074 [US5] Create Application Info panel with version, start time, current user, role labels
- [x] T075 [US5] Create Performance Metrics panel with memory usage, thread count, GC stats labels

### 7.2 Data Population

- [x] T076 [US5] Implement LoadSystemInfoAsync() calling _devToolsService.GetDatabaseHealthAsync() and GetPerformanceMetrics()
- [x] T077 [US5] Implement Refresh timer (5s) to update system info (Performance only)
- [x] T078 [US5] Implement Test Connection button logic
- [x] T079 [US5] Create md_devtools_GetDatabaseStats.sql stored procedure for detailed metrics
- [x] T080 [US5] Update IDao_DeveloperTools and Service_DeveloperTools to fetch and display extended database stats (Connections, Uptime, Queries)
- [x] T080a [US5] Implement manual "Update Stats" button for database metrics (disable auto-refresh for DB)

### 7.3 Constitution Compliance for US5

- [x] T081 [US5] **Error Handling**: Verify database health check handles offline gracefully
- [x] T082 [US5] **Theme Integration**: Verify all panels use themed colors

**Checkpoint**: System Info tab displays accurate diagnostic information

---

## Phase 8: User Story 6 - User-Facing System Health Monitor (Priority: P3)

**Goal**: Create Form_SystemHealth for non-developers to view health status and their feedback

**Independent Test**: Non-developer user opens System Health form, sees health indicator and their feedback only

### 8.1 Form Creation

- [x] T083 [US6] Create Forms/SystemHealth/ folder
- [x] T084 [US6] Create Form_SystemHealth.cs inheriting from ThemedForm in Forms/SystemHealth/Form_SystemHealth.cs
- [x] T085 [US6] Create Form_SystemHealth.Designer.cs with health indicator panel, feedback grid, action buttons

### 8.2 Health Indicator

- [x] T086 [US6] Implement health indicator Panel with color (Green/Yellow/Red) based on _devToolsService.GetSystemHealthAsync()
- [x] T087 [US6] Display health message: "System Operating Normally" / warning / "System Experiencing Issues"

### 8.3 User Feedback Display

- [x] T088 [US6] Add read-only DataGridView showing current user's feedback only (Date, Type, Status, Title columns)
- [x] T089 [US6] Implement LoadUserFeedbackAsync() calling _devToolsService.GetUserFeedbackAsync(currentUserId)
- [x] T090 [US6] Display "No feedback submitted yet" message if user has no feedback

### 8.4 Action Buttons

- [x] T091 [US6] Add "Submit New Feedback" button wired to open existing feedback form
- [x] T092 [US6] Add "Contact IT Support" button wired to open default email client with pre-filled support address

### 8.5 MainForm Integration

- [x] T093 [US6] Add "System Health" menu item under View menu in MainForm.cs
- [x] T094 [US6] Add Ctrl+Shift+H keyboard shortcut handler in MainForm.cs
- [x] T095 [US6] Implement tsmiSystemHealth_Click to open Form_SystemHealth with DI-resolved services

### 8.6 Constitution Compliance for US6

- [x] T096 [US6] **Theme Integration**: Verify Form_SystemHealth inherits ThemedForm
- [x] T097 [US6] **Error Handling**: Verify all operations use Service_ErrorHandler

**Checkpoint**: Non-developer users can access System Health via View menu

---

## Phase 9: Form_DeveloperTools Redesign (Integration)

**Goal**: Redesign Form_DeveloperTools as multi-tab container integrating all tabs

**Independent Test**: Open Developer Tools, verify all 4 tabs work with keyboard shortcuts

### 9.1 Form Restructure

- [x] T098 Replace current Form_DeveloperTools content with TabControl containing tabDashboard, tabLogs, tabFeedback, tabSystemInfo
- [x] T099 Update Form_DeveloperTools constructor to inject IService_DeveloperTools, IService_ErrorHandler, IService_FeedbackManager
- [x] T100 Implement Form_Load to check role access (Admin/Developer only) and load initial tab

### 9.2 Keyboard Shortcuts

- [ ] T101 Implement ProcessCmdKey override in Form_DeveloperTools.cs for Ctrl+F (focus search), Ctrl+R/F5 (refresh), Escape (clear filters)
- [ ] T102 Add keyboard navigation between tabs (Ctrl+1, Ctrl+2, Ctrl+3, Ctrl+4)

### 9.3 Auto-Refresh

- [ ] T103 Add auto-refresh Timer (30 second interval) for Logs tab
- [ ] T104 Implement RefreshCurrentTabAsync() method to refresh active tab data

### 9.4 Final Integration

- [x] T105 Update MainForm's Developer Tools menu click to instantiate Form_DeveloperTools with DI services
- [ ] T106 Test all tabs work correctly with real data

**Checkpoint**: Multi-tab Developer Tools form fully functional

---

## Phase 10: User Story 7 - Legacy Cleanup (Priority: P4)

**Goal**: Remove obsolete forms, DAOs, and controls

**Independent Test**: Build succeeds, no references to deleted components

### 10.1 Pre-Cleanup Verification

- [ ] T107 [US7] Verify all Form_ViewLogsForm functionality is replaced by Logs tab
- [ ] T108 [US7] Verify all Form_ViewErrorReports functionality is replaced or no longer needed
- [ ] T109 [US7] Search codebase for references to files being deleted

### 10.2 Forms Cleanup

- [ ] T110 [US7] Delete Forms/ViewLogs/Form_ViewLogsForm.cs, Form_ViewLogsForm.Designer.cs, Form_ViewLogsForm.resx
- [ ] T111 [US7] Delete Forms/ErrorReports/Form_ViewErrorReports.cs and related files (6 files total)
- [ ] T112 [US7] Delete Forms/ErrorDialog/Form_ReportIssue.cs, Form_ReportIssue.Designer.cs, Form_ReportIssue.resx
- [ ] T113 [US7] Delete Forms/ErrorDialog/Form_ErrorReportDialog.cs and related files (if exists)

### 10.3 Controls Cleanup

- [ ] T114 [US7] Delete Controls/ErrorReports/Control_ErrorReportDetails.cs and related files
- [ ] T115 [US7] Delete Controls/ErrorReports/Control_ErrorReportsGrid.cs and related files

### 10.4 DAO Cleanup

- [ ] T116 [US7] Delete Data/Dao_ErrorReports.cs after verifying no remaining references
- [ ] T117 [US7] Delete Data/Dao_ErrorLog.cs after verifying logic migrated to Service_DeveloperTools

### 10.5 Menu Cleanup

- [ ] T118 [US7] Remove any MainForm menu items that opened deleted forms
- [ ] T119 [US7] Update any shortcuts that pointed to deleted forms

### 10.6 Final Verification

- [ ] T120 [US7] Build project with `dotnet build` - verify zero compilation errors
- [ ] T121 [US7] Run application and verify no runtime errors related to deleted components

**Checkpoint**: Codebase clean of obsolete components, build succeeds

---

## Phase 11: Polish & Cross-Cutting Concerns

**Purpose**: Final improvements affecting multiple user stories

- [ ] T122 [P] Update CHANGELOG.md with feature summary
- [ ] T123 [P] Update AGENTS.md if new patterns were introduced
- [ ] T124 Run quickstart.md validation scenarios
- [ ] T125 Performance test: Verify Dashboard loads <1s, Log search <2s for 10K entries
- [ ] T126 [P] Add XML documentation to any public members missing docs
- [ ] T127 Verify all forms properly dispose resources in Cleanup region

---

## Dependencies & Execution Order

### Phase Dependencies

```
Phase 1 (Setup/Models) ──────┐
                             ▼
Phase 2 (Foundational) ──────┤
                             ▼
Phase 3 (US1: DI Core) ◄─────┤ CRITICAL GATE
                             │
        ┌────────────────────┼────────────────────┐
        ▼                    ▼                    ▼
Phase 4 (US2: Dashboard) Phase 5 (US3: Logs) Phase 6 (US4: Feedback)
        │                    │                    │
        └────────────────────┼────────────────────┘
                             ▼
                    Phase 7 (US5: System Info)
                             │
                             ▼
                    Phase 8 (US6: System Health Form)
                             │
                             ▼
                    Phase 9 (Integration)
                             │
                             ▼
                    Phase 10 (US7: Cleanup)
                             │
                             ▼
                    Phase 11 (Polish)
```

### User Story Dependencies

| Story | Priority | Can Start After | Notes |
|-------|----------|-----------------|-------|
| US1 (DI Core) | P1 | Phase 2 | **BLOCKS ALL OTHERS** |
| US2 (Dashboard) | P2 | US1 complete | Can parallel with US3, US4 |
| US3 (Logs) | P2 | US1 complete | Can parallel with US2, US4 |
| US4 (Feedback) | P3 | US1 complete | Can parallel with US2, US3 |
| US5 (System Info) | P3 | US1 complete | Can parallel after US1 |
| US6 (Health Form) | P3 | US1 complete | Requires US5 service methods |
| US7 (Cleanup) | P4 | US2-US6 complete | Must be last |

### Parallel Execution Opportunities

**After US1 (Phase 3) completes, these can run in parallel:**
- Tasks T031-T041 (Dashboard - US2)
- Tasks T042-T059 (Logs - US3)
- Tasks T060-T071 (Feedback - US4)
- Tasks T072-T080 (System Info - US5)

**Within Phase 1, all model tasks (T002-T009) can run in parallel**

**Within Phase 2, stored procedure tasks (T010-T012) can run in parallel**

---

## Summary

| Phase | Task Count | Parallel Tasks | Story Coverage |
|-------|------------|----------------|----------------|
| Phase 1 (Setup) | 9 | 8 | All stories |
| Phase 2 (Foundational) | 6 | 3 | All stories |
| Phase 3 (US1) | 15 | 0 | US1 |
| Phase 4 (US2) | 11 | 1 | US2 |
| Phase 5 (US3) | 18 | 0 | US3 |
| Phase 6 (US4) | 12 | 0 | US4 |
| Phase 7 (US5) | 9 | 0 | US5 |
| Phase 8 (US6) | 15 | 0 | US6 |
| Phase 9 (Integration) | 9 | 0 | Cross-cutting |
| Phase 10 (US7) | 15 | 0 | US7 |
| Phase 11 (Polish) | 6 | 3 | Cross-cutting |

**Total Tasks**: 125  
**Parallel Opportunities**: 15 tasks  
**MVP Scope**: Phases 1-3 (US1 complete) = 30 tasks

---

## Implementation Strategy

### MVP Delivery (Phases 1-3)
Complete US1 (DI refactoring) first. This is the foundation that enables all other work and provides immediate value by modernizing the codebase architecture.

### Incremental Delivery
After MVP, stories can be delivered independently:
1. **US2 + US3** together provide core diagnostic value (Dashboard + Logs)
2. **US4** enhances existing feedback functionality
3. **US5 + US6** provide transparency to all users
4. **US7** cleanup only after all functionality verified







