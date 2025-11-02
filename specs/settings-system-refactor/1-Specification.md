# Settings System - Feature Specification

**Version**: 2.0.0  
**Status**: Specification Finalized - Ready for Implementation  
**Created**: 2025-11-02  
**Last Updated**: 2025-11-02  
**Feature Category**: Application Configuration & User Management  
**Priority**: High  
**Complexity**: High  

---

## Executive Summary

Complete redesign of the Settings system for the MTM WIP Application. The current system (v1.0) consists of 19 UserControls scattered across Settings panels with inconsistent patterns, fragmented state management, and tightly-coupled dependencies. The new system (v2.0) will provide a unified, maintainable, and extensible settings architecture supporting user preferences, master data management (CRUD operations for Users, Parts, Locations, Operations, ItemTypes), system configuration, and appearance customization.

### Architectural Decisions (Finalized)

All architectural questions have been resolved. See `2-Clarification-Questions.md` for detailed rationale.

**Storage Architecture**:
- **Hybrid Approach**: Database primary storage with local JSON file fallback for offline resilience
- **Three-Tier Inheritance**: User Settings → Global Settings (appsettings.json) → Built-in Defaults
- **No Migration**: Settings reset to defaults on schema version mismatch (simple, reliable)

**Session Management**:
- **Single Instance**: Block multiple concurrent logins per user with remote logout capability
- **Admin Controls**: Force-close individual or all user sessions via dashboard
- **Session Timeout**: 55-minute warning with extension prompt, graceful close at 60 minutes
- **Manual Save**: User explicitly clicks Save button, with unsaved changes indicator

**Theme System**:
- **Read-Only Selection**: Choose from 9 predefined themes (MVP), no custom theme editor
- **Audit & Consolidate**: Reduce 203 color tokens to 50-80 essential colors
- **System DPI Only**: No user font size overrides, follow Windows display settings
- **Immediate Application**: Theme changes apply instantly to all forms without restart

**Quick Buttons**:
- **LRU Replacement**: Replace least recently used button with confirmation when capacity reached (max 10)
- **No Validation**: Errors caught at execution time by existing DAO error handling (MVP)
- **Database Sync**: No import/export needed, buttons available on any workstation via login

**UI/UX Design**:
- **Hybrid Navigation**: TreeView with expandable categories + top search filter
- **Inline Validation**: ErrorProvider on field exit with Apply button disabled until valid
- **Category Reset**: Reset per category or reset all, with confirmation dialogs

**Error Handling**:
- **Validate on Apply**: All settings validated together, rollback on error
- **Retry + Fallback**: Database error → retry once → fallback to defaults with user notification
- **No Audit Log**: Settings changes not logged (MVP), can add later if compliance requires

### Success Criteria

- **Unified Architecture**: All settings managed through a consistent Service layer
- **Type Safety**: Strongly-typed settings models with validation
- **Performance**: Settings load in under 500ms, save operations complete in under 1000ms
- **User Experience**: Intuitive navigation, real-time validation, instant preview for appearance changes
- **Maintainability**: Clear separation of concerns, documented patterns, extensible design
- **Data Integrity**: Atomic save operations, rollback on error, active session management

---

## Problem Statement

### Current State (v1.0) Analysis

**Research Summary** (from comprehensive codebase analysis):
- **19 UserControls**: 5 CRUD entities (User, PartID, Location, Operation, ItemType) with Add/Edit/Remove operations, plus 5 settings panels (Database, Theme, Shortcuts, About, Developer Tools)
- **Fragmented State**: Settings scattered across `Model_AppVariables`, `Model_Users`, `Dao_User` (18 getter/setter pairs), and direct database access
- **Inconsistent Patterns**: Each control implements its own save/load logic with varying error handling and validation approaches
- **Database Schema**: 10 primary tables (usr_users, usr_ui_settings, md_part_ids, md_locations, md_operation_numbers, md_item_types, sys_roles, sys_user_roles, sys_parameter_prefix_overrides)
- **Integration Points**: 23 files integrate with Theme System, 15 with Error Handling, 4 with Progress Reporting, 4 with Shortcut System

### Key Pain Points

1. **Coupling**: Direct DAO calls from UI controls bypass business logic layer
2. **Validation**: Inconsistent validation rules applied at different layers
3. **State Management**: No single source of truth for current settings state
4. **Error Handling**: Varied approaches across controls (MessageBox, status labels, Service_ErrorHandler)
5. **Testing**: Tight coupling makes unit testing impractical
6. **Navigation**: TreeView-based navigation lacks search, recent items, or favorites
7. **Performance**: Redundant database queries, no caching strategy
8. **Accessibility**: No keyboard shortcuts for common operations, poor screen reader support
9. **Audit Trail**: No comprehensive logging of who changed what when

