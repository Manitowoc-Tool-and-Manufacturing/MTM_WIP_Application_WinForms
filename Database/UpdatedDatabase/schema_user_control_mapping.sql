CREATE TABLE IF NOT EXISTS `UserControlMapping` (
  `MappingID` INT NOT NULL AUTO_INCREMENT,
  `WindowFormMappingID` INT NOT NULL,
  `CodebaseName` VARCHAR(100) NOT NULL,
  `UserFriendlyName` VARCHAR(100) NOT NULL,
  `IsActive` TINYINT(1) NOT NULL DEFAULT 1,
  `CreatedDateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`MappingID`),
  UNIQUE INDEX `IX_UserControlMapping_Window_Codebase` (`WindowFormMappingID` ASC, `CodebaseName` ASC),
  CONSTRAINT `FK_UserControlMapping_WindowFormMappingID`
    FOREIGN KEY (`WindowFormMappingID`)
    REFERENCES `WindowFormMapping` (`MappingID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
) ENGINE = InnoDB DEFAULT CHARSET=utf8mb4;
