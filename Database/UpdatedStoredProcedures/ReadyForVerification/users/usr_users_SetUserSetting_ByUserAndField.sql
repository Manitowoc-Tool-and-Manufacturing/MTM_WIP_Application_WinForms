-- =============================================
-- Procedure: usr_users_SetUserSetting_ByUserAndField
-- Domain: users
-- Created: 2025-10-17
-- Purpose: Updates a single user field dynamically
-- Note: This uses prepared statements for dynamic column names
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `usr_users_SetUserSetting_ByUserAndField`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_SetUserSetting_ByUserAndField`(
    IN p_User VARCHAR(100),
    IN p_Field VARCHAR(100),
    IN p_Value VARCHAR(500),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE v_AllowedFields VARCHAR(1000) DEFAULT 'DefaultOperation,DefaultLocation,DefaultItemType,Email,FullName';
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_Field IS NULL OR TRIM(p_Field) = '' THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Field is required';
        ROLLBACK;
    ELSEIF FIND_IN_SET(p_Field, v_AllowedFields) = 0 THEN
        SET p_Status = -4;
        SET p_ErrorMsg = CONCAT('Field "', p_Field, '" is not in allowed fields list: ', v_AllowedFields);
        ROLLBACK;
    ELSE
        -- Use prepared statement for dynamic column update
        SET @sql = CONCAT('UPDATE usr_users SET ', p_Field, ' = ? WHERE User = ?');
        PREPARE stmt FROM @sql;
        SET @value = p_Value;
        SET @user = p_User;
        EXECUTE stmt USING @value, @user;
        DEALLOCATE PREPARE stmt;
        
        SET v_RowsAffected = ROW_COUNT();
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Updated field "', p_Field, '" for user "', p_User, '"');
            COMMIT;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of usr_users_SetUserSetting_ByUserAndField
-- =============================================