---

## Feature Overview

### High-Level Architecture

The Settings System v2.0 introduces a layered architecture:

**Presentation Layer**: SettingsForm with plugin-based panel system  
**Service Layer**: Service_SettingsManager coordinates all settings operations  
**Data Access Layer**: Specialized DAOs for each entity type  
**Model Layer**: Strongly-typed settings classes with validation  
**Database Layer**: Normalized schema with audit triggers  

### Core Components

1. **Service_SettingsManager**: Central coordinator for all settings operations
2. **Settings Models**: Typed classes (UserSettings, ThemeSettings, DatabaseSettings, MasterDataSettings)
3. **Settings Panels**: Modular UI panels implementing ISettingsPanel interface
4. **Settings Repository**: Unified database access with caching
5. **Validation Engine**: Centralized validation rules with extensible rule system
6. **Audit Service**: Comprehensive change tracking for compliance

---

## Functional Requirements

### FR1: User Management Settings

**Description**: CRUD operations for user accounts with role-based access control

**Entities**: User accounts with properties (Username, Pin, FirstName, LastName, Shift, UserType, VisualAccess, ThemePreference)

**Operations**:
- **Add User**: Create new user with validation (unique username, valid PIN format, required fields)
- **Edit User**: Modify existing user properties with permission checks
- **Remove User**: Soft-delete user (archive) with confirmation dialog
- **User Roles**: Assign/revoke roles (Developer, Admin, ReadOnly, Normal)
- **Password Management**: Reset PIN, update Visual access credentials

**Validation Rules**:
- Username: Alphanumeric, 3-20 characters, no spaces, unique
- PIN: 4-6 digits, no sequential patterns (1234, 1111)
- Shift: Must be from predefined list (First, Second, Third, Weekend)
- User Type: At least one must be selected, mutual exclusivity rules apply

**Security**: Audit trail required, only Admin/Developer can add/edit/remove users

---

### FR2: Master Data Management

**Description**: CRUD operations for manufacturing master data entities

**Entities**: PartID, Location, Operation, ItemType

**Operations Per Entity**:
- **Add**: Create new record with validation
- **Edit**: Modify existing record with cascade update warnings
- **Remove**: Soft-delete with usage check (prevent if in active use)
- **Bulk Import**: CSV/Excel import with preview and validation
- **Export**: Export master data to CSV for backup/audit

**PartID Specific**:
- Fields: PartNumber, Description, ItemType, DefaultLocation, DefaultOperation
- Validation: Unique PartNumber, valid ItemType reference, optional defaults

**Location Specific**:
- Fields: LocationCode, Description, IsActive, Capacity
- Validation: Unique code, alphanumeric 2-10 chars, capacity > 0 if specified

**Operation Specific**:
- Fields: OperationNumber, Description, IsActive, Sequence
- Validation: Numeric operation number, must be in ValidOperations config or explicitly added

**ItemType Specific**:
- Fields: TypeCode, Description, Category, IsActive
- Validation: Unique code, predefined categories (Raw Material, WIP, Finished Goods)

**Common Requirements**:
- All entities support IsActive flag for soft deletion
- Edit operations warn if entity is referenced in active transactions
- Bulk operations provide progress indicators and rollback on partial failure

---

### FR3: Appearance Settings

**Description**: Theme selection, UI customization, and visual preferences

**Theme Management** (Decision: Read-Only Selection):
- Select from 9 available themes (Arctic, Default, Fire Storm, Glacier, Ice, Midnight, Neon, Sunset, Wood)
- Preview theme before saving (instant visual feedback)
- Per-user theme persistence in database
- Apply theme immediately to all open forms without restart
- **Note**: No custom theme editor in MVP (Phase 1), administrator can add themes via database insert

**DPI Scaling** (Decision: System DPI Only):
- Application respects Windows system DPI settings automatically via `Core_Themes.ApplyDpiScaling()`
- No user font size multiplier in application
- Users needing larger fonts adjust Windows display settings (100%, 125%, 150%, 200%)
- Follows Windows accessibility standards

**Color Token Optimization** (Decision: Audit & Consolidate):
- Reduce from 203 color properties to 50-80 essential colors
- Remove unused tokens based on code usage analysis
- Document which controls use which tokens
- Improves performance and reduces database bloat

