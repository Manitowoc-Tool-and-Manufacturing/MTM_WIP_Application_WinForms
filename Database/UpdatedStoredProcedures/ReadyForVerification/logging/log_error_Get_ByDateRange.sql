DELIMITER //
DROP PROCEDURE IF EXISTS `log_error_Get_ByDateRange`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `log_error_Get_ByDateRange`(
    IN p_StartDate DATETIME,
    IN p_EndDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    IF p_StartDate IS NULL OR p_EndDate IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Start date and end date are required';
    ELSEIF p_StartDate > p_EndDate THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Start date must be less than or equal to end date';
    ELSE
        SELECT * FROM `log_error`
        WHERE `ErrorTime` BETWEEN p_StartDate AND p_EndDate
        ORDER BY `ErrorTime` DESC;
        SELECT FOUND_ROWS() INTO v_Count;
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' error log entries between ',
                                   DATE_FORMAT(p_StartDate, '%Y-%m-%d %H:%i:%s'), ' and ',
                                   DATE_FORMAT(p_EndDate, '%Y-%m-%d %H:%i:%s'));
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No error log entries found between ',
                                   DATE_FORMAT(p_StartDate, '%Y-%m-%d %H:%i:%s'), ' and ',
                                   DATE_FORMAT(p_EndDate, '%Y-%m-%d %H:%i:%s'));
        END IF;
    END IF;
END
//
DELIMITER ;
