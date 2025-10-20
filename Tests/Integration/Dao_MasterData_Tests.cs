using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;

namespace MTM_Inventory_Application.Tests.Integration;

/// <summary>
/// Integration tests for master data DAO operations (ItemType, Location, Operation, Part).
/// Tests database interactions for CRUD operations on configuration tables.
/// </summary>
[TestClass]
public class Dao_MasterData_Tests : BaseIntegrationTest
{
    #region Item Type Tests

    /// <summary>
    /// Tests that GetAllItemTypes returns item type list.
    /// </summary>
    [TestMethod]
    public async Task GetAllItemTypes_Execution_ReturnsItemTypes()
    {
        // Act
        var result = await Dao_ItemType.GetAllItemTypes();

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected item type DataTable");
    }

    /// <summary>
    /// Tests that ItemTypeExists correctly identifies existing item types.
    /// </summary>
    [TestMethod]
    public async Task ItemTypeExists_ExistingType_ReturnsTrue()
    {
        // Arrange - Get an existing item type
        var allTypes = await Dao_ItemType.GetAllItemTypes();
        Assert.IsTrue(allTypes.IsSuccess && allTypes.Data != null && allTypes.Data.Rows.Count > 0, "Need at least one item type for test");
        var existingType = allTypes.Data.Rows[0]["ItemType"].ToString();

        // Act
        var result = await Dao_ItemType.ItemTypeExists(existingType!);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsTrue(result.Data, $"Expected item type {existingType} to exist");
    }

    /// <summary>
    /// Tests that ItemTypeExists correctly identifies non-existent item types.
    /// </summary>
    [TestMethod]
    public async Task ItemTypeExists_NonExistentType_ReturnsFalse()
    {
        // Arrange
        var nonExistentType = "NonExistent_" + Guid.NewGuid().ToString();

        // Act
        var result = await Dao_ItemType.ItemTypeExists(nonExistentType);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsFalse(result.Data, $"Expected item type {nonExistentType} to not exist");
    }

    #endregion

    #region Location Tests

    /// <summary>
    /// Tests that GetAllLocations returns location list.
    /// </summary>
    [TestMethod]
    public async Task GetAllLocations_Execution_ReturnsLocations()
    {
        // Act
        var result = await Dao_Location.GetAllLocations();

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected location DataTable");
    }

    /// <summary>
    /// Tests that LocationExists correctly identifies existing locations.
    /// </summary>
    [TestMethod]
    public async Task LocationExists_ExistingLocation_ReturnsTrue()
    {
        // Arrange - Get an existing location
        var allLocations = await Dao_Location.GetAllLocations();
        Assert.IsTrue(allLocations.IsSuccess && allLocations.Data != null && allLocations.Data.Rows.Count > 0, "Need at least one location for test");
        var existingLocation = allLocations.Data.Rows[0]["Location"].ToString();

        // Act
        var result = await Dao_Location.LocationExists(existingLocation!);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsTrue(result.Data, $"Expected location {existingLocation} to exist");
    }

    /// <summary>
    /// Tests that LocationExists correctly identifies non-existent locations.
    /// </summary>
    [TestMethod]
    public async Task LocationExists_NonExistentLocation_ReturnsFalse()
    {
        // Arrange
        var nonExistentLocation = "NonExistent_" + Guid.NewGuid().ToString();

        // Act
        var result = await Dao_Location.LocationExists(nonExistentLocation);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsFalse(result.Data, $"Expected location {nonExistentLocation} to not exist");
    }

    #endregion

    #region Operation Tests

    /// <summary>
    /// Tests that GetAllOperations returns operation list.
    /// </summary>
    [TestMethod]
    public async Task GetAllOperations_Execution_ReturnsOperations()
    {
        // Act
        var result = await Dao_Operation.GetAllOperations();

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected operation DataTable");
    }

    /// <summary>
    /// Tests that OperationExists correctly identifies existing operations.
    /// </summary>
    [TestMethod]
    public async Task OperationExists_ExistingOperation_ReturnsTrue()
    {
        // Arrange - Get an existing operation
        var allOperations = await Dao_Operation.GetAllOperations();
        Assert.IsTrue(allOperations.IsSuccess && allOperations.Data != null && allOperations.Data.Rows.Count > 0, "Need at least one operation for test");
        var existingOperation = allOperations.Data.Rows[0]["Operation"].ToString();

        // Act
        var result = await Dao_Operation.OperationExists(existingOperation!);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsTrue(result.Data, $"Expected operation {existingOperation} to exist");
    }

    /// <summary>
    /// Tests that OperationExists correctly identifies non-existent operations.
    /// </summary>
    [TestMethod]
    public async Task OperationExists_NonExistentOperation_ReturnsFalse()
    {
        // Arrange
        var nonExistentOperation = "999";

        // Act
        var result = await Dao_Operation.OperationExists(nonExistentOperation);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsFalse(result.Data, $"Expected operation {nonExistentOperation} to not exist");
    }

    #endregion

    #region Part Tests

    /// <summary>
    /// Tests that GetAllPartsAsync returns part list.
    /// </summary>
    [TestMethod]
    public async Task GetAllPartsAsync_Execution_ReturnsParts()
    {
        // Act
        var result = await Dao_Part.GetAllPartsAsync();

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected part DataTable");
    }

    /// <summary>
    /// Tests that GetPartByNumberAsync returns specific part.
    /// </summary>
    [TestMethod]
    public async Task GetPartByNumberAsync_ExistingPart_ReturnsPart()
    {
        // Arrange - Get an existing part
        var allParts = await Dao_Part.GetAllPartsAsync();
        Assert.IsTrue(allParts.IsSuccess && allParts.Data != null && allParts.Data.Rows.Count > 0, "Need at least one part for test");
        var existingPartNumber = allParts.Data.Rows[0]["PartID"].ToString();  // Column is PartID, not ItemNumber

        // Act
        var result = await Dao_Part.GetPartByNumberAsync(existingPartNumber!);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected part DataRow");
    }

    /// <summary>
    /// Tests that PartExistsAsync correctly identifies existing parts.
    /// </summary>
    [TestMethod]
    public async Task PartExistsAsync_ExistingPart_ReturnsTrue()
    {
        // Arrange - Get an existing part
        var allParts = await Dao_Part.GetAllPartsAsync();
        Assert.IsTrue(allParts.IsSuccess && allParts.Data != null && allParts.Data.Rows.Count > 0, "Need at least one part for test");
        var existingPartNumber = allParts.Data.Rows[0]["PartID"].ToString();  // Column is PartID, not ItemNumber

        // Act
        var result = await Dao_Part.PartExistsAsync(existingPartNumber!);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsTrue(result.Data, $"Expected part {existingPartNumber} to exist");
    }

    /// <summary>
    /// Tests that PartExistsAsync correctly identifies non-existent parts.
    /// </summary>
    [TestMethod]
    public async Task PartExistsAsync_NonExistentPart_ReturnsFalse()
    {
        // Arrange
        var nonExistentPart = "NonExistent_" + Guid.NewGuid().ToString();

        // Act
        var result = await Dao_Part.PartExistsAsync(nonExistentPart);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsFalse(result.Data, $"Expected part {nonExistentPart} to not exist");
    }

    #endregion
}
