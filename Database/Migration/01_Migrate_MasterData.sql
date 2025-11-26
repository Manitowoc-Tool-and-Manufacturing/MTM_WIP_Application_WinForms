-- 01_Migrate_MasterData.sql
-- Migrates master data from mtm_wip_application to mtm_wip_application_winforms

SET FOREIGN_KEY_CHECKS = 0;

-- Clean target tables
TRUNCATE TABLE mtm_wip_application_winforms.md_part_ids;
TRUNCATE TABLE mtm_wip_application_winforms.md_locations;
TRUNCATE TABLE mtm_wip_application_winforms.md_operation_numbers;
TRUNCATE TABLE mtm_wip_application_winforms.md_item_types;

INSERT INTO mtm_wip_application_winforms.md_part_ids (PartID, Customer, Description, IssuedBy, ItemType, Operations)
SELECT PartID, Customer, Description, IssuedBy, ItemType, Operations 
FROM mtm_wip_application.md_part_ids;

INSERT INTO mtm_wip_application_winforms.md_locations (Location, Building, IssuedBy)
SELECT Location, Building, IssuedBy 
FROM mtm_wip_application.md_locations;

INSERT INTO mtm_wip_application_winforms.md_operation_numbers (Operation, IssuedBy)
SELECT Operation, IssuedBy 
FROM mtm_wip_application.md_operation_numbers;

INSERT INTO mtm_wip_application_winforms.md_item_types (ItemType, IssuedBy)
SELECT ItemType, IssuedBy 
FROM mtm_wip_application.md_item_types;

SET FOREIGN_KEY_CHECKS = 1;
