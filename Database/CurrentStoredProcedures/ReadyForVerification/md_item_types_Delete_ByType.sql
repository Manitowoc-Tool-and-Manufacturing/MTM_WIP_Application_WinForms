DROP PROCEDURE IF EXISTS `md_item_types_Delete_ByType`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Delete_ByType`(
    IN p_ItemType VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate required parameter
    IF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
        ROLLBACK;
    ELSE
        -- Delete item type
        DELETE FROM `md_item_types`
        WHERE `ItemType` = p_ItemType;
        
        -- Check if delete was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$
DELIMITER ;