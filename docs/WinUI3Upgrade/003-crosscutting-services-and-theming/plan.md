# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `003-crosscutting-services-and-theming`
**Depends on**: `001-foundation-and-tooling`, `002-data-access-layer`
**Unblocks**: `004` (composition root), `005`–`009`
**Tasks**: TASK-035 … TASK-052
**Governing rules**: [`../constitution.md`](../constitution.md) §2 Rules 5, 6, 9, 11, 12

This spec ships no screen. It builds the shared machinery every screen uses and deletes three mechanisms with a replacement: the CSV logger, the 200-token theme engine, and the screenshot-painted suggestion overlay.

---

## 1. Project Topology & Directory Mapping

No new project is created. `MTM_WIP.Core` (contracts, enums, pure services) and `MTM_WIP.Shared` (dialogs, controls, grid, printing, export, behaviours) already exist from `001`; `MTM_WIP.Data` exists from `002`.

```text
MTM_WIP.Core/                              # contracts + pure services (no UI types)
├── Contracts/
│   ├── IErrorService.cs                   # HandleExceptionAsync(severity, context, retry)
│   ├── IDialogService.cs                  # contract only; impl in Shared
│   ├── IDispatcherService.cs              # TryEnqueue(Action) → bool
│   ├── ICurrentUserService.cs             # IsInRole / HasAccess
│   ├── IMasterDataCache.cs                # 5 collections, RefreshAsync, Changed
│   ├── IUserPreferences.cs                # frozen keys, Changed
│   ├── IShortcutService.cs                # override → system → compiled
│   ├── IThemeService.cs                   # Light / Dark / System
│   └── IDatabaseFailureSink.cs            # seam for the error service → 002's monitor
├── Enums/
│   ├── Enum_ErrorSeverity.cs              # Low / Medium / High / Fatal
│   ├── Enum_ThemeMode.cs                  # Light / Dark / System
│   └── Enum_Role.cs                       # Administrator=1, ReadOnly=2, Normal=3, Developer=4
├── Models/
│   ├── Model_ErrorSeverityMap.cs          # the single UI-title ↔ DB-vocabulary table
│   ├── Model_ErrorDetails.cs              # sentence / operation / technical detail
│   └── Model_ConnectionStrength.cs        # (int bars, int latencyMs)
├── Services/
│   ├── Service_ErrorHandler.cs            # IErrorService
│   ├── Service_Dispatcher.cs              # IDispatcherService + inline test double
│   ├── Service_CurrentUser.cs             # ICurrentUserService
│   ├── Service_MasterDataCache.cs         # IMasterDataCache
│   ├── Service_UserPreferences.cs         # IUserPreferences
│   ├── Service_Shortcut.cs                # IShortcutService
│   ├── Service_Theme.cs                   # IThemeService (state + persistence)
│   └── Helper_ConnectionStrength.cs       # pure threshold mapper
├── Logging/
│   └── SerilogLoggingConfiguration.cs     # consumed by 004's composition root
└── Models/Model_Result.cs, Model_Dao_Status.cs   # owned by 002 — not touched here

MTM_WIP.Shared/                            # Core + UI: DevWinUI, Toolkit DataGrid, ClosedXML
├── Dialogs/
│   ├── Service_Dialog.cs                  # IDialogService over DevWinUI.WindowedContentDialog
│   └── Dialog_ErrorDetails.xaml(.cs)      # renders Model_ErrorDetails
├── Suggestions/
│   ├── Service_SuggestionController.cs    # filtering, relevance, placement, no-match policy
│   └── Control_Suggestion.xaml(.cs)       # TextBox + Popup/Flyout + ListView
├── Grid/
│   ├── Control_DataGrid.xaml(.cs)         # shared grid component (Toolkit DataGrid)
│   └── Model_GridState.cs                 # column descriptors ↔ frozen usr_dgv_settings payload
├── Printing/
│   ├── Model_PrintDocument.cs
│   ├── Model_Print_PageBoundary.cs        # PageNumber / StartRow / EndRow
│   ├── Service_PrintRenderer.cs           # HTML/CSS renderer (D19)
│   └── Service_PrintSettingsStore.cs      # %APPDATA%\MTM\PrintSettings\{GridName}.json
├── Export/
│   └── Service_ExcelExporter.cs           # ClosedXML over Model_PrintDocument
├── Progress/
│   └── Control_Progress.xaml(.cs)         # IProgress<T>-based surface
├── Behaviors/
│   └── SelectAllOnFocusBehavior.cs        # no static control registry
└── Diagnostics/
    └── Control_ConnectionStrength.xaml(.cs)  # consumes 002's IDatabaseConnectionMonitor
```

