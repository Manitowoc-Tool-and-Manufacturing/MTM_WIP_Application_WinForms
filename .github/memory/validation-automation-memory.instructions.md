---
description: 'Automated validation patterns, structured reporting, and quality gate workflows for MTM database procedures'
applyTo: 'Database/Tools/**/*.ps1,Database/Checklists/**/*.md'
---

# Validation Automation Memory

Patterns for building reliable validation automation that produces auditable results and integrates with quality gates.

## Validation Script Architecture

### Structured Run Output Pattern

Emit consistent run metadata for tracking and auditing:

```powershell
[pscustomobject]@{
    RunId                 = $runId                    # Timestamp-based identifier
    StartedAtUtc          = $startTime
    FinishedAtUtc         = $endTime
    Server                = $Server
    Database              = $Database
    ProcedureCount        = $procedureFiles.Count
    Passed                = @($results | Where-Object { $_.Status -eq 'Pass' }).Count
    Failed                = @($results | Where-Object { $_.Status -eq 'Fail' }).Count
    LogsDirectory         = $logsRoot
    ResultsCsv            = $resultsPath
}
```

**Pattern**: Return a single summary object capturing execution context, outcomes, and artifact locations.

### Per-Item Logging Strategy

Store detailed logs in timestamped subdirectories for troubleshooting:

```powershell
$runRoot = Join-Path $baseDir "ValidationRuns/$runId"
$logsRoot = Join-Path $runRoot "procedure-logs"
New-Item -ItemType Directory -Path $logsRoot -Force | Out-Null

foreach ($item in $items) {
    $logFile = Join-Path $logsRoot "$($item.SafeName).log"
    Set-Content -Path $logFile -Value "# Validation log for $($item.Name)"
    # ... append execution results
}
```

**Pattern**: Organize validation runs by timestamp with artifact subdirectories (logs, reports, schemas).

## Checklist Integration

### Programmatic Checklist Updates

Update markdown checklists based on validation results:

```powershell
function Update-Checklist {
    param($RelativePath, [bool]$Completed, $Note)
    
    $content = Get-Content -Path $checklistFile
    $pattern = "^- \[ \] ``$([regex]::Escape($RelativePath))``"
    
    $updated = $content -replace $pattern, "- [x] ``$RelativePath`` — NOTE: $Note"
    Set-Content -Path $checklistFile -Value $updated
}
```

**Pattern**: Use regex replacement to mark checklist items with completion status and timestamped notes.

### Status Propagation Workflow

Surface validation outcomes in task tracking:

1. **Validation script** marks checklist items `[x]` with notes
2. **Task file** references checklist completion as gate
3. **Pull request** shows checklist progress in diff

## MySQL Command Execution Patterns

### Echo-Based Validation

Construct MySQL commands with proper escaping and capture both output and exit codes:

```powershell
$mysqlCommand = 'USE `{0}`; SOURCE {1}; SHOW WARNINGS;' -f $Database, $escapedPath
$result = & mysql -u $User -p$Password --database=$Database -e $mysqlCommand 2>&1

if ($LASTEXITCODE -eq 0) {
    $status = 'Pass'
} else {
    $status = 'Fail'
}
```

**Pattern**: Use format operator for predictable command building, capture stderr with `2>&1`, check `$LASTEXITCODE`.

### Stored Procedure Definition Validation

Validate procedure loaded successfully and extract definition for contract checks:

```powershell
# Execute SOURCE to load procedure
$sourceCommand = 'USE `{0}`; SOURCE {1}; SHOW WARNINGS;' -f $Database, $escapedPath
$sourceResult = Invoke-MySqlCommand -Command $sourceCommand

# Retrieve definition to verify OUT parameters
if ($sourceResult.ExitCode -eq 0) {
    $showCreateCommand = 'USE `{0}`; SHOW CREATE PROCEDURE `{0}`.`{1}`\G' -f $Database, $procedureName
    $showCreateResult = Invoke-MySqlCommand -Command $showCreateCommand
    
    if ($showCreateResult.ExitCode -eq 0 -and $showCreateResult.Output) {
        $definition = $showCreateResult.Output -join "`n"
        $hasStatusOut = $definition -match '(?i)OUT\s+`?p_Status`?'
        $hasErrorOut = $definition -match '(?i)OUT\s+`?p_ErrorMsg`?'
    }
}
```

**Pattern**: Two-stage validation verifies both execution (SOURCE) and contract compliance (SHOW CREATE PROCEDURE).

### Procedure Name Extraction

Parse procedure names from SQL file content with delimiter handling:

```powershell
function Get-ProcedureName {
    param($SqlContent, $File)
    
    # Match CREATE PROCEDURE with optional DEFINER and delimiter changes
    if ($SqlContent -match '(?i)CREATE\s+(?:DEFINER\s*=\s*[^\s]+\s+)?PROCEDURE\s+`?([a-zA-Z0-9_]+)`?') {
        return $Matches[1]
    }
    
    # Fallback: derive from filename
    return [System.IO.Path]::GetFileNameWithoutExtension($File.Name)
}
```

**Pattern**: Regex extraction with fallback ensures procedure names are captured even with non-standard SQL formatting.

### Validation Coverage Matrix

Track validation completeness across dimensions:

```csv
Procedure,Domain,Status,SourceExitCode,HasStatusOut,HasErrorOut,LogPath
inv_inventory_Add_Item,inventory,Pass,0,True,True,Database/ValidationRuns/.../inv_inventory_Add_Item.log
```

**Pattern**: Export validation results to CSV for matrix analysis and automated reporting.

## Quality Gate Integration

### Blocking vs Warning Validations

Structure scripts to return non-zero exit codes for blocking failures:

```powershell
$criticalFailures = @($results | Where-Object { $_.Status -eq 'Fail' -and $_.Severity -eq 'Critical' })
if ($criticalFailures.Count -gt 0) {
    Write-Error "Critical validation failures detected"
    exit 1
}
```

**Pattern**: Distinguish critical failures (block pipeline) from warnings (log but continue).

### Validation Artifact Retention

Preserve validation runs for audit and troubleshooting:

- Keep last N runs (e.g., 10 most recent)
- Archive failed runs indefinitely
- Include connection logs, schema snapshots, and temp table checks
- Tag runs with branch/commit metadata for traceability

**Pattern**: Structured artifact directories enable compliance reporting and historical analysis.

## Integration Test Infrastructure Patterns

### Shared Test Data Setup Pattern

Create reusable test data helpers in `BaseIntegrationTest` that multiple test classes can inherit and use.

**Benefits:**
- Reduces code duplication across test classes
- Ensures consistent test data across suites
- Simplifies maintenance (update once, affects all tests)
- Enables test isolation and idempotency

**Implementation Pattern:**
```csharp
// In BaseIntegrationTest.cs
protected async Task CreateTestUsersAsync()
{
    // Idempotent: Check if data exists before creating
    var existingUsers = await Dao_User.GetUserByUsernameAsync("TEST-USER");
    if (existingUsers.IsSuccess && existingUsers.Data != null) return;
    
    // Create test users with known credentials
    await Dao_User.AddUserAsync("TEST-USER", "Active", "Test", "User", ...);
    await Dao_User.AddUserAsync("TEST-ADMIN", "Active", "Test", "Admin", ...);
}

protected async Task CleanupTestUsersAsync()
{
    // Delete in reverse order of foreign key dependencies
    await Dao_User.DeleteUserAsync("TEST-USER");
    await Dao_User.DeleteUserAsync("TEST-ADMIN");
}
```

**Key Principles:**
1. **Idempotency**: Safe to call multiple times without errors
2. **Predictable naming**: Use `TEST-` prefix for easy identification
3. **Foreign key awareness**: Clean up child records before parents
4. **Error resilience**: Don't fail if cleanup finds nothing to delete

### Test Documentation Update Workflow

Update progress tracking documents at key workflow checkpoints to maintain accurate project status.

**Update Triggers:**
- Start of work session (mark tasks in-progress)
- After completing implementation (mark tasks complete)
- Before requesting review (update progress metrics)
- End of work cycle (refresh all tracking docs)

**Documents to Update Together:**
```
specs/test-fix-workspace/
├── TOC.md              # Table of contents with overall progress
├── DASHBOARD.md        # Metrics and status summary
└── categories/
    └── 01-*.md         # Detailed category status
```

**Pattern**: Batch documentation updates at workflow checkpoints (not after every small change).

### Build-Verify-Document Cycle

Follow consistent cycle for reliable test development:

1. **Make changes** to test infrastructure or test methods
2. **Build immediately** to catch compilation errors early
   ```powershell
   dotnet build MTM_Inventory_Application.csproj -c Debug
   ```
3. **Verify zero warnings** in files you edited
4. **Run affected tests** to validate functionality
5. **Update documentation** (TOC, DASHBOARD, category files)
6. **Commit with clear message** referencing task IDs

**Anti-Pattern**: Making multiple changes before building leads to cascading errors that are harder to debug.

**Pattern**: Incremental validation catches issues early and maintains clean build state.
