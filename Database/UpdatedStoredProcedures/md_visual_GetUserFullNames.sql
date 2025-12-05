DROP PROCEDURE IF EXISTS md_visual_GetUserFullNames;
DELIMITER //
CREATE PROCEDURE md_visual_GetUserFullNames(
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

    -- Assuming EMPLOYEE table exists. If not, this might fail.
    -- We should check if table exists or handle error gracefully.
    -- For now, we assume the table exists as per previous code assumption.
    
    SELECT USER_ID, FIRST_NAME, LAST_NAME 
    FROM EMPLOYEE;

    SET p_Status = 1;
    SET p_ErrorMsg = '';
END //
DELIMITER ;
