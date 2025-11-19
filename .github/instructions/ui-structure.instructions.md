---
description: 'Standard UI structure, naming conventions, and layout patterns for WinForms designer files'
applyTo: '**/*.Designer.cs, **/*.cs'
---

# UI Structure & Designer Guidelines

This document defines the standard patterns for constructing User Interfaces in the MTM WIP Application. These patterns are derived from existing controls (InventoryTab, Transactions, Settings) and must be followed for all new UI development to ensure consistency.

## 1. Naming Conventions

All controls in `.Designer.cs` files must follow the strict naming convention:
`[Context]_[ControlType]_[Functionality]`

### Component Prefixes
| Control Type | Prefix | Example |
| :--- | :--- | :--- |
| TableLayoutPanel | `TableLayout` | `Control_InventoryTab_TableLayout_Main` |
| FlowLayoutPanel | `FlowPanel` | `Control_Edit_PartID_FlowPanel_Actions` |
| Panel | `Panel` | `Transactions_Panel_Grid` |
| GroupBox | `GroupBox` | `Control_TransferTab_GroupBox_MainControl` |
| Button | `Button` | `Control_Database_Button_Save` |
| Label | `Label` | `Transactions_Label_Title` |
| TextBox | `TextBox` | `Control_Database_TextBox_Server` |
| SuggestionBox | `SuggestionBox` | `Control_InventoryTab_SuggestionBox_Part` |
| DataGridView | `DataGridView` | `Control_RemoveTab_DataGridView_Main` |
| CheckBox | `CheckBox` | `Control_Edit_PartID_CheckBox_RequiresColorCode` |

### Context Rules
- **Forms**: Use the Form name (e.g., `Transactions_...`)
- **UserControls**: Use the Control name (e.g., `Control_InventoryTab_...`)
- **Sub-sections**: Can include sub-context (e.g., `TransactionSearchControl_GroupBox_DateRange`)

## 2. Layout Architecture

The application uses a **TableLayout-first** approach. Absolute positioning (`Point(x, y)`) should be avoided in favor of `Dock` and `Anchor` properties within layout containers.

### Root Structure
Every Form or UserControl should start with a primary container:
1. **Main Container**: Usually a `TableLayoutPanel` or a `Panel` with `Dock = DockStyle.Fill`.
   - Name: `[Context]_TableLayout_Main` or `[Context]_Panel_Main`
2. **Header/Title**: (Optional) Top row/section for title labels.
3. **Content Area**: Middle section (often `Dock = Fill`).
4. **Action Area**: Bottom section or specific panel for buttons.

### Layout Containers
- **TableLayoutPanel**: Used for almost all structural layout (aligning labels with inputs, dividing screen into major sections).
- **FlowLayoutPanel**: Used for:
  - Lists of "Cards" (e.g., `Control_SettingsHome`).
  - Groups of Action Buttons (e.g., Save/Reset buttons in `Control_Edit_PartID`).
- **GroupBox**: Used to group related inputs logically (e.g., "Date Range", "Transaction Types").

## 3. Standard Control Patterns

### Input Fields
**Do not** use standalone `Label` + `TextBox` pairs.
- **Standard**: Use `SuggestionTextBoxWithLabel`.
- **Pattern**:
  ```csharp
  this.Control_InventoryTab_SuggestionBox_Part = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
  this.Control_InventoryTab_SuggestionBox_Part.LabelText = "Part Number";
  this.Control_InventoryTab_SuggestionBox_Part.EnableSuggestions = true;
  ```

### Data Grids
**Never** place a `DataGridView` directly into a form without a container.
- **Pattern**: Wrap `DataGridView` in a `Panel`.
- **Naming**: `[Context]_Panel_DataGridView` -> `[Context]_DataGridView_Main`.
- **Reason**: Provides better control over borders, scrolling, and docking behavior.

### Action Bars
Buttons should be grouped together, typically at the bottom or right of a view.
- **Container**: `FlowLayoutPanel`
- **Properties**: `FlowDirection = RightToLeft` (for bottom-right alignment) or `TopDown`.
- **Standard Buttons**: Save, Reset, Cancel, Print, Export.

### Settings/Dashboard "Cards"
For navigation or settings summaries (like `Control_SettingsHome`):
- Use `Control_SettingsCategoryCard` or `Control_SettingsCategoryTile`.
- Place them inside a `FlowLayoutPanel` with `AutoScroll = true`.

## 4. Inheritance Requirements

- **Forms**: MUST inherit from `ThemedForm`.
  ```csharp
  public partial class MyForm : ThemedForm
  ```
- **UserControls**: MUST inherit from `ThemedUserControl`.
  ```csharp
  public partial class MyControl : ThemedUserControl
  ```

## 5. Designer File Structure

The `.Designer.cs` file must be organized with regions:

```csharp
partial class MyControl
{
    #region Fields
    private System.ComponentModel.IContainer components = null;
    // Declare all controls here with proper naming
    private System.Windows.Forms.TableLayoutPanel MyControl_TableLayout_Main;
    #endregion

    #region Methods
    protected override void Dispose(bool disposing) { ... }

    private void InitializeComponent()
    {
        // 1. Instantiation
        // 2. Property Configuration
        // 3. Layout/Docking
        // 4. Event Subscription
    }
    #endregion
}
```

## 6. Workflow for Creating New UI

1. **Define the Context**: Determine the prefix (e.g., `Control_NewFeature`).
2. **Inherit Base**: Ensure class inherits `ThemedUserControl` or `ThemedForm`.
3. **Create Root Layout**: Add `TableLayoutPanel` (`..._TableLayout_Main`) and set `Dock = Fill`.
4. **Add Sections**:
   - If inputs are needed: Add `GroupBox` or nested `TableLayoutPanel`.
   - If grid is needed: Add `Panel` -> `DataGridView`.
   - If buttons are needed: Add `FlowLayoutPanel`.
5. **Add Controls**: Use `SuggestionTextBoxWithLabel` for inputs.
6. **Rename Controls**: Immediately rename all controls to follow the `[Context]_[Type]_[Name]` convention.
7. **Verify Tab Order**: Ensure logical tab progression.

## 7. Specific Screen Patterns

### Transaction/List Screens (e.g., `Transactions.cs`)
- **Structure**:
  - Top: Search/Filter Control (e.g., `TransactionSearchControl`).
  - Bottom/Fill: Grid Control (e.g., `TransactionGridControl`).
- **Composition**: Prefer composing complex screens from smaller, reusable UserControls rather than building everything in one monolithic file.

### Detail/Edit Screens (e.g., `Control_Edit_PartID.cs`)
- **Structure**:
  - `TableLayoutPanel` (2 columns: Labels, Inputs).
  - `FlowLayoutPanel` at bottom for Save/Cancel buttons.

### Tab Pages (e.g., `Control_InventoryTab.cs`)
- **Structure**:
  - `GroupBox` as the visual container.
  - `TableLayoutPanel` to organize Top (Inputs), Middle (Notes/Extras), Bottom (Actions).
