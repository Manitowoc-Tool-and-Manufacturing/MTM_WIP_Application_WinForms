---
description: 'Theme system integration requirements for WinForms Forms and UserControls - DPI scaling, color tokens, and UI architecture compliance'
applyTo: '**/*.cs,**/*.Designer.cs'
mcpTools: [validate_ui_scaling, generate_ui_fix_plan, apply_ui_fixes, check_xml_docs]
version: '1.0.0'
lastUpdated: '2025-11-01'
constitution: 'Principle IX - Theme System Integration via Core_Themes'
relatedFiles:
  - 'Documentation/Theme-System-Reference.md'
  - '.github/instructions/csharp-dotnet8.instructions.md'
  - '.github/instructions/ui-scaling-consistency.instructions.md'
  - '.github/instructions/winforms-responsive-layout.instructions.md'
---

# Theme System Integration Compliance

## Scope

This instruction file governs theme system integration for ALL WinForms Forms and UserControls in the MTM WIP Application. It defines mandatory constructor patterns, color token usage, DPI scaling requirements, and UI architecture standards per Constitution Principle IX.

**Applies To**:
- All classes inheriting from `Form` or `UserControl`
- All `.Designer.cs` files defining WinForms layouts
- All code-behind files (`.cs`) that modify control properties at runtime

**Does NOT Apply To**:
- Development tools in `Forms/Development/**`
- Test fixtures in `Tests/**`
- Non-UI classes (DAOs, Services, Models, Helpers)

## Required Constructor Pattern (MANDATORY)

<!-- Required Constructor Pattern Start -->

Every Form and UserControl constructor MUST include these calls immediately after `InitializeComponent()`:

```csharp
public MyForm()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);              // MANDATORY - DPI awareness
    Core_Themes.ApplyRuntimeLayoutAdjustments(this); // MANDATORY - Dynamic layout fixes
    WireUpEvents();                                  // Optional - Event handler registration
    ApplyPrivileges();                               // Optional - Permission-based UI adjustments
}
```

**Call Order MUST Be**:
1. `InitializeComponent()` - Designer-generated initialization
2. `Core_Themes.ApplyDpiScaling(this)` - Scale controls for current DPI
3. `Core_Themes.ApplyRuntimeLayoutAdjustments(this)` - Apply layout corrections
4. Custom initialization methods (WireUpEvents, ApplyPrivileges, etc.)

**Rationale**: DPI scaling must occur before event handlers are wired to prevent double-triggering layout events. Runtime adjustments must occur after DPI scaling completes to correct any remaining layout issues.

<!-- Required Constructor Pattern End -->

## Color Token Usage (MANDATORY)

<!-- Color Token Usage Start -->

### Theme Token Pattern

All custom colors MUST use `Model_UserUiColors` theme tokens with `SystemColors` fallbacks:

```csharp
// ✅ CORRECT: Theme token with system fallback
var colors = Model_AppVariables.UserUiColors;
button1.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
textBox1.ForeColor = colors.TextBoxForeColor ?? SystemColors.WindowText;
panel1.BackColor = colors.PanelBackColor ?? SystemColors.ControlLight;
```

### Semantic Color Tokens

Use semantic tokens for status/validation colors:

```csharp
// ✅ CORRECT: Semantic theme colors
labelError.ForeColor = colors.ErrorColor ?? Color.Red;
labelSuccess.ForeColor = colors.SuccessColor ?? Color.Green;
labelWarning.ForeColor = colors.WarningColor ?? Color.Orange;
labelInfo.ForeColor = colors.InfoColor ?? Color.Blue;
panelHighlight.BackColor = colors.AccentColor ?? SystemColors.Highlight;
```

### SystemColors Fallback Pattern

When theme token is null, ALWAYS provide SystemColors fallback:

```csharp
// ✅ CORRECT: Null-coalescing with SystemColors
control.BackColor = colors.ControlBackColor ?? SystemColors.Control;
control.ForeColor = colors.ControlForeColor ?? SystemColors.ControlText;

// ❌ WRONG: No fallback (crashes if theme not loaded)
control.BackColor = colors.ControlBackColor;

// ❌ WRONG: Hardcoded fallback (ignores system theme)
control.BackColor = colors.ControlBackColor ?? Color.White;
```

<!-- Color Token Usage End -->

## Hardcoded Color Whitelist (EXCEPTIONS)

