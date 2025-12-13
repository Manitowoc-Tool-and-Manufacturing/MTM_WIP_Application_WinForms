# Code Style and Conventions

## Naming Conventions

### Classes
- **Forms**: `{Feature}Form` → `SettingsForm`, `PrintForm`
- **Controls**: `Control_{Feature}_{Purpose}` → `Control_QuickButtons`
- **DAOs**: `Dao_{Entity}` → `Dao_Inventory`, `Dao_QuickButtons`
- **Services**: `Service_{Purpose}` → `Service_ErrorHandler`
- **Helpers**: `Helper_{Purpose}` → `Helper_Database_StoredProcedure`
- **Models**: `Model_{Scope}_{Entity}` → `Model_Shared_UserUiColors`
- **Enums**: `Enum_{Name}` → `Enum_ErrorSeverity`

### Form Controls
`{FormName}_{ControlType}_{Name}_{Number?}`
- Example: `MainForm_MenuStrip_File`, `SettingsForm_Button_Save_1`

### Methods
- **Async methods**: `{Action}Async` → `LoadThemesAsync`, `SaveSettingsAsync`
- **DAO methods**: Descriptive verbs → `GetAllQuickButtonsAsync`, `InsertErrorReportAsync`
- **Event handlers**: `{ControlName}_{Event}` → `MainForm_Button_Save_Click`

### Variables
- **Private fields**: `_camelCase` → `_logger`, `_themeProvider`
- **Parameters**: `camelCase` → `userId`, `themeName`
- **Constants**: `SCREAMING_SNAKE_CASE` or `PascalCase` for public

## File Organization (#region MANDATORY)

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

## XML Documentation (MANDATORY)

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

## EditorConfig Settings
- File-scoped namespaces
- 4 spaces indentation
- LF line endings
- Nullable reference types enabled
- Implicit usings enabled
