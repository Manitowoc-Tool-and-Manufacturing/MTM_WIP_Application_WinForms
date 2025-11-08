-- Procedure: inv_transaction_Delete_ByID
-- Description: Deletes a transaction record by ID (Admin/Developer only)
-- SECURITY: This operation should only be called by Admin/Developer users
-- WARNING: This permanently removes transaction history data

DELIMITER //
DROP PROCEDURE IF EXISTS `inv_transaction_Delete_ByID`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transaction_Delete_ByID`(
    IN p_ID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE v_ErrorMessage TEXT DEFAULT '';
    DECLARE v_TransactionExists INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_ErrorMessage = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error occurred while deleting transaction ID: ', p_ID, ' - ', v_ErrorMessage);
    END;

    -- Validate input
    IF p_ID IS NULL OR p_ID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid transaction ID provided';
    ELSE
        -- Check if transaction exists
        SELECT COUNT(*) INTO v_TransactionExists 
        FROM inv_transaction 
        WHERE ID = p_ID;
        
        IF v_TransactionExists = 0 THEN
            SET p_Status = -3;
            SET p_ErrorMsg = CONCAT('Transaction ID ', p_ID, ' not found');
        ELSE
            -- Delete the transaction
            DELETE FROM inv_transaction 
            WHERE ID = p_ID;
            
            SET v_RowsAffected = ROW_COUNT();
            
            IF v_RowsAffected > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Transaction ID ', p_ID, ' deleted successfully');
            ELSE
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('Failed to delete transaction ID ', p_ID);
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;

-- End of inv_transaction_Delete_ByID
