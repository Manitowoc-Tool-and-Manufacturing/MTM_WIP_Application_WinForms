# Stored Procedure Validation Tool

A portable, comprehensive stored procedure analysis and validation tool that can work with any project.

## 📁 Files Overview

### Configuration
- **`SPSettings.json`** - Project-specific configuration (database credentials, paths, analysis settings)

### Analysis Scripts
- **`0-Analyze-Procedures-Complete.ps1`** - Analyzes stored procedure patterns, DML operations, transaction handling
- **`1-Analyze-SQL-Operations.ps1`** - Detailed SQL operation breakdown (INSERT/UPDATE/DELETE with columns, values, conditions)
- **`2-Trace-Complete-CallHierarchy.ps1`** - Traces UI → DAO → Stored Procedure call chains
- **`3-Merge-All-Analysis.ps1`** - Merges all analysis data into single CSV
- **`RUN-COMPLETE-ANALYSIS.ps1`** - Master script that runs all analysis steps

### Interactive Review Tool
- **`procedure-review-tool.html`** - Interactive HTML viewer for reviewing and correcting analysis data

### Generated Files (after running analysis)
- **`procedure-transaction-analysis.csv`** - Master CSV with all analysis data
- **`sql-operations-detailed.json`** - Detailed SQL operation breakdown
- **`call-hierarchy-complete.json`** - Complete call hierarchy data
- **`procedure-base-analysis.csv`** - Base pattern analysis

## 🚀 Quick Start

### 1. Configure Settings

Edit `SPSettings.json` to match your project:

```json
{
  "ProjectSettings": {
    "ProjectName": "YourProjectName",
    "RootDirectory": "C:\\Path\\To\\Your\\Project"
  },
  "Paths": {
    "StoredProceduresFolder": "..\\Database\\StoredProcedures",
    "CSharpCodeFolder": ".."
  },
  "DatabaseConnection": {
    "Server": "localhost",
    "Database": "your_database",
    "Username": "your_username",
    "Password": "your_password"
  }
}
```

### 2. Run Analysis

```powershell
cd StoredProcedureValidation
.\RUN-COMPLETE-ANALYSIS.ps1
```

This will:
1. ✅ Analyze all stored procedures for patterns and DML operations
2. ✅ Extract detailed SQL operation breakdowns
3. ✅ Trace complete call hierarchies from UI to database
4. ✅ Merge all data into master CSV
5. ✅ Generate JSON files for detailed inspection

### 3. Review Results

Open `procedure-review-tool.html` in your browser:
- **Automatic Configuration**: HTML tool reads `SPSettings.json` for stored procedure paths
- Load the generated `procedure-transaction-analysis.csv`
- Review each stored procedure's analysis
- Click "View SQL" button to see actual procedure code (loaded from configured path)
- Add corrections and notes where needed
- Mark procedures as reviewed
- Flag procedures needing attention
- Export corrected CSV when done

**Note**: The HTML viewer automatically uses the `StoredProceduresFolder` path from SPSettings.json, so it works with any project structure without code changes.

## 📊 Features

### Stored Procedure Analysis
- **Pattern Detection**: READ_ONLY, SINGLE_STEP, MULTI_STEP_SEQUENTIAL, MULTI_STEP_CONDITIONAL
- **DML Operation Counting**: INSERT, UPDATE, DELETE counts
- **Transaction Handling Detection**: Identifies explicit transactions
- **Validation Detection**: Identifies input validation logic
- **Domain Categorization**: Inventory, Transactions, Logging, Master Data, System, Users

### SQL Operation Breakdown
- **Operation Type**: INSERT, UPDATE, DELETE, SELECT
- **Target Tables**: Which tables are affected
- **Columns**: Which columns are being modified/queried
- **Values**: What values are being set (parameterized)
- **Conditions**: WHERE clause conditions

### Call Hierarchy Tracing
- **Event Handler → DAO → Stored Procedure** complete chain
- **Method Chain**: Intermediate methods between UI and DAO
- **Tables Accessed**: All tables touched by the procedure
- **Return Types**: What the procedure returns
- **UI Elements Updated**: Success/failure UI update methods

### Interactive Review
- **Navigation**: Previous/Next, Jump to specific procedure, keyboard shortcuts (arrows, Home, End)
- **Filtering**: By domain, pattern, confidence, review status, search by name
- **Editing**: Add corrections and notes inline
- **Validation Tracking**: Mark as reviewed, flag for attention (auto-flags on edit)
- **SQL Viewer**: View actual stored procedure SQL in modal popup
- **Progress Persistence**: Save/load review sessions with all edits and position
- **Export**: Download corrected CSV with all changes

#### HTML Tool Features
1. **Drag-and-Drop**: Load CSV by dragging file onto the page
2. **Tooltips**: Hover over field labels to see explanations
3. **JSON Formatting**: Pretty-printed JSON for CallHierarchy and SQLOperations
4. **Auto-Flag**: Editing any field automatically sets NeedsAttention=True
5. **Progress Tracking**: Visual progress bar showing review completion
6. **Session Persistence**: 
   - Save progress to JSON file (includes all edits, review status, filters, position)
   - Load previous session to continue where you left off
   - Multiple save files supported (timestamped filenames)

