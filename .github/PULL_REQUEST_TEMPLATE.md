# Pull Request

## Description

<!-- Provide a clear and concise description of what this PR accomplishes -->

Resolves #(issue)

## Type of Change

- [ ] Bug fix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change which adds functionality)
- [ ] Breaking change (fix or feature that would cause existing functionality to not work as expected)
- [ ] Documentation update
- [ ] Code refactoring (no functional changes)
- [ ] Performance improvement
- [ ] Test coverage improvement

## Feature Information (if applicable)

- **Feature Branch**: `###-feature-name`
- **Specification**: [Link to spec.md]
- **Tasks Completed**: [Link to tasks.md with completed checkboxes]

## Changes Made

<!-- Describe the changes in detail. Use bullet points for multiple changes. -->

-
-
-

## Constitution Compliance Checklist

All PRs MUST comply with the [MTM WIP Application Constitution](.specify/memory/constitution.md).

### Core Principles

- [ ] **Centralized Database Access**: All database operations use `Helper_Database_StoredProcedure` (no direct `MySqlConnection`/`SqlConnection` except documented exceptions)
- [ ] **Stored Procedures Only**: No inline SQL (only stored procedure calls or diagnostic queries through ExecuteScalarWithStatusAsync)
- [ ] **Model_Dao_Result Pattern**: All DAO methods return `Model_Dao_Result<T>` with IsSuccess, Data, and ErrorMessage properties
- [ ] **Centralized Error Handling**: No `MessageBox.Show` usage (only `Service_ErrorHandler.ShowUserError()` or `HandleException()`)
- [ ] **Immediate Connection Disposal**: All connections use `using` statements, `Pooling=false` in connection strings
- [ ] **Async-First I/O**: All I/O operations use async/await (no `.Result`, `.Wait()`, `.GetAwaiter().GetResult()`)
- [ ] **Theme System Integration**: Forms inherit from `ThemedForm`, controls from `ThemedUserControl` (not `Form`/`UserControl`)

### Technology Constraints

- [ ] **MySQL 5.7.24 Compatible**: No MySQL 8.0+ features (JSON functions, CTEs, window functions, CHECK constraints)
- [ ] **.NET 8.0/C# 12.0**: No .NET 9.0+ or C# 13.0+ features

### Quality Standards

- [ ] **XML Documentation**: All public classes, methods, properties have XML docs (`/// <summary>`)
- [ ] **Region Organization**: All C# files use standard #region structure (Fields, Properties, Constructors, Methods, Events, Helpers, Cleanup)
- [ ] **Structured Logging**: All logging uses `LoggingUtility.Log()` (no `Console.WriteLine()` in production code)

## Testing

- [ ] Unit tests added/updated (if applicable)
- [ ] Integration tests added/updated (if applicable)
- [ ] All tests passing locally
- [ ] Manual testing performed

### Test Evidence

<!-- Describe testing performed or provide screenshots/logs -->

## Automated Checks

- [ ] Constitution compliance validation passed: `.specify/scripts/powershell/validate-constitution-compliance.ps1`
- [ ] Build succeeded: `dotnet build MTM_WIP_Application_Winforms.csproj`
- [ ] No new compiler warnings introduced

## Documentation

- [ ] Code comments added for complex logic
- [ ] README updated (if needed)
- [ ] Architectural exceptions documented (if applicable)
- [ ] RELEASE_NOTES.json updated (if user-facing changes)

## Screenshots (if UI changes)

<!-- Add screenshots or GIFs demonstrating UI changes -->

## Deployment Notes

<!-- Any special deployment steps or database migrations required? -->

## Reviewer Checklist

**For Reviewers:**

- [ ] Code follows architectural boundaries (Forms → DAOs → Database)
- [ ] Error handling uses Service_ErrorHandler consistently
- [ ] Database access patterns match constitution requirements
- [ ] Theme integration appears correct (for UI changes)
- [ ] No architectural violations introduced
- [ ] Code is maintainable and well-documented
- [ ] Changes align with feature specification (if feature work)

## Additional Notes

<!-- Any additional context, concerns, or discussion points -->
