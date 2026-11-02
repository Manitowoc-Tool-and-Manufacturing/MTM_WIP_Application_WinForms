# Migration Task List (Execution Plan)

**Spec**: `008-visual-analytics`
**Range**: TASK-109 … TASK-119
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Ordering is load-bearing.** The chart builder (TASK-110) and the orchestration service (TASK-111) are proven before any surface consumes them. A surface built against an unproven builder or an unmocked data path produces failures that look like data bugs and are not.

---

## Phase 1 — Chart stack and shared builder

- [ ] **TASK-109** — Add `LiveChartsCore.SkiaSharpView.WinUI` 2.0.5 and `Microsoft.Graphics.Win2D` 1.4.0 to `MTM_WIP.Module_Visual`. Add `VisualModuleRegistration.Configure()` that calls `LiveCharts.Configure(s => s.AddSkiaSharp())` exactly once, and wire it from the composition root hook in `004`. Add a throwaway `CartesianChart` with `xmlns:lvc="using:LiveChartsCore.SkiaSharpView.WinUI"` on a scratch page to prove rendering under unpackaged `win-x64`.
  - **Verify**: `dotnet list MTM_WIP.Module_Visual package` shows both pinned versions; `git grep -n "AddSkiaSharp"` returns exactly one call site; the scratch chart renders with no SkiaSharp native-asset error; the scratch page is deleted before merge.

- [ ] **TASK-110** — Implement `Helper_LiveCharts` in `MTM_WIP.Module_Visual/Helpers/`: date → integer-index projection over a `SortedSet<DateTime>`; the `Axis` factory (`Labels = ToShortDateString`, `LabelsRotation = 0`, `TextSize = 12`, `MinStep = 1`, `UnitWidth = 1`); the Y-axis `Labeler = value => ((int)value).ToString()`; the paint factory (`new SolidColorPaint(colour) { StrokeThickness = 2 }`, `Fill = null` for lines); and the series switch (`ColumnSeries<T>`, `StackedColumnSeries<T>`, `LineSeries<T>`). Colours resolve from `ThemeResource` brushes, never literal `#RRGGBB`.
  - **Verify**: unit tests assert non-contiguous dates map to dense indices, the axis labels/`MinStep`/`UnitWidth`/`Labeler` values, that a line series has `Fill = null` and stroke thickness 2, and that the switch emits the correct series type for each of the three chart types.

---

## Phase 2 — Data path and forbidden-surface gate

- [ ] **TASK-111** — Implement `IService_VisualAnalytics` / `Service_VisualAnalytics` and the typed row models (`Model_VisualInventoryRow`, `Model_InventoryAuditRow`, `Model_ReceivingScheduleRow`, `Model_ReceivingAnalytics`, `Model_DieToolDiscoveryRow`, `Model_PoLine`, `Model_UserAnalyticsSnapshot`) in `MTM_WIP.Module_Visual`. Map each tool in [`plan.md`](./plan.md) §2.3 onto `Service_VisualDatabase` (ERP) and `IDao_VisualAnalytics` (`sys_visual`), projecting any `DataTable` result to typed rows and returning `Model_Result<T>` unchanged.
  - **Verify**: unit tests with mocked data contracts assert each of the seven orchestration methods dispatches to the correct data source and projects rows without opening a connection; `git grep` finds no `SqlConnection`, `SqlCommand` or `SELECT` in `MTM_WIP.Module_Visual`.

- [ ] **TASK-112** — Add the forbidden-surface gate: a test walking `MTM_WIP.Module_Visual/**` (`Views`, `ViewModels`, `Services`, `Helpers`, `Models`, `Converters`, `DependencyInjection`) and asserting **zero** hits for `WebView2`/`Microsoft.Web.WebView2`, `http://`, `https://`, `cdn.jsdelivr`, `Chart.js`, `Path.GetTempPath`, `%TEMP%`, and `.html` file writes. Add a second scan asserting **no Visual password literal** (including the legacy values) and no `Password = "…"` in any committed file of the solution.
  - **Verify**: temporarily adding `using Microsoft.Web.WebView2;` to a module file fails the test naming the file and line; the same for a temp-file write. Removing it passes. `git grep` for the legacy credential values across the repository returns zero.

