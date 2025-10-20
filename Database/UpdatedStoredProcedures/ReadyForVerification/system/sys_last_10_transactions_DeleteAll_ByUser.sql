-- =============================================
-- Procedure: sys_last_10_transactions_DeleteAll_ByUser
-- Domain: system
-- Created: 2025-10-17
-- Purpose: Deletes all quick buttons for a user
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_DeleteAll_ByUser`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_DeleteAll_ByUser`(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Validate input
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSE
        -- Delete all quick buttons for user
        DELETE FROM sys_last_10_transactions
        WHERE User = p_User;
        
        SET v_RowsAffected = ROW_COUNT();
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Deleted ', v_RowsAffected, ' quick button(s) for user "', p_User, '"');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No quick buttons found for user "', p_User, '"');
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of sys_last_10_transactions_DeleteAll_ByUser
-- =============================================
