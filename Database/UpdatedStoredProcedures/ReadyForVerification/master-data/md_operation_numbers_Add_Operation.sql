DELIMITER //
DROP PROCEDURE IF EXISTS `md_operation_numbers_Add_Operation`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Add_Operation`(
    IN p_Operation VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Operation is required';
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
    ELSE
        INSERT INTO `md_operation_numbers` (`Operation`, `IssuedBy`)
        VALUES (p_Operation, p_IssuedBy);
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" added successfully');
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to add operation';
        END IF;
    END IF;
END
//
DELIMITER ;
