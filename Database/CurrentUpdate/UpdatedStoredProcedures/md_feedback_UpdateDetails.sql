DELIMITER //

DROP PROCEDURE IF EXISTS md_feedback_UpdateDetails //

CREATE PROCEDURE md_feedback_UpdateDetails(
    IN p_FeedbackID INT,
    IN p_Description TEXT,
    IN p_StepsToReproduce TEXT,
    IN p_ExpectedBehavior TEXT,
    IN p_ActualBehavior TEXT,
    IN p_BusinessJustification TEXT,
    IN p_ExpectedConsistency TEXT,
    IN p_Location1 VARCHAR(255),
    IN p_Location2 VARCHAR(255),
    IN p_AffectedUsers VARCHAR(255)
)
BEGIN
    UPDATE usr_feedback
    SET 
        Description = p_Description,
        StepsToReproduce = p_StepsToReproduce,
        ExpectedBehavior = p_ExpectedBehavior,
        ActualBehavior = p_ActualBehavior,
        BusinessJustification = p_BusinessJustification,
        ExpectedConsistency = p_ExpectedConsistency,
        Location1 = p_Location1,
        Location2 = p_Location2,
        AffectedUsers = p_AffectedUsers,
        LastUpdatedDateTime = NOW()
    WHERE FeedbackID = p_FeedbackID;
END //

DELIMITER ;
