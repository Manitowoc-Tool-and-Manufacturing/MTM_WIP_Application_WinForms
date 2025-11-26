# MySQL Database Schema: Tables

This document outlines the table structure of the `mtm_wip_application_winforms` database.

## Inventory & Transactions

### `inv_inventory`
Stores current inventory items.
- **ID**: `int(11)` (PK, Auto Increment)
- **PartID**: `varchar(300)` (Indexed)
- **Location**: `varchar(100)`
- **Operation**: `varchar(100)` (Indexed)
- **Quantity**: `int(11)`
- **ItemType**: `varchar(100)` (Default: 'WIP')
- **ReceiveDate**: `datetime` (Indexed, Default: CURRENT_TIMESTAMP)
- **LastUpdated**: `datetime` (Default: CURRENT_TIMESTAMP, On Update: CURRENT_TIMESTAMP)
- **User**: `varchar(100)`
- **BatchNumber**: `varchar(300)`
- **Notes**: `varchar(1000)`
- **ColorCode**: `varchar(50)` (Indexed, Default: 'UNKNOWN')
- **WorkOrder**: `varchar(10)` (Indexed, Default: 'UNKNOWN')

### `inv_inventory_batch_seq`
Sequence table for batch numbers.
- **last_batch_number**: `bigint(20)` (PK)
- **current_match**: `int(11)`

### `inv_transaction`
History of inventory movements (IN, OUT, TRANSFER).
- **ID**: `int(11)` (PK, Auto Increment)
- **TransactionType**: `enum('IN','OUT','TRANSFER')`
- **BatchNumber**: `varchar(300)`
- **PartID**: `varchar(300)` (Indexed)
- **FromLocation**: `varchar(100)`
- **ToLocation**: `varchar(100)`
- **Operation**: `varchar(100)`
- **Quantity**: `int(11)`
- **Notes**: `varchar(1000)`
- **ColorCode**: `varchar(50)` (Default: 'Unknown')
- **WorkOrder**: `varchar(10)` (Default: 'Unknown')
- **User**: `varchar(100)` (Indexed)
- **ItemType**: `varchar(100)` (Default: 'WIP')
- **ReceiveDate**: `datetime` (Indexed, Default: CURRENT_TIMESTAMP)

## Master Data

### `md_part_ids`
Master list of parts.
- **ID**: `int(11)` (PK, Auto Increment)
- **PartID**: `varchar(300)` (Unique)
- **Customer**: `varchar(300)`
- **Description**: `varchar(300)`
- **IssuedBy**: `varchar(100)`
- **RequiresColorCode**: `tinyint(1)` (Indexed, Default: 0)
- **ItemType**: `varchar(100)`
- **Operations**: `json`

### `md_locations`
Master list of locations.
- **ID**: `int(11)` (PK, Auto Increment)
- **Location**: `varchar(100)` (Unique)
- **Building**: `varchar(100)` (Default: 'Expo')
- **IssuedBy**: `varchar(100)`

### `md_operation_numbers`
Master list of operations.
- **ID**: `int(11)` (PK, Auto Increment)
- **Operation**: `varchar(100)` (Unique)
- **IssuedBy**: `varchar(100)`

### `md_item_types`
Master list of item types.
- **ID**: `int(11)` (PK, Auto Increment)
- **ItemType**: `varchar(100)` (Unique)
- **IssuedBy**: `varchar(100)`

### `md_color_codes`
Master list of color codes.
- **ColorCode**: `varchar(50)` (PK)
- **IsUserDefined**: `tinyint(1)` (Indexed, Default: 0)
- **CreatedDate**: `datetime` (Default: CURRENT_TIMESTAMP)

## Users & Settings

### `usr_users`
User accounts and basic preferences.
- **ID**: `int(11)` (PK, Auto Increment)
- **User**: `varchar(100)` (Unique)
- **Full Name**: `varchar(200)`
- **Shift**: `varchar(50)` (Default: '1')
- **VitsUser**: `tinyint(1)` (Default: 0)
- **Pin**: `varchar(50)`
- **LastShownVersion**: `varchar(50)` (Default: '0.0.0.0')
- **HideChangeLog**: `varchar(50)` (Default: 'false')

### `usr_settings`
JSON-based user settings storage.
- **UserId**: `varchar(64)` (PK)
- **SettingsJson**: `json`
- **UpdatedAt**: `datetime` (Default: CURRENT_TIMESTAMP, On Update: CURRENT_TIMESTAMP)

