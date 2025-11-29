# MTM WIP Application

> Windows Forms-based inventory management system for Manitowoc Tool and Manufacturing. Built on .NET 8.0 with MySQL 5.7.24 backend, providing real-time work-in-progress tracking, transaction management, and reporting capabilities with a layered architecture enforcing strict separation between Data, Services, and Presentation layers.

This repository follows a strict architectural constitution emphasizing centralized error handling, structured logging, async-first database operations, and dependency injection-based theming. All code must comply with established patterns for DAO implementation, stored procedure usage, and WinForms best practices.

## Documentation

- [AGENTS.md](AGENTS.md): Comprehensive AI agent guide covering project overview, setup commands, development workflow, code style guidelines, architecture patterns, and common workflows
- [GitHub Copilot Instructions](.github/copilot-instructions.md): Auto-generated coding standards, technology stack requirements, architectural boundaries, naming conventions, and core patterns
- [Constitution](.specify/memory/constitution.md): Non-negotiable architectural principles including centralized error handling, structured logging, Model_Dao_Result pattern, async-first operations, and WinForms best practices
- [Release Notes (User-Friendly)](RELEASE_NOTES_USER_FRIENDLY.md): User-facing release notes with feature summaries, upgrade guidance, and FAQs for shop floor and office staff

## Architecture & Standards

- [DAO Patterns](.github/instructions/dao-patterns.instructions.md): Data Access Object implementation requirements, Model_Dao_Result pattern, and stored procedure integration
- [Service Error Handler](.github/instructions/service-error-handler.instructions.md): Centralized error handling service with exception management, user notifications, and WinForms integration patterns
- [Service Logging](.github/instructions/service-logging.instructions.md): Structured CSV logging, error tracking, and log management for WinForms applications
- [Theme System](.github/instructions/theme-system.instructions.md): Dependency injection-based theming architecture, ThemedForm/ThemedUserControl base classes, and implementation guide
- [UI Structure](.github/instructions/ui-structure.instructions.md): Standard UI layout, naming conventions for controls, and WinForms designer patterns
- [Startup Sequence](.github/instructions/startup-sequence.instructions.md): Application initialization order, dependency injection configuration, and bootstrap process

## Database Standards

- [MySQL Conventions](.github/instructions/mysql-conventions.instructions.md): MySQL 5.7.24 compatibility rules, forbidden 8.0+ features, and naming conventions
- [MySQL Schema Tables](.github/instructions/mysql-schema-tables.instructions.md): Database schema documentation, table structures, and relationships
- [MySQL Stored Procedures](.github/instructions/mysql-stored-procedures.instructions.md): Stored procedure standards, parameter conventions, and return code patterns
- [Stored Procedure Return Codes](.github/instructions/stored-procedure-return-codes.instructions.md): Standard status codes, error severity mapping, and handling patterns
- [Legacy MySQL Conventions](.github/instructions/legacy-mysql-conventions.instructions.md): Historical MySQL patterns for reference when maintaining legacy code
- [Legacy MySQL Schema Tables](.github/instructions/legacy-mysql-schema-tables.instructions.md): Historical database schema documentation for legacy systems
- [Legacy MySQL Stored Procedures](.github/instructions/legacy-mysql-stored-procedures.instructions.md): Legacy stored procedure patterns and migration guidance

## Specifications & Features

- [Visual Dashboard Specification](specs/014-visual-dashboard/spec.md): Infor Visual ERP integration feature specification with user scenarios, requirements, and AI workflow guidance
- [Visual Dashboard Plan](specs/014-visual-dashboard/plan.md): Implementation plan for Visual Dashboard feature
- [Visual Dashboard Tasks](specs/014-visual-dashboard/tasks.md): Task breakdown for Visual Dashboard development
- [Visual Dashboard Data Model](specs/014-visual-dashboard/data-model.md): Entity relationships and data structures for Visual Dashboard

## AI Workflow & Prompts

- [Generate Suggestions](.github/prompts/mtm.generate-suggestions.prompt.md): AI-powered code improvement and refactoring suggestions
- [Update Release Notes](.github/prompts/mtm.update-release-notes.prompt.md): Automated release note generation from version changes
- [SpecKit Analyze](.github/prompts/speckit.analyze.prompt.md): Codebase analysis and pattern detection
- [SpecKit Checklist](.github/prompts/speckit.checklist.prompt.md): Quality assurance checklist generation for features
- [SpecKit Clarify](.github/prompts/speckit.clarify.prompt.md): Requirements clarification and ambiguity resolution workflow
- [SpecKit Constitution](.github/prompts/speckit.constitution.prompt.md): Constitution compliance verification workflow
- [SpecKit Implement](.github/prompts/speckit.implement.prompt.md): Code implementation workflow with constitution compliance
- [SpecKit Plan](.github/prompts/speckit.plan.prompt.md): Implementation planning and task sequencing workflow
- [SpecKit Specify](.github/prompts/speckit.specify.prompt.md): Feature specification generation with user scenarios and requirements
- [SpecKit Tasks](.github/prompts/speckit.tasks.prompt.md): Task breakdown and estimation workflow

## Configuration & Setup

- [App.config](App.config): Application configuration including database connection strings, logging paths, and runtime settings
- [Project File](MTM_WIP_Application_Winforms.csproj): .NET 8.0 project configuration with dependencies, build settings, and resource references
- [Solution File](MTM_WIP_Application.sln): Visual Studio solution structure
- [Release Notes JSON](RELEASE_NOTES.json): Structured release history with technical details and version metadata

## Optional

- [UI Suggestion TextBox](.github/instructions/ui-suggestion-textbox.instructions.md): Implementation guide for SuggestionTextBoxWithLabel control with autocomplete and validation
- [Data Migration](.github/instructions/data-migration.instructions.md): Database migration procedures and version upgrade patterns
- [Suggestion Control Implementation Workflow](.github/workflows/suggestion-control-implementation.md): Step-by-step workflow for implementing suggestion controls
- [Service Refactoring Workflow](.github/workflows/service-refactoring.workflow.md): Service layer refactoring patterns and migration strategies
- [UI Development Workflow](.github/workflows/ui-development.workflow.md): UI component development standardization workflow
- [Visual Dashboard Prompts](.github/prompts/Visual-Prompts/): Category-specific SQL generation prompts and instructions for Infor Visual integration (Inventory, Receiving, Shipping, etc.)
- [SpecKit Templates](.specify/templates/): Reusable templates for specifications, plans, tasks, checklists, and agent files
- [Design Documents](.github/design/): UI redesign proposals and mockups for advanced inventory controls
- [Release Notes Validation Agent](.github/agents/release-notes-validation.agent.md): Automated agent for validating release notes format and content
- [Database Stored Procedures](Database/UpdatedStoredProcedures/): Organized collection of stored procedures by category (inventory, transactions, master-data, error-reports, logging, etc.)
- [Database Schema Scripts](Database/UpdatedDatabase/): Current database schema definitions and table structures
- [Maintenance Procedures](Database/UpdatedStoredProcedures/maintenance/README_MAINTENANCE_PROCEDURES.md): Database maintenance and cleanup procedures
- [Archived Specifications](specs/Archives/): Historical feature specifications and implementation records (001-theme-refactor through 013-startup-audit)
