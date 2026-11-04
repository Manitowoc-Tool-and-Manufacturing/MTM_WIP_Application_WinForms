# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `002-data-access-layer`
**Tasks**: TASK-014 … TASK-034

---

## 1. Project Topology & Directory Mapping

All of this spec lands in **`MTM_WIP.Data`** plus one model in **`MTM_WIP.Core`**.

```text
MTM_WIP.Core/
└── Models/
    ├── Model_Result.cs                  # Model_Result<T> + Model_Result
    └── Model_Dao_Status.cs              # status-code constants (-1..-5, 0, 1)

MTM_WIP.Data/
├── Configuration/
│   ├── DatabaseOptions.cs               # Wip + Visual sections
│   └── DatabaseOptionsValidator.cs
├── Contracts/                            # 22 IDao_* interfaces
│   ├── IDao_Inventory.cs
│   ├── IDao_Part.cs
│   ├── IDao_Transactions.cs
│   ├── IDao_TransactionHistory.cs
│   ├── IDao_QuickButtons.cs
│   ├── IDao_User.cs
│   ├── IDao_System.cs
│   ├── IDao_Location.cs
│   ├── IDao_Operation.cs
│   ├── IDao_ItemType.cs
│   ├── IDao_ColorCode.cs
│   ├── IDao_Shortcuts.cs
│   ├── IDao_VisualAnalytics.cs
│   ├── IDao_DeveloperTools.cs
│   ├── IDao_ErrorLog.cs
│   ├── IDao_ErrorReports.cs
│   ├── IDao_UserFeedback.cs
│   ├── IDao_UserFeedbackComments.cs
│   ├── IDao_WindowFormMapping.cs
│   ├── IDao_UserControlMapping.cs
│   ├── IDao_ParameterPrefixOverrides.cs
│   ├── IDao_EmailNotification.cs
│   ├── IParameterPrefixCache.cs
│   └── IDatabaseConnectionMonitor.cs
├── Helpers/
│   ├── Helper_Database_StoredProcedure.cs
│   ├── Helper_Database_ErrorTranslator.cs   # error number → plain sentence
│   └── Helper_StoredProcedureProgress.cs    # IProgress-based, thin
├── Services/
│   ├── Service_ParameterPrefixCache.cs
│   └── Service_DatabaseConnectionMonitor.cs
├── Visual/
│   └── Service_VisualDatabase.cs            # 41 read-only SELECTs, verbatim
├── Dao_Inventory.cs
├── Dao_Part.cs
├── Dao_Transactions.cs
├── Dao_TransactionHistory.cs
├── Dao_QuickButtons.cs
├── Dao_User.cs
├── Dao_System.cs
├── Dao_Location.cs
├── Dao_Operation.cs
├── Dao_ItemType.cs
├── Dao_ColorCode.cs
├── Dao_Shortcuts.cs
├── Dao_VisualAnalytics.cs
├── Dao_DeveloperTools.cs
├── Dao_ErrorLog.cs
├── Dao_ErrorReports.cs
├── Dao_UserFeedback.cs
├── Dao_UserFeedbackComments.cs
├── Dao_WindowFormMapping.cs
├── Dao_UserControlMapping.cs
├── Dao_ParameterPrefixOverrides.cs
├── Dao_EmailNotification.cs
└── DependencyInjection/
    └── DataServiceRegistration.cs           # AddDataServices(IServiceCollection, IConfiguration)
```

`Dao_TransactionHistory` is the renamed `Dao_History`. The rename is deliberate: `Dao_History` collides conceptually with the transaction search screen and reads as though it owns history queries, which it does not — it only appends.

`MTM_WIP.Data` references **`MTM_WIP.Core` only**. It must not reference `MTM_WIP.Shared`, which is asserted by TASK-007 from spec `001`.

---

## 2. Component Migration Strategy

### 2.1 Porting order

The helper is ported and proven before any DAO moves. A DAO ported against an unproven helper produces failures that look like DAO bugs and are not.

