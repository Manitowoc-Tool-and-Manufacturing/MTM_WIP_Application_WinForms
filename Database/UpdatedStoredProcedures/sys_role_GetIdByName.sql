-- =============================================
-- Stored Procedure: sys_role_GetIdByName
-- Description: Retrieves role ID by role name (scalar result)
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_role_GetIdByName`$$

CREATE PROCEDURE `sys_role_GetIdByName`(
    IN p_RoleName VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RoleId INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving role ID';
    END;

    -- Validate input
    IF p_RoleName IS NULL OR p_RoleName = '' THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Role name is required';
        SELECT 0 AS RoleID; -- Return 0 for error case
    ELSE
        -- Get role ID
        SELECT ID INTO v_RoleId FROM sys_roles WHERE RoleName = p_RoleName LIMIT 1;
        
        IF v_RoleId IS NULL OR v_RoleId = 0 THEN
            SET p_Status = -1;
            SET p_ErrorMsg = CONCAT('Role ''', p_RoleName, ''' not found');
            SELECT 0 AS RoleID;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('Role ID retrieved for ', p_RoleName);
            SELECT v_RoleId AS RoleID;
        END IF;
    END IF;
END$$

DELIMITER ;
