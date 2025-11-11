-- ============================================================================
-- Cleanup Old Transaction Data (3+ months old)
-- ============================================================================
-- Date: 2025-11-07
-- Purpose: Remove transactions older than 3 months from inv_transaction
-- ============================================================================

SET @cutoff_date = DATE_SUB(CURDATE(), INTERVAL 3 MONTH);

SELECT CONCAT('Cutoff date: ', @cutoff_date) AS Info;

-- Show what will be deleted
SELECT 
    'Records to be deleted' AS Action,
    COUNT(*) AS Count,
    MIN(ReceiveDate) AS OldestDate,
    MAX(ReceiveDate) AS NewestDate
FROM inv_transaction
WHERE ReceiveDate < @cutoff_date;

-- Perform the deletion
DELETE FROM inv_transaction
WHERE ReceiveDate < @cutoff_date;

-- Show results
SELECT 
    'Remaining records' AS Status,
    COUNT(*) AS Count,
    MIN(ReceiveDate) AS OldestDate,
    MAX(ReceiveDate) AS NewestDate
FROM inv_transaction;
