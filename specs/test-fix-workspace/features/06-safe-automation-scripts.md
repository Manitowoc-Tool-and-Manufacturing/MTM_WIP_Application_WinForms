# Feature 6: Safe Automation Scripts

**Created**: 2025-10-19  
**Purpose**: Provide PowerShell automation scripts for workspace management with comprehensive safety features, dry-run mode, and backup capabilities

---

## Feature Overview

Create a collection of PowerShell scripts that automate repetitive workspace tasks while prioritizing safety. Each script includes dry-run mode, input validation, backup creation before changes, and clear success/failure reporting. Scripts handle progress updates, dashboard regeneration, test execution by category, and validation checks.

---

## Current Situation

**Manual Tasks That Take Time**:
- Updating checkboxes in category files after test fixes (2+ minutes)
- Regenerating dashboard from category files (3+ minutes manual calculation)
- Running specific test categories (remembering filter syntax)
- Validating workspace structure after changes

**Risks of Manual Updates**:
- Checkbox count mistakes (manual math errors)
- Forgetting to update dashboard after category changes
- Inconsistent formatting across files
- Breaking markdown syntax accidentally

**Opportunity**: Automate safe, repetitive tasks while keeping humans in control.

---

## User Needs

### Primary Users

**Developers fixing tests**: Need fast, safe automation for:
- Marking tests complete and updating metrics
- Regenerating dashboard after progress changes
- Running test categories without remembering filter syntax
- Validating workspace structure

**Tech Leads ensuring safety**: Need confidence that scripts:
- Won't corrupt workspace files
- Create backups before changes
- Validate inputs before execution
- Report clear success/failure
- Support dry-run mode

---

## What Users Need to Accomplish

### For Progress Updates

1. **Mark test complete quickly**: Run script, provide test name, checkboxes update
2. **Recalculate percentages**: Script updates completion counts automatically
3. **Regenerate dashboard**: Dashboard reflects category file changes
4. **Validate changes**: Script confirms changes are correct before applying

### For Test Execution

1. **Run category tests**: Run script with category name, correct filter applied
2. **Run single test**: Script handles full filter syntax
3. **See results clearly**: Script outputs pass/fail status prominently

### For Validation

1. **Check workspace structure**: Validate all expected files and folders exist
2. **Check links**: Verify no broken links in markdown files
3. **Check formatting**: Ensure consistent markdown formatting
4. **Check math**: Verify completion percentages calculate correctly

---

## Success Outcomes

### For Safety

- Zero file corruption from script execution
- All changes backed up before modification
- Dry-run mode shows exactly what will change
- Scripts validate inputs before executing
- Clear error messages when validation fails

### For Efficiency

- Time to update progress: < 30 seconds (baseline: 2+ minutes)
- Time to regenerate dashboard: < 10 seconds (baseline: 3+ minutes)
- Time to run category tests: < 20 seconds (baseline: 1+ minute)
- Zero manual calculation errors (script calculates correctly)

### For Usability

- Scripts run with minimal parameters (smart defaults)
- Help documentation built into scripts (Get-Help works)
- Clear success/failure messages
- No need to remember syntax (intuitive parameters)

---

## Scripts to Create

### 1. Update-Progress.ps1

**Purpose**: Mark tests complete in category files and update metrics

```powershell
<#
.SYNOPSIS
    Updates test completion status in category files.

.DESCRIPTION
    Marks specified tests as complete by changing [ ] to [x] in category files.
    Updates category completion counts and percentages.
    Backs up category file before modification.
    Supports dry-run mode to preview changes.

.PARAMETER TestName
    Name of test method to mark complete (e.g., "AddQuickButton_ValidData")

.PARAMETER Category
    Category file to update (1-4 or name: "QuickButtons", "SystemDAO", "Helper", "Phase1")

.PARAMETER WhatIf
    Shows what would change without modifying files

.EXAMPLE
    .\Update-Progress.ps1 -TestName "AddQuickButton_ValidData" -Category 1
    Marks test complete in Category 1 file

.EXAMPLE
    .\Update-Progress.ps1 -TestName "AddQuickButton_ValidData" -Category "QuickButtons" -WhatIf
    Shows what would change without modifying file
#>

[CmdletBinding(SupportsShouldProcess)]
param(
    [Parameter(Mandatory=$true)]
    [string]$TestName,
    
    [Parameter(Mandatory=$true)]
    [ValidateSet("1", "2", "3", "4", "QuickButtons", "SystemDAO", "Helper", "Phase1")]
    [string]$Category,
    
    [switch]$WhatIf
)

# Implementation:
# 1. Map category name/number to file path
# 2. Validate file exists and contains test name
# 3. Create backup with timestamp
# 4. Update checkbox for test: [ ] -> [x]
# 5. Recalculate completion count and percentage
# 6. Update category header with new metrics
# 7. If -WhatIf, show changes without applying
# 8. Return success/failure with clear message
```

