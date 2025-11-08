-- ============================================================================
-- COMPREHENSIVE INVENTORY DATA INTEGRITY VALIDATION
-- ============================================================================
-- Date: 2025-11-07
-- Purpose: Validate IN/OUT/TRANSFER transactions against inv_inventory
-- ============================================================================

-- ============================================================================
-- SECTION 1: IN TRANSACTION VALIDATION
-- ============================================================================
SELECT '==================================================' AS '';
SELECT 'IN TRANSACTION VALIDATION' AS '';
SELECT '==================================================' AS '';

SELECT 
    'Total IN Transactions' AS Metric,
    COUNT(*) AS Count
FROM inv_transaction
WHERE TransactionType = 'IN'

UNION ALL

SELECT 
    'Distinct IN Batches',
    COUNT(DISTINCT BatchNumber)
FROM inv_transaction
WHERE TransactionType = 'IN'

UNION ALL

SELECT 
    'Still in Inventory',
    COUNT(DISTINCT t.BatchNumber)
FROM inv_transaction t
INNER JOIN inv_inventory i ON t.BatchNumber = i.BatchNumber
WHERE t.TransactionType = 'IN'

UNION ALL

SELECT 
    'Removed via OUT',
    COUNT(DISTINCT t_in.BatchNumber)
FROM inv_transaction t_in
INNER JOIN inv_transaction t_out ON t_in.BatchNumber = t_out.BatchNumber AND t_out.TransactionType = 'OUT'
LEFT JOIN inv_inventory i ON t_in.BatchNumber = i.BatchNumber
WHERE t_in.TransactionType = 'IN' AND i.BatchNumber IS NULL

UNION ALL

SELECT 
    'Removed via TRANSFER',
    COUNT(DISTINCT t_in.BatchNumber)
FROM inv_transaction t_in
INNER JOIN inv_transaction t_xfer ON t_in.BatchNumber = t_xfer.BatchNumber AND t_xfer.TransactionType = 'TRANSFER'
LEFT JOIN inv_inventory i ON t_in.BatchNumber = i.BatchNumber
LEFT JOIN inv_transaction t_out ON t_in.BatchNumber = t_out.BatchNumber AND t_out.TransactionType = 'OUT'
WHERE t_in.TransactionType = 'IN' AND i.BatchNumber IS NULL AND t_out.BatchNumber IS NULL

UNION ALL

SELECT 
    '❌ MISSING UNEXPLAINED',
    COUNT(DISTINCT t_in.BatchNumber)
FROM inv_transaction t_in
LEFT JOIN inv_inventory i ON t_in.BatchNumber = i.BatchNumber
LEFT JOIN inv_transaction t_out ON t_in.BatchNumber = t_out.BatchNumber AND t_out.TransactionType = 'OUT'
LEFT JOIN inv_transaction t_xfer ON t_in.BatchNumber = t_xfer.BatchNumber AND t_xfer.TransactionType = 'TRANSFER'
WHERE t_in.TransactionType = 'IN' 
  AND i.BatchNumber IS NULL 
  AND t_out.BatchNumber IS NULL 
  AND t_xfer.BatchNumber IS NULL;

-- ============================================================================
-- SECTION 2: OUT TRANSACTION VALIDATION
-- ============================================================================
SELECT '' AS '';
SELECT '==================================================' AS '';
SELECT 'OUT TRANSACTION VALIDATION' AS '';
SELECT '==================================================' AS '';

SELECT 
    'Total OUT Transactions' AS Metric,
    COUNT(*) AS Count
FROM inv_transaction
WHERE TransactionType = 'OUT'

UNION ALL

SELECT 
    'Distinct OUT Batches',
    COUNT(DISTINCT BatchNumber)
FROM inv_transaction
WHERE TransactionType = 'OUT'

UNION ALL

SELECT 
    'Had Matching IN',
    COUNT(DISTINCT t_out.BatchNumber)
