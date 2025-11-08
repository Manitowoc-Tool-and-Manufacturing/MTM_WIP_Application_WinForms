---
description: 'Step-by-step migration guide from keyboard shortcuts v1.0 to v2.0 system'
applyTo: '**/*.cs'
---

# Keyboard Shortcuts System - Refactoring Guide (v1.0 → v2.0)

## Overview

This guide provides a step-by-step migration path from the legacy keyboard shortcuts system (v1.0) to the new centralized, event-driven system (v2.0). The refactoring is designed to be done incrementally without breaking existing functionality.

**Migration Strategy**: Clean break (no backward compatibility required per user requirements)

---

## Pre-Migration Checklist

Before starting the refactoring, ensure:

- [ ] Current system is documented (screenshots of shortcuts editor, list of all shortcuts)
- [ ] Database backup created
- [ ] All forms using shortcuts are identified
- [ ] Test environment is ready
- [ ] User notification plan is in place (shortcuts will reset to defaults)

---

## Phase 1: Foundation Setup (Days 1-2)

### **Step 1.1: Create New Models**

Create `Models/Model_ShortcutAction.cs`:

```csharp
namespace MTM_WIP_Application_Winforms.Models;

public class Model_ShortcutAction
{
    public string Id { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Keys DefaultKeys { get; init; } = Keys.None;
    public ShortcutScope Scope { get; init; } = ShortcutScope.Form;
    public bool AllowCustomization { get; init; } = true;
    public Func<ShortcutContext, bool>? ContextFilter { get; set; }
    public EventHandler? Handler { get; set; }
    public Control? TargetControl { get; set; }
}

public enum ShortcutScope
{
    Global,
    Form,
    Control,
    ContextMenu
}

public class ShortcutContext
{
    public bool IsTextInputFocused { get; set; }
    public string CurrentTab { get; set; } = string.Empty;
    public bool IsDialogOpen { get; set; }
    public bool IsReadOnly { get; set; }
    public Control? FocusedControl { get; set; }
    public Form? ActiveForm { get; set; }
}
```

### **Step 1.2: Create Database Schema**

Run these SQL scripts in order:

**1. Create sys_shortcut_defaults table:**

```sql
CREATE TABLE sys_shortcut_defaults (
    ActionId VARCHAR(100) PRIMARY KEY,
    DisplayName VARCHAR(200) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Description TEXT,
    DefaultKeys VARCHAR(50) NOT NULL,
    Scope VARCHAR(20) NOT NULL,
    AllowCustomization BOOLEAN DEFAULT TRUE,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    
    INDEX IX_Category (Category)
);
```

**2. Populate defaults (map from v1.0):**

