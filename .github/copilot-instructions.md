# GitHub Copilot Instructions - MTM WIP Application

> **Auto-generated** from codebase analysis following copilot-instructions-blueprint-generator.prompt.md  
> **Last Updated**: 2025-11-12  
> **Version**: 1.0.0

## Priority Guidelines

When generating code for this repository:

1. **Version Compatibility**: Always use .NET 8.0, C# 12.0, and WinForms patterns. Never suggest features beyond these versions.
2. **Context Files**: Prioritize patterns and standards defined in the .github/instructions directory
3. **Codebase Patterns**: When context files don't provide specific guidance, scan existing code for established patterns
4. **Architectural Consistency**: Maintain layered architecture with strict separation between Data, Services, Forms, Controls, and Helpers
5. **Code Quality**: Prioritize maintainability, testability, and centralized error handling in all generated code

## Technology Stack (EXACT VERSIONS - DO NOT EXCEED)

### Core Platform
- **.NET**: 8.0-windows (`<TargetFramework>net8.0-windows</TargetFramework>`)
- **C# Language**: 12.0 (implicit with .NET 8.0)
- **WinForms**: .NET 8.0 Windows Forms
- **Nullable**: Enabled (`<Nullable>enable</Nullable>`)
- **Implicit Usings**: Enabled (`<ImplicitUsings>enable</ImplicitUsings>`)

### Key Dependencies
- **MySQL.Data**: 9.4.0 → MySQL Server 5.7.24 (LEGACY - NO 8.0 features!)
- **Microsoft.Extensions.DependencyInjection**: 8.0.0
- **Microsoft.Extensions.Logging**: 8.0.0
- **ClosedXML**: 0.105.0 (Excel export)
- **Microsoft.Web.WebView2**: 1.0.2792.45

### Critical MySQL Constraint
**MySQL Server**: 5.7.24 (LEGACY VERSION)

**Forbidden MySQL 8.0+ Features:**
- ❌ JSON functions
- ❌ Common Table Expressions (CTEs)
- ❌ Window functions
- ❌ CHECK constraints

## Architecture & Folder Structure

```
MTM_WIP_Application_WinForms/
├── Core/                   # Core utilities and theme system
│   ├── Theming/           # NEW: Dependency injection-based theme system
│   └── *.cs               # Core utilities
├── Data/                   # DAOs - ONLY use Helper_Database_StoredProcedure
├── Services/               # Business logic services
├── Helpers/                # Helper utilities
│   └── Helper_Database_*  # REQUIRED for ALL database access
├── Forms/                  # WinForms forms (inherit from ThemedForm)
│   └── Shared/            # ThemedForm, ThemedUserControl base classes
├── Controls/               # User controls (inherit from ThemedUserControl)
├── Models/                 # Data models, enums
│   └── Model_Dao_Result.cs # REQUIRED return type for ALL DAO methods
├── Logging/                # LoggingUtility (centralized CSV logging)
└── Database/               # Stored procedures ONLY
```

### Architectural Boundaries (NON-NEGOTIABLE)

1. **Forms → DAOs → Database** (never skip layers)
2. **NO direct database access in Forms** (go through DAOs)
3. **DAOs ONLY call Helper_Database_StoredProcedure** (no MySqlConnection)
4. **ALL database access uses stored procedures** (NO inline SQL)
5. **ALL errors use Service_ErrorHandler** (NEVER MessageBox.Show)
6. **ALL logging uses LoggingUtility** (structured CSV format)

## Naming Conventions

### Classes
- Forms: `{Feature}Form` → `SettingsForm`, `PrintForm`
- Controls: `Control_{Feature}_{Purpose}` → `Control_QuickButtons`
- DAOs: `Dao_{Entity}` → `Dao_Inventory`, `Dao_QuickButtons`
- Services: `Service_{Purpose}` → `Service_ErrorHandler`
- Helpers: `Helper_{Purpose}` → `Helper_Database_StoredProcedure`
- Models: `Model_{Scope}_{Entity}` → `Model_Shared_UserUiColors`
- Enums: `Enum_{Name}` → `Enum_ErrorSeverity`

### Form Controls
`{FormName}_{ControlType}_{Name}_{Number?}`
- Example: `MainForm_MenuStrip_File`, `SettingsForm_Button_Save_1`

### Methods
- Async methods: `{Action}Async` → `LoadThemesAsync`, `SaveSettingsAsync`
- DAO methods: Descriptive verbs → `GetAllQuickButtonsAsync`, `InsertErrorReportAsync`

### Variables
- Private fields: `_camelCase` → `_logger`, `_themeProvider`
- Parameters: `camelCase` → `userId`, `themeName`
- Constants: `SCREAMING_SNAKE_CASE` or `PascalCase` for public

## Code Organization (MANDATORY #region Structure)

Every C# file MUST have #region blocks in this exact order:

```csharp
#region Fields
// Private fields, constants
#endregion

#region Properties
// Public and private properties
#endregion

#region Constructors
// Constructors (public first, then private)
#endregion

#region Methods
// Public → protected → private → static
#endregion

#region Events
// Event handlers
#endregion

#region Helpers
// Private helper methods
#endregion

#region Cleanup / Dispose
// IDisposable implementation
#endregion
```

## Core Patterns (Follow These EXACTLY)

### DAO Pattern (MANDATORY for all database access)

