---
description: 'AI-driven prompt for refactoring WinForms Forms/UserControls to achieve complete theme system compliance'
type: 'refactoring'
targets: ['Forms', 'UserControls', 'Designer.cs']
requiredInstructions: ['.github/instructions/ui-compliance/theming-compliance.instructions.md']
mcpTools: ['validate_ui_scaling', 'generate_ui_fix_plan', 'apply_ui_fixes']
---

# Refactor Theme Compliance Prompt

## Agent Communication Rules

**⚠️ EXTREMELY IMPORTANT - Maximize Premium Request Value**:

This prompt handles theme compliance refactoring for WinForms Forms and UserControls. To maximize value:

- **Complete full analysis** (validation → fix plan → backup → apply → verify) in a single session
- **Process all violations** in the target file without stopping between fixes
- **Generate complete remediation report** with before/after metrics
- **Provide rollback instructions** in case of issues
- **Continue through validation** to confirm compliance achieved
- **Only stop for user input** when critical design decisions required (e.g., brand color justification)

**Do NOT stop after generating fix plan** - continue through application and validation unless user approval explicitly required.

---

## Required MCP Tools

This workflow requires the following MCP tools from the **mtm-workflow** server:
- `validate_ui_scaling` - Scan files for theme compliance violations
- `generate_ui_fix_plan` - Create structured fix plan JSON
- `apply_ui_fixes` - Apply fixes with backup and corruption detection (optional - use carefully)

---

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

## Outline

You are refactoring a WinForms Form or UserControl to achieve complete theme system compliance per Constitution Principle IX. This includes adding mandatory constructor calls, replacing hardcoded colors with theme tokens, fixing control naming, and correcting layout architecture.

Follow this execution flow:

### Phase 1: Analysis and Validation

1. **Load Target File(s)**
   - If user provides specific file path, load that file
   - If user provides directory, scan all `.cs` and `.Designer.cs` files recursively
   - Read current constructor implementation
   - Identify existing theme integration (if any)

2. **Run Validation**
   - Execute `validate_ui_scaling` on target file(s)
   - Capture all violations: missing constructor calls, hardcoded colors, naming issues, layout problems
   - Categorize by severity: CRITICAL (missing constructor calls), ERROR (hardcoded colors), WARNING (naming/layout)
   - Report violation count and file-level compliance score

3. **Load Required Instruction Files**
   - Read `.github/instructions/ui-compliance/theming-compliance.instructions.md`
   - Extract required constructor pattern
   - Extract color token usage patterns
   - Extract control naming conventions
   - Extract layout architecture requirements

### Phase 2: Fix Plan Generation

4. **Generate Fix Plan**
   - For each violation, determine remediation action:
     * **Missing Constructor Calls**: Insert `Core_Themes.ApplyDpiScaling(this)` and `Core_Themes.ApplyRuntimeLayoutAdjustments(this)` after `InitializeComponent()`
     * **Hardcoded Colors**: Replace with `Model_UserUiColors` token + `SystemColors` fallback OR document as `// ACCEPTABLE:`
     * **Control Naming**: Rename to `{ComponentName}_{ControlType}_{Purpose}` format (Designer.cs only - requires regeneration)
     * **Layout Issues**: Fix AutoSize, MinimumSize/MaximumSize, Dock properties
   - Prioritize: CRITICAL → ERROR → WARNING
   - Include before/after code snippets for each fix
   - Estimate effort (lines changed, risk level)

5. **User Approval Gate (OPTIONAL)**
   - If `--require-approval` flag set, present fix plan and await user confirmation
   - If hardcoded colors detected, ask user to justify or approve token replacement
   - Otherwise, proceed automatically to Phase 3

### Phase 3: Backup and Application

6. **Create Backup**
   - Generate backup directory: `backups/theme-refactor-{filename}-{timestamp}/`
   - Copy original file(s) to backup location
   - Store fix plan JSON in backup directory for rollback reference

7. **Apply Constructor Fixes**
   - Locate `InitializeComponent()` call in constructor
   - Insert theme integration immediately after:
     ```csharp
     InitializeComponent();
     Core_Themes.ApplyDpiScaling(this);
     Core_Themes.ApplyRuntimeLayoutAdjustments(this);
     ```
   - Verify correct indentation and formatting
   - Handle edge cases: multiple constructors, existing theme calls

