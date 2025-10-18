-- =============================================
-- Procedure: inv_transaction_Add
-- Domain: inventory
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_transaction_Add`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transaction_Add`(

    IN p_TransactionType ENUM('IN','OUT','TRANSFER'),

    IN p_PartID VARCHAR(300),

    IN p_BatchNumber VARCHAR(100),

    IN p_FromLocation VARCHAR(300),

    IN p_ToLocation VARCHAR(100),

    IN p_Operation VARCHAR(100),

    IN p_Quantity INT,

    IN p_Notes VARCHAR(1000),

    IN p_User VARCHAR(100),

    IN p_ItemType VARCHAR(100),

    IN p_ReceiveDate DATETIME,

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

    IF p_TransactionType IS NULL THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'TransactionType is required';

        ROLLBACK;

    ELSEIF p_PartID IS NULL OR p_PartID = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'PartID is required';

        ROLLBACK;

    ELSEIF p_Quantity IS NULL OR p_Quantity <= 0 THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Quantity must be greater than zero';

        ROLLBACK;

    ELSE

        -- Insert transaction

        INSERT INTO inv_transaction (

            TransactionType, PartID, `BatchNumber`, FromLocation, ToLocation, 

            Operation, Quantity, Notes, User, ItemType, ReceiveDate

        ) VALUES (

            p_TransactionType, p_PartID, p_BatchNumber, p_FromLocation, p_ToLocation, 

            p_Operation, p_Quantity, p_Notes, p_User, p_ItemType, p_ReceiveDate

        );

        

        SET v_RowsAffected = ROW_COUNT();

        

        IF v_RowsAffected > 0 THEN

            COMMIT;

            SET p_Status = 1;

            SET p_ErrorMsg = 'Transaction added successfully';

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
-- End of inv_transaction_Add
-- =============================================
