---
description: 'UI scaling and DPI consistency standards for WinForms controls across all screen sizes and resolutions'
applyTo: '**/*.cs,**/*.Designer.cs'
---

# UI Scaling and DPI Consistency Standards

**Cross-Reference**: This file works with `.github/instructions/winforms-responsive-layout.instructions.md` which covers layout architecture patterns. This file focuses specifically on DPI scaling and size consistency.

## Overview

This file defines standards and patterns for ensuring all WinForms controls maintain consistent visual appearance, aspect ratios, and usability across different screen sizes, resolutions, and DPI settings in Windows.

**Critical**: Manufacturing environments often have mixed hardware - from 1080p monitors to 4K displays, from standard DPI (96) to high DPI (144-192). Controls MUST look and function consistently across all configurations.

## Relevant MCP Tools

- `validate_ui_scaling` – Primary validator for DPI compliance; run against forms and controls after layout changes to capture scaling regressions.
- `generate_ui_fix_plan` – Produces JSON action lists for correcting layout issues flagged by the validator.
- `apply_ui_fixes` – Applies generated fix plans with backups and corruption checks to safely update designer files.
- `analyze_performance` – Helpful when UI responsiveness issues arise in tandem with scaling concerns (detects UI thread blocking or layout hotspots).

---

## Core Principles

### 1. Visual Consistency
- **Fixed aspect ratios**: Controls maintain proportional sizing across resolutions
- **Readable text**: Font sizes scale appropriately with DPI
- **Touch-friendly targets**: Buttons/controls remain adequately sized for interaction
- **Alignment preservation**: Relative positioning stays consistent

### 2. DPI Awareness
- **AutoScaleMode**: Use `Dpi` mode for all forms and user controls
- **Dynamic scaling**: Let WinForms handle DPI scaling automatically
- **Theme integration**: Use `Core_Themes.ApplyDpiScaling(this)` in all constructors
- **Runtime adjustments**: Apply `Core_Themes.ApplyRuntimeLayoutAdjustments(this)` for fine-tuning

### 3. Layout Strategy
- **Anchoring**: Use `Anchor` property for responsive behavior
- **Docking**: Use `Dock` for fill/edge-aligned controls
- **TableLayoutPanel**: Preferred for complex responsive layouts
- **FlowLayoutPanel**: Use for dynamic content that wraps
- **Avoid absolute positioning**: Use layout containers instead of fixed X/Y coordinates

---

## Standard Patterns

### Form Initialization Pattern

**REQUIRED** in all Form and UserControl constructors:

```csharp
public partial class MyForm : Form
{
    public MyForm()
    {
        InitializeComponent();
        
        // ✅ REQUIRED: Apply DPI scaling
        Core_Themes.ApplyDpiScaling(this);
        
        // ✅ REQUIRED: Apply runtime layout adjustments
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        // Additional initialization...
    }
}
```

**Why this matters**:
- `ApplyDpiScaling`: Adjusts control sizes based on current DPI (96/120/144/192)
- `ApplyRuntimeLayoutAdjustments`: Fixes layout issues from designer vs runtime differences
- Missing these causes controls to appear too small/large on different displays

---

### AutoScaleMode Configuration

**REQUIRED** in all Form.Designer.cs files:

```csharp
private void InitializeComponent()
{
    // ... other initialization ...
    
    // ✅ CORRECT: Use Dpi mode
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
    this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
    
    // ❌ WRONG: Font mode doesn't handle DPI properly
    // this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    
    // ❌ WRONG: None disables scaling entirely
    // this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
}
```

**Validation**:
```csharp
// All forms/controls MUST have:
Assert.AreEqual(AutoScaleMode.Dpi, form.AutoScaleMode);
```

---

### Layout Container Best Practices

#### TableLayoutPanel (Preferred for Grid Layouts)

```csharp
// ✅ CORRECT: Use percentage-based sizing
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F)); // Label column
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F)); // Input column

// ✅ CORRECT: Auto-size rows based on content
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

// ✅ CORRECT: Use Dock.Fill for responsive behavior
textBox.Dock = DockStyle.Fill;
textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
```

