DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Reorder_ByUser`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Reorder_ByUser`(
    IN p_User VARCHAR(255), 
    IN p_SrcReceiveDate DATETIME, 
    IN p_TargetIndex INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_SrcIdx INT;
    DECLARE v_TotalCount INT;
    DECLARE v_SrcDate DATETIME;
    DECLARE v_CurDate DATETIME;
    DECLARE v_Counter INT;
    DECLARE v_BaseTime DATETIME;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        DROP TEMPORARY TABLE IF EXISTS tempDates;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_SrcReceiveDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Source receive date is required';
        ROLLBACK;
    ELSEIF p_TargetIndex IS NULL OR p_TargetIndex < 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid target index is required';
        ROLLBACK;
    ELSE
        -- Create temp table
        CREATE TEMPORARY TABLE tempDates (idx INT AUTO_INCREMENT PRIMARY KEY, dt DATETIME);
        INSERT INTO tempDates (dt)
            SELECT ReceiveDate FROM sys_last_10_transactions WHERE User = p_User ORDER BY ReceiveDate;
        
        -- Find source index
        SELECT idx INTO v_SrcIdx FROM tempDates WHERE dt = p_SrcReceiveDate LIMIT 1;
        SELECT COUNT(*) INTO v_TotalCount FROM tempDates;
        
        IF v_SrcIdx IS NULL THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Source transaction not found';
            DROP TEMPORARY TABLE tempDates;
            ROLLBACK;
        ELSEIF v_SrcIdx = p_TargetIndex + 1 THEN
            SET p_Status = 0;
            SET p_ErrorMsg = 'Transaction already at target position';
            DROP TEMPORARY TABLE tempDates;
            COMMIT;
        ELSE
            -- Get source date
            SELECT dt INTO v_SrcDate FROM tempDates WHERE idx = v_SrcIdx LIMIT 1;
            
            -- Remove from current position
            DELETE FROM tempDates WHERE idx = v_SrcIdx;
            UPDATE tempDates SET idx = idx - 1 WHERE idx > v_SrcIdx;
            
            -- Insert at new position
            UPDATE tempDates SET idx = idx + 1 WHERE idx > p_TargetIndex;
            INSERT INTO tempDates (idx, dt) VALUES (p_TargetIndex + 1, v_SrcDate);
            
            -- Reorder by updating ReceiveDates
            SET v_Counter = 1;
            SET v_BaseTime = NOW();
            
            WHILE v_Counter <= v_TotalCount DO
                SELECT dt INTO v_CurDate FROM tempDates WHERE idx = v_Counter LIMIT 1;
                IF v_CurDate IS NOT NULL THEN
                    UPDATE sys_last_10_transactions
                    SET ReceiveDate = DATE_ADD(v_BaseTime, INTERVAL v_Counter SECOND)
                    WHERE User = p_User AND ReceiveDate = v_CurDate;
                END IF;
                SET v_Counter = v_Counter + 1;
            END WHILE;
            
            DROP TEMPORARY TABLE tempDates;
            
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Transaction reordered to index ', p_TargetIndex);
            COMMIT;
        END IF;
    END IF;
END$$
DELIMITER ;