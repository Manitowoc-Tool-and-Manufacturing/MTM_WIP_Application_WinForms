mysqldump: [Warning] Using a password on the command line interface can be insecure.
-- MySQL dump 10.13  Distrib 5.7.24, for Win64 (x86_64)
--
-- Host: localhost    Database: MTM_WIP_Application_Winforms
-- ------------------------------------------------------
-- Server version	5.7.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `app_themes`
--

DROP TABLE IF EXISTS `app_themes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `app_themes` (
  `ThemeName` varchar(100) NOT NULL,
  `SettingsJson` json NOT NULL,
  PRIMARY KEY (`ThemeName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `debug_matching`
--

DROP TABLE IF EXISTS `debug_matching`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `debug_matching` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `in_id` int(11) DEFAULT NULL,
  `in_part` varchar(100) DEFAULT NULL,
  `in_loc` varchar(100) DEFAULT NULL,
  `in_batch` varchar(100) DEFAULT NULL,
  `out_id` int(11) DEFAULT NULL,
  `out_part` varchar(100) DEFAULT NULL,
  `out_loc` varchar(100) DEFAULT NULL,
  `out_batch` varchar(100) DEFAULT NULL,
  `matched_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `inv_inventory`
--

DROP TABLE IF EXISTS `inv_inventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `inv_inventory` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
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
  PRIMARY KEY (`ID`),
  KEY `idx_partid_location` (`PartID`,`Location`),
  KEY `idx_operation` (`Operation`),
  KEY `idx_receivedate` (`ReceiveDate`)
) ENGINE=InnoDB AUTO_INCREMENT=12671 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `inv_inventory_batch_seq`
--

DROP TABLE IF EXISTS `inv_inventory_batch_seq`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `inv_inventory_batch_seq` (
  `last_batch_number` bigint(20) NOT NULL,
  `current_match` int(11) NOT NULL,
  PRIMARY KEY (`last_batch_number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `inv_transaction`
--

DROP TABLE IF EXISTS `inv_transaction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `inv_transaction` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TransactionType` enum('IN','OUT','TRANSFER') NOT NULL,
  `BatchNumber` varchar(300) DEFAULT NULL,
  `PartID` varchar(300) NOT NULL,
  `FromLocation` varchar(100) DEFAULT NULL,
  `ToLocation` varchar(100) DEFAULT NULL,
  `Operation` varchar(100) DEFAULT NULL,
  `Quantity` int(11) NOT NULL,
  `Notes` varchar(1000) DEFAULT NULL,
  `User` varchar(100) NOT NULL,
  `ItemType` varchar(100) NOT NULL DEFAULT 'WIP',
  `ReceiveDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  KEY `idx_partid` (`PartID`),
  KEY `idx_user` (`User`),
  KEY `idx_datetime` (`ReceiveDate`)
) ENGINE=InnoDB AUTO_INCREMENT=36088 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `log_changelog`
--

DROP TABLE IF EXISTS `log_changelog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `log_changelog` (
  `Version` varchar(50) NOT NULL,
  `Notes` longtext,
  PRIMARY KEY (`Version`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `log_error`
--

DROP TABLE IF EXISTS `log_error`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `log_error` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `User` varchar(100) DEFAULT NULL,
  `Severity` enum('Information','Warning','Error','Critical','High') NOT NULL DEFAULT 'Error',
  `ErrorType` varchar(100) DEFAULT NULL,
  `ErrorMessage` text NOT NULL,
  `StackTrace` text,
  `ModuleName` varchar(200) DEFAULT NULL,
  `MethodName` varchar(200) DEFAULT NULL,
  `AdditionalInfo` text,
  `OSVersion` varchar(100) DEFAULT NULL,
  `AppVersion` varchar(50) DEFAULT NULL,
  `ErrorTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  KEY `idx_errortime` (`ErrorTime`),
  KEY `idx_user` (`User`),
  KEY `idx_severity` (`Severity`),
  KEY `idx_errortype` (`ErrorType`)
) ENGINE=InnoDB AUTO_INCREMENT=1521 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `md_item_types`
--

DROP TABLE IF EXISTS `md_item_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `md_item_types` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ItemType` varchar(100) NOT NULL,
  `IssuedBy` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uq_type` (`ItemType`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `md_locations`
--

DROP TABLE IF EXISTS `md_locations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `md_locations` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Location` varchar(100) NOT NULL,
  `Building` varchar(100) NOT NULL DEFAULT 'Expo',
  `IssuedBy` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uq_location` (`Location`)
) ENGINE=InnoDB AUTO_INCREMENT=55389 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `md_operation_numbers`
--

