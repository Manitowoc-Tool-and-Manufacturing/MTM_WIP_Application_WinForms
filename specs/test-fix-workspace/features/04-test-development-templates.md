# Feature 4: Test Development Templates

**Created**: 2025-10-19  
**Purpose**: Provide copy-paste ready templates for SQL testing, PowerShell commands, test data setup, and integration test patterns

---

## Feature Overview

Create a collection of practical templates and command references that developers can copy directly while fixing tests. Includes SQL scripts for manual stored procedure testing, PowerShell commands for build/test/database operations, test data setup patterns, and integration test code examples following BaseIntegrationTest standards.

---

## Current Situation

**Problem**: Developers fixing tests need to:
- Manually test stored procedures before writing C# tests
- Remember PowerShell commands for build/test operations
- Create test data setup code from scratch
- Follow integration testing patterns without clear examples

**Impact**: 
- Time wasted searching for command syntax
- Inconsistent test data setup approaches
- Tests that don't follow BaseIntegrationTest conventions
- Manual SQL testing skipped due to complexity

**Opportunity**: Provide ready-to-use templates that enforce best practices and save time.

---

## User Needs

### Primary Users

**Developers fixing integration tests**: Need quick access to commands and patterns for:
- Testing stored procedures manually in MySQL before writing C# tests
- Running specific test categories or individual tests
- Setting up test users and quick button data
- Writing new integration tests following project standards

**QA Engineers validating test fixes**: Need to:
- Manually verify stored procedure behavior
- Understand test data requirements
- Run tests with proper isolation
- Validate cleanup after test runs

**AI Agents generating test code**: Need:
- Integration test templates following BaseIntegrationTest patterns
- Test data setup examples
- Proper async/await patterns for database operations
- Documentation comment examples

---

## What Users Need to Accomplish

### For Manual Stored Procedure Testing

1. **Test procedure quickly**: Copy SQL template, replace parameters, run in MySQL
2. **Verify outputs**: Check p_Status and p_ErrorMsg values
3. **Inspect results**: See actual DataTable-like results before C# test
4. **Test error cases**: Try invalid parameters to verify error handling

### For PowerShell Commands

1. **Build without searching**: Copy exact dotnet build command
2. **Run specific tests**: Copy test filter commands for categories
3. **Database operations**: Copy MySQL connection and query commands
4. **Progress tracking**: Copy commands to update workspace files

### For Test Data Setup

1. **Create test users**: Copy SQL or C# code to insert usr_users records
2. **Set up quick buttons**: Copy pattern for creating quick button test data
3. **Seed multiple records**: Copy batch insert patterns
4. **Clean up data**: Copy DELETE patterns that respect foreign keys

### For Integration Test Writing

1. **Start new test class**: Copy BaseIntegrationTest inheritance template
2. **Write async test methods**: Copy async Task pattern with proper attributes
3. **Handle DaoResult safely**: Copy null-safety pattern for checking results
4. **Add XML documentation**: Copy standard doc comment template

---

## Success Outcomes

### For Developer Efficiency

- Commands copied and run successfully without modification: 90%+
- Time to test stored procedure manually: < 2 minutes (baseline: 5+ minutes)
- Time to write new integration test: < 5 minutes (baseline: 15+ minutes)
- Test data setup code correct on first try: 95%+

### For Test Quality

- All tests follow BaseIntegrationTest patterns: 100%
- Proper null safety on DaoResult.Data: 100%
- Async/await used correctly: 100%
- XML documentation on all test methods: 100%

### For Code Consistency

- Test data setup follows common pattern across all tests
- SQL parameter examples use correct p_ prefix convention
- PowerShell commands use consistent flags and filters
- Integration tests use discovery-first workflow

---

## Template Categories

### 1. SQL Test Templates

File: `reference/sql-templates.md`

#### Stored Procedure Testing Template

```sql
-- Test Stored Procedure: [sp_name]
-- Purpose: [What you're testing]
-- Expected: [Expected outcome]

-- Set up test session variables
SET @p_Status = 0;
SET @p_ErrorMsg = '';

-- Call procedure with test parameters
CALL sp_ProcedureName(
    'TestValue1',          -- p_Parameter1 (string)
    123,                   -- p_Parameter2 (int)
    '2025-01-01',         -- p_Parameter3 (date)
    @p_Status,            -- OUT p_Status
    @p_ErrorMsg           -- OUT p_ErrorMsg
);

-- Check output parameters
SELECT @p_Status AS Status, @p_ErrorMsg AS ErrorMsg;

-- Verify results (if procedure returns data)
-- Results appear above the SELECT statement

-- Clean up (if needed)
-- DELETE FROM table WHERE condition;
```

