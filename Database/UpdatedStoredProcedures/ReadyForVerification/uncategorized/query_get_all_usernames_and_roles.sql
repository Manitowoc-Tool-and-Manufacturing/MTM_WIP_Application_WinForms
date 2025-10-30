DELIMITER //
DROP PROCEDURE IF EXISTS `query_get_all_usernames_and_roles`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `query_get_all_usernames_and_roles`(
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
    SELECT u.User AS Username, s.RoleName
    FROM `MTM_WIP_Application_Winforms`.usr_users u
    JOIN `MTM_WIP_Application_Winforms`.sys_user_roles r ON r.UserID = u.ID
    JOIN `MTM_WIP_Application_Winforms`.sys_roles s ON s.ID = r.RoleID
    ORDER BY u.User;
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' user(s) with roles');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No users with roles found';
    END IF;
END
//
DELIMITER ;
