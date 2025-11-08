---
description: 'Documentation standards for code comments, XML docs, and markdown files'
applyTo: '**/*.cs,**/*.md'
---

<!-- Based on patterns from: https://github.com/github/awesome-copilot -->

# Documentation Standards

## Available MCP Tools for Documentation

When working on documentation, use these MCP tools from the **mtm-workflow** server:
- `check_xml_docs` - Analyze XML documentation coverage and identify missing docs
- `validate_dao_patterns` - Verify code structure before documenting
- `analyze_dependencies` - Map system architecture for documentation
- `check_checklists` - Confirm quality-gate checklists referenced in documentation are complete before publishing.

## Overview

This file defines documentation standards for code comments, XML documentation, README files, and markdown documentation for the MTM WIP Application.

## Core Principles

### Documentation Serves Readers
- Write for developers who will maintain the code
- Explain "why", not "what"
- Keep documentation up-to-date with code changes
- Use clear, concise language

### Code is Self-Documenting
- Use descriptive names for classes, methods, and variables
- Code structure should reveal intent
- Comments explain non-obvious decisions
- Avoid stating the obvious

## XML Documentation Comments

### Required XML Comments
- All public classes, interfaces, and enums
- All public methods, properties, and events
- Complex internal methods when logic is non-obvious

### XML Comment Tags

#### Summary Tag
```
/// <summary>
/// Retrieves inventory items for the specified location code.
/// </summary>
```

#### Parameter Tags
```
/// <param name="locationCode">The location code to filter inventory items.</param>
/// <param name="includeInactive">Whether to include inactive items in results.</param>
```

#### Returns Tag
```
/// <returns>A list of inventory items matching the specified criteria.</returns>
```

#### Exception Tags
```
/// <exception cref="ArgumentNullException">Thrown when locationCode is null.</exception>
/// <exception cref="InvalidOperationException">Thrown when database connection fails.</exception>
```

#### Remarks Tag
```
/// <remarks>
/// This method queries the database using stored procedure usp_GetInventory.
/// Results are cached for 5 minutes to improve performance.
/// </remarks>
```

### Complete Example
```
/// <summary>
/// Saves inventory changes to the database.
/// </summary>
/// <param name="inventoryItem">The inventory item to save.</param>
/// <param name="cancellationToken">Cancellation token for async operation.</param>
/// <returns>A ServiceResult indicating success or failure.</returns>
/// <exception cref="ArgumentNullException">Thrown when inventoryItem is null.</exception>
/// <remarks>
/// This method validates the inventory item before saving and logs all operations.
/// If validation fails, no database operation is performed.
/// </remarks>
public async Task<ServiceResult> SaveInventoryAsync(
    InventoryItem inventoryItem,
    CancellationToken cancellationToken = default)
{
    // Implementation
}
```

## Code Comments

### When to Write Comments
- Explain non-obvious design decisions
- Document complex algorithms
- Clarify business rules
- Explain workarounds for known issues
- Provide context for future maintainers

### When NOT to Write Comments
- Don't state the obvious: `// Increment counter` → `counter++;`
- Don't comment bad code - refactor it instead
- Don't leave commented-out code - use version control
- Don't write comments that will become outdated

### Comment Style
```
// Single-line comments for brief explanations

// Multi-line comments for longer explanations that need
// multiple lines to fully describe the context and reasoning
// behind a particular implementation choice.

/* Block comments for very long explanations or when temporarily
   disabling code during debugging. Prefer // comments for production code. */
```

### TODO Comments
```
// TODO: Implement retry logic for transient database failures
// TODO: Add validation for date range inputs
// HACK: Temporary guard for legacy WinForms layout; remove after redesign
// FIXME: Memory leak when loading large datasets - needs optimization
```

## README Documentation

### README.md Structure
1. **Project Title and Description**: Brief overview of application purpose
2. **Features**: Key capabilities and functionality
3. **Prerequisites**: Required software and tools
4. **Installation**: Step-by-step setup instructions
5. **Configuration**: How to configure the application
6. **Usage**: How to run and use the application
7. **Architecture**: High-level architectural overview
8. **Contributing**: Guidelines for contributors (if applicable)
9. **License**: License information

