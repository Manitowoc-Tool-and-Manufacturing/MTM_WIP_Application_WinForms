# UI Development Workflow

This workflow defines the standard processes for creating new User Interfaces and refactoring existing ones in the MTM WIP Application.

## 1. New UI Creation Workflow

Follow this process when creating a new Form or UserControl.

### Phase 1: Setup & Inheritance
1.  **Create File**: Add new Form/UserControl to the appropriate folder (e.g., `Forms/Sales/`).
2.  **Inherit Base Class**:
    -   Change `public partial class MyForm : Form` to `: ThemedForm`.
    -   Change `public partial class MyControl : UserControl` to `: ThemedUserControl`.
3.  **Verify Designer**: Rebuild solution and open the designer to ensure it loads without errors.

### Phase 2: Layout Structure
1.  **Root Container**:
    -   Add a `TableLayoutPanel` to the form.
    -   Name it `[Context]_TableLayout_Main` (e.g., `SalesForm_TableLayout_Main`).
    -   Set `Dock = Fill`.
2.  **Define Regions**:
    -   Configure rows/columns for Header, Content, and Actions.
    -   Avoid absolute positioning.

### Phase 3: Control Implementation
1.  **Add Inputs**:
    -   Use `SuggestionTextBoxWithLabel` for data entry.
    -   Name them `[Context]_SuggestionBox_[Entity]`.
2.  **Add Grids**:
    -   Place `DataGridView` inside a `Panel` (e.g., `[Context]_Panel_Grid`).
    -   Dock the panel to Fill its table cell.
3.  **Add Actions**:
    -   Use `FlowLayoutPanel` for button groups.
    -   Name buttons `[Context]_Button_[Action]`.
4.  **Settings-Specific UI**:
    -   When working inside the Settings area, use the card/tile pattern from `Control_SettingsHome.Designer.cs` and `Control_Database.Designer.cs` instead of `GroupBox` layouts.
    -   Create any new card/tile variations as separate reusable controls so other settings tabs can consume them.

### Phase 4: Theme Verification
1.  **Check Code**: Ensure NO `Core_Themes.ApplyTheme()` calls exist.
2.  **Run & Test**: Launch app, open form, and toggle Light/Dark themes to verify automatic updates.

---

## 2. UI Refactoring Workflow

Follow this process when modernizing legacy forms.

### Phase 1: Analysis & Prep
1.  **Audit Controls**: Identify legacy patterns (Label+TextBox pairs, absolute positioning).
2.  **Backup**: Ensure you have a clean git state before starting.

### Phase 2: Structural Updates
1.  **Update Inheritance**:
    -   Switch to `ThemedForm` / `ThemedUserControl`.
    -   Remove manual theme calls.
2.  **Introduce Layout**:
    -   Add a root `TableLayoutPanel`.
    -   Move existing controls into the layout structure.
    -   *Note*: This may break tab order initially.

### Phase 3: Component Replacement
1.  **Replace Inputs**:
    -   Delete old Label + TextBox pairs.
    -   Insert `SuggestionTextBoxWithLabel`.
    -   Map old event handlers (`TextChanged` -> `SuggestionSelected`).
2.  **Wrap Grids**:
    -   Move DataGridViews into container Panels if they aren't already.

### Phase 4: Standardization
1.  **Rename Controls**:
    -   Apply `[Context]_[Type]_[Name]` convention to ALL controls.
    -   Update references in code-behind.
2.  **Fix Tab Order**:
    -   Use Visual Studio's "View > Tab Order" tool to fix navigation sequence.
3.  **Clean Code**:
    -   Organize `.Designer.cs` into standard `#region` blocks.
    -   Remove unused legacy fields.

### Phase 5: Validation
1.  **Build**: Ensure no compilation errors from renaming.
2.  **Runtime Test**: Verify all functionality works and theming applies correctly.
