DELIMITER $$

DROP PROCEDURE IF EXISTS `md_system_CheckColumnExists`$$

CREATE PROCEDURE `md_system_CheckColumnExists`(
    IN p_TableName VARCHAR(100),
    IN p_ColumnName VARCHAR(100),
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
    FROM information_schema.COLUMNS
    WHERE table_schema = DATABASE() AND table_name = p_TableName AND column_name = p_ColumnName;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;
