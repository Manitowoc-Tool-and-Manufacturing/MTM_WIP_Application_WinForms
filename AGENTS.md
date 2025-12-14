# AGENTS.md

## Project Overview

**MTM WIP Application** is a Windows Forms-based inventory management system for Manitowoc Tool and Manufacturing. Built on .NET 8.0 with C# 12.0, it provides real-time work-in-progress tracking, transaction management, and reporting capabilities with MySQL 5.7.24 backend.

**Architecture**: Layered architecture with strict separation:
- **Data Layer**: DAOs using stored procedures only (via `Helper_Database_StoredProcedure`)
- **Service Layer**: Business logic and error handling (`Service_ErrorHandler`, logging)
- **Presentation Layer**: WinForms with dependency injection-based theming system
- **Database**: MySQL 5.7.24 (LEGACY - no 8.0+ features allowed)

**Key Technologies**:
- .NET 8.0 Windows Forms
- MySQL 5.7.24 (via MySql.Data 9.4.0)
- Microsoft.Extensions.DependencyInjection 8.0.0
- ClosedXML 0.105.0 (Excel export)

## Setup Commands

### Prerequisites
- .NET 8.0 SDK
- MySQL Server 5.7.24 (running on localhost:3306)
- Visual Studio 2022 or VS Code with C# extension

### Initial Setup

```powershell
# Clone repository
git clone https://github.com/Dorotel/MTM_WIP_Application_WinForms.git
cd MTM_WIP_Application_WinForms

# Restore dependencies
dotnet restore MTM_WIP_Application_Winforms.csproj

# Build application
dotnet build MTM_WIP_Application_Winforms.csproj --configuration Debug

# Run application
dotnet run --project MTM_WIP_Application_Winforms.csproj
```

### Database Setup

```powershell
# MAMP MySQL Connection Details:
# Host: localhost
# Port: 3306
# Username: root
# Password: root
# MySQL Path: C:\MAMP\bin\mysql\bin\mysql.exe

# Connect to MySQL (add to PATH or use full path)
# Option 1: If mysql is in PATH
mysql -h localhost -P 3306 -u root -proot

# Option 2: Using full MAMP path
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot

# Connect to production database
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms

# Connect to test database
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test

# Show all databases
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot -e "SHOW DATABASES;"

# Verify production database tables
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "SHOW TABLES;"

# Verify test database tables
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SHOW TABLES;"

# Run database schema scripts (from Database/ directory)
# Schema files located in: Database/CurrentDatabase/
# Stored procedures in: Database/CurrentStoredProcedures/

# Execute a SQL file
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms < Database/CurrentDatabase/schema.sql
```

## Development Workflow

### Building

```powershell
# Debug build (default)
dotnet build MTM_WIP_Application_Winforms.csproj

# Release build
dotnet build MTM_WIP_Application_Winforms.csproj --configuration Release

# Clean build artifacts
dotnet clean MTM_WIP_Application_Winforms.csproj
```

### Running the Application

```powershell
# Run from command line
dotnet run --project MTM_WIP_Application_Winforms.csproj

# Or use Visual Studio: Press F5 (Debug) or Ctrl+F5 (Run without debugging)
```

### Hot Reload
- WinForms does not support hot reload like web applications
- Changes require rebuild and restart
- Use Debug mode (F5) for faster iteration with debugger attached

### Environment Configuration

**MAMP MySQL Server Configuration:**
- **Host**: localhost
- **Port**: 3306
- **Username**: root
- **Password**: root
- **MySQL Binary**: `C:\MAMP\bin\mysql\bin\mysql.exe`

**Database Names:**
- **Production**: `mtm_wip_application_winforms`
- **Testing**: `mtm_wip_application_winforms_test`