### 1.1 Reference-matrix constraint this spec must honour

`Core` references nothing; `Shared` references `Core` only. `Shared` may **not** reference `Data`. One consequence is load-bearing: the error service (in `Core`) must notify the connection monitor (in `Data`) without referencing it. The seam is `IDatabaseFailureSink` in `Core`, implemented by `Service_DatabaseConnectionMonitor` from `002` and wired by `004`'s composition root. *(to confirm: whether `002`'s `IDatabaseConnectionMonitor` already exposes an equivalent notify method that can be reused instead of a new contract.)*

### 1.2 Package references

| Package | Version | Added by this spec to | Consumed by |
| :--- | :--- | :--- | :--- |
| `Serilog` + `Serilog.Sinks.File` + `Serilog.Extensions.Hosting` + `Serilog.Settings.Configuration` + `Serilog.Enrichers.Thread` + `Serilog.Enrichers.Environment` | 4.x | `MTM_WIP.Core` (helper) and the app (in `004`) | logging |
| `DevWinUI` | 10.8.0 | `MTM_WIP.Shared` | `WindowedContentDialog`, `AutoScrollView` |
| `CommunityToolkit.WinUI.UI.Controls` | 7.1.2 | `MTM_WIP.Shared` | `DataGrid` |
| `ClosedXML` | 0.105.0 | `MTM_WIP.Shared` | Excel export |
| `Microsoft.Web.WebView2` | 1.0.4022.49 (match the WinForms build) | `MTM_WIP.Shared` | **Required by D19** — the print renderer prints generated HTML through WebView2. Also used by `009` for help topic bodies (D20). |
| `WinUIEx` | 2.9.2 | app (`004`) *(to confirm placement)* | `WindowEx` on `MainWindow` |
| `LiveChartsCore.SkiaSharpView.WinUI` | 2.0.5 | `008` | charts — out of scope here |
| `HtmlSanitizer` | 9.0.892 | `009` | help feedback text — out of scope here |

---

## 2. Component Migration Strategy

### 2.1 Dialogs — `IDialogService`

One service, one implementation (`Service_Dialog` in `Shared`), wrapping `DevWinUI.WindowedContentDialog`. The wrapper exists so no caller builds a dialog: it always sets `Owner` to the main window, `Header`, `Content`, `PrimaryButtonContent`, `CloseButtonContent` and `DefaultButton`, then `await ShowAsync()` directly. Deferring that await breaks the modal lifetime — this is the known trap the wrapper removes. Surface from the top-level plan: `ShowMessageAsync`, `ShowConfirmationAsync`, `ShowContentDialogAsync<TContent>`, `ShowWindowedDialogAsync<TContent>`. The ten legacy dialogs map onto these four plus `Dialog_ErrorDetails`.

### 2.2 Errors — `IErrorService` + `Dialog_ErrorDetails`

`Service_ErrorHandler` (instance, in `Core`) replaces the static `Service_ErrorHandler.Instance` and its facades. The flow is the spec §3.1 flow, unmodified:

1. log at the severity's level with structured context (`user, machine, operation, control, exception type, message`);
2. if the exception is a database error, notify `IDatabaseFailureSink`;
3. suppress an identical signature (`Type : caller : message-hash`) within 5 seconds — per signature, not global;
4. present via `IDialogService` with the severity's title;
5. invoke the caller's retry action if the user chose Retry;
6. never rethrow.

Logging happens **before** presentation. `Fatal` terminates through `004`'s orderly exit, not `Environment.Exit`. `Model_ErrorSeverityMap` is the single table owning both vocabularies (UI title vs `log_error.Severity` column value); the DB values `Information/Warning/Error/Critical/High` survive for the column, while `Low/Medium/High/Fatal` drive the four titles `Warning / Error / Critical Database Issue / Fatal Application Error`.

`Dialog_ErrorDetails.xaml` renders `Model_ErrorDetails` — three layers: a plain sentence, the affected operation, and expandable copyable technical detail. Layer one never contains a connection string, a password, or a stack trace.

### 2.3 Logging — Serilog

