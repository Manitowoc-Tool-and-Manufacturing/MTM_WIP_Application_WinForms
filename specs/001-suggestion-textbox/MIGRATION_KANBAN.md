# SuggestionTextBox Migration - Kanban Board

**Last Updated**: November 12, 2025

```mermaid
---
config:
  kanban:
    ticketBaseUrl: 'https://github.com/Dorotel/MTM_WIP_Application_WinForms/issues/#TICKET#'
---
kanban
  todo[Todo]
    t1[Control_TransferTab - 3 ComboBoxes]@{ assigned: 'MainForm', ticket: 'T051-T062', priority: 'Very High' }
    t2[Control_RemoveTab - 2 ComboBoxes]@{ assigned: 'MainForm', ticket: 'T063-T071', priority: 'Very High' }
    t3[Control_AdvancedInventory - 6 ComboBoxes]@{ assigned: 'MainForm', ticket: 'T072-T086', priority: 'Very High' }
    t4[Form_QuickButtonEdit - 1 ComboBox]@{ assigned: 'Standalone', ticket: 'T097-T104', priority: 'High' }
    t5[Control_Edit_PartID - 1 ComboBox]@{ assigned: 'SettingsForm', ticket: 'T105-T111', priority: 'Low' }
    t6[Control_Edit_User - 1 ComboBox]@{ assigned: 'SettingsForm', ticket: 'T119-T125', priority: 'Low' }
    t7[Control_Remove_PartID - 1 ComboBox]@{ assigned: 'SettingsForm', ticket: 'T126-T132', priority: 'Low' }
    t8[TransactionSearchControl - 1 ComboBox]@{ assigned: 'Optional', ticket: 'T112-T118', priority: 'Very Low' }
    t9[Control_Edit_ItemType - 1 DropDownList]@{ assigned: 'SettingsForm', ticket: 'TBD', priority: 'Low' }
    t10[Control_Edit_Operation - 1 DropDownList]@{ assigned: 'SettingsForm', ticket: 'TBD', priority: 'Low' }
    t11[Control_Edit_Location - 1 DropDownList]@{ assigned: 'SettingsForm', ticket: 'TBD', priority: 'Low' }
    t12[Control_Remove_ItemType - 1 DropDownList]@{ assigned: 'SettingsForm', ticket: 'TBD', priority: 'Low' }
    t13[Control_Remove_Operation - 1 DropDownList]@{ assigned: 'SettingsForm', ticket: 'TBD', priority: 'Low' }
    t14[Control_Remove_Location - 1 DropDownList]@{ assigned: 'SettingsForm', ticket: 'TBD', priority: 'Low' }
    t15[Control_Remove_User - 1 DropDownList]@{ assigned: 'SettingsForm', ticket: 'TBD', priority: 'Low' }
  
  inProgress[In Progress]
  
  done[Done]
    d1[Control_InventoryTab - 3 ComboBoxes]@{ assigned: 'MainForm', ticket: 'T024-T042' }
```

---

## Complete Application Form Hierarchy (VERIFIED FROM ACTUAL FILES)

### MainForm (Forms\MainForm\MainForm.cs)
**Type**: Form | **Parent**: None (Top-level)

| Child Control | ComboBoxes Found | DropDownStyle | Migration Status | Reason / Notes |
|---------------|------------------|---------------|------------------|----------------|
| **Control_InventoryTab** | Part, Operation, Location | N/A (SuggestionTextBox) | ✅ MIGRATED | Already uses SuggestionTextBox (T024-T042) |
| **Control_TransferTab** | Part, Operation, ToLocation | DropDown (editable) | 📋 NEEDS MIGRATION | T051-T062 - 3 editable ComboBoxes |
| **Control_RemoveTab** | Part, Operation | DropDown (editable) | 📋 NEEDS MIGRATION | T063-T071 - 2 editable ComboBoxes |
| **Control_AdvancedInventory** | Multiple across 2 tabs | DropDown (editable) | 📋 NEEDS MIGRATION | T072-T086 - 6 editable ComboBoxes (Single + Multi-Loc tabs) |
| **Control_AdvancedRemove** | None found | N/A | ⚪ NO MIGRATION | Uses TextBoxes for Part/Op/Location, not ComboBoxes |
| **Control_QuickButtons** | None | N/A | ⚪ NO MIGRATION | Button management only - no ComboBoxes |

