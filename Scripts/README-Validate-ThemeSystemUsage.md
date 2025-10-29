# Validate-ThemeSystemUsage.ps1

## Purpose

Validates that theme system methods are used correctly according to the rules defined in `.github/instructions/theme-system.instructions.md`.

## Features

- ✅ Scans C# files for theme method usage
- ✅ Validates method call contexts (constructor, Load event, etc.)
- ✅ Detects UserControls incorrectly calling `ApplyTheme()`
- ✅ Checks for theme calls in forbidden event handlers
- ✅ Verifies `AutoScaleMode.Dpi` in Designer files
- ✅ Color-coded output (Green=Success, Yellow=Warning, Red=Error)
- ✅ Detailed violation reporting with file/line numbers
- ✅ Summary statistics

## Usage

### Validate All Forms and UserControls

```powershell
.\Scripts\Validate-ThemeSystemUsage.ps1
```

Scans:
- `Forms\MainForm\MainForm.cs`
- All files matching `Controls/**\Control_*.cs`
- All files matching `Forms/**\*Form.cs`

### Validate Specific File

```powershell
.\Scripts\Validate-ThemeSystemUsage.ps1 -FilePath "Forms\ViewLogs\ViewApplicationLogsForm.cs"
```

### Strict Mode (Warnings = Errors)

```powershell
.\Scripts\Validate-ThemeSystemUsage.ps1 -Strict
```

In strict mode, any warnings will cause the script to exit with code 1 (failure).

### Export Results to File

```powershell
# Export to default location (.\theme-validation-results.txt)
.\Scripts\Validate-ThemeSystemUsage.ps1

# Export to custom location
.\Scripts\Validate-ThemeSystemUsage.ps1 -ExportPath "C:\Reports\theme-validation.txt"

# Validate specific file and export
.\Scripts\Validate-ThemeSystemUsage.ps1 -FilePath "Forms\ViewLogs\ViewApplicationLogsForm.cs" -ExportPath ".\logs-validation.txt"
```

**Exported File Format:**
- Timestamp and scan summary
- File-by-file validation results
- Error/Warning messages with line numbers and context
- Summary statistics (successes, warnings, errors)
- Clean text format for easy review and archiving

## Validation Rules

### ❌ Errors (Must Fix)

1. **UserControls calling ApplyTheme()**
   - Rule: Theme colors cascade from parent form
   - Fix: Remove `Core_Themes.ApplyTheme(this)` from UserControls

2. **Theme calls in forbidden event handlers**
   - Rule: Theme methods should NOT be in click/change/mouse events
   - Fix: Move theme calls to constructor, Load event, or initialization methods

### ⚠️ Warnings (Should Review)

1. **DPI/Layout methods outside constructor**
   - Rule: `ApplyDpiScaling()` and `ApplyRuntimeLayoutAdjustments()` should be in constructor
   - Note: Some false positives if constructor not detected correctly

2. **ApplyTheme() in unexpected contexts**
   - Rule: `ApplyTheme()` should be in Load, Shown, DpiChanged, or Settings events
   - Note: May have legitimate reasons for other placements

3. **AutoScaleMode not set to Dpi**
   - Rule: Designer should have `AutoScaleMode = AutoScaleMode.Dpi`
   - Fix: Set in Designer.cs file

## Output Format

```
================================================
Theme System Usage Validator
================================================

Scanning MainForm and all UserControls...
Found 37 files to validate

Validating: Forms\MainForm\MainForm.cs
✅ [SUCCESS] Forms\MainForm\MainForm.cs:128 - ApplyTheme() correctly placed in MainForm_Load
⚠️  [WARNING] Forms\MainForm\MainForm.cs:569 - ApplyTheme() called outside recommended contexts
    Context: MainForm_DpiChanged
❌ [ERROR] Controls\SettingsForm\Control_Theme.cs:83 - ApplyTheme() called in UserControl
    Context: SaveButton_Click

================================================
Validation Summary
================================================

Files Scanned: 37
Successes: 4
Warnings: 105
Errors: 3

❌ Validation FAILED - 3 error(s) found
```

## Exit Codes

- **0** - Validation passed (no errors, warnings OK)
- **1** - Validation failed (errors found, or warnings in strict mode)

## Integration with CI/CD

### Pre-commit Hook

Add to `.git/hooks/pre-commit`:

```bash
#!/bin/bash
pwsh -Command ".\Scripts\Validate-ThemeSystemUsage.ps1 -Strict"
if [ $? -ne 0 ]; then
    echo "Theme validation failed. Fix errors before committing."
    exit 1
fi
```

### GitHub Actions Workflow

```yaml
- name: Validate Theme System Usage
  run: |
    .\Scripts\Validate-ThemeSystemUsage.ps1 -Strict
  shell: pwsh
```

## Known Limitations

1. **Constructor Detection**: May not correctly identify constructors if they span multiple lines or have complex signatures
2. **False Positives**: Some warnings may be legitimate (e.g., DPI scaling in special initialization methods)
3. **Designer Files**: Only checks for `AutoScaleMode` presence, not comprehensive Designer validation

## Current Findings (as of 2025-10-28)

### Summary Statistics (After Logic Improvements)

- **Files Scanned**: 37 (MainForm + all UserControls + Forms)
- **Successes**: 74 ✅ (+70 from initial run)
- **Warnings**: 35 ⚠️ (-70 from initial run)  
- **Errors**: 3 ❌ (Real violations that need fixing)

### Errors Found: 3 (MUST FIX)

1. `Controls\SettingsForm\Control_Shortcuts.cs:276` - UserControl calling ApplyTheme()
2. `Controls\SettingsForm\Control_Theme.cs:83` - UserControl calling ApplyTheme() in SaveButton_Click
3. `Controls\SettingsForm\Control_Theme.cs:115` - UserControl calling ApplyTheme() in PreviewButton_Click

### Warnings: 105

- Most warnings are DPI/Layout methods detected outside constructors (may be false positives)
- Several controls missing `AutoScaleMode.Dpi` in Designer files

## Related Documentation

- `.github/instructions/theme-system.instructions.md` - Complete theme system reference
- `.github/instructions/ui-scaling-consistency.instructions.md` - DPI scaling standards
- `.github/instructions/winforms-responsive-layout.instructions.md` - Layout patterns

## Support

For questions or issues with this script, contact the development team or create an issue in the repository.
