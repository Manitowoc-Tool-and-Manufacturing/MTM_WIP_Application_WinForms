using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.Transactions;

/// <summary>
/// UserControl for transaction search criteria input.
/// </summary>
/// <remarks>
/// Provides UI for filtering transactions by part number, user, locations,
/// operation, transaction type, date range, and notes. Supports quick date filters
/// (Today/Week/Month/Custom) and raises SearchRequested event when user initiates search.
/// </remarks>
internal partial class TransactionSearchControl : UserControl
{
    #region Events

    /// <summary>
    /// Raised when the user clicks the Search button with valid criteria.
    /// </summary>
    public event EventHandler<TransactionSearchCriteria>? SearchRequested;

    /// <summary>
    /// Raised when the user clicks the Reset button.
    /// </summary>
    public event EventHandler? ResetRequested;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionSearchControl"/> class.
    /// </summary>
    public TransactionSearchControl()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);

        WireUpEvents();
        InitializeDateRangeDefaults();
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Wires up event handlers for UI controls.
    /// </summary>
    private void WireUpEvents()
    {
        btnSearch.Click += BtnSearch_Click;
        btnReset.Click += BtnReset_Click;

        // Quick filter radio buttons
        rdoToday.CheckedChanged += QuickFilterChanged;
        rdoWeek.CheckedChanged += QuickFilterChanged;
        rdoMonth.CheckedChanged += QuickFilterChanged;
        rdoCustom.CheckedChanged += QuickFilterChanged;
    }

    /// <summary>
    /// Sets default date range to current month.
    /// </summary>
    private void InitializeDateRangeDefaults()
    {
        rdoMonth.Checked = true;
        ApplyQuickFilter();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Populates the Part Number dropdown with available parts.
    /// </summary>
    /// <param name="parts">List of part numbers.</param>
    public void LoadParts(List<string> parts)
    {
        cboPartNumber.Items.Clear();
        cboPartNumber.Items.Add(""); // Add empty option for "All Parts"
        cboPartNumber.Items.AddRange(parts.ToArray());
        if (cboPartNumber.Items.Count > 0)
        {
            cboPartNumber.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Populates the User dropdown with available users.
    /// </summary>
    /// <param name="users">List of usernames.</param>
    public void LoadUsers(List<string> users)
    {
        cboUser.Items.Clear();
        cboUser.Items.Add(""); // Add empty option for "All Users"
        cboUser.Items.AddRange(users.ToArray());
        if (cboUser.Items.Count > 0)
        {
            cboUser.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Populates the location dropdowns with available locations.
    /// </summary>
    /// <param name="locations">List of location codes.</param>
    public void LoadLocations(List<string> locations)
    {
        // From Location
        cboFromLocation.Items.Clear();
        cboFromLocation.Items.Add(""); // Add empty option for "All Locations"
        cboFromLocation.Items.AddRange(locations.ToArray());
        if (cboFromLocation.Items.Count > 0)
        {
            cboFromLocation.SelectedIndex = 0;
        }

        // To Location
        cboToLocation.Items.Clear();
        cboToLocation.Items.Add(""); // Add empty option for "All Locations"
        cboToLocation.Items.AddRange(locations.ToArray());
        if (cboToLocation.Items.Count > 0)
        {
            cboToLocation.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Clears all search criteria and resets to defaults.
    /// </summary>
    public void ClearCriteria()
    {
        cboPartNumber.SelectedIndex = 0;
        cboUser.SelectedIndex = 0;
        cboFromLocation.SelectedIndex = 0;
        cboToLocation.SelectedIndex = 0;
        txtOperation.Clear();
        txtNotes.Clear();

        // Reset transaction type checkboxes to all checked
        chkIN.Checked = true;
        chkOUT.Checked = true;
        chkTRANSFER.Checked = true;

        // Reset date range to month
        rdoMonth.Checked = true;
        ApplyQuickFilter();
    }

    #endregion

    #region Button Clicks

    private void BtnSearch_Click(object? sender, EventArgs e)
    {
        var criteria = BuildCriteria();

        // Validate criteria
        if (!criteria.IsValid())
        {
            Service_ErrorHandler.HandleValidationError(
                "Search criteria is incomplete. Please check your inputs.",
                "Search Validation"
            );
            return;
        }

        // Validate date range
        if (!criteria.IsDateRangeValid())
        {
            Service_ErrorHandler.HandleValidationError(
                "Invalid date range. 'Date From' must be before or equal to 'Date To'.",
                "Date Range Validation"
            );
            return;
        }

        // Validate at least one transaction type is selected
        if (string.IsNullOrWhiteSpace(criteria.TransactionType))
        {
            Service_ErrorHandler.HandleValidationError(
                "Please select at least one transaction type (IN, OUT, or TRANSFER).",
                "Transaction Type Validation"
            );
            return;
        }

        // Raise event with valid criteria
        SearchRequested?.Invoke(this, criteria);
    }

    private void BtnReset_Click(object? sender, EventArgs e)
    {
        ClearCriteria();
        ResetRequested?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region Quick Filter Handlers

    private void QuickFilterChanged(object? sender, EventArgs e)
    {
        if (sender is RadioButton rdo && rdo.Checked)
        {
            ApplyQuickFilter();
        }
    }

    private void ApplyQuickFilter()
    {
        var now = DateTime.Now;

        if (rdoToday.Checked)
        {
            // Today: 00:00 to 23:59
            dtpDateFrom.Value = now.Date;
            dtpDateTo.Value = now.Date.AddDays(1).AddSeconds(-1);
        }
        else if (rdoWeek.Checked)
        {
            // This Week: Monday to Sunday
            int daysFromMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday = now.Date.AddDays(-daysFromMonday);
            DateTime sunday = monday.AddDays(6);

            dtpDateFrom.Value = monday;
            dtpDateTo.Value = sunday.AddDays(1).AddSeconds(-1);
        }
        else if (rdoMonth.Checked)
        {
            // This Month: 1st to last day
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            dtpDateFrom.Value = firstDay;
            dtpDateTo.Value = lastDay.AddDays(1).AddSeconds(-1);
        }
        // Custom: user sets dates manually, no automatic adjustment
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Builds a TransactionSearchCriteria object from current UI values.
    /// </summary>
    /// <returns>Populated search criteria object.</returns>
    private TransactionSearchCriteria BuildCriteria()
    {
        var criteria = new TransactionSearchCriteria
        {
            PartID = string.IsNullOrWhiteSpace(cboPartNumber.Text) ? null : cboPartNumber.Text.Trim(),
            User = string.IsNullOrWhiteSpace(cboUser.Text) ? null : cboUser.Text.Trim(),
            FromLocation = string.IsNullOrWhiteSpace(cboFromLocation.Text) ? null : cboFromLocation.Text.Trim(),
            ToLocation = string.IsNullOrWhiteSpace(cboToLocation.Text) ? null : cboToLocation.Text.Trim(),
            Operation = string.IsNullOrWhiteSpace(txtOperation.Text) ? null : txtOperation.Text.Trim(),
            Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
            DateFrom = dtpDateFrom.Value.Date,
            DateTo = dtpDateTo.Value.Date.AddDays(1).AddSeconds(-1) // End of selected day
        };

        // Build transaction type string from checked boxes
        var types = new List<string>();
        if (chkIN.Checked) types.Add("IN");
        if (chkOUT.Checked) types.Add("OUT");
        if (chkTRANSFER.Checked) types.Add("TRANSFER");

        criteria.TransactionType = types.Count > 0 ? string.Join(",", types) : null;

        return criteria;
    }

    #endregion
}
