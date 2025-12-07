-- Seed WindowFormMapping with all forms from FR-001 table

INSERT INTO WindowFormMapping (CodebaseName, UserFriendlyName, IsActive) VALUES
('MainForm', 'Main Form', 1),
('SettingsForm', 'Settings', 1),
('PrintForm', 'Print', 1),
('Transactions', 'Transaction History', 1),
('TransactionLifecycleForm', 'Transaction Lifecycle', 1),
('Form_InforVisualDashboard', 'Infor Visual Dashboard', 1),
('Form_WIPUserAnalytics', 'WIP User Analytics', 1),
('Form_AnalyticsViewer', 'Analytics Viewer', 1),
('Form_ViewLogsForm', 'View Logs', 1),
('Form_ViewErrorReports', 'View Error Reports', 1),
('SettingsForm_ViewReleaseNotesHTML', 'Release Notes', 1),
('Form_QuickButtonEdit', 'Quick Button Editor', 1),
('Form_ShortcutEdit', 'Shortcut Editor', 1),
('Form_PODetails', 'Purchase Order Details', 1),
('HelpViewerForm', 'Help Viewer', 1)
ON DUPLICATE KEY UPDATE
    UserFriendlyName = VALUES(UserFriendlyName),
    IsActive = VALUES(IsActive);
