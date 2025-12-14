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
        private System.Windows.Forms.Timer _autoRefreshTimer;
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
            _autoRefreshTimer = new System.Windows.Forms.Timer();

            _ = InitializeCustomControls();
        }

        #endregion

        #region Initialization

        private async Task InitializeCustomControls()
        {
            DeveloperToolsForm_Control_LogViewer.Initialize(_devToolsService, _errorHandler);
            DeveloperToolsForm_Control_SystemInfo.Initialize(_devToolsService, _errorHandler);
            await DeveloperToolsForm_Control_FeedbackManager.Initialize(_feedbackManager, _errorHandler);
            DeveloperToolsForm_Control_LogStatistics.Initialize(_devToolsService, _errorHandler);
            
            DeveloperToolsForm_TabControl_Main.SelectedIndexChanged += DeveloperToolsForm_TabControl_Main_SelectedIndexChanged;
            DeveloperToolsForm_Control_DatabaseHealth.Initialize(_devToolsService, _errorHandler);
        }

        public void SelectLogsTab()
        {
            DeveloperToolsForm_TabControl_Main.SelectedTab = DeveloperToolsForm_TabPage_Logs;
        }

        public void SelectTab(int index)
        {
            if (index >= 0 && index < DeveloperToolsForm_TabControl_Main.TabCount)
            {
                DeveloperToolsForm_TabControl_Main.SelectedIndex = index;
            }
        }

        #endregion

        #region Event Handlers

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Tab Navigation
            if (keyData == (Keys.Control | Keys.D1))
            {
                DeveloperToolsForm_TabControl_Main.SelectedTab = DeveloperToolsForm_TabPage_Dashboard;
                return true;
            }
            if (keyData == (Keys.Control | Keys.D2))
            {
                DeveloperToolsForm_TabControl_Main.SelectedTab = DeveloperToolsForm_TabPage_Logs;
                return true;
            }
            if (keyData == (Keys.Control | Keys.D3))
            {
                DeveloperToolsForm_TabControl_Main.SelectedTab = DeveloperToolsForm_TabPage_Feedback;
                return true;
            }
            if (keyData == (Keys.Control | Keys.D4))
            {
                DeveloperToolsForm_TabControl_Main.SelectedTab = DeveloperToolsForm_TabPage_SystemInfo;
                return true;
            }

            // Search Focus
            if (keyData == (Keys.Control | Keys.F))
            {
                DeveloperToolsForm_TabControl_Main.SelectedTab = DeveloperToolsForm_TabPage_Logs;
                DeveloperToolsForm_Control_LogViewer.FocusSearch();
                return true;
            }

            // Refresh
            if (keyData == (Keys.Control | Keys.R) || keyData == Keys.F5)
            {
                _ = RefreshCurrentTabAsync();
                return true;
            }

            // Clear Filters
            if (keyData == Keys.Escape)
            {
                if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_Logs)
                {
                    DeveloperToolsForm_Control_LogViewer.ClearFilters();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async Task RefreshCurrentTabAsync()
        {
            if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_Dashboard)
            {
                await LoadDashboardAsync();
            }
            else if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_Logs)
            {
                await DeveloperToolsForm_Control_LogViewer.RefreshLogsAsync();
            }
            else if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_Feedback)
            {
                await DeveloperToolsForm_Control_FeedbackManager.LoadDataAsync();
            }
            else if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_SystemInfo)
            {
                await DeveloperToolsForm_Control_SystemInfo.RefreshDataAsync();
            }
        }

        private async void DeveloperToolsForm_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadDashboardAsync();
                DeveloperToolsForm_Control_RecentErrors.ErrorSelected += DeveloperToolsForm_Control_RecentErrors_ErrorSelected;
                DeveloperToolsForm_Control_SystemHealth.StatusClicked += DeveloperToolsForm_Control_SystemHealth_StatusClicked;

                _autoRefreshTimer.Interval = 30000; // 30 seconds
                _autoRefreshTimer.Tick += async (s, args) => 
                {
                    if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_Logs)
                    {
                        await DeveloperToolsForm_Control_LogViewer.RefreshLogsAsync();
                    }
                };
                _autoRefreshTimer.Start();
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

        private async void DeveloperToolsForm_TabControl_Main_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_Dashboard)
                {
                    await LoadDashboardAsync();
                }
                else if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_Logs)
                {
                    // LogViewer usually loads on init or has its own refresh. 
                    // We can trigger a refresh if needed.
                }
                else if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_Feedback)
                {
                    await DeveloperToolsForm_Control_FeedbackManager.LoadDataAsync();
                }
                else if (DeveloperToolsForm_TabControl_Main.SelectedTab == DeveloperToolsForm_TabPage_SystemInfo)
                {
                    await DeveloperToolsForm_Control_SystemInfo.RefreshDataAsync();
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, 
                    callerName: nameof(DeveloperToolsForm_TabControl_Main_SelectedIndexChanged), 
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

