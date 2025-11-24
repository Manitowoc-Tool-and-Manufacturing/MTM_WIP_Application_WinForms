/*!50003 DROP PROCEDURE IF EXISTS `md_part_ids_Get_ByItemNumber` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Get_ByItemNumber`(IN `p_ItemNumber` VARCHAR(300), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_ItemNumber IS NULL OR TRIM(p_ItemNumber) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemNumber is required';
    ELSE
        SELECT * FROM `md_part_ids`
        WHERE `PartID` = p_ItemNumber;
        SELECT FOUND_ROWS() INTO v_Count;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved part "', p_ItemNumber, '"');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" not found');
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

