DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_ExportToCsv`$$

CREATE PROCEDURE `md_feedback_ExportToCsv`(
    IN p_FilterStatus VARCHAR(50),
    IN p_FilterFeedbackType VARCHAR(50),
    IN p_FilterUserID INT,
    IN p_FilterDateFrom DATETIME,
    IN p_FilterDateTo DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error exporting feedback records';
    END;

    SELECT 
        f.TrackingNumber,
        f.FeedbackType,
        f.Status,
        f.Severity,
        f.Priority,
        f.Title,
        f.Description,
        f.Category,
        f.CustomCategory,
        u.User AS SubmittedBy,
        u.`Full Name` AS SubmittedByFullName,
        f.SubmissionDateTime,
        f.LastUpdatedDateTime,
        dev.User AS AssignedDeveloper,
        f.WindowForm,
        f.ActiveSection,
        f.ApplicationVersion,
        f.OSVersion,
        f.IsDuplicate,
        f.DuplicateOfFeedbackID
    FROM UserFeedback f
    INNER JOIN usr_users u ON f.UserID = u.ID
    LEFT JOIN usr_users dev ON f.AssignedToDeveloperID = dev.ID
    WHERE 
        (p_FilterStatus IS NULL OR f.Status = p_FilterStatus)
        AND (p_FilterFeedbackType IS NULL OR f.FeedbackType = p_FilterFeedbackType)
        AND (p_FilterUserID IS NULL OR f.UserID = p_FilterUserID)
        AND (p_FilterDateFrom IS NULL OR f.SubmissionDateTime >= p_FilterDateFrom)
        AND (p_FilterDateTo IS NULL OR f.SubmissionDateTime <= p_FilterDateTo)
    ORDER BY f.SubmissionDateTime DESC;

    SET p_Status = 0;
    SET p_ErrorMsg = '';
END$$

DELIMITER ;
