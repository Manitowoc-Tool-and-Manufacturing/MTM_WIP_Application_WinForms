# Phase 1: Data Model - Comprehensive Database Layer Refactor

**Generated**: 2025-10-13  
**Feature**: Comprehensive Database Layer Refactor  
**Branch**: `002-comprehensive-database-layer`

## Purpose

Document the data structures, relationships, and validation rules for the refactored database access layer. This model defines the contracts between DAOs, helpers, and calling code.

## Core Entities

### Model_Dao_Result (Base Wrapper)

**Purpose**: Encapsulates operation outcomes for non-data-returning operations (INSERT, UPDATE, DELETE).

**Fields**:
| Field | Type | Required | Description |
|-------|------|----------|-------------|
| IsSuccess | bool | Yes | True if operation succeeded, false otherwise |
| Message | string | Yes | Human-readable outcome description |
| Exception | Exception? | No | Exception object if operation failed, null otherwise |

**Validation Rules**:
- `Message` MUST NOT be null or empty
- If `IsSuccess == false`, `Message` MUST describe the failure reason
- If `Exception` is not null, `IsSuccess` MUST be false

**Factory Methods**:
```csharp
public static Model_Dao_Result Success(string message = "Operation completed successfully")
public static Model_Dao_Result Failure(string message, Exception? exception = null)
```

**Usage**:
- Return type for DAO methods that perform INSERT/UPDATE/DELETE operations
- Consumed by UI layer to show success/error messages
- Integrated with Service_ErrorHandler for error display

---

### Model_Dao_Result<T> (Generic Data Wrapper)

**Purpose**: Extends Model_Dao_Result to include query result data for SELECT operations.

**Fields**:
| Field | Type | Required | Description |
|-------|------|----------|-------------|
| IsSuccess | bool | Yes | Inherited from Model_Dao_Result |
| Message | string | Yes | Inherited from Model_Dao_Result |
| Exception | Exception? | No | Inherited from Model_Dao_Result |
| Data | T? | No | Query result data, null if operation failed |

**Validation Rules**:
- All Model_Dao_Result validation rules apply
- If `IsSuccess == true`, `Data` SHOULD NOT be null (except for queries returning zero rows)
- If `IsSuccess == false`, `Data` MUST be null
- Callers MUST check `IsSuccess` before accessing `Data`

**Factory Methods**:
```csharp
public static Model_Dao_Result<T> Success(T data, string message = "Operation completed successfully")
public static Model_Dao_Result<T> Failure(string message, Exception? exception = null)
```

**Common Type Parameters**:
- `Model_Dao_Result<DataTable>`: Most common for SELECT queries returning multiple rows
- `Model_Dao_Result<DataRow>`: Single row results
- `Model_Dao_Result<int>`: Scalar results (counts, IDs)
- `Model_Dao_Result<bool>`: Boolean results (exists checks)

---

### Helper_Database_StoredProcedure (Central Helper)

**Purpose**: Provides uniform interface for executing stored procedures with automatic parameter prefix detection and error handling.

**Key Methods**:

#### ExecuteNonQueryWithStatus
```csharp
public static async Task<Model_Dao_Result> ExecuteNonQueryWithStatus(
    string connectionString,
    string storedProcedureName,
    Dictionary<string, object> parameters,
    Helper_StoredProcedureProgress? progressHelper = null,
    bool useAsync = true)
```

**Purpose**: Execute INSERT/UPDATE/DELETE stored procedures  
**Returns**: Model_Dao_Result with success/failure status  
**Usage**: Inventory additions, user updates, transaction logging

#### ExecuteDataTableWithStatus
```csharp
public static async Task<Model_Dao_Result<DataTable>> ExecuteDataTableWithStatus(
    string connectionString,
    string storedProcedureName,
    Dictionary<string, object> parameters,
    Helper_StoredProcedureProgress? progressHelper = null,
    bool useAsync = true)
```

**Purpose**: Execute SELECT queries returning multiple rows  
**Returns**: Model_Dao_Result<DataTable> with query results  
**Usage**: Inventory searches, user lists, transaction history

#### ExecuteScalarWithStatus
```csharp
public static async Task<Model_Dao_Result<T>> ExecuteScalarWithStatus<T>(
    string connectionString,
    string storedProcedureName,
    Dictionary<string, object> parameters,
    Helper_StoredProcedureProgress? progressHelper = null,
    bool useAsync = true)
```

**Purpose**: Execute queries returning single scalar value  
**Returns**: Model_Dao_Result<T> with scalar result (int, string, bool, etc.)  
**Usage**: Row counts, existence checks, ID lookups