---

### SettingsForm (Forms\Settings\SettingsForm.cs)
**Type**: Form | **Parent**: None (Dialog)  
**Navigation**: TreeView with dynamically loaded panels

| Child Control | ComboBoxes Found | DropDownStyle | Migration Status | Reason / Notes |
|---------------|------------------|---------------|------------------|----------------|
| **Control_Database** | None | N/A | ⚪ NO MIGRATION | Connection settings - TextBoxes only |
| **Control_Add_User** | Shift | DropDownList | ⚪ NO MIGRATION | Fixed shift list (Day/Night/Swing) - selection only |
| **Control_Edit_User** | Users, Shift | DropDown (Users), DropDownList (Shift) | 📋 NEEDS MIGRATION | T119+ Users ComboBox is editable (AutoCompleteMode.Suggest), Shift is DropDownList (no migration) |
| **Control_Remove_User** | Users | Unknown (no DropDownStyle) | 📋 NEEDS MIGRATION | Loads from database via Helper_UI_ComboBoxes.FillUserComboBoxesAsync() |
| **Control_Add_PartID** | ItemType | DropDownList | ⚪ NO MIGRATION | Fixed list selection only |
| **Control_Edit_PartID** | Part, ItemType | DropDown (Part), DropDownList (ItemType) | 📋 NEEDS MIGRATION | T105-T111 - Part ComboBox is editable (AutoCompleteMode.SuggestAppend) |
| **Control_Remove_PartID** | Parts | DropDown (editable) | 📋 NEEDS MIGRATION | T120+ Parts ComboBox has AutoCompleteMode.SuggestAppend |
| **Control_Add_Operation** | None | N/A | ⚪ NO MIGRATION | Text entry only |
| **Control_Edit_Operation** | Operations | DropDownList | 📋 NEEDS MIGRATION | Loads from database via Helper_UI_ComboBoxes.FillOperationComboBoxesAsync() |
| **Control_Remove_Operation** | Operations | DropDownList | 📋 NEEDS MIGRATION | Loads from database via Helper_UI_ComboBoxes.FillOperationComboBoxesAsync() |
| **Control_Add_Location** | None | N/A | ⚪ NO MIGRATION | Text entry only |
| **Control_Edit_Location** | Locations, Building | DropDownList both | 📋 NEEDS MIGRATION | Locations loads from database via Helper_UI_ComboBoxes.FillLocationComboBoxesAsync(), Building is hardcoded |
| **Control_Remove_Location** | Locations | DropDownList | 📋 NEEDS MIGRATION | Loads from database via Helper_UI_ComboBoxes.FillLocationComboBoxesAsync() |
| **Control_Add_ItemType** | None | N/A | ⚪ NO MIGRATION | Text entry only |
| **Control_Edit_ItemType** | ItemTypes | DropDownList | 📋 NEEDS MIGRATION | Loads from database via Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync() |
| **Control_Remove_ItemType** | ItemTypes | DropDownList | 📋 NEEDS MIGRATION | Loads from database via Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync() |
| **Control_Theme** | Theme | DropDownList | ⚪ NO MIGRATION | Fixed theme list - selection only |
| **Control_Shortcuts** | None | N/A | ⚪ NO MIGRATION | Keyboard shortcut config - no ComboBoxes |
| **Control_About** | None | N/A | ⚪ NO MIGRATION | Display only - version info |
| **Control_Developer_ParameterPrefixMaintenance** | None | N/A | ⚪ NO MIGRATION | Developer tool - DataGridView only, no ComboBoxes |

---

### Standalone Forms

| Form | ComboBoxes Found | DropDownStyle | Migration Status | Reason / Notes |
|------|------------------|---------------|------------------|----------------|
| **Form_QuickButtonEdit** | PartId, Operation | DropDown (PartId), DropDownList (Operation) | 📋 NEEDS MIGRATION | T097-T104 - PartId ComboBox is editable with AutoCompleteMode.SuggestAppend |
| **Form_QuickButtonOrder** | None expected | N/A | ⚪ NO MIGRATION | Reordering UI - ListView/DataGridView |
| **PrintForm** | Printer selection | DropDownList | ⚪ NO MIGRATION | Fixed printer list |
| **SuggestionOverlayForm** | None | N/A | ⚪ NO MIGRATION | Part of SuggestionTextBox system |

---

### Transaction Forms & Controls

