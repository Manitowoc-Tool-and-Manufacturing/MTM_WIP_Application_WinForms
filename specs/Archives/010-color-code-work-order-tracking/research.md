# Research: Color Code & Work Order Tracking

**Feature**: Color Code & Work Order Tracking  
**Date**: 2025-11-13  
**Status**: Complete

## Overview

This document captures research decisions for implementing color code and work order tracking in the MTM WIP Application. All technical unknowns have been resolved through analysis of existing codebase patterns and WinForms best practices.

## Research Areas

### 1. Work Order Format Validation in WinForms

**Decision**: Create `Service_ColorCodeValidator` class with static validation methods

**Rationale**:
- Aligns with existing `Service_ErrorHandler` pattern (static utility class)
- Centralized validation logic reusable across controls
- Separates validation concern from UI logic
- Enables unit testing independent of UI

**Implementation Pattern**:
```csharp
public static class Service_ColorCodeValidator
{
    public static (bool isValid, string formattedValue, string errorMessage) 
        ValidateAndFormatWorkOrder(string input)
    {
        // Strip WO- prefix if present
        // Extract numeric portion
        // Validate 5-6 digits
        // Pad to 6 digits with leading zeros
        // Return WO-######
    }
    
    public static string FormatColorToTitleCase(string input)
    {
        // Convert to Title Case (Blueberry, not blueberry)
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
    }
}
```

**Alternatives Considered**:
- Regex validation inline in controls → Rejected: duplicates logic, harder to test
- Database-side validation only → Rejected: poor UX, delayed error feedback
- FluentValidation library → Rejected: overkill for two simple validations

---

### 2. Dynamic UI Control Visibility Based on Data Flags

**Decision**: Use existing SuggestionTextBox control visibility pattern from Inventory Tab

**Rationale**:
- Inventory Tab already has dynamic visibility for Operation dropdown
- Consistent UX with existing Part ID auto-complete behavior
- Leverages proven control (`SuggestionTextBox` in Controls/Shared/)
- Maintains theme integration automatically

**Implementation Pattern**:
```csharp
private async void PartIDTextBox_TextChanged(object sender, EventArgs e)
{
    var partId = PartIDTextBox.Text.Trim();
    bool requiresColorCode = Model_Application_Variables.ColorFlaggedParts.Contains(partId);
    
    ColorCodeTextBox.Visible = requiresColorCode;
    ColorCodeLabel.Visible = requiresColorCode;
    WorkOrderTextBox.Visible = requiresColorCode;
    WorkOrderLabel.Visible = requiresColorCode;
    
    if (!requiresColorCode)
    {
        ColorCodeTextBox.Clear();
        WorkOrderTextBox.Clear();
    }
}
```

**Alternatives Considered**:
- Always show fields, enable/disable → Rejected: visual clutter, confusing UX
- Modal dialog for color code entry → Rejected: breaks workflow, extra clicks
- Separate tab for color-coded parts → Rejected: fragments inventory process

---

### 3. DataGridView Column Show/Hide Patterns

**Decision**: Use `DataGridViewColumn.Visible` property with column index tracking

**Rationale**:
- Simple, built-in WinForms feature
- No performance impact (columns exist, just hidden)
- Existing Remove Tab already uses column visibility for different search modes
- Maintains column order when toggling visibility

**Implementation Pattern**:
```csharp
private void ShowColorCodeColumns(bool show)
{
    if (dgvResults.Columns.Contains("Color"))
    {
        dgvResults.Columns["Color"].Visible = show;
    }
    if (dgvResults.Columns.Contains("WorkOrder"))
    {
        dgvResults.Columns["WorkOrder"].Visible = show;
    }
}

// Auto-sort when color columns visible
if (showColorColumns)
{
    dgvResults.Sort(dgvResults.Columns["Color"], 
        System.ComponentModel.ListSortDirection.Ascending);
}
```

**Alternatives Considered**:
- Dynamically add/remove columns → Rejected: complex, breaks column positions
- Separate DataGridViews → Rejected: duplicates code, theme application issues
- Virtual mode with custom painting → Rejected: overkill for simple hide/show

---

### 4. SuggestionTextBox Integration for Color Code Selection

**Decision**: Reuse existing `Controls/Shared/SuggestionTextBox.cs` with data provider delegate

**Rationale**:
- Already used successfully for Part ID, Operation, Location selection
- Provides auto-complete with dropdown suggestions
- Handles keyboard navigation (arrow keys, Enter)
- Theme-aware and tested in production

