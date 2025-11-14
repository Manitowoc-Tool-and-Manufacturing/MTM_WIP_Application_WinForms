-- Migration 001: Add md_color_codes table for color code master data
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

-- Create color codes master table
CREATE TABLE IF NOT EXISTS md_color_codes (
    ColorCode VARCHAR(50) NOT NULL PRIMARY KEY,
    IsUserDefined BOOLEAN NOT NULL DEFAULT FALSE,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_user_defined (IsUserDefined)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Verify table created
SELECT 'md_color_codes table created successfully' AS Status;

-- Show table structure
DESCRIBE md_color_codes;

/*
ROLLBACK PROCEDURE (if needed):
DROP TABLE IF EXISTS md_color_codes;
*/
