# SuggestionTextBox Migration Inventory

**Generated**: November 12, 2025  
**Status**: Discovery Phase Complete (UPDATED - includes DropDownList with database data)  
**Total Migration Targets**: 22 ComboBox/DropDownList controls across 15 forms/controls

---

## Migration Reference Pattern (T050)

See `Control_InventoryTab.cs` for complete working implementation. **7-Step Process**:

1. **Replace ComboBox declarations** with SuggestionTextBox in Designer.cs
2. **Implement async data provider methods** (`GetXxxSuggestionsAsync`) returning `List<string>`
3. **Configure SuggestionTextBox properties** (DataProvider, MaxResults, EnableWildcards) in constructor/Load event
4. **Add event handlers** (SuggestionSelected, SuggestionCancelled) for validation and logging
5. **Remove old ComboBox code** (SelectedIndexChanged handlers, DataSource binding)
6. **Update UI layout** (Anchor vs Dock for vertical centering)
7. **Set PlaceholderText** in Designer for user guidance

---

## Available DAO Methods (Frequently Referenced)

```csharp
// Part Numbers
Dao_Part.GetAllPartIDsAsync() → List<string>

// Operations
Dao_Operation.GetAllOperationsAsync() → List<string>

// Locations
Dao_Location.GetAllLocationsAsync() → List<string>

// Item Types
Dao_ItemType.GetAllItemTypesAsync() → List<string>

// Users
Dao_User.GetAllUsernamesAsync() → List<string>
Dao_User.GetAllFullNamesAsync() → List<string>
```

---

## Migration Inventory by Hierarchy

### ✅ COMPLETED: MainForm → Control_InventoryTab (Reference Implementation)

| Control Name | Field Purpose | Placeholder Text | Status |
|--------------|---------------|------------------|--------|
| Control_InventoryTab_TextBox_Part | Part Number | "Enter or Select Part Number" | ✅ DONE (T024-T031) |
| Control_InventoryTab_TextBox_Operation | Operation | "Enter or Select Operation" | ✅ DONE (T032-T037) |
| Control_InventoryTab_TextBox_Location | Location | "Enter or Select Location" | ✅ DONE (Already migrated) |

**File**: `Controls\MainForm\Control_InventoryTab.Designer.cs`  
**Data Providers**: GetPartNumberSuggestionsAsync, GetOperationSuggestionsAsync, GetLocationSuggestionsAsync

---

### 🔴 PRIORITY 1: MainForm Children

#### Control_TransferTab (3 ComboBoxes)

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_TransferTab_ComboBox_Part | Part Number | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |
| 2 | Control_TransferTab_ComboBox_Operation | Operation | "Enter or Select Operation" | Dao_Operation.GetAllOperationsAsync() | MaxResults=50, EnableWildcards=true |
| 3 | Control_TransferTab_ComboBox_ToLocation | To Location | "Enter or Select Location" | Dao_Location.GetAllLocationsAsync() | MaxResults=100, EnableWildcards=true |

- [ ] **Migrate Part ComboBox to SuggestionTextBox**
- [ ] **Migrate Operation ComboBox to SuggestionTextBox**
- [ ] **Migrate ToLocation ComboBox to SuggestionTextBox**
- [ ] **Test wildcard patterns on all three fields**
- [ ] **Verify tab order and keyboard navigation**

**File**: `Controls\MainForm\Control_TransferTab.Designer.cs` + `Control_TransferTab.cs`  
**Priority**: P1 - Critical user workflow  
**Complexity**: Medium (similar to InventoryTab)

---

#### Control_RemoveTab (2 ComboBoxes)

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_RemoveTab_ComboBox_Part | Part Number | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |
| 2 | Control_RemoveTab_ComboBox_Operation | Operation | "Enter or Select Operation" | Dao_Operation.GetAllOperationsAsync() | MaxResults=50, EnableWildcards=true |

- [ ] **Migrate Part ComboBox to SuggestionTextBox**
- [ ] **Migrate Operation ComboBox to SuggestionTextBox**
- [ ] **Test consistency with Inventory/Transfer tabs**
- [ ] **Verify keyboard navigation**

