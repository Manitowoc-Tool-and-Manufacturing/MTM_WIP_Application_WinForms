DELIMITER //
DROP PROCEDURE IF EXISTS `md_item_types_Add_ItemType`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Add_ItemType`(
    IN p_ItemType VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    IF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSE
        INSERT INTO `md_item_types` (`ItemType`, `IssuedBy`)
        VALUES (p_ItemType, p_IssuedBy);
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" added successfully');
            COMMIT;
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to add item type';
            ROLLBACK;
        END IF;
    END IF;
END
//
DELIMITER ;
