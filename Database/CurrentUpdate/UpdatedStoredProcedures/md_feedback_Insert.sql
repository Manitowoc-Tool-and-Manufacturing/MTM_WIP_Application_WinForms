DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_Insert`$$

CREATE PROCEDURE `md_feedback_Insert`(
    IN p_FeedbackType VARCHAR(50),
    IN p_UserID INT,
    IN p_WindowForm VARCHAR(100),
    IN p_ActiveSection VARCHAR(100),
    IN p_Category VARCHAR(100),
    IN p_CustomCategory VARCHAR(100),
    IN p_Severity VARCHAR(50),
    IN p_Priority VARCHAR(50),
    IN p_Title VARCHAR(255),
    IN p_Description MEDIUMTEXT,
    IN p_StepsToReproduce MEDIUMTEXT,
    IN p_ExpectedBehavior MEDIUMTEXT,
    IN p_ActualBehavior MEDIUMTEXT,
    IN p_BusinessJustification MEDIUMTEXT,
    IN p_AffectedUsers VARCHAR(50),
    IN p_Location1 VARCHAR(255),
    IN p_Location2 VARCHAR(255),
    IN p_ExpectedConsistency MEDIUMTEXT,
    IN p_ApplicationVersion VARCHAR(50),
    IN p_OSVersion VARCHAR(100),
    IN p_MachineIdentifier VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500),
    OUT p_FeedbackID INT,
    OUT p_TrackingNumber VARCHAR(50)
)
BEGIN
    DECLARE v_Year INT;
    DECLARE v_TrackingNumber VARCHAR(50);
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error inserting feedback record';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Get current year
    SET v_Year = YEAR(CURDATE());

    -- Generate tracking number
    CALL sys_tracking_number_GetNext(p_FeedbackType, v_Year, @track_status, @track_error, v_TrackingNumber);
    
    IF @track_status != 0 THEN
        SET p_Status = @track_status;
        SET p_ErrorMsg = CONCAT('Failed to generate tracking number: ', @track_error);
        ROLLBACK;
    ELSE
        -- Insert feedback record
        INSERT INTO UserFeedback (
            FeedbackType, TrackingNumber, UserID,
            ApplicationVersion, OSVersion, MachineIdentifier,
            WindowForm, ActiveSection, Category, CustomCategory,
            Severity, Priority, Title, Description,
            StepsToReproduce, ExpectedBehavior, ActualBehavior,
            BusinessJustification, AffectedUsers,
            Location1, Location2, ExpectedConsistency,
            Status, SubmissionDateTime, LastUpdatedDateTime
        ) VALUES (
            p_FeedbackType, v_TrackingNumber, p_UserID,
            p_ApplicationVersion, p_OSVersion, p_MachineIdentifier,
            p_WindowForm, p_ActiveSection, p_Category, p_CustomCategory,
            p_Severity, p_Priority, p_Title, p_Description,
            p_StepsToReproduce, p_ExpectedBehavior, p_ActualBehavior,
            p_BusinessJustification, p_AffectedUsers,
            p_Location1, p_Location2, p_ExpectedConsistency,
            'New', NOW(), NOW()
        );

        SET p_FeedbackID = LAST_INSERT_ID();
        SET p_TrackingNumber = v_TrackingNumber;
        SET p_Status = 0;
        SET p_ErrorMsg = '';

        COMMIT;
    END IF;
END$$

DELIMITER ;