### `usr_dgv_settings`
DataGridView layout settings per user.
- **UserId**: `varchar(100)` (PK)
- **DgvName**: `varchar(100)` (PK)
- **SettingsJson**: `json`
- **UpdatedAt**: `datetime` (Default: CURRENT_TIMESTAMP, On Update: CURRENT_TIMESTAMP)

### `usr_user_shortcuts`
User-defined keyboard shortcuts.
- **UserName**: `varchar(50)` (PK)
- **ShortcutName**: `varchar(100)` (PK)
- **CustomKeys**: `int(11)`
- **LastModified**: `datetime` (Default: CURRENT_TIMESTAMP, On Update: CURRENT_TIMESTAMP)

### `sys_roles`
System roles definition.
- **ID**: `int(11)` (PK, Auto Increment)
- **RoleName**: `varchar(50)` (Unique)
- **Description**: `varchar(255)`
- **Permissions**: `varchar(1000)`
- **IsSystem**: `tinyint(1)` (Default: 0)
- **CreatedBy**: `varchar(100)`
- **CreatedAt**: `datetime` (Default: CURRENT_TIMESTAMP)

### `sys_user_roles`
Mapping of users to roles.
- **UserID**: `int(11)` (PK)
- **RoleID**: `int(11)` (PK)
- **AssignedBy**: `varchar(100)`
- **AssignedAt**: `datetime` (Default: CURRENT_TIMESTAMP)

## System & Logs

### `app_themes`
Application themes.
- **ThemeName**: `varchar(100)` (PK)
- **SettingsJson**: `json`

### `error_reports`
Detailed error reports.
- **ReportID**: `int(11)` (PK, Auto Increment)
- **ReportDate**: `datetime` (Indexed)
- **UserName**: `varchar(100)` (Indexed)
- **MachineName**: `varchar(200)` (Indexed)
- **AppVersion**: `varchar(50)`
- **ErrorType**: `varchar(255)`
- **ErrorSummary**: `text`
- **UserNotes**: `text`
- **TechnicalDetails**: `text`
- **CallStack**: `text`
- **Status**: `enum('New','Reviewed','Resolved')` (Indexed, Default: 'New')
- **ReviewedBy**: `varchar(100)`
- **ReviewedDate**: `datetime`
- **DeveloperNotes**: `text`

### `log_error`
Application error logs.
- **ID**: `int(11)` (PK, Auto Increment)
- **User**: `varchar(100)` (Indexed)
- **Severity**: `enum('Information','Warning','Error','Critical','High')` (Indexed, Default: 'Error')
- **ErrorType**: `varchar(100)` (Indexed)
- **ErrorMessage**: `text`
- **StackTrace**: `text`
- **ModuleName**: `varchar(200)`
- **MethodName**: `varchar(200)`
- **AdditionalInfo**: `text`
- **MachineName**: `varchar(100)`
- **OSVersion**: `varchar(100)`
- **AppVersion**: `varchar(50)`
- **ErrorTime**: `datetime` (Indexed, Default: CURRENT_TIMESTAMP)

### `log_changelog`
Application changelog.
- **Version**: `varchar(50)` (PK)
- **Notes**: `longtext`

### `sys_last_10_transactions`
Cache for user's recent transactions (Quick Buttons).
- **id**: `int(11)` (PK, Auto Increment)
- **in_id**, **in_part**, **in_loc**, **in_batch**
- **out_id**, **out_part**, **out_loc**, **out_batch**
- **matched_at**: `timestamp` (Default: CURRENT_TIMESTAMP)

### `sys_shortcuts`
System default shortcuts.
- **ShortcutName**: `varchar(100)` (PK)
- **ShortcutKeys**: `int(11)`
- **Description**: `varchar(255)`
- **Category**: `varchar(50)`
- **LastModified**: `datetime` (Default: CURRENT_TIMESTAMP, On Update: CURRENT_TIMESTAMP)

### `sys_parameter_prefix_overrides`
Configuration for stored procedure parameter naming overrides.
- **OverrideId**: `int(11)` (PK, Auto Increment)
- **ProcedureName**: `varchar(128)` (Indexed)
- **ParameterName**: `varchar(128)`
- **OverridePrefix**: `varchar(10)`
- **Reason**: `varchar(500)`
- **CreatedBy**: `varchar(50)`
- **CreatedDate**: `datetime` (Default: CURRENT_TIMESTAMP)
- **ModifiedBy**: `varchar(50)`
- **ModifiedDate**: `datetime`
- **IsActive**: `tinyint(1)` (Indexed, Default: 1)
