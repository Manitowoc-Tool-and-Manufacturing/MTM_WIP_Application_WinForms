DELIMITER //
DROP PROCEDURE IF EXISTS `sys_last_10_transactions_RemoveAndShift_ByUser`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_RemoveAndShift_ByUser`(
    IN p_User VARCHAR(255),
    IN p_ReceiveDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE target_exists INT;
    DECLARE last_id INT;
    DECLARE done INT DEFAULT 0;
    DECLARE curr_id INT;
    DECLARE curr_date DATETIME;
    DECLARE prev_date DATETIME;
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE dates_to_shift CURSOR FOR
        SELECT id, ReceiveDate
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate > p_ReceiveDate
        ORDER BY ReceiveDate;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
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
        SELECT COUNT(*) INTO target_exists
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate = p_ReceiveDate;
        IF target_exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Transaction not found for given user and date';
        ELSE
            SET prev_date = p_ReceiveDate;
            OPEN dates_to_shift;
            read_loop: LOOP
                FETCH dates_to_shift INTO curr_id, curr_date;
                IF done THEN
                    LEAVE read_loop;
                END IF;
                UPDATE sys_last_10_transactions
                SET ReceiveDate = prev_date
                WHERE id = curr_id;
                SET prev_date = curr_date;
                SET last_id = curr_id;
            END LOOP;
            CLOSE dates_to_shift;
            DELETE FROM sys_last_10_transactions
            WHERE User = p_User AND ReceiveDate = p_ReceiveDate
            LIMIT 1;
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = 'Transaction removed and remaining transactions shifted';
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to delete transaction';
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
