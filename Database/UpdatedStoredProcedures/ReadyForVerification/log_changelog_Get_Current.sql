-- ============================================================================
-- Procedure: log_changelog_Get_Current
-- Description: Gets the most recent changelog entry for version display
-- Parameters:
--   OUT p_Status INT           - Status code (0=success, 1=no data, -1=error)
--   OUT p_ErrorMsg VARCHAR(500) - Error message if any
-- Returns: DataTable with single row containing Version and Notes
-- ============================================================================

DROP PROCEDURE IF EXISTS log_changelog_Get_Current;

DELIMITER $$

CREATE PROCEDURE log_changelog_Get_Current(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while retrieving current changelog';
        ROLLBACK;
    END;

    -- Check if any changelog entries exist
    IF (SELECT COUNT(*) FROM log_changelog) = 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = 'No changelog entries found';
        SELECT 
            '0.0.0.0' AS Version,
            'No version information available' AS Notes,
            NULL AS Date
        LIMIT 0; -- Return empty result set with structure
    ELSE
        -- Return most recent changelog entry
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
END$$

DELIMITER ;
