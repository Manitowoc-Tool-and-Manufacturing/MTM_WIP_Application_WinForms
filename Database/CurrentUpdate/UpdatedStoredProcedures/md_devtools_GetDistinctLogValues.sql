DELIMITER $$
DROP PROCEDURE IF EXISTS `md_devtools_GetDistinctLogValues`$$
CREATE PROCEDURE `md_devtools_GetDistinctLogValues`(
    IN p_Field VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    IF p_Field = 'Source' THEN
        SELECT DISTINCT ModuleName AS Value FROM log_error ORDER BY ModuleName;
    ELSEIF p_Field = 'User' THEN
        SELECT DISTINCT User AS Value FROM log_error ORDER BY User;
    ELSEIF p_Field = 'ErrorType' THEN
        SELECT DISTINCT ErrorType AS Value FROM log_error ORDER BY ErrorType;
    ELSEIF p_Field = 'Level' THEN
        SELECT DISTINCT Severity AS Value FROM log_error ORDER BY Severity;
    END IF;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$
DELIMITER ;
