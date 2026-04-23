# Visual SQL / Infor Visual Risk Report

## Executive Summary

I reviewed the full Infor Visual connection surface in this repository with emphasis on connection open/close behavior, request fan-out, queueing pressure, disconnect/reconnect handling, and anything that could leak resources or damage the SQL Server.

The most important conclusion is this:

- I did **not** find evidence of a classic client-side `SqlConnection` leak in the main Visual service. The service consistently uses short-lived `using` scopes for `SqlConnection`, `SqlCommand`, and `SqlDataReader`.
- I **did** find several high-risk paths that can create **server pressure, duplicate concurrent queries, uncancellable in-flight work, and repeated full-list lookups** against Visual SQL.
- There is **no dedicated Visual-side queue manager or disconnect recovery layer**. The primary risk is not a forgotten open connection object; it is **connection churn and overlapping requests** caused by UI/event behavior.

If this code hurts the Visual SQL Server, it is most likely to happen through:

1. repeated autocomplete list queries,
2. overlapping async searches with no single-flight guard,
3. disabled SQL connection pooling,
4. lack of cancellation when forms close or users change filters rapidly.

## Scope Reviewed

Primary files reviewed:

- `Services/Visual/Service_VisualDatabase.cs`
- `Services/Visual/IService_VisualDatabase.cs`
- `Services/Startup/Service_OnStartup_DependencyInjection.cs`
- `Components/Shared/Component_SuggestionTextBox.cs`
- `Helpers/Helper_SuggestionTextBox.cs`
- `Forms/Visual/Form_InforVisualDashboard.cs`
- `Forms/Visual/Form_PODetails.cs`
- `Controls/Visual/Control_VisualInventory.cs`
- `Controls/Visual/Control_ReceivingAnalytics.cs`
- `Controls/Visual/Control_InventoryAudit.cs`
- `Controls/Visual/Control_DieToolDiscovery.cs`
- `Controls/Visual/Control_VisualUserAnalytics.cs`
- `Helpers/Helper_VisualLifecycle.cs`
- `App.config`

Related but importantly separate from Visual SQL:

- `Services/Database/Service_ConnectionRecoveryManager.cs`
- `Helpers/Helper_Database_ConnectionMonitor.cs`

Those last two are MySQL-side recovery/monitoring helpers and do **not** protect the Visual SQL connection path.

## Verified Architecture

### Visual connection model

- `IService_VisualDatabase` is registered as a **transient** service in `Services/Startup/Service_OnStartup_DependencyInjection.cs:42`.
- Each Visual query method in `Services/Visual/Service_VisualDatabase.cs` creates a new `SqlConnection`, opens it, executes the query, and disposes it via `using`.
- Representative open points are at `Services/Visual/Service_VisualDatabase.cs:60`, `:114`, `:232`, `:302`, `:380`, `:570`, `:669`, `:809`, `:999`, `:1110`, `:1225`, `:1280`, `:1348`, `:1684`, `:1782`, `:1971`, `:2030`, and `:2102`.
- Connection string construction is centralized in `Services/Visual/Service_VisualDatabase.cs:1450`.

### What this means

- There is **no persistent Visual SQL connection object** held open by the service.
- There is **no explicit Visual request queue** object, channel, semaphore, or worker loop.
- Any queueing/spam behavior is emergent from UI events calling the transient service repeatedly.

## Findings

## Critical Findings

### 1. Visual autocomplete methods are not cached despite their names

Evidence:

- `Helpers/Helper_SuggestionTextBox.cs:90-92` resolves a Visual service from DI on demand.
- `Helpers/Helper_SuggestionTextBox.cs:232-298` methods named `GetCachedInfor...Async` all fetch through a service call each time.
- `Services/Visual/Service_VisualDatabase.cs:1165-1328` shows these lookup methods route to live SQL queries.

Why this matters:

- The naming strongly suggests memoized data, but these methods are **live round-trips to SQL Server**.
- Because `IService_VisualDatabase` is transient, each helper lookup can resolve a **fresh service instance** and then open a **new SQL connection**.
- This is especially dangerous for suggestion lists, because users tend to trigger them many times in a short period.

Server-risk impact:

