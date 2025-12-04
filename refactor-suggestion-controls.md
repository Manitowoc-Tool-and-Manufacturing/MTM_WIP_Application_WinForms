# Suggestion Control Refactoring Checklist

This document tracks the refactoring of `SuggestionTextBox` and `SuggestionTextBoxWithLabel` controls to use the new enum-based configuration system.

## Refactoring Goals
1.  Replace hardcoded `Helper_SuggestionTextBox.ConfigureFor...` calls with Designer properties.
2.  Set `SuggestionDataSource` property in Designer (or code-behind if dynamic).
3.  Set `SelectionAction` and `NoMatchAction` properties in Designer.
4.  Remove manual event wiring if handled by the control's internal logic (where applicable).
5.  Ensure all `ComboBox` and `TextBox` controls that should be suggestion boxes are converted.

## Checklist

- [ ] Update `Enum_SuggestionDataSource.cs` with missing types (ItemType, Building, Warehouse, Infor types).
- [ ] Refactor `Control_ReceivingAnalytics.cs`
- [ ] Refactor `Control_VisualInventory.cs`
- [ ] Refactor `Control_InventoryAudit.cs`
- [ ] Refactor `Control_PartIDManagement.cs`
- [ ] Refactor `Control_OperationManagement.cs`
- [ ] Refactor `Control_LocationManagement.cs`
- [ ] Refactor `TransactionSearchControl.cs` (Check Designer for missing properties)

## Event Handler Refactoring

Review the following event handlers. If the handler manually moves focus, set `SelectionAction = None` to avoid conflict. If the handler only moves focus, remove it and use `SelectionAction = MoveFocusToNextControl`.

| File | Line | Control Name | Event | Action |
| :--- | :--- | :--- | :--- | :--- |
| `Control_ReceivingAnalytics.cs` | 168 | `Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber` | `SuggestionSelected` | Review logic. Ensure `SelectionAction` is appropriate. |
| `Control_ReceivingAnalytics.cs` | 173 | `Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber` | `KeyDown` (Enter) | Keep if triggering search. |
| `Control_InventoryAudit.cs` | 80 | `_txtSearchBy` | `SuggestionSelected` | Review `OnSearchBySelected`. |
| `Control_InventoryAudit.cs` | 101 | `_txtLifecyclePart` | `KeyDown` (Enter) | Keep if triggering search. |
| `Control_PartIDManagement.cs` | 176 | `Control_PartIDManagement_Suggestion_EditSelectPart` | `SuggestionSelected` | Handler calls `LoadEditPartAsync` which sets focus. Set `SelectionAction = None`. |
| `Control_PartIDManagement.cs` | 182 | `Control_PartIDManagement_Suggestion_RemoveSelectPart` | `SuggestionSelected` | Handler calls `LoadRemovePartAsync`. Set `SelectionAction = None`. |
| `Control_OperationManagement.cs` | 163 | `Control_OperationManagement_Suggestion_EditSelectOperation` | `SuggestionSelected` | Handler likely loads operation. Set `SelectionAction = None`. |
| `Control_OperationManagement.cs` | 169 | `Control_OperationManagement_Suggestion_RemoveSelectOperation` | `SuggestionSelected` | Handler likely loads operation. Set `SelectionAction = None`. |
| `Control_LocationManagement.cs` | 176 | `Control_LocationManagement_Suggestion_EditSelectLocation` | `SuggestionSelected` | Handler likely loads location. Set `SelectionAction = None`. |
| `Control_LocationManagement.cs` | 182 | `Control_LocationManagement_Suggestion_RemoveSelectLocation` | `SuggestionSelected` | Handler likely loads location. Set `SelectionAction = None`. |
| `Control_ItemTypeManagement.cs` | 168 | `Control_ItemTypeManagement_Suggestion_EditSelectItemType` | `SuggestionSelected` | Handler likely loads item type. Set `SelectionAction = None`. |
| `Control_ItemTypeManagement.cs` | 174 | `Control_ItemTypeManagement_Suggestion_RemoveSelectItemType` | `SuggestionSelected` | Handler likely loads item type. Set `SelectionAction = None`. |

## Implementation Table

