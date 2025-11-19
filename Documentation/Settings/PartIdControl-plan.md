# Part ID Management Control Merge Plan

Checklist-driven plan for replacing `Control_Add_PartID`, `Control_Edit_PartID`, and `Control_Remove_PartID` with a single consolidated control, per `ui-development.workflow.md`, `service-refactoring.workflow.md`, and `suggestion-control-implementation.md` requirements.

## Preparation
- [ ] Review `Documentation/Settings/PartIdControls-reference.md` for legacy behaviors and required data points.
- [ ] Confirm UI constraints from `SettingsForm` (available panel space, dependency injection, event contracts).
- [ ] Ensure SuggestionTextBox data sources exist for all needed entities (parts, item types, customers if required).

## Legacy File Backup
- [ ] Rename each legacy code-behind to `.backup` (`Control_Add_PartID.cs`, `Control_Edit_PartID.cs`, `Control_Remove_PartID.cs`).
- [ ] Commit/track matching designer files separately (designer files removed entirely after new control created).

## New Control Design
- [ ] Define new control name (e.g., `Control_PartIDManagement`) and folder placement under `Controls/SettingsForm/`.
- [ ] Layout using TableLayout/FlowLayout card pattern from `Control_Database.Designer.cs` and `Control_SettingsHome.Designer.cs` (no `GroupBox`).
- [ ] Create three action cards or sections (Add, Edit, Remove) with consistent card styling.
- [ ] Use `SuggestionTextBoxWithLabel` for every textual input/selection (part number, item type, etc.).
- [ ] Provide dedicated action buttons (Save/Add, Update, Remove, Reset/Cancel) in right-aligned `FlowLayoutPanel`.

## Code-Behind Implementation
- [ ] Implement region structure and XML documentation per repo standards.
- [ ] Consolidate DAO interactions:
  - Add: create new part via `Dao_Part.CreatePartAsync` and refresh caches if `RequiresColorCode` set.
  - Edit: update part via `Dao_Part.UpdatePartAsync` with duplicate check.
  - Remove: delete part via `Dao_Part.DeletePartAsync` with confirmation dialog (use `Service_ErrorHandler` + custom confirmation pattern).
- [ ] Replace all `MessageBox.Show` usages with `Service_ErrorHandler` and logging patterns.
- [ ] Expose unified events for parent form notifications (e.g., `PartListChanged`).
- [ ] Ensure async/await usage for DAO calls and suggestion loading.

## SettingsForm Integration
- [ ] Remove existing `Part Numbers` child nodes referencing Add/Edit/Remove controls.
- [ ] Point root `Part Numbers` node to the new unified control.
- [ ] Update `_settingsPanels` dictionary and initialization logic accordingly.

## Verification
- [ ] Build project to ensure no compilation errors.
- [ ] Runtime smoke test: navigate to Settings → Part Numbers; validate Add/Edit/Remove flows.
- [ ] Confirm theming and card styling align with `ui-structure.instructions.md`.