Replaces `LoggingUtility`/`Service_Logging` and `Helper_LogPath` entirely. Configuration (owned here, applied by `004`): file sink path `logs/app-.txt`, `rollingInterval: Day`, `retainedFileCountLimit: 30`, `shared: true`, plus the Thread and Environment enrichers. Structured properties replace the CSV columns `Timestamp, Level, Source, Message, Details`. Both hard-coded paths — the `X:\MH_RESOURCE\...` network share and the developer-specific OneDrive override — are deleted, as is the `%ProgramData%` fallback; `004` chooses the single writable per-machine folder. *(to confirm: whether `logs/` is written beside the network-share payload or redirected to `%LOCALAPPDATA%\MTM\WIP\logs` — an unpackaged app launched from a network share may not be writable.)*

### 2.4 Dispatch — `IDispatcherService`

`TryEnqueue(Action)` returns `bool`. `Service_Dispatcher` (in `Core`) delegates to `DispatcherQueue`; the inline test double runs the callback synchronously and returns `true`. ViewModels never reference `DispatcherQueue` directly — that is a constitution §2 Rule 6 defect.

### 2.5 Identity — `ICurrentUserService`

`Service_CurrentUser` replaces `Model_Application_Variables.User` plus the four role flags. Written once at sign-in, read-only afterwards. `IsInRole(Enum_Role)` and `HasAccess(...)` are checked when an action runs, not only when a view is built. Role IDs are the frozen matrix: `1=Administrator, 2=ReadOnly, 3=Normal, 4=Developer`.

### 2.6 Master data — `IMasterDataCache`

`Service_MasterDataCache` replaces `ColorCodeParts`, `ValidColorCodes` and the `Helper_UI_ComboBoxes` lists — one cache, one `RefreshAsync`, one `Changed` event. Five collections: parts, operations, locations, item types, colour codes. The refresh loads all five in parallel; one failing lookup must not blank the others. The suggestion source caches (master-data and ERP) fold into this singleton instead of the legacy `Helper_SuggestionTextBox` static caches. *(to confirm: whether the five collections are typed model records in `Core/Models` or `IReadOnlyList<string>`, which depends on the `IDao_*` return shapes delivered by `002`.)*

### 2.7 Preferences — `IUserPreferences`

`Service_UserPreferences` replaces the scattered `Dao_User.Get…Async` calls with one accessor and one `Changed` event, over `IDao_User`. Frozen keys: `LastShownVersion, HideChangeLog, VisualUserName, VisualPassword, WipServerAddress, WipServerPort, WIPDatabase, AnimationsEnabled, AutoExpandPanels, ShowTotalSummaryPanel, Theme_Name, Theme_FontSize`. Typed conveniences for `AnimationsEnabled`, `AutoExpandPanels`, `ShowTotalSummaryPanel` and `TransactionPageSize`.

### 2.8 Shortcuts — `IShortcutService`

`Service_Shortcut` replaces `Service_Shortcut`. Precedence: user override → system default (`sys_shortcuts`) → compiled default. Seeding from `Assets/default_shortcuts.json` writes a row only when the value differs (avoids the ~50 no-op upserts). Nine categories: Inventory, Remove, Transfer, Advanced Inventory, Advanced Remove, Quick Buttons, Navigation, Transactions, General. Capture rejects duplicates and `Alt+0..9`.

### 2.9 Theming — `IThemeService` + `Themes/*.xaml`

**Deleted, not ported**: `ThemeManager`, `ThemeStore`, `ThemeDebouncer` (300 ms), 18 `IThemeApplier` implementations, `Model_Shared_UserUiColors` (~200 nullable `Color?` properties), `Core_Themes`, `Core_AppThemes`, `Core_JsonColorConverter`, and `ThemedForm`/`ThemedUserControl`. The `app_themes` table and `sys_theme_GetAll` become unread; the data is not deleted.

Replacement: `Enum_ThemeMode { Light, Dark, System }` and `IThemeService` owning the current mode, its persistence (new key `ThemeMode` *(to confirm the exact key string with `007`)*; old `Theme_Name`/`Theme_FontSize` left in place and ignored), and a `ThemeModeChanged` event. `Service_Theme` is pure state + persistence; `004` applies `RequestedTheme` on the root element and the title bar, and applies Mica with a solid `ThemeResource` fallback on Windows 10.

