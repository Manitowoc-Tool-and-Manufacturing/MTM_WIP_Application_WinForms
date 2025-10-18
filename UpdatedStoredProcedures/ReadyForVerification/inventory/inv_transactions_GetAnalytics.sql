-- =============================================
-- Procedure: inv_transactions_GetAnalytics
-- Domain: inventory
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

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

    DECLARE v_ErrorMessage VARCHAR(500) DEFAULT '';

    DECLARE v_WhereClause TEXT DEFAULT '';

    

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        ROLLBACK;

        GET DIAGNOSTICS CONDITION 1

            @sqlstate = RETURNED_SQLSTATE, 

            @errno = MYSQL_ERRNO, 

            @text = MESSAGE_TEXT;

        SET p_Status = -1;

        SET p_ErrorMsg = CONCAT('Database error: ', @text);

    END;

    

    START TRANSACTION;

    

    -- Build WHERE clause

    SET v_WhereClause = 'WHERE 1=1 ';

    

    IF NOT p_IsAdmin AND p_UserName IS NOT NULL AND LENGTH(TRIM(p_UserName)) > 0 THEN

        SET v_WhereClause = CONCAT(v_WhereClause, 'AND User = ''', REPLACE(p_UserName, '''', ''''''), ''' ');

    END IF;

    

    IF p_FromDate IS NOT NULL THEN

        SET v_WhereClause = CONCAT(v_WhereClause, 'AND ReceiveDate >= ''', p_FromDate, ''' ');

    END IF;

    

    IF p_ToDate IS NOT NULL THEN

        SET v_WhereClause = CONCAT(v_WhereClause, 'AND ReceiveDate <= ''', p_ToDate, ''' ');

    END IF;

    

    -- Execute analytics query

    SET @sql = CONCAT(

        'SELECT ',

        '    COUNT(*) as TotalTransactions, ',

        '    SUM(CASE WHEN TransactionType = ''IN'' THEN 1 ELSE 0 END) as InTransactions, ',

        '    SUM(CASE WHEN TransactionType = ''OUT'' THEN 1 ELSE 0 END) as OutTransactions, ',

        '    SUM(CASE WHEN TransactionType = ''TRANSFER'' THEN 1 ELSE 0 END) as TransferTransactions, ',

        '    COALESCE(SUM(Quantity), 0) as TotalQuantity, ',

        '    COUNT(DISTINCT PartID) as UniquePartIds, ',

        '    COUNT(DISTINCT User) as ActiveUsers, ',

        '    COALESCE((SELECT PartID FROM inv_transaction t2 ', v_WhereClause, ' GROUP BY PartID ORDER BY SUM(Quantity) DESC LIMIT 1), '''') as TopPartId, ',

        '    COALESCE((SELECT User FROM inv_transaction t3 ', v_WhereClause, ' GROUP BY User ORDER BY COUNT(*) DESC LIMIT 1), '''') as TopUser ',

        'FROM inv_transaction t1 ',

        v_WhereClause

    );

    

    PREPARE stmt FROM @sql;

    EXECUTE stmt;

    DEALLOCATE PREPARE stmt;

    

    SELECT FOUND_ROWS() INTO v_Count;

    

    COMMIT;

    

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

-- =============================================
-- End of inv_transactions_GetAnalytics
-- =============================================