**Implementation Pattern**:
```csharp
// In Control_InventoryTab initialization
ColorCodeSuggestionTextBox.DataProvider = async () =>
{
    var result = await Dao_ColorCode.GetAllAsync();
    if (result.IsSuccess && result.Data != null)
    {
        return result.Data.AsEnumerable()
            .Select(row => row["ColorCode"].ToString()!)
            .ToList();
    }
    return new List<string>();
};

// "OTHER" selection triggers dialog
ColorCodeSuggestionTextBox.TextChanged += (s, e) =>
{
    if (ColorCodeSuggestionTextBox.Text == "OTHER")
    {
        ShowCustomColorDialog();
    }
};
```

**Alternatives Considered**:
- Standard ComboBox → Rejected: doesn't match existing Part ID UX
- Custom dropdown control → Rejected: reinvents wheel, theme issues
- Radio button list → Rejected: takes too much screen space (10+ colors)

---

### 5. Title-Case String Formatting for Custom Colors

**Decision**: Use `TextInfo.ToTitleCase()` with lowercase conversion

**Rationale**:
- Built-in .NET feature, culture-aware
- Handles edge cases (McDonald's, O'Brien correctly)
- One-line implementation, no regex complexity
- Matches user expectations for proper nouns

**Implementation**:
```csharp
var titleCased = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
// "blueberry" → "Blueberry"
// "DARK GREEN" → "Dark Green"
// "mcdonald's red" → "Mcdonald'S Red" (acceptable for color names)
```

**Alternatives Considered**:
- Regex-based capitalization → Rejected: complex, culture-unaware
- First char uppercase only → Rejected: "dark green" → "Dark green" (inconsistent)
- Manual word splitting → Rejected: duplicates built-in functionality

---

### 6. MySQL 5.7.24 ALTER TABLE Strategies

**Decision**: Use separate ALTER TABLE statements with DEFAULT values for backward compatibility

**Rationale**:
- MySQL 5.7.24 doesn't support adding multiple columns in single statement reliably
- DEFAULT 'Unknown' ensures existing data remains valid
- NULL not allowed (enforces data integrity for new records)
- Separate migrations enable rollback per-column

**Implementation Pattern**:
```sql
-- Migration 003: Add ColorCode to inv_inventory
ALTER TABLE inv_inventory 
ADD COLUMN ColorCode VARCHAR(50) NOT NULL DEFAULT 'Unknown'
AFTER Notes;

CREATE INDEX idx_colorcode ON inv_inventory(ColorCode);

-- Migration 004: Add WorkOrder to inv_inventory
ALTER TABLE inv_inventory
ADD COLUMN WorkOrder VARCHAR(10) NOT NULL DEFAULT 'Unknown'
AFTER ColorCode;

CREATE INDEX idx_workorder ON inv_inventory(WorkOrder);
```

**Alternatives Considered**:
- Single ALTER TABLE with multiple columns → Rejected: MySQL 5.7.24 compatibility risk
- Nullable columns → Rejected: complicates validation, allows inconsistent data
- Separate color_codes table joined → Rejected: over-normalization, performance cost

---

### 7. Cache Refresh Patterns in Existing Codebase

**Decision**: Follow existing `Helper_UI_ComboBoxes.ReloadAllTabComboBoxesAsync()` pattern

**Rationale**:
- Proven pattern used by Part IDs, Operations, Locations master data
- Centralized cache management in `Model_Application_Variables`
- Shift+Click Reset already implemented in main form tabs
- Async loading prevents UI blocking

**Implementation Pattern**:
```csharp
// In Model_Application_Variables.cs
public static List<string> ColorFlaggedParts { get; set; } = new();
public static DataTable ColorCodes { get; set; } = new();

// In Helper_UI_ComboBoxes.cs
public static async Task ReloadColorCodeCachesAsync()
{
    var flaggedResult = await Dao_Part.GetAllColorCodeFlaggedAsync();
    if (flaggedResult.IsSuccess)
    {
        Model_Application_Variables.ColorFlaggedParts = 
            flaggedResult.Data.AsEnumerable()
            .Select(row => row["PartID"].ToString()!)
            .ToList();
    }
    
    var colorsResult = await Dao_ColorCode.GetAllAsync();
    if (colorsResult.IsSuccess)
    {
        Model_Application_Variables.ColorCodes = colorsResult.Data;
    }
}

// Called from Shift+Click Reset in Control_InventoryTab, etc.
await Helper_UI_ComboBoxes.ReloadColorCodeCachesAsync();
```