FROM inv_transaction t_out
INNER JOIN inv_transaction t_in ON t_out.BatchNumber = t_in.BatchNumber AND t_in.TransactionType = 'IN'
WHERE t_out.TransactionType = 'OUT'

UNION ALL

SELECT 
    '❌ ORPHANED OUTs (No IN)',
    COUNT(DISTINCT t_out.BatchNumber)
FROM inv_transaction t_out
LEFT JOIN inv_transaction t_in ON t_out.BatchNumber = t_in.BatchNumber AND t_in.TransactionType = 'IN'
WHERE t_out.TransactionType = 'OUT' AND t_in.BatchNumber IS NULL;

-- ============================================================================
-- SECTION 3: TRANSFER TRANSACTION VALIDATION
-- ============================================================================
SELECT '' AS '';
SELECT '==================================================' AS '';
SELECT 'TRANSFER TRANSACTION VALIDATION' AS '';
SELECT '==================================================' AS '';

SELECT 
    'Total TRANSFER Transactions' AS Metric,
    COUNT(*) AS Count
FROM inv_transaction
WHERE TransactionType = 'TRANSFER'

UNION ALL

SELECT 
    'Distinct TRANSFER Batches',
    COUNT(DISTINCT BatchNumber)
FROM inv_transaction
WHERE TransactionType = 'TRANSFER'

UNION ALL

SELECT 
    'Had Matching IN',
    COUNT(DISTINCT t_xfer.BatchNumber)
FROM inv_transaction t_xfer
INNER JOIN inv_transaction t_in ON t_xfer.BatchNumber = t_in.BatchNumber AND t_in.TransactionType = 'IN'
WHERE t_xfer.TransactionType = 'TRANSFER'

UNION ALL

SELECT 
    'Still in Inventory',
    COUNT(DISTINCT t_xfer.BatchNumber)
FROM inv_transaction t_xfer
INNER JOIN inv_inventory i ON t_xfer.BatchNumber = i.BatchNumber
WHERE t_xfer.TransactionType = 'TRANSFER'

UNION ALL

SELECT 
    '❌ ORPHANED TRANSFERs (No IN)',
    COUNT(DISTINCT t_xfer.BatchNumber)
FROM inv_transaction t_xfer
LEFT JOIN inv_transaction t_in ON t_xfer.BatchNumber = t_in.BatchNumber AND t_in.TransactionType = 'IN'
WHERE t_xfer.TransactionType = 'TRANSFER' AND t_in.BatchNumber IS NULL;

-- ============================================================================
-- SECTION 4: TIMESTAMP VALIDATION (OUT/TRANSFER after IN)
-- ============================================================================
SELECT '' AS '';
SELECT '==================================================' AS '';
SELECT 'TIMESTAMP VALIDATION' AS '';
SELECT '==================================================' AS '';

SELECT 
    '❌ OUTs BEFORE INs' AS Metric,
    COUNT(*) AS Count
FROM inv_transaction t_out
INNER JOIN inv_transaction t_in ON t_out.BatchNumber = t_in.BatchNumber AND t_in.TransactionType = 'IN'
WHERE t_out.TransactionType = 'OUT' AND t_out.ReceiveDate < t_in.ReceiveDate

UNION ALL

SELECT 
    '❌ TRANSFERs BEFORE INs',
    COUNT(*)
FROM inv_transaction t_xfer
INNER JOIN inv_transaction t_in ON t_xfer.BatchNumber = t_in.BatchNumber AND t_in.TransactionType = 'IN'
WHERE t_xfer.TransactionType = 'TRANSFER' AND t_xfer.ReceiveDate < t_in.ReceiveDate;

-- ============================================================================
-- SECTION 5: TRANSFER LOCATION VALIDATION
-- ============================================================================
SELECT '' AS '';
SELECT '==================================================' AS '';
SELECT 'TRANSFER LOCATION VALIDATION' AS '';
SELECT '==================================================' AS '';

SELECT 
    'TRANSFERs with Valid Locations' AS Metric,
    COUNT(*) AS Count
