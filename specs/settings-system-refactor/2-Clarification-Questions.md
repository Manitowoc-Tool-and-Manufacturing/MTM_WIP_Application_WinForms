# Settings System - Clarification Questions

**Created**: 2025-11-02  
**Updated**: 2025-11-02  
**Purpose**: Document architectural decisions for Settings System implementation  
**Status**: ✅ Decisions Finalized

---

## Category 1: Settings Persistence Architecture

### Q1.1: Database vs File-Based Storage Strategy

**Question**: Which storage approach should be used for different setting types?

**Answer**: ✅ **Hybrid with Database Primary, File Fallback**
- **Primary**: Database stores all user settings (theme, quick buttons, layout)
- **Fallback**: Local JSON files cache last-known-good settings
- **Sync**: On startup, load from database → write to local cache
- **Offline**: If database unavailable, load from local cache
- **Environments**: Separate files (`appsettings.debug.json` / `appsettings.release.json`)

**Reasoning**: Manufacturing environments often have intermittent network issues. Database provides centralized management for multi-user environment, while local files provide offline resilience. This hybrid approach offers best balance.

**Impact**: Medium - Affects DAO layer, error handling, and startup sequence

---

### Q1.2: Settings Migration Strategy

**Question**: How should settings schema changes be handled during version upgrades?

**Answer**: ✅ **No Migration - Reset to Defaults**
- On version mismatch, show message: "Settings format updated. Resetting to defaults."
- User settings cleared, defaults applied
- No migration code or version tracking needed

**Reasoning**: Simpler codebase without migration logic complexity. Users accept reconfiguration after updates in exchange for reliability. Clear communication prevents confusion.

**Impact**: Low - Simplifies implementation, users reconfigure preferences after updates

---

### Q1.3: Settings Scope and Inheritance

**Question**: How should settings be organized and prioritized?

**Answer**: ✅ **Three-Tier: User → Global → Built-in Default**
- **User settings**: Theme, quick buttons, layout (in database per user)
- **Global settings**: Connection strings, timeouts, logging (in `appsettings.json`)
- **Built-in defaults**: Hardcoded fallbacks in code when neither exist
- **Resolution order**: User setting → Global setting → Built-in default
- **Restore**: User can reset their settings; admins can reset global via file edit

**Reasoning**: Provides user personalization while allowing admin control of critical settings. No role complexity needed for manufacturing use case. Balanced control without complex permission systems.

**Impact**: High - Affects database schema, settings loading sequence, UI permissions

---

### Q1.4: Real-Time Settings Application

**Question**: Should settings changes apply immediately or require restart?

**Answer**: ✅ **Immediate for UI, Restart for Critical**
- **Immediate**: Theme changes, layout preferences → refresh all forms
- **Restart required**: Database connection, logging paths → show restart message
- **Apply/Cancel**: Pending changes in memory, save on "Apply", discard on "Cancel"

**Reasoning**: Balance between responsiveness and safety. Theme changes are low-risk (immediate feedback), connection changes are high-risk (restart prevents mid-operation failures). Apply/Cancel workflow is familiar to users.

**Impact**: Medium - Requires observer pattern for forms, validation logic, restart prompts

---

## Category 2: Theme System Integration

### Q2.1: Theme Color Token Coverage

**Question**: What should be done about the 203 color properties in `Model_UserUiColors`?

**Answer**: ✅ **Audit and Consolidate**
- Run code search to identify which color properties are actually used
- Remove unused tokens, keep 50-80 essential colors
- Document which controls use which tokens

**Reasoning**: 203 properties likely includes unused tokens from over-engineering. Audit reduces database bloat, JSON file size, and memory footprint while improving performance. Use MCP tool `grep_search` to find actual usage.

**Impact**: Medium - Affects theme database schema, JSON structure, and performance

---

### Q2.2: Theme Customization Workflow

**Question**: Should users be able to create or edit custom themes?

**Answer**: ✅ **Read-Only Theme Selection (MVP)**
- Users select from 9 predefined themes only
- No theme editor UI needed
- Administrator can add themes via database insert

