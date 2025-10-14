-- =============================================
-- Stored Procedure: sys_user_GetByName
-- Description: Retrieves user information by username
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_user_GetByName`$$

CREATE PROCEDURE `sys_user_GetByName`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving user information';
    END;

    -- Validate input
    IF p_User IS NULL OR p_User = '' THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'User name is required';
    ELSE
        -- Check if user exists
        IF NOT EXISTS (SELECT 1 FROM usr_users WHERE User = p_User) THEN
            SET p_Status = 1; -- Success but no data
            SET p_ErrorMsg = CONCAT('User ''', p_User, ''' not found');
            SELECT * FROM usr_users WHERE 1=0; -- Return empty result set with correct structure
        ELSE
            -- Return user information
            SELECT * FROM usr_users WHERE User = p_User;
            
            SET p_Status = 0;
            SET p_ErrorMsg = NULL;
        END IF;
    END IF;
END$$

DELIMITER ;
