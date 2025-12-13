# Data Model: Fix MySQL Database Connection Leaks

**Feature**: 001-fix-mysql-connection-leaks  
**Date**: 2025-12-13

## Overview

This feature primarily modifies existing infrastructure components rather than introducing new data entities. The changes focus on connection management patterns and helper methods. Below are the key "entities" (components/models) involved in this feature.

## Connection Statistics Model

### ConnectionStats

**Purpose**: Represents database connection health snapshot for monitoring

**Properties**:
```csharp
public class ConnectionStats
{
    public string ServerAddress { get; set; }      // MySQL server address
    public int OpenConnections { get; set; }       // Number of connections currently open from this app
    public DateTime Timestamp { get; set; }        // When stats were captured
    public bool IsHealthy { get; set; }            // True if OpenConnections == 0 when app is idle
    public string? WarningMessage { get; set; }    // Set if IsHealthy == false
}
```

**Validation Rules**:
- ServerAddress: Required, non-empty string
- OpenConnections: ≥ 0
- Timestamp: Must be UTC
- IsHealthy: False if OpenConnections > 0 when app should be idle
- WarningMessage: Set to "Potential connection leak: {count} connections remain open" when IsHealthy == false

**State Transitions**: N/A (immutable snapshot)

**Relationships**:
- Created by: Helper_Database_ConnectionMonitor.GetConnectionStatsAsync()
- Logged by: MainForm timer every 5 minutes

---

## Analytics Data Model (Existing)

### Transaction Analytics Query Result

**Purpose**: Results from md_analytics_GetTransactionsByRange stored procedure

**Properties** (returned as DataTable):
```
- TransactionID: INT
- UserID: INT  
- TransactionDate: DATETIME
- PartID: INT
- Quantity: INT
- OperationType: VARCHAR(50)  // 'Add', 'Remove', 'Transfer'
- Location: VARCHAR(100)
```

**Source**: Replaces inline SQL in Service_Analytics (3 locations)

**Validation**: Handled by stored procedure (date range validation, user permission checks)

---

### User Activity Query Result

**Purpose**: Results from md_analytics_GetUsersByDateRange stored procedure

**Properties** (returned as DataTable):
```
- UserID: INT
- UserName: VARCHAR(100)
- ActivityCount: INT  // Number of transactions in date range
- LastActivityDate: DATETIME
```

**Source**: Replaces inline SQL in Service_Analytics

**Validation**: Date range must be ≤ 1 year, StartDate < EndDate

---

## Helper Method Signatures (Modified/New)

### ExecuteReaderAsync (Removed)

**Purpose**: Method completely removed to prevent connection leaks

**Rationale**: 
- Returned MySqlDataReader keeping connections open
- All 4 callers replaced with ExecuteDataTableWithStatusAsync
- Complete removal prevents future misuse
- Compile-time enforcement (no obsolete warnings to ignore)

**Migration Path**: All usages replaced with ExecuteDataTableWithStatusAsync which:
- Loads data into DataTable in memory
- Automatically disposes connection
- Returns Model_Dao_Result<DataTable> for consistency

---

### ExecuteRawSqlAsync (New)

**Purpose**: Execute raw SQL for database migrations while maintaining centralized access

**Signature**:
```csharp
/// <summary>
/// Executes raw SQL (DDL/DML) for database migrations.
/// ARCHITECTURAL EXCEPTION: Only for Service_Migration schema changes.
/// </summary>
/// <param name="connectionString">Database connection string (Pooling=false enforced)</param>
/// <param name="sql">Raw SQL statement (ALTER TABLE, CREATE INDEX, etc.)</param>
/// <param name="parameters">Optional parameters for parameterized queries</param>
/// <returns>Model_Dao_Result with rows affected count</returns>
public static async Task<Model_Dao_Result<int>> ExecuteRawSqlAsync(
    string connectionString,
    string sql,
    Dictionary<string, object>? parameters = null)
```

**Validation Rules**:
- connectionString: Must contain "Pooling=false"
- sql: Non-empty, trimmed
- Returns Model_Dao_Result<int> (IsSuccess, Data = rows affected, ErrorMessage)

**Error Handling**: Catches all exceptions, logs via LoggingUtility, returns failure Model_Dao_Result

---

## Configuration Changes

### Connection String (Modified)

**Location**: Helper_Database_Variables.cs

**Before**:
```csharp
ConnectionString = $"Server={server};Database={database};Uid={user};Pwd={password};...";
```

**After**:
```csharp
ConnectionString = $"Server={server};Database={database};Uid={user};Pwd={password};Pooling=false;...";
```

**Rationale**: Immediate disposal, no connection pooling per Constitution Principle V

---

### SQL Server Connection String (Modified)

**Location**: Service_VisualDatabase.cs (GetConnectionString method)

**After**:
```csharp
var builder = new SqlConnectionStringBuilder
{
    DataSource = _serverAddress,
    InitialCatalog = _databaseName,
    UserID = _userName,
    Password = _password,
    ConnectTimeout = 5,
    ApplicationName = "MTM_WIP_App_VisualDashboard",
    TrustServerCertificate = true,
    Pooling = false  // NEW: Immediate disposal
};
```

---

## Stored Procedure Parameters

### md_analytics_GetTransactionsByRange

**Input Parameters**:
- `p_StartDate` (DATETIME): Start of date range (required)
- `p_EndDate` (DATETIME): End of date range (required)

**Output Parameters**:
- `p_Status` (INT): 0 = success, non-zero = error code
- `p_ErrorMsg` (VARCHAR(500)): User-friendly error message

**Returns**: Result set (DataTable) with transaction analytics

**Validation**: StartDate < EndDate, date range ≤ 1 year

---

### md_analytics_GetUsersByDateRange

**Input Parameters**:
- `p_StartDate` (DATETIME): Start of date range (required)
- `p_EndDate` (DATETIME): End of date range (required)

**Output Parameters**:
- `p_Status` (INT): 0 = success, non-zero = error code
- `p_ErrorMsg` (VARCHAR(500)): User-friendly error message

**Returns**: Result set (DataTable) with user activity statistics

**Validation**: StartDate < EndDate, date range ≤ 1 year

---

## Entity Relationships

```
Helper_Database_StoredProcedure
├── ExecuteReaderAsync [REMOVED - all callers migrated]
├── ExecuteDataTableWithStatusAsync (standard for queries)
├── ExecuteRawSqlAsync [NEW] → used by Service_Migration
└── All methods use Model_Dao_Result<T> return pattern

Helper_Database_ConnectionMonitor [NEW]
└── GetConnectionStatsAsync() → ConnectionStats model

Service_Analytics
├── Uses md_analytics_GetTransactionsByRange
├── Uses md_analytics_GetUsersByDateRange
└── Returns data via Model_Dao_Result<DataTable>

MainForm Timer (existing)
└── Calls Helper_Database_ConnectionMonitor.GetConnectionStatsAsync() every 5 minutes
    └── Logs ConnectionStats to CSV via LoggingUtility
```

---

## Database Schema Changes

**None** - No table modifications required. Only new stored procedures:
- `md_analytics_GetTransactionsByRange`
- `md_analytics_GetUsersByDateRange`

Both procedures query existing tables (transactions, users, inventory) without schema changes.

---

## Notes

- All components follow Model_Dao_Result<T> pattern for consistency
- ConnectionStats is in-memory only (not persisted to database)
- Stored procedures use MySQL 5.7.24 compatible syntax (no CTEs, window functions, JSON)
- All database connections enforce `Pooling=false` for immediate disposal
