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
    DECLARE v_Count INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    -- Use parameterized query with proper WHERE conditions instead of dynamic SQL
    SELECT 
        COUNT(*) as TotalTransactions,
        SUM(CASE WHEN TransactionType = 'IN' THEN 1 ELSE 0 END) as InTransactions,
        SUM(CASE WHEN TransactionType = 'OUT' THEN 1 ELSE 0 END) as OutTransactions,
        SUM(CASE WHEN TransactionType = 'TRANSFER' THEN 1 ELSE 0 END) as TransferTransactions,
        COALESCE(SUM(Quantity), 0) as TotalQuantity,
        COUNT(DISTINCT PartID) as UniquePartIds,
        COUNT(DISTINCT User) as ActiveUsers,
        (SELECT PartID FROM inv_transaction t2
         WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR t2.User = p_UserName)
           AND (p_FromDate IS NULL OR t2.ReceiveDate >= p_FromDate)
           AND (p_ToDate IS NULL OR t2.ReceiveDate <= p_ToDate)
         GROUP BY PartID ORDER BY SUM(Quantity) DESC LIMIT 1) as TopPartId,
        (SELECT User FROM inv_transaction t3
         WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR t3.User = p_UserName)
           AND (p_FromDate IS NULL OR t3.ReceiveDate >= p_FromDate)
           AND (p_ToDate IS NULL OR t3.ReceiveDate <= p_ToDate)
         GROUP BY User ORDER BY COUNT(*) DESC LIMIT 1) as TopUser
    FROM inv_transaction t1
    WHERE (p_IsAdmin OR p_UserName IS NULL OR LENGTH(TRIM(p_UserName)) = 0 OR t1.User = p_UserName)
      AND (p_FromDate IS NULL OR t1.ReceiveDate >= p_FromDate)
      AND (p_ToDate IS NULL OR t1.ReceiveDate <= p_ToDate);
    
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No transaction data found for analytics';
    END IF;
END
//
DELIMITER ;
