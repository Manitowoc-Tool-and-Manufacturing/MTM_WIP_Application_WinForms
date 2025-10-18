-- =============================================
-- Procedure: usr_ui_settings_SetJsonSetting
-- Domain: users
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `usr_ui_settings_SetJsonSetting`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_SetJsonSetting`(

    IN p_UserId VARCHAR(64),

    IN p_DgvName VARCHAR(128),

    IN p_SettingJson JSON,

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_Existing INT DEFAULT 0;

    DECLARE v_CurrentJson JSON;

    DECLARE v_RowCount INT DEFAULT 0;

    

    -- Exit handler for any SQL exception

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1

            p_ErrorMsg = MESSAGE_TEXT;

        SET p_Status = -1;

        ROLLBACK;

    END;

    

    START TRANSACTION;

    

    -- Validate required parameters

    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'UserId is required';

        ROLLBACK;

    ELSEIF p_DgvName IS NULL OR TRIM(p_DgvName) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'DgvName is required';

        ROLLBACK;

    ELSEIF p_SettingJson IS NULL THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'SettingJson is required';

        ROLLBACK;

    ELSE

        -- Check if user settings exist

        SELECT COUNT(*) INTO v_Existing

        FROM usr_ui_settings

        WHERE UserId = p_UserId;

        

        IF v_Existing = 0 THEN

            -- Insert new settings

            INSERT INTO usr_ui_settings (UserId, SettingsJson)

            VALUES (p_UserId, JSON_OBJECT(p_DgvName, p_SettingJson));

            

            SET v_RowCount = ROW_COUNT();

        ELSE

            -- Update existing settings

            SELECT SettingsJson INTO v_CurrentJson 

            FROM usr_ui_settings 

            WHERE UserId = p_UserId 

            LIMIT 1;

            

            SET v_CurrentJson = JSON_SET(IFNULL(v_CurrentJson, JSON_OBJECT()), 

                                        CONCAT('$.', p_DgvName), p_SettingJson);

            

            UPDATE usr_ui_settings 

            SET SettingsJson = v_CurrentJson, UpdatedAt = NOW() 

            WHERE UserId = p_UserId;

            

            SET v_RowCount = ROW_COUNT();

        END IF;

        

        IF v_RowCount > 0 THEN

            SET p_Status = 1;  -- Success

            SET p_ErrorMsg = CONCAT('JSON setting "', p_DgvName, '" saved for user "', p_UserId, '"');

            COMMIT;

        ELSE

            SET p_Status = -3;  -- Business logic error

            SET p_ErrorMsg = 'Failed to save JSON setting';

            ROLLBACK;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of usr_ui_settings_SetJsonSetting
-- =============================================
