-- =============================================
-- Procedure: log_error_Get_ByUser
-- Domain: logging
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `log_error_Get_ByUser`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Get_ByUser`(

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

        -- Get error logs by user

        SELECT * FROM `log_error` 

        WHERE `User` = p_User 

        ORDER BY `ErrorTime` DESC;

        

        -- Check if data was returned

        SELECT FOUND_ROWS() INTO v_Count;



        IF v_Count > 0 THEN

            SET p_Status = 1;  -- Success with data

            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' error log entries for user: ', p_User);

        ELSE

            SET p_Status = 0;  -- Success but no data

            SET p_ErrorMsg = CONCAT('No error log entries found for user: ', p_User);

        END IF;

        

        COMMIT;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of log_error_Get_ByUser
-- =============================================
