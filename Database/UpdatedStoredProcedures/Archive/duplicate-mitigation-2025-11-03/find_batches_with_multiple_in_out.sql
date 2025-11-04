-- =============================================
-- Script: find_batches_with_multiple_in_out.sql
-- Purpose: Find batch numbers with multiple IN or OUT transactions
--          Useful for identifying split scenarios, data anomalies, or lifecycle patterns
-- Created: 2025-11-03
-- =============================================

-- =============================================
-- SECTION 1: Batches with Multiple IN Transactions
-- =============================================
-- These batches received inventory multiple times (unusual - may indicate data entry errors)

SELECT 
    'MULTIPLE_IN_TRANSACTIONS' AS Analysis_Type,
    BatchNumber,
    PartID,
    COUNT(*) AS IN_Count,
    GROUP_CONCAT(DISTINCT ToLocation ORDER BY ToLocation SEPARATOR ', ') AS Locations,
    SUM(Quantity) AS Total_Quantity,
    MIN(ReceiveDate) AS First_IN_Date,
    MAX(ReceiveDate) AS Last_IN_Date,
    DATEDIFF(MAX(ReceiveDate), MIN(ReceiveDate)) AS Days_Between
FROM inv_transaction
WHERE TransactionType = 'IN'
GROUP BY BatchNumber, PartID
HAVING COUNT(*) > 1
ORDER BY IN_Count DESC, BatchNumber;

-- =============================================
-- SECTION 2: Batches with Multiple OUT Transactions
-- =============================================
-- These batches were consumed/removed multiple times (indicates splits or phased removal)

SELECT 
    'MULTIPLE_OUT_TRANSACTIONS' AS Analysis_Type,
    BatchNumber,
    PartID,
    COUNT(*) AS OUT_Count,
    GROUP_CONCAT(DISTINCT FromLocation ORDER BY FromLocation SEPARATOR ', ') AS Locations,
    SUM(Quantity) AS Total_Quantity_Out,
    MIN(ReceiveDate) AS First_OUT_Date,
    MAX(ReceiveDate) AS Last_OUT_Date,
    DATEDIFF(MAX(ReceiveDate), MIN(ReceiveDate)) AS Days_Between
FROM inv_transaction
WHERE TransactionType = 'OUT'
GROUP BY BatchNumber, PartID
HAVING COUNT(*) > 1
ORDER BY OUT_Count DESC, BatchNumber;

-- =============================================
-- SECTION 3: Batches with Multiple TRANSFERs
-- =============================================
-- These batches moved between locations multiple times (normal for manufacturing)

SELECT 
    'MULTIPLE_TRANSFER_TRANSACTIONS' AS Analysis_Type,
    BatchNumber,
    PartID,
    COUNT(*) AS TRANSFER_Count,
    GROUP_CONCAT(
        DISTINCT CONCAT(FromLocation, ' → ', ToLocation) 
        ORDER BY FromLocation, ToLocation 
        SEPARATOR ' | '
    ) AS Location_Paths,
    SUM(Quantity) AS Total_Quantity_Transferred,
    MIN(ReceiveDate) AS First_TRANSFER_Date,
    MAX(ReceiveDate) AS Last_TRANSFER_Date,
    DATEDIFF(MAX(ReceiveDate), MIN(ReceiveDate)) AS Days_Between
FROM inv_transaction
WHERE TransactionType = 'TRANSFER'
GROUP BY BatchNumber, PartID
HAVING COUNT(*) > 1
ORDER BY TRANSFER_Count DESC, BatchNumber;

-- =============================================
-- SECTION 4: Complex Batches (Multiple IN AND Multiple OUT)
-- =============================================
-- These batches have multiple IN and OUT transactions (unusual pattern - investigate)

SELECT 
    'COMPLEX_BATCH_LIFECYCLE' AS Analysis_Type,
    t.BatchNumber,
    t.PartID,
    COUNT(CASE WHEN t.TransactionType = 'IN' THEN 1 END) AS IN_Count,
    COUNT(CASE WHEN t.TransactionType = 'OUT' THEN 1 END) AS OUT_Count,
    COUNT(CASE WHEN t.TransactionType = 'TRANSFER' THEN 1 END) AS TRANSFER_Count,
    COUNT(*) AS Total_Transactions,
    MIN(t.ReceiveDate) AS First_Transaction_Date,
    MAX(t.ReceiveDate) AS Last_Transaction_Date
FROM inv_transaction t
GROUP BY t.BatchNumber, t.PartID
HAVING 
    COUNT(CASE WHEN t.TransactionType = 'IN' THEN 1 END) > 1
    AND COUNT(CASE WHEN t.TransactionType = 'OUT' THEN 1 END) > 1
ORDER BY Total_Transactions DESC, BatchNumber;

-- =============================================
-- SECTION 5: Split Detection - Partial OUT Transactions
-- =============================================
-- Batches where OUT quantity < IN quantity (indicates splits)

SELECT 
    'SPLIT_DETECTED' AS Analysis_Type,
    i.BatchNumber,
    i.PartID,
    i.Total_IN_Quantity,
    o.Total_OUT_Quantity,
    (i.Total_IN_Quantity - COALESCE(o.Total_OUT_Quantity, 0)) AS Remaining_Quantity,
    i.IN_Locations,
    o.OUT_Locations,
    i.First_IN_Date,
    o.Last_OUT_Date
