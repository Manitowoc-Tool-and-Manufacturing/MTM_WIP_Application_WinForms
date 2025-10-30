# Implementation Plan: Developer Tools Suite Integration

**Branch**: `002-003-database-layer-complete` | **Date**: 2025-10-18 | **Spec**: [spec.md](./spec.md)  
**Parent Feature**: [002-003-database-layer-complete](../spec.md) | **Sub-Feature ID**: `002-003-001`

---

## Summary

Integrate developer tools into the Settings form hierarchical navigation system, converting existing DebugDashboardForm to UserControl, removing obsolete dependency chart utilities, and implementing five new developer-focused tools: Parameter Prefix Maintenance, Schema Inspector, Procedure Call Hierarchy Viewer, and Code Generator. This sub-feature supports Phase 2.5 Part C (Refactoring & Tooling) by providing developer-accessible utilities for database layer standardization, debugging, and stored procedure analysis.

**Primary Technical Approach**: Convert standalone Forms to UserControls following existing Settings form integration patterns (Control_Add_User, Control_Theme, etc.). Implement Developer role checking at Settings form initialization. Create new `sys_parameter_prefix_overrides` database table with CRUD stored procedures. Extend Helper_Database_StoredProcedure to load and apply parameter prefix overrides at startup.

---

## Technical Context

**Language/Version**: C# 12, .NET 8.0 Windows Forms  
**Primary Dependencies**: 
- MySql.Data 9.4.0 (database connectivity)
- System.Text.Json (JSON parsing for call hierarchy artifacts)
- Existing MTM infrastructure (Core_Themes, Helper_Database_StoredProcedure, Service_ErrorHandler)

**Storage**: MySQL 5.7.24+ (MAMP compatible)
- New table: `sys_parameter_prefix_overrides` (parameter override storage)
- New stored procedures: 5 CRUD operations for override management
- Read-only access to `INFORMATION_SCHEMA` (Tables, Columns, Routines, Parameters)

**Testing**: Manual validation approach (no automated unit tests per MTM standards)
- Exercise Settings form integration with Developer role enabled
- Validate CRUD operations on parameter prefix overrides
- Test Schema Inspector with live database connection
- Verify Code Generator output compiles in DAO classes

**Target Platform**: Windows 10/11 Desktop (.NET 8.0 Windows Forms)  
**Project Type**: WinForms desktop application (existing project structure)

**Performance Goals**:
- Settings form TreeView expansion with Developer category: <100ms response time
- Parameter prefix override cache loading at startup: <500ms
- Schema Inspector table list query: <5 seconds for 50+ table database
- Code Generator method generation: <1 second for procedures with <20 parameters
- Debug Dashboard output rendering: <500ms for 100-line batches

**Constraints**:
- Must integrate with existing Settings form UserControl pattern
- Cannot break existing Settings categories (User, Theme, Database, About)
- Must respect Developer role checking (Admin + Developer flag)
- Parameter prefix overrides must be database-specific (no cross-environment sharing)
- All database operations must use Helper_Database_StoredProcedure patterns
- Must follow WinForms designer compatibility (no AXAML/XAML patterns)

**Scale/Scope**:
- 5 developer tool UserControls (DebugDashboard, ParameterPrefixMaintenance, SchemaInspector, ProcedureCallHierarchy, CodeGenerator)
- 1 database table + 5 stored procedures for parameter overrides
- 2 forms to remove (DependencyChartViewerForm, DependencyChartConverterForm)
- 1 existing UserControl to refactor (Control_Database → integrate into Developer category)
- TreeView structure: 1 parent node (Developer) + 6 child nodes
- Estimated 74+ stored procedures to potentially override during Phase 2.5

---

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Simplicity Gates

✅ **Single Project Rule**: Feature operates within existing MTM_WIP_Application_Winforms.csproj project. No new projects introduced.

✅ **Flat Structure Rule**: Developer tools live in existing `Controls/SettingsForm/` directory alongside other setting controls. No new nested hierarchies.

⚠️ **Avoid Premature Patterns**: 
- **Concern**: Introducing parameter prefix override system adds abstraction layer over INFORMATION_SCHEMA queries
- **Justification**: Required for Phase 2.5 gradual migration strategy. Without overrides, must refactor all 74 procedures at once (high risk). Override table enables incremental standardization with tracking.
- **Simpler Alternative Rejected**: "Just fix all procedures now" - rejected because multi-week all-or-nothing refactoring blocks other Phase 2.5 work and creates deployment coordination issues across environments.

