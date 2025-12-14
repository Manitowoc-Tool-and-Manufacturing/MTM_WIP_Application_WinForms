# GitHub Copilot Instructions - MTM WIP Application

> **Auto-generated** from codebase analysis following copilot-instructions-blueprint-generator.prompt.md  
> **Last Updated**: 2025-12-06  
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

## Dependency Injection Strategy (Hybrid Approach)

**Policy**:
- **Legacy DAOs**: Keep as `static` classes (non-injected). Do NOT refactor existing static DAOs to DI unless explicitly requested.
- **New Components**: All NEW Services, DAOs, and Forms MUST be designed for Dependency Injection.
  - Use Interfaces (`IUserService`, `IInventoryDao`).
  - Register in `Program.cs` or `Startup.cs`.
  - Inject via constructor.

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

**Note**: Existing DAOs are `static`. New DAOs must be instance-based and implement an interface for DI.

```csharp
// Interface for DI (New DAOs)
public interface IDao_Entity
{
    Task<Model_Dao_Result<DataTable>> GetAllAsync();
    Task<Model_Dao_Result<bool>> InsertAsync(string name);
}

// Implementation
public class Dao_Entity : IDao_Entity
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

## Serena Semantic Tools Integration

**Serena** is a semantic coding toolkit providing IDE-like capabilities through Language Server Protocol (LSP). It's essential for efficient work on the MTM codebase (300+ files).

### Decision Tree: Serena vs. Standard Tools

```
Task involves code navigation/editing?
├─ YES: Multiple files (3+) OR finding usages OR architectural validation?
│  ├─ YES: ✅ USE SERENA (80-90% token savings)
│  │  └─ Examples: Exploring DAOs, finding MessageBox.Show, refactoring methods
│  └─ NO: Single-line edit in known location?
│     ├─ YES: ❌ Use standard edit tools
│     └─ NO: ✅ USE SERENA (precision + token efficiency)
│        └─ Examples: Modifying method, adding DAO method, symbol-level edits
└─ NO: Creating new file from scratch?
   └─ YES: ❌ Use standard file creation
      └─ Note: Serena for reading/exploring, standard tools for creation
```

### When to Use Serena (MTM Context)

#### ✅ ALWAYS Use Serena For:
1. **Multi-DAO exploration** - Understanding 20+ DAO files
2. **Architectural validation** - Finding `MessageBox.Show`, direct `MySqlConnection`, missing XML docs
3. **Pre-refactoring analysis** - Finding all usages of a method before changing signature
4. **Theme system exploration** - Finding all `ThemedForm` / `ThemedUserControl` implementations
5. **Stored procedure mapping** - Tracing SP → DAO → Service → Form chains
6. **Service_ErrorHandler migration** - Replacing error handling patterns
7. **#region compliance** - Checking file organization
8. **Token efficiency** - When context window is limited (>80% savings)

#### ❌ Don't Use Serena For:
1. **Single-line edits** - Changing one variable name in known location
2. **New file creation** - Creating Forms, Controls, Models from scratch
3. **Non-code files** - Editing XML, JSON, config files
4. **Simple reads** - Reading small files (<100 lines) once

### Core Serena Tools for MTM

#### Symbol Navigation
```csharp
// 1. Get file structure (ALWAYS first step)
get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
// Returns: Class + all method signatures (NO bodies)
// Token usage: ~200-300 tokens (vs ~5,000 for full file)

// 2. Read specific method
find_symbol("Dao_Inventory/GetAllAsync", include_body=true)
// Returns: Just this method's code
// Token usage: ~100-200 tokens

// 3. Find all usages (BEFORE modifying)
find_referencing_symbols("GetAllAsync", "Data/Dao_Inventory.cs")
// Returns: All call sites with context
// Critical before breaking changes!
```

#### Symbol Editing
```csharp
// 1. Replace entire method
replace_symbol_body(
    name_path="Dao_Inventory/GetAllAsync",
    relative_path="Data/Dao_Inventory.cs",
    body=complete_new_method_with_xml_docs
)

// 2. Insert new method after existing
insert_after_symbol(
    name_path="Dao_Inventory/GetAllAsync",
    relative_path="Data/Dao_Inventory.cs",
    body=new_method_with_xml_docs
)

// 3. Rename across entire codebase
rename_symbol(
    name_path="GetAllAsync",
    relative_path="Data/Dao_Inventory.cs",
    new_name="GetAllInventoryAsync"
)
// Updates ALL references automatically via LSP
```

#### Architectural Validation
```csharp
// Find anti-patterns
search_for_pattern(
    substring_pattern="MessageBox\\.Show",
    restrict_search_to_code_files=true,
    context_lines_before=2,
    context_lines_after=2
)