**Alternatives Considered**:
- Database query per use → Rejected: poor performance, unnecessary DB load
- In-memory cache with expiration → Rejected: complexity not needed for static data
- Redis/external cache → Rejected: overkill for desktop app, adds dependency

---

### 8. Settings Form Restart Prompt Integration

**Decision**: Use existing `Form_Settings.requiresRestart` boolean flag pattern

**Rationale**:
- Settings form already prompts restart for theme changes
- Proven UX: user closes Settings, sees "Restart required?" dialog
- Consistent with existing behavior users understand
- No code duplication needed

**Implementation Pattern**:
```csharp
// In Control_Edit_PartID.cs
private void SaveButton_Click(object sender, EventArgs e)
{
    bool colorFlagChanged = /* compare original vs current RequiresColorCode */;
    
    if (colorFlagChanged)
    {
        var parentForm = this.FindForm() as Form_Settings;
        if (parentForm != null)
        {
            parentForm.requiresRestart = true;
        }
    }
}

// In Form_Settings.FormClosing (already exists)
if (requiresRestart)
{
    var result = MessageBox.Show(
        "Changes require application restart. Restart now?",
        "Restart Required",
        MessageBoxButtons.YesNo);
        
    if (result == DialogResult.Yes)
    {
        Application.Restart();
    }
}
```

**Alternatives Considered**:
- Immediate restart after save → Rejected: loses unsaved changes in other tabs
- Manual refresh button → Rejected: users forget, causes data inconsistency
- Hot-reload cache without restart → Rejected: complex, may miss dependent state

---

## Technology Stack Summary

### Confirmed Technologies
- **Language**: C# 12.0 (.NET 8.0 Windows Forms)
- **Database**: MySQL 5.7.24 (no 8.0+ features)
- **ORM**: None (direct stored procedure calls via MySql.Data 9.4.0)
- **Validation**: Custom `Service_ColorCodeValidator` static class
- **UI Controls**: `SuggestionTextBox` (existing shared control)
- **Caching**: In-memory static properties in `Model_Application_Variables`
- **Error Handling**: `Service_ErrorHandler` (constitution requirement)
- **Logging**: `LoggingUtility` CSV format (constitution requirement)
- **Result Pattern**: `Model_Dao_Result<T>` (constitution requirement)

### Key Design Patterns
1. **DAO Pattern**: All database access through Data/ layer returning `Model_Dao_Result<T>`
2. **Service Layer**: Validation logic in Services/ (stateless static methods)
3. **Helper Pattern**: Caching and utility methods in Helpers/
4. **Theme Integration**: All controls inherit from `ThemedForm` or `ThemedUserControl`
5. **Async-First**: All I/O operations use async/await (no blocking)

### Performance Optimizations
- Cached color-flagged parts list (avoid repeated DB queries)
- Cached color codes (reloaded only on demand)
- Indexed ColorCode and WorkOrder columns
- 1000-record warning threshold for Show All operations
- Database-side sorting (ORDER BY in SQL, not in-memory)

## Risks & Mitigations

| Risk | Mitigation |
|------|-----------|
| Schema changes break existing queries | DEFAULT 'Unknown' ensures backward compatibility |
| Color sorting slows large datasets | Indexes on ColorCode/WorkOrder columns, 1000-record warning |
| Dynamic field visibility confuses users | Consistent with existing Part ID patterns, clear labeling |
| Custom colors create data inconsistencies | Title-case normalization, duplicate prevention in stored procedure |
| Cache staleness after Settings changes | Restart prompt forces cache refresh on app reload |
| Work order format variations | Auto-format validation with clear error messages |

## Open Questions

**None remaining** - All technical unknowns resolved through existing codebase analysis and WinForms best practices.

## References

- Existing codebase patterns: `Control_InventoryTab.cs`, `Helper_UI_ComboBoxes.cs`, `Service_ErrorHandler.cs`
- Constitution compliance: `.specify/memory/constitution.md`
- MySQL 5.7.24 documentation: ALTER TABLE syntax limitations
- WinForms DataGridView documentation: Column visibility and sorting
- .NET TextInfo documentation: ToTitleCase() behavior

---

**Research Complete**: All decisions documented, ready for Phase 1 Design
