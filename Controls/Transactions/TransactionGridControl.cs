using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.Transactions;

/// <summary>
/// UserControl for displaying transaction search results in a paginated DataGridView.
/// </summary>
/// <remarks>
/// This control handles the grid display, pagination controls, and row selection.
/// It raises events for page changes and row selection to allow parent forms to react.
/// </remarks>
internal partial class TransactionGridControl : UserControl
{
    #region Fields

    private TransactionSearchResult? _currentResults;
    private int _currentPage = 1;

    #endregion

    #region Events

    /// <summary>
    /// Raised when the user navigates to a different page.
    /// </summary>
    public event EventHandler<int>? PageChanged;

    /// <summary>
    /// Raised when the user selects a transaction row.
    /// </summary>
    public event EventHandler<Model_Transactions>? RowSelected;

    /// <summary>
    /// Raised when the user clicks the Show/Hide Search button.
    /// </summary>
    public event EventHandler? ToggleSearchRequested;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the currently selected transaction, or null if no selection.
    /// </summary>
    public Model_Transactions? SelectedTransaction
    {
        get
        {
            if (TransactionGridControl_DataGridView_Transactions.SelectedRows.Count > 0)
            {
                return TransactionGridControl_DataGridView_Transactions.SelectedRows[0].DataBoundItem as Model_Transactions;
            }
            return null;
        }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionGridControl"/> class.
    /// </summary>
    public TransactionGridControl()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);

