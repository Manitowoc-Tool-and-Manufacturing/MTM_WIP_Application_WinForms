---
description: 'Keyboard shortcuts system architecture, implementation patterns, and registration guide'
applyTo: '**/*.cs'
version: '2.0.0'
lastUpdated: '2025-11-02'
relatedFiles:
  - 'Services/Service_ShortcutManager.cs'
  - 'Models/Model_ShortcutAction.cs'
  - 'Database/UpdatedStoredProcedures/ReadyForVerification/users/usr_shortcuts_*.sql'
---

# Keyboard Shortcuts System v2.0 - Implementation Guide

## Overview

This instruction file defines the complete keyboard shortcuts architecture for the MTM WIP Application. The system provides centralized shortcut management, automatic registration, context-aware filtering, and user customization with conflict detection.

**Key Improvements Over v1.0**:
- ✅ Centralized registry (no hardcoded strings)
- ✅ Event-driven architecture (automatic KeyDown handling)
- ✅ Context-aware shortcuts (respects TextBox focus, user roles, modal dialogs)
- ✅ Normalized database schema (no JSON blobs)
- ✅ In-app discovery (tooltips, cheat sheet)
- ✅ Import/export functionality
- ✅ Quick setup wizard
- ✅ Chainable shortcuts (VS Code style)

---

## Core Architecture

### **1. ShortcutAction Model**

Defines metadata for each keyboard shortcut action.

```csharp
namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Represents a keyboard shortcut action with metadata and behavior.
/// </summary>
public class Model_ShortcutAction
{
    /// <summary>
    /// Unique identifier for the action (e.g., "inventory.save").
    /// Format: {category}.{action}
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// User-friendly display name (e.g., "Inventory - Save").
    /// </summary>
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>
    /// Category for grouping shortcuts (e.g., "inventory", "transfer").
    /// </summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>
    /// Description shown in tooltips and help system.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Default keyboard keys for this action.
    /// </summary>
    public Keys DefaultKeys { get; init; } = Keys.None;

    /// <summary>
    /// Scope where this shortcut is active.
    /// </summary>
    public ShortcutScope Scope { get; init; } = ShortcutScope.Form;

    /// <summary>
    /// Whether user can customize this shortcut.
    /// Set to false for system shortcuts like F1 (Help) and Escape (Close).
    /// </summary>
    public bool AllowCustomization { get; init; } = true;

    /// <summary>
    /// Context filter predicate. Shortcut only fires if this returns true.
    /// </summary>
    public Func<ShortcutContext, bool>? ContextFilter { get; set; }

    /// <summary>
    /// Action to execute when shortcut is triggered.
    /// </summary>
    public EventHandler? Handler { get; set; }

    /// <summary>
    /// Control this shortcut is attached to (for Control scope).
    /// </summary>
    public Control? TargetControl { get; set; }
}

/// <summary>
/// Defines where a shortcut is active.
/// </summary>
public enum ShortcutScope
{
    /// <summary>
    /// Works anywhere in the application.
    /// Example: F1 for Help, Ctrl+H for Help browser.
    /// </summary>
    Global,

    /// <summary>
    /// Active only within a specific form.
    /// Example: Ctrl+S for Save in Inventory form.
    /// </summary>
    Form,

    /// <summary>
    /// Active only when a specific control has focus.
    /// Example: F2 to edit DataGridView cell.
    /// </summary>
    Control,

    /// <summary>
    /// Context menu accelerator keys.
    /// Example: Delete key in context menu.
    /// </summary>
    ContextMenu
}

/// <summary>
/// Provides context information for shortcut filtering.
/// </summary>
public class ShortcutContext
{
    /// <summary>
    /// True if a text input control (TextBox, ComboBox, RichTextBox) has focus.
    /// </summary>
    public bool IsTextInputFocused { get; set; }

    /// <summary>
    /// Currently active tab/form name (e.g., "Inventory", "Transfer").
    /// </summary>
    public string CurrentTab { get; set; } = string.Empty;

    /// <summary>
    /// True if a modal dialog is currently open.
    /// </summary>
    public bool IsDialogOpen { get; set; }

    /// <summary>
    /// True if current user has ReadOnly role.
    /// </summary>
    public bool IsReadOnly { get; set; }

    /// <summary>
    /// Active control that currently has focus.
    /// </summary>
    public Control? FocusedControl { get; set; }

    /// <summary>
    /// Parent form of the focused control.
    /// </summary>
    public Form? ActiveForm { get; set; }
}
```

