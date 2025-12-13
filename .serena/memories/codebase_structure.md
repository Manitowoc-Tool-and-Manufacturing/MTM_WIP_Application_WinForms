# Codebase Structure

## Root Directory
```
MTM_WIP_Application_WinForms/
├── .git/                    # Git repository
├── .github/                 # GitHub configuration and instructions
│   └── instructions/        # Detailed coding guidelines by topic
├── .serena/                 # Serena configuration and project data
├── .specify/                # Project specifications and memory
├── .vscode/                 # VS Code configuration
├── bin/                     # Build output
├── obj/                     # Build artifacts
└── specs/                   # Feature specifications
```

## Source Code Structure

### Core/
Core utilities and theme system
- `Core_JsonColorConverter.cs` - JSON color serialization
- `Core_TablePrinter.cs` - Table printing utilities
- `Core_Themes.cs` - Theme management
- `Core_WipAppVariables.cs` - Application-wide variables
- `DependencyInjection/` - DI container setup
- `Theming/` - Theme system (DI-based)
- `Utilities/` - General utilities

### Data/
Data Access Objects (DAOs) - Database interaction layer
- `Dao_Inventory.cs` - Inventory data access
- `Dao_User.cs` - User data access
- `Dao_Transactions.cs` - Transaction data access
- `Dao_QuickButtons.cs` - Quick button data access
- `Dao_ErrorReports.cs` - Error report data access
- And more...

**Rules**:
- ALL database access goes through DAOs
- Use ONLY `Helper_Database_StoredProcedure` for database calls
- Return `Model_Dao_Result<T>` from all methods
- Async methods only

### Services/
Business logic and cross-cutting concerns
- Services for error handling, logging, validation
- Help system services
- Export/import services

### Helpers/
Helper utilities for various tasks
- `Helper_Database_StoredProcedure.cs` - **REQUIRED** for ALL database access
- `Helper_Database_Variables.cs` - Database connection configuration
- `Helper_ExportManager.cs` - Export functionality
- `Helper_PrintManager.cs` - Printing utilities
- `Helper_LogPath.cs` - Log file path management

### Forms/
Windows Forms (UI)
- `MainForm/` - Main application window
- `Settings/` - Settings form
- `Transactions/` - Transaction forms
- `ErrorReports/` - Error reporting forms
- `Shared/` - Shared forms (ThemedForm, ThemedUserControl base classes)

**Rules**:
- ALL forms inherit from `ThemedForm`
- Use dependency injection for services
- NO direct database access

### Controls/
Custom user controls
- `Control_QuickButtons` - Quick action buttons
- `Visual/` - Visual components
- `Addons/` - Additional controls

**Rules**:
- ALL controls inherit from `ThemedUserControl`
- Follow naming: `Control_{Feature}_{Purpose}`

### Models/
Data models, enums, DTOs
- `Model_Dao_Result.cs` - **REQUIRED** return type for all DAO methods
- `Model_Application_Variables.cs` - App-wide variables
- Various domain models

### Database/
Database schema and stored procedures
- `UpdatedDatabase/` - Current database schema
- `UpdatedStoredProcedures/` - Current stored procedures
- `Migration/` - Migration scripts
- `OldDatabase/` - Historical schema

**Rules**:
- ALL SQL must be in stored procedures
- MySQL 5.7.24 compatible only
- NO inline SQL in code

### Documentation/
Project documentation
- `Help/` - User help documentation (JSON + HTML templates)
- `ReleaseNotes/` - Release notes
- Various implementation summaries

### Resources/
Application resources
- Icons
- Images
- Help files
- Excel templates

### Properties/
Assembly properties and settings

## Key Files

- `Program.cs` - Application entry point
- `App.config` - Application configuration
- `MTM_WIP_Application_Winforms.csproj` - Project file
- `.editorconfig` - Editor configuration
- `AGENTS.md` - AI agent instructions
- `.github/copilot-instructions.md` - Copilot instructions

## Configuration Files

- `.editorconfig` - Code style (4 spaces, LF, file-scoped namespaces)
- `App.config` - Database connection, logging paths
- `.serena/project.yml` - Serena project configuration
- `.github/copilot-instructions.md` - Coding standards

## Important Patterns

### File Naming
- Forms: `{Feature}Form.cs` + `{Feature}Form.Designer.cs`
- Controls: `Control_{Feature}_{Purpose}.cs`
- DAOs: `Dao_{Entity}.cs`
- Services: `Service_{Purpose}.cs`
- Helpers: `Helper_{Purpose}.cs`
- Models: `Model_{Scope}_{Entity}.cs`

### Namespace Structure
All use file-scoped namespaces (C# 10+):
```csharp
namespace MTM_WIP_Application_WinForms.Data;

public class Dao_Entity { }
```
