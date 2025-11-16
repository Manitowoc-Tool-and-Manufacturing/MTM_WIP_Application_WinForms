using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvancedInventory
{
    /// <summary>
    /// Step 1: Mode Selection - Choose entry method (Single, Batch, Excel, Templates)
    /// </summary>
    public partial class Control_AdvInv_Main : UserControl
    {
        #region Fields

        // Wizard navigation will be implemented later

        #endregion

        #region Events

        /// <summary>
        /// Fired when user selects Single Transaction mode
        /// </summary>
        public event EventHandler? SingleTransactionSelected;

        /// <summary>
        /// Fired when user selects Batch Entry mode
        /// </summary>
        public event EventHandler? BatchEntrySelected;

        /// <summary>
        /// Fired when user selects Excel Import mode
        /// </summary>
        public event EventHandler? ExcelImportSelected;

        /// <summary>
        /// Fired when user selects Template mode
        /// </summary>
        public event EventHandler? TemplateSelected;

        #endregion

        #region Constructors

        public Control_AdvInv_Main()
        {
            InitializeComponent();
            WireUpEvents();
        }

        #endregion

        #region Event Wiring

        private void WireUpEvents()
        {
            // Wire up button click events to fire mode selection events
            // Implementation will be added during design phase
        }

        #endregion


    }
}
