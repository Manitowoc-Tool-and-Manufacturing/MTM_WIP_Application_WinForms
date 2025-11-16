# SuggestionTextBoxWithLabel - Validated Mode Guide

## Overview

SuggestionTextBoxWithLabel now supports **two modes**:

1. **Suggestion Mode** (default): Datatable-based autocomplete with F4 button
2. **Validated Mode**: Simple validated text input (no datatable, no F4 button)

This dual-mode approach eliminates code duplication for common validation patterns like quantity, weight, and price inputs.

## When to Use Validated Mode

### ✅ Use Validated Mode For:
- **Quantity fields**: Positive integers (1-999,999)
- **Weight fields**: Decimal values (0.01-99,999.99)
- **Price fields**: Decimal values (0.01-9,999,999.99)
- **Count fields**: Positive integers
- **Any numeric input with range validation**
- **Custom text validation** (email, phone, etc.)

### ❌ Do NOT Use Validated Mode For:
- Part number selection (use Suggestion Mode)
- Operation selection (use Suggestion Mode)
- Location selection (use Suggestion Mode)
- Color code selection (use Suggestion Mode)
- Any field requiring autocomplete from master data

## Quick Start - Quantity Validation

### Old Pattern (InventoryTab)

```csharp
// ❌ OLD: Manual validation in form
private TextBox Control_InventoryTab_TextBox_Quantity;

// In constructor:
Control_InventoryTab_TextBox_Quantity.TextChanged += (s, e) =>
{
    Control_AdvancedInventory.ValidateQtyTextBox(Control_InventoryTab_TextBox_Quantity);
};

Control_InventoryTab_TextBox_Quantity.Leave += (s, e) =>
{
    Control_AdvancedInventory.ValidateQtyTextBox(Control_InventoryTab_TextBox_Quantity);
};

// In save method:
string qtyText = Control_InventoryTab_TextBox_Quantity.Text.Trim();
if (!int.TryParse(qtyText, out int qty) || qty <= 0)
{
    Service_ErrorHandler.HandleValidationError(...);
    Control_InventoryTab_TextBox_Quantity.Focus();
    return;
}
```

### New Pattern (SuggestionTextBoxWithLabel)

```csharp
// ✅ NEW: Validation built into control
private SuggestionTextBoxWithLabel QuantityTextBox;

// In constructor (ONE LINE):
Helper_ValidatedTextBox.ConfigureForQuantity(QuantityTextBox, showValidationColor: true);

// In save method (ONE LINE):
if (!Helper_ValidatedTextBox.ValidateControl(QuantityTextBox, "Quantity"))
{
    return; // Validation error shown automatically, focus set automatically
}

int qty = int.Parse(QuantityTextBox.Text); // Safe - already validated
```

**Benefits:**
- 90% less code
- No manual validation logic
- Automatic visual feedback (red text for invalid)
- Consistent error messages
- Reusable across forms

## Configuration Methods

### Helper_ValidatedTextBox.ConfigureForQuantity()

```csharp
/// <summary>
/// Configures for positive integer quantity (1-999,999).
/// </summary>
Helper_ValidatedTextBox.ConfigureForQuantity(quantityTextBox, showValidationColor: true);
```

**Validation Rules:**
- Must be integer
- Must be > 0
- Max value: 999,999

**Visual Feedback:**
- Valid: Black text
- Invalid: Red text

### Helper_ValidatedTextBox.ConfigureForWeight()

```csharp
/// <summary>
/// Configures for weight with decimals (0.01-99,999.99).
/// </summary>
Helper_ValidatedTextBox.ConfigureForWeight(weightTextBox, showValidationColor: true);
```

**Validation Rules:**
- Allows decimals
- Min: 0.01
- Max: 99,999.99

### Helper_ValidatedTextBox.ConfigureForPrice()

```csharp
/// <summary>
/// Configures for price with decimals (0.01-9,999,999.99).
/// </summary>
Helper_ValidatedTextBox.ConfigureForPrice(priceTextBox, showValidationColor: true);
```

**Validation Rules:**
- Allows decimals
- Min: 0.01
- Max: 9,999,999.99

