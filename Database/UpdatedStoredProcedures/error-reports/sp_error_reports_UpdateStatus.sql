/*!50003 DROP PROCEDURE IF EXISTS `sp_error_reports_UpdateStatus` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_error_reports_UpdateStatus`(IN `p_ReportID` INT, IN `p_NewStatus` VARCHAR(20), IN `p_DeveloperNotes` TEXT, IN `p_ReviewedBy` VARCHAR(100), IN `p_ReviewedDate` DATETIME, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
proc:BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        IF p_ErrorMsg IS NULL OR p_ErrorMsg = '' THEN
            SET p_ErrorMsg = 'Database error occurred while updating error report status';
        END IF;
        SET p_Status = -1;
    END;

    -- Validate new status value
    IF p_NewStatus NOT IN ('New', 'Reviewed', 'Resolved') THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Invalid status value. Must be: New, Reviewed, or Resolved';
        LEAVE proc;
    END IF;

    -- Ensure report exists before attempting update
    IF NOT EXISTS (SELECT 1 FROM error_reports WHERE ReportID = p_ReportID) THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ReportID not found';
        LEAVE proc;
    END IF;

    START TRANSACTION;

    UPDATE error_reports
    SET
        Status = p_NewStatus,
        ReviewedBy = p_ReviewedBy,
        ReviewedDate = p_ReviewedDate,
        DeveloperNotes = CASE
            WHEN p_DeveloperNotes IS NULL THEN DeveloperNotes
            ELSE p_DeveloperNotes
        END
    WHERE ReportID = p_ReportID;

    COMMIT;

    SET p_Status = 0;
    SET p_ErrorMsg = 'Error report status updated successfully';
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

