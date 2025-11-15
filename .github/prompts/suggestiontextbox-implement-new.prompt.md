# SuggestionTextBox Implementation - New Control/Form

## OBJECTIVE
Implement SuggestionTextBox controls in a NEW control or form file that doesn't currently exist. This prompt creates the control from scratch with proper SuggestionTextBox integration following MTM constitution standards.

## INPUT REQUIREMENTS

Before executing this prompt, you need:
1. **Control/Form Name**: The name of the new control/form to create (e.g., `Control_Add_WorkOrder`, `Form_SelectPart`)
2. **Target Location**: Full path where file should be created (e.g., `c:\...\Controls\SettingsForm\Control_Add_WorkOrder.cs`)
3. **Required SuggestionTextBox Fields**: List of fields needed with their data types:
   - Part Number (PartNumber type)
   - Operation (Operation type)
   - Location (Location type)
   - Item Type (ItemType type)
   - Color Code (ColorCode type)
4. **Additional Controls**: Any other controls needed (buttons, labels, checkboxes, etc.)
5. **Purpose**: Brief description of what the control/form does

## EXECUTION STEPS

### Step 1: Read Required Instructions
Read the SuggestionTextBox implementation instructions:
- `c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.github\instructions\suggestiontextbox-implementation.instructions.md`

### Step 2: Read Constitution
Read and understand the MTM constitution:
- `c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.specify\memory\constitution.md`

### Step 3: Analyze Existing Patterns
Read a reference implementation to understand the pattern:
- `c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Controls\SettingsForm\Control_Edit_PartID.cs`

### Step 4: Create the Control/Form

#### 4.1: Create .cs File Structure

```csharp
using System.Data;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.{Namespace}
{
    /// <summary>
    /// {Description of what this control does}
    /// {Additional details about functionality}
    /// </summary>
    public partial class {ControlName} : UserControl
    {
        #region Fields

        // Private fields for state management
        private DataRow? _currentData;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when {describe event trigger}
        /// </summary>
        public event EventHandler? DataChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the {ControlName} control.
        /// Configures SuggestionTextBox controls with F4 support.
        /// </summary>
        public {ControlName}()
        {
            try
            {
                InitializeComponent();
                ConfigureSuggestionTextBoxes();
                WireUpEventHandlers();

                LoggingUtility.Log($"[{nameof({ControlName})}] Control initialized successfully");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.High,
                    contextData: new Dictionary<string, object>
                    {
                        ["InitializationPhase"] = "Constructor"
                    },
                    controlName: nameof({ControlName}),
                    callerName: nameof({ControlName}));
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Configures SuggestionTextBox controls with appropriate data providers and F4 key support.
        /// </summary>
        private void ConfigureSuggestionTextBoxes()
        {
            // Configure each SuggestionTextBox field
            // Example for Part Number:
            Helper_SuggestionTextBox.ConfigureForPartNumbers(
                partNumberSuggestionTextBox,
                GetPartNumberSuggestionsAsync,
                enableF4: true);

            // Add more configurations as needed
        }

        /// <summary>
        /// Wires up event handlers for controls.
        /// </summary>
        private void WireUpEventHandlers()
        {
            // Subscribe to SuggestionSelected events
            partNumberSuggestionTextBox.SuggestionSelected += PartNumberSuggestionTextBox_SuggestionSelected;
            
            // Subscribe to button click events
            saveButton.Click += SaveButton_Click;
            resetButton.Click += ResetButton_Click;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles part number selection from SuggestionTextBox.
        /// </summary>
        private async void PartNumberSuggestionTextBox_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            string selectedPart = e.SelectedValue;

            try
            {
                LoggingUtility.Log($"[{nameof({ControlName})}] Part selected: {selectedPart}");

                // Load related data based on selection
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

                // Update UI with loaded data
                _currentData = result.Data;
                LoadDataIntoForm();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["SelectedPart"] = selectedPart,
                        ["Operation"] = "LoadPartData"
                    },
                    controlName: nameof({ControlName}),
                    callerName: nameof(PartNumberSuggestionTextBox_SuggestionSelected));
            }
        }

        /// <summary>
        /// Handles save button click.
        /// </summary>
        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (!ValidateInputs())
                {
                    return;
                }

                // Perform save operation
                await SaveDataAsync();

                // Show success message
                Service_ErrorHandler.ShowInformation(
                    "Data saved successfully!",
                    "Save Complete",
                    controlName: nameof({ControlName}));

                // Reset form
                ResetForm();

                // Trigger event
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "SaveData"
                    },
                    controlName: nameof({ControlName}),
                    callerName: nameof(SaveButton_Click));
            }
        }

        /// <summary>
        /// Handles reset button click.
        /// </summary>
        private void ResetButton_Click(object? sender, EventArgs e)
        {
            ResetForm();
        }

        #endregion

        #region Data Providers

        /// <summary>
        /// Data provider for part number SuggestionTextBox.
        /// </summary>
        private async Task<List<string>> GetPartNumberSuggestionsAsync() =>
            await GetSuggestionsFromDatabaseAsync("md_part_ids_Get_All", "PartID", "part numbers");

        /// <summary>
        /// Generic method to load suggestions from database stored procedure.
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
                    controlName: nameof({ControlName}),
                    callerName: nameof(GetSuggestionsFromDatabaseAsync));
                return new List<string>();
            }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates all form inputs before saving.
        /// </summary>
        private bool ValidateInputs()
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(partNumberSuggestionTextBox.Text))
            {
                Service_ErrorHandler.ShowWarning("Please select a part number.");
                partNumberSuggestionTextBox.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads data into form controls.
        /// </summary>
        private void LoadDataIntoForm()
        {
            if (_currentData == null)
            {
                return;
            }

            // Load data into controls
            // Example: someTextBox.Text = _currentData["FieldName"]?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Saves form data to database.
        /// </summary>
        private async Task SaveDataAsync()
        {
            // Implement save logic using DAOs
            // Follow Model_Dao_Result pattern
        }

        /// <summary>
        /// Resets form to initial state.
        /// </summary>
        private void ResetForm()
        {
            _currentData = null;
            partNumberSuggestionTextBox.Clear();
            // Clear other controls
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Disposes resources used by this control.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    // Unsubscribe from events to prevent memory leaks
                    if (partNumberSuggestionTextBox != null)
                    {
                        partNumberSuggestionTextBox.SuggestionSelected -= PartNumberSuggestionTextBox_SuggestionSelected;
                    }

                    if (saveButton != null)
                    {
                        saveButton.Click -= SaveButton_Click;
                    }

                    if (resetButton != null)
                    {
                        resetButton.Click -= ResetButton_Click;
                    }
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                }
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
```

