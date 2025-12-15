-- Seed UserControlMapping with all controls from FR-001 table mapped to their parent forms

-- First, get the WindowFormMappingID values (we need to reference the MainForm and SettingsForm IDs)
-- These will be inserted conditionally based on the parent form

-- MainForm controls
INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_InventoryTab', 'Inventory Tab', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'MainForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_AdvancedInventory', 'Advanced Inventory', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'MainForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_RemoveTab', 'Remove Tab', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'MainForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_AdvancedRemove', 'Advanced Removal', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'MainForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_TransferTab', 'Transfer Tab', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'MainForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

-- SettingsForm controls
INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_SettingsHome', 'Settings Home', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_About', 'About', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_Database', 'Database Configuration', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_PartIDManagement', 'Part Number Management', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_OperationManagement', 'Operation Management', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_LocationManagement', 'Location Management', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_ItemTypeManagement', 'Inventory Type Management', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_User_Management', 'User Management', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_Shortcuts', 'Keyboard Shortcuts', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);

INSERT INTO UserControlMapping (WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive)
SELECT wfm.MappingID, 'Control_Theme', 'Theme Settings', 1
FROM WindowFormMapping wfm
WHERE wfm.CodebaseName = 'SettingsForm'
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);