✅ **Avoid Gold Plating**: Feature implements only tools needed for Phase 2.5 support (parameter overrides, schema inspection, debug monitoring). Advanced features like automated refactoring, migration script generation, and performance profiling are explicitly out of scope.

### Quality Gates

✅ **Error Handling**: All developer tools use Service_ErrorHandler for exceptions, connection failures, and validation errors. Schema Inspector and Procedure Call Hierarchy degrade gracefully with friendly messages when artifacts missing.

✅ **Testing Strategy**: Manual validation through Settings form access. Exercise CRUD operations on parameter overrides. Test code generation output by compiling in DAO class. Validate Debug Dashboard tracing with Service_DebugTracer.

✅ **Documentation Requirements**: 
- XML comments on all public UserControl methods
- Quickstart.md with developer onboarding workflow
- Data model documentation for sys_parameter_prefix_overrides table
- Inline comments explaining override application logic in Helper_Database_StoredProcedure

### Consistency Gates

✅ **Existing Patterns**: Follows WinForms UserControl integration pattern established by Control_Add_User, Control_Theme, Control_Database. Uses Settings form progress bar and status label helpers. Applies Core_Themes.ApplyDpiScaling in constructors.

✅ **Code Style**: Adheres to C# region organization (Fields, Properties, Constructors, Button Clicks, Helpers, Cleanup). Uses async/await for database operations. Follows naming conventions (Control_Developer_{ToolName}).

✅ **Technology Stack**: Uses existing MySQL 5.7, MySql.Data connector, Helper_Database_StoredProcedure abstractions. No new database engines or data access libraries introduced.

---

## Project Structure

### Documentation (this sub-feature)

```
specs/002-003-database-layer-complete/002-003-001-developer-tools-suite/
├── spec.md              # Feature specification (COMPLETE)
├── clarifications.html  # Stakeholder decision questionnaire (COMPLETE)
├── plan.md              # This file (IN PROGRESS)
├── research.md          # Phase 0 output (NOT YET CREATED)
├── data-model.md        # Phase 1 output (NOT YET CREATED)
├── quickstart.md        # Phase 1 output (NOT YET CREATED)
├── contracts/           # Phase 1 output (NOT YET CREATED)
│   ├── stored-procedures.md  # Stored procedure contracts
│   └── usercontrol-api.md    # UserControl public interface contracts
└── tasks.md             # Phase 2 output (NOT YET CREATED - will be integrated into parent tasks.md)
```

### Source Code (repository root)

```
MTM_WIP_Application_Winforms/
├── Controls/
│   └── SettingsForm/
│       ├── Control_Developer_DebugDashboard.cs           # [NEW] Converted from DebugDashboardForm
│       ├── Control_Developer_DebugDashboard.Designer.cs  # [NEW]
│       ├── Control_Developer_ParameterPrefixMaintenance.cs  # [NEW] CRUD for overrides
│       ├── Control_Developer_ParameterPrefixMaintenance.Designer.cs  # [NEW]
│       ├── Control_Developer_SchemaInspector.cs          # [NEW] INFORMATION_SCHEMA viewer
│       ├── Control_Developer_SchemaInspector.Designer.cs # [NEW]
│       ├── Control_Developer_ProcedureCallHierarchy.cs   # [NEW] Dependency graph viewer
│       ├── Control_Developer_ProcedureCallHierarchy.Designer.cs  # [NEW]
│       ├── Control_Developer_CodeGenerator.cs            # [NEW] DAO method scaffolding
│       ├── Control_Developer_CodeGenerator.Designer.cs   # [NEW]
│       ├── Control_Database.cs                           # [REFACTOR] Move into Developer category
│       └── Control_Database.Designer.cs                  # [REFACTOR]
│
├── Forms/
│   ├── Settings/
│   │   └── SettingsForm.cs                               # [MODIFY] Add Developer TreeView node
│   └── Development/                                      # [DELETE DIRECTORY]
│       ├── DebugDashboardForm.cs                         # [DELETE] Converted to UserControl
│       ├── DependencyChartViewer/                        # [DELETE] Obsolete tool
│       └── DependencyChartConverter/                     # [DELETE] Obsolete tool
│
├── Helpers/
│   └── Helper_Database_StoredProcedure.cs                # [MODIFY] Add override cache loading
│
├── Data/
│   ├── Dao_ParameterPrefixOverrides.cs                   # [NEW] CRUD DAO for override table
│   └── Dao_System.cs                                     # [MODIFY] Add Developer role check method
│
├── Models/
│   └── Model_ParameterPrefixOverride.cs                  # [NEW] POCO for override records
│
└── Database/
    └── UpdatedStoredProcedures/
        └── ReadyForVerification/
            ├── sys_parameter_prefix_overrides_Get_All.sql        # [NEW]
            ├── sys_parameter_prefix_overrides_Get_ById.sql       # [NEW]
            ├── sys_parameter_prefix_overrides_Add.sql            # [NEW]
            ├── sys_parameter_prefix_overrides_Update_ById.sql    # [NEW]
            └── sys_parameter_prefix_overrides_Delete_ById.sql    # [NEW]
```

