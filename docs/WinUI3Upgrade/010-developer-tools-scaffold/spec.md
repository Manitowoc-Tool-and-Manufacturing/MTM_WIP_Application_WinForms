# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `010-developer-tools-scaffold`
**Depends on**: `001-foundation-and-tooling`, `002-data-access-layer`, `003-crosscutting-services-and-theming`, `004-shell-and-startup` (navigation registration)
**Unblocks**: `011-deployment-and-cutover` (TASK-129 validates it)
**Tasks**: TASK-127 … TASK-129
**Governing rules**: [`../constitution.md`](../constitution.md) §2 Rules 4, 5, 10, 11, 12; §3

---

## 1. Executive Summary & Scope

This spec scaffolds `MTM_WIP.Module_DeveloperTools` **only**. In v1 the module compiles, is navigable from the shell, and performs no work. Every destination screen renders an explicit "not available in this release" state, and no destructive action is reachable. Its purpose is threefold: (a) preserve the navigation shape so the application looks complete, (b) document what exists in the WinForms build so a later spec can finish it, and (c) make the absence visible rather than silent.

This is not a feature deferral by accident. The WinForms Developer Tools surface contains the highest-risk code in the application — a database-maintenance console whose factory reset truncates three tables. Shipping an inert placeholder is the correct v1 outcome; shipping a half-ported maintenance console is a data-loss incident waiting to happen.

### In scope

- One Developer Tools page registered with the shell, hosting four destination tabs: Dashboard, Logs, Feedback, System Info.
- One Database Maintenance page, equally inert, carrying no reachable action.
- A single reusable unavailable-state surface used by every page in the module.
- This documentation: the authoritative inventory of the WinForms developer-tools surface.

### Out of scope

- Any DAO call, any ViewModel that resolves a service, any timer, any keyboard-shortcut handler, any export, any SMTP/email, any error-report queue.
- Every action of `Form_DatabaseMaintenance` (bulk migration, single-table reload, clean-test-data delete, OPTIMIZE, log truncation, archive, factory reset). None is implemented; none is reachable.
- The end-user error dialog itself — `EnhancedErrorDialog` is ported by `003`, not here.

### Success condition

`dotnet build MTM_WIP.sln -c Debug` is green. The Developer Tools page (four tabs) and the Database Maintenance page are navigable from the shell. A grep gate returns zero hits inside `MTM_WIP.Module_DeveloperTools` for `IDao_*`, `IErrorService`, `IDialogService`, `ICurrentUserService`, `TRUNCATE`, `DELETE`, and `OPTIMIZE`.

---

## 2. Legacy Feature Assessment & UI Mapping Table

Delivery status is **Scaffolded** for every row: the target page exists and navigates, but no behaviour is wired. "What a future spec must build" names the work the scaffold deliberately omits. Everything below is grounded in the verified WinForms source; nothing is invented.

### 2.1 Screens and controls

