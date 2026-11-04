# Migration Task List (Execution Plan)

**Spec**: `006-transaction-history`
**Range**: TASK-085 … TASK-096
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Ordering is load-bearing.** Models and enums precede the search builder (TASK-085 → 086); the dispatch and paging contract precede the grid (087 → 088); the grid is proven before its pattern is extracted (088 → 095). The threading sweep (094) runs after export/print because those are the offenders.

---

## Phase 1 — Models and module shape

- [ ] **TASK-085** — Create the module's models and enums in `MTM_WIP.Module_Transactions`: `Enum_TransactionType` (IN/OUT/TRANSFER), `Enum_TransactionDateRange` (Today/Week/Month/Everything/Custom), `Model_TransactionCriteria` (six fields, three type flags default-on, date-range enum, picker values), `Model_TransactionRow` (typed grid row carrying `Enum_TransactionType` — the row-colour discriminator), `Model_TransactionDetail`, `Model_TransactionAnalyticsSummary`, `Model_TransactionLifecycleNode`, and `TransactionModuleServiceRegistration`.
  - **Verify**: `dotnet build` is clean; a unit test asserts `Model_TransactionCriteria` defaults (three type flags true, range `Month`) and that `Model_TransactionRow.Type` round-trips the enum.

---

## Phase 2 — Search, dispatch and paging

- [ ] **TASK-086** — Implement `ViewModel_TransactionSearch` and `Helper_TransactionCriteriaBuilder.BuildCriteria`: six criteria (Part Number uppercased on commit; User blank = all; From/To Location; Operation; Notes keyword), IN/OUT/TRANSFER checkboxes default-on, quick ranges Today / Week (Mon–Sun) / Month (default) / Everything (`now-10y..now+1y`) / Custom, and `DateTo = DateTo.Date.AddDays(1).AddSeconds(-1)`. Non-Custom ranges disable the pickers.
  - **Verify**: unit tests — Month is default; a non-Custom range disables the pickers; `BuildCriteria` applies the inclusive end-of-day upper bound; `IsValid()` rejects zero selected types; `IsDateRangeValid()` is skipped for `Everything`.

