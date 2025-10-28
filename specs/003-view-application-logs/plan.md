# Implementation Plan: View Application Logs with User Selection

**Branch**: `003-view-application-logs` | **Date**: 2025-10-28 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/003-view-application-logs/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Create a log viewer form that enables developers to select any user from a dropdown, browse their CSV log files from network storage, view parsed log entries in structured textboxes with type-specific formatting (Normal/Application Error/Database Error), apply filtering by date/severity/source/search text, toggle between parsed and raw views, and export/copy log data.

**Enhancement Features (Added 2025-10-28)**: CSV log format migration for 100% reliable parsing, structured textbox display with labeled fields, Copilot prompt generation system with central storage and status tracking, batch prompt creation, error grouping/deduplication, quick fix templates, enhanced copy-to-clipboard, color-coded severity indicators, prompt status filtering, and comprehensive statistical error analysis dashboard with progress tracking.

The form must handle three distinct CSV log formats with appropriate field layouts, support performance requirements (sub-100ms UI, async file I/O), integrate with existing error dialog system, and provide developer tools for systematic error resolution through automated prompt generation and tracking.

## Technical Context

**Language/Version**: C# 12 / .NET 8.0 Windows Forms  
**Primary Dependencies**: MySql.Data 9.4.0, System.Text.Json, Microsoft.Web.WebView2, ClosedXML  
**Storage**: MySQL 5.7+ (stored procedures only), local file system for log files on network storage  
**Testing**: Manual validation approach with defined success criteria  
**Target Platform**: Windows desktop (primary), high-DPI scaling support required  
**Project Type**: Single WinForms desktop application  
**Performance Goals**: Sub-100ms UI response, async file I/O, parse 1000-entry file in <2s  
**Constraints**: AutoScaleMode.Dpi required, 30-second database timeout, memory stable during extended sessions  
**Scale/Scope**: 20-100 users generating logs, 50+ log files per user typical, 1000-5000 entries per file

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Applicable Principles

#### I. Stored Procedure Only Database Access
**Status**: ✅ PASS  
**Rationale**: Feature does not require new database operations. Log retrieval uses file system I/O only. Existing user data may be retrieved via existing stored procedures if needed for user dropdown population.

#### II. DaoResult<T> Wrapper Pattern
**Status**: ✅ PASS  
**Rationale**: No new DAO methods required. If existing DAOs are used (e.g., user list retrieval), they already comply with DaoResult pattern.

#### III. Region Organization and Method Ordering
**Status**: ✅ PASS (Post-Phase 1)  
**Rationale**: quickstart.md defines ViewApplicationLogsForm with standard region organization: Fields → Properties → Progress Control Methods → Constructors → Specific Functionality → Key Processing → Button Clicks → ComboBox & UI Events → Helpers → Cleanup. Implementation must follow this structure.

#### IV. Manual Validation Testing Approach
**Status**: ✅ PASS  
**Rationale**: Spec includes comprehensive test scenarios (7 user stories with independent tests). Success criteria defined (SC-001 through SC-012). quickstart.md includes detailed manual validation procedures. Manual validation approach aligns with constitution.

#### V. Environment-Aware Database Selection
**Status**: ✅ PASS  
**Rationale**: Feature uses file system for log access, not database. If database access required (user list), existing Helper_Database_Variables handles environment selection.

#### VI. Async-First UI Responsiveness
**Status**: ✅ PASS (Post-Phase 1)  
**Rationale**: research.md defines async file I/O patterns. Service_LogFileReader uses async methods with CancellationToken support. quickstart.md demonstrates async event handlers (async void for UI events). All file operations will execute asynchronously per FR-041/FR-043.

#### VII. Centralized Error Handling with Service_ErrorHandler
**Status**: ✅ PASS (Post-Phase 1)  
**Rationale**: research.md Section 6 defines error handling integration with Service_ErrorHandler. quickstart.md Phase 6 demonstrates error handling patterns with severity classification and context data. No MessageBox.Show() usage planned.

#### VIII. Documentation and XML Comments
**Status**: ✅ PASS (Post-Phase 1)  
**Rationale**: data-model.md includes XML documentation on all model classes and methods. contracts/README.md defines interface contracts with XML comments. Implementation will include XML docs on all public APIs.

### Technology Stack Compliance
**Status**: ✅ PASS  
**Rationale**: .NET 8.0 WinForms with file system I/O. No ORM usage. No forbidden practices detected. Core_Themes integration for DPI scaling.

### Security Best Practices
**Status**: ✅ PASS (Post-Phase 1)  
**Rationale**: research.md Section 3 defines Helper_LogPath with Path.Combine, regex validation, and path traversal prevention. contracts/README.md documents security contracts. All FR-047 through FR-051 requirements addressed in design.

### Performance Standards
**Status**: ✅ PASS (Post-Phase 1)  
**Rationale**: research.md Section 7 defines performance monitoring and windowing strategy. quickstart.md Phase 4 includes performance validation tests. All SC-001 through SC-007 benchmarks mapped to implementation patterns.