| WinForms Artefact | Target WinUI 3 Artefact | Delivery | What a future spec must build |
| :--- | :--- | :--- | :--- |
| `DeveloperToolsForm` — 4-tab shell (Dashboard / Logs / Feedback / System Info) with shortcuts Ctrl+1..4, Ctrl+F, Ctrl+R/F5, Esc, and a 30-second timer that refreshes the Logs tab only | `View_DeveloperToolsPage` host with four destination pages | Scaffolded | Restore tab shortcuts and the Logs-only 30-second refresh |
| `Control_SystemHealth` — Green/Yellow/Red tile with error/warn counts; click filters logs to "Today". Source `md_devtools_GetLogStatistics`. Thresholds: errorCount > 10 = Red, > 0 = Yellow, else Green over a one-hour window | `View_DeveloperDashboardPage` section | Scaffolded | Wire `Dao_DeveloperTools`, reproduce the one-hour threshold, click-to-filter |
| `Control_LogStatistics` — 24-hour totals + "Sync DB" + "Purge Logs" buttons. Sync truncates the log table then re-imports from `*_app_error.csv` / `*_db_error.csv` under the log directory; Purge deletes all `*.csv` there | `View_DeveloperDashboardPage` section | Scaffolded | Wire sync/purge with explicit confirmation; preserve "truncate then re-import" order |
| `Control_RecentErrors` — last 10 Error/Critical rows; row click navigates to the log viewer | `View_DeveloperDashboardPage` section | Scaffolded | Wire recent-errors query and deep-link into Logs |
| `Control_LogViewer` — date preset (Today / Last 7 Days default / Last 30 / Custom), 4 severity checkboxes, group-by (None / Error Type / Source / Hour / Day), search text with 500 ms debounce, optional regex; detail pane with Copy; CSV/JSON/TXT export. The DAO accepts MaxResults (default 100) and Skip, but the UI never sets them, so there is NO paging and a hard 100-row cap | `View_DeveloperLogsPage` | Scaffolded | Reproduce the 100-row/no-paging cap exactly, or decide to expose paging (product decision, not implementation) |
| `Control_FeedbackManager` — feedback grid with status/type/date filters, bulk status buttons, inline priority edit | `View_DeveloperFeedbackPage` | Scaffolded | Wire `Dao_UserFeedback` and the status vocabulary New/Open/In Progress/Resolved/Closed |
| `Control_SystemInfo` — OS/.NET/machine/user/version (static) + memory/threads/handles/uptime + DB stats + Test Connection latency. Uses a 5-second `System.Threading.Timer` for process metrics; CPU is hard-coded 0 | `View_DeveloperSystemInfoPage` section | Scaffolded | Reproduce or fix the hard-coded CPU 0 |
| `Control_DatabaseHealth` — connection status, active connections, per-table row/size grid. Sources `md_system_GetTableSizes` and `md_devtools_GetDatabaseStats` | `View_DeveloperSystemInfoPage` section | Scaffolded | Wire the two source procedures |
| `Form_SystemHealth` — health banner + the CURRENT USER's own feedback list with Prev/Next + Submit Feedback button; 30-second WinForms Timer | `View_DeveloperDashboardPage` current-user feedback strip | Scaffolded | Wire current-user feedback list and Submit navigation |
| `Form_SubmitFeedback` — bug/suggestion/question form with category (UI/UX, Performance, Data Accuracy, …) and severity Low..Critical; writes `md_feedback_Insert` and `sys_tracking_number_GetNext` | `View_SubmitFeedbackPage` | Scaffolded | Wire `Dao_UserFeedback`, `Dao_UserFeedbackComments`, tracking-number acquisition |
| `EnhancedErrorDialog` — the end-user error dialog; on Load it calls `SendQuietErrorReportAsync` → `Dao_ErrorReports.InsertReportAsync` (`sp_error_reports_Insert`) on EVERY display; has "View Logs" (opens `DeveloperToolsForm.SelectLogsTab`) and a deprecated "Report Issue" that only shows a message box | Owned by `003`; this spec owns only the "View Logs" navigation target | Scaffolded | Preserve the open-`SelectLogsTab` deep link into the Logs tab |
| `Form_DatabaseMaintenance` — migration/backup/reset console. Bulk migration from `Database\Migration` scripts 01/02/03 which OVERWRITE target data; single-table migration doing TRUNCATE then reload for md_part_ids, md_locations, md_operation_numbers, md_item_types, usr_users, inv_inventory, inv_transaction; clean-test-data DELETE where user is JOHNK or test; OPTIMIZE tables; truncate log_error; archive+truncate logs; and a FACTORY RESET behind three sequential confirmations that TRUNCATEs inv_inventory, inv_transaction and sys_last_10_transactions. Highest-risk surface in the application | `View_DatabaseMaintenancePage` | Scaffolded (inert) | A dedicated safety review before any port; possibly never |
| `Control_ConnectionStrengthControl` — 5-bar custom paint with ping tooltip; host pushes Strength and Ping | Owned by `003` diagnostics; not reproduced here | N/A | n/a — delivered by `003` |

### 2.2 Supporting services — documented, not wired

| Service | Legacy behaviour | Owner spec | v1 status here |
| :--- | :--- | :--- | :--- |
| `Service_FeedbackManager.SubmitFeedbackAsync` | Resolves UserID from username, validates, HTML-sanitizes text fields, inserts via `md_feedback_Insert` which returns OUT FeedbackID and TrackingNumber; if FeedbackType = "Bug Report" and Severity is Critical or High it calls `Service_EmailNotification.SendNotification` | `009` | Not wired |
| `Service_EmailNotification` | Fire-and-forget `Task.Run`; recipients from `sys_email_notification_GetRecipients` (category, fallback "All"); 3 attempts with 5/10/15-minute backoff; SMTP is SIMULATED — log lines only, sends nothing | `009` (Q7 in `00-index`) | Not wired |
| `Service_ErrorReportQueue` / `Service_ErrorReportSync` | Writes `ErrorReport_{yyyyMMdd_HHmmss}_{user}_{guid}.sql` files into `%APPDATA%\MTM_Application\ErrorReports\Pending` as an offline fallback; re-submits pending files at startup under `SemaphoreSlim(1,1)`. Statuses live in `error_reports.Status` (New/Reviewed/Resolved) | `009` | Not wired |
| `sys_windowform_mapping` (`sys_windowform_mapping_GetAll` / `_Upsert`) and `sys_usercontrol_mapping` (`_GetByWindow` / `_Upsert`) | Seeded in the database; consumed by the help system to translate codebase control names into user-friendly labels. `UpsertAsync` has no caller; no developer UI produces them in this scope | `002` (DAOs), `009` (consumer) | Not wired; no UI |
| `sys_parameter_prefix_overrides_*` | Loaded at startup into the prefix cache used by the data helper; the developer-tools editing surface exists only as DAO methods | `002` | Not wired; no UI |
| `log_error` via `Dao_ErrorLog.LogErrorToDatabaseAsync` → `log_error_Add_Error` | Has NO caller in the shipped code; live errors go to CSV plus `error_reports` | `002` (DAO), `003` (Serilog replaces CSV) | Not wired |

