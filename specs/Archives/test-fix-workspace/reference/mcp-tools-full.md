# MCP Tools - Full Documentation

**Purpose**: Comprehensive documentation for all mtm-workflow MCP tools  
**Last Updated**: 2025-10-19  
**Quick Reference**: [mcp-tools-quick.md](mcp-tools-quick.md)  
**Server**: mtm-workflow

---

## Table of Contents

- [Database Analysis Tools](#database-analysis-tools)
- [DAO & Code Analysis Tools](#dao--code-analysis-tools)
- [Testing Support Tools](#testing-support-tools)
- [Common Workflows](#common-workflows)

---

## Database Analysis Tools

### analyze_stored_procedures

**Purpose**: Scan SQL stored procedure files for compliance with MTM standards (p_Status, p_ErrorMsg outputs, transaction handling, parameter naming).

**When to use**: Before committing stored procedure changes, during code reviews, when investigating database layer issues.

**Parameters**:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| procedures_dir | string | Yes | Absolute path to directory containing SQL procedure files |
| recursive | boolean | No | Whether to search subdirectories (default: true) |

**Output**: Compliance report with issues found per procedure.

**Example Usage**:

```typescript
analyze_stored_procedures({
  procedures_dir: "c:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms\\Database\\UpdatedStoredProcedures\\ReadyForVerification",
  recursive: true
})
```

**Expected Output**:
```
Analyzing 74 stored procedures...
✅ sp_QuickButtons_Add.sql - COMPLIANT
❌ sp_OldProcedure.sql - MISSING p_Status output parameter
⚠️  sp_Another.sql - WARNING: No transaction management detected

Summary:
- Total: 74 procedures
- Compliant: 72
- Issues: 2
```

**Common Workflows**:
1. Run before committing SP changes
2. Use with analyze_dependencies to understand SP relationships
3. Pair with install_stored_procedures to apply fixes

**Troubleshooting**:
- **Issue**: Tool reports false positives for p_Status  
  **Solution**: Ensure OUT parameters are defined correctly: `OUT p_Status INT`

---

### generate_test_seed_sql

**Purpose**: Generate SQL seed scripts from JSON configuration for repeatable test data setup.

**When to use**: Setting up test users, quick buttons, or other test data needed for integration tests (Categories 1 & 2).

**Parameters**:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| config_file | string | No | Absolute path to JSON configuration file with seed rules |
| database | string | No | Target database name (default: mtm_wip_application_winforms_test) |
| include_logging | boolean | No | Include default log seed rows |
| include_history | boolean | No | Include default transaction history rows |
| user | string | No | Base user identifier for default quick button seeds |
| output_sql | string | No | Optional: path to write generated SQL script |

**Output**: SQL script with INSERT statements using ON DUPLICATE KEY UPDATE.

**Example Usage**:

```typescript
generate_test_seed_sql({
  config_file: "c:\\.mcp\\samples\\test-seed-quickbuttons.json",
  database: "mtm_wip_application_winforms_test",
  output_sql: "c:\\temp\\seed-test-data.sql"
})
```

**Config File Format**:
```json
{
  "users": [
    {"UserID": "TEST-USER", "UserName": "Test User", "IsActive": 1, "IsAdmin": 0},
    {"UserID": "TEST-ADMIN", "UserName": "Test Admin", "IsActive": 1, "IsAdmin": 1}
  ],
  "quick_buttons": [
    {"UserID": "TEST-USER", "ButtonText": "Test Button", "Operation": "100", "Location": "FLOOR", "Position": 1}
  ]
}
```

**Common Workflows**:
1. Generate seed SQL from JSON config
2. Apply SQL to test database
3. Use verify_test_seed to confirm data

---

### verify_test_seed

**Purpose**: Validate seeded data against expected results using MySQL connection.

**When to use**: After running seed SQL, before integration tests, to verify test data setup worked correctly.

**Parameters**:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| config_file | string | Yes | Absolute path to JSON configuration used for seeding |
| database | string | No | MySQL database to validate (default from config) |
| host | string | No | MySQL host (default: localhost) |
| port | number | No | MySQL port (default: 3306) |
| user | string | No | MySQL user (default: root) |
| password | string | No | MySQL password (default: root) |

**Output**: Validation report showing expected vs actual record counts.

**Example Usage**:

```typescript
verify_test_seed({
  config_file: "c:\\.mcp\\samples\\test-seed-quickbuttons.json",
  database: "mtm_wip_application_winforms_test"
})
```

**Expected Output**:
```
Verifying test seed data...
✅ usr_users: Expected 2, Found 2
✅ sys_quick_buttons: Expected 1, Found 1

All seed data verified successfully.
```

---

## DAO & Code Analysis Tools

### validate_dao_patterns

**Purpose**: Check DAO files for compliance with MTM patterns (region organization, async/await, Helper_Database_StoredProcedure usage, Service_ErrorHandler, XML docs).

**When to use**: After creating/modifying DAOs, before committing, during code reviews.

**Parameters**:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| dao_dir | string | Yes | Absolute path to directory containing DAO C# files |
| recursive | boolean | No | Search subdirectories (default: true) |

**Output**: Validation results with specific issues found per DAO file.

**Example Usage**:

```typescript
validate_dao_patterns({
  dao_dir: "c:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms\\Data",
  recursive: true
})
```

**Expected Output**:
```
Validating DAO patterns...
✅ Dao_QuickButtons.cs - COMPLIANT
❌ Dao_Legacy.cs - MISSING proper region organization
⚠️  Dao_Another.cs - WARNING: MessageBox.Show detected (use Service_ErrorHandler)

Summary:
- Total: 12 DAOs
- Compliant: 10
- Issues: 2
```

**Common Workflows**:
1. Run after DAO modifications
2. Combine with validate_error_handling
3. Pair with check_xml_docs for documentation

---

### validate_error_handling

**Purpose**: Check C# source files for proper error handling patterns (Service_ErrorHandler usage, no MessageBox.Show anti-pattern).

**When to use**: After modifying Forms or DAOs, before committing, when refactoring error handling.

**Parameters**:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| source_dir | string | Yes | Absolute path to source code directory |
| recursive | boolean | No | Search subdirectories (default: true) |

**Output**: Report of MessageBox.Show usage and Service_ErrorHandler compliance.

**Example Usage**:

```typescript
validate_error_handling({
  source_dir: "c:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms\\Data",
  recursive: true
})
```

**Expected Output**:
```
Checking error handling patterns...
✅ No MessageBox.Show found
✅ Service_ErrorHandler usage: 45 instances

All error handling follows MTM patterns.
```

---

### check_xml_docs

**Purpose**: Validate XML documentation coverage for C# code (public classes, methods, properties).

**When to use**: Before committing, ensuring documentation standards met, during code reviews.

**Parameters**:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| source_dir | string | Yes | Absolute path to source code directory |
| min_coverage | number | No | Minimum required coverage percentage (default: 80) |
| recursive | boolean | No | Search subdirectories (default: true) |

**Output**: Coverage percentage and list of undocumented members.

**Example Usage**:

```typescript
check_xml_docs({
  source_dir: "c:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms\\Data",
  min_coverage: 80,
  recursive: true
})
```

**Expected Output**:
```
Checking XML documentation...
Coverage: 92% (46/50 public members documented)

Undocumented members:
- Dao_QuickButtons.cs:45 - Method GetNextPositionAsync
- Dao_QuickButtons.cs:78 - Property DefaultLocation

Coverage exceeds minimum (80%). PASS
```

---

## Testing Support Tools

### generate_unit_tests

**Purpose**: Auto-generate test scaffolding for C# classes (xUnit, NUnit, or MSTest frameworks).

**When to use**: Creating new test classes, scaffolding tests for DAOs or helpers.

**Parameters**:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| source_file | string | Yes | Absolute path to C# source file to generate tests for |
| output_dir | string | No | Directory to write generated test file |
| test_framework | string | No | Test framework: xunit, nunit, or mstest (default: xunit) |

**Output**: Generated test class file with method stubs.

**Example Usage**:

```typescript
generate_unit_tests({
  source_file: "c:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms\\Data\\Dao_QuickButtons.cs",
  output_dir: "c:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms\\Tests\\Integration",
  test_framework: "xunit"
})
```

**Expected Output**: Creates `Dao_QuickButtons_Tests.cs` with scaffolded test methods.

---

### validate_build

**Purpose**: Run dotnet build and validate compilation, check for errors/warnings.

**When to use**: After code changes, before committing, as pre-test validation.

**Parameters**:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| workspace_root | string | Yes | Absolute path to workspace root |
| project_file | string | No | Optional: path to .csproj file (auto-detected if omitted) |
| check_errors | boolean | No | Scan for compilation errors (default: true) |
| run_tests | boolean | No | Run tests after build (default: false) |

**Output**: Build results with error/warning counts.

**Example Usage**:

```typescript
validate_build({
  workspace_root: "c:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms",
  check_errors: true,
  run_tests: false
})
```

**Expected Output**:
```
Running dotnet build...
Build succeeded.
Errors: 0
Warnings: 3

Warnings:
- CS1591: Missing XML comment for publicly visible type 'Dao_Test'

Build validation: PASS (no errors)
```

---

## Common Workflows

### Workflow 1: Validating DAO Changes

**Purpose**: Ensure DAO changes meet all MTM standards before committing.

**Steps**:
```
1. validate_dao_patterns(dao_dir: "Data/", recursive: true)
2. validate_error_handling(source_dir: "Data/", recursive: true)
3. check_xml_docs(source_dir: "Data/", min_coverage: 80)
4. analyze_performance(source_dir: "Data/", focus: "database")
```

**Success Criteria**: All tools report PASS with no critical issues.

---

### Workflow 2: Preparing for Integration Test Run

**Purpose**: Set up test environment and validate before running tests.

**Steps**:
```
1. validate_schema(config_file: ".mcp/samples/schema-validation-config.json")
2. generate_test_seed_sql(config_file: ".mcp/samples/test-seed-config.json")
3. verify_test_seed(config_file: ".mcp/samples/test-seed-config.json")
4. run_integration_harness(config_file: ".mcp/samples/integration-harness-config.json")
5. audit_database_cleanup(config_file: ".mcp/samples/cleanup-config.json")
```

**Success Criteria**: Clean test run with no environmental issues.

---

### Workflow 3: Fixing Stored Procedure Issues

**Purpose**: Update stored procedures and ensure compliance.

**Steps**:
```
1. analyze_stored_procedures(procedures_dir: "Database/UpdatedStoredProcedures/")
2. analyze_dependencies(procedures_dir: "Database/UpdatedStoredProcedures/")
3. install_stored_procedures(config_file: ".mcp/samples/install-procedures-config.json")
4. validate_schema(config_file: ".mcp/samples/schema-validation-config.json")
5. generate_dao_wrapper(procedure_file: "path/to/updated/procedure.sql")
```

**Success Criteria**: All procedures compliant, DAOs updated, tests passing.

---

**Tool Count**: 26 tools documented (focused on most critical for test fixing)  
**Server Version**: mtm-workflow v1.0  
**Maintained by**: Development Team

**Note**: Full documentation for all 26 tools available. This guide focuses on tools most relevant for current test fixing work. See [mcp-tools-quick.md](mcp-tools-quick.md) for complete tool list.
