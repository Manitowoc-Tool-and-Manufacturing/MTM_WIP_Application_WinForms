# UserControl API Contracts

**Feature**: Developer Tools Suite Integration  
**Sub-Feature ID**: 002-003-001  
**Date**: 2025-10-18

---

## Overview

This document defines the public API contracts for all developer tool UserControls. These controls integrate with the Settings form's hierarchical navigation system and must expose standard lifecycle methods for Settings form coordination.

---

## Standard UserControl Integration Pattern

All developer tool controls follow this pattern established by existing Settings controls (Control_Add_User, Control_Theme, etc.):

### Required Constructor Pattern
```csharp
public Control_Developer_{ToolName}()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    // Additional initialization
}
```

### Required Public Methods

#### ReloadAsync()
**Purpose**: Reload control data when user navigates to this control in Settings TreeView.

**Signature**:
```csharp
public async Task ReloadAsync()
```

**Contract**:
- Called by Settings form when TreeView node is selected
- Must be idempotent (safe to call multiple times)
- Should refresh data from database/files/cache
- Must handle connection failures gracefully (Service_ErrorHandler)
- Should update progress bar and status label via Settings form properties

**Example Implementation**:
```csharp
public async Task ReloadAsync()
{
    try
    {
        // Show progress
        _settingsForm?.ShowProgress("Loading schema...");
        
        // Load data
        await LoadDataAsync();
        
        // Hide progress
        _settingsForm?.HideProgress();
        _settingsForm?.UpdateStatus("Schema loaded successfully");
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
            retryAction: () => { ReloadAsync().Wait(); return true; },
            controlName: nameof(Control_Developer_SchemaInspector));
    }
}
```

### Optional Public Methods

#### ClearAsync()
**Purpose**: Clear control state when user navigates away.

**Signature**:
```csharp
public async Task ClearAsync()
```

**Contract**:
- Called by Settings form when user navigates to different category
- Should dispose of large objects (DataTable, images, etc.)
- Should pause background operations (timers, threads)
- Not required if control has no expensive state

---

## 1. Control_Developer_DebugDashboard

### Purpose
Real-time debugging dashboard for monitoring application activity (converted from DebugDashboardForm).

### Public API

#### Constructor
```csharp
public Control_Developer_DebugDashboard()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    _debugLog = new List<string>();
    _refreshTimer = new Timer { Interval = 1000 };
    _refreshTimer.Tick += RefreshTimer_Tick;
    _isCapturingDebug = true;
    
    LoadCurrentConfiguration();
}
```

#### ReloadAsync()
```csharp
/// <summary>
/// Reload debug configuration from Service_DebugTracer.
/// </summary>
public async Task ReloadAsync()
{
    LoadCurrentConfiguration();
    StartCapture();
    await Task.CompletedTask;
}
```

#### ClearAsync()
```csharp
/// <summary>
/// Clear debug output and stop capture.
/// </summary>
public async Task ClearAsync()
{
    _refreshTimer?.Stop();
    _debugLog?.Clear();
    txtDebugOutput.Clear();
    _isCapturingDebug = false;
    await Task.CompletedTask;
}
```

#### StartCapture()
```csharp
/// <summary>
/// Start capturing debug output from Service_DebugTracer.
/// </summary>
public void StartCapture()
{
    _isCapturingDebug = true;
    _refreshTimer.Start();
}
```

#### PauseCapture()
```csharp
/// <summary>
/// Pause capturing debug output (output frozen, app continues).
/// </summary>
public void PauseCapture()
{
    _isCapturingDebug = false;
    _refreshTimer.Stop();
}
```

#### SaveLog(string filePath)
```csharp
/// <summary>
/// Save current debug output to file.
/// </summary>
/// <param name="filePath">Full path to save file</param>
public void SaveLog(string filePath)
{
    File.WriteAllLines(filePath, _debugLog);
}
```

### Events
None (uses internal event handlers for UI controls).

