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
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving themes';
    END;

    -- Return all themes
    SELECT ThemeName, SettingsJson FROM app_themes ORDER BY ThemeName;
    
    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;