The resource dictionaries live in `MTM_WIP/Themes/` — `Colors.Light.xaml`, `Colors.Dark.xaml` (as `ThemeDictionaries`), `Typography.xaml`, `Thickness.xaml`, `ControlStyles.xaml`, `Converters.xaml` — mapping the legacy groups onto the WinUI resource names from spec §3.2 (`AccentFillColorDefaultBrush`, `TextFillColorPrimaryBrush`, `ControlStrokeColorDefaultBrush`, `SystemFillColorCriticalBrush`, …). Roughly 110 legacy tokens have no equivalent and disappear. *(Resolved: the folder is `Themes/` in the app project, per `constitution.md` §2 Rule 9, which is authoritative. Spec `001` was corrected to match.)*

### 2.10 Suggestions

`Service_SuggestionController` + `Control_Suggestion` (`TextBox` + `Popup`/`Flyout` + `ListView`) replace `Component_SuggestionTextBox`, `Component_SuggestionTextBoxWithLabel` and `SuggestionOverlayForm`. **No secondary window, no `CopyFromScreen` screenshot, no `EM_SETCUEBANNER` P/Invoke** — the placeholder is `PlaceholderText`. Preserved: below-then-above placement against window bounds, `%` wildcard with relevance ordering, 100-result cap, configurable minimum input length, F4 / Down-on-empty full list, Up/Down/Enter/Escape navigation, Enter uppercases and moves focus per the field's configured action, exact-match suppression, and the per-source no-match policy (master data warns and clears; ERP stays silent). The controller depends on `IMasterDataCache` and `IDispatcherService`, never on a static cache.

### 2.11 Grids

`Control_DataGrid` wraps Community Toolkit `DataGrid` and replaces `DataGridView` + `Service_DataGridView`. Spec §3.3 differences apply: `DataSource = DataTable` becomes `ItemsSource = IEnumerable<TModel>`; `CellFormatting` becomes a `RowStyleSelector` keyed on a row model that carries whatever decides the colour; `AutoSizeColumnsMode.Fill` becomes `DataGridLength(1, Star)`; per-user column state maps column descriptors onto the **frozen** `usr_dgv_settings` payload (no new format); header sorting is not offered. Delete-key interception becomes an accelerator with `CanExecute`. `Model_GridState` is the seam every grid screen reuses; each module still owns its row model and its colour rule.

### 2.12 Printing

`Model_PrintDocument` is the seam that replaces `Core_TablePrinter` + `Helper_PrintManager` + the 2000-line `PrintForm`. It must represent logo, centred title, right-aligned `DateTime.Now` "g" timestamp, diagonal watermark, equal column widths, 30 px header, 25 px rows, per-row colours, footer "Page N of TotalPages", and `Model_Print_PageBoundary { PageNumber, StartRow, EndRow }`. **Per D19**, `Service_PrintRenderer` renders that model to HTML/CSS and printing goes through WebView2 (`ShowPrintUI`), which supplies pagination, page-break control via CSS and the system print dialog. The model is the seam precisely so the route can be swapped without touching a screen. `Service_PrintSettingsStore` persists `%APPDATA%\MTM\PrintSettings\{GridName}.json` with `{"GridName","PrinterName","VisibleColumns","ColumnOrder","LastModified"}` — same path, same shape.

### 2.13 Export

`Service_ExcelExporter` (ClosedXML) exports the same `Model_PrintDocument`: single sheet `Export`, bold header filled `XLColor.FromArgb(230,230,230)`, friendly headers from `ColumnHeaders`, columns = `ColumnOrder ∩ VisibleColumns`, rows from the page range, `DateTime`→date, `bool`→bool, `IFormattable`→`ToString(null, CurrentCulture)`, `DBNull`→empty, thin borders, `AdjustToContents()`.

### 2.14 Progress

`Control_Progress` replaces `Helper_StoredProcedureProgress` (`ToolStripProgressBar` + status label). A thin `IProgress<T>` surface reporting 0–100, monotonic, clamped, and cancellable via `CancellationToken`.

### 2.15 Focus

`SelectAllOnFocusBehavior` replaces `Focus_Utilities`. Select-all on focus (text controls only), restore the theme background on blur. No static `HashSet<Control>` registry; the behaviour is attached per element in XAML.

### 2.16 Connection strength

`Helper_ConnectionStrength` (pure, in `Core`) maps latency to bars: `<50 ms → 5, <100 → 4, <200 → 3, <400 → 2, <800 → 1, else 0`; failure returns `(0, -1)`. `Control_ConnectionStrength` (in `Shared/Diagnostics`) consumes `IDatabaseConnectionMonitor` from `002` via `IDispatcherService` and renders the result.

---

## 3. Data & State Migration

