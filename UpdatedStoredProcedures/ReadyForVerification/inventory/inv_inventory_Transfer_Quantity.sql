-- =============================================
-- Procedure: inv_inventory_Transfer_Quantity
-- Domain: inventory
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_inventory_Transfer_Quantity`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Transfer_Quantity`(

    IN p_BatchNumber VARCHAR(255),

    IN p_PartID VARCHAR(255),

    IN p_Operation VARCHAR(255),

    IN p_TransferQuantity INT,

    IN p_OriginalQuantity INT,

    IN p_NewLocation VARCHAR(255),

    IN p_User VARCHAR(255),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_RowsAffected INT DEFAULT 0;

    

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

    IF p_TransferQuantity IS NULL OR p_TransferQuantity <= 0 THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Transfer quantity must be greater than zero';

        ROLLBACK;

    ELSEIF p_TransferQuantity > p_OriginalQuantity THEN

        SET p_Status = -2;

        SET p_ErrorMsg = CONCAT('Transfer quantity (', p_TransferQuantity, ') cannot exceed original quantity (', p_OriginalQuantity, ')');

        ROLLBACK;

    ELSEIF p_NewLocation IS NULL OR p_NewLocation = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'New location is required';

        ROLLBACK;

    ELSE

        -- Deduct from original location

        UPDATE inv_inventory

        SET Quantity = Quantity - p_TransferQuantity,

            User = p_User

        WHERE BatchNumber = p_BatchNumber

          AND PartID = p_PartID

          AND Operation = p_Operation

          AND Quantity = p_OriginalQuantity;

        

        SET v_RowsAffected = ROW_COUNT();

        

        IF v_RowsAffected = 0 THEN

            SET p_Status = -4;

            SET p_ErrorMsg = 'Original inventory record not found or quantity mismatch';

            ROLLBACK;

        ELSE

            -- Add to new location

            INSERT INTO inv_inventory (BatchNumber, PartID, Operation, Quantity, Location, User)

            VALUES (p_BatchNumber, p_PartID, p_Operation, p_TransferQuantity, p_NewLocation, p_User);

            

            COMMIT;

            SET p_Status = 1;

            SET p_ErrorMsg = CONCAT('Successfully transferred quantity ', p_TransferQuantity, ' to ', p_NewLocation);

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of inv_inventory_Transfer_Quantity
-- =============================================
