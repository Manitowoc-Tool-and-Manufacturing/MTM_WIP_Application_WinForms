-- =============================================
-- Procedure: inv_inventory_Get_ByPartID
-- Domain: inventory
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

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

    

    -- Validate input

    IF p_PartID IS NULL OR p_PartID = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'PartID is required';

    ELSE

        -- Execute query

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

            Notes

        FROM inv_inventory

        WHERE PartID = p_PartID;

        

        -- Check row count

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

-- =============================================
-- End of inv_inventory_Get_ByPartID
-- =============================================
