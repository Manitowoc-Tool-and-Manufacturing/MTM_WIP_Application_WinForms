# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `006-transaction-history`
**Tasks**: TASK-085 … TASK-096

---

## 1. Project Topology & Directory Mapping

All of this spec lands in **`MTM_WIP.Module_Transactions`**, with one artefact extracted into **`MTM_WIP.Shared`** (TASK-095). The module project already exists (`001` TASK-005); `002` provides the DAOs and `003` provides the services and the shared grid component this module consumes.

```text
MTM_WIP.Module_Transactions/
├── Views/
│   ├── View_Transactions.xaml(.cs)            # page: search bar, grid/analytics toggle, detail, pagination
│   └── View_TransactionLifecycle.xaml(.cs)    # modal lifecycle viewer (named secondary window)
├── ViewModels/
│   ├── ViewModel_TransactionsPage.cs          # orchestrator (child VM host, view-state toggle)
│   ├── ViewModel_TransactionSearch.cs
│   ├── ViewModel_TransactionResults.cs
│   ├── ViewModel_TransactionDetail.cs
│   ├── ViewModel_TransactionAnalytics.cs
│   └── ViewModel_TransactionLifecycle.cs
├── Models/
│   ├── Model_TransactionCriteria.cs           # six fields + type flags + date range
│   ├── Model_TransactionRow.cs                # typed grid row; carries Enum_TransactionType (row colour)
│   ├── Model_TransactionDetail.cs
│   ├── Model_TransactionAnalyticsSummary.cs
│   └── Model_TransactionLifecycleNode.cs
├── Enums/
│   ├── Enum_TransactionType.cs                # IN, OUT, TRANSFER
│   └── Enum_TransactionDateRange.cs           # Today, Week, Month, Everything, Custom
├── Converters/
│   └── EmptyToEmDashConverter.cs              # em-dash placeholders (detail + analytics)
├── Services/
│   ├── Service_TransactionExport.cs           # ClosedXML export + print document model
│   └── Service_TransactionAnalyticsReader.cs  # 10-result-set mapping
├── Helpers/
│   └── Helper_TransactionCriteriaBuilder.cs   # BuildCriteria + dispatch rule + lifecycle tree build
└── DependencyInjection/
    └── TransactionModuleServiceRegistration.cs

MTM_WIP.Shared/                                  # TASK-095 — extracted, module-agnostic
├── Controls/DataGrid/
│   ├── GridColumnDescriptor.cs                 # name, width/star, alignment, format, min-width
│   ├── GridConfiguration.cs                    # read-only, single-select, no header sort, pagination bar
│   └── IRowStyleSelector.cs                    # row model → ThemeResource brush key
└── ViewModels/
    └── PagedResultsViewModelBase.cs            # paging contract, unreliable-count handling
```

`MTM_WIP.Module_Transactions` references `Core`, `Shared`, `Data` only (constitution Core Rule 10). It never references another module, the WinForms solution, or the stored-procedure helper directly — all data flows through `IDao_*`.

---

## 2. Component Migration Strategy

### 2.1 ViewModel shape — one orchestrator, four children, one dialog

**Chosen: a page-level `ViewModel_TransactionsPage` that hosts four child ViewModels (`Search`, `Results`, `Detail`, `Analytics`) plus a separate `ViewModel_TransactionLifecycle`.**

Justification:

- It mirrors the legacy `Model_Transactions_ViewModel`, which already orchestrates four controls, so the responsibility split is a 1:1 port rather than a redesign.
- Each child is unit-testable in isolation (constitution §4) — the search state, the current page, the selected row, and the analytics summary have independent lifetimes and failure modes.
- The grid/analytics mutual exclusion is a single parent view-state property (`ViewMode` = `Grid` | `Analytics`), not a hidden cross-binding between two children.
- Selection flows are explicit: `Search → Results` (one direction, via a command argument), `Results → Detail` (a selected-row property). No child reaches into another child.

Communication: `ViewModel_TransactionsPage` injects the four children; `Results.SelectedRow` raises `Detail.Load(transaction)`; `Search.Criteria` raises `Results.SearchAsync(criteria, page, token)`; the parent exposes a `ToggleView` command that flips `ViewMode` and re-runs the analytics load on entry.

### 2.2 Search — criteria model and builder

`Model_TransactionCriteria` holds the six fields, the three type flags, the date-range enum, and the two picker values. `Helper_TransactionCriteriaBuilder.BuildCriteria` applies:

