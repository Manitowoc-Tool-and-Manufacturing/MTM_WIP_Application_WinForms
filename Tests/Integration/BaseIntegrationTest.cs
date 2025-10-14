using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MTM_Inventory_Application.Tests.Integration;

/// <summary>
/// Base class for integration tests that provides transaction isolation.
/// Each test runs within a transaction that is rolled back after completion,
/// ensuring tests don't affect the database or each other.
/// </summary>
/// <remarks>
/// <para>
/// <strong>Test Database Setup:</strong>
/// Integration tests use the <c>mtm_wip_application_winforms_test</c> database (see <see cref="Helper_Database_Variables.TestDatabaseName"/>).
/// This database must be created and schema-synchronized before running tests.
/// </para>
/// <para>
/// <strong>Transaction Isolation:</strong>
/// Each test method executes within a database transaction that is automatically
/// rolled back in <see cref="TestCleanup"/>, preventing side effects between tests.
/// The connection remains open for the duration of the test to maintain transaction scope.
/// </para>
/// <para>
/// <strong>Usage Pattern:</strong>
/// <code>
/// [TestClass]
/// public class Dao_System_Tests : BaseIntegrationTest
/// {
///     [TestMethod]
///     public async Task GetDatabaseVersionAsync_ReturnsVersion()
///     {
///         // Arrange
///         var connectionString = GetTestConnectionString();
///         
///         // Act
///         var result = await Dao_System.GetDatabaseVersionAsync();
///         
///         // Assert
///         Assert.IsTrue(result.IsSuccess);
///         Assert.IsNotNull(result.Data);
///         // Transaction automatically rolled back after test
///     }
/// }
/// </code>
/// </para>
/// </remarks>
public abstract class BaseIntegrationTest
{
    #region Fields

    /// <summary>
    /// Database connection for the current test.
    /// Opened in <see cref="TestInitialize"/> and closed in <see cref="TestCleanup"/>.
    /// </summary>
    private MySqlConnection? _connection;

    /// <summary>
    /// Transaction wrapping the current test.
    /// Begun in <see cref="TestInitialize"/> and rolled back in <see cref="TestCleanup"/>.
    /// </summary>
    private MySqlTransaction? _transaction;

    #endregion

    #region Test Lifecycle

