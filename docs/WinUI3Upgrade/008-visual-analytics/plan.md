# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `008-visual-analytics`
**Tasks**: TASK-109 … TASK-119

---

## 1. Project Topology & Directory Mapping

All of this spec lands in **`MTM_WIP.Module_Visual`**. The ERP reader (`Service_VisualDatabase`) and `IDao_VisualAnalytics` already live in `MTM_WIP.Data` (spec `002`); this module references them and adds no data-access code.

```text
MTM_WIP.Module_Visual/
├── Views/
│   ├── View_VisualDashboard.xaml(.cs)      # NavigationView + content host + launcher card
│   ├── View_UserAnalytics.xaml(.cs)        # native chart surface over the snapshot
│   ├── View_VisualInventory.xaml(.cs)
│   ├── View_InventoryAudit.xaml(.cs)
│   ├── View_ReceivingAnalytics.xaml(.cs)
│   ├── View_DieToolDiscovery.xaml(.cs)
│   └── Dialog_PoDetails.xaml(.cs)          # property-list dialog, prev/next
├── ViewModels/
│   ├── ViewModel_VisualDashboard.cs
│   ├── ViewModel_UserAnalytics.cs
│   ├── ViewModel_VisualInventory.cs
│   ├── ViewModel_InventoryAudit.cs
│   ├── ViewModel_ReceivingAnalytics.cs
│   ├── ViewModel_DieToolDiscovery.cs
│   └── ViewModel_PoDetails.cs
├── Services/
│   └── Service_VisualAnalytics.cs          # orchestration: maps per-tool calls to Data
├── Contracts/
│   └── IService_VisualAnalytics.cs
├── Models/
│   ├── Model_VisualInventoryRow.cs
│   ├── Model_InventoryAuditRow.cs
│   ├── Model_ReceivingScheduleRow.cs
│   ├── Model_ReceivingAnalytics.cs         # schedule counts + composition + forecast series
│   ├── Model_DieToolDiscoveryRow.cs
│   ├── Model_PoLine.cs
│   ├── Model_UserAnalyticsSnapshot.cs
│   └── Model_ChartData.cs                  # prepared ISeries / Axis / XAxes / YAxes bundle
├── Enums/
│   ├── Enum_VisualCategory.cs              # Inventory, InventoryAuditing, Receiving, Shipping, DieToolDiscovery
│   └── Enum_ReceivingStatusGroup.cs        # OnTime, Partial, Late, Open, Closed, ...
├── Helpers/
│   └── Helper_LiveCharts.cs                # date-index projection, axis factory, paints, series switch
├── Converters/                              # created only if a binding needs one
└── DependencyInjection/
    └── VisualModuleRegistration.cs         # LiveCharts.Configure + AddVisualModule(IServiceCollection)
```

Reference rule (constitution §2 Rule 10): `MTM_WIP.Module_Visual` references **`Core`, `Shared`, `Data` only**. It references no other `Module_*`. Views reference ViewModels in the same project; ViewModels reference `IService_VisualAnalytics` and the `003` contracts (`IDialogService`, `IErrorService`, `IDispatcherService`, `ICurrentUserService`, the shared grid component). `Helper_LiveCharts` is module-local; promote it to `Shared` only if a second module later needs charts — do not pre-emptively.

---

## 2. Component Migration Strategy

### 2.1 Porting order

```
chart stack + LiveCharts.Configure (TASK-109)
→ shared chart builder + axis strategy (TASK-110)
→ orchestration service + typed row models (TASK-111)
→ forbidden-surface gate: no network, no temp file, no credential literal (TASK-112)
→ dashboard shell + launcher (TASK-113)
→ User Analytics viewer (TASK-114)
→ Visual Inventory (TASK-115)
→ Inventory Audit (TASK-116)
→ Receiving Analytics + PO Details (TASK-117)
→ Die/Tool Discovery (TASK-118)
→ downstream validation (TASK-119)
```

The chart builder is proven before any surface consumes it; a chart bound against an unproven builder produces failures that look like data bugs and are not.

### 2.2 Chart stack and axis strategy — the fixed configuration

Packages, pinned exactly (constitution §1.1 and the sibling `MTM_Receiving_Application`):

```
LiveChartsCore.SkiaSharpView.WinUI  2.0.5
Microsoft.Graphics.Win2D            1.4.0
```

Startup, called **once** from the composition root (`004`) via `VisualModuleRegistration.Configure()`:

```csharp
LiveCharts.Configure(s => s.AddSkiaSharp());
```

XAML namespace, on every chart page:

```xml
xmlns:lvc="using:LiveChartsCore.SkiaSharpView.WinUI"
```

Every chart is a `lvc:CartesianChart` bound to three ViewModel properties: `Series` (`IEnumerable<ISeries>`), `XAxes`, `YAxes` (`IEnumerable<Axis>`). The ViewModel exposes these as `[ObservableProperty]` values built by `Helper_LiveCharts`.

**Series switch** — the only three series types used, chosen by chart type:

```csharp
ColumnSeries<T>         // per-day schedule counts (integer counts)
StackedColumnSeries<T>  // status composition (on-time / partial / late / open …)
LineSeries<T>           // forecast line; user-analytics daily trend
```

