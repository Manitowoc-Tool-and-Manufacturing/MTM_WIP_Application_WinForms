# Implementation Tasks: Refactor Shortcuts UI

**Feature Branch**: `001-refactor-shortcuts-ui`
**Spec**: [spec.md](spec.md)
**Plan**: [plan.md](plan.md)

## Implementation Strategy

This feature refactors the shortcut management system to use a centralized service and a modern card-based UI. The execution follows a strict sequence:
1.  **Setup**: Define all shortcuts in JSON and create reusable UI components.
2.  **Foundation**: Refactor all existing controls to use `Service_Shortcut` instead of hardcoded keys. This ensures the backend is ready before the UI is swapped.
3.  **UI Implementation**: Build the new `Control_Shortcuts` using the new components.
4.  **Modernization**: Apply the new header styling to other settings controls for consistency.

## Dependencies

- **US1 (View/Manage)** depends on **Foundation** (Service updates & Consumer refactors).
- **US2 (QuickButtons)** is integrated into **Foundation** (Service validation) and **US1** (UI validation).
- **US3 (Modernization)** depends on **Setup** (CollapsibleCard component) but can run parallel to US1.

## Phase 1: Setup & Components

**Goal**: Prepare the data structures and reusable UI components.

- [x] T001 Update `Resources/default_shortcuts.json` to include all missing shortcuts (Inventory, Remove, Transfer, etc.) and QuickButtons (QuickButton_01 to QuickButton_10).
- [x] T002 Create `Control_SettingsCollapsibleCard` in `Controls/SettingsForm/Control_SettingsCollapsibleCard.cs` inheriting `ThemedUserControl` with Title, Description, Icon, and Expand/Collapse logic.
- [x] T003 Create `Form_ShortcutEdit` in `Forms/Shared/Form_ShortcutEdit.cs` as a modal dialog to capture key combinations and return them.

## Phase 2: Foundational (Service & Consumers)

**Goal**: Centralize all shortcut logic in `Service_Shortcut` and remove hardcoded keys from the codebase.

- [x] T004 Update `Services/Input/Service_Shortcut.cs` to implement `IsReservedKey` (Alt+0-9), `IsDuplicate`, and `GetShortcutDisplay`.
- [x] T005 Refactor `Controls/MainForm/Control_InventoryTab.cs` to replace `Keys` enums with `_shortcutService.GetShortcutKey()`.
- [x] T006 Refactor `Controls/MainForm/Control_RemoveTab.cs` to replace `Keys` enums with `_shortcutService.GetShortcutKey()`.
- [x] T007 Refactor `Controls/MainForm/Control_TransferTab.cs` to replace `Keys` enums with `_shortcutService.GetShortcutKey()`.
- [x] T008 Refactor `Controls/MainForm/Control_AdvancedInventory.cs` to replace `Keys` enums with `_shortcutService.GetShortcutKey()`.
- [x] T009 Refactor `Controls/MainForm/Control_AdvancedRemove.cs` to replace `Keys` enums with `_shortcutService.GetShortcutKey()`.
- [x] T010 Refactor `Controls/MainForm/Control_QuickButtons.cs` to register its 10 shortcuts via `Service_Shortcut` and respect exclusivity.
- [x] T011 Refactor `Forms/Transactions/TransactionsForm.cs` to replace `Keys` enums with `_shortcutService.GetShortcutKey()`.
- [x] T012 Refactor `Forms/MainForm.cs` to replace global `Keys` enums with `_shortcutService.GetShortcutKey()`.

## Phase 3: User Story 1 - View and Manage Shortcuts

**Goal**: Replace the legacy DataGridView with the new card-based UI.
**Independent Test**: Open Settings > Shortcuts, verify categories are collapsible, and shortcuts can be changed.

- [x] T013 [US1] Remove legacy DataGridView and logic from `Controls/SettingsForm/Control_Shortcuts.cs` and `.Designer.cs`.
- [x] T014 [US1] Implement new layout in `Controls/SettingsForm/Control_Shortcuts.cs` using a scrollable `FlowLayoutPanel` or `TableLayoutPanel`.
- [x] T015 [US1] Implement logic in `Controls/SettingsForm/Control_Shortcuts.cs` to populate `Control_SettingsCollapsibleCard` instances from `Service_Shortcut` data.
- [x] T016 [US1] Implement "Change" button logic in `Control_Shortcuts.cs` using `Form_ShortcutEdit` and `Service_Shortcut.UpdateShortcutAsync`.
- [x] T017 [US1] Implement "Reset" logic in `Control_Shortcuts.cs` using `Service_Shortcut.ResetToDefaultsAsync`.

## Phase 4: User Story 2 - QuickButton Exclusivity

**Goal**: Ensure QuickButton keys are reserved and cannot be assigned to other functions.
**Independent Test**: Try to assign `Alt+1` to "Inventory Save" and verify it is rejected.

- [x] T018 [US2] Verify `Service_Shortcut.UpdateShortcutAsync` rejects reserved keys and returns a clear error message.
- [x] T019 [US2] Update `Control_Shortcuts.cs` to display validation errors from the service when a reserved key is selected.

## Phase 5: User Story 3 - UI Modernization

**Goal**: Apply consistent header styling to all settings controls.
**Independent Test**: Check all settings tabs for consistent header look and feel.

- [ ] T020 [P] [US3] Apply header style to `Controls/SettingsForm/Control_About.cs`.
- [ ] T021 [P] [US3] Apply header style to `Controls/SettingsForm/Control_Add_User.cs`.
- [ ] T022 [P] [US3] Apply header style to `Controls/SettingsForm/Control_Database.cs`.
- [ ] T023 [P] [US3] Apply header style to `Controls/SettingsForm/Control_Developer_ParameterPrefixMaintenance.cs`.
- [ ] T024 [P] [US3] Apply header style to `Controls/SettingsForm/Control_Edit_User.cs`.
- [ ] T025 [P] [US3] Apply header style to `Controls/SettingsForm/Control_ItemTypeManagement.cs`.
- [ ] T026 [P] [US3] Apply header style to `Controls/SettingsForm/Control_LocationManagement.cs`.
- [ ] T027 [P] [US3] Apply header style to `Controls/SettingsForm/Control_OperationManagement.cs`.
- [ ] T028 [P] [US3] Apply header style to `Controls/SettingsForm/Control_PartIDManagement.cs`.
- [ ] T029 [P] [US3] Apply header style to `Controls/SettingsForm/Control_Remove_User.cs`.
- [ ] T030 [P] [US3] Apply header style to `Controls/SettingsForm/Control_Theme.cs`.
- [ ] T031 [P] [US3] Apply header style to `Controls/SettingsForm/Control_User_Management.cs`.

## Phase 6: Polish & Cleanup

**Goal**: Final verification and cleanup.

- [ ] T032 Verify all shortcuts work as expected in the main application.
- [ ] T033 Verify "Back to Home" button visibility and scrolling behavior in `Control_Shortcuts`.
