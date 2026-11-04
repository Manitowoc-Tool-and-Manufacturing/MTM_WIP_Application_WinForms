# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `003-crosscutting-services-and-theming`
**Depends on**: `001-foundation-and-tooling`, `002-data-access-layer`
**Unblocks**: `004` (composition root), `005`–`009` (every screen)
**Tasks**: TASK-035 … TASK-052
**Governing rules**: [`../constitution.md`](../constitution.md) §2 Rules 5, 6, 9, 11, 12

---

## 1. Executive Summary & Scope

This spec builds the shared machinery every screen uses: dialogs, errors, logging, dispatch, identity, caches, preferences, shortcuts, theming, suggestions, grids, printing and export. It ships no screen of its own.

Three of these are not "new services" but deletions with a replacement. The WinForms application themes itself by enumerating child controls and painting each one with one of eighteen appliers over a two-hundred-property colour model. WinUI 3 does not work that way, and reproducing that mechanism would be both impossible and wrong. The specification's job here is to name what replaces each deleted mechanism so no screen is left looking for it.

### In scope

| Group | Delivered |
| :--- | :--- |
| Dialogs | one `IDialogService` wrapping a windowed content dialog |
| Errors | one `IErrorService`: log, suppress duplicates, present, offer retry |
| Logging | Serilog file sink with day rolling and 30-file retention |
| Dispatch | `IDispatcherService` with an inline test double |
| Identity | `ICurrentUserService` with the four-role matrix |
| Master data | `IMasterDataCache` for parts, operations, locations, item types, colour codes |
| Preferences | `IUserPreferences` over the existing settings procedures |
| Shortcuts | `IShortcutService` with override precedence |
| Theming | `Themes/` dictionaries, `IThemeService` (`Light`/`Dark`/`System`) |
| Suggestions | the auto-suggest controller and its popup control |
| Grids | the shared grid component with per-user state |
| Printing | the print document model and its renderer |
| Export | Excel export over the same model |
| Progress | `IProgress`-based reporting with cancellation |
| Focus | select-all-on-focus behaviour |
| Diagnostics | connection-strength indicator |

### Out of scope

- The shell, windows and launch sequence — `004`.
- Any operation screen.
- Help rendering — `009`.

### Success condition

A throwaway test page can open a dialog, show an error, select a theme, bind a suggestion field and display a grid — all through interfaces, all of them unit-tested without a UI thread.

---

