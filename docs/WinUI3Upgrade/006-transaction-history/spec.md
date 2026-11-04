# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `006-transaction-history`
**Depends on**: `002-data-access-layer` (DAOs), `003-crosscutting-services-and-theming` (dialogs, errors, dispatch, identity, preferences, shortcuts, shared grid, print/export), `004` (navigation / composition root)
**Unblocks**: `007` (consumes the extracted grid pattern), `008`–`011` (same grid pattern)
**Tasks**: TASK-085 … TASK-096
**Governing rules**: [`../constitution.md`](../constitution.md) §1 (stack), §2 Rules 1, 2, 5, 6, 7, 8, 9, 10, 11, 12; [`../003-crosscutting-services-and-theming/spec.md`](../003-crosscutting-services-and-theming/spec.md) §3.3 (grids), §4.1

---

## 1. Executive Summary & Scope

This spec ports the **Transactions screen** — search, results grid, detail panel, analytics cards, batch-lifecycle tree, admin delete, export and print — to `MTM_WIP.Module_Transactions`. It is also the **reference implementation for the shared grid**: the grid pattern proven here is extracted in TASK-095 and is what `007`–`011` consume, so the other eleven grid screens do not each re-derive how a `WinUI.TableView` binds typed rows, colours rows, and refuses header sorting.

The WinForms host is `Forms\Transactions\Transactions.cs`, a `ThemedForm` at 1159×661 laid out as a title label plus a dock-filled grid panel plus a 207 px search panel, with a 24×24 "?" help button that navigates to help topics `transaction-history` / `transaction-overview`. Four controls compose the screen — `TransactionSearchControl`, `TransactionGridControl`, `TransactionDetailPanel`, `TransactionAnalyticsControl` — coordinated by `Model_Transactions_ViewModel`. `TransactionLifecycleForm` is a modal lifecycle viewer. All five map to WinUI artefacts below.

### In scope

- The transactions page: search bar, results grid, pagination bar, detail panel, analytics card view, and the grid/analytics toggle.
- The batch-lifecycle viewer (modal) with its embedded detail panel and Export/Print/Close.
- Admin-only transaction delete, including the follow-up inventory prompt and the debug-only privilege-cycle button.
- Excel export and the print path through the shared print pipeline from `003`.
- The threading corrections: no `async void`, no `ConfigureAwait(false)` + `Invoke`, no fire-and-forget initialisation, no `Task.Run` for Excel, no `ContinueWith` for print.
- The extracted shared-grid pattern (TASK-095).

### Out of scope

- Any database change. The ~8 procedures consumed here already exist and stay frozen (constitution Core Rule 4).
- The help *rendering* of `transaction-history` / `transaction-overview` — that is `009` (Q4). This spec only preserves the two topic identifiers on the help button.
- Developer Tools, Error Reports, Visual/ERP analytics — other specs.
- The shell and navigation plumbing itself — `004`.

### Success condition

The transactions page renders a typed-row grid with per-type row colour and no header sorting, searches via the preserved dispatch rule, pages at 50 per page in fixed `ReceiveDate DESC` order, shows the twelve-card analytics view, opens the lifecycle tree with split marking, deletes as admin through the confirm→act→follow-up→refresh flow, and exports/prints — all unit-tested without a WinUI thread, with `dotnet build MTM_WIP.sln -c Debug` clean and `dotnet test MTM_WIP.Tests` green.

---

