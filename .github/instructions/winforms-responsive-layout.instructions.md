---
description: 'WinForms responsive layout architecture, TableLayoutPanel patterns, and grid-based design best practices'
applyTo: '**/*.Designer.cs,**/*.cs'
---

# WinForms Responsive Layout Architecture

Comprehensive guide for building responsive, DPI-aware WinForms applications using grid-based layout patterns. Based on Microsoft documentation, community best practices, and manufacturing application requirements.

## Layout Pattern Name

**Grid-Based Responsive Layout** (also called Table Layout, Constraint-Based Layout)

This is the WinForms implementation of universal responsive design patterns found across:
- **WPF/XAML**: Grid layout with star sizing
- **Web**: CSS Grid Layout / Flexbox
- **Mobile**: Auto Layout (iOS), ConstraintLayout (Android)

## Core Components

### TableLayoutPanel - The Foundation

`TableLayoutPanel` is the primary container for responsive layouts:

```csharp
var tableLayout = new TableLayoutPanel
{
    Dock = DockStyle.Fill,
    ColumnCount = 4,
    RowCount = 2,
    AutoSize = true,
    AutoSizeMode = AutoSizeMode.GrowAndShrink,
    Padding = new Padding(10),  // Internal spacing
    CellBorderStyle = TableLayoutPanelCellBorderStyle.None
};
```

### Column and Row Sizing Strategies

**Three sizing types available:**

1. **SizeType.Absolute** - Fixed pixel width/height
   - Use for: Labels, fixed-width buttons, icons
   - Example: `new ColumnStyle(SizeType.Absolute, 100F)`

2. **SizeType.Percent** - Percentage-based sizing
   - Use for: Equal distribution of space
   - Example: `new ColumnStyle(SizeType.Percent, 25F)`

3. **SizeType.AutoSize** - Content-dependent sizing
   - Use for: Controls that should fit their content
   - Example: `new RowStyle(SizeType.AutoSize)`

### Best Practice Sizing Patterns

#### Filter Panel Pattern (Fixed Labels + Flexible Controls)
```csharp
// RECOMMENDED: Mix absolute and percent for optimal responsiveness
tableLayoutPanel.ColumnStyles.Clear();
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100)); // Label
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F)); // Control
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100)); // Label
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F)); // Control
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100)); // Label
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F)); // Control

// Row sizing with expandable sections
tableLayoutPanel.RowStyles.Clear();
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Filter row
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Main content
```

#### Main Content Grid Pattern (Fixed/Dynamic Mix)
```csharp
// For forms with fixed header/footer and expandable content
tableLayoutPanel.RowStyles.Clear();
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));  // Header
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 12));  // Spacer
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Content (star sizing)
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 12));  // Spacer
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));  // Footer
```

## Margin and Padding - Critical for Professional UI

### The Difference

- **Margin**: Space AROUND a control (external spacing)
- **Padding**: Space INSIDE a control (internal spacing)

![Margin vs Padding](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/media/layout/margin-padding.png)

### Standard Spacing Values

```csharp
// Container padding (internal spacing from edges)
tableLayoutPanel.Padding = new Padding(10);  // Uniform 10px padding
filterPanel.Padding = new Padding(15, 10, 15, 10);  // Left, Top, Right, Bottom

// Control margins (external spacing between controls)
dgvErrorReports.Margin = new Padding(5);  // 5px on all sides
filterPanel.Margin = new Padding(5, 5, 5, 10);  // Extra bottom margin
btnApply.Margin = new Padding(0, 0, 5, 0);  // Right margin only
```

### Margin Best Practices

1. **Consistent spacing**: Use same margin values across similar controls
2. **Visual hierarchy**: Larger margins separate major sections
3. **Compact sections**: Smaller margins (3-5px) within related groups
4. **No negative margins**: These cause overlap and rendering issues

## Anchor and Dock - Control Positioning

### Dock Property

**Use when**: Control should fill entire side(s) of parent

