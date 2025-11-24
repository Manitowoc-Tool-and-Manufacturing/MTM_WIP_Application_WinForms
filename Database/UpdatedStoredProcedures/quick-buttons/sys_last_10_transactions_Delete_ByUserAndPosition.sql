/*!50003 DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Delete_ByUserAndPosition` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Delete_ByUserAndPosition`(IN `p_User` VARCHAR(100), IN `p_Position` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSEIF p_Position IS NULL OR p_Position < 1 OR p_Position > 10 THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Position must be between 1 and 10';
    ELSE
        -- Delete the quick button at the specified position
        DELETE FROM sys_last_10_transactions
        WHERE User = p_User AND Position = p_Position;
        
        SET v_RowsAffected = ROW_COUNT();
        
        -- Shift remaining buttons down
        UPDATE sys_last_10_transactions
        SET Position = Position - 1
        WHERE User = p_User AND Position > p_Position
        ORDER BY Position ASC;
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Deleted quick button at position ', p_Position, ' and shifted remaining buttons');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No quick button found at position ', p_Position, ' for user "', p_User, '"');
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