**Date → integer-index axis** (fixed, not per-surface): dates are projected onto a `SortedSet<DateTime>` and each series point's X is the index into that set; the axis is built once per data set.

```csharp
var labels = sortedDates.Select(d => d.ToShortDateString()).ToArray();
new Axis
{
    Labels          = labels,
    LabelsRotation  = 0,
    TextSize        = 12,
    MinStep         = 1,
    UnitWidth       = 1
}
```

**Y axis**:

```csharp
new Axis { Labeler = value => ((int)value).ToString() }
```

**Paints** — one `SolidColorPaint` per series, sourced from `ThemeResource` brushes (never literal `#RRGGBB`; constitution §2 Rule 9):

```csharp
new SolidColorPaint(seriesColor) { StrokeThickness = 2 }   // stroke for all series
// lines additionally set Fill = null so the area under the line is not painted
```

Line series: `StrokeThickness = 2` and `Fill = null`. Column/stacked series use the stroke paint for their outline and a themed fill. The helper resolves `Color` from `Application.Current.Resources` so a theme change recolours charts without a reload.

### 2.3 ViewModel / service / data path per tool

Every tool follows one path: **View → ViewModel → `IService_VisualAnalytics` → (`Service_VisualDatabase` | `IDao_VisualAnalytics`) → `Model_Result<T>`**. The ViewModel never calls the data layer and never builds a query.

| Tool | ViewModel | Orchestration method | Data source | Returns |
| :--- | :--- | :--- | :--- | :--- |
| Dashboard | `ViewModel_VisualDashboard` | `GetDashboardDataAsync(category, ct)` | `Service_VisualDatabase` (ERP) | category result, projected to the hosted tool's row model |
| User Analytics | `ViewModel_UserAnalytics` | `GetAnalyticsSnapshotAsync()` | `Service_VisualDatabase` (snapshot) + `IDao_VisualAnalytics` (`sys_visual` shift/names) | `Model_UserAnalyticsSnapshot` |
| Visual Inventory | `ViewModel_VisualInventory` | `GetInventoryAsync(part, warehouse, location, nonZeroOnly, ct)` | `Service_VisualDatabase` | `IReadOnlyList<Model_VisualInventoryRow>` |
| Inventory Audit | `ViewModel_InventoryAudit` | `GetAuditAsync(part, start, end, ct)` | `Service_VisualDatabase` | `IReadOnlyList<Model_InventoryAuditRow>` |
| Receiving Analytics | `ViewModel_ReceivingAnalytics` | `GetScheduleAsync(po, part, week, filters, ct)` + `GetAnalyticsAsync(po, part, window, filters, ct)` | `Service_VisualDatabase` | schedule rows + `Model_ReceivingAnalytics` |
| PO Details | `ViewModel_PoDetails` | `GetPoLinesAsync(poNumber, ct)` | `Service_VisualDatabase` | `IReadOnlyList<Model_PoLine>` |
| Die/Tool Discovery | `ViewModel_DieToolDiscovery` | `SearchDiesAsync(...)` / `GetCoilFlatstockInfoAsync(...)` / `GetWhereUsedAsync(...)` | `Service_VisualDatabase` | `IReadOnlyList<Model_DieToolDiscoveryRow>` |

`Service_VisualAnalytics` is a thin, testable mapping layer: it receives the ERP `Model_Result<T>` (or a `DataTable` result where `002` preserves the legacy shape), projects each row into the typed model, and returns `Model_Result<T>` unchanged otherwise. It holds no static state, opens no connection, and contains no SQL.

### 2.4 Chart type per surface

| Surface | Chart | Series type | X |
| :--- | :--- | :--- | :--- |
| Receiving Analytics — schedule | Receiving counts per day (Monday–Friday) | `ColumnSeries<int>` | week day index |
| Receiving Analytics — composition | On-time / partial / late (plus open/closed/MMC/MMF groups) | `StackedColumnSeries<int>` | week day index |
| Receiving Analytics — forecast | Forecast line over +180 days (default) | `LineSeries<double>` (Fill = null) | date index |
| User Analytics | Transactions per day over the window (365-day window, 30-day default) | `LineSeries<int>` (Fill = null) | date index |
| User Analytics — composition | Transaction-type mix (IN / OUT / TRANSFER) per day | `StackedColumnSeries<int>` | date index |
| Visual Inventory / Audit / Die/Tool Discovery / PO Details | none — grids only | — | — |

The `Enum_ReceivingStatusGroup` and its mapping onto the nine legacy checkboxes (ShowClosed/ShowOutsideService/ShowWithPartID/ShowLate/ShowPartial/ShowOnTime/ShowOpen/ShowMMC/ShowMMF) is built in TASK-117; the three receiving chart types above are the only charts the surface needs.

### 2.5 Grids

All grid surfaces use the shared grid component from `003`. `DataTable` results are projected to typed row models in `Service_VisualAnalytics` before binding (constitution: the Toolkit grid binds `IEnumerable`, not `DataTable`). Row colour — where the legacy grid coloured rows — is carried on the row model and consumed by the component's style selector.

