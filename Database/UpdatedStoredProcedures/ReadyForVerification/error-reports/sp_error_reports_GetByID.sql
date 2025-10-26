-- sp_error_reports_GetByID
-- Purpose: Retrieve a single error report including full detail payload
-- Created: 2025-10-26
-- Returns: Full error report row or empty result when ReportID not found

DROP PROCEDURE IF EXISTS sp_error_reports_GetByID;

DELIMITER $$

CREATE PROCEDURE sp_error_reports_GetByID(
    IN p_ReportID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_row_count INT DEFAULT 0;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        IF p_ErrorMsg IS NULL OR p_ErrorMsg = '' THEN
            SET p_ErrorMsg = 'Database error occurred while retrieving error report detail';
        END IF;
        SET p_Status = -1;
    END;

    SELECT
        ReportID,
        ReportDate,
        UserName,
        MachineName,
        AppVersion,
        ErrorType,
        ErrorSummary,
        UserNotes,
        TechnicalDetails,
        CallStack,
        Status,
        ReviewedBy,
        ReviewedDate,
        DeveloperNotes
    FROM error_reports
    WHERE ReportID = p_ReportID;

    SET v_row_count = ROW_COUNT();

    IF v_row_count = 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ReportID not found';
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'Error report retrieved successfully';
    END IF;
END$$

DELIMITER ;
