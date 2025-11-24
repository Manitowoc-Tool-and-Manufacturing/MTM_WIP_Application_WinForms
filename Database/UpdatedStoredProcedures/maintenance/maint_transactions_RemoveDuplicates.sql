/*!50003 DROP PROCEDURE IF EXISTS `maint_transactions_RemoveDuplicates` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `maint_transactions_RemoveDuplicates`(IN `p_DuplicateType` VARCHAR(50), IN `p_CreateBackup` BOOLEAN, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
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
    
    
    IF p_DuplicateType IS NULL OR p_DuplicateType = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'DuplicateType is required (EXACT, SAME_TIMESTAMP, or ALL)';
    ELSEIF p_DuplicateType NOT IN ('EXACT', 'SAME_TIMESTAMP', 'ALL') THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid DuplicateType. Must be EXACT, SAME_TIMESTAMP, or ALL';
    ELSE
        
        START TRANSACTION;
        
        
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
        
        
        IF p_DuplicateType = 'EXACT' THEN
            
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
            
        ELSEIF p_DuplicateType = 'SAME_TIMESTAMP' THEN
            
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
            
            SET v_DeletedCount = ROW_COUNT();
            DROP TEMPORARY TABLE IF EXISTS temp_ids_to_delete;
            
        ELSE 
            
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
        
        
        COMMIT;
        
        
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

