using MTM_WIP_Application_Winforms.Controls.Transactions;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.Transactions;

/// <summary>
/// Transaction viewer form using modular UserControl architecture.
/// Refactored from 2136-line monolithic implementation to clean separation of concerns.
/// </summary>
internal partial class Transactions : Form
{
    #region Fields

    private readonly TransactionViewModel _viewModel;
    private readonly string _currentUser;
    private readonly bool _isAdmin;
    private TransactionSearchResult? _currentSearchResults;

    #endregion

    #region Constructors

    public Transactions(string connectionString, string currentUser)
    {
        InitializeComponent();

        LoggingUtility.Log("[Transactions] Form initializing...");

        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);

        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _isAdmin = Model_AppVariables.UserTypeDeveloper || Model_AppVariables.UserTypeAdmin;
        _viewModel = new TransactionViewModel();

        LoggingUtility.Log($"[Transactions] User: {_currentUser}, IsAdmin: {_isAdmin}");

        WireUpEvents();
        ApplyThemeColors();
        
        LoggingUtility.Log("[Transactions] Starting async initialization...");
        _ = InitializeAsync();
    }

    #endregion

    #region Initialization

    private void WireUpEvents()
    {
        Transactions_UserControl_Search.SearchRequested += SearchControl_SearchRequested;
        Transactions_UserControl_Search.ResetRequested += SearchControl_ResetRequested;
        Transactions_UserControl_Search.ExportRequested += SearchControl_ExportRequested;
        Transactions_UserControl_Search.PrintRequested += SearchControl_PrintRequested;
        Transactions_UserControl_Grid.PageChanged += GridControl_PageChanged;
        Transactions_UserControl_Grid.RowSelected += GridControl_RowSelected;
        Transactions_UserControl_Grid.ToggleSearchRequested += GridControl_ToggleSearchRequested;
        Transactions_UserControl_Grid.ExportRequested += SearchControl_ExportRequested;
        Transactions_UserControl_Grid.PrintRequested += SearchControl_PrintRequested;
        Transactions_UserControl_Grid.AnalyticsRequested += GridControl_AnalyticsRequested;
        this.Load += Transactions_Load;
    }

    private async Task InitializeAsync()
    {
        try
        {
            LoggingUtility.Log("[Transactions] Loading dropdown data (parts, users, locations)...");

            var partsTask = _viewModel.LoadPartsAsync();
            var usersTask = _viewModel.LoadUsersAsync(_currentUser, _isAdmin);
            var locationsTask = _viewModel.LoadLocationsAsync();

            await Task.WhenAll(partsTask, usersTask, locationsTask).ConfigureAwait(false);

            LoggingUtility.Log($"[Transactions] Data loaded - Parts: {(partsTask.Result.IsSuccess ? partsTask.Result.Data?.Count ?? 0 : 0)}, " +
                             $"Users: {(usersTask.Result.IsSuccess ? usersTask.Result.Data?.Count ?? 0 : 0)}, " +
                             $"Locations: {(locationsTask.Result.IsSuccess ? locationsTask.Result.Data?.Count ?? 0 : 0)}");

            this.Invoke(() =>
            {
                if (partsTask.Result.IsSuccess)
                {
                    Transactions_UserControl_Search.LoadParts(partsTask.Result.Data ?? new List<string>());
                }

                if (usersTask.Result.IsSuccess)
                {
                    Transactions_UserControl_Search.LoadUsers(usersTask.Result.Data ?? new List<string>());
                }

                if (locationsTask.Result.IsSuccess)
                {
                    Transactions_UserControl_Search.LoadLocations(locationsTask.Result.Data ?? new List<string>());
                }

                LoggingUtility.Log("[Transactions] Dropdown data loaded into search control.");
            });
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> { ["User"] = _currentUser },
                controlName: nameof(Transactions));
        }
    }

    #endregion

    #region Theme Application

    private void ApplyThemeColors()
    {
        try
        {
            LoggingUtility.Log("[Transactions] Applying theme colors...");
            
            var colors = Model_AppVariables.UserUiColors;
            if (colors == null)
            {
                LoggingUtility.Log("[Transactions] WARNING: UserUiColors is null, using SystemColors fallback");
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
                return;
            }

            // Apply theme to form
            this.BackColor = colors.FormBackColor ?? SystemColors.Control;
            this.ForeColor = colors.FormForeColor ?? SystemColors.ControlText;

            // Apply theme to panels
            Transactions_Panel_Search.BackColor = colors.PanelBackColor ?? SystemColors.ControlLight;
            Transactions_Panel_Search.ForeColor = colors.PanelForeColor ?? SystemColors.ControlText;

            Transactions_Panel_Grid.BackColor = colors.PanelBackColor ?? SystemColors.ControlLight;
            Transactions_Panel_Grid.ForeColor = colors.PanelForeColor ?? SystemColors.ControlText;

            LoggingUtility.Log($"[Transactions] Theme applied: {Model_AppVariables.ThemeName}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                controlName: nameof(Transactions));
        }
    }

    #endregion

    #region Event Handlers

    private void Transactions_Load(object? sender, EventArgs e)
    {
        this.Text = $"Transaction Viewer - {_currentUser}" + (_isAdmin ? " (Admin)" : "");
        LoggingUtility.Log($"[Transactions] Form loaded. Title: {this.Text}");
    }

    private async void SearchControl_SearchRequested(object? sender, TransactionSearchCriteria criteria)
    {
        try
        {
            LoggingUtility.Log($"[Transactions] Search requested with criteria: {criteria}");

            // Clear previous results before new search
            Transactions_UserControl_Grid.ClearResults();

            var result = await _viewModel.SearchTransactionsAsync(criteria, _currentUser, _isAdmin, page: 1)
                .ConfigureAwait(false);

            LoggingUtility.Log($"[Transactions] Search completed. Success: {result.IsSuccess}, HasData: {result.Data != null}");

            this.Invoke(() =>
            {
                if (result.IsSuccess && result.Data != null)
                {
                    LoggingUtility.Log($"[Transactions] Displaying {result.Data.Transactions.Count} transactions (Page {result.Data.CurrentPage} of {result.Data.TotalPages})");
                    _currentSearchResults = result.Data; // Store for analytics
                    Transactions_UserControl_Grid.DisplayResults(result.Data);
                }
                else
                {
                    LoggingUtility.Log($"[Transactions] Search failed or returned no data. Error: {result.ErrorMessage}");
                    Service_ErrorHandler.HandleValidationError(result.ErrorMessage ?? "Search failed", "Search");
                }
            });
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> { ["Criteria"] = criteria.ToString() },
                controlName: nameof(Transactions_UserControl_Search));
        }
    }

    private void SearchControl_ResetRequested(object? sender, EventArgs e)
    {
        LoggingUtility.Log("[Transactions] Reset requested, clearing grid results.");
        Transactions_UserControl_Grid.ClearResults();
    }

    private async void SearchControl_ExportRequested(object? sender, EventArgs e)
    {
        try
        {
            LoggingUtility.Log("[Transactions] Export requested.");

            // Check if there are results to export
            if (_viewModel.CurrentResults == null || _viewModel.CurrentResults.Transactions == null || _viewModel.CurrentResults.Transactions.Count == 0)
            {
                LoggingUtility.Log("[Transactions] Export aborted - no results to export.");
                Service_ErrorHandler.HandleValidationError("No transactions to export. Please perform a search first.", "Export");
                return;
            }

            // Generate default filename: Transactions_yyyyMMdd_Username.xlsx
            string defaultFileName = $"Transactions_{DateTime.Now:yyyyMMdd}_{_currentUser}.xlsx";
            
            // Show SaveFileDialog
            using var saveDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = defaultFileName,
                Title = "Export Transactions to Excel",
                DefaultExt = "xlsx",
                AddExtension = true,
                OverwritePrompt = true
            };

            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                string filePath = saveDialog.FileName;
                
                // Validate file path (security best practice)
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    LoggingUtility.Log("[Transactions] Export aborted - invalid file path.");
                    Service_ErrorHandler.HandleValidationError("Invalid file path selected.", "Export");
                    return;
                }

                // Validate directory exists
                string? directory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
                {
                    LoggingUtility.Log($"[Transactions] Export aborted - directory does not exist: {directory}");
                    Service_ErrorHandler.HandleValidationError("Selected directory does not exist.", "Export");
                    return;
                }

                LoggingUtility.Log($"[Transactions] Exporting {_viewModel.CurrentResults.Transactions.Count} transactions to: {filePath}");

                // Call ViewModel export method
                var exportResult = await _viewModel.ExportToExcelAsync(filePath, null).ConfigureAwait(false);

                this.Invoke(() =>
                {
                    if (exportResult.IsSuccess)
                    {
                        LoggingUtility.Log($"[Transactions] Export successful: {filePath}");
                        Service_ErrorHandler.ShowConfirmation($"Transactions exported successfully!\n\nFile: {Path.GetFileName(filePath)}\nLocation: {directory}", "Export Complete");
                    }
                    else
                    {
                        LoggingUtility.Log($"[Transactions] Export failed: {exportResult.ErrorMessage}");
                        Service_ErrorHandler.HandleValidationError(exportResult.ErrorMessage ?? "Export failed", "Export");
                    }
                });
            }
            else
            {
                LoggingUtility.Log("[Transactions] Export cancelled by user.");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> 
                { 
                    ["User"] = _currentUser,
                    ["TransactionCount"] = _viewModel.CurrentResults?.Transactions?.Count ?? 0
                },
                controlName: nameof(Transactions));
        }
    }

    private async void SearchControl_PrintRequested(object? sender, EventArgs e)
    {
        try
        {
            LoggingUtility.Log("[Transactions] Print requested.");

            // Check if there are results to print
            if (_viewModel.CurrentResults == null || _viewModel.CurrentResults.Transactions == null || _viewModel.CurrentResults.Transactions.Count == 0)
            {
                LoggingUtility.Log("[Transactions] Print aborted - no results to print.");
                Service_ErrorHandler.HandleValidationError("No transactions to print. Please perform a search first.", "Print");
                return;
            }

            LoggingUtility.Log($"[Transactions] Printing {_viewModel.CurrentResults.Transactions.Count} transactions.");

            // Call ViewModel print method
            await _viewModel.PrintPreviewAsync(_viewModel.CurrentResults.Transactions).ConfigureAwait(false);

            LoggingUtility.Log("[Transactions] Print dialog shown.");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low, retryAction: null,
                contextData: new Dictionary<string, object> 
                { 
                    ["User"] = _currentUser,
                    ["TransactionCount"] = _viewModel.CurrentResults?.Transactions?.Count ?? 0
                },
                controlName: nameof(Transactions));
        }
    }

    private async void GridControl_PageChanged(object? sender, int newPage)
    {
        try
        {
            if (_viewModel.CurrentCriteria == null)
            {
                LoggingUtility.Log("[Transactions] Page change requested but no current criteria available.");
                return;
            }

            LoggingUtility.Log($"[Transactions] Page change requested to page {newPage}.");

            var result = await _viewModel.SearchTransactionsAsync(_viewModel.CurrentCriteria, _currentUser, _isAdmin, page: newPage)
                .ConfigureAwait(false);

            this.Invoke(() =>
            {
                if (result.IsSuccess && result.Data != null)
                {
                    LoggingUtility.Log($"[Transactions] Page {newPage} loaded with {result.Data.Transactions.Count} transactions.");
                    _currentSearchResults = result.Data; // Store for analytics
                    Transactions_UserControl_Grid.DisplayResults(result.Data);
                }
                else
                {
                    LoggingUtility.Log($"[Transactions] Failed to load page {newPage}. Error: {result.ErrorMessage}");
                    Service_ErrorHandler.HandleValidationError(result.ErrorMessage ?? "Failed to load page", "Pagination");
                }
            });
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> { ["Page"] = newPage },
                controlName: nameof(Transactions_UserControl_Grid));
        }
    }

    private void GridControl_RowSelected(object? sender, Model_Transactions transaction)
    {
        if (transaction != null)
        {
            LoggingUtility.Log($"[Transactions] Row selected: Transaction #{transaction.ID} ({transaction.TransactionType})");
            this.Text = $"Transaction Viewer - Selected: {transaction.TransactionType} #{transaction.ID}";
        }
    }

    private void GridControl_ToggleSearchRequested(object? sender, EventArgs e)
    {
        Transactions_Panel_Search.Visible = !Transactions_Panel_Search.Visible;
        LoggingUtility.Log($"[Transactions] Search panel toggled. Now visible: {Transactions_Panel_Search.Visible}");
    }

    private async void GridControl_AnalyticsRequested(object? sender, EventArgs e)
    {
        try
        {
            // Check if analytics panel is now visible
            if (!Transactions_UserControl_Grid.AnalyticsControl.Visible)
            {
                LoggingUtility.Log("[Transactions] Analytics panel hidden, no data load needed.");
                return;
            }

            LoggingUtility.Log("[Transactions] Analytics panel shown, loading analytics data from database...");

            // Use a wide date range to capture all transactions
            // TODO: Store search criteria to use actual date range from search filters
            DateTime fromDate = DateTime.Now.AddYears(-10); // Default: 10 years ago
            DateTime toDate = DateTime.Now.AddDays(1); // Default: tomorrow

            // Load analytics data from database
            var analyticsResult = await _viewModel.GetAnalyticsAsync(fromDate, toDate).ConfigureAwait(false);

            if (!analyticsResult.IsSuccess || analyticsResult.Data == null)
            {
                LoggingUtility.Log($"[Transactions] Analytics load failed: {analyticsResult.ErrorMessage}");
                this.Invoke(() =>
                {
                    Service_ErrorHandler.HandleValidationError(
                        analyticsResult.ErrorMessage ?? "Failed to load analytics",
                        nameof(Transactions));
                    Transactions_UserControl_Grid.AnalyticsControl.ClearAnalytics();
                });
                return;
            }

            LoggingUtility.Log($"[Transactions] Analytics loaded: Total={analyticsResult.Data.TotalTransactions}, " +
                             $"IN={analyticsResult.Data.TotalIN}, OUT={analyticsResult.Data.TotalOUT}, " +
                             $"TRANSFER={analyticsResult.Data.TotalTRANSFER}");

            // Update the analytics control on the UI thread
            this.Invoke(() =>
            {
                Transactions_UserControl_Grid.AnalyticsControl.Analytics = analyticsResult.Data;
            });
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["User"] = _currentUser },
                controlName: nameof(Transactions));
        }
    }

    #endregion
}
