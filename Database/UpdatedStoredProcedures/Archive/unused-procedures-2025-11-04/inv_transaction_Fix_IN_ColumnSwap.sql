-- =============================================
-- Procedure: inv_transaction_Fix_IN_ColumnSwap
-- Domain: inventory
-- Created: 2025-11-03
-- Purpose: One-time migration to fix historical IN transactions with swapped FromLocation/ToLocation columns
--          Swaps FromLocation to ToLocation for all IN transactions where FromLocation is populated and ToLocation is NULL
-- =============================================
-- MIGRATION DETAILS:
--   Issue: All 6,317 IN transactions have location in FromLocation instead of ToLocation
--   Fix: Move FromLocation value to ToLocation, set FromLocation to NULL
--   Impact: Historical data correction for TransactionLifecycleForm compatibility
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_transaction_Fix_IN_ColumnSwap`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transaction_Fix_IN_ColumnSwap`(
    IN p_DryRun BOOLEAN,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_AffectedCount INT DEFAULT 0;
    DECLARE v_BeforeIncorrect INT DEFAULT 0;
    DECLARE v_AfterIncorrect INT DEFAULT 0;
    DECLARE v_AfterCorrect INT DEFAULT 0;
    
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
    
    -- Count affected records BEFORE migration
    SELECT COUNT(*) INTO v_BeforeIncorrect
    FROM inv_transaction
    WHERE TransactionType = 'IN'
      AND FromLocation IS NOT NULL
      AND ToLocation IS NULL;
    
    -- Create rollback table if it doesn't exist
    CREATE TABLE IF NOT EXISTS inv_transaction_rollback_20251103 (
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
        migration_backup_date DATETIME DEFAULT CURRENT_TIMESTAMP,
        INDEX idx_transaction_type (TransactionType),
        INDEX idx_batch_number (BatchNumber)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
    
    -- Backup affected records to rollback table (INSERT IGNORE to avoid duplicates if run multiple times)
    INSERT IGNORE INTO inv_transaction_rollback_20251103
        (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation, 
         Quantity, Notes, User, ItemType, ReceiveDate)
    SELECT 
        ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation,
        Quantity, Notes, User, ItemType, ReceiveDate
    FROM inv_transaction
    WHERE TransactionType = 'IN'
      AND FromLocation IS NOT NULL
      AND ToLocation IS NULL;
    
    -- Log to console
    SELECT CONCAT('Backed up ', ROW_COUNT(), ' records to rollback table') AS BackupStatus;
    
    IF p_DryRun THEN
        -- DRY RUN MODE: Show what WOULD change without actually changing it
        SELECT 
            'DRY_RUN_PREVIEW' AS Mode,
            ID,
            TransactionType,
            FromLocation AS 'Current_FromLocation_WRONG',
            ToLocation AS 'Current_ToLocation_WRONG',
            FromLocation AS 'Will_Become_ToLocation_CORRECT',
            NULL AS 'Will_Become_FromLocation_CORRECT',
            PartID,
            BatchNumber,
            ReceiveDate
        FROM inv_transaction
        WHERE TransactionType = 'IN'
          AND FromLocation IS NOT NULL
          AND ToLocation IS NULL
        ORDER BY ReceiveDate DESC
        LIMIT 20;
        
        -- Rollback dry run (no changes made)
        ROLLBACK;
        
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('DRY RUN: Would affect ', v_BeforeIncorrect, ' IN transactions. No changes made. Review preview results.');
        
    ELSE
        -- ACTUAL MIGRATION: Swap columns for IN transactions
        UPDATE inv_transaction
        SET
            ToLocation = FromLocation,    -- Move location to correct column
            FromLocation = NULL           -- Clear incorrect column
        WHERE
            TransactionType = 'IN'
            AND FromLocation IS NOT NULL
            AND ToLocation IS NULL;
        
        SET v_AffectedCount = ROW_COUNT();
        
        -- Validate migration results
        SELECT COUNT(*) INTO v_AfterIncorrect
        FROM inv_transaction
        WHERE TransactionType = 'IN'
          AND FromLocation IS NOT NULL
          AND ToLocation IS NULL;
        
        SELECT COUNT(*) INTO v_AfterCorrect
        FROM inv_transaction
        WHERE TransactionType = 'IN'
          AND FromLocation IS NULL
          AND ToLocation IS NOT NULL;
        
        -- Verify migration success
        IF v_AfterIncorrect = 0 AND v_AfterCorrect = v_BeforeIncorrect THEN
            -- Success: Commit transaction
            COMMIT;
            
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT(
                'SUCCESS: Migrated ', v_AffectedCount, ' IN transactions. ',
                'Before: ', v_BeforeIncorrect, ' incorrect. ',
                'After: ', v_AfterCorrect, ' correct, ', v_AfterIncorrect, ' incorrect. ',
                'Rollback table: inv_transaction_rollback_20251103'
            );
            
            -- Show sample of corrected records
            SELECT 
                'MIGRATION_SUCCESS' AS Status,
                ID,
                TransactionType,
                FromLocation AS 'FromLocation_Now_NULL',
                ToLocation AS 'ToLocation_Now_Populated',
                PartID,
                BatchNumber,
                ReceiveDate
            FROM inv_transaction
            WHERE TransactionType = 'IN'
            ORDER BY ReceiveDate DESC
            LIMIT 10;
            
        ELSE
            -- Validation failed: Rollback
            ROLLBACK;
            
            SET p_Status = -2;
            SET p_ErrorMsg = CONCAT(
                'VALIDATION FAILED: Expected 0 incorrect after migration, got ', v_AfterIncorrect, '. ',
                'Expected ', v_BeforeIncorrect, ' correct, got ', v_AfterCorrect, '. ',
                'Transaction rolled back. No changes made.'
            );
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- USAGE INSTRUCTIONS
-- =============================================
-- 
-- DRY RUN (Preview changes without modifying data):
--   CALL inv_transaction_Fix_IN_ColumnSwap(TRUE, @status, @msg);
--   SELECT @status AS Status, @msg AS Message;
-- 
-- ACTUAL MIGRATION (Fix historical data):
--   CALL inv_transaction_Fix_IN_ColumnSwap(FALSE, @status, @msg);
--   SELECT @status AS Status, @msg AS Message;
-- 
-- ROLLBACK (If needed):
--   START TRANSACTION;
--   UPDATE inv_transaction t
--   INNER JOIN inv_transaction_rollback_20251103 r ON t.ID = r.ID
--   SET t.FromLocation = r.FromLocation, t.ToLocation = r.ToLocation
--   WHERE t.TransactionType = 'IN';
--   COMMIT;
-- 
-- CHECK MIGRATION STATUS:
--   SELECT 
--       TransactionType,
--       COUNT(*) AS Total,
--       SUM(CASE WHEN FromLocation IS NULL AND ToLocation IS NOT NULL THEN 1 ELSE 0 END) AS Correct,
--       SUM(CASE WHEN FromLocation IS NOT NULL AND ToLocation IS NULL THEN 1 ELSE 0 END) AS Incorrect
--   FROM inv_transaction
--   WHERE TransactionType = 'IN'
--   GROUP BY TransactionType;
-- 
-- =============================================
-- End of inv_transaction_Fix_IN_ColumnSwap
-- =============================================
