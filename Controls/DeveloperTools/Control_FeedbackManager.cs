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
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Models;

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

            // Type Filter
            Control_FeedbackManager_ComboBox_Type.Items.Add("All");
            Control_FeedbackManager_ComboBox_Type.Items.Add("Bug");
            Control_FeedbackManager_ComboBox_Type.Items.Add("Feature");
            Control_FeedbackManager_ComboBox_Type.Items.Add("Question");
            Control_FeedbackManager_ComboBox_Type.SelectedIndex = 0;

            // Grid
            Control_FeedbackManager_DataGridView_Feedback.AutoGenerateColumns = false;
            Control_FeedbackManager_DataGridView_Feedback.ReadOnly = false;
            Control_FeedbackManager_DataGridView_Feedback.EditMode = DataGridViewEditMode.EditOnEnter;
            
            // Add Checkbox Column
            var checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "",
                Width = 30,
                Name = "colCheck",
                ReadOnly = false
            };
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(checkColumn);

            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FeedbackID", HeaderText = "ID", Width = 50, ReadOnly = true });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SubmissionDateTime", HeaderText = "Date", Width = 120, DefaultCellStyle = { Format = "MM/dd HH:mm" }, ReadOnly = true });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UserName", HeaderText = "User", Width = 100, ReadOnly = true });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FeedbackType", HeaderText = "Type", Width = 80, ReadOnly = true });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 80, ReadOnly = true });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Subject", Width = 200, ReadOnly = true });
            Control_FeedbackManager_DataGridView_Feedback.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            // Events
            Control_FeedbackManager_Button_Refresh.Click += (s, e) => LoadDataAsync();
            Control_FeedbackManager_ComboBox_Status.SelectedIndexChanged += (s, e) => LoadDataAsync();
            Control_FeedbackManager_ComboBox_Type.SelectedIndexChanged += (s, e) => LoadDataAsync();
            Control_FeedbackManager_DateTimePicker_Start.ValueChanged += (s, e) => LoadDataAsync();
            Control_FeedbackManager_DateTimePicker_End.ValueChanged += (s, e) => LoadDataAsync();
            
            Control_FeedbackManager_Button_MarkNew.Click += async (s, e) => await BulkUpdateStatusAsync("New");
            Control_FeedbackManager_Button_MarkOpen.Click += async (s, e) => await BulkUpdateStatusAsync("Open");
            Control_FeedbackManager_Button_MarkInProgress.Click += async (s, e) => await BulkUpdateStatusAsync("In Progress");
            Control_FeedbackManager_Button_MarkResolved.Click += async (s, e) => await BulkUpdateStatusAsync("Resolved");
            Control_FeedbackManager_Button_MarkClosed.Click += async (s, e) => await BulkUpdateStatusAsync("Closed");
            
            Control_FeedbackManager_DataGridView_Feedback.RowPrePaint += Control_FeedbackManager_DataGridView_Feedback_RowPrePaint;
        }

        public async Task LoadDataAsync()
        {
            if (_feedbackManager == null || _errorHandler == null) return;

            try
            {
                var filters = new Dictionary<string, object>();
                
                // Status Filter
                if (Control_FeedbackManager_ComboBox_Status.SelectedIndex > 0 && Control_FeedbackManager_ComboBox_Status.SelectedItem != null)
                {
                    filters["FilterStatus"] = Control_FeedbackManager_ComboBox_Status.SelectedItem.ToString() ?? "";
                }

                // Type Filter
                if (Control_FeedbackManager_ComboBox_Type.SelectedIndex > 0 && Control_FeedbackManager_ComboBox_Type.SelectedItem != null)
                {
                    filters["FilterFeedbackType"] = Control_FeedbackManager_ComboBox_Type.SelectedItem.ToString() ?? "";
                }

                // Date Filters
                if (Control_FeedbackManager_DateTimePicker_Start.Checked)
                {
                    filters["FilterDateFrom"] = Control_FeedbackManager_DateTimePicker_Start.Value.Date;
                }
                
                if (Control_FeedbackManager_DateTimePicker_End.Checked)
                {
                    filters["FilterDateTo"] = Control_FeedbackManager_DateTimePicker_End.Value.Date.AddDays(1).AddTicks(-1);
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

        private void Control_FeedbackManager_DataGridView_Feedback_RowPrePaint(object? sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0) return;
            
            var row = Control_FeedbackManager_DataGridView_Feedback.Rows[e.RowIndex];
            
            // Check if we have a Priority column in the data source
            string? priority = null;
            if (row.DataBoundItem is DataRowView drv)
            {
                // Check if column exists
                if (drv.DataView != null && drv.DataView.Table != null && drv.DataView.Table.Columns.Contains("Priority"))
                {
                    priority = drv["Priority"]?.ToString();
                }
            }

            if (string.IsNullOrEmpty(priority)) return;

            if (priority.Equals("High", StringComparison.OrdinalIgnoreCase))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238); // Light Red
            }
            else if (priority.Equals("Medium", StringComparison.OrdinalIgnoreCase))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225); // Light Yellow
            }
        }

        private async Task BulkUpdateStatusAsync(string newStatus)
        {
            if (_feedbackManager == null || _errorHandler == null) return;

            try
            {
                var selectedIds = new List<int>();
                foreach (DataGridViewRow row in Control_FeedbackManager_DataGridView_Feedback.Rows)
                {
                    var cell = row.Cells["colCheck"] as DataGridViewCheckBoxCell;
                    if (cell != null && Convert.ToBoolean(cell.Value) == true)
                    {
                        if (row.DataBoundItem is DataRowView drv)
                        {
                            if (int.TryParse(drv["FeedbackID"].ToString(), out int id))
                            {
                                selectedIds.Add(id);
                            }
                        }
                    }
                }

                if (selectedIds.Count == 0)
                {
                    _errorHandler.ShowUserError("Please select at least one item.");
                    return;
                }

                int successCount = 0;
                foreach (var id in selectedIds)
                {
                    // UpdateStatusAsync(int feedbackId, string newStatus, int? assignedDeveloperId, string? notes, int modifiedByUserId)
                    var result = await _feedbackManager.UpdateStatusAsync(id, newStatus, null, "Bulk update via Developer Tools", Model_Application_Variables.User);
                    if (result.IsSuccess) successCount++;
                }
                
                if (successCount > 0)
                {
                    LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium,
                    callerName: nameof(BulkUpdateStatusAsync),
                    controlName: this.Name);
            }
        }
    }
}

