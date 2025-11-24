/*!50003 DROP PROCEDURE IF EXISTS `inv_inventory_Fix_BatchNumbers` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Fix_BatchNumbers`(OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    DECLARE done INT DEFAULT FALSE;
    DECLARE null_id INT;
    DECLARE nextBatch BIGINT;
    DECLARE cur CURSOR FOR SELECT ID FROM inv_inventory WHERE BatchNumber IS NULL ORDER BY ID;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    SELECT last_batch_number INTO nextBatch FROM inv_inventory_batch_seq;
    OPEN cur;
    read_loop: LOOP
        FETCH cur INTO null_id;
        IF done THEN
            LEAVE read_loop;
        END IF;
        SET nextBatch = nextBatch + 1;
        UPDATE inv_inventory SET BatchNumber = LPAD(nextBatch, 10, '0') WHERE ID = null_id;
        SET v_RowsAffected = v_RowsAffected + ROW_COUNT();
        UPDATE inv_inventory_batch_seq SET last_batch_number = nextBatch;
    END LOOP;
    CLOSE cur;
    IF v_RowsAffected > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Successfully fixed ', v_RowsAffected, ' batch number(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No batch numbers needed fixing';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

