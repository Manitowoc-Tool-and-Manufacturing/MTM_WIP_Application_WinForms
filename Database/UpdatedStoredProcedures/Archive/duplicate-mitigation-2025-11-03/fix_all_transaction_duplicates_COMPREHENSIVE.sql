-- ============================================================================
-- COMPREHENSIVE TRANSACTION DUPLICATE CLEANUP - OPTIMIZED
-- ============================================================================
-- Purpose: Remove ALL historical duplicate transactions (IN, OUT, TRANSFER)
-- Created: 2025-11-03
-- Optimized: Uses temporary tables and indexes for faster execution
-- 
-- Safety: Creates backup table, keeps lowest ID (original record), removes extras
-- Rollback: Backup table allows complete restoration if needed
-- ============================================================================

-- ============================================================================
-- STEP 1: ANALYSIS (Quick count)
-- ============================================================================
SELECT '=== COMPREHENSIVE DUPLICATE ANALYSIS STARTING ===' AS Status;

SELECT 
    'DUPLICATE_COUNT' AS Analysis,
    COUNT(*) AS Total_Duplicate_Records
FROM inv_transaction t1
WHERE EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.ID > t1.ID
    AND t2.BatchNumber = t1.BatchNumber
    AND t2.TransactionType = t1.TransactionType
    AND t2.PartID = t1.PartID
    AND COALESCE(t2.FromLocation, '') = COALESCE(t1.FromLocation, '')
    AND COALESCE(t2.ToLocation, '') = COALESCE(t1.ToLocation, '')
    AND t2.Operation = t1.Operation
    AND t2.Quantity = t1.Quantity
    AND t2.User = t1.User
    AND t2.ReceiveDate = t1.ReceiveDate
);

-- ============================================================================
-- STEP 2: CREATE BACKUP TABLE
-- ============================================================================
SELECT '=== CREATING BACKUP TABLE ===' AS Status;

DROP TABLE IF EXISTS inv_transaction_duplicates_backup_comprehensive_20251103;

CREATE TABLE inv_transaction_duplicates_backup_comprehensive_20251103 LIKE inv_transaction;

SELECT 'BACKUP_TABLE_CREATED' AS Status, 
       'inv_transaction_duplicates_backup_comprehensive_20251103' AS Table_Name;

-- ============================================================================
-- STEP 3: IDENTIFY DUPLICATES TO REMOVE
-- ============================================================================
SELECT '=== IDENTIFYING DUPLICATES ===' AS Status;

-- Create temp table with duplicate IDs to remove (keeping lowest ID)
DROP TEMPORARY TABLE IF EXISTS temp_ids_to_remove;

CREATE TEMPORARY TABLE temp_ids_to_remove (
    id_to_remove BIGINT PRIMARY KEY
);

INSERT INTO temp_ids_to_remove
SELECT t1.ID
FROM inv_transaction t1
WHERE EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.ID < t1.ID  -- Keep the lower ID
    AND t2.BatchNumber = t1.BatchNumber
    AND t2.TransactionType = t1.TransactionType
    AND t2.PartID = t1.PartID
    AND COALESCE(t2.FromLocation, '') = COALESCE(t1.FromLocation, '')
    AND COALESCE(t2.ToLocation, '') = COALESCE(t1.ToLocation, '')
    AND t2.Operation = t1.Operation
    AND t2.Quantity = t1.Quantity
    AND t2.User = t1.User
    AND t2.ReceiveDate = t1.ReceiveDate
);

SELECT 'DUPLICATES_IDENTIFIED' AS Status, COUNT(*) AS Records_To_Remove 
FROM temp_ids_to_remove;

-- ============================================================================
-- STEP 4: BACKUP DUPLICATES
-- ============================================================================
SELECT '=== BACKING UP DUPLICATES ===' AS Status;