**Application Settings:**
- Database connection managed in `Helpers/Helper_Database_Variables.cs`
- User-specific settings stored in `%APPDATA%\MTM\`
- Theme preferences in database (`app_themes`, `app_users` tables)


## Code Style Guidelines

### Critical Rules (NON-NEGOTIABLE)

1. **NO direct database access** - Use `Helper_Database_StoredProcedure` only
2. **NO MessageBox.Show** - Use `Service_ErrorHandler` for all errors
3. **ALL database operations must be async** - Use async/await
4. **ALL DAO methods return Model_Dao_Result<T>** - Never throw exceptions to callers
5. **ALL forms inherit from ThemedForm** - Never inherit from Form directly
6. **ALL user controls inherit from ThemedUserControl** - Never inherit from UserControl
7. **MySQL 5.7.24 only** - NO JSON functions, CTEs, window functions, or 8.0+ features

### Naming Conventions

```csharp
// Classes
public class Dao_Inventory { }                    // DAOs
public class Service_ErrorHandler { }             // Services
public class Helper_Database_StoredProcedure { } // Helpers
public partial class SettingsForm : ThemedForm { } // Forms
public class Control_QuickButtons { }             // Controls
public class Model_Dao_Result<T> { }             // Models
public enum Enum_ErrorSeverity { }               // Enums

// Form controls
private Button MainForm_Button_Save_1;
private TextBox SettingsForm_TextBox_UserName;

// Methods
public async Task<Model_Dao_Result<DataTable>> GetAllAsync() { }
private void MainForm_Button_Save_Click(object sender, EventArgs e) { }

// Fields and variables
private readonly ILogger<MyClass> _logger;  // Private fields with underscore
public string ThemeName { get; set; }       // Properties PascalCase
```

### File Organization (#region MANDATORY)

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

### XML Documentation (MANDATORY)

ALL public members must have XML documentation:

```csharp
/// <summary>
/// Brief description of what this does.
/// </summary>
/// <param name="userId">Description of parameter.</param>
/// <returns>
/// Model_Dao_Result containing DataTable on success.
/// Check IsSuccess before accessing Data.
/// ErrorMessage contains user-friendly message on failure.
/// </returns>
public async Task<Model_Dao_Result<DataTable>> GetDataAsync(string userId)
{
    // Implementation
}
```

### Linting and Formatting

```powershell
# Check for errors (no separate lint command - use build)
dotnet build MTM_WIP_Application_Winforms.csproj

# EditorConfig settings in .editorconfig
# - File-scoped namespaces
# - 4 spaces indentation
# - LF line endings
# - Nullable reference types enabled
```

### Code Style Validation Tools

Available MCP tools for validation (if MCP server running):

```powershell
# Validate DAO patterns (regions, async/await, Helper usage, error handling)
# mcp tool: validate_dao_patterns

# Check error handling (Service_ErrorHandler usage, no MessageBox.Show)
# mcp tool: validate_error_handling

# Validate UI DPI scaling patterns
# mcp tool: validate_ui_scaling

# Check XML documentation coverage
# mcp tool: check_xml_docs
```

## Build and Deployment

### Build Configurations

```powershell
# Debug build (default)
dotnet build --configuration Debug

# Release build (optimized, no debug symbols)
dotnet build --configuration Release

# Output locations:
# Debug:   bin/Debug/net8.0-windows/
# Release: bin/Release/net8.0-windows/
```

### Build Outputs

- **Executable**: `MTM_WIP_Application_Winforms.exe`
- **Dependencies**: Copied to output directory (CopyLocalLockFileAssemblies=true)
- **Resources**: Icon, Help documentation, Excel templates
- **Configuration**: App.config (connection strings, logging paths)

### Pre-deployment Checklist

1. Build succeeds in Release configuration
2. Database migrations applied to production database
3. User permissions configured in MySQL
4. Application logs directory accessible (%APPDATA%\MTM\Logs\)

### Deployment Steps

1. Build Release configuration
2. Copy `bin/Release/net8.0-windows/` contents to deployment location
3. Ensure MySQL 5.7.24 server accessible
4. Verify database schema matches code expectations
5. Test application launch and database connectivity

## Architecture Patterns

### DAO Pattern (Data Access)

```csharp
public class Dao_Entity
{
    public async Task<Model_Dao_Result<DataTable>> GetAllAsync()
    {
        return await Helper_Database_StoredProcedure
            .ExecuteDataTableWithStatusAsync("md_entity_GetAll", null);
    }

