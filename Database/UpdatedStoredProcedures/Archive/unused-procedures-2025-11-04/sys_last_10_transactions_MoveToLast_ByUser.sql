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
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSEIF p_ReceiveDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Receive date is required';
    ELSE
        SELECT id INTO target_id
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate = p_ReceiveDate
        LIMIT 1;
        IF target_id IS NULL THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Transaction not found for given user and date';
        ELSE
            SELECT MAX(ReceiveDate) INTO latest_date
            FROM sys_last_10_transactions
            WHERE User = p_User;
            IF p_ReceiveDate < latest_date THEN
                UPDATE sys_last_10_transactions
                SET ReceiveDate = DATE_ADD(latest_date, INTERVAL 1 SECOND)
                WHERE id = target_id;
                SET v_RowCount = ROW_COUNT();
                IF v_RowCount > 0 THEN
                    SET p_Status = 1;
                    SET p_ErrorMsg = 'Transaction moved to last position';
                ELSE
                    SET p_Status = -3;
                    SET p_ErrorMsg = 'Failed to update transaction date';
                END IF;
            ELSE
                SET p_Status = 0;
                SET p_ErrorMsg = 'Transaction already at last position';
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
