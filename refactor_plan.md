# Refactoring Plan for Control_VisualUserAnalytics and Control_InventoryAudit

## Phase 1: Pre-Refactoring Checklist & Analysis (Persona 1: Code Archaeologist)
**Objective:** Inventory, map dependencies, and validate compliance before changes.

- [x] **Control Inventory & Naming Validation**
    - [x] List all controls in `Control_VisualUserAnalytics.Designer.cs`.
    - [x] Verify naming pattern: `{FormOrControlName}_{ControlType}_{Name}`.
    - [x] List all controls in `Control_InventoryAudit.Designer.cs`.
    - [x] Identify renaming requirements for both files.
- [x] **Dependency Mapping**
    - [x] Identify all references to `Control_VisualUserAnalytics` controls in `Control_InventoryAudit.cs`.
    - [x] Identify external dependencies (Services, DAOs, Models).
- [x] **Property Categorization**
    - [x] Identify Design-time properties (Size, Location, Text) vs Runtime properties (DataSource, Items).
- [x] **Error Handling & Logging Audit**
    - [x] Scan for `MessageBox.Show` usage to replace with `Service_ErrorHandler`.
    - [x] Identify missing `Service_LoggingUtility` calls.
- [x] **Theme & Scaling Compliance Check**
    - [x] Verify inheritance from `ThemedUserControl`.
    - [x] Verify `AutoScaleMode = AutoScaleMode.Dpi` and `AutoScaleDimensions = new SizeF(96F, 96F)`.

## Phase 2: Designer Refactoring (Persona 2: Designer Architect)
**Objective:** Standardize `Control_VisualUserAnalytics.Designer.cs` structure and naming.

- [x] **File Structure & Header**
    - [x] Ensure correct namespace and partial class declaration.
    - [x] Verify `components` container existence.
- [x] **Control Renaming (In Designer)**
    - [x] Rename `pnlUserAnalytics` -> `Control_VisualUserAnalytics_TableLayout_Main`.
    - [x] Rename `tableLayoutPanel4` -> `Control_VisualUserAnalytics_TableLayout_Content`.
    - [x] Rename `_flpAnalyticsDateRanges` -> `Control_VisualUserAnalytics_FlowPanel_DateRanges`.
    - [x] Rename `_rbAnalyticsToday` -> `Control_VisualUserAnalytics_RadioButton_Today`.
    - [x] Rename `_rbAnalyticsWeek` -> `Control_VisualUserAnalytics_RadioButton_Week`.
    - [x] Rename `_rbAnalyticsMonth` -> `Control_VisualUserAnalytics_RadioButton_Month`.
    - [x] Rename `_rbAnalyticsCustom` -> `Control_VisualUserAnalytics_RadioButton_Custom`.
    - [x] Rename `lblAnalyticsStart` -> `Control_VisualUserAnalytics_Label_StartDate`.
    - [x] Rename `_dtpAnalyticsStart` -> `Control_VisualUserAnalytics_DateTimePicker_StartDate`.
    - [x] Rename `lblAnalyticsEnd` -> `Control_VisualUserAnalytics_Label_EndDate`.
    - [x] Rename `_dtpAnalyticsEnd` -> `Control_VisualUserAnalytics_DateTimePicker_EndDate`.
    - [x] Rename `_flpAnalyticsShifts` -> `Control_VisualUserAnalytics_FlowPanel_Shifts`.
    - [x] Rename `_cbShift1` -> `Control_VisualUserAnalytics_CheckBox_Shift1`.
    - [x] Rename `_cbShift2` -> `Control_VisualUserAnalytics_CheckBox_Shift2`.
    - [x] Rename `_cbShift3` -> `Control_VisualUserAnalytics_CheckBox_Shift3`.
    - [x] Rename `_cbShiftWeekend` -> `Control_VisualUserAnalytics_CheckBox_ShiftWeekend`.
    - [x] Rename `_btnLoadUsers` -> `Control_VisualUserAnalytics_Button_LoadUsers`.
    - [x] Rename `_btnSelectAllUsers` -> `Control_VisualUserAnalytics_Button_SelectAllUsers`.
    - [x] Rename `_btnGenerateReport` -> `Control_VisualUserAnalytics_Button_GenerateReport`.
    - [x] Rename `_lblUserCount` -> `Control_VisualUserAnalytics_Label_UserCount`.
    - [x] Rename `groupBox1` -> `Control_VisualUserAnalytics_GroupBox_Instructions`.
    - [x] Rename `_ProcessUserAnalytics` -> `Control_VisualUserAnalytics_CheckedListBox_Instructions`.
    - [x] Rename `_clbUsers` -> `Control_VisualUserAnalytics_CheckedListBox_Users`.
