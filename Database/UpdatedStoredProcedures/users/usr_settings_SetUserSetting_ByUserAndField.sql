/*!50003 DROP PROCEDURE IF EXISTS `usr_settings_SetUserSetting_ByUserAndField` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_settings_SetUserSetting_ByUserAndField`(IN `p_User` VARCHAR(100), IN `p_Field` VARCHAR(100), IN `p_Value` VARCHAR(500), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE v_AllowedFields VARCHAR(1000) DEFAULT 'Theme_Name,Theme_FontSize,VisualUserName,VisualPassword,WipServerAddress,WIPDatabase,WipServerPort,AnimationsEnabled,AutoExpandPanels,ShowTotalSummaryPanel';
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSEIF p_Field IS NULL OR TRIM(p_Field) = '' THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Field is required';
    ELSEIF FIND_IN_SET(p_Field, v_AllowedFields) = 0 THEN
        SET p_Status = -4;
        SET p_ErrorMsg = CONCAT('Field "', p_Field, '" is not in allowed fields list: ', v_AllowedFields);
    ELSE
        -- Insert or update the setting in SettingsJson
        INSERT INTO usr_settings (UserId, SettingsJson)
        VALUES (
            p_User,
            JSON_OBJECT(p_Field, p_Value)
        )
        ON DUPLICATE KEY UPDATE
            SettingsJson = JSON_SET(SettingsJson, CONCAT('$.', p_Field), p_Value);
        
        SET v_RowsAffected = ROW_COUNT();
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Updated field "', p_Field, '" for user "', p_User, '"');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No changes made for user "', p_User, '"');
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

