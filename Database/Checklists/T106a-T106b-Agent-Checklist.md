# T106a & T106b Agent Checklist

**Purpose:** Ensure the transaction analysis review (T106a) and stored procedure validation sweep (T106b) finish with no gaps before moving to Part B/C work.

---

## T106a – Transaction Analysis CSV Review

- [x] Pull latest discovery artifacts (call-site inventory, compliance report, transaction analysis CSV) into working session.
- [x] Spot-check high-priority procedures (top 10 from `refactoring-priority.csv`) for pattern/strategy accuracy.
- [x] Verify `DetectedPattern`, `RecommendedStrategy`, and `ProcedureType` align with DML counts and helper expectations.
- [x] Confirm `HasTransactionHandling` matches procedure implementation (presence of explicit transactions).
- [x] Validate `HasValidation` flag against stored procedure parameter checks/early exits.
- [x] Populate `DeveloperCorrection` notes for any corrections applied.
- [ ] Save updated `procedure-transaction-analysis.csv` with corrections.
- [ ] Archive findings in session notes or relevant documentation if major adjustments were required.

## T106b – Stored Procedure User Validation Checklist

- [ ] Ensure ReadyForVerification SQL set (all 75 procedures) is current (`UpdatedStoredProcedures/ReadyForVerification`).
  - [ ] `inventory/inv_inventory_Add_Item.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `inventory/inv_inventory_Fix_BatchNumbers.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `inventory/inv_inventory_Get_ByPartID.sql`
  - [ ] `inventory/inv_inventory_Get_ByPartIDandOperation.sql`
  - [ ] `inventory/inv_inventory_Get_ByUser.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `inventory/inv_inventory_Remove_Item.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `inventory/inv_inventory_Search_Advanced.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `inventory/inv_inventory_Transfer_Part.sql`
  - [ ] `inventory/inv_inventory_Transfer_Quantity.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `inventory/inv_transaction_Add.sql`
  - [ ] `inventory/inv_transactions_GetAnalytics.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `inventory/inv_transactions_SmartSearch.sql`
  - [ ] `logging/log_changelog_Get_Current.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `logging/log_error_Add_Error.sql`
  - [ ] `logging/log_error_Delete_All.sql`
  - [ ] `logging/log_error_Delete_ById.sql`
  - [ ] `logging/log_error_Get_All.sql`
  - [ ] `logging/log_error_Get_ByDateRange.sql`
  - [ ] `logging/log_error_Get_ByUser.sql`
  - [ ] `logging/log_error_Get_Unique.sql`
  - [ ] `master-data/md_item_types_Add_ItemType.sql`
  - [ ] `master-data/md_item_types_Delete_ByID.sql`
  - [ ] `master-data/md_item_types_Delete_ByType.sql`
  - [ ] `master-data/md_item_types_Get_All.sql`
  - [ ] `master-data/md_item_types_Update_ItemType.sql`
  - [ ] `master-data/md_locations_Add_Location.sql`
  - [ ] `master-data/md_locations_Delete_ByLocation.sql`
  - [ ] `master-data/md_locations_Get_All.sql`
  - [ ] `master-data/md_locations_Update_Location.sql`
  - [ ] `master-data/md_operation_numbers_Add_Operation.sql`
  - [ ] `master-data/md_operation_numbers_Delete_ByOperation.sql`
  - [ ] `master-data/md_operation_numbers_Get_All.sql`
  - [ ] `master-data/md_operation_numbers_Update_Operation.sql`
  - [ ] `master-data/md_part_ids_Add_Part.sql`
  - [ ] `master-data/md_part_ids_Delete_ByItemNumber.sql`
  - [ ] `master-data/md_part_ids_Get_All.sql`
  - [ ] `master-data/md_part_ids_Get_ByItemNumber.sql`
  - [ ] `master-data/md_part_ids_Update_Part.sql`
  - [ ] `system/sys_GetUserAccessType.sql`
  - [ ] `system/sys_last_10_transactions_AddQuickButton_1.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `system/sys_last_10_transactions_Get_ByUser_1.sql`
  - [ ] `system/sys_last_10_transactions_Get_ByUser.sql`
  - [ ] `system/sys_last_10_transactions_Move_1.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `system/sys_last_10_transactions_MoveToLast_ByUser.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `system/sys_last_10_transactions_RemoveAndShift_ByUser_1.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `system/sys_last_10_transactions_RemoveAndShift_ByUser.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `system/sys_last_10_transactions_Reorder_ByUser.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `system/sys_last_10_transactions_SwapPositions_ByUser.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `system/sys_last_10_transactions_Update_ByUserAndDate.sql`
  - [ ] `system/sys_last_10_transactions_Update_ByUserAndPosition_1.sql`
  - [ ] `system/sys_role_GetIdByName.sql`
  - [ ] `system/sys_roles_Get_ById.sql`
  - [ ] `system/sys_theme_GetAll.sql`
  - [ ] `system/sys_user_access_SetType.sql`
  - [ ] `system/sys_user_GetByName.sql`
  - [ ] `system/sys_user_GetIdByName.sql`
  - [ ] `system/sys_user_roles_Add.sql`
  - [ ] `system/sys_user_roles_Delete.sql`
  - [ ] `system/sys_user_roles_Update.sql`
  - [ ] `uncategorized/maint_InsertMissingUserUiSettings.sql`
  - [ ] `uncategorized/maint_reload_part_ids_and_operation_numbers.sql` — NOTE: CSV updated to match auto-analysis
  - [ ] `uncategorized/migrate_user_roles_debug.sql`
  - [ ] `uncategorized/query_get_all_usernames_and_roles.sql`
  - [ ] `uncategorized/sp_ReassignBatchNumbers.sql`
  - [ ] `users/usr_ui_settings_Get.sql`
  - [ ] `users/usr_ui_settings_GetShortcutsJson.sql`
  - [ ] `users/usr_ui_settings_SetJsonSetting.sql`
  - [ ] `users/usr_ui_settings_SetShortcutsJson.sql`
  - [ ] `users/usr_ui_settings_SetThemeJson.sql`
  - [ ] `users/usr_users_Add_User.sql`
  - [ ] `users/usr_users_Delete_User.sql`
  - [ ] `users/usr_users_Exists.sql`
  - [ ] `users/usr_users_Get_All.sql`
  - [ ] `users/usr_users_Get_ByUser.sql`
  - [ ] `users/usr_users_Update_User.sql`
- [ ] Establish connection to test database (`mtm_wip_application_winforms_test`).
- [ ] For each procedure:
  - [ ] Execute the script; verify `p_Status` / `p_ErrorMsg` outputs and expected data changes.
  - [ ] Capture supporting evidence (result set snapshots, logs) when behavior deviates from expectations.
  - [ ] Update the user validation checklist with pass/fail status and notes.
- [ ] Confirm no lingering temp tables or schema drift after execution.
- [ ] Submit compiled validation checklist and supporting artifacts for stakeholder review.

---

**Reminder:** Both tasks gate refactoring work (T113+). Do not advance until every item above is checked off and artifacts are stored in version control.
