/*!50003 DROP PROCEDURE IF EXISTS `usr_users_Add_User` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Add_User`(IN `p_User` VARCHAR(100), IN `p_FullName` VARCHAR(200), IN `p_Shift` VARCHAR(50), IN `p_VitsUser` TINYINT, IN `p_Pin` VARCHAR(50), IN `p_LastShownVersion` VARCHAR(50), IN `p_HideChangeLog` VARCHAR(50), IN `p_ThemeName` VARCHAR(50), IN `p_ThemeFontSize` INT, IN `p_VisualUserName` VARCHAR(50), IN `p_VisualPassword` VARCHAR(50), IN `p_WipServerAddress` VARCHAR(15), IN `p_WipServerPort` VARCHAR(10), IN `p_WipDatabase` VARCHAR(100), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN

    DECLARE v_RowCount INT DEFAULT 0;

    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1

            p_ErrorMsg = MESSAGE_TEXT;

        SET p_Status = -1;

    END;

    IF p_User IS NULL OR TRIM(p_User) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'User is required';

    ELSEIF p_FullName IS NULL OR TRIM(p_FullName) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Full Name is required';

    ELSE

        -- Validate username contains only safe characters (alphanumeric, underscore, hyphen)

        IF p_User REGEXP '[^A-Za-z0-9_-]' THEN

            SET p_Status = -2;

            SET p_ErrorMsg = 'Username contains invalid characters. Only alphanumeric, underscore, and hyphen allowed.';

        ELSE

            -- 1. Insert into usr_users (Basic Info)
            INSERT INTO usr_users (
                `User`, `Full Name`, `Shift`, `VitsUser`, `Pin`, `LastShownVersion`, `HideChangeLog`
            ) VALUES (
                p_User, p_FullName, p_Shift, p_VitsUser, p_Pin, p_LastShownVersion, p_HideChangeLog
            );

            SET v_RowCount = ROW_COUNT();

            IF v_RowCount > 0 THEN
                -- 2. Insert into usr_settings (JSON Settings)
                -- Construct JSON object with provided parameters + defaults for missing ones
                INSERT INTO usr_settings (UserId, SettingsJson)
                VALUES (
                    p_User,
                    JSON_OBJECT(
                        'Theme_Name', p_ThemeName,
                        'Theme_FontSize', p_ThemeFontSize,
                        'VisualUserName', p_VisualUserName,
                        'VisualPassword', p_VisualPassword,
                        'WipServerAddress', p_WipServerAddress,
                        'WIPDatabase', p_WipDatabase,
                        'WipServerPort', p_WipServerPort,
                        'AutoExpandPanels', 'False',
                        'AnimationsEnabled', 'false',
                        'ShowTotalSummaryPanel', 'true'
                    )
                )
                ON DUPLICATE KEY UPDATE SettingsJson = VALUES(SettingsJson);

                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('User "', p_User, '" added successfully');

            ELSE

                SET p_Status = -3;

                SET p_ErrorMsg = 'Failed to add user';

            END IF;

        END IF;

    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

