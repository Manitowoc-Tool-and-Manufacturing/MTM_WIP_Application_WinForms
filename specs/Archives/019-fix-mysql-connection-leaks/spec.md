# Feature Specification: Fix MySQL Database Connection Leaks

**Feature Branch**: `001-fix-mysql-connection-leaks`  
**Created**: 2025-12-13  
**Status**: Draft  
**Input**: User description: "Fix critical MySQL database connection leaks causing max users reached errors. Implements proper connection disposal for ExecuteReaderAsync, disables connection pooling for immediate disposal (Pooling=false), adds connection lifecycle monitoring, and refactors architectural violations in Service_Migration, Service_Analytics, and Service_ErrorReportSync to use Helper_Database_StoredProcedure pattern."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Application Runs Without Connection Exhaustion (Priority: P1)

As a user of the MTM WIP Application, I need the application to run continuously without encountering "max users reached" MySQL errors, so that I can perform inventory operations reliably throughout my work shift without interruption.

**Why this priority**: This is CRITICAL because connection leaks cause complete application failure when MySQL server's max_connections limit is reached ("max users reached" error). This impacts all users and all features, making it the highest priority issue.

**Independent Test**: Can be fully tested by running the application for 4+ hours with moderate database activity (100+ transactions) and verifying no "max users reached" errors occur and all connections are properly closed after use.

**Acceptance Scenarios**:

1. **Given** the application is running with the fixed ExecuteReaderAsync method, **When** the user performs 100 database operations that previously used ExecuteReaderAsync, **Then** all MySqlDataReader connections are properly disposed and no connection leaks occur
2. **Given** the application has been running for 4 hours with normal usage, **When** checking MySQL SHOW PROCESSLIST, **Then** zero sleeping connections remain from the application and no connection exhaustion occurs
3. **Given** a developer is implementing new database access code, **When** they attempt to use ExecuteReaderAsync, **Then** the method is marked [Obsolete] and compiler warnings guide them to use ExecuteDataTableWithStatusAsync instead

---

### User Story 2 - Proactive Connection Lifecycle Monitoring (Priority: P2)

As a system administrator, I need visibility into database connection lifecycle through logging, so that I can detect connection leaks or issues before they cause application failures.

**Why this priority**: While not fixing the immediate leak, monitoring provides early warning systems and diagnostic capabilities to prevent future issues and troubleshoot problems faster.

**Independent Test**: Can be fully tested by enabling connection lifecycle monitoring and verifying that connection statistics are logged every 5 minutes showing currently open connections and verification that connections are properly closed after operations.

**Acceptance Scenarios**:

1. **Given** connection lifecycle monitoring is enabled, **When** the application runs normally, **Then** connection statistics are logged every 5 minutes showing server address and count of currently open connections (should be 0 when idle)
2. **Given** connection monitoring shows connections remaining open when application is idle, **When** the monitoring system detects this, **Then** a warning is logged indicating potential connection leak
3. **Given** a developer is troubleshooting connection issues, **When** they review the application logs, **Then** they can see historical connection lifecycle statistics to identify when leaks began

---

### User Story 3 - Immediate Connection Disposal (Priority: P3)

As a system operator, I need the MySQL connection string to disable connection pooling and enforce immediate connection disposal, so that every database operation opens a fresh connection, performs its action, and closes immediately to prevent connection leaks.

**Why this priority**: Simplifies connection management by eliminating pooling complexity. While slightly less performant than pooling, it's more reliable and easier to debug connection issues.

**Independent Test**: Can be fully tested by verifying the connection string contains Pooling=false, monitoring that connections are opened/closed for each operation, and confirming no idle connections remain after operations complete.

**Acceptance Scenarios**:

1. **Given** the connection string has Pooling=false, **When** the application performs a database operation, **Then** a new connection is opened, the operation executes, and the connection is immediately closed
2. **Given** multiple database operations are executed sequentially, **When** monitoring connection lifecycle, **Then** each operation opens and closes its own connection without reusing previous connections
3. **Given** the application is idle after database operations, **When** checking MySQL server connections, **Then** zero connections from the application remain open

---

### User Story 4 - Centralized Database Access Pattern (Priority: P3)

As a developer maintaining the codebase, I need all database access to go through Helper_Database_StoredProcedure, so that connection management is consistent and architectural standards are enforced.

**Why this priority**: This is architectural cleanup that reduces future maintenance risk but doesn't fix immediate bugs. It prevents future connection leaks by centralizing connection management.

