DELIMITER //
DROP PROCEDURE IF EXISTS `usr_ui_settings_Get`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_Get`(
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
        FROM usr_ui_settings
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
