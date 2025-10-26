using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_Inventory_Application.Core;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Services;
using MTM_WIP_Application_WinForms.Models;

namespace MTM_Inventory_Application.Controls.ErrorReports
{
    public partial class Control_ErrorReportsGrid : UserControl
    {
        #region Fields

    private readonly BindingSource _bindingSource = new();
    private bool _isLoading;
    private bool _hasLoaded;
    private Model_ErrorReportFilter? _lastFilter;
    private const string AllUsersOption = "[ All Users ]";
    private const string AllMachinesOption = "[ All Machines ]";
    private const string AllStatusesOption = "[ All Statuses ]";

        #endregion

        #region Properties

        public bool IsLoading => _isLoading;

        internal Model_ErrorReportFilter? LastFilter => _lastFilter;

        public int? SelectedReportId
        {
            get
            {
                if (dgvErrorReports.SelectedRows.Count == 0)
                {
                    return null;
                }

                object? value = dgvErrorReports.SelectedRows[0].Cells["colReportId"].Value;
                return value switch
                {
                    null => null,
                    int id => id,
                    long longId => (int)longId,
                    _ => int.TryParse(value.ToString(), out int parsed) ? parsed : null
                };
            }
        }

        #endregion

        #region Progress Control Methods

        // Reserved for future progress integration

        #endregion

        #region Constructors