**File**: `Controls\MainForm\Control_RemoveTab.Designer.cs` + `Control_RemoveTab.cs`  
**Priority**: P1 - Consistent UX across all tabs  
**Complexity**: Low (identical pattern to InventoryTab)

---

#### Control_AdvancedInventory (6 ComboBoxes across 2 tabs)

**Single Transaction Tab:**

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | AdvancedInventory_Single_ComboBox_PartID | Part Number | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |
| 2 | AdvancedInventory_Single_ComboBox_Operation | Operation | "Enter or Select Operation" | Dao_Operation.GetAllOperationsAsync() | MaxResults=50, EnableWildcards=true |
| 3 | AdvancedInventory_Single_ComboBox_Location | Location | "Enter or Select Location" | Dao_Location.GetAllLocationsAsync() | MaxResults=100, EnableWildcards=true |

**Multi-Location Tab:**

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 4 | AdvancedInventory_MultiLoc_ComboBox_PartID | Part Number | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |
| 5 | AdvancedInventory_MultiLoc_ComboBox_Operation | Operation | "Enter or Select Operation" | Dao_Operation.GetAllOperationsAsync() | MaxResults=50, EnableWildcards=true |
| 6 | AdvancedInventory_MultiLoc_ComboBox_Location | Location | "Enter or Select Location" | Dao_Location.GetAllLocationsAsync() | MaxResults=100, EnableWildcards=true |

- [ ] **Migrate Single tab Part ComboBox to SuggestionTextBox**
- [ ] **Migrate Single tab Operation ComboBox to SuggestionTextBox**
- [ ] **Migrate Single tab Location ComboBox to SuggestionTextBox**
- [ ] **Migrate Multi-Loc tab Part ComboBox to SuggestionTextBox**
- [ ] **Migrate Multi-Loc tab Operation ComboBox to SuggestionTextBox**
- [ ] **Migrate Multi-Loc tab Location ComboBox to SuggestionTextBox**
- [ ] **Test consistency between Single and Multi-Loc tabs**
- [ ] **Verify tab navigation between tabs**

**File**: `Controls\MainForm\Control_AdvancedInventory.Designer.cs` + `Control_AdvancedInventory.cs`  
**Priority**: P1 - Power user feature  
**Complexity**: High (6 controls, 2 tabs, shared data providers)

---

#### Control_AdvancedRemove (3 ComboBoxes)

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | AdvancedRemove_ComboBox_PartID | Part Number | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |
| 2 | AdvancedRemove_ComboBox_Operation | Operation | "Enter or Select Operation" | Dao_Operation.GetAllOperationsAsync() | MaxResults=50, EnableWildcards=true |
| 3 | AdvancedRemove_ComboBox_Location | Location | "Enter or Select Location" | Dao_Location.GetAllLocationsAsync() | MaxResults=100, EnableWildcards=true |

- [ ] **Migrate Part ComboBox to SuggestionTextBox**
- [ ] **Migrate Operation ComboBox to SuggestionTextBox**
- [ ] **Migrate Location ComboBox to SuggestionTextBox**
- [ ] **Test consistency with standard RemoveTab**
- [ ] **Verify keyboard navigation**

**File**: `Controls\MainForm\Control_AdvancedRemove.Designer.cs` + `Control_AdvancedRemove.cs`  
**Priority**: P2 - Advanced feature  
**Complexity**: Medium (similar to AdvancedInventory but simpler)

---

### 🟡 PRIORITY 2: Standalone Forms (1 control)

#### Form_QuickButtonEdit (1 ComboBox - PartId only)

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Form_QuickButtonEdit_ComboBox_PartId | Part Number | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |

**NOTE**: Operation ComboBox is DropDownList (not editable) - NO MIGRATION NEEDED

- [ ] **Migrate PartId ComboBox to SuggestionTextBox**
- [ ] **Test in Quick Button edit workflow**
- [ ] **Verify dialog form behavior (modal)**

**File**: `Forms\Shared\Form_QuickButtonEdit.Designer.cs` + `Form_QuickButtonEdit.cs`  
**Priority**: P2 - Quick Button management  
**Complexity**: Low (single control, simple dialog)

---

### 🟢 PRIORITY 3: SettingsForm Children

