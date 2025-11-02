using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
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

        LoggingUtility.Log("[TransactionSearchControl] Initializing...");

        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);

        WireUpEvents();
        InitializeDateRangeDefaults();

        LoggingUtility.Log("[TransactionSearchControl] Initialization complete.");
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
        try
        {
            LoggingUtility.Log($"[TransactionSearchControl] LoadParts called with {parts?.Count ?? 0} parts");

            TransactionSearchControl_ComboBox_PartNumber.Items.Clear();
            TransactionSearchControl_ComboBox_PartNumber.Items.Add(""); // Add empty option for "All Parts"
            
            if (parts != null && parts.Count > 0)
            {
                TransactionSearchControl_ComboBox_PartNumber.Items.AddRange(parts.ToArray());
            }
            
            if (TransactionSearchControl_ComboBox_PartNumber.Items.Count > 0)
            {
                TransactionSearchControl_ComboBox_PartNumber.SelectedIndex = 0;
            }
            
            LoggingUtility.Log($"[TransactionSearchControl] Parts loaded: {TransactionSearchControl_ComboBox_PartNumber.Items.Count} items");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionSearchControl] Exception in LoadParts: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);
            
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["PartsCount"] = parts?.Count ?? 0,
                    ["Method"] = "LoadParts"
                },
                controlName: nameof(TransactionSearchControl));
        }
    }

    /// <summary>
    /// Populates the User dropdown with available users.
    /// </summary>
    /// <param name="users">List of usernames.</param>
    public void LoadUsers(List<string> users)
    {
        try
        {
            LoggingUtility.Log($"[TransactionSearchControl] LoadUsers called with {users?.Count ?? 0} users");

            TransactionSearchControl_ComboBox_User.Items.Clear();
            TransactionSearchControl_ComboBox_User.Items.Add(""); // Add empty option for "All Users"
            
            if (users != null && users.Count > 0)
            {
                TransactionSearchControl_ComboBox_User.Items.AddRange(users.ToArray());
            }
            
            if (TransactionSearchControl_ComboBox_User.Items.Count > 0)
            {
                TransactionSearchControl_ComboBox_User.SelectedIndex = 0;
            }
            
            LoggingUtility.Log($"[TransactionSearchControl] Users loaded: {TransactionSearchControl_ComboBox_User.Items.Count} items");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionSearchControl] Exception in LoadUsers: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);
            
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["UsersCount"] = users?.Count ?? 0,
                    ["Method"] = "LoadUsers"
                },
                controlName: nameof(TransactionSearchControl));
        }
    }

    /// <summary>
    /// Populates the location dropdowns with available locations.
    /// </summary>
    /// <param name="locations">List of location codes.</param>
    public void LoadLocations(List<string> locations)
    {
        try
        {
            LoggingUtility.Log($"[TransactionSearchControl] LoadLocations called with {locations?.Count ?? 0} locations");

            if (locations == null || locations.Count == 0)
            {
                LoggingUtility.Log("[TransactionSearchControl] WARNING: Locations list is null or empty");
                return;
            }

            // From Location
            LoggingUtility.Log("[TransactionSearchControl] Loading FromLocation combobox...");
            TransactionSearchControl_ComboBox_FromLocation.Items.Clear();
            TransactionSearchControl_ComboBox_FromLocation.Items.Add(""); // Add empty option for "All Locations"
            
            foreach (var location in locations)
            {
                if (!string.IsNullOrWhiteSpace(location))
                {
                    TransactionSearchControl_ComboBox_FromLocation.Items.Add(location);
                }
            }
            
            if (TransactionSearchControl_ComboBox_FromLocation.Items.Count > 0)
            {
                TransactionSearchControl_ComboBox_FromLocation.SelectedIndex = 0;
            }
            LoggingUtility.Log($"[TransactionSearchControl] FromLocation loaded: {TransactionSearchControl_ComboBox_FromLocation.Items.Count} items");

            // To Location
            LoggingUtility.Log("[TransactionSearchControl] Loading ToLocation combobox...");
            TransactionSearchControl_ComboBox_ToLocation.Items.Clear();
            TransactionSearchControl_ComboBox_ToLocation.Items.Add(""); // Add empty option for "All Locations"
            
            foreach (var location in locations)
            {
                if (!string.IsNullOrWhiteSpace(location))
                {
                    TransactionSearchControl_ComboBox_ToLocation.Items.Add(location);
                }
            }
            
            if (TransactionSearchControl_ComboBox_ToLocation.Items.Count > 0)
            {
                TransactionSearchControl_ComboBox_ToLocation.SelectedIndex = 0;
            }
            LoggingUtility.Log($"[TransactionSearchControl] ToLocation loaded: {TransactionSearchControl_ComboBox_ToLocation.Items.Count} items");
        }
        catch (ArgumentException argEx)
        {
            LoggingUtility.Log($"[TransactionSearchControl] ArgumentException in LoadLocations: {argEx.Message}");
            LoggingUtility.Log($"[TransactionSearchControl] ArgumentException ParamName: {argEx.ParamName}");
            LoggingUtility.Log($"[TransactionSearchControl] ArgumentException StackTrace: {argEx.StackTrace}");
            LoggingUtility.LogApplicationError(argEx);
            
            Service_ErrorHandler.HandleException(argEx, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["LocationsCount"] = locations?.Count ?? 0,
                    ["Method"] = "LoadLocations"
                },
                controlName: nameof(TransactionSearchControl));
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionSearchControl] Exception in LoadLocations: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);
            
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["LocationsCount"] = locations?.Count ?? 0,
                    ["Method"] = "LoadLocations"
                },
                controlName: nameof(TransactionSearchControl));
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
        try
        {
            LoggingUtility.Log("[TransactionSearchControl] Search button clicked.");

            var criteria = BuildCriteria();

            // Validate criteria
            if (!criteria.IsValid())
            {
                LoggingUtility.Log("[TransactionSearchControl] Search validation failed: Incomplete criteria.");
                Service_ErrorHandler.HandleValidationError(
                    "Search criteria is incomplete. Please check your inputs.",
                    "Search Validation"
                );
                return;
            }

            // Validate date range
            if (!criteria.IsDateRangeValid())
            {
                LoggingUtility.Log("[TransactionSearchControl] Search validation failed: Invalid date range.");
                Service_ErrorHandler.HandleValidationError(
                    "Invalid date range. 'Date From' must be before or equal to 'Date To'.",
                    "Date Range Validation"
                );
                return;
            }

            // Validate at least one transaction type is selected
            if (string.IsNullOrWhiteSpace(criteria.TransactionType))
            {
                LoggingUtility.Log("[TransactionSearchControl] Search validation failed: No transaction type selected.");
                Service_ErrorHandler.HandleValidationError(
                    "Please select at least one transaction type (IN, OUT, or TRANSFER).",
                    "Transaction Type Validation"
                );
                return;
            }

            LoggingUtility.Log($"[TransactionSearchControl] Search criteria validated. DateRange: {criteria.DateFrom:MM/dd/yy} - {criteria.DateTo:MM/dd/yy}, Types: {criteria.TransactionType}");

            // Raise event with valid criteria
            SearchRequested?.Invoke(this, criteria);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                controlName: nameof(TransactionSearchControl));
        }
    }

    private void BtnReset_Click(object? sender, EventArgs e)
    {
        try
        {
            LoggingUtility.Log("[TransactionSearchControl] Reset button clicked.");
            ClearCriteria();
            ResetRequested?.Invoke(this, EventArgs.Empty);
            LoggingUtility.Log("[TransactionSearchControl] Search criteria reset successfully.");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                controlName: nameof(TransactionSearchControl));
        }
    }

    #endregion

    #region Quick Filter Handlers

    private void QuickFilterChanged(object? sender, EventArgs e)
    {
        try
        {
            if (sender is RadioButton rdo && rdo.Checked)
            {
                LoggingUtility.Log($"[TransactionSearchControl] Quick filter changed: {rdo.Text}");
                ApplyQuickFilter();
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                controlName: nameof(TransactionSearchControl));
        }
    }

    private void ApplyQuickFilter()
    {
        try
        {
            var now = DateTime.Now;

            if (TransactionSearchControl_RadioButton_Today.Checked)
            {
                // Today: 00:00 to 23:59
                TransactionSearchControl_DateTimePicker_DateFrom.Value = now.Date;
                TransactionSearchControl_DateTimePicker_DateTo.Value = now.Date.AddDays(1).AddSeconds(-1);
                LoggingUtility.Log("[TransactionSearchControl] Quick filter applied: Today");
            }
            else if (TransactionSearchControl_RadioButton_Week.Checked)
            {
                // This Week: Monday to Sunday
                int daysFromMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                DateTime monday = now.Date.AddDays(-daysFromMonday);
                DateTime sunday = monday.AddDays(6);

                TransactionSearchControl_DateTimePicker_DateFrom.Value = monday;
                TransactionSearchControl_DateTimePicker_DateTo.Value = sunday.AddDays(1).AddSeconds(-1);
                LoggingUtility.Log("[TransactionSearchControl] Quick filter applied: Week");
            }
            else if (TransactionSearchControl_RadioButton_Month.Checked)
            {
                // This Month: 1st to last day
                DateTime firstDay = new DateTime(now.Year, now.Month, 1);
                DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

                TransactionSearchControl_DateTimePicker_DateFrom.Value = firstDay;
                TransactionSearchControl_DateTimePicker_DateTo.Value = lastDay.AddDays(1).AddSeconds(-1);
                LoggingUtility.Log("[TransactionSearchControl] Quick filter applied: Month");
            }
            // Custom: user sets dates manually, no automatic adjustment
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                controlName: nameof(TransactionSearchControl));
        }
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

