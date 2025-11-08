---
description: 'MySQL 5.7 database patterns and manufacturing domain context for MTM'
applyTo: 'Data/**/*.cs,Helpers/**/*.cs,Services/**/*.cs'
---

<!-- Based on patterns from: https://github.com/github/awesome-copilot -->

# MySQL Database Patterns for MTM Manufacturing

## Overview

This file defines MySQL 5.7 database patterns, connection management, and manufacturing domain context for the MTM WIP Application. The application uses MySQL 5.7 via MAMP with the MySql.Data connector and custom helper classes for stored procedure execution.

## Relevant MCP Tools

- `analyze_stored_procedures` – Audit procedure files in `Database/UpdatedStoredProcedures/` for required `p_Status`/`p_ErrorMsg` outputs, transaction handling, and `p_` parameter naming whenever you modify SQL.
- `analyze_dependencies` – Map CALL hierarchies before refactoring to understand impact and to confirm you are updating every dependent procedure mentioned here.
- `compare_databases` – Run against the `Current*/Updated*` folders to spot schema drift that needs to be reconciled before promoting changes.
- `install_stored_procedures` – Apply updated SQL to the test database using the JSON config as part of deployment dry runs.
- `validate_schema` – Verify the live database matches `database-schema-snapshot.json` after migrations or before running integration suites.
- `generate_dao_wrapper` – Produce DAO skeletons that follow the helper usage patterns documented below when introducing new stored procedures.
- `generate_test_seed_sql` & `verify_test_seed` – Seed deterministic datasets and confirm results for integration tests exercising new procedures.
- `audit_database_cleanup` – Check for and optionally remove `TEST-*` residues after suites that touch manufacturing tables.
- `run_integration_harness` – Chain seeding, procedure installs, schema validation, DAO tests, and cleanup in a single scripted run that mirrors the workflows described in this guide.

## Core Principles

### Stored Procedure First
- All database operations use stored procedures
- No inline SQL in application code (except simple queries if necessary)
- 45+ stored procedures handle all CRUD and business logic operations
- Stored procedures encapsulate manufacturing business rules

### Connection Pooling
- Always use connection pooling for performance
- Configuration: MinPoolSize=5, MaxPoolSize=100
- Timeout: 30 seconds (align with `Model_Application_Variables.CommandTimeoutSeconds` settings)
- MaxRetryAttempts: 3 with exponential backoff

### Async/Await for All Operations
- All database operations must be asynchronous
- Use `async Task<T>` for methods that return data
- Use `async Task` for methods that don't return data
- Never block with `.Result` or `.Wait()`

## Database Connection Configuration

### MAMP MySQL 5.7 Credentials
- Server: `localhost`
- Port: `3306` (default)
- Database: `MTM_WIP_Application_Winforms` (production)
- Database: `mtm_wip_application_winforms_test` (development)
- Username: `root`
- Password: `root`
- Connection String: `Server=localhost;Database=MTM_WIP_Application_Winforms;SslMode=none;AllowPublicKeyRetrieval=true;`

### Connection String Pattern
```
SERVER=<host>;DATABASE=<db>;UID=<user>;PASSWORD=<password>;Allow User Variables=True;SslMode=none;AllowPublicKeyRetrieval=true;MinPoolSize=5;MaxPoolSize=100;ConnectionTimeout=30;
```

### Configuration Source
- Use `Helper_Database_Variables.GetConnectionString` to build connection strings based on `Model_Shared_Users` and `Model_Application_Variables`.
- Keep credentials outside of source control. When local secrets are required, store them in environment-specific config files that are ignored by Git.
- Validate connection strings on startup using the helper methods in `Program.cs` to surface user-friendly errors.

## Stored Procedure Execution Patterns

### Discovering Missing Stored Procedures

When refactoring or auditing the codebase for missing stored procedures:

1. **Use regex scanning** to find all stored procedure references in C# code:
   ```python
   pattern = re.compile(r'"((?:inv|md|sys|log|usr|maint|query)_[A-Za-z0-9_]+)"')
   ```
2. **Cross-reference** found procedure names against SQL definitions in `UpdatedStoredProcedures/ReadyForVerification/`
3. **Extract parameters** from code by parsing `ExecuteDataTableWithStatusAsync` calls and Dictionary initializers
4. **Create action plan CSV** with columns: Procedure, Domain, CallFile, CallLine, Action (CREATE_NEW/RENAME_CALL/IGNORE), Parameters, ExpectedReturn, Notes, SimilarProcedure
5. **Prioritize quick wins**: Rename code references to existing procedures before creating new ones
6. **Use existing procedures as templates** when creating new stored procedures (check SimilarProcedure column)

