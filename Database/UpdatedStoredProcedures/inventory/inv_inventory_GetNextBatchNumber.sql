/*!50003 DROP PROCEDURE IF EXISTS `inv_inventory_GetNextBatchNumber` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_GetNextBatchNumber`(OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_NextBatch BIGINT;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Get and increment batch number
    SELECT last_batch_number INTO v_NextBatch 
    FROM inv_inventory_batch_seq;
    
    SET v_NextBatch = v_NextBatch + 1;
    
    -- Update sequence
    UPDATE inv_inventory_batch_seq 
    SET last_batch_number = v_NextBatch;
    
    -- Return the new batch number
    SELECT v_NextBatch AS NextBatchNumber;
    
    SET p_Status = 1;
    SET p_ErrorMsg = CONCAT('Generated batch number: ', v_NextBatch);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

