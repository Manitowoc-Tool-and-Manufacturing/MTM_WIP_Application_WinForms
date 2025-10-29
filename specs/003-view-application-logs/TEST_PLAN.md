# View Application Logs - Manual Test Plan

**Feature Branch**: `003-view-application-logs`  
**Test Plan Version**: 1.0  
**Last Updated**: 2025-10-29  
**Test Environment**: Windows 10/11, .NET 8.0, MySQL 5.7

---

## Test Environment Setup

### Prerequisites
- Windows 10 or Windows 11
- .NET 8.0 Runtime installed
- MySQL 5.7+ running (MAMP or standalone)
- MTM WIP Application built in Debug configuration
- Network storage accessible at configured base path
- Multiple user log directories with sample logs

### Test Data Requirements
- At least 3 user directories with log files
- Mix of Normal, Application Error, and Database Error logs
- At least one log file with 100+ entries
- At least one log file with ERROR/CRITICAL entries
- Some errors should have corresponding prompt files
- Some errors should have status entries in Prompt_Status.json

---

## Test Cases

### TC001: User Selection and Log File Loading

**Priority**: P1  
**User Story**: US1  
**Objective**: Verify user selection and log file loading functionality

#### Test Steps:
1. Launch View Application Logs Form
2. Verify user dropdown is populated with users from network storage
3. Verify "All Users" option appears in dropdown
4. Select a user from dropdown
5. Verify log files list populates for selected user
6. Verify files are grouped by type (Normal, App Error, DB Error)
7. Verify files are sorted by date descending (newest first)
8. Select a log file from list
9. Verify log file contents load into viewer
10. Verify entry count displays "Entry 1 of X"

#### Expected Results:
✅ User dropdown populates without errors  
✅ "All Users" option present  
✅ Log files load for selected user  
✅ Files grouped and sorted correctly  
✅ Log file contents display properly  
✅ Entry navigation works

#### Test Data:
- User: Any user with existing log files
- Expected: At least 5 log files of mixed types

---

### TC002: Log File Filtering

**Priority**: P1  
**User Story**: US2  
**Objective**: Verify log type filtering functionality

#### Test Steps:
1. Load logs for a user with multiple log types
2. Note total count of files
3. Select "Normal" from log type filter dropdown
4. Verify only Normal log files displayed
5. Select "Application Error" from filter
6. Verify only App Error log files displayed
7. Select "Database Error" from filter
8. Verify only DB Error log files displayed
9. Select "All" from filter
10. Verify all log files displayed again

#### Expected Results:
✅ Filter dropdown operational  
✅ Filtering works for each type  
✅ Correct files shown for each filter  
✅ "All" restores full list

---

### TC003: Parsed vs Raw View Toggle

**Priority**: P2  
**User Story**: US5  
**Objective**: Verify view mode switching

#### Test Steps:
1. Load a CSV log file with entries
2. Verify default view shows parsed fields (Timestamp, Level, Source, Message, Details)
3. Click "Show Raw Text" button
4. Verify view switches to plain text display
5. Verify raw CSV content visible
6. Click "Show Parsed Fields" button
7. Verify view switches back to structured display

#### Expected Results:
✅ Parsed view shows labeled textboxes  
✅ Raw view shows plain text  
✅ Toggle button text changes appropriately  
✅ Content preserved during toggle

---

### TC004: Entry Navigation

**Priority**: P1  
**User Story**: US3  
**Objective**: Verify navigation through log entries

#### Test Steps:
1. Load a log file with 50+ entries
2. Verify position shows "Entry 1 of X"
3. Click "Next Entry" button
4. Verify position updates to "Entry 2 of X"
5. Verify entry content changes
6. Click "Previous Entry" button
7. Verify position updates to "Entry 1 of X"
8. Navigate to last entry (click Next repeatedly or use keyboard)
9. Click "Next Entry" at last entry
10. Verify stays at last entry
11. Navigate to first entry
12. Click "Previous Entry" at first entry
13. Verify stays at first entry

#### Expected Results:
✅ Navigation buttons functional  
✅ Position label updates correctly  
✅ Entry content updates  
✅ Boundary conditions handled (first/last)  
✅ Keyboard shortcuts work (arrow keys)

---

### TC005: Export to CSV

**Priority**: P2  
**User Story**: US6  
**Objective**: Verify CSV export functionality

#### Test Steps:
1. Load a log file
2. Click "Export to CSV" button
3. Choose save location in file dialog
4. Verify file saved successfully
5. Open exported CSV in Excel or text editor
6. Verify header row present: Timestamp,Level,Source,Message,Details
7. Verify all entries exported
8. Verify CSV properly formatted (quotes, escaping)
9. Verify special characters handled (commas, newlines, quotes)

