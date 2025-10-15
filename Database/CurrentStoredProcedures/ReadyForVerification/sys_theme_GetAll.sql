-- =============================================
-- Stored Procedure: sys_theme_GetAll
-- Description: Retrieves all theme configurations
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_theme_GetAll`$$

CREATE PROCEDURE `sys_theme_GetAll`(
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
        ROLLBACK;
    END;
    
    START TRANSACTION;

    -- Return all themes
    SELECT ThemeName, SettingsJson 
    FROM app_themes 
    ORDER BY ThemeName;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' theme(s)');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No themes found';
    END IF;
    
    COMMIT;
END$$

DELIMITER ;
