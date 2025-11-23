DELIMITER //

DROP PROCEDURE IF EXISTS sp_error_reports_Delete //

CREATE PROCEDURE sp_error_reports_Delete(
    IN p_ReportID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(255)
)
BEGIN
    -- Check if report exists
    IF NOT EXISTS (SELECT 1 FROM error_reports WHERE ReportID = p_ReportID) THEN
        SET p_Status = 0;
        SET p_ErrorMsg = 'Error report not found.';
    ELSE
        DELETE FROM error_reports WHERE ReportID = p_ReportID;
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error report deleted successfully.';
    END IF;
END //

DELIMITER ;
