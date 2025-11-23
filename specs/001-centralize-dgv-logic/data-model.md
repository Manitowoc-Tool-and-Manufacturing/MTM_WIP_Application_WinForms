# Data Model: Service_DataGridView

**Feature**: Centralize DataGridView Logic
**Status**: Draft

## Service Definition

### `Service_DataGridView`

A static service class providing centralized logic for DataGridView configuration, theming, and operations.

#### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `ConfigureColumns(DataGridView dgv, string[] visibleColumns, Dictionary<string, string>? headerRenames = null)` | `void` | Configures column visibility, order, and headers. |
| `ApplyStandardSettingsAsync(DataGridView dgv, string userId)` | `Task` | Applies standard theme and loads user settings. |
| `ApplyInventoryColorCoding(DataGridView dgv)` | `void` | Applies background colors to rows based on "ColorCode" column. |
| `SortInventoryByColorPriority(DataTable source)` | `DataTable` | Sorts inventory data by ColorCode priority then Location. |
| `ApplyTransactionRowColors(DataGridView dgv)` | `void` | Applies background colors based on TransactionType (IN/OUT/TRANSFER). |
| `PrintGridAsync(Control parent, DataGridView dgv, string title)` | `Task` | Validates grid content and shows print dialog. |

#### Constants / Internal Data

- **PredefinedColorCodes**: `HashSet<string>` - List of recognized colors (Red, Blue, Green, etc.) for sorting priority.

## Entities

### `GridSettings` (Internal to Core_Themes, but conceptually relevant)

| Field | Type | Description |
|-------|------|-------------|
| `Columns` | `List<ColumnSetting>` | List of column configurations. |

### `ColumnSetting`

| Field | Type | Description |
|-------|------|-------------|
| `Name` | `string` | Column name. |
| `Visible` | `bool` | Visibility state. |
| `DisplayIndex` | `int` | Display order. |
