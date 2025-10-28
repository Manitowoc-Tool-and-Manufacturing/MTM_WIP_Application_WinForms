# View Application Logs - Manual Test Scenarios

**Feature**: 003-view-application-logs  
**Date**: 2025-10-28  
**Version**: 1.0  
**Status**: Ready for QA Execution

---

## Overview

This document provides step-by-step manual test scenarios for the View Application Logs feature. Each scenario corresponds to a user story and includes expected results and pass/fail criteria.

**Total Scenarios**: 7 user stories + 3 edge cases = 10 scenarios  
**Estimated Test Time**: 45-60 minutes

---

## Test Environment Setup

### Prerequisites
- MTM WIP Application installed and configured
- Multiple user accounts with log files (bjones, jsullivan, admin)
- Log files of different types (Normal, Application Error, Database Error)
- At least 1000 entries in one log file for performance testing

### Test Data Requirements
- Users with log directories: bjones, jsullivan, admin
- Log files with various entry counts: <100, 100-500, 500-1000, >1000
- Log files with parse failures (malformed entries)
- Multiple log types per user

---

## US1: User Selection and File Browsing

### Scenario 1.1: Load User List
**Priority**: P0 (Critical)  
**User Story**: US1  
**Acceptance Criteria**: AS 1.1

**Steps**:
1. Launch MTM WIP Application
2. Navigate to View Application Logs form
3. Observe user dropdown (cmbUsers)

**Expected Results**:
- ✅ Form opens without errors
- ✅ User dropdown populated with all users who have log directories
- ✅ User count label displays correct count (e.g., "3 users")
- ✅ Load completes in <500ms (SC-001)
- ✅ Users sorted alphabetically

**Pass/Fail Criteria**:
- [ ] **PASS** - All users displayed, count accurate, performance acceptable
- [ ] **FAIL** - Missing users, incorrect count, or slow load (>500ms)

---

### Scenario 1.2: Select User and Load Files
**Priority**: P0 (Critical)  
**User Story**: US1  
**Acceptance Criteria**: AS 1.2, AS 1.3

**Steps**:
1. Select "bjones" from user dropdown
2. Observe log file list (lstLogFiles)
3. Note file grouping by type

**Expected Results**:
- ✅ Log files loaded and displayed for selected user
- ✅ Files grouped by type with headers (Normal Logs, Application Errors, Database Errors)
- ✅ Each file shows: filename, modified date, size, entry count
- ✅ Files sorted newest first within each group
- ✅ File count label updated (e.g., "5 files")
- ✅ Load completes in <1s (SC-002)
- ✅ Auto-refresh timer starts (5s interval)

**Pass/Fail Criteria**:
- [ ] **PASS** - Files displayed, grouped correctly, performance acceptable
- [ ] **FAIL** - Missing files, wrong grouping, or slow load (>1s)

---

## US2: Entry Navigation

### Scenario 2.1: Load and Navigate Entries
**Priority**: P0 (Critical)  
**User Story**: US2  
**Acceptance Criteria**: AS 2.1, AS 2.2, AS 2.3

**Steps**:
1. From file list, double-click a Normal log file with 100+ entries
2. Observe entry display panel
3. Click "Next ►" button multiple times
4. Click "◄ Previous" button multiple times
5. Use keyboard: Arrow Down, Arrow Up, Page Down, Page Up

**Expected Results**:
- ✅ First entry displays immediately after file load
- ✅ Entry formatted with timestamp, level, source, message
- ✅ Entry position label shows "Entry 1 of X"
- ✅ Navigation buttons work (Next/Previous)
- ✅ Keyboard shortcuts work (arrows, page up/down)
- ✅ Load and parse completes in <2s for 1000 entries (SC-003)
- ✅ Navigation responds in <100ms (SC-004)

**Pass/Fail Criteria**:
- [ ] **PASS** - All navigation methods work, performance acceptable
- [ ] **FAIL** - Navigation broken, slow response (>100ms), or parse errors

---

## US3: Entry Display Formatting

### Scenario 3.1: View Different Log Types
**Priority**: P1 (High)  
**User Story**: US3  
**Acceptance Criteria**: AS 3.1, AS 3.2, AS 3.3

**Steps**:
1. Load a Normal log entry - verify formatting
2. Load an Application Error log entry - verify stack trace display
3. Load a Database Error log entry - verify severity and SQL context
4. Navigate to an entry with JSON details - verify indentation
5. Navigate to an entry with emoji - verify emoji display

