DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_email_notification_GetRecipients`$$

CREATE PROCEDURE `sys_email_notification_GetRecipients`(
    IN p_FeedbackCategory VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500),
    OUT p_RecipientEmails TEXT
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error retrieving email recipients';
    END;

    -- Try category-specific first
    SELECT RecipientEmails INTO p_RecipientEmails
    FROM EmailNotificationConfig
    WHERE FeedbackCategory = p_FeedbackCategory
      AND IsActive = 1
    LIMIT 1;

    -- Fallback to 'All' category if not found
    IF p_RecipientEmails IS NULL THEN
        SELECT RecipientEmails INTO p_RecipientEmails
        FROM EmailNotificationConfig
        WHERE FeedbackCategory = 'All'
          AND IsActive = 1
        LIMIT 1;
    END IF;

    IF p_RecipientEmails IS NULL THEN
        SET p_RecipientEmails = '';
    END IF;

    SET p_Status = 0;
    SET p_ErrorMsg = '';
END$$

DELIMITER ;