<!-- Hardcoded Color Whitelist Start -->

Hardcoded colors are FORBIDDEN except in these documented scenarios:

### 1. System Colors (Always Acceptable)

```csharp
// ✅ ACCEPTABLE: SystemColors integration
control.BackColor = SystemColors.Control;
control.ForeColor = SystemColors.WindowText;
panel.BackColor = SystemColors.ControlLight;
```

### 2. Brand Colors with Documentation

```csharp
// ✅ ACCEPTABLE: Brand color for company logo (not user-themeable)
panelCompanyLogo.BackColor = Color.FromArgb(0, 122, 204);

// ✅ ACCEPTABLE: Brand accent for marketing materials
labelCompanyName.ForeColor = Color.FromArgb(0, 122, 204);
```

**Requirement**: MUST include `// ACCEPTABLE: [reason]` comment explaining why theme tokens are insufficient.

### 3. Data Visualization Colors

```csharp
// ✅ ACCEPTABLE: Chart series colors for data distinction
// DATA COLOR: Pie chart requires 8 distinct hues for category visualization
var seriesColors = new Color[]
{
    Color.FromArgb(31, 119, 180),  // Blue
    Color.FromArgb(255, 127, 14),  // Orange
    Color.FromArgb(44, 160, 44),   // Green
    Color.FromArgb(214, 39, 40),   // Red
    Color.FromArgb(148, 103, 189), // Purple
    Color.FromArgb(140, 86, 75),   // Brown
    Color.FromArgb(227, 119, 194), // Pink
    Color.FromArgb(127, 127, 127)  // Gray
};
```

**Requirement**: MUST include `// DATA COLOR: [reason]` comment explaining data-driven necessity.

### 4. Development Tools (Automatic Exception)

Files in `Forms/Development/**` are exempt from theme compliance (development-time tools only).

### Forbidden Patterns

```csharp
// ❌ FORBIDDEN: Undocumented hardcoded color
button1.BackColor = Color.Blue;

// ❌ FORBIDDEN: Color.FromArgb without justification
panel1.BackColor = Color.FromArgb(30, 30, 30);

// ❌ FORBIDDEN: Direct color assignment bypassing theme
textBox1.ForeColor = Color.White;
```

<!-- Hardcoded Color Whitelist End -->

## WinForms UI Architecture Requirements

<!-- UI Architecture Start -->

### Control Naming Convention (MANDATORY)

**Format**: `{ComponentName}_{ControlType}_{Purpose}`

```csharp
// ✅ CORRECT: Full type name with context
private Panel TransactionSearchControl_Panel_Main;
private TableLayoutPanel TransactionSearchControl_TableLayout_Filters;
private ComboBox TransactionSearchControl_ComboBox_PartNumber;
private Label TransactionSearchControl_Label_PartNumber;
private Button TransactionSearchControl_Button_Search;

// ❌ WRONG: Abbreviations
private Panel pnlMain;
private ComboBox cboPartNumber;
private Button btnSearch;

// ❌ WRONG: Generic names without context
private Panel panel1;
private ComboBox comboBox3;
```

**No Abbreviations**: Always use full control type names
- `ComboBox` not `cbo`
- `TextBox` not `txt`
- `Label` not `lbl`
- `Button` not `btn`
- `DataGridView` not `dgv`

### Layout Architecture (MANDATORY)

**AutoSize Cascade Pattern**:

```csharp
// Root container
myControl_Panel_Main.AutoSize = true;
myControl_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
myControl_Panel_Main.Dock = DockStyle.Fill;

// Master layout
myControl_TableLayout_Main.AutoSize = true;
myControl_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
myControl_TableLayout_Main.Dock = DockStyle.Fill;
myControl_TableLayout_Main.Margin = new Padding(0);
```

**Per-Field Container Pattern**:

```csharp
// Each input field gets dedicated TableLayoutPanel
myControl_TableLayout_PartNumber.AutoSize = true;
myControl_TableLayout_PartNumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
myControl_TableLayout_PartNumber.ColumnCount = 1;
myControl_TableLayout_PartNumber.RowCount = 2;

// Row 0: Label (AutoSize)
myControl_TableLayout_PartNumber.RowStyles.Add(new RowStyle());

// Row 1: Control (Percent 100%)
myControl_TableLayout_PartNumber.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
```

