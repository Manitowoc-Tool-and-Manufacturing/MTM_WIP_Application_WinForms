-- Stored Procedure: md_color_codes_GetAll
-- Purpose: Retrieve all color codes from master table for dropdown/suggestion lists
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

DROP PROCEDURE IF EXISTS md_color_codes_GetAll;

DELIMITER $$

CREATE PROCEDURE md_color_codes_GetAll(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error retrieving color codes';
        ROLLBACK;
    END;

    -- Start transaction
    START TRANSACTION;

    -- Retrieve all color codes except "OTHER" (UI-only trigger)
    SELECT 
        ColorCode,
        IsUserDefined,
        CreatedDate
    FROM md_color_codes
    WHERE ColorCode != 'Other'  -- "OTHER" handled by UI dialog
    ORDER BY ColorCode ASC;

    -- Set success status
    IF FOUND_ROWS() > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;  -- Success but no data (unusual, should have predefined colors)
        SET p_ErrorMsg = 'No color codes found';
    END IF;

    COMMIT;
END$$

DELIMITER ;

-- Test procedure
CALL md_color_codes_GetAll(@status, @msg);
SELECT @status AS Status, @msg AS ErrorMessage;
