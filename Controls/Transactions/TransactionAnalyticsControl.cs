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
                        ? string.Format("({0:0.0}%)", _analytics.PercentageIN)
                        : "(0.0%)";

                // OUT transactions card
                TransactionAnalyticsControl_Label_OutValue.Text = _analytics.TotalOUT.ToString("N0");
                TransactionAnalyticsControl_Label_OutPercentage.Text =
                    _analytics.TotalTransactions > 0
                        ? string.Format("({0:0.0}%)", _analytics.PercentageOUT)
                        : "(0.0%)";

                // TRANSFER transactions card
                TransactionAnalyticsControl_Label_TransferValue.Text = _analytics.TotalTRANSFER.ToString("N0");
                TransactionAnalyticsControl_Label_TransferPercentage.Text =
                    _analytics.TotalTransactions > 0
                        ? string.Format("({0:0.0}%)", _analytics.PercentageTRANSFER)
                        : "(0.0%)";

                // Database Lifespan card
                if (_analytics.DatabaseDaySpan > 0)
                {
                    TransactionAnalyticsControl_Label_DatabaseLifespanValue.Text =
                        $"{_analytics.DatabaseDaySpan:N0} {(_analytics.DatabaseDaySpan == 1 ? "day" : "days")}";
                    TransactionAnalyticsControl_Label_AvgDaily.Text =
                        $"Avg. Daily: {_analytics.AverageDailyTransactions:F1}";
                }
                else
                {
                    TransactionAnalyticsControl_Label_DatabaseLifespanValue.Text = "—";
                    TransactionAnalyticsControl_Label_AvgDaily.Text = "Avg. Daily: —";
                }

                // Most Active User card
                if (!string.IsNullOrWhiteSpace(_analytics.MostActiveUser.UserName))
                {
                    TransactionAnalyticsControl_Label_TopUserName.Text = _analytics.MostActiveUser.UserName;
                    TransactionAnalyticsControl_Label_TopUserCount.Text =
                        $"{_analytics.MostActiveUser.TransactionCount:N0} {(_analytics.MostActiveUser.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    TransactionAnalyticsControl_Label_TopUserName.Text = "—";
                    TransactionAnalyticsControl_Label_TopUserCount.Text = "0 transactions";
                }

                // Most Transacted Part card
                if (!string.IsNullOrWhiteSpace(_analytics.MostTransactedPart.PartID))
                {
                    TransactionAnalyticsControl_Label_TopPartID.Text = _analytics.MostTransactedPart.PartID;
                    TransactionAnalyticsControl_Label_TopPartCount.Text =
                        $"{_analytics.MostTransactedPart.TransactionCount:N0} {(_analytics.MostTransactedPart.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    TransactionAnalyticsControl_Label_TopPartID.Text = "—";
                    TransactionAnalyticsControl_Label_TopPartCount.Text = "0 transactions";
                }

                // Busiest Location card
                if (!string.IsNullOrWhiteSpace(_analytics.BusiestLocation.LocationName))
                {
                    TransactionAnalyticsControl_Label_BusiestLocationName.Text = _analytics.BusiestLocation.LocationName;
                    TransactionAnalyticsControl_Label_BusiestLocationCount.Text =
                        $"{_analytics.BusiestLocation.TransactionCount:N0} {(_analytics.BusiestLocation.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    TransactionAnalyticsControl_Label_BusiestLocationName.Text = "—";
                    TransactionAnalyticsControl_Label_BusiestLocationCount.Text = "0 transactions";
                }

                // Most Transferred Part card (Row 3 - Card 1)
                if (!string.IsNullOrWhiteSpace(_analytics.MostTransferredPart.PartID))
                {
                    TransactionAnalyticsControl_Label_MostTransferredPartID.Text = _analytics.MostTransferredPart.PartID;
                    TransactionAnalyticsControl_Label_MostTransferredPartCount.Text =
                        $"{_analytics.MostTransferredPart.TransferCount:N0} {(_analytics.MostTransferredPart.TransferCount == 1 ? "transfer" : "transfers")}";
                }
                else
                {
                    TransactionAnalyticsControl_Label_MostTransferredPartID.Text = "—";
                    TransactionAnalyticsControl_Label_MostTransferredPartCount.Text = "0 transfers";
                }

                // Transaction Rate card (Row 3 - Card 2)
                if (_analytics.TransactionRate > 0)
                {
                    TransactionAnalyticsControl_Label_TransactionRateValue.Text = $"{_analytics.TransactionRate:F1} /day";
                    TransactionAnalyticsControl_Label_TransactionRateTrend.Text =
                        _analytics.TransactionRateTrend ?? "➡️ Stable";
                }
                else
                {
                    TransactionAnalyticsControl_Label_TransactionRateValue.Text = "—";
                    TransactionAnalyticsControl_Label_TransactionRateTrend.Text = "";
                }

                // Busiest Day card (Row 3 - Card 3)
                if (!string.IsNullOrWhiteSpace(_analytics.BusiestDay.DayName))
                {
                    TransactionAnalyticsControl_Label_BusiestDayValue.Text = _analytics.BusiestDay.DayName;
                    TransactionAnalyticsControl_Label_BusiestDayCount.Text =
                        $"{_analytics.BusiestDay.TransactionCount:N0} {(_analytics.BusiestDay.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    TransactionAnalyticsControl_Label_BusiestDayValue.Text = "—";
                    TransactionAnalyticsControl_Label_BusiestDayCount.Text = "0 transactions";
                }

                // Peak Hour card (Row 3 - Card 4)
                if (_analytics.PeakHour.TransactionCount > 0)
                {
                    // Format hour as 12-hour time
                    int hour = _analytics.PeakHour.Hour;
                    string amPm = hour >= 12 ? "PM" : "AM";
                    int displayHour = hour == 0 ? 12 : (hour > 12 ? hour - 12 : hour);
                    TransactionAnalyticsControl_Label_PeakHourValue.Text = $"{displayHour}:00 {amPm}";
                    TransactionAnalyticsControl_Label_PeakHourCount.Text =
                        $"{_analytics.PeakHour.TransactionCount:N0} {(_analytics.PeakHour.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    TransactionAnalyticsControl_Label_PeakHourValue.Text = "—";
                    TransactionAnalyticsControl_Label_PeakHourCount.Text = "0 transactions";
                }

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

                // Row 1: Transaction Type Breakdown
                TransactionAnalyticsControl_Label_TotalValue.Text = "—";
                TransactionAnalyticsControl_Label_InValue.Text = "—";
                TransactionAnalyticsControl_Label_InPercentage.Text = "";
                TransactionAnalyticsControl_Label_OutValue.Text = "—";
                TransactionAnalyticsControl_Label_OutPercentage.Text = "";
                TransactionAnalyticsControl_Label_TransferValue.Text = "—";
                TransactionAnalyticsControl_Label_TransferPercentage.Text = "";

                // Row 2: Database Insights
                TransactionAnalyticsControl_Label_DatabaseLifespanValue.Text = "—";
                TransactionAnalyticsControl_Label_AvgDaily.Text = "Avg. Daily: —";
                TransactionAnalyticsControl_Label_TopUserName.Text = "—";
                TransactionAnalyticsControl_Label_TopUserCount.Text = "0 transactions";
                TransactionAnalyticsControl_Label_TopPartID.Text = "—";
                TransactionAnalyticsControl_Label_TopPartCount.Text = "0 transactions";
                TransactionAnalyticsControl_Label_BusiestLocationName.Text = "—";
                TransactionAnalyticsControl_Label_BusiestLocationCount.Text = "0 transactions";

                // Row 3: Advanced Metrics
                TransactionAnalyticsControl_Label_MostTransferredPartID.Text = "—";
                TransactionAnalyticsControl_Label_MostTransferredPartCount.Text = "0 transfers";
                TransactionAnalyticsControl_Label_TransactionRateValue.Text = "—";
                TransactionAnalyticsControl_Label_TransactionRateTrend.Text = "";
                TransactionAnalyticsControl_Label_BusiestDayValue.Text = "—";
                TransactionAnalyticsControl_Label_BusiestDayCount.Text = "0 transactions";
                TransactionAnalyticsControl_Label_PeakHourValue.Text = "—";
                TransactionAnalyticsControl_Label_PeakHourCount.Text = "0 transactions";

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
