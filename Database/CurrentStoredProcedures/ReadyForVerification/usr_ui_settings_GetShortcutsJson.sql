DROP PROCEDURE IF EXISTS `usr_ui_settings_GetShortcutsJson`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_GetShortcutsJson`(
    IN p_UserId VARCHAR(255),
    OUT p_ShortcutsJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ShortcutsJson = NULL;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate parameter
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
        SET p_ShortcutsJson = NULL;
        ROLLBACK;
    ELSE
        -- Get shortcuts JSON for user
        SELECT ShortcutsJson INTO p_ShortcutsJson
        FROM usr_ui_settings
        WHERE UserId = p_UserId
        LIMIT 1;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 AND p_ShortcutsJson IS NOT NULL THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved shortcuts for user "', p_UserId, '"');
        ELSEIF v_Count > 0 AND p_ShortcutsJson IS NULL THEN
            SET p_Status = 0;  -- User exists but no shortcuts JSON
            SET p_ErrorMsg = CONCAT('No shortcuts JSON for user "', p_UserId, '"');
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('User "', p_UserId, '" not found in settings');
        END IF;
        
        COMMIT;
    END IF;
END$$
DELIMITER ;