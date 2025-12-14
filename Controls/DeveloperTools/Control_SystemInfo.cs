using System.Diagnostics;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    public partial class Control_SystemInfo : ThemedUserControl
    {
        private IService_DeveloperTools? _devToolsService;
        private IService_ErrorHandler? _errorHandler;
        private System.Threading.Timer? _refreshTimer;

        public Control_SystemInfo()
        {
            InitializeComponent();
            InitializeStaticInfo();
        }

        public void Initialize(IService_DeveloperTools devToolsService, IService_ErrorHandler errorHandler)
        {
            _devToolsService = devToolsService;
            _errorHandler = errorHandler;

            // Initial load of database stats
            if (!DesignMode)
            {
                _ = RefreshDatabaseStatsAsync();
            }

            // Start refresh timer
            _refreshTimer = new System.Threading.Timer(async _ =>
            {
                if (InvokeRequired) Invoke(new Action(async () => await RefreshDataAsync()));
                else await RefreshDataAsync();
            }, null, 0, 5000); // Refresh every 5 seconds
        }

        private void InitializeStaticInfo()
        {
            Control_SystemInfo_Label_OSValue.Text = Environment.OSVersion.ToString();
            Control_SystemInfo_Label_DotNetValue.Text = Environment.Version.ToString();
            Control_SystemInfo_Label_MachineValue.Text = Environment.MachineName;
            Control_SystemInfo_Label_UserValue.Text = $"{Environment.UserDomainName}\\{Environment.UserName}";
            Control_SystemInfo_Label_AppVersionValue.Text = Application.ProductVersion;
        }

        public async Task RefreshDataAsync()
        {
            if (_devToolsService == null) return;

            try
            {
                // Performance Metrics
                var metrics = _devToolsService.GetPerformanceMetrics();
                UpdatePerformanceUI(metrics);
            }
            catch (Exception ex)
            {
                // Log silently to avoid spamming errors on timer
                Debug.WriteLine($"Error refreshing system info: {ex.Message}");
            }
        }

        private void UpdatePerformanceUI(Model_PerformanceMetrics metrics)
        {
            Control_SystemInfo_Label_MemoryValue.Text = $"{metrics.MemoryUsageMB:F2} MB";
            Control_SystemInfo_Label_ThreadsValue.Text = metrics.ThreadCount.ToString();
            Control_SystemInfo_Label_HandlesValue.Text = metrics.HandleCount.ToString();

            var uptime = DateTime.Now - metrics.StartTime;
            Control_SystemInfo_Label_UptimeValue.Text = $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
        }

        private void UpdateDatabaseUI(Model_DatabaseHealth health)
        {
            Control_SystemInfo_Label_DbStatusValue.Text = health.StatusMessage;
            Control_SystemInfo_Label_DbStatusValue.ForeColor = health.IsConnected ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            Control_SystemInfo_Label_LastQueryValue.Text = health.LastSuccessfulQuery?.ToString("HH:mm:ss") ?? "-";

            if (health.IsConnected)
            {
                Control_SystemInfo_Label_ConnectionsValue.Text = health.ConnectionCount.ToString();
                var uptime = TimeSpan.FromSeconds(health.UptimeSeconds);
                Control_SystemInfo_Label_DbUptimeValue.Text = $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m";
                Control_SystemInfo_Label_DbVersionValue.Text = health.Version;
                Control_SystemInfo_Label_QueriesValue.Text = health.Queries.ToString("N0");
            }
            else
            {
                Control_SystemInfo_Label_ConnectionsValue.Text = "-";
                Control_SystemInfo_Label_DbUptimeValue.Text = "-";
                Control_SystemInfo_Label_DbVersionValue.Text = "-";
                Control_SystemInfo_Label_QueriesValue.Text = "-";
            }
        }

        private async void BtnRefreshDb_Click(object sender, EventArgs e)
        {
            await RefreshDatabaseStatsAsync();
        }

        public async Task RefreshDatabaseStatsAsync()
        {
            if (_devToolsService == null || _errorHandler == null) return;

            Control_SystemInfo_Button_RefreshDb.Enabled = false;
            Control_SystemInfo_Button_RefreshDb.Text = "Updating...";

            try
            {
                var result = await _devToolsService.GetDatabaseHealthAsync();
                if (result.IsSuccess && result.Data != null)
                {
                    UpdateDatabaseUI(result.Data);
                }
                else
                {
                    _errorHandler.ShowUserError($"Failed to update database stats: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium,
                    callerName: nameof(RefreshDatabaseStatsAsync),
                    controlName: this.Name);
            }
            finally
            {
                Control_SystemInfo_Button_RefreshDb.Enabled = true;
                Control_SystemInfo_Button_RefreshDb.Text = "Update Stats";
            }
        }

        private async void BtnTestConnection_Click(object sender, EventArgs e)
        {
            if (_devToolsService == null || _errorHandler == null) return;

            Control_SystemInfo_Button_TestConnection.Enabled = false;
            Control_SystemInfo_Button_TestConnection.Text = "Testing...";

            try
            {
                var stopwatch = Stopwatch.StartNew();
                var result = await _devToolsService.GetDatabaseHealthAsync();
                stopwatch.Stop();

                if (result.IsSuccess && result.Data != null && result.Data.IsConnected)
                {
                    _errorHandler.ShowInformation($"Connection Successful!\nLatency: {stopwatch.ElapsedMilliseconds}ms", "Database Test");
                    UpdateDatabaseUI(result.Data);
                }
                else
                {
                    _errorHandler.ShowUserError($"Connection Failed.\nError: {result.ErrorMessage}", "Database Test");
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium,
                    callerName: nameof(BtnTestConnection_Click),
                    controlName: this.Name);
            }
            finally
            {
                Control_SystemInfo_Button_TestConnection.Enabled = true;
                Control_SystemInfo_Button_TestConnection.Text = "Test Connection";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Control_SystemInfo_Label_DbUptimeValue_Click(object sender, EventArgs e)
        {

        }
    }
}

