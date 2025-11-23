# Stored Procedure User Validation Quality Checklist

**Purpose**: Track validation, testing, and correction of all 75 stored procedures based on the merged discovery artifacts (including `procedure-transaction-analysis.csv` and ReadyForVerification SQL files).

**Usage**: This checklist is processed automatically during Phase 2.5B (T106b) of the database layer implementation. Each stored procedure is validated with the combined instructions from:
- DeveloperCorrection column (specific instructions to follow)
- Notes column (additional context or instructions)
- NeedsAttention flag (requires validation/testing even without explicit instructions)
- Call hierarchy metadata (`call-hierarchy-complete.json`) for edge-case verification

**Completion Requirement**: ALL 75 checkboxes must be checked before proceeding to Phase 3 (DAO refactoring).

---

## Validation Rules

Automation applies the following in order:

1. **Both DeveloperCorrection AND Notes have content**: Follow instructions from both fields.
2. **Only DeveloperCorrection has content**: Follow those specific instructions.
3. **Only Notes has content**: Follow those instructions.
4. **Neither field has content but NeedsAttention = True**: Validate/test using CallHierarchy and ReadyForVerification SQL.
5. **Neither field has content and NeedsAttention = False**: Mark as passing, move to next.

Manual overrides pause execution and request guidance when automation cannot determine the correct action.

---

## Inventory Domain (9 procedures)

- [ ] **inv_inventory_Add_Item** — Pattern: SEQUENTIAL — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_inventory_Fix_BatchNumbers** — Pattern: BATCH_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_inventory_Get_ByPartID** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_inventory_Get_ByPartIDandOperation** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_inventory_Get_ByUser** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_inventory_Remove_Item** — Pattern: MIXED_DML — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_inventory_Search_Advanced** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_inventory_Transfer_Part** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_inventory_Transfer_Quantity** — Pattern: SEQUENTIAL — Status: _Pending validation_ — Notes: _[Automation fills]_ 

---

## Transactions Domain (3 procedures)

- [ ] **inv_transaction_Add** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_transactions_GetAnalytics** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **inv_transactions_SmartSearch** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 

---

## Logging Domain (8 procedures)

- [ ] **log_changelog_Get_Current** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **log_error_Add_Error** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **log_error_Delete_All** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **log_error_Delete_ById** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **log_error_Get_All** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **log_error_Get_ByDateRange** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **log_error_Get_ByUser** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **log_error_Get_Unique** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 

---

## Master Data Domain (18 procedures)

- [ ] **md_item_types_Add_ItemType** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_item_types_Delete_ByID** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_item_types_Delete_ByType** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_item_types_Get_All** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_item_types_Update_ItemType** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_locations_Add_Location** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_locations_Delete_ByLocation** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_locations_Get_All** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_locations_Update_Location** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_operation_numbers_Add_Operation** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_operation_numbers_Delete_ByOperation** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_operation_numbers_Get_All** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_operation_numbers_Update_Operation** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_part_ids_Add_Part** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_part_ids_Delete_ByItemNumber** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_part_ids_Get_All** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_part_ids_Get_ByItemNumber** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **md_part_ids_Update_Part** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 

---

## System Domain (20 procedures)

- [ ] **sys_GetUserAccessType** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_AddQuickButton_1** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_Get_ByUser_1** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_Get_ByUser** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_Move_1** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_MoveToLast_ByUser** — Pattern: BATCH_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_RemoveAndShift_ByUser_1** — Pattern: MIXED_DML — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_RemoveAndShift_ByUser** — Pattern: MIXED_DML — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_Reorder_ByUser** — Pattern: BATCH_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_SwapPositions_ByUser** — Pattern: BATCH_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_Update_ByUserAndDate** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_last_10_transactions_Update_ByUserAndPosition_1** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_role_GetIdByName** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_roles_Get_ById** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_theme_GetAll** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_user_access_SetType** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_user_GetByName** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_user_GetIdByName** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_user_roles_Add** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_user_roles_Delete** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sys_user_roles_Update** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 

---

## Unknown/Maintenance Domain (5 procedures)

- [ ] **maint_InsertMissingUserUiSettings** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **maint_reload_part_ids_and_operation_numbers** — Pattern: BATCH_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **migrate_user_roles_debug** — Pattern: MIXED_DML — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **query_get_all_usernames_and_roles** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **sp_ReassignBatchNumbers** — Pattern: BATCH_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 

---

## Users Domain (12 procedures)

- [ ] **usr_settings_Get** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_settings_GetShortcutsJson** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_settings_SetJsonSetting** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_settings_SetShortcutsJson** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_settings_SetThemeJson** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_users_Add_User** — Pattern: SIMPLE_INSERT — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_users_Delete_User** — Pattern: SIMPLE_DELETE — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_users_Exists** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_users_Get_All** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_users_Get_ByUser** — Pattern: QUERY — Status: _Pending validation_ — Notes: _[Automation fills]_ 
- [ ] **usr_users_Update_User** — Pattern: SIMPLE_UPDATE — Status: _Pending validation_ — Notes: _[Automation fills]_ 

---

## Completion Summary

- **Total Procedures**: 75  
- **Inventory**: 9  
- **Transactions**: 3  
- **Logging**: 8  
- **Master Data**: 18  
- **System**: 20  
- **Unknown/Maintenance**: 5  
- **Users**: 12

**Phase Gate**: Checklist MUST be 100% complete before Phase 3 begins.

---

## Automated Validation Process

1. **Load Artifacts**: CSV, ReadyForVerification SQL, call hierarchy, and compliance reports.
2. **Evaluate Rules**: Apply the validation logic above for each stored procedure.
3. **Execute Tests**: Run required validation scripts/tests per procedure instructions.
4. **Record Outcome**: Update checklist entry and append summary to validation report.
5. **Gate Check**: Confirm all 75 procedures validated; halt with explicit message if any remain.

---

## Manual Override Workflow

Automation stops and requests input when:
- Call hierarchy data conflicts with CSV instructions.
- Stored procedure script missing from ReadyForVerification set.
- Validation result ambiguous (e.g., inconsistent status codes).

Document guidance directly in this checklist or the CSV so the agent can resume confidently.