```
DatabaseOptions → Model_Result → helper signature port → real async read path
→ error-number revalidation → prefix cache → pooling on
→ simple DAOs (master data) → complex DAOs (transactions, user)
→ Visual ERP reader → DI registration
```

Master-data DAOs come before transactions because they are single-call, single-result-set and give the helper a wide workout cheaply.

### 2.2 `Model_Result<T>`

```
Model_Result<T>
  bool     IsSuccess      => StatusCode >= 0
  T?       Data
  string   ErrorMessage   => "" when successful and the procedure said nothing
  Exception? Exception
  string   StatusMessage  => the procedure's p_ErrorMsg
  int      RowsAffected
  int      StatusCode     => raw p_Status

  static Model_Result<T> Success(T data, string statusMessage = "", int rowsAffected = 0)
  static Model_Result<T> Success(string statusMessage = "", int rowsAffected = 0)
  static Model_Result<T> Failure(string errorMessage, Exception? exception = null, int statusCode = -1)
  static Model_Result<T> Failure(Exception exception, int statusCode = -1)

Model_Result           # identical, without Data
```

Kept from the current type: `IsSuccess`, `Data`, `ErrorMessage`, `Exception`, `StatusMessage`, `RowsAffected`, the four factory overloads, and the implicit conversion to `T`. Added: `StatusCode`, and `statusCode` on the failure factories so the caller can tell validation from not-found from database error.

Dropped deliberately: the current implicit `operator bool` on the non-generic type. It invites `if (result)` in place of `if (result.IsSuccess)`, which reads the same but fails differently when a future overload appears.

### 2.3 The helper

One instance class, one constructor taking `DatabaseOptions` and `IParameterPrefixCache`. Six public methods with these shapes:

```
Task<Model_Result<DataSet>>              ExecuteDataSetWithStatusAsync   (string procedure, IDictionary<string,object?>? parameters = null, IProgress<...>? progress = null, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
Task<Model_Result<DataTable>>            ExecuteDataTableWithStatusAsync (… same …)
Task<Model_Result<object?>>              ExecuteScalarWithStatusAsync    (… same …)
Task<Model_Result>                       ExecuteNonQueryWithStatusAsync  (… same …)
Task<Model_Result<Dictionary<string,object?>>> ExecuteWithCustomOutputAsync (string procedure, IDictionary<string,object?>? parameters, IReadOnlyList<string> outputParameters, IProgress<...>? progress = null)
Task<Model_Result<int>>                  ExecuteRawSqlAsync             (string sql, IDictionary<string,object?>? parameters = null)
```

Note the connection string parameter disappears — it comes from the injected options. That is the one signature change, and it makes every DAO call site shorter.

Shared private flow, used by all six:

```
1. Resolve connection (caller's or new) and open async if we own it.
2. Build MySqlCommand: CommandType.StoredProcedure (or Text for raw), CommandTimeout from options.
3. Attach transaction if supplied.
4. AddParametersWithPrefixDetection  → returns the resolved names, for tracing.
5. AddStandardOutputParameters       → p_Status Int32, p_ErrorMsg VarChar(500).
6. Execute with retry (3 attempts, 500 ms × 2^n-1, transient only).
7. Read status + error message.
8. Translate to Model_Result via the sign rule.
9. Dispose only what we opened.
```

### 2.4 Real async read path

Replacing `Task.Run(() => adapter.Fill(table))`:

- `ExecuteDataTableWithStatusAsync`: create the `MySqlDataTable`/`DataTable`, `using var reader = await command.ExecuteReaderAsync(ct)`, then `dataTable.Load(reader)`.
- `ExecuteDataSetWithStatusAsync`: `await command.ExecuteReaderAsync(ct)`, then for each result set `dataSet.Tables.Add(...)` and load; `reader.NextResultAsync()` between sets. The transactions analytics path depends on **ten** result sets arriving in order, so the loop must not stop early on an empty set.

