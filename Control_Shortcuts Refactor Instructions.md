# Control_Shortcuts Refactor Instructions

## Overview
Refactor `Control_Shortcuts` into a modern card-based UI, ensure all shortcuts flow through `Service_Shortcut`, and align default shortcuts with the codebase.

## 1. Preparation & Cleanup

### 1.1 Remove Legacy UI
- Delete the existing DataGridView UI and logic from `Control_Shortcuts.cs` and `.Designer.cs`.
- Keep integration points in `SettingsForm` intact.

### 1.2 Service Alignment
- Use only `Services/Service_Shortcut.cs` for loading, updating, and resetting shortcuts.
- Review `Service_Shortcut.cs` before making changes.
- Remember: data comes from the database or `Resources/default_shortcuts.json` when no overrides exist.

## 2. Shortcut Inventory & Defaults

### 2.1 Single-Pass Audit (Review + Notes)
Audit each file once, recording the current shortcut, usage context, and whether it already calls `Service_Shortcut`. Do **not** modify during this pass.
1. `Controls/MainForm/Control_InventoryTab.cs`
2. `Controls/MainForm/Control_RemoveTab.cs`
3. `Controls/MainForm/Control_TransferTab.cs`
4. `Controls/MainForm/Control_AdvancedInventory.cs` (include sub-controls if any)
5. `Controls/MainForm/Control_AdvancedRemove.cs`
6. `Controls/MainForm/Control_QuickButtons.cs`
    - Only allow ten hotkeys (default: `Alt+1` … `Alt+0`). Remove all others. Note missing ones.
7. `Forms/Transactions/TransactionsForm.cs` (or `Transactions.cs`)
8. `Forms/MainForm.cs` (collect global shortcuts not covered above)

### 2.2 Update `default_shortcuts.json`
- Compare audit notes against `Resources/default_shortcuts.json`.
- Add missing shortcuts using the standard naming convention: `<Feature>_<Action>` (e.g., `Inventory_Save`, `QuickButton_01`, `Remove_Confirm`). Always use PascalCase for feature and action names, and number quick buttons as `QuickButton_01` through `QuickButton_10`. See existing entries in `default_shortcuts.json` for further examples.
- Add missing shortcuts with clear, descriptive names.
- Keep ordering logical (group by feature/category).

### 2.3 Reset User Overrides (Testing Prep)
1. Once `Resources/default_shortcuts.json` is updated, connect to the production database and invoke the stored procedure that clears overrides for a single user:
    ```
    & "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms -e "CALL md_shortcuts_ResetUserOverrides('JOHNK');"
    ```
2. Repeat the call against the test database:
    ```
    & "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "CALL md_shortcuts_ResetUserOverrides('JOHNK');"
    ```
3. Verify the reset by querying `Service_Shortcut.GetAllShortcutsAsync()` (or the DAO wrapper) and confirming no `JOHNK`-scoped records remain; defaults should reload on the next application start.

## 3. Refactor Target Files (Single Pass per File)
Using the audit results, edit each file once to:

- Replace every direct `Keys` literal or enum comparison with `_shortcutService.GetShortcutKey("<ShortcutName>")` and, when displaying the shortcut, call `_shortcutService.GetShortcutDisplay("<ShortcutName>")`. This aligns UI logic with `Service_Shortcut`’s in-memory cache and keeps code consistent with `Dao_Shortcuts`.
- After calling `UpdateShortcutAsync` or `ResetToDefaultsAsync`, re-query `Service_Shortcut` for the current values (e.g., call `GetShortcutKey`, `GetShortcutDisplay`, or `GetAllShortcuts`) so controls pick up the refreshed cache immediately—`Service_Shortcut` does not raise events.
- Enforce QuickButton exclusivity by comparing any requested keys against the ten QuickButton assignments (`QuickButton01` … `QuickButton10`). Reject or remap duplicates before saving so no `MainForm` child control reuses those combinations. `Control_QuickButtons.cs` currently does not register these shortcuts through `Service_Shortcut`, so add the registration logic for all ten buttons as part of this step.
- Before moving on to the next file, confirm that no duplicate keystrokes remain because of leftover direct key handling.

## 4. New `Control_Shortcuts` UI

### 4.1 Layout
- Build the surface as a single-column, vertically scrollable stack composed of `Panel` and `TableLayoutPanel` controls, mirroring `Control_SettingsHome` with one collapsible card per row.
- Let the structure stretch across the available width while enabling vertical scrolling for overflow content.
- Present a prominent title header followed immediately by a concise descriptive subtitle.
- Keep a “Back to Home” button anchored to `Settings_Home` at the bottom: configure the rows as Auto, 100%, Auto so the 2nd to last row absorbs excess space and remains empty, leaving the button fixed in the final auto row (more than 3 rows will be required).

### 4.2 Card Structure
- Implement a new `Control_SettingsCollapsibleCard` (inherits `ThemedUserControl`) modeled on `Control_SettingsCategoryCard`, adding expand/collapse support (toggle button, smooth height animation, state persistence).
- Expose the collapsible control via DI so `Control_Shortcuts` and other settings panels can instantiate it per shortcut category (e.g., “Inventory Transaction Tab”, “General Application”).
- For each card instance, place shortcut rows inside the collapsible region; ensure collapsed content is removed from the tab order and resizes correctly when expanded.

### 4.3 Header Styling (Consistency)
Apply the `Control_SettingsCategoryCard` header pattern (30–40 px tall header, 1 px border, themed background, bold label, optional icon, theme-aware) to each card in:
1. `Control_Shortcuts` (new cards)
2. `Control_About.cs`
3. `Control_Add_User.cs`
4. `Control_Database.cs`
5. `Control_Developer_ParameterPrefixMaintenance.cs`
6. `Control_Edit_User.cs`
7. `Control_ItemTypeManagement.cs`
8. `Control_LocationManagement.cs`
9. `Control_OperationManagement.cs`
10. `Control_PartIDManagement.cs`
11. `Control_Remove_User.cs`
12. `Control_Theme.cs`
13. `Control_User_Management.cs`

### 4.4 Shortcut Interaction
- Within each card, list shortcuts with:
  - Button (max 4-word label, opens modal dialog to capture new key).
  - Label showing the current key combination.
- Dialog must validate input before saving via `Service_Shortcut`.

### 4.5 Validation Rules
- No duplicate keys within a single card.
- Global rule: shortcuts assigned to `Control_QuickButtons` remain unique across all `MainForm` child controls.
- Provide clear error messages on conflicts.

## 5. Execution Sequence (Minimize Rework)
1. Audit files (Section 2.1) and record findings.
2. Update `Resources/default_shortcuts.json` (Section 2.2).
3. Reset user `JOHNK` shortcuts in both databases (Section 2.3).
4. Refactor audited files to use `Service_Shortcut` (Section 3).
5. Build the new `Control_Shortcuts` UI and interaction logic (Section 4.1, 4.2, 4.4, 4.5).
6. Apply unified header styling across the listed settings controls (Section 4.3).
7. Verify default loading, user updates, and conflict handling end-to-end.

