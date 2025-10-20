-- BYPASS_MCP_CHECK: SQL_INJECTION
-- Reason: MySQL user management requires dynamic SQL to CREATE USER and GRANT privileges.
-- Security: Username validated with REGEXP to allow only alphanumeric, underscore, and hyphen.
-- Defense in depth: Additional escaping via REPLACE for single quotes.
DELIMITER //
DROP PROCEDURE IF EXISTS `usr_users_Add_User`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Add_User`(
    IN p_User VARCHAR(100),
    IN p_FullName VARCHAR(200),
    IN p_Shift VARCHAR(50),
    IN p_VitsUser TINYINT,
    IN p_Pin VARCHAR(50),
    IN p_LastShownVersion VARCHAR(50),
    IN p_HideChangeLog VARCHAR(50),
    IN p_Theme_Name VARCHAR(50),
    IN p_Theme_FontSize INT,
    IN p_VisualUserName VARCHAR(50),
    IN p_VisualPassword VARCHAR(50),
    IN p_WipServerAddress VARCHAR(15),
    IN p_WipServerPort VARCHAR(10),
    IN p_WipDatabase VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSEIF p_FullName IS NULL OR TRIM(p_FullName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Full Name is required';
    ELSE
        -- Validate username contains only safe characters (alphanumeric, underscore, hyphen)
        IF p_User REGEXP '[^A-Za-z0-9_-]' THEN
            SET p_Status = -2;
            SET p_ErrorMsg = 'Username contains invalid characters. Only alphanumeric, underscore, and hyphen allowed.';
        ELSE
            INSERT INTO usr_users (
                `User`, `Full Name`, `Shift`, `VitsUser`, `Pin`, `LastShownVersion`, `HideChangeLog`,
                `Theme_Name`, `Theme_FontSize`, `VisualUserName`, `VisualPassword`,
                `WipServerAddress`, `WipDatabase`, `WipServerPort`
            ) VALUES (
                p_User, p_FullName, p_Shift, p_VitsUser, p_Pin, p_LastShownVersion, p_HideChangeLog,
                p_Theme_Name, p_Theme_FontSize, p_VisualUserName, p_VisualPassword,
                p_WipServerAddress, p_WipDatabase, p_WipServerPort
            );
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                -- NOTE: Dynamic SQL required for MySQL user management
                -- Username is validated above to contain only safe characters
                -- Additional escaping applied via REPLACE for defense in depth
                SET @createUserQuery := CONCAT(
                    'CREATE USER IF NOT EXISTS ''', REPLACE(p_User, '''', ''''''), '''@''%'''
                );
                PREPARE stmt FROM @createUserQuery;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                SET @grantAllQuery := CONCAT(
                    'GRANT ALL PRIVILEGES ON *.* TO ''', REPLACE(p_User, '''', ''''''), '''@''%'';'
                );
                PREPARE stmt FROM @grantAllQuery;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                FLUSH PRIVILEGES;
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('User "', p_User, '" added successfully');
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to add user';
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
