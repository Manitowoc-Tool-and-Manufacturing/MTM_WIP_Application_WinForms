# Log Viewer Enhancements Summary

**Date**: 2025-10-28  
**Feature Branch**: 003-view-application-logs  
**Status**: Enhancement Design

---

## User-Approved Enhancements

Based on clarification responses, the following enhancements will be added to the View Application Logs feature:

### 1. CSV Log Format Migration

**Decision**: Option B - CSV Only (Delete old .log files when patch goes live)

- Change log file extensions from `.log` to `.csv`
- CSV structure: `Timestamp,Level,Source,Message,Details`
- Update LoggingUtility to write CSV format with proper escaping
- Update Service_LogFileReader to read `.csv` files
- Update Service_LogParser to parse CSV rows (no regex needed!)
- All old `.log` files will be irrelevant and deleted when patch goes live

**Benefits**:
- Eliminates parsing complexity - just read CSV!
- 100% reliable data extraction
- No more "Parse Failed" messages
- Easy to open in Excel for analysis

---

### 2. Structured Textbox Display

**Decision**: Option A - Vertical Stack Layout

Replace current text display with labeled, read-only textboxes:

```
┌─────────────────────────────────────┐
│ Timestamp: [ReadOnly TextBox      ]│
│ Level:     [ReadOnly TextBox      ]│
│ Source:    [ReadOnly TextBox      ]│
│ Message:   [ReadOnly MultiLine TB ]│
│ Details:   [ReadOnly MultiLine TB ]│
└─────────────────────────────────────┘
```

**UI Components**:
- Label + TextBox pairs for each field
- ReadOnly=true, TabStop=false
- Multiline=true for Message and Details
- ScrollBars=Vertical where needed
- Proper anchoring for resize

---

### 3. Copilot Prompt Generation System

**Core Feature**: Generate template-based Copilot prompts for error fixes

**Error Identification**: By calling method name (from stack trace)
- Example: `Control_InventoryTab_Button_Save_Click`
- File: `Prompt_Fix_Control_InventoryTab_Button_Save_Click.md`

**Folder Location**: Central location (All Users share)
```
C:\Users\johnk\OneDrive\...\WIP App Logs\
  └─ Prompt Fixes\
     └─ Prompt_Fix_Control_InventoryTab_Button_Save_Click.md
```

**Rationale**: If User A encounters error and generates prompt, User B shouldn't regenerate the same prompt

**Prompt Template Format**: Rich Context + Guided Prompt
```markdown
# Error Fix Prompt - {MethodName}

## Error Information
- **Timestamp**: {timestamp}
- **Error Type**: {exceptionType}
- **Message**: {errorMessage}
- **Location**: {filename}, line {lineNumber}
- **Method**: {methodName}

## Stack Trace
```
{stackTrace}
```

## Copilot Prompt

Fix the {exceptionType} error in the `{methodName}` method at line {lineNumber} 
of {filename}. The error occurs when {contextDescription}. Analyze the stack 
trace above and implement proper {suggestedFix}.

#file:{filename}
```

**Button Behavior**: 
- If prompt exists: Show dialog with "Open Existing Prompt" option
- Dialog text: "A prompt fix for this error has already been generated: {MethodName}"
- Options: [Open Existing Prompt] [Cancel]

**Button States**:
- **Normal Mode**: "Create Prompt" - Active only on ERROR/CRITICAL logs
- **Shift+Click Mode**: "Batch Creation" - Generate prompts for all unique errors in current file

**Button Placement**: Next to navigation buttons in the entry display panel

---

### 4. Feature #1: Prompt Status Tracking

**JSON Status File**: `C:\...\Prompt Fixes\Prompt_Status.json`

```json
{
  "prompts": [
    {
      "promptFile": "Prompt_Fix_Control_InventoryTab_Button_Save_Click.md",
      "methodName": "Control_InventoryTab_Button_Save_Click",
      "status": "In Progress",
      "createdDate": "2025-10-28T16:30:00",
      "lastUpdated": "2025-10-28T17:45:00",
      "assignee": "johnk",
      "notes": "Fixing null reference on quantity validation"
    }
  ]
}
```

**Status Values**: New, In Progress, Fixed, Won't Fix

**Developer UI**: 
- Add "Manage Prompt Status" dialog accessible from main viewer
- DataGridView showing all prompts with status
- Ability to update Status, Assignee, Notes
- Save updates back to JSON
- Filter view by status

