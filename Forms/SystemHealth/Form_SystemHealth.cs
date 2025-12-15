using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;
using MTM_WIP_Application_Winforms.Models.Entities;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

namespace MTM_WIP_Application_Winforms.Forms.SystemHealth
{
    public partial class Form_SystemHealth : ThemedForm
    {
        #region Fields

        private readonly IService_DeveloperTools _devToolsService;
        private readonly IService_FeedbackManager _feedbackManager;
        private readonly IService_ErrorHandler _errorHandler;
        private readonly string _currentUserId;
        private readonly System.Windows.Forms.Timer _refreshTimer;
        private List<Model_UserFeedback> _feedbackList = new List<Model_UserFeedback>();
        private int _currentFeedbackIndex = 0;

        #endregion

        #region Constructors

        public Form_SystemHealth(
            IService_DeveloperTools devToolsService,
            IService_FeedbackManager feedbackManager,
            IService_ErrorHandler errorHandler)
        {
            _devToolsService = devToolsService ?? throw new ArgumentNullException(nameof(devToolsService));
            _feedbackManager = feedbackManager ?? throw new ArgumentNullException(nameof(feedbackManager));
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
            // Ensure textboxes are visible
            foreach (Control c in Form_SystemHealth_TableLayout_FeedbackDetails.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.BorderStyle = BorderStyle.FixedSingle;
                }
            }

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
                    Form_SystemHealth_Label_HealthIcon.BackColor = Color.FromArgb(46, 204, 113); // Green
                    Form_SystemHealth_Label_HealthMessage.BackColor = Color.FromArgb(46, 204, 113); // Green
                    Form_SystemHealth_Label_HealthIcon.Text = "✅";
                    break;
                case Enum_HealthIndicator.Yellow:
                    Form_SystemHealth_Panel_HealthIndicator.BackColor = Color.FromArgb(241, 196, 15); // Yellow
                    Form_SystemHealth_Label_HealthIcon.BackColor = Color.FromArgb(241, 196, 15); // Yellow
                    Form_SystemHealth_Label_HealthMessage.BackColor = Color.FromArgb(241, 196, 15); // Yellow
                    Form_SystemHealth_Label_HealthIcon.Text = "⚠️";
                    break;
                case Enum_HealthIndicator.Red:
                    Form_SystemHealth_Panel_HealthIndicator.BackColor = Color.FromArgb(231, 76, 60); // Red
                    Form_SystemHealth_Label_HealthIcon.BackColor = Color.FromArgb(231, 76, 60); // Red
                    Form_SystemHealth_Label_HealthMessage.BackColor = Color.FromArgb(231, 76, 60); // Red
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
                    _feedbackList = new List<Model_UserFeedback>();
                    foreach (System.Data.DataRow row in result.Data.Rows)
                    {
                        _feedbackList.Add(new Model_UserFeedback
                        {
                            FeedbackID = Convert.ToInt32(row["FeedbackID"]),
                            FeedbackType = row["FeedbackType"].ToString() ?? "",
                            SubmissionDateTime = Convert.ToDateTime(row["SubmissionDateTime"]),
                            ActiveSection = row["ActiveSection"].ToString(),
                            Category = row["Category"].ToString(),
                            Severity = row["Severity"].ToString(),
                            Priority = row["Priority"].ToString(),
                            Title = row["Title"].ToString(),
                            Description = row["Description"].ToString(),
                            Status = row["Status"].ToString() ?? "New",
                            UserFullName = row["UserFullName"].ToString()
                        });
                    }

