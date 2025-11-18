# Quick Start Guide: Suggestion TextBox Integration

**Feature**: Universal Suggestion System for TextBox Inputs  
**Date**: November 12, 2025  
**Audience**: Developers integrating SuggestionTextBox into forms

## Overview

This guide shows how to replace existing ComboBox controls with SuggestionTextBox to enable intelligent autocomplete/filtering for master data fields.

**Benefits**:
- Reduces memory usage (no pre-populated ComboBoxes)
- Faster data entry (intelligent filtering + keyboard navigation)
- Better UX for large datasets (1000+ items)
- Wildcard search support (% symbol)
- Consistent behavior across all forms

---

## Quick Integration (5 Minutes)

### Step 1: Replace ComboBox with SuggestionTextBox

**Before** (ComboBox):
```csharp
// In Designer.cs
private System.Windows.Forms.ComboBox comboBoxPartNumber;

// In form constructor
comboBoxPartNumber.DataSource = Model_Application_Variables.CachedParts;
```

**After** (SuggestionTextBox):
```csharp
// In Designer.cs
private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox suggestionTextBoxPartNumber;

// In form constructor or Load event
suggestionTextBoxPartNumber.DataProvider = GetPartNumberSuggestionsAsync;
suggestionTextBoxPartNumber.MaxResults = 100;
suggestionTextBoxPartNumber.EnableWildcards = true;
```

### Step 2: Implement Data Provider

```csharp
private async Task<List<string>> GetPartNumberSuggestionsAsync()
{
    try
    {
        var dao = new Dao_Part();
        var result = await dao.GetAllPartIDsAsync();
        
        if (result.IsSuccess && result.Data != null)
        {
            return result.Data.AsEnumerable()
                              .Select(r => r["PartID"]?.ToString() ?? "")
                              .Where(s => !string.IsNullOrWhiteSpace(s))
                              .Distinct()
                              .ToList();
        }
        
        Service_ErrorHandler.HandleDatabaseError(
            new Exception(result.ErrorMessage),
            contextData: new Dictionary<string, object>
            {
                ["Source"] = "GetPartNumberSuggestions",
                ["Form"] = this.Name
            }
        );
        
        return new List<string>();
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
        return new List<string>();
    }
}
```

### Step 3: Handle Events (Optional)

```csharp
// In form constructor or Load event
suggestionTextBoxPartNumber.SuggestionSelected += SuggestionTextBoxPartNumber_SuggestionSelected;
suggestionTextBoxPartNumber.SuggestionCancelled += SuggestionTextBoxPartNumber_SuggestionCancelled;

private void SuggestionTextBoxPartNumber_SuggestionSelected(object sender, SuggestionSelectedEventArgs e)
{
    
    
    // Optional: Trigger dependent field updates
    LoadPartDetailsAsync(e.SelectedValue);
}

private void SuggestionTextBoxPartNumber_SuggestionCancelled(object sender, SuggestionCancelledEventArgs e)
{
    if (e.Reason == SuggestionCancelReason.NoMatches)
    {
        
    }
}
```

---

## Common Scenarios

### Scenario 1: Part Number Field (Database Query)

```csharp
// Property
private async Task<List<string>> GetPartNumberSuggestionsAsync()
{
    var dao = new Dao_Part();
    var result = await dao.GetAllPartIDsAsync();
    return result.IsSuccess 
        ? result.Data.AsEnumerable().Select(r => r["PartID"].ToString()).ToList()
        : new List<string>();
}

// Configuration
suggestionTextBoxPartNumber.DataProvider = GetPartNumberSuggestionsAsync;
suggestionTextBoxPartNumber.MaxResults = 100;
suggestionTextBoxPartNumber.EnableWildcards = true;
suggestionTextBoxPartNumber.ClearOnNoMatch = true; // MTM validation pattern
```

### Scenario 2: Operation Field (In-Memory Cached)

```csharp
// Property (if operations cached at startup)
private Task<List<string>> GetOperationSuggestionsAsync()
{
    return Task.FromResult(Model_Application_Variables.CachedOperations.ToList());
}

// Configuration
suggestionTextBoxOperation.DataProvider = GetOperationSuggestionsAsync;
suggestionTextBoxOperation.MaxResults = 50; // Operations typically <100
suggestionTextBoxOperation.EnableWildcards = true;
```

