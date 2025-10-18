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
    DECLARE v_RoleId INT DEFAULT NULL;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate input
    IF p_RoleName IS NULL OR TRIM(p_RoleName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'RoleName is required';
        ROLLBACK;
    ELSE
        -- Get role ID by name
        SELECT RoleID INTO v_RoleId
        FROM sys_roles
        WHERE RoleName = p_RoleName
        LIMIT 1;
        
        IF v_RoleId IS NOT NULL THEN
            -- Return role ID as scalar
            SELECT v_RoleId AS RoleId;
            
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Found RoleId ', v_RoleId, ' for role "', p_RoleName, '"');
            COMMIT;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('Role "', p_RoleName, '" not found');
            COMMIT;
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of sys_GetRoleIdByName
-- =============================================
