DELIMITER $$

DROP PROCEDURE IF EXISTS `md_system_CheckTable`$$

CREATE PROCEDURE `md_system_CheckTable`(
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

    SET @s = CONCAT('CHECK TABLE ', p_TableName);
    PREPARE stmt FROM @s;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;