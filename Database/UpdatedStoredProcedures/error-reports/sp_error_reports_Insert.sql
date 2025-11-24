/*!50003 DROP PROCEDURE IF EXISTS `sp_error_reports_Insert` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_error_reports_Insert`(IN `p_UserName` VARCHAR(100), IN `p_MachineName` VARCHAR(200), IN `p_AppVersion` VARCHAR(50), IN `p_ErrorType` VARCHAR(255), IN `p_ErrorSummary` TEXT, IN `p_UserNotes` TEXT, IN `p_TechnicalDetails` TEXT, IN `p_CallStack` TEXT, OUT `p_ReportID` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    -- Declare variables
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while inserting error report';
        SET p_ReportID = NULL;
    END;
    
    -- Validate required parameters
    IF p_UserName IS NULL OR TRIM(p_UserName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserName is required';
        SET p_ReportID = NULL;
    ELSE
        START TRANSACTION;
        
        -- Insert error report
        INSERT INTO error_reports (
            ReportDate,
            UserName,
            MachineName,
            AppVersion,
            ErrorType,
            ErrorSummary,
            UserNotes,
            TechnicalDetails,
            CallStack,
            Status
        ) VALUES (
            NOW(),
            p_UserName,
            p_MachineName,
            p_AppVersion,
            p_ErrorType,
            p_ErrorSummary,
            p_UserNotes,
            p_TechnicalDetails,
            p_CallStack,
            'New'
        );
        
        -- Get the generated ReportID
        SET p_ReportID = LAST_INSERT_ID();
        
        -- Success
        SET p_Status = 0;
        SET p_ErrorMsg = 'Error report inserted successfully';
        
        COMMIT;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

