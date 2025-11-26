# Implementation Plan: Startup Process Analysis and Hardening

**Branch**: `002-startup-audit` | **Date**: 2025-11-25 | **Spec**: [specs/002-startup-audit/spec.md](specs/002-startup-audit/spec.md)
**Input**: Feature specification from `/specs/002-startup-audit/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

The startup process will be refactored to eliminate race conditions, remove redundant database checks, and improve user feedback. The `Program.cs` entry point will be simplified to handle only environment setup, while `StartupSplashApplicationContext` will manage the async loading sequence, including user settings, permissions, and database connectivity verification. A new "Shared Workstation" login flow will be implemented for generic accounts (SHOP2/MTMDC), and strict error handling will be enforced for database failures and unauthorized users.

## Technical Context

**Language/Version**: C# 12.0 / .NET 8.0
**Primary Dependencies**: WinForms, MySQL.Data 9.4.0, Microsoft.Extensions.DependencyInjection
**Storage**: MySQL 5.7.24 (Legacy)
**Testing**: xUnit 2.6.2 (Integration Tests)
**Target Platform**: Windows Desktop
**Project Type**: WinForms Application
**Performance Goals**: Startup < 5 seconds (network dependent), Splash screen updates < 100ms
**Constraints**: No blocking I/O on UI thread, 30s DB timeout, ReadOnly fallback for technical failures
**Scale/Scope**: Core startup sequence affecting all users

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### I. Centralized Error Handling
- **Requirement**: All exceptions MUST be handled through `Service_ErrorHandler`.
- **Plan**: Startup exceptions in `StartupSplashApplicationContext` will use `Service_ErrorHandler.HandleException` (Fatal/High severity) before exiting. `Program.cs` global handlers remain as safety net.
- **Status**: ✅ Compliant

### II. Structured Logging
- **Requirement**: All logging MUST use `LoggingUtility`.
- **Plan**: Startup steps will log progress and errors to `LoggingUtility`. `Console.WriteLine` usage in `Program.cs` will be minimized/removed in favor of `LoggingUtility`.
- **Status**: ✅ Compliant

### III. Model_Dao_Result Pattern
- **Requirement**: All DAO methods MUST return `Model_Dao_Result<T>`.
- **Plan**: `Dao_System` and `Dao_User` methods used in startup already follow this pattern. New validation logic will also use it.
- **Status**: ✅ Compliant

### IV. Async-First Database Operations
- **Requirement**: All database operations MUST be async.
- **Plan**: `Service_OnStartup_User` methods will be refactored to return `Task`. `Program.Main` will no longer call sync DB methods.
- **Status**: ✅ Compliant

### V. WinForms Best Practices
- **Requirement**: UI thread marshaling, proper disposal.
- **Plan**: Splash screen updates will happen on UI thread. `StartupSplashApplicationContext` manages form lifecycle.
- **Status**: ✅ Compliant

### VIII. Null Safety Requirements
- **Requirement**: Nullable reference types enabled.
- **Plan**: New code will use nullable annotations.
- **Status**: ✅ Compliant

### IX. Theme System Integration
- **Requirement**: Inherit from `ThemedForm`.
- **Plan**: `SplashScreenForm` (existing) should ideally inherit `ThemedForm`, but scope is focused on logic. Will verify if `SplashScreenForm` needs update or if it's out of scope. *Self-correction: Spec focuses on logic, but UI consistency is good. Will check if Splash needs theming.*
- **Status**: ✅ Compliant

### XI. Input Validation Service
- **Requirement**: Use `Service_Validation`.
- **Plan**: Shared Workstation login (Username/PIN) will use `Service_Validation` before checking database.
- **Status**: ✅ Compliant

## Project Structure

### Documentation (this feature)

```text
specs/002-startup-audit/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output
└── tasks.md             # Phase 2 output
```

### Source Code (repository root)

```text
MTM_WIP_Application_WinForms/
├── Program.cs                                      # Entry point refactoring
├── Services/
│   └── Startup/
│       ├── Service_OnStartup_StartupSplashApplicationContext.cs # Main logic
│       ├── Service_OnStartup_User.cs               # Async refactoring
│       └── Service_OnStartup_Database.cs           # Cleanup
├── Forms/
│   └── Splash/
│       └── SplashScreenForm.cs                     # UI updates
└── .github/
    └── instructions/
        └── startup-sequence.instructions.md        # Documentation
```

**Structure Decision**: Standard WinForms structure, modifying existing files in `Services/Startup` and `Program.cs`.

## Complexity Tracking

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| None | N/A | N/A |