**Independent Test**: Can be fully tested by verifying that Service_Migration, Service_Analytics, and Service_ErrorReportSync all use Helper_Database_StoredProcedure methods (ExecuteRawSqlAsync, ExecuteDataTableWithStatusAsync) instead of creating direct MySqlConnection instances.

**Acceptance Scenarios**:

1. **Given** Service_Migration needs to execute a raw SQL script, **When** RunMigrationScriptAsync is called, **Then** it uses Helper_Database_StoredProcedure.ExecuteRawSqlAsync instead of creating a direct MySqlConnection
2. **Given** Service_Analytics needs to fetch transaction data, **When** GetUserPerformanceMetricsAsync is called, **Then** it uses stored procedures through Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
3. **Given** Service_ErrorReportSync needs to check if a report exists, **When** ReportExistsAsync is called, **Then** it calls a stored procedure via Helper_Database_StoredProcedure instead of direct SQL
4. **Given** architectural compliance tools scan the codebase, **When** they check for direct MySqlConnection usage, **Then** only approved exceptions (Service_OnStartup_Database for parameter cache, Helper_Control_MySqlSignal for network diagnostics) are found

---

### User Story 5 - Visual SQL Server Immediate Disposal (Priority: P3)

As a system operator, I need Visual SQL Server (Infor Visual ERP) connections to also disable pooling and use immediate disposal, so that all database connections across the application follow the same simple pattern and prevent any SQL Server connection leaks.

**Why this priority**: While Visual SQL connections are already properly disposed with `using` statements, they currently use connection pooling. Disabling pooling provides consistency across all database types and eliminates any potential SQL Server pooling issues.

**Independent Test**: Can be fully tested by verifying Service_VisualDatabase connection string contains Pooling=false, monitoring SQL Server connections show zero idle connections from application when idle, and all 18 connection usages remain properly wrapped in `using` statements.

**Acceptance Scenarios**:

1. **Given** Service_VisualDatabase connection string is configured, **When** the application starts, **Then** the connection string includes "Pooling=false" for SQL Server
2. **Given** Visual dashboard performs a database query, **When** monitoring SQL Server connections, **Then** a connection is opened, query executes, and connection closes immediately without pooling
3. **Given** the application is idle after Visual operations, **When** checking SQL Server sys.dm_exec_connections, **Then** zero connections from the application remain open
4. **Given** all 18 Visual SQL connection usages, **When** reviewing code, **Then** all remain properly wrapped in `using` statements for immediate disposal

---

### Edge Cases

- What happens when ExecuteReaderAsync callers don't properly dispose the reader? *Fixed by marking method [Obsolete] and providing safe alternatives*
- How does the system handle multiple rapid database calls without pooling? *Each call opens/closes its own connection; MySQL server handles connection overhead efficiently*
- What happens if connection monitoring causes performance degradation? *Monitoring runs on background thread every 5 minutes with minimal overhead*
- How are existing ExecuteReaderAsync usages migrated without breaking functionality? *Each caller is identified and either wrapped with `using var` or refactored to use ExecuteDataTableWithStatusAsync*
- What happens when Service_Migration runs a very long SQL script (> 5 minutes)? *ExecuteRawSqlAsync accepts custom timeout parameter (default 300 seconds = 5 minutes)*
- What is the performance impact of disabling connection pooling? *Slight overhead (~10-20ms per operation) but eliminates pooling complexity and leak risks*

## Requirements *(mandatory)*

### Functional Requirements

#### Phase 1: Critical Connection Leak Fix

- **FR-001**: System MUST mark Helper_Database_StoredProcedure.ExecuteReaderAsync with [Obsolete] attribute directing developers to use ExecuteDataTableWithStatusAsync instead
- **FR-002**: System MUST identify all existing callers of ExecuteReaderAsync and either wrap them with proper `using var` disposal or refactor to ExecuteDataTableWithStatusAsync
- **FR-003**: ExecuteReaderAsync callers MUST properly dispose the MySqlDataReader using `using var reader = await ...` pattern to ensure connection disposal
- **FR-004**: System MUST document that CommandBehavior.CloseConnection is used in ExecuteReaderAsync, requiring reader disposal to close connection
- **FR-005**: All database read operations MUST preferentially use ExecuteDataTableWithStatusAsync which handles connection disposal automatically

#### Phase 2: Connection Lifecycle Monitoring

