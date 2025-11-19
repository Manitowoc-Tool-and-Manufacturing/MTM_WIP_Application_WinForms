# Stored Procedure User Validation Quality Checklist

**Purpose**: Track validation, testing, and correction of all 75 stored procedures based on user instructions in the procedure-transaction-analysis.csv file.

**Usage**: This checklist is processed automatically during Phase 2.5B (T106b) of the database layer implementation. Each stored procedure is validated based on:
- Developer Correction column (specific instructions to follow)
- Notes column (additional context or instructions)
- NeedsAttention flag (requires validation/testing even without explicit instructions)

**Completion Requirement**: ALL 75 checkboxes must be checked before proceeding to Phase 3 (DAO refactoring).

---

## Validation Rules

For each stored procedure, the automated validation follows this logic:

1. **Both DeveloperCorrection AND Notes have content**: Follow instructions from both fields
2. **Only DeveloperCorrection has content**: Follow those specific instructions
3. **Only Notes has content**: Follow those instructions
4. **Neither has content but NeedsAttention = True**: Validate/test using CallHierarchy data
5. **Neither has content and NeedsAttention = False**: Mark as passing, move to next

---

## Inventory Domain (9 procedures)

- [ ] **inv_inventory_Add_Item**
  - Domain: Inventory
  - Pattern: SEQUENTIAL
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_inventory_Fix_BatchNumbers**
  - Domain: Inventory
  - Pattern: BATCH_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_inventory_Get_ByPartID**
  - Domain: Inventory
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_inventory_Get_ByPartIDandOperation**
  - Domain: Inventory
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_inventory_Get_ByUser**
  - Domain: Inventory
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_inventory_Remove_Item**
  - Domain: Inventory
  - Pattern: MIXED_DML
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_inventory_Search_Advanced**
  - Domain: Inventory
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_inventory_Transfer_Part**
  - Domain: Inventory
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_inventory_Transfer_Quantity**
  - Domain: Inventory
  - Pattern: SEQUENTIAL
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

---

## Transactions Domain (3 procedures)

- [ ] **inv_transaction_Add**
  - Domain: Transactions
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_transactions_GetAnalytics**
  - Domain: Transactions
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **inv_transactions_SmartSearch**
  - Domain: Transactions
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

---

## Logging Domain (8 procedures)

- [ ] **log_changelog_Get_Current**
  - Domain: Logging
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **log_error_Add_Error**
  - Domain: Logging
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **log_error_Delete_All**
  - Domain: Logging
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **log_error_Delete_ById**
  - Domain: Logging
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **log_error_Get_All**
  - Domain: Logging
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **log_error_Get_ByDateRange**
  - Domain: Logging
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **log_error_Get_ByUser**
  - Domain: Logging
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **log_error_Get_Unique**
  - Domain: Logging
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

---

## Master Data Domain (18 procedures)

- [ ] **md_item_types_Add_ItemType**
  - Domain: Master Data
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_item_types_Delete_ByID**
  - Domain: Master Data
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_item_types_Delete_ByType**
  - Domain: Master Data
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_item_types_Get_All**
  - Domain: Master Data
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_item_types_Update_ItemType**
  - Domain: Master Data
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_locations_Add_Location**
  - Domain: Master Data
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_locations_Delete_ByLocation**
  - Domain: Master Data
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_locations_Get_All**
  - Domain: Master Data
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_locations_Update_Location**
  - Domain: Master Data
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_operation_numbers_Add_Operation**
  - Domain: Master Data
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_operation_numbers_Delete_ByOperation**
  - Domain: Master Data
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_operation_numbers_Get_All**
  - Domain: Master Data
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_operation_numbers_Update_Operation**
  - Domain: Master Data
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_part_ids_Add_Part**
  - Domain: Master Data
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_part_ids_Delete_ByItemNumber**
  - Domain: Master Data
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_part_ids_Get_All**
  - Domain: Master Data
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_part_ids_Get_ByItemNumber**
  - Domain: Master Data
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **md_part_ids_Update_Part**
  - Domain: Master Data
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

---

## System Domain (20 procedures)

