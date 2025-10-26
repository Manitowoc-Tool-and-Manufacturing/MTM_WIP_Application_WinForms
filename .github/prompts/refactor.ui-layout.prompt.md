---
description: Refactor WinForms Designer files to implement responsive grid-based layout following MTM standards
---

## Agent Communication Rules

**⚠️ CRITICAL - Work Completion Standards**:

- **Read instruction files FIRST** before making ANY changes
- **Analyze existing layout** completely before proposing changes
- **Make incremental changes** - one layout section at a time
- **Validate after each change** - ensure code compiles
- **Test the result** - verify form/control renders without errors
- **Continue through all issues** found in validation
- **Complete all refactoring steps** in a single session when possible

**Do NOT**:
- Make assumptions about layout structure without reading the files
- Skip reading the instruction files
- Stop after fixing one issue when multiple issues exist
- Leave partially completed refactoring

---

## Objective

Refactor a WinForms UserControl or Form to implement professional responsive layout using TableLayoutPanel, proper margins/padding, Min/Max sizes, and grid-based design patterns.

---

## Required Context

Before starting, you MUST read:

1. **`.github/instructions/winforms-responsive-layout.instructions.md`** - Complete layout architecture guide
2. **`.github/instructions/ui-scaling-consistency.instructions.md`** - DPI scaling standards
3. **Target Designer file** (`.Designer.cs`) - The file to refactor
4. **Target code-behind file** (`.cs`) - Supporting logic

---

## User Input

```text
$ARGUMENTS
```

Expected format: Path to Designer file to refactor (e.g., `Forms/ErrorReports/Form_ViewErrorReports.Designer.cs`)

---

## Refactoring Workflow

### Step 1: Analysis Phase

**Load and analyze target files:**

```bash
# Read the responsive layout instruction file
Read: .github/instructions/winforms-responsive-layout.instructions.md

# Read UI scaling instruction file
Read: .github/instructions/ui-scaling-consistency.instructions.md

# Read target Designer file
Read: [TargetDesignerFile]

# Read corresponding code-behind
Read: [TargetCodeBehindFile]
```

**Analyze current layout:**
- Identify container type (Form, UserControl, Panel)
- List all child controls and their current positioning
- Note current sizing approach (fixed, docked, anchored)
- Identify layout issues:
  - Missing Margin/Padding
  - No Min/MaximumSize constraints
  - All controls using fixed positions
  - Improper Anchor/Dock settings
  - Missing TableLayoutPanel structure
  - Wrong Z-order for docked controls
  - No responsive row/column sizing

**Document findings:**
```markdown
## Current Layout Analysis

**Container Type**: [Form/UserControl]
**Current Approach**: [Fixed positions / Basic docking / etc.]

**Child Controls**:
1. [ControlName] - [Type] - [Current positioning]
2. [ControlName] - [Type] - [Current positioning]
...

**Issues Found**:
- [ ] No TableLayoutPanel structure
- [ ] Missing Margin/Padding
- [ ] No MinimumSize/MaximumSize
- [ ] Improper Anchor settings
- [ ] Fixed column/row percentages
- [ ] etc.

**Recommended Changes**:
1. [Change description]
2. [Change description]
...
```

### Step 2: Planning Phase

**Create refactoring plan based on control layout:**

**For Filter/Search Panels:**
```csharp
// Pattern: Fixed labels + flexible controls
ColumnStyles:
- Absolute(100) - Label
- Percent(33.33) - Control
- Absolute(100) - Label
- Percent(33.33) - Control
- Absolute(100) - Label
- Percent(33.34) - Control
```

**For Main Content Areas:**
```csharp
// Pattern: Header + Content + Footer
RowStyles:
- Absolute(80) or AutoSize - Header/Filters
- Percent(100) - Main content (DataGridView, etc.)
- Absolute(30-40) - Status/Footer
```

**For Master-Detail Views:**
```csharp
// Use SplitContainer instead of TableLayoutPanel
SplitContainer:
- Orientation: Horizontal or Vertical
- Panel1: Master list/grid
- Panel2: Detail view
- User-adjustable splitter
```

### Step 3: Implementation Phase

**Execute changes in this order:**

#### 3.1: Backup Current Approach (Comment Out)
```csharp
// Keep original InitializeComponent logic as comments for reference
// Original positioning:
// this.button1.Location = new Point(10, 10);
// this.button1.Size = new Size(75, 23);
```

#### 3.2: Create TableLayoutPanel Structure
```csharp
private TableLayoutPanel CreateMainLayout()
{
    var layout = new TableLayoutPanel
    {
        Dock = DockStyle.Fill,
        ColumnCount = [N],
        RowCount = [M],
        Padding = new Padding(10),
        AutoSize = false
    };
    
    // Add column styles (mix Absolute and Percent)
    layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
    layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
    
    // Add row styles
    layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
    
    return layout;
}
```

