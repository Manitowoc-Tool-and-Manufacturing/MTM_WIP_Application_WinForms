# MTM Workflow MCP Server – Tool Reference

This document is the authoritative catalog for MCP tools that ship with the MTM workflow server. Keep it in sync with the code in `.mcp/mtm-workflow` and with the live installation in `C:\.mcp`.

## Directory Roles

- `.mcp/` inside the repository is the **development sandbox**. Create or modify tools here first, run tests, and validate output.
- `C:\.mcp` is the **global runtime** used by VS Code. After every change, copy the updated `mtm-workflow` contents from the repo into `C:\.mcp` so both locations remain identical.

## Available Tools (mtm-workflow)

| Tool | Purpose | Key Inputs |
| --- | --- | --- |
| `check_checklists` | Summarise Markdown checklist completion. | `checklist_dir` |
| `validate_dao_patterns` | Enforce DAO coding standards. | `dao_dir`, `recursive` |
| `analyze_stored_procedures` | Verify stored procedure contract compliance. | `procedures_dir`, `recursive` |
| `compare_databases` | Spot schema drift between Current/Updated snapshots. | `current_dir`, `updated_dir` |
| `generate_dao_wrapper` | Scaffold DAO wrappers from stored procedures. | `procedure_file`, `output_dir?` |
| `validate_error_handling` | Audit Service_ErrorHandler usage and anti-patterns. | `source_dir`, `recursive` |
| `analyze_dependencies` | Build stored procedure dependency graphs. | `procedures_dir` |
| `check_xml_docs` | Report XML documentation coverage. | `source_dir`, `recursive`, `min_coverage` |
| `generate_unit_tests` | Create unit test scaffolding for C# classes. | `source_file`, `output_dir?`, `test_framework?` |
| `suggest_refactoring` | Surface refactoring opportunities in C#/SQL. | `source_dir`, `recursive`, `file_type` |
| `analyze_performance` | Detect performance issues in C#. | `source_dir`, `recursive`, `focus` |
| `check_security` | Perform security scans of C#/config files. | `source_dir`, `recursive`, `scan_type` |
| `apply_ui_fixes` | Apply automated WinForms UI corrections from a JSON plan. | `fix_plan_file`, `backup_dir?`, `dry_run?` |
| `generate_test_seed_sql` | Produce SQL seed script from JSON-defined datasets for integration testing. | `config_file`, `output_sql?` |
| `verify_test_seed` | Validate seeded data against JSON expectations using MySQL. | `config_file`, `host?`, `port?`, `user?`, `password?`, `database?` |
| `generate_ui_fix_plan` | Produce JSON fix plans for WinForms UI issues. | `source_dir`, `recursive`, `include_warnings?`, `output_file?` |
| `validate_ui_scaling` | Validate DPI/layout consistency in WinForms files. | `source_dir`, `recursive`, `file_types` |
| `analyze_spec_context` | Parse SpecKit feature specs for context. | `feature_dir` |
| `load_instructions` | Load SpecKit instruction mappings. | `tasks_file`, `instructions_dir` |
| `parse_tasks` | Parse SpecKit task lists into structured data. | `tasks_file` |
| `mark_task_complete` | Mark SpecKit tasks complete with optional note. | `tasks_file`, `task_ids`, `note?` |
| `validate_build` | Run `dotnet build` (optionally tests) to validate projects. | `workspace_root`, `project_file?`, `run_tests?`, `check_errors?` |
| `verify_ignore_files` | Check /.gitignore style coverage for required patterns. | `workspace_root`, `tech_stack?` |

> ℹ️ Optional parameters are marked with `?`. See TypeScript source in `src/tools` for exact signatures.

## Adding a New Tool

1. Author the tool in `mtm-workflow/src/tools/` and export it from `src/index.ts`.
2. Update this reference table.
3. Re-run `npm install` (if new deps) and `npm run build` inside `.mcp/mtm-workflow`.
4. Sync the built server to `C:\.mcp` (see `GLOBAL-INSTALL.md`).
5. Restart VS Code to pick up the changes.

## Verifying Tool Availability

After syncing, confirm tools are exposed:

```powershell
# From any PowerShell window
node C:\.mcp\mtm-workflow\dist\index.js --list-tools
```

The list returned should match the table above. If it does not, rebuild and re-sync.