| File | Line | Control Name | Current Implementation | Required Action |
| :--- | :--- | :--- | :--- | :--- |
| `Control_ReceivingAnalytics.cs` | 223 | `Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber` | `Helper_SuggestionTextBox.ConfigureForPartNumbers` | Set `SuggestionDataSource = MTM_PartNumber` |
| `Control_VisualInventory.cs` | 54 | `Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber` | `Helper_SuggestionTextBox.ConfigureForPartNumbers` | Set `SuggestionDataSource = MTM_PartNumber` |
| `Control_VisualInventory.cs` | 61 | `Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse` | `Helper_SuggestionTextBox.ConfigureForWarehouses` | Set `SuggestionDataSource = MTM_Warehouse` |
| `Control_VisualInventory.cs` | 73 | `Control_VisualInventory_SuggestionTextBoxWithLabel_Location` | `Helper_SuggestionTextBox.ConfigureForLocations` | Set `SuggestionDataSource = MTM_Location` |
| `Control_InventoryAudit.cs` | 59 | `_txtLifecyclePart` | `Helper_SuggestionTextBox.ConfigureForPartNumbers` | Set `SuggestionDataSource = MTM_PartNumber` |
| `Control_InventoryAudit.cs` | 127 | `_txtLifecyclePart` | `Helper_SuggestionTextBox.ConfigureForUsers` | Set `SuggestionDataSource = MTM_User` |
| `Control_InventoryAudit.cs` | 132 | `_txtLifecyclePart` | `Helper_SuggestionTextBox.ConfigureForWorkOrders` | Set `SuggestionDataSource = Infor_WorkOrder` |
| `Control_InventoryAudit.cs` | 137 | `_txtLifecyclePart` | `Helper_SuggestionTextBox.ConfigureForCustomerOrders` | Set `SuggestionDataSource = Infor_CustomerOrder` |
| `Control_InventoryAudit.cs` | 142 | `_txtLifecyclePart` | `Helper_SuggestionTextBox.ConfigureForPurchaseOrders` | Set `SuggestionDataSource = Infor_PurchaseOrder` |
| `Control_InventoryAudit.cs` | 148 | `_txtLifecyclePart` | `Helper_SuggestionTextBox.ConfigureForPartNumbers` | Set `SuggestionDataSource = MTM_PartNumber` |
| `Control_PartIDManagement.cs` | 145 | `Control_PartIDManagement_Suggestion_AddItemType` | `Helper_SuggestionTextBox.ConfigureForItemTypes` | Set `SuggestionDataSource = MTM_ItemType` |
| `Control_PartIDManagement.cs` | 149 | `Control_PartIDManagement_Suggestion_AddPartNumber` | `Helper_SuggestionTextBox.ConfigureForPartNumbers` | Set `SuggestionDataSource = MTM_PartNumber` |
| `Control_PartIDManagement.cs` | 163 | `Control_PartIDManagement_Suggestion_EditItemType` | `Helper_SuggestionTextBox.ConfigureForItemTypes` | Set `SuggestionDataSource = MTM_ItemType` |
| `Control_PartIDManagement.cs` | 167 | `Control_PartIDManagement_Suggestion_EditSelectPart` | `Helper_SuggestionTextBox.ConfigureForPartNumbers` | Set `SuggestionDataSource = MTM_PartNumber` |
| `Control_OperationManagement.cs` | 150 | `Control_OperationManagement_TextBox_AddOperation` | `Helper_SuggestionTextBox.ConfigureForOperations` | Set `SuggestionDataSource = MTM_Operation` |
| `Control_OperationManagement.cs` | 154 | `Control_OperationManagement_Suggestion_EditSelectOperation` | `Helper_SuggestionTextBox.ConfigureForOperations` | Set `SuggestionDataSource = MTM_Operation` |
| `Control_LocationManagement.cs` | 159 | `Control_LocationManagement_TextBox_AddLocation` | `Helper_SuggestionTextBox.ConfigureForLocations` | Set `SuggestionDataSource = MTM_Location` |
| `Control_LocationManagement.cs` | 163 | `Control_LocationManagement_Suggestion_EditSelectLocation` | `Helper_SuggestionTextBox.ConfigureForLocations` | Set `SuggestionDataSource = MTM_Location` |
| `Control_LocationManagement.cs` | 167 | `Control_LocationManagement_ComboBox_AddBuilding` | `Helper_SuggestionTextBox.ConfigureForBuildings` | Set `SuggestionDataSource = MTM_Building` |
| `Control_LocationManagement.cs` | 168 | `Control_LocationManagement_ComboBox_EditBuilding` | `Helper_SuggestionTextBox.ConfigureForBuildings` | Set `SuggestionDataSource = MTM_Building` |
