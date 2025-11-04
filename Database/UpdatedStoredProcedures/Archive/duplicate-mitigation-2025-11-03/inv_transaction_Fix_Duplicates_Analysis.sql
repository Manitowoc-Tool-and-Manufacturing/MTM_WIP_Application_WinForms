-- =============================================
-- Procedure: inv_transaction_Fix_Duplicates_Analysis
-- Domain: inventory
-- Created: 2025-11-03
-- Purpose: Analyze and fix duplicate transaction entries that cause quantity mismatches
-- =============================================
-- ROOT CAUSE ANALYSIS:
--   Issue: Multiple identical transaction records exist with same BatchNumber, PartID, 
--          TransactionType, Location, Quantity, User, and Timestamp
--   Impact: Causes OUT quantities to exceed IN quantities (data integrity violation)
--   Example: Batch 0000010441 has 4 identical OUT transactions (should be 1)
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_transaction_Fix_Duplicates_Analysis`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transaction_Fix_Duplicates_Analysis`(
    IN p_DryRun BOOLEAN,
    IN p_DeleteDuplicates BOOLEAN,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_DuplicateGroups INT DEFAULT 0;
    DECLARE v_TotalDuplicates INT DEFAULT 0;
    DECLARE v_DuplicatesDeleted INT DEFAULT 0;
    
    -- Error handler
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    -- Start transaction for atomic operation
    START TRANSACTION;
    
    -- Create temporary table to identify duplicates (MySQL 5.7 compatible)
    DROP TEMPORARY TABLE IF EXISTS temp_duplicates;
    CREATE TEMPORARY TABLE temp_duplicates (
        ID INT PRIMARY KEY,
        BatchNumber VARCHAR(50),
        PartID VARCHAR(100),
        TransactionType VARCHAR(50),
        FromLocation VARCHAR(100),
        ToLocation VARCHAR(100),
        Operation VARCHAR(100),
        Quantity INT,
        User VARCHAR(100),
        ReceiveDate DATETIME,
        MinIDInGroup INT,
        IsKeep BOOLEAN DEFAULT FALSE,
        INDEX idx_group (BatchNumber, PartID, TransactionType, FromLocation, ToLocation, Operation, Quantity, User, ReceiveDate)
    );
    
    -- Step 1: Insert all duplicate transactions
    INSERT INTO temp_duplicates (ID, BatchNumber, PartID, TransactionType, FromLocation, 
                                  ToLocation, Operation, Quantity, User, ReceiveDate)
    SELECT 
        t.ID,
        t.BatchNumber,
        t.PartID,
        t.TransactionType,
        t.FromLocation,
        t.ToLocation,
        t.Operation,
        t.Quantity,
        t.User,
        t.ReceiveDate
    FROM inv_transaction t
    WHERE (t.BatchNumber, t.PartID, t.TransactionType, 
           COALESCE(t.FromLocation, ''), COALESCE(t.ToLocation, ''), 
           t.Operation, t.Quantity, t.User, t.ReceiveDate) IN (
        SELECT BatchNumber, PartID, TransactionType, 
               COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
               Operation, Quantity, User, ReceiveDate
        FROM inv_transaction
        GROUP BY BatchNumber, PartID, TransactionType, 
                 COALESCE(FromLocation, ''), COALESCE(ToLocation, ''), 
                 Operation, Quantity, User, ReceiveDate
        HAVING COUNT(*) > 1
    );
    
    -- Step 2: Find the minimum ID for each duplicate group (this is the one to keep)
    UPDATE temp_duplicates td
    INNER JOIN (
        SELECT 
            BatchNumber, PartID, TransactionType,
            COALESCE(FromLocation, '') AS FromLoc,
            COALESCE(ToLocation, '') AS ToLoc,
            Operation, Quantity, User, ReceiveDate,
            MIN(ID) AS MinID
        FROM temp_duplicates
        GROUP BY BatchNumber, PartID, TransactionType,
                 COALESCE(FromLocation, ''), COALESCE(ToLocation, ''),
                 Operation, Quantity, User, ReceiveDate
    ) grp ON td.BatchNumber = grp.BatchNumber
         AND td.PartID = grp.PartID
         AND td.TransactionType = grp.TransactionType
         AND COALESCE(td.FromLocation, '') = grp.FromLoc
         AND COALESCE(td.ToLocation, '') = grp.ToLoc
         AND td.Operation = grp.Operation
         AND td.Quantity = grp.Quantity
         AND td.User = grp.User
         AND td.ReceiveDate = grp.ReceiveDate
    SET td.MinIDInGroup = grp.MinID;
    
    -- Step 3: Mark records to keep (lowest ID in each group)
    UPDATE temp_duplicates
    SET IsKeep = TRUE
    WHERE ID = MinIDInGroup;
    
    -- Count duplicate groups and total duplicates
    SELECT COUNT(*) INTO v_TotalDuplicates FROM temp_duplicates;
    SELECT COUNT(DISTINCT MinIDInGroup) INTO v_DuplicateGroups FROM temp_duplicates;
    
    IF p_DryRun THEN
        -- DRY RUN: Show analysis without making changes
        SELECT 
            'DUPLICATE_ANALYSIS' AS Report_Type,
            v_DuplicateGroups AS Total_Duplicate_Groups,
            v_TotalDuplicates AS Total_Duplicate_Records,
            (v_TotalDuplicates - v_DuplicateGroups) AS Records_To_Delete;
        
        -- Show sample duplicates
        SELECT 
            'SAMPLE_DUPLICATES' AS Sample_Type,
            BatchNumber,
            PartID,
            TransactionType,
            FromLocation,
            ToLocation,
            Quantity,
            User,
            ReceiveDate,
            COUNT(*) AS Duplicate_Count,
            GROUP_CONCAT(ID ORDER BY ID) AS Duplicate_IDs,
            MIN(CASE WHEN IsKeep THEN ID END) AS ID_To_Keep,
            GROUP_CONCAT(CASE WHEN NOT IsKeep THEN ID END ORDER BY ID) AS IDs_To_Delete
        FROM temp_duplicates
        GROUP BY BatchNumber, PartID, TransactionType, FromLocation, ToLocation, 
                 Operation, Quantity, User, ReceiveDate
        ORDER BY COUNT(*) DESC
        LIMIT 20;
        
        -- Show batches that will be fixed
        SELECT 
            'BATCHES_TO_FIX' AS Report_Type,
            t.BatchNumber,
            t.PartID,
            SUM(CASE WHEN t.TransactionType = 'IN' AND td.IsKeep THEN t.Quantity ELSE 0 END) AS Current_IN_Total,
            SUM(CASE WHEN t.TransactionType = 'OUT' AND td.IsKeep THEN t.Quantity ELSE 0 END) AS Current_OUT_Total,
            SUM(CASE WHEN t.TransactionType = 'IN' THEN t.Quantity ELSE 0 END) AS Before_Fix_IN_Total,
            SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity ELSE 0 END) AS Before_Fix_OUT_Total,
            (SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity ELSE 0 END) - 
             SUM(CASE WHEN t.TransactionType = 'OUT' AND td.IsKeep THEN t.Quantity ELSE 0 END)) AS Excess_OUT_Removed
        FROM inv_transaction t
        INNER JOIN temp_duplicates td ON t.ID = td.ID
        GROUP BY t.BatchNumber, t.PartID
        HAVING Excess_OUT_Removed > 0
        ORDER BY Excess_OUT_Removed DESC
        LIMIT 20;
        
        ROLLBACK;
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('DRY RUN: Found ', v_DuplicateGroups, ' duplicate groups containing ', 
                                v_TotalDuplicates, ' records. Would delete ', 
                                (v_TotalDuplicates - v_DuplicateGroups), ' duplicate records.');
        
    ELSE
        -- Create backup table if it doesn't exist
        CREATE TABLE IF NOT EXISTS inv_transaction_duplicates_backup_20251103 (
            ID INT PRIMARY KEY,
            TransactionType VARCHAR(50),
            BatchNumber VARCHAR(50),
            PartID VARCHAR(100),
            FromLocation VARCHAR(100),
            ToLocation VARCHAR(100),
            Operation VARCHAR(100),
            Quantity INT,
            Notes VARCHAR(1000),
            User VARCHAR(100),
            ItemType VARCHAR(200),
            ReceiveDate DATETIME,
            backup_date DATETIME DEFAULT CURRENT_TIMESTAMP,
            deletion_reason VARCHAR(255),
            INDEX idx_batch_part (BatchNumber, PartID)
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        
        IF p_DeleteDuplicates THEN
            -- ACTUAL DELETION: Backup duplicates, then delete
            INSERT INTO inv_transaction_duplicates_backup_20251103
                (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation,
                 Quantity, Notes, User, ItemType, ReceiveDate, deletion_reason)
            SELECT 
                t.ID, t.TransactionType, t.BatchNumber, t.PartID, t.FromLocation, t.ToLocation,
                t.Operation, t.Quantity, t.Notes, t.User, t.ItemType, t.ReceiveDate,
                CONCAT('Duplicate of ID ', td.ID, ' (kept)')
            FROM inv_transaction t
            INNER JOIN temp_duplicates td ON t.ID = td.ID
            WHERE td.IsKeep = FALSE;
            
            -- Delete duplicate records (keep first occurrence)
            DELETE t FROM inv_transaction t
            INNER JOIN temp_duplicates td ON t.ID = td.ID
            WHERE td.IsKeep = FALSE;
            
            SET v_DuplicatesDeleted = ROW_COUNT();
            
            COMMIT;
            
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT(
                'SUCCESS: Deleted ', v_DuplicatesDeleted, ' duplicate records from ', 
                v_DuplicateGroups, ' duplicate groups. ',
                'Backup table: inv_transaction_duplicates_backup_20251103'
            );
            
            -- Show results
            SELECT 
                'DELETION_COMPLETE' AS Status,
                v_DuplicateGroups AS Duplicate_Groups_Fixed,
                v_DuplicatesDeleted AS Records_Deleted,
                'inv_transaction_duplicates_backup_20251103' AS Backup_Table;
            
        ELSE
            -- ANALYSIS MODE: Just show what would be fixed
            ROLLBACK;
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('ANALYSIS: Found ', v_DuplicateGroups, ' duplicate groups. ',
                                    'Run with p_DeleteDuplicates=TRUE to fix.');
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- USAGE INSTRUCTIONS
-- =============================================
-- 
-- DRY RUN (Preview duplicates without making changes):
--   CALL inv_transaction_Fix_Duplicates_Analysis(TRUE, FALSE, @status, @msg);
--   SELECT @status AS Status, @msg AS Message;
-- 
-- ANALYSIS MODE (Identify duplicates, create backup, but don't delete):
--   CALL inv_transaction_Fix_Duplicates_Analysis(FALSE, FALSE, @status, @msg);
--   SELECT @status AS Status, @msg AS Message;
-- 
-- ACTUAL FIX (Delete duplicates after backup):
--   CALL inv_transaction_Fix_Duplicates_Analysis(FALSE, TRUE, @status, @msg);
--   SELECT @status AS Status, @msg AS Message;
-- 
-- RESTORE FROM BACKUP (If needed):
--   INSERT INTO inv_transaction 
--       (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, 
--        Operation, Quantity, Notes, User, ItemType, ReceiveDate)
--   SELECT 
--       ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation,
--       Operation, Quantity, Notes, User, ItemType, ReceiveDate
--   FROM inv_transaction_duplicates_backup_20251103
--   WHERE ID NOT IN (SELECT ID FROM inv_transaction);
-- 
-- VERIFY FIX:
--   SELECT COUNT(*) AS Remaining_Duplicates FROM (
--       SELECT BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
--              Operation, Quantity, User, ReceiveDate, COUNT(*) AS cnt
--       FROM inv_transaction
--       GROUP BY BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
--                Operation, Quantity, User, ReceiveDate
--       HAVING COUNT(*) > 1
--   ) sub;
-- 
-- =============================================
-- End of inv_transaction_Fix_Duplicates_Analysis
-- =============================================
