# MySQL Database Conventions

This document outlines the naming conventions and patterns used in the `mtm_wip_application_winforms` database.

## Naming Conventions

### Tables

Tables are prefixed to indicate their functional area:

- **`inv_`**: Inventory and Transaction data (e.g., `inv_inventory`, `inv_transaction`).
- **`md_`**: Master Data (e.g., `md_part_ids`, `md_locations`).
- **`usr_`**: User-specific data and settings (e.g., `usr_users`, `usr_settings`).
- **`sys_`**: System configuration and metadata (e.g., `sys_roles`, `sys_shortcuts`).
- **`log_`**: Logging tables (e.g., `log_error`, `log_changelog`).
- **`app_`**: Application-wide configuration (e.g., `app_themes`).

### Stored Procedures

Stored procedures generally follow the pattern: `[Prefix]_[Entity]_[Action]`.

- **Prefix**: Matches the table prefix (e.g., `inv_`, `md_`, `usr_`).
- **Entity**: The specific entity being acted upon (e.g., `inventory`, `part_ids`).
- **Action**: The operation being performed (e.g., `Get_All`, `Add`, `Delete_ById`).

**Examples:**
- `inv_inventory_Get_All`
- `md_part_ids_Add_Part`
- `usr_users_Update_User`

**Exceptions:**
- Some legacy or specific reporting procedures use `sp_` prefix (e.g., `sp_error_reports_GetAll`).
- Maintenance procedures use `maint_` (e.g., `maint_transactions_RemoveDuplicates`).
- Helper queries use `query_` (e.g., `query_get_procedure_parameters`).

### Columns

- **Primary Keys**: Typically named `ID` (int, auto-increment).
- **Foreign Keys**: Often named `[Entity]ID` or just `[Entity]` (e.g., `PartID`, `UserID`).
- **Casing**: PascalCase is the standard for business data (e.g., `PartID`, `ReceiveDate`).
    - *Note*: Some system tables use snake_case (e.g., `last_batch_number`).
- **Booleans**: `tinyint(1)` is used for boolean flags (e.g., `IsActive`, `VitsUser`).
- **JSON**: The `json` type is used for flexible settings storage (`SettingsJson`).

## Standard Data Types

- **IDs**: `int(11)`
- **Short Text**: `varchar(50)` - `varchar(100)` (e.g., Usernames, Locations)
- **Long Text**: `varchar(300)` - `varchar(1000)` (e.g., Descriptions, Notes)
- **Timestamps**: `datetime` (Default: `CURRENT_TIMESTAMP`)
- **Booleans**: `tinyint(1)`
- **Quantities**: `int(11)`

## Best Practices

1.  **No Direct Table Access**: The application should interact with the database **exclusively** through stored procedures.
2.  **Parameter Naming**: Stored procedure parameters should generally match the column names they affect, sometimes prefixed with `p_` in the SQL definition (though the C# layer maps them by name).
3.  **Output Parameters**: Most procedures (especially those used by DAOs) should include `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)` to communicate success/failure back to the application.
    - `p_Status = 1`: Success
    - `p_Status = 0`: Warning / No Data
    - `p_Status = -1`: Error
4.  **Soft Deletes**: While not universal, consider using `IsActive` flags for master data instead of hard deletes where appropriate.
4.  **JSON Usage**: Use the `json` column type for user preferences and flexible configuration to avoid frequent schema changes for UI settings.
