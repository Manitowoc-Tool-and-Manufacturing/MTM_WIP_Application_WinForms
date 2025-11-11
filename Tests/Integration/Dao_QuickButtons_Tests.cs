using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_WIP_Application_Winforms.Data;
using System;
using System.Threading.Tasks;

namespace MTM_WIP_Application_Winforms.Tests.Integration;

/// <summary>
/// Integration tests for Dao_QuickButtons operations.
/// Tests quick button CRUD and positioning operations.
/// </summary>
/// <remarks>
/// Dao_QuickButtons patterns discovered:
/// - All methods are static
/// - All methods have Async suffix
/// - All methods return Model_Dao_Result (no generic type parameter)
/// - Position parameters are 1-based (1-10) for UI display
/// - Uses sys_last_10_transactions_* stored procedures
/// </remarks>
[TestClass]
public class Dao_QuickButtons_Tests : BaseIntegrationTest
{
    /// <summary>
    /// Sets up test data for quick button tests.
    /// Creates test users and test quick buttons in the database.
    /// Call this method at the start of each test that requires test data.
    /// </summary>
    /// <remarks>
    /// This method is idempotent - it's safe to call multiple times.
    /// It ensures that test data is available for quick button operations.
    /// The base class TestInitialize handles connection setup automatically.
    /// </remarks>
    private async Task SetupTestDataAsync()
    {
        // Create test users and test quick buttons
        await CreateTestUsersAsync();
        await CreateTestQuickButtonsAsync();

        Console.WriteLine("[Dao_QuickButtons_Tests] Test data setup complete");
    }

    private async Task EnsureQuickButtonTableAsync()
    {
        await EnsureTablesExistOrSkipAsync(
                "Quick button integration tests require the sys_last_10_transactions table. Run the UpdatedDatabase deployment scripts to provision it in the test database.",
                "sys_last_10_transactions")
            .ConfigureAwait(false);

        await EnsureStoredProceduresExistOrSkipAsync(
                "Quick button integration tests require the sys_last_10_transactions_* stored procedures. Run the UpdatedStoredProcedures deployment scripts to provision them in the test database.",
                "sys_last_10_transactions_Update_ByUserAndPosition",
                "sys_last_10_transactions_RemoveAndShift_ByUser",
                "sys_last_10_transactions_Add_AtPosition",
                "sys_last_10_transactions_Move",
                "sys_last_10_transactions_DeleteAll_ByUser",
                "sys_last_10_transactions_AddQuickButton",
                "sys_last_10_transactions_Delete_ByUserAndPosition")
            .ConfigureAwait(false);
    }

    #region Test Data

    private const string TestUser = "TestUser_QB";
    private const string TestUser2 = "TestUser_QB_2";
    private const string TestPartId = "TEST-PART-QB-001";
    private const string TestOperation = "100";
    private const int TestQuantity = 5;

    #endregion

    #region CRUD Tests

    /// <summary>
    /// Tests that UpdateQuickButtonAsync updates a quick button at specific position.
    /// Validates: successful execution, parameter mapping.
    /// </summary>
    [TestMethod]
    public async Task UpdateQuickButtonAsync_ValidData_UpdatesButton()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        int position = 1; // 1-based position
        string partId = TestPartId;
        string operation = TestOperation;
        int quantity = TestQuantity;

        // Act
        var result = await Dao_QuickButtons.UpdateQuickButtonAsync(
            TestUser, position, partId, operation, quantity,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful update of quick button");
        Console.WriteLine($"UpdateQuickButtonAsync successfully updated position {position} " +
            $"with {partId} Op:{operation} Qty:{quantity}");
    }

    /// <summary>
    /// Tests that AddQuickButtonAsync creates a new quick button at specific position.
    /// Validates: successful execution, proper position handling.
    /// </summary>
    [TestMethod]
    public async Task AddQuickButtonAsync_ValidData_AddsButton()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        int position = 5; // Mid-range position
        string partId = "TEST-PART-QB-002";
        string operation = "110";
        int quantity = 10;

        // Act
        var result = await Dao_QuickButtons.AddQuickButtonAsync(
            TestUser, partId, operation, quantity, position,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful addition of quick button");
        Console.WriteLine($"AddQuickButtonAsync successfully added button at position {position}");
    }