**Structure Decision**: Feature integrates into existing `Controls/SettingsForm/` directory following established UserControl pattern. All developer tools are peer controls alongside existing setting controls (Control_Add_User, Control_Theme, etc.). Database artifacts (stored procedures) go into UpdatedStoredProcedures/ReadyForVerification/ per Phase 2.5 workflow. Obsolete Forms/Development/ directory is completely removed.

---

## Complexity Tracking

*Justification for Constitution Check warning (Avoid Premature Patterns)*

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| Parameter Prefix Override System | Enables gradual migration of 74+ procedures from inconsistent prefixes (p_, in_, none) to standard p_ prefix without blocking Phase 2.5 progress. Provides fallback when INFORMATION_SCHEMA unavailable. Supports Production hotfixes that aren't yet in Dev database. | **Alternative 1 - "Fix all procedures now"**: Rejected because multi-week effort blocks other Phase 2.5 tasks, requires coordinated deployment across environments, and creates high-risk big-bang release. **Alternative 2 - "Live with inconsistency"**: Rejected because violates Phase 2.5 standardization goals and perpetuates Helper_Database_StoredProcedure convention guessing that led to Phase 2.5 discovery work. **Alternative 3 - "Configuration file approach"**: Rejected per stakeholder decision (Q1) - database-specific storage provides environment isolation and prevents Production contamination from Development experiments. |

---

## Phase 0: Research & Decision Points

### Resolved Through Stakeholder Clarifications (2025-10-18)

All technical decisions have been resolved through the clarification questionnaire. Key decisions:

1. **Parameter Prefix Override Scope** → Database-specific (separate per environment)
2. **Debug Dashboard Output Persistence** → Ephemeral only (cleared on close)
3. **Schema Inspector Edit Capabilities** → Read-only (view metadata only)
4. **Code Generator Scope** → Methods only (add to existing DAOs)
5. **Developer Tools Availability in Production** → Available with warning dialog
6. **Parameter Prefix Override Validation** → Warn but allow save
7. **Debug Dashboard Output Limit** → Auto-truncate oldest 100 lines
8. **Schema Inspector Connection Failure** → Show error with retry button
9. **Code Generator Complex Procedures** → Generate with warning comment
10. **Procedure Call Hierarchy Missing Artifacts** → Show message + "Regenerate" button
11. **Developer Role Check Timing** → When Settings form opens
12. **Parameter Prefix Override Audit Trail** → Full (Created + Modified tracking)
13. **Schema Inspector Refresh Frequency** → Manual refresh only (F5 keyboard shortcut)
14. **Code Generator Template Selection** → Single standard template (MTM pattern)
15. **TreeView Category Ordering** → After "About" (last in list), integrate Control_Database into Developer category

### Additional Clarifications from Stakeholder

- **JOHNK User Setup**: Run MySQL script to grant JOHNK Admin + Developer roles before implementation to enable manual testing
- **Control_Database Integration**: Refactor existing Control_Database to move under Developer category (currently under Database category)
- **Regenerate Button**: Add "Regenerate" button to Procedure Call Hierarchy control alongside friendly error message when artifacts missing

No further research required. Proceeding to Phase 1 (Design & Contracts).