INSERT INTO inv_transaction_duplicates_backup_comprehensive_20251103
SELECT t.*
FROM inv_transaction t
JOIN temp_ids_to_remove r ON t.ID = r.id_to_remove;

SELECT 'BACKUP_COMPLETE' AS Status, 
       COUNT(*) AS Records_Backed_Up,
       NOW() AS Backup_Time
FROM inv_transaction_duplicates_backup_comprehensive_20251103;

-- ============================================================================
-- STEP 5: DELETE DUPLICATES
-- ============================================================================
SELECT '=== DELETING DUPLICATES ===' AS Status;

DELETE FROM inv_transaction
WHERE ID IN (SELECT id_to_remove FROM temp_ids_to_remove);

SELECT 'DELETION_COMPLETE' AS Status, ROW_COUNT() AS Records_Deleted;

-- ============================================================================
-- STEP 6: VERIFICATION
-- ============================================================================
SELECT '=== VERIFYING FIX ===' AS Status;

-- Check for remaining duplicates
SELECT 
    'REMAINING_DUPLICATES' AS Verification,
    COUNT(*) AS Duplicate_Groups_Remaining
FROM (
    SELECT 
        BatchNumber, 
        TransactionType, 
        COUNT(*) 
    FROM inv_transaction 
    WHERE BatchNumber IS NOT NULL
    GROUP BY BatchNumber, TransactionType, PartID, FromLocation, ToLocation, 
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
) AS remaining_dups;

-- Summary by transaction type
SELECT 
    'FINAL_SUMMARY' AS Status,
    TransactionType,
    COUNT(*) AS Total_Transactions,
    COUNT(DISTINCT BatchNumber) AS Unique_Batches
FROM inv_transaction
GROUP BY TransactionType
ORDER BY TransactionType;

-- Cleanup
DROP TEMPORARY TABLE IF EXISTS temp_ids_to_remove;

SELECT '=== COMPREHENSIVE FIX COMPLETE ===' AS Status;

-- ============================================================================
-- ROLLBACK INSTRUCTIONS
-- ============================================================================
-- To rollback this fix, execute:
-- 
-- INSERT INTO inv_transaction 
-- SELECT * FROM inv_transaction_duplicates_backup_comprehensive_20251103;
-- 
-- -- Verify restoration
-- SELECT COUNT(*) FROM inv_transaction;
-- SELECT COUNT(*) FROM inv_transaction_duplicates_backup_comprehensive_20251103;
-- ============================================================================

-- ============================================================================
-- COMPLETION
-- ============================================================================
SELECT '=== COMPREHENSIVE FIX COMPLETE ===' AS Status;
SELECT 
    'BACKUP_LOCATION' AS Info, 
    @backup_table_name AS Backup_Table,
    'Use this table to rollback if needed' AS Note;

-- ============================================================================
-- ROLLBACK INSTRUCTIONS
-- ============================================================================
-- To rollback this fix, execute:
-- 
-- SET @backup_table = 'inv_transaction_duplicates_backup_comprehensive_YYYYMMDD_HHMMSS';
-- 
-- -- 1. Restore deleted records
-- SET @sql = CONCAT(
--     'INSERT INTO inv_transaction ',
--     'SELECT ID, BatchNumber, PartID, TransactionType, FromLocation, ',
--     '       ToLocation, Operation, Quantity, User, ReceiveDate, ItemType, Notes ',
--     'FROM ', @backup_table
-- );
-- PREPARE stmt FROM @sql;
-- EXECUTE stmt;
-- DEALLOCATE PREPARE stmt;
-- 
-- -- 2. Verify restoration
-- SELECT BatchNumber, TransactionType, PartID, COUNT(*) 
-- FROM inv_transaction 
-- GROUP BY BatchNumber, TransactionType, PartID, FromLocation, ToLocation, Operation, Quantity, User, ReceiveDate
-- HAVING COUNT(*) > 1
-- LIMIT 20;
-- ============================================================================
