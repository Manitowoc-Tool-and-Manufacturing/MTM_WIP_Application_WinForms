/*!50003 DROP PROCEDURE IF EXISTS `md_part_ids_Add_Part` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Add_Part`(IN `p_ItemNumber` VARCHAR(300), IN `p_Customer` VARCHAR(300), IN `p_Description` VARCHAR(300), IN `p_IssuedBy` VARCHAR(100), IN `p_ItemType` VARCHAR(100), IN `p_RequiresColorCode` TINYINT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_ItemNumber IS NULL OR TRIM(p_ItemNumber) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemNumber is required';
    ELSEIF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
    ELSE
        INSERT INTO `md_part_ids` (`PartID`, `Customer`, `Description`, `IssuedBy`, `ItemType`, `RequiresColorCode`)
        VALUES (p_ItemNumber, p_Customer, p_Description, p_IssuedBy, p_ItemType, COALESCE(p_RequiresColorCode, 0));
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" added successfully');
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to add part';
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

