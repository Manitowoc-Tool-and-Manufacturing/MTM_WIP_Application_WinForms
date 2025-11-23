# Control_AutoExpandPanels Instructions

# Control_AutoExpandPanels Instructions

## Overview
This document outlines the controls and buttons that should be affected by the "Auto-Expand Panels on Reset/Search" setting in `Control_Theme`.

## Target Controls

| UserControl | ToggleButton1 | ToggleButton2 | Notes |
| :--- | :--- | :--- | :--- |
| **Control_InventoryTab** | Quick Buttons Toggle | | Expand Quick Buttons on Save? |
| **Control_RemoveTab** | Quick Buttons Toggle | Search Controls Panel | Collapse Search Panel on Search. Expand on Reset. |
| **Control_TransferTab** | Quick Buttons Toggle | Search Controls Panel | Collapse Search Panel on Search. Expand on Reset. |
| **TransactionsForm** | SearchPanel | DetailsPanel | Collapse Search Panel on Search? |
| **SettingsForm** | TreeViewPanel | | Collapse TreeView when panel shown? |
| **AdvancedInventory (Single)** | Quick Buttons Toggle | Search Controls Panel | Also fix this button's directions, they are backwards |
| **AdvancedInventory (Multi)** | Quick Buttons Toggle | Search Controls Panel | Also fix this button's directions, they are backwards |
| **AdvancedRemove** | Quick Buttons Toggle | Search Controls Panel | Also fix this button's directions, they are backwards |

## Implementation Plan

1.  **Settings**: Ensure `Model_Application_Variables.AutoExpandPanels` is toggled via `Control_Theme`.
2.  **Logic**: Update each control to respect this setting during Search, Save, and Reset operations.
3.  **Fixes**: Correct button directions for Advanced controls.


## Overview
This document outlines the instructions for implementing a new user setting "Auto Expand Panels" and fixing button direction issues. This setting will control the automatic expansion and collapse of side panels (Quick Buttons, Search Panels, etc.) during key operations (Search, Save, Reset).

## 1. Database & DAO Layer

### 1.1 Add Setting to Dao_User
- **File**: `Data/Dao_User.cs`
- **Task**: Add `GetAutoExpandPanelsAsync` and `SetAutoExpandPanelsAsync` methods.
- **Storage**: Use the existing `SettingsJson` column via `GetSettingsJsonInternalAsync` and `SetUserSettingInternalAsync`.
- **Key**: `AutoExpandPanels`
- **Default**: `true` (or `false` if preferred, but `true` seems to be the requested behavior).

## 2. Settings UI (`Control_Theme`)

### 2.1 Add Checkbox
- **File**: `Controls/SettingsForm/Control_Theme.cs` & `.Designer.cs`
- **UI**: Add a new CheckBox `Control_Themes_CheckBox_AutoExpandPanels` below the existing "Enable Animations" checkbox.
- **Label**: "Auto Expand/Collapse Panels"
- **Logic**:
    - Load the setting in `LoadThemeSettingsAsync`.
    - Save the setting in `SaveButton_Click`.

## 3. Logic Implementation

### 3.1 Global Access
- **File**: `Core/Model_Application_Variables.cs` (or similar)
- **Task**: Add a property `AutoExpandPanels` to cache this setting on startup, similar to `ThemeEnabled`.
- **Update**: Ensure it's updated when changed in Settings.

### 3.2 Control_InventoryTab
- **File**: `Controls/MainForm/Control_InventoryTab.cs`
- **Target**: Quick Buttons Panel (`MainForm_SplitContainer_Middle.Panel2`)
- **Logic**:
    - **Save Success**: If `AutoExpandPanels` is true, **EXPAND** the Quick Buttons panel (show history).
    - **Reset**: If `AutoExpandPanels` is true, **COLLAPSE** the Quick Buttons panel (focus on input).

### 3.3 Control_RemoveTab & Control_TransferTab
- **Files**: `Controls/MainForm/Control_RemoveTab.cs`, `Controls/MainForm/Control_TransferTab.cs`
- **Target**: Quick Buttons Panel & Search Controls Panel (if applicable).
- **Logic**: Same as InventoryTab.

### 3.4 TransactionsForm
- **File**: `Forms/Transactions/Transactions.cs`
- **Target**: Search Panel (`Transactions_Panel_Search`) & Details Panel (Information Panel).
- **Logic**:
    - **Search Success**: If `AutoExpandPanels` is true, **COLLAPSE** the Search Panel (to show more grid).
    - **Reset**: If `AutoExpandPanels` is true, **EXPAND** the Search Panel.

### 3.5 SettingsForm
- **File**: `Forms/Settings/SettingsForm.cs`
- **Target**: TreeView Panel (`SplitContainer.Panel1`).
- **Logic**:
    - **Node Selected**: If `AutoExpandPanels` is true, maybe **COLLAPSE** the TreeView (if on small screen)? *Clarification needed, but for now, ensure the toggle button works correctly.*

### 3.6 AdvancedInventory & AdvancedRemove
- **Files**: `Controls/MainForm/Control_AdvancedInventory.cs`, `Controls/MainForm/Control_AdvancedRemove.cs`
- **Target**: Quick Buttons Panel & Search Controls Panel (Input Panel).
- **Logic**:
    - **Save/Send Success**: If `AutoExpandPanels` is true, **EXPAND** Quick Buttons.
    - **Reset**: If `AutoExpandPanels` is true, **COLLAPSE** Quick Buttons.
- **Fix Button Directions**:
    - The toggle buttons for the Input Panel (Left side) are backwards.
    - **Current**: `collapsed=true` -> ArrowLeft (🡰).
    - **Fix**: For Left-side panels, `collapsed=true` should show ArrowRight (🡲).
    - **Implementation**: In `UpdateSingleInputArrow` / `UpdateMultiInputArrow`, invert the `collapsed` boolean passed to `ApplyHorizontalArrow` OR use `ApplyAnimationState` with explicit glyphs.

## 4. Execution Plan
1.  **DAO**: Update `Dao_User.cs`.
2.  **Model**: Update `Model_Application_Variables.cs`.
3.  **Settings UI**: Update `Control_Theme.cs`.
4.  **Fix Directions**: Fix `Control_AdvancedInventory.cs` and `Control_AdvancedRemove.cs`.
5.  **Implement Logic**: Add the auto-expand logic to all listed controls.
