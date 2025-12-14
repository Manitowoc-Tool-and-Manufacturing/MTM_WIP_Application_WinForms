DELIMITER $$
DROP PROCEDURE IF EXISTS `md_devtools_GetFeedbackSummary`$$
CREATE PROCEDURE `md_devtools_GetFeedbackSummary`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT 
        Status,
        COUNT(*) as Count
    FROM UserFeedback
    GROUP BY Status;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$
DELIMITER ;