#### Expected Results:
✅ Export dialog appears  
✅ File saves successfully  
✅ CSV format correct  
✅ All entries present  
✅ Special characters escaped properly

---

### TC006: Copy Current Entry

**Priority**: P2  
**User Story**: US6  
**Objective**: Verify copy to clipboard functionality

#### Test Steps:
1. Load a log file and navigate to an entry
2. Press Ctrl+C or click Copy button (if present)
3. Open Notepad or another text editor
4. Paste clipboard contents (Ctrl+V)
5. Verify entry details pasted as formatted text
6. Verify includes: Timestamp, Level, Source, Message, Details

#### Expected Results:
✅ Copy operation successful  
✅ Formatted text in clipboard  
✅ All fields included  
✅ Readable format

---

### TC007: Refresh and Auto-Refresh

**Priority**: P1  
**User Story**: US2  
**Objective**: Verify file list refresh functionality

#### Test Steps:
1. Load logs for a user
2. Note file count
3. Add a new log file to user's directory (manually or via logging)
4. Click "Refresh" button
5. Verify new file appears in list
6. Check "Auto-Refresh" checkbox
7. Wait 5+ seconds
8. Add another log file
9. Verify file list updates automatically
10. Uncheck "Auto-Refresh"
11. Verify auto-refresh stops

#### Expected Results:
✅ Manual refresh updates file list  
✅ New files appear  
✅ Auto-refresh activates  
✅ Auto-refresh interval ~5 seconds  
✅ Auto-refresh deactivates when unchecked

---

### TC008: Error Dialog Integration

**Priority**: P1  
**User Story**: US7  
**Objective**: Verify opening logs from error dialog

#### Test Steps:
1. Trigger an application error (or use existing error report)
2. Open Error Reports dialog
3. Select an error report
4. Click "View Logs" or equivalent button
5. Verify View Application Logs Form opens
6. Verify correct user auto-selected
7. Verify most recent log file auto-loaded
8. Verify error entry visible

#### Expected Results:
✅ Form opens from error dialog  
✅ User pre-selected  
✅ Most recent log loaded  
✅ Error entry found

---

### TC009: Copilot Prompt Generation

**Priority**: P2  
**User Story**: US9  
**Objective**: Verify prompt file generation for errors

#### Test Steps:
1. Load a log file with ERROR or CRITICAL entries
2. Navigate to an error entry
3. Verify "Create Prompt" button enabled
4. Click "Create Prompt" button
5. Verify prompt file created in Prompt Fixes directory
6. Verify file named "Prompt_Fix_{MethodName}.md"
7. Open prompt file
8. Verify includes: Error Info, Stack Trace, #file: reference, Suggested Fix section
9. Navigate to same error again
10. Click "Create Prompt" button
11. Verify "Existing Prompt" dialog appears
12. Click "Open Existing Prompt"
13. Verify file opens in default markdown editor

#### Expected Results:
✅ Button enabled only for ERROR/CRITICAL  
✅ Prompt file generated  
✅ File naming correct  
✅ Content includes all required sections  
✅ Existing prompt detected  
✅ File opens successfully

---

### TC010: Batch Prompt Generation

**Priority**: P2  
**User Story**: US10  
**Objective**: Verify batch processing of errors

#### Test Steps:
1. Load a log file with 10+ unique error entries
2. Hold Shift key and hover over "Create Prompt" button
3. Verify button text changes to "Batch Creation"
4. While holding Shift, click button
5. Verify batch processing starts
6. Wait for completion
7. Verify summary dialog appears
8. Verify shows: Created count, Skipped count, Failed count
9. Verify DataGridView shows per-prompt breakdown
10. Verify rows color-coded (Green=Created, Yellow=Skipped, Red=Failed)
11. Click "View Created Prompts" button
12. Verify Prompt Fixes folder opens in Explorer
13. Verify new prompt files present

#### Expected Results:
✅ Shift+hover changes button text  
✅ Batch processing executes  
✅ Only creates prompts for unique errors  
✅ Skips existing prompts  
✅ Summary dialog accurate  
✅ DataGridView populated  
✅ Color coding applied  
✅ Folder opens correctly

---

### TC011: Prompt Status Management

**Priority**: P2  
**User Story**: US11  
**Objective**: Verify prompt status tracking

