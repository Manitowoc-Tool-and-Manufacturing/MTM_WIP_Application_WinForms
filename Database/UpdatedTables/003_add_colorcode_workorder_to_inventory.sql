-- Migration 003: Add ColorCode and WorkOrder columns to inv_inventory table
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

-- Add ColorCode column after Notes
ALTER TABLE inv_inventory
ADD COLUMN ColorCode VARCHAR(50) NOT NULL DEFAULT 'Unknown'
AFTER Notes;

-- Add WorkOrder column after ColorCode
ALTER TABLE inv_inventory
ADD COLUMN WorkOrder VARCHAR(10) NOT NULL DEFAULT 'Unknown'
AFTER ColorCode;

-- Create indexes for performance (used in search/sort operations)
CREATE INDEX idx_colorcode ON inv_inventory(ColorCode);
CREATE INDEX idx_workorder ON inv_inventory(WorkOrder);

-- Verify columns added
SELECT 'ColorCode and WorkOrder columns added to inv_inventory successfully' AS Status;

-- Show updated table structure
DESCRIBE inv_inventory;

-- Show count of records (all should have "Unknown" as default)
SELECT 
    COUNT(*) AS TotalRecords,
    SUM(CASE WHEN ColorCode = 'Unknown' THEN 1 ELSE 0 END) AS UnknownColorRecords,
    SUM(CASE WHEN WorkOrder = 'Unknown' THEN 1 ELSE 0 END) AS UnknownWorkOrderRecords
FROM inv_inventory;

/*
ROLLBACK PROCEDURE (if needed):
DROP INDEX idx_colorcode ON inv_inventory;
DROP INDEX idx_workorder ON inv_inventory;
ALTER TABLE inv_inventory DROP COLUMN WorkOrder;
ALTER TABLE inv_inventory DROP COLUMN ColorCode;
*/