**Leaf Control Constraints**:

```csharp
// Input controls MUST have MinimumSize and MaximumSize
myControl_ComboBox_PartNumber.MinimumSize = new Size(175, 23);
myControl_ComboBox_PartNumber.MaximumSize = new Size(175, 23);
myControl_ComboBox_PartNumber.Dock = DockStyle.Fill;

myControl_TextBox_Notes.MinimumSize = new Size(175, 23);
myControl_TextBox_Notes.MaximumSize = new Size(175, 23);
myControl_TextBox_Notes.Dock = DockStyle.Fill;
```

**Forbidden Patterns**:

```csharp
// ❌ WRONG: Hardcoded Size on container
myControl_Panel_Main.Size = new Size(1175, 622);

// ❌ WRONG: No AutoSize on container
myControl_TableLayout_Main.AutoSize = false;

// ❌ WRONG: Missing MinimumSize/MaximumSize on leaf control
myControl_ComboBox_PartNumber.Size = new Size(1073, 23); // Will expand uncontrollably

// ❌ WRONG: Width exceeding 1000px (indicates missing AutoSize)
myControl_ComboBox_Location.Size = new Size(1073, 23);
```

<!-- UI Architecture End -->

## AutoScaleMode Requirement

<!-- AutoScaleMode Start -->

Every Form and UserControl MUST set `AutoScaleMode = AutoScaleMode.Dpi`:

```csharp
// In Designer.cs InitializeComponent()
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
```

**Rationale**: Enables per-monitor DPI awareness. Required for `Core_Themes.ApplyDpiScaling` to function correctly.

**Forbidden Values**:
- `AutoScaleMode.Font` (legacy, inconsistent scaling)
- `AutoScaleMode.None` (disables DPI awareness)
- `AutoScaleMode.Inherit` (unpredictable in nested controls)

<!-- AutoScaleMode End -->

## Database-Backed Theme System

<!-- Database Theme System Start -->

### Theme Storage

Themes are stored in MySQL `app_themes` table:
- **9 available themes**: Arctic, Default, Fire Storm, Glacier, Ice, Midnight, Neon, Sunset, Wood
- **203 color properties per theme**: Stored as JSON in `SettingsJson` column
- **User selection**: Stored in `md_users.ThemeName` column

### Theme Loading

Themes load automatically at application startup via `Core_Themes.LoadTheme()`:

```csharp
// Application startup (Program.cs)
Core_Themes.LoadTheme(currentUser.ThemeName);

// Theme applied to Model_AppVariables.UserUiColors
var colors = Model_AppVariables.UserUiColors;
```

### Theme Tokens Available

203 properties accessible via `Model_UserUiColors` (all nullable `Color?`):

**Common Controls**:
- `ButtonBackColor`, `ButtonForeColor`, `ButtonBorderColor`
- `TextBoxBackColor`, `TextBoxForeColor`, `TextBoxBorderColor`
- `ComboBoxBackColor`, `ComboBoxForeColor`, `ComboBoxBorderColor`
- `LabelForeColor`, `PanelBackColor`

**Semantic Colors**:
- `ErrorColor`, `WarningColor`, `SuccessColor`, `InfoColor`, `AccentColor`

**Data Grids**:
- `DataGridViewBackColor`, `DataGridViewForeColor`
- `DataGridViewRowBackColor`, `DataGridViewAlternatingRowBackColor`
- `DataGridViewHeaderBackColor`, `DataGridViewHeaderForeColor`

**See**: `Documentation/Theme-System-Reference.md` Section 4 for complete catalog.

<!-- Database Theme System End -->

## Validation and Enforcement

<!-- Validation Start -->

### MCP Tool Integration

**validate_ui_scaling**:
- Scans `.cs` and `.Designer.cs` files for theme compliance
- Checks for `Core_Themes.ApplyDpiScaling` and `ApplyRuntimeLayoutAdjustments` calls
- Detects hardcoded colors without `// ACCEPTABLE:` comments
- Validates control naming conventions
- Flags widths exceeding 1000px

**generate_ui_fix_plan**:
- Generates JSON fix plan from validation results
- Includes before/after code snippets
- Provides line numbers for edits
- Includes risk assessment and effort estimates

**apply_ui_fixes**:
- Applies fix plan with backup creation
- Performs pre/post validation
- Detects corruption (mismatched braces, size changes >50%)
- Automatic rollback on failure