- repeated `DISTINCT` scans,
- repeated login/open handshake cost,
- no shared list reuse across controls or forms.

### 2. Suggestion textbox has no in-flight guard before the provider call

Evidence:

- `Components/Shared/Component_SuggestionTextBox.cs:621` starts `ShowSuggestionOverlayAsync()`.
- `Components/Shared/Component_SuggestionTextBox.cs:442` and `:641` invoke `DataProvider.Invoke()`.
- `_isOverlayVisible` exists at `Components/Shared/Component_SuggestionTextBox.cs:35`, but it is only set later when the overlay is displayed at `:701`.
- Lost-focus entry point is `Components/Shared/Component_SuggestionTextBox.cs:858-877`.
- F4/full-list entry point is `Components/Shared/Component_SuggestionTextBox.cs:435-454` and `:928`.

Why this matters:

- There is **no guard for “lookup already in progress.”**
- `_isOverlayVisible` does not help during the await of `DataProvider.Invoke()` because it is still `false` until display time.
- If the user tabs quickly, presses F4 repeatedly, or triggers focus churn, the same control can issue **multiple overlapping provider calls**.

Server-risk impact:

- parallel duplicate list queries from a single field,
- avoidable burst load on Visual SQL,
- UI races where a slower response can win after a faster one.

### 3. Several Infor suggestion sources hit large tables with unbounded `SELECT DISTINCT`

Evidence:

- `Services/Visual/Service_VisualDatabase.cs:1173-1191` maps work orders, purchase orders, and customer orders to `GetDistinctColumnValuesAsync(...)`.
- `Services/Visual/Service_VisualDatabase.cs:1167` maps user IDs to the same helper.
- `Services/Visual/Service_VisualDatabase.cs:1328` builds:

```sql
SELECT DISTINCT {columnName}
FROM {tableName}
WHERE {columnName} IS NOT NULL AND {columnName} <> ''
ORDER BY {columnName}
```

Why this matters:

- For `USER_ID`, `WORKORDER_BASE_ID`, `PURC_ORDER_ID`, and `CUST_ORDER_ID`, the source table is `INVENTORY_TRANS`.
- There is no date bound, no `TOP`, no cache, no debounce, and no cancellation.
- These queries are being used to support autocomplete and suggestion overlays, which are exactly the kind of UX that gets triggered repeatedly.

Server-risk impact:

- repeated full-history scans or large index walks,
- unnecessary read pressure on `INVENTORY_TRANS`,
- server slowdown under normal operator use.

### 4. SQL Server pooling is explicitly disabled

Evidence:

- `Services/Visual/Service_VisualDatabase.cs:1458-1461` sets:

```csharp
ConnectTimeout = 5,
ApplicationName = "MTM_WIP_App_VisualDashboard",
TrustServerCertificate = true,
Pooling = false
```

Why this matters:

- Every Visual request performs a full connection open path instead of reusing pooled connections.
- In this codebase, that combines badly with transient service resolution and repeated list/search requests.
- This does **not** create a classical leak. It creates **connection churn**.

Server-risk impact:

- higher connection/login overhead,
- more TCP/session churn,
- worse behavior under rapid filter changes or repeated autocomplete usage.

### 5. Visual requests are not cancellable

Evidence:

- `Services/Visual/IService_VisualDatabase.cs` contains no `CancellationToken` parameters.
- `Forms/Visual/Form_InforVisualDashboard.cs:73` / `:166` / `:231` / `:287` call the service without cancellation.
- `Forms/Visual/Form_PODetails.cs:46` / `:60` / `:139` call the service without cancellation.
- Search controls do the same, for example:
    - `Controls/Visual/Control_VisualInventory.cs:71` / `:90`
    - `Controls/Visual/Control_InventoryAudit.cs:235` / `:253` and `:283` / `:333`
    - `Controls/Visual/Control_DieToolDiscovery.cs:124`, `:197`, `:334`

Why this matters:

- When a form closes or the user changes their mind, the app cannot tell SQL Server to stop work.
- The client object will eventually dispose, but the query may continue server-side until completion or timeout.

Server-risk impact:

- in-flight work survives UI abandonment,
- long-running reads consume resources longer than necessary,
- operators can accidentally stack replacement queries on top of still-running older ones.

