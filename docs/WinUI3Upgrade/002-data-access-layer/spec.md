# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `002-data-access-layer`
**Depends on**: `001-foundation-and-tooling`
**Unblocks**: `003` (contracts), `005`, `006`, `007`, `008`, `010`
**Tasks**: TASK-014 … TASK-034
**Governing rules**: [`../constitution.md`](../constitution.md) §2 Rules 4, 7, 8, 11

---

## 1. Executive Summary & Scope

This spec ports the data layer. It changes no behaviour the user can see, and it changes nothing in the database.

The WinForms application reaches MySQL 5.7.24 through one static helper and 22 DAOs. The helper has six public methods, each of which opens a connection, builds a command, calls a stored procedure, and translates two output parameters into a result object. The DAOs are thin: they name a procedure and supply parameters. This spec reproduces that exactly, with three deliberate improvements — a real async read path, a provider that supports it, and the raw status code that the current result type throws away.

### In scope

- The result type, extended with `StatusCode`.
- Connection configuration as injectable options instead of a rebuilt static string.
- The stored-procedure helper, all six methods, on `MySqlConnector`.
- The parameter-prefix cache and the two-scheme prefix rules.
- All 22 DAOs as instance classes with interfaces.
- The Infor Visual ERP reader, kept as read-only inline SQL.
- DI registration for every DAO.

### Out of scope

- Any screen. No ViewModel calls a DAO until `005` onward.
- Any change to a table, procedure, parameter name or status contract.
- Migration of any data. Production and test databases already hold correct data.

### Success condition

Every DAO resolves from the container, each has at least three integration tests against `mtm_wip_application_winforms_test` (success, no rows, invalid parameter), and no source file mentions `MySql.Data`.

---

## 2. Legacy Feature Assessment & UI Mapping Table

No visual controls are in scope. The table maps the data-access artefacts.

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `Helper_Database_StoredProcedure` (static, 6 methods) | `Helper_Database_StoredProcedure` (instance, same 6 methods) | Same names, same parameter order, same return shapes. Callers change from `Static.Method(...)` to `_helper.Method(...)` and nothing else. |
| `MySql.Data.MySqlClient` | `MySqlConnector` | Different provider, same semantics. Every error-number branch re-validated, not assumed. |
| `ExecuteDataSetWithStatusAsync`, `ExecuteDataTableWithStatusAsync` using `MySqlDataAdapter.Fill` inside `Task.Run` | Real `ExecuteReaderAsync` + `DataTable.Load` / materialisation | Today these two are synchronous I/O wearing an async signature. They must not remain so: a slow query currently occupies a thread-pool thread while blocking a second. |
| `ExecuteScalarWithStatusAsync`, `ExecuteNonQueryWithStatusAsync` | Unchanged shape, real async | Already genuinely async; port as-is. |
| `ExecuteWithCustomOutputAsync` (caller-specified output parameters) | Unchanged | Only used where a procedure returns more than the standard pair. |
| `ExecuteRawSqlAsync` (inline SQL, `@`-prefixed parameters) | Unchanged, documented exception | Used by the Visual ERP reader only. Requires the same read-only discipline, not the same pooling workaround. |
| `AddParametersWithPrefixDetection` — keys starting `p_`, `in_`, `o_` used verbatim, others prefixed from the cache | Identical rule, same cache | This is why callers pass `"PartID"` and not `"p_PartID"`. Changing it breaks ~120 calls at once. |
| `AddStandardOutputParameters` — always `p_Status` (`Int32`) and `p_ErrorMsg` (`VarChar(500)`) | Identical | Every procedure in this database declares exactly these two. |
| `Model_Dao_Result<T>` and `Model_Dao_Result` | `Model_Result<T>` and `Model_Result`, plus `StatusCode` | Field-for-field compatible, so ported DAO code reads the same. |
| `Model_ParameterPrefix_Cache` (static dictionary) | `IParameterPrefixCache` (injected singleton) | Populated during launch from `INFORMATION_SCHEMA.PARAMETERS` plus the overrides table. |
| `Helper_Database_Variables.GetConnectionString(...)` rebuilding from statics on every call | `DatabaseOptions` bound once from configuration | One read per process, not one per query. |
| `Helper_Control_MySqlSignal` (`DatabaseConfig.ConnectionString` settable static) | Same probe, reading `DatabaseOptions` | Diagnostics only: measures open latency and maps it to strength 0–5. |
| `Helper_Database_ConnectionMonitor` | `IDatabaseConnectionMonitor` | Statistics only. |
| `Dao_Inventory`, `Dao_Transactions`, `Dao_History`, `Dao_QuickButtons`, `Dao_Part`, `Dao_Location`, `Dao_Operation`, `Dao_ItemType`, `Dao_ColorCode`, `Dao_User`, `Dao_System`, `Dao_Shortcuts`, `Dao_VisualAnalytics`, `Dao_DeveloperTools`, `Dao_ErrorLog`, `Dao_ErrorReports`, `Dao_UserFeedback`, `Dao_UserFeedbackComments`, `Dao_WindowFormMapping`, `Dao_UserControlMapping`, `Dao_ParameterPrefixOverrides`, `Dao_EmailNotification` | Same 22 classes, instance, each with an `IDao_*` in `MTM_WIP.Data/Contracts/` | Eleven are static today; all become instance. Fifteen interfaces are new (seven already exist). |
| ~20 synchronous convenience overloads on `Dao_Part` (`GetAllParts`, `InsertPart`, `AddPartWithStoredProcedure`, …) | Dropped | Each duplicates an async method. Only the async twin survives; nothing in the new client may call a blocking data method. |
| `Service_VisualDatabase` (41 inline `SELECT`s via `Microsoft.Data.SqlClient`) | `MTM_WIP.Data/Visual/Service_VisualDatabase.cs`, SQL verbatim | The sanctioned read-only exception. It leaves the UI assembly, which is the actual fix. |
| `App.config` keys `VisualServer`, `VisualDatabase`, `VisualUserName`, `VisualPassword` | `DatabaseOptions.Visual` from configuration | Same values, no credentials in source. |