```csharp
// Common docking patterns
dgvErrorReports.Dock = DockStyle.Fill;    // Fills entire parent
statusStrip.Dock = DockStyle.Bottom;      // Anchored to bottom
toolStrip.Dock = DockStyle.Top;           // Anchored to top
splitContainer.Dock = DockStyle.Fill;     // Fills remaining space
```

**Z-Order matters**: Controls dock in the order they're added
```csharp
// Add in reverse visual order (back to front)
this.Controls.Add(mainContent);    // Bottom layer (Fill)
this.Controls.Add(filterPanel);    // Middle layer
this.Controls.Add(statusStrip);    // Top layer (Bottom dock)
```

### Anchor Property

**Use when**: Control should maintain distance from specific edges

```csharp
// Stretch horizontally
txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;

// Pin to bottom-right (common for buttons)
btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

// Pin to top-right
btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;

// Stretch both directions (fills cell in TableLayoutPanel)
panel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
```

**Animation during resize**: Anchored controls automatically reposition/resize

## MinimumSize and MaximumSize - Usability Boundaries

### Critical for Responsive Design

```csharp
// DataGridView needs minimum size for usability
dgvErrorReports.MinimumSize = new Size(400, 200);

// Filter panel minimum prevents collapse
filterPanel.MinimumSize = new Size(600, 80);

// Form minimum size calculation
this.MinimumSize = new Size(
    filterPanel.MinimumSize.Width + 40,  // Add padding
    filterPanel.MinimumSize.Height + 300  // Grid needs space
);

// Maximum size limits expansion (useful for pop-ups)
detailPanel.MaximumSize = new Size(0, 400);  // Max height only
```

### Best Practices

- **Always set MinimumSize** on main content areas
- **Calculate form MinimumSize** from child control requirements
- **Use MaximumSize sparingly** - usually for height limits only
- **Test at various screen sizes** to verify boundaries work

## SplitContainer - Master-Detail Pattern

**Use for**: Two-panel layouts where user controls the split

```csharp
var splitContainer = new SplitContainer
{
    Dock = DockStyle.Fill,
    Orientation = Orientation.Horizontal,  // or Vertical
    SplitterDistance = 400,  // Initial split position
    FixedPanel = FixedPanel.None,  // Both panels resize
    IsSplitterFixed = false,  // User can drag splitter
    SplitterWidth = 6,  // Thicker = easier to grab
    Panel1MinSize = 200,  // Prevent collapse
    Panel2MinSize = 150
};

// Add controls to panels
splitContainer.Panel1.Controls.Add(gridControl);
splitContainer.Panel2.Controls.Add(detailControl);
```

### When to Use SplitContainer

✅ **Good for:**
- Grid + detail view (error reports, transactions)
- Navigation + content (tree view + editor)
- Dual list views (available items + selected items)
- Any master-detail relationship

❌ **Avoid for:**
- Simple stacked layouts (use TableLayoutPanel)
- Fixed-ratio splits (use TableLayoutPanel with Percent sizing)
- More than 2 panels (nest SplitContainers)

## AutoSize and AutoSizeMode

### AutoSize Property

```csharp
// Control grows/shrinks to fit content
filterPanel.AutoSize = true;
filterPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
```

### AutoSizeMode Values

- **GrowAndShrink**: Control resizes to fit content perfectly
- **GrowOnly**: Control grows but never shrinks below initial size

### Controls That Support AutoSize

| Control | AutoSize | AutoSizeMode |
|---------|----------|--------------|
| Button | ✔️ | ✔️ |
| Panel | ✔️ | ✔️ |
| TableLayoutPanel | ✔️ | ✔️ |
| Label | ✔️ | ❌ |
| TextBox | ✔️ | ❌ |
| DataGridView | ❌ | ❌ |
| ComboBox | ❌ | ❌ |

## Cell Spanning in TableLayoutPanel

**Use when**: Control needs more space than one cell

