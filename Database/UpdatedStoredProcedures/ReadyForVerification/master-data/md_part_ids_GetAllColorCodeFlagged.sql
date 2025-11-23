-- Stored Procedure: md_part_ids_GetAllColorCodeFlagged
-- Purpose: Retrieve all part IDs that require color code tracking for cache population
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

DROP PROCEDURE IF EXISTS md_part_ids_GetAllColorCodeFlagged;

DELIMITER $$

CREATE PROCEDURE md_part_ids_GetAllColorCodeFlagged(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error retrieving color-coded part IDs';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Retrieve all part IDs where RequiresColorCode flag is TRUE
    SELECT PartID
    FROM md_part_ids
    WHERE RequiresColorCode = TRUE
    ORDER BY PartID ASC;

    -- Set success status
    IF FOUND_ROWS() > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;  -- Success but no data (no parts flagged yet)
        SET p_ErrorMsg = NULL;
    END IF;

    COMMIT;
END$$

DELIMITER ;

-- Test procedure
CALL md_part_ids_GetAllColorCodeFlagged(@status, @msg);
SELECT @status AS Status, @msg AS ErrorMessage;
