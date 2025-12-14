using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

namespace MTM_WIP_Application_Winforms.Forms.SystemHealth
{
    public partial class Form_SystemHealth : ThemedForm
    {
        #region Fields

        private readonly IService_DeveloperTools _devToolsService;
        private readonly IService_ErrorHandler _errorHandler;
        private readonly string _currentUserId;
        private readonly System.Windows.Forms.Timer _refreshTimer;

        #endregion

        #region Constructors

        public Form_SystemHealth(
            IService_DeveloperTools devToolsService,
            IService_ErrorHandler errorHandler)
        {
            _devToolsService = devToolsService ?? throw new ArgumentNullException(nameof(devToolsService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            _currentUserId = Model_Application_Variables.User;
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 30000; // 30 seconds
            _refreshTimer.Tick += async (s, e) => await LoadHealthStatusAsync();

            InitializeComponent();
        }

        #endregion

        #region Methods

        private async void Form_SystemHealth_Load(object sender, EventArgs e)
        {
            await LoadHealthStatusAsync();
            await LoadUserFeedbackAsync();
            _refreshTimer.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _refreshTimer.Stop();
            base.OnFormClosed(e);
        }

        private async Task LoadHealthStatusAsync()
        {
            try
            {
                var result = await _devToolsService.GetSystemHealthAsync();
                if (result.IsSuccess && result.Data != null)
                {
                    UpdateHealthIndicator(result.Data);
                }
                else
                {
                    _errorHandler.ShowUserError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    callerName: nameof(LoadHealthStatusAsync),
                    controlName: Name);
            }
        }

        private void UpdateHealthIndicator(Model_SystemHealthStatus status)
        {
            Form_SystemHealth_Label_HealthMessage.Text = status.Message;
            
            switch (status.Status)
            {
                case Enum_HealthIndicator.Green:
                    Form_SystemHealth_Panel_HealthIndicator.BackColor = Color.FromArgb(46, 204, 113); // Green
                    Form_SystemHealth_Label_HealthIcon.Text = "✅";
                    break;
                case Enum_HealthIndicator.Yellow:
                    Form_SystemHealth_Panel_HealthIndicator.BackColor = Color.FromArgb(241, 196, 15); // Yellow
                    Form_SystemHealth_Label_HealthIcon.Text = "⚠️";
                    break;
                case Enum_HealthIndicator.Red:
                    Form_SystemHealth_Panel_HealthIndicator.BackColor = Color.FromArgb(231, 76, 60); // Red
                    Form_SystemHealth_Label_HealthIcon.Text = "❌";
                    break;
            }
        }

        private async Task LoadUserFeedbackAsync()
        {
            try
            {
                var result = await _devToolsService.GetUserFeedbackAsync(_currentUserId);
                if (result.IsSuccess && result.Data != null)
                {
                    Form_SystemHealth_DataGridView_Feedback.DataSource = result.Data;
                    
                    if (result.Data.Rows.Count == 0)
                    {
                        Form_SystemHealth_Label_NoFeedback.Visible = true;
                        Form_SystemHealth_DataGridView_Feedback.Visible = false;
                    }
                    else
                    {
                        Form_SystemHealth_Label_NoFeedback.Visible = false;
                        Form_SystemHealth_DataGridView_Feedback.Visible = true;
                    }
                }
                else
                {
                    _errorHandler.ShowUserError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    callerName: nameof(LoadUserFeedbackAsync),
                    controlName: Name);
            }
        }

        private void Form_SystemHealth_Button_SubmitFeedback_Click(object sender, EventArgs e)
        {
            // Open feedback form (assuming it exists and is accessible)
            // For now, we might need to check how to open it.
            // Usually via MainForm or a specific service.
            // If not available, show message.
            _errorHandler.ShowInformation("Feedback submission form is not yet linked.");
        }

        private void Form_SystemHealth_Button_ContactSupport_Click(object sender, EventArgs e)
        {
            try
            {
                var subject = $"Support Request - {Model_Application_Variables.User}";
                var body = $"Machine: {Environment.MachineName}%0D%0AVersion: {Application.ProductVersion}";
                var mailto = $"mailto:support@mtm.com?subject={subject}&body={body}";
                
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = mailto,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    callerName: nameof(Form_SystemHealth_Button_ContactSupport_Click),
                    controlName: Name);
            }
        }

        #endregion
    }
}
