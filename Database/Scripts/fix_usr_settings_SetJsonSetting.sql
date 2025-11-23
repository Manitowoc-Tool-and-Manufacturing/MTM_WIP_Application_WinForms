DROP PROCEDURE IF EXISTS usr_settings_SetJsonSetting;

DELIMITER $$

CREATE PROCEDURE usr_settings_SetJsonSetting(
    IN p_UserId VARCHAR(64),
    IN p_DgvName VARCHAR(100),
    IN p_SettingJson LONGTEXT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    -- Ensure p_SettingJson is valid JSON before proceeding
    IF NOT JSON_VALID(p_SettingJson) THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid JSON format in p_SettingJson';
    ELSE
        INSERT INTO usr_settings (UserId, SettingsJson)
        VALUES (p_UserId, JSON_OBJECT(p_DgvName, CAST(p_SettingJson AS JSON)))
        ON DUPLICATE KEY UPDATE
            SettingsJson = JSON_SET(IFNULL(SettingsJson, '{}'), CONCAT('$."', p_DgvName, '"'), CAST(p_SettingJson AS JSON));

        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Setting saved for DGV: ', p_DgvName);
    END IF;
END$$

DELIMITER ;