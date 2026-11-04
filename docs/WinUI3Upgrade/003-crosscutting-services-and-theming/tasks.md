# Migration Task List (Execution Plan)

**Spec**: `003-crosscutting-services-and-theming`
**Range**: TASK-035 … TASK-052
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Ordering is load-bearing.** Contracts and primitives come first (TASK-035 … TASK-040); the identity/cache/preferences/shortcuts services build on `002`'s DAOs (TASK-041 … TASK-044); theming, suggestions and the grid depend on those caches (TASK-045 … TASK-047); printing/export/progress/focus/diagnostics finish the surface (TASK-048 … TASK-051); TASK-052 closes the loop against `004`.

---

## Phase 1 — Logging replacement

- [ ] **TASK-035** — Delete `Helper_LogPath` and replace the CSV logger with Serilog. Add the six Serilog packages (`Serilog`, `Serilog.Sinks.File`, `Serilog.Extensions.Hosting`, `Serilog.Settings.Configuration`, `Serilog.Enrichers.Thread`, `Serilog.Enrichers.Environment`) and implement `MTM_WIP.Core/Logging/SerilogLoggingConfiguration.cs` producing a local file sink at `%LOCALAPPDATA%\MTM_WIP\logs\app-.txt` with `rollingInterval: Day`, `retainedFileCountLimit: 30` and `shared: true`, plus a background copier that mirrors sealed log files to the configured share. Per [`../000-pre-foundation/spec.md`](../000-pre-foundation/spec.md) D54 the share is **never** the primary sink.
  - **Verify**: a test constructs the logger with the share path pointed at an unreachable directory and asserts that construction succeeds, that a log call succeeds, and that the local file receives the entry. A second test asserts the copier retries a failing share without throwing into the caller. Startup must make no network call: `SerilogLoggingConfiguration` performs no I/O against the share synchronously.
  - **Why the share is not the sink**: Serilog's `FileSink` constructor calls `Directory.CreateDirectory`, so a dead share throws at construction and the application does not start; and `FileShare.Read` makes it single-writer, so two machines writing one file collide. Both facts were verified against the sink's source.
  - **Verify**: a unit test asserts the sink path, day rolling, 30-file retention and `shared` flag; `git grep -n "X:\\\\MH_RESOURCE\|Helper_LogPath\|OneDrive"` over the new solution returns zero hits (Q8 in `00-index.md`); the WinForms solution is untouched.

---

## Phase 2 — Primitive services: dispatch, dialogs, errors

