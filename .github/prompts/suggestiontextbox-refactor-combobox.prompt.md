# SuggestionTextBox Migration - Replace ComboBox Controls

## OBJECTIVE
Migrate existing ComboBox controls to SuggestionTextBox in an existing control/form. This prompt replaces ComboBox-based selection with intelligent suggestion support while preserving all business logic and improving user experience.

## INPUT REQUIREMENTS

Before executing this prompt, you need:
1. **Target File**: Full path to the .cs file to refactor (e.g., `c:\...\Controls\SettingsForm\Control_Add_Location.cs`)
2. **ComboBox Controls to Replace**: List of ComboBox control names and their data types
3. **Confirmation**: User approval to proceed with refactoring

## EXECUTION STEPS

### Step 1: Read Required Files

<tool>read_file</tool> on these files:
```
c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.github\instructions\suggestiontextbox-implementation.instructions.md
c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.specify\memory\constitution.md
{target_file}
{target_file_designer} (if exists)
```

### Step 2: Analyze Current Implementation

Identify in the target file:
- [ ] All ComboBox control declarations
- [ ] ComboBox data loading methods (LoadXXXComboBox, SetupXXXDataTable, etc.)
- [ ] ComboBox event handlers (SelectedIndexChanged, SelectionChangeCommitted, etc.)
- [ ] ComboBox databinding code (DataSource, DisplayMember, ValueMember)
- [ ] Any ComboBox-related validation logic

### Step 3: Create Migration Plan

For each ComboBox identified, determine:
- **Control Name**: Original ComboBox name
- **Data Type**: Part, Operation, Location, ItemType, ColorCode, or Custom
- **Data Source**: Where ComboBox data comes from (DAO method, stored procedure, static list)
- **Event Logic**: What happens when user makes selection
- **Dependencies**: What other code depends on this ComboBox

### Step 4: Backup Original Code

Create commented-out backup section at bottom of file:
```csharp
#region ORIGINAL_COMBOBOX_CODE_BACKUP

/*
 * BACKUP DATE: {current_date}
 * MIGRATION: ComboBox to SuggestionTextBox
 * 
 * Original ComboBox declarations:
 * {paste original field declarations}
 * 
 * Original data loading methods:
 * {paste original LoadComboBox methods}
 * 
 * Original event handlers:
 * {paste original SelectedIndexChanged handlers}
 * 
 * Original databinding:
 * {paste original DataSource assignments}
 */

#endregion
```

### Step 5: Modify Designer.cs File

For each ComboBox being replaced:

**Remove:**
```csharp
private System.Windows.Forms.ComboBox MyControl_ComboBox_Part;

// In InitializeComponent():
this.MyControl_ComboBox_Part = new System.Windows.Forms.ComboBox();
this.MyControl_ComboBox_Part.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
this.MyControl_ComboBox_Part.FormattingEnabled = true;
// ... other properties
```

**Add:**
```csharp
private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox MyControl_SuggestionTextBox_Part;

// In InitializeComponent():
this.MyControl_SuggestionTextBox_Part = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
this.MyControl_SuggestionTextBox_Part.Name = "MyControl_SuggestionTextBox_Part";
this.MyControl_SuggestionTextBox_Part.TabIndex = {same_as_before};
// ... copy other layout properties from original ComboBox
```

### Step 6: Refactor .cs File

#### 6.1: Add Using Statement (if not present)
```csharp
using MTM_WIP_Application_Winforms.Controls.Shared;
```

#### 6.2: Remove ComboBox Loading Methods

**Remove methods like:**
```csharp
private async void LoadPartComboBox()
{
    var result = await Dao_Part.GetAllPartsAsync();
    MyControl_ComboBox_Part.DataSource = result.Data;
    MyControl_ComboBox_Part.DisplayMember = "PartID";
    MyControl_ComboBox_Part.ValueMember = "ID";
}

private void SetupPartDataTable()
{
    // ComboBox data table setup
}
```

