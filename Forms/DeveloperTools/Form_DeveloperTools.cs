using System.Data;
using System.Text;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models.Entities;

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

            // Auto-filter when selection changes
            cboStatus.SelectedIndexChanged += async (s, e) => await LoadDataAsync();
            cboType.SelectedIndexChanged += async (s, e) => await LoadDataAsync();
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
                    dgvList.DataSource = result.Data;
                    ConfigureGrid();
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

        private void ConfigureGrid()
        {
            if (dgvList.Columns.Count == 0) return;

            // Hide detail columns
            string[] hideCols = { "Description", "StepsToReproduce", "ExpectedBehavior", "ActualBehavior", 
                                  "BusinessJustification", "AffectedUsers", "Location1", "Location2", "ExpectedConsistency" };
            
            foreach (var col in hideCols)
            {
                if (dgvList.Columns.Contains(col)) dgvList.Columns[col].Visible = false;
            }

            // Auto-size remaining
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count > 0)
            {
                var row = dgvList.SelectedRows[0];
                PopulateDetails(row);
            }
            else
            {
                ClearDetails();
            }
        }

        private void PopulateDetails(DataGridViewRow row)
        {
            // Basic Fields
            txtId.Text = GetVal(row, "FeedbackID");
            txtDate.Text = GetVal(row, "SubmissionDateTime");
            txtUser.Text = GetVal(row, "User");
            txtType.Text = GetVal(row, "FeedbackType");
            txtStatus.Text = GetVal(row, "Status");
            txtCategory.Text = GetVal(row, "Category");
            txtTitle.Text = GetVal(row, "Title");
            txtDescription.Text = GetVal(row, "Description");

            string type = txtType.Text;

            // Bug Report Fields
            bool isBug = type == "Bug Report";
            SetFieldVisibility(lblSteps, txtSteps, isBug, GetVal(row, "StepsToReproduce"));
            SetFieldVisibility(lblExpected, txtExpected, isBug, GetVal(row, "ExpectedBehavior"));
            SetFieldVisibility(lblActual, txtActual, isBug, GetVal(row, "ActualBehavior"));

            // Suggestion Fields
            bool isSuggestion = type == "Suggestion";
            SetFieldVisibility(lblJustification, txtJustification, isSuggestion, GetVal(row, "BusinessJustification"));
            SetFieldVisibility(lblAffected, txtAffected, isSuggestion, GetVal(row, "AffectedUsers"));

            // Inconsistency Fields
            bool isInconsistency = type == "Inconsistency";
            SetFieldVisibility(lblLoc1, txtLoc1, isInconsistency, GetVal(row, "Location1"));
            SetFieldVisibility(lblLoc2, txtLoc2, isInconsistency, GetVal(row, "Location2"));
            SetFieldVisibility(lblConsistency, txtConsistency, isInconsistency, GetVal(row, "ExpectedConsistency"));
        }

        private void SetFieldVisibility(Label lbl, TextBox txt, bool isTypeMatch, string value)
        {
            // Show if it matches the type OR if there is data in it (legacy/mixed data)
            bool visible = isTypeMatch || !string.IsNullOrWhiteSpace(value);
            lbl.Visible = visible;
            txt.Visible = visible;
            if (visible) txt.Text = value;
        }

        private string GetVal(DataGridViewRow row, string colName)
        {
            if (dgvList.Columns.Contains(colName) && row.Cells[colName].Value != null && row.Cells[colName].Value != DBNull.Value)
            {
                return row.Cells[colName].Value.ToString() ?? "";
            }
            return "";
        }

        private void ClearDetails()
        {
            foreach (Control c in tlpDetails.Controls)
            {
                if (c is TextBox txt) txt.Clear();
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
            if (dgvList.SelectedRows.Count == 0) return;
            var row = dgvList.SelectedRows[0];
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
            if (dgvList.SelectedRows.Count == 0) return;
            var row = dgvList.SelectedRows[0];
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
            if (dgvList.SelectedRows.Count == 0) return;
            var row = dgvList.SelectedRows[0];
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
            if (dgvList.SelectedRows.Count == 0) return;
            var row = dgvList.SelectedRows[0];
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
            // Details are now always visible in the right panel
            if (dgvList.SelectedRows.Count == 0) return;
            // Optional: Focus the details panel or scroll to top
        }

        private async void btnRepairData_Click(object sender, EventArgs e)
        {
            if (!await CheckRoleStillValid()) return;
            
            if (MessageBox.Show("This will attempt to parse and migrate legacy description data into specific columns. Continue?", "Repair Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                var result = await _feedbackManager.GetAllAsync(new Dictionary<string, object>());
                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.ShowUserError("Failed to load data for repair.");
                    return;
                }

                int count = 0;
                foreach (DataRow row in result.Data.Rows)
                {
                    string desc = row["Description"]?.ToString() ?? "";
                    if (string.IsNullOrWhiteSpace(desc)) continue;

                    // Skip if already migrated (check if specific columns are populated)
                    // But since we are reading from DataTable, we might need to check if columns exist or are DBNull
                    // For simplicity, we check if description contains the markers
                    if (!desc.Contains("Steps to Reproduce:") && !desc.Contains("Justification:") && !desc.Contains("Location 1:"))
                        continue;

                    var model = new Model_UserFeedback
                    {
                        FeedbackID = Convert.ToInt32(row["FeedbackID"]),
                        Description = desc,
                        UserID = Convert.ToInt32(row["UserID"]),
                        TrackingNumber = row["TrackingNumber"]?.ToString() ?? ""
                    };

                    bool updated = false;

                    // Parse Bug Report
                    if (desc.Contains("Steps to Reproduce:"))
                    {
                        model.Description = ExtractSection(desc, "Description:", "Steps to Reproduce:");
                        model.StepsToReproduce = ExtractSection(desc, "Steps to Reproduce:", "Expected:");
                        model.ExpectedBehavior = ExtractSection(desc, "Expected:", "Actual:");
                        model.ActualBehavior = ExtractSection(desc, "Actual:", null);
                        updated = true;
                    }
                    // Parse Suggestion
                    else if (desc.Contains("Justification:"))
                    {
                        model.Description = ExtractSection(desc, "Description:", "Justification:");
                        model.BusinessJustification = ExtractSection(desc, "Justification:", "Affected Users:");
                        model.AffectedUsers = ExtractSection(desc, "Affected Users:", null);
                        updated = true;
                    }
                    // Parse Inconsistency
                    else if (desc.Contains("Location 1:"))
                    {
                        model.Description = ExtractSection(desc, "Description:", "Location 1:");
                        model.Location1 = ExtractSection(desc, "Location 1:", "Location 2:");
                        model.Location2 = ExtractSection(desc, "Location 2:", "Expected:");
                        model.ExpectedConsistency = ExtractSection(desc, "Expected:", null);
                        updated = true;
                    }

                    if (updated)
                    {
                        var updateResult = await _feedbackManager.UpdateDetailsAsync(model);
                        if (updateResult.IsSuccess) count++;
                    }
                }

                Service_ErrorHandler.ShowInformation($"Successfully repaired {count} records.");
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(btnRepairData_Click));
            }
        }

        private string ExtractSection(string text, string startMarker, string? endMarker)
        {
            int startIndex = text.IndexOf(startMarker);
            if (startIndex == -1) return "";
            
            startIndex += startMarker.Length;
            
            int endIndex = endMarker != null ? text.IndexOf(endMarker, startIndex) : text.Length;
            if (endIndex == -1) endIndex = text.Length;

            return text.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}
