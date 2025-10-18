---
description: 'Integration test development patterns, DAO method discovery, and test isolation strategies'
applyTo: 'Tests/**/*.cs'
---

<!-- Based on lessons learned from Phase 2.5 database layer standardization -->

# Integration Testing Standards

## Available MCP Tools for Integration Testing

When developing integration tests, use these MCP tools from the **mtm-workflow** server:
- `generate_unit_tests` - Auto-generate test scaffolding (use as starting point only)
- `check_security` - Identify security test scenarios
- `analyze_performance` - Create performance test cases
- `validate_dao_patterns` - Verify DAO structure before testing

## Overview

This file defines integration testing patterns specific to the MTM WIP Application's DAO layer, BaseIntegrationTest usage, and method signature discovery workflow to prevent common pitfalls.

## Core Principles

### Always Verify Before Writing
- **Never assume method signatures** - always verify actual implementations
- **Check static vs instance** - DAOs may use different patterns
- **Verify parameter names** - don't rely on conventions
- **Confirm return types** - DaoResult vs DaoResult<T> matters

### Discovery-First Workflow
Before writing any test:
1. Use `grep_search` to find actual method signatures
2. Verify static vs instance method patterns
3. Check parameter names and types
4. Identify return type (DaoResult vs DaoResult<T>)
5. Only then write the test

## Method Signature Discovery Pattern

### Step 1: Find All Methods in DAO

```bash
# Use grep_search with regex to find all public/internal methods
grep_search(
  isRegexp=true,
  includePattern="Data/Dao_TargetName.cs",
  query="(public|internal) (static )?.*Task.*\\("
)
```

### Step 2: Read Method Signatures

```bash
# Read the specific method to see full signature
read_file(
  filePath="Data/Dao_TargetName.cs",
  offset=lineNumber,
  limit=30
)
```

### Step 3: Verify Pattern