```sql
-- Map old Core_WipAppVariables shortcuts to new system
INSERT INTO sys_shortcut_defaults (ActionId, DisplayName, Category, Description, DefaultKeys, Scope, AllowCustomization) VALUES
-- Global shortcuts
('global.help', 'Help', 'global', 'Open help documentation', 'F1', 'Global', FALSE),
('global.help_browser', 'Help Browser', 'global', 'Open help browser window', 'Ctrl + H', 'Global', TRUE),
('global.settings', 'Settings', 'global', 'Open settings dialog', 'Ctrl + Comma', 'Global', TRUE),

-- Main Form tabs (from Core_WipAppVariables.Shortcut_MainForm_Tab1/2/3)
('mainform.tab_inventory', 'Main Form - Inventory Tab', 'mainform', 'Switch to Inventory tab', 'Ctrl + 1', 'Global', TRUE),
('mainform.tab_transfer', 'Main Form - Transfer Tab', 'mainform', 'Switch to Transfer tab', 'Ctrl + 2', 'Global', TRUE),
('mainform.tab_remove', 'Main Form - Remove Tab', 'mainform', 'Switch to Remove tab', 'Ctrl + 3', 'Global', TRUE),

-- Inventory shortcuts (from Core_WipAppVariables.Shortcut_Inventory_*)
('inventory.save', 'Inventory - Save', 'inventory', 'Save current inventory entry', 'Ctrl + S', 'Form', TRUE),
('inventory.reset', 'Inventory - Reset', 'inventory', 'Reset inventory form', 'Ctrl + R', 'Form', TRUE),
('inventory.advanced', 'Inventory - Advanced Mode', 'inventory', 'Toggle advanced inventory mode', 'Ctrl + A', 'Form', TRUE),
('inventory.toggle_right_panel', 'Inventory - Toggle Right Panel', 'inventory', 'Show/hide right panel', 'Ctrl + Right', 'Form', TRUE),

-- Advanced Inventory (from Core_WipAppVariables.Shortcut_AdvInv_*)
('advinv.send', 'Advanced Inventory - Send', 'advinv', 'Send inventory data', 'Ctrl + Enter', 'Form', TRUE),
('advinv.save', 'Advanced Inventory - Save', 'advinv', 'Save advanced inventory', 'Ctrl + S', 'Form', TRUE),
('advinv.reset', 'Advanced Inventory - Reset', 'advinv', 'Reset advanced inventory form', 'Ctrl + R', 'Form', TRUE),
('advinv.normal', 'Advanced Inventory - Normal Mode', 'advinv', 'Return to normal mode', 'Ctrl + N', 'Form', TRUE),

-- Advanced Inventory MultiLoc (from Core_WipAppVariables.Shortcut_AdvInv_Multi_*)
('advinv_multi.add_location', 'Advanced Inventory MultiLoc - Add Location', 'advinv_multi', 'Add new location', 'Ctrl + L', 'Form', TRUE),
('advinv_multi.save_all', 'Advanced Inventory MultiLoc - Save All', 'advinv_multi', 'Save all locations', 'Ctrl + Shift + S', 'Form', TRUE),
('advinv_multi.reset', 'Advanced Inventory MultiLoc - Reset', 'advinv_multi', 'Reset all locations', 'Ctrl + Shift + R', 'Form', TRUE),
('advinv_multi.normal', 'Advanced Inventory MultiLoc - Normal', 'advinv_multi', 'Return to normal mode', 'Ctrl + N', 'Form', TRUE),

-- Advanced Inventory Import (from Core_WipAppVariables.Shortcut_AdvInv_Import_*)
('advinv_import.open_excel', 'Advanced Inventory Import - Open Excel', 'advinv_import', 'Open Excel file', 'Ctrl + O', 'Form', TRUE),
('advinv_import.import_excel', 'Advanced Inventory Import - Import Excel', 'advinv_import', 'Import Excel data', 'Ctrl + I', 'Form', TRUE),
('advinv_import.save', 'Advanced Inventory Import - Save', 'advinv_import', 'Save imported data', 'Ctrl + S', 'Form', TRUE),
('advinv_import.normal', 'Advanced Inventory Import - Normal', 'advinv_import', 'Return to normal mode', 'Ctrl + N', 'Form', TRUE),

-- Remove shortcuts (from Core_WipAppVariables.Shortcut_Remove_*)
('remove.search', 'Remove - Search', 'remove', 'Focus search field', 'Ctrl + F', 'Form', TRUE),
('remove.delete', 'Remove - Delete', 'remove', 'Delete selected item', 'Ctrl + D', 'Form', TRUE),
('remove.undo', 'Remove - Undo', 'remove', 'Undo last delete', 'Ctrl + Z', 'Form', TRUE),
('remove.reset', 'Remove - Reset', 'remove', 'Reset remove form', 'Ctrl + R', 'Form', TRUE),
('remove.advanced', 'Remove - Advanced Mode', 'remove', 'Toggle advanced mode', 'Ctrl + A', 'Form', TRUE),
('remove.normal', 'Remove - Normal Mode', 'remove', 'Return to normal mode', 'Ctrl + N', 'Form', TRUE),

-- Transfer shortcuts (from Core_WipAppVariables.Shortcut_Transfer_*)
('transfer.search', 'Transfer - Search', 'transfer', 'Focus search field', 'Ctrl + F', 'Form', TRUE),
('transfer.transfer', 'Transfer - Execute Transfer', 'transfer', 'Execute inventory transfer', 'Ctrl + T', 'Form', TRUE),
('transfer.reset', 'Transfer - Reset', 'transfer', 'Reset transfer form', 'Ctrl + R', 'Form', TRUE),
('transfer.toggle_right_panel', 'Transfer - Toggle Right Panel', 'transfer', 'Show/hide right panel', 'Ctrl + Right', 'Form', TRUE),

-- Transaction viewer shortcuts (new)
('transactions.search', 'Transactions - Search', 'transactions', 'Execute transaction search', 'Ctrl + F', 'Form', TRUE),
('transactions.reset', 'Transactions - Reset', 'transactions', 'Reset search criteria', 'Ctrl + R', 'Form', TRUE),
('transactions.export', 'Transactions - Export', 'transactions', 'Export to Excel', 'Ctrl + E', 'Form', TRUE),
('transactions.print', 'Transactions - Print', 'transactions', 'Print report', 'Ctrl + P', 'Form', TRUE),
('transactions.toggle_search', 'Transactions - Toggle Search', 'transactions', 'Show/hide search panel', 'Ctrl + Shift + F', 'Form', TRUE),

-- Dialog shortcuts (system)
('dialog.close', 'Close Dialog', 'dialog', 'Close current dialog', 'Escape', 'Global', FALSE),
('dialog.submit', 'Submit Dialog', 'dialog', 'Submit current dialog', 'Ctrl + Enter', 'Global', TRUE);
```

