-- =============================================
-- Procedure: sys_user_GetIdByName
-- Domain: system
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_user_GetIdByName`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_GetIdByName`(

    IN p_UserName VARCHAR(100),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_UserId INT DEFAULT 0;

    

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1

            p_ErrorMsg = MESSAGE_TEXT;

        SET p_Status = -1;

        ROLLBACK;

    END;

    

    START TRANSACTION;



    -- Validate input

    IF p_UserName IS NULL OR TRIM(p_UserName) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'User name is required';

        SELECT 0 AS UserID;

        ROLLBACK;

    ELSE

        -- Get user ID

        SELECT ID INTO v_UserId FROM usr_users WHERE User = p_UserName LIMIT 1;

        

        IF v_UserId IS NULL OR v_UserId = 0 THEN

            SET p_Status = 0;  -- Success but no data (not found)

            SET p_ErrorMsg = CONCAT('User "', p_UserName, '" not found');

            SELECT 0 AS UserID;

        ELSE

            SET p_Status = 1;  -- Success with data

            SET p_ErrorMsg = CONCAT('User ID retrieved for "', p_UserName, '"');

            SELECT v_UserId AS UserID;

        END IF;

        

        COMMIT;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of sys_user_GetIdByName
-- =============================================
