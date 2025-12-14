# Requirements Validation Checklist

**Feature**: 006-dev-tools-consolidation  
**Date**: 2025-12-13  
**Status**: Pre-Implementation

---

## Database Access Requirements

- [ ] **FR-DB-001**: All new database operations use `Helper_Database_StoredProcedure` methods
- [ ] **FR-DB-002**: New analytics queries use stored procedures with `md_devtools_` prefix
- [ ] **FR-DB-003**: All DAO methods return `Model_Dao_Result<T>` with IsSuccess, Data, and ErrorMessage

## Core Services Requirements

- [ ] **FR-CORE-001**: `LoggingUtility` refactored to `ILoggingService` / `Service_Logging` using DI
- [ ] **FR-CORE-002**: `Service_ErrorHandler` refactored to `IService_ErrorHandler` (non-static) using DI
- [ ] **FR-CORE-003**: Both services registered as Singletons in `Service_OnStartup_DependencyInjection.cs`
- [ ] **FR-CORE-004**: All existing references to `LoggingUtility` updated to inject `ILoggingService`
- [ ] **FR-CORE-005**: All existing references to static `Service_ErrorHandler` updated to inject `IService_ErrorHandler`
- [ ] **FR-CORE-006**: `IService_ErrorHandler` injects `ILoggingService` for internal logging

## Developer Tools Requirements

- [ ] **FR-DT-001**: `Form_DeveloperTools` redesigned as multi-tab interface (Dashboard, Logs, Feedback, System Info)
- [ ] **FR-DT-002**: Dashboard tab displays error/warning/feedback count for last 24 hours
- [ ] **FR-DT-003**: Dashboard tab displays 7-day error trend chart
- [ ] **FR-DT-004**: Logs tab supports regex search
- [ ] **FR-DT-005**: Logs tab supports filtering by date range, severity, and source
- [ ] **FR-DT-006**: Logs tab supports grouping by error type, source, hour, or day
- [ ] **FR-DT-007**: Logs tab displays log details (message, stack trace, context) in details panel
- [ ] **FR-DT-008**: Logs tab supports export to CSV, JSON, and TXT formats
- [ ] **FR-DT-009**: Feedback tab migrates existing functionality from current `Form_DeveloperTools`
- [ ] **FR-DT-010**: Feedback tab displays summary statistics (Total, Open, In Progress, Resolved)
- [ ] **FR-DT-011**: Feedback tab supports bulk operations (mark multiple as reviewed)
- [ ] **FR-DT-012**: System Info tab displays database health (connection status, version, uptime)
- [ ] **FR-DT-013**: System Info tab displays application info (version, start time, current user)
- [ ] **FR-DT-014**: System Info tab displays performance metrics (response time, memory, threads)
- [ ] **FR-DT-015**: All tabs support keyboard shortcuts (Ctrl+F=Search, Ctrl+R=Refresh, F5=Refresh All)

## System Health Requirements

- [ ] **FR-SH-001**: `Form_SystemHealth` created as new form inheriting from `ThemedForm`
- [ ] **FR-SH-002**: Form accessible via `MainForm → View → System Health` menu item
- [ ] **FR-SH-003**: Form accessible via `Ctrl+Shift+H` keyboard shortcut
- [ ] **FR-SH-004**: Form displays overall health indicator (Green/Yellow/Red based on error count)
- [ ] **FR-SH-005**: Form displays user's submitted feedback in read-only grid
- [ ] **FR-SH-006**: Form provides "Submit New Feedback" and "Contact IT Support" actions

## Backend Service Requirements

- [ ] **FR-BE-001**: `IService_DeveloperTools` / `Service_DeveloperTools` created for diagnostic data access
- [ ] **FR-BE-002**: Service implements `GetLogStatisticsAsync(DateTime start, DateTime end)`
- [ ] **FR-BE-003**: Service implements `GetLogEntriesAsync(filters)` with advanced filtering
- [ ] **FR-BE-004**: Service implements `GetErrorGroupingsAsync(groupBy)`
- [ ] **FR-BE-005**: Service implements `GetLogTimelineAsync(granularity)` for chart data
- [ ] **FR-BE-006**: Service implements `GetRecentErrorSummaryAsync(hours)`
- [ ] **FR-BE-007**: Service implements `GetUserFeedbackStatusAsync(userId)`
- [ ] **FR-BE-008**: `IService_FeedbackManager` enhanced with `GetFeedbackSummaryAsync()` and `GetUserFeedbackAsync(userId)`

