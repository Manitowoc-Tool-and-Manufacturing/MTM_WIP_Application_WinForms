# Developer Tools Consolidation & Core Services Refactoring Spec (v2.0 - Redesigned)

## 1. Overview
This specification outlines a **complete redesign** of the developer and user diagnostic interfaces, modernizing the application's core error handling and logging architecture while creating best-in-class diagnostic tools.

**Key Changes from v1.0**:
- **Complete UI Redesign**: Modern, information-dense layouts inspired by Visual Studio, Chrome DevTools, and Azure Portal
- **Dual-Purpose Interface**: Separate tools for **Developers** (full diagnostics) and **Users** (status monitoring)
- **Enhanced Visualization**: Real-time statistics, trend charts, severity heatmaps, and timeline views
- **Advanced Tooling**: Log filtering, grouping, search, export, and direct issue creation from logs
- **Status Dashboard**: New user-facing "System Health" view accessible via View menu
- **Retained**: Core services refactoring to DI (`IService_ErrorHandler`, `ILoggingService`)
- **Removed**: Manual error reporting feature (automatic logging only)

## 2. Scope

### 2.1 Core Modernization (No Change from v1.0)
*   **Refactor Core Services**: Convert `Service_ErrorHandler` and `LoggingUtility` to `IService_ErrorHandler` and `ILoggingService`.
*   **Backend Modernization**: Create `Service_DeveloperTools` for log analytics, statistics, and grouping.
*   **Cleanup**: Remove obsolete forms, DAOs, user-facing error reporting features.
    *   **Remove**: `Dao_ErrorReports.cs` (Manual reporting DAO)
    *   **Remove**: `Forms/ErrorDialog/Form_ReportIssue.cs` & `EnhancedErrorDialog.cs` (Manual reporting UI)
    *   **Remove**: `Controls/ErrorReports/` (All controls)
    *   **Remove**: `Forms/ErrorReports/` (All forms)
    *   **Remove**: `Forms/ViewLogs/Form_ViewLogsForm.cs` (Replaced by Logs tab)

### 2.2 New Developer Tools (Complete Redesign)
*   **Form_DeveloperTools**: Transform into a **multi-tab diagnostic powerhouse**:
    *   *Note*: Currently exists as a Feedback Manager. This functionality will be moved to the **Feedback Tab**.
    1.  **Dashboard Tab**: Real-time statistics, charts, recent errors/feedback summary
    2.  **Logs Tab**: Advanced log viewer with filtering, search, grouping, timeline (Migrated from `Form_ViewLogsForm`)
    3.  **Feedback Tab**: Existing feedback management with enhanced filtering/export (Migrated from current `Form_DeveloperTools`)
    4.  **System Info Tab**: Database health, performance metrics, configuration
*   **Enhanced Features**:
    *   Real-time log tailing
    *   Log severity heatmap (by hour/day)
    *   Error grouping by type/source
    *   Quick actions: "Create Issue from Log", "Export Filtered", "Copy Stack Trace"
    *   Keyboard shortcuts for navigation

### 2.3 New User-Facing Status View
*   **Form_SystemHealth**: NEW form accessible via `View → System Health` menu:
    *   **Purpose**: Allow non-developer users to check application health and their submitted feedback status
    *   **Read-Only Interface**: No admin controls, simplified view
    *   **Features**:
        *   Last 24-hour error summary (count only, no details)
        *   User's submitted feedback status (Pending/Reviewed/Resolved)
        *   System uptime and last error timestamp
        *   Contact developer button (opens email/creates feedback)

## 3. Target Architecture

### 3.1 Core Services (Dependency Injection) - UNCHANGED
*   **`ILoggingService` / `Service_Logging`**: Replaces static `LoggingUtility`.
*   **`IService_ErrorHandler` / `Service_ErrorHandler`**: Replaces static `Service_ErrorHandler`.
*   Both registered as `Singleton` in `Service_OnStartup_DependencyInjection`.

### 3.2 New Backend Services
*   **`IService_DeveloperTools` / `Service_DeveloperTools`**:
    *   Handles all diagnostic data access (logs, statistics, analytics).
    *   **Key Methods**:
        *   `GetLogStatisticsAsync(DateTime start, DateTime end)` → Returns error counts, severity distribution
        *   `GetLogEntriesAsync(filters)` → Advanced filtering (severity, source, date range, search term)
        *   `GetErrorGroupingsAsync(groupBy)` → Group errors by type, source, or user
        *   `GetLogTimelineAsync(granularity)` → Hourly/daily error counts for charts
        *   `GetRecentErrorSummaryAsync(hours)` → Last N hours summary for dashboard
        *   `GetUserFeedbackStatusAsync(userId)` → User's submitted feedback status
