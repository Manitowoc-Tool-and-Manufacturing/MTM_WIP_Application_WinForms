DELIMITER $$

DROP PROCEDURE IF EXISTS `md_analytics_GetTransactionsByRange`$$

CREATE PROCEDURE `md_analytics_GetTransactionsByRange`(
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
    IF p_StartDate IS NULL OR p_EndDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Start date and end date are required';
        SELECT NULL LIMIT 0; -- Return empty result set
    ELSEIF p_StartDate > p_EndDate THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Start date must be before end date';
        SELECT NULL LIMIT 0;
    ELSEIF DATEDIFF(p_EndDate, p_StartDate) > 365 THEN
        SET p_Status = -4;
        SET p_ErrorMsg = 'Date range cannot exceed 1 year';
        SELECT NULL LIMIT 0;
    ELSE
        -- Return transaction analytics
        SELECT 
            ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation,
            Operation, Quantity, Notes, User, ItemType, ReceiveDate
        FROM inv_transaction
        WHERE ReceiveDate >= p_StartDate AND ReceiveDate <= p_EndDate;

        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
    END IF;
END$$

DELIMITER ;