        InitializeColumns();
        WireUpEvents();
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Configures DataGridView columns programmatically.
    /// </summary>
    private void InitializeColumns()
    {
        TransactionGridControl_DataGridView_Transactions.AutoGenerateColumns = false;
        TransactionGridControl_DataGridView_Transactions.Columns.Clear();

        // ID Column - 80px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colID",
            HeaderText = "ID",
            DataPropertyName = "ID",
            Width = 80,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        // Type Column - 100px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colType",
            HeaderText = "Type",
            DataPropertyName = "TransactionType",
            Width = 100,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        });

        // Part Number Column - 150px, Fill
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPartNumber",
            HeaderText = "Part Number",
            DataPropertyName = "PartID",
            Width = 150,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            MinimumWidth = 100
        });

        // Quantity Column - 80px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colQuantity",
            HeaderText = "Qty",
            DataPropertyName = "Quantity",
            Width = 80,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        // From Location Column - 120px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colFromLocation",
            HeaderText = "From",
            DataPropertyName = "FromLocation",
            Width = 120,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        });

        // To Location Column - 120px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colToLocation",
            HeaderText = "To",
            DataPropertyName = "ToLocation",
            Width = 120,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        });

        // User Column - 100px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colUser",
            HeaderText = "User",
            DataPropertyName = "User",
            Width = 100,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        });

        // Date/Time Column - 140px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDateTime",
            HeaderText = "Date/Time",
            DataPropertyName = "DateTime",
            Width = 140,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "MM/dd/yy HH:mm" }
        });

        // Enable sorting on all columns
        foreach (DataGridViewColumn column in TransactionGridControl_DataGridView_Transactions.Columns)
        {
            column.SortMode = DataGridViewColumnSortMode.Automatic;
        }

        // Set alternating row colors for better readability
        TransactionGridControl_DataGridView_Transactions.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
    }

    /// <summary>
    /// Wires up event handlers for pagination and selection.
    /// </summary>
    private void WireUpEvents()
    {
        TransactionGridControl_Button_Previous.Click += BtnPrevious_Click;
        TransactionGridControl_Button_Next.Click += BtnNext_Click;
        TransactionGridControl_Button_GoToPage.Click += BtnGoToPage_Click;
        TransactionGridControl_TextBox_GoToPage.KeyPress += TxtGoToPage_KeyPress;
        TransactionGridControl_DataGridView_Transactions.SelectionChanged += DgvTransactions_SelectionChanged;
        TransactionGridControl_Button_ShowHideSearch.Click += BtnShowHideSearch_Click;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Displays transaction search results and updates pagination controls.
    /// </summary>
    /// <param name="results">The search results with pagination metadata.</param>
    public void DisplayResults(TransactionSearchResult results)
    {
        if (results == null)
        {
            throw new ArgumentNullException(nameof(results));
        }

        _currentResults = results;
        _currentPage = results.CurrentPage;

        // Suspend layout to prevent flickering
        TransactionGridControl_DataGridView_Transactions.SuspendLayout();

        try
        {
            // Bind data
            TransactionGridControl_DataGridView_Transactions.DataSource = new BindingSource { DataSource = results.Transactions };

            // Update pagination controls
            UpdatePaginationControls();
        }
        finally
        {
            TransactionGridControl_DataGridView_Transactions.ResumeLayout();
        }
    }

    /// <summary>
    /// Clears the grid and resets pagination controls.
    /// </summary>
    public void ClearResults()
    {
        _currentResults = null;
        _currentPage = 1;
        TransactionGridControl_DataGridView_Transactions.DataSource = null;
        UpdatePaginationControls();
    }

    #endregion

    #region Button Clicks

    private void BtnPrevious_Click(object? sender, EventArgs e)
    {
        if (_currentResults != null && _currentResults.HasPreviousPage)
        {
            PageChanged?.Invoke(this, _currentPage - 1);
        }
    }

    private void BtnNext_Click(object? sender, EventArgs e)
    {
        if (_currentResults != null && _currentResults.HasNextPage)
        {
            PageChanged?.Invoke(this, _currentPage + 1);
        }
    }

    private void BtnGoToPage_Click(object? sender, EventArgs e)
    {
        GoToPageFromTextBox();
    }

    private void BtnShowHideSearch_Click(object? sender, EventArgs e)
    {
        ToggleSearchRequested?.Invoke(this, EventArgs.Empty);
    }

    private void TxtGoToPage_KeyPress(object? sender, KeyPressEventArgs e)
    {
        // Allow only digits, backspace, and Enter
        if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
        {
            e.Handled = true;
            return;
        }

        // Handle Enter key
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            GoToPageFromTextBox();
        }
    }

    #endregion

    #region UI Events

    private void DgvTransactions_SelectionChanged(object? sender, EventArgs e)
    {
        var selectedTransaction = SelectedTransaction;
        if (selectedTransaction != null)
        {
            RowSelected?.Invoke(this, selectedTransaction);
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Updates the pagination controls based on current results.
    /// </summary>
    private void UpdatePaginationControls()
    {
        if (_currentResults == null)
        {
            TransactionGridControl_Button_Previous.Enabled = false;
            TransactionGridControl_Button_Next.Enabled = false;
            TransactionGridControl_Label_PageIndicator.Text = "Page 0 of 0";
            TransactionGridControl_Label_RecordCount.Text = "0 records";
            TransactionGridControl_TextBox_GoToPage.Enabled = false;
            TransactionGridControl_Button_GoToPage.Enabled = false;
            return;
        }

        // Enable/disable navigation buttons
        TransactionGridControl_Button_Previous.Enabled = _currentResults.HasPreviousPage;
        TransactionGridControl_Button_Next.Enabled = _currentResults.HasNextPage;

        // Update labels
        TransactionGridControl_Label_PageIndicator.Text = $"Page {_currentResults.CurrentPage} of {_currentResults.TotalPages}";
        TransactionGridControl_Label_RecordCount.Text = $"{_currentResults.TotalRecordCount} records found";

        // Enable page jump controls
        TransactionGridControl_TextBox_GoToPage.Enabled = _currentResults.TotalPages > 1;
        TransactionGridControl_Button_GoToPage.Enabled = _currentResults.TotalPages > 1;
    }

    /// <summary>
    /// Navigates to the page number entered in the text box.
    /// </summary>
    private void GoToPageFromTextBox()
    {
        if (_currentResults == null || string.IsNullOrWhiteSpace(TransactionGridControl_TextBox_GoToPage.Text))
        {
            return;
        }

        if (int.TryParse(TransactionGridControl_TextBox_GoToPage.Text, out int pageNumber))
        {
            if (pageNumber >= 1 && pageNumber <= _currentResults.TotalPages)
            {
                PageChanged?.Invoke(this, pageNumber);
                TransactionGridControl_TextBox_GoToPage.Text = string.Empty; // Clear after successful navigation
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Page number must be between 1 and {_currentResults.TotalPages}.",
                    "Invalid Page",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }

    #endregion
}

