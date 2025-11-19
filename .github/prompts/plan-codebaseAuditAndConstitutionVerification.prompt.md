## Plan: Comprehensive Documentation & Codebase Health Check

This plan aims to synchronize the codebase with the "Constitution" (documentation), fill missing instruction gaps, and identify/resolve architectural discrepancies.

### Steps
1. **Create Missing Instructions**:
   - Create `.github/instructions/dao-patterns.instructions.md` (Define `Model_Dao_Result`, `Helper_Database_StoredProcedure` usage, Async patterns).
   - Create `.github/instructions/stored-procedures.instructions.md` (MySQL 5.7 constraints, Naming conventions `md_`/`inv_`/`sys_`, Parameter prefixes).
   - Create `.github/instructions/helper-utilities.instructions.md` (Static vs Instance, Naming `Helper_`).

2. **Update Existing Documentation**:
   - Update `.github/copilot-instructions.md` to include `Service_DebugTracer` patterns found in code.

3. **Run Health Check (Code Discrepancies)**:
   - Scan for `MessageBox.Show` (Forbidden).
   - Scan for direct `MySqlCommand` usage outside `Helper_Database_StoredProcedure`.
   - Scan for `static class Dao_*` vs `public class Dao_*` (Architecture mismatch).
   - Scan for missing `BaseIntegrationTest`.
   - Scan for `Form` inheritance (must be `ThemedForm`).

4. **Generate Workflows**:
   - Create `.github/workflows/new-dao-implementation.md`.
   - Create `.github/workflows/new-stored-procedure.md`.

### Code Discrepancies & Action Items
- [ ] **DAO Architecture Mismatch**: `copilot-instructions.md` specifies Instance-based DAOs, but `Dao_Inventory` is `static`. **Action**: Decide on standard and refactor or update docs.
- [ ] **Error Handling**: `Dao_Inventory` uses `Dao_ErrorLog` directly instead of `Service_ErrorHandler`. **Action**: Refactor to use `Service_ErrorHandler`.
- [ ] **Missing Test Base**: `BaseIntegrationTest` is referenced in docs but missing in file list. **Action**: Locate or recreate. - Remove references if this pretains to unit tests only.
- [ ] **Tracing**: `Service_DebugTracer` is heavily used but undocumented. **Action**: Add to `service-logging.instructions.md` or new file.
- [ ] **Forbidden UI**: Check for any remaining `MessageBox.Show` calls.

### Further Considerations
1. Should we enforce Dependency Injection for DAOs (requires refactoring static DAOs to instance)? If this only affects the dao files then yes, but if a full refactor is needed across multiple layers, we may need to reconsider.
2. Should `Service_DebugTracer` be integrated into `Service_ErrorHandler` or kept separate? What would the benefits be of each approach?

### Tasks Generated from Audit (2025-11-19)

#### 1. Cleanup Legacy Async Parameters
- **Target**: `Data/Dao_Part.cs`, `Data/Dao_Inventory.cs`
- **Action**: Remove `bool useAsync` parameter from all methods. It is ignored by `Helper_Database_StoredProcedure` which is always async.
- **Files**:
  - `Data/Dao_Part.cs`: `InsertPart`, `UpdatePart`, `DeletePart`, `GetAllParts`, `GetPartByNumber`, `PartExists`, `GetPartTypes`
  - `Data/Dao_Inventory.cs`: `GetAllInventoryAsync`, `SearchInventoryAsync`, `GetInventoryByPartIdAsync`, `GetInventoryByPartIdAndOperationAsync`, `AddInventoryItemAsync`, `RemoveInventoryItemsFromDataGridViewAsync`, `RemoveInventoryItemAsync`

#### 2. Refactor Forbidden UI Logic
- **Target**: `Services/Service_ErrorHandler.cs`, `Program.cs`, `Forms/ViewLogs/PromptStatusManagerDialog.cs`
- **Action**: Replace `MessageBox.Show` with `Service_ErrorHandler.ShowUserError` or `Service_ErrorHandler.ShowUserMessage`.
- **Note**: `Service_ErrorHandler.cs` itself uses `MessageBox.Show` internally - this is the *only* allowed place, but it should be reviewed to ensure it's not overused.
- **Critical**: `Program.cs` has raw `MessageBox.Show` calls in exception handlers. These should use `Service_ErrorHandler`.