**Layout Preferences**:
- DataGridView column visibility and order
- Panel expansion states saved per form
- Splitter positions persisted per user
- Window size/position restoration on startup

---

### FR4: Database Connection Settings

**Description**: Configure database connection parameters

**Connection Parameters**:
- Server Address (default: localhost or 172.16.1.104 based on environment)
- Port (default: 3306)
- Database Name (default: MTM_WIP_Application_Winforms)
- Connection Timeout (default: 30 seconds)

**Features**:
- Test Connection button with detailed diagnostics
- Connection string preview (password masked)
- Environment-aware defaults (Debug vs Release)
- Validation: Numeric port (1-65535), reachable server, valid database name

**Security**:
- Passwords never stored in plain text
- Credentials validated against MySQL user permissions
- Connection strings encrypted at rest
- Admin/Developer roles required to modify

**Application Restart** (Decision: Restart Required for Critical Settings):
- Database connection changes require application restart
- Prompt user with restart dialog showing: "Database settings changed. Restart required to take effect. [Restart Now] [Restart Later]"
- Save current work before restart option
- Theme/UI settings apply immediately without restart

---

### FR5: Keyboard Shortcuts Management

**Description**: Configure and manage application keyboard shortcuts (integrates with Shortcut System v2.0)

**Features**:
- View all registered shortcuts organized by category
- Customize user-specific shortcut mappings
- Conflict detection and resolution
- Reset to default shortcuts per category or globally
- Import/export shortcut profiles
- Quick setup wizard (VS Code style, Excel style, Custom)

**Categories**:
- Navigation (forms, panels, controls)
- Inventory Management (add, edit, remove, transfer)
- Transactions (view, search, export)
- Master Data (CRUD operations on entities)
- System (settings, help, logout)

**Validation**:
- Prevent conflicts with system shortcuts (F1 Help, Escape Close)
- Warn on conflicts across categories
- Block reassignment of non-customizable shortcuts

---

### FR6: About & System Information

**Description**: Display application metadata and system diagnostics

**Information Displayed**:
- Application Name, Version, Build Date
- Author and Copyright
- Database Connection Status (server, database, user)
- Active Theme Name
- Current User (username, role, shift)
- System Environment (OS, .NET version, memory usage)

**Diagnostic Tools**:
- View Application Logs (last 100 entries)
- Export Diagnostics Report (ZIP with logs, config, schema snapshot)
- Check for Updates (manual check only, no auto-update)
- View Changelog (release notes for current version)

**Links**:
- Documentation (opens Help system to index)
- Report Issue (opens Error Reporting dialog)
- License Information (displays full license text)

---

### FR7: Developer Tools

**Description**: Advanced settings for developers and administrators

**Parameter Prefix Maintenance**:
- View stored procedure parameter prefix overrides
- Add/edit/remove overrides for specific procedures
- Refresh parameter cache
- Validate prefix consistency across codebase

**Debug Settings**:
- Enable/disable Service_DebugTracer logging
- Set trace level (Off, Critical, Error, Warning, Info, Verbose)
- View trace output in real-time
- Export trace logs

**Schema Tools**:
- Export current database schema snapshot
- Compare schema versions
- View stored procedure definitions
- Execute custom SQL queries (read-only by default)

**Performance Monitoring**:
- View query performance thresholds
- Adjust timeout values per operation type
- Monitor connection pool statistics
- Clear cached data

**Security**: Developer role required, all actions logged with full audit trail

---

## Non-Functional Requirements

### NFR1: Performance

- Settings form opens in < 300ms (excluding database queries)
- Individual panel loads in < 500ms
- Save operations complete in < 1000ms
- Bulk import processes 1000 records in < 5 seconds
- Theme preview applies in < 200ms
- Database connection test completes in < 3 seconds

### NFR2: Usability

- TreeView navigation supports keyboard (arrow keys, type-ahead search)
- All controls accessible via Tab key in logical order
- Tooltips explain purpose of each setting
- Validation messages display inline near affected controls
- Confirmation dialogs for destructive operations (delete, reset)
- Undo capability for accidental changes (before save)

### NFR3: Reliability

- All save operations are atomic (all-or-nothing)
- Database errors trigger automatic rollback
- User prompted to retry failed operations
- Offline changes queued and synced when connection restored
- Settings backup created before major updates

### NFR4: Security