## 🔧 Configuration Options

### SPSettings.json Sections

#### ProjectSettings
- `ProjectName`: Your project name
- `RootDirectory`: Absolute path to project root
- `Description`: Project description

#### Paths
- `StoredProceduresFolder`: Relative path to stored procedures
- `CSharpCodeFolder`: Relative path to C# code
- `ExcludeFolders`: Folders to skip during analysis
- `OutputCSV`: Name of output CSV file
- `SQLOperationsJSON`: Name of SQL operations JSON file
- `CallHierarchyJSON`: Name of call hierarchy JSON file

#### DatabaseConnection
- `Server`: Database server address
- `Port`: Database port (default: 3306 for MySQL)
- `Database`: Database name
- `Username`: Database username
- `Password`: Database password

#### AnalysisSettings
- `EnableProgressBars`: Show progress during analysis
- `RecursiveSearch`: Search subdirectories
- `DetectEventHandlers`: Trace UI event handlers
- `TraceCallHierarchy`: Build complete call chains
- `AnalyzeSQLOperations`: Extract SQL operation details
- `MaxCallDepth`: Maximum call chain depth
- `EventHandlerPatterns`: Patterns to detect event handlers

#### DomainCategories
- Define domain categorization rules (prefix matching)

#### FilePatterns
- Define where to find DAOs, Forms, Helpers, SQL files

#### ValidationSettings
- Define validation rules for stored procedures

#### HTMLViewerSettings
- Customize the interactive review tool appearance

## 📝 Workflow

### Analysis Phase
1. Configure `SPSettings.json` for your project
2. Run `RUN-COMPLETE-ANALYSIS.ps1`
3. Review console output for any errors
4. Check generated files

### Review Phase
1. Open `procedure-review-tool.html` in browser
2. Load `procedure-transaction-analysis.csv`
3. For each stored procedure:
   - Review detected pattern (is it correct?)
   - Review SQL operations (are they complete?)
   - Review call hierarchy (is it traced?)
   - Add corrections if pattern detection is wrong
   - Add notes for future reference
   - Mark as reviewed when done
   - Flag for attention if needs work

### Save/Load Progress
- **Save Progress** (📥 button): Saves your current edits, notes, review status, and position to a JSON file
- **Load Progress** (📤 button): Restores a previously saved session
- Progress files include:
  - All edits (DeveloperCorrection, Notes, NeedsAttention flags)
  - Review status for each procedure
  - Current position and filters
  - Timestamp of when saved

**Workflow:**
1. Review procedures and make edits
2. Click "📥 Save Progress" to save current state
3. Continue later by clicking "📤 Load Progress"
4. Export final CSV when all reviews complete

### Correction Phase
1. Export corrected CSV from HTML tool
2. Address flagged procedures
3. Re-run analysis if code changes made
4. Repeat until all procedures validated

## 🎯 Use Cases

### Before Refactoring
- Understand what each stored procedure does
- Identify complex procedures needing special attention
- Map UI → Database call chains
- Document current behavior

### During Development
- Validate new stored procedures follow patterns
- Ensure transaction handling is present
- Verify validation logic exists
- Document SQL operations

### Code Review
- Quickly see what a procedure does
- Verify pattern compliance
- Check for missing transaction handling
- Review call hierarchy

### Testing
- Identify which UI paths call each procedure
- Know what tables are affected
- Understand success/failure flows
- Plan test scenarios

## 🔍 Troubleshooting

### "SPSettings.json not found"
- Ensure SPSettings.json exists in StoredProcedureValidation folder
- Check file path is correct

### "No stored procedures found"
- Check `StoredProceduresFolder` path in SPSettings.json
- Ensure path is relative to StoredProcedureValidation folder
- Enable `RecursiveSearch` if SPs are in subdirectories

### "No C# files found"
- Check `CSharpCodeFolder` path in SPSettings.json
- Verify `ExcludeFolders` isn't excluding needed folders
- Ensure C# files aren't in test folders (automatically excluded)

### "Call hierarchy shows 'Legacy'"
- Procedure may not be called from current codebase
- Check if procedure is called from external applications
- Verify DAO file patterns in SPSettings.json

### "SQL operations not detailed"
- Check stored procedure file encoding (should be UTF-8)
- Verify SQL syntax is parseable
- Review console output for parsing errors

## 📦 Portability

This tool is designed to work with **any project**:

1. **Copy the entire `StoredProcedureValidation` folder** to new project
2. **Edit `SPSettings.json`** with new project details
3. **Run `RUN-COMPLETE-ANALYSIS.ps1`**
4. **Review in browser** with `procedure-review-tool.html`

No code changes needed - everything is configured through SPSettings.json!

## 📄 License

Part of the MTM_WIP_Application_WinForms project.

## 🤝 Contributing

To improve this tool:
1. Add new analysis patterns to detection scripts
2. Enhance SQL parsing for edge cases
3. Improve call hierarchy tracing depth
4. Add new filtering/sorting options to HTML viewer
5. Expand validation rules in SPSettings.json
