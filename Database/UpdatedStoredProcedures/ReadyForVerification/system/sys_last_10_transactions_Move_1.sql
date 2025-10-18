DELIMITER //
DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Move_1`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Move_1`(
    IN p_User VARCHAR(255),
    IN p_FromPosition INT,
    IN p_ToPosition INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE tempPartID VARCHAR(255);
    DECLARE tempOperation VARCHAR(255);
    DECLARE tempQuantity INT;
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
    ELSEIF p_FromPosition IS NULL OR p_FromPosition < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid from position is required';
        ROLLBACK;
    ELSEIF p_ToPosition IS NULL OR p_ToPosition < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid to position is required';
        ROLLBACK;
    ELSE
        SELECT PartID, Operation, Quantity
        INTO tempPartID, tempOperation, tempQuantity
        FROM sys_last_10_transactions
        WHERE User = p_User AND Position = p_FromPosition
        LIMIT 1;
        IF tempPartID IS NULL THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('No transaction found at position ', p_FromPosition, ' for user: ', p_User);
            ROLLBACK;
        ELSE
            DELETE FROM sys_last_10_transactions
            WHERE User = p_User AND Position = p_FromPosition;
            IF p_FromPosition < p_ToPosition THEN
                UPDATE sys_last_10_transactions
                SET Position = Position - 1
                WHERE User = p_User AND Position > p_FromPosition AND Position <= p_ToPosition;
            ELSE
                UPDATE sys_last_10_transactions
                SET Position = Position + 1
                WHERE User = p_User AND Position >= p_ToPosition AND Position < p_FromPosition;
            END IF;
            INSERT INTO sys_last_10_transactions (User, PartID, Operation, Quantity, Position)
            VALUES (p_User, tempPartID, tempOperation, tempQuantity, p_ToPosition);
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Moved transaction from position ', p_FromPosition, ' to ', p_ToPosition);
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to insert transaction at new position';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
