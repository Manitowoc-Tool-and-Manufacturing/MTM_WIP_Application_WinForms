DELIMITER //
DROP PROCEDURE IF EXISTS `md_part_ids_Delete_ByItemNumber`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Delete_ByItemNumber`(
    IN p_ItemNumber VARCHAR(300),
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
    IF p_ItemNumber IS NULL OR TRIM(p_ItemNumber) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemNumber is required';
        ROLLBACK;
    ELSE
        DELETE FROM `md_part_ids`
        WHERE `PartID` = p_ItemNumber;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END
//
DELIMITER ;
