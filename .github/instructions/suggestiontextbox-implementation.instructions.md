---
description: 'SuggestionTextBox implementation patterns - replacing ComboBox/TextBox controls with intelligent suggestion support'
applyTo: '**/*.cs,**/*.Designer.cs'
---

# SuggestionTextBox Implementation Guide

## Overview

SuggestionTextBox is a custom WinForms control that provides intelligent autocomplete/suggestion functionality with:
- On-demand data loading via async delegates
- Wildcard pattern matching with `%` symbol
- F4 key support to show full suggestion list
- Keyboard navigation (Up/Down/Enter/Escape)
- Automatic capitalization and validation
- Event-driven architecture for integration

## When to Use SuggestionTextBox

### ✅ Use SuggestionTextBox For:
- Part number selection (large datasets, wildcard search needed)
- Operation number selection (medium datasets, sequential browsing)
- Location selection (hierarchical data, building/area filtering)
- Item type selection (small datasets, categorical data)
- Color code selection (fixed list, validation required)
- Any field where users need to select from master data with search capability

### ❌ Do NOT Use SuggestionTextBox For:
- Free-form text input (names, descriptions, notes)
- Numeric input (quantities, prices, measurements)
- Date/time selection (use DateTimePicker)
- Boolean flags (use CheckBox)
- Small fixed lists (<5 items where ComboBox is clearer)

## Helper Methods Overview

### Configuration Methods

All configuration is done via `Helper_SuggestionTextBox` static methods:

```csharp
// Standard configurations for common data types
Helper_SuggestionTextBox.ConfigureForPartNumbers(control, dataProvider, enableF4: true);
Helper_SuggestionTextBox.ConfigureForOperations(control, dataProvider, enableF4: true);
Helper_SuggestionTextBox.ConfigureForLocations(control, dataProvider, enableF4: true);
Helper_SuggestionTextBox.ConfigureForItemTypes(control, dataProvider, enableF4: true);
Helper_SuggestionTextBox.ConfigureForColorCodes(control, dataProvider, enableF4: true);

// Bulk configuration for multiple controls
Helper_SuggestionTextBox.ConfigureMultiple(
    (partControl, GetPartDataAsync, SuggestionType.PartNumber),
    (opControl, GetOperationDataAsync, SuggestionType.Operation)
);
```

### Validation Methods

```csharp
// Validate that selection is from valid master data
bool isValid = await Helper_SuggestionTextBox.ValidateSelectionAsync(control, "Part Number");

// Clear control(s)
Helper_SuggestionTextBox.Clear(control, refreshDataSource: true);
Helper_SuggestionTextBox.ClearMultiple(refreshDataSource: false, control1, control2);

// Enable/disable controls
Helper_SuggestionTextBox.SetEnabled(control, enabled: true);
Helper_SuggestionTextBox.SetEnabledMultiple(true, control1, control2, control3);
```

### F4 Trigger Method

```csharp
// Manually trigger full suggestion list (called automatically if enableF4: true)
await Helper_SuggestionTextBox.ShowFullSuggestionListAsync(control);
```

## Standard Configuration Settings

### Part Numbers
- **MaxResults**: 100
- **EnableWildcards**: true
- **ClearOnNoMatch**: true
- **Use Case**: Large datasets, complex part numbering schemes
- **Example**: `R-%-01` matches `R-ABC-01`, `R-XYZ-01`

### Operations
- **MaxResults**: 50
- **EnableWildcards**: true
- **ClearOnNoMatch**: true
- **Use Case**: Sequential operation numbers with pattern matching

### Locations
- **MaxResults**: 100
- **EnableWildcards**: true
- **ClearOnNoMatch**: true
- **Use Case**: Building/area/row location hierarchies

### Item Types
- **MaxResults**: 50
- **EnableWildcards**: false
- **ClearOnNoMatch**: true
- **Use Case**: Fixed categorical data (no wildcard needed)

