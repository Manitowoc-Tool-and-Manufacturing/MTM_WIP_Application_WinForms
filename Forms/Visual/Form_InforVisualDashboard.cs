using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Controls.Visual;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Dashboard form for viewing Infor Visual ERP data.
    /// </summary>
    public partial class Form_InforVisualDashboard : ThemedForm
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private Control_DieToolDiscovery? _controlDieToolDiscovery;
        private Control_ReceivingAnalytics? _controlReceivingAnalytics;
        private Control_VisualInventory? _controlVisualInventory;
        private Control_InventoryAudit? _controlInventoryAudit;
        private Control_VisualUserAnalytics? _controlVisualUserAnalytics;
        #endregion

        #region Properties
        // Intentionally left blank.
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Form_InforVisualDashboard"/> class.
        /// </summary>
        public Form_InforVisualDashboard()
        {
            InitializeComponent();
            
            // Prevent accessing DI container at design time
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            }
            
            WireUpEvents();
        }
        #endregion

        #region Methods
        private void WireUpEvents()
        {
            Load += InforVisualDashboard_Load;
            
            // Wire up category buttons
            InforVisualDashboard_Button_Inventory.Click += CategoryButton_Click;
            InforVisualDashboard_Button_Receiving.Click += CategoryButton_Click;
            InforVisualDashboard_Button_Shipping.Click += CategoryButton_Click;
            InforVisualDashboard_Button_InventoryAuditing.Click += CategoryButton_Click;
            InforVisualDashboard_Button_DieToolDiscovery.Click += CategoryButton_Click;
            InforVisualDashboard_Button_MaterialHandlerGeneral.Click += CategoryButton_Click;
        }

        private async Task LoadCategoryDataAsync(Enum_VisualDashboardCategory category)
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available.");
                return;
            }

            if (category == Enum_VisualDashboardCategory.DieToolDiscovery)
            {
                try
                {
                    ShowDieToolDiscoveryControl();
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(LoadCategoryDataAsync), controlName: Name);
                }
                return;
            }

            if (category == Enum_VisualDashboardCategory.Receiving)
            {
                try
                {
                    ShowReceivingAnalyticsControl();
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(LoadCategoryDataAsync), controlName: Name);
                }
                return;
            }

            if (category == Enum_VisualDashboardCategory.Inventory)
            {
                try
                {
                    ShowVisualInventoryControl();
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(LoadCategoryDataAsync), controlName: Name);
                }
                return;
            }

            if (category == Enum_VisualDashboardCategory.InventoryAuditing)
            {
                try
                {
                    ShowInventoryAuditControl();
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(LoadCategoryDataAsync), controlName: Name);
                }
                return;
            }

            if (category == Enum_VisualDashboardCategory.Shipping)
            {
                Service_ErrorHandler.ShowUserError("This category is not yet implemented.");
                return;
            }

            if (category == Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General ||
                category == Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team)
            {
                try
                {
                    ShowMaterialHandlerAnalyticsControl();
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(LoadCategoryDataAsync), controlName: Name);
                }
                return;
            }

            HideCustomControls();

            try
            {
                SetLoadingState(true);
                InforVisualDashboard_Control_EmptyState.Visible = false;

                var result = await _visualService.GetDashboardDataAsync(category);
                if (result.IsSuccess && result.Data != null)
                {
                    InforVisualDashboard_Control_EmptyState.Message = string.Empty;

                    if (result.Data.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        InforVisualDashboard_Control_EmptyState.Visible = true;
                        InforVisualDashboard_Control_EmptyState.Message = "No records found for this category.";
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Failed to load data: {result.ErrorMessage}");
                    InforVisualDashboard_Control_EmptyState.Visible = true;
                    InforVisualDashboard_Control_EmptyState.Message = "Error loading data.";
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Category"] = category.ToString()
                    },
                    callerName: nameof(LoadCategoryDataAsync),
                    controlName: Name);

                InforVisualDashboard_Control_EmptyState.Visible = true;
                InforVisualDashboard_Control_EmptyState.Message = "An unexpected error occurred.";
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool isLoading)
        {
            InforVisualDashboard_Panel_Navigation.Enabled = !isLoading;

            if (isLoading)
            {
                InforVisualDashboard_Control_EmptyState.Visible = false;
            }
        }
        #endregion

        #region Events
        private async void InforVisualDashboard_Load(object? sender, EventArgs e)
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available.");
                return;
            }

            try
            {
                var result = await _visualService.TestConnectionAsync();
                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.ShowUserError($"Connection to Visual ERP failed: {result.ErrorMessage}");
                }
                else
                {
                    // Load default category
                    await LoadCategoryDataAsync(Enum_VisualDashboardCategory.Inventory);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Stage"] = "TestConnection"
                    },
                    callerName: nameof(InforVisualDashboard_Load),
                    controlName: Name);
            }
        }

        private async void CategoryButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                LoggingUtility.Log($"Category button clicked: {btn.Name}, Tag: {btn.Tag}");
                
                Enum_VisualDashboardCategory category;
                if (btn.Tag is Enum_VisualDashboardCategory cat)
                {
                    category = cat;
                }
                else
                {
                    // Fallback based on name
                    if (btn.Name == "InforVisualDashboard_Button_Receiving") category = Enum_VisualDashboardCategory.Receiving;
                    else if (btn.Name == "InforVisualDashboard_Button_Inventory") category = Enum_VisualDashboardCategory.Inventory;
                    else if (btn.Name == "InforVisualDashboard_Button_Shipping") category = Enum_VisualDashboardCategory.Shipping;
                    else if (btn.Name == "InforVisualDashboard_Button_InventoryAuditing") category = Enum_VisualDashboardCategory.InventoryAuditing;
                    else if (btn.Name == "InforVisualDashboard_Button_DieToolDiscovery") category = Enum_VisualDashboardCategory.DieToolDiscovery;
                    else if (btn.Name == "InforVisualDashboard_Button_MaterialHandlerGeneral") category = Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General;
                    else if (btn.Name == "InforVisualDashboard_Button_MaterialHandlerTeam") category = Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team;
                    else
                    {
                        Service_ErrorHandler.ShowUserError($"Invalid category configuration for button: {btn.Name}. Tag is {btn.Tag?.GetType().Name ?? "null"}");
                        return;
                    }
                    
                    // Fix the tag for next time
                    btn.Tag = category;
                }

                await LoadCategoryDataAsync(category);
            }
        }
        #endregion

        #region Helpers
        private void ShowDieToolDiscoveryControl()
        {
            HideGenericControls();
            EnsureDieToolDiscoveryControl();
            HideAllCustomControls(_controlDieToolDiscovery);
            _controlDieToolDiscovery!.Visible = true;
            CenterFormOnScreen();
        }

        private void ShowReceivingAnalyticsControl()
        {
            HideGenericControls();
            EnsureReceivingAnalyticsControl();
            HideAllCustomControls(_controlReceivingAnalytics);
            _controlReceivingAnalytics!.Visible = true;
            CenterFormOnScreen();
        }

        private void ShowVisualInventoryControl()
        {
            HideGenericControls();
            EnsureVisualInventoryControl();
            HideAllCustomControls(_controlVisualInventory);
            _controlVisualInventory!.Visible = true;
            CenterFormOnScreen();
        }

        private void ShowInventoryAuditControl()
        {
            HideGenericControls();
            EnsureInventoryAuditControl();
            HideAllCustomControls(_controlInventoryAudit);
            _controlInventoryAudit!.Visible = true;
            CenterFormOnScreen();
        }

        private void ShowMaterialHandlerAnalyticsControl()
        {
            HideGenericControls();
            EnsureVisualUserAnalyticsControl();
            HideAllCustomControls(_controlVisualUserAnalytics);
            _controlVisualUserAnalytics!.Visible = true;
            CenterFormOnScreen();
        }

        private void HideGenericControls()
        {
            InforVisualDashboard_Control_EmptyState.Visible = false;
        }

        private void HideCustomControls()
        {
            HideAllCustomControls();
        }

        private void HideAllCustomControls(Control? exception = null)
        {
            if (_controlDieToolDiscovery != null)
            {
                _controlDieToolDiscovery.Visible = _controlDieToolDiscovery == exception;
            }

            if (_controlReceivingAnalytics != null)
            {
                _controlReceivingAnalytics.Visible = _controlReceivingAnalytics == exception;
            }

            if (_controlVisualInventory != null)
            {
                _controlVisualInventory.Visible = _controlVisualInventory == exception;
            }

            if (_controlInventoryAudit != null)
            {
                _controlInventoryAudit.Visible = _controlInventoryAudit == exception;
            }

            if (_controlVisualUserAnalytics != null)
            {
                _controlVisualUserAnalytics.Visible = _controlVisualUserAnalytics == exception;
            }
        }

        private void EnsureDieToolDiscoveryControl()
        {
            if (_controlDieToolDiscovery != null)
            {
                return;
            }

            _controlDieToolDiscovery = new Control_DieToolDiscovery { Dock = DockStyle.Fill };
            InforVisualDashboard_Panel_Content.Controls.Add(_controlDieToolDiscovery);
            _controlDieToolDiscovery.BringToFront();
        }

        private void EnsureReceivingAnalyticsControl()
        {
            if (_controlReceivingAnalytics != null)
            {
                return;
            }

            _controlReceivingAnalytics = new Control_ReceivingAnalytics { Dock = DockStyle.Fill };
            InforVisualDashboard_Panel_Content.Controls.Add(_controlReceivingAnalytics);
            _controlReceivingAnalytics.BringToFront();
        }

        private void EnsureVisualInventoryControl()
        {
            if (_controlVisualInventory != null)
            {
                return;
            }

            _controlVisualInventory = new Control_VisualInventory { Dock = DockStyle.Fill };
            InforVisualDashboard_Panel_Content.Controls.Add(_controlVisualInventory);
            _controlVisualInventory.BringToFront();
        }

        private void EnsureInventoryAuditControl()
        {
            if (_controlInventoryAudit != null)
            {
                return;
            }

            _controlInventoryAudit = new Control_InventoryAudit { Dock = DockStyle.Fill };
            InforVisualDashboard_Panel_Content.Controls.Add(_controlInventoryAudit);
            _controlInventoryAudit.BringToFront();
        }

        private void EnsureVisualUserAnalyticsControl()
        {
            if (_controlVisualUserAnalytics != null)
            {
                return;
            }

            _controlVisualUserAnalytics = new Control_VisualUserAnalytics { Dock = DockStyle.Fill };
            InforVisualDashboard_Panel_Content.Controls.Add(_controlVisualUserAnalytics);
            _controlVisualUserAnalytics.BringToFront();
        }

        /// <summary>
        /// Opens the Inventory Search tab and performs a search for the specified part number.
        /// </summary>
        /// <param name="partNumber">The part number to search for.</param>
        public async Task OpenInventorySearchAsync(string partNumber)
        {
            ShowVisualInventoryControl();
            if (_controlVisualInventory != null)
            {
                await _controlVisualInventory.PerformExternalSearchAsync(partNumber);
            }
        }

        /// <summary>
        /// Selects and loads a specific dashboard category programmatically.
        /// </summary>
        /// <param name="category">The category to load.</param>
        public async Task SelectCategoryAsync(Enum_VisualDashboardCategory category)
        {
            await LoadCategoryDataAsync(category);
        }

        /// <summary>
        /// Centers the form on the screen.
        /// </summary>
        private void CenterFormOnScreen()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(CenterFormOnScreen));
                return;
            }
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.CenterToScreen();
        }
        #endregion

        #region Cleanup / Dispose
        // Note: Controls added to the Controls collection (like _controlDieToolDiscovery) 
        // are automatically disposed when the Form is disposed.
        // Manual disposal is only needed for resources NOT added to the UI tree.
        #endregion
    }
}