- **FR-006**: System MUST provide Helper_Database_ConnectionMonitor.LogConnectionStats() method to retrieve and log open connection statistics
- **FR-007**: System MUST log connection statistics including server address and count of currently open connections from the application
- **FR-008**: Connection lifecycle monitoring MUST run on a background thread to avoid blocking application operations
- **FR-009**: Connection statistics MUST be logged to the standard application log using LoggingUtility
- **FR-010**: Monitoring MUST be triggered periodically (every 5 minutes) from application main form timer

#### Phase 3: Connection String Configuration

- **FR-011**: MySQL connection string MUST include "Pooling=false" to disable connection pooling and enforce immediate disposal
- **FR-012**: MySQL connection string MUST include "Connection Timeout=30" to fail fast (30 seconds) if connection cannot be established
- **FR-013**: All database operations MUST use `using` statement or `using var` pattern to ensure connections are disposed immediately after use
- **FR-014**: Helper_Database_StoredProcedure methods MUST NOT cache connections and MUST create new connection for each operation
- **FR-015**: System MUST log warning if any connection remains open longer than expected operation time (> 60 seconds)
- **FR-016**: Connection monitoring MUST verify zero idle connections from application when no operations are active
- **FR-017**: Visual SQL Server connection string MUST include "Pooling=false" to disable SQL Server connection pooling
- **FR-018**: Visual SQL Server connection string MUST include "Connection Timeout=30" to match MySQL timeout behavior
- **FR-019**: Service_VisualDatabase MUST maintain all existing `using` statements (18 locations) for proper connection disposal
- **FR-020**: GetConnectionString() method in Service_VisualDatabase MUST build connection string with Pooling=false parameter

#### Phase 4: Architectural Refactoring

- **FR-021**: Helper_Database_StoredProcedure MUST provide ExecuteRawSqlAsync(connectionString, sqlScript, timeout) method for executing arbitrary SQL scripts
- **FR-022**: ExecuteRawSqlAsync MUST return Model_Dao_Result<bool> following standard DAO pattern
- **FR-023**: ExecuteRawSqlAsync MUST properly dispose connections using `using var` pattern
- **FR-024**: Service_Migration MUST use ExecuteRawSqlAsync instead of creating direct MySqlConnection instances (8 locations)
- **FR-025**: Service_Analytics MUST create stored procedures md_analytics_GetTransactionsByRange and md_analytics_GetUsersByDateRange
- **FR-026**: Service_Analytics MUST use Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync to call analytics stored procedures
- **FR-027**: Service_ErrorReportSync MUST use ExecuteRawSqlAsync for ExecuteSqlFileAsync method
- **FR-028**: Service_ErrorReportSync MUST create stored procedure md_error_reports_CheckExists for ReportExistsAsync method
- **FR-029**: Service_ErrorReportSync MUST use ExecuteScalarWithStatusAsync for IsDatabaseAvailableAsync with "SELECT 1" query
- **FR-030**: Service_OnStartup_Database MUST include comment documenting architectural exception for parameter cache initialization
- **FR-031**: Helper_Control_MySqlSignal MUST include comment documenting architectural exception for network diagnostics

### Key Entities *(include if feature involves data)*

- **MySqlConnection**: Represents database connection that must be properly disposed immediately after use. With Pooling=false, each connection is created fresh and closed completely after operation.
- **MySqlDataReader**: Forward-only result set reader that owns an underlying MySqlConnection. Must be disposed to close connection when using CommandBehavior.CloseConnection.
- **Connection Lifecycle**: Pattern of Open → Execute → Close for every database operation. No connection reuse or pooling occurs.
- **Helper_Database_StoredProcedure**: Centralized database access layer providing consistent connection management with immediate disposal, automatic parameter prefix detection, and Model_Dao_Result pattern.
- **Model_Dao_Result<T>**: Standard return type for all DAO operations providing IsSuccess, Data, and ErrorMessage properties for consistent error handling.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Application runs continuously for 8+ hours under normal production load without encountering "max users reached" MySQL errors (100% reliability)
- **SC-002**: Zero idle connections remain on MySQL server when application is idle (immediate disposal working correctly)
- **SC-003**: All ExecuteReaderAsync usages are either properly disposed with `using var` or migrated to ExecuteDataTableWithStatusAsync (100% compliance)
- **SC-004**: Connection lifecycle monitoring logged every 5 minutes shows connections are opened and closed for each operation (12 log entries per hour)
- **SC-005**: 90% of architectural violations (direct MySqlConnection usage) are refactored to use Helper_Database_StoredProcedure pattern
- **SC-006**: ExecuteRawSqlAsync is successfully used by Service_Migration for all 8 migration script executions without connection leaks
- **SC-007**: Zero new instances of direct MySqlConnection usage are introduced after deployment (verified by code review and grep scans)
- **SC-008**: Database connection timeout errors decrease to < 1 per day (down from multiple per hour)
- **SC-009**: Average database operation time increases by no more than 20ms compared to pooled connections (acceptable overhead)
- **SC-010**: MySQL server SHOW PROCESSLIST shows zero sleeping connections from application when idle
- **SC-011**: SQL Server sys.dm_exec_connections shows zero sleeping connections from application when Visual operations are idle
- **SC-012**: Both MySQL and Visual SQL Server connection strings explicitly set Pooling=false (verified in configuration)

