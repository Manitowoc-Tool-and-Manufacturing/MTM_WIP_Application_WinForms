DELIMITER //
DROP PROCEDURE IF EXISTS `inv_transactions_GetAnalytics`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transactions_GetAnalytics`(
    IN p_UserName VARCHAR(100),
    IN p_IsAdmin BOOLEAN,
    IN p_FromDate DATETIME,
    IN p_ToDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    /*
    ============================================================================
    Procedure: inv_transactions_GetAnalytics
    Purpose: Get comprehensive transaction analytics for the given date range
    
    Parameters:
        p_UserName - Username for filtering (if not admin)
        p_IsAdmin - Whether user has admin privileges (sees all data)
        p_FromDate - Start date for analytics (inclusive)
        p_ToDate - End date for analytics (inclusive)
        p_Status - Output status (0=success with data, 1=success no data, -1=error)
        p_ErrorMsg - Output error message
    
    Returns 10 result sets:
        1. Transaction counts by type
        2. Quantity totals by type
        3. Date range (first/last transaction)
        4. Most active user
        5. Most transacted part (all transactions)
        6. Busiest location
        7. Most transferred part (TRANSFER only)
        8. Busiest day of week
        9. Peak hour
        10. Transaction rate metrics
    
    Author: Copilot
    Date: 2025-11-07
    ============================================================================
    */
    
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    
    -- Result Set 1: Transaction counts by type
    SELECT 
        COUNT(*) as TotalTransactions,
        SUM(CASE WHEN TransactionType = 'IN' THEN 1 ELSE 0 END) as TotalIN,
        SUM(CASE WHEN TransactionType = 'OUT' THEN 1 ELSE 0 END) as TotalOUT,
        SUM(CASE WHEN TransactionType = 'TRANSFER' THEN 1 ELSE 0 END) as TotalTRANSFER
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate);
    
    -- Result Set 2: Quantity totals by type
    SELECT 
        COALESCE(SUM(Quantity), 0) as TotalQuantityMoved,
        COALESCE(SUM(CASE WHEN TransactionType = 'IN' THEN Quantity ELSE 0 END), 0) as QuantityIN,
        COALESCE(SUM(CASE WHEN TransactionType = 'OUT' THEN Quantity ELSE 0 END), 0) as QuantityOUT,
        COALESCE(SUM(CASE WHEN TransactionType = 'TRANSFER' THEN Quantity ELSE 0 END), 0) as QuantityTRANSFER
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate);
    
    -- Result Set 3: Date range (first/last transaction)
    SELECT 
        MIN(ReceiveDate) as FirstTransactionDate,
        MAX(ReceiveDate) as LastTransactionDate
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate);
    
    -- Result Set 4: Most active user
    SELECT 
        COALESCE(User, '') as UserName,
        COUNT(*) as TransactionCount
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
      AND User IS NOT NULL
      AND User != ''
    GROUP BY User
    ORDER BY COUNT(*) DESC
    LIMIT 1;
    
    -- Result Set 5: Most transacted part (all transaction types)
    SELECT 
        COALESCE(PartID, '') as PartID,
        COUNT(*) as TransactionCount
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
      AND PartID IS NOT NULL
      AND PartID != ''
    GROUP BY PartID
    ORDER BY COUNT(*) DESC
    LIMIT 1;
    
    -- Result Set 6: Busiest location (FromLocation or ToLocation)
    SELECT 
        COALESCE(Location, '') as LocationName,
        COUNT(*) as TransactionCount
    FROM (
        SELECT FromLocation as Location
        FROM inv_transaction
        WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
          AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
          AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
          AND FromLocation IS NOT NULL
          AND FromLocation != ''
        
        UNION ALL
        
        SELECT ToLocation as Location
        FROM inv_transaction
        WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
          AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
          AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
          AND ToLocation IS NOT NULL
          AND ToLocation != ''
    ) AS AllLocations
    GROUP BY Location
    ORDER BY COUNT(*) DESC
    LIMIT 1;
    
    -- Result Set 7: Most transferred part (TRANSFER transactions only)
    SELECT 
        COALESCE(PartID, '') as PartID,
        COUNT(*) as TransferCount
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
      AND TransactionType = 'TRANSFER'
      AND PartID IS NOT NULL
      AND PartID != ''
    GROUP BY PartID
    ORDER BY COUNT(*) DESC
    LIMIT 1;
    
    -- Result Set 8: Busiest day of week
    SELECT 
        DAYNAME(ReceiveDate) as DayName,
        COUNT(*) as TransactionCount
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
    GROUP BY DAYOFWEEK(ReceiveDate), DAYNAME(ReceiveDate)
    ORDER BY COUNT(*) DESC
    LIMIT 1;
    
    -- Result Set 9: Peak hour (0-23)
    SELECT 
        HOUR(ReceiveDate) as Hour,
        COUNT(*) as TransactionCount
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
    GROUP BY HOUR(ReceiveDate)
    ORDER BY COUNT(*) DESC
    LIMIT 1;
    
    -- Result Set 10: Transaction rate metrics
    -- Calculate daily average and trend (comparing first half vs second half of date range)
    SELECT 
        -- Overall transaction rate (transactions per day)
        CASE 
            WHEN p_FromDate IS NOT NULL AND p_ToDate IS NOT NULL AND DATEDIFF(p_ToDate, p_FromDate) > 0 
            THEN (
                SELECT COUNT(*) 
                FROM inv_transaction 
                WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
                  AND ReceiveDate BETWEEN p_FromDate AND p_ToDate
            ) / DATEDIFF(p_ToDate, p_FromDate)
            ELSE (
                SELECT COUNT(*) 
                FROM inv_transaction 
                WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
                  AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
                  AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
            )
        END as TransactionRate,
        
        -- Trend: Compare first half vs second half
        CASE 
            WHEN p_FromDate IS NOT NULL AND p_ToDate IS NOT NULL AND DATEDIFF(p_ToDate, p_FromDate) > 1 THEN
                CASE 
                    WHEN (
                        SELECT COUNT(*) 
                        FROM inv_transaction 
                        WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
                          AND ReceiveDate BETWEEN 
                            DATE_ADD(p_FromDate, INTERVAL DATEDIFF(p_ToDate, p_FromDate) / 2 DAY) 
                            AND p_ToDate
                    ) > (
                        SELECT COUNT(*) 
                        FROM inv_transaction 
                        WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
                          AND ReceiveDate BETWEEN 
                            p_FromDate 
                            AND DATE_ADD(p_FromDate, INTERVAL DATEDIFF(p_ToDate, p_FromDate) / 2 DAY)
                    )
                    THEN '📈 Increasing'
                    WHEN (
                        SELECT COUNT(*) 
                        FROM inv_transaction 
                        WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
                          AND ReceiveDate BETWEEN 
                            DATE_ADD(p_FromDate, INTERVAL DATEDIFF(p_ToDate, p_FromDate) / 2 DAY) 
                            AND p_ToDate
                    ) < (
                        SELECT COUNT(*) 
                        FROM inv_transaction 
                        WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
                          AND ReceiveDate BETWEEN 
                            p_FromDate 
                            AND DATE_ADD(p_FromDate, INTERVAL DATEDIFF(p_ToDate, p_FromDate) / 2 DAY)
                    )
                    THEN '📉 Decreasing'
                    ELSE '➡️ Stable'
                END
            ELSE '➡️ Stable'
        END as TransactionRateTrend;
    
    -- Set status based on whether we found any data
    SELECT COUNT(*) INTO v_Count
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate);
    
    IF v_Count > 0 THEN
        SET p_Status = 0;
        SET p_ErrorMsg = 'Analytics retrieved successfully';
    ELSE
        SET p_Status = 1;
        SET p_ErrorMsg = 'No transaction data found for analytics';
    END IF;
    
END
//
DELIMITER ;

