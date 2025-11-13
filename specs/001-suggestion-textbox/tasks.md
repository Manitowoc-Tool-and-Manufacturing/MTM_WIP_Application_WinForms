# Tasks: Universal Suggestion System for TextBox Inputs

**Feature**: 001-suggestion-textbox  
**Date**: November 12, 2025  
**Input**: Design documents from `/specs/001-suggestion-textbox/`  
**Prerequisites**: ✅ plan.md, ✅ spec.md, ✅ research.md, ✅ data-model.md, ✅ contracts/, ✅ quickstart.md

**Tests**: Manual testing only (no automated tests per user request and spec.md confirmation)

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Path Conventions

- **WinForms application**: Controls/, Forms/, Services/, Models/, Data/, Helpers/ at repository root
- All paths are absolute: `C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\[folder]\[file]`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Create foundational components that ALL user stories depend on

- [X] T001 Create Service_SuggestionFilter static class in `Services\Service_SuggestionFilter.cs` with filtering, wildcard, and sorting logic per research.md
- [X] T002 [P] Create Model_Suggestion_Config class in `Models\Model_Suggestion_Config.cs` with configuration properties and validation
- [X] T003 [P] Create SuggestionSelectedEventArgs class in `Models\SuggestionSelectedEventArgs.cs` with event properties per contracts
- [X] T004 [P] Create SuggestionCancelledEventArgs class in `Models\SuggestionCancelledEventArgs.cs` with event properties and SuggestionCancelReason enum

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core SuggestionTextBox control and overlay form that ALL user stories require

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [X] T005 Create SuggestionOverlayForm.cs in `Forms\Shared\SuggestionOverlayForm.cs` inheriting from ThemedForm with ListBox and label controls
- [X] T006 Create SuggestionOverlayForm.Designer.cs in `Forms\Shared\SuggestionOverlayForm.Designer.cs` with UI layout (ListBox, lblMatchCount, proper anchoring)
- [X] T007 Implement SuggestionOverlayForm navigation methods (SelectNext, SelectPrevious, SelectFirst, SelectLast) with wrap-around logic
- [X] T008 Implement SuggestionOverlayForm selection methods (AcceptSelection sets DialogResult.OK, CancelSelection sets DialogResult.Cancel)
- [X] T009 Implement SuggestionOverlayForm keyboard handling (OnKeyDown) for arrow keys, Home, End, Enter, Escape per research.md
- [X] T010 Implement SuggestionOverlayForm mouse handling (double-click accepts, Deactivate event cancels for light dismiss)
- [X] T011 Implement SuggestionOverlayForm positioning (CalculatePosition method) with multi-monitor support and boundary detection
- [X] T012 Create SuggestionTextBox.cs in `Controls\Shared\SuggestionTextBox.cs` inheriting from System.Windows.Forms.TextBox
- [X] T013 Add SuggestionTextBox properties (DataProvider, MaxResults, EnableWildcards, ShowLoadingIndicator, LoadingThresholdMs, SuppressExactMatch, ClearOnNoMatch, MinimumInputLength) per contracts
- [X] T014 Add SuggestionTextBox events (SuggestionSelected, SuggestionCancelled, SuggestionOverlayOpened, SuggestionOverlayClosed) per contracts
- [X] T015 Implement SuggestionTextBox.OnLostFocus override to trigger suggestion display (invoke DataProvider, filter with Service_SuggestionFilter, show overlay if matches)
- [X] T016 Implement SuggestionTextBox.OnKeyDown override to intercept navigation keys when overlay visible and delegate to overlay form
- [X] T017 Implement SuggestionTextBox.ShowSuggestionsAsync method for manual trigger with validation and error handling via Service_ErrorHandler
- [X] T018 Implement SuggestionTextBox.ApplyConfig method to apply Model_Suggestion_Config settings
- [X] T019 Implement SuggestionTextBox.Dispose override to ensure overlay form is disposed properly
- [X] T020 Add SuggestionTextBox designer attributes ([DesignerCategory("Component")], [ToolboxItem(true)]) for toolbox support
- [X] T021 Add comprehensive XML documentation to all SuggestionTextBox public members per MTM standards
- [X] T022 Add comprehensive XML documentation to all SuggestionOverlayForm public members per MTM standards
- [X] T023 Test SuggestionTextBox and SuggestionOverlayForm foundational integration (create test form with sample data provider, verify overlay displays, keyboard navigation works, selection updates text)

**Checkpoint**: Foundation ready - SuggestionTextBox control fully functional and ready for integration into user stories

---

## Phase 3: User Story 1 - Part Number Entry with Autocomplete (Priority: P1) 🎯 MVP

**Goal**: Enable part number autocomplete on Inventory tab with wildcard support, reducing entry time and errors

**Independent Test**: 
1. Open application to Inventory tab
2. Type "R-" in Part Number field and press Tab
3. Verify overlay shows filtered part numbers starting with "R-"
4. Press Down arrow twice, press Enter
5. Verify field updates with selected part and focus moves to Operation field
6. Type "R-%-01" and press Tab
7. Verify wildcard pattern matching works (shows R-ABC-01, R-XYZ-01, etc.)

### Implementation for User Story 1

- [X] T024 [US1] Open Control_InventoryTab.cs in `Controls\MainForm\Control_InventoryTab.cs` and locate existing part number ComboBox control
- [X] T025 [US1] Replace ComboBox declaration with SuggestionTextBox in Control_InventoryTab.Designer.cs for part number field
- [X] T026 [US1] Update Control_InventoryTab.Designer.cs InitializeComponent to instantiate SuggestionTextBox instead of ComboBox for part number
- [X] T027 [US1] Remove ComboBox DataSource binding code from Control_InventoryTab constructor or Load event for part number
- [X] T028 [US1] Implement GetPartNumberSuggestionsAsync method in Control_InventoryTab.cs calling Dao_Part.GetAllPartIDsAsync with proper error handling
- [X] T029 [US1] Configure part number SuggestionTextBox in Control_InventoryTab Load event (set DataProvider, MaxResults=100, EnableWildcards=true, ClearOnNoMatch=true)
- [X] T030 [US1] Add SuggestionSelected event handler for part number field to log selection and optionally trigger dependent field updates
- [X] T031 [US1] Add SuggestionCancelled event handler for part number field to log cancellation with reason
- [X] T032 [US1] Replace ComboBox declaration with SuggestionTextBox in Control_InventoryTab.Designer.cs for operation field
- [X] T033 [US1] Update Control_InventoryTab.Designer.cs InitializeComponent for operation SuggestionTextBox
- [X] T034 [US1] Remove ComboBox DataSource binding for operation field
- [X] T035 [US1] Implement GetOperationSuggestionsAsync method in Control_InventoryTab.cs (use Dao_Operation or cached data if available)
- [X] T036 [US1] Configure operation SuggestionTextBox in Control_InventoryTab Load event (DataProvider, MaxResults=50, EnableWildcards=true)
- [X] T037 [US1] Add event handlers for operation field (SuggestionSelected, SuggestionCancelled)
- [X] T038 [US1] Test part number autocomplete: type partial text, verify overlay, wildcard patterns, selection, focus navigation
- [X] T039 [US1] Test operation autocomplete: type partial text, verify filtering, selection works consistently with part number
- [X] T040 [US1] Test edge cases: empty input (no overlay), exact match (no overlay), invalid input (field cleared with warning), no matches (field cleared)
- [X] T041 [US1] Verify keyboard navigation (arrow keys, Home, End, Enter, Escape) works correctly for both fields
- [X] T042 [US1] Add logging via LoggingUtility for all user interactions (overlay opened, selection made, cancellation) on Inventory tab

