DELIMITER //
DROP PROCEDURE IF EXISTS `usr_ui_settings_SetShortcutsJson`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_SetShortcutsJson`(
    IN p_UserId VARCHAR(255),
    IN p_ShortcutsJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
    ELSEIF p_ShortcutsJson IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ShortcutsJson is required';
    ELSE
        UPDATE usr_ui_settings
        SET ShortcutsJson = p_ShortcutsJson
        WHERE UserId = p_UserId;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Shortcuts JSON saved for user "', p_UserId, '"');
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User "', p_UserId, '" not found in settings');
        END IF;
    END IF;
END
//
DELIMITER ;
