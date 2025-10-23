# Feature Specification: Developer Tools Suite Integration

**Parent Spec**: `002-003-database-layer-complete`  
**Feature Branch**: `002-003-database-layer-complete`  
**Sub-Feature ID**: `002-003-001`  
**Created**: 2025-10-18  
**Status**: Draft

---

## Overview

The MTM WIP Application requires a centralized Developer Tools suite accessible through the Settings interface. This sub-feature consolidates existing developer utilities, removes obsolete tools, and introduces new developer-focused functionality to support database layer standardization, debugging, and maintenance workflows.

Currently, developer tools are scattered across standalone forms (`Forms/Development/`) with inconsistent access patterns. This feature integrates developer tools into the Settings form's hierarchical navigation, adds new parameter prefix maintenance capabilities, and removes deprecated dependency chart tools.

---

## Parent Feature Context

This sub-feature supports **Phase 2.5 Part C (Refactoring & Tooling)** of the Comprehensive Database Layer Standardization initiative, specifically tasks:
- **T113c** – Implement Developer role & prefix override table
- **T113d** – Build parameter prefix maintenance form (Developer tools)

The parent feature requires developer-accessible tooling for:
- Parameter prefix override management (stored procedure standardization)
- Real-time debug monitoring during refactoring validation
- Database schema inspection and stored procedure analysis

---

## Stakeholder Decisions (2025-10-18)

All technical decisions have been finalized through stakeholder clarification questionnaire. Key decisions:

| Decision Point | Selected Option | Rationale |
|----------------|----------------|-----------|
| **Parameter Prefix Override Scope** | Database-specific (separate per environment) | Prevents Production contamination from Development experiments; environment isolation critical |
| **Debug Dashboard Output Persistence** | Ephemeral only (cleared on close) | Logs contain sensitive business data; explicit save required for retention |
| **Schema Inspector Edit Capabilities** | Read-only (view metadata only) | Prevents accidental schema modifications; database changes require formal migration scripts |
| **Code Generator Scope** | Methods only (add to existing DAOs) | Avoids creating duplicate DAOs; integrates with existing DAO architecture |
| **Developer Tools in Production** | Available with warning dialog | Supports Production troubleshooting; warning prevents casual access |
| **Parameter Prefix Override Validation** | Warn but allow save | Flexible for Production hotfixes; allows saving overrides for procedures not yet in Dev |
| **Debug Dashboard Output Limit** | Auto-truncate oldest 100 lines when 1000 reached | Prevents unbounded memory growth; retains recent context |
| **Schema Inspector Connection Failure** | Show error with retry button | Graceful degradation; allows immediate retry without closing control |
| **Code Generator Complex Procedures** | Generate with warning comment | Acknowledges complexity; includes manual review recommendation |
| **Procedure Call Hierarchy Missing Artifacts** | Show message with "Regenerate" button | User-friendly recovery; button launches PowerShell script automatically |
| **Developer Role Check Timing** | When Settings form opens | Single check at form initialization; prevents repeated database queries |
| **Parameter Prefix Override Audit Trail** | Full (Created + Modified tracking) | Complete audit history; tracks who made changes and when |
| **Schema Inspector Refresh Frequency** | Manual refresh only (F5 keyboard shortcut) | Avoids slow INFORMATION_SCHEMA queries on every navigation; user-controlled |
| **Code Generator Template Selection** | Single standard template (MTM pattern only) | Consistency over flexibility; all DAOs follow same pattern |
| **TreeView Category Ordering** | After "About" (last in list) | Developer tools accessed infrequently; keeps primary categories visible |

**Additional Requirements from Stakeholder**:
- Run MySQL script to grant JOHNK Admin + Developer roles before implementation testing
- Integrate existing Control_Database into Developer category (move from Database category)
- Add "Regenerate" button to Procedure Call Hierarchy alongside error message for missing artifacts

---

## User Scenarios & Testing