## 2. Legacy Feature Assessment & UI Mapping Table

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `Forms\Transactions\Transactions.cs` — `ThemedForm` 1159×661, title label + grid panel (dock fill) + 207 px search panel | `View_Transactions.xaml` + `ViewModel_TransactionsPage` | One page in the shell's `Frame`. Same visual order: search on top, results/analytics fill, detail beside/below, pagination under the grid. |
| 24×24 "?" help button | Help button bound to a help-navigation command | Carries topic ids `transaction-history` (search/grid) and `transaction-overview` (analytics/lifecycle). Rendering stays in `009`. |
| `Controls\Transactions\TransactionSearchControl.cs` | `ViewModel_TransactionSearch` + search section | Six fields (Part Number uppercased; User blank = all; From Location; To Location; Operation; Notes keyword), IN/OUT/TRANSFER checkboxes all default-on, DateFrom/DateTo pickers, quick ranges Today / Week (Mon–Sun) / Month (default) / Everything / Custom. |
| `Controls\Transactions\TransactionGridControl.cs` | `ViewModel_TransactionResults` + shared grid component | Eight columns, read-only single-select, alternating rows, per-type row colour, pagination bar, **no** column-header sorting, sort order stated in the UI. |
| `Controls\Transactions\TransactionDetailPanel.cs` | `ViewModel_TransactionDetail` + detail section | Eleven caption/value pairs plus Notes; em dash for empty; title carries the transaction ID; View Batch History enabled only when PartID and BatchNumber are both non-empty. |
| `Controls\Transactions\TransactionAnalyticsControl.cs` | `ViewModel_TransactionAnalytics` + card section | Eleven summary cards, no charts, no tables; grid and analytics are mutually exclusive views toggled by one button (`Analytics`/`Grid`). |
| `Forms\Transactions\TransactionLifecycleForm.cs` — modal viewer | `View_TransactionLifecycle.xaml` + `ViewModel_TransactionLifecycle` | Modal document-style secondary window (named here, per constitution Rule 6): TreeView + embedded detail panel + Export/Print/Close. |
| `Model_Transactions_ViewModel` (orchestrator) | `ViewModel_TransactionsPage` (orchestrator) + four child ViewModels | Same split of responsibility; selection flows Results→Detail, Search→Results; grid/analytics exclusivity is a parent view-state property. See plan §2.1. |
| `DataGridView` with `AutoGenerateColumns=false`, `CellFormatting` row colouring, `DataBindingComplete` re-apply | `WinUI.TableView` bound to `IEnumerable<Model_TransactionRow>` with a row-style selector | The grid binds `IEnumerable`, not `DataTable`; there is no `CellFormatting`, so the **row model carries the transaction type** and the style selector maps it to a brush. No `VirtualMode`, no context menu, no cell painting. This is a **new build** on `TableView`, not a port of the archived Toolkit `DataGrid` (`000-pre-foundation` D37). |
| Column-header sort (sorts the current page only) | **Removed**; sort order text | Header sorting over server-paged data is a usability trap. The grid does not expose it; the fixed order is displayed. |
| `Helper_ExportManager` / ClosedXML export, default `Transactions_yyyyMMdd_{user}.xlsx` | `Service_TransactionExport` | Same 12 columns in order: ID, Type, Batch, Part, From, To, Operation, Quantity, User, ItemType, Date, Notes. |
| `Core_TablePrinter` / `PrintForm` (GDI+) | Shared print pipeline from `003` | Same document model: logo, title, timestamp, watermark, column widths, per-row colour, footer, page boundaries. |

### Procedures in scope (frozen; consumed via `002`'s DAOs)

| Procedure | Feature | DAO |
| :--- | :--- | :--- |
| `inv_transactions_Search` | Basic search (1 / all-3 / 0 types) | `IDao_Transactions` |
| `inv_transactions_SmartSearch` | Smart search (exactly 2 types) | `IDao_Transactions` |
| `inv_transactions_GetBatchLifecycle` | Lifecycle tree | `IDao_Transactions` |
| `inv_transactions_GetAnalytics` | Analytics cards (10 result sets) | `IDao_Transactions` |
| `inv_transaction_Delete_ByID` | Admin delete | `IDao_Transactions` |
| `inv_inventory_Get_ByPartID` | Admin-delete follow-up (related inventory) | `IDao_Inventory` — *(to confirm)* |
| `sys_GetRoleIdByName`, `sys_user_roles_Update` | Debug-only privilege-cycle button | `IDao_System` / `IDao_User` |

