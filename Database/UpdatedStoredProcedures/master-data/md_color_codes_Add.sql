/*!50003 DROP PROCEDURE IF EXISTS `md_color_codes_Add` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_color_codes_Add`(IN `p_ColorCode` VARCHAR(50), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE color_exists INT DEFAULT 0;
    
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error adding color code';
        ROLLBACK;
    END;

    
    IF p_ColorCode IS NULL OR p_ColorCode = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ColorCode is required';
    ELSE
        
        IF p_ColorCode IN ('Unknown', 'Other') THEN
            SET p_Status = -3;
            SET p_ErrorMsg = 'Cannot add reserved color codes';
        ELSE
            START TRANSACTION;

            
            SELECT COUNT(*) INTO color_exists
            FROM md_color_codes
            WHERE ColorCode = p_ColorCode;

            IF color_exists > 0 THEN
                
                SET p_Status = 0;
                SET p_ErrorMsg = NULL;
                COMMIT;
            ELSE
                
                INSERT INTO md_color_codes (ColorCode, IsUserDefined, CreatedDate)
                VALUES (p_ColorCode, TRUE, NOW());

                SET p_Status = 1;
                SET p_ErrorMsg = NULL;
                COMMIT;
            END IF;
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

