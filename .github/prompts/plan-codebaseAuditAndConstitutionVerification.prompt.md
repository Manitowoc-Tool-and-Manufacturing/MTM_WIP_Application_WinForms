## Plan: Comprehensive Documentation & Codebase Health Check

This plan aims to synchronize the codebase with the "Constitution" (documentation), fill missing instruction gaps, and identify/resolve architectural discrepancies.

### Steps
1. **Create Missing Instructions**:
   - Create `.github/instructions/dao-patterns.instructions.md` (Define `Model_Dao_Result`, `Helper_Database_StoredProcedure` usage, Async patterns).
   - Create `.github/instructions/stored-procedures.instructions.md` (MySQL 5.7 constraints, Naming conventions `md_`/`inv_`/`sys_`, Parameter prefixes).
   - Create `.github/instructions/testing-standards.instructions.md` (Integration testing patterns, `BaseIntegrationTest` usage).
   - Create `.github/instructions/helper-utilities.instructions.md` (Static vs Instance, Naming `Helper_`).

2. **Update Existing Documentation**:
   - Update `.github/copilot-instructions.md` to include `Service_DebugTracer` patterns found in code.
   - Update `.github/copilot-instructions.md` to clarify DAO patterns (Instance vs Static discrepancy).

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

### Verification Questions (Pending User Approval)
1. **DAO Pattern Mismatch**: The codebase uses `static class Dao_*` (e.g., `Dao_Inventory`), but documentation specifies `public class Dao_*` (Instance-based).
   - **Option A**: Update documentation to reflect the `static` pattern (Match Code).
   - **Option B**: Refactor codebase to use Instance/DI pattern (Match Docs).
   - *Recommendation*: Option A for immediate consistency, Option B for long-term architecture.
   If Option B only requires the dao files to be refactored, then we can go with that. otherwise, Option A is preferred.

2. **Error Handling Architecture**: `Dao_ErrorLog` currently violates separation of concerns by handling both Database logging and UI (`MessageBox.Show`).
   - **Question**: Should we refactor `Dao_ErrorLog` to be a pure Data Access Object and move all UI logic to `Service_ErrorHandler`?
   Yes, as long as it only affects the dao and service layers.

3. **Forbidden UI Cleanup**: Detected ~20+ instances of `MessageBox.Show` in `Controls/SettingsForm/` and `Dao_ErrorLog.cs`.
   - **Question**: Authorize bulk refactor to replace all `MessageBox.Show` with `Service_ErrorHandler.ShowUserError()`?
   Read what each MessageBox.Show is doing first to ensure that it is appropriate to replace it with ShowUserError().

4. **Missing Test Infrastructure**: `BaseIntegrationTest` is referenced in docs but missing from the workspace.
   - **Question**: Create `BaseIntegrationTest.cs` with transaction rollback support?
   Integration Tests were removed, so we should remove references to it in the documentation.

5. **Service_DebugTracer**: Found extensive usage of `Service_DebugTracer` but no documentation.
   - **Question**: Create new `.github/instructions/service-debug-tracer.instructions.md` to standardize its usage?
    Yes, create the new instructions file to document its usage.

6. **Stored Procedure Naming Standards**: Found `usr_` prefix in `Dao_System` (e.g., `usr_settings_Get_All`) which is not in the proposed standard (`md_`, `inv_`, `sys_`).
   - **Question**: Add `usr_` to the allowed prefixes list?
    Yes, add `usr_` to the allowed prefixes list.

7. **Legacy Async Parameter**: DAOs accept `bool useAsync = true` but `Helper_Database_StoredProcedure` ignores it (always async).
   - **Question**: Remove `bool useAsync` parameter from all DAO methods to clean up the API?
    Yes, remove the parameter to clean up the API.

8. **Unthemed Forms**: Detected ~10 forms inheriting directly from `Form` (e.g., `Transactions`, `SplashScreenForm`).
   - **Question**: Create a task to migrate all remaining forms to `ThemedForm`?
   Yes, but not SplashScreenForm as it is run at startup before themes are applied.

9. **Deprecated Helper Methods**: `Helper_Database_StoredProcedure` contains deprecated compatibility wrappers.
   - **Question**: Schedule removal of deprecated wrappers (marked "remove after Phase 7")?
   Just did this manually

10. **JSON Standard**: `Dao_System` uses `System.Text.Json`.
    - **Question**: Standardize on `System.Text.Json` and document it?
    Yes, standardize on System.Text.Json and document it.