---

## Phase 1: Design & Contracts

### Entities (see data-model.md)

1. **ParameterPrefixOverride** - Database record defining non-standard parameter prefix for specific procedure parameter
2. **DeveloperRole** - User permission level (Admin + Developer flag) enabling developer tools access
3. **SchemaMetadata** - INFORMATION_SCHEMA data (Tables, Columns, Routines, Parameters)
4. **ProcedureCallHierarchy** - Dependency graph from call-hierarchy-complete.json
5. **GeneratedDaoCode** - C# method scaffold conforming to MTM patterns

### Contracts (see contracts/ directory)

1. **Stored Procedure Contracts** - 5 CRUD procedures for sys_parameter_prefix_overrides table
2. **UserControl Public API Contracts** - Public methods exposed by each developer tool control for Settings form integration

### Quickstart (see quickstart.md)

Developer onboarding guide covering:
- How to enable Developer role for testing user
- How to access Developer tools through Settings form
- How to add/edit/delete parameter prefix overrides
- How to use Code Generator to scaffold DAO methods
- How to interpret Schema Inspector output
- How to navigate Procedure Call Hierarchy dependencies

---

## Phase 2: Task Breakdown

Tasks will be integrated into parent `specs/002-003-database-layer-complete/tasks.md` under refined T113c and T113d:

### T113c - Implement Developer Role & Infrastructure
- Create sys_parameter_prefix_overrides table
- Create 5 CRUD stored procedures
- Create Model_ParameterPrefixOverride POCO
- Create Dao_ParameterPrefixOverrides DAO class
- Modify Dao_System to add Developer role check method
- Modify Helper_Database_StoredProcedure to load override cache at startup
- Create SQL script to grant JOHNK Admin + Developer roles
- Update SettingsForm TreeView with Developer category
- Write integration tests for override CRUD operations

### T113d - Build Developer Tools User Controls
- Convert DebugDashboardForm to Control_Developer_DebugDashboard
- Create Control_Developer_ParameterPrefixMaintenance (CRUD UI)
- Create Control_Developer_SchemaInspector (INFORMATION_SCHEMA viewer)
- Create Control_Developer_ProcedureCallHierarchy (dependency graph + regenerate button)
- Create Control_Developer_CodeGenerator (DAO method scaffolding)
- Refactor Control_Database to integrate into Developer category
- Delete Forms/Development/DependencyChartViewer directory
- Delete Forms/Development/DependencyChartConverter directory
- Update any MainForm references to removed forms
- Manual validation testing of all 5+1 developer tools

---

## Implementation Order

### Sprint 1: Foundation (T113c)
1. Database schema and stored procedures
2. Model and DAO classes for parameter overrides
3. Helper_Database_StoredProcedure override cache integration
4. Developer role checking infrastructure
5. JOHNK user setup script
6. SettingsForm TreeView Developer category addition

### Sprint 2: Core Tools (T113d Part 1)
1. Convert DebugDashboardForm to UserControl
2. Create ParameterPrefixMaintenance control (highest priority for Phase 2.5)
3. Integrate Control_Database into Developer category
4. Manual validation of parameter override workflow

### Sprint 3: Analysis Tools (T113d Part 2)
1. Create SchemaInspector control
2. Create ProcedureCallHierarchy control with regenerate button
3. Create CodeGenerator control
4. Delete obsolete dependency chart forms
5. Comprehensive manual validation suite

---

## Success Metrics

- ✅ Developer with appropriate role can access all 6 developer tools through Settings → Developer
- ✅ Parameter prefix override CRUD workflow completes in under 2 minutes
- ✅ Debug Dashboard captures database traces with <500ms latency
- ✅ Schema Inspector loads all tables/procedures within 5 seconds
- ✅ Code Generator produces compilable C# code for 95% of procedures
- ✅ Procedure Call Hierarchy search returns results within 2 seconds
- ✅ Parameter prefix overrides persist across application restarts
- ✅ Obsolete dependency chart forms completely removed
- ✅ All developer tools include proper Service_ErrorHandler integration
- ✅ Settings form progress bar and status label work with all developer tools

---

## Risk Mitigation

