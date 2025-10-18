-- =============================================
-- Procedure: sys_last_10_transactions_Delete_ByUserAndPosition_1
-- Domain: system
-- Created: 2025-10-17
-- Purpose: Deletes quick button at position and shifts remaining buttons
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Delete_ByUserAndPosition_1`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Delete_ByUserAndPosition_1`(
    IN p_User VARCHAR(100),
    IN p_Position INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
        ROLLBACK;
    ELSEIF p_Position IS NULL OR p_Position < 1 OR p_Position > 10 THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Position must be between 1 and 10';
        ROLLBACK;
    ELSE
        -- Delete the quick button at the specified position
        DELETE FROM sys_last_10_transactions
        WHERE User = p_User AND Position = p_Position;
        
        SET v_RowsAffected = ROW_COUNT();
        
        -- Shift remaining buttons down
        UPDATE sys_last_10_transactions
        SET Position = Position - 1
        WHERE User = p_User AND Position > p_Position
        ORDER BY Position ASC;
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Deleted quick button at position ', p_Position, ' and shifted remaining buttons');
            COMMIT;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No quick button found at position ', p_Position, ' for user "', p_User, '"');
            COMMIT;
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of sys_last_10_transactions_Delete_ByUserAndPosition_1
-- =============================================
