-- =============================================
-- Procedure: log_changelog_Get_Current
-- Domain: logging
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

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
        ROLLBACK;
    END;

    
    IF (SELECT COUNT(*) FROM log_changelog) = 0 THEN
        SET p_Status = 1;
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
        
        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
    END IF;
    
    COMMIT;
END
//

DELIMITER ;

-- =============================================
-- End of log_changelog_Get_Current
-- =============================================
