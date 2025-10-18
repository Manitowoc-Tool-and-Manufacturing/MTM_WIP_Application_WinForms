-- =============================================
-- Procedure: log_error_Get_Unique
-- Domain: logging
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `log_error_Get_Unique`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Get_Unique`(

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



    -- Get unique error combinations

    SELECT DISTINCT `MethodName`, `ErrorMessage` 

    FROM `log_error` 

    WHERE `MethodName` IS NOT NULL AND `ErrorMessage` IS NOT NULL

    ORDER BY `MethodName`, `ErrorMessage`;

    

    -- Check if data was returned

    SELECT FOUND_ROWS() INTO v_Count;



    IF v_Count > 0 THEN

        SET p_Status = 1;  -- Success with data

        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' unique error combinations');

    ELSE

        SET p_Status = 0;  -- Success but no data

        SET p_ErrorMsg = 'No unique error combinations found';

    END IF;



    COMMIT;

END
//

DELIMITER ;

-- =============================================
-- End of log_error_Get_Unique
-- =============================================
