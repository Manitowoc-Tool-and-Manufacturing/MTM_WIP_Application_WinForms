DELIMITER //
DROP PROCEDURE IF EXISTS `sys_GetUserAccessType`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_GetUserAccessType`(
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
    IF NOT EXISTS (SELECT 1 FROM information_schema.tables
                   WHERE table_schema = DATABASE() AND table_name = 'usr_users') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table usr_users not found';
        SELECT NULL LIMIT 0;
        ROLLBACK;
    ELSEIF NOT EXISTS (SELECT 1 FROM information_schema.tables
                       WHERE table_schema = DATABASE() AND table_name = 'sys_user_roles') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table sys_user_roles not found';
        SELECT NULL LIMIT 0;
        ROLLBACK;
    ELSEIF NOT EXISTS (SELECT 1 FROM information_schema.tables
                       WHERE table_schema = DATABASE() AND table_name = 'sys_roles') THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Required table sys_roles not found';
        SELECT NULL LIMIT 0;
        ROLLBACK;
    ELSE
        SELECT
            u.ID AS UserID,
            u.User AS UserName,
            COALESCE(r.RoleName, 'User') AS RoleName
        FROM usr_users u
        LEFT JOIN sys_user_roles ur ON u.ID = ur.UserID
        LEFT JOIN sys_roles r ON ur.RoleID = r.ID
        ORDER BY u.User;
        SELECT FOUND_ROWS() INTO v_Count;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' user access type(s)');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = 'No user access types found';
        END IF;
        COMMIT;
    END IF;
END
//
DELIMITER ;