## 2. Legacy Feature Assessment & UI Mapping Table

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `Service_ErrorHandler` (static `Instance` + static facades, shows `EnhancedErrorDialog`, 5 s suppression) | `IErrorService` / `Service_ErrorHandler` (instance) | Same flow: log → suppress duplicates → present → optional retry. Same severity-to-title mapping. No static facades, no `MessageBox`. |
| `EnhancedErrorDialog` (severity-sized, plain English, technical details expander, Retry, View Logs) | `Dialog_ErrorDetails.xaml` opened through `IDialogService` | Same three layers of information: a plain sentence, the affected operation, and expandable technical detail. Technical detail is copyable. |
| `Enum_ErrorSeverity` — `Low`, `Medium`, `High`, `Fatal` mapped to titles Warning / Error / Critical Database Issue / Fatal Application Error | Same enum, same four titles | The severity vocabulary in `log_error.Severity` is `Information`, `Warning`, `Error`, `Critical`, `High`. Both must survive: one drives the UI, one drives the column. A single mapping table owns the conversion. |
| `LoggingUtility` / `Service_Logging` — CSV, three files per user (`_normal`, `_db_error`, `_app_error`), `BlockingCollection` writer, 5 IO retries, 30-day + 50-file cleanup | Serilog local file sink at `%LOCALAPPDATA%\MTM_WIP\logs\app-.txt`, `rollingInterval: Day`, `retainedFileCountLimit: 30`, `shared: true`, plus a background copier to the configured share | Replaces it entirely. Structured properties replace the CSV columns `Timestamp, Level, Source, Message, Details`. The share is a copy destination, never the sink (D54). |
| `Helper_LogPath` — primary path a **network share**, a developer-specific OneDrive override, then `%ProgramData%` | One per-machine folder, configured once | Both the hard-coded share and the personal override are deleted. A personal path in shipped code is a defect. |
| `Service_OnStartup_AppLifecycle.SetupGlobalExceptionHandling` (WinForms `ThreadException` + `AppDomain.UnhandledException`) | Handled in `004` using this spec's error service | Split out because it is a lifecycle concern. |
| `Control.Invoke` / `BeginInvoke` / `InvokeRequired` (used on ~20 screens) | `IDispatcherService.TryEnqueue(...)` | ViewModels must not reference `DispatcherQueue` directly, or they cannot be tested. The test double runs the callback inline. |
| `Model_Application_Variables.User` + `UserTypeDeveloper`/`Admin`/`Normal`/`ReadOnly` | `ICurrentUserService` with `IsInRole(...)` and `HasAccess(...)` | Written once at sign-in, read-only afterwards. Role checks happen when an action runs, not only when a view is built. |
| `Model_Application_Variables.ColorCodeParts`, `ValidColorCodes`, and the combo lists from `Helper_UI_ComboBoxes` | `IMasterDataCache` (5 collections, `RefreshAsync`, change event) | One cache, one refresh, one notification. Today the colour-code cache and the combo caches refresh on different triggers and drift apart. |
| `Dao_User.GetAnimationsEnabledAsync`, `GetAutoExpandPanelsAsync`, `GetShowTotalSummaryPanelAsync`, `TransactionPageSize` | `IUserPreferences` | Same stored keys, one accessor, one change event. |
| `Service_Shortcut` (override → system default → compiled default; seeded from `default_shortcuts.json`) | `IShortcutService` | Same precedence, same seeding rule (write only when a value differs), same nine categories. |
| `Core/Theming/**` — `ThemeManager`, `ThemeStore`, `ThemeDebouncer` (300 ms), 18 `IThemeApplier` implementations, `Model_Shared_UserUiColors` (~200 nullable `Color?` properties), `Core_Themes`, `Core_AppThemes`, `Core_JsonColorConverter` | `Themes/*.xaml` + `IThemeService` | **Deleted, not ported.** See §3.2 for the token-by-token fate. |
| `ThemedForm` / `ThemedUserControl` base classes | *(deleted)* | No form enumerates its children to paint them. Theming comes from resources resolved by the framework. |
| `app_themes` table and `sys_theme_GetAll` | Unread | The table stays; the new client does not read it. User data is not deleted. |
| `Component_SuggestionTextBox` + `Component_SuggestionTextBoxWithLabel` + `SuggestionOverlayForm` (modal full-screen form painted with a screenshot of its parent, owner-drawn list, `EM_SETCUEBANNER` P/Invoke) | Suggestion control: `TextBox` + `Popup`/`Flyout` + `ListView`, driven by a controller | **No secondary window, no screenshot capture, no P/Invoke.** Behaviour preserved: below-then-above placement, `%` wildcards, 100-result cap, F4 full list, exact-match suppression, per-source no-match policy. |
| `DataGridView` + `Service_DataGridView` (row colouring, per-user column state, summary panel injection) | Shared grid component over `WinUI.TableView` | Column descriptors, row-style selector, per-user state, `MenuFlyout`. **Not a drop-in** — see §4.1. |
| `Core_TablePrinter` + `Helper_PrintManager` + `PrintForm` (GDI+ drawing, 2000-line dialog) | Print document model + renderer + a dialog over the model | The model is the seam. Logo, title, timestamp, watermark, column widths, per-row colour, footer and page boundaries must all be representable. |
| `Helper_ExportManager` (ClosedXML, sheet `Export`, grey bold header, visible columns in order) | Same, over the print document model | Identical output rules. |
| `Helper_StoredProcedureProgress` (`ToolStripProgressBar` + status label) | `IProgress<T>` + a progress surface | Reported 0–100, monotonic, clamped, cancellable. |
| `Focus_Utilities` (static `HashSet<Control>` tracking, select-all on focus, `BeginInvoke` deferral) | `SelectAllOnFocusBehavior` | Select-all on focus, restore on blur. No static control registry. |
| `Control_ConnectionStrengthControl` (custom paint, 5 bars, ping tooltip) | `FontIcon` + `ToolTip` fed by `IDatabaseConnectionMonitor` | Same 0–5 thresholds: <50 ms = 5, <100 = 4, <200 = 3, <400 = 2, <800 = 1, else 0. |

---

## 3. Behavioral Edge Cases & State Constraints

### 3.1 The error path, exactly

