/*!50003 DROP PROCEDURE IF EXISTS `md_part_ids_UpdateColorCodeFlag` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_UpdateColorCodeFlag`(IN `p_PartID` VARCHAR(300), IN `p_RequiresColorCode` BOOLEAN, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE part_exists INT DEFAULT 0;
    
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error updating color code flag';
        ROLLBACK;
    END;

    
    IF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
    ELSE
        START TRANSACTION;

        
        SELECT COUNT(*) INTO part_exists
        FROM md_part_ids
        WHERE PartID = p_PartID;

        IF part_exists = 0 THEN
            SET p_Status = -3;
            SET p_ErrorMsg = 'Part ID not found';
            ROLLBACK;
        ELSE
            
            UPDATE md_part_ids
            SET RequiresColorCode = p_RequiresColorCode
            WHERE PartID = p_PartID;

            SET p_Status = 1;
            SET p_ErrorMsg = NULL;
            COMMIT;
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