```csharp
public class Dao_Entity
{
    /// <summary>
    /// Gets all entities from database.
    /// </summary>
    /// <returns>Model_Dao_Result with DataTable of entities.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetAllAsync()
    {
        return await Helper_Database_StoredProcedure
            .ExecuteDataTableWithStatusAsync(
                "md_entity_GetAll",  // Stored procedure name
                null);                // No parameters
    }
    
    /// <summary>
    /// Inserts a new entity.
    /// </summary>
    /// <param name="name">Entity name</param>
    /// <returns>Model_Dao_Result with success/failure.</returns>
    public async Task<Model_Dao_Result<bool>> InsertAsync(string name)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Name", name }  // NO p_ prefix in C# (only in SQL)
        };
        
        return await Helper_Database_StoredProcedure
            .ExecuteNonQueryWithStatusAsync(
                "md_entity_Insert",
                parameters);
    }
}
```

### Error Handling Pattern (NON-NEGOTIABLE)

```csharp
// ✅ CORRECT: Use Service_ErrorHandler
try
{
    var result = await dao.GetDataAsync();
    if (!result.IsSuccess)
    {
        Service_ErrorHandler.ShowUserError(result.ErrorMessage);
        return;
    }
}
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(
        ex,
        Enum_ErrorSeverity.Medium,
        contextData: new Dictionary<string, object>
        {
            ["User"] = Model_Application_Variables.User,
            ["Operation"] = "GetData"
        },
        callerName: nameof(GetDataAsync),
        controlName: this.Name);
}

// ❌ WRONG: Never use MessageBox.Show directly
catch (Exception ex)
{
    MessageBox.Show(ex.Message); // FORBIDDEN!
}
```

### Model_Dao_Result Pattern

```csharp
// Always check IsSuccess before accessing Data
var result = await dao.GetDataAsync();
if (result.IsSuccess)
{
    var data = result.Data; // Safe to access
}
else
{
    // ErrorMessage contains user-friendly message
    Service_ErrorHandler.ShowUserError(result.ErrorMessage);
}
```

### Theme Integration Pattern

```csharp
// ALL forms must inherit from ThemedForm
public partial class MyForm : ThemedForm
{
    public MyForm()
    {
        InitializeComponent();
        // Theme automatically applied by ThemedForm base class
        // NO manual color setting needed
    }
}

// ALL user controls must inherit from ThemedUserControl
public partial class MyControl : ThemedUserControl
{
    public MyControl()
    {
        InitializeComponent();
        // Theme automatically applied
    }
}
```

## XML Documentation (MANDATORY for ALL public members)

```csharp
/// <summary>
/// Brief description of what this does (one line preferred).
/// </summary>
/// <param name="paramName">Description of parameter.</param>
/// <returns>
/// Description of return value.
/// For Model_Dao_Result: Check IsSuccess before accessing Data.
/// ErrorMessage contains user-friendly message on failure.
/// </returns>
/// <exception cref="ExceptionType">When this exception is thrown.</exception>
/// <remarks>
/// Additional notes, usage examples, or important details.
/// </remarks>
```

**Required Tags:**
- `<summary>`: ALWAYS for public members
- `<param>`: For each parameter
- `<returns>`: If method returns a value
- `<exception>`: Document thrown exceptions
- `<remarks>`: Optional for complex scenarios

**NO inline comments** (`//` or `/* */`) except for:
- Non-obvious business logic
- Workarounds for known issues
- Complex algorithms

## Testing

### Integration Testing (xUnit 2.6.2)

```csharp
public class Dao_EntityTests : BaseIntegrationTest
{
    [Fact]
    public async Task MethodName_ValidInput_ReturnsSuccess()
    {
        // Arrange
        var dao = new Dao_Entity();
        var input = "test";
        
        // Act
        var result = await dao.MethodAsync(input);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
    }
}
```

**Test Database**: `mtm_wip_application_winforms_test`  
**Base Class**: `BaseIntegrationTest` (provides transaction management, auto-rollback)

### MySQL Database Access

**MAMP MySQL Server Configuration:**
- **Host**: localhost
- **Port**: 3306
- **Username**: root
- **Password**: root
- **MySQL Binary**: `C:\MAMP\bin\mysql\bin\mysql.exe`

**Database Names:**
- **Production**: `mtm_wip_application_winforms` (24 tables)
- **Testing**: `mtm_wip_application_winforms_test` (20 tables)

**Quick Access Commands:**
```powershell
# Connect to production database
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms

# Connect to test database
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test

# Show all databases
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot -e "SHOW DATABASES;"

# Show tables in production
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "SHOW TABLES;"

# Execute SQL file
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms < path/to/script.sql
```

## Constitution Compliance

This project follows the "MTM WIP Application Constitution" (.specify/memory/constitution.md):

1. ✅ **Centralized Error Handling** - Service_ErrorHandler only
2. ✅ **Structured Logging** - CSV format via LoggingUtility
3. ✅ **Model_Dao_Result Pattern** - All DAO methods return this
4. ✅ **Async-First** - All I/O operations must be async
5. ✅ **Stored Procedures** - NO direct SQL in code
6. ✅ **WinForms Best Practices** - InvokeRequired, disposal, theme integration

## When in Doubt

1. Search for similar code in the codebase
2. Check .github/instructions/ for specific guidance
3. Review specs/*/tasks.md for implementation examples
4. Prioritize consistency over external best practices
5. Follow the constitution - it's non-negotiable

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
