# **Settings Form SQL/Stored Procedure Fix TODO List**

Must read through "C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-02-2025 @ 2-49 PM_normal.log" and "C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-02-2025 @ 2-42 PM_normal.log" before each task


## **📋 Controls/SettingsForm - User Management (5 tasks)**

- [ ] **Task 1: Fix Control_Add_User.cs**
- **Issue**: Uses `Dao_User.UserExistsAsync()`, `Dao_User.CreateUserAsync()`, `Dao_User.GetUserByUsernameAsync()`, `Dao_User.AddUserRoleAsync()` - need to verify these call correct stored procedures with proper parameter naming
- **Issue #2**: Need to add Developer under User Privileges (User will add the UI elements, but AI must wire up and do codebehind to give the new user the developer role if that radio button is selected)
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.
- **Resolution**: 
    - Verified all DAO methods call correct stored procedures: `usr_users_Exists`, `usr_users_Add_User`, `usr_users_Get_ByUser`, `sys_user_roles_Add`
    - Added Developer role (roleId=4) support in `Control_Add_User.cs` Button_Save_Click handler
    - Added Developer role (roleId=4) support in `Control_Edit_User.cs` Button_Save_Click handler and LoadUserData
    - Build succeeded with no errors

- [ ] **Task 2: Fix `Control_Edit_User.cs`**
- **Issue**: Uses DAO methods for user updates/queries - verify stored procedure calls
- **Issue #2**: Need to add Developer inside User Privileges Groupbox (User will add the UI elements, but AI must wire up and do codebehind for changing user's role to Developer if Developer Radio Box is selected)
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.
- **Resolution**:
    - Verified all DAO methods call correct stored procedures
    - Added Developer role (roleId=4) support in LoadUserData method
    - Added Developer role (roleId=4) support in Button_Save_Click handler

- [ ] **Task 3: Fix `Control_Remove_User.cs`**
- **Issue**: Uses DAO methods for user deletion - verify stored procedure calls
- **Validate**: Check delete/soft-delete stored procedures
- **Fix**: Ensure proper error handling with `DaoResult` pattern
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

---

## **📋 Controls/SettingsForm - ItemType Management (3 tasks)**

- [ ] **Task 4: Fix Control_Add_ItemType.cs**
- **Issue**: Uses `Dao_ItemType.ItemTypeExists()` and `Dao_ItemType.InsertItemType()` - need to verify stored procedure calls
- **Validate**: Check stored procedures: `md_item_types_Exists_ByItemType`, `md_item_types_Add_ItemType`
- **Fix**: Ensure DAO methods properly call stored procedures with status/error handling
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 5: Fix Control_Edit_ItemType.cs**
- **Issue**: Uses `Dao_ItemType.GetItemTypeByName()`, `Dao_ItemType.ItemTypeExists()`, `Dao_ItemType.UpdateItemType()`
- **Validate**: Check stored procedures for ItemType retrieval/update
- **Fix**: Verify parameter naming (`p_ItemType`, `p_ID`, etc.)
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 6: Fix `Control_Remove_ItemType.cs`**
- **Issue**: Uses DAO methods for ItemType removal
- **Validate**: Check delete stored procedure
- **Fix**: Ensure proper cascading/reference checks before delete
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

---

## **📋 Controls/SettingsForm - Location Management (3 tasks)**

- [ ] **Task 7: Fix Control_Add_Location.cs**
- **Issue**: Uses `Dao_Location.LocationExists()` and `Dao_Location.InsertLocation()` - verify stored procedures
- **Validate**: Check stored procedures: `md_locations_Exists_ByLocation`, `md_locations_Add_Location`
- **Fix**: Ensure proper parameter passing (Location, Building, IssuedBy)
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 8: Fix Control_Edit_Location.cs**
- **Issue**: Uses `Dao_Location.GetLocationByName()`, `Dao_Location.LocationExists()`, `Dao_Location.UpdateLocation()`
- **Validate**: Verify Location retrieval/update stored procedures
- **Fix**: Check that `UpdateLocation` passes correct parameters with `p_` prefix
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 9: Fix `Control_Remove_Location.cs`**
- **Issue**: Uses DAO methods for Location deletion
- **Validate**: Check if location removal checks for active inventory
- **Fix**: Add validation before deletion
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

---

## **📋 Controls/SettingsForm - Operation Management (3 tasks)**

- [ ] **Task 10: Fix Control_Add_Operation.cs**
- **Issue**: Uses `Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync` directly with `md_operation_numbers_Exists_ByOperation` and `md_operation_numbers_Add_Operation`
- **Validate**: Verify stored procedures exist and accept correct parameters (`p_Operation`, `IssuedBy`)
- **Fix**: Check parameter naming - should use `p_Operation` not just `Operation`
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 11: Fix Control_Edit_Operation.cs**
- **Issue**: Uses `Dao_Operation.GetOperationByNumber()`, `Dao_Operation.OperationExists()`, `Dao_Operation.UpdateOperation()`
- **Validate**: Verify Operation update stored procedure
- **Fix**: Ensure column name matches (`p_Operation` vs `Operation`)
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 12: Fix `Control_Remove_Operation.cs`**
- **Issue**: Uses DAO methods for Operation removal
- **Validate**: Check removal stored procedure
- **Fix**: Verify cascade/reference checks
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

