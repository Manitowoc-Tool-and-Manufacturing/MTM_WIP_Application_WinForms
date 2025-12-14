using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Entities;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

namespace MTM_WIP_Application_Winforms.Forms.SystemHealth
{
    public partial class Form_SubmitFeedback : ThemedForm
    {
        private readonly IService_FeedbackManager _feedbackManager;
        private readonly IService_ErrorHandler _errorHandler;

        public Form_SubmitFeedback(IService_FeedbackManager feedbackManager, IService_ErrorHandler errorHandler)
        {
            _feedbackManager = feedbackManager ?? throw new ArgumentNullException(nameof(feedbackManager));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
            InitializeComponent();
            InitializeDropdowns();
        }

        private void InitializeDropdowns()
        {
            Form_SubmitFeedback_ComboBox_Type.Items.AddRange(new object[] { "Bug", "Suggestion", "Question" });
            Form_SubmitFeedback_ComboBox_Type.SelectedIndex = 0;

            Form_SubmitFeedback_ComboBox_Category.Items.AddRange(new object[] { "UI/UX", "Performance", "Data Accuracy", "Feature Request", "Other" });
            Form_SubmitFeedback_ComboBox_Category.SelectedIndex = 0;

            Form_SubmitFeedback_ComboBox_Priority.Items.AddRange(new object[] { "Low", "Medium", "High", "Critical" });
            Form_SubmitFeedback_ComboBox_Priority.SelectedIndex = 1;

            Form_SubmitFeedback_ComboBox_Severity.Items.AddRange(new object[] { "Low", "Medium", "High", "Critical" });
            Form_SubmitFeedback_ComboBox_Severity.SelectedIndex = 1;
        }

        private async void Form_SubmitFeedback_Button_Submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Form_SubmitFeedback_TextBox_Title.Text))
            {
                _errorHandler.ShowUserError("Please enter a title.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Form_SubmitFeedback_TextBox_Description.Text))
            {
                _errorHandler.ShowUserError("Please enter a description.");
                return;
            }

            try
            {
                var feedback = new Model_UserFeedback
                {
                    FeedbackType = Form_SubmitFeedback_ComboBox_Type.SelectedItem?.ToString() ?? "Bug",
                    Title = Form_SubmitFeedback_TextBox_Title.Text,
                    Description = Form_SubmitFeedback_TextBox_Description.Text,
                    Category = Form_SubmitFeedback_ComboBox_Category.SelectedItem?.ToString(),
                    Priority = Form_SubmitFeedback_ComboBox_Priority.SelectedItem?.ToString(),
                    Severity = Form_SubmitFeedback_ComboBox_Severity.SelectedItem?.ToString(),
                    UserID = 0, // Should be resolved by service or we need to get it. 
                    // Wait, Model_UserFeedback has UserID. I should probably get it from Model_Application_Variables.User (which is string username).
                    // The service SubmitFeedbackAsync might handle UserID lookup or I need to do it.
                    // Let's assume service handles it or I pass 0 and it uses current user context if available.
                    // Actually, Model_Application_Variables.User is the username.
                    UserName = Model_Application_Variables.User,
                    SubmissionDateTime = DateTime.Now,
                    Status = "New",
                    WindowForm = "MainForm", // Default to MainForm to avoid validation errors with unmapped forms
                    ApplicationVersion = Application.ProductVersion,
                    MachineIdentifier = Environment.MachineName,
                    OSVersion = Environment.OSVersion.ToString()
                };

                var result = await _feedbackManager.SubmitFeedbackAsync(feedback);
                if (result.IsSuccess)
                {
                    _errorHandler.ShowInformation($"Feedback submitted successfully! Tracking Number: {result.Data}");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    _errorHandler.ShowUserError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    callerName: nameof(Form_SubmitFeedback_Button_Submit_Click),
                    controlName: Name);
            }
        }

        private void Form_SubmitFeedback_Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