#### Quick Button Test Data Setup

```sql
-- Create test users for quick button tests
INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
VALUES 
    ('TEST-USER', 'Test User', 'hash', 1, NOW()),
    ('TEST-USER-2', 'Test User 2', 'hash', 1, NOW())
ON DUPLICATE KEY UPDATE UserID = UserID; -- Prevent errors if already exists

-- Create test quick buttons
INSERT INTO sys_quick_buttons (
    ButtonID, UserID, ButtonText, Operation, Location, 
    PartNumber, Position, IsActive
)
VALUES
    (NULL, 'TEST-USER', 'Test Button 1', '100', 'FLOOR', 'PART-001', 1, 1),
    (NULL, 'TEST-USER', 'Test Button 2', '100', 'FLOOR', 'PART-002', 2, 1),
    (NULL, 'TEST-USER-2', 'Test Button 3', '110', 'SHIPPING', 'PART-003', 1, 1);

-- Verify insertion
SELECT * FROM sys_quick_buttons WHERE UserID LIKE 'TEST-USER%';

-- Clean up (run after tests)
DELETE FROM sys_quick_buttons WHERE UserID LIKE 'TEST-USER%';
DELETE FROM usr_users WHERE UserID LIKE 'TEST-USER%';
```

#### Test User Creation

```sql
-- Create test users with proper structure
INSERT INTO usr_users (
    UserID,
    UserName,
    PasswordHash,
    IsActive,
    IsAdmin,
    CreatedDate,
    LastLoginDate
)
VALUES
    ('TEST-USER', 'Test User', SHA2('password', 256), 1, 0, NOW(), NULL),
    ('TEST-ADMIN', 'Test Admin', SHA2('password', 256), 1, 1, NOW(), NULL)
ON DUPLICATE KEY UPDATE UserID = UserID;

-- Verify users exist
SELECT UserID, UserName, IsActive, IsAdmin FROM usr_users WHERE UserID LIKE 'TEST-%';
```

---

### 2. PowerShell Commands

File: `reference/powershell-commands.md`

#### Build Commands

```powershell
# Clean solution
dotnet clean

# Restore dependencies
dotnet restore

# Build Debug (uses test database)
dotnet build MTM_Inventory_Application.csproj -c Debug

# Build Release (uses production database)
dotnet build MTM_Inventory_Application.csproj -c Release

# Build and suppress warnings for specific codes
dotnet build -c Debug /p:NoWarn=CS1591,CS8618
```

#### Test Commands

```powershell
# Run ALL integration tests
dotnet test --filter "FullyQualifiedName~Integration"

# Run specific DAO test class
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"

# Run single test method
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests.AddQuickButton_ValidData_InsertsButton"

# Run tests with detailed output
dotnet test --filter "FullyQualifiedName~Integration" --logger "console;verbosity=detailed"

# Run tests and collect coverage (if configured)
dotnet test --filter "FullyQualifiedName~Integration" --collect:"XPlat Code Coverage"

# Run category of tests (Quick Buttons)
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"

# Run category of tests (System DAO)
dotnet test --filter "FullyQualifiedName~Dao_System_Tests"

# Run category of tests (Helper & Validation)
dotnet test --filter "FullyQualifiedName~Helper_Tests|FullyQualifiedName~Validation_Tests"
```

#### Database Commands

```powershell
# Connect to test database (PowerShell with MySQL module)
$connectionString = "Server=localhost;Port=3306;Database=mtm_wip_application_winforms_test;User=root;Password=root;SslMode=none;"

# Execute SQL file
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test < script.sql

# Export query results to CSV
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT * FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%'" > results.csv

# Count test records
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT COUNT(*) FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%'"

# Clean up test data
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "DELETE FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%'"
```

#### Workspace Commands

```powershell
# Update progress after fixing tests
.\tools\update-progress.ps1

# Generate dashboard from category files
.\tools\generate-dashboard.ps1

# Run specific category tests
.\tools\run-category-tests.ps1 -Category "QuickButtons"

# Dry run mode (test without changes)
.\tools\update-progress.ps1 -WhatIf
```

---

### 3. Integration Test Patterns

File: `reference/test-patterns.md`

#### Test Class Template

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Models;
using System.Threading.Tasks;
using System.Data;

