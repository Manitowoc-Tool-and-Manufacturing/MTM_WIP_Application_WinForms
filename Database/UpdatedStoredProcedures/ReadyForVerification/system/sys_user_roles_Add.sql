DELIMITER //
DROP PROCEDURE IF EXISTS `sys_user_roles_Add`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_roles_Add`(
    IN p_UserID INT,
    IN p_RoleID INT,
    IN p_AssignedBy VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_UserExists INT DEFAULT 0;
    DECLARE v_RoleExists INT DEFAULT 0;
    DECLARE v_DuplicateExists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    IF p_UserID IS NULL OR p_UserID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid user ID is required';
        ROLLBACK;
    ELSEIF p_RoleID IS NULL OR p_RoleID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid role ID is required';
        ROLLBACK;
    ELSEIF p_AssignedBy IS NULL OR TRIM(p_AssignedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Assigned by user is required';
        ROLLBACK;
    ELSE
        SELECT COUNT(*) INTO v_UserExists FROM usr_users WHERE ID = p_UserID;
        IF v_UserExists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User ID ', p_UserID, ' not found');
            ROLLBACK;
        ELSE
            SELECT COUNT(*) INTO v_RoleExists FROM sys_roles WHERE ID = p_RoleID;
            IF v_RoleExists = 0 THEN
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('Role ID ', p_RoleID, ' not found');
                ROLLBACK;
            ELSE
                SELECT COUNT(*) INTO v_DuplicateExists
                FROM sys_user_roles
                WHERE UserID = p_UserID AND RoleID = p_RoleID;
                IF v_DuplicateExists > 0 THEN
                    SET p_Status = -5;
                    SET p_ErrorMsg = 'User already has this role assigned';
                    ROLLBACK;
                ELSE
                    INSERT INTO sys_user_roles (UserID, RoleID, AssignedBy)
                    VALUES (p_UserID, p_RoleID, p_AssignedBy);
                    SET v_RowCount = ROW_COUNT();
                    IF v_RowCount > 0 THEN
                        SET p_Status = 1;
                        SET p_ErrorMsg = CONCAT('Role ', p_RoleID, ' assigned to user ', p_UserID);
                        COMMIT;
                    ELSE
                        SET p_Status = -3;
                        SET p_ErrorMsg = 'Failed to assign role';
                        ROLLBACK;
                    END IF;
                END IF;
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
