DROP PROCEDURE IF EXISTS `log_error_Delete_All`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Delete_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Check if any entries exist
    SELECT COUNT(*) INTO v_Count FROM `log_error`;
    
    IF v_Count = 0 THEN
        SET p_Status = 0;
        SET p_ErrorMsg = 'No error log entries found to delete';
        COMMIT;
    ELSE
        -- Delete all error log entries
        DELETE FROM `log_error`;
        
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Successfully deleted ', v_RowCount, ' error log entry(ies)');
            COMMIT;
        ELSE
            SET p_Status = -3;
            SET p_ErrorMsg = 'Failed to delete error log entries';
            ROLLBACK;
        END IF;
    END IF;
END$$
DELIMITER ;