CREATE TABLE IF NOT EXISTS `UserFeedback` (
  `FeedbackID` INT NOT NULL AUTO_INCREMENT,
  `FeedbackType` VARCHAR(50) NOT NULL,
  `TrackingNumber` VARCHAR(50) NOT NULL,
  `UserID` INT NOT NULL,
  `SubmissionDateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastUpdatedDateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `ApplicationVersion` VARCHAR(50) NULL,
  `OSVersion` VARCHAR(100) NULL,
  `MachineIdentifier` VARCHAR(100) NULL,
  `WindowForm` VARCHAR(100) NULL,
  `ActiveSection` VARCHAR(100) NULL,
  `Category` VARCHAR(100) NULL,
  `CustomCategory` VARCHAR(100) NULL,
  `Severity` VARCHAR(50) NULL,
  `Priority` VARCHAR(50) NULL,
  `Title` VARCHAR(255) NULL,
  `Description` MEDIUMTEXT NULL,
  `StepsToReproduce` MEDIUMTEXT NULL,
  `ExpectedBehavior` MEDIUMTEXT NULL,
  `ActualBehavior` MEDIUMTEXT NULL,
  `BusinessJustification` MEDIUMTEXT NULL,
  `AffectedUsers` VARCHAR(50) NULL,
  `Location1` VARCHAR(255) NULL,
  `Location2` VARCHAR(255) NULL,
  `ExpectedConsistency` MEDIUMTEXT NULL,
  `Status` VARCHAR(50) NOT NULL DEFAULT 'New',
  `AssignedToDeveloperID` INT NULL,
  `DeveloperNotes` MEDIUMTEXT NULL,
  `ResolutionDateTime` DATETIME NULL,
  `IsDuplicate` TINYINT(1) NOT NULL DEFAULT 0,
  `DuplicateOfFeedbackID` INT NULL,
  PRIMARY KEY (`FeedbackID`),
  UNIQUE INDEX `IX_UserFeedback_TrackingNumber` (`TrackingNumber` ASC),
  INDEX `IX_UserFeedback_UserID` (`UserID` ASC),
  INDEX `IX_UserFeedback_Status` (`Status` ASC),
  INDEX `IX_UserFeedback_FeedbackType` (`FeedbackType` ASC),
  INDEX `IX_UserFeedback_SubmissionDateTime` (`SubmissionDateTime` ASC),
  CONSTRAINT `FK_UserFeedback_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `usr_users` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_UserFeedback_AssignedToDeveloperID`
    FOREIGN KEY (`AssignedToDeveloperID`)
    REFERENCES `usr_users` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_UserFeedback_DuplicateOfFeedbackID`
    FOREIGN KEY (`DuplicateOfFeedbackID`)
    REFERENCES `UserFeedback` (`FeedbackID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
) ENGINE = InnoDB DEFAULT CHARSET=utf8mb4;
