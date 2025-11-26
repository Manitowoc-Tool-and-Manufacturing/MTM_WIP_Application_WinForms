# MySQL Database Schema: Stored Procedures

This document outlines the stored procedures in the `mtm_wip_application_winforms` database, categorized by function.

## Inventory Operations (`inv_`)

Core procedures for managing inventory and transactions.

- `inv_inventory_Add_Item`: Adds a new item to inventory.
- `inv_inventory_Fix_BatchNumbers`: Utility to fix batch number issues.
- `inv_inventory_GetNextBatchNumber`: Generates the next batch sequence.
- `inv_inventory_Get_All`: Retrieves all current inventory.
- `inv_inventory_Get_ByPartID`: Retrieves inventory by Part ID.
- `inv_inventory_Get_ByPartIDandOperation`: Retrieves inventory by Part ID and Operation.
- `inv_inventory_Get_ByUser`: Retrieves inventory created by a specific user.
- `inv_inventory_Remove_Item`: Removes an item from inventory.
- `inv_inventory_Search_Advanced`: Advanced search functionality.
- `inv_inventory_Transfer_Part`: Transfers a part between locations.
- `inv_inventory_Transfer_Quantity`: Transfers a specific quantity.
- `inv_transactions_GetAnalytics`: Retrieves transaction analytics.
- `inv_transactions_GetBatchLifecycle`: Traces the lifecycle of a batch.
- `inv_transactions_Search`: Basic transaction search.
- `inv_transactions_SmartSearch`: Intelligent transaction search.
- `inv_transaction_Add`: Records a new transaction.
- `inv_transaction_Delete_ByID`: Deletes a transaction by ID.
- `inv_transaction_Fix_Duplicates_Analysis`: Analyzes duplicate transactions.
- `inv_transaction_Fix_IN_ColumnSwap`: Fixes column swap issues in IN transactions.

## Master Data Management (`md_`)

CRUD operations for master data entities.

- `md_color_codes_Add`: Adds a new color code.
- `md_color_codes_GetAll`: Retrieves all color codes.
- `md_item_types_Add_ItemType`: Adds a new item type.
- `md_item_types_Delete_ByID`: Deletes an item type by ID.
- `md_item_types_Delete_ByType`: Deletes an item type by name.
- `md_item_types_Exists_ByType`: Checks if an item type exists.
- `md_item_types_GetDistinct`: Retrieves distinct item types.
- `md_item_types_Get_All`: Retrieves all item types.
- `md_item_types_Update_ItemType`: Updates an item type.
- `md_locations_Add_Location`: Adds a new location.
- `md_locations_Delete_ByLocation`: Deletes a location.
- `md_locations_Exists_ByLocation`: Checks if a location exists.
- `md_locations_Get_All`: Retrieves all locations.
- `md_locations_Update_Location`: Updates a location.
- `md_operation_numbers_Add_Operation`: Adds a new operation.
- `md_operation_numbers_Delete_ByOperation`: Deletes an operation.
- `md_operation_numbers_Exists_ByOperation`: Checks if an operation exists.
- `md_operation_numbers_Get_All`: Retrieves all operations.
- `md_operation_numbers_Update_Operation`: Updates an operation.
- `md_part_ids_Add_Part`: Adds a new part.
- `md_part_ids_Delete_ByItemNumber`: Deletes a part by item number.
- `md_part_ids_GetAllColorCodeFlagged`: Retrieves parts requiring color codes.
- `md_part_ids_Get_All`: Retrieves all parts.
- `md_part_ids_Get_ByItemNumber`: Retrieves a part by item number.
- `md_part_ids_UpdateColorCodeFlag`: Updates the color code requirement flag.
- `md_part_ids_Update_Part`: Updates part details.

## User Management (`usr_`)

User accounts, settings, and shortcuts.