## High Findings

### 6. `Control_ReceivingAnalytics` can overlap fetches and stale the UI

Evidence:

- Fire-and-forget initial load: `Controls/Visual/Control_ReceivingAnalytics.cs:101`.
- Server-fetch triggers on checkbox changes: `:135`, `:138`, `:139`.
- Client-filter triggers on more checkbox and suggestion events: `:142-155`, `:158-161`.
- The fetch path itself is `Controls/Visual/Control_ReceivingAnalytics.cs:247-296`.

Why this matters:

- There is no semaphore, request version, cancellation token, or “current fetch id” guard.
- Multiple rapid filter changes can produce **overlapping `GetReceivingScheduleAsync()` calls**.
- A slower older fetch can finish after a newer one and overwrite the displayed state.

Server-risk impact:

- repeated expensive schedule queries,
- out-of-order UI updates causing users to retry again,
- burst load without backpressure.

### 7. Search controls allow re-entry from keyboard/click paths

Evidence:

- Visual inventory:
    - `Controls/Visual/Control_VisualInventory.cs:62`, `:66-68`, `:71`, `:90`
- Inventory audit:
    - `Controls/Visual/Control_InventoryAudit.cs:69`, `:83`, `:235`, `:253`, `:283`, `:333`
- Die/tool discovery:
    - `Controls/Visual/Control_DieToolDiscovery.cs:34`, `:38`, `:89`, `:104`, `:124`, `:197`, `:279`, `:293`, `:303`, `:317`, `:334`

Why this matters:

- `Control_VisualInventory` and `Control_InventoryAudit` disable the search button while running, but the keyboard paths remain separate async entry points.
- `Control_DieToolDiscovery` does not implement the same kind of “search in progress” lock around its methods.

Server-risk impact:

- duplicate query bursts from repeated Enter presses or rapid clicks,
- no effective single-flight behavior per control.

### 8. There is no Visual-specific disconnect/recovery path

Evidence:

- `Services/Database/Service_ConnectionRecoveryManager.cs` uses `MySqlConnection`, not `SqlConnection`.
- `Helpers/Helper_Database_ConnectionMonitor.cs` is MySQL process-list monitoring.
- Nothing equivalent was found for the Visual service path.

Why this matters:

- The app has a recovery story for MySQL but not for Visual SQL.
- If Visual becomes slow or unreachable, users only see failures at request time.
- That tends to create human retry storms.

Server-risk impact:

- no backoff or circuit breaker for Visual,
- user-driven refresh spam during outages,
- avoidable load during partial service degradation.

## Medium Findings

### 9. Most Visual queries use default command timeout

Evidence:

- Only some analytics methods set explicit timeouts, e.g. `Services/Visual/Service_VisualDatabase.cs:1787` and `:2107`.
- Most other commands rely on the default timeout.

Why this matters:

- Long-running queries are neither aggressively bounded nor cancellable.
- Default behavior is inconsistent across methods.

Server-risk impact:

- longer-than-expected resource retention,
- harder-to-predict operator retry behavior.

### 10. Debug sample-data fallback can hide real connectivity defects

Evidence:

- `_useSampleData` is toggled in debug-path connection failures at `Services/Visual/Service_VisualDatabase.cs:44`, `:72`, and many other debug-only branches such as `:605`, `:633`, `:693`, `:719`.
- The flag is instance-scoped, and because the service is transient, this is not a global app-wide state leak.

Why this matters:

- It will not directly damage the SQL server, but it can hide broken connection behavior during debugging.
- That increases the chance that operators or testers underestimate load and failure conditions.

## Low Findings

### 11. Visual server/database are static app settings, while credentials are dynamic

Evidence:

- `App.config` defines `VisualServer`, `VisualDatabase`, `VisualUserName`, and `VisualPassword`.
- `Services/Visual/Service_VisualDatabase.cs:20-27` reads server/database from app settings and credentials from either runtime state or app settings.
- `Forms/MainForm/Classes/MainFormUserSettingsHelper.cs:25-35` loads user-level MySQL and Visual credential settings at startup.

Why this matters:

- This looks intentional, but operationally it means Visual endpoint changes require config deployment rather than user-level override.
- It is a configuration rigidity issue, not a leak issue.

## Connection Leak Assessment

### Confirmed safe patterns

The following are positives:

- Main Visual SQL calls are short-lived and use `using` scopes consistently.
- I found no persistent `SqlConnection` field stored in forms or controls.
- I found no explicit Visual-side request queue that could grow unbounded in memory.
- I found no write-path SQL in the Visual service during this review. The service appears to be read-only.

### What is still risky despite proper disposal

Proper disposal prevents a classic client-side leak, but it does **not** protect against:

- too many short-lived opens,
- repeated full-list lookup queries,
- overlapping uncancelled reads,
- user-driven retry storms during outages.

That is the real operational risk profile here.

## Edge Cases Most Likely To Hurt Production

### Edge case A: tabbing through multiple Infor suggestion fields

Path:

- user tabs through Visual fields,
- each field loses focus,
- `InnerTextBox_LostFocus` triggers provider calls,
- helper methods query Visual for distinct lists.

Likely effect:

- multiple full-list queries in quick succession,
- especially expensive for work order / PO / CO / user suggestions backed by `INVENTORY_TRANS`.

### Edge case B: repeated F4 on a slow link

Path:

- user presses F4 while a provider call is still awaiting,
- `ShowFullListAsync()` issues another provider call,
- no in-flight guard prevents duplication.

Likely effect:

- duplicate list-fetch queries,
- unnecessary SQL load,
- operator perception that the system is hung, followed by more retries.

### Edge case C: rapid receiving filter changes

Path:

- user changes multiple receiving filter checkboxes quickly,
- `CheckedChanged` handlers each invoke fetch/apply logic,
- no request serialization exists.

Likely effect:

- overlapping schedule queries,
- stale response wins,
- user retries again because results appear inconsistent.

### Edge case D: form closed while query is still running

Path:

- query starts,
- user closes the form or navigates elsewhere,
- no `CancellationToken` exists.

Likely effect:

- server continues processing a query the user no longer needs,
- newer follow-up queries may be stacked on top.

## Damage Potential To SQL Server / Database

### Direct data corruption risk

Current evidence suggests **low direct data corruption risk** from this Visual layer because I did not find write-oriented SQL paths in the reviewed Visual service code.

### Operational damage risk

Operational risk is **high enough to take seriously** because the combination of these traits is unfavorable:

- pooling disabled,
- transient service resolution,
- no shared lookup cache,
- no in-flight guard for suggestions,
- no cancellation tokens,
- no Visual outage circuit breaker.

That combination can produce:

- unnecessary SQL login churn,
- excess read load,
- human retry storms,
- slowdowns that look like “the server is broken” and trigger even more retries.

## Recommended Remediation Order

### Priority 1

1. Add a shared in-memory cache for Visual suggestion lists with TTL and explicit invalidation.
2. Add single-flight guards to `Component_SuggestionTextBox` so only one provider call per control can be active at a time.
3. Stop using `Pooling = false` for Visual SQL unless there is a proven defect that requires it.

### Priority 2

1. Add `CancellationToken` to `IService_VisualDatabase` and propagate it from forms/controls.
2. Add request-versioning or a semaphore to `Control_ReceivingAnalytics` and other async search controls.
3. Add explicit throttle/debounce around F4/full-list and lost-focus suggestion loading.

### Priority 3

1. Replace unbounded `SELECT DISTINCT` autocomplete sources on `INVENTORY_TRANS` with bounded or precomputed sources.
2. Add a Visual-specific health/circuit-breaker layer so outages do not create user retry storms.
3. Standardize command timeouts and log slow-query metrics per Visual method.

## Bottom Line

The Visual SQL layer is **not leaking connections in the usual sense**. The bigger danger is that it is too easy for the UI to create **many short-lived, overlapping, uncancelled read requests**.

If nothing changes, the most realistic failure mode is not a permanently unclosed socket. It is this:

- users interact normally,
- the app fans out more SQL reads than necessary,
- pooling is disabled so each read is expensive,
- slow responses trigger more user retries,
- the Visual SQL Server gets hammered by duplicated lookup and report traffic.

That is the area that should be treated as critical.
