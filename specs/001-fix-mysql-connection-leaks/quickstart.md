# Quickstart Guide: Fix MySQL Database Connection Leaks

**Feature**: 001-fix-mysql-connection-leaks  
**Date**: 2025-12-13  
**Target Audience**: Developers implementing this feature

## Prerequisites

- .NET 8.0 SDK installed
- Visual Studio 2022 or VS Code with C# extension
- MySQL 5.7.24 server running (localhost:3306)
- MAMP or equivalent MySQL server management tool
- Access to production database: `mtm_wip_application_winforms`
- Serena MCP server running (optional but recommended for code navigation)

## Quick Start (< 5 minutes)

### 1. Setup Development Environment

```powershell
# Navigate to repository root
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms

# Checkout feature branch
git checkout 001-fix-mysql-connection-leaks

# Restore dependencies
dotnet restore MTM_WIP_Application_Winforms.csproj

# Build to verify setup
dotnet build MTM_WIP_Application_Winforms.csproj
```

### 2. Verify Constitution Compliance Baseline

```powershell
# Run constitution validation (should show known violations)
.\.specify\scripts\powershell\validate-constitution-compliance.ps1

# Expected output:
# - Service_Analytics: 3 direct MySqlConnection instances
# - Service_Migration: 8 direct MySqlConnection instances
# - Service_ErrorReportSync: 3 direct MySqlConnection instances
```

### 3. Key Files to Modify

| File | Change Type | Description |
|------|-------------|-------------|
| `Helpers/Helper_Database_StoredProcedure.cs` | MODIFY | Mark ExecuteReaderAsync [Obsolete], add ExecuteRawSqlAsync |
| `Helpers/Helper_Database_Variables.cs` | MODIFY | Add Pooling=false to connection strings |
| `Helpers/Helper_Database_ConnectionMonitor.cs` | CREATE | New connection lifecycle monitoring |
| `Services/Analytics/Service_Analytics.cs` | REFACTOR | Replace inline SQL with stored procedure calls |
| `Services/Maintenance/Service_Migration.cs` | REFACTOR | Replace direct MySqlConnection with ExecuteRawSqlAsync |
| `Forms/MainForm/MainForm.cs` | MODIFY | Add connection monitoring to timer |
| `Database/UpdatedStoredProcedures/md_analytics_*.sql` | CREATE | New analytics stored procedures |

## Development Workflow

### Phase 1: Replace All ExecuteReaderAsync Callers

**Step 1a: Replace Service_Analytics usages (3 locations)**

```csharp
// File: Services/Analytics/Service_Analytics.cs
// Replace each occurrence (3 total)

// OLD CODE (to be deleted):
// await using (var connection = new MySqlConnection(Model_Application_Variables.ConnectionString))
// {
//     await connection.OpenAsync();
//     using var cmd = new MySqlCommand(inlineSQL, connection);
//     using (var reader = await cmd.ExecuteReaderAsync())
//     { ... process reader ... }
// }

// NEW CODE:
var parameters = new Dictionary<string, object>
{
    { "StartDate", startDate },
    { "EndDate", endDate }
};

var result = await Helper_Database_StoredProcedure
    .ExecuteDataTableWithStatusAsync(
        "md_analytics_GetTransactionsByRange",
        parameters);

if (!result.IsSuccess)
{
    Service_ErrorHandler.ShowUserError(result.ErrorMessage);
    return;
}

// Process result.Data (DataTable) instead of reader
foreach (DataRow row in result.Data.Rows)
{
    // Access columns: row["ColumnName"]
}
```

**Step 1b: Replace Service_Migration usage (1 location)**

```csharp
// File: Services/Maintenance/Service_Migration.cs
// Line ~224: Replace ExecuteReaderAsync with ExecuteDataTableWithStatusAsync

// OLD:
// using var reader = await cmd.ExecuteReaderAsync();
// while (await reader.ReadAsync()) { ... }

// NEW:
var result = await Helper_Database_StoredProcedure
    .ExecuteDataTableWithStatusAsync(
        procedureName,
        parameters);

if (!result.IsSuccess)
{
    LoggingUtility.Log($"Migration query failed: {result.ErrorMessage}");
    return;
}

foreach (DataRow row in result.Data.Rows)
{
    // Access columns as needed
}
```