### 2.6 Legacy problems fixed

Each fix is a porting decision, not an afterthought:

- **Cancellation**: `AsyncRelayCommand` replaces per-control `CancellationTokenSource` + `async void`. No `async void` load handlers survive.
- **Credentials**: `DatabaseOptions.Visual` replaces `Model_Application_Variables.VisualUserName`/`VisualPassword`. The module never builds a connection string.
- **Sample data**: `Service_VisualDatabase._useSampleData` generators are not ported. Empty result → explicit empty state.
- **Service location**: no `Program.ServiceProvider.GetService<...>()`. ViewModels receive `IService_VisualAnalytics` via constructor.
- **Temp files / WebView2 / Chart.js / CDN**: deleted (TASK-112 gate).
- **"?" help button**: removed. Help is `009`'s native page.
- **Animated collapsible sections**: replaced by static, non-animated `Expander`s.

### 2.7 What is deliberately not ported

| Not ported | Why |
| :--- | :--- |
| `Form_AnalyticsViewer`'s WebView2 + `%TEMP%` HTML write | Forbidden surface; replaced by LiveCharts |
| `Form_HtmlViewer` and the temp-file analytics flow | Forbidden surface |
| `Resources\Html\*.html` (both) and `Resources\Sql\Visual\*.sql` placeholders | Dead or forbidden; not assets in the new solution |
| `_useSampleData` sample-data generators | Fake data on a real shop floor is a defect |
| Shipping | Not implemented in the WinForms app; stays visibly unavailable |
| `Control_VisualUserAnalytics` as a modal window | Replaced by a navigation card + page |

---

## 3. Data & State Migration

### 3.1 Second database — read-only contract

- All ERP access goes through `Service_VisualDatabase` in `MTM_WIP.Data` (ported verbatim by `002` TASK-031, returning `Model_Result<T>`).
- The module adds **no** `INSERT`/`UPDATE`/`DELETE`/`DROP`/`ALTER`/`TRUNCATE`/`CREATE`/`MERGE` and no `SqlConnection`/`SqlCommand`. The `002` keyword-scan test already guards the data project; TASK-112 extends the scan to the module's own source.
- `IDao_VisualAnalytics` reads `sys_visual` (main MySQL DB) through the stored-procedure helper. The `md_sys_visual_Update` path is read by nothing in this module.

### 3.2 Credentials

- **The ERP credential comes from configuration — spec `002`'s `DatabaseOptions.Visual`** (`Server`, `Database`, `User`, `Password`, `ConnectTimeoutSeconds 5`). It is bound once at startup and injected.
- **No Visual password literal may exist in the repository** — code, comments, test fixtures, or sample config. TASK-112 asserts this with a `git grep`-style scan for the legacy literal values and any `Password = "…"` in module source.

### 3.3 State replaced by this spec

| Legacy static / state | Replacement | Lifetime |
| :--- | :--- | :--- |
| `VisualUserName`, `VisualPassword` | `DatabaseOptions.Visual` (configuration) | bound once |
| Per-control `CancellationTokenSource` | `AsyncRelayCommand` cancellation | per command execution |
| `_useSampleData` flag | *(deleted)* | — |
| Cached `DataTable`s on Inventory/Audit controls | ViewModel-owned typed collections | per navigation |
| `Program.ServiceProvider` lookups | Constructor injection | application |

### 3.4 Persistence touched

None by the module. Export writes user-chosen `.xlsx` files via `SaveFileDialog` (`VisualAudit_{yyyyMMdd_HHmmss}.xlsx`, `VisualInventory_{yyyyMMdd_HHmmss}.xlsx`) using the shared ClosedXML exporter from `003`. No settings key is added or read beyond what `004`/`003` already manage.

### 3.5 Test strategy

- **ViewModels are UI-free** (constitution §4): every dependency is an interface, `IDispatcherService`'s test double runs callbacks inline, and no ViewModel references `DispatcherQueue` or `Window`. Tests cover initial state, each `AsyncRelayCommand`'s happy path, its failure path (service returns failure → `IErrorService` invoked), `CanExecute` transitions, and cancellation.
- **`Helper_LiveCharts` is unit-tested headlessly** for: date-index projection (non-contiguous dates map to dense indices), axis label/`MinStep`/`UnitWidth`/`Labeler` construction, and the series-type switch (column vs stacked vs line with `Fill = null`).
- **`Service_VisualAnalytics` is unit-tested with mocked data contracts** — no database and no ERP server needed — asserting per-tool query dispatch and row projection.
- **ERP integration tests** reuse `002`'s convention: if the server is reachable, assert one query returns `IsSuccess`; otherwise skip with `Visual ERP unreachable from build agent`.
- **The forbidden-surface gate** (TASK-112) is a test, not a manual step, so a future `WebView2`/`http://`/temp-file regression fails the build.

### 3.6 Downstream consistency

This module is the last feature module. TASK-119 re-reads `009` and sweeps `010`–`011` to confirm nothing there assumed a chart surface, a WebView2 host, or a temp-file analytics flow that this spec has now deleted.
