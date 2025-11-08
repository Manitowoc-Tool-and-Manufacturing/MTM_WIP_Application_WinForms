using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.Transactions;

/// <summary>
/// Modal dialog for displaying transaction lifecycle visualization.
/// Shows full batch timeline with TreeView hierarchy and transaction details.
/// </summary>
/// <remarks>
/// Displays transactions in chronological order with split visualization.
/// TreeView shows relationships between parent and child transactions.
/// Detail panel updates based on TreeView selection.
/// Icon legend at bottom: 📥 IN (Green), 🔄 TRANSFER (Blue), 📤 OUT (Red), 📦 Split (Orange)
/// </remarks>
internal partial class TransactionLifecycleForm : Form
{
    #region Fields

    private readonly string _partId;
    private readonly string _batchNumber;
    private readonly Dao_Transactions _daoTransactions;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionLifecycleForm"/> class.
    /// </summary>
    /// <param name="partId">The part ID for the transaction lifecycle.</param>
    /// <param name="batchNumber">The batch number to trace through the lifecycle.</param>
    public TransactionLifecycleForm(string partId, string batchNumber)
    {
        InitializeComponent();

        LoggingUtility.Log("[TransactionLifecycleForm] Initializing...");

        // MANDATORY theme system integration per Constitution Principle IX
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);

        _partId = partId ?? throw new ArgumentNullException(nameof(partId));
        _batchNumber = batchNumber ?? throw new ArgumentNullException(nameof(batchNumber));
        _daoTransactions = new Dao_Transactions();

        // Set form title with part and batch info
        this.Text = $"{_partId} - {_batchNumber} - Transaction Lifecycle";

        // Configure embedded detail panel to prevent recursive navigation
        TransactionLifecycleForm_DetailPanel.IsEmbeddedMode = true;

        WireUpEvents();
        ApplyThemeColors();
        
        // Load lifecycle data asynchronously after form is shown
        this.Load += async (s, e) => await LoadLifecycleAsync();

        LoggingUtility.Log($"[TransactionLifecycleForm] Initialized for Part: {_partId}, Batch: {_batchNumber}");
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Wires up event handlers for UI controls.
    /// </summary>
    private void WireUpEvents()
    {
        TransactionLifecycleForm_Button_Export.Click += BtnExport_Click;
        TransactionLifecycleForm_Button_Print.Click += BtnPrint_Click;
        TransactionLifecycleForm_Button_Close.Click += BtnClose_Click;
        TransactionLifecycleForm_TreeView_Lifecycle.AfterSelect += TreeView_AfterSelect;
    }

    /// <summary>
    /// Applies theme colors to form controls.
    /// </summary>
    private void ApplyThemeColors()
    {
        try
        {
            var colors = Model_Application_Variables.UserUiColors;

            // Apply theme tokens with SystemColors fallbacks
            this.BackColor = colors.PanelBackColor ?? SystemColors.Control;
            TransactionLifecycleForm_Panel_TreeView.BackColor = colors.PanelBackColor ?? SystemColors.ControlLight;
            TransactionLifecycleForm_Panel_DetailView.BackColor = colors.PanelBackColor ?? SystemColors.ControlLight;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            // Continue with default colors if theme application fails
        }
    }

    #endregion

    #region Lifecycle Loading