8. **Apply Color Token Replacements**
   - For each hardcoded color identified:
     * Determine appropriate theme token (Button → ButtonBackColor, TextBox → TextBoxBackColor, etc.)
     * Replace with token + fallback pattern:
       ```csharp
       var colors = Model_AppVariables.UserUiColors;
       control.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
       ```
     * If user indicated "brand color", add `// ACCEPTABLE: [reason]` comment instead
   - Group token assignments in dedicated `ApplyThemeColors()` method in `#region Theme Application`
   - Call `ApplyThemeColors()` from constructor after theme integration

9. **Apply Layout Fixes (if applicable)**
   - Fix AutoSize properties on containers
   - Add MinimumSize/MaximumSize to leaf controls
   - Correct Dock properties for cascade pattern
   - Remove hardcoded Size properties on containers

### Phase 4: Validation and Verification

10. **Re-run Validation**
    - Execute `validate_ui_scaling` on modified file(s)
    - Compare violation counts: before vs after
    - Target: Zero CRITICAL violations, zero ERROR violations
    - Acceptable: Some WARNING violations if justified (e.g., legacy control compatibility)

11. **Compilation Check**
    - Attempt to compile modified files
    - If compilation fails:
      * Identify syntax errors
      * Attempt automatic correction (missing semicolons, brace mismatches)
      * If correction fails, rollback from backup and report error

12. **Corruption Detection**
    - Check file size change (should not exceed 50% increase/decrease)
    - Verify brace matching (`{` count == `}` count)
    - Validate region tags (`#region` count == `#endregion` count)
    - Check for duplicate method declarations
    - If corruption detected, automatic rollback

### Phase 5: Reporting and Rollback Instructions

13. **Generate Remediation Report**
    - Summary: File path, before/after violation counts, fixes applied
    - Detailed changes: Line-by-line edits with before/after code
    - Compliance score: Percentage improvement
    - Remaining violations: List any unfixed WARNING-level issues
    - Backup location: Path to rollback files

14. **Provide Rollback Instructions**
    ```powershell
    # If issues detected, rollback with:
    Copy-Item -Path "backups/theme-refactor-{filename}-{timestamp}/*" -Destination "original/path/" -Force
    ```

15. **Document Follow-Up Actions**
    - Manual testing required: Open Form/UserControl in designer, test at multiple DPI levels
    - If Designer.cs modified: Verify designer opens without errors
    - If control naming changed: Update all references in code-behind
    - Schedule code review: Theme compliance verification

---

## Execution Workflow Example

### Example 1: Single File Refactor

**User Input**: `/refactor-theme-compliance Controls/MainForm/Control_InventoryTab.cs`

**Agent Actions**:
1. Validate `Control_InventoryTab.cs` → finds 15 violations (2 CRITICAL, 8 ERROR, 5 WARNING)
2. Generate fix plan → 15 remediation actions
3. Create backup → `backups/theme-refactor-Control_InventoryTab-20251101-143022/`
4. Apply constructor fixes → Insert `Core_Themes.ApplyDpiScaling(this)` and `ApplyRuntimeLayoutAdjustments(this)`
5. Apply color replacements → Replace 8 hardcoded colors with theme tokens
6. Re-validate → 0 CRITICAL, 0 ERROR, 2 WARNING (legacy control compatibility)
7. Compile → Success
8. Report → 86% compliance improvement, 13 fixes applied, 2 warnings remain