**Key Features**:
- Validates test name exists in category file
- Creates timestamped backup: `01-quick-buttons.md.backup.20251019-143000`
- Supports both category numbers (1-4) and names
- Updates both checkbox and header metrics
- Dry-run shows exact changes
- Returns structured result for scripting

### 2. New-Dashboard.ps1

**Purpose**: Regenerate dashboard from category files

```powershell
<#
.SYNOPSIS
    Regenerates DASHBOARD.md from category files.

.DESCRIPTION
    Scans all category files in categories/ folder.
    Counts checked vs unchecked boxes for each category.
    Calculates completion percentages.
    Generates new DASHBOARD.md with current metrics.
    Preserves manual sections (Recent Activity, Blockers).
    Creates backup of existing dashboard.

.PARAMETER Force
    Regenerate even if dashboard appears current

.PARAMETER WhatIf
    Shows dashboard content without writing file

.EXAMPLE
    .\New-Dashboard.ps1
    Regenerates dashboard from category files

.EXAMPLE
    .\New-Dashboard.ps1 -WhatIf
    Shows new dashboard content without writing
#>

[CmdletBinding(SupportsShouldProcess)]
param(
    [switch]$Force,
    [switch]$WhatIf
)

# Implementation:
# 1. Scan categories/*.md files
# 2. Parse checkboxes: [ ] vs [x]
# 3. Extract category metadata (priority, effort, status)
# 4. Calculate overall metrics
# 5. Read existing DASHBOARD.md to preserve manual sections
# 6. Generate new dashboard content
# 7. Create backup of existing dashboard
# 8. Write new DASHBOARD.md (unless -WhatIf)
# 9. Validate generated dashboard (no broken links)
# 10. Return success with metrics summary
```

**Key Features**:
- Reads ALL category files automatically
- Preserves manually written sections
- Validates all links to category files
- Creates backup before overwriting
- Shows summary of changes
- Detects if dashboard is already current

### 3. Invoke-CategoryTests.ps1

**Purpose**: Run specific test category with correct filter syntax

```powershell
<#
.SYNOPSIS
    Runs integration tests for a specific category.

.DESCRIPTION
    Executes dotnet test with correct filter for specified category.
    Handles filter syntax complexity automatically.
    Displays results with clear pass/fail status.
    Optionally updates progress for passing tests.

.PARAMETER Category
    Category to test (1-4 or name: "QuickButtons", "SystemDAO", "Helper", "Phase1")

.PARAMETER Detailed
    Show detailed test output (default: summary only)

.PARAMETER UpdateProgress
    Automatically mark passing tests complete in category file

.EXAMPLE
    .\Invoke-CategoryTests.ps1 -Category 1
    Runs all tests in Category 1 (Quick Buttons)

.EXAMPLE
    .\Invoke-CategoryTests.ps1 -Category "QuickButtons" -Detailed
    Runs Category 1 tests with detailed output

.EXAMPLE
    .\Invoke-CategoryTests.ps1 -Category 2 -UpdateProgress
    Runs Category 2 tests and marks passing ones complete
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("1", "2", "3", "4", "QuickButtons", "SystemDAO", "Helper", "Phase1")]
    [string]$Category,
    
    [switch]$Detailed,
    [switch]$UpdateProgress
)

# Implementation:
# 1. Map category to test filter syntax
#    Category 1: --filter "FullyQualifiedName~Dao_QuickButtons_Tests"
#    Category 2: --filter "FullyQualifiedName~Dao_System_Tests"
#    Category 3: --filter "FullyQualifiedName~Helper_Tests|FullyQualifiedName~Validation_Tests"
#    Category 4: (specific test names from investigation)
# 2. Execute dotnet test with filter
# 3. Capture test results
# 4. Parse pass/fail status for each test
# 5. If -UpdateProgress, call Update-Progress.ps1 for passing tests
# 6. Display summary with color coding
# 7. Return structured results
```

**Key Features**:
- Handles complex filter syntax automatically
- Maps category names to correct test classes
- Shows clear pass/fail summary
- Optionally auto-updates progress
- Color-coded output (green/red)
- Returns results for scripting

