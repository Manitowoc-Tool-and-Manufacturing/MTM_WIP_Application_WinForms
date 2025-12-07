DELIMITER $$

DROP PROCEDURE IF EXISTS `md_feedback_UpdateStatus`$$

CREATE PROCEDURE `md_feedback_UpdateStatus`(
    IN p_FeedbackID INT,
    IN p_NewStatus VARCHAR(50),
    IN p_AssignedToDeveloperID INT,
    IN p_DeveloperNotes MEDIUMTEXT,
    IN p_ModifiedByUserID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_DeveloperRoleID INT;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error updating feedback status';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Validate developer role if assigning
    IF p_AssignedToDeveloperID IS NOT NULL THEN
        SELECT RoleID INTO v_DeveloperRoleID
        FROM sys_user_roles
        WHERE UserID = p_AssignedToDeveloperID
        LIMIT 1;

        -- RoleID 2 = Developer, 1 = Admin (both can be assigned)
        IF v_DeveloperRoleID IS NULL OR (v_DeveloperRoleID != 1 AND v_DeveloperRoleID != 2) THEN
            SET p_Status = 2;
            SET p_ErrorMsg = 'Assigned user must have Developer or Admin role';
            ROLLBACK;
        ELSE
            -- Update with assignment
            UPDATE UserFeedback
            SET Status = p_NewStatus,
                AssignedToDeveloperID = p_AssignedToDeveloperID,
                DeveloperNotes = COALESCE(p_DeveloperNotes, DeveloperNotes),
                LastUpdatedDateTime = NOW(),
                ResolutionDateTime = CASE WHEN p_NewStatus IN ('Resolved', 'Closed', 'Won''t Fix') THEN NOW() ELSE ResolutionDateTime END
            WHERE FeedbackID = p_FeedbackID;

            SET p_Status = 0;
            SET p_ErrorMsg = '';
            COMMIT;
        END IF;
    ELSE
        -- Update without assignment
        UPDATE UserFeedback
        SET Status = p_NewStatus,
            DeveloperNotes = COALESCE(p_DeveloperNotes, DeveloperNotes),
            LastUpdatedDateTime = NOW(),
            ResolutionDateTime = CASE WHEN p_NewStatus IN ('Resolved', 'Closed', 'Won''t Fix') THEN NOW() ELSE ResolutionDateTime END
        WHERE FeedbackID = p_FeedbackID;

        SET p_Status = 0;
        SET p_ErrorMsg = '';
        COMMIT;
    END IF;
END$$

DELIMITER ;
