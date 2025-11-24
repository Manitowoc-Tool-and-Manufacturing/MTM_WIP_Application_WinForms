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
        public event EventHandler? RequestNavigationHome;
        private readonly IShortcutService? _shortcutService;

        public Control_Shortcuts()
        {
            InitializeComponent();
            _shortcutService = Program.ServiceProvider?.GetService<IShortcutService>();

            // Wire up buttons
            Control_Shortcuts_Button_Reset.Click += Control_Shortcuts_Button_Reset_Click;
            Control_Shortcuts_Button_Home.Click += (s, e) => RequestNavigationHome?.Invoke(this, EventArgs.Empty);

            _ = LoadShortcuts();
        }

        private async Task LoadShortcuts()
        {
            try
            {
                // Clear existing content in all cards
                Control_Shortcuts_Card_Inventory.ClearContent();
                Control_Shortcuts_Card_Remove.ClearContent();
                Control_Shortcuts_Card_Transfer.ClearContent();
                Control_Shortcuts_Card_AdvancedInventory.ClearContent();
                Control_Shortcuts_Card_AdvancedRemove.ClearContent();
                Control_Shortcuts_Card_QuickButtons.ClearContent();
                Control_Shortcuts_Card_Navigation.ClearContent();
                Control_Shortcuts_Card_Transactions.ClearContent();
                Control_Shortcuts_Card_General.ClearContent();

                if (_shortcutService == null)
                {
                    StatusMessageChanged?.Invoke(this, "Error: Shortcut service not available.");
                    return;
                }

                // Ensure service is initialized for current user
                await _shortcutService.InitializeAsync(Core_WipAppVariables.User);
                var shortcuts = _shortcutService.GetAllShortcuts();

                LoggingUtility.Log($"[Control_Shortcuts] Loaded {shortcuts.Count} shortcuts from service.");

                // Group shortcuts by category
                var groupedShortcuts = shortcuts
                    .GroupBy(s => GetShortcutCategory(s))
                    .OrderBy(g => g.Key);

                var populatedCards = new HashSet<Control_SettingsCollapsibleCard>();

                foreach (var group in groupedShortcuts)
                {
                    LoggingUtility.Log($"[Control_Shortcuts] Processing category group: '{group.Key}' with {group.Count()} shortcuts.");
                    var card = GetCardForCategory(group.Key);
                    if (card != null)
                    {
                        PopulateCard(card, group.ToList());
                        populatedCards.Add(card);
                    }
                    else
                    {
                        LoggingUtility.Log($"[Control_Shortcuts] Warning: No card found for category '{group.Key}'");
                    }
                }

                // Ensure all cards are sized correctly and visibility is set
                foreach (Control ctrl in Control_Shortcuts_FlowLayoutPanel_Cards.Controls)
                {
                    if (ctrl is Control_SettingsCollapsibleCard card)
                    {
                        if (populatedCards.Contains(card))
                        {
                            card.Visible = true;
                            // Width is handled by Dock=Fill in TableLayout
                            card.AdjustHeight();
                        }
                        else
                        {
                            card.Visible = false;
                        }
                    }
                }

                // Force layout update
                Control_Shortcuts_FlowLayoutPanel_Cards.PerformLayout();
                Control_Shortcuts_Panel_ScrollContainer.PerformLayout();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                StatusMessageChanged?.Invoke(this, $"Error loading shortcuts: {ex.Message}");
            }
        }

        private Control_SettingsCollapsibleCard? GetCardForCategory(string category)
        {
            return category switch
            {
                "Inventory" => Control_Shortcuts_Card_Inventory,
                "Remove" => Control_Shortcuts_Card_Remove,
                "Transfer" => Control_Shortcuts_Card_Transfer,
                "Advanced Inventory" => Control_Shortcuts_Card_AdvancedInventory,
                "Advanced Remove" => Control_Shortcuts_Card_AdvancedRemove,
                "Quick Buttons" => Control_Shortcuts_Card_QuickButtons,
                "Navigation" => Control_Shortcuts_Card_Navigation,
                "Transactions" => Control_Shortcuts_Card_Transactions,
                _ => Control_Shortcuts_Card_General
            };
        }

        private void PopulateCard(Control_SettingsCollapsibleCard card, List<Model_Shortcut> shortcuts)
        {
            // Create a panel for the content to ensure proper layout
            var contentPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 3,
                RowCount = shortcuts.Count + 1,
                Padding = new Padding(0),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                BackColor = Color.White
            };

            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Action Name
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F)); // Shortcut Key
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Button

            // Header Row
            contentPanel.Controls.Add(CreateHeaderLabel("Action"), 0, 0);
            contentPanel.Controls.Add(CreateHeaderLabel("Shortcut"), 1, 0);
            contentPanel.Controls.Add(CreateHeaderLabel("Edit"), 2, 0);

            int row = 1;
            foreach (var shortcut in shortcuts)
            {
                string actionName = !string.IsNullOrEmpty(shortcut.Description) ? shortcut.Description : shortcut.Name;

                var lblAction = new Label
                {
                    Text = actionName,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 8.5F),
                    BackColor = Color.Transparent,
                    Padding = new Padding(2)
                };

                var lblShortcut = new Label
                {
                    Text = shortcut.DisplayString,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Consolas", 9F, FontStyle.Bold),
                    BackColor = Color.Transparent,
                    Padding = new Padding(2)
                };

                var btnChange = new Button
                {
                    Text = "...",
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Tag = shortcut,
                    Dock = DockStyle.Fill,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8F)
                };
                btnChange.FlatAppearance.BorderSize = 0;
                btnChange.Click += async (s, e) => await HandleChangeShortcut(shortcut);

                contentPanel.Controls.Add(lblAction, 0, row);
                contentPanel.Controls.Add(lblShortcut, 1, row);
                contentPanel.Controls.Add(btnChange, 2, row);

                row++;
            }

            card.AddContent(contentPanel);
        }

        private Label CreateHeaderLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(2)
            };
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

        private static readonly Dictionary<string, string> _shortcutCategoryMap = new()
        {
            { "Shortcut_AdvInv_Import_ImportExcel", "AdvancedInventory" },
            { "Shortcut_AdvInv_Import_Normal", "AdvancedInventory" },
            { "Shortcut_AdvInv_Import_OpenExcel", "AdvancedInventory" },
            { "Shortcut_AdvInv_Import_Save", "AdvancedInventory" },
            { "Shortcut_AdvInv_Multi_Normal", "AdvancedInventory" },
            { "Shortcut_AdvInv_Multi_AddLoc", "AdvancedInventory" },
            { "Shortcut_AdvInv_Multi_Reset", "AdvancedInventory" },
            { "Shortcut_AdvInv_Multi_SaveAll", "AdvancedInventory" },
            { "Shortcut_AdvInv_Normal", "AdvancedInventory" },
            { "Shortcut_AdvInv_Reset", "AdvancedInventory" },
            { "Shortcut_AdvInv_Save", "AdvancedInventory" },
            { "Shortcut_AdvInv_Send", "AdvancedInventory" },
            { "Shortcut_Remove_Delete", "Remove" },
            { "Shortcut_Remove_Normal", "Remove" },
            { "Shortcut_Remove_Reset", "AdvancedRemove" },
            { "Shortcut_Remove_Search", "AdvancedRemove" },
            { "Shortcut_Remove_Undo", "Remove" },
            { "Shortcut_Inventory_Advanced", "Inventory" },
            { "Shortcut_Inventory_Reset", "Inventory" },
            { "Shortcut_Inventory_Save", "Inventory" },
            { "Shortcut_Inventory_ToggleRightPanel_Right", "Inventory" },
            { "Shortcut_Inventory_ToggleRightPanel_Left", "Inventory" },
            { "Shortcut_Transfer_Reset", "Transfer" },
            { "Shortcut_Transfer_Search", "Transfer" },
            { "Shortcut_Transfer_ToggleRightPanel_Right", "Transfer" },
            { "Shortcut_Transfer_ToggleRightPanel_Left", "Transfer" },
            { "Shortcut_Transfer_Transfer", "Transfer" },
            { "Shortcut_MainForm_Tab1", "MainForm" },
            { "Shortcut_MainForm_Tab2", "MainForm" },
            { "Shortcut_MainForm_Tab3", "MainForm" },
            { "Shortcut_QuickButton_01", "QuickButtons" },
            { "Shortcut_QuickButton_02", "QuickButtons" },
            { "Shortcut_QuickButton_03", "QuickButtons" },
            { "Shortcut_QuickButton_04", "QuickButtons" },
            { "Shortcut_QuickButton_05", "QuickButtons" },
            { "Shortcut_QuickButton_06", "QuickButtons" },
            { "Shortcut_QuickButton_07", "QuickButtons" },
            { "Shortcut_QuickButton_08", "QuickButtons" },
            { "Shortcut_QuickButton_09", "QuickButtons" },
            { "Shortcut_QuickButton_10", "QuickButtons" }
        };

        private string GetShortcutCategory(Model_Shortcut shortcut)
        {
            string category = shortcut.Category;

            // Override from static map if available (fixes wrong/missing categories in DB)
            if (_shortcutCategoryMap.TryGetValue(shortcut.Name, out var mappedCategory))
            {
                category = mappedCategory;
            }

            if (!string.IsNullOrEmpty(category))
            {
                return category switch
                {
                    "AdvancedInventory" => "Advanced Inventory",
                    "AdvancedRemove" => "Advanced Remove",
                    "QuickButtons" => "Quick Buttons",
                    "MainForm" => "Navigation",
                    _ => category // Inventory, Remove, Transfer, Transactions match directly
                };
            }

            // Fallback: Parse from Name if Category is missing
            string name = shortcut.Name;
            if (string.IsNullOrEmpty(name)) return "General";

            // Normalize name to check categories
            if (name.StartsWith("Shortcut_")) name = name.Substring(9);

            if (name.StartsWith("Inventory")) return "Inventory";
            if (name.StartsWith("Remove")) return "Remove";
            if (name.StartsWith("Transfer")) return "Transfer";
            if (name.StartsWith("AdvancedInventory") || name.StartsWith("Advanced Inventory") || name.StartsWith("AdvInv")) return "Advanced Inventory";
            if (name.StartsWith("AdvancedRemove") || name.StartsWith("Advanced Remove")) return "Advanced Remove";
            if (name.StartsWith("QuickButton")) return "Quick Buttons";
            if (name.StartsWith("MainForm") || name.StartsWith("Navigation")) return "Navigation";
            if (name.StartsWith("Transactions")) return "Transactions";

            LoggingUtility.Log($"[Control_Shortcuts] Warning: Could not categorize shortcut '{shortcut.Name}'. Defaulting to General.");
            return "General";
        }

        private void Control_Shortcuts_Card_Transfer_Load(object sender, EventArgs e)
        {

        }

        private void Control_Shortcuts_TableLayout_ScrollContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
