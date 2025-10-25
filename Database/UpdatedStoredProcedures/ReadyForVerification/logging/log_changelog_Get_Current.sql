DELIMITER //
DROP PROCEDURE IF EXISTS `log_changelog_Get_Current`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `log_changelog_Get_Current`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
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
            -- Note: Table structure may not have Date column in all databases
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
END
//
DELIMITER ;
