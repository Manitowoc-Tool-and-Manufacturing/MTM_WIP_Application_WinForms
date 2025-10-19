-- sys_parameter_prefix_overrides_Update_ById
-- Purpose: Update existing parameter prefix override
-- Created: 2025-10-18

DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Update_ById;

DELIMITER $$

CREATE PROCEDURE sys_parameter_prefix_overrides_Update_ById(
    IN p_OverrideId INT,
    IN p_ProcedureName VARCHAR(128),
    IN p_ParameterName VARCHAR(128),
    IN p_OverridePrefix VARCHAR(10),
    IN p_Reason VARCHAR(500),
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
    ELSEIF p_ProcedureName IS NULL OR TRIM(p_ProcedureName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ProcedureName is required';
        ROLLBACK;
    ELSEIF p_ParameterName IS NULL OR TRIM(p_ParameterName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ParameterName is required';
        ROLLBACK;
    ELSEIF p_ModifiedBy IS NULL OR TRIM(p_ModifiedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ModifiedBy is required';
        ROLLBACK;
    ELSEIF p_OverridePrefix IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'OverridePrefix cannot be NULL';
        ROLLBACK;
    ELSE
        -- Check if record exists
        IF NOT EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides WHERE OverrideId = p_OverrideId) THEN
            SET p_Status = 1;
            SET p_ErrorMsg = 'Override not found';
            ROLLBACK;
        ELSE
            -- Check for duplicate (excluding current record)
            IF EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides 
                       WHERE ProcedureName = p_ProcedureName 
                       AND ParameterName = p_ParameterName
                       AND OverrideId != p_OverrideId) THEN
                SET p_Status = -3;
                SET p_ErrorMsg = 'Override already exists for this procedure-parameter combination';
                ROLLBACK;
            ELSE
                -- Update record
                UPDATE sys_parameter_prefix_overrides
                SET ProcedureName = p_ProcedureName,
                    ParameterName = p_ParameterName,
                    OverridePrefix = p_OverridePrefix,
                    Reason = p_Reason,
                    ModifiedBy = p_ModifiedBy,
                    ModifiedDate = NOW()
                WHERE OverrideId = p_OverrideId;
                
                SET p_Status = 0;
                SET p_ErrorMsg = 'Override updated successfully';
                COMMIT;
            END IF;
        END IF;
    END IF;
END$$

DELIMITER ;
