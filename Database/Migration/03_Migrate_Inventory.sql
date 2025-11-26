-- 03_Migrate_Inventory.sql
-- Migrates inventory and transactions from mtm_wip_application to mtm_wip_application_winforms

SET FOREIGN_KEY_CHECKS = 0;

-- Clean target tables
TRUNCATE TABLE mtm_wip_application_winforms.inv_inventory;
TRUNCATE TABLE mtm_wip_application_winforms.inv_transaction;
TRUNCATE TABLE mtm_wip_application_winforms.sys_last_10_transactions;

INSERT INTO mtm_wip_application_winforms.inv_inventory (PartID, Location, Operation, Quantity, ItemType, ReceiveDate, LastUpdated, User, BatchNumber, Notes, ColorCode, WorkOrder)
SELECT PartID, Location, Operation, Quantity, ItemType, ReceiveDate, LastUpdated, User, BatchNumber, Notes, 'UNKNOWN', 'UNKNOWN'
FROM mtm_wip_application.inv_inventory;

INSERT INTO mtm_wip_application_winforms.inv_transaction (TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation, Quantity, Notes, User, ItemType, ReceiveDate, ColorCode, WorkOrder)
SELECT TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation, Quantity, Notes, User, ItemType, ReceiveDate, 'Unknown', 'Unknown'
FROM mtm_wip_application.inv_transaction;

INSERT INTO mtm_wip_application_winforms.sys_last_10_transactions (in_id, in_part, in_loc, in_batch, out_id, out_part, out_loc, out_batch, matched_at)
SELECT in_id, in_part, in_loc, in_batch, out_id, out_part, out_loc, out_batch, matched_at
FROM mtm_wip_application.sys_last_10_transactions;

SET FOREIGN_KEY_CHECKS = 1;
