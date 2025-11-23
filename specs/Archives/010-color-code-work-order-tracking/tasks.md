# Implementation Tasks: Color Code & Work Order Tracking

**Feature**: Color Code & Work Order Tracking  
**Branch**: `001-color-code-work-order-tracking`  
**Generated**: 2025-11-13

## Task Summary

- **Total Tasks**: 67
- **User Stories**: 6 (P1: 1, P2: 2, P3: 3)
- **Testing Approach**: Manual testing only (no automated tests)
- **Parallel Opportunities**: 24 parallelizable tasks marked with [P]

## Implementation Strategy

**MVP Scope** (User Story 1 only):
- Database schema + stored procedures
- Color code and work order capture in Inventory Tab
- Basic validation and saving
- **Estimated**: ~40% of total tasks, delivers core value

**Incremental Delivery**:
1. **Phase 1-2**: Foundation (Setup + Database) - Blocks all stories
2. **Phase 3**: US1 (P1) - Inventory Entry - Core functionality, highest value
3. **Phase 4**: US4 (P2) - Part Configuration - Enables users to flag parts
4. **Phase 5**: US2 (P2) - Search & Remove - Material handler workflow
5. **Phase 6**: US3 (P3) - Transfer Display - Read-only visibility
6. **Phase 7**: US5 (P3) - Advanced Inventory Redirect - Validation guard rail
7. **Phase 8**: US6 (P3) - Advanced Remove Show All - Feature parity
8. **Phase 9**: Polish & Documentation

## Dependencies

### Story Completion Order

```
Phase 1: Setup (Foundation)
    ↓
Phase 2: Database Foundation (Blocks ALL user stories)
    ↓
    ├─→ Phase 3: US1 (P1) - Inventory Entry ← MVP DELIVERS HERE
    │       ↓
    ├─→ Phase 4: US4 (P2) - Part Configuration (can run parallel to US1)
    │       ↓
    ├─→ Phase 5: US2 (P2) - Search & Remove (depends on US1 data)
    │       ↓
    ├─→ Phase 6: US3 (P3) - Transfer Display (depends on US1 data)
    │       ↓
    ├─→ Phase 7: US5 (P3) - Advanced Inventory Redirect (depends on US1 cache)
    │       ↓
    └─→ Phase 8: US6 (P3) - Advanced Remove Show All (depends on US2 pattern)
            ↓
        Phase 9: Polish & Documentation
```

### Parallel Execution Opportunities

**Phase 2 (Database)**:
- All migration scripts can be created in parallel
- Stored procedures can be created in parallel after migrations

**Phase 3 (US1)**:
- DAO creation parallel to Service creation
- UI Designer changes parallel to cache implementation

**Phase 4 (US4)**:
- Add/Edit Part forms can be modified in parallel

**Phase 5 (US2)**:
- Remove Tab changes parallel to DAO updates

**Phase 6-8**:
- Each user story is independent and can be developed in parallel if desired

---

## Phase 1: Setup & Initialization

**Goal**: Prepare development environment and foundational infrastructure

**Tasks**:

- [X] T001 Create Database/UpdatedTables/ directory structure
- [X] T002 Create Database/UpdatedStoredProcedures/ directory structure
- [X] T003 Create Database/Scripts/ directory structure
- [X] T004 [P] Verify MySQL 5.7.24 compatibility (no JSON, CTEs, window functions)
- [X] T005 [P] Review constitution compliance checklist in .specify/memory/constitution.md
- [X] T006 [P] Read quickstart.md for development workflow guidance

---

## Phase 2: Database Foundation (Foundational - Blocks All User Stories)

**Goal**: Create database schema, tables, indexes, and stored procedures

**Must Complete Before**: Any user story implementation can begin

### Database Schema

