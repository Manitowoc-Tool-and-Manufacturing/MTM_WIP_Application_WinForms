using System;
using System.Collections.Generic;

namespace MTM_WIP_Application_Winforms.Models
{
    public class Model_ReceivingAnalytics
    {
        public List<AnalyticsDataPoint> History { get; set; } = new List<AnalyticsDataPoint>();
        public List<AnalyticsDataPoint> Forecast { get; set; } = new List<AnalyticsDataPoint>();
    }

    public class AnalyticsDataPoint
    {
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
        public int Count { get; set; }
        public string Type { get; set; } = string.Empty; // "Part", "MMC", "MMF", "Service", etc.
        public string PartNumber { get; set; } = string.Empty;
    }
}
