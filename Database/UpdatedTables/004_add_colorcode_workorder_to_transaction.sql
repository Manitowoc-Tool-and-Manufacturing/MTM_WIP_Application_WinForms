-- Migration 004: Add ColorCode and WorkOrder columns to inv_transaction table
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

-- Add ColorCode column after Notes
ALTER TABLE inv_transaction
ADD COLUMN ColorCode VARCHAR(50) NOT NULL DEFAULT 'Unknown'
AFTER Notes;

-- Add WorkOrder column after ColorCode
ALTER TABLE inv_transaction
ADD COLUMN WorkOrder VARCHAR(10) NOT NULL DEFAULT 'Unknown'
AFTER ColorCode;

-- Note: No indexes needed on inv_transaction (historical table, not used in searches)

-- Verify columns added
SELECT 'ColorCode and WorkOrder columns added to inv_transaction successfully' AS Status;

-- Show updated table structure
DESCRIBE inv_transaction;

-- Show count of records (all should have "Unknown" as default)
SELECT 
    COUNT(*) AS TotalTransactions,
    SUM(CASE WHEN ColorCode = 'Unknown' THEN 1 ELSE 0 END) AS UnknownColorTransactions,
    SUM(CASE WHEN WorkOrder = 'Unknown' THEN 1 ELSE 0 END) AS UnknownWorkOrderTransactions
FROM inv_transaction;

/*
ROLLBACK PROCEDURE (if needed):
ALTER TABLE inv_transaction DROP COLUMN WorkOrder;
ALTER TABLE inv_transaction DROP COLUMN ColorCode;
*/