**Reasoning**: Simplest implementation that covers 90% of use cases. Manufacturing users typically prefer presets over extensive customization. Can consider customization in Phase 2 based on user feedback.

**Impact**: High - Determines UI scope, database schema for custom themes

---

### Q2.3: DPI Scaling User Overrides

**Question**: Can users override system DPI scaling with custom font sizes?

**Answer**: ✅ **System DPI Only, No User Override**
- Users adjust Windows display settings (system DPI)
- Application respects system DPI automatically via `Core_Themes.ApplyDpiScaling()`
- No custom font multiplier in application

**Reasoning**: Follows Windows accessibility standards. Users needing larger fonts adjust system DPI settings. Avoids complex validation logic for font size changes that could break TableLayoutPanel constraints.

**Impact**: Low - No changes needed, application already follows system DPI

---

## Category 3: Quick Buttons System

### Q3.1: Quick Button Capacity Behavior

**Question**: What happens when user tries to add 11th quick button (max is 10)?

**Answer**: ✅ **Replace Least Recently Used (LRU) with Warning**
- Track last-used timestamp on each button
- When adding 11th, show dialog: "Remove '[ButtonName]' (last used 3 days ago)? [Yes] [No]"
- User can cancel or confirm replacement

**Reasoning**: Balance between automation and user control. Tracks usage naturally via timestamps. Users retain control but don't have to manually manage deletions during busy work.

**Impact**: Low-Medium - Requires database schema update (add `LastUsedTimestamp` column), UI confirmation dialog

---

### Q3.2: Quick Button Validation and Intelligence

**Question**: Should quick buttons validate that referenced parts exist before execution?

**Answer**: ✅ **Simple Shortcuts, No Validation (MVP)**
- Buttons execute immediately without pre-validation
- If part doesn't exist, DAO returns error → show error dialog
- No visual indicators of validity

**Reasoning**: Simplest implementation. Errors caught naturally at execution time by existing DAO error handling. No performance overhead from validation queries. Can consider validation in Phase 2 if users report frequent invalid clicks.

**Impact**: Low - No changes needed, use existing error handling

---

### Q3.3: Quick Button Backup and Sharing

**Question**: Should quick buttons have import/export functionality?

**Answer**: ✅ **No Import/Export (Database Provides Sync)**
- Quick buttons stored in database, tied to user account
- User logs in on any workstation → buttons available automatically
- No export/import needed if database is source of truth

**Reasoning**: Database already provides "cloud sync" functionality. User logs in anywhere and gets their buttons. No additional backup/restore mechanism needed unless supporting offline-only mode.

**Impact**: Low - No changes needed

---

## Category 4: Session Management

### Q4.1: Session Timeout Behavior

**Question**: What happens when session timeout (60 minutes) is reached?

**Answer**: ✅ **Warning + Extension Prompt (Close on Timeout)**
- At 55 minutes: Show dialog "Session expires in 5 minutes. [Extend Session] [Close Now]"
- User clicks "Extend Session" → reset timer to 0
- If no response by 60 minutes → Close application gracefully
- Unsaved work prompts save dialog before closing

**Reasoning**: Manufacturing environments need balance between security and usability. 5-minute warning gives users control. Closing application (rather than just locking) ensures clean state and prevents abandoned sessions from consuming resources.

**Impact**: Medium - Requires timer service, warning dialog, graceful shutdown logic

---

### Q4.2: Multi-Session Concurrency

**Question**: Can same user open multiple application instances simultaneously?

**Answer**: ✅ **Block Multiple Instances with Remote Logout**
- Check `active_sessions` table for existing session on login
- If found, show dialog: "Already logged in on [PC Name]. [Force Logout] [Cancel]"
- **User Feature**: [Force Logout] button sends logout signal to remote instance
- **Admin Feature**: Admin dashboard shows all active sessions with "Force Close" buttons
  - Force close individual user session
  - Force close all sessions
- Remote logout via database flag that running instances poll every 5-10 seconds
- Session heartbeat mechanism to detect crashed instances

