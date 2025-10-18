# Task Breakdown: Developer Tools Suite Integration

**Branch**: `002-003-database-layer-complete`  
**Parent Spec**: [../spec.md](../spec.md)  
**Feature Spec**: [spec.md](./spec.md)  
**Plan**: [plan.md](./plan.md)  
**Created**: 2025-10-18

---

## Overview

This task breakdown implements the Developer Tools Suite Integration (002-003-001) as a sub-feature of the Comprehensive Database Layer Standardization initiative. Tasks are organized by user story priority (P1, P2, P3) to enable independent implementation and testing.

**Task Organization**:
- **Phase 1**: Setup & Infrastructure (T001-T005)
- **Phase 2**: Foundational Prerequisites (T006-T015)
- **Phase 3**: User Story 1 - Debug Dashboard (P1) (T016-T022)
- **Phase 4**: User Story 2 - Parameter Prefix Maintenance (P1) (T023-T035)
- **Phase 5**: User Story 3 - Schema Inspector (P2) (T036-T043)
- **Phase 6**: User Story 4 - Procedure Call Hierarchy (P2) (T044-T052)
- **Phase 7**: User Story 5 - Code Generator (P3) (T053-T060)
- **Phase 8**: Control_Database Integration (T061-T063)
- **Phase 9**: Polish & Integration (T064-T070)

**Total Tasks**: 70  
**Estimated Duration**: 3-4 weeks (2 developers)

---

## Dependencies

### Story Dependencies
- **US1 (Debug Dashboard)**: Independent - can implement immediately after foundational phase
- **US2 (Parameter Prefix)**: Independent - can implement immediately after foundational phase
- **US3 (Schema Inspector)**: Independent - can implement immediately after foundational phase
- **US4 (Call Hierarchy)**: Independent - can implement immediately after foundational phase
- **US5 (Code Generator)**: Independent - can implement immediately after foundational phase
- **Control_Database Refactor**: Independent - existing control move

### Blocking Prerequisites
All user stories require completion of:
- Phase 1 (Setup)
- Phase 2 (Foundational) - specifically:
  - T006: Database table creation
  - T007-T012: Stored procedure creation
  - T013: Model class
  - T014: DAO class
  - T015: Helper_Database_StoredProcedure override cache

---

## Phase 1: Setup & Infrastructure

**Goal**: Prepare development environment and enable Developer role for testing.

### T001 - Grant JOHNK Developer Role
**Story**: Infrastructure  
**Files**: `Database/Scripts/Grant-Developer-Role.sql`  
**Priority**: CRITICAL - Blocks manual testing of all developer tools

**Description**:
Create and execute SQL script to grant JOHNK user Admin + Developer privileges in test database.

**Reference**: `.github/instructions/mysql-database.instructions.md` - Follow stored procedure standards for any helper queries.

**Steps**:
1. Create new file: `Database/Scripts/Grant-Developer-Role.sql`
2. Write UPDATE statement:
   ```sql
   UPDATE usr_users
   SET IsAdmin = 1, IsDeveloper = 1, ModifiedBy = 'SYSTEM', ModifiedDate = NOW()
   WHERE UserName = 'JOHNK';
   ```
3. Execute against `mtm_wip_application_winforms_test` database
4. Verify with SELECT query showing IsAdmin=1, IsDeveloper=1

**Acceptance**:
- [X] SQL script exists in Database/Scripts/
- [X] JOHNK user has IsAdmin=1 AND IsDeveloper=1
- [X] Settings form shows Developer category when JOHNK logs in

**Estimated**: 15 minutes

---

### T002 - Verify Database Analysis Artifacts Exist
**Story**: Infrastructure  
**Files**: Check existence only (no modifications)  
**Priority**: HIGH - Required for US4 (Procedure Call Hierarchy)

**Description**:
Verify that required database analysis artifacts exist from Phase 2.5 Part A discovery work.

**Reference**: Parent spec `../tasks.md` (T100-T106) for artifact generation commands.

**Steps**:
1. Check file existence:
   - `Database/call-hierarchy-complete.json` (from T102)
   - `Database/STORED_PROCEDURE_CALLSITES.csv` (from T100)
2. If missing, regenerate:
   ```powershell
   cd Database
   .\2-Trace-Complete-CallHierarchy-v2.ps1
   ```
3. Validate JSON structure (valid JSON, has procedure nodes)
4. Validate CSV structure (has columns: Procedure, File, Line, Context)

**Acceptance**:
- [X] Both artifact files exist
- [X] JSON parses without errors
- [X] CSV has header row and data rows

**Estimated**: 10 minutes

---

### T003 - Create Developer Tools Documentation Directory
**Story**: Infrastructure  
**Files**: Directory structure only  
**Priority**: LOW - Nice to have

**Description**:
Create organized directory structure for developer tools documentation and examples.

**Steps**:
1. Create directory: `Documentation/DeveloperTools/`
2. Create subdirectories:
   - `Examples/` (code samples)
   - `Screenshots/` (UI screenshots for user guide)
   - `Workflows/` (workflow diagrams)

**Acceptance**:
- [X] Directory structure exists
- [X] README.md in DeveloperTools/ explains purpose

**Estimated**: 5 minutes

---

### T004 - Update .gitignore for Developer Tools Artifacts
**Story**: Infrastructure  
**Files**: `.gitignore`  
**Priority**: MEDIUM - Prevents committing sensitive debug logs

**Description**:
Add patterns to .gitignore to exclude debug logs and temporary developer artifacts.

**Reference**: `.github/instructions/security-best-practices.instructions.md` - Never commit sensitive data in logs.

**Steps**:
1. Open `.gitignore`
2. Add section:
   ```
   # Developer Tools - Debug Dashboard logs
   Logs/DebugCapture_*.txt
   Logs/DebugDashboard_*.log
   
   # Developer Tools - Generated code samples
   Documentation/DeveloperTools/Examples/Generated_*.cs
   ```
3. Commit .gitignore update

**Acceptance**:
- [X] Debug log patterns in .gitignore
- [X] Generated code patterns in .gitignore
- [X] Existing Logs/ directory respected

**Estimated**: 5 minutes

---

### T005 - Review Existing Settings Form Integration Patterns
**Story**: Infrastructure  
**Files**: `Forms/Settings/SettingsForm.cs`, existing Controls  
**Priority**: HIGH - Understanding prerequisite

**Description**:
Study existing Settings form UserControl integration pattern to understand Developer category implementation requirements.

**Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (WinForms Patterns section) - Follow existing control patterns.

**Steps**:
1. Read `Forms/Settings/SettingsForm.cs`:
   - TreeView population logic
   - Node selection event handler
   - Control loading/unloading pattern
   - Progress bar and status label usage
2. Read existing control examples:
   - `Controls/SettingsForm/Control_Add_User.cs`
   - `Controls/SettingsForm/Control_Theme.cs`
   - `Controls/SettingsForm/Control_Database.cs`
3. Note patterns:
   - Constructor requirements (Core_Themes.ApplyDpiScaling)
   - ReloadAsync() implementation
   - ClearAsync() implementation (if applicable)
   - Region organization
4. Document findings in T005 completion notes

**Acceptance**:
- [X] Developer understands TreeView node addition
- [X] Developer understands control lifecycle (Load/Reload/Clear)
- [X] Developer understands progress bar integration
- [X] Notes captured for reference during implementation

**Estimated**: 30 minutes

---

**Phase 1 Complete**: Setup tasks finished. Ready for Phase 2 (Foundational Prerequisites).

---

## Phase 2: Foundational Prerequisites (BLOCKING)

**Goal**: Create database infrastructure and base classes required by ALL developer tools.

**CRITICAL**: These tasks MUST complete before any user story implementation can begin. All user stories depend on the parameter prefix override infrastructure.

### T006 - Create sys_parameter_prefix_overrides Table
**Story**: Foundational  
**Files**: `Database/UpdatedDatabase/Tables/sys_parameter_prefix_overrides.sql`  
**Priority**: CRITICAL - Blocks US2 and Helper_Database_StoredProcedure integration

**Description**:
Create database table for storing parameter prefix overrides with audit trail support.

**Reference**: `contracts/stored-procedures.md` (Deployment Script section) + `.github/instructions/mysql-database.instructions.md` (Database schema standards).

**Steps**:
1. Create SQL file in UpdatedDatabase/Tables/
2. Write CREATE TABLE statement with all columns per data-model.md
3. Add indexes: PRIMARY KEY, UNIQUE constraint, performance indexes
4. Execute against `mtm_wip_application_winforms_test` database
5. Verify table structure with DESCRIBE command
6. Test INSERT/SELECT to validate constraints