// Find compliance patterns
search_for_pattern(
    substring_pattern="Helper_Database_StoredProcedure\\.Execute",
    relative_path="Data"
)
```

### Serena + MTM Architectural Patterns

#### Pattern 1: DAO Exploration & Validation

**Scenario:** Understand new DAO and verify architectural compliance.

```
Workflow:
1. get_symbols_overview("Data/Dao_Entity.cs", depth=1)
   → See all methods (200 tokens)

2. Verify Model_Dao_Result usage:
   search_for_pattern("Task<Model_Dao_Result", relative_path="Data/Dao_Entity.cs")
   → Should match all public methods

3. Verify Helper_Database_StoredProcedure usage:
   search_for_pattern("Helper_Database_StoredProcedure", relative_path="Data/Dao_Entity.cs")
   → Should be present (no direct MySqlConnection)

4. Check specific method implementation:
   find_symbol("Dao_Entity/GetAllAsync", include_body=true)
   → Verify stored procedure pattern (100 tokens)

Total: ~500 tokens (vs ~5,000 reading full file)
```

#### Pattern 2: Service_ErrorHandler Migration

**Scenario:** Replace all `MessageBox.Show` with `Service_ErrorHandler`.

```
Workflow:
1. Find all violations:
   search_for_pattern("MessageBox\\.Show", restrict_search_to_code_files=true)
   → Returns list of files + line numbers

2. For each violation:
   a. Read method context:
      find_symbol("ClassName/MethodName", include_body=true)
   
   b. Replace with correct pattern:
      replace_symbol_body(
          name_path="ClassName/MethodName",
          relative_path="path/to/file.cs",
          body=method_with_service_error_handler
      )

3. Verify fix:
   search_for_pattern("MessageBox\\.Show")
   → Should return 0 results
```

#### Pattern 3: Multi-File Refactoring

**Scenario:** Change DAO method signature affecting 10+ Forms.

```
Workflow:
1. Find all usages FIRST:
   find_referencing_symbols("GetAllAsync", "Data/Dao_Inventory.cs")
   → Returns all 10+ call sites with code snippets

2. Document impact:
   - Write down all affected files
   - Note each calling context

3. Update DAO method:
   replace_symbol_body(
       name_path="Dao_Inventory/GetAllAsync",
       relative_path="Data/Dao_Inventory.cs",
       body=updated_method_with_new_parameter
   )

4. Update each caller (from step 1 list):
   For each caller:
   find_symbol("FormName/MethodName", include_body=true)
   replace_symbol_body(
       name_path="FormName/MethodName",
       relative_path="Forms/.../FormName.cs",
       body=updated_caller
   )

5. Build verification:
   execute_shell_command("dotnet build MTM_WIP_Application_Winforms.csproj")
   → Verify no compilation errors
```

#### Pattern 4: Theme System Integration Check

**Scenario:** Verify all Forms inherit from `ThemedForm`.

```
Workflow:
1. Find all Form classes:
   search_for_pattern(
       substring_pattern="class \\w+Form : (Form|ThemedForm)",
       relative_path="Forms"
   )

2. Identify violations (inheriting from Form, not ThemedForm)

3. For each violation:
   get_symbols_overview("Forms/.../ViolatingForm.cs", depth=0)
   → Understand form structure
   
   replace_symbol_body(
       name_path="ViolatingForm",
       relative_path="Forms/.../ViolatingForm.cs",
       body=class_inheriting_from_themed_form
   )

4. Verify fix:
   search_for_pattern("class \\w+Form : Form[^T]", relative_path="Forms")
   → Should return 0 results
```

### Gemini-Specific Optimizations

#### Gemini Context Management

**Gemini Advantage:** 2M token context window (vs 200K for GPT-4)

**Strategy:**
1. **Use Serena for exploration phase** (reduce tokens 80-90%)
2. **Keep full context for editing phase** (Gemini can handle it)
3. **Use memories for recurring patterns** (load once per session)

```
# Start of conversation:
activate_project("MTM_WIP_Application")
read_memory("architectural_patterns.md")  # Load once
read_memory("dao_best_practices.md")      # Load once

# Then use Serena for efficient exploration:
get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
find_symbol("Dao_Inventory/GetAllAsync", include_body=true)
# ... more targeted reads

# Gemini can hold ALL this in context without summarization
```

#### Gemini Function Calling Best Practices

**Parallel Tool Calls:**
Gemini supports parallel function calling. Use it for batch operations:

```
# ✅ Efficient: Parallel reads
Batch call:
- get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
- get_symbols_overview("Data/Dao_User.cs", depth=1)
- get_symbols_overview("Data/Dao_Transactions.cs", depth=1)