### Code Review Checklist

- [ ] Constructor includes `Core_Themes.ApplyDpiScaling(this)`
- [ ] Constructor includes `Core_Themes.ApplyRuntimeLayoutAdjustments(this)`
- [ ] Call order: InitializeComponent → ApplyDpiScaling → ApplyRuntimeLayoutAdjustments
- [ ] Custom colors use `Model_UserUiColors` tokens with `SystemColors` fallbacks
- [ ] Hardcoded colors have `// ACCEPTABLE:` or `// DATA COLOR:` justification
- [ ] Control names follow `{ComponentName}_{ControlType}_{Purpose}` convention
- [ ] No abbreviations in control names (ComboBox not cbo, TextBox not txt)
- [ ] All containers use `AutoSize = true` with `AutoSizeMode.GrowAndShrink`
- [ ] Leaf controls have `MinimumSize` and `MaximumSize` set
- [ ] No container widths exceed 1000px
- [ ] `AutoScaleMode = AutoScaleMode.Dpi` set on Form/UserControl

### Refactoring Workflow

When refactoring non-compliant files:

1. **Pre-Refactor Analysis**: Run `validate_ui_scaling` to identify violations
2. **Generate Fix Plan**: Run `generate_ui_fix_plan` to create remediation plan
3. **Review Fix Plan**: Manually review before applying automated fixes
4. **Apply Fixes**: Run `apply_ui_fixes` with backup enabled
5. **Validate**: Re-run `validate_ui_scaling` to confirm compliance
6. **Manual Testing**: Open Form/UserControl in designer, test at 100%/125%/150%/200% DPI

<!-- Validation End -->

## Examples

<!-- Examples Start -->

### Complete Compliant Form

```csharp
// MyForm.cs
using System;
using System.Windows.Forms;

namespace MTM.Forms
{
    public partial class MyForm : Form
    {
        #region Constructors
        
        public MyForm()
        {
            InitializeComponent();
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);
            WireUpEvents();
            ApplyThemeColors();
        }
        
        #endregion
        
        #region Theme Application
        
        private void ApplyThemeColors()
        {
            var colors = Model_AppVariables.UserUiColors;
            
            // Use theme tokens with SystemColors fallbacks
            MyForm_Panel_Main.BackColor = colors.PanelBackColor ?? SystemColors.Control;
            MyForm_Button_Save.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
            MyForm_Button_Save.ForeColor = colors.ButtonForeColor ?? SystemColors.ControlText;
            MyForm_TextBox_Input.BackColor = colors.TextBoxBackColor ?? SystemColors.Window;
            MyForm_TextBox_Input.ForeColor = colors.TextBoxForeColor ?? SystemColors.WindowText;
            
            // Semantic colors for validation
            MyForm_Label_Error.ForeColor = colors.ErrorColor ?? Color.Red;
            MyForm_Label_Success.ForeColor = colors.SuccessColor ?? Color.Green;
        }
        
        #endregion
    }
}
```

### Complete Compliant Designer.cs