### Qualitative Outcomes

- Developers have clear guidance via [Obsolete] attribute on ExecuteReaderAsync directing them to safer alternatives
- System administrators can verify zero leaked connections by checking MySQL server SHOW PROCESSLIST when application is idle
- Codebase has reduced architectural violations improving maintainability and reducing future leak risk
- All architectural exceptions (Service_OnStartup_Database, Helper_Control_MySqlSignal) are clearly documented explaining why direct connection usage is acceptable
- Simpler connection management model (open→execute→close) is easier to understand and debug than pooling

## Assumptions

- **A-001**: MySQL Server 5.7.24 remains the target database version (no upgrade to 8.0)
- **A-002**: MySql.Data 9.4.0 driver is used and properly disposes connections when Pooling=false
- **A-003**: Database operations are not extremely high-frequency (< 100 operations/second) where pooling overhead would be critical
- **A-004**: Application runs in single-instance mode with typical concurrent operations (< 20 simultaneous database calls)
- **A-005**: Existing stored procedures in database are compatible with Helper_Database_StoredProcedure parameter prefix detection
- **A-006**: Service_OnStartup_Database parameter cache initialization runs once at startup and requires direct INFORMATION_SCHEMA access
- **A-007**: Helper_Control_MySqlSignal network diagnostics require direct connection test and cannot use stored procedure layer
- **A-008**: Migration scripts in Service_Migration are one-way (no rollback required) and can use ExecuteRawSqlAsync
- **A-009**: Service_Analytics queries are high-volume and justify creating stored procedures rather than inline SQL
- **A-010**: LoggingUtility.Log() is thread-safe and can be called from background monitoring thread

## Dependencies

- **D-001**: MySql.Data 9.4.0 NuGet package for MySQL connectivity
- **D-002**: Microsoft.Data.SqlClient NuGet package for SQL Server connectivity
- **D-003**: Helper_Database_Variables for MySQL connection string management
- **D-004**: Service_VisualDatabase for Visual SQL Server connection string management
- **D-005**: Helper_Database_StoredProcedure existing methods (ExecuteDataTableWithStatusAsync, ExecuteScalarWithStatusAsync)
- **D-006**: Model_Dao_Result pattern for consistent error handling
- **D-007**: LoggingUtility for connection lifecycle monitoring logs
- **D-008**: Service_DebugTracer for performance tracing (already integrated in Helper_Database_StoredProcedure)
- **D-009**: Model_Application_Variables for CommandTimeoutSeconds and other configuration
- **D-010**: MySQL Server 5.7.24 configured to handle frequent connection creation/disposal without performance degradation
- **D-011**: Visual SQL Server (Infor Visual ERP) configured to handle frequent connection creation/disposal without performance degradation

## Out of Scope

- **OS-001**: Migration to MySQL 8.0+ (remains on 5.7.24)
- **OS-002**: Creating stored procedures for ALL inline SQL (only focusing on Service_Analytics as high-volume target)
- **OS-003**: Implementing connection lifecycle monitoring UI dashboard (only logging for Phase 2)
- **OS-004**: Re-enabling connection pooling (using simple immediate disposal approach instead)
- **OS-005**: Refactoring ALL architectural violations immediately (prioritizing high-impact areas)
- **OS-006**: Database schema changes or stored procedure parameter standardization (handled separately)
- **OS-007**: Performance optimization beyond connection management (separate effort)
- **OS-008**: Unit test coverage for connection lifecycle monitoring (integration testing sufficient)
- **OS-009**: Rollback mechanisms for ExecuteRawSqlAsync migrations (migrations are one-way)

