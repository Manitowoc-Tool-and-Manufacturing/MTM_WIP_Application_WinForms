# Part ID Settings Controls Reference

This document captures the current behavior and requirements of the existing `Control_Add_PartID`, `Control_Edit_PartID`, and `Control_Remove_PartID` controls before they are merged into a single consolidated control.

## Shared Concepts
- **Common Dependencies**
  - `Dao_Part` for CRUD operations (`CreatePartAsync`, `UpdatePartAsync`, `DeletePartAsync`, `PartExistsAsync`, `GetPartByNumberAsync`).
  - `Model_Application_Variables` for current user, ReloadColorCodePartsAsync, user privilege checks handled upstream in `SettingsForm`.
  - `Helper_UI_ComboBoxes` and `Helper_SuggestionTextBox` for populating dropdowns and suggestion inputs.
  - `Helper_UI_SuggestionBoxes` indirectly via helper methods (cached part/item type lists).
  - `Service_ErrorHandler`, `LoggingUtility`, `Service_DebugTracer` (edit control) for structured logging/error handling.
- **UI Patterns**
  - Add/Remove controls currently rely on `GroupBox`, `ComboBox`, `TextBox`, `Label`, `Button`.
  - Edit control already uses `SuggestionTextBoxWithLabel` for part and item type selection and card-like layout.
  - All controls surface `Requires Color Code & Work Order` boolean via checkbox.
- **Events Raised**
  - `Control_Add_PartID.PartAdded`
  - `Control_Edit_PartID.PartUpdated`
  - `Control_Remove_PartID.PartRemoved`

## Control_Add_PartID Behavior Summary
- **Fields/UI Elements**
  - `itemNumberTextBox`: plain input for part number (required).
  - `Control_Add_PartID_ComboBox_ItemType`: dropdown for item types (required, default to "WIP" if available).
  - `issuedByValueLabel`: displays current user on load.
  - `Control_Add_PartID_CheckBox_RequiresColorCode`: toggles color-code requirement.
  - Save/Cancel buttons.
- **Initialization**
  - Constructor logs via `Service_DebugTracer`, calls `LoadPartTypes()`.
  - `LoadPartTypes` populates combo using `Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync`, selects "WIP".
  - `OnLoad` sets issued-by label to current user.
- **Validation Flow (SaveButton_Click)**
  1. Item number required (MessageBox warnings currently).
  2. Item type selection required.
  3. `Dao_Part.PartExistsAsync` ensures uniqueness.
  4. `AddPartAsync` calls `Dao_Part.CreatePartAsync` with blank description/customer.
  5. If `RequiresColorCode` true, call `Model_Application_Variables.ReloadColorCodePartsAsync`.
  6. Clear form and fire `PartAdded` event.
- **Cancel** resets fields (`ClearForm`), unchecks checkbox, reselects WIP, focuses item number.

## Control_Edit_PartID Behavior Summary
- **Fields/UI Elements**
  - `Control_Edit_PartID_SuggestionBox_Part`: selects existing part, F4 enabled.
  - `itemNumberTextBox`: new part number (required for rename).
  - `Control_Edit_PartID_SuggestionBox_ItemType`: stores new item type.
  - `issuedByValueLabel`, `Control_Edit_PartID_CheckBox_RequiresColorCode`.
  - Save/Reset buttons in right-aligned flow panel.
- **Initialization**
  - Constructor configures suggestion boxes via `Helper_SuggestionTextBox.ConfigureForPartNumbers/ItemTypes`.
  - `OnLoad` sets issued-by label.
- **Data Loading**
  - `LoadSelectedPartAsync(string selectedPart)` fetches part via `Dao_Part.GetPartByNumberAsync`, updates `_currentPart`, `_originalRequiresColorCode`, populates UI, enables form.
  - `LoadPartData` extracts columns: `PartID`, `ItemType`, `IssuedBy`, `RequiresColorCode`.
- **Validation & Save**
  1. Ensure `_currentPart` not null.
  2. Ensure item type selected (warning).
  3. `ValidatePartNumberNotDuplicateAsync` compares new part number vs existing and calls `Dao_Part.PartExistsAsync` when changed.
  4. `UpdatePartAsync` calls `Dao_Part.UpdatePartAsync(id, partId, description, customer, issuedBy, type, requiresColorCode)`.
  5. If color code flag changed, reload cache.
  6. On success, show info message, clear form, fire `PartUpdated`.
- **Reset** clears suggestions, disables form, focuses part selector.
- **Error Handling** uses `Service_ErrorHandler` extensively; `Service_DebugTracer` used for performance traces.

## Control_Remove_PartID Behavior Summary
- **Fields/UI Elements**
  - `partsComboBox`: standard combo with autocomplete for part selection (populated via `Helper_UI_ComboBoxes.FillPartComboBoxesAsync`).
  - `detailsGroupBox`: displays part metadata (item number, customer, description, type, issued by).
  - `warningLabel`, `removeButton`, `cancelButton`.
- **Initialization**
  - `OnLoad` sets issued-by label to current user, calls `LoadParts()` to populate combo.
- **Selection Flow**
  - On part change, fetch via `Dao_Part.GetPartByNumberAsync`.
  - If data returned, fill labels, enable `removeButton`, show details group.
- **Removal Flow**
  1. Ensure `_currentPart` present.
  2. Confirm via MessageBox warning.
  3. `Dao_Part.DeletePartAsync(itemNumber)` deletes record.
  4. On success, show info message, reload combos, clear/disable UI, fire `PartRemoved`.
- **Cancel** resets selection and disables form.

## Observations for Consolidation
- Add & Remove controls still rely on legacy MessageBox error handling and standard WinForms inputs; future control must use `Service_ErrorHandler` and `SuggestionTextBoxWithLabel` to comply with instructions.
- All three controls depend on part + item type data sources and share repeated UI patterns (labels, combos, buttons).
- New merged control must:
  - Provide three operations (Add, Edit, Remove) in a single UI following card/tile styling from `Control_SettingsHome` / `Control_Database`.
  - Use `SuggestionTextBoxWithLabel` for all inputs (part selection, new part id, item type, etc.).
  - Centralize color code checkbox handling and caching triggers.
  - Replace MessageBox usage with `Service_ErrorHandler` friendly notifications + confirmation dialogs as per repo standards.
  - Emit separate events or a unified change event to inform parent forms when data changes.
