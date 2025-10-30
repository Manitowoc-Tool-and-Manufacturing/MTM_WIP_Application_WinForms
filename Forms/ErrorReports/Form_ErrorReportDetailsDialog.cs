using System;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.ErrorReports;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Models;
using static MTM_WIP_Application_Winforms.Controls.ErrorReports.Control_ErrorReportDetails;

namespace MTM_WIP_Application_Winforms.Forms.ErrorReports
{
    /// <summary>
    /// Modal dialog for displaying complete error report details with status update capabilities.
    /// </summary>
    public partial class Form_ErrorReportDetailsDialog : Form
    {
        #region Fields

        private readonly int _reportId;
        private bool _isInitialized;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the error report ID being displayed.
        /// </summary>
        public int ReportId => _reportId;

        #endregion

        #region Events

        /// <summary>
        /// Raised when the error report status changes (Reviewed/Resolved).
        /// </summary>
        public event EventHandler<StatusChangedEventArgs>? StatusChanged;

        #endregion

        #region Progress Control Methods

        // Reserved for future progress integration

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Form_ErrorReportDetailsDialog"/> class.
        /// </summary>
        /// <param name="reportId">The error report ID to display.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when reportId is less than or equal to 0.</exception>
        public Form_ErrorReportDetailsDialog(int reportId)
        {
            if (reportId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reportId), "Report ID must be greater than 0.");
            }

            _reportId = reportId;

            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);
            WireUpEvents();
        }

        #endregion

        #region Key Processing

        private void WireUpEvents()
        {
            controlErrorReportDetails.StatusChanged += ControlErrorReportDetails_StatusChanged;
            Shown += Form_ErrorReportDetailsDialog_Shown;
        }

        #endregion

        #region ComboBox & UI Events

        private async void Form_ErrorReportDetailsDialog_Shown(object? sender, EventArgs e)
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            // Load report details asynchronously after form is visible
            await controlErrorReportDetails.LoadReportAsync(_reportId);
        }

        private void ControlErrorReportDetails_StatusChanged(object? sender, StatusChangedEventArgs e)
        {
            // Bubble the event up to the parent form
            StatusChanged?.Invoke(this, e);

            // Close dialog after status update (event only fires on successful update)
            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Helpers

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            controlErrorReportDetails.StatusChanged -= ControlErrorReportDetails_StatusChanged;
            Shown -= Form_ErrorReportDetailsDialog_Shown;
            base.OnFormClosed(e);
        }

        #endregion

        #region Cleanup

        // Additional cleanup not required at this time

        #endregion
    }
}
