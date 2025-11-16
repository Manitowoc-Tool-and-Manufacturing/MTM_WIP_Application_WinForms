# SuggestionTextBoxWithLabel - Validated Mode Implementation Guide

**CRITICAL**: SuggestionTextBoxWithLabel now supports TWO modes - use the correct mode for your use case.

## Mode Selection

### Suggestion Mode (Default)
- **When**: User selects from master data (parts, locations, operations, color codes)
- **Features**: F4 button, autocomplete overlay, datatable data provider
- **Example**: Part number selection, location selection

### Validated Mode
- **When**: User enters validated text (quantity, weight, price, numeric input)
- **Features**: Automatic validation, visual feedback, NO F4 button, NO datatable
- **Example**: Quantity input, weight input, price input

## Mandatory Pattern - Validated Mode

### ✅ CORRECT: Use Helper_ValidatedTextBox

```csharp
// Step 1: Configure control (ONE LINE)
Helper_ValidatedTextBox.ConfigureForQuantity(quantityTextBox);

// Step 2: Validate in save method (ONE LINE)
if (!Helper_ValidatedTextBox.ValidateControl(quantityTextBox, "Quantity"))
{
    return; // Error shown, focus set automatically
}

// Step 3: Safe parsing (already validated)
int qty = int.Parse(quantityTextBox.Text);
```

### ❌ WRONG: Manual validation

```csharp
// ❌ DO NOT DO THIS - Duplicates code
Control_InventoryTab_TextBox_Quantity.TextChanged += (s, e) =>
{
    Control_AdvancedInventory.ValidateQtyTextBox(Control_InventoryTab_TextBox_Quantity);
};

// ❌ DO NOT DO THIS - Manual parsing
if (!int.TryParse(qtyText, out int qty) || qty <= 0)
{
    Service_ErrorHandler.HandleValidationError(...);
    textBox.Focus();
    return;
}

// ❌ DO NOT DO THIS - Manual color management
textBox.ForeColor = isValid ? Color.Black : Color.Red;
```

## Built-in Validators

### Quantity (Positive Integer)

```csharp
Helper_ValidatedTextBox.ConfigureForQuantity(quantityTextBox);
```

**Validation:**
- Must be integer
- Must be > 0
- Max: 999,999

**Use cases:** Inventory quantity, item count, piece count

### Weight (Decimal)

```csharp
Helper_ValidatedTextBox.ConfigureForWeight(weightTextBox);
```

**Validation:**
- Allows decimals
- Min: 0.01
- Max: 99,999.99

**Use cases:** Item weight, package weight, material weight

### Price (Decimal)

```csharp
Helper_ValidatedTextBox.ConfigureForPrice(priceTextBox);
```

**Validation:**
- Allows decimals
- Min: 0.01
- Max: 9,999,999.99

**Use cases:** Item price, cost, unit price

### Custom Numeric

```csharp
Helper_ValidatedTextBox.ConfigureForNumeric(
    control: countTextBox,
    min: 1,
    max: 100,
    allowDecimals: false,
    fieldName: "Item Count");
```

**Use cases:** Custom range validation, percentage (0-100), age, etc.

## Visual Feedback (Automatic)

### Valid State
- **Color**: Black (`Model_Application_Variables.UserUiColors.TextBoxForeColor`)
- **When**: Value passes validation OR field is empty

### Invalid State
- **Color**: Red (`Model_Application_Variables.UserUiColors.TextBoxErrorForeColor`)
- **When**: Value fails validation (e.g., negative number, non-numeric, out of range)

**NO manual color management needed** - control handles it automatically.

## Required Migration - InventoryTab Example

### Before (Manual Validation)

```csharp
// ❌ OLD - 30+ lines of code

// Field declaration
private TextBox Control_InventoryTab_TextBox_Quantity;

// Constructor
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

// Helper method
private void Control_InventoryTab_TextBox_Quantity_TextChanged()
{
    try
    {
        // Logging and validation logic
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, ...);
    }
}

// Save method
string qtyText = Control_InventoryTab_TextBox_Quantity.Text.Trim();
if (!int.TryParse(qtyText, out int qty) || qty <= 0)
{
    Service_ErrorHandler.HandleValidationError(
        "Quantity must be a positive integer.",
        nameof(Control_InventoryTab_TextBox_Quantity));
    Control_InventoryTab_TextBox_Quantity.Focus();
    return;
}

// Static validation method in Control_AdvancedInventory
public static void ValidateQtyTextBox(TextBox textBox)
{
    string text = textBox.Text.Trim();
    bool isValid = int.TryParse(text, out int value) && value > 0;
    if (isValid)
    {
        textBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
    }
    else
    {
        textBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
    }
}
```

### After (Automatic Validation)

```csharp
// ✅ NEW - 5 lines of code

// Field declaration (use SuggestionTextBoxWithLabel)
private SuggestionTextBoxWithLabel Control_InventoryTab_TextBox_Quantity;

// Constructor
Helper_ValidatedTextBox.ConfigureForQuantity(Control_InventoryTab_TextBox_Quantity);
Control_InventoryTab_TextBox_Quantity.ValidationChanged += (s, e) => 
    Control_InventoryTab_Update_SaveButtonState();

// Save method
if (!Helper_ValidatedTextBox.ValidateControl(Control_InventoryTab_TextBox_Quantity, "Quantity"))
{
    return;
}

int qty = int.Parse(Control_InventoryTab_TextBox_Quantity.Text);
```

**Result:**
- ✅ 90% less code
- ✅ No duplicated validation logic
- ✅ Automatic visual feedback
- ✅ Consistent error messages
- ✅ Reusable across all forms

## Bulk Operations

