# Clarification File for Control_InventoryTab.cs

**Generated**: 2025-10-24 
**Target File**: Controls/MainForm/Control_InventoryTab.cs
**Dependencies Identified**: 5 (Dao_Inventory, Dao_QuickButtons, Dao_ErrorLog, Helper_UI_ComboBoxes, Helper_StoredProcedureProgress)
**Additional Files with Violations**: 2 (Forms/Transactions/Transactions.cs, Controls/SettingsForm/Control_Edit_User.cs)

## Clarifications Required

### CL-001: Error Handling Migration Strategy
**Files**: Multiple (Control_InventoryTab.cs uses Dao_ErrorLog.HandleException_GeneralError_CloseApp 13 times)
**Issue**: Current code uses legacy `Dao_ErrorLog.HandleException_GeneralError_CloseApp` method
**Context**: FR-008 requires Service_ErrorHandler adoption, but the file already has comprehensive error handling
**Question**: Should we migrate all Dao_ErrorLog calls to Service_ErrorHandler, or keep them since they're already structured?
**Options**:
- [x] A: Migrate ALL to Service_ErrorHandler (full FR-008 compliance, may require significant rework)
- [ ] B: Keep Dao_ErrorLog as-is (already structured, functional, but not spec-compliant)
- [ ] C: Hybrid approach - new code uses Service_ErrorHandler, existing Dao_ErrorLog calls remain
**Answer**: _______________
**Resolved**: [x] Yes [ ] No

### CL-002: Service_DebugTracer Completion Level
**File**: Control_InventoryTab.cs
**Methods**: Multiple event handlers and button click methods
**Issue**: File has EXTENSIVE Service_DebugTracer integration already (constructor has 9 trace calls), but some methods may lack entry/exit tracing
**Context**: FR-006 requires complete method entry/exit tracing for all database-facing operations
**Question**: What level of Service_DebugTracer integration is required for this file?
**Options**:
- [x] A: Add entry/exit to EVERY method that directly or indirectly touches database (comprehensive, verbose logs)
- [ ] B: Add entry/exit only to methods that DIRECTLY call DAO methods (focused, manageable)
- [ ] C: Current level is sufficient (constructor + key operations already traced)
**Answer**: _______________
**Resolved**: [x] Yes [ ] No

### CL-003: Progress Reporting Scope
**File**: Control_InventoryTab.cs
**Methods**: Control_InventoryTab_Button_Save_Click_Async, Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync
**Issue**: File has _progressHelper field initialized, but not all async operations use it
**Context**: FR-005 requires Helper_StoredProcedureProgress for long operations (>1 second)
**Question**: Which operations need progress reporting?
**Options**:
- [x] A: Add progress to ALL async DAO operations (comprehensive UX feedback)
- [ ] B: Add progress only to save operation (most critical user-initiated action)
- [ ] C: Current setup is sufficient (progress helper exists, can be added per-operation as needed)
**Answer**: _______________
**Resolved**: [x] Yes [ ] No

### CL-004: Transaction Management for Quick Button Update
**File**: Control_InventoryTab.cs
**Method**: AddToLast10TransactionsIfUniqueAsync (Line 726)
**Issue**: Method calls Dao_QuickButtons.AddOrShiftQuickButtonAsync after inventory save
**Context**: FR-011 requires transaction management for multi-step workflows
**Question**: Should inventory save + quick button update be in a transaction?
**Options**:
- [ ] A: Wrap both operations in transaction (ensures atomicity, but complicates error handling)
- [ ] B: Keep operations independent (current behavior - inventory save succeeds even if quick button update fails)
- [x] C: Make quick button update optional/non-critical (best effort, log failure but don't block inventory save)
**Answer**: _______________
**Resolved**: [x] Yes [ ] No

### CL-005: Column Naming Violations in Dependencies
**Files**: 
- Forms/Transactions/Transactions.cs (Lines 361, 364)
- Controls/SettingsForm/Control_Edit_User.cs (Line 160)
**Issue**: Found `p_Operation`, `p_User` column name violations in related files
**Context**: FR-012 - These are CRITICAL runtime errors that must be fixed
**Question**: Should these files be included in this compliance review scope?
**Options**:
- [x] A: YES - Fix all violations in dependencies (comprehensive, prevents runtime crashes)
- [ ] B: NO - Create separate compliance reviews for each file (focused, manageable)
- [ ] C: Create separate checklist items but fix in this session (efficient, complete solution)
**Answer**: _______________
**Resolved**: [x] Yes [ ] No

---

## Resolution Status

**Total Clarifications**: 5
**Resolved**: 5
**Pending**: 5

**Ready to Proceed**: [x] Yes [ ] No

---

## Instructions for User

1. Review each clarification above
2. Select an option (A, B, C) or provide custom answer
3. Mark "Resolved: [X] Yes" for each answered clarification
4. When ALL clarifications are resolved, mark "Ready to Proceed: [X] Yes"
5. Save this file
6. Re-run the database-compliance-reviewer prompt

**Note**: Work on Control_InventoryTab.cs is BLOCKED until all clarifications are resolved.
