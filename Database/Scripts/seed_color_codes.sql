-- Seed Script: Prepopulate md_color_codes with predefined colors
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

-- Insert predefined colors (IsUserDefined = FALSE)
INSERT INTO md_color_codes (ColorCode, IsUserDefined) VALUES
('Red', FALSE),
('Blue', FALSE),
('Green', FALSE),
('Yellow', FALSE),
('Orange', FALSE),
('Purple', FALSE),
('Pink', FALSE),
('White', FALSE),
('Black', FALSE),
('Unknown', FALSE);  -- System reserved for legacy data

-- Verify seed data
SELECT 'Predefined colors seeded successfully' AS Status;

-- Show all predefined colors
SELECT ColorCode, IsUserDefined, CreatedDate 
FROM md_color_codes 
ORDER BY ColorCode ASC;

-- Count predefined vs user-defined
SELECT 
    COUNT(*) AS TotalColors,
    SUM(CASE WHEN IsUserDefined = FALSE THEN 1 ELSE 0 END) AS PredefinedColors,
    SUM(CASE WHEN IsUserDefined = TRUE THEN 1 ELSE 0 END) AS CustomColors
FROM md_color_codes;
