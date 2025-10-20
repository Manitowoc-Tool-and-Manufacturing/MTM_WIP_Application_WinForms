DELIMITER $$

DROP PROCEDURE IF EXISTS `query_check_procedure_exists`$$

CREATE PROCEDURE `query_check_procedure_exists`(
    IN p_ProcedureName VARCHAR(255),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;

    -- Initialize output parameters
    SET p_Status = 0;
    SET p_ErrorMsg = '';

    BEGIN
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n        DECLARE EXIT HANDLER FOR SQLEXCEPTION
        BEGIN
            SET p_Status = -1;
            SET p_ErrorMsg = 'Error checking if procedure exists';
        END;
        SELECT COUNT(*) INTO v_Count
        FROM INFORMATION_SCHEMA.ROUTINES
        WHERE ROUTINE_SCHEMA = DATABASE()
          AND ROUTINE_TYPE = 'PROCEDURE'
          AND ROUTINE_NAME = p_ProcedureName;

        -- Return result set with count
        SELECT v_Count AS ProcedureCount;
        IF v_Count > 0 THEN
            SET p_Status = 0;
            SET p_ErrorMsg = 'Procedure exists';
        ELSE
            SET p_Status = 1;
            SET p_ErrorMsg = 'Procedure not found';
        END IF;
    END;
END$$

DELIMITER ;