### User Story 1 - Developer Accesses Debug Dashboard from Settings (Priority: P1)

A developer needs to monitor real-time application activity while testing database refactoring changes.

**Why this priority**: Essential for validating stored procedure refactoring and debugging integration issues.

**Independent Test**: Developer can open Settings → Developer → Debug Dashboard, enable database tracing, and see real-time stored procedure execution logs.

**Acceptance Scenarios**:

1. **Given** user has Developer role (Admin + Developer privilege), **When** user opens Settings form, **Then** Developer category appears in TreeView with Debug Dashboard child node
2. **Given** Debug Dashboard is displayed in Settings, **When** user toggles "Database Operations" tracing, **Then** Service_DebugTracer configuration updates and subsequent database calls appear in output
3. **Given** Debug Dashboard is capturing output, **When** user clicks "Pause Capture", **Then** output freezes but application continues running
4. **Given** Debug Dashboard has captured logs, **When** user clicks "Save Log", **Then** file dialog opens and log saves to selected location with timestamp

---

### User Story 2 - Developer Manages Parameter Prefix Overrides (Priority: P1)

A developer discovers a stored procedure with non-standard parameter naming and needs to create an override without modifying the procedure itself.

**Why this priority**: Core requirement for Phase 2.5 parameter prefix standardization; blocks T113c/T113d completion.

**Independent Test**: Developer can add/edit/delete parameter prefix overrides through Settings UI and see changes reflected in startup cache.

**Acceptance Scenarios**:

1. **Given** user has Developer role, **When** user opens Settings → Developer → Parameter Prefix Maintenance, **Then** control displays existing overrides in DataGridView
2. **Given** Parameter Prefix Maintenance is open, **When** user clicks "Add Override", **Then** dialog opens with fields for ProcedureName, ParameterName, OverridePrefix, Reason
3. **Given** user enters valid override data, **When** user clicks "Save", **Then** override persists to `sys_parameter_prefix_overrides` table with audit trail (User, Timestamp)
4. **Given** override exists in database, **When** application restarts and executes that stored procedure, **Then** Helper_Database_StoredProcedure uses override prefix instead of convention
5. **Given** user selects existing override, **When** user clicks "Delete", **Then** confirmation dialog appears and override is removed with audit log entry

---

### User Story 3 - Developer Inspects Database Schema Metadata (Priority: P2)

A developer needs to verify current database schema state during deployment validation.

**Why this priority**: Supports deployment validation workflows; useful but not blocking for Phase 2.5 completion.

**Independent Test**: Developer can view table structures, stored procedure lists, and INFORMATION_SCHEMA data through Settings interface.

**Acceptance Scenarios**:

1. **Given** user has Developer role, **When** user opens Settings → Developer → Schema Inspector, **Then** control displays list of all tables with row counts
2. **Given** Schema Inspector is open, **When** user selects a table, **Then** column details display (Name, Type, Nullable, Key)
3. **Given** Schema Inspector is open, **When** user switches to "Stored Procedures" tab, **Then** list of all procedures displays with parameter counts
4. **Given** user selects a stored procedure, **When** user clicks "View Parameters", **Then** INFORMATION_SCHEMA.PARAMETERS data displays with detected prefixes

---

### User Story 4 - Developer Views Stored Procedure Call Hierarchy (Priority: P2)

A developer needs to understand procedure dependencies before refactoring.

**Why this priority**: Improves refactoring safety; supports dependency analysis but not required for T113 completion.

**Independent Test**: Developer can search for a procedure and see all call sites plus called procedures.

**Acceptance Scenarios**:

1. **Given** user has Developer role, **When** user opens Settings → Developer → Procedure Call Hierarchy, **Then** control displays search box and results area
2. **Given** Procedure Call Hierarchy is open, **When** user searches for "inv_inventory_Add_Item", **Then** results show C# call sites (file, line number) and called procedures
3. **Given** search results are displayed, **When** user clicks a C# file reference, **Then** option to open file in associated editor appears
4. **Given** procedure has dependencies, **When** user clicks "Show Dependency Tree", **Then** hierarchical view displays with depth levels