- [x] **Property Purification**
    - [x] Ensure NO runtime properties (DataSource, Items.Add) in `InitializeComponent`.
    - [x] Ensure `Dispose` pattern is correct.
- [x] **UI Scaling Enforcement**
    - [x] Set `AutoScaleMode = AutoScaleMode.Dpi`.
    - [x] Set `AutoScaleDimensions = new SizeF(96F, 96F)`.

## Phase 3: Code Separation & Logic Migration (Persona 4: Code Separator)
**Objective:** Move logic from `Control_InventoryAudit.cs` to `Control_VisualUserAnalytics.cs`.

- [x] **Setup Target File (`Control_VisualUserAnalytics.cs`)**
    - [x] Inherit from `ThemedUserControl`.
    - [x] Add required `using` statements.
- [x] **Migrate Fields & Dependencies**
    - [x] Move `_userShifts`, `_userNames`.
    - [x] Inject/Initialize `IService_VisualDatabase`, `IDao_VisualAnalytics`, `IService_UserShiftLogic`.
- [x] **Migrate Methods & Logic**
    - [x] Move `LoadUsersForAnalyticsAsync`.
    - [x] Move `IsUserMatch`.
    - [x] Move `UpdateUserSelectionState`.
    - [x] Move `SelectAllUsers`.
    - [x] Move `GenerateAnalyticsReportAsync`.
    - [x] Move `UpdateAnalyticsWorkflow`.
    - [x] Move `SetWorkflowStep`.
    - [x] Move `OnAnalyticsDateRangeChanged` & `SetDateRange`.
- [x] **Update References**
    - [x] Update all code to use new control names from Phase 2.
    - [x] Ensure `Service_ErrorHandler` is used for all exceptions.
    - [x] Ensure `LoggingUtility` is used for tracing.
- [x] **Constructor Purification**
    - [x] Ensure constructor only calls `InitializeComponent` and basic setup.
    - [x] Move heavy initialization to `Load` event or async method.

## Phase 4: Cleanup Control_InventoryAudit (Persona 4: Code Separator)
**Objective:** Remove migrated logic and standardize remaining code.

- [ ] **Remove Migrated Code**
    - [ ] Delete moved fields and methods from `Control_InventoryAudit.cs`.
    - [ ] Remove `_tabUserAnalytics` references.
- [ ] **Refactor Designer (`Control_InventoryAudit.Designer.cs`)**
    - [ ] Rename remaining controls to `Control_InventoryAudit_{ControlType}_{Name}`.
    - [ ] Enforce UI Scaling (`Dpi`, `96F`).
- [ ] **Update Code Behind (`Control_InventoryAudit.cs`)**
    - [ ] Update references to renamed controls.
    - [ ] Fix `PerformSearchAsync` (remove analytics branch).
    - [ ] Verify `Service_ErrorHandler` usage.

## Phase 5: Access Control & Encapsulation (Persona 3: Access Control Specialist)
**Objective:** Secure the controls.

- [ ] **Access Modifier Review**
    - [ ] Ensure all UI controls are `private` or `protected` (if needed for inheritance).
    - [ ] Create public properties/methods if external access is required (Encapsulation).
- [ ] **User Privilege Enforcement**
    - [ ] Implement `ApplyPrivileges` method in `Control_VisualUserAnalytics.cs` (Reference: `Control_InventoryTab.cs`).
    - [ ] Restrict access to Admin and Developer roles only (`Model_Application_Variables.UserTypeAdmin || Model_Application_Variables.UserTypeDeveloper`).
    - [ ] Hide/Disable main layout or show "Access Denied" message if user lacks privileges.

## Phase 6: Quality Verification (Persona 5: Quality Validator)
**Objective:** Verify the refactoring.

- [ ] **Compilation Check**
    - [ ] Build project to ensure no errors.
- [ ] **Designer Check**
    - [ ] Open `Control_VisualUserAnalytics` in Designer.
    - [ ] Open `Control_InventoryAudit` in Designer.
- [ ] **Functional Check**
    - [ ] Verify User Analytics features work in new control.
    - [ ] Verify Inventory Audit features still work.