**Checkpoint**: User Story 1 complete - Part number and operation autocomplete working on Inventory tab with all acceptance criteria met

---

## Phase 4: User Story 2 - Keyboard Navigation for Power Users (Priority: P1)

**Goal**: Ensure full keyboard-only operation for shop floor users who prefer no mouse interaction

**Independent Test**:
1. Open Inventory tab
2. Type text in part number field, press Tab (no mouse)
3. Use only arrow keys (Down 3 times, Up 1 time) to navigate suggestions
4. Press Enter to select
5. Verify field updates and focus moves to next field
6. Repeat on operation field with Home/End keys
7. Test Escape key preserves original input

### Implementation for User Story 2

- [X] T043 [US2] Verify SuggestionTextBox keyboard handling (already implemented in T016) supports all required keys (arrow, Home, End, Enter, Escape, Tab)
- [X] T044 [US2] Verify SuggestionOverlayForm keyboard handling (already implemented in T009) properly responds to all navigation keys
- [X] T045 [US2] Test keyboard-only workflow on Inventory tab: Tab to trigger, arrows to navigate, Enter to select, no mouse needed
- [X] T046 [US2] Test Home key jumps to first suggestion, End key jumps to last suggestion
- [X] T047 [US2] Test Escape key closes overlay and preserves original input (focus stays on field)
- [X] T048 [US2] Test rapid Tab navigation through multiple fields (overlays don't stack or interfere)
- [X] T049 [US2] Verify tab order is correct (part number → operation → other fields) for logical data entry flow
- [X] T050 [US2] **REFERENCE IMPLEMENTATION**: See Control_InventoryTab.cs for complete working example of migrating ComboBox controls to SuggestionTextBox system. Key steps: (1) Replace ComboBox declarations with SuggestionTextBox in Designer.cs, (2) Implement async data provider methods (GetXxxSuggestionsAsync) that return List<string>, (3) Configure SuggestionTextBox properties (DataProvider, MaxResults, EnableWildcards) in constructor or Load event, (4) Add SuggestionSelected/SuggestionCancelled event handlers for validation and logging, (5) Remove old ComboBox SelectedIndexChanged handlers and DataSource binding code, (6) Update UI layout (Anchor vs Dock) for proper vertical centering, (7) Set PlaceholderText in Designer for user guidance. This pattern applies to ALL remaining form/control migrations.

**Checkpoint**: User Story 2 complete - Full keyboard navigation verified and working smoothly for power users

---

## Phase 5: Form/Control Migration (Organized by Hierarchy)

**Goal**: Migrate remaining ComboBox controls to SuggestionTextBox system using T050 reference pattern

**Reference Documents**:
- **Migration Inventory**: `specs/001-suggestion-textbox/MIGRATION_INVENTORY.md` (complete control list, placeholders, DAO methods)
- **Kanban Board**: `specs/001-suggestion-textbox/MIGRATION_KANBAN.md` (visual progress tracking)
- **Reference Implementation**: `Controls/MainForm/Control_InventoryTab.cs` (working example)

**Migration Statistics**: 22 remaining controls across 15 forms/controls (12% complete - includes DropDownList with database data)

---

### 🔴 Priority 1: MainForm Children (11 controls)

#### Control_TransferTab (3 ComboBoxes) - T051-T062

**Controls**: Part, Operation, ToLocation  
**File**: `Controls\MainForm\Control_TransferTab.Designer.cs` + `Control_TransferTab.cs`  
**Priority**: P1 - Critical user workflow  
**Complexity**: Medium

- [X] T051 Migrate Control_TransferTab_ComboBox_Part to SuggestionTextBox (follow T050 pattern, placeholder: "Enter or Select Part Number", MaxResults=100, EnableWildcards=true)
- [X] T052 Migrate Control_TransferTab_ComboBox_Operation to SuggestionTextBox (follow T050 pattern, placeholder: "Enter or Select Operation", MaxResults=50, EnableWildcards=true)
- [X] T053 Migrate Control_TransferTab_ComboBox_ToLocation to SuggestionTextBox (follow T050 pattern, placeholder: "Enter or Select Location", MaxResults=100, EnableWildcards=true)
- [X] T054 Implement GetPartNumberSuggestionsAsync calling Dao_Part.GetAllPartIDsAsync()
- [X] T055 Implement GetOperationSuggestionsAsync calling Dao_Operation.GetAllOperationsAsync()
- [X] T056 Implement GetLocationSuggestionsAsync calling Dao_Location.GetAllLocationsAsync()
- [X] T057 Configure all three SuggestionTextBox controls (DataProvider, MaxResults, EnableWildcards properties)
- [X] T058 Add event handlers (SuggestionSelected, SuggestionCancelled) for all three fields with logging
- [X] T059 Remove old ComboBox code (SelectedIndexChanged handlers, DataSource binding)
- [ ] T060 Test wildcard patterns on all three fields (%, %-suffix, prefix-%, complex patterns)
- [ ] T061 Verify keyboard navigation and tab order (Part → Operation → ToLocation)
- [ ] T062 Update MIGRATION_KANBAN.md: Move Control_TransferTab from Todo to Done

**Checkpoint**: Transfer tab migration complete

---

#### Control_RemoveTab (2 ComboBoxes) - T063-T071

**Controls**: Part, Operation  
**File**: `Controls\MainForm\Control_RemoveTab.Designer.cs` + `Control_RemoveTab.cs`  
**Priority**: P1 - Consistent UX across tabs  
**Complexity**: Low (identical to InventoryTab pattern)

- [ ] T063 Migrate Control_RemoveTab_ComboBox_Part to SuggestionTextBox (follow T050 pattern, placeholder: "Enter or Select Part Number", MaxResults=100, EnableWildcards=true)
- [ ] T064 Migrate Control_RemoveTab_ComboBox_Operation to SuggestionTextBox (follow T050 pattern, placeholder: "Enter or Select Operation", MaxResults=50, EnableWildcards=true)
- [ ] T065 Implement GetPartNumberSuggestionsAsync calling Dao_Part.GetAllPartIDsAsync()
- [ ] T066 Implement GetOperationSuggestionsAsync calling Dao_Operation.GetAllOperationsAsync()
- [ ] T067 Configure both SuggestionTextBox controls (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T068 Add event handlers (SuggestionSelected, SuggestionCancelled) for both fields with logging
- [ ] T069 Remove old ComboBox code (SelectedIndexChanged handlers, DataSource binding)
- [ ] T070 Test consistency with Inventory/Transfer tabs (identical behavior, keyboard nav)
- [ ] T071 Update MIGRATION_KANBAN.md: Move Control_RemoveTab from Todo to Done

**Checkpoint**: Remove tab migration complete

---

#### Control_AdvancedInventory (6 ComboBoxes across 2 tabs) - T072-T090

**Controls**: Part, Operation, Location (Single tab) + Part, Operation, Location (Multi-Loc tab)  
**File**: `Controls\MainForm\Control_AdvancedInventory.Designer.cs` + `Control_AdvancedInventory.cs`  
**Priority**: P1 - Power user feature  
**Complexity**: High (6 controls, 2 tabs, shared data providers)

**Single Transaction Tab:**
- [ ] T072 Migrate AdvancedInventory_Single_ComboBox_PartID to SuggestionTextBox (placeholder: "Enter or Select Part Number", MaxResults=100, EnableWildcards=true)
- [ ] T073 Migrate AdvancedInventory_Single_ComboBox_Operation to SuggestionTextBox (placeholder: "Enter or Select Operation", MaxResults=50, EnableWildcards=true)
- [ ] T074 Migrate AdvancedInventory_Single_ComboBox_Location to SuggestionTextBox (placeholder: "Enter or Select Location", MaxResults=100, EnableWildcards=true)

**Multi-Location Tab:**
- [ ] T075 Migrate AdvancedInventory_MultiLoc_ComboBox_PartID to SuggestionTextBox (placeholder: "Enter or Select Part Number", MaxResults=100, EnableWildcards=true)
- [ ] T076 Migrate AdvancedInventory_MultiLoc_ComboBox_Operation to SuggestionTextBox (placeholder: "Enter or Select Operation", MaxResults=50, EnableWildcards=true)
- [ ] T077 Migrate AdvancedInventory_MultiLoc_ComboBox_Location to SuggestionTextBox (placeholder: "Enter or Select Location", MaxResults=100, EnableWildcards=true)

**Implementation:**
- [ ] T078 Implement shared data provider methods (GetPartNumberSuggestionsAsync, GetOperationSuggestionsAsync, GetLocationSuggestionsAsync) - reuse for both tabs
- [ ] T079 Configure all six SuggestionTextBox controls (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T080 Add event handlers (SuggestionSelected, SuggestionCancelled) for all six fields with logging
- [ ] T081 Remove old ComboBox code from both tabs (SelectedIndexChanged handlers, DataSource binding)
- [ ] T082 Test Single tab: keyboard navigation, wildcard patterns, focus management
- [ ] T083 Test Multi-Loc tab: keyboard navigation, wildcard patterns, focus management
- [ ] T084 Test tab switching: verify no state conflicts between Single and Multi-Loc tabs
- [ ] T085 Verify consistency with standard InventoryTab behavior
- [ ] T086 Update MIGRATION_KANBAN.md: Move Control_AdvancedInventory from Todo to Done

**Checkpoint**: Advanced Inventory tab migration complete

---

#### Control_AdvancedRemove (3 ComboBoxes) - T087-T097

**Controls**: Part, Operation, Location  
**File**: `Controls\MainForm\Control_AdvancedRemove.Designer.cs` + `Control_AdvancedRemove.cs`  
**Priority**: P2 - Advanced feature  
**Complexity**: Medium

- [ ] T087 Migrate AdvancedRemove_ComboBox_PartID to SuggestionTextBox (placeholder: "Enter or Select Part Number", MaxResults=100, EnableWildcards=true)
- [ ] T088 Migrate AdvancedRemove_ComboBox_Operation to SuggestionTextBox (placeholder: "Enter or Select Operation", MaxResults=50, EnableWildcards=true)
- [ ] T089 Migrate AdvancedRemove_ComboBox_Location to SuggestionTextBox (placeholder: "Enter or Select Location", MaxResults=100, EnableWildcards=true)
- [ ] T090 Implement data provider methods (GetPartNumberSuggestionsAsync, GetOperationSuggestionsAsync, GetLocationSuggestionsAsync)
- [ ] T091 Configure all three SuggestionTextBox controls (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T092 Add event handlers (SuggestionSelected, SuggestionCancelled) for all three fields with logging
- [ ] T093 Remove old ComboBox code (SelectedIndexChanged handlers, DataSource binding)
- [ ] T094 Test consistency with standard RemoveTab behavior
- [ ] T095 Verify keyboard navigation and wildcard patterns
- [ ] T096 Update MIGRATION_KANBAN.md: Move Control_AdvancedRemove from Todo to Done

**Checkpoint**: Advanced Remove tab migration complete

---

### 🟡 Priority 2: Standalone Forms (1 control)

#### Form_QuickButtonEdit (1 ComboBox) - T097-T104

**Controls**: PartId  
**File**: `Forms\Shared\Form_QuickButtonEdit.Designer.cs` + `Form_QuickButtonEdit.cs`  
**Priority**: P2 - Quick Button management  
**Complexity**: Low (single control, simple dialog)

- [ ] T097 Migrate Form_QuickButtonEdit_ComboBox_PartId to SuggestionTextBox (placeholder: "Enter or Select Part Number", MaxResults=100, EnableWildcards=true)
- [ ] T098 Implement GetPartNumberSuggestionsAsync calling Dao_Part.GetAllPartIDsAsync()
- [ ] T099 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T100 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T101 Remove old ComboBox code (SelectedIndexChanged handler, DataSource binding)
- [ ] T102 Test in Quick Button edit workflow (modal dialog behavior)
- [ ] T103 Verify keyboard navigation in dialog context
- [ ] T104 Update MIGRATION_KANBAN.md: Move Form_QuickButtonEdit from Todo to Done

**Checkpoint**: Quick Button Edit form migration complete

---

### 🟢 Priority 3: Settings/Transaction Controls (2 controls)

#### Control_Edit_PartID (1 ComboBox) - T105-T111

**Controls**: Part Selector  
**File**: `Controls\SettingsForm\Control_Edit_PartID.Designer.cs` + `Control_Edit_PartID.cs`  
**Priority**: P3 - Admin feature  
**Complexity**: Low

- [ ] T105 Migrate Control_Edit_PartID_ComboBox_SelectPart to SuggestionTextBox (placeholder: "Enter or Select Part Number", MaxResults=100, EnableWildcards=true)
- [ ] T106 Implement GetPartNumberSuggestionsAsync calling Dao_Part.GetAllPartIDsAsync()
- [ ] T107 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T108 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T109 Remove old ComboBox code (SelectedIndexChanged handler, DataSource binding)
- [ ] T110 Test in Settings → Edit Part Number workflow (verify part selection triggers data load)
- [ ] T111 Update MIGRATION_KANBAN.md: Move Control_Edit_PartID from Todo to Done

**Checkpoint**: Settings Edit Part control migration complete

---

#### TransactionSearchControl (1 ComboBox) - T112-T118 [OPTIONAL]

**Controls**: PartNumber Filter  
**File**: `Controls\Transactions\TransactionSearchControl.Designer.cs` + `TransactionSearchControl.cs`  
**Priority**: P3 - Optional enhancement  
**Complexity**: Low

- [ ] T112 Migrate TransactionSearch_ComboBox_PartNumber to SuggestionTextBox (placeholder: "Enter or Select Part Number", MaxResults=100, EnableWildcards=true)
- [ ] T113 Implement GetPartNumberSuggestionsAsync calling Dao_Part.GetAllPartIDsAsync()
- [ ] T114 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T115 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T116 Remove old ComboBox code (SelectedIndexChanged handler, DataSource binding)
- [ ] T117 Test in transaction search/filter workflow (verify search performance with suggestions)
- [ ] T118 Update MIGRATION_KANBAN.md: Move TransactionSearchControl from Todo to Done

**Checkpoint**: Transaction Search control migration complete (optional)

---

### 🟢 Priority 3: SettingsForm DropDownList Controls (7 controls with database data)

**CRITICAL DISCOVERY**: These DropDownList controls load data from database via `Helper_UI_ComboBoxes` and should be migrated for consistency!

#### Control_Edit_ItemType (1 DropDownList) - T133-T140

**Controls**: ItemTypes (DropDownList loading from `md_item_types_Get_All`)  
**File**: `Controls\SettingsForm\Control_Edit_ItemType.Designer.cs` + `Control_Edit_ItemType.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low

- [ ] T133 Migrate Control_Edit_ItemType ItemTypesComboBox from DropDownList to SuggestionTextBox (placeholder: "Enter or Select Item Type", MaxResults=50, EnableWildcards=true)
- [ ] T134 Implement GetItemTypeSuggestionsAsync calling Dao_ItemType.GetAllItemTypesAsync()
- [ ] T135 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T136 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T137 Remove old ComboBox code and Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync() call
- [ ] T138 Test in Settings → Edit Item Type workflow
- [ ] T139 Verify ItemType selection triggers data load
- [ ] T140 Update MIGRATION_KANBAN.md: Move Control_Edit_ItemType from Todo to Done

---

#### Control_Remove_ItemType (1 DropDownList) - T141-T148

**Controls**: ItemTypes (DropDownList loading from `md_item_types_Get_All`)  
**File**: `Controls\SettingsForm\Control_Remove_ItemType.Designer.cs` + `Control_Remove_ItemType.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low

- [ ] T141 Migrate Control_Remove_ItemType ItemTypesComboBox from DropDownList to SuggestionTextBox (placeholder: "Enter or Select Item Type", MaxResults=50, EnableWildcards=true)
- [ ] T142 Implement GetItemTypeSuggestionsAsync calling Dao_ItemType.GetAllItemTypesAsync()
- [ ] T143 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T144 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T145 Remove old ComboBox code and Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync() call
- [ ] T146 Test in Settings → Remove Item Type workflow
- [ ] T147 Verify ItemType selection for deletion
- [ ] T148 Update MIGRATION_KANBAN.md: Move Control_Remove_ItemType from Todo to Done

---

#### Control_Edit_Operation (1 DropDownList) - T149-T156

**Controls**: Operations (DropDownList loading from `md_operation_numbers_Get_All`)  
**File**: `Controls\SettingsForm\Control_Edit_Operation.Designer.cs` + `Control_Edit_Operation.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low

- [ ] T149 Migrate Control_Edit_Operation OperationsComboBox from DropDownList to SuggestionTextBox (placeholder: "Enter or Select Operation", MaxResults=50, EnableWildcards=true)
- [ ] T150 Implement GetOperationSuggestionsAsync calling Dao_Operation.GetAllOperationsAsync()
- [ ] T151 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T152 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T153 Remove old ComboBox code and Helper_UI_ComboBoxes.FillOperationComboBoxesAsync() call
- [ ] T154 Test in Settings → Edit Operation workflow
- [ ] T155 Verify Operation selection triggers data load
- [ ] T156 Update MIGRATION_KANBAN.md: Move Control_Edit_Operation from Todo to Done

---

#### Control_Remove_Operation (1 DropDownList) - T157-T164

**Controls**: Operations (DropDownList loading from `md_operation_numbers_Get_All`)  
**File**: `Controls\SettingsForm\Control_Remove_Operation.Designer.cs` + `Control_Remove_Operation.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low

- [ ] T157 Migrate Control_Remove_Operation OperationsComboBox from DropDownList to SuggestionTextBox (placeholder: "Enter or Select Operation", MaxResults=50, EnableWildcards=true)
- [ ] T158 Implement GetOperationSuggestionsAsync calling Dao_Operation.GetAllOperationsAsync()
- [ ] T159 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T160 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T161 Remove old ComboBox code and Helper_UI_ComboBoxes.FillOperationComboBoxesAsync() call
- [ ] T162 Test in Settings → Remove Operation workflow
- [ ] T163 Verify Operation selection for deletion
- [ ] T164 Update MIGRATION_KANBAN.md: Move Control_Remove_Operation from Todo to Done

---

#### Control_Edit_Location (1 DropDownList) - T165-T172

**Controls**: Locations (DropDownList loading from `md_locations_Get_All`)  
**File**: `Controls\SettingsForm\Control_Edit_Location.Designer.cs` + `Control_Edit_Location.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low  
**NOTE**: Building ComboBox is hardcoded (Expo/Vits) - no migration needed

- [ ] T165 Migrate Control_Edit_Location LocationsComboBox from DropDownList to SuggestionTextBox (placeholder: "Enter or Select Location", MaxResults=100, EnableWildcards=true)
- [ ] T166 Implement GetLocationSuggestionsAsync calling Dao_Location.GetAllLocationsAsync()
- [ ] T167 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T168 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T169 Remove old ComboBox code and Helper_UI_ComboBoxes.FillLocationComboBoxesAsync() call
- [ ] T170 Test in Settings → Edit Location workflow
- [ ] T171 Verify Location selection triggers data load
- [ ] T172 Update MIGRATION_KANBAN.md: Move Control_Edit_Location from Todo to Done

---

#### Control_Remove_Location (1 DropDownList) - T173-T180

**Controls**: Locations (DropDownList loading from `md_locations_Get_All`)  
**File**: `Controls\SettingsForm\Control_Remove_Location.Designer.cs` + `Control_Remove_Location.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low

- [ ] T173 Migrate Control_Remove_Location LocationsComboBox from DropDownList to SuggestionTextBox (placeholder: "Enter or Select Location", MaxResults=100, EnableWildcards=true)
- [ ] T174 Implement GetLocationSuggestionsAsync calling Dao_Location.GetAllLocationsAsync()
- [ ] T175 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T176 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T177 Remove old ComboBox code and Helper_UI_ComboBoxes.FillLocationComboBoxesAsync() call
- [ ] T178 Test in Settings → Remove Location workflow
- [ ] T179 Verify Location selection for deletion
- [ ] T180 Update MIGRATION_KANBAN.md: Move Control_Remove_Location from Todo to Done

---

#### Control_Remove_User (1 DropDownList) - T181-T188

**Controls**: Users (DropDownList loading from `md_users_Get_All`)  
**File**: `Controls\SettingsForm\Control_Remove_User.Designer.cs` + `Control_Remove_User.cs`  
**Priority**: P3 - Admin feature, consistency  
**Complexity**: Low

- [ ] T181 Migrate Control_Remove_User UsersComboBox from DropDownList to SuggestionTextBox (placeholder: "Enter or Select User", MaxResults=100, EnableWildcards=true)
- [ ] T182 Implement GetUserSuggestionsAsync calling Dao_User.GetAllUsernamesAsync()
- [ ] T183 Configure SuggestionTextBox (DataProvider, MaxResults, EnableWildcards properties)
- [ ] T184 Add event handlers (SuggestionSelected, SuggestionCancelled) with logging
- [ ] T185 Remove old ComboBox code and Helper_UI_ComboBoxes.FillUserComboBoxesAsync() call
- [ ] T186 Test in Settings → Remove User workflow
- [ ] T187 Verify User selection for deletion
- [ ] T188 Update MIGRATION_KANBAN.md: Move Control_Remove_User from Todo to Done

---

**Phase 5 Complete**: All ComboBox and database-loaded DropDownList controls migrated to SuggestionTextBox. System-wide autocomplete with wildcard search fully deployed.

---

## Phase 6: User Story 5 - Mouse Interaction Validation (Priority: P3)

**Goal**: Validate point-and-click interaction works across all migrated controls

**Independent Test**:
1. Open any tab with suggestion fields
2. Type partial text, press Tab to show overlay
3. Single-click an item (verify highlighted but not selected)
4. Single-click different item (verify highlight moves)
5. Double-click an item (verify selected, field updated, overlay closed, focus moved)
6. Trigger overlay again, click outside overlay (verify light dismiss, original input preserved)

### Implementation for User Story 5

- [X] T119 [US5] Verify SuggestionOverlayForm mouse handling (already implemented in T010) supports single-click and double-click
- [X] T120 [US5] Test single-click on suggestion item highlights but doesn't select
- [X] T121 [US5] Test double-click on suggestion item accepts selection and closes overlay
- [ ] T122 [US5] Verify mouse interaction works consistently across ALL migrated controls (run through MIGRATION_INVENTORY.md checklist)
- [X] T123 [US5] Test mixed keyboard/mouse interaction (arrow keys + double-click, mouse highlight + Enter key)

**Checkpoint**: User Story 5 complete - Mouse interaction validated across all controls

---

## Phase 7: Polish & Final Validation

**Purpose**: Final improvements, documentation updates, and system-wide validation

- [ ] T124 Update quickstart.md with actual implementation examples from completed migrations (replace placeholder code with real patterns)
- [ ] T125 Verify all SuggestionTextBox properties have comprehensive XML documentation with usage examples
- [ ] T126 Verify all event args classes have complete XML documentation
- [ ] T127 Review all error handling across migrated controls: ensure Service_ErrorHandler used consistently (no MessageBox.Show)
- [ ] T128 Run system-wide testing: validate all migrated controls work correctly (keyboard nav, mouse, wildcards, logging)
- [ ] T129 Update MIGRATION_KANBAN.md: Verify all controls moved from Todo → Done (100% complete)
- [ ] T130 Performance validation: test suggestion overlay performance with large datasets (100+ items)
- [ ] T131 Accessibility review: verify screen reader compatibility and keyboard-only workflows
- [ ] T132 Create user documentation: keyboard shortcuts, wildcard patterns, best practices guide
- [ ] T133 Final acceptance testing: run through all user stories (US1-US5) end-to-end

**Checkpoint**: Feature complete - Universal suggestion system fully deployed and validated

---

## Summary

**Total Tasks**: 188  
**Completed**: 59 (31%)  
**Remaining**: 129 (69%)

**Migration Progress**:
- ✅ Foundation (Phase 1-2): Complete
- ✅ MVP (Phase 3-4): Complete - Control_InventoryTab
- 🚧 Form Migrations (Phase 5): 16% complete (4/25 controls)
  - MainForm: 4/14 controls done (29%)
  - Standalone: 0/1 controls done (0%)
  - SettingsForm: 0/10 controls done (0%)
- ⏳ Validation (Phase 6-7): Pending

**Critical Discovery**: Added 7 DropDownList controls (T133-T188) that load from database via Helper_UI_ComboBoxes - these need migration for consistency!

**Next Steps**:
1. Test Control_TransferTab migration (T060-T062)
2. Continue with Control_RemoveTab (T063-T071) - P1 priority
3. Move to additional MainForm controls
3. Tackle Control_AdvancedInventory (T072-T086) - High complexity
4. Complete remaining P2/P3 migrations
5. Run Phase 6-7 validation and polish

**Reference Documents**:
- `MIGRATION_INVENTORY.md` - Complete control inventory with placeholders and DAO methods
- `MIGRATION_KANBAN.md` - Visual progress tracking with Kanban board
- `Control_InventoryTab.cs` - Working reference implementation (T050 pattern)
1. Test operation field on Inventory tab (already done in US1)
2. Test operation field on Transfer tab
3. Test operation field on Remove tab
4. Verify identical behavior (filtering, keyboard nav, selection) across all three tabs

### Implementation for User Story 4

- [ ] T066 [US4] Follow T050 reference pattern: Open Control_TransferTab.cs and migrate Operation ComboBox to SuggestionTextBox
- [ ] T067 [US4] Follow T050 reference pattern: Replace Operation ComboBox with SuggestionTextBox in Control_TransferTab.Designer.cs
- [ ] T068 [US4] Follow T050 reference pattern: Update Control_TransferTab.Designer.cs InitializeComponent for Operation SuggestionTextBox
- [ ] T069 [US4] Follow T050 reference pattern: Remove Operation ComboBox DataSource binding code from Control_TransferTab
- [ ] T070 [US4] Follow T050 reference pattern: Implement GetOperationSuggestionsAsync method in Control_TransferTab.cs (reuse Dao_Operation or cached data)
- [ ] T071 [US4] Follow T050 reference pattern: Configure Operation SuggestionTextBox on Transfer tab (DataProvider, MaxResults=50, EnableWildcards=true)
- [ ] T072 [US4] Follow T050 reference pattern: Add event handlers for operation field on Transfer tab
- [ ] T073 [US4] Follow T050 reference pattern: Open Control_RemoveTab.cs and migrate Operation ComboBox to SuggestionTextBox
- [ ] T074 [US4] Follow T050 reference pattern: Replace Operation ComboBox with SuggestionTextBox in Control_RemoveTab.Designer.cs
- [ ] T075 [US4] Follow T050 reference pattern: Update Control_RemoveTab.Designer.cs InitializeComponent for Operation SuggestionTextBox
- [ ] T076 [US4] Follow T050 reference pattern: Remove Operation ComboBox DataSource binding code from Control_RemoveTab
- [ ] T077 [US4] Follow T050 reference pattern: Implement GetOperationSuggestionsAsync method in Control_RemoveTab.cs
- [ ] T078 [US4] Follow T050 reference pattern: Configure Operation SuggestionTextBox on Remove tab (DataProvider, MaxResults=50, EnableWildcards=true)
- [ ] T079 [US4] Follow T050 reference pattern: Add event handlers for operation field on Remove tab
- [ ] T080 [US4] Test operation selection on Transfer tab matches Inventory tab behavior (filtering, keyboard nav, focus movement)
- [ ] T081 [US4] Test operation selection on Remove tab matches Inventory and Transfer tab behavior
- [ ] T082 [US4] Verify consistent user experience: same keyboard shortcuts, same visual feedback, same error handling across all tabs

**Checkpoint**: User Story 4 complete - Operation selection consistent across Inventory, Transfer, and Remove tabs

---

## Phase 7: User Story 5 - Mouse Interaction for Casual Users (Priority: P3)

**Goal**: Support point-and-click interaction for administrators and office staff who prefer mouse

**Independent Test**:
1. Open any tab with suggestion fields
2. Type partial text, press Tab to show overlay
3. Single-click an item (verify highlighted but not selected)
4. Single-click different item (verify highlight moves)
5. Double-click an item (verify selected, field updated, overlay closed, focus moved)
6. Trigger overlay again, click outside overlay (verify light dismiss, original input preserved)

### Implementation for User Story 5

- [X] T083 [US5] Verify SuggestionOverlayForm mouse handling (already implemented in T010) supports single-click and double-click
- [X] T085 [US5] Test single-click on suggestion item highlights but doesn't select
- [X] T086 [US5] Test double-click on suggestion item accepts selection and closes overlay
- [ ] T088 [US5] Verify mouse interaction works consistently across all tabs (Inventory, Transfer, Remove)
- [X] T089 [US5] Test mixed keyboard/mouse interaction (arrow keys + double-click, mouse highlight + Enter key)

**Checkpoint**: User Story 5 complete - Mouse interaction fully supported for casual users

---

## Phase 8: User Story 6 - Customer Name Entry in Part Management (Priority: P3)

**Goal**: Enable customer name autocomplete in Settings form for administrators managing parts

**Independent Test**:
1. Open Settings form → Part Management section
2. Click "Add New Part" button
3. Type partial customer name (e.g., "Acme") in Customer field and press Tab
4. Verify overlay shows matching customers from master data
5. Select customer and verify field is filled with validated name

### Implementation for User Story 6

- [ ] T091 [US6] Follow T050 reference pattern: Open SettingsForm.cs and locate Part Management section with Customer ComboBox
- [ ] T092 [US6] Follow T050 reference pattern: Identify Customer ComboBox in Part Management UI (may be in separate panel or grouped controls)
- [ ] T093 [US6] Follow T050 reference pattern: Replace Customer ComboBox with SuggestionTextBox in SettingsForm.Designer.cs
- [ ] T094 [US6] Follow T050 reference pattern: Update SettingsForm.Designer.cs InitializeComponent for Customer SuggestionTextBox
- [ ] T095 [US6] Follow T050 reference pattern: Remove Customer ComboBox DataSource binding code from SettingsForm
- [ ] T096 [US6] Follow T050 reference pattern: Implement GetCustomerSuggestionsAsync method in SettingsForm.cs calling Dao_Part or equivalent to get distinct customers
- [ ] T097 [US6] Follow T050 reference pattern: Configure Customer SuggestionTextBox (DataProvider, MaxResults=100, EnableWildcards=false, MinimumInputLength=2)
- [ ] T098 [US6] Follow T050 reference pattern: Add event handlers for customer field (SuggestionSelected, SuggestionCancelled)
- [ ] T099 [US6] Test customer name autocomplete: type partial name, verify matching customers displayed
- [ ] T100 [US6] Verify invalid customer name (not in master data) clears field and shows validation message
- [ ] T101 [US6] Add logging for customer selections in Part Management section

**Checkpoint**: User Story 6 complete - Customer name autocomplete working in Settings → Part Management

---

## Phase 9: Advanced Inventory Tab - Part Number and Operation (Priority: P1 Extension)

**Goal**: Extend suggestion system to Advanced Inventory tab for consistency

**Independent Test**: Same as US1 but on Advanced Inventory tab

### Implementation for Advanced Inventory

- [ ] T102 [US1-ADV] Follow T050 reference pattern: Open Control_AdvancedInventory.cs and migrate Part Number ComboBox to SuggestionTextBox
- [ ] T103 [US1-ADV] Follow T050 reference pattern: Replace Part Number ComboBox with SuggestionTextBox in Control_AdvancedInventory.Designer.cs
- [ ] T104 [US1-ADV] Follow T050 reference pattern: Update Designer InitializeComponent for Part Number SuggestionTextBox
- [ ] T105 [US1-ADV] Follow T050 reference pattern: Remove ComboBox DataSource binding for part number
- [ ] T106 [US1-ADV] Follow T050 reference pattern: Implement GetPartNumberSuggestionsAsync method (reuse Dao_Part logic)
- [ ] T107 [US1-ADV] Follow T050 reference pattern: Configure Part Number SuggestionTextBox (DataProvider, MaxResults=100, EnableWildcards=true)
- [ ] T108 [US1-ADV] Follow T050 reference pattern: Replace Operation ComboBox with SuggestionTextBox in Control_AdvancedInventory.Designer.cs
- [ ] T109 [US1-ADV] Follow T050 reference pattern: Update Designer InitializeComponent for Operation SuggestionTextBox
- [ ] T110 [US1-ADV] Follow T050 reference pattern: Remove ComboBox DataSource binding for operation
- [ ] T111 [US1-ADV] Follow T050 reference pattern: Implement GetOperationSuggestionsAsync method
- [ ] T112 [US1-ADV] Follow T050 reference pattern: Configure Operation SuggestionTextBox (DataProvider, MaxResults=50, EnableWildcards=true)
- [ ] T113 [US1-ADV] Follow T050 reference pattern: Add event handlers for both fields
- [ ] T114 [US1-ADV] Test Advanced Inventory tab matches standard Inventory tab behavior

**Checkpoint**: Advanced Inventory tab complete

---

## Phase 10: Advanced Remove Tab - Part Number and Operation (Priority: P2 Extension)

**Goal**: Extend suggestion system to Advanced Remove tab

**Independent Test**: Same as Remove tab but with advanced features

### Implementation for Advanced Remove

- [ ] T115 [US4-ADV] Follow T050 reference pattern: Open Control_AdvancedRemove.cs and migrate Part Number ComboBox to SuggestionTextBox
- [ ] T116 [US4-ADV] Follow T050 reference pattern: Replace Part Number ComboBox with SuggestionTextBox in Control_AdvancedRemove.Designer.cs
- [ ] T117 [US4-ADV] Follow T050 reference pattern: Update Designer InitializeComponent for Part Number SuggestionTextBox
- [ ] T118 [US4-ADV] Follow T050 reference pattern: Remove ComboBox DataSource binding for part number
- [ ] T119 [US4-ADV] Follow T050 reference pattern: Implement GetPartNumberSuggestionsAsync method
- [ ] T120 [US4-ADV] Follow T050 reference pattern: Configure Part Number SuggestionTextBox
- [ ] T121 [US4-ADV] Follow T050 reference pattern: Replace Operation ComboBox with SuggestionTextBox in Control_AdvancedRemove.Designer.cs
- [ ] T122 [US4-ADV] Follow T050 reference pattern: Update Designer InitializeComponent for Operation SuggestionTextBox
- [ ] T123 [US4-ADV] Follow T050 reference pattern: Remove ComboBox DataSource binding for operation
- [ ] T124 [US4-ADV] Follow T050 reference pattern: Implement GetOperationSuggestionsAsync method
- [ ] T125 [US4-ADV] Follow T050 reference pattern: Configure Operation SuggestionTextBox
- [ ] T126 [US4-ADV] Follow T050 reference pattern: Add event handlers
- [ ] T127 [US4-ADV] Test Advanced Remove tab

**Checkpoint**: Advanced Remove tab complete

---

## Phase 11: Quick Button Edit Form - Part and Operation (Priority: P2 Extension)

**Goal**: Add suggestion support to Quick Button Edit dialog

**Independent Test**: Edit quick button, verify part and operation autocomplete

### Implementation for Quick Button Edit

- [ ] T128 [QBE] Open Form_QuickButtonEdit.cs in `Forms\Shared\Form_QuickButtonEdit.cs`
- [ ] T129 [QBE] Replace Part ID ComboBox with SuggestionTextBox in Form_QuickButtonEdit.Designer.cs
- [ ] T130 [QBE] Update Designer InitializeComponent for Part ID SuggestionTextBox
- [ ] T131 [QBE] Implement GetPartNumberSuggestionsAsync method in Form_QuickButtonEdit
- [ ] T132 [QBE] Configure Part ID SuggestionTextBox
- [ ] T133 [QBE] Replace Operation ComboBox with SuggestionTextBox in Form_QuickButtonEdit.Designer.cs
- [ ] T134 [QBE] Update Designer InitializeComponent for Operation SuggestionTextBox
**Goal**: Add Quick Button Edit dialog

**Independent Test**: Edit quick button, verify part and operation autocomplete

### Implementation for Quick Button Edit

- [ ] T128 [QBE] Follow T050 reference pattern: Open Form_QuickButtonEdit.cs and migrate Part ID ComboBox to SuggestionTextBox
- [ ] T129 [QBE] Follow T050 reference pattern: Replace Part ID ComboBox with SuggestionTextBox in Form_QuickButtonEdit.Designer.cs
- [ ] T130 [QBE] Follow T050 reference pattern: Update Designer InitializeComponent for Part ID SuggestionTextBox
- [ ] T131 [QBE] Follow T050 reference pattern: Implement GetPartNumberSuggestionsAsync method in Form_QuickButtonEdit
- [ ] T132 [QBE] Follow T050 reference pattern: Configure Part ID SuggestionTextBox
- [ ] T133 [QBE] Follow T050 reference pattern: Replace Operation ComboBox with SuggestionTextBox in Form_QuickButtonEdit.Designer.cs
- [ ] T134 [QBE] Follow T050 reference pattern: Update Designer InitializeComponent for Operation SuggestionTextBox
- [ ] T135 [QBE] Follow T050 reference pattern: Implement GetOperationSuggestionsAsync method
- [ ] T136 [QBE] Follow T050 reference pattern: Configure Operation SuggestionTextBox
- [ ] T137 [QBE] Follow T050 reference pattern: Add event handlers
- [ ] T138 [QBE] Test Quick Button Edit form with suggestions

**Checkpoint**: Quick Button Edit form complete

---

## Phase 12: Settings Form Additional Fields (Priority: P3 Extension)

**Goal**: Add remaining Settings form fields (Item Type, User, Role, etc.)

**Independent Test**: Verify each Settings field has appropriate autocomplete

### Implementation for Settings Additional Fields

- [ ] T139 [SET] Follow T050 reference pattern: Add Item Type SuggestionTextBox in Settings → Part Management (replace ComboBox in SettingsForm.Designer.cs)
- [ ] T140 [SET] Follow T050 reference pattern: Implement GetItemTypeSuggestionsAsync method calling Dao_ItemType
- [ ] T141 [SET] Follow T050 reference pattern: Configure Item Type SuggestionTextBox (DataProvider, MaxResults=50)
- [ ] T142 [SET] Follow T050 reference pattern: Add Username SuggestionTextBox in Settings → User Management (replace ComboBox)
- [ ] T143 [SET] Follow T050 reference pattern: Implement GetUsernameSuggestionsAsync method calling Dao_User
- [ ] T144 [SET] Follow T050 reference pattern: Configure Username SuggestionTextBox (DataProvider, MaxResults=100)
- [ ] T145 [SET] Follow T050 reference pattern: Add Full Name SuggestionTextBox in Settings → User Management
- [ ] T146 [SET] Follow T050 reference pattern: Implement GetFullNameSuggestionsAsync method
- [ ] T147 [SET] Follow T050 reference pattern: Configure Full Name SuggestionTextBox
- [ ] T148 [SET] Follow T050 reference pattern: Add Role Name SuggestionTextBox in Settings → Role Management (replace ComboBox)
- [ ] T149 [SET] Follow T050 reference pattern: Implement GetRoleNameSuggestionsAsync method calling Dao_System
- [ ] T150 [SET] Follow T050 reference pattern: Configure Role Name SuggestionTextBox (DataProvider, MaxResults=20)
- [ ] T151 [SET] Follow T050 reference pattern: Add Shift SuggestionTextBox in Settings → User Management
- [ ] T152 [SET] Follow T050 reference pattern: Implement GetShiftSuggestionsAsync method (may be simple list: Day, Night, Swing)
- [ ] T153 [SET] Follow T050 reference pattern: Configure Shift SuggestionTextBox (DataProvider, MaxResults=10, EnableWildcards=false)
- [ ] T154 [SET] Follow T050 reference pattern: Add Theme Name SuggestionTextBox in Settings → Theme Settings
- [ ] T155 [SET] Follow T050 reference pattern: Implement GetThemeNameSuggestionsAsync method
- [ ] T156 [SET] Follow T050 reference pattern: Configure Theme Name SuggestionTextBox (DataProvider, MaxResults=20, EnableWildcards=false)
- [ ] T157 [SET] Follow T050 reference pattern: Add event handlers for all Settings fields
- [ ] T158 [SET] Test all Settings form autocomplete fields

**Checkpoint**: All Settings form fields converted to suggestions

---

## Phase 13: Polish & Cross-Cutting Concerns

**Purpose**: Final improvements affecting multiple user stories

- [ ] T159 [P] Update quickstart.md with actual implementation examples from completed integration (replace placeholder code)
- [ ] T160 [P] Add comprehensive XML documentation to Service_SuggestionFilter if not already complete
- [ ] T161 Verify all SuggestionTextBox properties have appropriate XML docs with examples
- [ ] T162 Verify all event args classes have complete XML documentation
- [ ] T163 Review all error handling: ensure Service_ErrorHandler used consistently (no MessageBox.Show)
- [ ] T164 Review all logging: ensure LoggingUtility used for user actions (overlay opened, selections, cancellations)
- [ ] T165 Performance test: Load form with 10,000+ items in data provider, verify <50ms filtering and <100ms overlay display
- [ ] T166 Performance test: Open/close overlay 50 times rapidly, check for memory leaks or handle exhaustion
- [ ] T167 [P] Test multi-monitor setup: Position form near screen edge, verify overlay positions correctly
- [ ] T168 [P] Test DPI scaling: Run at 100%, 125%, 150%, 200%, verify overlay renders correctly at all scales
- [ ] T169 Test theme switching: Open overlay, switch theme (light ↔ dark), verify overlay adapts colors immediately
- [ ] T170 Test rapid tab navigation: Tab through 5+ suggestion fields quickly, verify no overlay stacking or interference
- [ ] T171 Code review: Check #region organization in all new files (Fields, Properties, Constructors, Methods, Events, Helpers, Cleanup)
- [ ] T172 Code review: Verify IDisposable implementation in SuggestionTextBox disposes overlay properly
- [ ] T173 Code review: Verify async/await patterns used correctly (no .Result or .Wait() blocking)
- [ ] T174 Run full application manual test: Navigate through all tabs, test all suggestion fields, verify no crashes or exceptions
- [ ] T175 Update AGENTS.md or RELEASE_NOTES_USER_FRIENDLY.md with suggestion system usage summary

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
  - Creates foundational models and service classes
- **Foundational (Phase 2)**: Depends on Phase 1 (needs models and service) - BLOCKS all user stories
  - Creates SuggestionTextBox control and SuggestionOverlayForm
- **User Stories (Phase 3-12)**: All depend on Foundational phase completion
  - Phase 3 (US1): Inventory tab part/operation - CAN START IMMEDIATELY after Phase 2
  - Phase 4 (US2): Keyboard navigation - CAN START IMMEDIATELY after Phase 2 (validates existing behavior)
  - Phase 5 (US3): Transfer tab locations - CAN START IMMEDIATELY after Phase 2
  - Phase 6 (US4): Operation consistency - Depends on Phase 3 partially (Inventory), but can parallelize Transfer/Remove
  - Phase 7 (US5): Mouse interaction - CAN START IMMEDIATELY after Phase 2 (validates existing behavior)
  - Phase 8 (US6): Settings customer - CAN START IMMEDIATELY after Phase 2
  - Phase 9 (Advanced Inventory): Can start after Phase 2
  - Phase 10 (Advanced Remove): Can start after Phase 2
  - Phase 11 (Quick Button Edit): Can start after Phase 2
  - Phase 12 (Settings additional): Can start after Phase 2
- **Polish (Phase 13)**: Depends on all desired user stories being complete

### User Story Dependencies

**INDEPENDENT** (can work in parallel after Phase 2):
- User Story 1 (Inventory tab) - Independent
- User Story 2 (Keyboard nav) - Independent (validation task)
- User Story 3 (Transfer locations) - Independent
- User Story 5 (Mouse interaction) - Independent (validation task)
- User Story 6 (Settings customer) - Independent
- Advanced Inventory - Independent
- Advanced Remove - Independent
- Quick Button Edit - Independent
- Settings additional fields - Independent

**PARTIAL DEPENDENCY**:
- User Story 4 (Operation consistency) - Integrates with US1 but can proceed in parallel for Transfer/Remove tabs

### Within Each User Story Phase

1. Locate existing ComboBox controls
2. Replace ComboBox declarations in Designer.cs files
3. Update InitializeComponent in Designer.cs
4. Remove ComboBox DataSource binding code
5. Implement GetXxxSuggestionsAsync data provider methods
6. Configure SuggestionTextBox properties
7. Add event handlers
8. Test functionality
9. Add logging

### Parallel Opportunities

**Phase 1 (Setup)**: All 4 tasks marked [P] can run in parallel (different files, no dependencies)

**Phase 2 (Foundational)**: Some parallelization possible:
- T005-T011 (SuggestionOverlayForm) - Must be sequential within this group
- T012-T023 (SuggestionTextBox) - Must be sequential within this group
- BUT: OverlayForm and TextBox can be developed in parallel by 2 developers

**Phase 3-12 (User Stories)**: MAXIMUM PARALLELIZATION
- With sufficient team capacity, ALL user story phases can work in parallel after Phase 2
- Each developer takes a different form/control integration
- Example: 6 developers complete 6 user stories simultaneously

**Phase 13 (Polish)**: Tasks marked [P] can run in parallel (documentation, testing on different setups)

---

## Parallel Example: After Phase 2 Complete

```bash
# Maximum parallelization with 6 developers:

Developer A: Phase 3 (US1 - Inventory tab)
Developer B: Phase 5 (US3 - Transfer tab) 
Developer C: Phase 6 (US4 - Remove tab operations)
Developer D: Phase 8 (US6 - Settings customer)
Developer E: Phase 9 (Advanced Inventory)
Developer F: Phase 11 (Quick Button Edit)

# All work independently, no file conflicts
```

---

## Implementation Strategy

### MVP First (Phases 1-4 Only)

**Recommended MVP Scope**: User Story 1 + User Story 2

1. Complete Phase 1: Setup (4 tasks) - ~2 hours
2. Complete Phase 2: Foundational (19 tasks) - ~1-2 days
3. Complete Phase 3: User Story 1 (19 tasks) - ~1 day
4. Complete Phase 4: User Story 2 (8 tasks) - ~2 hours (mostly validation)
5. **STOP and VALIDATE**: Test Inventory tab thoroughly
6. Deploy/demo if ready (Inventory tab fully functional)

**MVP Benefits**:
- Delivers highest priority feature (P1)
- Validates technical approach
- Gets user feedback early
- Proves suggestion system works before expanding

### Incremental Delivery

1. **Foundation** (Phases 1-2) → SuggestionTextBox control ready → 2-3 days
2. **MVP** (Add Phase 3-4) → Inventory tab working → +1 day → DEPLOY
3. **Expand** (Add Phase 5) → Transfer tab locations → +1 day → DEPLOY
4. **Consistency** (Add Phase 6) → All main tabs → +1 day → DEPLOY
5. **Settings** (Add Phase 8) → Admin features → +1 day → DEPLOY
6. **Complete** (Add remaining phases) → All 24 fields → +3 days → DEPLOY
7. **Polish** (Phase 13) → Production-ready → +1 day → FINAL RELEASE

**Total Estimated Time**: 9-12 days for complete implementation

### Parallel Team Strategy

With 3 developers working together:

1. **Week 1, Days 1-2**: All 3 developers collaborate on Phases 1-2 (Foundational)
2. **Week 1, Day 3**: Split into parallel tracks:
   - Developer A: Phase 3 (Inventory)
   - Developer B: Phase 5 (Transfer)
   - Developer C: Phase 8 (Settings)
3. **Week 1, Day 4**: Continue parallel:
   - Developer A: Phase 9 (Advanced Inventory)
   - Developer B: Phase 6 (Remove operations)
   - Developer C: Phase 11 (Quick Button Edit)
4. **Week 1, Day 5**: Complete remaining:
   - Developer A: Phase 10 (Advanced Remove)
   - Developer B: Phase 12 (Settings additional)
   - Developer C: Start Phase 13 (Polish)
5. **Week 2, Day 1**: All 3 developers on Phase 13 (Polish & Testing)

**Total Team Time**: 6 days with 3 developers = 18 person-days

---

## Success Metrics

Track these metrics from spec.md Success Criteria:

- [ ] **SC-001**: Part number entry time <5 seconds (down from 12 seconds) - MEASURE after Phase 3
- [ ] **SC-002**: Data entry error rate reduces 50% (from 8% to 4%) - MEASURE after 1 month
- [ ] **SC-003**: Overlay displays <100ms for 95% of queries - MEASURE during Phase 13
- [ ] **SC-004**: Filtering 1000 items <50ms - MEASURE during Phase 13
- [ ] **SC-005**: 80% user satisfaction with suggestions - SURVEY after deployment
- [ ] **SC-006**: New user training 30% faster - MEASURE with next training session
- [ ] **SC-007**: "Invalid data" support tickets reduce 40% - TRACK for 1 month
- [ ] **SC-008**: 90% of eligible fields use suggestions within 3 months - COUNT fields converted
- [ ] **SC-009**: Memory <10MB per control - MEASURE during Phase 13
- [ ] **SC-010**: Zero crashes/exceptions in production - MONITOR logs continuously

---

## Notes

- **[P] tasks** = Different files, no dependencies, safe to parallelize
- **[Story] label** = Maps task to user story for traceability (US1-US6, extensions, or settings)
- **No automated tests** = Manual testing per spec.md and user request
- Each user story should be independently testable and deployable
- Constitution compliance: Service_ErrorHandler (no MessageBox.Show), LoggingUtility (no Console.WriteLine), Model_Dao_Result pattern, async/await, ThemedForm integration
- Commit after each logical task group (e.g., after completing one form's conversion)
- Stop at any checkpoint to validate independently before proceeding
- Total: **188 tasks** organized across **7 phases** (updated after DropDownList discovery)
- **MVP**: 50 tasks (Phases 1-4) delivers core functionality
- **Complete**: All 188 tasks converts all 25 fields (18 ComboBox + 7 DropDownList) per MIGRATION_INVENTORY.md

---

**CURRENT PROGRESS**: Phase 5 - Control_TransferTab complete (T051-T059 ✓), 59 tasks done, 129 remaining