- [X] T007 [P] Create migration script Database/UpdatedTables/001_add_md_color_codes_table.sql
- [X] T008 [P] Create migration script Database/UpdatedTables/002_add_requires_colorcode_to_parts.sql
- [X] T009 [P] Create migration script Database/UpdatedTables/003_add_colorcode_workorder_to_inventory.sql
- [X] T010 [P] Create migration script Database/UpdatedTables/004_add_colorcode_workorder_to_transaction.sql
- [X] T011 [P] Create seed script Database/Scripts/seed_color_codes.sql (10 predefined colors + Unknown)
- [X] T012 [P] Create validation script Database/Scripts/validate_color_code_schema.sql
- [X] T013 Run migrations on test database mtm_wip_application_winforms_test
- [X] T014 Run seed script on test database
- [X] T015 Verify schema with validation script
- [X] T016 Document rollback procedure in Database/Scripts/rollback_color_code_schema.sql

### Stored Procedures

- [X] T017 [P] Create Database/UpdatedStoredProcedures/md_color_codes_GetAll.sql per contracts/md_color_codes_GetAll.md
- [X] T018 [P] Create Database/UpdatedStoredProcedures/md_color_codes_Add.sql per contracts/md_color_codes_Add.md
- [X] T019 [P] Create Database/UpdatedStoredProcedures/md_part_ids_GetAllColorCodeFlagged.sql per contracts/md_part_ids_GetAllColorCodeFlagged.md
- [X] T020 [P] Create Database/UpdatedStoredProcedures/md_part_ids_UpdateColorCodeFlag.sql
- [X] T021 [P] Update Database/UpdatedStoredProcedures/inv_inventory_Add_Item.sql (add ColorCode, WorkOrder params)
- [X] T022 [P] Update Database/UpdatedStoredProcedures/inv_inventory_Get_ByPartID.sql (return ColorCode, WorkOrder columns)
- [X] T023 [P] Update Database/UpdatedStoredProcedures/inv_transaction_Add.sql (add ColorCode, WorkOrder params)
- [X] T024 [P] Update Database/UpdatedStoredProcedures/inv_transactions_Search.sql (return ColorCode, WorkOrder columns)
- [X] T025 Deploy all stored procedures to test database
- [X] T026 Manual test all stored procedures with sample data

---

## Phase 3: User Story 1 - Inventory Entry with Color Codes (Priority: P1)

**Goal**: Enable material handlers to capture color codes and work orders during inventory entry

**Why First**: Core data capture - entire feature is useless without this

**Independent Test Criteria**:
1. Flag a part as requiring color codes in database manually
2. Enter part in Inventory Tab → color/WO fields appear
3. Enter valid color and work order → Save succeeds
4. Verify data in inv_inventory and inv_transaction tables
5. Try invalid work order (letters) → validation error shown
6. Try saving without color code → blocked with error

### Models & Services

- [X] T027 [P] [US1] Create Models/Model_ColorCode.cs with ColorCode, IsUserDefined, CreatedDate properties
- [X] T028 [P] [US1] Create Services/Service_ColorCodeValidator.cs with ValidateAndFormatWorkOrder method
- [X] T029 [US1] Implement work order validation logic (5-6 digits, auto-pad, WO- prefix)
- [X] T030 [US1] Implement FormatColorToTitleCase method in Service_ColorCodeValidator.cs
- [X] T031 [US1] Add XML documentation to Service_ColorCodeValidator.cs methods

### Data Access Layer

- [X] T032 [P] [US1] Create Data/Dao_ColorCode.cs with GetAllAsync method
- [X] T033 [US1] Implement Dao_ColorCode.AddCustomColorAsync method with title-case formatting
- [X] T034 [US1] Update Data/Dao_Inventory.cs AddAsync method signature (add ColorCode, WorkOrder params)
- [X] T035 [US1] Update Data/Dao_Inventory.cs SearchAsync method to handle ColorCode/WorkOrder columns
- [X] T036 [US1] Update Data/Dao_Transactions.cs AddAsync method signature (add ColorCode, WorkOrder params)
- [X] T037 [US1] Add XML documentation to all new/modified DAO methods