---

### 5. Feature #2: Batch Prompt Generation

**Button Behavior**:
- **Normal Click**: "Create Prompt" - Generate for current entry
- **Shift+Hold**: Button text changes to "Batch Creation"
- **Shift+Click**: Generate prompts for all unique errors in current file

**Batch Process**:
1. Scan all entries in current log file
2. Extract unique method names from error stack traces
3. For each unique method:
   - Check if prompt already exists
   - If not, generate prompt file
   - If yes, skip and track as "already exists"
4. Generate summary report

**Summary Report Dialog**:
```
Batch Prompt Generation Complete

✅ Created: 8 new prompts
⚠️ Skipped: 3 already exist
❌ Failed: 1 (couldn't parse stack trace)

[View Created Prompts] [View Details] [Close]
```

---

### 6. Feature #3: Error Grouping/Deduplication

**Log List Enhancement**: Group identical errors

**Display Format**:
```
Entry 45/859 - NullReferenceException in Button_Save_Click (12 occurrences)
```

**Navigation Behavior**:
- Pressing Next when on grouped error: Skip to next UNIQUE error
- New button: "Show All Occurrences" - Expands to show all 12 instances
- Collapsible tree view in entry list showing occurrences

**Implementation**:
- Group by: ErrorType + MethodName
- Track occurrence indices
- Modified LogEntryNavigator to support grouping mode

---

### 7. Feature #4: Quick Fix Templates

**Template System**: Pre-defined fix suggestions for common errors

**Template Mapping**:
```csharp
Dictionary<string, string> QuickFixTemplates = new()
{
    { "NullReferenceException", "Add null checks before accessing object properties" },
    { "TimeoutException", "Increase timeout value or optimize query performance" },
    { "FileNotFoundException", "Validate file path exists before attempting to access" },
    { "SqlException", "Check database connection string and verify stored procedure exists" },
    { "InvalidOperationException", "Verify object is in valid state before operation" }
};
```

**Integration**: Template text inserted into generated prompt under "Suggested Fix Approach" section

---

### 8. Feature #5: Copy Error Context (Shift+Click)

**Button Behavior**:
- **Normal Click**: Copy current entry (Ctrl+C) - existing functionality
- **Shift+Hold**: Button text changes to "Copy Context"
- **Shift+Click**: Copy formatted error context ready for Copilot Chat

**Format Template**:
```
Error Context for Copilot Analysis
===================================

Error Type: {exceptionType}
Method: {methodName}
File: {filename}, Line {lineNumber}
Timestamp: {timestamp}

Message:
{errorMessage}

Stack Trace:
{stackTrace}

#file:{filename}

Please analyze this error and suggest a fix.
```

**Clipboard Action**: Copy formatted text, show toast "Error context copied to clipboard"

---

### 9. Feature #6: Error Severity Indicators

**Visual Color Coding**: Apply to log file list and entry display

**Colors**:
- 🔴 **Critical**: DarkRed background (crashes, unhandled exceptions)
- 🟠 **Error**: Red background (handled exceptions)
- 🟡 **Warning**: Yellow background (potential issues)
- 🔵 **Info**: Blue background (normal logs)

**Application**:
- File list: Row background color based on file type
- Entry display: Border color around entry panel
- Entry list (if added): Icon + color indicator per entry

---

### 10. Feature #7: Filter by Prompt Status

**New Filter Option**: "Errors Needing Attention"

**Filter UI**: Add to existing filter panel
- Checkbox: "Only errors without prompts"
- Checkbox: "Only errors with 'New' status"
- Checkbox: "Only errors with 'In Progress' status"

**Filter Logic**:
1. Check if current entry is an error
2. Extract method name from stack trace
3. Check if prompt file exists
4. If exists, load status from Prompt_Status.json
5. Include/exclude based on filter selection

---

### 11. Feature #9: Statistical Dashboard

**Report Type**: "Error Analysis Report"

**Access**: New button "Generate Error Report" in toolbar

**Report Sections**:

1. **Most Frequent Errors** (Top 10)
   - Error Type, Method Name, Occurrence Count
   - Percentage of total errors

2. **Error Trends Over Time** 
   - Errors per day (last 30 days)
   - Line chart showing trend

