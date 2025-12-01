using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using System.Linq;
using System.Data;
using MTM_WIP_Application_Winforms.Controls.Visual;
using System.Collections.Generic;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Dashboard form for viewing Infor Visual ERP data.
    /// </summary>
    public partial class InforVisualDashboard : ThemedForm
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private Control_DieToolDiscovery? _controlDieToolDiscovery;
        private Control_ReceivingAnalytics? _controlReceivingAnalytics;
        private Control_VisualInventory? _controlVisualInventory;
        private Control_InventoryAudit? _controlInventoryAudit;
        #endregion

        #region Properties
        // Intentionally left blank.
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InforVisualDashboard"/> class.
        /// </summary>
        public InforVisualDashboard()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            Load += InforVisualDashboard_Load;
        }
        #endregion

        #region Methods
        private async Task LoadCategoryDataAsync(Enum_VisualDashboardCategory category)
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available.");
                return;
            }

            if (category == Enum_VisualDashboardCategory.DieToolDiscovery)
            {
                ShowDieToolDiscoveryControl();
                return;
            }

            if (category == Enum_VisualDashboardCategory.Receiving)
            {
                ShowReceivingAnalyticsControl();
                return;
            }

            if (category == Enum_VisualDashboardCategory.Inventory)
            {
                ShowVisualInventoryControl();
                return;
            }

            if (category == Enum_VisualDashboardCategory.InventoryAuditing)
            {
                ShowInventoryAuditControl();
                return;
            }

            if (category == Enum_VisualDashboardCategory.Shipping ||
                category == Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General ||
                category == Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team)
            {
                Service_ErrorHandler.ShowUserError("This category is not yet implemented.");
                return;
            }

            HideCustomControls();

            try
            {
                SetLoadingState(true);
                controlEmptyState.Visible = false;

                var result = await _visualService.GetDashboardDataAsync(category);
                if (result.IsSuccess && result.Data != null)
                {
                    controlEmptyState.Message = string.Empty;

                    if (result.Data.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        controlEmptyState.Visible = true;
                        controlEmptyState.Message = "No records found for this category.";
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Failed to load data: {result.ErrorMessage}");
                    controlEmptyState.Visible = true;
                    controlEmptyState.Message = "Error loading data.";
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

                controlEmptyState.Visible = true;
                controlEmptyState.Message = "An unexpected error occurred.";
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool isLoading)
        {
            labelLoading.Visible = isLoading;
            panelSidebar.Enabled = !isLoading;


            if (isLoading)
            {
                controlEmptyState.Visible = false;
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
            if (sender is Button btn && btn.Tag is Enum_VisualDashboardCategory category)
            {
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

        private void HideGenericControls()
        {
            controlEmptyState.Visible = false;
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
        }

        private void EnsureDieToolDiscoveryControl()
        {
            if (_controlDieToolDiscovery != null)
            {
                return;
            }

            _controlDieToolDiscovery = new Control_DieToolDiscovery { Dock = DockStyle.Fill };
            panelContent.Controls.Add(_controlDieToolDiscovery);
        }

        private void EnsureReceivingAnalyticsControl()
        {
            if (_controlReceivingAnalytics != null)
            {
                return;
            }

            _controlReceivingAnalytics = new Control_ReceivingAnalytics { Dock = DockStyle.Fill };
            panelContent.Controls.Add(_controlReceivingAnalytics);
        }

        private void EnsureVisualInventoryControl()
        {
            if (_controlVisualInventory != null)
            {
                return;
            }

            _controlVisualInventory = new Control_VisualInventory { Dock = DockStyle.Fill };
            panelContent.Controls.Add(_controlVisualInventory);
        }

        private void EnsureInventoryAuditControl()
        {
            if (_controlInventoryAudit != null)
            {
                return;
            }

            _controlInventoryAudit = new Control_InventoryAudit { Dock = DockStyle.Fill };
            panelContent.Controls.Add(_controlInventoryAudit);
        }

        private string GetCategoryTitle(Enum_VisualDashboardCategory category)
        {
            return category switch
            {
                Enum_VisualDashboardCategory.Inventory => "Inventory",
                Enum_VisualDashboardCategory.Receiving => "Receiving",
                Enum_VisualDashboardCategory.Shipping => "Shipping",
                Enum_VisualDashboardCategory.InventoryAuditing => "Inventory Auditing",
                Enum_VisualDashboardCategory.DieToolDiscovery => "Die Tool Discovery",
                Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General => "MH Analytics (General)",
                Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team => "MH Analytics (Team)",
                _ => "Dashboard"
            };
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Load -= InforVisualDashboard_Load;
                _controlDieToolDiscovery?.Dispose();
                _controlReceivingAnalytics?.Dispose();
                _controlVisualInventory?.Dispose();
                _controlInventoryAudit?.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}
