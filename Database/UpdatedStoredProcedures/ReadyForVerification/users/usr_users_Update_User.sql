DELIMITER //
DROP PROCEDURE IF EXISTS `usr_users_Update_User`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Update_User`(
    IN p_User VARCHAR(100),
    IN p_FullName VARCHAR(200),
    IN p_Shift VARCHAR(50),
    IN p_Pin VARCHAR(50),
    IN p_VisualUserName VARCHAR(50),
    IN p_VisualPassword VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
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
        UPDATE usr_users SET
            `Full Name` = p_FullName,
            `Shift` = p_Shift,
            `Pin` = p_Pin,
            `VisualUserName` = p_VisualUserName,
            `VisualPassword` = p_VisualPassword
        WHERE `User` = p_User;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" updated successfully');
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
        END IF;
    END IF;
END
//
DELIMITER ;
