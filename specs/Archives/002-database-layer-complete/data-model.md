# Phase 1: Data Model - Comprehensive Database Layer Standardization

**Generated**: 2025-10-17  
**Feature**: Comprehensive Database Layer Standardization  
**Branch**: `002-003-database-layer-complete`

## Purpose

Document the entities, relationships, and validation rules that underpin the standardized database layer, merging the original phase 1-2 definitions with the phase 2.5 refresh scope. This file is the authoritative reference for DAO, helper, and stored procedure contracts.

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
- `Message` MUST NOT be null or empty.
- If `IsSuccess == false`, `Message` MUST describe the failure reason.
- If `Exception` is not null, `IsSuccess` MUST be false.

**Factory Methods**:
```csharp
public static Model_Dao_Result Success(string message = "Operation completed successfully")
public static Model_Dao_Result Failure(string message, Exception? exception = null)
```

**Usage**:
- Return type for DAO methods that perform INSERT/UPDATE/DELETE operations.
- Consumed by UI layer for user messaging via Service_ErrorHandler.
- Logged with severity metadata when `Exception` supplied.

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
- All Model_Dao_Result validation rules apply.
- If `IsSuccess == true`, `Data` SHOULD NOT be null (except for zero-row queries).
- If `IsSuccess == false`, `Data` MUST be null.
- Callers MUST check `IsSuccess` before accessing `Data`.

**Factory Methods**:
```csharp
public static new Model_Dao_Result<T> Success(T data, string message = "Operation completed successfully")
public static new Model_Dao_Result<T> Failure(string message, Exception? exception = null)
```

**Common Type Parameters**:
- `Model_Dao_Result<DataTable>`: Multi-row SELECT results.
- `Model_Dao_Result<DataRow>`: Single-row results.
- `Model_Dao_Result<int>` / `Model_Dao_Result<long>`: Scalar counts or identifiers.
- `Model_Dao_Result<bool>`: Existence checks and feature toggles.

---

### Helper_Database_StoredProcedure (Central Helper)

**Purpose**: Provides uniform routing to stored procedures with automatic parameter prefix detection, retry logic, and status/error handling.

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
- Inserts, updates, deletes, and mixed DML.
- Returns status/error message envelope.

#### ExecuteDataTableWithStatus
```csharp
public static async Task<Model_Dao_Result<DataTable>> ExecuteDataTableWithStatus(
    string connectionString,
    string storedProcedureName,
    Dictionary<string, object> parameters,
    Helper_StoredProcedureProgress? progressHelper = null,
    bool useAsync = true)
```
- SELECT queries returning multi-row result sets.
- Wraps DataTable payload in Model_Dao_Result<T>.

#### ExecuteScalarWithStatus
```csharp
public static async Task<Model_Dao_Result<T>> ExecuteScalarWithStatus<T>(
    string connectionString,
    string storedProcedureName,
    Dictionary<string, object> parameters,
    Helper_StoredProcedureProgress? progressHelper = null,
    bool useAsync = true)
```
- Scalar SELECT queries (counts, existence checks, IDs).

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
- Procedures returning custom OUT parameters beyond `p_Status` / `p_ErrorMsg`.

**Shared Behaviors**:
- Parameter prefix detection via cached INFORMATION_SCHEMA data.
- OUT parameter processing (`p_Status`, `p_ErrorMsg`).
- Connection pooling (MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30s).
- Retry logic for transient MySQL error numbers (1205, 1213, 2006, 2013).
- Optional progress reporting hook.
- Execution timing with threshold warnings.

---

### ParameterPrefixCache (Internal State)

**Purpose**: Startup-populated dictionary enabling automatic prefix inference.

**Structure**:
```csharp
Dictionary<string, Dictionary<string, string>>
// Outer key: stored procedure name (case-insensitive)
// Inner key: parameter name with prefix
// Inner value: parameter mode (IN, OUT, INOUT)
```

**Lookup Algorithm**:
1. Attempt to match `p_`, then `in_`, then `o_` plus the PascalCase name.
2. If cache miss, fall back to convention (`in_` for Transfer/transaction procedures, otherwise `p_`).
3. Automation agent maintains override data via `sys_parameter_prefix_override` when edge cases arise.

---

### DAO Class Structure

All DAO classes follow the same structure: static async methods returning Model_Dao_Result variants, no shared mutable state, and deterministic logging/exception handling.

**DAO Inventory** (`Data/Dao_Inventory.cs`)
- Operations: search, add, remove, transfer, analytics.
- Stored procedure prefix: `inv_`.
- Categories: Query, Modification, Multi-step Transactions.

**DAO Transactions** (`Data/Dao_Transactions.cs`)
- Operations: add transaction, smart search, analytics.
- Prefix: `inv_transactions_`.

