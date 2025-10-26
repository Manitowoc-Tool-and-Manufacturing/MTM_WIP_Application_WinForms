-- sp_error_reports_GetAll
-- Purpose: Retrieve error reports with optional filtering for the View Error Reports window
-- Created: 2025-10-26
-- Returns: Filtered error report rows ordered by most recent first

DROP PROCEDURE IF EXISTS sp_error_reports_GetAll;

DELIMITER $$

CREATE PROCEDURE sp_error_reports_GetAll(
    IN p_DateFrom DATETIME,
    IN p_DateTo DATETIME,
    IN p_UserName VARCHAR(100),
    IN p_MachineName VARCHAR(200),
    IN p_StatusFilter VARCHAR(20),
    IN p_SearchText VARCHAR(255),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_row_count INT DEFAULT 0;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        IF p_ErrorMsg IS NULL OR p_ErrorMsg = '' THEN
            SET p_ErrorMsg = 'Database error occurred while retrieving error reports';
        END IF;
        SET p_Status = -1;
    END;

    SELECT
        ReportID,
        ReportDate,
        UserName,
        MachineName,
        ErrorType,
        ErrorSummary,
        Status,
        ReviewedBy,
        ReviewedDate
    FROM error_reports
    WHERE
        (p_DateFrom IS NULL OR ReportDate >= p_DateFrom) AND
        (p_DateTo IS NULL OR ReportDate <= p_DateTo) AND
        (p_UserName IS NULL OR UserName = p_UserName) AND
        (p_MachineName IS NULL OR MachineName = p_MachineName) AND
        (p_StatusFilter IS NULL OR Status = p_StatusFilter) AND
        (
            p_SearchText IS NULL OR
            ErrorSummary LIKE CONCAT('%', p_SearchText, '%') OR
            UserNotes LIKE CONCAT('%', p_SearchText, '%') OR
            TechnicalDetails LIKE CONCAT('%', p_SearchText, '%')
        )
    ORDER BY ReportDate DESC;

    SET v_row_count = ROW_COUNT();

    IF v_row_count = 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = 'No error reports found';
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_row_count, ' error reports');
    END IF;
END$$

DELIMITER ;
