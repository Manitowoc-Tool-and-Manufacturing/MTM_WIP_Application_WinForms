using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.ErrorReports;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.ErrorReports
{
    public partial class Form_ViewErrorReports : ThemedForm
    {
        #region Fields

        private bool _isInitialized;

        #endregion

        #region Properties

        internal Control_ErrorReportsGrid GridControl => controlErrorReportsGrid;

        #endregion

        #region Progress Control Methods

        // Reserved for future progress integration

        #endregion

        #region Constructors

        public Form_ViewErrorReports()
        {
            InitializeComponent();
            // DPI scaling and layout now handled by ThemedForm.OnLoad
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            WireUpEvents();
        }

        #endregion

        #region Key Processing

        private void WireUpEvents()
        {
            controlErrorReportsGrid.ReportSelected += ControlErrorReportsGrid_ReportSelected;
            Shown += Form_ViewErrorReports_Shown;
        }

        #endregion

        #region ComboBox & UI Events

        private void Form_ViewErrorReports_Shown(object? sender, EventArgs e)
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;
        }

        private async void ControlErrorReportsGrid_ReportSelected(object? sender, int reportId)
        {
            await ShowErrorReportDetailsDialogAsync(reportId);
        }

        #endregion

        #region Helpers

        private Task ShowErrorReportDetailsDialogAsync(int reportId)
        {
            try
            {
                using Form_ErrorReportDetailsDialog dialog = new(reportId);
                dialog.StatusChanged += async (s, e) =>
                {
                    await controlErrorReportsGrid.LoadReportsAsync(controlErrorReportsGrid.LastFilter);
                    controlErrorReportsGrid.SelectReportById(e.ReportId);
                };

                dialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: Name);
            }
            return Task.CompletedTask;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            controlErrorReportsGrid.ReportSelected -= ControlErrorReportsGrid_ReportSelected;
            Shown -= Form_ViewErrorReports_Shown;
            base.OnFormClosed(e);
        }

        #endregion

        #region Cleanup

        // Additional cleanup not required at this time

        #endregion
    }
}