#### ExecuteWithCustomOutput
```csharp
public static async Task<Model_Dao_Result<Dictionary<string, object>>> ExecuteWithCustomOutput(
    string connectionString,
    string storedProcedureName,
    Dictionary<string, object> parameters,
    string[] outputParameterNames,
    Helper_StoredProcedureProgress? progressHelper = null,
    bool useAsync = true)
```

**Purpose**: Execute procedures with non-standard output parameters  
**Returns**: Model_Dao_Result<Dictionary<string, object>> with output parameter values  
**Usage**: Special operations returning multiple OUT parameters

**Shared Behavior**:
- Automatic parameter prefix detection (p_, in_, o_) via INFORMATION_SCHEMA cache
- OUT p_Status and OUT p_ErrorMsg processing
- Connection pooling (MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30s)
- Retry logic for transient errors (deadlock, timeout, connection lost)
- Progress reporting integration via Helper_StoredProcedureProgress
- Performance monitoring with configurable thresholds
- Comprehensive error logging via LoggingUtility

---

### ParameterPrefixCache (Internal Helper State)

**Purpose**: Cache INFORMATION_SCHEMA.PARAMETERS query results at application startup for fast parameter prefix detection.

**Structure**:
```csharp
private static readonly Dictionary<string, Dictionary<string, string>> _procedureParameterCache;
// Outer key: Stored procedure name
// Inner key: Parameter name (with prefix)
// Inner value: Parameter mode (IN, OUT, INOUT)
```

**Initialization**:
- Executed during application startup in Program.cs
- Query: `SELECT ROUTINE_NAME, PARAMETER_NAME, PARAMETER_MODE FROM INFORMATION_SCHEMA.PARAMETERS WHERE ROUTINE_SCHEMA = DATABASE() AND ROUTINE_TYPE = 'PROCEDURE'`
- Startup cost: ~100-200ms for 60+ stored procedures
- Fallback: Convention-based detection if query fails (p_ default, in_ for Transfer*/transaction* procedures)

**Lookup Pattern**:
```csharp
private static string GetParameterPrefix(string procedureName, string paramName)
{
    if (_procedureParameterCache.TryGetValue(procedureName, out var parameters))
    {
        if (parameters.ContainsKey($"p_{paramName}")) return "p_";
        if (parameters.ContainsKey($"in_{paramName}")) return "in_";
        if (parameters.ContainsKey($"o_{paramName}")) return "o_";
    }
    
    // Fallback convention
    if (procedureName.Contains("Transfer") || procedureName.Contains("transaction"))
        return "in_";
    
    return "p_"; // Default
}
```

---

### DAO Class Structure (12 Classes)

All DAO classes follow identical structure to ensure consistency and maintainability.

**Standard DAO Pattern**:
```csharp
public static class Dao_[Domain]
{
    // No instance state - all methods static for simplicity
    
    /// <summary>
    /// [Operation description]
    /// </summary>
    /// <param name="[param1]">[Description]</param>
    /// <returns>Model_Dao_Result or Model_Dao_Result&lt;T&gt; with operation outcome</returns>
    public static async Task<Model_Dao_Result<T>> [OperationName]Async([parameters])
    {
        try
        {
            var result = await Helper_Database_StoredProcedure.[ExecutionMethod](
                Model_Application_Variables.ConnectionString,
                "[stored_procedure_name]",
                new Dictionary<string, object>
                {
                    ["Parameter1"] = value1, // No p_ prefix
                    ["Parameter2"] = value2,
                    ["User"] = Model_Application_Variables.User,
                    ["DateTime"] = DateTime.Now
                },
                progressHelper: null, // Or passed from caller
                useAsync: true);
            
            if (result.IsSuccess)
                return Model_Dao_Result<T>.Success(result.Data, $"[Success message]");
            else
                return Model_Dao_Result<T>.Failure(result.Message);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            return Model_Dao_Result<T>.Failure($"[Operation] failed: {ex.Message}", ex);
        }
    }
}
```

**12 DAO Classes**:
| DAO Class | Domain | Primary Operations | Stored Procedure Prefix |
|-----------|--------|-------------------|------------------------|
| Dao_Inventory | Inventory management | Get, Add, Remove, Transfer, Search | inv_inventory_* |
| Dao_User | User authentication | Authenticate, GetAll, Create, Update | user_* |
| Dao_Transactions | Transaction logging | Log, GetHistory, Search | inv_transaction_* |
| Dao_Part | Part master data | Get, Create, Update, Delete, Search | part_* |
| Dao_Location | Location master data | Get, Create, Update, Delete | location_* |
| Dao_Operation | Operation master data | Get, Create, Update, Delete | operation_* |
| Dao_ItemType | Item type master data | Get, Create, Update, Delete | itemtype_* |
| Dao_QuickButtons | User preferences | Get, Save, Delete | quickbutton_* |
| Dao_History | Historical queries | GetInventoryHistory, GetRemoveHistory, GetTransferHistory | inv_history_* |
| Dao_ErrorLog | Error logging | LogError, GetErrors, Search | log_error_* |
| Dao_System | System metadata | GetVersion, CheckConnectivity, GetSettings | system_* |

