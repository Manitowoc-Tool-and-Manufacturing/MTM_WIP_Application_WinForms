---
description: 'Standard implementation guide for SuggestionTextBoxWithLabel control'
applyTo: '**/*.Designer.cs, **/*.cs'
---

# SuggestionTextBoxWithLabel Implementation Guide

## Table of Contents
- **Control Overview**: Purpose and usage of the `SuggestionTextBoxWithLabel` control.
- **Implementation Patterns**: Code examples for Standard Entity Input, Custom Data Sources, and Validated Input.
- **Migration Guide**: Steps to replace legacy Label+TextBox pairs.
- **Best Practices**: Naming, initialization, and disposal rules.
- **Troubleshooting**: Common issues with suggestions and validation.

Standardized pattern for implementing intelligent suggestion inputs using the `SuggestionTextBoxWithLabel` composite control.

## Control Overview

`SuggestionTextBoxWithLabel` is the standard input control for:
- Part Numbers
- Operation Numbers
- Locations
- Item Types
- Color Codes
- Any input requiring autocomplete/suggestions

It replaces the legacy pattern of separate `Label` + `TextBox` + `Button` controls.

## Implementation Patterns

### Pattern 1: Standard Entity Input (Preferred)

Use this pattern for standard system entities (Parts, Operations, Locations, etc.).

**Designer (.Designer.cs):**
```csharp
// 1. Declare control
private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_Input_PartNumber;

// 2. Initialize
this.Control_Input_PartNumber = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();

// 3. Configure properties
this.Control_Input_PartNumber.LabelText = "Part Number";
this.Control_Input_PartNumber.PlaceholderText = "Enter or select part...";
this.Control_Input_PartNumber.EnableSuggestions = true;
this.Control_Input_PartNumber.ShowF4Button = true;
this.Control_Input_PartNumber.ShowValidationColor = true;
this.Control_Input_PartNumber.Dock = DockStyle.Top; // or Fill inside TableLayoutPanel
```

**Code-Behind (.cs):**
```csharp
// In Constructor or Initialize method
public void InitializeControls()
{
    // Configure using Helper_SuggestionTextBox
    Helper_SuggestionTextBox.ConfigureForPartNumbers(
        Control_Input_PartNumber,
        Helper_SuggestionTextBox.GetCachedPartNumbersAsync);
        
    // Subscribe to events
    Control_Input_PartNumber.SuggestionSelected += OnPartSelected;
}

private void OnPartSelected(object sender, SuggestionSelectedEventArgs e)
{
    // Handle selection (e.SelectedValue)
    LoadPartData(e.SelectedValue);
}
```

### Pattern 2: Custom Data Source

Use this pattern for non-standard lists or context-specific suggestions.

**Code-Behind (.cs):**
```csharp
public void InitializeControls()
{
    // 1. Configure generic settings
    Control_Input_Custom.EnableSuggestions = true;
    Control_Input_Custom.ShowF4Button = true;
    
    // 2. Assign custom DataProvider
    Control_Input_Custom.TextBox.DataProvider = GetCustomSuggestionsAsync;
    
    // 3. Optional: Configure behavior
    Control_Input_Custom.TextBox.MaxResults = 50;
    Control_Input_Custom.TextBox.EnableWildcards = true;
}

private async Task<List<string>> GetCustomSuggestionsAsync()
{
    // Return list of strings from service/database
    return await _myService.GetNamesAsync();
}
```

### Pattern 3: Validated Input (No Suggestions)

Use this pattern for inputs that need validation styling but no autocomplete (e.g., Quantity, Price).

**Designer (.Designer.cs):**
```csharp
this.Control_Input_Quantity.EnableSuggestions = false; // Disables overlay & F4 button
this.Control_Input_Quantity.ShowValidationColor = true;
this.Control_Input_Quantity.ValidatorType = "Quantity"; // Must match a registered validator
```

## Migration Guide (Legacy -> New)

When replacing `Label` + `TextBox`:

1.  **Identify Components**: Find the Label, TextBox, and optional Button in Designer.cs.
2.  **Remove Legacy**: Delete the declarations and initialization code for these 3 controls.
3.  **Add New Control**: Add `SuggestionTextBoxWithLabel` in their place.
4.  **Map Properties**:
    - `Label.Text` -> `Control.LabelText`
    - `TextBox.Text` -> `Control.Text`
    - `TextBox.PlaceholderText` -> `Control.PlaceholderText`
5.  **Update Layout**:
    - If using `TableLayoutPanel`, the new control takes 2 columns (Label + Input) or sits in a single cell spanning columns depending on layout needs.
    - **Recommended**: Place in a single cell and set `Dock = DockStyle.Fill` or `Top`.
6.  **Update Events**:
    - `TextBox.TextChanged` -> `Control.SuggestionSelected` (for selection logic)
    - `TextBox.Leave` -> `Control.ValidationChanged` (for validation logic)

## Best Practices

1.  **Naming Convention**: `Control_{Context}_SuggestionBox_{Entity}`
    - Example: `Control_Edit_PartID_SuggestionBox_Part`
2.  **Initialization**: Always configure the DataProvider in the Constructor or `OnLoad`.
3.  **Disposal**: Unsubscribe from events in `Dispose()` to prevent memory leaks.
4.  **Layout**: The control is `AutoSize` friendly but works best with `Dock = Fill` inside a container.

## Troubleshooting

-   **Suggestions not showing?** Ensure `Helper_UI_SuggestionBoxes.LoadAllDataAsync()` has been called (usually in App Startup).
-   **F4 not working?** Ensure `ShowF4Button` is true and `EnableSuggestions` is true.
-   **Validation colors missing?** Ensure `ShowValidationColor` is true.
