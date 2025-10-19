DELIMITER $$

DROP PROCEDURE IF EXISTS `query_get_all_stored_procedures`$$

CREATE PROCEDURE `query_get_all_stored_procedures`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;

    START TRANSACTION;

    -- Returns list of all stored procedures in current database
    SELECT
        ROUTINE_NAME
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_SCHEMA = DATABASE()
      AND ROUTINE_TYPE = 'PROCEDURE'
    ORDER BY ROUTINE_NAME;

    COMMIT;
    SET p_Status = 1;
    SET p_ErrorMsg = 'Query executed successfully';
END$$

DELIMITER ;
