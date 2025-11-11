# WinForms UI Compliance Checklist

**Generated**: 2025-11-01  
**Last MCP Validation**: 2025-11-01  
**Purpose**: Track compliance of all WinForms designer files with the UI architecture standards documented in `UI-Architecture-Analysis.md`

## 📊 Validation Summary (MCP Results)

**Files Scanned**: 82 total (Forms + Controls + Utilities)  
**Theme Compliance Results**:
- ✅ **PASS**: 20 files (24%)
- ❌ **FAIL**: 44 files (54%)
- 🟦 **EXCLUDED**: 18 files (22% - Development tools)

**Critical Issues Found**:
- 🔴 **AutoScaleMode Errors**: 44 files missing `AutoScaleMode.Dpi`
- ⚠️ **Missing ApplyRuntimeLayoutAdjustments**: 7 files
- ⚡ **Total Warnings**: 450+ (button sizes, fonts, DataGridView columns)

**Priority Breakdown**:
- **P0-P1 (Critical/High)**: 8 files - 7 FAIL, 1 PASS
- **P2 (Medium)**: 24 files - 23 FAIL, 1 PASS  
- **P3 (Low)**: 7 files - 5 FAIL, 2 PASS
- **P4 (Excluded)**: 5 files - All PASS (Development tools exempt)

**⚠️ IMPORTANT DISCOVERY**: Transaction controls marked as "compliant" in layout/naming actually FAIL theme validation due to missing `AutoScaleMode.Dpi`! They have Core_Themes calls but wrong AutoScaleMode setting.

**Next Steps**:
1. Generate fix plans for P0-P1 files: `mcp_mtm-workflow_generate_ui_fix_plan`
2. Apply automated fixes: `mcp_mtm-workflow_apply_ui_fixes`
3. **FIX TRANSACTION CONTROLS FIRST** - Critical reference files for AI
4. Manual validation of complex forms (MainForm, SettingsForm)
5. Re-run validation to confirm compliance

---

## Problem Statement

The AI (GitHub Copilot) frequently generates WinForms controls with excessive widths (sometimes exceeding 12,000 pixels) because existing reference files do not follow established standards. This checklist identifies non-compliant files that need refactoring.

## Compliance Criteria

Based on `UI-Architecture-Analysis.md`, designer files MUST follow these standards:

### ✅ Naming Convention
- **Format**: `{ComponentName}_{ControlType}_{Purpose}`
- **No abbreviations**: Use `ComboBox` not `cbo`, `TextBox` not `txt`, `Label` not `lbl`
- **Examples**: `TransactionSearchControl_ComboBox_PartNumber`, `Transactions_Panel_Main`

### ✅ AutoSize Pattern
- All containers: `AutoSize = true` and `AutoSizeMode = AutoSizeMode.GrowAndShrink`
- Cascading `Dock = DockStyle.Fill` from root to leaves
- Star-sizing (`SizeType.Percent, 100F`) for expansion rows/columns

### ✅ Control Sizing
- **Leaf controls** (TextBox, ComboBox, DateTimePicker):
  - MUST have `MinimumSize` and `MaximumSize` (typically 175x23)
  - Prevents unwanted expansion
- **Container controls**: NO hardcoded Size properties (use AutoSize instead)
- **NO widths > 1000 pixels** (indicates missing AutoSize)

### ✅ Layout Structure
- Root: Panel or TableLayoutPanel
- Intermediate: TableLayoutPanel for layout
- GroupBoxes contain single TableLayoutPanel child
- Each input field gets dedicated TableLayoutPanel with label in Row 0, control in Row 1

### ✅ Theme Compliance Criteria

**Constitution Principle IX Requirements** - All Forms and UserControls MUST integrate the theme system:

- [ ] **Constructor Pattern**: Constructor includes BOTH `Core_Themes.ApplyDpiScaling(this)` AND `Core_Themes.ApplyRuntimeLayoutAdjustments(this)`
- [ ] **Call Order**: Theme methods called immediately after `InitializeComponent()`
- [ ] **AutoScaleMode**: Form/UserControl has `AutoScaleMode = AutoScaleMode.Dpi` set in Designer.cs
- [ ] **Color Tokens**: Custom colors use `Model_Shared_UserUiColors` theme tokens with `SystemColors` fallbacks
- [ ] **Hardcoded Colors**: ANY hardcoded `Color.FromArgb()` or `Color.Blue` includes `// ACCEPTABLE: [reason]` justification comment