**3. Create usr_shortcuts table:**

```sql
CREATE TABLE usr_shortcuts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId VARCHAR(255) NOT NULL,
    ActionId VARCHAR(100) NOT NULL,
    ShortcutKeys VARCHAR(50) NOT NULL,
    IsCustomized BOOLEAN DEFAULT TRUE,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    UNIQUE KEY UK_UserAction (UserId, ActionId),
    INDEX IX_UserId (UserId),
    INDEX IX_ActionId (ActionId),
    
    FOREIGN KEY FK_UserId (UserId) 
        REFERENCES md_users(User) 
        ON DELETE CASCADE
);
```

**4. Create stored procedures:**

Copy SQL from `keyboard-shortcuts-system.instructions.md`:
- `usr_shortcuts_Get`
- `usr_shortcuts_Set`
- `usr_shortcuts_Delete`
- `usr_shortcuts_ValidateConflict`

### **Step 1.3: Create DAO Methods**

Add to `Data/Dao_User.cs`:

```csharp
/// <summary>
/// Gets user's customized shortcuts.
/// </summary>
public static async Task<Model_Dao_Result<DataTable>> GetUserShortcutsAsync(string userId)
{
    string connectionString = Helper_Database_Variables.GetConnectionString();
    
    var parameters = new Dictionary<string, object>
    {
        ["UserId"] = userId
    };

    var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
        connectionString,
        "usr_shortcuts_Get",
        parameters,
        useAsync: true
    );

    return new Model_Dao_Result<DataTable>
    {
        IsSuccess = result.IsSuccess,
        Data = result.Payload,
        ErrorMessage = result.StatusMessage
    };
}

/// <summary>
/// Saves user's customized shortcut.
/// </summary>
public static async Task<Model_Dao_Result> SaveUserShortcutAsync(string userId, string actionId, string shortcutKeys)
{
    string connectionString = Helper_Database_Variables.GetConnectionString();
    
    var parameters = new Dictionary<string, object>
    {
        ["UserId"] = userId,
        ["ActionId"] = actionId,
        ["ShortcutKeys"] = shortcutKeys
    };

    var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
        connectionString,
        "usr_shortcuts_Set",
        parameters,
        useAsync: true
    );

    return new Model_Dao_Result
    {
        IsSuccess = result.IsSuccess,
        ErrorMessage = result.StatusMessage
    };
}

/// <summary>
/// Deletes user's customized shortcut (resets to default).
/// </summary>
public static async Task<Model_Dao_Result> DeleteUserShortcutAsync(string userId, string actionId)
{
    string connectionString = Helper_Database_Variables.GetConnectionString();
    
    var parameters = new Dictionary<string, object>
    {
        ["UserId"] = userId,
        ["ActionId"] = actionId
    };

    var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
        connectionString,
        "usr_shortcuts_Delete",
        parameters,
        useAsync: true
    );

    return new Model_Dao_Result
    {
        IsSuccess = result.IsSuccess,
        ErrorMessage = result.StatusMessage
    };
}
```