### Procedures in scope

All ~120 procedures already called by the DAOs are in scope; none is renamed. Grouped by family:

| Family | Count | Owner spec |
| :--- | :--- | :--- |
| `inv_inventory_*`, `inv_transaction*` | ~20 | `005`, `006` |
| `md_part_ids_*`, `md_locations_*`, `md_operation_numbers_*`, `md_item_types_*`, `md_color_codes_*` | ~28 | `005`, `007` |
| `usr_users_*`, `usr_settings_*`, `usr_dgv_settings_*`, `usr_user_shortcuts_*` | ~18 | `004`, `007` |
| `sys_*` (roles, shortcuts, themes, quick-button slots, mappings, prefixes, tracking numbers) | ~30 | `004`, `005`, `007`, `009` |
| `log_error_*`, `md_devtools_*`, `md_feedback_*`, `sp_error_reports_*`, `md_system_*`, `md_sys_visual_*`, `sys_email_notification_*` | ~24 | `008`, `009`, `010` |

---

## 3. Behavioral Edge Cases & State Constraints

### 3.1 Result translation — the rule that must not drift

Verified against the current helper, which performs a **sign check only**:

| `p_Status` | `IsSuccess` | `ErrorMessage` |
| :--- | :--- | :--- |
| `1` | `true` | the procedure's `p_ErrorMsg` |
| `0` | **`true`** | the procedure's `p_ErrorMsg` |
| `-1` … `-5` and any other negative | `false` | the procedure's `p_ErrorMsg` |
| negative with empty `p_ErrorMsg` | `false` | `"Stored procedure {name} returned error status: {status}"` |

Two consequences:

- **Status `0` is success.** Roughly a third of the read paths rely on it: "no rows found" is a normal outcome on a search screen. Treating it as failure would turn every empty search into an error dialog.
- **`-2`, `-3`, `-4` and `-5` are indistinguishable today.** The current type stores no status code. The new `StatusCode` property fixes that without touching a procedure. It is additive: `IsSuccess` still derives from the sign, so no ported branch changes meaning.

One deliberate difference: the current code converts a **missing** status parameter to `0` (success). The new helper treats a missing status parameter as a failure with a clear message, because a procedure that returns no status is a procedure that did not run. This is recorded as an intentional deviation in the drift log.

### 3.2 Parameter binding

