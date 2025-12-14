using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;
using MTM_WIP_Application_Winforms.Controls.MainForm;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.ErrorReports;
using MTM_WIP_Application_Winforms.Forms.Settings;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Database;
using Timer = System.Windows.Forms.Timer;
using Microsoft.Extensions.DependencyInjection;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace MTM_WIP_Application_Winforms.Forms.MainForm
{
    #region MainForm

using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

    public partial class MainForm : ThemedForm, IConnectionRecoveryView
    {
        #region Fields

        private Timer? _connectionStrengthTimer;
        private Timer? _connectionMonitorTimer;
        public Helper_Control_MySqlSignal ConnectionStrengthChecker = null!;
        private Helper_StoredProcedureProgress? _progressHelper;
        private Form_ViewErrorReports? _viewErrorReportsForm;


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Service_ConnectionRecoveryManager ConnectionRecoveryManager { get; private set; } = null!;
        private readonly IShortcutService? _shortcutService;

        /// <summary>
        /// Flag to skip the next soft reset of the Inventory Tab.
        /// Used when redirecting from Advanced Inventory with pre-populated data.
        /// </summary>

        /// Used when redirecting from Advanced Inventory with pre-populated data.
        /// </summary>
        public bool SkipNextInventoryTabReset { get; set; }

        #endregion

        #region Constructors

        #region Initialization

        public MainForm()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["FormType"] = nameof(MainForm),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(MainForm), nameof(MainForm));

            Debug.WriteLine("[DEBUG] [MainForm.ctor] Constructing MainForm...");
            try
            {
                Service_DebugTracer.TraceUIAction("FORM_INITIALIZATION", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["Phase"] = "START",
                        ["ComponentType"] = "MainForm"
                    });

                InitializeComponent();

                // Resolve shortcut service
                _shortcutService = Program.ServiceProvider?.GetService<IShortcutService>();
                if (_shortcutService != null && !string.IsNullOrEmpty(Model_Application_Variables.User))
                {
                    // Initialize asynchronously
                    _ = _shortcutService.InitializeAsync(Model_Application_Variables.User);
                }

                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                AutoScaleMode = AutoScaleMode.Dpi;

                Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["DpiScaling"] = "APPLIED",
                        ["LayoutAdjustments"] = "APPLIED"
                    });

                // Apply comprehensive DPI scaling and runtime layout adjustments
                // THEME POLICY: Only update theme on startup, in settings menu, or on DPI change.
                // Do NOT call theme update methods from arbitrary event handlers or business logic.

                Debug.WriteLine("[DEBUG] [MainForm.ctor] InitializeComponent complete.");

                InitializeFormTitle();
                InitializeProgressControl();
                Debug.WriteLine("[DEBUG] [MainForm.ctor] Progress control initialized.");

                Service_DebugTracer.TraceUIAction("CONNECTION_CHECKER_INIT", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["Component"] = "Helper_Control_MySqlSignal"
                    });
                ConnectionStrengthChecker = new Helper_Control_MySqlSignal();
                Debug.WriteLine("[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.");

                Service_DebugTracer.TraceUIAction("CONNECTION_RECOVERY_INIT", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["Component"] = "Service_ConnectionRecoveryManager"
                    });
                ConnectionRecoveryManager = new Service_ConnectionRecoveryManager(this);
                Debug.WriteLine("[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.");

                InitializeStartupComponents();
                Debug.WriteLine("[DEBUG] [MainForm.ctor] Startup components initialized.");

                WireUpFormShownEvent();

                Service_DebugTracer.TraceUIAction("FORM_INITIALIZATION", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["Phase"] = "COMPLETE",
                        ["Success"] = true
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DEBUG] [MainForm.ctor] Exception: {ex}");
                Service_DebugTracer.TraceUIAction("FORM_INITIALIZATION", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["Phase"] = "ERROR",
                        ["Success"] = false,
                        ["Exception"] = ex.Message
                    });
                LoggingUtility.LogApplicationError(ex);
                // NOTE: Fire-and-forget required here - constructors cannot be async.
                // Error logging may be incomplete if application terminates immediately.
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: nameof(MainForm));
            }

            Debug.WriteLine("[DEBUG] [MainForm.ctor] MainForm constructed.");

            Service_DebugTracer.TraceMethodExit(null, nameof(MainForm), nameof(MainForm));
        }

        #endregion

        #region Initialization Methods

        private void InitializeFormTitle()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["User"] = Model_Application_Variables.User,
                ["UserType"] = Model_Application_Variables.UserTypeDeveloper ? "Developer" : Model_Application_Variables.UserTypeAdmin ? "Admin" : Model_Application_Variables.UserTypeNormal ? "Normal" : "ReadOnly"
            }, nameof(InitializeFormTitle), nameof(MainForm));

            try
            {
                string privilege = GetUserPrivilegeDisplayText();
                var formTitleData = new Dictionary<string, object>
                {
                    ["User"] = Model_Application_Variables.User,
                    ["Privilege"] = privilege,
                    ["Title"] = $"Manitowoc Tool and Manufacturing WIP Inventory System | {Model_Application_Variables.User} | {privilege}"
                };

                Service_DebugTracer.TraceBusinessLogic("FORM_TITLE_GENERATION",
                    inputData: new { User = Model_Application_Variables.User, UserType = privilege },
                    outputData: formTitleData["Title"]);

                Text = formTitleData["Title"].ToString();

                Service_DebugTracer.TraceUIAction("FORM_TITLE_SET", nameof(MainForm), formTitleData);
            }
            catch (Exception ex)
            {
                Service_DebugTracer.TraceUIAction("FORM_TITLE_ERROR", nameof(MainForm),
                    new Dictionary<string, object> { ["Exception"] = ex.Message });
                LoggingUtility.LogApplicationError(ex);
            }

            Service_DebugTracer.TraceMethodExit(null, nameof(InitializeFormTitle), nameof(MainForm));
        }

        private static string GetUserPrivilegeDisplayText()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["UserTypeDeveloper"] = Model_Application_Variables.UserTypeDeveloper,
                ["UserTypeAdmin"] = Model_Application_Variables.UserTypeAdmin,
                ["UserTypeNormal"] = Model_Application_Variables.UserTypeNormal,
                ["UserTypeReadOnly"] = Model_Application_Variables.UserTypeReadOnly
            }, nameof(GetUserPrivilegeDisplayText), nameof(MainForm));

            string privilege;
            if (Model_Application_Variables.UserTypeDeveloper)
                privilege = "Developer";
            else if (Model_Application_Variables.UserTypeAdmin)
                privilege = "Administrator";
            else if (Model_Application_Variables.UserTypeNormal)
                privilege = "Normal User";
            else if (Model_Application_Variables.UserTypeReadOnly)
                privilege = "Read Only";
            else
                privilege = "Unknown";

            Service_DebugTracer.TraceBusinessLogic("USER_PRIVILEGE_DETERMINATION",
                inputData: new {
                    Developer = Model_Application_Variables.UserTypeDeveloper,
                    Admin = Model_Application_Variables.UserTypeAdmin,
                    Normal = Model_Application_Variables.UserTypeNormal,
                    ReadOnly = Model_Application_Variables.UserTypeReadOnly
                },
                outputData: privilege);

            Service_DebugTracer.TraceMethodExit(privilege, nameof(GetUserPrivilegeDisplayText), nameof(MainForm));
            return privilege;
        }

        private void InitializeStartupComponents()
        {
            Service_DebugTracer.TraceMethodEntry(null, nameof(InitializeStartupComponents), nameof(MainForm));

            try
            {
                Service_DebugTracer.TraceUIAction("CONNECTION_STRENGTH_SETUP", nameof(MainForm),
                    new Dictionary<string, object> { ["Phase"] = "START" });
                MainForm_OnStartup_SetupConnectionStrengthControl();
                MainForm_OnStartup_SetupConnectionMonitor();
                Debug.WriteLine("[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.");

                Service_DebugTracer.TraceUIAction("EVENTS_WIREUP", nameof(MainForm),
                    new Dictionary<string, object> { ["Phase"] = "START" });
                MainForm_OnStartup_WireUpEvents();
                Debug.WriteLine("[DEBUG] [MainForm.ctor] Events wired up.");

                Service_DebugTracer.TraceUIAction("DPI_EVENTS_WIREUP", nameof(MainForm),
                    new Dictionary<string, object> { ["Phase"] = "START" });
                // Wire up DPI change handling for runtime DPI awareness
                MainForm_OnStartup_WireUpDpiChangeEvents();
                Debug.WriteLine("[DEBUG] [MainForm.ctor] DPI change events wired up.");

                InitializeViewMenuItems();

                Service_DebugTracer.TraceUIAction("STARTUP_COMPONENTS", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["Phase"] = "COMPLETE",
                        ["ComponentsInitialized"] = new[] { "ConnectionStrength", "Events", "DpiChangeEvents" }
                    });
            }
            catch (Exception ex)
            {
                Service_DebugTracer.TraceUIAction("STARTUP_COMPONENTS_ERROR", nameof(MainForm),
                    new Dictionary<string, object> { ["Exception"] = ex.Message });
                LoggingUtility.LogApplicationError(ex);
                throw;
            }

            Service_DebugTracer.TraceMethodExit(null, nameof(InitializeStartupComponents), nameof(MainForm));
        }

        private void ApplyPrivileges()
        {
            ConfigureDevelopmentMenuVisibility();
            ConfigureVisualMenuVisibility();
        }

        private void WireUpFormShownEvent()
        {
            try
            {
                Shown += async (s, e) =>
                {
                    try
                    {
                        Debug.WriteLine("[DEBUG] [MainForm.ctor] MainForm Shown event triggered.");
                        await MainForm_OnStartup_GetUserFullNameAsync();

                        // Load user settings (AutoExpandPanels, AnimationsEnabled)
                        await MainForm_OnStartup_LoadUserSettingsAsync();

                        Debug.WriteLine("[DEBUG] [MainForm.ctor] User full name loaded.");

                        ApplyPrivileges();

                        await Task.Delay(500);
                        SetInitialFocusToInventoryTab();

                        Debug.WriteLine("[DEBUG] [MainForm.ctor] MainForm is now idle and ready.");
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                        await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "MainForm_Shown_Event");
                    }
                };
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                throw;
            }
        }

        /// <summary>
        /// Configures Development Menu visibility based on current user
        /// Users with Developer role can access the Development Menu
        /// </summary>
        private void ConfigureDevelopmentMenuVisibility()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["CurrentUser"] = Model_Application_Variables.User ?? "Unknown",
                ["DevelopmentMenuExists"] = MainForm_MenuStrip_Development != null
            }, nameof(ConfigureDevelopmentMenuVisibility), nameof(MainForm));

            try
            {
                string currentUser = Model_Application_Variables.User?.ToUpperInvariant() ?? "";

                // Check both the database role flag and the hardcoded legacy users
                bool isDeveloper = Model_Application_Variables.UserTypeDeveloper;

                if (MainForm_MenuStrip_Development != null)
                {
                    MainForm_MenuStrip_Development.Visible = isDeveloper;

                    Service_DebugTracer.TraceBusinessLogic("DEVELOPMENT_MENU_VISIBILITY",
                        inputData: new {
                            User = Model_Application_Variables.User,
                            UserUpperCase = currentUser,
                            IsDeveloper = isDeveloper,
                            IsUserTypeDeveloper = Model_Application_Variables.UserTypeDeveloper
                        },
                        outputData: new {
                            MenuVisible = isDeveloper,
                            AccessGranted = isDeveloper ? "Yes" : "No"
                        });

                    Service_DebugTracer.TraceUIAction("DEVELOPMENT_MENU_CONFIGURED", nameof(MainForm),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["MenuVisible"] = isDeveloper,
                            ["AccessLevel"] = isDeveloper ? "Developer" : "Standard User"
                        });

                    LoggingUtility.LogApplicationInfo($"Development Menu configured for user '{Model_Application_Variables.User}': {(isDeveloper ? "Visible" : "Hidden")}");

                    if (isDeveloper)
                    {
                        InitializeDevelopmentMenuItems();
                    }
                }
                else
                {
                    Service_DebugTracer.TraceUIAction("DEVELOPMENT_MENU_NOT_FOUND", nameof(MainForm),
                        new Dictionary<string, object>
                        {
                            ["Warning"] = "developmentToolStripMenuItem is null"
                        });

                }
            }
            catch (Exception ex)
            {
                Service_DebugTracer.TraceUIAction("DEVELOPMENT_MENU_CONFIG_ERROR", nameof(MainForm),
                    new Dictionary<string, object> { ["Exception"] = ex.Message });

                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    controlName: nameof(MainForm),
                    contextData: new Dictionary<string, object>
                    {
                        ["Method"] = nameof(ConfigureDevelopmentMenuVisibility),
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    });
            }

            Service_DebugTracer.TraceMethodExit(null, nameof(ConfigureDevelopmentMenuVisibility), nameof(MainForm));
        }

        /// <summary>
        /// Configures Visual Menu visibility based on current user credentials.
        /// </summary>
        private void ConfigureVisualMenuVisibility()
        {
            bool hasDbCredentials = !string.IsNullOrEmpty(Model_Application_Variables.VisualUserName) &&
                                    !string.IsNullOrEmpty(Model_Application_Variables.VisualPassword);

            bool hasConfigCredentials = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["VisualUserName"]) &&
                                        !string.IsNullOrEmpty(ConfigurationManager.AppSettings["VisualPassword"]);

            bool isVisible = hasDbCredentials || hasConfigCredentials;

