# Quickstart: Using Service_DataGridView

**Feature**: Centralize DataGridView Logic
**Status**: Draft

## Overview

`Service_DataGridView` centralizes common DataGridView operations to ensure consistency and reduce code duplication. Use this service instead of manually configuring columns, applying themes, or handling printing logic.

## Usage Examples

### 1. Configuring Columns & Theming

Replace manual column loops and theme calls with:

```csharp
// Define columns to show in order
string[] columnsToShow = { "Location", "PartID", "ColorCode", "Quantity", "Notes" };

// Configure columns (visibility, order)
Service_DataGridView.ConfigureColumns(MyDataGridView, columnsToShow);

// Apply theme and load user settings
await Service_DataGridView.ApplyStandardSettingsAsync(MyDataGridView, Model_Application_Variables.User);
```

### 2. Applying Color Coding (Inventory)

For grids showing inventory with "ColorCode" column:

```csharp
// Apply row colors based on ColorCode
Service_DataGridView.ApplyInventoryColorCoding(MyDataGridView);
```

### 3. Sorting by Color Priority

When loading data, use the custom sort before binding:

```csharp
DataTable rawData = ...;
DataTable sortedData = Service_DataGridView.SortInventoryByColorPriority(rawData);
MyDataGridView.DataSource = sortedData;
```

### 4. Printing

Replace the print button handler logic with:

```csharp
private async void Button_Print_Click(object sender, EventArgs e)
{
    await Service_DataGridView.PrintGridAsync(this, MyDataGridView, "Inventory Report");
}
```

## Migration Guide

1. **Identify** where `Core_Themes.ApplyThemeToDataGridView` is called.
2. **Replace** the surrounding column setup logic with `Service_DataGridView.ConfigureColumns`.
3. **Replace** the theme call with `Service_DataGridView.ApplyStandardSettingsAsync`.
4. **Identify** any `ApplyColorCodingToRows` private methods and replace with `Service_DataGridView.ApplyInventoryColorCoding`.
5. **Identify** print button handlers and replace body with `Service_DataGridView.PrintGridAsync`.
