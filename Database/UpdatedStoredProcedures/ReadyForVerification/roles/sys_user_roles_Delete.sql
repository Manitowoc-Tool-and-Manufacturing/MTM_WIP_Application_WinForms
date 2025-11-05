DELIMITER //
DROP PROCEDURE IF EXISTS `sys_user_roles_Delete`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_roles_Delete`(
    IN p_UserID INT,
    IN p_RoleID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_UserID IS NULL OR p_UserID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid user ID is required';
    ELSEIF p_RoleID IS NULL OR p_RoleID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid role ID is required';
    ELSE
        SELECT COUNT(*) INTO v_Exists
        FROM sys_user_roles
        WHERE UserID = p_UserID AND RoleID = p_RoleID;
        IF v_Exists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Role ', p_RoleID, ' not assigned to user ', p_UserID);
        ELSE
            DELETE FROM sys_user_roles
            WHERE UserID = p_UserID AND RoleID = p_RoleID;
            SET v_RowCount = ROW_COUNT();
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('Role ', p_RoleID, ' removed from user ', p_UserID);
            ELSE
                SET p_Status = -3;
                SET p_ErrorMsg = 'Failed to remove role';
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