### Summary
**Post-Phase 1 Status**: ✅ PASS - All compliance requirements satisfied by design artifacts  
**Items Verified**:
- ✅ Region organization defined in quickstart.md
- ✅ Async-first file I/O patterns documented in research.md
- ✅ Service_ErrorHandler integration demonstrated in multiple docs
- ✅ XML documentation included in data-model.md and contracts
- ✅ Security validation (Path.Combine, input validation, path traversal prevention) in Helper_LogPath design
- ✅ Performance monitoring and windowing patterns documented with test cases

**Ready for Phase 2 (Tasks)**: Yes - No blocking constitution violations

## Project Structure

### Documentation (this feature)

```
specs/003-view-application-logs/
├── spec.md              # Feature specification (complete)
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

```
MTM_WIP_Application_WinForms/
├── Forms/
│   └── ViewLogs/                           # New directory for log viewer
│       ├── ViewApplicationLogsForm.cs      # Main form class with region organization
│       ├── ViewApplicationLogsForm.Designer.cs
│       └── ViewApplicationLogsForm.resx
├── Models/
│   ├── Model_LogEntry.cs                   # Parsed log entry with type-specific fields
│   ├── Model_LogFile.cs                    # Log file metadata (path, size, type, date)
│   ├── Model_LogFilter.cs                  # Active filter criteria
│   └── Model_UserLogDirectory.cs           # User log directory info
├── Services/
│   ├── Service_LogParser.cs                # Parses log entries with format detection
│   └── Service_LogFileReader.cs            # Async file reading with windowing
├── Helpers/
│   └── Helper_LogPath.cs                   # Path construction with security validation
└── Tests/
    └── Manual/
        └── ViewApplicationLogs_TestScenarios.md  # Manual validation scenarios
```

**Structure Decision**: Single WinForms project structure with new Forms/ViewLogs directory for log viewer form. Services layer for parsing and file I/O. Models for data structures. Helpers for path management with security validation (Path.Combine, traversal prevention).

---

## Enhancement Architecture (Added 2025-10-28)

### CSV Log Format Migration

**Decision**: Migrate from text-based .log files to CSV format for 100% reliable parsing.

**File Structure**:
```csv
Timestamp,Level,Source,Message,Details
"2025-10-28 16:30:41",ERROR,Control_InventoryTab,"NullReferenceException: Object reference...","at MTM.Application.Control_InventoryTab.Button_Save_Click()..."
```

**Benefits**:
- Eliminates regex parsing complexity and failures
- CSV escaping handles commas, quotes, newlines automatically
- Can open in Excel for ad-hoc analysis
- 100% parse success rate (SC-013)

**Updated Components**:
- `LoggingUtility.cs`: Write CSV format with proper escaping
- `Service_LogFileReader.cs`: Read .csv files instead of .log
- `Service_LogParser.cs`: Simplified - just split CSV fields (no regex)
- File extension pattern: `*_normal.csv`, `*_app_error.csv`, `*_db_error.csv`

### Copilot Prompt Generation System

**Central Storage Structure**:
```
C:\Users\{user}\OneDrive\...\WIP App Logs\
└── Prompt Fixes\                          # Central location (all users share)
    ├── Prompt_Status.json                 # Status tracking file
    ├── QuickFixTemplates.json             # Custom templates (optional)
    ├── Prompt_Fix_Control_InventoryTab_Button_Save_Click.md
    ├── Prompt_Fix_Dao_Inventory_GetAllItems.md
    └── ...
```

**Rationale for Central Location**: If User A encounters error and generates prompt, User B shouldn't regenerate - they should see it already exists and check status.

**New Components**:
```
MTM_WIP_Application_WinForms/
├── Services/
│   ├── Service_PromptGenerator.cs         # Template-based prompt generation
│   ├── Service_PromptStatusManager.cs     # JSON status persistence
│   └── Service_ErrorAnalyzer.cs           # Statistical dashboard generator
├── Models/
│   ├── Model_PromptStatus.cs              # Status tracking model
│   ├── Model_ErrorStatistics.cs           # Dashboard statistics
│   ├── Model_ErrorGroup.cs                # Error grouping model
│   └── Model_DashboardReport.cs           # Complete report model
└── Forms/
    └── ViewLogs/
        ├── PromptStatusManagerDialog.cs   # Developer UI for status management
        ├── BatchGenerationReportDialog.cs # Batch creation summary
        └── ErrorAnalysisReportDialog.cs   # Statistical dashboard
```

**Prompt Template Structure**:
```markdown
# Error Fix Prompt - {MethodName}

## Error Information
- **Timestamp**: {timestamp}
- **Error Type**: {exceptionType}
- **Message**: {errorMessage}
- **Location**: {fileName}, line {lineNumber}
- **Method**: {methodName}

## Stack Trace
```
{stackTrace}
```

## Suggested Fix Approach
{quickFixTemplate}

## Copilot Prompt
Fix the {exceptionType} error in the `{methodName}` method...

