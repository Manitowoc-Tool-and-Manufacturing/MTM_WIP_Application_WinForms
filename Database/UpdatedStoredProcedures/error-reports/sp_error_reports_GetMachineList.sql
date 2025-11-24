/*!50003 DROP PROCEDURE IF EXISTS `sp_error_reports_GetMachineList` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_error_reports_GetMachineList`(OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_row_count INT DEFAULT 0;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        IF p_ErrorMsg IS NULL OR p_ErrorMsg = '' THEN
            SET p_ErrorMsg = 'Database error occurred while retrieving machine list';
        END IF;
        SET p_Status = -1;
    END;

    SELECT DISTINCT MachineName
    FROM error_reports
    WHERE MachineName IS NOT NULL AND TRIM(MachineName) <> ''
    ORDER BY MachineName ASC;

    SET v_row_count = ROW_COUNT();

    IF v_row_count = 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = 'No machines found';
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_row_count, ' unique machines');
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