FROM (
    SELECT 
        BatchNumber,
        PartID,
        SUM(Quantity) AS Total_IN_Quantity,
        GROUP_CONCAT(DISTINCT ToLocation ORDER BY ToLocation SEPARATOR ', ') AS IN_Locations,
        MIN(ReceiveDate) AS First_IN_Date
    FROM inv_transaction
    WHERE TransactionType = 'IN'
    GROUP BY BatchNumber, PartID
) i
LEFT JOIN (
    SELECT 
        BatchNumber,
        PartID,
        SUM(Quantity) AS Total_OUT_Quantity,
        GROUP_CONCAT(DISTINCT FromLocation ORDER BY FromLocation SEPARATOR ', ') AS OUT_Locations,
        MAX(ReceiveDate) AS Last_OUT_Date
    FROM inv_transaction
    WHERE TransactionType = 'OUT'
    GROUP BY BatchNumber, PartID
) o ON i.BatchNumber = o.BatchNumber AND i.PartID = o.PartID
WHERE 
    o.Total_OUT_Quantity IS NOT NULL
    AND i.Total_IN_Quantity > o.Total_OUT_Quantity
ORDER BY Remaining_Quantity DESC, i.BatchNumber;

-- =============================================
-- SECTION 6: Detailed Lifecycle View for Specific Batch
-- =============================================
-- Uncomment and modify BatchNumber to see full lifecycle of a specific batch

/*
SELECT 
    ID,
    TransactionType,
    PartID,
    BatchNumber,
    FromLocation,
    ToLocation,
    Operation,
    Quantity,
    User,
    ReceiveDate,
    Notes
FROM inv_transaction
WHERE BatchNumber = '0000010374'  -- Change this to your target batch
ORDER BY ReceiveDate ASC;
*/

-- =============================================
-- SECTION 7: Summary Statistics
-- =============================================

SELECT 
    'BATCH_SUMMARY' AS Report_Type,
    COUNT(DISTINCT BatchNumber) AS Total_Unique_Batches,
    COUNT(DISTINCT CASE WHEN TransactionType = 'IN' THEN BatchNumber END) AS Batches_With_IN,
    COUNT(DISTINCT CASE WHEN TransactionType = 'OUT' THEN BatchNumber END) AS Batches_With_OUT,
    COUNT(DISTINCT CASE WHEN TransactionType = 'TRANSFER' THEN BatchNumber END) AS Batches_With_TRANSFER,
    SUM(CASE WHEN TransactionType = 'IN' THEN 1 ELSE 0 END) AS Total_IN_Transactions,
    SUM(CASE WHEN TransactionType = 'OUT' THEN 1 ELSE 0 END) AS Total_OUT_Transactions,
    SUM(CASE WHEN TransactionType = 'TRANSFER' THEN 1 ELSE 0 END) AS Total_TRANSFER_Transactions
FROM inv_transaction;

-- =============================================
-- SECTION 8: Batches with Quantity Mismatches
-- =============================================
-- Find batches where total OUT quantity exceeds total IN quantity (data error!)

SELECT 
    'QUANTITY_MISMATCH' AS Analysis_Type,
    i.BatchNumber,
    i.PartID,
    i.Total_IN_Quantity,
    COALESCE(o.Total_OUT_Quantity, 0) AS Total_OUT_Quantity,
    (COALESCE(o.Total_OUT_Quantity, 0) - i.Total_IN_Quantity) AS Excess_OUT_Quantity
FROM (
    SELECT 
        BatchNumber,
        PartID,
        SUM(Quantity) AS Total_IN_Quantity
    FROM inv_transaction
    WHERE TransactionType = 'IN'
    GROUP BY BatchNumber, PartID
) i
LEFT JOIN (
    SELECT 
        BatchNumber,
        PartID,
        SUM(Quantity) AS Total_OUT_Quantity
    FROM inv_transaction
    WHERE TransactionType = 'OUT'
    GROUP BY BatchNumber, PartID
) o ON i.BatchNumber = o.BatchNumber AND i.PartID = o.PartID
WHERE 
    COALESCE(o.Total_OUT_Quantity, 0) > i.Total_IN_Quantity
ORDER BY Excess_OUT_Quantity DESC;

-- =============================================
-- USAGE INSTRUCTIONS
-- =============================================
-- 
-- Run entire script: mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms < find_batches_with_multiple_in_out.sql
-- 
-- Run specific section: Copy section query and run in MySQL client or workbench
-- 
-- Interpret results:
--   SECTION 1: Multiple INs = potential data entry duplicates
--   SECTION 2: Multiple OUTs = split scenarios or phased consumption
--   SECTION 3: Multiple TRANSFERs = normal manufacturing movement
--   SECTION 4: Complex batches = requires investigation
--   SECTION 5: Splits = partial consumption (expected in manufacturing)
--   SECTION 6: Detailed view = trace specific batch lifecycle
--   SECTION 7: Summary = overall database health metrics
--   SECTION 8: Quantity mismatches = data integrity errors
-- 
-- =============================================
-- End of find_batches_with_multiple_in_out.sql
-- =============================================
