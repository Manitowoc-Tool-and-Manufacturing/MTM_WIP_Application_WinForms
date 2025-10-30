# Quickstart Guide: Comprehensive Database Layer

**Last Updated**: 2025-10-13  
**Feature**: Comprehensive Database Layer Refactor  
**Branch**: `002-comprehensive-database-layer`

## Purpose

This guide helps developers quickly understand and use the refactored database access layer. Follow these patterns for consistent, safe, and performant database operations.

---

## Table of Contents

1. [Getting Started](#getting-started)
2. [Creating a New DAO Method](#creating-a-new-dao-method)
3. [Testing Your DAO](#testing-your-dao)
4. [Common Patterns](#common-patterns)
5. [Troubleshooting](#troubleshooting)

---

## Getting Started

### Prerequisites

-   Visual Studio 2022 with .NET 8.0 SDK installed
-   MySQL 5.7.24+ running locally (MAMP recommended)
-   Test database `mtm_wip_application_winform_test` created and populated

### Test Database Setup

**1. Create test database**:

```sql
CREATE DATABASE mtm_wip_application_winform_test;
```

**2. Import schema** from `Database/CurrentDatabase/MTM_WIP_Application_Winforms.sql`:

```powershell
mysql -u root -proot mtm_wip_application_winform_test < Database/CurrentDatabase/MTM_WIP_Application_Winforms.sql
```

**3. Verify connection** in `Helper_Database_Variables.cs`:

```csharp
// Environment-aware database selection
public static string DatabaseName =>
    Debugger.IsAttached
        ? "mtm_wip_application_winforms_test" // Development
        : "MTM_WIP_Application_Winforms";     // Production

// Test database for integration tests (manual selection)
public const string TestDatabaseName = "mtm_wip_application_winform_test";
```

**4. Update connection string** for tests:

```csharp
var connectionString = Helper_Database_Variables.GetConnectionString(
    databaseName: Helper_Database_Variables.TestDatabaseName);
```

---

## Creating a New DAO Method

### Template for Query Operations (SELECT)

**File**: `Data/Dao_[Domain].cs`

```csharp
/// <summary>
/// Retrieves [description of what is retrieved].
/// </summary>
/// <param name="[param1]">[Description of param1].</param>
/// <param name="[param2]">[Description of param2].</param>
/// <returns>DaoResult&lt;DataTable&gt; containing query results or error.</returns>
public static async Task<DaoResult<DataTable>> Get[EntityName]Async(
    string param1,
    int param2)
{
    try
    {
        var parameters = new Dictionary<string, object>
        {
            ["Param1"] = param1,  // NO p_ prefix in C# code
            ["Param2"] = param2,
            ["User"] = Model_AppVariables.User,
            ["DateTime"] = DateTime.Now
        };

        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatus(
            Model_AppVariables.ConnectionString,
            "[stored_procedure_name]",  // e.g., "inv_inventory_get_all"
            parameters,
            progressHelper: null,
            useAsync: true);

        if (result.IsSuccess)
        {
            return DaoResult<DataTable>.Success(
                result.Data,
                $"Retrieved {result.Data.Rows.Count} [entity name](s)");
        }
        else
        {
            return DaoResult<DataTable>.Failure(result.Message);
        }
    }
    catch (Exception ex)
    {
        LoggingUtility.LogDatabaseError(ex);
        return DaoResult<DataTable>.Failure(
            $"Failed to retrieve [entity name]: {ex.Message}",
            ex);
    }
}
```

### Template for Modification Operations (INSERT/UPDATE/DELETE)

```csharp
/// <summary>
/// [Adds/Updates/Deletes] [description].
/// </summary>
/// <param name="[param1]">[Description].</param>
/// <returns>DaoResult indicating success or failure.</returns>
public static async Task<DaoResult> [Add/Update/Delete][EntityName]Async(
    string param1,
    int param2)
{
    try
    {
        var parameters = new Dictionary<string, object>
        {
            ["Param1"] = param1,
            ["Param2"] = param2,
            ["User"] = Model_AppVariables.User,
            ["DateTime"] = DateTime.Now
        };

        var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
            Model_AppVariables.ConnectionString,
            "[stored_procedure_name]",
            parameters,
            progressHelper: null,
            useAsync: true);

        if (result.IsSuccess)
        {
            return DaoResult.Success($"[Entity name] [added/updated/deleted] successfully");
        }
        else
        {
            return DaoResult.Failure(result.Message);
        }
    }
    catch (Exception ex)
    {
        LoggingUtility.LogDatabaseError(ex);
        return DaoResult.Failure(
            $"Failed to [add/update/delete] [entity name]: {ex.Message}",
            ex);
    }
}
```

### Template for Multi-Step Transactions

```csharp
/// <summary>
/// Performs multi-step operation with transaction.
/// </summary>
public static async Task<DaoResult> PerformMultiStepOperationAsync(
    string param1,
    int param2)
{
    using var connection = new MySqlConnection(Model_AppVariables.ConnectionString);
    await connection.OpenAsync();

    using var transaction = await connection.BeginTransactionAsync();

    try
    {
        // Step 1: First operation
        var step1Parameters = new Dictionary<string, object>
        {
            ["Param1"] = param1,
            ["User"] = Model_AppVariables.User
        };

        var result1 = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
            Model_AppVariables.ConnectionString,
            "stored_procedure_step1",
            step1Parameters,
            progressHelper: null,
            useAsync: true);

        if (!result1.IsSuccess)
        {
            await transaction.RollbackAsync();
            return DaoResult.Failure($"Step 1 failed: {result1.Message}");
        }

        // Step 2: Second operation
        var step2Parameters = new Dictionary<string, object>
        {
            ["Param2"] = param2,
            ["User"] = Model_AppVariables.User
        };

        var result2 = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
            Model_AppVariables.ConnectionString,
            "stored_procedure_step2",
            step2Parameters,
            progressHelper: null,
            useAsync: true);

        if (!result2.IsSuccess)
        {
            await transaction.RollbackAsync();
            return DaoResult.Failure($"Step 2 failed: {result2.Message}");
        }

        // All steps succeeded - commit
        await transaction.CommitAsync();
        return DaoResult.Success("Multi-step operation completed successfully");
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        LoggingUtility.LogDatabaseError(ex);
        return DaoResult.Failure($"Transaction failed: {ex.Message}", ex);
    }
}
```

---

## Testing Your DAO

### Integration Test Template

**File**: `Tests/Integration/Dao_[Domain]Tests.cs`

```csharp
[TestClass]
public class Dao_InventoryTests
{
    private MySqlTransaction? _transaction;
    private MySqlConnection? _connection;

    [TestInitialize]
    public async Task Setup()
    {
        // Use test database connection
        var connectionString = Helper_Database_Variables.GetConnectionString(
            databaseName: Helper_Database_Variables.TestDatabaseName);

        _connection = new MySqlConnection(connectionString);
        await _connection.OpenAsync();

        // Begin transaction for test isolation
        _transaction = await _connection.BeginTransactionAsync();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        // Rollback transaction to leave database clean
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        if (_connection != null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
    }

    [TestMethod]
    public async Task GetInventoryAsync_ValidLocation_ReturnsData()
    {
        // Arrange
        var locationCode = "FLOOR";
        var includeInactive = false;

        // Act
        var result = await Dao_Inventory.GetInventoryAsync(locationCode, includeInactive);

        // Assert
        Assert.IsTrue(result.IsSuccess, "Operation should succeed");
        Assert.IsNotNull(result.Data, "Data should not be null");
        Assert.IsTrue(result.Data.Rows.Count > 0, "Should return inventory rows");
    }

    [TestMethod]
    public async Task AddInventoryAsync_ValidData_Succeeds()
    {
        // Arrange
        var partNumber = "TEST123";
        var locationCode = "FLOOR";
        var quantity = 10;

        // Act
        var result = await Dao_Inventory.AddInventoryAsync(partNumber, locationCode, quantity);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Operation should succeed: {result.Message}");
        Assert.IsTrue(result.Message.Contains("success", StringComparison.OrdinalIgnoreCase),
            "Message should indicate success");

        // Verify data was added
        var verifyResult = await Dao_Inventory.GetInventoryByPartAsync(partNumber);
        Assert.IsTrue(verifyResult.IsSuccess);
        Assert.IsTrue(verifyResult.Data.Rows.Count > 0, "Part should exist after add");
    }

    [TestMethod]
    public async Task AddInventoryAsync_DuplicatePart_ReturnsFail()
    {
        // Arrange
        var partNumber = "EXISTING_PART";
        var locationCode = "FLOOR";
        var quantity = 10;

        // Add first time
        await Dao_Inventory.AddInventoryAsync(partNumber, locationCode, quantity);

        // Act - try to add duplicate
        var result = await Dao_Inventory.AddInventoryAsync(partNumber, locationCode, quantity);

        // Assert
        Assert.IsFalse(result.IsSuccess, "Duplicate add should fail");
        Assert.IsTrue(result.Message.Contains("exist", StringComparison.OrdinalIgnoreCase),
            "Message should indicate duplicate");
    }
}
```

---

## Common Patterns

### Pattern 1: Async Event Handlers in WinForms

**Problem**: WinForms event handlers are synchronous by default.

**Solution**: Use `async void` for event handlers and proper error handling.

```csharp
private async void btnSave_Click(object sender, EventArgs e)
{
    try
    {
        // Disable UI during operation
        btnSave.Enabled = false;
        Cursor = Cursors.WaitCursor;

        // Perform async DAO operation
        var result = await Dao_Inventory.AddInventoryAsync(
            txtPartNumber.Text,
            cboLocation.Text,
            int.Parse(txtQuantity.Text));

        // Update UI based on result
        if (result.IsSuccess)
        {
            MessageBox.Show(result.Message, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh data
            await LoadInventoryAsync();
        }
        else
        {
            MessageBox.Show(result.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        MessageBox.Show($"An error occurred: {ex.Message}", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        // Re-enable UI
        btnSave.Enabled = true;
        Cursor = Cursors.Default;
    }
}
```

### Pattern 2: UserControl Initialization with Async Data Loading

**Problem**: UserControl constructor can't be async.

**Solution**: Use `Load` event for async initialization.

```csharp
public partial class InventoryControl : UserControl
{
    public InventoryControl()
    {
        InitializeComponent();

        // Subscribe to Load event for async initialization
        Load += InventoryControl_Load;
    }

    private async void InventoryControl_Load(object? sender, EventArgs e)
    {
        await LoadInitialDataAsync();
    }

    private async Task LoadInitialDataAsync()
    {
        try
        {
            var result = await Dao_Inventory.GetAllInventoryAsync();

            if (result.IsSuccess)
            {
                dgvInventory.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show(result.Message, "Load Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }
}
```

### Pattern 3: Service Background Operations

**Problem**: Services need periodic database operations.

**Solution**: Use `System.Threading.Timer` with async operations.

```csharp
public class InventoryMonitoringService
{
    private Timer? _timer;

    public void Start()
    {
        _timer = new Timer(
            callback: async _ => await CheckInventoryLevelsAsync(),
            state: null,
            dueTime: TimeSpan.Zero,
            period: TimeSpan.FromMinutes(5));
    }

    private async Task CheckInventoryLevelsAsync()
    {
        try
        {
            var result = await Dao_Inventory.GetLowStockItemsAsync();

            if (result.IsSuccess && result.Data.Rows.Count > 0)
            {
                // Send alerts for low stock items
                await SendLowStockAlertsAsync(result.Data);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    public void Stop()
    {
        _timer?.Dispose();
        _timer = null;
    }
}
```

### Pattern 4: Progress Reporting for Long Operations

**Problem**: Long database operations need progress feedback.

**Solution**: Use `Helper_StoredProcedureProgress` for progress reporting.

```csharp
private async void btnExport_Click(object sender, EventArgs e)
{
    var progressHelper = new Helper_StoredProcedureProgress();

    // Subscribe to progress events
    progressHelper.ProgressChanged += (sender, progress) =>
    {
        Invoke(() => progressBar.Value = progress);
    };

    progressHelper.StatusChanged += (sender, status) =>
    {
        Invoke(() => lblStatus.Text = status);
    };

    try
    {
        var result = await Dao_Inventory.ExportInventoryAsync(progressHelper);

        if (result.IsSuccess)
        {
            MessageBox.Show("Export completed successfully", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show(result.Message, "Export Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
    }
}
```

---

## Troubleshooting

### Issue 1: "Parameter prefix not found" Error

**Symptom**: Error message indicates parameter prefix (p*, in*, o\_) could not be detected.

**Cause**: Parameter not in INFORMATION_SCHEMA cache and fallback convention failed.

**Solution**:

1. Verify stored procedure exists in database
2. Check parameter name spelling in C# code matches stored procedure
3. Run INFORMATION_SCHEMA query manually to verify parameter name:
    ```sql
    SELECT ROUTINE_NAME, PARAMETER_NAME, PARAMETER_MODE
    FROM INFORMATION_SCHEMA.PARAMETERS
    WHERE ROUTINE_SCHEMA = DATABASE()
      AND ROUTINE_NAME = '[your_procedure_name]';
    ```
4. If parameter has non-standard prefix, update fallback convention in `Helper_Database_StoredProcedure.GetParameterPrefix()`

### Issue 2: "Async method is blocking UI thread" Error

**Symptom**: UI freezes during database operations.

**Cause**: Using `.Result` or `.Wait()` instead of `await`.

**Solution**:

```csharp
// ❌ BAD: Blocks UI thread
var result = Dao_Inventory.GetInventoryAsync().Result;

// ✅ GOOD: Async all the way
var result = await Dao_Inventory.GetInventoryAsync();
```

### Issue 3: "Transaction rolled back unexpectedly" Error

**Symptom**: Multi-step operations fail with rollback message.

**Cause**: One step failed but error not properly checked.

**Solution**:

```csharp
// Always check IsSuccess after each step
var step1Result = await ExecuteStep1Async();
if (!step1Result.IsSuccess)
{
    await transaction.RollbackAsync();
    return DaoResult.Failure($"Step 1 failed: {step1Result.Message}");
}

// Continue to step 2 only if step 1 succeeded
var step2Result = await ExecuteStep2Async();
if (!step2Result.IsSuccess)
{
    await transaction.RollbackAsync();
    return DaoResult.Failure($"Step 2 failed: {step2Result.Message}");
}
```

### Issue 4: "Connection pool exhausted" Error

**Symptom**: TimeoutException with message about connection pool.

**Cause**: Connections not properly disposed.

**Solution**:

```csharp
// ❌ BAD: Connection not disposed
var connection = new MySqlConnection(connectionString);
connection.Open();
// ... use connection
// Missing connection.Dispose()!

// ✅ GOOD: using statement ensures disposal
using (var connection = new MySqlConnection(connectionString))
{
    await connection.OpenAsync();
    // ... use connection
} // Automatically disposed here
```

### Issue 5: "Slow query threshold exceeded" Warning

**Symptom**: Warning log entry indicates query exceeded performance threshold.

**Cause**: Query took longer than configured threshold (500ms for Query, 1000ms for Modification, etc.).

**Solution**:

1. Review stored procedure execution plan:
    ```sql
    EXPLAIN [your_stored_procedure_call];
    ```
2. Add indexes for columns in WHERE/JOIN clauses
3. Limit result set with appropriate filters
4. Consider paginating large result sets
5. If legitimately slow (e.g., complex report), increase threshold for that specific operation category

### Issue 6: "DaoResult.Data is null" Error

**Symptom**: NullReferenceException when accessing `result.Data`.

**Cause**: Not checking `IsSuccess` before accessing `Data`.

**Solution**:

```csharp
// ❌ BAD: Data might be null
var result = await Dao_Inventory.GetInventoryAsync();
var rowCount = result.Data.Rows.Count; // NullReferenceException if failed!

// ✅ GOOD: Always check IsSuccess first
var result = await Dao_Inventory.GetInventoryAsync();
if (result.IsSuccess)
{
    var rowCount = result.Data.Rows.Count; // Safe
}
else
{
    MessageBox.Show(result.Message, "Error");
}
```

---

## Next Steps

-   Review [data-model.md](./data-model.md) for entity structure and relationships
-   Review [contracts/](./contracts/) for detailed API schemas
-   Review [plan.md](./plan.md) for implementation phases and architecture
-   Consult [.github/instructions/mysql-database.instructions.md](../../.github/instructions/mysql-database.instructions.md) for MySQL best practices
