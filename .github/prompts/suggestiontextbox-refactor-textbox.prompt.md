# SuggestionTextBox Migration - Replace TextBox Controls

## OBJECTIVE
Migrate existing TextBox controls to SuggestionTextBox where the TextBox is being used for master data selection (Part numbers, Operations, Locations, etc.) rather than free-form text input. This improves user experience with autocomplete and validation while preserving all business logic.

## INPUT REQUIREMENTS

Before executing this prompt, you need:
1. **Target File**: Full path to the .cs file to refactor
2. **TextBox Controls to Assess**: List of TextBox control names
3. **Confirmation**: Determine which TextBoxes should become SuggestionTextBoxes - This MUST be verified by the user!

## DECISION CRITERIA

### ✅ Convert TextBox to SuggestionTextBox IF:
- TextBox is used to select from master data (parts, operations, locations, etc.)
- Validation checks if entered value exists in database
- User needs to know what values are available
- Text is typically uppercase or standardized format
- Field has limited valid values (even if large list)

### ❌ Keep as TextBox IF:
- Free-form text entry (descriptions, notes, names)
- Numeric values only (use NumericUpDown instead)
- Dates (use DateTimePicker instead)
- Multi-line text entry
- Calculated/display-only fields (use Label instead)

## EXECUTION STEPS

### Step 1: Read Required Files

<tool>read_file</tool> on:
```
c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.github\instructions\suggestiontextbox-implementation.instructions.md
c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.specify\memory\constitution.md
{target_file}
{target_file_designer} (if exists)
```

### Step 2: Analyze TextBox Usage

For each TextBox in the file, determine:

**Is this TextBox used for selection?**
```csharp
// Look for patterns like:

// Pattern 1: Validation against database
private async void PartTextBox_Leave(object sender, EventArgs e)
{
    string part = PartTextBox.Text;
    var exists = await Dao_Part.PartExistsAsync(part);
    if (!exists) {
        MessageBox.Show("Part not found!");
    }
}
// ✅ CONVERT to SuggestionTextBox

// Pattern 2: Loading related data on entry
private async void PartTextBox_Leave(object sender, EventArgs e)
{
    string part = PartTextBox.Text;
    var result = await Dao_Part.GetPartByNumberAsync(part);
    if (result.IsSuccess) {
        LoadPartDetails(result.Data);
    }
}
// ✅ CONVERT to SuggestionTextBox

// Pattern 3: Free-form text entry
private void DescriptionTextBox_Leave(object sender, EventArgs e)
{
    // No validation, just trimming
    DescriptionTextBox.Text = DescriptionTextBox.Text.Trim();
}
// ❌ KEEP as TextBox

// Pattern 4: Uppercase transformation
private void PartTextBox_Leave(object sender, EventArgs e)
{
    PartTextBox.Text = PartTextBox.Text.ToUpper();
}
// ✅ CONVERT to SuggestionTextBox (handles auto-capitalization)
```

### Step 3: Create Conversion Plan

For each TextBox to convert, document:
- **Control Name**: Original TextBox name
- **Data Type**: Part, Operation, Location, etc.
- **Current Validation**: What Leave/Validating events check
- **Current Events**: What happens on TextChanged, Leave, Validating, etc.
- **Related Logic**: Business logic triggered by this TextBox

### Step 4: Backup Original Code

Add to bottom of file:
```csharp
#region ORIGINAL_TEXTBOX_CODE_BACKUP

/*
 * BACKUP DATE: {current_date}
 * MIGRATION: TextBox to SuggestionTextBox
 * 
 * Original TextBox event handlers:
 * {paste original Leave/Validating/TextChanged handlers}
 * 
 * Original validation logic:
 * {paste validation methods}
 * 
 * Original business logic triggers:
 * {paste related logic}
 */

#endregion
```

### Step 5: Modify Designer.cs File

For each TextBox being replaced:

