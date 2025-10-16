DELIMITER //
DROP PROCEDURE IF EXISTS `usr_ui_settings_SetThemeJson`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_SetThemeJson`(
    IN p_UserId VARCHAR(64),
    IN p_ThemeJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_SettingsJson JSON DEFAULT NULL;
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
        ROLLBACK;
    ELSEIF p_ThemeJson IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ThemeJson is required';
        ROLLBACK;
    ELSE
        SELECT COUNT(*) INTO v_Exists
        FROM usr_ui_settings
        WHERE UserId = p_UserId;
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User "', p_UserId, '" does not exist in usr_ui_settings');
            ROLLBACK;
        ELSE
            SELECT SettingsJson INTO v_SettingsJson
            FROM usr_ui_settings
            WHERE UserId = p_UserId
            FOR UPDATE;
            IF v_SettingsJson IS NULL THEN
                SET v_SettingsJson = p_ThemeJson;
            ELSE
                SET v_SettingsJson = JSON_MERGE_PATCH(v_SettingsJson, p_ThemeJson);
            END IF;
            UPDATE usr_ui_settings
            SET SettingsJson = v_SettingsJson
            WHERE UserId = p_UserId;
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Theme JSON saved for user "', p_UserId, '"');
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to save theme JSON';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