- [ ] **sys_GetUserAccessType**
  - Domain: System
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_AddQuickButton_1**
  - Domain: System
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_Get_ByUser_1**
  - Domain: System
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_Get_ByUser**
  - Domain: System
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_Move_1**
  - Domain: System
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_MoveToLast_ByUser**
  - Domain: System
  - Pattern: BATCH_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_RemoveAndShift_ByUser_1**
  - Domain: System
  - Pattern: MIXED_DML
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_RemoveAndShift_ByUser**
  - Domain: System
  - Pattern: MIXED_DML
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_Reorder_ByUser**
  - Domain: System
  - Pattern: BATCH_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_SwapPositions_ByUser**
  - Domain: System
  - Pattern: BATCH_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_Update_ByUserAndDate**
  - Domain: System
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_last_10_transactions_Update_ByUserAndPosition_1**
  - Domain: System
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_role_GetIdByName**
  - Domain: System
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_roles_Get_ById**
  - Domain: System
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_theme_GetAll**
  - Domain: System
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_user_access_SetType**
  - Domain: System
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_user_GetByName**
  - Domain: System
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_user_GetIdByName**
  - Domain: System
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_user_roles_Add**
  - Domain: System
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_user_roles_Delete**
  - Domain: System
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sys_user_roles_Update**
  - Domain: System
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

---

## Unknown/Maintenance Domain (5 procedures)

- [ ] **maint_InsertMissingUserUiSettings**
  - Domain: Unknown
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **maint_reload_part_ids_and_operation_numbers**
  - Domain: Unknown
  - Pattern: BATCH_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **migrate_user_roles_debug**
  - Domain: Unknown
  - Pattern: MIXED_DML
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **query_get_all_usernames_and_roles**
  - Domain: Unknown
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **sp_ReassignBatchNumbers**
  - Domain: Unknown
  - Pattern: BATCH_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

---

## Users Domain (12 procedures)

- [ ] **usr_settings_Get**
  - Domain: Users
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_settings_GetShortcutsJson**
  - Domain: Users
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_settings_SetJsonSetting**
  - Domain: Users
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_settings_SetShortcutsJson**
  - Domain: Users
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_settings_SetThemeJson**
  - Domain: Users
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_users_Add_User**
  - Domain: Users
  - Pattern: SIMPLE_INSERT
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_users_Delete_User**
  - Domain: Users
  - Pattern: SIMPLE_DELETE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_users_Exists**
  - Domain: Users
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_users_Get_All**
  - Domain: Users
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_users_Get_ByUser**
  - Domain: Users
  - Pattern: QUERY
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

- [ ] **usr_users_Update_User**
  - Domain: Users
  - Pattern: SIMPLE_UPDATE
  - Status: _Pending validation_
  - Validation Notes: _[To be filled during automated validation]_

---

## Completion Summary

- **Total Procedures**: 75
- **Inventory Domain**: 9 procedures
- **Transactions Domain**: 3 procedures
- **Logging Domain**: 8 procedures
- **Master Data Domain**: 18 procedures
- **System Domain**: 20 procedures
- **Unknown/Maintenance Domain**: 5 procedures
- **Users Domain**: 12 procedures

**Phase Gate**: This checklist MUST be 100% complete (all 75 checkboxes checked) before proceeding to Phase 3 (DAO Refactoring).

---

## Automated Validation Process

When this checklist is processed by the automated validation system (Phase 2.5B - T106b):

1. **Load CSV**: Read `procedure-transaction-analysis.csv`
2. **For Each Procedure**:
   - Check `DeveloperCorrection` column
   - Check `Notes` column
   - Check `NeedsAttention` flag
   - Apply validation logic based on rules above
   - Execute required actions (correction, testing, validation)
   - Check off corresponding checklist item
   - Update "Validation Notes" with results
3. **Generate Report**: Summary of all validations, corrections, and pass/fail status
4. **Verify 100% Complete**: Ensure all 75 procedures validated before phase advancement

---

## Manual Override

If automated validation encounters ambiguous data or inconclusive CallHierarchy information, it will:
- **STOP** processing
- **ASK** for human clarification
- **DOCUMENT** the specific issue in this checklist
- **WAIT** for explicit instruction before continuing

This ensures no stored procedure is marked complete without proper validation.
