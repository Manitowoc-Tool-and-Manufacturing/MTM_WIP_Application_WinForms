# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `008-visual-analytics`
**Depends on**: `001-foundation-and-tooling`, `002-data-access-layer`, `003-crosscutting-services-and-theming`, `004-shell-and-startup`
**Unblocks**: none — `010` and `011` are scaffolds and read nothing from this module
**Tasks**: TASK-109 … TASK-119
**Governing rules**: [`../constitution.md`](../constitution.md) §1.1, §2 Rules 4, 5, 6, 10, 11

---

## 1. Executive Summary & Scope

The Visual & Analytics module is the only part of the application that reaches a second database. Everything else reads MySQL 5.7.24 through stored procedures; these seven surfaces read the Infor Visual ERP (SQL Server) through `MTM_WIP.Data/Visual/Service_VisualDatabase.cs`, the one sanctioned exception to "stored procedures only" carried forward by spec `002`. This spec ports the *presentation* of those surfaces from WinForms + WebView2 HTML to WinUI 3 + LiveCharts, and changes nothing about the ERP queries themselves.

Two legacy rendering mechanisms are deleted outright:

1. The Chart.js dashboards. `Resources\Html\ReceivingAnalytics_Enhanced.html` loads Chart.js from `https://cdn.jsdelivr.net/npm/chart.js` — a **network call** and a defect on an offline shop floor.
2. The WebView2 analytics viewer, which on every open rewrites `Resources\Html\VisualUserAnalytics_Enhanced.html` into `%TEMP%\MTM_Analytics_{guid}.html` and navigates to it.

Both are replaced by native `CartesianChart` surfaces over the same data. **No HTML, no Chart.js, no WebView2 and no temp file survive anywhere in this module.**

### In scope

- The dashboard shell with five categories and an explicit empty state.
- Visual Inventory, Inventory Audit, Receiving Analytics, Die/Tool Discovery, PO Details, and the User Analytics launcher + viewer.
- The chart stack (`LiveChartsCore.SkiaSharpView.WinUI` 2.0.5 + `Microsoft.Graphics.Win2D` 1.4.0) and the shared chart builder (date-index axis strategy, series switch, paints).
- The module orchestration service wrapping `Service_VisualDatabase` (ERP, read-only) and `IDao_VisualAnalytics` (`sys_visual` shift/name data).
- Excel export for the two surfaces that already have it (Inventory, Audit).

### Out of scope

- Any change to an ERP query string, the `md_sys_visual_*` procedures, or either database. Both are frozen by `002`.
- **Shipping.** It is not implemented in the WinForms application and stays visibly unavailable.
- Developer Tools (`010`) and deployment (`011`).
- The help system (`009`). The legacy "?" help button is removed here and not replaced; help navigation is `009`'s concern.

### Success condition

All seven surfaces open from the dashboard, render grids and charts natively with **no network access and no temp-file write**, ERP failures surface through `IErrorService` exactly as WIP failures do, and `dotnet build` + `dotnet test` are clean with zero hits for the forbidden-surface gate (TASK-112).

---