### Color Codes
- **MaxResults**: 20
- **EnableWildcards**: false
- **ClearOnNoMatch**: false (allows custom codes like "OTHER")
- **Use Case**: Small fixed list with occasional custom values

## Data Provider Pattern

### Basic Data Provider

All SuggestionTextBox controls require a data provider delegate:

```csharp
/// <summary>
/// Data provider for part number suggestions.
/// Returns list of all part IDs from database.
/// </summary>
private async Task<List<string>> GetPartNumberSuggestionsAsync()
{
    try
    {
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_Application_Variables.ConnectionString,
            "md_part_ids_Get_All",
            null);

        if (!result.IsSuccess || result.Data == null)
        {
            Service_ErrorHandler.ShowWarning(result.ErrorMessage ?? "Failed to load part numbers");
            return new List<string>();
        }

        var suggestions = new List<string>(result.Data.Rows.Count);
        foreach (DataRow row in result.Data.Rows)
        {
            var value = row["PartID"];
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
            controlName: nameof(MyControl),
            callerName: nameof(GetPartNumberSuggestionsAsync));
        return new List<string>();
    }
}
```

### Generic Data Provider Pattern

Reduce duplication with a generic method:

```csharp
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
            controlName: nameof(MyControl),
            callerName: nameof(GetSuggestionsFromDatabaseAsync));
        return new List<string>();
    }
}

// Use generic method for specific data types
private async Task<List<string>> GetPartNumberSuggestionsAsync() =>
    await GetSuggestionsFromDatabaseAsync("md_part_ids_Get_All", "PartID", "part numbers");

private async Task<List<string>> GetOperationSuggestionsAsync() =>
    await GetSuggestionsFromDatabaseAsync("md_operation_numbers_Get_All", "OperationNumber", "operations");
```

## Event Handling Pattern

### SuggestionSelected Event

Subscribe to this event to handle user selections:

```csharp
/// <summary>
/// Handles part selection from SuggestionTextBox.
/// </summary>
private async void PartTextBox_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
{
    string selectedPart = e.SelectedValue;

    try
    {
        LoggingUtility.Log($"[{nameof(MyControl)}] Part selected: {selectedPart}");

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
        LoadPartDetails(result.Data);
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
            contextData: new Dictionary<string, object>
            {
                ["SelectedPart"] = selectedPart
            },
            controlName: nameof(MyControl),
            callerName: nameof(PartTextBox_SuggestionSelected));
    }
}
```

## Complete Implementation Example

### New Control with SuggestionTextBox

```csharp
public partial class Control_MyNewControl : UserControl
{
    #region Fields

    private DataRow? _currentPart;

    #endregion

    #region Events

    public event EventHandler? DataChanged;

    #endregion

    #region Constructors

    public Control_MyNewControl()
    {
        InitializeComponent();
        ConfigureSuggestionTextBoxes();
        WireUpEventHandlers();
    }

    #endregion

    #region Initialization

    private void ConfigureSuggestionTextBoxes()
    {
        Helper_SuggestionTextBox.ConfigureForPartNumbers(
            partNumberTextBox,
            GetPartNumberSuggestionsAsync,
            enableF4: true);

        Helper_SuggestionTextBox.ConfigureForOperations(
            operationTextBox,
            GetOperationSuggestionsAsync,
            enableF4: true);
    }

    private void WireUpEventHandlers()
    {
        partNumberTextBox.SuggestionSelected += PartNumberTextBox_SuggestionSelected;
        operationTextBox.SuggestionSelected += OperationTextBox_SuggestionSelected;
    }

    #endregion

    #region Events

    private async void PartNumberTextBox_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
    {
        // Handle selection
    }

    private async void OperationTextBox_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
    {
        // Handle selection
    }

    #endregion

    #region Data Providers

    private async Task<List<string>> GetPartNumberSuggestionsAsync() =>
        await GetSuggestionsFromDatabaseAsync("md_part_ids_Get_All", "PartID", "part numbers");

    private async Task<List<string>> GetOperationSuggestionsAsync() =>
        await GetSuggestionsFromDatabaseAsync("md_operation_numbers_Get_All", "OperationNumber", "operations");

    private async Task<List<string>> GetSuggestionsFromDatabaseAsync(
        string procedureName, 
        string columnName, 
        string dataTypeName)
    {
        // Generic implementation (see above)
    }

    #endregion

    #region Cleanup

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unsubscribe from events
            if (partNumberTextBox != null)
                partNumberTextBox.SuggestionSelected -= PartNumberTextBox_SuggestionSelected;
            
            if (operationTextBox != null)
                operationTextBox.SuggestionSelected -= OperationTextBox_SuggestionSelected;
        }

        base.Dispose(disposing);
    }

    #endregion
}
```