### Custom Numeric Validation

```csharp
/// <summary>
/// Configures custom numeric range with optional decimals.
/// </summary>
Helper_ValidatedTextBox.ConfigureForNumeric(
    control: myTextBox,
    min: 1,
    max: 100,
    allowDecimals: false,
    fieldName: "Item Count",
    showValidationColor: true);
```

### Custom Validator

```csharp
// Create custom validator
var emailValidator = new MyEmailValidator();

// Configure control
Helper_ValidatedTextBox.ConfigureForCustomValidation(
    emailTextBox,
    emailValidator,
    showValidationColor: true);
```

## Validation Methods

### Single Control Validation

```csharp
// Validate and show error dialog
bool isValid = Helper_ValidatedTextBox.ValidateControl(
    quantityTextBox,
    fieldName: "Quantity",
    showError: true);

if (!isValid)
{
    return; // User already notified, focus set
}

// Safe to use value
int qty = int.Parse(quantityTextBox.Text);
```

### Multiple Control Validation

```csharp
// Validate all controls, stop at first error
bool allValid = Helper_ValidatedTextBox.ValidateMultiple(
    showError: true,
    quantityTextBox,
    weightTextBox,
    priceTextBox);

if (!allValid)
{
    return; // First invalid control focused, user notified
}
```

### Get Validation Results (No Errors)

```csharp
// Get validation results without showing errors
var results = Helper_ValidatedTextBox.GetValidationResults(
    quantityTextBox,
    weightTextBox,
    priceTextBox);

foreach (var kvp in results)
{
    if (!kvp.Value.IsValid)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value.ErrorMessage}");
    }
}
```

## Bulk Configuration

### Configure Multiple Controls (Same Type)

```csharp
// Configure all for quantity validation
Helper_ValidatedTextBox.ConfigureMultipleForQuantity(
    showValidationColor: true,
    quantityTextBox1,
    quantityTextBox2,
    quantityTextBox3);
```

### Configure Multiple Controls (Different Types)

```csharp
// Configure with different validators
Helper_ValidatedTextBox.ConfigureMultiple(
    (quantityTextBox, "Quantity", true),
    (weightTextBox, "Weight", true),
    (priceTextBox, "Price", true));
```

## Clear Methods

```csharp
// Clear single control
Helper_ValidatedTextBox.Clear(quantityTextBox);

// Clear multiple controls
Helper_ValidatedTextBox.ClearMultiple(
    quantityTextBox,
    weightTextBox,
    priceTextBox);
```

## Enable/Disable Methods

```csharp
// Enable/disable single control
Helper_ValidatedTextBox.SetEnabled(quantityTextBox, enabled: false);

// Enable/disable multiple controls
Helper_ValidatedTextBox.SetEnabledMultiple(
    enabled: true,
    quantityTextBox,
    weightTextBox,
    priceTextBox);
```

## Complete Example - Inventory Form

```csharp
public partial class MyInventoryForm : ThemedForm
{
    #region Fields

    private SuggestionTextBoxWithLabel PartNumberTextBox;
    private SuggestionTextBoxWithLabel QuantityTextBox;
    private SuggestionTextBoxWithLabel WeightTextBox;
    private SuggestionTextBoxWithLabel LocationTextBox;

    #endregion

    #region Initialization

    private void ConfigureControls()
    {
        // Part number: Suggestion mode (datatable autocomplete)
        Helper_SuggestionTextBox.ConfigureForPartNumbers(
            PartNumberTextBox.TextBox,
            GetPartNumberSuggestionsAsync,
            enableF4: true);

        // Quantity: Validated mode (no datatable)
        Helper_ValidatedTextBox.ConfigureForQuantity(QuantityTextBox);

        // Weight: Validated mode (decimals allowed)
        Helper_ValidatedTextBox.ConfigureForWeight(WeightTextBox);

        // Location: Suggestion mode (datatable autocomplete)
        Helper_SuggestionTextBox.ConfigureForLocations(
            LocationTextBox.TextBox,
            GetLocationSuggestionsAsync,
            enableF4: true);

        // Wire up events
        PartNumberTextBox.SuggestionSelected += PartNumberTextBox_SuggestionSelected;
        LocationTextBox.SuggestionSelected += LocationTextBox_SuggestionSelected;
    }

    #endregion

    #region Save

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        // Validate all controls
        if (!Helper_ValidatedTextBox.ValidateMultiple(
            showError: true,
            QuantityTextBox,
            WeightTextBox))
        {
            return; // Validation failed, user notified
        }

        // Suggestion controls: check for selection
        if (string.IsNullOrWhiteSpace(PartNumberTextBox.Text))
        {
            Service_ErrorHandler.HandleValidationError(
                "Part Number is required.",
                "Part Number");
            PartNumberTextBox.Focus();
            return;
        }

        // All valid - safe to parse
        string partNumber = PartNumberTextBox.Text;
        int quantity = int.Parse(QuantityTextBox.Text);
        decimal weight = decimal.Parse(WeightTextBox.Text);
        string location = LocationTextBox.Text;

        // Save data
        await SaveInventoryAsync(partNumber, quantity, weight, location);
    }

    #endregion
}
```