**Remove:**
```csharp
private System.Windows.Forms.TextBox MyControl_TextBox_Part;

// In InitializeComponent():
this.MyControl_TextBox_Part = new System.Windows.Forms.TextBox();
this.MyControl_TextBox_Part.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
this.MyControl_TextBox_Part.MaxLength = 50;
// ... other properties
```

**Add:**
```csharp
private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox MyControl_SuggestionTextBox_Part;

// In InitializeComponent():
this.MyControl_SuggestionTextBox_Part = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
this.MyControl_SuggestionTextBox_Part.Name = "MyControl_SuggestionTextBox_Part";
this.MyControl_SuggestionTextBox_Part.TabIndex = {same_as_before};
// ... copy other layout properties
```

### Step 6: Refactor .cs File

#### 6.1: Remove TextBox Event Handlers

**Remove handlers like:**
```csharp
private async void PartTextBox_Leave(object sender, EventArgs e)
{
    string part = PartTextBox.Text.Trim().ToUpper();
    
    if (string.IsNullOrEmpty(part))
        return;

    var result = await Dao_Part.GetPartByNumberAsync(part);
    
    if (!result.IsSuccess || result.Data == null)
    {
        MessageBox.Show($"Part '{part}' not found!");
        PartTextBox.Clear();
        PartTextBox.Focus();
        return;
    }

    LoadPartDetails(result.Data);
}

private void PartTextBox_TextChanged(object sender, EventArgs e)
{
    // Clear dependent fields when part changes
    ClearOperationField();
}

private void PartTextBox_Validating(object sender, CancelEventArgs e)
{
    if (string.IsNullOrEmpty(PartTextBox.Text))
    {
        errorProvider.SetError(PartTextBox, "Part number required");
        e.Cancel = true;
    }
}
```

#### 6.2: Add SuggestionTextBox Configuration

