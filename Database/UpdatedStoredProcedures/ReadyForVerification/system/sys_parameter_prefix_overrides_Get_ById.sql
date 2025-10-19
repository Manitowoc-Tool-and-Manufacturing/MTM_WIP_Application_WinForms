-- sys_parameter_prefix_overrides_Get_ById
-- Purpose: Retrieve single parameter prefix override by ID
-- Created: 2025-10-18

DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Get_ById;

DELIMITER $$

CREATE PROCEDURE sys_parameter_prefix_overrides_Get_ById(
    IN p_OverrideId INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Validation
    IF p_OverrideId IS NULL OR p_OverrideId <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid OverrideId - must be positive integer';
    ELSE
        -- Check if record exists
        IF EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides WHERE OverrideId = p_OverrideId) THEN
            SELECT * FROM sys_parameter_prefix_overrides WHERE OverrideId = p_OverrideId;
            SET p_Status = 0;
            SET p_ErrorMsg = 'Override retrieved successfully';
        ELSE
            SET p_Status = 1;
            SET p_ErrorMsg = 'Override not found';
            SELECT * FROM sys_parameter_prefix_overrides WHERE 1 = 0;
        END IF;
    END IF;
END$$

DELIMITER ;
