using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.Transactions;

/// <summary>
/// UserControl for transaction search criteria input.
/// Migrated to ThemedUserControl for automatic DPI scaling and theme support.
/// </summary>
/// <remarks>
/// Provides UI for filtering transactions by part number, user, locations,
/// operation, transaction type, date range, and notes. Supports quick date filters
/// (Today/Week/Month/Custom) and raises SearchRequested event when user initiates search.
/// </remarks>
internal partial class TransactionSearchControl : ThemedUserControl
{
    #region Events

    /// <summary>
    /// Raised when the user clicks the Search button with valid criteria.
    /// </summary>
    public event EventHandler<Model_Transactions_SearchCriteria>? SearchRequested;

    /// <summary>
    /// Raised when the user clicks the Reset button.
    /// </summary>
    public event EventHandler? ResetRequested;

    /// <summary>
    /// Raised when the user clicks the Export button to export search results.
    /// </summary>
    public event EventHandler? ExportRequested;

    /// <summary>
    /// Raised when the user clicks the Print button to print search results.
    /// </summary>
    public event EventHandler? PrintRequested;

    #endregion

    #region Fields

    private readonly List<string> _partOptions = new();
    private readonly List<string> _userOptions = new();
    private readonly List<string> _locationOptions = new();
    private readonly List<string> _operationOptions = new();

