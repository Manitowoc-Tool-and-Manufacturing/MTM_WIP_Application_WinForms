using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
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
/// Base class for integration tests that provides test database connection management and cleanup.
/// Each test runs with a dedicated connection and test data is cleaned up after completion.
/// </summary>
/// <remarks>
/// <para>
/// <strong>Test Database Setup:</strong>
/// Integration tests use the <c>mtm_wip_application_winforms_test</c> database (see <see cref="Helper_Database_Variables.TestDatabaseName"/>).
/// This database must be created and schema-synchronized before running tests.
/// </para>
/// <para>
/// <strong>Test Isolation Strategy:</strong>
/// Tests use explicit cleanup instead of transaction rollback. Each test method commits its changes
/// to the database, and <see cref="TestCleanup"/> removes test data by pattern matching (TEST-*, TEMP-*, etc.).
/// </para>
/// <para>
/// <strong>Why No Transaction Isolation?:</strong>
/// MySQL.Data connector cannot retrieve OUTPUT parameters (p_Status, p_ErrorMsg) from stored procedures
/// when using external transactions. This was blocking ~30 tests. See BLOCKER-ANALYSIS.md for details.
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
///         // Act - connection parameter optional, transaction always null
///         var result = await Dao_System.GetDatabaseVersionAsync(
///             connection: GetTestConnection());
///         
///         // Assert
///         Assert.IsTrue(result.IsSuccess);
///         Assert.IsNotNull(result.Data);
///         // Test data automatically cleaned up after test
///     }
/// }
/// </code>
/// </para>
/// <para>
/// <strong>Test Data Naming Convention:</strong>
/// Use identifiable prefixes for test data to ensure proper cleanup:
/// - BatchNumber: TEST-*, TEMP-*, WORKFLOW-*
/// - User/UserName: Test*, TestUser*
/// - PartID: TEST-PART-*
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
    /// Transaction field - DEPRECATED and always null.
    /// Transaction isolation removed due to MySQL.Data connector limitations.
    /// See BLOCKER-ANALYSIS.md for details.
    /// </summary>
    [Obsolete("Transaction isolation removed - field kept for backward compatibility only")]
    private MySqlTransaction? _transaction;

    private ProcedureDiagnosticContext? _diagnosticContext;

    private static readonly IReadOnlyDictionary<string, object?> EmptyParameters = new Dictionary<string, object?>();

    #endregion

    #region Test Lifecycle

    /// <summary>
    /// Initializes the test by opening a database connection.
    /// Called automatically before each test method.
    /// </summary>
    /// <remarks>
    /// NOTE: Transaction-based isolation has been removed due to MySQL.Data connector limitations
    /// with OUTPUT parameters. Tests now commit data and clean up explicitly in TestCleanup.
    /// See BLOCKER-ANALYSIS.md for details.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the connection cannot be opened.
    /// </exception>
    [TestInitialize]
    public void TestInitialize()
    {
        try
        {
            // Enable test mode to suppress MessageBox dialogs
            Dao_ErrorLog.IsTestMode = true;

            // Get connection string for test database
            var connectionString = GetTestConnectionString();

            // Open connection (no transaction - see remarks above)
            _connection = new MySqlConnection(connectionString);
            _connection.Open();

            // Log test initialization
            Console.WriteLine($"[Test Setup] Connection opened for test database: {Helper_Database_Variables.TestDatabaseName}");
            Console.WriteLine($"[Test Setup] Using explicit cleanup strategy (no transaction isolation)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Test Setup ERROR] Failed to initialize test: {ex.Message}");
            throw new InvalidOperationException("Failed to initialize integration test. Ensure test database exists and is accessible.", ex);
        }
    }

    /// <summary>
    /// Cleans up the test by removing test data and closing the connection.
    /// Called automatically after each test method, regardless of test outcome.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <strong>Cleanup Strategy:</strong>
    /// Instead of transaction rollback, this method explicitly deletes test data by pattern matching.
    /// Test data should use identifiable prefixes (TEST-, TEMP-, TestUser) for reliable cleanup.
    /// </para>
    /// <para>
    /// <strong>Why No Transaction?:</strong>
    /// MySQL.Data connector cannot retrieve OUTPUT parameters (p_Status, p_ErrorMsg) from stored
    /// procedures when using external transactions. See BLOCKER-ANALYSIS.md for technical details.
    /// </para>
    /// </remarks>
    [TestCleanup]
    public void TestCleanup()
    {
        try
        {
            // Clean up test data before closing connection
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                CleanupTestData();
            }

            // Note: _transaction is now always null (removed transaction isolation)
            // Keeping this check for backward compatibility
#pragma warning disable CS0618 // Type or member is obsolete
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
#pragma warning restore CS0618 // Type or member is obsolete

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

    /// <summary>
    /// Cleans up test data from the database by pattern matching.
    /// Called automatically in <see cref="TestCleanup"/> to remove data created during tests.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <strong>Cleanup Patterns:</strong>
    /// Deletes records where identifiers match test patterns:
    /// - BatchNumber: TEST-*, TEMP-*, WORKFLOW-*
    /// - User/UserName: Test*, TestUser*
    /// - PartID: TEST-PART-*
    /// </para>
    /// <para>
    /// <strong>Tables Cleaned:</strong>
    /// - inv_inventory (test inventory records)
    /// - inv_transaction (test transaction history)
    /// - sys_last_10_transactions (test quick button configurations)
    /// - log_error (test error logs)
    /// </para>
    /// <para>
    /// <strong>Safety:</strong>
    /// Only affects test database (mtm_wip_application_winforms_test).
    /// Production database uses different connection string.
    /// </para>
    /// </remarks>
    private void CleanupTestData()
    {
        if (_connection == null || _connection.State != ConnectionState.Open)
        {
            Console.WriteLine("[Cleanup] Skipping cleanup - connection not available");
            return;
        }

        try
        {
            // Clean up test inventory records
            try
            {
                using var cmd1 = _connection.CreateCommand();
                cmd1.CommandText = @"
                    DELETE FROM inv_inventory 
                    WHERE BatchNumber LIKE 'TEST-%' 
                       OR BatchNumber LIKE 'TEMP-%'
                       OR BatchNumber LIKE 'WORKFLOW-%'
                       OR BatchNumber LIKE 'BATCH-POOL-%'
                       OR PartID LIKE 'TEST-PART-%'
                       OR PartID LIKE 'POOL-TEST-%';
                ";
                int rows1 = cmd1.ExecuteNonQuery();
                if (rows1 > 0) Console.WriteLine($"[Cleanup] Removed {rows1} test inventory records");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cleanup] Failed to clean inv_inventory: {ex.Message}");
            }

            // Clean up quick button test records (sys_last_10_transactions)
            try
            {
                using var cmd2 = _connection.CreateCommand();
                cmd2.CommandText = @"
                    DELETE FROM sys_last_10_transactions 
                    WHERE User LIKE 'Test%' 
                       OR User LIKE 'TestUser%'
                       OR User LIKE 'PoolTestUser%';
                ";
                int rows2 = cmd2.ExecuteNonQuery();
                if (rows2 > 0) Console.WriteLine($"[Cleanup] Removed {rows2} quick button test records");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cleanup] Failed to clean sys_last_10_transactions: {ex.Message}");
            }

            // Clean up test quick buttons (sys_quick_buttons) - new cleanup
            try
            {
                CleanupTestQuickButtonsAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cleanup] Failed to clean sys_quick_buttons: {ex.Message}");
            }

            // Clean up test users (usr_users) - new cleanup
            try
            {
                CleanupTestUsersAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cleanup] Failed to clean usr_users: {ex.Message}");
            }

            // Clean up error log test records (table may not exist in test database)
            try
            {
                // First check if table exists
                using var checkCmd = _connection.CreateCommand();
                checkCmd.CommandText = @"
                    SELECT COUNT(*) 
                    FROM information_schema.tables 
                    WHERE table_schema = DATABASE() 
                      AND table_name = 'log_error'
                ";
                var tableExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;

                if (tableExists)
                {
                    using var cmd3 = _connection.CreateCommand();
                    cmd3.CommandText = @"
                        DELETE FROM log_error 
                        WHERE ErrorMessage LIKE '%Test%' 
                           OR ErrorMessage LIKE '%TEST%';
                    ";
                    int rows3 = cmd3.ExecuteNonQuery();
                    if (rows3 > 0) Console.WriteLine($"[Cleanup] Removed {rows3} test error log records");
                }
            }
            catch (Exception ex)
            {
                // Silent fail - log_error table is optional in test database
                Console.WriteLine($"[Cleanup] Skipped error log cleanup: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            // Log but don't throw - cleanup failures shouldn't fail tests
            Console.WriteLine($"[Cleanup] Failed to clean test data: {ex.Message}");
        }
    }

    #endregion

    #region Test Data Setup Helpers

    /// <summary>
    /// Creates test users in the usr_users table for integration testing.
    /// Uses ON DUPLICATE KEY UPDATE to make this method idempotent (safe to call multiple times).
    /// </summary>
    /// <returns>Task representing the async operation.</returns>
    /// <remarks>
    /// <para>
    /// <strong>Test Users Created:</strong>
    /// - TEST-USER: Regular active user (password: TestPass123)
    /// - TEST-ADMIN: Admin user (password: AdminPass123)
    /// - TEST-INACTIVE: Inactive user (password: password)
    /// - TEST-USER-2: Second regular user for multi-user tests (password: TestPass456)
    /// </para>
    /// <para>
    /// <strong>Usage Pattern:</strong>
    /// Call this method in TestInitialize or at the start of tests that need test users.
    /// The method is idempotent, so calling it multiple times is safe.
    /// </para>
    /// <para>
    /// <strong>Cleanup:</strong>
    /// Test users are automatically cleaned up by CleanupTestData() in TestCleanup.
    /// Users with UserID starting with 'TEST-' are removed after each test.
    /// </para>
    /// </remarks>
    protected async Task CreateTestUsersAsync()
    {
        if (_connection == null || _connection.State != ConnectionState.Open)
        {
            Console.WriteLine("[Test Setup] Cannot create test users - connection not available");
            return;
        }

        try
        {
            using var cmd = _connection.CreateCommand();
            // Note: usr_users table uses 'User' column (not 'UserID'), 'Full Name' column (with space)
            // Table has minimal columns: ID (auto), User (unique), 'Full Name', Shift, VitsUser, Pin, etc.
            cmd.CommandText = @"
                INSERT INTO usr_users (
                    `User`, `Full Name`, `Shift`, `VitsUser`, `Pin`
                )
                VALUES
                    -- Regular active user
                    ('TEST-USER', 'Test User', '1', 0, 'TestPass123'),
                    -- Admin user
                    ('TEST-ADMIN', 'Test Admin', '1', 1, 'AdminPass123'),
                    -- Inactive user (for active/inactive filtering tests)
                    ('TEST-INACTIVE', 'Test Inactive User', '1', 0, 'password'),
                    -- Second regular user for multi-user scenarios
                    ('TEST-USER-2', 'Test User Two', '1', 0, 'TestPass456')
                ON DUPLICATE KEY UPDATE 
                    `User` = VALUES(`User`);  -- No-op update to prevent errors on re-run
            ";

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"[Test Setup] Created/verified {rowsAffected / 2} test users (TEST-USER, TEST-ADMIN, TEST-INACTIVE, TEST-USER-2)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Test Setup ERROR] Failed to create test users: {ex.Message}");
            // Don't throw - let tests fail naturally if users are required
        }
    }

    /// <summary>
    /// Creates test quick buttons in the sys_last_10_transactions table for integration testing.
    /// Note: Despite the table name, this table stores quick button configurations, not transaction history.
    /// Uses ON DUPLICATE KEY UPDATE to make this method idempotent (safe to call multiple times).
    /// </summary>
    /// <returns>Task representing the async operation.</returns>
    /// <remarks>
    /// <para>
    /// <strong>Test Quick Buttons Created:</strong>
    /// - 3 buttons for TEST-USER (positions 1, 2, 3)
    /// - 1 button for TEST-USER-2 (position 1)
    /// </para>
    /// <para>
    /// <strong>Prerequisites:</strong>
    /// Test users must exist before calling this method. Call <see cref="CreateTestUsersAsync"/> first.
    /// The sys_last_10_transactions table stores quick button data (User field, not UserID).
    /// </para>
    /// <para>
    /// <strong>Usage Pattern:</strong>
    /// Call this method after CreateTestUsersAsync() in TestInitialize or at the start of quick button tests.
    /// </para>
    /// <para>
    /// <strong>Cleanup:</strong>
    /// Test quick buttons are automatically cleaned up by CleanupTestData() in TestCleanup.
    /// Buttons with User starting with 'TEST-' or 'Test' are removed after each test.
    /// </para>
    /// </remarks>
    protected async Task CreateTestQuickButtonsAsync()
    {
        if (_connection == null || _connection.State != ConnectionState.Open)
        {
            Console.WriteLine("[Test Setup] Cannot create test quick buttons - connection not available");
            return;
        }

        try
        {
            // Note: sys_last_10_transactions table stores quick buttons
            // Check if table exists first
            using var checkCmd = _connection.CreateCommand();
            checkCmd.CommandText = @"
                SELECT COUNT(*) 
                FROM information_schema.tables 
                WHERE table_schema = DATABASE() 
                  AND table_name = 'sys_last_10_transactions'
            ";
            var tableExists = Convert.ToInt32(await checkCmd.ExecuteScalarAsync()) > 0;

            if (!tableExists)
            {
                Console.WriteLine("[Test Setup] sys_last_10_transactions table not found - skipping quick button setup");
                return;
            }

            // Check table structure to determine column names
            using var colCmd = _connection.CreateCommand();
            colCmd.CommandText = "DESCRIBE sys_last_10_transactions";
            using var reader = await colCmd.ExecuteReaderAsync();
            
            var hasUserColumn = false;
            var hasUserIDColumn = false;
            while (await reader.ReadAsync())
            {
                var colName = reader.GetString(0);
                if (colName.Equals("User", StringComparison.OrdinalIgnoreCase))
                    hasUserColumn = true;
                if (colName.Equals("UserID", StringComparison.OrdinalIgnoreCase))
                    hasUserIDColumn = true;
            }
            reader.Close();

            if (!hasUserColumn && !hasUserIDColumn)
            {
                Console.WriteLine("[Test Setup] sys_last_10_transactions table has no User or UserID column - cannot create test buttons");
                return;
            }

            // Insert test quick buttons (structure depends on actual table schema)
            using var cmd = _connection.CreateCommand();
            
            // First delete any existing test data to ensure clean slate
            cmd.CommandText = "DELETE FROM sys_last_10_transactions WHERE `User` LIKE 'TestUser_QB%'";
            await cmd.ExecuteNonQueryAsync();
            
            // Table columns: ID (auto), User, PartID, Operation, Quantity, ReceiveDate (auto), Position
            // Create contiguous data at positions 1-10 for comprehensive testing
            // Quick buttons must be contiguous - no gaps allowed
            cmd.CommandText = @"
                INSERT INTO sys_last_10_transactions (
                    `User`, `PartID`, `Operation`, `Quantity`, `Position`
                )
                VALUES
                    ('TestUser_QB', 'TEST-PART-QB-001', '100', 5, 1),
                    ('TestUser_QB', 'TEST-PART-QB-002', '100', 10, 2),
                    ('TestUser_QB', 'TEST-PART-QB-003', '110', 15, 3),
                    ('TestUser_QB', 'TEST-PART-QB-004', '100', 20, 4),
                    ('TestUser_QB', 'TEST-PART-QB-005', '110', 25, 5),
                    ('TestUser_QB', 'TEST-PART-QB-006', '100', 30, 6),
                    ('TestUser_QB', 'TEST-PART-QB-007', '110', 35, 7),
                    ('TestUser_QB', 'TEST-PART-QB-008', '100', 40, 8),
                    ('TestUser_QB', 'TEST-PART-QB-009', '110', 45, 9),
                    ('TestUser_QB', 'TEST-PART-QB-010', '100', 50, 10),
                    ('TestUser_QB_2', 'TEST-PART-QB-004', '100', 20, 1)
            ";

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"[Test Setup] Created {rowsAffected} test quick buttons (TestUser_QB: positions 1-10 contiguous; TestUser_QB_2: pos 1)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Test Setup ERROR] Failed to create test quick buttons: {ex.Message}");
            // Don't throw - let tests fail naturally if buttons are required
        }
    }

    /// <summary>
    /// Cleans up test quick buttons from the sys_last_10_transactions table.
    /// Note: This table stores quick button configurations despite its name.
    /// Called automatically by TestCleanup, but can be called explicitly if needed.
    /// </summary>
    /// <returns>Task representing the async operation.</returns>
    /// <remarks>
    /// This method removes all quick buttons where User starts with 'Test' or 'TEST-'.
    /// It's safe to call multiple times as it uses pattern matching for cleanup.
    /// </remarks>
    protected async Task CleanupTestQuickButtonsAsync()
    {
        if (_connection == null || _connection.State != ConnectionState.Open)
        {
            Console.WriteLine("[Cleanup] Cannot cleanup test quick buttons - connection not available");
            return;
        }

        try
        {
            // Check if table exists
            using var checkCmd = _connection.CreateCommand();
            checkCmd.CommandText = @"
                SELECT COUNT(*) 
                FROM information_schema.tables 
                WHERE table_schema = DATABASE() 
                  AND table_name = 'sys_last_10_transactions'
            ";
            var tableExists = Convert.ToInt32(await checkCmd.ExecuteScalarAsync()) > 0;

            if (!tableExists)
            {
                return; // Silently skip if table doesn't exist
            }

            using var cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                DELETE FROM sys_last_10_transactions 
                WHERE User LIKE 'Test%' 
                   OR User LIKE 'TEST-%';
            ";

            int rowsDeleted = await cmd.ExecuteNonQueryAsync();
            if (rowsDeleted > 0)
            {
                Console.WriteLine($"[Cleanup] Removed {rowsDeleted} test quick button records from sys_last_10_transactions");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Cleanup ERROR] Failed to cleanup test quick buttons: {ex.Message}");
        }
    }

    /// <summary>
    /// Cleans up test users from the usr_users table.
    /// Called automatically by TestCleanup, but can be called explicitly if needed.
    /// </summary>
    /// <returns>Task representing the async operation.</returns>
    /// <remarks>
    /// This method removes all users where UserID starts with 'TEST-'.
    /// It automatically removes dependent records (quick buttons) first to avoid foreign key violations.
    /// </remarks>
    protected async Task CleanupTestUsersAsync()
    {
        if (_connection == null || _connection.State != ConnectionState.Open)
        {
            Console.WriteLine("[Cleanup] Cannot cleanup test users - connection not available");
            return;
        }

        try
        {
            // First cleanup dependent records (quick buttons)
            await CleanupTestQuickButtonsAsync();

            // Then cleanup users (note: table uses 'User' column, not 'UserID')
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                DELETE FROM usr_users 
                WHERE `User` LIKE 'TEST-%';
            ";

            int rowsDeleted = await cmd.ExecuteNonQueryAsync();
            if (rowsDeleted > 0)
            {
                Console.WriteLine($"[Cleanup] Removed {rowsDeleted} test user records");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Cleanup ERROR] Failed to cleanup test users: {ex.Message}");
        }
    }

    /// <summary>
    /// Seeds the sys_roles table with standard test role data.
    /// Used by System DAO tests that need role lookup functionality.
    /// Uses ON DUPLICATE KEY UPDATE to make this method idempotent (safe to call multiple times).
    /// </summary>
    /// <returns>Task representing the async operation.</returns>
    /// <remarks>
    /// <para>
    /// <strong>Test Roles Created:</strong>
    /// - Admin (RoleID=1): Administrator with full access
    /// - ReadOnly (RoleID=2): Read-only access
    /// - User (RoleID=3): Standard user access
    /// </para>
    /// <para>
    /// <strong>Usage Pattern:</strong>
    /// Call this method in TestInitialize for tests that use GetRoleIdByNameAsync or similar methods.
    /// The method is idempotent, so calling it multiple times is safe.
    /// </para>
    /// </remarks>
    protected async Task EnsureUserTableAsync()
    {
        if (_connection == null || _connection.State != ConnectionState.Open)
        {
            Console.WriteLine("[Test Setup] Cannot seed sys_roles - connection not available");
            return;
        }

        try
        {
            // Check if sys_roles table exists
            using var checkCmd = _connection.CreateCommand();
            checkCmd.CommandText = @"
                SELECT COUNT(*) 
                FROM information_schema.tables 
                WHERE table_schema = DATABASE() 
                  AND table_name = 'sys_roles'
            ";
            var tableExists = Convert.ToInt32(await checkCmd.ExecuteScalarAsync()) > 0;

            if (!tableExists)
            {
                Console.WriteLine("[Test Setup] sys_roles table not found - skipping role data setup");
                return;
            }

            // Seed standard roles for testing
            using var cmd = _connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO sys_roles (ID, RoleName, Description, CreatedBy, IsSystem)
                VALUES
                    (1, 'Admin', 'Administrator with full access', 'TestSetup', 1),
                    (2, 'ReadOnly', 'Read-only access', 'TestSetup', 1),
                    (3, 'User', 'Standard user access', 'TestSetup', 1)
                ON DUPLICATE KEY UPDATE 
                    ID = VALUES(ID);  -- No-op update to prevent errors on re-run
            ";

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"[Test Setup] Seeded/verified {rowsAffected / 2} standard roles (Admin, ReadOnly, User)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Test Setup ERROR] Failed to seed sys_roles: {ex.Message}");
            // Don't throw - let tests fail naturally if roles are required
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
    /// Determines whether the specified table exists in the test database.
    /// </summary>
    /// <param name="tableName">Name of the table to check (case-insensitive).
    /// </param>
    /// <returns><c>true</c> if the table exists; otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="tableName"/> is null or whitespace.</exception>
    protected async Task<bool> TableExistsAsync(string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
        {
            throw new ArgumentException("Table name must be provided", nameof(tableName));
        }

        MySqlConnection? externalConnection = null;

        try
        {
            var connection = _connection;
            if (connection is null || connection.State != ConnectionState.Open)
            {
                externalConnection = new MySqlConnection(GetTestConnectionString());
                await externalConnection.OpenAsync().ConfigureAwait(false);
                connection = externalConnection;
            }

            await using var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*)
                                    FROM information_schema.tables
                                    WHERE table_schema = @schema AND table_name = @table
                                    LIMIT 1";
            command.Parameters.AddWithValue("@schema", Helper_Database_Variables.TestDatabaseName);
            command.Parameters.AddWithValue("@table", tableName);

            var result = await command.ExecuteScalarAsync().ConfigureAwait(false);
            var count = Convert.ToInt32(result ?? 0);
            return count > 0;
        }
        finally
        {
            if (externalConnection != null)
            {
                externalConnection.Close();
                externalConnection.Dispose();
            }
        }
    }

    /// <summary>
    /// Ensures that all specified tables exist; marks the test inconclusive when any are missing.
    /// </summary>
    /// <param name="reason">Additional context explaining why the tables are required.</param>
    /// <param name="tableNames">Names of tables that must exist for the test to run.</param>
    protected async Task EnsureTablesExistOrSkipAsync(string reason, params string[] tableNames)
    {
        if (tableNames == null || tableNames.Length == 0)
        {
            return;
        }

        var missing = new List<string>();
        foreach (var table in tableNames)
        {
            if (string.IsNullOrWhiteSpace(table))
            {
                continue;
            }

            if (!await TableExistsAsync(table).ConfigureAwait(false))
            {
                missing.Add(table);
            }
        }

        if (missing.Count > 0)
        {
            var tableList = string.Join(", ", missing);
            var message = $"Skipping test: required table(s) {tableList} missing in '{Helper_Database_Variables.TestDatabaseName}'.";
            if (!string.IsNullOrWhiteSpace(reason))
            {
                message += $" {reason}";
            }

            Assert.Inconclusive(message);
        }
    }

    /// <summary>
    /// Determines whether the specified stored procedure exists in the test database.
    /// </summary>
    /// <param name="procedureName">Name of the stored procedure to check (case-insensitive).</param>
    /// <returns><c>true</c> if the stored procedure exists; otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="procedureName"/> is null or whitespace.</exception>
    protected async Task<bool> StoredProcedureExistsAsync(string procedureName)
    {
        if (string.IsNullOrWhiteSpace(procedureName))
        {
            throw new ArgumentException("Procedure name must be provided", nameof(procedureName));
        }

        MySqlConnection? externalConnection = null;

        try
        {
            var connection = _connection;
            if (connection is null || connection.State != ConnectionState.Open)
            {
                externalConnection = new MySqlConnection(GetTestConnectionString());
                await externalConnection.OpenAsync().ConfigureAwait(false);
                connection = externalConnection;
            }

            await using var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*)
                                    FROM information_schema.routines
                                    WHERE routine_schema = @schema
                                      AND routine_name = @procedure
                                      AND routine_type = 'PROCEDURE'
                                    LIMIT 1";
            command.Parameters.AddWithValue("@schema", Helper_Database_Variables.TestDatabaseName);
            command.Parameters.AddWithValue("@procedure", procedureName);

            var result = await command.ExecuteScalarAsync().ConfigureAwait(false);
            var count = Convert.ToInt32(result ?? 0);
            return count > 0;
        }
        finally
        {
            if (externalConnection != null)
            {
                externalConnection.Close();
                externalConnection.Dispose();
            }
        }
    }

    /// <summary>
    /// Ensures that all specified stored procedures exist; marks the test inconclusive when any are missing.
    /// </summary>
    /// <param name="reason">Additional context explaining why the procedures are required.</param>
    /// <param name="procedureNames">Names of stored procedures that must exist for the test to run.</param>
    protected async Task EnsureStoredProceduresExistOrSkipAsync(string reason, params string[] procedureNames)
    {
        if (procedureNames == null || procedureNames.Length == 0)
        {
            return;
        }

        var missing = new List<string>();
        foreach (var procedure in procedureNames)
        {
            if (string.IsNullOrWhiteSpace(procedure))
            {
                continue;
            }

            if (!await StoredProcedureExistsAsync(procedure).ConfigureAwait(false))
            {
                missing.Add(procedure);
            }
        }

        if (missing.Count > 0)
        {
            var procList = string.Join(", ", missing);
            var message = $"Skipping test: required stored procedure(s) {procList} missing in '{Helper_Database_Variables.TestDatabaseName}'.";
            if (!string.IsNullOrWhiteSpace(reason))
            {
                message += $" {reason}";
            }

            Assert.Inconclusive(message);
        }
    }

    /// <summary>
    /// Gets the active test transaction.
    /// </summary>
    /// <returns>Always returns null - transaction isolation has been removed.</returns>
    /// <remarks>
    /// <para>
    /// <strong>DEPRECATED:</strong> Transaction-based test isolation has been removed due to
    /// MySQL.Data connector limitations with OUTPUT parameters from stored procedures.
    /// </para>
    /// <para>
    /// <strong>Migration Guide:</strong>
    /// Remove the <c>transaction: GetTestTransaction()</c> parameter from DAO method calls.
    /// Keep <c>connection: GetTestConnection()</c> for connection reuse.
    /// </para>
    /// <para>
    /// See BLOCKER-ANALYSIS.md for technical details on why this was necessary.
    /// </para>
    /// </remarks>
    [Obsolete("Transaction isolation removed - use explicit cleanup instead. Remove transaction parameter from DAO calls.")]
    protected MySqlTransaction? GetTestTransaction()
    {
        return null; // Always null - no transaction isolation
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
                // Transaction support removed - no longer needed
                // Keeping check for backward compatibility but will always be null
#pragma warning disable CS0618 // Type or member is obsolete
                if (_transaction != null && ReferenceEquals(connection, _connection))
                {
                    command.Transaction = _transaction;
                }
#pragma warning restore CS0618 // Type or member is obsolete

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
              (result.Exception != null ? $"\nException: {result.Exception.Message}\nStackTrace: {result.Exception.StackTrace}" : string.Empty);

        if (!result.IsSuccess)
        {
            Console.WriteLine($"[FAILURE DETAILS] {failureDetails}");
        }

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