**Expected Results**:
- ✅ Normal log: Timestamp, Level, Source, Message, Details (if present)
- ✅ Application Error: Timestamp, Error Type, Exception, Stack Trace
- ✅ Database Error: Timestamp, Severity, Message, Stack Trace
- ✅ JSON details formatted with 2-space indentation
- ✅ Emoji displayed correctly (🔧, ⚠️, ✅, ❌, 🚀)
- ✅ Color coding applied based on severity

**Pass/Fail Criteria**:
- [ ] **PASS** - All log types display correctly with proper formatting
- [ ] **FAIL** - Formatting issues, missing fields, or emoji not displayed

---

### Scenario 3.2: Severity Color Coding
**Priority**: P1 (High)  
**User Story**: US3  
**Acceptance Criteria**: AS 3.4

**Steps**:
1. Navigate to entries with different severity levels
2. Observe background colors

**Expected Results**:
- ✅ ERROR/CRITICAL: Light red background
- ✅ WARNING: Light yellow background  
- ✅ INFO/SUCCESS: Light green background
- ✅ DEBUG: Light gray background
- ✅ DEFAULT: White background

**Pass/Fail Criteria**:
- [ ] **PASS** - All severity levels have correct colors
- [ ] **FAIL** - Wrong colors or no color coding applied

---

## US4: Filtering

### Scenario 4.1: Apply Date Range Filter
**Priority**: P1 (High)  
**User Story**: US4  
**Acceptance Criteria**: AS 4.1

**Steps**:
1. Load a file with entries spanning multiple days
2. Set Start Date to 3 days ago
3. Set End Date to 1 day ago
4. Click "Apply Filter" button
5. Navigate through filtered entries

**Expected Results**:
- ✅ Only entries within date range displayed
- ✅ Entry count updated to reflect filtered results
- ✅ Navigation works within filtered set
- ✅ Filter applies in <300ms for 5000→100 entries (SC-006)

**Pass/Fail Criteria**:
- [ ] **PASS** - Filter works correctly, performance acceptable
- [ ] **FAIL** - Wrong entries shown or slow filter (>300ms)

---

### Scenario 4.2: Apply Quick Filters
**Priority**: P2 (Medium)  
**User Story**: US4  
**Acceptance Criteria**: AS 4.2

**Steps**:
1. Click "Errors Only" quick filter button
2. Observe filtered results
3. Click "Performance" quick filter button
4. Observe filtered results
5. Click "Today" quick filter button
6. Observe filtered results
7. Click "Clear Filters" button

**Expected Results**:
- ✅ "Errors Only": Shows only ERROR/CRITICAL level entries
- ✅ "Performance": Shows entries with "Performance" in message/source
- ✅ "Today": Shows only entries from current date
- ✅ "Clear Filters": Restores all entries
- ✅ Each filter applies in <300ms

**Pass/Fail Criteria**:
- [ ] **PASS** - All quick filters work correctly
- [ ] **FAIL** - Filters don't apply or wrong entries shown

---

## US5: Raw View Toggle

### Scenario 5.1: Toggle Between Views
**Priority**: P2 (Medium)  
**User Story**: US5  
**Acceptance Criteria**: AS 5.1, AS 5.2

**Steps**:
1. View a parsed entry
2. Click "Show Raw View" button
3. Observe raw text display
4. Click "Show Parsed View" button
5. Observe return to parsed display

**Expected Results**:
- ✅ "Show Raw View": Displays exact file text in monospace font
- ✅ "Show Parsed View": Returns to formatted display
- ✅ Button text changes appropriately
- ✅ Toggle responds instantly (<100ms)

**Pass/Fail Criteria**:
- [ ] **PASS** - Toggle works, text matches file content exactly
- [ ] **FAIL** - Toggle broken, text modified, or performance issues

---

### Scenario 5.2: Parse Failure Fallback
**Priority**: P2 (Medium)  
**User Story**: US5  
**Acceptance Criteria**: AS 5.3

**Steps**:
1. Load a log file with malformed entries (test data)
2. Navigate to a malformed entry
3. Observe automatic fallback

**Expected Results**:
- ✅ Form automatically switches to raw view
- ✅ "Parse failed" notification displayed
- ✅ Raw text shown with indication of parse failure
- ✅ User can still navigate to other entries

**Pass/Fail Criteria**:
- [ ] **PASS** - Parse failures handled gracefully with raw view fallback
- [ ] **FAIL** - Application crash, no notification, or blank display

---

## US6: Export and Copy

### Scenario 6.1: Export Filtered Entries
**Priority**: P2 (Medium)  
**User Story**: US6  
**Acceptance Criteria**: AS 6.1