**Reasoning**: Single instance enforcement prevents settings conflicts and ensures resource cleanup. Remote logout provides user convenience (no need to walk to other PC). Admin force-close is critical for maintenance windows and troubleshooting.

**Implementation Details**:
- Add `active_sessions` table: `UserID`, `PCName`, `SessionGUID`, `LastHeartbeat`, `ForceLogoutFlag`
- Running instances update heartbeat every 30 seconds
- Poll for `ForceLogoutFlag` every 5 seconds
- Stale sessions (no heartbeat >2 minutes) auto-expire

**Impact**: Medium-High - Requires active sessions table, heartbeat mechanism, remote logout signaling, and admin UI

---

### Q4.3: Auto-Save Trigger Strategy

**Question**: What triggers settings to save (timer, events, or manual)?

**Answer**: ✅ **Manual Save Only**
- User clicks "Save" button to persist changes
- No auto-save timer
- Visual "Unsaved changes ⚠️" indicator when modifications pending
- Confirmation prompt if closing dialog with unsaved changes

**Reasoning**: Gives users complete control over when settings persist. No surprise auto-saves or database writes. "Unsaved changes" warning prevents accidental loss. Standard pattern for settings dialogs.

**Impact**: Low - Requires dirty flag tracking and unsaved changes indicator

---

## Category 5: Error Handling and Validation

### Q5.1: Settings Validation Strategy

**Question**: When and how should settings be validated?

**Answer**: ✅ **Validate on Apply, Rollback on Error**
- Settings accumulate in memory while editing
- Click "Apply" → validate all settings together
- If validation fails: show error dialog, discard changes, keep old values
- If success: save to database/file, apply to UI

**Reasoning**: Standard settings dialog pattern. Simple and safe. Prevents invalid settings from persisting. Users familiar with Apply/Cancel workflow from Windows conventions.

**Impact**: Medium - Requires validation logic and rollback mechanism

---

### Q5.2: Settings Change Auditing

**Question**: Should settings changes be logged for audit trail?

**Answer**: ✅ **No Audit Logging (MVP)**
- Settings changes not logged
- Database only stores current values
- Simpler schema, no storage bloat

**Reasoning**: Manufacturing WIP tracking app doesn't require settings audit trail for compliance. Simpler implementation. Can add audit logging later if compliance requirements emerge.

**Impact**: Low - No changes needed

---

### Q5.3: Settings Error Recovery

**Question**: How should errors during settings save/load be handled?

**Answer**: ✅ **Retry + Fallback to Defaults**
- **Save error**: Retry once, if still fails → alert user but continue running with current settings
- **Load error**: Try database → try file cache → use built-in defaults
- **Corrupted file**: Log error, delete corrupt file, use defaults
- **User notification**: Dialog explaining fallback: "Could not load settings. Using defaults."

**Reasoning**: Graceful degradation prevents application crashes from settings issues. Retry handles transient database errors. Fallback chain ensures application always starts. User notification explains why settings reset.

**Impact**: Medium - Requires retry logic, fallback chain, and error notification UI

---

## Category 6: UI/UX Design

### Q6.1: Settings Form Organization

**Question**: How should settings be organized and navigated in the UI?

**Answer**: ✅ **Hybrid: Tree Navigation + Search Filter**
- **Left pane**: Tree view with expandable categories
  - General
  - Theme & Appearance
  - Quick Buttons
  - Database & Connection
  - Session & Security
  - Advanced
- **Right pane**: Selected category's settings displayed as grouped panels
- **Search box**: At top, filters tree and highlights matching settings as user types
- **Breadcrumb**: Shows current location in tree

**Reasoning**: Combines best of both approaches. Tree navigation scales well and provides structure. Search filter helps users find specific settings quickly without hunting through categories. Modern UX that works for both novice and power users.

**Implementation Notes**:
- Use `TreeView` control in left pane (150-200px width)
- Right pane uses `FlowLayoutPanel` with `GroupBox` containers
- Search filters tree nodes and scrolls to first match
- Expandable tree state persists across sessions

**Impact**: Medium-High - Requires TreeView + search infrastructure, more complex layout than simple tabs

---

