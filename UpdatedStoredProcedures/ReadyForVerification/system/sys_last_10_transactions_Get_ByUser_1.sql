-- =============================================
-- Procedure: sys_last_10_transactions_Get_ByUser_1
-- Domain: system
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Get_ByUser_1`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Get_ByUser_1`(

    IN p_User VARCHAR(255),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_Count INT DEFAULT 0;

    

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

        SET p_ErrorMsg = 'User is required';

        ROLLBACK;

    ELSE

        -- Get last 10 transactions for user

        SELECT *

        FROM sys_last_10_transactions

        WHERE User = p_User

        ORDER BY Position ASC

        LIMIT 10;

        

        SELECT FOUND_ROWS() INTO v_Count;

        

        IF v_Count > 0 THEN

            SET p_Status = 1;

            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' transaction(s) for user: ', p_User);

        ELSE

            SET p_Status = 0;

            SET p_ErrorMsg = CONCAT('No transactions found for user: ', p_User);

        END IF;

        

        COMMIT;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of sys_last_10_transactions_Get_ByUser_1
-- =============================================
