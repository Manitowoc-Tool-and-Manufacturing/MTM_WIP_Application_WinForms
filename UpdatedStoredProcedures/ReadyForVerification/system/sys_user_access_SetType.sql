-- =============================================
-- Procedure: sys_user_access_SetType
-- Domain: system
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_user_access_SetType`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_access_SetType`(

    IN p_User VARCHAR(100),

    IN p_AccessType INT,

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_Exists INT DEFAULT 0;

    DECLARE v_RowCount INT DEFAULT 0;

    

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1

            p_ErrorMsg = MESSAGE_TEXT;

        SET p_Status = -1;

        ROLLBACK;

    END;

    

    START TRANSACTION;



    -- Validate input

    IF p_User IS NULL OR TRIM(p_User) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'User name is required';

        ROLLBACK;

    ELSEIF p_AccessType IS NULL THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Access type is required';

        ROLLBACK;

    ELSE

        -- Check if user exists

        SELECT COUNT(*) INTO v_Exists FROM usr_users WHERE User = p_User;

        

        IF v_Exists = 0 THEN

            SET p_Status = -4;  -- Not found

            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');

            ROLLBACK;

        ELSE

            -- Update access type (using VitsUser as a proxy field for access type)

            UPDATE usr_users SET VitsUser = p_AccessType WHERE User = p_User;

            

            -- Check if update was successful

            SET v_RowCount = ROW_COUNT();

            

            IF v_RowCount > 0 THEN

                SET p_Status = 1;  -- Success

                SET p_ErrorMsg = CONCAT('Access type updated for user "', p_User, '"');

                COMMIT;

            ELSE

                SET p_Status = -3;  -- Business logic error

                SET p_ErrorMsg = 'Failed to update access type';

                ROLLBACK;

            END IF;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of sys_user_access_SetType
-- =============================================