### Properties
```csharp
public bool IsCapturing => _isCapturingDebug;
public int LogEntryCount => _debugLog?.Count ?? 0;
```

---

## 2. Control_Developer_ParameterPrefixMaintenance

### Purpose
CRUD interface for managing parameter prefix overrides.

### Public API

#### Constructor
```csharp
public Control_Developer_ParameterPrefixMaintenance()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

#### ReloadAsync()
```csharp
/// <summary>
/// Reload all active parameter prefix overrides from database.
/// </summary>
public async Task ReloadAsync()
{
    try
    {
        var result = await Dao_ParameterPrefixOverrides.GetAllActiveAsync();
        
        if (result.IsSuccess && result.Data != null)
        {
            dgvOverrides.DataSource = result.Data;
            // Configure DataGridView columns
        }
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
            retryAction: () => { ReloadAsync().Wait(); return true; },
            controlName: nameof(Control_Developer_ParameterPrefixMaintenance));
    }
}
```

#### AddOverrideAsync(Model_ParameterPrefixOverride override)
```csharp
/// <summary>
/// Add new parameter prefix override.
/// </summary>
/// <param name="override">Override details</param>
/// <returns>DaoResult with new OverrideId</returns>
public async Task<DaoResult<int>> AddOverrideAsync(Model_ParameterPrefixOverride override)
{
    // Validation
    if (string.IsNullOrWhiteSpace(override.ProcedureName))
        return DaoResult<int>.Failure("Procedure name is required");
    
    if (string.IsNullOrWhiteSpace(override.ParameterName))
        return DaoResult<int>.Failure("Parameter name is required");
    
    // Check if procedure exists (warning, not error)
    var procedureExists = await CheckProcedureExistsAsync(override.ProcedureName);
    if (!procedureExists)
    {
        var result = Service_ErrorHandler.ShowWarning(
            $"Procedure '{override.ProcedureName}' not found in INFORMATION_SCHEMA. Continue anyway?",
            "Procedure Not Found",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);
        
        if (result != DialogResult.Yes)
            return DaoResult<int>.Failure("Operation cancelled by user");
    }
    
    // Call DAO
    return await Dao_ParameterPrefixOverrides.AddAsync(override);
}
```

#### UpdateOverrideAsync(Model_ParameterPrefixOverride override)
```csharp
/// <summary>
/// Update existing parameter prefix override.
/// </summary>
/// <param name="override">Updated override details</param>
/// <returns>DaoResult indicating success/failure</returns>
public async Task<DaoResult> UpdateOverrideAsync(Model_ParameterPrefixOverride override)
{
    // Validation (same as Add)
    // Call DAO
    return await Dao_ParameterPrefixOverrides.UpdateAsync(override);
}
```

#### DeleteOverrideAsync(int overrideId)
```csharp
/// <summary>
/// Soft-delete parameter prefix override.
/// </summary>
/// <param name="overrideId">Override ID to delete</param>
/// <returns>DaoResult indicating success/failure</returns>
public async Task<DaoResult> DeleteOverrideAsync(int overrideId)
{
    var result = Service_ErrorHandler.ShowConfirmation(
        "Are you sure you want to delete this override?",
        "Confirm Delete");
    
    if (result != DialogResult.Yes)
        return DaoResult.Failure("Operation cancelled by user");
    
    return await Dao_ParameterPrefixOverrides.DeleteAsync(overrideId);
}
```

### Events
```csharp
public event EventHandler<OverrideChangedEventArgs>? OverrideAdded;
public event EventHandler<OverrideChangedEventArgs>? OverrideUpdated;
public event EventHandler<OverrideChangedEventArgs>? OverrideDeleted;
```

### Properties
```csharp
public int OverrideCount => dgvOverrides.Rows.Count;
public Model_ParameterPrefixOverride? SelectedOverride { get; }
```

---

## 3. Control_Developer_SchemaInspector

### Purpose
Read-only viewer for database schema metadata from INFORMATION_SCHEMA.

### Public API

#### Constructor
```csharp
public Control_Developer_SchemaInspector()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

