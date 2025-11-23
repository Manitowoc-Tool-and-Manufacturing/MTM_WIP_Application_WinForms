DROP PROCEDURE IF EXISTS usr_settings_SaveFullJson;

DELIMITER $$

CREATE PROCEDURE usr_settings_SaveFullJson(
    IN p_UserId VARCHAR(64),
    IN p_Json LONGTEXT,
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

    -- Ensure p_Json is valid JSON before proceeding
    IF NOT JSON_VALID(p_Json) THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid JSON format';
    ELSE
        INSERT INTO usr_settings (UserId, SettingsJson)
        VALUES (p_UserId, CAST(p_Json AS JSON))
        ON DUPLICATE KEY UPDATE
            SettingsJson = CAST(p_Json AS JSON);

        SET p_Status = 1;
        SET p_ErrorMsg = 'Settings saved successfully';
    END IF;
END$$

DELIMITER ;