---

### **2. Service_ShortcutManager**

Centralized service for shortcut registration, execution, and management.

```csharp
namespace MTM_WIP_Application_Winforms.Services;

/// <summary>
/// Manages keyboard shortcuts across the application.
/// Handles registration, conflict detection, context filtering, and execution.
/// </summary>
public class Service_ShortcutManager
{
    #region Singleton

    private static Service_ShortcutManager? _instance;
    private static readonly object _lock = new();

    public static Service_ShortcutManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new Service_ShortcutManager();
                }
            }
            return _instance;
        }
    }

    private Service_ShortcutManager()
    {
        InitializeDefaultActions();
    }

    #endregion

    #region Fields

    private readonly Dictionary<string, Model_ShortcutAction> _actions = new();
    private readonly Dictionary<string, Keys> _userCustomizations = new();
    private readonly List<(Form Form, string ActionId)> _formRegistrations = new();
    private ShortcutContext _currentContext = new();

    #endregion

    #region Initialization

    /// <summary>
    /// Initializes default shortcut actions.
    /// Called once during service construction.
    /// </summary>
    private void InitializeDefaultActions()
    {
        // Global shortcuts (work anywhere)
        RegisterAction(new Model_ShortcutAction
        {
            Id = "global.help",
            DisplayName = "Help",
            Category = "global",
            Description = "Open help documentation",
            DefaultKeys = Keys.F1,
            Scope = ShortcutScope.Global,
            AllowCustomization = false  // System shortcut
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "global.help_browser",
            DisplayName = "Help Browser",
            Category = "global",
            Description = "Open help browser window",
            DefaultKeys = Keys.Control | Keys.H,
            Scope = ShortcutScope.Global,
            AllowCustomization = true
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "global.settings",
            DisplayName = "Settings",
            Category = "global",
            Description = "Open settings dialog",
            DefaultKeys = Keys.Control | Keys.Comma,
            Scope = ShortcutScope.Global,
            AllowCustomization = true
        });

        // Inventory tab shortcuts
        RegisterAction(new Model_ShortcutAction
        {
            Id = "inventory.save",
            DisplayName = "Inventory - Save",
            Category = "inventory",
            Description = "Save current inventory entry",
            DefaultKeys = Keys.Control | Keys.S,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "inventory.reset",
            DisplayName = "Inventory - Reset",
            Category = "inventory",
            Description = "Reset inventory form to default state",
            DefaultKeys = Keys.Control | Keys.R,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "inventory.advanced",
            DisplayName = "Inventory - Advanced Mode",
            Category = "inventory",
            Description = "Toggle advanced inventory mode",
            DefaultKeys = Keys.Control | Keys.A,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        // Transfer tab shortcuts
        RegisterAction(new Model_ShortcutAction
        {
            Id = "transfer.search",
            DisplayName = "Transfer - Search",
            Category = "transfer",
            Description = "Focus search field",
            DefaultKeys = Keys.Control | Keys.F,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "transfer.transfer",
            DisplayName = "Transfer - Execute Transfer",
            Category = "transfer",
            Description = "Execute inventory transfer",
            DefaultKeys = Keys.Control | Keys.T,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        // Remove tab shortcuts
        RegisterAction(new Model_ShortcutAction
        {
            Id = "remove.search",
            DisplayName = "Remove - Search",
            Category = "remove",
            Description = "Focus search field",
            DefaultKeys = Keys.Control | Keys.F,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "remove.delete",
            DisplayName = "Remove - Delete",
            Category = "remove",
            Description = "Delete selected inventory item",
            DefaultKeys = Keys.Control | Keys.D,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        // Transaction viewer shortcuts
        RegisterAction(new Model_ShortcutAction
        {
            Id = "transactions.search",
            DisplayName = "Transactions - Search",
            Category = "transactions",
            Description = "Execute transaction search",
            DefaultKeys = Keys.Control | Keys.F,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "transactions.reset",
            DisplayName = "Transactions - Reset",
            Category = "transactions",
            Description = "Reset search criteria",
            DefaultKeys = Keys.Control | Keys.R,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "transactions.export",
            DisplayName = "Transactions - Export",
            Category = "transactions",
            Description = "Export transactions to Excel",
            DefaultKeys = Keys.Control | Keys.E,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "transactions.print",
            DisplayName = "Transactions - Print",
            Category = "transactions",
            Description = "Print transaction report",
            DefaultKeys = Keys.Control | Keys.P,
            Scope = ShortcutScope.Form,
            AllowCustomization = true
        });

        // Dialog shortcuts (non-customizable)
        RegisterAction(new Model_ShortcutAction
        {
            Id = "dialog.close",
            DisplayName = "Close Dialog",
            Category = "dialog",
            Description = "Close current dialog (Escape)",
            DefaultKeys = Keys.Escape,
            Scope = ShortcutScope.Global,
            AllowCustomization = false  // System shortcut
        });

        RegisterAction(new Model_ShortcutAction
        {
            Id = "dialog.submit",
            DisplayName = "Submit Dialog",
            Category = "dialog",
            Description = "Submit current dialog (Ctrl+Enter)",
            DefaultKeys = Keys.Control | Keys.Enter,
            Scope = ShortcutScope.Global,
            AllowCustomization = true
        });
    }

    #endregion

    #region Registration

    /// <summary>
    /// Registers a shortcut action in the central registry.
    /// </summary>
    public void RegisterAction(Model_ShortcutAction action)
    {
        if (string.IsNullOrEmpty(action.Id))
        {
            throw new ArgumentException("Action ID cannot be null or empty", nameof(action));
        }

        _actions[action.Id] = action;
        LoggingUtility.Log($"[ShortcutManager] Registered action: {action.Id} ({action.DisplayName})");
    }

    /// <summary>
    /// Registers a shortcut for a specific form.
    /// Automatically hooks into form's KeyPreview and KeyDown events.
    /// </summary>
    public void RegisterFormShortcut(Form form, string actionId, EventHandler handler)
    {
        if (!_actions.ContainsKey(actionId))
        {
            LoggingUtility.Log($"[ShortcutManager] Warning: Action '{actionId}' not found in registry");
            return;
        }

        var action = _actions[actionId];
        action.Handler = handler;
        action.TargetControl = form;

        _formRegistrations.Add((form, actionId));

        // Ensure form has KeyPreview enabled
        if (!form.KeyPreview)
        {
            form.KeyPreview = true;
        }

        // Hook KeyDown event if not already hooked
        form.KeyDown -= Form_KeyDown;
        form.KeyDown += Form_KeyDown;

        LoggingUtility.Log($"[ShortcutManager] Registered form shortcut: {form.Name}.{actionId}");
    }

    /// <summary>
    /// Auto-registers shortcuts based on button naming convention.
    /// Example: btnSave on InventoryForm → inventory.save
    /// </summary>
    public void AutoRegisterFormShortcuts(Form form)
    {
        string formCategory = GetFormCategory(form);
        if (string.IsNullOrEmpty(formCategory))
        {
            LoggingUtility.Log($"[ShortcutManager] Could not determine category for form: {form.Name}");
            return;
        }

        // Find all buttons on the form
        var buttons = form.Controls.OfType<Button>().ToList();
        
        // Recursively find buttons in nested controls
        buttons.AddRange(FindButtonsRecursive(form.Controls));

        foreach (var button in buttons)
        {
            string actionId = GetActionIdFromButtonName(formCategory, button.Name);
            if (!string.IsNullOrEmpty(actionId) && _actions.ContainsKey(actionId))
            {
                RegisterFormShortcut(form, actionId, (s, e) => button.PerformClick());
                LoggingUtility.Log($"[ShortcutManager] Auto-registered: {button.Name} → {actionId}");
            }
        }
    }

    private List<Button> FindButtonsRecursive(Control.ControlCollection controls)
    {
        var buttons = new List<Button>();
        foreach (Control control in controls)
        {
            if (control is Button btn)
            {
                buttons.Add(btn);
            }
            if (control.HasChildren)
            {
                buttons.AddRange(FindButtonsRecursive(control.Controls));
            }
        }
        return buttons;
    }

    private string GetFormCategory(Form form)
    {
        string formName = form.Name.ToLower();
        if (formName.Contains("inventory")) return "inventory";
        if (formName.Contains("transfer")) return "transfer";
        if (formName.Contains("remove")) return "remove";
        if (formName.Contains("transaction")) return "transactions";
        return string.Empty;
    }

    private string GetActionIdFromButtonName(string category, string buttonName)
    {
        string buttonNameLower = buttonName.ToLower();
        
        // Map common button naming patterns to action IDs
        if (buttonNameLower.Contains("save")) return $"{category}.save";
        if (buttonNameLower.Contains("reset")) return $"{category}.reset";
        if (buttonNameLower.Contains("search")) return $"{category}.search";
        if (buttonNameLower.Contains("delete")) return $"{category}.delete";
        if (buttonNameLower.Contains("transfer")) return $"{category}.transfer";
        if (buttonNameLower.Contains("export")) return $"{category}.export";
        if (buttonNameLower.Contains("print")) return $"{category}.print";
        if (buttonNameLower.Contains("advanced")) return $"{category}.advanced";

        return string.Empty;
    }

    #endregion

    #region Event Handling

    private void Form_KeyDown(object? sender, KeyEventArgs e)
    {
        if (sender is not Form form) return;

        // Update current context
        UpdateContext(form);

        // Get user-customized keys or default
        Keys pressedKeys = e.KeyData;

        // Find matching action for this form
        var matchingActions = _formRegistrations
            .Where(reg => reg.Form == form && GetKeysForAction(reg.ActionId) == pressedKeys)
            .Select(reg => reg.ActionId)
            .ToList();

        foreach (var actionId in matchingActions)
        {
            if (TryExecuteShortcut(actionId))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                break;
            }
        }
    }

    private void UpdateContext(Form form)
    {
        _currentContext = new ShortcutContext
        {
            ActiveForm = form,
            FocusedControl = form.ActiveControl,
            IsTextInputFocused = IsTextInputControl(form.ActiveControl),
            CurrentTab = GetFormCategory(form),
            IsDialogOpen = form.Modal,
            IsReadOnly = Model_Application_Variables.UserTypeReadOnly
        };
    }

    private bool IsTextInputControl(Control? control)
    {
        return control is TextBox or ComboBox or RichTextBox or MaskedTextBox;
    }

    private bool TryExecuteShortcut(string actionId)
    {
        if (!_actions.TryGetValue(actionId, out var action))
        {
            return false;
        }

        // Check context filter
        if (action.ContextFilter != null && !action.ContextFilter(_currentContext))
        {
            LoggingUtility.Log($"[ShortcutManager] Context filter blocked shortcut: {actionId}");
            return false;
        }

        // Default context filtering: Don't fire shortcuts when text input has focus
        if (_currentContext.IsTextInputFocused && action.Scope != ShortcutScope.Global)
        {
            LoggingUtility.Log($"[ShortcutManager] Text input has focus, shortcut blocked: {actionId}");
            return false;
        }

        // Execute handler
        try
        {
            action.Handler?.Invoke(this, EventArgs.Empty);
            LoggingUtility.Log($"[ShortcutManager] Executed shortcut: {actionId}");
            return true;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["ActionId"] = actionId,
                    ["ActionName"] = action.DisplayName
                },
                controlName: nameof(Service_ShortcutManager));
            return false;
        }
    }

    #endregion

    #region User Customization

    /// <summary>
    /// Loads user-customized shortcuts from database.
    /// </summary>
    public async Task LoadUserShortcutsAsync(string userId)
    {
        try
        {
            var result = await Dao_User.GetUserShortcutsAsync(userId);
            if (result.IsSuccess && result.Data != null)
            {
                _userCustomizations.Clear();
                foreach (DataRow row in result.Data.Rows)
                {
                    string actionId = row["ActionId"].ToString() ?? string.Empty;
                    string keysString = row["ShortcutKeys"].ToString() ?? string.Empty;
                    
                    Keys keys = Helper_UI_Shortcuts.FromShortcutString(keysString);
                    _userCustomizations[actionId] = keys;
                }

                LoggingUtility.Log($"[ShortcutManager] Loaded {_userCustomizations.Count} user shortcuts for {userId}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["UserId"] = userId },
                controlName: nameof(Service_ShortcutManager));
        }
    }

    /// <summary>
    /// Saves user-customized shortcut to database.
    /// </summary>
    public async Task<Model_Dao_Result> SaveUserShortcutAsync(string userId, string actionId, Keys keys)
    {
        if (!_actions.ContainsKey(actionId))
        {
            return Model_Dao_Result.Failure($"Action '{actionId}' not found");
        }

        var action = _actions[actionId];
        if (!action.AllowCustomization)
        {
            return Model_Dao_Result.Failure($"Action '{actionId}' cannot be customized (system shortcut)");
        }

        // Check for conflicts
        var conflictResult = await CheckConflictAsync(userId, actionId, keys);
        if (!conflictResult.IsSuccess)
        {
            return conflictResult;
        }

        // Save to database
        string keysString = Helper_UI_Shortcuts.ToShortcutString(keys);
        var result = await Dao_User.SaveUserShortcutAsync(userId, actionId, keysString);

        if (result.IsSuccess)
        {
            // Update in-memory cache
            _userCustomizations[actionId] = keys;
            LoggingUtility.Log($"[ShortcutManager] Saved user shortcut: {userId}.{actionId} = {keysString}");
        }

        return result;
    }

    /// <summary>
    /// Checks if shortcut keys conflict with existing shortcuts in same category.
    /// </summary>
    private async Task<Model_Dao_Result> CheckConflictAsync(string userId, string actionId, Keys keys)
    {
        if (!_actions.TryGetValue(actionId, out var action))
        {
            return Model_Dao_Result.Failure("Action not found");
        }

        string category = action.Category;

        // Get all actions in same category
        var categoryActions = _actions.Values
            .Where(a => a.Category == category && a.Id != actionId)
            .ToList();

        foreach (var otherAction in categoryActions)
        {
            Keys otherKeys = GetKeysForAction(otherAction.Id);
            if (otherKeys == keys)
            {
                return Model_Dao_Result.Failure($"Shortcut conflicts with: {otherAction.DisplayName}");
            }
        }

        return Model_Dao_Result.Success();
    }

    /// <summary>
    /// Gets effective keys for an action (user customization or default).
    /// </summary>
    public Keys GetKeysForAction(string actionId)
    {
        if (_userCustomizations.TryGetValue(actionId, out Keys customKeys))
        {
            return customKeys;
        }

        if (_actions.TryGetValue(actionId, out var action))
        {
            return action.DefaultKeys;
        }

        return Keys.None;
    }

    /// <summary>
    /// Resets action to default shortcut.
    /// </summary>
    public async Task<Model_Dao_Result> ResetToDefaultAsync(string userId, string actionId)
    {
        if (!_actions.ContainsKey(actionId))
        {
            return Model_Dao_Result.Failure("Action not found");
        }

        _userCustomizations.Remove(actionId);
        
        var result = await Dao_User.DeleteUserShortcutAsync(userId, actionId);
        if (result.IsSuccess)
        {
            LoggingUtility.Log($"[ShortcutManager] Reset to default: {actionId}");
        }

        return result;
    }

    #endregion

    #region Tooltip Integration

    /// <summary>
    /// Updates button tooltip to include shortcut.
    /// Example: "Save inventory entry (Ctrl+S)"
    /// </summary>
    public void ApplyShortcutTooltips(Form form)
    {
        string category = GetFormCategory(form);
        if (string.IsNullOrEmpty(category)) return;

        var buttons = FindButtonsRecursive(form.Controls);
        
        foreach (var button in buttons)
        {
            string actionId = GetActionIdFromButtonName(category, button.Name);
            if (!string.IsNullOrEmpty(actionId) && _actions.TryGetValue(actionId, out var action))
            {
                Keys keys = GetKeysForAction(actionId);
                if (keys != Keys.None)
                {
                    string keysString = Helper_UI_Shortcuts.ToShortcutString(keys);
                    string description = action.Description;
                    
                    // Create tooltip
                    if (form.Controls.Find("toolTip1", true).FirstOrDefault() is ToolTip toolTip)
                    {
                        toolTip.SetToolTip(button, $"{description} ({keysString})");
                    }
                }
            }
        }

        LoggingUtility.Log($"[ShortcutManager] Applied tooltips for form: {form.Name}");
    }

    #endregion

    #region Query Methods

    /// <summary>
    /// Gets all registered actions.
    /// </summary>
    public IEnumerable<Model_ShortcutAction> GetAllActions()
    {
        return _actions.Values;
    }

    /// <summary>
    /// Gets actions by category.
    /// </summary>
    public IEnumerable<Model_ShortcutAction> GetActionsByCategory(string category)
    {
        return _actions.Values.Where(a => a.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets action by ID.
    /// </summary>
    public Model_ShortcutAction? GetAction(string actionId)
    {
        return _actions.TryGetValue(actionId, out var action) ? action : null;
    }

    #endregion
}
```

