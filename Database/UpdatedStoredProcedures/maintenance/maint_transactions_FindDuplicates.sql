/*!50003 DROP PROCEDURE IF EXISTS `maint_transactions_FindDuplicates` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `maint_transactions_FindDuplicates`(IN `p_DuplicateType` VARCHAR(50), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    
    
    IF p_DuplicateType IS NULL OR p_DuplicateType = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'DuplicateType is required (EXACT, SAME_TIMESTAMP, or ALL)';
        SELECT p_Status AS Status, p_ErrorMsg AS ErrorMsg;
    ELSEIF p_DuplicateType NOT IN ('EXACT', 'SAME_TIMESTAMP', 'ALL') THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid DuplicateType. Must be EXACT, SAME_TIMESTAMP, or ALL';
        SELECT p_Status AS Status, p_ErrorMsg AS ErrorMsg;
    ELSE
        
        IF p_DuplicateType = 'EXACT' THEN
            
            SELECT 
                'EXACT_DUPLICATE' AS DuplicateType,
                t.ID,
                t.BatchNumber,
                t.PartID,
                t.TransactionType,
                t.FromLocation,
                t.ToLocation,
                t.Operation,
                t.Quantity,
                t.User,
                t.ReceiveDate,
                t.ItemType,
                t.Notes,
                (SELECT MIN(t2.ID) 
                 FROM inv_transaction t2
                 WHERE t2.BatchNumber <=> t.BatchNumber
                 AND t2.PartID = t.PartID
                 AND t2.TransactionType = t.TransactionType
                 AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
                 AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
                 AND t2.Operation = t.Operation
                 AND t2.Quantity = t.Quantity
                 AND t2.User = t.User
                 AND t2.ReceiveDate = t.ReceiveDate
                ) AS MinID_ToKeep,
                'DELETE' AS Action
            FROM inv_transaction t
            WHERE EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.ID < t.ID
                AND t2.BatchNumber <=> t.BatchNumber
                AND t2.PartID = t.PartID
                AND t2.TransactionType = t.TransactionType
                AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
                AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
                AND t2.Operation = t.Operation
                AND t2.Quantity = t.Quantity
                AND t2.User = t.User
                AND t2.ReceiveDate = t.ReceiveDate
            )
            ORDER BY t.BatchNumber, t.ID;
            
        ELSEIF p_DuplicateType = 'SAME_TIMESTAMP' THEN
            
            SELECT 
                'SAME_TIMESTAMP_OUT' AS DuplicateType,
                t1.ID,
                t1.BatchNumber,
                t1.PartID,
                t1.TransactionType,
                t1.FromLocation,
                t1.ToLocation,
                t1.Operation,
                t1.Quantity,
                t1.User,
                t1.ReceiveDate,
                t1.ItemType,
                t1.Notes,
                (SELECT t2.ID 
                 FROM inv_transaction t2
                 WHERE t2.BatchNumber = t1.BatchNumber
                 AND t2.TransactionType = 'IN'
                 AND t2.ReceiveDate = t1.ReceiveDate
                 LIMIT 1
                ) AS Matching_IN_ID,
                'DELETE' AS Action
            FROM inv_transaction t1
            WHERE t1.TransactionType = 'OUT'
            AND EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.BatchNumber = t1.BatchNumber
                AND t2.TransactionType = 'IN'
                AND t2.ReceiveDate = t1.ReceiveDate
            )
            ORDER BY t1.BatchNumber, t1.ID;
            
        ELSE 
            
            
            
            DROP TEMPORARY TABLE IF EXISTS temp_all_duplicates;
            CREATE TEMPORARY TABLE temp_all_duplicates (
                DuplicateType VARCHAR(50),
                ID BIGINT,
                BatchNumber VARCHAR(20),
                PartID VARCHAR(100),
                TransactionType VARCHAR(20),
                FromLocation VARCHAR(100),
                ToLocation VARCHAR(100),
                Operation VARCHAR(100),
                Quantity INT,
                User VARCHAR(100),
                ReceiveDate DATETIME,
                ItemType VARCHAR(200),
                Notes VARCHAR(1000),
                Reference_ID BIGINT,
                Action VARCHAR(20)
            );
            
            
            INSERT INTO temp_all_duplicates
            SELECT 
                'EXACT_DUPLICATE' AS DuplicateType,
                t.ID,
                t.BatchNumber,
                t.PartID,
                t.TransactionType,
                t.FromLocation,
                t.ToLocation,
                t.Operation,
                t.Quantity,
                t.User,
                t.ReceiveDate,
                t.ItemType,
                t.Notes,
                (SELECT MIN(t2.ID) 
                 FROM inv_transaction t2
                 WHERE t2.BatchNumber <=> t.BatchNumber
                 AND t2.PartID = t.PartID
                 AND t2.TransactionType = t.TransactionType
                 AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
                 AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
                 AND t2.Operation = t.Operation
                 AND t2.Quantity = t.Quantity
                 AND t2.User = t.User
                 AND t2.ReceiveDate = t.ReceiveDate
                ) AS Reference_ID,
                'DELETE' AS Action
            FROM inv_transaction t
            WHERE EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.ID < t.ID
                AND t2.BatchNumber <=> t.BatchNumber
                AND t2.PartID = t.PartID
                AND t2.TransactionType = t.TransactionType
                AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
                AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
                AND t2.Operation = t.Operation
                AND t2.Quantity = t.Quantity
                AND t2.User = t.User
                AND t2.ReceiveDate = t.ReceiveDate
            );
            
            
            INSERT INTO temp_all_duplicates
            SELECT 
                'SAME_TIMESTAMP_OUT' AS DuplicateType,
                t1.ID,
                t1.BatchNumber,
                t1.PartID,
                t1.TransactionType,
                t1.FromLocation,
                t1.ToLocation,
                t1.Operation,
                t1.Quantity,
                t1.User,
                t1.ReceiveDate,
                t1.ItemType,
                t1.Notes,
                (SELECT t2.ID 
                 FROM inv_transaction t2
                 WHERE t2.BatchNumber = t1.BatchNumber
                 AND t2.TransactionType = 'IN'
                 AND t2.ReceiveDate = t1.ReceiveDate
                 LIMIT 1
                ) AS Reference_ID,
                'DELETE' AS Action
            FROM inv_transaction t1
            WHERE t1.TransactionType = 'OUT'
            AND EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.BatchNumber = t1.BatchNumber
                AND t2.TransactionType = 'IN'
                AND t2.ReceiveDate = t1.ReceiveDate
            );
            
            
            SELECT * FROM temp_all_duplicates
            ORDER BY DuplicateType, BatchNumber, ID;
            
            DROP TEMPORARY TABLE IF EXISTS temp_all_duplicates;
        END IF;
        
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Found duplicates of type: ', p_DuplicateType);
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