---

## **📋 Controls/SettingsForm - Part Management (3 tasks)**

- [ ] **Task 13: Fix Control_Add_PartID.cs**
- **Issue**: Uses `Dao_Part.PartExistsAsync()` and `Dao_Part.CreatePartAsync()` - verify stored procedures
- **Issue #2**: Type Combobox sould defalt to "WIP", yes it pulls this from the database but the WIP row should exists so it should default to it
- **Validate**: Check stored procedures: `md_part_ids_Exists_ByPartNumber`, `md_part_ids_Add_Part`
- **Fix**: Ensure ItemType is passed correctly
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 14: Fix `Control_Edit_PartID.cs`**
- **Issue**: Uses DAO methods for Part retrieval/update
- **Validate**: Check Part edit stored procedures
- **Fix**: Verify ItemType, Description, IssuedBy parameters
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 15: Fix `Control_Remove_PartID.cs`**
- **Issue**: Uses DAO methods for Part deletion
- **Validate**: Check if part removal validates no active inventory
- **Fix**: Add usage checks before deletion
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

---

## **📋 Controls/SettingsForm - Other Settings (5 tasks)**

- [ ] **Task 16: Fix `Control_Theme.cs`**
- **Issue**: Uses `Dao_User` methods for theme operations
- **Validate**: Check theme retrieval/save stored procedures
- **Fix**: Ensure theme name updates propagate correctly
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 17: Fix `Control_Shortcuts.cs`**
- **Issue**: Uses `Dao_User` methods for shortcut management
- **Validate**: Check shortcut persistence stored procedures
- **Fix**: Verify JSON serialization/deserialization for shortcut storage
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 18: Fix Control_Database.cs**
- **Issue**: Connection string testing and database settings
- **Validate**: No stored procedures, but verify connection validation logic
- **Fix**: Ensure proper error messages for connection failures
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 19: Fix Control_About.cs**
- **Issue**: Version display and system info retrieval
- **Validate**: Check if any database queries are used
- **Fix**: Minimal - likely no SQL issues
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 20: Fix Control_Developer_ParameterPrefixMaintenance.cs**
- **Issue**: Uses `Dao_ParameterPrefixOverrides` methods
- **Validate**: Check parameter override CRUD stored procedures
- **Fix**: Ensure override table updates work correctly
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

---

## **📋 Forms/Settings - Dialogs (2 tasks)**

- [ ] **Task 21: Fix Dialog_AddParameterOverride.cs**
- **Issue**: Uses stored procedures for parameter override addition
- **Validate**: Check `sys_parameter_prefix_overrides_Add` stored procedure
- **Fix**: Verify procedure name, prefix, override value parameters
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

- [ ] **Task 22: Fix Dialog_EditParameterOverride.cs**
- **Issue**: Uses stored procedures for parameter override editing
- **Validate**: Check `sys_parameter_prefix_overrides_Update` stored procedure
- **Fix**: Ensure ID and updated values pass correctly
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

---

## **📋 Forms/Settings - Main Form (1 task)**

- [ ] **Task 23: Fix SettingsForm.cs**
- **Issue**: Main container form that hosts all settings controls
- **Validate**: Check for any direct stored procedure calls
- **Fix**: Ensure proper coordination between child controls and progress reporting
- **Error Handling Check**: Verify all DAO calls use `Service_ErrorHandler.HandleDatabaseError()` for database exceptions and `Service_ErrorHandler.HandleException()` for general errors, replacing any `MessageBox.Show()`. Ensure `LoggingUtility.LogDatabaseError()` or `LoggingUtility.LogApplicationError()` is used appropriately. Check compliance with #file:database-patterns.memory.instructions.md for connection pooling, retry logic, and stored procedure patterns, and #file:database-ui-integration.memory.instructions.md for safe column access and DataTable mapping.

---

## **🎯 Priority Order**

**High Priority (Do First):**
1. Task 1 - Control_Add_User (most complex, creates users)
2. Task 10 - Control_Add_Operation (already uses Helper directly, verify parameters)
3. Task 13 - Control_Add_PartID (critical for inventory management)

**Medium Priority:**
4. Tasks 2-3, 5-6, 8-9, 11-12, 14-15 (Edit/Remove controls)

**Low Priority:**
5. Tasks 16-23 (Theme, Shortcuts, About, Dialogs)

---

## **🔍 Validation Strategy for Each Task**

For each file, follow this workflow:

1. **Read the file** - Identify all DAO method calls
2. **Check corresponding DAO** - Find the actual stored procedure name being called
3. **Verify stored procedure exists** - Check ReadyForVerification
4. **Validate parameters** - Ensure parameter names match stored procedure expectations (with `p_` prefix in MySQL, without in C# Dictionary)
5. **Test execution** - Run the control and verify no SQL errors occur
6. **Check error handling** - Ensure `DaoResult` pattern is used with proper `IsSuccess` checks
7. **Update if needed** - Fix parameter mismatches, missing stored procedures, or error handling issues
8. **Error Handling Verification** - Confirm `Service_ErrorHandler` is used for all exceptions, `LoggingUtility` for logging, and compliance with database patterns and UI integration memory files

---

**Total Tasks: 23 files to investigate, validate, and fix**

Would you like me to start with Task 1 (Control_Add_User.cs) and work through them systematically?