-- Seed Test Data for error_reports table
-- Purpose: Add diverse error reports for filter testing
-- Created: 2025-10-26
-- Database: mtm_wip_application_winforms_test (or MTM_WIP_Application_Winforms)

USE mtm_wip_application_winforms_test;

-- Insert 50 diverse error reports spanning different dates, users, machines, statuses, and error types

INSERT INTO error_reports 
(ReportDate, UserName, MachineName, AppVersion, ErrorType, ErrorSummary, UserNotes, TechnicalDetails, CallStack, Status, ReviewedBy, ReviewedDate, DeveloperNotes)
VALUES

-- Recent errors (last 7 days) - Various users and machines
('2025-10-26 09:15:23', 'JOHNK', 'JOHNKSPC', '5.2.0', 'Exception', 'Failed to remove quick button', 'I was trying to delete an old quick button that I no longer use', 'System.Exception: Failed to remove quick button', '(No data provided)', 'New', NULL, NULL, NULL),
('2025-10-26 08:30:45', 'MARYS', 'SHOP-PC-01', '5.2.0', 'DatabaseError', 'Connection timeout when loading inventory', 'The inventory screen froze while I was checking part counts', 'MySql.Data.MySqlClient.MySqlException: Timeout expired waiting for connection', 'at MySql.Data.MySqlClient.MySqlConnection.Open()', 'New', NULL, NULL, NULL),
('2025-10-25 14:22:10', 'BOBR', 'SHOP-PC-02', '5.2.0', 'ValidationError', 'Invalid quantity entered for transfer', 'I accidentally typed letters instead of numbers in the quantity field', 'System.FormatException: Input string was not in correct format', 'at System.Number.StringToNumber()', 'Reviewed', 'JOHNK', '2025-10-25 15:00:00', 'Added input validation to prevent non-numeric entry'),
('2025-10-25 11:45:33', 'SARAHJ', 'OFFICE-PC-03', '5.2.0', 'NullReferenceException', 'Crash when viewing empty transaction history', 'I clicked on a part with no history and the app crashed', 'System.NullReferenceException: Object reference not set to an instance', 'at MTM_Inventory.TransactionHistory.Load()', 'Resolved', 'JOHNK', '2025-10-25 16:30:00', 'Fixed null check in transaction history loader'),
('2025-10-24 16:20:15', 'TOMMYL', 'WAREHOUSE-PC-01', '5.2.0', 'Exception', 'Quick button save failed', 'Tried to save a new quick button but got an error', 'System.IO.IOException: The process cannot access the file', 'at System.IO.File.WriteAllText()', 'New', NULL, NULL, NULL),

-- Mid-range errors (8-14 days ago) - Different machines
('2025-10-22 10:30:00', 'JOHNK', 'JOHNKSPC', '5.2.0', 'DatabaseError', 'Stored procedure timeout on large report', 'Generating the monthly inventory report took too long and timed out', 'MySql.Data.MySqlClient.MySqlException: Timeout expired', 'at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader()', 'Reviewed', 'JOHNK', '2025-10-22 14:00:00', 'Optimized stored procedure with indexes'),
('2025-10-21 13:15:44', 'MARYS', 'SHOP-PC-01', '5.1.9', 'Exception', 'Failed to update part location', 'Was moving parts from RECEIVING to FLOOR and got an error', 'System.Exception: Failed to update part location', '(No data provided)', 'New', NULL, NULL, NULL),
('2025-10-20 09:00:12', 'BOBR', 'SHOP-PC-02', '5.1.9', 'ValidationError', 'Cannot transfer negative quantity', 'System let me enter -10 in transfer quantity', 'System.ArgumentException: Quantity must be positive', 'at MTM_Inventory.Transfer.Validate()', 'Resolved', 'JOHNK', '2025-10-20 10:30:00', 'Added server-side validation'),
('2025-10-19 15:45:20', 'SARAHJ', 'OFFICE-PC-03', '5.1.9', 'NullReferenceException', 'Crash on user settings page', 'Opened settings and app froze then crashed', 'System.NullReferenceException: Object reference not set', 'at MTM_Inventory.Settings.LoadUserPreferences()', 'Reviewed', 'JOHNK', '2025-10-19 16:00:00', 'Investigating settings loader'),
('2025-10-18 11:22:55', 'TOMMYL', 'WAREHOUSE-PC-01', '5.1.9', 'DatabaseError', 'Duplicate key error when adding inventory', 'Tried to add inventory but got duplicate error', 'MySql.Data.MySqlClient.MySqlException: Duplicate entry', 'at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()', 'Resolved', 'JOHNK', '2025-10-18 14:00:00', 'Fixed primary key conflict in insert procedure'),

