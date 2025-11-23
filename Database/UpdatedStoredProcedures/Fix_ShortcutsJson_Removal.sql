DELIMITER //

DROP PROCEDURE IF EXISTS `usr_settings_SetUserSetting_ByUserAndField`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_settings_SetUserSetting_ByUserAndField`(
    IN p_User VARCHAR(100),
    IN p_Field VARCHAR(100),
    IN p_Value VARCHAR(500),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE v_AllowedFields VARCHAR(1000) DEFAULT 'Theme_Name,Theme_FontSize,VisualUserName,VisualPassword,WipServerAddress,WIPDatabase,WipServerPort,AnimationsEnabled';
    
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
END
//

DROP PROCEDURE IF EXISTS `usr_settings_Get`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_settings_Get`(
    IN p_UserId VARCHAR(64),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
    ELSE
        SELECT SettingsJson
        FROM usr_settings
        WHERE UserId = p_UserId;
        SELECT FOUND_ROWS() INTO v_Count;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved settings for user "', p_UserId, '"');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No settings found for user "', p_UserId, '"');
        END IF;
    END IF;
END
//

DELIMITER ;
