# Feature Specification: Developer Tools Consolidation & Core Services Refactoring

**Feature Branch**: `006-dev-tools-consolidation`  
**Created**: 2025-12-13  
**Status**: Draft  
**Version**: 2.0 (Complete Redesign)

## Executive Summary

This specification outlines a **complete redesign** of the developer and user diagnostic interfaces, modernizing the application's core error handling and logging architecture while creating best-in-class diagnostic tools.

**Key Deliverables**:
- **Core Services Refactoring**: Convert `Service_ErrorHandler` and `LoggingUtility` from static classes to dependency-injected services (`IService_ErrorHandler`, `ILoggingService`)
- **Developer Tools Redesign**: Transform existing `Form_DeveloperTools` into a multi-tab diagnostic powerhouse with Dashboard, Logs, Feedback, and System Info tabs
- **New User-Facing Status View**: Create `Form_SystemHealth` accessible via View menu for non-developer users to check application health
- **Legacy Cleanup**: Remove obsolete forms, DAOs, and manual error reporting features

---

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Core Service Dependency Injection (Priority: P1)

As a developer, I want the core services (`Service_ErrorHandler`, `LoggingUtility`) to use dependency injection instead of static classes, so that the codebase follows modern best practices, enables proper unit testing, and maintains consistency with the DI architecture used elsewhere in the application.

**Why this priority**: Foundation for all other features. Without DI-enabled services, the new Developer Tools forms cannot be properly constructed and tested. This is a prerequisite for all subsequent phases.

**Independent Test**: Can be fully tested by verifying that all existing functionality continues to work after refactoring (error handling, logging), and that the services can be injected via constructor into any test class.

**Acceptance Scenarios**:

1. **Given** a form or service that needs logging, **When** the developer adds `ILoggingService` to the constructor, **Then** the service is automatically resolved from the DI container and available for use
2. **Given** a form that needs error handling, **When** an exception occurs, **Then** the `IService_ErrorHandler` processes it identically to the previous static implementation
3. **Given** the application starts, **When** the DI container initializes, **Then** `ILoggingService` and `IService_ErrorHandler` are registered as singletons and available throughout the application lifecycle
4. **Given** legacy code that used `LoggingUtility.Log()` statically, **When** the refactoring is complete, **Then** all references have been updated to use the injected `ILoggingService`

---

### User Story 2 - Developer Dashboard View (Priority: P2)

As a developer, I want to see a dashboard with real-time statistics, recent errors, and feedback summary at a glance, so that I can quickly assess application health without digging through individual log files.

**Why this priority**: Provides immediate value to developers by consolidating key metrics into one view. This is the "landing page" of the Developer Tools.

**Independent Test**: Can be fully tested by opening Developer Tools and verifying that dashboard shows accurate counts for errors, warnings, and feedback from the last 24 hours.

**Acceptance Scenarios**:

1. **Given** Developer Tools is opened, **When** the Dashboard tab is active, **Then** I see summary cards showing error count, warning count, and feedback count for the last 24 hours
2. **Given** the Dashboard tab is displayed, **When** I look at the timeline chart, **Then** I see a 7-day error trend with severity breakdown
3. **Given** errors have occurred, **When** I view the "Recent Errors" list, **Then** I see the 10 most recent errors with timestamp and brief description, and clicking one navigates to the Logs tab filtered to that error
4. **Given** the Dashboard is visible, **When** I click the "Refresh" button, **Then** all statistics and charts update to reflect the latest data

---

### User Story 3 - Advanced Log Viewer (Priority: P2)

As a developer, I want to search, filter, and analyze application logs with advanced tools (regex search, date range, severity filters, grouping), so that I can quickly diagnose issues without manual log file manipulation.

**Why this priority**: Core diagnostic capability. The advanced log viewer replaces the legacy `Form_ViewLogsForm` with significantly enhanced functionality.

**Independent Test**: Can be fully tested by generating test log entries and verifying they can be searched, filtered, and grouped correctly.

**Acceptance Scenarios**:

