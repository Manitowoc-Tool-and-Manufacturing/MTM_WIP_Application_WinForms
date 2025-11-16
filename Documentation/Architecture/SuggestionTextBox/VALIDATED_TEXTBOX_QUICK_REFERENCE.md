# SuggestionTextBoxWithLabel - Quick Reference

## Mode Selection Decision Tree

```
Is this a selection from master data?
│
├─ YES → Use Suggestion Mode
│   ├─ Part numbers? → Helper_SuggestionTextBox.ConfigureForPartNumbers()
│   ├─ Locations? → Helper_SuggestionTextBox.ConfigureForLocations()
│   ├─ Operations? → Helper_SuggestionTextBox.ConfigureForOperations()
│   └─ Color codes? → Helper_SuggestionTextBox.ConfigureForColorCodes()
│
└─ NO → Use Validated Mode
    ├─ Quantity (integer)? → Helper_ValidatedTextBox.ConfigureForQuantity()
    ├─ Weight (decimal)? → Helper_ValidatedTextBox.ConfigureForWeight()
    ├─ Price (decimal)? → Helper_ValidatedTextBox.ConfigureForPrice()
    └─ Custom range? → Helper_ValidatedTextBox.ConfigureForNumeric()
```

---

## Validated Mode - Common Patterns

### Pattern 1: Quantity Input (Most Common)

```csharp
// Step 1: Configure (constructor)
Helper_ValidatedTextBox.ConfigureForQuantity(quantityTextBox);

// Step 2: Optional - Real-time feedback
quantityTextBox.ValidationChanged += (s, e) => UpdateSaveButtonState();

// Step 3: Validate (save method)
if (!Helper_ValidatedTextBox.ValidateControl(quantityTextBox, "Quantity"))
{
    return; // Error shown, focus set automatically
}

// Step 4: Safe parsing (already validated)
int qty = int.Parse(quantityTextBox.Text);
```

---

### Pattern 2: Multiple Controls (Same Type)

```csharp
// Configure all
Helper_ValidatedTextBox.ConfigureMultipleForQuantity(
    showValidationColor: true,
    quantityTextBox1,
    quantityTextBox2,
    quantityTextBox3);

// Validate all (stops at first error)
if (!Helper_ValidatedTextBox.ValidateMultiple(
    showError: true,
    quantityTextBox1,
    quantityTextBox2,
    quantityTextBox3))
{
    return;
}
```

---

### Pattern 3: Mixed Validator Types

```csharp
// Configure different types
Helper_ValidatedTextBox.ConfigureMultiple(
    (quantityTextBox, "Quantity", true),
    (weightTextBox, "Weight", true),
    (priceTextBox, "Price", true));

// Validate all
bool allValid = Helper_ValidatedTextBox.ValidateMultiple(
    showError: true,
    quantityTextBox,
    weightTextBox,
    priceTextBox);
```

---

## Built-in Validators

| Validator | Method | Type | Range | Use Case |
|-----------|--------|------|-------|----------|
| **Quantity** | `ConfigureForQuantity()` | Integer | 1-999,999 | Inventory quantity, item count |
| **Weight** | `ConfigureForWeight()` | Decimal | 0.01-99,999.99 | Item weight, package weight |
| **Price** | `ConfigureForPrice()` | Decimal | 0.01-9,999,999.99 | Unit price, cost |
| **Custom** | `ConfigureForNumeric()` | Integer/Decimal | Custom | Age, percentage, custom range |

---

## Visual Feedback (Automatic)

| State | Text Color | When |
|-------|------------|------|
| **Valid** | Black | Value passes validation OR field empty |
| **Invalid** | Red | Value fails validation (negative, non-numeric, out of range) |

**NO manual color management needed!**

---

## Key Properties

```csharp
// Check validation state
if (quantityTextBox.IsValid)
{
    int qty = int.Parse(quantityTextBox.Text);
}

// Enable/disable suggestions (set automatically)
quantityTextBox.EnableSuggestions = false; // Validated mode
partNumberTextBox.EnableSuggestions = true; // Suggestion mode

// Show/hide validation colors
quantityTextBox.ShowValidationColor = true; // Default
```

---

## Key Events

```csharp
// Fires when validation state changes
quantityTextBox.ValidationChanged += (sender, result) =>
{
    if (result.IsValid)
    {
        Console.WriteLine($"{result.FieldName} is valid!");
    }
    else
    {
        Console.WriteLine($"Error: {result.ErrorMessage}");
    }
    
    UpdateSaveButtonState();
};
```

---

## Common Mistakes

### ❌ WRONG

```csharp
// Manual validation
if (!int.TryParse(textBox.Text, out int qty) || qty <= 0)
{
    Service_ErrorHandler.HandleValidationError(...);
    textBox.Focus();
    return;
}

// Manual color management
textBox.ForeColor = isValid ? Color.Black : Color.Red;

// Using plain TextBox
private TextBox Control_InventoryTab_TextBox_Quantity;
```

### ✅ CORRECT

```csharp
// Automatic validation
if (!Helper_ValidatedTextBox.ValidateControl(quantityTextBox, "Quantity"))
{
    return; // Error shown, focus set, color managed automatically
}

int qty = int.Parse(quantityTextBox.Text); // Safe!

// Using SuggestionTextBoxWithLabel
private SuggestionTextBoxWithLabel Control_InventoryTab_TextBox_Quantity;
```

