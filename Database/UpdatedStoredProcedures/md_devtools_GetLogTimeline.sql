DELIMITER $$
DROP PROCEDURE IF EXISTS `md_devtools_GetLogTimeline`$$
CREATE PROCEDURE `md_devtools_GetLogTimeline`(
    IN p_DateFrom DATETIME,
    IN p_DateTo DATETIME,
    IN p_GroupBy VARCHAR(10),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    IF p_GroupBy = 'Hour' THEN
        SELECT 
            DATE_FORMAT(ErrorTime, '%Y-%m-%d %H:00:00') AS TimeSlot,
            COUNT(*) AS TotalCount,
            SUM(CASE WHEN Severity IN ('Error', 'Critical') THEN 1 ELSE 0 END) AS ErrorCount
        FROM log_error
        WHERE ErrorTime BETWEEN p_DateFrom AND p_DateTo
        GROUP BY DATE_FORMAT(ErrorTime, '%Y-%m-%d %H:00:00')
        ORDER BY TimeSlot;
    ELSE
        SELECT 
            DATE(ErrorTime) AS TimeSlot,
            COUNT(*) AS TotalCount,
            SUM(CASE WHEN Severity IN ('Error', 'Critical') THEN 1 ELSE 0 END) AS ErrorCount
        FROM log_error
        WHERE ErrorTime BETWEEN p_DateFrom AND p_DateTo
        GROUP BY DATE(ErrorTime)
        ORDER BY TimeSlot;
    END IF;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$
DELIMITER ;