**Add SuggestionTextBox Data Provider:**
```csharp
/// <summary>
/// Data provider for part SuggestionTextBox.
/// Returns list of all part IDs from database.
/// </summary>
private async Task<List<string>> GetPartSuggestionsAsync() =>
    await GetSuggestionsFromDatabaseAsync("md_part_ids_Get_All", "PartID", "parts");

/// <summary>
/// Generic method to load suggestions from database.
/// </summary>
private async Task<List<string>> GetSuggestionsFromDatabaseAsync(
    string procedureName, 
    string columnName, 
    string dataTypeName)
{
    try
    {
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            procedureName,
            null);

        if (!result.IsSuccess || result.Data == null)
        {
            Service_ErrorHandler.ShowWarning(result.ErrorMessage ?? $"Failed to load {dataTypeName}");
            return new List<string>();
        }

        var suggestions = new List<string>(result.Data.Rows.Count);
        foreach (DataRow row in result.Data.Rows)
        {
            var value = row[columnName];
            if (value != null && value != DBNull.Value)
            {
                suggestions.Add(value.ToString() ?? string.Empty);
            }
        }

        return suggestions;
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
            contextData: new Dictionary<string, object>
            {
                ["Procedure"] = procedureName,
                ["DataType"] = dataTypeName
            },
            controlName: nameof(MyControl),
            callerName: nameof(GetSuggestionsFromDatabaseAsync));
        return new List<string>();
    }
}
```

#### 6.3: Update Constructor/Initialization

**Remove:**
```csharp
LoadPartComboBox();
MyControl_ComboBox_Part.SelectedIndexChanged += MyControl_ComboBox_Part_SelectedIndexChanged;
```

**Add:**
```csharp
private void ConfigureSuggestionTextBoxes()
{
    Helper_SuggestionTextBox.ConfigureForPartNumbers(
        MyControl_SuggestionTextBox_Part,
        GetPartSuggestionsAsync,
        enableF4: true);
}

private void WireUpEventHandlers()
{
    MyControl_SuggestionTextBox_Part.SuggestionSelected += MyControl_SuggestionTextBox_Part_SuggestionSelected;
}

// In constructor:
ConfigureSuggestionTextBoxes();
WireUpEventHandlers();
```

#### 6.4: Convert Event Handlers

**Old ComboBox Handler:**
```csharp
private void MyControl_ComboBox_Part_SelectedIndexChanged(object sender, EventArgs e)
{
    if (MyControl_ComboBox_Part.SelectedValue == null)
        return;

    int partId = (int)MyControl_ComboBox_Part.SelectedValue;
    var part = GetPartById(partId);
    
    // ... business logic
}
```

**New SuggestionTextBox Handler:**
```csharp
private async void MyControl_SuggestionTextBox_Part_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
{
    string selectedPart = e.SelectedValue;

    try
    {
        LoggingUtility.Log($"[{nameof(MyControl)}] Part selected: {selectedPart}");

        var result = await Dao_Part.GetPartByNumberAsync(selectedPart);

        if (!result.IsSuccess)
        {
            Service_ErrorHandler.ShowWarning($"Part '{selectedPart}' could not be loaded: {result.ErrorMessage}");
            return;
        }

        if (result.Data == null)
        {
            Service_ErrorHandler.ShowWarning($"Part '{selectedPart}' was not found.");
            return;
        }

        // ... preserve original business logic using result.Data
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
            contextData: new Dictionary<string, object>
            {
                ["SelectedPart"] = selectedPart
            },
            controlName: nameof(MyControl),
            callerName: nameof(MyControl_SuggestionTextBox_Part_SuggestionSelected));
    }
}
```

#### 6.5: Update Dispose Method

**Remove:**
```csharp
if (MyControl_ComboBox_Part != null)
{
    MyControl_ComboBox_Part.SelectedIndexChanged -= MyControl_ComboBox_Part_SelectedIndexChanged;
}
```

**Add:**
```csharp
if (MyControl_SuggestionTextBox_Part != null)
{
    MyControl_SuggestionTextBox_Part.SuggestionSelected -= MyControl_SuggestionTextBox_Part_SuggestionSelected;
}
```

### Step 7: Remove Obsolete Code

Identify and remove:
- [ ] Old ComboBox loading methods (no longer called)
- [ ] DataTable setup methods specific to ComboBox
- [ ] ComboBox-specific validation logic (if replaced by SuggestionTextBox validation)
- [ ] Unused using statements
- [ ] Commented-out code blocks unrelated to migration

**IMPORTANT:** Only remove code that is TRULY obsolete. Preserve:
- Shared utility methods
- Business logic methods
- Validation that still applies
- Event handlers for other controls

### Step 8: Reorganize Regions

Ensure proper #region structure per constitution:
```csharp
#region Fields
#region Events
#region Constructors
#region Initialization
#region Events (event handlers)
#region Data Providers
#region Validation
#region Methods
#region Helpers
#region Cleanup
```

