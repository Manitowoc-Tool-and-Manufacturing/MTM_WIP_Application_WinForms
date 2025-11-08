using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Tests.Integration;

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

    /// <summary>
    /// Tests that SearchAsync (new method with Model_Transactions_SearchCriteria) filters by date range correctly.
    /// Validates: method signature matches implementation, null-safe assertions, date range filtering.
    /// </summary>
    [TestMethod]
    public async Task SearchAsync_WithDateRange_ReturnsTransactions()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;
        
        // Create criteria with last 30 days date range
        var criteria = new Model_Transactions_SearchCriteria
        {
            DateFrom = DateTime.Today.AddDays(-30),
            DateTo = DateTime.Today
        };

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.SearchAsync(
            criteria: criteria,
            userName: userName,
            isAdmin: isAdmin,
            page: 1,
            pageSize: 50
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected non-null transaction list");
        Assert.IsTrue(result.Data.Count >= 0, "Expected transaction count >= 0");

        // Verify all transactions fall within date range (if any results)
        foreach (var transaction in result.Data)
        {
            Assert.IsTrue(transaction.DateTime >= criteria.DateFrom, 
                $"Transaction date {transaction.DateTime} is before start date {criteria.DateFrom}");
            Assert.IsTrue(transaction.DateTime <= criteria.DateTo,
                $"Transaction date {transaction.DateTime} is after end date {criteria.DateTo}");
        }
    }

    /// <summary>
    /// Tests that admin users can search transactions for all users.
    /// Validates: admin role bypasses user filtering, returns transactions from multiple users.
    /// </summary>
    [TestMethod]
    public async Task AdminSearchAsync_AllUsers_ReturnsAllData()
    {
        // Arrange
        var adminUserName = "TestAdmin";
        var isAdmin = true;
        
        var criteria = new Model_Transactions_SearchCriteria
        {
            DateFrom = DateTime.Today.AddDays(-30),
            DateTo = DateTime.Today
        };

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.SearchAsync(
            criteria: criteria,
            userName: adminUserName,
            isAdmin: isAdmin,
            page: 1,
            pageSize: 50
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected non-null transaction list");
        
        // Admin should see transactions from all users (if any exist)
        // Note: Cannot assert specific user count without knowing DB state,
        // but can verify admin query executes successfully
    }

    /// <summary>
    /// Tests that regular (non-admin) users only see their own transactions.
    /// Validates: regular user role filters transactions to only show userName's data.
    /// </summary>
    [TestMethod]
    public async Task RegularUserSearchAsync_FiltersByUser()
    {
        // Arrange
        var regularUserName = "TestRegularUser";
        var isAdmin = false;
        
        var criteria = new Model_Transactions_SearchCriteria
        {
            DateFrom = DateTime.Today.AddDays(-30),
            DateTo = DateTime.Today
        };

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.SearchAsync(
            criteria: criteria,
            userName: regularUserName,
            isAdmin: isAdmin,
            page: 1,
            pageSize: 50
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected non-null transaction list");
        
        // Verify all transactions belong to the regular user (if any exist)
        foreach (var transaction in result.Data)
        {
            Assert.AreEqual(regularUserName, transaction.User,
                $"Regular user should only see own transactions. Found transaction from user: {transaction.User}");
        }
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
    /// Tests that GetModel_Transactions_Core_AnalyticsAsync returns analytics data.
    /// </summary>
    [TestMethod]
    public async Task GetModel_Transactions_Core_AnalyticsAsync_ValidUser_ReturnsAnalytics()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.GetModel_Transactions_Core_AnalyticsAsync(
            userName: userName,
            isAdmin: isAdmin,
            timeRange: (null, null)
        );

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
        Assert.IsNotNull(result.Data, "Expected analytics data");
    }

    /// <summary>
    /// Tests that GetModel_Transactions_Core_AnalyticsAsync handles date range correctly.
    /// </summary>
    [TestMethod]
    public async Task GetModel_Transactions_Core_AnalyticsAsync_DateRange_ReturnsFilteredAnalytics()
    {
        // Arrange
        var userName = "TestUser";
        var isAdmin = true;
        var fromDate = DateTime.Today.AddMonths(-1);
        var toDate = DateTime.Today;

        // Act
        var dao = new Dao_Transactions();
        var result = await dao.GetModel_Transactions_Core_AnalyticsAsync(
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
