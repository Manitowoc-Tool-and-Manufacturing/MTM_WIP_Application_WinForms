-- =============================================
-- Procedure: usr_ui_settings_GetJsonSetting
-- Domain: users
-- Created: 2025-10-17
-- Purpose: Returns JSON settings for a user
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `usr_ui_settings_GetJsonSetting`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_GetJsonSetting`(
    IN p_UserId INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_JsonSetting TEXT DEFAULT NULL;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Validate input
    IF p_UserId IS NULL OR p_UserId <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid UserId is required';
    ELSE
        -- Get JSON setting for user
        SELECT SettingJson INTO v_JsonSetting
        FROM usr_ui_settings
        WHERE UserId = p_UserId
        LIMIT 1;
        
        IF v_JsonSetting IS NOT NULL THEN
            -- Return JSON setting as scalar
            SELECT v_JsonSetting AS JsonSetting;
            
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved JSON settings for user ', p_UserId);
        ELSE
            -- Return empty JSON object
            SELECT '{}' AS JsonSetting;
            
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No JSON settings found for user ', p_UserId);
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of usr_ui_settings_GetJsonSetting
-- =============================================