- Role-based access control enforced at service layer
- Sensitive settings (passwords, connection strings) encrypted
- Audit trail captures who/what/when for all changes
- Session timeout applies to Settings form
- Settings changes require re-authentication for critical operations

### NFR5: Maintainability

- All settings panels implement ISettingsPanel interface
- Service layer fully unit-testable (mocked DAOs)
- Clear separation of UI, business logic, and data access
- XML documentation on all public APIs
- Comprehensive logging via Service_DebugTracer

### NFR6: Compatibility

- Settings data migrates automatically from v1.0 schema
- Backward-compatible database schema (no breaking changes)
- Settings export format versioned for future-proofing
- Works with existing MySQL 5.7 infrastructure

---

## User Interface Requirements

### UI1: Settings Form Layout (Decision: Tree Navigation + Search Filter)

**Structure**:
- SplitContainer with resizable panels
- Left panel: TreeView navigation (150-200px min width, resizable)
- Right panel: Content area with grouped panels
- Top: Search box filters tree and highlights matching settings
- Bottom: Status bar with progress indicator

**TreeView Categories**:
- User Management (Add, Edit, Remove)
- Master Data (Part Numbers, Locations, Operations, Item Types - each with Add/Edit/Remove)
- Appearance (Theme, Shortcuts)
- System (Database, Developer Tools, About)

**Search Functionality**:
- Search box at top filters tree nodes as user types
- Highlights matching settings in right pane
- Scrolls to first match automatically
- Clear button (X) resets search

**Status Bar**:
- Progress bar for long operations
- Status text for feedback messages
- Current operation indicator

**Toolbar**:
- Save button (Ctrl+S, disabled if no changes)
- Cancel button (Escape)
- Reset Category button (Ctrl+R, resets current category)
- Reset All button (resets everything with confirmation)
- Help button (F1)

**Breadcrumb**:
- Shows current location in tree (e.g., "Master Data > Part Numbers > Edit")
- Clickable for quick navigation

### UI2: Common Panel Patterns (Decision: Inline Validation on Field Exit)

**All Panels Must Include**:
- Panel title (Label, bold, 12pt)
- Description text (Label, regular, 9pt)
- Input controls with clear labels
- Validation indicators (ErrorProvider icons)
- Action buttons (Save, Cancel, Clear)
- Progress controls for async operations

**Validation Display**:
- When user leaves field (LostFocus event), validate that field
- Show ErrorProvider icon + tooltip next to invalid field
- Error message explains what's wrong and how to fix it
- Disable "Apply" button if any validation errors exist
- Summary panel lists all validation errors with links to focus invalid fields

**Responsive Behavior**:
- Controls anchor/dock for resizable layouts
- Minimum panel size prevents control clipping
- Scrollbars appear when content exceeds panel height

### UI3: Master Data Panel Pattern

**Standard Layout for Add/Edit/Remove**:
- **Add Panel**: Input form → Add button → Clear button
- **Edit Panel**: ComboBox selector → Input form (populated) → Save button → Clear button
- **Remove Panel**: ComboBox selector → Details view (read-only) → Remove button → Cancel button

**Bulk Operations** (future enhancement):
- Import button opens file picker (CSV/Excel)
- Preview grid shows imported data with validation status
- Import button processes rows (progress bar)
- Results summary (successful, failed, skipped)

---

## Data Model Requirements

### DM1: Settings Models (Decision: Three-Tier Inheritance)

**UserSettingsModel** (User-Specific):
- Properties: Username, FirstName, LastName, Pin, Shift, UserType (flags), VisualUserName, VisualPassword, ThemeName, QuickButtons (collection), Roles (collection)
- Storage: Database (`md_users` table + `usr_quick_buttons` table)
- Validation: DataAnnotations attributes on properties
- Methods: Validate(), ToDto(), FromEntity()

**GlobalSettingsModel** (Application-Wide):
- Properties: ConnectionString, CommandTimeout, LoggingPath, SessionTimeout, MaxQuickButtons, ValidOperations
- Storage: `appsettings.json` (or `appsettings.debug.json` / `appsettings.release.json`)
- Read by: Admin/Developer roles only
- Modified by: Manual file edit or dedicated admin UI (future)

**Built-in Defaults** (Hardcoded Fallbacks):
- Default theme: "Default"
- Default timeout: 30 seconds
- Default session timeout: 60 minutes
- Max quick buttons: 10
- Applied when neither User nor Global settings exist

**Resolution Order**: User Setting → Global Setting → Built-in Default

