/*!50003 DROP PROCEDURE IF EXISTS `inv_transactions_GetBatchLifecycle` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transactions_GetBatchLifecycle`(IN `p_BatchNumber` VARCHAR(50), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    
    IF p_BatchNumber IS NULL OR TRIM(p_BatchNumber) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Batch number is required';
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Batch number is required';
    END IF;
    
    
    SELECT COUNT(*)
    INTO v_Count
    FROM inv_transaction
    WHERE BatchNumber = p_BatchNumber;
    
    
    SELECT 
        ID,
        TransactionType,
        PartID,
        BatchNumber,
        Quantity,
        FromLocation,
        ToLocation,
        Operation,
        User,
        ReceiveDate,
        ItemType,
        Notes
    FROM inv_transaction
    WHERE BatchNumber = p_BatchNumber
    ORDER BY ReceiveDate ASC;
    
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Found ', v_Count, ' transaction(s) for batch ', p_BatchNumber);
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('No transactions found for batch ', p_BatchNumber);
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

