# Checklist: Parameter Prefix Maintenance Form Requirements Quality

**Phase**: Part C - Stored Procedure Refactoring (T113d)  
**Purpose**: Validate that Parameter Prefix Maintenance Form requirements are complete, clear, and measurable  
**Type**: Requirements Quality Validation (NOT implementation verification)  
**Created**: 2025-10-15

---

## Overview

This checklist validates the **quality of Parameter Prefix Maintenance Form requirements** as defined in FR-023, FR-028, SC-017, and T113d. It does NOT test the implementation—that happens during execution. Use this checklist during Checkpoint 2 review (after T113d planning, before execution begins).

**Target Audience**: UI/UX Lead, Architect, Lead Developer  
**When to Use**: Before starting T113d execution, to ensure requirements are ready  
**Pass Criteria**: All sections score ≥80% (individual items can fail if documented)

---

## Section 1: Requirement Completeness

### 1.1 UserControl Structure

- [ ] **Control name specified**: Control_Settings_ParameterPrefixMaintenance following MTM naming convention
- [ ] **Namespace specified**: Controls.SettingsForm namespace for Settings dialog integration
- [ ] **Base class specified**: Inherits from UserControl (standard WinForms pattern)
- [ ] **Designer file**: Includes .Designer.cs for Visual Studio designer compatibility
- [ ] **Size constraints**: MinimumSize and PreferredSize documented (e.g., 800x600 minimum for DataGridView)
- [ ] **Docking behavior**: Dock=Fill to integrate into Settings Form tab container

**Score**: ___ / 6 (requires ≥5 pass)

### 1.2 DataGridView Configuration

- [ ] **Column count**: 6 columns specified (ProcedureName, ParameterName, CurrentPrefix, OverridePrefix, Reason, ModifiedDate)
- [ ] **Column widths**: Auto-size mode documented (ProcedureName=200px, ParameterName=150px, CurrentPrefix=100px, OverridePrefix=100px, Reason=250px, ModifiedDate=130px)
- [ ] **Column types**: Text columns use DataGridViewTextBoxColumn, no custom cell types needed
- [ ] **Read-only columns**: CurrentPrefix and ModifiedDate marked ReadOnly=True (derived data, not editable)
- [ ] **Required field validation**: ProcedureName, ParameterName, OverridePrefix required (validation on CellEndEdit event)
- [ ] **Sorting enabled**: AllowUserToOrderColumns=True, initial sort by ProcedureName ascending
- [ ] **Selection mode**: FullRowSelect for CRUD operations targeting entire records
- [ ] **Data binding**: Uses BindingSource connected to List<ParameterPrefixOverride> for two-way binding

**Score**: ___ / 8 (requires ≥6 pass)

### 1.3 CRUD Button Configuration

- [ ] **Add button**: btnAdd with icon, positioned top-left of button panel, Text="Add Override"
- [ ] **Edit button**: btnEdit with icon, enabled only when row selected, Text="Edit Override"
- [ ] **Delete button**: btnDelete with icon, enabled only when row selected, Text="Delete Override", confirmation dialog required
- [ ] **Save button**: btnSave with icon, enabled only when changes pending, Text="Save Changes"
- [ ] **Cancel button**: btnCancel enabled only when changes pending, Text="Cancel Changes"
- [ ] **Reload button**: btnReload with refresh icon, Text="Reload from Database"
- [ ] **Button layout**: FlowLayoutPanel with 10px spacing, anchored to top of UserControl
- [ ] **Keyboard shortcuts**: Ctrl+N (Add), Ctrl+E (Edit), Delete (Delete), Ctrl+S (Save), Esc (Cancel)

**Score**: ___ / 8 (requires ≥6 pass)

### 1.4 Audit Log Panel

- [ ] **TextBox control**: txtAuditLog, multiline, read-only, scrollbars vertical
- [ ] **Position**: Docked bottom, 150px height, above status bar
- [ ] **Content format**: Timestamp, Action, User, Details (e.g., "2025-10-15 14:32:15 | ADD | johnk | sp_GetInventory.p_LocationCode → p_")
- [ ] **Append-only**: New entries append to top (most recent first), max 100 entries retained in UI
- [ ] **Refresh behavior**: Audit log reloads when Reload button clicked or Save succeeds

**Score**: ___ / 5 (requires ≥4 pass)

### 1.5 Filter Controls

- [ ] **Procedure name filter**: txtFilterProcedure, TextBox with placeholder "Filter by procedure..."
- [ ] **Parameter name filter**: txtFilterParameter, TextBox with placeholder "Filter by parameter..."
- [ ] **Filter button**: btnApplyFilter with search icon, applies both filters with AND logic
- [ ] **Clear button**: btnClearFilter, resets filters and shows all rows
- [ ] **Filter position**: Grouped in Panel or GroupBox above DataGridView, anchored top
- [ ] **Real-time filtering**: TextChanged event applies filter immediately (no button click required for better UX)

