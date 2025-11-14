-- Updated Stored Procedure: inv_transaction_Add
-- Purpose: Add transaction record with color code and work order tracking
-- Feature: Color Code & Work Order Tracking - UPDATED
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

DELIMITER //
DROP PROCEDURE IF EXISTS `inv_transaction_Add`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transaction_Add`(
    IN p_TransactionType ENUM('IN','OUT','TRANSFER'),
    IN p_PartID VARCHAR(300),
    IN p_BatchNumber VARCHAR(100),
    IN p_FromLocation VARCHAR(300),
    IN p_ToLocation VARCHAR(300),
    IN p_Operation VARCHAR(100),
    IN p_Quantity INT,
    IN p_Notes VARCHAR(1000),
    IN p_User VARCHAR(100),
    IN p_ItemType VARCHAR(100),
    IN p_ReceiveDate DATETIME,
    IN p_ColorCode VARCHAR(50),      -- NEW: Color code parameter
    IN p_WorkOrder VARCHAR(10),      -- NEW: Work order parameter
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE v_ColorCode VARCHAR(50);
    DECLARE v_WorkOrder VARCHAR(10);
    
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    
    IF p_TransactionType IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'TransactionType is required';
    ELSEIF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
    ELSEIF p_Quantity IS NULL OR p_Quantity <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Quantity must be greater than zero';
    ELSE
        -- Default ColorCode and WorkOrder to 'Unknown' if not provided
        SET v_ColorCode = COALESCE(p_ColorCode, 'Unknown');
        SET v_WorkOrder = COALESCE(p_WorkOrder, 'Unknown');
        
        INSERT INTO inv_transaction (
            TransactionType, PartID, `BatchNumber`, FromLocation, ToLocation,
            Operation, Quantity, Notes, User, ItemType, ReceiveDate,
            ColorCode, WorkOrder
        ) VALUES (
            p_TransactionType, p_PartID, p_BatchNumber, p_FromLocation, p_ToLocation,
            p_Operation, p_Quantity, p_Notes, p_User, p_ItemType, p_ReceiveDate,
            v_ColorCode, v_WorkOrder
        );
        
        SET v_RowsAffected = ROW_COUNT();
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = 'Transaction added successfully';
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = 'No rows were affected';
        END IF;
    END IF;
END
//
DELIMITER ;

-- Test procedure (requires existing part)
-- CALL inv_transaction_Add('IN', 'TEST-PART', '0000000001', NULL, 'TEST-LOC', 'TEST-OP', 10, 'Test notes', 'TestUser', 'Piece', NOW(), 'Red', 'WO-123456', @status, @msg);
-- SELECT @status AS Status, @msg AS ErrorMessage;
