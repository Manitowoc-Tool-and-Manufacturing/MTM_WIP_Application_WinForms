/*!50003 DROP PROCEDURE IF EXISTS `inv_inventory_Get_ByUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_inventory_Get_ByUser`(IN `p_User` VARCHAR(100), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving inventory by User';
    END;
    SELECT * FROM inv_inventory
    WHERE (p_User IS NULL OR p_User = '' OR User = p_User)
    ORDER BY LastUpdated DESC;
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No inventory records found';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

