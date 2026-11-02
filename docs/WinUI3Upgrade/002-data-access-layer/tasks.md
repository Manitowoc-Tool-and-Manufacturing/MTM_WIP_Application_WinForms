# Migration Task List (Execution Plan)

**Spec**: `002-data-access-layer`
**Range**: TASK-014 … TASK-034
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Ordering is load-bearing.** The helper is proven before any DAO is ported (TASK-014 … TASK-020). A DAO ported against an unproven helper produces failures that look like DAO bugs and are not.

---

## Phase 1 — Result type and configuration

- [ ] **TASK-014** — Implement `Model_Result<T>` and `Model_Result` in `MTM_WIP.Core/Models/Model_Result.cs` per [`plan.md`](./plan.md) §2.2, plus `Model_Dao_Status` constants for `1`, `0`, `-1` … `-5`. `IsSuccess` derives from `StatusCode >= 0`.
  - **Verify**: unit tests cover all four factories, `IsSuccess` for status `1`, `0`, `-1` and `-2`, the implicit conversion to `T`, and that a failure has an empty `Data`.

- [ ] **TASK-015** — Implement `DatabaseOptions` with `Wip` and `Visual` sections per [`plan.md`](./plan.md) §3.1, plus `DatabaseOptionsValidator` rejecting a missing server or a port outside 1–65535.
  - **Verify**: unit test binds a sample `appsettings.json` fragment and asserts every property; the validator rejects port `0` and `70000`. `git grep` finds no Visual password literal anywhere in the solution.

---

## Phase 2 — Helper

- [ ] **TASK-016** — Port all six helper methods to an instance class on `MySqlConnector`, keeping the parameter-prefix rules, the two standard output parameters, the caller-supplied connection/transaction path, and the 30-second command timeout.
  - **Verify**: integration test calls `inv_inventory_Get_All` against the test database through a caller-supplied transaction and asserts `IsSuccess` and `StatusCode == 1`; a second test rolls the transaction back and asserts no row persisted.

- [ ] **TASK-017** — Replace `MySqlDataAdapter.Fill` wrapped in `Task.Run` with `ExecuteReaderAsync` + `DataTable.Load`, and implement the multi-result-set loop for `ExecuteDataSetWithStatusAsync` using `NextResultAsync`.
  - **Verify**: a test with a ten-result-set procedure (`inv_transactions_GetAnalytics`) asserts `DataSet.Tables.Count == 10` in order; a grep of the helper finds no `Task.Run` and no `MySqlDataAdapter`.

- [ ] **TASK-018** — Re-validate the transient-error predicate against `MySqlConnector.MySqlException`. Enumerate the numbers actually raised for deadlock, lock-wait timeout, connection lost, connection refused, unknown procedure and authentication failure. Correct the retry set and the friendly-message table.
  - **Verify**: a table-driven test asserts **retry** for deadlock, lock-wait and connection-lost; asserts **no retry** for connection-refused, unknown-procedure and authentication-failure; and asserts each maps to a distinct plain message.

- [ ] **TASK-019** — Implement `IParameterPrefixCache` / `Service_ParameterPrefixCache`: overrides table first, then `INFORMATION_SCHEMA.PARAMETERS` probing `p_`, `in_`, `o_`, then the `p_` fallback. `p_Status` and `p_ErrorMsg` always resolve to `p_` without consulting either source.
  - **Verify**: a test resolves a real `p_`-prefixed parameter, a real `in_`-prefixed parameter, an unprefixed parameter of a real procedure, and both standard output parameters. A test on an empty cache falls back to `p_` and logs exactly one warning.

- [ ] **TASK-020** — Enable connection pooling, remove `Pooling=false`, and confirm `ExecuteRawSqlAsync` still functions without it.
  - **Verify**: 100 sequential read calls against the test database complete without pool exhaustion, and `SHOW STATUS LIKE 'Threads_connected'` does not climb proportionally. The read-only Visual query test (TASK-031) still passes.

---

## Phase 3 — DAOs (simple first)