#### ReloadAsync()
```csharp
/// <summary>
/// Reload schema metadata from INFORMATION_SCHEMA.
/// </summary>
public async Task ReloadAsync()
{
    try
    {
        await LoadTablesAsync();
        await LoadStoredProceduresAsync();
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
            retryAction: () => { ReloadAsync().Wait(); return true; },
            controlName: nameof(Control_Developer_SchemaInspector));
    }
}
```

#### LoadTablesAsync()
```csharp
/// <summary>
/// Load all tables from INFORMATION_SCHEMA.TABLES.
/// </summary>
private async Task LoadTablesAsync()
{
    var query = @"
        SELECT 
            TABLE_NAME, 
            TABLE_TYPE, 
            TABLE_ROWS, 
            ENGINE, 
            CREATE_TIME, 
            UPDATE_TIME, 
            TABLE_COMMENT
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = @DatabaseName
        ORDER BY TABLE_NAME";
    
    var parameters = new Dictionary<string, object>
    {
        ["DatabaseName"] = "MTM_WIP_Application_Winforms"
    };
    
    var result = await Helper_Database_Query.ExecuteQueryAsync(query, parameters);
    
    if (result.IsSuccess && result.Data != null)
    {
        dgvTables.DataSource = result.Data;
    }
}
```

#### LoadTableColumnsAsync(string tableName)
```csharp
/// <summary>
/// Load columns for selected table from INFORMATION_SCHEMA.COLUMNS.
/// </summary>
/// <param name="tableName">Name of table to inspect</param>
public async Task LoadTableColumnsAsync(string tableName)
{
    var query = @"
        SELECT 
            COLUMN_NAME,
            ORDINAL_POSITION,
            COLUMN_DEFAULT,
            IS_NULLABLE,
            DATA_TYPE,
            CHARACTER_MAXIMUM_LENGTH,
            NUMERIC_PRECISION,
            NUMERIC_SCALE,
            COLUMN_KEY,
            EXTRA,
            COLUMN_COMMENT
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_SCHEMA = @DatabaseName
        AND TABLE_NAME = @TableName
        ORDER BY ORDINAL_POSITION";
    
    var parameters = new Dictionary<string, object>
    {
        ["DatabaseName"] = "MTM_WIP_Application_Winforms",
        ["TableName"] = tableName
    };
    
    var result = await Helper_Database_Query.ExecuteQueryAsync(query, parameters);
    
    if (result.IsSuccess && result.Data != null)
    {
        dgvColumns.DataSource = result.Data;
    }
}
```

#### LoadStoredProceduresAsync()
```csharp
/// <summary>
/// Load all stored procedures from INFORMATION_SCHEMA.ROUTINES.
/// </summary>
private async Task LoadStoredProceduresAsync()
{
    // Similar to LoadTablesAsync but for ROUTINES
}
```

#### LoadProcedureParametersAsync(string procedureName)
```csharp
/// <summary>
/// Load parameters for selected procedure from INFORMATION_SCHEMA.PARAMETERS.
/// </summary>
/// <param name="procedureName">Name of procedure to inspect</param>
public async Task LoadProcedureParametersAsync(string procedureName)
{
    // Similar to LoadTableColumnsAsync but for PARAMETERS
}
```

### Events
```csharp
public event EventHandler<TableSelectedEventArgs>? TableSelected;
public event EventHandler<ProcedureSelectedEventArgs>? ProcedureSelected;
```

### Properties
```csharp
public int TableCount => dgvTables.Rows.Count;
public int ProcedureCount => dgvProcedures.Rows.Count;
public string? SelectedTableName { get; }
public string? SelectedProcedureName { get; }
```

---

## 4. Control_Developer_ProcedureCallHierarchy

### Purpose
Visualize stored procedure dependencies and C# call sites from analysis artifacts.

### Public API

