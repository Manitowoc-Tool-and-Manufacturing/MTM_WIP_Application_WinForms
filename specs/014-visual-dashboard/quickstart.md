# Quickstart: Infor Visual Dashboard

## Prerequisites
1.  Ensure you have the `System.Data.SqlClient` NuGet package installed.
2.  Ensure you have valid Infor Visual credentials set in your `Model_Application_Variables` (or Settings form) for testing.

## Development Steps

### 1. Infrastructure Setup
1.  Create `Services/Visual/Service_VisualDatabase.cs`.
2.  Implement `IService_VisualDatabase` interface.
3.  Register the service in `Program.cs` (Dependency Injection).

### 2. UI Components
1.  Create `Controls/Shared/Control_EmptyState.cs`.
2.  Copy logic/image from `Control_AdvancedRemove` to `Control_EmptyState`.
3.  Replace usage in `Control_AdvancedRemove` with the new control (Refactor).

### 3. Dashboard Form
1.  Create `Forms/Visual/InforVisualDashboard.cs` (Inherit `ThemedForm`).
2.  Add Sidebar (Panel/Buttons) and Main Content Area (Panel).
3.  Add `DataGridView` and `Control_EmptyState` to Content Area.

### 4. SQL Assets
1.  Create `Resources/Sql/Visual/` folder.
2.  Add dummy `.sql` files for each category (to be filled by AI later).
3.  Set Build Action to **Embedded Resource**.

### 5. Integration
1.  Add "Visual" menu item to `MainForm`.
2.  Create sub-menu branches for each category under the "Visual" menu item.
3.  Wire up the click events to open `InforVisualDashboard` initialized to the selected category.

## Testing
1.  Open the Dashboard.
2.  Verify it loads without crashing (even if connection fails).
3.  Test "Empty State" by forcing a query to return 0 rows.
4.  Test "Connection Error" by providing bad credentials.
