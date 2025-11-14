-- Validation Script: Verify color code schema changes
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

SELECT '=== Color Code Schema Validation ===' AS ValidationSection;

-- Check md_color_codes table exists
SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: md_color_codes table exists'
        ELSE '✗ FAIL: md_color_codes table missing'
    END AS ValidationResult
FROM information_schema.TABLES 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'md_color_codes';

-- Check md_part_ids has RequiresColorCode column
SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: md_part_ids.RequiresColorCode column exists'
        ELSE '✗ FAIL: md_part_ids.RequiresColorCode column missing'
    END AS ValidationResult
FROM information_schema.COLUMNS 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'md_part_ids' 
AND COLUMN_NAME = 'RequiresColorCode';

-- Check inv_inventory has ColorCode column
SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: inv_inventory.ColorCode column exists'
        ELSE '✗ FAIL: inv_inventory.ColorCode column missing'
    END AS ValidationResult
FROM information_schema.COLUMNS 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'inv_inventory' 
AND COLUMN_NAME = 'ColorCode';

-- Check inv_inventory has WorkOrder column
SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: inv_inventory.WorkOrder column exists'
        ELSE '✗ FAIL: inv_inventory.WorkOrder column missing'
    END AS ValidationResult
FROM information_schema.COLUMNS 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'inv_inventory' 
AND COLUMN_NAME = 'WorkOrder';

-- Check inv_transaction has ColorCode column
SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: inv_transaction.ColorCode column exists'
        ELSE '✗ FAIL: inv_transaction.ColorCode column missing'
    END AS ValidationResult
FROM information_schema.COLUMNS 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'inv_transaction' 
AND COLUMN_NAME = 'ColorCode';

-- Check inv_transaction has WorkOrder column
SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: inv_transaction.WorkOrder column exists'
        ELSE '✗ FAIL: inv_transaction.WorkOrder column missing'
    END AS ValidationResult
FROM information_schema.COLUMNS 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'inv_transaction' 
AND COLUMN_NAME = 'WorkOrder';

-- Check indexes exist
SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: idx_requires_colorcode index exists'
        ELSE '✗ FAIL: idx_requires_colorcode index missing'
    END AS ValidationResult
FROM information_schema.STATISTICS 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'md_part_ids' 
AND INDEX_NAME = 'idx_requires_colorcode';

SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: idx_colorcode index exists on inv_inventory'
        ELSE '✗ FAIL: idx_colorcode index missing on inv_inventory'
    END AS ValidationResult
FROM information_schema.STATISTICS 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'inv_inventory' 
AND INDEX_NAME = 'idx_colorcode';

SELECT 
    CASE 
        WHEN COUNT(*) > 0 THEN '✓ PASS: idx_workorder index exists on inv_inventory'
        ELSE '✗ FAIL: idx_workorder index missing on inv_inventory'
    END AS ValidationResult
FROM information_schema.STATISTICS 
WHERE TABLE_SCHEMA = 'mtm_wip_application_winforms' 
AND TABLE_NAME = 'inv_inventory' 
AND INDEX_NAME = 'idx_workorder';

-- Check seed data exists
SELECT 
    CASE 
        WHEN COUNT(*) >= 10 THEN CONCAT('✓ PASS: ', COUNT(*), ' predefined colors seeded')
        ELSE CONCAT('✗ FAIL: Only ', COUNT(*), ' colors found (expected 10+)')
    END AS ValidationResult
FROM md_color_codes 
WHERE IsUserDefined = FALSE;

SELECT '=== Validation Complete ===' AS ValidationSection;
