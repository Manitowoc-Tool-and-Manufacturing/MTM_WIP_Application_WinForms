using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
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
            
            // Wire up reset button
            Control_Shortcuts_Button_Reset.Click += Control_Shortcuts_Button_Reset_Click;
            
            _ = LoadShortcuts();
        }

        private async Task LoadShortcuts()
        {
            try
            {
                Control_Shortcuts_FlowLayout_Cards.Controls.Clear();
                Control_Shortcuts_FlowLayout_Cards.SuspendLayout();

                if (_shortcutService == null)
                {
                    StatusMessageChanged?.Invoke(this, "Error: Shortcut service not available.");
                    return;
                }

                // Ensure service is initialized for current user
                await _shortcutService.InitializeAsync(Core_WipAppVariables.User);
                var shortcuts = _shortcutService.GetAllShortcuts();

                // Group shortcuts by category
                var groupedShortcuts = shortcuts
                    .GroupBy(s => GetShortcutGroup(s.Name))
                    .OrderBy(g => g.Key);

                foreach (var group in groupedShortcuts)
                {
                    var card = CreateCategoryCard(group.Key, group.ToList());
                    Control_Shortcuts_FlowLayout_Cards.Controls.Add(card);
                }

                Control_Shortcuts_FlowLayout_Cards.ResumeLayout();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                StatusMessageChanged?.Invoke(this, $"Error loading shortcuts: {ex.Message}");
            }
        }

        private Control_SettingsCollapsibleCard CreateCategoryCard(string category, List<Model_Shortcut> shortcuts)
        {
            var card = new Control_SettingsCollapsibleCard
            {
                CardTitle = GetCategoryDisplayName(category),
                CardDescription = GetCategoryDescription(category),
                CardIcon = GetCategoryIcon(category),
                Width = Control_Shortcuts_FlowLayout_Cards.Width - 25, // Account for scrollbar
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            // Create a panel for the content to ensure proper layout
            var contentPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 3,
                RowCount = shortcuts.Count,
                Padding = new Padding(0, 5, 0, 5)
            };
            
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Action Name
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F)); // Shortcut Key
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Button

            int row = 0;
            foreach (var shortcut in shortcuts)
            {
                string actionName = !string.IsNullOrEmpty(shortcut.Description) ? shortcut.Description : shortcut.Name;
                
                var lblAction = new Label
                {
                    Text = actionName,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 9.5F)
                };

                var lblShortcut = new Label
                {
                    Text = shortcut.DisplayString,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Consolas", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(240, 240, 240),
                    Padding = new Padding(5)
                };

                var btnChange = new Button
                {
                    Text = "Change",
                    AutoSize = true,
                    Tag = shortcut
                };
                btnChange.Click += async (s, e) => await HandleChangeShortcut(shortcut);

                contentPanel.Controls.Add(lblAction, 0, row);
                contentPanel.Controls.Add(lblShortcut, 1, row);
                contentPanel.Controls.Add(btnChange, 2, row);
                
                row++;
            }

            card.AddContent(contentPanel);
            return card;
        }

        private async Task HandleChangeShortcut(Model_Shortcut shortcut)
        {
            using (var form = new Form_ShortcutEdit(shortcut.Name, shortcut.Keys))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        if (_shortcutService != null)
                        {
                            // Call service to update - it handles validation
                            var result = await _shortcutService.UpdateShortcutAsync(shortcut.Name, form.SelectedKeys);
                            
                            if (!result.IsSuccess)
                            {
                                Service_ErrorHandler.ShowWarning(result.ErrorMessage, "Shortcut Update Failed");
                                return;
                            }
                            
                            // Refresh UI
                            await LoadShortcuts();
                            
                            ShortcutsUpdated?.Invoke(this, EventArgs.Empty);
                            StatusMessageChanged?.Invoke(this, $"Shortcut for '{shortcut.Description}' updated.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(Control_Shortcuts));
                    }
                }
            }
        }

        private async void Control_Shortcuts_Button_Reset_Click(object? sender, EventArgs e)
        {
            if (Service_ErrorHandler.ShowConfirmation("Are you sure you want to reset all shortcuts to their default values?", "Reset Shortcuts") == DialogResult.Yes)
            {
                try
                {
                    if (_shortcutService != null)
                    {
                        await _shortcutService.ResetToDefaultsAsync();
                        await LoadShortcuts();
                        ShortcutsUpdated?.Invoke(this, EventArgs.Empty);
                        StatusMessageChanged?.Invoke(this, "All shortcuts reset to defaults.");
                    }
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    StatusMessageChanged?.Invoke(this, $"Error resetting shortcuts: {ex.Message}");
                }
            }
        }

        private string GetShortcutGroup(string actionName)
        {
            if (string.IsNullOrEmpty(actionName)) return "General";
            if (actionName.StartsWith("Inventory")) return "Inventory";
            if (actionName.StartsWith("Remove")) return "Remove";
            if (actionName.StartsWith("Transfer")) return "Transfer";
            if (actionName.StartsWith("Advanced Inventory")) return "Advanced Inventory";
            if (actionName.StartsWith("Advanced Remove")) return "Advanced Remove";
            if (actionName.StartsWith("QuickButton")) return "Quick Buttons";
            if (actionName.StartsWith("Shortcut_MainForm")) return "Navigation";
            if (actionName.StartsWith("Shortcut_Transactions")) return "Transactions";
            
            return "General";
        }

        private string GetCategoryDisplayName(string category)
        {
            return category switch
            {
                "Inventory" => "Inventory Tab",
                "Remove" => "Remove Tab",
                "Transfer" => "Transfer Tab",
                "Advanced Inventory" => "Advanced Inventory",
                "Advanced Remove" => "Advanced Remove",
                "Quick Buttons" => "Quick Buttons (Alt+0-9)",
                "Navigation" => "Navigation",
                "Transactions" => "Transactions Viewer",
                _ => category
            };
        }

        private string GetCategoryDescription(string category)
        {
            return category switch
            {
                "Inventory" => "Shortcuts for the main Inventory tab operations.",
                "Remove" => "Shortcuts for the Remove tab operations.",
                "Transfer" => "Shortcuts for the Transfer tab operations.",
                "Advanced Inventory" => "Shortcuts for Advanced Inventory management.",
                "Advanced Remove" => "Shortcuts for Advanced Remove operations.",
                "Quick Buttons" => "Customizable shortcuts for the Quick Button bar.",
                "Navigation" => "Global navigation shortcuts.",
                "Transactions" => "Shortcuts for the Transaction Viewer.",
                _ => "General application shortcuts."
            };
        }

        private string GetCategoryIcon(string category)
        {
            return category switch
            {
                "Inventory" => "📦",
                "Remove" => "🗑️",
                "Transfer" => "↔️",
                "Advanced Inventory" => "🏭",
                "Advanced Remove" => "📤",
                "Quick Buttons" => "⚡",
                "Navigation" => "🧭",
                "Transactions" => "📝",
                _ => "⚙️"
            };
        }
    }
}