**Score**: ___ / 6 (requires ≥5 pass)

### 1.6 Export/Import Functionality

- [ ] **Export button**: btnExport with icon, Text="Export to JSON", SaveFileDialog with .json filter
- [ ] **Import button**: btnImport with icon, Text="Import from JSON", OpenFileDialog with .json filter
- [ ] **Export format**: JSON array of objects with fields: ProcedureName, ParameterName, OverridePrefix, Reason
- [ ] **Import validation**: Validates JSON schema before import, rejects invalid files with error message
- [ ] **Import conflict handling**: Duplicate ProcedureName+ParameterName → prompt user (Overwrite/Skip/Cancel)
- [ ] **Post-import action**: Reload DataGridView, refresh audit log, set unsaved changes flag

**Score**: ___ / 6 (requires ≥5 pass)

### 1.7 Cache Integration

- [ ] **Reload cache button**: btnReloadCache, Text="Reload Application Cache", separate from Reload button
- [ ] **Cache reload method**: Calls Helper_Database_Variables.RefreshParameterPrefixCache() (assumes this method exists)
- [ ] **Confirmation dialog**: "Reload cache from database? This will affect all parameter prefix lookups." → Yes/No
- [ ] **Success notification**: MessageBox or status bar message "Cache reloaded successfully. X overrides loaded."
- [ ] **Error handling**: Catches exceptions from cache reload, displays user-friendly error message

**Score**: ___ / 5 (requires ≥4 pass)

---

## Section 2: Requirement Clarity

### 2.1 Task Instructions Unambiguous

- [ ] **UserControl creation steps**: Step-by-step documented (1. Add new UserControl file, 2. Add controls in designer, 3. Wire up events, 4. Implement CRUD methods)
- [ ] **DataGridView setup code**: Column definitions provided as code snippet for copy-paste
- [ ] **BindingSource setup**: Code example shows binding to List<Model> with BindingSource intermediary
- [ ] **CRUD method signatures**: Method names and signatures documented (e.g., `private async Task AddOverrideAsync()`)
- [ ] **TreeView integration**: Code snippet shows adding UserControl to Settings Form TreeView node "Development → Parameter Prefix Management"

**Score**: ___ / 5 (requires ≥4 pass)

### 2.2 Definitions Consistent

- [ ] **"Override"**: Defined as sys_parameter_prefix_override table row (not application-level override)
- [ ] **"Current prefix"**: Defined as prefix detected from INFORMATION_SCHEMA (read-only, for comparison)
- [ ] **"Audit log"**: UI-only display (not sys_parameter_prefix_override audit columns) - separate concerns
- [ ] **"Cache reload"**: Defined as re-querying override table and updating in-memory cache (not application restart)
- [ ] **"Unsaved changes"**: Defined as BindingSource.Current != original data snapshot (dirty flag tracking)

**Score**: ___ / 5 (requires ≥4 pass)

### 2.3 Edge Cases Addressed

- [ ] **Empty DataGridView**: Show placeholder message "No overrides defined. Click Add to create your first override."
- [ ] **Duplicate override attempt**: Validate ProcedureName+ParameterName uniqueness before INSERT, show error if exists
- [ ] **Delete last row**: DataGridView remains functional (no index errors), shows empty state message
- [ ] **Filter returns no results**: Show "No matching overrides found. Clear filters to see all." message
- [ ] **Export with no data**: Prompt "No overrides to export. Create overrides first."
- [ ] **Import file too large**: Reject files >1MB with error "Import file too large. Maximum 1MB allowed."
- [ ] **Cache reload during edit**: Warn "Unsaved changes will be lost. Continue?" before reloading
- [ ] **Non-Developer opens form**: Form constructor checks CurrentUser.IsDeveloper, shows access denied message if FALSE

**Score**: ___ / 8 (requires ≥6 pass)

---

## Section 3: Requirement Measurability

### 3.1 Quantitative Metrics Defined

- [ ] **UI layout time**: Expected 3 hours for DataGridView + button panel + filters layout (documented in T113d)
- [ ] **CRUD implementation time**: Expected 3 hours for 5 operations (Add/Edit/Delete/Save/Cancel logic) (documented in T113d)
- [ ] **Export/Import time**: Expected 1 hour for JSON serialization and file I/O (documented in T113d)
- [ ] **Cache integration time**: Expected 1 hour for reload cache button and Helper integration (documented in T113d)
- [ ] **Total T113d duration**: 8 hours documented as target completion time