#### 4.2: Create Designer.cs File

Create the Designer.cs file with proper WinForms designer code for all controls including SuggestionTextBox instances.

### Step 5: Build and Test

Build the project to ensure no compilation errors:
```powershell
dotnet build
```

### Step 6: Verify Constitution Compliance

Verify the implementation against constitution requirements:
- [ ] All exceptions through Service_ErrorHandler
- [ ] All logging through LoggingUtility
- [ ] All database operations async
- [ ] All DAO methods return Model_Dao_Result<T>
- [ ] Proper event subscription/unsubscription
- [ ] Proper #region organization
- [ ] XML documentation on all public members

## OUTPUT FORMAT

After execution, provide:

1. **Summary of changes**:
   - Files created
   - SuggestionTextBox controls configured
   - Data providers implemented

2. **Constitution compliance report**:
   - ✅ All error handling through Service_ErrorHandler
   - ✅ All logging through LoggingUtility
   - ✅ Proper async patterns
   - ✅ Event cleanup in Dispose

3. **Testing checklist**:
   - [ ] F4 key shows full list
   - [ ] Selection events fire
   - [ ] Validation works
   - [ ] Save operation works
   - [ ] Reset clears properly

4. **Next steps suggestion**:
   - Suggested prompt to run next (if applicable)
   - Additional testing needed
   - Integration points to verify

## EXAMPLE USAGE

**User Input:**
```
Control Name: Control_Add_WorkOrder
Location: c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Controls\WorkOrders\Control_Add_WorkOrder.cs
Required Fields:
  - Part Number (PartNumber type)
  - Operation (Operation type)
  - Color Code (ColorCode type)
Additional Controls: Save button, Reset button, Quantity NumericUpDown
Purpose: Allows users to add new work orders by selecting part, operation, and color code
```

**AI Execution:**
1. Read instructions and constitution
2. Create Control_Add_WorkOrder.cs with proper structure
3. Configure 3 SuggestionTextBox controls
4. Implement data providers
5. Add validation and save logic
6. Create Designer.cs file
7. Build and verify

**AI Output:**
```
✅ Control created successfully at specified location
✅ 3 SuggestionTextBox controls configured with F4 support
✅ Data providers implemented using generic pattern
✅ Constitution compliant (all checks passed)
✅ Build succeeded with no errors

Next Steps:
- Run manual testing with F4 key on each field
- Verify database save operation with test data
- Consider running "suggestiontextbox-refactor-combobox.prompt.md" if you have similar controls to migrate
```

## VERSION
v1.0 - 2025-11-15

## RELATED PROMPTS
- `suggestiontextbox-refactor-combobox.prompt.md` - Migrate existing ComboBox controls
- `suggestiontextbox-refactor-textbox.prompt.md` - Migrate existing TextBox controls
- `suggestiontextbox-constitution-audit.prompt.md` - Audit existing implementations
