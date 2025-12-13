# Architectural Patterns and Rules

## Architectural Boundaries (NON-NEGOTIABLE)

1. **Forms → DAOs → Database** (never skip layers)
2. **NO direct database access in Forms** (go through DAOs)
3. **DAOs ONLY call Helper_Database_StoredProcedure** (no MySqlConnection)
4. **ALL database access uses stored procedures** (NO inline SQL)
5. **ALL errors use Service_ErrorHandler** (NEVER MessageBox.Show)
6. **ALL logging uses LoggingUtility** (structured CSV format)

## DAO Pattern (MANDATORY for all database access)

### Legacy DAOs (Existing)
Keep as `static` classes. Do NOT refactor unless explicitly requested.

### New DAOs
Must be instance-based and implement an interface for DI:

```csharp
// Interface for DI (New DAOs)
public interface IDao_Entity
{
    Task<Model_Dao_Result<DataTable>> GetAllAsync();
    Task<Model_Dao_Result<bool>> InsertAsync(string name);
}

// Implementation
public class Dao_Entity : IDao_Entity
{
    /// <summary>
    /// Gets all entities from database.
    /// </summary>
    /// <returns>Model_Dao_Result with DataTable of entities.</returns>
    public async Task<Model_Dao_Result<DataTable>> GetAllAsync()
    {
        return await Helper_Database_StoredProcedure
            .ExecuteDataTableWithStatusAsync(
                "md_entity_GetAll",  // Stored procedure name
                null);                // No parameters
    }
    
    /// <summary>
    /// Inserts a new entity.
    /// </summary>
    /// <param name="name">Entity name</param>
    /// <returns>Model_Dao_Result with success/failure.</returns>
    public async Task<Model_Dao_Result<bool>> InsertAsync(string name)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Name", name }  // NO p_ prefix in C# (only in SQL)
        };
        
        return await Helper_Database_StoredProcedure
            .ExecuteNonQueryWithStatusAsync(
                "md_entity_Insert",
                parameters);
    }
}
```

## Error Handling Pattern (NON-NEGOTIABLE)

```csharp
// ✅ CORRECT: Use Service_ErrorHandler
try
{
    var result = await dao.GetDataAsync();
    if (!result.IsSuccess)
    {
        Service_ErrorHandler.ShowUserError(result.ErrorMessage);
        return;
    }
    // Process result.Data
}
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(
        ex,
        Enum_ErrorSeverity.Medium,
        contextData: new Dictionary<string, object>
        {
            ["User"] = Model_Application_Variables.User,
            ["Operation"] = "GetData"
        },
        callerName: nameof(GetDataAsync),
        controlName: this.Name);
}

// ❌ WRONG: Never do this
catch (Exception ex)
{
    MessageBox.Show(ex.Message); // FORBIDDEN!
}
```

## Model_Dao_Result Pattern

ALL DAO methods must return `Model_Dao_Result<T>`:

```csharp
// Always check IsSuccess before accessing Data
var result = await dao.GetDataAsync();
if (result.IsSuccess)
{
    var data = result.Data; // Safe to access
}
else
{
    // ErrorMessage contains user-friendly message
    Service_ErrorHandler.ShowUserError(result.ErrorMessage);
}
```

## Theme Integration Pattern

```csharp
// ALL forms must inherit from ThemedForm (NOT Form!)
public partial class MyForm : ThemedForm
{
    public MyForm()
    {
        InitializeComponent();
        // Theme automatically applied by base class
    }
}

// ALL user controls must inherit from ThemedUserControl
public partial class MyControl : ThemedUserControl
{
    public MyControl()
    {
        InitializeComponent();
        // Theme automatically applied
    }
}
```

## Dependency Injection Strategy (Hybrid Approach)

- **Legacy DAOs**: Keep as `static` classes (non-injected)
- **New Components**: All NEW Services, DAOs, and Forms MUST use DI
  - Use Interfaces (`IUserService`, `IInventoryDao`)
  - Register in `Program.cs` or `Startup.cs`
  - Inject via constructor

## MySQL Constraints (5.7.24 - LEGACY)

**Forbidden MySQL 8.0+ Features:**
- ❌ JSON functions
- ❌ Common Table Expressions (CTEs)
- ❌ Window functions
- ❌ CHECK constraints

## Async-First
- ALL I/O operations must be async/await
- NO blocking database calls
- Use `ConfigureAwait(false)` where appropriate
