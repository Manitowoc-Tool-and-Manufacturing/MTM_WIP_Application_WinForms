using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;
using Newtonsoft.Json;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Form for displaying the User Analytics HTML report.
    /// </summary>
    public partial class Form_AnalyticsViewer : ThemedForm
    {
        private readonly DataTable _analyticsData;

        public Form_AnalyticsViewer(DataTable data)
        {
            InitializeComponent();
            _analyticsData = data;
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            try
            {
                await webView.EnsureCoreWebView2Async();
                
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Html", "UserAnalytics.html");
                
                if (File.Exists(htmlPath))
                {
                    string htmlContent = await File.ReadAllTextAsync(htmlPath);
                    webView.NavigateToString(htmlContent);
                    webView.CoreWebView2.DOMContentLoaded += CoreWebView2_DOMContentLoaded;
                }
                else
                {
                    Service_ErrorHandler.ShowError($"HTML template not found at: {htmlPath}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, callerName: nameof(InitializeWebView), controlName: this.Name);
            }
        }

        private async void CoreWebView2_DOMContentLoaded(object? sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            try
            {
                // Convert DataTable to list of objects for JSON serialization
                var dataList = new List<object>();
                foreach (DataRow row in _analyticsData.Rows)
                {
                    dataList.Add(new
                    {
                        User = row["User"],
                        Type = row["Type"],
                        Part = row["Part"],
                        Qty = row["Qty"],
                        Date = row["Date"],
                        FromLoc = row["FromLoc"] == DBNull.Value ? null : row["FromLoc"],
                        ToLoc = row["ToLoc"] == DBNull.Value ? null : row["ToLoc"],
                        WorkOrder = row["WorkOrder"] == DBNull.Value ? null : row["WorkOrder"]
                    });
                }

                string json = JsonConvert.SerializeObject(dataList);
                
                // Inject data into the WebView
                await webView.CoreWebView2.ExecuteScriptAsync($"window.injectedData = {json};");
                
                // Trigger chart initialization
                await webView.CoreWebView2.ExecuteScriptAsync("if(window.initChart) window.initChart();");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, callerName: nameof(CoreWebView2_DOMContentLoaded), controlName: this.Name);
            }
        }
    }
}
