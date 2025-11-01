# MTM WIP Application - Theme System Reference

**Last Updated**: 2025-11-01  
**Version**: 1.0  
**Status**: Comprehensive Reference

---

## Table of Contents

1. [Overview](#overview)
2. [Core Components](#core-components)
3. [Public API Reference](#public-api-reference)
4. [Color Token Catalog](#color-token-catalog)
5. [Required Constructor Pattern](#required-constructor-pattern)
6. [DPI Scaling System](#dpi-scaling-system)
7. [Runtime Layout Adjustments](#runtime-layout-adjustments)
8. [Theme Application Workflow](#theme-application-workflow)
9. [Control-Specific Theme Appliers](#control-specific-theme-appliers)
10. [Database Theme Management](#database-theme-management)
11. [Migration Guide](#migration-guide)
12. [Code Examples](#code-examples)
13. [Troubleshooting](#troubleshooting)

---

## Overview

The MTM WIP Application theme system provides comprehensive DPI scaling, UI responsiveness, and consistent visual theming across all forms and controls. The system ensures pixel-perfect rendering at all DPI settings (100%, 125%, 150%, 200%) and supports dynamic theme switching for user preferences.

### Key Features

- **Automatic DPI Scaling**: Works at 96, 120, 144, and 192 DPI settings
- **Runtime Layout Adjustments**: Fixes designer vs runtime differences
- **Dynamic Theme Application**: Apply themes without restarting
- **Database-Backed Themes**: 9 themes stored in MySQL `app_themes` table
- **Multi-Monitor Support**: Handles DPI changes when moving windows
- **Comprehensive Control Coverage**: 40+ control types supported
- **Focus Highlighting**: Visual feedback for keyboard navigation
- **DataGridView Enhancements**: Context menus, column visibility, grid settings

### Architecture

```
┌─────────────────────────────────────────┐
│         Core_Themes (Static)            │
│  - ApplyTheme()                         │
│  - ApplyDpiScaling()                    │
│  - ApplyRuntimeLayoutAdjustments()      │
│  - HandleDpiChanged()                   │
└─────────────────────────────────────────┘
                    │
                    ├──────────────────┬───────────────────┐
                    ▼                  ▼                   ▼
        ┌───────────────────┐ ┌──────────────┐  ┌─────────────────┐
        │ Model_UserUiColors │ │ Core_AppThemes│  │ FocusUtils      │
        │  (Color Tokens)    │ │  (Theme Mgmt) │  │ (Focus Handling)│
        └───────────────────┘ └──────────────┘  └─────────────────┘
                    │                  │
                    │                  ▼
                    │         ┌─────────────────┐
                    │         │  MySQL Database │
                    │         │   app_themes    │
                    │         │  (9 themes,     │
                    │         │   203 props ea.)│
                    │         └─────────────────┘
                    ▼
        ┌──────────────────────────────┐
        │   ThemeAppliers (Internal)    │
        │  - Per-control theme logic    │
        │  - Owner-draw theme helper    │
        └──────────────────────────────┘
```

---

## Core Components

### 1. Core_Themes.cs

**Location**: `Core/Core_Themes.cs` (3087 lines)  
**Purpose**: Central theming system with DPI scaling and layout management

**Responsibilities**:
- Apply themes to forms and controls
- Handle DPI scaling (96-192 DPI)
- Runtime layout adjustments
- Dynamic DPI change detection
- Focus highlighting
- DataGridView enhancements

### 2. Model_UserUiColors.cs

**Location**: `Models/Model_UserUiColors.cs`  
**Purpose**: Color token catalog with nullable Color properties

**Contains**:
- 200+ color properties organized by control type
- Default dark theme values (FromArgb)
- Nullable Color? types for optional overrides
- Semantic theme colors (Accent, Error, Warning, Success)

### 3. Core_AppThemes

**Location**: Referenced in `Core_Themes.cs`  
**Purpose**: Theme storage and management

**Provides**:
- `GetCurrentTheme()` - Active theme
- `GetTheme(string name)` - Named theme
- `LoadThemesFromDatabaseAsync()` - Load user themes

### 4. Database Storage (app_themes Table)

**Location**: MySQL 5.7 database on MAMP  
**Database**: `MTM_WIP_Application_Winforms`  
**Table**: `app_themes`

**Schema**:
```sql
CREATE TABLE app_themes (
    ThemeName    VARCHAR(100) NOT NULL PRIMARY KEY,
    SettingsJson JSON         NOT NULL
);
```

**Table Structure**:
| Field | Type | Null | Key | Description |
|-------|------|------|-----|-------------|
| ThemeName | VARCHAR(100) | NO | PRI | Unique theme identifier |
| SettingsJson | JSON | NO | | Complete theme configuration (200+ color properties) |

**Available Themes** (9 total):
1. **Arctic** - Cool blue/white theme
2. **Default** - Standard dark theme (default)
3. **Fire Storm** - Red/orange warm theme
4. **Forest** - Green nature theme
5. **Lavender** - Purple soft theme
6. **Midnight** - Deep dark blue theme
7. **Ocean** - Blue/teal aquatic theme
8. **Sunset** - Orange/pink warm theme
9. **Urban Bloom** - Modern mixed theme

**JSON Structure** (203 properties per theme):
- All 200+ color tokens from `Model_UserUiColors`
- Stored as JSON object with flat structure
- Example keys: `FormBackColor`, `ButtonBackColor`, `DataGridBackColor`, etc.
- Each property stores ARGB color value

**Example Query**:
```sql
-- Get a specific theme
SELECT ThemeName, SettingsJson 
FROM app_themes 
WHERE ThemeName = 'Default';

-- List all available themes
SELECT ThemeName FROM app_themes ORDER BY ThemeName;

-- Count total themes
SELECT COUNT(*) as TotalThemes FROM app_themes;
```

**Loading Themes**:
```csharp
// Load themes from database
await Core_AppThemes.LoadThemesFromDatabaseAsync();

// Get user's theme
var colors = await Core_Themes.GetUserThemeColorsAsync(userId);

// Apply theme to form
Core_Themes.ApplyTheme(this);
```

**Database Connection**:
- **Host**: localhost (or 172.16.1.104 for production)
- **Port**: 3306
- **Database**: MTM_WIP_Application_Winforms
- **User**: root
- **Connection managed by**: `Helper_Database_Variables.GetConnectionString()`

---

## Public API Reference

### ApplyTheme

**{ApplyTheme} Start**

```csharp
public static void ApplyTheme(Form form)
```

**Purpose**: Applies complete theme (colors, DPI, layout) to a form and all its controls.

**Parameters**:
- `form` - The form to theme

**Process**:
1. Suspends layout
2. Applies DPI scaling
3. Applies runtime layout adjustments
4. Sets form theme colors
5. Recursively themes all controls
6. Resumes layout

**When to Call**:
- ✅ Form constructor (after InitializeComponent)
- ✅ Settings dialog (user changes theme)
- ✅ DPI change event handler
- ❌ Business logic or arbitrary event handlers

**Example**:
```csharp
public MainForm()
{
    InitializeComponent();
    Core_Themes.ApplyTheme(this);  // Complete theme application
}
```

**{ApplyTheme} End**

---

### ApplyDpiScaling

**{ApplyDpiScaling} Start**

```csharp
public static void ApplyDpiScaling(Form form)
public static void ApplyDpiScaling(UserControl userControl)
```

**Purpose**: Applies DPI-aware scaling to ensure pixel-perfect rendering at all DPI settings (100%, 125%, 150%, 200%).

**Parameters**:
- `form` or `userControl` - The container to scale

**How It Works**:
1. Suspends layout
2. Recursively traverses control hierarchy
3. Sets AutoScaleMode.Dpi where needed
4. Applies DPI-specific adjustments
5. Resumes layout

**When to Call**:
- ✅ **REQUIRED** in all Form/UserControl constructors
- ✅ After InitializeComponent()
- ✅ DPI change event handlers
- ❌ Runtime (after initial load)

**Example**:
```csharp
public Control_InventoryTab()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);  // REQUIRED
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);  // REQUIRED
}
```

**{ApplyDpiScaling} End**

---

### ApplyRuntimeLayoutAdjustments

**{ApplyRuntimeLayoutAdjustments} Start**

```csharp
public static void ApplyRuntimeLayoutAdjustments(Form form)
public static void ApplyRuntimeLayoutAdjustments(UserControl userControl)
```

**Purpose**: Fixes layout discrepancies between designer and runtime. Handles margins, padding, and SplitContainer configurations.

**Parameters**:
- `form` or `userControl` - The container to adjust

**Adjustments Applied**:
- **TableLayoutPanel**: Margin=0, Padding=1
- **GroupBox**: Margin=1, Padding=3
- **Panel**: Margin=0
- **SplitContainer**: Splitter distance calculations, panel margins
- **Button**: Margin=1
- **Label**: Margin=0

**When to Call**:
- ✅ **REQUIRED** in all Form/UserControl constructors
- ✅ After ApplyDpiScaling()
- ✅ DPI change event handlers
- ❌ Runtime (after initial load)

**Example**:
```csharp
public Control_RemoveTab()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);  // Fixes layout issues
}
```

**{ApplyRuntimeLayoutAdjustments} End**

---

### ApplyThemeToDataGridView

**{ApplyThemeToDataGridView} Start**

```csharp
public static void ApplyThemeToDataGridView(DataGridView dataGridView)
```

**Purpose**: Applies theme colors to DataGridView and attaches column visibility context menu.

**Parameters**:
- `dataGridView` - The grid to theme

**Features**:
- Applies background, foreground, header, row, alternating row colors
- Applies selection colors
- Applies grid line and border colors
- Attaches right-click context menu for column visibility
- Adds "Change Column Order" dialog
- Adds "Save Grid Settings" to user preferences

**When to Call**:
- ✅ After DataGridView creation
- ✅ When theme changes
- ❌ Every paint event (only once per grid)

**Example**:
```csharp
Core_Themes.ApplyThemeToDataGridView(dataGridView);
```

**{ApplyThemeToDataGridView} End**

---

### HandleDpiChanged

**{HandleDpiChanged} Start**

```csharp
public static void HandleDpiChanged(Form form, int oldDpi, int newDpi)
```

**Purpose**: Handles DPI changes when moving windows between monitors with different DPI settings.

**Parameters**:
- `form` - The form experiencing DPI change
- `oldDpi` - Previous DPI (e.g., 96)
- `newDpi` - New DPI (e.g., 144)

**Process**:
1. Calculates scale factor
2. Reapplies DPI scaling
3. Reapplies runtime layout adjustments
4. Recursively handles child UserControls
5. Logs DPI change

**When to Call**:
- ✅ Form.DpiChanged event handler
- ✅ Monitor switch detection
- ❌ Manually (use event-driven approach)

**Example**:
```csharp
protected override void OnDpiChanged(DpiChangedEventArgs e)
{
    base.OnDpiChanged(e);
    Core_Themes.HandleDpiChanged(this, e.DeviceDpiOld, e.DeviceDpiNew);
}
```

**{HandleDpiChanged} End**

---

### GetUserThemeColorsAsync

**{GetUserThemeColorsAsync} Start**

```csharp
public static async Task<Model_UserUiColors> GetUserThemeColorsAsync(string userId)
```

**Purpose**: Retrieves user-specific theme colors from database.

**Parameters**:
- `userId` - The user ID

**Returns**: `Model_UserUiColors` with user's theme preferences

**Process**:
1. Fetches theme name from database
2. Loads theme if not cached
3. Returns color token model

**When to Call**:
- ✅ User login
- ✅ Settings change
- ❌ Every UI update (cache results)

**Example**:
```csharp
var colors = await Core_Themes.GetUserThemeColorsAsync(Model_AppVariables.User);
Model_AppVariables.UserUiColors = colors;
```

**{GetUserThemeColorsAsync} End**

---

### EnsureParentChainAccommodates

**{EnsureParentChainAccommodates} Start**

```csharp
public static void EnsureParentChainAccommodates(Control control)
```

**Purpose**: Ensures entire parent chain resizes to fully accommodate a control after DPI scaling or runtime loading.

**Parameters**:
- `control` - The control to start from (typically a UserControl)

**Process**:
1. Gets current DPI scale
2. Calculates required size recursively
3. Walks up parent chain
4. Resizes parents if needed
5. Updates MaximumSize if necessary

**When to Call**:
- ✅ After loading UserControl dynamically
- ✅ After DPI change
- ❌ During initial form load (not needed)

**Example**:
```csharp
var userControl = new Control_TransactionViewer();
panel.Controls.Add(userControl);
Core_Themes.EnsureParentChainAccommodates(userControl);
```

**{EnsureParentChainAccommodates} End**

---

## Color Token Catalog

### Theme Colors (Semantic)

```csharp
AccentColor             = Color.FromArgb(0, 122, 204)     // Primary brand blue
SecondaryAccentColor    = Color.FromArgb(102, 204, 255)   // Light blue
ErrorColor              = Color.FromArgb(229, 115, 115)   // Red
WarningColor            = Color.FromArgb(255, 167, 38)    // Orange
SuccessColor            = Color.FromArgb(102, 187, 106)   // Green
InfoColor               = Color.FromArgb(51, 153, 255)    // Info blue
```

### Form Colors

```csharp
FormBackColor           = Color.FromArgb(30, 30, 30)      // Dark background
FormForeColor           = Color.FromArgb(255, 255, 255)   // White text
FormBorderColor         = (nullable)
```

### Control Colors

```csharp
ControlBackColor        = Color.FromArgb(30, 30, 30)
ControlForeColor        = Color.FromArgb(255, 255, 255)
ControlFocusedBackColor = Color.FromArgb(0, 122, 204)     // Blue focus
```

### Button Colors

```csharp
ButtonBackColor         = Color.FromArgb(60, 60, 60)
ButtonForeColor         = Color.FromArgb(255, 255, 255)
ButtonBorderColor       = Color.FromArgb(68, 68, 68)
ButtonHoverBackColor    = Color.FromArgb(0, 122, 204)
ButtonHoverForeColor    = Color.FromArgb(255, 255, 255)
ButtonPressedBackColor  = Color.FromArgb(0, 90, 158)
ButtonPressedForeColor  = Color.FromArgb(255, 255, 255)
```

### TextBox Colors

```csharp
TextBoxBackColor         = Color.FromArgb(30, 30, 30)
TextBoxForeColor         = Color.FromArgb(255, 255, 255)
TextBoxSelectionBackColor = Color.FromArgb(0, 122, 204)
TextBoxSelectionForeColor = Color.FromArgb(255, 255, 255)
TextBoxErrorForeColor    = Color.FromArgb(229, 115, 115)  // Red for errors
TextBoxBorderColor       = Color.FromArgb(60, 60, 60)
```

### ComboBox Colors

```csharp
ComboBoxBackColor         = Color.FromArgb(45, 45, 48)
ComboBoxForeColor         = Color.FromArgb(255, 255, 255)
ComboBoxErrorForeColor    = Color.FromArgb(229, 115, 115)
ComboBoxBorderColor       = Color.FromArgb(60, 60, 60)
ComboBoxDropDownBackColor = Color.FromArgb(45, 45, 48)
```

### DataGrid Colors

```csharp
DataGridBackColor           = Color.FromArgb(30, 30, 30)
DataGridForeColor           = Color.FromArgb(255, 255, 255)
DataGridSelectionBackColor  = Color.FromArgb(0, 122, 204)
DataGridSelectionForeColor  = Color.FromArgb(255, 255, 255)
DataGridRowBackColor        = Color.FromArgb(30, 30, 30)
DataGridAltRowBackColor     = Color.FromArgb(42, 45, 46)
DataGridHeaderBackColor     = Color.FromArgb(37, 37, 38)
DataGridHeaderForeColor     = Color.FromArgb(255, 255, 255)
DataGridGridColor           = Color.FromArgb(60, 60, 60)
DataGridBorderColor         = Color.FromArgb(60, 60, 60)
```

### Container Colors

```csharp
GroupBoxBackColor       = Color.FromArgb(37, 37, 38)
GroupBoxForeColor       = Color.FromArgb(255, 255, 255)
PanelBackColor          = Color.FromArgb(37, 37, 38)
TableLayoutPanelBackColor = Color.FromArgb(37, 37, 38)
SplitContainerBackColor = Color.FromArgb(30, 30, 30)
```

**Total Color Tokens**: 200+ properties covering all WinForms control types

---

## Required Constructor Pattern

### MANDATORY Pattern for All Forms and UserControls

```csharp
public partial class YourFormOrControl : Form  // or UserControl
{
    public YourFormOrControl()
    {
        InitializeComponent();
        
        // ✅ REQUIRED: Apply DPI scaling (FIRST)
        Core_Themes.ApplyDpiScaling(this);
        
        // ✅ REQUIRED: Apply runtime layout adjustments (SECOND)
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        // Then your custom initialization...
        WireUpEvents();
        ApplyPrivileges();
    }
}
```

### Pattern Used in P0 Files

**Control_InventoryTab.cs** (Line 52-79):
```csharp
public Control_InventoryTab()
{
    Service_DebugTracer.TraceMethodEntry(/* ... */);
    
    InitializeComponent();
    
    // THEME POLICY: Only update theme on startup, in settings menu, or on DPI change.
    // Do NOT call theme update methods from arbitrary event handlers or business logic.
    Core_Themes.ApplyDpiScaling(this); // Allowed: UserControl initialization
    Core_Themes.ApplyRuntimeLayoutAdjustments(this); // Allowed: UserControl initialization
    
    // Additional initialization...
}
```

**Control_TransferTab.cs** (Line 55-76):
```csharp
public Control_TransferTab()
{
    Service_DebugTracer.TraceMethodEntry(/* ... */);
    
    InitializeComponent();
    
    // Apply comprehensive DPI scaling and runtime layout adjustments
    // THEME POLICY: Only update theme on startup, in settings menu, or on DPI change.
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    Control_TransferTab_Initialize();
    ApplyPrivileges();
}
```

**Control_RemoveTab.cs** (Line 59-82):
```csharp
public Control_RemoveTab()
{
    Service_DebugTracer.TraceMethodEntry(/* ... */);
    
    InitializeComponent();
    
    // Apply comprehensive DPI scaling and runtime layout adjustments
    // THEME POLICY: Only update theme on startup, in settings menu, or on DPI change.
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    // Additional initialization...
}
```

### Theme Policy

**ONLY call theme methods in these locations:**
1. ✅ Form/UserControl constructors (after InitializeComponent)
2. ✅ Settings menu logic (after user changes theme)
3. ✅ DPI change event handlers (Form.DpiChanged)

**NEVER call theme methods:**
- ❌ Arbitrary event handlers (button clicks, etc.)
- ❌ Business logic
- ❌ Data processing
- ❌ Every UI update

---

## DPI Scaling System

### Supported DPI Settings

| DPI | Scale | Resolution Example | Font 9pt | Button 75x32 |
|-----|-------|-------------------|----------|--------------|
| 96  | 100%  | 1920x1080         | 9pt      | 75x32px      |
| 120 | 125%  | 1920x1080         | 11.25pt  | ~94x40px     |
| 144 | 150%  | 3840x2160         | 13.5pt   | ~113x48px    |
| 192 | 200%  | 3840x2160         | 18pt     | 150x64px     |

### How DPI Scaling Works

1. **AutoScaleMode.Dpi** set on all forms/controls
2. **Recursive hierarchy traversal** applies scaling to all children
3. **Control-specific adjustments** handle margins, padding, splitters
4. **Font scaling** happens automatically via AutoScaleMode
5. **Layout recalculations** ensure proper positioning

### DPI Change Detection

```csharp
// In Form
protected override void OnDpiChanged(DpiChangedEventArgs e)
{
    base.OnDpiChanged(e);
    Core_Themes.HandleDpiChanged(this, e.DeviceDpiOld, e.DeviceDpiNew);
}
```

---

## Runtime Layout Adjustments

### Why Needed

Designer files generate code that sometimes conflicts with runtime requirements:
- Fixed margins that should be minimal
- Padding that causes overflow
- SplitContainer distances that don't scale
- Panel positioning that breaks responsiveness

### What Gets Adjusted

#### TableLayoutPanel
```csharp
// Before (Designer)
tableLayoutPanel.Margin = new Padding(3);
tableLayoutPanel.Padding = new Padding(3);

// After (Runtime)
tableLayoutPanel.Margin = new Padding(0);  // Flush with parent
tableLayoutPanel.Padding = new Padding(1);  // Minimal internal padding
```

#### GroupBox
```csharp
// Runtime adjustment
groupBox.Margin = new Padding(1);
groupBox.Padding = new Padding(3);  // Slightly larger for groupbox appearance
```

#### SplitContainer
```csharp
// Special cases handled:
// - Control_TransferTab_SplitContainer_Main: 35% left, 65% right
// - SettingsForm_SplitContainer_Main: 15% left, 85% right
// - MainForm_SplitContainer_Middle: 80% left, 20% right
// - Others: 50/50 split

if (splitContainer.Name == "Control_TransferTab_SplitContainer_Main")
{
    targetDistance = (int)(splitContainer.Width * 0.35);
}
```

---

## Theme Application Workflow

### Complete Theme Application (ApplyTheme)

```
ApplyTheme(form)
    │
    ├─► SuspendLayout()
    │
    ├─► ApplyDpiScaling(form)
    │    └─► Recursive control hierarchy traversal
    │
    ├─► ApplyRuntimeLayoutAdjustments(form)
    │    └─► Control-specific adjustments
    │
    ├─► SetFormTheme(form, theme, themeName)
    │    ├─► Set form BackColor, ForeColor, Font
    │    └─► Update form title with theme name
    │
    ├─► ApplyThemeToControls(form.Controls)
    │    ├─► Attach EnabledChanged handlers
    │    ├─► Apply control-specific themes
    │    ├─► Apply focus event handling
    │    └─► Recurse into child controls
    │
    ├─► ResumeLayout()
    │
    └─► Log completion
```

### Partial Theme Application (DPI Only)

```
ApplyDpiScaling(control)
    │
    ├─► SuspendLayout()
    │
    ├─► ApplyDpiScalingToControlHierarchy(controls)
    │    └─► Recursive traversal (DPI-specific logic)
    │
    ├─► ResumeLayout()
    │
    └─► Log completion
```

---

## Control-Specific Theme Appliers

### How Theme Application Works

Core_Themes maintains a `ConcurrentDictionary<Type, ControlThemeApplier>` mapping control types to theme applier functions:

```csharp
private static readonly ConcurrentDictionary<Type, ControlThemeApplier> ThemeAppliers = new()
{
    [typeof(Button)] = ThemeAppliersInternal.ApplyButtonTheme,
    [typeof(TextBox)] = ThemeAppliersInternal.ApplyTextBoxTheme,
    [typeof(DataGridView)] = Core_Themes.ApplyThemeToDataGridView,
    // 40+ more control types...
};
```

### Supported Control Types (40+)

- **Input Controls**: Button, TextBox, MaskedTextBox, RichTextBox, ComboBox, NumericUpDown, DateTimePicker
- **Selection Controls**: ListBox, CheckedListBox, RadioButton, CheckBox, TreeView, ListView
- **Containers**: GroupBox, Panel, SplitContainer, FlowLayoutPanel, TableLayoutPanel, TabControl, TabPage
- **Menus**: MenuStrip, StatusStrip, ToolStrip, ContextMenuStrip
- **Advanced**: DataGridView, PropertyGrid, WebBrowser, PictureBox, ProgressBar, TrackBar
- **Custom**: Control_QuickButtons, Control_AdvancedInventory, Control_ConnectionStrengthControl
- **Tab Controls**: Control_InventoryTab, Control_TransferTab, Control_RemoveTab

### Example: Button Theme Applier

```csharp
public static void ApplyButtonTheme(Control control, Model_UserUiColors colors)
{
    if (control is Button btn)
    {
        // Skip if inside Control_QuickButtons (special handling)
        if (btn.Parent is Control_QuickButtons) return;
        
        // Set colors
        btn.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
        btn.ForeColor = colors.ButtonForeColor ?? SystemColors.ControlText;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 2;
        btn.FlatAppearance.BorderColor = colors.ButtonBorderColor ?? SystemColors.ControlDark;
        
        // Attach hover/press event handlers
        btn.MouseEnter += (s, e) => { /* Hover colors */ };
        btn.MouseLeave += (s, e) => { /* Normal colors */ };
        btn.MouseDown += (s, e) => { /* Pressed colors */ };
        btn.MouseUp += (s, e) => { /* Normal colors */ };
        
        // Auto-shrink text for long labels
        btn.Paint += AutoShrinkText_Paint;
    }
}
```

---

## Migration Guide

### Step 1: Update Designer.cs (AutoScaleMode)

**Find in Designer.cs:**
```csharp
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
```

**Replace with:**
```csharp
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
```

### Step 2: Add Theme Calls to Constructor

**Before:**
```csharp
public MyForm()
{
    InitializeComponent();
    // Custom initialization...
}
```

**After:**
```csharp
public MyForm()
{
    InitializeComponent();
    
    // ✅ REQUIRED: Theme system integration
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    // Custom initialization...
}
```

### Step 3: Remove Hardcoded Colors

**Before:**
```csharp
button1.BackColor = Color.Blue;
textBox1.ForeColor = Color.White;
```

**After:**
```csharp
// Colors applied automatically by Core_Themes.ApplyTheme()
// Or explicitly:
var colors = Core_AppThemes.GetCurrentTheme().Colors;
button1.BackColor = colors.ButtonBackColor ?? SystemColors.Control;
textBox1.ForeColor = colors.TextBoxForeColor ?? SystemColors.WindowText;
```

### Step 4: Fix Absolute Positioning

**Before:**
```csharp
button1.Location = new Point(450, 380);
button1.Size = new Size(75, 32);
```

**After:**
```csharp
// Use anchoring for responsive layout
button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
button1.MinimumSize = new Size(75, 32);
```

### Step 5: Convert DataGridView Columns

**Before:**
```csharp
dataGridView.Columns["Part"].Width = 150;  // Fixed width
```

**After:**
```csharp
dataGridView.Columns["Part"].FillWeight = 30;  // Percentage-based
dataGridView.Columns["Part"].MinimumWidth = 100;
Core_Themes.ApplyThemeToDataGridView(dataGridView);
```

---

## Code Examples

### Example 1: Complete Form Setup

```csharp
public partial class InventoryForm : Form
{
    public InventoryForm()
    {
        InitializeComponent();
        
        // ✅ Required theme calls
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        // Setup responsive layout
        SetupDataGrid();
        WireUpEvents();
    }
    
    private void SetupDataGrid()
    {
        // Apply theme to DataGridView
        Core_Themes.ApplyThemeToDataGridView(dataGridViewInventory);
        
        // Configure columns with percentage-based sizing
        dataGridViewInventory.Columns["PartID"].FillWeight = 20;
        dataGridViewInventory.Columns["Description"].FillWeight = 40;
        dataGridViewInventory.Columns["Quantity"].FillWeight = 15;
        dataGridViewInventory.Columns["Location"].FillWeight = 25;
    }
    
    protected override void OnDpiChanged(DpiChangedEventArgs e)
    {
        base.OnDpiChanged(e);
        Core_Themes.HandleDpiChanged(this, e.DeviceDpiOld, e.DeviceDpiNew);
    }
}
```

### Example 2: UserControl with Custom Theme Logic

```csharp
public partial class Control_CustomTab : UserControl
{
    public Control_CustomTab()
    {
        InitializeComponent();
        
        // ✅ Required theme calls
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        // Apply custom color overrides
        ApplyCustomColors();
    }
    
    private void ApplyCustomColors()
    {
        var colors = Core_AppThemes.GetCurrentTheme().Colors;
        
        // Use theme colors with fallbacks
        panel1.BackColor = colors.PanelBackColor ?? SystemColors.Control;
        label1.ForeColor = colors.LabelForeColor ?? SystemColors.ControlText;
        
        // Custom accent color for branding
        // ACCEPTABLE: Brand color for specific control (documented exception)
        panelLogo.BackColor = Color.FromArgb(0, 122, 204);  // Brand blue
    }
}
```

### Example 3: Dynamic UserControl Loading

```csharp
private void LoadCustomControl()
{
    var customControl = new Control_CustomTab();
    
    // Add to parent
    panelContent.Controls.Clear();
    panelContent.Controls.Add(customControl);
    customControl.Dock = DockStyle.Fill;
    
    // Ensure parent chain accommodates control size after DPI scaling
    Core_Themes.EnsureParentChainAccommodates(customControl);
}
```

### Example 4: Theme Change in Settings

```csharp
private async void btnApplyTheme_Click(object sender, EventArgs e)
{
    string selectedTheme = comboBoxThemes.SelectedItem.ToString();
    
    // Save theme preference
    await Dao_User.SetThemeNameAsync(Model_AppVariables.User, selectedTheme);
    
    // Reload theme
    await Core_AppThemes.LoadThemesFromDatabaseAsync();
    
    // Apply to all open forms
    // ALLOWED: Settings menu logic
    foreach (Form form in Application.OpenForms)
    {
        Core_Themes.ApplyTheme(form);
    }
    
    MessageBox.Show("Theme applied successfully!", "Settings", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
}
```

---

## Troubleshooting

### Issue: Controls Too Small on High DPI

**Symptom**: Buttons, textboxes, labels appear tiny on 4K displays

**Cause**: Missing `Core_Themes.ApplyDpiScaling(this)` in constructor

**Fix**:
```csharp
public YourForm()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);  // Add this
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

### Issue: Layout Broken After Scaling

**Symptom**: Controls overlap, misaligned, or extend beyond boundaries

**Cause**: Absolute positioning with fixed coordinates instead of layout containers

**Fix**: Use TableLayoutPanel or proper Anchor/Dock properties
```csharp
// ❌ BAD
button1.Location = new Point(450, 380);

// ✅ GOOD
button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
```

### Issue: Colors Not Applied

**Symptom**: Controls show default system colors instead of theme colors

**Cause**: Theme not applied or applied before InitializeComponent

**Fix**:
```csharp
public YourForm()
{
    InitializeComponent();  // FIRST
    Core_Themes.ApplyTheme(this);  // THEN theme
}
```

### Issue: DataGridView Columns Don't Resize

**Symptom**: DataGridView columns stay fixed width

**Cause**: Fixed pixel widths instead of FillWeight

**Fix**:
```csharp
// ❌ BAD
dataGridView.Columns["Part"].Width = 150;

// ✅ GOOD
dataGridView.Columns["Part"].FillWeight = 30;
dataGridView.Columns["Part"].MinimumWidth = 100;
```

### Issue: Text Clipped in Labels

**Symptom**: Long text cut off with "..."

**Cause**: Fixed width with AutoEllipsis

**Fix**:
```csharp
// Option 1: Auto-size
label.AutoSize = true;

// Option 2: Increase width
label.MinimumSize = new Size(200, 0);
```

### Issue: Theme Changes Don't Apply

**Symptom**: Changing theme in settings has no effect

**Cause**: Theme policy violation - calling theme methods in wrong location

**Fix**: Only call from allowed locations:
```csharp
// ✅ ALLOWED in settings
private async void ApplyThemeButton_Click(object sender, EventArgs e)
{
    await SaveThemePreference();
    Core_Themes.ApplyTheme(this);  // OK here
}

// ❌ NOT ALLOWED in business logic
private void ProcessData()
{
    Core_Themes.ApplyTheme(this);  // WRONG! Don't do this
}
```

---

## Database Theme Management

### Querying Themes via MySQL CLI

**Using MAMP MySQL**:
```powershell
# Access MySQL (Windows PowerShell)
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot MTM_WIP_Application_Winforms

# List all themes
mysql> SELECT ThemeName FROM app_themes ORDER BY ThemeName;

# View theme JSON structure
mysql> SELECT ThemeName, JSON_KEYS(SettingsJson) FROM app_themes WHERE ThemeName = 'Default';

# Export theme for backup
mysql> SELECT * FROM app_themes WHERE ThemeName = 'Custom' INTO OUTFILE '/tmp/custom_theme.json';
```

### Adding a New Theme

**Method 1: Database Insert**
```sql
INSERT INTO app_themes (ThemeName, SettingsJson)
VALUES (
    'Custom Theme',
    '{
        "FormBackColor": "30,30,30",
        "FormForeColor": "255,255,255",
        "ButtonBackColor": "60,60,60",
        -- ... 200+ more properties
    }'
);
```

**Method 2: Application Code**
```csharp
// Create theme programmatically
var customTheme = new Core_AppThemes.AppTheme
{
    Name = "Custom Theme",
    Colors = new Model_UserUiColors
    {
        FormBackColor = Color.FromArgb(30, 30, 30),
        FormForeColor = Color.FromArgb(255, 255, 255),
        // ... set all 200+ color properties
    }
};

// Save to database (via DAO)
await Dao_Themes.SaveThemeAsync(customTheme);
```

### Theme JSON Format

**Structure**:
```json
{
    "FormBackColor": "30,30,30",
    "FormForeColor": "255,255,255",
    "ButtonBackColor": "60,60,60",
    "ButtonForeColor": "255,255,255",
    "ButtonBorderColor": "68,68,68",
    "ButtonHoverBackColor": "0,122,204",
    "DataGridBackColor": "30,30,30",
    "DataGridForeColor": "255,255,255",
    "AccentColor": "0,122,204",
    "ErrorColor": "229,115,115",
    "WarningColor": "255,167,38",
    "SuccessColor": "102,187,106"
    // ... 190+ more color properties
}
```

**Color Format**: Colors stored as "R,G,B" string (e.g., "30,30,30")

### User Theme Preferences

Users can select themes via Settings dialog:
1. Settings → Appearance → Theme dropdown
2. Select from 9 available themes
3. Theme saved to user preferences
4. Applied on next login or immediately if user clicks "Apply"

**User Theme Storage**:
- User preferences stored in `users` table
- `ThemeName` column references `app_themes.ThemeName`
- Retrieved via `Dao_User.GetThemeNameAsync(userId)`

---

## Related Files

- `Core/Core_Themes.cs` - Main implementation (3087 lines)
- `Models/Model_UserUiColors.cs` - Color token catalog
- `.github/instructions/ui-scaling-consistency.instructions.md` - UI scaling standards
- `.github/instructions/winforms-responsive-layout.instructions.md` - Responsive layout patterns
- `.github/instructions/csharp-dotnet8.instructions.md` - C# coding standards

---

## References

- **Telerik WinForms DPI Scaling**: https://www.telerik.com/blogs/winforms-scaling-at-large-dpi-settings-is-it-even-possible-
- **Grant Winney Async WinForms**: https://grantwinney.com/using-async-await-and-task-to-keep-the-winforms-ui-more-responsive/
- **Microsoft DPI Documentation**: Embedded in Core_Themes.cs XML comments

---

**End of Theme System Reference**
