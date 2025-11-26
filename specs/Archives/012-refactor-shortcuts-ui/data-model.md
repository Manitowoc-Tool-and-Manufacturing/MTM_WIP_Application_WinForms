# Data Model: Shortcuts

## Entities

### Shortcut
Represents a keyboard shortcut action in the application.

| Property | Type | Description |
|----------|------|-------------|
| `Name` | `string` | Unique identifier (e.g., "Inventory_Save"). |
| `Keys` | `int` (Keys enum) | The key combination (e.g., `Keys.Control | Keys.S`). |
| `Description` | `string` | User-friendly description. |
| `Category` | `string` | Grouping category (e.g., "Inventory", "General"). |
| `IsCustom` | `bool` | *Computed*: True if the user has overridden the default. |

### ShortcutCategory
Logical grouping of shortcuts for UI display.

| Property | Type | Description |
|----------|------|-------------|
| `Name` | `string` | Category name (e.g., "Inventory"). |
| `Shortcuts` | `List<Shortcut>` | List of shortcuts in this category. |

## Storage

- **Database**: `app_user_shortcuts` table (User overrides).
- **File**: `Resources/default_shortcuts.json` (System defaults).
- **In-Memory**: `Service_Shortcut` cache (`Dictionary<string, Model_Shortcut>`).

## Validation Rules

1. **Uniqueness**: A key combination MUST NOT be assigned to more than one action within the same scope (Global vs. Context-Specific). *Current implementation assumes global uniqueness for simplicity unless scoped.*
2. **Reserved Keys**: `Alt+0` through `Alt+9` are reserved for QuickButtons.
3. **System Keys**: OS-reserved keys (e.g., `Win+L`) cannot be captured/assigned.
