/*!50003 DROP PROCEDURE IF EXISTS `inv_transactions_SmartSearch` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transactions_SmartSearch`(IN `p_WhereClause` TEXT, IN `p_Page` INT, IN `p_PageSize` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE v_ErrorMessage VARCHAR(500) DEFAULT '';
    DECLARE v_Offset INT DEFAULT 0;
    DECLARE v_FinalWhereClause TEXT DEFAULT '';
    
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    IF p_Page < 1 THEN SET p_Page = 1; END IF;
    IF p_PageSize < 1 OR p_PageSize > 1000 THEN SET p_PageSize = 20; END IF;
    SET v_Offset = (p_Page - 1) * p_PageSize;
    IF p_WhereClause IS NOT NULL AND LENGTH(TRIM(p_WhereClause)) > 0 THEN
        SET v_FinalWhereClause = CONCAT('WHERE ', TRIM(p_WhereClause));
    ELSE
        SET v_FinalWhereClause = 'WHERE 1=1';
    END IF;
    
    SET @countSql = CONCAT(
        'SELECT COUNT(*) INTO @totalCount FROM inv_transaction ',
        v_FinalWhereClause
    );
    PREPARE countStmt FROM @countSql;
    EXECUTE countStmt;
    DEALLOCATE PREPARE countStmt;
    SET v_Count = @totalCount;
    
    
    SET @sql = CONCAT(
        'SELECT ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, ',
        'Operation, Quantity, Notes, User, ItemType, ReceiveDate ',
        'FROM inv_transaction ',
        v_FinalWhereClause, ' ',
        'ORDER BY ReceiveDate DESC ',
        'LIMIT ', v_Offset, ', ', p_PageSize
    );
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Found ', v_Count, ' transaction(s) matching criteria');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No transactions found matching search criteria';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

