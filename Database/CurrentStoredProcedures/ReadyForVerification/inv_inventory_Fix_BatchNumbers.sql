DELIMITER $$

DROP PROCEDURE IF EXISTS `inv_inventory_Fix_BatchNumbers`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Fix_BatchNumbers`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE done INT DEFAULT FALSE;
    DECLARE null_id INT;
    DECLARE nextBatch BIGINT;
    
    DECLARE cur CURSOR FOR SELECT ID FROM inv_inventory WHERE BatchNumber IS NULL ORDER BY ID;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE, 
            @errno = MYSQL_ERRNO, 
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;

    START TRANSACTION;
    
    -- Get current batch number
    SELECT last_batch_number INTO nextBatch FROM inv_inventory_batch_seq;
    
    -- Process each null batch number
    OPEN cur;
    read_loop: LOOP
        FETCH cur INTO null_id;
        IF done THEN
            LEAVE read_loop;
        END IF;
        
        SET nextBatch = nextBatch + 1;
        UPDATE inv_inventory SET BatchNumber = LPAD(nextBatch, 10, '0') WHERE ID = null_id;
        SET v_RowsAffected = v_RowsAffected + ROW_COUNT();
        
        -- Update sequence table
        UPDATE inv_inventory_batch_seq SET last_batch_number = nextBatch;
    END LOOP;
    CLOSE cur;
    
    COMMIT;
    
    IF v_RowsAffected > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Successfully fixed ', v_RowsAffected, ' batch number(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No batch numbers needed fixing';
    END IF;
END$$

DELIMITER ;