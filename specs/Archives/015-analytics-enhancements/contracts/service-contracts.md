# API Contracts

*Note: Since this is a WinForms application with direct database access via DAOs, "API Contracts" refer to the public interfaces of the new Services and DAOs.*

## 1. Service_UserShiftLogic

```csharp
public interface IService_UserShiftLogic
{
    /// <summary>
    /// Analyzes transaction history to calculate shift assignments for all users.
    /// </summary>
    Task<Model_Dao_Result<Dictionary<string, int>>> CalculateAllUserShiftsAsync();

    /// <summary>
    /// Retrieves full names for all users from Infor Visual database.
    /// </summary>
    Task<Model_Dao_Result<Dictionary<string, string>>> FetchUserFullNamesAsync();
    
    /// <summary>
    /// Persists shift and name data to sys_visual table.
    /// </summary>
    Task<Model_Dao_Result<bool>> SaveVisualMetadataAsync(
        Dictionary<string, int> shifts, 
        Dictionary<string, string> names);
}
```

## 2. Dao_VisualAnalytics

```csharp
public interface IDao_VisualAnalytics
{
    /// <summary>
    /// Gets the current metadata from sys_visual table.
    /// </summary>
    Task<Model_Dao_Result<Model_Visual_SysVisual>> GetSysVisualDataAsync();

    /// <summary>
    /// Updates the sys_visual table with new JSON data.
    /// </summary>
    Task<Model_Dao_Result<bool>> UpdateSysVisualDataAsync(string jsonShifts, string jsonNames);

    /// <summary>
    /// Gets raw transaction counts for material handler scoring.
    /// </summary>
    Task<Model_Dao_Result<DataTable>> GetMaterialHandlerStatsAsync(DateTime startDate, DateTime endDate);
}
```
