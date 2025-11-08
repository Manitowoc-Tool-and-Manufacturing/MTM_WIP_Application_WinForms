using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    /// <summary>
    /// UserControl for displaying transaction analytics with summary cards.
    /// Shows total transactions, IN/OUT/TRANSFER counts and percentages.
    /// </summary>
    public partial class TransactionAnalyticsControl : UserControl
    {
        #region Fields

        private TransactionAnalytics? _analytics;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the analytics data to display.
        /// </summary>
        internal TransactionAnalytics? Analytics
        {
            get => _analytics;
            set
            {
                _analytics = value;
                DisplayAnalytics();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionAnalyticsControl"/> class.
        /// </summary>
        public TransactionAnalyticsControl()
        {
            InitializeComponent();

            LoggingUtility.Log("[TransactionAnalyticsControl] Initializing...");

            // MANDATORY: Constitution Principle IX - Theme System Integration
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);

            LoggingUtility.Log("[TransactionAnalyticsControl] Initialization complete.");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays the analytics data in the summary cards.
        /// </summary>
        public void DisplayAnalytics()
        {
            if (_analytics == null)
            {
                ClearAnalytics();
                return;
            }

            try
            {
                LoggingUtility.Log($"[TransactionAnalyticsControl] Displaying analytics: Total={_analytics.TotalTransactions}");

                // Total count card
                TransactionAnalyticsControl_Label_TotalValue.Text = _analytics.TotalTransactions.ToString("N0");

                // IN transactions card
                TransactionAnalyticsControl_Label_InValue.Text = _analytics.TotalIN.ToString("N0");
                TransactionAnalyticsControl_Label_InPercentage.Text = 
                    _analytics.TotalTransactions > 0 
                        ? $"({_analytics.PercentageIN:F1}%)"
                        : "(0.0%)";

                // OUT transactions card
                TransactionAnalyticsControl_Label_OutValue.Text = _analytics.TotalOUT.ToString("N0");
                TransactionAnalyticsControl_Label_OutPercentage.Text = 
                    _analytics.TotalTransactions > 0 
                        ? $"({_analytics.PercentageOUT:F1}%)"
                        : "(0.0%)";

                // TRANSFER transactions card
                TransactionAnalyticsControl_Label_TransferValue.Text = _analytics.TotalTRANSFER.ToString("N0");
                TransactionAnalyticsControl_Label_TransferPercentage.Text = 
                    _analytics.TotalTransactions > 0 
                        ? $"({_analytics.PercentageTRANSFER:F1}%)"
                        : "(0.0%)";

                LoggingUtility.Log("[TransactionAnalyticsControl] Analytics displayed successfully.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Services.Service_ErrorHandler.HandleException(ex, Models.ErrorSeverity.Low,
                    controlName: nameof(TransactionAnalyticsControl));
            }
        }

        /// <summary>
        /// Clears all analytics data from the control.
        /// </summary>
        public void ClearAnalytics()
        {
            try
            {
                LoggingUtility.Log("[TransactionAnalyticsControl] Clearing analytics.");

                _analytics = null;

                TransactionAnalyticsControl_Label_TotalValue.Text = "—";
                TransactionAnalyticsControl_Label_InValue.Text = "—";
                TransactionAnalyticsControl_Label_InPercentage.Text = "";
                TransactionAnalyticsControl_Label_OutValue.Text = "—";
                TransactionAnalyticsControl_Label_OutPercentage.Text = "";
                TransactionAnalyticsControl_Label_TransferValue.Text = "—";
                TransactionAnalyticsControl_Label_TransferPercentage.Text = "";

                LoggingUtility.Log("[TransactionAnalyticsControl] Analytics cleared.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Services.Service_ErrorHandler.HandleException(ex, Models.ErrorSeverity.Low,
                    controlName: nameof(TransactionAnalyticsControl));
            }
        }

        #endregion
    }
}