---

## 3. Behavioral Edge Cases & State Constraints

> **Screen constraints** (`000-pre-foundation` D58, D59): every screen must be fully usable at **1366×768 with 100 % DPI**, and every interactive control carries `AutomationProperties.Name` matching its visible label plus a stable `AutomationId`. High contrast and text scaling are out of scope by decision.

### 3.1 Search semantics

- **Six criteria boxes.** Part Number is uppercased on commit. User is blank = "all users". From Location, To Location, Operation and Notes are keyword fields.
- **Type checkboxes** IN / OUT / TRANSFER are all checked by default. At least one must remain checked: `IsValid()` rejects zero selected types.
- **Quick ranges.** Today; Week (Monday–Sunday); **Month (default)**; Everything (`now-10y .. now+1y`); Custom (enables the two pickers). Any non-Custom range disables the pickers.
- **Inclusive upper bound.** `BuildCriteria` sets `DateTo = DateTo.Date.AddDays(1).AddSeconds(-1)` — the search window includes the whole end day.
- **Range validation.** `IsDateRangeValid()` is checked on every range **except** Everything.
- **No quantity or item-type filter** is exposed, even though the DAO accepts them. This omission is preserved.

### 3.2 Procedure dispatch — must be preserved exactly

| Selected types | Procedure | Notes |
| :--- | :--- | :--- |
| 0 (none) | `inv_transactions_Search` | Normally unreachable because `IsValid` requires ≥1 type. |
| 1 | `inv_transactions_Search` | |
| **2** | `inv_transactions_SmartSearch` | The WHERE clause is built in C# with a `StringBuilder` and passed as `SearchTerm`. |
| 3 (all) | `inv_transactions_Search` | |

### 3.3 Paging and count

- Page size comes from `IUserPreferences.TransactionPageSize` (default **50**); page numbers are **1-based**.
- Sort is **fixed** to `ReceiveDate DESC`; there is no user-selectable sort.
- **The total count is unreliable.** It is parsed out of the status message text (`"Found X transaction(s)…"`), and the SmartSearch path returns no count, falling back to `RowsAffected > 0 ? count : count`. The pagination bar displays it as best-effort; the Next button must not depend solely on it. See §4.3.

### 3.4 Grid rendering

- `AutoGenerateColumns=false`. Columns and geometry:

| Column | Width | Alignment / behaviour |
| :--- | :--- | :--- |
| ID | 80 | right |
| Type | 100 | left |
| Part Number | 150, Fill, min 100 | Star-sized `TableViewColumn` + `MinWidth=100` |
| Qty | 80 | right |
| From | 120 | left |
| To | 120 | left |
| User | 100 | left |
| Date/Time | 140 | `MM/dd/yy HH:mm` |

- Read-only, full-row select, `MultiSelect=false`, no add/delete/resize rows, `AllowUserToOrderColumns=true`. Alternating-row brush (legacy `#F8FAFC`) becomes a ThemeResource. **No** `VirtualMode`, **no** context menu, **no** cell painting.
- **Row colour by type**: IN → green, TRANSFER → yellow, OUT → red, with a darkened selection variant. Because the Toolkit grid has no `CellFormatting`, the row model (`Model_TransactionRow`) carries `Enum_TransactionType`, and the grid's `IRowStyleSelector` maps the enum to a `ThemeResource` brush. Re-application after sort/data-load is unnecessary — the selector runs per row on every layout.
- **Header sorting is removed**; the fixed sort order is stated in the UI.
- **Pagination bar**: Prev / Next / GoToPage textbox / page indicator / record count.

### 3.5 Detail panel