`DataTable.Load` is synchronous in-memory work over an already-materialised reader, which is correct: the I/O is awaited and only the in-memory shaping is synchronous, on the calling thread.

### 2.5 Error-number revalidation

`MySqlConnector` does not use the same client-side error numbers as `MySql.Data` for every condition. TASK-018 enumerates the numbers `MySqlConnector` actually raises for:

| Condition | What to prove |
| :--- | :--- |
| Deadlock | retried |
| Lock wait timeout | retried |
| Connection lost mid-command (server closed it) | retried |
| Connection could not be established | **not** retried three times — fails once with a friendly message |
| Unknown procedure / bad parameter | **not** retried |
| Authentication failure | **not** retried, and the message names credentials without printing them |

The test asserts the predicate's verdict per condition, so a future provider upgrade that shifts a number fails a test rather than silently disabling retry.

### 2.6 Parameter-prefix cache

Two sources, in priority order, matching today:

1. **Overrides table** (`sys_parameter_prefix_overrides_*`) — an explicit per-procedure, per-parameter prefix. Rare.
2. **`INFORMATION_SCHEMA.PARAMETERS`** — the live parameter list, probed for `p_`, `in_`, `o_` in that order.
3. **Fallback** — `p_`.

`p_Status` and `p_ErrorMsg` always resolve to `p_` without consulting either source; they are the documented contract.

The cache is populated during launch (`004`) and is a singleton. If it is empty when a call arrives — for example a test that did not run the launch pipeline — the helper falls back to `p_` rather than throwing, because that is what the current code does and every procedure's standard parameters use `p_` anyway. A warning is logged once.

### 2.7 Visual ERP reader

`Service_VisualDatabase` moves into `MTM_WIP.Data/Visual/` with **all 41 query strings unchanged**. It keeps using `Microsoft.Data.SqlClient`, because that is the provider the ERP server speaks.

Only two changes:

1. The connection string comes from `DatabaseOptions.Visual` instead of `App.config` plus static user fields.
2. Every method is `async Task<Model_Result<T>>` returning the shared result type, so the module in `008` handles ERP failures and WIP failures identically.

A test asserts the file contains no `INSERT`, `UPDATE`, `DELETE`, `DROP`, `ALTER`, `TRUNCATE`, `CREATE` or `MERGE` keyword. That is a coarse guard, and it is deliberate: a coarse guard that cannot be argued with beats a nuanced one that gets waived.

### 2.8 DAO port shape

Every DAO follows one template, so a reviewer can read any of the 22 in seconds:

```csharp
public sealed class Dao_Inventory : IDao_Inventory
{
    private readonly Helper_Database_StoredProcedure _db;
    private readonly ILogger<Dao_Inventory> _logger;

    public Dao_Inventory(Helper_Database_StoredProcedure db, ILogger<Dao_Inventory> logger)
    { _db = db; _logger = logger; }

    /// <summary>Returns all inventory rows.</summary>
    public async Task<Model_Result<DataTable>> GetAllInventoryAsync(CancellationToken ct = default)
    {
        var result = await _db.ExecuteDataTableWithStatusAsync("inv_inventory_Get_All");
        if (!result.IsSuccess)
            _logger.LogWarning("inv_inventory_Get_All failed: {Status} {Message}",
                               result.StatusCode, result.ErrorMessage);
        return result;
    }
}
```

Rules the template encodes:

- Never throw for a data failure; return the failure.
- Log at Warning for a failed call, at Debug for a successful one — not `Information`, which would flood the file on the search screens.
- Never catch and swallow; either return the result or rethrow a programmer error.
- No `Task.Run`, no `.Result`, no `.Wait()`.
- `CancellationToken` is threaded through every method that can hang, even where the DAO ignores it, so the caller can cancel.

### 2.9 What is deliberately not ported

