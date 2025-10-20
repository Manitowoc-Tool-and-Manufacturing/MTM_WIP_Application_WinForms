DELIMITER //
DROP PROCEDURE IF EXISTS `md_item_types_Delete_ByType`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Delete_ByType`(
    IN p_ItemType VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
    ELSE
        DELETE FROM `md_item_types`
        WHERE `ItemType` = p_ItemType;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" deleted successfully');
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" not found');
        END IF;
    END IF;
END
//
DELIMITER ;
