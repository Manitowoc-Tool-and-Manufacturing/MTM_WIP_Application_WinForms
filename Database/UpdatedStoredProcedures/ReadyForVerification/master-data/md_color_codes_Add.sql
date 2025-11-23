-- Stored Procedure: md_color_codes_Add
-- Purpose: Add custom color code to master table with duplicate prevention
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

DROP PROCEDURE IF EXISTS md_color_codes_Add;

DELIMITER $$

CREATE PROCEDURE md_color_codes_Add(
    IN p_ColorCode VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE color_exists INT DEFAULT 0;
    
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error adding color code';
        ROLLBACK;
    END;

    -- Validation: Required field
    IF p_ColorCode IS NULL OR p_ColorCode = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ColorCode is required';
    ELSE
        -- Validation: Reserved colors
        IF p_ColorCode IN ('Unknown', 'Other') THEN
            SET p_Status = -3;
            SET p_ErrorMsg = 'Cannot add reserved color codes';
        ELSE
            START TRANSACTION;

            -- Check for duplicate
            SELECT COUNT(*) INTO color_exists
            FROM md_color_codes
            WHERE ColorCode = p_ColorCode;

            IF color_exists > 0 THEN
                -- Duplicate exists, return success without inserting
                SET p_Status = 0;
                SET p_ErrorMsg = NULL;
                COMMIT;
            ELSE
                -- Insert new color
                INSERT INTO md_color_codes (ColorCode, IsUserDefined, CreatedDate)
                VALUES (p_ColorCode, TRUE, NOW());

                SET p_Status = 1;
                SET p_ErrorMsg = NULL;
                COMMIT;
            END IF;
        END IF;
    END IF;
END$$

DELIMITER ;

-- Test procedure
CALL md_color_codes_Add('Blueberry', @status, @msg);
SELECT @status AS Status, @msg AS ErrorMessage;

-- Verify inserted
SELECT * FROM md_color_codes WHERE ColorCode = 'Blueberry';