---

## Phase 3 — Surfaces

- [ ] **TASK-113** — Implement `View_VisualDashboard` + `ViewModel_VisualDashboard`: `NavigationView` with the five categories of `Enum_VisualCategory` (Inventory, Inventory Auditing, Receiving, Shipping, Die/Tool Discovery) and a content host; an explicit empty-state; Shipping guarded and visibly "not yet implemented" (never silently empty); and the User Analytics launcher card with role-gated text via `ICurrentUserService`.
  - **Verify**: ViewModel tests cover category selection (host swaps, no data → empty state), the Shipping guard, and the launcher text for admin/developer vs normal roles. Manual smoke test confirms the empty state renders as a message, not a blank pane.

- [ ] **TASK-114** — Implement `View_UserAnalytics` + `ViewModel_UserAnalytics`: native `CartesianChart` surfaces over `GetAnalyticsSnapshotAsync()` (365-day window, 30-day default) using `Helper_LiveCharts` — a `LineSeries<int>` daily trend and a `StackedColumnSeries<int>` type composition — plus the role-aware "My Analytics"/Team header.
  - **Verify**: ViewModel tests cover snapshot success (series built), failure (via `IErrorService`), the 30-day default range, and cancellation of an in-flight load. `git grep` finds no `__ANALYTICS_DATA__`, no `.html`, and no `WebView2` in the module.

- [ ] **TASK-115** — Implement `View_VisualInventory` + `ViewModel_VisualInventory`: PartNumber / Warehouse / Location filters, a "non-zero only" toggle, the shared grid over `Model_VisualInventoryRow`, and Excel export via `SaveFileDialog` with `VisualInventory_{yyyyMMdd_HHmmss}.xlsx`.
  - **Verify**: ViewModel tests cover filter → re-query, the non-zero-only toggle, empty result state, export success and export cancellation. Manual smoke test confirms a non-zero-only result never includes a zero row.

- [ ] **TASK-116** — Implement `View_InventoryAudit` + `ViewModel_InventoryAudit`: lifecycle-part suggestion, Start/End date range, the shared grid over `Model_InventoryAuditRow`, row activation that drills into the lifecycle view for that part/batch, and export `VisualAudit_{yyyyMMdd_HHmmss}.xlsx`.
  - **Verify**: ViewModel tests cover date-range validation (start ≤ end), drill-down navigation on row activation, empty result state, and export. Manual smoke test confirms double-activating a row opens the lifecycle view for the correct part.

- [ ] **TASK-117** — Implement `View_ReceivingAnalytics` + `ViewModel_ReceivingAnalytics` + `Dialog_PoDetails` + `ViewModel_PoDetails`: schedule grid over `Model_ReceivingScheduleRow`; PO#/Part# suggestions; week navigation Monday–Friday (`SetCurrentWeek`/`SetPreviousWeek`/`SetNextWeek`); the **nine** status toggles (ShowClosed/ShowOutsideService/ShowWithPartID/ShowLate/ShowPartial/ShowOnTime/ShowOpen/ShowMMC/ShowMMF); static (non-animated) filter sections; and the three native charts per [`plan.md`](./plan.md) §2.4 — per-day schedule `ColumnSeries<int>`, on-time/partial/late `StackedColumnSeries<int>`, forecast `LineSeries<double>` over +180 days. Row activation opens `Dialog_PoDetails` with prev/next over the PO's lines and no data change.
  - **Verify**: ViewModel tests cover week navigation (Mon→Fri bounds), each of the nine toggles toggling its filter, the three chart types built from one window, forecast default +180 days, PO drill-down with prev/next and a read-only model. `git grep` finds no `chart.js`, no `cdn.jsdelivr` and no `Form_HtmlViewer` in the module.