---

## Utility Methods

```csharp
// Clear controls
Helper_ValidatedTextBox.Clear(quantityTextBox);
Helper_ValidatedTextBox.ClearMultiple(qty1, qty2, qty3);

// Enable/disable controls
Helper_ValidatedTextBox.SetEnabled(quantityTextBox, enabled: false);
Helper_ValidatedTextBox.SetEnabledMultiple(true, qty1, qty2, qty3);

// Get validation results (no error dialogs)
var results = Helper_ValidatedTextBox.GetValidationResults(qty1, qty2, qty3);
foreach (var kvp in results)
{
    if (!kvp.Value.IsValid)
    {
        Console.WriteLine($"{kvp.Key}: {kvp.Value.ErrorMessage}");
    }
}
```

---

## Migration Checklist

Migrating from manual validation to validated mode:

- [ ] Replace `TextBox` with `SuggestionTextBoxWithLabel` in Designer.cs
- [ ] Remove `TextChanged` event handler
- [ ] Remove `Leave` event handler
- [ ] Remove manual validation method (e.g., `ValidateQtyTextBox()`)
- [ ] Add `Helper_ValidatedTextBox.ConfigureForQuantity()` in constructor
- [ ] Optional: Add `ValidationChanged` event for real-time feedback
- [ ] Replace manual `int.TryParse()` with `ValidateControl()` in save method
- [ ] Test: Red text for invalid, black for valid
- [ ] Test: Validation error dialog on save
- [ ] Test: Focus set to invalid control

---

## When to Use Which Mode

### Suggestion Mode (Datatable)
- ✅ Part number selection
- ✅ Location selection
- ✅ Operation selection
- ✅ Color code selection
- ✅ Any autocomplete from database

### Validated Mode (No Datatable)
- ✅ Quantity input
- ✅ Weight input
- ✅ Price input
- ✅ Age, percentage, count
- ✅ Any numeric validation

---

## Example: Form with Both Modes

```csharp
public partial class MyForm : ThemedForm
{
    // Suggestion mode (datatable)
    private SuggestionTextBoxWithLabel PartNumberTextBox = new();
    private SuggestionTextBoxWithLabel LocationTextBox = new();
    
    // Validated mode (no datatable)
    private SuggestionTextBoxWithLabel QuantityTextBox = new();
    private SuggestionTextBoxWithLabel WeightTextBox = new();
    
    private void ConfigureControls()
    {
        // Suggestion mode
        Helper_SuggestionTextBox.ConfigureForPartNumbers(
            PartNumberTextBox.TextBox,
            GetPartNumberSuggestionsAsync,
            enableF4: true);
        
        // Validated mode
        Helper_ValidatedTextBox.ConfigureForQuantity(QuantityTextBox);
        Helper_ValidatedTextBox.ConfigureForWeight(WeightTextBox);
    }
    
    private void SaveButton_Click(object sender, EventArgs e)
    {
        // Validate validated controls
        if (!Helper_ValidatedTextBox.ValidateMultiple(
            showError: true,
            QuantityTextBox,
            WeightTextBox))
        {
            return; // Error shown, focus set
        }
        
        // Check suggestion controls
        if (string.IsNullOrWhiteSpace(PartNumberTextBox.Text))
        {
            Service_ErrorHandler.HandleValidationError(
                "Part Number is required.",
                "Part Number");
            PartNumberTextBox.Focus();
            return;
        }
        
        // Safe to parse (already validated)
        int qty = int.Parse(QuantityTextBox.Text);
        decimal weight = decimal.Parse(WeightTextBox.Text);
        
        // Save data...
    }
}
```

---

## Code Savings

| Scenario | Before | After | Savings |
|----------|--------|-------|---------|
| Single quantity field | 50 lines | 5 lines | 90% |
| Three quantity fields | 150 lines | 15 lines | 90% |
| Mixed validators (3 fields) | 150 lines | 15 lines | 90% |

---

## Related Documentation

- **User Guide**: `Documentation/SuggestionTextBoxWithLabel-Validated-Mode.md`
- **Migration Plan**: `Documentation/SuggestionTextBoxWithLabel-Migration-Plan.md`
- **Instructions**: `.github/instructions/suggestiontextboxwithlabel-validated-mode.instructions.md`
- **Example**: `Examples/Example_ValidatedTextBox_Form.cs`
- **Summary**: `VALIDATED_TEXTBOX_SUMMARY.md`

---

## Quick Start (Copy-Paste Template)

```csharp
// === In Designer.cs or fields section ===
private SuggestionTextBoxWithLabel QuantityTextBox = new();

// === In constructor ===
Helper_ValidatedTextBox.ConfigureForQuantity(QuantityTextBox);
QuantityTextBox.ValidationChanged += (s, e) => UpdateSaveButtonState();

// === In save method ===
if (!Helper_ValidatedTextBox.ValidateControl(QuantityTextBox, "Quantity"))
{
    return;
}

int qty = int.Parse(QuantityTextBox.Text);
// Use qty safely...
```

---

**Remember:**
- **Suggestion Mode** = Master data selection (F4 button, datatable)
- **Validated Mode** = Simple validation (NO F4 button, NO datatable)

Choose the right tool for the job!