**Agent Output**:
```
✅ Theme Compliance Refactor Complete

File: Controls/MainForm/Control_InventoryTab.cs
Before: 15 violations (2 CRITICAL, 8 ERROR, 5 WARNING)
After: 2 violations (0 CRITICAL, 0 ERROR, 2 WARNING)
Compliance: 86% improvement

Fixes Applied:
  ✅ Added Core_Themes.ApplyDpiScaling(this) in constructor
  ✅ Added Core_Themes.ApplyRuntimeLayoutAdjustments(this) in constructor
  ✅ Replaced 8 hardcoded colors with theme tokens
  ✅ Fixed 3 AutoSize properties on containers
  ✅ Added MinimumSize/MaximumSize to 2 TextBox controls

Remaining Warnings:
  ⚠️ Line 156: Custom control inherits non-standard base (legacy compatibility)
  ⚠️ Line 289: GroupBox width 1024px exceeds guideline (acceptable for this layout)

Backup Location: backups/theme-refactor-Control_InventoryTab-20251101-143022/

Next Steps:
  1. Open Control_InventoryTab in WinForms designer to verify no errors
  2. Test at 100%, 125%, 150%, 200% DPI scaling
  3. Verify theme switching updates colors correctly
  4. Schedule code review for theme compliance verification
```

### Example 2: Directory Scan with Approval

**User Input**: `/refactor-theme-compliance Controls/MainForm/ --require-approval`

**Agent Actions**:
1. Scan directory → finds 3 files with violations
2. Validate all files → 45 total violations
3. Generate combined fix plan → present to user
4. **STOP** - await user approval
5. User approves → proceed with Phase 3
6. Apply fixes to all 3 files
7. Re-validate → 3 violations remain (all WARNING)
8. Report combined metrics

---

## Before/After Examples

### Example 1: Constructor Missing Theme Integration

**Before**:
```csharp
public Control_InventoryTab()
{
    InitializeComponent();
    WireUpEvents();
}
```

**After**:
```csharp
public Control_InventoryTab()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    WireUpEvents();
    ApplyThemeColors();
}

private void ApplyThemeColors()
{
    var colors = Model_AppVariables.UserUiColors;
    Control_InventoryTab_Panel_Main.BackColor = colors.PanelBackColor ?? SystemColors.Control;
}
```

### Example 2: Hardcoded Color Replacement

**Before**:
```csharp
private void InitializeComponent()
{
    this.button1.BackColor = Color.Blue;
    this.textBox1.ForeColor = Color.White;
    this.panel1.BackColor = Color.FromArgb(30, 30, 30);
}
```

**After**:
```csharp
private void InitializeComponent()
{
    // Colors applied in ApplyThemeColors() method - removed hardcoded values
    this.button1.Name = "MyControl_Button_Action"; // Also fixed naming
    this.textBox1.Name = "MyControl_TextBox_Input";
    this.panel1.Name = "MyControl_Panel_Main";
}

// In code-behind .cs file:
private void ApplyThemeColors()
{
    var colors = Model_AppVariables.UserUiColors;
    MyControl_Button_Action.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
    MyControl_TextBox_Input.ForeColor = colors.TextBoxForeColor ?? SystemColors.WindowText;
    MyControl_Panel_Main.BackColor = colors.PanelBackColor ?? SystemColors.ControlDark;
}
```

### Example 3: Layout Architecture Fix

**Before**:
```csharp
this.panel1.Size = new Size(1175, 622); // Hardcoded size - problematic
this.comboBox1.Size = new Size(1073, 23); // Will expand uncontrollably
```

**After**:
```csharp
this.MyControl_Panel_Main.AutoSize = true;
this.MyControl_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
this.MyControl_Panel_Main.Dock = DockStyle.Fill;
// Removed hardcoded Size property

this.MyControl_ComboBox_Location.MinimumSize = new Size(175, 23);
this.MyControl_ComboBox_Location.MaximumSize = new Size(175, 23);
this.MyControl_ComboBox_Location.Dock = DockStyle.Fill;
// Added constraints, removed hardcoded Size
```

---

## Validation Checklist

After refactoring, verify:

- [ ] Constructor includes `Core_Themes.ApplyDpiScaling(this)` immediately after `InitializeComponent()`
- [ ] Constructor includes `Core_Themes.ApplyRuntimeLayoutAdjustments(this)` immediately after `ApplyDpiScaling`
- [ ] Call order correct: InitializeComponent → ApplyDpiScaling → ApplyRuntimeLayoutAdjustments → custom methods
- [ ] All hardcoded colors replaced with theme tokens OR documented with `// ACCEPTABLE:` comment
- [ ] Theme token usage follows pattern: `colors.PropertyName ?? SystemColors.Fallback`
- [ ] `ApplyThemeColors()` method created in `#region Theme Application`
- [ ] Control names follow `{ComponentName}_{ControlType}_{Purpose}` convention (if Designer.cs modified)
- [ ] Containers use `AutoSize = true` with `AutoSizeMode.GrowAndShrink`
- [ ] Leaf controls have `MinimumSize` and `MaximumSize` set
- [ ] No container widths exceed 1000px
- [ ] File compiles without errors
- [ ] No brace mismatches or corruption detected
- [ ] Backup created before modifications
- [ ] Validation shows zero CRITICAL and ERROR violations

---

## Rollback Procedure

If refactoring introduces issues:

```powershell
# 1. Identify backup location from remediation report
$backupPath = "backups/theme-refactor-{filename}-{timestamp}/"

# 2. Stop any running application instances
Stop-Process -Name "MTM_WIP_Application_Winforms" -Force -ErrorAction SilentlyContinue

# 3. Restore original files
Copy-Item -Path "$backupPath\*" -Destination "Controls/MainForm/" -Force

# 4. Rebuild solution
dotnet clean
dotnet build -c Debug

# 5. Verify restoration
git diff Controls/MainForm/Control_InventoryTab.cs
```

---

## Common Pitfalls and Solutions

### Pitfall 1: Missing using directives

**Symptom**: Compilation error "The type or namespace name 'Core_Themes' could not be found"

**Solution**: Ensure code-behind file includes:
```csharp
using MTM.Core;
using MTM.Models;
```

### Pitfall 2: Null reference on colors

**Symptom**: NullReferenceException when accessing `Model_AppVariables.UserUiColors`

**Solution**: Always use null-coalescing with SystemColors fallback:
```csharp
var colors = Model_AppVariables.UserUiColors;
control.BackColor = colors?.ButtonBackColor ?? SystemColors.Control;
```

### Pitfall 3: Designer regeneration breaks theme code

**Symptom**: Changes in Designer.cs overwrite theme integration

**Solution**: Keep ALL theme code in code-behind `.cs` file, NOT in Designer.cs. Only structural changes (AutoSize, Dock, MinimumSize) go in Designer.cs.

### Pitfall 4: Event handlers fire during theme application

**Symptom**: Unexpected behavior when theme colors applied

**Solution**: Apply themes AFTER event wiring:
```csharp
InitializeComponent();
Core_Themes.ApplyDpiScaling(this);
Core_Themes.ApplyRuntimeLayoutAdjustments(this);
WireUpEvents();        // Events wired
ApplyThemeColors();    // Colors applied AFTER events
```

---

## Cross-References

**Constitution**: Principle IX - Theme System Integration via Core_Themes  
**Instruction File**: `.github/instructions/ui-compliance/theming-compliance.instructions.md`  
**Reference Documentation**: `Documentation/Theme-System-Reference.md`  

**Related Instructions**:
- `.github/instructions/csharp-dotnet8.instructions.md` - WinForms patterns and region organization
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling guidance
- `.github/instructions/winforms-responsive-layout.instructions.md` - Layout architecture

**Compliance Tracking**:
- `specs/005-transaction-viewer-form/RefactorPortion/WinForms-UI-Compliance-Checklist.md` - Track refactored files

---

## Success Metrics

**Target Compliance**:
- 100% of Forms/UserControls have theme constructor pattern
- 95%+ of custom colors use theme tokens (5% allowance for documented brand colors)
- 100% of Designer.cs files follow naming convention
- 90%+ of layout uses AutoSize pattern (10% allowance for legacy constraints)

**Refactoring Session Success**:
- Zero CRITICAL violations remaining
- Zero ERROR violations remaining
- <5 WARNING violations per file (documented and justified)
- File compiles without errors
- Designer opens without errors (for Designer.cs modifications)
- All tests pass (if applicable)

---

## Version History

**v1.0.0** (2025-11-01):
- Initial release
- Comprehensive refactoring workflow
- Automatic fix application with backup/rollback
- Validation and reporting
- Common pitfall solutions