1. **Given** I am on the Logs tab, **When** I enter a search term with regex support, **Then** only log entries matching the pattern are displayed
2. **Given** logs from multiple days exist, **When** I select a date range (Today/Last 7 days/Last 30 days/Custom), **Then** only logs within that range are shown
3. **Given** I select specific severity levels (Info, Warning, Error, Critical), **When** the filter is applied, **Then** only logs of those severities appear
4. **Given** I select "Group By: Error Type", **When** the grouping is applied, **Then** logs are grouped by error type with expandable sections
5. **Given** I select a log entry in the list, **When** I view the details panel, **Then** I see the full message, stack trace (if applicable), and context data in formatted JSON
6. **Given** I am viewing a log entry, **When** I click "Copy" or "Export Entry", **Then** the log details are copied to clipboard or exported to a file

---

### User Story 4 - Enhanced Feedback Management (Priority: P3)

As a developer, I want to manage user feedback with enhanced filtering, bulk operations, and priority indicators, so that I can efficiently triage and resolve user-reported issues.

**Why this priority**: Builds on existing feedback functionality. The current `Form_DeveloperTools` feedback features are migrated to a dedicated tab with improvements.

**Independent Test**: Can be fully tested by creating test feedback entries and verifying they can be filtered, bulk-updated, and prioritized.

**Acceptance Scenarios**:

1. **Given** I am on the Feedback tab, **When** I view the stats bar, **Then** I see Total, Open, In Progress, and Resolved counts
2. **Given** multiple feedback items exist, **When** I filter by Status, Type, or Date range, **Then** only matching feedback items are displayed
3. **Given** I select multiple feedback items, **When** I use bulk operations, **Then** I can mark all selected as reviewed or change their status
4. **Given** feedback items have different priorities, **When** I view the list, **Then** priorities are indicated with color-coding (Red=High, Yellow=Medium, White=Low)

---

### User Story 5 - System Information View (Priority: P3)

As a developer, I want to see database health, application info, and performance metrics in one place, so that I can diagnose infrastructure issues without accessing external tools.

**Why this priority**: Complements the diagnostic suite but is not required for core error/log functionality.

**Independent Test**: Can be fully tested by verifying each panel displays accurate information about the database, application, and current performance.

**Acceptance Scenarios**:

1. **Given** I am on the System Info tab, **When** I view Database Health, **Then** I see connection status (online/offline), database version, and uptime
2. **Given** the application is running, **When** I view Application Info, **Then** I see version number, start time, current user, and role
3. **Given** the application is under load, **When** I view Performance Metrics, **Then** I see average response time, memory usage, and thread count

---

### User Story 6 - User-Facing System Health Monitor (Priority: P3)

As a non-developer user, I want to check application health status and view my submitted feedback, so that I can stay informed about system issues and track my reported problems without developer assistance.

**Why this priority**: New feature for end users. Provides transparency and reduces developer burden for status inquiries.

**Independent Test**: Can be fully tested by a non-developer user opening the System Health form and verifying they see only their own feedback and a simplified health indicator.

**Acceptance Scenarios**:

1. **Given** I am a logged-in user, **When** I open View → System Health (or press Ctrl+Shift+H), **Then** the System Health form opens
2. **Given** no critical errors in the last 24 hours, **When** I view the health indicator, **Then** it shows green with "System Operating Normally"
3. **Given** 1-5 errors in the last 24 hours, **When** I view the health indicator, **Then** it shows yellow with a warning message
4. **Given** 6+ errors in the last 24 hours, **When** I view the health indicator, **Then** it shows red with "System Experiencing Issues"
5. **Given** I have submitted feedback, **When** I view "My Submitted Feedback", **Then** I see only my feedback entries with Date, Type, Status, and Title (read-only)
6. **Given** I need help, **When** I click "Submit New Feedback" or "Contact IT Support", **Then** the appropriate action is taken (open feedback form or email client)

---

### User Story 7 - Legacy Cleanup (Priority: P4)

As a maintainer, I want obsolete forms, DAOs, and controls removed from the codebase, so that the project remains clean and developers aren't confused by deprecated code.

**Why this priority**: Cleanup task that can only be done after new features replace the old ones.

**Independent Test**: Can be fully tested by verifying the build succeeds and deleted components are not referenced anywhere.

**Acceptance Scenarios**:

1. **Given** the new Developer Tools is complete, **When** the cleanup is performed, **Then** `Forms/ViewLogs/`, `Forms/ErrorReports/`, `Forms/ErrorDialog/Form_ReportIssue.cs`, and `Forms/ErrorDialog/Form_ErrorReportDialog.cs` are deleted
2. **Given** the cleanup is performed, **When** I build the project, **Then** there are no compilation errors referencing deleted components
3. **Given** `Dao_ErrorReports.cs` functionality is migrated, **When** the cleanup is performed, **Then** the DAO is deleted and no code references it