- [ ] **TASK-021** — Port `Dao_Inventory` + `IDao_Inventory` (8 methods: get all, search, by part, by part and operation, add item, transfer part, transfer quantity, fix batch numbers).
  - **Verify**: three integration tests (success, no rows, invalid parameter). A test adds an item inside a rolled-back transaction and reads it back.

- [ ] **TASK-022** — Port `Dao_Part` + `IDao_Part`, keeping only the async methods and dropping the ~20 blocking overloads.
  - **Verify**: three integration tests pass; `git grep` confirms no `GetAllParts(`, `InsertPart(` or `AddPartWithStoredProcedure(` exists in the solution.

- [ ] **TASK-023** — Port `Dao_Location`, `Dao_Operation`, `Dao_ItemType`, `Dao_ColorCode` with interfaces.
  - **Verify**: three integration tests each, including the existence check per DAO.

- [ ] **TASK-024** — Port `Dao_System` + `IDao_System` (user name, access type, role id, theme list, connectivity check).
  - **Verify**: tests resolve user name, access type for a known user, and a role id; connectivity check returns success against the test database.

---

## Phase 4 — DAOs (complex)

- [ ] **TASK-025** — Port `Dao_Transactions` + `IDao_Transactions`, preserving the two-of-three-types → smart-search dispatch rule and the result-count parsing from the status message.
  - **Verify**: a unit test with a mocked helper asserts which procedure is called for 0, 1, 2 and 3 selected types. An integration test returns a page and a lifecycle tree.

- [ ] **TASK-026** — Port `Dao_TransactionHistory` + `IDao_TransactionHistory` (the renamed `Dao_History`, append-only).
  - **Verify**: an integration test appends one row inside a rolled-back transaction and asserts the procedure received the expected parameters.

- [ ] **TASK-027** — Port `Dao_QuickButtons` + `IDao_QuickButtons` (9 methods: update, remove-and-shift, add, move, delete-all, add-or-shift, add-at-position, cleanup).
  - **Verify**: an integration test for a synthetic user adds to position 1, asserts existing rows shift down, moves a row, and removes a row with the gap closed. All inside a rolled-back transaction.

- [ ] **TASK-028** — Port `Dao_User` + `IDao_User` (settings read/write, user CRUD, role assignment, grid settings).
  - **Verify**: a test round-trips the frozen `usr_settings` keys (`LastShownVersion`, `HideChangeLog`, `VisualUserName`, `WipServerAddress`, `WipServerPort`, `WIPDatabase`, `AnimationsEnabled`, `AutoExpandPanels`, `ShowTotalSummaryPanel`) through a rolled-back transaction and asserts every key survives unchanged.

- [ ] **TASK-029** — Port `Dao_Shortcuts` + `IDao_Shortcuts` (system list, per-user overrides with effective key, upsert, reset).
  - **Verify**: an integration test upserts a user override, reads back the effective key, resets, and asserts the system default is returned.

- [ ] **TASK-030** — Port `Dao_VisualAnalytics` + `IDao_VisualAnalytics`.
  - **Verify**: an integration test reads `sys_visual` and asserts the shape when a row exists, and a successful empty result when none does.

---

## Phase 5 — Visual ERP

- [ ] **TASK-031** — Move `Service_VisualDatabase` into `MTM_WIP.Data/Visual/` with all 41 query strings unchanged; source the connection from `DatabaseOptions.Visual`; return `Model_Result<T>` from every method.
  - **Verify**: a test scans the file and asserts no DDL/DML keywords. If the ERP server is reachable, one query returns rows; otherwise the test is skipped with the reason `Visual ERP unreachable from build agent`.

- [ ] **TASK-032** — Port the remaining DAOs, compile-only: `Dao_DeveloperTools`, `Dao_ErrorLog`, `Dao_ErrorReports`, `Dao_UserFeedback`, `Dao_UserFeedbackComments`, `Dao_WindowFormMapping`, `Dao_UserControlMapping`, `Dao_ParameterPrefixOverrides`, `Dao_EmailNotification`, plus `IDatabaseConnectionMonitor`.
  - **Verify**: all compile with interfaces and no behaviour is wired. `dotnet build` is clean.

