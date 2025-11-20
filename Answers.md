# Task List

## Add Part Number (Control_PartIDManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Default `Control_PartIDManagement_Suggestion_AddItemType.Text` to "WIP" on form load |

## Edit Part Number (Control_PartIDManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Change `Control_PartIDManagement_Suggestion_EditNewPartNumber.LabelText` from "New Part Number" to "New Part Number:" |
| ☐ | Set `Control_PartIDManagement_Suggestion_EditNewPartNumber.Visible = false` and `Control_PartIDManagement_Suggestion_EditItemType.Visible = false` in `SetEditSectionEnabled(false)` |
| ☐ | Set `Control_PartIDManagement_Suggestion_EditNewPartNumber.Visible = true` and `Control_PartIDManagement_Suggestion_EditItemType.Visible = true` in `SetEditSectionEnabled(true)` |

## Remove Part Number (Control_PartIDManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Remove `Control_PartIDManagement_Label_RemoveCustomer`, `Control_PartIDManagement_Label_RemoveCustomerValue`, `Control_PartIDManagement_Label_RemoveDescription`, `Control_PartIDManagement_Label_RemoveDescriptionValue` from `Control_PartIDManagement_TableLayout_RemoveDetails` |
| ☐ | Change `Control_PartIDManagement_TableLayout_RemoveDetails` from 5 rows to 3 rows (remove rows 2 and 3) |
| ☐ | Remove Customer/Description field declarations from Designer.cs Fields region |
| ☐ | Remove Customer/Description assignments in `LoadRemovePartAsync()` method (lines setting `Control_PartIDManagement_Label_RemoveCustomerValue.Text` and `Control_PartIDManagement_Label_RemoveDescriptionValue.Text`) |

## Edit Operation (Control_OperationManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Change `Control_OperationManagement_TextBox_EditNewOperation.LabelText` from "New Operation" to "New Operation:" |
| ☐ | Set `Control_OperationManagement_TextBox_EditNewOperation.Visible = false` in `SetEditSectionEnabled(false)` |
| ☐ | Set `Control_OperationManagement_TextBox_EditNewOperation.Visible = true` in `SetEditSectionEnabled(true)` |

## Remove Operation (Control_OperationManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Set `Control_OperationManagement_TableLayout_RemoveDetails.Visible = false` in `ClearRemoveSection()` |
| ☐ | Set `Control_OperationManagement_TableLayout_RemoveDetails.Visible = true` in `SetRemoveSectionEnabled(true)` |

## Edit Location (Control_LocationManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Replace `Control_LocationManagement_TextBox_EditNewLocation` (TextBox) with `SuggestionTextBoxWithLabel` control |
| ☐ | Replace `Control_LocationManagement_ComboBox_EditBuilding` (ComboBox) with `SuggestionTextBoxWithLabel` control |
| ☐ | Add `GetCachedBuildingsAsync()` method to `Helper_SuggestionTextBox` returning DataTable with rows: "Expo", "Vits", "KK Warehouse", "Other" |
| ☐ | Configure new building SuggestionBox with `Helper_SuggestionTextBox.ConfigureForBuildings()` in `ConfigureInputs()` |
| ☐ | In `Control_LocationManagement_TableLayout_EditContent`, remove column 2, set column 1 width to 100% |
| ☐ | Change `ColumnSpan` from 2 to 1 for all 3 input controls in Edit card TableLayoutPanel |
| ☐ | Set `Padding = new Padding(3, 3, 3, 3)` for both new SuggestionTextBoxWithLabel controls |

## Remove Location (Control_LocationManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Set `Control_LocationManagement_TableLayout_RemoveDetails.Visible = false` in `ClearRemoveSection()` |
| ☐ | Set `Control_LocationManagement_TableLayout_RemoveDetails.Visible = true` in `SetRemoveSectionEnabled(true)` |

## Add Item Type (Control_ItemTypeManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Change `Control_ItemTypeManagement_TextBox_AddItemType.LabelText` from "New Item Type" to "New Item Type:" in `InitializeControlText()` |

## Remove Item Type (Control_ItemTypeManagement.cs/Designer.cs)

| Status | Task |
|--------|------|
| ☐ | Set `Control_ItemTypeManagement_TableLayout_RemoveDetails.Visible = false` in `ClearRemoveSection()` |
| ☐ | Set `Control_ItemTypeManagement_TableLayout_RemoveDetails.Visible = true` in `SetRemoveSectionEnabled(true)` |

## All Removal Pages (PartID, Operation, Location, ItemType)

| Status | Task |
|--------|------|
| ☐ | In each management control's `ApplyTheme()` override, set `Control_*Management_Label_RemoveWarning.ForeColor = theme.ErrorColor` |
| ☐ | In each management control's `ApplyTheme()` override, set `Control_*Management_Button_RemoveConfirm.ForeColor = theme.ErrorColor` (background unchanged) |
| ☐ | Add `ApplyTheme()` override to: `Control_PartIDManagement`, `Control_OperationManagement`, `Control_LocationManagement`, `Control_ItemTypeManagement` |

