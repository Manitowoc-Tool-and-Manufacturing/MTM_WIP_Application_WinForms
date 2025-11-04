-- =============================================
-- Script: analyze_and_fix_duplicates.sql
-- Purpose: Analyze and fix duplicate transactions causing quantity mismatches
-- Created: 2025-11-03
-- 
-- ROOT CAUSE: Multiple identical transaction records with same:
--   - BatchNumber, PartID, TransactionType
--   - Location, Operation, Quantity, User, Timestamp
-- 
-- SOLUTION: Keep only the lowest ID from each duplicate group
-- =============================================

-- =============================================
-- STEP 1: ANALYSIS - Count duplicates
-- =============================================

SELECT 
    'DUPLICATE_SUMMARY' AS Analysis,
    COUNT(*) AS Total_Duplicate_Records,
    COUNT(DISTINCT CONCAT(BatchNumber, '|', PartID, '|', TransactionType, '|',
                          COALESCE(FromLocation, ''), '|', COALESCE(ToLocation, ''), '|',
                          Operation, '|', Quantity, '|', User, '|', ReceiveDate)) AS Unique_Duplicate_Groups
FROM inv_transaction t
WHERE (t.BatchNumber, t.PartID, t.TransactionType, 
       COALESCE(t.FromLocation, ''), COALESCE(t.ToLocation, ''), 
       t.Operation, t.Quantity, t.User, t.ReceiveDate) IN (
    SELECT BatchNumber, PartID, TransactionType, 
           COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
           Operation, Quantity, User, ReceiveDate
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType, 
             COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
);

-- =============================================
-- STEP 2: PREVIEW - Show sample duplicates
-- =============================================

SELECT 
    'SAMPLE_DUPLICATES' AS Report,
    t.BatchNumber,
    t.PartID,
    t.TransactionType,
    t.FromLocation,
    t.ToLocation,
    t.Quantity,
    t.User,
    t.ReceiveDate,
    COUNT(*) AS Duplicate_Count,
    GROUP_CONCAT(t.ID ORDER BY t.ID) AS All_IDs,
    MIN(t.ID) AS ID_To_Keep,
    GROUP_CONCAT(CASE WHEN t.ID > MIN(t.ID) THEN t.ID END ORDER BY t.ID) AS IDs_To_Delete
FROM inv_transaction t
WHERE (t.BatchNumber, t.PartID, t.TransactionType, 
       COALESCE(t.FromLocation, ''), COALESCE(t.ToLocation, ''), 
       t.Operation, t.Quantity, t.User, t.ReceiveDate) IN (
    SELECT BatchNumber, PartID, TransactionType, 
           COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
           Operation, Quantity, User, ReceiveDate
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType, 
             COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
)
GROUP BY t.BatchNumber, t.PartID, t.TransactionType, 
         t.FromLocation, t.ToLocation, t.Operation, 
         t.Quantity, t.User, t.ReceiveDate
ORDER BY Duplicate_Count DESC, t.BatchNumber
LIMIT 20;

-- =============================================
-- STEP 3: CREATE BACKUP TABLE
-- =============================================

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
    backup_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    deletion_reason VARCHAR(255),
    INDEX idx_batch_part (BatchNumber, PartID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =============================================
-- STEP 4: BACKUP DUPLICATES (Before deletion)
-- =============================================

INSERT INTO inv_transaction_duplicates_backup_20251103
    (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation,
     Quantity, Notes, User, ItemType, ReceiveDate, deletion_reason)
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
    CONCAT('Duplicate - keeping ID ',
           (SELECT MIN(t2.ID)
            FROM inv_transaction t2
            WHERE t2.BatchNumber = t.BatchNumber
              AND t2.PartID = t.PartID
              AND t2.TransactionType = t.TransactionType
              AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
              AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
              AND t2.Operation = t.Operation
              AND t2.Quantity = t.Quantity
              AND t2.User = t.User
              AND t2.ReceiveDate = t.ReceiveDate))
FROM inv_transaction t
WHERE t.ID NOT IN (
    -- Keep the minimum ID from each duplicate group
    SELECT MIN(ID)
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType,
             COALESCE(FromLocation, ''), COALESCE(ToLocation, ''),
             Operation, Quantity, User, ReceiveDate
)
AND (t.BatchNumber, t.PartID, t.TransactionType, 
     COALESCE(t.FromLocation, ''), COALESCE(t.ToLocation, ''), 
     t.Operation, t.Quantity, t.User, t.ReceiveDate) IN (
    SELECT BatchNumber, PartID, TransactionType, 
           COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
           Operation, Quantity, User, ReceiveDate
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType, 
             COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
);

SELECT 
    'BACKUP_COMPLETE' AS Status,
    COUNT(*) AS Records_Backed_Up
FROM inv_transaction_duplicates_backup_20251103;

-- =============================================
-- STEP 5: DELETE DUPLICATES (Keep lowest ID)
-- =============================================
-- UNCOMMENT BELOW TO EXECUTE THE FIX:

/*
DELETE t FROM inv_transaction t
WHERE t.ID NOT IN (
    -- Keep the minimum ID from each duplicate group
    SELECT MIN(ID)
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType,
             COALESCE(FromLocation, ''), COALESCE(ToLocation, ''),
             Operation, Quantity, User, ReceiveDate
)
AND (t.BatchNumber, t.PartID, t.TransactionType, 
     COALESCE(t.FromLocation, ''), COALESCE(t.ToLocation, ''), 
     t.Operation, t.Quantity, t.User, t.ReceiveDate) IN (
    SELECT BatchNumber, PartID, TransactionType, 
           COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
           Operation, Quantity, User, ReceiveDate
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType, 
             COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
);

SELECT 'DELETION_COMPLETE' AS Status, ROW_COUNT() AS Records_Deleted;
*/

-- =============================================
-- STEP 6: VERIFY FIX
-- =============================================

SELECT 
    'VERIFICATION' AS Status,
    COUNT(*) AS Remaining_Duplicate_Groups
FROM (
    SELECT 
        BatchNumber, PartID, TransactionType,
        COALESCE(FromLocation, ''), COALESCE(ToLocation, ''),
        Operation, Quantity, User, ReceiveDate,
        COUNT(*) AS cnt
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType,
             COALESCE(FromLocation, ''), COALESCE(ToLocation, ''),
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
) sub;

-- =============================================
-- RESTORE INSTRUCTIONS (If needed)
-- =============================================
-- 
-- INSERT INTO inv_transaction 
--     (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, 
--      Operation, Quantity, Notes, User, ItemType, ReceiveDate)
-- SELECT 
--     ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation,
--     Operation, Quantity, Notes, User, ItemType, ReceiveDate
-- FROM inv_transaction_duplicates_backup_20251103
-- WHERE ID NOT IN (SELECT ID FROM inv_transaction);
-- 
-- =============================================
-- End of analyze_and_fix_duplicates.sql
-- =============================================
