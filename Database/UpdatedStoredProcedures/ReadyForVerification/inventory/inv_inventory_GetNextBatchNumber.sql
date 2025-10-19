-- =============================================
-- Procedure: inv_inventory_GetNextBatchNumber
-- Domain: inventory
-- Created: 2025-10-17
-- Purpose: Returns next available batch number from sequence table
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_inventory_GetNextBatchNumber`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_GetNextBatchNumber`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_NextBatch BIGINT;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get and increment batch number
    SELECT last_batch_number INTO v_NextBatch 
    FROM inv_inventory_batch_seq;
    
    SET v_NextBatch = v_NextBatch + 1;
    
    -- Update sequence
    UPDATE inv_inventory_batch_seq 
    SET last_batch_number = v_NextBatch;
    
    -- Return the new batch number
    SELECT v_NextBatch AS NextBatchNumber;
    
    SET p_Status = 1;
    SET p_ErrorMsg = CONCAT('Generated batch number: ', v_NextBatch);
    COMMIT;
END
//

DELIMITER ;

-- =============================================
-- End of inv_inventory_GetNextBatchNumber
-- =============================================