- [ ] **TASK-087** — Implement the search dispatch and the paging contract in `ViewModel_TransactionResults`: exactly 2 of 3 selected types → `IDao_Transactions.SmartSearchAsync` (`inv_transactions_SmartSearch`, C# `StringBuilder` WHERE passed as `SearchTerm`); 0/1/3 → `SearchAsync` (`inv_transactions_Search`); page size from `IUserPreferences.TransactionPageSize` (default 50); 1-based page; sort fixed `ReceiveDate DESC`; count parsed from the status message and treated as unreliable.
  - **Verify**: a table-driven unit test asserts the selected procedure for 0, 1, 2 and 3 types; a test asserts the SmartSearch path forwards the WHERE as `SearchTerm`; a test asserts `page` and `pageSize` are forwarded 1-based with the fixed sort.

---

## Phase 3 — Grid and detail

- [ ] **TASK-088** — Build the grid section of `View_Transactions` and complete `ViewModel_TransactionResults` over the shared grid component: 8 columns (ID 80 right; Type 100; Part Number `Star` + `MinWidth 100`; Qty 80 right; From 120; To 120; User 100; Date/Time 140 `MM/dd/yy HH:mm`), read-only full-row single-select, alternating-row brush, per-type row colour via the row model (`IN`/`TRANSFER`/`OUT` → green/yellow/red with darkened selection), **no** header sorting (sort order stated in the UI), and the pagination bar (Prev / Next / GoToPage / page indicator / record count).
  - **Verify**: a unit test asserts the `IRowStyleSelector` maps each enum to its brush key; `grep` of the view finds no `SortMemberPath` and no column-header sort handler; the view binds `IEnumerable<Model_TransactionRow>` (not `DataTable`).

- [ ] **TASK-089** — Build the detail section and `ViewModel_TransactionDetail`: eleven caption/value pairs (ID, Type, Item Type, Part Number, Batch, Quantity, From, To, Operation, User, Date/Time `MM/dd/yyyy HH:mm:ss`) plus Notes; empty values render as an em dash via `EmptyToEmDashConverter`; the title carries the transaction ID; `ViewBatchHistoryCommand` enabled only when `PartID` and `BatchNumber` are both non-empty.
  - **Verify**: a unit test renders a detail with empty fields as em dashes; `ViewBatchHistoryCommand.CanExecute` is true only with both `PartID` and `BatchNumber` set.

---

## Phase 4 — Analytics and lifecycle

- [ ] **TASK-090** — Build the analytics card view and `ViewModel_TransactionAnalytics`: **twelve** cards (Total; IN/OUT/TRANSFER +%; Database Lifespan +Avg Daily; Most Active User; Most Used Part; Busiest Location; Most Moved Part; Transaction Rate +trend; Busiest Day; Peak Hour 12-hour), `N0` / `0.0%` / em-dash formatting, fed by `inv_transactions_GetAnalytics` through `ExecuteDataSetWithStatusAsync` requiring exactly 10 result sets in the fixed order (counts, quantities, first/last date, top user, top part, busiest location, most transferred part, busiest weekday, peak hour, rate/trend); date range hard-coded `now-10y..now+1d` with a TODO; grid/analytics mutually exclusive via the parent view-state toggle.
  - **Verify**: a fixture data set maps to the expected twelve displayed values with correct formatting; switching to Analytics hides the grid and loads once; switching back restores the previous page without a re-query.
  - **Verify**: a test with a 10-result-set fixture asserts each card reads its result-set index and formats `N0` / `0.0%` / em dash; a parent test asserts `ViewMode` flips between `Grid` and `Analytics`.

- [ ] **TASK-091** — Build `View_TransactionLifecycle` (modal) and `ViewModel_TransactionLifecycle`: TreeView + embedded detail panel + Export/Print/Close; `GetBatchLifecycleAsync` returns the batch's transactions chronologically; `BuildLifecycleTree` — first row is root, node text `"{Type} - {From} -> {To} (Qty: N)"`, tag carries the transaction, parent found by matching a row's `FromLocation` to a prior node's `ToLocation`, split marked with the attention/orange brush when a TRANSFER quantity is below the per-location remaining quantity; `ExpandAll` then select the first node; selection loads the transaction into the detail panel; Export builds a `DataTable` (Type/Date/From/To/Qty/User/Notes).
  - **Verify**: a unit test builds a tree from a synthetic batch including a split and asserts parent/child links, node text, split flags and first-node selection; an export test asserts the 7-column table.

---

## Phase 5 — Delete, export/print and threading sweep

- [ ] **TASK-092** — Implement admin transaction delete: Delete-key / `Shortcut_Transactions_Delete` accelerator with `CanExecute` requiring a selected row and admin role; confirmation via `IDialogService`; `DeleteByIdAsync` per row; a follow-up prompt listing up to 10 related inventory items from `IDao_Inventory`; then re-run the search on the same page; hidden entirely for non-admins; debug-only (`#if DEBUG`) privilege-cycle button using `sys_GetRoleIdByName` + `sys_user_roles_Update`.
  - **Verify**: `CanExecute` is false without a selection and false for a non-admin; a test with mocked DAOs asserts delete → follow-up → re-search ordering; a release build omits the privilege button.

- [ ] **TASK-093** — Wire export and print: Export via file picker with default `Transactions_yyyyMMdd_{user}.xlsx`, 12 columns ID/Type/Batch/Part/From/To/Operation/Quantity/User/ItemType/Date/Notes, through ClosedXML; Print through the shared `003` print pipeline with per-row colour, logo, title, timestamp, watermark, footer and page boundaries.
  - **Verify**: an export test produces a 12-column workbook in order with the default filename pattern; a print-model test asserts the document model carries each row's colour.

- [ ] **TASK-094** — Sweep the module for the legacy threading patterns: no `async void` handlers, no `ConfigureAwait(false)` followed by `Invoke`/`BeginInvoke` (marshal via `IDispatcherService`), no fire-and-forget `_ = InitializeAsync()` from constructors, no `Task.Run` for the Excel write (async save), and print awaited rather than `ContinueWith`.
  - **Verify**: `git grep` over `MTM_WIP.Module_Transactions` returns zero hits for `async void`, `ConfigureAwait`, `Task.Run`, `.ContinueWith(`, and `_ = InitializeAsync(`.

---

## Phase 6 — Grid extraction and downstream validation

- [ ] **TASK-095** — Extract the grid pattern proven here — `GridColumnDescriptor`, `GridConfiguration` (read-only, single-select, no header sort, pagination bar), `IRowStyleSelector`, `PagedResultsViewModelBase` — into `MTM_WIP.Shared`, leaving the transactions module as the reference consumer that supplies only its row model, style-selector mapping and column descriptors.
  - **Verify**: the shared component is exercised by a second consumer test with a different row model; `007` and `008` reference the shared component, not a module-private grid.

- [ ] **TASK-096** — **Downstream validation: `007-settings-and-administration` (and a sweep of `008`–`011`).**

  Re-read [`../007-settings-and-administration/spec.md`](../007-settings-and-administration/spec.md), [`plan.md`](../007-settings-and-administration/plan.md) and [`tasks.md`](../007-settings-and-administration/tasks.md), then sweep `008`–`011`. Record the assumptions this spec imposes on those specs:

  1. **The shared grid component (TASK-095) is the only grid.** `007`–`011` supply their own typed row models and style selectors and must not reintroduce column-header sorting.
  2. **`003` services are resolved, not redeclared.** `IDialogService`, `IErrorService`, `IDispatcherService`, `ICurrentUserService`, `IUserPreferences`, `IShortcutService` are registered once and available to every module ViewModel.
  3. **DAOs are instance classes behind interfaces.** Master-data, user and settings pages call `IDao_*`; no ViewModel touches the stored-procedure helper directly.
  4. **`Model_Result<T>.IsSuccess` is the only success signal** and `StatusCode == 0` ("no rows") is success, not an error.
  5. **Role checks are per-action** (`ICurrentUserService.IsInRole` at `CanExecute`), and destructive actions follow confirm → act → follow-up → refresh (the TASK-092 pattern).
  6. **The threading rules hold everywhere**: no `async void`, no `ConfigureAwait(false)` in ViewModels, no `Task.Run`, no `.ContinueWith`, no fire-and-forget initialisation.

  If any item is false in `007`, fix **this** spec before proceeding; do not amend `007` to work around a defect here. Append a `## Drift log` to `007`'s `tasks.md` with these six items as `*(pending)*` rows.

  - **Verify**: a written `## Drift log` exists in `../007-settings-and-administration/tasks.md` with the six items above as `*(pending)*` or `*confirmed*` rows, and the full `dotnet test MTM_WIP.Tests` suite is green.

---

## Drift log

Filled in by TASK-096.

| Checked spec | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `007` | Shared grid component is the only grid; typed rows + selector, no header sort | *(pending)* | — |
| `007` | `003` services resolved, not redeclared | *(pending)* | — |
| `007` | DAOs instance-based behind interfaces; no direct helper use | *(pending)* | — |
| `007` | `IsSuccess` is the only success signal; status `0` = success | *(pending)* | — |
| `007` | Per-action role checks; confirm → act → follow-up → refresh | *(pending)* | — |
| `007`–`011` | Threading rules: no async void / ConfigureAwait / Task.Run / ContinueWith | *(pending)* | — |
| `008`–`011` | Grid pattern reuse; no module-private grid fork | *(pending)* | — |

## Known deviations from the WinForms behaviour

Recorded so a reviewer does not mistake them for defects.

| Deviation | Rationale | Reviewed by |
| :--- | :--- | :--- |
| Column-header sorting is **removed** | Over server-paged data it sorted only the current page; the sort order is stated in the UI instead | *(this spec)* |
| Grid binds **typed row models**, not `DataTable` | `TableView` binds `IEnumerable`; row colour moves to the row model + a style selector | *(this spec)* |
| Row/alternating colours become **ThemeResource brushes** | Constitution Rule 9 forbids literal hex and `Colors.*` in Views | *(this spec)* |
| The total count is displayed but treated as **unreliable** | It is parsed from status-message text and absent on the SmartSearch path | *(this spec)* |
| `async void`, `ConfigureAwait(false)`+`Invoke`, fire-and-forget init, `Task.Run` Excel, `ContinueWith` print are **fixed, not carried** | They are threading defects under the constitution | *(this spec)* |