#### Constructor
```csharp
public Control_Developer_ProcedureCallHierarchy()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

#### ReloadAsync()
```csharp
/// <summary>
/// Load procedure call hierarchy from JSON and CSV artifacts.
/// </summary>
public async Task ReloadAsync()
{
    try
    {
        await LoadCallHierarchyAsync();
        await LoadCallSitesAsync();
    }
    catch (FileNotFoundException ex)
    {
        ShowRegenerateArtifactsMessage(ex.FileName);
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
            retryAction: () => { ReloadAsync().Wait(); return true; },
            controlName: nameof(Control_Developer_ProcedureCallHierarchy));
    }
}
```

#### LoadCallHierarchyAsync()
```csharp
/// <summary>
/// Load procedure dependencies from call-hierarchy-complete.json.
/// </summary>
private async Task LoadCallHierarchyAsync()
{
    var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
        "Database", "call-hierarchy-complete.json");
    
    if (!File.Exists(jsonPath))
        throw new FileNotFoundException("Call hierarchy JSON not found", jsonPath);
    
    var jsonContent = await File.ReadAllTextAsync(jsonPath);
    _callHierarchy = JsonSerializer.Deserialize<Dictionary<string, ProcedureNode>>(jsonContent);
}
```

#### LoadCallSitesAsync()
```csharp
/// <summary>
/// Load C# call sites from STORED_PROCEDURE_CALLSITES.csv.
/// </summary>
private async Task LoadCallSitesAsync()
{
    var csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        "Database", "STORED_PROCEDURE_CALLSITES.csv");
    
    if (!File.Exists(csvPath))
        throw new FileNotFoundException("Call sites CSV not found", csvPath);
    
    // Parse CSV into List<Model_ProcedureCallSite>
}
```

#### SearchProcedure(string procedureName)
```csharp
/// <summary>
/// Search for procedure in call hierarchy.
/// </summary>
/// <param name="procedureName">Procedure name to search (supports partial match)</param>
public void SearchProcedure(string procedureName)
{
    // Autocomplete search
    // Display matching procedures
    // Show details when selected
}
```

#### ShowDependencyTree(string procedureName)
```csharp
/// <summary>
/// Display dependency tree for selected procedure.
/// </summary>
/// <param name="procedureName">Root procedure for tree</param>
public void ShowDependencyTree(string procedureName)
{
    // Build TreeView showing:
    // - Procedures called by this procedure (children)
    // - Procedures that call this procedure (parents)
    // - Circular dependency indicators
}
```

#### RegenerateArtifacts()
```csharp
/// <summary>
/// Launch PowerShell script to regenerate call hierarchy artifacts.
/// </summary>
public void RegenerateArtifacts()
{
    var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        "Database", "2-Trace-Complete-CallHierarchy-v2.ps1");
    
    var startInfo = new ProcessStartInfo
    {
        FileName = "pwsh.exe",
        Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"",
        UseShellExecute = false,
        CreateNoWindow = false
    };
    
    Process.Start(startInfo);
}
```

### Events
```csharp
public event EventHandler<ProcedureSearchEventArgs>? ProcedureSearched;
public event EventHandler<DependencyTreeEventArgs>? DependencyTreeShown;
```

### Properties
```csharp
public int TotalProcedures => _callHierarchy?.Count ?? 0;
public int RootProcedures => _callHierarchy?.Values.Count(p => p.IsRoot) ?? 0;
public int LeafProcedures => _callHierarchy?.Values.Count(p => p.IsLeaf) ?? 0;
public bool HasCircularDependencies => _callHierarchy?.Values.Any(p => p.HasCircularDependency) ?? false;
```

---

## 5. Control_Developer_CodeGenerator

### Purpose
Generate C# DAO method code from stored procedure definitions.

### Public API

#### Constructor
```csharp
public Control_Developer_CodeGenerator()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

#### ReloadAsync()
```csharp
/// <summary>
/// Load stored procedure list from INFORMATION_SCHEMA.
/// </summary>
public async Task ReloadAsync()
{
    try
    {
        await LoadProcedureListAsync();
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
            retryAction: () => { ReloadAsync().Wait(); return true; },
            controlName: nameof(Control_Developer_CodeGenerator));
    }
}
```

