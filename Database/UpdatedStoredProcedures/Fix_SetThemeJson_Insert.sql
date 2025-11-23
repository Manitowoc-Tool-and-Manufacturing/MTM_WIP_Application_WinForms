DELIMITER //

DROP PROCEDURE IF EXISTS `usr_settings_SetThemeJson`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_settings_SetThemeJson`(
    IN p_UserId VARCHAR(64),
    IN p_ThemeJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
    ELSEIF p_ThemeJson IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ThemeJson is required';
    ELSE
        -- Insert or update using ON DUPLICATE KEY UPDATE
        -- If row exists, merge the new JSON with existing JSON
        -- If row doesn't exist, insert the new JSON
        INSERT INTO usr_settings (UserId, SettingsJson)
        VALUES (p_UserId, p_ThemeJson)
        ON DUPLICATE KEY UPDATE
            SettingsJson = JSON_MERGE_PATCH(IFNULL(SettingsJson, JSON_OBJECT()), p_ThemeJson);
            
        SET v_RowCount = ROW_COUNT();
        
        -- ROW_COUNT() returns:
        -- 1 if a new row was inserted
        -- 2 if an existing row was updated
        -- 0 if an existing row was updated but values were identical
        
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Theme JSON saved for user "', p_UserId, '"');
    END IF;
END
//

DELIMITER ;
