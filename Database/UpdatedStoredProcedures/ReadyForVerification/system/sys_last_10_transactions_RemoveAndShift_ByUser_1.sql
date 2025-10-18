DELIMITER //
DROP PROCEDURE IF EXISTS `sys_last_10_transactions_RemoveAndShift_ByUser_1`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_RemoveAndShift_ByUser_1`(
    IN p_User VARCHAR(255),
    IN p_Position INT,
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
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_Position IS NULL OR p_Position < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid position is required';
        ROLLBACK;
    ELSE
        SELECT COUNT(*) INTO v_Exists
        FROM sys_last_10_transactions
        WHERE User = p_User AND Position = p_Position;
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('No transaction found at position ', p_Position, ' for user: ', p_User);
            ROLLBACK;
        ELSE
            DELETE FROM sys_last_10_transactions
            WHERE User = p_User AND Position = p_Position;
            SET v_RowCount = ROW_COUNT();
            UPDATE sys_last_10_transactions
            SET Position = Position - 1
            WHERE User = p_User AND Position > p_Position;
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Transaction at position ', p_Position, ' removed and remaining transactions shifted');
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to remove transaction';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
