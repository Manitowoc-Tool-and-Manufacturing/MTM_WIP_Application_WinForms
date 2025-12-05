# Tasks: Analytics & Inventory Management Enhancements

**Feature**: Analytics & Inventory Management Enhancements
**Status**: Draft

## Phase 1: Setup
*Goal: Initialize project structure and database schema.*

- [x] T001 Create `sys_visual` table in `mtm_wip_application_winforms` database (Production) `Database/UpdatedDatabase/schema_sys_visual.sql`
- [x] T002 Create `sys_visual` table in `mtm_wip_application_winforms_test` database (Test) `Database/UpdatedDatabase/schema_sys_visual_test.sql`
- [x] T003 Update `Enum_SuggestionDataSource.cs` with new values (MTM_ItemType, MTM_Building, MTM_Warehouse, Infor_WorkOrder, Infor_CustomerOrder, Infor_PurchaseOrder) `Models/Enums/Enum_SuggestionDataSource.cs`
- [x] T004 Create `Model_Visual_UserShift.cs` in `Models/Analytics/` `Models/Analytics/Model_Visual_UserShift.cs`
- [x] T005 Create `Model_Visual_SysVisual.cs` in `Models/Analytics/` `Models/Analytics/Model_Visual_SysVisual.cs`
- [x] T006 Create `IDao_VisualAnalytics.cs` interface in `Data/` `Data/IDao_VisualAnalytics.cs`
- [x] T007 Create `Dao_VisualAnalytics.cs` implementation in `Data/` `Data/Dao_VisualAnalytics.cs`
- [x] T008 Create `IService_UserShiftLogic.cs` interface in `Services/Analytics/` `Services/Analytics/IService_UserShiftLogic.cs`
- [x] T009 Create `Service_UserShiftLogic.cs` implementation in `Services/Analytics/` `Services/Analytics/Service_UserShiftLogic.cs`

## Phase 2: Foundational
*Goal: Implement backend logic for user shifts and metadata sync.*

- [x] T010 [US2] Implement `CalculateAllUserShiftsAsync` in `Service_UserShiftLogic.cs` (Last 50 transactions logic) `Services/Analytics/Service_UserShiftLogic.cs`
- [x] T011 [US2] Implement `FetchUserFullNamesAsync` in `Service_UserShiftLogic.cs` (Visual DB query) `Services/Analytics/Service_UserShiftLogic.cs`
- [x] T012 [US2] Implement `SaveVisualMetadataAsync` in `Service_UserShiftLogic.cs` (Save to sys_visual) `Services/Analytics/Service_UserShiftLogic.cs`
- [x] T013 [US2] Implement `GetSysVisualDataAsync` in `Dao_VisualAnalytics.cs` `Data/Dao_VisualAnalytics.cs`
- [x] T014 [US2] Implement `UpdateSysVisualDataAsync` in `Dao_VisualAnalytics.cs` `Data/Dao_VisualAnalytics.cs`
- [x] T015 [US2] Add "InforVisual Related" Groupbox to `MainForm` Database Maintenance tab `Forms/MainForm/MainForm.Designer.cs`
- [x] T016 [US2] Wire up "Update User Shifts" button to `Service_UserShiftLogic` `Forms/MainForm/MainForm.cs`
- [x] T017 [US2] Wire up "Update User Names" button to `Service_UserShiftLogic` `Forms/MainForm/MainForm.cs`

## Phase 3: Material Handler Analytics (P1)
*Goal: Implement Fair Grading and new analytics dashboard.*

- [x] T018 [US1] Implement `GetMaterialHandlerStatsAsync` in `Dao_VisualAnalytics.cs` `Data/Dao_VisualAnalytics.cs`
- [x] T019 [US1] Create `Model_Visual_MaterialHandlerScore.cs` `Models/Analytics/Model_Visual_MaterialHandlerScore.cs`
- [x] T020 [US1] Implement scoring logic (1pt/2pt) and ShiftVolumeFactor calculation in `Service_UserShiftLogic.cs` `Services/Analytics/Service_UserShiftLogic.cs`
- [x] T021 [US1] Create new HTML template `MaterialHandlerAnalytics_Enhanced.html` with graphs and grading policy `Resources/Html/MaterialHandlerAnalytics_Enhanced.html`
- [x] T022 [US1] Refactor `Control_MaterialHandlerAnalytics.cs` to use new logic and HTML template `Controls/Visual/Control_MaterialHandlerAnalytics.cs`
- [x] T023 [US1] Remove obsolete tabs (Quality, User Detail, Glossary) from `Control_MaterialHandlerAnalytics.cs` `Controls/Visual/Control_MaterialHandlerAnalytics.cs`
- [x] T024 [US1] Move "MaterialHandlerAnalytics" menu item to View menu in `MainForm` `Forms/MainForm/MainForm.Designer.cs`

## Phase 4: Inventory Audit Enhancements (P2)
*Goal: Improve filtering and user identification.*