## Notes

### Why Immediate Disposal Over Connection Pooling?

The decision to disable connection pooling (Pooling=false) in favor of immediate disposal is based on:

1. **Simplicity**: Open → Execute → Close is easier to understand, implement, and debug than managing a connection pool
2. **Leak Prevention**: Impossible to leak connections to the pool when there is no pool
3. **Explicit Resource Management**: Every database operation has clear connection lifecycle visibility
4. **Acceptable Performance**: Modern MySQL servers handle connection creation efficiently; ~10-20ms overhead is acceptable for this application's usage patterns
5. **Reduced Complexity**: No need to monitor pool exhaustion, tune pool parameters, or handle pool recycling edge cases

**Trade-offs Accepted**:
- Slight performance overhead (~10-20ms per operation) due to connection creation
- Not suitable for extremely high-frequency operations (> 100/sec), but this application operates well below that threshold

### Why ExecuteReaderAsync is Problematic

The current ExecuteReaderAsync implementation is error-prone because:

1. **Implicit Ownership Transfer**: The method creates a MySqlConnection but transfers ownership to the caller via MySqlDataReader. This implicit contract is easy to violate.

2. **Disposal Dependency**: Connection disposal depends on CommandBehavior.CloseConnection and reader disposal. If caller forgets `using`, connection leaks.

3. **No Compile-Time Safety**: Unlike `using` statements that are enforced by C# language, reader disposal is a runtime concern that can be forgotten.

4. **Alternative Exists**: ExecuteDataTableWithStatusAsync provides the same functionality with automatic connection management and Model_Dao_Result pattern.

### Architectural Exception Rationale

Two direct MySqlConnection usages are acceptable:

1. **Service_OnStartup_Database (Line 184)**: Parameter cache initialization requires querying INFORMATION_SCHEMA.PARAMETERS at startup. This is a one-time operation that cannot use stored procedures (it's building the stored procedure metadata cache). Uses `using var` for proper disposal.

2. **Helper_Control_MySqlSignal (Line 33)**: Network diagnostic tool that measures raw connection performance. Using Helper_Database_StoredProcedure would add overhead and defeat the purpose of measuring connection speed. Uses `using var` for proper disposal.

Both exceptions are justified, properly disposed, and will be documented with comments.

### Migration Strategy

Phase 1 (Critical - Week 1):
- Mark ExecuteReaderAsync [Obsolete]
- Find and fix all ExecuteReaderAsync callers
- Test thoroughly to confirm no connection leaks

Phase 2 (High Priority - Week 1):
- Implement Helper_Database_ConnectionMonitor
- Add connection lifecycle monitoring to MainForm timer
- Deploy MySQL connection string with Pooling=false
- Update Visual SQL Server connection string with Pooling=false

Phase 3 (Medium Priority - Week 2-3):
- Create ExecuteRawSqlAsync helper
- Refactor Service_Migration (8 locations)
- Create analytics stored procedures
- Refactor Service_Analytics (3 locations)

Phase 4 (Low Priority - Week 3-4):
- Create error reporting stored procedures
- Refactor Service_ErrorReportSync (3 locations)
- Add architectural exception comments
- Final compliance scan

### Testing Approach

**Connection Leak Testing**:
- Run application for 8 hours with automated database activity (both MySQL and Visual SQL)
- Monitor MySQL server SHOW PROCESSLIST every 5 minutes
- Monitor SQL Server sys.dm_exec_connections every 5 minutes
- Verify zero sleeping connections when application is idle (both database types)
- Confirm no "max users reached" errors occur on either MySQL or SQL Server

**Regression Testing**:
- Execute all existing DAO integration tests
- Test Visual dashboard operations (receiving schedule, inventory, transactions)
- Run full application smoke test
- Verify all database operations work correctly after disabling pooling on both database types

**Performance Testing**:
- Measure connection creation overhead for MySQL (acceptable if < 20ms per operation)
- Measure connection creation overhead for Visual SQL Server (acceptable if < 20ms per operation)
- Verify connections are closed immediately after operations complete (both database types)
- Test under load (20 concurrent operations per database) to ensure no connection exhaustion

**Monitoring Testing**:
- Verify connection lifecycle statistics logged every 5 minutes for both MySQL and SQL Server
- Confirm log entries show connections opened and closed for each operation
- Verify zero idle connections remain when application is idle (both database types)
- Test Visual operations: dashboard data loads, die searches, receiving analytics
