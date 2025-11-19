DELIMITER //
DROP PROCEDURE IF EXISTS `usr_settings_GetShortcutsJson`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_settings_GetShortcutsJson`(
    IN p_UserId VARCHAR(255),
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
        -- Return ShortcutsJson as a result set (not OUT parameter)
        SELECT ShortcutsJson AS SettingJson
        FROM usr_settings
        WHERE UserId = p_UserId
        LIMIT 1;
        
        SELECT FOUND_ROWS() INTO v_Count;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved shortcuts for user "', p_UserId, '"');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('User "', p_UserId, '" not found in settings');
        END IF;
    END IF;
END
//
DELIMITER ;