**Score**: ___ / 5 (requires ≥4 pass)

### 3.2 Qualitative Acceptance Criteria

- [ ] **DataGridView population**: SELECT * FROM sys_parameter_prefix_override displays all columns correctly
- [ ] **Add operation**: New row persists after Save, appears in DataGridView and database
- [ ] **Edit operation**: Modified Reason column persists, ModifiedDate updates, ModifiedBy populated
- [ ] **Delete operation**: Row removed from DataGridView and database after confirmation
- [ ] **Filter operation**: Typing "inv_" in Procedure filter shows only inventory procedures
- [ ] **Export operation**: JSON file opens in text editor, contains valid JSON array
- [ ] **Import operation**: Exported JSON re-imports without errors, data matches original
- [ ] **Cache reload**: After override change + cache reload, startup parameter prefix detection uses new override

**Score**: ___ / 8 (requires ≥6 pass)

### 3.3 Success Criteria Traceability

- [ ] **SC-017 mapped to T113d**: Parameter prefix override persistence requirement directly fulfilled by this form
- [ ] **FR-028 components traced**: Database table CRUD, export/import, audit trail, cache reload all addressed
- [ ] **FR-023 access control**: Developer role gating integrated into form constructor

**Score**: ___ / 3 (requires ≥2 pass)

---

## Section 4: Requirement Testability

### 4.1 Validation Method Specified

- [ ] **Form load test**: Launch Settings → Development → Parameter Prefix Management, verify DataGridView populates without errors
- [ ] **Add test scenario**: Click Add → Enter sp_TestProcedure, p_TestParam, p_ → Save → Verify row appears in grid and database
- [ ] **Edit test scenario**: Select row → Click Edit → Change Reason → Save → Verify ModifiedDate updated in grid
- [ ] **Delete test scenario**: Select row → Click Delete → Confirm → Verify row removed from grid and database
- [ ] **Filter test scenario**: Enter "sp_Get" in Procedure filter → Verify only sp_Get* procedures shown
- [ ] **Export test scenario**: Click Export → Save JSON → Open in text editor → Verify valid JSON structure
- [ ] **Import test scenario**: Click Import → Select exported JSON → Verify rows appear in grid (no errors)
- [ ] **Cache reload test**: Add override → Click Reload Cache → Restart app → Verify parameter prefix uses override at startup

**Score**: ___ / 8 (requires ≥6 pass)

### 4.2 Expected Outcomes Documented

- [ ] **Successful form load**: DataGridView shows existing overrides (or empty state message if none)
- [ ] **Successful Add**: New row appears in grid, audit log shows "ADD" entry, Save button enabled
- [ ] **Successful Edit**: ModifiedDate column updates to current timestamp, audit log shows "EDIT" entry
- [ ] **Successful Delete**: Row disappears from grid immediately, audit log shows "DELETE" entry
- [ ] **Successful Export**: SaveFileDialog returns .json file path, file size >0 bytes
- [ ] **Successful Import**: ImportFileDialog returns .json file path, rows added to grid, audit log shows "IMPORT" entries
- [ ] **Successful Cache Reload**: MessageBox shows "Cache reloaded successfully. X overrides loaded."

**Score**: ___ / 7 (requires ≥5 pass)

### 4.3 Failure Scenarios Defined

- [ ] **Form load failure**: Database connection error → show error message, disable CRUD buttons except Reload
- [ ] **Add duplicate**: ProcedureName+ParameterName exists → error message "Override already exists for this parameter."
- [ ] **Edit unsaved**: Clicking Edit while unsaved changes pending → prompt "Save changes first?" (Yes/No/Cancel)
- [ ] **Delete confirmation cancel**: User clicks No on confirmation → row remains, no database DELETE executed
- [ ] **Export file write error**: Permission denied → error message "Cannot write to selected file. Choose different location."
- [ ] **Import invalid JSON**: Malformed JSON → error message "Invalid JSON file. Cannot import."
- [ ] **Cache reload error**: Helper method throws exception → error message "Cache reload failed: [exception message]"
- [ ] **Non-Developer access**: IsDeveloper=FALSE → form shows access denied message, all controls disabled

**Score**: ___ / 8 (requires ≥6 pass)

---

## Section 5: Requirement Dependencies

### 5.1 Prerequisite Requirements

- [ ] **T113c complete**: sys_parameter_prefix_override table exists, Developer role infrastructure in place
- [ ] **Helper_Database_Variables exists**: RefreshParameterPrefixCache() method available (or stubbed for T113d)
- [ ] **Settings Form TreeView**: Development node created in T113c, ready for UserControl integration
- [ ] **Model_Users.CurrentUser**: Provides IsDeveloper property for access control check