---

## Database Schema

### **usr_shortcuts Table**

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

### **sys_shortcut_defaults Table**

System-wide default shortcuts that users can reset to.

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

-- Populate defaults
INSERT INTO sys_shortcut_defaults (ActionId, DisplayName, Category, Description, DefaultKeys, Scope, AllowCustomization) VALUES
('global.help', 'Help', 'global', 'Open help documentation', 'F1', 'Global', FALSE),
('global.help_browser', 'Help Browser', 'global', 'Open help browser window', 'Ctrl + H', 'Global', TRUE),
('inventory.save', 'Inventory - Save', 'inventory', 'Save current inventory entry', 'Ctrl + S', 'Form', TRUE),
('inventory.reset', 'Inventory - Reset', 'inventory', 'Reset inventory form', 'Ctrl + R', 'Form', TRUE),
('transfer.search', 'Transfer - Search', 'transfer', 'Focus search field', 'Ctrl + F', 'Form', TRUE),
('transactions.export', 'Transactions - Export', 'transactions', 'Export to Excel', 'Ctrl + E', 'Form', TRUE);
```

---

## Stored Procedures

### **usr_shortcuts_Get**

```sql
DELIMITER //
DROP PROCEDURE IF EXISTS `usr_shortcuts_Get`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_shortcuts_Get`(
    IN p_UserId VARCHAR(255),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
    ELSE
        SELECT 
            ActionId,
            ShortcutKeys,
            IsCustomized,
            LastModified
        FROM usr_shortcuts
        WHERE UserId = p_UserId
        ORDER BY ActionId;

        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved shortcuts for user "', p_UserId, '"');
    END IF;
END
//
DELIMITER ;
```

