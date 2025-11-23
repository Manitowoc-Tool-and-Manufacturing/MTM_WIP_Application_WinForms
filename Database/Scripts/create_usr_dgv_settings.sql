-- Create table for DataGridView settings
CREATE TABLE IF NOT EXISTS usr_dgv_settings (
    UserId VARCHAR(100) NOT NULL,
    DgvName VARCHAR(100) NOT NULL,
    SettingsJson JSON,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (UserId, DgvName),
    CONSTRAINT fk_usr_dgv_settings_user FOREIGN KEY (UserId) REFERENCES usr_users(User) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Procedure to Save Grid Settings
DROP PROCEDURE IF EXISTS usr_dgv_settings_Set;
DELIMITER $$
CREATE PROCEDURE usr_dgv_settings_Set(
    IN p_UserId VARCHAR(64),
    IN p_DgvName VARCHAR(100),
    IN p_SettingsJson LONGTEXT,
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

    IF NOT JSON_VALID(p_SettingsJson) THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid JSON format';
    ELSE
        INSERT INTO usr_dgv_settings (UserId, DgvName, SettingsJson)
        VALUES (p_UserId, p_DgvName, CAST(p_SettingsJson AS JSON))
        ON DUPLICATE KEY UPDATE
            SettingsJson = CAST(p_SettingsJson AS JSON);

        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Settings saved for grid: ', p_DgvName);
    END IF;
END$$
DELIMITER ;

-- Procedure to Get Grid Settings
DROP PROCEDURE IF EXISTS usr_dgv_settings_Get;
DELIMITER $$
CREATE PROCEDURE usr_dgv_settings_Get(
    IN p_UserId VARCHAR(64),
    IN p_DgvName VARCHAR(100),
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

    SELECT SettingsJson
    FROM usr_dgv_settings
    WHERE UserId = p_UserId AND DgvName = p_DgvName;
    
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Settings retrieved successfully';
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No settings found';
    END IF;
END$$
DELIMITER ;