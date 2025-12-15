DELIMITER $$
DROP PROCEDURE IF EXISTS `md_devtools_TruncateLogs`$$
CREATE PROCEDURE `md_devtools_TruncateLogs`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    TRUNCATE TABLE log_error;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$
DELIMITER ;