```csharp
// MyForm.Designer.cs
partial class MyForm
{
    private void InitializeComponent()
    {
        this.MyForm_Panel_Main = new System.Windows.Forms.Panel();
        this.MyForm_TableLayout_Main = new System.Windows.Forms.TableLayoutPanel();
        this.MyForm_TableLayout_Input = new System.Windows.Forms.TableLayoutPanel();
        this.MyForm_Label_Input = new System.Windows.Forms.Label();
        this.MyForm_TextBox_Input = new System.Windows.Forms.TextBox();
        this.MyForm_Button_Save = new System.Windows.Forms.Button();
        
        this.MyForm_Panel_Main.SuspendLayout();
        this.MyForm_TableLayout_Main.SuspendLayout();
        this.MyForm_TableLayout_Input.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // MyForm_Panel_Main
        // 
        this.MyForm_Panel_Main.AutoSize = true;
        this.MyForm_Panel_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.MyForm_Panel_Main.Controls.Add(this.MyForm_TableLayout_Main);
        this.MyForm_Panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
        this.MyForm_Panel_Main.Padding = new System.Windows.Forms.Padding(0);
        
        // 
        // MyForm_TableLayout_Main
        // 
        this.MyForm_TableLayout_Main.AutoSize = true;
        this.MyForm_TableLayout_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.MyForm_TableLayout_Main.ColumnCount = 1;
        this.MyForm_TableLayout_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.MyForm_TableLayout_Main.Controls.Add(this.MyForm_TableLayout_Input, 0, 0);
        this.MyForm_TableLayout_Main.Controls.Add(this.MyForm_Button_Save, 0, 1);
        this.MyForm_TableLayout_Main.Dock = System.Windows.Forms.DockStyle.Fill;
        this.MyForm_TableLayout_Main.RowCount = 2;
        this.MyForm_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.MyForm_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
        
        // 
        // MyForm_TableLayout_Input
        // 
        this.MyForm_TableLayout_Input.AutoSize = true;
        this.MyForm_TableLayout_Input.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.MyForm_TableLayout_Input.ColumnCount = 1;
        this.MyForm_TableLayout_Input.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.MyForm_TableLayout_Input.Controls.Add(this.MyForm_Label_Input, 0, 0);
        this.MyForm_TableLayout_Input.Controls.Add(this.MyForm_TextBox_Input, 0, 1);
        this.MyForm_TableLayout_Input.Dock = System.Windows.Forms.DockStyle.Fill;
        this.MyForm_TableLayout_Input.RowCount = 2;
        this.MyForm_TableLayout_Input.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.MyForm_TableLayout_Input.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        
        // 
        // MyForm_Label_Input
        // 
        this.MyForm_Label_Input.AutoSize = true;
        this.MyForm_Label_Input.Dock = System.Windows.Forms.DockStyle.Fill;
        this.MyForm_Label_Input.Text = "Input:";
        
        // 
        // MyForm_TextBox_Input
        // 
        this.MyForm_TextBox_Input.Dock = System.Windows.Forms.DockStyle.Fill;
        this.MyForm_TextBox_Input.MinimumSize = new System.Drawing.Size(175, 23);
        this.MyForm_TextBox_Input.MaximumSize = new System.Drawing.Size(175, 23);
        
        // 
        // MyForm_Button_Save
        // 
        this.MyForm_Button_Save.AutoSize = true;
        this.MyForm_Button_Save.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.MyForm_Button_Save.MinimumSize = new System.Drawing.Size(75, 23);
        this.MyForm_Button_Save.MaximumSize = new System.Drawing.Size(75, 23);
        this.MyForm_Button_Save.Text = "Save";
        
        // 
        // MyForm
        // 
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.ClientSize = new System.Drawing.Size(400, 300);
        this.Controls.Add(this.MyForm_Panel_Main);
        
        this.MyForm_Panel_Main.ResumeLayout(false);
        this.MyForm_Panel_Main.PerformLayout();
        this.MyForm_TableLayout_Main.ResumeLayout(false);
        this.MyForm_TableLayout_Main.PerformLayout();
        this.MyForm_TableLayout_Input.ResumeLayout(false);
        this.MyForm_TableLayout_Input.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
    
    private System.Windows.Forms.Panel MyForm_Panel_Main;
    private System.Windows.Forms.TableLayoutPanel MyForm_TableLayout_Main;
    private System.Windows.Forms.TableLayoutPanel MyForm_TableLayout_Input;
    private System.Windows.Forms.Label MyForm_Label_Input;
    private System.Windows.Forms.TextBox MyForm_TextBox_Input;
    private System.Windows.Forms.Button MyForm_Button_Save;
}
```

<!-- Examples End -->

## Cross-References

**Constitution**: Principle IX - Theme System Integration via Core_Themes  
**Reference Documentation**: `Documentation/Theme-System-Reference.md`  
**Related Instructions**:
- `.github/instructions/csharp-dotnet8.instructions.md` - WinForms Patterns section
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling guidance
- `.github/instructions/winforms-responsive-layout.instructions.md` - Layout architecture

**Related Prompts**:
- `.github/prompts/refactor-theme-compliance.prompt.md` - Automated theme refactoring

**Compliance Tracking**:
- `specs/005-transaction-viewer-form/RefactorPortion/WinForms-UI-Compliance-Checklist.md`
- `specs/005-transaction-viewer-form/RefactorPortion/UI-Architecture-Analysis.md`

## Version History

**v1.0.0** (2025-11-01):
- Initial release
- Comprehensive theme integration requirements
- MCP tool integration metadata
- Complete code examples
