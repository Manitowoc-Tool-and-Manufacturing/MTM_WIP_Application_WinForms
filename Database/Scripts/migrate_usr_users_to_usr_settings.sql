-- =============================================
-- Script: Migrate usr_users settings columns to usr_settings
-- Purpose: Move all user settings from usr_users to usr_settings.SettingsJson
-- Date: 2025-11-18
-- =============================================

USE mtm_wip_application_winforms;

-- =============================================
-- Step 1: Migrate data to usr_settings.SettingsJson
-- =============================================

-- Step 1: Update existing users' settings
UPDATE usr_settings s
INNER JOIN usr_users u ON s.UserId = u.User
SET s.SettingsJson = JSON_MERGE_PATCH(
    s.SettingsJson,
    JSON_OBJECT(
        'Theme_Name', IFNULL(u.Theme_Name, ''),
        'Theme_FontSize', IFNULL(u.Theme_FontSize, 9),
        'VisualUserName', IFNULL(u.VisualUserName, ''),
        'VisualPassword', IFNULL(u.VisualPassword, ''),
        'WipServerAddress', IFNULL(u.WipServerAddress, 'localhost'),
        'WIPDatabase', IFNULL(u.WIPDatabase, 'mtm_wip_application_winforms'),
        'WipServerPort', IFNULL(u.WipServerPort, '3306')
    )
);

-- Step 2: Insert new users who don't have settings yet
INSERT INTO usr_settings (UserId, SettingsJson, ShortcutsJson)
SELECT
    u.User,
    JSON_OBJECT(
        'Theme_Name', IFNULL(u.Theme_Name, ''),
        'Theme_FontSize', IFNULL(u.Theme_FontSize, 9),
        'VisualUserName', IFNULL(u.VisualUserName, ''),
        'VisualPassword', IFNULL(u.VisualPassword, ''),
        'WipServerAddress', IFNULL(u.WipServerAddress, 'localhost'),
        'WIPDatabase', IFNULL(u.WIPDatabase, 'mtm_wip_application_winforms'),
        'WipServerPort', IFNULL(u.WipServerPort, '3306')
    ),
    JSON_OBJECT()
FROM usr_users u
WHERE NOT EXISTS (SELECT 1 FROM usr_settings WHERE UserId = u.User);-- =============================================
-- Step 2: Verify migration (optional - comment out if not needed)
-- =============================================

SELECT 'Migration verification - Sample of migrated data:' AS Status;
SELECT 
    UserId,
    JSON_EXTRACT(SettingsJson, '$.Theme_Name') AS Theme_Name,
    JSON_EXTRACT(SettingsJson, '$.Theme_FontSize') AS Theme_FontSize,
    JSON_EXTRACT(SettingsJson, '$.VisualUserName') AS VisualUserName,
    JSON_EXTRACT(SettingsJson, '$.WipServerAddress') AS WipServerAddress,
    JSON_EXTRACT(SettingsJson, '$.WIPDatabase') AS WIPDatabase,
    JSON_EXTRACT(SettingsJson, '$.WipServerPort') AS WipServerPort
FROM usr_settings
LIMIT 5;

-- =============================================
-- Step 3: Drop migrated columns from usr_users
-- =============================================

ALTER TABLE usr_users
    DROP COLUMN Theme_Name,
    DROP COLUMN Theme_FontSize,
    DROP COLUMN VisualUserName,
    DROP COLUMN VisualPassword,
    DROP COLUMN WipServerAddress,
    DROP COLUMN WIPDatabase,
    DROP COLUMN WipServerPort;

-- =============================================
-- Step 4: Verify final usr_users structure
-- =============================================

SELECT 'Final usr_users columns (should only be core user info):' AS Status;
SHOW COLUMNS FROM usr_users;

-- =============================================
-- End of migration
-- =============================================
