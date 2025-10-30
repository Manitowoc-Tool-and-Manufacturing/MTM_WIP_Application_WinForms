# Quickstart Guide: Comprehensive Database Layer Standardization

**Last Updated**: 2025-10-17  
**Feature**: Comprehensive Database Layer Standardization  
**Branch**: `002-003-database-layer-complete`

## Purpose

Help developers adopt the standardized DAO, helper, and stored procedure patterns introduced by the combined phase 1-2 foundation and phase 2.5 refresh work. Follow these instructions when adding new data access features or refactoring existing ones.

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

- Visual Studio 2022 with .NET 8.0 SDK
- MySQL 5.7.24+ running locally (MAMP or equivalent)
- Test database `mtm_wip_application_winform_test`
- Access to supporting discovery artifacts in `Database/`

### Test Database Setup

1. **Create the schema**
   ```sql
   CREATE DATABASE mtm_wip_application_winform_test;
   ```

2. **Import baseline schema**
   ```powershell
   mysql -u root -proot mtm_wip_application_winform_test < Database/CurrentDatabase/MTM_WIP_Application_Winforms.sql
   ```

3. **Configure helper to target test database** (excerpt)
   ```csharp
   public static string DatabaseName =>
#if DEBUG
       "mtm_wip_application_winform_test";
#else
       "MTM_WIP_Application_Winforms";
#endif

   public const string TestDatabaseName = "mtm_wip_application_winform_test";
   ```

4. **Verify connection**
   ```csharp
   var connectionString = Helper_Database_Variables.GetConnectionString(
       databaseName: Helper_Database_Variables.TestDatabaseName);
   ```

> **Reminder**: The automation agent handles T106a/T106b validation using the curated artifact set. Developers can review outputs in `Database/AnalysisReports/` before proceeding with refactors.

---

## Creating a New DAO Method

### Query Operations (SELECT)

```csharp
/// <summary>
/// Retrieves inventory rows for the supplied location.
/// </summary>
public static async Task<DaoResult<DataTable>> GetInventoryByLocationAsync(
    string locationCode,
    bool includeInactive)
{
    try
    {
        var parameters = new Dictionary<string, object>
        {
            ["LocationCode"] = locationCode,
            ["IncludeInactive"] = includeInactive,
            ["User"] = Model_AppVariables.User
        };

        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatus(
            Model_AppVariables.ConnectionString,
            "inv_inventory_Get_ByLocation",
            parameters,
            progressHelper: null,
            useAsync: true);

        return result.IsSuccess
            ? DaoResult<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} rows")
            : DaoResult<DataTable>.Failure(result.Message);
    }
    catch (Exception ex)
    {
        LoggingUtility.LogDatabaseError(ex);
        return DaoResult<DataTable>.Failure($"Database error: {ex.Message}", ex);
    }
}
```

### Modification Operations (INSERT/UPDATE/DELETE)

```csharp
/// <summary>
/// Adds inventory for the provided part number.
/// </summary>
public static async Task<DaoResult> AddInventoryAsync(
    string partNumber,
    string locationCode,
    int quantity)
{
    try
    {
        var parameters = new Dictionary<string, object>
        {
            ["PartNumber"] = partNumber,
            ["LocationCode"] = locationCode,
            ["Quantity"] = quantity,
            ["User"] = Model_AppVariables.User
        };

        var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
            Model_AppVariables.ConnectionString,
            "inv_inventory_Add_Item",
            parameters,
            progressHelper: null,
            useAsync: true);

        return result.IsSuccess
            ? DaoResult.Success("Inventory added successfully")
            : DaoResult.Failure(result.Message);
    }
    catch (Exception ex)
    {
        LoggingUtility.LogDatabaseError(ex);
        return DaoResult.Failure($"Failed to add inventory: {ex.Message}", ex);
    }
}
```

### Multi-Step Transactions

```csharp
public static async Task<DaoResult> TransferInventoryAsync(
    string partNumber,
    string fromLocation,
    string toLocation,
    int quantity)
{
    await using var connection = new MySqlConnection(Model_AppVariables.ConnectionString);
    await connection.OpenAsync();

    await using var transaction = await connection.BeginTransactionAsync();
    try
    {
        var validation = await ValidateTransferAsync(connection, transaction, partNumber, fromLocation, quantity);
        if (!validation.IsSuccess)
        {
            await transaction.RollbackAsync();
            return DaoResult.Failure(validation.Message);
        }

        var deduct = await DeductInventoryAsync(connection, transaction, partNumber, fromLocation, quantity);
        if (!deduct.IsSuccess)
        {
            await transaction.RollbackAsync();
            return DaoResult.Failure(deduct.Message);
        }

        var add = await AddInventoryAsync(connection, transaction, partNumber, toLocation, quantity);
        if (!add.IsSuccess)
        {
            await transaction.RollbackAsync();
            return DaoResult.Failure(add.Message);
        }

        var log = await LogTransferAsync(connection, transaction, partNumber, fromLocation, toLocation, quantity);
        if (!log.IsSuccess)
        {
            await transaction.RollbackAsync();
            return DaoResult.Failure(log.Message);
        }

        await transaction.CommitAsync();
        return DaoResult.Success($"Transferred {quantity} of {partNumber}.");
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        LoggingUtility.LogDatabaseError(ex);
        return DaoResult.Failure($"Transfer failed: {ex.Message}", ex);
    }
}
```

