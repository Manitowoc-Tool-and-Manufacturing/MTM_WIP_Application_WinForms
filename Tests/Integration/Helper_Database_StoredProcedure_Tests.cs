using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Tests.Integration;

/// <summary>
/// Integration tests for Helper_Database_StoredProcedure with focus on parameter prefix detection,
/// DaoResult pattern integration, and stored procedure execution.
/// </summary>
[TestClass]
public class Helper_Database_StoredProcedure_Tests : BaseIntegrationTest
{
    #region Parameter Prefix Detection Tests

    /// <summary>
    /// Tests that ExecuteDataTableWithStatusAsync correctly applies p_ prefix from cache
    /// </summary>
    [TestMethod]
    public async Task ExecuteDataTableWithStatusAsync_WithP_Prefix_AppliesCorrectly()
    {
        // Arrange - Get a valid test user from the system
        var usersResult = await Dao_System.System_UserAccessTypeAsync();
        AssertSuccessWithData(usersResult);
        var testUser = usersResult.Data![0].User;

        var parameters = new Dictionary<string, object>
        {
            ["User"] = testUser // Parameter name without prefix
        };

        // Act
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            "sys_user_GetByName", // This procedure uses p_ prefix (p_User)
            parameters);

        // Assert - Should succeed because prefix detection adds p_ automatically
        AssertSuccessWithData(result);
        Assert.IsNotNull(result.Data, "Should return DataTable");
    }

    /// <summary>
    /// Tests that ExecuteScalarWithStatusAsync correctly applies p_ prefix from cache
    /// </summary>
    [TestMethod]
    public async Task ExecuteScalarWithStatusAsync_WithP_Prefix_AppliesCorrectly()
    {
        // Arrange - Get a valid test user from the system
        var usersResult = await Dao_System.System_UserAccessTypeAsync();
        AssertSuccessWithData(usersResult);
        var testUser = usersResult.Data![0].User;

        var parameters = new Dictionary<string, object>
        {
            ["UserName"] = testUser // Parameter name without prefix
        };

        // Act
        var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
            GetTestConnectionString(),
            "sys_user_GetIdByName", // This procedure uses p_ prefix (p_UserName)
            parameters);

        // Assert
        AssertSuccessWithData(result);
        Assert.IsNotNull(result.Data, "Should return user ID scalar value");
    }

    /// <summary>
    /// Tests that ExecuteNonQueryWithStatusAsync correctly applies p_ prefix from cache
    /// </summary>
    [TestMethod]
    public async Task ExecuteNonQueryWithStatusAsync_WithP_Prefix_AppliesCorrectly()
    {
        // Arrange - Get a valid test user from the system
        var usersResult = await MTM_WIP_Application_Winforms.Data.Dao_System.System_UserAccessTypeAsync();
        AssertSuccessWithData(usersResult);
        var testUser = usersResult.Data![0].User;
        var currentAccessType = usersResult.Data![0].VitsUser;
        var newAccessType = currentAccessType ? 0 : 1; // Toggle between 0 and 1 (valid tinyint(1) values)

        var parameters = new Dictionary<string, object>
        {
            ["User"] = testUser,
            ["AccessType"] = newAccessType
        };

        // Act
        var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
            GetTestConnectionString(),
            "sys_user_access_SetType", // This procedure uses p_ prefix
            parameters);

        // Assert - Should succeed (updates access type successfully)
        Console.WriteLine($"Result - IsSuccess: {result.IsSuccess}, Message: '{result.StatusMessage}', Error: '{result.ErrorMessage}'");
        Assert.IsTrue(result.IsSuccess, $"Expected success but got Message='{result.StatusMessage}', Error='{result.ErrorMessage}'");
        // ExecuteNonQueryWithStatusAsync returns DaoResult (no Data property), not DaoResult<T>
    }

    /// <summary>
    /// Tests fallback prefix convention when procedure not in cache
    /// </summary>
    [TestMethod]
    public async Task ParameterPrefixDetection_WithUncachedProcedure_UsesFallback()
    {
        // Act - Call with a procedure that should work with fallback prefix logic
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            "sys_theme_GetAll", // Procedure that takes no input parameters
            parameters: null);

        // Assert - Should not crash even if prefix detection uses fallback
        Assert.IsNotNull(result, "Should return a result even with fallback prefix");
        AssertSuccessWithData(result);
    }

    #endregion

    #region DaoResult Pattern Tests

    /// <summary>
    /// Tests that successful ExecuteDataTableWithStatusAsync returns DaoResult with IsSuccess=true
    /// </summary>
    [TestMethod]
    public async Task ExecuteDataTableWithStatusAsync_Success_ReturnsDaoResultSuccess()
    {
        // Arrange - Get a valid test user from the system
        var usersResult = await Dao_System.System_UserAccessTypeAsync();
        AssertSuccessWithData(usersResult);
        var testUser = usersResult.Data![0].User;

        var parameters = new Dictionary<string, object>
        {
            ["User"] = testUser
        };

        // Act
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            "sys_user_GetByName",
            parameters);

        // Assert
        Assert.IsTrue(result.IsSuccess, "IsSuccess should be true for valid operation");
        Assert.IsNotNull(result.Data, "Data should contain DataTable");
        Assert.IsTrue(result.Data.Rows.Count >= 0, "DataTable should have rows collection");
        Assert.IsNull(result.Exception, "Exception should be null on success");
    }

    /// <summary>
    /// Tests that database errors are captured in DaoResult with IsSuccess=false
    /// </summary>
    [TestMethod]
    public async Task ExecuteDataTableWithStatusAsync_WithInvalidConnection_ReturnsDaoResultFailure()
    {
        // Arrange - Invalid connection string
        var invalidConnectionString = "Server=invalid_host;Database=nonexistent;User=nobody;Password=wrong;";
        
        var parameters = new Dictionary<string, object>
        {
            ["User"] = "test_user"
        };

        // Act
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            invalidConnectionString,
            "sys_user_GetByName",
            parameters);

        // Assert
        Assert.IsFalse(result.IsSuccess, "IsSuccess should be false for connection failure");
        Assert.IsNotNull(result.StatusMessage, "StatusMessage should contain error details");
        Assert.IsNotNull(result.Exception, "Exception should be captured");
    }

    #endregion

    #region Null and Empty Parameter Tests

    /// <summary>
    /// Tests that null parameters dictionary is handled gracefully
    /// </summary>
    [TestMethod]
    public async Task ExecuteDataTableWithStatusAsync_WithNullParameters_HandlesGracefully()
    {
        // Act
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            "sys_theme_GetAll", // Procedure that takes no input parameters
            parameters: null);

        // Assert
        AssertSuccessWithData(result);
        Assert.IsNotNull(result.Data, "Should return DataTable even with null parameters");
    }

    /// <summary>
    /// Tests that empty parameters dictionary is handled gracefully
    /// </summary>
    [TestMethod]
    public async Task ExecuteDataTableWithStatusAsync_WithEmptyParameters_HandlesGracefully()
    {
        // Arrange
        var parameters = new Dictionary<string, object>(); // Empty dictionary

        // Act
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            "sys_theme_GetAll",
            parameters);

        // Assert
        AssertSuccessWithData(result);
        Assert.IsNotNull(result.Data, "Should return DataTable even with empty parameters");
    }

    #endregion

    #region Connection Management Tests

    /// <summary>
    /// Tests that connection is properly disposed after successful execution
    /// </summary>
    [TestMethod]
    public async Task ExecuteDataTableWithStatusAsync_AfterExecution_ConnectionDisposed()
    {
        // Arrange - Get a valid test user from the system
        var usersResult = await Dao_System.System_UserAccessTypeAsync();
        AssertSuccessWithData(usersResult);
        var testUser = usersResult.Data![0].User;

        var parameters = new Dictionary<string, object>
        {
            ["User"] = testUser
        };

        // Act
        var result1 = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            "sys_user_GetByName",
            parameters);

        // Act - Execute again immediately to verify connection pool healthy
        var result2 = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            "sys_user_GetByName",
            parameters);

        // Assert - Both calls should succeed (connection properly released)
        AssertSuccessWithData(result1);
        AssertSuccessWithData(result2);
    }

    /// <summary>
    /// Tests that multiple concurrent operations share connection pool correctly
    /// </summary>
    [TestMethod]
    public async Task ExecuteDataTableWithStatusAsync_ConcurrentOperations_ShareConnectionPool()
    {
        // Arrange - Get a valid test user from the system
        var usersResult = await Dao_System.System_UserAccessTypeAsync();
        AssertSuccessWithData(usersResult);
        var testUser = usersResult.Data![0].User;

        var parameters = new Dictionary<string, object>
        {
            ["User"] = testUser
        };

        // Act - Execute 10 concurrent operations
        var tasks = new List<Task<DaoResult<DataTable>>>();
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                GetTestConnectionString(),
                "sys_user_GetByName",
                parameters));
        }

        var results = await Task.WhenAll(tasks);

        // Assert - All operations should succeed
        foreach (var result in results)
        {
            AssertSuccessWithData(result);
        }
    }

    #endregion

    #region Performance Tests

    /// <summary>
    /// Tests that operations complete within reasonable timeout
    /// </summary>
    [TestMethod]
    public async Task ExecuteDataTableWithStatusAsync_CompletesWithinTimeout()
    {
        // Arrange - Get a valid test user from the system
        var usersResult = await Dao_System.System_UserAccessTypeAsync();
        AssertSuccessWithData(usersResult);
        var testUser = usersResult.Data![0].User;

        var parameters = new Dictionary<string, object>
        {
            ["User"] = testUser
        };

        var startTime = DateTime.UtcNow;

        // Act
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            "sys_user_GetByName",
            parameters);

        var elapsed = DateTime.UtcNow - startTime;

        // Assert
        AssertSuccessWithData(result);
        Assert.IsTrue(elapsed.TotalSeconds < 30, 
            $"Operation should complete within 30 seconds (took {elapsed.TotalSeconds:F2}s)");
    }

    #endregion
}