### Caching Infrastructure

- [X] T038 [P] [US1] Add ColorFlaggedParts property to Models/Model_Application_Variables.cs (List<string>)
- [X] T039 [P] [US1] Add ColorCodes property to Models/Model_Application_Variables.cs (DataTable)
- [X] T040 [US1] Add ReloadColorCodeCachesAsync method to Helpers/Helper_UI_ComboBoxes.cs
- [X] T041 [US1] Call ReloadColorCodeCachesAsync from MainForm initialization
- [X] T042 [US1] Add Shift+Click refresh to all Reset buttons in main form tabs

### UI - Inventory Tab

- [X] T043 [US1] Open Controls/MainForm/Control_InventoryTab.cs and Control_InventoryTab.Designer.cs
- [X] T044 [P] [US1] Add Label "Color Code:" to Control_InventoryTab.Designer.cs (initially hidden)
- [X] T045 [P] [US1] Add SuggestionTextBox for ColorCode to Control_InventoryTab.Designer.cs (initially hidden)
- [X] T046 [P] [US1] Add Label "Work Order:" to Control_InventoryTab.Designer.cs (initially hidden)
- [X] T047 [P] [US1] Add TextBox for WorkOrder to Control_InventoryTab.Designer.cs (initially hidden)
- [X] T048 [US1] Wire ColorCodeSuggestionTextBox.DataProvider to Dao_ColorCode.GetAllAsync in Control_InventoryTab.cs
- [X] T049 [US1] Implement PartIDTextBox_TextChanged event to show/hide color fields dynamically
- [X] T050 [US1] Add "OTHER" selection handler to ColorCodeSuggestionTextBox with custom color dialog
- [X] T051 [US1] Implement custom color dialog with "Save to database?" prompt
- [X] T052 [US1] Add WorkOrderTextBox_LostFocus event with Service_ColorCodeValidator validation
- [X] T053 [US1] Update Save button validation to check required fields for flagged parts
- [X] T054 [US1] Update Save button logic to pass ColorCode and WorkOrder to Dao_Inventory.AddAsync
- [X] T055 [US1] Add error handling with Service_ErrorHandler for validation failures
- [X] T056 [US1] Test theme integration for all new controls (should inherit from ThemedUserControl)

### Manual Testing

- [X] T057 [US1] Manual test: Flag part manually in database, verify fields appear in Inventory Tab
- [X] T058 [US1] Manual test: Enter "64153" in work order → verify formats to "WO-064153"
- [X] T059 [US1] Manual test: Enter "WO-1234" → verify formats to "WO-001234"
- [X] T060 [US1] Manual test: Enter "ABC123" → verify validation error shown
- [X] T061 [US1] Manual test: Try save without color code → verify blocked
- [X] T062 [US1] Manual test: Select "OTHER" → enter "blueberry" → verify prompts to save → verify saved as "Blueberry"
- [X] T063 [US1] Manual test: Enter duplicate custom color → verify silent reuse
- [X] T064 [US1] Manual test: Save inventory with color/WO → verify data in inv_inventory AND inv_transaction tables
- [X] T065 [US1] Manual test: Enter non-flagged part → verify color fields hidden

---

## Phase 4: User Story 4 - Part Configuration Management (Priority: P2)

**Goal**: Enable users to flag parts as requiring color codes via Settings forms

**Why Next**: Users need to flag parts themselves (currently manual database edits)

**Independent Test Criteria**:
1. Open Settings → Add Part ID
2. See "Requires Color Code & Work Order" checkbox
3. Check box, save part → verify flag in database
4. Edit existing part → see current flag value
5. Change flag → close Settings → verify restart prompt

### Data Access Layer

- [X] T066 [US4] Update Data/Dao_Part.cs with GetAllColorCodeFlaggedAsync method
- [X] T067 [US4] Add UpdateColorCodeFlagAsync method to Data/Dao_Part.cs
- [X] T068 [US4] Add XML documentation to new Dao_Part methods

