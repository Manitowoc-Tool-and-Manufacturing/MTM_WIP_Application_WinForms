DELIMITER $$
DROP PROCEDURE IF EXISTS `md_devtools_GetLogStatistics`$$
CREATE PROCEDURE `md_devtools_GetLogStatistics`(
    IN p_DateFrom DATETIME,
    IN p_DateTo DATETIME,
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
        COUNT(*) AS TotalCount,
        COALESCE(SUM(CASE WHEN Severity = 'Error' OR Severity = 'Critical' THEN 1 ELSE 0 END), 0) AS ErrorCount,
        COALESCE(SUM(CASE WHEN Severity = 'Warning' THEN 1 ELSE 0 END), 0) AS WarningCount,
        COALESCE(SUM(CASE WHEN Severity = 'Information' THEN 1 ELSE 0 END), 0) AS InfoCount,
        (SELECT MAX(ErrorTime) FROM log_error WHERE ErrorTime BETWEEN p_DateFrom AND p_DateTo AND (Severity = 'Error' OR Severity = 'Critical')) AS LastErrorTime,
        (SELECT ErrorMessage FROM log_error WHERE ErrorTime BETWEEN p_DateFrom AND p_DateTo AND (Severity = 'Error' OR Severity = 'Critical') ORDER BY ErrorTime DESC LIMIT 1) AS LastErrorMessage
    FROM log_error
    WHERE ErrorTime BETWEEN p_DateFrom AND p_DateTo;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$
DELIMITER ;
