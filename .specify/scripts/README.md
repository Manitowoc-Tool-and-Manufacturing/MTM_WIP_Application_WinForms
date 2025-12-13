# Constitution Compliance Validation

## Overview

This directory contains automated validation tools to ensure codebase compliance with the [MTM WIP Application Constitution](../../memory/constitution.md).

## Available Scripts

### `validate-constitution-compliance.ps1`

PowerShell script that scans C# files for constitution violations.

**Checks Performed:**
1. ✅ **MessageBox.Show Usage** - FORBIDDEN (must use Service_ErrorHandler)
2. ✅ **Direct Database Connections** - FORBIDDEN except approved exceptions (must use Helper_Database_StoredProcedure)
3. ⚠️ **Blocking Async Calls** - WARNING (.Result, .Wait(), .GetAwaiter().GetResult() detected)
4. ⚠️ **Missing Using Statements** - WARNING (connections without using statements)

**Approved Architectural Exceptions:**
- `Service_OnStartup_Database.cs` - Parameter cache initialization
- `Helper_Control_MySqlSignal.cs` - Network diagnostics

**Usage:**

```powershell
# Scan entire repository
.\.specify\scripts\powershell\validate-constitution-compliance.ps1

# Scan specific directory
.\.specify\scripts\powershell\validate-constitution-compliance.ps1 -Path "Services"

# Fail on violations (for CI/CD)
.\.specify\scripts\powershell\validate-constitution-compliance.ps1 -FailOnViolations

# Exclude test files
.\.specify\scripts\powershell\validate-constitution-compliance.ps1 -ExcludeTests
```

**Exit Codes:**
- `0` - All checks passed
- `1` - Violations found (only with `-FailOnViolations` flag)

**Example Output:**

```
==================================================================
MTM WIP Application - Constitution Compliance Validation
==================================================================

Scanning: C:\path\to\repo
Found 250 C# files to scan

[1/4] Checking for MessageBox.Show usage...
  ✅ PASS: No MessageBox.Show usage found

[2/4] Checking for direct database connection instantiation...
  ⚠️  SKIP: Service_OnStartup_Database.cs (approved architectural exception)
  ⚠️  SKIP: Helper_Control_MySqlSignal.cs (approved architectural exception)
  ❌ VIOLATION: Found 3 direct connection instantiation(s)

[3/4] Checking for blocking async calls...
  ✅ PASS: No blocking async calls found

[4/4] Checking for connections without using statements...
  ✅ PASS: All connections appear to use using statements

==================================================================
COMPLIANCE SUMMARY
==================================================================

❌ VIOLATIONS FOUND: 3 total

Services\Analytics\Service_Analytics.cs (3 violation(s))
  Line 59: await using (var connection = new MySqlConnection(...))
  Line 174: await using (var connection = new MySqlConnection(...))
  Line 232: await using (var connection = new MySqlConnection(...))

ACTION REQUIRED:
  1. MessageBox.Show → Use Service_ErrorHandler.ShowUserError()
  2. Direct connections → Use Helper_Database_StoredProcedure methods
  3. Document any legitimate exceptions with comments
```

## Integration with CI/CD

Add to GitHub Actions workflow:

```yaml
- name: Validate Constitution Compliance
  run: |
    .\.specify\scripts\powershell\validate-constitution-compliance.ps1 -FailOnViolations
  shell: powershell
```

## Integration with Pre-commit

Add to `.git/hooks/pre-commit`:

```bash
#!/bin/sh
pwsh .specify/scripts/powershell/validate-constitution-compliance.ps1 -FailOnViolations
```

## Manual Review Items

The following constitution principles require manual code review (not automated):

- **Model_Dao_Result Pattern** - Verify DAO methods return proper type
- **Stored Procedures Only** - Verify no inline SQL (except diagnostic)
- **XML Documentation** - Verify public members have docs
- **Region Organization** - Verify standard #region structure
- **MySQL 5.7.24 Compatibility** - Verify no 8.0+ features in stored procedures
- **.NET 8.0/C# 12.0** - Verify no 9.0+/13.0+ features

These items are included in the [PR Checklist](../../../.github/PULL_REQUEST_TEMPLATE.md).

## Constitution Version

This validation script corresponds to **Constitution v1.0.0** (ratified 2025-12-13).

For full constitution details, see: `.specify/memory/constitution.md`