namespace MTM_Inventory_Application.Tests.Integration
{
    /// <summary>
    /// Integration tests for Dao_[EntityName] class.
    /// Tests stored procedure execution through DAO wrapper methods.
    /// </summary>
    [TestClass]
    public class Dao_[EntityName]_Tests : BaseIntegrationTest
    {
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
        /// </summary>
        [TestMethod]
        public async Task Create[Entity]_ValidData_CreatesEntity()
        {
            // Arrange
            var testId = "TEST-" + Guid.NewGuid().ToString().Substring(0, 8);
            
            // Act
            var createResult = await Dao_[Entity].Create[Entity]Async(testId, ...);
            
            // Assert
            Assert.IsTrue(createResult.IsSuccess, 
                $"Create failed: {createResult.ErrorMessage}");
            
            // Verify - can retrieve created entity
            var getResult = await Dao_[Entity].Get[Entity]Async(testId);
            Assert.IsTrue(getResult.IsSuccess && getResult.Data != null, 
                "Should be able to retrieve created entity");
            
            // Cleanup (optional for test DB)
            await Dao_[Entity].Delete[Entity]Async(testId);
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
                "Expected entity to exist");
        }

        #endregion
    }
}
```

#### Discovery-First Workflow Template

```csharp
// BEFORE writing tests, discover actual method signatures:

// 1. Use grep_search to find all methods
grep_search(
    isRegexp: true,
    includePattern: "Data/Dao_[Entity].cs",
    query: "(public|internal) (static )?.*Task.*\\("
)

// 2. Read actual signatures
read_file(
    filePath: "Data/Dao_[Entity].cs",
    offset: [lineNumber],
    limit: 30
)

// 3. Document findings at top of test file:
// Dao_[Entity] patterns discovered:
// - All methods are [static/instance]
// - [No/Has] Async suffix on method names
// - Returns DaoResult<DataTable> for Get operations
// - Returns DaoResult<bool> for Exists operations
// - Parameter names: [list actual parameters]

// 4. THEN write tests based on actual signatures
```

#### Null-Safe DaoResult Pattern

```csharp
// ❌ WRONG: Assumes Data is non-null
var result = await Dao_Something.GetDataAsync();
Assert.IsTrue(result.IsSuccess && result.Data.Rows.Count > 0);

// ✅ CORRECT: Null-safe check
var result = await Dao_Something.GetDataAsync();
Assert.IsTrue(result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0,
    "Expected successful result with non-null DataTable and rows");

// For DaoResult<bool>
Assert.IsTrue(result.IsSuccess && result.Data, 
    "Expected successful result with Data = true");

// For DaoResult<DataRow>
Assert.IsTrue(result.IsSuccess && result.Data != null,
    "Expected successful result with non-null DataRow");

// For DaoResult (no type parameter)
Assert.IsTrue(result.IsSuccess, 
    $"Expected success, got failure: {result.ErrorMessage}");
```

#### Test Data Setup Pattern

```csharp
/// <summary>
/// Creates test user records for integration tests.
/// Called in TestInitialize or individual test setup.
/// </summary>
private async Task CreateTestUsersAsync()
{
    var connectionString = GetTestConnectionString();
    
    var sql = @"
        INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
        VALUES 
            ('TEST-USER', 'Test User', SHA2('password', 256), 1, NOW()),
            ('TEST-ADMIN', 'Test Admin', SHA2('password', 256), 1, NOW())
        ON DUPLICATE KEY UPDATE UserID = UserID;
    ";
    
    // Execute using Helper_Database_StoredProcedure or direct MySqlCommand
    // Implementation depends on project helpers
}

/// <summary>
/// Cleans up test data after tests complete.
/// Called in TestCleanup or individual test teardown.
/// </summary>
private async Task CleanupTestDataAsync()
{
    var connectionString = GetTestConnectionString();
    
    var sql = @"
        DELETE FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%';
        DELETE FROM usr_users WHERE UserID LIKE 'TEST-%';
    ";
    
    // Execute cleanup
}
```

---

### 4. Test Data Setup Examples

Include in `reference/test-patterns.md`:

#### Creating Test Users with Proper Foreign Keys

```csharp
// Pattern: Create parent records first, then dependent records

// 1. Create test users
await CreateTestUsersAsync();

// 2. Create dependent quick buttons
var quickButtonSql = @"
    INSERT INTO sys_quick_buttons (UserID, ButtonText, Operation, Location, Position)
    VALUES 
        ('TEST-USER', 'Test Button 1', '100', 'FLOOR', 1),
        ('TEST-USER', 'Test Button 2', '100', 'FLOOR', 2);
";

