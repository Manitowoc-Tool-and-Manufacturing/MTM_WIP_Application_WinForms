DELIMITER $$

DROP PROCEDURE IF EXISTS `query_get_all_stored_procedures`$$

CREATE PROCEDURE `query_get_all_stored_procedures`()
BEGIN
    -- Returns list of all stored procedures in current database
    SELECT
        ROUTINE_NAME
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_SCHEMA = DATABASE()
      AND ROUTINE_TYPE = 'PROCEDURE'
    ORDER BY ROUTINE_NAME;
END$$

DELIMITER ;