### **usr_shortcuts_Set**

```sql
DELIMITER //
DROP PROCEDURE IF EXISTS `usr_shortcuts_Set`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_shortcuts_Set`(
    IN p_UserId VARCHAR(255),
    IN p_ActionId VARCHAR(100),
    IN p_ShortcutKeys VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE v_AllowCustomization BOOLEAN DEFAULT TRUE;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
    ELSEIF p_ActionId IS NULL OR TRIM(p_ActionId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ActionId is required';
    ELSEIF p_ShortcutKeys IS NULL OR TRIM(p_ShortcutKeys) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ShortcutKeys is required';
    ELSE
        -- Check if action exists and is customizable
        SELECT AllowCustomization INTO v_AllowCustomization
        FROM sys_shortcut_defaults
        WHERE ActionId = p_ActionId;

        IF v_AllowCustomization IS NULL THEN
            SET p_Status = -3;
            SET p_ErrorMsg = CONCAT('Action "', p_ActionId, '" not found');
        ELSEIF v_AllowCustomization = FALSE THEN
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Action "', p_ActionId, '" cannot be customized (system shortcut)');
        ELSE
            -- Insert or update shortcut
            INSERT INTO usr_shortcuts (UserId, ActionId, ShortcutKeys, IsCustomized)
            VALUES (p_UserId, p_ActionId, p_ShortcutKeys, TRUE)
            ON DUPLICATE KEY UPDATE 
                ShortcutKeys = p_ShortcutKeys,
                IsCustomized = TRUE,
                LastModified = CURRENT_TIMESTAMP;

            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Saved shortcut: ', p_ActionId, ' = ', p_ShortcutKeys);
        END IF;
    END IF;
END
//
DELIMITER ;
```

### **usr_shortcuts_Delete**

```sql
DELIMITER //
DROP PROCEDURE IF EXISTS `usr_shortcuts_Delete`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_shortcuts_Delete`(
    IN p_UserId VARCHAR(255),
    IN p_ActionId VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'UserId is required';
    ELSEIF p_ActionId IS NULL OR TRIM(p_ActionId) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ActionId is required';
    ELSE
        DELETE FROM usr_shortcuts
        WHERE UserId = p_UserId AND ActionId = p_ActionId;

        SET v_RowCount = ROW_COUNT();

        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Reset shortcut to default: ', p_ActionId);
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No customization found for: ', p_ActionId);
        END IF;
    END IF;
END
//
DELIMITER ;
```

### **usr_shortcuts_ValidateConflict**

```sql
DELIMITER //
DROP PROCEDURE IF EXISTS `usr_shortcuts_ValidateConflict`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_shortcuts_ValidateConflict`(
    IN p_UserId VARCHAR(255),
    IN p_ActionId VARCHAR(100),
    IN p_ShortcutKeys VARCHAR(50),
    OUT p_ConflictingActionId VARCHAR(100),
    OUT p_ConflictingActionName VARCHAR(200),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Category VARCHAR(50);
    DECLARE v_ConflictCount INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SET p_ConflictingActionId = NULL;
    SET p_ConflictingActionName = NULL;

    -- Get category of the action being changed
    SELECT Category INTO v_Category
    FROM sys_shortcut_defaults
    WHERE ActionId = p_ActionId;

    IF v_Category IS NULL THEN
        SET p_Status = -2;
        SET p_ErrorMsg = CONCAT('Action "', p_ActionId, '" not found');
    ELSE
        -- Check for conflicts within same category
        SELECT 
            u.ActionId,
            d.DisplayName,
            COUNT(*) INTO p_ConflictingActionId, p_ConflictingActionName, v_ConflictCount
        FROM usr_shortcuts u
        INNER JOIN sys_shortcut_defaults d ON u.ActionId = d.ActionId
        WHERE u.UserId = p_UserId
          AND u.ActionId != p_ActionId
          AND u.ShortcutKeys = p_ShortcutKeys
          AND d.Category = v_Category
        GROUP BY u.ActionId, d.DisplayName
        LIMIT 1;

        IF v_ConflictCount > 0 THEN
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('Shortcut conflicts with: ', p_ConflictingActionName);
        ELSE
            SET p_Status = 1;
            SET p_ErrorMsg = 'No conflicts found';
        END IF;
    END IF;
END
//
DELIMITER ;
```

---

## Usage Patterns

### **Pattern 1: Form with Manual Registration**

```csharp
public partial class TransactionsForm : Form
{
    public TransactionsForm()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);

        // Manual shortcut registration
        var mgr = Service_ShortcutManager.Instance;
        mgr.RegisterFormShortcut(this, "transactions.search", btnSearch_Click);
        mgr.RegisterFormShortcut(this, "transactions.reset", btnReset_Click);
        mgr.RegisterFormShortcut(this, "transactions.export", btnExport_Click);
        mgr.RegisterFormShortcut(this, "transactions.print", btnPrint_Click);

        // Apply tooltips with shortcut hints
        mgr.ApplyShortcutTooltips(this);
    }
}
```

### **Pattern 2: Form with Auto-Registration**

```csharp
public partial class InventoryForm : Form
{
    public InventoryForm()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);

        // Auto-register shortcuts based on button names
        // btnSave → inventory.save, btnReset → inventory.reset, etc.
        var mgr = Service_ShortcutManager.Instance;
        mgr.AutoRegisterFormShortcuts(this);
        mgr.ApplyShortcutTooltips(this);
    }
}
```

### **Pattern 3: Context-Aware Shortcut**

```csharp
// Only fire shortcut when not in read-only mode
var saveAction = Service_ShortcutManager.Instance.GetAction("inventory.save");
if (saveAction != null)
{
    saveAction.ContextFilter = context => !context.IsReadOnly;
}
```

### **Pattern 4: Loading User Shortcuts at Startup**

```csharp
// In Program.cs or MainForm constructor
var mgr = Service_ShortcutManager.Instance;
await mgr.LoadUserShortcutsAsync(Model_Application_Variables.User);
```

---

## Shortcuts Editor UI

### **Control_Shortcuts Refactored**

```csharp
public partial class Control_Shortcuts : UserControl
{
    private Service_ShortcutManager _manager = Service_ShortcutManager.Instance;

