# NuGet Packages Reference

This document lists all NuGet packages used in MTM_WIP_Application_Winforms with their purposes. 

## Database Connectivity

| Package | Version | Purpose |
|---------|---------|---------|
| **MySql.Data** | 9.4.0 | Primary MySQL database connector (targeting MySQL 5.7.24 server) |
| **Microsoft.Data.SqlClient** | 6.1.3 | Modern SQL Server data provider |
| **System.Data.SqlClient** | 4.9.0 | Legacy SQL Server data provider (compatibility) |

⚠️ **Important**: MySQL 5.7.24 limitations:
- No CTEs (Common Table Expressions)
- No Window Functions
- No JSON functions (limited support)
- No generated columns

## Dependency Injection & Logging

| Package | Version | Purpose |
|---------|---------|---------|
| **Microsoft.Extensions.DependencyInjection** | 8.0.0 | IoC container for service management |
| **Microsoft.Extensions.Logging** | 8.0.0 | Logging abstractions |
| **Microsoft.Extensions.Logging.Console** | 8.0.0 | Console logging provider |

## Excel Operations

| Package | Version | Purpose |
|---------|---------|---------|
| **ClosedXML** | 0.105.0 | Create, read, and manipulate Excel files (.xlsx) |

## Web & UI

| Package | Version | Purpose |
|---------|---------|---------|
| **Microsoft.Web.WebView2** | 1.0.2792.45 | Embedded Chromium-based web browser control |

## Utilities

| Package | Version | Purpose |
|---------|---------|---------|
| **Newtonsoft.Json** | 13.0.4 | JSON serialization/deserialization |
| **System.Configuration.ConfigurationManager** | 8.0.1 | App.config and configuration file management |

## AI Agent Notes

- All packages target **.NET 8.0**
- WinForms project type (not WPF, not Blazor)
- MySQL 5.7.24 server version enforced
- Use `MySql.Data` 9.4.0 for all MySQL operations
- Prefer `Microsoft.Data.SqlClient` over `System.Data.SqlClient` for new code