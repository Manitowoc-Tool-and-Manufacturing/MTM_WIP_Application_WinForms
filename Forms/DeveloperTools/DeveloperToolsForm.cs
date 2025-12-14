using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;

namespace MTM_WIP_Application_Winforms.Forms.DeveloperTools
{
    public partial class DeveloperToolsForm : ThemedForm
    {
        #region Fields

        private readonly IService_DeveloperTools _devToolsService;
        private readonly ILoggingService _logger;
        private readonly IService_ErrorHandler _errorHandler;
        private readonly IService_FeedbackManager _feedbackManager;

        #endregion

        #region Constructor

        public DeveloperToolsForm(
            IService_DeveloperTools devToolsService,
            ILoggingService logger,
            IService_ErrorHandler errorHandler,
            IService_FeedbackManager feedbackManager)
        {
            InitializeComponent();
            _devToolsService = devToolsService;
            _logger = logger;
            _errorHandler = errorHandler;
            _feedbackManager = feedbackManager;

            InitializeCustomControls();
        }

        #endregion

        #region Initialization

        private void InitializeCustomControls()
        {
            DeveloperToolsForm_Control_LogViewer.Initialize(_devToolsService, _errorHandler);
            DeveloperToolsForm_Control_SystemInfo.Initialize(_devToolsService, _errorHandler);
            DeveloperToolsForm_Control_FeedbackManager.Initialize(_feedbackManager, _errorHandler);
        }

        #endregion

        #region Event Handlers

        private async void DeveloperToolsForm_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadDashboardAsync();
                DeveloperToolsForm_Control_RecentErrors.ErrorSelected += DeveloperToolsForm_Control_RecentErrors_ErrorSelected;
                DeveloperToolsForm_Control_SystemHealth.StatusClicked += DeveloperToolsForm_Control_SystemHealth_StatusClicked;
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, 
                    callerName: nameof(DeveloperToolsForm_Load), 
                    controlName: this.Name);
            }
        }

        private void DeveloperToolsForm_Control_SystemHealth_StatusClicked(object? sender, EventArgs e)
        {
            DeveloperToolsForm_TabControl_Main.SelectedTab = DeveloperToolsForm_TabPage_Logs;
            DeveloperToolsForm_Control_LogViewer.FilterByDateRange("Today");
        }

        private async void DeveloperToolsForm_Control_RecentErrors_ErrorSelected(object? sender, Model_DevLogEntry e)
        {
            // Navigate to Logs tab and filter by this error
            DeveloperToolsForm_TabControl_Main.SelectedTab = DeveloperToolsForm_TabPage_Logs;
            await DeveloperToolsForm_Control_LogViewer.ShowLogEntryAsync(e);
        }

        private async void DeveloperToolsForm_Button_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadDashboardAsync();
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, 
                    callerName: nameof(DeveloperToolsForm_Button_Refresh_Click), 
                    controlName: this.Name);
            }
        }

        #endregion

        #region Methods

        private async Task LoadDashboardAsync()
        {
            try
            {
                // 1. System Health
                var healthResult = await _devToolsService.GetSystemHealthAsync();
                if (healthResult.IsSuccess && healthResult.Data != null)
                {
                    DeveloperToolsForm_Control_SystemHealth.UpdateHealth(healthResult.Data);
                }

                // 2. Log Statistics (24h)
                var statsResult = await _devToolsService.GetLogStatisticsAsync(DateTime.Now.AddHours(-24), DateTime.Now);
                if (statsResult.IsSuccess && statsResult.Data != null)
                {
                    DeveloperToolsForm_Control_LogStatistics.UpdateStatistics(statsResult.Data);
                }

                // 3. Recent Errors
                var recentErrorsResult = await _devToolsService.GetRecentErrorsAsync(10);
                if (recentErrorsResult.IsSuccess && recentErrorsResult.Data != null)
                {
                    DeveloperToolsForm_Control_RecentErrors.UpdateErrors(recentErrorsResult.Data);
                }

                // 4. System Info
                await DeveloperToolsForm_Control_SystemInfo.RefreshDataAsync();
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, 
                    callerName: nameof(LoadDashboardAsync), 
                    controlName: this.Name);
            }
        }

        #endregion
    }
}