-- Older errors (15-30 days ago) - Various error types
('2025-10-15 14:30:00', 'LINDAP', 'RECEIVING-PC-01', '5.1.8', 'Exception', 'Failed to scan barcode', 'Barcode scanner not responding when receiving parts', 'System.IO.IOException: COM port access denied', 'at System.IO.Ports.SerialPort.Open()', 'New', NULL, NULL, NULL),
('2025-10-14 10:15:30', 'MIKEC', 'SHIPPING-PC-01', '5.1.8', 'ValidationError', 'Invalid part number format', 'Entered part number but system said format was wrong', 'System.FormatException: Part number must match pattern', 'at MTM_Inventory.PartValidator.Validate()', 'Reviewed', 'JOHNK', '2025-10-14 11:00:00', 'Documented valid part number format'),
('2025-10-13 16:45:10', 'JOHNK', 'JOHNKSPC', '5.1.8', 'NullReferenceException', 'Crash when printing report with no data', 'Tried to print empty report and app crashed', 'System.NullReferenceException: Object reference not set', 'at MTM_Inventory.Printing.PrintReport()', 'Resolved', 'JOHNK', '2025-10-13 17:00:00', 'Added null check before printing'),
('2025-10-12 09:20:40', 'MARYS', 'SHOP-PC-01', '5.1.8', 'DatabaseError', 'Deadlock detected during inventory update', 'Multiple users updating inventory caused deadlock', 'MySql.Data.MySqlClient.MySqlException: Deadlock found', 'at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()', 'Reviewed', 'JOHNK', '2025-10-12 10:00:00', 'Implementing retry logic'),
('2025-10-11 13:50:25', 'BOBR', 'SHOP-PC-02', '5.1.8', 'Exception', 'Failed to export to Excel', 'Clicked export button but file was not created', 'System.UnauthorizedAccessException: Access to path denied', 'at System.IO.File.WriteAllBytes()', 'New', NULL, NULL, NULL),

-- Old errors (31-60 days ago) - Mixing all statuses
('2025-10-08 11:00:00', 'SARAHJ', 'OFFICE-PC-03', '5.1.7', 'DatabaseError', 'Cannot connect to database server', 'App shows connection error on startup', 'MySql.Data.MySqlClient.MySqlException: Unable to connect', 'at MySql.Data.MySqlClient.MySqlConnection.Open()', 'Resolved', 'JOHNK', '2025-10-08 12:00:00', 'Fixed connection string configuration'),
('2025-10-05 14:22:15', 'TOMMYL', 'WAREHOUSE-PC-01', '5.1.7', 'ValidationError', 'Transfer between same locations not allowed', 'Tried to transfer from FLOOR to FLOOR', 'System.ValidationException: Source and destination cannot be same', '(No data provided)', 'Reviewed', 'JOHNK', '2025-10-05 15:00:00', 'Added UI validation to prevent same-location transfer'),
('2025-10-03 09:45:30', 'LINDAP', 'RECEIVING-PC-01', '5.1.7', 'NullReferenceException', 'Crash when user profile incomplete', 'New user account missing required fields', 'System.NullReferenceException: User profile data is null', 'at MTM_Inventory.User.LoadProfile()', 'Resolved', 'JOHNK', '2025-10-03 10:30:00', 'Added default values for new user profiles'),
('2025-10-01 16:10:45', 'MIKEC', 'SHIPPING-PC-01', '5.1.7', 'Exception', 'Failed to generate shipping label', 'Printer error when generating label', 'System.Printing.PrintException: Printer not ready', 'at System.Printing.PrintQueue.AddJob()', 'New', NULL, NULL, NULL),
('2025-09-28 10:30:20', 'JOHNK', 'JOHNKSPC', '5.1.7', 'DatabaseError', 'Transaction log full error', 'Database transaction log exceeded size limit', 'MySql.Data.MySqlClient.MySqlException: Transaction log full', '(No data provided)', 'Reviewed', 'JOHNK', '2025-09-28 11:00:00', 'Scheduled transaction log maintenance'),

