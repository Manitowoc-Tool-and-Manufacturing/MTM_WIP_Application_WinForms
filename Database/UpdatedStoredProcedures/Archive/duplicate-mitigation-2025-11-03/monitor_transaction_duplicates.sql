-- ============================================================================
-- TRANSACTION DUPLICATE MONITORING SCRIPT
-- ============================================================================
-- Purpose: Monitor duplicate transaction records and verify cleanup results
-- Usage: Run this before and after cleanup to compare results
-- ============================================================================

-- Summary of current duplicates by transaction type
SELECT '=== DUPLICATE SUMMARY BY TYPE ===' AS Section;
SELECT 
    TransactionType,
    COUNT(DISTINCT BatchNumber) AS Batch_Count_With_Duplicates,
    SUM(duplicate_count - 1) AS Duplicate_Records_To_Remove,
    SUM(duplicate_count) AS Total_Records_Affected
FROM (
    SELECT 
        BatchNumber, 
        TransactionType, 
        COUNT(*) AS duplicate_count 
    FROM inv_transaction 
    WHERE BatchNumber IS NOT NULL 
    GROUP BY BatchNumber, TransactionType 
    HAVING COUNT(*) > 1
) AS dups 
GROUP BY TransactionType 
WITH ROLLUP;

-- Count of duplicate groups (exact duplicates across all fields)
SELECT '=== EXACT DUPLICATE GROUPS ===' AS Section;
SELECT 
    TransactionType,
    COUNT(*) AS Duplicate_Groups,
    SUM(duplicate_count - 1) AS Records_To_Remove
FROM (
    SELECT 
        TransactionType,
        COUNT(*) AS duplicate_count
    FROM inv_transaction
    WHERE BatchNumber IS NOT NULL
    GROUP BY BatchNumber, TransactionType, PartID, FromLocation, ToLocation, 
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
) AS exact_dups
GROUP BY TransactionType
WITH ROLLUP;

-- Top 10 batches with most duplicates
SELECT '=== TOP 10 WORST DUPLICATE BATCHES ===' AS Section;
SELECT 
    BatchNumber,
    TransactionType,
    PartID,
    COUNT(*) AS Duplicate_Count,
    MIN(ID) AS Keep_ID,
    GROUP_CONCAT(ID ORDER BY ID) AS All_IDs,
    MIN(ReceiveDate) AS First_Date,
    MAX(ReceiveDate) AS Last_Date
FROM inv_transaction
WHERE BatchNumber IS NOT NULL
GROUP BY BatchNumber, TransactionType, PartID, FromLocation, ToLocation, 
         Operation, Quantity, User, ReceiveDate
HAVING COUNT(*) > 1
ORDER BY Duplicate_Count DESC
LIMIT 10;

-- Date range of duplicates
SELECT '=== DUPLICATE DATE RANGE ===' AS Section;
SELECT 
    DATE(MIN(ReceiveDate)) AS Earliest_Duplicate,
    DATE(MAX(ReceiveDate)) AS Latest_Duplicate,
    DATEDIFF(MAX(ReceiveDate), MIN(ReceiveDate)) AS Days_Span
FROM inv_transaction t1
WHERE EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.BatchNumber = t1.BatchNumber
    AND t2.TransactionType = t1.TransactionType
    AND t2.BatchNumber IS NOT NULL
    GROUP BY t2.BatchNumber, t2.TransactionType, t2.PartID, t2.FromLocation, 
             t2.ToLocation, t2.Operation, t2.Quantity, t2.User, t2.ReceiveDate
    HAVING COUNT(*) > 1
);

-- Duplicates by date (last 30 days)
SELECT '=== DUPLICATES BY DATE (LAST 30 DAYS) ===' AS Section;
SELECT 
    DATE(ReceiveDate) AS Date,
    TransactionType,
    COUNT(*) AS Duplicate_Transactions
FROM inv_transaction t1
WHERE ReceiveDate >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
AND EXISTS (
    SELECT 1 FROM inv_transaction t2
    WHERE t2.BatchNumber = t1.BatchNumber
    AND t2.TransactionType = t1.TransactionType
    AND t2.BatchNumber IS NOT NULL
    GROUP BY t2.BatchNumber, t2.TransactionType, t2.PartID, t2.FromLocation, 
             t2.ToLocation, t2.Operation, t2.Quantity, t2.User, t2.ReceiveDate
    HAVING COUNT(*) > 1
)
GROUP BY DATE(ReceiveDate), TransactionType
ORDER BY Date DESC, TransactionType;

-- Overall transaction table statistics
SELECT '=== OVERALL TABLE STATISTICS ===' AS Section;
SELECT 
    COUNT(*) AS Total_Transactions,
    COUNT(DISTINCT BatchNumber) AS Unique_Batches,
    COUNT(DISTINCT PartID) AS Unique_Parts,
    MIN(ReceiveDate) AS Earliest_Transaction,
    MAX(ReceiveDate) AS Latest_Transaction
FROM inv_transaction;

-- Transaction type distribution
SELECT '=== TRANSACTION TYPE DISTRIBUTION ===' AS Section;
SELECT 
    TransactionType,
    COUNT(*) AS Total_Count,
    COUNT(DISTINCT BatchNumber) AS Unique_Batches,
    ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM inv_transaction), 2) AS Percentage
FROM inv_transaction
GROUP BY TransactionType
ORDER BY Total_Count DESC;

-- Check for NULL batch numbers
SELECT '=== NULL BATCH NUMBER CHECK ===' AS Section;
SELECT 
    TransactionType,
    COUNT(*) AS Records_With_Null_Batch
FROM inv_transaction
WHERE BatchNumber IS NULL
GROUP BY TransactionType;