---

### Edge Cases

- What happens when the database is unavailable during Developer Tools initialization? → Dashboard shows "Database Offline" status with cached data if available
- How does the system handle very large log files (100,000+ entries)? → Pagination limits display to 50 entries; virtual scrolling for performance
- What if a user has no submitted feedback? → "My Submitted Feedback" section shows "No feedback submitted yet" message
- How does the system handle concurrent log writes during active log viewing? → Auto-refresh timer (configurable) fetches new entries; manual refresh available
- What happens if theming is disabled? → Forms fall back to system colors while maintaining layout

---

## Requirements *(mandatory)*

### Functional Requirements

#### Database Access Requirements

- **FR-DB-001**: All new database operations MUST use `Helper_Database_StoredProcedure` methods
- **FR-DB-002**: New analytics queries MUST use stored procedures following naming convention: `md_devtools_{action}` (e.g., `md_devtools_GetLogStatistics`)
- **FR-DB-003**: All DAO methods MUST return `Model_Dao_Result<T>` with IsSuccess, Data, and ErrorMessage properties

#### Core Services Requirements

- **FR-CORE-001**: `LoggingUtility` MUST be refactored to `ILoggingService` / `Service_Logging` using dependency injection
- **FR-CORE-002**: `Service_ErrorHandler` MUST be refactored to `IService_ErrorHandler` / `Service_ErrorHandler` (non-static) using dependency injection
- **FR-CORE-003**: Both services MUST be registered as Singletons in `Service_OnStartup_DependencyInjection.cs`
- **FR-CORE-004**: All existing references to `LoggingUtility` (200+ files) MUST be updated to inject `ILoggingService`
- **FR-CORE-005**: All existing references to static `Service_ErrorHandler` MUST be updated to inject `IService_ErrorHandler`
- **FR-CORE-006**: `IService_ErrorHandler` MUST inject `ILoggingService` for internal logging

#### Developer Tools Requirements

- **FR-DT-001**: `Form_DeveloperTools` MUST be redesigned as a multi-tab interface with Dashboard, Logs, Feedback, and System Info tabs
- **FR-DT-002**: Dashboard tab MUST display error count, warning count, and feedback count for the last 24 hours
- **FR-DT-003**: Dashboard tab MUST display a 7-day error trend chart
- **FR-DT-004**: Logs tab MUST support search with regex capability
- **FR-DT-005**: Logs tab MUST support filtering by date range, severity, and source
- **FR-DT-006**: Logs tab MUST support grouping by error type, source, hour, or day
- **FR-DT-007**: Logs tab MUST display log details (full message, stack trace, context) in a details panel
- **FR-DT-008**: Logs tab MUST support export to CSV, JSON, and TXT formats
- **FR-DT-009**: Feedback tab MUST migrate existing functionality from current `Form_DeveloperTools`
- **FR-DT-010**: Feedback tab MUST display summary statistics (Total, Open, In Progress, Resolved)
- **FR-DT-011**: Feedback tab MUST support bulk operations (mark multiple as reviewed)
- **FR-DT-012**: System Info tab MUST display database health (connection status, version, uptime)
- **FR-DT-013**: System Info tab MUST display application info (version, start time, current user)
- **FR-DT-014**: System Info tab MUST display performance metrics (response time, memory, threads)
- **FR-DT-015**: All tabs MUST support keyboard shortcuts (Ctrl+F=Search, Ctrl+R=Refresh, F5=Refresh All)

#### System Health Requirements

- **FR-SH-001**: `Form_SystemHealth` MUST be created as a new form inheriting from `ThemedForm`
- **FR-SH-002**: Form MUST be accessible via `MainForm → View → System Health` menu item
- **FR-SH-003**: Form MUST be accessible via `Ctrl+Shift+H` keyboard shortcut
- **FR-SH-004**: Form MUST display overall health indicator (Green/Yellow/Red based on error count)
- **FR-SH-005**: Form MUST display user's submitted feedback in read-only grid
- **FR-SH-006**: Form MUST provide "Submit New Feedback" and "Contact IT Support" actions

#### Backend Service Requirements

