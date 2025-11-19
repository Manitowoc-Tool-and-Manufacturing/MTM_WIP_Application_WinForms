-- =============================================
-- Script: Rename usr_ui_settings procedures to usr_settings
-- Purpose: Standardize naming convention to match table name
-- Date: 2025-11-18
-- =============================================

USE mtm_wip_application_winforms;

-- Disable foreign key checks for faster execution
SET FOREIGN_KEY_CHECKS = 0;

-- =============================================
-- 1. usr_ui_settings_Delete_ByUserId → usr_settings_Delete_ByUserId
-- =============================================
DROP PROCEDURE IF EXISTS `usr_settings_Delete_ByUserId`;

DELIMITER //
CREATE PROCEDURE `usr_settings_Delete_ByUserId`(
    IN p_UserId VARCHAR(64),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    DELETE FROM usr_settings WHERE UserId = p_UserId;
    
    IF ROW_COUNT() > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Settings deleted for user: ', p_UserId);
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('No settings found for user: ', p_UserId);
    END IF;
END //
DELIMITER ;

-- =============================================
-- 2. usr_ui_settings_Get → usr_settings_Get
-- =============================================
DROP PROCEDURE IF EXISTS `usr_settings_Get`;

DELIMITER //
CREATE PROCEDURE `usr_settings_Get`(
    IN p_UserId VARCHAR(64),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT UserId, SettingsJson, ShortcutsJson, UpdatedAt
    FROM usr_settings
    WHERE UserId = p_UserId;
    
    IF ROW_COUNT() > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Settings retrieved for user: ', p_UserId);
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('No settings found for user: ', p_UserId);
    END IF;
END //
DELIMITER ;

-- =============================================
-- 3. usr_ui_settings_GetJsonSetting → usr_settings_GetJsonSetting
-- =============================================
DROP PROCEDURE IF EXISTS `usr_settings_GetJsonSetting`;

DELIMITER //
CREATE PROCEDURE `usr_settings_GetJsonSetting`(
    IN p_UserId VARCHAR(64),
    IN p_DgvName VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT JSON_EXTRACT(SettingsJson, CONCAT('$."', p_DgvName, '"')) AS SettingJson
    FROM usr_settings
    WHERE UserId = p_UserId;
    
    IF ROW_COUNT() > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Setting retrieved for DGV: ', p_DgvName);
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('No setting found for DGV: ', p_DgvName);
    END IF;
END //
DELIMITER ;

-- =============================================
-- 4. usr_ui_settings_GetShortcutsJson → usr_settings_GetShortcutsJson
-- =============================================
DROP PROCEDURE IF EXISTS `usr_settings_GetShortcutsJson`;

DELIMITER //
CREATE PROCEDURE `usr_settings_GetShortcutsJson`(
    IN p_UserId VARCHAR(64),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT ShortcutsJson
    FROM usr_settings
    WHERE UserId = p_UserId;
    
    IF ROW_COUNT() > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Shortcuts retrieved for user: ', p_UserId);
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('No shortcuts found for user: ', p_UserId);
    END IF;
END //
DELIMITER ;

-- =============================================
-- 5. usr_ui_settings_Get_All → usr_settings_Get_All
-- =============================================
DROP PROCEDURE IF EXISTS `usr_settings_Get_All`;

DELIMITER //
CREATE PROCEDURE `usr_settings_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT UserId, SettingsJson, ShortcutsJson, UpdatedAt
    FROM usr_settings;
    
    SET p_Status = 1;
    SET p_ErrorMsg = CONCAT('Retrieved ', ROW_COUNT(), ' user settings');
END //
DELIMITER ;

-- =============================================
-- 6. usr_ui_settings_SetJsonSetting → usr_settings_SetJsonSetting
-- =============================================
DROP PROCEDURE IF EXISTS `usr_settings_SetJsonSetting`;

DELIMITER //
CREATE PROCEDURE `usr_settings_SetJsonSetting`(
    IN p_UserId VARCHAR(64),
    IN p_DgvName VARCHAR(100),
    IN p_SettingJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    INSERT INTO usr_settings (UserId, SettingsJson, ShortcutsJson)
    VALUES (p_UserId, JSON_OBJECT(p_DgvName, p_SettingJson), JSON_OBJECT())
    ON DUPLICATE KEY UPDATE
        SettingsJson = JSON_SET(SettingsJson, CONCAT('$."', p_DgvName, '"'), p_SettingJson);
    
    SET p_Status = 1;
    SET p_ErrorMsg = CONCAT('Setting saved for DGV: ', p_DgvName);
END //
DELIMITER ;

-- =============================================
-- 7. usr_ui_settings_SetShortcutsJson → usr_settings_SetShortcutsJson
-- =============================================
DROP PROCEDURE IF EXISTS `usr_settings_SetShortcutsJson`;

DELIMITER //
CREATE PROCEDURE `usr_settings_SetShortcutsJson`(
    IN p_UserId VARCHAR(64),
    IN p_ShortcutsJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    INSERT INTO usr_settings (UserId, SettingsJson, ShortcutsJson)
    VALUES (p_UserId, JSON_OBJECT(), p_ShortcutsJson)
    ON DUPLICATE KEY UPDATE
        ShortcutsJson = p_ShortcutsJson;
    
    SET p_Status = 1;
    SET p_ErrorMsg = CONCAT('Shortcuts saved for user: ', p_UserId);
END //
DELIMITER ;

-- =============================================
-- 8. usr_ui_settings_SetThemeJson → usr_settings_SetThemeJson
-- =============================================
DROP PROCEDURE IF EXISTS `usr_settings_SetThemeJson`;

DELIMITER //
CREATE PROCEDURE `usr_settings_SetThemeJson`(
    IN p_UserId VARCHAR(64),
    IN p_ThemeJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    INSERT INTO usr_settings (UserId, SettingsJson, ShortcutsJson)
    VALUES (p_UserId, p_ThemeJson, JSON_OBJECT())
    ON DUPLICATE KEY UPDATE
        SettingsJson = p_ThemeJson;
    
    SET p_Status = 1;
    SET p_ErrorMsg = CONCAT('Theme JSON saved for user: ', p_UserId);
END //
DELIMITER ;

-- =============================================
-- Drop old procedures
-- =============================================
DROP PROCEDURE IF EXISTS `usr_ui_settings_Delete_ByUserId`;
DROP PROCEDURE IF EXISTS `usr_ui_settings_Get`;
DROP PROCEDURE IF EXISTS `usr_ui_settings_GetJsonSetting`;
DROP PROCEDURE IF EXISTS `usr_ui_settings_GetShortcutsJson`;
DROP PROCEDURE IF EXISTS `usr_ui_settings_Get_All`;
DROP PROCEDURE IF EXISTS `usr_ui_settings_SetJsonSetting`;
DROP PROCEDURE IF EXISTS `usr_ui_settings_SetShortcutsJson`;
DROP PROCEDURE IF EXISTS `usr_ui_settings_SetThemeJson`;

-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS = 1;

-- =============================================
-- Verification
-- =============================================
SELECT 'New procedures created:' AS Status;
SELECT ROUTINE_NAME 
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_SCHEMA = 'mtm_wip_application_winforms' 
AND ROUTINE_NAME LIKE 'usr_settings%' 
ORDER BY ROUTINE_NAME;

SELECT 'Old procedures (should be empty):' AS Status;
SELECT ROUTINE_NAME 
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_SCHEMA = 'mtm_wip_application_winforms' 
AND ROUTINE_NAME LIKE 'usr_ui_settings%' 
ORDER BY ROUTINE_NAME;
