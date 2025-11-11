# Service_DebugTracer Integration Checklist

**Feature**: Comprehensive Database Layer Refactor  
**Related Requirement**: FR-012  
**Related Task**: T035  
**Purpose**: Track method entry/exit for all DAO methods with parameters and results logged

---

## Implementation Scope

**CRITICAL CLARIFICATION**: Service_DebugTracer integration should occur at the **DAO method level only**, not at the Helper_Database_StoredProcedure level.

**Rationale**: Integrating at Helper level would create excessive logging noise (~60+ procedures × multiple calls = 300+ log entries for complex operations). DAO-level integration provides optimal balance of visibility and log volume.

---

## Integration Pattern

### Standard DAO Method Template

```csharp
/// <summary>
/// [Operation description]
/// </summary>
public static async Task<Model_Dao_Result<DataTable>> OperationNameAsync(
    string param1,
    int param2)
{
    // Entry trace with parameters
    Service_DebugTracer.TraceMethodEntry(
        nameof(OperationNameAsync),
        new { param1, param2 });
    
    try
    {
        var parameters = new Dictionary<string, object>
        {
            ["Param1"] = param1,
            ["Param2"] = param2,
            ["User"] = Model_Application_Variables.User
        };

        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatus(
            Model_Application_Variables.ConnectionString,
            "stored_procedure_name",
            parameters,
            progressHelper: null,
            useAsync: true);

        // Exit trace with result status
        Service_DebugTracer.TraceMethodExit(
            nameof(OperationNameAsync),
            new { IsSuccess = result.IsSuccess, RowCount = result.Data?.Rows.Count ?? 0 });

        if (result.IsSuccess)
        {
            return Model_Dao_Result<DataTable>.Success(result.Data, "Success message");
        }
        else
        {
            return Model_Dao_Result<DataTable>.Failure(result.Message);
        }
    }
    catch (Exception ex)
    {
        // Exit trace with exception
        Service_DebugTracer.TraceMethodExit(
            nameof(OperationNameAsync),
            new { Exception = ex.Message });
        
        LoggingUtility.LogDatabaseError(ex);
        return Model_Dao_Result<DataTable>.Failure($"Operation failed: {ex.Message}", ex);
    }
}
```

---

## DAO Method Inventory (60+ Methods)

### Phase 3: User Story 1 (Task T017-T018)

**Dao_System.cs** (Task T017):
- [ ] GetDatabaseVersionAsync
- [ ] CheckConnectivityAsync
- [ ] GetSettingsAsync

**Dao_ErrorLog.cs** (Task T018):
- [ ] LogErrorAsync (with recursive prevention check)
- [ ] GetErrorsAsync
- [ ] SearchErrorsAsync

**Checkpoint US1**: 6 methods with Service_DebugTracer integration

---

### Phase 4: User Story 2 (Task T024-T026)

**Dao_Inventory.cs** (Task T024):
- [ ] GetAllInventoryAsync
- [ ] AddInventoryAsync
- [ ] RemoveInventoryAsync
- [ ] TransferInventoryAsync (multi-step transaction)
- [ ] SearchInventoryAsync
- [ ] UpdateInventoryAsync

**Dao_Transactions.cs** (Task T025):
- [ ] LogTransactionAsync
- [ ] GetTransactionHistoryAsync
- [ ] SearchTransactionsAsync

**Dao_History.cs** (Task T026):
- [ ] GetInventoryHistoryAsync
- [ ] GetRemoveHistoryAsync
- [ ] GetTransferHistoryAsync

**Checkpoint US2**: 15 methods total (6 + 9) with Service_DebugTracer integration

---

### Phase 5: User Story 3 (Task T033-T034)

**Dao_User.cs** (Task T033):
- [ ] AuthenticateUserAsync
- [ ] GetAllUsersAsync
- [ ] CreateUserAsync
- [ ] UpdateUserAsync
- [ ] DeleteUserAsync

**Dao_Part.cs** (Task T034):
- [ ] GetPartAsync
- [ ] CreatePartAsync
- [ ] UpdatePartAsync
- [ ] DeletePartAsync
- [ ] SearchPartsAsync

**Checkpoint US3**: 25 methods total (15 + 10) with Service_DebugTracer integration

---

### Phase 6: User Story 4 (Task T042-T045)

**Dao_Location.cs** (Task T042):
- [ ] GetAllLocationsAsync
- [ ] CreateLocationAsync
- [ ] UpdateLocationAsync
- [ ] DeleteLocationAsync

**Dao_Operation.cs** (Task T043):
- [ ] GetAllOperationsAsync
- [ ] CreateOperationAsync
- [ ] UpdateOperationAsync
- [ ] DeleteOperationAsync

**Dao_ItemType.cs** (Task T044):
- [ ] GetAllItemTypesAsync
- [ ] CreateItemTypeAsync
- [ ] UpdateItemTypeAsync
- [ ] DeleteItemTypeAsync