#### Test Steps:
1. Generate several prompt files (via TC009 or TC010)
2. Click "Manage Prompt Status" menu item or button
3. Verify Prompt Status Manager dialog opens
4. Verify DataGridView shows all prompt files
5. Verify columns: Method Name, Status, Assignee, Notes, Created, Last Updated
6. Change status of a prompt to "In Progress"
7. Verify row background changes to yellow
8. Add assignee name
9. Add notes
10. Click "Save" button
11. Close dialog
12. Reopen Prompt Status Manager
13. Verify changes persisted
14. Verify Prompt_Status.json file updated

#### Expected Results:
✅ Dialog opens  
✅ All prompts listed  
✅ Status dropdown functional  
✅ Row color updates (New=Blue, InProgress=Yellow, Fixed=Green, WontFix=Gray)  
✅ Edits save correctly  
✅ JSON file updated  
✅ Changes persist across sessions

---

### TC012: Prompt Status Filtering

**Priority**: P3  
**User Story**: US14  
**Objective**: Verify filtering by prompt status

#### Test Steps:
1. Generate prompts with mixed statuses (some New, some In Progress, etc.)
2. Load a log file with errors
3. Check "Only errors without prompts" filter checkbox
4. Verify navigation only shows errors lacking prompt files
5. Uncheck that filter
6. Check "Only 'New' status" filter checkbox
7. Verify navigation only shows errors with New status
8. Check "Only 'In Progress' status" filter checkbox
9. Verify navigation only shows errors with In Progress status
10. Check multiple status filters
11. Verify AND logic applied (must match all selected)

#### Expected Results:
✅ Filter checkboxes operational  
✅ "Without prompts" filter works  
✅ Status filters work individually  
✅ Multiple filters combine correctly  
✅ Navigation updates properly

---

### TC013: Color Coding

**Priority**: P3  
**User Story**: US14  
**Objective**: Verify visual indicators

#### Test Steps:
1. Load logs for a user with mixed log types
2. Verify file list rows color-coded:
   - Normal logs: Light blue background
   - App Error logs: Light coral background
   - DB Error logs: Light yellow background
3. Load a log file with mixed severity entries
4. Navigate through entries
5. Verify entry panel border color changes:
   - CRITICAL: Dark red
   - ERROR: Red
   - WARNING: Yellow
   - INFO: Blue
6. Verify position label includes emoji prefix for critical errors (🔴)

#### Expected Results:
✅ File list color coding correct  
✅ Entry panel border updates  
✅ Colors match severity levels  
✅ Emoji prefix appears for critical

---

### TC014: Quick Fix Templates

**Priority**: P3  
**User Story**: US13  
**Objective**: Verify error-specific fix suggestions

#### Test Steps:
1. Navigate to an error entry (NullReferenceException preferred)
2. Click "Create Prompt" button
3. Open generated prompt file
4. Verify "Suggested Fix Approach:" section present
5. Verify fix steps specific to error type
6. Verify numbered steps (1., 2., 3., etc.)
7. Test with different error types:
   - NullReferenceException
   - TimeoutException
   - FileNotFoundException
   - SqlException
   - InvalidOperationException
8. Verify each has unique, relevant suggestions

#### Expected Results:
✅ Fix suggestions included in prompt  
✅ Suggestions error-type specific  
✅ Steps numbered and actionable  
✅ Templates available for 15+ error types

---

### TC015: Shift+Ctrl+C Error Context Copy

**Priority**: P3  
**User Story**: US13  
**Objective**: Verify Copilot-ready error copying

#### Test Steps:
1. Navigate to an ERROR or CRITICAL entry
2. Press Shift+Ctrl+C
3. Verify information dialog appears
4. Open Copilot Chat or any text editor
5. Paste clipboard contents
6. Verify formatted error context includes:
   - Error Type
   - Method name
   - File and line number
   - Timestamp
   - Message
   - Stack Trace (in code block)
   - #file: reference
   - Analysis questions
7. Navigate to a non-error entry (INFO)
8. Press Shift+Ctrl+C
9. Verify warning message that feature only works for errors

#### Expected Results:
✅ Keyboard shortcut works  
✅ Context formatted correctly  
✅ All sections present  
✅ #file: reference included  
✅ Only works for ERROR/CRITICAL  
✅ User-friendly message for non-errors

---

### TC016: DPI Scaling and UI Responsiveness

**Priority**: P1 (Technical)  
**Objective**: Verify UI scales properly across resolutions

#### Test Steps:
1. Open form on 1080p monitor
2. Verify form minimum size 1200x800
3. Verify all controls visible and properly sized
4. Verify buttons minimum 100x30 pixels
5. Verify proper padding (10px) and margins (5px)
6. If available, test on 4K monitor or change Windows DPI scaling
7. Verify form scales proportionally
8. Verify text remains readable
9. Verify no overlapping controls
10. Resize form window
11. Verify TableLayoutPanel adjusts properly