#### LoadProcedureListAsync()
```csharp
/// <summary>
/// Load all stored procedures for dropdown selection.
/// </summary>
private async Task LoadProcedureListAsync()
{
    var query = @"
        SELECT ROUTINE_NAME
        FROM INFORMATION_SCHEMA.ROUTINES
        WHERE ROUTINE_SCHEMA = @DatabaseName
        AND ROUTINE_TYPE = 'PROCEDURE'
        ORDER BY ROUTINE_NAME";
    
    var parameters = new Dictionary<string, object>
    {
        ["DatabaseName"] = "MTM_WIP_Application_Winforms"
    };
    
    var result = await Helper_Database_Query.ExecuteQueryAsync(query, parameters);
    
    if (result.IsSuccess && result.Data != null)
    {
        cmbProcedures.DataSource = result.Data;
        cmbProcedures.DisplayMember = "ROUTINE_NAME";
        cmbProcedures.ValueMember = "ROUTINE_NAME";
    }
}
```

#### GenerateDaoMethodAsync(string procedureName)
```csharp
/// <summary>
/// Generate C# DAO method code for selected stored procedure.
/// </summary>
/// <param name="procedureName">Stored procedure to generate code for</param>
/// <returns>Generated C# code</returns>
public async Task<string> GenerateDaoMethodAsync(string procedureName)
{
    // 1. Query INFORMATION_SCHEMA.PARAMETERS
    var parameters = await GetProcedureParametersAsync(procedureName);
    
    // 2. Generate method name (e.g., "inv_inventory_Add_Item" → "AddItemAsync")
    var methodName = GenerateMethodName(procedureName);
    
    // 3. Generate method signature
    var signature = GenerateMethodSignature(methodName, parameters);
    
    // 4. Generate XML documentation
    var xmlDocs = GenerateXmlDocumentation(procedureName, parameters);
    
    // 5. Generate parameter dictionary
    var paramDict = GenerateParameterDictionary(parameters);
    
    // 6. Generate Helper_Database_StoredProcedure call
    var helperCall = GenerateHelperCall(procedureName, paramDict);
    
    // 7. Generate error handling
    var errorHandling = GenerateErrorHandling();
    
    // 8. Combine all parts
    return $@"{xmlDocs}
{signature}
{{
    try
    {{
        {paramDict}
        
        {helperCall}
        
        if (result.IsSuccess)
        {{
            return DaoResult<DataTable>.Success(result.Data);
        }}
        else
        {{
            LoggingUtility.LogApplicationError(result.Exception, result.StatusMessage);
            return DaoResult<DataTable>.Failure(result.StatusMessage);
        }}
    }}
    catch (Exception ex)
    {{
        {errorHandling}
    }}
}}";
}
```

#### CopyToClipboard(string code)
```csharp
/// <summary>
/// Copy generated code to clipboard.
/// </summary>
/// <param name="code">C# code to copy</param>
public void CopyToClipboard(string code)
{
    Clipboard.SetText(code);
    Service_ErrorHandler.ShowInformation(
        "Code copied to clipboard. Paste into DAO class.",
        "Copy Success");
}
```

### Events
```csharp
public event EventHandler<CodeGeneratedEventArgs>? CodeGenerated;
```

### Properties
```csharp
public int ProcedureCount => cmbProcedures.Items.Count;
public string? SelectedProcedureName => cmbProcedures.SelectedValue?.ToString();
public string GeneratedCode => txtGeneratedCode.Text;
```

---

## 6. Control_Database (Refactored)

### Purpose
Existing database connection strength monitor, moved into Developer category.

### Public API

**No changes to public API**. Control simply moves from "Database" category to "Developer" category in Settings TreeView structure.

#### Existing ReloadAsync()
```csharp
public async Task ReloadAsync()
{
    // Existing implementation unchanged
    await TestConnectionAsync();
    UpdateConnectionStrengthUI();
}
```

