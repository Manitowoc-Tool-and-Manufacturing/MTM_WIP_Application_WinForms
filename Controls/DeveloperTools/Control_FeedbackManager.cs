using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    public partial class Control_FeedbackManager : ThemedUserControl
    {
        private IService_FeedbackManager? _feedbackManager;
        private IService_ErrorHandler? _errorHandler;

        public Control_FeedbackManager()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void Initialize(IService_FeedbackManager feedbackManager, IService_ErrorHandler errorHandler)
        {
            _feedbackManager = feedbackManager;
            _errorHandler = errorHandler;
            LoadDataAsync();
        }

        private void InitializeControls()
        {
            // Status Filter
            Control_FeedbackManager_ComboBox_Status.Items.Add("All");
            Control_FeedbackManager_ComboBox_Status.Items.Add("New");
            Control_FeedbackManager_ComboBox_Status.Items.Add("Open");
            Control_FeedbackManager_ComboBox_Status.Items.Add("In Progress");
            Control_FeedbackManager_ComboBox_Status.Items.Add("Resolved");
            Control_FeedbackManager_ComboBox_Status.Items.Add("Closed");
            Control_FeedbackManager_ComboBox_Status.SelectedIndex = 0;

            // Grid
            Control_FeedbackManager_DataGridView_Feedback.AutoGenerateColumns = false;
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FeedbackID", HeaderText = "ID", Width = 50 });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SubmissionDateTime", HeaderText = "Date", Width = 120, DefaultCellStyle = { Format = "MM/dd HH:mm" } });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UserName", HeaderText = "User", Width = 100 });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FeedbackType", HeaderText = "Type", Width = 80 });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 80 });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Subject", Width = 200 });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            // Events
            Control_FeedbackManager_Button_Refresh.Click += (s, e) => LoadDataAsync();
            Control_FeedbackManager_ComboBox_Status.SelectedIndexChanged += (s, e) => LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            if (_feedbackManager == null || _errorHandler == null) return;

            try
            {
                var filters = new Dictionary<string, object>();
                if (Control_FeedbackManager_ComboBox_Status.SelectedIndex > 0 && Control_FeedbackManager_ComboBox_Status.SelectedItem != null)
                {
                    filters["FilterStatus"] = Control_FeedbackManager_ComboBox_Status.SelectedItem.ToString() ?? "";
                }

                var result = await _feedbackManager.GetAllAsync(filters);
                if (result.IsSuccess && result.Data != null)
                {
                    Control_FeedbackManager_DataGridView_Feedback.DataSource = result.Data;
                    Control_FeedbackManager_Label_Count.Text = $"{result.Data.Rows.Count} items found";
                }
                else
                {
                    _errorHandler.ShowUserError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, 
                    callerName: nameof(LoadDataAsync), 
                    controlName: this.Name);
            }
        }
    }
}