*   **`IService_FeedbackManager`** (Enhanced):
    *   Add `GetFeedbackSummaryAsync()` → Summary stats for dashboard
    *   Add `GetUserFeedbackAsync(userId)` → User-specific feedback

### 3.3 Developer Tools UI (REDESIGNED)
*   **Form**: `Form_DeveloperTools` (Forms/DeveloperTools/Form_DeveloperTools.cs)
*   **Layout**: Modern `TabControl` with 4 tabs:

#### 3.3.1 Dashboard Tab
*   **Left Panel** (Summary Cards):
    *   Last 24 Hours: Error count, Warning count, Feedback count
    *   Today's Activity: Log entries by hour (bar chart)
    *   Top Issues: Most frequent errors (top 5)
*   **Center Panel** (Timeline Chart):
    *   Last 7 days error trend (line chart)
    *   Severity breakdown (stacked area chart)
*   **Right Panel** (Quick Actions):
    *   Recent Errors (last 10, clickable to Logs tab)
    *   Recent Feedback (last 5, clickable to Feedback tab)
    *   Refresh button, Export Dashboard PDF

#### 3.3.2 Logs Tab (Advanced)
*   **Top Toolbar**:
    *   Search box with regex support
    *   Date range picker (Today/Last 7 days/Last 30 days/Custom)
    *   Severity filter (checkboxes: Info, Warning, Error, Critical)
    *   Source filter (dropdown: All/Forms/Services/Data/etc.)
    *   Group By dropdown (None/Error Type/Source/Hour/Day)
    *   Export button (CSV/JSON/TXT)
*   **Main View** (Split Container):
    *   **Left**: Log list (DataGridView or custom list)
        *   Columns: Timestamp, Severity (emoji), Source, Message (truncated)
        *   Color-coded by severity
        *   Double-click to show details in right panel
    *   **Right**: Log Details Panel
        *   Full message, stack trace, context data (JSON formatted)
        *   Quick actions: Copy, Create Feedback, Export Entry
*   **Bottom Status Bar**:
    *   Showing X of Y entries | Last updated: {time} | Auto-refresh toggle

#### 3.3.3 Feedback Tab (Enhanced)
*   **Existing functionality PLUS**:
    *   Enhanced filtering (Status, Type, Date range, Assigned to)
    *   Bulk operations (Mark multiple as reviewed)
    *   Priority indicators (color-coded)
    *   Quick stats at top: Total/Open/In Progress/Resolved

#### 3.3.4 System Info Tab (NEW)
*   **Database Health**:
    *   Connection status, version, uptime
    *   Table sizes, index health
*   **Application Info**:
    *   Version, last restart, current user, role
*   **Performance Metrics**:
    *   Avg response time (last hour)
    *   Memory usage, thread count

### 3.4 User-Facing Status UI (NEW)
*   **Form**: `Form_SystemHealth` (Forms/SystemHealth/Form_SystemHealth.cs)
*   **Layout**: Simple, read-only dashboard
*   **Sections**:
    1.  **Application Status**: Green/Yellow/Red indicator
        *   Green: No errors in last 24 hours
        *   Yellow: 1-5 errors
        *   Red: 6+ errors
    2.  **My Feedback**: User's submitted feedback list
        *   Columns: Date, Type, Status, Title
        *   Read-only, no editing
    3.  **Contact Support**: Button to create new feedback/email IT
*   **Access**: `MainForm → View Menu → System Health`

## 4. Implementation Plan

### Phase 1: Core Service Refactoring (The Foundation)
*Objective: Modernize core services to support DI before building new features.*

1.  **Logging Service**:
    *   Define `ILoggingService` interface matching `LoggingUtility` public API.
    *   Create `Service_Logging` implementation (move logic from `LoggingUtility`).
    *   Register `ILoggingService` in `Service_OnStartup_DependencyInjection.cs`.
    *   **Mass Refactoring**: Update all >200 references to `LoggingUtility` to inject `ILoggingService`.
        *   *Note*: This affects almost every file in the project.
    *   Delete `LoggingUtility.cs`.