// 3. Run tests

// 4. Clean up in reverse order (children first, then parents)
await CleanupQuickButtonsAsync();
await CleanupTestUsersAsync();
```

#### Using BaseIntegrationTest Helpers

```csharp
[TestClass]
public class Dao_Example_Tests : BaseIntegrationTest
{
    [TestInitialize]
    public async Task TestSetup()
    {
        // BaseIntegrationTest provides GetTestConnectionString()
        var connString = GetTestConnectionString();
        
        // Set up test data here
        await CreateTestDataAsync();
    }
    
    [TestCleanup]
    public async Task TestTeardown()
    {
        // Clean up test data
        await CleanupTestDataAsync();
    }
}
```

---

## Special Requirements

### Copy-Paste Ready

All templates must be:
- Runnable with minimal editing (replace bracketed placeholders only)
- Include all necessary using statements
- Have correct syntax (no compilation errors)
- Use actual project namespaces and class names

### Real Examples

- SQL templates use actual table names from MTM schema
- C# templates use actual DAO class names from Data/ folder
- PowerShell commands use actual project file names
- Parameter examples match real stored procedure signatures

### Comments Included

- Every template section has comments explaining purpose
- Placeholders clearly marked with [brackets]
- Gotchas and common mistakes noted
- Expected outcomes documented

### Version Specific

- .NET 8 C# syntax
- MySQL 5.7 SQL syntax
- PowerShell 7+ commands
- MSTest framework attributes

---

## Out of Scope

This feature does NOT include:
- Actually running the templates (just documentation)
- Creating automation to apply templates
- Template generation tools
- Custom template creation for new scenarios
- Template validation or testing

Only provides **ready-to-use template documentation**.

---

## Dependencies

**Depends on**: Feature 1 (Workspace Foundation) - needs reference/ folder

**Depended on by**: Feature 2 (Test Category Tracking) - category files link to templates

**Related to**:
- .github/instructions/integration-testing.instructions.md
- BaseIntegrationTest.cs class
- Helper_Database_StoredProcedure.cs

---

## Assumptions

- Developers have MySQL command-line client available
- .NET 8 SDK installed for dotnet commands
- PowerShell 7+ available for script execution
- Test database (mtm_wip_application_winforms_test) is accessible
- BaseIntegrationTest class patterns are stable

---

## Acceptance Criteria

### SQL Templates Complete
- [ ] Stored procedure testing template with parameter examples
- [ ] Quick button test data setup SQL
- [ ] Test user creation SQL
- [ ] Data cleanup SQL (respecting foreign keys)
- [ ] All SQL uses actual MTM table names

### PowerShell Commands Complete
- [ ] Build commands (clean, restore, build Debug/Release)
- [ ] Test commands (all tests, category, single test)
- [ ] Database commands (connect, query, cleanup)
- [ ] Workspace commands (progress update, dashboard)
- [ ] All commands tested and work correctly

### Integration Test Patterns Complete
- [ ] Test class template with BaseIntegrationTest inheritance
- [ ] Discovery-first workflow template
- [ ] Null-safe DaoResult pattern examples
- [ ] Test data setup pattern
- [ ] Async/await pattern examples
- [ ] XML documentation template

### Template Quality
- [ ] All templates copy-pasteable with minimal editing
- [ ] Placeholders clearly marked with [brackets]
- [ ] Comments explain purpose and expected outcomes
- [ ] No syntax errors in templates
- [ ] Real examples use actual project names

### Usability
- [ ] Developer can find relevant template in under 1 minute
- [ ] Templates work without modification (except placeholders)
- [ ] Common mistakes documented as warnings
- [ ] Expected outcomes clearly stated

---

## Success Metrics

**Template Usage**:
- Templates work without modification: 90%+ (only need placeholder replacement)
- Time to start manual SP test: < 2 minutes (baseline: 5+ minutes)
- Time to write new integration test: < 5 minutes (baseline: 15+ minutes)

**Code Quality**:
- Integration tests following patterns: 100%
- Null safety on DaoResult.Data: 100%
- Proper async/await usage: 100%
- XML documentation on tests: 100%

**Documentation Quality**:
- Template files under 500 lines each
- No syntax errors in templates
- All templates include comments
- Cross-references to related docs

---

## Notes for /speckit.specify

This feature provides **practical templates and command references** for developers fixing tests. All content is based on existing project patterns and tested commands.

**No clarifications needed** - templates are derived from existing code patterns and documented best practices in instruction files.

Can be implemented early as reference material that test-fixing work will use.