## 2. Legacy Feature Assessment & UI Mapping Table

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `Form_InforVisualDashboard` — shell with a nav panel + content panel hosting 5 UserControls; category buttons call `LoadCategoryDataAsync` → `IService_VisualDatabase.GetDashboardDataAsync(category, token)`; explicit empty-state control | `View_VisualDashboard` — `NavigationView` nav + a content host; one `AsyncRelayCommand` per category | Five categories: Inventory, Inventory Auditing, Receiving, Shipping, Die/Tool Discovery. **Shipping is not implemented** (guarded, returns "not yet implemented") and must remain *visibly unavailable*, never silently empty. The empty state is an explicit message ("no data"), never a blank pane. |
| `Form_AnalyticsViewer` — role-aware "My Analytics"/Team workspace rendered as HTML in `WebView2` (`Microsoft.Web.WebView2.WinForms`); reads `Resources\Html\VisualUserAnalytics_Enhanced.html`, replaces `window.__ANALYTICS_DATA__ = null;` with camelCase JSON, writes `%TEMP%\MTM_Analytics_{guid}.html`, navigates `Source`; data from `Service_Analytics.GetAnalyticsSnapshotAsync()` (365-day window, 30-day default) | `View_UserAnalytics` — native page with `CartesianChart` surfaces bound to `ISeries` / `XAxes` / `YAxes` on the ViewModel | Same snapshot data, same 365-day window with a 30-day default range. Role-aware section header (My Analytics vs Team). **No WebView2, no HTML template, no temp file.** |
| `Control_VisualUserAnalytics` — launcher card only; one button opens `Form_AnalyticsViewer` modally; text varies by admin/developer role | Launcher card inside `View_VisualDashboard`; a `RelayCommand` navigates to `View_UserAnalytics` | Same role-gated text. The card is a navigation surface, not a separate modal window. |
| `Control_VisualInventory` — filters PartNumber / Warehouse / Location + `CheckBox_NonZeroOnly`; `DataGridView` bound to a cached `DataTable` | `View_VisualInventory` — filter row + the shared grid over typed rows + a "non-zero only" toggle | Same three filters and the toggle. Results are the ERP query output. Export to Excel via the shared ClosedXML exporter, filename `VisualInventory_{yyyyMMdd_HHmmss}.xlsx`. |
| `Control_InventoryAudit` — `SuggestionTextBox_LifecyclePart` + Start/End `DateTimePicker`s + Export; `DataGridView` with in-memory re-filtering; double-click drills down | `View_InventoryAudit` — part suggestion + date range + shared grid; row activation drills into the lifecycle view | Part suggestion plus a date range. Row activation opens the lifecycle view for that part/batch. Export `VisualAudit_{yyyyMMdd_HHmmss}.xlsx`. In-memory re-filtering is replaced by the ViewModel re-querying/paging the filtered set. |
| `Control_ReceivingAnalytics` — `DataGridView` schedule + Chart.js HTML analytics; PO# and Part# suggestions, Start/End pickers, week navigation Mon→Fri (`SetCurrentWeek`/`SetPreviousWeek`/`SetNextWeek`), **nine** checkboxes ShowClosed/ShowOutsideService/ShowWithPartID/ShowLate/ShowPartial/ShowOnTime/ShowOpen/ShowMMC/ShowMMF; collapsible filter sections with ToolTip headers; double-click opens `Form_PODetails`; Analytics button builds HTML and opens `Form_HtmlViewer` from a temp file | `View_ReceivingAnalytics` — schedule grid + native `CartesianChart` views; PO/part suggestions, week navigation, the same nine toggles; static (non-animated) filter sections; row activation opens `Dialog_PoDetails` | Schedule grid for a Monday–Friday week with week navigation, part/PO filters and nine status toggles. Analytics is a chart of the same window plus a forecast (1 year history + 6 months forecast; +180 days default). **Chart.js CDN dependency removed. No temp file.** |
| `Control_DieToolDiscovery` — about 13 suggestion boxes (Search Term, Coil/Flatstock, Thickness, Width, Length, Ga., Where Used, Progression, Customer, Scrap Location, Generic Type, Detailed Type, Auto-Issue Location) over three query modes `SearchDiesAsync` / `GetCoilFlatstockInfoAsync` / `GetWhereUsedAsync` | `View_DieToolDiscovery` — 13 suggestion fields grouped by query mode + the shared grid | Three query modes preserved exactly; each returns a grid. **No charts.** |
| `Form_PODetails` — hand-built Labels + read-only TextBoxes (NOT a grid): LineNo, PartNumber, Status, Ordered, Received, Remaining, DesiredDate, PromiseDate, UnitPrice, TotalAmount, Specs; prev/next over `_currentIndex` | `Dialog_PoDetails` — property-list dialog (labels + read-only text) with prev/next commands | The same fields, and previous/next moves through the current PO's lines without closing and **with no data change possible** (the dialog edits nothing). |
| `Resources\Html\ReceivingAnalytics_Enhanced.html` — Chart.js from `https://cdn.jsdelivr.net/npm/chart.js` (a network call) | *(deleted)* — replaced by LiveCharts | The CDN dependency is a defect for an offline shop floor. Nothing in the new module may fetch anything over the network. |
| `Resources\Html\VisualUserAnalytics_Enhanced.html` — renders from `window.__ANALYTICS_DATA__` via DOM/SVG/table | *(deleted)* — replaced by LiveCharts over a snapshot model | Same data, native rendering. |
| `Resources\Sql\Visual\{DieToolDiscovery,Inventory,InventoryAuditing,Receiving,Shipping}.sql` — single-line placeholders (`SELECT 'Inventory Placeholder' AS Status`) | Not ported | Dead files. The real Visual SQL is inline C# inside `Service_VisualDatabase.cs` (spec `002`). These `.sql` files are not functional and must not be described as such. |
| `Service_VisualDatabase` — 41 inline `SELECT`s via `Microsoft.Data.SqlClient`; connection from `AppSettings["VisualServer"] ?? "VISUALSQL"`, `["VisualDatabase"] ?? "VMFG"`, `UserID`/`Password` from `Model_Application_Variables.VisualUserName`/`VisualPassword` ?? `AppSettings`; `ConnectTimeout 5`, `ApplicationName "MTM_WIP_App_VisualDashboard"`, `TrustServerCertificate true`, `Pooling true`; tables PART, PURC_ORDER, POLINE, RECEIVED_LINE, WORK_ORDER, WAREHOUSE/LOCATION | Unchanged in `MTM_WIP.Data/Visual/` (spec `002` TASK-031); connection from `DatabaseOptions.Visual`; every method `async Task<Model_Result<T>>` | The module consumes this through an orchestration service and **never constructs a connection string or a query string**. Read-only: no `INSERT`/`UPDATE`/`DELETE`/`DROP`/`ALTER`/`TRUNCATE`/`CREATE`/`MERGE` against Visual, ever. |
| `Dao_VisualAnalytics` — `md_sys_visual_Get(OUT p_Status, OUT p_ErrorMsg)` → `id, json_shift_data, json_user_fullnames, last_updated` (`ORDER BY id DESC LIMIT 1`); `md_sys_visual_Update(IN p_JsonShifts JSON, IN p_JsonNames JSON, ...)` | `IDao_VisualAnalytics` in `MTM_WIP.Data` (spec `002` TASK-030) | Shift data and user full names feed the User Analytics surface. The update path is read by no ViewModel in this module (all seven surfaces are read-only); whether it is needed at all is `(to confirm)` in §4. |