**Steps**:
1. Apply a filter to get ~30 entries
2. Click "Export Visible" button (when added to UI)
3. Choose save location in dialog
4. Save file
5. Open exported file in text editor

**Expected Results**:
- ✅ SaveFileDialog appears with default filename (LogExport_YYYYMMDD_HHmmss.txt)
- ✅ File contains all 30 filtered entries
- ✅ Entries formatted with clear separation (--- dividers)
- ✅ Export completes in <1s for 500 entries (SC-006)
- ✅ File readable with proper line breaks

**Pass/Fail Criteria**:
- [ ] **PASS** - File exported correctly, all entries present, performance acceptable
- [ ] **FAIL** - Missing entries, corrupt file, or slow export (>1s for 500)

---

### Scenario 6.2: Copy Current Entry (Parsed View)
**Priority**: P2 (Medium)  
**User Story**: US6  
**Acceptance Criteria**: AS 6.2, AS 6.3

**Steps**:
1. Navigate to a parsed entry
2. Press Ctrl+C
3. Open Notepad
4. Paste (Ctrl+V)

**Expected Results**:
- ✅ Status shows "Entry copied to clipboard"
- ✅ Pasted text matches formatted display exactly
- ✅ All fields present (timestamp, level, source, message, details)

**Pass/Fail Criteria**:
- [ ] **PASS** - Copy works, paste shows complete formatted entry
- [ ] **FAIL** - Copy fails, partial data, or formatting lost

---

### Scenario 6.3: Copy Current Entry (Raw View)
**Priority**: P2 (Medium)  
**User Story**: US6  
**Acceptance Criteria**: AS 6.4

**Steps**:
1. Toggle to raw view
2. Press Ctrl+C
3. Paste in Notepad

**Expected Results**:
- ✅ Raw text copied exactly as shown
- ✅ Matches file content precisely

**Pass/Fail Criteria**:
- [ ] **PASS** - Raw text copied correctly
- [ ] **FAIL** - Text modified or incomplete

---

### Scenario 6.4: Open Log Directory
**Priority**: P3 (Low)  
**User Story**: US6  
**Acceptance Criteria**: FR-036

**Steps**:
1. Select a user (e.g., bjones)
2. Click "Open Directory" button (when added to UI)
3. Observe Windows Explorer

**Expected Results**:
- ✅ Windows Explorer opens
- ✅ Correct user log directory displayed
- ✅ Status shows "Opened log directory for bjones"

**Pass/Fail Criteria**:
- [ ] **PASS** - Explorer opens to correct directory
- [ ] **FAIL** - Wrong directory, error, or no response

---

## US7: Error Dialog Integration

### Scenario 7.1: Access from Error Dialog
**Priority**: P3 (Low)  
**User Story**: US7  
**Acceptance Criteria**: AS 7.1, AS 7.2

**Steps**:
1. Log in as bjones
2. Trigger an application error (e.g., invalid database operation)
3. In error dialog, click "View Logs" button (when added)
4. Observe log viewer form

**Expected Results**:
- ✅ Log viewer form opens
- ✅ bjones pre-selected in user dropdown
- ✅ Most recent log file loaded automatically
- ✅ Current log entries displayed

**Pass/Fail Criteria**:
- [ ] **PASS** - Form opens with correct user pre-selected, recent log loaded
- [ ] **FAIL** - Wrong user, no pre-selection, or form fails to open

---

## Edge Cases and Error Handling

### Scenario E1: Large File Performance
**Priority**: P1 (High)  
**Acceptance Criteria**: SC-003

**Steps**:
1. Select a log file with >1000 entries
2. Measure load time
3. Navigate through entries
4. Apply filters

**Expected Results**:
- ✅ Load and parse completes in <2s (SC-003)
- ✅ Navigation remains responsive (<100ms per action)
- ✅ Filter applies in <300ms

**Pass/Fail Criteria**:
- [ ] **PASS** - All performance targets met
- [ ] **FAIL** - Any operation exceeds performance target

---

### Scenario E2: No Log Files
**Priority**: P2 (Medium)  
**Acceptance Criteria**: FR-010

**Steps**:
1. Create a new test user with no log files
2. Select that user in dropdown

**Expected Results**:
- ✅ Friendly message: "No log files found for this user"
- ✅ File list remains empty
- ✅ No errors or crashes

**Pass/Fail Criteria**:
- [ ] **PASS** - Handled gracefully with clear message
- [ ] **FAIL** - Crash, error dialog, or unclear message

