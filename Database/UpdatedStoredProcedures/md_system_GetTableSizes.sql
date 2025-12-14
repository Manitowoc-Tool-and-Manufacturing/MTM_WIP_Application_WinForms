DELIMITER $$

DROP PROCEDURE IF EXISTS `md_system_GetTableSizes`$$

CREATE PROCEDURE `md_system_GetTableSizes`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT
        table_name AS `TableName`,
        table_rows AS `RowCount`,
        ROUND(data_length / 1024 / 1024, 2) AS `DataSizeMB`,
        ROUND(index_length / 1024 / 1024, 2) AS `IndexSizeMB`,
        ROUND((data_length + index_length) / 1024 / 1024, 2) AS `TotalSizeMB`
    FROM information_schema.TABLES
    WHERE table_schema = DATABASE()
    ORDER BY (data_length + index_length) DESC;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;