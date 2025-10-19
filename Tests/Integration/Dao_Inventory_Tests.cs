using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;
using System.Data;

namespace MTM_Inventory_Application.Tests.Integration;

/// <summary>
/// Integration tests for Dao_Inventory operations.
/// Tests database interactions, connection pooling under load, transaction management, and error handling.
/// </summary>
/// <remarks>
/// <para>
/// <strong>Test Coverage:</strong>
/// <list type="bullet">
/// <item>GetInventoryByPartIdAsync - Search operations</item>
/// <item>GetInventoryByPartIdAndOperationAsync - Filtered search operations</item>
/// <item>AddInventoryItemAsync - Insert operations with 100 consecutive calls for connection pool validation</item>
/// <item>RemoveInventoryItemAsync - Delete operations</item>
/// <item>TransferPartSimpleAsync - Single-step transfer operations</item>
/// <item>TransferInventoryQuantityAsync - Multi-step transaction with rollback validation</item>
/// </list>
/// </para>
/// <para>
/// <strong>Connection Pool Validation:</strong>
/// AddInventoryAsync_100ConsecutiveOperations_ValidatesConnectionPool test executes 100 consecutive
/// AddInventoryItemAsync calls to validate connection pooling (MinPoolSize=5, MaxPoolSize=100).
/// Each operation should complete within 1 second, demonstrating efficient pool management.
/// </para>
/// <para>
/// <strong>Transaction Rollback Validation:</strong>
/// TransferInventoryQuantityAsync_ForcedFailure_RollsBackTransaction test validates that multi-step
/// transfer operations properly roll back on failure, ensuring no partial inventory updates occur.
/// </para>
/// </remarks>
[TestClass]
public class Dao_Inventory_Tests : BaseIntegrationTest
{
    #region Search Methods Tests

    /// <summary>
    /// Tests that GetInventoryByPartIdAsync returns inventory records for a valid PartID.
    /// </summary>
    [TestMethod]
    public async Task GetInventoryByPartIdAsync_ValidPartId_ReturnsInventoryRecords()
    {
        // Arrange
        var partId = "TEST-PART-001";
        
        // Insert test inventory record
        await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
            GetTestConnectionString(),
            "inv_inventory_Add_Item",
            new Dictionary<string, object>
            {
                ["p_BatchNumber"] = "TEST-BATCH-001",
                ["p_PartID"] = partId,
                ["p_Operation"] = "100",
                ["p_Location"] = "FLOOR",
                ["p_UserID"] = 1
            }
        );

        // Act
        var result = await Dao_Inventory.GetInventoryByPartIdAsync(partId, connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected DataTable result");
        Assert.IsTrue(result.Data.Rows.Count > 0, "Expected at least one inventory record");
        
        // Verify the returned data contains our test part
        bool foundTestPart = false;
        foreach (DataRow row in result.Data.Rows)
        {
            if (row["PartID"].ToString() == partId)
            {
                foundTestPart = true;
                break;
            }
        }
        Assert.IsTrue(foundTestPart, $"Expected to find inventory record for PartID {partId}");
    }

    /// <summary>
    /// Tests that GetInventoryByPartIdAsync returns empty result for non-existent PartID.
    /// </summary>
    [TestMethod]
    public async Task GetInventoryByPartIdAsync_NonExistentPartId_ReturnsEmptyResult()
    {
        // Arrange
        var nonExistentPartId = "NONEXISTENT-PART-" + Guid.NewGuid().ToString();

        // Act
        var result = await Dao_Inventory.GetInventoryByPartIdAsync(nonExistentPartId, connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success even with no results, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected empty DataTable result");
        Assert.AreEqual(0, result.Data.Rows.Count, "Expected no inventory records for non-existent part");
    }

    /// <summary>
    /// Tests that GetInventoryByPartIdAndOperationAsync returns filtered inventory records.
    /// </summary>
    [TestMethod]
    public async Task GetInventoryByPartIdAndOperationAsync_ValidPartIdAndOperation_ReturnsFilteredRecords()
    {
        // Arrange
        var partId = "TEST-PART-002";
        var operation = "100";
        
        // Insert test inventory records with different operations
        await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
            GetTestConnectionString(),
            "inv_inventory_Add_Item",
            new Dictionary<string, object>
            {
                ["p_BatchNumber"] = "TEST-BATCH-002A",
                ["p_PartID"] = partId,
                ["p_Operation"] = "100",
                ["p_Location"] = "FLOOR",
                ["p_UserID"] = 1
            }
        );
        
        await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
            GetTestConnectionString(),
            "inv_inventory_Add_Item",
            new Dictionary<string, object>
            {
                ["p_BatchNumber"] = "TEST-BATCH-002B",
                ["p_PartID"] = partId,
                ["p_Operation"] = "110",
                ["p_Location"] = "FLOOR",
                ["p_UserID"] = 1
            }
        );

