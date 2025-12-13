# Project Overview - MTM WIP Application

## Purpose
**MTM WIP Application** is a Windows Forms-based inventory management system for Manitowoc Tool and Manufacturing. It provides real-time work-in-progress (WIP) tracking, transaction management, and reporting capabilities with MySQL backend.

## Tech Stack
- **.NET 8.0 Windows Forms** - Desktop application framework
- **C# 12.0** - Programming language
- **MySQL 5.7.24** - Database (LEGACY - NO 8.0+ features allowed)
- **MySql.Data 9.4.0** - MySQL database driver
- **Microsoft.Extensions.DependencyInjection 8.0.0** - DI container
- **ClosedXML 0.105.0** - Excel export functionality
- **Microsoft.Web.WebView2 1.0.2792.45** - HTML help viewer

## Architecture
Layered architecture with strict separation:
- **Data Layer**: DAOs using stored procedures only (via `Helper_Database_StoredProcedure`)
- **Service Layer**: Business logic and error handling (`Service_ErrorHandler`, logging)
- **Presentation Layer**: WinForms with dependency injection-based theming system
- **Database**: MySQL 5.7.24 with stored procedures

## Key Features
- Real-time inventory tracking
- Transaction management (add/remove items)
- Part and location management
- Excel reporting
- Multi-user support with themes
- Error reporting and logging
- Visual analytics dashboard

## Project Structure
```
MTM_WIP_Application_WinForms/
├── Core/                   # Core utilities and theme system
├── Data/                   # DAOs - ONLY use Helper_Database_StoredProcedure
├── Services/               # Business logic services
├── Helpers/                # Helper utilities for database, export, etc.
├── Forms/                  # WinForms forms (inherit from ThemedForm)
├── Controls/               # User controls (inherit from ThemedUserControl)
├── Models/                 # Data models, enums
├── Database/               # Stored procedures ONLY
└── Documentation/          # Help files, release notes
```

## Database
- **Production**: `mtm_wip_application_winforms` (24 tables)
- **Test**: `mtm_wip_application_winforms_test` (20 tables)
- **Server**: MAMP MySQL on localhost:3306
- **Credentials**: root/root (development)
- **Stored Procedures**: All database access via stored procedures

## Version
- **Current**: 6.4.1.0
- **Repository**: https://github.com/Dorotel/MTM_WIP_Application_Winforms
- **Maintained By**: Manitowoc Tool and Manufacturing
