/*!50003 DROP PROCEDURE IF EXISTS `sp_error_reports_Delete` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_error_reports_Delete`(IN `p_ReportID` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(255))
BEGIN
    
    IF NOT EXISTS (SELECT 1 FROM error_reports WHERE ReportID = p_ReportID) THEN
        SET p_Status = 0;
        SET p_ErrorMsg = 'Error report not found.';
    ELSE
        DELETE FROM error_reports WHERE ReportID = p_ReportID;
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error report deleted successfully.';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

