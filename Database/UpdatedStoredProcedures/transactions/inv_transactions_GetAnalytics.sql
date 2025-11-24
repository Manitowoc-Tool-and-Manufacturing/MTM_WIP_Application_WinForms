/*!50003 DROP PROCEDURE IF EXISTS `inv_transactions_GetAnalytics` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transactions_GetAnalytics`(IN `p_UserName` VARCHAR(100), IN `p_IsAdmin` BOOLEAN, IN `p_FromDate` DATETIME, IN `p_ToDate` DATETIME, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    
    
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
    
    
    SELECT 
        COUNT(*) as TotalTransactions,
        SUM(CASE WHEN TransactionType = 'IN' THEN 1 ELSE 0 END) as TotalIN,
        SUM(CASE WHEN TransactionType = 'OUT' THEN 1 ELSE 0 END) as TotalOUT,
        SUM(CASE WHEN TransactionType = 'TRANSFER' THEN 1 ELSE 0 END) as TotalTRANSFER
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate);
    
    
    SELECT 
        COALESCE(SUM(Quantity), 0) as TotalQuantityMoved,
        COALESCE(SUM(CASE WHEN TransactionType = 'IN' THEN Quantity ELSE 0 END), 0) as QuantityIN,
        COALESCE(SUM(CASE WHEN TransactionType = 'OUT' THEN Quantity ELSE 0 END), 0) as QuantityOUT,
        COALESCE(SUM(CASE WHEN TransactionType = 'TRANSFER' THEN Quantity ELSE 0 END), 0) as QuantityTRANSFER
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate);
    
    
    SELECT 
        MIN(ReceiveDate) as FirstTransactionDate,
        MAX(ReceiveDate) as LastTransactionDate
    FROM inv_transaction
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
      AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate);
    
    
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
    
    
    
    SELECT 
        Location as LocationName,
        COUNT(*) as TransactionCount
    FROM (
        SELECT FromLocation as Location
        FROM inv_transaction
        WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
          AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
          AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
          AND FromLocation IS NOT NULL
          AND FromLocation != ''
          AND FromLocation NOT IN ('FLOOR', 'RECEIVING', 'SHIPPING')  
        
        UNION ALL
        
        SELECT ToLocation as Location
        FROM inv_transaction
        WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR User = p_UserName)
          AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
          AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
          AND ToLocation IS NOT NULL
          AND ToLocation != ''
          AND ToLocation NOT IN ('FLOOR', 'RECEIVING', 'SHIPPING')  
    ) AS AllLocations
    GROUP BY Location
    ORDER BY COUNT(*) DESC
    LIMIT 1;
    
    
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
    
    
    
    SELECT 
        
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
                    THEN 'Increasing'
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
                    THEN 'Decreasing'
                    ELSE 'Stable'
                END
            ELSE 'Stable'
        END as TransactionRateTrend;
    
    
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

