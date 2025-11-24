/*!50003 DROP PROCEDURE IF EXISTS `md_item_types_Add_ItemType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Add_ItemType`(IN `p_ItemType` VARCHAR(100), IN `p_IssuedBy` VARCHAR(100), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_ItemType IS NULL OR TRIM(p_ItemType) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ItemType is required';
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
    ELSE
        INSERT INTO `md_item_types` (`ItemType`, `IssuedBy`)
        VALUES (p_ItemType, p_IssuedBy);
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Item type "', p_ItemType, '" added successfully');
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to add item type';
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

