DELIMITER //
DROP PROCEDURE IF EXISTS `sys_last_10_transactions_SwapPositions_ByUser`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_SwapPositions_ByUser`(
    IN p_User VARCHAR(255),
    IN p_ReceiveDate1 DATETIME,
    IN p_ReceiveDate2 DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE id1 INT;
    DECLARE id2 INT;
    DECLARE temp_date DATETIME;
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
    ELSEIF p_ReceiveDate1 IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'First receive date is required';
        ROLLBACK;
    ELSEIF p_ReceiveDate2 IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Second receive date is required';
        ROLLBACK;
    ELSE
        SELECT id INTO id1
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate = p_ReceiveDate1
        LIMIT 1;
        SELECT id INTO id2
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate = p_ReceiveDate2
        LIMIT 1;
        IF id1 IS NULL OR id2 IS NULL THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'One or both transactions not found';
            ROLLBACK;
        ELSE
            SET temp_date = p_ReceiveDate1;
            UPDATE sys_last_10_transactions
            SET ReceiveDate = p_ReceiveDate2
            WHERE id = id1;
            UPDATE sys_last_10_transactions
            SET ReceiveDate = temp_date
            WHERE id = id2;
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = 'Transaction positions swapped successfully';
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to swap positions';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