#### FlowLayoutPanel (For Wrapping Content)

```csharp
// ✅ CORRECT: Allow wrapping for responsive layouts
flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
flowLayoutPanel.WrapContents = true;
flowLayoutPanel.AutoSize = true;
flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
```

#### Panel with Anchoring

```csharp
// ✅ CORRECT: Anchor controls to maintain relative positioning
okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

// ✅ CORRECT: Dock for full-width controls
dataGridView.Dock = DockStyle.Fill;
```

---

### Font Sizing Standards

#### Base Font Sizes (at 96 DPI)

```csharp
// Standard text (forms, labels, inputs)
Font = new Font("Segoe UI", 9F, FontStyle.Regular);

// Headers/section titles
Font = new Font("Segoe UI", 12F, FontStyle.Bold);

// Small text (hints, metadata)
Font = new Font("Segoe UI", 8F, FontStyle.Regular);

// Large displays (dashboards, status)
Font = new Font("Segoe UI", 14F, FontStyle.Bold);
```

**DPI Scaling**:
- At 120 DPI (125%): 9pt → 11.25pt
- At 144 DPI (150%): 9pt → 13.5pt
- At 192 DPI (200%): 9pt → 18pt

**Validation**: Fonts MUST be specified in points (pt), not pixels (px).

---

### Control Sizing Standards

#### Minimum Touch Targets

```csharp
// ✅ Buttons must be at least 32x32 at 96 DPI (scales to 40x40 at 120 DPI)
button.MinimumSize = new Size(75, 32);  // Width x Height

// ✅ Touch-friendly spacing between interactive elements
const int MinimumSpacing = 8; // Pixels between controls
```

#### Standard Control Heights (at 96 DPI)

```csharp
// TextBox, ComboBox (single line)
control.Height = 23;

// Button (standard)
button.Height = 32;

// DataGridView row height
dataGridView.RowTemplate.Height = 28;

// Label (auto-size preferred)
label.AutoSize = true;
```

**Note**: These scale automatically with DPI when `AutoScaleMode.Dpi` is used.

---

### DataGridView Responsive Patterns

```csharp
// ✅ CORRECT: Use percentage-based column widths
dataGridView.Columns["PartID"].FillWeight = 20;      // 20% of available width
dataGridView.Columns["Description"].FillWeight = 40;  // 40% of available width
dataGridView.Columns["Quantity"].FillWeight = 15;     // 15% of available width
dataGridView.Columns["Location"].FillWeight = 25;     // 25% of available width

// ✅ CORRECT: Enable auto-resize for last column
dataGridView.Columns[dataGridView.Columns.Count - 1].AutoSizeMode = 
    DataGridViewAutoSizeColumnMode.Fill;

// ✅ CORRECT: Set minimum widths to prevent over-shrinking
dataGridView.Columns["PartID"].MinimumWidth = 80;

// ❌ WRONG: Fixed pixel widths don't scale
// dataGridView.Columns["PartID"].Width = 150; // Bad!
```

---

### Margin and Padding Standards

```csharp
// Standard spacing (at 96 DPI - scales with DPI)
const int SmallMargin = 4;   // Between related controls
const int MediumMargin = 8;  // Between control groups
const int LargeMargin = 16;  // Between major sections

// Panel padding
panel.Padding = new Padding(8); // All sides

// Asymmetric padding
groupBox.Padding = new Padding(8, 16, 8, 8); // Left, Top, Right, Bottom
```

---

## DPI Scaling Scenarios

### Scenario 1: 1080p Monitor (1920x1080, 96 DPI)
- **Base resolution**: Standard sizing, no scaling
- **Font 9pt**: Renders at 9pt
- **Button 75x32**: Renders at 75x32 pixels
- **Expected**: Baseline reference for design