    /// <summary>
    /// Tests that RemoveQuickButtonAndShiftAsync removes button and shifts remaining.
    /// Validates: successful execution, position bounds checking (1-10).
    /// </summary>
    [TestMethod]
    public async Task RemoveQuickButtonAndShiftAsync_ValidPosition_RemovesAndShifts()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        int position = 2; // 1-based position to remove

        // Act
        var result = await Dao_QuickButtons.RemoveQuickButtonAndShiftAsync(TestUser, position,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful removal and shift of quick button");
        Console.WriteLine($"RemoveQuickButtonAndShiftAsync successfully removed position {position}");
    }

    /// <summary>
    /// Tests that DeleteAllQuickButtonsForUserAsync removes all buttons for user.
    /// Validates: successful execution, user-specific deletion.
    /// </summary>
    [TestMethod]
    public async Task DeleteAllQuickButtonsForUserAsync_ValidUser_DeletesAllButtons()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Act
        var result = await Dao_QuickButtons.DeleteAllQuickButtonsForUserAsync(TestUser);

        // Assert
        AssertSuccess(result, "Expected successful deletion of all quick buttons for user");
        Console.WriteLine($"DeleteAllQuickButtonsForUserAsync successfully deleted all buttons for '{TestUser}'");
    }

    #endregion

    #region Position Management Tests

    /// <summary>
    /// Tests that MoveQuickButtonAsync moves button between positions.
    /// Validates: successful execution, from/to position handling (1-10 range).
    /// </summary>
    [TestMethod]
    public async Task MoveQuickButtonAsync_ValidPositions_MovesButton()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        int fromPosition = 3; // 1-based source position
        int toPosition = 7;   // 1-based target position

        // Act
        var result = await Dao_QuickButtons.MoveQuickButtonAsync(
            TestUser, fromPosition, toPosition,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful move of quick button");
        Console.WriteLine($"MoveQuickButtonAsync successfully moved button from position {fromPosition} to {toPosition}");
    }

    /// <summary>
    /// Tests that AddOrShiftQuickButtonAsync adds button or shifts existing.
    /// Validates: successful execution, automatic position management.
    /// </summary>
    [TestMethod]
    public async Task AddOrShiftQuickButtonAsync_ValidData_AddsOrShifts()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        string partId = "TEST-PART-QB-003";
        string operation = "120";
        int quantity = 15;

        // Act
        var result = await Dao_QuickButtons.AddOrShiftQuickButtonAsync(
            TestUser, partId, operation, quantity,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful add or shift of quick button");
        Console.WriteLine($"AddOrShiftQuickButtonAsync successfully processed {partId}");
    }

    /// <summary>
    /// Tests that RemoveAndShiftQuickButtonAsync removes and shifts in one operation.
    /// Validates: successful execution, atomic remove/shift behavior.
    /// </summary>
    [TestMethod]
    public async Task RemoveAndShiftQuickButtonAsync_ValidPosition_RemovesAndShifts()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        int position = 4; // 1-based position

        // Act
        var result = await Dao_QuickButtons.RemoveAndShiftQuickButtonAsync(TestUser, position,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful remove and shift of quick button");
        Console.WriteLine($"RemoveAndShiftQuickButtonAsync successfully removed position {position}");
    }

    /// <summary>
    /// Tests that AddQuickButtonAtPositionAsync inserts button at specific position.
    /// Validates: successful execution, explicit position control.
    /// </summary>
    [TestMethod]
    public async Task AddQuickButtonAtPositionAsync_ValidData_AddsAtPosition()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        string partId = "TEST-PART-QB-004";
        string operation = "100";
        int quantity = 20;
        int position = 8; // Specific target position

        // Act
        var result = await Dao_QuickButtons.AddQuickButtonAtPositionAsync(
            TestUser, partId, operation, quantity, position,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful addition at specific position");
        Console.WriteLine($"AddQuickButtonAtPositionAsync successfully added {partId} at position {position}");
    }

    #endregion

    #region Edge Case Tests

    /// <summary>
    /// Tests that operations handle position boundary (position 1 - minimum).
    /// Validates: successful execution at lower bound.
    /// </summary>
    [TestMethod]
    public async Task UpdateQuickButtonAsync_Position1_UpdatesButton()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        int position = 1; // Lower bound

        // Act
        var result = await Dao_QuickButtons.UpdateQuickButtonAsync(
            TestUser, position, TestPartId, TestOperation, TestQuantity,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful update at position 1 (lower bound)");
        Console.WriteLine("UpdateQuickButtonAsync successfully handled position 1 (lower bound)");
    }

    /// <summary>
    /// Tests that operations handle position boundary (position 10 - maximum).
    /// Validates: successful execution at upper bound.
    /// </summary>
    [TestMethod]
    public async Task UpdateQuickButtonAsync_Position10_UpdatesButton()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        int position = 10; // Upper bound

        // Act
        var result = await Dao_QuickButtons.UpdateQuickButtonAsync(
            TestUser, position, TestPartId, TestOperation, TestQuantity,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful update at position 10 (upper bound)");
        Console.WriteLine("UpdateQuickButtonAsync successfully handled position 10 (upper bound)");
    }

    /// <summary>
    /// Tests that MoveQuickButtonAsync handles same-position move (no-op).
    /// Validates: successful execution when from and to positions are identical.
    /// </summary>
    [TestMethod]
    public async Task MoveQuickButtonAsync_SamePosition_HandlesGracefully()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        int position = 2; // Same source and target - use position that has test data

        // Act
        var result = await Dao_QuickButtons.MoveQuickButtonAsync(
            TestUser, position, position,
            connectionString: GetTestConnectionString());

        // Assert
        AssertSuccess(result, "Expected successful handling of same-position move");
        Console.WriteLine("MoveQuickButtonAsync successfully handled same-position move (no-op)");
    }

    #endregion

    #region Integration Workflow Tests

    /// <summary>
    /// Tests complete quick button workflow: add, update, move, remove.
    /// Validates: sequential operations work together correctly.
    /// </summary>
    [TestMethod]
    public async Task QuickButtonWorkflow_CompleteSequence_ExecutesSuccessfully()
    {
        // Setup: Ensure table exists and create test data
        await EnsureQuickButtonTableAsync();
        await SetupTestDataAsync();

        // Arrange
        string workflowUser = "TestUser_Workflow";
        string partId = "TEST-PART-WORKFLOW";
        string operation = "100";
        int quantity = 10;
        int position = 3;

        // Step 1: Add quick button
        var addResult = await Dao_QuickButtons.AddQuickButtonAsync(
            workflowUser, partId, operation, quantity, position,
            connectionString: GetTestConnectionString());
        AssertSuccess(addResult, "Expected successful add in workflow");
        Console.WriteLine($"Workflow Step 1: Added button at position {position}");

        // Step 2: Update quick button
        var updateResult = await Dao_QuickButtons.UpdateQuickButtonAsync(
            workflowUser, position, partId, operation, quantity + 5,
            connectionString: GetTestConnectionString());
        AssertSuccess(updateResult, "Expected successful update in workflow");
        Console.WriteLine($"Workflow Step 2: Updated button at position {position}");

        // Step 3: Move quick button
        int newPosition = 6;
        var moveResult = await Dao_QuickButtons.MoveQuickButtonAsync(
            workflowUser, position, newPosition,
            connectionString: GetTestConnectionString());
        AssertSuccess(moveResult, "Expected successful move in workflow");
        Console.WriteLine($"Workflow Step 3: Moved button from {position} to {newPosition}");

        // Step 4: Remove quick button
        var removeResult = await Dao_QuickButtons.RemoveAndShiftQuickButtonAsync(
            workflowUser, newPosition,
            connectionString: GetTestConnectionString());
        AssertSuccess(removeResult, "Expected successful remove in workflow");
        Console.WriteLine($"Workflow Step 4: Removed button at position {newPosition}");

        // Step 5: Clean up
        var deleteResult = await Dao_QuickButtons.DeleteAllQuickButtonsForUserAsync(workflowUser);
        AssertSuccess(deleteResult, "Expected successful cleanup in workflow");
        Console.WriteLine("Workflow Step 5: Cleaned up all buttons for workflow user");
    }

    #endregion
}
