DELIMITER //
DROP PROCEDURE IF EXISTS `md_item_types_Update_ItemType`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Update_ItemType`(
    IN p_ID INT,
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
    IF p_ID IS NULL OR p_ID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid ID is required';
        ROLLBACK;
    ELSEIF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSE
        UPDATE `md_item_types`
        SET `ItemType` = p_ItemType,
            `IssuedBy` = p_IssuedBy
        WHERE `ID` = p_ID;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Item type ID ', p_ID, ' updated successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Item type ID ', p_ID, ' not found');
            ROLLBACK;
        END IF;
    END IF;
END
//
DELIMITER ;