---

### User Story 5 - Developer Generates Stored Procedure Wrapper Code (Priority: P3)

A developer needs to quickly scaffold a new DAO method for a newly created stored procedure.

**Why this priority**: Quality-of-life improvement; reduces boilerplate but not essential for Phase 2.5.

**Independent Test**: Developer can select a stored procedure and generate compliant C# DAO method code.

**Acceptance Scenarios**:

1. **Given** user has Developer role, **When** user opens Settings → Developer → Code Generator, **Then** control displays dropdown of stored procedures
2. **Given** Code Generator is open, **When** user selects "inv_inventory_Transfer", **Then** procedure parameters load from INFORMATION_SCHEMA
3. **Given** procedure parameters are loaded, **When** user clicks "Generate DAO Method", **Then** C# code appears in output box following Helper_Database_StoredProcedure patterns
4. **Given** generated code is displayed, **When** user clicks "Copy to Clipboard", **Then** code is copied for pasting into DAO class

---

### Edge Cases

- What happens when user without Developer role tries to access Developer category? (Should not appear in TreeView)
- How does system handle parameter prefix overrides for non-existent procedures? (Validation at save time; warning if procedure not found)
- What happens when Debug Dashboard output exceeds 1000 lines? (Auto-truncate oldest 100 lines)
- How does Schema Inspector handle connection failures? (Show error message with retry button)
- What happens when Code Generator encounters procedures with 10+ parameters? (Generate with warning about review needed)

---

## Requirements

### Functional Requirements

#### FR-001: Developer Role Management
- **FR-001.1**: System MUST check user has Developer role (requires Admin role + Developer flag in `usr_users` table)
- **FR-001.2**: Settings form MUST show/hide Developer TreeView category based on role check
- **FR-001.3**: Developer controls MUST perform role check on load and display "Access Denied" if role missing

#### FR-002: Settings Form Integration
- **FR-002.1**: Settings form TreeView MUST include "Developer" parent node with child nodes for each tool
- **FR-002.2**: Developer category MUST appear after "About" in TreeView hierarchy
- **FR-002.3**: Each developer tool MUST be implemented as a UserControl inheriting from base pattern
- **FR-002.4**: Developer controls MUST integrate with Settings form progress bar and status label

#### FR-003: Debug Dashboard Integration
- **FR-003.1**: System MUST convert `DebugDashboardForm` to `Control_Developer_DebugDashboard` UserControl
- **FR-003.2**: Debug Dashboard MUST maintain all existing functionality (tracing toggles, log save/clear, pause/resume)
- **FR-003.3**: Debug Dashboard MUST use Settings form's panel for display (remove standalone Form chrome)
- **FR-003.4**: Debug Dashboard MUST expose public `ReloadAsync()` method for Settings form integration

#### FR-004: Parameter Prefix Maintenance
- **FR-004.1**: System MUST create `sys_parameter_prefix_overrides` table with columns: OverrideId (PK), ProcedureName, ParameterName, OverridePrefix, Reason, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
- **FR-004.2**: System MUST create stored procedure `sys_parameter_prefix_overrides_Get_All` returning all overrides
- **FR-004.3**: System MUST create stored procedure `sys_parameter_prefix_overrides_Add` with input validation
- **FR-004.4**: System MUST create stored procedure `sys_parameter_prefix_overrides_Update_ById` with audit trail
- **FR-004.5**: System MUST create stored procedure `sys_parameter_prefix_overrides_Delete_ById` with soft delete support
- **FR-004.6**: System MUST create `Control_Developer_ParameterPrefixMaintenance` UserControl with CRUD operations
- **FR-004.7**: Helper_Database_StoredProcedure MUST query override table at startup and cache results
- **FR-004.8**: Helper_Database_StoredProcedure MUST apply overrides before INFORMATION_SCHEMA lookup

