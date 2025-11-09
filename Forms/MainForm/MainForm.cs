using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;
using MTM_WIP_Application_Winforms.Controls.MainForm;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.ErrorDialog;
using MTM_WIP_Application_Winforms.Forms.ErrorReports;
using MTM_WIP_Application_Winforms.Forms.Settings;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;
using Timer = System.Windows.Forms.Timer;

namespace MTM_WIP_Application_Winforms.Forms.MainForm
{
    #region MainForm

    public partial class MainForm : Form
    {
        #region Fields

        private Timer? _connectionStrengthTimer;
        public Helper_Control_MySqlSignal ConnectionStrengthChecker = null!;
        private Helper_StoredProcedureProgress? _progressHelper;
        private Forms.Development.DebugDashboardForm? _debugDashboard;
    private Form_ViewErrorReports? _viewErrorReportsForm;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Service_ConnectionRecoveryManager ConnectionRecoveryManager { get; private set; } = null!;

        public Helper_StoredProcedureProgress? ProgressHelper => _progressHelper;

        private CancellationTokenSource? _batchCancelTokenSource;

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
                Core_Themes.ApplyDpiScaling(this); // Allowed: Form initialization
                Core_Themes.ApplyRuntimeLayoutAdjustments(this); // Allowed: Form initialization

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
                        Debug.WriteLine("[DEBUG] [MainForm.ctor] User full name loaded.");