### 4. Test-WorkspaceStructure.ps1

**Purpose**: Validate workspace structure and content

```powershell
<#
.SYNOPSIS
    Validates test fix workspace structure and content.

.DESCRIPTION
    Checks that all expected files and folders exist.
    Validates markdown links in all workspace files.
    Verifies completion percentage calculations are correct.
    Checks for consistent formatting.
    Reports any issues found.

.PARAMETER Fix
    Attempt to fix issues automatically (creates backups)

.EXAMPLE
    .\Test-WorkspaceStructure.ps1
    Validates workspace and reports issues

.EXAMPLE
    .\Test-WorkspaceStructure.ps1 -Fix
    Validates and attempts to fix issues
#>

[CmdletBinding(SupportsShouldProcess)]
param(
    [switch]$Fix
)

# Implementation:
# 1. Check folder structure (categories/, reference/, tools/, history/)
# 2. Check required files (TOC.md, DASHBOARD.md, category files)
# 3. Validate markdown links in all files
# 4. Check completion percentage math in category files
# 5. Verify checkbox format consistency
# 6. Check for broken cross-references
# 7. If -Fix, attempt to repair issues (with backups)
# 8. Return validation report with issues found
```

**Key Features**:
- Comprehensive structure validation
- Link checking (no broken links)
- Math validation (percentages correct)
- Format consistency checks
- Optional auto-fix mode
- Detailed issue reporting

### 5. Backup-WorkspaceFile.ps1

**Purpose**: Create timestamped backup of workspace file (utility function)

```powershell
<#
.SYNOPSIS
    Creates timestamped backup of a workspace file.

.DESCRIPTION
    Creates backup with timestamp in filename.
    Validates source file exists before backup.
    Places backup in same directory as source.
    Returns backup file path.

.PARAMETER FilePath
    Path to file to backup

.EXAMPLE
    .\Backup-WorkspaceFile.ps1 -FilePath "categories/01-quick-buttons.md"
    Creates: categories/01-quick-buttons.md.backup.20251019-143522
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$FilePath
)

# Implementation:
# 1. Validate source file exists
# 2. Generate timestamp: yyyyMMdd-HHmmss
# 3. Create backup path: $FilePath.backup.$timestamp
# 4. Copy file to backup location
# 5. Return backup path
```

**Key Features**:
- Timestamped backups (sortable)
- Validation before backup
- Returns backup path for logging
- Simple utility for other scripts

---

## Script Safety Features

### Mandatory Safety Checks

Every script must include:

1. **Input Validation**:
   - Validate all parameters before execution
   - Check files exist before reading/writing
   - Verify paths are within workspace
   - Reject invalid or malicious inputs

2. **Backup Before Changes**:
   - Create timestamped backup before modifying any file
   - Backup location in same directory as source
   - Never modify without backup

3. **Dry-Run Mode**:
   - Support `-WhatIf` parameter
   - Show exactly what will change
   - No modifications in dry-run mode
   - Preview changes before committing

4. **Corruption Detection**:
   - Validate file format before/after changes
   - Check markdown syntax is valid
   - Verify no data loss (line counts similar)
   - Rollback on corruption detection

5. **Clear Error Handling**:
   - Try/catch around risky operations
   - Clear error messages (no stack traces to user)
   - Return structured result (success/failure)
   - Log errors for troubleshooting

### Rollback Capability

If script detects issues after changes:
```powershell
# Automatic rollback pattern
try {
    $backup = Backup-WorkspaceFile -FilePath $targetFile
    # Make changes
    # Validate changes
    if (Test-FileCorrupted $targetFile) {
        throw "Corruption detected, rolling back"
    }
} catch {
    # Restore from backup
    Copy-Item $backup $targetFile -Force
    Write-Error "Changes rolled back due to: $($_.Exception.Message)"
}
```

### Help Documentation

Every script must have:
- Synopsis (one-line description)
- Full description
- Parameter descriptions with examples
- Multiple usage examples
- Notes about safety features

Accessible via:
```powershell
Get-Help .\Update-Progress.ps1 -Full
Get-Help .\Update-Progress.ps1 -Examples
```

---

## Script Output Format

### Structured Results

Scripts return PowerShell objects for scriptability:

```powershell
[PSCustomObject]@{
    Success = $true
    Message = "Test marked complete in Category 1"
    TestName = "AddQuickButton_ValidData"
    CategoryFile = "categories/01-quick-buttons.md"
    BackupFile = "categories/01-quick-buttons.md.backup.20251019-143522"
    OldCompletion = "1/10 (10%)"
    NewCompletion = "2/10 (20%)"
    ChangesApplied = $true
}
```

