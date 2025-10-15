DROP PROCEDURE IF EXISTS `md_part_ids_Update_Part`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Update_Part`(
    IN p_ID INT,
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
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate required parameters
    IF p_ID IS NULL OR p_ID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid ID is required';
        ROLLBACK;
    ELSEIF p_ItemNumber IS NULL OR TRIM(p_ItemNumber) = '' THEN
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
        -- Update part
        UPDATE `md_part_ids`
        SET `PartID` = p_ItemNumber,
            `Customer` = p_Customer,
            `Description` = p_Description,
            `IssuedBy` = p_IssuedBy,
            `ItemType` = p_ItemType
        WHERE `ID` = p_ID;
        
        -- Check if update was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Part ID ', p_ID, ' updated successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Part ID ', p_ID, ' not found');
            ROLLBACK;
        END IF;
    END IF;
END$$
DELIMITER ;