using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    /// <summary>
    /// UserControl for displaying detailed information about a single transaction.
    /// Phase 1 shell implementation - full details UI to be implemented in Phase 2.
    /// </summary>
    public partial class TransactionDetailPanel : UserControl
    {
        #region Fields

        private Model_Transactions? _transaction;
        private bool _detailsCollapsed;
        private float _notesRowOriginalHeight;
        private bool _isEmbeddedMode;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the transaction to display in the dialog.
        /// </summary>
        internal Model_Transactions? Transaction
        {
            get => _transaction;
            set
            {
                _transaction = value;
                if (_transaction != null)
                {
                    LoadTransactionDetails();
                }
            }
        }

        /// <summary>
        /// Gets or sets whether this panel is embedded in another form (e.g., TransactionLifecycleForm).
        /// When true, hides recursive navigation controls like "View Batch History" button.
        /// </summary>
        internal bool IsEmbeddedMode
        {
            get => _isEmbeddedMode;
            set
            {
                _isEmbeddedMode = value;
                ConfigureEmbeddedMode();
            }
        }

        /// <summary>
        /// Gets or sets whether the details section is collapsed.
        /// </summary>
        public bool DetailsCollapsed
        {
            get => _detailsCollapsed;
            set
            {
                if (_detailsCollapsed != value)
                {
                    ToggleDetailsCollapsed();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDetailPanel"/> class.
        /// </summary>
        public TransactionDetailPanel()
        {
            InitializeComponent();

            LoggingUtility.Log("[TransactionDetailPanel] Initializing...");

            // MANDATORY: Constitution Principle IX - Theme System Integration
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);

            _notesRowOriginalHeight = TransactionDetailPanel_TableLayout_Main.RowStyles.Count > 3
                ? TransactionDetailPanel_TableLayout_Main.RowStyles[3].Height
                : 70F;

            // Add tooltip to make collapse feature discoverable
            var toolTip = new ToolTip();
            toolTip.SetToolTip(TransactionDetailPanel_GroupBox_Main, "Click header to collapse/expand details");

            WireUpEvents();

            LoggingUtility.Log("[TransactionDetailPanel] Initialization complete.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDetailPanel"/> class with a transaction.
        /// </summary>
        /// <param name="transaction">The transaction to display.</param>
        internal TransactionDetailPanel(Model_Transactions transaction) : this()
        {
            Transaction = transaction;
        }

        #endregion

        #region Event Handlers

        private void WireUpEvents()
        {
            // Event handlers are wired in Designer.cs
            // No manual wiring needed to prevent double-subscription
            
            // Wire up collapse/expand button click
            TransactionDetailPanel_GroupBox_Main.Click += GroupBox_Header_Click;
        }

        private void GroupBox_Header_Click(object? sender, EventArgs e)
        {
            // Toggle collapse/expand when clicking the GroupBox header
            ToggleDetailsCollapsed();
        }

        private void TransactionDetailPanel_Button_ViewBatchHistory_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validate that we have a transaction with required data
                if (_transaction == null)
                {
                    LoggingUtility.Log("[TransactionDetailPanel] Cannot open lifecycle - no transaction loaded.");
                    Service_ErrorHandler.HandleValidationError("No transaction selected.", "Transaction Lifecycle");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_transaction.PartID))
                {
                    LoggingUtility.Log("[TransactionDetailPanel] Cannot open lifecycle - missing PartID.");
                    Service_ErrorHandler.HandleValidationError("Transaction is missing Part ID.", "Transaction Lifecycle");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_transaction.BatchNumber))
                {
                    LoggingUtility.Log("[TransactionDetailPanel] Cannot open lifecycle - missing BatchNumber.");
                    Service_ErrorHandler.HandleValidationError("Transaction is missing Batch Number.", "Transaction Lifecycle");
                    return;
                }

                LoggingUtility.Log($"[TransactionDetailPanel] Opening lifecycle form for Part: {_transaction.PartID}, Batch: {_transaction.BatchNumber}");

                // Create and show the lifecycle form as a modal dialog
                using var lifecycleForm = new Forms.Transactions.TransactionLifecycleForm(_transaction.PartID, _transaction.BatchNumber);
                lifecycleForm.ShowDialog(this.FindForm());

                LoggingUtility.Log("[TransactionDetailPanel] Lifecycle form closed.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["TransactionID"] = _transaction?.ID ?? 0,
                        ["PartID"] = _transaction?.PartID ?? "N/A",
                        ["BatchNumber"] = _transaction?.BatchNumber ?? "N/A"
                    },
                    controlName: nameof(TransactionDetailPanel));
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Loads the transaction details into the panel controls.
        /// Phase 2 implementation will populate detail fields.
        /// </summary>
        private void LoadTransactionDetails()
        {
            if (_transaction == null) return;

            try
            {
                LoggingUtility.Log($"[TransactionDetailPanel] Loading transaction details for ID: {_transaction.ID}");

                // Update GroupBox title with collapse indicator
                UpdateGroupBoxTitle();
                
                // Populate caption labels (left column)
                TransactionDetailPanel_Label_IdCaption.Text = "ID:";
                TransactionDetailPanel_Label_TypeCaption.Text = "Type:";
                TransactionDetailPanel_Label_ItemTypeCaption.Text = "Item Type:";
                TransactionDetailPanel_Label_PartCaption.Text = "Part Number:";
                TransactionDetailPanel_Label_BatchCaption.Text = "Batch:";
                TransactionDetailPanel_Label_QuantityCaption.Text = "Quantity:";
                TransactionDetailPanel_Label_FromCaption.Text = "From:";
                TransactionDetailPanel_Label_ToCaption.Text = "To:";
                TransactionDetailPanel_Label_OperationCaption.Text = "Operation:";
                TransactionDetailPanel_Label_UserCaption.Text = "User:";
                TransactionDetailPanel_Label_DateCaption.Text = "Date/Time:";
                
                // Populate value labels (right column)
                TransactionDetailPanel_Label_IdValue.Text = _transaction.ID.ToString();
                TransactionDetailPanel_Label_TypeValue.Text = _transaction.TransactionType.ToString();
                TransactionDetailPanel_Label_ItemTypeValue.Text = _transaction.ItemType ?? "—";
                TransactionDetailPanel_Label_PartValue.Text = _transaction.PartID ?? "—";
                TransactionDetailPanel_Label_BatchValue.Text = _transaction.BatchNumber ?? "—";
                TransactionDetailPanel_Label_QuantityValue.Text = _transaction.Quantity.ToString();
                TransactionDetailPanel_Label_FromValue.Text = _transaction.FromLocation ?? "—";
                TransactionDetailPanel_Label_ToValue.Text = _transaction.ToLocation ?? "—";
                TransactionDetailPanel_Label_OperationValue.Text = _transaction.Operation ?? "—";
                TransactionDetailPanel_Label_UserValue.Text = _transaction.User ?? "—";
                TransactionDetailPanel_Label_DateValue.Text = _transaction.DateTime.ToString("MM/dd/yyyy HH:mm:ss");
                TransactionDetailPanel_TextBox_Notes.Text = string.IsNullOrWhiteSpace(_transaction.Notes) ? "—" : _transaction.Notes;

                LoggingUtility.Log($"[TransactionDetailPanel] Transaction details loaded successfully.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    controlName: nameof(TransactionDetailPanel));
            }
        }

        /// <summary>
        /// Clears all transaction details from the panel.
        /// </summary>
        public void ClearDetails()
        {
            try
            {
                LoggingUtility.Log("[TransactionDetailPanel] Clearing transaction details.");

                _transaction = null;

                // Update GroupBox title
                UpdateGroupBoxTitle();

                // Clear caption labels (left column)
                TransactionDetailPanel_Label_IdCaption.Text = string.Empty;
                TransactionDetailPanel_Label_TypeCaption.Text = string.Empty;
                TransactionDetailPanel_Label_ItemTypeCaption.Text = string.Empty;
                TransactionDetailPanel_Label_PartCaption.Text = string.Empty;
                TransactionDetailPanel_Label_BatchCaption.Text = string.Empty;
                TransactionDetailPanel_Label_QuantityCaption.Text = string.Empty;
                TransactionDetailPanel_Label_FromCaption.Text = string.Empty;
                TransactionDetailPanel_Label_ToCaption.Text = string.Empty;
                TransactionDetailPanel_Label_OperationCaption.Text = string.Empty;
                TransactionDetailPanel_Label_UserCaption.Text = string.Empty;
                TransactionDetailPanel_Label_DateCaption.Text = string.Empty;

                // Clear value labels (right column)
                TransactionDetailPanel_Label_IdValue.Text = "—";
                TransactionDetailPanel_Label_TypeValue.Text = "—";
                TransactionDetailPanel_Label_ItemTypeValue.Text = "—";
                TransactionDetailPanel_Label_PartValue.Text = "—";
                TransactionDetailPanel_Label_BatchValue.Text = "—";
                TransactionDetailPanel_Label_QuantityValue.Text = "—";
                TransactionDetailPanel_Label_FromValue.Text = "—";
                TransactionDetailPanel_Label_ToValue.Text = "—";
                TransactionDetailPanel_Label_OperationValue.Text = "—";
                TransactionDetailPanel_Label_UserValue.Text = "—";
                TransactionDetailPanel_Label_DateValue.Text = "—";
                TransactionDetailPanel_TextBox_Notes.Text = "—";

                LoggingUtility.Log("[TransactionDetailPanel] Transaction details cleared successfully.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                    controlName: nameof(TransactionDetailPanel));
            }
        }

        /// <summary>
        /// Configures the panel for embedded mode (inside TransactionLifecycleForm).
        /// Hides the "View Batch History" button and Related Transactions section to prevent recursion.
        /// </summary>
        private void ConfigureEmbeddedMode()
        {
            try
            {
                if (_isEmbeddedMode)
                {
                    // Hide the entire Related Transactions section when embedded
                    TransactionDetailPanel_TableLayout_RelatedHeader.Visible = false;
                    TransactionDetailPanel_Label_RelatedStatus.Visible = false;
                    
                    // Collapse the row containing related transactions
                    if (TransactionDetailPanel_TableLayout_Main.RowStyles.Count > 3)
                    {
                        TransactionDetailPanel_TableLayout_Main.RowStyles[3].SizeType = SizeType.Absolute;
                        TransactionDetailPanel_TableLayout_Main.RowStyles[3].Height = 0;
                    }
                    
                    LoggingUtility.Log("[TransactionDetailPanel] Configured for embedded mode - related transactions section hidden.");
                }
                else
                {
                    // Show everything in standalone mode
                    TransactionDetailPanel_TableLayout_RelatedHeader.Visible = true;
                    TransactionDetailPanel_Label_RelatedStatus.Visible = true;
                    
                    // Restore the row height
                    if (TransactionDetailPanel_TableLayout_Main.RowStyles.Count > 3)
                    {
                        TransactionDetailPanel_TableLayout_Main.RowStyles[3].SizeType = SizeType.AutoSize;
                    }
                    
                    LoggingUtility.Log("[TransactionDetailPanel] Configured for standalone mode.");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }
      
        /// <summary>
        /// Toggles the collapsed/expanded state of the transaction details section.
        /// </summary>
        private void ToggleDetailsCollapsed()
        {
            try
            {
                _detailsCollapsed = !_detailsCollapsed;

                LoggingUtility.Log($"[TransactionDetailPanel] Toggling details. Collapsed: {_detailsCollapsed}");

                if (_detailsCollapsed)
                {
                    // Collapse the details section
                    TransactionDetailPanel_TableLayout_Details.Visible = false;
                    TransactionDetailPanel_TextBox_Notes.Visible = false;
                    TransactionDetailPanel_Label_NotesCaption.Visible = false;
                    TransactionDetailPanel_TableLayout_RelatedHeader.Visible = false;
                    TransactionDetailPanel_Label_RelatedStatus.Visible = false;

                    // Update GroupBox title to show it's collapsed
                    UpdateGroupBoxTitle();
                }
                else
                {
                    // Expand the details section
                    TransactionDetailPanel_TableLayout_Details.Visible = true;
                    TransactionDetailPanel_TextBox_Notes.Visible = true;
                    TransactionDetailPanel_Label_NotesCaption.Visible = true;
                    
                    // Only show related section if not in embedded mode
                    if (!_isEmbeddedMode)
                    {
                        TransactionDetailPanel_TableLayout_RelatedHeader.Visible = true;
                        TransactionDetailPanel_Label_RelatedStatus.Visible = true;
                    }

                    // Update GroupBox title to show it's expanded
                    UpdateGroupBoxTitle();
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                    controlName: nameof(TransactionDetailPanel));
            }
        }

        /// <summary>
        /// Updates the GroupBox title to show collapse/expand indicator and transaction ID.
        /// </summary>
        private void UpdateGroupBoxTitle()
        {
            try
            {
                var indicator = _detailsCollapsed ? "▶" : "▼";
                var transactionId = _transaction?.ID.ToString() ?? "";
                
                if (string.IsNullOrEmpty(transactionId))
                {
                    TransactionDetailPanel_GroupBox_Main.Text = $"{indicator} Transaction Details (Click to {(_detailsCollapsed ? "Expand" : "Collapse")})";
                }
                else
                {
                    TransactionDetailPanel_GroupBox_Main.Text = $"{indicator} Transaction Details - ID: {transactionId} (Click to {(_detailsCollapsed ? "Expand" : "Collapse")})";
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion
    }
}