This systematic approach discovered 19 missing procedures in one session, preventing runtime errors and enabling comprehensive stored procedure inventory management.

### Helper_Database_StoredProcedure.ExecuteDataTableWithStatus
This helper wraps stored procedure calls and returns a `StoredProcedureResult<DataTable>` along with the standard `p_Status` and `p_ErrorMsg` outputs.

```
var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatus(
    connectionString,
    "usp_GetInventoryTransactions",
    new Dictionary<string, object>
    {
        ["UserID"] = userId,
        ["StartDate"] = startDate,
        ["EndDate"] = endDate
    },
    progressHelper: null,
    useAsync: true);

if (result.IsSuccess)
{
    foreach (DataRow row in result.Payload.Rows)
    {
        // Map DataRow to model
    }
}
else
{
    LoggingUtility.LogApplicationError(result.Exception, result.StatusMessage);
    // Surface result.StatusMessage to the UI layer
}
```

### Return Value Handling
- **Payload**: DataTable containing query results (may be empty).
- **StatusMessage**: Human-readable status or error text surfaced to the UI.
- **Exception**: Original exception when the call fails; always null on success.

### Parameter Naming Conventions
- Pass parameters without the `p_` prefix; the helper will add it automatically.
- Use PascalCase names to match the stored procedure signature (e.g., `InventoryId`, `QuantityDelta`).
- Provide `DBNull.Value` for optional parameters instead of omitting keys so the stored procedure receives explicit nulls.

## Manufacturing Domain Context

### Work Order Operations (Sequence Steps)
Operations represent steps in a work order's manufacturing routing sequence, NOT transaction types:

- **10, 20, 30**: Early routing steps in manufacturing sequence
- **90, 100, 110**: Standard manufacturing sequence steps (ValidOperations from appsettings.json)
- **120, 130**: Additional sequence steps (extended in code beyond config)
- **NOT transaction types**: Operations indicate where a part is in its manufacturing workflow

### ValidOperations Configuration
From appsettings.json `MTM.ValidOperations`:
```json
"ValidOperations": [ "90", "100", "110" ]
```

These are the commonly validated operation numbers, but other operations (10, 20, 30, 120, 130) may be used depending on manufacturing routing requirements.

### Transaction Types (Separate from Operations)
Transaction types represent inventory movement intent:

- **IN**: Receiving inventory into the system (incoming shipments, returned items)
- **OUT**: Removing inventory from the system (shipments, scrap, consumption)
- **TRANSFER**: Moving inventory between locations or operations (internal movements)

**Determination**: Transaction type is determined by user intent and workflow context, not by the operation number.

### Location Codes
From appsettings.json `MTM.DefaultLocations`:
```json
"DefaultLocations": [ "FLOOR", "RECEIVING", "SHIPPING" ]
```

- **FLOOR**: Shop floor inventory (active manufacturing)
- **RECEIVING**: Incoming shipments and receiving area
- **SHIPPING**: Outbound shipments and staging area
- **Custom locations**: Additional locations can be defined in the database

### Session Management
From appsettings.json `MTM` configuration:
```json
"SessionTimeoutMinutes": 60,
"MaxQuickButtons": 10,
"AutoSaveUserPreferences": true,
"AutoSaveIntervalMinutes": 5
```

- Sessions expire after 60 minutes of inactivity
- Maximum 10 quick buttons per user for rapid transaction shortcuts
- User preferences auto-save every 5 minutes

## Data Mapping Patterns

### Mapping DataTable Results
- Convert each `DataRow` into the appropriate model in a dedicated mapper or within the DAO immediately after execution.
- Use extension methods or helper routines in `Helpers/` when multiple DAOs share the same mapping logic.
- Normalize database values (trim strings, convert `DBNull.Value` to defaults) before returning models to the UI layer.

### Handling Output Parameters
- Inspect `result.OutputParameters` from the helper when stored procedures expose additional values (e.g., identity keys or status flags).
- Document expected output parameters in the DAO XML comments to keep the stored procedure contract visible in code.

### Streaming Large Results
- Prefer paging stored procedures or filter parameters to keep result sets manageable for WinForms DataGridView controls.
- If a large export is required, stream the data to CSV inside the DAO or a dedicated service rather than loading everything into the UI at once.

## Error Handling and Retry Logic

