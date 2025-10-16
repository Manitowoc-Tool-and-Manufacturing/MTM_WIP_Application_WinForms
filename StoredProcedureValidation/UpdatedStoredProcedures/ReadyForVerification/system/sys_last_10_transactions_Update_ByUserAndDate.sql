DELIMITER //
DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Update_ByUserAndDate`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Update_ByUserAndDate`(
    IN p_User VARCHAR(255),
    IN p_OldReceiveDate DATETIME,
    IN p_PartID VARCHAR(255),
    IN p_Operation VARCHAR(255),
    IN p_Quantity INT,
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
    ELSEIF p_OldReceiveDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Old receive date is required';
        ROLLBACK;
    ELSEIF p_PartID IS NULL OR TRIM(p_PartID) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Part ID is required';
        ROLLBACK;
    ELSEIF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSEIF p_Quantity IS NULL OR p_Quantity < 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid quantity is required';
        ROLLBACK;
    ELSE
        SELECT COUNT(*) INTO v_Exists
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate = p_OldReceiveDate;
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Transaction not found for given user and date';
            ROLLBACK;
        ELSE
            UPDATE sys_last_10_transactions
            SET PartID = p_PartID,
                Operation = p_Operation,
                Quantity = p_Quantity,
                ReceiveDate = NOW()
            WHERE User = p_User AND ReceiveDate = p_OldReceiveDate;
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = 'Transaction updated successfully';
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to update transaction';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