## Migration Patterns

### From ComboBox to SuggestionTextBox

**Old ComboBox Pattern:**
```csharp
// Constructor
ComboBox_Part.DropDownStyle = ComboBoxStyle.DropDownList;
LoadPartComboBox();

// Load method
private async void LoadPartComboBox()
{
    var result = await Dao_Part.GetAllPartsAsync();
    ComboBox_Part.DataSource = result.Data;
    ComboBox_Part.DisplayMember = "PartID";
    ComboBox_Part.ValueMember = "ID";
}

// Event handler
private void ComboBox_Part_SelectedIndexChanged(object sender, EventArgs e)
{
    if (ComboBox_Part.SelectedValue != null)
    {
        int partId = (int)ComboBox_Part.SelectedValue;
        LoadPartDetails(partId);
    }
}
```

**New SuggestionTextBox Pattern:**
```csharp
// Constructor - much simpler!
Helper_SuggestionTextBox.ConfigureForPartNumbers(
    SuggestionTextBox_Part,
    GetPartNumberSuggestionsAsync,
    enableF4: true);
SuggestionTextBox_Part.SuggestionSelected += SuggestionTextBox_Part_SuggestionSelected;

// Data provider - loads on demand
private async Task<List<string>> GetPartNumberSuggestionsAsync()
{
    var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
        Model_Application_Variables.ConnectionString,
        "md_part_ids_Get_All",
        null);

    if (!result.IsSuccess || result.Data == null)
        return new List<string>();

    return result.Data.Rows
        .Cast<DataRow>()
        .Select(r => r["PartID"]?.ToString() ?? string.Empty)
        .Where(s => !string.IsNullOrEmpty(s))
        .ToList();
}

// Event handler - string-based, simpler
private async void SuggestionTextBox_Part_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
{
    string partNumber = e.SelectedValue;
    var result = await Dao_Part.GetPartByNumberAsync(partNumber);
    
    if (result.IsSuccess && result.Data != null)
    {
        LoadPartDetails(result.Data);
    }
}
```

### From Plain TextBox to SuggestionTextBox

**Old TextBox Pattern:**
```csharp
// Constructor
TextBox_Part.Leave += TextBox_Part_Leave;

// Validation on leave
private async void TextBox_Part_Leave(object sender, EventArgs e)
{
    string partNumber = TextBox_Part.Text.Trim().ToUpper();
    
    if (string.IsNullOrEmpty(partNumber))
        return;

    var result = await Dao_Part.GetPartByNumberAsync(partNumber);
    
    if (!result.IsSuccess || result.Data == null)
    {
        MessageBox.Show($"Part '{partNumber}' not found!");
        TextBox_Part.Clear();
        TextBox_Part.Focus();
        return;
    }

    LoadPartDetails(result.Data);
}
```

