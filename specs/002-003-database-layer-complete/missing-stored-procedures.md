# Missing Stored Procedures Inventory

Generated: 2025-10-17

The entries below come from a repository-wide scan of non-test C# files. Each stored procedure listed here is referenced in code but does not have a matching definition under `UpdatedStoredProcedures/ReadyForVerification/`.

| Procedure | Call Sites | Should Be |
|-----------|-------------|-----------|
| `inv_inventory_Get_All` | Controls/MainForm/Control_RemoveTab.cs | Needs to be created, validate with Caller what needs to be returned |
| `inv_inventory_get_all` | Models/Model_ParameterPrefixCache.cs | inv_inventory_Get_All (needs to be created) |
| `inv_inventory_GetNextBatchNumber` | Data/Dao_Inventory.cs | Needs to be created |
| `inv_transaction_log` | Models/Model_ParameterPrefixCache.cs | is in a comment, not applicable |
| `inv_transactions_Search` | Data/Dao_Transactions.cs | Needs to be created, validate with Caller what needs to be returned |
| `md_item_types_Exists_ByType` | Data/Dao_ItemType.cs | Needs to be created, validate with Caller what needs to be returned |
| `md_item_types_GetDistinct` | Data/Dao_Part.cs | Needs to be created, validate with Caller what needs to be returned |
| `md_locations_Exists_ByLocation` | Data/Dao_Location.cs | Needs to be created, validate with Caller what needs to be returned |
| `md_operation_numbers_Exists_ByOperation` | Controls/SettingsForm/Control_Add_Operation.cs<br>Data/Dao_Operation.cs | Needs to be created, validate with Caller what needs to be returned |
| `md_part_ids_GetItemType_ByPartID` | Data/Dao_Inventory.cs | md_part_ids_Get_ByItemNumber |
| `sys_GetRoleIdByName` | Data/Dao_System.cs | Needs to be created, validate with Caller what needs to be returned |
| `sys_last_10_transactions_Add_AtPosition` | Data/Dao_QuickButtons.cs | Needs to be created, validate with Caller what needs to be returned |
| `sys_last_10_transactions_Delete_ByUserAndPosition_1` | Data/Dao_QuickButtons.cs | Needs to be created, validate with Caller what needs to be returned |
| `sys_last_10_transactions_DeleteAll_ByUser` | Data/Dao_QuickButtons.cs | Needs to be created, validate with Caller what needs to be returned |
| `sys_last_10_transactions_Move` | Data/Dao_QuickButtons.cs | sys_last_10_transactions_Move_1 |
| `usr_ui_settings_Delete_ByUserId` | Data/Dao_User.cs | Needs to be created, validate with Caller what needs to be returned |
| `usr_ui_settings_GetJsonSetting` | Data/Dao_User.cs | Needs to be created, validate with Caller what needs to be returned |
| `usr_user_roles_GetRoleId_ByUserId` | Data/Dao_User.cs | sys_user_roles_Get_ById |
| `usr_users_SetUserSetting_ByUserAndField` | Data/Dao_User.cs | Needs to be created, validate with Caller what needs to be returned |
