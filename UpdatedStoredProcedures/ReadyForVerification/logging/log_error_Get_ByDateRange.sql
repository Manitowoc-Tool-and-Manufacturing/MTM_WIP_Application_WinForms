-- =============================================
-- Procedure: log_error_Get_ByDateRange
-- Domain: logging
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `log_error_Get_ByDateRange`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Get_ByDateRange`(

    IN p_StartDate DATETIME,

    IN p_EndDate DATETIME,

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



    -- Validate parameters

    IF p_StartDate IS NULL OR p_EndDate IS NULL THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Start date and end date are required';

        ROLLBACK;

    ELSEIF p_StartDate > p_EndDate THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Start date must be less than or equal to end date';

        ROLLBACK;

    ELSE

        -- Get error logs by date range

        SELECT * FROM `log_error` 

        WHERE `ErrorTime` BETWEEN p_StartDate AND p_EndDate 

        ORDER BY `ErrorTime` DESC;

        

        -- Check if data was returned

        SELECT FOUND_ROWS() INTO v_Count;



        IF v_Count > 0 THEN

            SET p_Status = 1;  -- Success with data

            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' error log entries between ', 

                                   DATE_FORMAT(p_StartDate, '%Y-%m-%d %H:%i:%s'), ' and ', 

                                   DATE_FORMAT(p_EndDate, '%Y-%m-%d %H:%i:%s'));

        ELSE

            SET p_Status = 0;  -- Success but no data

            SET p_ErrorMsg = CONCAT('No error log entries found between ', 

                                   DATE_FORMAT(p_StartDate, '%Y-%m-%d %H:%i:%s'), ' and ', 

                                   DATE_FORMAT(p_EndDate, '%Y-%m-%d %H:%i:%s'));

        END IF;

        

        COMMIT;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of log_error_Get_ByDateRange
-- =============================================