### Scenario 2: 1080p Monitor with 125% Scaling (1920x1080, 120 DPI)
- **DPI scaling**: All controls scale by 1.25x
- **Font 9pt**: Renders at 11.25pt
- **Button 75x32**: Renders at ~94x40 pixels
- **Expected**: Slightly larger, more readable

### Scenario 3: 4K Monitor (3840x2160, 144 DPI at 150%)
- **DPI scaling**: All controls scale by 1.5x
- **Font 9pt**: Renders at 13.5pt
- **Button 75x32**: Renders at ~113x48 pixels
- **Expected**: Significantly larger, touch-friendly

### Scenario 4: 4K Monitor (3840x2160, 192 DPI at 200%)
- **DPI scaling**: All controls scale by 2x
- **Font 9pt**: Renders at 18pt
- **Button 75x32**: Renders at 150x64 pixels
- **Expected**: Double size, very touch-friendly

---

## Testing & Validation

### Manual Testing Checklist

Test each form/control at these configurations:

- [ ] **1920x1080 @ 96 DPI (100%)** - Standard desktop
- [ ] **1920x1080 @ 120 DPI (125%)** - Common laptop setting
- [ ] **2560x1440 @ 96 DPI (100%)** - High-res desktop
- [ ] **2560x1440 @ 144 DPI (150%)** - High-res laptop
- [ ] **3840x2160 @ 192 DPI (200%)** - 4K display
- [ ] **Minimum resolution: 1366x768** - Old/small displays

### Validation Points

For each configuration, verify:

1. ✅ **Text readability**: All text legible without strain
2. ✅ **Button targets**: All buttons easy to click/touch
3. ✅ **Spacing consistency**: Controls don't overlap or have excessive gaps
4. ✅ **Alignment**: Labels align with inputs, columns align in grids
5. ✅ **No clipping**: No text/controls cut off or hidden
6. ✅ **No scrollbars**: Forms fit on screen at target resolution (unless intentional)
7. ✅ **Aspect ratio**: Proportions look consistent across resolutions

### Common Issues to Check

```csharp
// ❌ ISSUE: Form too large for small screens
// FIX: Use MaximumSize or make form resizable with minimum size
this.MinimumSize = new Size(800, 600);
this.MaximumSize = new Size(1600, 1200);

// ❌ ISSUE: Controls overlap at high DPI
// FIX: Use TableLayoutPanel instead of absolute positioning

// ❌ ISSUE: Text clipped in labels
// FIX: Set AutoSize = true or use sufficient Width with AutoEllipsis

// ❌ ISSUE: DataGridView columns too narrow at low DPI
// FIX: Set MinimumWidth on columns
```

---

## MCP Validation Tool Requirements

### Tool Purpose
Create an MCP tool that validates UI scaling consistency across all forms and user controls.

### Validation Checks

1. **AutoScaleMode Verification**
   - All forms/controls use `AutoScaleMode.Dpi`
   - Correct AutoScaleDimensions (96F, 96F)

2. **Constructor Pattern Check**
   - `Core_Themes.ApplyDpiScaling(this)` present
   - `Core_Themes.ApplyRuntimeLayoutAdjustments(this)` present
   - Called in correct order (after InitializeComponent)

3. **Font Size Validation**
   - All fonts specified in points (not pixels)
   - Font sizes within acceptable ranges (8pt-18pt for standard controls)
   - No hardcoded pixel-based font sizes

4. **Control Sizing**
   - Minimum button sizes met (75x32 at 96 DPI)
   - Touch targets adequate (32px minimum height)
   - TextBox heights consistent

5. **Layout Container Usage**
   - Percentage-based sizing in TableLayoutPanel
   - Proper anchoring on controls
   - No excessive absolute positioning (X/Y coordinates)

6. **DataGridView Configuration**
   - Column widths use FillWeight or Fill mode
   - MinimumWidth set on columns
   - No fixed pixel widths on columns

### Output Format

