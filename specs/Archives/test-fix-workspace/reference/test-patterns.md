# Test Development Patterns

**Purpose**: Integration test templates and C# patterns for MTM project  
**Last Updated**: 2025-10-19  
**Framework**: xUnit/MSTest, BaseIntegrationTest

---

## Quick Navigation

- [Test Class Template](#test-class-template)
- [Discovery-First Workflow](#discovery-first-workflow)
- [Null-Safe DaoResult Pattern](#null-safe-daoresult-pattern)
- [Test Data Setup](#test-data-setup-patterns)
- [Common Test Patterns](#common-test-patterns)

---

## Test Class Template

**Purpose**: Complete test class template following BaseIntegrationTest and integration-testing.instructions.md patterns.

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Models;
using System;
using System.Threading.Tasks;
using System.Data;

namespace MTM_Inventory_Application.Tests.Integration
{
    /// <summary>
    /// Integration tests for Dao_[EntityName] class.
    /// Tests stored procedure execution through DAO wrapper methods.
    /// Follows discovery-first workflow: verified method signatures before writing tests.
    /// </summary>
    [TestClass]
    public class Dao_[EntityName]_Tests : BaseIntegrationTest
    {
        #region Test Setup and Cleanup

        /// <summary>
        /// Runs before each test. Sets up test data if needed.
        /// </summary>
        [TestInitialize]
        public async Task TestSetup()
        {
            // Create test users if tests need them
            await CreateTestUsersAsync();
            
            // Create entity-specific test data if needed
            // await CreateTest[Entity]DataAsync();
        }

        /// <summary>
        /// Runs after each test. Cleans up test data (optional for test DB).
        /// </summary>
        [TestCleanup]
        public async Task TestTeardown()
        {
            // Cleanup is optional for test database
            // await CleanupTest[Entity]DataAsync();
            // await CleanupTestUsersAsync();
        }

        #endregion

        #region Query Methods Tests

        /// <summary>
        /// Tests that GetAll[Entity] returns the complete entity list.
        /// Validates: successful execution, non-null DataTable, at least one row.
        /// </summary>
        [TestMethod]
        public async Task GetAll[Entity]_Execution_ReturnsData()
        {
            // Arrange
            var connectionString = GetTestConnectionString();

            // Act
            var result = await Dao_[Entity].GetAll[Entity]Async();

            // Assert
            Assert.IsTrue(result.IsSuccess, 
                $"Expected success, got failure: {result.ErrorMessage}");
            Assert.IsNotNull(result.Data, 
                "Expected non-null DataTable");
            Assert.IsTrue(result.Data.Rows.Count > 0, 
                "Expected at least one row");
        }

        #endregion

        #region CRUD Methods Tests

        /// <summary>
        /// Tests entity creation with valid data.
        /// Setup: Creates unique test entity with GUID.
        /// Validates: successful creation, entity can be retrieved.
        /// Cleanup: Deletes test entity (optional for test DB).
        /// </summary>
        [TestMethod]
        public async Task Create[Entity]_ValidData_CreatesEntity()
        {
            // Arrange
            var testId = "TEST-" + Guid.NewGuid().ToString().Substring(0, 8);
            var testName = "Test Entity " + testId;
            
            try
            {
                // Act
                var createResult = await Dao_[Entity].Create[Entity]Async(
                    testId, 
                    testName,
                    // ... other parameters
                );
                
                // Assert - Creation
                Assert.IsTrue(createResult.IsSuccess, 
                    $"Create failed: {createResult.ErrorMessage}");
                
                // Assert - Retrieval (verify entity was created)
                var getResult = await Dao_[Entity].Get[Entity]ByIdAsync(testId);
                Assert.IsTrue(getResult.IsSuccess && getResult.Data != null, 
                    "Should be able to retrieve created entity");
                
                if (getResult.Data is DataRow row)
                {
                    Assert.AreEqual(testId, row["EntityID"].ToString(),
                        "Retrieved entity ID should match created entity");
                }
            }
            finally
            {
                // Cleanup (optional for test DB)
                await Dao_[Entity].Delete[Entity]Async(testId);
            }
        }

        #endregion

        #region Validation Methods Tests

        /// <summary>
        /// Tests that [Entity]Exists returns true for existing entity.
        /// Uses existing data from database to avoid test data creation.
        /// </summary>
        [TestMethod]
        public async Task [Entity]Exists_ExistingEntity_ReturnsTrue()
        {
            // Arrange - use existing entity from GetAll
            var allEntities = await Dao_[Entity].GetAll[Entity]Async();
            Assert.IsTrue(allEntities.IsSuccess && 
                         allEntities.Data != null && 
                         allEntities.Data.Rows.Count > 0,
                "Need at least one entity for test");
            
            var existingId = allEntities.Data.Rows[0]["EntityID"].ToString();
            
            // Act
            var result = await Dao_[Entity].[Entity]ExistsAsync(existingId);
            
            // Assert
            Assert.IsTrue(result.IsSuccess && result.Data, 
                $"Expected entity '{existingId}' to exist");
        }

        /// <summary>
        /// Tests that [Entity]Exists returns false for non-existent entity.
        /// </summary>
        [TestMethod]
        public async Task [Entity]Exists_NonExistentEntity_ReturnsFalse()
        {
            // Arrange
            var nonExistentId = "NONEXISTENT-" + Guid.NewGuid().ToString();
            
            // Act
            var result = await Dao_[Entity].[Entity]ExistsAsync(nonExistentId);
            
            // Assert
            Assert.IsTrue(result.IsSuccess && !result.Data, 
                $"Expected entity '{nonExistentId}' to not exist");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates test users needed for entity tests.
        /// Uses ON DUPLICATE KEY UPDATE to be idempotent.
        /// </summary>
        private async Task CreateTestUsersAsync()
        {
            // Implementation: Use Helper_Database_StoredProcedure or direct SQL
            // See Test Data Setup Patterns section below
        }

        /// <summary>
        /// Cleans up test users created during tests.
        /// Optional for test database.
        /// </summary>
        private async Task CleanupTestUsersAsync()
        {
            // Implementation: Delete TEST-* users
            // Remember to delete child records first (foreign keys)
        }

        #endregion
    }
}
```

---

## Discovery-First Workflow

**Purpose**: Verify actual method signatures before writing tests to avoid common mistakes.

### Step 1: Find All Methods in DAO

```csharp
// ❌ DON'T: Assume method signature based on other DAOs
var result = await Dao_Something.GetDataAsync();  // Might not exist!

// ✅ DO: Use grep_search to find actual methods
```

**Tool Usage**:
```typescript
grep_search({
  isRegexp: true,
  includePattern: "Data/Dao_[Entity].cs",
  query: "(public|internal) (static )?.*Task.*\\("
})
```

### Step 2: Read Method Signatures

```csharp
// After finding method at line 45, read the actual signature
```

**Tool Usage**:
```typescript
read_file({
  filePath: "Data/Dao_[Entity].cs",
  offset: 45,
  limit: 30
})
```

### Step 3: Document Findings

```csharp
// At top of test file, document DAO patterns:
// Dao_[Entity] patterns discovered:
// - All methods are static (no instance needed)
// - No Async suffix on method names (GetAllItems not GetAllItemsAsync)
// - Returns DaoResult<DataTable> for Get operations
// - Returns DaoResult<bool> for Exists operations
// - Parameter names: itemId, locationCode, includeInactive
```

### Step 4: Write Tests Based on Actual Signatures

```csharp
// ✅ CORRECT: Using verified signature
var result = await Dao_ItemType.GetAllItemTypes();  // No Async suffix!

// ❌ WRONG: Assuming Async suffix
var result = await Dao_ItemType.GetAllItemTypesAsync();  // Doesn't exist!
```

---

## Null-Safe DaoResult Pattern

**Purpose**: Safely check DaoResult.Data without null reference exceptions.

### Common DaoResult Return Types

```csharp
// DaoResult<DataTable> - most Get operations
DaoResult<DataTable> result = await Dao_Something.GetDataAsync();

// DaoResult<bool> - Exists operations
DaoResult<bool> result = await Dao_Something.ItemExistsAsync();

// DaoResult<DataRow> - GetById operations
DaoResult<DataRow> result = await Dao_Something.GetByIdAsync();

// DaoResult (no type parameter) - Add/Update/Delete operations
DaoResult result = await Dao_Something.DeleteItemAsync();
```

### Null-Safe Assertion Patterns

```csharp
// ✅ CORRECT: DaoResult<DataTable> with null check
var result = await Dao_Something.GetDataAsync();
Assert.IsTrue(result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0,
    "Expected successful result with non-null DataTable and rows");

// ❌ WRONG: Assumes Data is non-null
var result = await Dao_Something.GetDataAsync();
Assert.IsTrue(result.IsSuccess && result.Data.Rows.Count > 0);  // NullReferenceException if Data is null!

// ✅ CORRECT: DaoResult<bool>
var result = await Dao_Something.ItemExistsAsync(id);
Assert.IsTrue(result.IsSuccess && result.Data,
    "Expected successful result with Data = true");

// ✅ CORRECT: DaoResult<DataRow>
var result = await Dao_Something.GetByIdAsync(id);
Assert.IsTrue(result.IsSuccess && result.Data != null,
    "Expected successful result with non-null DataRow");

// ✅ CORRECT: DaoResult (no type parameter)
var result = await Dao_Something.DeleteItemAsync(id);
Assert.IsTrue(result.IsSuccess,
    $"Expected successful deletion, got: {result.ErrorMessage}");
```

---

## Test Data Setup Patterns

### Pattern 1: TestInitialize Setup (Recommended)

```csharp
[TestClass]
public class Dao_Example_Tests : BaseIntegrationTest
{
    [TestInitialize]
    public async Task TestSetup()
    {
        // Create test users once per test
        await CreateTestUsersAsync();
        
        // Create entity-specific test data
        await CreateTestQuickButtonsAsync();
    }
    
    [TestCleanup]
    public async Task TestTeardown()
    {
        // Optional cleanup for test database
        await CleanupTestQuickButtonsAsync();
        await CleanupTestUsersAsync();
    }
    
    private async Task CreateTestUsersAsync()
    {
        var sql = @"
            INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
            VALUES 
                ('TEST-USER', 'Test User', SHA2('password', 256), 1, NOW()),
                ('TEST-ADMIN', 'Test Admin', SHA2('password', 256), 1, NOW())
            ON DUPLICATE KEY UPDATE UserID = UserID;
        ";
        
        // Execute SQL using Helper_Database_StoredProcedure or MySqlCommand
        // Implementation depends on project helpers
    }
}
```

### Pattern 2: Per-Test Setup (For Unique Data)

```csharp
[TestMethod]
public async Task CreateItem_ValidData_CreatesItem()
{
    // Arrange - unique test ID per test
    var testId = "TEST-" + Guid.NewGuid().ToString().Substring(0, 8);
    
    try
    {
        // Act
        var result = await Dao_Item.CreateItemAsync(testId, ...);
        
        // Assert
        Assert.IsTrue(result.IsSuccess);
    }
    finally
    {
        // Cleanup this test's data
        await Dao_Item.DeleteItemAsync(testId);
    }
}
```

### Pattern 3: Using Existing Data (Read-Only Tests)

```csharp
[TestMethod]
public async Task GetAllItems_Execution_ReturnsItems()
{
    // Arrange - no setup needed, use existing data
    
    // Act
    var result = await Dao_Item.GetAllItemsAsync();
    
    // Assert - just verify operation succeeds
    Assert.IsTrue(result.IsSuccess && result.Data != null);
    
    // Use first item for further tests if needed
    if (result.Data.Rows.Count > 0)
    {
        var existingItemId = result.Data.Rows[0]["ItemID"].ToString();
        // Can use existingItemId for Get/Exists tests
    }
}
```

---

## Common Test Patterns

### Pattern: Test with Known Test Data

```csharp
[TestMethod]
public async Task GetQuickButtonsByUser_ExistingUser_ReturnsButtons()
{
    // Arrange
    const string testUserId = "TEST-USER";  // Known test user from setup
    
    // Act
    var result = await Dao_QuickButtons.GetQuickButtonsByUserAsync(testUserId);
    
    // Assert
    Assert.IsTrue(result.IsSuccess && result.Data != null,
        $"Expected success for user '{testUserId}'");
    Assert.IsTrue(result.Data.Rows.Count > 0,
        $"Expected user '{testUserId}' to have quick buttons");
}
```

### Pattern: Test with Generated Unique Data

```csharp
[TestMethod]
public async Task AddQuickButton_ValidData_InsertsButton()
{
    // Arrange
    var testUserId = "TEST-USER";
    var uniqueButtonText = "Test Button " + Guid.NewGuid().ToString().Substring(0, 8);
    
    try
    {
        // Act
        var result = await Dao_QuickButtons.AddQuickButtonAsync(
            testUserId,
            uniqueButtonText,
            "100",  // Operation
            "FLOOR",  // Location
            "PART-001",  // PartNumber
            1  // Position
        );
        
        // Assert
        Assert.IsTrue(result.IsSuccess,
            $"Expected successful insertion: {result.ErrorMessage}");
    }
    finally
    {
        // Cleanup - delete by button text or ID
        // (Implementation depends on DAO methods available)
    }
}
```

### Pattern: Test Error Scenarios

```csharp
[TestMethod]
public async Task AddQuickButton_InvalidUser_ReturnsError()
{
    // Arrange
    var nonExistentUser = "NONEXISTENT-USER";
    
    // Act
    var result = await Dao_QuickButtons.AddQuickButtonAsync(
        nonExistentUser,
        "Test Button",
        "100",
        "FLOOR",
        "PART-001",
        1
    );
    
    // Assert - expect failure due to foreign key constraint
    Assert.IsFalse(result.IsSuccess,
        "Expected failure for non-existent user");
    Assert.IsNotNull(result.ErrorMessage,
        "Expected error message explaining failure");
    Assert.IsTrue(result.ErrorMessage.Contains("user") || 
                  result.ErrorMessage.Contains("foreign key"),
        "Error message should mention user or foreign key issue");
}
```

### Pattern: Test with Multiple Related Records

```csharp
[TestMethod]
public async Task GetQuickButtonCount_MultipleButtons_ReturnsCorrectCount()
{
    // Arrange
    const string testUserId = "TEST-USER";
    const int expectedCount = 3;  // Known from test data setup
    
    // Act
    var result = await Dao_QuickButtons.GetQuickButtonCountAsync(testUserId);
    
    // Assert
    Assert.IsTrue(result.IsSuccess && result.Data == expectedCount,
        $"Expected count of {expectedCount} for user '{testUserId}', got {result.Data}");
}
```

---

## Naming Conventions

### Test Method Naming Pattern

```csharp
// Pattern: MethodName_Scenario_ExpectedResult

[TestMethod]
public async Task GetAllItems_Execution_ReturnsItems()

[TestMethod]
public async Task ItemExists_ExistingItem_ReturnsTrue()

[TestMethod]
public async Task CreateItem_InvalidData_ReturnsError()

[TestMethod]
public async Task UpdateItem_NonExistentItem_ReturnsError()
```

### Test Class Naming Pattern

```csharp
// Pattern: Dao_[EntityName]_Tests

public class Dao_QuickButtons_Tests : BaseIntegrationTest
public class Dao_System_Tests : BaseIntegrationTest
public class Dao_Inventory_Tests : BaseIntegrationTest
```

### Test Data Naming Pattern

```csharp
// Pattern: TEST-* for all test data IDs

var testUserId = "TEST-USER";
var testButtonId = "TEST-BUTTON-" + Guid.NewGuid().ToString().Substring(0, 8);
var testPartNumber = "TEST-PART-001";

// Benefits:
// - Easy to identify test data in database
// - Easy to clean up: DELETE FROM table WHERE ID LIKE 'TEST-%'
// - Prevents accidents with production-like data
```

---

## Troubleshooting Test Issues

### Issue: Test Fails with "Data is null"

**Problem**: DaoResult.Data is null even though IsSuccess is true.

**Solution**:
```csharp
// ❌ Incorrect assertion
Assert.IsTrue(result.IsSuccess && result.Data.Rows.Count > 0);

// ✅ Correct null-safe assertion
Assert.IsTrue(result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0,
    "Expected non-null DataTable with rows");
```

### Issue: Test Fails with Foreign Key Constraint

**Problem**: Cannot insert test data due to missing parent record.

**Solution**:
```csharp
// Ensure parent records exist first
await CreateTestUsersAsync();  // Create users before quick buttons

// Then create child records
await CreateTestQuickButtonsAsync();  // Quick buttons reference users
```

### Issue: Tests Pass Individually But Fail Together

**Problem**: Tests interfere with each other due to shared data.

**Solution**:
```csharp
// Use unique IDs per test
var uniqueId = "TEST-" + Guid.NewGuid().ToString().Substring(0, 8);

// Or ensure proper cleanup
[TestCleanup]
public async Task TestTeardown()
{
    await CleanupTestDataAsync();
}
```

---

**Pattern Count**: 15+ ready-to-use patterns  
**Last Updated**: 2025-10-19  
**Maintained by**: Development Team  
**Reference**: .github/instructions/integration-testing.instructions.md
