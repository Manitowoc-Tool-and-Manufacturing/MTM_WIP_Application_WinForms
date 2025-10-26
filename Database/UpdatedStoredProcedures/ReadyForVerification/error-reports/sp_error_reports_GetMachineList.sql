-- sp_error_reports_GetMachineList
-- Purpose: Retrieve distinct machine names for filter dropdown population
-- Created: 2025-10-26
-- Returns: Alphabetically sorted machine names excluding NULL/blank values

DROP PROCEDURE IF EXISTS sp_error_reports_GetMachineList;

DELIMITER $$

CREATE PROCEDURE sp_error_reports_GetMachineList(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_row_count INT DEFAULT 0;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        IF p_ErrorMsg IS NULL OR p_ErrorMsg = '' THEN
            SET p_ErrorMsg = 'Database error occurred while retrieving machine list';
        END IF;
        SET p_Status = -1;
    END;

    SELECT DISTINCT MachineName
    FROM error_reports
    WHERE MachineName IS NOT NULL AND TRIM(MachineName) <> ''
    ORDER BY MachineName ASC;

    SET v_row_count = ROW_COUNT();

    IF v_row_count = 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = 'No machines found';
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_row_count, ' unique machines');
    END IF;
END$$

DELIMITER ;