- [ ] **TASK-035a** — Implement crash evidence per [`../000-pre-foundation/spec.md`](../000-pre-foundation/spec.md) D55. `MTM_WIP.Core/Diagnostics/Service_CrashReporter.cs` subscribes to `Application.UnhandledException`, `AppDomain.CurrentDomain.UnhandledException` and `TaskScheduler.UnobservedTaskException`, logs through `ILogger`, and writes a minidump to `%LOCALAPPDATA%\MTM_WIP\dumps\` via `MiniDumpWriteDump`. Separately, document the WER `LocalDumps` registry configuration as a **machine prerequisite** in `011`, because the application cannot set an `HKLM` key for itself.
  - **Verify**: a handler test asserts the minidump path is produced for a deliberately thrown managed exception, and that a throw on a pool thread is caught by the `AppDomain` handler rather than silently terminating. The `011` prerequisite exists and names both the registry path and the per-application subkey.
  - **Why both mechanisms**: `Application.UnhandledException` sees only UI-thread/XAML exceptions, background-thread exceptions bypass it and end the process, and neither it nor `AppDomain.UnhandledException` fires for a stack overflow or an access violation. `LocalDumps` catches what no managed handler can; the handler catches managed failures with better context.

- [ ] **TASK-035b** — Implement the configuration store per [`../000-pre-foundation/spec.md`](../000-pre-foundation/spec.md) D51 and `001`'s plan §3.1. `Service_Configuration` loads the committed `appsettings.json` from `AppContext.BaseDirectory`, then the optional `%PROGRAMDATA%\MTM_WIP\appsettings.local.json` (`optional: true`, `reloadOnChange: false`), binds `DatabaseSettings` from the `ConnectionStrings` section, and exposes the resolved values without leaking them into logs or error dialogs (`constitution.md` §2 Rule 4).
  - **Verify**: with no override present the committed defaults are returned and the application starts; with an override present its `ConnectionStrings:MySql` wins; a diagnostic dump of configuration renders the credential as redacted, never in clear text.

- [ ] **TASK-036** — Implement `Enum_ErrorSeverity` (`Low`/`Medium`/`High`/`Fatal`) and `Model_ErrorSeverityMap`, the single table owning the UI-title and `log_error.Severity` vocabularies (Warning / Error / Critical Database Issue / Fatal Application Error ↔ Information / Warning / Error / Critical / High).
  - **Verify**: a unit test asserts all four title mappings and the four DB-vocabulary values from [`plan.md`](./plan.md) §3.2, and that no other type in the solution defines a severity-to-title mapping.

- [ ] **TASK-037** — Implement `IDispatcherService.TryEnqueue(Action)` with `Service_Dispatcher` delegating to `DispatcherQueue`, plus an inline test double that runs the callback synchronously and returns `true`.
  - **Verify**: a unit test drives the test double and asserts the callback ran inline and `true` was returned; `git grep -n "DispatcherQueue"` over `MTM_WIP.Module_*` and `MTM_WIP.Core` returns no ViewModel or service reference outside `Service_Dispatcher`.

- [ ] **TASK-038** — Implement `IDialogService` in `MTM_WIP.Shared/Dialogs/Service_Dialog.cs` wrapping `DevWinUI.WindowedContentDialog`: every call sets `Owner` (main window), `Header`, `Content`, `PrimaryButtonContent`, `CloseButtonContent` and `DefaultButton`, then `await ShowAsync()` directly — never deferred. Add the `DevWinUI` package reference to `MTM_WIP.Shared`.
  - **Verify**: a unit test with a fake content dialog asserts all six properties are set and the await completes; `git grep -n "new WindowedContentDialog\|new ContentDialog"` over `MTM_WIP.Module_*` returns zero hits (dialogs are opened only through the service).

- [ ] **TASK-039** — Implement `IErrorService` / `Service_ErrorHandler` (instance, in `Core`) with the spec §3.1 flow: structured log → notify `IDatabaseFailureSink` on database error → 5-second per-signature suppression (`Type : caller : message-hash`) → present via `IDialogService` with the severity title → optional retry → never rethrow.
  - **Verify**: unit tests assert (a) an identical signature within 5 s presents once, (b) two different messages both present, (c) Retry invokes the supplied action, (d) the method never rethrows, (e) a database exception notifies the sink. `git grep -n "static.*Instance\|MessageBox"` over `MTM_WIP.Core` returns no static error-service facade.

- [ ] **TASK-040** — Implement `Model_ErrorDetails` and `MTM_WIP.Shared/Dialogs/Dialog_ErrorDetails.xaml` rendering three layers — plain sentence, affected operation, expandable copyable technical detail — opened through `IDialogService`.
  - **Verify**: a unit test asserts the model exposes exactly three layers and that layer one contains no connection string, no password and no stack trace; the dialog binds the model with no code-behind logic beyond lifecycle forwarding.

---

## Phase 3 — Identity, cache, preferences, shortcuts

- [ ] **TASK-041** — Implement `ICurrentUserService` / `Service_CurrentUser` with `Enum_Role` (`Administrator=1`, `ReadOnly=2`, `Normal=3`, `Developer=4`), `IsInRole(...)` and `HasAccess(...)`. Written once at sign-in, read-only afterwards.
  - **Verify**: a unit test asserts all four role IDs map correctly and the `HasAccess` matrix; a second test asserts the service rejects a second `SetUser` call after the first.

- [ ] **TASK-042** — Implement `IMasterDataCache` / `Service_MasterDataCache`: five collections (parts, operations, locations, item types, colour codes), `RefreshAsync` loading all five in parallel, and one `Changed` event. The master-data and ERP suggestion source caches fold into this singleton.
  - **Verify**: a unit test with mocked `IDao_*` asserts parallel refresh, exactly one `Changed` event, and that a failing collection leaves the other four populated rather than blanking them.

- [ ] **TASK-043** — Implement `IUserPreferences` / `Service_UserPreferences` over `IDao_User`, covering the frozen keys (`LastShownVersion`, `HideChangeLog`, `VisualUserName`, `VisualPassword`, `WipServerAddress`, `WipServerPort`, `WIPDatabase`, `AnimationsEnabled`, `AutoExpandPanels`, `ShowTotalSummaryPanel`, `Theme_Name`, `Theme_FontSize`) plus typed conveniences and a `Changed` event.
  - **Verify**: a unit test round-trips every frozen key through a mocked `IDao_User` and asserts each survives unchanged; a second test asserts `Changed` fires exactly once per `Set`.

- [ ] **TASK-044** — Implement `IShortcutService` / `Service_Shortcut`: precedence user override → system default → compiled default; seed from `Assets/default_shortcuts.json` only when a value differs; nine categories (Inventory, Remove, Transfer, Advanced Inventory, Advanced Remove, Quick Buttons, Navigation, Transactions, General); capture rejects duplicates and `Alt+0..9`.
  - **Verify**: unit tests assert the three-level precedence, seed-on-difference (no write when values match), duplicate rejection, and `Alt+0..9` rejection. The nine categories are asserted exhaustively.

---

## Phase 4 — Theming

- [ ] **TASK-045** — Implement `Enum_ThemeMode` (`Light`/`Dark`/`System`) and `IThemeService` / `Service_Theme` owning the current mode, persistence under the new key `ThemeMode` (old `Theme_Name`/`Theme_FontSize` ignored, not deleted), and a `ThemeModeChanged` event. Create the `MTM_WIP/Themes/` resource dictionaries (`Colors.Light.xaml`, `Colors.Dark.xaml` as `ThemeDictionaries`, `Typography.xaml`, `Thickness.xaml`, `ControlStyles.xaml`, `Converters.xaml`) mapping the legacy groups to the WinUI `ThemeResource` names from spec §3.2.
  - **Verify**: a unit test asserts the three-mode enum and that `Service_Theme` persists `ThemeMode` through a mocked `IUserPreferences` and raises `ThemeModeChanged`; `git grep -n "#[0-9A-Fa-f]\{6\}\|Colors\."` over `*.xaml` returns zero literal colour usage; every `Style`/`Brush` resolves through the dictionaries.

---

## Phase 5 — Suggestions and grid

- [ ] **TASK-046** — Implement `Service_SuggestionController` and `MTM_WIP.Shared/Suggestions/Control_Suggestion` (`TextBox` + `Popup`/`Flyout` + `ListView`). Preserve: below-then-above placement against window bounds, `%` wildcard with relevance ordering, 100-result cap, configurable minimum input length, F4 / Down-on-empty full list, Up/Down/Enter/Escape navigation, Enter uppercases and moves focus per the field's action, exact-match suppression, and the per-source no-match policy (master data warns and clears; ERP stays silent). Placeholder via `PlaceholderText` — no P/Invoke, no screenshot, no secondary window.
  - **Verify**: a unit test drives the controller through a fake provider and asserts wildcard filter, relevance order, 100-result cap, exact-match suppression and both no-match policies; `git grep -n "CopyFromScreen\|EM_SETCUEBANNER\|SuggestionOverlayForm"` over the new solution returns zero hits.

- [ ] **TASK-047** — Implement `Model_GridState` and `MTM_WIP.Shared/Grid/Control_DataGrid` wrapping `WinUI.TableView`: column descriptors mapped to the frozen `usr_dgv_settings` payload (no new format), row-style selection keyed on the row model, `MenuFlyout`, star-sized columns for fill, delete-key as an accelerator with `CanExecute`, and no header sorting.
  - **Verify**: a unit test round-trips a `Model_GridState` through the frozen payload and back unchanged; a second asserts the style selector chooses the row style from the row model's colour field. Add the `WinUI.TableView` package to `MTM_WIP.Shared`, and assert no `CommunityToolkit.WinUI.UI.Controls` reference exists. Acceptance against `006`'s reference grid is confirmed at TASK-052.

- [ ] **TASK-047a** — Add the automation annotations from [`../000-pre-foundation/spec.md`](../000-pre-foundation/spec.md) D59 to every shared control this spec ships: `Control_DataGrid`, `Control_Suggestion`, `Control_Progress`, `Control_ConnectionStrength` and both dialogs. Each interactive element carries `AutomationProperties.Name` matching its visible label, `HelpText` where the label is not self-explanatory, and a stable `AutomationId`.
  - **Verify**: a test enumerates every `Control_*.xaml` in `MTM_WIP.Shared` and asserts each interactive element has a non-empty `AutomationId`. **No high-contrast `ThemeDictionary` is authored** — D59 puts high contrast out of scope and its absence is the decision, not an omission.

---

## Phase 6 — Printing, export, progress, focus, diagnostics

- [ ] **TASK-048** — Implement `Model_PrintDocument`, `Model_Print_PageBoundary { PageNumber, StartRow, EndRow }`, `Service_PrintRenderer` and `Service_PrintSettingsStore`. The model represents logo, centred title, right-aligned `DateTime.Now` "g" timestamp, diagonal watermark, equal column widths, 30 px header, 25 px rows, per-row colours and footer "Page N of TotalPages"; the renderer paginates deterministically; settings persist to `%APPDATA%\MTM\PrintSettings\{GridName}.json` (`{"GridName","PrinterName","VisibleColumns","ColumnOrder","LastModified"}`).
  - **Verify**: a unit test builds a model with all visual elements and asserts the renderer's page-boundary records partition the rows deterministically; a second round-trips a print-settings JSON file and asserts all five fields survive; a third asserts the generated HTML contains the logo, title, timestamp, watermark, footer page counter and the per-row colour for a known row. Printing goes through WebView2 per D19.

- [ ] **TASK-049** — Implement `Service_ExcelExporter` (ClosedXML) over `Model_PrintDocument`: sheet `Export`, bold header filled `XLColor.FromArgb(230,230,230)`, friendly headers from `ColumnHeaders`, columns = `ColumnOrder ∩ VisibleColumns`, rows from the page range, `DateTime`→date, `bool`→bool, `IFormattable`→`ToString(null, CurrentCulture)`, `DBNull`→empty, thin borders, `AdjustToContents()`.
  - **Verify**: a unit test exports a small model and asserts sheet name, header fill, the visible-column subset, a date cell and an empty cell; add the `ClosedXML` package to `MTM_WIP.Shared`.

- [ ] **TASK-050** — Implement `Control_Progress` (Shared/Progress) as a thin `IProgress<T>`-based surface replacing `Helper_StoredProcedureProgress`: reports 0–100, monotonic, clamped, cancellable via `CancellationToken`.
  - **Verify**: a unit test feeds an out-of-order sequence and asserts the reported values are monotonic and clamped to 0–100; a second asserts a cancelled token stops reporting.

- [ ] **TASK-051** — Implement `SelectAllOnFocusBehavior` (Shared/Behaviors, select-all on focus for text controls, restore theme background on blur, no static control registry) and `Helper_ConnectionStrength` + `Control_ConnectionStrength` (Shared/Diagnostics) with the thresholds `<50→5, <100→4, <200→3, <400→2, <800→1, else 0` and failure `(0, -1)`, consuming `IDatabaseConnectionMonitor` via `IDispatcherService`.
  - **Verify**: a unit test asserts every threshold boundary and the failure mapping; a behaviour test on a fake control asserts select-all on focus and background restore on blur; `git grep -n "_controlsWithActiveHandlers\|Focus_Utilities"` over the new solution returns zero hits.

---

## Phase 7 — Downstream consistency validation

- [ ] **TASK-052** — **Downstream validation: `004-shell-and-startup` (and a sweep of `005`–`011`).**

  Spec `004` is the first consumer of everything this spec defines — its composition root registers these services, applies the theme, and runs the boot sequence. Re-read [`../004-shell-and-startup/spec.md`](../004-shell-and-startup/spec.md), [`plan.md`](../004-shell-and-startup/plan.md) and [`tasks.md`](../004-shell-and-startup/tasks.md), then sweep `005`–`011`.

  Confirm and record:
  1. **`004`'s composition root registers every service this spec defines** — `IDialogService`, `IErrorService`, `IDispatcherService`, `ICurrentUserService`, `IMasterDataCache`, `IUserPreferences`, `IShortcutService`, `IThemeService` — with the singleton lifetimes assumed here, and calls `SerilogLoggingConfiguration` from TASK-035.
  2. **`004` applies `IThemeService.Mode`** by setting the root element's `RequestedTheme` and the title bar, and wires the Windows 10 Mica fallback to a solid `ThemeResource` background.
  3. **`004` populates `IParameterPrefixCache` during launch** before any screen runs, and writes `ICurrentUserService` exactly once at sign-in.
  4. **`005`–`008` own their grid row models** and consume `Model_GridState` rather than re-implementing grid state; `006`'s reference grid accepts TASK-047.
  5. **`005`–`009` open every dialog through `IDialogService`** and every user-visible error through `IErrorService`; no module references `DispatcherQueue`, `Window`, or `ContentDialog` directly.
  6. **`008` handles ERP failures through the same `Model_Result<T>` + `IErrorService` path** as WIP failures (the Visual reader from `002` returns `Model_Result<T>`).
  7. **`011` does not restore the deleted `%APPDATA%`/`%LOCALAPPDATA%` startup wipe**, and deploys the Serilog `logs/` folder to a writable per-user path.

  If any item is false, fix **this** spec before proceeding. Do not amend `004`–`011` to work around a defect here.

  - **Verify**: a written entry exists in the Drift log for each of the seven items, each marked *confirmed* or *amended*, and the full test suite is green.

---

## Drift log

Filled in by TASK-052.

| Checked spec | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `004` | All eight services registered with the assumed singleton lifetimes | *(pending)* | — |
| `004` | `RequestedTheme` + title bar + Mica fallback applied from `IThemeService` | *(pending)* | — |
| `004` | `IParameterPrefixCache` populated at launch; `ICurrentUserService` written once | *(pending)* | — |
| `005`–`008` | Modules reuse `Model_GridState`; `006` reference grid accepts TASK-047 | *(pending)* | — |
| `005`–`009` | Dialog/error paths go through `IDialogService`/`IErrorService`; no direct `DispatcherQueue`/`Window`/`ContentDialog` | *(pending)* | — |
| `008` | ERP failures flow through `Model_Result<T>` + `IErrorService` | *(pending)* | — |
| `011` | No AppData wipe; Serilog `logs/` deployed to a writable per-user path | *(pending)* | — |

## Known deviations from the WinForms behaviour

Recorded so a reviewer does not mistake them for defects.

| Deviation | Rationale | Reviewed by |
| :--- | :--- | :--- |
| The ~200-token theme engine is deleted, not ported | WinUI paints from resources; per-control border/selection colours have no home | *(this spec)* |
| The CSV logger and its hard-coded log paths are deleted | Serilog file sink replaces them; a personal path in shipped code is a defect | *(this spec)* |
| The suggestion overlay's screenshot/`EM_SETCUEBANNER` mechanism is deleted | A `Popup`/`Flyout` replaces it; no secondary window, no P/Invoke | *(this spec)* |
| Header sorting on grids is removed | Sorting a server-paged page implies sorting the set; the sort order is stated in the UI | *(this spec)* |
| `Theme_FontSize` is not re-introduced | It reached almost nothing in the legacy UI; Windows text scaling applies instead | *(this spec)* |
| Master-data vs ERP no-match inconsistency is preserved | Operators have learned it; unifying is a product decision, not an implementation one | *(this spec)* |