#### 3.3: Add Margins and Padding
```csharp
// Container padding
mainLayout.Padding = new Padding(10);

// Control margins
dgvData.Margin = new Padding(5);
filterPanel.Margin = new Padding(0, 0, 0, 10); // Extra bottom space
statusLabel.Margin = new Padding(0, 10, 0, 0); // Extra top space
```

#### 3.4: Set Min/Max Sizes
```csharp
// DataGridView
dgvData.MinimumSize = new Size(400, 200);

// Filter panels
filterPanel.MinimumSize = new Size(600, 40);

// Form/UserControl
this.MinimumSize = new Size(800, 600);
```

#### 3.5: Configure Anchor/Dock Properties
```csharp
// Main content fills
dgvData.Dock = DockStyle.Fill;

// Buttons anchor to corners
btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

// Search box stretches horizontally
txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
```

#### 3.6: Add Controls in Correct Z-Order
```csharp
this.SuspendLayout();

// Add in reverse visual order (back to front)
this.Controls.Add(mainLayout);  // Fill control first
// Then any docked controls

this.ResumeLayout(false);
this.PerformLayout();
```

#### 3.7: Update Code-Behind (if needed)
```csharp
public TargetControl()
{
    InitializeComponent();
    
    // Add if missing
    SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

### Step 4: Validation Phase

**After implementation, validate:**

```bash
# Build to check for compilation errors
dotnet build [ProjectFile] -c Debug

# Get errors in specific file
get_errors(filePaths=["[TargetDesignerFile]", "[TargetCodeBehindFile]"])
```

**Manual checks:**
- [ ] Code compiles without errors
- [ ] Designer file opens without errors in Visual Studio
- [ ] Form/Control renders correctly
- [ ] All controls visible at minimum size
- [ ] Margins and padding look professional
- [ ] Controls resize appropriately with form
- [ ] No overlap or clipping at various sizes

### Step 5: Documentation Phase

**Add XML comments to code-behind:**
```csharp
/// <summary>
/// [Control/Form] implementing responsive grid-based layout.
/// Uses TableLayoutPanel for adaptive sizing across screen resolutions.
/// </summary>
/// <remarks>
/// Layout structure:
/// - Row 1: [Description] (AutoSize/Absolute/Percent)
/// - Row 2: [Description] (AutoSize/Absolute/Percent)
/// Column sizing: [Mix of Absolute and Percent]
/// Minimum size: [Width]x[Height]
/// </remarks>
public partial class TargetControl : UserControl
{
    // ...
}
```

### Step 6: Testing Phase

**Generate test instructions:**

Create a testing checklist for manual validation:

```markdown
## Testing Checklist for [ControlName]

### Resolution Testing
- [ ] Test at 1920x1080 (1080p)
- [ ] Test at 1366x768 (laptop)
- [ ] Test at 3840x2160 (4K)

### DPI Scaling Testing
- [ ] Test at 100% scaling
- [ ] Test at 125% scaling
- [ ] Test at 150% scaling

### Resize Testing
- [ ] Resize to minimum size - usable?
- [ ] Maximize - no weird stretching?
- [ ] Intermediate sizes - smooth resize?

### Visual Testing
- [ ] Margins look professional (not cramped)
- [ ] Controls aligned properly
- [ ] No overlap at any size
- [ ] Scrollbars appear only when needed
- [ ] Focus indicators visible

### Functional Testing
- [ ] All controls accessible
- [ ] Tab order logical
- [ ] Keyboard shortcuts work
- [ ] Mouse interaction smooth
```

---

## Output Format

After completing the refactoring, provide:

1. **Summary of changes made**
2. **Before/After comparison** (high-level structure)
3. **List of files modified**
4. **Compilation status**
5. **Testing checklist** for manual validation
6. **Any warnings or notes** for the developer

---

## Example Usage

```
User: Refactor Forms/ErrorReports/Form_ViewErrorReports.Designer.cs to use responsive layout

Agent:
1. Reads winforms-responsive-layout.instructions.md
2. Reads ui-scaling-consistency.instructions.md
3. Analyzes Form_ViewErrorReports.Designer.cs
4. Identifies: Simple Dock=Fill, no TableLayoutPanel, no margins
5. Creates refactoring plan
6. Implements TableLayoutPanel structure with proper sizing
7. Adds margins/padding (10px container, 5px controls)
8. Sets MinimumSize (800x600)
9. Validates compilation
10. Generates testing checklist
11. Reports completion
```

---

## Common Refactoring Patterns

### Pattern 1: Simple Form with DataGridView

**Before:**
```csharp
dgvData.Dock = DockStyle.Fill;
```

**After:**
```csharp
var mainLayout = new TableLayoutPanel
{
    Dock = DockStyle.Fill,
    Padding = new Padding(10),
    RowCount = 2,
    RowStyles = {
        new RowStyle(SizeType.Percent, 100F),  // Grid
        new RowStyle(SizeType.Absolute, 30)     // Status
    }
};

