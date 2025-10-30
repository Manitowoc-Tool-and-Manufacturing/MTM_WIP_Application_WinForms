using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_WinForms.Models;

namespace MTM_WIP_Application_Winforms.Controls.ErrorReports
{
    public partial class Control_ErrorReportDetails : UserControl
    {
        #region Fields

        private Model_ErrorReport? _currentReport;
        private bool _isLoading;
        private readonly Dictionary<ErrorReportStatus, string> _statusDisplay = new()
        {
            { ErrorReportStatus.New, "New" },
            { ErrorReportStatus.Reviewed, "Reviewed" },
            { ErrorReportStatus.Resolved, "Resolved" }
        };

        private const string PlaceholderNoData = "(No data provided)";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently loaded error report, or null if no report is loaded.
        /// </summary>
        public Model_ErrorReport? CurrentReport => _currentReport;

        /// <summary>
        /// Gets a value indicating whether the control is currently loading report data.
        /// </summary>
        public bool IsLoading => _isLoading;

        #endregion

        #region Constructors

        public Control_ErrorReportDetails()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);
            ClearDisplay();
        }

        #endregion

        #region Database Operations

        /// <summary>
        /// Asynchronously loads an error report by ID and populates the control with report details.
        /// </summary>
        /// <param name="reportId">The error report ID to load.</param>
        /// <returns>A task representing the asynchronous load operation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when reportId is less than or equal to 0.</exception>
        public async Task LoadReportAsync(int reportId)
        {
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;

            try
            {
                ToggleLoadingState(true);

                var result = await Dao_ErrorReports.GetErrorReportByIdAsync(reportId);

                if (!result.IsSuccess || result.Data == null)
                {
                    HandleLoadFailure(result.ErrorMessage ?? result.StatusMessage ?? "Unable to load error report details.");
                    return;
                }

                PopulateFromModel(result.Data);
                Service_DebugTracer.TraceUIAction("ERROR_REPORT_DETAILS_LOADED", nameof(Control_ErrorReportDetails), new Dictionary<string, object>
                {
                    ["ReportID"] = result.Data.ReportID,
                    ["Status"] = result.Data.Status.ToString()
                });
            }
            catch (Exception ex)
            {
                HandleLoadFailure(ex.Message, ex);
            }
            finally
            {
                ToggleLoadingState(false);
                _isLoading = false;
            }
        }

        #endregion

        #region Helpers

        private void PopulateFromModel(Model_ErrorReport report)
        {
            _currentReport = report;
            txtReportId.Text = report.ReportID.ToString();
            txtReportDate.Text = report.ReportDate.ToString("yyyy-MM-dd HH:mm:ss");
            txtUserName.Text = string.IsNullOrWhiteSpace(report.UserName) ? PlaceholderNoData : report.UserName;
            txtMachineName.Text = string.IsNullOrWhiteSpace(report.MachineName) ? PlaceholderNoData : report.MachineName;
            txtAppVersion.Text = string.IsNullOrWhiteSpace(report.AppVersion) ? PlaceholderNoData : report.AppVersion;
            txtErrorType.Text = string.IsNullOrWhiteSpace(report.ErrorType) ? PlaceholderNoData : report.ErrorType;
            txtStatus.Text = _statusDisplay.TryGetValue(report.Status, out string? statusText) ? statusText : report.Status.ToString();
            txtReviewedBy.Text = string.IsNullOrWhiteSpace(report.ReviewedBy) ? PlaceholderNoData : report.ReviewedBy;
            txtReviewedDate.Text = report.ReviewedDate.HasValue ? report.ReviewedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : PlaceholderNoData;
            txtErrorSummary.Text = string.IsNullOrWhiteSpace(report.ErrorSummary) ? PlaceholderNoData : report.ErrorSummary;
            txtUserNotes.Text = string.IsNullOrWhiteSpace(report.UserNotes) ? PlaceholderNoData : report.UserNotes;
            txtTechnicalDetails.Text = string.IsNullOrWhiteSpace(report.TechnicalDetails) ? PlaceholderNoData : report.TechnicalDetails;
            txtCallStack.Text = string.IsNullOrWhiteSpace(report.CallStack) ? PlaceholderNoData : report.CallStack;
            txtDeveloperNotes.Text = string.IsNullOrWhiteSpace(report.DeveloperNotes) ? PlaceholderNoData : report.DeveloperNotes;

            UpdateStatusButtons(report.Status);
        }

        private void ClearDisplay()
        {
            _currentReport = null;
            lblHeader.Text = "Error Report Details";
            txtReportId.Text = "";
            txtReportDate.Text = "";
            txtUserName.Text = "";
            txtMachineName.Text = "";
            txtAppVersion.Text = "";
            txtErrorType.Text = "";
            txtStatus.Text = "";
            txtReviewedBy.Text = "";
            txtReviewedDate.Text = "";
            txtErrorSummary.Text = "Select a report to view details.";
            txtUserNotes.Text = PlaceholderNoData;
            txtTechnicalDetails.Text = PlaceholderNoData;
            txtCallStack.Text = PlaceholderNoData;
            txtDeveloperNotes.Text = PlaceholderNoData;
            UpdateStatusButtons(null);
        }

        private void ToggleLoadingState(bool isLoading)
        {
            lblHeader.Text = isLoading ? "Loading error report details..." : "Error Report Details";
            UseWaitCursor = isLoading;
            foreach (Control control in tableLayoutMain.Controls)
            {
                control.Enabled = !isLoading;
            }
        }

        private void HandleLoadFailure(string message, Exception? exception = null)
        {
            ClearDisplay();
            lblHeader.Text = "Error loading report details";

            if (exception != null)
            {
                Service_ErrorHandler.HandleException(exception, ErrorSeverity.Medium, controlName: Name);
            }
            else
            {
                Service_ErrorHandler.HandleException(new InvalidOperationException(message), ErrorSeverity.Medium, controlName: Name);
            }
        }

        private void UpdateStatusButtons(ErrorReportStatus? status)
        {
            btnMarkReviewed.Visible = false;
            btnMarkResolved.Visible = false;
            btnMarkReviewed.Text = "Mark as Reviewed";

            if (status is null)
            {
                return;
            }

            switch (status)
            {
                case ErrorReportStatus.New:
                    btnMarkReviewed.Visible = true;
                    btnMarkResolved.Visible = true;
                    break;
                case ErrorReportStatus.Reviewed:
                    btnMarkResolved.Visible = true;
                    break;
                case ErrorReportStatus.Resolved:
                    btnMarkReviewed.Visible = true;
                    btnMarkReviewed.Text = "Reopen (Mark as Reviewed)";
                    break;
                default:
                    btnMarkReviewed.Visible = true;
                    btnMarkResolved.Visible = true;
                    break;
            }
        }

        #endregion

        #region Button Clicks

        private void btnCopyAll_Click(object? sender, EventArgs e)
        {
            if (_currentReport == null)
            {
                return;
            }

            StringBuilder builder = new();
            builder.AppendLine($"Report #{_currentReport.ReportID}");
            builder.AppendLine($"Date: {_currentReport.ReportDate:yyyy-MM-dd HH:mm:ss}");
            builder.AppendLine($"User: {_currentReport.UserName ?? PlaceholderNoData}");
            builder.AppendLine($"Machine: {_currentReport.MachineName ?? PlaceholderNoData}");
            builder.AppendLine($"Version: {_currentReport.AppVersion ?? PlaceholderNoData}");
            builder.AppendLine($"Error Type: {_currentReport.ErrorType ?? PlaceholderNoData}");
            builder.AppendLine($"Status: {(_statusDisplay.TryGetValue(_currentReport.Status, out string? statusText) ? statusText : _currentReport.Status.ToString())}");
            builder.AppendLine($"Reviewed By: {_currentReport.ReviewedBy ?? PlaceholderNoData}");
            builder.AppendLine($"Reviewed Date: {(_currentReport.ReviewedDate.HasValue ? _currentReport.ReviewedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : PlaceholderNoData)}");
            builder.AppendLine();
            builder.AppendLine("Summary:");
            builder.AppendLine(_currentReport.ErrorSummary ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("User Notes:");
            builder.AppendLine(_currentReport.UserNotes ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("Developer Notes:");
            builder.AppendLine(_currentReport.DeveloperNotes ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("Technical Details:");
            builder.AppendLine(_currentReport.TechnicalDetails ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("Call Stack:");
            builder.AppendLine(_currentReport.CallStack ?? PlaceholderNoData);

            Clipboard.SetText(builder.ToString());
            Service_ErrorHandler.ShowInformation("Error report details copied to clipboard.", "Copy Complete");
        }

        private void btnExportReport_Click(object? sender, EventArgs e)
        {
            if (_currentReport == null)
            {
                Service_ErrorHandler.ShowWarning("Select a report before exporting.", "Export Report");
                return;
            }

            using SaveFileDialog dialog = new()
            {
                Title = "Export Error Report",
                Filter = "Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json",
                FileName = $"ErrorReport_{_currentReport.ReportID}_{DateTime.Now:yyyyMMddHHmmss}"
            };

            if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
            {
                return;
            }

            try
            {
                string extension = System.IO.Path.GetExtension(dialog.FileName).ToLowerInvariant();
                string content = extension switch
                {
                    ".json" => System.Text.Json.JsonSerializer.Serialize(_currentReport, new System.Text.Json.JsonSerializerOptions
                    {
                        WriteIndented = true
                    }),
                    _ => BuildPlainTextExport(_currentReport)
                };

                System.IO.File.WriteAllText(dialog.FileName, content, Encoding.UTF8);
                Service_ErrorHandler.ShowInformation("Error report exported successfully.", "Export Complete");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, controlName: Name);
            }
        }

        private static string BuildPlainTextExport(Model_ErrorReport report)
        {
            StringBuilder builder = new();
            builder.AppendLine($"Report #{report.ReportID}");
            builder.AppendLine(new string('=', 60));
            builder.AppendLine($"Timestamp: {report.ReportDate:yyyy-MM-dd HH:mm:ss}");
            builder.AppendLine($"User: {report.UserName ?? PlaceholderNoData}");
            builder.AppendLine($"Machine: {report.MachineName ?? PlaceholderNoData}");
            builder.AppendLine($"App Version: {report.AppVersion ?? PlaceholderNoData}");
            builder.AppendLine($"Status: {report.Status}");
            builder.AppendLine($"Reviewed By: {report.ReviewedBy ?? PlaceholderNoData}");
            builder.AppendLine($"Reviewed Date: {(report.ReviewedDate.HasValue ? report.ReviewedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : PlaceholderNoData)}");
            builder.AppendLine();
            builder.AppendLine("Error Summary:");
            builder.AppendLine(report.ErrorSummary ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("User Notes:");
            builder.AppendLine(report.UserNotes ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("Developer Notes:");
            builder.AppendLine(report.DeveloperNotes ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("Technical Details:");
            builder.AppendLine(report.TechnicalDetails ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("Call Stack:");
            builder.AppendLine(report.CallStack ?? PlaceholderNoData);
            builder.AppendLine();
            builder.AppendLine("Exported: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return builder.ToString();
        }

        private async void btnMarkReviewed_Click(object? sender, EventArgs e)
        {
            await ChangeStatusAsync(ErrorReportStatus.Reviewed);
        }

        private async void btnMarkResolved_Click(object? sender, EventArgs e)
        {
            await ChangeStatusAsync(ErrorReportStatus.Resolved);
        }

        private async Task ChangeStatusAsync(ErrorReportStatus targetStatus)
        {
            if (_currentReport == null || _isLoading)
            {
                return;
            }

            string statusText = _statusDisplay.TryGetValue(targetStatus, out string? display) ? display : targetStatus.ToString();
            string confirmationMessage =
                $"Are you sure you want to change report #{_currentReport.ReportID} to status '{statusText}'?\n\n" +
                "You can add developer notes in the next step.";

            DialogResult confirmationResult = Service_ErrorHandler.ShowConfirmation(
                confirmationMessage,
                "Confirm Status Change");

            if (confirmationResult != DialogResult.Yes)
            {
                return;
            }

            string existingNotes = _currentReport.DeveloperNotes ?? string.Empty;

            using StatusChangeNotesDialog notesDialog = new(statusText, existingNotes);
            IWin32Window owner = (IWin32Window?)FindForm() ?? this;
            if (notesDialog.ShowDialog(owner) != DialogResult.OK)
            {
                return;
            }

            string? developerNotes = string.IsNullOrWhiteSpace(notesDialog.DeveloperNotes)
                ? null
                : notesDialog.DeveloperNotes.Trim();

            string reviewedBy = Model_AppVariables.User ?? Environment.UserName ?? "UNKNOWN";

            try
            {
                UseWaitCursor = true;

                var result = await Dao_ErrorReports.UpdateErrorReportStatusAsync(
                    _currentReport.ReportID,
                    statusText,
                    developerNotes,
                    reviewedBy);

                if (!result.IsSuccess)
                {
                    string errorMessage = result.ErrorMessage
                        ?? result.StatusMessage
                        ?? "Failed to update error report status.";

                    Service_ErrorHandler.HandleException(
                        new InvalidOperationException(errorMessage),
                        ErrorSeverity.Medium,
                        controlName: Name);
                    return;
                }

                Service_DebugTracer.TraceUIAction(
                    "ERROR_REPORT_STATUS_UPDATED",
                    nameof(Control_ErrorReportDetails),
                    new Dictionary<string, object>
                    {
                        ["ReportID"] = _currentReport.ReportID,
                        ["TargetStatus"] = statusText,
                        ["ReviewedBy"] = reviewedBy
                    });

                await LoadReportAsync(_currentReport.ReportID);

                Service_ErrorHandler.ShowInformation(
                    $"Report #{_currentReport?.ReportID} marked as {statusText}.",
                    "Status Updated",
                    controlName: Name);

                if (_currentReport != null)
                {
                    OnStatusChanged(new StatusChangedEventArgs(_currentReport.ReportID, _currentReport.Status));
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.High, controlName: Name);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        #endregion

        #region Events

        public event EventHandler<StatusChangedEventArgs>? StatusChanged;

        protected virtual void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Event arguments for the StatusChanged event, providing information about the updated error report.
        /// </summary>
        public sealed class StatusChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="StatusChangedEventArgs"/> class.
            /// </summary>
            /// <param name="reportId">The error report ID that was updated.</param>
            /// <param name="newStatus">The new status value after the update.</param>
            public StatusChangedEventArgs(int reportId, ErrorReportStatus newStatus)
            {
                ReportId = reportId;
                NewStatus = newStatus;
            }

            /// <summary>
            /// Gets the error report ID that was updated.
            /// </summary>
            public int ReportId { get; }

            /// <summary>
            /// Gets the new status value after the update.
            /// </summary>
            public ErrorReportStatus NewStatus { get; }
        }

        private sealed class StatusChangeNotesDialog : Form
        {
            private readonly TextBox _txtNotes;

            internal StatusChangeNotesDialog(string statusText, string existingNotes)
            {
                Text = $"Developer Notes - {statusText}";
                StartPosition = FormStartPosition.CenterParent;
                MinimizeBox = false;
                MaximizeBox = false;
                ShowInTaskbar = false;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                Size = new Size(480, 360);

                Label lblMessage = new()
                {
                    Text = $"Optional: Add developer notes for status '{statusText}'. Leave blank to keep existing notes.",
                    Dock = DockStyle.Top,
                    Padding = new Padding(10),
                    AutoSize = true
                };

                _txtNotes = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    ScrollBars = ScrollBars.Vertical,
                    Text = existingNotes,
                    Margin = new Padding(10)
                };

                FlowLayoutPanel buttonPanel = new()
                {
                    Dock = DockStyle.Bottom,
                    FlowDirection = FlowDirection.RightToLeft,
                    Padding = new Padding(10),
                    AutoSize = true
                };

                Button btnOk = new()
                {
                    Text = "Save",
                    DialogResult = DialogResult.OK,
                    AutoSize = true,
                    Padding = new Padding(12, 6, 12, 6)
                };

                Button btnCancel = new()
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    AutoSize = true,
                    Padding = new Padding(12, 6, 12, 6)
                };

                buttonPanel.Controls.Add(btnOk);
                buttonPanel.Controls.Add(btnCancel);

                Controls.Add(_txtNotes);
                Controls.Add(buttonPanel);
                Controls.Add(lblMessage);

                AcceptButton = btnOk;
                CancelButton = btnCancel;

                Core_Themes.ApplyDpiScaling(this);
            }

            internal string DeveloperNotes => _txtNotes.Text;
        }

        #endregion
    }
}