### 3.1 Persistence inventory

| Data | Store | Compatibility |
| :--- | :--- | :--- |
| Appearance mode (`Light`/`Dark`/`System`) | user settings JSON, new key `ThemeMode` | new key; old `Theme_Name`/`Theme_FontSize` ignored, not deleted |
| `AnimationsEnabled`, `AutoExpandPanels`, `ShowTotalSummaryPanel`, `TransactionPageSize` | user settings JSON | same keys |
| Shortcut overrides | `usr_user_shortcuts` | unchanged |
| Shortcut defaults | `sys_shortcuts`, seeded from `Assets/default_shortcuts.json` only when values differ | unchanged |
| Grid layout | `usr_dgv_settings` via `usr_dgv_settings_Get`/`Set` | payload shape frozen |
| Print settings | `%APPDATA%\MTM\PrintSettings\{GridName}.json` | same path, same shape |
| Log files | Serilog `logs/app-*.txt` | replaces the three CSV files entirely |

### 3.2 Severity vocabulary — the single mapping table

`Model_ErrorSeverityMap` owns the conversion; both vocabularies survive.

| UI severity | UI title | `log_error.Severity` value |
| :--- | :--- | :--- |
| `Low` | Warning | `Information` |
| `Medium` | Error | `Error` |
| `High` | Critical Database Issue | `Critical` |
| `Fatal` | Fatal Application Error | `High` |

The exact low→`Information` and fatal→`High` pairings are the legacy behaviour and are asserted in a unit test rather than left to convention.

### 3.3 Connection-strength thresholds

| Latency | Bars |
| :--- | :--- |
| `< 50 ms` | 5 |
| `< 100 ms` | 4 |
| `< 200 ms` | 3 |
| `< 400 ms` | 2 |
| `< 800 ms` | 1 |
| otherwise | 0 |
| failure | `(0, -1)` |

### 3.4 What is not ported

| Legacy | Fate |
| :--- | :--- |
| `Helper_LogPath` hard-coded network share and OneDrive override | deleted — Serilog owns file paths |
| `%APPDATA%` / `%LOCALAPPDATA%` application-folder wipe on launch | not ported — a wiped folder cannot hold preferences |
| `Service_ErrorHandler.Instance` + static facades | instance `IErrorService` |
| `LoggingUtility`, `Service_Logging.Instance` | `ILogger<T>` (Serilog) |
| `Core/Theming/**` engine, 18 appliers, `Model_Shared_UserUiColors` | deleted — native theming |
| `ThemedForm` / `ThemedUserControl` | deleted |
| `app_themes` table, `sys_theme_GetAll` | unread; data not deleted |
| `Focus_Utilities._controlsWithActiveHandlers` | deleted — per-element behaviour |
| `Helper_SuggestionTextBox` static caches | `IMasterDataCache` + ERP cache |

---

## 4. Gaps & open questions

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | Print-rendering route | **Resolved (D19)** — HTML/CSS from the print model, printed through WebView2. TASK-048 builds the renderer; the model (TASK-047) is unchanged by the choice. |
| 2 | Whether `IDatabaseConnectionMonitor` (from `002`) already exposes a notify method the error service can reuse, or `IDatabaseFailureSink` is a new contract | *(to confirm)* — either way the seam lives in `Core` |
| 3 | `IMasterDataCache` entry types: typed records vs `IReadOnlyList<string>` | *(to confirm)* — depends on `002` DAO return shapes |
| 4 | Exact new theme-mode settings key string | *(to confirm)* with `007`'s settings page |
| 5 | `Themes/` location | **Resolved** — app-project `MTM_WIP/Themes/`, per `constitution.md` §2 Rule 9. Spec `001` was corrected from `Styles/` to `Themes/`. |
| 6 | Serilog `logs/` folder when the app is launched from a network share | *(to confirm)* — relocate to `%LOCALAPPDATA%\MTM\WIP\logs` if the share is read-only |
| 7 | `WinUIEx` package placement (Shared vs app) | *(to confirm)* — `WindowEx` is consumed by `004`'s `MainWindow` |

### Assumptions

1. `CommunityToolkit.WinUI.UI.Controls` 7.1.2 `DataGrid` serves 12+ screens; final acceptance is against `006`'s reference grid (re-checked by TASK-052).
2. A single `Popup`-based suggestion control serves both master-data and ERP sources.
3. Windows 10 without Mica falls back to a solid `ThemeResource` background, not an unstyled default.