**Add to Initialization region:**
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
```

#### 6.3: Add Data Provider

**Add to Data Providers region:**
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

#### 6.4: Add SuggestionSelected Event Handler

**Add to Events region:**
```csharp
/// <summary>
/// Handles part selection from SuggestionTextBox.
/// Replaces previous TextBox Leave event validation.
/// </summary>
private async void MyControl_SuggestionTextBox_Part_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
{
    string selectedPart = e.SelectedValue;

    try
    {
        LoggingUtility.Log($"[{nameof(MyControl)}] Part selected: {selectedPart}");

        // Load part details (preserving original business logic)
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

        // Original business logic preserved:
        LoadPartDetails(result.Data);
        
        // If original TextBox_TextChanged cleared dependent fields, preserve that:
        ClearOperationField();
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
            contextData: new Dictionary<string, object>
            {
                ["SelectedPart"] = selectedPart,
                ["Operation"] = "LoadPartDetails"
            },
            controlName: nameof(MyControl),
            callerName: nameof(MyControl_SuggestionTextBox_Part_SuggestionSelected));
    }
}
```

#### 6.5: Remove Obsolete Validation

**Remove from constructor:**
```csharp
// Old event subscriptions
PartTextBox.Leave += PartTextBox_Leave;
PartTextBox.TextChanged += PartTextBox_TextChanged;
PartTextBox.Validating += PartTextBox_Validating;
```

**Remove validation methods:**
```csharp
// Remove entire method if only used for TextBox validation
private bool ValidatePartNumber()
{
    if (string.IsNullOrEmpty(PartTextBox.Text))
    {
        errorProvider.SetError(PartTextBox, "Required");
        return false;
    }
    return true;
}
```

**Note:** If validation method is used elsewhere, preserve it but remove TextBox-specific logic.

#### 6.6: Update Save/Submit Methods

**Old pattern:**
```csharp
private async void SaveButton_Click(object sender, EventArgs e)
{
    // Validate all fields
    if (!ValidatePartNumber()) return;
    if (!ValidateOperation()) return;
    
    string part = PartTextBox.Text.Trim().ToUpper();
    string operation = OperationTextBox.Text.Trim().ToUpper();
    
    // Save logic...
}
```

**New pattern:**
```csharp
private async void SaveButton_Click(object sender, EventArgs e)
{
    try
    {
        // Validation now simpler - SuggestionTextBox only allows valid selections
        if (string.IsNullOrWhiteSpace(MyControl_SuggestionTextBox_Part.Text))
        {
            Service_ErrorHandler.ShowWarning("Please select a part number.");
            MyControl_SuggestionTextBox_Part.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(MyControl_SuggestionTextBox_Operation.Text))
        {
            Service_ErrorHandler.ShowWarning("Please select an operation.");
            MyControl_SuggestionTextBox_Operation.Focus();
            return;
        }

        // Text is already uppercase from SuggestionTextBox
        string part = MyControl_SuggestionTextBox_Part.Text;
        string operation = MyControl_SuggestionTextBox_Operation.Text;

        // Save logic preserved...
        await SaveDataAsync(part, operation);
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
            contextData: new Dictionary<string, object>
            {
                ["Operation"] = "SaveData"
            },
            controlName: nameof(MyControl),
            callerName: nameof(SaveButton_Click));
    }
}
```

#### 6.7: Update Dispose

**Remove:**
```csharp
if (PartTextBox != null)
{
    PartTextBox.Leave -= PartTextBox_Leave;
    PartTextBox.TextChanged -= PartTextBox_TextChanged;
    PartTextBox.Validating -= PartTextBox_Validating;
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
- [ ] TextBox Leave event handlers (replaced by SuggestionSelected)
- [ ] TextBox Validating event handlers (built into SuggestionTextBox)
- [ ] Text case conversion code (ToUpper/ToLower - handled automatically)
- [ ] Existence validation methods (SuggestionTextBox only shows valid items)
- [ ] ErrorProvider logic for these TextBoxes (validation built-in)

**PRESERVE:**
- Business logic methods (LoadPartDetails, etc.)
- Save/submit methods (just update to use new control names)
- Other validation that applies beyond selection

### Step 8: Reorganize Regions

Ensure proper structure:
```csharp
#region Fields
#region Events
#region Constructors
#region Initialization
  ConfigureSuggestionTextBoxes()
  WireUpEventHandlers()
#region Events
  XXX_SuggestionSelected event handlers
#region Data Providers
  GetXXXSuggestionsAsync() methods
  GetSuggestionsFromDatabaseAsync() generic method
#region Validation
  Any remaining validation methods
#region Methods
#region Helpers
#region Cleanup
  Dispose() method
```

### Step 9: Enhance Constitution Compliance

#### 9.1: Replace MessageBox.Show
```csharp
// ❌ Remove
MessageBox.Show("Part not found!");

// ✅ Replace with
Service_ErrorHandler.ShowWarning("Part not found!");
```

#### 9.2: Add Structured Logging
```csharp
// Add logging for user actions
LoggingUtility.Log($"[{nameof(MyControl)}] Part selected: {selectedPart}");
LoggingUtility.Log($"[{nameof(MyControl)}] Data saved successfully");
```

#### 9.3: Improve Error Context
```csharp
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
        contextData: new Dictionary<string, object>
        {
            ["Operation"] = "SpecificOperation",
            ["InputPart"] = selectedPart,
            ["User"] = Model_Application_Variables.User
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
- [ ] Control names updated throughout
- [ ] Event handlers properly wired

### Step 11: Add Proposed Improvements Section

```csharp
#region PROPOSED_IMPROVEMENTS

/*
 * PROPOSED IMPROVEMENTS BEYOND SCOPE OF THIS REFACTOR
 * Generated: {current_date}
 * 
 * 1. ENHANCED USER EXPERIENCE
 *    - Add "Recently Used" quick-select for frequently entered parts
 *    - Implement barcode scanner integration for part field
 *    - Add visual indicators for required vs optional fields
 * 
 * 2. VALIDATION ENHANCEMENTS
 *    - Add cross-field validation (Part + Operation compatibility check)
 *    - Implement real-time availability checking
 *    - Add warnings for inactive/discontinued parts
 * 
 * 3. PERFORMANCE OPTIMIZATION
 *    - Cache suggestion data for frequently used fields
 *    - Implement progressive loading for large datasets
 *    - Add suggestion pre-fetching on form load
 * 
 * 4. ACCESSIBILITY
 *    - Add ARIA labels for screen readers
 *    - Implement keyboard shortcuts for common actions
 *    - Add color-blind friendly visual indicators
 * 
 * 5. DATA INTEGRITY
 *    - Add change tracking to warn on unsaved edits
 *    - Implement optimistic locking for concurrent edits
 *    - Add audit trail for data modifications
 * 
 * 6. CODE QUALITY
 *    - Extract common patterns to base class
 *    - Add unit tests for data providers
 *    - Implement integration tests for workflows
 */

#endregion
```

## OUTPUT FORMAT

### 1. Migration Summary
```
TextBox Controls Migrated: 2
- MyControl_TextBox_Part → MyControl_SuggestionTextBox_Part (PartNumber type)
- MyControl_TextBox_Operation → MyControl_SuggestionTextBox_Operation (Operation type)

TextBox Controls Preserved: 3
- MyControl_TextBox_Description (Free-form text)
- MyControl_TextBox_Notes (Multi-line text)
- MyControl_TextBox_Quantity (Should be NumericUpDown)

Code Removed:
- 2 TextBox Leave event handlers
- 2 TextBox Validating event handlers  
- 1 TextBox TextChanged handler
- 3 validation methods (now built into SuggestionTextBox)
- 2 ToUpper() conversion calls (automatic now)
- 38 lines of obsolete validation code

Code Added:
- 1 generic GetSuggestionsFromDatabaseAsync method
- 2 specific data provider methods
- 2 SuggestionSelected event handlers
- ConfigureSuggestionTextBoxes method
- WireUpEventHandlers method
```

### 2. Constitution Compliance Report
```
✅ Error Handling: Replaced 3 MessageBox.Show with Service_ErrorHandler
✅ Logging: Added LoggingUtility calls for user actions
✅ Async Patterns: All database operations properly awaited
✅ Context Enrichment: Added contextData dictionaries to exception handlers
✅ Event Cleanup: Proper unsubscription in Dispose
✅ Region Organization: Proper #region structure maintained
✅ XML Documentation: All new methods documented
```

### 3. Business Logic Verification
```
Preserved Functionality:
✅ Part selection triggers LoadPartDetails (same as before)
✅ Part change clears operation field (same as before)
✅ Save validates required fields (improved validation)
✅ Reset clears all fields (same as before)

Improved Functionality:
✨ Autocomplete shows available parts (new)
✨ F4 key shows full part list (new)
✨ Automatic uppercase conversion (previously manual)
✨ Validation happens during selection (previously on Leave)
✨ Better error messages through Service_ErrorHandler
```

### 4. Testing Checklist
```
Manual Testing Required:
[ ] F4 key shows full list
[ ] Typing filters suggestions
[ ] Selection loads related data correctly
[ ] All business logic preserved
[ ] Save operation works
[ ] Reset operation works
[ ] Tab order maintained
[ ] Focus behavior correct
```

### 5. Next Steps
```
✅ Migration complete
✅ Build succeeded
✅ Constitution compliance verified

RECOMMENDED ACTIONS:
1. Manual testing of migrated controls
2. Verify user workflows unchanged
3. Check if any TextBox controls should be NumericUpDown (Quantity field)

SUGGESTED NEXT PROMPT:
If more files need TextBox migration:
  "suggestiontextbox-refactor-textbox.prompt.md" on next file

If you want to convert remaining TextBox to NumericUpDown:
  Create new prompt for TextBox → NumericUpDown migration

If done with migrations:
  "suggestiontextbox-constitution-audit.prompt.md" to verify all implementations
```

## VERSION
v1.0 - 2025-11-15

## RELATED PROMPTS
- `suggestiontextbox-implement-new.prompt.md` - Create new controls
- `suggestiontextbox-refactor-combobox.prompt.md` - Migrate ComboBox controls
- `suggestiontextbox-constitution-audit.prompt.md` - Audit implementations
