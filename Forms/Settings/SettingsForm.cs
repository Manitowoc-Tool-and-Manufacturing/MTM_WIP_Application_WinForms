using System.Reflection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Forms.Help;

namespace MTM_WIP_Application_Winforms.Forms.Settings
{
    public partial class SettingsForm : ThemedForm
    {
        #region Fields

        public bool HasChanges = false;
        private readonly Dictionary<string, Panel> _settingsPanels;

        #endregion

        #region Constructors

        public SettingsForm()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["FormType"] = nameof(SettingsForm),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(SettingsForm), nameof(SettingsForm));

            Service_DebugTracer.TraceUIAction("SETTINGS_FORM_INITIALIZATION", nameof(SettingsForm),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "SettingsForm"
                });

            InitializeComponent();
            InitializeHelpButtons();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            AutoScaleMode = AutoScaleMode.Dpi;
            Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(SettingsForm),
                new Dictionary<string, object>
                {
                    ["DpiScaling"] = "APPLIED",
                    ["LayoutAdjustments"] = "APPLIED"
                });

            Service_DebugTracer.TraceUIAction("SETTINGS_PANELS_INITIALIZATION", nameof(SettingsForm),
                new Dictionary<string, object>
                {
                    ["PanelsCount"] = 18,
                    ["PanelTypes"] = new[] { "Database", "User Management", "Part Management", "Operations", "Locations", "ItemTypes", "Theme", "About" }
                });

            _settingsPanels = new Dictionary<string, Panel>
            {
                ["🏠 Back to Home"] = SettingsForm_Panel_Home,
                ["Database"] = SettingsForm_Panel_Database,
                ["User Management"] = SettingsForm_Panel_AddUser,
                ["Part Numbers"] = SettingsForm_Panel_PartNumbers,
                ["Operations"] = SettingsForm_Panel_AddOperation,
                ["Locations"] = SettingsForm_Panel_AddLocation,
                ["ItemTypes"] = SettingsForm_Panel_AddItemType,
                ["Theme"] = SettingsForm_Panel_Theme,
                ["Shortcuts"] = SettingsForm_Panel_Shortcuts,
                ["About"] = SettingsForm_Panel_About
            };

            Service_DebugTracer.TraceUIAction("SETTINGS_FORM_INITIALIZATION", nameof(SettingsForm),
                new Dictionary<string, object>
                {
                    ["Phase"] = "COMPLETE",
                    ["Success"] = true,
                    ["HasChanges"] = HasChanges
                });

            Service_DebugTracer.TraceMethodExit(null, nameof(SettingsForm), nameof(SettingsForm));
        }

        #endregion

        #region Methods

        private void InitializeCategoryTreeView()
        {
            // Temporarily unhook the AfterSelect event to prevent navigation during rebuild
            SettingsForm_TreeView_Category.AfterSelect -= CategoryTreeView_AfterSelect;
            
            SettingsForm_TreeView_Category.Nodes.Clear();

            TreeNode homeNode = SettingsForm_TreeView_Category.Nodes.Add("🏠 Back to Home", "🏠 Back to Home");
            TreeNode databaseNode = SettingsForm_TreeView_Category.Nodes.Add("Database", "Database");

            SettingsForm_TreeView_Category.Nodes.Add("User Management", "User Management");

            SettingsForm_TreeView_Category.Nodes.Add("Part Numbers", "Part Numbers");

            SettingsForm_TreeView_Category.Nodes.Add("Operations", "Operations");

            SettingsForm_TreeView_Category.Nodes.Add("Locations", "Locations");

            SettingsForm_TreeView_Category.Nodes.Add("ItemTypes", "ItemTypes");

            TreeNode themeNode = SettingsForm_TreeView_Category.Nodes.Add("Theme", "Theme");
            TreeNode shortcutsNode = SettingsForm_TreeView_Category.Nodes.Add("Shortcuts", "Shortcuts");
            TreeNode aboutNode = SettingsForm_TreeView_Category.Nodes.Add("About", "About");

            SettingsForm_TreeView_Category.CollapseAll();

            // Re-hook the AfterSelect event after initialization is complete
            SettingsForm_TreeView_Category.AfterSelect += CategoryTreeView_AfterSelect;
            
            // Don't auto-select any node - homepage is the default view
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Service_DebugTracer.TraceUIAction("INITIALIZE_FORM", nameof(SettingsForm),
                new Dictionary<string, object> { ["Phase"] = "START" });

            foreach (Panel panel in _settingsPanels.Values)
            {
                panel.Visible = false;
            }

            InitializeCategoryTreeView();
            ShowPanel("🏠 Back to Home");

            // Wire up events
            SettingsForm_Control_Home.NavigationRequested += (s, args) =>
            {
                ShowPanel(args.Target);
                SelectTreeNodeByName(args.Target);
            };
            this.Shown += (s, args) => SettingsForm_Control_Home.InitializeCategories();

            SettingsForm_Control_Shortcuts.ShortcutsUpdated += (s, args) =>
            {
                UpdateStatus("Shortcuts updated successfully.");
                HasChanges = true;
            };
            SettingsForm_Control_Shortcuts.StatusMessageChanged += (s, message) => { UpdateStatus(message); };
            SettingsForm_Control_Shortcuts.RequestNavigationHome += (_, _) => ShowPanel("🏠 Back to Home");

            SettingsForm_Control_Theme.ThemeChanged += (s, args) =>
            {
                UpdateStatus("Theme changed successfully.");
                HasChanges = true;
            };
            SettingsForm_Control_Theme.StatusMessageChanged += (s, message) => { UpdateStatus(message); };
            SettingsForm_Control_Theme.BackToHomeRequested += (_, _) => ShowPanel("🏠 Back to Home");

            SettingsForm_Control_Database.DatabaseSettingsUpdated += (s, args) =>
            {
                UpdateStatus("Database settings updated successfully.");
                HasChanges = true;
            };
            SettingsForm_Control_Database.StatusMessageChanged += (s, message) => { UpdateStatus(message); };
            SettingsForm_Control_Database.BackToHomeRequested += (_, _) => ShowPanel("🏠 Back to Home");

            SettingsForm_Control_UserManagement.BackToHomeRequested += (_, _) => ShowPanel("🏠 Back to Home");
            SettingsForm_Control_UserManagement.UserListChanged += (_, _) =>
            {
                UpdateStatus("User list updated successfully.");
                HasChanges = true;
            };

            SettingsForm_Control_PartManagement.PartListChanged += (_, _) =>
            {
                UpdateStatus("Part numbers updated successfully.");
                HasChanges = true;
            };
            SettingsForm_Control_PartManagement.BackToHomeRequested += (_, _) => ShowPanel("🏠 Back to Home");

            SettingsForm_Control_OperationManagement.SetProgressControls(SettingsForm_ProgressBar, SettingsForm_StatusText);
            SettingsForm_Control_OperationManagement.OperationListChanged += (_, _) =>
            {
                UpdateStatus("Operations updated successfully.");
                HasChanges = true;
            };
            SettingsForm_Control_OperationManagement.StatusMessageChanged += (_, message) => UpdateStatus(message);
            SettingsForm_Control_OperationManagement.BackToHomeRequested += (_, _) => ShowPanel("🏠 Back to Home");
            SettingsForm_Panel_EditOperation.Visible = false;
            SettingsForm_Panel_RemoveOperation.Visible = false;

            SettingsForm_Control_LocationManagement.LocationListChanged += (_, _) =>
            {
                UpdateStatus("Locations updated successfully.");
                HasChanges = true;
            };
            SettingsForm_Control_LocationManagement.BackToHomeRequested += (_, _) => ShowPanel("🏠 Back to Home");
            SettingsForm_Panel_EditLocation.Visible = false;
            SettingsForm_Panel_RemoveLocation.Visible = false;

            SettingsForm_Control_ItemTypeManagement.SetProgressControls(SettingsForm_ProgressBar, SettingsForm_StatusText);
            SettingsForm_Control_ItemTypeManagement.ItemTypeListChanged += (_, _) =>
            {
                UpdateStatus("ItemTypes updated successfully.");
                HasChanges = true;
            };
            SettingsForm_Control_ItemTypeManagement.StatusMessageChanged += (_, message) => UpdateStatus(message);
            SettingsForm_Control_ItemTypeManagement.BackToHomeRequested += (_, _) => ShowPanel("🏠 Back to Home");
            SettingsForm_Panel_EditItemType.Visible = false;
            SettingsForm_Panel_RemoveItemType.Visible = false;

            SettingsForm_Control_About.BackToHomeRequested += (_, _) => ShowPanel("🏠 Back to Home");

            ApplyPrivileges();
        }

        private void ApplyPrivileges()
        {
            bool isDeveloper = Model_Application_Variables.UserTypeDeveloper;
            bool isAdmin = Model_Application_Variables.UserTypeAdmin;
            bool isNormal = Model_Application_Variables.UserTypeNormal;
            bool isReadOnly = Model_Application_Variables.UserTypeReadOnly;

            // Developers have all Admin privileges plus developer tools
            bool hasAdminAccess = isDeveloper || isAdmin;

            // Rebuild tree to ensure all nodes are present before hiding
            InitializeCategoryTreeView();

            // Helper to find a node by path (root or child)
            TreeNode? FindNodeByPath(params string[] path)
            {
                TreeNodeCollection nodes = SettingsForm_TreeView_Category.Nodes;
                TreeNode? node = null;
                foreach (string name in path)
                {
                    node = (node == null ? nodes.Cast<TreeNode>() : node.Nodes.Cast<TreeNode>()).FirstOrDefault(n =>
                        n.Name == name);
                    if (node == null)
                    {
                        break;
                    }
                }

                return node;
            }

            // Helper to hide a node by path
            void HideNode(params string[] path)
            {
                TreeNode? node = FindNodeByPath(path);
                if (node != null)
                {
                    if (node.Parent == null)
                    {
                        SettingsForm_TreeView_Category.Nodes.Remove(node);
                    }
                    else
                    {
                        node.Parent.Nodes.Remove(node);
                    }
                }
            }

            bool canAddParts = hasAdminAccess || isNormal;
            bool canEditParts = hasAdminAccess;
            bool canRemoveParts = hasAdminAccess;

            SettingsForm_Control_PartManagement.ApplyPrivileges(canAddParts, canEditParts, canRemoveParts);

            if (!canAddParts && !canEditParts && !canRemoveParts)
            {
                HideNode("Part Numbers");
            }

            // ItemTypes privileges (same as Parts for now, or adjust as needed)
            bool canAddItemTypes = hasAdminAccess || isNormal;
            bool canEditItemTypes = hasAdminAccess;
            bool canRemoveItemTypes = hasAdminAccess;

            SettingsForm_Control_ItemTypeManagement.ApplyPrivileges(canAddItemTypes, canEditItemTypes, canRemoveItemTypes);

            if (!canAddItemTypes && !canEditItemTypes && !canRemoveItemTypes)
            {
                HideNode("ItemTypes");
            }

            // Operations privileges
            bool canAddOperations = hasAdminAccess || isNormal;
            bool canEditOperations = hasAdminAccess;
            bool canRemoveOperations = hasAdminAccess;

            SettingsForm_Control_OperationManagement.ApplyPrivileges(canAddOperations, canEditOperations, canRemoveOperations);

            if (!canAddOperations && !canEditOperations && !canRemoveOperations)
            {
                HideNode("Operations");
            }

            // Locations privileges
            bool canAddLocations = hasAdminAccess || isNormal;
            bool canEditLocations = hasAdminAccess;
            bool canRemoveLocations = hasAdminAccess;

            SettingsForm_Control_LocationManagement.ApplyPrivileges(canAddLocations, canEditLocations, canRemoveLocations);

            if (!canAddLocations && !canEditLocations && !canRemoveLocations)
            {
                HideNode("Locations");
            }

            if (hasAdminAccess)
            {
                // All nodes shown for Admin and Developer
                return;
            }

            if (isNormal)
            {
                HideNode("Database");
                HideNode("User Management");
            }

            if (isReadOnly)
            {
                HideNode("Database");
                HideNode("User Management");
                HideNode("Part Numbers");
                HideNode("Operations");
                HideNode("Locations");
                HideNode("ItemTypes");
                HideNode("Shortcuts");
            }
        }

        private async void CategoryTreeView_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node == null || string.IsNullOrEmpty(e.Node.Name))
            {
                return;
            }

            string selected = e.Node.Name;

            // Don't navigate if this is a parent node with children
            if (e.Node.Nodes.Count > 0)
            {
                return;
            }

            ShowProgress($"Loading {selected} settings...");
            UpdateProgress(0, $"Loading {selected} settings...");

            UpdateProgress(100, $"{selected} loaded");
            await Task.Delay(300);
            HideProgress();
            ShowPanel(selected);

            if (_settingsPanels.TryGetValue(selected, out Panel? panel) && panel.Controls.Count > 0)
            {
                Control control = panel.Controls[0];
                MethodInfo? reloadMethod = control.GetType().GetMethod("ReloadComboBoxDataAsync");
                if (reloadMethod != null)
                {
                    Task? task = reloadMethod.Invoke(control, null) as Task;
                    if (task != null)
                    {
                        await task;
                    }
                }
            }
        }

        private void ShowPanel(string panelName)
        {
            foreach (Panel panel in _settingsPanels.Values)
            {
                panel.Visible = false;
            }

            if (_settingsPanels.ContainsKey(panelName))
            {
                _settingsPanels[panelName].Visible = true;
            }
        }

        private void UpdateStatus(string message) => SettingsForm_StatusText.Text = message;

        /// <summary>
        /// Selects a TreeView node by its name, expanding parent nodes as needed.
        /// </summary>
        private void SelectTreeNodeByName(string nodeName)
        {
            TreeNode? FindNode(TreeNodeCollection nodes, string name)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Name == name)
                        return node;
                    var found = FindNode(node.Nodes, name);
                    if (found != null)
                        return found;
                }
                return null;
            }

            var targetNode = FindNode(SettingsForm_TreeView_Category.Nodes, nodeName);
            if (targetNode != null)
            {
                targetNode.Parent?.Expand();
                SettingsForm_TreeView_Category.SelectedNode = targetNode;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (HasChanges)
                {
                    DialogResult result = Service_ErrorHandler.ShowConfirmation(
                        @"You have changes that require a restart. Exit and reset the application?",
                        @"Unsaved Changes",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Allow closing, then restart
                        e.Cancel = false;

                        // Release the single instance mutex before restarting
                        // This prevents the "Application Already Running" error
                        if (Program.AppMutex != null)
                        {
                            try
                            {
                                Program.AppMutex.ReleaseMutex();
                            }
                            catch (ApplicationException)
                            {
                                // Ignore if not owned
                            }
                            Program.AppMutex.Dispose();
                            Program.AppMutex = null;
                        }

                        Application.Restart();
                        Application.ExitThread();
                    }
                    else
                    {
                        // Prevent closing
                        e.Cancel = true;
                    }
                }
                else
                {
                    // Allow closing
                    e.Cancel = false;
                }
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        #endregion

        #region Progress Control Methods

        private Helper_StoredProcedureProgress? _progressHelper;

        /// <summary>
        /// Get or create progress helper instance
        /// </summary>
        private Helper_StoredProcedureProgress ProgressHelper
        {
            get
            {
                _progressHelper ??= Helper_StoredProcedureProgress.Create(
                    SettingsForm_ProgressBar, 
                    SettingsForm_StatusText, 
                    this);
                return _progressHelper;
            }
        }

        private void ShowProgress(string status = "Loading...")
        {
            try
            {
                ProgressHelper.ShowProgress(status);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Warning: Progress display error - {ex.Message}");
            }
        }

        private void UpdateProgress(int progress, string status)
        {
            try
            {
                ProgressHelper.UpdateProgress(progress, status);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Warning: Progress update error - {ex.Message}");
            }
        }

        private void ShowError(string errorMessage, int? progress = null)
        {
            try
            {
                ProgressHelper.ShowError(errorMessage, progress);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Warning: Error display error - {ex.Message}");
            }
        }

        private void ShowSuccess(string successMessage)
        {
            try
            {
                ProgressHelper.ShowSuccess(successMessage);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Warning: Success display error - {ex.Message}");
            }
        }

        private void HideProgress()
        {
            try
            {
                ProgressHelper.HideProgress();
            }
            catch (Exception ex)
            {
                UpdateStatus($"Warning: Progress hide error - {ex.Message}");
            }
        }

        /// <summary>
        /// Process stored procedure result and update progress accordingly
        /// </summary>
        /// <param name="result">Stored procedure result</param>
        /// <param name="successMessage">Custom success message (optional)</param>
        public void ProcessStoredProcedureResult(StoredProcedureResult result, string? successMessage = null)
        {
            try
            {
                if (result.IsSuccess)
                {
                    ShowSuccess(successMessage ?? result.ErrorMessage);
                }
                else
                {
                    ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Warning: Result processing error - {ex.Message}");
            }
        }

        /// <summary>
        /// Process generic stored procedure result and update progress accordingly
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="result">Stored procedure result</param>
        /// <param name="successMessage">Custom success message (optional)</param>
        public void ProcessStoredProcedureResult<T>(StoredProcedureResult<T> result, string? successMessage = null)
        {
            try
            {
                if (result.IsSuccess)
                {
                    ShowSuccess(successMessage ?? result.ErrorMessage);
                }
                else
                {
                    ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Warning: Result processing error - {ex.Message}");
            }
        }

        #endregion
        #region Helpers

        private void InitializeHelpButtons()
        {
            ApplyHelpButton("SettingsForm_Button_Help_General", "settings-general", "general-overview");
            ApplyHelpButton("SettingsForm_Button_Help_Database", "settings-database", "database-overview");
            ApplyHelpButton("SettingsForm_Button_Help_Theme", "settings-theme", "theme-overview");
            ApplyHelpButton("SettingsForm_Button_Help_Users", "settings-users", "users-overview");
            ApplyHelpButton("SettingsForm_Button_Help_Shortcuts", "settings-shortcuts", "shortcuts-overview");
            ApplyHelpButton("SettingsForm_Button_Help_About", "settings-about", "about-overview");
            
            ApplyHelpButton("SettingsForm_Button_Help_PartNumbers", "settings-management", "part-number-overview");
            ApplyHelpButton("SettingsForm_Button_Help_Operations", "settings-management", "operation-overview");
            ApplyHelpButton("SettingsForm_Button_Help_Locations", "settings-management", "location-overview");
            ApplyHelpButton("SettingsForm_Button_Help_ItemTypes", "settings-management", "item-type-overview");
        }

        private void ApplyHelpButton(string buttonName, string category, string topic)
        {
            var helpButton = Controls.Find(buttonName, true).FirstOrDefault() as Button;
            if (helpButton != null)
            {
                helpButton.Click += (s, e) =>
                {
                    HelpViewerForm.GetInstance().BringToFrontAndNavigate(category, topic);
                };
            }
        }

        #endregion

    }
}