Check for:
- Static vs instance method (presence of `static` keyword)
- Async suffix convention (some DAOs use `Async`, some don't)
- Parameter names (exact casing matters)
- Return type details (`DaoResult`, `DaoResult<DataTable>`, `DaoResult<bool>`, etc.)

## Common DAO Patterns

### Static Method Pattern

```csharp
// DAO Definition
internal static async Task<DaoResult<DataTable>> GetAllItems()

// Test Usage
var result = await Dao_Item.GetAllItems();
```

### Instance Method Pattern

```csharp
// DAO Definition
public async Task<DaoResult<List<Model>>> SearchAsync(...)

// Test Usage
var dao = new Dao_Item();
var result = await dao.SearchAsync(...);
```

### Inconsistent Async Naming

```csharp
// Some DAOs have Async suffix
var result = await Dao_Part.GetAllPartsAsync();

// Others don't
var result = await Dao_ItemType.GetAllItemTypes(); // No Async suffix!
```

**Rule**: Always verify - never assume based on other DAOs.

## DaoResult Null Safety Pattern

### Always Check Data for Null

```csharp
// ❌ WRONG: Assumes Data is non-null
var result = await Dao_Something.GetData();
Assert.IsTrue(result.IsSuccess && result.Data.Rows.Count > 0);

// ✅ CORRECT: Null-safe check
var result = await Dao_Something.GetData();
Assert.IsTrue(result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0,
    "Need at least one item for test");
```

### Handle Different Return Types

```csharp
// DaoResult<DataTable> - check for null and rows
Assert.IsTrue(result.Data != null && result.Data.Rows.Count > 0);

// DaoResult<bool> - Data is boolean
Assert.IsTrue(result.IsSuccess && result.Data);

// DaoResult<DataRow> - check for null
Assert.IsNotNull(result.Data);

// DaoResult (no type parameter) - only check IsSuccess
Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
```

## BaseIntegrationTest Usage

### Inherit from BaseIntegrationTest

```csharp
[TestClass]
public class Dao_Something_Tests : BaseIntegrationTest
{
    // BaseIntegrationTest provides:
    // - GetTestConnectionString()
    // - Verbose JSON diagnostics on failure
    // - Test isolation guidance
}
```

### Test Isolation Best Practices

- Use existing data for read-only tests (Get, Exists, Search)
- Create unique test data with GUIDs for write tests
- Clean up test data in test cleanup when possible
- Avoid dependencies between tests

```csharp
// ✅ GOOD: Read from existing data
var allItems = await Dao_Item.GetAllItems();
Assert.IsTrue(allItems.Data != null && allItems.Data.Rows.Count > 0);
var existingItem = allItems.Data.Rows[0]["ItemNumber"].ToString();

// ✅ GOOD: Create unique test data
var testItem = "TestItem_" + Guid.NewGuid().ToString().Substring(0, 8);
```

## Integration Test Naming Conventions

### Method Naming Pattern

```csharp
// Pattern: MethodName_Scenario_ExpectedResult
[TestMethod]
public async Task GetAllItems_Execution_ReturnsItems()

[TestMethod]
public async Task ItemExists_ExistingItem_ReturnsTrue()

[TestMethod]
public async Task SearchItems_WithPagination_ReturnsPaginatedResults()
```

### Test Organization

```csharp
[TestClass]
public class Dao_Something_Tests : BaseIntegrationTest
{
    #region Query Methods Tests
    
    [TestMethod]
    public async Task GetAll_Test() { }
    
    #endregion
    
    #region CRUD Methods Tests
    
    [TestMethod]
    public async Task Create_Test() { }
    
    #endregion
    
    #region Validation Methods Tests
    
    [TestMethod]
    public async Task Exists_Test() { }
    
    #endregion
}
```

## Common Pitfalls to Avoid

### Pitfall 1: Method Name Assumptions

```csharp
// ❌ WRONG: Assuming Async suffix
var result = await Dao_ItemType.GetAllItemTypesAsync();

// ✅ CORRECT: Verified actual method name
var result = await Dao_ItemType.GetAllItemTypes();
```

### Pitfall 2: Static vs Instance Confusion

```csharp
// ❌ WRONG: Calling instance method statically
var result = await Dao_Transactions.SearchTransactionsAsync(...);

// ✅ CORRECT: Creating instance first
var dao = new Dao_Transactions();
var result = await dao.SearchTransactionsAsync(...);
```

### Pitfall 3: Parameter Name Mistakes

```csharp
// ❌ WRONG: Guessing parameter name
var result = await dao.SmartSearchAsync(searchTerm: "test");

// ✅ CORRECT: Using actual parameter name
var result = await dao.SmartSearchAsync(query: "test");
// OR for complex signature:
var result = await dao.SmartSearchAsync(
    searchTerms: new Dictionary<string, string>(),
    transactionTypes: new List<TransactionType>(),
    timeRange: (null, null),
    locations: new List<string>(),
    userName: "test",
    isAdmin: true
);
```

### Pitfall 4: Missing Null Checks

```csharp
// ❌ WRONG: No null check before accessing Data
Assert.IsTrue(result.Data.Rows.Count > 0);

// ✅ CORRECT: Null-safe access
Assert.IsTrue(result.Data != null && result.Data.Rows.Count > 0,
    "Expected non-null DataTable with rows");
```

## File Editing Best Practices

### Avoid File Corruption

When making multiple edits:
- Make one logical change at a time
- Verify each edit compiles before continuing
- If file becomes corrupted, delete and recreate cleanly
- Use specific context in oldString (3-5 lines before/after)

### Replacement String Specificity

```csharp
// ❌ WRONG: Generic replacement that matches multiple locations
oldString: "var result = await Dao_Something.Method();"

// ✅ CORRECT: Include surrounding context
oldString: """
    /// <summary>
    /// Test method description
    /// </summary>
    [TestMethod]
    public async Task TestMethod()
    {
        // Arrange
        var testData = "data";
        
        // Act
        var result = await Dao_Something.Method();
        
        // Assert
"""
```

## Test Data Management

### Use Test Connection String

```csharp
// Always use BaseIntegrationTest helper
var connectionString = GetTestConnectionString();

// Points to: mtm_wip_application_winforms_test (Debug)
//         or mtm_wip_application (Release - production)
```

### Test Data Cleanup

```csharp
[TestMethod]
public async Task CreateItem_ValidData_CreatesItem()
{
    // Arrange
    var testItem = "Test_" + Guid.NewGuid().ToString().Substring(0, 8);
    
    try
    {
        // Act
        var result = await Dao_Item.CreateItemAsync(testItem, ...);
        
        // Assert
        Assert.IsTrue(result.IsSuccess);
    }
    finally
    {
        // Cleanup - optional for test database
        await Dao_Item.DeleteItemAsync(testItem);
    }
}
```

## Test Validation Patterns

### Success Path Validation

```csharp
// Always check IsSuccess first
Assert.IsTrue(result.IsSuccess, 
    $"Expected success, got failure: {result.ErrorMessage}");

// Then validate payload
Assert.IsNotNull(result.Data, "Expected non-null data");
```

### Failure Path Validation

```csharp
// For expected failures
Assert.IsFalse(result.IsSuccess, 
    "Expected failure with invalid input");
Assert.IsNotNull(result.ErrorMessage, 
    "Expected error message");
```

### Collection Validation

```csharp
// DataTable results
Assert.IsNotNull(result.Data, "Expected DataTable");
Assert.IsTrue(result.Data.Rows.Count > 0, 
    "Expected at least one row");

// List results
Assert.IsNotNull(result.Data, "Expected list");
Assert.IsTrue(result.Data.Count > 0,
    "Expected at least one item");
```

## Async/Await Best Practices

### All Tests Must Be Async

```csharp
// ✅ CORRECT: Async test method
[TestMethod]
public async Task GetData_Execution_ReturnsData()
{
    var result = await Dao_Something.GetDataAsync();
    Assert.IsTrue(result.IsSuccess);
}

// ❌ WRONG: Blocking on async
[TestMethod]
public void GetData_Execution_ReturnsData()
{
    var result = Dao_Something.GetDataAsync().Result; // Deadlock risk!
    Assert.IsTrue(result.IsSuccess);
}
```

## Discovery Tool Usage Workflow

### Before Writing Any Test Class

1. **List methods**:
   ```bash
   grep_search(isRegexp=true, 
               includePattern="Data/Dao_Target.cs",
               query="(public|internal).*Task.*\\(")
   ```

2. **Read signatures**:
   ```bash
   read_file(filePath="Data/Dao_Target.cs",
             offset=foundLine, limit=20)
   ```

3. **Document findings**:
   ```csharp
   // At top of test file, document DAO patterns:
   // Dao_Target patterns discovered:
   // - All methods are static
   // - No Async suffix on method names
   // - Returns DaoResult<DataTable> for Get operations
   // - Returns DaoResult<bool> for Exists operations
   ```

4. **Write tests** based on actual signatures

## Performance Considerations

### Keep Tests Fast

- Use existing data for reads when possible
- Minimize database writes in tests
- Consider parallel execution (but ensure isolation)
- Use connection pooling (already configured)

### Connection Management

```csharp
// Connection pooling is automatic via ConnectionString
// No need to manually manage connections in tests
var result = await Dao_Item.GetAllItemsAsync();
// Connection returned to pool automatically
```

## Documentation

### Document Test Purpose

```csharp
/// <summary>
/// Tests that GetAllItems returns the complete item list.
/// Validates: successful execution, non-null DataTable, at least one row.
/// </summary>
[TestMethod]
public async Task GetAllItems_Execution_ReturnsItems()
```

### Document Test Setup

```csharp
/// <summary>
/// Tests item creation with valid data.
/// Setup: Creates unique test item with GUID.
/// Validates: successful creation, item can be retrieved.
/// Cleanup: Deletes test item (optional for test DB).
/// </summary>
```

## Test Execution

### Run Tests via MSTest

```bash
# Run all integration tests
dotnet test --filter "FullyQualifiedName~Integration"

# Run specific test class
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests"

# Run single test
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests.GetAllInventory"
```

### Verify Compilation First

```bash
# Always build before running tests
dotnet build MTM_Inventory_Application.csproj -c Debug

# Check for specific file errors
get_errors(filePaths=["Tests/Integration/Dao_Something_Tests.cs"])
```

## Quality Gates

### Before Committing Integration Tests

- [ ] All method signatures verified via grep_search
- [ ] All tests compile without errors
- [ ] All tests include null safety checks
- [ ] Test naming follows conventions
- [ ] BaseIntegrationTest inheritance confirmed
- [ ] Async/await used correctly throughout
- [ ] XML documentation complete
- [ ] No hardcoded connection strings
- [ ] Test isolation ensured (unique test data)
- [ ] Success/failure paths both tested
