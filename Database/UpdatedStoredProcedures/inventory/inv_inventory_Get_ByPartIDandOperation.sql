/*!50003 DROP PROCEDURE IF EXISTS `inv_inventory_Get_ByPartIDandOperation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_ByPartIDandOperation`(IN `p_PartID` VARCHAR(300), IN `p_Operation` VARCHAR(300), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving inventory by PartID and Operation';
    END;
    IF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
    ELSEIF p_Operation IS NULL OR p_Operation = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
    ELSE
        SELECT
            ID,
            PartID,
            Location,
            Operation,
            Quantity,
            ItemType,
            ReceiveDate,
            LastUpdated,
            User,
            BatchNumber AS `BatchNumber`,
            Notes,
            ColorCode,
            WorkOrder
        FROM inv_inventory
        WHERE PartID = p_PartID AND Operation = p_Operation;
        SELECT FOUND_ROWS() INTO v_Count;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = NULL;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No inventory found for PartID: ', p_PartID, ', Operation: ', p_Operation);
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