3. **Prompt Coverage**
   - Total unique errors: 45
   - Prompts created: 23 (51%)
   - Prompts in progress: 12 (26%)
   - Prompts fixed: 8 (18%)
   - No prompt: 22 (49%)

4. **Priority Recommendations**
   - Errors without prompts sorted by frequency
   - "Fix these first" list

**Progress Indication**:
- Reading all log files is time-consuming
- Show progress bar: "Analyzing logs... 45 of 150 files processed"
- Allow cancel during analysis
- Cache results for 1 hour

**Export Options**:
- Save report as HTML
- Save report as CSV
- Copy to clipboard

---

## Implementation Phases

### Phase 11: CSV Migration (Foundation)
- T052: Update LoggingUtility to write CSV format
- T053: Update Service_LogFileReader for CSV files
- T054: Simplify Service_LogParser (CSV parsing only)
- T055: Create test log data in CSV format
- T056: Update file extension patterns throughout codebase

### Phase 12: Structured Textbox Display
- T057: Redesign entry display panel with labeled textboxes
- T058: Implement vertical stack layout with proper anchoring
- T059: Wire up textbox population from CSV data
- T060: Test multi-line display and scrolling behavior

### Phase 13: Prompt Generation Core
- T061: Create Prompt Fixes central directory structure
- T062: Implement error method name extraction from stack traces
- T063: Create prompt template engine
- T064: Implement "Create Prompt" button with error-only activation
- T065: Check for existing prompts and show dialog
- T066: Implement "Open Existing Prompt" functionality

### Phase 14: Enhanced Features Set 1
- T067: Implement Prompt Status Tracking (JSON file management)
- T068: Create Developer UI for status management
- T069: Implement Shift+Click batch prompt generation
- T070: Create batch generation summary report
- T071: Implement error grouping/deduplication
- T072: Add Quick Fix Templates system
- T073: Implement Shift+Copy error context

### Phase 15: Enhanced Features Set 2
- T074: Add error severity color indicators
- T075: Implement "Filter by Prompt Status" options
- T076: Create Statistical Dashboard report generator
- T077: Implement progress bar for dashboard analysis
- T078: Add report export options (HTML/CSV)

---

## Success Criteria

### CSV Migration
- ✅ All log files write to `.csv` format
- ✅ CSV parsing is 100% reliable (no parse failures)
- ✅ Log viewer reads CSV files correctly
- ✅ Old `.log` files deleted on deployment

### Structured Display
- ✅ All fields display in labeled textboxes
- ✅ Multi-line fields scroll properly
- ✅ Layout responsive to window resize
- ✅ Read-only textboxes prevent editing

### Prompt Generation
- ✅ Prompts generate with complete context
- ✅ Existing prompt detection works
- ✅ Central Prompt Fixes folder created
- ✅ One prompt per unique method
- ✅ Batch generation processes all errors
- ✅ Summary report shows accurate counts

### Enhanced Features
- ✅ Status tracking updates persist
- ✅ Developer UI allows status management
- ✅ Error grouping reduces navigation noise
- ✅ Quick fix templates apply correctly
- ✅ Copy context formats properly for Copilot
- ✅ Color indicators visible and accurate
- ✅ Prompt status filtering works
- ✅ Statistical dashboard generates within reasonable time
- ✅ Dashboard progress bar updates smoothly
- ✅ Report exports successfully

---

## Testing Strategy

### Integration Testing
- End-to-end prompt generation flow
- Batch generation with mixed new/existing
- Status updates across multiple sessions
- Dashboard report with large dataset (1000+ files)

### Manual Testing
- Verify textbox layout at 96/120/144 DPI
- Test Shift+Click button text changes
- Validate color indicators are visible
- Confirm prompts paste correctly into Copilot Chat
- Test dashboard cancellation
- Final Task in Task.md is to generate a comprehensive Test.md file to help the developer test the entire feature

---

## Dependencies

**Blocked By**: None (CSV migration independent)

**Blocks**: None (enhancements are additive)

**Related Features**:
- Error Reporting (001-error-reporting-with) - Uses same log paths
- View Error Reports (002-view-error-reports) - Similar UI patterns

---

## Notes

- Prompt Fixes folder should be created on first use
- Status JSON should initialize empty if not exists
- Dashboard caching should use file modification times to invalidate
- Error grouping should be toggle-able (on/off preference)
- Template system should be extensible for future error types
