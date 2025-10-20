-- =============================================
-- Procedure: inv_inventory_Get_All
-- Domain: inventory
-- Created: 2025-10-17
-- Purpose: Returns all inventory records for display
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_inventory_Get_All`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Get all inventory records
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
        BatchNumber,
        Notes
    FROM inv_inventory
    ORDER BY LastUpdated DESC;
    
    -- Check row count
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' inventory record(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No inventory records found';
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of inv_inventory_Get_All
-- =============================================
