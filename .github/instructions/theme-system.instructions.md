---
description: 'Comprehensive MTM theme system architecture, initialization, and usage patterns'
applyTo: '**/*.cs,**/*.Designer.cs'
---

# MTM Theme System - Complete Reference

## Overview

The MTM WIP Application implements a comprehensive theme system that provides:
- **User-customizable color schemes** stored in MySQL database
- **DPI-aware scaling** for 100%, 125%, 150%, 200% display settings
- **Runtime layout adjustments** for responsive UI across screen sizes
- **Focus highlighting** for improved keyboard navigation
- **Per-monitor DPI awareness** for multi-monitor setups
- **Graceful fallback** to default themes when database unavailable

## Architecture

### Core Components

```
Core/
├── Core_Themes.cs              # Main theme API (74 public static methods)
│   ├── Core_AppThemes          # Nested class for theme management
│   ├── ThemeAppliersInternal   # Internal theme application logic
│   ├── FocusUtils              # Focus highlighting utilities
│   └── ColorUtils              # Color manipulation helpers
├── Core_JsonColorConverter.cs  # JSON serialization for Color types
└── Core_WipAppVariables.cs     # Global theme configuration

Models/
└── Model_UserUiColors.cs       # 40+ color properties for comprehensive theming

Data/
└── Dao_System.cs               # Database access for themes (GetAllThemesAsync)
    └── sys_GetAllThemes        # Stored procedure returning ThemeName, SettingsJson

Services/
└── Service_OnStartup_StartupSplashApplicationContext.cs
    └── RunStartupAsync()       # Step 7: Theme system initialization
```

### Database Schema

**Table**: `sys_user_themes` (or similar)
```sql
CREATE TABLE sys_user_themes (
    ThemeID INT PRIMARY KEY AUTO_INCREMENT,
    ThemeName VARCHAR(50) UNIQUE NOT NULL,
    SettingsJson LONGTEXT NOT NULL,  -- JSON-serialized Model_UserUiColors
    IsActive TINYINT DEFAULT 1,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate DATETIME ON UPDATE CURRENT_TIMESTAMP
);
```

**Stored Procedure**: `sys_GetAllThemes`
```sql
-- Returns: ThemeName, SettingsJson
-- Used by: Dao_System.GetAllThemesAsync()
-- Purpose: Load all available themes from database
```

## Theme Initialization Flow

### Startup Sequence (Service_OnStartup_StartupSplashApplicationContext)

```csharp
// Step 7 of 12: Initialize theme system (70% progress)
await Core_Themes.Core_AppThemes.InitializeThemeSystemAsync(Model_AppVariables.User);

// Step 9 of 12: Load theme settings (85% progress)
await LoadThemeSettingsAsync();

// Step 11 of 12: Create MainForm (95% progress)
_mainForm = new MainForm();
```

### InitializeThemeSystemAsync Flow

```
1. LoadThemesFromDatabaseAsync()
   ├─ Call Dao_System.GetAllThemesAsync()
   ├─ Deserialize JSON → Model_UserUiColors
   ├─ Create AppTheme objects
   └─ Store in static Dictionary<string, AppTheme>

2. LoadAndSetUserThemeNameAsync(userId)
   ├─ Call Dao_User.GetThemeNameAsync(userId)
   └─ Set Model_AppVariables.ThemeName

3. Validate Theme Exists
   ├─ Check if user's theme in loaded themes
   ├─ Fallback to "Default" → "Dark" → "Blue" → First available
   └─ Log theme selection decision

4. Apply Font Settings
   ├─ Set theme.FormFont = "Segoe UI Emoji"
   ├─ Use Model_AppVariables.ThemeFontSize
   └─ Apply to all loaded themes

5. Error Handling
   ├─ On database failure: CreateDefaultThemes()
   ├─ On JSON parse failure: Skip theme, log error
   └─ On empty themes: Create fallback themes
```

## Model_UserUiColors - Complete Reference

### 40+ Themable Color Properties