**New SuggestionTextBox Pattern:**
```csharp
// Constructor - validation built-in!
Helper_SuggestionTextBox.ConfigureForPartNumbers(
    SuggestionTextBox_Part,
    GetPartNumberSuggestionsAsync,
    enableF4: true);
SuggestionTextBox_Part.SuggestionSelected += SuggestionTextBox_Part_SuggestionSelected;

// Automatic validation, capitalization, and suggestion
private async void SuggestionTextBox_Part_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
{
    // Only called for valid selections!
    var result = await Dao_Part.GetPartByNumberAsync(e.SelectedValue);
    
    if (result.IsSuccess && result.Data != null)
    {
        LoadPartDetails(result.Data);
    }
}
```

## Constitution Compliance

### Error Handling (Principle I)
- ✅ All exceptions through `Service_ErrorHandler.HandleException()`
- ✅ Context dictionaries for debugging
- ✅ Appropriate severity levels

### Structured Logging (Principle II)
- ✅ Use `LoggingUtility.Log()` for operations
- ✅ Log selection events for audit trail

### Async-First (Principle IV)
- ✅ All data providers are async
- ✅ Event handlers use async void pattern
- ✅ Proper await usage throughout

### WinForms Best Practices (Principle V)
- ✅ Proper event subscription/unsubscription in Dispose
- ✅ Null-conditional operators for safety
- ✅ UI marshaling handled automatically by control

## Common Pitfalls

### ❌ Don't: Manually set Text property
```csharp
SuggestionTextBox_Part.Text = "R-123-01"; // Wrong! Doesn't trigger event
```

### ✅ Do: Let user select or programmatically trigger
```csharp
// User types and selects from overlay - triggers SuggestionSelected event
// OR programmatically:
await Helper_SuggestionTextBox.ShowFullSuggestionListAsync(SuggestionTextBox_Part);
```

### ❌ Don't: Subscribe to TextChanged
```csharp
SuggestionTextBox_Part.TextChanged += ...; // Too frequent, use SuggestionSelected
```

### ✅ Do: Subscribe to SuggestionSelected
```csharp
SuggestionTextBox_Part.SuggestionSelected += SuggestionTextBox_Part_SuggestionSelected;
```

### ❌ Don't: Forget to unsubscribe in Dispose
```csharp
protected override void Dispose(bool disposing)
{
    base.Dispose(disposing); // Memory leak!
}
```

### ✅ Do: Unsubscribe from events
```csharp
protected override void Dispose(bool disposing)
{
    if (disposing && SuggestionTextBox_Part != null)
    {
        SuggestionTextBox_Part.SuggestionSelected -= SuggestionTextBox_Part_SuggestionSelected;
    }
    base.Dispose(disposing);
}
```

## Performance Considerations

### Data Provider Caching
- Data providers are called on-demand (not at form load)
- Results are NOT cached by default
- For frequently accessed data, consider caching in Helper_UI_SuggestionBoxes

### Large Datasets
- Use MaxResults to limit overlay size
- Enable wildcards for better filtering
- Consider stored procedures that return pre-filtered data

### Memory Management
- SuggestionOverlayForm is disposed after each use
- Event handlers must be unsubscribed in Dispose
- DataTable results should be processed and discarded

## Testing Checklist

When implementing SuggestionTextBox:

- [ ] F4 key shows full suggestion list
- [ ] Down arrow (empty field) shows full list
- [ ] Typing filters suggestions correctly
- [ ] Wildcard `%` character works (if enabled)
- [ ] Enter key selects highlighted item
- [ ] Escape key cancels selection
- [ ] No match clears field (if ClearOnNoMatch: true)
- [ ] Exact match doesn't show overlay (if SuppressExactMatch: true)
- [ ] Text automatically capitalizes on selection
- [ ] SuggestionSelected event fires correctly
- [ ] Events unsubscribed in Dispose
- [ ] Error handling shows user-friendly messages
- [ ] Performance acceptable with large datasets

## Version History

- **v1.0** (2025-11-15): Initial implementation guide based on Control_Edit_PartID refactoring
