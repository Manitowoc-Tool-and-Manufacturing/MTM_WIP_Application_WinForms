-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Nov 24, 2025 at 01:44 PM
-- Server version: 5.7.24
-- PHP Version: 8.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mtm_wip_application_winforms`
--

-- --------------------------------------------------------

--
-- Table structure for table `app_themes`
--

CREATE TABLE `app_themes` (
  `ThemeName` varchar(100) NOT NULL,
  `SettingsJson` json NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `debug_matching`
--

CREATE TABLE `debug_matching` (
  `id` int(11) NOT NULL,
  `in_id` int(11) DEFAULT NULL,
  `in_part` varchar(100) DEFAULT NULL,
  `in_loc` varchar(100) DEFAULT NULL,
  `in_batch` varchar(100) DEFAULT NULL,
  `out_id` int(11) DEFAULT NULL,
  `out_part` varchar(100) DEFAULT NULL,
  `out_loc` varchar(100) DEFAULT NULL,
  `out_batch` varchar(100) DEFAULT NULL,
  `matched_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `error_reports`
--

CREATE TABLE `error_reports` (
  `ReportID` int(11) NOT NULL,
  `ReportDate` datetime NOT NULL,
  `UserName` varchar(100) NOT NULL,
  `MachineName` varchar(200) DEFAULT NULL,
  `AppVersion` varchar(50) DEFAULT NULL,
  `ErrorType` varchar(255) DEFAULT NULL,
  `ErrorSummary` text,
  `UserNotes` text,
  `TechnicalDetails` text,
  `CallStack` text,
  `Status` enum('New','Reviewed','Resolved') NOT NULL DEFAULT 'New',
  `ReviewedBy` varchar(100) DEFAULT NULL,
  `ReviewedDate` datetime DEFAULT NULL,
  `DeveloperNotes` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `inv_inventory`
--

CREATE TABLE `inv_inventory` (
  `ID` int(11) NOT NULL,
  `PartID` varchar(300) NOT NULL,
  `Location` varchar(100) NOT NULL,
  `Operation` varchar(100) DEFAULT NULL,
  `Quantity` int(11) NOT NULL,
  `ItemType` varchar(100) NOT NULL DEFAULT 'WIP',
  `ReceiveDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastUpdated` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `User` varchar(100) NOT NULL,
  `BatchNumber` varchar(300) DEFAULT NULL,
  `Notes` varchar(1000) DEFAULT NULL,
  `ColorCode` varchar(50) NOT NULL DEFAULT 'UNKNOWN',
  `WorkOrder` varchar(10) NOT NULL DEFAULT 'UNKNOWN'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `inv_inventory_batch_seq`
--

CREATE TABLE `inv_inventory_batch_seq` (
  `last_batch_number` bigint(20) NOT NULL,
  `current_match` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `inv_transaction`
--

CREATE TABLE `inv_transaction` (
  `ID` int(11) NOT NULL,
  `TransactionType` enum('IN','OUT','TRANSFER') NOT NULL,
  `BatchNumber` varchar(300) DEFAULT NULL,
  `PartID` varchar(300) NOT NULL,
  `FromLocation` varchar(100) DEFAULT NULL,
  `ToLocation` varchar(100) DEFAULT NULL,
  `Operation` varchar(100) DEFAULT NULL,
  `Quantity` int(11) NOT NULL,
  `Notes` varchar(1000) DEFAULT NULL,
  `ColorCode` varchar(50) NOT NULL DEFAULT 'Unknown',
  `WorkOrder` varchar(10) NOT NULL DEFAULT 'Unknown',
  `User` varchar(100) NOT NULL,
  `ItemType` varchar(100) NOT NULL DEFAULT 'WIP',
  `ReceiveDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `log_changelog`
--

CREATE TABLE `log_changelog` (
  `Version` varchar(50) NOT NULL,
  `Notes` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `log_error`
--

CREATE TABLE `log_error` (
  `ID` int(11) NOT NULL,
  `User` varchar(100) DEFAULT NULL,
  `Severity` enum('Information','Warning','Error','Critical','High') NOT NULL DEFAULT 'Error',
  `ErrorType` varchar(100) DEFAULT NULL,
  `ErrorMessage` text NOT NULL,
  `StackTrace` text,
  `ModuleName` varchar(200) DEFAULT NULL,
  `MethodName` varchar(200) DEFAULT NULL,
  `AdditionalInfo` text,
  `MachineName` varchar(100) DEFAULT NULL,
  `OSVersion` varchar(100) DEFAULT NULL,
  `AppVersion` varchar(50) DEFAULT NULL,
  `ErrorTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `md_color_codes`
--

CREATE TABLE `md_color_codes` (
  `ColorCode` varchar(50) NOT NULL,
  `IsUserDefined` tinyint(1) NOT NULL DEFAULT '0',
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `md_item_types`
--

CREATE TABLE `md_item_types` (
  `ID` int(11) NOT NULL,
  `ItemType` varchar(100) NOT NULL,
  `IssuedBy` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `md_locations`
--

CREATE TABLE `md_locations` (
  `ID` int(11) NOT NULL,
  `Location` varchar(100) NOT NULL,
  `Building` varchar(100) NOT NULL DEFAULT 'Expo',
  `IssuedBy` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `md_operation_numbers`
--

CREATE TABLE `md_operation_numbers` (
  `ID` int(11) NOT NULL,
  `Operation` varchar(100) NOT NULL,
  `IssuedBy` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `md_part_ids`
--

CREATE TABLE `md_part_ids` (
  `ID` int(11) NOT NULL,
  `PartID` varchar(300) NOT NULL,
  `Customer` varchar(300) NOT NULL,
  `Description` varchar(300) NOT NULL,
  `IssuedBy` varchar(100) NOT NULL,
  `RequiresColorCode` tinyint(1) NOT NULL DEFAULT '0',
  `ItemType` varchar(100) NOT NULL,
  `Operations` json DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `sys_last_10_transactions`
--

CREATE TABLE `sys_last_10_transactions` (
  `ID` int(11) NOT NULL,
  `User` varchar(100) NOT NULL,
  `PartID` varchar(300) NOT NULL,
  `Operation` varchar(100) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `ReceiveDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Position` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `sys_parameter_prefix_overrides`
--

CREATE TABLE `sys_parameter_prefix_overrides` (
  `OverrideId` int(11) NOT NULL COMMENT 'Unique identifier for override record',
  `ProcedureName` varchar(128) NOT NULL COMMENT 'Stored procedure name (e.g., inv_inventory_Add)',
  `ParameterName` varchar(128) NOT NULL COMMENT 'Parameter name without prefix (e.g., PartNumber)',
  `OverridePrefix` varchar(10) NOT NULL COMMENT 'Override prefix to use (e.g., p_, in_, or empty string)',
  `Reason` varchar(500) DEFAULT NULL COMMENT 'Explanation for override (optional)',
  `CreatedBy` varchar(50) NOT NULL COMMENT 'User who created the override',
  `CreatedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Timestamp of creation',
  `ModifiedBy` varchar(50) DEFAULT NULL COMMENT 'User who last modified the override',
  `ModifiedDate` datetime DEFAULT NULL COMMENT 'Timestamp of last modification',
  `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Soft delete flag (1=active, 0=deleted)'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Parameter prefix overrides for stored procedure execution';

-- --------------------------------------------------------

--
-- Table structure for table `sys_roles`
--

CREATE TABLE `sys_roles` (
  `ID` int(11) NOT NULL,
  `RoleName` varchar(50) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Permissions` varchar(1000) DEFAULT NULL,
  `IsSystem` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'True for built-in roles',
  `CreatedBy` varchar(100) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `sys_shortcuts`
--

CREATE TABLE `sys_shortcuts` (
  `ShortcutName` varchar(100) NOT NULL,
  `ShortcutKeys` int(11) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Category` varchar(50) DEFAULT NULL,
  `LastModified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `sys_user_roles`
--

CREATE TABLE `sys_user_roles` (
  `UserID` int(11) NOT NULL,
  `RoleID` int(11) NOT NULL,
  `AssignedBy` varchar(100) NOT NULL,
  `AssignedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `usr_dgv_settings`
--

CREATE TABLE `usr_dgv_settings` (
  `UserId` varchar(100) NOT NULL,
  `DgvName` varchar(100) NOT NULL,
  `SettingsJson` json DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `usr_settings`
--

CREATE TABLE `usr_settings` (
  `UserId` varchar(64) NOT NULL,
  `SettingsJson` json NOT NULL,
  `UpdatedAt` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `usr_users`
--

CREATE TABLE `usr_users` (
  `ID` int(11) NOT NULL,
  `User` varchar(100) NOT NULL,
  `Full Name` varchar(200) DEFAULT NULL,
  `Shift` varchar(50) NOT NULL DEFAULT '1',
  `VitsUser` tinyint(1) NOT NULL DEFAULT '0',
  `Pin` varchar(50) DEFAULT NULL,
  `LastShownVersion` varchar(50) NOT NULL DEFAULT '0.0.0.0',
  `HideChangeLog` varchar(50) NOT NULL DEFAULT 'false'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `usr_user_shortcuts`
--

CREATE TABLE `usr_user_shortcuts` (
  `UserName` varchar(50) NOT NULL,
  `ShortcutName` varchar(100) NOT NULL,
  `CustomKeys` int(11) NOT NULL,
  `LastModified` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `app_themes`
--
ALTER TABLE `app_themes`
  ADD PRIMARY KEY (`ThemeName`);

--
-- Indexes for table `debug_matching`
--
ALTER TABLE `debug_matching`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `error_reports`
--
ALTER TABLE `error_reports`
  ADD PRIMARY KEY (`ReportID`),
  ADD KEY `idx_user` (`UserName`),
  ADD KEY `idx_machine` (`MachineName`),
  ADD KEY `idx_date` (`ReportDate`),
  ADD KEY `idx_status` (`Status`);

--
-- Indexes for table `inv_inventory`
--
ALTER TABLE `inv_inventory`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `idx_partid_location` (`PartID`,`Location`),
  ADD KEY `idx_operation` (`Operation`),
  ADD KEY `idx_receivedate` (`ReceiveDate`),
  ADD KEY `idx_colorcode` (`ColorCode`),
  ADD KEY `idx_workorder` (`WorkOrder`);

--
-- Indexes for table `inv_inventory_batch_seq`
--
ALTER TABLE `inv_inventory_batch_seq`
  ADD PRIMARY KEY (`last_batch_number`);

--
-- Indexes for table `inv_transaction`
--
ALTER TABLE `inv_transaction`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `idx_partid` (`PartID`),
  ADD KEY `idx_user` (`User`),
  ADD KEY `idx_datetime` (`ReceiveDate`);

--
-- Indexes for table `log_changelog`
--
ALTER TABLE `log_changelog`
  ADD PRIMARY KEY (`Version`);

--
-- Indexes for table `log_error`
--
ALTER TABLE `log_error`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `idx_errortime` (`ErrorTime`),
  ADD KEY `idx_user` (`User`),
  ADD KEY `idx_severity` (`Severity`),
  ADD KEY `idx_errortype` (`ErrorType`);

--
-- Indexes for table `md_color_codes`
--
ALTER TABLE `md_color_codes`
  ADD PRIMARY KEY (`ColorCode`),
  ADD KEY `idx_user_defined` (`IsUserDefined`);

--
-- Indexes for table `md_item_types`
--
ALTER TABLE `md_item_types`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `uq_type` (`ItemType`);

--
-- Indexes for table `md_locations`
--
ALTER TABLE `md_locations`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `uq_location` (`Location`);

--
-- Indexes for table `md_operation_numbers`
--
ALTER TABLE `md_operation_numbers`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `uq_operation` (`Operation`);

--
-- Indexes for table `md_part_ids`
--
ALTER TABLE `md_part_ids`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `uq_item_number` (`PartID`),
  ADD KEY `idx_requires_colorcode` (`RequiresColorCode`);

--
-- Indexes for table `sys_last_10_transactions`
--
ALTER TABLE `sys_last_10_transactions`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `idx_user_datetime` (`User`,`ReceiveDate`);

--
-- Indexes for table `sys_parameter_prefix_overrides`
--
ALTER TABLE `sys_parameter_prefix_overrides`
  ADD PRIMARY KEY (`OverrideId`),
  ADD UNIQUE KEY `UQ_ProcParam` (`ProcedureName`,`ParameterName`) COMMENT 'Prevent duplicate procedure-parameter combinations',
  ADD KEY `IDX_ProcName` (`ProcedureName`) COMMENT 'Performance index for procedure lookup',
  ADD KEY `IDX_IsActive` (`IsActive`) COMMENT 'Performance index for active record filtering';

--
-- Indexes for table `sys_roles`
--
ALTER TABLE `sys_roles`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `uq_rolename` (`RoleName`);

--
-- Indexes for table `sys_shortcuts`
--
ALTER TABLE `sys_shortcuts`
  ADD PRIMARY KEY (`ShortcutName`);

--
-- Indexes for table `sys_user_roles`
--
ALTER TABLE `sys_user_roles`
  ADD PRIMARY KEY (`UserID`,`RoleID`),
  ADD KEY `idx_userid` (`UserID`),
  ADD KEY `idx_roleid` (`RoleID`);

--
-- Indexes for table `usr_dgv_settings`
--
ALTER TABLE `usr_dgv_settings`
  ADD PRIMARY KEY (`UserId`,`DgvName`);

--
-- Indexes for table `usr_settings`
--
ALTER TABLE `usr_settings`
  ADD PRIMARY KEY (`UserId`);

--
-- Indexes for table `usr_users`
--
ALTER TABLE `usr_users`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `uq_user` (`User`);

--
-- Indexes for table `usr_user_shortcuts`
--
ALTER TABLE `usr_user_shortcuts`
  ADD PRIMARY KEY (`UserName`,`ShortcutName`),
  ADD KEY `ShortcutName` (`ShortcutName`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `debug_matching`
--
ALTER TABLE `debug_matching`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `error_reports`
--
ALTER TABLE `error_reports`
  MODIFY `ReportID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `inv_inventory`
--
ALTER TABLE `inv_inventory`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `inv_transaction`
--
ALTER TABLE `inv_transaction`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `log_error`
--
ALTER TABLE `log_error`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `md_item_types`
--
ALTER TABLE `md_item_types`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `md_locations`
--
ALTER TABLE `md_locations`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `md_operation_numbers`
--
ALTER TABLE `md_operation_numbers`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `md_part_ids`
--
ALTER TABLE `md_part_ids`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sys_last_10_transactions`
--
ALTER TABLE `sys_last_10_transactions`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sys_parameter_prefix_overrides`
--
ALTER TABLE `sys_parameter_prefix_overrides`
  MODIFY `OverrideId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Unique identifier for override record';

--
-- AUTO_INCREMENT for table `sys_roles`
--
ALTER TABLE `sys_roles`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `usr_users`
--
ALTER TABLE `usr_users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `sys_user_roles`
--
ALTER TABLE `sys_user_roles`
  ADD CONSTRAINT `sys_user_roles_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `usr_users` (`ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `sys_user_roles_ibfk_2` FOREIGN KEY (`RoleID`) REFERENCES `sys_roles` (`ID`) ON DELETE CASCADE;

--
-- Constraints for table `usr_dgv_settings`
--
ALTER TABLE `usr_dgv_settings`
  ADD CONSTRAINT `fk_usr_dgv_settings_user` FOREIGN KEY (`UserId`) REFERENCES `usr_users` (`User`) ON DELETE CASCADE;

--
-- Constraints for table `usr_settings`
--
ALTER TABLE `usr_settings`
  ADD CONSTRAINT `usr_settings_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `usr_users` (`User`);

--
-- Constraints for table `usr_user_shortcuts`
--
ALTER TABLE `usr_user_shortcuts`
  ADD CONSTRAINT `usr_user_shortcuts_ibfk_1` FOREIGN KEY (`ShortcutName`) REFERENCES `sys_shortcuts` (`ShortcutName`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
