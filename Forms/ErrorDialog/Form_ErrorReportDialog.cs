using System.Data;
using System.IO.Compression;
using System.Text;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.ViewLogs
{
    /// <summary>
    /// Dialog for displaying comprehensive error analysis reports.
    /// Implements T073, T074, T075 - Statistical Dashboard with progress, caching, and export.
    /// Migrated to ThemedForm for automatic DPI scaling and theme support.
    /// </summary>
    public partial class Form_ErrorReportDialog : ThemedForm
    {
        #region Fields

        private CancellationTokenSource? _cancellationTokenSource;
        private ErrorAnalysisReport? _cachedReport;
        private DateTime _cacheTimestamp = DateTime.MinValue;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromHours(1);
        private readonly string _logBasePath;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ErrorAnalysisReportDialog.
        /// </summary>
        public Form_ErrorReportDialog(string logBasePath)
        {
            InitializeComponent();
            _logBasePath = logBasePath ?? throw new ArgumentNullException(nameof(logBasePath));
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles form load event. Starts error analysis automatically.
        /// </summary>
        private async void ErrorAnalysisReportDialog_Load(object? sender, EventArgs e)
        {
            try
            {
                await GenerateReportAsync();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["Operation"] = "LoadDialog" },
                    controlName: nameof(Form_ErrorReportDialog));
            }
        }

        #endregion

        #region Button Click Events

        /// <summary>
        /// Handles Refresh button click. Regenerates the report.
        /// </summary>
        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                // Clear cache to force regeneration
                _cachedReport = null;
                _cacheTimestamp = DateTime.MinValue;

                await GenerateReportAsync();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object> { ["Operation"] = "Refresh" },
                    controlName: nameof(Form_ErrorReportDialog));
            }
        }

        /// <summary>
        /// Handles Cancel button click. Cancels ongoing analysis.
        /// </summary>
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            try
            {
                _cancellationTokenSource?.Cancel();
                btnCancel.Enabled = false;
                lblStatus.Text = "Cancelling...";
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Handles Close button click.
        /// </summary>
        private void BtnClose_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles Export HTML button click. T075: Export to styled HTML.
        /// </summary>
        private void BtnExportHtml_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_cachedReport == null)
                {
                    Service_ErrorHandler.ShowWarning("No report available to export.", "Export HTML");
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "HTML Files (*.html)|*.html",
                    FileName = $"ErrorAnalysisReport_{DateTime.Now:yyyyMMdd_HHmmss}.html",
                    Title = "Export Error Analysis Report as HTML"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportToHtml(_cachedReport, sfd.FileName);
                    Service_ErrorHandler.ShowInformation($"Report exported successfully to:\n{sfd.FileName}", "Export Complete");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object> { ["Operation"] = "ExportHtml" },
                    controlName: nameof(Form_ErrorReportDialog));
            }
        }

        /// <summary>
        /// Handles Export CSV button click. T075: Export to zipped CSV sections.
        /// </summary>
        private void BtnExportCsv_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_cachedReport == null)
                {
                    Service_ErrorHandler.ShowWarning("No report available to export.", "Export CSV");
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "ZIP Files (*.zip)|*.zip",
                    FileName = $"ErrorAnalysisReport_{DateTime.Now:yyyyMMdd_HHmmss}.zip",
                    Title = "Export Error Analysis Report as CSV (Zipped)"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportToCsv(_cachedReport, sfd.FileName);
                    Service_ErrorHandler.ShowInformation($"Report exported successfully to:\n{sfd.FileName}", "Export Complete");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object> { ["Operation"] = "ExportCsv" },
                    controlName: nameof(Form_ErrorReportDialog));
            }
        }

        /// <summary>
        /// Handles Copy to Clipboard button click. T075: Copy as plain text with ASCII tables.
        /// </summary>
        private void BtnCopyToClipboard_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_cachedReport == null)
                {
                    Service_ErrorHandler.ShowWarning("No report available to copy.", "Copy to Clipboard");
                    return;
                }

                string plainText = FormatReportAsPlainText(_cachedReport);
                Clipboard.SetText(plainText);
                Service_ErrorHandler.ShowInformation("Report copied to clipboard as plain text.", "Copy Complete");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object> { ["Operation"] = "CopyClipboard" },
                    controlName: nameof(Form_ErrorReportDialog));
            }
        }

        #endregion

        #region Report Generation

        /// <summary>
        /// T073, T074: Generates error analysis report with progress and caching.
        /// </summary>
        private async Task GenerateReportAsync()
        {
            // T074: Check cache first (1-hour expiration)
            if (_cachedReport != null && DateTime.Now - _cacheTimestamp < _cacheExpiration)
            {
                // Check if any log files were modified since cache was created
                if (!await HasLogFilesModifiedSinceCacheAsync())
                {
                    DisplayReport(_cachedReport);
                    lblStatus.Text = $"Using cached report (generated {_cacheTimestamp:g})";
                    return;
                }
            }

            // T074: Create cancellation token
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            try
            {
                // T074: Show progress bar and enable cancel button
                progressBar.Visible = true;
                progressBar.Value = 0;
                btnCancel.Enabled = true;
                btnRefresh.Enabled = false;
                btnExportHtml.Enabled = false;
                btnExportCsv.Enabled = false;
                btnCopyToClipboard.Enabled = false;
                lblStatus.Text = "Analyzing logs...";

                var report = await Task.Run(() => AnalyzeLogs(token), token);

                if (token.IsCancellationRequested)
                {
                    lblStatus.Text = "Analysis cancelled";
                    return;
                }

                // T074: Cache the report
                _cachedReport = report;
                _cacheTimestamp = DateTime.Now;

                DisplayReport(report);
                lblStatus.Text = $"Analysis complete. {report.TotalErrorsAnalyzed} errors analyzed.";
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Analysis cancelled by user";
            }
            catch (Exception)
            {
                lblStatus.Text = "Analysis failed";
                throw;
            }
            finally
            {
                progressBar.Visible = false;
                btnCancel.Enabled = false;
                btnRefresh.Enabled = true;
                btnExportHtml.Enabled = _cachedReport != null;
                btnExportCsv.Enabled = _cachedReport != null;
                btnCopyToClipboard.Enabled = _cachedReport != null;
            }
        }

        /// <summary>
        /// T074: Checks if any log files were modified since cache was created.
        /// </summary>
        private async Task<bool> HasLogFilesModifiedSinceCacheAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var allLogFiles = Directory.GetFiles(_logBasePath, "*.csv", SearchOption.AllDirectories);
                    return allLogFiles.Any(f => File.GetLastWriteTime(f) > _cacheTimestamp);
                }
                catch
                {
                    return true; // Assume modified on error
                }
            });
        }

        /// <summary>
        /// T073: Analyzes all log files and generates comprehensive report.
        /// </summary>
        private ErrorAnalysisReport AnalyzeLogs(CancellationToken token)
        {
            var report = new ErrorAnalysisReport();
            var allLogFiles = Directory.GetFiles(_logBasePath, "*.csv", SearchOption.AllDirectories);
            var errorFrequency = new Dictionary<string, ErrorInfo>();
            var dailyErrorCounts = new Dictionary<DateTime, int>();
            var promptFiles = GetPromptFiles();

            int processedFiles = 0;

            foreach (var logFile in allLogFiles)
            {
                token.ThrowIfCancellationRequested();

                // T074: Update progress
                processedFiles++;
                UpdateProgress(processedFiles, allLogFiles.Length, $"Processing file {processedFiles} of {allLogFiles.Length}");

                ProcessLogFile(logFile, errorFrequency, dailyErrorCounts, token);
            }

            // T073: Calculate most frequent errors (top 10)
            report.MostFrequentErrors = errorFrequency
                .OrderByDescending(kvp => kvp.Value.Count)
                .Take(10)
                .Select(kvp => kvp.Value)
                .ToList();

            // T073: Calculate error trends (last 30 days)
            var thirtyDaysAgo = DateTime.Now.AddDays(-30).Date;
            report.ErrorTrends = dailyErrorCounts
                .Where(kvp => kvp.Key >= thirtyDaysAgo)
                .OrderBy(kvp => kvp.Key)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // T073: Calculate prompt coverage statistics
            int uniqueErrors = errorFrequency.Count;
            int errorsWithPrompts = errorFrequency.Count(kvp => promptFiles.Contains(kvp.Key));
            report.TotalUniqueErrors = uniqueErrors;
            report.ErrorsWithPrompts = errorsWithPrompts;
            report.ErrorsWithoutPrompts = uniqueErrors - errorsWithPrompts;
            report.PromptCoveragePercent = uniqueErrors > 0 ? (errorsWithPrompts * 100.0 / uniqueErrors) : 0;

            // T073: Priority recommendations (errors without prompts, sorted by frequency)
            report.PriorityRecommendations = errorFrequency
                .Where(kvp => !promptFiles.Contains(kvp.Key))
                .OrderByDescending(kvp => kvp.Value.Count)
                .Select(kvp => kvp.Value)
                .ToList();

            report.TotalErrorsAnalyzed = errorFrequency.Sum(kvp => kvp.Value.Count);
            report.GeneratedAt = DateTime.Now;

            return report;
        }

        /// <summary>
        /// Processes a single log file and updates error statistics.
        /// </summary>
        private void ProcessLogFile(string filePath, Dictionary<string, ErrorInfo> errorFrequency,
            Dictionary<DateTime, int> dailyErrorCounts, CancellationToken token)
        {
            try
            {
                // Load and parse entries using existing services
                var rawEntries = Service_LogFileReader.LoadEntriesAsync(filePath, 0, 10000).Result;
                var fileDate = File.GetLastWriteTime(filePath).Date;

                // Detect log format from file name
                string fileName = Path.GetFileName(filePath).ToLower();
                Model_LogFormat logFormat = Model_LogFormat.Normal;
                if (fileName.Contains("application"))
                    logFormat = Model_LogFormat.ApplicationError;
                else if (fileName.Contains("database"))
                    logFormat = Model_LogFormat.DatabaseError;

                foreach (var rawEntry in rawEntries)
                {
                    token.ThrowIfCancellationRequested();

                    var entry = Service_LogParser.ParseEntry(rawEntry, logFormat);

                    if (entry.Level != "ERROR" && entry.Level != "CRITICAL")
                        continue;

                    // Extract error type and method
                    string errorType = ExtractErrorType(entry);
                    string methodName = ExtractMethodName(entry);
                    string errorKey = $"{errorType}_{methodName}";

                    if (!errorFrequency.ContainsKey(errorKey))
                    {
                        errorFrequency[errorKey] = new ErrorInfo
                        {
                            ErrorType = errorType,
                            MethodName = methodName,
                            ErrorKey = errorKey,
                            Count = 0,
                            FirstSeen = entry.Timestamp,
                            LastSeen = entry.Timestamp
                        };
                    }

                    var errorInfo = errorFrequency[errorKey];
                    errorInfo.Count++;
                    if (entry.Timestamp < errorInfo.FirstSeen) errorInfo.FirstSeen = entry.Timestamp;
                    if (entry.Timestamp > errorInfo.LastSeen) errorInfo.LastSeen = entry.Timestamp;

                    // Track daily counts
                    if (!dailyErrorCounts.ContainsKey(fileDate))
                        dailyErrorCounts[fileDate] = 0;
                    dailyErrorCounts[fileDate]++;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Extracts error type from log entry.
        /// </summary>
        private string ExtractErrorType(Model_LogEntry entry)
        {
            string text = (entry.Message ?? "") + " " + (entry.Details ?? "");

            var exceptionTypes = new[] {
                "NullReferenceException", "ArgumentNullException", "ArgumentException",
                "InvalidOperationException", "TimeoutException", "SqlException",
                "FileNotFoundException", "DirectoryNotFoundException", "IOException",
                "UnauthorizedAccessException", "FormatException", "IndexOutOfRangeException",
                "ArgumentOutOfRangeException", "ObjectDisposedException", "NotImplementedException"
            };

            foreach (var exType in exceptionTypes)
            {
                if (text.Contains(exType, StringComparison.OrdinalIgnoreCase))
                    return exType;
            }

            var words = (entry.Message ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words.Length > 0 ? words[0] : "UnknownError";
        }

        /// <summary>
        /// Extracts method name from log entry.
        /// </summary>
        private string ExtractMethodName(Model_LogEntry entry)
        {
            if (!string.IsNullOrWhiteSpace(entry.Source))
            {
                var parts = entry.Source.Split('.', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                    return parts[^1];
                return entry.Source;
            }

            if (!string.IsNullOrWhiteSpace(entry.Details))
            {
                var lines = entry.Details.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Contains(" at ") && line.Contains("MTM_"))
                    {
                        int atIndex = line.IndexOf(" at ");
                        if (atIndex >= 0)
                        {
                            string methodPart = line.Substring(atIndex + 4);
                            int parenIndex = methodPart.IndexOf('(');
                            if (parenIndex > 0)
                            {
                                methodPart = methodPart.Substring(0, parenIndex).Trim();
                                var parts = methodPart.Split('.', StringSplitOptions.RemoveEmptyEntries);
                                if (parts.Length > 0)
                                    return parts[^1];
                            }
                        }
                    }
                }
            }

            return "UnknownMethod";
        }

        /// <summary>
        /// Gets list of existing prompt file keys.
        /// </summary>
        private HashSet<string> GetPromptFiles()
        {
            var promptFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                string promptDir = Helper_LogPath.GetPromptFixesDirectory();
                if (Directory.Exists(promptDir))
                {
                    var files = Directory.GetFiles(promptDir, "Prompt_Fix_*.md");
                    foreach (var file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string methodName = fileName.Replace("Prompt_Fix_", "");
                        promptFiles.Add(methodName);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }

            return promptFiles;
        }

        /// <summary>
        /// T074: Updates progress bar and status label on UI thread.
        /// </summary>
        private void UpdateProgress(int current, int total, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress(current, total, message)));
                return;
            }

            int percent = total > 0 ? (current * 100 / total) : 0;
            progressBar.Value = Math.Min(percent, 100);
            lblStatus.Text = message;
        }

        #endregion

        #region Display Report

        /// <summary>
        /// Displays the generated report in the UI.
        /// </summary>
        private void DisplayReport(ErrorAnalysisReport report)
        {
            var sb = new StringBuilder();

            sb.AppendLine("=" + new string('=', 78));
            sb.AppendLine("                    ERROR ANALYSIS REPORT");
            sb.AppendLine("=" + new string('=', 78));
            sb.AppendLine($"Generated: {report.GeneratedAt:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Total Errors Analyzed: {report.TotalErrorsAnalyzed:N0}");
            sb.AppendLine($"Unique Error Types: {report.TotalUniqueErrors}");
            sb.AppendLine();

            // Most Frequent Errors
            sb.AppendLine("TOP 10 MOST FREQUENT ERRORS");
            sb.AppendLine(new string('-', 79));
            sb.AppendLine($"{"#",-3} {"Error Type",-30} {"Method",-25} {"Count",10} {"%",6}");
            sb.AppendLine(new string('-', 79));

            int rank = 1;
            foreach (var error in report.MostFrequentErrors)
            {
                double percent = report.TotalErrorsAnalyzed > 0
                    ? (error.Count * 100.0 / report.TotalErrorsAnalyzed)
                    : 0;
                sb.AppendLine($"{rank,-3} {TruncateString(error.ErrorType, 30),-30} {TruncateString(error.MethodName, 25),-25} {error.Count,10:N0} {percent,5:F1}%");
                rank++;
            }
            sb.AppendLine();

            // Prompt Coverage
            sb.AppendLine("PROMPT COVERAGE STATISTICS");
            sb.AppendLine(new string('-', 79));
            sb.AppendLine($"Total Unique Errors:        {report.TotalUniqueErrors,10}");
            sb.AppendLine($"With Prompts:               {report.ErrorsWithPrompts,10} ({report.PromptCoveragePercent:F1}%)");
            sb.AppendLine($"Without Prompts:            {report.ErrorsWithoutPrompts,10} ({100 - report.PromptCoveragePercent:F1}%)");
            sb.AppendLine();

            // Priority Recommendations
            sb.AppendLine("PRIORITY RECOMMENDATIONS (Errors Without Prompts)");
            sb.AppendLine(new string('-', 79));
            sb.AppendLine($"{"Error Type",-30} {"Method",-25} {"Count",10} {"First",12} {"Last",12}");
            sb.AppendLine(new string('-', 79));

            foreach (var error in report.PriorityRecommendations.Take(15))
            {
                sb.AppendLine($"{TruncateString(error.ErrorType, 30),-30} {TruncateString(error.MethodName, 25),-25} {error.Count,10:N0} {error.FirstSeen:MM/dd HH:mm} {error.LastSeen:MM/dd HH:mm}");
            }

            if (report.PriorityRecommendations.Count > 15)
            {
                sb.AppendLine($"... and {report.PriorityRecommendations.Count - 15} more");
            }
            sb.AppendLine();

            // Error Trends (last 30 days)
            sb.AppendLine("ERROR TRENDS (Last 30 Days)");
            sb.AppendLine(new string('-', 79));

            if (report.ErrorTrends.Any())
            {
                int maxCount = report.ErrorTrends.Values.Max();
                const int barWidth = 50;

                foreach (var trend in report.ErrorTrends.OrderBy(kvp => kvp.Key))
                {
                    int barLength = maxCount > 0 ? (trend.Value * barWidth / maxCount) : 0;
                    string bar = new string('█', barLength);
                    sb.AppendLine($"{trend.Key:MM/dd} {trend.Value,6:N0} {bar}");
                }
            }
            else
            {
                sb.AppendLine("No error trend data available.");
            }

            txtReport.Text = sb.ToString();
        }

        #endregion

        #region Export Methods

        /// <summary>
        /// T075: Exports report to HTML with embedded CSS styling.
        /// </summary>
        private void ExportToHtml(ErrorAnalysisReport report, string filePath)
        {
            var html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html><head>");
            html.AppendLine("<meta charset='UTF-8'>");
            html.AppendLine("<title>Error Analysis Report</title>");
            html.AppendLine("<style>");
            html.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; margin: 20px; background: #f5f5f5; }");
            html.AppendLine("h1 { color: #2c3e50; border-bottom: 3px solid #3498db; padding-bottom: 10px; }");
            html.AppendLine("h2 { color: #34495e; margin-top: 30px; border-bottom: 2px solid #95a5a6; padding-bottom: 5px; }");
            html.AppendLine("table { width: 100%; border-collapse: collapse; margin: 15px 0; background: white; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }");
            html.AppendLine("th { background: #3498db; color: white; padding: 12px; text-align: left; }");
            html.AppendLine("td { padding: 10px; border-bottom: 1px solid #ecf0f1; }");
            html.AppendLine("tr:hover { background: #f8f9fa; }");
            html.AppendLine(".stat { background: white; padding: 15px; margin: 10px 0; border-left: 4px solid #3498db; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }");
            html.AppendLine(".chart { margin: 20px 0; }");
            html.AppendLine(".bar { height: 20px; background: #3498db; margin: 2px 0; display: inline-block; }");
            html.AppendLine("</style>");
            html.AppendLine("</head><body>");

            html.AppendLine("<h1>Error Analysis Report</h1>");
            html.AppendLine($"<p>Generated: {report.GeneratedAt:yyyy-MM-dd HH:mm:ss}</p>");

            html.AppendLine("<div class='stat'>");
            html.AppendLine($"<p><strong>Total Errors Analyzed:</strong> {report.TotalErrorsAnalyzed:N0}</p>");
            html.AppendLine($"<p><strong>Unique Error Types:</strong> {report.TotalUniqueErrors}</p>");
            html.AppendLine($"<p><strong>Prompt Coverage:</strong> {report.PromptCoveragePercent:F1}% ({report.ErrorsWithPrompts}/{report.TotalUniqueErrors})</p>");
            html.AppendLine("</div>");

            // Most Frequent Errors
            html.AppendLine("<h2>Top 10 Most Frequent Errors</h2>");
            html.AppendLine("<table><tr><th>#</th><th>Error Type</th><th>Method</th><th>Count</th><th>Percentage</th></tr>");
            int rank = 1;
            foreach (var error in report.MostFrequentErrors)
            {
                double percent = report.TotalErrorsAnalyzed > 0 ? (error.Count * 100.0 / report.TotalErrorsAnalyzed) : 0;
                html.AppendLine($"<tr><td>{rank}</td><td>{error.ErrorType}</td><td>{error.MethodName}</td><td>{error.Count:N0}</td><td>{percent:F1}%</td></tr>");
                rank++;
            }
            html.AppendLine("</table>");

            // Priority Recommendations
            html.AppendLine("<h2>Priority Recommendations (Errors Without Prompts)</h2>");
            html.AppendLine("<table><tr><th>Error Type</th><th>Method</th><th>Count</th><th>First Seen</th><th>Last Seen</th></tr>");
            foreach (var error in report.PriorityRecommendations.Take(20))
            {
                html.AppendLine($"<tr><td>{error.ErrorType}</td><td>{error.MethodName}</td><td>{error.Count:N0}</td><td>{error.FirstSeen:yyyy-MM-dd HH:mm}</td><td>{error.LastSeen:yyyy-MM-dd HH:mm}</td></tr>");
            }
            html.AppendLine("</table>");

            html.AppendLine("</body></html>");

            File.WriteAllText(filePath, html.ToString());
        }

        /// <summary>
        /// T075: Exports report to CSV files in a ZIP archive.
        /// </summary>
        private void ExportToCsv(ErrorAnalysisReport report, string zipFilePath)
        {
            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                // Most Frequent Errors CSV
                var frequentEntry = archive.CreateEntry("MostFrequentErrors.csv");
                using (var writer = new StreamWriter(frequentEntry.Open()))
                {
                    writer.WriteLine("Rank,ErrorType,Method,Count,Percentage");
                    int rank = 1;
                    foreach (var error in report.MostFrequentErrors)
                    {
                        double percent = report.TotalErrorsAnalyzed > 0 ? (error.Count * 100.0 / report.TotalErrorsAnalyzed) : 0;
                        writer.WriteLine($"{rank},\"{error.ErrorType}\",\"{error.MethodName}\",{error.Count},{percent:F2}");
                        rank++;
                    }
                }

                // Priority Recommendations CSV
                var priorityEntry = archive.CreateEntry("PriorityRecommendations.csv");
                using (var writer = new StreamWriter(priorityEntry.Open()))
                {
                    writer.WriteLine("ErrorType,Method,Count,FirstSeen,LastSeen");
                    foreach (var error in report.PriorityRecommendations)
                    {
                        writer.WriteLine($"\"{error.ErrorType}\",\"{error.MethodName}\",{error.Count},\"{error.FirstSeen:yyyy-MM-dd HH:mm:ss}\",\"{error.LastSeen:yyyy-MM-dd HH:mm:ss}\"");
                    }
                }

                // Error Trends CSV
                var trendsEntry = archive.CreateEntry("ErrorTrends.csv");
                using (var writer = new StreamWriter(trendsEntry.Open()))
                {
                    writer.WriteLine("Date,ErrorCount");
                    foreach (var trend in report.ErrorTrends.OrderBy(kvp => kvp.Key))
                    {
                        writer.WriteLine($"{trend.Key:yyyy-MM-dd},{trend.Value}");
                    }
                }

                // Summary CSV
                var summaryEntry = archive.CreateEntry("Summary.csv");
                using (var writer = new StreamWriter(summaryEntry.Open()))
                {
                    writer.WriteLine("Metric,Value");
                    writer.WriteLine($"GeneratedAt,\"{report.GeneratedAt:yyyy-MM-dd HH:mm:ss}\"");
                    writer.WriteLine($"TotalErrorsAnalyzed,{report.TotalErrorsAnalyzed}");
                    writer.WriteLine($"UniqueErrorTypes,{report.TotalUniqueErrors}");
                    writer.WriteLine($"ErrorsWithPrompts,{report.ErrorsWithPrompts}");
                    writer.WriteLine($"ErrorsWithoutPrompts,{report.ErrorsWithoutPrompts}");
                    writer.WriteLine($"PromptCoveragePercent,{report.PromptCoveragePercent:F2}");
                }
            }

            File.WriteAllBytes(zipFilePath, memoryStream.ToArray());
        }

        /// <summary>
        /// T075: Formats report as plain text with ASCII tables for clipboard.
        /// </summary>
        private string FormatReportAsPlainText(ErrorAnalysisReport report)
        {
            return txtReport.Text; // Already formatted as plain text in DisplayReport
        }

        #endregion

        #region Helper Methods

        private string TruncateString(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input ?? "";
            return input.Substring(0, maxLength - 3) + "...";
        }

        #endregion

    }

    #region Report Model Classes

    /// <summary>
    /// Model representing the complete error analysis report.
    /// </summary>
    internal class ErrorAnalysisReport
    {
        public List<ErrorInfo> MostFrequentErrors { get; set; } = new();
        public Dictionary<DateTime, int> ErrorTrends { get; set; } = new();
        public List<ErrorInfo> PriorityRecommendations { get; set; } = new();
        public int TotalErrorsAnalyzed { get; set; }
        public int TotalUniqueErrors { get; set; }
        public int ErrorsWithPrompts { get; set; }
        public int ErrorsWithoutPrompts { get; set; }
        public double PromptCoveragePercent { get; set; }
        public DateTime GeneratedAt { get; set; }
    }

    /// <summary>
    /// Model representing information about a specific error.
    /// </summary>
    internal class ErrorInfo
    {
        public string ErrorType { get; set; } = "";
        public string MethodName { get; set; } = "";
        public string ErrorKey { get; set; } = "";
        public int Count { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }

    #endregion
}