- [x] T025 [US3] Add Date Range Radio Buttons to `Control_InventoryAudit.cs` (General Tab) `Controls/Visual/Control_InventoryAudit.cs`
- [x] T026 [US3] Implement logic to disable calendars unless "Custom" is selected `Controls/Visual/Control_InventoryAudit.cs`
- [x] T027 [US3] Implement date preset logic (Today, Week, Month, etc.) `Controls/Visual/Control_InventoryAudit.cs`
- [x] T028 [US3] Update `Control_InventoryAudit.cs` (User Analytics Tab) to use `sys_visual` for full names `Controls/Visual/Control_InventoryAudit.cs`
- [x] T029 [US3] Add Shift Filter Checkboxes to User Analytics Tab `Controls/Visual/Control_InventoryAudit.cs`
- [x] T030 [US3] Add Date Range Radio Buttons to User Analytics Tab `Controls/Visual/Control_InventoryAudit.cs`

## Phase 5: Search & Discovery (P2)
*Goal: Enhance Die Tool Discovery and PO Details.*

- [x] T031 [US4] Update `Control_DieToolDiscovery.cs` search logic to accept Part Number OR FGT `Controls/Visual/Control_DieToolDiscovery.cs`
- [x] T032 [US6] Implement Auto-issue Location lookup in `Control_DieToolDiscovery.cs` (Coil/Flatstock) `Controls/Visual/Control_DieToolDiscovery.cs`
- [x] T033 [US7] Add "Where Used" button to `Control_DieToolDiscovery.cs` `Controls/Visual/Control_DieToolDiscovery.cs`
- [x] T034 [US7] Implement "Where Used" logic (BOM lookup) and DGV display `Controls/Visual/Control_DieToolDiscovery.cs`
- [x] T035 [US5] Refactor `Form_PODetails.cs` layout (TextBoxes + Labels) `Forms/Transactions/Form_PODetails.Designer.cs`
- [x] T036 [US5] Implement Next/Back navigation logic in `Form_PODetails.cs` `Forms/Transactions/Form_PODetails.cs`
- [x] T037 [US5] Add RichTextBox for Line Specs in `Form_PODetails.cs` `Forms/Transactions/Form_PODetails.cs`

## Phase 6: Technical Refactoring & Polish
*Goal: Apply constitution standards and clean up.*

- [x] T038 [Refactor] Replace `MessageBox.Show` with `Service_ErrorHandler` in `MainForm.cs` `Forms/MainForm/MainForm.cs`
- [x] T039 [Refactor] Replace `MessageBox.Show` with `Service_ErrorHandler` in `SettingsForm.cs` `Forms/Settings/SettingsForm.cs`
- [x] T040 [Refactor] Replace `MessageBox.Show` with `Service_ErrorHandler` in `Dialog_EditParameterOverride.cs` `Forms/Shared/Dialog_EditParameterOverride.cs`
- [x] T041 [Refactor] Replace `MessageBox.Show` with `Service_ErrorHandler` in `Form_QuickButtonEdit.cs` `Forms/Settings/Form_QuickButtonEdit.cs`
- [ ] T042 [Refactor] Update `Control_ReceivingAnalytics.cs` to use `SuggestionDataSource` `Controls/Visual/Control_ReceivingAnalytics.cs`
- [ ] T043 [Refactor] Update `Control_VisualInventory.cs` to use `SuggestionDataSource` `Controls/Visual/Control_VisualInventory.cs`
- [ ] T044 [Refactor] Update `Control_InventoryAudit.cs` to use `SuggestionDataSource` `Controls/Visual/Control_InventoryAudit.cs`
- [ ] T045 [Refactor] Update `Control_PartIDManagement.cs` to use `SuggestionDataSource` `Controls/SettingsForm/Control_PartIDManagement.cs`
- [ ] T046 [Refactor] Update `Control_OperationManagement.cs` to use `SuggestionDataSource` `Controls/SettingsForm/Control_OperationManagement.cs`
- [ ] T047 [Refactor] Update `Control_LocationManagement.cs` to use `SuggestionDataSource` `Controls/SettingsForm/Control_LocationManagement.cs`
- [ ] T048 [Refactor] Hide "Help" menu item in `MainForm` `Forms/MainForm/MainForm.Designer.cs`
- [ ] T049 [Refactor] Fix `Control_ReceivingAnalytics.cs` layout (Groupboxes extend to bottom) `Controls/Visual/Control_ReceivingAnalytics.cs`
- [ ] T050 [Refactor] Ensure `Control_ReceivingAnalytics.cs` toggle buttons respect theme animation `Controls/Visual/Control_ReceivingAnalytics.cs`

## Dependencies

1. Phase 1 (Setup) MUST be completed first.
2. Phase 2 (Foundational) MUST be completed before Phase 3 and Phase 4.
3. Phase 3, 4, 5, 6 can be executed in parallel or any order, but Phase 6 (Refactoring) is best done last to avoid merge conflicts with feature work.

## Implementation Strategy

- **MVP**: Complete Phase 1, 2, and 3 (Material Handler Analytics). This delivers the highest priority value.
- **Increment 1**: Phase 4 (Inventory Audit).
- **Increment 2**: Phase 5 (Search & Discovery).
- **Increment 3**: Phase 6 (Refactoring).
