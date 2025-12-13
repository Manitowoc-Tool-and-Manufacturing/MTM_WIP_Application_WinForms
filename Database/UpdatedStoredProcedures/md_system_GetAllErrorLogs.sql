DELIMITER $$

DROP PROCEDURE IF EXISTS `md_system_GetAllErrorLogs`$$

CREATE PROCEDURE `md_system_GetAllErrorLogs`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT * FROM log_error;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;