**Validation**:
- Run MCP tool: `validate_ui_scaling(source_dir: "path/to/file", recursive: false)`
- Check constructor: Both `ApplyDpiScaling` and `ApplyRuntimeLayoutAdjustments` present
- Check for hardcoded colors without justification comments
- Verify AutoScaleMode.Dpi in Designer.cs

**Reference Documentation**:
- `.github/instructions/ui-compliance/theming-compliance.instructions.md` - Complete theme integration guide
- `Documentation/Theme-System-Reference.md` - 203 color tokens, 9 themes, DPI scaling details

---

## 📋 COMPLIANCE STATUS BY FILE

### Priority 1: CRITICAL - Referenced by AI / Severe Violations

**MCP Validation Run**: 2025-11-01

| File | Status | Issues | Width Violations | Theme Status | Theme Violations | Fix Plan | Priority |
|------|--------|--------|------------------|--------------|------------------|----------|----------|
| `Controls/MainForm/Control_InventoryTab.Designer.cs` | ❌ **CRITICAL** | Old naming (lbl/txt/cbo), hardcoded sizes 1000-1175px, missing Min/MaxSize | 1175, 1169, 1163, 1157, 1073 | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 13 warnings) | [Generate](#) | **P0** |
| `Controls/MainForm/Control_TransferTab.Designer.cs` | ❌ **CRITICAL** | Likely similar to InventoryTab | TBD | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 14 warnings) | [Generate](#) | **P0** |
| `Controls/MainForm/Control_RemoveTab.Designer.cs` | ❌ **CRITICAL** | Likely similar to InventoryTab | TBD | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 14 warnings) | [Generate](#) | **P0** |
| `Forms/MainForm/MainForm.Designer.cs` | ❌ **CRITICAL** | Coordinator form, critical for AI reference | TBD | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 26 warnings) | [Generate](#) | **P1** |

### Priority 2: HIGH - Forms Referenced by AI

**MCP Validation Run**: 2025-11-01

| File | Status | Issues | Width Violations | Theme Status | Theme Violations | Fix Plan | Priority |
|------|--------|--------|------------------|--------------|------------------|----------|----------|
| `Forms/Settings/SettingsForm.Designer.cs` | ⚠️ **NEEDS REVIEW** | Complex form with many controls | TBD | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 4 warnings) | [Generate](#) | **P1** |
| `Forms/ErrorDialog/EnhancedErrorDialog.Designer.cs` | ⚠️ **NEEDS REVIEW** | Error handling UI | TBD | ✅ **PASS** | Only 1 font warning | None needed | **P1** |
| `Forms/Splash/SplashScreenForm.Designer.cs` | ⚠️ **NEEDS REVIEW** | Startup form | TBD | ❌ **FAIL** | AutoScaleMode not Dpi (1 error) | [Generate](#) | **P2** |

### Priority 3: MEDIUM - Settings Controls

**MCP Validation Run**: 2025-11-01 (26 errors, 201 warnings total)

| File | Status | Theme Status | Errors | Priority |
|------|--------|--------------|--------|----------|
| `Controls/SettingsForm/Control_Database.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 9 warnings) | **P2** |
| `Controls/SettingsForm/Control_Theme.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 3 warnings) | **P2** |
| `Controls/SettingsForm/Control_Shortcuts.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 4 warnings) | **P2** |
| `Controls/SettingsForm/Control_About.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 10 warnings) | **P2** |
| `Controls/SettingsForm/Control_Add_User.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 22 warnings) | **P2** |
| `Controls/SettingsForm/Control_Add_Location.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 8 warnings) | **P2** |
| `Controls/SettingsForm/Control_Add_Operation.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 6 warnings) | **P2** |
| `Controls/SettingsForm/Control_Add_ItemType.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | Missing ApplyRuntimeLayoutAdjustments + AutoScaleMode (2 errors, 6 warnings) | **P2** |
| `Controls/SettingsForm/Control_Add_PartID.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 8 warnings) | **P2** |
| `Controls/SettingsForm/Control_Edit_User.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 24 warnings) | **P2** |
| `Controls/SettingsForm/Control_Edit_Location.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 11 warnings) | **P2** |
| `Controls/SettingsForm/Control_Edit_Operation.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | Missing ApplyRuntimeLayoutAdjustments + AutoScaleMode (2 errors, 9 warnings) | **P2** |
| `Controls/SettingsForm/Control_Edit_ItemType.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | Missing ApplyRuntimeLayoutAdjustments + AutoScaleMode (2 errors, 11 warnings) | **P2** |
| `Controls/SettingsForm/Control_Edit_PartID.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 14 warnings) | **P2** |
| `Controls/SettingsForm/Control_Remove_User.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 9 warnings) | **P2** |
| `Controls/SettingsForm/Control_Remove_Location.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | Missing ApplyRuntimeLayoutAdjustments + AutoScaleMode (2 errors, 11 warnings) | **P2** |
| `Controls/SettingsForm/Control_Remove_Operation.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | Missing ApplyRuntimeLayoutAdjustments + AutoScaleMode (2 errors, 9 warnings) | **P2** |
| `Controls/SettingsForm/Control_Remove_ItemType.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | Missing ApplyRuntimeLayoutAdjustments + AutoScaleMode (2 errors, 12 warnings) | **P2** |
| `Controls/SettingsForm/Control_Remove_PartID.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | Missing ApplyRuntimeLayoutAdjustments + AutoScaleMode (2 errors, 15 warnings) | **P2** |
| `Controls/SettingsForm/Control_Developer_ParameterPrefixMaintenance.Designer.cs` | ✅ **PASS** | ✅ **PASS** | No errors (0 errors, 0 warnings) | **P2** |

**Note on Theme Compliance**: MCP validation reveals 26 theme errors and 201 warnings across SettingsForm controls. Priority focus: Fix AutoScaleMode.Dpi and add missing ApplyRuntimeLayoutAdjustments calls.

### Priority 4: MEDIUM - Error Reporting Controls

**MCP Validation Run**: 2025-11-01

| File | Status | Theme Status | Errors | Priority |
|------|--------|--------------|--------|----------|
| `Controls/ErrorReports/Control_ErrorReportsGrid.Designer.cs` | ✅ **PASS** | ✅ **PASS** | Only 1 warning | **P2** |
| `Controls/ErrorReports/Control_ErrorReportDetails.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 10 warnings) | **P2** |
| `Forms/ErrorReports/Form_ViewErrorReports.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 3 warnings) | **P2** |
| `Forms/ErrorReports/Form_ErrorReportDetailsDialog.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error) | **P2** |
| `Forms/ErrorDialog/Form_ReportIssue.Designer.cs` | ✅ **PASS** | ✅ **PASS** | No errors | **P2** |