FROM inv_transaction
WHERE TransactionType = 'TRANSFER' 
  AND FromLocation IS NOT NULL 
  AND ToLocation IS NOT NULL
  AND FromLocation != ToLocation

UNION ALL

SELECT 
    '❌ TRANSFERs Missing FromLocation',
    COUNT(*)
FROM inv_transaction
WHERE TransactionType = 'TRANSFER' AND FromLocation IS NULL

UNION ALL

SELECT 
    '❌ TRANSFERs Missing ToLocation',
    COUNT(*)
FROM inv_transaction
WHERE TransactionType = 'TRANSFER' AND ToLocation IS NULL

UNION ALL

SELECT 
    '❌ TRANSFERs Same From/To Location',
    COUNT(*)
FROM inv_transaction
WHERE TransactionType = 'TRANSFER' 
  AND FromLocation = ToLocation
  AND FromLocation IS NOT NULL;

-- ============================================================================
-- SECTION 6: PROBLEMATIC BATCH DETAILS
-- ============================================================================
SELECT '' AS '';
SELECT '==================================================' AS '';
SELECT 'PROBLEMATIC BATCHES - MISSING UNEXPLAINED INs' AS '';
SELECT '==================================================' AS '';

SELECT 
    t_in.BatchNumber,
    t_in.PartID,
    t_in.Quantity,
    t_in.User,
    t_in.ReceiveDate,
    DATEDIFF(CURDATE(), t_in.ReceiveDate) AS DaysOld
FROM inv_transaction t_in
LEFT JOIN inv_inventory i ON t_in.BatchNumber = i.BatchNumber
LEFT JOIN inv_transaction t_out ON t_in.BatchNumber = t_out.BatchNumber AND t_out.TransactionType = 'OUT'
LEFT JOIN inv_transaction t_xfer ON t_in.BatchNumber = t_xfer.BatchNumber AND t_xfer.TransactionType = 'TRANSFER'
WHERE t_in.TransactionType = 'IN' 
  AND i.BatchNumber IS NULL 
  AND t_out.BatchNumber IS NULL 
  AND t_xfer.BatchNumber IS NULL
ORDER BY t_in.ReceiveDate DESC
LIMIT 50;

SELECT '' AS '';
SELECT '==================================================' AS '';
SELECT 'PROBLEMATIC BATCHES - ORPHANED OUTs' AS '';
SELECT '==================================================' AS '';

SELECT 
    t_out.BatchNumber,
    t_out.PartID,
    t_out.Quantity,
    t_out.User,
    t_out.ReceiveDate
FROM inv_transaction t_out
LEFT JOIN inv_transaction t_in ON t_out.BatchNumber = t_in.BatchNumber AND t_in.TransactionType = 'IN'
WHERE t_out.TransactionType = 'OUT' AND t_in.BatchNumber IS NULL
ORDER BY t_out.ReceiveDate DESC
LIMIT 50;

SELECT '' AS '';
SELECT '==================================================' AS '';
SELECT 'PROBLEMATIC BATCHES - ORPHANED TRANSFERs' AS '';
SELECT '==================================================' AS '';

SELECT 
    t_xfer.BatchNumber,
    t_xfer.PartID,
    t_xfer.FromLocation,
    t_xfer.ToLocation,
    t_xfer.Quantity,
    t_xfer.User,
    t_xfer.ReceiveDate
FROM inv_transaction t_xfer
LEFT JOIN inv_transaction t_in ON t_xfer.BatchNumber = t_in.BatchNumber AND t_in.TransactionType = 'IN'
WHERE t_xfer.TransactionType = 'TRANSFER' AND t_in.BatchNumber IS NULL
ORDER BY t_xfer.ReceiveDate DESC
LIMIT 50;

SELECT '' AS '';
SELECT '==================================================' AS '';
SELECT 'VALIDATION COMPLETE' AS '';
SELECT '==================================================' AS '';