#### FR-005: Schema Inspector
- **FR-005.1**: System MUST create `Control_Developer_SchemaInspector` UserControl
- **FR-005.2**: Schema Inspector MUST display tables list with row counts via `INFORMATION_SCHEMA.TABLES`
- **FR-005.3**: Schema Inspector MUST display selected table columns via `INFORMATION_SCHEMA.COLUMNS`
- **FR-005.4**: Schema Inspector MUST display stored procedures list via `INFORMATION_SCHEMA.ROUTINES`
- **FR-005.5**: Schema Inspector MUST display selected procedure parameters via `INFORMATION_SCHEMA.PARAMETERS`
- **FR-005.6**: Schema Inspector MUST include refresh button to reload schema metadata

#### FR-006: Procedure Call Hierarchy Viewer
- **FR-006.1**: System MUST create `Control_Developer_ProcedureCallHierarchy` UserControl
- **FR-006.2**: Call Hierarchy MUST load `Database/call-hierarchy-complete.json` on initialization
- **FR-006.3**: Call Hierarchy MUST provide search functionality with autocomplete
- **FR-006.4**: Call Hierarchy MUST display C# call sites from `STORED_PROCEDURE_CALLSITES.csv`
- **FR-006.5**: Call Hierarchy MUST display called procedures from dependency graph
- **FR-006.6**: Call Hierarchy MUST show dependency depth (root procedures, leaf procedures, circular dependencies)

#### FR-007: Code Generator
- **FR-007.1**: System MUST create `Control_Developer_CodeGenerator` UserControl
- **FR-007.2**: Code Generator MUST load stored procedure list from INFORMATION_SCHEMA.ROUTINES
- **FR-007.3**: Code Generator MUST query INFORMATION_SCHEMA.PARAMETERS for selected procedure
- **FR-007.4**: Code Generator MUST generate C# DAO method code following MTM patterns:
  - Async Task<DaoResult> or Task<DaoResult<T>> return type
  - Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync call
  - Dictionary<string, object> parameter construction (without p_ prefix)
  - XML documentation comments
  - Try-catch with Dao_ErrorLog.HandleException_GeneralError_CloseApp
- **FR-007.5**: Code Generator MUST provide "Copy to Clipboard" functionality

#### FR-008: Obsolete Form Removal
- **FR-008.1**: System MUST delete `Forms/Development/DependencyChartViewer/` directory and all contents
- **FR-008.2**: System MUST delete `Forms/Development/DependencyChartConverter/` directory and all contents
- **FR-008.3**: System MUST remove any references to DependencyChartViewerForm and DependencyChartConverterForm from MainForm or other launching points
- **FR-008.4**: System MUST update any documentation referencing removed forms

---

### Key Entities

- **Developer Role**: User permission level (Admin + Developer flag) enabling access to developer tools category
- **Parameter Prefix Override**: Database record defining non-standard parameter prefix for specific stored procedure parameter
- **Schema Metadata**: INFORMATION_SCHEMA data representing current database structure (tables, columns, procedures, parameters)
- **Procedure Call Hierarchy**: Dependency graph showing which procedures call other procedures and which C# code invokes procedures
- **Generated DAO Code**: C# method scaffold conforming to MTM coding standards and using Helper_Database_StoredProcedure patterns

---

## Success Criteria

### Measurable Outcomes