### Console Output

Clear, formatted output for humans:

```
✅ Success: Test marked complete

Test: AddQuickButton_ValidData
Category: 1 - Quick Buttons
File: categories/01-quick-buttons.md
Backup: categories/01-quick-buttons.md.backup.20251019-143522

Progress: 1/10 (10%) -> 2/10 (20%)

Next: Run .\New-Dashboard.ps1 to update dashboard
```

Use:
- ✅ for success
- ❌ for errors
- ⚠️ for warnings
- ℹ️ for information

---

## Special Requirements

### No External Dependencies

Scripts use only:
- Built-in PowerShell cmdlets
- .NET classes available in PowerShell 7+
- No third-party modules

Ensures scripts work without setup.

### Cross-Platform Consideration

While primarily for Windows:
- Use `[System.IO.Path]::Combine()` for paths
- Avoid Windows-specific commands where possible
- Test on PowerShell 7+ (cross-platform)

### Performance

Scripts should be fast:
- Target: < 2 seconds for progress updates
- Target: < 5 seconds for dashboard generation
- Target: < 1 second for validation
- Use efficient file reading (stream vs load entire file)

### Logging

Optional verbose logging:
```powershell
# Use built-in verbose support
Write-Verbose "Scanning category files..."

# Run with verbose output
.\Update-Progress.ps1 -Verbose
```

---

## Out of Scope

This feature does NOT include:
- Scripts that modify test code
- Scripts that run builds or deployments
- Scripts that modify database
- Scripts that commit to git automatically
- Scripts that send notifications
- Real-time automation (all manual invocation)

Only provides **safe workspace management scripts**.

---

## Dependencies

**Depends on**: 
- Feature 1 (Workspace Foundation) - needs folder structure
- Feature 2 (Test Category Tracking) - modifies category files
- Feature 5 (Progress Dashboard) - generates dashboard

**Depended on by**: None (scripts are optional automation)

---

## Assumptions

- PowerShell 7+ available on developer machines
- Scripts run from workspace root (specs/test-fix-workspace/)
- Workspace structure follows Feature 1 specification
- Category files follow Feature 2 format
- Developers have write permissions to workspace files

---

## Acceptance Criteria

### Scripts Created
- [ ] Update-Progress.ps1 exists and works
- [ ] New-Dashboard.ps1 exists and works
- [ ] Invoke-CategoryTests.ps1 exists and works
- [ ] Test-WorkspaceStructure.ps1 exists and works
- [ ] Backup-WorkspaceFile.ps1 exists and works

### Safety Features
- [ ] All scripts support -WhatIf parameter
- [ ] All scripts create backups before changes
- [ ] All scripts validate inputs
- [ ] All scripts detect corruption
- [ ] All scripts have rollback capability

### Help Documentation
- [ ] Every script has complete help
- [ ] Get-Help works for all scripts
- [ ] Examples provided for common scenarios
- [ ] Parameter descriptions clear

### Usability
- [ ] Scripts run with minimal parameters
- [ ] Clear success/failure messages
- [ ] Structured results for scripting
- [ ] Formatted console output for humans
- [ ] No external dependencies

### Testing
- [ ] Scripts tested with valid inputs
- [ ] Scripts tested with invalid inputs
- [ ] WhatIf mode tested (no changes)
- [ ] Rollback tested (corruption scenario)
- [ ] Performance targets met

---

## Success Metrics

**Efficiency Gains**:
- Time to update progress: < 30 seconds (baseline: 2+ minutes)
- Time to regenerate dashboard: < 10 seconds (baseline: 3+ minutes)
- Time to run category tests: < 20 seconds (baseline: 1+ minute)

**Safety**:
- File corruption incidents: 0
- Successful rollbacks: 100% (when corruption detected)
- Manual calculation errors: 0 (scripts calculate correctly)

**Usability**:
- Scripts run successfully on first try: 95%+
- Help documentation rated helpful: 90%+
- Time to learn script usage: < 5 minutes per script

---

## Notes for /speckit.specify

This feature provides **safe automation scripts** for workspace management. All scripts include comprehensive safety features (backups, validation, rollback).

**No clarifications needed** - script requirements are well-defined with clear safety boundaries.

Can be implemented after Features 1, 2, and 5 since scripts operate on those features' outputs.

Scope is deliberately conservative (workspace management only, no test code changes, no database operations) to maximize safety.