```csharp
// Make button span 2 columns
tableLayoutPanel.SetColumnSpan(btnApplyFilters, 2);

// Make panel span 3 rows
tableLayoutPanel.SetRowSpan(descriptionPanel, 3);
```

### Common Patterns

```csharp
// Full-width title row
tableLayoutPanel.SetColumnSpan(lblTitle, tableLayoutPanel.ColumnCount);

// Button row spanning multiple columns
tableLayoutPanel.SetColumnSpan(buttonPanel, 2);

// Tall side panel
tableLayoutPanel.SetRowSpan(navigationPanel, tableLayoutPanel.RowCount);
```

## AutoScroll - Content Overflow Management

```csharp
// Enable scrolling when content exceeds bounds
filterPanel.AutoScroll = true;
filterPanel.MaximumSize = new Size(0, 200);  // Limit max height

// Control scroll behavior
panel.HorizontalScroll.Enabled = false;  // No horizontal scroll
panel.VerticalScroll.Enabled = true;
panel.VerticalScroll.Visible = true;
```

## Performance Optimization

### Double Buffering

**Critical for flicker-free resizing:**

```csharp
// Enable in constructor
public Control_ErrorReportsGrid()
{
    InitializeComponent();
    SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    SetStyle(ControlStyles.UserPaint, true);
}
```

### SuspendLayout / ResumeLayout

**Use when**: Making multiple layout changes

```csharp
tableLayoutPanel.SuspendLayout();
SuspendLayout();

// Make all layout changes here
tableLayoutPanel.ColumnCount = 6;
tableLayoutPanel.ColumnStyles.Clear();
// ... add styles ...
// ... add controls ...

tableLayoutPanel.ResumeLayout(false);
ResumeLayout(false);
PerformLayout();  // Force immediate layout calculation
```

## DPI Scaling Integration

**Always call in UserControl constructor:**

```csharp
public Control_Example()
{
    InitializeComponent();
    
    // MTM-specific DPI scaling
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    // Performance
    SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    
    // Then custom initialization
    InitializeLayout();
}
```

## Common Anti-Patterns and Fixes

### ❌ Anti-Pattern 1: All Percent Sizing

```csharp
// BAD: Everything is percentage-based
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
```

✅ **Fix: Mix Absolute and Percent**

```csharp
// GOOD: Labels fixed, controls flexible
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
```

### ❌ Anti-Pattern 2: Missing Min/Max Sizes

```csharp
// BAD: No size constraints
var dgv = new DataGridView { Dock = DockStyle.Fill };
```

✅ **Fix: Add Minimum Size**

```csharp
// GOOD: Usable at any screen size
var dgv = new DataGridView 
{ 
    Dock = DockStyle.Fill,
    MinimumSize = new Size(400, 200)
};
```

### ❌ Anti-Pattern 3: No Margins or Padding

```csharp
// BAD: Controls touch edges
tableLayoutPanel.Padding = new Padding(0);
button.Margin = new Padding(0);
```

✅ **Fix: Add Professional Spacing**

```csharp
// GOOD: Breathing room around controls
tableLayoutPanel.Padding = new Padding(10);
button.Margin = new Padding(5);
```

### ❌ Anti-Pattern 4: Wrong Z-Order for Docking

```csharp
// BAD: Adding docked controls in wrong order
this.Controls.Add(statusStrip);  // Docks to bottom
this.Controls.Add(mainContent);  // Tries to fill, but statusStrip took space
```

✅ **Fix: Add in Reverse Visual Order**

```csharp
// GOOD: Fill control first, then docked controls
this.Controls.Add(mainContent);   // Fill
this.Controls.Add(statusStrip);   // Bottom
```

### ❌ Anti-Pattern 5: Fixed MaxHeight on Expandable Controls

```csharp
// BAD: Prevents responsive expansion
textBox.MaxHeight = 100;
textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
```

✅ **Fix: Remove MaxHeight, Use Container Constraints**

```csharp
// GOOD: Let container determine size
textBox.MinHeight = 60;
textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
```

