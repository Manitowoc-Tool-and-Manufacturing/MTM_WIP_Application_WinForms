-- sys_parameter_prefix_overrides_Get_All
-- Purpose: Retrieve all active parameter prefix overrides for startup cache loading
-- Created: 2025-10-18
-- Returns: All active overrides ordered by ProcedureName, ParameterName

DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Get_All;

DELIMITER $$

CREATE PROCEDURE sys_parameter_prefix_overrides_Get_All(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Check if table has active records
    IF EXISTS (SELECT 1 FROM sys_parameter_prefix_overrides WHERE IsActive = 1) THEN
        -- Return all active overrides
        SELECT 
            OverrideId,
            ProcedureName,
            ParameterName,
            OverridePrefix,
            Reason,
            CreatedBy,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            IsActive
        FROM sys_parameter_prefix_overrides
        WHERE IsActive = 1
        ORDER BY ProcedureName, ParameterName;
        
        SET p_Status = 0;
        SET p_ErrorMsg = 'Active overrides retrieved successfully';
    ELSE
        -- No active records found (not an error, just empty result)
        SELECT 
            OverrideId,
            ProcedureName,
            ParameterName,
            OverridePrefix,
            Reason,
            CreatedBy,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            IsActive
        FROM sys_parameter_prefix_overrides
        WHERE 1 = 0; -- Empty result set with correct structure
        
        SET p_Status = 1;
        SET p_ErrorMsg = 'No active overrides found';
    END IF;
END$$

DELIMITER ;

-- Test the stored procedure
CALL sys_parameter_prefix_overrides_Get_All(@status, @errorMsg);
SELECT @status AS Status, @errorMsg AS ErrorMessage;
