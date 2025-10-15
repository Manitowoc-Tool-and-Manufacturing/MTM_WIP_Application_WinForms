DROP PROCEDURE IF EXISTS `md_operation_numbers_Update_Operation`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Update_Operation`(
    IN p_Operation VARCHAR(100),
    IN p_NewOperation VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    
    -- Exit handler for any SQL exception
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate required parameters
    IF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
        ROLLBACK;
    ELSEIF p_NewOperation IS NULL OR TRIM(p_NewOperation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'NewOperation is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSE
        -- Update operation
        UPDATE `md_operation_numbers`
        SET `Operation` = p_NewOperation,
            `IssuedBy` = p_IssuedBy
        WHERE `Operation` = p_Operation;
        
        -- Check if update was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" updated successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;  -- Not found
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END$$
DELIMITER ;