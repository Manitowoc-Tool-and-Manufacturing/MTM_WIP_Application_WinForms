---
description: 'C# and .NET 8 development guidelines for MTM manufacturing application'
applyTo: '**/*.cs'
---

<!-- Based on patterns from: https://github.com/github/awesome-copilot -->

# C# and .NET 8 Development Guidelines

## Overview

These guidelines describe how to structure and implement C# code within the MTM WinForms application. Favor consistency with the existing Forms/DAO/helpers architecture and .NET 8 language improvements that do not interfere with the WinForms designer.

## Relevant MCP Tools

- `validate_dao_patterns` – Run against `Data/` when creating or refactoring DAOs to confirm region layout, Helper_Database usage, async patterns, and XML docs match these rules.
- `validate_error_handling` – Scan updated C# folders to verify Service_ErrorHandler adoption and to catch lingering MessageBox.Show calls.
- `analyze_performance` – Target UI or database code paths during refactors to surface blocking awaits, N+1 loops, and other pitfalls called out in this guide.
- `suggest_refactoring` – Generate prioritized clean-up opportunities before large rewrites so new code adheres to the architecture described here.
- `check_xml_docs` – Confirm public APIs touched by your changes meet the documentation expectations outlined below.
- `generate_unit_tests` – Produce scaffolding for helper/service classes when expanding coverage while staying within the mandated patterns.

## Core Principles

- Target `.NET 8.0` across the solution; no multi-targeting is required.
- Keep UI event handling, helper utilities, and database logic separated. Forms react to UI events, helpers encapsulate reusable logic, and DAOs perform data access through stored procedures.
- Prefer clarity over clever constructs. The application depends on long-lived maintainers who benefit from predictable patterns.
- Adopt newer C# features (file-scoped namespaces, required members, pattern matching) when they do not confuse the designer or existing style.

## Language & Style

- Use file-scoped namespaces and `internal` visibility where applicable; leave WinForms designer files in their generated structure.
- Favor `readonly` fields and `sealed` classes when extensions are not required.
- Use `var` when the type is obvious from the right-hand side; otherwise spell out the type for readability.
- Prefer pattern matching and switch expressions for complex branching, but keep guard clauses explicit in startup and error handling code.
- Dispose `IDisposable` objects with `using` declarations or try/finally when scope is not block-based (e.g., Form lifetime).

## Naming Conventions

- Classes and interfaces use PascalCase. Forms end with `Form`, user controls end with `Control`, dialogs end with `Dialog`.
- Data access classes live in `Data/` and begin with `Dao_` (e.g., `Dao_Inventory`), matching current practice.
- Helper classes live in `Helpers/` and begin with `Helper_` to keep discoverability high.
- Private fields use the `_camelCase` convention; parameters remain camelCase without underscores.
- Async methods end with `Async`. WinForms event handlers remain `void` unless explicitly using async/await patterns with `async void`.

## WinForms Patterns

**Cross-Reference**: For responsive layout architecture, see `.github/instructions/winforms-responsive-layout.instructions.md`

- Keep event handlers thin. Collect user input, call into helpers/DAOs/services, then update controls.
- Avoid blocking the UI thread. Use background workers, `Task.Run`, or asynchronous DAO calls and marshal back to the UI thread with `BeginInvoke`/`Invoke`/`SynchronizationContext.Post` when updating controls.
- Respect designer-generated code. Do not hand-edit `.Designer.cs` files; move logic into partial class files or helpers.
- Centralize shared UI logic in `Controls/Shared` or helper classes rather than duplicating across forms.
- Use `Model_AppVariables`, `Service_DebugTracer`, and logging helpers for shared application state and diagnostics instead of global statics scattered throughout the codebase.
- **Layout**: Use TableLayoutPanel with mixed Absolute/Percent sizing for responsive designs (see winforms-responsive-layout.instructions.md)
- **Spacing**: Add Padding (10px) to containers, Margin (5px) to controls for professional appearance
- **Constraints**: Set MinimumSize on DataGridView and main content areas to prevent unusable collapse

