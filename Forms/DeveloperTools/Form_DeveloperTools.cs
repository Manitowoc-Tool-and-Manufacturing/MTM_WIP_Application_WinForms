using System.Data;
using System.Text;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Data;

namespace MTM_WIP_Application_Winforms.Forms.DeveloperTools
{
    /// <summary>
    /// Form for developers to manage user feedback and system mappings.
    /// </summary>
    public partial class Form_DeveloperTools : ThemedForm
    {
        private readonly IService_FeedbackManager _feedbackManager;
        private int _currentUserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form_DeveloperTools"/> class.
        /// </summary>
        public Form_DeveloperTools()
        {
            InitializeComponent();
            _feedbackManager = new Service_FeedbackManager();
        }

        private async void Form_DeveloperTools_Load(object sender, EventArgs e)
        {
            if (!Model_Application_Variables.UserTypeDeveloper && !Model_Application_Variables.UserTypeAdmin)
            {
                Service_ErrorHandler.ShowUserError("Access denied. You do not have permission to view this page.");
                this.Close();
                return;
            }

            var userResult = await Dao_System.GetUserIdByNameAsync(Model_Application_Variables.User);
            if (userResult.IsSuccess) _currentUserId = userResult.Data;

            LoadCombos();
            await LoadDataAsync();
        }

        private void LoadCombos()
        {
            cboStatus.Items.Add("All");
            cboStatus.Items.Add("New");
            cboStatus.Items.Add("In Review");
            cboStatus.Items.Add("In Progress");
            cboStatus.Items.Add("Resolved");
            cboStatus.Items.Add("Closed");
            cboStatus.Items.Add("Won't Fix");
            cboStatus.SelectedIndex = 0;

            cboType.Items.Add("All");
            cboType.Items.Add("Bug Report");
            cboType.Items.Add("Suggestion");
            cboType.Items.Add("Inconsistency");
            cboType.Items.Add("Question");
            cboType.Items.Add("General");
            cboType.SelectedIndex = 0;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var filters = new Dictionary<string, object>();
                if (cboStatus.SelectedIndex > 0 && cboStatus.SelectedItem != null) filters.Add("FilterStatus", cboStatus.SelectedItem.ToString()!);
                if (cboType.SelectedIndex > 0 && cboType.SelectedItem != null) filters.Add("FilterFeedbackType", cboType.SelectedItem.ToString()!);
                
                var result = await _feedbackManager.GetAllAsync(filters);
                if (result.IsSuccess)
                {
                    dgvFeedback.DataSource = result.Data;
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Failed to load data: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(LoadDataAsync));
            }
        }

        private async Task<bool> CheckRoleStillValid()
        {
            // Refresh roles from DB
            await Dao_System.System_UserAccessTypeAsync();
            
            if (!Model_Application_Variables.UserTypeDeveloper && !Model_Application_Variables.UserTypeAdmin)
            {
                LoggingUtility.Log(Enum_LogLevel.Warning, "Security", $"Unauthorized access attempt to Developer Tools by {Model_Application_Variables.User}", _currentUserId.ToString());
                Service_ErrorHandler.ShowUserError("Your permissions have changed. You no longer have access to this page.");
                this.Close();
                return false;
            }
            return true;
        }

