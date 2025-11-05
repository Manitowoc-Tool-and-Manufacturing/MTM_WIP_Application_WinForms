-- =============================================
-- Drop procedure if it exists
-- =============================================
DROP PROCEDURE IF EXISTS `usr_ui_settings_Get_All`;

-- =============================================
-- Stored Procedure: usr_ui_settings_Get_All
-- Description: Retrieves all user UI settings for validation/reporting
-- Parameters:
--   OUT p_Status INT - Status code (1=Success, 0=No data, -1=Error)
--   OUT p_ErrorMsg VARCHAR(500) - Status/error message
-- Returns: ResultSet with all user settings (UserId, SettingsJson, UpdatedAt)
-- =============================================
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    -- Get all user settings
    SELECT 
        UserId,
        SettingsJson,
        UpdatedAt
    FROM usr_ui_settings
    ORDER BY UserId;
    
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' user settings records');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No user settings found';
    END IF;
END//
DELIMITER ;
