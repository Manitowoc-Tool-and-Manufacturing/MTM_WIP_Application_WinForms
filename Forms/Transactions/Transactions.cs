using System.Text;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace MTM_WIP_Application_Winforms.Forms.Transactions;

/// <summary>
/// Transaction viewer form using modular UserControl architecture.
/// Refactored from 2136-line monolithic implementation to clean separation of concerns.
/// Migrated to ThemedForm for automatic DPI scaling and theme support.
/// </summary>
internal partial class Transactions : ThemedForm
{
    #region Fields

    private readonly Model_Transactions_ViewModel _viewModel;
    private readonly string _currentUser;
    private readonly bool _isAdmin;
    private Model_Transactions_SearchResult? _currentSearchResults;
    private readonly IShortcutService? _shortcutService;

    #endregion

    #region Constructors

    public Transactions(string connectionString, string currentUser)
    {
        InitializeComponent();

        
        // DPI scaling and layout now handled by ThemedForm.OnLoad
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _isAdmin = Model_Application_Variables.UserTypeDeveloper || Model_Application_Variables.UserTypeAdmin;
        _viewModel = new Model_Transactions_ViewModel();

        // Resolve shortcut service
        _shortcutService = Program.ServiceProvider?.GetService<IShortcutService>();
        if (_shortcutService != null && !string.IsNullOrEmpty(Model_Application_Variables.User))
        {
            // Initialize asynchronously
            _ = _shortcutService.InitializeAsync(Model_Application_Variables.User);
        }

        Transactions_UserControl_Search.SetExportPrintButtonsEnabled(false);
        Transactions_UserControl_Search.SetInformationPanelCollapsed(!Transactions_UserControl_Grid.InformationPanelVisible);

        WireUpEvents();
        
        
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
        Transactions_UserControl_Search.ToggleInformationPanelRequested += SearchControl_ToggleInformationPanelRequested;
        Transactions_UserControl_Grid.PageChanged += GridControl_PageChanged;
        Transactions_UserControl_Grid.RowSelected += GridControl_RowSelected;
        Transactions_UserControl_Grid.ToggleSearchRequested += GridControl_ToggleSearchRequested;
        Transactions_UserControl_Grid.ExportRequested += SearchControl_ExportRequested;
        Transactions_UserControl_Grid.PrintRequested += SearchControl_PrintRequested;
        Transactions_UserControl_Grid.AnalyticsRequested += GridControl_AnalyticsRequested;
        Transactions_UserControl_Grid.InformationPanelToggled += GridControl_InformationPanelToggled;
        Transactions_UserControl_Grid.DataGridView.KeyDown += DataGridView_KeyDown;
        this.Load += Transactions_Load;
    }

