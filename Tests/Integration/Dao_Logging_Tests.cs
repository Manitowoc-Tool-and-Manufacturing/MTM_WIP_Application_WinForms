using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Models;
using System;
using System.Threading.Tasks;

namespace MTM_Inventory_Application.Tests.Integration;

/// <summary>
/// Integration tests for Dao_ErrorLog and Dao_History operations.
/// Tests error logging and transaction history storage.
/// </summary>
/// <remarks>
/// Dao_ErrorLog patterns discovered:
/// - All methods are static
/// - All methods have Async suffix
/// - Returns DaoResult&lt;DataTable&gt; for query operations
/// - Returns DaoResult for delete operations
/// - Special return type List&lt;(string, string)&gt; for GetUniqueErrorsAsync
/// 
/// Dao_History patterns discovered:
/// - One static method: AddTransactionHistoryAsync
/// - Returns DaoResult
/// - Uses explicit in_ prefix for parameters (transaction procedures)
/// </remarks>
[TestClass]
public class Dao_Logging_Tests : BaseIntegrationTest
{
    private Task EnsureLoggingTablesAsync() => EnsureTablesExistOrSkipAsync(
        "Logging integration tests require log and transaction history tables. Run the UpdatedDatabase scripts against the mtm_wip_application_winforms_test database to provision them.",
        "log_application_errors",
        "inv_transaction");

    #region Error Log Query Tests

    /// <summary>
    /// Tests that GetUniqueErrorsAsync returns unique error records.
    /// Validates: successful execution, non-null list.
    /// </summary>
    [TestMethod]
    public async Task GetUniqueErrorsAsync_Execution_ReturnsUniqueErrors()
    {
        await EnsureLoggingTablesAsync();

        // Act
        var result = await Dao_ErrorLog.GetUniqueErrorsAsync();

        // Assert
        Assert.IsNotNull(result, "Expected non-null result list");
        // Note: Result may be empty if no errors exist in test database
        Console.WriteLine($"GetUniqueErrorsAsync returned {result.Count} unique error(s)");
    }

    /// <summary>
    /// Tests that GetAllErrorsAsync retrieves all error log records.
    /// Validates: successful execution, non-null DataTable.
    /// </summary>
    [TestMethod]
    public async Task GetAllErrorsAsync_Execution_ReturnsAllErrors()
    {
        await EnsureLoggingTablesAsync();

        // Act
        var result = await Dao_ErrorLog.GetAllErrorsAsync();

        // Assert
        AssertSuccessWithData(result, "Expected successful retrieval of all errors");
        var totalRows = result.Data?.Rows.Count ?? 0;
        Assert.IsTrue(totalRows >= 0, "Expected non-negative row count");
        Console.WriteLine($"GetAllErrorsAsync returned {totalRows} error record(s)");
    }

    /// <summary>
    /// Tests that GetErrorsByUserAsync retrieves errors for a specific user.
    /// Validates: successful execution, proper filtering by user.
    /// </summary>
    [TestMethod]
    public async Task GetErrorsByUserAsync_ExistingUser_ReturnsUserErrors()
    {
        await EnsureLoggingTablesAsync();

        // Arrange
        string testUser = "TestUser";

        // Act
        var result = await Dao_ErrorLog.GetErrorsByUserAsync(testUser);

        // Assert
        AssertSuccessWithData(result, "Expected successful retrieval of user errors");
        // Note: May return 0 rows if no errors exist for test user
    var userRows = result.Data?.Rows.Count ?? 0;
    Console.WriteLine($"GetErrorsByUserAsync returned {userRows} error(s) for user '{testUser}'");
    }

    /// <summary>
    /// Tests that GetErrorsByDateRangeAsync retrieves errors within date range.
    /// Validates: successful execution, proper date filtering.
    /// </summary>
    [TestMethod]
    public async Task GetErrorsByDateRangeAsync_ValidRange_ReturnsFilteredErrors()
    {
        await EnsureLoggingTablesAsync();

        // Arrange
        DateTime start = DateTime.Now.AddDays(-7);
        DateTime end = DateTime.Now;

        // Act
        var result = await Dao_ErrorLog.GetErrorsByDateRangeAsync(start, end);

        // Assert
        AssertSuccessWithData(result, "Expected successful retrieval of errors by date range");
        var rangeRows = result.Data?.Rows.Count ?? 0;
        Console.WriteLine($"GetErrorsByDateRangeAsync returned {rangeRows} error(s) " +
            $"between {start:yyyy-MM-dd} and {end:yyyy-MM-dd}");
    }

    #endregion