- **SC-001**: Developer with appropriate role can access all 5 developer tools through Settings → Developer category within 3 clicks
- **SC-002**: Parameter prefix override workflow (add/edit/delete) completes in under 2 minutes including validation
- **SC-003**: Debug Dashboard captures and displays database trace output with less than 500ms latency
- **SC-004**: Schema Inspector loads and displays all tables/procedures within 5 seconds on standard development database
- **SC-005**: Code Generator produces valid, compilable C# DAO method code for 95% of stored procedures
- **SC-006**: Procedure Call Hierarchy search returns results within 2 seconds for any procedure name
- **SC-007**: Parameter prefix overrides persist across application restarts and are applied correctly to stored procedure calls
- **SC-008**: Obsolete dependency chart forms are completely removed with zero remaining references in codebase
- **SC-009**: 100% of developer tools include proper error handling via Service_ErrorHandler patterns
- **SC-010**: All developer tools respect Settings form progress bar and status label integration patterns

---

## Technical Notes

### Database Schema Changes

New table required for parameter prefix overrides:

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
    UNIQUE KEY UQ_ProcParam (ProcedureName, ParameterName)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

### Helper_Database_StoredProcedure Enhancement

Add startup cache loading:

```csharp
private static Dictionary<string, Dictionary<string, string>>? _parameterPrefixOverrides;

public static async Task LoadParameterPrefixOverridesAsync()
{
    var result = await ExecuteDataTableWithStatusAsync(
        Model_AppVariables.ConnectionString,
        "sys_parameter_prefix_overrides_Get_All",
        null);
    
    if (result.IsSuccess && result.Data != null)
    {
        _parameterPrefixOverrides = new Dictionary<string, Dictionary<string, string>>();
        foreach (DataRow row in result.Data.Rows)
        {
            // Build nested dictionary: procedure -> parameter -> prefix
        }
    }
}
```

### UserControl Naming Convention

All developer tools follow pattern: `Control_Developer_{ToolName}`

Example hierarchy:
- Control_Developer_DebugDashboard
- Control_Developer_ParameterPrefixMaintenance
- Control_Developer_SchemaInspector
- Control_Developer_ProcedureCallHierarchy
- Control_Developer_CodeGenerator

---

## Out of Scope

- Automated testing of generated DAO code (developer must validate)
- Real-time schema change detection (manual refresh required)
- Parameter prefix override import/export functionality
- Integration with external code editors (VS Code, Visual Studio)
- Advanced dependency visualization (beyond tree view)
- Procedure performance profiling tools
- Automated refactoring suggestions
- Database migration script generation

---

## Dependencies

- Parent feature `002-003-database-layer-complete` Phase 2.5 must be in progress
- Settings form must be accessible and stable
- Service_DebugTracer must be functional for Debug Dashboard integration
- INFORMATION_SCHEMA queries must return valid data for Schema Inspector
- Existing database analysis artifacts must be present (call-hierarchy-complete.json, STORED_PROCEDURE_CALLSITES.csv)
- Helper_Database_StoredProcedure must support parameter prefix override injection point

---

## Risks & Mitigation

| Risk | Impact | Mitigation |
|------|--------|------------|
| Settings form UserControl integration breaks existing controls | High | Create controls in isolation first, test integration separately |
| Parameter prefix overrides conflict with INFORMATION_SCHEMA data | Medium | Override takes precedence, log conflicts for review |
| Debug Dashboard causes performance degradation | Medium | Implement capture pause/limit, async output updates |
| Generated code doesn't compile due to edge cases | Low | Include disclaimer, provide manual review checklist |
| call-hierarchy-complete.json file missing or corrupted | Low | Graceful degradation, show message to regenerate artifacts |

---

## Open Questions

1. Should parameter prefix overrides be database-specific or shared across environments? **(Suggested: Database-specific for safety)**
2. Should Debug Dashboard output be persisted across application sessions? **(Suggested: No, ephemeral only)**
3. Should Schema Inspector support editing schema or read-only? **(Suggested: Read-only, use MySQL Workbench for changes)**
4. Should Code Generator support creating entire DAO classes or just methods? **(Suggested: Methods only to avoid overwriting existing code)**
5. Should developer tools be available in Production mode or Development only? **(Suggested: Available but with warning dialog in Production)**

