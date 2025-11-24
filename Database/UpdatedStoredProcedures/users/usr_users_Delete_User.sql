/*!50003 DROP PROCEDURE IF EXISTS `usr_users_Delete_User` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Delete_User`(IN `p_User` VARCHAR(100), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE v_UserId INT DEFAULT 0;
    
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSE
        
        SELECT ID INTO v_UserId FROM usr_users WHERE `User` = p_User LIMIT 1;
        
        IF v_UserId IS NULL OR v_UserId = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
        ELSE
            
            DELETE FROM sys_user_roles WHERE UserID = v_UserId;
            DELETE FROM usr_settings WHERE UserId = p_User;
            
            
            DELETE FROM usr_users WHERE `User` = p_User;
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('User "', p_User, '" deleted successfully');
            ELSE
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            END IF;
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