---

## 3. Behavioral Edge Cases & State Constraints

### 3.1 The second database is read-only and frozen

- The module never opens a `SqlConnection`, never builds a `SqlCommand`, and never writes a query string. It calls the orchestration service, which calls `Service_VisualDatabase` (ERP) and `IDao_VisualAnalytics` (main DB).
- A failed ERP call returns `Model_Result<T>.Failure` with `StatusCode < 0` and flows through the **same** `IErrorService` path as a WIP failure. The module does not special-case "ERP vs WIP" when presenting an error.
- If the ERP server is unreachable, the surface shows the explicit empty/error state via `IErrorService` — never a crash and never a blank pane.

### 3.2 Credentials

- ERP credentials come from **configuration** (`DatabaseOptions.Visual` per spec `002` §3.1), never from a static field and never from source.
- **No Visual password literal may exist anywhere in the repository** — not in code, not in comments, not in test fixtures, not in configuration samples committed to the repo. A committed credential is a release blocker (constitution §2 Rule 4).
- The connection string is never logged and never printed in an error dialog.

### 3.3 Offline, no-network, no-temp-file

- The module makes **zero network requests**. The legacy CDN fetch is the motivating defect; the gate in TASK-112 asserts no `http://`/`https://` reachability call and no `WebView2` anywhere in the module.
- The module writes **no temp file**. The legacy `%TEMP%\MTM_Analytics_{guid}.html` write is gone. The only file output is user-initiated Excel export through `SaveFileDialog` (`VisualAudit_*`, `VisualInventory_*`) — that is a user-chosen destination, not a temp write.
- `Microsoft.Web.WebView2` is not referenced by the module.

### 3.4 Dates, axes and chart data

- Every date on a chart axis is projected to an **integer index** over a `SortedSet<DateTime>`; the axis carries `Labels = labels.Select(ToShortDateString)`, `LabelsRotation = 0`, `TextSize = 12`, `MinStep = 1`, `UnitWidth = 1`. The Y axis uses `Labeler = value => ((int)value).ToString()`. This is a fixed strategy, not a per-surface choice (see plan §2.2).
- Data windows are frozen: User Analytics 365-day window with 30-day default; Receiving Analytics fetches 1 year of history + 6 months of forecast; forecast default +180 days; transaction lookups are limited to 180 days and 250 rows; reference lookups are capped at 500.

### 3.5 Cancellation and threading

- The legacy pattern — a per-control `CancellationTokenSource` field used from `async void` handlers — is **not ported**. Each load is an `AsyncRelayCommand` with its own cancellation; a new load cancels the in-flight load.
- No `async void` outside the generated command plumbing; no `.Result`/`.Wait()`/`GetAwaiter().GetResult()`; UI-thread hops go through `IDispatcherService` only.