- Names are passed **without** the `p_` prefix from C#; the helper adds it. Callers that already pass `p_`, `in_` or `o_` are left untouched — the real procedure set contains all three schemes.
- Values bind with `AddWithValue` and **no explicit type or size**, matching today. Adding types would be an improvement that silently changes how MySQL compares a string to a number.
- `null` becomes `DBNull.Value`. An empty string stays an empty string; the two are not interchangeable to these procedures.
- The two output parameters are always appended, even when the caller supplies none.

### 3.3 Connection and pooling

- The current connection string carries **no password clause at all** — a redaction placeholder sits where the password would be. Authentication therefore depends on server configuration, and the new client must not assume a password is present.
- `Pooling=false` is set today. It exists because `ExecuteRawSqlAsync` was written to require it, not because the workload wants it. Pooling is enabled in the new client; opening a connection per query at shop-floor call rates is the dominant cost on the search screens.
- Command timeout is 30 seconds, applied per command, not in the connection string.
- A connection supplied by the caller (test transaction support) is reused and never disposed by the helper.

### 3.4 Retry

Three attempts, 500 ms × 2ⁿ⁻¹ backoff, on MySQL error numbers **1205, 1213, 2006, 2013** only. These numbers belong to `MySql.Data`. `MySqlConnector` reports the same server-side numbers for the first two but its client-side connection numbers must be checked rather than assumed — a wrong predicate means either no retry at all or a retry storm on a genuine failure.

Friendly translation covers error numbers 0, 1042, 1045, 1205, 1213, 2002, 2006, 2013 plus timeouts and socket errors. Each becomes one plain sentence naming a next step.

### 3.5 State eliminated by this spec

| Legacy static | Replacement |
| :--- | :--- |
| `Model_Application_Variables.ConnectionString` (rebuilt per call) | `IOptionsMonitor<DatabaseOptions>` |
| `Model_Shared_Users.WipServerAddress` / `Database` / `WipServerPort` | `DatabaseOptions.Wip` |
| `Helper_Database_Variables.GetConnectionString(...)` | `DatabaseOptions.Wip.ConnectionString` |
| `DatabaseConfig.ConnectionString` (settable) | `DatabaseOptions`, not settable at runtime |
| `Model_ParameterPrefix_Cache` | `IParameterPrefixCache` singleton |
| The eleven `static` DAO classes | Instance classes behind interfaces |
| `Program.ServiceProvider` used from inside a DAO | Constructor injection |

### 3.6 Persistence touched

None. This spec reads and writes only the tables the procedures already address. It creates no file and no folder. The `DatabaseOptions` values are read from configuration supplied by `004`; this spec defines the type and binds it in tests only.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **Database credentials** | **Resolved (D18)** — keep current behaviour: the WIP connection string carries no password and relies on server configuration. `DatabaseOptions.Wip.Password` stays optional and unused in practice. Accepted risk recorded; not revisited. |
| 2 | **Visual ERP credentials** | **Resolved (D18)** — they move from committed `App.config` into configuration. The `JKOLL` / `KOLL` literals must not appear anywhere in the new repository, including comments and test fixtures. Accepted risk: a config-file secret is readable by anyone with share access. |
| 3 | Whether `log_error` is still meant to be written. The DAO method exists but has no caller; live errors go to CSV and to `error_reports`. | Undecided. The DAO is ported so the surface is complete; wiring is `010`'s problem. |
| 4 | Whether `MD_devtools_*` and `md_feedback_*` procedures are stable enough to test | Assumed yes. If a procedure cannot be exercised without disturbing production data, its DAO test is skipped with a written reason and the gap is raised. |
| 5 | `Dao_Transactions` parses a result count out of the status message text | Fragile but current. Preserved exactly; not repaired here. |
| 6 | The `md_sys_visual_*` procedures accept JSON parameters on a MySQL 5.7.24 server | Already in production use; no change. |

### Assumptions

1. `mtm_wip_application_winforms_test` exists on the same server and mirrors the production procedure set.
2. Every procedure declares `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)`.
3. Integration tests may write to the test database, always inside a rolled-back transaction.
4. The Visual ERP server is reachable from a developer machine or VPN; if not, the Visual tests are skipped with a reason.