- Eleven caption/value pairs: ID, Type, Item Type, Part Number, Batch, Quantity, From, To, Operation, User, Date/Time (`MM/dd/yyyy HH:mm:ss`) — plus Notes.
- Empty values render as an em dash.
- The panel title (group title) carries the transaction ID.
- **View Batch History** is enabled only when both `PartID` and `BatchNumber` are non-empty.

### 3.6 Lifecycle tree

- `inv_transactions_GetBatchLifecycle` returns every transaction for the batch in chronological order.
- `BuildLifecycleTree`:
  1. First row = the root node.
  2. Node text = `"{Type} - {From} -> {To} (Qty: N)"`; node `Tag` = the transaction.
  3. Each subsequent row finds its parent by matching the row's `FromLocation` to a prior node's `ToLocation`.
  4. **Split detection**: a per-location remaining-quantity counter is tracked; when a TRANSFER's quantity is less than the remaining quantity at that location, the node is marked with the orange/attention brush. Remaining quantity is decremented by the transferred amount.
  5. `ExpandAll`, then select the first node.
- Selection loads that transaction into the embedded detail panel.
- Export builds a `DataTable` with columns Type / Date / From / To / Qty / User / Notes.

### 3.7 Analytics

- Eleven summary cards, no charts, no tables. Metrics: Total; IN (+%); OUT (+%); TRANSFER (+%); Database Lifespan (+Avg Daily); Most Active User; Most Transacted Part; Busiest Location; Most Transferred Part; Transaction Rate (+trend); Busiest Day; Peak Hour (12-hour format).
- Values format `N0`; percentages format `0.0%`; empty values render as an em dash.
- Data comes from `inv_transactions_GetAnalytics` through `ExecuteDataSetWithStatusAsync`, which must return **exactly 10 result sets in this order**:

| # | Result set |
| :--- | :--- |
| 1 | Counts by type |
| 2 | Quantities by type |
| 3 | First / last date |
| 4 | Top user |
| 5 | Top part |
| 6 | Busiest location |
| 7 | Most transferred part |
| 8 | Busiest weekday |
| 9 | Peak hour |
| 10 | Rate / trend |

- The date range is **hard-coded** to `DateTime.Now.AddYears(-10) .. Now.AddDays(1)`, with the TODO carried over in code.
- Grid and analytics are mutually exclusive: one button toggles the label `Analytics` / `Grid`.

### 3.8 Admin delete