## Migration Guide - InventoryTab

### Step 1: Replace TextBox with SuggestionTextBoxWithLabel

**Designer.cs:**
```csharp
// ❌ OLD
private TextBox Control_InventoryTab_TextBox_Quantity;

// ✅ NEW
private SuggestionTextBoxWithLabel Control_InventoryTab_TextBox_Quantity;
```

### Step 2: Remove Manual Validation Logic

**Old Constructor:**
```csharp
// ❌ REMOVE ALL OF THIS
Control_InventoryTab_TextBox_Quantity.TextChanged += (s, e) =>
{
    Control_InventoryTab_TextBox_Quantity_TextChanged();
    Control_AdvancedInventory.ValidateQtyTextBox(Control_InventoryTab_TextBox_Quantity);
    Control_InventoryTab_Update_SaveButtonState();
};

Control_InventoryTab_TextBox_Quantity.Leave += (s, e) =>
{
    Control_AdvancedInventory.ValidateQtyTextBox(Control_InventoryTab_TextBox_Quantity);
};
```

**New Constructor:**
```csharp
// ✅ ADD JUST THIS ONE LINE
Helper_ValidatedTextBox.ConfigureForQuantity(Control_InventoryTab_TextBox_Quantity);

// Optional: Subscribe to ValidationChanged event
Control_InventoryTab_TextBox_Quantity.ValidationChanged += (s, e) =>
{
    Control_InventoryTab_Update_SaveButtonState();
};
```

### Step 3: Simplify Save Method

**Old Save:**
```csharp
// ❌ OLD: Manual parsing and validation
string qtyText = Control_InventoryTab_TextBox_Quantity.Text.Trim();
if (!int.TryParse(qtyText, out int qty) || qty <= 0)
{
    Service_ErrorHandler.HandleValidationError(
        "Quantity must be a positive integer.",
        nameof(Control_InventoryTab_TextBox_Quantity));
    Control_InventoryTab_TextBox_Quantity.Focus();
    return;
}
```

**New Save:**
```csharp
// ✅ NEW: One-line validation
if (!Helper_ValidatedTextBox.ValidateControl(Control_InventoryTab_TextBox_Quantity, "Quantity"))
{
    return; // Validation error shown, focus set automatically
}

int qty = int.Parse(Control_InventoryTab_TextBox_Quantity.Text); // Safe!
```

## Properties

### EnableSuggestions

Controls whether suggestion functionality is active:

```csharp
// Default: true (suggestion mode with F4 button)
partNumberTextBox.EnableSuggestions = true;

// Validated mode: false (no F4 button, validation only)
quantityTextBox.EnableSuggestions = false;
```

### ShowValidationColor

Controls whether validation colors are shown:

```csharp
// Default: true (red for invalid, black for valid)
quantityTextBox.ShowValidationColor = true;

// No colors: false
quantityTextBox.ShowValidationColor = false;
```

### IsValid

Gets current validation state (read-only):

