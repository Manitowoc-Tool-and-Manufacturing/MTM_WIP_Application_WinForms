# Data Model & Contracts: Infor Visual Dashboard

## 1. Entities

### Visual Credentials
*Stored in MySQL `usr_users` table; loaded into `Model_Application_Variables` at login*
- `VisualUserName` (string?): The SQL Server username for Infor Visual access.
- `VisualPassword` (string?): The SQL Server password for Infor Visual access.

### Dashboard Category
*Enum to manage navigation state*
```csharp
public enum Enum_VisualDashboardCategory
{
    Inventory,
    Receiving,
    Shipping,
    InventoryAuditing,
    DieToolDiscovery,
    MaterialHandlerAnalytics_General,
    MaterialHandlerAnalytics_Team
}
```

## 2. Service Interface

### IService_VisualDatabase
*Contract for the new service handling SQL Server connections.*

```csharp
public interface IService_VisualDatabase
{
    /// <summary>
    /// Tests the connection to the Infor Visual database using current credentials.
    /// </summary>
    Task<Model_Dao_Result<bool>> TestConnectionAsync();

    /// <summary>
    /// Executes a read-only query from an embedded resource file.
    /// </summary>
    /// <param name="category">The dashboard category to load query for.</param>
    /// <returns>DataTable containing the results.</returns>
    Task<Model_Dao_Result<DataTable>> GetDashboardDataAsync(Enum_VisualDashboardCategory category);
}
```

## 3. UI Components

### Control_EmptyState
*Refactored UserControl for consistent empty states.*

**Properties:**
- `Message` (string): The text to display (e.g., "No records found").
- `Image` (Image): The icon to display (defaults to `Resources.NothingFound`).
- `ActionText` (string?): Optional text for a call-to-action button.
- `Action` (Action?): Optional delegate to run when button clicked.

## 4. Database Schema (Read-Only)

*We do not control the schema, but we map to it.*

**Key Tables (Infor Visual):**
- `PART`: Part master data.
- `INVENTORY`: Current inventory levels.
- `RECEIVER`: Receiving records.
- `SHIPMENT`: Shipping records.
- `TRACE`: Audit trails.

*Note: Exact column mappings will be defined in the `[Category].instruction.md` files.*
