DELIMITER //
DROP PROCEDURE IF EXISTS `log_error_Add_Error`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Add_Error`(
    IN p_User VARCHAR(100),
    IN p_Severity VARCHAR(50),
    IN p_ErrorType VARCHAR(100),
    IN p_ErrorMessage TEXT,
    IN p_StackTrace TEXT,
    IN p_ModuleName VARCHAR(100),
    IN p_MethodName VARCHAR(100),
    IN p_AdditionalInfo TEXT,
    IN p_MachineName VARCHAR(100),
    IN p_OSVersion VARCHAR(100),
    IN p_AppVersion VARCHAR(50),
    IN p_ErrorTime DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_User = 'Unknown';
    END IF;
    IF p_Severity IS NULL OR TRIM(p_Severity) = '' THEN
        SET p_Severity = 'Error';
    END IF;
    IF p_ErrorTime IS NULL THEN
        SET p_ErrorTime = NOW();
    END IF;
    IF p_ErrorMessage IS NULL OR TRIM(p_ErrorMessage) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Error message is required';
    ELSE
        INSERT INTO `log_error` (
            `User`, `Severity`, `ErrorType`, `ErrorMessage`, `StackTrace`,
            `ModuleName`, `MethodName`, `AdditionalInfo`, `MachineName`,
            `OSVersion`, `AppVersion`, `ErrorTime`
        ) VALUES (
            p_User, p_Severity, p_ErrorType, p_ErrorMessage, p_StackTrace,
            p_ModuleName, p_MethodName, p_AdditionalInfo, p_MachineName,
            p_OSVersion, p_AppVersion, p_ErrorTime
        );
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Error logged successfully for user: ', p_User);
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to create error log entry';
        END IF;
    END IF;
END
//
DELIMITER ;
