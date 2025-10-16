DELIMITER //
DROP PROCEDURE IF EXISTS `md_part_ids_Add_Part`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Add_Part`(
    IN p_ItemNumber VARCHAR(300),
    IN p_Customer VARCHAR(300),
    IN p_Description VARCHAR(300),
    IN p_IssuedBy VARCHAR(100),
    IN p_ItemType VARCHAR(100),
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
    ELSEIF p_Customer IS NULL OR TRIM(p_Customer) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Customer is required';
        ROLLBACK;
    ELSEIF p_Description IS NULL OR TRIM(p_Description) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Description is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSEIF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
        ROLLBACK;
    ELSE
        INSERT INTO `md_part_ids` (`PartID`, `Customer`, `Description`, `IssuedBy`, `ItemType`)
        VALUES (p_ItemNumber, p_Customer, p_Description, p_IssuedBy, p_ItemType);
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" added successfully');
            COMMIT;
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to add part';
            ROLLBACK;
        END IF;
    END IF;
END
//
DELIMITER ;