| Not ported | Why |
| :--- | :--- |
| ~20 synchronous `Dao_Part` overloads | Blocking data calls are forbidden by the constitution |
| `Dao_ErrorLog.LogErrorToDatabaseAsync` wiring | It has no caller today; `010` decides whether it gets one |
| `ExecuteRawSqlAsync`'s `Pooling=false` requirement | That was a workaround, not a design |
| `Model_Application_Variables.ConnectionString` | Replaced by options |

---

## 3. Data & State Migration

### 3.1 Configuration contract

```
DatabaseOptions
├── Wip
│   ├── Server                "172.16.1.104"
│   ├── Port                  3306
│   ├── Database              "mtm_wip_application_winforms"
│   ├── User                  "root"
│   ├── Password              (optional; today the connection string carries none)
│   ├── CommandTimeoutSeconds 30
│   ├── ConnectTimeoutSeconds 15
│   └── Pooling               true          ← changed from false
└── Visual
    ├── Server                "VISUAL"
    ├── Database              "MTMFG"
    ├── User                  (from configuration, never source)
    ├── Password              (from configuration, never source)
    └── ConnectTimeoutSeconds 5
```

Built into a connection string by a single method per section. Two properties of that method matter:

- It never logs, and never returns the string in an error message.
- It reproduces the existing WIP string's options — `Allow User Variables=True`, `SslMode=Disabled`, `AllowPublicKeyRetrieval=true` — because the server sits behind a LAN behind a legacy connection contract, and changing TLS behaviour would break connectivity rather than improve it. `AllowPublicKeyRetrieval=true` is **not optional** once the target server is 8.4 LTS: `caching_sha2_password` over an unencrypted TCP connection needs the server's RSA public key, so removing it fails authentication rather than tightening security (`000-pre-foundation` D40/D41).

### 3.2 Transaction and connection ownership

- `Helper_Database_StoredProcedure` never starts a transaction. One call, one procedure, one implicit commit — matching today.
- A caller may supply a connection and transaction; the helper then neither opens nor disposes.
- This is the mechanism the integration tests use to roll back, so it is load-bearing and must be covered by a test: a helper call on a caller-supplied transaction must be rolled back by the caller.

### 3.3 Test strategy

Three integration tests per DAO, all against `mtm_wip_application_winforms_test`:

| Test | Asserts |
| :--- | :--- |
| Success | `IsSuccess` true, `Data` non-null, `StatusCode` ≥ 0 |
| No rows | `IsSuccess` **true**, empty data, `StatusCode == 0` |
| Invalid parameter | `IsSuccess` false, `ErrorMessage` non-empty, `StatusCode` < 0 |

Where a procedure cannot be called safely without disturbing shared state, the test is skipped with `Skip = "<reason>"` and the gap is listed in `tasks.md`'s drift log. A skipped test with a reason is honest; a green test that calls nothing is not.

**Fixture data comes from a deterministic seed script, never from production** (`000-pre-foundation` D56). The live 5.7.24 server is read-only to this migration (D42), so no real part number, location or transaction can leave it. The seed is a committed `.sql` file applied before the suite runs; it truncates the mutable tables and inserts a fixed set of rows with stable identifiers, so two runs produce identical counts and checksums. Tests that need a specific shape create their own rows inside the rolled-back transaction rather than depending on another test's leftovers — the seed provides a floor, not a shared fixture contract.

Two behaviour tests sit above the per-DAO set:

- **Transaction rollback**: insert inside a caller transaction, roll back, assert the row is absent.
- **Seed determinism**: apply the seed twice and assert identical row counts and a stable checksum per table.
- **No provider leak**: the forbidden-API gate from `001` already covers `MySql.Data`; a second assertion walks `obj/project.assets.json` for the package name, because a transitive reference would compile and still load the wrong provider.

One constraint on the data layer that comes from the launch budget rather than from correctness: **no synchronous database call may occur during application launch** (`000-pre-foundation` D57, ≤ 5 s to interactive from a network share). Every method on this helper is `async Task<T>` and there is no synchronous counterpart, so a launch-time caller has nothing to block on — which is the point.
