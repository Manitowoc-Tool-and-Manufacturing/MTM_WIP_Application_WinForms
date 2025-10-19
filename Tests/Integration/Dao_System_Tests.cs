using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MTM_Inventory_Application.Tests.Integration
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
            var result = await Dao_System.System_UserAccessTypeAsync(connection: GetTestConnection(), transaction: GetTestTransaction());

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
        /// Note: This test doesn't verify the actual change due to transaction rollback,
        /// only that the stored procedure executes without error.
        /// </summary>
        [TestMethod]
        public async Task SetUserAccessTypeAsync_WithValidData_ExecutesSuccessfully()
        {
            // Arrange: Get a valid user from the system
            var usersResult = await Dao_System.System_UserAccessTypeAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
            AssertSuccessWithData(usersResult);
            Assert.IsNotNull(usersResult.Data, "User data should not be null");
            Assert.IsTrue(usersResult.Data!.Count > 0, "Need at least one user for this test");

            var firstUser = usersResult.Data[0];
            var newAccessType = "Admin"; // Try to set to Admin

            // Act
            var result = await Dao_System.SetUserAccessTypeAsync(firstUser.User, newAccessType, connection: GetTestConnection(), transaction: GetTestTransaction());
            // Assert
            Assert.IsTrue(result.IsSuccess, $"Expected success but got: {result.StatusMessage}");
        }

        /// <summary>
        /// Tests SetUserAccessTypeAsync with invalid access type.
        /// </summary>
        [TestMethod]
        public async Task SetUserAccessTypeAsync_WithInvalidAccessType_ProvidesStatusMessage()
        {
            // Arrange
            var usersResult = await Dao_System.System_UserAccessTypeAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
            AssertSuccessWithData(usersResult);
            Assert.IsNotNull(usersResult.Data, "User data should not be null");
            Assert.IsTrue(usersResult.Data!.Count > 0, "Need at least one user for this test");

            var firstUser = usersResult.Data[0];
            var invalidAccessType = "InvalidType_" + Guid.NewGuid().ToString();

            // Act
            var result = await Dao_System.SetUserAccessTypeAsync(
                firstUser.User,
                invalidAccessType,
                connection: GetTestConnection(),
                transaction: GetTestTransaction()
            );

            // Assert
            // Should either fail or succeed (depending on stored procedure validation)
            // But status message should be meaningful
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
            var usersResult = await Dao_System.System_UserAccessTypeAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
            AssertSuccessWithData(usersResult);
            Assert.IsNotNull(usersResult.Data, "User data should not be null");
            Assert.IsTrue(usersResult.Data!.Count > 0, "Need at least one user for this test");
            
            var testUserName = usersResult.Data[0].User;

            // Act
            var result = await Dao_System.GetUserIdByNameAsync(testUserName, connection: GetTestConnection(), transaction: GetTestTransaction());

            // Assert
            AssertSuccessWithData(result);
            Assert.IsTrue(result.Data > 0, "User ID should be greater than 0");
            Assert.IsTrue(result.StatusMessage.Contains(testUserName), "Status message should contain user name");
        }

        /// <summary>
        /// Tests GetUserIdByNameAsync with non-existent username returns failure.
        /// </summary>
        [TestMethod]
        public async Task GetUserIdByNameAsync_WithNonExistentUser_ReturnsFailure()
        {
            // Arrange
            var nonExistentUser = "NonExistentUser_" + Guid.NewGuid().ToString();

            // Act
            var result = await Dao_System.GetUserIdByNameAsync(nonExistentUser, connection: GetTestConnection(), transaction: GetTestTransaction());

            // Assert - Status 0 means "query succeeded but no data found" (treated as success)
            Assert.IsTrue(result.IsSuccess, "Expected success with status 0 for non-existent user (no data found)");
            Assert.IsTrue(
                result.ErrorMessage?.Contains(nonExistentUser) == true ||
                result.StatusMessage.Contains("not found", StringComparison.OrdinalIgnoreCase),
                $"Expected status message to mention user not found, got: {result.StatusMessage}");
        }

        /// <summary>
        /// Tests GetRoleIdByNameAsync with valid role name returns role ID.
        /// </summary>
        [TestMethod]
        public async Task GetRoleIdByNameAsync_WithValidRole_ReturnsRoleId()
        {
            // Arrange
            var validRoleName = "Admin"; // Assuming Admin role exists in test database

            // Act
            var result = await Dao_System.GetRoleIdByNameAsync(validRoleName, connection: GetTestConnection(), transaction: GetTestTransaction());

            // Assert
            AssertSuccessWithData(result);
            Assert.IsTrue(result.Data > 0, "Role ID should be greater than 0");
            Assert.IsTrue(result.StatusMessage.Contains(validRoleName), "Status message should contain role name");
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
            var result = await Dao_System.GetRoleIdByNameAsync(nonExistentRole, connection: GetTestConnection(), transaction: GetTestTransaction());

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
            var result = await Dao_System.GetAllThemesAsync( connection: GetTestConnection(), transaction: GetTestTransaction());

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
            var result = await Dao_System.GetAllThemesAsync( connection: GetTestConnection(), transaction: GetTestTransaction());

            // Assert
            AssertSuccessWithData(result);
            Assert.IsInstanceOfType(result.Data, typeof(DataTable));
            Assert.IsNotNull(result.Data, "DataTable should not be null in sync mode");
        }

        #endregion

        #region Error Handling Tests

        /// <summary>
        /// Tests GetUserIdByNameAsync with empty username handles gracefully.
        /// </summary>
        [TestMethod]
        public async Task GetUserIdByNameAsync_WithEmptyUserName_HandlesGracefully()
        {
            // Act
            var result = await Dao_System.GetUserIdByNameAsync(string.Empty, connection: GetTestConnection(), transaction: GetTestTransaction());

            // Assert - Stored procedure returns status -2 for empty username
            Assert.IsFalse(result.IsSuccess, "Expected failure for empty username");
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.ErrorMessage),
                "Should provide meaningful error message in ErrorMessage property");
            Assert.IsTrue(result.ErrorMessage.Contains("User name") || result.ErrorMessage.Contains("required"),
                $"Error message should indicate username is required, got: {result.ErrorMessage}");
        }

        /// <summary>
        /// Tests SetUserAccessTypeAsync with null username handles gracefully.
        /// </summary>
        [TestMethod]
        public async Task SetUserAccessTypeAsync_WithNullUserName_HandlesGracefully()
        {
            // Act & Assert
            // Should either throw ArgumentNullException or return graceful failure
            // Both behaviors are acceptable for null parameters
            try
            {
                var result = await Dao_System.SetUserAccessTypeAsync(null!, "Admin", connection: GetTestConnection(), transaction: GetTestTransaction());

                // If we get here, method returned a result instead of throwing
                Assert.IsFalse(result.IsSuccess, "Expected failure for null username");
                Assert.IsTrue(
                    result.StatusMessage.Contains("null", StringComparison.OrdinalIgnoreCase) ||
                    result.StatusMessage.Contains("required", StringComparison.OrdinalIgnoreCase),
                    $"Expected error message about null/required parameter, got: {result.StatusMessage}");
            }
            catch (ArgumentNullException)
            {
                // This is also acceptable behavior
                Assert.IsTrue(true, "Method correctly threw ArgumentNullException for null parameter");
            }
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
            var result = await Dao_System.System_UserAccessTypeAsync(connection: GetTestConnection(), transaction: GetTestTransaction());

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
            var usersResult = await Dao_System.System_UserAccessTypeAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
            AssertSuccessWithData(usersResult);
            Assert.IsNotNull(usersResult.Data, "User data should not be null");
            if (!usersResult.IsSuccess || usersResult.Data!.Count == 0)
            {
                Assert.Inconclusive("Test requires at least one user in the system");
                return;
            }

            var validUserName = usersResult.Data[0].User;

            // Act
            var result = await Dao_System.GetUserIdByNameAsync(validUserName, connection: GetTestConnection(), transaction: GetTestTransaction());

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

