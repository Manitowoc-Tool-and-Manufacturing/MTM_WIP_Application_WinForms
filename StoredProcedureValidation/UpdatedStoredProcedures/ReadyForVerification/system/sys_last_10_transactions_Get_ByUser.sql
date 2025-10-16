DELIMITER //
DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Get_ByUser`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Get_ByUser`(
    IN p_User VARCHAR(100),
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
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        SELECT
            Position,
            User,
            PartID AS p_PartID,
            Operation AS p_Operation,
            Quantity,
            ReceiveDate
        FROM sys_last_10_transactions
        WHERE User = p_User
        ORDER BY Position;
        SELECT FOUND_ROWS() INTO v_Count;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' quick button(s) for user: ', p_User);
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No quick buttons found for user: ', p_User);
        END IF;
        COMMIT;
    END IF;
END
//
DELIMITER ;