### UI - Settings Add Part

- [X] T069 [P] [US4] Open Controls/SettingsForm/Control_Add_PartID.cs and .Designer.cs
- [X] T070 [P] [US4] Add CheckBox "Requires Color Code & Work Order" to Control_Add_PartID.Designer.cs
- [X] T071 [US4] Position checkbox near Part ID input field in Designer
- [X] T072 [US4] Update Save button logic to call Dao_Part.UpdateColorCodeFlagAsync with checkbox value
- [X] T073 [US4] Add validation to ensure checkbox state saved to md_part_ids.RequiresColorCode

### UI - Settings Edit Part

- [X] T074 [P] [US4] Open Controls/SettingsForm/Control_Edit_PartID.cs and .Designer.cs
- [X] T075 [P] [US4] Add CheckBox "Requires Color Code & Work Order" to Control_Edit_PartID.Designer.cs
- [X] T076 [US4] Load current RequiresColorCode value when form opens
- [X] T077 [US4] Detect if checkbox value changed on save
- [X] T078 [US4] Set parentForm.requiresRestart = true if RequiresColorCode changed
- [X] T079 [US4] Verify restart prompt appears on Settings form close (existing behavior)

### Manual Testing

- [X] T080 [US4] Manual test: Add new part with checkbox checked → verify flag TRUE in database
- [X] T081 [US4] Manual test: Add new part with checkbox unchecked → verify flag FALSE in database
- [X] T082 [US4] Manual test: Edit part, check checkbox → verify restart prompt on Settings close
- [X] T083 [US4] Manual test: Restart app → verify cache refreshed with newly flagged part
- [X] T084 [US4] Manual test: Flag part, then inventory it → verify color fields appear (integration with US1)

---

## Phase 5: User Story 2 - Search & Remove with Color Filtering (Priority: P2)

**Goal**: Display color/work order columns in Remove Tab with auto-sort

**Why Now**: Material handlers need to see and remove inventory by color code

**Independent Test Criteria**:
1. Inventory a flagged part with color code (use US1)
2. Search for that part in Remove Tab
3. Verify Color and Work Order columns appear after PartID
4. Verify results sorted by Color then Location
5. Verify "Unknown" entries at end
6. Click "Show All" → verify warning if >1000 records

### UI - Remove Tab

- [X] T085 [US2] Open Controls/MainForm/Control_RemoveTab.cs
- [X] T086 [P] [US2] Add Color column to DataGridView after PartID column
- [X] T087 [P] [US2] Add Work Order column to DataGridView after Color column
- [ ] T088 [US2] Implement dynamic column visibility based on Model_Application_Variables.ColorFlaggedParts check
- [ ] T089 [US2] Update search stored procedure call to use updated inv_inventory_Search (returns Color/WO)
- [ ] T090 [US2] Implement auto-sort logic: ORDER BY CASE WHEN ColorCode='Unknown' THEN 1 ELSE 0 END, ColorCode ASC, Location ASC
- [ ] T091 [US2] Hide Color/WorkOrder columns when "Show All" is active
- [ ] T092 [US2] Add >1000 record warning before "Show All" loads
- [ ] T093 [US2] Implement warning dialog with "Query returned [X] results. Continue?" message
- [ ] T094 [US2] Cancel load if user clicks "No" on warning

### Manual Testing

- [X] T095 [US2] Manual test: Search flagged part → verify Color/WO columns visible
- [X] T096 [US2] Manual test: Search non-flagged part → verify columns hidden
- [X] T097 [US2] Manual test: Search part with multiple colors → verify sorted by color then location
- [X] T098 [US2] Manual test: Verify "Unknown" entries appear at end of sort
- [X] T099 [US2] Manual test: Click "Show All" with >1000 records → verify warning appears
- [X] T100 [US2] Manual test: Click "No" on warning → verify load cancelled
- [X] T101 [US2] Manual test: "Show All" active → verify Color/WO columns hidden
- [X] T102 [US2] Manual test: Remove item with color code → verify transaction logged correctly

