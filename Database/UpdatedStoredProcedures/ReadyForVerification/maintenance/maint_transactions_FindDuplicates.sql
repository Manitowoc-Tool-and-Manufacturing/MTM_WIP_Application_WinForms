DELIMITER //
DROP PROCEDURE IF EXISTS `maint_transactions_FindDuplicates`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `maint_transactions_FindDuplicates`(
    IN p_DuplicateType VARCHAR(50),  -- 'EXACT', 'SAME_TIMESTAMP', 'ALL'
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- ========================================================================
    -- Procedure: maint_transactions_FindDuplicates
    -- Purpose: Find duplicate transaction records based on specified criteria
    -- 
    -- Parameters:
    --   p_DuplicateType: Type of duplicates to find
    --     'EXACT' - Exact duplicates (all fields including timestamp match)
    --     'SAME_TIMESTAMP' - OUT transactions with same timestamp as IN
    --     'ALL' - All types of duplicates
    --   p_Status: 1=Success, -1=Error
    --   p_ErrorMsg: Error or success message
    --
    -- Returns: Result set with duplicate transaction details
    -- ========================================================================
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    
    -- Validate input
    IF p_DuplicateType IS NULL OR p_DuplicateType = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'DuplicateType is required (EXACT, SAME_TIMESTAMP, or ALL)';
        SELECT p_Status AS Status, p_ErrorMsg AS ErrorMsg;
    ELSEIF p_DuplicateType NOT IN ('EXACT', 'SAME_TIMESTAMP', 'ALL') THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid DuplicateType. Must be EXACT, SAME_TIMESTAMP, or ALL';
        SELECT p_Status AS Status, p_ErrorMsg AS ErrorMsg;
    ELSE
        -- Find duplicates based on type
        IF p_DuplicateType = 'EXACT' THEN
            -- Exact duplicates: All fields including timestamp match
            SELECT 
                'EXACT_DUPLICATE' AS DuplicateType,
                t.ID,
                t.BatchNumber,
                t.PartID,
                t.TransactionType,
                t.FromLocation,
                t.ToLocation,
                t.Operation,
                t.Quantity,
                t.User,
                t.ReceiveDate,
                t.ItemType,
                t.Notes,
                (SELECT MIN(t2.ID) 
                 FROM inv_transaction t2
                 WHERE t2.BatchNumber <=> t.BatchNumber
                 AND t2.PartID = t.PartID
                 AND t2.TransactionType = t.TransactionType
                 AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
                 AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
                 AND t2.Operation = t.Operation
                 AND t2.Quantity = t.Quantity
                 AND t2.User = t.User
                 AND t2.ReceiveDate = t.ReceiveDate
                ) AS MinID_ToKeep,
                'DELETE' AS Action
            FROM inv_transaction t
            WHERE EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.ID < t.ID
                AND t2.BatchNumber <=> t.BatchNumber
                AND t2.PartID = t.PartID
                AND t2.TransactionType = t.TransactionType
                AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
                AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
                AND t2.Operation = t.Operation
                AND t2.Quantity = t.Quantity
                AND t2.User = t.User
                AND t2.ReceiveDate = t.ReceiveDate
            )
            ORDER BY t.BatchNumber, t.ID;
            
        ELSEIF p_DuplicateType = 'SAME_TIMESTAMP' THEN
            -- Same-timestamp duplicates: OUT with same timestamp as IN
            SELECT 
                'SAME_TIMESTAMP_OUT' AS DuplicateType,
                t1.ID,
                t1.BatchNumber,
                t1.PartID,
                t1.TransactionType,
                t1.FromLocation,
                t1.ToLocation,
                t1.Operation,
                t1.Quantity,
                t1.User,
                t1.ReceiveDate,
                t1.ItemType,
                t1.Notes,
                (SELECT t2.ID 
                 FROM inv_transaction t2
                 WHERE t2.BatchNumber = t1.BatchNumber
                 AND t2.TransactionType = 'IN'
                 AND t2.ReceiveDate = t1.ReceiveDate
                 LIMIT 1
                ) AS Matching_IN_ID,
                'DELETE' AS Action
            FROM inv_transaction t1
            WHERE t1.TransactionType = 'OUT'
            AND EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.BatchNumber = t1.BatchNumber
                AND t2.TransactionType = 'IN'
                AND t2.ReceiveDate = t1.ReceiveDate
            )
            ORDER BY t1.BatchNumber, t1.ID;
            
        ELSE -- 'ALL'
            -- Return all types of duplicates with type indicator
            
            -- Create temporary table for results
            DROP TEMPORARY TABLE IF EXISTS temp_all_duplicates;
            CREATE TEMPORARY TABLE temp_all_duplicates (
                DuplicateType VARCHAR(50),
                ID BIGINT,
                BatchNumber VARCHAR(20),
                PartID VARCHAR(100),
                TransactionType VARCHAR(20),
                FromLocation VARCHAR(100),
                ToLocation VARCHAR(100),
                Operation VARCHAR(100),
                Quantity INT,
                User VARCHAR(100),
                ReceiveDate DATETIME,
                ItemType VARCHAR(200),
                Notes VARCHAR(1000),
                Reference_ID BIGINT,
                Action VARCHAR(20)
            );
            
            -- Insert exact duplicates
            INSERT INTO temp_all_duplicates
            SELECT 
                'EXACT_DUPLICATE' AS DuplicateType,
                t.ID,
                t.BatchNumber,
                t.PartID,
                t.TransactionType,
                t.FromLocation,
                t.ToLocation,
                t.Operation,
                t.Quantity,
                t.User,
                t.ReceiveDate,
                t.ItemType,
                t.Notes,
                (SELECT MIN(t2.ID) 
                 FROM inv_transaction t2
                 WHERE t2.BatchNumber <=> t.BatchNumber
                 AND t2.PartID = t.PartID
                 AND t2.TransactionType = t.TransactionType
                 AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
                 AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
                 AND t2.Operation = t.Operation
                 AND t2.Quantity = t.Quantity
                 AND t2.User = t.User
                 AND t2.ReceiveDate = t.ReceiveDate
                ) AS Reference_ID,
                'DELETE' AS Action
            FROM inv_transaction t
            WHERE EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.ID < t.ID
                AND t2.BatchNumber <=> t.BatchNumber
                AND t2.PartID = t.PartID
                AND t2.TransactionType = t.TransactionType
                AND COALESCE(t2.FromLocation, '') = COALESCE(t.FromLocation, '')
                AND COALESCE(t2.ToLocation, '') = COALESCE(t.ToLocation, '')
                AND t2.Operation = t.Operation
                AND t2.Quantity = t.Quantity
                AND t2.User = t.User
                AND t2.ReceiveDate = t.ReceiveDate
            );
            
            -- Insert same-timestamp OUT duplicates
            INSERT INTO temp_all_duplicates
            SELECT 
                'SAME_TIMESTAMP_OUT' AS DuplicateType,
                t1.ID,
                t1.BatchNumber,
                t1.PartID,
                t1.TransactionType,
                t1.FromLocation,
                t1.ToLocation,
                t1.Operation,
                t1.Quantity,
                t1.User,
                t1.ReceiveDate,
                t1.ItemType,
                t1.Notes,
                (SELECT t2.ID 
                 FROM inv_transaction t2
                 WHERE t2.BatchNumber = t1.BatchNumber
                 AND t2.TransactionType = 'IN'
                 AND t2.ReceiveDate = t1.ReceiveDate
                 LIMIT 1
                ) AS Reference_ID,
                'DELETE' AS Action
            FROM inv_transaction t1
            WHERE t1.TransactionType = 'OUT'
            AND EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.BatchNumber = t1.BatchNumber
                AND t2.TransactionType = 'IN'
                AND t2.ReceiveDate = t1.ReceiveDate
            );
            
            -- Return combined results
            SELECT * FROM temp_all_duplicates
            ORDER BY DuplicateType, BatchNumber, ID;
            
            DROP TEMPORARY TABLE IF EXISTS temp_all_duplicates;
        END IF;
        
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Found duplicates of type: ', p_DuplicateType);
    END IF;
END
//
DELIMITER ;