```csharp
public class Model_UserUiColors
{
    // Form-level colors (2)
    public Color? FormBackColor { get; set; }          // Form background
    public Color? FormForeColor { get; set; }          // Form text
    public Color? FormBorderColor { get; internal set; }  // Form border (internal)

    // Generic control colors (4)
    public Color? ControlBackColor { get; set; }       // Control background
    public Color? ControlForeColor { get; set; }       // Control text
    public Color? ControlFocusedBackColor { get; set; } // Focused control highlight
    public Color? ControlDisabledBackColor { get; set; } // Disabled control background

    // Label colors (2)
    public Color? LabelBackColor { get; set; }         // Label background
    public Color? LabelForeColor { get; set; }         // Label text

    // TextBox colors (4)
    public Color? TextBoxBackColor { get; set; }       // TextBox background
    public Color? TextBoxForeColor { get; set; }       // TextBox text
    public Color? TextBoxBorderColor { get; set; }     // TextBox border
    public Color? TextBoxFocusedBorderColor { get; set; } // Focused TextBox border

    // Button colors (6)
    public Color? ButtonBackColor { get; set; }        // Button background
    public Color? ButtonForeColor { get; set; }        // Button text
    public Color? ButtonBorderColor { get; set; }      // Button border
    public Color? ButtonHoverBackColor { get; set; }   // Button hover state
    public Color? ButtonPressedBackColor { get; set; } // Button pressed state
    public Color? ButtonDisabledBackColor { get; set; } // Disabled button

    // ComboBox colors (4)
    public Color? ComboBoxBackColor { get; set; }      // ComboBox background
    public Color? ComboBoxForeColor { get; set; }      // ComboBox text
    public Color? ComboBoxBorderColor { get; set; }    // ComboBox border
    public Color? ComboBoxErrorForeColor { get; set; } // Error state text (red)

    // DataGridView colors (9)
    public Color? DataGridBackColor { get; set; }      // Grid background
    public Color? DataGridForeColor { get; set; }      // Grid text
    public Color? DataGridHeaderBackColor { get; set; } // Header background
    public Color? DataGridHeaderForeColor { get; set; } // Header text
    public Color? DataGridRowBackColor { get; set; }   // Row background
    public Color? DataGridAltRowBackColor { get; set; } // Alternate row background
    public Color? DataGridSelectionBackColor { get; set; } // Selected row background
    public Color? DataGridSelectionForeColor { get; set; } // Selected row text
    public Color? DataGridGridColor { get; set; }      // Grid lines

    // Panel colors (2)
    public Color? PanelBackColor { get; set; }         // Panel background
    public Color? PanelBorderColor { get; set; }       // Panel border

    // GroupBox colors (2)
    public Color? GroupBoxBackColor { get; set; }      // GroupBox background
    public Color? GroupBoxForeColor { get; set; }      // GroupBox text

    // MenuStrip colors (4)
    public Color? MenuStripBackColor { get; set; }     // Menu background
    public Color? MenuStripForeColor { get; set; }     // Menu text
    public Color? MenuItemSelectedBackColor { get; set; } // Selected menu item
    public Color? MenuItemHoverBackColor { get; set; } // Hovered menu item

    // ToolStrip colors (2)
    public Color? ToolStripBackColor { get; set; }     // Toolbar background
    public Color? ToolStripForeColor { get; set; }     // Toolbar text
}
```

## Core_Themes Public API (74 Methods)

### Primary Theme Application

```csharp
// Apply complete theme (colors, DPI, layout, focus highlighting)
Core_Themes.ApplyTheme(Form form)
    └─ Calls: ApplyDpiScaling() → ApplyRuntimeLayoutAdjustments() → SetFormTheme() → ApplyThemeToControls()

// Get user's theme colors from database
Task<Model_UserUiColors> GetUserThemeColorsAsync(string userId)

// Apply theme to DataGridView specifically
ApplyThemeToDataGridView(DataGridView dataGridView)

// Apply focus highlighting to control hierarchy
ApplyFocusHighlighting(Control parentControl)
```

### DPI Scaling Methods

```csharp
// Apply DPI scaling to form and all controls
ApplyDpiScaling(Form form)
ApplyDpiScaling(UserControl userControl)

// Apply runtime layout adjustments (margins, padding, splitter distances)
ApplyRuntimeLayoutAdjustments(Form form)
ApplyRuntimeLayoutAdjustments(UserControl userControl)

// Handle dynamic DPI changes (multi-monitor scenarios)
HandleDpiChanged(Form form, int oldDpi, int newDpi)
```

