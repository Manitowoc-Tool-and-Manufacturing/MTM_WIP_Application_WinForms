DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_MarkDuplicate`$$

CREATE PROCEDURE `md_feedback_MarkDuplicate`(
    IN p_FeedbackID INT,
    IN p_DuplicateOfFeedbackID INT,
    IN p_ModifiedByUserID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error marking feedback as duplicate';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Verify both IDs exist
    SELECT COUNT(*) INTO v_Count
    FROM UserFeedback
    WHERE FeedbackID IN (p_FeedbackID, p_DuplicateOfFeedbackID);

    IF v_Count != 2 THEN
        SET p_Status = 2;
        SET p_ErrorMsg = 'One or both feedback IDs not found';
        ROLLBACK;
    ELSE
        UPDATE UserFeedback
        SET IsDuplicate = 1,
            DuplicateOfFeedbackID = p_DuplicateOfFeedbackID,
            LastUpdatedDateTime = NOW()
        WHERE FeedbackID = p_FeedbackID;

        SET p_Status = 0;
        SET p_ErrorMsg = '';
        COMMIT;
    END IF;
END$$

DELIMITER ;
