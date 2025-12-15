DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_comment_Insert`$$

CREATE PROCEDURE `md_feedback_comment_Insert`(
    IN p_FeedbackID INT,
    IN p_UserID INT,
    IN p_CommentText MEDIUMTEXT,
    IN p_IsInternalNote TINYINT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500),
    OUT p_CommentID INT
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error inserting feedback comment';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Insert comment
    INSERT INTO UserFeedbackComments (
        FeedbackID, UserID, CommentText, IsInternalNote, CommentDateTime
    ) VALUES (
        p_FeedbackID, p_UserID, p_CommentText, p_IsInternalNote, NOW()
    );

    SET p_CommentID = LAST_INSERT_ID();

    -- Update parent LastUpdatedDateTime
    UPDATE UserFeedback
    SET LastUpdatedDateTime = NOW()
    WHERE FeedbackID = p_FeedbackID;

    SET p_Status = 0;
    SET p_ErrorMsg = '';

    COMMIT;
END$$

DELIMITER ;
