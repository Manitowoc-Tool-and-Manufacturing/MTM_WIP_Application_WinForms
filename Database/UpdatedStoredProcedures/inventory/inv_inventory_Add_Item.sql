/*!50003 DROP PROCEDURE IF EXISTS `inv_inventory_Add_Item` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Add_Item`(IN `p_PartID` VARCHAR(100), IN `p_Location` VARCHAR(100), IN `p_Operation` VARCHAR(100), IN `p_Quantity` INT, IN `p_ItemType` VARCHAR(200), IN `p_User` VARCHAR(100), IN `p_Notes` VARCHAR(1000), IN `p_ColorCode` VARCHAR(50), IN `p_WorkOrder` VARCHAR(10), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE nextBatch BIGINT;
    DECLARE batchStr VARCHAR(10);
    DECLARE v_ColorCode VARCHAR(50);
    DECLARE v_WorkOrder VARCHAR(10);
    
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    
    
    IF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
    ELSEIF p_Location IS NULL OR p_Location = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Location is required';
    ELSEIF p_Operation IS NULL OR p_Operation = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
    ELSEIF p_Quantity IS NULL OR p_Quantity <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Quantity must be greater than 0';
    ELSEIF p_User IS NULL OR p_User = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSE
        
        SET v_ColorCode = COALESCE(p_ColorCode, 'Unknown');
        SET v_WorkOrder = COALESCE(p_WorkOrder, 'Unknown');
        
        
        SELECT last_batch_number INTO nextBatch FROM inv_inventory_batch_seq FOR UPDATE;
        SET nextBatch = nextBatch + 1;
        SET batchStr = LPAD(nextBatch, 10, '0');
        UPDATE inv_inventory_batch_seq SET last_batch_number = nextBatch;
        
        
        INSERT INTO inv_inventory
            (PartID, Location, Operation, Quantity, ItemType, User, BatchNumber, Notes, ColorCode, WorkOrder)
        VALUES
            (p_PartID, p_Location, p_Operation, p_Quantity, p_ItemType, p_User, batchStr, p_Notes, v_ColorCode, v_WorkOrder);
        
        SET v_RowsAffected = ROW_COUNT();
        
        
        INSERT INTO inv_transaction
            (
                TransactionType,
                BatchNumber,
                PartID,
                FromLocation,
                ToLocation,
                Operation,
                Quantity,
                Notes,
                User,
                ItemType,
                ColorCode,
                WorkOrder
            )
        VALUES
            (
                'IN',
                batchStr,
                p_PartID,
                NULL,
                p_Location,
                p_Operation,
                p_Quantity,
                p_Notes,
                p_User,
                p_ItemType,
                v_ColorCode,
                v_WorkOrder
            );
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Successfully added inventory item with batch number ', batchStr);
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = 'No rows were affected';
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

