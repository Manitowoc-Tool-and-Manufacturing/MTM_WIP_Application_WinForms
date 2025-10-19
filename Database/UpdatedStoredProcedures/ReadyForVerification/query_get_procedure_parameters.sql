DELIMITER $$

DROP PROCEDURE IF EXISTS `query_get_procedure_parameters`$$

CREATE PROCEDURE `query_get_procedure_parameters`(
    IN p_ProcedureName VARCHAR(255)
)
BEGIN
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
END$$

DELIMITER ;
