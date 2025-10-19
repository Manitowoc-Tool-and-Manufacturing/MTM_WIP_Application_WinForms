-- =============================================
-- Procedure: md_item_types_GetDistinct
-- Domain: master-data
-- Created: 2025-10-17
-- Purpose: Returns distinct item types for dropdown population
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `md_item_types_GetDistinct`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_GetDistinct`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get distinct item types
    SELECT DISTINCT ItemType
    FROM md_item_types
    ORDER BY ItemType;
    
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' distinct item type(s)');
        COMMIT;
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No item types found';
        COMMIT;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of md_item_types_GetDistinct
-- =============================================