#### Control_Edit_PartID (1 ComboBox)

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Edit_PartID_ComboBox_SelectPart | Part Selector | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |

- [ ] **Migrate SelectPart ComboBox to SuggestionTextBox**
- [ ] **Test in Settings → Edit Part Number workflow**
- [ ] **Verify part selection triggers data load**

**File**: `Controls\SettingsForm\Control_Edit_PartID.Designer.cs` + `Control_Edit_PartID.cs`  
**Priority**: P3 - Admin feature  
**Complexity**: Low (single control)

---

### ⚪ OPTIONAL: Transaction Search & Settings (2 controls)

#### TransactionSearchControl (1 ComboBox - PartNumber only)

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | TransactionSearchControl_ComboBox_PartNumber | Part Number Filter | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |

**NOTE**: User, ToLocation, FromLocation ComboBoxes are DropDownList (not editable) - NO MIGRATION NEEDED

- [ ] **Migrate PartNumber ComboBox to SuggestionTextBox**
- [ ] **Test in transaction search/filter workflow**
- [ ] **Verify search performance with suggestions**

**File**: `Controls\Transactions\TransactionSearchControl.Designer.cs` + `TransactionSearchControl.cs`  
**Priority**: P3 - Optional enhancement  
**Complexity**: Low (single control)

---

#### Control_Edit_User (1 ComboBox - Users only)

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Edit_User_ComboBox_Users | User Selection | "Enter or Select User" | Dao_User.GetAllUsernamesAsync() | MaxResults=100, EnableWildcards=true |

**NOTE**: Shift ComboBox is DropDownList (not editable) - NO MIGRATION NEEDED

- [ ] **Migrate Users ComboBox to SuggestionTextBox**
- [ ] **Test in Settings → Edit User workflow**
- [ ] **Verify user selection triggers data load**

**File**: `Controls\SettingsForm\Control_Edit_User.Designer.cs` + `Control_Edit_User.cs`  
**Priority**: P3 - Admin feature  
**Complexity**: Low (single control)

---

#### Control_Remove_PartID (1 ComboBox)

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Remove_PartID_ComboBox_Parts | Part Selector | "Enter or Select Part Number" | Dao_Part.GetAllPartIDsAsync() | MaxResults=100, EnableWildcards=true |

- [ ] **Migrate Parts ComboBox to SuggestionTextBox**
- [ ] **Test in Settings → Remove Part workflow**
- [ ] **Verify part selection for deletion**

**File**: `Controls\SettingsForm\Control_Remove_PartID.Designer.cs` + `Control_Remove_PartID.cs`  
**Priority**: P3 - Admin feature  
**Complexity**: Low (single control)

---

## ❌ EXCLUDED: Read-Only DropDownList Controls (18 items)

These ComboBoxes use `DropDownStyle = ComboBoxStyle.DropDownList` (selection-only, no text entry) with **hardcoded/static data** and should **NOT** be migrated:

### Theme/UI Selectors
- Control_Theme: Theme picker (fixed list of themes from database, but curated list)
- Various forms: Printer selection dialogs
- Control_Add_User / Control_Edit_User: Shift selector (Day/Night/Swing - hardcoded)
- Control_Add_Location / Control_Edit_Location: Building picker (Expo/Vits - hardcoded)

### User/Filter Dropdowns
- ViewApplicationLogsForm: Log source filters, severity filters
- TransactionGridControl: User filters, date range filters (DropDownList, read-only)
- TransactionSearchControl: User, ToLocation, FromLocation (DropDownList, read-only)
- Form_QuickButtonEdit: Operation selector (DropDownList, read-only)

**Reason for Exclusion**: These controls present fixed, curated lists or hardcoded values for selection. Users should not type free text or use wildcards. ComboBox.DropDownList is the correct control type for these use cases.

**⚠️ IMPORTANT DISTINCTION**: DropDownList controls that load from database via `Helper_UI_ComboBoxes` (ItemTypes, Operations, Locations, Users) **SHOULD** be migrated for consistency with the new suggestion system!

---

## 🆕 ADDED: DropDownList Controls with Database Data (7 items)

**CRITICAL DISCOVERY**: These DropDownList controls load data from database via `Helper_UI_ComboBoxes` and should be migrated for consistency!