| Risk | Mitigation Strategy |
|------|---------------------|
| Settings form UserControl integration breaks existing controls | Create controls in isolation first, test individually before SettingsForm integration |
| Parameter prefix overrides conflict with INFORMATION_SCHEMA | Override takes precedence, log conflicts to Logging/DebugTracer for review |
| Debug Dashboard causes performance degradation | Implement 1000-line auto-truncation, pause/resume capture, async output rendering |
| Generated code doesn't compile | Include XML comment disclaimer, provide manual review checklist in quickstart.md |
| call-hierarchy-complete.json missing | Graceful degradation with regenerate button, instructions in error message |
| TreeView expansion breaks existing Settings navigation | Test all existing categories after Developer node addition, verify no layout shifts |

---

## Dependencies

- Parent feature `002-003-database-layer-complete` Phase 2.5 must be in progress
- Settings form must be stable and accessible
- Service_DebugTracer must be functional for Debug Dashboard
- INFORMATION_SCHEMA queries must work for Schema Inspector
- Database analysis artifacts must exist (`call-hierarchy-complete.json`, `STORED_PROCEDURE_CALLSITES.csv`)
- Helper_Database_StoredProcedure must support override injection point

---

## Deployment Notes

### Database Migration Script

```sql
-- Run against: mtm_wip_application_winforms_test (Development)
--          and: MTM_WIP_Application_Winforms (Production)

-- 1. Create parameter prefix overrides table
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

-- 2. Grant JOHNK Developer role (Development environment only)
UPDATE usr_users
SET IsDeveloper = 1
WHERE UserName = 'JOHNK' AND IsAdmin = 1;
```

### Application Deployment Checklist

1. ✅ Deploy stored procedures to database (5 CRUD procedures)
2. ✅ Run database migration script (create table, grant JOHNK developer role)
3. ✅ Build application with new developer controls
4. ✅ Verify Settings form TreeView shows Developer category for JOHNK
5. ✅ Test parameter prefix override CRUD operations
6. ✅ Validate Debug Dashboard tracing functionality
7. ✅ Confirm obsolete dependency chart forms removed from deployment package
8. ✅ Update any user documentation referencing removed forms

---

## Post-Implementation Validation

### Manual Test Scenarios

1. **Developer Role Access**
   - Log in as JOHNK (Admin + Developer)
   - Open Settings form
   - Verify Developer category appears after About
   - Verify 6 child nodes visible (DebugDashboard, ParameterPrefixMaintenance, SchemaInspector, ProcedureCallHierarchy, CodeGenerator, Database)

2. **Parameter Prefix Override Workflow**
   - Navigate to Settings → Developer → Parameter Prefix Maintenance
   - Click "Add Override"
   - Enter test procedure name, parameter, prefix, reason
   - Save override
   - Restart application
   - Verify override appears in loaded cache
   - Call stored procedure and confirm override applied

3. **Debug Dashboard Integration**
   - Navigate to Settings → Developer → Debug Dashboard
   - Enable Database Operations tracing
   - Execute inventory adjustment in main application
   - Verify stored procedure execution appears in dashboard output
   - Click "Pause Capture" and confirm output freezes
   - Click "Save Log" and verify file saves with timestamp

4. **Schema Inspector Functionality**
   - Navigate to Settings → Developer → Schema Inspector
   - Verify table list loads within 5 seconds
   - Select a table and verify columns display
   - Switch to Stored Procedures tab
   - Select a procedure and verify parameters display
   - Click Refresh (F5) and confirm data reloads

5. **Code Generator Output**
   - Navigate to Settings → Developer → Code Generator
   - Select stored procedure from dropdown
   - Click "Generate DAO Method"
   - Copy generated code to clipboard
   - Paste into temporary DAO class
   - Build project and verify code compiles

6. **Procedure Call Hierarchy**
   - Navigate to Settings → Developer → Procedure Call Hierarchy
   - Search for "inv_inventory_Add_Item"
   - Verify C# call sites display with file paths
   - Verify called procedures display in dependency tree
   - Test "Show Dependency Tree" visualization

---

## Next Steps

After this plan is approved:

1. **Generate Phase 1 artifacts** (data-model.md, contracts/, quickstart.md)
2. **Update AGENTS.md** with developer tools context
3. **Create detailed tasks.md** breakdown with time estimates
4. **Begin Sprint 1 implementation** (database foundation)
5. **Continuous manual validation** after each control completion