### Scenario 3: Location Field with Building Filter

```csharp
// Property with parameter
private async Task<List<string>> GetLocationSuggestionsAsync()
{
    var building = comboBoxBuilding.SelectedValue?.ToString();
    var dao = new Dao_Location();
    var result = await dao.GetLocationsByBuildingAsync(building);
    return result.IsSuccess
        ? result.Data.AsEnumerable().Select(r => r["Location"].ToString()).ToList()
        : new List<string>();
}

// Configuration
suggestionTextBoxLocation.DataProvider = GetLocationSuggestionsAsync;
suggestionTextBoxLocation.MaxResults = 100;
suggestionTextBoxLocation.EnableWildcards = true;

// Update when building changes
comboBoxBuilding.SelectedIndexChanged += (s, e) =>
{
    suggestionTextBoxLocation.RefreshDataSource(); // Clear cache
};
```

### Scenario 4: Customer Name (Settings Form)

```csharp
// Property
private async Task<List<string>> GetCustomerSuggestionsAsync()
{
    var dao = new Dao_Part();
    var result = await dao.GetAllCustomersAsync();
    return result.IsSuccess
        ? result.Data.AsEnumerable()
                     .Select(r => r["Customer"].ToString())
                     .Distinct()
                     .OrderBy(c => c)
                     .ToList()
        : new List<string>();
}

// Configuration
suggestionTextBoxCustomer.DataProvider = GetCustomerSuggestionsAsync;
suggestionTextBoxCustomer.MaxResults = 100;
suggestionTextBoxCustomer.EnableWildcards = false; // Customer names don't need wildcards
suggestionTextBoxCustomer.MinimumInputLength = 2; // Require 2+ characters before triggering
```

---

## Configuration Options

### Basic Configuration

```csharp
suggestionTextBox1.DataProvider = GetDataAsync;          // REQUIRED
suggestionTextBox1.MaxResults = 100;                     // Default: 100
suggestionTextBox1.EnableWildcards = true;               // Default: true
suggestionTextBox1.ShowLoadingIndicator = true;          // Default: true
suggestionTextBox1.LoadingThresholdMs = 100;             // Default: 100ms
suggestionTextBox1.SuppressExactMatch = true;            // Default: true
suggestionTextBox1.ClearOnNoMatch = true;                // Default: true (MTM pattern)
suggestionTextBox1.MinimumInputLength = 0;               // Default: 0 (no minimum)
```

### Using Configuration Model

```csharp
var config = new Model_Suggestion_Config
{
    DataProvider = GetPartNumberSuggestionsAsync,
    MaxResults = 50,
    EnableWildcards = true,
    ShowLoadingIndicator = true,
    ClearOnNoMatch = true
};

suggestionTextBoxPartNumber.ApplyConfig(config);
```

---

## User Experience

### Keyboard Navigation

| Key | Action |
|-----|--------|
| **Tab** | Trigger overlay (exit field) |
| **↓ Down Arrow** | Select next item |
| **↑ Up Arrow** | Select previous item |
| **Home** | Select first item |
| **End** | Select last item |
| **Enter** | Accept selection, move to next field |
| **Escape** | Cancel, keep original input, stay on field |

### Mouse Interaction

| Action | Result |
|--------|--------|
| **Single Click** | Highlight item (not selected) |
| **Double Click** | Accept selection, move to next field |
| **Click Outside** | Cancel (light dismiss), keep original input |

### Wildcard Patterns

| Pattern | Matches |
|---------|---------|
| `R-%` | All items starting with "R-" |
| `%-A` | All items ending with "A" |
| `R-%-01` | Items like "R-ABC-01", "R-XYZ-01" |
| `%SHOP%` | All items containing "SHOP" anywhere |

---

## Error Handling

### DAO Error Handling

```csharp
private async Task<List<string>> GetDataAsync()
{
    try
    {
        var dao = new Dao_Something();
        var result = await dao.GetAllAsync();
        
        if (result.IsSuccess)
        {
            return result.Data.AsEnumerable()
                              .Select(r => r["ColumnName"].ToString())
                              .ToList();
        }
        
        // Handle DAO failure
        Service_ErrorHandler.HandleDatabaseError(
            new Exception(result.ErrorMessage),
            contextData: new Dictionary<string, object>
            {
                ["Source"] = "GetDataAsync",
                ["Form"] = this.Name,
                ["Field"] = "FieldName"
            }
        );
        
        return new List<string>(); // Return empty list (no crash)
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
        return new List<string>();
    }
}
```

