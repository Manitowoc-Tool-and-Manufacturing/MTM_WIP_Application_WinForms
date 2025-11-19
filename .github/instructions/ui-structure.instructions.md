---
description: 'Standard UI structure, naming conventions, and layout patterns for WinForms designer files'
applyTo: '**/*.Designer.cs, **/*.cs'
---

# UI Structure & Designer Guidelines

## Table of Contents
- **Naming Conventions**: Strict rules for naming controls in Designer files.
- **Layout Architecture**: TableLayout-first approach and root structure guidelines.
- **Standard Control Patterns**: Guidelines for Inputs, Grids, Action Bars, and Cards.
- **Inheritance Requirements**: Mandatory base classes for Forms and UserControls.
- **Designer File Structure**: Required #region blocks and organization.
- **Workflow for Creating New UI**: Step-by-step process for new screens.
- **Specific Screen Patterns**: Structural guides for Transactions, Edit Screens, and Tabs.
- **Workflow for Refactoring Existing UI**: Checklist for modernizing legacy screens.

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
- Use the existing card/tile controls (`Control_SettingsCategoryCard`, `Control_SettingsCategoryTile`) shown in `Control_SettingsHome.Designer.cs`.
- Place them inside a `FlowLayoutPanel` or `TableLayoutPanel` with `AutoScroll = true`.
- **Settings Form Controls**: When building Settings tab content (e.g., Database, Theme, About), follow the card-based layout pattern from `Control_Database.Designer.cs` instead of nested `GroupBox` controls. This ensures consistent spacing, typography, and iconography.
- **Reusable Components**: If a new card type or custom UI element is required, create it as a separate control (e.g., `Controls/Shared` or `Controls/SettingsForm`) rather than inlining it inside the current file so that it can be reused by future settings tabs.

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

## 8. Workflow for Refactoring Existing UI

When updating legacy forms or controls to meet these standards:

1.  **Update Inheritance**:
    -   Change inheritance from `Form` to `ThemedForm`.
    -   Change inheritance from `UserControl` to `ThemedUserControl`.
    -   Remove any manual `Core_Themes.ApplyTheme()` calls.

2.  **Standardize Layout**:
    -   If the form uses absolute positioning, introduce a root `TableLayoutPanel` (`Dock = Fill`).
    -   Move existing controls into the TableLayout cells.
    -   Replace `Anchor` logic with `Dock` logic where appropriate.

3.  **Replace Legacy Inputs**:
    -   Identify `Label` + `TextBox` pairs.
    -   Replace them with a single `SuggestionTextBoxWithLabel`.
    -   Update code-behind to use the new control's properties (`.Text`, `.LabelText`).

4.  **Rename Controls**:
    -   Go through the `.Designer.cs` file (or use the Properties window).
    -   Rename every control to follow `[Context]_[Type]_[Name]`.
    -   *Tip*: Do this one by one to catch compile errors in code-behind immediately.

5.  **Clean Up Designer Code**:
    -   Ensure the `#region` structure matches the standard.
    -   Remove unused fields or components.

6.  **Verify Tab Order**:
    -   Use the "View > Tab Order" tool in Visual Studio to reset the tab sequence, as moving controls to TableLayouts often breaks it.
