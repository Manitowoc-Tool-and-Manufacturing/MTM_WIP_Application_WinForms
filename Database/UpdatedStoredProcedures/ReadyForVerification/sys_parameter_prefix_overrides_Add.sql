-- sys_parameter_prefix_overrides_Add
-- Purpose: Add new parameter prefix override with validation
-- Created: 2025-10-18

USE mtm_wip_application_winforms_test;

DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Add;

DELIMITER $$

CREATE PROCEDURE sys_parameter_prefix_overrides_Add(
    IN p_ProcedureName VARCHAR(128),
    IN p_ParameterName VARCHAR(128),
    IN p_OverridePrefix VARCHAR(10),
    IN p_Reason VARCHAR(500),
    IN p_CreatedBy VARCHAR(50),
    OUT p_OverrideId INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_OverrideId = NULL;
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Validation
    IF p_ProcedureName IS NULL OR TRIM(p_ProcedureName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ProcedureName is required';
        SET p_OverrideId = NULL;
        ROLLBACK;
    ELSEIF p_ParameterName IS NULL OR TRIM(p_ParameterName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ParameterName is required';
        SET p_OverrideId = NULL;
        ROLLBACK;
    ELSEIF p_CreatedBy IS NULL OR TRIM(p_CreatedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'CreatedBy is required';
        SET p_OverrideId = NULL;
        ROLLBACK;
    ELSEIF p_OverridePrefix IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'OverridePrefix cannot be NULL (use empty string for no prefix)';
        SET p_OverrideId = NULL;
        ROLLBACK;
    ELSE
        -- Check for duplicate
        IF EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides 
                   WHERE ProcedureName = p_ProcedureName 
                   AND ParameterName = p_ParameterName) THEN
            SET p_Status = -3;
            SET p_ErrorMsg = 'Override already exists for this procedure-parameter combination';
            SET p_OverrideId = NULL;
            ROLLBACK;
        ELSE
            -- Insert new override
            INSERT INTO sys_parameter_prefix_overrides 
                (ProcedureName, ParameterName, OverridePrefix, Reason, CreatedBy, CreatedDate, IsActive)
            VALUES 
                (p_ProcedureName, p_ParameterName, p_OverridePrefix, p_Reason, p_CreatedBy, NOW(), 1);
            
            SET p_OverrideId = LAST_INSERT_ID();
            SET p_Status = 0;
            SET p_ErrorMsg = 'Override created successfully';
            COMMIT;
        END IF;
    END IF;
END$$

DELIMITER ;
