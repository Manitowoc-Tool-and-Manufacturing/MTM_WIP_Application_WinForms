DELIMITER //
DROP PROCEDURE IF EXISTS `sys_user_GetIdByName`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_GetIdByName`(
    IN p_UserName VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE p_UserId INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_UserName IS NULL OR TRIM(p_UserName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User name is required';
        SELECT 0 AS UserID;
    ELSE
        SELECT ID INTO p_UserId FROM usr_users WHERE User = p_UserName LIMIT 1;
        IF p_UserId IS NULL OR p_UserId = 0 THEN
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('User "', p_UserName, '" not found');
            SELECT 0 AS UserID;
        ELSE
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('User ID retrieved for "', p_UserName, '"');
            SELECT p_UserId AS UserID;
        END IF;
    END IF;
END
//
DELIMITER ;
