# Legacy MySQL Database Conventions

This document outlines the conventions used in the legacy `mtm_wip_application` database.

## Naming Conventions

### Tables

- **`inv_`**: Inventory and Transaction data.
- **`md_`**: Master Data.
- **`usr_`**: User-specific data.
- **`sys_`**: System configuration.
- **`log_`**: Logging tables.
- **`app_`**: Application configuration.

### Stored Procedures

Follows `[Prefix]_[Entity]_[Action]`, similar to the new database.

### Columns

- **Primary Keys**: `ID` (int, auto-increment).
- **Foreign Keys**: `PartID`, `UserID`.
- **Casing**: PascalCase generally used.
- **Legacy Columns**: `usr_users` contains many columns that were moved to JSON settings in the new schema (`Theme_Name`, `WipServerAddress`, etc.).

## Key Differences from New Schema

1.  **User Settings**: Legacy uses `usr_ui_settings` and columns on `usr_users`. New schema uses `usr_settings` (JSON) and `usr_dgv_settings`.
2.  **Missing Columns**: `ColorCode` and `WorkOrder` are missing from inventory/transaction tables.
3.  **Missing Tables**: `md_color_codes`, `error_reports`, `sys_shortcuts`, `sys_parameter_prefix_overrides` do not exist in legacy.
4.  **Batch Numbers**: Legacy has complex batch number assignment procedures (`assign_BatchNumber_StepX`).
