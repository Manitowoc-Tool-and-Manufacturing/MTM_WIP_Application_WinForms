/*!50003 DROP PROCEDURE IF EXISTS `sys_user_roles_Get_ById` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_roles_Get_ById`(IN `p_UserID` INT, OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    
    IF p_UserID IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserID is required';
    ELSE
        
        SELECT RoleID, UserID, AssignedBy, AssignedAt
        FROM sys_user_roles
        WHERE UserID = p_UserID
        LIMIT 1;
        
        
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved role for user ID ', p_UserID);
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No role found for user ID ', p_UserID);
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

