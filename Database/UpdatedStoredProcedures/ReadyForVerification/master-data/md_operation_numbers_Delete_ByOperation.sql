DELIMITER //
DROP PROCEDURE IF EXISTS `md_operation_numbers_Delete_ByOperation`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Delete_ByOperation`(
    IN p_Operation VARCHAR(100),
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
    ELSE
        DELETE FROM `md_operation_numbers`
        WHERE `Operation` = p_Operation;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" deleted successfully');
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" not found');
        END IF;
    END IF;
END
//
DELIMITER ;
