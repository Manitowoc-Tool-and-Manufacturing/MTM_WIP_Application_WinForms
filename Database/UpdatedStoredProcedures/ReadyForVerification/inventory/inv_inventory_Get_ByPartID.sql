-- Updated Stored Procedure: inv_inventory_Get_ByPartID
-- Purpose: Retrieve inventory by PartID with color code and work order
-- Feature: Color Code & Work Order Tracking - UPDATED
-- Date: 2025-11-13
-- MySQL Version: 5.7.24
-- BYPASS_MCP_CHECK: TRANSACTION_MANAGEMENT
-- Reason: READ-ONLY query procedure. No transactions needed for SELECT operations.

USE mtm_wip_application_winforms;

DELIMITER //
DROP PROCEDURE IF EXISTS `inv_inventory_Get_ByPartID`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_ByPartID`(
    IN p_PartID VARCHAR(300),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving inventory by PartID';
    END;
    
    IF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
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
            ColorCode,        -- NEW: Color code column
            WorkOrder         -- NEW: Work order column
        FROM inv_inventory
        WHERE PartID = p_PartID
        ORDER BY 
            CASE WHEN ColorCode = 'Unknown' THEN 1 ELSE 0 END,  -- Unknown to end
            ColorCode ASC,                                       -- Alphabetical by color
            Location ASC;                                        -- Then by location
        
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = NULL;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No inventory found for PartID: ', p_PartID);
        END IF;
    END IF;
END
//
DELIMITER ;

-- Test procedure (requires existing part)
-- CALL inv_inventory_Get_ByPartID('TEST-PART', @status, @msg);
-- SELECT @status AS Status, @msg AS ErrorMessage;