- `usr_dgv_settings_Get`: Retrieves DataGridView settings.
- `usr_dgv_settings_Set`: Saves DataGridView settings.
- `usr_settings_Delete_ByUserId`: Deletes all settings for a user.
- `usr_settings_Get`: Retrieves user settings JSON.
- `usr_settings_GetJsonSetting`: Retrieves a specific JSON setting.
- `usr_settings_Get_All`: Retrieves all user settings.
- `usr_settings_SaveFullJson`: Saves the full settings JSON.
- `usr_settings_SetJsonSetting`: Sets a specific JSON setting.
- `usr_settings_SetThemeJson`: Sets theme-related JSON settings.
- `usr_settings_SetUserSetting_ByUserAndField`: Legacy setting update.
- `usr_users_Add_User`: Creates a new user.
- `usr_users_Delete_User`: Deletes a user.
- `usr_users_Exists`: Checks if a user exists.
- `usr_users_Get_All`: Retrieves all users.
- `usr_users_Get_ByUser`: Retrieves a specific user.
- `usr_users_SetUserSetting_ByUserAndField`: Updates a user field.
- `usr_users_Update_User`: Updates user details.
- `usr_user_shortcuts_GetByUser`: Retrieves shortcuts for a user.
- `usr_user_shortcuts_Reset`: Resets user shortcuts to defaults.
- `usr_user_shortcuts_Upsert`: Updates or inserts a user shortcut.

## System & Configuration (`sys_`)

System-wide settings, roles, and configuration.

- `sys_GetRoleIdByName`: Retrieves a role ID by name.
- `sys_GetUserAccessType`: Determines a user's access level.
- `sys_last_10_transactions_*`: Procedures for managing the "Quick Buttons" / recent transactions cache.
- `sys_parameter_prefix_overrides_*`: CRUD for parameter prefix overrides.
- `sys_roles_Get_ById`: Retrieves a role by ID.
- `sys_role_GetIdByName`: Retrieves a role ID by name.
- `sys_shortcuts_GetAll`: Retrieves system default shortcuts.
- `sys_theme_GetAll`: Retrieves all available themes.
- `sys_user_access_SetType`: Sets a user's access type.
- `sys_user_GetByName`: Retrieves a user by name (system context).
- `sys_user_GetIdByName`: Retrieves a user ID by name.
- `sys_user_roles_Add`: Assigns a role to a user.
- `sys_user_roles_Delete`: Removes a role from a user.
- `sys_user_roles_Get_ById`: Retrieves roles for a user.
- `sys_user_roles_Update`: Updates a user's role.

## Logging & Error Reporting (`log_`, `sp_error_`)

- `log_changelog_Get_Current`: Retrieves the current version changelog.
- `log_error_Add_Error`: Logs a new application error.
- `log_error_Delete_All`: Clears the error log.
- `log_error_Delete_ById`: Deletes a specific error log entry.
- `log_error_Get_All`: Retrieves all error logs.
- `log_error_Get_ByDateRange`: Retrieves error logs by date.
- `log_error_Get_ByUser`: Retrieves error logs by user.
- `log_error_Get_Unique`: Retrieves unique error types.
- `sp_error_reports_Delete`: Deletes an error report.
- `sp_error_reports_GetAll`: Retrieves all error reports.
- `sp_error_reports_GetByID`: Retrieves an error report by ID.
- `sp_error_reports_GetMachineList`: Retrieves distinct machines from reports.
- `sp_error_reports_GetUserList`: Retrieves distinct users from reports.
- `sp_error_reports_Insert`: Creates a new error report.
- `sp_error_reports_UpdateStatus`: Updates the status of an error report.

## Maintenance & Utilities (`maint_`, `query_`, `migrate_`)

- `maint_InsertMissingUserUiSettings`: Backfills missing UI settings.
- `maint_reload_part_ids_and_operation_numbers`: Refreshes master data caches.
- `maint_transactions_FindDuplicates`: Identifies duplicate transactions.
- `maint_transactions_RemoveDuplicates`: Removes duplicate transactions.
- `migrate_user_roles_debug`: Debug utility for role migration.
- `query_check_procedure_exists`: Checks if a stored procedure exists.
- `query_get_all_stored_procedures`: Lists all stored procedures.
- `query_get_all_usernames_and_roles`: Audits user roles.
- `query_get_procedure_parameters`: Retrieves parameters for a procedure.
- `sp_ReassignBatchNumbers`: Utility to reassign batch numbers.
