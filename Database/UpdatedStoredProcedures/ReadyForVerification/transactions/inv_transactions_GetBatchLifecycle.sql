-- =============================================
-- Procedure: inv_transactions_GetBatchLifecycle
-- Domain: inventory
-- Created: 2025-11-03
-- Purpose: Retrieve complete transaction lifecycle for a specific batch number
--          Returns all transactions chronologically ordered for lifecycle visualization
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_transactions_GetBatchLifecycle`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transactions_GetBatchLifecycle`(
    IN p_BatchNumber VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Error handler
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    -- Validate input parameters
    IF p_BatchNumber IS NULL OR TRIM(p_BatchNumber) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Batch number is required';
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Batch number is required';
    END IF;
    
    -- Get count of matching transactions
    SELECT COUNT(*)
    INTO v_Count
    FROM inv_transaction
    WHERE BatchNumber = p_BatchNumber;
    
    -- Return all transactions for the batch in chronological order
    SELECT 
        ID,
        TransactionType,
        PartID,
        BatchNumber,
        Quantity,
        FromLocation,
        ToLocation,
        Operation,
        User,
        ReceiveDate,
        ItemType,
        Notes
    FROM inv_transaction
    WHERE BatchNumber = p_BatchNumber
    ORDER BY ReceiveDate ASC;
    
    -- Set success status
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Found ', v_Count, ' transaction(s) for batch ', p_BatchNumber);
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('No transactions found for batch ', p_BatchNumber);
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of inv_transactions_GetBatchLifecycle
-- =============================================
