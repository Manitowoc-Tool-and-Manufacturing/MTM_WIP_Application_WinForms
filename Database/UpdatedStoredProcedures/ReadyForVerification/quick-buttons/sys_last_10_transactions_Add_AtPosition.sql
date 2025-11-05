-- =============================================
-- Procedure: sys_last_10_transactions_Add_AtPosition
-- Domain: system
-- Created: 2025-10-17
-- Purpose: Adds quick button at specific position, shifting existing buttons
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `sys_last_10_transactions_Add_AtPosition`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_last_10_transactions_Add_AtPosition`(
    IN p_User VARCHAR(100),
    IN p_PartID VARCHAR(300),
    IN p_Operation VARCHAR(100),
    IN p_Quantity INT,
    IN p_Position INT,
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
    -- Validate inputs
    IF p_User IS NULL OR TRIM(p_User) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'User is required';
    ELSEIF p_Position IS NULL OR p_Position < 1 OR p_Position > 10 THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Position must be between 1 and 10';
    ELSE
        -- Shift existing buttons at or after this position
        UPDATE sys_last_10_transactions
        SET Position = Position + 1
        WHERE User = p_User AND Position >= p_Position AND Position < 10
        ORDER BY Position DESC;
        
        -- Delete any button that would be pushed past position 10
        DELETE FROM sys_last_10_transactions
        WHERE User = p_User AND Position > 10;
        
        -- Insert new button at specified position
        INSERT INTO sys_last_10_transactions (User, PartID, Operation, Quantity, Position, ReceiveDate)
        VALUES (p_User, p_PartID, p_Operation, p_Quantity, p_Position, NOW())
        ON DUPLICATE KEY UPDATE
            PartID = VALUES(PartID),
            Operation = VALUES(Operation),
            Quantity = VALUES(Quantity),
            ReceiveDate = NOW();
        
        SET v_RowsAffected = ROW_COUNT();
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Added quick button at position ', p_Position);
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = 'No rows affected';
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of sys_last_10_transactions_Add_AtPosition
-- =============================================