**ThemeSettingsModel**:
- Properties: ThemeName, CustomColors (dictionary - Phase 2), LayoutPreferences (dictionary)
- Methods: ApplyTheme(), ResetToDefaults()
- Note: Color token count reduced to 50-80 essential properties

**DatabaseSettingsModel**:
- Properties: ServerAddress, Port, DatabaseName, ConnectionTimeout, PoolSize
- Methods: TestConnection(), GetConnectionString(), Validate()

**MasterDataSettingsModel** (base class):
- Properties: Id, Code, Description, IsActive, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
- Derived: PartModel, LocationModel, OperationModel, ItemTypeModel

### DM2: Settings Repository (Decision: Hybrid Storage with Retry + Fallback)

**Interface**: ISettingsRepository
- Methods: GetUserSettings(), SaveUserSettings(), GetThemeSettings(), SaveThemeSettings(), GetDatabaseSettings(), SaveDatabaseSettings(), GetMasterData<T>(), SaveMasterData<T>(), DeleteMasterData<T>()
- Caching: In-memory cache with 5-minute expiration, invalidate on save
- Transactions: All save operations wrapped in database transactions

**Storage Strategy**:
- **Primary**: Database stores all user settings
- **Fallback**: Local JSON files cache last-known-good settings
- **Sync**: On startup, load from database → write to local cache
- **Offline**: If database unavailable, load from local cache
- **Error Recovery**: 
  - Save error → retry once → alert user but continue running with current settings
  - Load error → try database → try file cache → use built-in defaults
  - Corrupted file → log error, delete corrupt file, use defaults

### DM3: Database Schema Changes (Decision: Session Management + No Audit Log in MVP)

**New Tables**:
- **active_sessions**: Session management and remote logout
  - Columns: `SessionGUID` (PK), `UserID`, `PCName`, `LoginTime`, `LastHeartbeat`, `ForceLogoutFlag`
  - Purpose: Track active sessions, enable remote logout, detect crashed instances
  - Cleanup: Stale sessions (no heartbeat >2 minutes) auto-expire
- **usr_quick_buttons**: Quick button storage with LRU tracking
  - Columns: `ButtonID` (PK), `UserID`, `PartNumber`, `Location`, `Operation`, `LastUsedTimestamp`, `DisplayOrder`
  - Purpose: Store up to 10 quick buttons per user with usage tracking

**Modified Tables**:
- **usr_ui_settings**: Add JsonSchema column for structured settings, deprecate individual columns over time
- **md_part_ids**, **md_locations**, **md_operations**, **md_item_types**: Add IsActive, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate columns

**Stored Procedures**:
- **usr_settings_Get**: Single procedure to retrieve all user settings as JSON
- **usr_settings_Save**: Single procedure to save user settings with validation
- **session_Heartbeat_Update**: Update LastHeartbeat timestamp for active session
- **session_ForceLogout_Check**: Check if ForceLogoutFlag is set for current session
- **quickbuttons_Replace_LRU**: Replace least recently used button when at capacity
- Refactor existing CRUD procedures to include audit calls (future enhancement)

**Note**: Audit logging (usr_settings_audit table) deferred to Phase 2 based on compliance requirements

---

## Integration Requirements

### INT1: Theme System Integration

- Settings system uses Core_Themes for theme application
- Theme changes trigger ThemeChanged event
- All open forms subscribe to ThemeChanged event
- Theme preview mode temporarily overrides Model_AppVariables.ThemeName

### INT2: Shortcut System Integration

- Settings panel embeds Control_Shortcuts for shortcut management
- Shortcut changes saved via Service_ShortcutManager
- Settings form registers its own shortcuts (Ctrl+S Save, Escape Close)

### INT3: Error Handling Integration

- All exceptions caught and passed to Service_ErrorHandler
- Validation errors displayed inline + Service_ErrorHandler for critical failures
- User-friendly error messages with retry/cancel options

### INT4: Progress Reporting Integration

- Long operations use Helper_StoredProcedureProgress
- Progress bar in status strip shows completion percentage
- Cancel button available for cancelable operations

### INT5: Audit and Logging Integration (Decision: No Audit Log in MVP)

- All settings changes logged via Service_DebugTracer for diagnostics
- Critical changes (user add/remove, database connection) logged with context information
- LoggingUtility captures exceptions and diagnostic info
- **Note**: usr_settings_audit table and comprehensive audit trail deferred to Phase 2
- Can be enabled later if compliance requirements emerge

---

## Migration Strategy (Decision: No Data Migration - Reset to Defaults)

