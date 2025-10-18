-- =============================================
-- Procedure: inv_inventory_Get_ByUser
-- Domain: inventory
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_inventory_Get_ByUser`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_ByUser`(

    IN p_User VARCHAR(100),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_Count INT DEFAULT 0;

    

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        SET p_Status = -1;

        SET p_ErrorMsg = 'Database error occurred while retrieving inventory by User';

    END;

    

    -- Execute query (empty or NULL user returns all records)

    SELECT * FROM inv_inventory

    WHERE (p_User IS NULL OR p_User = '' OR User = p_User)

    ORDER BY LastUpdated DESC;

    

    -- Check row count

    SELECT FOUND_ROWS() INTO v_Count;

    

    IF v_Count > 0 THEN

        SET p_Status = 1;

        SET p_ErrorMsg = NULL;

    ELSE

        SET p_Status = 0;

        SET p_ErrorMsg = 'No inventory records found';

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of inv_inventory_Get_ByUser
-- =============================================
