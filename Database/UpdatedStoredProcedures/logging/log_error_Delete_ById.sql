/*!50003 DROP PROCEDURE IF EXISTS `log_error_Delete_ById` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Delete_ById`(IN `p_Id` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_Id IS NULL OR p_Id <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid error log ID is required';
    ELSE
        SELECT COUNT(*) INTO v_Exists FROM `log_error` WHERE `ID` = p_Id;
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Error log entry with ID ', p_Id, ' not found');
        ELSE
            DELETE FROM `log_error` WHERE `ID` = p_Id;
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Error log entry with ID ', p_Id, ' deleted successfully');
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to delete error log entry';
            END IF;
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