---

## Settings Form Integration

### TreeView Structure

```
Settings
├── User
│   └── Add User
├── Theme
│   └── Theme Settings
├── About
│   └── About Application
└── Developer (NEW - only visible if IsDeveloper = true)
    ├── Debug Dashboard
    ├── Parameter Prefix Maintenance
    ├── Schema Inspector
    ├── Procedure Call Hierarchy
    ├── Code Generator
    └── Database (MOVED from Database category)
```

### SettingsForm Changes

```csharp
// In SettingsForm.cs

private void LoadTreeViewNodes()
{
    // Existing nodes...
    
    // Add Developer node if user has Developer role
    if (Model_AppVariables.CurrentUser.IsDeveloper)
    {
        var developerNode = new TreeNode("Developer");
        developerNode.Nodes.Add("Debug Dashboard");
        developerNode.Nodes.Add("Parameter Prefix Maintenance");
        developerNode.Nodes.Add("Schema Inspector");
        developerNode.Nodes.Add("Procedure Call Hierarchy");
        developerNode.Nodes.Add("Code Generator");
        developerNode.Nodes.Add("Database"); // Moved from Database category
        
        treeViewSettings.Nodes.Add(developerNode);
    }
}

private async void TreeViewSettings_AfterSelect(object sender, TreeViewEventArgs e)
{
    // Clear current control
    if (_currentControl != null)
    {
        if (_currentControl is IAsyncControl asyncControl)
            await asyncControl.ClearAsync();
        
        pnlContent.Controls.Remove(_currentControl);
    }
    
    // Load selected control
    UserControl? newControl = e.Node.Text switch
    {
        "Debug Dashboard" => new Control_Developer_DebugDashboard(),
        "Parameter Prefix Maintenance" => new Control_Developer_ParameterPrefixMaintenance(),
        "Schema Inspector" => new Control_Developer_SchemaInspector(),
        "Procedure Call Hierarchy" => new Control_Developer_ProcedureCallHierarchy(),
        "Code Generator" => new Control_Developer_CodeGenerator(),
        _ => null
    };
    
    if (newControl != null)
    {
        _currentControl = newControl;
        newControl.Dock = DockStyle.Fill;
        pnlContent.Controls.Add(newControl);
        
        if (newControl is IAsyncControl asyncControl)
            await asyncControl.ReloadAsync();
    }
}
```

---

## Testing Checklist

### Integration Tests
- [ ] Settings form shows Developer category for users with IsDeveloper = true
- [ ] Settings form hides Developer category for users without IsDeveloper
- [ ] All 6 developer tools load without exceptions
- [ ] TreeView navigation between tools clears previous control state
- [ ] Progress bar and status label update during operations
- [ ] Core_Themes.ApplyDpiScaling works on all controls

### Individual Control Tests
- [ ] DebugDashboard: Capture/pause/resume/save log workflow
- [ ] ParameterPrefixMaintenance: Add/edit/delete override workflow
- [ ] SchemaInspector: Load tables, select table, view columns
- [ ] ProcedureCallHierarchy: Search procedure, view dependencies, regenerate artifacts
- [ ] CodeGenerator: Select procedure, generate code, copy to clipboard
- [ ] Database: Connection strength test (existing functionality)

### Error Handling Tests
- [ ] Connection failure in SchemaInspector shows retry dialog
- [ ] Missing artifacts in ProcedureCallHierarchy shows regenerate message
- [ ] Duplicate override in ParameterPrefixMaintenance shows validation error
- [ ] Invalid procedure selection in CodeGenerator shows friendly error

---

## Deployment Notes

1. All UserControls inherit from `System.Windows.Forms.UserControl`
2. All UserControls use WinForms designer (`.Designer.cs` files)
3. No AXAML/XAML/Avalonia patterns (this is WinForms application)
4. Core_Themes integration required in all constructors
5. Service_ErrorHandler used for all exception handling
6. Settings form progress bar and status label accessed via properties