        public Control_ErrorReportsGrid()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);
            InitializeDataGridView();
            InitializeFilterControls();
        }

        #endregion

        #region Database Operations

    internal async Task LoadReportsAsync(Model_ErrorReportFilter? filter = null, Helper_StoredProcedureProgress? progressHelper = null)
        {
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;

            try
            {
                progressHelper?.ShowProgress("Loading error reports...");

                var result = await Dao_ErrorReports.GetAllErrorReportsAsync(filter, progressHelper);

                if (!result.IsSuccess)
                {
                    HandleLoadFailure(result.ErrorMessage ?? result.StatusMessage ?? "Failed to retrieve error reports.");
                    return;
                }

                DataTable data = result.Data ?? new DataTable();
                _lastFilter = filter;
                _bindingSource.DataSource = data;
                lblResultCount.Text = BuildResultCountText(data.Rows.Count);
                dgvErrorReports.ClearSelection();

                progressHelper?.ShowSuccess(result.StatusMessage ?? $"Loaded {data.Rows.Count} reports");
            }
            catch (Exception ex)
            {
                HandleLoadFailure(ex.Message, ex);
            }
            finally
            {
                progressHelper?.HideProgress();
                _isLoading = false;
            }
        }

        #endregion

        #region Key Processing

        private void HandleLoadFailure(string message, Exception? exception = null)
        {
            _bindingSource.DataSource = null;
            lblResultCount.Text = BuildResultCountText(0);
            if (exception != null)
            {
                Service_ErrorHandler.HandleException(exception, ErrorSeverity.Medium, controlName: Name);
            }
            else
            {
                Service_ErrorHandler.HandleException(new InvalidOperationException(message), ErrorSeverity.Medium, controlName: Name);
            }
        }

        #endregion

        #region Button Clicks

        private async void btnApplyFilters_Click(object? sender, EventArgs e)
        {
            await ApplyFiltersAsync();
        }

        private async void btnClearFilters_Click(object? sender, EventArgs e)
        {
            await ClearFiltersAsync();
        }

        #endregion

        #region ComboBox & UI Events

        private async void Control_ErrorReportsGrid_Load(object? sender, EventArgs e)
        {
            if (_hasLoaded || DesignMode)
            {
                return;
            }

            _hasLoaded = true;

            await PopulateStatusFilterAsync();
            await PopulateUserFilterAsync();
            await PopulateMachineFilterAsync();
            await LoadReportsAsync(null);
        }

        private void dgvErrorReports_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvErrorReports.Rows[e.RowIndex];
            string columnName = dgvErrorReports.Columns[e.ColumnIndex].Name;

            if (columnName == "colStatus")
            {
                string? status = row.Cells[e.ColumnIndex].Value?.ToString();
                row.DefaultCellStyle.BackColor = GetStatusColor(status);
            }

            if (columnName == "colErrorSummary" && e.Value is string summary && !string.IsNullOrWhiteSpace(summary))
            {
                if (summary.Length > 100)
                {
                    e.Value = summary.Substring(0, 100) + "...";
                    e.FormattingApplied = true;
                }

                row.Cells[e.ColumnIndex].ToolTipText = summary;
            }
        }

        private void dgvErrorReports_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvErrorReports.Rows[e.RowIndex];
            object? value = row.Cells["colReportId"].Value;

            if (value == null)
            {
                return;
            }

            int? reportId = value switch
            {
                int id => id,
                long longId => (int)longId,
                _ => int.TryParse(value.ToString(), out int parsed) ? parsed : null
            };

            if (reportId.HasValue)
            {
                OnReportSelected(reportId.Value);
            }
        }

        #endregion

        #region Helpers

        private void InitializeDataGridView()
        {
            dgvErrorReports.DataSource = _bindingSource;
            dgvErrorReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvErrorReports.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvErrorReports.EnableHeadersVisualStyles = false;
            dgvErrorReports.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlLight;
            dgvErrorReports.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            dgvErrorReports.ColumnHeadersDefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
        }

        private void InitializeFilterControls()
        {
            dtpDateFrom.Value = DateTime.Today.AddDays(-6);
            dtpDateFrom.Checked = false;
            dtpDateTo.Value = DateTime.Today;
            dtpDateTo.Checked = false;
            txtSearch.Clear();
        }

        private async Task PopulateUserFilterAsync()
        {
            cboUser.BeginUpdate();
            try
            {
                cboUser.Items.Clear();
                cboUser.Items.Add(AllUsersOption);

                var result = await Dao_ErrorReports.GetUserListAsync();
                if (result.IsSuccess && result.Data is { Count: > 0 })
                {
                    IEnumerable<string> users = result.Data
                        .Where(static user => !string.IsNullOrWhiteSpace(user))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .OrderBy(static user => user, StringComparer.OrdinalIgnoreCase);

                    foreach (string user in users)
                    {
                        cboUser.Items.Add(user);
                    }
                }
                else if (!result.IsSuccess)
                {
                    Service_ErrorHandler.HandleException(
                        new InvalidOperationException(result.ErrorMessage ?? "Failed to load user list."),
                        ErrorSeverity.Low,
                        controlName: Name);
                }

                if (cboUser.Items.Count == 0)
                {
                    cboUser.Items.Add(AllUsersOption);
                }

                cboUser.SelectedIndex = 0;
            }
            finally
            {
                cboUser.EndUpdate();
            }
        }

        private async Task PopulateMachineFilterAsync()
        {
            cboMachine.BeginUpdate();
            try
            {
                cboMachine.Items.Clear();
                cboMachine.Items.Add(AllMachinesOption);

                var result = await Dao_ErrorReports.GetMachineListAsync();
                if (result.IsSuccess && result.Data is { Count: > 0 })
                {
                    IEnumerable<string> machines = result.Data
                        .Where(static machine => !string.IsNullOrWhiteSpace(machine))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .OrderBy(static machine => machine, StringComparer.OrdinalIgnoreCase);

                    foreach (string machine in machines)
                    {
                        cboMachine.Items.Add(machine);
                    }
                }
                else if (!result.IsSuccess)
                {
                    Service_ErrorHandler.HandleException(
                        new InvalidOperationException(result.ErrorMessage ?? "Failed to load machine list."),
                        ErrorSeverity.Low,
                        controlName: Name);
                }

                if (cboMachine.Items.Count == 0)
                {
                    cboMachine.Items.Add(AllMachinesOption);
                }

                cboMachine.SelectedIndex = 0;
            }
            finally
            {
                cboMachine.EndUpdate();
            }
        }

        private Task PopulateStatusFilterAsync()
        {
            cboStatus.BeginUpdate();
            try
            {
                cboStatus.Items.Clear();
                cboStatus.Items.Add(AllStatusesOption);
                cboStatus.Items.Add("New");
                cboStatus.Items.Add("Reviewed");
                cboStatus.Items.Add("Resolved");
                cboStatus.SelectedIndex = 0;
            }
            finally
            {
                cboStatus.EndUpdate();
            }

            return Task.CompletedTask;
        }

        private async Task ApplyFiltersAsync()
        {
            var filter = BuildFilterFromControls();

            if (!filter.TryValidate(out var validationMessage))
            {
                Service_ErrorHandler.HandleValidationError(
                    validationMessage ?? "Invalid filter values supplied.",
                    field: "Error Report Filters",
                    controlName: Name);
                return;
            }

            if (!filter.HasFilters)
            {
                await LoadReportsAsync(null);
                return;
            }

            await LoadReportsAsync(filter);
        }

        private async Task ClearFiltersAsync()
        {
            dtpDateFrom.Checked = false;
            dtpDateFrom.Value = DateTime.Today.AddDays(-6);
            dtpDateTo.Checked = false;
            dtpDateTo.Value = DateTime.Today;
            if (cboUser.Items.Count == 0)
            {
                cboUser.Items.Add(AllUsersOption);
            }
            cboUser.SelectedIndex = 0;
            if (cboMachine.Items.Count == 0)
            {
                cboMachine.Items.Add(AllMachinesOption);
            }
            cboMachine.SelectedIndex = 0;
            if (cboStatus.Items.Count == 0)
            {
                cboStatus.Items.Add(AllStatusesOption);
                cboStatus.Items.Add("New");
                cboStatus.Items.Add("Reviewed");
                cboStatus.Items.Add("Resolved");
            }
            cboStatus.SelectedIndex = 0;
            txtSearch.Clear();

            await LoadReportsAsync(null);
        }

        private Model_ErrorReportFilter BuildFilterFromControls()
        {
            var filter = new Model_ErrorReportFilter();

            if (dtpDateFrom.Checked)
            {
                filter.DateFrom = dtpDateFrom.Value.Date;
            }

            if (dtpDateTo.Checked)
            {
                filter.DateTo = dtpDateTo.Value.Date;
            }

            if (cboUser.SelectedIndex > 0)
            {
                filter.UserName = cboUser.SelectedItem?.ToString();
            }

            if (cboMachine.SelectedIndex > 0)
            {
                filter.MachineName = cboMachine.SelectedItem?.ToString();
            }

            if (cboStatus.SelectedIndex > 0)
            {
                filter.Status = cboStatus.SelectedItem?.ToString();
            }

            string? search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                filter.SearchText = search;
            }

            return filter;
        }

        private static Color GetStatusColor(string? status)
        {
            return status switch
            {
                "New" => Color.LightCoral,
                "Reviewed" => Color.LightGoldenrodYellow,
                "Resolved" => Color.LightGreen,
                _ => Color.White
            };
        }

        private static string BuildResultCountText(int count)
        {
            return count switch
            {
                1 => "Showing 1 report",
                _ => $"Showing {count} reports"
            };
        }

        /// <summary>
        /// Programmatically selects and scrolls to the row containing the specified report ID.
        /// </summary>
        /// <param name="reportId">The error report ID to select.</param>
        /// <remarks>
        /// If the report ID is not found in the current grid data, no selection change occurs.
        /// The method safely handles grid state and ensures the selected row is visible.
        /// </remarks>
        public void SelectReportById(int reportId)
        {
            if (dgvErrorReports.Rows.Count == 0)
            {
                return;
            }

            dgvErrorReports.ClearSelection();

            foreach (DataGridViewRow row in dgvErrorReports.Rows)
            {
                object? value = row.Cells["colReportId"].Value;
                if (value == null)
                {
                    continue;
                }

                int? currentId = value switch
                {
                    int id => id,
                    long longId => (int)longId,
                    _ => int.TryParse(value.ToString(), out int parsed) ? parsed : null
                };

                if (currentId == reportId)
                {
                    row.Selected = true;
                    if (row.Index >= 0 && row.Index < dgvErrorReports.RowCount)
                    {
                        dgvErrorReports.CurrentCell = row.Cells["colReportId"];
                        try
                        {
                            dgvErrorReports.FirstDisplayedScrollingRowIndex = row.Index;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                    break;
                }
            }
        }

        private void OnReportSelected(int reportId)
        {
            ReportSelected?.Invoke(this, reportId);
        }

        #endregion

        #region Cleanup

        // No additional cleanup needed - dispose handled by base class

        #endregion

        public event EventHandler<int>? ReportSelected;
    }
}
