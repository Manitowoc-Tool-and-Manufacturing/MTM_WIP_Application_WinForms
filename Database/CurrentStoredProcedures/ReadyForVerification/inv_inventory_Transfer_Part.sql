DELIMITER $$

DROP PROCEDURE IF EXISTS `inv_inventory_Transfer_Part`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Transfer_Part`(
    IN p_BatchNumber VARCHAR(300),
    IN p_PartID VARCHAR(300),
    IN p_Operation VARCHAR(100),
    IN p_NewLocation VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE v_Exists INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE, 
            @errno = MYSQL_ERRNO, 
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;

    START TRANSACTION;
    
    -- Validate inputs
    IF p_BatchNumber IS NULL OR p_BatchNumber = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'BatchNumber is required';
        ROLLBACK;
    ELSEIF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
        ROLLBACK;
    ELSEIF p_Operation IS NULL OR p_Operation = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSEIF p_NewLocation IS NULL OR p_NewLocation = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'NewLocation is required';
        ROLLBACK;
    ELSE
        -- Check if record exists
        SELECT COUNT(*) INTO v_Exists
        FROM inv_inventory
        WHERE BatchNumber = p_BatchNumber
          AND PartID = p_PartID
          AND Operation = p_Operation;
        
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Inventory record not found';
            ROLLBACK;
        ELSE
            -- Transfer to new location
            UPDATE inv_inventory
            SET Location = p_NewLocation,
                LastUpdated = CURRENT_TIMESTAMP
            WHERE BatchNumber = p_BatchNumber
              AND PartID = p_PartID
              AND Operation = p_Operation;
            
            SET v_RowsAffected = ROW_COUNT();
            
            IF v_RowsAffected > 0 THEN
                COMMIT;
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Successfully transferred ', v_RowsAffected, ' item(s) to ', p_NewLocation);
            ELSE
                ROLLBACK;
                SET p_Status = 0;
                SET p_ErrorMsg = 'No rows were affected';
            END IF;
        END IF;
    END IF;
END$$

DELIMITER ;