## Data Access & Async

- All database operations should route through DAO classes which call stored procedures via `Helper_Database_StoredProcedure` or `Helper_Database_Variables`.
- Prefer async DAO methods (`Task<Model>` or `Task<DaoResult>`) so callers can keep the UI responsive. Only block when a WinForms API requires synchronous completion.
- Never call `.Result` or `.Wait()` on tasks inside UI code; schedule continuation work with `await` or explicit callbacks.
- Capture stored procedure outputs in POCO models located in `Models/`. Keep transformation logic close to the DAO when it is purely data shaping.

### Refactoring to Use Existing Stored Procedures

When a DAO calls a non-existent stored procedure, check if an existing procedure returns the needed data before creating a new one:

```csharp
// ❌ BEFORE: Calling non-existent scalar procedure
var itemTypeResult = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
    connectionString,
    "md_part_ids_GetItemType_ByPartID",  // Doesn't exist
    new Dictionary<string, object> { ["p_PartID"] = partId }
);
itemType = itemTypeResult.IsSuccess ? itemTypeResult.Data?.ToString() : "None";

// ✅ AFTER: Using existing procedure that returns full record
var itemTypeResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "md_part_ids_Get_ByItemNumber",  // Existing procedure
    new Dictionary<string, object> { ["ItemNumber"] = partId }
);
if (itemTypeResult.IsSuccess && itemTypeResult.Data.Rows.Count > 0)
{
    itemType = itemTypeResult.Data.Rows[0]["ItemType"]?.ToString() ?? "None";
}
```

This pattern avoids creating duplicate procedures when existing ones already return the required data.

## Error Handling & Logging

- Wrap external boundary calls (database, file system, interop) in try/catch blocks that log using `LoggingUtility` or `Service_DebugTracer`.
- Convert raw exceptions into `DaoResult` or other structured responses so the UI layer can show actionable messages.
- Use targeted catch blocks (`MySqlException`, `TimeoutException`, etc.) to provide accurate user prompts. Let unexpected exceptions bubble to the global handlers configured in `Program.cs`.
- Do not swallow exceptions silently. If an exception is expected and handled, document why in a brief comment.

## Configuration & Environment

- Store connection strings and environment-sensitive settings in `Helper_Database_Variables` or configuration files, never hard-code credentials.
- When adding settings, extend existing helper classes or add new strongly-typed models in `Models/` rather than scattering literals.
- Use `Path.Combine` and `Environment` APIs for file system access to remain portable across Windows configurations.

## Performance Considerations

- Keep long-running work off the UI thread. For example, wrap inventory exports or report generation in background tasks and provide progress updates through the existing `Helper_StoredProcedureProgress` patterns.
- Batch database operations when possible and rely on stored procedures for complex filtering to reduce client-side iteration.
- Ensure DataGridView and other heavy controls only bind to data that is already materialized in memory to avoid repeated database calls during painting.

## Documentation & Comments

- Public APIs (especially helpers and DAOs) should carry XML documentation that explains the intent and any side effects. Include `<exception>` tags when throwing meaningful exceptions.
- Keep inline comments minimal and focused on “why” decisions (e.g., rationale for retry limits, ordering requirements). The structure should remain self-explanatory.
- Update `Documentation/` artifacts alongside code when procedures or workflows change.

## Testing & Validation

- Define manual validation steps for each feature—exercise form workflows, confirm DAO updates, and capture results per `testing-standards.instructions.md`.
- When adding new database interactions, document the required stored procedure changes and run through both success and failure paths.
- After significant changes, run `dotnet build` and open affected forms to ensure the designer renders without errors.

## File Organization

- Place new forms under `Forms/<Area>/` with matching designer/code-behind partial classes.
- Add new DAO files to `Data/` with the `Dao_` prefix and keep related stored procedure documentation in `Database/UpdatedStoredProcedures/`.
- Shared models belong in `Models/`; avoid adding DTOs inline within forms or services.
- Keep using directives ordered by System → external packages → project namespaces and remove unused imports to avoid noise.