-- Additional diverse errors for thorough testing
('2025-10-26 07:15:00', 'AMYH', 'QUALITY-PC-01', '5.2.0', 'ValidationError', 'QC inspection count exceeds available quantity', 'Tried to inspect 100 units but only 50 available', 'System.ValidationException: Inspection quantity exceeds available', 'at MTM_Inventory.QualityControl.ValidateQuantity()', 'New', NULL, NULL, NULL),
('2025-10-25 12:30:45', 'STEVEB', 'SHOP-PC-03', '5.2.0', 'Exception', 'Cannot delete quick button in use', 'Button is assigned to active work orders', 'System.Exception: Quick button currently in use', '(No data provided)', 'New', NULL, NULL, NULL),
('2025-10-24 08:45:22', 'AMYH', 'QUALITY-PC-01', '5.2.0', 'NullReferenceException', 'Crash when opening work order with no parts', 'Empty work order caused crash', 'System.NullReferenceException: Part list is null', 'at MTM_Inventory.WorkOrder.LoadParts()', 'Reviewed', 'JOHNK', '2025-10-24 10:00:00', 'Added empty list handling'),
('2025-10-23 15:20:10', 'STEVEB', 'SHOP-PC-03', '5.2.0', 'DatabaseError', 'Query timeout on large inventory report', 'Report with 50,000+ records times out', 'MySql.Data.MySqlClient.MySqlException: Timeout expired', 'at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader()', 'Resolved', 'JOHNK', '2025-10-23 16:00:00', 'Added pagination to large reports'),
('2025-10-22 11:05:35', 'MARYS', 'SHOP-PC-01', '5.2.0', 'ValidationError', 'Part number contains invalid characters', 'Part number has special symbols not allowed', 'System.FormatException: Invalid characters in part number', 'at MTM_Inventory.PartValidator.ValidateFormat()', 'Reviewed', 'JOHNK', '2025-10-22 12:00:00', 'Updated validation rules documentation'),

-- Edge cases and unusual scenarios
('2025-10-21 07:00:00', 'LINDAP', 'RECEIVING-PC-01', '5.1.9', 'Exception', 'Memory error when loading large dataset', 'App becomes unresponsive with 100K+ records', 'System.OutOfMemoryException: Insufficient memory', 'at System.Data.DataTable.Load()', 'New', NULL, NULL, NULL),
('2025-10-20 14:30:15', 'MIKEC', 'SHIPPING-PC-01', '5.1.9', 'DatabaseError', 'Foreign key constraint violation', 'Cannot delete record referenced by other tables', 'MySql.Data.MySqlClient.MySqlException: Foreign key constraint fails', 'at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()', 'Resolved', 'JOHNK', '2025-10-20 15:00:00', 'Implemented cascade delete'),
('2025-10-19 09:15:40', 'AMYH', 'QUALITY-PC-01', '5.1.9', 'NullReferenceException', 'Crash on malformed JSON settings', 'Corrupted user settings file', 'System.NullReferenceException: Cannot parse settings JSON', 'at Newtonsoft.Json.JsonConvert.DeserializeObject()', 'Reviewed', 'JOHNK', '2025-10-19 10:00:00', 'Added JSON validation with fallback defaults'),
('2025-10-18 16:45:50', 'STEVEB', 'SHOP-PC-03', '5.1.9', 'ValidationError', 'Date in future not allowed', 'Entered tomorrow date for completed transaction', 'System.ValidationException: Date cannot be in future', '(No data provided)', 'Resolved', 'JOHNK', '2025-10-18 17:00:00', 'Added date range validation'),
('2025-10-17 10:20:30', 'JOHNK', 'JOHNKSPC', '5.1.9', 'Exception', 'File locked by another process', 'Cannot save report - file in use', 'System.IO.IOException: File in use by another process', 'at System.IO.File.WriteAllText()', 'New', NULL, NULL, NULL),