```
catch (Exception ex)
  → IErrorService.HandleExceptionAsync(ex, severity, context, callerName, controlName)
      1. log at the severity's level with structured context
         (user, machine, operation, control, exception type, message)
      2. if the exception is a database error → notify the connection monitor
      3. suppression: identical signature (type : caller : message-hash)
         within 5 seconds is logged but not presented
      4. present via IDialogService with the severity's title
         (Low="Warning", Medium="Error", High="Critical Database Issue", Fatal="Fatal Application Error")
      5. if the caller supplied a retry action and the user chose Retry → invoke it
      6. never rethrow
```

Constraints:

- Logging happens **before** presentation. If the dialog throws — which happens when the UI thread is already faulting — the record still exists.
- The suppression window is per signature, not global. Two different errors in the same second both present.
- `Fatal` still terminates, but through `004`'s orderly exit, not `Environment.Exit`.
- The error dialog never shows a connection string, a password, or a full stack trace in the first layer.

### 3.2 Theme tokens — what survives

The analysis found roughly **200** colour properties across the old model. They collapse to about twenty resources. This is the intended outcome, and the table exists so a reviewer can confirm no visual intent was lost.

| Legacy group | Count | WinUI resource |
| :--- | :--- | :--- |
| Accent, secondary accent | 2 | `AccentFillColorDefaultBrush`, `AccentFillColorSecondaryBrush` |
| Form / panel / user-control / control background | 4 | `ApplicationPageBackgroundThemeBrush`, `LayerFillColorDefaultBrush`, `CardBackgroundFillColorDefaultBrush` |
| Control backgrounds (text box, combo, list, tree, grid row, rich text, numeric, date picker) | 9 | `TextControlBackground*`, `ControlFillColorDefaultBrush` |
| Foreground (form, control, label, text box, grid) | 5 | `TextFillColorPrimaryBrush`; dimmed variants `TextFillColorSecondary/TertiaryBrush` |
| All `*BorderColor` (control, text box, combo, panel, group box, tab, grid, list, tree, split, layout panels, window) | 13+ | `ControlStrokeColorDefaultBrush`, `CardStrokeColorDefaultBrush`, `DividerStrokeColorDefaultBrush` |
| Grid / list headers | 3 | `SubtleFillColorSecondaryBrush` + `TextFillColorPrimaryBrush` |
| Selection / hover pairs per control | 18 | `SubtleFillColorSecondaryBrush`, `ListViewItemBackgroundSelected(PointerOver)`, `TextControlSelectionHighlightColor` |
| Error | 8 | `SystemFillColorCriticalBrush` |
| Warning, success, info | 3 | `SystemFillColorCautionBrush`, `SystemFillColorSuccessBrush`, `SystemFillColorAttentionBrush` |
| Tooltip, context menu, separator | 6 | `ToolTipBackground`, `FlyoutBackground`, `DividerStrokeColorDefaultBrush` |
| Status / tool / menu strip | 3 | `LayerFillColorDefaultBrush` |
| Progress bar | 2 | `AccentFillColorDefaultBrush`, `ControlFillColorSecondaryBrush` |
| Window title bar | 4 | Drawn by `AppWindowTitleBar`; caption foreground from `WindowCaptionForeground` |
| **No equivalent — deleted** | ~110 | Scrollbar parts, `TrackBar` parts, check/radio glyph colours, `MonthCalendar` parts, `PropertyGrid`, `WebBrowser`, `PictureBox`, `SplitContainer` splitter, `TableLayoutPanel` cell borders, per-control border widths, per-state foregrounds. |

Two consequences worth stating plainly:

1. **Per-control border and selection colours have no home.** WinUI derives them from a handful of stroke brushes and control templates. A user who had customised, say, the ComboBox drop-down background cannot carry that forward. This is a capability reduction, not a regression, and it is why the appearance page shrinks to a three-way mode choice.
2. **Dark mode becomes real.** Today it is incidental: there is no OS dark-mode detection anywhere in the application, the shipped themes are both light, and several appliers ignore dark-relevant tokens. Under WinUI 3, `Dark` is a first-class mode and `System` follows Windows.

### 3.3 Grids — the differences that bite

| Current behaviour | Constraint under WinUI 3 |
| :--- | :--- |
| `DataSource = DataTable` | The Toolkit grid binds `IEnumerable`. A `DataTable` must be projected to typed rows. Every grid screen changes its model, not just its markup. |
| `CellFormatting` event | Does not exist. Row colour comes from a style selector bound to the row model, so **the row model must carry whatever decides the colour**. |
| `AutoSizeColumnsMode.Fill` | Replaced by a star-sized `TableViewColumn` width per column. |
| Per-user column state via the existing grid-settings procedure | Payload shape is frozen. The component maps its column descriptors to that payload; it does not invent a new format. |
| Column-header sort over server-paged data | Sorts the page only. The component **does not expose header sorting**; the sort order is stated in the UI. |
| Delete-key interception | Becomes an accelerator with `CanExecute`, so it cannot fire on a screen that has no delete. |