2.  **Error Handler Service**:
    *   Define `IService_ErrorHandler` interface matching `Service_ErrorHandler` public API.
    *   Create `Service_ErrorHandler` implementation (non-static).
    *   Inject `ILoggingService` into `Service_ErrorHandler`.
    *   Register `IService_ErrorHandler` in `Service_OnStartup_DependencyInjection.cs`.
    *   **Mass Refactoring**: Update all references to `Service_ErrorHandler` to inject `IService_ErrorHandler`.
    *   Delete static `Service_ErrorHandler.cs` (or rename implementation file).

#### Implementation Guidance for Mass Refactoring
*   **Order of Operations**:
    1.  **Interface Extraction**: Extract interfaces (`ILoggingService`, `IService_ErrorHandler`) first.
    2.  **Implementation**: Create the new service classes (`Service_Logging`, `Service_ErrorHandler`).
    3.  **Registration**: Add to `Service_OnStartup_DependencyInjection.cs`.
    4.  **Refactoring Loop**:
        *   Identify a batch of files (e.g., by folder: `Data/`, then `Services/`, then `Forms/`).
        *   Add the interface to the constructor.
        *   Replace static calls with instance calls. This may NOT be done with scripting!
*   **Handling Static Methods**:
    *   If a static method uses the static service, convert the method to non-static and inject the service into the containing class.
    *   If the class *must* remain static (e.g., Extension methods), pass the service as a parameter to the method.
*   **WinForms Considerations**:
    *   Ensure `Program.cs` or the main entry point resolves the main form via DI to start the dependency chain.
    *   For child forms/controls, pass dependencies down or resolve via a Factory/ServiceProvider.

### Phase 2: Backend Implementation (Developer Tools)
*Objective: Create the backend logic for the new consolidated tools.*

1.  **Service_DeveloperTools**:
    *   Create `IService_DeveloperTools` and `Service_DeveloperTools`.
    *   **Migrate Logs Logic**: Move read-only log viewing logic from `Dao_ErrorLog` and `Form_ViewLogsForm` (Log parsing, filtering) to this service.
    *   Register in DI container.

2.  **Update Error Logging**:
    *   Ensure `Service_ErrorHandler` continues automatic error logging to database (no user interaction required).
    *   Remove any user-facing error submission dialogs or forms.

### Phase 3: UI Implementation (Complete Redesign)
*Objective: Build modern, information-dense diagnostic interfaces.*

#### 3.1 Form_DeveloperTools Redesign

**Main Layout**:
```
┌─ Form_DeveloperTools ────────────────────────────────────────────────────────┐
│ [Dashboard] [Logs] [Feedback] [System Info]                    [Refresh] [?] │
├──────────────────────────────────────────────────────────────────────────────┤
│ TabControl Content (See below)                                                │
├──────────────────────────────────────────────────────────────────────────────┤
│ Status Bar: Loading... | Last updated: 2:34 PM                               │
└──────────────────────────────────────────────────────────────────────────────┘
```

**Tab 1: Dashboard** (`Form_DeveloperTools_TabPage_Dashboard`)
```
┌─ 3-Column Layout (TableLayoutPanel) ──────────────────────────────────────┐
│ ┌─ Summary Cards ─┐  ┌─ Timeline Chart ──────┐  ┌─ Recent Activity ──┐ │
│ │ 📊 Last 24 Hrs   │  │ 📈 7-Day Error Trend  │  │ 🔴 Recent Errors  │ │
│ │ Errors: 23       │  │ [Line Chart]          │  │ • 2:30 PM NullRef │ │
│ │ Warnings: 45     │  │                       │  │ • 2:15 PM SQL Err │ │
│ │ Feedback: 5      │  │ [Severity Breakdown]  │  │ • 2:00 PM Timeout │ │
│ │                  │  │                       │  │                   │ │
│ │ 📈 Today's       │  │                       │  │ 💬 Recent Feedbk  │ │
│ │    Activity      │  │                       │  │ • User requested  │ │
│ │ [Bar Chart]      │  │                       │  │ • Bug report      │ │
│ │                  │  │                       │  │                   │ │
│ │ 🔥 Top Issues    │  │                       │  │ [Export PDF]      │ │
│ │ 1. DB Timeout    │  │                       │  │                   │ │
│ │ 2. Null Ref      │  │                       │  │                   │ │
│ └──────────────────┘  └───────────────────────┘  └───────────────────┘ │
└────────────────────────────────────────────────────────────────────────────┘
```

