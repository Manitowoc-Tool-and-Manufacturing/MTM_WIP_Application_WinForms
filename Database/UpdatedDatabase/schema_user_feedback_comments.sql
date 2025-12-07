CREATE TABLE IF NOT EXISTS `UserFeedbackComments` (
  `CommentID` INT NOT NULL AUTO_INCREMENT,
  `FeedbackID` INT NOT NULL,
  `UserID` INT NOT NULL,
  `CommentDateTime` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `CommentText` MEDIUMTEXT NOT NULL,
  `IsInternalNote` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`CommentID`),
  INDEX `IX_UserFeedbackComments_FeedbackID` (`FeedbackID` ASC),
  INDEX `IX_UserFeedbackComments_UserID` (`UserID` ASC),
  CONSTRAINT `FK_UserFeedbackComments_FeedbackID`
    FOREIGN KEY (`FeedbackID`)
    REFERENCES `UserFeedback` (`FeedbackID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_UserFeedbackComments_UserID`
    FOREIGN KEY (`UserID`)
    REFERENCES `usr_users` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
) ENGINE = InnoDB DEFAULT CHARSET=utf8mb4;