- [ ] **TASK-118** — Implement `View_DieToolDiscovery` + `ViewModel_DieToolDiscovery`: the 13 suggestion fields (Search Term, Coil/Flatstock, Thickness, Width, Length, Ga., Where Used, Progression, Customer, Scrap Location, Generic Type, Detailed Type, Auto-Issue Location) grouped over the three query modes `SearchDiesAsync` / `GetCoilFlatstockInfoAsync` / `GetWhereUsedAsync`, each returning a grid of `Model_DieToolDiscoveryRow`; no charts.
  - **Verify**: ViewModel tests assert each mode dispatches to its own orchestration method, a mode switch clears the previous result grid, and an empty result renders the explicit empty state. Manual smoke test confirms the three modes are selectable and produce distinct grids.

---

## Phase 4 — Downstream consistency validation

- [ ] **TASK-119** — **Downstream validation: `009-help-and-feedback` (and a sweep of `010`–`011`).** Re-read [`../009-help-and-feedback/spec.md`](../009-help-and-feedback/spec.md), [`plan.md`](../009-help-and-feedback/plan.md) and [`tasks.md`](../009-help-and-feedback/tasks.md), then sweep `010-developer-tools-scaffold` and `011-deployment-and-cutover` for anything that assumed a chart surface, a WebView2 host, or a temp-file analytics flow that this spec has deleted.

  List **5–7 concrete assumptions** this spec makes about `009` and the downstream specs, and append each to the **Drift log** below as a *(pending)* row. Specifically confirm at least:

  1. `009` does not reuse the deleted `Form_HtmlViewer` or the `%TEMP%` HTML analytics flow — help is a native page, not a WebView2 over a temp file.
  2. `009` owns the only remaining WebView2 decision (if any); this module references no WebView2.
  3. The "?" help button removed by this spec is re-homed by `009`, not re-created by any chart surface.
  4. `011`'s publish includes `MTM_WIP.Module_Visual` and its SkiaSharp/Win2D native assets in the unpackaged `win-x64` payload.
  5. `010` does not scaffold a dashboard or analytics surface that overlaps this module's screens.
  6. No downstream spec re-introduces the Chart.js CDN or a network dependency for charts.

  If any item is false, fix **this** spec before proceeding. Do not amend `009` to work around a defect here.

  - **Verify**: a written entry exists in the Drift log for each assumption, each marked *(pending)* until `009` exists, and `dotnet test MTM_WIP.Tests` is green.

---

## Drift log

Filled in by TASK-119. One row per downstream assumption checked.

| Checked spec | Assumption | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `009` | Help is a native page; no reuse of `Form_HtmlViewer` or the `%TEMP%` HTML flow | *(pending)* | — |
| `009` | `009` owns the only remaining WebView2 decision; this module references none | *(pending)* | — |
| `009` | The removed "?" help button is re-homed by `009`, not re-created by a chart surface | *(pending)* | — |
| `011` | Publish includes `MTM_WIP.Module_Visual` + SkiaSharp/Win2D native assets in `win-x64` | *(pending)* | — |
| `010` | Developer Tools scaffold does not overlap this module's dashboard/analytics screens | *(pending)* | — |
| `009`, `010`, `011` | No downstream spec re-introduces Chart.js CDN or a chart network dependency | *(pending)* | — |

## Known deviations from the WinForms behaviour

Recorded so a reviewer does not mistake them for defects.

| Deviation | Rationale | Reviewed by |
| :--- | :--- | :--- |
| Chart.js / WebView2 HTML dashboards are **deleted**, not ported | Constitution §1.3 (HTML surfaces become native charts); the CDN fetch is an offline-shop-floor defect | *(this spec)* |
| `%TEMP%` HTML analytics files are **not written** | Forbidden surface; user-chosen Excel export is the only file output | *(this spec)* |
| `_useSampleData` sample-data fallback is **not ported** | Fake data on a production shop floor is a defect; empty state replaces it | *(this spec)* |
| Animated collapsible filter sections become **static** | WinUI `Expander` without the legacy animation; no functional loss | *(this spec)* |
| Per-user Visual credentials held in statics are **not ported** | Credentials come from `DatabaseOptions.Visual`; no credential literal in the repo | *(this spec)* |
| Shipping remains **visibly unavailable** | It is not implemented in the WinForms app; parity means keeping it unavailable, not inventing it | *(this spec)* |
