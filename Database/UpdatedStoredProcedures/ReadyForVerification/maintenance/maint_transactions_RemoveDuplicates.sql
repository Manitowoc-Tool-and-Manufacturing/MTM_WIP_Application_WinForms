DELIMITER //
DROP PROCEDURE IF EXISTS `maint_transactions_RemoveDuplicates`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `maint_transactions_RemoveDuplicates`(
    IN p_DuplicateType VARCHAR(50),  -- 'EXACT', 'SAME_TIMESTAMP', 'ALL'
    IN p_CreateBackup BOOLEAN,       -- TRUE to create backup table, FALSE to skip
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- ========================================================================
    -- Procedure: maint_transactions_RemoveDuplicates
    -- Purpose: Remove duplicate transaction records based on specified criteria
    -- 
    -- Parameters:
    --   p_DuplicateType: Type of duplicates to remove
    --     'EXACT' - Exact duplicates (all fields including timestamp match)
    --     'SAME_TIMESTAMP' - OUT transactions with same timestamp as IN
    --     'ALL' - All types of duplicates
    --   p_CreateBackup: TRUE to create backup table before deletion
    --   p_Status: 1=Success with deletions, 0=Success no duplicates, -1=Error
    --   p_ErrorMsg: Error or success message with count
    --
    -- Safety: Always keeps the lowest ID for exact duplicates
    --         Creates backup table if p_CreateBackup = TRUE
    -- ========================================================================
    
    DECLARE v_DeletedCount INT DEFAULT 0;
    DECLARE v_BackupTableName VARCHAR(100);
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
        ROLLBACK;
    END;
    
    -- Validate input
    IF p_DuplicateType IS NULL OR p_DuplicateType = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'DuplicateType is required (EXACT, SAME_TIMESTAMP, or ALL)';
    ELSEIF p_DuplicateType NOT IN ('EXACT', 'SAME_TIMESTAMP', 'ALL') THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid DuplicateType. Must be EXACT, SAME_TIMESTAMP, or ALL';
    ELSE
        -- Start transaction
        START TRANSACTION;
        
        -- Create backup table if requested
        IF p_CreateBackup THEN
            SET v_BackupTableName = CONCAT('inv_transaction_backup_', 
                                           LOWER(p_DuplicateType), '_',
                                           DATE_FORMAT(NOW(), '%Y%m%d_%H%i%s'));
            
            SET @sql = CONCAT('DROP TABLE IF EXISTS ', v_BackupTableName);
            PREPARE stmt FROM @sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
            
            SET @sql = CONCAT('CREATE TABLE ', v_BackupTableName, ' LIKE inv_transaction');
            PREPARE stmt FROM @sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
        END IF;
        
        -- Remove duplicates based on type
        IF p_DuplicateType = 'EXACT' THEN
            -- Backup exact duplicates if requested
            IF p_CreateBackup THEN
                SET @sql = CONCAT(
                    'INSERT INTO ', v_BackupTableName, ' ',
                    'SELECT t.* FROM inv_transaction t ',
                    'WHERE EXISTS (',
                    '    SELECT 1 FROM inv_transaction t2 ',
                    '    WHERE t2.ID < t.ID ',
                    '    AND t2.BatchNumber <=> t.BatchNumber ',
                    '    AND t2.PartID = t.PartID ',
                    '    AND t2.TransactionType = t.TransactionType ',
                    '    AND COALESCE(t2.FromLocation, '''') = COALESCE(t.FromLocation, '''') ',
                    '    AND COALESCE(t2.ToLocation, '''') = COALESCE(t.ToLocation, '''') ',
                    '    AND t2.Operation = t.Operation ',
                    '    AND t2.Quantity = t.Quantity ',
                    '    AND t2.User = t.User ',
                    '    AND t2.ReceiveDate = t.ReceiveDate',
                    ')'
                );
                PREPARE stmt FROM @sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            END IF;
            
            -- Create temp table with IDs to delete (keep lowest ID)
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
            CREATE TEMPORARY TABLE temp_ids_to_delete AS
            SELECT t.ID
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
            
            -- Delete exact duplicates
            DELETE FROM inv_transaction
            WHERE ID IN (SELECT ID FROM temp_ids_to_delete);
            
            SET v_DeletedCount = ROW_COUNT();
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
            
        ELSEIF p_DuplicateType = 'SAME_TIMESTAMP' THEN
            -- Backup same-timestamp duplicates if requested
            IF p_CreateBackup THEN
                SET @sql = CONCAT(
                    'INSERT INTO ', v_BackupTableName, ' ',
                    'SELECT t1.* FROM inv_transaction t1 ',
                    'WHERE t1.TransactionType = ''OUT'' ',
                    'AND EXISTS (',
                    '    SELECT 1 FROM inv_transaction t2 ',
                    '    WHERE t2.BatchNumber = t1.BatchNumber ',
                    '    AND t2.TransactionType = ''IN'' ',
                    '    AND t2.ReceiveDate = t1.ReceiveDate',
                    ')'
                );
                PREPARE stmt FROM @sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            END IF;
            
            -- Create temp table with same-timestamp OUT IDs
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
            CREATE TEMPORARY TABLE temp_ids_to_delete AS
            SELECT t1.ID
            FROM inv_transaction t1
            WHERE t1.TransactionType = 'OUT'
            AND EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.BatchNumber = t1.BatchNumber
                AND t2.TransactionType = 'IN'
                AND t2.ReceiveDate = t1.ReceiveDate
            );
            
            -- Delete same-timestamp OUT transactions
            DELETE FROM inv_transaction
            WHERE ID IN (SELECT ID FROM temp_ids_to_delete);
            
            SET v_DeletedCount = ROW_COUNT();
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
            
        ELSE -- 'ALL'
            -- Backup all duplicates if requested
            IF p_CreateBackup THEN
                -- Backup exact duplicates
                SET @sql = CONCAT(
                    'INSERT INTO ', v_BackupTableName, ' ',
                    'SELECT t.* FROM inv_transaction t ',
                    'WHERE EXISTS (',
                    '    SELECT 1 FROM inv_transaction t2 ',
                    '    WHERE t2.ID < t.ID ',
                    '    AND t2.BatchNumber <=> t.BatchNumber ',
                    '    AND t2.PartID = t.PartID ',
                    '    AND t2.TransactionType = t.TransactionType ',
                    '    AND COALESCE(t2.FromLocation, '''') = COALESCE(t.FromLocation, '''') ',
                    '    AND COALESCE(t2.ToLocation, '''') = COALESCE(t.ToLocation, '''') ',
                    '    AND t2.Operation = t.Operation ',
                    '    AND t2.Quantity = t.Quantity ',
                    '    AND t2.User = t.User ',
                    '    AND t2.ReceiveDate = t.ReceiveDate',
                    ')'
                );
                PREPARE stmt FROM @sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                
                -- Backup same-timestamp OUT duplicates
                SET @sql = CONCAT(
                    'INSERT INTO ', v_BackupTableName, ' ',
                    'SELECT t1.* FROM inv_transaction t1 ',
                    'WHERE t1.TransactionType = ''OUT'' ',
                    'AND EXISTS (',
                    '    SELECT 1 FROM inv_transaction t2 ',
                    '    WHERE t2.BatchNumber = t1.BatchNumber ',
                    '    AND t2.TransactionType = ''IN'' ',
                    '    AND t2.ReceiveDate = t1.ReceiveDate',
                    ')'
                );
                PREPARE stmt FROM @sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
            END IF;
            
            -- Delete exact duplicates first
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
            CREATE TEMPORARY TABLE temp_ids_to_delete AS
            SELECT t.ID
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
            
            DELETE FROM inv_transaction
            WHERE ID IN (SELECT ID FROM temp_ids_to_delete);
            
            SET v_DeletedCount = ROW_COUNT();
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
            
            -- Delete same-timestamp OUT transactions
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
            CREATE TEMPORARY TABLE temp_ids_to_delete AS
            SELECT t1.ID
            FROM inv_transaction t1
            WHERE t1.TransactionType = 'OUT'
            AND EXISTS (
                SELECT 1 FROM inv_transaction t2
                WHERE t2.BatchNumber = t1.BatchNumber
                AND t2.TransactionType = 'IN'
                AND t2.ReceiveDate = t1.ReceiveDate
            );
            
            DELETE FROM inv_transaction
            WHERE ID IN (SELECT ID FROM temp_ids_to_delete);
            
            SET v_DeletedCount = v_DeletedCount + ROW_COUNT();
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
        END IF;
        
        -- Commit transaction
        COMMIT;
        
        -- Set success status
        IF v_DeletedCount > 0 THEN
            SET p_Status = 1;
            IF p_CreateBackup THEN
                SET p_ErrorMsg = CONCAT('Successfully removed ', v_DeletedCount, 
                                       ' duplicate(s). Backup: ', v_BackupTableName);
            ELSE
                SET p_ErrorMsg = CONCAT('Successfully removed ', v_DeletedCount, ' duplicate(s).');
            END IF;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = 'No duplicates found to remove.';
        END IF;
    END IF;
END
//
DELIMITER ;
