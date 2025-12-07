DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_email_notification_Upsert`$$

CREATE PROCEDURE `sys_email_notification_Upsert`(
    IN p_FeedbackCategory VARCHAR(100),
    IN p_RecipientEmails TEXT,
    IN p_IsActive TINYINT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_ExistingID INT;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error upserting email notification config';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Check if exists
    SELECT ConfigID INTO v_ExistingID
    FROM EmailNotificationConfig
    WHERE FeedbackCategory = p_FeedbackCategory
    LIMIT 1;

    IF v_ExistingID IS NOT NULL THEN
        -- Update existing
        UPDATE EmailNotificationConfig
        SET RecipientEmails = p_RecipientEmails,
            IsActive = p_IsActive,
            LastModifiedDateTime = NOW()
        WHERE ConfigID = v_ExistingID;
    ELSE
        -- Insert new
        INSERT INTO EmailNotificationConfig (FeedbackCategory, RecipientEmails, IsActive)
        VALUES (p_FeedbackCategory, p_RecipientEmails, p_IsActive);
    END IF;

    SET p_Status = 0;
    SET p_ErrorMsg = '';

    COMMIT;
END$$

DELIMITER ;