#if DEBUG
            // In DEBUG mode, always show the menu to allow testing with sample data
            isVisible = true;
#endif

            if (MainForm_MenuStrip_Visual != null)
            {
                MainForm_MenuStrip_Visual.Visible = isVisible;
                MainForm_MenuStrip_Visual.Enabled = isVisible;
            }
        }

        private void OpenVisualDashboard(Enum_VisualDashboardCategory category)
        {
            try
            {
                bool hasDbCredentials = !string.IsNullOrEmpty(Model_Application_Variables.VisualUserName) &&
                                        !string.IsNullOrEmpty(Model_Application_Variables.VisualPassword);

                bool hasConfigCredentials = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["VisualUserName"]) &&
                                            !string.IsNullOrEmpty(ConfigurationManager.AppSettings["VisualPassword"]);

#if DEBUG
                // In DEBUG mode, bypass credential check to allow sample data testing
#else
                if (!hasDbCredentials && !hasConfigCredentials)
                {
                    Service_ErrorHandler.ShowUserError(
                        "You do not have Infor Visual credentials configured.\n\nPlease go to File > Settings > User Management to configure your Visual ERP access.",
                        "Access Denied");
                    return;
                }
#endif

                var visualForm = new Forms.Visual.Form_InforVisualDashboard();
                visualForm.Show();
                _ = visualForm.SelectCategoryAsync(category);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(MainForm));
            }
        }

        private void MainForm_MenuStrip_Visual_Inventory_Click(object? sender, EventArgs e)
        {
            OpenVisualDashboard(Enum_VisualDashboardCategory.Inventory);
        }

        private void MainForm_MenuStrip_Visual_Receiving_Click(object? sender, EventArgs e)
        {
            OpenVisualDashboard(Enum_VisualDashboardCategory.Receiving);
        }

        private void MainForm_MenuStrip_Visual_DieTool_Click(object? sender, EventArgs e)
        {
            OpenVisualDashboard(Enum_VisualDashboardCategory.DieToolDiscovery);
        }

        private void MainForm_MenuStrip_Visual_Audit_Click(object? sender, EventArgs e)
        {
            OpenVisualDashboard(Enum_VisualDashboardCategory.InventoryAuditing);
        }

        private void MainForm_MenuStrip_Visual_UserAnalytics_Click(object? sender, EventArgs e)
        {
            OpenVisualDashboard(Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General);
        }

        private void SetInitialFocusToInventoryTab()
        {
            try
            {
                if (MainForm_UserControl_InventoryTab?.Control_InventoryTab_SuggestionBox_Part != null)
                {
                    MainForm_UserControl_InventoryTab.Control_InventoryTab_SuggestionBox_Part.Focus();
                    MainForm_UserControl_InventoryTab.Control_InventoryTab_SuggestionBox_Part.TextBox.SelectAll();
                    MainForm_UserControl_InventoryTab.Control_InventoryTab_SuggestionBox_Part.TextBox.BackColor =
                        Model_Application_Variables.UserUiColors.ControlFocusedBackColor ?? Color.LightBlue;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void InitializeProgressControl()
        {
            try
            {
                // Initialize progress helper using StatusStrip components
                _progressHelper = Helper_StoredProcedureProgress.Create(
                    MainForm_ProgressBar,
                    MainForm_StatusText,
                    this);

                // Initialize progress controls for all UserControls
                InitializeUserControlsProgress();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void InitializeUserControlsProgress()
        {
            try
            {
                // Set progress controls for main tab UserControls
                MainForm_UserControl_InventoryTab?.SetProgressControls(MainForm_ProgressBar, MainForm_StatusText);
                MainForm_UserControl_RemoveTab?.SetProgressControls(MainForm_ProgressBar, MainForm_StatusText);
                MainForm_UserControl_TransferTab?.SetProgressControls(MainForm_ProgressBar, MainForm_StatusText);

                // Set progress controls for advanced UserControls
                MainForm_UserControl_AdvancedInventory?.SetProgressControls(MainForm_ProgressBar, MainForm_StatusText);
                MainForm_UserControl_AdvancedRemove?.SetProgressControls(MainForm_ProgressBar, MainForm_StatusText);

                // Set progress controls for QuickButtons
                MainForm_UserControl_QuickButtons?.SetProgressControls(MainForm_ProgressBar, MainForm_StatusText);

                Debug.WriteLine("[DEBUG] [MainForm] UserControl progress helpers initialized.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion

        #region Toggle Panel Methods

        // Interface-based approach replacing reflection for toggle button text updates
        private void UpdateQuickButtonsToggleTextForAllTabs()
        {
            try
            {
                bool isCollapsed = MainForm_SplitContainer_Middle.Panel2Collapsed;
                MainForm_UserControl_InventoryTab?.SyncQuickButtonsPanelState(isCollapsed);
                MainForm_UserControl_RemoveTab?.SyncQuickButtonsPanelState(isCollapsed);
                MainForm_UserControl_TransferTab?.SyncQuickButtonsPanelState(isCollapsed);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion

        #region Startup / Events

        private void MainForm_OnStartup_WireUpEvents()
        {
            try
            {
                MainForm_TabControl.SelectedIndexChanged += async (s, e) =>
                {
                    try
                    {
                        MainForm_TabControl_SelectedIndexChanged(s, e);
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                        await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "MainForm_TabControl_SelectedIndexChanged_Handler");
                    }
                };
                MainForm_TabControl.Selecting += MainForm_TabControl_Selecting;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                throw;
            }
        }

        /// <summary>
        /// Wires up DPI change event handling for runtime DPI awareness.
        /// This ensures the application responds properly to DPI changes when moving between monitors
        /// or when the user changes system DPI settings.
        /// </summary>
        private void MainForm_OnStartup_WireUpDpiChangeEvents()
        {
            try
            {
                // Handle DPI changes when form is moved between monitors or DPI settings change
                DpiChanged += MainForm_DpiChanged;

                // Handle system DPI changes
                SystemEvents.DisplaySettingsChanged += (s, e) =>
                {
                    try
                    {
                        // Prompt user to restart instead of auto-refresh


                        // Note: We don't auto-refresh here because the DpiChanged event
                        // on the form will handle it and prompt the user
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                    }
                };


            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Debug.WriteLine($"[DEBUG] Error wiring up DPI change events: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles DPI changes for the main form and all its controls.
        /// </summary>
        private void MainForm_DpiChanged(object? sender, DpiChangedEventArgs e)
        {
            try
            {


                // Calculate scaling percentage for user-friendly message
                int oldPercent = (int)Math.Round(e.DeviceDpiOld / 96.0 * 100);
                int newPercent = (int)Math.Round(e.DeviceDpiNew / 96.0 * 100);

                // Prompt user for restart or auto-resize
                var result = Service_ErrorHandler.ShowConfirmation(
                    $"Display scaling has changed from {oldPercent}% to {newPercent}%.\n\n" +
                    "For the best appearance, it is recommended to restart the application.\n\n" +
                    "Click 'Yes' to restart now (recommended)\n" +
                    "Click 'No' to automatically resize all forms (may cause visual glitches)\n" +
                    "Click 'Cancel' to continue without changes",
                    "Display Scaling Changed",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // User chose to restart - save state and restart application


                    // Get the application executable path
                    string appPath = Application.ExecutablePath;

                    // Start new instance
                    Process.Start(appPath);

                    // Close current instance gracefully
                    Application.Exit();
                }
                else if (result == DialogResult.No)
                {

                    // Theme reapplied automatically by ThemedForm base class

                }
                else
                {
                    // User cancelled - do nothing

                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Debug.WriteLine($"[DEBUG] Error handling DPI change: {ex.Message}");
            }
        }

        private async Task MainForm_OnStartup_LoadUserSettingsAsync()
        {
            try
            {
                string user = Model_Application_Variables.User;

                // Load AutoExpandPanels
                var autoExpandResult = await Dao_User.GetAutoExpandPanelsAsync(user);
                if (autoExpandResult.IsSuccess)
                {
                    Model_Application_Variables.AutoExpandPanels = autoExpandResult.Data;
                }

                // Load AnimationsEnabled
                var animResult = await Dao_User.GetAnimationsEnabledAsync(user);
                if (animResult.IsSuccess)
                {
                    Model_Application_Variables.AnimationsEnabled = animResult.Data;
                }

                // Load ShowTotalSummaryPanel
                var showTotalResult = await Dao_User.GetShowTotalSummaryPanelAsync(user);
                if (showTotalResult.IsSuccess)
                {
                    Model_Application_Variables.ShowTotalSummaryPanel = showTotalResult.Data;
                }

                // Load Visual Credentials
                var visualUserResult = await Dao_User.GetVisualUserNameAsync(user);
                if (visualUserResult.IsSuccess)
                {
                    Model_Application_Variables.VisualUserName = visualUserResult.Data;
                }

                var visualPassResult = await Dao_User.GetVisualPasswordAsync(user);
                if (visualPassResult.IsSuccess)
                {
                    Model_Application_Variables.VisualPassword = visualPassResult.Data;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                // Don't block startup on settings load failure
            }
        }

        private static async Task MainForm_OnStartup_GetUserFullNameAsync()
        {
            try
            {
                Model_Application_Variables.UserFullName =
                    await Dao_User.GetUserFullNameAsync(Model_Application_Variables.User);

                if (string.IsNullOrEmpty(Model_Application_Variables.UserFullName))
                {
                    Model_Application_Variables.UserFullName = Model_Application_Variables.User;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "MainForm_OnStartup_GetUserFullNameAsync");
            }
        }

        private async void MainForm_OnStartup_SetupConnectionStrengthControl()
        {
            try
            {
                _connectionStrengthTimer = new Timer { Interval = 5000 };
                _connectionStrengthTimer.Tick += async (s, e) =>
                {
                    try
                    {
                        await ConnectionRecoveryManager.UpdateConnectionStrengthAsync();
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                    }
                };
                _connectionStrengthTimer.Start();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    nameof(MainForm_OnStartup_SetupConnectionStrengthControl));
            }
        }

        private void MainForm_OnStartup_SetupConnectionMonitor()
        {
            try
            {
                // 5 minutes = 300,000 ms
                _connectionMonitorTimer = new Timer { Interval = 300000 };
                _connectionMonitorTimer.Tick += async (s, e) =>
                {
                    try
                    {
                        var stats = await Helper_Database_ConnectionMonitor.GetConnectionStatsAsync();
                        LoggingUtility.Log($"[ConnectionMonitor] Server: {stats.ServerAddress}, Open: {stats.OpenConnections}, Healthy: {stats.IsHealthy}");
                        
                        if (!stats.IsHealthy)
                        {
                            LoggingUtility.Log($"[ConnectionMonitor] WARNING: {stats.WarningMessage}");
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                    }
                };
                _connectionMonitorTimer.Start();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion

        #region Tab Control

        private void MainForm_TabControl_Selecting(object? sender, TabControlCancelEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);
            try
            {
                Control_AdvancedInventory? advancedInvTab = MainForm_UserControl_AdvancedInventory;
                Control_AdvancedRemove? advancedRemoveTab = MainForm_UserControl_AdvancedRemove;

                if ((advancedInvTab?.Visible == true) || (advancedRemoveTab?.Visible == true))
                {
                    DialogResult result = Service_ErrorHandler.ShowWarning(
                        @"If you change the current tab now, any work will be lost.",
                        @"Warning",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning
                    );
                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm));
            }
        }

        private async void MainForm_TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                LoggingUtility.Log("[MainForm] TabControl_SelectedIndexChanged started");
                await ShowTabLoadingProgressAsync();
                Debug.WriteLine("Resetting user controls...");

                await ResetAllUserControlsAsync();

                // Update Quick Buttons toggle text for all tabs
                UpdateQuickButtonsToggleTextForAllTabs();

                SetTabVisibility();
                LoggingUtility.Log("[MainForm] TabControl_SelectedIndexChanged finished logic");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm));
            }
            finally
            {
                SetFocusForCurrentTab();
                HideTabLoadingProgress();
                LoggingUtility.Log("[MainForm] TabControl_SelectedIndexChanged finally block executed");
            }
        }

        #region Tab Control Helper Methods

        private async Task ResetAllUserControlsAsync()
        {
            try
            {
                var resetTasks = new List<Task>();
                LoggingUtility.Log($"[MainForm] ResetAllUserControlsAsync called. SkipNextInventoryTabReset: {SkipNextInventoryTabReset}");

                // Create reset tasks for each user control
                if (MainForm_UserControl_InventoryTab != null)
                {
                    if (!SkipNextInventoryTabReset)
                    {
                        LoggingUtility.Log("[MainForm] Queueing Inventory Tab reset");
                        resetTasks.Add(Task.Run(() => InvokeResetMethod(MainForm_UserControl_InventoryTab, "Control_InventoryTab_SoftReset")));
                    }
                    else
                    {
                        LoggingUtility.Log("[MainForm] Skipping Inventory Tab reset as requested.");
                        Debug.WriteLine("[DEBUG] Skipping Inventory Tab reset as requested.");
                        SkipNextInventoryTabReset = false;
                    }
                }

                if (MainForm_UserControl_AdvancedInventory != null)
                    resetTasks.Add(Task.Run(() => InvokeResetMethod(MainForm_UserControl_AdvancedInventory, "Control_AdvancedInventory_SoftReset")));

                if (MainForm_UserControl_RemoveTab != null)
                    resetTasks.Add(Task.Run(() => InvokeResetMethod(MainForm_UserControl_RemoveTab, "Control_RemoveTab_SoftReset")));

                if (MainForm_UserControl_AdvancedRemove != null)
                    resetTasks.Add(Task.Run(() => InvokeResetMethod(MainForm_UserControl_AdvancedRemove, "Control_AdvancedRemove_SoftReset")));

                if (MainForm_UserControl_TransferTab != null)
                    resetTasks.Add(Task.Run(() => InvokeResetMethod(MainForm_UserControl_TransferTab, "Control_TransferTab_SoftReset")));

                // Execute all reset tasks concurrently
                await Task.WhenAll(resetTasks);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Debug.WriteLine($"[DEBUG] Error resetting user controls: {ex.Message}");
            }
        }

        private static void InvokeResetMethod(UserControl control, string methodName)
        {
            try
            {
            string controlName = control.GetType().Name;
            LoggingUtility.Log($"[MainForm] Attempting to invoke {methodName} on {controlName}");
            Debug.WriteLine($"Attempting to invoke {methodName} on {controlName}");

            MethodInfo? method = control.GetType().GetMethod(methodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (method != null)
            {
                Debug.WriteLine($"Invoking {method.Name} on {controlName}");

                if (control.InvokeRequired)
                {
                control.BeginInvoke(new Action(() =>
                {
                    try
                    {
                    method.Invoke(control, null);
                    LoggingUtility.Log($"[MainForm] {method.Name} invoked on UI thread for {controlName}");
                    }
                    catch (Exception ex)
                    {
                    LoggingUtility.LogApplicationError(ex);
                    }
                }));
                }
                else
                {
                method.Invoke(control, null);
                LoggingUtility.Log($"[MainForm] {method.Name} invoked directly for {controlName}");
                }
            }
            else
            {
                LoggingUtility.Log($"[MainForm] Method {methodName} not found on {controlName}");
                Debug.WriteLine($"Method {methodName} not found on {controlName}");
            }
            }
            catch (Exception ex)
            {
            LoggingUtility.LogApplicationError(ex);
            Debug.WriteLine($"[DEBUG] Error invoking {methodName} on {control.GetType().Name}: {ex.Message}");
            }
        }

        private void SetTabVisibility()
        {
            try
            {
                // Only handle visibility after resets
                switch (MainForm_TabControl.SelectedIndex)
                {
                    case 0:
                        SetInventoryTabVisibility();
                        break;
                    case 1:
                        SetRemoveTabVisibility();
                        break;
                    case 2:
                        SetTransferTabVisibility();
                        break;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void SetInventoryTabVisibility()
        {
            if (MainForm_UserControl_InventoryTab != null)
            {
                MainForm_UserControl_InventoryTab.Visible = true;
                if (MainForm_UserControl_AdvancedInventory != null)
                {
                    MainForm_UserControl_AdvancedInventory.Visible = false;
                }
            }
        }

        private void SetRemoveTabVisibility()
        {
            if (MainForm_UserControl_RemoveTab != null)
            {
                MainForm_UserControl_RemoveTab.Visible = true;
                if (MainForm_UserControl_AdvancedRemove != null)
                {
                    MainForm_UserControl_AdvancedRemove.Visible = false;
                }
            }
        }

        private void SetTransferTabVisibility()
        {
            if (MainForm_UserControl_TransferTab != null)
            {
                MainForm_UserControl_TransferTab.Visible = true;
            }
        }

        private void SetFocusForCurrentTab()
        {
            try
            {
                // Set focus to the main input control for the currently visible tab
                switch (MainForm_TabControl.SelectedIndex)
                {
                    case 0:
                        MainForm_UserControl_InventoryTab?.Control_InventoryTab_SuggestionBox_Part?.Focus();
                        break;
                    case 1:
                        MainForm_UserControl_RemoveTab?.Control_RemoveTab_TextBox_Part?.Focus();
                        break;
                    case 2:
                        MainForm_UserControl_TransferTab?.Control_TransferTab_TextBox_Part?.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion

        private async Task ShowTabLoadingProgressAsync()
        {
            try
            {
                if (_progressHelper != null)
                {
                    _progressHelper.ShowProgress("Switching tab...");
                    _progressHelper.UpdateProgress(25, "Loading controls...");

                    await Task.Delay(100);
                    _progressHelper.UpdateProgress(50, "Applying settings...");

                    await Task.Delay(100);
                    _progressHelper.UpdateProgress(75, "Ready");

                    await Task.Delay(100);
                    _progressHelper.UpdateProgress(100, "Tab loaded");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void HideTabLoadingProgress()
        {
            try
            {
                _progressHelper?.HideProgress();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion

        #region Tab Shortcuts

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                // Help Shortcuts (Prioritize F1)
                Keys helpKey = _shortcutService?.GetShortcutKey("Shortcut_Help") ?? Keys.F1;
                if (keyData == helpKey)
                {
                    ShowHelp();
                    return true;
                }

                Keys gettingStartedKey = _shortcutService?.GetShortcutKey("Shortcut_Help_GettingStarted") ?? (Keys.Control | Keys.F1);
                if (keyData == gettingStartedKey)
                {
                    ShowHelp("getting-started");
                    return true;
                }

                Keys keyboardShortcutsKey = _shortcutService?.GetShortcutKey("Shortcut_Help_KeyboardShortcuts") ?? (Keys.Control | Keys.Shift | Keys.K);
                if (keyData == keyboardShortcutsKey)
                {
                    ShowHelp("keyboard-shortcuts");
                    return true;
                }

                // Ctrl+Shift+H - System Health
                if (keyData == (Keys.Control | Keys.Shift | Keys.H))
                {
                    MainForm_MenuStrip_View_SystemHealth_Click(this, EventArgs.Empty);
                    return true;
                }

                // Ctrl+P - Print active grid
                if (keyData == (Keys.Control | Keys.P))
                {
                    HandlePrintShortcut();
                    return true;
                }

                // QuickButton shortcuts (F1-F10)
                for (int i = 0; i < 10; i++)
                {
                    string shortcutName = $"Shortcut_QuickButton_{(i + 1):D2}";
                    Keys defaultKey = (Keys)(Keys.F1 + i);
                    Keys quickButtonKey = _shortcutService?.GetShortcutKey(shortcutName) ?? defaultKey;

                    // If Help is F1, and QuickButton 1 is F1, Help already handled it.
                    // But if Help is remapped to F12, and QuickButton 1 is F1, we want QuickButton 1 to work.
                    // The previous check handles the collision if keys are identical.
                    
                    if (keyData == quickButtonKey)
                    {
                        // Trigger QuickButton click
                        if (MainForm_UserControl_QuickButtons?.quickButtons != null &&
                            i < MainForm_UserControl_QuickButtons.quickButtons.Count)
                        {
                            MainForm_UserControl_QuickButtons.quickButtons[i].PerformClick();
                            return true;
                        }
                    }
                }

                Keys tab1Key = _shortcutService?.GetShortcutKey("Shortcut_MainForm_Tab1") ?? Core_WipAppVariables.Shortcut_MainForm_Tab1;
                if (keyData == tab1Key)
                {
                    MainForm_TabControl.SelectedIndex = 0;
                    return true;
                }

                Keys tab2Key = _shortcutService?.GetShortcutKey("Shortcut_MainForm_Tab2") ?? Core_WipAppVariables.Shortcut_MainForm_Tab2;
                if (keyData == tab2Key)
                {
                    MainForm_TabControl.SelectedIndex = 1;
                    return true;
                }

                Keys tab3Key = _shortcutService?.GetShortcutKey("Shortcut_MainForm_Tab3") ?? Core_WipAppVariables.Shortcut_MainForm_Tab3;
                if (keyData == tab3Key)
                {
                    MainForm_TabControl.SelectedIndex = 2;
                    return true;
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return false;
            }
        }

        /// <summary>
        /// Handles Ctrl+P keyboard shortcut to print the active DataGridView
        /// </summary>
        private async void HandlePrintShortcut()
        {
            try
            {
                // Determine which tab is active and get its printable grid
                DataGridView? activeGrid = null;
                string gridName = string.Empty;

                int selectedIndex = MainForm_TabControl.SelectedIndex;

                if (selectedIndex >= 0 && selectedIndex < MainForm_TabControl.TabPages.Count)
                {
                    var selectedTab = MainForm_TabControl.TabPages[selectedIndex];
                    // Use user-friendly names for print titles
                    if (selectedTab.Text.Contains("Inventory")) gridName = "Inventory List";
                    else if (selectedTab.Text.Contains("Remove")) gridName = "Remove Inventory";
                    else if (selectedTab.Text.Contains("Transfer")) gridName = "Transfer Inventory";
                    else gridName = selectedTab.Text;

                    // Find first DataGridView in the selected tab
                    activeGrid = FindFirstDataGridView(selectedTab);
                }

                if (activeGrid is null || activeGrid.Rows.Count == 0)
                {
                    Service_ErrorHandler.ShowWarning("No printable data available in the current tab.", "Print");
                    return;
                }

                // Call print manager with active grid
                await Helper_PrintManager.ShowPrintDialogAsync(this, activeGrid, gridName);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm),
                    contextData: new Dictionary<string, object>
                    {
                        ["Action"] = "Ctrl+P Print Shortcut"
                    });
            }
        }

        /// <summary>
        /// Recursively finds the first DataGridView in a control hierarchy
        /// </summary>
        private DataGridView? FindFirstDataGridView(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child is DataGridView dgv)
                {
                    return dgv;
                }

                if (child.Controls.Count > 0)
                {
                    var found = FindFirstDataGridView(child);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }

            return null;
        }

        #endregion

        #region Form Closing

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _connectionStrengthTimer?.Stop();
                _connectionStrengthTimer?.Dispose();

                // Close View Error Reports form if it's open
                if (_viewErrorReportsForm != null && !_viewErrorReportsForm.IsDisposed)
                {
                    _viewErrorReportsForm.Close();
                    _viewErrorReportsForm.Dispose();
                    _viewErrorReportsForm = null;
                }

            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "MainForm_OnFormClosing");
            }

            base.OnFormClosing(e);
        }

        #endregion

        #region Menu Event Handlers

        private async void MainForm_MenuStrip_File_Settings_Click(object sender, EventArgs e)
        {
            Service_DebugTracer.TraceUIAction("SETTINGS_MENU_CLICK", nameof(MainForm),
                new Dictionary<string, object>
                {
                    ["MenuAction"] = "File > Settings",
                    ["UserInitiated"] = true
                });

            Service_DebugTracer.TraceMethodEntry(null, nameof(MainForm_MenuStrip_File_Settings_Click), nameof(MainForm));

            try
            {
                Service_DebugTracer.TraceUIAction("SETTINGS_FORM_OPEN", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["FormType"] = "SettingsForm",
                        ["Modal"] = true
                    });

                using SettingsForm settingsForm = new();
                if (settingsForm.ShowDialog(this) != DialogResult.OK)
                {
                    Service_DebugTracer.TraceUIAction("SETTINGS_FORM_CANCELED", nameof(MainForm),
                        new Dictionary<string, object> { ["UserAction"] = "CANCELED" });
                    return;
                }

                Service_DebugTracer.TraceUIAction("SETTINGS_FORM_ACCEPTED", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["UserAction"] = "ACCEPTED",
                        ["RequiredOperations"] = new[] { "HardReset", "ThemeApply" }
                    });

                Service_DebugTracer.TraceUIAction("INVENTORY_TAB_RESET", nameof(MainForm));
                MainForm_UserControl_InventoryTab?.Control_InventoryTab_HardReset();

                // Theme reapplied automatically by ThemedForm base class when settings change
                Service_DebugTracer.TraceUIAction("THEME_AUTO_REAPPLY", nameof(MainForm),
                    new Dictionary<string, object> { ["Reason"] = "SettingsChanged" });
            }
            catch (Exception ex)
            {
                Service_DebugTracer.TraceUIAction("SETTINGS_MENU_ERROR", nameof(MainForm),
                    new Dictionary<string, object> { ["Exception"] = ex.Message });
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: nameof(MainForm_MenuStrip_File_Settings_Click));
            }

            Service_DebugTracer.TraceMethodExit(null, nameof(MainForm_MenuStrip_File_Settings_Click), nameof(MainForm));
        }

        private void MainForm_MenuStrip_Exit_Click(object sender, EventArgs e)
        {
            Service_DebugTracer.TraceUIAction("EXIT_MENU_CLICK", nameof(MainForm),
                new Dictionary<string, object>
                {
                    ["MenuAction"] = "File > Exit",
                    ["UserInitiated"] = true
                });

            Service_DebugTracer.TraceMethodEntry(null, nameof(MainForm_MenuStrip_Exit_Click), nameof(MainForm));

            try
            {
                Service_DebugTracer.TraceUIAction("EXIT_CONFIRMATION_SHOW", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["DialogType"] = "Confirmation",
                        ["Buttons"] = "YesNo",
                        ["Icon"] = "Question"
                    });

                DialogResult result = Service_ErrorHandler.ShowConfirmation(
                    @"Are you sure you want to exit?",
                    @"Exit Application",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                Service_DebugTracer.TraceUIAction("EXIT_CONFIRMATION_RESULT", nameof(MainForm),
                    new Dictionary<string, object>
                    {
                        ["UserChoice"] = result.ToString(),
                        ["WillExit"] = result == DialogResult.Yes
                    });

                if (result == DialogResult.Yes)
                {
                    Service_DebugTracer.TraceUIAction("APPLICATION_EXIT", nameof(MainForm),
                        new Dictionary<string, object>
                        {
                            ["ExitMethod"] = "User Requested",
                            ["Confirmed"] = true
                        });
                    Application.Exit();
                }
                else
                {
                    Service_DebugTracer.TraceUIAction("EXIT_CANCELED", nameof(MainForm),
                        new Dictionary<string, object> { ["UserAction"] = "CANCELED" });
                }
            }
            catch (Exception ex)
            {
                Service_DebugTracer.TraceUIAction("EXIT_MENU_ERROR", nameof(MainForm),
                    new Dictionary<string, object> { ["Exception"] = ex.Message });
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm));
            }

            Service_DebugTracer.TraceMethodExit(null, nameof(MainForm_MenuStrip_Exit_Click), nameof(MainForm));
        }

        private void MainForm_MenuStrip_View_PersonalHistory_Click(object sender, EventArgs e)
        {
            try
            {
                string currentUser = Model_Application_Variables.User;

                Transactions.Transactions transactionsForm = new(connectionString: string.Empty, currentUser);
                transactionsForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    retryAction: null,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["MenuAction"] = "ViewTransactionHistory"
                    },
                    controlName: nameof(MainForm)
                );
            }
        }

        #endregion

        private void MainForm_MenuStrip_Development_ViewApplicationLogs_Click(object? sender, EventArgs e)
        {
            try
            {
                // Redirect to Developer Tools
                MainForm_MenuStrip_Development_DeveloperTools_Click(sender, e);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(MainForm));
            }
        }

        private async void MainForm_MenuStrip_Development_SyncReports_Click(object sender, EventArgs e)
        {
            try
            {
                // Disable menu item during sync
                if (sender is ToolStripMenuItem menuItem)
                {
                    menuItem.Enabled = false;
                }

                // Show progress indication
                Cursor = Cursors.WaitCursor;

                // Trigger manual sync
                var result = await Service_ErrorReportSync.SyncManuallyAsync();

                Cursor = Cursors.Default;

                // Show result to user
                if (result.IsSuccess)
                {
                    Service_ErrorHandler.ShowInformation(
                        result.StatusMessage ?? $"Sync completed successfully. {result.Data} report(s) submitted.",
                        "Sync Complete");
                }
                else
                {
                    Service_ErrorHandler.ShowWarning(
                        result.ErrorMessage ?? "Failed to synchronize pending error reports.",
                        "Sync Failed");
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                LoggingUtility.LogApplicationError(ex);

                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "ManualSync",
                        ["User"] = Model_Application_Variables.User
                    },
                    controlName: nameof(MainForm_MenuStrip_Development_SyncReports_Click));
            }
            finally
            {
                // Re-enable menu item
                if (sender is ToolStripMenuItem menuItem)
                {
                    menuItem.Enabled = true;
                }
            }
        }

        private void MainForm_MenuStrip_Development_ViewErrorReports_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_viewErrorReportsForm is { IsDisposed: false })
                {
                    if (_viewErrorReportsForm.WindowState == FormWindowState.Minimized)
                    {
                        _viewErrorReportsForm.WindowState = FormWindowState.Normal;
                    }

                    _viewErrorReportsForm.BringToFront();
                    _viewErrorReportsForm.Focus();
                    return;
                }

                _viewErrorReportsForm = new Form_ViewErrorReports();
                _viewErrorReportsForm.FormClosed += (_, _) => _viewErrorReportsForm = null;
                _viewErrorReportsForm.Show(this);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(MainForm_MenuStrip_Development_ViewErrorReports_Click));
            }
        }

        private void MainForm_MenuStrip_Development_DeveloperTools_Click(object? sender, EventArgs e)
        {
            try
            {
                var devToolsService = ServiceProviderServiceExtensions.GetRequiredService<Services.DeveloperTools.IService_DeveloperTools>(Program.ServiceProvider!);
                var logger = ServiceProviderServiceExtensions.GetRequiredService<Services.Logging.ILoggingService>(Program.ServiceProvider!);
                var errorHandler = ServiceProviderServiceExtensions.GetRequiredService<Services.ErrorHandling.IService_ErrorHandler>(Program.ServiceProvider!);
                var feedbackManager = ServiceProviderServiceExtensions.GetRequiredService<Services.IService_FeedbackManager>(Program.ServiceProvider!);

                var form = new Forms.DeveloperTools.DeveloperToolsForm(devToolsService, logger, errorHandler, feedbackManager);
                form.Show();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(MainForm_MenuStrip_Development_DeveloperTools_Click));
            }
        }

        #region Help Menu Event Handlers


        private void MainForm_MenuStrip_Help_ViewHelp_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHelp();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm));
            }
        }

        private void MainForm_MenuStrip_Help_ReleaseNotes_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHelp("getting-started", "release-notes");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm),
                    contextData: new Dictionary<string, object> { ["HelpCategory"] = "getting-started", ["HelpTopic"] = "release-notes" });
            }
        }

        private void MainForm_MenuStrip_Help_About_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHelp("getting-started", "about-application");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm),
                    contextData: new Dictionary<string, object> { ["HelpCategory"] = "getting-started", ["HelpTopic"] = "about-application" });
            }
        }

        #endregion

        #region Help System Methods

        /// <summary>
        /// Opens the new Help Viewer.
        /// </summary>
        /// <param name="categoryId">Optional category ID to open.</param>
        /// <param name="topicId">Optional topic ID to open.</param>
        private void ShowHelp(string? categoryId = null, string? topicId = null)
        {
            try
            {
                var helpForm = new Forms.Help.HelpViewerForm();
                helpForm.Show(); // Show non-modal
                
                if (!string.IsNullOrEmpty(categoryId))
                {
                    helpForm.ShowHelp(categoryId, topicId);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(MainForm));
            }
        }

        #endregion

        private void MainForm_MenuStrip_Development_DatabaseMaintenance_Click(object? sender, EventArgs e)
        {
            try
            {
                using var maintenanceForm = new Forms.Maintenance.Form_DatabaseMaintenance();
                maintenanceForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(MainForm));
            }
        }

        private void MainForm_MenuStrip_View_Analytics_Click(object? sender, EventArgs e)
        {
            try
            {
                using var analyticsForm = new Forms.WIPAppAnalytics.Form_WIPUserAnalytics();
                analyticsForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(MainForm));
            }
        }

        private void MainForm_MenuStrip_View_SystemHealth_Click(object? sender, EventArgs e)
        {
            try
            {
                if (Program.ServiceProvider == null) throw new InvalidOperationException("ServiceProvider is not initialized.");
                var devToolsService = Program.ServiceProvider.GetRequiredService<IService_DeveloperTools>();
                var errorHandler = Program.ServiceProvider.GetRequiredService<IService_ErrorHandler>();
                
                using var healthForm = new Forms.SystemHealth.Form_SystemHealth(devToolsService, errorHandler);
                healthForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(MainForm));
            }
        }

        private void InitializeDevelopmentMenuItems()
        {
            if (MainForm_MenuStrip_Development == null) return;

            // Database Maintenance
            bool hasDbMenu = false;
            foreach (ToolStripItem item in MainForm_MenuStrip_Development.DropDownItems)
            {
                if (item.Text == "Database Maintenance") hasDbMenu = true;
            }

            if (!hasDbMenu)
            {
                var dbMenu = new ToolStripMenuItem("Database Maintenance");
                dbMenu.Click += MainForm_MenuStrip_Development_DatabaseMaintenance_Click;
                MainForm_MenuStrip_Development.DropDownItems.Add(dbMenu);
            }
        }

        private void InitializeViewMenuItems()
        {
            if (MainForm_MenuStrip_View == null) return;

            bool hasAnalyticsMenu = false;
            foreach (ToolStripItem item in MainForm_MenuStrip_View.DropDownItems)
            {
                if (item.Text == "Material Handler Analytics") hasAnalyticsMenu = true;
            }

            if (!hasAnalyticsMenu)
            {
                // Add separator if needed
                if (MainForm_MenuStrip_View.DropDownItems.Count > 0 && 
                    !(MainForm_MenuStrip_View.DropDownItems[MainForm_MenuStrip_View.DropDownItems.Count - 1] is ToolStripSeparator))
                {
                     MainForm_MenuStrip_View.DropDownItems.Add(new ToolStripSeparator());
                }

                var analyticsMenu = new ToolStripMenuItem("Material Handler Analytics");
                analyticsMenu.Click += MainForm_MenuStrip_View_Analytics_Click;
                MainForm_MenuStrip_View.DropDownItems.Add(analyticsMenu);
            }
        }

        #region IConnectionRecoveryView Implementation

        public void UpdateSignalStrength(int strength, int ping)
        {
            if (MainForm_UserControl_SignalStrength != null)
            {
                MainForm_UserControl_SignalStrength.Strength = strength;
                MainForm_UserControl_SignalStrength.Ping = ping;
            }
        }

        public void SetApplicationEnabled(bool enabled)
        {
            if (MainForm_TabControl != null) MainForm_TabControl.Enabled = enabled;
            if (MainForm_SplitContainer_Middle?.Panel2 != null) MainForm_SplitContainer_Middle.Panel2.Enabled = enabled;
            if (MainForm_MenuStrip != null) MainForm_MenuStrip.Enabled = enabled;
        }

        public void SetReadyStatusVisible(bool visible)
        {
            if (MainForm_StatusText != null) MainForm_StatusText.Visible = visible;
        }

        public void SetDisconnectedStatusVisible(bool visible)
        {
            if (MainForm_StatusStrip_Disconnected != null) MainForm_StatusStrip_Disconnected.Visible = visible;
        }

        public void SetDisconnectedStatusText(string text)
        {
            if (MainForm_StatusStrip_Disconnected != null) MainForm_StatusStrip_Disconnected.Text = text;
        }

        #endregion
    }

    #endregion
}
#endregion