### Control_Edit_ItemType (1 DropDownList) - T133-T140

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Edit_ItemType_ComboBox_ItemTypes | ItemType Selector | "Enter or Select Item Type" | Dao_ItemType.GetAllItemTypesAsync() | MaxResults=50, EnableWildcards=true |

- [ ] **Migrate ItemTypes DropDownList to SuggestionTextBox**
- [ ] **Replace Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync() with direct DAO call**
- [ ] **Test in Settings → Edit Item Type workflow**
- [ ] **Verify ItemType selection triggers data load**

**File**: `Controls\SettingsForm\Control_Edit_ItemType.Designer.cs` + `Control_Edit_ItemType.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low  
**Database**: Loads from `md_item_types_Get_All` via Helper_UI_ComboBoxes

---

### Control_Remove_ItemType (1 DropDownList) - T141-T148

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Remove_ItemType_ComboBox_ItemTypes | ItemType Selector | "Enter or Select Item Type" | Dao_ItemType.GetAllItemTypesAsync() | MaxResults=50, EnableWildcards=true |

- [ ] **Migrate ItemTypes DropDownList to SuggestionTextBox**
- [ ] **Replace Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync() with direct DAO call**
- [ ] **Test in Settings → Remove Item Type workflow**
- [ ] **Verify ItemType selection for deletion**

**File**: `Controls\SettingsForm\Control_Remove_ItemType.Designer.cs` + `Control_Remove_ItemType.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low  
**Database**: Loads from `md_item_types_Get_All` via Helper_UI_ComboBoxes

---

### Control_Edit_Operation (1 DropDownList) - T149-T156

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Edit_Operation_ComboBox_Operations | Operation Selector | "Enter or Select Operation" | Dao_Operation.GetAllOperationsAsync() | MaxResults=50, EnableWildcards=true |

- [ ] **Migrate Operations DropDownList to SuggestionTextBox**
- [ ] **Replace Helper_UI_ComboBoxes.FillOperationComboBoxesAsync() with direct DAO call**
- [ ] **Test in Settings → Edit Operation workflow**
- [ ] **Verify Operation selection triggers data load**

**File**: `Controls\SettingsForm\Control_Edit_Operation.Designer.cs` + `Control_Edit_Operation.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low  
**Database**: Loads from `md_operation_numbers_Get_All` via Helper_UI_ComboBoxes

---

### Control_Remove_Operation (1 DropDownList) - T157-T164

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Remove_Operation_ComboBox_Operations | Operation Selector | "Enter or Select Operation" | Dao_Operation.GetAllOperationsAsync() | MaxResults=50, EnableWildcards=true |

- [ ] **Migrate Operations DropDownList to SuggestionTextBox**
- [ ] **Replace Helper_UI_ComboBoxes.FillOperationComboBoxesAsync() with direct DAO call**
- [ ] **Test in Settings → Remove Operation workflow**
- [ ] **Verify Operation selection for deletion**

**File**: `Controls\SettingsForm\Control_Remove_Operation.Designer.cs` + `Control_Remove_Operation.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low  
**Database**: Loads from `md_operation_numbers_Get_All` via Helper_UI_ComboBoxes

---

### Control_Edit_Location (1 DropDownList) - T165-T172

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Edit_Location_ComboBox_Locations | Location Selector | "Enter or Select Location" | Dao_Location.GetAllLocationsAsync() | MaxResults=100, EnableWildcards=true |

**NOTE**: Building ComboBox is hardcoded (Expo/Vits) - NO MIGRATION NEEDED

- [ ] **Migrate Locations DropDownList to SuggestionTextBox**
- [ ] **Replace Helper_UI_ComboBoxes.FillLocationComboBoxesAsync() with direct DAO call**
- [ ] **Test in Settings → Edit Location workflow**
- [ ] **Verify Location selection triggers data load**

