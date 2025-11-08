using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_WinForms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Forms.ErrorDialog;

/// <summary>
/// Dialog form for reporting errors with user-provided contextual notes.
/// Supports both online (direct database submission) and offline (queued file) reporting.
/// Uses TableLayoutPanel for responsive grid-based layout.
/// </summary>
/// <remarks>
/// Layout structure:
/// - Row 1: Error Summary label (25px fixed)
/// - Row 2: Error Summary textbox (90px fixed, read-only)
/// - Row 3: User Notes label (25px fixed)
/// - Row 4: User Notes textbox (Percent 100%, expandable)
/// - Row 5: Button row (38px fixed)
/// Column sizing: Single column (100%)
/// Minimum size: 600x350
/// Form is resizable to allow users to expand notes area as needed
/// </remarks>
public partial class Form_ReportIssue : Form
{
    #region Fields

    private readonly Model_ErrorReport_Core _report;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the Form_ReportIssue dialog.
    /// </summary>
    /// <param name="report">Pre-populated error report with exception details.</param>
    /// <exception cref="ArgumentNullException">Thrown when report is null.</exception>
    public Form_ReportIssue(Model_ErrorReport_Core report)
    {
        ArgumentNullException.ThrowIfNull(report);
        
        _report = report;
        
        InitializeComponent();
        
        // Apply theme and scaling
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        // Populate error summary (read-only display of exception details)
        // DEBUG: Log what we're setting
        LoggingUtility.Log($"[Form_ReportIssue] Setting txtErrorSummary.Text to: '{_report.ErrorSummary ?? "(null)"}'");
        txtErrorSummary.Text = _report.ErrorSummary ?? "An error occurred.";
        LoggingUtility.Log($"[Form_ReportIssue] After setting, txtErrorSummary.Text = '{txtErrorSummary.Text}'");
        
        // Set focus to user notes field
        txtUserNotes.Select();
    }

    #endregion

    #region Button Clicks

    /// <summary>
    /// Handles the Submit button click event.
    /// Captures user notes, checks database connectivity, and submits report either online or offline.
    /// </summary>
    private async void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // Disable submit button to prevent double-submission
            btnSubmit.Enabled = false;
            Cursor = Cursors.WaitCursor;
            
            // Capture user notes
            _report.UserNotes = txtUserNotes.Text.Trim();
            
            // Check database connectivity
            bool isOnline = await CheckDatabaseConnectivityAsync();
            
            if (isOnline)
            {
                // Online: Submit directly to database
                var result = await Dao_ErrorReports.InsertReportAsync(_report);
                
                if (result.IsSuccess)
                {
                    // Success - show report ID
                    Service_ErrorHandler.ShowInformation(
                        result.StatusMessage ?? $"Error report submitted successfully. Report ID: {result.Data}",
                        "Report Submitted");
                    
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    // Database operation failed
                    Service_ErrorHandler.HandleException(
                        result.Exception ?? new Exception(result.ErrorMessage),
                        Enum_ErrorSeverity.Medium,
                        controlName: nameof(Form_ReportIssue));
                    
                    // Re-enable submit button for retry
                    btnSubmit.Enabled = true;
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                // Offline: Queue report for later submission
                var queueResult = await Service_ErrorReportQueue.QueueReportAsync(_report);
                
                if (queueResult.IsSuccess)
                {
                    Service_ErrorHandler.ShowInformation(
                        queueResult.StatusMessage ?? "Report queued. It will be submitted when connection is restored.",
                        "Queued for Later");
                    
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    Service_ErrorHandler.HandleException(
                        queueResult.Exception ?? new Exception(queueResult.ErrorMessage),
                        Enum_ErrorSeverity.Medium,
                        controlName: nameof(Form_ReportIssue));
                    
                    // Re-enable submit button for retry
                    btnSubmit.Enabled = true;
                    Cursor = Cursors.Default;
                }
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            
            Service_ErrorHandler.HandleException(
                ex,
                Enum_ErrorSeverity.Medium,
                contextData: new System.Collections.Generic.Dictionary<string, object>
                {
                    ["User"] = _report.UserName,
                    ["ErrorType"] = _report.ErrorType ?? "Unknown"
                },
                controlName: nameof(Form_ReportIssue));
            
            // Re-enable submit button
            btnSubmit.Enabled = true;
            Cursor = Cursors.Default;
        }
    }

    /// <summary>
    /// Handles the Cancel button click event.
    /// </summary>
    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Checks if the database is available by attempting to open a connection.
    /// </summary>
    /// <returns>True if database is reachable, false otherwise.</returns>
    private async Task<bool> CheckDatabaseConnectivityAsync()
    {
        try
        {
            string connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
            
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                
                // Execute simple query to verify connectivity
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT 1";
                    await command.ExecuteScalarAsync();
                }
                
                LoggingUtility.Log("[Form_ReportIssue] Database connectivity check: ONLINE");
                return true;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Form_ReportIssue] Database connectivity check: OFFLINE - {ex.Message}");
            return false;
        }
    }

    #endregion
}
