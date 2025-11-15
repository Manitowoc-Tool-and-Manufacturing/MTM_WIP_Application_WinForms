-- BYPASS_MCP_CHECK: TRANSACTION_MANAGEMENT
-- Reason: READ-ONLY query procedure. No transactions needed for SELECT operations.
DELIMITER //
DROP PROCEDURE IF EXISTS `inv_inventory_Get_ByPartIDandOperation`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_ByPartIDandOperation`(
    IN p_PartID VARCHAR(300),
    IN p_Operation VARCHAR(300),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving inventory by PartID and Operation';
    END;
    IF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
    ELSEIF p_Operation IS NULL OR p_Operation = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
    ELSE
        SELECT
            ID,
            PartID,
            Location,
            Operation,
            Quantity,
            ItemType,
            ReceiveDate,
            LastUpdated,
            User,
            BatchNumber AS `BatchNumber`,
            Notes,
            ColorCode,
            WorkOrder
        FROM inv_inventory
        WHERE PartID = p_PartID AND Operation = p_Operation;
        SELECT FOUND_ROWS() INTO v_Count;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = NULL;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No inventory found for PartID: ', p_PartID, ', Operation: ', p_Operation);
        END IF;
    END IF;
END
//
DELIMITER ;
