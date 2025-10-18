-- =============================================
-- Procedure: usr_users_Delete_User
-- Domain: users
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `usr_users_Delete_User`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Delete_User`(

    IN p_User VARCHAR(100),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_RowCount INT DEFAULT 0;

    

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

        -- Drop MySQL user first

        SET @d := CONCAT('DROP USER IF EXISTS \\'', REPLACE(p_User, '\\'', '\\\\\\''), '\\'@\\'%\\';');

        PREPARE stmt FROM @d;

        EXECUTE stmt;

        DEALLOCATE PREPARE stmt;

        

        -- Delete user from table

        DELETE FROM usr_users WHERE `User` = p_User;

        

        -- Check if delete was successful

        SET v_RowCount = ROW_COUNT();

        

        IF v_RowCount > 0 THEN

            SET p_Status = 1;  -- Success

            SET p_ErrorMsg = CONCAT('User "', p_User, '" deleted successfully');

            COMMIT;

        ELSE

            SET p_Status = -4;  -- Not found

            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');

            ROLLBACK;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of usr_users_Delete_User
-- =============================================
