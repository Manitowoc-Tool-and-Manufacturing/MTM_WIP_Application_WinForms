DELIMITER $$

DROP PROCEDURE IF EXISTS `query_get_procedure_parameters`$$

CREATE PROCEDURE `query_get_procedure_parameters`(
    IN p_ProcedureName VARCHAR(255),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE,
            @errno = MYSQL_ERRNO,
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;
    -- Validate input
    IF p_ProcedureName IS NULL OR p_ProcedureName = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Procedure name is required';
    ELSE
        -- Returns list of input/inout parameters for a specific stored procedure
        SELECT 
            PARAMETER_NAME,
            PARAMETER_MODE,
            DATA_TYPE,
            ORDINAL_POSITION
        FROM INFORMATION_SCHEMA.PARAMETERS 
        WHERE SPECIFIC_SCHEMA = DATABASE() 
          AND SPECIFIC_NAME = p_ProcedureName
          AND PARAMETER_MODE IN ('IN', 'INOUT')
        ORDER BY ORDINAL_POSITION;

        SELECT COUNT(*) INTO v_Count
        FROM INFORMATION_SCHEMA.PARAMETERS 
        WHERE SPECIFIC_SCHEMA = DATABASE() 
          AND SPECIFIC_NAME = p_ProcedureName;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Found ', v_Count, ' parameters for procedure ', p_ProcedureName);
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No parameters found for procedure ', p_ProcedureName);
        END IF;
    END IF;
END$$

DELIMITER ;