# All execute simultaneously, results returned together

# ❌ Inefficient: Sequential reads
Call 1: get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
Wait for result...
Call 2: get_symbols_overview("Data/Dao_User.cs", depth=1)
Wait for result...
# 3x slower
```

**Thinking Before Acting:**
Use Serena's thinking tools to organize complex tasks:

```
# After gathering info:
think_about_collected_information()
# Gemini reflects: Is this enough? What's missing?

# Before major changes:
think_about_task_adherence()
# Gemini verifies: Still solving the right problem?

# Before reporting done:
think_about_whether_you_are_done()
# Gemini checks: All requirements met?
```

### Tool Chaining Examples

#### Chain 1: Explore → Validate → Fix
```
Step 1 (Explore):
get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
→ See all methods

Step 2 (Validate):
search_for_pattern("MessageBox\\.Show", relative_path="Data/Dao_Inventory.cs")
→ Find anti-patterns

Step 3 (Fix if found):
find_symbol("Dao_Inventory/MethodWithViolation", include_body=true)
→ Read method

replace_symbol_body(
    name_path="Dao_Inventory/MethodWithViolation",
    relative_path="Data/Dao_Inventory.cs",
    body=corrected_method
)
→ Fix violation
```

#### Chain 2: Find Usages → Modify → Update Callers
```
Step 1 (Impact Analysis):
find_referencing_symbols("MethodName", "Data/Dao_Entity.cs")
→ Find all 5 call sites

Step 2 (Modify):
replace_symbol_body(
    name_path="Dao_Entity/MethodName",
    relative_path="Data/Dao_Entity.cs",
    body=modified_method
)
→ Change method

Step 3 (Update Callers - parallel if possible):
For each of 5 callers:
  find_symbol("Caller/CallerMethod", include_body=true)
  replace_symbol_body(...)
→ Update all call sites

Step 4 (Verify):
execute_shell_command("dotnet build")
→ Ensure compilation success
```

#### Chain 3: Memory-Driven Development
```
Step 1 (Load Context):
activate_project("MTM_WIP_Application")
read_memory("architectural_patterns.md")
→ Load coding standards once

Step 2 (Work):
get_symbols_overview(...)
find_symbol(...)
replace_symbol_body(...)
→ Make changes following patterns

Step 3 (Save Learnings):
write_memory(
    memory_file_name="refactoring_completed_2025-12-14.md",
    content=summary_of_work_done
)
→ Document for next conversation
```

### Token Efficiency Examples

**Example 1: DAO Exploration**
```
❌ Without Serena (reading full files):
read_file("Data/Dao_Inventory.cs", 1, 500)     # 5,000 tokens
read_file("Data/Dao_User.cs", 1, 400)          # 4,000 tokens
read_file("Data/Dao_Transactions.cs", 1, 600)  # 6,000 tokens
Total: 15,000 tokens

✅ With Serena (symbol-level reads):
get_symbols_overview("Data/Dao_Inventory.cs", depth=1)      # 250 tokens
get_symbols_overview("Data/Dao_User.cs", depth=1)           # 200 tokens
get_symbols_overview("Data/Dao_Transactions.cs", depth=1)   # 300 tokens
find_symbol("Dao_Inventory/GetAllAsync", include_body=true) # 150 tokens
find_symbol("Dao_User/GetByIdAsync", include_body=true)     # 120 tokens
Total: 1,020 tokens

Savings: 93%
```

**Example 2: Refactoring Impact Analysis**
```
❌ Without Serena (grep + read):
grep_search("GetAllAsync")                      # 2,000 tokens
read_file("Forms/MainForm.cs", 1, 800)         # 8,000 tokens
read_file("Forms/SettingsForm.cs", 1, 600)     # 6,000 tokens
... (8 more files)
Total: 50,000+ tokens

✅ With Serena (reference discovery):
find_referencing_symbols("GetAllAsync", "Data/Dao_Inventory.cs")  # 1,500 tokens
→ Returns all 10 call sites with code snippets
Total: 1,500 tokens

Savings: 97%
```

### Quick Reference: Most-Used Serena Tools

| Tool | Use Case | Token Savings |
|------|----------|---------------|
| `get_symbols_overview` | Explore file structure | 95% |
| `find_symbol` | Read specific method | 90% |
| `find_referencing_symbols` | Find usages before refactoring | 90% |
| `replace_symbol_body` | Update method precisely | N/A |
| `search_for_pattern` | Find architectural violations | Variable |
| `read_memory` | Load project knowledge | 100% |

**Full Documentation**: `.github/instructions/serena-semantic-tools.instructions.md`  
**Serena Official Docs**: https://oraios.github.io/serena/

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
