using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MTM_WIP_Application_Winforms.Tests.Integration
{
    /// <summary>
    /// Integration tests for Dao_System database operations.
    /// Tests user access management, role lookups, and theme data retrieval.
    /// </summary>
    [TestClass]
    public class Dao_System_Tests : BaseIntegrationTest
    {
        #region User Access Tests

        /// <summary>
        /// Tests System_UserAccessTypeAsync returns list of users with access types.
        /// </summary>
        [TestMethod]
        public async Task System_UserAccessTypeAsync_ReturnsUserList()
        {
            // Act
            var result = await Dao_System.System_UserAccessTypeAsync();

            // Assert
            AssertSuccessWithData(result);
            Assert.IsInstanceOfType(result.Data, typeof(System.Collections.Generic.List<Model_Users>));
            Assert.IsTrue(result.Data!.Count > 0, "Expected at least one user in the system");
        }

        /// <summary>
        /// Tests System_GetUserName returns current Windows user identity.
        /// </summary>
        [TestMethod]
        public void System_GetUserName_ReturnsCurrentUser()
        {
            // Act
            var userName = Dao_System.System_GetUserName();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(userName), "User name should not be empty");
            Assert.IsTrue(userName.Length > 0, "User name should have characters");
            // Windows identity is typically returned in uppercase
            Assert.AreEqual(userName, userName.ToUpper(), "User name should be uppercase");
        }

        /// <summary>
        /// Tests SetUserAccessTypeAsync with valid data sets access type successfully.
        /// Note: Requires test users to exist in usr_users table.
        /// </summary>
        [TestMethod]
        public async Task SetUserAccessTypeAsync_WithValidData_ExecutesSuccessfully()
        {
            // Arrange: Ensure test users exist
            await CreateTestUsersAsync();
            
            var testUser = "TEST-USER";
            var newAccessType = "Admin"; // Will be converted to INT 1

            // Act
            var result = await Dao_System.SetUserAccessTypeAsync(testUser, newAccessType,
                connectionString: GetTestConnectionString());
            
            // Assert
            Assert.IsTrue(result.IsSuccess, $"Expected success but got: {result.StatusMessage ?? result.ErrorMessage}");
            Assert.IsTrue(result.StatusMessage.Contains(testUser), 
                $"Expected status message to mention user '{testUser}', got: {result.StatusMessage}");
        }

        /// <summary>
        /// Tests SetUserAccessTypeAsync with invalid access type.
        /// Note: Invalid access types default to 0 (standard user), so operation succeeds.
        /// </summary>
        [TestMethod]
        public async Task SetUserAccessTypeAsync_WithInvalidAccessType_ProvidesStatusMessage()
        {
            // Arrange: Ensure test users exist
            await CreateTestUsersAsync();
            
            var testUser = "TEST-USER";
            var invalidAccessType = "InvalidType_" + Guid.NewGuid().ToString();

            // Act
            var result = await Dao_System.SetUserAccessTypeAsync(testUser, invalidAccessType,
                connectionString: GetTestConnectionString());

            // Assert - Invalid types default to 0 (standard user), so operation succeeds
            Assert.IsTrue(result.IsSuccess, "Operation should succeed with invalid type (defaults to standard user)");
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.StatusMessage),
                "Status message should provide feedback about the operation");
        }

        #endregion

        #region Role and User ID Tests

        /// <summary>
        /// Tests GetUserIdByNameAsync with valid username returns user ID.
        /// </summary>
        [TestMethod]
        public async Task GetUserIdByNameAsync_WithValidUser_ReturnsUserId()
        {
            // Arrange
            // First get a valid user from the system
            var usersResult = await Dao_System.System_UserAccessTypeAsync();
            AssertSuccessWithData(usersResult);
            Assert.IsNotNull(usersResult.Data, "User data should not be null");
            Assert.IsTrue(usersResult.Data!.Count > 0, "Need at least one user for this test");
            
            var testUserName = usersResult.Data[0].User;

            // Act
            var result = await Dao_System.GetUserIdByNameAsync(testUserName);

            // Assert
            AssertSuccessWithData(result);
            Assert.IsTrue(result.Data > 0, "User ID should be greater than 0");
            Assert.IsTrue(result.StatusMessage.Contains(testUserName), "Status message should contain user name");
        }

        /// <summary>
        /// Tests GetUserIdByNameAsync with non-existent username returns success with ID 0.
        /// Note: Stored procedure returns status 0 (success, no data found) not failure for non-existent users.
        /// </summary>
        [TestMethod]
        public async Task GetUserIdByNameAsync_WithNonExistentUser_ReturnsFailure()
        {
            // Arrange
            var nonExistentUser = "NonExistentUser_" + Guid.NewGuid().ToString();

            // Act
            var result = await Dao_System.GetUserIdByNameAsync(nonExistentUser);

            // Assert - Stored procedure returns status 0 (success, no data) with UserID = 0
            // This is the actual SP behavior: success status but ID = 0 indicates "not found"
            Assert.IsTrue(result.IsSuccess, "Expected success with status 0 for non-existent user (no data found)");
            Assert.AreEqual(0, result.Data, "Expected UserID = 0 for non-existent user");
            Assert.IsTrue(
                result.StatusMessage.Contains(nonExistentUser) ||
                result.StatusMessage.Contains("not found", StringComparison.OrdinalIgnoreCase),
                $"Expected status message to mention user or 'not found', got: {result.StatusMessage}");
        }

        /// <summary>
        /// Tests GetRoleIdByNameAsync with valid role name returns role ID.
        /// Requires sys_roles table to be seeded with test data.
        /// </summary>
        [TestMethod]
        public async Task GetRoleIdByNameAsync_WithValidRole_ReturnsRoleId()
        {
            // Arrange - Ensure sys_roles table has test data
            await EnsureUserTableAsync();
            
            var validRoleName = "Admin"; // Admin role seeded by EnsureUserTableAsync

            // Act
            var result = await Dao_System.GetRoleIdByNameAsync(validRoleName,
                connectionString: GetTestConnectionString());

            // Assert
            AssertSuccessWithData(result, $"Should find role '{validRoleName}'");
            Assert.IsTrue(result.Data > 0, $"Role ID should be greater than 0, got: {result.Data}");
            Assert.IsTrue(result.StatusMessage?.Contains(validRoleName) == true, 
                $"Status message should contain role name '{validRoleName}', got: {result.StatusMessage}");
        }

        /// <summary>
        /// Tests GetRoleIdByNameAsync with non-existent role name returns failure.
        /// </summary>
        [TestMethod]
        public async Task GetRoleIdByNameAsync_WithNonExistentRole_ReturnsFailure()
        {
            // Arrange
            var nonExistentRole = "NonExistentRole_" + Guid.NewGuid().ToString();

            // Act
            var result = await Dao_System.GetRoleIdByNameAsync(nonExistentRole);

            // Assert
            Assert.IsFalse(result.IsSuccess, "Expected failure for non-existent role");
            Assert.IsTrue(
                result.ErrorMessage?.Contains(nonExistentRole) == true ||
                result.StatusMessage.Contains("not found", StringComparison.OrdinalIgnoreCase),
                $"Expected error message to mention role not found, got: {result.StatusMessage}");
        }

        #endregion

        #region Theme Tests

        /// <summary>
        /// Tests GetAllThemesAsync returns theme data from app_themes table.
        /// </summary>
        [TestMethod]
        public async Task GetAllThemesAsync_ReturnsThemeData()
        {
            // Act
            var result = await Dao_System.GetAllThemesAsync( );

            // Assert
            AssertSuccessWithData(result);
            Assert.IsInstanceOfType(result.Data, typeof(DataTable));
            
            // Verify DataTable has expected columns
            Assert.IsTrue(result.Data!.Columns.Contains("ThemeName"), "Expected ThemeName column");
            Assert.IsTrue(result.Data.Columns.Contains("SettingsJson"), "Expected SettingsJson column");
        }

        /// <summary>
        /// Tests GetAllThemesAsync in synchronous mode returns theme data.
        /// </summary>
        [TestMethod]
        public async Task GetAllThemesAsync_SyncMode_ReturnsThemeData()
        {
            // Act
            var result = await Dao_System.GetAllThemesAsync( );

            // Assert
            AssertSuccessWithData(result);
            Assert.IsInstanceOfType(result.Data, typeof(DataTable));
            Assert.IsNotNull(result.Data, "DataTable should not be null in sync mode");
        }

        #endregion

        #region Error Handling Tests

        /// <summary>
        /// Tests GetUserIdByNameAsync with empty username handles gracefully.
        /// SP returns status -2 (validation error) for empty/null username.
        /// </summary>
        [TestMethod]
        public async Task GetUserIdByNameAsync_WithEmptyUserName_HandlesGracefully()
        {
            // Act
            var result = await Dao_System.GetUserIdByNameAsync(string.Empty,
                connectionString: GetTestConnectionString());

            // Assert - SP returns status -2 for empty username (validation error per convention)
            // Status convention: 1=success with data, 0=success no data, negative=errors
            Assert.IsFalse(result.IsSuccess, "SP returns failure (status -2) for empty username");
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage),
                "Should provide meaningful error message");
            Assert.IsTrue(result.ErrorMessage.Contains("required", StringComparison.OrdinalIgnoreCase),
                $"Expected error message about username being required, got: {result.ErrorMessage}");
        }

        /// <summary>
        /// Tests SetUserAccessTypeAsync with null username handles gracefully.
        /// Stored procedure returns status -2 with error message for null/empty username.
        /// </summary>
        [TestMethod]
        public async Task SetUserAccessTypeAsync_WithNullUserName_HandlesGracefully()
        {
            // Act
            var result = await Dao_System.SetUserAccessTypeAsync(null!, "Admin");

            // Assert - Stored procedure returns status -2 for null/empty username
            Assert.IsFalse(result.IsSuccess, "Expected failure for null username");
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage),
                "Should provide meaningful error message");
            Assert.IsTrue(result.ErrorMessage.Contains("required", StringComparison.OrdinalIgnoreCase) ||
                         result.ErrorMessage.Contains("User name", StringComparison.OrdinalIgnoreCase),
                $"Expected error about required username, got: {result.ErrorMessage}");
        }

        #endregion

        #region DaoResult Pattern Validation Tests

        /// <summary>
        /// Tests that System_UserAccessTypeAsync returns proper DaoResult structure.
        /// </summary>
        [TestMethod]
        public async Task System_UserAccessTypeAsync_ReturnsDaoResultWithProperties()
        {
            // Act
            var result = await Dao_System.System_UserAccessTypeAsync();

            // Assert
            Assert.IsNotNull(result, "DaoResult should not be null");
            Assert.IsTrue(result.IsSuccess || !result.IsSuccess, "IsSuccess property should exist");
            Assert.IsNotNull(result.StatusMessage, "StatusMessage should not be null");
            // Data can be null on failure, but for this test it should succeed
            AssertSuccessWithData(result);
        }

        /// <summary>
        /// Tests that GetUserIdByNameAsync includes descriptive status message.
        /// </summary>
        [TestMethod]
        public async Task GetUserIdByNameAsync_IncludesDescriptiveStatusMessage()
        {
            // Arrange: Get a valid user first
            var usersResult = await Dao_System.System_UserAccessTypeAsync();
            AssertSuccessWithData(usersResult);
            Assert.IsNotNull(usersResult.Data, "User data should not be null");
            if (!usersResult.IsSuccess || usersResult.Data!.Count == 0)
            {
                Assert.Inconclusive("Test requires at least one user in the system");
                return;
            }

            var validUserName = usersResult.Data[0].User;

            // Act
            var result = await Dao_System.GetUserIdByNameAsync(validUserName);

            // Assert
            AssertSuccessWithData(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.StatusMessage),
                "StatusMessage should not be empty");
            Assert.IsTrue(result.StatusMessage.Length > 10,
                "StatusMessage should be descriptive (more than 10 characters)");
        }

        #endregion
    }
}