### Transient Error Retry
```
private async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> operation)
{
    int attempt = 0;
    while (true)
    {
        try
        {
            return await operation();
        }
        catch (MySqlException ex) when (IsTransientError(ex) && attempt < _maxRetryAttempts)
        {
            attempt++;
            LoggingUtility.Log($"[Database] Transient failure, retrying {attempt}/{_maxRetryAttempts}: {ex.Message}");
            await Task.Delay(TimeSpan.FromSeconds(_retryDelaySeconds * attempt));
        }
    }
}

private bool IsTransientError(MySqlException ex)
{
    // Transient error codes: connection timeout, deadlock, etc.
    return ex.Number == 1205 || // Deadlock
           ex.Number == 1213 || // Lock wait timeout
           ex.Number == 2006 || // Server has gone away
           ex.Number == 2013;   // Lost connection during query
}
```

> Define `_maxRetryAttempts` and `_retryDelaySeconds` at the service level or read them from `Model_Application_Variables` so retry behavior stays consistent across DAOs.

### Connection Validation
```
private async Task<bool> ValidateConnectionAsync()
{
    try
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return connection.State == ConnectionState.Open;
        }
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        return false;
    }
}
```

## MySQL 5.7 Specific Considerations

### Known Limitations
- **No CTEs (Common Table Expressions)**: Use subqueries or temporary tables instead
- **No window functions**: Use variables and subqueries for ranking/aggregation
- **Limited JSON support**: JSON functions available but limited compared to MySQL 8.0

### Date and Time Handling
- Use `DateTime` in C# code
- MySQL 5.7 stores as `DATETIME` or `TIMESTAMP`
- Always specify time zone handling explicitly
- Use UTC for storage, convert to local time in application layer

### String Encoding
- Default charset: utf8mb4 for full Unicode support
- Collation: utf8mb4_general_ci (case-insensitive) or utf8mb4_bin (case-sensitive)
- Handle emoji and special characters correctly

## Performance Optimization

### Connection Pooling Best Practices
- Dispose connections properly (use `using` statements)
- Don't hold connections open longer than necessary
- Let connection pool manage connection lifecycle
- Monitor connection pool metrics in production

### Query Optimization
- Index columns used in WHERE clauses and JOINs
- Use EXPLAIN to analyze query execution plans
- Avoid SELECT * - specify columns explicitly
- Limit result sets with LIMIT clauses when appropriate

### Stored Procedure Performance
- Keep stored procedures focused and single-purpose
- Avoid cursors - use set-based operations
- Cache execution plans by using parameterized procedures
- Monitor slow query log for optimization opportunities

## Transaction Management

### Transaction Pattern
```
using (var connection = new MySqlConnection(_connectionString))
{
    await connection.OpenAsync();
    using (var transaction = connection.BeginTransaction())
    {
        try
        {
            // Execute operations
            await connection.ExecuteAsync("usp_Operation1", param1, transaction: transaction);
            await connection.ExecuteAsync("usp_Operation2", param2, transaction: transaction);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed, rolling back");
            transaction.Rollback();
            throw;
        }
    }
}
```

### Transaction Isolation Levels
- Default: Read Committed
- Use explicit isolation levels when needed for consistency
- Be aware of locking implications

## Logging Database Operations

### Log Structured Data
```
LoggingUtility.Log($"[Database] Executing stored procedure {storedProcedureName} with parameters {JsonSerializer.Serialize(parameters)}");
```

### Log Performance Metrics
```
var stopwatch = Stopwatch.StartNew();
var result = await ExecuteStoredProcedureAsync(procName, parameters);
stopwatch.Stop();

if (stopwatch.ElapsedMilliseconds > 1000)
{
        LoggingUtility.Log($"[Database] Slow query detected: {procName} took {stopwatch.ElapsedMilliseconds}ms");
}
```

### Never Log Sensitive Data
- Don't log passwords, connection strings, or sensitive business data
- Sanitize parameters before logging
- Use log level appropriately (Debug for detailed parameters)

## Security Best Practices

### SQL Injection Prevention
- Always use stored procedures or parameterized queries exposed through helpers
- Never concatenate user input into SQL strings
- Validate and sanitize input at the application layer before database calls

### Connection String Security
- Store connection strings in appsettings.json
- Never hardcode credentials in code
- Use secure configuration for production (Azure Key Vault, environment variables)
- Rotate passwords regularly

### Least Privilege Access
- Database user should have only necessary permissions
- Use separate credentials for different environments (dev, test, prod)
- Audit database access and operations

## Testing Database Operations

### Manual Validation Approach
- Test stored procedure calls with valid and invalid parameters
- Verify error handling for database failures
- Test transaction rollback scenarios
- Validate connection pooling behavior under load

### Integration Testing
- Use test database (`mtm_wip_application_winforms_test`) for integration tests
- Clean up test data after test execution
- Test with realistic data volumes
- Verify stored procedure results match expectations