---

### StoredProcedureResult (Helper Internal)

**Purpose**: Internal intermediate result from stored procedure execution before wrapping in Model_Dao_Result.

**Fields**:
| Field | Type | Description |
|-------|------|-------------|
| Payload | object | DataTable, scalar value, or output parameters |
| StatusCode | int | OUT p_Status value (0=success, 1=no data, -1=error) |
| StatusMessage | string | OUT p_ErrorMsg value |
| IsSuccess | bool | Computed from StatusCode (0 or 1 = success) |
| Exception | Exception? | Exception if operation threw |

**Processing Flow**:
1. Execute stored procedure
2. Read OUT p_Status and OUT p_ErrorMsg parameters
3. Map StatusCode to IsSuccess (0 or 1 → true, otherwise false)
4. Create StoredProcedureResult with Payload and status
5. Wrap in Model_Dao_Result or Model_Dao_Result<T> for return to caller

---

### Error Log Entry (Database Entity)

**Purpose**: Represents a row in the `log_error` table for troubleshooting and audit.

**Fields**:
| Field | Type | Description |
|-------|------|-------------|
| User | string | Username who triggered the error |
| Severity | string | Critical / Error / Warning (clarification Q5) |
| ErrorType | string | Exception type name |
| ErrorMessage | string | User-friendly error description |
| StackTrace | string | Full stack trace for debugging |
| ModuleName | string | Source file name |
| MethodName | string | Method where error occurred |
| AdditionalInfo | string | Context data (JSON serialized) |
| MachineName | string | Computer name |
| OSVersion | string | Windows version |
| AppVersion | string | Application version |
| ErrorTime | DateTime | Timestamp when error occurred |

**Severity Levels**:
- **Critical**: Data integrity risk or app cannot continue (e.g., database unavailable at startup, OutOfMemoryException)
- **Error**: Operation failed, user sees error message (e.g., DAO operation failure, validation error)
- **Warning**: Unexpected but handled, no user impact (e.g., slow query threshold exceeded, retry successful)

**Validation Rules**:
- `User` MUST NOT be null (use "SYSTEM" for startup errors before authentication)
- `Severity` MUST be one of: "Critical", "Error", "Warning"
- `ErrorMessage` MUST be user-friendly (no technical jargon or stack traces)
- `StackTrace` MAY be null for warnings that don't have exceptions

---

### Transaction History (Database Entity)

**Purpose**: Represents a row in the `inv_transaction` table for audit trail.

**Fields**:
| Field | Type | Description |
|-------|------|-------------|
| TransactionType | string | IN / OUT / TRANSFER |
| PartID | string | Part identifier |
| FromLocation | string? | Source location (null for IN transactions) |
| ToLocation | string? | Destination location (null for OUT transactions) |
| Operation | string | Manufacturing operation (90, 100, 110, etc.) |
| Quantity | int | Quantity moved (positive integer) |
| User | string | Username who performed transaction |
| ItemType | string | Item type classification |
| BatchNumber | string? | Batch identifier (optional) |
| Notes | string? | User-entered notes (optional) |
| TransactionDate | DateTime | Timestamp of transaction |

**Validation Rules**:
- `TransactionType` MUST be one of: "IN", "OUT", "TRANSFER"
- If `TransactionType == "IN"`: `FromLocation` MUST be null, `ToLocation` MUST NOT be null
- If `TransactionType == "OUT"`: `ToLocation` MUST be null, `FromLocation` MUST NOT be null
- If `TransactionType == "TRANSFER"`: Both `FromLocation` and `ToLocation` MUST NOT be null
- `Quantity` MUST be positive integer
- `User` MUST NOT be null
- `PartID`, `Operation`, `ItemType` MUST NOT be null

---

## Relationships

### DAO → Helper → Stored Procedure Flow

