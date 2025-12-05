DROP PROCEDURE IF EXISTS md_visual_GetUserShiftData;
DELIMITER //
CREATE PROCEDURE md_visual_GetUserShiftData(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Get last 50 transactions for each user who has been active in the last 30 days
    -- Note: MySQL 5.7 doesn't support window functions like ROW_NUMBER()
    -- So we will just return all transactions for the last 30 days and let C# filter the top 50 per user
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE, @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Error ', @errno, ' (', @sqlstate, '): ', @text);
    END;

    SELECT USER_ID, TRANSACTION_DATE
    FROM INVENTORY_TRANS
    WHERE TRANSACTION_DATE >= DATE_SUB(NOW(), INTERVAL 30 DAY)
    ORDER BY USER_ID, TRANSACTION_DATE DESC;

    SET p_Status = 1;
    SET p_ErrorMsg = '';
END //
DELIMITER ;