| Form/Control | ComboBoxes Found | DropDownStyle | Migration Status | Reason / Notes |
|--------------|------------------|---------------|------------------|----------------|
| **Transactions** (main form) | None directly | N/A | ⚪ NO MIGRATION | Container for child controls |
| **TransactionSearchControl** | PartNumber, User, ToLocation, FromLocation | DropDown (PartNumber), DropDownList (others) | 📋 NEEDS MIGRATION | T112-T118 (Optional) - PartNumber ComboBox is editable with AutoCompleteMode.SuggestAppend, others are DropDownList |
| **TransactionGridControl** | None expected | N/A | ⚪ NO MIGRATION | DataGridView display only |
| **TransactionDetailPanel** | None | N/A | ⚪ NO MIGRATION | Detail display only |
| **TransactionAnalyticsControl** | None | N/A | ⚪ NO MIGRATION | Charts/analytics display |
| **TransactionLifecycleForm** | None | N/A | ⚪ NO MIGRATION | Display only - no ComboBoxes |

---

### Error Reporting Forms

| Form/Control | ComboBoxes Found | DropDownStyle | Migration Status | Reason / Notes |
|--------------|------------------|---------------|------------------|----------------|
| **Form_ViewErrorReports** | Filter dropdowns | DropDownList | ⚪ NO MIGRATION | Fixed filter lists |
| **Form_ErrorReportDetailsDialog** | None | N/A | ⚪ NO MIGRATION | Display only |
| **Form_ReportIssue** | Severity | DropDownList | ⚪ NO MIGRATION | Fixed severity list |
| **EnhancedErrorDialog** | None | N/A | ⚪ NO MIGRATION | Error display with buttons |
| **Control_ErrorReportsGrid** | None | N/A | ⚪ NO MIGRATION | DataGridView display |
| **Control_ErrorReportDetails** | None | N/A | ⚪ NO MIGRATION | Detail display |

---

### Other Forms

| Form | ComboBoxes Found | DropDownStyle | Migration Status | Reason / Notes |
|------|------------------|---------------|------------------|----------------|
| **SplashScreenForm** | None | N/A | ⚪ NO MIGRATION | Startup screen |
| **ViewApplicationLogsForm** | User filter | DropDownList | ⚪ NO MIGRATION | Fixed user list filter |
| **BatchGenerationReportDialog** | None | N/A | ⚪ NO MIGRATION | Report display |
| **ErrorAnalysisReportDialog** | None | N/A | ⚪ NO MIGRATION | Report display |
| **PromptStatusManagerDialog** | None | N/A | ⚪ NO MIGRATION | Status management |
| **Dialog_AddParameterOverride** | None | N/A | ⚪ NO MIGRATION | Developer tool - no ComboBoxes found |
| **Dialog_EditParameterOverride** | None | N/A | ⚪ NO MIGRATION | Developer tool - no ComboBoxes found |
| **Control_ProgressBarUserControl** | None | N/A | ⚪ NO MIGRATION | Progress display |
| **Control_ConnectionStrengthControl** | None | N/A | ⚪ NO MIGRATION | Signal strength indicator |

---

## Migration Summary (VERIFIED - ALL FILES READ)

| Migration Status | Forms/Controls | ComboBoxes | Percentage | Notes |
|------------------|----------------|------------|------------|-------|
| ✅ **MIGRATED** | 1 | 3 | 6% | Control_InventoryTab complete (Part, Operation, Location) |
| 📋 **NEEDS MIGRATION** | 15 | 22 | 94% | 3 MainForm (11 controls), 1 Standalone (1 control), 10 SettingsForm (10 controls), 1 Optional (1 control) |
| ⚪ **NO MIGRATION NEEDED** | 27 | N/A | N/A | Hardcoded lists only (Shift, Building, Theme), no ComboBoxes, or display only |
| **TOTAL FORMS ANALYZED** | **43** | **25** | 100% | ALL 43 forms/controls analyzed - INCLUDES DropDownList with database data! |

**CRITICAL FINDING**: 7 additional DropDownList controls load data from database via `Helper_UI_ComboBoxes`:
- ✅ Control_Edit_ItemType, Control_Remove_ItemType (ItemTypes from `md_item_types_Get_All`)
- ✅ Control_Edit_Operation, Control_Remove_Operation (Operations from `md_operation_numbers_Get_All`)
- ✅ Control_Edit_Location, Control_Remove_Location (Locations from `md_locations_Get_All`)
- ✅ Control_Remove_User (Users from `md_users_Get_All`)