**Tab 2: Logs** (`Form_DeveloperTools_TabPage_Logs`)
```
┌─ Toolbar Panel ────────────────────────────────────────────────────────────┐
│ 🔍 [Search: regex support...] [📅 Last 7 days ▼] [Severity: ☑️ All]      │
│ [Source: All ▼] [Group By: Error Type ▼] [🔄 Auto-refresh] [📤 Export]   │
├────────────────────────────────────────────────────────────────────────────┤
│ ┌─ Log List (60%) ────────────┐ ┌─ Details Panel (40%) ───────────────┐ │
│ │ Time      │ Lvl │ Source │ M │ │ 📋 Full Message:                    │ │
│ │ 2:34:12   │ 🔴  │ Data   │ N │ │ NullReferenceException at line 234  │ │
│ │ 2:33:45   │ ⚠️  │ Form   │ L │ │                                     │ │
│ │ 2:32:10   │ 🔴  │ Svc    │ T │ │ 📚 Stack Trace:                     │ │
│ │ 2:30:05   │ ℹ️  │ Data   │ Q │ │ [Formatted stack trace]             │ │
│ │ (Color-coded by severity)   │ │                                     │ │
│ │ [Paginate: 1-50 of 234]     │ │ 🔧 Quick Actions:                   │ │
│ └─────────────────────────────┘ │ [Copy] [Create Feedback] [Export]   │ │
│                                   └─────────────────────────────────────┘ │
├────────────────────────────────────────────────────────────────────────────┤
│ Showing 234 of 1,245 entries | Last updated: 2:34 PM | Auto-refresh: ON   │
└────────────────────────────────────────────────────────────────────────────┘
```

**Tab 3: Feedback** (`Form_DeveloperTools_TabPage_Feedback`)
```
┌─ Enhanced Filters ─────────────────────────────────────────────────────────┐
│ [Status: All ▼] [Type: All ▼] [📅 Date: Last 30 days ▼] [Apply]          │
│ 📊 Stats: Total: 45 | Open: 12 | In Progress: 8 | Resolved: 25           │
├────────────────────────────────────────────────────────────────────────────┤
│ ┌─ Feedback List (60%) ───┐ ┌─ Details Panel (40%) ──────────────────┐  │
│ │ ID │ Date  │ Type │ Pri │ │ ID: 123                                 │  │
│ │ 45 │ 12/10 │ Bug  │ 🔴  │ │ Date: 12/10/2025                        │  │
│ │ 44 │ 12/09 │ Feat │ ⚪  │ │ User: John Doe                          │  │
│ │ 43 │ 12/08 │ Bug  │ 🟡  │ │ Type: Bug Report                        │  │
│ │ (Context menu: Update,   │ │ Status: Open                            │  │
│ │  Assign, Mark Duplicate) │ │                                         │  │
│ │ [Bulk Actions ▼]         │ │ Description:                            │  │
│ └──────────────────────────┘ │ [Full description text]                 │  │
│                                │ [Update Status] [Assign] [Add Notes]   │  │
│                                └────────────────────────────────────────┘  │
└────────────────────────────────────────────────────────────────────────────┘
```

**Tab 4: System Info** (`Form_DeveloperTools_TabPage_SystemInfo`)
```
┌─ 3-Panel Layout ───────────────────────────────────────────────────────────┐
│ ┌─ Database Health ──┐ ┌─ Application Info ─┐ ┌─ Performance Metrics ─┐ │
│ │ Status: 🟢 Online  │ │ Version: 6.4.1.0   │ │ Avg Response: 45ms    │ │
│ │ Uptime: 5d 3h 12m  │ │ Started: 12/8 8AM  │ │ Memory: 245 MB        │ │
│ │ Version: 5.7.24    │ │ User: JohnDoe      │ │ Threads: 18           │ │
│ │ Connections: 12    │ │ Role: Developer    │ │ CPU: 12%              │ │
│ │                    │ │                    │ │                       │ │
│ │ [Run Diagnostics]  │ │ [View Config]      │ │ [Export Report]       │ │
│ └────────────────────┘ └────────────────────┘ └───────────────────────┘ │
└────────────────────────────────────────────────────────────────────────────┘
```

**Naming Conventions** (CRITICAL):
- TabControl: `Form_DeveloperTools_TabControl_Main`
- TabPages: `Form_DeveloperTools_TabPage_{Name}` (Dashboard/Logs/Feedback/SystemInfo)
- All controls: `Form_DeveloperTools_{ControlType}_{TabName}_{Purpose}`
- Example: `Form_DeveloperTools_Button_Dashboard_Refresh`

