/*!50003 DROP PROCEDURE IF EXISTS `sys_parameter_prefix_overrides_Add` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_parameter_prefix_overrides_Add`(IN `p_ProcedureName` VARCHAR(128), IN `p_ParameterName` VARCHAR(128), IN `p_OverridePrefix` VARCHAR(10), IN `p_Reason` VARCHAR(500), IN `p_CreatedBy` VARCHAR(50), OUT `p_OverrideId` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_OverrideId = NULL;
    END;
    -- Validation
    IF p_ProcedureName IS NULL OR TRIM(p_ProcedureName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ProcedureName is required';
        SET p_OverrideId = NULL;
    ELSEIF p_ParameterName IS NULL OR TRIM(p_ParameterName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ParameterName is required';
        SET p_OverrideId = NULL;
    ELSEIF p_CreatedBy IS NULL OR TRIM(p_CreatedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'CreatedBy is required';
        SET p_OverrideId = NULL;
    ELSEIF p_OverridePrefix IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'OverridePrefix cannot be NULL (use empty string for no prefix)';
        SET p_OverrideId = NULL;
    ELSE
        -- Check for duplicate
        IF EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides 
                   WHERE ProcedureName = p_ProcedureName 
                   AND ParameterName = p_ParameterName) THEN
            SET p_Status = -3;
            SET p_ErrorMsg = 'Override already exists for this procedure-parameter combination';
            SET p_OverrideId = NULL;
        ELSE
            -- Insert new override
            INSERT INTO sys_parameter_prefix_overrides 
                (ProcedureName, ParameterName, OverridePrefix, Reason, CreatedBy, CreatedDate, IsActive)
            VALUES 
                (p_ProcedureName, p_ParameterName, p_OverridePrefix, p_Reason, p_CreatedBy, NOW(), 1);
            
            SET p_OverrideId = LAST_INSERT_ID();
            SET p_Status = 0;
            SET p_ErrorMsg = 'Override created successfully';
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