**Score**: ___ / 4 (requires ≥3 pass)

### 5.2 Dependent Requirements

- [ ] **T123 depends on T113d**: Startup cache loading tests assume override table can be populated via this form
- [ ] **T124 depends on T113d**: Parameter prefix compliance validation assumes override table maintainable
- [ ] **T132 depends on T113d**: Completion report documents Parameter Prefix Maintenance Form as key tool

**Score**: ___ / 3 (requires ≥2 pass)

### 5.3 Integration Points

- [ ] **Dao_System integration**: CRUD operations use Dao_System methods for sys_parameter_prefix_override table access
- [ ] **Helper_Database_Variables integration**: RefreshParameterPrefixCache() refreshes in-memory cache from override table
- [ ] **Service_DebugTracer integration**: CRUD operations logged for debugging (verbose mode)
- [ ] **LoggingUtility integration**: Exceptions logged with full context (stack trace, parameters)
- [ ] **System.Text.Json integration**: Export/Import use JsonSerializer for file I/O

**Score**: ___ / 5 (requires ≥4 pass)

---

## Section 6: Requirement Risks

### 6.1 Data Integrity Risks Identified

- [ ] **Concurrent edit risk**: Two Developers editing same override → last write wins (mitigation: ModifiedDate shows conflict, no optimistic locking)
- [ ] **Orphaned override risk**: Procedure deleted from database but override remains → mitigation: audit log helps identify stale overrides
- [ ] **Invalid prefix risk**: User enters non-existent prefix (e.g., "xyz_") → mitigation: validation rule enforces p_/in_/o_/none prefix values
- [ ] **Case sensitivity risk**: MySQL parameter names case-sensitive → mitigation: document convention (use exact case from INFORMATION_SCHEMA)

**Score**: ___ / 4 (requires ≥3 pass)

### 6.2 Usability Risks Identified

- [ ] **Filter confusion risk**: Users may not understand AND logic (both filters applied) → mitigation: UI tooltip explains filter behavior
- [ ] **Export/Import workflow risk**: Users may export, edit JSON manually, re-import invalid data → mitigation: import validation rejects bad schema
- [ ] **Cache reload timing risk**: Users may forget to reload cache after changes → mitigation: prominent "Reload Cache" button with warning icon
- [ ] **Audit log overflow risk**: Long-running session accumulates 100+ audit entries → mitigation: UI retains only last 100 entries (memory management)

**Score**: ___ / 4 (requires ≥3 pass)

### 6.3 Technical Risks Identified

- [ ] **DataGridView performance risk**: Large override table (1000+ rows) may slow UI → mitigation: paging or virtual mode (defer until needed)
- [ ] **JSON file size risk**: Large export may exceed MessageBox display limits → mitigation: file size validation before import (reject >1MB)
- [ ] **Cache thread safety risk**: RefreshParameterPrefixCache() called during Helper usage → mitigation: document cache reload as manual-only (no auto-refresh)
- [ ] **BindingSource memory leak risk**: Forgot to dispose BindingSource → mitigation: UserControl Dispose() method calls BindingSource.Dispose()

**Score**: ___ / 4 (requires ≥3 pass)

---

## Scoring Summary

| Section | Score | Pass Threshold | Status |
|---------|-------|----------------|--------|
| 1. Requirement Completeness | ___ / 44 | ≥35 | ☐ |
| 2. Requirement Clarity | ___ / 18 | ≥14 | ☐ |
| 3. Requirement Measurability | ___ / 16 | ≥13 | ☐ |
| 4. Requirement Testability | ___ / 23 | ≥18 | ☐ |
| 5. Requirement Dependencies | ___ / 12 | ≥10 | ☐ |
| 6. Requirement Risks | ___ / 12 | ≥10 | ☐ |
| **Total** | **___ / 125** | **≥100 (80%)** | **☐** |

---

## Validation Instructions

1. **Pre-Execution Review**: Run this checklist during Checkpoint 2 planning, before T113d implementation begins
2. **Scoring**: Check each item as Pass (☑) or Fail (☐), calculate section scores
3. **Gap Analysis**: For any section scoring <80%, document missing requirements in clarification-questions.md
4. **Approval Gate**: All sections must achieve ≥80% before T113d execution approved
5. **Revision Tracking**: Document checklist version and review date at bottom

---

**Checklist Version**: 1.0  
**Last Reviewed**: _________  
**Reviewed By**: _________  
**Overall Status**: ☐ PASS | ☐ FAIL | ☐ NEEDS REVISION