---

## Phase 2: Service Implementation (Days 3-5)

### **Step 2.1: Create Service_ShortcutManager**

Create `Services/Service_ShortcutManager.cs` with full implementation from `keyboard-shortcuts-system.instructions.md`.

### **Step 2.2: Initialize in Program.cs**

```csharp
// In Program.cs, after user login
var shortcutManager = Service_ShortcutManager.Instance;
await shortcutManager.LoadUserShortcutsAsync(Model_Application_Variables.User);
```

### **Step 2.3: Keep Helper_UI_Shortcuts**

**DO NOT DELETE** `Helper_UI_Shortcuts.cs` - it's still needed for:
- `ToShortcutString(Keys)` - Convert Keys to display string
- `FromShortcutString(string)` - Parse string to Keys

These utilities are reused by the new system.

---

## Phase 3: Form Migration (Days 6-10)

### **Migration Pattern for Each Form**

#### **Step 3.1: Identify Current Shortcuts**

Find all `ProcessCmdKey` overrides:

```csharp
// OLD PATTERN (v1.0)
protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
{
    if (keyData == Core_WipAppVariables.Shortcut_Inventory_Save)
    {
        btnSave_Click(this, EventArgs.Empty);
        return true;
    }
    if (keyData == Core_WipAppVariables.Shortcut_Inventory_Reset)
    {
        btnReset_Click(this, EventArgs.Empty);
        return true;
    }
    return base.ProcessCmdKey(ref msg, keyData);
}
```

#### **Step 3.2: Replace with New System**

```csharp
// NEW PATTERN (v2.0)
public InventoryForm()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);

    // Register shortcuts
    var mgr = Service_ShortcutManager.Instance;
    mgr.RegisterFormShortcut(this, "inventory.save", btnSave_Click);
    mgr.RegisterFormShortcut(this, "inventory.reset", btnReset_Click);
    mgr.RegisterFormShortcut(this, "inventory.advanced", btnAdvanced_Click);
    
    // Auto-apply tooltips
    mgr.ApplyShortcutTooltips(this);
}

// DELETE ProcessCmdKey override entirely
```

#### **Step 3.3: Auto-Registration Alternative**

If button names follow convention:

```csharp
public InventoryForm()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);

    // Auto-register all buttons (btnSave → inventory.save, etc.)
    var mgr = Service_ShortcutManager.Instance;
    mgr.AutoRegisterFormShortcuts(this);
    mgr.ApplyShortcutTooltips(this);
}
```

### **Forms to Migrate (Priority Order)**

1. ✅ **Transactions.cs** - Start here (newest, simplest)
2. **MainForm.cs** - Tab switching shortcuts
3. **Inventory forms** - Most used
4. **Transfer forms**
5. **Remove forms**
6. **Settings dialogs**
7. **Advanced forms** (MultiLoc, Import)

---

## Phase 4: Remove Legacy Code (Day 11)

### **Step 4.1: Remove Core_WipAppVariables Shortcut Fields**

**BEFORE** (in `Core/Core_WipAppVariables.cs`):
```csharp
public static Keys Shortcut_MainForm_Tab1 = Keys.Control | Keys.D1;
public static Keys Shortcut_MainForm_Tab2 = Keys.Control | Keys.D2;
public static Keys Shortcut_MainForm_Tab3 = Keys.Control | Keys.D3;
public static Keys Shortcut_Inventory_Save = Keys.Control | Keys.S;
public static Keys Shortcut_Inventory_Reset = Keys.Control | Keys.R;
// ... 30+ more fields
```

**AFTER**:
```csharp
// DELETE ALL Shortcut_* fields
// Shortcuts now managed by Service_ShortcutManager
```

### **Step 4.2: Simplify Helper_UI_Shortcuts**

