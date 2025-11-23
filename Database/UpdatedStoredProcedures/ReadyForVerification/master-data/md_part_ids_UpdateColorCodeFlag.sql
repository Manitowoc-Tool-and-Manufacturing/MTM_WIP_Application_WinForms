-- Stored Procedure: md_part_ids_UpdateColorCodeFlag
-- Purpose: Set RequiresColorCode flag for specific part
-- Feature: Color Code & Work Order Tracking
-- Date: 2025-11-13
-- MySQL Version: 5.7.24

USE mtm_wip_application_winforms;

DROP PROCEDURE IF EXISTS md_part_ids_UpdateColorCodeFlag;

DELIMITER $$

CREATE PROCEDURE md_part_ids_UpdateColorCodeFlag(
    IN p_PartID VARCHAR(300),
    IN p_RequiresColorCode BOOLEAN,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE part_exists INT DEFAULT 0;
    
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error updating color code flag';
        ROLLBACK;
    END;

    -- Validation: Required field
    IF p_PartID IS NULL OR p_PartID = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'PartID is required';
    ELSE
        START TRANSACTION;

        -- Check if part exists
        SELECT COUNT(*) INTO part_exists
        FROM md_part_ids
        WHERE PartID = p_PartID;

        IF part_exists = 0 THEN
            SET p_Status = -3;
            SET p_ErrorMsg = 'Part ID not found';
            ROLLBACK;
        ELSE
            -- Update RequiresColorCode flag
            UPDATE md_part_ids
            SET RequiresColorCode = p_RequiresColorCode
            WHERE PartID = p_PartID;

            SET p_Status = 1;
            SET p_ErrorMsg = NULL;
            COMMIT;
        END IF;
    END IF;
END$$

DELIMITER ;

-- Test procedure (requires existing part)
-- CALL md_part_ids_UpdateColorCodeFlag('TEST-PART', TRUE, @status, @msg);
-- SELECT @status AS Status, @msg AS ErrorMessage;