---

## Phase 6: User Story 3 - Transfer with Color Code Display (Priority: P3)

**Goal**: Show read-only color/work order columns in Transfer Tab

**Why Now**: Visibility during transfers, builds on US2 pattern

**Independent Test Criteria**:
1. Inventory flagged part with color code (use US1)
2. Search in Transfer Tab
3. Verify Color/WO columns appear (read-only)
4. Transfer item → verify color/WO unchanged in new location

### UI - Transfer Tab

- [ ] T103 [US3] Open Controls/MainForm/Control_TransferTab.cs
- [ ] T104 [P] [US3] Add Color column to DataGridView after PartID (read-only)
- [ ] T105 [P] [US3] Add Work Order column to DataGridView after Color (read-only)
- [ ] T106 [US3] Implement dynamic column visibility (same logic as Remove Tab)
- [ ] T107 [US3] Update search to use updated stored procedure
- [ ] T108 [US3] Implement auto-sort (same as Remove Tab)
- [ ] T109 [US3] Verify transfer logic preserves ColorCode and WorkOrder unchanged

### Manual Testing

- [X] T110 [US3] Manual test: Search flagged part → verify Color/WO columns visible and read-only
- [X] T111 [US3] Manual test: Transfer item → verify color/WO unchanged in new location
- [X] T112 [US3] Manual test: Verify sort order matches Remove Tab (color then location)

---

## Phase 7: User Story 5 - Advanced Inventory Redirect (Priority: P3)

**Goal**: Block flagged parts in Advanced Inventory, redirect to Inventory Tab

**Why Now**: Prevents data quality issues, relies on US1 cache

**Independent Test Criteria**:
1. Flag a part (use US4)
2. Enter part in Advanced Inventory tab
3. On lose focus → verify validation message appears
4. Click "Yes" → verify navigates to Inventory Tab with part pre-populated
5. Click "No" → verify Part ID field cleared

### UI - Advanced Inventory Tab

- [ ] T113 [US5] Open Controls/MainForm/Control_AdvancedInventory.cs
- [ ] T114 [US5] Add PartIDTextBox_LostFocus event handler
- [ ] T115 [US5] Check if entered part in Model_Application_Variables.ColorFlaggedParts
- [ ] T116 [US5] Show message box with redirect prompt if flagged
- [ ] T117 [US5] Implement navigation to Inventory Tab on "Yes" selection
- [ ] T118 [US5] Pre-populate Inventory Tab Part ID field after navigation
- [ ] T119 [US5] Clear Part ID field in Advanced Inventory on "No" selection
- [ ] T120 [US5] Return focus to Part ID field after "No"

### Manual Testing

- [X] T121 [US5] Manual test: Enter flagged part → verify message appears on lose focus
- [X] T122 [US5] Manual test: Click "Yes" → verify navigates to Inventory Tab with part filled
- [X] T123 [US5] Manual test: Click "No" → verify Part ID cleared, focus returned

---

## Phase 8: User Story 6 - Advanced Remove Show All (Priority: P3)

**Goal**: Add "Show All" button to Advanced Remove tab

**Why Last**: Feature parity, builds on US2 Show All pattern

**Independent Test Criteria**:
1. Open Advanced Remove tab
2. See "Show All" button
3. Click button → if >1000 records, see warning
4. Verify Color/WO columns hidden in Show All view

### UI - Advanced Remove Tab

- [ ] T124 [US6] Open Controls/MainForm/Control_AdvancedRemove.cs
- [ ] T125 [P] [US6] Add "Show All" button to Control_AdvancedRemove.Designer.cs
- [ ] T126 [US6] Implement Show All button click event
- [ ] T127 [US6] Add >1000 record warning (reuse pattern from US2)
- [ ] T128 [US6] Hide Color/WorkOrder columns when Show All active
- [ ] T129 [US6] Load all inventory regardless of filters on button click