- Part Number `.ToUpperInvariant()`.
- User: empty string = all users (no filter passed).
- `DateTo = DateTo.Date.AddDays(1).AddSeconds(-1)`.
- Range defaults and picker enable/disable (`Month` default; `Custom` enables the pickers; others disable them).
- Validation: `IsValid()` (≥1 type selected), `IsDateRangeValid()` (skipped for `Everything`).

### 2.3 Search dispatch — exact table

`Helper_TransactionCriteriaBuilder` (or the Results ViewModel) selects the procedure:

| Selected types | Method | Procedure |
| :--- | :--- | :--- |
| 0 | `SearchAsync` | `inv_transactions_Search` |
| 1 | `SearchAsync` | `inv_transactions_Search` |
| 2 | `SmartSearchAsync` | `inv_transactions_SmartSearch` — C# `StringBuilder` WHERE passed as `SearchTerm` |
| 3 | `SearchAsync` | `inv_transactions_Search` |

The count of selected types is computed from the three flags; the dispatch is a pure function and is unit-tested for all four inputs.

### 2.4 Paging contract

- `pageSize` ← `IUserPreferences.TransactionPageSize` (default 50). `page` is 1-based.
- Request: `{criteria, page, pageSize}`, sort fixed `ReceiveDate DESC` (not a parameter the user can change).
- Count: parsed from the status message, unreliable; the SmartSearch path falls back to `RowsAffected > 0 ? count : count`. The pagination bar shows the parsed count but the **Next** button uses the "page is full" heuristic: enabled when `rowsReturned == pageSize` or the parsed total exceeds `page × pageSize`. Prev enabled when `page > 1`. GoToPage clamps to `[1, max(1, ceil(parsedTotal/pageSize))]` when a parsed total exists, else is only upper-bounded by the page that stopped returning full pages.

### 2.5 Grid — typed rows, not DataTable

The shared grid component binds `IEnumerable<Model_TransactionRow>`. `Model_TransactionRow` carries `Enum_TransactionType`, so **row colour comes from the row model**: the module registers an `IRowStyleSelector` mapping `IN → TransactionRowInBrush`, `TRANSFER → TransactionRowTransferBrush`, `OUT → TransactionRowOutBrush` (ThemeResource keys with light/dark variants, constitution Rule 9), plus a darkened selected variant and an alternating-row brush (legacy `#F8FAFC` becomes a resource).

Column descriptors: ID 80 right; Type 100; Part Number `Star` + `MinWidth 100`; Qty 80 right; From 120; To 120; User 100; Date/Time 140 format `MM/dd/yy HH:mm`. Grid flags: read-only, full-row select, `MultiSelect=false`, no add/delete/resize rows, allow reorder, no header sorting, sort order label in the UI, pagination bar below.

### 2.6 Detail panel

`ViewModel_TransactionDetail` exposes eleven labelled values + Notes, each rendered through `EmptyToEmDashConverter` (em dash when empty). Title = transaction ID. `ViewBatchHistoryCommand` `CanExecute` requires both `PartID` and `BatchNumber` non-empty; it opens the lifecycle viewer for that batch.

### 2.7 Analytics

`Service_TransactionAnalyticsReader` calls `inv_transactions_GetAnalytics` via `ExecuteDataSetWithStatusAsync` and asserts `DataSet.Tables.Count == 10`, then maps by index (not by name) per spec §3.7. `Model_TransactionAnalyticsSummary` holds the twelve metrics with format rules `N0` / `0.0%` / em dash. Date range hard-coded `now-10y .. now+1d` with a TODO. `ViewModel_TransactionsPage.ViewMode` enforces mutual exclusion; entering Analytics triggers the load.

### 2.8 Lifecycle tree algorithm

`ViewModel_TransactionLifecycle`:

1. `GetBatchLifecycleAsync(batch)` → chronological rows.
2. `BuildLifecycleTree(rows)`:
   - Row 0 = root node; node text `"{Type} - {From} -> {To} (Qty: N)"`; `Tag` = transaction.
   - Maintain `Dictionary<string,int> remainingByLocation` (remaining quantity per location) and the ordered list of added nodes.
   - For each subsequent row, find its parent as the **last added node whose `ToLocation` equals this row's `FromLocation`**; if none, attach at root level *(to confirm)*.
   - For TRANSFER rows, if `quantity < remainingByLocation[FromLocation]`, mark the node with the attention/orange brush (split). Decrement `remainingByLocation[FromLocation]` by the quantity; for IN rows, set/accumulate the location's remaining quantity.
   - `ExpandAll`; select the first node.