    /// <summary>
    /// Initializes the test by opening a database connection and starting a transaction.
    /// Called automatically before each test method.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the connection cannot be opened or transaction cannot be started.
    /// </exception>
    [TestInitialize]
    public void TestInitialize()
    {
        try
        {
            // Get connection string for test database
            var connectionString = GetTestConnectionString();

            // Open connection
            _connection = new MySqlConnection(connectionString);
            _connection.Open();

            // Start transaction for test isolation
            _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);

            // Log test initialization
            Console.WriteLine($"[Test Setup] Connection opened and transaction started for test database: {Helper_Database_Variables.TestDatabaseName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Test Setup ERROR] Failed to initialize test: {ex.Message}");
            throw new InvalidOperationException("Failed to initialize integration test. Ensure test database exists and is accessible.", ex);
        }
    }

    /// <summary>
    /// Cleans up the test by rolling back the transaction and closing the connection.
    /// Called automatically after each test method, regardless of test outcome.
    /// </summary>
    /// <remarks>
    /// The rollback ensures that no test data persists in the database, maintaining
    /// test isolation and repeatability.
    /// </remarks>
    [TestCleanup]
    public void TestCleanup()
    {
        try
        {
            // Rollback transaction to undo any changes made during test
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
                Console.WriteLine("[Test Cleanup] Transaction rolled back successfully");
            }

            // Close and dispose connection
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
                _connection.Dispose();
                _connection = null;
                Console.WriteLine("[Test Cleanup] Connection closed successfully");
            }
        }
        catch (Exception ex)
        {
            // Log cleanup errors but don't throw - avoid masking test failures
            Console.WriteLine($"[Test Cleanup ERROR] Failed to cleanup test: {ex.Message}");
        }
    }

    #endregion

    #region Protected Helper Methods

    /// <summary>
    /// Gets the connection string for the test database.
    /// </summary>
    /// <returns>Connection string configured for <c>mtm_wip_application_winforms_test</c> database.</returns>
    /// <remarks>
    /// Uses <see cref="Helper_Database_Variables.GetConnectionString"/> with explicit
    /// database name override to target the test database instead of production.
    /// </remarks>
    protected static string GetTestConnectionString()
    {
        return Helper_Database_Variables.GetConnectionString(
            server: null,  // Use default from Model_Users
            database: Helper_Database_Variables.TestDatabaseName,
            uid: null,     // Use default "root"
            password: null // Use default "root"
        );
    }

    /// <summary>
    /// Gets the active test connection (opened in <see cref="TestInitialize"/>).
    /// </summary>
    /// <returns>The MySqlConnection for the current test, or null if not initialized.</returns>
    /// <remarks>
    /// <para>
    /// This connection is managed by the base class lifecycle. Tests should not close
    /// or dispose this connection directly.
    /// </para>
    /// <para>
    /// <strong>Use Case:</strong> When a test needs direct database access outside of
    /// DAO methods (e.g., setup test data, verify side effects).
    /// </para>
    /// </remarks>
    protected MySqlConnection? GetTestConnection()
    {
        return _connection;
    }

    /// <summary>
    /// Gets the active test transaction (started in <see cref="TestInitialize"/>).
    /// </summary>
    /// <returns>The MySqlTransaction for the current test, or null if not initialized.</returns>
    /// <remarks>
    /// <para>
    /// This transaction is managed by the base class lifecycle. Tests should not commit
    /// or rollback this transaction directly - it will be automatically rolled back in
    /// <see cref="TestCleanup"/>.
    /// </para>
    /// <para>
    /// <strong>Use Case:</strong> When a DAO method requires an explicit transaction parameter
    /// (e.g., multi-step operations like inventory transfers).
    /// </para>
    /// </remarks>
    protected MySqlTransaction? GetTestTransaction()
    {
        return _transaction;
    }

    #endregion

    #region Assertion Helpers

    /// <summary>
    /// Asserts that a DaoResult operation succeeded.
    /// </summary>
    /// <param name="result">The DaoResult to validate.</param>
    /// <param name="message">Optional custom assertion message.</param>
    /// <exception cref="AssertFailedException">Thrown if result.IsSuccess is false.</exception>
    /// <remarks>
    /// Provides detailed failure information including the result's error message
    /// and exception details (if present).
    /// </remarks>
    protected static void AssertSuccess(DaoResult result, string? message = null)
    {
        var failureDetails = result.IsSuccess
            ? string.Empty
            : $"\nReason: {result.ErrorMessage}" +
              (result.Exception != null ? $"\nException: {result.Exception.Message}" : string.Empty);

        Assert.IsTrue(
            result.IsSuccess,
            message ?? $"Expected DaoResult.IsSuccess to be true, but was false.{failureDetails}");
    }

    /// <summary>
    /// Asserts that a DaoResult&lt;T&gt; operation succeeded and returned data.
    /// </summary>
    /// <typeparam name="T">The data type returned by the DaoResult.</typeparam>
    /// <param name="result">The DaoResult&lt;T&gt; to validate.</param>
    /// <param name="message">Optional custom assertion message.</param>
    /// <exception cref="AssertFailedException">
    /// Thrown if result.IsSuccess is false or result.Data is null.
    /// </exception>
    /// <remarks>
    /// Validates both success status and data presence. For operations that may
    /// legitimately return null data on success, use <see cref="AssertSuccess"/> instead.
    /// </remarks>
    protected static void AssertSuccessWithData<T>(DaoResult<T> result, string? message = null)
    {
        var failureDetails = result.IsSuccess
            ? string.Empty
            : $"\nReason: {result.ErrorMessage}" +
              (result.Exception != null ? $"\nException: {result.Exception.Message}" : string.Empty);

        Assert.IsTrue(
            result.IsSuccess,
            message ?? $"Expected DaoResult.IsSuccess to be true, but was false.{failureDetails}");

        Assert.IsNotNull(
            result.Data,
            message ?? $"Expected DaoResult<{typeof(T).Name}>.Data to be non-null after successful operation.");
    }

    /// <summary>
    /// Asserts that a DaoResult operation failed.
    /// </summary>
    /// <param name="result">The DaoResult to validate.</param>
    /// <param name="message">Optional custom assertion message.</param>
    /// <exception cref="AssertFailedException">Thrown if result.IsSuccess is true.</exception>
    /// <remarks>
    /// Use this to test error handling paths and validation logic.
    /// </remarks>
    protected static void AssertFailure(DaoResult result, string? message = null)
    {
        Assert.IsFalse(
            result.IsSuccess,
            message ?? "Expected DaoResult.IsSuccess to be false, but was true.");

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(result.ErrorMessage),
            "Expected DaoResult.ErrorMessage to contain error details, but was null or empty.");
    }

    /// <summary>
    /// Asserts that a DaoResult operation failed with a specific error message substring.
    /// </summary>
    /// <param name="result">The DaoResult to validate.</param>
    /// <param name="expectedMessageSubstring">
    /// A substring that should appear in the error message (case-insensitive).
    /// </param>
    /// <param name="message">Optional custom assertion message.</param>
    /// <exception cref="AssertFailedException">
    /// Thrown if result succeeded or error message doesn't contain expected substring.
    /// </exception>
    protected static void AssertFailureWithMessage(
        DaoResult result,
        string expectedMessageSubstring,
        string? message = null)
    {
        AssertFailure(result, message);

        Assert.IsTrue(
            result.ErrorMessage.Contains(expectedMessageSubstring, StringComparison.OrdinalIgnoreCase),
            message ?? $"Expected error message to contain '{expectedMessageSubstring}', but got: {result.ErrorMessage}");
    }

    #endregion
}
