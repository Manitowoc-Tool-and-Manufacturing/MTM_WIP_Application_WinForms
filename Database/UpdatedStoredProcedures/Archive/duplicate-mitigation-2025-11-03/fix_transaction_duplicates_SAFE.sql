-- =============================================
-- Script: fix_transaction_duplicates_SAFE.sql
-- Purpose: Fix duplicate transactions causing quantity mismatches
-- Created: 2025-11-03
-- MySQL Version: 5.7+ compatible
-- 
-- EXECUTION PLAN:
--   1. Analyze current duplicates
--   2. Create backup table
--   3. Backup all duplicates
--   4. DELETE duplicates (keeps lowest ID)
--   5. Verify fix
-- =============================================

-- =============================================
-- STEP 1: ANALYZE DUPLICATES
-- =============================================

SELECT '=== DUPLICATE ANALYSIS STARTING ===' AS Status;

SELECT 
    'DUPLICATE_COUNT' AS Analysis,
    COUNT(*) AS Total_Records_With_Duplicates
FROM inv_transaction t
WHERE EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.BatchNumber = t.BatchNumber
      AND t2.PartID = t.PartID
      AND t2.TransactionType = t.TransactionType
      AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
      AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
      AND t2.Operation = t.Operation
      AND t2.Quantity = t.Quantity
      AND t2.User = t.User
      AND t2.ReceiveDate = t.ReceiveDate
      AND t2.ID != t.ID
);

SELECT 
    'TOP_DUPLICATES' AS Sample,
    BatchNumber,
    PartID,
    TransactionType,
    Quantity,
    COUNT(*) AS Duplicate_Count,
    MIN(ID) AS ID_To_Keep,
    GROUP_CONCAT(ID ORDER BY ID) AS All_IDs
FROM inv_transaction
GROUP BY BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
         Operation, Quantity, User, ReceiveDate
HAVING COUNT(*) > 1
ORDER BY Duplicate_Count DESC, BatchNumber
LIMIT 10;

-- =============================================
-- STEP 2: CREATE BACKUP TABLE
-- =============================================

SELECT '=== CREATING BACKUP TABLE ===' AS Status;

