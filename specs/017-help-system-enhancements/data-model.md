# Data Model (Phase 1)

## Entities

### UserFeedback
- **Keys**: FeedbackID (PK, IDENTITY), TrackingNumber (UNIQUE)
- **Fields**: FeedbackType, UserID, SubmissionDateTime, LastUpdatedDateTime, ApplicationVersion, OSVersion, MachineIdentifier, WindowForm, ActiveSection, Category, CustomCategory, Severity, Priority, Title, Description, StepsToReproduce, ExpectedBehavior, ActualBehavior, BusinessJustification, AffectedUsers, Location1, Location2, ExpectedConsistency, Status, AssignedToDeveloperID, DeveloperNotes, ResolutionDateTime, IsDuplicate, DuplicateOfFeedbackID
- **Relationships**: FK to Users (UserID, AssignedToDeveloperID), self-FK (DuplicateOfFeedbackID)
- **Validation**: Required fields per form type; Description ≤ 50,000 chars; CustomCategory required when Category='Other'; user-friendly names only; role validation on AssignedToDeveloperID; XSS/SQL injection prevention (HtmlSanitizer + parameterized SPs)
- **States**: Status transitions New → In Review → In Progress → Resolved → Closed, plus Won't Fix; backward transitions allowed with justification (per FR-047 role-change handling)

### UserFeedbackComments
- **Keys**: CommentID (PK)
- **Fields**: FeedbackID, UserID, CommentDateTime, CommentText, IsInternalNote
- **Relationships**: FK to UserFeedback (cascade delete), FK to Users
- **Validation**: CommentText required; length practical limits (<10,000 chars recommended)

### WindowFormMapping
- **Keys**: MappingID (PK), CodebaseName (UNIQUE)
- **Fields**: CodebaseName, UserFriendlyName, IsActive, CreatedDateTime, LastModifiedDateTime
- **Relationships**: Parent for UserControlMapping
- **Validation**: CodebaseName must map to existing form; IsActive soft-delete

### UserControlMapping
- **Keys**: MappingID (PK), (WindowFormMappingID, CodebaseName) UNIQUE
- **Fields**: WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive, CreatedDateTime, LastModifiedDateTime
- **Relationships**: FK to WindowFormMapping (cascade delete)
- **Validation**: CodebaseName must map to existing control under parent form; IsActive soft-delete

### TrackingNumberSequence
- **Keys**: SequenceID (PK), (FeedbackType, Year) UNIQUE
- **Fields**: FeedbackType, Year, NextNumber, LastGeneratedDateTime
- **Relationships**: None
- **Validation**: FeedbackType constrained to Bug/Suggestion/Inconsistency/Question; Year = current year; increment atomic with retry on collision

### EmailNotificationConfig
- **Keys**: ConfigID (PK), FeedbackCategory (UNIQUE)
- **Fields**: FeedbackCategory, RecipientEmails, IsActive, CreatedDateTime, LastModifiedDateTime
- **Relationships**: None
- **Validation**: RecipientEmails semicolon-delimited, valid email format; default/fallback category 'All'

## Derived/Presentation Models
- View models for Contact Support forms (per type) containing validation annotations aligned with FR-040.
- Developer Tools grid view models: include Type, Status, Priority/Severity, User, Dates, Assigned Developer, Duplicate link, TrackingNumber.

## State & Transition Notes
- Tracking number generation: annual reset, retry up to 3 times on collision, log fatal on repeated failure.
- Role change handling: if user loses Developer role while Developer Tools open, surface error and disable controls (FR-047).

## Data Retention & Deletion
- Soft-delete via IsActive flags on mapping tables; duplicates retained and linked.
- Retention policy for feedback (3 years min) noted in constitution; implementation deferred to tasks.

## Current Database Inventory (mtm_wip_application_winforms)

Observed tables (live DB, 2025-12-07):
- app_themes
- debug_matching
- error_reports
- inv_inventory
- inv_inventory_batch_seq
- inv_transaction
- log_changelog
- log_error
- md_color_codes
- md_item_types
- md_locations
- md_operation_numbers
- md_part_ids
- sys_last_10_transactions
- sys_parameter_prefix_overrides
- sys_roles
- sys_shortcuts
- sys_user_roles
- sys_visual
- usr_dgv_settings
- usr_settings
- usr_user_shortcuts
- usr_users

Not yet present (to be added by this feature):
- UserFeedback
- UserFeedbackComments
- WindowFormMapping
- UserControlMapping
- TrackingNumberSequence
- EmailNotificationConfig
