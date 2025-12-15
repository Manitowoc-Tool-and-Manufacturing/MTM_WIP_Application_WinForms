DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_GetAll`$$

CREATE PROCEDURE `md_feedback_GetAll`(
    IN p_FilterStatus VARCHAR(50),
    IN p_FilterFeedbackType VARCHAR(50),
    IN p_FilterUserID INT,
    IN p_FilterDateFrom DATETIME,
    IN p_FilterDateTo DATETIME,
    IN p_FilterAssignedDeveloperID INT,
    IN p_FilterCategory VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error retrieving feedback records';
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
        f.DeveloperNotes,
        f.ResolutionDateTime,
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
        AND (p_FilterAssignedDeveloperID IS NULL OR f.AssignedToDeveloperID = p_FilterAssignedDeveloperID)
        AND (p_FilterCategory IS NULL OR f.Category = p_FilterCategory)
    ORDER BY f.SubmissionDateTime DESC;

    SET p_Status = 0;
    SET p_ErrorMsg = '';
END$$

DELIMITER ;
