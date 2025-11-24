/*!50003 DROP PROCEDURE IF EXISTS `sys_last_10_transactions_AddQuickButton` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_AddQuickButton`(IN `p_User` VARCHAR(255), IN `p_PartID` VARCHAR(255), IN `p_Operation` VARCHAR(255), IN `p_Quantity` INT, IN `p_Position` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSEIF p_PartID IS NULL OR TRIM(p_PartID) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Part ID is required';
    ELSEIF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
    ELSEIF p_Quantity IS NULL OR p_Quantity < 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid quantity is required';
    ELSEIF p_Position IS NULL OR p_Position < 1 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid position is required';
    ELSE
        UPDATE sys_last_10_transactions
        SET Position = Position + 1
        WHERE User = p_User AND Position >= p_Position;
        INSERT INTO sys_last_10_transactions (User, PartID, Operation, Quantity, Position)
        VALUES (p_User, p_PartID, p_Operation, p_Quantity, p_Position);
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Quick button added at position ', p_Position);
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to add quick button';
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

