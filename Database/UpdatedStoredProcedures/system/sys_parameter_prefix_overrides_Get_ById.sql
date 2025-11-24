/*!50003 DROP PROCEDURE IF EXISTS `sys_parameter_prefix_overrides_Get_ById` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_parameter_prefix_overrides_Get_ById`(IN `p_OverrideId` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Validation
    IF p_OverrideId IS NULL OR p_OverrideId <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid OverrideId - must be positive integer';
    ELSE
        -- Check if record exists
        IF EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides WHERE OverrideId = p_OverrideId) THEN
            SELECT * FROM sys_parameter_prefix_overrides WHERE OverrideId = p_OverrideId;
            SET p_Status = 0;
            SET p_ErrorMsg = 'Override retrieved successfully';
        ELSE
            SET p_Status = 1;
            SET p_ErrorMsg = 'Override not found';
            SELECT * FROM sys_parameter_prefix_overrides WHERE 1 = 0;
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

