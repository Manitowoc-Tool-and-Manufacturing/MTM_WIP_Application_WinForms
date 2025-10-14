-- =============================================
-- Stored Procedure: sys_GetUserAccessType
-- Description: Retrieves all users with their role information
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_GetUserAccessType`$$

CREATE PROCEDURE `sys_GetUserAccessType`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving user access types';
    END;

    -- Validate that required tables exist
    IF NOT EXISTS (SELECT 1 FROM information_schema.tables 
                   WHERE table_schema = DATABASE() AND table_name = 'usr_users') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table usr_users not found';
        SELECT NULL LIMIT 0; -- Return empty result
    ELSEIF NOT EXISTS (SELECT 1 FROM information_schema.tables 
                       WHERE table_schema = DATABASE() AND table_name = 'sys_user_roles') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table sys_user_roles not found';
        SELECT NULL LIMIT 0; -- Return empty result
    ELSEIF NOT EXISTS (SELECT 1 FROM information_schema.tables 
                       WHERE table_schema = DATABASE() AND table_name = 'sys_roles') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table sys_roles not found';
        SELECT NULL LIMIT 0; -- Return empty result
    ELSE
        -- Return users with their roles
        -- Users without roles will still appear with NULL RoleName
        SELECT 
            u.ID AS UserID,
            u.User AS UserName,
            COALESCE(r.RoleName, 'User') AS RoleName
        FROM usr_users u
        LEFT JOIN sys_user_roles ur ON u.ID = ur.UserID
        LEFT JOIN sys_roles r ON ur.RoleID = r.ID
        ORDER BY u.User;
        
        SET p_Status = 0;
        SET p_ErrorMsg = 'User access types retrieved successfully';
    END IF;
END$$

DELIMITER ;
