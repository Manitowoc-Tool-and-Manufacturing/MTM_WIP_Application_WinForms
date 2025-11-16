using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvInv
{
    /// <summary>
    /// Step 3: Review & Confirm - Preview transactions before saving
    /// </summary>
    public partial class Control_AdvInv_Review : UserControl
    {
        #region Fields

        // Will hold transaction preview data

        #endregion

        #region Events

        /// <summary>
        /// Fired when user confirms and wants to proceed to save
        /// </summary>
        public event EventHandler? ConfirmAndSaveRequested;

        /// <summary>
        /// Fired when user wants to go back and edit
        /// </summary>
        public event EventHandler? BackToEditRequested;

        /// <summary>
        /// Fired when user requests dry-run validation
        /// </summary>
        public event EventHandler? DryRunRequested;

        #endregion

        #region Constructors

        public Control_AdvInv_Review()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads transaction preview data into the review grid
        /// </summary>
        public void LoadTransactionPreview(object transactionData)
        {
            // Implementation will be added during design phase
        }

        /// <summary>
        /// Updates validation status display
        /// </summary>
        public void UpdateValidationStatus(bool allValid, int warningCount, int errorCount)
        {
            // Implementation will be added during design phase
        }

        #endregion
    }
}
