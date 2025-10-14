-- =============================================
-- Stored Procedure: sys_user_GetIdByName
-- Description: Retrieves user ID by username (scalar result)
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_user_GetIdByName`$$

CREATE PROCEDURE `sys_user_GetIdByName`(
    IN p_UserName VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_UserId INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving user ID';
    END;

    -- Validate input
    IF p_UserName IS NULL OR p_UserName = '' THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'User name is required';
        SELECT 0 AS UserID; -- Return 0 for error case
    ELSE
        -- Get user ID
        SELECT ID INTO v_UserId FROM usr_users WHERE User = p_UserName LIMIT 1;
        
        IF v_UserId IS NULL OR v_UserId = 0 THEN
            SET p_Status = -1;
            SET p_ErrorMsg = CONCAT('User ''', p_UserName, ''' not found');
            SELECT 0 AS UserID;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('User ID retrieved for ', p_UserName);
            SELECT v_UserId AS UserID;
        END IF;
    END IF;
END$$

DELIMITER ;
