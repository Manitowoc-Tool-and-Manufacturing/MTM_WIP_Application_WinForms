-- =============================================
-- Procedure: md_item_types_Exists_ByType
-- Domain: master-data
-- Created: 2025-10-17
-- Purpose: Checks if item type exists in md_item_types table
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `md_item_types_Exists_ByType`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Exists_ByType`(
    IN p_ItemType VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate input
    IF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
        ROLLBACK;
    ELSE
        -- Check if item type exists
        SELECT COUNT(*) INTO v_Exists
        FROM md_item_types
        WHERE ItemType = p_ItemType;
        
        -- Return existence as scalar
        SELECT v_Exists AS Exists;
        
        IF v_Exists > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('ItemType "', p_ItemType, '" exists');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('ItemType "', p_ItemType, '" does not exist');
        END IF;
        COMMIT;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of md_item_types_Exists_ByType
-- =============================================
