# Task Completion Checklist

When completing a task, ensure the following:

## Before Committing Code

### 1. Build Verification
```powershell
dotnet build MTM_WIP_Application_Winforms.csproj
```
- Build must succeed with no errors
- Warnings should be reviewed and addressed if critical

### 2. Code Quality Checks

#### XML Documentation
- All public members have `<summary>` tags
- Parameters documented with `<param>`
- Return values documented with `<returns>`
- Exceptions documented with `<exception>`

#### #region Structure
- All C# files have proper #region organization:
  - Fields
  - Properties
  - Constructors
  - Methods
  - Events
  - Helpers
  - Cleanup / Dispose

#### Naming Conventions
- Classes follow naming patterns (Dao_, Service_, Helper_, etc.)
- Variables use proper casing (_camelCase for private fields, camelCase for params)
- Async methods end with "Async"

### 3. Architectural Compliance

#### DAO Pattern
- All database access goes through DAOs
- DAOs use `Helper_Database_StoredProcedure` ONLY
- All DAO methods return `Model_Dao_Result<T>`
- NO direct MySqlConnection usage

#### Error Handling
- Use `Service_ErrorHandler.HandleException()` for exceptions
- Use `Service_ErrorHandler.ShowUserError()` for user messages
- NO `MessageBox.Show()` usage anywhere

#### Theme Integration
- Forms inherit from `ThemedForm` (NOT `Form`)
- User controls inherit from `ThemedUserControl` (NOT `UserControl`)

#### Async Operations
- All I/O operations use async/await
- NO blocking database calls

### 4. Database Compliance
- All SQL in stored procedures (NO inline SQL)
- MySQL 5.7.24 compatible (NO 8.0+ features)
- Stored procedures in `Database/UpdatedStoredProcedures/`
- Parameter names: no `p_` prefix in C# (only in SQL)

### 5. Testing (if applicable)
```powershell
dotnet test
```
- All tests pass
- New features have corresponding tests

### 6. File Organization
- Code files in appropriate folders (Data/, Services/, Forms/, etc.)
- Resources properly embedded
- No orphaned files

## After Changes

### 1. Run Application
```powershell
dotnet run --project MTM_WIP_Application_Winforms.csproj
```
- Application launches without errors
- New features work as expected
- Existing features still work (regression test)

### 2. Verify Logs
- Check `%APPDATA%\MTM\Logs\` for unexpected errors
- Ensure proper log entries for new features

### 3. Git Commit
```powershell
git add .
git commit -m "{TaskID}: {Description}"
git push origin master
```

## Common Issues to Avoid

❌ Using `MessageBox.Show()` instead of `Service_ErrorHandler`
❌ Direct MySqlConnection instead of `Helper_Database_StoredProcedure`
❌ Inheriting from `Form` instead of `ThemedForm`
❌ Missing XML documentation on public members
❌ Forgetting #region organization
❌ Using MySQL 8.0+ features (JSON, CTEs, window functions)
❌ Synchronous database calls (not async/await)
❌ Inline SQL instead of stored procedures

## Constitution Compliance

Verify all "MTM WIP Application Constitution" rules:
1. ✅ Centralized Error Handling (Service_ErrorHandler only)
2. ✅ Structured Logging (CSV format via LoggingUtility)
3. ✅ Model_Dao_Result Pattern (All DAO methods)
4. ✅ Async-First (All I/O operations)
5. ✅ Stored Procedures (NO inline SQL)
6. ✅ WinForms Best Practices (InvokeRequired, disposal, themes)