```
Calling Code (Form/Control/Service)
    ↓ async/await
Dao_[Domain].[Operation]Async()
    ↓ async/await
Helper_Database_StoredProcedure.[ExecutionMethod]()
    ↓
ParameterPrefixCache.GetParameterPrefix() [lookup cached prefixes]
    ↓
MySqlConnection (pooled connection)
    ↓
MySqlCommand.ExecuteAsync() [execute stored procedure]
    ↓
Stored Procedure (database)
    ↓ OUT p_Status, OUT p_ErrorMsg
MySqlCommand.Parameters[p_Status, p_ErrorMsg]
    ↓
StoredProcedureResult (internal)
    ↓
Model_Dao_Result or Model_Dao_Result<T> (wrapped)
    ↓ return
Calling Code (check IsSuccess, use Data, show Message)
```

### Transaction Flow for Multi-Step Operations

```
Dao_[Domain].MultiStepOperationAsync()
    ↓
MySqlConnection.BeginTransactionAsync()
    ↓
Step 1: Execute stored procedure (pass transaction)
    ↓ Check IsSuccess
Step 2: Execute stored procedure (pass transaction)
    ↓ Check IsSuccess
Step 3: Execute stored procedure (pass transaction)
    ↓ Check IsSuccess
...
    ↓ All steps succeeded?
Yes: MySqlTransaction.CommitAsync()
No: MySqlTransaction.RollbackAsync()
    ↓
Return Model_Dao_Result with final outcome
```

### Error Handling Flow

```
Dao method throws exception
    ↓
Catch block in DAO
    ↓
LoggingUtility.LogDatabaseError(ex)
    ↓
Dao_ErrorLog.LogErrorAsync() [recursive prevention check]
    ↓
log_error table INSERT
    ↓
Return Model_Dao_Result.Failure(message, ex)
    ↓
Calling code checks IsSuccess
    ↓
Service_ErrorHandler.HandleException() [if UI display needed]
    ↓
User sees error dialog with retry/close options
```

---

## State Transitions

### Connection Pool State Machine

```
[Idle Connection]
    ↓ Request from DAO
[Connection Acquired]
    ↓ Execute stored procedure
[Active Query]
    ↓ Query completes
[Connection Released]
    ↓ Return to pool
[Idle Connection]

Timeout branch:
[Active Query] -- 30s timeout --> [Connection Terminated] --> [Removed from pool]

Error branch:
[Active Query] -- Exception --> [Connection Rollback] --> [Connection Released (healthy)] --> [Idle Connection]
```

### DAO Method Execution State

```
[Entry]
    ↓
[Parameter Validation]
    ↓ Valid?
No: [Return Model_Dao_Result.Failure (validation error)]
Yes: ↓
[Call Helper_Database_StoredProcedure]
    ↓
[Stored Procedure Execution]
    ↓ Success (p_Status = 0 or 1)?
Yes: [Return Model_Dao_Result.Success with data]
No: [Return Model_Dao_Result.Failure with p_ErrorMsg]
    ↓ Exception?
Yes: [Log error, Return Model_Dao_Result.Failure with exception]
    ↓
[Exit]
```

---

## Validation Rules Summary

### Parameter Naming
- **MySQL**: Use p_ prefix (e.g., p_UserID, p_PartNumber)
- **C#**: Remove p_ prefix (e.g., UserID, PartNumber)
- **Casing**: PascalCase for all parameter names

### Model_Dao_Result Construction
- **Success**: Always provide meaningful message, Data may be null for zero-row queries
- **Failure**: Always provide user-friendly message, Exception optional but recommended

### Transaction Management
- **Multi-step**: Use explicit MySqlTransaction for all multi-operation methods
- **Rollback**: Automatic on exception or any step failure
- **Commit**: Only after all steps succeed

### Error Logging
- **Severity**: Always specify Critical/Error/Warning based on impact
- **User context**: Include username (use "SYSTEM" for pre-authentication errors)
- **Recursive prevention**: Check if error logging itself fails, fallback to file logging

### Performance Monitoring
- **Thresholds**: Query (500ms), Modification (1000ms), Batch (5000ms), Report (2000ms)
- **Logging**: Warn when threshold exceeded, include operation name and elapsed time
- **Categories**: Tag each DAO method with appropriate OperationCategory

---

## Next Steps

This data model serves as the contract for Phase 2 task decomposition. Implementation tasks will:
1. Create Model_Dao_Result.cs with Model_Dao_Result and Model_Dao_Result<T> classes
2. Refactor Helper_Database_StoredProcedure with parameter prefix detection
3. Implement ParameterPrefixCache initialization in Program.cs
4. Refactor all 12 DAO classes to async methods returning Model_Dao_Result variants
5. Migrate 100+ call sites to async/await patterns
6. Create integration test infrastructure with per-test-class transactions
7. Update documentation and developer quickstart guide
