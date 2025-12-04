using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    /// <summary>
    /// UserControl for displaying detailed information about a single transaction.
    /// Phase 1 shell implementation - full details UI to be implemented in Phase 2.
    /// </summary>
    public partial class TransactionDetailPanel : ThemedUserControl
    {
        #region Fields

        private Model_Transactions_Core? _transaction;
        private bool _isEmbeddedMode;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the transaction to display in the dialog.
        /// </summary>
        internal Model_Transactions_Core? Transaction
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDetailPanel"/> class.
        /// </summary>
        public TransactionDetailPanel()
        {
            InitializeComponent();

            // Disable lifecycle button by default
            TransactionDetailPanel_Button_ViewBatchHistory.Enabled = false;

            WireUpEvents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDetailPanel"/> class with a transaction.
        /// </summary>
        /// <param name="transaction">The transaction to display.</param>
        internal TransactionDetailPanel(Model_Transactions_Core transaction) : this()
        {
            Transaction = transaction;
        }

        #endregion

        #region Event Handlers

        private void WireUpEvents()
        {
            // Event handlers are wired in Designer.cs
            // No manual wiring needed to prevent double-subscription
        }

        private void TransactionDetailPanel_Button_ViewBatchHistory_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validate that we have a transaction with required data
                if (_transaction == null)
                {

                    Service_ErrorHandler.HandleValidationError("No transaction selected.", "Transaction Lifecycle");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_transaction.PartID))
                {

                    Service_ErrorHandler.HandleValidationError("Transaction is missing Part ID.", "Transaction Lifecycle");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_transaction.BatchNumber))
                {

                    Service_ErrorHandler.HandleValidationError("Transaction is missing Batch Number.", "Transaction Lifecycle");
                    return;
                }



                // Create and show the lifecycle form as a modal dialog
                using var lifecycleForm = new Forms.Transactions.TransactionLifecycleForm(_transaction.PartID, _transaction.BatchNumber);
                lifecycleForm.ShowDialog(this.FindForm());


            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
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

                // Enable lifecycle button if we have valid data
                TransactionDetailPanel_Button_ViewBatchHistory.Enabled =
                    !string.IsNullOrWhiteSpace(_transaction.PartID) &&
                    !string.IsNullOrWhiteSpace(_transaction.BatchNumber);

            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
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

                // Disable lifecycle button
                TransactionDetailPanel_Button_ViewBatchHistory.Enabled = false;

            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
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


                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Updates the GroupBox title to show transaction ID.
        /// </summary>
        private void UpdateGroupBoxTitle()
        {
            try
            {
                var transactionId = _transaction?.ID.ToString() ?? "";

                if (string.IsNullOrEmpty(transactionId))
                {
                    TransactionDetailPanel_GroupBox_Main.Text = "Transaction Details";
                }
                else
                {
                    TransactionDetailPanel_GroupBox_Main.Text = $"Transaction Details - ID: {transactionId}";
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