---

## Phase 6 — Registration

- [ ] **TASK-033** — Implement `DataServiceRegistration.AddDataServices(IServiceCollection, IConfiguration)` registering the helper, the prefix cache, the monitor, all 22 DAOs and the Visual service. Lifetimes: helper and cache singleton; DAOs singleton (they hold nothing mutable); Visual service singleton.
  - **Verify**: a test builds a `ServiceProvider` from that method and resolves **every** `IDao_*` plus `Helper_Database_StoredProcedure` and `IParameterPrefixCache`. A deliberately unregistered interface throws from `GetService<T>()` with the type name.

---

## Phase 7 — Downstream consistency validation

- [ ] **TASK-034** — **Downstream validation: `003-crosscutting-services-and-theming` (and a sweep of `004`–`011`).**

  Spec `003` builds the services that *call* this layer, so it is the first consumer of every decision made here. Re-read [`../003-crosscutting-services-and-theming/spec.md`](../003-crosscutting-services-and-theming/spec.md), [`plan.md`](../003-crosscutting-services-and-theming/plan.md) and [`tasks.md`](../003-crosscutting-services-and-theming/tasks.md), then sweep the rest.

  Confirm and record:
  1. **Every interface referenced by `003` actually exists** with the method names and signatures `003` assumes — in particular `IDao_Shortcuts` (used by `IShortcutService`), `IDao_User` (used by `IUserPreferences` and `ICurrentUserService`) and `IDao_Part`/`IDao_Location`/`IDao_Operation`/`IDao_ItemType`/`IDao_ColorCode` (used by `IMasterDataCache`).
  2. **`Model_Result<T>` has `StatusCode`** and `003`'s error service distinguishes failure causes using it, not by string matching.
  3. **The helper is injectable**, so `003`'s services can be unit-tested with a mocked `IDao_*` rather than a mocked helper.
  4. **`DatabaseOptions` is registered** in a way `004` can bind from `appsettings.json` without re-declaring the type.
  5. **No DAO is static**, because `003`'s services are resolved from DI and a static DAO cannot be substituted in a test.
  6. **The Visual reader returns `Model_Result<T>`**, because `008` handles ERP failures through the same path as WIP failures.
  7. Run `dotnet test` and confirm the whole suite — not just this spec's tests — is green.

  If any item is false, fix **this** spec before proceeding. Do not amend `003` to work around a defect here.

  - **Verify**: a written entry exists in the Drift log for each of the seven items, each marked *confirmed* or *amended*, and the full test suite is green.

---

## Drift log

Filled in by TASK-034.

| Checked spec | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `003` | All `IDao_*` signatures match what `003` calls | *(pending)* | — |
| `003` | Error service uses `StatusCode`, not message text | *(pending)* | — |
| `003` | Services testable with mocked DAOs (helper injectable) | *(pending)* | — |
| `004` | `DatabaseOptions` registration reusable from `appsettings.json` | *(pending)* | — |
| `003`, `005`–`008` | No static DAO remains | *(pending)* | — |
| `008` | Visual reader returns `Model_Result<T>` | *(pending)* | — |
| all | Full solution test suite green | *(pending)* | — |

## Known deviations from the WinForms behaviour

Recorded so a reviewer does not mistake them for defects.

| Deviation | Rationale | Reviewed by |
| :--- | :--- | :--- |
| A missing `p_Status` parameter is a **failure**, not success-as-`0` | A procedure that returns no status did not run; calling that success hides a real error | *(this spec)* |
| Pooling is **on** | `Pooling=false` existed to satisfy `ExecuteRawSqlAsync`, not the workload | *(this spec)* |
| The implicit `operator bool` is **dropped** | It makes `if (result)` compile and read like `if (result.IsSuccess)` while failing differently | *(this spec)* |
| `Dao_History` is renamed `Dao_TransactionHistory` | The old name implies it owns history queries; it only appends | *(this spec)* |
| Synchronous `Dao_Part` overloads are **not** ported | Blocking data calls are forbidden by the constitution | *(this spec)* |
