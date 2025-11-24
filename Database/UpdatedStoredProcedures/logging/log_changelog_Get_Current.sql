/*!50003 DROP PROCEDURE IF EXISTS `log_changelog_Get_Current` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `log_changelog_Get_Current`(OUT `p_Status` INT, OUT `p_ErrorMsg` VARCHAR(500))
BEGIN
    DECLARE table_exists INT DEFAULT 0;
    DECLARE row_count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving current changelog';
    END;
    
    -- Check if log_changelog table exists
    SELECT COUNT(*)
    INTO table_exists
    FROM information_schema.TABLES
    WHERE TABLE_SCHEMA = DATABASE()
    AND TABLE_NAME = 'log_changelog';
    
    IF table_exists = 0 THEN
        -- Table doesn't exist yet - return empty result with status 0
        SET p_Status = 0;
        SET p_ErrorMsg = 'Changelog table does not exist yet';
        SELECT
            '0.0.0.0' AS Version,
            'No version information available' AS Notes,
            NULL AS Date
        LIMIT 0;
    ELSE
        -- Table exists - check for rows
        SELECT COUNT(*) INTO row_count FROM log_changelog;
        
        IF row_count = 0 THEN
            -- Table exists but no entries
            SET p_Status = 0;
            SET p_ErrorMsg = 'No changelog entries found';
            SELECT
                '0.0.0.0' AS Version,
                'No version information available' AS Notes,
                NULL AS Date
            LIMIT 0;
        ELSE
            -- Return most recent changelog entry
            -- Note: Table doesn't have Date column, so we just return the first row
            SELECT
                Version,
                Notes,
                NULL AS Date
            FROM log_changelog
            LIMIT 1;
            SET p_Status = 1;
            SET p_ErrorMsg = 'Retrieved current changelog version';
        END IF;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

