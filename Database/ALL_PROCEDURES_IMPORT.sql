-- ========================================
-- MTM WIP Application - Stored Procedures
-- Consolidated Import File
-- Generated: 2025-10-14 20:50:39
-- Total Procedures: 73
-- ========================================

DELIMITER $$


-- ========================================
-- Procedure: inv_inventory_Add_Item
-- ========================================

DROP PROCEDURE IF EXISTS `inv_inventory_Add_Item`$$

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
END$$


-- ========================================
-- Procedure: inv_inventory_Fix_BatchNumbers
-- ========================================

DROP PROCEDURE IF EXISTS `inv_inventory_Fix_BatchNumbers`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Fix_BatchNumbers`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE done INT DEFAULT FALSE;
    DECLARE null_id INT;
    DECLARE nextBatch BIGINT;
    
    DECLARE cur CURSOR FOR SELECT ID FROM inv_inventory WHERE BatchNumber IS NULL ORDER BY ID;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
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
    
    -- Get current batch number
    SELECT last_batch_number INTO nextBatch FROM inv_inventory_batch_seq;
    
    -- Process each null batch number
    OPEN cur;
    read_loop: LOOP
        FETCH cur INTO null_id;
        IF done THEN
            LEAVE read_loop;
        END IF;
        
        SET nextBatch = nextBatch + 1;
        UPDATE inv_inventory SET BatchNumber = LPAD(nextBatch, 10, '0') WHERE ID = null_id;
        SET v_RowsAffected = v_RowsAffected + ROW_COUNT();
        
        -- Update sequence table
        UPDATE inv_inventory_batch_seq SET last_batch_number = nextBatch;
    END LOOP;
    CLOSE cur;
    
    COMMIT;
    
    IF v_RowsAffected > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Successfully fixed ', v_RowsAffected, ' batch number(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No batch numbers needed fixing';
    END IF;
END$$


-- ========================================
-- Procedure: inv_inventory_Get_ByPartID
-- ========================================

DROP PROCEDURE IF EXISTS `inv_inventory_Get_ByPartID`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_ByPartID`(
    IN p_PartID VARCHAR(300),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving inventory by PartID';
    END;
    
    -- Validate input
    IF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
    ELSE
        -- Execute query
        SELECT 
            ID,
            PartID,
            Location,
            Operation,
            Quantity,
            ItemType,
            ReceiveDate,
            LastUpdated,
            User,
            BatchNumber AS `BatchNumber`,
            Notes
        FROM inv_inventory
        WHERE PartID = p_PartID;
        
        -- Check row count
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = NULL;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No inventory found for PartID: ', p_PartID);
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: inv_inventory_Get_ByPartIDandOperation
-- ========================================

DROP PROCEDURE IF EXISTS `inv_inventory_Get_ByPartIDandOperation`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_ByPartIDandOperation`(
    IN p_PartID VARCHAR(300),
    IN p_Operation VARCHAR(300),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving inventory by PartID and Operation';
    END;
    
    -- Validate inputs
    IF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
    ELSEIF p_Operation IS NULL OR p_Operation = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
    ELSE
        -- Execute query
        SELECT 
            ID,
            PartID,
            Location,
            Operation,
            Quantity,
            ItemType,
            ReceiveDate,
            LastUpdated,
            User,
            BatchNumber AS `BatchNumber`,
            Notes
        FROM inv_inventory
        WHERE PartID = p_PartID AND Operation = p_Operation;
        
        -- Check row count
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = NULL;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No inventory found for PartID: ', p_PartID, ', Operation: ', p_Operation);
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: inv_inventory_Get_ByUser
-- ========================================

DROP PROCEDURE IF EXISTS `inv_inventory_Get_ByUser`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_ByUser`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving inventory by User';
    END;
    
    -- Execute query (empty or NULL user returns all records)
    SELECT * FROM inv_inventory
    WHERE (p_User IS NULL OR p_User = '' OR User = p_User)
    ORDER BY LastUpdated DESC;
    
    -- Check row count
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No inventory records found';
    END IF;
END$$


-- ========================================
-- Procedure: inv_inventory_Remove_Item
-- ========================================

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Remove_Item`(

    IN p_PartID VARCHAR(300),

    IN p_Location VARCHAR(100),

    IN p_Operation VARCHAR(100),

    IN p_Quantity INT,

    IN p_ItemType VARCHAR(100),

    IN p_User VARCHAR(100),

    IN p_BatchNumber VARCHAR(100),

    IN p_Notes VARCHAR(1000),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(255)

)
BEGIN

    DECLARE v_RowsAffected INT DEFAULT 0;

    DECLARE v_ErrorMessage TEXT DEFAULT '';

    DECLARE v_RecordCount INT DEFAULT 0;

    

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1 v_ErrorMessage = MESSAGE_TEXT;

        SET p_Status = -1;

        SET p_ErrorMsg = CONCAT('Database error occurred while removing inventory item for part: ', p_PartID);

        ROLLBACK;

    END;

    

    START TRANSACTION;

    

    IF p_Quantity <= 0 THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Quantity must be greater than zero';

        ROLLBACK;

    ELSE

        SELECT COUNT(*) INTO v_RecordCount FROM inv_inventory WHERE PartID = p_PartID AND Location = p_Location AND Operation = p_Operation;

          

        IF v_RecordCount = 0 THEN

            SET p_Status = -4;

            SET p_ErrorMsg = CONCAT('No inventory records found for PartID: ', p_PartID, ', Location: ', p_Location, ', Operation: ', p_Operation);

        ELSE

            DELETE FROM inv_inventory WHERE PartID = p_PartID AND Location = p_Location AND Operation = p_Operation AND Quantity = p_Quantity

                AND (p_BatchNumber IS NULL OR p_BatchNumber = '' OR BatchNumber = p_BatchNumber)

                AND (p_Notes IS NULL OR p_Notes = '' OR Notes IS NULL OR Notes = '' OR Notes = p_Notes) LIMIT 1;

            

            SET v_RowsAffected = ROW_COUNT();

            

            IF v_RowsAffected = 0 THEN

                DELETE FROM inv_inventory WHERE PartID = p_PartID AND Location = p_Location AND Operation = p_Operation AND Quantity = p_Quantity LIMIT 1;

                SET v_RowsAffected = ROW_COUNT();

            END IF;

            

            IF v_RowsAffected > 0 THEN

                INSERT INTO inv_transaction (TransactionType, PartID, FromLocation, Operation, Quantity, ItemType, User, BatchNumber, Notes, ReceiveDate)

                VALUES ('OUT', p_PartID, p_Location, p_Operation, p_Quantity, p_ItemType, p_User, p_BatchNumber, p_Notes, NOW());

                

                SET p_Status = 1;

                SET p_ErrorMsg = CONCAT('Inventory item removed successfully for part: ', p_PartID, ', quantity: ', p_Quantity);

            ELSE

                SET p_Status = -4;

                SET p_ErrorMsg = CONCAT('No matching inventory item found for removal. Found ', v_RecordCount, ' records for PartID: ', p_PartID, ', Location: ', p_Location, ', Operation: ', p_Operation, ' but none matched Quantity: ', p_Quantity);

            END IF;

        END IF;

        COMMIT;

    END IF;

END$$


-- ========================================
-- Procedure: inv_inventory_Transfer_Part
-- ========================================

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


-- ========================================
-- Procedure: inv_inventory_Transfer_Quantity
-- ========================================

DROP PROCEDURE IF EXISTS `inv_inventory_Transfer_Quantity`$$

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
END$$


-- ========================================
-- Procedure: inv_transaction_Add
-- ========================================

DROP PROCEDURE IF EXISTS `inv_transaction_Add`$$

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
END$$


-- ========================================
-- Procedure: inv_transactions_GetAnalytics
-- ========================================

DROP PROCEDURE IF EXISTS `inv_transactions_GetAnalytics`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transactions_GetAnalytics`(
    IN p_UserName VARCHAR(100),
    IN p_IsAdmin BOOLEAN,
    IN p_FromDate DATETIME,
    IN p_ToDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE v_ErrorMessage VARCHAR(500) DEFAULT '';
    DECLARE v_WhereClause TEXT DEFAULT '';
    
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
    
    -- Build WHERE clause
    SET v_WhereClause = 'WHERE 1=1 ';
    
    IF NOT p_IsAdmin AND p_UserName IS NOT NULL AND LENGTH(TRIM(p_UserName)) > 0 THEN
        SET v_WhereClause = CONCAT(v_WhereClause, 'AND User = ''', REPLACE(p_UserName, '''', ''''''), ''' ');
    END IF;
    
    IF p_FromDate IS NOT NULL THEN
        SET v_WhereClause = CONCAT(v_WhereClause, 'AND ReceiveDate >= ''', p_FromDate, ''' ');
    END IF;
    
    IF p_ToDate IS NOT NULL THEN
        SET v_WhereClause = CONCAT(v_WhereClause, 'AND ReceiveDate <= ''', p_ToDate, ''' ');
    END IF;
    
    -- Execute analytics query
    SET @sql = CONCAT(
        'SELECT ',
        '    COUNT(*) as TotalTransactions, ',
        '    SUM(CASE WHEN TransactionType = ''IN'' THEN 1 ELSE 0 END) as InTransactions, ',
        '    SUM(CASE WHEN TransactionType = ''OUT'' THEN 1 ELSE 0 END) as OutTransactions, ',
        '    SUM(CASE WHEN TransactionType = ''TRANSFER'' THEN 1 ELSE 0 END) as TransferTransactions, ',
        '    COALESCE(SUM(Quantity), 0) as TotalQuantity, ',
        '    COUNT(DISTINCT PartID) as UniquePartIds, ',
        '    COUNT(DISTINCT User) as ActiveUsers, ',
        '    COALESCE((SELECT PartID FROM inv_transaction t2 ', v_WhereClause, ' GROUP BY PartID ORDER BY SUM(Quantity) DESC LIMIT 1), '''') as TopPartId, ',
        '    COALESCE((SELECT User FROM inv_transaction t3 ', v_WhereClause, ' GROUP BY User ORDER BY COUNT(*) DESC LIMIT 1), '''') as TopUser ',
        'FROM inv_transaction t1 ',
        v_WhereClause
    );
    
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
    
    SELECT FOUND_ROWS() INTO v_Count;
    
    COMMIT;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No transaction data found for analytics';
    END IF;
END$$


-- ========================================
-- Procedure: inv_transactions_SmartSearch
-- ========================================

DROP PROCEDURE IF EXISTS `inv_transactions_SmartSearch`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transactions_SmartSearch`(
    IN p_WhereClause TEXT,
    IN p_Page INT,
    IN p_PageSize INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE v_ErrorMessage VARCHAR(500) DEFAULT '';
    DECLARE v_Offset INT DEFAULT 0;
    DECLARE v_FinalWhereClause TEXT DEFAULT '';
    
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
    
    -- Parameter validation
    IF p_Page < 1 THEN SET p_Page = 1; END IF;
    IF p_PageSize < 1 OR p_PageSize > 1000 THEN SET p_PageSize = 20; END IF;
    SET v_Offset = (p_Page - 1) * p_PageSize;
    
    -- Build final WHERE clause
    IF p_WhereClause IS NOT NULL AND LENGTH(TRIM(p_WhereClause)) > 0 THEN
        SET v_FinalWhereClause = CONCAT('WHERE ', TRIM(p_WhereClause));
    ELSE
        SET v_FinalWhereClause = 'WHERE 1=1';
    END IF;
    
    -- Build and execute dynamic query
    SET @sql = CONCAT(
        'SELECT ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, ',
        'Operation, Quantity, Notes, User, ItemType, ReceiveDate ',
        'FROM inv_transaction ',
        v_FinalWhereClause, ' ',
        'ORDER BY ReceiveDate DESC ',
        'LIMIT ', v_Offset, ', ', p_PageSize
    );
    
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
    
    SELECT FOUND_ROWS() INTO v_Count;
    
    COMMIT;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No transactions found matching search criteria';
    END IF;
END$$


-- ========================================
-- Procedure: log_error_Add_Error
-- ========================================

DROP PROCEDURE IF EXISTS `log_error_Add_Error`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Add_Error`(
    IN p_User VARCHAR(100),
    IN p_Severity VARCHAR(50),
    IN p_ErrorType VARCHAR(100),
    IN p_ErrorMessage TEXT,
    IN p_StackTrace TEXT,
    IN p_ModuleName VARCHAR(100),
    IN p_MethodName VARCHAR(100),
    IN p_AdditionalInfo TEXT,
    IN p_MachineName VARCHAR(100),
    IN p_OSVersion VARCHAR(100),
    IN p_AppVersion VARCHAR(50),
    IN p_ErrorTime DATETIME,
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
    
    -- Apply defaults for optional fields
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_User = 'Unknown';
    END IF;
    
    IF p_Severity IS NULL OR TRIM(p_Severity) = '' THEN
        SET p_Severity = 'Error';
    END IF;
    
    IF p_ErrorTime IS NULL THEN
        SET p_ErrorTime = NOW();
    END IF;
    
    -- Validate required fields
    IF p_ErrorMessage IS NULL OR TRIM(p_ErrorMessage) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Error message is required';
        ROLLBACK;
    ELSE
        -- Insert error log entry
        INSERT INTO `log_error` (
            `User`, `Severity`, `ErrorType`, `ErrorMessage`, `StackTrace`, 
            `ModuleName`, `MethodName`, `AdditionalInfo`, `MachineName`, 
            `OSVersion`, `AppVersion`, `ErrorTime`
        ) VALUES (
            p_User, p_Severity, p_ErrorType, p_ErrorMessage, p_StackTrace,
            p_ModuleName, p_MethodName, p_AdditionalInfo, p_MachineName,
            p_OSVersion, p_AppVersion, p_ErrorTime
        );
        
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Error logged successfully for user: ', p_User);
            COMMIT;
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to create error log entry';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: log_error_Delete_All
-- ========================================

DROP PROCEDURE IF EXISTS `log_error_Delete_All`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Delete_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Check if any entries exist
    SELECT COUNT(*) INTO v_Count FROM `log_error`;
    
    IF v_Count = 0 THEN
        SET p_Status = 0;
        SET p_ErrorMsg = 'No error log entries found to delete';
        COMMIT;
    ELSE
        -- Delete all error log entries
        DELETE FROM `log_error`;
        
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Successfully deleted ', v_RowCount, ' error log entry(ies)');
            COMMIT;
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to delete error log entries';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: log_error_Delete_ById
-- ========================================

DROP PROCEDURE IF EXISTS `log_error_Delete_ById`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Delete_ById`(
    IN p_Id INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate input
    IF p_Id IS NULL OR p_Id <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid error log ID is required';
        ROLLBACK;
    ELSE
        -- Check if error log entry exists
        SELECT COUNT(*) INTO v_Exists FROM `log_error` WHERE `ID` = p_Id;
        
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Error log entry with ID ', p_Id, ' not found');
            ROLLBACK;
        ELSE
            -- Delete error log entry
            DELETE FROM `log_error` WHERE `ID` = p_Id;
            
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Error log entry with ID ', p_Id, ' deleted successfully');
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to delete error log entry';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: log_error_Get_All
-- ========================================

DROP PROCEDURE IF EXISTS `log_error_Get_All`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;

    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Get all error logs
    SELECT 
        ID,
        User,
        ErrorMessage,
        StackTrace,
        MethodName,
        ErrorType,
        LoggedDate
    FROM log_error 
    ORDER BY LoggedDate DESC;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;

    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' error log entries');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No error log entries found';
    END IF;

    COMMIT;
END$$


-- ========================================
-- Procedure: log_error_Get_ByDateRange
-- ========================================

DROP PROCEDURE IF EXISTS `log_error_Get_ByDateRange`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Get_ByDateRange`(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;

    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Validate parameters
    IF p_StartDate IS NULL OR p_EndDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Start date and end date are required';
        ROLLBACK;
    ELSEIF p_StartDate > p_EndDate THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Start date must be less than or equal to end date';
        ROLLBACK;
    ELSE
        -- Get error logs by date range
        SELECT * FROM `log_error` 
        WHERE `ErrorTime` BETWEEN p_StartDate AND p_EndDate 
        ORDER BY `ErrorTime` DESC;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;

        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' error log entries between ', 
                                   DATE_FORMAT(p_StartDate, '%Y-%m-%d %H:%i:%s'), ' and ', 
                                   DATE_FORMAT(p_EndDate, '%Y-%m-%d %H:%i:%s'));
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('No error log entries found between ', 
                                   DATE_FORMAT(p_StartDate, '%Y-%m-%d %H:%i:%s'), ' and ', 
                                   DATE_FORMAT(p_EndDate, '%Y-%m-%d %H:%i:%s'));
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: log_error_Get_ByUser
-- ========================================

DROP PROCEDURE IF EXISTS `log_error_Get_ByUser`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Get_ByUser`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;

    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Validate parameter
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        -- Get error logs by user
        SELECT * FROM `log_error` 
        WHERE `User` = p_User 
        ORDER BY `ErrorTime` DESC;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;

        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' error log entries for user: ', p_User);
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('No error log entries found for user: ', p_User);
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: log_error_Get_Unique
-- ========================================

DROP PROCEDURE IF EXISTS `log_error_Get_Unique`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Get_Unique`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;

    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Get unique error combinations
    SELECT DISTINCT `MethodName`, `ErrorMessage` 
    FROM `log_error` 
    WHERE `MethodName` IS NOT NULL AND `ErrorMessage` IS NOT NULL
    ORDER BY `MethodName`, `ErrorMessage`;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;

    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' unique error combinations');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No unique error combinations found';
    END IF;

    COMMIT;
END$$


-- ========================================
-- Procedure: maint_InsertMissingUserUiSettings
-- ========================================

DROP PROCEDURE IF EXISTS `maint_InsertMissingUserUiSettings`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `maint_InsertMissingUserUiSettings`(
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
    
    -- Insert missing UI settings for users
    INSERT INTO `mtm_wip_application`.usr_ui_settings (UserId, SettingsJson, ShortcutsJson, UpdatedAt)
    SELECT u.User, '{"Theme_Name": "Default"}', '{}', NOW()
    FROM `mtm_wip_application`.usr_users u
    LEFT JOIN `mtm_wip_application`.usr_ui_settings s ON s.UserId = u.User
    WHERE s.UserId IS NULL;
    
    SET v_RowCount = ROW_COUNT();
    
    IF v_RowCount > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Inserted ', v_RowCount, ' missing user UI settings');
        COMMIT;
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No missing user UI settings found';
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: maint_reload_part_ids_and_operation_numbers
-- ========================================

DROP PROCEDURE IF EXISTS `maint_reload_part_ids_and_operation_numbers`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `maint_reload_part_ids_and_operation_numbers`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Truncate and reload part IDs
    TRUNCATE TABLE mtm_wip_application.md_part_ids;
    
    INSERT INTO mtm_wip_application.md_part_ids (
        `PartID`,
        `Operations`,
        `Customer`,
        `Description`,
        `IssuedBy`,
        `ItemType`
    )
    SELECT
        seqs.ID AS `PartID`,
        CAST(seqs.sequence_array AS JSON) AS `Operations`,
        '' AS `Customer`,
        descs.Description AS `Description`,
        '[ System ]' AS `IssuedBy`,
        'WIP' AS `ItemType`
    FROM (
        SELECT
            ID,
            CONCAT('[', GROUP_CONCAT(DISTINCT SEQUENCE_NO ORDER BY SEQUENCE_NO), ']') AS sequence_array
        FROM
            `mtm database`.part_requirement
        GROUP BY
            ID
    ) seqs
    LEFT JOIN (
        SELECT
            ID,
            MIN(Description) AS Description
        FROM
            `mtm database`.part_requirement
        WHERE
            Description IS NOT NULL AND Description <> ''
        GROUP BY
            ID
    ) descs
    ON seqs.ID = descs.ID;
    
    COMMIT;
    
    -- Reload operation numbers
    START TRANSACTION;
    
    INSERT IGNORE INTO mtm_wip_application.md_operation_numbers (`Operation`)
    SELECT DISTINCT op_num
    FROM (
        SELECT
            TRIM(BOTH '"' FROM
                JSON_UNQUOTE(
                    JSON_EXTRACT(mpi.Operations, CONCAT('$[', n.n, ']'))
                )
            ) AS op_num
        FROM
            mtm_wip_application.md_part_ids mpi
        JOIN (
            SELECT 0 AS n UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL
            SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL
            SELECT 8 UNION ALL SELECT 9 UNION ALL SELECT 10 UNION ALL SELECT 11 UNION ALL
            SELECT 12 UNION ALL SELECT 13 UNION ALL SELECT 14 UNION ALL SELECT 15
        ) n
        ON n.n < JSON_LENGTH(mpi.Operations)
        WHERE mpi.Operations IS NOT NULL
    ) AS all_ops
    WHERE op_num IS NOT NULL AND op_num <> '';
    
    COMMIT;
    
    SET p_Status = 1;
    SET p_ErrorMsg = 'Part IDs and operation numbers reloaded successfully';
END$$


-- ========================================
-- Procedure: md_item_types_Add_ItemType
-- ========================================

DROP PROCEDURE IF EXISTS `md_item_types_Add_ItemType`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Add_ItemType`(
    IN p_ItemType VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
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
    IF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSE
        -- Insert new item type
        INSERT INTO `md_item_types` (`ItemType`, `IssuedBy`)
        VALUES (p_ItemType, p_IssuedBy);
        
        -- Check if insert was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" added successfully');
            COMMIT;
        ELSE
            SET p_Status = -3;  -- Business logic error
            SET p_ErrorMsg = 'Failed to add item type';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_item_types_Delete_ByID
-- ========================================

DROP PROCEDURE IF EXISTS `md_item_types_Delete_ByID`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Delete_ByID`(
    IN p_ID INT,
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
    
    -- Validate required parameter
    IF p_ID IS NULL OR p_ID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid ID is required';
        ROLLBACK;
    ELSE
        -- Delete item type
        DELETE FROM `md_item_types`
        WHERE `ID` = p_ID;
        
        -- Check if delete was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Item type ID ', p_ID, ' deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Item type ID ', p_ID, ' not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_item_types_Delete_ByType
-- ========================================

DROP PROCEDURE IF EXISTS `md_item_types_Delete_ByType`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Delete_ByType`(
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
    
    -- Validate required parameter
    IF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
        ROLLBACK;
    ELSE
        -- Delete item type
        DELETE FROM `md_item_types`
        WHERE `ItemType` = p_ItemType;
        
        -- Check if delete was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_item_types_Get_All
-- ========================================

DROP PROCEDURE IF EXISTS `md_item_types_Get_All`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get all item types
    SELECT * FROM `md_item_types`
    ORDER BY `ItemType`;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' item type(s)');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No item types found';
    END IF;
    
    COMMIT;
END$$


-- ========================================
-- Procedure: md_item_types_Update_ItemType
-- ========================================

DROP PROCEDURE IF EXISTS `md_item_types_Update_ItemType`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Update_ItemType`(
    IN p_ID INT,
    IN p_ItemType VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
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
    ELSEIF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSE
        -- Update item type
        UPDATE `md_item_types`
        SET `ItemType` = p_ItemType,
            `IssuedBy` = p_IssuedBy
        WHERE `ID` = p_ID;
        
        -- Check if update was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Item type ID ', p_ID, ' updated successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Item type ID ', p_ID, ' not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_locations_Add_Location
-- ========================================

DROP PROCEDURE IF EXISTS `md_locations_Add_Location`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Add_Location`(
    IN p_Location VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
    IN p_Building VARCHAR(100),
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
    IF p_Location IS NULL OR TRIM(p_Location) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Location is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSEIF p_Building IS NULL OR TRIM(p_Building) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Building is required';
        ROLLBACK;
    ELSE
        -- Insert new location
        INSERT INTO `md_locations` (`Location`, `Building`, `IssuedBy`)
        VALUES (p_Location, p_Building, p_IssuedBy);
        
        -- Check if insert was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Location "', p_Location, '" added successfully');
            COMMIT;
        ELSE
            SET p_Status = -3;  -- Business logic error
            SET p_ErrorMsg = 'Failed to add location';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_locations_Delete_ByLocation
-- ========================================

DROP PROCEDURE IF EXISTS `md_locations_Delete_ByLocation`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Delete_ByLocation`(
    IN p_Location VARCHAR(100),
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
    
    -- Validate required parameter
    IF p_Location IS NULL OR TRIM(p_Location) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Location is required';
        ROLLBACK;
    ELSE
        -- Delete location
        DELETE FROM `md_locations`
        WHERE `Location` = p_Location;
        
        -- Check if delete was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Location "', p_Location, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Location "', p_Location, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_locations_Get_All
-- ========================================

DROP PROCEDURE IF EXISTS `md_locations_Get_All`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get all locations
    SELECT * FROM `md_locations`
    ORDER BY `Location`;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' location(s)');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No locations found';
    END IF;
    
    COMMIT;
END$$


-- ========================================
-- Procedure: md_locations_Update_Location
-- ========================================

DROP PROCEDURE IF EXISTS `md_locations_Update_Location`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Update_Location`(
    IN p_OldLocation VARCHAR(100),
    IN p_Location VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
    IN p_Building VARCHAR(100),
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
    IF p_OldLocation IS NULL OR TRIM(p_OldLocation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'OldLocation is required';
        ROLLBACK;
    ELSEIF p_Location IS NULL OR TRIM(p_Location) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Location is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSEIF p_Building IS NULL OR TRIM(p_Building) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Building is required';
        ROLLBACK;
    ELSE
        -- Update location
        UPDATE `md_locations`
        SET `Location` = p_Location,
            `Building` = p_Building,
            `IssuedBy` = p_IssuedBy
        WHERE `Location` = p_OldLocation;
        
        -- Check if update was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Location "', p_OldLocation, '" updated successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Location "', p_OldLocation, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_operation_numbers_Add_Operation
-- ========================================

DROP PROCEDURE IF EXISTS `md_operation_numbers_Add_Operation`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Add_Operation`(
    IN p_Operation VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
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
    IF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSE
        -- Insert new operation
        INSERT INTO `md_operation_numbers` (`Operation`, `IssuedBy`)
        VALUES (p_Operation, p_IssuedBy);
        
        -- Check if insert was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" added successfully');
            COMMIT;
        ELSE
            SET p_Status = -3;  -- Business logic error
            SET p_ErrorMsg = 'Failed to add operation';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_operation_numbers_Delete_ByOperation
-- ========================================

DROP PROCEDURE IF EXISTS `md_operation_numbers_Delete_ByOperation`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Delete_ByOperation`(
    IN p_Operation VARCHAR(100),
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
    
    -- Validate required parameter
    IF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSE
        -- Delete operation
        DELETE FROM `md_operation_numbers`
        WHERE `Operation` = p_Operation;
        
        -- Check if delete was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_operation_numbers_Get_All
-- ========================================

DROP PROCEDURE IF EXISTS `md_operation_numbers_Get_All`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get all operation numbers
    SELECT * FROM `md_operation_numbers`
    ORDER BY `Operation`;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' operation(s)');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No operations found';
    END IF;
    
    COMMIT;
END$$


-- ========================================
-- Procedure: md_operation_numbers_Update_Operation
-- ========================================

DROP PROCEDURE IF EXISTS `md_operation_numbers_Update_Operation`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Update_Operation`(
    IN p_Operation VARCHAR(100),
    IN p_NewOperation VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
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
    IF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSEIF p_NewOperation IS NULL OR TRIM(p_NewOperation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'NewOperation is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSE
        -- Update operation
        UPDATE `md_operation_numbers`
        SET `Operation` = p_NewOperation,
            `IssuedBy` = p_IssuedBy
        WHERE `Operation` = p_Operation;
        
        -- Check if update was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" updated successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_part_ids_Add_Part
-- ========================================

DROP PROCEDURE IF EXISTS `md_part_ids_Add_Part`;

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
        -- Insert new part
        INSERT INTO `md_part_ids` (`PartID`, `Customer`, `Description`, `IssuedBy`, `ItemType`)
        VALUES (p_ItemNumber, p_Customer, p_Description, p_IssuedBy, p_ItemType);
        
        -- Check if insert was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" added successfully');
            COMMIT;
        ELSE
            SET p_Status = -3;  -- Business logic error
            SET p_ErrorMsg = 'Failed to add part';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_part_ids_Delete_ByItemNumber
-- ========================================

DROP PROCEDURE IF EXISTS `md_part_ids_Delete_ByItemNumber`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Delete_ByItemNumber`(
    IN p_ItemNumber VARCHAR(300),
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
    
    -- Validate required parameter
    IF p_ItemNumber IS NULL OR TRIM(p_ItemNumber) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemNumber is required';
        ROLLBACK;
    ELSE
        -- Delete part
        DELETE FROM `md_part_ids`
        WHERE `PartID` = p_ItemNumber;
        
        -- Check if delete was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_part_ids_Get_All
-- ========================================

DROP PROCEDURE IF EXISTS `md_part_ids_Get_All`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get all parts
    SELECT * FROM `md_part_ids`
    ORDER BY `PartID`;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' part(s)');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No parts found';
    END IF;
    
    COMMIT;
END$$


-- ========================================
-- Procedure: md_part_ids_Get_ByItemNumber
-- ========================================

DROP PROCEDURE IF EXISTS `md_part_ids_Get_ByItemNumber`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Get_ByItemNumber`(
    IN p_ItemNumber VARCHAR(300),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate required parameter
    IF p_ItemNumber IS NULL OR TRIM(p_ItemNumber) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemNumber is required';
        ROLLBACK;
    ELSE
        -- Get part by item number
        SELECT * FROM `md_part_ids`
        WHERE `PartID` = p_ItemNumber;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved part "', p_ItemNumber, '"');
            COMMIT;
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('Part "', p_ItemNumber, '" not found');
            COMMIT;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: md_part_ids_Update_Part
-- ========================================

DROP PROCEDURE IF EXISTS `md_part_ids_Update_Part`;

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


-- ========================================
-- Procedure: migrate_user_roles_debug
-- ========================================

DROP PROCEDURE IF EXISTS `migrate_user_roles_debug`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `migrate_user_roles_debug`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE done INT DEFAULT FALSE;
    DECLARE v_old_username VARCHAR(100);
    DECLARE v_new_userid INT;
    DECLARE v_roleid INT;
    DECLARE v_rolename VARCHAR(50);
    DECLARE v_existing_roleid INT;
    DECLARE v_ProcessedCount INT DEFAULT 0;
    DECLARE v_ErrorCount INT DEFAULT 0;
    
    DECLARE user_cur CURSOR FOR
        SELECT User FROM `mtm database`.users;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    OPEN user_cur;
    
    read_loop: LOOP
        FETCH user_cur INTO v_old_username;
        IF done THEN
            LEAVE read_loop;
        END IF;
        
        INSERT INTO migration_debug_log (message) VALUES (CONCAT('Processing user: ', v_old_username));
        
        SET v_new_userid = NULL;
        SELECT ID INTO v_new_userid
        FROM mtm_wip_application.usr_users
        WHERE User = v_old_username
        LIMIT 1;
        
        IF v_new_userid IS NULL THEN
            INSERT INTO migration_debug_log (message) VALUES (CONCAT('User NOT found in new system: ', v_old_username));
            SET v_ErrorCount = v_ErrorCount + 1;
            ITERATE read_loop;
        END IF;
        
        -- Determine role based on old system
        IF EXISTS (SELECT 1 FROM `mtm database`.leads WHERE USER = v_old_username) THEN
            SET v_rolename = 'Admin';
        ELSEIF EXISTS (SELECT 1 FROM `mtm database`.readonly WHERE USER = v_old_username) THEN
            SET v_rolename = 'ReadOnly';
        ELSE
            SET v_rolename = 'User';
        END IF;
        
        INSERT INTO migration_debug_log (message) VALUES (CONCAT('Assigned role: ', v_rolename));
        
        -- Get role ID
        SET v_roleid = NULL;
        SELECT ID INTO v_roleid
        FROM mtm_wip_application.sys_roles
        WHERE RoleName = v_rolename
        LIMIT 1;
        
        IF v_roleid IS NULL THEN
            INSERT INTO migration_debug_log (message) VALUES (CONCAT('Role NOT found: ', v_rolename));
            SET v_ErrorCount = v_ErrorCount + 1;
            ITERATE read_loop;
        END IF;
        
        -- Check existing role
        SET v_existing_roleid = NULL;
        SELECT RoleID INTO v_existing_roleid
        FROM mtm_wip_application.sys_user_roles
        WHERE UserID = v_new_userid
        LIMIT 1;
        
        IF v_existing_roleid IS NOT NULL THEN
            IF v_existing_roleid = v_roleid THEN
                INSERT INTO migration_debug_log (message) VALUES (CONCAT('User-role already exists for: ', v_old_username, ' | Role: ', v_rolename));
            ELSE
                -- Update role
                UPDATE mtm_wip_application.sys_user_roles
                SET RoleID = v_roleid, AssignedBy = '[ System Migration ]', AssignedAt = NOW()
                WHERE UserID = v_new_userid;
                
                INSERT INTO migration_debug_log (message) VALUES (CONCAT('User-role updated for: ', v_old_username, ' to role: ', v_rolename));
                SET v_ProcessedCount = v_ProcessedCount + 1;
            END IF;
        ELSE
            -- Insert new role
            INSERT INTO mtm_wip_application.sys_user_roles
                (UserID, RoleID, AssignedBy, AssignedAt)
            VALUES
                (v_new_userid, v_roleid, '[ System Migration ]', NOW());
            
            INSERT INTO migration_debug_log (message) VALUES (CONCAT('Migrated user: ', v_old_username, ' with role: ', v_rolename));
            SET v_ProcessedCount = v_ProcessedCount + 1;
        END IF;
    END LOOP;
    
    CLOSE user_cur;
    COMMIT;
    
    IF v_ProcessedCount > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Processed ', v_ProcessedCount, ' user(s), ', v_ErrorCount, ' error(s)');
    ELSEIF v_ErrorCount > 0 THEN
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('No users processed, ', v_ErrorCount, ' error(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No users needed migration';
    END IF;
END$$


-- ========================================
-- Procedure: query_get_all_usernames_and_roles
-- ========================================

DROP PROCEDURE IF EXISTS `query_get_all_usernames_and_roles`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `query_get_all_usernames_and_roles`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get all usernames and their roles
    SELECT u.User AS Username, s.RoleName
    FROM `mtm_wip_application`.usr_users u
    JOIN `mtm_wip_application`.sys_user_roles r ON r.UserID = u.ID
    JOIN `mtm_wip_application`.sys_roles s ON s.ID = r.RoleID
    ORDER BY u.User;
    
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' user(s) with roles');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No users with roles found';
    END IF;
    
    COMMIT;
END$$


-- ========================================
-- Procedure: sp_ReassignBatchNumbers
-- ========================================

DROP PROCEDURE IF EXISTS `sp_ReassignBatchNumbers`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ReassignBatchNumbers`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE done INT DEFAULT 0;
    DECLARE curINID BIGINT;
    DECLARE curPartID VARCHAR(50);
    DECLARE curINDate DATETIME;
    DECLARE curQty DECIMAL(18,4);
    DECLARE curFromLocation VARCHAR(50);
    DECLARE curToLocation VARCHAR(50);
    DECLARE newBatch BIGINT;
    DECLARE batch_str VARCHAR(20);
    DECLARE curOtherID BIGINT;
    DECLARE curOtherPartID VARCHAR(50);
    DECLARE v_ProcessedCount INT DEFAULT 0;
    
    DECLARE in_cursor CURSOR FOR
        SELECT ID, PartID, ReceiveDate, Quantity, FromLocation, ToLocation
        FROM mtm_wip_application.inv_transaction
        WHERE TransactionType = 'IN' AND BatchNumber IS NULL
        ORDER BY PartID, ReceiveDate;
    
    DECLARE other_cursor CURSOR FOR
        SELECT ID, PartID
        FROM mtm_wip_application.inv_transaction
        WHERE TransactionType IN ('TRANSFER', 'OUT') AND BatchNumber IS NULL
        ORDER BY PartID;
    
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get current batch number
    SELECT last_batch_number INTO newBatch FROM mtm_wip_application.inv_inventory_batch_seq;
    
    -- Process IN transactions
    OPEN in_cursor;
    in_loop: LOOP
        FETCH in_cursor INTO curINID, curPartID, curINDate, curQty, curFromLocation, curToLocation;
        IF done THEN
            LEAVE in_loop;
        END IF;
        SET newBatch = newBatch + 1;
        SET batch_str = LPAD(newBatch, 10, '0');
        UPDATE mtm_wip_application.inv_transaction
        SET BatchNumber = batch_str
        WHERE ID = curINID;
        SET v_ProcessedCount = v_ProcessedCount + 1;
    END LOOP;
    CLOSE in_cursor;
    
    SET done = 0;
    
    -- Process other transaction types
    OPEN other_cursor;
    other_loop: LOOP
        FETCH other_cursor INTO curOtherID, curOtherPartID;
        IF done THEN
            LEAVE other_loop;
        END IF;
        SET newBatch = newBatch + 1;
        SET batch_str = LPAD(newBatch, 10, '0');
        UPDATE mtm_wip_application.inv_transaction
        SET BatchNumber = batch_str
        WHERE ID = curOtherID;
        SET v_ProcessedCount = v_ProcessedCount + 1;
    END LOOP;
    CLOSE other_cursor;
    
    -- Update batch sequence
    UPDATE mtm_wip_application.inv_inventory_batch_seq
    SET last_batch_number = newBatch;
    
    COMMIT;
    
    IF v_ProcessedCount > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Reassigned ', v_ProcessedCount, ' batch number(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No batch numbers needed reassignment';
    END IF;
END$$


-- ========================================
-- Procedure: sys_GetUserAccessType
-- ========================================

-- =============================================
-- Stored Procedure: sys_GetUserAccessType
-- Description: Retrieves all users with their role information
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DROP PROCEDURE IF EXISTS `sys_GetUserAccessType`$$

CREATE PROCEDURE `sys_GetUserAccessType`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;

    -- Validate that required tables exist
    IF NOT EXISTS (SELECT 1 FROM information_schema.tables 
                   WHERE table_schema = DATABASE() AND table_name = 'usr_users') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table usr_users not found';
        SELECT NULL LIMIT 0;
        ROLLBACK;
    ELSEIF NOT EXISTS (SELECT 1 FROM information_schema.tables 
                       WHERE table_schema = DATABASE() AND table_name = 'sys_user_roles') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table sys_user_roles not found';
        SELECT NULL LIMIT 0;
        ROLLBACK;
    ELSEIF NOT EXISTS (SELECT 1 FROM information_schema.tables 
                       WHERE table_schema = DATABASE() AND table_name = 'sys_roles') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table sys_roles not found';
        SELECT NULL LIMIT 0;
        ROLLBACK;
    ELSE
        -- Return users with their roles
        SELECT 
            u.ID AS UserID,
            u.User AS UserName,
            COALESCE(r.RoleName, 'User') AS RoleName
        FROM usr_users u
        LEFT JOIN sys_user_roles ur ON u.ID = ur.UserID
        LEFT JOIN sys_roles r ON ur.RoleID = r.ID
        ORDER BY u.User;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' user access type(s)');
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = 'No user access types found';
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_AddQuickButton_1
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_AddQuickButton_1`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_AddQuickButton_1`(
    IN p_User VARCHAR(255),
    IN p_PartID VARCHAR(255),
    IN p_Operation VARCHAR(255),
    IN p_Quantity INT,
    IN p_Position INT,
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
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_PartID IS NULL OR TRIM(p_PartID) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Part ID is required';
        ROLLBACK;
    ELSEIF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSEIF p_Quantity IS NULL OR p_Quantity < 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid quantity is required';
        ROLLBACK;
    ELSEIF p_Position IS NULL OR p_Position < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid position is required';
        ROLLBACK;
    ELSE
        -- Shift existing positions
        UPDATE sys_last_10_transactions
        SET Position = Position + 1
        WHERE User = p_User AND Position >= p_Position;
        
        -- Insert new quick button
        INSERT INTO sys_last_10_transactions (User, PartID, Operation, Quantity, Position)
        VALUES (p_User, p_PartID, p_Operation, p_Quantity, p_Position);
        
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Quick button added at position ', p_Position);
            COMMIT;
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to add quick button';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_Get_ByUser_1
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Get_ByUser_1`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Get_ByUser_1`(
    IN p_User VARCHAR(255),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate input
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        -- Get last 10 transactions for user
        SELECT *
        FROM sys_last_10_transactions
        WHERE User = p_User
        ORDER BY Position ASC
        LIMIT 10;
        
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' transaction(s) for user: ', p_User);
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No transactions found for user: ', p_User);
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_Get_ByUser
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Get_ByUser`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Get_ByUser`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate parameter
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        -- Return quick button data ordered by position
        SELECT 
            Position,
            User,
            PartID AS p_PartID,
            Operation AS p_Operation,
            Quantity,
            ReceiveDate
        FROM sys_last_10_transactions 
        WHERE User = p_User 
        ORDER BY Position;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' quick button(s) for user: ', p_User);
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('No quick buttons found for user: ', p_User);
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_Move_1
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Move_1`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Move_1`(
    IN p_User VARCHAR(255),
    IN p_FromPosition INT,
    IN p_ToPosition INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE tempPartID VARCHAR(255);
    DECLARE tempOperation VARCHAR(255);
    DECLARE tempQuantity INT;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_FromPosition IS NULL OR p_FromPosition < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid from position is required';
        ROLLBACK;
    ELSEIF p_ToPosition IS NULL OR p_ToPosition < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid to position is required';
        ROLLBACK;
    ELSE
        -- Get transaction data from source position
        SELECT PartID, Operation, Quantity
        INTO tempPartID, tempOperation, tempQuantity
        FROM sys_last_10_transactions
        WHERE User = p_User AND Position = p_FromPosition
        LIMIT 1;
        
        IF tempPartID IS NULL THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('No transaction found at position ', p_FromPosition, ' for user: ', p_User);
            ROLLBACK;
        ELSE
            -- Delete from original position
            DELETE FROM sys_last_10_transactions
            WHERE User = p_User AND Position = p_FromPosition;
            
            -- Shift positions
            IF p_FromPosition < p_ToPosition THEN
                UPDATE sys_last_10_transactions
                SET Position = Position - 1
                WHERE User = p_User AND Position > p_FromPosition AND Position <= p_ToPosition;
            ELSE
                UPDATE sys_last_10_transactions
                SET Position = Position + 1
                WHERE User = p_User AND Position >= p_ToPosition AND Position < p_FromPosition;
            END IF;
            
            -- Insert at new position
            INSERT INTO sys_last_10_transactions (User, PartID, Operation, Quantity, Position)
            VALUES (p_User, tempPartID, tempOperation, tempQuantity, p_ToPosition);
            
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Moved transaction from position ', p_FromPosition, ' to ', p_ToPosition);
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to insert transaction at new position';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_MoveToLast_ByUser
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_MoveToLast_ByUser`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_MoveToLast_ByUser`(
    IN p_User VARCHAR(255),
    IN p_ReceiveDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE latest_date DATETIME;
    DECLARE target_id INT;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_ReceiveDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Receive date is required';
        ROLLBACK;
    ELSE
        -- Get target transaction ID
        SELECT id INTO target_id
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate = p_ReceiveDate
        LIMIT 1;
        
        IF target_id IS NULL THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Transaction not found for given user and date';
            ROLLBACK;
        ELSE
            -- Get latest date
            SELECT MAX(ReceiveDate) INTO latest_date
            FROM sys_last_10_transactions
            WHERE User = p_User;
            
            -- Only update if not already at the end
            IF p_ReceiveDate < latest_date THEN
                UPDATE sys_last_10_transactions
                SET ReceiveDate = DATE_ADD(latest_date, INTERVAL 1 SECOND)
                WHERE id = target_id;
                
                SET v_RowCount = ROW_COUNT();
                
                IF v_RowCount > 0 THEN
                    SET p_Status = 1;
                    SET p_ErrorMsg = 'Transaction moved to last position';
                    COMMIT;
                ELSE
                    SET p_Status = -3;
                    SET p_ErrorMsg = 'Failed to update transaction date';
                    ROLLBACK;
                END IF;
            ELSE
                SET p_Status = 0;
                SET p_ErrorMsg = 'Transaction already at last position';
                COMMIT;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_RemoveAndShift_ByUser_1
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_RemoveAndShift_ByUser_1`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_RemoveAndShift_ByUser_1`(
    IN p_User VARCHAR(255),
    IN p_Position INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_Position IS NULL OR p_Position < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid position is required';
        ROLLBACK;
    ELSE
        -- Check if transaction exists at position
        SELECT COUNT(*) INTO v_Exists
        FROM sys_last_10_transactions
        WHERE User = p_User AND Position = p_Position;
        
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('No transaction found at position ', p_Position, ' for user: ', p_User);
            ROLLBACK;
        ELSE
            -- Delete transaction
            DELETE FROM sys_last_10_transactions
            WHERE User = p_User AND Position = p_Position;
            
            SET v_RowCount = ROW_COUNT();
            
            -- Shift remaining positions
            UPDATE sys_last_10_transactions
            SET Position = Position - 1
            WHERE User = p_User AND Position > p_Position;
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Transaction at position ', p_Position, ' removed and remaining transactions shifted');
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to remove transaction';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_RemoveAndShift_ByUser
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_RemoveAndShift_ByUser`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_RemoveAndShift_ByUser`(
    IN p_User VARCHAR(255),
    IN p_ReceiveDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE target_exists INT;
    DECLARE last_id INT;
    DECLARE done INT DEFAULT 0;
    DECLARE curr_id INT;
    DECLARE curr_date DATETIME;
    DECLARE prev_date DATETIME;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE dates_to_shift CURSOR FOR
        SELECT id, ReceiveDate
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate > p_ReceiveDate
        ORDER BY ReceiveDate;
    
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_ReceiveDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Receive date is required';
        ROLLBACK;
    ELSE
        -- Check if target exists
        SELECT COUNT(*) INTO target_exists 
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate = p_ReceiveDate;
        
        IF target_exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Transaction not found for given user and date';
            ROLLBACK;
        ELSE
            -- Set starting date for shift
            SET prev_date = p_ReceiveDate;
            
            -- Shift all later dates backward
            OPEN dates_to_shift;
            read_loop: LOOP
                FETCH dates_to_shift INTO curr_id, curr_date;
                IF done THEN
                    LEAVE read_loop;
                END IF;
                
                UPDATE sys_last_10_transactions
                SET ReceiveDate = prev_date
                WHERE id = curr_id;
                
                SET prev_date = curr_date;
                SET last_id = curr_id;
            END LOOP;
            CLOSE dates_to_shift;
            
            -- Delete the target transaction
            DELETE FROM sys_last_10_transactions
            WHERE User = p_User AND ReceiveDate = p_ReceiveDate
            LIMIT 1;
            
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = 'Transaction removed and remaining transactions shifted';
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to delete transaction';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_Reorder_ByUser
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Reorder_ByUser`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Reorder_ByUser`(
    IN p_User VARCHAR(255), 
    IN p_SrcReceiveDate DATETIME, 
    IN p_TargetIndex INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_SrcIdx INT;
    DECLARE v_TotalCount INT;
    DECLARE v_SrcDate DATETIME;
    DECLARE v_CurDate DATETIME;
    DECLARE v_Counter INT;
    DECLARE v_BaseTime DATETIME;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        DROP TEMPORARY TABLE IF EXISTS tempDates;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_SrcReceiveDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Source receive date is required';
        ROLLBACK;
    ELSEIF p_TargetIndex IS NULL OR p_TargetIndex < 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid target index is required';
        ROLLBACK;
    ELSE
        -- Create temp table
        CREATE TEMPORARY TABLE tempDates (idx INT AUTO_INCREMENT PRIMARY KEY, dt DATETIME);
        INSERT INTO tempDates (dt)
            SELECT ReceiveDate FROM sys_last_10_transactions WHERE User = p_User ORDER BY ReceiveDate;
        
        -- Find source index
        SELECT idx INTO v_SrcIdx FROM tempDates WHERE dt = p_SrcReceiveDate LIMIT 1;
        SELECT COUNT(*) INTO v_TotalCount FROM tempDates;
        
        IF v_SrcIdx IS NULL THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Source transaction not found';
            DROP TEMPORARY TABLE tempDates;
            ROLLBACK;
        ELSEIF v_SrcIdx = p_TargetIndex + 1 THEN
            SET p_Status = 0;
            SET p_ErrorMsg = 'Transaction already at target position';
            DROP TEMPORARY TABLE tempDates;
            COMMIT;
        ELSE
            -- Get source date
            SELECT dt INTO v_SrcDate FROM tempDates WHERE idx = v_SrcIdx LIMIT 1;
            
            -- Remove from current position
            DELETE FROM tempDates WHERE idx = v_SrcIdx;
            UPDATE tempDates SET idx = idx - 1 WHERE idx > v_SrcIdx;
            
            -- Insert at new position
            UPDATE tempDates SET idx = idx + 1 WHERE idx > p_TargetIndex;
            INSERT INTO tempDates (idx, dt) VALUES (p_TargetIndex + 1, v_SrcDate);
            
            -- Reorder by updating ReceiveDates
            SET v_Counter = 1;
            SET v_BaseTime = NOW();
            
            WHILE v_Counter <= v_TotalCount DO
                SELECT dt INTO v_CurDate FROM tempDates WHERE idx = v_Counter LIMIT 1;
                IF v_CurDate IS NOT NULL THEN
                    UPDATE sys_last_10_transactions
                    SET ReceiveDate = DATE_ADD(v_BaseTime, INTERVAL v_Counter SECOND)
                    WHERE User = p_User AND ReceiveDate = v_CurDate;
                END IF;
                SET v_Counter = v_Counter + 1;
            END WHILE;
            
            DROP TEMPORARY TABLE tempDates;
            
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Transaction reordered to index ', p_TargetIndex);
            COMMIT;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_SwapPositions_ByUser
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_SwapPositions_ByUser`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_SwapPositions_ByUser`(
    IN p_User VARCHAR(255),
    IN p_ReceiveDate1 DATETIME,
    IN p_ReceiveDate2 DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE id1 INT;
    DECLARE id2 INT;
    DECLARE temp_date DATETIME;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_ReceiveDate1 IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'First receive date is required';
        ROLLBACK;
    ELSEIF p_ReceiveDate2 IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Second receive date is required';
        ROLLBACK;
    ELSE
        -- Get IDs for both transactions
        SELECT id INTO id1 
        FROM sys_last_10_transactions 
        WHERE User = p_User AND ReceiveDate = p_ReceiveDate1
        LIMIT 1;
        
        SELECT id INTO id2 
        FROM sys_last_10_transactions 
        WHERE User = p_User AND ReceiveDate = p_ReceiveDate2
        LIMIT 1;
        
        IF id1 IS NULL OR id2 IS NULL THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'One or both transactions not found';
            ROLLBACK;
        ELSE
            -- Store temp date
            SET temp_date = p_ReceiveDate1;
            
            -- Swap dates
            UPDATE sys_last_10_transactions
            SET ReceiveDate = p_ReceiveDate2
            WHERE id = id1;
            
            UPDATE sys_last_10_transactions
            SET ReceiveDate = temp_date
            WHERE id = id2;
            
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = 'Transaction positions swapped successfully';
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to swap positions';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_Update_ByUserAndDate
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Update_ByUserAndDate`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Update_ByUserAndDate`(
    IN p_User VARCHAR(255),
    IN p_OldReceiveDate DATETIME,
    IN p_PartID VARCHAR(255),
    IN p_Operation VARCHAR(255),
    IN p_Quantity INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_OldReceiveDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Old receive date is required';
        ROLLBACK;
    ELSEIF p_PartID IS NULL OR TRIM(p_PartID) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Part ID is required';
        ROLLBACK;
    ELSEIF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSEIF p_Quantity IS NULL OR p_Quantity < 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid quantity is required';
        ROLLBACK;
    ELSE
        -- Check if transaction exists
        SELECT COUNT(*) INTO v_Exists
        FROM sys_last_10_transactions
        WHERE User = p_User AND ReceiveDate = p_OldReceiveDate;
        
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = 'Transaction not found for given user and date';
            ROLLBACK;
        ELSE
            -- Update transaction
            UPDATE sys_last_10_transactions
            SET PartID = p_PartID,
                Operation = p_Operation,
                Quantity = p_Quantity,
                ReceiveDate = NOW()
            WHERE User = p_User AND ReceiveDate = p_OldReceiveDate;
            
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = 'Transaction updated successfully';
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to update transaction';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_last_10_transactions_Update_ByUserAndPosition_1
-- ========================================

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Update_ByUserAndPosition_1`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Update_ByUserAndPosition_1`(
    IN p_User VARCHAR(255),
    IN p_Position INT,
    IN p_PartID VARCHAR(255),
    IN p_Operation VARCHAR(255),
    IN p_Quantity INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_Position IS NULL OR p_Position < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid position is required';
        ROLLBACK;
    ELSEIF p_PartID IS NULL OR TRIM(p_PartID) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Part ID is required';
        ROLLBACK;
    ELSEIF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSEIF p_Quantity IS NULL OR p_Quantity < 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid quantity is required';
        ROLLBACK;
    ELSE
        -- Check if transaction exists
        SELECT COUNT(*) INTO v_Exists
        FROM sys_last_10_transactions
        WHERE User = p_User AND Position = p_Position;
        
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('No transaction found at position ', p_Position);
            ROLLBACK;
        ELSE
            -- Update transaction
            UPDATE sys_last_10_transactions
            SET PartID = p_PartID,
                Operation = p_Operation,
                Quantity = p_Quantity
            WHERE User = p_User AND Position = p_Position;
            
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Transaction at position ', p_Position, ' updated successfully');
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to update transaction';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_role_GetIdByName
-- ========================================

-- =============================================
-- Stored Procedure: sys_role_GetIdByName
-- Description: Retrieves role ID by role name (scalar result)
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DROP PROCEDURE IF EXISTS `sys_role_GetIdByName`$$

CREATE PROCEDURE `sys_role_GetIdByName`(
    IN p_RoleName VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RoleId INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;

    -- Validate input
    IF p_RoleName IS NULL OR TRIM(p_RoleName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Role name is required';
        SELECT 0 AS RoleID;
        ROLLBACK;
    ELSE
        -- Get role ID
        SELECT ID INTO v_RoleId FROM sys_roles WHERE RoleName = p_RoleName LIMIT 1;
        
        IF v_RoleId IS NULL OR v_RoleId = 0 THEN
            SET p_Status = 0;  -- Success but no data (not found)
            SET p_ErrorMsg = CONCAT('Role "', p_RoleName, '" not found');
            SELECT 0 AS RoleID;
        ELSE
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Role ID retrieved for "', p_RoleName, '"');
            SELECT v_RoleId AS RoleID;
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: sys_roles_Get_ById
-- ========================================

DROP PROCEDURE IF EXISTS `sys_roles_Get_ById`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_roles_Get_ById`(
    IN p_ID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate parameter
    IF p_ID IS NULL OR p_ID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid role ID is required';
        ROLLBACK;
    ELSE
        -- Get role by ID
        SELECT * FROM sys_roles WHERE ID = p_ID;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved role ID ', p_ID);
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('Role ID ', p_ID, ' not found');
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: sys_theme_GetAll
-- ========================================

-- =============================================
-- Stored Procedure: sys_theme_GetAll
-- Description: Retrieves all theme configurations
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DROP PROCEDURE IF EXISTS `sys_theme_GetAll`$$

CREATE PROCEDURE `sys_theme_GetAll`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;

    -- Return all themes
    SELECT ThemeName, SettingsJson 
    FROM app_themes 
    ORDER BY ThemeName;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' theme(s)');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No themes found';
    END IF;
    
    COMMIT;
END$$


-- ========================================
-- Procedure: sys_user_access_SetType
-- ========================================

-- =============================================
-- Stored Procedure: sys_user_access_SetType
-- Description: Sets user access type (for testing purposes)
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DROP PROCEDURE IF EXISTS `sys_user_access_SetType`$$

CREATE PROCEDURE `sys_user_access_SetType`(
    IN p_User VARCHAR(100),
    IN p_AccessType INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;

    -- Validate input
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User name is required';
        ROLLBACK;
    ELSEIF p_AccessType IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Access type is required';
        ROLLBACK;
    ELSE
        -- Check if user exists
        SELECT COUNT(*) INTO v_Exists FROM usr_users WHERE User = p_User;
        
        IF v_Exists = 0 THEN
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            ROLLBACK;
        ELSE
            -- Update access type (using VitsUser as a proxy field for access type)
            UPDATE usr_users SET VitsUser = p_AccessType WHERE User = p_User;
            
            -- Check if update was successful
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;  -- Success
                SET p_ErrorMsg = CONCAT('Access type updated for user "', p_User, '"');
                COMMIT;
            ELSE
                SET p_Status = -3;  -- Business logic error
                SET p_ErrorMsg = 'Failed to update access type';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_user_GetByName
-- ========================================

-- =============================================
-- Stored Procedure: sys_user_GetByName
-- Description: Retrieves user information by username
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DROP PROCEDURE IF EXISTS `sys_user_GetByName`$$

CREATE PROCEDURE `sys_user_GetByName`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;

    -- Validate input
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User name is required';
        ROLLBACK;
    ELSE
        -- Return user information
        SELECT * FROM usr_users WHERE User = p_User;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved user "', p_User, '"');
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: sys_user_GetIdByName
-- ========================================

-- =============================================
-- Stored Procedure: sys_user_GetIdByName
-- Description: Retrieves user ID by username (scalar result)
-- Created: 2025-10-14
-- Part of: 002-comprehensive-database-layer refactor
-- =============================================

DROP PROCEDURE IF EXISTS `sys_user_GetIdByName`$$

CREATE PROCEDURE `sys_user_GetIdByName`(
    IN p_UserName VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_UserId INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;

    -- Validate input
    IF p_UserName IS NULL OR TRIM(p_UserName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User name is required';
        SELECT 0 AS UserID;
        ROLLBACK;
    ELSE
        -- Get user ID
        SELECT ID INTO v_UserId FROM usr_users WHERE User = p_UserName LIMIT 1;
        
        IF v_UserId IS NULL OR v_UserId = 0 THEN
            SET p_Status = 0;  -- Success but no data (not found)
            SET p_ErrorMsg = CONCAT('User "', p_UserName, '" not found');
            SELECT 0 AS UserID;
        ELSE
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('User ID retrieved for "', p_UserName, '"');
            SELECT v_UserId AS UserID;
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: sys_user_roles_Add
-- ========================================

DROP PROCEDURE IF EXISTS `sys_user_roles_Add`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_roles_Add`(
    IN p_UserID INT, 
    IN p_RoleID INT, 
    IN p_AssignedBy VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_UserExists INT DEFAULT 0;
    DECLARE v_RoleExists INT DEFAULT 0;
    DECLARE v_DuplicateExists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_UserID IS NULL OR p_UserID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid user ID is required';
        ROLLBACK;
    ELSEIF p_RoleID IS NULL OR p_RoleID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid role ID is required';
        ROLLBACK;
    ELSEIF p_AssignedBy IS NULL OR TRIM(p_AssignedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Assigned by user is required';
        ROLLBACK;
    ELSE
        -- Check if user exists
        SELECT COUNT(*) INTO v_UserExists FROM usr_users WHERE ID = p_UserID;
        
        IF v_UserExists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User ID ', p_UserID, ' not found');
            ROLLBACK;
        ELSE
            -- Check if role exists
            SELECT COUNT(*) INTO v_RoleExists FROM sys_roles WHERE ID = p_RoleID;
            
            IF v_RoleExists = 0 THEN
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('Role ID ', p_RoleID, ' not found');
                ROLLBACK;
            ELSE
                -- Check for duplicate
                SELECT COUNT(*) INTO v_DuplicateExists 
                FROM sys_user_roles 
                WHERE UserID = p_UserID AND RoleID = p_RoleID;
                
                IF v_DuplicateExists > 0 THEN
                    SET p_Status = -5;
                    SET p_ErrorMsg = 'User already has this role assigned';
                    ROLLBACK;
                ELSE
                    -- Insert user role
                    INSERT INTO sys_user_roles (UserID, RoleID, AssignedBy)
                    VALUES (p_UserID, p_RoleID, p_AssignedBy);
                    
                    SET v_RowCount = ROW_COUNT();
                    
                    IF v_RowCount > 0 THEN
                        SET p_Status = 1;
                        SET p_ErrorMsg = CONCAT('Role ', p_RoleID, ' assigned to user ', p_UserID);
                        COMMIT;
                    ELSE
                        SET p_Status = -3;
                        SET p_ErrorMsg = 'Failed to assign role';
                        ROLLBACK;
                    END IF;
                END IF;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_user_roles_Delete
-- ========================================

DROP PROCEDURE IF EXISTS `sys_user_roles_Delete`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_roles_Delete`(
    IN p_UserID INT, 
    IN p_RoleID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_UserID IS NULL OR p_UserID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid user ID is required';
        ROLLBACK;
    ELSEIF p_RoleID IS NULL OR p_RoleID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid role ID is required';
        ROLLBACK;
    ELSE
        -- Check if assignment exists
        SELECT COUNT(*) INTO v_Exists 
        FROM sys_user_roles 
        WHERE UserID = p_UserID AND RoleID = p_RoleID;
        
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Role ', p_RoleID, ' not assigned to user ', p_UserID);
            ROLLBACK;
        ELSE
            -- Delete user role
            DELETE FROM sys_user_roles
            WHERE UserID = p_UserID AND RoleID = p_RoleID;
            
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Role ', p_RoleID, ' removed from user ', p_UserID);
                COMMIT;
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to remove role';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: sys_user_roles_Update
-- ========================================

DROP PROCEDURE IF EXISTS `sys_user_roles_Update`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_roles_Update`(
    IN p_UserID INT, 
    IN p_NewRoleID INT, 
    IN p_AssignedBy VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_UserExists INT DEFAULT 0;
    DECLARE v_RoleExists INT DEFAULT 0;
    DECLARE v_OldRoleExists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_UserID IS NULL OR p_UserID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid user ID is required';
        ROLLBACK;
    ELSEIF p_NewRoleID IS NULL OR p_NewRoleID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid new role ID is required';
        ROLLBACK;
    ELSEIF p_AssignedBy IS NULL OR TRIM(p_AssignedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Assigned by user is required';
        ROLLBACK;
    ELSE
        -- Check if user exists
        SELECT COUNT(*) INTO v_UserExists FROM usr_users WHERE ID = p_UserID;
        
        IF v_UserExists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User ID ', p_UserID, ' not found');
            ROLLBACK;
        ELSE
            -- Check if new role exists
            SELECT COUNT(*) INTO v_RoleExists FROM sys_roles WHERE ID = p_NewRoleID;
            
            IF v_RoleExists = 0 THEN
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('Role ID ', p_NewRoleID, ' not found');
                ROLLBACK;
            ELSE
                -- Check if user has any existing role
                SELECT COUNT(*) INTO v_OldRoleExists FROM sys_user_roles WHERE UserID = p_UserID;
                
                -- Delete all old roles for user
                DELETE FROM sys_user_roles WHERE UserID = p_UserID;
                
                -- Insert new role
                INSERT INTO sys_user_roles (UserID, RoleID, AssignedBy)
                VALUES (p_UserID, p_NewRoleID, p_AssignedBy);
                
                SET v_RowCount = ROW_COUNT();
                
                IF v_RowCount > 0 THEN
                    SET p_Status = 1;
                    SET p_ErrorMsg = CONCAT('User ', p_UserID, ' role updated to ', p_NewRoleID);
                    COMMIT;
                ELSE
                    SET p_Status = -3;
                    SET p_ErrorMsg = 'Failed to update user role';
                    ROLLBACK;
                END IF;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: usr_ui_settings_Get
-- ========================================

DROP PROCEDURE IF EXISTS `usr_ui_settings_Get`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_Get`(
    IN p_UserId VARCHAR(64),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate parameter
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
        ROLLBACK;
    ELSE
        -- Get UI settings for user
        SELECT SettingsJson
        FROM usr_ui_settings
        WHERE UserId = p_UserId;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved settings for user "', p_UserId, '"');
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('No settings found for user "', p_UserId, '"');
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: usr_ui_settings_GetShortcutsJson
-- ========================================

DROP PROCEDURE IF EXISTS `usr_ui_settings_GetShortcutsJson`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_GetShortcutsJson`(
    IN p_UserId VARCHAR(255),
    OUT p_ShortcutsJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ShortcutsJson = NULL;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate parameter
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
        SET p_ShortcutsJson = NULL;
        ROLLBACK;
    ELSE
        -- Get shortcuts JSON for user
        SELECT ShortcutsJson INTO p_ShortcutsJson
        FROM usr_ui_settings
        WHERE UserId = p_UserId
        LIMIT 1;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 AND p_ShortcutsJson IS NOT NULL THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved shortcuts for user "', p_UserId, '"');
        ELSEIF v_Count > 0 AND p_ShortcutsJson IS NULL THEN
            SET p_Status = 0;  -- User exists but no shortcuts JSON
            SET p_ErrorMsg = CONCAT('No shortcuts JSON for user "', p_UserId, '"');
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('User "', p_UserId, '" not found in settings');
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: usr_ui_settings_SetJsonSetting
-- ========================================

DROP PROCEDURE IF EXISTS `usr_ui_settings_SetJsonSetting`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_SetJsonSetting`(
    IN p_UserId VARCHAR(64),
    IN p_DgvName VARCHAR(128),
    IN p_SettingJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Existing INT DEFAULT 0;
    DECLARE v_CurrentJson JSON;
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
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
        ROLLBACK;
    ELSEIF p_DgvName IS NULL OR TRIM(p_DgvName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'DgvName is required';
        ROLLBACK;
    ELSEIF p_SettingJson IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'SettingJson is required';
        ROLLBACK;
    ELSE
        -- Check if user settings exist
        SELECT COUNT(*) INTO v_Existing
        FROM usr_ui_settings
        WHERE UserId = p_UserId;
        
        IF v_Existing = 0 THEN
            -- Insert new settings
            INSERT INTO usr_ui_settings (UserId, SettingsJson)
            VALUES (p_UserId, JSON_OBJECT(p_DgvName, p_SettingJson));
            
            SET v_RowCount = ROW_COUNT();
        ELSE
            -- Update existing settings
            SELECT SettingsJson INTO v_CurrentJson 
            FROM usr_ui_settings 
            WHERE UserId = p_UserId 
            LIMIT 1;
            
            SET v_CurrentJson = JSON_SET(IFNULL(v_CurrentJson, JSON_OBJECT()), 
                                        CONCAT('$.', p_DgvName), p_SettingJson);
            
            UPDATE usr_ui_settings 
            SET SettingsJson = v_CurrentJson, UpdatedAt = NOW() 
            WHERE UserId = p_UserId;
            
            SET v_RowCount = ROW_COUNT();
        END IF;
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('JSON setting "', p_DgvName, '" saved for user "', p_UserId, '"');
            COMMIT;
        ELSE
            SET p_Status = -3;  -- Business logic error
            SET p_ErrorMsg = 'Failed to save JSON setting';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: usr_ui_settings_SetShortcutsJson
-- ========================================

DROP PROCEDURE IF EXISTS `usr_ui_settings_SetShortcutsJson`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_SetShortcutsJson`(
    IN p_UserId VARCHAR(255),
    IN p_ShortcutsJson JSON,
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
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
        ROLLBACK;
    ELSEIF p_ShortcutsJson IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ShortcutsJson is required';
        ROLLBACK;
    ELSE
        -- Update shortcuts JSON
        UPDATE usr_ui_settings
        SET ShortcutsJson = p_ShortcutsJson
        WHERE UserId = p_UserId;
        
        -- Check if update was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Shortcuts JSON saved for user "', p_UserId, '"');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('User "', p_UserId, '" not found in settings');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: usr_ui_settings_SetThemeJson
-- ========================================

DROP PROCEDURE IF EXISTS `usr_ui_settings_SetThemeJson`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_SetThemeJson`(
    IN p_UserId VARCHAR(64),
    IN p_ThemeJson JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_SettingsJson JSON DEFAULT NULL;
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
    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
        ROLLBACK;
    ELSEIF p_ThemeJson IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ThemeJson is required';
        ROLLBACK;
    ELSE
        -- Check if user exists in settings
        SELECT COUNT(*) INTO v_Exists 
        FROM usr_ui_settings 
        WHERE UserId = p_UserId;
        
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User "', p_UserId, '" does not exist in usr_ui_settings');
            ROLLBACK;
        ELSE
            -- Get existing settings JSON
            SELECT SettingsJson INTO v_SettingsJson 
            FROM usr_ui_settings 
            WHERE UserId = p_UserId 
            FOR UPDATE;
            
            -- Merge theme JSON
            IF v_SettingsJson IS NULL THEN
                SET v_SettingsJson = p_ThemeJson;
            ELSE
                SET v_SettingsJson = JSON_MERGE_PATCH(v_SettingsJson, p_ThemeJson);
            END IF;
            
            -- Update settings
            UPDATE usr_ui_settings
            SET SettingsJson = v_SettingsJson
            WHERE UserId = p_UserId;
            
            -- Check if update was successful
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;  -- Success
                SET p_ErrorMsg = CONCAT('Theme JSON saved for user "', p_UserId, '"');
                COMMIT;
            ELSE
                SET p_Status = -3;  -- Business logic error
                SET p_ErrorMsg = 'Failed to save theme JSON';
                ROLLBACK;
            END IF;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: usr_users_Add_User
-- ========================================

DROP PROCEDURE IF EXISTS `usr_users_Add_User`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Add_User`(
    IN p_User VARCHAR(100),
    IN p_FullName VARCHAR(200),
    IN p_Shift VARCHAR(50),
    IN p_VitsUser TINYINT,
    IN p_Pin VARCHAR(50),
    IN p_LastShownVersion VARCHAR(50),
    IN p_HideChangeLog VARCHAR(50),
    IN p_Theme_Name VARCHAR(50),
    IN p_Theme_FontSize INT,
    IN p_VisualUserName VARCHAR(50),
    IN p_VisualPassword VARCHAR(50),
    IN p_WipServerAddress VARCHAR(15),
    IN p_WipServerPort VARCHAR(10),
    IN p_WipDatabase VARCHAR(100),
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
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_FullName IS NULL OR TRIM(p_FullName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Full Name is required';
        ROLLBACK;
    ELSE
        -- Insert new user
        INSERT INTO usr_users (
            `User`, `Full Name`, `Shift`, `VitsUser`, `Pin`, `LastShownVersion`, `HideChangeLog`, 
            `Theme_Name`, `Theme_FontSize`, `VisualUserName`, `VisualPassword`, 
            `WipServerAddress`, `WipDatabase`, `WipServerPort`
        ) VALUES (
            p_User, p_FullName, p_Shift, p_VitsUser, p_Pin, p_LastShownVersion, p_HideChangeLog, 
            p_Theme_Name, p_Theme_FontSize, p_VisualUserName, p_VisualPassword, 
            p_WipServerAddress, p_WipDatabase, p_WipServerPort
        );
        
        -- Check if insert was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            -- Create MySQL user for database access
            SET @createUserQuery := CONCAT(
                'CREATE USER IF NOT EXISTS \'', REPLACE(p_User, '\'', '\\\''), '\'@\'%\''
            );
            PREPARE stmt FROM @createUserQuery;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
            
            -- Grant privileges
            SET @grantAllQuery := CONCAT(
                'GRANT ALL PRIVILEGES ON *.* TO \'', REPLACE(p_User, '\'', '\\\''), '\'@\'%\';'
            );
            PREPARE stmt FROM @grantAllQuery;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
            
            FLUSH PRIVILEGES;
            
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('User "', p_User, '" added successfully');
            COMMIT;
        ELSE
            SET p_Status = -3;  -- Business logic error
            SET p_ErrorMsg = 'Failed to add user';
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: usr_users_Delete_User
-- ========================================

DROP PROCEDURE IF EXISTS `usr_users_Delete_User`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Delete_User`(
    IN p_User VARCHAR(100),
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
    
    -- Validate parameter
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        -- Drop MySQL user first
        SET @d := CONCAT('DROP USER IF EXISTS \'', REPLACE(p_User, '\'', '\\\''), '\'@\'%\';');
        PREPARE stmt FROM @d;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
        
        -- Delete user from table
        DELETE FROM usr_users WHERE `User` = p_User;
        
        -- Check if delete was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('User "', p_User, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: usr_users_Exists
-- ========================================

DROP PROCEDURE IF EXISTS `usr_users_Exists`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Exists`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate parameter
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        -- Check if user exists
        SELECT COUNT(*) AS UserExists INTO v_Count
        FROM usr_users 
        WHERE `User` = p_User;
        
        -- Return result
        SELECT v_Count AS UserExists;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data (user exists)
            SET p_ErrorMsg = CONCAT('User "', p_User, '" exists');
        ELSE
            SET p_Status = 0;  -- Success but no data (user does not exist)
            SET p_ErrorMsg = CONCAT('User "', p_User, '" does not exist');
        END IF;
        
        COMMIT;
    END IF;
END$$


-- ========================================
-- Procedure: usr_users_Get_All
-- ========================================

DROP PROCEDURE IF EXISTS `usr_users_Get_All`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Get all users
    SELECT * FROM usr_users
    ORDER BY `User`;
    
    -- Check if data was returned
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' user(s)');
    ELSE
        SET p_Status = 0;  -- Success but no data
        SET p_ErrorMsg = 'No users found';
    END IF;
    
    COMMIT;
END$$


-- ========================================
-- Procedure: usr_users_Get_ByUser
-- ========================================

DROP PROCEDURE IF EXISTS `usr_users_Get_ByUser`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Get_ByUser`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate parameter
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        -- Get user by username
        SELECT * FROM usr_users 
        WHERE `User` = p_User;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved user "', p_User, '"');
            COMMIT;
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            COMMIT;
        END IF;
    END IF;
END$$


-- ========================================
-- Procedure: usr_users_Update_User
-- ========================================

DROP PROCEDURE IF EXISTS `usr_users_Update_User`;

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Update_User`(
    IN p_User VARCHAR(100),
    IN p_FullName VARCHAR(200),
    IN p_Shift VARCHAR(50),
    IN p_Pin VARCHAR(50),
    IN p_VisualUserName VARCHAR(50),
    IN p_VisualPassword VARCHAR(50),
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
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        -- Update user
        UPDATE usr_users SET
            `Full Name` = p_FullName,
            `Shift` = p_Shift,
            `Pin` = p_Pin,
            `VisualUserName` = p_VisualUserName,
            `VisualPassword` = p_VisualPassword
        WHERE `User` = p_User;
        
        -- Check if update was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('User "', p_User, '" updated successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$


DELIMITER ;

-- ========================================
-- Import Complete: 73 procedures
-- ========================================
