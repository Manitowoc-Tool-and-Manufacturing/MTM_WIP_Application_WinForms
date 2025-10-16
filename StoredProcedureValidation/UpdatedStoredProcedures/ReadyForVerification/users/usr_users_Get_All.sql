DELIMITER //
DROP PROCEDURE IF EXISTS `usr_users_Get_All`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    SELECT * FROM usr_users
    ORDER BY `User`;
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' user(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No users found';
    END IF;
    COMMIT;
END
//
DELIMITER ;