## Complete Example: Error Reports Grid

```csharp
public partial class Control_ErrorReportsGrid : UserControl
{
    public Control_ErrorReportsGrid()
    {
        InitializeComponent();
        
        // Performance and DPI
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        ConfigureResponsiveLayout();
    }
    
    private void ConfigureResponsiveLayout()
    {
        SuspendLayout();
        
        // Main layout container
        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10),
            AutoSize = false
        };
        
        // Row sizing: Filter | Content | Status
        mainLayout.RowStyles.Clear();
        mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));    // Filter (auto-height)
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid (fills)
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));  // Status

        // Filter panel
        var filterPanel = CreateFilterPanel();
        filterPanel.Margin = new Padding(0, 0, 0, 10);
        filterPanel.MinimumSize = new Size(600, 40);
        
        // DataGridView
        var dgv = new DataGridView
        {
            Dock = DockStyle.Fill,
            MinimumSize = new Size(400, 200),
            Margin = new Padding(0)
        };
        
        // Status label
        var statusLabel = new Label
        {
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Margin = new Padding(0, 10, 0, 0)
        };
        
        mainLayout.Controls.Add(filterPanel, 0, 0);
        mainLayout.Controls.Add(dgv, 0, 1);
        mainLayout.Controls.Add(statusLabel, 0, 2);
        
        this.Controls.Add(mainLayout);
        
        ResumeLayout(false);
        PerformLayout();
    }
    
    private Panel CreateFilterPanel()
    {
        var panel = new Panel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        
        var tableLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 6,
            RowCount = 1,
            AutoSize = true,
            Padding = new Padding(5)
        };
        
        // Fixed label width, flexible controls
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        
        panel.Controls.Add(tableLayout);
        return panel;
    }
}
```

## Testing Responsive Layouts

### Manual Testing Checklist

- [ ] Test at 1920x1080 (1080p - most common)
- [ ] Test at 1366x768 (laptop standard)
- [ ] Test at 3840x2160 (4K)
- [ ] Test at 125% DPI scaling
- [ ] Test at 150% DPI scaling
- [ ] Resize form to minimum size - still usable?
- [ ] Maximize form - no weird stretching?
- [ ] Check all controls visible without scrolling
- [ ] Verify margins and padding look professional
- [ ] Ensure buttons don't overlap at small sizes

### Common Issues to Watch For

1. **Controls disappearing**: Check Z-order and Dock settings
2. **Unexpected scrollbars**: Check MinimumSize constraints
3. **Controls not resizing**: Check Anchor settings
4. **Layout jumps during resize**: Enable DoubleBuffering
5. **DPI issues**: Ensure Core_Themes calls are present

## Summary: Responsive Layout Checklist

When creating or refactoring a UserControl/Form:

- [ ] Use TableLayoutPanel for main layout structure
- [ ] Mix Absolute + Percent column/row styles
- [ ] Add Padding (10px) to containers
- [ ] Add Margins (5px) between controls
- [ ] Set MinimumSize on grids and main controls
- [ ] Use Anchor for control positioning
- [ ] Use Dock for fill/side positioning
- [ ] Enable DoubleBuffering for performance
- [ ] Call Core_Themes.ApplyDpiScaling()
- [ ] Test at multiple screen sizes and DPI scales
- [ ] Add in correct Z-order (fill first, then docked)
- [ ] Use SplitContainer for master-detail layouts
- [ ] Implement AutoScroll for overflow scenarios
- [ ] Calculate Form.MinimumSize from child controls

## References

- [Microsoft: Position and layout of controls](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/layout)
- [Microsoft: TableLayoutPanel Control](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/tablelayoutpanel-control-windows-forms)
- [Microsoft: Best Practices for TableLayoutPanel](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/best-practices-for-the-tablelayoutpanel-control)
- [GitHub: WinForms Issues (Layout)](https://github.com/dotnet/winforms/issues?q=is%3Aissue+label%3Aarea-Layout)
