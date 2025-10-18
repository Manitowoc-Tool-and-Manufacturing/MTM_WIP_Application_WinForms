DELIMITER //
DROP PROCEDURE IF EXISTS `usr_users_Delete_User`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Delete_User`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSE
        SET @d := CONCAT('DROP USER IF EXISTS ''', REPLACE(p_User, '''', ''''''), '''@''%'';');
        PREPARE stmt FROM @d;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
        DELETE FROM usr_users WHERE `User` = p_User;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END
//
DELIMITER ;