#### Expected Results:
✅ Form renders at minimum size  
✅ Controls properly sized  
✅ Padding and margins consistent  
✅ DPI scaling works (AutoScaleMode.Dpi)  
✅ Responsive layout functional

---

### TC017: Performance and Error Handling

**Priority**: P1 (Technical)  
**Objective**: Verify performance and error recovery

#### Test Steps:
1. Load a large log file (10MB+, 1000+ entries)
2. Verify load completes within reasonable time (< 5 seconds)
3. Verify UI remains responsive during load
4. Navigate through entries
5. Verify navigation responds within 100ms
6. Test network path not accessible scenario:
   - Disconnect network or change path
   - Select user
   - Verify graceful error message
7. Test corrupted log file:
   - Create file with invalid CSV format
   - Select file
   - Verify error handled gracefully
8. Monitor memory usage
9. Verify no obvious memory leaks

#### Expected Results:
✅ Large files load acceptably  
✅ UI responsive during operations  
✅ Navigation fast (< 100ms)  
✅ Network errors handled gracefully  
✅ Parse errors handled gracefully  
✅ No memory leaks

---

### TC018: Keyboard Shortcuts

**Priority**: P2 (Technical)  
**Objective**: Verify all keyboard shortcuts functional

#### Test Steps:
1. Load a log file
2. Test navigation shortcuts:
   - Right Arrow → Next Entry
   - Left Arrow → Previous Entry
3. Test Copy shortcut:
   - Ctrl+C → Copy Current Entry
4. Test Error Context shortcut:
   - Shift+Ctrl+C → Copy Error Context (on error entry)
5. Test Prompt Generation shortcut:
   - Ctrl+P → Create Prompt (on error entry)
6. Test Refresh shortcut:
   - F5 → Refresh file list
7. Test Error Report shortcut:
   - Ctrl+R → Generate Error Report (if implemented)

#### Expected Results:
✅ All shortcuts respond correctly  
✅ No conflicts with system shortcuts  
✅ Shortcuts documented or intuitive

---

## Test Execution Checklist

### Pre-Test Setup
- [ ] Test environment configured per prerequisites
- [ ] Test data available (user directories, log files)
- [ ] Application built in Debug configuration
- [ ] Prompt Fixes directory accessible
- [ ] Prompt_Status.json backed up (if exists)

### Core Functionality (P1)
- [ ] TC001: User Selection and Log File Loading
- [ ] TC002: Log File Filtering
- [ ] TC004: Entry Navigation
- [ ] TC007: Refresh and Auto-Refresh
- [ ] TC008: Error Dialog Integration
- [ ] TC016: DPI Scaling and UI Responsiveness
- [ ] TC017: Performance and Error Handling

### Enhanced Features (P2)
- [ ] TC003: Parsed vs Raw View Toggle
- [ ] TC005: Export to CSV
- [ ] TC006: Copy Current Entry
- [ ] TC009: Copilot Prompt Generation
- [ ] TC010: Batch Prompt Generation
- [ ] TC011: Prompt Status Management
- [ ] TC018: Keyboard Shortcuts

### Quality of Life (P3)
- [ ] TC012: Prompt Status Filtering
- [ ] TC013: Color Coding
- [ ] TC014: Quick Fix Templates
- [ ] TC015: Shift+Ctrl+C Error Context Copy

---

## Defect Reporting Template

```markdown
### Defect #XXX: [Brief Description]

**Severity**: Critical / High / Medium / Low
**Test Case**: TCXXX
**Frequency**: Always / Sometimes / Rare

**Steps to Reproduce**:
1. Step one
2. Step two
3. Step three

**Expected Result**:
What should happen

**Actual Result**:
What actually happened

**Environment**:
- OS: Windows 10/11
- .NET Version: 8.0.x
- Branch: 003-view-application-logs
- Build: Debug/Release

**Screenshots/Logs**:
[Attach if applicable]

**Workaround**:
[If known]
```

---

## Test Sign-Off

### Test Summary
- **Total Test Cases**: 18
- **Passed**: ___
- **Failed**: ___
- **Blocked**: ___
- **Pass Rate**: ___%

### Test Coverage
- **P1 Features**: ___/7 passed (___%)
- **P2 Features**: ___/7 passed (___%)
- **P3 Features**: ___/4 passed (___%)

### Sign-Off
- **Tester Name**: _______________
- **Test Date**: _______________
- **Build Version**: _______________
- **Approved for Release**: Yes / No / With Conditions

### Notes
[Any additional observations or recommendations]

---

**End of Test Plan**
