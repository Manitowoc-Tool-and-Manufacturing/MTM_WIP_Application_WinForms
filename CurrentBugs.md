# Bug Fix Tasks - Ordered by Estimated Time to Complete

## Quick Fixes (1-2 hours)

### 1. **Quickbuttons - Add Padding of 3 to main control** *(1 hour)*
- Locate `Control_QuickButtons` class
- Add `Padding = new Padding(3)` to constructor or design time properties
- Test visual appearance

### 2. **Transactions - Lifecycle Button needs to be disabled on form load** *(1 hour)*
- Find Transactions form in `Forms/` directory
- Set `LifecycleButton.Enabled = false` in form constructor
- Add logic to enable when row selected and DataGridView visible

### 3. **Advanced Inventory - Send Button needs to be disabled on control load** *(1 hour)*
- Locate Advanced Inventory form/control
- Set `SendButton.Enabled = false` in constructor
- Add validation logic to enable when appropriate

### 4. **Remove Enable / Disable Theming** *(1.5 hours)*
- Remove theming toggle controls from Settings
- Clean up related code in theme management
- Remove database references if any

### 5. **Import Excel button - Show MessageBox instead of ErrorDialog for open files** *(1.5 hours)*
- Modify Excel import logic to detect file locks
- Replace `Service_ErrorHandler.ShowErrorDialog` with `MessageBox.Show`
- Add appropriate exception handling for file access

## Medium Tasks (3-6 hours)

### 6. **All Themes - Set selection colors to -75% brightness** *(3 hours)*
- Query `app_themes` table for all themes
- Calculate -75% brightness for all selection color values
- Update `SettingsJson` column with new color values
- Test theme switching and selection visibility

### 7. **MainForm - Fix Developer menu not showing** *(3 hours)*
- Check user permission logic in `MainForm`
- Verify `Model_Application_Variables.User` role detection
- Debug menu visibility conditions
- Test with different user types

### 8. **Set proper tab indexing for all controls** *(4 hours)*
- Review all forms in `Forms/` directory
- Set logical `TabIndex` values (0, 1, 2, etc.)
- Test tab navigation on each form
- Document tab order patterns

### 9. **Settings - Themes - Add Enable Animation toggle checkbox** *(4 hours)*
- Add animation setting to `usr_settings` table `settingsjson`
- Create UI toggle in Settings → Themes
- Implement animation enable/disable logic
- Update theme system to respect setting

### 10. **Clean Excel Button investigation and fix** *(4 hours)*
- Locate Clean Excel button implementation
- Debug and trace execution path
- Identify why no action occurs
- Implement proper functionality or remove if obsolete

### 11. **Print - Use proper easy-to-understand header names** *(5 hours)*
- Review all print service calls
- Replace technical column names with user-friendly headers
- Update print templates and formatting
- Test print output readability

## Major Refactoring (8-16 hours)

### 12. **Advanced Inventory - Convert textboxes to SuggestionTextBoxWithLabel** *(8 hours)*
- Identify Quantity and How Many textboxes
- Replace with `SuggestionTextBoxWithLabel` controls
- Implement suggestion logic and data sources
- Update validation and form logic
- Test user experience

### 13. **Print - Fix transparent background and cell backgrounds** *(10 hours)*
- Modify print service to use transparent backgrounds
- Implement proper DataGridView cell background rendering
- Test print preview and actual printing
- Ensure compatibility with different printers

### 14. **Print - Fix settings not passing to Windows print manager** *(12 hours)*
- Debug print settings pipeline
- Identify where settings are lost between UI and print manager
- Implement proper `PrintDocument` configuration
- Test with various printer settings and paper sizes

### 15. **Settings - Add Auto-Expand/Collapse toggle** *(12 hours)*
- Add new setting to `usr_settings` table `settingsjson`
- Create UI toggle in Settings
- Implement expand/collapse logic across all applicable forms/controls
- Update existing TreeView, Panel, and GroupBox behaviors

### 16. **Redesign Shortcut Settings Menu** *(14 hours)*
- Analyze current shortcut settings implementation
- Design new user interface layout
- Refactor settings storage and retrieval
- Implement new keyboard shortcut management
- Test shortcut conflicts and validation

### 17. **Redesign About Settings Menu** *(16 hours)*
- Review current About dialog functionality
- Design new layout with proper version info, credits, system info
- Implement new UI using `ThemedForm` patterns
- Add useful troubleshooting information
- Test across different system configurations

**Total Estimated Time: 98.5 hours**

**Priority Recommendation:** Start with Quick Fixes (1-5) to provide immediate user experience improvements, then tackle Medium Tasks based on user impact and business priority.