#### 3.2 Form_SystemHealth (NEW - User-Facing)

**Layout**:
```
┌─ System Health Monitor ────────────────────────────────────────────────────┐
│ Application Status                                       [Close] [Refresh] │
├────────────────────────────────────────────────────────────────────────────┤
│ ┌─ Overall Health ─────────────────────────────────────────────────────┐  │
│ │              🟢 System Operating Normally                            │  │
│ │                                                                       │  │
│ │ Last 24 Hours:  3 warnings, 0 critical errors                        │  │
│ │ Last Error:     2 hours ago (resolved)                               │  │
│ │ System Uptime:  5 days, 3 hours                                      │  │
│ └───────────────────────────────────────────────────────────────────────┘  │
│                                                                             │
│ ┌─ My Submitted Feedback ──────────────────────────────────────────────┐  │
│ │ Date     │ Type        │ Status      │ Title                         │  │
│ │ 12/10    │ Bug Report  │ In Progress │ Print dialog freezes         │  │
│ │ 12/05    │ Feature Req │ Reviewed    │ Add export to Excel          │  │
│ │ 12/01    │ Bug Report  │ Resolved    │ Inventory count wrong        │  │
│ └───────────────────────────────────────────────────────────────────────┘  │
│                                                                             │
│ Need help? [Submit New Feedback] [Contact IT Support]                     │
└────────────────────────────────────────────────────────────────────────────┘
```

**Access**:
- Menu: `MainForm → View → System Health`
- Shortcut: `Ctrl+Shift+H`

#### 3.3 Implementation Details

**Dependencies**:
- Inject `IService_DeveloperTools`, `IService_ErrorHandler`, `ILoggingService`
- Inject `IService_FeedbackManager` for both forms

**Key Features**:
- **Real-time updates**: Use `Timer` for auto-refresh (configurable interval)
- **Lazy loading**: Load tabs only when activated
- **Caching**: Cache dashboard statistics for 30 seconds
- **Pagination**: Logs tab shows 50 entries at a time
- **Keyboard shortcuts**: 
  - `Ctrl+F`: Focus search
  - `Ctrl+R`: Refresh current tab
  - `Ctrl+E`: Export current view
  - `F5`: Refresh all

**Theme Integration**:
- Both forms inherit from `ThemedForm`
- Charts use theme colors
- Severity colors: Red (Error), Yellow (Warning), Blue (Info), Gray (Debug)

### Phase 4: Cleanup & Verification
*Objective: Remove legacy code and verify system integrity.*

1.  **Delete Legacy Components**:
    *   `Forms/ViewLogs/` (Folder)
    *   `Forms/ErrorReports/` (Folder)
    *   `Forms/ErrorDialog/Form_ReportIssue.cs`
    *   `Forms/ErrorDialog/Form_ErrorReportDialog.cs`
    *   `Controls/ErrorReports/` (Folder)
    *   `Data/Dao_ErrorReports.cs`
    *   `Data/Dao_ErrorLog.cs` (Logic moved to Services)

2.  **Verification**:
    *   Verify automatic error logging.
    *   Verify Developer Tools and System Health forms.

## 5. Work to be Done (Checklist v2.0)

### Phase 1: Core Services (UNCHANGED)
- [ ] **Core: Logging**
    - [ ] Create `ILoggingService` & `Service_Logging`.
    - [ ] Register in DI.
    - [ ] Refactor all `LoggingUtility` usages.
    - [ ] Delete `LoggingUtility`.
    - [ ] **Doc**: Update Copilot instructions & write Serena memory.

- [ ] **Core: Error Handling**
    - [ ] Create `IService_ErrorHandler` & `Service_ErrorHandler` (impl).
    - [ ] Register in DI.
    - [ ] Refactor all `Service_ErrorHandler` usages.
    - [ ] Delete static `Service_ErrorHandler`.
    - [ ] **Doc**: Update Copilot instructions & write Serena memory.

### Phase 2: Backend Services (ENHANCED)
- [ ] **Service_DeveloperTools**
    - [ ] Create `IService_DeveloperTools` interface
    - [ ] Implement `GetLogStatisticsAsync(start, end)`
    - [ ] Implement `GetLogEntriesAsync(filters)`
    - [ ] Implement `GetErrorGroupingsAsync(groupBy)`
    - [ ] Implement `GetLogTimelineAsync(granularity)`
    - [ ] Implement `GetRecentErrorSummaryAsync(hours)`
    - [ ] Implement `GetUserFeedbackStatusAsync(userId)`
    - [ ] Register in DI
    - [ ] **Doc**: Write Serena memory for analytics architecture

