-- sys_parameter_prefix_overrides Table Creation Script
-- Purpose: Store parameter prefix overrides for stored procedures
-- Created: 2025-10-18
-- Database: mtm_wip_application_winforms_test

USE mtm_wip_application_winforms_test;

-- Drop table if exists (for clean development)
-- DROP TABLE IF EXISTS sys_parameter_prefix_overrides;

CREATE TABLE IF NOT EXISTS sys_parameter_prefix_overrides (
    OverrideId INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Unique identifier for override record',
    ProcedureName VARCHAR(128) NOT NULL COMMENT 'Stored procedure name (e.g., inv_inventory_Add)',
    ParameterName VARCHAR(128) NOT NULL COMMENT 'Parameter name without prefix (e.g., PartNumber)',
    OverridePrefix VARCHAR(10) NOT NULL COMMENT 'Override prefix to use (e.g., p_, in_, or empty string)',
    Reason VARCHAR(500) NULL COMMENT 'Explanation for override (optional)',
    CreatedBy VARCHAR(50) NOT NULL COMMENT 'User who created the override',
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Timestamp of creation',
    ModifiedBy VARCHAR(50) NULL COMMENT 'User who last modified the override',
    ModifiedDate DATETIME NULL COMMENT 'Timestamp of last modification',
    IsActive TINYINT(1) NOT NULL DEFAULT 1 COMMENT 'Soft delete flag (1=active, 0=deleted)',
    
    -- Constraints
    UNIQUE KEY UQ_ProcParam (ProcedureName, ParameterName) COMMENT 'Prevent duplicate procedure-parameter combinations',
    INDEX IDX_ProcName (ProcedureName) COMMENT 'Performance index for procedure lookup',
    INDEX IDX_IsActive (IsActive) COMMENT 'Performance index for active record filtering'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci 
COMMENT='Parameter prefix overrides for stored procedure execution';

-- Verify table creation
DESCRIBE sys_parameter_prefix_overrides;

-- Test insert (should succeed)
INSERT INTO sys_parameter_prefix_overrides 
    (ProcedureName, ParameterName, OverridePrefix, Reason, CreatedBy, IsActive)
VALUES 
    ('test_procedure', 'TestParam', 'p_', 'Test override for validation', 'SYSTEM', 1);

-- Verify insert worked
SELECT * FROM sys_parameter_prefix_overrides WHERE ProcedureName = 'test_procedure';

-- Test duplicate prevention (should fail with UNIQUE constraint error)
-- INSERT INTO sys_parameter_prefix_overrides 
--     (ProcedureName, ParameterName, OverridePrefix, Reason, CreatedBy, IsActive)
-- VALUES 
--     ('test_procedure', 'TestParam', 'in_', 'Duplicate test', 'SYSTEM', 1);

-- Cleanup test data
DELETE FROM sys_parameter_prefix_overrides WHERE ProcedureName = 'test_procedure';

-- Show final empty table
SELECT COUNT(*) AS RecordCount FROM sys_parameter_prefix_overrides;
