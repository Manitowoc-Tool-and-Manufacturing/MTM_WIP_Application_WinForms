using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.Transactions
{
    /// <summary>
    /// Modal dialog for displaying detailed information about a single transaction.
    /// Phase 1 shell implementation - full details UI to be implemented in Phase 2.
    /// </summary>
    public partial class TransactionDetailPanel : Form
    {
        #region Fields

        private Model_Transactions? _transaction;

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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDetailPanel"/> class.
        /// </summary>
        public TransactionDetailPanel()
        {
            InitializeComponent();

            // MANDATORY: Constitution Principle IX - Theme System Integration
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);

            WireUpEvents();
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
            // Event handlers will be wired in Phase 2 when detail UI is implemented
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Loads the transaction details into the dialog controls.
        /// Phase 2 implementation will populate detail fields.
        /// </summary>
        private void LoadTransactionDetails()
        {
            if (_transaction == null) return;

            // Phase 2: Populate detail fields with transaction data
            // For now, just set the window title
            Text = $"Transaction Details - ID: {_transaction.ID}";
        }

        #endregion
    }
}
