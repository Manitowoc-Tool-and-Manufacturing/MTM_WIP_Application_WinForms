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
        TransactionSearchControl_Button_Search.Click += BtnSearch_Click;
        TransactionSearchControl_Button_Reset.Click += BtnReset_Click;

        // Quick filter radio buttons
        TransactionSearchControl_RadioButton_Today.CheckedChanged += QuickFilterChanged;
        TransactionSearchControl_RadioButton_Week.CheckedChanged += QuickFilterChanged;
        TransactionSearchControl_RadioButton_Month.CheckedChanged += QuickFilterChanged;
        TransactionSearchControl_RadioButton_Custom.CheckedChanged += QuickFilterChanged;
    }

    /// <summary>
    /// Sets default date range to current month.
    /// </summary>
    private void InitializeDateRangeDefaults()
    {
        TransactionSearchControl_RadioButton_Month.Checked = true;
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
        TransactionSearchControl_ComboBox_PartNumber.Items.Clear();
        TransactionSearchControl_ComboBox_PartNumber.Items.Add(""); // Add empty option for "All Parts"
        TransactionSearchControl_ComboBox_PartNumber.Items.AddRange(parts.ToArray());
        if (TransactionSearchControl_ComboBox_PartNumber.Items.Count > 0)
        {
            TransactionSearchControl_ComboBox_PartNumber.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Populates the User dropdown with available users.
    /// </summary>
    /// <param name="users">List of usernames.</param>
    public void LoadUsers(List<string> users)
    {
        TransactionSearchControl_ComboBox_User.Items.Clear();
        TransactionSearchControl_ComboBox_User.Items.Add(""); // Add empty option for "All Users"
        TransactionSearchControl_ComboBox_User.Items.AddRange(users.ToArray());
        if (TransactionSearchControl_ComboBox_User.Items.Count > 0)
        {
            TransactionSearchControl_ComboBox_User.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Populates the location dropdowns with available locations.
    /// </summary>
    /// <param name="locations">List of location codes.</param>
    public void LoadLocations(List<string> locations)
    {
        // From Location
        TransactionSearchControl_ComboBox_FromLocation.Items.Clear();
        TransactionSearchControl_ComboBox_FromLocation.Items.Add(""); // Add empty option for "All Locations"
        TransactionSearchControl_ComboBox_FromLocation.Items.AddRange(locations.ToArray());
        if (TransactionSearchControl_ComboBox_FromLocation.Items.Count > 0)
        {
            TransactionSearchControl_ComboBox_FromLocation.SelectedIndex = 0;
        }

        // To Location
        TransactionSearchControl_ComboBox_ToLocation.Items.Clear();
        TransactionSearchControl_ComboBox_ToLocation.Items.Add(""); // Add empty option for "All Locations"
        TransactionSearchControl_ComboBox_ToLocation.Items.AddRange(locations.ToArray());
        if (TransactionSearchControl_ComboBox_ToLocation.Items.Count > 0)
        {
            TransactionSearchControl_ComboBox_ToLocation.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Clears all search criteria and resets to defaults.
    /// </summary>
    public void ClearCriteria()
    {
        TransactionSearchControl_ComboBox_PartNumber.SelectedIndex = 0;
        TransactionSearchControl_ComboBox_User.SelectedIndex = 0;
        TransactionSearchControl_ComboBox_FromLocation.SelectedIndex = 0;
        TransactionSearchControl_ComboBox_ToLocation.SelectedIndex = 0;
        TransactionSearchControl_TextBox_Operation.Clear();
        TransactionSearchControl_TextBox_Notes.Clear();

        // Reset transaction type checkboxes to all checked
        TransactionSearchControl_CheckBox_IN.Checked = true;
        TransactionSearchControl_CheckBox_OUT.Checked = true;
        TransactionSearchControl_CheckBox_TRANSFER.Checked = true;

        // Reset date range to month
        TransactionSearchControl_RadioButton_Month.Checked = true;
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

        if (TransactionSearchControl_RadioButton_Today.Checked)
        {
            // Today: 00:00 to 23:59
            TransactionSearchControl_DateTimePicker_DateFrom.Value = now.Date;
            TransactionSearchControl_DateTimePicker_DateTo.Value = now.Date.AddDays(1).AddSeconds(-1);
        }
        else if (TransactionSearchControl_RadioButton_Week.Checked)
        {
            // This Week: Monday to Sunday
            int daysFromMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday = now.Date.AddDays(-daysFromMonday);
            DateTime sunday = monday.AddDays(6);

            TransactionSearchControl_DateTimePicker_DateFrom.Value = monday;
            TransactionSearchControl_DateTimePicker_DateTo.Value = sunday.AddDays(1).AddSeconds(-1);
        }
        else if (TransactionSearchControl_RadioButton_Month.Checked)
        {
            // This Month: 1st to last day
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            TransactionSearchControl_DateTimePicker_DateFrom.Value = firstDay;
            TransactionSearchControl_DateTimePicker_DateTo.Value = lastDay.AddDays(1).AddSeconds(-1);
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
            PartID = string.IsNullOrWhiteSpace(TransactionSearchControl_ComboBox_PartNumber.Text) ? null : TransactionSearchControl_ComboBox_PartNumber.Text.Trim(),
            User = string.IsNullOrWhiteSpace(TransactionSearchControl_ComboBox_User.Text) ? null : TransactionSearchControl_ComboBox_User.Text.Trim(),
            FromLocation = string.IsNullOrWhiteSpace(TransactionSearchControl_ComboBox_FromLocation.Text) ? null : TransactionSearchControl_ComboBox_FromLocation.Text.Trim(),
            ToLocation = string.IsNullOrWhiteSpace(TransactionSearchControl_ComboBox_ToLocation.Text) ? null : TransactionSearchControl_ComboBox_ToLocation.Text.Trim(),
            Operation = string.IsNullOrWhiteSpace(TransactionSearchControl_TextBox_Operation.Text) ? null : TransactionSearchControl_TextBox_Operation.Text.Trim(),
            Notes = string.IsNullOrWhiteSpace(TransactionSearchControl_TextBox_Notes.Text) ? null : TransactionSearchControl_TextBox_Notes.Text.Trim(),
            DateFrom = TransactionSearchControl_DateTimePicker_DateFrom.Value.Date,
            DateTo = TransactionSearchControl_DateTimePicker_DateTo.Value.Date.AddDays(1).AddSeconds(-1) // End of selected day
        };

        // Build transaction type string from checked boxes
        var types = new List<string>();
        if (TransactionSearchControl_CheckBox_IN.Checked) types.Add("IN");
        if (TransactionSearchControl_CheckBox_OUT.Checked) types.Add("OUT");
        if (TransactionSearchControl_CheckBox_TRANSFER.Checked) types.Add("TRANSFER");

        criteria.TransactionType = types.Count > 0 ? string.Join(",", types) : null;

        return criteria;
    }

    #endregion
}

