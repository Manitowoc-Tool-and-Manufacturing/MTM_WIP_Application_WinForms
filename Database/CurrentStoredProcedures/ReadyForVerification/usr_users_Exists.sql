DROP PROCEDURE IF EXISTS `usr_users_Exists`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Exists`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate parameter
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        -- Check if user exists
        SELECT COUNT(*) AS UserExists INTO v_Count
        FROM usr_users 
        WHERE `User` = p_User;
        
        -- Return result
        SELECT v_Count AS UserExists;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data (user exists)
            SET p_ErrorMsg = CONCAT('User "', p_User, '" exists');
        ELSE
            SET p_Status = 0;  -- Success but no data (user does not exist)
            SET p_ErrorMsg = CONCAT('User "', p_User, '" does not exist');
        END IF;
        
        COMMIT;
    END IF;
END$$
DELIMITER ;