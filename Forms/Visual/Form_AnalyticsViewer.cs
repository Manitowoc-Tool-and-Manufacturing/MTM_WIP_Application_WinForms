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
        private readonly object _analyticsData;
        private readonly string _htmlTemplateName;

        public Form_AnalyticsViewer(object data, string htmlTemplateName = "VisualUserAnalytics_Enhanced.html")
        {
            InitializeComponent();
            _analyticsData = data;
            _htmlTemplateName = htmlTemplateName;
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            try
            {
                await webView.EnsureCoreWebView2Async();
                
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Html", _htmlTemplateName);
                
                if (File.Exists(htmlPath))
                {
                    string htmlContent = await File.ReadAllTextAsync(htmlPath);
                    
                    // Pre-inject data if using the new template
                    if (_htmlTemplateName == "VisualUserAnalytics_Enhanced.html")
                    {
                        string json = GetSerializedData();
                        htmlContent = htmlContent.Replace("// DATA_INJECTION_POINT", $"window.injectedData = {json};");
                    }

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

        private string GetSerializedData()
        {
            if (_analyticsData is DataTable dt && _htmlTemplateName == "VisualUserAnalytics_Enhanced.html")
            {
                // Convert DataTable to list of objects for JSON serialization (Legacy support)
                var dataList = new List<object>();
                foreach (DataRow row in dt.Rows)
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
                return JsonConvert.SerializeObject(dataList);
            }
            
            // Generic serialization for new models
            return JsonConvert.SerializeObject(_analyticsData);
        }

        private async void CoreWebView2_DOMContentLoaded(object? sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            try
            {
                // Only handle legacy injection for WIPUserAnalytics
                if (_htmlTemplateName == "WIPUserAnalytics_Enhanced.html")
                {
                     string json = GetSerializedData();
                     await webView.CoreWebView2.ExecuteScriptAsync($"renderData({json});");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, callerName: nameof(CoreWebView2_DOMContentLoaded), controlName: this.Name);
            }
        }
    }
}
