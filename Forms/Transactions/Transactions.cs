using MTM_WIP_Application_Winforms.Controls.Transactions;
using MTM_WIP_Application_Winforms.Core;
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

    #endregion

    #region Constructors

    public Transactions(string connectionString, string currentUser)
    {
        InitializeComponent();

        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);

        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _isAdmin = Model_AppVariables.UserTypeAdmin;
        _viewModel = new TransactionViewModel();

        WireUpEvents();
        _ = InitializeAsync();
    }

    #endregion

    #region Initialization

    private void WireUpEvents()
    {
        searchControl.SearchRequested += SearchControl_SearchRequested;
        searchControl.ResetRequested += SearchControl_ResetRequested;
        gridControl.PageChanged += GridControl_PageChanged;
        gridControl.RowSelected += GridControl_RowSelected;
        this.Load += Transactions_Load;
    }

    private async Task InitializeAsync()
    {
        try
        {
            var partsTask = _viewModel.LoadPartsAsync();
            var usersTask = _viewModel.LoadUsersAsync(_currentUser, _isAdmin);

            await Task.WhenAll(partsTask, usersTask).ConfigureAwait(false);

            this.Invoke(() =>
            {
                if (partsTask.Result.IsSuccess)
                {
                    searchControl.LoadParts(partsTask.Result.Data ?? new List<string>());
                }

                if (usersTask.Result.IsSuccess)
                {
                    searchControl.LoadUsers(usersTask.Result.Data ?? new List<string>());
                }
            });
        }
        catch (Exception ex)
        {
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, retryAction: null,
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

    private async void SearchControl_SearchRequested(object? sender, TransactionSearchCriteria criteria)
    {
        try
        {
            var result = await _viewModel.SearchTransactionsAsync(criteria, _currentUser, _isAdmin, page: 1)
                .ConfigureAwait(false);

            this.Invoke(() =>
            {
                if (result.IsSuccess && result.Data != null)
                {
                    gridControl.DisplayResults(result.Data);
                }
                else
                {
                    Service_ErrorHandler.HandleValidationError(result.ErrorMessage ?? "Search failed", "Search");
                }
            });
        }
        catch (Exception ex)
        {
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> { ["Criteria"] = criteria.ToString() },
                controlName: nameof(searchControl));
        }
    }

    private void SearchControl_ResetRequested(object? sender, EventArgs e)
    {
        gridControl.ClearResults();
    }

    private async void GridControl_PageChanged(object? sender, int newPage)
    {
        try
        {
            if (_viewModel.CurrentCriteria == null) return;

            var result = await _viewModel.SearchTransactionsAsync(_viewModel.CurrentCriteria, _currentUser, _isAdmin, page: newPage)
                .ConfigureAwait(false);

            this.Invoke(() =>
            {
                if (result.IsSuccess && result.Data != null)
                {
                    gridControl.DisplayResults(result.Data);
                }
                else
                {
                    Service_ErrorHandler.HandleValidationError(result.ErrorMessage ?? "Failed to load page", "Pagination");
                }
            });
        }
        catch (Exception ex)
        {
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, retryAction: null,
                contextData: new Dictionary<string, object> { ["Page"] = newPage },
                controlName: nameof(gridControl));
        }
    }

    private void GridControl_RowSelected(object? sender, Model_Transactions transaction)
    {
        if (transaction != null)
        {
            this.Text = $"Transaction Viewer - Selected: {transaction.TransactionType} #{transaction.ID}";
        }
    }

    #endregion
}
