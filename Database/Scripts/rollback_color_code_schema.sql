-- Rollback Script: Remove color code schema changes
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24
-- WARNING: This will remove all color code and work order data!

USE mtm_wip_application_winforms;

-- Step 1: Drop indexes
DROP INDEX idx_colorcode ON inv_inventory;
DROP INDEX idx_workorder ON inv_inventory;
DROP INDEX idx_requires_colorcode ON md_part_ids;

-- Step 2: Remove columns from inv_transaction
ALTER TABLE inv_transaction DROP COLUMN WorkOrder;
ALTER TABLE inv_transaction DROP COLUMN ColorCode;

-- Step 3: Remove columns from inv_inventory
ALTER TABLE inv_inventory DROP COLUMN WorkOrder;
ALTER TABLE inv_inventory DROP COLUMN ColorCode;

-- Step 4: Remove column from md_part_ids
ALTER TABLE md_part_ids DROP COLUMN RequiresColorCode;

-- Step 5: Drop md_color_codes table
DROP TABLE IF EXISTS md_color_codes;

-- Step 6: Drop updated stored procedures
DROP PROCEDURE IF EXISTS md_color_codes_GetAll;
DROP PROCEDURE IF EXISTS md_color_codes_Add;
DROP PROCEDURE IF EXISTS md_part_ids_GetAllColorCodeFlagged;
DROP PROCEDURE IF EXISTS md_part_ids_UpdateColorCodeFlag;

-- Note: inv_inventory_Add_Item, inv_inventory_Get_ByPartID, inv_transaction_Add
-- will need to be restored from backup if they were updated

SELECT 'Rollback completed - all color code schema changes removed' AS Status;