-- More variations for comprehensive filter testing
('2025-10-16 13:40:25', 'MARYS', 'SHOP-PC-01', '5.1.8', 'DatabaseError', 'Index corruption detected', 'Database index needs rebuild', 'MySql.Data.MySqlClient.MySqlException: Index corrupted', '(No data provided)', 'Reviewed', 'JOHNK', '2025-10-16 14:00:00', 'Scheduled database maintenance'),
('2025-10-15 08:25:10', 'BOBR', 'SHOP-PC-02', '5.1.8', 'Exception', 'Network share unavailable', 'Cannot access shared drive for export', 'System.IO.IOException: Network path not found', 'at System.IO.Directory.Exists()', 'New', NULL, NULL, NULL),
('2025-10-14 15:50:45', 'SARAHJ', 'OFFICE-PC-03', '5.1.8', 'ValidationError', 'Email format invalid', 'Notification email address malformed', 'System.FormatException: Invalid email format', 'at System.Net.Mail.MailAddress..ctor()', 'Resolved', 'JOHNK', '2025-10-14 16:00:00', 'Added email validation regex'),
('2025-10-13 11:15:30', 'TOMMYL', 'WAREHOUSE-PC-01', '5.1.8', 'NullReferenceException', 'Report parameter missing', 'Required report parameter not provided', 'System.NullReferenceException: Parameter is null', 'at MTM_Inventory.Reports.GenerateReport()', 'Reviewed', 'JOHNK', '2025-10-13 12:00:00', 'Added parameter validation'),
('2025-10-12 14:35:20', 'LINDAP', 'RECEIVING-PC-01', '5.1.8', 'DatabaseError', 'Connection pool exhausted', 'Too many concurrent database connections', 'MySql.Data.MySqlClient.MySqlException: Connection pool exhausted', 'at MySql.Data.MySqlClient.MySqlConnection.Open()', 'Resolved', 'JOHNK', '2025-10-12 15:00:00', 'Increased connection pool size'),

-- Final batch - variety of machines and users
('2025-10-11 09:40:15', 'MIKEC', 'SHIPPING-PC-01', '5.1.8', 'Exception', 'Barcode generation failed', 'Cannot generate barcode for part', 'System.Exception: Barcode generation error', 'at MTM_Inventory.Barcode.Generate()', 'New', NULL, NULL, NULL),
('2025-10-10 12:25:50', 'AMYH', 'QUALITY-PC-01', '5.1.7', 'ValidationError', 'Measurement out of tolerance', 'QC measurement exceeds specification limits', 'System.ValidationException: Value out of tolerance range', '(No data provided)', 'Reviewed', 'JOHNK', '2025-10-10 13:00:00', 'Updated tolerance ranges in system'),
('2025-10-09 16:10:35', 'STEVEB', 'SHOP-PC-03', '5.1.7', 'DatabaseError', 'Trigger execution failed', 'Database trigger error during insert', 'MySql.Data.MySqlClient.MySqlException: Trigger error', 'at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()', 'Resolved', 'JOHNK', '2025-10-09 17:00:00', 'Fixed trigger logic'),
('2025-10-08 08:30:20', 'JOHNK', 'JOHNKSPC', '5.1.7', 'NullReferenceException', 'Theme settings not loaded', 'User theme preferences missing', 'System.NullReferenceException: Theme settings null', 'at MTM_Inventory.Core_Themes.ApplyTheme()', 'Reviewed', 'JOHNK', '2025-10-08 09:00:00', 'Added default theme fallback'),
('2025-10-07 13:45:10', 'MARYS', 'SHOP-PC-01', '5.1.7', 'Exception', 'DPI scaling issue on 4K display', 'Controls overlapping on high-res monitor', 'System.Exception: DPI scaling calculation error', 'at MTM_Inventory.Core_Themes.ApplyDpiScaling()', 'New', NULL, NULL, NULL),