- **FR-BE-001**: `IService_DeveloperTools` / `Service_DeveloperTools` MUST be created for diagnostic data access
- **FR-BE-002**: Service MUST implement `GetLogStatisticsAsync(DateTime start, DateTime end)`
- **FR-BE-003**: Service MUST implement `GetLogEntriesAsync(filters)` with advanced filtering
- **FR-BE-004**: Service MUST implement `GetErrorGroupingsAsync(groupBy)`
- **FR-BE-005**: Service MUST implement `GetLogTimelineAsync(granularity)` for chart data
- **FR-BE-006**: Service MUST implement `GetRecentErrorSummaryAsync(hours)`
- **FR-BE-007**: Service MUST implement `GetUserFeedbackStatusAsync(userId)`
- **FR-BE-008**: `IService_FeedbackManager` MUST be enhanced with `GetFeedbackSummaryAsync()` and `GetUserFeedbackAsync(userId)`

#### Cleanup Requirements

- **FR-CL-001**: `Forms/ViewLogs/` folder MUST be deleted after migration
- **FR-CL-002**: `Forms/ErrorReports/` folder MUST be deleted
- **FR-CL-003**: `Forms/ErrorDialog/Form_ReportIssue.cs` MUST be deleted
- **FR-CL-004**: `Forms/ErrorDialog/Form_ErrorReportDialog.cs` MUST be deleted (if exists)
- **FR-CL-005**: `Controls/ErrorReports/` folder MUST be deleted
- **FR-CL-006**: `Data/Dao_ErrorReports.cs` MUST be deleted
- **FR-CL-007**: `Data/Dao_ErrorLog.cs` MUST be deleted after logic migration to services

### Key Entities

- **LogEntry**: Represents a single log record with timestamp, severity, source, message, stack trace, and context data
- **LogStatistics**: Aggregated metrics including counts by severity, time period, and source
- **ErrorGrouping**: Grouped error data with count, first/last occurrence, and representative error
- **FeedbackSummary**: Summary of feedback counts by status (Total, Open, In Progress, Resolved)
- **SystemHealthStatus**: Health indicator (Green/Yellow/Red) with error count and last error timestamp
- **DatabaseHealth**: Connection status, version, uptime, and connection count
- **PerformanceMetrics**: Response time, memory usage, thread count, CPU usage

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Developers can access all diagnostic tools (logs, feedback, system info) from a single form with 4 tabs, reducing context switching between 3+ separate forms to 1
- **SC-002**: Log searches return results in under 2 seconds for datasets up to 10,000 entries
- **SC-003**: Dashboard statistics load within 1 second of tab activation
- **SC-004**: All existing logging and error handling functionality continues to work identically after DI refactoring (zero regression)
- **SC-005**: Build succeeds with zero compilation errors after all cleanup tasks are completed
- **SC-006**: Non-developer users can check system health status in under 10 seconds via the View menu
- **SC-007**: 100% of LoggingUtility and Service_ErrorHandler references are updated to use DI (no static calls remain)
- **SC-008**: Developer Tools form loads within 3 seconds on average hardware
- **SC-009**: Auto-refresh in Logs tab updates data every 30 seconds without user-visible lag

---

## Assumptions

1. The existing feedback management logic in `Form_DeveloperTools` is functional and only needs UI migration to a tab
2. The application already has a working DI container (`Service_OnStartup_DependencyInjection.cs`)
3. MySQL 5.7.24 constraints apply - no JSON functions, CTEs, or window functions in new stored procedures
4. Charts will use simple custom drawing or a compatible WinForms charting library (no external dependencies required)
5. The "Contact IT Support" action will open the default email client with a pre-filled support address
6. Log retention policy is already handled by existing mechanisms (no changes needed)
7. Performance metrics (response time, memory) will use .NET built-in diagnostics, not external monitoring

---

## Out of Scope

- Real-time push notifications for new errors (pull-based refresh only)
- Multi-user concurrent editing of feedback items
- Historical trend analysis beyond 30 days
- Integration with external issue tracking systems (GitHub Issues, Jira)
- Mobile or web-based diagnostic interfaces
- Automated error remediation or self-healing capabilities

---

## Dependencies

- Existing `Service_OnStartup_DependencyInjection.cs` for DI registration
- Existing `ThemedForm` base class for theme integration
- Existing `IService_FeedbackManager` for feedback data
- MySQL 5.7.24 database with existing logging tables
