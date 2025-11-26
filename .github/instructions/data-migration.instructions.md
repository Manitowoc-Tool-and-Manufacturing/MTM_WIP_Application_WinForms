# Data Migration Instructions

This document outlines the strategy for migrating data from the legacy `mtm_wip_application` database to the new `mtm_wip_application_winforms` database.

## Migration Strategy

The migration will be performed using SQL scripts executed from the application via a dedicated "Migration" menu.

### 1. Schema Synchronization
Ensure the target database (`mtm_wip_application_winforms`) has the latest schema.

### 2. Master Data Migration
Migrate master data tables first to satisfy foreign key constraints (logical).

- **`md_part_ids`**: Copy all records. Set `RequiresColorCode` to 0 (default).
- **`md_locations`**: Copy all records.
- **`md_operation_numbers`**: Copy all records.
- **`md_item_types`**: Copy all records.

### 3. User Data Migration
Migrate users and transform legacy settings to the new JSON format.

- **`usr_users`**: Copy users.
- **Settings Transformation**:
    - Read `Theme_Name`, `Theme_FontSize` from legacy `usr_users`.
    - Create JSON object: `{"Theme": "...", "FontSize": ...}`.
    - Insert into `usr_settings`.

### 4. Inventory & Transaction Migration
Migrate core business data.

- **`inv_inventory`**: Copy records. Set `ColorCode` and `WorkOrder` to 'UNKNOWN'.
- **`inv_transaction`**: Copy records. Set `ColorCode` and `WorkOrder` to 'Unknown'.

### 5. System Data Migration
- **`sys_roles`**, **`sys_user_roles`**: Copy records.
- **`sys_last_10_transactions`**: Copy records.

## Migration Scripts

Scripts should be idempotent where possible (using `INSERT IGNORE` or `ON DUPLICATE KEY UPDATE`).

### Script 1: Master Data
```sql
INSERT IGNORE INTO mtm_wip_application_winforms.md_part_ids (PartID, Customer, Description, IssuedBy, ItemType, Operations)
SELECT PartID, Customer, Description, IssuedBy, ItemType, Operations FROM mtm_wip_application.md_part_ids;
-- Repeat for other md_ tables
```

### Script 2: Users & Settings
```sql
INSERT IGNORE INTO mtm_wip_application_winforms.usr_users (User, `Full Name`, Shift, VitsUser, Pin, LastShownVersion, HideChangeLog)
SELECT User, `Full Name`, Shift, VitsUser, Pin, LastShownVersion, HideChangeLog FROM mtm_wip_application.usr_users;

-- Settings migration requires application logic or complex SQL to construct JSON
```

### Script 3: Inventory
```sql
INSERT INTO mtm_wip_application_winforms.inv_inventory (PartID, Location, Operation, Quantity, ItemType, ReceiveDate, LastUpdated, User, BatchNumber, Notes, ColorCode, WorkOrder)
SELECT PartID, Location, Operation, Quantity, ItemType, ReceiveDate, LastUpdated, User, BatchNumber, Notes, 'UNKNOWN', 'UNKNOWN'
FROM mtm_wip_application.inv_inventory;
```

## Execution
The application will provide a UI to execute these scripts.
1.  **Backup**: Prompt user to backup the target database.
2.  **Execute**: Run scripts in order.
3.  **Verify**: Check row counts.