**DAO User / DAO Role** (`Data/Dao_User.cs`, `Data/Dao_Role.cs`)
- Operations: authenticate, CRUD users/roles, permissions.
- Integration with Developer role gating (T113c).

**DAO Part / Location / ItemType / Operation**
- Master data CRUD with consistent validation and transaction handling for cascades.

**DAO QuickButtons / System / ErrorLog / History**
- User settings, diagnostics, logging, historical queries.
- ErrorLog DAO integrates recursion guard and severity mapping.

---

### StoredProcedureResult (Helper Internal)

**Purpose**: Intermediate structure representing stored procedure execution prior to Model_Dao_Result wrapping.

**Fields**:
| Field | Type | Description |
|-------|------|-------------|
| Payload | object? | DataTable, scalar value, or custom OUT parameters |
| StatusCode | int | `p_Status` value (0: success, 1: success/no data, negative: error) |
| StatusMessage | string | `p_ErrorMsg` value |
| IsSuccess | bool | Derived from StatusCode |
| Exception | Exception? | Execution exception captured by helper |

**Processing Flow**:
1. Execute stored procedure with enriched parameters.
2. Read OUT parameters (`p_Status`, `p_ErrorMsg`).
3. Compose `StoredProcedureResult`.
4. Convert to Model_Dao_Result/Model_Dao_Result<T> in helper before returning to caller.

---

### Error Log Entry

**Purpose**: Represents rows in `log_error` and underpins automated severity reporting.

**Fields**:
| Field | Type | Description |
|-------|------|-------------|
| User | string | Username or `SYSTEM` when not authenticated |
| Severity | string | Critical / Error / Warning |
| ErrorType | string | Exception type or diagnostic category |
| ErrorMessage | string | User-friendly message |
| StackTrace | string | Serialized stack trace when available |
| ModuleName | string | Source file or logical component |
| MethodName | string | Method where error originated |
| AdditionalInfo | string | JSON context (parameters, environment) |
| MachineName | string | Hostname |
| OSVersion | string | Windows version |
| AppVersion | string | Application version |
| ErrorTime | DateTime | Timestamp (UTC) |

**Severity Rules**:
- Critical: Startup/database unavailable, data corruption risk.
- Error: Operation failed but application remains stable.
- Warning: Unexpected condition handled gracefully (e.g., slow query).

---

### Transaction History Entry

**Purpose**: Captures audit trail for inventory movements in `inv_transaction`.

**Fields**:
| Field | Type | Description |
|-------|------|-------------|
| TransactionType | string | IN / OUT / TRANSFER |
| PartID | string | Affected part number |
| FromLocation | string? | Source location (null for IN) |
| ToLocation | string? | Destination location (null for OUT) |
| Operation | string | Manufacturing operation (e.g., 90, 100, 110) |
| Quantity | decimal | Movement quantity |
| User | string | Initiating user |
| ItemType | string | Inventory classification |
| BatchNumber | string? | Optional batch identifier |
| Notes | string? | Optional free-form notes |
| TransactionDate | DateTime | Timestamp |

**Validation Rules**:
- `TransactionType` must be IN/OUT/TRANSFER.
- `FromLocation` required when TransactionType == OUT or TRANSFER.
- `ToLocation` required when TransactionType == IN or TRANSFER.
- `Quantity` must be positive.

---

## Relationships & State

### DAO → Helper → Stored Procedure Flow
```
Form/Control/Service
    ↓ async call
Dao_[Domain].MethodAsync
    ↓ helper invocation
Helper_Database_StoredProcedure.Execute*
    ↓ prefix cache
MySqlCommand + Stored Procedure
    ↓ OUT parameters & payload
StoredProcedureResult
    ↓ wrap
Model_Dao_Result / Model_Dao_Result<T>
    ↓ UI error handling & logging
Service_ErrorHandler / LoggingUtility
```

### Multi-Step Transaction Lifecycle
1. Open connection and begin transaction.
2. Execute validation procedure(s); rollback and return failure on error.
3. Execute each DML step; rollback immediately if any fail.
4. Log history entry once core steps succeed.
5. Commit transaction and return success message.

### Documentation & Automation Touchpoints
- Procedure refactors (T113–T118) update headers, DAO XML, standards, and quickstart entries tracked via Documentation Update Matrix.
- Automation agent (T106a/T106b) validates discovery outputs before any procedure edits commence.

---

## Next Steps

Use this model to:
- Guide DAO method implementations and async migrations.
- Validate stored procedure parameters, outputs, and transaction handling.
- Align documentation and analyzer rules with entity contracts.
- Support automation workflows that rely on consistent data structures (e.g., CSV analysis, validation checklists).
