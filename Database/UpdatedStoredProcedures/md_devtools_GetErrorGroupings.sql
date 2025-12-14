DELIMITER $$
DROP PROCEDURE IF EXISTS `md_devtools_GetErrorGroupings`$$
CREATE PROCEDURE `md_devtools_GetErrorGroupings`(
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
        ErrorType AS GroupName,
        COUNT(*) AS Count,
        MAX(ErrorTime) AS LastOccurrence,
        MAX(ErrorMessage) AS ExampleMessage
    FROM log_error
    WHERE ErrorTime BETWEEN p_DateFrom AND p_DateTo
    AND (Severity = 'Error' OR Severity = 'Critical')
    GROUP BY ErrorType
    ORDER BY Count DESC;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$
DELIMITER ;
