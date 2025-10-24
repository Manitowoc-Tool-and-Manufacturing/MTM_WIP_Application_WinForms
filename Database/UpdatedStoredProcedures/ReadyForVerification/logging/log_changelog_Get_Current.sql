DELIMITER //
DROP PROCEDURE IF EXISTS `log_changelog_Get_Current`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `log_changelog_Get_Current`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving current changelog';
    END;
    
    IF (SELECT COUNT(*) FROM log_changelog) = 0 THEN
        SET p_Status = 0;
        SET p_ErrorMsg = 'No changelog entries found';
        SELECT
            '0.0.0.0' AS Version,
            'No version information available' AS Notes,
            NULL AS Date
        LIMIT 0;
    ELSE
        SELECT
            Version,
            Notes,
            Date
        FROM log_changelog
        ORDER BY Date DESC
        LIMIT 1;
        SET p_Status = 1;
        SET p_ErrorMsg = 'Retrieved current changelog version';
    END IF;
END
//
DELIMITER ;
