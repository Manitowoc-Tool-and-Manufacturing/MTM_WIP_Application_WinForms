-- =============================================
-- Procedure: sys_role_GetIdByName
-- Domain: system
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_role_GetIdByName`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_role_GetIdByName`(

    IN p_RoleName VARCHAR(50),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_RoleId INT DEFAULT 0;

    

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

        SET p_ErrorMsg = 'Role name is required';

        SELECT 0 AS RoleID;

        ROLLBACK;

    ELSE

        -- Get role ID

        SELECT ID INTO v_RoleId FROM sys_roles WHERE RoleName = p_RoleName LIMIT 1;

        

        IF v_RoleId IS NULL OR v_RoleId = 0 THEN

            SET p_Status = 0;  -- Success but no data (not found)

            SET p_ErrorMsg = CONCAT('Role "', p_RoleName, '" not found');

            SELECT 0 AS RoleID;

        ELSE

            SET p_Status = 1;  -- Success with data

            SET p_ErrorMsg = CONCAT('Role ID retrieved for "', p_RoleName, '"');

            SELECT v_RoleId AS RoleID;

        END IF;

        

        COMMIT;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of sys_role_GetIdByName
-- =============================================