**Step 1c: Remove ExecuteReaderAsync method**

```csharp
// File: Helpers/Helper_Database_StoredProcedure.cs
// Delete entire method (lines ~745-782)

// DELETE THIS ENTIRE METHOD:
/*
public static async Task<MySqlDataReader> ExecuteReaderAsync(
    string connectionString,
    string procedureName,
    Dictionary<string, object>? parameters = null,
    CommandType commandType = CommandType.StoredProcedure)
{
    var connection = new MySqlConnection(connectionString);
    // ... rest of implementation ...
}
*/
```

**Verification**: 
1. Build project - should succeed with 0 errors
2. If build fails with "ExecuteReaderAsync does not exist", you missed a caller
3. Search entire solution for "ExecuteReaderAsync" - should find 0 results

---

### Phase 2: Disable Connection Pooling

```csharp
// File: Helpers/Helper_Database_Variables.cs
// Find existing connection string construction

// OLD: No Pooling parameter
// NEW: Add Pooling=false

ConnectionString = $"Server={server};Database={database};Uid={user};Pwd={password};Pooling=false;Allow User Variables=True;...";
```

**Verification**: Run app, check SHOW PROCESSLIST - should show 0 sleeping connections when idle

---

### Phase 3: Create ExecuteRawSqlAsync

```csharp
// File: Helpers/Helper_Database_StoredProcedure.cs
// Add new method in #region Methods

/// <summary>
/// Executes raw SQL (DDL/DML) for database migrations.
/// ARCHITECTURAL EXCEPTION: Only for Service_Migration schema changes.
/// </summary>
/// <remarks>
/// This method provides controlled exception to "stored procedures only" rule.
/// Should ONLY be used by Service_Migration for schema changes that cannot be
/// done via stored procedures (ALTER TABLE, CREATE INDEX, etc.).
/// </remarks>
public static async Task<Model_Dao_Result<int>> ExecuteRawSqlAsync(
    string connectionString,
    string sql,
    Dictionary<string, object>? parameters = null)
{
    // Verify Pooling=false in connection string
    if (!connectionString.Contains("Pooling=false", StringComparison.OrdinalIgnoreCase))
    {
        return Model_Dao_Result<int>.Failure(
            "ExecuteRawSqlAsync requires Pooling=false in connection string");
    }

    try
    {
        using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        using var command = new MySqlCommand(sql, connection)
        {
            CommandType = CommandType.Text,
            CommandTimeout = Model_Application_Variables.CommandTimeoutSeconds
        };

        // Add parameters if provided
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue($"@{param.Key}", param.Value);
            }
        }

        int rowsAffected = await command.ExecuteNonQueryAsync();
        return Model_Dao_Result<int>.Success(rowsAffected);
    }
    catch (Exception ex)
    {
        LoggingUtility.LogDatabaseError(ex);
        string userMessage = IsConnectionRelatedError(ex)
            ? GetUserFriendlyConnectionError(ex, "ExecuteRawSqlAsync")
            : $"Database error during raw SQL execution: {ex.Message}";
        
        return Model_Dao_Result<int>.Failure(userMessage);
    }
}
```

---

### Phase 4: Create Analytics Stored Procedures

