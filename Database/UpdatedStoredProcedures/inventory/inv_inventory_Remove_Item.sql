/*!50003 DROP PROCEDURE IF EXISTS `inv_inventory_Remove_Item` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Remove_Item`(IN `p_PartID` VARCHAR(300), IN `p_Location` VARCHAR(100), IN `p_Operation` VARCHAR(100), IN `p_Quantity` INT, IN `p_ItemType` VARCHAR(100), IN `p_User` VARCHAR(100), IN `p_BatchNumber` VARCHAR(100), IN `p_Notes` VARCHAR(1000), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(255))
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE v_ErrorMessage TEXT DEFAULT '';
    DECLARE v_RecordCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_ErrorMessage = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error occurred while removing inventory item for part: ', p_PartID);
    END;
    IF p_Quantity <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Quantity must be greater than zero';
    ELSE
        SELECT COUNT(*) INTO v_RecordCount FROM inv_inventory WHERE PartID = p_PartID AND Location = p_Location AND Operation = p_Operation;
        IF v_RecordCount = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('No inventory records found for PartID: ', p_PartID, ', Location: ', p_Location, ', Operation: ', p_Operation);
        ELSE
            DELETE FROM inv_inventory WHERE PartID = p_PartID AND Location = p_Location AND Operation = p_Operation AND Quantity = p_Quantity
                AND (p_BatchNumber IS NULL OR p_BatchNumber = '' OR BatchNumber = p_BatchNumber)
                AND (p_Notes IS NULL OR p_Notes = '' OR Notes IS NULL OR Notes = '' OR Notes = p_Notes) LIMIT 1;
            SET v_RowsAffected = ROW_COUNT();
            IF v_RowsAffected = 0 THEN
                DELETE FROM inv_inventory WHERE PartID = p_PartID AND Location = p_Location AND Operation = p_Operation AND Quantity = p_Quantity LIMIT 1;
                SET v_RowsAffected = ROW_COUNT();
            END IF;
            IF v_RowsAffected > 0 THEN
                INSERT INTO inv_transaction (TransactionType, PartID, FromLocation, Operation, Quantity, ItemType, User, BatchNumber, Notes, ReceiveDate)
                VALUES ('OUT', p_PartID, p_Location, p_Operation, p_Quantity, p_ItemType, p_User, p_BatchNumber, p_Notes, NOW());
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Inventory item removed successfully for part: ', p_PartID, ', quantity: ', p_Quantity);
            ELSE
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('No matching inventory item found for removal. Found ', v_RecordCount, ' records for PartID: ', p_PartID, ', Location: ', p_Location, ', Operation: ', p_Operation, ' but none matched Quantity: ', p_Quantity);
            END IF;
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