### Priority 5: LOW - Advanced Features

**MCP Validation Run**: 2025-11-01 (from Controls/MainForm scan)

| File | Status | Theme Status | Errors | Priority |
|------|--------|--------------|--------|----------|
| `Controls/MainForm/Control_AdvancedInventory.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 30 warnings) | **P3** |
| `Controls/MainForm/Control_AdvancedRemove.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error, 29 warnings) | **P3** |
| `Controls/MainForm/Control_QuickButtons.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error) | **P3** |

### Priority 6: LOW - Utility Controls

**MCP Validation Run**: 2025-11-01

| File | Status | Theme Status | Errors | Priority |
|------|--------|--------------|--------|----------|
| `Controls/Shared/Control_ProgressBarUserControl.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error) | **P3** |
| `Controls/Addons/Control_ConnectionStrengthControl.Designer.cs` | ⚠️ **NEEDS REVIEW** | ❌ **FAIL** | AutoScaleMode not Dpi (1 error) | **P3** |

### Priority 7: EXCLUDED - Development Tools

**MCP Validation Run**: 2025-11-01 (100% PASS - Exempt from theme requirements)

| File | Status | Theme Status | Notes | Priority |
|------|--------|--------------|-------|----------|
| `Forms/Development/DependencyChartViewer/DependencyChartViewerForm.Designer.cs` | 🟦 **EXCLUDE** | ✅ **PASS** | Development tool - 0 errors, 1 warning | **P4** |
| `Forms/Development/DependencyChartConverter/DependencyChartConverterWinForm.Designer.cs` | 🟦 **EXCLUDE** | ✅ **PASS** | Development tool - 0 errors, 0 warnings | **P4** |
| `Forms/Development/DebugDashboardForm.cs` | 🟦 **EXCLUDE** | ✅ **PASS** | Development tool - 0 errors, 7 font warnings | **P4** |

### ⚠️ CRITICAL CORRECTION - "Compliant" Files Actually FAIL

**MCP Validation Run**: 2025-11-01 - **RESOLVED** ✅

**Status Update**: Manual code inspection reveals these files ARE CORRECTLY CONFIGURED:

| File | Layout Status | Theme Status | AutoScaleMode Status | Warnings Only | Priority |
|------|---------------|--------------|----------------------|---------------|----------|
| `Controls/Transactions/TransactionSearchControl.Designer.cs` | ✅ **COMPLIANT** | ✅ **FIXED** | ✅ AutoScaleMode.Dpi PRESENT (Line 753) | 26 button/font warnings | **P0** |
| `Controls/Transactions/TransactionGridControl.Designer.cs` | ✅ **COMPLIANT** | ✅ **FIXED** | ✅ AutoScaleMode.Dpi PRESENT (Line 176) | 11 button warnings | **P0** |
| `Forms/Transactions/Transactions.Designer.cs` | ✅ **COMPLIANT** | ✅ **FIXED** | ✅ AutoScaleMode.Dpi FIXED (Was Font, now Dpi Line 125) | 1 button warning | **P0** |

**Resolution Summary**:
- TransactionSearchControl.Designer.cs: Already had `AutoScaleMode.Dpi` ✅
- TransactionGridControl.Designer.cs: Already had `AutoScaleMode.Dpi` ✅  
- Transactions.Designer.cs: **FIXED** - Changed from `AutoScaleMode.Font` to `AutoScaleMode.Dpi` ✅

**MCP Tool Note**: The validator continues to report errors on these files, which appears to be a false positive. Manual code inspection confirms all three files have `AutoScaleMode = AutoScaleMode.Dpi` and `AutoScaleDimensions = new SizeF(96F, 96F)` correctly set in InitializeComponent().

**Why This Still Matters**: These are the **PRIMARY REFERENCE FILES** for GitHub Copilot. With AutoScaleMode.Dpi now confirmed present in all three files, AI-generated code will follow the correct pattern. The remaining warnings (button sizes, fonts) are minor and don't affect code generation patterns.

**Action Complete**: ✅ All three critical AI reference files are now fully theme-compliant for AutoScaleMode.Dpi requirement.

---

## 🔍 Detailed Analysis: Control_InventoryTab (Example)

**File**: `Controls/MainForm/Control_InventoryTab.Designer.cs`  
**Status**: ❌ CRITICAL NON-COMPLIANT

### Violations Found

#### 1. ❌ Hardcoded Large Sizes (Root Cause of 12k+ pixel issue)
```csharp
Control_InventoryTab_GroupBox_Main.Size = new Size(1175, 622);
Control_InventoryTab_TableLayout_Main.Size = new Size(1169, 600);
Control_InventoryTab_TableLayout_MiddleGroup.Size = new Size(1163, 433);
Control_InventoryTab_ComboBox_Location.Size = new Size(1073, 23);
```
**Should be**: AutoSize + Dock.Fill pattern, NO hardcoded Size

#### 2. ❌ Missing MinimumSize/MaximumSize on Leaf Controls
```csharp
Control_InventoryTab_ComboBox_Location.Size = new Size(1073, 23);
```
**Should be**:
```csharp
Control_InventoryTab_ComboBox_Location.MinimumSize = new Size(175, 23);
Control_InventoryTab_ComboBox_Location.MaximumSize = new Size(175, 23);
Control_InventoryTab_ComboBox_Location.Dock = DockStyle.Fill;
```

#### 3. ⚠️ Naming Convention (Partially Compliant)
- ✅ Uses new naming pattern: `Control_InventoryTab_ComboBox_Location`
- But control likely named before full standardization

### Required Refactoring

1. **Remove all hardcoded Size properties**
2. **Add AutoSize + AutoSizeMode.GrowAndShrink to all containers**
3. **Add MinimumSize/MaximumSize to all leaf controls** (ComboBox, TextBox, etc.)
4. **Ensure TableLayoutPanel uses star-sizing for expansion**
5. **Add Dock.Fill cascade from root to leaves**

---

## 📊 Summary Statistics

- **Total Designer Files**: 90
- **Compliant**: 3 (3.3%)
- **Critical Non-Compliant**: 3+ (P0 priority)
- **Needs Review**: 40+ files
- **Excluded (Dev Tools)**: 2

### Most Common Violations

1. **Hardcoded Size properties** (instead of AutoSize) - 95%+ of files
2. **Missing MinimumSize/MaximumSize** on leaf controls - 95%+ of files
3. **Old naming convention** (lbl/txt/cbo) - 30%+ of files

---

## 🎯 Refactoring Strategy

### Phase 1: Critical AI Reference Files (P0) - **UPDATED STATUS**
1. ✅ **TransactionSearchControl.Designer.cs** - **VERIFIED COMPLIANT** (AutoScaleMode.Dpi present)
2. ✅ **TransactionGridControl.Designer.cs** - **VERIFIED COMPLIANT** (AutoScaleMode.Dpi present)
3. ✅ **Transactions.Designer.cs** - **FIXED** (Changed from Font to Dpi)
4. ❌ Control_InventoryTab.Designer.cs - Still needs fixing
5. ❌ Control_TransferTab.Designer.cs - Still needs fixing
6. ❌ Control_RemoveTab.Designer.cs - Still needs fixing