### Q6.2: Settings Validation Feedback UI

**Question**: How should validation errors be displayed to users?

**Answer**: ✅ **Inline Validation on Field Exit**
- When user leaves field (`LostFocus`), validate that field
- Show `ErrorProvider` icon + tooltip next to invalid field
- Disable "Apply" button if any errors exist
- Error message explains what's wrong and how to fix it

**Reasoning**: Standard WinForms pattern using built-in `ErrorProvider`. Provides immediate feedback without performance cost of real-time validation. Users see errors before clicking Apply.

**Impact**: Low-Medium - ErrorProvider is built-in, just needs validation rules

---

### Q6.3: Settings Reset Granularity

**Question**: What granularity should "restore defaults" functionality provide?

**Answer**: ✅ **Reset Per Category + Reset All**
- "Reset Category" button in each tree node → resets that category's settings only
- "Reset All Settings" button in toolbar → resets everything
- Confirmation dialog: "Are you sure you want to reset [category/all] to defaults? This cannot be undone."
- Preserved settings: Username, PC name (never reset)

**Reasoning**: Provides flexibility without UI clutter. Users can reset theme without losing database settings. Reset All is useful when everything is misconfigured. Category-based reset aligns with tree navigation structure.

**Impact**: Low-Medium - Requires reset methods per category, confirmation dialogs

---

## Summary: Final Decisions

| Category | Decision | Impact |
|----------|----------|--------|
| **Storage** | Hybrid (DB primary, file fallback) | Medium |
| **Migration** | No migration, reset to defaults | Low |
| **Scope** | Three-tier (User → Global → Default) | High |
| **Real-Time** | Immediate UI, restart for critical | Medium |
| **Theme Tokens** | Audit and consolidate to 50-80 colors | Medium |
| **Theme Edit** | Read-only selection (9 presets) | High |
| **DPI** | System DPI only, no overrides | Low |
| **Quick Buttons Cap** | Replace LRU with warning | Low-Medium |
| **Quick Buttons Validate** | No validation (MVP) | Low |
| **Quick Buttons Export** | No (database provides sync) | Low |
| **Session Timeout** | Warning + close on timeout | Medium |
| **Multi-Instance** | Block with remote logout + admin force-close | Medium-High |
| **Auto-Save** | Manual save only | Low |
| **Validation** | Validate on apply, rollback on error | Medium |
| **Audit Log** | No audit logging (MVP) | Low |
| **Error Recovery** | Retry + fallback to defaults | Medium |
| **UI Organization** | Tree navigation + search filter | Medium-High |
| **Validation UI** | Inline on field exit (ErrorProvider) | Low-Medium |
| **Reset** | Per category + reset all | Low-Medium |

---

## Implementation Priority

### Phase 1 (MVP - Core Functionality)
1. **Q1.1, Q1.3**: Storage architecture (hybrid DB + file)
2. **Q1.4**: Real-time application (immediate vs restart)
3. **Q6.1, Q6.2, Q6.3**: Settings form UI (tree + search + validation)
4. **Q4.2**: Session management (block multiple, remote logout)
5. **Q5.1, Q5.3**: Validation and error recovery

### Phase 2 (Enhancement)
6. **Q2.1**: Theme token audit and consolidation
7. **Q3.1**: Quick button LRU replacement logic
8. **Q4.1**: Session timeout warning system

### Phase 3 (Nice to Have)
9. **Q3.2**: Quick button validation (if users request)
10. **Q2.2**: Theme customization (if users request)
11. **Q5.2**: Audit logging (if compliance requires)

---

## Next Steps

1. ✅ **Decisions Finalized**: All architectural questions answered
2. ☐ **Update Specification**: Integrate decisions into `1-Specification.md`
3. ☐ **Generate Implementation Plan**: Create `3-Plan.md` with timeline
4. ☐ **Create Task Breakdown**: Generate `8-Tasks.md` with actionable items
5. ☐ **Database Schema**: Design `active_sessions` table and settings storage
6. ☐ **UI Mockups**: Wireframe settings form with tree navigation

**Status**: ✅ Ready to proceed with implementation planning
