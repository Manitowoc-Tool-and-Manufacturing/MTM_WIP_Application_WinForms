-- =============================================
-- Procedure: sys_GetRoleIdByName
-- Domain: system
-- Created: 2025-10-17
-- Purpose: Returns RoleId for given RoleName from sys_roles table
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_GetRoleIdByName`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_GetRoleIdByName`(
    IN p_RoleName VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE p_RoleId INT DEFAULT NULL;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Validate input
    IF p_RoleName IS NULL OR TRIM(p_RoleName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'RoleName is required';
    ELSE
        -- Get role ID by name
        SELECT ID INTO p_RoleId
        FROM sys_roles
        WHERE RoleName = p_RoleName
        LIMIT 1;
        
        IF p_RoleId IS NOT NULL THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Found RoleId ', p_RoleId, ' for role "', p_RoleName, '"');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('Role "', p_RoleName, '" not found');
            SET p_RoleId = 0;  -- Return 0 for not found
        END IF;
        
        -- Always return role ID as scalar (0 if not found)
        SELECT p_RoleId AS RoleId;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of sys_GetRoleIdByName
-- =============================================
