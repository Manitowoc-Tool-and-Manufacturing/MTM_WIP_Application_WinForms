DELIMITER //
DROP PROCEDURE IF EXISTS `usr_users_Exists`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Exists`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSE
        SELECT COUNT(*) AS UserExists INTO v_Count
        FROM usr_users
        WHERE `User` = p_User;
        SELECT v_Count AS UserExists;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" exists');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" does not exist');
        END IF;
    END IF;
END
//
DELIMITER ;
