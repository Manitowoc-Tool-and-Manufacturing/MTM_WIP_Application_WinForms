/*!50003 DROP PROCEDURE IF EXISTS `usr_users_Update_User` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Update_User`(IN `p_User` VARCHAR(100), IN `p_FullName` VARCHAR(200), IN `p_Shift` VARCHAR(50), IN `p_Pin` VARCHAR(50), IN `p_VisualUserName` VARCHAR(50), IN `p_VisualPassword` VARCHAR(50), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE v_SettingsJson JSON;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSE
        -- Update core user info in usr_users
        UPDATE usr_users SET
            `Full Name` = p_FullName,
            `Shift` = p_Shift,
            `Pin` = p_Pin
        WHERE `User` = p_User;
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            -- Update settings in usr_settings.SettingsJson
            UPDATE usr_settings
            SET SettingsJson = JSON_SET(
                COALESCE(SettingsJson, JSON_OBJECT()),
                '$.VisualUserName', p_VisualUserName,
                '$.VisualPassword', p_VisualPassword
            )
            WHERE UserId = p_User;
            
            -- If no settings row exists, create one
            IF ROW_COUNT() = 0 THEN
                INSERT INTO usr_settings (UserId, SettingsJson, ShortcutsJson)
                VALUES (
                    p_User,
                    JSON_OBJECT(
                        'VisualUserName', p_VisualUserName,
                        'VisualPassword', p_VisualPassword
                    ),
                    JSON_OBJECT()
                );
            END IF;
            
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" updated successfully');
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

