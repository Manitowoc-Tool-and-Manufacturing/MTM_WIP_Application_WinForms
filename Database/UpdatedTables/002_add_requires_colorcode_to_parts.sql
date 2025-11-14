-- Migration 002: Add RequiresColorCode flag to md_part_ids table
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

-- Add RequiresColorCode column to md_part_ids
ALTER TABLE md_part_ids 
ADD COLUMN RequiresColorCode BOOLEAN NOT NULL DEFAULT FALSE
AFTER IssuedBy;

-- Create index for performance (used in cache loading)
CREATE INDEX idx_requires_colorcode ON md_part_ids(RequiresColorCode);

-- Verify column added
SELECT 'RequiresColorCode column added to md_part_ids successfully' AS Status;

-- Show updated table structure
DESCRIBE md_part_ids;

-- Show count of flagged parts (should be 0 initially)
SELECT COUNT(*) AS FlaggedParts 
FROM md_part_ids 
WHERE RequiresColorCode = TRUE;

/*
ROLLBACK PROCEDURE (if needed):
DROP INDEX idx_requires_colorcode ON md_part_ids;
ALTER TABLE md_part_ids DROP COLUMN RequiresColorCode;
*/