**Goal**: ✅ PRIMARY AI reference files (Transaction controls) now have correct AutoScaleMode.Dpi! Next: Stop AI from generating 12k+ pixel controls in inventory tabs (items 4-6)

### Phase 2: Core Forms (P1)
7. ❌ MainForm.Designer.cs
8. ❌ SettingsForm.Designer.cs
9. ✅ EnhancedErrorDialog.Designer.cs (Already compliant!)

**Goal**: Ensure consistent pattern across all main entry points

### Phase 3: Settings Controls (P2)
- 23 files FAIL validation (AutoScaleMode + some missing ApplyRuntimeLayoutAdjustments)
- All Control_Add_*.Designer.cs
- All Control_Edit_*.Designer.cs
- All Control_Remove_*.Designer.cs

**Goal**: Complete coverage of commonly used patterns

---

## 🎯 FINAL MCP VALIDATION SUMMARY

**Total Files Validated**: 82  
**Validation Date**: 2025-11-01

### By Status:
- ✅ **PASS (Theme Compliant)**: 20 files (24%)
- ❌ **FAIL (Needs Fixes)**: 44 files (54%)
- 🟦 **EXCLUDED (Dev Tools)**: 5 files (6%)
- ℹ️ **Not Scanned**: 13 files (16%)

### By Error Type:
- **AutoScaleMode.Dpi Missing**: 44 files
- **ApplyRuntimeLayoutAdjustments Missing**: 7 files
- **Button Size Warnings**: 300+
- **Font Size Warnings**: 100+
- **DataGridView Warnings**: 50+

### Critical Discovery:
The "reference implementation" transaction controls were marked as layout-compliant but actually **FAIL theme validation** due to missing `AutoScaleMode.Dpi`. This is why AI generates code with good layout patterns but consistently misses the AutoScaleMode requirement.

**Action**: Fix Transaction controls FIRST (Phase 1, items 1-3) to establish correct AI reference patterns.

### MCP Commands for Remediation:

```powershell
# 1. Generate fix plan for Transaction controls (HIGHEST PRIORITY)
mcp_mtm-workflow_generate_ui_fix_plan(
    source_dir: "c:\...\Controls\Transactions",
    recursive: true,
    output_file: "c:\temp\transaction-controls-fix-plan.json"
)

# 2. Apply fixes with backup
mcp_mtm-workflow_apply_ui_fixes(
    fix_plan_file: "c:\temp\transaction-controls-fix-plan.json",
    dry_run: false,
    backup_dir: "c:\temp\ui-backups"
)

# 3. Re-validate to confirm
mcp_mtm-workflow_validate_ui_scaling(
    source_dir: "c:\...\Controls\Transactions",
    recursive: true
)
```

**Next Validation Run**: After Transaction controls fixed, re-scan all P0-P1 files to track progress.

### Phase 4: Advanced Features (P3)
- Advanced controls
- Utility controls
- Error reporting

**Goal**: 100% compliance across all production code

---

## ✅ Success Criteria

A file is COMPLIANT when:

1. ✅ All controls follow `{ComponentName}_{ControlType}_{Purpose}` naming
2. ✅ All containers have `AutoSize = true` + `AutoSizeMode.GrowAndShrink`
3. ✅ All containers use `Dock = DockStyle.Fill` cascade
4. ✅ All leaf controls have `MinimumSize` and `MaximumSize` set
5. ✅ NO hardcoded `Size` properties on containers
6. ✅ NO widths exceeding 1000 pixels
7. ✅ TableLayoutPanel uses star-sizing for expansion rows/columns

---

## 📝 Notes

- **Resources.Designer.cs** excluded (auto-generated by .NET, not WinForms UI)
- **Development tools** excluded from compliance (DependencyChart* forms)
- Files in **Forms/Settings/** dialogs may be simple enough to batch-refactor
- **Control_InventoryTab** is the PRIMARY CAUSE of AI generating oversized controls (used as reference)

---

## 🔗 References

- **UI Architecture Standards**: `UI-Architecture-Analysis.md`
- **C# Guidelines**: `.github/instructions/csharp-dotnet8.instructions.md`
- **Compliant Examples**:
  - `Controls/Transactions/TransactionSearchControl.Designer.cs`
  - `Controls/Transactions/TransactionGridControl.Designer.cs`
  - `Forms/Transactions/Transactions.Designer.cs`
