# Spec Compliance Rules Database

_Last generated: 2025-10-24_

The following rules were extracted from completed specification tasks in `specs/` and `specs/Archives/`. Each rule has been validated against the current codebase to confirm adoption. These rules must be enforced when refactoring existing files or adding new code.

---

## Rule CR-DAO-001: DAO Methods Return `DaoResult` and Use Stored Procedure Helpers

- **Source Spec**: `specs/Archives/002-comprehensive-database-layer/spec.md`
- **Source Tasks**: `specs/Archives/002-comprehensive-database-layer/tasks.md` (T024a–T045h, lines 1247-1452), `specs/Archives/002-003-database-layer-complete/tasks.md` (T113–T118)
- **Implementation Status**: ✅ Validated
- **Evidence Files**: `Data/Dao_Inventory.cs`, `Data/Dao_Part.cs`, `Data/Dao_User.cs`, `Data/Dao_QuickButtons.cs`
- **Evidence Count**: 80+ method implementations

### Description
All DAO methods must be asynchronous, call the `Helper_Database_StoredProcedure.Execute*WithStatusAsync` helpers, and return `DaoResult` or `DaoResult<T>` envelopes. Direct `MySqlCommand` usage or returning raw `DataTable`/primitive types is prohibited.

### Pattern to Enforce
**Before (Anti-pattern)**
```csharp
var table = Helper_Database_Core.ExecuteDataTable(command);
return table;
```

**After (Compliant pattern)**
```csharp
var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    Model_AppVariables.ConnectionString,
    "inv_inventory_Get_ByUser",
    new Dictionary<string, object> { ["p_User"] = userName });

return result.IsSuccess && result.Data != null
    ? DaoResult<DataTable>.Success(result.Data)
    : DaoResult<DataTable>.Failure(result.ErrorMessage ?? "Inventory query failed");
```

### Violation Detection
- **Search Pattern**: `return .*DataTable` OR `new MySqlCommand(` inside `Data/Dao_*.cs`
- **Common Locations**: Legacy DAO files, new DAO additions
- **File Types**: `Data/*.cs`

### Remediation Steps
1. Wrap stored procedure invocation with the appropriate helper method.
2. Map helper responses to `DaoResult`/`DaoResult<T>` using Success/Failure factories.
3. Ensure all consumers inspect `DaoResult` instead of raw tables.

### Instruction References
- `.github/instructions/mysql-database.instructions.md`
- `.github/instructions/csharp-dotnet8.instructions.md`

---

## Rule CR-DAO-002: DAO Methods Instrumented with `Service_DebugTracer`

- **Source Tasks**: `specs/Archives/002-comprehensive-database-layer/tasks.md` (T035, lines 1371-1373)
- **Implementation Status**: ✅ Validated
- **Evidence Files**: `Data/Dao_Inventory.cs`, `Data/Dao_Transactions.cs`

### Description
Every DAO method must call `Service_DebugTracer.TraceMethodEntry`, `TraceBusinessLogic` (where applicable), and `TraceMethodExit` to provide diagnostic coverage.

### Pattern to Enforce
**Before**
```csharp
public static async Task<DaoResult<DataTable>> GetInventoryAsync()
{
    var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(...);
    return DaoResult<DataTable>.Success(result.Data!);
}
```

**After**
```csharp
Service_DebugTracer.TraceMethodEntry(parameters, nameof(GetInventoryAsync), "Dao_Inventory");
...
Service_DebugTracer.TraceBusinessLogic("INVENTORY_SEARCH_COMPLETE", outputData: diagnostics);
...
Service_DebugTracer.TraceMethodExit(resultEnvelope, nameof(GetInventoryAsync), "Dao_Inventory");
return resultEnvelope;
```

### Violation Detection
- **Search Pattern**: `public .* Task<DaoResult` without `TraceMethodEntry`
- **File Types**: `Data/*.cs`

### Remediation Steps
1. Capture key method inputs in a dictionary and log entry/exit.
2. Add relevant `TraceBusinessLogic` events for notable operations.
3. Ensure tracing occurs before each return path.

---

## Rule CR-UI-001: UI Event Handlers Must Evaluate `DaoResult`

- **Source Tasks**: `specs/Archives/002-comprehensive-database-layer/tasks.md` (T046a–T046r, lines 1461-1643)
- **Implementation Status**: ✅ Validated across updated controls except legacy AdvancedInventory
- **Evidence Files**: `Controls/SettingsForm/Control_Add_User.cs`, `Controls/Shared/Control_RemoveTab.cs`

### Description
UI layers must never assume success when invoking DAO methods. Event handlers should inspect `DaoResult.IsSuccess` (and `Data` when applicable), surface error messages via `Service_ErrorHandler`, and avoid directly reading `DataTable`/`DataRow` without null checks.

