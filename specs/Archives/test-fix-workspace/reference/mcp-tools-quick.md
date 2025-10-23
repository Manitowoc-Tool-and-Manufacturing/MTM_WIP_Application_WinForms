# MCP Tools - Quick Reference

**Purpose**: Fast lookup of tool names and capabilities  
**Last Updated**: 2025-10-19  
**Full Documentation**: [mcp-tools-full.md](mcp-tools-full.md)  
**Server**: mtm-workflow

---

## By Category

### Database Analysis (8 tools)

- **analyze_stored_procedures** - Scan SQL files for p_Status/p_ErrorMsg compliance
- **analyze_dependencies** - Map stored procedure call hierarchies and dependency graphs
- **compare_databases** - Detect schema drift between Current/Updated directories
- **validate_schema** - Compare live MySQL schema against database-schema-snapshot.json
- **install_stored_procedures** - Apply SP scripts from JSON config and report drift
- **generate_test_seed_sql** - Generate SQL seed scripts from JSON configuration
- **verify_test_seed** - Validate seeded data against expected results using MySQL
- **audit_database_cleanup** - Inspect and clear residual TEST-* rows for isolation

### DAO & Code Analysis (6 tools)

- **validate_dao_patterns** - Check DAO compliance (regions, async, XML docs, Helper usage)
- **validate_error_handling** - Check Service_ErrorHandler usage, find MessageBox.Show
- **check_security** - Scan for SQL injection, hardcoded credentials, CWE vulnerabilities
- **analyze_performance** - Identify N+1 queries, blocking async, UI thread issues
- **check_xml_docs** - Validate XML documentation coverage with min_coverage param
- **suggest_refactoring** - AI-powered refactoring suggestions with priority ranking

### UI Validation (3 tools)

- **validate_ui_scaling** - Check WinForms DPI scaling and Core_Themes.ApplyDpiScaling calls
- **generate_ui_fix_plan** - Create JSON fix plan from UI validation results
- **apply_ui_fixes** - Apply UI fixes with backups, corruption detection, and rollback

### Testing Support (4 tools)

- **generate_unit_tests** - Auto-generate xUnit/NUnit/MSTest scaffolding for C# classes
- **run_integration_harness** - Execute integration test harness with seed/teardown steps
- **check_checklists** - Analyze markdown checklist files for completion status
- **validate_build** - Run dotnet build and validate compilation with error counts

### Code Generation (3 tools)

- **generate_dao_wrapper** - Auto-generate C# DAO wrapper from stored procedure file
- **parse_tasks** - Extract structured task information from tasks.md files
- **mark_task_complete** - Mark tasks complete in tasks.md with timestamps
- **load_instructions** - Load and analyze instruction file references from tasks.md

### Project Management (2 tools)

- **verify_ignore_files** - Check .gitignore for essential patterns based on tech stack
- **analyze_spec_context** - Extract implementation context from spec directory structure

---

## Alphabetical Index

| Tool | Category | Link |
|------|----------|------|
| analyze_dependencies | Database | [Details](mcp-tools-full.md#analyze_dependencies) |
| analyze_performance | DAO/Code | [Details](mcp-tools-full.md#analyze_performance) |
| analyze_spec_context | Project Mgmt | [Details](mcp-tools-full.md#analyze_spec_context) |
| analyze_stored_procedures | Database | [Details](mcp-tools-full.md#analyze_stored_procedures) |
| apply_ui_fixes | UI | [Details](mcp-tools-full.md#apply_ui_fixes) |
| audit_database_cleanup | Database | [Details](mcp-tools-full.md#audit_database_cleanup) |
| check_checklists | Testing | [Details](mcp-tools-full.md#check_checklists) |
| check_security | DAO/Code | [Details](mcp-tools-full.md#check_security) |
| check_xml_docs | DAO/Code | [Details](mcp-tools-full.md#check_xml_docs) |
| compare_databases | Database | [Details](mcp-tools-full.md#compare_databases) |
| generate_dao_wrapper | Code Gen | [Details](mcp-tools-full.md#generate_dao_wrapper) |
| generate_test_seed_sql | Database | [Details](mcp-tools-full.md#generate_test_seed_sql) |
| generate_ui_fix_plan | UI | [Details](mcp-tools-full.md#generate_ui_fix_plan) |
| generate_unit_tests | Testing | [Details](mcp-tools-full.md#generate_unit_tests) |
| install_stored_procedures | Database | [Details](mcp-tools-full.md#install_stored_procedures) |
| load_instructions | Code Gen | [Details](mcp-tools-full.md#load_instructions) |
| mark_task_complete | Code Gen | [Details](mcp-tools-full.md#mark_task_complete) |
| parse_tasks | Code Gen | [Details](mcp-tools-full.md#parse_tasks) |
| run_integration_harness | Testing | [Details](mcp-tools-full.md#run_integration_harness) |
| suggest_refactoring | DAO/Code | [Details](mcp-tools-full.md#suggest_refactoring) |
| validate_build | Testing | [Details](mcp-tools-full.md#validate_build) |
| validate_dao_patterns | DAO/Code | [Details](mcp-tools-full.md#validate_dao_patterns) |
| validate_error_handling | DAO/Code | [Details](mcp-tools-full.md#validate_error_handling) |
| validate_schema | Database | [Details](mcp-tools-full.md#validate_schema) |
| validate_ui_scaling | UI | [Details](mcp-tools-full.md#validate_ui_scaling) |
| verify_ignore_files | Project Mgmt | [Details](mcp-tools-full.md#verify_ignore_files) |
| verify_test_seed | Database | [Details](mcp-tools-full.md#verify_test_seed) |

---

## Quick Usage Patterns

### Pattern: Validate DAO Changes

```
1. validate_dao_patterns(dao_dir: "Data/")
2. validate_error_handling(source_dir: "Data/")
3. check_xml_docs(source_dir: "Data/", min_coverage: 80)
4. analyze_performance(source_dir: "Data/", focus: "database")
```

### Pattern: Prepare for Integration Test Run

```
1. validate_schema(config_file: "path/to/schema-config.json")
2. generate_test_seed_sql(config_file: "path/to/seed-config.json")
3. verify_test_seed(config_file: "path/to/seed-config.json")
4. run_integration_harness(config_file: "path/to/harness-config.json")
```

### Pattern: Fix Stored Procedure Issues

```
1. analyze_stored_procedures(procedures_dir: "Database/UpdatedStoredProcedures/")
2. analyze_dependencies(procedures_dir: "Database/UpdatedStoredProcedures/")
3. install_stored_procedures(config_file: "path/to/install-config.json")
4. validate_schema(config_file: "path/to/schema-config.json")
```

---

**Tool Count**: 26 tools documented  
**Server Version**: mtm-workflow v1.0  
**Maintained by**: Development Team