    /// <summary>
    /// Loads the batch lifecycle from the database and builds the TreeView visualization.
    /// </summary>
    private async Task LoadLifecycleAsync()
    {
        try
        {
            LoggingUtility.Log($"[TransactionLifecycleForm] Loading lifecycle for batch: {_batchNumber}");

            // Retrieve lifecycle data from database
            var result = await _daoTransactions.GetBatchLifecycleAsync(_batchNumber);

            if (!result.IsSuccess || result.Data == null)
            {
                Service_ErrorHandler.HandleValidationError(
                    result.ErrorMessage ?? "Failed to load batch lifecycle.",
                    "Lifecycle Loading"
                );
                return;
            }

            var transactions = result.Data;
            LoggingUtility.Log($"[TransactionLifecycleForm] Retrieved {transactions.Count} transactions");

            if (transactions.Count == 0)
            {
                Service_ErrorHandler.HandleValidationError(
                    $"No transactions found for batch {_batchNumber}.",
                    "Lifecycle Loading"
                );
                return;
            }

            // Build the TreeView from transactions
            BuildLifecycleTree(transactions);

            LoggingUtility.Log("[TransactionLifecycleForm] Lifecycle loaded successfully");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["PartID"] = _partId,
                    ["BatchNumber"] = _batchNumber
                },
                controlName: nameof(TransactionLifecycleForm));
        }
    }

    /// <summary>
    /// Builds the TreeView hierarchy from the list of transactions.
    /// Detects splits by comparing quantities and creates child branches.
    /// </summary>
    /// <param name="transactions">List of transactions in chronological order.</param>
    private void BuildLifecycleTree(List<Model_Transactions_Core> transactions)
    {
        try
        {
            TransactionLifecycleForm_TreeView_Lifecycle.BeginUpdate();
            TransactionLifecycleForm_TreeView_Lifecycle.Nodes.Clear();

            if (transactions.Count == 0) return;

            // Track remaining quantity at each location for split detection
            var locationQuantities = new Dictionary<string, decimal>();

            // First transaction should be IN - this is the root
            var firstTransaction = transactions[0];
            var rootNode = CreateTransactionNode(firstTransaction);
            TransactionLifecycleForm_TreeView_Lifecycle.Nodes.Add(rootNode);

            // Initialize location quantity tracking
            if (!string.IsNullOrEmpty(firstTransaction.ToLocation))
            {
                locationQuantities[firstTransaction.ToLocation] = firstTransaction.Quantity;
            }

            // Process remaining transactions chronologically
            for (int i = 1; i < transactions.Count; i++)
            {
                var transaction = transactions[i];
                var node = CreateTransactionNode(transaction);

                // Check if this is a split transaction
                // Split occurs when TRANSFER quantity < remaining quantity at source location
                bool isSplit = false;
                if (transaction.TransactionType == TransactionType.TRANSFER &&
                    !string.IsNullOrEmpty(transaction.FromLocation) &&
                    locationQuantities.ContainsKey(transaction.FromLocation))
                {
                    var remainingQty = locationQuantities[transaction.FromLocation];
                    if (transaction.Quantity < remainingQty)
                    {
                        isSplit = true;
                    }
                }

                // Update location quantities
                if (!string.IsNullOrEmpty(transaction.FromLocation) &&
                    locationQuantities.ContainsKey(transaction.FromLocation))
                {
                    locationQuantities[transaction.FromLocation] -= transaction.Quantity;
                    if (locationQuantities[transaction.FromLocation] <= 0)
                    {
                        locationQuantities.Remove(transaction.FromLocation);
                    }
                }

                if (!string.IsNullOrEmpty(transaction.ToLocation))
                {
                    if (locationQuantities.ContainsKey(transaction.ToLocation))
                    {
                        locationQuantities[transaction.ToLocation] += transaction.Quantity;
                    }
                    else
                    {
                        locationQuantities[transaction.ToLocation] = transaction.Quantity;
                    }
                }

                // Add node to tree based on location flow
                // Find parent node where ToLocation matches this transaction's FromLocation
                TreeNode? parentNode = null;
                
                if (!string.IsNullOrEmpty(transaction.FromLocation))
                {
                    // Search entire tree for node with ToLocation matching this FromLocation
                    parentNode = FindNodeByLocation(rootNode, transaction.FromLocation);
                }
                
                if (parentNode != null)
                {
                    // Add as child of the node that led to this location
                    parentNode.Nodes.Add(node);
                    if (isSplit)
                    {
                        // Mark split nodes for visual distinction
                        node.ForeColor = Color.Orange;
                    }
                    parentNode.Expand();
                }
                else
                {
                    // No parent found - add as root level (shouldn't happen except for orphaned transactions)
                    TransactionLifecycleForm_TreeView_Lifecycle.Nodes.Add(node);
                }
            }

            // Expand all nodes and select the first one
            TransactionLifecycleForm_TreeView_Lifecycle.ExpandAll();
            if (TransactionLifecycleForm_TreeView_Lifecycle.Nodes.Count > 0)
            {
                TransactionLifecycleForm_TreeView_Lifecycle.SelectedNode = TransactionLifecycleForm_TreeView_Lifecycle.Nodes[0];
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                controlName: nameof(TransactionLifecycleForm));
        }
        finally
        {
            TransactionLifecycleForm_TreeView_Lifecycle.EndUpdate();
        }
    }

    /// <summary>
    /// Creates a TreeNode from a transaction with proper formatting and metadata.
    /// </summary>
    /// <param name="transaction">The transaction to create a node for.</param>
    /// <returns>TreeNode configured with transaction data.</returns>
    private TreeNode CreateTransactionNode(Model_Transactions_Core transaction)
    {
        // Node format: "{Type} - {Location}" (no dates per spec)
        var nodeText = $"{transaction.TransactionType}";
        
        if (!string.IsNullOrEmpty(transaction.FromLocation) && !string.IsNullOrEmpty(transaction.ToLocation))
        {
            nodeText += $" - {transaction.FromLocation} → {transaction.ToLocation}";
        }
        else if (!string.IsNullOrEmpty(transaction.ToLocation))
        {
            nodeText += $" - → {transaction.ToLocation}";
        }
        else if (!string.IsNullOrEmpty(transaction.FromLocation))
        {
            nodeText += $" - {transaction.FromLocation} →";
        }

        nodeText += $" (Qty: {transaction.Quantity})";

        var node = new TreeNode(nodeText)
        {
            Tag = transaction // Store transaction for detail panel updates
        };

        return node;
    }

    /// <summary>
    /// Recursively searches for a node with matching location.
    /// </summary>
    private TreeNode? FindNodeByLocation(TreeNode parentNode, string? location)
    {
        if (string.IsNullOrEmpty(location)) return null;

        // Check if parent node's transaction has this location
        if (parentNode.Tag is Model_Transactions_Core transaction)
        {
            if (transaction.ToLocation == location)
            {
                return parentNode;
            }
        }

        // Recursively search child nodes
        foreach (TreeNode childNode in parentNode.Nodes)
        {
            var found = FindNodeByLocation(childNode, location);
            if (found != null) return found;
        }

        return null;
    }

    #endregion

    #region TreeView Events

    /// <summary>
    /// Handles TreeView node selection changes to update the detail panel.
    /// </summary>
    private void TreeView_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        try
        {
            if (e.Node?.Tag is Model_Transactions_Core transaction)
            {
                LoggingUtility.Log($"[TransactionLifecycleForm] Node selected: Transaction ID {transaction.ID}");
                TransactionLifecycleForm_DetailPanel.Transaction = transaction;
            }
            else
            {
                TransactionLifecycleForm_DetailPanel.ClearDetails();
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionLifecycleForm));
        }
    }

    #endregion

    #region Button Clicks

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        try
        {
            LoggingUtility.Log("[TransactionLifecycleForm] Export button clicked.");
            
            // TODO: T069+ - Implement lifecycle export functionality
            Service_ErrorHandler.ShowConfirmation(
                "Export functionality will be implemented in a future task.",
                "Export"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionLifecycleForm));
        }
    }

    private void BtnPrint_Click(object? sender, EventArgs e)
    {
        // TEMPORARY: Print system being refactored (Phase 1 - Task T002)
        Service_ErrorHandler.ShowInformation(
            "Print functionality is being rebuilt. Coming soon!",
            "Feature Temporarily Unavailable");
        
        /* OLD IMPLEMENTATION - Kept for reference, will be restored in Phase 7
        try
        {
            LoggingUtility.Log("[TransactionLifecycleForm] Print button clicked.");
            
            // TODO: T069+ - Implement lifecycle print functionality
            Service_ErrorHandler.ShowConfirmation(
                "Print functionality will be implemented in a future task.",
                "Print"
            );
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionLifecycleForm));
        }
        */
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
        try
        {
            LoggingUtility.Log("[TransactionLifecycleForm] Close button clicked.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            // Close anyway even if logging fails
            this.Close();
        }
    }

    #endregion
}