---

## 3. Behavioral Edge Cases & State Constraints

### 3.1 The scaffold contract

- Every page in this module is reachable only through shell navigation; no page constructs a dialog, a `Window`, or a DAO.
- Every page renders a single unavailable-state surface: a heading naming the destination, one sentence stating the screen is not available in this release, and no actionable control. A screen cannot be half-scaffolded.
- The four Developer Tools destinations (Dashboard, Logs, Feedback, System Info) exist as navigable tabs even though each is inert, so the shell's navigation shape matches the WinForms `DeveloperToolsForm` tab set. The Database Maintenance page is a separate shell destination.

### 3.2 What must not happen in v1

- No ViewModel injects any `IDao_*`. The DAOs from `002` exist and are registered in DI, but no code path in this module resolves them.
- No timer of any kind: the 30-second Logs refresh, the 5-second process-metrics timer, and the 30-second `Form_SystemHealth` timer are all absent.
- No keyboard-shortcut handler: Ctrl+1..4, Ctrl+F, Ctrl+R/F5, and Esc are documented, not implemented.
- No destructive path: the maintenance page exposes no control that could invoke a migration, TRUNCATE, DELETE, OPTIMIZE, backup, or factory reset. Enforced by the grep gate in TASK-128.
- No error path: this module never calls `IErrorService` or `IDialogService`, because it has no operation that can fail.

### 3.3 State introduced

None. This module writes no user data, opens no log file, reads no table, and persists nothing. It adds no `Models/`, `Enums/`, or `Contracts/` content in v1.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **Navigation registration contract.** | **Resolved** — `004` registers routes through `INavigationService.NavigateTo<TViewModel>()` over a route table keyed by ViewModel type → Page type, populated from each module's own `AddXxxServices`. TASK-127 follows that shape. |
| 2 | **`011` publish contract and task range.** | **Resolved** — `011` is written and owns TASK-130 … TASK-137. Re-read it during TASK-129 and record the outcome. |
| 3 | Whether any Developer Tools behaviour is wanted at all, given `Form_DatabaseMaintenance` is the highest-risk surface in the application. | Product decision, post-v1 |
| 4 | The hard-coded CPU value of 0 in `Control_SystemInfo`, and the LogViewer 100-row/no-paging cap: preserve or fix when the real spec is written. | Undecided; documented so it is not silently "fixed" |
| 5 | Whether the deprecated "Report Issue" message box in `EnhancedErrorDialog` is carried forward. | Assumed dropped; `003` owns the dialog |
| 6 | Whether real SMTP is ever needed (the existing no-op stands). | Open Q7 in [`../00-index.md`](../00-index.md); not this module's concern in v1 |

### Downstream assumptions (validated by TASK-129)

- **A1.** `011-deployment-and-cutover` exists as a spec/plan/tasks triplet and is the sole owner of publishing and cutover; this module makes no publish claim.
- **A2.** The `004` shell exposes a module page-registration contract this module uses; the exact API is **(to confirm)** until `004` is written.
- **A3.** The unpackaged, untrimmed publish (`PublishTrimmed=false`) ships `MTM_WIP.Module_DeveloperTools` and its scaffold pages; nothing is linker-removed.
- **A4.** No downstream spec adds behaviour to `MTM_WIP.Module_DeveloperTools` in v1; decision D15 ("everything except Developer Tools" scaffolded) holds.
- **A5.** The database remains frozen; no spec introduces a table, procedure, or status-contract change for developer tools.
- **A6.** `009-help-and-feedback` owns feedback, error reporting, and the help system end-to-end; this module does not duplicate them.
