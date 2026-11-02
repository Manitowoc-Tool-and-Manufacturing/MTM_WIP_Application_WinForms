# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `010-developer-tools-scaffold`
**Tasks**: TASK-127 … TASK-129

---

## 1. Project Topology & Directory Mapping

### 1.1 Module layout

`MTM_WIP.Module_DeveloperTools` already exists as an empty project created by `001` (references `Core`, `Shared`, `Data`; `UseWinUI=true`). This spec fills it:

```text
MTM_WIP.Module_DeveloperTools/
├── MTM_WIP.Module_DeveloperTools.csproj      # existing; unchanged
├── Views/
│   ├── View_DeveloperToolsPage.xaml(.cs)     # host: four destination tabs
│   ├── View_DeveloperDashboardPage.xaml(.cs)
│   ├── View_DeveloperLogsPage.xaml(.cs)
│   ├── View_DeveloperFeedbackPage.xaml(.cs)
│   ├── View_DeveloperSystemInfoPage.xaml(.cs)
│   ├── View_SubmitFeedbackPage.xaml(.cs)
│   └── View_DatabaseMaintenancePage.xaml(.cs)
├── ViewModels/
│   ├── ViewModel_DeveloperToolsPage.cs
│   ├── ViewModel_DeveloperDashboardPage.cs
│   ├── ViewModel_DeveloperLogsPage.cs
│   ├── ViewModel_DeveloperFeedbackPage.cs
│   ├── ViewModel_DeveloperSystemInfoPage.cs
│   ├── ViewModel_SubmitFeedbackPage.cs
│   └── ViewModel_DatabaseMaintenancePage.cs
├── Models/                                   # not created in v1 (no content)
├── Enums/                                    # not created in v1
├── Contracts/                                # not created in v1
└── Helpers/                                  # not created in v1
```

Per [`../constitution.md`](../constitution.md) §3.3, folders are created only when they have content. `Models/`, `Enums/`, `Contracts/`, and `Helpers/` remain absent in v1.

Reference constraint: the project may reference `Core`, `Shared`, and `Data` (Rule 10). In v1 it references **no** `Data` type; the `Data` reference is inherited from `001` and unused. The forbidden-API and reference-matrix gates from `001` continue to apply.

### 1.2 Navigation registration

The module registers two shell destinations: `DeveloperTools` (host page) and `DatabaseMaintenance`. The registration contract is owned by `004-shell-and-startup`, whose API is **(to confirm)**. The host page owns its four destination tabs internally; the tabs are not separate shell destinations.

---

## 2. Component Migration Strategy

### 2.1 The scaffold pattern

One reusable unavailable-state surface is defined once in this module and reused by every page. It renders a heading (the destination name) and a message. The message text is a XAML resource, not a string literal repeated across pages, so a future spec can replace the wording without touching seven pages. Nothing in the surface is actionable: no button, no hyperlink, no command.

### 2.2 Per-page plan

| Target page | Legacy source | v1 content | Future-spec hook |
| :--- | :--- | :--- | :--- |
| `View_DeveloperToolsPage` | `DeveloperToolsForm` | Four tabs, each an inert destination page | Tab shortcuts, Logs-only 30 s refresh |
| `View_DeveloperDashboardPage` | `Control_SystemHealth`, `Control_LogStatistics`, `Control_RecentErrors`, `Form_SystemHealth` | Unavailable state | Health tile, 24-hour stats, recent errors, current-user feedback strip |
| `View_DeveloperLogsPage` | `Control_LogViewer` | Unavailable state | Filters, group-by, debounced search, export, 100-row cap |
| `View_DeveloperFeedbackPage` | `Control_FeedbackManager` | Unavailable state | Feedback grid, bulk status, inline priority edit |
| `View_DeveloperSystemInfoPage` | `Control_SystemInfo`, `Control_DatabaseHealth` | Unavailable state | Process metrics, DB health grid, Test Connection |
| `View_SubmitFeedbackPage` | `Form_SubmitFeedback` | Unavailable state | Feedback form, tracking number |
| `View_DatabaseMaintenancePage` | `Form_DatabaseMaintenance` | Unavailable state + "no maintenance actions are available" | Future, gated by a safety review |

### 2.3 Explicitly not implemented in v1

Plain statement, not softened: **no DAO is injected into any ViewModel in v1.** This module does not reference `IDialogService`, `IErrorService`, `ICurrentUserService`, `IDispatcherService`, or the shared grid component from `003`. It creates no timer, no `DispatcherQueueTimer`, and no keyboard accelerator. The connection-strength control is owned by `003` and does not appear here. Every ViewModel has a parameterless constructor, exposes only the not-available title/message pair, and declares no `[RelayCommand]`.

---

## 3. Data & State Migration

### 3.1 Data access

None. `MTM_WIP.Module_DeveloperTools` performs zero data access in v1. The DAOs ported by `002` — `Dao_DeveloperTools`, `Dao_ErrorLog`, `Dao_ErrorReports`, `Dao_UserFeedback`, `Dao_UserFeedbackComments`, `Dao_WindowFormMapping`, `Dao_UserControlMapping`, `Dao_ParameterPrefixOverrides`, `Dao_EmailNotification` — exist in `MTM_WIP.Data` and are registered in DI, but no code path in this module resolves them. The wire-up is a future spec's job.

### 3.2 State and persistence

None introduced. The module holds no state, writes no file, reads no configuration, and persists nothing. The error-report queue (`%APPDATA%\MTM_Application\ErrorReports\Pending`), the mapping tables, and the parameter-prefix cache remain the property of `002`, `003`, and `009` respectively; this module neither reads nor writes them.

### 3.3 Database

The database is frozen ([`../constitution.md`](../constitution.md) §2 Rule 4). This module introduces no SQL, no procedure call, and no schema change. The destructive surface of `Form_DatabaseMaintenance` — bulk migration overwriting target data, single-table TRUNCATE-then-reload for md_part_ids, md_locations, md_operation_numbers, md_item_types, usr_users, inv_inventory, inv_transaction; clean-test-data DELETE (user JOHNK or test); OPTIMIZE; truncate `log_error`; archive+truncate logs; and the three-confirmation factory reset truncating inv_inventory, inv_transaction, sys_last_10_transactions — is documented in the spec only and is unreachable from any v1 UI.
