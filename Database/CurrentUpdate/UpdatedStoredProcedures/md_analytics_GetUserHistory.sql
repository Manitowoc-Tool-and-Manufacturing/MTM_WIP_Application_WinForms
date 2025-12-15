DELIMITER $$

DROP PROCEDURE IF EXISTS `md_analytics_GetUserHistory`$$

CREATE PROCEDURE `md_analytics_GetUserHistory`(
    IN p_User VARCHAR(100),
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    -- Validation
    IF p_User IS NULL OR p_User = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        SELECT NULL LIMIT 0;
    ELSEIF p_StartDate IS NULL OR p_EndDate IS NULL THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Start date and end date are required';
        SELECT NULL LIMIT 0;
    ELSEIF p_StartDate > p_EndDate THEN
        SET p_Status = -4;
        SET p_ErrorMsg = 'Start date must be before end date';
        SELECT NULL LIMIT 0;
    ELSE
        -- Return user history
        SELECT 
            ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation,
            Operation, Quantity, Notes, User, ItemType, ReceiveDate
        FROM inv_transaction
        WHERE User = p_User AND ReceiveDate >= p_StartDate AND ReceiveDate <= p_EndDate
        ORDER BY ReceiveDate DESC;

        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
    END IF;
END$$

DELIMITER ;