# Bug Fix Tasks - Ordered by Estimated Time to Complete

## Quick Fixes (1-2 hours)

### 1. **Quickbuttons - Add Padding of 3 to main control** *(1 hour)* - Done!
- Locate `Control_QuickButtons` class
- Add `Padding = new Padding(3)` to constructor or design time properties
- Test visual appearance

### 2. **Transactions - Lifecycle Button needs to be disabled on form load** *(1 hour)* - Done!
- Find Transactions form in `Forms/` directory
- Set `LifecycleButton.Enabled = false` in form constructor
- Add logic to enable when row selected and DataGridView visible

### 3. **Advanced Inventory - Send Button needs to be disabled on control load** *(1 hour)* - Done!
- Locate Advanced Inventory form/control
- Set `SendButton.Enabled = false` in constructor
- Add validation logic to enable when appropriate

### 4. **Remove Enable / Disable Theming** *(1.5 hours)* - Done!
- Remove theming toggle controls from Settings
- Clean up related code in theme management
- Remove database references if any

### 5. **Import Excel button - Show MessageBox instead of ErrorDialog for open files** *(1.5 hours)* - Done!
- Modify Excel import logic to detect file locks
- Replace `Service_ErrorHandler.ShowErrorDialog` with `MessageBox.Show`
- Add appropriate exception handling for file access
- Improve Excel diagnostics (missing sheet, empty sheet, no data rows)
- Validate column names with aliases (e.g. Part Number -> Part)
- Clear rows from Excel file after successful save
- Reset DataGridView and re-enable Import button after save

### 6. **Persist DataGridView Column Settings** *(2 hours)* - Done!
- Implement `LoadAndApplyGridSettingsAsync` in `Core_Themes`
- Update Transfer, Remove, and Advanced Remove tabs to apply settings after data load
- Ensure column visibility and order are restored from database

## Medium Tasks (3-6 hours)

### 8. **All Themes - Set selection colors to -75% brightness** *(3 hours)* - Done!
- Query `app_themes` table for all themes
- Calculate -75% brightness for all selection color values
- Update `SettingsJson` column with new color values
- Test theme switching and selection visibility
- All this should be done using MAMP MySQL 5.7 CLI, do not create files to do this, and if you do remove them when you are done.

### 9. **MainForm - Fix Developer menu not showing** *(3 hours)* - Done!
- Check user permission logic in `MainForm`
- Verify `Model_Application_Variables.User` role detection
- Debug menu visibility conditions
- Test with different user types

### 10. **Set proper tab indexing for all controls** *(4 hours)* - Done!
- Review all forms in `Forms/` directory
- Set logical `TabIndex` values (0, 1, 2, etc.)
- Test tab navigation on each form
- Document tab order patterns

### 11. **Settings - Themes - Add Enable Animation toggle checkbox** *(4 hours)* - Done!
- Add animation setting to `usr_settings` table `settingsjson`
- Create UI toggle in Settings → Themes
- Implement animation enable/disable logic
- Update theme system to respect setting

### 13. **Print - Use proper easy-to-understand header names** *(5 hours)* - Done!
- Review all print service calls
- Replace technical column names with user-friendly headers
- Update print templates and formatting
- Test print output readability

## Major Refactoring (8-16 hours)

### 14. **Advanced Inventory - Convert textboxes to SuggestionTextBoxWithLabel** *(8 hours)* - Done!
- Identify Quantity and How Many textboxes
- Replace with `SuggestionTextBoxWithLabel` controls
- Implement suggestion logic and data sources
- Update validation and form logic
- Test user experience

### 15. **Print - Fix transparent background and cell backgrounds** *(10 hours)* - Done!
- Modify print service to use transparent backgrounds
- Implement proper DataGridView cell background rendering
- Test print preview and actual printing
- Ensure compatibility with different printers

### 16. **Print - Fix settings not passing to Windows print manager** *(12 hours)* - Done!
- Debug print settings pipeline
- Identify where settings are lost between UI and print manager
- Implement proper `PrintDocument` configuration
- Test with various printer settings and paper sizes

### 17. **Settings - Add Auto-Expand/Collapse toggle** *(12 hours)*
- Add new setting to `usr_settings` table `settingsjson`
- Create UI toggle in Settings
- Implement expand/collapse logic across all applicable forms/controls
- Update existing Panel, and GroupBox, TableLayoutPanel, Visiblilty triggers caused by auto collapse behaviors

### 18. **Redesign Shortcut Settings Menu** *(14 hours)*
- Analyze current shortcut settings implementation
- Design new user interface layout
- Refactor settings storage and retrieval
- Implement new keyboard shortcut management
- Test shortcut conflicts and validation

### 19. **Redesign About Settings Menu** *(16 hours)*
- Review current About dialog functionality
- Design new layout with proper version info, credits, system info
- Implement new UI using `ThemedForm` patterns
- Add useful troubleshooting information
- Test across different system configurations

**Total Estimated Time: 102.5 hours**

**Priority Recommendation:** Start with Quick Fixes (1-7) to provide immediate user experience improvements, then tackle Medium Tasks based on user impact and business priority.
