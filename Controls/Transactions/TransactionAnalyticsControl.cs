using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    /// <summary>
    /// UserControl for displaying transaction analytics with summary cards.
    /// Shows total transactions, IN/OUT/TRANSFER counts and percentages.
    /// </summary>
    public partial class Model_Transactions_Core_AnalyticsControl : UserControl
    {
        #region Fields

        private Model_Transactions_Core_Analytics? _analytics;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the analytics data to display.
        /// </summary>
        internal Model_Transactions_Core_Analytics? Analytics
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
        /// Initializes a new instance of the <see cref="Model_Transactions_Core_AnalyticsControl"/> class.
        /// </summary>
        public Model_Transactions_Core_AnalyticsControl()
        {
            InitializeComponent();

            LoggingUtility.Log("[Model_Transactions_Core_AnalyticsControl] Initializing...");

            // MANDATORY: Constitution Principle IX - Theme System Integration
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);

            LoggingUtility.Log("[Model_Transactions_Core_AnalyticsControl] Initialization complete.");
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
                LoggingUtility.Log($"[Model_Transactions_Core_AnalyticsControl] Displaying analytics: Total={_analytics.TotalTransactions}");

                // Total count card
                Model_Transactions_Core_AnalyticsControl_Label_TotalValue.Text = _analytics.TotalTransactions.ToString("N0");

                // IN transactions card
                Model_Transactions_Core_AnalyticsControl_Label_InValue.Text = _analytics.TotalIN.ToString("N0");
                Model_Transactions_Core_AnalyticsControl_Label_InPercentage.Text =
                    _analytics.TotalTransactions > 0
                        ? string.Format("({0:0.0}%)", _analytics.PercentageIN)
                        : "(0.0%)";

                // OUT transactions card
                Model_Transactions_Core_AnalyticsControl_Label_OutValue.Text = _analytics.TotalOUT.ToString("N0");
                Model_Transactions_Core_AnalyticsControl_Label_OutPercentage.Text =
                    _analytics.TotalTransactions > 0
                        ? string.Format("({0:0.0}%)", _analytics.PercentageOUT)
                        : "(0.0%)";

                // TRANSFER transactions card
                Model_Transactions_Core_AnalyticsControl_Label_TransferValue.Text = _analytics.TotalTRANSFER.ToString("N0");
                Model_Transactions_Core_AnalyticsControl_Label_TransferPercentage.Text =
                    _analytics.TotalTransactions > 0
                        ? string.Format("({0:0.0}%)", _analytics.PercentageTRANSFER)
                        : "(0.0%)";

                // Database Lifespan card
                if (_analytics.DatabaseDaySpan > 0)
                {
                    Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanValue.Text =
                        $"{_analytics.DatabaseDaySpan:N0} {(_analytics.DatabaseDaySpan == 1 ? "day" : "days")}";
                    Model_Transactions_Core_AnalyticsControl_Label_AvgDaily.Text =
                        $"Avg. Daily: {_analytics.AverageDailyTransactions:F1}";
                }
                else
                {
                    Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanValue.Text = "—";
                    Model_Transactions_Core_AnalyticsControl_Label_AvgDaily.Text = "Avg. Daily: —";
                }

                // Most Active User card
                if (!string.IsNullOrWhiteSpace(_analytics.MostActiveUser.UserName))
                {
                    Model_Transactions_Core_AnalyticsControl_Label_TopUserName.Text = _analytics.MostActiveUser.UserName;
                    Model_Transactions_Core_AnalyticsControl_Label_TopUserCount.Text =
                        $"{_analytics.MostActiveUser.TransactionCount:N0} {(_analytics.MostActiveUser.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    Model_Transactions_Core_AnalyticsControl_Label_TopUserName.Text = "—";
                    Model_Transactions_Core_AnalyticsControl_Label_TopUserCount.Text = "0 transactions";
                }

                // Most Transacted Part card
                if (!string.IsNullOrWhiteSpace(_analytics.MostTransactedPart.PartID))
                {
                    Model_Transactions_Core_AnalyticsControl_Label_TopPartID.Text = _analytics.MostTransactedPart.PartID;
                    Model_Transactions_Core_AnalyticsControl_Label_TopPartCount.Text =
                        $"{_analytics.MostTransactedPart.TransactionCount:N0} {(_analytics.MostTransactedPart.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    Model_Transactions_Core_AnalyticsControl_Label_TopPartID.Text = "—";
                    Model_Transactions_Core_AnalyticsControl_Label_TopPartCount.Text = "0 transactions";
                }

                // Busiest Location card
                if (!string.IsNullOrWhiteSpace(_analytics.BusiestLocation.LocationName))
                {
                    Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationName.Text = _analytics.BusiestLocation.LocationName;
                    Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCount.Text =
                        $"{_analytics.BusiestLocation.TransactionCount:N0} {(_analytics.BusiestLocation.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationName.Text = "—";
                    Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCount.Text = "0 transactions";
                }

                // Most Transferred Part card (Row 3 - Card 1)
                if (!string.IsNullOrWhiteSpace(_analytics.MostTransferredPart.PartID))
                {
                    Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartID.Text = _analytics.MostTransferredPart.PartID;
                    Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCount.Text =
                        $"{_analytics.MostTransferredPart.TransferCount:N0} {(_analytics.MostTransferredPart.TransferCount == 1 ? "transfer" : "transfers")}";
                }
                else
                {
                    Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartID.Text = "—";
                    Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCount.Text = "0 transfers";
                }

                // Transaction Rate card (Row 3 - Card 2)
                if (_analytics.TransactionRate > 0)
                {
                    Model_Transactions_Core_AnalyticsControl_Label_TransactionRateValue.Text = $"{_analytics.TransactionRate:F1} /day";
                    Model_Transactions_Core_AnalyticsControl_Label_TransactionRateTrend.Text =
                        _analytics.TransactionRateTrend ?? "➡️ Stable";
                }
                else
                {
                    Model_Transactions_Core_AnalyticsControl_Label_TransactionRateValue.Text = "—";
                    Model_Transactions_Core_AnalyticsControl_Label_TransactionRateTrend.Text = "";
                }

                // Busiest Day card (Row 3 - Card 3)
                if (!string.IsNullOrWhiteSpace(_analytics.BusiestDay.DayName))
                {
                    Model_Transactions_Core_AnalyticsControl_Label_BusiestDayValue.Text = _analytics.BusiestDay.DayName;
                    Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCount.Text =
                        $"{_analytics.BusiestDay.TransactionCount:N0} {(_analytics.BusiestDay.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    Model_Transactions_Core_AnalyticsControl_Label_BusiestDayValue.Text = "—";
                    Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCount.Text = "0 transactions";
                }

                // Peak Hour card (Row 3 - Card 4)
                if (_analytics.PeakHour.TransactionCount > 0)
                {
                    // Format hour as 12-hour time
                    int hour = _analytics.PeakHour.Hour;
                    string amPm = hour >= 12 ? "PM" : "AM";
                    int displayHour = hour == 0 ? 12 : (hour > 12 ? hour - 12 : hour);
                    Model_Transactions_Core_AnalyticsControl_Label_PeakHourValue.Text = $"{displayHour}:00 {amPm}";
                    Model_Transactions_Core_AnalyticsControl_Label_PeakHourCount.Text =
                        $"{_analytics.PeakHour.TransactionCount:N0} {(_analytics.PeakHour.TransactionCount == 1 ? "transaction" : "transactions")}";
                }
                else
                {
                    Model_Transactions_Core_AnalyticsControl_Label_PeakHourValue.Text = "—";
                    Model_Transactions_Core_AnalyticsControl_Label_PeakHourCount.Text = "0 transactions";
                }

                LoggingUtility.Log("[Model_Transactions_Core_AnalyticsControl] Analytics displayed successfully.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Services.Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Low,
                    controlName: nameof(Model_Transactions_Core_AnalyticsControl));
            }
        }

        /// <summary>
        /// Clears all analytics data from the control.
        /// </summary>
        public void ClearAnalytics()
        {
            try
            {
                LoggingUtility.Log("[Model_Transactions_Core_AnalyticsControl] Clearing analytics.");

                _analytics = null;

                // Row 1: Transaction Type Breakdown
                Model_Transactions_Core_AnalyticsControl_Label_TotalValue.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_InValue.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_InPercentage.Text = "";
                Model_Transactions_Core_AnalyticsControl_Label_OutValue.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_OutPercentage.Text = "";
                Model_Transactions_Core_AnalyticsControl_Label_TransferValue.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_TransferPercentage.Text = "";

                // Row 2: Database Insights
                Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanValue.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_AvgDaily.Text = "Avg. Daily: —";
                Model_Transactions_Core_AnalyticsControl_Label_TopUserName.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_TopUserCount.Text = "0 transactions";
                Model_Transactions_Core_AnalyticsControl_Label_TopPartID.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_TopPartCount.Text = "0 transactions";
                Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationName.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCount.Text = "0 transactions";

                // Row 3: Advanced Metrics
                Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartID.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCount.Text = "0 transfers";
                Model_Transactions_Core_AnalyticsControl_Label_TransactionRateValue.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_TransactionRateTrend.Text = "";
                Model_Transactions_Core_AnalyticsControl_Label_BusiestDayValue.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCount.Text = "0 transactions";
                Model_Transactions_Core_AnalyticsControl_Label_PeakHourValue.Text = "—";
                Model_Transactions_Core_AnalyticsControl_Label_PeakHourCount.Text = "0 transactions";

                LoggingUtility.Log("[Model_Transactions_Core_AnalyticsControl] Analytics cleared.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Services.Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Low,
                    controlName: nameof(Model_Transactions_Core_AnalyticsControl));
            }
        }

        #endregion

    }
}