**KEY FINDINGS**:
- ✅ Control_AdvancedRemove does NOT need migration (uses TextBoxes, not ComboBoxes)
- ✅ TransactionSearchControl has 1 editable ComboBox (PartNumber), 3 DropDownList (User, ToLocation, FromLocation)
- ✅ Form_QuickButtonEdit has 1 editable ComboBox (PartId), 1 DropDownList (Operation)
- ✅ Control_Add_User only has DropDownList (Shift) - no migration needed
- ✅ All developer tools have no ComboBoxes

---

## Priority Lanes

### 🔴 P1 - Critical (MainForm Children) - 3 Forms with 11 ComboBoxes
- **Control_TransferTab** → 3 ComboBoxes (Part, Operation, ToLocation)
- **Control_RemoveTab** → 2 ComboBoxes (Part, Operation)
- **Control_AdvancedInventory** → 6 ComboBoxes across Single + Multi-Loc tabs

### 🟡 P2 - Important (Standalone Forms) - 1 Form with 1 ComboBox
- **Form_QuickButtonEdit** → 1 ComboBox (PartId)

### 🟢 P3 - Enhancement (Settings/Transactions) - 11 Forms with 11 ComboBoxes
- **Control_Edit_PartID** → 1 ComboBox (Part selector)
- **Control_Edit_User** → 1 ComboBox (Users selector)
- **Control_Remove_PartID** → 1 ComboBox (Parts selector)
- **Control_Edit_ItemType** → 1 DropDownList (ItemType selector - database loaded)
- **Control_Remove_ItemType** → 1 DropDownList (ItemType selector - database loaded)
- **Control_Edit_Operation** → 1 DropDownList (Operation selector - database loaded)
- **Control_Remove_Operation** → 1 DropDownList (Operation selector - database loaded)
- **Control_Edit_Location** → 1 DropDownList (Location selector - database loaded)
- **Control_Remove_Location** → 1 DropDownList (Location selector - database loaded)
- **Control_Remove_User** → 1 DropDownList (User selector - database loaded)
- **TransactionSearchControl** → 1 ComboBox (PartNumber filter) - Optional

---

## Progress Tracking

| Status | Form/Controls | ComboBoxes | Percentage |
|--------|---------------|------------|------------|
| ✅ Done | 1 | 3 | 6% |
| 🚧 In Progress | 0 | 0 | 0% |
| 📋 Todo | 15 | 22 | 94% |
| **TOTAL** | **16** | **25** | **100%** |

**Migration Velocity**: ~1 form/week (based on Control_InventoryTab completion)  
**Estimated Completion**: 15 weeks for all P1-P3 migrations

**NOTE**: Includes 7 DropDownList controls that load from database - these need migration for consistency!

---

## Workflow States

### 📋 Todo
Controls identified and ready for migration. Reference implementation (Control_InventoryTab) available in T050.

**Next Steps**:
1. Pick a control from Todo lane (start with P1 - Control_TransferTab)
2. Follow T050 reference pattern (7-step migration process)
3. Move to "In Progress" lane
4. Complete all tasks for that control
5. Move to "Done" lane after verification

### 🚧 In Progress
Currently being migrated following T050 reference pattern:
1. Replace ComboBox with SuggestionTextBox in Designer.cs
2. Implement async data provider method
3. Configure SuggestionTextBox properties
4. Add event handlers
5. Remove old ComboBox code
6. Test keyboard navigation
7. Add logging

### ✅ Done
Migration complete, tested, and deployed. All acceptance criteria met:
- Overlay displays correctly
- Keyboard navigation works
- Selection updates field
- Tab order correct
- Logging added

---

## Next Card to Pull

**→ Control_TransferTab (3 ComboBoxes) - T051-T062**

**Details**:
- **Priority**: P1 (Very High)
- **Parent**: MainForm
- **Complexity**: Medium
- **Fields**: Part, Operation, ToLocation
- **Expected Duration**: 2-3 hours
- **Blocks**: None
- **Similar to**: Control_InventoryTab (reference implementation)
- **Dependencies**: Dao_Part, Dao_Operation, Dao_Location

**Tasks**: T051-T062 (12 tasks total)
