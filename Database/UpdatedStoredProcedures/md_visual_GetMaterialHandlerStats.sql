USE mtm_wip_application_winforms;
DROP PROCEDURE IF EXISTS md_visual_GetMaterialHandlerStats;

DELIMITER //

CREATE PROCEDURE md_visual_GetMaterialHandlerStats(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE, @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Error ', @errno, ' (', @sqlstate, '): ', @text);
    END;

    SELECT 
        User,
        TransactionType,
        COUNT(*) as TransactionCount
    FROM inv_transaction
    WHERE ReceiveDate >= p_StartDate AND ReceiveDate <= p_EndDate
    GROUP BY User, TransactionType;

    SET p_Status = 1;
    SET p_ErrorMsg = NULL;
END //

DELIMITER ;