3. `SelectedNode` → load the tagged transaction into the embedded detail panel.
4. Export builds a `DataTable` (Type/Date/From/To/Qty/User/Notes); Print feeds the same rows through the `003` pipeline.

### 2.9 Admin delete

Accelerator bound to `DeleteCommand` whose `CanExecute` requires a selected row **and** `ICurrentUserService.IsInRole(Admin)`. Flow: `IDialogService` confirmation → per-row `DeleteByIdAsync` → follow-up prompt listing up to 10 related inventory items from `IDao_Inventory` → re-run `SearchAsync` on the current page. The debug-only privilege-cycle button is compiled under `#if DEBUG` and uses `IDao_System`/`IDao_User` with `sys_GetRoleIdByName` + `sys_user_roles_Update`.

### 2.10 Export and print

`Service_TransactionExport` builds a 12-column worksheet (ID/Type/Batch/Part/From/To/Operation/Quantity/User/ItemType/Date/Notes) with the grey bold header rule from `003`, default filename `Transactions_yyyyMMdd_{user}.xlsx`, saved via ClosedXML async save. Print builds a document model (8 grid columns) and hands it to the `003` print pipeline, awaited.

### 2.11 Threading rules applied

Enforced by TASK-094 with a grep gate: no `async void`, no `ConfigureAwait`, no `Task.Run`, no `.ContinueWith(`, no `_ = InitializeAsync(` anywhere in the module. ViewModels marshal through `IDispatcherService`.

### 2.12 Grid extraction (TASK-095)

The module-agnostic pieces — `GridColumnDescriptor`, `GridConfiguration`, `IRowStyleSelector`, `PagedResultsViewModelBase` — move to `MTM_WIP.Shared`. The transactions module keeps only its own row model, its style-selector mapping, and its column descriptors. `007`/`008` then consume the shared component; this is what makes `006` the reference grid.

---

## 3. Data & State Migration

### 3.1 Static state eliminated

| Legacy | Replacement | Owner |
| :--- | :--- | :--- |
| `Model_Application_Variables.TransactionPageSize` | `IUserPreferences.TransactionPageSize` (default 50) | `003` |
| `Model_Application_Variables.User`, `UserTypeAdmin` etc. | `ICurrentUserService.IsInRole(...)` | `003` |
| Grid row-colour selection in `Service_DataGridView` | Row-style selector over the typed row model | `003` + this module |
| GDI+ print (`Core_TablePrinter`) | print document model + renderer | `003` |

### 3.2 DAO surface consumed (all from `002`, frozen procedures)

| Feature | DAO method (assumed name) | Procedure |
| :--- | :--- | :--- |
| Basic search | `SearchAsync` | `inv_transactions_Search` |
| Smart search | `SmartSearchAsync` | `inv_transactions_SmartSearch` |
| Lifecycle | `GetBatchLifecycleAsync` | `inv_transactions_GetBatchLifecycle` |
| Analytics | `GetAnalyticsAsync` | `inv_transactions_GetAnalytics` |
| Admin delete | `DeleteByIdAsync` | `inv_transaction_Delete_ByID` |
| Delete follow-up | `GetByPartIdAsync` *(to confirm)* | `inv_inventory_Get_ByPartID` |
| Debug privilege cycle | `GetRoleIdByNameAsync` / `UpdateUserRoleAsync` | `sys_GetRoleIdByName` / `sys_user_roles_Update` |

### 3.3 Persistence touched

- Grid layout state: existing grid-settings procedure, payload shape frozen, owned by the shared component.
- Print settings: `%APPDATA%\MTM\PrintSettings\Transactions.json` (and the lifecycle grid), same path and shape.
- No new table, column, procedure, file or folder. Nothing is deleted at startup (the WinForms launch-wipe is not ported — `003` §3.6).

### 3.4 Result and error handling

- Every DAO call returns `Model_Result<T>`; success is `IsSuccess` (`StatusCode >= 0`), and `0` ("no rows") is success, not an error.
- Failures surface through `IErrorService`; no ViewModel builds a dialog or logs directly (constitution Rule 5).
- The unreliable count is displayed, never used as a correctness gate.