-- Last few for good measure
('2025-10-06 10:20:40', 'BOBR', 'SHOP-PC-02', '5.1.7', 'ValidationError', 'Unsupported file format for import', 'Tried to import CSV with wrong format', 'System.FormatException: Unsupported import format', 'at MTM_Inventory.Import.ParseFile()', 'Resolved', 'JOHNK', '2025-10-06 11:00:00', 'Added format validation with helpful error message'),
('2025-10-05 15:35:25', 'SARAHJ', 'OFFICE-PC-03', '5.1.7', 'DatabaseError', 'Table lock wait timeout', 'Cannot update record - table locked', 'MySql.Data.MySqlClient.MySqlException: Lock wait timeout', 'at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()', 'Reviewed', 'JOHNK', '2025-10-05 16:00:00', 'Optimizing transaction scope'),
('2025-10-04 09:50:15', 'TOMMYL', 'WAREHOUSE-PC-01', '5.1.7', 'Exception', 'PDF generation failed', 'Cannot create PDF report', 'System.Exception: PDF library error', 'at iTextSharp.PdfWriter.GetInstance()', 'New', NULL, NULL, NULL),
('2025-10-03 14:15:30', 'LINDAP', 'RECEIVING-PC-01', '5.1.7', 'NullReferenceException', 'User permission check failed', 'Permission object not initialized', 'System.NullReferenceException: Permission object null', 'at MTM_Inventory.Security.CheckPermission()', 'Resolved', 'JOHNK', '2025-10-03 15:00:00', 'Fixed permission loader initialization'),
('2025-10-02 11:30:45', 'MIKEC', 'SHIPPING-PC-01', '5.1.7', 'ValidationError', 'Shipping address incomplete', 'Required address fields missing', 'System.ValidationException: Address validation failed', '(No data provided)', 'Reviewed', 'JOHNK', '2025-10-02 12:00:00', 'Added address field validation to UI');

-- Verify the inserts
SELECT 
    COUNT(*) as TotalReports,
    COUNT(CASE WHEN Status = 'New' THEN 1 END) as NewCount,
    COUNT(CASE WHEN Status = 'Reviewed' THEN 1 END) as ReviewedCount,
    COUNT(CASE WHEN Status = 'Resolved' THEN 1 END) as ResolvedCount,
    COUNT(DISTINCT UserName) as UniqueUsers,
    COUNT(DISTINCT MachineName) as UniqueMachines,
    MIN(ReportDate) as OldestReport,
    MAX(ReportDate) as NewestReport
FROM error_reports;

-- Show sample of different filters
SELECT 'Recent New Reports (Last 7 days)' as TestCase, COUNT(*) as Count
FROM error_reports 
WHERE Status = 'New' AND ReportDate >= DATE_SUB(NOW(), INTERVAL 7 DAY)
UNION ALL
SELECT 'JOHNK Reports', COUNT(*)
FROM error_reports WHERE UserName = 'JOHNK'
UNION ALL
SELECT 'SHOP-PC-01 Reports', COUNT(*)
FROM error_reports WHERE MachineName = 'SHOP-PC-01'
UNION ALL
SELECT 'DatabaseError Type', COUNT(*)
FROM error_reports WHERE ErrorType = 'DatabaseError'
UNION ALL
SELECT 'Contains "database" in text', COUNT(*)
FROM error_reports 
WHERE ErrorSummary LIKE '%database%' 
   OR UserNotes LIKE '%database%' 
   OR TechnicalDetails LIKE '%database%';