### Configure Multiple Controls

```csharp
// Same type
Helper_ValidatedTextBox.ConfigureMultipleForQuantity(
    showValidationColor: true,
    quantityTextBox1,
    quantityTextBox2,
    quantityTextBox3);

// Different types
Helper_ValidatedTextBox.ConfigureMultiple(
    (quantityTextBox, "Quantity", true),
    (weightTextBox, "Weight", true),
    (priceTextBox, "Price", true));
```

### Validate Multiple Controls

```csharp
// Validate all, stop at first error
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

### Clear Multiple Controls

```csharp
Helper_ValidatedTextBox.ClearMultiple(
    quantityTextBox,
    weightTextBox,
    priceTextBox);
```

### Enable/Disable Multiple Controls

```csharp
Helper_ValidatedTextBox.SetEnabledMultiple(
    enabled: false,
    quantityTextBox,
    weightTextBox,
    priceTextBox);
```

## Events

### ValidationChanged

```csharp
quantityTextBox.ValidationChanged += (sender, result) =>
{
    if (result.IsValid)
    {
        Console.WriteLine($"{result.FieldName} is valid!");
    }
    else
    {
        Console.WriteLine($"{result.FieldName} error: {result.ErrorMessage}");
    }

    // Update save button state
    UpdateSaveButtonState();
};
```

**Use cases:**
- Real-time save button enable/disable
- Form-level validation state tracking
- Custom visual feedback

## Properties

### IsValid (Read-Only)

```csharp
if (quantityTextBox.IsValid)
{
    // Safe to parse
    int qty = int.Parse(quantityTextBox.Text);
}
else
{
    // Show error or disable save
}
```

### EnableSuggestions

```csharp
// Suggestion mode (F4 button visible)
partNumberTextBox.EnableSuggestions = true;

// Validated mode (F4 button hidden)
quantityTextBox.EnableSuggestions = false;
```

**Set automatically by Helper_ValidatedTextBox** - don't set manually.

### ShowValidationColor

```csharp
// Show validation colors (default)
quantityTextBox.ShowValidationColor = true;

// No validation colors
quantityTextBox.ShowValidationColor = false;
```

## When to Use Validated Mode vs Suggestion Mode

### Use Validated Mode (Helper_ValidatedTextBox)

| Use Case | Validator | Example |
|----------|-----------|---------|
| Quantity input | `ConfigureForQuantity()` | Inventory quantity, item count |
| Weight input | `ConfigureForWeight()` | Package weight, material weight |
| Price input | `ConfigureForPrice()` | Unit price, cost |
| Custom numeric | `ConfigureForNumeric()` | Age (0-120), percentage (0-100) |

**Characteristics:**
- User types value directly
- NO autocomplete needed
- Numeric validation with range
- Visual feedback (red/black)
- NO F4 button

### Use Suggestion Mode (Helper_SuggestionTextBox)

| Use Case | Configuration | Example |
|----------|---------------|---------|
| Part selection | `ConfigureForPartNumbers()` | Part number from database |
| Location selection | `ConfigureForLocations()` | Location code from database |
| Operation selection | `ConfigureForOperations()` | Operation number from database |
| Color code selection | `ConfigureForColorCodes()` | Color code from database |

**Characteristics:**
- User selects from dropdown
- Autocomplete overlay
- F4 button for full list
- Data provider required
- Wildcard search (%)

## Constitution Compliance

### Validation (Principle I)

✅ **MANDATORY**: Use `Service_ErrorHandler.HandleValidationError()` for validation errors

```csharp
// Helper_ValidatedTextBox does this automatically
if (!Helper_ValidatedTextBox.ValidateControl(quantityTextBox, "Quantity"))
{
    return; // Service_ErrorHandler.HandleValidationError called internally
}
```

### Code Reuse (Principle III)

✅ **MANDATORY**: Use centralized validation helpers - NO duplicated validation logic

```csharp
// ✅ CORRECT
Helper_ValidatedTextBox.ConfigureForQuantity(quantityTextBox);

// ❌ WRONG
private void ValidateQuantity(TextBox textBox) { /* duplicate logic */ }
```

## Testing Checklist

- [ ] Control configured with `Helper_ValidatedTextBox.ConfigureForX()`
- [ ] F4 button NOT visible (EnableSuggestions = false)
- [ ] Red text shows for invalid input
- [ ] Black text shows for valid input
- [ ] `ValidateControl()` called in save method
- [ ] Validation error shown to user on save
- [ ] Focus set to invalid control on error
- [ ] No manual `int.TryParse()` validation
- [ ] No manual color management
- [ ] No duplicated validation logic

## Related Documentation

- **Main Guide**: `Documentation/SuggestionTextBoxWithLabel-Validated-Mode.md`
- **Example**: `Examples/Example_ValidatedTextBox_Form.cs`
- **Helper**: `Helpers/Helper_ValidatedTextBox.cs`
- **Validators**: `Services/Validators/NumericValidator.cs`
- **Suggestion Mode**: `.github/instructions/suggestiontextbox-implementation.instructions.md`

## Summary

**SuggestionTextBoxWithLabel** supports two modes:

1. **Suggestion Mode**: Autocomplete from datatable (use `Helper_SuggestionTextBox`)
2. **Validated Mode**: Automatic validation (use `Helper_ValidatedTextBox`)

**Key Principle**: Use the right tool for the job:
- **Master data selection** → Suggestion Mode
- **Validated text input** → Validated Mode

**Migration Impact**:
- 📉 90% less validation code
- 📉 Zero manual color management
- 📉 Eliminates static validation methods (`ValidateQtyTextBox()`)
- 📈 Consistent validation across all forms
- 📈 Reusable validation logic