### Phase 1: Schema Updates

- Create new database tables (`active_sessions`, `usr_quick_buttons`)
- Add columns to existing tables (IsActive, timestamps, audit fields)
- Create new stored procedures for session management and settings access
- **No data migration code**: On first run of v2.0, show message "Settings format updated. Resetting to defaults."
- Users reconfigure their preferences (theme, quick buttons, layout)

### Phase 2: Code Implementation

- Implement Service_SettingsManager and models
- Refactor one entity at a time (User → PartID → Location → Operation → ItemType)
- Test each refactored entity in isolation
- Deploy incrementally behind feature flag

### Phase 3: UI Migration

- Replace SettingsForm.cs with new architecture
- Implement ISettingsPanel for each settings category
- Wire up navigation and state management
- Deprecate old UserControls (mark Obsolete, leave for 1 release cycle)

### Phase 4: Cleanup

- Remove obsolete UserControls and legacy DAOs
- Archive old stored procedures
- Update documentation and help system
- Announce deprecation of v1.0 APIs

---

## Success Metrics

- **Code Quality**: 0 compiler warnings, 95%+ XML documentation coverage
- **Performance**: All operations meet NFR1 thresholds
- **User Satisfaction**: Settings form opens within 300ms, no user complaints about usability
- **Reliability**: Zero data loss incidents, all save operations atomic
- **Maintainability**: New settings category can be added in < 4 hours by following patterns

---

## Out of Scope

- Multi-user concurrent editing (settings are per-user, single instance enforced per Q4.2)
- Real-time settings sync across sessions (changes apply on next login)
- Role-based settings templates (future enhancement)
- Settings versioning/rollback UI (no audit trail in MVP per Q5.2)
- Cloud-based settings backup (local database + file cache only)
- Custom theme editor (read-only selection in MVP per Q2.2)
- Quick button validation (MVP catches errors at execution time per Q3.2)
- Quick button import/export (database provides sync per Q3.3)
- User font size overrides (system DPI only per Q2.3)

---

## Resolved Questions

All clarification questions have been answered and integrated into this specification. See `2-Clarification-Questions.md` for detailed decision rationale organized by category:

1. **Settings Persistence Architecture** (Q1.1-Q1.4): Storage, migration, scope, real-time application
2. **Theme System Integration** (Q2.1-Q2.3): Color tokens, customization, DPI scaling
3. **Quick Buttons System** (Q3.1-Q3.3): Capacity, validation, backup/sharing
4. **Session Management** (Q4.1-Q4.3): Timeout, multi-instance, auto-save
5. **Error Handling and Validation** (Q5.1-Q5.3): Validation strategy, auditing, error recovery
6. **UI/UX Design** (Q6.1-Q6.3): Form organization, validation feedback, reset granularity

---

## Dependencies

- **Database**: MySQL 5.7+ with existing schema
- **Theme System**: Core_Themes functional
- **Shortcut System**: Service_ShortcutManager implemented (v2.0)
- **Error Handling**: Service_ErrorHandler available
- **Progress Reporting**: Helper_StoredProcedureProgress functional

---

## References

- Research Data: `settings-research.json`, `settings-entities.json`
- Current Implementation: `Controls/SettingsForm/*`, `Forms/Settings/*`
- Database Schema: `Database/database-schema-snapshot.json`
- Instruction Files: `.github/instructions/*.instructions.md`
- Related Features: Keyboard Shortcuts System v2.0

---

## Revision History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0.0 | 2025-11-02 | System | Initial draft based on comprehensive codebase analysis |
| 2.0.0 | 2025-11-02 | System | Integrated all architectural decisions from 2-Clarification-Questions.md |

---

## Implementation Priorities

Based on finalized decisions, implementation phases are:

### Phase 1 (MVP - Core Functionality)
1. Storage architecture (hybrid DB + file with retry/fallback)
2. Settings form UI (tree navigation + search filter + validation)
3. Session management (single instance enforcement, remote logout, admin controls)
4. Real-time application (immediate for UI, restart for critical)
5. Validation and error recovery

### Phase 2 (Enhancement)
6. Theme token audit and consolidation (203 → 50-80 colors)
7. Quick button LRU replacement logic
8. Session timeout warning system

### Phase 3 (Nice to Have)
9. Quick button validation (if users request)
10. Custom theme editor (if users request)
11. Audit logging (if compliance requires)

See `3-Plan.md` for detailed implementation timeline and `8-Tasks.md` for actionable task breakdown.

