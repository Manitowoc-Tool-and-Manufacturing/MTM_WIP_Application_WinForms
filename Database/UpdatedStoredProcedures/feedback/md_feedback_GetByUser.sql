DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_GetByUser`$$

CREATE PROCEDURE `md_feedback_GetByUser`(
    IN p_UserID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error retrieving user feedback records';
    END;

    SELECT 
        f.FeedbackID,
        f.FeedbackType,
        f.TrackingNumber,
        f.UserID,
        u.User AS UserName,
        u.`Full Name` AS UserFullName,
        f.SubmissionDateTime,
        f.LastUpdatedDateTime,
        f.ApplicationVersion,
        f.WindowForm,
        f.ActiveSection,
        f.Category,
        f.CustomCategory,
        f.Severity,
        f.Priority,
        f.Title,
        f.Description,
        f.Status,
        f.AssignedToDeveloperID,
        dev.User AS AssignedDeveloperName,
        f.ResolutionDateTime,
        f.IsDuplicate,
        f.DuplicateOfFeedbackID
    FROM UserFeedback f
    INNER JOIN usr_users u ON f.UserID = u.ID
    LEFT JOIN usr_users dev ON f.AssignedToDeveloperID = dev.ID
    WHERE f.UserID = p_UserID
    ORDER BY f.SubmissionDateTime DESC;

    SET p_Status = 0;
    SET p_ErrorMsg = '';
END$$

DELIMITER ;