### Pattern to Enforce
**Before (Anti-pattern)**
```csharp
var inventory = await Dao_Inventory.GetAllInventoryAsync();
InventoryGrid.DataSource = inventory.Data;
```

**After (Compliant)**
```csharp
var inventoryResult = await Dao_Inventory.GetAllInventoryAsync();
if (!inventoryResult.IsSuccess || inventoryResult.Data == null)
{
    Service_ErrorHandler.HandleValidationError(
        inventoryResult.ErrorMessage ?? "Inventory lookup failed",
        nameof(Control_AdvancedInventory));
    return;
}

InventoryGrid.DataSource = inventoryResult.Data;
```

### Violation Detection
- **Search Pattern**: `await Dao_` calls in `Controls/` without subsequent `IsSuccess`/`Data` checks
- **Common Locations**: Long-running operations, advanced inventory workflows

### Remediation Steps
1. Wrap DAO calls in local variables.
2. Check `IsSuccess` and `Data`/`Value` before using results.
3. Route errors through `Service_ErrorHandler` with contextual control names.

### Instruction References
- `.github/instructions/testing-standards.instructions.md`
- `.github/instructions/security-best-practices.instructions.md`

---

## Rule CR-UI-002: Long-Running UI Operations Must Remain Asynchronous and Off the UI Thread

- **Source Spec**: `specs/Archives/002-comprehensive-database-layer/spec.md` (US2)
- **Source Tasks**: `specs/Archives/002-003-database-layer-complete/tasks.md` (T108–T111), `specs/Archives/002-comprehensive-database-layer/tasks.md` (T027–T028)
- **Implementation Status**: ✅ Validated in updated Controls; gaps remain in `Control_AdvancedInventory.cs`

### Description
Inventory and transaction workflows must use `async`/`await` with `Task.Run` (when needed) to avoid blocking the WinForms UI thread. Synchronous loops over DAO calls or `Result`/`Wait()` usage is prohibited.

### Pattern to Enforce
**Before (Anti-pattern)**
```csharp
var table = Dao_Inventory.GetAllInventoryAsync().Result;
LoadInventoryIntoGrid(table.Data);
```

**After (Compliant)**
```csharp
var result = await Dao_Inventory.GetAllInventoryAsync();
if (!result.IsSuccess || result.Data == null)
{
    Service_ErrorHandler.HandleValidationError("Inventory retrieval failed", nameof(Control_AdvancedInventory));
    return;
}

await UpdateInventoryGridAsync(result.Data);
```

### Violation Detection
- **Search Pattern**: `.Result` / `.Wait(` / `Task.Run(() => Dao_` inside UI files
- **File Types**: `Controls/**/*.cs`, `Forms/**/*.cs`

### Remediation Steps
1. Mark event handlers as `async void` or extract `async Task` helpers.
2. Await DAO operations directly and marshal UI updates via `BeginInvoke` if needed.
3. Break large synchronous loops into asynchronous batches.

---

## Rule CR-ERR-001: Use `Service_ErrorHandler` and Severity-Aware Logging

- **Source Tasks**: `specs/Archives/002-comprehensive-database-layer/tasks.md` (T036–T037), `specs/Archives/002-003-database-layer-complete/tasks.md` (T122)
- **Implementation Status**: ✅ Validated
- **Evidence Files**: `Logging/LoggingUtility.cs`, `Services/Service_ErrorHandler.cs`, multiple controls

### Description
Errors surfaced from DAO or UI workflows must be routed through `Service_ErrorHandler`, which coordinates cooldown behavior, severity mapping, and logging via `LoggingUtility.LogDatabaseError(DatabaseErrorSeverity)`. Direct `MessageBox.Show` usage is disallowed.

### Pattern to Enforce
**Before (Anti-pattern)**
```csharp
MessageBox.Show("Inventory failed");
```

**After (Compliant)**
```csharp
Service_ErrorHandler.HandleDatabaseError(
    "Inventory retrieval failed",
    DatabaseErrorSeverity.Error,
    controlName: nameof(Control_AdvancedInventory));
```

### Violation Detection
- **Search Pattern**: `MessageBox.Show` or `LoggingUtility.LogApplicationError` without severity in UI files
- **File Types**: `Controls/*.cs`, `Forms/*.cs`

### Remediation Steps
1. Replace message boxes with appropriate `Service_ErrorHandler` calls.
2. Provide severity classification and control context.
3. Ensure database exceptions include structured logging data.

---

These rules will be kept up to date as new specifications are completed. When refactoring `Control_AdvancedInventory.cs`, ensure the file complies with CR-DAO-001, CR-UI-001, CR-UI-002, and CR-ERR-001. Document any deviations with rationale and plan for remediation.