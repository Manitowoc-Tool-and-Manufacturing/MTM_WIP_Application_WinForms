namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Runtime performance metrics.
/// </summary>
public class Model_PerformanceMetrics
{
    #region Properties

    /// <summary>
    /// Timestamp of the metric snapshot.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;

    /// <summary>
    /// Number of open handles.
    /// </summary>
    public int HandleCount { get; set; }

    /// <summary>
    /// Current memory usage in MB.
    /// </summary>
    public double MemoryUsageMB { get; set; }

    /// <summary>
    /// Peak memory usage in MB.
    /// </summary>
    public double PeakMemoryMB { get; set; }

    /// <summary>
    /// Number of active threads.
    /// </summary>
    public int ThreadCount { get; set; }

    /// <summary>
    /// Application start time.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Average response time for recent operations (ms).
    /// </summary>
    public double? AverageResponseTimeMs { get; set; }

    /// <summary>
    /// CPU usage percentage (if available).
    /// </summary>
    public double? CpuUsagePercent { get; set; }

    /// <summary>
    /// GC collection count (Gen 0).
    /// </summary>
    public int GCGen0Collections { get; set; }

    /// <summary>
    /// GC collection count (Gen 1).
    /// </summary>
    public int GCGen1Collections { get; set; }

    /// <summary>
    /// GC collection count (Gen 2).
    /// </summary>
    public int GCGen2Collections { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Application uptime.
    /// </summary>
    public TimeSpan Uptime => DateTime.Now - StartTime;

    /// <summary>
    /// Formatted uptime string.
    /// </summary>
    public string UptimeFormatted
    {
        get
        {
            var ts = Uptime;
            if (ts.TotalDays >= 1)
                return $"{ts.Days}d {ts.Hours}h {ts.Minutes}m";
            if (ts.TotalHours >= 1)
                return $"{ts.Hours}h {ts.Minutes}m {ts.Seconds}s";
            return $"{ts.Minutes}m {ts.Seconds}s";
        }
    }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Captures current performance metrics.
    /// </summary>
    public static Model_PerformanceMetrics Capture()
    {
        var process = System.Diagnostics.Process.GetCurrentProcess();
        return new Model_PerformanceMetrics
        {
            MemoryUsageMB = Math.Round(process.WorkingSet64 / (1024.0 * 1024.0), 1),
            PeakMemoryMB = Math.Round(process.PeakWorkingSet64 / (1024.0 * 1024.0), 1),
            ThreadCount = process.Threads.Count,
            StartTime = process.StartTime,
            GCGen0Collections = GC.CollectionCount(0),
            GCGen1Collections = GC.CollectionCount(1),
            GCGen2Collections = GC.CollectionCount(2)
        };
    }

    #endregion
}
