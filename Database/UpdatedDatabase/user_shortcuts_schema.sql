-- Create sys_shortcuts table if not exists
CREATE TABLE IF NOT EXISTS sys_shortcuts (
    ShortcutName VARCHAR(100) NOT NULL,
    ShortcutKeys INT NOT NULL,
    Description VARCHAR(255),
    Category VARCHAR(50),
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (ShortcutName)
);

-- Create usr_user_shortcuts table
CREATE TABLE IF NOT EXISTS usr_user_shortcuts (
    UserName VARCHAR(50) NOT NULL,
    ShortcutName VARCHAR(100) NOT NULL,
    CustomKeys INT NOT NULL,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (UserName, ShortcutName),
    FOREIGN KEY (ShortcutName) REFERENCES sys_shortcuts(ShortcutName) ON DELETE CASCADE
);

-- Populate sys_shortcuts with defaults (INSERT IGNORE to avoid duplicates)
INSERT IGNORE INTO sys_shortcuts (ShortcutName, ShortcutKeys, Description, Category) VALUES
('Shortcut_AdvInv_Import_ImportExcel', 131145, 'Import Excel Data', 'AdvancedInventory'),
('Shortcut_AdvInv_Import_Normal', 131138, 'Return to Normal Inventory (Import)', 'AdvancedInventory'),
('Shortcut_AdvInv_Import_OpenExcel', 131141, 'Open Excel File', 'AdvancedInventory'),
('Shortcut_AdvInv_Import_Save', 131155, 'Save Imported Data', 'AdvancedInventory'),
('Shortcut_AdvInv_Multi_Normal', 131138, 'Return to Normal Inventory (Multi)', 'AdvancedInventory'),
('Shortcut_AdvInv_Multi_AddLoc', 131085, 'Add Location (Multi)', 'AdvancedInventory'),
('Shortcut_AdvInv_Multi_Reset', 131154, 'Reset Multi Entry', 'AdvancedInventory'),
('Shortcut_AdvInv_Multi_SaveAll', 131155, 'Save All (Multi)', 'AdvancedInventory'),
('Shortcut_AdvInv_Normal', 131138, 'Return to Normal Inventory', 'AdvancedInventory'),
('Shortcut_AdvInv_Reset', 131154, 'Reset Single Entry', 'AdvancedInventory'),
('Shortcut_AdvInv_Save', 131155, 'Save Single Entry', 'AdvancedInventory'),
('Shortcut_AdvInv_Send', 131085, 'Send Single Entry', 'AdvancedInventory'),
('Shortcut_Remove_Delete', 46, 'Delete Selected Items', 'Remove'),
('Shortcut_Remove_Normal', 131138, 'Return to Normal Remove', 'Remove'),
('Shortcut_Remove_Reset', 131154, 'Reset Advanced Remove', 'Remove'),
('Shortcut_Remove_Search', 131155, 'Search Advanced Remove', 'Remove'),
('Shortcut_Remove_Undo', 131162, 'Undo Last Deletion', 'Remove'),
('Shortcut_Inventory_Advanced', 196673, 'Open Advanced Entry', 'Inventory'),
('Shortcut_Inventory_Reset', 131154, 'Reset Inventory Tab', 'Inventory'),
('Shortcut_Inventory_Save', 131155, 'Save Inventory Transaction', 'Inventory'),
('Shortcut_Inventory_ToggleRightPanel_Right', 262183, 'Toggle Right Panel (Right)', 'Inventory'),
('Shortcut_Inventory_ToggleRightPanel_Left', 262181, 'Toggle Right Panel (Left)', 'Inventory'),
('Shortcut_Transfer_Reset', 131154, 'Reset Transfer Tab', 'Transfer'),
('Shortcut_Transfer_Search', 131155, 'Search Inventory', 'Transfer'),
('Shortcut_Transfer_ToggleRightPanel_Right', 262183, 'Toggle Right Panel (Right)', 'Transfer'),
('Shortcut_Transfer_ToggleRightPanel_Left', 262181, 'Toggle Right Panel (Left)', 'Transfer'),
('Shortcut_Transfer_Transfer', 131156, 'Transfer Selected Items', 'Transfer'),
('Shortcut_MainForm_Tab1', 131121, 'Switch to Inventory Tab', 'MainForm'),
('Shortcut_MainForm_Tab2', 131122, 'Switch to Remove Tab', 'MainForm'),
('Shortcut_MainForm_Tab3', 131123, 'Switch to Transfer Tab', 'MainForm');

-- Stored Procedures

DELIMITER //

DROP PROCEDURE IF EXISTS sys_shortcuts_GetAll //
CREATE PROCEDURE sys_shortcuts_GetAll()
BEGIN
    SELECT * FROM sys_shortcuts;
END //

DROP PROCEDURE IF EXISTS usr_user_shortcuts_GetByUser //
CREATE PROCEDURE usr_user_shortcuts_GetByUser(IN p_UserName VARCHAR(50))
BEGIN
    SELECT 
        s.ShortcutName,
        COALESCE(u.CustomKeys, s.ShortcutKeys) as EffectiveKeys,
        s.Description,
        s.Category,
        CASE WHEN u.CustomKeys IS NOT NULL THEN 1 ELSE 0 END as IsCustom
    FROM sys_shortcuts s
    LEFT JOIN usr_user_shortcuts u ON s.ShortcutName = u.ShortcutName AND u.UserName = p_UserName;
END //

DROP PROCEDURE IF EXISTS usr_user_shortcuts_Upsert //
CREATE PROCEDURE usr_user_shortcuts_Upsert(
    IN p_UserName VARCHAR(50),
    IN p_ShortcutName VARCHAR(100),
    IN p_CustomKeys INT
)
BEGIN
    INSERT INTO usr_user_shortcuts (UserName, ShortcutName, CustomKeys)
    VALUES (p_UserName, p_ShortcutName, p_CustomKeys)
    ON DUPLICATE KEY UPDATE CustomKeys = p_CustomKeys;
END //

DROP PROCEDURE IF EXISTS usr_user_shortcuts_Reset //
CREATE PROCEDURE usr_user_shortcuts_Reset(
    IN p_UserName VARCHAR(50)
)
BEGIN
    DELETE FROM usr_user_shortcuts WHERE UserName = p_UserName;
END //

DELIMITER ;
