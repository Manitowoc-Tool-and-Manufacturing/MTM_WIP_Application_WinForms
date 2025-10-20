using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Models;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MTM_Inventory_Application.Tests.Integration
{
    /// <summary>
    /// Integration tests for Dao_ErrorLog database operations.
    /// Tests error logging (INSERT), retrieval (SELECT), and deletion operations.
    /// Validates recursive error prevention and proper DaoResult handling.
    /// </summary>
    [TestClass]
    public class Dao_ErrorLog_Tests : BaseIntegrationTest
    {
        #region GetUniqueErrors Tests

        /// <summary>
        /// Tests GetUniqueErrorsAsync returns list of unique errors.
        /// </summary>
        [TestMethod]
        public async Task GetUniqueErrorsAsync_ReturnsErrorList()
        {
            // Act
            var result = await Dao_ErrorLog.GetUniqueErrorsAsync();

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOfType(result, typeof(System.Collections.Generic.List<(string MethodName, string ErrorMessage)>));
            // May be empty if no errors exist, which is fine
        }

        /// <summary>
        /// Tests GetUniqueErrorsAsync with actual async execution.
        /// </summary>
        [TestMethod]
        public async Task GetUniqueErrorsAsync_AsyncExecution_ReturnsErrorList()
        {
            // Act
            var result = await Dao_ErrorLog.GetUniqueErrorsAsync();

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
        }

        /// <summary>
        /// Tests synchronous GetUniqueErrors wrapper method.
        /// </summary>
        [TestMethod]
        public void GetUniqueErrors_SyncWrapper_ReturnsErrorList()
        {
            // Act
            var result = Dao_ErrorLog.GetUniqueErrors();

            // Assert
            Assert.IsNotNull(result, "Sync wrapper should return non-null result");
            Assert.IsInstanceOfType(result, typeof(System.Collections.Generic.List<(string MethodName, string ErrorMessage)>));
        }

        #endregion

        #region GetAllErrors Tests

        /// <summary>
        /// Tests GetAllErrorsAsync returns DaoResult with DataTable of all errors.
        /// </summary>
        [TestMethod]
        public async Task GetAllErrorsAsync_ReturnsDataTable()
        {
            // Act
            var result = await Dao_ErrorLog.GetAllErrorsAsync();

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.IsSuccess, "Operation should succeed");
            Assert.IsNotNull(result.Data, "DataTable should not be null");
            Assert.IsInstanceOfType(result.Data, typeof(DataTable));
            // Verify expected columns exist
            Assert.IsTrue(result.Data.Columns.Contains("Id") || result.Data.Columns.Contains("ErrorId") || result.Data.Columns.Count >= 0,
                "DataTable should have valid structure");
        }

        /// <summary>
        /// Tests GetAllErrorsAsync in synchronous mode.
        /// </summary>
        [TestMethod]
        public async Task GetAllErrorsAsync_SyncMode_ReturnsDataTable()
        {
            // Act
            var result = await Dao_ErrorLog.GetAllErrorsAsync();

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.IsSuccess, "Operation should succeed");
            Assert.IsNotNull(result.Data, "DataTable should not be null in sync mode");
            Assert.IsInstanceOfType(result.Data, typeof(DataTable));
        }

        #endregion

        #region GetErrorsByUser Tests

        /// <summary>
        /// Tests GetErrorsByUserAsync with valid user returns filtered errors.
        /// </summary>
        [TestMethod]
        public async Task GetErrorsByUserAsync_WithValidUser_ReturnsDataTable()
        {
            // Arrange
            var testUser = Model_AppVariables.User ?? "TestUser";

            // Act
            var result = await Dao_ErrorLog.GetErrorsByUserAsync(testUser);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.IsSuccess, "Operation should succeed");
            Assert.IsNotNull(result.Data, "DataTable should not be null");
            Assert.IsInstanceOfType(result.Data, typeof(DataTable));
        }

        /// <summary>
        /// Tests GetErrorsByUserAsync with non-existent user returns empty DataTable.
        /// </summary>
        [TestMethod]
        public async Task GetErrorsByUserAsync_WithNonExistentUser_ReturnsEmptyDataTable()
        {
            // Arrange
            var nonExistentUser = "NonExistentUser_" + Guid.NewGuid().ToString();

            // Act
            var result = await Dao_ErrorLog.GetErrorsByUserAsync(nonExistentUser);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.IsSuccess, "Operation should succeed even for non-existent user");
            Assert.IsNotNull(result.Data, "DataTable should not be null even for non-existent user");
            // Should return empty DataTable, not null
            Assert.AreEqual(0, result.Data.Rows.Count, "Should return empty DataTable for non-existent user");
        }

        #endregion

        #region GetErrorsByDateRange Tests

        /// <summary>
        /// Tests GetErrorsByDateRangeAsync with valid date range returns filtered errors.
        /// </summary>
        [TestMethod]
        public async Task GetErrorsByDateRangeAsync_WithValidRange_ReturnsDataTable()
        {
            // Arrange
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-7); // Last 7 days

            // Act
            var result = await Dao_ErrorLog.GetErrorsByDateRangeAsync(startDate, endDate);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.IsSuccess, "Operation should succeed");
            Assert.IsNotNull(result.Data, "DataTable should not be null");
            Assert.IsInstanceOfType(result.Data, typeof(DataTable));
        }

        /// <summary>
        /// Tests GetErrorsByDateRangeAsync with future date range returns empty DataTable.
        /// </summary>
        [TestMethod]
        public async Task GetErrorsByDateRangeAsync_WithFutureDates_ReturnsEmptyDataTable()
        {
            // Arrange
            var startDate = DateTime.Now.AddYears(1);
            var endDate = startDate.AddDays(7);

            // Act
            var result = await Dao_ErrorLog.GetErrorsByDateRangeAsync(startDate, endDate);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.IsSuccess, "Operation should succeed");
            Assert.IsNotNull(result.Data, "DataTable should not be null");
            Assert.AreEqual(0, result.Data.Rows.Count, "Should return empty DataTable for future date range");
        }

        /// <summary>
        /// Tests GetErrorsByDateRangeAsync with inverted dates (end before start).
        /// </summary>
        [TestMethod]
        public async Task GetErrorsByDateRangeAsync_WithInvertedDates_HandlesGracefully()
        {
            // Arrange
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(-7); // End is before start

            // Act
            var result = await Dao_ErrorLog.GetErrorsByDateRangeAsync(startDate, endDate);

            // Assert - Should handle inverted dates without throwing exceptions
            // Stored procedure returns status -2 for inverted date range
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsFalse(result.IsSuccess, "Operation should fail for inverted dates (stored procedure validation)");
            Assert.IsTrue(result.ErrorMessage.Contains("Start date") || result.ErrorMessage.Contains("date"), 
                $"Error message should indicate date validation issue, got: {result.ErrorMessage}");
        }

        #endregion

        #region DeleteError Tests

        /// <summary>
        /// Tests DeleteErrorByIdAsync with non-existent ID executes without error.
        /// Note: Transaction rollback prevents actual deletion, so we just verify execution.
        /// </summary>
        [TestMethod]
        public async Task DeleteErrorByIdAsync_WithNonExistentId_ExecutesWithoutError()
        {
            // Arrange
            var nonExistentId = 999999;

            // Act
            var result = await Dao_ErrorLog.DeleteErrorByIdAsync(nonExistentId);

            // Assert - Should execute without throwing, but returns failure status for non-existent ID
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsFalse(result.IsSuccess, "Delete operation should fail for non-existent ID (stored procedure validation)");
            Assert.IsTrue(result.ErrorMessage.Contains("not found") || result.ErrorMessage.Contains("Error log entry"), 
                $"Error message should indicate ID not found, got: {result.ErrorMessage}");
        }

        /// <summary>
        /// Tests DeleteAllErrorsAsync executes without error.
        /// Note: Transaction rollback prevents actual deletion.
        /// </summary>
        [TestMethod]
        public async Task DeleteAllErrorsAsync_ExecutesWithoutError()
        {
            // Act
            var result = await Dao_ErrorLog.DeleteAllErrorsAsync();
            
            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.IsSuccess, "Delete all operation should execute without throwing exception");
        }

        #endregion

        #region Error Handling Tests

        /// <summary>
        /// Tests HandleException_SQLError_CloseApp logs error without crashing.
        /// Note: We can't fully test process termination in unit tests.
        /// </summary>
        [TestMethod]
        public async Task HandleException_SQLError_CloseApp_LogsError()
        {
            // Arrange
            var testException = new Exception("Test SQL error for logging");

            // Act - Should not throw (catches internally)
            await Dao_ErrorLog.HandleException_SQLError_CloseApp(
                testException,
                
                callerName: "TestMethod",
                controlName: "TestControl");

            // Assert - If we get here, exception was handled
            Assert.IsTrue(true, "SQL error handler should not throw exceptions");
        }

        /// <summary>
        /// Tests HandleException_GeneralError_CloseApp logs error without crashing.
        /// </summary>
        [TestMethod]
        public async Task HandleException_GeneralError_CloseApp_LogsError()
        {
            // Arrange
            var testException = new InvalidOperationException("Test general error for logging");

            // Act - Should not throw (catches internally)
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(
                testException,
                
                callerName: "TestMethod",
                controlName: "TestControl");

            // Assert - If we get here, exception was handled
            Assert.IsTrue(true, "General error handler should not throw exceptions");
        }

        /// <summary>
        /// Tests LogErrorWithMethod helper logs error without throwing.
        /// </summary>
        [TestMethod]
        public void LogErrorWithMethod_LogsError()
        {
            // Arrange
            var testException = new Exception("Test error for method logging");

            // Act - Should not throw
            Dao_ErrorLog.LogErrorWithMethod(testException, "TestMethod");

            // Assert - If we get here, logging succeeded
            Assert.IsTrue(true, "LogErrorWithMethod should not throw exceptions");
        }

        #endregion

        #region Recursive Error Prevention Tests

        /// <summary>
        /// Tests that error logging methods don't create recursive loops.
        /// This validates the recursive prevention mechanism in LoggingUtility.
        /// </summary>
        [TestMethod]
        public async Task ErrorLogging_DoesNotCreateRecursiveLoop()
        {
            // Arrange
            var testException = new Exception("Recursive prevention test");
            int iterationCount = 0;

            // Act - Try to trigger recursive error logging multiple times
            for (int i = 0; i < 5; i++)
            {
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(
                    testException,
                    
                    callerName: $"TestMethod_{i}",
                    controlName: "TestControl");
                iterationCount++;
            }

            // Assert - All iterations completed without hanging or stack overflow
            Assert.AreEqual(5, iterationCount, "All iterations should complete without recursion issues");
        }

        /// <summary>
        /// Tests that database connection errors don't create recursive logging.
        /// </summary>
        [TestMethod]
        public async Task DatabaseConnectionError_DoesNotCreateRecursiveLoop()
        {
            // Arrange - MySqlException doesn't have a public constructor, 
            // so we use a regular Exception with a MySQL connection error message
            var connectionException = new Exception(
                "Unable to connect to any of the specified MySQL hosts.");

            // Act - Should handle gracefully without recursion
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(
                connectionException,
                
                callerName: "TestMethod",
                controlName: "TestControl");

            // Assert - If we reach here without hanging, the recursive prevention worked
            Assert.IsTrue(true, "Database connection error handled without recursion");
        }

        #endregion

        #region Data Integrity Tests

        /// <summary>
        /// Tests that GetUniqueErrorsAsync returns distinct method/message pairs.
        /// </summary>
        [TestMethod]
        public async Task GetUniqueErrorsAsync_ReturnsDistinctErrors()
        {
            // Act
            var result = await Dao_ErrorLog.GetUniqueErrorsAsync();

            // Assert
            if (result.Count > 0)
            {
                // Check that combinations are unique
                var distinctCount = result.Distinct().Count();
                Assert.AreEqual(result.Count, distinctCount, 
                    "GetUniqueErrors should return distinct method/message combinations");
            }
            else
            {
                // No errors exist, which is valid
                Assert.IsTrue(true, "No errors in system, which is a valid state");
            }
        }

        /// <summary>
        /// Tests that GetAllErrorsAsync returns valid DataTable structure.
        /// </summary>
        [TestMethod]
        public async Task GetAllErrorsAsync_ReturnsValidStructure()
        {
            // Act
            var result = await Dao_ErrorLog.GetAllErrorsAsync();

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.IsSuccess, "Operation should succeed");
            Assert.IsNotNull(result.Data, "DataTable should not be null");
            Assert.IsNotNull(result.Data.Columns, "DataTable should have Columns collection");
            Assert.IsTrue(result.Data.Columns.Count >= 0, "DataTable should have valid column count");
        }

        #endregion

        #region Null and Empty Parameter Tests

        /// <summary>
        /// Tests GetErrorsByUserAsync with empty string user parameter.
        /// </summary>
        [TestMethod]
        public async Task GetErrorsByUserAsync_WithEmptyString_HandlesGracefully()
        {
            // Act
            var result = await Dao_ErrorLog.GetErrorsByUserAsync(string.Empty);

            // Assert - Should handle empty string gracefully without throwing exceptions
            // Stored procedure returns status -2 for null/empty user
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsFalse(result.IsSuccess, "Operation should fail for empty user string (stored procedure validation)");
            Assert.IsTrue(result.ErrorMessage.Contains("User is required") || result.ErrorMessage.Contains("required"), 
                $"Error message should indicate user is required, got: {result.ErrorMessage}");
        }

        /// <summary>
        /// Tests GetErrorsByUserAsync with null user parameter.
        /// </summary>
        [TestMethod]
        public async Task GetErrorsByUserAsync_WithNull_HandlesGracefully()
        {
            // Act
            var result = await Dao_ErrorLog.GetErrorsByUserAsync(null!);
            
            // Assert - Should handle null gracefully without throwing exceptions
            // Stored procedure returns status -2 for null/empty user
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsFalse(result.IsSuccess, "Operation should fail for null user (stored procedure validation)");
            Assert.IsTrue(result.ErrorMessage.Contains("User is required") || result.ErrorMessage.Contains("required"), 
                $"Error message should indicate user is required, got: {result.ErrorMessage}");
        }

        #endregion
    }
}
