DELIMITER $$

DROP PROCEDURE IF EXISTS `sys_tracking_number_GetNext`$$

CREATE PROCEDURE `sys_tracking_number_GetNext`(
    IN p_FeedbackType VARCHAR(50),
    IN p_Year INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500),
    OUT p_TrackingNumber VARCHAR(50)
)
BEGIN
    DECLARE v_NextNumber INT;
    DECLARE v_Prefix VARCHAR(10);
    DECLARE v_RetryCount INT DEFAULT 0;
    DECLARE v_MaxRetries INT DEFAULT 3;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = 1;
        SET p_ErrorMsg = 'Error generating tracking number';
        ROLLBACK;
    END;

    -- Determine prefix based on feedback type
    SET v_Prefix = CASE p_FeedbackType
        WHEN 'Bug' THEN 'BUG'
        WHEN 'Suggestion' THEN 'SUG'
        WHEN 'Inconsistency' THEN 'INC'
        WHEN 'Question' THEN 'QUE'
        ELSE 'GEN'
    END;

    retry_loop: WHILE v_RetryCount < v_MaxRetries DO
        START TRANSACTION;

        -- Lock row for update (atomic increment)
        SELECT NextNumber INTO v_NextNumber
        FROM TrackingNumberSequence
        WHERE FeedbackType = p_FeedbackType
          AND Year = p_Year
        FOR UPDATE;

        IF v_NextNumber IS NULL THEN
            -- Insert new sequence
            INSERT INTO TrackingNumberSequence (FeedbackType, Year, NextNumber, LastGeneratedDateTime)
            VALUES (p_FeedbackType, p_Year, 2, NOW());
            
            SET v_NextNumber = 1;
        ELSE
            -- Increment sequence
            UPDATE TrackingNumberSequence
            SET NextNumber = NextNumber + 1,
                LastGeneratedDateTime = NOW()
            WHERE FeedbackType = p_FeedbackType
              AND Year = p_Year;
        END IF;

        -- Format tracking number: PREFIX-YEAR-NNNNNN
        SET p_TrackingNumber = CONCAT(v_Prefix, '-', p_Year, '-', LPAD(v_NextNumber, 6, '0'));

        -- Verify uniqueness
        IF NOT EXISTS (SELECT 1 FROM UserFeedback WHERE TrackingNumber = p_TrackingNumber) THEN
            SET p_Status = 0;
            SET p_ErrorMsg = '';
            COMMIT;
            LEAVE retry_loop;
        ELSE
            -- Collision detected, rollback and retry
            ROLLBACK;
            SET v_RetryCount = v_RetryCount + 1;
        END IF;
    END WHILE;

    IF v_RetryCount >= v_MaxRetries THEN
        SET p_Status = 3;
        SET p_ErrorMsg = 'Failed to generate unique tracking number after maximum retries';
    END IF;
END$$

DELIMITER ;
