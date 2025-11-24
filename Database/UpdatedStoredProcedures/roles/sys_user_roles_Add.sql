/*!50003 DROP PROCEDURE IF EXISTS `sys_user_roles_Add` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_roles_Add`(IN `p_UserID` INT, IN `p_RoleID` INT, IN `p_AssignedBy` VARCHAR(100), OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE v_UserExists INT DEFAULT 0;
    DECLARE v_RoleExists INT DEFAULT 0;
    DECLARE v_DuplicateExists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_UserID IS NULL OR p_UserID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid user ID is required';
    ELSEIF p_RoleID IS NULL OR p_RoleID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid role ID is required';
    ELSEIF p_AssignedBy IS NULL OR TRIM(p_AssignedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Assigned by user is required';
    ELSE
        SELECT COUNT(*) INTO v_UserExists FROM usr_users WHERE ID = p_UserID;
        IF v_UserExists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User ID ', p_UserID, ' not found');
        ELSE
            SELECT COUNT(*) INTO v_RoleExists FROM sys_roles WHERE ID = p_RoleID;
            IF v_RoleExists = 0 THEN
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('Role ID ', p_RoleID, ' not found');
            ELSE
                SELECT COUNT(*) INTO v_DuplicateExists
                FROM sys_user_roles
                WHERE UserID = p_UserID AND RoleID = p_RoleID;
                IF v_DuplicateExists > 0 THEN
                    SET p_Status = -5;
                    SET p_ErrorMsg = 'User already has this role assigned';
                ELSE
                    INSERT INTO sys_user_roles (UserID, RoleID, AssignedBy)
                    VALUES (p_UserID, p_RoleID, p_AssignedBy);
                    SET v_RowCount = ROW_COUNT();
                    IF v_RowCount > 0 THEN
                        SET p_Status = 1;
                        SET p_ErrorMsg = CONCAT('Role ', p_RoleID, ' assigned to user ', p_UserID);
                    ELSE
                        SET p_Status = -3;
                        SET p_ErrorMsg = 'Failed to assign role';
                    END IF;
                END IF;
            END IF;
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