### Layout Adjustment Methods

```csharp
// Container adjustments
AdjustTableLayoutPanelForDpi(TableLayoutPanel panel)
AdjustGroupBoxForDpi(GroupBox groupBox)
AdjustPanelForDpi(Panel panel)
AdjustSplitContainerForDpi(SplitContainer splitContainer)

// Control sizing
SizeDataGrid(DataGridView dataGridView)
```

### Internal Theme Application

```csharp
// Control-specific theme application (called by ApplyTheme)
private SetFormTheme(Form form, AppTheme theme, string themeName)
private ApplyThemeToControls(Control.ControlCollection controls)
private ApplyThemeToButton(Button button, Model_UserUiColors colors)
private ApplyThemeToTextBox(TextBox textBox, Model_UserUiColors colors)
private ApplyThemeToComboBox(ComboBox comboBox, Model_UserUiColors colors)
private ApplyThemeToDataGridView(DataGridView dgv, Model_UserUiColors colors)
private ApplyThemeToLabel(Label label, Model_UserUiColors colors)
private ApplyThemeToPanel(Panel panel, Model_UserUiColors colors)
private ApplyThemeToGroupBox(GroupBox groupBox, Model_UserUiColors colors)
private ApplyThemeToMenuStrip(MenuStrip menuStrip, Model_UserUiColors colors)
```

## Form/Control Integration Pattern

### MainForm Pattern (Template for All Forms)

```csharp
public MainForm()
{
    try
    {
        InitializeComponent();
        AutoScaleMode = AutoScaleMode.Dpi;  // CRITICAL: Must be set

        // Theme application in constructor
        Core_Themes.ApplyDpiScaling(this);              // Step 1: DPI scaling
        Core_Themes.ApplyRuntimeLayoutAdjustments(this); // Step 2: Layout adjustments

        InitializeFormTitle();
        InitializeProgressControl();
        InitializeStartupComponents();
        WireUpFormShownEvent();
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: nameof(MainForm));
    }
}

private void WireUpFormShownEvent()
{
    Shown += async (s, e) =>
    {
        // Theme colors applied AFTER form is shown
        Core_Themes.ApplyTheme(this);  // Step 3: Apply colors
        // ... rest of initialization
    };
}

// Handle DPI changes (multi-monitor drag)
protected override void OnDpiChanged(DpiChangedEventArgs e)
{
    base.OnDpiChanged(e);
    DialogResult result = ShowDpiChangeDialog();
    if (result == DialogResult.No)
    {
        Core_Themes.HandleDpiChanged(this, e.DeviceDpiOld, e.DeviceDpiNew);
        Core_Themes.ApplyTheme(this);
    }
}
```

### UserControl Pattern (Inventory, Remove, Transfer Tabs)

```csharp
public Control_InventoryTab()
{
    InitializeComponent();
    
    // Theme application in constructor
    Core_Themes.ApplyDpiScaling(this);              // Step 1: DPI scaling
    Core_Themes.ApplyRuntimeLayoutAdjustments(this); // Step 2: Layout adjustments
    
    WireUpEvents();
    ApplyPrivileges();
    Control_InventoryTab_Initialize();
}

private void Control_InventoryTab_Initialize()
{
    // Additional initialization
    Core_Themes.ApplyFocusHighlighting(this);  // Step 3: Focus highlighting
}

// Theme colors inherited from parent form's ApplyTheme() call
// UserControls do NOT call Core_Themes.ApplyTheme() themselves
```

### Dialog/Child Form Pattern

```csharp
public ViewApplicationLogsForm()
{
    InitializeComponent();
    
    // Theme application in constructor
    Core_Themes.ApplyDpiScaling(this);              // Step 1: DPI scaling
    Core_Themes.ApplyRuntimeLayoutAdjustments(this); // Step 2: Layout adjustments
    Core_Themes.ApplyFocusHighlighting(this);        // Step 3: Focus highlighting
    
    WireUpEvents();
    InitializeAutoRefreshTimer();
}

private async void ViewApplicationLogsForm_Load(object sender, EventArgs e)
{
    // Apply theme colors in Load event
    Core_Themes.ApplyTheme(this);  // Step 4: Apply colors
    
    await LoadUserListAsync();
    // ... rest of initialization
}
```

