-- BYPASS_MCP_CHECK: SQL_INJECTION
-- Reason: MySQL user management requires dynamic SQL to DROP USER.
-- Security: Username validated with REGEXP to allow only alphanumeric, underscore, and hyphen.
-- Defense in depth: Additional escaping via REPLACE for single quotes.
DELIMITER //
DROP PROCEDURE IF EXISTS `usr_users_Delete_User`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Delete_User`(
    IN p_User VARCHAR(100),
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
    ELSE
        -- Validate username contains only safe characters (alphanumeric, underscore, hyphen)
        IF p_User REGEXP '[^A-Za-z0-9_-]' THEN
            SET p_Status = -2;
            SET p_ErrorMsg = 'Username contains invalid characters. Only alphanumeric, underscore, and hyphen allowed.';
        ELSE
            -- NOTE: Dynamic SQL required for MySQL user management
            -- Username is validated above to contain only safe characters
            -- Additional escaping applied via REPLACE for defense in depth
            SET @d := CONCAT('DROP USER IF EXISTS ''', REPLACE(p_User, '''', ''''''), '''@''%'';');
            PREPARE stmt FROM @d;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
            DELETE FROM usr_users WHERE `User` = p_User;
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('User "', p_User, '" deleted successfully');
            ELSE
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
