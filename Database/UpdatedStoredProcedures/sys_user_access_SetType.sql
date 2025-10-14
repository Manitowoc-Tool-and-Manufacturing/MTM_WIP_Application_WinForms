-- =============================================
-- Stored Procedure: sys_user_access_SetType
-- Description: Sets user access type (for testing purposes)
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_user_access_SetType`$$

CREATE PROCEDURE `sys_user_access_SetType`(
    IN p_User VARCHAR(100),
    IN p_AccessType INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while setting user access type';
    END;

    -- Validate input
    IF p_User IS NULL OR p_User = '' THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'User name is required';
    ELSEIF p_AccessType IS NULL THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Access type is required';
    ELSE
        -- Check if user exists
        IF NOT EXISTS (SELECT 1 FROM usr_users WHERE User = p_User) THEN
            SET p_Status = -1;
            SET p_ErrorMsg = CONCAT('User ''', p_User, ''' not found');
        ELSE
            -- Update access type (using VitsUser as a proxy field for access type)
            UPDATE usr_users SET VitsUser = p_AccessType WHERE User = p_User;
            
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('Access type updated for user ', p_User);
        END IF;
    END IF;
END$$

DELIMITER ;
