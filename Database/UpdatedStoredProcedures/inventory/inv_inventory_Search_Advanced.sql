/*!50003 DROP PROCEDURE IF EXISTS `inv_inventory_Search_Advanced` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Search_Advanced`(IN `p_PartID` VARCHAR(300), IN `p_Operation` VARCHAR(300), IN `p_Location` VARCHAR(300), IN `p_QtyMin` DECIMAL(10,2), IN `p_QtyMax` DECIMAL(10,2), IN `p_Notes` TEXT, IN `p_User` VARCHAR(300), IN `p_FilterByDate` BOOLEAN, IN `p_DateFrom` DATETIME, IN `p_DateTo` DATETIME, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while searching inventory';
        ROLLBACK;
    END;
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
        BatchNumber,
        Notes
    FROM inv_inventory
    WHERE
        (p_PartID IS NULL OR p_PartID = '' OR PartID LIKE CONCAT('%', p_PartID, '%'))
        AND (p_Operation IS NULL OR p_Operation = '' OR Operation LIKE CONCAT('%', p_Operation, '%'))
        AND (p_Location IS NULL OR p_Location = '' OR Location LIKE CONCAT('%', p_Location, '%'))
        AND (p_QtyMin IS NULL OR Quantity >= p_QtyMin)
        AND (p_QtyMax IS NULL OR Quantity <= p_QtyMax)
        AND (p_Notes IS NULL OR p_Notes = '' OR Notes LIKE CONCAT('%', p_Notes, '%'))
        AND (p_User IS NULL OR p_User = '' OR User LIKE CONCAT('%', p_User, '%'))
        AND (
            p_FilterByDate = FALSE
            OR (
                (p_DateFrom IS NULL OR LastUpdated >= p_DateFrom)
                AND (p_DateTo IS NULL OR LastUpdated <= p_DateTo)
            )
        )
    ORDER BY LastUpdated DESC;
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 1;
        SET p_ErrorMsg = 'No inventory records found matching search criteria';
    END IF;
    COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

