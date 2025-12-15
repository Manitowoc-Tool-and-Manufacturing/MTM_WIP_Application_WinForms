DELIMITER $$
DROP PROCEDURE IF EXISTS `md_devtools_GetLogEntries`$$
CREATE PROCEDURE `md_devtools_GetLogEntries`(
    IN p_DateFrom DATETIME,
    IN p_DateTo DATETIME,
    IN p_Severities VARCHAR(255),
    IN p_Source VARCHAR(100),
    IN p_SearchText VARCHAR(255),
    IN p_User VARCHAR(100),
    IN p_ErrorType VARCHAR(100),
    IN p_MaxResults INT,
    IN p_Skip INT,
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
        ErrorTime AS Timestamp,
        Severity AS Level,
        ModuleName AS Source,
        ErrorMessage AS Message,
        AdditionalInfo AS Details,
        User,
        ErrorType,
        StackTrace,
        MachineName,
        AppVersion
    FROM log_error
    WHERE ErrorTime BETWEEN p_DateFrom AND p_DateTo
    AND (p_Severities IS NULL OR p_Severities = '' OR FIND_IN_SET(Severity, p_Severities))
    AND (p_Source IS NULL OR p_Source = '' OR ModuleName LIKE CONCAT('%', p_Source, '%'))
    AND (p_SearchText IS NULL OR p_SearchText = '' OR ErrorMessage LIKE CONCAT('%', p_SearchText, '%') OR AdditionalInfo LIKE CONCAT('%', p_SearchText, '%'))
    AND (p_User IS NULL OR p_User = '' OR User = p_User)
    AND (p_ErrorType IS NULL OR p_ErrorType = '' OR ErrorType = p_ErrorType)
    ORDER BY ErrorTime DESC
    LIMIT p_MaxResults OFFSET p_Skip;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$