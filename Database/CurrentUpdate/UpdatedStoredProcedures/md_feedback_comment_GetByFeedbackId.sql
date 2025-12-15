DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_comment_GetByFeedbackId`$$

CREATE PROCEDURE `md_feedback_comment_GetByFeedbackId`(
    IN p_FeedbackID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error retrieving feedback comments';
    END;

    SELECT 
        c.CommentID,
        c.FeedbackID,
        c.UserID,
        u.User AS UserName,
        u.`Full Name` AS UserFullName,
        c.CommentDateTime,
        c.CommentText,
        c.IsInternalNote
    FROM UserFeedbackComments c
    INNER JOIN usr_users u ON c.UserID = u.ID
    WHERE c.FeedbackID = p_FeedbackID
    ORDER BY c.CommentDateTime ASC;

    SET p_Status = 0;
    SET p_ErrorMsg = '';
END$$

DELIMITER ;