**File**: `Controls\SettingsForm\Control_Edit_Location.Designer.cs` + `Control_Edit_Location.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low  
**Database**: Loads from `md_locations_Get_All` via Helper_UI_ComboBoxes

---

### Control_Remove_Location (1 DropDownList) - T173-T180

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Remove_Location_ComboBox_Locations | Location Selector | "Enter or Select Location" | Dao_Location.GetAllLocationsAsync() | MaxResults=100, EnableWildcards=true |

- [ ] **Migrate Locations DropDownList to SuggestionTextBox**
- [ ] **Replace Helper_UI_ComboBoxes.FillLocationComboBoxesAsync() with direct DAO call**
- [ ] **Test in Settings → Remove Location workflow**
- [ ] **Verify Location selection for deletion**

**File**: `Controls\SettingsForm\Control_Remove_Location.Designer.cs` + `Control_Remove_Location.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low  
**Database**: Loads from `md_locations_Get_All` via Helper_UI_ComboBoxes

---

### Control_Remove_User (1 DropDownList) - T181-T188

| # | Control Name | Field Purpose | Placeholder Text | DAO Method | Config |
|---|--------------|---------------|------------------|------------|--------|
| 1 | Control_Remove_User_ComboBox_Users | User Selector | "Enter or Select User" | Dao_User.GetAllUsernamesAsync() | MaxResults=100, EnableWildcards=true |

- [ ] **Migrate Users DropDownList to SuggestionTextBox**
- [ ] **Replace Helper_UI_ComboBoxes.FillUserComboBoxesAsync() with direct DAO call**
- [ ] **Test in Settings → Remove User workflow**
- [ ] **Verify User selection for deletion**

**File**: `Controls\SettingsForm\Control_Remove_User.Designer.cs` + `Control_Remove_User.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low  
**Database**: Loads from `md_users_Get_All` via Helper_UI_ComboBoxes

---

## ❌ EXCLUDED: Read-Only DropDownList Controls (18 items - UPDATED)

---

## Migration Statistics

| Category | Total | Migrated | Remaining | % Complete |
|----------|-------|----------|-----------|------------|
| **MainForm Children** | 14 | 3 | 11 | 21% |
| **Standalone Forms** | 1 | 0 | 1 | 0% |
| **SettingsForm Children (ComboBox)** | 3 | 0 | 3 | 0% |
| **SettingsForm Children (DropDownList)** | 7 | 0 | 7 | 0% |
| **Optional (TransactionSearch)** | 1 | 0 | 1 | 0% |
| **TOTAL** | **25** | **3** | **22** | **12%** |

**Critical Discovery** (November 12, 2025):
- Added 7 DropDownList controls that load from database via `Helper_UI_ComboBoxes`
- These controls use stored procedures (`md_item_types_Get_All`, `md_operation_numbers_Get_All`, `md_locations_Get_All`, `md_users_Get_All`)
- Migration ensures consistency across the application - all database-loaded dropdowns use suggestion system
- Helper_UI_ComboBoxes calls will be replaced with direct DAO method calls in data provider methods

---

## Next Steps

1. **Start with Control_TransferTab** (P1, medium complexity, high user impact)
2. **Follow with Control_RemoveTab** (P1, low complexity, quick win)
3. **Tackle Control_AdvancedInventory** (P1, high complexity, block other work)
4. **Complete Control_AdvancedRemove** (P2, medium complexity)
5. **Finish Form_QuickButtonEdit** (P2, low complexity)
6. **Polish Settings/Transaction controls** (P3, optional enhancements)

---

## Validation Checklist (Per Control)

After each migration, verify:

- [ ] ComboBox removed from Designer.cs and replaced with SuggestionTextBox
- [ ] Data provider method implemented (`GetXxxSuggestionsAsync`)
- [ ] SuggestionTextBox configured (DataProvider, MaxResults, EnableWildcards)
- [ ] Event handlers added (SuggestionSelected, SuggestionCancelled)
- [ ] Old ComboBox code removed (SelectedIndexChanged, DataSource binding)
- [ ] UI layout updated (Anchor properties for centering)
- [ ] PlaceholderText set in Designer
- [ ] Keyboard navigation tested (Tab, Arrow keys, Home, End, Enter, Escape)
- [ ] Mouse interaction tested (single-click, double-click, click outside)
- [ ] Wildcard patterns tested (%, %-suffix, prefix-%, prefix-%-suffix)
- [ ] Logging added via LoggingUtility
- [ ] No build errors or warnings
- [ ] User acceptance testing completed