    private bool _partSuggestionsConfigured;
    private bool _userSuggestionsConfigured;
    private bool _fromLocationSuggestionsConfigured;
    private bool _toLocationSuggestionsConfigured;
    private bool _operationSuggestionsConfigured;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionSearchControl"/> class.
    /// </summary>
    public TransactionSearchControl()
    {
        InitializeComponent();

        LoggingUtility.Log("[TransactionSearchControl] Initializing...");

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
        TransactionSearchControl_Button_Export.Click += BtnExport_Click;
        TransactionSearchControl_Button_Print.Click += BtnPrint_Click;

        // Quick filter radio buttons
        TransactionSearchControl_RadioButton_Today.CheckedChanged += QuickFilterChanged;
        TransactionSearchControl_RadioButton_Week.CheckedChanged += QuickFilterChanged;
        TransactionSearchControl_RadioButton_Month.CheckedChanged += QuickFilterChanged;
        TransactionSearchControl_RadioButton_Custom.CheckedChanged += QuickFilterChanged;
        TransactionSearchControl_RadioButton_Everything.CheckedChanged += QuickFilterChanged;

        // Transaction type checkboxes - validate at least one is selected
        TransactionSearchControl_CheckBox_IN.CheckedChanged += TransactionTypeCheckBox_CheckedChanged;
        TransactionSearchControl_CheckBox_OUT.CheckedChanged += TransactionTypeCheckBox_CheckedChanged;
        TransactionSearchControl_CheckBox_TRANSFER.CheckedChanged += TransactionTypeCheckBox_CheckedChanged;
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
    /// Populates the Part Number suggestion box with available parts.
    /// </summary>
    /// <param name="parts">List of part numbers.</param>
    public void LoadParts(List<string> parts)
    {
        try
        {
            var incomingCount = parts?.Count ?? 0;
            LoggingUtility.Log($"[TransactionSearchControl] LoadParts called with {incomingCount} parts");

            _partOptions.Clear();

            if (parts != null && parts.Count > 0)
            {
                var uniqueParts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var part in parts)
                {
                    if (string.IsNullOrWhiteSpace(part))
                    {
                        continue;
                    }

                    var normalized = part.Trim().ToUpperInvariant();
                    if (uniqueParts.Add(normalized))
                    {
                        _partOptions.Add(normalized);
                    }
                }
            }

            if (!_partSuggestionsConfigured)
            {
                Helper_SuggestionTextBox.ConfigureForPartNumbers(TransactionSearchControl_Suggestion_PartNumber, GetPartSuggestionsAsync);
                _partSuggestionsConfigured = true;
            }

            TransactionSearchControl_Suggestion_PartNumber.Text = string.Empty;

            LoggingUtility.Log($"[TransactionSearchControl] Parts loaded: {_partOptions.Count} unique values");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionSearchControl] Exception in LoadParts: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);

            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["PartsCount"] = parts?.Count ?? 0,
                    ["Method"] = nameof(LoadParts)
                },
                controlName: nameof(TransactionSearchControl));
        }
    }

    /// <summary>
    /// Populates the User suggestion box with available users.
    /// Defaults to current user. If user is not Admin/Developer, control is disabled (restricted to own transactions).
    /// </summary>
    /// <param name="users">List of usernames.</param>
    public void LoadUsers(List<string> users)
    {
        try
        {
            LoggingUtility.Log($"[TransactionSearchControl] LoadUsers called with {users?.Count ?? 0} users");
            bool isAdminOrDeveloper = Model_Application_Variables.UserTypeAdmin || Model_Application_Variables.UserTypeDeveloper;
            string currentUser = Model_Application_Variables.User;

            _userOptions.Clear();

            if (users != null && users.Count > 0)
            {
                var uniqueUsers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var user in users)
                {
                    if (string.IsNullOrWhiteSpace(user))
                    {
                        continue;
                    }

                    var normalized = user.Trim();
                    if (uniqueUsers.Add(normalized))
                    {
                        _userOptions.Add(normalized);
                    }
                }
            }

            if (!_userSuggestionsConfigured)
            {
                Helper_SuggestionTextBox.ConfigureForUsers(TransactionSearchControl_Suggestion_User, GetUserSuggestionsAsync);
                _userSuggestionsConfigured = true;
            }

            TransactionSearchControl_Suggestion_User.Enabled = isAdminOrDeveloper;
            TransactionSearchControl_Suggestion_User.Text = isAdminOrDeveloper ? string.Empty : currentUser;

            LoggingUtility.Log($"[TransactionSearchControl] Users loaded: {_userOptions.Count} values, Enabled: {TransactionSearchControl_Suggestion_User.Enabled}");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionSearchControl] Exception in LoadUsers: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);

            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["UsersCount"] = users?.Count ?? 0,
                    ["Method"] = nameof(LoadUsers),
                    ["CurrentUser"] = Model_Application_Variables.User,
                    ["IsAdmin"] = Model_Application_Variables.UserTypeAdmin,
                    ["IsDeveloper"] = Model_Application_Variables.UserTypeDeveloper
                },
                controlName: nameof(TransactionSearchControl));
        }
    }

    /// <summary>
    /// Populates the location suggestion boxes with available locations.
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

            _locationOptions.Clear();
            var uniqueLocations = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var location in locations)
            {
                if (string.IsNullOrWhiteSpace(location))
                {
                    continue;
                }

                var normalized = location.Trim().ToUpperInvariant();
                if (uniqueLocations.Add(normalized))
                {
                    _locationOptions.Add(normalized);
                }
            }

            if (!_fromLocationSuggestionsConfigured)
            {
                Helper_SuggestionTextBox.ConfigureForLocations(TransactionSearchControl_Suggestion_FromLocation, GetLocationSuggestionsAsync);
                _fromLocationSuggestionsConfigured = true;
            }

            if (!_toLocationSuggestionsConfigured)
            {
                Helper_SuggestionTextBox.ConfigureForLocations(TransactionSearchControl_Suggestion_ToLocation, GetLocationSuggestionsAsync);
                _toLocationSuggestionsConfigured = true;
            }

            TransactionSearchControl_Suggestion_FromLocation.Text = string.Empty;
            TransactionSearchControl_Suggestion_ToLocation.Text = string.Empty;

            LoggingUtility.Log($"[TransactionSearchControl] Locations loaded: {_locationOptions.Count} unique codes");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionSearchControl] Exception in LoadLocations: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);

            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["LocationsCount"] = locations?.Count ?? 0,
                    ["Method"] = nameof(LoadLocations)
                },
                controlName: nameof(TransactionSearchControl));
        }
    }

    /// <summary>
    /// Populates the Operation suggestion box with available operations.
    /// </summary>
    /// <param name="operations">List of operation identifiers.</param>
    public void LoadOperations(List<string> operations)
    {
        try
        {
            LoggingUtility.Log($"[TransactionSearchControl] LoadOperations called with {operations?.Count ?? 0} operations");

            _operationOptions.Clear();

            if (operations != null && operations.Count > 0)
            {
                var uniqueOperations = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var operation in operations)
                {
                    if (string.IsNullOrWhiteSpace(operation))
                    {
                        continue;
                    }

                    var normalized = operation.Trim().ToUpperInvariant();
                    if (uniqueOperations.Add(normalized))
                    {
                        _operationOptions.Add(normalized);
                    }
                }
            }

            if (!_operationSuggestionsConfigured)
            {
                Helper_SuggestionTextBox.ConfigureForOperations(TransactionSearchControl_Suggestion_Operation, GetOperationSuggestionsAsync);
                _operationSuggestionsConfigured = true;
            }

            TransactionSearchControl_Suggestion_Operation.Text = string.Empty;

            LoggingUtility.Log($"[TransactionSearchControl] Operations loaded: {_operationOptions.Count} unique values");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[TransactionSearchControl] Exception in LoadOperations: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);

            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["OperationsCount"] = operations?.Count ?? 0,
                    ["Method"] = nameof(LoadOperations)
                },
                controlName: nameof(TransactionSearchControl));
        }
    }

    /// <summary>
    /// Clears all search criteria and resets to defaults.
    /// User combobox resets to current user.
    /// </summary>
    public void ClearCriteria()
    {
        TransactionSearchControl_Suggestion_PartNumber.Text = string.Empty;
        TransactionSearchControl_Suggestion_FromLocation.Text = string.Empty;
        TransactionSearchControl_Suggestion_ToLocation.Text = string.Empty;
        TransactionSearchControl_Suggestion_Operation.Text = string.Empty;
        TransactionSearchControl_Suggestion_Notes.Text = string.Empty;

        if (TransactionSearchControl_Suggestion_User.Enabled)
        {
            TransactionSearchControl_Suggestion_User.Text = string.Empty;
        }
        else
        {
            TransactionSearchControl_Suggestion_User.Text = Model_Application_Variables.User;
        }

        // Reset transaction type checkboxes to all checked
        TransactionSearchControl_CheckBox_IN.Checked = true;
        TransactionSearchControl_CheckBox_OUT.Checked = true;
        TransactionSearchControl_CheckBox_TRANSFER.Checked = true;

        // Reset date range to month
        TransactionSearchControl_RadioButton_Month.Checked = true;
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

            // Skip date range validation when "Everything" is selected
            if (!TransactionSearchControl_RadioButton_Everything.Checked)
            {
                // Validate date range for non-Everything searches
                if (!criteria.IsDateRangeValid())
                {
                    LoggingUtility.Log("[TransactionSearchControl] Search validation failed: Invalid date range.");
                    Service_ErrorHandler.HandleValidationError(
                        "Invalid date range. 'Date From' must be before or equal to 'Date To'.",
                        "Date Range Validation"
                    );
                    return;
                }
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

            if (TransactionSearchControl_RadioButton_Everything.Checked)
            {
                LoggingUtility.Log($"[TransactionSearchControl] Search criteria validated (Everything mode). Types: {criteria.TransactionType}");
            }
            else
            {
                LoggingUtility.Log($"[TransactionSearchControl] Search criteria validated. DateRange: {criteria.DateFrom:MM/dd/yy} - {criteria.DateTo:MM/dd/yy}, Types: {criteria.TransactionType}");
            }

            // Raise event with valid criteria
            SearchRequested?.Invoke(this, criteria);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
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
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionSearchControl));
        }
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        try
        {
            LoggingUtility.Log("[TransactionSearchControl] Export button clicked.");
            ExportRequested?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionSearchControl));
        }
    }

    private void BtnPrint_Click(object? sender, EventArgs e)
    {
        try
        {
            LoggingUtility.Log("[TransactionSearchControl] Print button clicked.");
            PrintRequested?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
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
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
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
                TransactionSearchControl_DateTimePicker_DateFrom.Enabled = false;
                TransactionSearchControl_DateTimePicker_DateTo.Enabled = false;
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
                TransactionSearchControl_DateTimePicker_DateFrom.Enabled = false;
                TransactionSearchControl_DateTimePicker_DateTo.Enabled = false;
                LoggingUtility.Log("[TransactionSearchControl] Quick filter applied: Week");
            }
            else if (TransactionSearchControl_RadioButton_Month.Checked)
            {
                // This Month: 1st to last day
                DateTime firstDay = new DateTime(now.Year, now.Month, 1);
                DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

                TransactionSearchControl_DateTimePicker_DateFrom.Value = firstDay;
                TransactionSearchControl_DateTimePicker_DateTo.Value = lastDay.AddDays(1).AddSeconds(-1);
                TransactionSearchControl_DateTimePicker_DateFrom.Enabled = false;
                TransactionSearchControl_DateTimePicker_DateTo.Enabled = false;
                LoggingUtility.Log("[TransactionSearchControl] Quick filter applied: Month");
            }
            else if (TransactionSearchControl_RadioButton_Everything.Checked)
            {
                // Everything: No date filtering - set to wide range (past 10 years to future 1 year)
                TransactionSearchControl_DateTimePicker_DateFrom.Value = now.AddYears(-10);
                TransactionSearchControl_DateTimePicker_DateTo.Value = now.AddYears(1);
                TransactionSearchControl_DateTimePicker_DateFrom.Enabled = false;
                TransactionSearchControl_DateTimePicker_DateTo.Enabled = false;
                LoggingUtility.Log("[TransactionSearchControl] Quick filter applied: Everything (all dates)");
            }
            else if (TransactionSearchControl_RadioButton_Custom.Checked)
            {
                // Custom: user sets dates manually, enable date pickers
                TransactionSearchControl_DateTimePicker_DateFrom.Enabled = true;
                TransactionSearchControl_DateTimePicker_DateTo.Enabled = true;
                LoggingUtility.Log("[TransactionSearchControl] Quick filter applied: Custom (user-defined dates)");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionSearchControl));
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Handles CheckedChanged event for transaction type checkboxes.
    /// Enables/disables Search button based on whether at least one type is selected.
    /// </summary>
    private void TransactionTypeCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        try
        {
            // Enable Search button only if at least one transaction type is selected
            bool anyTypeSelected = TransactionSearchControl_CheckBox_IN.Checked ||
                                   TransactionSearchControl_CheckBox_OUT.Checked ||
                                   TransactionSearchControl_CheckBox_TRANSFER.Checked;

            TransactionSearchControl_Button_Search.Enabled = anyTypeSelected;

            if (!anyTypeSelected)
            {
                LoggingUtility.Log("[TransactionSearchControl] Search button disabled - no transaction types selected");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    /// <summary>
    /// Builds a Model_Transactions_SearchCriteria object from current UI values.
    /// </summary>
    /// <returns>Populated search criteria object.</returns>
    private Model_Transactions_SearchCriteria BuildCriteria()
    {
        var criteria = new Model_Transactions_SearchCriteria
        {
            PartID = NormalizeInput(TransactionSearchControl_Suggestion_PartNumber.Text, toUpper: true),
            User = NormalizeInput(TransactionSearchControl_Suggestion_User.Text),
            FromLocation = NormalizeInput(TransactionSearchControl_Suggestion_FromLocation.Text, toUpper: true),
            ToLocation = NormalizeInput(TransactionSearchControl_Suggestion_ToLocation.Text, toUpper: true),
            Operation = NormalizeInput(TransactionSearchControl_Suggestion_Operation.Text, toUpper: true),
            Notes = NormalizeInput(TransactionSearchControl_Suggestion_Notes.Text),
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

    private Task<List<string>> GetPartSuggestionsAsync() => Task.FromResult(_partOptions.ToList());

    private Task<List<string>> GetUserSuggestionsAsync() => Task.FromResult(_userOptions.ToList());

    private Task<List<string>> GetLocationSuggestionsAsync() => Task.FromResult(_locationOptions.ToList());

    private Task<List<string>> GetOperationSuggestionsAsync() => Task.FromResult(_operationOptions.ToList());

    private static string? NormalizeInput(string? value, bool toUpper = false)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var trimmed = value.Trim();
        return toUpper ? trimmed.ToUpperInvariant() : trimmed;
    }

    #endregion
}

