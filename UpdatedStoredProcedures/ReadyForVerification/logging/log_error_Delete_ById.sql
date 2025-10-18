-- =============================================
-- Procedure: log_error_Delete_ById
-- Domain: logging
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `log_error_Delete_ById`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Delete_ById`(

    IN p_Id INT,

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

    IF p_Id IS NULL OR p_Id <= 0 THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Valid error log ID is required';

        ROLLBACK;

    ELSE

        -- Check if error log entry exists

        SELECT COUNT(*) INTO v_Exists FROM `log_error` WHERE `ID` = p_Id;

        

        IF v_Exists = 0 THEN

            SET p_Status = -4;

            SET p_ErrorMsg = CONCAT('Error log entry with ID ', p_Id, ' not found');

            ROLLBACK;

        ELSE

            -- Delete error log entry

            DELETE FROM `log_error` WHERE `ID` = p_Id;

            

            SET v_RowCount = ROW_COUNT();

            

            IF v_RowCount > 0 THEN

                SET p_Status = 1;

                SET p_ErrorMsg = CONCAT('Error log entry with ID ', p_Id, ' deleted successfully');

                COMMIT;

            ELSE

                SET p_Status = -3;

                SET p_ErrorMsg = 'Failed to delete error log entry';

                ROLLBACK;

            END IF;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of log_error_Delete_ById
-- =============================================