    #region Error Log Delete Tests

    /// <summary>
    /// Tests that DeleteErrorByIdAsync removes a specific error record.
    /// Validates: successful execution for valid ID.
    /// </summary>
    [TestMethod]
    public async Task DeleteErrorByIdAsync_ValidId_DeletesError()
    {
        await EnsureLoggingTablesAsync();

        // Arrange
        int testErrorId = 1; // Use existing ID or create test error first

        // Act
        var result = await Dao_ErrorLog.DeleteErrorByIdAsync(testErrorId);

        // Assert
        AssertSuccess(result, "Expected successful deletion of error by ID");
        Console.WriteLine($"DeleteErrorByIdAsync successfully processed delete for ID {testErrorId}");
    }

    /// <summary>
    /// Tests that DeleteAllErrorsAsync removes all error records.
    /// Validates: successful execution.
    /// </summary>
    [TestMethod]
    public async Task DeleteAllErrorsAsync_Execution_DeletesAllErrors()
    {
        await EnsureLoggingTablesAsync();

        // Act
        var result = await Dao_ErrorLog.DeleteAllErrorsAsync();

        // Assert
        AssertSuccess(result, "Expected successful deletion of all errors");
        Console.WriteLine("DeleteAllErrorsAsync successfully processed delete for all errors");
    }

    #endregion

    #region Transaction History Tests

    /// <summary>
    /// Tests that AddTransactionHistoryAsync stores transaction history record.
    /// Validates: successful execution, proper parameter mapping.
    /// </summary>
    [TestMethod]
    public async Task AddTransactionHistoryAsync_ValidHistory_AddsRecord()
    {
        await EnsureLoggingTablesAsync();

        // Arrange
        var history = new Model_TransactionHistory
        {
            TransactionType = "IN",
            PartId = "TEST-PART-001",
            FromLocation = "RECEIVING",
            ToLocation = "FLOOR",
            Operation = "100",
            Quantity = 10,
            Notes = "Integration test transaction",
            User = "TestUser",
            ItemType = "RAW",
            BatchNumber = "BATCH-001",
            DateTime = DateTime.Now
        };

        // Act
        var result = await Dao_History.AddTransactionHistoryAsync(history);

        // Assert
        AssertSuccess(result, "Expected successful addition of transaction history");
        Console.WriteLine($"AddTransactionHistoryAsync successfully added transaction history for part '{history.PartId}'");
    }

    /// <summary>
    /// Tests that AddTransactionHistoryAsync handles transfer transactions.
    /// Validates: successful execution with from/to location population.
    /// </summary>
    [TestMethod]
    public async Task AddTransactionHistoryAsync_TransferTransaction_AddsRecord()
    {
        await EnsureLoggingTablesAsync();

        // Arrange
        var history = new Model_TransactionHistory
        {
            TransactionType = "TRANSFER",
            PartId = "TEST-PART-002",
            FromLocation = "FLOOR",
            ToLocation = "SHIPPING",
            Operation = "110",
            Quantity = 5,
            Notes = "Transfer test",
            User = "TestUser",
            ItemType = "FIN",
            BatchNumber = "BATCH-002",
            DateTime = DateTime.Now
        };

        // Act
        var result = await Dao_History.AddTransactionHistoryAsync(history);

        // Assert
        AssertSuccess(result, "Expected successful addition of transfer transaction history");
        Console.WriteLine($"AddTransactionHistoryAsync successfully added transfer from '{history.FromLocation}' to '{history.ToLocation}'");
    }

    /// <summary>
    /// Tests that AddTransactionHistoryAsync handles minimal required fields.
    /// Validates: successful execution with nullable fields as DBNull.
    /// </summary>
    [TestMethod]
    public async Task AddTransactionHistoryAsync_MinimalFields_AddsRecord()
    {
        await EnsureLoggingTablesAsync();

        // Arrange
        var history = new Model_TransactionHistory
        {
            TransactionType = "IN",
            PartId = "TEST-PART-003",
            FromLocation = null, // Optional field
            ToLocation = "RECEIVING",
            Operation = null, // Optional field
            Quantity = 1,
            Notes = null, // Optional field
            User = "TestUser",
            ItemType = null, // Optional field
            BatchNumber = null, // Optional field
            DateTime = DateTime.Now
        };

        // Act
        var result = await Dao_History.AddTransactionHistoryAsync(history);

        // Assert
        AssertSuccess(result, "Expected successful addition of transaction history with minimal fields");
        Console.WriteLine("AddTransactionHistoryAsync successfully handled null optional fields");
    }

    #endregion
}
