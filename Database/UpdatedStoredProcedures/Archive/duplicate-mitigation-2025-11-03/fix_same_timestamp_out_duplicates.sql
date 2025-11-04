-- ============================================================================
-- FIX SAME-TIMESTAMP OUT DUPLICATES
-- ============================================================================
-- Purpose: Remove OUT transactions that have the exact same timestamp as 
--          their corresponding IN transaction (data entry error)
-- Issue: 4,322 OUT transactions were created at the exact same second as
--        the IN transaction, causing inventory integrity issues
-- ============================================================================

SELECT '=== ANALYZING SAME-TIMESTAMP OUT DUPLICATES ===' AS Status;

-- Count suspicious transactions
SELECT 
    'SUSPICIOUS_TRANSACTIONS' AS Analysis,
    COUNT(*) AS Total_Suspicious_OUT_Transactions
FROM inv_transaction t1
WHERE TransactionType = 'OUT'
AND EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.BatchNumber = t1.BatchNumber
    AND t2.TransactionType = 'IN'
    AND t2.ReceiveDate = t1.ReceiveDate
);

-- Show sample of affected batches
SELECT 
    'SAMPLE_AFFECTED_BATCHES' AS Sample,
    t1.BatchNumber,
    t1.PartID,
    COUNT(CASE WHEN t1.TransactionType = 'IN' THEN 1 END) AS IN_Count,
    COUNT(CASE WHEN t1.TransactionType = 'OUT' AND EXISTS (
        SELECT 1 FROM inv_transaction t2 
        WHERE t2.BatchNumber = t1.BatchNumber 
        AND t2.TransactionType = 'IN' 
        AND t2.ReceiveDate = t1.ReceiveDate
    ) THEN 1 END) AS Same_Second_OUT_Count,
    COUNT(CASE WHEN t1.TransactionType = 'OUT' THEN 1 END) AS Total_OUT_Count
FROM inv_transaction t1
GROUP BY t1.BatchNumber, t1.PartID
HAVING COUNT(CASE WHEN t1.TransactionType = 'OUT' AND EXISTS (
    SELECT 1 FROM inv_transaction t2 
    WHERE t2.BatchNumber = t1.BatchNumber 
    AND t2.TransactionType = 'IN' 
    AND t2.ReceiveDate = t1.ReceiveDate
) THEN 1 END) > 0
ORDER BY Same_Second_OUT_Count DESC
LIMIT 10;

-- ============================================================================
-- CREATE BACKUP
-- ============================================================================
SELECT '=== CREATING BACKUP ===' AS Status;

DROP TABLE IF EXISTS inv_transaction_same_timestamp_backup_20251103;

CREATE TABLE inv_transaction_same_timestamp_backup_20251103 LIKE inv_transaction;

INSERT INTO inv_transaction_same_timestamp_backup_20251103
SELECT t1.*
FROM inv_transaction t1
WHERE TransactionType = 'OUT'
AND EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.BatchNumber = t1.BatchNumber
    AND t2.TransactionType = 'IN'
    AND t2.ReceiveDate = t1.ReceiveDate
);

SELECT 
    'BACKUP_COMPLETE' AS Status,
    COUNT(*) AS Records_Backed_Up
FROM inv_transaction_same_timestamp_backup_20251103;

-- ============================================================================
-- DELETE SAME-TIMESTAMP OUT TRANSACTIONS
-- ============================================================================
SELECT '=== DELETING SAME-TIMESTAMP OUT TRANSACTIONS ===' AS Status;

-- Create temp table with IDs to delete
DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;

CREATE TEMPORARY TABLE temp_ids_to_delete AS
SELECT t1.ID
FROM inv_transaction t1
WHERE TransactionType = 'OUT'
AND EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.BatchNumber = t1.BatchNumber
    AND t2.TransactionType = 'IN'
    AND t2.ReceiveDate = t1.ReceiveDate
);

SELECT 'IDS_IDENTIFIED' AS Status, COUNT(*) AS IDs_To_Delete FROM temp_ids_to_delete;

-- Delete using temp table
DELETE FROM inv_transaction
WHERE ID IN (SELECT ID FROM temp_ids_to_delete);

SELECT 'DELETION_COMPLETE' AS Status, ROW_COUNT() AS Records_Deleted;

DROP TEMPORARY TABLE temp_ids_to_delete;

-- ============================================================================
-- VERIFICATION
-- ============================================================================
SELECT '=== VERIFYING FIX ===' AS Status;

-- Check for remaining same-timestamp issues
SELECT 
    'REMAINING_SAME_TIMESTAMP' AS Verification,
    COUNT(*) AS Remaining_Suspicious_Transactions
FROM inv_transaction t1
WHERE TransactionType = 'OUT'
AND EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.BatchNumber = t1.BatchNumber
    AND t2.TransactionType = 'IN'
    AND t2.ReceiveDate = t1.ReceiveDate
);

-- Check batch inventory balance (IN vs OUT)
SELECT 
    'BATCH_BALANCE_CHECK' AS Verification,
    COUNT(*) AS Batches_With_More_OUT_Than_IN
FROM (
    SELECT 
        BatchNumber,
        SUM(CASE WHEN TransactionType = 'IN' THEN 1 ELSE 0 END) AS IN_Count,
        SUM(CASE WHEN TransactionType = 'OUT' THEN 1 ELSE 0 END) AS OUT_Count
    FROM inv_transaction
    GROUP BY BatchNumber
    HAVING SUM(CASE WHEN TransactionType = 'OUT' THEN 1 ELSE 0 END) > 
           SUM(CASE WHEN TransactionType = 'IN' THEN 1 ELSE 0 END)
) AS imbalanced;

-- Summary statistics
SELECT 
    'FINAL_SUMMARY' AS Status,
    TransactionType,
    COUNT(*) AS Total_Transactions
FROM inv_transaction
GROUP BY TransactionType
ORDER BY TransactionType;

SELECT '=== FIX COMPLETE ===' AS Status;
SELECT 'BACKUP_TABLE' AS Info, 'inv_transaction_same_timestamp_backup_20251103' AS Table_Name;

-- ============================================================================
-- ROLLBACK INSTRUCTIONS
-- ============================================================================
-- To rollback:
-- INSERT INTO inv_transaction SELECT * FROM inv_transaction_same_timestamp_backup_20251103;
-- ============================================================================