```csv
FormName,ControlName,IssueType,Severity,CurrentValue,ExpectedValue,Location
MainForm,btnSave,MinimumSize,Warning,70x28,75x32,MainForm.Designer.cs:145
Control_Inventory,txtPartID,AutoScaleMode,Error,Font,Dpi,Control_Inventory.Designer.cs:89
Control_Settings,dataGridView1,ColumnWidth,Warning,Fixed(150),FillWeight,Control_Settings.Designer.cs:234
```

### Severity Levels

- **Error**: Blocks proper DPI scaling (e.g., wrong AutoScaleMode)
- **Warning**: May cause issues at extreme DPI (e.g., small button size)
- **Info**: Recommendation for best practices (e.g., could use TableLayoutPanel)

---

## Migration Guide

### Fixing Existing Forms

**Step 1**: Update AutoScaleMode in Designer.cs
```csharp
// Change from Font to Dpi
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
```

**Step 2**: Add theme calls to constructor
```csharp
public MyForm()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

**Step 3**: Fix absolute positioning
```csharp
// Before: Absolute positioning
button1.Location = new Point(450, 380);
button2.Location = new Point(550, 380);

// After: Use anchoring
button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
```

**Step 4**: Convert DataGridView columns
```csharp
// Before: Fixed widths
dataGridView.Columns["Part"].Width = 150;

// After: Percentage-based
dataGridView.Columns["Part"].FillWeight = 30;
dataGridView.Columns["Part"].MinimumWidth = 100;
```

---

## Reference Examples

### Compliant Form Example

```csharp
public partial class CompliantForm : Form
{
    public CompliantForm()
    {
        InitializeComponent();
        
        // ✅ Required theme calls
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        SetupResponsiveLayout();
    }
    
    private void SetupResponsiveLayout()
    {
        // ✅ Use TableLayoutPanel for responsive grid
        var tableLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3,
            Padding = new Padding(8)
        };
        
        // ✅ Percentage-based columns
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
        
        // ✅ Auto-size rows
        tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        
        // ✅ Proper control sizing
        var submitButton = new Button
        {
            Text = "Submit",
            MinimumSize = new Size(75, 32),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right
        };
        
        this.Controls.Add(tableLayout);
    }
}

// Designer.cs
private void InitializeComponent()
{
    // ✅ Correct AutoScaleMode
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
    this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
    
    // ✅ Fonts in points
    this.Font = new System.Drawing.Font("Segoe UI", 9F);
    
    // ✅ Minimum form size
    this.MinimumSize = new System.Drawing.Size(800, 600);
}
```

---

## Troubleshooting

### Issue: Controls too small on high DPI
**Cause**: Missing `Core_Themes.ApplyDpiScaling(this)`  
**Fix**: Add to constructor after InitializeComponent()

### Issue: Layout broken after scaling
**Cause**: Absolute positioning with fixed coordinates  
**Fix**: Use Anchor/Dock or layout panels

### Issue: Text clipped in labels
**Cause**: Fixed width with long text  
**Fix**: Set AutoSize = true or increase width

### Issue: DataGridView columns not resizing
**Cause**: Fixed pixel widths on columns  
**Fix**: Use FillWeight or Fill mode with MinimumWidth

### Issue: Form doesn't fit on screen
**Cause**: Form size designed for single resolution  
**Fix**: Set MaximumSize and make resizable, or reduce content

---

## Success Criteria

A form/control is DPI-compliant when:

- ✅ Works correctly at 96, 120, 144, and 192 DPI
- ✅ Maintains aspect ratios across resolutions
- ✅ Text remains readable at all DPI settings
- ✅ Interactive controls remain usable (adequate touch targets)
- ✅ No clipping, overlapping, or layout breaks
- ✅ Passes MCP validation tool with 0 errors
- ✅ Manual testing confirms visual consistency

---

## Related Documentation

- Core_Themes.cs - DPI scaling implementation
- .github/instructions/csharp-dotnet8.instructions.md - WinForms patterns
- .github/instructions/performance-optimization.instructions.md - UI performance