DROP TABLE IF EXISTS `md_operation_numbers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `md_operation_numbers` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Operation` varchar(100) NOT NULL,
  `IssuedBy` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uq_operation` (`Operation`)
) ENGINE=InnoDB AUTO_INCREMENT=151 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `md_part_ids`
--

DROP TABLE IF EXISTS `md_part_ids`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `md_part_ids` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PartID` varchar(300) NOT NULL,
  `Customer` varchar(300) NOT NULL,
  `Description` varchar(300) NOT NULL,
  `IssuedBy` varchar(100) NOT NULL,
  `ItemType` varchar(100) NOT NULL,
  `Operations` json DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uq_item_number` (`PartID`)
) ENGINE=InnoDB AUTO_INCREMENT=5164 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `migration_debug_log`
--

DROP TABLE IF EXISTS `migration_debug_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `migration_debug_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `message` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sys_last_10_transactions`
--

DROP TABLE IF EXISTS `sys_last_10_transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_last_10_transactions` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `User` varchar(100) NOT NULL,
  `PartID` varchar(300) NOT NULL,
  `Operation` varchar(100) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `ReceiveDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Position` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `idx_user_datetime` (`User`,`ReceiveDate`)
) ENGINE=InnoDB AUTO_INCREMENT=2780 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sys_last_10_transactions_backup`
--

DROP TABLE IF EXISTS `sys_last_10_transactions_backup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_last_10_transactions_backup` (
  `ID` int(11) NOT NULL DEFAULT '0',
  `User` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
  `PartID` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
  `Operation` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
  `Quantity` int(11) NOT NULL,
  `ReceiveDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Position` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sys_roles`
--

DROP TABLE IF EXISTS `sys_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_roles` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(50) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Permissions` varchar(1000) DEFAULT NULL,
  `IsSystem` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'True for built-in roles',
  `CreatedBy` varchar(100) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uq_rolename` (`RoleName`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sys_user_roles`
--

DROP TABLE IF EXISTS `sys_user_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_user_roles` (
  `UserID` int(11) NOT NULL,
  `RoleID` int(11) NOT NULL,
  `AssignedBy` varchar(100) NOT NULL,
  `AssignedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserID`,`RoleID`),
  KEY `idx_userid` (`UserID`),
  KEY `idx_roleid` (`RoleID`),
  CONSTRAINT `sys_user_roles_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `usr_users` (`ID`) ON DELETE CASCADE,
  CONSTRAINT `sys_user_roles_ibfk_2` FOREIGN KEY (`RoleID`) REFERENCES `sys_roles` (`ID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `usr_settings`
--

DROP TABLE IF EXISTS `usr_settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usr_settings` (
  `UserId` varchar(64) NOT NULL,
  `SettingsJson` json NOT NULL,
  `ShortcutsJson` json NOT NULL,
  `UpdatedAt` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserId`),
  CONSTRAINT `usr_settings_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `usr_users` (`User`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `usr_users`
--

DROP TABLE IF EXISTS `usr_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usr_users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `User` varchar(100) NOT NULL,
  `Full Name` varchar(200) DEFAULT NULL,
  `Shift` varchar(50) NOT NULL DEFAULT '1',
  `VitsUser` tinyint(1) NOT NULL DEFAULT '0',
  `Pin` varchar(50) DEFAULT NULL,
  `LastShownVersion` varchar(50) NOT NULL DEFAULT '0.0.0.0',
  `HideChangeLog` varchar(50) NOT NULL DEFAULT 'false',
  `Theme_Name` varchar(50) NOT NULL DEFAULT 'Default (Black and White)',
  `Theme_FontSize` int(11) NOT NULL DEFAULT '9',
  `VisualUserName` varchar(50) NOT NULL DEFAULT 'User Name',
  `VisualPassword` varchar(50) NOT NULL DEFAULT 'Password',
  `WipServerAddress` varchar(15) NOT NULL DEFAULT '172.16.1.104',
  `WIPDatabase` varchar(300) NOT NULL DEFAULT 'MTM_WIP_Application_Winforms',
  `WipServerPort` varchar(10) NOT NULL DEFAULT '3306',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `uq_user` (`User`)
) ENGINE=InnoDB AUTO_INCREMENT=313 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-10-19 11:42:07
