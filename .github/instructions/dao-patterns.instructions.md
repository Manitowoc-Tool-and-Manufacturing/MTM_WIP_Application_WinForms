# DAO Patterns & Standards

## Table of Contents
- **Dependency Injection Strategy**: Rules for Legacy (static) vs New (DI) DAOs.
- **Standard DAO Pattern**: Code structure for Interfaces and Implementations.
- **Method Standards**: Return types (`Model_Dao_Result`), Async/Await rules, and Helper usage.
- **Error Handling**: How to handle exceptions, logging, and UI notifications.
- **Example Implementation**: A complete code example of a compliant DAO method.

## Dependency Injection Strategy (Hybrid)

**Policy**:
- **Legacy DAOs**: Keep as `static` classes (non-injected). Do NOT refactor existing static DAOs to DI unless explicitly requested.
- **New DAOs**: MUST be designed for Dependency Injection.
  - Define an interface (e.g., `IInventoryDao`).
  - Implement the interface in a non-static class.
  - Register in DI container.

## Standard DAO Pattern

### Class Structure (New/DI)

```csharp
public interface IDao_Entity
{
    Task<Model_Dao_Result<DataTable>> GetAllAsync();
}

public class Dao_Entity : IDao_Entity
{
    public async Task<Model_Dao_Result<DataTable>> GetAllAsync()
    {
        // Implementation
    }
}
```

### Class Structure (Legacy/Static)

```csharp
public static class Dao_LegacyEntity
{
    public static async Task<Model_Dao_Result<DataTable>> GetAllAsync()
    {
        // Implementation
    }
}
```

## Method Standards

1.  **Return Type**: ALWAYS return `Model_Dao_Result<T>`.
    -   `Model_Dao_Result<DataTable>` for queries returning rows.
    -   `Model_Dao_Result<T>` for scalar values.
    -   `Model_Dao_Result` (non-generic) for void operations (Insert/Update/Delete).
2.  **Async/Await**: ALL database methods must be `async Task`.
    -   **Forbidden**: `bool useAsync` parameter (Legacy artifact, do not use in new code).
3.  **Helper Usage**: ALWAYS use `Helper_Database_StoredProcedure`.
    -   `ExecuteDataTableWithStatusAsync`
    -   `ExecuteScalarWithStatusAsync`
    -   `ExecuteNonQueryWithStatusAsync`
4.  **Parameters**:
    -   Use `Dictionary<string, object>` for parameters.
    -   Do NOT include `p_` prefix in keys (Helper adds them automatically).

## Error Handling

-   **Internal Errors**: Catch exceptions and return `Model_Dao_Result.Failure`.
-   **Logging**: Log database errors via `LoggingUtility.LogDatabaseError(ex)`.
-   **UI**: Do NOT show MessageBoxes in DAOs. Return the error result, and let the UI/Service layer handle the display via `Service_ErrorHandler`.

## Example Implementation

```csharp
public async Task<Model_Dao_Result<DataTable>> GetItemsAsync(string category)
{
    try
    {
        var parameters = new Dictionary<string, object>
        {
            { "Category", category }
        };

        return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            "inv_items_GetByCategory",
            parameters);
    }
    catch (Exception ex)
    {
        LoggingUtility.LogDatabaseError(ex);
        return Model_Dao_Result<DataTable>.Failure($"Failed to get items: {ex.Message}", ex);
    }
}
```
