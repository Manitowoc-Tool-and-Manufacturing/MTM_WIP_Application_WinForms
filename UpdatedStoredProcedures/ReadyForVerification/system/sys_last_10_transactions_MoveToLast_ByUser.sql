-- =============================================
-- Procedure: sys_last_10_transactions_MoveToLast_ByUser
-- Domain: system
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_MoveToLast_ByUser`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_MoveToLast_ByUser`(

    IN p_User VARCHAR(255),

    IN p_ReceiveDate DATETIME,

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE latest_date DATETIME;

    DECLARE target_id INT;

    DECLARE v_RowCount INT DEFAULT 0;

    

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1

            p_ErrorMsg = MESSAGE_TEXT;

        SET p_Status = -1;

        ROLLBACK;

    END;

    

    START TRANSACTION;

    

    -- Validate inputs

    IF p_User IS NULL OR TRIM(p_User) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'User is required';

        ROLLBACK;

    ELSEIF p_ReceiveDate IS NULL THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Receive date is required';

        ROLLBACK;

    ELSE

        -- Get target transaction ID

        SELECT id INTO target_id

        FROM sys_last_10_transactions

        WHERE User = p_User AND ReceiveDate = p_ReceiveDate

        LIMIT 1;

        

        IF target_id IS NULL THEN

            SET p_Status = -4;

            SET p_ErrorMsg = 'Transaction not found for given user and date';

            ROLLBACK;

        ELSE

            -- Get latest date

            SELECT MAX(ReceiveDate) INTO latest_date

            FROM sys_last_10_transactions

            WHERE User = p_User;

            

            -- Only update if not already at the end

            IF p_ReceiveDate < latest_date THEN

                UPDATE sys_last_10_transactions

                SET ReceiveDate = DATE_ADD(latest_date, INTERVAL 1 SECOND)

                WHERE id = target_id;

                

                SET v_RowCount = ROW_COUNT();

                

                IF v_RowCount > 0 THEN

                    SET p_Status = 1;

                    SET p_ErrorMsg = 'Transaction moved to last position';

                    COMMIT;

                ELSE

                    SET p_Status = -3;

                    SET p_ErrorMsg = 'Failed to update transaction date';

                    ROLLBACK;

                END IF;

            ELSE

                SET p_Status = 0;

                SET p_ErrorMsg = 'Transaction already at last position';

                COMMIT;

            END IF;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of sys_last_10_transactions_MoveToLast_ByUser
-- =============================================
