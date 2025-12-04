---
description: 'Standard implementation guide for SuggestionTextBoxWithLabel control (v2)'
applyTo: '**/*.Designer.cs, **/*.cs'
---

# SuggestionTextBoxWithLabel Implementation Guide (v2)

## Table of Contents
- **Control Overview**: Purpose and usage of the `SuggestionTextBoxWithLabel` control.
- **Designer Configuration (Preferred)**: Using the new Enum-based properties for zero-code configuration.
- **Code-Behind Configuration (Advanced)**: Manual configuration for custom scenarios.
- **Validation Mode**: Using the control for validated inputs without suggestions.
- **Migration Guide**: Updating legacy implementations to the new standard.
- **Best Practices**: Naming, initialization, and disposal rules.

Standardized pattern for implementing intelligent suggestion inputs using the `SuggestionTextBoxWithLabel` composite control.

## Control Overview

`SuggestionTextBoxWithLabel` is the standard input control for:
- Part Numbers
- Operation Numbers
- Locations
- Item Types
- Color Codes
- Users
- Any input requiring autocomplete/suggestions

It replaces the legacy pattern of separate `Label` + `TextBox` + `Button` controls and simplifies the code-behind initialization.

## Designer Configuration (Preferred)

The preferred way to use this control is to configure it entirely in the Visual Studio Designer.

### 1. Add Control
Drag `SuggestionTextBoxWithLabel` from the Toolbox onto your form.

### 2. Set Properties
Configure the following properties in the **Properties Window**:

| Category | Property | Description |
| :--- | :--- | :--- |
| **Suggestion Data** | `SuggestionDataSource` | **CRITICAL**: Select the data type (e.g., `MTM_PartNumber`, `MTM_User`). This automatically wires up the data provider. |
| **Appearance** | `LabelText` | The text for the label (e.g., "Part Number"). The control automatically adds the colon suffix. |
| **Appearance** | `LabelVisibility` | `Visible` or `Hidden`. Controls whether the label is shown. |
| **Appearance** | `PlaceholderText` | Text shown inside the textbox when empty (e.g., "Enter part..."). |
| **Layout** | `MinLength` | Sets the **Label's** minimum width (useful for aligning multiple controls). |
| **Layout** | `MaxLength` | Sets the **Label's** maximum width. |

### 3. Supported Data Sources
The `SuggestionDataSource` enum supports:
- `MTM_PartNumber`
- `MTM_Operation`
- `MTM_Location`
- `MTM_Color`
- `MTM_User`
- *InforVisual types (Coming Soon)*

**Note**: When you set `SuggestionDataSource`, you do **NOT** need to write any initialization code in the form's constructor. The control handles it automatically.

## Code-Behind Configuration (Advanced)

Use this pattern ONLY for custom data sources not covered by the `SuggestionDataSource` enum.

### Pattern: Custom Data Source

**Designer (.Designer.cs):**
```csharp
// Leave SuggestionDataSource as 'None'
this.Control_Input_Custom.SuggestionDataSource = Enum_SuggestionDataSource.None;
```

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

## Validation Mode (No Suggestions)

Use this pattern for inputs that need validation styling but no autocomplete (e.g., Quantity, Price).

**Designer (.Designer.cs):**
```csharp
this.Control_Input_Quantity.EnableSuggestions = false; // Disables overlay & F4 button
this.Control_Input_Quantity.ShowValidationColor = true;
this.Control_Input_Quantity.ValidatorType = "Quantity"; // Must match a registered validator
```

## Migration Guide (Legacy -> New)

When updating existing `SuggestionTextBoxWithLabel` implementations:

1.  **Remove Code-Behind Initialization**:
    Look for lines like `Helper_SuggestionTextBox.ConfigureForPartNumbers(...)` in your form's constructor or `Load` event and **DELETE** them.

2.  **Update Designer**:
    Open the form in the Designer, select the control, and set the `SuggestionDataSource` property to the correct type (e.g., `MTM_PartNumber`).

3.  **Verify Label Visibility**:
    If you were setting `LabelVisible = "True/False"` (string), update it to use the `Enum_LabelVisibility` if doing so in code, or just use the dropdown in the Designer.

## Best Practices

1.  **Naming Convention**: `Control_{Context}_SuggestionBox_{Entity}`
    - Example: `Control_Edit_PartID_SuggestionBox_Part`
2.  **Alignment**: Use `MinLength` property to align labels across multiple stacked controls. Set them all to the same value (e.g., `100`).
3.  **Event Handling**: Subscribe to `SuggestionSelected` to handle user choices.
    ```csharp
    private void Control_Input_Part_SuggestionSelected(object sender, SuggestionSelectedEventArgs e)
    {
        LoadPartData(e.SelectedValue);
    }
    ```
4.  **Disposal**: The control automatically cleans up its resources, but ensure you unsubscribe from events if the control is removed dynamically.

## Troubleshooting

-   **Suggestions not showing?**
    -   Check if `SuggestionDataSource` is set correctly.
    -   Ensure `Helper_UI_SuggestionBoxes.LoadAllDataAsync()` has been called (usually in App Startup).
-   **Label not aligning?**
    -   Check `MinLength` property. It controls the label width, not the textbox width.
-   **"Object reference" error?**
    -   Ensure you aren't trying to configure the control in the constructor *before* `InitializeComponent()` is called.

