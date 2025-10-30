# Database and Testing Requirements Summary

**Feature**: Comprehensive Database Layer Refactor  
**Branch**: `002-comprehensive-database-layer`  
**Last Updated**: 2025-10-14  
**Purpose**: Complete reference for database setup, stored procedures, and testing infrastructure required for the database layer refactor

---

## Table of Contents

1. [Database Requirements](#database-requirements)
2. [Stored Procedure Requirements](#stored-procedure-requirements)
3. [Test Database Setup](#test-database-setup)
4. [Integration Testing Infrastructure](#integration-testing-infrastructure)
5. [Testing Checklist](#testing-checklist)
6. [Quick Reference](#quick-reference)

---

## Database Requirements

### Production Database

**Name**: `MTM_WIP_Application_Winforms`  
**Version**: MySQL 5.7.24+ (MAMP compatible)  
**Purpose**: Primary production database for manufacturing operations

**Connection Configuration**:

```
Server: localhost
Port: 3306
Database: MTM_WIP_Application_Winforms
Username: root
Password: root
Connection String: Server=localhost;Port=3306;Database=MTM_WIP_Application_Winforms;User=root;Password=root;SslMode=none;AllowPublicKeyRetrieval=true;MinPoolSize=5;MaxPoolSize=100;ConnectionTimeout=30;
```

**Connection Pooling**:

-   **MinPoolSize**: 5 (warm connections always available)
-   **MaxPoolSize**: 100 (prevent connection exhaustion)
-   **ConnectionTimeout**: 30 seconds (align with command timeout)

### Test Database

**Name**: `mtm_wip_application_winform_test`  
**Version**: MySQL 5.7.24+  
**Purpose**: Isolated integration testing environment

**Requirements**:

-   ✅ Schema-only copy of production database (no production data)
-   ✅ Same stored procedures as production
-   ✅ Seed data for master tables only (parts, locations, operations, users)
-   ✅ NO transactional data pre-populated (tests insert within transaction scope)
-   ✅ Each developer maintains local test database

**Creation Steps**:

```sql
-- 1. Create test database
CREATE DATABASE mtm_wip_application_winform_test;

-- 2. Import schema from production backup
USE mtm_wip_application_winform_test;
SOURCE Database/CurrentDatabase/MTM_WIP_Application_Winforms.sql;

-- 3. Insert minimal seed data for master tables
-- (locations, operations, item types, test users)
INSERT INTO location (LocationCode, LocationName, IsActive) VALUES
('FLOOR', 'Shop Floor', 1),
('RECEIVING', 'Receiving Area', 1),
('SHIPPING', 'Shipping Area', 1),
('STORAGE', 'Storage Area', 1);

INSERT INTO operation (OperationCode, OperationName, IsActive) VALUES
('90', 'Assembly', 1),
('100', 'Testing', 1),
('110', 'Packaging', 1);

INSERT INTO itemtype (ItemTypeCode, ItemTypeName, IsActive) VALUES
('RAW', 'Raw Material', 1),
('WIP', 'Work In Progress', 1),
('FIN', 'Finished Goods', 1);

INSERT INTO user (Username, PasswordHash, RoleID, IsActive) VALUES
('testuser', 'hash', 1, 1),
('admin', 'hash', 2, 1);
```

**Connection String**:

```csharp
var connectionString = Helper_Database_Variables.GetConnectionString(
    databaseName: Helper_Database_Variables.TestDatabaseName);
// Result: Server=localhost;Port=3306;Database=mtm_wip_application_winform_test;...
```

---

## Stored Procedure Requirements

### Standard Output Parameters (REQUIRED for ALL procedures)

Every stored procedure MUST declare these output parameters:

```sql
OUT p_Status INT,
OUT p_ErrorMsg VARCHAR(500)
```

**Status Codes**:

-   **0**: Success - operation completed successfully
-   **1**: Success with no data - query returned zero rows (still considered success)
-   **-1**: Error - operation failed (check `p_ErrorMsg` for details)

**Error Message**:

-   User-friendly description of error (no technical jargon or stack traces)
-   Empty/NULL when operation succeeds
-   Maximum 500 characters

### Parameter Naming Conventions

**MySQL Side** (stored procedure definition):

```sql
CREATE PROCEDURE inv_inventory_get_by_part(
    IN p_PartID VARCHAR(50),           -- Use p_ prefix for standard parameters
    IN p_LocationCode VARCHAR(20),
    IN p_IncludeInactive BOOLEAN,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
```

**C# Side** (DAO method parameters):

```csharp
var parameters = new Dictionary<string, object>
{
    ["PartID"] = partId,              // NO p_ prefix in C# code
    ["LocationCode"] = locationCode,  // Helper applies prefix automatically
    ["IncludeInactive"] = includeInactive
};
```

**Prefix Types**:

-   **p\_**: Standard CRUD operations (most common)
-   **in\_**: Transfer and transaction operations (special case)
-   **o\_**: Custom output parameters (rare)

**PascalCase Required**: All parameter names must use PascalCase matching C# model properties

-   ✅ Good: `PartID`, `LocationCode`, `OperationCode`
-   ❌ Bad: `partid`, `location_code`, `operationCode`

### Required Stored Procedures (60+ procedures)

**Inventory Operations** (`inv_inventory_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| inv_inventory_get_all | Get all inventory | LocationCode?, IncludeInactive | DataTable |
| inv_inventory_get_by_part | Get inventory by part | PartID, LocationCode? | DataTable |
| inv_inventory_add | Add new inventory | PartID, LocationCode, Quantity, Operation, ItemType, User | Status |
| inv_inventory_remove | Remove inventory | InventoryID, Quantity, Reason, User | Status |
| inv_inventory_transfer | Transfer between locations | InventoryID, FromLocation, ToLocation, Quantity, User | Status |
| inv_inventory_search | Search inventory | PartID?, LocationCode?, Operation?, DateRange? | DataTable |
| inv_inventory_update | Update inventory record | InventoryID, Quantity, Location, Notes, User | Status |

**Transaction Logging** (`inv_transaction_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| inv_transaction_log | Log transaction | TransactionType, PartID, FromLocation, ToLocation, Quantity, Operation, User, Notes | Status |
| inv_transaction_get_history | Get transaction history | StartDate, EndDate, PartID?, User? | DataTable |
| inv_transaction_search | Search transactions | PartID?, LocationCode?, TransactionType?, DateRange? | DataTable |

**User Management** (`user_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| user_authenticate | Authenticate user | Username, PasswordHash | DataTable (user record) |
| user_get_all | Get all users | IncludeInactive | DataTable |
| user_get_by_id | Get user by ID | UserID | DataTable |
| user_create | Create new user | Username, PasswordHash, RoleID, Email, User | Status |
| user_update | Update user | UserID, Username, RoleID, Email, IsActive, User | Status |
| user_delete | Delete user | UserID, User | Status |

**Part Master Data** (`part_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| part_get_all | Get all parts | IncludeInactive | DataTable |
| part_get_by_id | Get part by ID | PartID | DataTable |
| part_create | Create new part | PartNumber, PartName, Description, User | Status |
| part_update | Update part | PartID, PartNumber, PartName, Description, IsActive, User | Status |
| part_delete | Delete part | PartID, User | Status |
| part_search | Search parts | PartNumber?, PartName?, Description? | DataTable |

**Location Master Data** (`location_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| location_get_all | Get all locations | IncludeInactive | DataTable |
| location_get_by_code | Get location by code | LocationCode | DataTable |
| location_create | Create new location | LocationCode, LocationName, Description, User | Status |
| location_update | Update location | LocationCode, LocationName, Description, IsActive, User | Status |
| location_delete | Delete location | LocationCode, User | Status |

**Operation Master Data** (`operation_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| operation_get_all | Get all operations | IncludeInactive | DataTable |
| operation_get_by_code | Get operation by code | OperationCode | DataTable |
| operation_create | Create new operation | OperationCode, OperationName, Description, User | Status |
| operation_update | Update operation | OperationCode, OperationName, Description, IsActive, User | Status |
| operation_delete | Delete operation | OperationCode, User | Status |

**ItemType Master Data** (`itemtype_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| itemtype_get_all | Get all item types | IncludeInactive | DataTable |
| itemtype_create | Create new item type | ItemTypeCode, ItemTypeName, Description, User | Status |
| itemtype_update | Update item type | ItemTypeCode, ItemTypeName, Description, IsActive, User | Status |
| itemtype_delete | Delete item type | ItemTypeCode, User | Status |

**Quick Buttons** (`quickbutton_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| quickbutton_get_by_user | Get user's quick buttons | UserID | DataTable |
| quickbutton_save | Save quick button | UserID, ButtonIndex, PartID, LocationCode, Operation, User | Status |
| quickbutton_delete | Delete quick button | QuickButtonID, User | Status |

**History Queries** (`inv_history_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| inv_history_inventory | Get inventory history | PartID?, LocationCode?, StartDate, EndDate | DataTable |
| inv_history_remove | Get removal history | PartID?, StartDate, EndDate | DataTable |
| inv_history_transfer | Get transfer history | PartID?, StartDate, EndDate | DataTable |

**Error Logging** (`log_error_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| log_error_insert | Log error to database | User, Severity, ErrorType, ErrorMessage, StackTrace, MethodName, MachineName, OSVersion, AppVersion | Status |
| log_error_get_unique | Get unique errors | StartDate?, EndDate? | DataTable |
| log_error_get_all | Get all errors | StartDate?, EndDate? | DataTable |
| log_error_get_by_user | Get errors by user | UserID, StartDate?, EndDate? | DataTable |
| log_error_delete_by_id | Delete error by ID | ErrorID, User | Status |
| log_error_delete_by_date | Delete errors by date range | StartDate, EndDate, User | Status |

**System Metadata** (`system_*`):
| Procedure Name | Purpose | Input Parameters | Output |
|----------------|---------|------------------|--------|
| system_get_version | Get application version | (none) | Scalar (string) |
| system_check_connectivity | Validate database connection | (none) | Status |
| system_get_user_access_type | Get user access level | UserID | DataTable |
| system_set_user_access_type | Set user access level | UserID, AccessType, User | Status |
| system_get_themes | Get available UI themes | (none) | DataTable |
| system_get_user_id_by_name | Get user ID by username | Username | Scalar (int) |
| system_get_role_id_by_name | Get role ID by role name | RoleName | Scalar (int) |

### Stored Procedure Template

**For procedures returning data (SELECT)**:

```sql
CREATE PROCEDURE [procedure_name](
    IN p_Parameter1 VARCHAR(50),
    IN p_Parameter2 INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Validation
    IF p_Parameter1 IS NULL OR p_Parameter1 = '' THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Parameter1 is required';
        ROLLBACK;
    ELSE
        -- Perform query
        SELECT * FROM [table_name]
        WHERE [column] = p_Parameter1
          AND [column2] = p_Parameter2;

        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
        COMMIT;
    END IF;
END;
```

**For procedures modifying data (INSERT/UPDATE/DELETE)**:

```sql
CREATE PROCEDURE [procedure_name](
    IN p_Parameter1 VARCHAR(50),
    IN p_Parameter2 INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Validation
    IF p_Parameter1 IS NULL THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Parameter1 is required';
        ROLLBACK;
    ELSE
        -- Perform modification
        INSERT INTO [table_name] ([columns])
        VALUES (p_Parameter1, p_Parameter2);

        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
        COMMIT;
    END IF;
END;
```

---

## Test Database Setup

### Step-by-Step Setup Guide

**1. Install MySQL 5.7.24+ (MAMP Recommended)**

Download and install MAMP from https://www.mamp.info/

**2. Start MySQL Server**

```bash
# MAMP default configuration
# Server: localhost
# Port: 3306
# Root credentials: root/root
```

**3. Create Test Database**

```sql
-- Connect to MySQL server
mysql -u root -proot

-- Create test database
CREATE DATABASE mtm_wip_application_winform_test
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_general_ci;

-- Verify creation
SHOW DATABASES LIKE 'mtm_wip_application_winform%';
```

**4. Import Schema**

```powershell
# From repository root
cd Database/CurrentDatabase

# Import schema (Windows PowerShell)
Get-Content MTM_WIP_Application_Winforms.sql | mysql -u root -proot mtm_wip_application_winform_test

# Alternative: Import from MySQL client
mysql -u root -proot mtm_wip_application_winform_test < MTM_WIP_Application_Winforms.sql
```

**5. Import Stored Procedures**

```powershell
# Import each stored procedure file
cd Database/CurrentStoredProcedures

Get-ChildItem *.sql | ForEach-Object {
    Write-Host "Importing $_"
    Get-Content $_.FullName | mysql -u root -proot mtm_wip_application_winform_test
}
```

**6. Insert Seed Data**

```sql
USE mtm_wip_application_winform_test;

-- Locations (required for inventory operations)
INSERT INTO location (LocationCode, LocationName, Description, IsActive, CreatedBy, CreatedDate)
VALUES
    ('FLOOR', 'Shop Floor', 'Manufacturing floor area', 1, 'SYSTEM', NOW()),
    ('RECEIVING', 'Receiving Area', 'Incoming shipments', 1, 'SYSTEM', NOW()),
    ('SHIPPING', 'Shipping Area', 'Outbound shipments', 1, 'SYSTEM', NOW()),
    ('STORAGE', 'Storage Area', 'Long-term storage', 1, 'SYSTEM', NOW()),
    ('INSPECTION', 'Inspection Area', 'Quality inspection', 1, 'SYSTEM', NOW());

-- Operations (required for manufacturing workflow)
INSERT INTO operation (OperationCode, OperationName, Description, IsActive, CreatedBy, CreatedDate)
VALUES
    ('90', 'Assembly', 'Product assembly', 1, 'SYSTEM', NOW()),
    ('100', 'Testing', 'Quality testing', 1, 'SYSTEM', NOW()),
    ('110', 'Packaging', 'Final packaging', 1, 'SYSTEM', NOW()),
    ('10', 'Raw Material', 'Raw material receiving', 1, 'SYSTEM', NOW()),
    ('20', 'Cutting', 'Material cutting', 1, 'SYSTEM', NOW()),
    ('30', 'Welding', 'Metal welding', 1, 'SYSTEM', NOW());

-- Item Types (required for inventory classification)
INSERT INTO itemtype (ItemTypeCode, ItemTypeName, Description, IsActive, CreatedBy, CreatedDate)
VALUES
    ('RAW', 'Raw Material', 'Raw materials and components', 1, 'SYSTEM', NOW()),
    ('WIP', 'Work In Progress', 'Partially completed items', 1, 'SYSTEM', NOW()),
    ('FIN', 'Finished Goods', 'Completed products', 1, 'SYSTEM', NOW()),
    ('TOOL', 'Tooling', 'Manufacturing tools and fixtures', 1, 'SYSTEM', NOW());

-- Test Users (required for authentication tests)
INSERT INTO user (Username, PasswordHash, RoleID, Email, IsActive, CreatedBy, CreatedDate)
VALUES
    ('testuser', 'test_hash_placeholder', 1, 'test@example.com', 1, 'SYSTEM', NOW()),
    ('admin', 'admin_hash_placeholder', 2, 'admin@example.com', 1, 'SYSTEM', NOW()),
    ('operator', 'operator_hash_placeholder', 3, 'operator@example.com', 1, 'SYSTEM', NOW());

-- Roles (required for user management)
INSERT INTO role (RoleName, Description, IsActive, CreatedBy, CreatedDate)
VALUES
    ('User', 'Standard user access', 1, 'SYSTEM', NOW()),
    ('Admin', 'Administrator access', 1, 'SYSTEM', NOW()),
    ('Operator', 'Shop floor operator', 1, 'SYSTEM', NOW());

-- Verify seed data
SELECT 'Locations' AS TableName, COUNT(*) AS RowCount FROM location
UNION ALL
SELECT 'Operations', COUNT(*) FROM operation
UNION ALL
SELECT 'ItemTypes', COUNT(*) FROM itemtype
UNION ALL
SELECT 'Users', COUNT(*) FROM user
UNION ALL
SELECT 'Roles', COUNT(*) FROM role;
```

**7. Verify INFORMATION_SCHEMA Access**

```sql
-- Test parameter cache query
SELECT
    ROUTINE_NAME,
    PARAMETER_NAME,
    PARAMETER_MODE,
    DATA_TYPE
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE ROUTINE_SCHEMA = 'mtm_wip_application_winform_test'
  AND ROUTINE_TYPE = 'PROCEDURE'
ORDER BY ROUTINE_NAME, ORDINAL_POSITION
LIMIT 10;

-- Should return stored procedure parameters
-- If empty, check user permissions for INFORMATION_SCHEMA access
```

**8. Update C# Configuration**

**File**: `Helpers/Helper_Database_Variables.cs`

```csharp
public static class Helper_Database_Variables
{
    // Test database constant
    public const string TestDatabaseName = "mtm_wip_application_winform_test";

    // Environment-aware database selection
    public static string DatabaseName =>
        Debugger.IsAttached
            ? "mtm_wip_application_winforms_test"  // Development
            : "MTM_WIP_Application_Winforms";      // Production

    // Get connection string with specific database
    public static string GetConnectionString(string? databaseName = null)
    {
        var dbName = databaseName ?? DatabaseName;
        return $"Server=localhost;Port=3306;Database={dbName};User=root;Password=root;" +
               $"SslMode=none;AllowPublicKeyRetrieval=true;MinPoolSize=5;MaxPoolSize=100;ConnectionTimeout=30;";
    }
}
```

---

## Integration Testing Infrastructure

### Test Project Structure

```
Tests/
├── Integration/
│   ├── BaseIntegrationTest.cs          # Base class with transaction setup/cleanup
│   ├── Dao_System_Tests.cs             # System DAO tests
│   ├── Dao_ErrorLog_Tests.cs           # Error logging tests
│   ├── Dao_Inventory_Tests.cs          # Inventory DAO tests
│   ├── Dao_Transactions_Tests.cs       # Transaction DAO tests
│   ├── Dao_History_Tests.cs            # History DAO tests
│   ├── Dao_User_Tests.cs               # User DAO tests
│   ├── Dao_Part_Tests.cs               # Part DAO tests
│   ├── Dao_Location_Tests.cs           # Location DAO tests
│   ├── Dao_Operation_Tests.cs          # Operation DAO tests
│   ├── Dao_ItemType_Tests.cs           # ItemType DAO tests
│   ├── Dao_QuickButtons_Tests.cs       # QuickButtons DAO tests
│   ├── Helper_Database_StoredProcedure_Tests.cs  # Helper tests
│   ├── ConnectionPooling_Tests.cs      # Connection pool tests
│   ├── TransactionManagement_Tests.cs  # Transaction tests
│   ├── ErrorLogging_Tests.cs           # Error logging tests
│   ├── ValidationErrors_Tests.cs       # Validation tests
│   ├── ErrorCooldown_Tests.cs          # Error cooldown tests
│   ├── StoredProcedureValidation_Tests.cs  # Procedure validation
│   ├── ParameterNaming_Tests.cs        # Parameter naming tests
│   ├── PerformanceMonitoring_Tests.cs  # Performance tests
│   └── ConcurrentOperations_Tests.cs   # Concurrency tests
└── MTM_WIP_Application_Winforms.Tests.csproj
```

### Base Integration Test Class

**File**: `Tests/Integration/BaseIntegrationTest.cs`

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace MTM_WIP_Application_Winforms.Tests.Integration
{
    /// <summary>
    /// Base class for integration tests providing transaction isolation.
    /// </summary>
    public abstract class BaseIntegrationTest
    {
        protected MySqlConnection? Connection { get; private set; }
        protected MySqlTransaction? Transaction { get; private set; }
        protected string ConnectionString { get; private set; } = string.Empty;

        [TestInitialize]
        public async Task BaseSetup()
        {
            // Use test database
            ConnectionString = Helper_Database_Variables.GetConnectionString(
                databaseName: Helper_Database_Variables.TestDatabaseName);

            // Open connection
            Connection = new MySqlConnection(ConnectionString);
            await Connection.OpenAsync();

            // Begin transaction for test isolation
            Transaction = await Connection.BeginTransactionAsync();

            // Call derived class setup
            await OnSetupAsync();
        }

        [TestCleanup]
        public async Task BaseCleanup()
        {
            try
            {
                // Call derived class cleanup
                await OnCleanupAsync();
            }
            finally
            {
                // Rollback transaction (discards all test changes)
                if (Transaction != null)
                {
                    await Transaction.RollbackAsync();
                    await Transaction.DisposeAsync();
                }

                // Close connection
                if (Connection != null)
                {
                    await Connection.CloseAsync();
                    await Connection.DisposeAsync();
                }
            }
        }

        /// <summary>
        /// Override in derived class for test-specific setup.
        /// </summary>
        protected virtual Task OnSetupAsync() => Task.CompletedTask;

        /// <summary>
        /// Override in derived class for test-specific cleanup.
        /// </summary>
        protected virtual Task OnCleanupAsync() => Task.CompletedTask;

        /// <summary>
        /// Insert test data within current transaction.
        /// </summary>
        protected async Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object>? parameters = null)
        {
            using var command = new MySqlCommand(sql, Connection, Transaction);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            return await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Query test data within current transaction.
        /// </summary>
        protected async Task<DataTable> ExecuteQueryAsync(string sql, Dictionary<string, object>? parameters = null)
        {
            using var command = new MySqlCommand(sql, Connection, Transaction);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            var dataTable = new DataTable();
            using var adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataTable);

            return dataTable;
        }
    }
}
```

### Example Integration Test

**File**: `Tests/Integration/Dao_Inventory_Tests.cs`

```csharp
[TestClass]
public class Dao_Inventory_Tests : BaseIntegrationTest
{
    protected override async Task OnSetupAsync()
    {
        // Insert test inventory data within transaction
        await ExecuteNonQueryAsync(@"
            INSERT INTO inventory (PartID, LocationCode, Quantity, Operation, ItemType, CreatedBy, CreatedDate)
            VALUES
                (@PartID1, @Location1, @Qty1, @Op1, @Type1, @User, @Date),
                (@PartID2, @Location2, @Qty2, @Op2, @Type2, @User, @Date)",
            new Dictionary<string, object>
            {
                ["@PartID1"] = "TEST-PART-001",
                ["@Location1"] = "FLOOR",
                ["@Qty1"] = 100,
                ["@Op1"] = "90",
                ["@Type1"] = "WIP",
                ["@PartID2"] = "TEST-PART-002",
                ["@Location2"] = "STORAGE",
                ["@Qty2"] = 50,
                ["@Op2"] = "100",
                ["@Type2"] = "FIN",
                ["@User"] = "testuser",
                ["@Date"] = DateTime.Now
            });
    }

    [TestMethod]
    public async Task GetAllInventoryAsync_ReturnsAllRecords()
    {
        // Act
        var result = await Dao_Inventory.GetAllInventoryAsync();

        // Assert
        Assert.IsTrue(result.IsSuccess, "Operation should succeed");
        Assert.IsNotNull(result.Data, "Data should not be null");
        Assert.IsTrue(result.Data.Rows.Count >= 2, "Should return test inventory records");
    }

    [TestMethod]
    public async Task AddInventoryAsync_ValidData_Succeeds()
    {
        // Arrange
        var partId = "TEST-PART-NEW";
        var location = "FLOOR";
        var quantity = 25;

        // Act
        var result = await Dao_Inventory.AddInventoryAsync(partId, location, quantity, "90", "RAW");

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Add should succeed: {result.Message}");

        // Verify data was added
        var verify = await ExecuteQueryAsync(
            "SELECT * FROM inventory WHERE PartID = @PartID",
            new Dictionary<string, object> { ["@PartID"] = partId });

        Assert.AreEqual(1, verify.Rows.Count, "Part should exist after add");
    }

    [TestMethod]
    public async Task TransferInventoryAsync_ValidTransfer_UpdatesBothLocations()
    {
        // Arrange
        var partId = "TEST-PART-001";
        var fromLocation = "FLOOR";
        var toLocation = "SHIPPING";
        var quantity = 10;

        // Act
        var result = await Dao_Inventory.TransferInventoryAsync(
            partId, fromLocation, toLocation, quantity);

        // Assert
        Assert.IsTrue(result.IsSuccess, $"Transfer should succeed: {result.Message}");

        // Verify source deducted
        var sourceQty = await ExecuteQueryAsync(
            "SELECT Quantity FROM inventory WHERE PartID = @PartID AND LocationCode = @Location",
            new Dictionary<string, object>
            {
                ["@PartID"] = partId,
                ["@Location"] = fromLocation
            });
        Assert.AreEqual(90, Convert.ToInt32(sourceQty.Rows[0]["Quantity"]),
            "Source should be reduced by transfer quantity");

        // Verify destination increased
        var destQty = await ExecuteQueryAsync(
            "SELECT Quantity FROM inventory WHERE PartID = @PartID AND LocationCode = @Location",
            new Dictionary<string, object>
            {
                ["@PartID"] = partId,
                ["@Location"] = toLocation
            });
        Assert.IsTrue(destQty.Rows.Count > 0, "Destination should have inventory");
    }
}
```

### Test Execution

**Run all tests**:

```powershell
dotnet test
```

**Run specific test class**:

```powershell
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests"
```

**Run with detailed output**:

```powershell
dotnet test --logger "console;verbosity=detailed"
```

---

## Testing Checklist

### Pre-Implementation Checklist

Before writing any DAO implementation code:

-   [ ] **Test database created** (`mtm_wip_application_winform_test`)
-   [ ] **Schema imported** from production backup
-   [ ] **Stored procedures imported** (all 60+ procedures)
-   [ ] **Seed data inserted** (locations, operations, item types, users, roles)
-   [ ] **INFORMATION_SCHEMA access verified** (parameter cache query works)
-   [ ] **Connection string configured** in `Helper_Database_Variables.cs`
-   [ ] **Test project created** (`Tests/MTM_WIP_Application_Winforms.Tests.csproj`)
-   [ ] **Base integration test class created** (`BaseIntegrationTest.cs`)
-   [ ] **NuGet packages installed** (MSTest, MySql.Data)

### Per User Story Testing Checklist

For each user story (US1-US5):

-   [ ] **Write tests FIRST** (TDD approach - tests should FAIL before implementation)
-   [ ] **Create test class** inheriting from `BaseIntegrationTest`
-   [ ] **Insert test data** in `OnSetupAsync()` method
-   [ ] **Test success path** with valid data
-   [ ] **Test failure path** with invalid data
-   [ ] **Test edge cases** (nulls, empty strings, boundary values)
-   [ ] **Test error handling** (connection failures, timeouts, constraint violations)
-   [ ] **Verify DaoResult pattern** (IsSuccess, Message, Data/Exception)
-   [ ] **Verify transaction rollback** (no test data persists after test completion)
-   [ ] **Run tests** and verify all FAIL before implementation
-   [ ] **Implement DAO methods** following established pattern
-   [ ] **Run tests again** and verify all PASS after implementation
-   [ ] **Manual validation** per success criteria from spec.md

### Post-Implementation Validation Checklist

After completing all user stories:

-   [ ] **All integration tests passing** (0 failures)
-   [ ] **Connection pool healthy** under concurrent load (100+ operations)
-   [ ] **Transaction rollback working** (no orphaned test data)
-   [ ] **Parameter prefix detection working** for all 60+ procedures
-   [ ] **Performance thresholds met** (Query < 500ms, Modification < 1000ms, etc.)
-   [ ] **Error logging functional** without recursive failures
-   [ ] **Stored procedure validation script passing** (0 inconsistencies)
-   [ ] **Manual validation checklist complete** (SC-001 through SC-010 from spec.md)
-   [ ] **Documentation updated** (README.md, quickstart.md, migration guide)

---

## Quick Reference

### Connection Strings

**Production**:

```
Server=localhost;Port=3306;Database=MTM_WIP_Application_Winforms;User=root;Password=root;SslMode=none;AllowPublicKeyRetrieval=true;MinPoolSize=5;MaxPoolSize=100;ConnectionTimeout=30;
```

**Test**:

```
Server=localhost;Port=3306;Database=mtm_wip_application_winform_test;User=root;Password=root;SslMode=none;AllowPublicKeyRetrieval=true;MinPoolSize=5;MaxPoolSize=100;ConnectionTimeout=30;
```

### Key Files

| File                                       | Purpose                                                       |
| ------------------------------------------ | ------------------------------------------------------------- |
| `Helper_Database_Variables.cs`             | Connection string management                                  |
| `Helper_Database_StoredProcedure.cs`       | Central stored procedure execution                            |
| `Models/Model_DaoResult.cs`                | DaoResult base class                                          |
| `Models/Model_DaoResult_Generic.cs`        | DaoResult<T> generic class                                    |
| `Models/Model_ParameterPrefixCache.cs`     | Parameter prefix cache                                        |
| `Tests/Integration/BaseIntegrationTest.cs` | Base test class with transaction isolation                    |
| `Program.cs`                               | Application startup (includes parameter cache initialization) |

### Command Quick Reference

**Create test database**:

```sql
CREATE DATABASE mtm_wip_application_winform_test;
```

**Import schema**:

```powershell
mysql -u root -proot mtm_wip_application_winform_test < Database/CurrentDatabase/MTM_WIP_Application_Winforms.sql
```

**Run tests**:

```powershell
dotnet test
```

**Verify test database**:

```sql
USE mtm_wip_application_winform_test;
SHOW TABLES;
SELECT COUNT(*) FROM location;
SELECT COUNT(*) FROM operation;
```

### Status Codes

| Code | Meaning           | DaoResult.IsSuccess |
| ---- | ----------------- | ------------------- |
| 0    | Success           | true                |
| 1    | Success (no data) | true                |
| -1   | Error             | false               |

### Performance Thresholds

| Category     | Threshold | Stored Procedure Patterns                    |
| ------------ | --------- | -------------------------------------------- |
| Query        | 500ms     | `*_get_*`, `*_search_*`, `*_retrieve_*`      |
| Modification | 1000ms    | `*_add_*`, `*_update_*`, `*_delete_*`        |
| Batch        | 5000ms    | `*_batch_*`, `*_bulk_*`, `*_import_*`        |
| Report       | 2000ms    | `*_report_*`, `*_summary_*`, `*_dashboard_*` |

### Connection Pool Configuration

| Setting           | Value      | Purpose                           |
| ----------------- | ---------- | --------------------------------- |
| MinPoolSize       | 5          | Warm connections always available |
| MaxPoolSize       | 100        | Prevent connection exhaustion     |
| ConnectionTimeout | 30 seconds | Align with command timeout        |

---

## Related Documentation

-   **Specification**: [spec.md](./spec.md) - Full feature requirements and user stories
-   **Data Model**: [data-model.md](./data-model.md) - Entity structure and relationships
-   **Research**: [research.md](./research.md) - Technical decisions and patterns
-   **Tasks**: [tasks.md](./tasks.md) - Implementation task breakdown
-   **Quickstart**: [quickstart.md](./quickstart.md) - Developer getting started guide
-   **Contracts**: [contracts/](./contracts/) - API schemas and JSON contracts
-   **Instructions**: [.github/instructions/mysql-database.instructions.md](../../.github/instructions/mysql-database.instructions.md) - MySQL best practices

---

**Document Version**: 1.0  
**Last Updated**: 2025-10-14  
**Maintained By**: Development Team
