using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    public partial class Control_LogViewer : ThemedUserControl
    {
        private IService_DeveloperTools? _devToolsService;
        private IService_ErrorHandler? _errorHandler;
        private System.Threading.Timer? _debounceTimer;

        public Control_LogViewer()
        {
            InitializeComponent();
            InitializeControls();
        }

        public void Initialize(IService_DeveloperTools devToolsService, IService_ErrorHandler errorHandler)
        {
            _devToolsService = devToolsService;
            _errorHandler = errorHandler;
        }

        public void FilterByDateRange(string range)
        {
            if (Control_LogViewer_ComboBox_DateRange.Items.Contains(range))
            {
                Control_LogViewer_ComboBox_DateRange.SelectedItem = range;
                _ = PerformSearchAsync();
            }
        }

        public async Task ShowLogEntryAsync(Model_DevLogEntry entry)
        {
            // Set Date Range to the specific date of the entry
            Control_LogViewer_ComboBox_DateRange.SelectedItem = "Custom";
            Control_LogViewer_DateTimePicker_From.Value = entry.Timestamp.Date;
            Control_LogViewer_DateTimePicker_To.Value = entry.Timestamp.Date;

            // Reset other filters to ensure visibility
            Control_LogViewer_ComboBox_GroupBy.SelectedItem = "None";
            Control_LogViewer_CheckBox_Info.Checked = true;
            Control_LogViewer_CheckBox_Warning.Checked = true;
            Control_LogViewer_CheckBox_Error.Checked = true;
            Control_LogViewer_CheckBox_Critical.Checked = true;
            
            // Set search text to the message
            Control_LogViewer_TextBox_Search.Text = entry.Message;
            Control_LogViewer_CheckBox_Regex.Checked = false;

            // Execute search
            await PerformSearchAsync();

            // Find and select the row
            foreach (DataGridViewRow row in Control_LogViewer_DataGridView_Logs.Rows)
            {
                if (row.DataBoundItem is Model_DevLogEntry rowEntry)
                {
                    // Match by Timestamp (within 1 second) and Message
                    if (Math.Abs((rowEntry.Timestamp - entry.Timestamp).TotalSeconds) < 1 && 
                        rowEntry.Message == entry.Message)
                    {
                        row.Selected = true;
                        Control_LogViewer_DataGridView_Logs.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        public void FocusSearch()
        {
            Control_LogViewer_TextBox_Search.Focus();
        }

        public void ClearFilters()
        {
            Control_LogViewer_TextBox_Search.Clear();
            Control_LogViewer_CheckBox_Regex.Checked = false;
            Control_LogViewer_ComboBox_DateRange.SelectedItem = "Last 7 Days";
            Control_LogViewer_ComboBox_GroupBy.SelectedItem = "None";
            Control_LogViewer_CheckBox_Info.Checked = true;
            Control_LogViewer_CheckBox_Warning.Checked = true;
            Control_LogViewer_CheckBox_Error.Checked = true;
            Control_LogViewer_CheckBox_Critical.Checked = true;
            _ = PerformSearchAsync();
        }

        public async Task RefreshLogsAsync()
        {
            await PerformSearchAsync();
        }

        private void InitializeControls()
        {
            // Date Range
            Control_LogViewer_ComboBox_DateRange.Items.Add("Today");
            Control_LogViewer_ComboBox_DateRange.Items.Add("Last 7 Days");
            Control_LogViewer_ComboBox_DateRange.Items.Add("Last 30 Days");
            Control_LogViewer_ComboBox_DateRange.Items.Add("Custom");
            Control_LogViewer_ComboBox_DateRange.SelectedIndex = 1; // Default to Last 7 Days

            // Grid
            Control_LogViewer_DataGridView_Logs.AutoGenerateColumns = false;
            Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Timestamp", HeaderText = "Time", Width = 140, DefaultCellStyle = { Format = "MM/dd HH:mm:ss" } });
            Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Level", HeaderText = "Level", Width = 80 });
            Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Source", HeaderText = "Source", Width = 150 });
            Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Message", HeaderText = "Message", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "User", HeaderText = "User", Width = 100 });

            // Group By
            Control_LogViewer_ComboBox_GroupBy.Items.Add("None");
            Control_LogViewer_ComboBox_GroupBy.Items.Add("Error Type");
            Control_LogViewer_ComboBox_GroupBy.Items.Add("Source");
            Control_LogViewer_ComboBox_GroupBy.Items.Add("Hour");
            Control_LogViewer_ComboBox_GroupBy.Items.Add("Day");
            Control_LogViewer_ComboBox_GroupBy.SelectedIndex = 0;

            // Events
            Control_LogViewer_TextBox_Search.TextChanged += (s, e) => DebounceSearch();
            Control_LogViewer_CheckBox_Regex.CheckedChanged += (s, e) => _ = PerformSearchAsync();
            Control_LogViewer_ComboBox_DateRange.SelectedIndexChanged += CmbDateRange_SelectedIndexChanged;
            Control_LogViewer_DateTimePicker_From.ValueChanged += (s, e) => _ = PerformSearchAsync();
            Control_LogViewer_DateTimePicker_To.ValueChanged += (s, e) => _ = PerformSearchAsync();
            Control_LogViewer_CheckBox_Info.CheckedChanged += (s, e) => _ = PerformSearchAsync();
            Control_LogViewer_CheckBox_Warning.CheckedChanged += (s, e) => _ = PerformSearchAsync();
            Control_LogViewer_CheckBox_Error.CheckedChanged += (s, e) => _ = PerformSearchAsync();
            Control_LogViewer_CheckBox_Critical.CheckedChanged += (s, e) => _ = PerformSearchAsync();
            Control_LogViewer_ComboBox_GroupBy.SelectedIndexChanged += (s, e) => _ = PerformSearchAsync();
            Control_LogViewer_Button_Export.Click += BtnExport_Click;
            Control_LogViewer_Button_Sync.Click += BtnSync_Click;
            Control_LogViewer_DataGridView_Logs.SelectionChanged += DgvLogs_SelectionChanged;
            Control_LogViewer_Button_Copy.Click += BtnCopy_Click;
        }

        private void CmbDateRange_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool isCustom = Control_LogViewer_ComboBox_DateRange.SelectedItem?.ToString() == "Custom";
            Control_LogViewer_DateTimePicker_From.Visible = isCustom;
            Control_LogViewer_DateTimePicker_To.Visible = isCustom;
            Control_LogViewer_Label_From.Visible = isCustom;
            Control_LogViewer_Label_To.Visible = isCustom;

            if (!isCustom)
            {
                _ = PerformSearchAsync();
            }
        }

        private void DebounceSearch()
        {
            _debounceTimer?.Dispose();
            _debounceTimer = new System.Threading.Timer(_ =>
            {
                if (InvokeRequired) Invoke(new Action(PerformSearchVoid));
                else PerformSearchVoid();
            }, null, 500, System.Threading.Timeout.Infinite);
        }

        private void PerformSearchVoid()
        {
            _ = PerformSearchAsync();
        }

        private async Task PerformSearchAsync()
        {
            if (_devToolsService == null || _errorHandler == null) return;

            try
            {
                var filter = BuildFilter();
                var groupBy = Control_LogViewer_ComboBox_GroupBy.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(groupBy) || groupBy == "None")
                {
                    // Standard Log Search
                    var result = await _devToolsService.GetLogEntriesAsync(filter);
                    if (result.IsSuccess)
                    {
                        Control_LogViewer_DataGridView_Logs.DataSource = result.Data;
                        Control_LogViewer_Label_Count.Text = $"{result.Data?.Count ?? 0} entries found";
                        
                        // Restore columns for log entries
                        Control_LogViewer_DataGridView_Logs.Columns.Clear();
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Timestamp", HeaderText = "Time", Width = 140, DefaultCellStyle = { Format = "MM/dd HH:mm:ss" } });
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Level", HeaderText = "Level", Width = 80 });
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Source", HeaderText = "Source", Width = 150 });
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Message", HeaderText = "Message", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "User", HeaderText = "User", Width = 100 });
                    }
                    else
                    {
                        _errorHandler.ShowUserError(result.ErrorMessage);
                    }
                }
                else
                {
                    // Grouped Search
                    Enum_LogGroupBy groupEnum = Enum_LogGroupBy.None;
                    switch (groupBy)
                    {
                        case "Error Type": groupEnum = Enum_LogGroupBy.ErrorType; break;
                        case "Source": groupEnum = Enum_LogGroupBy.Source; break;
                        case "Hour": groupEnum = Enum_LogGroupBy.Hour; break;
                        case "Day": groupEnum = Enum_LogGroupBy.Day; break;
                    }

                    var result = await _devToolsService.GetErrorGroupingsAsync(groupEnum, filter);
                    if (result.IsSuccess)
                    {
                        Control_LogViewer_DataGridView_Logs.DataSource = result.Data;
                        Control_LogViewer_Label_Count.Text = $"{result.Data?.Count ?? 0} groups found";

                        // Setup columns for grouping
                        Control_LogViewer_DataGridView_Logs.Columns.Clear();
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GroupName", HeaderText = "Group", Width = 200 });
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Count", HeaderText = "Count", Width = 100 });
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LastOccurrence", HeaderText = "Last Seen", Width = 140, DefaultCellStyle = { Format = "MM/dd HH:mm:ss" } });
                        Control_LogViewer_DataGridView_Logs.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ExampleMessage", HeaderText = "Example", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                    }
                    else
                    {
                        _errorHandler.ShowUserError(result.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, 
                    callerName: nameof(PerformSearchAsync), 
                    controlName: this.Name);
            }
        }

        private Model_DevLogFilter BuildFilter()
        {
            var filter = new Model_DevLogFilter
            {
                SearchText = Control_LogViewer_TextBox_Search.Text,
                IsRegex = Control_LogViewer_CheckBox_Regex.Checked,
                Severities = new List<string>()
            };

            if (Control_LogViewer_CheckBox_Info.Checked) filter.Severities.Add("Info");
            if (Control_LogViewer_CheckBox_Warning.Checked) filter.Severities.Add("Warning");
            if (Control_LogViewer_CheckBox_Error.Checked) filter.Severities.Add("Error");
            if (Control_LogViewer_CheckBox_Critical.Checked) filter.Severities.Add("Critical");

            // Date Range
            var now = DateTime.Now;
            switch (Control_LogViewer_ComboBox_DateRange.SelectedItem?.ToString())
            {
                case "Today":
                    filter.DateFrom = now.Date;
                    filter.DateTo = now.Date.AddDays(1).AddTicks(-1);
                    break;
                case "Last 7 Days":
                    filter.DateFrom = now.Date.AddDays(-7);
                    filter.DateTo = now;
                    break;
                case "Last 30 Days":
                    filter.DateFrom = now.Date.AddDays(-30);
                    filter.DateTo = now;
                    break;
                case "Custom":
                    filter.DateFrom = Control_LogViewer_DateTimePicker_From.Value.Date;
                    filter.DateTo = Control_LogViewer_DateTimePicker_To.Value.Date.AddDays(1).AddTicks(-1);
                    break;
            }

            return filter;
        }

        private void DgvLogs_SelectionChanged(object? sender, EventArgs e)
        {
            if (Control_LogViewer_DataGridView_Logs.SelectedRows.Count > 0)
            {
                var entry = Control_LogViewer_DataGridView_Logs.SelectedRows[0].DataBoundItem as Model_DevLogEntry;
                if (entry != null)
                {
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine($"Time: {entry.Timestamp}");
                    sb.AppendLine($"Level: {entry.Level}");
                    sb.AppendLine($"Source: {entry.Source}");
                    sb.AppendLine($"User: {entry.User}");
                    sb.AppendLine();
                    sb.AppendLine("Message:");
                    sb.AppendLine(entry.Message);
                    
                    if (!string.IsNullOrEmpty(entry.Details))
                    {
                        sb.AppendLine();
                        sb.AppendLine("Details:");
                        try
                        {
                            // Try to format as JSON
                            if (entry.Details.Trim().StartsWith("{") || entry.Details.Trim().StartsWith("["))
                            {
                                var obj = System.Text.Json.JsonSerializer.Deserialize<object>(entry.Details);
                                var json = System.Text.Json.JsonSerializer.Serialize(obj, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                                sb.AppendLine(json);
                            }
                            else
                            {
                                sb.AppendLine(entry.Details);
                            }
                        }
                        catch
                        {
                            sb.AppendLine(entry.Details);
                        }
                    }

                    if (!string.IsNullOrEmpty(entry.StackTrace))
                    {
                        sb.AppendLine();
                        sb.AppendLine("Stack Trace:");
                        sb.AppendLine(entry.StackTrace);
                    }

                    Control_LogViewer_TextBox_Details.Text = sb.ToString();
                }
            }
            else
            {
                Control_LogViewer_TextBox_Details.Clear();
            }
        }

        private void BtnCopy_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Control_LogViewer_TextBox_Details.Text))
            {
                Clipboard.SetText(Control_LogViewer_TextBox_Details.Text);
            }
        }

        private async void BtnSync_Click(object? sender, EventArgs e)
        {
            if (_devToolsService == null || _errorHandler == null) return;

            if (_errorHandler.ShowConfirmation("This will truncate the database log table and re-import all logs from CSV files. Continue?", 
                "Sync Logs", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            Control_LogViewer_Button_Sync.Enabled = false;
            Control_LogViewer_Button_Sync.Text = "Syncing...";

            try
            {
                var result = await _devToolsService.SyncLogsToDatabaseAsync();
                if (result.IsSuccess)
                {
                    _errorHandler.ShowInformation("Logs synchronized successfully.", "Sync Logs");
                    await PerformSearchAsync();
                }
                else
                {
                    _errorHandler.ShowUserError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, 
                    callerName: nameof(BtnSync_Click), 
                    controlName: this.Name);
            }
            finally
            {
                Control_LogViewer_Button_Sync.Enabled = true;
                Control_LogViewer_Button_Sync.Text = "Sync DB";
            }
        }

        private async void BtnExport_Click(object? sender, EventArgs e)
        {
            if (_devToolsService == null || _errorHandler == null) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv|JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var filter = BuildFilter();
                        // Re-fetch logs to ensure we export what matches current filter (or use current grid source if preferred)
                        // Using current grid source avoids a DB call but might be incomplete if paging is added later.
                        // For now, let's use the grid source as it reflects what user sees.
                        var entries = Control_LogViewer_DataGridView_Logs.DataSource as List<Model_DevLogEntry>;
                        
                        if (entries == null || entries.Count == 0)
                        {
                            _errorHandler.ShowInformation("No logs to export.", "Export");
                            return;
                        }

                        Enum_ExportFormat format = Enum_ExportFormat.CSV;
                        if (sfd.FileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) format = Enum_ExportFormat.JSON;
                        else if (sfd.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)) format = Enum_ExportFormat.TXT;

                        var result = await _devToolsService.ExportLogsAsync(entries, sfd.FileName, format);
                        
                        if (result.IsSuccess)
                        {
                            _errorHandler.ShowInformation("Logs exported successfully.", "Export");
                        }
                        else
                        {
                            _errorHandler?.ShowUserError(result.ErrorMessage);
                        }
                    }
                    catch (Exception ex)
                    {
                        _errorHandler?.HandleException(ex, Models.Enum_ErrorSeverity.Medium, 
                            callerName: nameof(BtnExport_Click), 
                            controlName: this.Name);
                    }
                }
            }
        }
    }
}

