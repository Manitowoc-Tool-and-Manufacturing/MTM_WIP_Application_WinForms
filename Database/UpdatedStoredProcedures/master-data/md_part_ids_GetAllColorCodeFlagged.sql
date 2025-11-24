/*!50003 DROP PROCEDURE IF EXISTS `md_part_ids_GetAllColorCodeFlagged` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_GetAllColorCodeFlagged`(OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error retrieving color-coded part IDs';
        ROLLBACK;
    END;

    START TRANSACTION;

    
    SELECT PartID
    FROM md_part_ids
    WHERE RequiresColorCode = TRUE
    ORDER BY PartID ASC;

    
    IF FOUND_ROWS() > 0 THEN
        SET p_Status = 1;  
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;  
        SET p_ErrorMsg = NULL;
    END IF;

    COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

