using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;

namespace MTM_Inventory_Application.Tests.Integration;

/// <summary>
/// Integration tests for Dao_Transactions operations.
/// Tests database interactions for transaction search and analytics.
/// </summary>
[TestClass]
public class Dao_Transactions_Tests : BaseIntegrationTest
{
    #region Search Methods Tests

    /// <summary>
    /// Tests that SearchTransactionsAsync returns transactions for valid criteria.
    /// </summary>
    [TestMethod]
    public async Task SearchTransactionsAsync_ValidCriteria_ReturnsTransactions()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;
        
        // Act
        var dao = new Dao_Transactions();
        var result = await dao.SearchTransactionsAsync(
            userName: userName,
            isAdmin: isAdmin,
            partID: "TEST"
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected transaction list");
    }

    /// <summary>
    /// Tests that SearchTransactionsAsync handles pagination correctly.
    /// </summary>
    [TestMethod]
    public async Task SearchTransactionsAsync_Pagination_ReturnsPaginatedResults()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;
        var pageSize = 5;
        var page = 1;

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.SearchTransactionsAsync(
            userName: userName,
            isAdmin: isAdmin,
            page: page,
            pageSize: pageSize
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected transaction list");
        Assert.IsTrue(result.Data.Count <= pageSize, 
            $"Expected at most {pageSize} transactions, got {result.Data.Count}");
    }

    /// <summary>
    /// Tests that SearchTransactionsAsync filters by transaction type correctly.
    /// </summary>
    [TestMethod]
    public async Task SearchTransactionsAsync_FilterByType_ReturnsFilteredResults()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;
        var transactionType = TransactionType.IN;

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.SearchTransactionsAsync(
            userName: userName,
            isAdmin: isAdmin,
            transactionType: transactionType
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected transaction list");
        
        // Verify all returned transactions match the filter
        foreach (var transaction in result.Data)
        {
            Assert.AreEqual(transactionType, transaction.TransactionType,
                $"Expected all transactions to be {transactionType}");
        }
    }

    /// <summary>
    /// Tests that SearchTransactionsAsync filters by date range correctly.
    /// </summary>
    [TestMethod]
    public async Task SearchTransactionsAsync_DateRangeFilter_ReturnsFilteredResults()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;
        var fromDate = DateTime.Today.AddDays(-7);
        var toDate = DateTime.Today;

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.SearchTransactionsAsync(
            userName: userName,
            isAdmin: isAdmin,
            fromDate: fromDate,
            toDate: toDate
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected transaction list");
    }

    #endregion

    #region SmartSearch Methods Tests

    /// <summary>
    /// Tests that SmartSearchAsync returns results with valid parameters.
    /// </summary>
    [TestMethod]
    public async Task SmartSearchAsync_ValidParameters_ReturnsResults()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;
        var searchTerms = new Dictionary<string, string>();
        var transactionTypes = new List<TransactionType>();
        var timeRange = ((DateTime?)null, (DateTime?)null);
        var locations = new List<string>();

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.SmartSearchAsync(
            searchTerms: searchTerms,
            transactionTypes: transactionTypes,
            timeRange: timeRange,
            locations: locations,
            userName: userName,
            isAdmin: isAdmin
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected transaction list");
    }

    #endregion

    #region Analytics Methods Tests

    /// <summary>
    /// Tests that GetTransactionAnalyticsAsync returns analytics data.
    /// </summary>
    [TestMethod]
    public async Task GetTransactionAnalyticsAsync_ValidUser_ReturnsAnalytics()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.GetTransactionAnalyticsAsync(
            userName: userName,
            isAdmin: isAdmin,
            timeRange: (null, null)
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected analytics data");
    }

    /// <summary>
    /// Tests that GetTransactionAnalyticsAsync handles date range correctly.
    /// </summary>
    [TestMethod]
    public async Task GetTransactionAnalyticsAsync_DateRange_ReturnsFilteredAnalytics()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;
        var fromDate = DateTime.Today.AddMonths(-1);
        var toDate = DateTime.Today;

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.GetTransactionAnalyticsAsync(
            userName: userName,
            isAdmin: isAdmin,
            timeRange: (fromDate, toDate)
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected analytics data");
    }

    #endregion
}
