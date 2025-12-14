using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Services.DeveloperTools;

public class Service_DeveloperTools : IService_DeveloperTools
{
    #region Fields

    private readonly ILoggingService _logger;
    private readonly IDao_DeveloperTools _dao;
    private readonly IService_FeedbackManager _feedbackManager;

    #endregion

    #region Constructor

    public Service_DeveloperTools(
        ILoggingService logger,
        IDao_DeveloperTools dao,
        IService_FeedbackManager feedbackManager)
    {
        _logger = logger;
        _dao = dao;
        _feedbackManager = feedbackManager;
    }

    #endregion

    #region Dashboard Statistics

    public async Task<Model_Dao_Result<Model_LogStatistics>> GetLogStatisticsAsync(DateTime start, DateTime end)
    {
        try
        {
            var result = await _dao.GetLogStatisticsAsync(start, end);
            if (!result.IsSuccess) return Model_Dao_Result<Model_LogStatistics>.Failure(result.ErrorMessage);

            var stats = new Model_LogStatistics();
            if (result.Data != null && result.Data.Rows.Count > 0)
            {
                var row = result.Data.Rows[0];
                
                // Debug logging for columns
                var columns = string.Join(", ", result.Data.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                _logger.Log(Enum_LogLevel.Information, "DeveloperTools", $"GetLogStatistics columns: {columns}");

                stats.TotalCount = Convert.ToInt32(row["TotalCount"]);
                stats.ErrorCount = Convert.ToInt32(row["ErrorCount"]);
                stats.WarningCount = Convert.ToInt32(row["WarningCount"]);
                stats.InfoCount = Convert.ToInt32(row["InfoCount"]);
                
                if (result.Data.Columns.Contains("LastErrorTime") && row["LastErrorTime"] != DBNull.Value)
                    stats.LastErrorTime = Convert.ToDateTime(row["LastErrorTime"]);
                
                if (result.Data.Columns.Contains("LastErrorMessage"))
                    stats.LastErrorMessage = row["LastErrorMessage"]?.ToString();
            }
            return Model_Dao_Result<Model_LogStatistics>.Success(stats);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<Model_LogStatistics>.Failure(ex.Message);
        }
    }

    public async Task<Model_Dao_Result<Model_FeedbackSummary>> GetFeedbackSummaryAsync()
    {
        try
        {
            var result = await _dao.GetFeedbackSummaryAsync();
            if (!result.IsSuccess) return Model_Dao_Result<Model_FeedbackSummary>.Failure(result.ErrorMessage);

            var summary = new Model_FeedbackSummary();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    var status = row["Status"]?.ToString();
                    var count = Convert.ToInt32(row["Count"]);
                    
                    if (status == "New" || status == "Open") summary.NewCount += count;
                    else if (status == "In Progress") summary.InProgressCount += count;
                    else if (status == "Resolved" || status == "Closed") summary.ResolvedCount += count;
                }
                summary.TotalCount = summary.NewCount + summary.InProgressCount + summary.ResolvedCount;
            }
            return Model_Dao_Result<Model_FeedbackSummary>.Success(summary);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<Model_FeedbackSummary>.Failure(ex.Message);
        }
    }

    public async Task<Model_Dao_Result<List<Model_DevLogEntry>>> GetRecentErrorsAsync(int count = 10)
    {
        try
        {
            var result = await _dao.GetRecentErrorsAsync(count);
            if (!result.IsSuccess) return Model_Dao_Result<List<Model_DevLogEntry>>.Failure(result.ErrorMessage);

            var list = MapDataTableToLogEntries(result.Data);
            return Model_Dao_Result<List<Model_DevLogEntry>>.Success(list);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<List<Model_DevLogEntry>>.Failure(ex.Message);
        }
    }

    #endregion

    #region Log Queries

    public async Task<Model_Dao_Result<List<Model_DevLogEntry>>> GetLogEntriesAsync(Model_DevLogFilter filter)
    {
        try
        {
            var result = await _dao.GetLogEntriesAsync(filter);
            if (!result.IsSuccess) return Model_Dao_Result<List<Model_DevLogEntry>>.Failure(result.ErrorMessage);

            var list = MapDataTableToLogEntries(result.Data);
            return Model_Dao_Result<List<Model_DevLogEntry>>.Success(list);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<List<Model_DevLogEntry>>.Failure(ex.Message);
        }
    }

    public async Task<Model_Dao_Result<List<Model_ErrorGrouping>>> GetErrorGroupingsAsync(
        Enum_LogGroupBy groupBy, 
        Model_DevLogFilter? filter = null)
    {
        try
        {
            var start = filter?.DateFrom ?? new DateTime(1753, 1, 1);
            var end = filter?.DateTo ?? new DateTime(2100, 1, 1);

            var result = await _dao.GetErrorGroupingsAsync(start, end);
            if (!result.IsSuccess) return Model_Dao_Result<List<Model_ErrorGrouping>>.Failure(result.ErrorMessage);

            var list = new List<Model_ErrorGrouping>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    list.Add(new Model_ErrorGrouping
                    {
                        GroupKey = row["GroupKey"]?.ToString() ?? "",
                        ErrorType = row["ErrorType"]?.ToString() ?? "",
                        Source = row["Source"]?.ToString() ?? "",
                        Message = row["Message"]?.ToString() ?? "",
                        Count = Convert.ToInt32(row["Count"]),
                        FirstOccurrence = Convert.ToDateTime(row["FirstOccurrence"]),
                        LastOccurrence = Convert.ToDateTime(row["LastOccurrence"]),
                        AffectedUsers = row["AffectedUsers"]?.ToString()?.Split(',').ToList() ?? new List<string>()
                    });
                }
            }
            return Model_Dao_Result<List<Model_ErrorGrouping>>.Success(list);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<List<Model_ErrorGrouping>>.Failure(ex.Message);
        }
    }

    public async Task<Model_Dao_Result<Dictionary<DateTime, Model_LogStatistics>>> GetLogTimelineAsync(
        DateTime start, 
        DateTime end, 
        Enum_TimelineGranularity granularity = Enum_TimelineGranularity.Day)
    {
        try
        {
            var groupBy = granularity == Enum_TimelineGranularity.Hour ? "Hour" : "Day";
            var result = await _dao.GetLogTimelineAsync(start, end, groupBy);
            if (!result.IsSuccess) return Model_Dao_Result<Dictionary<DateTime, Model_LogStatistics>>.Failure(result.ErrorMessage);

            var dict = new Dictionary<DateTime, Model_LogStatistics>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    var time = Convert.ToDateTime(row["TimeBucket"]);
                    var stats = new Model_LogStatistics
                    {
                        ErrorCount = Convert.ToInt32(row["ErrorCount"]),
                        WarningCount = Convert.ToInt32(row["WarningCount"])
                    };
                    dict[time] = stats;
                }
            }
            return Model_Dao_Result<Dictionary<DateTime, Model_LogStatistics>>.Success(dict);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<Dictionary<DateTime, Model_LogStatistics>>.Failure(ex.Message);
        }
    }

    public async Task<Model_Dao_Result<List<string>>> GetDistinctValuesAsync(string field)
    {
        try
        {
            var result = await _dao.GetDistinctValuesAsync(field);
            if (!result.IsSuccess) return Model_Dao_Result<List<string>>.Failure(result.ErrorMessage);

            var list = new List<string>();
            if (result.Data != null)
            {
                foreach (DataRow row in result.Data.Rows)
                {
                    list.Add(row["Value"]?.ToString() ?? "");
                }
            }
            return Model_Dao_Result<List<string>>.Success(list);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<List<string>>.Failure(ex.Message);
        }
    }

    #endregion

    #region System Health

    public async Task<Model_Dao_Result<Model_SystemHealthStatus>> GetSystemHealthAsync()
    {
        try
        {
            var statsResult = await _dao.GetLogStatisticsAsync(DateTime.Now.AddHours(-1), DateTime.Now);
            if (!statsResult.IsSuccess)
            {
                return Model_Dao_Result<Model_SystemHealthStatus>.Failure(statsResult.ErrorMessage);
            }

            var errorCount = 0;
            var warningCount = 0;
            if (statsResult.Data != null && statsResult.Data.Rows.Count > 0)
            {
                errorCount = Convert.ToInt32(statsResult.Data.Rows[0]["ErrorCount"]);
                warningCount = Convert.ToInt32(statsResult.Data.Rows[0]["WarningCount"]);
            }

            var health = new Model_SystemHealthStatus
            {
                Timestamp = DateTime.Now,
                ErrorCount = errorCount,
                WarningCount = warningCount,
                Status = errorCount > 10 ? Enum_HealthIndicator.Red : (errorCount > 0 ? Enum_HealthIndicator.Yellow : Enum_HealthIndicator.Green),
                Message = errorCount > 10 ? "Critical Errors Detected" : (errorCount > 0 ? "Warnings Detected" : "System Healthy")
            };

            return Model_Dao_Result<Model_SystemHealthStatus>.Success(health);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<Model_SystemHealthStatus>.Failure(ex.Message);
        }
    }

    public async Task<Model_Dao_Result<Model_DatabaseHealth>> GetDatabaseHealthAsync()
    {
        try
        {
            var result = await _dao.GetDatabaseHealthAsync();
            if (!result.IsSuccess) return Model_Dao_Result<Model_DatabaseHealth>.Failure(result.ErrorMessage);

            var health = new Model_DatabaseHealth
            {
                IsConnected = true,
                StatusMessage = "Connected",
                LastSuccessfulQuery = DateTime.Now
            };

            // Get detailed stats
            var statsResult = await _dao.GetDatabaseStatsAsync();
            if (statsResult.IsSuccess && statsResult.Data != null && statsResult.Data.Rows.Count > 0)
            {
                var row = statsResult.Data.Rows[0];
                health.ConnectionCount = Convert.ToInt32(row["Threads_connected"]);
                health.UptimeSeconds = Convert.ToInt64(row["Uptime"]);
                health.BytesReceived = Convert.ToInt64(row["Bytes_received"]);
                health.BytesSent = Convert.ToInt64(row["Bytes_sent"]);
                health.OpenTables = Convert.ToInt32(row["Open_tables"]);
                health.Queries = Convert.ToInt32(row["Questions"]);
                health.Version = row["Version"].ToString() ?? "Unknown";
            }
            
            return Model_Dao_Result<Model_DatabaseHealth>.Success(health);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<Model_DatabaseHealth>.Failure(ex.Message);
        }
    }

    public Model_PerformanceMetrics GetPerformanceMetrics()
    {
        try
        {
            var process = Process.GetCurrentProcess();
            return new Model_PerformanceMetrics
            {
                Timestamp = DateTime.Now,
                CpuUsagePercent = 0,
                MemoryUsageMB = process.WorkingSet64 / 1024.0 / 1024.0,
                ThreadCount = process.Threads.Count,
                HandleCount = process.HandleCount,
                StartTime = process.StartTime
            };
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return new Model_PerformanceMetrics();
        }
    }

    #endregion

    #region User Feedback

    public async Task<Model_Dao_Result<DataTable>> GetUserFeedbackAsync(int userId)
    {
        try
        {
            return await _feedbackManager.GetUserSubmissionsAsync(userId);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<DataTable>.Failure(ex.Message);
        }
    }

    #endregion

    #region Export

    public async Task<Model_Dao_Result<bool>> ExportLogsAsync(
        List<Model_DevLogEntry> entries, 
        string filePath, 
        Enum_ExportFormat format)
    {
        try
        {
            var sb = new System.Text.StringBuilder();

            switch (format)
            {
                case Enum_ExportFormat.CSV:
                    sb.AppendLine("Timestamp,Level,Source,User,Message,Details");
                    foreach (var entry in entries)
                    {
                        sb.AppendLine($"{EscapeCsv(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"))},{EscapeCsv(entry.Level)},{EscapeCsv(entry.Source)},{EscapeCsv(entry.User)},{EscapeCsv(entry.Message)},{EscapeCsv(entry.Details)}");
                    }
                    break;

                case Enum_ExportFormat.JSON:
                    sb.AppendLine("[");
                    for (int i = 0; i < entries.Count; i++)
                    {
                        var entry = entries[i];
                        sb.AppendLine("  {");
                        sb.AppendLine($"    \"Timestamp\": \"{entry.Timestamp:yyyy-MM-dd HH:mm:ss}\",");
                        sb.AppendLine($"    \"Level\": \"{EscapeJson(entry.Level)}\",");
                        sb.AppendLine($"    \"Source\": \"{EscapeJson(entry.Source)}\",");
                        sb.AppendLine($"    \"User\": \"{EscapeJson(entry.User)}\",");
                        sb.AppendLine($"    \"Message\": \"{EscapeJson(entry.Message)}\",");
                        sb.AppendLine($"    \"Details\": \"{EscapeJson(entry.Details)}\"");
                        sb.Append("  }");
                        if (i < entries.Count - 1) sb.AppendLine(",");
                        else sb.AppendLine();
                    }
                    sb.AppendLine("]");
                    break;

                case Enum_ExportFormat.TXT:
                    foreach (var entry in entries)
                    {
                        sb.AppendLine($"[{entry.Timestamp:yyyy-MM-dd HH:mm:ss}] [{entry.Level}] [{entry.Source}] ({entry.User})");
                        sb.AppendLine(entry.Message);
                        if (!string.IsNullOrEmpty(entry.Details))
                        {
                            sb.AppendLine("Details:");
                            sb.AppendLine(entry.Details);
                        }
                        sb.AppendLine(new string('-', 80));
                    }
                    break;
            }

            await File.WriteAllTextAsync(filePath, sb.ToString());
            return Model_Dao_Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex);
            return Model_Dao_Result<bool>.Failure(ex.Message);
        }
    }

    #endregion

    #region Private Helpers

    private List<Model_DevLogEntry> MapDataTableToLogEntries(DataTable? dt)
    {
        var list = new List<Model_DevLogEntry>();
        if (dt == null) return list;

        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Model_DevLogEntry
            {
                Id = row.Table.Columns.Contains("Id") ? Convert.ToInt32(row["Id"]) : 0,
                Timestamp = row.Table.Columns.Contains("Timestamp") ? Convert.ToDateTime(row["Timestamp"]) : (row.Table.Columns.Contains("ErrorTime") ? Convert.ToDateTime(row["ErrorTime"]) : DateTime.MinValue),
                Level = row.Table.Columns.Contains("Level") ? row["Level"].ToString() ?? "" : (row.Table.Columns.Contains("Severity") ? row["Severity"].ToString() ?? "" : ""),
                Source = row.Table.Columns.Contains("Source") ? row["Source"].ToString() ?? "" : (row.Table.Columns.Contains("MethodName") ? row["MethodName"].ToString() ?? "" : ""),
                Message = row.Table.Columns.Contains("Message") ? row["Message"].ToString() ?? "" : (row.Table.Columns.Contains("ErrorMessage") ? row["ErrorMessage"].ToString() ?? "" : ""),
                Details = row.Table.Columns.Contains("Details") ? row["Details"].ToString() : null,
                User = row.Table.Columns.Contains("User") ? row["User"].ToString() : null,
                ErrorType = row.Table.Columns.Contains("ErrorType") ? row["ErrorType"].ToString() : null
            });
        }
        return list;
    }

    private string EscapeCsv(string? field)
    {
        if (string.IsNullOrEmpty(field)) return "";
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }

    private string EscapeJson(string? field)
    {
        if (string.IsNullOrEmpty(field)) return "";
        return field.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
    }

    #endregion
}