### 3.4 Suggestion behaviour to preserve

- Dropdown opens below the field, flips above when there is no room within the window.
- Type-ahead filters with `%` as a wildcard; results are relevance-ordered.
- Maximum 100 results; configurable minimum input length.
- F4, or Down on an empty field, shows the full list.
- Up/Down/Enter/Escape navigate; Enter accepts, uppercases the value, and moves focus per the field's configured action.
- An exact match suppresses the list.
- No-match policy differs by source: master-data sources warn and clear the field; ERP sources return nothing silently. **This inconsistency is preserved**, because operators have learned it; it is recorded in §4 rather than quietly unified.

### 3.5 State replaced by this spec

| Legacy static | Replacement | Lifetime |
| :--- | :--- | :--- |
| `Model_Application_Variables.User` and four role flags | `ICurrentUserService` | singleton, read-only after sign-in |
| `ColorCodeParts`, `ValidColorCodes`, combo caches | `IMasterDataCache` | singleton |
| `AnimationsEnabled`, `AutoExpandPanels`, `ShowTotalSummaryPanel`, `TransactionPageSize` | `IUserPreferences` | singleton |
| `ThemeName`, `ThemeEnabled`, `ThemeFontSize`, `UserUiColors`, `WipDataGridTheme` | `IThemeService` + `IUserPreferences` | singleton |
| `Service_ErrorHandler.Instance` and its static facades | `IErrorService` | singleton |
| `LoggingUtility`, `Service_Logging.Instance` | `ILogger<T>` | framework |
| `Focus_Utilities._controlsWithActiveHandlers` | *(deleted)* | — |
| `Helper_SuggestionTextBox` static caches and locks | suggestion cache inside `IMasterDataCache` + ERP cache | singleton |

### 3.6 Persistence introduced

| Data | Store | Compatibility |
| :--- | :--- | :--- |
| Appearance mode (`Light`/`Dark`/`System`) | user settings JSON | **new key**; old `Theme_Name` / `Theme_FontSize` are left in place and ignored |
| Auto-expand, summary panel, page size | user settings JSON | same keys as today |
| Shortcut overrides | `usr_user_shortcuts` | unchanged |
| Grid layout | existing grid-settings procedure | unchanged payload shape |
| Print settings | `%APPDATA%\MTM\PrintSettings\{GridName}.json` | **same path and same shape** |
| Log files | Serilog `%LOCALAPPDATA%\MTM_WIP\logs\app-*.txt`, copied to the share | replaces CSV entirely; the share is a copy destination, not the sink (D54) |

No folder is deleted at startup. The current application wipes `%APPDATA%` and `%LOCALAPPDATA%` application folders on every launch; that behaviour is not carried over, because a folder that is wiped cannot hold preferences.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **Print rendering route** | **Resolved (D19)** — HTML/CSS generated from the print document model, printed through WebView2. The model stays the seam so the route can change without touching a screen. |
| 2 | **Custom theme colours are lost.** A user who customised control colours cannot keep them. | Accepted, recorded. If unacceptable, the appearance page grows a palette editor and this spec roughly triples. |
| 3 | **`Theme_FontSize` reaches almost nothing today** — it is written to a font that no applier reads. | v1 does not reintroduce a font-size control; it relies on Windows text scaling. Confirm acceptable. |
| 4 | **Suggestion no-match inconsistency** — master data warns and clears, ERP stays silent. | Preserved as-is. Whether to unify is a product decision, not an implementation one. |
| 5 | **`DataTable`-shaped DAO results.** Most DAOs return `DataTable`, and the Toolkit grid needs typed rows. | Every grid screen needs a row model. `005`–`008` own their own models; this spec owns only the shared grid component and its state. |
| 6 | Whether any screen depends on the deleted `WipDataGridTheme` per-grid theme | Assumed no. If one does, it becomes a grid-level option in the shared component. |

### Assumptions

1. `WinUI.TableView` is acceptable for 12+ screens (`000-pre-foundation` D37). Verified at TASK-046 with the reference grid from `006`.
2. A single `Popup`-based suggestion control can serve both master-data and ERP sources.
3. Windows 10 machines without Mica are acceptable with a solid background fallback.
