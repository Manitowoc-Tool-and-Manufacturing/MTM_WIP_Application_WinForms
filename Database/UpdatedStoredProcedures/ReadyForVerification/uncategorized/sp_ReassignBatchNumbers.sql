DELIMITER //
DROP PROCEDURE IF EXISTS `sp_ReassignBatchNumbers`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ReassignBatchNumbers`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE done INT DEFAULT 0;
    DECLARE curINID BIGINT;
    DECLARE curPartID VARCHAR(50);
    DECLARE curINDate DATETIME;
    DECLARE curQty DECIMAL(18,4);
    DECLARE curFromLocation VARCHAR(50);
    DECLARE curToLocation VARCHAR(50);
    DECLARE newBatch BIGINT;
    DECLARE batch_str VARCHAR(20);
    DECLARE curOtherID BIGINT;
    DECLARE curOtherPartID VARCHAR(50);
    DECLARE v_ProcessedCount INT DEFAULT 0;
    DECLARE in_cursor CURSOR FOR
        SELECT ID, PartID, ReceiveDate, Quantity, FromLocation, ToLocation
        FROM mtm_wip_application.inv_transaction
        WHERE TransactionType = 'IN' AND BatchNumber IS NULL
        ORDER BY PartID, ReceiveDate;
    DECLARE other_cursor CURSOR FOR
        SELECT ID, PartID
        FROM mtm_wip_application.inv_transaction
        WHERE TransactionType IN ('TRANSFER', 'OUT') AND BatchNumber IS NULL
        ORDER BY PartID;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    SELECT last_batch_number INTO newBatch FROM mtm_wip_application.inv_inventory_batch_seq;
    OPEN in_cursor;
    in_loop: LOOP
        FETCH in_cursor INTO curINID, curPartID, curINDate, curQty, curFromLocation, curToLocation;
        IF done THEN
            LEAVE in_loop;
        END IF;
        SET newBatch = newBatch + 1;
        SET batch_str = LPAD(newBatch, 10, '0');
        UPDATE mtm_wip_application.inv_transaction
        SET BatchNumber = batch_str
        WHERE ID = curINID;
        SET v_ProcessedCount = v_ProcessedCount + 1;
    END LOOP;
    CLOSE in_cursor;
    SET done = 0;
    OPEN other_cursor;
    other_loop: LOOP
        FETCH other_cursor INTO curOtherID, curOtherPartID;
        IF done THEN
            LEAVE other_loop;
        END IF;
        SET newBatch = newBatch + 1;
        SET batch_str = LPAD(newBatch, 10, '0');
        UPDATE mtm_wip_application.inv_transaction
        SET BatchNumber = batch_str
        WHERE ID = curOtherID;
        SET v_ProcessedCount = v_ProcessedCount + 1;
    END LOOP;
    CLOSE other_cursor;
    UPDATE mtm_wip_application.inv_inventory_batch_seq
    SET last_batch_number = newBatch;
    IF v_ProcessedCount > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Reassigned ', v_ProcessedCount, ' batch number(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No batch numbers needed reassignment';
    END IF;
END
//
DELIMITER ;
