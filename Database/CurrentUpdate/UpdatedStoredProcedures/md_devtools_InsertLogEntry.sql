DELIMITER $$
DROP PROCEDURE IF EXISTS `md_devtools_InsertLogEntry`$$
CREATE PROCEDURE `md_devtools_InsertLogEntry`(
    IN p_ErrorTime DATETIME,
    IN p_Severity VARCHAR(255),
    IN p_ModuleName VARCHAR(255),
    IN p_ErrorMessage TEXT,
    IN p_AdditionalInfo TEXT,
    IN p_User VARCHAR(255),
    IN p_ErrorType VARCHAR(255),
    IN p_StackTrace TEXT,
    IN p_MachineName VARCHAR(255),
    IN p_AppVersion VARCHAR(255),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    INSERT INTO log_error (
        ErrorTime, Severity, ModuleName, ErrorMessage, AdditionalInfo, 
        User, ErrorType, StackTrace, MachineName, AppVersion
    ) VALUES (
        p_ErrorTime, p_Severity, p_ModuleName, p_ErrorMessage, p_AdditionalInfo, 
        p_User, p_ErrorType, p_StackTrace, p_MachineName, p_AppVersion
    );

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$
DELIMITER ;