                    if (_feedbackList.Count > 0)
                    {
                        _currentFeedbackIndex = 0;
                        ShowFeedback(_currentFeedbackIndex);
                    }
                    else
                    {
                        ClearFeedbackFields();
                        Form_SystemHealth_Label_UserFullName.Text = "No feedback found.";
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

        private void ShowFeedback(int index)
        {
            if (index < 0 || index >= _feedbackList.Count) return;

            var feedback = _feedbackList[index];
            
            Form_SystemHealth_Label_UserFullName.Text = $"User: {feedback.UserFullName}";
            Form_SystemHealth_TextBox_FeedbackType.Text = feedback.FeedbackType;
            Form_SystemHealth_TextBox_SubmissionDate.Text = feedback.SubmissionDateTime.ToShortDateString();
            Form_SystemHealth_TextBox_ActiveSection.Text = feedback.ActiveSection;
            Form_SystemHealth_TextBox_Category.Text = feedback.Category;
            Form_SystemHealth_TextBox_Severity.Text = feedback.Severity;
            Form_SystemHealth_TextBox_Priority.Text = feedback.Priority;
            Form_SystemHealth_TextBox_Title.Text = feedback.Title;
            Form_SystemHealth_TextBox_Description.Text = feedback.Description;
            Form_SystemHealth_TextBox_Status.Text = feedback.Status;

            // Apply colors based on Status
            Color statusColor = Color.White;
            if (string.Equals(feedback.Status, "New", StringComparison.OrdinalIgnoreCase))
            {
                statusColor = Color.FromArgb(255, 235, 238); // Light Red
            }
            else if (string.Equals(feedback.Status, "Open", StringComparison.OrdinalIgnoreCase))
            {
                statusColor = Color.FromArgb(255, 243, 224); // Light Orange
            }
            else if (string.Equals(feedback.Status, "In Progress", StringComparison.OrdinalIgnoreCase))
            {
                statusColor = Color.FromArgb(255, 248, 225); // Light Yellow
            }
            else if (string.Equals(feedback.Status, "Resolved", StringComparison.OrdinalIgnoreCase))
            {
                statusColor = Color.FromArgb(232, 245, 233); // Light Green
            }
            else if (string.Equals(feedback.Status, "Closed", StringComparison.OrdinalIgnoreCase))
            {
                statusColor = Color.FromArgb(200, 230, 201); // Green
            }

            // Apply to Form Background and Controls
            this.BackColor = statusColor;
            Form_SystemHealth_GroupBox_Feedback.BackColor = statusColor;
            Form_SystemHealth_TableLayout_FeedbackDetails.BackColor = statusColor;
            Form_SystemHealth_Panel_Actions.BackColor = statusColor;
            Form_SystemHealth_Label_UserFullName.BackColor = statusColor;
            Form_SystemHealth_TextBox_FeedbackType.BackColor = statusColor;
            Form_SystemHealth_TextBox_SubmissionDate.BackColor = statusColor;
            Form_SystemHealth_TextBox_ActiveSection.BackColor = statusColor;
            Form_SystemHealth_TextBox_Category.BackColor = statusColor;
            Form_SystemHealth_TextBox_Severity.BackColor = statusColor;
            Form_SystemHealth_TextBox_Priority.BackColor = statusColor;
            Form_SystemHealth_TextBox_Title.BackColor = statusColor;
            Form_SystemHealth_TextBox_Description.BackColor = statusColor;
            Form_SystemHealth_TextBox_Status.BackColor = statusColor;

            // Update labels inside TableLayoutPanel
            foreach (Control c in Form_SystemHealth_TableLayout_FeedbackDetails.Controls)
            {
                if (c is Label)
                {
                    c.BackColor = statusColor;
                }
            }

            // Update buttons state
            Form_SystemHealth_Button_Previous.Enabled = index > 0;
            Form_SystemHealth_Button_Next.Enabled = index < _feedbackList.Count - 1;
        }

        private void ClearFeedbackFields()
        {
            Form_SystemHealth_TextBox_FeedbackType.Clear();
            Form_SystemHealth_TextBox_SubmissionDate.Clear();
            Form_SystemHealth_TextBox_ActiveSection.Clear();
            Form_SystemHealth_TextBox_Category.Clear();
            Form_SystemHealth_TextBox_Severity.Clear();
            Form_SystemHealth_TextBox_Priority.Clear();
            Form_SystemHealth_TextBox_Title.Clear();
            Form_SystemHealth_TextBox_Description.Clear();
            Form_SystemHealth_TextBox_Status.Clear();
            Form_SystemHealth_Button_Previous.Enabled = false;
            Form_SystemHealth_Button_Next.Enabled = false;
        }

        private void Form_SystemHealth_Button_Previous_Click(object sender, EventArgs e)
        {
            if (_currentFeedbackIndex > 0)
            {
                _currentFeedbackIndex--;
                ShowFeedback(_currentFeedbackIndex);
            }
        }

        private void Form_SystemHealth_Button_Next_Click(object sender, EventArgs e)
        {
            if (_currentFeedbackIndex < _feedbackList.Count - 1)
            {
                _currentFeedbackIndex++;
                ShowFeedback(_currentFeedbackIndex);
            }
        }

        private void Form_SystemHealth_Button_SubmitFeedback_Click(object sender, EventArgs e)
        {
            using var form = new Form_SubmitFeedback(_feedbackManager, _errorHandler);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _ = LoadUserFeedbackAsync();
            }
        }

        #endregion
    }
}
