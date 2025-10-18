-- =============================================
-- Procedure: md_operation_numbers_Exists_ByOperation
-- Domain: master-data
-- Created: 2025-10-17
-- Purpose: Checks if operation exists in md_operation_numbers table
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `md_operation_numbers_Exists_ByOperation`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Exists_ByOperation`(
    IN p_Operation VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate input
    IF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSE
        -- Check if operation exists
        SELECT COUNT(*) INTO v_Exists
        FROM md_operation_numbers
        WHERE Operation = p_Operation;
        
        -- Return existence as scalar
        SELECT v_Exists AS Exists;
        
        IF v_Exists > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" exists');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" does not exist');
        END IF;
        COMMIT;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of md_operation_numbers_Exists_ByOperation
-- =============================================
