-- =============================================
-- Table: sys_parameter_prefix_overrides
-- Database: ALL (MTM_WIP_Application_Winforms, mtm_wip_application_winforms_test)
-- Created: 2025-10-19
-- Purpose: Stores parameter prefix overrides for stored procedure execution
--          Allows fine-grained control over parameter naming conventions
-- =============================================

-- Drop table if exists (for clean redeployment)
DROP TABLE IF EXISTS `sys_parameter_prefix_overrides`;

-- Create table with comprehensive comments
CREATE TABLE `sys_parameter_prefix_overrides` (
  `OverrideId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Unique identifier for override record',
  `ProcedureName` varchar(128) NOT NULL COMMENT 'Stored procedure name (e.g., inv_inventory_Add)',
  `ParameterName` varchar(128) NOT NULL COMMENT 'Parameter name without prefix (e.g., PartNumber)',
  `OverridePrefix` varchar(10) NOT NULL COMMENT 'Override prefix to use (e.g., p_, in_, or empty string)',
  `Reason` varchar(500) DEFAULT NULL COMMENT 'Explanation for override (optional)',
  `CreatedBy` varchar(50) NOT NULL COMMENT 'User who created the override',
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Timestamp of creation',
  `ModifiedBy` varchar(50) DEFAULT NULL COMMENT 'User who last modified the override',
  `ModifiedDate` datetime DEFAULT NULL COMMENT 'Timestamp of last modification',
  `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Soft delete flag (1=active, 0=deleted)',
  PRIMARY KEY (`OverrideId`),
  UNIQUE KEY `UQ_ProcParam` (`ProcedureName`,`ParameterName`) COMMENT 'Prevent duplicate procedure-parameter combinations',
  KEY `IDX_ProcName` (`ProcedureName`) COMMENT 'Performance index for procedure lookup',
  KEY `IDX_IsActive` (`IsActive`) COMMENT 'Performance index for active record filtering'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Parameter prefix overrides for stored procedure execution';

-- =============================================
-- Initial Data (Optional - examples commented out)
-- =============================================

-- Example override entries (uncomment to use):
-- INSERT INTO `sys_parameter_prefix_overrides`
--   (`ProcedureName`, `ParameterName`, `OverridePrefix`, `Reason`, `CreatedBy`, `IsActive`)
-- VALUES
--   ('inv_inventory_Add', 'PartNumber', 'in_', 'Legacy procedure uses in_ prefix', 'SYSTEM', 1),
--   ('usr_users_Get', 'UserName', '', 'No prefix for this parameter', 'SYSTEM', 1);

-- =============================================
-- Verification Query
-- =============================================
-- Run this to verify table creation:
-- SELECT COUNT(*) AS OverrideCount FROM sys_parameter_prefix_overrides;

-- =============================================
-- End of sys_parameter_prefix_overrides.sql
-- =============================================
