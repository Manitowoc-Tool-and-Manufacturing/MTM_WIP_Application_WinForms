DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_GetById`$$

CREATE PROCEDURE `md_feedback_GetById`(
    IN p_FeedbackID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error retrieving feedback record';
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
        f.OSVersion,
        f.MachineIdentifier,
        f.WindowForm,
        f.ActiveSection,
        f.Category,
        f.CustomCategory,
        f.Severity,
        f.Priority,
        f.Title,
        f.Description,
        f.StepsToReproduce,
        f.ExpectedBehavior,
        f.ActualBehavior,
        f.BusinessJustification,
        f.AffectedUsers,
        f.Location1,
        f.Location2,
        f.ExpectedConsistency,
        f.Status,
        f.AssignedToDeveloperID,
        dev.User AS AssignedDeveloperName,
        dev.`Full Name` AS AssignedDeveloperFullName,
        f.DeveloperNotes,
        f.ResolutionDateTime,
        f.IsDuplicate,
        f.DuplicateOfFeedbackID
    FROM UserFeedback f
    INNER JOIN usr_users u ON f.UserID = u.ID
    LEFT JOIN usr_users dev ON f.AssignedToDeveloperID = dev.ID
    WHERE f.FeedbackID = p_FeedbackID;

    SET p_Status = 0;
    SET p_ErrorMsg = '';
END$$

DELIMITER ;