**BEFORE** (old version with 300+ lines):
```csharp
public static Dictionary<string, Keys> GetShortcutDictionary() { ... }
public static void ApplyShortcutFromDictionary(string actionName, Keys newKeys) { ... }
public static void UpdateMainFormTabShortcuts(MainForm mainForm) { ... }
```

**AFTER** (slim version, keep only utilities):
```csharp
// KEEP these utility methods:
public static string ToShortcutString(Keys keys) { ... }
public static Keys FromShortcutString(string shortcutString) { ... }

// DELETE these methods (now in Service_ShortcutManager):
// - GetShortcutDictionary()
// - ApplyShortcutFromDictionary()
// - UpdateMainFormTabShortcuts()
```

### **Step 4.3: Remove Old Database Artifacts**

```sql
-- Drop old JSON-based stored procedures
DROP PROCEDURE IF EXISTS usr_ui_settings_GetShortcutsJson;
DROP PROCEDURE IF EXISTS usr_ui_settings_SetShortcutsJson;

-- Remove ShortcutsJson column from usr_ui_settings
ALTER TABLE usr_ui_settings DROP COLUMN ShortcutsJson;
```

### **Step 4.4: Delete Old Control_Shortcuts**

**Backup first**, then:

1. Rename: `Controls/SettingsForm/Control_Shortcuts.cs` → `Control_Shortcuts.OLD.cs`
2. Create new: `Controls/SettingsForm/Control_Shortcuts.cs` (use code from `keyboard-shortcuts-system.instructions.md`)
3. Update designer file
4. Test thoroughly
5. Delete `.OLD.cs` file once verified

---

## Phase 5: Testing & Validation (Days 12-14)

### **Test Checklist**

#### **Basic Functionality**
- [ ] All forms load without errors
- [ ] Shortcuts work on first form load
- [ ] Shortcuts work after form reload
- [ ] Multiple forms open simultaneously don't conflict
- [ ] Shortcuts don't fire when TextBox has focus
- [ ] Global shortcuts (F1 Help) work everywhere

#### **Customization**
- [ ] Settings > Shortcuts editor loads
- [ ] Can change shortcut in editor
- [ ] Changes persist after restart
- [ ] Conflict detection works within same category
- [ ] Can't customize system shortcuts (F1, Escape)
- [ ] Reset to default works
- [ ] Tooltips update after shortcut change

#### **Context Awareness**
- [ ] Shortcuts respect IsReadOnly flag
- [ ] Escape closes dialogs
- [ ] Ctrl+Enter submits dialogs
- [ ] Tab-specific shortcuts only work on correct tab

#### **Database**
- [ ] User shortcuts save correctly
- [ ] User shortcuts load correctly
- [ ] Delete/reset works
- [ ] Conflict validation stored procedure works
- [ ] Foreign key constraints work (delete user cascades)

---

## Phase 6: User Communication (Day 15)

### **User Notification**

**Email Template**:

> **Subject**: Keyboard Shortcuts System Upgrade
>
> Hi Team,
>
> We've upgraded the keyboard shortcuts system with the following improvements:
>
> **What's New:**
> - ✅ Shortcuts now shown in button tooltips (hover to see)
> - ✅ Improved shortcuts editor with real-time conflict detection
> - ✅ Import/export shortcuts for easy sharing
> - ✅ Keyboard shortcuts cheat sheet (press F1)
> - ✅ Better context awareness (shortcuts respect text input focus)
>
> **Action Required:**
> - Your custom shortcuts will reset to defaults
> - Please reconfigure your shortcuts in Settings > Keyboard Shortcuts
> - We apologize for the inconvenience - this is a one-time reset
>
> **Questions?**
> Contact IT support or see the help documentation (F1).

### **In-App Notification**

Show message box on first login after upgrade:

```csharp
// In Program.cs or MainForm.Load
if (IsFirstLoginAfterUpgrade())
{
    MessageBox.Show(
        "The keyboard shortcuts system has been upgraded!\n\n" +
        "✓ Shortcuts now shown in tooltips\n" +
        "✓ Improved customization editor\n" +
        "✓ Better conflict detection\n\n" +
        "Your custom shortcuts have been reset to defaults.\n" +
        "Please reconfigure in Settings > Keyboard Shortcuts.\n\n" +
        "Press F1 for help.",
        "Keyboard Shortcuts Upgraded",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    );
    
    MarkUpgradeNotificationShown();
}
```

