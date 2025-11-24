/*!50003 DROP PROCEDURE IF EXISTS `sys_user_GetIdByName` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_GetIdByName`(IN `p_UserName` VARCHAR(100), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE p_UserId INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_UserName IS NULL OR TRIM(p_UserName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User name is required';
        SELECT 0 AS UserID;
    ELSE
        SELECT ID INTO p_UserId FROM usr_users WHERE User = p_UserName LIMIT 1;
        IF p_UserId IS NULL OR p_UserId = 0 THEN
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('User "', p_UserName, '" not found');
            SELECT 0 AS UserID;
        ELSE
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('User ID retrieved for "', p_UserName, '"');
            SELECT p_UserId AS UserID;
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

