# MTM_WIP_Application_WinForms Copilot Configuration

Last updated: 2025-10-13

## Active Technologies
- .NET 8.0 Windows Forms (WinForms) desktop application
- MySQL 5.7 + MySql.Data connector (stored procedure driven data access)
- Dapper-style helper utilities in `Helpers/` for database calls and UI helpers
- C# 12, .NET 8.0 (file-scoped namespaces, required members, pattern matching) + MySql.Data 8.x (MySqlConnection, MySqlCommand), System.Text.Json, Microsoft.Web.WebView2, ClosedXML (002-comprehensive-database-layer)
- MySQL 5.7.24+ (MAMP compatible) - stored procedures only, no inline SQL permitted (002-comprehensive-database-layer)
- C# 12 / .NET 8.0 Windows Forms + MySql.Data 9.4.0, System.Text.Json (for offline queue serialization) (001-error-reporting-with)
- MySQL 5.7+ via stored procedures only (error_reports table), local file system for offline queue (001-error-reporting-with)
- [e.g., Python 3.11, Swift 5.9, Rust 1.75 or NEEDS CLARIFICATION] + [e.g., FastAPI, UIKit, LLVM or NEEDS CLARIFICATION] (005-transaction-viewer-form)
- [if applicable, e.g., PostgreSQL, CoreData, files or N/A] (005-transaction-viewer-form)

## Agent Communication Rules

**CRITICAL**: Agents working on this project MUST follow these communication protocols:

- **Remain silent** during work execution unless:
  - Acknowledging the user's prompt at the start
  - Asking clarifying questions when requirements are ambiguous
  - Providing a brief summary at the end of the current run
- **Keep chat summaries minimal** - one or two sentences maximum
- **No explanations or updates** during task execution
- **No unnecessary status updates** - let the code speak for itself
- **Maximize Premium Request value** - ⚠️ **EXTREMELY IMPORTANT - NEVER SKIP THIS**: Strive to complete multiple related tasks in a single session rather than stopping after one task. Continue working through sequential or related tasks until a natural checkpoint is reached or significant complexity requires user input. DO NOT stop prematurely just because one task is complete

## Core Instruction Files

GitHub Copilot will load these guidance files while generating code:

- #file:instructions/csharp-dotnet8.instructions.md
- #file:instructions/mysql-database.instructions.md
- #file:instructions/testing-standards.instructions.md
- #file:instructions/integration-testing.instructions.md (NEW: Integration test development patterns, method signature discovery workflow)
- #file:instructions/documentation.instructions.md
- #file:instructions/security-best-practices.instructions.md
- #file:instructions/performance-optimization.instructions.md
- #file:instructions/code-review-standards.instructions.md

> Note: Avalonia/MVVM instructions are being retired. Prefer WinForms event-driven patterns and the existing DAO/service abstractions when authoring new code.

## Memory Files

Cross-project lessons that remain relevant:

**Workspace-Specific:**
- #file:instructions/validation-automation-memory.instructions.md (Automation validation patterns and quality gates)

**Global (Cross-Project):**
- `vscode-userdata:/User/prompts/powershell-memory.instructions.md` (PowerShell scripting patterns and pipeline behaviors)
- `vscode-userdata:/User/prompts/debugging-memory.instructions.md` (Debugging workflows and troubleshooting strategies)
- `vscode-userdata:/User/prompts/memory.instructions.md` (Universal development patterns)

**Legacy (Being Phased Out):**
- All Avalonia/MVVM memories are being retired; ignore any that reference AXAML or MVVM patterns

## Useful Prompts

Prompts that still apply to this WinForms solution:

- `/database-operation` – scaffold stored-procedure execution helpers
- `/refactor-code` – assist with large WinForms/DAO refactors
- `/generate-docs` – add XML docs and inline explanations
- `/debug-issue` – gather structured context when troubleshooting
- `/write-tests` – outline manual validation scenarios for forms and workflows

## MCP Tooling Quick Reference

JSON configs for these tools live under `.mcp/samples/` (mirror to `C:\.mcp\samples` when syncing).

- `generate_test_seed_sql` – build seed scripts for integration fixtures from declarative JSON.
- `verify_test_seed` – check seeded records against expected rows before/after DAO runs.
- `install_stored_procedures` – detect drift and apply `Database/UpdatedStoredProcedures/ReadyForVerification` scripts.
- `validate_schema` – compare the live MySQL schema with `Database/database-schema-snapshot.json` before executing suites.
- `run_integration_harness` – orchestrate end-to-end DAO workflows (seed → install → tests → cleanup) via JSON-defined steps.
- `audit_database_cleanup` – report and optionally purge residual `TEST-*` style rows so each suite starts clean.

(Other Avalonia-specific prompts should be removed or rewritten before reuse.)

## Project Structure
```
.
├── Core/                     # Theme, JSON, and shared WinForms utilities
├── Data/                     # DAO classes wrapping stored procedures
├── Forms/                    # WinForms UI (MainForm, Settings, Transactions, etc.)
├── Helpers/                  # UI and database helper classes
├── Models/                   # POCOs for users, inventory, history, etc.
├── Services/                 # Background services (timers, startup, diagnostics)
├── Logging/                  # Logging utility abstractions
├── Resources/                # WinForms resource files
├── Database/                 # Stored procedures and schema snapshots
├── Documentation/            # End-user and developer documentation
└── .github/                  # Copilot configuration (instructions, prompts, workflows)
```

## Build and Run
```powershell
# Restore packages
dotnet restore

# Build WinForms project (Debug)
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug

# Build Release when preparing deployments
dotnet build MTM_WIP_Application_Winforms.csproj -c Release

# Launch application (opens WinForms UI)
dotnet run --project MTM_WIP_Application_Winforms.csproj
```

## Database Connection
```
Server: localhost
Port:   3306
Database: MTM_WIP_Application_Winforms
Username: root
Password: root
Connection string template:
  Server=localhost;Port=3306;Database=MTM_WIP_Application_Winforms;
  User=root;Password=root;SslMode=none;AllowPublicKeyRetrieval=true;
```

Data access code should continue to route through the helper classes in `Helpers/` and DAO wrappers in `Data/`. Every query is executed via stored procedures; avoid inline SQL.

## Testing Expectations

- Manual validation is the primary QA approach.
- Exercise key WinForms workflows (login, inventory adjustments, transfers, reporting).
- Confirm database side-effects using the DAO history tables and stored procedure outputs.
- Follow guidance in `testing-standards.instructions.md` to capture success criteria and regression coverage.

## Coding Guidelines

- Apply .NET 8 language features where helpful, but keep compatibility with WinForms designer code patterns.
- Maintain separation between UI event handlers, helper utilities, and DAO/service layers.
- Reuse existing logging and error-reporting utilities in `Logging/` and `Services/`.
- Keep long-running operations off the UI thread—use async patterns or background workers as documented in `performance-optimization.instructions.md`.

## Operations Checklist for Copilot Sessions

1. Respect existing file organization; prefer enhancing DAOs/services over introducing new abstractions.
2. Keep configuration secrets out of source (use `Helper_Database_Variables` and configuration files).
3. Align naming with existing WinForms controls and models.
4. Favor stored procedure updates over ad-hoc SQL changes.
5. Document manual validation steps whenever features touch production workflows.

## Work in Progress
- Avalonia/MVVM artifacts are being removed from `.github/`.
- Chatmodes will be rebuilt around WinForms troubleshooting and manufacturing workflows once cleanup completes.
- The Copilot workflow will be expanded with WinForms-specific prompts after the instruction set stabilizes.

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
