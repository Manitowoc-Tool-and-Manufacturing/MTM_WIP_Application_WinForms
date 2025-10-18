using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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

    private readonly object _diagnosticSyncRoot = new();

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

    private ProcedureDiagnosticContext? _diagnosticContext;

    private static readonly IReadOnlyDictionary<string, object?> EmptyParameters = new Dictionary<string, object?>();

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
    /// Gets the MSTest context for the current test execution.
    /// Populated automatically by MSTest before each test runs.
    /// </summary>
    public TestContext? TestContext { get; set; }

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

    /// <summary>
    /// Captures row counts for selected tables in the test database.
    /// </summary>
    /// <param name="tables">Optional list of table names to inspect.</param>
    /// <returns>Dictionary of table name to row count.</returns>
    /// <remarks>
    /// Executes counts within the current test transaction when available so diagnostics reflect uncommitted work.
    /// </remarks>
    protected IReadOnlyDictionary<string, int> CaptureTableRowCounts(params string[] tables)
    {
        if (tables == null || tables.Length == 0)
        {
            return new Dictionary<string, int>();
        }

        var distinctTables = new HashSet<string>(tables.Where(t => !string.IsNullOrWhiteSpace(t)), StringComparer.OrdinalIgnoreCase);
        if (distinctTables.Count == 0)
        {
            return new Dictionary<string, int>();
        }

        var results = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        MySqlConnection? externalConnection = null;

        try
        {
            var connection = _connection;
            if (connection is null)
            {
                externalConnection = new MySqlConnection(GetTestConnectionString());
                externalConnection.Open();
                connection = externalConnection;
            }

            foreach (var table in distinctTables)
            {
                using var command = connection.CreateCommand();
                command.CommandText = $"SELECT COUNT(*) FROM `{table}`";
                if (_transaction != null && ReferenceEquals(connection, _connection))
                {
                    command.Transaction = _transaction;
                }

                try
                {
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    results[table] = count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Diagnostics] Failed to capture row count for {table}: {ex.Message}");
                }
            }
        }
        finally
        {
            externalConnection?.Dispose();
        }

        return results;
    }

    #endregion

    #region Assertion Helpers

    /// <summary>
    /// Provides the most recent procedure parameter snapshot recorded for diagnostics.
    /// </summary>
    protected IReadOnlyDictionary<string, object?> CurrentProcedureParameters
    {
        get
        {
            lock (_diagnosticSyncRoot)
            {
                return _diagnosticContext?.Parameters ?? EmptyParameters;
            }
        }
    }

    /// <summary>
    /// Exposes the last measured execution duration for diagnostics (milliseconds).
    /// </summary>
    protected long? CurrentExecutionTimeMs
    {
        get
        {
            lock (_diagnosticSyncRoot)
            {
                return _diagnosticContext?.ElapsedMilliseconds;
            }
        }
    }

    /// <summary>
    /// Executes a stored procedure against the test database with automatic diagnostic capture.
    /// </summary>
    protected async Task<DaoResult<DataTable>> ExecuteTestProcedureAsync(
        string procedureName,
        Dictionary<string, object> parameters,
        params string[] tablesToSnapshot)
    {
        if (string.IsNullOrWhiteSpace(procedureName))
        {
            throw new ArgumentException("Procedure name is required", nameof(procedureName));
        }

        if (parameters is null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        using var diagnosticScope = BeginProcedureDiagnostics(
            procedureName,
            parameters,
            tablesToSnapshot ?? Array.Empty<string>());

        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            GetTestConnectionString(),
            procedureName,
            parameters);
    }

    /// <summary>
    /// Begins a diagnostic scope for manual DAO invocations.
    /// </summary>
    protected ProcedureDiagnosticScope BeginProcedureDiagnostics(
        string procedureName,
        IReadOnlyDictionary<string, object>? parameters = null,
        params string[] tablesToSnapshot)
    {
        if (string.IsNullOrWhiteSpace(procedureName))
        {
            throw new ArgumentException("Procedure name is required", nameof(procedureName));
        }

        var context = new ProcedureDiagnosticContext(
            procedureName,
            parameters,
            tablesToSnapshot ?? Array.Empty<string>());

        lock (_diagnosticSyncRoot)
        {
            _diagnosticContext = context;
        }

    return ProcedureDiagnosticScope.Create(this, context);
    }

    /// <summary>
    /// Updates the current diagnostic parameter snapshot. Useful when DAO helpers add defaults.
    /// </summary>
    protected void UpdateProcedureDiagnostics(IReadOnlyDictionary<string, object>? parameters)
    {
        if (parameters is null)
        {
            return;
        }

        lock (_diagnosticSyncRoot)
        {
            _diagnosticContext?.UpdateParameters(parameters);
        }
    }

    /// <summary>
    /// Clears any active diagnostic context.
    /// </summary>
    protected void ResetProcedureDiagnostics()
    {
        lock (_diagnosticSyncRoot)
        {
            _diagnosticContext = null;
        }
    }

    /// <summary>
    /// Asserts procedure results and emits verbose diagnostics if expectations are not met.
    /// </summary>
    protected void AssertProcedureResult(
        DaoResult result,
        bool expectedSuccess,
        string? expectedMessageSubstring = null,
        params string[] tablesToSnapshot)
    {
        if (result is null)
        {
            Assert.Fail("DaoResult was null; cannot validate outcome.");
            return;
        }

        AssertProcedureResultCore(
            result.IsSuccess,
            result.ErrorMessage,
            result.Exception,
            expectedSuccess,
            expectedMessageSubstring,
            tablesToSnapshot);
    }

    /// <summary>
    /// Asserts generic procedure results with verbose diagnostics.
    /// </summary>
    protected void AssertProcedureResult<T>(
        DaoResult<T> result,
        bool expectedSuccess,
        string? expectedMessageSubstring = null,
        params string[] tablesToSnapshot)
    {
        if (result is null)
        {
            Assert.Fail("DaoResult<T> was null; cannot validate outcome.");
            return;
        }

        AssertProcedureResultCore(
            result.IsSuccess,
            result.ErrorMessage,
            result.Exception,
            expectedSuccess,
            expectedMessageSubstring,
            tablesToSnapshot);
    }

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

    #region Diagnostics

    private void AssertProcedureResultCore(
        bool actualSuccess,
        string errorMessage,
        Exception? exception,
        bool expectedSuccess,
        string? expectedMessageSubstring,
        params string[] tablesToSnapshot)
    {
        var context = GetCurrentDiagnostics();

        if (context != null)
        {
            CompleteDiagnostics(context);

            if (tablesToSnapshot != null && tablesToSnapshot.Length > 0)
            {
                var counts = CaptureTableRowCounts(tablesToSnapshot);
                context.SetRowCounts(counts);
            }
        }

        var messageMatches = expectedMessageSubstring is null ||
            (!string.IsNullOrWhiteSpace(errorMessage) &&
             errorMessage.Contains(expectedMessageSubstring, StringComparison.OrdinalIgnoreCase));

        if (actualSuccess != expectedSuccess || (expectedMessageSubstring != null && !messageMatches))
        {
            var diagnosticPayload = new
            {
                Procedure = context?.ProcedureName,
                Expected = new { IsSuccess = expectedSuccess, MessageContains = expectedMessageSubstring },
                Actual = new { IsSuccess = actualSuccess, Message = errorMessage },
                Parameters = context?.Parameters,
                ExecutionTimeMs = context?.ElapsedMilliseconds,
                DatabaseState = context?.RowCounts,
                Exception = exception?.ToString(),
                TestMethod = TestContext?.TestName,
                TimestampUtc = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(diagnosticPayload, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Assert.Fail($"Procedure result assertion failed.{Environment.NewLine}{json}");
        }

        if (expectedMessageSubstring != null && !messageMatches)
        {
            Assert.Fail($"Expected error message to contain '{expectedMessageSubstring}', but got: {errorMessage}");
        }
    }

    private ProcedureDiagnosticContext? GetCurrentDiagnostics()
    {
        lock (_diagnosticSyncRoot)
        {
            return _diagnosticContext;
        }
    }

    private void CompleteDiagnostics(ProcedureDiagnosticContext context)
    {
        if (!context.Stopwatch.IsRunning)
        {
            return;
        }

        context.Stopwatch.Stop();
        context.Complete(context.Stopwatch.ElapsedMilliseconds);

        if (context.TablesToSnapshot.Length > 0 && context.RowCounts.Count == 0)
        {
            var counts = CaptureTableRowCounts(context.TablesToSnapshot);
            context.SetRowCounts(counts);
        }
    }

    protected sealed class ProcedureDiagnosticScope : IDisposable
    {
        private readonly BaseIntegrationTest _owner;
        private readonly ProcedureDiagnosticContext _context;
        private bool _disposed;

        private ProcedureDiagnosticScope(BaseIntegrationTest owner, ProcedureDiagnosticContext context)
        {
            _owner = owner;
            _context = context;
        }

        internal static ProcedureDiagnosticScope Create(BaseIntegrationTest owner, ProcedureDiagnosticContext context)
        {
            return new ProcedureDiagnosticScope(owner, context);
        }

        public void UpdateParameters(IReadOnlyDictionary<string, object>? parameters)
        {
            _owner.UpdateProcedureDiagnostics(parameters);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _owner.CompleteDiagnostics(_context);
        }
    }

    internal sealed class ProcedureDiagnosticContext
    {
        internal ProcedureDiagnosticContext(
            string procedureName,
            IReadOnlyDictionary<string, object>? parameters,
            string[] tablesToSnapshot)
        {
            ProcedureName = procedureName;
            Parameters = parameters?.ToDictionary(kvp => kvp.Key, kvp => (object?)kvp.Value, StringComparer.OrdinalIgnoreCase)
                ?? new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            TablesToSnapshot = tablesToSnapshot?.Distinct(StringComparer.OrdinalIgnoreCase).ToArray()
                ?? Array.Empty<string>();
            Stopwatch = Stopwatch.StartNew();
            RowCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

        internal string ProcedureName { get; }

        internal Dictionary<string, object?> Parameters { get; private set; }

        internal string[] TablesToSnapshot { get; }

        internal Stopwatch Stopwatch { get; }

        internal long? ElapsedMilliseconds { get; private set; }

        internal Dictionary<string, int> RowCounts { get; private set; }

        internal void UpdateParameters(IReadOnlyDictionary<string, object>? parameters)
        {
            Parameters = parameters?.ToDictionary(kvp => kvp.Key, kvp => (object?)kvp.Value, StringComparer.OrdinalIgnoreCase)
                ?? new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        }

        internal void Complete(long elapsedMilliseconds)
        {
            ElapsedMilliseconds = elapsedMilliseconds;
        }

        internal void SetRowCounts(IReadOnlyDictionary<string, int> counts)
        {
            RowCounts = counts?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value, StringComparer.OrdinalIgnoreCase)
                ?? new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }
    }

    #endregion
}
