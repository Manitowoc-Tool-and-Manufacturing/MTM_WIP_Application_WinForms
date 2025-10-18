-- =============================================
-- Procedure: inv_inventory_Add_Item
-- Domain: inventory
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_inventory_Add_Item`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Add_Item`(

    IN p_PartID VARCHAR(100),

    IN p_Location VARCHAR(100),

    IN p_Operation VARCHAR(100),

    IN p_Quantity INT,

    IN p_ItemType VARCHAR(200),

    IN p_User VARCHAR(100),

    IN p_Notes VARCHAR(1000),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_RowsAffected INT DEFAULT 0;

    DECLARE nextBatch BIGINT;

    DECLARE batchStr VARCHAR(10);

    

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

    IF p_PartID IS NULL OR p_PartID = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'PartID is required';

        ROLLBACK;

    ELSEIF p_Location IS NULL OR p_Location = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Location is required';

        ROLLBACK;

    ELSEIF p_Operation IS NULL OR p_Operation = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Operation is required';

        ROLLBACK;

    ELSEIF p_Quantity IS NULL OR p_Quantity <= 0 THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Quantity must be greater than 0';

        ROLLBACK;

    ELSEIF p_User IS NULL OR p_User = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'User is required';

        ROLLBACK;

    ELSE

        -- Get next batch number

        SELECT last_batch_number INTO nextBatch FROM inv_inventory_batch_seq FOR UPDATE;

        SET nextBatch = nextBatch + 1;

        SET batchStr = LPAD(nextBatch, 10, '0');

        

        -- Update batch sequence

        UPDATE inv_inventory_batch_seq SET last_batch_number = nextBatch;

        

        -- Insert into inventory

        INSERT INTO inv_inventory

            (PartID, Location, Operation, Quantity, ItemType, User, BatchNumber, Notes)

        VALUES

            (p_PartID, p_Location, p_Operation, p_Quantity, p_ItemType, p_User, batchStr, p_Notes);

        

        SET v_RowsAffected = ROW_COUNT();

        

        -- Insert into transaction log

        INSERT INTO inv_transaction

            (

                TransactionType, 

                BatchNumber, 

                PartID, 

                FromLocation, 

                ToLocation, 

                Operation, 

                Quantity, 

                Notes, 

                User, 

                ItemType

            )

        VALUES

            (

                'IN', 

                batchStr, 

                p_PartID, 

                p_Location, 

                NULL, 

                p_Operation, 

                p_Quantity, 

                p_Notes, 

                p_User, 

                p_ItemType

            );

        

        IF v_RowsAffected > 0 THEN

            COMMIT;

            SET p_Status = 1;

            SET p_ErrorMsg = CONCAT('Successfully added inventory item with batch number ', batchStr);

        ELSE

            ROLLBACK;

            SET p_Status = 0;

            SET p_ErrorMsg = 'No rows were affected';

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of inv_inventory_Add_Item
-- =============================================