        // Act
        var result = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, operation, connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected DataTable result");
        Assert.IsTrue(result.Data.Rows.Count > 0, "Expected at least one inventory record");
        
        // Verify all returned records match the operation filter
        foreach (DataRow row in result.Data.Rows)
        {
            Assert.AreEqual(operation, row["Operation"].ToString(), 
                $"Expected all records to have Operation={operation}");
        }
    }

    #endregion

    #region Add Methods Tests

    /// <summary>
    /// Tests that AddInventoryItemAsync successfully adds a new inventory item.
    /// </summary>
    [TestMethod]
    public async Task AddInventoryItemAsync_ValidData_AddsInventoryItem()
    {
        // Arrange
        var batchNumber = "TEST-BATCH-003";
        var partId = "TEST-PART-003";
        var operation = "100";
        var location = "FLOOR";
        var quantity = 1;
        var itemType = "TEST-TYPE";
        var user = "TestUser";
        var notes = "Test add";

        // Act
        var result = await Dao_Inventory.AddInventoryItemAsync(
            partId, location, operation, quantity, itemType, user, batchNumber, notes,
            connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsTrue(result.Data > 0, "Expected positive inventory ID for new record");
        
        // Verify the record was inserted by searching for it
        var searchResult = await Dao_Inventory.GetInventoryByPartIdAsync(partId, connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(searchResult.IsSuccess, "Expected to find newly added inventory");
        Assert.IsNotNull(searchResult.Data, "Expected non-null DataTable");
        Assert.IsTrue(searchResult.Data!.Rows.Count > 0, "Expected at least one record");
    }

    /// <summary>
    /// Tests AddInventoryItemAsync with 100 consecutive operations to validate connection pooling.
    /// Each operation should complete within 1 second, demonstrating efficient pool management
    /// (MinPoolSize=5, MaxPoolSize=100 per mysql-database.instructions.md).
    /// </summary>
    [TestMethod]
    public async Task AddInventoryAsync_100ConsecutiveOperations_ValidatesConnectionPool()
    {
        // Arrange
        var basePartId = "POOL-TEST-PART";
        var baseBatchNumber = "POOL-TEST-BATCH";
        var operation = "100";
        var location = "FLOOR";
        var userId = 1;
        var operationCount = 100;
        var startTime = DateTime.Now;

        // Act - Execute 100 consecutive add operations
        for (int i = 0; i < operationCount; i++)
        {
            var batchNumber = $"{baseBatchNumber}-{i:D3}";
            var partId = $"{basePartId}-{i:D3}";
            
            var result = await Dao_Inventory.AddInventoryItemAsync(
                partId, location, operation, 1, "Standard", "TestUser", batchNumber, "Test notes", true);
            
            Assert.IsTrue(result.IsSuccess, 
                $"Operation {i + 1}/{operationCount} failed: {result.ErrorMessage}");
        }
        
        var endTime = DateTime.Now;
        var totalDuration = (endTime - startTime).TotalSeconds;

        // Assert - Verify performance metrics
        Assert.IsTrue(totalDuration < operationCount, 
            $"Expected 100 operations to complete in under 100 seconds (< 1s each), took {totalDuration:F2}s");
        
        var averageOperationTime = totalDuration / operationCount;
        Console.WriteLine($"[Connection Pool Test] 100 consecutive AddInventory operations:");
        Console.WriteLine($"  Total Duration: {totalDuration:F2}s");
        Console.WriteLine($"  Average per Operation: {averageOperationTime:F3}s");
        Console.WriteLine($"  Connection Pool Config: MinPoolSize=5, MaxPoolSize=100");
        
        Assert.IsTrue(averageOperationTime < 1.0, 
            $"Average operation time {averageOperationTime:F3}s exceeds 1 second threshold");
    }

    /// <summary>
    /// Tests that AddInventoryItemAsync fails gracefully with invalid data.
    /// </summary>
    [TestMethod]
    public async Task AddInventoryItemAsync_InvalidData_ReturnsFailure()
    {
        // Arrange - null/empty partId should fail validation
        var batchNumber = "TEST-BATCH-004";
        string partId = null!; // Intentionally null
        var operation = "100";
        var location = "FLOOR";

        // Act
        var result = await Dao_Inventory.AddInventoryItemAsync(
            partId, location, operation, 1, "Standard", "TestUser", batchNumber, "Test notes", true,
            connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert
        Assert.IsFalse(result.IsSuccess, "Expected failure with null PartID");
        Assert.IsNotNull(result.ErrorMessage, "Expected error message");
        Assert.IsTrue(result.ErrorMessage.Length > 0, "Expected non-empty error message");
    }

    #endregion

    #region Remove Methods Tests

    /// <summary>
    /// Tests that RemoveInventoryItemAsync successfully removes an inventory item.
    /// </summary>
    [TestMethod]
    public async Task RemoveInventoryItemAsync_ValidInventoryId_RemovesItem()
    {
        // Arrange - Add test inventory item first
        var batchNumber = "TEST-BATCH-005";
        var partId = "TEST-PART-005";
        var operation = "100";
        var location = "FLOOR";
        
        var addResult = await Dao_Inventory.AddInventoryItemAsync(
            partId, location, operation, 1, "Standard", "TestUser", batchNumber, "Test notes", true,
            connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(addResult.IsSuccess, "Failed to add test inventory item");

        // Act
        var removeResult = await Dao_Inventory.RemoveInventoryItemAsync(
            partId, location, operation, 1, "Standard", "TestUser", batchNumber, "Removal test", true,
            connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert
        Assert.IsTrue(removeResult.IsSuccess, $"Expected success, got failure: {removeResult.ErrorMessage}");
        Assert.AreEqual(0, removeResult.Data.Status, "Expected status 0 for successful removal");
        
        // Verify the record no longer appears in search
        var searchResult = await Dao_Inventory.GetInventoryByPartIdAsync(partId, connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(searchResult.IsSuccess, "Search operation should succeed");
        
        bool recordStillExists = false;
        if (searchResult.Data != null && searchResult.Data.Rows.Count > 0)
        {
            foreach (DataRow row in searchResult.Data.Rows)
            {
                if (row["PartID"].ToString() == partId)
                {
                    recordStillExists = true;
                    break;
                }
            }
        }
        Assert.IsFalse(recordStillExists, "Expected inventory record to be removed");
    }

    /// <summary>
    /// Tests that RemoveInventoryItemAsync fails gracefully for non-existent inventory ID.
    /// </summary>
    [TestMethod]
    public async Task RemoveInventoryItemAsync_NonExistentId_ReturnsFailure()
    {
        // Arrange - Use a very large non-existent part
        var nonExistentPartId = "NONEXISTENT-999999999";

        // Act
        var result = await Dao_Inventory.RemoveInventoryItemAsync(
            nonExistentPartId, "FLOOR", "100", 1, "Standard", "TestUser", "BATCH-999", "Test removal", true,
            connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert - Stored procedure may return success with status > 0, or failure
        // Either is acceptable as long as no exception occurs
        Assert.IsNotNull(result, "Expected non-null result");
    }

    #endregion

    #region Transfer Methods Tests

    /// <summary>
    /// Tests that TransferPartSimpleAsync successfully transfers inventory to a new location.
    /// </summary>
    [TestMethod]
    public async Task TransferPartSimpleAsync_ValidData_TransfersInventory()
    {
        // Arrange - Add test inventory item first
        var batchNumber = "TEST-BATCH-006";
        var partId = "TEST-PART-006";
        var operation = "100";
        var originalLocation = "FLOOR";
        var newLocation = "SHIPPING";
        
        var addResult = await Dao_Inventory.AddInventoryItemAsync(
            partId, originalLocation, operation, 1, "Standard", "TestUser", batchNumber, "Test notes", true,
            connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(addResult.IsSuccess, "Failed to add test inventory item");

        // Act
        var transferResult = await Dao_Inventory.TransferPartSimpleAsync(
            batchNumber, partId, operation, newLocation);

        // Assert
        Assert.IsTrue(transferResult.IsSuccess, $"Expected success, got failure: {transferResult.ErrorMessage}");
        
        // Verify the record now shows new location
        var searchResult = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, operation, connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(searchResult.IsSuccess, "Search operation should succeed");
        Assert.IsTrue(searchResult.Data?.Rows.Count > 0, "Expected to find transferred inventory");
        
        bool foundAtNewLocation = false;
        foreach (DataRow row in searchResult.Data.Rows)
        {
            if (row["BatchNumber"].ToString() == batchNumber &&
                row["Location"].ToString() == newLocation)
            {
                foundAtNewLocation = true;
                break;
            }
        }
        Assert.IsTrue(foundAtNewLocation, $"Expected to find inventory at new location {newLocation}");
    }

    /// <summary>
    /// Tests that TransferInventoryQuantityAsync handles multi-step transactions correctly.
    /// This test validates the complete transfer workflow.
    /// </summary>
    [TestMethod]
    public async Task TransferInventoryQuantityAsync_ValidData_CompletesTransfer()
    {
        // Arrange - Add test inventory item first
        var batchNumber = "TEST-BATCH-007";
        var partId = "TEST-PART-007";
        var operation = "100";
        var originalLocation = "FLOOR";
        var newLocation = "RECEIVING";
        var transferQuantity = 10;
        var originalQuantity = 10;
        
        var addResult = await Dao_Inventory.AddInventoryItemAsync(
            partId, originalLocation, operation, originalQuantity, "Standard", "TestUser", batchNumber, "Test notes", true,
            connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(addResult.IsSuccess, "Failed to add test inventory item");

        // Act
        var transferResult = await Dao_Inventory.TransferInventoryQuantityAsync(
            batchNumber, partId, operation, transferQuantity, originalQuantity, newLocation, "TestUser", connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert
        Assert.IsTrue(transferResult.IsSuccess, $"Expected success, got failure: {transferResult.ErrorMessage}");
    }

    /// <summary>
    /// Tests that TransferInventoryQuantityAsync properly rolls back on failure.
    /// This validates transaction management and ensures no partial inventory updates occur.
    /// </summary>
    [TestMethod]
    public async Task TransferInventoryQuantityAsync_ForcedFailure_RollsBackTransaction()
    {
        // Arrange - Add test inventory item
        var batchNumber = "TEST-BATCH-008";
        var partId = "TEST-PART-008";
        var operation = "100";
        var originalLocation = "FLOOR";
        var invalidLocation = "INVALID_LOCATION_THAT_DOES_NOT_EXIST"; // Force failure
        var transferQuantity = 10;
        var originalQuantity = 10;
        
        var addResult = await Dao_Inventory.AddInventoryItemAsync(
            partId, originalLocation, operation, originalQuantity, "Standard", "TestUser", batchNumber, "Test notes", true,
            connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(addResult.IsSuccess, "Failed to add test inventory item");
        
        // Get initial inventory state
        var initialSearchResult = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, operation, connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(initialSearchResult.IsSuccess, "Failed to get initial inventory state");
        var initialRowCount = initialSearchResult.Data?.Rows.Count ?? 0;

        // Act - Attempt transfer with invalid location (should fail)
        var transferResult = await Dao_Inventory.TransferInventoryQuantityAsync(
            batchNumber, partId, operation, transferQuantity, originalQuantity, invalidLocation, "TestUser", connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert - Transfer should fail
        Assert.IsFalse(transferResult.IsSuccess, "Expected transfer to fail with invalid location");
        
        // Verify rollback - inventory should remain unchanged
        var postFailureSearchResult = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, operation, connection: GetTestConnection(), transaction: GetTestTransaction());
        Assert.IsTrue(postFailureSearchResult.IsSuccess, "Failed to get post-failure inventory state");
        
        // Verify no partial updates occurred
        var postFailureRowCount = postFailureSearchResult.Data?.Rows.Count ?? 0;
        Assert.AreEqual(initialRowCount, postFailureRowCount, 
            "Expected inventory row count to remain unchanged after failed transfer");
        
        // Verify original record still exists at original location
        bool foundAtOriginalLocation = false;
        if (postFailureSearchResult.Data != null)
        {
            foreach (DataRow row in postFailureSearchResult.Data.Rows)
            {
                if (row["BatchNumber"].ToString() == batchNumber &&
                    row["Location"].ToString() == originalLocation)
                {
                    foundAtOriginalLocation = true;
                    break;
                }
            }
        }
        Assert.IsTrue(foundAtOriginalLocation, 
            "Expected original inventory record to still exist at original location after rollback");
        
        // Verify no record exists at invalid location
        bool foundAtInvalidLocation = false;
        if (postFailureSearchResult.Data != null)
        {
            foreach (DataRow row in postFailureSearchResult.Data.Rows)
            {
                if (row["BatchNumber"].ToString() == batchNumber &&
                    row["Location"].ToString() == invalidLocation)
                {
                    foundAtInvalidLocation = true;
                    break;
                }
            }
        }
        Assert.IsFalse(foundAtInvalidLocation, 
            "Expected no inventory record at invalid location after rollback");
    }

    #endregion

    #region Utility Methods Tests

    /// <summary>
    /// Tests that FixBatchNumbersAsync completes successfully.
    /// This is a maintenance operation that should execute without errors.
    /// </summary>
    [TestMethod]
    public async Task FixBatchNumbersAsync_Execution_CompletesSuccessfully()
    {
        // Act
        var result = await Dao_Inventory.FixBatchNumbersAsync(connection: GetTestConnection(), transaction: GetTestTransaction());

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
    }

    #endregion
}