```csharp
if (quantityTextBox.IsValid)
{
    int qty = int.Parse(quantityTextBox.Text);
}
else
{
    // Show error or disable save button
}
```

## Events

### ValidationChanged

Fires when validation state changes:

```csharp
quantityTextBox.ValidationChanged += (sender, result) =>
{
    if (result.IsValid)
    {
        Console.WriteLine("Validation passed!");
    }
    else
    {
        Console.WriteLine($"Validation failed: {result.ErrorMessage}");
    }

    UpdateSaveButtonState();
};
```

## Visual Feedback

### Valid State
- **ForeColor**: `Model_Application_Variables.UserUiColors.TextBoxForeColor` (black)
- **Behavior**: Normal appearance

### Invalid State
- **ForeColor**: `Model_Application_Variables.UserUiColors.TextBoxErrorForeColor` (red)
- **Behavior**: Red text while typing invalid value

### Empty State
- **ForeColor**: Normal (black)
- **Behavior**: No validation error for empty (required check done on save)

## Best Practices

### DO

- ✅ Use `Helper_ValidatedTextBox.ConfigureForQuantity()` for quantity fields
- ✅ Use `Helper_ValidatedTextBox.ValidateControl()` in save methods
- ✅ Let validation errors show automatically (showError: true)
- ✅ Use bulk methods for multiple controls of same type
- ✅ Subscribe to `ValidationChanged` for real-time save button updates

### DON'T

- ❌ Don't manually validate with `int.TryParse()` - use `ValidateControl()`
- ❌ Don't set `ForeColor` manually - validation does it automatically
- ❌ Don't duplicate validation logic - reuse configured controls
- ❌ Don't use suggestion mode for simple numeric input
- ❌ Don't forget to configure control before using

## Validator Registration

### Built-in Validators

Already registered in `Service_Validation`:

- **"Quantity"**: Integer 0-999,999
- **"Weight"**: Decimal 0-99,999.99
- **"Price"**: Decimal 0-9,999,999.99
- **"PartNumber"**: Part number format
- **"WorkOrder"**: Work order format
- **"LocationCode"**: Location code format

### Register Custom Validator

```csharp
// Create validator
public class EmailValidator : IValidator
{
    public Model_Validation_Result Validate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure("Email is required.", "Email");
        }

        if (!input.Contains("@") || !input.Contains("."))
        {
            return Model_Validation_Result.Failure("Invalid email format.", "Email");
        }

        return Model_Validation_Result.Success("Email");
    }
}

// Register globally
Service_Validation.RegisterValidator("Email", new EmailValidator());

// Use in control
Helper_ValidatedTextBox.ConfigureForCustomValidation(
    emailTextBox,
    Service_Validation.GetValidator("Email"));
```

## Testing Checklist

When using validated mode:

- [ ] Control configured with appropriate validator
- [ ] Red text appears for invalid input
- [ ] Black text appears for valid input
- [ ] F4 button hidden (EnableSuggestions = false)
- [ ] `ValidateControl()` called in save method
- [ ] Validation error shown to user
- [ ] Focus set to invalid control
- [ ] Save blocked when validation fails
- [ ] No manual `int.TryParse()` validation
- [ ] `ValidationChanged` event wired up (if needed)

## Summary

**SuggestionTextBoxWithLabel** now supports both:

1. **Suggestion Mode**: Autocomplete from datatable (part numbers, locations, etc.)
2. **Validated Mode**: Simple validation (quantity, weight, price, etc.)

**Key Benefits:**

- ✅ **Code Reuse**: No more duplicated validation logic
- ✅ **Consistency**: Same validation behavior across all forms
- ✅ **Simplicity**: One-line configuration, one-line validation
- ✅ **Visual Feedback**: Automatic red/black text colors
- ✅ **Error Handling**: Automatic error messages and focus management

**Migration Impact:**

- 📉 **90% less validation code** in forms
- 📉 **Zero manual color management**
- 📉 **Eliminates `Control_AdvancedInventory.ValidateQtyTextBox()`**
- 📈 **Consistent UX across all quantity inputs**