    public async Task<Model_Dao_Result<bool>> InsertAsync(string name)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Name", name }  // NO p_ prefix in C# (only in SQL)
        };

        return await Helper_Database_StoredProcedure
            .ExecuteNonQueryWithStatusAsync("md_entity_Insert", parameters);
    }
}
```

### Error Handling Pattern

```csharp
// CORRECT: Use Service_ErrorHandler
try
{
    var result = await dao.GetDataAsync();
    if (!result.IsSuccess)
    {
        Service_ErrorHandler.ShowUserError(result.ErrorMessage);
        return;
    }
    // Process result.Data
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

// WRONG: Never do this
catch (Exception ex)
{
    MessageBox.Show(ex.Message); // FORBIDDEN!
}
```

### Theme Integration Pattern

```csharp
// Forms
public partial class MyForm : ThemedForm  // NOT Form!
{
    public MyForm()
    {
        InitializeComponent();
        // Theme automatically applied by base class
    }
}

// User Controls
public partial class MyControl : ThemedUserControl  // NOT UserControl!
{
    public MyControl()
    {
        InitializeComponent();
        // Theme automatically applied
    }
}
```

### Help System

The application features a modern, JSON-driven help system that renders HTML content via WebView2.

**Architecture**:
- **Content**: Stored as JSON files in `Documentation/Help/JSON/`.
- **Rendering**: `Service_HelpTemplateEngine` generates HTML from templates in `Documentation/Help/Templates/`.
- **Viewer**: `HelpViewerForm` hosts a WebView2 control to display content.
- **Search**: In-memory search implemented in `Service_HelpSystem`.

**Adding Help Content**:
1. Create a new JSON file in `Documentation/Help/JSON/`.
2. Follow the schema defined in `Models/Help/Model_HelpCategory.cs`.
3. Restart the application to see the new category.

## Pull Request Guidelines

### Title Format
`[Component] Brief description`

Examples:
- `[DAO] Add Dao_Inventory.GetByPartAsync method`
- `[Theme] Fix ComboBox border colors in Forest theme`

### Required Checks Before Submitting

```powershell
# 1. Build succeeds
dotnet build MTM_WIP_Application_Winforms.csproj

# 2. Code follows patterns
# - Check XML documentation on public members
# - Verify #region organization
# - Ensure Service_ErrorHandler used (no MessageBox.Show)
# - Confirm async/await for all I/O operations
```

### Review Requirements

- At least one approval from maintainer
- All CI checks passing
- No merge conflicts with target branch
- Code follows architectural boundaries (Forms→DAOs→Database)
- Constitution compliance verified

### Commit Message Conventions

Format: `{TaskID}: {Description}`

Examples:
- `T042: Add XML documentation to PrintForm`
- `T051: Implement ComboBox FlatStyle.Standard for visible borders`
- `Fix: Correct StatusStrip background color inheritance`

## Common Workflows

### Adding a New DAO

1. Create file in `Data/` folder: `Dao_EntityName.cs`
2. Add stored procedure calls using `Helper_Database_StoredProcedure`
3. Return `Model_Dao_Result<T>` from all methods
4. Add XML documentation to all public methods
5. Organize with #region blocks

### Adding a New Form

1. Create in `Forms/{FeatureName}/` folder
2. Inherit from `ThemedForm` (NOT `Form`)
3. Use dependency injection for services if needed
4. Handle errors with `Service_ErrorHandler`
5. Add XML documentation
6. Organize with #region blocks
7. Test theme integration (light/dark themes)

### Adding a New Stored Procedure

1. Create SQL file in `Database/UpdatedStoredProcedures/`
2. Use naming convention: `{prefix}_{entity}_{action}.sql`
3. Include output parameters: `p_Status INT`, `p_ErrorMsg VARCHAR(500)`
4. Follow MySQL 5.7.24 syntax (NO 8.0+ features)
5. Add corresponding DAO method
6. Document in DAO XML comments

### Modifying Theme Colors

1. Update theme in database (`app_themes` table, `SettingsJson` column)
2. Use JSON format for color properties
3. Restart application to load changes
4. Or use Settings Form → Themes tab → Save to apply immediately
5. Test with all controls (buttons, textboxes, comboboxes, etc.)

## Debugging and Troubleshooting

### Common Issues

**Build Errors:**
```powershell
# Clean and rebuild
dotnet clean
dotnet build

# Check for file locks (close Visual Studio)
# Verify .NET 8.0 SDK installed: dotnet --version
```

**Database Connection Issues:**
- Verify MySQL service running
- Check connection string in `Helper_Database_Variables.cs`
- Ensure database `mtm_wip_application_winforms` exists
- Verify user permissions for database access

**Theme Not Applying:**
- Verify form inherits from `ThemedForm` (not `Form`)
- Check `Model_Application_Variables.ThemeEnabled = true`
- Ensure theme exists in `app_themes` table
- Restart application after database theme changes

### Logging

Application logs location: `%APPDATA%\MTM\Logs\`

Log format: CSV with columns:
- Timestamp
- Log Level
- Component
- Message
- User (if applicable)
- Additional Context

View logs:
- Menu: View → Application Logs
- Or manually open CSV files in `%APPDATA%\MTM\Logs\`

### Debugging Tips

- Use `LoggingUtility.Log()` for custom log messages
- Set breakpoints in DAOs to inspect `Model_Dao_Result` values
- Check `Service_ErrorHandler` logs for exception details
- Enable debug-level logging in `Model_Application_Variables.DebugLevel`

## Performance Considerations

- **Database queries**: All use stored procedures with connection pooling
- **Async operations**: All I/O operations use async/await (no blocking)
- **Memory management**: Dispose of DataTables and database resources
- **Theme application**: Cached themes, debounced updates (300ms)
- **Form loading**: Lazy load data on form display, not constructor

## Project-Specific Context

### Constitution Principles

This project follows strict architectural rules (`.specify/memory/constitution.md`):

1. **Centralized Error Handling** - Service_ErrorHandler only (no MessageBox.Show)
2. **Structured Logging** - CSV format via LoggingUtility
3. **Model_Dao_Result Pattern** - All DAO methods return this type
4. **Async-First** - All I/O operations must be async
5. **Stored Procedures Only** - NO inline SQL anywhere
6. **WinForms Best Practices** - InvokeRequired, proper disposal, theme support

### Instruction Files

Detailed guidance in `.github/instructions/`:
- `csharp-dotnet8.instructions.md` - C# and .NET patterns
- `mysql-database.instructions.md` - Database patterns
- `testing-standards.instructions.md` - Test requirements
- `documentation.instructions.md` - XML documentation
- And more...

### Serena Semantic Coding Tools (ESSENTIAL for MTM Project)

**Serena** provides IDE-like code navigation and symbol-level editing for this 300+ file codebase. It's **critical** for efficient work on MTM project.

#### Installation & Setup

**Prerequisites:**
- Python 3.10+ with uv (recommended) or pip
- .NET 8.0 SDK (for C# language server)
- Git (for repository access)

**Quick Start:**

```powershell
# Method 1: Using uvx (recommended - no installation needed)
uvx --from git+https://github.com/oraios/serena serena --version

# Method 2: Local installation
git clone https://github.com/oraios/serena
cd serena
uv run serena --version

# Method 3: pip install
pip install git+https://github.com/oraios/serena
serena --version
```

**MTM Project Setup:**

```powershell
# 1. Navigate to MTM project
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms

# 2. Create Serena project (one-time)
serena project create --language csharp --name "MTM_WIP_Application" --index

# 3. Verify setup
serena project health-check

# Output should show:
# - Project: MTM_WIP_Application
# - Languages: csharp
# - Files indexed: 300+
# - Language server: Running
```

**Configuration File** (`.serena/project.yml`):
```yaml
name: MTM_WIP_Application
languages:
  - csharp
read_only: false  # Set true for QA/review-only agents
excluded_tools: []  # Add tools to disable (e.g., execute_shell_command for safety)
ignore_patterns:
  - "bin/**"
  - "obj/**"
  - "*.dll"
  - "*.exe"
```

#### MCP Server Setup (For AI Assistants)

**For Gemini CLI / Claude Desktop / VS Code:**

```powershell
# Start Serena MCP server
serena start-mcp-server --context ide --project "C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms"

# Server will:
# 1. Start on http://localhost:24282 (or next available port)
# 2. Open dashboard at http://localhost:24282/dashboard/
# 3. Expose 35+ tools via MCP protocol
# 4. Auto-index C# files in background
```

**MCP Client Configuration Examples:**

**Gemini CLI** (`~/.gemini-cli/config.json`):
```json
{
  "mcpServers": {
    "serena": {
      "command": "uvx",
      "args": [
        "--from", "git+https://github.com/oraios/serena",
        "serena", "start-mcp-server",
        "--context", "ide",
        "--project", "C:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms"
      ]
    }
  }
}
```

**Claude Desktop** (`claude_desktop_config.json`):
```json
{
  "mcpServers": {
    "serena": {
      "command": "C:\\path\\to\\uvx.exe",
      "args": [
        "--from", "git+https://github.com/oraios/serena",
        "serena", "start-mcp-server",
        "--context", "desktop-app",
        "--project", "C:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms"
      ]
    }
  }
}
```

**VS Code** (via Cline/Roo Code extension settings):
```json
{
  "mcp.servers": {
    "serena": {
      "command": "serena",
      "args": [
        "start-mcp-server",
        "--context", "ide",
        "--project", "${workspaceFolder}"
      ]
    }
  }
}
```

#### Contexts & Modes

**Contexts** (Choose based on environment):
- `ide` - For VS Code, Cursor, Cline (disables basic file ops AI already has)
- `desktop-app` - For Claude Desktop (full tool suite)
- `claude-code` - For Claude Code CLI (optimized for Claude Code)
- `agent` - For autonomous agents (Agno, custom frameworks)

**Modes** (Combine for workflow):
- `editing` - Code modification focus
- `interactive` - Asks questions before acting
- `planning` - Analysis/design, no immediate editing
- `one-shot` - Complete task in single response
- `no-onboarding` - Skip initial project exploration (use after first time)

**MTM Recommended Combinations:**
```powershell
# Daily development work
serena start-mcp-server --context ide --mode editing --mode interactive

# Initial project exploration
serena start-mcp-server --context ide --mode planning --mode interactive

# Quick bug fix
serena start-mcp-server --context ide --mode editing --mode one-shot --mode no-onboarding

# Code review / audit
serena start-mcp-server --context ide --mode planning --mode no-onboarding
# Also set read_only: true in project.yml
```

#### Essential Serena Commands for MTM

```powershell
# 1. Get file structure (instead of reading entire file)
get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
# Returns: Class + all method signatures (200-300 tokens vs 5,000+)

# 2. Read specific method only
find_symbol("Dao_Inventory/GetAllAsync", include_body=true)
# Returns: Just this method (100-200 tokens)

# 3. Find where method is used before modifying
find_referencing_symbols("GetAllAsync", "Data/Dao_Inventory.cs")
# Returns: All call sites with context (critical before breaking changes!)

# 4. Replace method precisely
replace_symbol_body("Dao_Inventory/GetAllAsync", "Data/Dao_Inventory.cs", new_code)
# Updates: Complete method implementation

# 5. Find architectural violations
search_for_pattern("MessageBox\\.Show", restrict_search_to_code_files=true)
# Finds: All anti-pattern usages

# 6. Access project knowledge
read_memory("architectural_patterns.md")
list_memories()
# Loads: Coding standards, patterns, common tasks
```

#### Serena Dashboard

- **URL**: http://127.0.0.1:24282/dashboard/
- **Features**:
  - Real-time tool usage monitoring
  - Execution queue status
  - Memory list and contents
  - Language server status
  - Configuration viewer
  - Log viewer

**Troubleshooting via Dashboard:**
- Check if language server is running (should show "csharp: Running")
- View execution logs for failed tool calls
- Monitor token usage per tool
- Verify active context and modes

#### Performance Optimization

**Pre-Indexing** (Recommended for MTM's 300+ files):
```powershell
# One-time indexing (takes ~60 seconds)
serena project index

# Benefit: Saves 5-10 seconds per tool call during session
```

**Git Worktrees** (Parallel development):
```powershell
# Create worktree for feature branch
git worktree add ../MTM_feature_branch

# Copy Serena cache (avoid re-indexing)
Copy-Item -Recurse .serena/cache ../MTM_feature_branch/.serena/cache

# Result: Instant Serena availability in new worktree
```

#### Token Efficiency Benchmarks

**MTM Codebase Exploration:**
- **Without Serena**: Reading 10 DAO files = ~50,000 tokens
- **With Serena**: Symbol-level exploration = ~3,000 tokens
- **Savings**: 94%

**Refactoring Impact Analysis:**
- **Without Serena**: Grep + read files = ~50,000 tokens
- **With Serena**: `find_referencing_symbols` = ~1,500 tokens
- **Savings**: 97%

#### Agent Profiles Using Serena

Serena enables specialized agent workflows for MTM project. Choose profile based on task:

##### 1. DAO Developer Agent

**Focus:** Data layer (DAOs, stored procedures, database access)

**Serena Workflow:**
```
1. Explore existing DAOs:
   get_symbols_overview("Data/Dao_Entity.cs", depth=1)

2. Validate patterns:
   search_for_pattern("Task<Model_Dao_Result", relative_path="Data")
   search_for_pattern("Helper_Database_StoredProcedure", relative_path="Data")

3. Add new methods:
   find_symbol("Dao_Entity/SimilarMethod", include_body=true)  # Template
   insert_after_symbol("Dao_Entity/SimilarMethod", ..., new_method)

4. Verify compilation:
   execute_shell_command("dotnet build")
```

**Memories to Load:**
- `dao_best_practices.md`
- `helper_database_usage.md`
- `model_dao_result_pattern.md`

##### 2. Forms & UI Agent

**Focus:** WinForms (Forms, Controls, theme integration)

**Serena Workflow:**
```
1. Explore Form structure:
   get_symbols_overview("Forms/Settings/SettingsForm.cs", depth=1)

2. Verify theme compliance:
   search_for_pattern("class \\w+Form : ThemedForm", relative_path="Forms")

3. Find event handlers:
   search_for_pattern("_Click\\(object sender, EventArgs e\\)", relative_path="Forms")

4. Modify handlers:
   find_symbol("SettingsForm/Button_Save_Click", include_body=true)
   replace_symbol_body(...)
```

**Memories to Load:**
- `theme_system_guide.md`
- `winforms_patterns.md`
- `ui_naming_conventions.md`

##### 3. Service Layer Architect

**Focus:** Business logic (Services, error handling, logging)

**Serena Workflow:**
```
1. Explore Service methods:
   get_symbols_overview("Services/Service_ErrorHandler.cs", depth=1)

2. Find Service usage:
   find_referencing_symbols("ShowUserError", "Services/Service_ErrorHandler.cs")

3. Validate error handling:
   search_for_pattern("Service_ErrorHandler\\.(ShowUserError|HandleException)")
   search_for_pattern("MessageBox\\.Show")  # Should be 0 results!

4. Update Service logic:
   find_symbol("Service_ErrorHandler/HandleException", include_body=true)
   replace_symbol_body(...)
```

**Memories to Load:**
- `service_error_handler_usage.md`
- `logging_patterns.md`
- `service_layer_architecture.md`

##### 4. Database Administrator Agent

**Focus:** Stored procedures, database migrations, SP-DAO mapping

**Serena Workflow:**
```
1. Map stored procedure to DAO:
   search_for_pattern('"md_inventory_GetAll"', relative_path="Data")
   → Find which DAO calls this SP

2. Trace SP → DAO → Service → Form:
   find_referencing_symbols("GetAllAsync", "Data/Dao_Inventory.cs")
   → See full call chain

3. Validate SP parameters:
   find_symbol("Dao_Inventory/GetAllAsync", include_body=true)
   → Verify parameter mapping

4. Execute SP migration:
   execute_shell_command('& "C:\\MAMP\\bin\\mysql\\bin\\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms < Database/UpdatedStoredProcedures/new_sp.sql')
```

**Memories to Load:**
- `stored_procedure_conventions.md`
- `database_schema.md`
- `sp_dao_mapping.md`

##### 5. Quality Assurance Agent

**Focus:** Code review, architectural compliance, documentation

**Serena Configuration:**
```yaml
# .serena/project.yml
read_only: true  # Prevent accidental edits
excluded_tools:
  - execute_shell_command  # No command execution
  - replace_symbol_body    # No code modification
  - insert_after_symbol
  - insert_before_symbol
```

**Serena Workflow:**
```
1. Architectural validation:
   search_for_pattern("MessageBox\\.Show")  # Should be 0
   search_for_pattern("new MySqlConnection")  # Should be 0
   search_for_pattern("Task<Model_Dao_Result", relative_path="Data")  # Should match all DAO methods

2. Documentation audit:
   search_for_pattern("^\\s*public (async )?Task", context_lines_before=1)
   → Check if preceding line has /// (XML docs)

3. #region compliance:
   search_for_pattern("#region Fields")
   search_for_pattern("#region Methods")
   → Verify all files have proper organization

4. Generate compliance report:
   write_memory("compliance_report_2025-12-14.md", findings)
```

**Memories to Load:**
- `architectural_patterns.md`
- `coding_standards.md`
- `compliance_checklist.md`

##### 6. Documentation Specialist Agent

**Focus:** XML documentation, help system, release notes

**Serena Workflow:**
```
1. Find undocumented methods:
   search_for_pattern("^\\s*public.*Task.*\\(", relative_path="Data", context_lines_before=1)
   → Check for missing /// comments

2. Read methods needing docs:
   find_symbol("Dao_Entity/UndocumentedMethod", include_body=true)

3. Add XML documentation:
   replace_symbol_body(
       name_path="Dao_Entity/UndocumentedMethod",
       relative_path="Data/Dao_Entity.cs",
       body=method_with_xml_docs_added
   )

4. Generate documentation:
   write_memory("undocumented_methods_log.md", list_of_completed_methods)
```

**Memories to Load:**
- `xml_documentation_standards.md`
- `help_system_guide.md`

---

#### Troubleshooting Serena

**Issue: "find_symbol returns empty"**
```powershell
# Solution 1: Restart language server
restart_language_server()

# Solution 2: Re-index project
serena project index

# Solution 3: Check dashboard
# Visit http://127.0.0.1:24282/dashboard/
# Look for language server status
```

**Issue: "Slow tool execution"**
```powershell
# Solution: Pre-index project
serena project index

# Also: Restrict search scope
search_for_pattern("pattern", relative_path="Data")  # Not entire project
```

**Issue: "Context window filling up"**
```
# Solution: Use memory system
write_memory("current_progress.md", summary_of_work)

# Start new conversation:
activate_project("MTM_WIP_Application")
read_memory("current_progress.md")
# Continue work
```

**Full Serena Guide**: `.github/instructions/serena-semantic-tools.instructions.md`  
**Serena Official Docs**: https://oraios.github.io/serena/  
**GitHub Repository**: https://github.com/oraios/serena

### MCP Workflow Tools

If MCP server is running (`.mcp/mtm-workflow/`), additional validation tools available:
- `validate_dao_patterns` - Check DAO compliance
- `validate_error_handling` - Verify Service_ErrorHandler usage
- `validate_ui_scaling` - Check DPI scaling patterns
- `check_xml_docs` - Verify documentation coverage
- `analyze_dependencies` - Map stored procedure dependencies
- `suggest_refactoring` - AI-powered code improvement suggestions

Refer to `.mcp/mtm-workflow/README.md` for full tool documentation.

---

**Last Updated**: 2025-12-13
**Project Version**: 6.4.1.0
**Maintained By**: Manitowoc Tool and Manufacturing
**For Questions**: Refer to `.github/copilot-instructions.md` for coding patterns
