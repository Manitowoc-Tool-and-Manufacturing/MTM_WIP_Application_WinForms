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
