-- Seed EmailNotificationConfig with fallback category and example team-specific categories

INSERT INTO EmailNotificationConfig (FeedbackCategory, RecipientEmails, IsActive) VALUES
('All', 'admin@example.com', 1),
('Bug - Critical', 'admin@example.com;dev-team@example.com', 1),
('Bug - High', 'admin@example.com;dev-team@example.com', 1),
('Integration Issue (Infor Visual)', 'visual-team@example.com;admin@example.com', 1)
ON DUPLICATE KEY UPDATE
    RecipientEmails = VALUES(RecipientEmails),
    IsActive = VALUES(IsActive);