### Designer Configuration (CRITICAL)

**EVERY form/control MUST have this in Designer.cs:**

```csharp
private void InitializeComponent()
{
    // ... control initialization ...
    
    // Form configuration
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;  // ⚠️ REQUIRED
    this.ClientSize = new System.Drawing.Size(1200, 800);
    this.Name = "FormName";
    // ... rest of configuration ...
}
```

## Theme Application Rules (CRITICAL)

### ✅ WHEN to Call Theme Methods

**Constructor (Forms and UserControls):**
```csharp
Core_Themes.ApplyDpiScaling(this);              // ✅ ALWAYS
Core_Themes.ApplyRuntimeLayoutAdjustments(this); // ✅ ALWAYS
Core_Themes.ApplyFocusHighlighting(this);        // ✅ OPTIONAL (recommended)
```

**Form Load Event (Forms ONLY):**
```csharp
Core_Themes.ApplyTheme(this);  // ✅ Forms apply colors in Load event
```

**After Settings Change (MainForm ONLY):**
```csharp
Core_Themes.ApplyTheme(this);  // ✅ Refresh theme after user changes settings
```

**After DPI Change (MainForm ONLY):**
```csharp
Core_Themes.HandleDpiChanged(this, oldDpi, newDpi);
Core_Themes.ApplyTheme(this);  // ✅ Reapply theme after DPI change
```

### ❌ WHEN NOT to Call Theme Methods

**UserControls (Tabs):**
```csharp
// ❌ DO NOT call ApplyTheme() in UserControls
// Theme colors cascade from parent form
```

**Business Logic:**
```csharp
// ❌ DO NOT call theme methods in:
// - Button click handlers
// - Data processing methods
// - Validation logic
// - Background workers
```

**Arbitrary Event Handlers:**
```csharp
// ❌ DO NOT call theme methods in:
// - TextChanged events
// - SelectedIndexChanged events
// - Mouse/keyboard events
// - Timer ticks
```

## Theme Policy Document (From Core_Themes.cs)

```
THEME POLICY: Only update theme on:
1. Application startup (Service_OnStartup_StartupSplashApplicationContext)
2. Settings menu save (MainForm_MenuStrip_File_Settings_Click)
3. DPI change event (MainForm.OnDpiChanged)

DO NOT call theme update methods from:
- Arbitrary event handlers
- Business logic
- Data processing code
- Background workers
```

## Default Theme Fallback

### Fallback Theme Creation

When database access fails or returns no themes, `CreateDefaultThemes()` provides:

```csharp
Dictionary<string, AppTheme> CreateDefaultThemes()
{
    return new Dictionary<string, AppTheme>
    {
        ["Default"] = new AppTheme { Colors = DefaultLightTheme },
        ["Dark"]    = new AppTheme { Colors = DarkTheme },
        ["Blue"]    = new AppTheme { Colors = BlueTheme },
        ["Green"]   = new AppTheme { Colors = GreenTheme }
    };
}
```

### Fallback Priority Order

```
1. User's preferred theme (from database)
   ↓ (if doesn't exist)
2. "Default" theme
   ↓ (if doesn't exist)
3. "Dark" theme
   ↓ (if doesn't exist)
4. "Blue" theme
   ↓ (if doesn't exist)
5. First available theme in dictionary
   ↓ (if dictionary empty)
6. CreateDefaultThemes() - hardcoded fallbacks
```

## DPI Scaling Details

### Supported DPI Settings

- **100%** - 96 DPI (Standard)
- **125%** - 120 DPI (Recommended)
- **150%** - 144 DPI (Large)
- **175%** - 168 DPI (Extra Large)
- **200%** - 192 DPI (Maximum)

### DPI Scaling Features

1. **AutoScaleMode.Dpi**
   - Set in Designer.cs for every form/control
   - Enables Windows automatic DPI scaling
   - Base for all custom scaling logic

2. **Runtime Layout Adjustments**
   - Margins and padding scaled by DPI ratio
   - SplitContainer distances recalculated
   - Control minimum/maximum sizes adjusted
   - Font sizes scaled appropriately

3. **Per-Monitor DPI Awareness**
   - Detects DPI changes when dragging between monitors
   - Prompts user: Restart or Auto-resize
   - Logs DPI changes for diagnostics