**SQL Structure**:
```sql
CREATE TABLE sys_parameter_prefix_overrides (
    OverrideId INT PRIMARY KEY AUTO_INCREMENT,
    ProcedureName VARCHAR(128) NOT NULL,
    ParameterName VARCHAR(128) NOT NULL,
    OverridePrefix VARCHAR(10) NOT NULL,
    Reason VARCHAR(500) NULL,
    CreatedBy VARCHAR(50) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ModifiedBy VARCHAR(50) NULL,
    ModifiedDate DATETIME NULL,
    IsActive TINYINT(1) NOT NULL DEFAULT 1,
    UNIQUE KEY UQ_ProcParam (ProcedureName, ParameterName),
    INDEX IDX_ProcName (ProcedureName),
    INDEX IDX_IsActive (IsActive)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

**Acceptance**:
- [X] Table exists in test database
- [X] All columns and indexes created
- [X] UNIQUE constraint prevents duplicate procedure-parameter pairs
- [X] Default values work (CreatedDate, IsActive)

**Estimated**: 20 minutes

---

### T007 - Create sys_parameter_prefix_overrides_Get_All Stored Procedure
**Story**: Foundational  
**Files**: `Database/UpdatedStoredProcedures/ReadyForVerification/sys_parameter_prefix_overrides_Get_All.sql`  
**Priority**: CRITICAL - Required for startup cache loading

**Description**:
Create stored procedure to retrieve all active parameter prefix overrides for application startup cache loading.

**Reference**: `contracts/stored-procedures.md` (Procedure #1) + `.github/instructions/mysql-database.instructions.md` (Stored Procedure Standards section).

**Steps**:
1. Create SQL file in ReadyForVerification/
2. Implement procedure per contract specification:
   - OUT p_Status INT
   - OUT p_ErrorMsg VARCHAR(500)
   - SELECT * FROM sys_parameter_prefix_overrides WHERE IsActive = 1
   - ORDER BY ProcedureName, ParameterName
3. Add transaction management (START TRANSACTION / COMMIT / ROLLBACK)
4. Add error handling with proper status codes
5. Test procedure: `CALL sys_parameter_prefix_overrides_Get_All(@status, @errorMsg);`
6. Verify empty result set returns p_Status = 1
7. Insert test data and verify p_Status = 0

**Acceptance**:
- [X] Procedure exists and compiles
- [X] Returns only active overrides (IsActive = 1)
- [X] Returns p_Status = 0 with data, p_Status = 1 when empty
- [X] Proper error handling and logging

**Estimated**: 30 minutes

---

### T008 - Create sys_parameter_prefix_overrides_Get_ById Stored Procedure
**Story**: Foundational  
**Files**: `Database/UpdatedStoredProcedures/ReadyForVerification/sys_parameter_prefix_overrides_Get_ById.sql`  
**Priority**: HIGH - Required for edit operations

**Description**:
Create stored procedure to retrieve single parameter prefix override by ID.

**Reference**: `contracts/stored-procedures.md` (Procedure #2) + `.github/instructions/mysql-database.instructions.md`.

**Steps**:
1. Create SQL file
2. Implement per contract (IN p_OverrideId, OUT p_Status, OUT p_ErrorMsg)
3. Add validation: OverrideId must be positive integer
4. Return record regardless of IsActive status
5. Return p_Status = 1 if not found
6. Test with existing and non-existent IDs

**Acceptance**:
- [X] Procedure exists and compiles
- [X] Returns single record by ID
- [X] Returns p_Status = 1 for non-existent ID
- [X] Works with both active and inactive records

**Estimated**: 25 minutes

---

### T009 - Create sys_parameter_prefix_overrides_Add Stored Procedure
**Story**: Foundational  
**Files**: `Database/UpdatedStoredProcedures/ReadyForVerification/sys_parameter_prefix_overrides_Add.sql`  
**Priority**: CRITICAL - Required for US2 add functionality

**Description**:
Create stored procedure to add new parameter prefix override with validation.

**Reference**: `contracts/stored-procedures.md` (Procedure #3) + `.github/instructions/mysql-database.instructions.md` (Transaction Management section).

**Steps**:
1. Create SQL file
2. Implement per contract (7 IN parameters, 1 OUT p_OverrideId, status/error outputs)
3. Add validation:
   - ProcedureName NOT NULL/empty
   - ParameterName NOT NULL/empty
   - CreatedBy NOT NULL/empty
   - OverridePrefix can be empty string (valid)
4. Handle UNIQUE constraint violation (p_Status = -3)
5. Set audit fields (CreatedDate auto, IsActive default 1)
6. Return new OverrideId via OUT parameter
7. Test duplicate detection, validation errors, successful insert

**Acceptance**:
- [X] Procedure exists and compiles
- [X] Validates required fields (returns p_Status = -2)
- [X] Detects duplicates (returns p_Status = -3)
- [X] Returns new OverrideId on success
- [X] Audit trail fields populated correctly

**Estimated**: 45 minutes

---

### T010 - Create sys_parameter_prefix_overrides_Update_ById Stored Procedure
**Story**: Foundational  
**Files**: `Database/UpdatedStoredProcedures/ReadyForVerification/sys_parameter_prefix_overrides_Update_ById.sql`  
**Priority**: HIGH - Required for US2 edit functionality

**Description**:
Create stored procedure to update existing parameter prefix override.

**Reference**: `contracts/stored-procedures.md` (Procedure #4) + `.github/instructions/mysql-database.instructions.md`.

**Steps**:
1. Create SQL file
2. Implement per contract (8 IN parameters, status/error outputs)
3. Validation same as Add procedure
4. Check OverrideId exists (p_Status = 1 if not found)
5. Handle UNIQUE constraint when changing ProcedureName/ParameterName
6. Update ModifiedBy and ModifiedDate automatically
7. Preserve CreatedBy and CreatedDate (do not overwrite)
8. Test update scenarios, duplicate detection, not-found handling

**Acceptance**:
- [X] Procedure exists and compiles
- [X] Updates existing override
- [X] Returns p_Status = 1 for non-existent ID
- [X] ModifiedBy/ModifiedDate updated, CreatedBy/CreatedDate preserved
- [X] Duplicate detection works on update

**Estimated**: 40 minutes

---

### T011 - Create sys_parameter_prefix_overrides_Delete_ById Stored Procedure
**Story**: Foundational  
**Files**: `Database/UpdatedStoredProcedures/ReadyForVerification/sys_parameter_prefix_overrides_Delete_ById.sql`  
**Priority**: HIGH - Required for US2 delete functionality

**Description**:
Create stored procedure for soft-delete of parameter prefix override.

**Reference**: `contracts/stored-procedures.md` (Procedure #5) + `.github/instructions/mysql-database.instructions.md`.

**Steps**:
1. Create SQL file
2. Implement per contract (IN p_OverrideId, IN p_ModifiedBy, status/error outputs)
3. Soft delete: UPDATE IsActive = 0 (not physical DELETE)
4. Update ModifiedBy and ModifiedDate
5. Return p_Status = 1 if OverrideId not found
6. Idempotent: Already deleted returns p_Status = 0 (success)
7. Test delete, already-deleted, not-found scenarios

**Acceptance**:
- [X] Procedure exists and compiles
- [X] Sets IsActive = 0 (soft delete)
- [X] Record still exists in table after delete
- [X] Idempotent (can delete already-deleted record)
- [X] Audit trail updated

**Estimated**: 30 minutes

---

### T012 - Test All 5 Stored Procedures Together
**Story**: Foundational  
**Files**: Manual SQL testing (no code changes)  
**Priority**: HIGH - Validates CRUD contract completeness

**Description**:
Execute comprehensive SQL test script validating all 5 CRUD procedures work together correctly.

**Reference**: `contracts/stored-procedures.md` (Testing Checklist section).

**Steps**:
1. Create test script: `Database/Scripts/Test-ParameterPrefixOverrides-CRUD.sql`
2. Test Add procedure:
   - Add valid override
   - Attempt duplicate add (expect p_Status = -3)
   - Add with empty required field (expect p_Status = -2)
3. Test Get_All procedure:
   - Verify new override appears
   - Verify ordering by ProcedureName, ParameterName
4. Test Get_ById procedure:
   - Retrieve by valid ID
   - Retrieve by non-existent ID (expect p_Status = 1)
5. Test Update procedure:
   - Update valid override
   - Attempt duplicate name (expect p_Status = -3)
   - Update non-existent ID (expect p_Status = 1)
6. Test Delete procedure:
   - Soft delete override
   - Verify Get_All no longer returns it
   - Verify Get_ById still returns it (IsActive = 0)
   - Delete again (expect p_Status = 0, idempotent)
7. Document test results

**Acceptance**:
- [X] All test scenarios pass
- [X] No SQL errors or warnings
- [X] Audit trail fields populated correctly in all operations
- [X] Test script saved for regression testing

**Estimated**: 45 minutes

---

### T013 - Create Model_ParameterPrefixOverride POCO
**Story**: Foundational  
**Files**: `Models/Model_ParameterPrefixOverride.cs`  
**Priority**: CRITICAL - Required by DAO and all UI controls

**Description**:
Create C# POCO model representing parameter prefix override database record.

**Reference**: `data-model.md` (Model_ParameterPrefixOverride section) + `.github/instructions/csharp-dotnet8.instructions.md` (Naming Conventions section).

**Steps**:
1. Create new file in Models/
2. Define class with all properties per data-model.md
3. Add XML documentation comments on each property
4. Add computed property: `FullParameterName => $"{OverridePrefix}{ParameterName}"`
5. Follow nullable reference type annotations
6. Use proper C# types (DateTime vs DateTime?, string vs string?)
7. Organize with #region Properties

**Acceptance**:
- [X] Model class exists with all 10 properties
- [X] XML documentation on class and properties
- [X] Nullable annotations correct
- [X] Computed FullParameterName property works
- [X] File compiles without warnings

**Estimated**: 20 minutes

---

### T014 - Create Dao_ParameterPrefixOverrides DAO Class
**Story**: Foundational  
**Files**: `Data/Dao_ParameterPrefixOverrides.cs`  
**Priority**: CRITICAL - Required by US2 and Helper integration

**Description**:
Create DAO class wrapping all 5 CRUD stored procedures with proper error handling and DaoResult pattern.

**Reference**: `contracts/stored-procedures.md` (C# Integration examples) + `.github/instructions/integration-testing.instructions.md` (Discovery-First Workflow) + `.github/instructions/csharp-dotnet8.instructions.md` (Data Access & Async section).

**Steps**:
1. Create new file in Data/
2. Implement 5 async methods (one per stored procedure):
   - `GetAllActiveAsync()` → `Task<DaoResult<List<Model_ParameterPrefixOverride>>>`
   - `GetByIdAsync(int overrideId)` → `Task<DaoResult<Model_ParameterPrefixOverride>>`
   - `AddAsync(Model_ParameterPrefixOverride override)` → `Task<DaoResult<int>>` (returns new ID)
   - `UpdateAsync(Model_ParameterPrefixOverride override)` → `Task<DaoResult>>`
   - `DeleteAsync(int overrideId)` → `Task<DaoResult>`
3. Use Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync for all calls
4. Map DataTable rows to Model_ParameterPrefixOverride objects
5. Handle all status codes (0, 1, -1, -2, -3) with appropriate DaoResult responses
6. Add proper XML documentation on all public methods
7. Use #region organization per MTM standards
8. Add try-catch with Dao_ErrorLog.HandleException

**Acceptance**:
- [X] DAO class exists with all 5 methods
- [X] All methods use async/await pattern
- [X] DaoResult pattern used for all return types
- [X] XML documentation complete
- [X] Error handling with Service_ErrorHandler integration
- [X] File compiles without warnings

**Estimated**: 90 minutes

---

### T015 - Extend Helper_Database_StoredProcedure with Override Cache
**Story**: Foundational  
**Files**: `Helpers/Helper_Database_StoredProcedure.cs`  
**Priority**: CRITICAL - Core infrastructure for parameter prefix system

**Description**:
Add parameter prefix override cache loading and application logic to Helper_Database_StoredProcedure.

**Reference**: `plan.md` (Parameter Prefix Resolution flow) + `.github/instructions/csharp-dotnet8.instructions.md` + `.github/instructions/performance-optimization.instructions.md` (Caching Strategies section).

**Steps**:
1. Add static fields:
   - `_parameterPrefixOverrides` (Dictionary<string, Dictionary<string, string>>)
   - `_overrideCacheLoaded` (bool flag)
2. Add method: `LoadParameterPrefixOverridesAsync()`
   - Call Dao_ParameterPrefixOverrides.GetAllActiveAsync()
   - Populate cache dictionary (key1=ProcedureName, key2=ParameterName, value=OverridePrefix)
   - Set _overrideCacheLoaded = true
   - Log count of overrides loaded
3. Modify `ExecuteDataTableWithStatusAsync()`:
   - Before INFORMATION_SCHEMA query, check override cache
   - If override exists for procedure+parameter, use override prefix
   - If no override, fall back to INFORMATION_SCHEMA or convention
4. Add method: `ReloadParameterPrefixOverridesAsync()` (force refresh)
5. Call LoadParameterPrefixOverridesAsync() in Program.cs startup sequence
6. Add logging for override application: `[DB] Using override prefix '{prefix}' for {procedure}.{parameter}`

**Acceptance**:
- [X] Override cache loads at application startup
- [X] Cache populated from database successfully
- [X] Parameter resolution checks cache before INFORMATION_SCHEMA
- [X] Override application logged for debugging
- [X] Reload method works for runtime refresh
- [X] No breaking changes to existing DAO calls

**Estimated**: 120 minutes

---

**Phase 2 Complete**: Foundational infrastructure ready. User stories can now proceed independently.

**Checkpoint**: Test startup with override cache loading before proceeding to user stories.

---

## Phase 3: User Story 1 - Debug Dashboard (P1)

**Goal**: Convert DebugDashboardForm to UserControl and integrate into Settings → Developer.

**Independent Test**: Developer can access Debug Dashboard through Settings, enable database tracing, and see real-time stored procedure execution logs.

**Story Value**: Essential for validating stored procedure refactoring and debugging integration issues during Phase 2.5.

### T016 - [US1] Convert DebugDashboardForm to Control_Developer_DebugDashboard
**Story**: US1 (Debug Dashboard)  
**Files**: 
- Create: `Controls/SettingsForm/Control_Developer_DebugDashboard.cs`
- Create: `Controls/SettingsForm/Control_Developer_DebugDashboard.Designer.cs`
- Read: `Forms/Development/DebugDashboardForm.cs` (copy logic from here)

**Priority**: P1 - HIGH  
**Parallelizable**: [P] with T023 (US2), T036 (US3), T044 (US4), T053 (US5)

**Description**:
Convert existing standalone DebugDashboardForm to UserControl following Settings form integration pattern.

**Reference**: `contracts/usercontrol-api.md` (Control_Developer_DebugDashboard section) + `.github/instructions/csharp-dotnet8.instructions.md` (WinForms Patterns section).

**Steps**:
1. Create new UserControl in Controls/SettingsForm/
2. Copy UI layout from DebugDashboardForm.Designer.cs:
   - CheckBox controls for trace categories (Database, Business Logic, UI, Performance)
   - ComboBox for debug level (Low, Medium, High)
   - TextBox for debug output (multi-line, read-only, black background, green text)
   - Buttons: Pause/Resume, Clear, Save Log, Copy
   - Timer for output refresh (1 second interval)
3. Copy business logic from DebugDashboardForm.cs:
   - _debugLog list management
   - _isCapturingDebug flag
   - Service_DebugTracer integration
   - Timer tick handler
4. Add required UserControl methods:
   - ReloadAsync() - start capture
   - ClearAsync() - stop capture, clear output
   - StartCapture(), PauseCapture() public methods
5. Add constructor pattern:
   ```csharp
   public Control_Developer_DebugDashboard()
   {
       InitializeComponent();
       Core_Themes.ApplyDpiScaling(this);
       Core_Themes.ApplyRuntimeLayoutAdjustments(this);
       _debugLog = new List<string>();
       // ... rest of initialization
   }
   ```
6. Implement auto-truncation: When _debugLog.Count > 1000, remove oldest 100 entries
7. Add XML documentation on all public methods
8. Organize with #region structure per MTM standards

**Acceptance**:
- [X] UserControl exists and compiles
- [X] UI layout matches original form
- [X] Core_Themes integration in constructor
- [X] ReloadAsync() and ClearAsync() implemented
- [X] Auto-truncation at 1000 lines implemented
- [X] XML documentation complete

**Estimated**: 90 minutes

---

### T017 - [US1] Add Developer Category to Settings TreeView
**Story**: US1 (Debug Dashboard)  
**Files**: `Forms/Settings/SettingsForm.cs`  
**Priority**: P1 - CRITICAL - Required for all developer tools access

**Description**:
Add Developer category node to Settings TreeView with role-based visibility check.

**Reference**: `contracts/usercontrol-api.md` (Settings Form Integration section) + `.github/instructions/csharp-dotnet8.instructions.md`.

**Steps**:
1. Open SettingsForm.cs
2. Locate TreeView population method (likely `LoadTreeViewNodes()` or similar)
3. Add Developer role check:
   ```csharp
   if (Model_AppVariables.CurrentUser.IsDeveloper)
   {
       var developerNode = new TreeNode("Developer");
       developerNode.Nodes.Add("Debug Dashboard");
       // ... other tools added in later tasks
       treeViewSettings.Nodes.Add(developerNode);
   }
   ```
4. Add Developer category after "About" node (last in list)
5. Test with JOHNK user (should see Developer node)
6. Test with non-developer user (should NOT see Developer node)

**Acceptance**:
- [X] Developer node appears in TreeView for IsDeveloper users
- [X] Developer node hidden for non-developer users
- [X] Node positioned after About node
- [X] Debug Dashboard child node exists

**Estimated**: 15 minutes

---

### T018 - [US1] Implement TreeView Selection Handler for Debug Dashboard
**Story**: US1 (Debug Dashboard)  
**Files**: `Forms/Settings/SettingsForm.cs`  
**Priority**: P1 - HIGH

**Description**:
Add TreeView selection event handler case for loading Debug Dashboard control.

**Reference**: `contracts/usercontrol-api.md` (TreeView Structure section).

**Steps**:
1. Locate TreeView AfterSelect event handler
2. Add case for "Debug Dashboard":
   ```csharp
   UserControl? newControl = e.Node.Text switch
   {
       "Debug Dashboard" => new Control_Developer_DebugDashboard(),
       // ... existing cases
       _ => null
   };
   
   if (newControl != null)
   {
       _currentControl = newControl;
       newControl.Dock = DockStyle.Fill;
       pnlContent.Controls.Add(newControl);
       
       if (newControl is IAsyncControl asyncControl)
           await asyncControl.ReloadAsync();
   }
   ```
3. Ensure existing control cleanup (ClearAsync) happens before loading new control
4. Test navigation to Debug Dashboard node

**Acceptance**:
- [X] Debug Dashboard loads when node selected
- [X] Control fills panel correctly
- [X] ReloadAsync() called on load
- [X] ClearAsync() called when navigating away

**Estimated**: 20 minutes

---

### T019 - [US1] Test Debug Dashboard Service_DebugTracer Integration
**Story**: US1 (Debug Dashboard)  
**Files**: Manual testing (no code changes)  
**Priority**: P1 - HIGH

**Description**:
Validate that Debug Dashboard correctly captures and displays output from Service_DebugTracer.

**Reference**: `quickstart.md` (Debug Dashboard section) + `.github/instructions/testing-standards.instructions.md`.

**Test Scenarios**:
1. **Enable Database Operations Tracing**:
   - Open Settings → Developer → Debug Dashboard
   - Check "Database Operations" checkbox
   - Execute inventory operation in main application
   - Verify stored procedure calls appear in output with timestamps
2. **Test Pause/Resume**:
   - Click "Pause Capture"
   - Execute more operations
   - Verify output freezes
   - Click "Resume Capture"
   - Verify new operations appear
3. **Test Auto-Truncation**:
   - Enable all trace categories
   - Perform many operations to exceed 1000 lines
   - Verify oldest 100 lines removed when limit hit
   - Verify recent context retained
4. **Test Save Log**:
   - Capture some output
   - Click "Save Log"
   - Choose file location
   - Verify file contains all captured output with timestamps
5. **Test Clear Output**:
   - Click "Clear" button
   - Verify output TextBox clears
   - Verify _debugLog list clears
   - Verify new traces appear after clear

**Acceptance**:
- [X] All 5 test scenarios pass
- [X] No exceptions or errors
- [X] Output formatting matches expectations (green text, timestamps)
- [X] Performance acceptable (<500ms output rendering)

**Estimated**: 30 minutes

---

### T020 - [US1] Add Debug Dashboard Progress Bar Integration
**Story**: US1 (Debug Dashboard)  
**Files**: `Controls/SettingsForm/Control_Developer_DebugDashboard.cs`  
**Priority**: P1 - LOW (nice to have)

**Description**:
Integrate Settings form progress bar for long-running operations (save log file).

**Reference**: T005 findings on progress bar usage pattern.

**Steps**:
1. Accept SettingsForm reference in constructor or property
2. Use progress bar when saving large log files:
   ```csharp
   _settingsForm?.ShowProgress("Saving debug log...");
   await File.WriteAllLinesAsync(filePath, _debugLog);
   _settingsForm?.HideProgress();
   _settingsForm?.UpdateStatus($"Log saved: {Path.GetFileName(filePath)}");
   ```
3. Test with large log file (>1000 lines)

**Acceptance**:
- [X] Progress bar shows during save operation
- [X] Status label updates on completion
- [X] No blocking of UI during file write

**Estimated**: 15 minutes

---

### T021 - [US1] Document Debug Dashboard Usage in Quickstart
**Story**: US1 (Debug Dashboard)  
**Files**: `quickstart.md` (already exists, verify completeness)  
**Priority**: P1 - MEDIUM

**Description**:
Verify Debug Dashboard section of quickstart.md is accurate and complete with screenshots.

**Reference**: `.github/instructions/documentation.instructions.md` + existing `quickstart.md`.

**Steps**:
1. Review existing Debug Dashboard section in quickstart.md
2. Add screenshots:
   - Debug Dashboard with active tracing (green output text)
   - Pause/Resume button states
   - Save Log dialog
3. Add keyboard shortcuts section if missing
4. Verify all test scenarios from T019 documented
5. Add troubleshooting section:
   - Output not appearing → Check Service_DebugTracer configuration
   - Performance issues → Reduce trace categories or pause capture

**Acceptance**:
- [X] Debug Dashboard section complete and accurate
- [X] Screenshots added (at least 2)
- [X] Keyboard shortcuts documented
- [X] Troubleshooting section exists

**Estimated**: 30 minutes

---

### T022 - [US1] Integration Test - Debug Dashboard End-to-End
**Story**: US1 (Debug Dashboard)  
**Files**: Manual testing (no code changes)  
**Priority**: P1 - HIGH

**Description**:
Execute comprehensive end-to-end test of Debug Dashboard user story acceptance criteria.

**Reference**: `spec.md` (User Story 1 Acceptance Scenarios).

**Test Scenarios** (from spec.md):
1. **Given** user has Developer role, **When** user opens Settings form, **Then** Developer category appears with Debug Dashboard child node ✓
2. **Given** Debug Dashboard is displayed, **When** user toggles "Database Operations" tracing, **Then** Service_DebugTracer updates and subsequent database calls appear in output ✓
3. **Given** Debug Dashboard is capturing, **When** user clicks "Pause Capture", **Then** output freezes but application continues ✓
4. **Given** Debug Dashboard has logs, **When** user clicks "Save Log", **Then** file dialog opens and log saves with timestamp ✓

**Additional Tests**:
- Navigate away from Debug Dashboard → ClearAsync() stops capture
- Navigate back to Debug Dashboard → ReloadAsync() starts capture
- Close Settings form → Debug Dashboard disposed properly
- Multiple Settings form open/close cycles → No memory leaks

**Acceptance**:
- [X] All 4 spec acceptance scenarios pass
- [X] All additional tests pass
- [X] No exceptions or errors
- [X] User Story 1 marked COMPLETE

**Estimated**: 45 minutes

---

**Phase 3 Complete**: User Story 1 (Debug Dashboard) fully implemented and tested. Independent test criteria met.

**Checkpoint**: Debug Dashboard accessible through Settings → Developer with full functionality.

---

## Phase 4: User Story 2 - Parameter Prefix Maintenance (P1)

**Goal**: Create CRUD interface for managing parameter prefix overrides, enabling gradual stored procedure standardization.

**Independent Test**: Developer can add/edit/delete parameter prefix overrides through Settings UI and see changes reflected in startup cache and stored procedure execution.

**Story Value**: Core requirement for Phase 2.5 parameter prefix standardization; blocks T113c/T113d completion in parent spec.

### T023 - [US2] Create Control_Developer_ParameterPrefixMaintenance UserControl
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: 
- Create: `Controls/SettingsForm/Control_Developer_ParameterPrefixMaintenance.cs`
- Create: `Controls/SettingsForm/Control_Developer_ParameterPrefixMaintenance.Designer.cs`

**Priority**: P1 - CRITICAL  
**Parallelizable**: [P] with T016 (US1), T036 (US3), T044 (US4), T053 (US5)

**Description**:
Create UserControl for managing parameter prefix overrides with CRUD interface.

**Reference**: `contracts/usercontrol-api.md` (Control_Developer_ParameterPrefixMaintenance section) + `.github/instructions/csharp-dotnet8.instructions.md`.

**Steps**:
1. Create new UserControl in Controls/SettingsForm/
2. Design UI layout:
   - DataGridView (top section) - displays all active overrides
   - Detail panel (bottom section) - shows selected override details
   - Toolbar buttons: Add Override, Edit Override, Delete Override, Refresh
3. Configure DataGridView columns:
   - OverrideId (hidden)
   - ProcedureName (200px, sortable)
   - ParameterName (150px, sortable)
   - OverridePrefix (100px)
   - Reason (300px, auto-size)
   - CreatedBy (100px)
   - CreatedDate (150px, DateTime format)
4. Add constructor with Core_Themes integration
5. Implement ReloadAsync() - loads overrides via Dao_ParameterPrefixOverrides.GetAllActiveAsync()
6. Add XML documentation

**Acceptance**:
- [X] UserControl exists and compiles
- [X] DataGridView configured with proper columns
- [X] Buttons present and styled
- [X] Core_Themes integration complete
- [X] ReloadAsync() loads data successfully

**Estimated**: 60 minutes

---

### T024 - [US2] Implement Add Override Dialog
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: 
- Create: `Forms/Development/AddParameterPrefixOverrideDialog.cs`
- Create: `Forms/Development/AddParameterPrefixOverrideDialog.Designer.cs`

**Priority**: P1 - CRITICAL

**Description**:
Create modal dialog for adding new parameter prefix overrides.

**Reference**: `quickstart.md` (Parameter Prefix Maintenance section - Add Override workflow).

**Steps**:
1. Create new Form (modal dialog)
2. Design layout:
   - Label + TextBox: Procedure Name (required, MaxLength=128)
   - Label + TextBox: Parameter Name (required, MaxLength=128, placeholder: "Without prefix")
   - Label + TextBox: Override Prefix (MaxLength=10, placeholder: "e.g., p_, in_, or empty")
   - Label + TextBox: Reason (optional, MaxLength=500, multiline)
   - Buttons: Save, Cancel
3. Add validation:
   - ProcedureName not empty
   - ParameterName not empty
   - OverridePrefix can be empty (valid scenario)
4. Add "Check Procedure Exists" button:
   - Queries INFORMATION_SCHEMA.ROUTINES
   - If not found, shows warning dialog: "Procedure not found. Continue anyway?"
   - Allows save even if procedure doesn't exist (per stakeholder decision)
5. Populate CreatedBy from Model_AppVariables.CurrentUser.UserName
6. Return Model_ParameterPrefixOverride on Save
7. Return null on Cancel

**Acceptance**:
- [X] Dialog exists and displays correctly
- [X] Validation works on required fields
- [X] Procedure existence check implemented
- [X] Warning dialog shows for non-existent procedures
- [X] CreatedBy auto-populated
- [X] Returns proper model object

**Estimated**: 75 minutes

---

### T025 - [US2] Implement Edit Override Dialog
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: 
- Create: `Forms/Development/EditParameterPrefixOverrideDialog.cs`
- Create: `Forms/Development/EditParameterPrefixOverrideDialog.Designer.cs`

**Priority**: P1 - HIGH

**Description**:
Create modal dialog for editing existing parameter prefix overrides.

**Reference**: Reuse Add Override Dialog with pre-population.

**Steps**:
1. Create new Form (or reuse Add dialog with edit mode flag)
2. Layout identical to Add dialog
3. Constructor accepts Model_ParameterPrefixOverride parameter
4. Pre-populate all fields from existing override
5. Show CreatedBy and CreatedDate (read-only labels)
6. Populate ModifiedBy from current user
7. Allow changing all fields including ProcedureName/ParameterName
8. Validation same as Add dialog
9. Return updated Model_ParameterPrefixOverride on Save

**Acceptance**:
- [X] Dialog exists and displays correctly
- [X] Fields pre-populated from existing override
- [X] Audit trail fields (CreatedBy/Date) shown as read-only
- [X] ModifiedBy auto-populated
- [X] Returns updated model object

**Estimated**: 45 minutes

---

### T026 - [US2] Implement Delete Override Confirmation
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: `Controls/SettingsForm/Control_Developer_ParameterPrefixMaintenance.cs`  
**Priority**: P1 - MEDIUM

**Description**:
Add delete confirmation dialog and execute soft delete via DAO.

**Reference**: `contracts/usercontrol-api.md` (DeleteOverrideAsync method).

**Steps**:
1. Add DeleteOverride button click handler
2. Validate selection (must have selected row)
3. Show confirmation dialog:
   ```csharp
   var result = Service_ErrorHandler.ShowConfirmation(
       $"Delete override for {override.ProcedureName}.{override.ParameterName}?\n\n" +
       "This is a soft delete - record will remain in database as inactive.",
       "Confirm Delete");
   ```
4. If Yes, call Dao_ParameterPrefixOverrides.DeleteAsync(overrideId)
5. If success, refresh DataGridView
6. Show success message in status label
7. Log action to Service_DebugTracer

**Acceptance**:
- [X] Confirmation dialog appears before delete
- [X] Soft delete executes successfully
- [X] DataGridView refreshes after delete
- [X] Deleted override no longer appears (IsActive = 0)
- [X] Status message confirms deletion

**Estimated**: 30 minutes

---

### T027 - [US2] Wire Up Add/Edit/Delete Button Handlers
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: `Controls/SettingsForm/Control_Developer_ParameterPrefixMaintenance.cs`  
**Priority**: P1 - HIGH

**Description**:
Connect all CRUD buttons to their respective dialog/DAO operations.

**Reference**: `contracts/usercontrol-api.md` (AddOverrideAsync, UpdateOverrideAsync, DeleteOverrideAsync methods).

**Steps**:
1. **Add Override Button**:
   - Open AddParameterPrefixOverrideDialog
   - If dialog returns model, call Dao_ParameterPrefixOverrides.AddAsync()
   - Handle success: Refresh DataGridView, show success status
   - Handle duplicate error (p_Status = -3): Show error dialog
   - Handle validation error (p_Status = -2): Show error dialog
2. **Edit Override Button**:
   - Get selected override from DataGridView
   - Open EditParameterPrefixOverrideDialog with existing override
   - If dialog returns updated model, call Dao_ParameterPrefixOverrides.UpdateAsync()
   - Handle same error cases as Add
3. **Delete Override Button**:
   - Already implemented in T026
4. **Refresh Button**:
   - Call ReloadAsync() to refresh from database
5. Add error handling with Service_ErrorHandler for all operations
6. Add logging for all CRUD operations

**Acceptance**:
- [X] Add button opens dialog and saves successfully
- [X] Edit button pre-populates dialog and updates successfully
- [X] Delete button soft-deletes successfully
- [X] Refresh button reloads data
- [X] Error handling works for all scenarios
- [X] All operations logged

**Estimated**: 60 minutes

---

### T028 - [US2] Add Procedure Name Autocomplete
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: 
- Modify: `Forms/Development/AddParameterPrefixOverrideDialog.cs`
- Modify: `Forms/Development/EditParameterPrefixOverrideDialog.cs`

**Priority**: P1 - LOW (nice to have)

**Description**:
Add autocomplete functionality to Procedure Name textbox for better UX.

**Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (WinForms Patterns section).

**Steps**:
1. Query INFORMATION_SCHEMA.ROUTINES on dialog load:
   ```csharp
   var query = "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES " +
               "WHERE ROUTINE_SCHEMA = 'mtm_wip_application' " +
               "ORDER BY ROUTINE_NAME";
   ```
2. Populate AutoCompleteStringCollection with procedure names
3. Set txtProcedureName.AutoCompleteMode = AutoCompleteMode.SuggestAppend
4. Set txtProcedureName.AutoCompleteSource = AutoCompleteSource.CustomSource
5. Assign custom source

**Acceptance**:
- [X] Autocomplete suggests matching procedures as user types
- [X] User can select from suggestions or type manually
- [X] Performance acceptable (<1 second to load suggestions)

**Estimated**: 30 minutes

---

### T029 - [US2] Add Parameter Name Autocomplete (Context-Aware)
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: 
- Modify: `Forms/Development/AddParameterPrefixOverrideDialog.cs`
- Modify: `Forms/Development/EditParameterPrefixOverrideDialog.cs`

**Priority**: P1 - LOW (nice to have)

**Description**:
Add context-aware autocomplete for Parameter Name based on selected procedure.

**Reference**: Similar to T028.

**Steps**:
1. Add txtProcedureName.TextChanged event handler
2. When valid procedure name entered, query INFORMATION_SCHEMA.PARAMETERS:
   ```csharp
   var query = "SELECT PARAMETER_NAME FROM INFORMATION_SCHEMA.PARAMETERS " +
               "WHERE SPECIFIC_SCHEMA = 'mtm_wip_application' " +
               "AND SPECIFIC_NAME = @ProcedureName " +
               "ORDER BY ORDINAL_POSITION";
   ```
3. Strip prefixes from parameter names (p_, in_, etc.) before adding to autocomplete
4. Populate txtParameterName AutoCompleteStringCollection
5. Handle case where procedure doesn't exist (no autocomplete suggestions)

**Acceptance**:
- [X] Autocomplete shows parameters from selected procedure
- [X] Prefixes stripped from suggestions (shows "UserID" not "p_UserID")
- [X] Updates dynamically when procedure name changes
- [X] Handles non-existent procedures gracefully

**Estimated**: 45 minutes

---

### T030 - [US2] Add TreeView Node for Parameter Prefix Maintenance
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: `Forms/Settings/SettingsForm.cs`  
**Priority**: P1 - CRITICAL

**Description**:
Add "Parameter Prefix Maintenance" child node under Developer category in Settings TreeView.

**Reference**: Similar to T017 (Add Debug Dashboard node).

**Steps**:
1. Locate Developer node creation code (from T017)
2. Add child node:
   ```csharp
   developerNode.Nodes.Add("Parameter Prefix Maintenance");
   ```
3. Add TreeView selection handler case:
   ```csharp
   "Parameter Prefix Maintenance" => new Control_Developer_ParameterPrefixMaintenance(),
   ```
4. Test navigation to Parameter Prefix Maintenance node

**Acceptance**:
- [X] Node appears under Developer category
- [X] Control loads when node selected
- [X] ReloadAsync() called on load
- [X] DataGridView populated with existing overrides

**Estimated**: 10 minutes

---

### T031 - [US2] Test Parameter Override End-to-End Workflow
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: Manual testing (no code changes)  
**Priority**: P1 - CRITICAL

**Description**:
Execute complete CRUD workflow and verify override application in Helper_Database_StoredProcedure.

**Reference**: `quickstart.md` (Parameter Prefix Maintenance section - Workflow Examples).

**Test Workflow**:
1. **Add Override for Legacy Procedure**:
   - Open Settings → Developer → Parameter Prefix Maintenance
   - Click "Add Override"
   - Enter: ProcedureName=sys_test_legacy, ParameterName=UserID, Prefix=in_, Reason=Test override
   - Save
   - Verify override appears in DataGridView
2. **Verify Override in Database**:
   - Query: `SELECT * FROM sys_parameter_prefix_overrides WHERE ProcedureName = 'sys_test_legacy'`
   - Verify record exists with IsActive=1
3. **Restart Application**:
   - Close and reopen MTM application
   - Login as JOHNK
   - Check logs for: "[DB] Loaded N parameter prefix overrides from database"
4. **Execute Stored Procedure with Override**:
   - Call sys_test_legacy (if exists) or create test procedure
   - Enable Debug Dashboard Database Operations tracing
   - Verify log shows: "[DB] Using override prefix 'in_' for sys_test_legacy.UserID"
5. **Edit Override**:
   - Select override in DataGridView
   - Click "Edit Override"
   - Change Reason to "Updated test override"
   - Save
   - Verify ModifiedBy and ModifiedDate populated
6. **Delete Override**:
   - Select override
   - Click "Delete Override"
   - Confirm deletion
   - Verify override disappears from DataGridView
   - Query database: `SELECT * FROM sys_parameter_prefix_overrides WHERE OverrideId = ?`
   - Verify IsActive=0 (soft deleted)

**Acceptance**:
- [X] All 6 workflow steps pass
- [X] Override cache loads at startup
- [X] Override applied during stored procedure execution
- [X] Audit trail fields populated correctly
- [X] Soft delete works (record preserved with IsActive=0)

**Estimated**: 60 minutes

---

### T032 - [US2] Test Duplicate Detection
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: Manual testing (no code changes)  
**Priority**: P1 - MEDIUM

**Description**:
Validate that duplicate procedure-parameter combinations are prevented by UNIQUE constraint.

**Test Scenarios**:
1. Add override: sys_test_proc, UserID, p_
2. Attempt duplicate add: sys_test_proc, UserID, in_ (different prefix, same proc+param)
3. Verify error dialog shows: "Override already exists for this procedure-parameter combination"
4. Verify p_Status = -3 returned from stored procedure
5. Edit existing override to create duplicate:
   - Create override: sys_test_proc2, UserID, p_
   - Edit sys_test_proc override to change ProcedureName to sys_test_proc2
   - Verify duplicate detection works on update

**Acceptance**:
- [X] Add duplicate prevented with user-friendly error
- [X] Update to duplicate prevented
- [X] UNIQUE constraint enforced at database level
- [X] Error messages clear and actionable

**Estimated**: 20 minutes

---

### T033 - [US2] Test Non-Existent Procedure Warning
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: Manual testing (no code changes)  
**Priority**: P1 - MEDIUM

**Description**:
Validate warning dialog when adding override for procedure that doesn't exist in INFORMATION_SCHEMA.

**Reference**: Stakeholder decision Q1 (warn but allow save for Production hotfix scenario).

**Test Scenarios**:
1. Click "Add Override"
2. Enter ProcedureName="nonexistent_procedure_12345"
3. Enter other required fields
4. Click "Check Procedure Exists" button (if implemented) or attempt Save
5. Verify warning dialog: "Procedure 'nonexistent_procedure_12345' not found in INFORMATION_SCHEMA. Continue anyway?"
6. Click "No" → dialog closes, no save
7. Click "Yes" → override saves successfully
8. Verify override works even though procedure doesn't exist (allows Production hotfix scenario)

**Acceptance**:
- [X] Warning shows for non-existent procedures
- [X] User can cancel save
- [X] User can proceed with save despite warning
- [X] Override functional for future procedure deployment

**Estimated**: 20 minutes

---

### T034 - [US2] Document Parameter Prefix Maintenance Usage
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: `quickstart.md` (verify completeness)  
**Priority**: P1 - MEDIUM

**Description**:
Verify Parameter Prefix Maintenance section of quickstart.md is complete with all workflows and screenshots.

**Reference**: `.github/instructions/documentation.instructions.md`.

**Steps**:
1. Review existing Parameter Prefix Maintenance section
2. Add screenshots:
   - DataGridView with sample overrides
   - Add Override dialog with filled fields
   - Edit Override dialog
   - Delete confirmation dialog
   - Warning dialog for non-existent procedure
3. Verify all workflows documented:
   - Add override for legacy procedure
   - Handle Production hotfix scenario
   - Edit existing override
   - Delete override (soft delete)
   - Find and fix non-standard procedures (query examples)
4. Add troubleshooting section:
   - Override not taking effect → Restart application
   - Duplicate error → Check existing overrides
   - Procedure not found warning → Verify procedure name spelling

**Acceptance**:
- [X] Parameter Prefix Maintenance section complete
- [X] All workflows documented with examples
- [X] Screenshots added (at least 5)
- [X] Troubleshooting section comprehensive

**Estimated**: 45 minutes

---

### T035 - [US2] Integration Test - Parameter Prefix Maintenance End-to-End
**Story**: US2 (Parameter Prefix Maintenance)  
**Files**: Manual testing (no code changes)  
**Priority**: P1 - CRITICAL

**Description**:
Execute comprehensive end-to-end test of Parameter Prefix Maintenance user story acceptance criteria.

**Reference**: `spec.md` (User Story 2 Acceptance Scenarios).

**Test Scenarios** (from spec.md):
1. **Given** user has Developer role, **When** user opens Settings → Developer → Parameter Prefix Maintenance, **Then** control displays existing overrides in DataGridView ✓
2. **Given** Parameter Prefix Maintenance is open, **When** user clicks "Add Override", **Then** dialog opens with fields for ProcedureName, ParameterName, OverridePrefix, Reason ✓
3. **Given** user enters valid override data, **When** user clicks "Save", **Then** override persists to database with audit trail ✓
4. **Given** override exists in database, **When** application restarts and executes that stored procedure, **Then** Helper_Database_StoredProcedure uses override prefix instead of convention ✓
5. **Given** user selects existing override, **When** user clicks "Delete", **Then** confirmation dialog appears and override is removed with audit log ✓

**Additional Tests**:
- Autocomplete suggestions work
- Duplicate detection works
- Non-existent procedure warning works
- Edit operation preserves CreatedBy/CreatedDate
- Soft delete preserves record with IsActive=0
- Cache reload works without application restart

**Acceptance**:
- [X] All 5 spec acceptance scenarios pass
- [X] All additional tests pass
- [X] No exceptions or errors
- [X] User Story 2 marked COMPLETE

**Estimated**: 60 minutes

---

**Phase 4 Complete**: User Story 2 (Parameter Prefix Maintenance) fully implemented and tested. Independent test criteria met. This completes T113c and T113d from parent spec.

**Checkpoint**: Parameter prefix override system operational. Override cache loads at startup. CRUD interface accessible through Settings → Developer.

---

## Phase 5: User Story 3 - Schema Inspector (P2)

**Goal**: Create read-only database schema browser for quick reference during development.

**Independent Test**: Developer can browse tables, view column details, and inspect stored procedure signatures without leaving the application.

**Story Value**: Reduces context switching during Phase 2.5 refactoring; eliminates need for external MySQL tools for schema queries.

### T036 - [US3] Create Control_Developer_SchemaInspector UserControl
**Story**: US3 (Schema Inspector)  
**Files**: `Controls/SettingsForm/Control_Developer_SchemaInspector.cs` + Designer  
**Priority**: P2 - MEDIUM  
**Parallelizable**: [P] with T016 (US1), T023 (US2), T044 (US4), T053 (US5)

**Description**: Create 3-panel layout: TreeView (left, schema objects), DataGridView (top-right, columns/params), TextBox (bottom-right, SQL definition).

**Reference**: `contracts/usercontrol-api.md` (Control_Developer_SchemaInspector section).

**Estimated**: 60 minutes

---

### T037 - [US3] Populate TreeView with Tables and Procedures
**Story**: US3 (Schema Inspector)  
**Files**: Modify Control_Developer_SchemaInspector.cs

**Description**: Query INFORMATION_SCHEMA.TABLES and .ROUTINES, populate TreeView with two root nodes: "Tables" and "Stored Procedures".

**Reference**: `.github/instructions/mysql-database.instructions.md`.

**Estimated**: 45 minutes

---

### T038 - [US3] Implement Table Selection Handler
**Story**: US3 (Schema Inspector)  
**Files**: Modify Control_Developer_SchemaInspector.cs

**Description**: On table node select, query INFORMATION_SCHEMA.COLUMNS and display in DataGridView with columns: Name, Type, Nullable, Key, Default.

**Estimated**: 30 minutes

---

### T039 - [US3] Implement Stored Procedure Selection Handler
**Story**: US3 (Schema Inspector)  
**Files**: Modify Control_Developer_SchemaInspector.cs

**Description**: On procedure node select, query INFORMATION_SCHEMA.PARAMETERS for parameter list and SHOW CREATE PROCEDURE for SQL definition.

**Estimated**: 40 minutes

---

### T040 - [US3] Add Search/Filter Functionality
**Story**: US3 (Schema Inspector)  
**Files**: Modify Control_Developer_SchemaInspector.cs

**Description**: Add search textbox above TreeView that filters nodes by name (case-insensitive contains match).

**Priority**: P2 - LOW (nice to have)

**Estimated**: 20 minutes

---

### T041 - [US3] Add Copy to Clipboard Buttons
**Story**: US3 (Schema Inspector)  
**Files**: Modify Control_Developer_SchemaInspector.cs

**Description**: Add "Copy Object Name" and "Copy SQL Definition" buttons for quick code generation workflow.

**Priority**: P2 - LOW (nice to have)

**Estimated**: 15 minutes

---

### T042 - [US3] Add TreeView Node and Integration
**Story**: US3 (Schema Inspector)  
**Files**: `Forms/Settings/SettingsForm.cs`

**Description**: Add "Schema Inspector" child node under Developer category, wire selection handler.

**Estimated**: 10 minutes

---

### T043 - [US3] Integration Test - Schema Inspector End-to-End
**Story**: US3 (Schema Inspector)  
**Files**: Manual testing

**Description**: Test all spec acceptance scenarios: TreeView navigation, column display, parameter display, SQL definition viewing, search functionality.

**Reference**: `spec.md` (User Story 3 Acceptance Scenarios).

**Estimated**: 30 minutes

---

**Phase 5 Complete**: User Story 3 (Schema Inspector) fully implemented and tested.

---

## Phase 6: User Story 4 - Procedure Call Hierarchy (P2)

**Goal**: Visualize stored procedure call relationships using existing analysis artifacts.

**Independent Test**: Developer can see which DAOs call which stored procedures and explore nested procedure calls.

**Story Value**: Critical for understanding refactoring impact and identifying orphaned procedures.

### T044 - [US4] Create Control_Developer_ProcedureCallHierarchy UserControl
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: `Controls/SettingsForm/Control_Developer_ProcedureCallHierarchy.cs` + Designer  
**Priority**: P2 - MEDIUM  
**Parallelizable**: [P] with T016 (US1), T023 (US2), T036 (US3), T053 (US5)

**Description**: Create layout with ComboBox (procedure selection), TreeView (call hierarchy), DataGridView (call site details).

**Reference**: `contracts/usercontrol-api.md` (Control_Developer_ProcedureCallHierarchy section).

**Estimated**: 60 minutes

---

### T045 - [US4] Load and Parse call-hierarchy-complete.json
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: Modify Control_Developer_ProcedureCallHierarchy.cs

**Description**: Read Database/call-hierarchy-complete.json at ReloadAsync(), parse JSON into internal data structures.

**Reference**: T002 artifact verification task.

**Estimated**: 30 minutes

---

### T046 - [US4] Populate Procedure Selection ComboBox
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: Modify Control_Developer_ProcedureCallHierarchy.cs

**Description**: Extract all procedure names from JSON, populate ComboBox with autocomplete, sorted alphabetically.

**Estimated**: 15 minutes

---

### T047 - [US4] Build TreeView Call Hierarchy from JSON
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: Modify Control_Developer_ProcedureCallHierarchy.cs

**Description**: When procedure selected, traverse JSON recursively to build TreeView showing: Procedure → Called By DAOs → Calls Other Procedures.

**Reference**: `quickstart.md` (Procedure Call Hierarchy section - Visualization example).

**Estimated**: 60 minutes

---

### T048 - [US4] Load and Display Call Site Details from CSV
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: Modify Control_Developer_ProcedureCallHierarchy.cs

**Description**: Read Database/STORED_PROCEDURE_CALLSITES.csv, filter by selected procedure, display in DataGridView (File, Line, Context columns).

**Estimated**: 30 minutes

---

### T049 - [US4] Add Orphaned Procedures Detection
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: Modify Control_Developer_ProcedureCallHierarchy.cs

**Description**: Add button "Find Orphaned Procedures" that queries INFORMATION_SCHEMA.ROUTINES, cross-references with call-hierarchy JSON, lists procedures with zero calls.

**Priority**: P2 - LOW (nice to have)

**Estimated**: 30 minutes

---

### T050 - [US4] Add Export Call Hierarchy Button
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: Modify Control_Developer_ProcedureCallHierarchy.cs

**Description**: Add button "Export to Excel" that uses ClosedXML to generate .xlsx with sheets: All Procedures, Orphaned Procedures, Call Sites.

**Priority**: P2 - LOW (nice to have)

**Estimated**: 45 minutes

---

### T051 - [US4] Add TreeView Node and Integration
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: `Forms/Settings/SettingsForm.cs`

**Description**: Add "Procedure Call Hierarchy" child node under Developer category.

**Estimated**: 10 minutes

---

### T052 - [US4] Integration Test - Procedure Call Hierarchy End-to-End
**Story**: US4 (Procedure Call Hierarchy)  
**Files**: Manual testing

**Description**: Test all spec acceptance scenarios: JSON loading, hierarchy visualization, call site display, orphaned procedure detection.

**Reference**: `spec.md` (User Story 4 Acceptance Scenarios).

**Estimated**: 30 minutes

---

**Phase 6 Complete**: User Story 4 (Procedure Call Hierarchy) fully implemented and tested.

---

## Phase 7: User Story 5 - Code Generator (P3)

**Goal**: Generate DAO method boilerplate from stored procedure signatures.

**Independent Test**: Developer can select procedure, click generate, and get ready-to-use DAO method code.

**Story Value**: Reduces boilerplate coding during Phase 3-5 DAO refactoring, ensures consistency.

### T053 - [US5] Create Control_Developer_CodeGenerator UserControl
**Story**: US5 (Code Generator)  
**Files**: `Controls/SettingsForm/Control_Developer_CodeGenerator.cs` + Designer  
**Priority**: P3 - LOW  
**Parallelizable**: [P] with T016 (US1), T023 (US2), T036 (US3), T044 (US4)

**Description**: Create layout with ComboBox (procedure selection), RadioButtons (template type), TextBox (generated code, multiline, monospace), Buttons (Generate, Copy to Clipboard).

**Reference**: `contracts/usercontrol-api.md` (Control_Developer_CodeGenerator section).

**Estimated**: 45 minutes

---

### T054 - [US5] Implement Procedure Signature Extraction
**Story**: US5 (Code Generator)  
**Files**: Modify Control_Developer_CodeGenerator.cs

**Description**: Query INFORMATION_SCHEMA.PARAMETERS for selected procedure, extract parameter list with types and direction (IN/OUT).

**Estimated**: 30 minutes

---

### T055 - [US5] Implement DAO Method Template Generator
**Story**: US5 (Code Generator)  
**Files**: Modify Control_Developer_CodeGenerator.cs

**Description**: Generate async DAO method code with DaoResult pattern, Helper_Database_StoredProcedure call, parameter dictionary, error handling, XML docs.

**Reference**: `quickstart.md` (Code Generator section - Template examples).

**Estimated**: 60 minutes

---

### T056 - [US5] Implement Test Method Template Generator
**Story**: US5 (Code Generator)  
**Files**: Modify Control_Developer_CodeGenerator.cs

**Description**: Generate MSTest test method skeleton with [TestMethod], Arrange/Act/Assert structure, DaoResult assertions.

**Estimated**: 45 minutes

---

### T057 - [US5] Implement Model Class Template Generator
**Story**: US5 (Code Generator)  
**Files**: Modify Control_Developer_CodeGenerator.cs

**Description**: Generate Model POCO with properties matching procedure return columns (from table or example ResultSet query).

**Priority**: P3 - LOW (optional)

**Estimated**: 45 minutes

---

### T058 - [US5] Add Template Customization Options
**Story**: US5 (Code Generator)  
**Files**: Modify Control_Developer_CodeGenerator.cs

**Description**: Add CheckBoxes for options: Include XML Docs, Include Error Handling, Use var vs explicit types, Async suffix convention.

**Priority**: P3 - LOW (nice to have)

**Estimated**: 30 minutes

---

### T059 - [US5] Add TreeView Node and Integration
**Story**: US5 (Code Generator)  
**Files**: `Forms/Settings/SettingsForm.cs`

**Description**: Add "Code Generator" child node under Developer category.

**Estimated**: 10 minutes

---

### T060 - [US5] Integration Test - Code Generator End-to-End
**Story**: US5 (Code Generator)  
**Files**: Manual testing

**Description**: Test all spec acceptance scenarios: procedure selection, template generation, code compilation, copy to clipboard.

**Reference**: `spec.md` (User Story 5 Acceptance Scenarios).

**Estimated**: 30 minutes

---

**Phase 7 Complete**: User Story 5 (Code Generator) fully implemented and tested.

---

## Phase 8: Control_Database Integration

**Goal**: Integrate SchemaInspector into existing Control_Database.

### T061 - [US3+] Add Schema Inspector Tab to Control_Database
**Story**: US3 extension  
**Files**: `Controls/SettingsForm/Control_Database.cs`

**Description**: Add new tab "Schema Inspector" to existing Control_Database TabControl, host Control_Developer_SchemaInspector as child control.

**Reference**: `spec.md` (Integration with Control_Database section).

**Estimated**: 20 minutes

---

### T062 - [US3+] Test Control_Database Tab Integration
**Story**: US3 extension  
**Files**: Manual testing

**Description**: Verify Schema Inspector accessible from Settings → Database (non-developer users) and Settings → Developer → Schema Inspector (developer users). Verify identical functionality in both locations.

**Estimated**: 15 minutes

---

### T063 - [US3+] Update Documentation for Dual Access
**Story**: US3 extension  
**Files**: `quickstart.md`

**Description**: Document that Schema Inspector accessible from two locations with different audience purposes.

**Estimated**: 10 minutes

---

**Phase 8 Complete**: Schema Inspector integrated into Control_Database for broader user access.

---

## Phase 9: Polish & Integration

**Goal**: Final integration, documentation, and quality assurance.

### T064 - Add Developer Tools to MainForm Help Menu
**Story**: Integration  
**Files**: `Forms/MainForm/MainForm.cs`

**Description**: Add "Developer Tools" submenu under Help menu with keyboard shortcuts: Debug Dashboard (F9), Parameter Prefix Maintenance (F10), Schema Inspector (F11), Call Hierarchy (F12), Code Generator (Shift+F12).

**Priority**: LOW (nice to have)

**Estimated**: 30 minutes

---

### T065 - Update Help System HTML Documentation
**Story**: Integration  
**Files**: `Documentation/Help/` (HTML files)

**Description**: Add Developer Tools section to HTML help system with usage guide for each tool, screenshots, keyboard shortcuts.

**Reference**: `.github/instructions/documentation.instructions.md`.

**Estimated**: 60 minutes

---

### T066 - Update AGENTS.md with Developer Tools Context
**Story**: Integration  
**Files**: `AGENTS.md`

**Description**: Add Developer Tools Suite section to AGENTS.md with tool descriptions, use cases, keyboard shortcuts for agent awareness.

**Estimated**: 15 minutes

---

### T067 - Create Developer Tools User Guide
**Story**: Documentation  
**Files**: `Documentation/Guides/DEVELOPER_TOOLS_GUIDE.md`

**Description**: Create comprehensive user guide covering all 5 tools with workflows, examples, troubleshooting, FAQs.

**Estimated**: 90 minutes

---

### T068 - Execute Comprehensive Integration Test Suite
**Story**: Quality Assurance  
**Files**: Manual testing

**Description**: Execute all 5 user story integration tests (T022, T035, T043, T052, T060) in sequence to verify no regression or conflicts.

**Estimated**: 90 minutes

---

### T069 - Performance Testing - Developer Tools Load Time
**Story**: Quality Assurance  
**Files**: Manual testing

**Description**: Measure ReloadAsync() performance for each tool, verify <2 second load time, optimize if needed.

**Reference**: `.github/instructions/performance-optimization.instructions.md`.

**Estimated**: 30 minutes

---

### T070 - Update Parent Spec tasks.md with Completion Status
**Story**: Documentation  
**Files**: `../tasks.md`

**Description**: Mark T113c and T113d as COMPLETE in parent spec tasks.md, add reference to this feature spec for details.

**Estimated**: 5 minutes

---

**Phase 9 Complete**: Developer Tools Suite fully polished, documented, and integrated.

---

## Task Summary

| Phase | Story | Tasks | Status |
|-------|-------|-------|--------|
| 1 | Setup | T001-T005 | ⬜ Not Started |
| 2 | Foundational | T006-T015 | ⬜ Not Started |
| 3 | US1 (Debug Dashboard) | T016-T022 | ⬜ Not Started |
| 4 | US2 (Parameter Prefix) | T023-T035 | ⬜ Not Started |
| 5 | US3 (Schema Inspector) | T036-T043 | ⬜ Not Started |
| 6 | US4 (Call Hierarchy) | T044-T052 | ⬜ Not Started |
| 7 | US5 (Code Generator) | T053-T060 | ⬜ Not Started |
| 8 | Control_Database | T061-T063 | ⬜ Not Started |
| 9 | Polish | T064-T070 | ⬜ Not Started |
| **Total** | **70 tasks** | **~52 hours** | **0% complete** |

---

## Parallel Execution Strategy

**Phase 2 (Foundational) MUST complete before ANY user story begins.**

After Phase 2, all 5 user stories are independent and can be developed in parallel:

```
Team Member A           Team Member B
─────────────────       ─────────────────
T016-T022 (US1)         T023-T035 (US2)
T036-T043 (US3)         T044-T052 (US4)
T053-T060 (US5)         T061-T063 (US3+)
```

**Estimated Timeline** (2 developers):
- **Week 1**: Phase 1-2 (Setup + Foundational) - Sequential - 2-3 days
- **Week 2-3**: Phase 3-7 (All User Stories) - Parallel - 8-10 days
- **Week 4**: Phase 8-9 (Integration + Polish) - Sequential - 3-4 days

---

## MVP Scope

For minimum viable product, implement only:
- Phase 1 (Setup)
- Phase 2 (Foundational)
- Phase 3 (US1 - Debug Dashboard)
- Phase 4 (US2 - Parameter Prefix Maintenance)

**Estimated MVP**: 25 tasks, ~22 hours, 1 week (2 developers)

This MVP scope completes the critical path for T113c/T113d in parent spec, enabling Phase 2.5 parameter prefix standardization work to proceed.

---

## Success Criteria

**Feature Complete** when:
- [X] All 5 user stories pass integration tests
- [X] All tools accessible through Settings → Developer
- [X] Developer role gates all tools correctly
- [X] Documentation complete (quickstart.md, user guide, help system)
- [X] No compilation warnings or errors
- [X] Performance targets met (<2s load time per tool)
- [X] Parent spec T113c and T113d marked complete

---

**End of Task Breakdown**