- Trigger: Delete key or the `Shortcut_Transactions_Delete` chord. Requires a selected row; requires admin role.
- Flow: confirmation → `DeleteTransactionByIdAsync` per row → a follow-up prompt listing up to 10 related inventory items → re-run the search on the same page.
- Hidden entirely for non-admins (the accelerator's `CanExecute` is false, not just an invisible button).
- **Debug builds only**: a privilege-cycle button using `sys_GetRoleIdByName` + `sys_user_roles_Update`. It must not exist in release.

### 3.9 Export and print

- Export: file picker with default `Transactions_yyyyMMdd_{user}.xlsx`; 12 columns ID / Type / Batch / Part / From / To / Operation / Quantity / User / ItemType / Date / Notes; via ClosedXML.
- Print: through the shared print pipeline from `003`, fed by a document model carrying per-row colour, logo, title, timestamp, watermark, footer and page boundaries.

### 3.10 Threading corrections (defects fixed, not carried)

| Legacy defect | WinUI 3 replacement |
| :--- | :--- |
| `async void` event handlers | `[RelayCommand]` / `AsyncRelayCommand`; handlers only forward to the ViewModel. |
| `ConfigureAwait(false)` then `this.Invoke(...)` | No `ConfigureAwait(false)` in ViewModels; marshal via `IDispatcherService.TryEnqueue`. |
| `_ = InitializeAsync()` fire-and-forget from the constructor | `Loaded`/`OnNavigatedTo` forwards to `InitializeAsync` awaited by the framework. |
| `Task.Run` for the Excel write | `ClosedXML` async save inside the export service, awaited. |
| Print via `ContinueWith` (not awaited) | Await the print pipeline. |

### 3.11 State replaced by this spec

| Legacy static / member | Replacement | Owner |
| :--- | :--- | :--- |
| `Model_Application_Variables.TransactionPageSize` | `IUserPreferences.TransactionPageSize` | `003` |
| `Model_Application_Variables.User` + role flags | `ICurrentUserService.IsInRole(...)` | `003` |
| `Model_Transactions_ViewModel` static / singleton orchestration | Injected `ViewModel_TransactionsPage` + children | this spec |

### 3.12 Persistence touched

- Grid layout state uses the existing grid-settings procedure (`usr_dgv_settings`), payload shape frozen — the shared component owns it; this module supplies column descriptors.
- Print settings stay at `%APPDATA%\MTM\PrintSettings\{GridName}.json` (grid name `Transactions` and the lifecycle grid name), same path and shape as today.
- No new table, column, procedure, file or folder is introduced.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | Analytics card count | **Resolved** — the designer labels confirm **12** distinct metric blocks: Total, IN, OUT, TRANSFER, Database Lifespan (+Avg Daily), Most Active User, Most Used Part, Transaction Rate (+Trend), Busiest Day, Peak Hour Activity, Busiest Location, Most Moved Part. The "11 cards" figure in earlier notes was a miscount of the layout, not a grouping of two metrics. TASK-090 lays out 12. |
| 2 | Grid/detail date formats are fixed strings (`MM/dd/yy HH:mm`, `MM/dd/yyyy HH:mm:ss`) which conflict with constitution Rule 12 (current UI culture). | *(to confirm)* — keep the fixed en-US formats, or express them culture-aware with the US format as default. |
| 3 | With an unreliable total count, the exact has-next heuristic for the Next button is unspecified in legacy (it used the parsed count). | *(to confirm)* — plan adopts "Next enabled when the returned page is full (rows == pageSize) or the parsed total exceeds page × pageSize". |
| 4 | `BuildLifecycleTree`'s fallback when a row's `FromLocation` matches no prior node's `ToLocation` (e.g., an IN after a gap) is not explicitly documented. | *(to confirm)* — assumed to attach at root level. |
| 5 | Whether admin delete appends to the append-only `IDao_TransactionHistory`, and which procedure `inv_inventory_Get_ByPartID`-style lookup feeds the follow-up prompt. | *(to confirm)* — `inv_inventory_Get_ByPartID` is assumed for the follow-up list. |
| 6 | The help button's navigation mechanism is Q4, owned by `009`. `004`'s navigation contract (`INavigationService.NavigateTo<TViewModel>()`) is written and is what this screen uses. | *(to confirm)* — this spec only preserves the two topic ids. |
| 7 | Whether the print document uses the 8 grid columns or the 12 export columns (legacy `PrintForm` printed the visible grid). | *(to confirm)* — plan assumes the grid's 8 columns; lifecycle print uses its 7-column export set. |
| 8 | The top-level `docs/WinUI3Upgrade/tasks.md` still carries the legacy flat numbering (its `TASK-085..096` differ from this spec's). | *(to confirm)* — the flat list is superseded by the per-spec numbering; it must be regenerated from the sub-spec task ranges. |

### Assumptions

1. `004` provides navigation and DI composition, so `View_Transactions` and the lifecycle window can be registered and reached; the exact navigation interface is *(to confirm)*.
2. `IDao_Transactions` exposes `SearchAsync`, `SmartSearchAsync`, `GetBatchLifecycleAsync`, `GetAnalyticsAsync` and `DeleteByIdAsync` (all ported in `002` TASK-025).
3. The shared grid component (`003` TASK-046) accepts a typed row model plus an `IRowStyleSelector`, and owns per-user column state.
4. The print pipeline (`003` TASK-047) accepts a document model built from typed rows, including per-row colour.
5. `IUserPreferences.TransactionPageSize` exists with default 50 and is read-only for this module.