```sql
-- File: Database/UpdatedStoredProcedures/md_analytics_GetTransactionsByRange.sql

DELIMITER $$

DROP PROCEDURE IF EXISTS `md_analytics_GetTransactionsByRange`$$

CREATE PROCEDURE `md_analytics_GetTransactionsByRange`(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    -- Validation
    IF p_StartDate IS NULL OR p_EndDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Start date and end date are required';
        SELECT NULL LIMIT 0; -- Return empty result set
    ELSEIF p_StartDate >= p_EndDate THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Start date must be before end date';
        SELECT NULL LIMIT 0;
    ELSEIF DATEDIFF(p_EndDate, p_StartDate) > 365 THEN
        SET p_Status = -4;
        SET p_ErrorMsg = 'Date range cannot exceed 1 year';
        SELECT NULL LIMIT 0;
    ELSE
        -- Return transaction analytics
        SELECT 
            t.TransactionID,
            t.UserID,
            t.TransactionDate,
            t.PartID,
            t.Quantity,
            t.OperationType,
            l.LocationName as Location
        FROM app_transactions t
        LEFT JOIN app_locations l ON t.LocationID = l.LocationID
        WHERE t.TransactionDate BETWEEN p_StartDate AND p_EndDate
        ORDER BY t.TransactionDate DESC;

        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
    END IF;
END$$

DELIMITER ;
```

**Installation**: Run script in MAMP MySQL console or via command line:
```powershell
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms < Database/UpdatedStoredProcedures/md_analytics_GetTransactionsByRange.sql
```

---

### Phase 5: Refactor Service_Analytics

```csharp
// File: Services/Analytics/Service_Analytics.cs
// Replace direct MySqlConnection usages (3 locations)

// OLD CODE (to be replaced):
// await using (var connection = new MySqlConnection(Model_Application_Variables.ConnectionString))
// {
//     await connection.OpenAsync();
//     using var cmd = new MySqlCommand(inlineSQL, connection);
//     using (var reader = await cmd.ExecuteReaderAsync())
//     { ... }
// }

// NEW CODE:
var parameters = new Dictionary<string, object>
{
    { "StartDate", startDate },
    { "EndDate", endDate }
};

var result = await Helper_Database_StoredProcedure
    .ExecuteDataTableWithStatusAsync(
        "md_analytics_GetTransactionsByRange",
        parameters);

if (!result.IsSuccess)
{
    Service_ErrorHandler.ShowUserError(result.ErrorMessage);
    return;
}

// Process result.Data (DataTable)
```

---

### Phase 6: Create Connection Monitor

```csharp
// File: Helpers/Helper_Database_ConnectionMonitor.cs (NEW FILE)

using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Monitors MySQL connection lifecycle for leak detection.
/// </summary>
public static class Helper_Database_ConnectionMonitor
{
    #region Methods

    /// <summary>
    /// Gets current connection statistics from MySQL server.
    /// </summary>
    /// <returns>ConnectionStats with open connection count.</returns>
    public static async Task<ConnectionStats> GetConnectionStatsAsync()
    {
        try
        {
            var result = await Helper_Database_StoredProcedure
                .ExecuteDataTableWithStatusAsync(
                    "SHOW PROCESSLIST",
                    null,
                    CommandType.Text);

            if (!result.IsSuccess || result.Data == null)
            {
                return new ConnectionStats
                {
                    ServerAddress = "Unknown",
                    OpenConnections = -1,
                    Timestamp = DateTime.UtcNow,
                    IsHealthy = false,
                    WarningMessage = $"Failed to query connections: {result.ErrorMessage}"
                };
            }

            // Count connections from this application
            string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            int openConnections = result.Data.AsEnumerable()
                .Count(row => row.Field<string>("db") == Helper_Database_Variables.Database);

            bool isHealthy = openConnections == 0; // Should be 0 when idle

            return new ConnectionStats
            {
                ServerAddress = Helper_Database_Variables.Server,
                OpenConnections = openConnections,
                Timestamp = DateTime.UtcNow,
                IsHealthy = isHealthy,
                WarningMessage = isHealthy ? null : $"Potential connection leak: {openConnections} connections remain open"
            };
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"Connection monitoring error: {ex.Message}");
            return new ConnectionStats
            {
                ServerAddress = "Unknown",
                OpenConnections = -1,
                Timestamp = DateTime.UtcNow,
                IsHealthy = false,
                WarningMessage = $"Monitoring error: {ex.Message}"
            };
        }
    }

    #endregion
}
```

---

## Manual Testing Checklist

### Test 1: Verify ExecuteReaderAsync Completely Removed

