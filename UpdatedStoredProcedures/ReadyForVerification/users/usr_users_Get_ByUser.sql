-- =============================================
-- Procedure: usr_users_Get_ByUser
-- Domain: users
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `usr_users_Get_ByUser`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Get_ByUser`(

    IN p_User VARCHAR(100),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_Count INT DEFAULT 0;

    

    -- Exit handler for any SQL exception

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1

            p_ErrorMsg = MESSAGE_TEXT;

        SET p_Status = -1;

        ROLLBACK;

    END;

    

    START TRANSACTION;

    

    -- Validate parameter

    IF p_User IS NULL OR TRIM(p_User) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'User is required';

        ROLLBACK;

    ELSE

        -- Get user by username

        SELECT * FROM usr_users 

        WHERE `User` = p_User;

        

        -- Check if data was returned

        SELECT FOUND_ROWS() INTO v_Count;

        

        IF v_Count > 0 THEN

            SET p_Status = 1;  -- Success with data

            SET p_ErrorMsg = CONCAT('Retrieved user "', p_User, '"');

            COMMIT;

        ELSE

            SET p_Status = 0;  -- Success but no data

            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');

            COMMIT;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of usr_users_Get_ByUser
-- =============================================