## Cleanup Requirements

- [ ] **FR-CL-001**: `Forms/ViewLogs/` folder deleted after migration
- [ ] **FR-CL-002**: `Forms/ErrorReports/` folder deleted
- [ ] **FR-CL-003**: `Forms/ErrorDialog/Form_ReportIssue.cs` deleted
- [ ] **FR-CL-004**: `Forms/ErrorDialog/Form_ErrorReportDialog.cs` deleted
- [ ] **FR-CL-005**: `Controls/ErrorReports/` folder deleted
- [ ] **FR-CL-006**: `Data/Dao_ErrorReports.cs` deleted
- [ ] **FR-CL-007**: `Data/Dao_ErrorLog.cs` deleted after logic migration to services

---

## Success Criteria Validation

- [ ] **SC-001**: Single form with 4 tabs (Dashboard, Logs, Feedback, System Info)
- [ ] **SC-002**: Log search <2s for 10K entries
- [ ] **SC-003**: Dashboard loads <1s
- [ ] **SC-004**: Zero regression in existing logging/error handling
- [ ] **SC-005**: Build succeeds after cleanup
- [ ] **SC-006**: System Health accessible via View menu in <10s
- [ ] **SC-007**: No static LoggingUtility/Service_ErrorHandler calls remain
- [ ] **SC-008**: Form loads <3s
- [ ] **SC-009**: Auto-refresh works without lag

---

## Acceptance Test Scenarios

### User Story 1 - Core Service Dependency Injection

- [ ] AS-1.1: Form with `ILoggingService` constructor injection works
- [ ] AS-1.2: Exception handling identical to previous static implementation
- [ ] AS-1.3: Services available as singletons after DI container init
- [ ] AS-1.4: All LoggingUtility references updated

### User Story 2 - Developer Dashboard View

- [ ] AS-2.1: Dashboard shows summary cards with 24h counts
- [ ] AS-2.2: 7-day error trend chart displays
- [ ] AS-2.3: Recent errors list with navigation to Logs tab
- [ ] AS-2.4: Refresh button updates all statistics

### User Story 3 - Advanced Log Viewer

- [ ] AS-3.1: Regex search works
- [ ] AS-3.2: Date range filtering works
- [ ] AS-3.3: Severity filtering works
- [ ] AS-3.4: Error type grouping works
- [ ] AS-3.5: Details panel shows full message/stack trace
- [ ] AS-3.6: Copy and export work

### User Story 4 - Enhanced Feedback Management

- [ ] AS-4.1: Stats bar shows Total/Open/In Progress/Resolved
- [ ] AS-4.2: Filtering by Status/Type/Date works
- [ ] AS-4.3: Bulk operations work
- [ ] AS-4.4: Priority color-coding works

### User Story 5 - System Information View

- [ ] AS-5.1: Database health panel shows connection status
- [ ] AS-5.2: Application info panel shows version/user
- [ ] AS-5.3: Performance metrics panel shows memory/threads

### User Story 6 - User-Facing System Health Monitor

- [ ] AS-6.1: Form opens via View → System Health
- [ ] AS-6.2: Green indicator when no errors
- [ ] AS-6.3: Yellow indicator for 1-5 errors
- [ ] AS-6.4: Red indicator for 6+ errors
- [ ] AS-6.5: User's feedback displayed read-only
- [ ] AS-6.6: Submit New Feedback and Contact IT Support actions work

### User Story 7 - Legacy Cleanup

- [ ] AS-7.1: Specified folders/files deleted
- [ ] AS-7.2: Build succeeds
- [ ] AS-7.3: No references to deleted components

---

## Completion Sign-Off

| Phase | Completed | Date | Verified By |
|-------|-----------|------|-------------|
| Phase 0 (Research) | ✅ | 2025-12-13 | - |
| Phase 1 (Design) | ✅ | 2025-12-13 | - |
| Phase 2 (Core Services) | ☐ | - | - |
| Phase 3 (UI Development) | ☐ | - | - |
| Phase 4 (Integration) | ☐ | - | - |
| Phase 5 (Cleanup) | ☐ | - | - |
| Final Validation | ☐ | - | - |