---

## Testing Your DAO

### Integration Test Skeleton

```csharp
[TestClass]
public class DaoInventoryTests : BaseIntegrationTest
{
    [TestMethod]
    public async Task AddInventoryAsync_ValidParameters_Succeeds()
    {
        // Arrange
        var partNumber = "QT-123";
        var locationCode = "FLOOR";
        const int quantity = 5;

        // Act
        var result = await Dao_Inventory.AddInventoryAsync(partNumber, locationCode, quantity);

        // Assert
        AssertProcedureResult(result, expectedSuccess: true);

        var fetch = await Dao_Inventory.GetInventoryByPartIdAsync(partNumber);
        AssertProcedureResult(fetch, expectedSuccess: true);
        Assert.AreEqual(1, fetch.Data.Rows.Count);
    }

    [TestMethod]
    public async Task AddInventoryAsync_DuplicatePart_Fails()
    {
        var partNumber = "DUP-001";
        const int quantity = 1;
        await Dao_Inventory.AddInventoryAsync(partNumber, "FLOOR", quantity);

        var duplicate = await Dao_Inventory.AddInventoryAsync(partNumber, "FLOOR", quantity);

        AssertProcedureResult(duplicate, expectedSuccess: false, expectedMessagePattern: "exists");
    }
}
```

> **Verbose Diagnostics**: BaseIntegrationTest captures exception details, parameters, expected/actual outcomes, execution time, table row counts, test method name, and timestamp. Failures emit JSON to simplify triage.

---

## Common Patterns

### Async Event Handlers

```csharp
private async void btnSave_Click(object sender, EventArgs e)
{
    try
    {
        btnSave.Enabled = false;
        Cursor = Cursors.WaitCursor;

        var result = await Dao_Inventory.AddInventoryAsync(
            txtPart.Text,
            cboLocation.Text,
            int.Parse(txtQuantity.Text));

        if (result.IsSuccess)
        {
            MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadInventoryAsync();
        }
        else
        {
            Service_ErrorHandler.HandleValidationError(result.Message, nameof(txtPart));
        }
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, ErrorSeverity.Error, controlName: nameof(btnSave));
    }
    finally
    {
        btnSave.Enabled = true;
        Cursor = Cursors.Default;
    }
}
```

### UserControl Async Initialization

```csharp
public partial class InventoryControl : UserControl
{
    public InventoryControl()
    {
        InitializeComponent();
        Load += async (_, _) => await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        var result = await Dao_Inventory.GetInventorySummaryAsync();
        if (result.IsSuccess)
        {
            dgvInventory.DataSource = result.Data;
        }
        else
        {
            Service_ErrorHandler.HandleException(
                new InvalidOperationException(result.Message),
                ErrorSeverity.Warning,
                controlName: nameof(InventoryControl));
        }
    }
}
```

### Background Timer

```csharp
private async void Timer_Tick(object? sender, EventArgs e)
{
    var result = await Dao_System.CheckVersionAsync();
    if (!result.IsSuccess)
    {
        Service_ErrorHandler.HandleException(
            new InvalidOperationException(result.Message),
            ErrorSeverity.Warning,
            controlName: nameof(Timer_Tick));
    }
}
```

### Progress Reporting

```csharp
public async Task ExportInventoryAsync()
{
    using var progress = Helper_StoredProcedureProgress.Create(progressBar, lblStatus, FindForm());

    var result = await Dao_Inventory.ExportInventoryAsync(progress);
    AssertProcedureResult(result, expectedSuccess: true);
}
```

---

## Troubleshooting

| Issue | Likely Cause | Resolution |
|-------|--------------|------------|
| "Parameter prefix not found" | Parameter absent from cache and fallback ambiguous | Confirm stored procedure signature, update overrides via Developer tools if needed |
| UI freeze | `.Result` / `.Wait()` used on async method | Convert entire call chain to async/await |
| Transaction rolled back | Step failure not short-circuiting | Validate each step and return immediately on failure |
| Connection pool exhausted | Undisposed MySqlConnection | Use `await using` or `using` statements for all connections/commands |
| Slow query warning | Execution time exceeded threshold | Analyze stored procedure (EXPLAIN), review indexes, adjust strategy |
| `DaoResult.Data` null access | Caller skipped `IsSuccess` check | Guard access with `if (result.IsSuccess)` |

---

## Next Steps

- Consult [data-model.md](./data-model.md) for entity details.
- Review `/contracts/` for DaoResult and stored procedure contracts.
- Track documentation obligations via the Documentation Update Matrix once generated.
- Reference the plan and tasks files for phase sequencing and automation gates.
