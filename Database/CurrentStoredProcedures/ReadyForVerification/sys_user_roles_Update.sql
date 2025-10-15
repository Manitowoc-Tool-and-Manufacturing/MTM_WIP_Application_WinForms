DROP PROCEDURE IF EXISTS `sys_user_roles_Update`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_user_roles_Update`(
    IN p_UserID INT, 
    IN p_NewRoleID INT, 
    IN p_AssignedBy VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_UserExists INT DEFAULT 0;
    DECLARE v_RoleExists INT DEFAULT 0;
    DECLARE v_OldRoleExists INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_UserID IS NULL OR p_UserID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid user ID is required';
        ROLLBACK;
    ELSEIF p_NewRoleID IS NULL OR p_NewRoleID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid new role ID is required';
        ROLLBACK;
    ELSEIF p_AssignedBy IS NULL OR TRIM(p_AssignedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Assigned by user is required';
        ROLLBACK;
    ELSE
        -- Check if user exists
        SELECT COUNT(*) INTO v_UserExists FROM usr_users WHERE ID = p_UserID;
        
        IF v_UserExists = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User ID ', p_UserID, ' not found');
            ROLLBACK;
        ELSE
            -- Check if new role exists
            SELECT COUNT(*) INTO v_RoleExists FROM sys_roles WHERE ID = p_NewRoleID;
            
            IF v_RoleExists = 0 THEN
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('Role ID ', p_NewRoleID, ' not found');
                ROLLBACK;
            ELSE
                -- Check if user has any existing role
                SELECT COUNT(*) INTO v_OldRoleExists FROM sys_user_roles WHERE UserID = p_UserID;
                
                -- Delete all old roles for user
                DELETE FROM sys_user_roles WHERE UserID = p_UserID;
                
                -- Insert new role
                INSERT INTO sys_user_roles (UserID, RoleID, AssignedBy)
                VALUES (p_UserID, p_NewRoleID, p_AssignedBy);
                
                SET v_RowCount = ROW_COUNT();
                
                IF v_RowCount > 0 THEN
                    SET p_Status = 1;
                    SET p_ErrorMsg = CONCAT('User ', p_UserID, ' role updated to ', p_NewRoleID);
                    COMMIT;
                ELSE
                    SET p_Status = -3;
                    SET p_ErrorMsg = 'Failed to update user role';
                    ROLLBACK;
                END IF;
            END IF;
        END IF;
    END IF;
END$$
DELIMITER ;