### Logging User Actions

```csharp
// Selection events
suggestionTextBox1.SuggestionSelected += (sender, e) =>
{
    
};

suggestionTextBox1.SuggestionCancelled += (sender, e) =>
{
    
};

// Overlay events (optional, for diagnostics)
suggestionTextBox1.SuggestionOverlayOpened += (sender, e) =>
{
    
};
```

---

## Testing Checklist

### Manual Test Cases

✅ **Basic Functionality**
- [ ] Type partial text, tab away → Overlay displays filtered results
- [ ] Select item with Enter → Field updates, focus moves to next
- [ ] Press Escape → Overlay closes, original text preserved
- [ ] Click outside overlay → Overlay closes (light dismiss)

✅ **Wildcard Search**
- [ ] Type "R-%" → Shows all items starting with "R-"
- [ ] Type "%-A" → Shows all items ending with "A"
- [ ] Type "R-%-01" → Shows pattern matches

✅ **Edge Cases**
- [ ] Type exact match → No overlay (suppressed)
- [ ] Type invalid text → Field cleared, warning shown
- [ ] Type nothing, tab away → No overlay
- [ ] Type <MinimumInputLength characters → No overlay

✅ **Keyboard Navigation**
- [ ] Arrow keys navigate list
- [ ] Home/End jump to first/last
- [ ] Enter accepts selection
- [ ] Escape cancels

✅ **Performance**
- [ ] Overlay appears <100ms
- [ ] Filtering 1000+ items is smooth
- [ ] No UI lag during typing

✅ **Theme Integration**
- [ ] Overlay matches application theme (light/dark)
- [ ] Colors consistent with ThemedForm

✅ **Error Handling**
- [ ] Database error shows user-friendly message
- [ ] Slow data source shows loading indicator
- [ ] Empty data source returns empty list (no crash)

---

## Troubleshooting

### Issue: Overlay Doesn't Appear

**Possible Causes**:
1. DataProvider not set → Check `suggestionTextBox1.DataProvider != null`
2. MinimumInputLength not met → Check text length >= MinimumInputLength
3. Exact match found → Check SuppressExactMatch setting
4. No matches in data → Check data provider returns non-empty list

**Solution**: Add logging to data provider to verify it's being called:
```csharp
private async Task<List<string>> GetDataAsync()
{
    
    var result = await dao.GetAllAsync();
    
    return ...; 
}
```

### Issue: Overlay Positioned Off-Screen

**Cause**: Multi-monitor setup or form near screen edge

**Solution**: Automatic - overlay detects screen boundaries and positions itself above TextBox if would extend beyond screen bottom.

### Issue: Slow Performance

**Possible Causes**:
1. Data provider takes >100ms (database query slow)
2. Large dataset (>10,000 items)

**Solutions**:
- Add database indexes on queried columns
- Implement caching at application startup
- Reduce MaxResults to limit filtering work

### Issue: Field Cleared Unexpectedly

**Cause**: ClearOnNoMatch is true (MTM validation pattern) and no suggestions matched user input

**Solution**: Either:
- Set `ClearOnNoMatch = false` if you want to preserve invalid input
- Improve data source to include expected values

---

## Migration Guide: ComboBox → SuggestionTextBox

### 1. Identify ComboBoxes to Replace

**Criteria for replacement**:
- ✅ Populated from master data tables (md_*, usr_*, sys_*)
- ✅ Contains >20 items (benefits from filtering)
- ✅ Users frequently scroll/search
- ❌ Fixed enum values (3-10 options like Shift: Day/Night)
- ❌ Boolean toggles or Yes/No selections

### 2. Update Designer File

```csharp
// Find and replace in .Designer.cs
// OLD:
private System.Windows.Forms.ComboBox comboBoxPartNumber;

// NEW:
private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox suggestionTextBoxPartNumber;

// Update InitializeComponent():
// OLD:
this.comboBoxPartNumber = new System.Windows.Forms.ComboBox();
// ... properties ...
this.comboBoxPartNumber.DataSource = ...;

// NEW:
this.suggestionTextBoxPartNumber = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
// ... properties (Location, Size, Name, etc.) ...
// NO DataSource property!
```