#### 3. Refactor Dao_ErrorLog
- **Target**: `Data/Dao_ErrorLog.cs` (and all callers)
- **Action**: `Dao_ErrorLog` currently mixes data access with UI logic (`HandleException_GeneralError_CloseApp`).
- **Plan**:
  1. Create `Service_ErrorHandling_Logic` (or similar) to contain the business logic of *how* to handle errors (logging + UI).
  2. Reduce `Dao_ErrorLog` to *only* database operations (`InsertError`, `GetErrors`).
  3. Update all `Dao_*.cs` files to call `Service_ErrorHandler` instead of `Dao_ErrorLog.HandleException...`.

#### 4. Service_ErrorHandler Refactor
- **Target**: `Services/Service_ErrorHandler.cs`
- **Action**: Centralize all error handling logic, currently spread across `Dao_ErrorLog` and `Service_ErrorHandler`.
- **Plan**:
  1. Move relevant code from `Dao_ErrorLog` to `Service_ErrorHandler`.
  2. Ensure `Service_ErrorHandler` has methods for all necessary error handling paths (e.g., logging, user notifications).
  3. Update all calls from `Dao_ErrorLog` to the new centralized methods in `Service_ErrorHandler`.

#### 5. Document Service_DebugTracer Usage
- **Target**: `Services/Service_DebugTracer.cs`
- **Action**: Comprehensive documentation of `Service_DebugTracer` usage patterns and examples.
- **Output**: Update `service-logging.instructions.md` or create a new dedicated document.

#### 6. Dependency Injection Feasibility Study --- DONE
- **Target**: Entire codebase
- **Action**: Analyze the impact of enforcing Dependency Injection for DAOs.
- **Decision**: **Hybrid Approach**. Legacy DAOs remain static. New DAOs must be Instance/DI. Documentation updated.

#### 7. Inline SQL Cleanup
- **Target**: `Services/Service_ErrorReportQueue.cs`, `Services/Service_OnStartup_StartupSplashApplicationContext.cs`, `Services/Service_ErrorReportSync.cs`, `Forms/ErrorDialog/Form_ReportIssue.cs`
- **Action**: Found inline SQL ("SELECT ...").
- **Task**: Move these queries to Stored Procedures and call them via `Helper_Database_StoredProcedure`.
- **Specifics**:
  - `Service_ErrorReportQueue.cs`: `SELECT @status AS Status...`
  - `Service_OnStartup_StartupSplashApplicationContext.cs`: `SELECT VERSION()`
  - `Service_ErrorReportSync.cs`: `SELECT 1` (Connectivity check?) - Y
  - `Form_ReportIssue.cs`: `SELECT 1`

#### 8. UserControl Inheritance
- **Target**: `Controls/**/*.cs`
- **Action**: Verify all UserControls inherit from `ThemedUserControl`.
- **Finding**: `grep` for `:\s*UserControl\b` returned no matches, which suggests they might already be migrated or the regex missed them.
- **Task**: Manual spot check of 3-5 random controls to confirm `ThemedUserControl` inheritance.

#### 9. Hardcoded Paths
- **Target**: `**/*.cs`
- **Action**: `grep` for `[a-zA-Z]:\\` returned no matches.
- **Task**: Verify `Helper_LogPath` is used for all file operations and no other hardcoded paths exist (e.g. `C:\MAMP`).

#### 10. Service Pattern Verification
- **Target**: `Services/`
- **Action**:
  - `Service_DebugTracer.cs`: Needs documentation (already noted).
  - `Service_ErrorReportQueue.cs`: Contains inline SQL (noted above).
  - `Service_OnStartup_*.cs`: Check if these should be in a `Startup` folder or if the naming convention is sufficient.
