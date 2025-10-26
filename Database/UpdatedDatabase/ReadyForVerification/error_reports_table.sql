-- =============================================
-- Table: error_reports
-- Description: Stores user-submitted error reports with contextual notes
-- Created: 2025-10-25
-- Feature: Error Reporting with User Notes & Offline Queue
-- =============================================

CREATE TABLE IF NOT EXISTS error_reports (
    ReportID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    ReportDate DATETIME NOT NULL,
    UserName VARCHAR(100) NOT NULL,
    AppVersion VARCHAR(50) NULL,
    ErrorType VARCHAR(255) NULL,
    ErrorSummary TEXT NULL,
    UserNotes TEXT NULL,
    TechnicalDetails TEXT NULL,
    CallStack TEXT NULL,
    Status ENUM('New', 'Reviewed', 'Resolved') NOT NULL DEFAULT 'New',
    ReviewedBy VARCHAR(100) NULL,
    ReviewedDate DATETIME NULL,
    DeveloperNotes TEXT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Create indexes for efficient querying
CREATE INDEX idx_user ON error_reports(UserName);
CREATE INDEX idx_date ON error_reports(ReportDate DESC);
CREATE INDEX idx_status ON error_reports(Status);

-- =============================================
-- Table Documentation
-- =============================================

-- COLUMNS:
--   ReportID (INT, PRIMARY KEY, AUTO_INCREMENT)
--     Unique identifier for each error report
--   
--   ReportDate (DATETIME, NOT NULL)
--     Timestamp when error occurred and report was submitted
--   
--   UserName (VARCHAR 100, NOT NULL, INDEXED)
--     Windows username of user who encountered error
--     Example: 'John.Smith'
--   
--   AppVersion (VARCHAR 50, NULL)
--     Application version at time of error
--     Example: '5.0.0.142'
--   
--   ErrorType (VARCHAR 255, NULL)
--     Exception type name
--     Example: 'NullReferenceException', 'SqlException'
--   
--   ErrorSummary (TEXT, NULL)
--     User-friendly error message (max 64KB)
--   
--   UserNotes (TEXT, NULL)
--     User-provided context describing what they were doing when error occurred
--     This is the KEY field for user input
--   
--   TechnicalDetails (TEXT, NULL)
--     Full exception details including inner exceptions
--   
--   CallStack (TEXT, NULL)
--     Complete stack trace at time of error
--   
--   Status (ENUM, NOT NULL, DEFAULT 'New', INDEXED)
--     Lifecycle tracking: 'New' → 'Reviewed' → 'Resolved'
--   
--   ReviewedBy (VARCHAR 100, NULL)
--     Developer username who reviewed the report
--   
--   ReviewedDate (DATETIME, NULL)
--     When report was marked as Reviewed
--   
--   DeveloperNotes (TEXT, NULL)
--     Internal notes from developers about resolution

-- INDEXES:
--   idx_user - Query reports by specific user
--   idx_date - Chronological report listing (DESC for recent-first)
--   idx_status - Filter by review status (New/Reviewed/Resolved)

-- VALIDATION RULES:
--   1. ReportDate must be provided (auto-set to NOW() by stored procedure)
--   2. UserName is required (enforced by stored procedure)
--   3. Status defaults to 'New' on creation
--   4. ReviewedBy should be set when Status changes to 'Reviewed' or 'Resolved'

-- USAGE:
--   Primary access via sp_error_reports_Insert stored procedure
--   No direct INSERT statements from application code

-- TESTING:
--   -- Verify table structure
--   DESCRIBE error_reports;
--   
--   -- Verify indexes
--   SHOW INDEX FROM error_reports;
--   
--   -- Test insert via stored procedure (see sp_error_reports_Insert.sql)
