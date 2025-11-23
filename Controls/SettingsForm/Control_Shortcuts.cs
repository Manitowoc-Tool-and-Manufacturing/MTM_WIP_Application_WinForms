using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Shortcuts : ThemedUserControl
    {
        public event EventHandler? ShortcutsUpdated;
        public event EventHandler<string>? StatusMessageChanged;
        private readonly IShortcutService? _shortcutService;

        public Control_Shortcuts()
        {
            InitializeComponent();
            _shortcutService = Program.ServiceProvider?.GetService<IShortcutService>();
            _ = LoadShortcuts();
        }

        private async Task LoadShortcuts()
        {
            try
            {
                Control_Shortcuts_DataGridView_Shortcuts.Columns.Clear();
                Control_Shortcuts_DataGridView_Shortcuts.Columns.Add("Action", "Action");
                Control_Shortcuts_DataGridView_Shortcuts.Columns.Add("Shortcut", "Shortcut");
                // Hidden column to store the internal shortcut name (key)
                Control_Shortcuts_DataGridView_Shortcuts.Columns.Add("InternalName", "InternalName");
                Control_Shortcuts_DataGridView_Shortcuts.Columns["InternalName"].Visible = false;
                
                Control_Shortcuts_DataGridView_Shortcuts.Rows.Clear();

                if (_shortcutService == null)
                {
                    StatusMessageChanged?.Invoke(this, "Error: Shortcut service not available.");
                    return;
                }

                // Ensure service is initialized for current user
                await _shortcutService.InitializeAsync(Core_WipAppVariables.User);
                var shortcuts = _shortcutService.GetAllShortcuts();

                foreach (var shortcut in shortcuts)
                {
                    // Use Description for display, Name for internal key
                    string action = !string.IsNullOrEmpty(shortcut.Description) ? shortcut.Description : shortcut.Name;
                    string shortcutValue = shortcut.DisplayString;
                    string internalName = shortcut.Name;

                    // Also update the helper for immediate effect in current session if needed
                    // (Though ideally the app should listen to service updates)
                    Helper_UI_Shortcuts.ApplyShortcutFromDictionary(action, shortcut.Keys);

                    Control_Shortcuts_DataGridView_Shortcuts.Rows.Add(action, shortcutValue, internalName);
                }

                Control_Shortcuts_DataGridView_Shortcuts.ReadOnly = false;
                Control_Shortcuts_DataGridView_Shortcuts.AllowUserToAddRows = false;
                Control_Shortcuts_DataGridView_Shortcuts.AllowUserToDeleteRows = false;
                Control_Shortcuts_DataGridView_Shortcuts.Columns[0].ReadOnly = true;
                Control_Shortcuts_DataGridView_Shortcuts.Columns[1].ReadOnly = false;
                Control_Shortcuts_DataGridView_Shortcuts.CellValueChanged += ShortcutsDataGridView_CellValueChanged;
                Control_Shortcuts_DataGridView_Shortcuts.CellValidating += ShortcutsDataGridView_CellValidating;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                StatusMessageChanged?.Invoke(this, $"Error loading shortcuts: {ex.Message}");
            }
        }

        private void ShortcutsDataGridView_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string shortcutString = e.FormattedValue?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(shortcutString))
                {
                    return;
                }

                try
                {
                    Keys keys = Helper_UI_Shortcuts.FromShortcutString(shortcutString);
                    if (keys == Keys.None && !string.IsNullOrWhiteSpace(shortcutString))
                    {
                        e.Cancel = true;
                        StatusMessageChanged?.Invoke(this, "Invalid shortcut format. Use combinations like 'CTRL + S' or 'ALT + F1'");
                    }
                }
                catch
                {
                    e.Cancel = true;
                    StatusMessageChanged?.Invoke(this, "Invalid shortcut format. Use combinations like 'CTRL + S' or 'ALT + F1'");
                }
            }
        }

        private void ShortcutsDataGridView_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                string? actionName = Control_Shortcuts_DataGridView_Shortcuts.Rows[e.RowIndex].Cells[0].Value?.ToString();
                string shortcutString =
                    Control_Shortcuts_DataGridView_Shortcuts.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";

                if (!string.IsNullOrEmpty(actionName))
                {
                    try
                    {
                        Keys keys = Helper_UI_Shortcuts.FromShortcutString(shortcutString);
                        Helper_UI_Shortcuts.ApplyShortcutFromDictionary(actionName, keys);
                        ShortcutsUpdated?.Invoke(this, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                        StatusMessageChanged?.Invoke(this, $"Error updating shortcut: {ex.Message}");
                    }
                }
            }
        }

        private void ShortcutsDataGridView_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                e.Cancel = true;

                string? actionName = Control_Shortcuts_DataGridView_Shortcuts.Rows[e.RowIndex].Cells[0].Value?.ToString();
                string currentShortcut =
                    Control_Shortcuts_DataGridView_Shortcuts.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                string? internalName = Control_Shortcuts_DataGridView_Shortcuts.Rows[e.RowIndex].Cells[2].Value?.ToString();

                using (Form inputForm = new())
                {
                    inputForm.Text = $"Set Shortcut for '{actionName}'";
                    inputForm.Size = new Size(400, 180);
                    inputForm.StartPosition = FormStartPosition.CenterParent;
                    inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    inputForm.MaximizeBox = false;
                    inputForm.MinimizeBox = false;

                    Label label = new()
                    {
                        Text = "Press the new shortcut key combination:",
                        Location = new Point(10, 20),
                        Size = new Size(360, 20)
                    };
                    TextBox shortcutBox = new()
                    {
                        Location = new Point(10, 45),
                        Size = new Size(360, 20),
                        ReadOnly = true,
                        TabStop = false,
                        BackColor = SystemColors.Control,
                        ForeColor = SystemColors.GrayText
                    };
                    shortcutBox.Text = currentShortcut;

                    Label errorLabel = new()
                    {
                        Text = "",
                        ForeColor = Color.Red,
                        Location = new Point(10, 70),
                        Size = new Size(360, 30),
                        Visible = false
                    };

                    Keys newKeys = Helper_UI_Shortcuts.FromShortcutString(currentShortcut);

                    inputForm.KeyPreview = true;
                    inputForm.KeyDown += (s, ke) =>
                    {
                        if (ke.KeyCode == Keys.Escape)
                        {
                            inputForm.DialogResult = DialogResult.Cancel;
                            inputForm.Close();
                            return;
                        }

                        if (ke.KeyCode == Keys.ControlKey || ke.KeyCode == Keys.ShiftKey || ke.KeyCode == Keys.Menu)
                        {
                            return;
                        }

                        newKeys = ke.KeyData;
                        shortcutBox.Text = Helper_UI_Shortcuts.ToShortcutString(newKeys);

                        if (newKeys == Keys.None)
                        {
                            errorLabel.Text = "Shortcut cannot be empty.";
                            errorLabel.Visible = true;
                        }
                        else if (IsShortcutConflict(actionName, newKeys))
                        {
                            errorLabel.Text = "This shortcut is already assigned to another action in the same tab.";
                            errorLabel.Visible = true;
                        }
                        else
                        {
                            errorLabel.Text = "";
                            errorLabel.Visible = false;
                        }

                        ke.SuppressKeyPress = true;
                    };

                    Button okButton = new() { Text = "OK", Location = new Point(215, 110), Size = new Size(75, 23) };
                    Button Control_Shortcuts_Button_Cancel = new()
                    {
                        Text = "Cancel",
                        Location = new Point(295, 110),
                        Size = new Size(75, 23)
                    };

                    okButton.Click += (s, args) =>
                    {
                        if (newKeys == Keys.None)
                        {
                            errorLabel.Text = "Shortcut cannot be empty.";
                            errorLabel.Visible = true;
                            return;
                        }

                        if (IsShortcutConflict(actionName, newKeys))
                        {
                            errorLabel.Text = "This shortcut is already assigned to another action in the same tab.";
                            errorLabel.Visible = true;
                            return;
                        }

                        errorLabel.Text = "";
                        errorLabel.Visible = false;
                        inputForm.DialogResult = DialogResult.OK;
                        inputForm.Close();
                    };
                    Control_Shortcuts_Button_Cancel.Click += (s, args) =>
                    {
                        inputForm.DialogResult = DialogResult.Cancel;
                        inputForm.Close();
                    };

                    inputForm.Controls.AddRange(new Control[]
                    {
                label, shortcutBox, errorLabel, okButton, Control_Shortcuts_Button_Cancel
                    });
                    inputForm.AcceptButton = okButton;
                    inputForm.CancelButton = Control_Shortcuts_Button_Cancel;

                    if (inputForm.ShowDialog(this) == DialogResult.OK)
                    {
                        string newShortcut = shortcutBox.Text.Trim();
                        Control_Shortcuts_DataGridView_Shortcuts.Rows[e.RowIndex].Cells[1].Value = newShortcut;
                        if (!string.IsNullOrEmpty(actionName))
                        {
                            Helper_UI_Shortcuts.ApplyShortcutFromDictionary(actionName, newKeys);
                        }

                        ShortcutsUpdated?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        private bool IsShortcutConflict(string? actionName, Keys newKeys)
        {
            if (string.IsNullOrEmpty(actionName) || newKeys == Keys.None)
            {
                return false;
            }

            string group = GetShortcutGroup(actionName);

            for (int i = 0; i < Control_Shortcuts_DataGridView_Shortcuts.Rows.Count; i++)
            {
                string? otherAction = Control_Shortcuts_DataGridView_Shortcuts.Rows[i].Cells[0].Value?.ToString();
                if (otherAction == actionName)
                {
                    continue;
                }

                if (GetShortcutGroup(otherAction) != group)
                {
                    continue;
                }

                string shortcutString = Control_Shortcuts_DataGridView_Shortcuts.Rows[i].Cells[1].Value?.ToString() ?? "";
                Keys otherKeys = Helper_UI_Shortcuts.FromShortcutString(shortcutString);
                if (otherKeys == newKeys)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetShortcutGroup(string? actionName)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                return "";
            }

            if (actionName.StartsWith("Inventory"))
            {
                return "Inventory";
            }

            if (actionName.StartsWith("Advanced Inventory MultiLoc"))
            {
                return "AdvancedInventoryMultiLoc";
            }

            if (actionName.StartsWith("Advanced Inventory Import"))
            {
                return "AdvancedInventoryImport";
            }

            if (actionName.StartsWith("Advanced Inventory"))
            {
                return "AdvancedInventory";
            }

            if (actionName.StartsWith("Remove"))
            {
                return "Remove";
            }

            if (actionName.StartsWith("Transfer"))
            {
                return "Transfer";
            }

            return "";
        }

        private async Task Control_Shortcuts_Button_Save_Click(object sender, EventArgs e)
        {
         
            try
            {
                if (Control_Shortcuts_DataGridView_Shortcuts.IsCurrentCellInEditMode)
                {
                    Control_Shortcuts_DataGridView_Shortcuts.EndEdit();
                }

                if (_shortcutService == null)
                {
                    throw new Exception("Shortcut service not available.");
                }

                for (int i = 0; i < Control_Shortcuts_DataGridView_Shortcuts.Rows.Count; i++)
                {
                    DataGridViewRow row = Control_Shortcuts_DataGridView_Shortcuts.Rows[i];
                    string? actionName = row.Cells[0].Value?.ToString();
                    string shortcutString = row.Cells[1].Value?.ToString() ?? "";
                    string? internalName = row.Cells[2].Value?.ToString();

                    if (!string.IsNullOrEmpty(internalName))
                    {
                        Keys keys = Helper_UI_Shortcuts.FromShortcutString(shortcutString);
                        
                        // Update via service (persists to DB)
                        await _shortcutService.UpdateShortcutAsync(internalName, keys);
                        
                        // Update helper for immediate effect
                        if (!string.IsNullOrEmpty(actionName))
                        {
                            Helper_UI_Shortcuts.ApplyShortcutFromDictionary(actionName, keys);
                        }
                    }
                }

                ShortcutsUpdated?.Invoke(this, EventArgs.Empty);
                StatusMessageChanged?.Invoke(this, "Shortcuts saved successfully.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                StatusMessageChanged?.Invoke(this, $"Error saving shortcuts: {ex.Message}");
            }
        }
    }
}