                        // Configure Development Menu visibility based on username
                        ConfigureDevelopmentMenuVisibility();

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
        /// Only users JKOLL or JOHNK can access the Development Menu
        /// </summary>
        private void ConfigureDevelopmentMenuVisibility()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["CurrentUser"] = Model_Application_Variables.User ?? "Unknown",
                ["DevelopmentMenuExists"] = developmentToolStripMenuItem != null
            }, nameof(ConfigureDevelopmentMenuVisibility), nameof(MainForm));

            try
            {
                string currentUser = Model_Application_Variables.User?.ToUpperInvariant() ?? "";
                bool isDeveloper = currentUser == "JKOLL" || currentUser == "JOHNK";

                if (developmentToolStripMenuItem != null)
                {
                    developmentToolStripMenuItem.Visible = isDeveloper;

                    Service_DebugTracer.TraceBusinessLogic("DEVELOPMENT_MENU_VISIBILITY",
                        inputData: new {
                            User = Model_Application_Variables.User,
                            UserUpperCase = currentUser,
                            IsDeveloper = isDeveloper
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
                }
                else
                {
                    Service_DebugTracer.TraceUIAction("DEVELOPMENT_MENU_NOT_FOUND", nameof(MainForm),
                        new Dictionary<string, object>
                        {
                            ["Warning"] = "developmentToolStripMenuItem is null"
                        });
                    LoggingUtility.Log("Development Menu item not found during visibility configuration");
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

        private void SetInitialFocusToInventoryTab()
        {
            try
            {
                if (MainForm_UserControl_InventoryTab?.Control_InventoryTab_ComboBox_Part != null)
                {
                    MainForm_UserControl_InventoryTab.Control_InventoryTab_ComboBox_Part.Focus();
                    MainForm_UserControl_InventoryTab.Control_InventoryTab_ComboBox_Part.SelectAll();
                    MainForm_UserControl_InventoryTab.Control_InventoryTab_ComboBox_Part.BackColor =
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
                string text = isCollapsed ? "⬅️" : "➡️";

                UpdateToggleButtonText(MainForm_UserControl_InventoryTab, text);
                UpdateToggleButtonText(MainForm_UserControl_RemoveTab, text);
                UpdateToggleButtonText(MainForm_UserControl_TransferTab, text);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private static void UpdateToggleButtonText(Control? control, string text)
        {
            if (control == null) return;

            try
            {
                // Use safe control search instead of reflection
                var toggleButtons = control.Controls.Find("*Toggle_RightPanel", true)
                    .OfType<Button>()
                    .Where(b => b.Name.Contains("Toggle_RightPanel"));

                foreach (Button btn in toggleButtons)
                {
                    btn.Text = text;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Debug.WriteLine($"[DEBUG] Error updating toggle button text for {control.Name}: {ex.Message}");
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
                        LoggingUtility.Log("Display settings changed - prompting user for restart");

                        // Note: We don't auto-refresh here because the DpiChanged event
                        // on the form will handle it and prompt the user
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                    }
                };

                LoggingUtility.Log("DPI change event handlers wired up successfully");
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
                LoggingUtility.Log($"MainForm DPI changed from {e.DeviceDpiOld} to {e.DeviceDpiNew}");

                // Calculate scaling percentage for user-friendly message
                int oldPercent = (int)Math.Round(e.DeviceDpiOld / 96.0 * 100);
                int newPercent = (int)Math.Round(e.DeviceDpiNew / 96.0 * 100);

                // Prompt user for restart or auto-resize
                var result = MessageBox.Show(
                    $"Display scaling has changed from {oldPercent}% to {newPercent}%.\n\n" +
                    "For the best appearance, it is recommended to restart the application.\n\n" +
                    "Click 'Yes' to restart now (recommended)\n" +
                    "Click 'No' to automatically resize all forms (may cause visual glitches)\n" +
                    "Click 'Cancel' to continue without changes",
                    "Display Scaling Changed",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    // User chose to restart - save state and restart application
                    LoggingUtility.Log("User chose to restart application after DPI change");

                    // Get the application executable path
                    string appPath = Application.ExecutablePath;

                    // Start new instance
                    Process.Start(appPath);

                    // Close current instance gracefully
                    Application.Exit();
                }
                else if (result == DialogResult.No)
                {
                    // User chose auto-resize - use the Core_Themes DPI change handler
                    LoggingUtility.Log("User chose auto-resize after DPI change");
                    Core_Themes.HandleDpiChanged(this, e.DeviceDpiOld, e.DeviceDpiNew);
                    Core_Themes.ApplyTheme(this);
                    LoggingUtility.Log("MainForm DPI change handling completed");
                }
                else
                {
                    // User cancelled - do nothing
                    LoggingUtility.Log("User cancelled DPI change handling");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Debug.WriteLine($"[DEBUG] Error handling DPI change: {ex.Message}");
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

        #endregion

        #region Tab Control

        private void MainForm_TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
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

        private async void MainForm_TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                await ShowTabLoadingProgressAsync();
                Debug.WriteLine("Resetting user controls...");

                await ResetAllUserControlsAsync();

                // Update Quick Buttons toggle text for all tabs
                UpdateQuickButtonsToggleTextForAllTabs();

                SetTabVisibility();
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
            }
        }

        #region Tab Control Helper Methods

        private async Task ResetAllUserControlsAsync()
        {
            try
            {
                var resetTasks = new List<Task>();

                // Create reset tasks for each user control
                if (MainForm_UserControl_InventoryTab != null)
                    resetTasks.Add(Task.Run(() => InvokeResetMethod(MainForm_UserControl_InventoryTab, "Control_InventoryTab_SoftReset")));

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
                Debug.WriteLine($"Attempting to invoke {methodName} on {control.GetType().Name}");

                MethodInfo? method = control.GetType().GetMethod(methodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (method != null)
                {
                    Debug.WriteLine($"Invoking {method.Name} on {control.GetType().Name}");

                    // Ensure method is invoked on UI thread if needed
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new Action(() => method.Invoke(control, null)));
                    }
                    else
                    {
                        method.Invoke(control, null);
                    }
                }
                else
                {
                    Debug.WriteLine($"Method {methodName} not found on {control.GetType().Name}");
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
                        MainForm_UserControl_InventoryTab?.Control_InventoryTab_ComboBox_Part?.Focus();
                        break;
                    case 1:
                        MainForm_UserControl_RemoveTab?.Control_RemoveTab_ComboBox_Part?.Focus();
                        break;
                    case 2:
                        MainForm_UserControl_TransferTab?.Control_TransferTab_ComboBox_Part?.Focus();
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
                if (keyData == Core_WipAppVariables.Shortcut_MainForm_Tab1)
                {
                    MainForm_TabControl.SelectedIndex = 0;
                    return true;
                }

                if (keyData == Core_WipAppVariables.Shortcut_MainForm_Tab2)
                {
                    MainForm_TabControl.SelectedIndex = 1;
                    return true;
                }

                if (keyData == Core_WipAppVariables.Shortcut_MainForm_Tab3)
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

        #endregion

        #region Form Closing

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _connectionStrengthTimer?.Stop();
                _connectionStrengthTimer?.Dispose();

                // Close Debug Dashboard if it's open
                if (_debugDashboard != null && !_debugDashboard.IsDisposed)
                {
                    _debugDashboard.Close();
                    _debugDashboard.Dispose();
                    _debugDashboard = null;
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

                Service_DebugTracer.TraceUIAction("THEME_REAPPLY", nameof(MainForm),
                    new Dictionary<string, object> { ["Reason"] = "SettingsChanged" });
                Core_Themes.ApplyTheme(this);
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

        private async void MainForm_MenuStrip_View_PersonalHistory_Click(object sender, EventArgs e)
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

        private async void MainForm_MenuStrip_Development_DebugDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                using var debugDashboard = new Forms.Development.DebugDashboardForm();
                debugDashboard.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: nameof(MainForm_MenuStrip_Development_DebugDashboard_Click));
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

        private async void MainForm_MenuStrip_Development_Conversion_Click(object sender, EventArgs e)
        {
            try
            {
                using var converterForm = new Forms.Development.DependencyChartConverter.DependencyChartConverterForm();
                converterForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: nameof(MainForm_MenuStrip_Development_Conversion_Click));
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

        #region Help Menu Event Handlers

        private void MainForm_MenuStrip_Help_GettingStarted_Click(object sender, EventArgs e)
        {
            try
            {
                OpenHelpFile("getting-started.html");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm),
                    contextData: new Dictionary<string, object> { ["HelpFile"] = "getting-started.html" });
            }
        }

        private void MainForm_MenuStrip_Help_UserGuide_Click(object sender, EventArgs e)
        {
            try
            {
                OpenHelpFile("index.html");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm),
                    contextData: new Dictionary<string, object> { ["HelpFile"] = "index.html" });
            }
        }

        private void MainForm_MenuStrip_Help_KeyboardShortcuts_Click(object sender, EventArgs e)
        {
            try
            {
                OpenHelpFile("keyboard-shortcuts.html");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(MainForm),
                    contextData: new Dictionary<string, object> { ["HelpFile"] = "keyboard-shortcuts.html" });
            }
        }

        private void MainForm_MenuStrip_Help_About_Click(object sender, EventArgs e)
        {
            try
            {
                var aboutMessage = $"MTM Inventory Application\n" +
                                  $"Version: {Assembly.GetExecutingAssembly().GetName().Version}\n" +
                                  $"© 2025 Manitowoc Tool and Manufacturing\n\n" +
                                  $"Built with .NET 8 and Windows Forms\n" +
                                  $"Database: MySQL with stored procedures\n" +
                                  $"Environment: {(Model_Shared_Users.Database == "MTM_WIP_Application_Winforms" ? "Release" : "Debug")}";

                Service_ErrorHandler.ShowInformation("About MTM Inventory", aboutMessage);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, controlName: nameof(MainForm));
            }
        }

        #endregion

        #region Help System Methods

        /// <summary>
        /// Opens a help file using the default browser
        /// </summary>
        /// <param name="fileName">Name of the help file (e.g., "getting-started.html")</param>
        private void OpenHelpFile(string fileName)
        {
            try
            {
                var helpPath = Path.Combine(Application.StartupPath, "Documentation", "Help", fileName);

                if (!File.Exists(helpPath))
                {
                    // If file doesn't exist locally, create a basic error message
                    var errorMessage = $"Help file not found: {fileName}\n\n" +
                                     $"Expected location: {helpPath}\n\n" +
                                     $"Please ensure the Documentation/Help folder exists and contains the help files.";
                    Service_ErrorHandler.ShowWarning("Help File Missing", errorMessage);
                    return;
                }

                // Try to open with default browser
                var startInfo = new ProcessStartInfo
                {
                    FileName = helpPath,
                    UseShellExecute = true,
                    Verb = "open"
                };

                Process.Start(startInfo);
                LoggingUtility.LogApplicationInfo($"Opened help file: {fileName}");
            }
            catch (Exception ex)
            {
                var fallbackMessage = $"Unable to open help file: {fileName}\n\n" +
                                    $"Error: {ex.Message}\n\n" +
                                    $"Please check that you have a web browser installed and configured as default.";
                Service_ErrorHandler.ShowWarning("Cannot Open Help", fallbackMessage);
            }
        }

        #endregion

        private async void viewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var viewerForm = new Forms.Development.DependencyChartConverter.DependencyChartViewerForm();
                viewerForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: nameof(viewerToolStripMenuItem_Click));
            }
        }
    }

    #endregion
}
#endregion