### 3.6 Legacy problems fixed, stated explicitly

| Legacy defect | Fix in this spec |
| :--- | :--- |
| Per-control `CancellationTokenSource` + `async void` load handlers | `AsyncRelayCommand` with command-managed cancellation |
| `Model_Application_Variables.VisualUserName`/`VisualPassword` statics consumed by the service | `DatabaseOptions.Visual` from configuration (§3.2) |
| `Service_VisualDatabase._useSampleData` sample-data fallback generators | Not ported; a failure or empty result produces the explicit empty state |
| `Program.ServiceProvider.GetService<IService_VisualDatabase>()` service-location | Constructor injection of the orchestration service into ViewModels |
| `Form_HtmlViewer` / temp-file HTML writes | Deleted; native `CartesianChart` |
| A code-created "?" help button anchored top-right | Removed; help is `009`'s native page |
| Animated collapsible filter sections | Static, non-animated filter sections (WinUI `Expander` without animation) |
| `DataTable`-bound `DataGridView`s | Typed row models projected from the ERP result; the shared grid from `003` |

### 3.7 State eliminated by this spec

| Legacy static / state | Replacement |
| :--- | :--- |
| `Model_Application_Variables.VisualUserName`, `VisualPassword` | `DatabaseOptions.Visual` (read once, injected) |
| Per-control `CancellationTokenSource` fields | `AsyncRelayCommand` cancellation |
| `Service_VisualDatabase._useSampleData` flag and generators | *(deleted)* — empty state |
| The `DataTable` caches held by `Control_VisualInventory` and `Control_InventoryAudit` | ViewModel-owned typed collections, re-queried on filter change |
| `Program.ServiceProvider` lookups inside the module | Constructor injection |

### 3.8 Persistence touched

None beyond user-initiated export. This spec creates no application-state folder, writes no settings key, and changes no row in either database. The `usr_settings` Visual-related keys that the WinForms user record carried are left where they are; the new module does not read them unless §4 item 1 is confirmed.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **Whether a per-user Visual identity is still required.** Legacy stores `VisualUserName`/`VisualPassword` in user settings and loads them into statics. The new connection comes from `DatabaseOptions.Visual`; whether a per-user override must survive is unresolved. | `(to confirm)`. If yes, it is supplied by `ICurrentUserService`/`IUserPreferences` from the persisted key, never a static. |
| 2 | **Exact legacy method names for the Inventory / Audit / Receiving queries** on `Service_VisualDatabase` (the dashboard, analytics-snapshot and die-discovery names are known: `GetDashboardDataAsync`, `GetAnalyticsSnapshotAsync`, `SearchDiesAsync`, `GetCoilFlatstockInfoAsync`, `GetWhereUsedAsync`). | `(to confirm)` — irrelevant to correctness: spec `002` ports all 41 query strings verbatim and the orchestration service maps onto whatever they are named. |
| 3 | **Which surface consumes `sys_visual` shift data / user full names** and whether `md_sys_visual_Update` has any caller in the new application. | `(to confirm)`. The DAO is ported (TASK-030); this module treats it as read-only. |
| 4 | **"Team" workspace in User Analytics** | **Resolved (D23)** — v1 keeps both My Analytics and the Team view; the role-gated header and both scopes are in scope. |
| 5 | **Which ERP query produces the 6-month receiving forecast.** The window (+180 days) is known; the producing query is internal to `Service_VisualDatabase`. | `(to confirm)`. The module consumes whatever `Model_Result<T>` the ported method returns. |
| 6 | **LiveCharts 2.0.5 + Win2D 1.4.0 rendering under unpackaged `win-x64`.** | Proven at TASK-109 with a throwaway chart in the module; if SkiaSharp fails to resolve a native asset in the unpackaged publish, the fallback is a self-contained publish with `IncludeNativeLibrariesForSelfExtract` — recorded, not silently chosen. |

### Assumptions

1. The Visual ERP server is reachable from a developer machine or VPN. If not, ERP integration tests are skipped with the reason `Visual ERP unreachable from build agent` (same convention as `002` TASK-031).
2. The shared grid component from `003` accepts typed row models and the per-user column-state payload shape is frozen.
3. `004` exposes a navigation route and a composition-root hook for `MTM_WIP.Module_Visual` registration (the `LiveCharts.Configure` call site).
4. No surface in this module needs a document-style secondary `Window`; `Dialog_PoDetails` is a windowed content dialog via `IDialogService`.