- [ ] **Service_FeedbackManager** (Enhance)
    - [ ] Add `GetFeedbackSummaryAsync()`
    - [ ] Add `GetUserFeedbackAsync(userId)`

### Phase 3: UI Implementation (COMPLETE REDESIGN)
- [ ] **Form_DeveloperTools Redesign**
    - [ ] **Dashboard Tab**
        - [ ] Create summary cards (errors, warnings, feedback)
        - [ ] Implement 7-day trend chart (line chart control or custom)
        - [ ] Add recent activity lists
        - [ ] Wire up data binding to `IService_DeveloperTools`
    - [ ] **Logs Tab**
        - [ ] Create advanced toolbar (search, filters, group by)
        - [ ] Implement split view (list + details)
        - [ ] Add color-coding by severity
        - [ ] Implement pagination (50 entries at a time)
        - [ ] Add quick actions (Copy, Create Feedback, Export)
        - [ ] Implement auto-refresh timer
    - [ ] **Feedback Tab**
        - [ ] **Migrate existing logic**: Move all code from current `Form_DeveloperTools` to this tab.
        - [ ] Enhance existing UI with stats bar
        - [ ] Add bulk operations support
        - [ ] Improve filtering controls
    - [ ] **System Info Tab**
        - [ ] Create 3-panel layout
        - [ ] Add database health checks
        - [ ] Display application info
        - [ ] Show performance metrics

- [ ] **Form_SystemHealth (NEW)**
    - [ ] Create new form inheriting from `ThemedForm`
    - [ ] Implement overall health indicator (Green/Yellow/Red)
    - [ ] Add user feedback status grid (read-only)
    - [ ] Wire up "Submit Feedback" button
    - [ ] Add menu item: `MainForm → View → System Health`
    - [ ] Implement keyboard shortcut: `Ctrl+Shift+H`

### Phase 4: Integration & Polish
- [ ] **Keyboard Shortcuts**
    - [ ] `Ctrl+F`: Focus search in Logs tab
    - [ ] `Ctrl+R`: Refresh current tab
    - [ ] `Ctrl+E`: Export current view
    - [ ] `F5`: Refresh all data
    - [ ] `Ctrl+Shift+H`: Open System Health

- [ ] **Theme Integration**
    - [ ] Apply theme colors to charts
    - [ ] Ensure severity colors respect theme
    - [ ] Test light/dark theme switching

- [ ] **Performance Optimization**
    - [ ] Implement lazy loading for tabs
    - [ ] Cache dashboard data (30 seconds)
    - [ ] Use virtual scrolling for large log lists

### Phase 5: Cleanup (UNCHANGED)
- [ ] Delete legacy Forms:
    - [ ] `Forms/ViewLogs/`
    - [ ] `Forms/ErrorReports/`
    - [ ] `Forms/ErrorDialog/Form_ReportIssue.cs`
    - [ ] `Forms/ErrorDialog/Form_ErrorReportDialog.cs`
- [ ] Delete legacy DAOs:
    - [ ] `Data/Dao_ErrorReports.cs`
    - [ ] `Data/Dao_ErrorLog.cs` (after logic migration)
- [ ] Delete legacy Controls:
    - [ ] `Controls/ErrorReports/`
- [ ] Remove "Report Issue" menu items/buttons from EnhancedErrorDialog
- [ ] Delete error reporting models and services
- [ ] Update Release Notes
- [ ] **Doc**: Finalize all documentation updates

## 6. Documentation & Knowledge Management
*   **Requirement**: After completing each major task (or phase), documentation must be generated to ensure future maintainability and AI context awareness.
*   **Deliverables**:
    1.  **GitHub Copilot Instructions**: Update `.github/copilot-instructions.md` (or similar) if new patterns are introduced (e.g., "Always inject `ILoggingService` instead of using static `LoggingUtility`").
    2.  **Serena Memories**: Create or update memory files (e.g., `architectural_decisions.md`, `service_layer.md`) using `mcp_oraios_serena_write_memory` to capture:
        *   The shift from static to DI for core services.
        *   The structure of the new `Service_DeveloperTools`.
        *   Any specific "gotchas" encountered during the refactoring.