#file:{fileName}
```

### Status Tracking System

**JSON Structure** (`Prompt_Status.json`):
```json
{
  "prompts": [
    {
      "promptFile": "Prompt_Fix_Control_InventoryTab_Button_Save_Click.md",
      "methodName": "Control_InventoryTab_Button_Save_Click",
      "status": "InProgress",
      "createdDate": "2025-10-28T16:30:00",
      "lastUpdated": "2025-10-28T17:45:00",
      "assignee": "johnk",
      "notes": "Fixing null reference on quantity validation"
    }
  ]
}
```

**Status Workflow**:
1. Prompt generated → Status = "New" (blue background)
2. Developer starts work → Status = "InProgress" (yellow background)
3. Fix deployed → Status = "Fixed" (green background)
4. Won't address → Status = "WontFix" (gray background)

**Developer UI**: DataGridView with editable Status/Assignee/Notes columns, color-coded rows, save changes back to JSON.

### Error Grouping Architecture

**Grouping Strategy**: Group by `ErrorType + MethodName`

**Navigation Modes**:
- **Flat Mode** (default): Navigate all 1000 entries sequentially
- **Grouped Mode**: Navigate 50 unique errors, show occurrence counts

**Display Example**:
```
Entry 5 of 50 (1000 total)
NullReferenceException in Button_Save_Click (32 occurrences)
```

**Implementation**: `LogEntryNavigator` class maintains:
- `_allEntries`: Complete list
- `_filteredIndices`: After applying filters
- `_groupedEntries`: Dictionary<groupKey, List<int>>
- `_groupingEnabled`: Toggle grouping on/off

### Statistical Dashboard Architecture

**Report Sections**:
1. **Most Frequent Errors** (Top 10 table)
2. **Error Trends** (30-day bar chart)
3. **Prompt Coverage** (Pie chart with percentages)
4. **Priority Recommendations** (Sorted list)

**Generation Process**:
1. Scan all users' CSV log files
2. Parse error entries (skip INFO/WARNING)
3. Group by ErrorType + MethodName
4. Count occurrences, calculate frequencies
5. Check prompt existence and status
6. Generate report data
7. Cache for 1 hour (check file mod times)

**Progress Tracking**: Progress bar updates every 10 files to avoid UI freezes. CancellationToken support for early abort.

**Export Formats**:
- **HTML**: Styled with embedded CSS, tables, charts
- **CSV**: ZIP file with separate CSVs per section
- **Clipboard**: Plain text with ASCII tables

### UI Enhancement Patterns

**Structured Textbox Layout**:
```
┌─────────────────────────────────────┐
│ Timestamp: [ReadOnly TextBox      ]│
│ Level:     [ReadOnly TextBox      ]│
│ Source:    [ReadOnly TextBox      ]│
│ Message:   [Multiline ReadOnly TB ]│
│ Details:   [Multiline ReadOnly TB ]│
└─────────────────────────────────────┘
```

**Button State Management**:
- **Normal Mode**: "Create Prompt" (enabled for ERROR/CRITICAL only)
- **Shift+Hold**: "Batch Creation" (text changes)
- **Shift+Click**: Execute batch generation

**Color Indicators**:
- File list row backgrounds: Normal=LightBlue, AppError=LightCoral, DbError=LightYellow
- Entry panel borders: Critical=DarkRed, Error=Red, Warning=Yellow, Info=Blue
- Position label emojis: 🔴 Critical, 🟠 Error, 🟡 Warning, 🔵 Info

## Complexity Tracking

*No constitution violations requiring justification.*

---

## Relevant Instruction Files

**Note**: These instruction files provide coding patterns and standards for implementation. Review relevant files when moving from planning to task execution.

### Core Development:
- `.github/instructions/csharp-dotnet8.instructions.md` - Language features, WinForms patterns, async/await, file organization
- `.github/instructions/mysql-database.instructions.md` - Stored procedures, connection management, Helper_Database_StoredProcedure usage
- `.github/instructions/documentation.instructions.md` - XML comments, README structure, code documentation

### Quality & Security:
- `.github/instructions/testing-standards.instructions.md` - Manual validation approach, success criteria, test scenarios
- `.github/instructions/integration-testing.instructions.md` - Discovery-first workflow, method signature verification, DAO testing patterns
- `.github/instructions/security-best-practices.instructions.md` - Input validation, credential management, SQL injection prevention
- `.github/instructions/performance-optimization.instructions.md` - Async patterns, connection pooling, memory management, caching strategies
- `.github/instructions/code-review-standards.instructions.md` - Quality gates, review process, common issues

### When to Use:
- **During task generation** (`/speckit.tasks`): Reference to add instruction file pointers to specific tasks
- **During implementation** (`/speckit.implement`): Load instruction files to apply correct patterns
- **During code review**: Validate compliance with documented standards

**Instruction File Reference Format in Tasks**:
```markdown
- [ ] T100 - Implement DAO method for inventory queries
  - **Reference**: .github/instructions/mysql-database.instructions.md - Use Helper_Database_StoredProcedure pattern
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md - Follow async/await patterns
```

---