---

## Rollback Plan

If critical issues are discovered:

### **Step 1: Revert Database Changes**

```sql
-- Restore ShortcutsJson column
ALTER TABLE usr_ui_settings ADD COLUMN ShortcutsJson JSON;

-- Restore old stored procedures
-- (run backup SQL scripts)

-- Drop new tables
DROP TABLE IF EXISTS usr_shortcuts;
DROP TABLE IF EXISTS sys_shortcut_defaults;
```

### **Step 2: Revert Code Changes**

```bash
# Git rollback
git revert <commit-hash-of-shortcuts-upgrade>
git push origin master
```

### **Step 3: Restore Core_WipAppVariables**

Restore `Shortcut_*` static fields from backup.

---

## Post-Migration Monitoring

### **Week 1 After Deployment**

Monitor for:
- [ ] Shortcuts not firing
- [ ] Unexpected shortcuts firing
- [ ] Conflict detection false positives
- [ ] Performance issues (KeyDown lag)
- [ ] Database errors in logs

### **User Feedback Collection**

- [ ] Survey users on new shortcuts system
- [ ] Document pain points
- [ ] Prioritize fixes based on impact

---

## Common Migration Issues

### **Issue 1: Form doesn't receive KeyDown events**

**Symptom**: Shortcuts don't work  
**Cause**: KeyPreview not enabled  
**Fix**: Service_ShortcutManager auto-enables, but verify:

```csharp
this.KeyPreview = true;  // Should be automatic
```

### **Issue 2: Shortcuts fire when typing in TextBox**

**Symptom**: Ctrl+S saves while typing in search box  
**Cause**: Context filter not working  
**Fix**: Ensure `IsTextInputControl` check is working:

```csharp
private bool IsTextInputControl(Control? control)
{
    return control is TextBox or ComboBox or RichTextBox or MaskedTextBox;
}
```

### **Issue 3: Tooltips don't show shortcuts**

**Symptom**: Hover shows old tooltip without shortcut  
**Cause**: `ApplyShortcutTooltips` not called  
**Fix**: Call in form constructor:

```csharp
Service_ShortcutManager.Instance.ApplyShortcutTooltips(this);
```

### **Issue 4: Database constraint errors**

**Symptom**: Foreign key violation on user delete  
**Cause**: Missing CASCADE  
**Fix**: Recreate foreign key:

```sql
ALTER TABLE usr_shortcuts 
DROP FOREIGN KEY FK_UserId;

ALTER TABLE usr_shortcuts 
ADD CONSTRAINT FK_UserId 
FOREIGN KEY (UserId) REFERENCES md_users(User) 
ON DELETE CASCADE;
```

---

## Success Criteria

Migration is complete when:

- [ ] All forms migrated to new system
- [ ] Zero compilation errors
- [ ] Zero runtime errors in test environment
- [ ] All test checklist items pass
- [ ] User documentation updated
- [ ] Help system (F1) updated
- [ ] Code review approved
- [ ] Deployed to production
- [ ] No critical bugs reported in first week

---

## Timeline Summary

| Phase | Duration | Tasks |
|-------|----------|-------|
| Phase 1: Foundation | 2 days | Models, database, DAOs |
| Phase 2: Service | 3 days | Service_ShortcutManager |
| Phase 3: Forms | 5 days | Migrate all forms |
| Phase 4: Cleanup | 1 day | Remove legacy code |
| Phase 5: Testing | 3 days | Full QA cycle |
| Phase 6: Deployment | 1 day | Deploy & monitor |
| **Total** | **15 days** | **3 weeks** |

---

## Related Files

- `keyboard-shortcuts-system.instructions.md` - Complete system reference
- `keyboard-shortcuts-improvement-suggestions.md` - Requirements and Q&A
- `AGENTS.md` - Agent development guidelines
