CREATE TABLE IF NOT EXISTS `TrackingNumberSequence` (
  `SequenceID` INT NOT NULL AUTO_INCREMENT,
  `FeedbackType` VARCHAR(50) NOT NULL,
  `Year` INT NOT NULL,
  `NextNumber` INT NOT NULL DEFAULT 1,
  `LastGeneratedDateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`SequenceID`),
  UNIQUE INDEX `IX_TrackingNumberSequence_Type_Year` (`FeedbackType` ASC, `Year` ASC)
) ENGINE = InnoDB DEFAULT CHARSET=utf8mb4;
