-- =============================================================================
-- Stored Procedure: sys_user_roles_Get_ById
-- Purpose: Retrieves the role ID for a specific user by UserID
-- 
-- Parameters:
--   IN  p_UserID    INT            - The user ID to look up
--   OUT p_Status    INT            - Status code (1=success with data, 0=no data, -1=error, -2=validation error)
--   OUT p_ErrorMsg  VARCHAR(500)   - Error or status message
--
-- Returns: ResultSet with RoleID for the specified user
--
-- Status Codes:
--   1  = Success - User role found
--   0  = Success - No role found for user
--  -1  = SQL Exception occurred
--  -2  = Validation error (missing UserID)
--
-- Author: Auto-generated based on Dao_User.GetUserRoleIdAsync requirements
-- Date: 2025-11-02
-- =============================================================================

DELIMITER $$

DROP PROCEDURE IF EXISTS sys_user_roles_Get_ById$$

CREATE PROCEDURE sys_user_roles_Get_ById(
    IN p_UserID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Error handler for SQL exceptions
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    -- Validate required parameter
    IF p_UserID IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserID is required';
    ELSE
        -- Retrieve role for user
        SELECT RoleID, UserID, AssignedBy, AssignedAt
        FROM sys_user_roles
        WHERE UserID = p_UserID
        LIMIT 1;
        
        -- Check if any rows were returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved role for user ID ', p_UserID);
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No role found for user ID ', p_UserID);
        END IF;
    END IF;
END$$

DELIMITER ;
