DELIMITER $$

DROP PROCEDURE IF EXISTS `md_system_CheckTableExists`$$

CREATE PROCEDURE `md_system_CheckTableExists`(
    IN p_TableName VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT COUNT(*) > 0 as `Exists`
    FROM information_schema.TABLES
    WHERE table_schema = DATABASE() AND table_name = p_TableName;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;