4. **DPI Change Dialog**
   ```
   "Display DPI Changed"
   "This application requires a restart to properly adjust to the new DPI setting."
   
   [Yes, restart] [No, auto-resize] [Cancel]
   ```

## Focus Highlighting System

### Purpose

Improves keyboard navigation visibility by highlighting focused controls with theme-aware colors.

### Implementation

```csharp
Core_Themes.ApplyFocusHighlighting(Control parentControl)
    └─ FocusUtils.ApplyFocusEventHandlingToControls(controls, themeColors)
        ├─ GotFocus: Change BackColor to ControlFocusedBackColor
        └─ LostFocus: Restore original BackColor
```

### Supported Controls

- TextBox
- ComboBox
- ListBox
- CheckBox
- RadioButton
- DataGridView
- Button

### Usage

```csharp
// Apply once in constructor or initialization
Core_Themes.ApplyFocusHighlighting(this);

// Focus highlighting persists for form lifetime
// No need to reapply unless controls dynamically added
```

## Troubleshooting

### Theme Colors Not Applied

**Symptom**: Form shows default Windows colors instead of theme colors

**Solution**:
1. Verify `Core_Themes.ApplyTheme(this)` called in Form Load event
2. Check `AutoScaleMode = AutoScaleMode.Dpi` in Designer.cs
3. Ensure theme system initialized in startup sequence
4. Check logs for theme loading errors

### DPI Scaling Issues

**Symptom**: Controls misaligned or wrong size at high DPI

**Solution**:
1. Verify `Core_Themes.ApplyDpiScaling(this)` in constructor
2. Verify `Core_Themes.ApplyRuntimeLayoutAdjustments(this)` in constructor
3. Check `AutoScaleMode.Dpi` set in Designer
4. Avoid hardcoded pixel sizes in code (use DPI-scaled values)

### UserControl Theme Not Applied

**Symptom**: UserControl colors don't match parent form

**Solution**:
1. DO NOT call `Core_Themes.ApplyTheme()` in UserControl
2. Theme colors cascade from parent form
3. Only call DPI scaling methods in UserControl constructor
4. Parent form's ApplyTheme() handles all descendants

### Database Theme Loading Fails

**Symptom**: "Creating fallback themes" in logs

**Solution**:
1. Verify `sys_user_themes` table exists
2. Verify `sys_GetAllThemes` stored procedure exists
3. Check database connectivity during startup
4. Fallback themes provide safe defaults (expected behavior)

## Best Practices

### ✅ DO

- Set `AutoScaleMode.Dpi` in every form/control Designer
- Call DPI scaling methods in constructor
- Call `ApplyTheme()` in Form Load event (forms only)
- Apply focus highlighting once in initialization
- Test at multiple DPI settings (100%, 125%, 150%, 200%)
- Store themes in database as JSON
- Provide fallback themes for offline scenarios
- Log theme loading decisions for diagnostics

### ❌ DON'T

- Call `ApplyTheme()` in UserControls
- Call theme methods in business logic
- Call theme methods in event handlers
- Hardcode colors (use theme system)
- Assume specific DPI (scale everything)
- Modify `Designer.cs` files manually
- Skip error handling in theme initialization
- Remove fallback theme creation

## References

### Key Files

- **Core/Core_Themes.cs** - Main theme API (3087 lines, 74 public methods)
- **Models/Model_UserUiColors.cs** - Theme color structure (40+ properties)
- **Core/Core_JsonColorConverter.cs** - JSON serialization for Color
- **Data/Dao_System.cs** - `GetAllThemesAsync()` database access
- **Services/Service_OnStartup_StartupSplashApplicationContext.cs** - Theme initialization

### Related Instructions

- `.github/instructions/csharp-dotnet8.instructions.md` - C# patterns
- `.github/instructions/winforms-responsive-layout.instructions.md` - Layout patterns
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI consistency

### External References

- [Telerik WinForms DPI Scaling](https://www.telerik.com/blogs/winforms-scaling-at-large-dpi-settings-is-it-even-possible-)
- [Grant Winney Async WinForms](https://grantwinney.com/using-async-await-and-task-to-keep-the-winforms-ui-more-responsive/)

---

**Last Updated**: 2025-10-28  
**Version**: 1.0  
**Maintainer**: Development Team
