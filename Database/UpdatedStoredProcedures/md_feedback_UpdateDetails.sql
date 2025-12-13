DELIMITER //
CREATE PROCEDURE md_feedback_UpdateDetails(
    IN p_FeedbackID INT,
    IN p_Description TEXT,
    IN p_StepsToReproduce TEXT,
    IN p_ExpectedBehavior TEXT,
    IN p_ActualBehavior TEXT,
    IN p_BusinessJustification TEXT,
    IN p_AffectedUsers TEXT,
    IN p_Location1 VARCHAR(255),
    IN p_Location2 VARCHAR(255),
    IN p_ExpectedConsistency TEXT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(255)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    UPDATE usr_feedback
    SET 
        Description = p_Description,
        StepsToReproduce = p_StepsToReproduce,
        ExpectedBehavior = p_ExpectedBehavior,
        ActualBehavior = p_ActualBehavior,
        BusinessJustification = p_BusinessJustification,
        AffectedUsers = p_AffectedUsers,
        Location1 = p_Location1,
        Location2 = p_Location2,
        ExpectedConsistency = p_ExpectedConsistency
    WHERE FeedbackID = p_FeedbackID;

    SET p_Status = 0;
    SET p_ErrorMsg = 'Success';
END //
DELIMITER ;