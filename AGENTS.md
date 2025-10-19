# MTM WIP Application - Agent Development Guidelines

Auto-generated agent guidelines. Last updated: 2025-10-18

## Agent Communication Rules

**CRITICAL**: Agents working on this project MUST follow these communication protocols:

- **Remain silent** during work execution unless:
  - Acknowledging the user's prompt at the start
  - Asking clarifying questions when requirements are ambiguous
  - Providing a brief summary at the end of the current run
- **Keep chat summaries minimal** - one or two sentences maximum
- **No explanations or updates** during task execution
- **No unnecessary status updates** - let the code speak for itself
- **Maximize Premium Request value** - ⚠️ **EXTREMELY IMPORTANT - NEVER SKIP THIS**: Strive to complete multiple related tasks in a single session rather than stopping after one task. Continue working through sequential or related tasks until a natural checkpoint is reached or significant complexity requires user input. DO NOT stop prematurely just because one task is complete

## Project Overview

**MTM WIP Application** is a .NET 8.0 Windows Forms desktop application for managing Work-In-Progress (WIP) inventory in a manufacturing environment. It uses MySQL 5.7 with a stored-procedure-only data access pattern and includes advanced theming, progress reporting, and error handling systems.

**Key Technologies:**
- .NET 8.0 (C# 12) Windows Forms
- MySQL 5.7.24+ with MySql.Data 9.4.0 connector
- Stored procedures only (no inline SQL permitted)
- Microsoft.Web.WebView2 for help system
- ClosedXML for Excel export functionality

**Architecture:**
- WinForms event-driven UI with custom controls
- DAO pattern wrapping stored procedures
- Helper classes for database and UI operations
- Centralized error handling and logging
- Environment-aware configuration (Debug/Release)

## Project Structure

```
MTM_Inventory_Application/
├── Controls/              # UserControl implementations
│   ├── Addons/           # Specialized controls (connection strength, etc.)
│   ├── MainForm/         # Main application tabs
│   ├── SettingsForm/     # Settings dialog controls
│   └── Shared/           # Reusable controls
├── Core/                 # Core services
│   ├── Core_Themes.cs    # Theming and DPI scaling
│   ├── Core_WipAppVariables.cs  # Application constants
│   └── Core_DgvPrinter.cs       # DataGridView printing
├── Data/                 # DAO layer (stored procedure wrappers)
├── Database/             # Database scripts
│   ├── CurrentDatabase/           # ⚠️ REFERENCE ONLY
│   ├── CurrentStoredProcedures/   # ⚠️ REFERENCE ONLY
│   ├── UpdatedDatabase/           # ✅ ACTIVE - development structure
│   └── UpdatedStoredProcedures/   # ✅ ACTIVE - 74+ procedures
├── Documentation/        # Comprehensive guides
│   ├── Copilot Files/   # Modular documentation (26 files)
│   ├── Guides/          # Technical architecture guides
│   ├── Help/            # HTML help system
│   └── Patches/         # Historical fix documentation
├── Forms/               # Form definitions
├── Helpers/             # Utility classes
│   ├── Helper_Database_Variables.cs      # Connection logic
│   ├── Helper_Database_StoredProcedure.cs # SP execution
│   └── Helper_StoredProcedureProgress.cs  # Progress reporting
├── Logging/             # Centralized logging
├── Models/              # Data models and DTOs
├── Services/            # Background services
└── Program.cs           # Application entry point
```

## Setup Commands

### Initial Setup

```powershell
# Clone repository
git clone https://github.com/Dorotel/MTM_WIP_Application_WinForms.git
cd MTM_WIP_Application_WinForms

# Restore dependencies
dotnet restore

# Build Debug configuration (uses test database)
dotnet build MTM_Inventory_Application.csproj -c Debug

# Build Release configuration (uses production database)
dotnet build MTM_Inventory_Application.csproj -c Release
```

### Database Setup

**Environment-Aware Database Selection:**
- **Debug Mode**: `mtm_wip_application_winforms_test` on `localhost` or `172.16.1.104`
- **Release Mode**: `mtm_wip_application` on `172.16.1.104` (production)

**Connection String Template:**
```
Server=localhost;Port=3306;Database=mtm_wip_application;
User=root;Password=root;SslMode=none;AllowPublicKeyRetrieval=true;
```

**Server Selection Logic (Debug Mode):**
- If current machine IP is `172.16.1.104` → connects to `172.16.1.104`
- Otherwise → connects to `localhost`

## Instruction Files System

**Location**: `.github/instructions/`

The project uses instruction files to provide authoritative guidance on coding patterns, best practices, and common pitfalls. **Always consult relevant instruction files before implementing tasks.**

### Available Instruction Files

1. **csharp-dotnet8.instructions.md** - C# and .NET 8 development guidelines
   - Language features, naming conventions, WinForms patterns
   - Async/await best practices, data access patterns
   - File organization and code structure

2. **mysql-database.instructions.md** - MySQL 5.7 database patterns
   - Stored procedure standards (p_Status, p_ErrorMsg outputs)
   - Connection management and pooling
   - Parameter naming conventions (p_ prefix)
   - Transaction management and retry logic

3. **testing-standards.instructions.md** - Manual validation and testing
   - Success criteria patterns
   - Validation workflows and quality gates
   - Manual testing approach

4. **integration-testing.instructions.md** - Integration test development
   - **Discovery-first workflow**: grep_search → verify signatures → write tests
   - Method signature verification patterns
   - Static vs instance method detection
   - DaoResult null safety patterns
   - Common pitfalls and how to avoid them

5. **documentation.instructions.md** - Documentation standards
   - XML documentation comments
   - README structure, code comments
   - Markdown conventions

6. **markdown.instructions.md** - Markdown content creation standards
   - Markdown formatting rules and structure
   - Heading hierarchy, lists, code blocks
   - Front matter requirements for documentation
   - Validation requirements for markdown files

7. **security-best-practices.instructions.md** - Security standards
   - Input validation, SQL injection prevention
   - Credential management, error handling
   - Secure configuration practices

8. **performance-optimization.instructions.md** - Performance guidelines
   - Async/await patterns, connection pooling
   - Memory management, caching strategies
   - Collection optimization

9. **code-review-standards.instructions.md** - Code review checklist
   - Quality gates, review process
   - Common issues to watch for

### Using Instruction Files

**When starting any task:**
1. Check if task includes **Reference**: marker in tasks.md
2. Read referenced instruction files BEFORE implementing
3. Apply documented patterns
4. Avoid documented pitfalls
5. If conflict or ambiguity: Ask for clarification

**Example task with reference:**
```markdown
- [ ] **T111** – Author logging/quick button integration tests
  - **Reference**: .github/instructions/integration-testing.instructions.md - 
    Follow discovery-first workflow (grep_search → verify signatures → write tests)
```

**Discovery-First Workflow (Integration Testing)**:
```bash
# 1. Find all methods in DAO
grep_search(isRegexp=true, includePattern="Data/Dao_Target.cs", 
            query="(public|internal) (static )?.*Task.*\\(")

# 2. Read actual signatures
read_file(filePath="Data/Dao_Target.cs", offset=lineNumber, limit=30)

# 3. Verify patterns (static vs instance, Async suffix, parameter names)

# 4. Write tests based on actual signatures (not assumptions)
```

## Memory Files (Pitfall Mediation)

**Location**: `.github/memory/`

Memory files capture lessons learned, common pitfalls, and evolving patterns discovered during development. They complement instruction files with real-world experience and debugging insights.

### Available Memory Files

1. **database-patterns.memory.instructions.md** - MySQL 5.7 patterns and limitations
   - No CTEs (Common Table Expressions) - use subqueries instead
   - Connection pooling pitfalls and recovery patterns
   - Stored procedure debugging techniques
   - Transaction isolation gotchas

2. **database-ui-integration.memory.instructions.md** - Database-to-UI data flow
   - DataGridView binding patterns
   - Column mapping strategies
   - Dynamic data display lessons
   - Performance optimization for large datasets

3. **html-css-forms.memory.instructions.md** - HTML/CSS form patterns
   - Help system styling approaches
   - Responsive design lessons
   - Cross-browser compatibility fixes

4. **javascript-modules.memory.instructions.md** - JavaScript module patterns
   - ES6 module lessons learned
   - WebView2 integration patterns
   - Browser compatibility considerations

5. **mcp-development-memory.instructions.md** - MCP server development
   - Tool implementation workflows
   - VS Code integration patterns
   - Error handling strategies

6. **spec-organization-memory.instructions.md** - Feature specification patterns
   - Spec document organization
   - Task breakdown strategies
   - Requirement tracking approaches

7. **validation-automation-memory.instructions.md** - Automation validation
   - Quality gate patterns
   - Automated validation workflows
   - Test data management

### Using Memory Files

- **Read memory files alongside instruction files** to understand both the "right way" (instructions) and "learned lessons" (memory)
- **Memory files are living documents** - updated as new pitfalls are discovered
- **Pitfall mediation**: Memory files prevent repeating past mistakes
- **Cross-reference with instruction files**: Memory files reference corresponding instruction files when applicable

**Example workflow:**
```bash
# Working on database task?
1. Read .github/instructions/mysql-database.instructions.md (patterns)
2. Read .github/memory/database-patterns.memory.instructions.md (pitfalls)
3. Implement with both pattern knowledge and pitfall awareness
```

## Development Workflow

### Running the Application

```powershell
# Run in Debug mode (development)
dotnet run --project MTM_Inventory_Application.csproj

# Build and run in Release mode
dotnet build -c Release
dotnet run --project MTM_Inventory_Application.csproj -c Release
```

### Code Organization Requirements

**ALL C# files MUST follow this region organization:**

```csharp
#region Fields
#region Properties
#region Progress Control Methods
#region Constructors
#region [Specific Functionality]  // e.g., "Database Operations"
#region Key Processing
#region Button Clicks
#region ComboBox & UI Events
#region Helpers
#region Cleanup
```

**Method Ordering Within Regions:**
1. Public methods first
2. Protected methods second
3. Private methods third
4. Static methods at end of each access level

### Database Operations Pattern

**MANDATORY Pattern for All Database Calls:**

```csharp
// Use Helper_Database_StoredProcedure for all stored procedure calls
var parameters = new Dictionary<string, object>
{
    ["UserID"] = userId,           // NO p_ prefix in C#
    ["PartNumber"] = partNumber    // MySQL SP uses p_UserID, p_PartNumber
};

var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatus(
    connectionString,
    "sp_GetInventoryData",
    parameters,
    progressHelper: _progressHelper,
    useAsync: true);

if (result.IsSuccess)
{
    // Process result.Payload (DataTable)
}
else
{
    LoggingUtility.LogApplicationError(result.Exception, result.StatusMessage);
    // Handle error
}
```

**Stored Procedure Requirements:**
- Every SP MUST have `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)`
- Use `p_` prefix in MySQL, remove prefix in C# parameters
- Status codes: 0=Success, 1=Success no data, -1 to -5=Various errors

### Error Handling Requirements

**Replace ALL MessageBox.Show() with Service_ErrorHandler:**

```csharp
// For exceptions
Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, 
    retryAction: () => RetryOperation(),
    contextData: new Dictionary<string, object> { ["UserId"] = userId },
    controlName: nameof(CurrentControl));

// For confirmations
var result = Service_ErrorHandler.ShowConfirmation("Are you sure?", "Confirmation");

// For validation errors
Service_ErrorHandler.HandleValidationError("Invalid input", "FieldName");
```

**Error Severity Levels:**
- `Low`: Information/Warning - application continues
- `Medium`: Recoverable error - can retry
- `High`: Critical error - data integrity affected
- `Fatal`: Application termination required

### Theme and UI Standards

**Required in ALL UserControl Constructors:**

```csharp
public Control_Example()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    WireUpEvents();
    ApplyPrivileges();
}
```

## Testing Instructions

**Manual Validation Approach:**
- No automated unit tests currently implemented
- Test through application execution
- Define success criteria before implementation
- Document test results and scenarios

**Testing Checklist:**
- [ ] Feature matches specification requirements
- [ ] All user scenarios tested (happy path + error cases)
- [ ] Database operations verified through stored procedures
- [ ] UI responsiveness (sub-100ms interactions)
- [ ] Error handling displays appropriate messages
- [ ] No compilation errors or warnings
- [ ] Cross-platform compatibility (Windows primary)

**Common Test Scenarios:**
```powershell
# Build and verify no errors
dotnet build MTM_Inventory_Application.csproj -c Debug

# Check for warnings
dotnet build MTM_Inventory_Application.csproj -c Debug > build-warnings.txt

# Run application and test workflows:
# - Login and session management
# - Inventory adjustments
# - Transfer operations
# - Reporting functionality
```

## Code Style Guidelines

### C# Conventions

**Language Features:**
- Use C# 12 features (file-scoped namespaces, pattern matching, required members)
- Target .NET 8.0 Windows Forms
- Enable nullable reference types
- Use implicit usings

**Naming Conventions:**
- Classes/Interfaces: PascalCase
- Methods: PascalCase
- Private fields: `_camelCase`
- Parameters: camelCase (no prefix)
- Forms: `[Name]Form` suffix
- Controls: `Control_[Name]` prefix
- DAOs: `Dao_[Entity]` prefix
- Helpers: `Helper_[Purpose]` prefix

**File Organization:**
- Place DAOs in `Data/` folder
- Place helpers in `Helpers/` folder
- Place forms in `Forms/[Category]/` folders
- Place controls in `Controls/[Category]/` folders
- Place models in `Models/` folder

### Database Conventions

**Stored Procedure Standards:**
- Prefix: `sp_` for stored procedures
- Parameters: `p_` prefix in MySQL (e.g., `p_UserID`)
- C# parameters: Remove `p_` prefix (e.g., `UserID`)
- Always include: `OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)`

**Connection Management:**
- Always use connection pooling (MinPoolSize=5, MaxPoolSize=100)
- Dispose connections with `using` statements
- Use async/await for all database operations
- Connection timeout: 30 seconds default

### WinForms Best Practices

- Keep event handlers thin - delegate to helpers/DAOs/services
- No logic in `.Designer.cs` files
- Use async/await for I/O operations
- Marshal UI updates to UI thread with `Invoke`/`BeginInvoke`
- Implement proper disposal in `Dispose()` method

## Build and Deployment

### Build Commands

```powershell
# Clean solution
dotnet clean

# Restore packages
dotnet restore

# Build Debug (test database)
dotnet build MTM_Inventory_Application.csproj -c Debug

# Build Release (production database)
dotnet build MTM_Inventory_Application.csproj -c Release

# Build with specific framework
dotnet build -p:TargetFramework=net8.0-windows -p:Configuration=Debug
```

### Output Directories

- Debug builds: `bin/Debug/net8.0-windows/`
- Release builds: `bin/Release/net8.0-windows/`
- Help system: `bin/[Config]/net8.0-windows/Documentation/Help/`
- Templates: `bin/[Config]/net8.0-windows/Controls/MainForm/WIPAppTemplate.xlsx`

### Deployment Checklist

- [ ] Build in Release configuration
- [ ] Verify database connection points to production
- [ ] Test all stored procedures in production database
- [ ] Verify help system files copied to output
- [ ] Test application startup and connectivity
- [ ] Verify logging and error handling
- [ ] Document deployment steps and rollback plan

## Pull Request Guidelines

### Title Format
```
[component] Brief description

Examples:
[DAO] Add inventory adjustment stored procedure wrapper
[UI] Implement progress reporting for transfer operations
[Database] Update stored procedures with uniform p_ naming
[Docs] Add agent development guidelines
```

### Required Checks Before PR

```powershell
# Build must succeed
dotnet build MTM_Inventory_Application.csproj -c Debug

# Check for warnings
dotnet build MTM_Inventory_Application.csproj -c Debug 2>&1 | Select-String "warning"

# Manual validation testing
# - Run application
# - Test affected features
# - Verify database operations
# - Check error handling
```

### PR Submission Checklist

- [ ] Code follows region organization standards
- [ ] All database calls use Helper_Database_StoredProcedure
- [ ] Error handling uses Service_ErrorHandler (no MessageBox.Show)
- [ ] Theme application in constructors (Core_Themes.ApplyDpiScaling)
- [ ] Proper null safety and error handling
- [ ] XML documentation on public APIs
- [ ] Manual testing completed with success criteria met
- [ ] No hardcoded credentials or connection strings
- [ ] Documentation updated if needed

## Security Considerations

### Critical Security Rules

1. **NO inline SQL** - stored procedures only
2. **NO hardcoded credentials** - use Helper_Database_Variables
3. **NO sensitive data in logs** - sanitize before logging
4. **Parameter validation** - validate all user input before database calls
5. **Environment separation** - Debug uses test database, Release uses production

### Connection String Security

```csharp
// ✅ CORRECT: Use Helper_Database_Variables
string connectionString = Helper_Database_Variables.GetConnectionString();

// ❌ WRONG: Hardcoded connection string
string connectionString = "Server=172.16.1.104;Database=mtm_wip_application;User=root;Password=root";
```

### SQL Injection Prevention

**ALL database operations MUST use parameterized stored procedures:**

```csharp
// ✅ SECURE: Parameterized stored procedure
var parameters = new Dictionary<string, object>
{
    ["PartNumber"] = userInput  // Safely parameterized
};
var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatus(...);

// ❌ NEVER DO THIS: String concatenation
var sql = $"SELECT * FROM Parts WHERE PartNumber = '{userInput}'";  // UNSAFE!
```

## Troubleshooting and Common Issues

### Database Connection Issues

```powershell
# Verify MySQL service is running
Get-Service -Name "*mysql*"

# Test connection manually
mysql -h localhost -P 3306 -u root -p

# Check environment configuration
# Debug mode should use: mtm_wip_application_winforms_test
# Release mode should use: mtm_wip_application
```

### Build Issues

```powershell
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build MTM_Inventory_Application.csproj -c Debug

# Check for missing dependencies
dotnet list package --outdated

# Verify .NET 8 SDK installed
dotnet --version  # Should be 8.0.x
```

### UI Rendering Issues

```csharp
// Verify theme application in constructor:
Core_Themes.ApplyDpiScaling(this);
Core_Themes.ApplyRuntimeLayoutAdjustments(this);

// Check for null references in UI controls
// Verify UserControl inheritance and InitializeComponent() calls
```

### Progress Control Issues

```csharp
// Verify progress helper initialization:
_progressHelper = Helper_StoredProcedureProgress.Create(
    progressBar, statusLabel, this.FindForm());

// Ensure proper ShowProgress/UpdateProgress/ShowSuccess patterns
// Check try/catch/finally blocks with progress cleanup
```

## Additional Context

### File Structure Compliance

**CRITICAL FILE STRUCTURE RULES:**
- **Current\*** folders (CurrentDatabase, CurrentStoredProcedures): **REFERENCE ONLY** - DO NOT ALTER
- **Updated\*** folders (UpdatedDatabase, UpdatedStoredProcedures): **ACTIVE DEVELOPMENT** - USE FOR ALL CHANGES

### Environment Configuration

**Automatic Environment Selection:**
```csharp
#if DEBUG
    string database = "mtm_wip_application_winforms_test";
    string server = GetLocalIpAddress() == "172.16.1.104" ? "172.16.1.104" : "localhost";
#else
    string database = "mtm_wip_application";
    string server = "172.16.1.104";  // Production server only
#endif
```

### Documentation Resources

**Essential Documentation Files:**
- Core Technical: `Documentation/Copilot Files/01-overview-architecture.md`
- Patterns/Templates: `Documentation/Copilot Files/04-patterns-and-templates.md`
- Database/Stored Procedures: `Documentation/Copilot Files/07-database-and-stored-procedures.md`
- Refactoring Workflow: `Documentation/Copilot Files/21-refactoring-workflow.md`
- User Guides: `Documentation/Guides/USER_GUIDE_COMPLETE.md`
- Help System: `Documentation/Help/index.html`

### Memory Files & Learning Resources

**Workspace-Specific Memory:**
- `.github/instructions/validation-automation-memory.instructions.md` (Automation validation patterns)

**Global Cross-Project Memory:**
- `vscode-userdata:/User/prompts/powershell-memory.instructions.md` (PowerShell scripting patterns)
- `vscode-userdata:/User/prompts/debugging-memory.instructions.md` (Debugging workflows and troubleshooting)
- `vscode-userdata:/User/prompts/memory.instructions.md` (Universal development patterns)

**Note:** These memory files contain hard-won lessons and best practices from previous sessions. Always consult them when working with PowerShell automation, debugging issues, or implementing validation workflows.

### Performance Considerations

- UI response target: Sub-100ms for interactions
- Database timeout: 30 seconds default
- Use async/await for all I/O operations
- Implement connection pooling (configured in connection string)
- Keep long operations off UI thread
- Use progress reporting for operations > 1 second

### Recent Changes (Last 3 Major Updates)

1. **Service_ErrorHandler Implementation** (Jan 2025)
   - Centralized error handling system
   - Enhanced error dialog with tabbed interface
   - Automatic logging with caller context
   - Replaced MessageBox.Show throughout application

2. **Help System Integration** (Jan 2025)
   - Modern HTML help system with search
   - WebView2 integration in MainForm
   - Keyboard shortcuts (F1, Ctrl+F1, Ctrl+Shift+K)
   - Comprehensive user and technical guides

3. **Environment-Aware Configuration** (Jan 2025)
   - Automatic Debug/Release database selection
   - Intelligent server address detection
   - Updated deployment scripts for safety
   - Clear Current\*/Updated\* folder separation

## Quick Commands Reference

```powershell
# Restore and build
dotnet restore && dotnet build -c Debug

# Clean build
dotnet clean && dotnet restore && dotnet build -c Debug

# Run application
dotnet run --project MTM_Inventory_Application.csproj

# Build for production
dotnet build -c Release

# Check for outdated packages
dotnet list package --outdated

# Find files by pattern
Get-ChildItem -Recurse -Filter "*.cs" | Select-String "pattern"
```

---

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