CREATE TABLE IF NOT EXISTS inv_transaction_duplicates_backup_20251103 (
    ID INT PRIMARY KEY,
    TransactionType VARCHAR(50),
    BatchNumber VARCHAR(50),
    PartID VARCHAR(100),
    FromLocation VARCHAR(100),
    ToLocation VARCHAR(100),
    Operation VARCHAR(100),
    Quantity INT,
    Notes VARCHAR(1000),
    User VARCHAR(100),
    ItemType VARCHAR(200),
    ReceiveDate DATETIME,
    OriginalRecordID INT,
    backup_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    deletion_reason VARCHAR(255),
    INDEX idx_batch_part (BatchNumber, PartID),
    INDEX idx_original (OriginalRecordID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

SELECT 
    'BACKUP_TABLE_CREATED' AS Status,
    'inv_transaction_duplicates_backup_20251103' AS Table_Name;

-- =============================================
-- STEP 3: BACKUP DUPLICATES BEFORE DELETION
-- =============================================

SELECT '=== BACKING UP DUPLICATES ===' AS Status;

-- Create temporary table to identify min IDs
DROP TEMPORARY TABLE IF EXISTS temp_min_ids;
CREATE TEMPORARY TABLE temp_min_ids (
    BatchNumber VARCHAR(50),
    PartID VARCHAR(100),
    TransactionType VARCHAR(50),
    FromLocation VARCHAR(100),
    ToLocation VARCHAR(100),
    Operation VARCHAR(100),
    Quantity INT,
    User VARCHAR(100),
    ReceiveDate DATETIME,
    MinID INT,
    INDEX idx_lookup (BatchNumber, PartID, TransactionType)
) ENGINE=MEMORY;

INSERT INTO temp_min_ids
SELECT 
    BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
    Operation, Quantity, User, ReceiveDate, MIN(ID) AS MinID
FROM inv_transaction
GROUP BY BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
         Operation, Quantity, User, ReceiveDate
HAVING COUNT(*) > 1;

SELECT 
    'MIN_IDS_IDENTIFIED' AS Status,
    COUNT(*) AS Duplicate_Groups
FROM temp_min_ids;

-- Backup all duplicates (excluding the MIN ID to keep)
INSERT INTO inv_transaction_duplicates_backup_20251103
    (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation,
     Quantity, Notes, User, ItemType, ReceiveDate, OriginalRecordID, deletion_reason)
SELECT 
    t.ID,
    t.TransactionType,
    t.BatchNumber,
    t.PartID,
    t.FromLocation,
    t.ToLocation,
    t.Operation,
    t.Quantity,
    t.Notes,
    t.User,
    t.ItemType,
    t.ReceiveDate,
    m.MinID AS OriginalRecordID,
    CONCAT('Duplicate removed - Original ID ', m.MinID, ' kept') AS deletion_reason
FROM inv_transaction t
INNER JOIN temp_min_ids m 
    ON t.BatchNumber = m.BatchNumber
   AND t.PartID = m.PartID
   AND t.TransactionType = m.TransactionType
   AND COALESCE(t.FromLocation, '') = COALESCE(m.FromLocation, '')
   AND COALESCE(t.ToLocation, '') = COALESCE(m.ToLocation, '')
   AND t.Operation = m.Operation
   AND t.Quantity = m.Quantity
   AND t.User = m.User
   AND t.ReceiveDate = m.ReceiveDate
   AND t.ID > m.MinID;

SELECT 
    'BACKUP_COMPLETE' AS Status,
    COUNT(*) AS Records_Backed_Up,
    NOW() AS Backup_Time
FROM inv_transaction_duplicates_backup_20251103;

-- =============================================
-- STEP 4: DELETE DUPLICATES (KEEP LOWEST ID)
-- =============================================

SELECT '=== DELETING DUPLICATES ===' AS Status;

DELETE t FROM inv_transaction t
INNER JOIN temp_min_ids m 
    ON t.BatchNumber = m.BatchNumber
   AND t.PartID = m.PartID
   AND t.TransactionType = m.TransactionType
   AND COALESCE(t.FromLocation, '') = COALESCE(m.FromLocation, '')
   AND COALESCE(t.ToLocation, '') = COALESCE(m.ToLocation, '')
   AND t.Operation = m.Operation
   AND t.Quantity = m.Quantity
   AND t.User = m.User
   AND t.ReceiveDate = m.ReceiveDate
   AND t.ID > m.MinID;

SELECT 
    'DELETION_COMPLETE' AS Status,
    ROW_COUNT() AS Records_Deleted;

-- Clean up temporary table
DROP TEMPORARY TABLE IF EXISTS temp_min_ids;

-- =============================================
-- STEP 5: VERIFY FIX
-- =============================================

SELECT '=== VERIFYING FIX ===' AS Status;

-- Should return 0 or very few rows
SELECT 
    'REMAINING_DUPLICATES' AS Verification,
    COUNT(*) AS Duplicate_Groups_Remaining
FROM (
    SELECT 
        BatchNumber, PartID, TransactionType,
        FromLocation, ToLocation, Operation, Quantity, User, ReceiveDate,
        COUNT(*) AS cnt
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType,
             FromLocation, ToLocation, Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
) sub;

-- Show fixed batches
SELECT 
    'FIXED_BATCHES' AS Verification,
    b.BatchNumber,
    b.PartID,
    SUM(CASE WHEN t.TransactionType = 'IN' THEN t.Quantity ELSE 0 END) AS Total_IN,
    SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity ELSE 0 END) AS Total_OUT,
    (SUM(CASE WHEN t.TransactionType = 'IN' THEN t.Quantity ELSE 0 END) -
     SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity ELSE 0 END)) AS Net_Inventory
FROM (
    SELECT DISTINCT BatchNumber, PartID
    FROM inv_transaction_duplicates_backup_20251103
    LIMIT 10
) b
INNER JOIN inv_transaction t ON b.BatchNumber = t.BatchNumber AND b.PartID = t.PartID
GROUP BY b.BatchNumber, b.PartID
ORDER BY b.BatchNumber;

SELECT '=== FIX COMPLETE ===' AS Status;

-- =============================================
-- RESTORE INSTRUCTIONS (If Needed)
-- =============================================
-- 
-- To restore deleted records:
-- 
-- INSERT INTO inv_transaction 
--     (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, 
--      Operation, Quantity, Notes, User, ItemType, ReceiveDate)
-- SELECT 
--     ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation,
--     Operation, Quantity, Notes, User, ItemType, ReceiveDate
-- FROM inv_transaction_duplicates_backup_20251103
-- WHERE ID NOT IN (SELECT ID FROM inv_transaction)
-- ORDER BY ID;
-- 
-- =============================================
