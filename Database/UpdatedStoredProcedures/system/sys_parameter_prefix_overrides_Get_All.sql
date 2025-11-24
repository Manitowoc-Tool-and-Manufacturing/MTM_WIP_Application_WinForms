/*!50003 DROP PROCEDURE IF EXISTS `sys_parameter_prefix_overrides_Get_All` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_parameter_prefix_overrides_Get_All`(OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Check if table has active records
    IF EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides WHERE IsActive = 1) THEN
        -- Return all active overrides
        SELECT 
            OverrideId,
            ProcedureName,
            ParameterName,
            OverridePrefix,
            Reason,
            CreatedBy,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            IsActive
        FROM sys_parameter_prefix_overrides
        WHERE IsActive = 1
        ORDER BY ProcedureName, ParameterName;
        
        SET p_Status = 0;
        SET p_ErrorMsg = 'Active overrides retrieved successfully';
    ELSE
        -- No active records found (not an error, just empty result)
        SELECT 
            OverrideId,
            ProcedureName,
            ParameterName,
            OverridePrefix,
            Reason,
            CreatedBy,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            IsActive
        FROM sys_parameter_prefix_overrides
        WHERE 1 = 0; -- Empty result set with correct structure
        
        SET p_Status = 1;
        SET p_ErrorMsg = 'No active overrides found';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

