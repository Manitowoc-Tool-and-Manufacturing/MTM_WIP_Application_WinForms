-- =============================================
-- Stored Procedure: sp_error_reports_Insert
-- Description: Inserts a new error report with user notes
-- Created: 2025-10-25
-- Feature: Error Reporting with User Notes & Offline Queue
-- =============================================

DELIMITER $$

CREATE PROCEDURE sp_error_reports_Insert(
    IN p_UserName VARCHAR(100),
    IN p_AppVersion VARCHAR(50),
    IN p_ErrorType VARCHAR(255),
    IN p_ErrorSummary TEXT,
    IN p_UserNotes TEXT,
    IN p_TechnicalDetails TEXT,
    IN p_CallStack TEXT,
    OUT p_ReportID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Declare variables
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while inserting error report';
        SET p_ReportID = NULL;
    END;
    
    -- Validate required parameters
    IF p_UserName IS NULL OR TRIM(p_UserName) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserName is required';
        SET p_ReportID = NULL;
    ELSE
        START TRANSACTION;
        
        -- Insert error report
        INSERT INTO error_reports (
            ReportDate,
            UserName,
            AppVersion,
            ErrorType,
            ErrorSummary,
            UserNotes,
            TechnicalDetails,
            CallStack,
            Status
        ) VALUES (
            NOW(),
            p_UserName,
            p_AppVersion,
            p_ErrorType,
            p_ErrorSummary,
            p_UserNotes,
            p_TechnicalDetails,
            p_CallStack,
            'New'
        );
        
        -- Get the generated ReportID
        SET p_ReportID = LAST_INSERT_ID();
        
        -- Success
        SET p_Status = 0;
        SET p_ErrorMsg = 'Error report inserted successfully';
        
        COMMIT;
    END IF;
END$$

DELIMITER ;

-- =============================================
-- Contract Documentation
-- =============================================

-- INPUT PARAMETERS:
--   p_UserName (VARCHAR 100, REQUIRED)
--     Windows username of user submitting report
--     Example: 'John.Smith'
--
--   p_AppVersion (VARCHAR 50, OPTIONAL)
--     Application version at time of error
--     Example: '5.0.0.142'
--
--   p_ErrorType (VARCHAR 255, OPTIONAL)
--     Exception type name
--     Example: 'NullReferenceException'
--
--   p_ErrorSummary (TEXT, OPTIONAL)
--     User-friendly error message (max 64KB)
--     Example: 'Object reference not set to an instance of an object.'
--
--   p_UserNotes (TEXT, OPTIONAL)
--     User-provided context describing what they were doing
--     Example: 'I was clicking the Save button after entering inventory data.'
--
--   p_TechnicalDetails (TEXT, OPTIONAL)
--     Full exception details including inner exceptions
--     Example: Full exception.ToString() output
--
--   p_CallStack (TEXT, OPTIONAL)
--     Complete stack trace
--     Example: exception.StackTrace value

-- OUTPUT PARAMETERS:
--   p_ReportID (INT)
--     Generated ReportID on success, NULL on failure
--     Example: 12345
--
--   p_Status (INT)
--     Status code indicating result:
--       0 = Success
--      -1 = Database error (SQLEXCEPTION)
--      -2 = Validation error (UserName missing)
--
--   p_ErrorMsg (VARCHAR 500)
--     Human-readable status message
--     Examples:
--       'Error report inserted successfully'
--       'UserName is required'
--       'Database error occurred while inserting error report'

-- USAGE EXAMPLE (Direct Call):
--   CALL sp_error_reports_Insert(
--       'John.Smith',                              -- p_UserName
--       '5.0.0.142',                               -- p_AppVersion
--       'NullReferenceException',                  -- p_ErrorType
--       'Object reference not set...',             -- p_ErrorSummary
--       'I was clicking the Save button...',       -- p_UserNotes
--       'Full exception details...',               -- p_TechnicalDetails
--       'at MTM.Forms.MainForm.btnSave_Click...', -- p_CallStack
--       @reportID,                                 -- OUT p_ReportID
--       @status,                                   -- OUT p_Status
--       @errorMsg                                  -- OUT p_ErrorMsg
--   );
--   SELECT @reportID, @status, @errorMsg;

-- USAGE EXAMPLE (C# via Helper_Database_StoredProcedure):
--   var parameters = new Dictionary<string, object>
--   {
--       ["UserName"] = userName,          // No p_ prefix in C#
--       ["AppVersion"] = appVersion,
--       ["ErrorType"] = errorType,
--       ["ErrorSummary"] = errorSummary,
--       ["UserNotes"] = userNotes,
--       ["TechnicalDetails"] = technicalDetails,
--       ["CallStack"] = callStack
--   };
--   
--   var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
--       connectionString,
--       "sp_error_reports_Insert",
--       parameters,
--       useAsync: true
--   );
--   
--   if (result.IsSuccess)
--   {
--       int reportID = Convert.ToInt32(result.OutputParameters["ReportID"]);
--       // Success handling
--   }

-- OFFLINE QUEUE USAGE (Generated SQL File):
--   -- File: ErrorReport_20251025_143022_JohnSmith_a3f8b2.sql
--   START TRANSACTION;
--   
--   CALL sp_error_reports_Insert(
--       'John.Smith',
--       'WORKSTATION-05',
--       '5.0.0.142',
--       'NullReferenceException',
--       'Object reference not set...',
--       'User was clicking Save button...',
--       'Full technical details...',
--       'Stack trace...',
--       @reportID,
--       @status,
--       @errorMsg
--   );
--   
--   SELECT @status AS Status, @errorMsg AS Message, @reportID AS ReportID;
--   COMMIT;

-- VALIDATION RULES:
--   1. UserName is required (cannot be NULL or empty string)
--   2. All TEXT fields accept NULL (optional)
--   3. Transaction ensures atomicity
--   4. LAST_INSERT_ID() captures auto-generated ReportID
--   5. Default Status = 'New' on creation

-- PERFORMANCE CONSIDERATIONS:
--   - Single INSERT operation (minimal overhead)
--   - Indexes on UserName, ReportDate, Status speed up queries
--   - TEXT fields may impact storage for very large call stacks
--   - Expected latency: <100ms for typical error reports

-- SECURITY CONSIDERATIONS:
--   - Parameters are properly typed (no SQL injection risk)
--   - No sensitive data logged (passwords, secrets excluded)
--   - UserName is Windows account (not personally identifiable)
--   - Stack traces may contain file paths (review sensitivity)

-- TESTING SCENARIOS:
--   1. Happy path: All parameters provided
--   2. Minimal: Only UserName provided (all others NULL)
--   3. Validation: UserName = NULL or empty string
--   4. Large data: TechnicalDetails/CallStack near 64KB TEXT limit
--   5. Special characters: UserNotes with quotes, newlines, unicode
--   6. Concurrent inserts: Multiple users submitting simultaneously