    private async Task InitializeAsync()
    {
        try
        {
            

            var partsTask = _viewModel.LoadPartsAsync();
            var usersTask = _viewModel.LoadUsersAsync(_currentUser, _isAdmin);
            var locationsTask = _viewModel.LoadLocationsAsync();
            var operationsTask = _viewModel.LoadOperationsAsync();

            await Task.WhenAll(partsTask, usersTask, locationsTask, operationsTask).ConfigureAwait(false);

            LoggingUtility.Log($"[Transactions] Data loaded - Parts: {(partsTask.Result.IsSuccess ? partsTask.Result.Data?.Count ?? 0 : 0)}, " +
                             $"Users: {(usersTask.Result.IsSuccess ? usersTask.Result.Data?.Count ?? 0 : 0)}, " +
                             $"Locations: {(locationsTask.Result.IsSuccess ? locationsTask.Result.Data?.Count ?? 0 : 0)}, " +
                             $"Operations: {(operationsTask.Result.IsSuccess ? operationsTask.Result.Data?.Count ?? 0 : 0)}");

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

                if (operationsTask.Result.IsSuccess)
                {
                    Transactions_UserControl_Search.LoadOperations(operationsTask.Result.Data ?? new List<string>());
                }

                
            });
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> { ["User"] = _currentUser },
                controlName: nameof(Transactions));
        }
    }

    #endregion

    #region Event Handlers

    private void Transactions_Load(object? sender, EventArgs e)
    {
        this.Text = $"Transaction Viewer - {_currentUser}" + (_isAdmin ? " (Admin)" : "");
        
    }

    private async void SearchControl_SearchRequested(object? sender, Model_Transactions_SearchCriteria criteria)
    {
        try
        {
            

            // Clear previous results before new search
            Transactions_UserControl_Grid.ClearResults();
            Transactions_UserControl_Search.SetExportPrintButtonsEnabled(false);

            var result = await _viewModel.SearchTransactionsAsync(criteria, _currentUser, _isAdmin, page: 1)
                .ConfigureAwait(false);

            

            this.Invoke(() =>
            {
                if (result.IsSuccess && result.Data != null)
                {
                    
                    _currentSearchResults = result.Data; // Store for analytics
                    Transactions_UserControl_Grid.DisplayResults(result.Data);

                    bool hasData = result.Data.Transactions != null && result.Data.Transactions.Count > 0;
                    Transactions_UserControl_Search.SetExportPrintButtonsEnabled(hasData);

                    if (Model_Application_Variables.AutoExpandPanels)
                    {
                        Transactions_Panel_Search.Visible = false;
                    }
                }
                else
                {
                    
                    Service_ErrorHandler.HandleValidationError(result.ErrorMessage ?? "Search failed", "Search");
                    Transactions_UserControl_Search.SetExportPrintButtonsEnabled(false);
                }
            });
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> { ["Criteria"] = criteria.ToString() },
                controlName: nameof(Transactions_UserControl_Search));

            if (!IsDisposed)
            {
                this.Invoke(() => Transactions_UserControl_Search.SetExportPrintButtonsEnabled(false));
            }
        }
    }

    private void SearchControl_ResetRequested(object? sender, EventArgs e)
    {
        
        Transactions_UserControl_Grid.ClearResults();
        Transactions_UserControl_Search.SetExportPrintButtonsEnabled(false);

        if (Model_Application_Variables.AutoExpandPanels)
        {
            Transactions_Panel_Search.Visible = true;
        }
    }

    private async void SearchControl_ExportRequested(object? sender, EventArgs e)
    {
        try
        {
            

            // Check if there are results to export
            if (_viewModel.CurrentResults == null || _viewModel.CurrentResults.Transactions == null || _viewModel.CurrentResults.Transactions.Count == 0)
            {
                
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
                    
                    Service_ErrorHandler.HandleValidationError("Invalid file path selected.", "Export");
                    return;
                }

                // Validate directory exists
                string? directory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
                {
                    
                    Service_ErrorHandler.HandleValidationError("Selected directory does not exist.", "Export");
                    return;
                }

                

                // Call ViewModel export method
                var exportResult = await _viewModel.ExportToExcelAsync(filePath, null).ConfigureAwait(false);

                this.Invoke(() =>
                {
                    if (exportResult.IsSuccess)
                    {
                        
                        Service_ErrorHandler.ShowConfirmation($"Transactions exported successfully!\n\nFile: {Path.GetFileName(filePath)}\nLocation: {directory}", "Export Complete");
                    }
                    else
                    {
                        
                        Service_ErrorHandler.HandleValidationError(exportResult.ErrorMessage ?? "Export failed", "Export");
                    }
                });
            }
            else
            {
                
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> 
                { 
                    ["User"] = _currentUser,
                    ["TransactionCount"] = _viewModel.CurrentResults?.Transactions?.Count ?? 0
                },
                controlName: nameof(Transactions));
        }
    }

    private void SearchControl_PrintRequested(object? sender, EventArgs e)
    {
        try
        {
            if (_viewModel.CurrentResults == null || _viewModel.CurrentResults.Transactions == null || _viewModel.CurrentResults.Transactions.Count == 0)
            {

                Service_ErrorHandler.HandleValidationError("No transactions to print. Please perform a search first.", "Print");
                return;
            }

            // Show print dialog for the grid and await result
            var dialogResultTask = Helper_PrintManager.ShowPrintDialogAsync(this, Transactions_UserControl_Grid.DataGridView, "Transaction History");
            dialogResultTask.ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {

                }
                else if (t.IsFaulted && t.Exception != null)
                {
                    LoggingUtility.LogApplicationError(t.Exception.GetBaseException() ?? t.Exception);
                }
            });
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, retryAction: null,
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
                
                return;
            }

            

            var result = await _viewModel.SearchTransactionsAsync(_viewModel.CurrentCriteria, _currentUser, _isAdmin, page: newPage)
                .ConfigureAwait(false);

            this.Invoke(() =>
            {
                if (result.IsSuccess && result.Data != null)
                {
                    
                    _currentSearchResults = result.Data; // Store for analytics
                    Transactions_UserControl_Grid.DisplayResults(result.Data);

                    bool hasData = result.Data.Transactions != null && result.Data.Transactions.Count > 0;
                    Transactions_UserControl_Search.SetExportPrintButtonsEnabled(hasData);
                }
                else
                {
                    
                    Service_ErrorHandler.HandleValidationError(result.ErrorMessage ?? "Failed to load page", "Pagination");
                    Transactions_UserControl_Search.SetExportPrintButtonsEnabled(false);
                }
            });
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> { ["Page"] = newPage },
                controlName: nameof(Transactions_UserControl_Grid));
        }
    }

    private void GridControl_RowSelected(object? sender, Model_Transactions_Core transaction)
    {
        if (transaction != null)
        {
            
            this.Text = $"Transaction Viewer - Selected: {transaction.TransactionType} #{transaction.ID}";
        }
    }

    private void SearchControl_ToggleInformationPanelRequested(object? sender, EventArgs e)
    {
        try
        {
            
            Transactions_UserControl_Grid.ToggleInformationPanel();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(Transactions_UserControl_Grid));
        }
    }

    private void GridControl_InformationPanelToggled(object? sender, bool isVisible)
    {
        Transactions_UserControl_Search.SetInformationPanelCollapsed(!isVisible);
    }

    private void GridControl_ToggleSearchRequested(object? sender, EventArgs e)
    {
        Transactions_Panel_Search.Visible = !Transactions_Panel_Search.Visible;
        
    }

    private async void GridControl_AnalyticsRequested(object? sender, EventArgs e)
    {
        try
        {
            // Check if analytics panel is now visible
            if (!Transactions_UserControl_Grid.AnalyticsControl.Visible)
            {
                
                return;
            }

            

            // Use a wide date range to capture all transactions
            // TODO: Store search criteria to use actual date range from search filters
            DateTime fromDate = DateTime.Now.AddYears(-10); // Default: 10 years ago
            DateTime toDate = DateTime.Now.AddDays(1); // Default: tomorrow

            // Load analytics data from database
            var analyticsResult = await _viewModel.GetAnalyticsAsync(fromDate, toDate).ConfigureAwait(false);

            if (!analyticsResult.IsSuccess || analyticsResult.Data == null)
            {
                
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
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["User"] = _currentUser },
                controlName: nameof(Transactions));
        }
    }

    private async void DataGridView_KeyDown(object? sender, KeyEventArgs e)
    {
        // Only allow delete for Admin/Developer users
        if (!_isAdmin)
        {
            return;
        }

        // Check if Delete key was pressed
        Keys deleteKey = _shortcutService?.GetShortcutKey("Shortcut_Transactions_Delete") ?? Keys.Delete;
        if (e.KeyCode != deleteKey && e.KeyCode != Keys.Delete) // Support both configured and default
        {
            return;
        }

        try
        {
            var dgv = Transactions_UserControl_Grid.DataGridView;
            
            if (dgv.SelectedRows.Count == 0)
            {
                
                return;
            }

            // Get selected transaction(s)
            List<(int ID, string Display, string? BatchNumber)> selectedTransactions = new();
            
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                if (row.DataBoundItem is Model_Transactions_Core transaction)
                {
                    string display = $"ID: {transaction.ID} - {transaction.TransactionType} - Part: {transaction.PartID} - Qty: {transaction.Quantity}";
                    selectedTransactions.Add((transaction.ID, display, transaction.BatchNumber));
                }
            }

            if (selectedTransactions.Count == 0)
            {
                
                return;
            }

            // Build confirmation message
            StringBuilder confirmMessage = new();
            confirmMessage.AppendLine("WARNING: You are about to permanently delete the following transaction(s):");
            confirmMessage.AppendLine();
            
            foreach (var (ID, Display, _) in selectedTransactions)
            {
                confirmMessage.AppendLine($"  • {Display}");
            }
            
            confirmMessage.AppendLine();
            confirmMessage.AppendLine("This action CANNOT be undone!");
            confirmMessage.AppendLine();
            confirmMessage.AppendLine("Are you sure you want to delete these transaction record(s)?");

            // Show confirmation dialog
            var confirmResult = Service_ErrorHandler.ShowConfirmation(
                confirmMessage.ToString(),
                "Confirm Delete Transaction(s)",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult != DialogResult.Yes)
            {
                
                return;
            }

            // Delete transactions
            
            
            int successCount = 0;
            int failureCount = 0;
            List<string> errors = new();
            HashSet<int> deletedIds = new();

            var daoTransactions = new Dao_Transactions();

            foreach (var (ID, Display, _) in selectedTransactions)
            {
                var deleteResult = await daoTransactions.DeleteTransactionByIdAsync(ID);
                
                if (deleteResult.IsSuccess)
                {
                    successCount++;
                    deletedIds.Add(ID);
                }
                else
                {
                    failureCount++;
                    string errorMsg = $"ID {ID}: {deleteResult.ErrorMessage}";
                    errors.Add(errorMsg);
                    
                }
            }

            // --- BATCH DELETION CHECK ---
            // After initial deletion, check if we should delete related batch items
            if (successCount > 0)
            {
                // Get unique batch numbers from successfully deleted items
                var batchNumbers = selectedTransactions
                    .Where(x => deletedIds.Contains(x.ID) && !string.IsNullOrWhiteSpace(x.BatchNumber))
                    .Select(x => x.BatchNumber!)
                    .Distinct()
                    .ToList();

                foreach (var batchNumber in batchNumbers)
                {
                    // Find other transactions with this batch number
                    var batchResult = await _viewModel.GetBatchLifecycleAsync(batchNumber);
                    
                    if (batchResult.IsSuccess && batchResult.Data != null)
                    {
                        // Filter out items that were already deleted in the initial selection
                        var relatedItems = batchResult.Data
                            .Where(t => !deletedIds.Contains(t.ID))
                            .ToList();

                        if (relatedItems.Count > 0)
                        {
                            // Prompt user to delete related items
                            StringBuilder batchMsg = new();
                            batchMsg.AppendLine($"Transaction(s) deleted. Found {relatedItems.Count} other transaction(s) with Batch Number '{batchNumber}'.");
                            batchMsg.AppendLine();
                            batchMsg.AppendLine("Do you want to delete them as well?");
                            batchMsg.AppendLine();
                            batchMsg.AppendLine("Affected rows:");
                            
                            // Limit list size in message box
                            int displayLimit = 10;
                            foreach (var item in relatedItems.Take(displayLimit))
                            {
                                batchMsg.AppendLine($"  • ID: {item.ID} - {item.TransactionType} - Part: {item.PartID} - Qty: {item.Quantity}");
                            }
                            
                            if (relatedItems.Count > displayLimit)
                            {
                                batchMsg.AppendLine($"  • ... and {relatedItems.Count - displayLimit} more.");
                            }

                            var batchConfirm = Service_ErrorHandler.ShowConfirmation(
                                batchMsg.ToString(),
                                "Delete Related Batch Items?",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (batchConfirm == DialogResult.Yes)
                            {
                                foreach (var item in relatedItems)
                                {
                                    var batchDeleteResult = await daoTransactions.DeleteTransactionByIdAsync(item.ID);
                                    if (batchDeleteResult.IsSuccess)
                                    {
                                        successCount++;
                                        deletedIds.Add(item.ID);
                                    }
                                    else
                                    {
                                        failureCount++;
                                        errors.Add($"ID {item.ID} (Batch {batchNumber}): {batchDeleteResult.ErrorMessage}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // ----------------------------

            // Build result message
            StringBuilder resultMessage = new();
            
            if (successCount > 0)
            {
                resultMessage.AppendLine($"Successfully deleted {successCount} transaction(s).");
            }
            
            if (failureCount > 0)
            {
                resultMessage.AppendLine();
                resultMessage.AppendLine($"Failed to delete {failureCount} transaction(s):");
                foreach (var error in errors)
                {
                    resultMessage.AppendLine($"  • {error}");
                }
            }

            // Show result
            if (failureCount > 0)
            {
                Service_ErrorHandler.ShowWarning(resultMessage.ToString(), "Delete Results");
            }
            else
            {
                Service_ErrorHandler.ShowInformation(resultMessage.ToString(), "Delete Successful");
            }

            // Refresh the grid if any deletions were successful
            if (successCount > 0 && _viewModel.CurrentCriteria != null)
            {
                
                
                var refreshResult = await _viewModel.SearchTransactionsAsync(
                    _viewModel.CurrentCriteria,
                    _currentUser,
                    _isAdmin,
                    _viewModel.CurrentResults?.CurrentPage ?? 1
                ).ConfigureAwait(false);

                this.Invoke(() =>
                {
                    if (refreshResult.IsSuccess && refreshResult.Data != null)
                    {
                        Transactions_UserControl_Grid.DisplayResults(refreshResult.Data);
                    }
                });
            }

            // Suppress the key press to prevent beep
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object>
                {
                    ["User"] = _currentUser,
                    ["IsAdmin"] = _isAdmin
                },
                controlName: nameof(Transactions));
        }
    }

    #endregion
}