### README Example Sections

#### Installation Section
````markdown
## Installation

1. Clone the repository:
```powershell
git clone https://github.com/org/MTM_WIP_Application_WinForms.git
cd MTM_WIP_Application_WinForms
```

2. Restore dependencies:
```powershell
dotnet restore
```

3. Configure database connection via `Helpers/Helper_Database_Variables.cs`

4. Build and run:
```powershell
dotnet build MTM_WIP_Application_Winforms.csproj
dotnet run --project MTM_WIP_Application_Winforms.csproj
```
````

#### Configuration Section
```markdown
## Configuration

Update `Helper_Database_Variables.cs` or environment configuration to set:
- Database connection strings and credentials
- Logging locations and retention settings
- Manufacturing domain values (valid operations, default locations)
- Session timeout and user preference flags
```

## Operational Documentation

- Maintain developer-facing notes in `Documentation/` alongside user guides (e.g., `TransactionHelp.md`, update summaries).
- When adding new workflows or stored procedures, capture the intent, validation rules, and rollback strategy in the relevant markdown file.
- Summaries of troubleshooting steps or production incidents should land in `Documentation/Fixes/` so future work benefits from that knowledge.

## Inline Documentation

### WinForms Designer Considerations
- Designer files are generated code—avoid editing unless a diff is unavoidable. Add explanations in the code-behind or helper files instead.
- When documenting control purpose, add the comment next to the control field declaration in the non-designer partial class.

### Event Handler Notes
```csharp
// Event handler registered in InitializeComponent. Keep the handler short and
// forward work to Helper_ or Dao_ classes to maintain testability.
private void btnSave_Click(object sender, EventArgs e)
{
    // Validation logic and DAO invocation here.
}
```

### Region Usage (Sparingly)
- Reserve regions for collapsing low-change helper sections (e.g., data-binding helpers).
- Avoid splitting core logic across multiple regions when a clear method structure would communicate intent better.

Prefer well-structured files over heavy region use. Regions exist to collapse infrequently edited helper sections, not to hide complex logic.

## API Documentation

### DAO and Helper Documentation
```
/// <summary>
/// Executes the inventory adjustment stored procedure and returns the result envelope.
/// </summary>
/// <param name="inventoryId">Unique identifier of the inventory record.</param>
/// <param name="quantityDelta">Quantity adjustment to apply.</param>
/// <returns>A <see cref="Model_Dao_Result"/> containing status, error message, and optional payload.</returns>
/// <remarks>
/// The underlying stored procedure enforces manufacturing business rules. Callers should inspect
/// the returned status and message before updating UI state.
/// </remarks>
public static Model_Dao_Result Transactions_AdjustInventory(long inventoryId, decimal quantityDelta)
{
    // Implementation
}
```

## Change Documentation

### CHANGELOG.md
- Track changes by version
- Categories: Added, Changed, Deprecated, Removed, Fixed, Security
- Date each release
- Follow semantic versioning

### Example
```markdown
# Changelog

## [5.0.0] - 2025-10-10

### Added
- Comprehensive GitHub Copilot configuration system
- 9 instruction files for code generation patterns
- 10 reusable prompts for component scaffolding

### Changed
- Refined WinForms data-entry workflows and DAO helpers
- Updated logging configuration to align with new diagnostics requirements

### Fixed
- Eliminated DataGridView refresh timing issues
- Resolved cleanup bug in temp file handling during splash shutdown
```

## Architecture Documentation

### Architecture Notes
- Capture significant WinForms refactors, DAO reorganizations, or service introductions in `Documentation/`.
- Show form navigation flow diagrams and DAO relationships when major changes occur.
- Document stored procedure dependencies alongside DAO updates so database changes remain traceable.

## Maintenance

### Keeping Documentation Current
- Update documentation in same commit as code changes
- Review documentation during code reviews
- Remove outdated documentation promptly
- Use automated tools to check for broken links

### Documentation Debt
- Track missing or outdated documentation
- Prioritize documentation for public APIs
- Schedule regular documentation review sessions
- Treat documentation gaps as technical debt