dgvData.Dock = DockStyle.Fill;
dgvData.MinimumSize = new Size(400, 200);
dgvData.Margin = new Padding(0, 0, 0, 10);

lblStatus.Dock = DockStyle.Fill;
lblStatus.Margin = new Padding(0);

mainLayout.Controls.Add(dgvData, 0, 0);
mainLayout.Controls.Add(lblStatus, 0, 1);
this.Controls.Add(mainLayout);
```

### Pattern 2: Filter Panel + Grid

**Before:**
```csharp
filterPanel.Dock = DockStyle.Top;
filterPanel.Height = 60;
dgvData.Dock = DockStyle.Fill;
```

**After:**
```csharp
var mainLayout = new TableLayoutPanel
{
    Dock = DockStyle.Fill,
    Padding = new Padding(10),
    RowCount = 2,
    RowStyles = {
        new RowStyle(SizeType.AutoSize),       // Filter
        new RowStyle(SizeType.Percent, 100F)   // Grid
    }
};

filterPanel.AutoSize = true;
filterPanel.MinimumSize = new Size(600, 40);
filterPanel.Margin = new Padding(0, 0, 0, 10);

dgvData.Dock = DockStyle.Fill;
dgvData.MinimumSize = new Size(400, 200);
dgvData.Margin = new Padding(0);

mainLayout.Controls.Add(filterPanel, 0, 0);
mainLayout.Controls.Add(dgvData, 0, 1);
this.Controls.Add(mainLayout);
```

### Pattern 3: Master-Detail with SplitContainer

**Before:**
```csharp
gridPanel.Dock = DockStyle.Top;
gridPanel.Height = 300;
detailPanel.Dock = DockStyle.Fill;
```

**After:**
```csharp
var splitContainer = new SplitContainer
{
    Dock = DockStyle.Fill,
    Orientation = Orientation.Horizontal,
    SplitterDistance = 400,
    FixedPanel = FixedPanel.None,
    Panel1MinSize = 200,
    Panel2MinSize = 150,
    SplitterWidth = 6
};

gridPanel.Dock = DockStyle.Fill;
gridPanel.MinimumSize = new Size(400, 200);

detailPanel.Dock = DockStyle.Fill;
detailPanel.MinimumSize = new Size(400, 150);

splitContainer.Panel1.Controls.Add(gridPanel);
splitContainer.Panel2.Controls.Add(detailPanel);
this.Controls.Add(splitContainer);
```

---

## Troubleshooting Common Issues

### Issue: Controls Overlap After Refactoring

**Cause**: Wrong Z-order or missing margins
**Fix**: 
1. Add controls in correct order (fill first, then docked)
2. Add Margin = new Padding(5) to all controls

### Issue: Form Won't Resize Below Certain Size

**Cause**: Child controls have MinimumSize larger than form
**Fix**: Calculate form MinimumSize from child requirements

### Issue: Layout Jumps During Resize

**Cause**: Missing double buffering
**Fix**: Add SetStyle(ControlStyles.OptimizedDoubleBuffer, true)

### Issue: Designer Won't Open File

**Cause**: Syntax error in InitializeComponent
**Fix**: 
1. Check for missing semicolons
2. Verify all controls declared in partial class
3. Ensure proper closing braces

### Issue: Controls Don't Stretch with Form

**Cause**: Wrong Anchor settings or not using Dock
**Fix**: Use Dock=Fill for main content, Anchor for buttons/labels

---

## Success Criteria

Refactoring is complete when:

- [ ] Code compiles without errors
- [ ] Designer opens without errors
- [ ] TableLayoutPanel structure in place
- [ ] All controls have Margin set (5px typical)
- [ ] Container has Padding set (10px typical)
- [ ] MinimumSize set on DataGridView and main container
- [ ] Proper Anchor/Dock on all controls
- [ ] Double buffering enabled
- [ ] Core_Themes.ApplyDpiScaling() called
- [ ] XML documentation added
- [ ] Testing checklist generated
- [ ] Form renders correctly at multiple sizes

---

## Important Reminders

1. **Always read instruction files first** - Don't guess at patterns
2. **Test incrementally** - Build after each major change
3. **Preserve functionality** - Layout changes shouldn't break behavior
4. **Use SuspendLayout/ResumeLayout** - Better performance
5. **Comment original code** - Easy to reference or rollback
6. **Validate with designer** - Ensure Visual Studio can still edit
7. **Generate testing checklist** - Help ensure quality
8. **Complete all steps** - Don't stop at first issue
