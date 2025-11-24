/*!50003 DROP PROCEDURE IF EXISTS `md_item_types_Exists_ByType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Exists_ByType`(IN `p_ItemType` VARCHAR(100), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    -- Validate input
    IF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
    ELSE
        -- Check if item type exists
        SELECT COUNT(*) INTO v_Exists
        FROM md_item_types
        WHERE ItemType = p_ItemType;

        IF v_Exists > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('ItemType "', p_ItemType, '" exists');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('ItemType "', p_ItemType, '" does not exist');
        END IF;
        
        -- Return existence value for ExecuteScalarAsync
        SELECT v_Exists;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