### 3. Update Form Code

```csharp
// OLD ComboBox pattern:
private void Form_Load(object sender, EventArgs e)
{
    comboBoxPartNumber.DataSource = Model_Application_Variables.CachedParts;
}

// NEW SuggestionTextBox pattern:
private void Form_Load(object sender, EventArgs e)
{
    suggestionTextBoxPartNumber.DataProvider = GetPartNumberSuggestionsAsync;
    suggestionTextBoxPartNumber.MaxResults = 100;
    suggestionTextBoxPartNumber.EnableWildcards = true;
}

private async Task<List<string>> GetPartNumberSuggestionsAsync()
{
    var dao = new Dao_Part();
    var result = await dao.GetAllPartIDsAsync();
    return result.IsSuccess 
        ? result.Data.AsEnumerable().Select(r => r["PartID"].ToString()).ToList()
        : new List<string>();
}
```

### 4. Update Event Handlers

```csharp
// OLD ComboBox event:
comboBoxPartNumber.SelectedIndexChanged += ComboBoxPartNumber_SelectedIndexChanged;

private void ComboBoxPartNumber_SelectedIndexChanged(object sender, EventArgs e)
{
    var selectedPart = comboBoxPartNumber.SelectedValue?.ToString();
    LoadPartDetails(selectedPart);
}

// NEW SuggestionTextBox event:
suggestionTextBoxPartNumber.SuggestionSelected += SuggestionTextBoxPartNumber_SuggestionSelected;

private void SuggestionTextBoxPartNumber_SuggestionSelected(object sender, SuggestionSelectedEventArgs e)
{
    LoadPartDetails(e.SelectedValue);
}
```

### 5. Test Migration

✅ **Verify**:
- Form loads without errors
- Overlay displays when tabbing out of field
- Selections update field correctly
- Focus moves to next field after selection
- Error handling works (invalid input)

---

## Best Practices

### ✅ DO

- Use async data providers for database queries
- Handle errors gracefully (return empty list, don't throw)
- Log user selections for analytics
- Set appropriate MaxResults based on dataset size
- Enable wildcards for large datasets (>100 items)
- Use ClearOnNoMatch for strict validation (MTM pattern)

### ❌ DON'T

- Use MessageBox.Show for errors → Use Service_ErrorHandler
- Use Console.WriteLine for logging → Use LoggingUtility
- Block UI thread in data provider → Use async/await
- Return null from data provider → Return empty list
- Set MaxResults >1000 → Impacts performance
- Forget to dispose forms → SuggestionTextBox handles this

---

## Performance Optimization

### Caching Strategies

**Application Startup Cache**:
```csharp
// In application startup (Program.cs or main form)
public static class Model_Application_Variables
{
    public static List<string> CachedOperations { get; private set; }
    public static List<string> CachedLocations { get; private set; }
    
    public static async Task LoadMasterDataCacheAsync()
    {
        var daoOp = new Dao_Operation();
        var resultOp = await daoOp.GetAllOperationsAsync();
        CachedOperations = resultOp.IsSuccess 
            ? resultOp.Data.AsEnumerable().Select(r => r["Operation"].ToString()).ToList()
            : new List<string>();
        
        // Similar for locations, item types, etc.
    }
}

// In forms:
suggestionTextBoxOperation.DataProvider = () => Task.FromResult(Model_Application_Variables.CachedOperations.ToList());
```

**Per-Form Cache**:
```csharp
private List<string> _cachedParts = null;

private async Task<List<string>> GetPartNumberSuggestionsAsync()
{
    if (_cachedParts != null)
        return _cachedParts;
    
    var dao = new Dao_Part();
    var result = await dao.GetAllPartIDsAsync();
    _cachedParts = result.IsSuccess 
        ? result.Data.AsEnumerable().Select(r => r["PartID"].ToString()).ToList()
        : new List<string>();
    
    return _cachedParts;
}
```

---

## Support

**Questions?** Refer to:
- Constitution: `.specify/memory/constitution.md`
- Specification: `specs/001-suggestion-textbox/spec.md`
- Data Model: `specs/001-suggestion-textbox/data-model.md`
- API Contracts: `specs/001-suggestion-textbox/contracts/`

**Bugs/Issues?** Use Service_ErrorHandler and check log files in `%APPDATA%\MTM\Logs\`