```
1. Build project - should succeed with 0 errors
2. Search entire solution for "ExecuteReaderAsync" (Ctrl+Shift+F)
3. Expected: 0 results (method and all usages removed)
4. Verify Service_Analytics and Service_Migration compile and run correctly
```

### Test 2: Verify Connection Pooling Disabled

```
1. Start application
2. Open MySQL command line: SHOW PROCESSLIST;
3. Perform 10 database operations (add/remove inventory)
4. Wait 30 seconds (ensure operations complete)
5. Run SHOW PROCESSLIST; again
6. Expected: 0 sleeping connections from mtm_wip_application_winforms
```

### Test 3: Verify Connection Monitoring

```
1. Start application
2. Wait 5 minutes (monitoring interval)
3. Check logs: %APPDATA%\MTM\Logs\
4. Expected: Log entry showing ConnectionStats with OpenConnections = 0
5. Perform database operation
6. Wait 5 minutes
7. Expected: Log still shows OpenConnections = 0 (operation completed)
```

### Test 4: 8-Hour Stability Test

```
1. Start application in morning
2. Perform moderate usage (50+ transactions throughout day)
3. Leave application running 8+ hours
4. Monitor for "max users reached" errors
5. Expected: No connection errors, app runs continuously
6. End-of-day SHOW PROCESSLIST should show 0 connections
```

### Test 5: Service_Analytics Refactoring

```
1. Navigate to Analytics section in app
2. Generate transaction report for date range
3. Generate user activity report for date range
4. Verify reports load correctly
5. Check logs for any database errors
6. Expected: Reports function identically to before refactoring
```

---

## Troubleshooting

### Issue: "max users reached" still occurring

**Solution**:
1. Verify Pooling=false in connection string: Check Helper_Database_Variables.cs
2. Run SHOW PROCESSLIST during idle - should be 0
3. Check connection monitoring logs for leak patterns
4. Use Serena to find remaining ExecuteReaderAsync callers: `mcp_oraios_serena_search_for_pattern("ExecuteReaderAsync")`

### Issue: Performance degradation

**Solution**:
1. Measure operation times - should be <20ms overhead
2. If >50ms overhead, check network latency to MySQL server
3. Consider connection string optimizations (CommandTimeout, compression)
4. Non-pooled connections are expected to be slightly slower - this is acceptable tradeoff

### Issue: Stored procedures not found

**Solution**:
```powershell
# Verify stored procedures exist
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "SHOW PROCEDURE STATUS WHERE Db = 'mtm_wip_application_winforms' AND Name LIKE 'md_analytics%';"

# Re-run installation scripts if missing
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms < Database/UpdatedStoredProcedures/md_analytics_GetTransactionsByRange.sql
```

---

## Resources

- **Constitution**: `.specify/memory/constitution.md`
- **Feature Spec**: `specs/001-fix-mysql-connection-leaks/spec.md`
- **Research**: `specs/001-fix-mysql-connection-leaks/research.md`
- **Data Model**: `specs/001-fix-mysql-connection-leaks/data-model.md`
- **Serena Documentation**: `.github/instructions/serena-semantic-tools.instructions.md`
- **AGENTS.md**: Development environment setup and common workflows

---

## Next Steps

After completing implementation:

1. ✅ Replace all ExecuteReaderAsync callers (4 locations)
2. ✅ Remove ExecuteReaderAsync method entirely
3. ✅ Disable connection pooling (Pooling=false)
3. ✅ Create ExecuteRawSqlAsync
4. ✅ Create analytics stored procedures
5. ✅ Refactor Service_Analytics
6. ✅ Refactor Service_Migration
7. ✅ Create Helper_Database_ConnectionMonitor
8. ✅ Add monitoring to MainForm timer
9. ✅ Run manual tests (all 5 test scenarios)
10. ✅ Run constitution compliance validation
11. → Create pull request using `.github/PULL_REQUEST_TEMPLATE.md`
12. → Deploy to production after approval

**Estimated Development Time**: 8-12 hours