        private async void btnApplyFilter_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (!await CheckRoleStillValid()) return;
            try
            {
                var filters = new Dictionary<string, object>();
                if (cboStatus.SelectedIndex > 0 && cboStatus.SelectedItem != null) filters.Add("FilterStatus", cboStatus.SelectedItem.ToString()!);
                if (cboType.SelectedIndex > 0 && cboType.SelectedItem != null) filters.Add("FilterFeedbackType", cboType.SelectedItem.ToString()!);

                var result = await _feedbackManager.ExportToCsvAsync(filters);
                if (result.IsSuccess && result.Data != null)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "CSV Files (*.csv)|*.csv";
                        sfd.FileName = $"Feedback_Export_{DateTime.Now:yyyyMMddHHmmss}.csv";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            await Task.Run(() =>
                            {
                                var sb = new StringBuilder();
                                var dt = result.Data;
                                
                                // Headers
                                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                                sb.AppendLine(string.Join(",", columnNames));

                                // Rows
                                foreach (DataRow row in dt.Rows)
                                {
                                    // Handle nulls and quotes
                                    var fields = row.ItemArray.Select(field => {
                                        string s = field?.ToString() ?? "";
                                        if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
                                        {
                                            return $"\"{s.Replace("\"", "\"\"")}\"";
                                        }
                                        return s;
                                    });
                                    sb.AppendLine(string.Join(",", fields));
                                }

                                File.WriteAllText(sfd.FileName, sb.ToString());
                            });
                            
                            Service_ErrorHandler.ShowInformation($"Exported to {sfd.FileName}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(btnExport_Click));
            }
        }

        private async void tsmiUpdateStatus_Click(object sender, EventArgs e)
        {
            if (!await CheckRoleStillValid()) return;
            if (dgvFeedback.SelectedRows.Count == 0) return;
            var row = dgvFeedback.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["FeedbackID"].Value);
            
            string newStatus = Form_InputBox.Show("Enter new status (New, In Review, In Progress, Resolved, Closed, Won't Fix):", "Update Status", row.Cells["Status"].Value?.ToString() ?? "");
            if (!string.IsNullOrWhiteSpace(newStatus))
            {
                var result = await _feedbackManager.UpdateStatusAsync(id, newStatus, null, null, _currentUserId);
                if (result.IsSuccess) await LoadDataAsync();
                else Service_ErrorHandler.ShowUserError(result.ErrorMessage);
            }
        }

        private async void tsmiAddNotes_Click(object sender, EventArgs e)
        {
            if (!await CheckRoleStillValid()) return;
            if (dgvFeedback.SelectedRows.Count == 0) return;
            var row = dgvFeedback.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["FeedbackID"].Value);
            
            string note = Form_InputBox.Show("Enter note:", "Add Note");
            if (!string.IsNullOrWhiteSpace(note))
            {
                string currentStatus = row.Cells["Status"].Value?.ToString() ?? "";
                var result = await _feedbackManager.UpdateStatusAsync(id, currentStatus, null, note, _currentUserId);
                if (result.IsSuccess) await LoadDataAsync();
                else Service_ErrorHandler.ShowUserError(result.ErrorMessage);
            }
        }

        private async void tsmiAssignDeveloper_Click(object sender, EventArgs e)
        {
            if (!await CheckRoleStillValid()) return;
            if (dgvFeedback.SelectedRows.Count == 0) return;
            var row = dgvFeedback.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["FeedbackID"].Value);
            
            string devIdStr = Form_InputBox.Show("Enter Developer User ID:", "Assign Developer");
            if (int.TryParse(devIdStr, out int devId))
            {
                string currentStatus = row.Cells["Status"].Value?.ToString() ?? "";
                var result = await _feedbackManager.UpdateStatusAsync(id, currentStatus, devId, null, _currentUserId);
                if (result.IsSuccess) await LoadDataAsync();
                else Service_ErrorHandler.ShowUserError(result.ErrorMessage);
            }
        }

        private async void tsmiMarkDuplicate_Click(object sender, EventArgs e)
        {
            if (!await CheckRoleStillValid()) return;
            if (dgvFeedback.SelectedRows.Count == 0) return;
            var row = dgvFeedback.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["FeedbackID"].Value);
            
            string dupIdStr = Form_InputBox.Show("Enter Original Feedback ID:", "Mark Duplicate");
            if (int.TryParse(dupIdStr, out int dupId))
            {
                var result = await _feedbackManager.MarkDuplicateAsync(id, dupId, _currentUserId);
                if (result.IsSuccess) await LoadDataAsync();
                else Service_ErrorHandler.ShowUserError(result.ErrorMessage);
            }
        }

        private void tsmiViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvFeedback.SelectedRows.Count == 0) return;
            var row = dgvFeedback.SelectedRows[0];
            string desc = row.Cells["Description"].Value?.ToString() ?? "No description";
            Service_ErrorHandler.ShowInformation(desc, "Feedback Details");
        }
    }
}