    public Control_Shortcuts()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        _ = LoadShortcutsAsync();
    }

    private async Task LoadShortcutsAsync()
    {
        try
        {
            Control_Shortcuts_DataGridView_Shortcuts.Columns.Clear();
            Control_Shortcuts_DataGridView_Shortcuts.Columns.Add("Category", "Category");
            Control_Shortcuts_DataGridView_Shortcuts.Columns.Add("Action", "Action");
            Control_Shortcuts_DataGridView_Shortcuts.Columns.Add("Shortcut", "Shortcut");
            Control_Shortcuts_DataGridView_Shortcuts.Columns.Add("Status", "");
            
            // Add button column for reset
            var resetColumn = new DataGridViewButtonColumn
            {
                Name = "Reset",
                HeaderText = "",
                Text = "⟲",
                UseColumnTextForButtonValue = true,
                Width = 40
            };
            Control_Shortcuts_DataGridView_Shortcuts.Columns.Add(resetColumn);

            Control_Shortcuts_DataGridView_Shortcuts.Rows.Clear();

            string user = Model_Application_Variables.User;
            await _manager.LoadUserShortcutsAsync(user);

            var actions = _manager.GetAllActions()
                .Where(a => a.AllowCustomization)
                .OrderBy(a => a.Category)
                .ThenBy(a => a.DisplayName);

            foreach (var action in actions)
            {
                Keys keys = _manager.GetKeysForAction(action.Id);
                string keysString = Helper_UI_Shortcuts.ToShortcutString(keys);
                string status = keys == action.DefaultKeys ? "" : "✓";  // Checkmark if customized

                int rowIndex = Control_Shortcuts_DataGridView_Shortcuts.Rows.Add(
                    action.Category,
                    action.DisplayName,
                    keysString,
                    status
                );

                // Store action ID in row tag
                Control_Shortcuts_DataGridView_Shortcuts.Rows[rowIndex].Tag = action.Id;
            }

            Control_Shortcuts_DataGridView_Shortcuts.Columns[0].ReadOnly = true;
            Control_Shortcuts_DataGridView_Shortcuts.Columns[1].ReadOnly = true;
            Control_Shortcuts_DataGridView_Shortcuts.Columns[2].ReadOnly = false;
            Control_Shortcuts_DataGridView_Shortcuts.Columns[3].ReadOnly = true;

            LoggingUtility.Log("[Control_Shortcuts] Loaded shortcuts into grid");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                controlName: nameof(Control_Shortcuts));
        }
    }

    private async Task SaveShortcutsAsync()
    {
        try
        {
            string user = Model_Application_Variables.User;

            for (int i = 0; i < Control_Shortcuts_DataGridView_Shortcuts.Rows.Count; i++)
            {
                DataGridViewRow row = Control_Shortcuts_DataGridView_Shortcuts.Rows[i];
                string actionId = row.Tag?.ToString() ?? string.Empty;
                string keysString = row.Cells[2].Value?.ToString() ?? string.Empty;

                if (!string.IsNullOrEmpty(actionId) && !string.IsNullOrEmpty(keysString))
                {
                    Keys keys = Helper_UI_Shortcuts.FromShortcutString(keysString);
                    await _manager.SaveUserShortcutAsync(user, actionId, keys);
                }
            }

            MessageBox.Show("Shortcuts saved successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                controlName: nameof(Control_Shortcuts));
        }
    }

    private async void ResetButton_Click(int rowIndex)
    {
        string actionId = Control_Shortcuts_DataGridView_Shortcuts.Rows[rowIndex].Tag?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(actionId)) return;

        var action = _manager.GetAction(actionId);
        if (action == null) return;

        string user = Model_Application_Variables.User;
        var result = await _manager.ResetToDefaultAsync(user, actionId);

        if (result.IsSuccess)
        {
            string defaultKeys = Helper_UI_Shortcuts.ToShortcutString(action.DefaultKeys);
            Control_Shortcuts_DataGridView_Shortcuts.Rows[rowIndex].Cells[2].Value = defaultKeys;
            Control_Shortcuts_DataGridView_Shortcuts.Rows[rowIndex].Cells[3].Value = "";  // Clear customized marker
        }
    }
}
```

---

## Testing Checklist

- [ ] Service_ShortcutManager initializes with default actions
- [ ] RegisterFormShortcut enables KeyPreview automatically
- [ ] AutoRegisterFormShortcuts finds buttons correctly
- [ ] Shortcuts don't fire when TextBox has focus
- [ ] Context filters work correctly
- [ ] User customizations persist to database
- [ ] Conflict detection blocks duplicate shortcuts in same category
- [ ] Non-customizable shortcuts (F1, Escape) can't be changed
- [ ] Tooltips show shortcut keys
- [ ] Reset to default works
- [ ] Import/export functionality works
- [ ] Cheat sheet displays all shortcuts grouped by category

---

## Migration from v1.0

See separate instruction file: `keyboard-shortcuts-refactoring.instructions.md`

---

## Related Files

- `Services/Service_ShortcutManager.cs` - Core service implementation
- `Models/Model_ShortcutAction.cs` - Action metadata model
- `Helpers/Helper_UI_Shortcuts.cs` - Key conversion utilities (reuse from v1.0)
- `Controls/SettingsForm/Control_Shortcuts.cs` - Editor UI
- `Database/UpdatedStoredProcedures/ReadyForVerification/users/usr_shortcuts_*.sql` - Database procedures
