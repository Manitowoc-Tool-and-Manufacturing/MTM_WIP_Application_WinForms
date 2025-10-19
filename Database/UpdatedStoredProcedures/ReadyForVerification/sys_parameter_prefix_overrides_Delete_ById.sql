-- sys_parameter_prefix_overrides_Delete_ById
-- Purpose: Soft-delete parameter prefix override
-- Created: 2025-10-18

DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Delete_ById;

DELIMITER $$

CREATE PROCEDURE sys_parameter_prefix_overrides_Delete_ById(
    IN p_OverrideId INT,
    IN p_ModifiedBy VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Validation
    IF p_OverrideId IS NULL OR p_OverrideId <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Invalid OverrideId';
        ROLLBACK;
    ELSEIF p_ModifiedBy IS NULL OR TRIM(p_ModifiedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ModifiedBy is required';
        ROLLBACK;
    ELSE
        -- Check if record exists
        IF NOT EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides WHERE OverrideId = p_OverrideId) THEN
            SET p_Status = 1;
            SET p_ErrorMsg = 'Override not found';
            ROLLBACK;
        ELSE
            -- Soft delete (idempotent - already deleted returns success)
            UPDATE sys_parameter_prefix_overrides
            SET IsActive = 0,
                ModifiedBy = p_ModifiedBy,
                ModifiedDate = NOW()
            WHERE OverrideId = p_OverrideId;
            
            SET p_Status = 0;
            SET p_ErrorMsg = 'Override deleted successfully';
            COMMIT;
        END IF;
    END IF;
END$$

DELIMITER ;
