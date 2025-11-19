-- Stored procedure to delete a user from usr_users table
-- Note: Does NOT drop MySQL user account - that must be handled separately if needed
-- Note: Deletes related records from sys_user_roles and usr_settings due to FK constraints
DELIMITER //
DROP PROCEDURE IF EXISTS `usr_users_Delete_User`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_users_Delete_User`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE v_UserId INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSE
        -- Get user ID for FK deletions
        SELECT ID INTO v_UserId FROM usr_users WHERE `User` = p_User LIMIT 1;
        
        IF v_UserId IS NULL OR v_UserId = 0 THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
        ELSE
            -- Delete related records first (FK constraints)
            DELETE FROM sys_user_roles WHERE UserID = v_UserId;
            DELETE FROM usr_settings WHERE UserId = p_User;
            
            -- Now delete the user
            DELETE FROM usr_users WHERE `User` = p_User;
            SET v_RowCount = ROW_COUNT();
            
            IF v_RowCount > 0 THEN
                SET p_Status = 1;
                SET p_ErrorMsg = CONCAT('User "', p_User, '" deleted successfully');
            ELSE
                SET p_Status = -4;
                SET p_ErrorMsg = CONCAT('User "', p_User, '" not found');
            END IF;
        END IF;
    END IF;
END
//
DELIMITER ;
