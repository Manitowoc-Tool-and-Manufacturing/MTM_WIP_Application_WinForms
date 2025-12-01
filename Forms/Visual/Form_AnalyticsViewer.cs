using System.Data;
using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
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
                // Configure WebView2 to use a local user data folder to prevent file locking issues
                // when the application is run from a network share by multiple users.
                string userDataFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                    "MTM", 
                    "WebView2");
                
                var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
                await webView.EnsureCoreWebView2Async(env);
                
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Html", "UserAnalytics.html");
                
                if (File.Exists(htmlPath))
                {
                    // Use FileStream with FileShare.ReadWrite to allow concurrent reads
                    string htmlContent;
                    using (var fs = new FileStream(htmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var sr = new StreamReader(fs))
                    {
                        htmlContent = await sr.ReadToEndAsync();
                    }
                    
                    // Serialize data
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

                    // Inject data directly into HTML before navigation
                    // This avoids race conditions with DOMContentLoaded
                    htmlContent = htmlContent.Replace("// DATA_INJECTION_POINT", $"window.injectedData = {json};");

                    webView.NavigateToString(htmlContent);
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
        // Removed CoreWebView2_DOMContentLoaded as it is no longer needed
    }
}