---

### Scenario E3: Access Denied
**Priority**: P2 (Medium)  
**Acceptance Criteria**: FR-014

**Steps**:
1. Attempt to view logs for a directory with restricted permissions (test setup)
2. Observe error handling

**Expected Results**:
- ✅ Error dialog shown via Service_ErrorHandler
- ✅ Clear message: "Access denied to log directory"
- ✅ User can retry or close
- ✅ No application crash

**Pass/Fail Criteria**:
- [ ] **PASS** - Error handled gracefully, retry option available
- [ ] **FAIL** - Application crash or unclear error message

---

## Performance Benchmarks

### Required Performance Targets

| Operation | Target | Success Criteria |
|-----------|--------|------------------|
| Load user list | <500ms | SC-001 |
| Enumerate files | <1s | SC-002 |
| Load/parse 1000 entries | <2s | SC-003 |
| Entry navigation | <100ms | SC-004 |
| Filter application (5000→100) | <300ms | SC-006 |
| Export 500 entries | <1s | SC-006 |
| Auto-refresh interval | 5s | SC-007 |

**Test Method**:
- Use Stopwatch logging already implemented (T048)
- Check application logs for [Performance] warnings
- Record actual times during manual testing

---

## Auto-Refresh Functionality

### Scenario AR1: Auto-Refresh Operation
**Priority**: P2 (Medium)  
**Acceptance Criteria**: FR-033, SC-007

**Steps**:
1. Select a user (bjones)
2. Wait 5 seconds
3. Observe file list updates
4. Add a new log file to bjones directory externally
5. Wait 5 seconds

**Expected Results**:
- ✅ File list refreshes every 5 seconds
- ✅ New files appear automatically
- ✅ Modified timestamps updated
- ✅ No disruption to current viewing

**Pass/Fail Criteria**:
- [ ] **PASS** - Auto-refresh works, 5s interval maintained
- [ ] **FAIL** - No refresh, wrong interval, or disrupts viewing

---

### Scenario AR2: Pause on Minimize
**Priority**: P2 (Medium)  
**Acceptance Criteria**: FR-046

**Steps**:
1. Start auto-refresh (select user)
2. Minimize form
3. Wait 10 seconds
4. Restore form
5. Check logs for timer activity

**Expected Results**:
- ✅ Timer pauses when minimized (logged)
- ✅ Timer resumes when restored (logged)
- ✅ No background refreshes while minimized

**Pass/Fail Criteria**:
- [ ] **PASS** - Timer pauses/resumes correctly
- [ ] **FAIL** - Timer continues while minimized or doesn't resume

---

## Test Execution Checklist

### Pre-Test Setup
- [ ] Install latest MTM WIP Application build
- [ ] Verify test data (users with log files)
- [ ] Clear any previous test results
- [ ] Review performance logging configuration

### Test Execution
- [ ] Execute all 10 main scenarios (1.1 - 7.1)
- [ ] Execute 3 edge case scenarios (E1 - E3)
- [ ] Execute 2 auto-refresh scenarios (AR1 - AR2)
- [ ] Record all pass/fail results
- [ ] Note any deviations or unexpected behavior
- [ ] Capture screenshots of failures

### Post-Test Review
- [ ] Review performance logs for warnings
- [ ] Document any defects found
- [ ] Calculate pass rate (target: >95%)
- [ ] Prepare test summary report
- [ ] Share results with development team

---

## Defect Reporting Template

If defects are found during testing, report using this template:

**Defect ID**: [Auto-increment or assigned]  
**Scenario**: [e.g., Scenario 2.1]  
**Severity**: [Critical/High/Medium/Low]  
**Priority**: [P0/P1/P2/P3]  

**Description**: [Clear description of what went wrong]  

**Steps to Reproduce**:
1. [Step 1]
2. [Step 2]
3. [Step 3]

**Expected Result**: [What should happen]  
**Actual Result**: [What actually happened]  

**Environment**:
- Build Version: [e.g., 5.0.0]
- OS: [e.g., Windows 11]
- User Account: [e.g., bjones]

**Screenshots/Logs**: [Attach if available]

---

## Test Sign-Off

**Tested By**: ___________________________  
**Date**: ___________________________  
**Overall Result**: [ ] PASS  [ ] FAIL  
**Pass Rate**: _____ / 15 scenarios (target >95%)  

**Notes**: _________________________________________________________________

_________________________________________________________________

**Approved By**: ___________________________  
**Date**: ___________________________  

---

**End of Test Scenarios Document**
