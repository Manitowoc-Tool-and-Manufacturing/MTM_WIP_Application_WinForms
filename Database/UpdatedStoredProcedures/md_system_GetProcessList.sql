DELIMITER $$

DROP PROCEDURE IF EXISTS `md_system_GetProcessList`$$

CREATE PROCEDURE `md_system_GetProcessList`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SHOW PROCESSLIST;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;