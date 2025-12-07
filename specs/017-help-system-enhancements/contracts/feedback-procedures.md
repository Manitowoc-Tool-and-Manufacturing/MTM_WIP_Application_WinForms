# Contracts: Feedback Stored Procedures (Phase 1)

All procedures return `p_Status INT` (0=success) and `p_ErrorMsg NVARCHAR(500)` outputs. DB: MySQL 5.7.24 (no JSON/CTE/window functions).

**File locations (Database folder)**
- UserFeedback and UserFeedbackComments procedures: place .sql files under `Database/UpdatedStoredProcedures/feedback/` (e.g., `md_feedback_Insert.sql`).
- WindowFormMapping and UserControlMapping procedures: place .sql files under `Database/UpdatedStoredProcedures/system/`.
- TrackingNumberSequence procedure: place .sql file under `Database/UpdatedStoredProcedures/system/`.
- EmailNotificationConfig procedures: place .sql files under `Database/UpdatedStoredProcedures/system/`.

## UserFeedback
- **md_feedback_Insert**(FeedbackType, UserID, WindowForm, ActiveSection, Category, CustomCategory, Severity, Priority, Title, Description, StepsToReproduce, ExpectedBehavior, ActualBehavior, BusinessJustification, AffectedUsers, Location1, Location2, ExpectedConsistency, ApplicationVersion, OSVersion, MachineIdentifier, OUT p_FeedbackID, OUT p_TrackingNumber)
- **md_feedback_GetAll**(FilterStatus?, FilterFeedbackType?, FilterUserID?, FilterDateFrom?, FilterDateTo?, FilterAssignedDeveloperID?, FilterCategory?)
- **md_feedback_GetByUser**(UserID)
- **md_feedback_GetById**(FeedbackID)
- **md_feedback_UpdateStatus**(FeedbackID, NewStatus, AssignedToDeveloperID?, DeveloperNotes?, ModifiedByUserID)
- **md_feedback_MarkDuplicate**(FeedbackID, DuplicateOfFeedbackID, ModifiedByUserID)
- **md_feedback_ExportToCsv**(FilterStatus?, FilterFeedbackType?, FilterUserID?, FilterDateFrom?, FilterDateTo?)

## UserFeedbackComments
- **md_feedback_comment_Insert**(FeedbackID, UserID, CommentText, IsInternalNote, OUT p_CommentID)
- **md_feedback_comment_GetByFeedbackId**(FeedbackID)

## Window/Form Mappings
- **sys_windowform_mapping_GetAll**(IncludeInactive?)
- **sys_usercontrol_mapping_GetByWindow**(WindowFormMappingID, IncludeInactive?)
- **sys_windowform_mapping_Upsert**(CodebaseName, UserFriendlyName, IsActive, OUT p_MappingID)
- **sys_usercontrol_mapping_Upsert**(WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive, OUT p_MappingID)

## Tracking Numbers
- **sys_tracking_number_GetNext**(FeedbackType, Year, OUT p_TrackingNumber)

## Email Notifications
- **sys_email_notification_GetRecipients**(FeedbackCategory, OUT p_RecipientEmails)
- **sys_email_notification_Upsert**(FeedbackCategory, RecipientEmails, IsActive)

## Contracts Expectations
- All callers use `Helper_Database_StoredProcedure` with async execution.
- Validation occurs before calls (Service_Validation), plus DB constraints enforce integrity.
- Service_ErrorHandler handles non-zero p_Status with user-friendly messaging.