Move methods to appropriate regions:
- `ConfigureSuggestionTextBoxes()` → Initialization
- `WireUpEventHandlers()` → Initialization
- `GetXXXSuggestionsAsync()` → Data Providers
- `GetSuggestionsFromDatabaseAsync()` → Data Providers
- `XXX_SuggestionSelected()` → Events
- `Dispose()` → Cleanup

### Step 9: Enhance Error Handling

Audit all exception handling to ensure constitution compliance:

**Replace:**
```csharp
catch (Exception ex)
{
    MessageBox.Show(ex.Message); // ❌ FORBIDDEN
}
```

**With:**
```csharp
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
        contextData: new Dictionary<string, object>
        {
            ["Operation"] = "DescriptiveOperationName",
            ["InputData"] = relevantData
        },
        controlName: nameof(MyControl),
        callerName: nameof(MethodName));
}
```

### Step 10: Build and Test

```powershell
dotnet build
```

Verify:
- [ ] No compilation errors
- [ ] No warnings related to changes
- [ ] Designer file properly updated

## OUTPUT FORMAT

After execution, provide:

### 1. Migration Summary
```
ComboBox Controls Migrated: 3
- MyControl_ComboBox_Part → MyControl_SuggestionTextBox_Part (PartNumber type)
- MyControl_ComboBox_Operation → MyControl_SuggestionTextBox_Operation (Operation type)
- MyControl_ComboBox_Location → MyControl_SuggestionTextBox_Location (Location type)

Code Removed:
- 3 ComboBox loading methods (LoadXXXComboBox)
- 3 SelectedIndexChanged event handlers
- 2 DataTable setup methods
- 45 lines of obsolete code total

Code Added:
- 1 generic GetSuggestionsFromDatabaseAsync method
- 3 specific data provider methods
- 3 SuggestionSelected event handlers
- ConfigureSuggestionTextBoxes method
- WireUpEventHandlers method
```

### 2. Constitution Compliance Report
```
✅ Error Handling: All exceptions through Service_ErrorHandler
✅ Logging: All operations logged via LoggingUtility
✅ Async Patterns: All database operations properly awaited
✅ DAO Pattern: All data access uses Model_Dao_Result<T>
✅ Event Cleanup: Proper unsubscription in Dispose
✅ Region Organization: Proper #region structure maintained
✅ XML Documentation: All public methods documented
✅ Null Safety: Null-conditional operators used throughout
```

### 3. Testing Checklist
```
Manual Testing Required:
[ ] F4 key shows full list for each SuggestionTextBox
[ ] Down arrow (empty field) shows full list
[ ] Typing filters suggestions correctly
[ ] Wildcard % character works (where enabled)
[ ] Selection triggers correct business logic
[ ] Form save/reset operations work correctly
[ ] No memory leaks (events properly unsubscribed)
[ ] Error messages are user-friendly
```

### 4. Next Steps
```
✅ Migration complete - file ready for testing
✅ Build succeeded with no errors
✅ Constitution compliance verified
✅ Run suggestiontextbox-constitution-audit.prompt.md to generate improvement suggestions

SUGGESTED NEXT PROMPT:
If you have other forms with ComboBox controls, run:
  "suggestiontextbox-refactor-combobox.prompt.md" on next file

If you want to audit all SuggestionTextBox implementations, run:
  "suggestiontextbox-constitution-audit.prompt.md"

If you're done with migrations, consider:
  Manual testing of migrated controls
  Code review by team member
  User acceptance testing
```

## EXAMPLE USAGE

**User Input:**
```
Target File: c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Controls\SettingsForm\Control_Add_Location.cs
ComboBox Controls:
  - Control_Add_Location_ComboBox_Building (Building type - custom)
Proceed: Yes
```

**AI Execution:**
1. Reads target file and instructions
2. Identifies ComboBox usage pattern
3. Creates backup section
4. Replaces ComboBox with SuggestionTextBox in Designer
5. Removes LoadBuildingComboBox method
6. Adds GetBuildingSuggestionsAsync data provider
7. Converts SelectedIndexChanged to SuggestionSelected handler
8. Removes obsolete code
9. Reorganizes regions
10. Enhances error handling
11. Builds successfully

## VERSION
v1.0 - 2025-11-15

## RELATED PROMPTS
- `suggestiontextbox-implement-new.prompt.md` - Create new controls with SuggestionTextBox
- `suggestiontextbox-refactor-textbox.prompt.md` - Migrate TextBox controls
- `suggestiontextbox-constitution-audit.prompt.md` - Audit implementations