**Dao_QuickButtons.cs** (Task T045):
- [ ] GetQuickButtonsAsync
- [ ] SaveQuickButtonAsync
- [ ] DeleteQuickButtonAsync

**Checkpoint US4**: 40 methods total (25 + 15) with Service_DebugTracer integration

---

## Verification Strategy

### Per-Phase Verification

After completing each phase's DAO refactoring:

1. **Build verification**: Ensure all TraceMethodEntry/TraceMethodExit calls compile
2. **Naming verification**: Confirm `nameof()` operator used (prevents refactoring errors)
3. **Parameter logging**: Verify sensitive data (passwords, tokens) NOT logged
4. **Exception handling**: Confirm TraceMethodExit called in catch blocks

### Integration Test Verification

For each phase's integration tests:

1. **Enable Service_DebugTracer** in test configuration
2. **Execute test suite** for that phase's DAOs
3. **Review debug output**: Verify entry/exit traces appear for all methods
4. **Check trace content**: Confirm parameters and results logged correctly

---

## Task T035 Refinement

**Original Task T035**: "Add Service_DebugTracer integration to all DAO methods: TraceMethodEntry with parameters at method start, TraceMethodExit with result before return"

**Refined Approach**:

Instead of single monolithic task, integrate Service_DebugTracer **incrementally per phase**:

- **Phase 3 (US1)**: During T017-T018, add tracer calls to Dao_System and Dao_ErrorLog
- **Phase 4 (US2)**: During T024-T026, add tracer calls to Dao_Inventory, Dao_Transactions, Dao_History
- **Phase 5 (US3)**: During T033-T034, add tracer calls to Dao_User and Dao_Part
- **Phase 6 (US4)**: During T042-T045, add tracer calls to Dao_Location, Dao_Operation, Dao_ItemType, Dao_QuickButtons

**Benefit**: Avoids large merge conflicts by integrating tracer calls as each DAO is refactored, rather than retrofitting afterward.

---

## Sensitive Data Handling

**CRITICAL**: Do NOT log sensitive data in TraceMethodEntry parameters.

### Safe Parameter Logging
```csharp
// ✓ SAFE: General parameters
Service_DebugTracer.TraceMethodEntry(
    nameof(GetInventoryAsync),
    new { locationCode, includeInactive });

// ✓ SAFE: User reference (not password)
Service_DebugTracer.TraceMethodEntry(
    nameof(AuthenticateUserAsync),
    new { username }); // Password NOT included

// ✗ UNSAFE: Logging password
Service_DebugTracer.TraceMethodEntry(
    nameof(AuthenticateUserAsync),
    new { username, password }); // NEVER DO THIS
```

### Safe Result Logging
```csharp
// ✓ SAFE: Status and counts only
Service_DebugTracer.TraceMethodExit(
    nameof(GetUsersAsync),
    new { IsSuccess = result.IsSuccess, UserCount = result.Data.Rows.Count });

// ✗ UNSAFE: Full result data (may contain sensitive info)
Service_DebugTracer.TraceMethodExit(
    nameof(GetUsersAsync),
    result.Data); // NEVER DO THIS
```

---

## Performance Considerations

### Conditional Tracing

If Service_DebugTracer checks are expensive, consider wrapping in conditional:

```csharp
if (Service_DebugTracer.IsEnabled)
{
    Service_DebugTracer.TraceMethodEntry(
        nameof(OperationAsync),
        new { param1, param2 });
}
```

### Trace Level Configuration

Ensure Service_DebugTracer respects configuration:
- **Development**: Full tracing enabled
- **Staging**: Tracing enabled with reduced detail
- **Production**: Tracing disabled or error-only

---

## Completion Criteria

### Phase-Level Criteria

After each phase (3-6), verify:
- [ ] All DAO methods in that phase have TraceMethodEntry at start
- [ ] All DAO methods have TraceMethodExit before return
- [ ] All catch blocks have TraceMethodExit with exception info
- [ ] No sensitive data logged in traces
- [ ] Integration tests show expected trace output

### Final Validation (After Phase 6)

- [ ] All 40+ DAO methods have complete Service_DebugTracer integration
- [ ] Static code analysis confirms no TraceMethodEntry without TraceMethodExit
- [ ] Manual review of trace output shows useful debugging information
- [ ] Performance impact measured: tracing overhead <5ms per method call
- [ ] Documentation updated with tracer usage examples

---

## Next Steps

1. **Before starting Phase 3**: Review this checklist with development team
2. **During each phase**: Check off methods as tracer integration completes
3. **After each phase**: Run integration tests and verify trace output
4. **After Phase 6**: Perform final validation and update success criteria SC-007 (developer workflow time)

---

## References

- **Requirement**: FR-012 in spec.md
- **Task**: T035 in tasks.md
- **Pattern**: See quickstart.md Section "Common Patterns"
- **Service**: Services/Service_DebugTracer.cs (existing implementation)