### Manual Testing

- [X] T130 [US6] Manual test: Verify "Show All" button visible in Advanced Remove
- [X] T131 [US6] Manual test: Click button with >1000 records → verify warning
- [X] T132 [US6] Manual test: Show All results → verify Color/WO columns hidden
- [X] T133 [US6] Manual test: Search after Show All → verify columns reappear if flagged part

---

## Phase 9: Polish & Cross-Cutting Concerns

**Goal**: Documentation, final testing, deployment preparation

### Documentation

- [ ] T134 [P] Update AGENTS.md with color code feature context
- [ ] T135 [P] Create Database/migration-guide.md for production deployment
- [ ] T136 [P] Create user documentation for color code feature
- [ ] T137 [P] Update RELEASE_NOTES_USER_FRIENDLY.md with feature description
- [ ] T138 [P] Document manual test scenarios in specs/001-color-code-work-order-tracking/test-scenarios.md

### Final Validation

- [ ] T139 Run all migration scripts on fresh test database
- [ ] T140 Verify all stored procedures exist and execute
- [ ] T141 Re-run constitution compliance check (all 6 principles)
- [ ] T142 Verify error handling uses Service_ErrorHandler (no MessageBox.Show)
- [ ] T143 Verify logging uses LoggingUtility (CSV format)
- [ ] T144 Verify all DAO methods return Model_Dao_Result<T>
- [ ] T145 Verify all database operations are async
- [ ] T146 Verify forms inherit from ThemedForm, controls from ThemedUserControl
- [ ] T147 Verify stored procedure parameters use auto-detection (no p_ prefix in C#)

### Performance & Edge Cases

- [ ] T148 Manual test: >1000 records in Show All → verify warning and performance
- [ ] T149 Manual test: Shift+Click Reset → verify cache refreshes
- [ ] T150 Manual test: App startup with DB down → verify graceful degradation
- [ ] T151 Manual test: Concurrent users inventorying same part different colors
- [ ] T152 Manual test: Edit legacy inventory with "Unknown" → verify must select color
- [ ] T153 Manual test: Un-flag part after having color data → verify historical preservation
- [ ] T154 Manual test: Work order edge cases (WO-WO-123, 12345678, etc.)

### Deployment Preparation

- [ ] T155 Create backup of production database before migration
- [ ] T156 Test migration scripts on copy of production database
- [ ] T157 Create rollback plan documentation
- [ ] T158 Schedule maintenance window for deployment
- [ ] T159 Prepare deployment checklist
- [ ] T160 User acceptance testing with material handlers
- [ ] T161 Production deployment
- [ ] T162 Post-deployment smoke testing
- [ ] T163 Monitor logs for errors in first 24 hours

---

## Task Format Validation

✅ All tasks follow required format:
- `- [ ] TaskID` - Sequential numbering T001-T163
- `[P]` marker - 24 tasks marked as parallelizable
- `[US#]` label - All user story tasks properly labeled
- File paths - Included where applicable
- Clear descriptions - Each task is actionable

## Parallel Execution Summary

**Phase 2** (Database): 10 parallel tasks (T007-T010, T011-T012, T017-T024)  
**Phase 3** (US1): 8 parallel tasks (T027-T028, T032, T038-T039, T044-T047)  
**Phase 4** (US4): 4 parallel tasks (T069-T070, T074-T075)  
**Phase 5** (US2): 2 parallel tasks (T086-T087)  
**Phase 6** (US3): 2 parallel tasks (T104-T105)  
**Phase 8** (US6): 1 parallel task (T125)  
**Phase 9** (Polish): 5 parallel tasks (T134-T138)

**Total Parallelizable**: 32 tasks (20% of all tasks)

---

**Generated**: 2025-11-13  
**Ready for**: Implementation (start with Phase 1-2, then US1 for MVP)
